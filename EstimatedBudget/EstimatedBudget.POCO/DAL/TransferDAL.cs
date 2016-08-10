using Dapper;
using EstimatedBudget.POCO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO.DAL
{
    [Table("Transfer")]
    public static class TransferDAL
    {
        public static ConnectionProvider Cnn = new ConnectionProvider();

        public static List<Transfer> Load(IEnumerable<Frequency> DataFrequency = null, IEnumerable<Category> DataCategory = null, string Where = "")
        {
            IEnumerable<Transfer> DataList = null;
            using (var myCnn = Cnn.GetOpenConnection())
            {
                DataList = myCnn.GetList<Transfer>(Where);
            }

            if (DataList == null)
                return null;

            return DataList.ToList();
        }

        public static void Save(Transfer T)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                if(T.Beneficiary != null ||T.Beneficiary != string.Empty)
                {
                    T.B_CodeBeneficiary = 0;
                }

                if (T.Beneficiary == null)
                    T.Beneficiary = string.Empty;

                //If the register is Immediat
                if(T.F_Code == "IMM")
                {
                    //Only register but no Add to Transfer

                    //If The destination is a Exist Account
                    if(T.B_CodeBeneficiary != 0)
                    {

                    }
                    else //The destination is Other
                    {

                    }

                }

                myCnn.Execute("INSERT INTO Transfer (Wording, Description, F_Code,C_Id,DateL,B_Code,Price,Way,B_CodeBeneficiary,Beneficiary) VALUES(@Wording, @Description, @F_Code,@C_Id,@DateL,@B_Code,@Price,@Way,@B_CodeBeneficiary,@Beneficiary)", new
                {
                    T.Wording,
                    T.Description,
                    T.F_Code,
                    T.C_Id,
                    T.DateL,
                    T.Way,
                    T.B_Code,
                    T.Price,
                    T.B_CodeBeneficiary,
                    T.Beneficiary
                });
            }
        }

        public static void Update(Transfer L)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Update(L);
            }
        }
        public static void Delete(Transfer L)
        {
            using (var myCnn = Cnn.GetOpenConnection())
            {
                myCnn.Delete(L);
            }
        }

        public static void CheckTransfersNotRegister()
        {
            var Today = DateTime.Now.ToString("yyyy-MM-dd");

            using (var myCnn = Cnn.GetOpenConnection())
            {
                //Inférieur égale à la date d'aujourd'hui
                var Transfers = myCnn.GetList<Transfer>("where date(DateL) < date('"+ Today +"')");

                var Registrations = RegistrationDAL.Load();
                foreach(var Transfer in Transfers)
                {
                    /*Verifier si l'enregisrement n'a pas été fait par 
                        * la date et 
                        * si il 'ya un code T_Id 
                        * categorie et */
                    Registration R = Registrations.Where(x => 
                    x.DateR == Transfer.DateL 
                    && x.T_Id != 0 
                    && x.C_Id == Transfer.C_Id).SingleOrDefault();

                    //register
                    if(R == null)
                    {
                        RegistrationDAL.Save(new Registration
                        {
                            DateR = Transfer.DateL,
                            T_Id = Transfer.Id,
                            C_Id = Transfer.C_Id,
                            Price = Transfer.Price,
                            B_Code = Transfer.B_Code,
                            Wording = Transfer.Wording,
                            Description = Transfer.Description
                        });
                    }
                }

            }
        }
    }
}

