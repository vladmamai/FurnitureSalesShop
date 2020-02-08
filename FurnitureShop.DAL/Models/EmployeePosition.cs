using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class EmployeePosition
    {
        public EmployeePosition()
        {
            Employee = new HashSet<Employee>();
        }

        [Column("positionId")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("positionName")]
        [StringLength(30, ErrorMessage = "Максимально допустима кількість символів - 30")]
        public string PositionName { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("salary", TypeName = "money")]
        [Range(0, double.MaxValue, ErrorMessage = "Значення не може бути від'ємним числом!")]
        public decimal Salary { get; set; }

        [InverseProperty("Position")]
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
