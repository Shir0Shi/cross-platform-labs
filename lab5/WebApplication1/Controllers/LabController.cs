using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LabController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public LabController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        private ActionResult LabGeneric(int labNumber, LabViewModel model, bool isPost)
        {
            string pathToInputFile = Path.Combine(_environment.ContentRootPath, $"App_Data/Lab{labNumber}Input.txt");
            string pathToOutputFile = Path.Combine(_environment.ContentRootPath, $"App_Data/Lab{labNumber}Output.txt");

            if (isPost)
            {
                System.IO.File.WriteAllText(pathToInputFile, model.InputData);

                switch (labNumber)
                {
                    case 1:
                        ConsoleApp1.Program.RunLab1(pathToInputFile, pathToOutputFile);
                        
                        break;
                    case 2:
                        ConsoleApp2.Program.RunLab2(pathToInputFile, pathToOutputFile);
                        break;
                    case 3:
                        ConsoleApp3.Program.RunLab3(pathToInputFile, pathToOutputFile);
                        break;
                    default:
                        throw new ArgumentException("Invalid lab number");
                }
                if (System.IO.File.Exists(pathToOutputFile))
                {
                    model.Result = System.IO.File.ReadAllText(pathToOutputFile);
                }
                else
                {
                    model.Result = "Error - file with result not found.";
                }
            }
            else
            {
                if (System.IO.File.Exists(pathToInputFile))
                {
                    model.InputData = System.IO.File.ReadAllText(pathToInputFile);
                }

            }

            ViewBag.LabNumber = labNumber;
            return View("Lab", model);
        }

        public ActionResult Lab1()
        {
            return LabGeneric(1, new LabViewModel(), isPost: false);
        }

        [HttpPost]
        public ActionResult Lab1(LabViewModel model)
        {
            return LabGeneric(1, model, isPost: true);
        }

        public ActionResult Lab2()
        {
            return LabGeneric(2, new LabViewModel(), isPost: false);
        }

        [HttpPost]
        public ActionResult Lab2(LabViewModel model)
        {
            return LabGeneric(2, model, isPost: true);
        }

        public ActionResult Lab3()
        {
            return LabGeneric(3, new LabViewModel(), isPost: false);
        }

        [HttpPost]
        public ActionResult Lab3(LabViewModel model)
        {
            return LabGeneric(3, model, isPost: true);
        }

    }

}
