using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransportWorkshopUserAuth.Models
{
    public class WinterTime                  //Зимнее время
    {
        //[Key]
        [Display(Name = "Зимнее время")]
        public int WinterTimeId { get; set; }

        [Range(1, 100, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Display(Name = "Зимняя норма")]
        public int WinterNorma { get; set; }

        [Required(ErrorMessage = "Укажите период")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Начало периода")]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "Укажите период")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Конец периода")]
        public DateTime DateEnd { get; set; }

        [Required(ErrorMessage = "Укажите температуру")]
        //[Range(typeof(double), "-40.00", "60.00",ErrorMessage = "Значение{0} должно быть в пределах от {1} до {2}")]
        [Range(-40.000, 60.000, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Display(Name = "Температура С")]
        public double Temperature { get; set; }

        public ICollection<Device> Devices { get; set; }
    }
}
