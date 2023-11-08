using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Lab1ViewModel
    {
        [Required(ErrorMessage = "Введення даних є обов'язковим")]
        public string InputData { get; set; }
        public string Result { get; set; }
    }

}
