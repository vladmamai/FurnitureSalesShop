using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class FurnitureInStorage
    {
        public FurnitureInStorage()
        {
            CheckDetails = new HashSet<CheckDetails>();
        }

        [Column("furnitureId")]
        public int FurnitureId { get; set; }

        [Column("catalogId")]
        public int CatalogId { get; set; }

        [Column("storageId")]
        public int StorageId { get; set; }

        public string FurnitureNameWithColor
        {
            get
            {
                try
                {
                    return Catalog.FurnitureName + " (" + Catalog.Color + ")";
                }
                catch (NullReferenceException ex)
                {
                    return "null name 'FurnitureNameWithColor' ";
                }
            }
        }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("wholesalePrice", TypeName = "money")]
        [Range(0, double.MaxValue, ErrorMessage = "Ціна не може бути від'ємною!")]
        public decimal WholesalePrice { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("retailPrice", TypeName = "money")]
        [Range(0, double.MaxValue, ErrorMessage = "Ціна не може бути від'ємною!")]
        public decimal RetailPrice { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("quantityInStorage")]
        [Range(0, int.MaxValue, ErrorMessage = "Кількість одиниць не може бути від'ємним числом!")]
        public int QuantityInStorage { get; set; }

        [ForeignKey("CatalogId")]
        [InverseProperty("FurnitureInStorage")]
        public virtual Furniture Catalog { get; set; }

        [ForeignKey("StorageId")]
        [InverseProperty("FurnitureInStorage")]
        public virtual Storage Storage { get; set; }

        [InverseProperty("Furniture")]
        public virtual ICollection<CheckDetails> CheckDetails { get; set; }
    }
}
