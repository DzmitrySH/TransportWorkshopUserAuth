using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransportWorkshopUserAuth.Models
{
    public class Trailer                           //Прицепы
    {
        //[Key]
        [Display(Name = "Прицепы")]
        public int TrailerId { get; set; }

        [Required(ErrorMessage = "Укажите номер прицепа")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Номер должен быть от 3 до 20 символов")]
        [Display(Name = "Прицеп №")]
        public string Number { get; set; }

        [Range(1, 30, ErrorMessage = "Недопустимый диапазон массы")]
        [Display(Name = "Масса")]
        public int Massa { get; set; }

        [Display(Name = "Покрышки")]
        public int TireId { get; set; }
        [Display(Name = "Покрышки")]
        public Tire Tire { get; set; }

        public ICollection<AutoCar> AutoCars { get; set; }
        //public ICollection<Transport> Transports { get; set; }
        //public ICollection<Tire> Tires { get; set; }
    }
}
