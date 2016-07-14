using Dapper;
using EstimatedBudget.POCO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.DAL
{
    [Table ("Mode")]
    public static class ModeDAL
    {
        public static ConnectionProvider Cnn = new ConnectionProvider();

        public static List<Mode> Load(string Where = "")
        {
            IEnumerable<Mode> DataList = null;
            using (var myCnn = Cnn.GetOpenConnection())
            {
                DataList = myCnn.GetList<Mode>(Where);
            }
            return DataList.ToList();
        }

        public static void Save(Mode M)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Insert<Mode>(M);
            }
        }

        public static void Update(Mode M)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Update(M);
            }
        }
    }
}

