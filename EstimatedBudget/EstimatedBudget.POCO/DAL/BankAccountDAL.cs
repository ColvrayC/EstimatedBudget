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
            return DataList.ToList();
        }

        public static void Save(BankAccount B)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Insert<BankAccount>(B);
            }

        }

        public static void Update(BankAccount B)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Update(B);
            }

        }
    }
}
