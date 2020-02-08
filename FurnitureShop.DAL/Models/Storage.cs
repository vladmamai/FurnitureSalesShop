using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class Storage
    {
        public Storage()
        {
            FurnitureInStorage = new HashSet<FurnitureInStorage>();
        }

        [Column("storageId")]
        public int StorageId { get; set; }

        [Column("shopId")]
        public int ShopId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("storageAddress")]
        [StringLength(100, ErrorMessage = "Максимально допустима кількість символів - 100")]
        public string StorageAddress { get; set; }

        [ForeignKey("ShopId")]
        [InverseProperty("Storage")]
        public virtual FurnitureShop Shop { get; set; }

        [InverseProperty("Storage")]
        public virtual ICollection<FurnitureInStorage> FurnitureInStorage { get; set; }
    }
}
