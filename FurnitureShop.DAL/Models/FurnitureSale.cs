using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class FurnitureSale
    {
        public FurnitureSale()
        {
            CheckDetails = new HashSet<CheckDetails>();
            Delivery = new HashSet<Delivery>();
        }

        [Column("checkId")]
        public int CheckId { get; set; }

        [Column("customerId")]
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("employeeId")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("buyingDate", TypeName = "datetime")]
        public DateTime BuyingDate { get; set; }

        [Column("totalPrice", TypeName = "money")]
        [Range(0, double.MaxValue, ErrorMessage = "Значення не може бути від'ємним числом!")]
        public decimal TotalPrice { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("FurnitureSale")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("FurnitureSale")]
        public virtual Employee Employee { get; set; }

        [InverseProperty("Check")]
        public virtual ICollection<CheckDetails> CheckDetails { get; set; }

        [InverseProperty("Check")]
        public virtual ICollection<Delivery> Delivery { get; set; }
    }
}
