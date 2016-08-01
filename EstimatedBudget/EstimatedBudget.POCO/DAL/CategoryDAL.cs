using Dapper;
using EstimatedBudget.POCO.Models;
using System.Collections.Generic;
using System.Linq;

namespace EstimatedBudget.POCO.DAL
{
    [Table("Category")]
    public static class CategoryDAL
    {
        public static ConnectionProvider Cnn = new ConnectionProvider();

        public static List<Category> Load(string Where = "")
        {
            IEnumerable<Category> DataList = null;
            using (var myCnn = Cnn.GetOpenConnection())
            {
                DataList = myCnn.GetList<Category>(Where);
            }
            if (DataList == null)
                return null;

            return DataList.ToList();
        }

        public static void Save(Category C)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Execute("INSERT INTO CATEGORY (Wording, Targetprice) VALUES(@Wording, @Targetprice)", new
                {
                    C.Wording,
                    C.Targetprice
                });  
            }

        }

        public static void Update(Category C)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Update(C);
            }

        }
        public static void Delete(Category C)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Delete(C);
            }
        }
    }
}
