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
                        CreateRegistration(T.DateL);
                        CreateRegistrationForReicever(T.DateL);
                    }
                    else //The destination is Other
                    {
                        CreateRegistration(T.DateL);
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

        public static List<Registration> RegistrationsOfTransfers { get; set; }
        public static List<Registration> AnticipatedRegistrationsOfTransfers { get; set; }
        public static Transfer myTransfer { get; set; }
        public static bool Anticipated { get; set; }
        public static void Action(List<Registration> RegistrationsTransferts,List<Transfer> Transferts,bool Anticipate = false)
        {
            Anticipated = Anticipate;
            AnticipatedRegistrationsOfTransfers = new List<Registration>();

            var Today = DateTime.Now;
            RegistrationsOfTransfers = new List<Registration>();

            //Browse Transferts
            foreach (var T in Transferts)
            {
                //List Registrayions Of The Transfer (T)
                RegistrationsOfTransfers = RegistrationsTransferts.Where(r => r.T_Id == T.Id).ToList();

                //Add T to Property
                myTransfer = T;

                //Find the Frequency
                switch (T.F_Code)
                {
                    case "MENS": BrowseMonths(1); break;
                    case "TRI": BrowseMonths(3); break;
                    case "SEME": BrowseMonths(6); break;
                    case "ANN": BrowseMonths(12); break;
                }
               
            }
           
        }

        private static void BrowseMonths(int FrequencyMonths)
        {
            //Month Of Today
            var Today = DateTime.Now;
            var DateTransfer = myTransfer.DateL;

            if(Anticipated)
                //Simulate Today Is End Day of the Month
                Today = new DateTime(Today.Year, Today.Month, DateTime.DaysInMonth(Today.Year, Today.Month));


            while (DateTime.Parse(DateTransfer.ToString("dd/MM/yyyy")) <= DateTime.Parse(Today.ToString("dd/MM/yyyy")))
           {
                switch (FrequencyMonths)
                {
                    case 1: CheckTransferNotRegister(DateTransfer); break;
                    case 3: CheckTransferNotRegister(DateTransfer); break;
                    case 6: CheckTransferNotRegister(DateTransfer); break;
                    case 12: CheckTransferNotRegister(DateTransfer); break;
                }

                //Next Months
                DateTransfer = DateTransfer.AddMonths(FrequencyMonths);
            }
        }

        public static void CheckTransferNotRegister(DateTime DateTransfer)
        {
                //On check si un enregistrement du transfert, à été enregistrer dans le mois de DateTransfer
                var TransferDone = RegistrationsOfTransfers.Where(r => r.DateR == DateTransfer).SingleOrDefault();

                //If not done
                if (TransferDone == null)
                {
                    //If my transfer has an Internal Reicever 
                    //Else simple Registration
                    if (myTransfer.B_CodeBeneficiary != null)
                        {
                            CreateRegistrationForReicever(DateTransfer);
                            CreateRegistration(DateTransfer);
                        }
                else
                    CreateRegistration(DateTransfer);
            }
                                       
        }


        //Simple Registration
        public static void CreateRegistration(DateTime DateTransfer)
        {
             int Price = (int)myTransfer.Price;
            /* if (!myTransfer.Way)
                 Price = Price * -1;*/

            if (Anticipated)
            {
                AnticipatedRegistrationsOfTransfers.Add(new Registration
                {
                    Wording = myTransfer.Wording,
                    Description = myTransfer.Description,
                    DateR = DateTransfer,
                    Way = myTransfer.Way,
                    Price = Price,
                    B_Code = myTransfer.B_Code,
                    C_Id = myTransfer.C_Id,
                    T_Id = myTransfer.Id
                });
            }
            else
            {
                RegistrationDAL.Save(new Registration
                {
                    Wording = myTransfer.Wording,
                    Description = myTransfer.Description,
                    DateR = DateTransfer,
                    Way = myTransfer.Way,
                    Price = Price,
                    B_Code = myTransfer.B_Code,
                    C_Id = myTransfer.C_Id,
                    T_Id = myTransfer.Id
                });
            }
        }
        public static void CreateRegistrationForReicever(DateTime DateTransfer)
        {
            if (Anticipated)
                return;

            int Price = (int)myTransfer.Price;
            /* if (myTransfer.Way)
                 Price = Price * -1;*/
            RegistrationDAL.Save(new Registration
            {
                Wording = myTransfer.Wording,
                Description = myTransfer.Description,
                DateR = DateTransfer,
                Way = myTransfer.Way,
                Price = Price,
                B_Code =(int)myTransfer.B_CodeBeneficiary,
                C_Id = myTransfer.C_Id,
                T_Id = myTransfer.Id
            });
        }
     
    }
}

