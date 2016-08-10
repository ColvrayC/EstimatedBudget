using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.Models
{
    [Table("Registration")]
    public partial class Registration : ICloneable
    {
        public Registration()
        {
        }

        [System.ComponentModel.DataAnnotations.Key]
        public int? Id { get; set; }

        public string Wording { get; set; }
        public string Description { get; set; }
        public DateTime DateR { get; set; }
        public decimal Price { get; set; }
        public int B_Code { get; set; }
        public Nullable<int> C_Id { get; set; }
        public int? T_Id { get; set; }

        public virtual BankAccount BankAccount { get; set; }
        public virtual Category Category { get; set; }
        public virtual Transfer Transfer { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
