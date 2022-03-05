using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransportWorkshopUserAuth.Models
{
    public class TypeAuto                  //Тип автомобиля
    {
        //[Key]
        [Display(Name = "Тип Авто")]
        public int TypeAutoId { get; set; }

        [Required(ErrorMessage = "Укажите наименование автомобиля")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Название должно быть от 8 до 50 символов")]
        [Display(Name = "Наименование")]
        public string NameType { get; set; }

        public ICollection<AutoCar> AutoCars { get; set; }
    }
}
