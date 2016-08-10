using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.Models
{
    /// <summary>
    /// BUSSINESS OBJECT
    /// </summary>
    public class BusinessBudgetMonitoring
    {
        public BusinessBudgetMonitoring()
        {

        }
        public string WordingCategory { get; set; }
        public int HundredPerCent { get; set; }
        public decimal RegistrationsPrice { get; set; }
        public decimal TargetPrice { get; set; }
    }
}
