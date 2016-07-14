using Dapper;
using EstimatedBudget.POCO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.DAL
{
    [Table("Registration")]
    public static class RegistrationDAL
    { 
        public static ConnectionProvider Cnn = new ConnectionProvider();

        public static List<Registration> Load(string Where = "")
        {
            IEnumerable<Registration> DataList = null;
            using (var myCnn = Cnn.GetOpenConnection())
            {
                DataList = myCnn.GetList<Registration>(Where);
            }
            return DataList.ToList();
        }

        public static void Save(Registration R)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Insert<Registration>(R);
            }
        }

        public static void Update(Registration R)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Update(R);
            }
        }
    }
}
