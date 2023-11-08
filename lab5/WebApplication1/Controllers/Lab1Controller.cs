using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class Lab1Controller : Controller
    {
        public ActionResult Lab1()
        {
            return View(new Lab1ViewModel());
        }

        [HttpPost]
        public ActionResult Lab1(Lab1ViewModel model)
        {
            if (ModelState.IsValid)
            {
                string pathToInputFile = Server.MapPath("~/App_Data/Lab1Input.txt");
                string pathToOutputFile = Server.MapPath("~/App_Data/Lab1Output.txt");

                System.IO.File.WriteAllText(pathToInputFile, model.InputData);

                LabsLibrary.LabWork.RunLab1(pathToInputFile, pathToOutputFile);

                model.Result = System.IO.File.ReadAllText(pathToOutputFile);
            }

            return View(model);
        }
    }

}
