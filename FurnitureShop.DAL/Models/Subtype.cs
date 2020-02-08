using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class Subtype
    {
        public Subtype()
        {
            Furniture = new HashSet<Furniture>();
        }

        [Column("subtypeId")]
        public int SubtypeId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("subtypeName")]
        [StringLength(50, ErrorMessage = "Максимально допустима кількість символів - 50")]
        public string SubtypeName { get; set; }

        [Column("typeId")]
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        [InverseProperty("Subtype")]
        public virtual Type Type { get; set; }

        [InverseProperty("Subtype")]
        public virtual ICollection<Furniture> Furniture { get; set; }
    }
}
