using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class Furniture
    {
        public Furniture()
        {
            FurnitureInStorage = new HashSet<FurnitureInStorage>();
        }

        [Column("catalogId")]
        public int CatalogId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("furnitureName")]
        [StringLength(50, ErrorMessage = "Максимально допустима кількість символів - 50")]
        public string FurnitureName { get; set; }

        public string FurnitureNameWithColor { get { return FurnitureName + " (" + Color + ")"; }  }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("subtypeId")]
        public int SubtypeId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("companyId")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("material")]
        [StringLength(20, ErrorMessage = "Максимально допустима кількість символів - 20")]
        public string Material { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("color")]
        [StringLength(30, ErrorMessage = "Максимально допустима кількість символів - 30")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("guarantee")]
        [StringLength(20, ErrorMessage = "Максимально допустима кількість символів - 20")]
        public string Guarantee { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [ForeignKey("CompanyId")]
        [InverseProperty("Furniture")]
        public virtual CompanyDeveloper Company { get; set; }

        [ForeignKey("SubtypeId")]
        [InverseProperty("Furniture")]
        public virtual Subtype Subtype { get; set; }

        [InverseProperty("Catalog")]
        public virtual ICollection<FurnitureInStorage> FurnitureInStorage { get; set; }
    }
}
