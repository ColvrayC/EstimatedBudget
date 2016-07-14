using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.Models
{
    public partial class Mode
    {
        public Mode()
        {
            this.Levy = new HashSet<Levy>();
        }

        [Dapper.Key]
        public string Code { get; set; }
        public string Wording { get; set; }

        public virtual ICollection<Levy> Levy { get; set; }
    }
}
