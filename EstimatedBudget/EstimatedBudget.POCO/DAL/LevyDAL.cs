using Dapper;
using EstimatedBudget.POCO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.DAL
{
    [Table("Levy")]
    public static class LevyDAL
    {
        public static ConnectionProvider Cnn = new ConnectionProvider();

        public static List<Levy> Load(string Where = "")
        {
            IEnumerable<Levy> DataList = null;
            using (var myCnn = Cnn.GetOpenConnection())
            {
                DataList = myCnn.GetList<Levy>(Where);
            }
            return DataList.ToList();
        }

        public static void Save(Levy C)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Insert<Levy>(C);
            }
        }

        public static void Update(Levy L)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Update(L);
            }
        }
    }
}

