using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EstimatedBudget.POCO
{
    public static class DriverSqlite
    {
        public static  bool Installed()
        {
            string ProgramFilesx86 = Environment.GetEnvironmentVariable("ProgramFiles(x86)");

            if (Directory.Exists(ProgramFilesx86 + "\\System.Data.SQLite"))
                return true;
            else
                return false;

        }

        public static void InstallSqLiteDriver()
        {
            var AssemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            Process.Start(AssemblyPath + "\\DriverSQLite\\DriverSqLitex86.exe");
        }
    }
}
