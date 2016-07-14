using System.Data.Common;
using System.Configuration;
using System.Windows.Forms;

namespace EstimatedBudget.POCO
{
    public class ConnectionProvider
    {
        //IDbConnection con;
        DbConnection conn;
        string connectionString;
        DbProviderFactory factory;

        // constructeur qui récèpure la connection string et le provider dans le fichier config 
        public ConnectionProvider()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["myDataBase"].ConnectionString.ToString();
            factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["myDataBase"].ProviderName.ToString());
        }

        //constructeur 2 ou on lui indique les paramètres de connexion et provider
        public ConnectionProvider(string connectionString, string connectionProviderName)
        {
            this.connectionString = connectionString;
            factory = DbProviderFactories.GetFactory(connectionProviderName);
        }

        // Méthode pour ouvrir la connexion
        public DbConnection GetOpenConnection()
        {
            conn = factory.CreateConnection();
            conn.ConnectionString = this.connectionString;
            try
            {
                conn.Open();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("La connection à la base de données a été perdu.", "Connection Perdu.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return conn;
        }
    }
}
