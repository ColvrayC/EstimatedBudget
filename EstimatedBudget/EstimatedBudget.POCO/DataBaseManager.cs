using Dapper;
using System.Data.SQLite;

namespace EstimatedBudget.POCO
{
    public static class DataBaseManager
    {
        public static void CreateDatabase()
        {
            using (var cnn = new ConnectionProvider().GetOpenConnection())
            {
               cnn.Execute(
                    @"CREATE TABLE BankAccount
                    (
                        Code INT PRIMARY KEY NOT NULL,
                        Wording VARCHAR(200) NOT NULL,
                        Investment BOOLEAN NOT NULL
                    );

                    CREATE TABLE Category
                    (
                        Code VARCHAR(4) PRIMARY KEY NOT NULL,
                        Wording VARCHAR(200) NOT NULL,
                        Targetprice decimal(7,2) NOT NULL
                    );

                    CREATE TABLE Mode(
                        Code VARCHAR(4) PRIMARY KEY NOT NULL,
                        Wording VARCHAR(200) NOT NULL
                    );

                    CREATE TABLE Levy(
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Wording VARCHAR(200) NOT NULL,
                        Description VARCHAR(300) NULL,
                        DateL Date NOT NULL,
                        M_Code VARCHAR(4) NOT NULL,
                        FOREIGN KEY(M_Code) REFERENCES Mode(Code)
                    );

                    CREATE TABLE Registration(
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Wording VARCHAR(200) NOT NULL,
                        Description VARCHAR(300) NULL,
                        DateR Date NOT NULL,
                        Price decimal(7,2) NOT NULL,
                        Way decimal(7,2) NOT NULL,
                        B_Code int NOT NULL,
                        C_Code varchar(4) NOT NULL,
                        L_Id int NOT NULL,
                        FOREIGN KEY(B_Code) REFERENCES BankAccount(Code),
                        FOREIGN KEY(C_Code) REFERENCES Category(Code),
                        FOREIGN KEY(L_Id) REFERENCES Levy(Id)
                    );"

                );
            }
        }
    }
}
