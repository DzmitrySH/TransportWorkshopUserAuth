using System;
using System.ComponentModel.DataAnnotations;

namespace TransportWorkshopUserAuth.Models
{
    public class Balance
    {
        //[Key]
        [Display(Name = "Остаток")]
        public int BalanceId { get; set; }

        [Required(ErrorMessage = "Ведите дату")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "На Дату")]
        public DateTime Date { get; set; }

        [Display(Name = "Машины")]
        public int AutoCarId { get; set; }
        [Display(Name = "Машины")]
        public AutoCar AutoCar { get; set; }

        [Range(0, 1000, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Display(Name = "Остаток")]
        public int Leftovers { get; set; }

        [Range(0, 1000, ErrorMessage = "Значение должно быть в пределах от {1} до {2}")]
        [Display(Name = "СУГ")]
        public int Sug { get; set; }


    }
}
