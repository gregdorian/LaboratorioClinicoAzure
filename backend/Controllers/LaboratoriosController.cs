using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Lab.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class LaboratoriosController : ControllerBase
    {
        /// <summary>
        /// GET api/laboratorios - Listar laboratorios disponibles
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            // Datos hardcodeados basados en el frontend (sample-analysis-hub)
            var laboratorios = new List<LaboratorioDto>
            {
                new LaboratorioDto
                {
                    Id = "lab1",
                    Nombre = "Laboratorio Central",
                    Direccion = "Calle Principal 123",
                    Telefono = "+57 1 2345678",
                    Email = "central@laboratorio.com",
                    Ciudad = "Bogotá",
                    Horario = "07:00 - 17:00",
                    Examenes = new List<string> { "Hemograma Completo", "Glucosa en Ayunas", "Perfil Lipídico", "Examen General de Orina" }
                },
                new LaboratorioDto
                {
                    Id = "lab2",
                    Nombre = "Laboratorio Norte",
                    Direccion = "Carrera 5 No. 100",
                    Telefono = "+57 1 9876543",
                    Email = "norte@laboratorio.com",
                    Ciudad = "Bogotá",
                    Horario = "08:00 - 18:00",
                    Examenes = new List<string> { "Urocultivo", "Prueba de Tiroides (TSH)" }
                },
                new LaboratorioDto
                {
                    Id = "lab3",
                    Nombre = "Laboratorio Sur",
                    Direccion = "Diagonal 10 No. 50",
                    Telefono = "+57 1 5555555",
                    Email = "sur@laboratorio.com",
                    Ciudad = "Bogotá",
                    Horario = "07:00 - 17:00",
                    Examenes = new List<string> { "Hemoglobina Glicosilada", "Perfil Hepático" }
                }
            };

            return Ok(laboratorios);
        }

        /// <summary>
        /// GET api/laboratorios/{id} - Obtener información de un laboratorio específico
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var laboratorios = new Dictionary<string, LaboratorioDto>
            {
                {
                    "lab1", new LaboratorioDto
                    {
                        Id = "lab1",
                        Nombre = "Laboratorio Central",
                        Direccion = "Calle Principal 123",
                        Telefono = "+57 1 2345678",
                        Email = "central@laboratorio.com",
                        Ciudad = "Bogotá",
                        Horario = "07:00 - 17:00",
                        Examenes = new List<string> { "Hemograma Completo", "Glucosa en Ayunas", "Perfil Lipídico", "Examen General de Orina" }
                    }
                },
                {
                    "lab2", new LaboratorioDto
                    {
                        Id = "lab2",
                        Nombre = "Laboratorio Norte",
                        Direccion = "Carrera 5 No. 100",
                        Telefono = "+57 1 9876543",
                        Email = "norte@laboratorio.com",
                        Ciudad = "Bogotá",
                        Horario = "08:00 - 18:00",
                        Examenes = new List<string> { "Urocultivo", "Prueba de Tiroides (TSH)" }
                    }
                },
                {
                    "lab3", new LaboratorioDto
                    {
                        Id = "lab3",
                        Nombre = "Laboratorio Sur",
                        Direccion = "Diagonal 10 No. 50",
                        Telefono = "+57 1 5555555",
                        Email = "sur@laboratorio.com",
                        Ciudad = "Bogotá",
                        Horario = "07:00 - 17:00",
                        Examenes = new List<string> { "Hemoglobina Glicosilada", "Perfil Hepático" }
                    }
                }
            };

            if (laboratorios.TryGetValue(id, out var laboratorio))
                return Ok(laboratorio);

            return NotFound();
        }

        /// <summary>
        /// GET api/laboratorios/{id}/examenes - Obtener exámenes disponibles en un laboratorio
        /// </summary>
        [HttpGet("{id}/examenes")]
        public IActionResult GetExamenes(string id)
        {
            var examenesPorLab = new Dictionary<string, List<string>>
            {
                { "lab1", new List<string> { "Hemograma Completo", "Glucosa en Ayunas", "Perfil Lipídico", "Examen General de Orina" } },
                { "lab2", new List<string> { "Urocultivo", "Prueba de Tiroides (TSH)" } },
                { "lab3", new List<string> { "Hemoglobina Glicosilada", "Perfil Hepático" } }
            };

            if (examenesPorLab.TryGetValue(id, out var examenes))
                return Ok(examenes);

            return NotFound();
        }
    }
}
