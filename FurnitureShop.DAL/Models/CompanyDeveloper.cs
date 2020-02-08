using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class CompanyDeveloper
    {
        public CompanyDeveloper()
        {
            Furniture = new HashSet<Furniture>();
        }

        [Column("companyId")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("comName")]
        [StringLength(30)]
        public string ComName { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("comAddress")]
        [StringLength(100, ErrorMessage = "Максимально допустима кількість символів - 100")]
        public string ComAddress { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("comEmail")]
        [StringLength(30, ErrorMessage = "Максимально допустима кількість символів - 30")]
        public string ComEmail { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("comPhoneNum")]
        [StringLength(20, ErrorMessage = "Максимально допустима кількість символів - 20")]
        public string ComPhoneNum { get; set; }

        [InverseProperty("Company")]
        public virtual ICollection<Furniture> Furniture { get; set; }
    }
}
