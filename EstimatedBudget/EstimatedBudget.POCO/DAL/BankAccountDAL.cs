using EstimatedBudget.POCO.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace EstimatedBudget.POCO.DAL
{
    [Table ("BankAccount")]
    public static class BankAccountDAL
    {
        public static ConnectionProvider Cnn = new ConnectionProvider();

        public static List<BankAccount> Load(string Where = "")
        {
            IEnumerable<BankAccount> DataList = null;
            using (var myCnn = Cnn.GetOpenConnection())
            {
                DataList = myCnn.GetList<BankAccount>(Where);

            }
            if (DataList == null)
                return null;

            return DataList.ToList();
        }

        public static void Save(BankAccount B)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                //myCnn.Insert<BankAccount>(B);
                myCnn.Execute("INSERT INTO BankAccount (Code, Wording,Description, Investment) VALUES(@Code, @Wording,@Description,@Investment)", new
                {
                    B.Code,
                    B.Wording,
                    B.Description,
                    B.Investment          
                });
            }

        }

        public static void Update(BankAccount B)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Update(B);
            }

        }

        public static void Delete(BankAccount B)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Delete(B);
            }
        }

        public static bool IsCodeExistPK(string value)
        {
            BankAccount Result;
            using (var myCnn = Cnn.GetOpenConnection())
            {
              Result = myCnn.Get<BankAccount>(value);
            }

            if (Result != null)
                return true;
            else
                return false;
        }
    }
}
