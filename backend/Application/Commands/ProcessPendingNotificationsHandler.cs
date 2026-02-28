using System;
using System.Linq;
using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Lab.Api.Application.Commands
{
    public class ProcessPendingNotificationsHandler : ICommandHandler<ProcessPendingNotificationsCommand, int>
    {
        private readonly LabDbContext _db;
        public ProcessPendingNotificationsHandler(LabDbContext db) => _db = db;

        public async Task<int> Handle(ProcessPendingNotificationsCommand command)
        {
            var now = DateTime.UtcNow;
            var pending = await _db.NotificacionesCita
                .Where(n => !n.Enviado && !n.Eliminado && n.ProximoIntento <= now && n.IntentosEnvio < n.MaxIntentos)
                .OrderBy(n => n.ProximoIntento)
                .Take(command.MaxItems)
                .ToListAsync();

            var processed = 0;

            foreach (var n in pending)
            {
                try
                {
                    // Simular envío (aquí integrarías SMTP/Push/etc.)
                    // Para prototipo, consideramos éxito si destinatario contiene '@'
                    bool success = !string.IsNullOrEmpty(n.Destinatario) && n.Destinatario.Contains("@");

                    n.IntentosEnvio += 1;
                    if (success)
                    {
                        n.Enviado = true;
                        n.MensajeError = null;
                    }
                    else
                    {
                        n.MensajeError = "Destino no válido";
                        // backoff exponencial: now + 2^attempts minutes
                        var minutes = (int)Math.Pow(2, n.IntentosEnvio);
                        n.ProximoIntento = DateTime.UtcNow.AddMinutes(minutes);
                    }

                    n.FechaModificacion = DateTime.UtcNow;
                    _db.NotificacionesCita.Update(n);
                    processed++;
                }
                catch (Exception ex)
                {
                    n.IntentosEnvio += 1;
                    n.MensajeError = ex.Message;
                    n.ProximoIntento = DateTime.UtcNow.AddMinutes(5);
                    n.FechaModificacion = DateTime.UtcNow;
                    _db.NotificacionesCita.Update(n);
                }
            }

            await _db.SaveChangesAsync();
            return processed;
        }
    }
}
