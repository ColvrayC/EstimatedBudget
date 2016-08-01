using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EstimatedBudget.POCO.Models
{
    [Table("BankAccount")]
    public partial class BankAccount: ICloneable
    {
        public BankAccount()
        {
            this.Registration = new HashSet<Registration>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public int Code { get; set; }

        public string Wording { get; set; }

        public string Description { get; set; }

        public bool Investment { get; set; }

        public virtual ICollection<Registration> Registration { get; set; }

        public override string ToString()
        {
            return Code.ToString();
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


}
