using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransportWorkshopUserAuth.Models
{
    public class AutoCar                //Автомобили
    {
        //[Key]
        public int AutoCarId { get; set; }

        [Required(ErrorMessage = "Укажите название автомобиля")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Недопустимое название должено быть 3 до 30 символов")]
        [Display(Name = "Машина")]
        public string NameAuto { get; set; }

        [Required(ErrorMessage = "Укажите номер автомобиля")]
        [StringLength(12, MinimumLength = 5, ErrorMessage = "Недопустимый номер должен быть 5 до 12 символов")]
        [Display(Name = "Номер")]
        public string Number { get; set; }

        [Range(0, 10000000, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Display(Name = "Пробег")]
        public int Mileage { get; set; }

        [Display(Name = "Тип топлива")]
        public int TypeFuelId { get; set; }
        [Display(Name = "Тип топлива")]
        public TypeFuel TypeFuel { get; set; }

        [Display(Name = "Норма")]
        public int NormaFuelId { get; set; }
        [Display(Name = "Норма")]
        public NormaFuel NormaFuel { get; set; }

        [Display(Name = "Прицепы")]
        public int TrailerId { get; set; }//int?
        [Display(Name = "Прицепы")]
        public Trailer Trailer { get; set; }

        [Display(Name = "Водитель")]
        public int DriverId { get; set; }
        [Display(Name = "Водитель")]
        public Driver Driver { get; set; }

        [Display(Name = "Тип Авто")]
        public int TypeAutoId { get; set; }
        [Display(Name = "Тип Авто")]
        public TypeAuto TypeAuto { get; set; }

        [Display(Name = "Покрышки")]
        public int TireId { get; set; }
        [Display(Name = "Покрышки")]
        public Tire Tire { get; set; }

        [Range(0, 65535, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Display(Name = "Формула Вредности")]
        public int Harmfulness { get; set; }

        [Display(Name = "Навигация")]
        public bool Navigation { get; set; }

        [Display(Name = "Инжектр")]
        public bool Injector { get; set; }

        //   public ICollection<Device> Devices { get; set; }
        //   public ICollection<Tire> Tires { get; set; }
        //    public ICollection<Transport> Transports { get; set; }
        public ICollection<Balance> Balances { get; set; }
        public ICollection<Maintenance> Maintenances { get; set; }

    }
}
