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
            if (DataList == null)
                return null;

            return DataList.ToList();
        }

        public static void Save(Registration R)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {

                DBNull NullValue = DBNull.Value;

              //  myCnn.Insert<Registration>(R);
                myCnn.Execute("INSERT INTO Registration (Wording, Description,DateR,Price,B_Code,C_Id,T_Id) VALUES(@Wording, @Description,@DateR,@Price,@B_Code,@C_Id,@T_Id)", new
                {
                    R.Wording,
                    R.Description,
                    R.DateR,
                    R.Price,
                    R.B_Code,
                    R.C_Id,
                    R.T_Id
                });
            }
        }

        public static void Update(Registration R)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Update(R);
            }
        }

        public static void Delete(Registration R)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Delete(R);
            }
        }
    }
}
