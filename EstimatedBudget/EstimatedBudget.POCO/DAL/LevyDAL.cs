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

        public static List<Levy> Load(IEnumerable<Frequency> DataFrequency = null, IEnumerable<Category> DataCategory = null, string Where = "")
        {
            IEnumerable<Levy> DataList = null;
            using (var myCnn = Cnn.GetOpenConnection())
            {
                DataList = myCnn.GetList<Levy>(Where);
            }

            if (DataList == null)
                return null;

            return DataList.ToList();
        }

        public static void Save(Levy L)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Execute("INSERT INTO Levy (Wording, Description, F_Code,C_Id,DateL,B_Code,Price) VALUES(@Wording, @Description, @F_Code,@C_Id,@DateL,@B_Code,@Price)", new
                {
                    L.Wording,
                    L.Description,
                    L.F_Code,
                    L.C_Id,
                    L.DateL,
                    L.B_Code,
                    L.Price
                });
            }
        }

        public static void Update(Levy L)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Update(L);
            }
        }
        public static void Delete(Levy L)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Delete(L);
            }
        }

        public static void CheckLevies(Levy L)
        {
          /*  var Today = DateTime.Now.ToString("yyyy-MM-dd");

            using (var myCnn = Cnn.GetOpenConnection())
            {
                //Inférieur égale à la date d'aujourd'hui
                var Levies = myCnn.GetList<Levy>("where DateL <= TO_DATE(?, '"+  Today + "')");

                var Registrations = RegistrationDAL.Load();
                foreach(var Levy in Levies)
                {*/
                    /*Verifier si l'enregisrement n'a pas été fait par 
                        * la date et 
                        * si il 'ya un code L_Id 
                        * categorie et */
                   /* Registration R = Registrations.Where(x => x.DateR == Levy.DateL && x.L_Id != 0 && x.C_Id == Levy.C_Id).SingleOrDefault();

                    //register
                    if(R == null)
                    {
                        RegistrationDAL.Save(new Registration
                        {
                            DateR = Levy.DateL,
                            L_Id = Levy.Id,
                            C_Id = Levy.C_Id,
                            Price = Levy.Price,
                            B_Code = Levy.B_Code,
                            Wording = Levy.Wording,
                            Description = Levy.Description
                        });
                    }
                }

            }*/
        }
    }
}

