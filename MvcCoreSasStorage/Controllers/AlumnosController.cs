using Microsoft.AspNetCore.Mvc;
using MvcCoreSasStorage.Models;
using MvcCoreSasStorage.Services;

namespace MvcCoreSasStorage.Controllers
{
    public class AlumnosController : Controller
    {
        private ServiceStorageAlumnosXML service;

        public AlumnosController(ServiceStorageAlumnosXML service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            List<Alumno> alumnos = this.service.MostrarAlumnosXML();
            return View(alumnos);
        }

        public async Task<IActionResult> Create()
        {
            List<Alumno> listalumno = this.service.MostrarAlumnosXML();
            await this.service.InsertarAlumnos(listalumno);
            return RedirectToAction("Index");
        }
    }
}
