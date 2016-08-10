using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.Errors
{
    public static class ProprertiesName
    {
        public static string[] BankAccount = { "Code", "Wording","Description", "Investment"};
        public static string[] Category = { "Id", "Wording", "Targetprice"};
        public static string[] Transfer = { "Id", "Wording","Description","DateL","F_Code","C_Id","Price","Way", "Beneficiary" };
        public static string[] Registration = { "Id", "Wording", "Description", "DateR", "Price","B_Code","C_Id","T_Id" };
    }
}
