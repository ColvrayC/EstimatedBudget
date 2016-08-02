using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.Models
{
    [Table("Frequency")]
    public partial class Frequency : ICloneable
    {
        public Frequency()
        {
            this.Transfer = new HashSet<Transfer>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public string Code { get; set; }
        public string Wording { get; set; }

        public virtual ICollection<Transfer> Transfer { get; set; }

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
