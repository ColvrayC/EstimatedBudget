using System;
using System.Collections.Generic;
using Dapper;
using System.ComponentModel.DataAnnotations;

namespace EstimatedBudget.POCO.Models
{
    public partial class BankAccount
    {
        public BankAccount()
        {
            this.Registration = new HashSet<Registration>();
        }

        [Dapper.Key]
        public int Code { get; set; }

        public string Wording { get; set; }

        public bool Investment { get; set; }

        public virtual ICollection<Registration> Registration { get; set; }
    }


}
