using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransportWorkshopUserAuth.Models
{
    public class NormaFuel                            //Норма топлива
    {
        //[Key]
        [Display(Name = "Норма")]
        public int NormaFuelId { get; set; }

        [Required(ErrorMessage = "Укажите линйную норму")]
        [Range(0.01, 100.00, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        //[Range(0, 100, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Display(Name = "Линейная")]
        public double Linear { get; set; }

        [Required(ErrorMessage = "Укажите норму лето")]
        //[Range(0, 100, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Range(0.01, 100.00, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Display(Name = "Лето")]
        public double Summer { get; set; }

        [Required(ErrorMessage = "Укажите норму зима")]
        [Range(0.01, 100.00, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        //[Range(0, 100, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Display(Name = "Зима")]
        public double Winter { get; set; }

        public ICollection<AutoCar> AutoCars { get; set; }
    }
}
