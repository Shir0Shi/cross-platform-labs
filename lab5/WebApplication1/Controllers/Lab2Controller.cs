using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Lab2Controller : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public Lab2Controller(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public ActionResult Lab2()
        {
            return View(new LabViewModel());
        }

        [HttpPost]
        public ActionResult Lab2(LabViewModel model)
        {
            if (ModelState.IsValid)
            {
                string pathToInputFile = Path.Combine(_environment.ContentRootPath, "App_Data", "Lab2Input.txt");
                string pathToOutputFile = Path.Combine(_environment.ContentRootPath, "App_Data", "Lab2Output.txt");

                System.IO.File.WriteAllText(pathToInputFile, model.InputData);

                LabsLibrary.LabWork.RunLab2(pathToInputFile, pathToOutputFile);

                model.Result = System.IO.File.ReadAllText(pathToOutputFile);
            }

            return View(model);
        }
    }
}
