using System;
using System.ComponentModel.DataAnnotations;

namespace TransportWorkshopUserAuth.Models
{
    public class Device                  //Устройства
    {
        //[Key]
        [Display(Name = "Устройства")]
        public int DeviceId { get; set; }

        [Required(ErrorMessage = "Укажите название устройства")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Название должно быть от 3 до 30 символов")]
        [Display(Name = "Название устройства")]
        public string Namedevice { get; set; }

        [Display(Name = "Тип топлива")]
        public int TypeFuelId { get; set; }
        [Display(Name = "Тип топлива")]
        public TypeFuel TypeFuel { get; set; }

        [Required(ErrorMessage = "Ведите начало летнего периода")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Летнее время")]
        public DateTime SumerTime { get; set; }

        [Display(Name = "Зимнее время")]
        public int WinterTimeId { get; set; }
        [Display(Name = "Зимнее время")]
        public WinterTime WinterTime { get; set; }

        [Display(Name = "Вредность")]
        public bool Harmfulness { get; set; }

        [Display(Name = "Покрышки")]
        public int TireId { get; set; }
        [Display(Name = "Покрышки")]
        public Tire Tire { get; set; }

        //public ICollection<Transport> Transports { get; set; }
        //public int AutoCarId { get; set; }
        //public AutoCar AutoCar { get; set; }
    }
}
