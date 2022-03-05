using System;
using System.ComponentModel.DataAnnotations;

namespace TransportWorkshopUserAuth.Models
{
    public class Maintenance                 //Техобслуживание
    {
        //[Key]
        [Display(Name = "Техобслуживание")]
        public int MaintenanceId { get; set; }

        [Required(ErrorMessage = "Укажите тип технического осмотра")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Название должно быть от 3 до 10 символов")]
        [Display(Name = "Тип ТО")]
        public string TypeTO { get; set; }

        [Display(Name = "Машины")]
        public int AutoCarId { get; set; }
        [Display(Name = "Машины")]
        public AutoCar AutoCar { get; set; }

        [Required(ErrorMessage = "Ведите дату ТО")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата ТО")]
        public DateTime DateTO { get; set; }


    }
}
