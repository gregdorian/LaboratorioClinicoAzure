using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.DTOs;
using Lab.Api.Infrastructure;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Lab.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MuestrasController : ControllerBase
    {
        private readonly LabDbContext _db;

        public MuestrasController(LabDbContext db) => _db = db;

        /// <summary>
        /// GET api/muestras - Listar todas las muestras en recepción/análisis
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var muestras = _db.ExamenMuestra
                .Select(m => new MuestraDto
                {
                    IdMuestra = m.IdExamenMuestra,
                    IdSolicitud = m.IdExamenSolicitado,
                    IdPaciente = 0,
                    TipoMuestra = "Muestra Estándar",
                    AreaAnalisis = "Por Asignar",
                    Prioridad = "Normal",
                    FechaRecepcion = m.FechaToma,
                    Estado = "Recibida"
                })
                .ToList();

            return Ok(muestras);
        }

        /// <summary>
        /// GET api/muestras/{id} - Obtener muestra específica
        /// </summary>
        [HttpGet("{id:long}")]
        public IActionResult Get(long id)
        {
            var muestra = _db.ExamenMuestra
                .Where(m => m.IdExamenMuestra == id)
                .Select(m => new MuestraDto
                {
                    IdMuestra = m.IdExamenMuestra,
                    IdSolicitud = m.IdExamenSolicitado,
                    IdPaciente = 0,
                    TipoMuestra = "Muestra Estándar",
                    AreaAnalisis = "Por Asignar",
                    Prioridad = "Normal",
                    FechaRecepcion = m.FechaToma,
                    Estado = "Recibida"
                })
                .FirstOrDefault();

            if (muestra == null) return NotFound();
            return Ok(muestra);
        }

        /// <summary>
        /// POST api/muestras/recepcion - Registrar recepción de muestra
        /// </summary>
        [HttpPost("recepcion")]
        public IActionResult Recepcion([FromBody] RecepcionMuestraDto recepcion)
        {
            if (recepcion == null) return BadRequest("Datos de muestra requeridos");

            try
            {
                // En una implementación real, esto crearía ExamenMuestra
                // Por ahora, retornamos un mensaje de éxito simulado
                return Ok(new 
                { 
                    IdMuestra = new Random().Next(1000, 9999), 
                    Mensaje = "Muestra recibida exitosamente" 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensaje = "Error al registrar muestra: " + ex.Message });
            }
        }

        /// <summary>
        /// PUT api/muestras/{id}/analisis - Registrar análisis de muestra
        /// </summary>
        [HttpPut("{id:long}/analisis")]
        public IActionResult RegistrarAnalisis(long id, [FromBody] AnalisisMuestraDto analisis)
        {
            if (analisis == null) return BadRequest("Datos de análisis requeridos");

            var muestra = _db.ExamenMuestra.FirstOrDefault(m => m.IdExamenMuestra == id);
            if (muestra == null) return NotFound();

            try
            {
                // Crear registro de resultado
                var resultado = new Domain.Entities.Resultado
                {
                    IdExamenMuestra = id,
                    IdExamenSolicitado = muestra.IdExamenSolicitado,
                    IdPrueba = 1,
                    IdPersonalLaboratorio = 1,
                    ResultadoValor = analisis.Resultado,
                    ResultadoTexto = analisis.Observaciones,
                    Unidad = "N/A",
                    EstaFueraRango = false,
                    EsValidado = false,
                    FechaResultado = DateTime.UtcNow
                };

                _db.Resultados.Add(resultado);
                _db.SaveChanges();

                return Ok(new { IdResultado = resultado.IdResultado, Mensaje = "Análisis registrado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensaje = "Error al registrar análisis: " + ex.Message });
            }
        }
    }
}
