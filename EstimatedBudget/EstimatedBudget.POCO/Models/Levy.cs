using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.Models
{
    public partial class Levy
    {
        public Levy()
        {
            this.Registration = new HashSet<Registration>();
        }

        [Dapper.Key]
        public int Id { get; set; }

        public string Wording { get; set; }

        public string Description { get; set; }
        public DateTime DateL { get; set; }
        public string DesM_Code { get; set; }

        public virtual Mode Mode { get; set; }
        public virtual ICollection<Registration> Registration { get; set; }

    }
}
