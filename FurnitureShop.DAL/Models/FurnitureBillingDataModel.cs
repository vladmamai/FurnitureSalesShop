using System.ComponentModel.DataAnnotations;

namespace FurnitureShopApp.DAL.Models
{
    public class FurnitureBillingDataModel
    {
        public int CheckDetailsId { get; set; }
        public int CheckId { get; set; }

        public int FurnitureId { get; set; }

        public string FurnitureName { get; set; }

        public string Color { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        public string FurnitureNameWithColor { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Range(0,int.MaxValue, ErrorMessage="Значення не може бути від'ємним")]
        public int QuantitySelected { get; set; }

        public decimal RetailPrice { get; set; }
    }
}
