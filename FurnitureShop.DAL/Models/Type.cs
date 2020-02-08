using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class Type
    {
        public Type()
        {
            Subtype = new HashSet<Subtype>();
        }

        [Column("typeId")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("typeName")]
        [StringLength(30, ErrorMessage = "Максимально допустима кількість символів - 30")]
        public string TypeName { get; set; }

        [InverseProperty("Type")]
        public virtual ICollection<Subtype> Subtype { get; set; }
    }
}
