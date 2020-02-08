using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class FurnitureShop
    {
        public FurnitureShop()
        {
            Employee = new HashSet<Employee>();
            Storage = new HashSet<Storage>();
        }

        [Column("shopId")]
        public int ShopId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("shopAddress")]
        [StringLength(100, ErrorMessage = "Максимально допустима кількість символів - 100")]
        public string ShopAddress { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("shopEmail")]
        [StringLength(30, ErrorMessage = "Максимально допустима кількість символів - 30")]
        public string ShopEmail { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("shopPhoneNum")]
        [StringLength(20, ErrorMessage = "Максимально допустима кількість символів - 20")]
        public string ShopPhoneNum { get; set; }

        [InverseProperty("Shop")]
        public virtual ICollection<Employee> Employee { get; set; }

        [InverseProperty("Shop")]
        public virtual ICollection<Storage> Storage { get; set; }
    }
}
