using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransportWorkshopUserAuth.Models
{
    public class TypeFuel                 //Тип топлива
    {
        [Display(Name = "Тип топлива")]
        public int TypeFuelId { get; set; }

        [Required(ErrorMessage = "Укажите тип топлива")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Тип должен быть от 3 до 20 символов")]
        [Display(Name = "Тип топлива")]
        public string Fuel { get; set; }

        [Required(ErrorMessage = "Укажите стоимость топлива")]
        //[Range(typeof(double), "0.00", "100.00",ErrorMessage = "Значение{0} должно быть в пределах от {1} до {2}")]
        [Range(0.01, 100.00, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Display(Name = "Стоимость")]
        public double Cost { get; set; }

        [Required(ErrorMessage = "Укажите дату")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "На Дату")]
        public DateTime ToDate { get; set; }

        //   public ICollection<Balance> Balances { get; set; }
        public ICollection<AutoCar> AutoCars { get; set; }
        public ICollection<Device> Devices { get; set; }

    }
}
