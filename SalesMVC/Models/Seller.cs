using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesMVC.Models
{
    public class Seller
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Display(Name="Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double Salary { get; set; }

        public Department Department { get; set; }
        public long DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(long id, string name, string email, DateTime birthDate, double salary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Salary = salary;
            Department = department;
        }

        public void AddSales(SalesRecord saleRecord) {
            Sales.Add(saleRecord);
        }
        public void RemoveSales(SalesRecord saleRecord)
        {
            Sales.Remove(saleRecord);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales
                .Where(s => s.Date <= initial && s.Date <= final)
                .Select(s => s.Amount)
                .Sum();
        }
    }
}
