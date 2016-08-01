using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.Models
{
    [Table("Category")]
    public partial class Category : ICloneable
    {
        public Category()
        {
        }

        [System.ComponentModel.DataAnnotations.Key]
        public Nullable<int> Id { get; set; }

        public string Wording { get; set; }

        public decimal Targetprice { get; set; }
        public int B_Code { get; set; }
        public override string ToString()
        {
            return Wording;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
