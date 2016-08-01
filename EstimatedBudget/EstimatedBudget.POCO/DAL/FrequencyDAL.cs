using Dapper;
using EstimatedBudget.POCO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.DAL
{
    [Table ("Frequency")]
    public static class FrequencyDAL
    {
        public static ConnectionProvider Cnn = new ConnectionProvider();

        public static List<Frequency> Load(string Where = "")
        {
            IEnumerable<Frequency> DataList = null;
            using (var myCnn = Cnn.GetOpenConnection())
            {
                DataList = myCnn.GetList<Frequency>(Where);
            }
            if (DataList == null)
                return null;

            return DataList.ToList();
        }

        public static void Save(Frequency F)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Insert<Frequency>(F);
            }
        }

        public static void Update(Frequency F)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Update(F);
            }
        }

        public static void Delete(Frequency F)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Delete(F);
            }
        }
    }
}

