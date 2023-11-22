using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Lab1Controller : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public Lab1Controller(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        //[Route("Lab1")]
        public ActionResult Lab1()
        {
            return View(new LabViewModel());
        }

        [HttpPost]
        //[Route("Lab1")]
        public ActionResult Lab1(LabViewModel model)
        {
            if (ModelState.IsValid)
            {

                string pathToInputFile = Path.Combine(_environment.WebRootPath, "App_Data", "Lab1Input.txt");
                string pathToOutputFile = Path.Combine(_environment.WebRootPath, "App_Data", "Lab1Output.txt");

                System.IO.File.WriteAllText(pathToInputFile, model.InputData);

                LabsLibrary.LabWork.RunLab1(pathToInputFile, pathToOutputFile);

                model.Result = System.IO.File.ReadAllText(pathToOutputFile);
            }

            return View(model);
        }
    }
}
