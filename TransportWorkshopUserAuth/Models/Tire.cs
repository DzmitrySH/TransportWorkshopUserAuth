using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransportWorkshopUserAuth.Models
{
    public class Tire                  //покрышки
    {
        //[Key]
        [Display(Name = "Покрышки")]
        public int TireId { get; set; }

        [Required(ErrorMessage = "Укажите номиналы покрышки")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Номинал должен быть от 3 до 20 символов")]
        [Display(Name = "Номинал")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите марку покрышки")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Бренд должен быть от 5 до 20 символов")]
        [Display(Name = "Марка")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Ведите дату начала пробега")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Range(0, 1000, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Display(Name = "Начало пробега")]
        public int RunStart { get; set; }

        //public int DeviceId { get; set; }
        //public int TrailerId { get; set; }
        //public int TransportId { get; set; }
        //public Transport Transport { get; set; }
        //public Device Device { get; set; }
        //public Trailer Trailer { get; set; }
        public ICollection<AutoCar> AutoCars { get; set; }
        public ICollection<Device> Devices { get; set; }
        public ICollection<Trailer> Trailers { get; set; }
        //public ICollection<Transport> Transports { get; set; }

    }
}
