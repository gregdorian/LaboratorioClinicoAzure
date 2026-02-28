using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Domain.Entities;
using Lab.Api.Infrastructure;

namespace Lab.Api.Application.Commands
{
    public class EnqueueNotificationHandler : ICommandHandler<EnqueueNotificationCommand, long>
    {
        private readonly LabDbContext _db;
        public EnqueueNotificationHandler(LabDbContext db) => _db = db;

        public async Task<long> Handle(EnqueueNotificationCommand command)
        {
            var n = new NotificacionCita
            {
                IdPaciente = command.IdPaciente,
                IdCita = command.IdCita,
                TipoNotif = command.TipoNotif,
                Destinatario = command.Destinatario,
                Asunto = command.Asunto,
                Cuerpo = command.Cuerpo,
                Enviado = false,
                IntentosEnvio = 0,
                ProximoIntento = System.DateTime.UtcNow,
                MaxIntentos = 5
            };
            _db.NotificacionesCita.Add(n);
            await _db.SaveChangesAsync();
            return n.IdNotificacion;
        }
    }
}
