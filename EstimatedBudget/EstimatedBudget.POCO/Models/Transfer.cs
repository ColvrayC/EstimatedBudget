using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.Models
{
    [Table("Transfer")]
    public partial class Transfer : ICloneable
    {
        public Transfer()
        {
            this.Registration = new HashSet<Registration>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public string Wording { get; set; }

        public string Description { get; set; }
        public DateTime DateL { get; set; }
        public bool Way { get; set; }
        public decimal Price { get; set; }
        public string F_Code { get; set; }
        public Nullable<int> C_Id { get; set; }
        public int B_Code { get; set; }
        public Nullable<int> B_CodeBeneficiary { get; set; }
        public string Beneficiary { get; set; }

        public virtual Frequency Frequency { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Registration> Registration { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
