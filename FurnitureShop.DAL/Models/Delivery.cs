using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class Delivery
    {
        [Column("deliveryId")]
        public int DeliveryId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("checkId")]
        public int CheckId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("address")]
        [StringLength(100, ErrorMessage = "Максимальна допустима кількість символів - 100")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("shippingDate", TypeName = "datetime")]
        public DateTime ShippingDate { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("deliveryDate", TypeName = "datetime")]
        public DateTime DeliveryDate { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("deliveryPrice", TypeName = "money")]
        [Range(0, double.MaxValue, ErrorMessage = "Ціна не може бути від'ємною!")]
        [DataType(DataType.Currency)]
        public decimal DeliveryPrice { get; set; }

        [ForeignKey("CheckId")]
        [InverseProperty("Delivery")]
        public virtual FurnitureSale Check { get; set; }
    }
}
