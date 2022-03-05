using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransportWorkshopUserAuth.Models
{
    public class Driver                  //Водители
    {
        [Display(Name = "Водитель")]
        public int DriverId { get; set; }

        [Required(ErrorMessage = "Укажите Фамилию Имя Отчество")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Ф.И.О. должна быть от 3 до 30 символов")]
        [Display(Name = "Ф.И.О")]
        public string FirsLastMidName { get; set; }

        [Required(ErrorMessage = "Укажите категорию водителя ")]
        [Range(1, 5, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Display(Name = "Категория")]
        public int Category { get; set; }

        [Required(ErrorMessage = "Укажите права водителя")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Права должны быть от 3 до 15 символов")]
        [Display(Name = "Права №")]
        public string RightsNumber { get; set; }

        public ICollection<AutoCar> AutoCars { get; set; }
        //   public ICollection<Balance> Balances { get; set; }
    }
}
