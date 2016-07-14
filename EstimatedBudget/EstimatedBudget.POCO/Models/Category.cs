using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.Models
{
    public partial class Category
    {
        public Category()
        {
        }

        [Dapper.Key]
        public int Code { get; set; }

        public string Wording { get; set; }

        public decimal TargetPrice { get; set; }
    }
}
