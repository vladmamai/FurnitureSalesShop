using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class EmployeeUser
    {
        [Column("userId")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("login")]
        [StringLength(50, ErrorMessage = "Максимально допустима кількість символів - 50")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("password")]
        [StringLength(50, ErrorMessage = "Максимально допустима кількість символів - 50")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("employeeId")]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EmployeeUser")]
        public virtual Employee Employee { get; set; }
    }
}
