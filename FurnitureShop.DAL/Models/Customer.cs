using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class Customer
    {
        public Customer()
        {
            FurnitureSale = new HashSet<FurnitureSale>();
        }

        [Column("customerId")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("custName")]
        [StringLength(30, ErrorMessage = "Максимальна допустима кількість символів - 30")]
        public string CustName { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("custSurname")]
        [StringLength(30, ErrorMessage = "Максимальна допустима кількість символів - 30")]
        public string CustSurname { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("empSurname")]
        [StringLength(60, ErrorMessage = "Максимальна допустима кількість символів - 60")]
        public string CustFullName { get { return CustName + " " + CustSurname; } }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("custEmail")]
        [StringLength(30, ErrorMessage = "Максимальна допустима кількість символів - 30")]
        public string CustEmail { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("custPhoneNum")]
        [StringLength(20, ErrorMessage = "Максимальна допустима кількість символів - 20")]
        public string CustPhoneNum { get; set; }

        [Column("discount")]
        [Range(0, 0.5, ErrorMessage = "Знижка може бути в діапазоні лише від 0 до 0.5")]
        public double? Discount { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<FurnitureSale> FurnitureSale { get; set; }
    }
}
