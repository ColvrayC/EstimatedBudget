using Dapper;
using EstimatedBudget.POCO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.DAL
{
    public static class BudgetMonitoringDAL
    {
        public static ConnectionProvider Cnn = new ConnectionProvider();

        public static List<BusinessBudgetMonitoring> Load(string Where,DateTime Date,bool Anticipated = false)
        {
            var DataList = new List<BusinessBudgetMonitoring>(); ;
            using (var myCnn = Cnn.GetOpenConnection())
            {
                //Filter for the Current Month
                var Month = Date.ToString("MM");
                //Categories
                var Categories = CategoryDAL.Load(Where);
                //Registration Filter By Month, BankAccount
                var Registrations = RegistrationDAL.Load(Where + " AND strftime('%m', DateR) ='" + Month + "'");

                if (Anticipated)
                {
                    //Add Virtual Registration
                    foreach (var R in TransferDAL.AnticipatedRegistrationsOfTransfers)
                        Registrations.Add(R);
                }

                foreach (var C in Categories)
                {
                    //Sum of All Registrations for Current Category
                    var RegistrationsPrice = Registrations.Where(r => r.C_Id == C.Id).ToList().Sum(s=>s.Price);
                    DataList.Add(new BusinessBudgetMonitoring
                    {
                        WordingCategory = C.Wording,
                        TargetPrice = C.Targetprice,
                        RegistrationsPrice = RegistrationsPrice,
                        HundredPerCent = PerCent(RegistrationsPrice,C.Targetprice)
                    });
                }
            }

            if (DataList == null)
                return null;

            return DataList.ToList(); ;
        }

        private static decimal CheckZero(decimal Value)
        {
            if (Value == 0M)
                return 1M;
            else
                return Value;
        }

        private static int PerCent(decimal Value1,decimal Value2)
        {
            int Result = 0;
            if (Value1 != 0M)
                Result = Convert.ToInt32(CheckZero(Value1) * 100 / Value2);

             return Result;
        }
    }
}
