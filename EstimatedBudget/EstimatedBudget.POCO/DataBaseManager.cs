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
                        Description VARCHAR(200) NOT NULL,
                        Investment BOOLEAN NOT NULL
                    );

                    CREATE TABLE Category
                    (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Wording VARCHAR(200) NOT NULL,
                        Targetprice decimal(8,2) NOT NULL,
                        B_Code int NOT NULL,
                        FOREIGN KEY(B_Code) REFERENCES BankAccount(Code) ON DELETE CASCADE
                    );

                    CREATE TABLE Frequency(
                        Code VARCHAR(4) PRIMARY KEY NOT NULL,
                        Wording VARCHAR(200) NOT NULL
                    );

                    CREATE TABLE Levy(
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Wording VARCHAR(200) NOT NULL,
                        Description VARCHAR(300) NULL,
                        Price decimal(8,2) NOT NULL,
                        DateL Date NOT NULL,
                        F_Code VARCHAR(4) NULL,
                        C_Id INTEGER NOT NULL,
                        B_Code int NOT NULL,
                        FOREIGN KEY(B_Code) REFERENCES BankAccount(Code)  ON DELETE CASCADE,
                        FOREIGN KEY(C_Id) REFERENCES Category(Id) ON DELETE CASCADE,
                        FOREIGN KEY(F_Code) REFERENCES Frequency(Code) ON DELETE CASCADE
                    );

                    CREATE TABLE Registration(
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Wording VARCHAR(200) NOT NULL,
                        Description VARCHAR(300) NULL,
                        DateR Date NOT NULL,
                        Price decimal(8,2) NOT NULL,
                        B_Code int NOT NULL,
                        C_Id INTEGER NOT NULL,
                        L_Id int NULL,
                        FOREIGN KEY(B_Code) REFERENCES BankAccount(Code)  ON DELETE CASCADE,
                        FOREIGN KEY(C_Id) REFERENCES Category(Id)  ON DELETE CASCADE,
                        FOREIGN KEY(L_Id) REFERENCES Levy(Id) ON DELETE CASCADE
                    );
                  
                    INSERT INTO Category(Wording,Targetprice,B_Code) VALUES('Nourriture',400.00,184645511);
                    INSERT INTO Category(Wording, Targetprice,B_Code) VALUES('Téléphones', 20.00,184645511); 
                    INSERT INTO Category(Wording, Targetprice,B_Code) VALUES('Internet', 40.00,1848451); 
                    INSERT INTO Category(Wording, Targetprice,B_Code) VALUES('EDF', 70.00,1848451);

                    INSERT INTO BankAccount(Code,Wording,Description,Investment) VALUES(184645511,'CCP','Melanie', 1);
                    INSERT INTO BankAccount(Code,Wording,Description,Investment) VALUES(1848451,'Livret A','Camille', 0);

                    INSERT INTO Frequency(Code,Wording) VALUES('IMM','Immédiat');
                    INSERT INTO Frequency(Code,Wording) VALUES('MENS','Mensuels');
                    INSERT INTO Frequency(Code,Wording) VALUES('TRI','Trimestriel');
                    INSERT INTO Frequency(Code,Wording) VALUES('SEME','Semestriel');
                    INSERT INTO Frequency(Code,Wording) VALUES('ANN','Annuel');

                    INSERT INTO Levy(Wording,Description,DateL,F_Code,C_Id,B_Code,Price) VALUES('CANAL+','','2016-08-12','IMM',3,184645511,30.00);
                    INSERT INTO Levy(Wording,Description,DateL,F_Code,C_Id,B_Code,Price) VALUES('EDF','','2016-07-25','MENS',2,184645511,20.00);
                    INSERT INTO Levy(Wording,Description,DateL,F_Code,C_Id,B_Code,Price) VALUES('MELANIE','','2016-08-12','IMM',3,1848451,15.00);
                 
"
                );
            }
        }
    }
}
