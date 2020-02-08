using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FurnitureShopApp.DAL.Models
{
    [DataContract]
    public partial class CheckDetails
    {
        [DataMember]
        [Column("checkDetailsId")]
        public int CheckDetailsId { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("quantitySelected")]    
        [Range(1, int.MaxValue, ErrorMessage = "Кількість не може бути від'ємним числом або рівна нулю")]
        public int QuantitySelected { get; set; }

        [DataMember]
        [Column("checkId")]
        public int? CheckId { get; set; }

        [DataMember]
        [Column("furnitureId")]
        public int FurnitureId { get; set; }

        [DataMember]
        [ForeignKey("CheckId")]
        [InverseProperty("CheckDetails")]
        public virtual FurnitureSale Check { get; set; }

        [DataMember]
        [ForeignKey("FurnitureId")]
        [InverseProperty("CheckDetails")]
        public virtual FurnitureInStorage Furniture { get; set; }
    }
}
