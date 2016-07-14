using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.Models
{
    public partial class Registration
    {
        public Registration()
        {
        }

        [Dapper.Key]
        public string Id { get; set; }

        public string Wording { get; set; }
        public string Description { get; set; }
        public DateTime DateR { get; set; }
        public decimal Price { get; set; }
        public bool Way { get; set; }
        public int B_Code { get; set; }
        public string C_Code { get; set; }
        public int L_Id { get; set; }

        public virtual BankAccount BankAccount { get; set; }
        public virtual Category Category { get; set; }
        public virtual Levy Levy { get; set; }
    }
}
