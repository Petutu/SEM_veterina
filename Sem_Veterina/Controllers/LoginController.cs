using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sem_Veterina.CRUD;
using Sem_Veterina.Entity;
using Sem_Veterina.Models;

namespace Sem_Veterina.Controllers
{

    public class LoginController : Controller
    {
        private readonly UzivatelService _uzivatelService;
        private readonly RoleService _roleService;
        private readonly PersonalService _personalService;
        private readonly MajitelService _majitelService;

        public LoginController(
            UzivatelService uzivatelService,
            RoleService roleService,
            PersonalService personalService,
            MajitelService majitelService
        )
        {
            _uzivatelService = uzivatelService;
            _roleService = roleService;
            _personalService = personalService;
            _majitelService = majitelService;
        }

        public IActionResult Index()
        {
            // var viewModel = new LoginViewModel
            // {
            // };
            // return View(viewModel);
            return View();
        }

        // [HttpPost]
        // public IActionResult Register(string Username, string Heslo)
        // {

        // }
    }
}