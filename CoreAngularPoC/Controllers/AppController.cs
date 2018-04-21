using CoreAngularPoC.Services;
using CoreAngularPoC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreAngularPoC.Controllers
{
    public class AppController : Controller
    {
        private readonly IEmailService _mailServer;

        public AppController(IEmailService mailServer)
        {
            _mailServer = mailServer;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailServer.SendMessage("me@yahoo.com", model.Subject, $"From: {model.Name} - {model.Email} , Message : {model.Message}");

                ViewBag.UserMessage = "Mail Send!";

                ModelState.Clear();
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }
    }
}