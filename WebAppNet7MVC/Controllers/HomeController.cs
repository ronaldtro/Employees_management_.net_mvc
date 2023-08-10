using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using WebAppNet7MVC.models;
using WebAppNet7MVC.repositorio.contrato;


namespace WebAppNet7MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Departamento> _departamentoRepositorio;
        private readonly IGenericRepository<Empleado> _empleadoRepositorio;


        public HomeController(ILogger<HomeController> logger, IGenericRepository<Departamento> departamentoRepositorio, IGenericRepository<Empleado> empleadoRepositorio)
        {
            _logger = logger;
            _departamentoRepositorio = departamentoRepositorio;
            _empleadoRepositorio = empleadoRepositorio;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> listaDepartamentos()
        {
            List<Departamento> _lista = await _departamentoRepositorio.Lista();

            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpGet]
        public async Task<IActionResult> listaEmpleados()
        {
            List<Empleado> _lista = await _empleadoRepositorio.Lista();

            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]
        public async Task<IActionResult> guardarEmpleado([FromBody] Empleado modelo)
        {
            bool _resultado = await _empleadoRepositorio.Guardar(modelo);

            if (_resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "Error" });
            }

        }

        [HttpPut]
        public async Task<IActionResult> editarEmpleado([FromBody] Empleado modelo)
        {
            bool _resultado = await _empleadoRepositorio.Editar(modelo);

            if (_resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "Error" });
            }

        }

        [HttpDelete]
        public async Task<IActionResult> eliminarEmpleado(int idEmpleado)
        {
            bool _resultado = await _empleadoRepositorio.Eliminar(idEmpleado);

            if (_resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "Error" });
            }

        }


        public IActionResult Privacy()
        {
            return View();
        }

    }
}