using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShopApp.DAL.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeUser = new HashSet<EmployeeUser>();
            FurnitureSale = new HashSet<FurnitureSale>();
        }

        [Column("employeeId")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("empName")]
        [StringLength(30, ErrorMessage = "Максимально допустима кількість символів - 30")]
        public string EmpName { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("empSurname")]
        [StringLength(30, ErrorMessage = "Максимально допустима кількість символів - 30")]
        public string EmpSurname { get; set; }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("empSurname")]
        [StringLength(60, ErrorMessage = "Максимально допустима кількість символів - 60")]
        public string EmpFullName { get { return EmpName + " " + EmpSurname; } }

        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("passport")]
        [StringLength(20, ErrorMessage = "Максимально допустима кількість символів - 20")]
        public string Passport { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Це поле є обов'язковим до заповнення")]
        [Column("employmentDate", TypeName = "datetime")]
        public DateTime EmploymentDate { get; set; }

        [Column("positionId")]
        public int PositionId { get; set; }

        [Column("shopId")]
        public int ShopId { get; set; }

        [ForeignKey("PositionId")]
        [InverseProperty("Employee")]
        public virtual EmployeePosition Position { get; set; }
        [ForeignKey("ShopId")]
        [InverseProperty("Employee")]
        public virtual FurnitureShop Shop { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeUser> EmployeeUser { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<FurnitureSale> FurnitureSale { get; set; }
    }
}
