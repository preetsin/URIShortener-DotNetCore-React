using System;
using System.Data.SQLite;
using URIShortener.Models;

namespace URIShortener.Repositories
{
    public class UriRepository : IRespository<UriModel>
    {
        private string _connString;

        public UriRepository(string connectionString)
        {
            _connString = connectionString;
        }
        
        public void Add(UriModel entity)
        {
            //bool isAdded = false;
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                
                using (var cmd = new SQLiteCommand(null, conn))
                {
                    var query = "INSERT INTO Uri (Original, Alias) VALUES (@Original, @Alias)";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("Original", entity.Original);
                    cmd.Parameters.AddWithValue("Alias", entity.Alias);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    //isAdded = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                conn.Close();
            }
            //return isAdded;
        }
        

        public UriModel FindByField(string fieldName, string fieldValue)
        {
            UriModel uri = null;
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(null, conn))
                {
                    var query = String.Format("SELECT * FROM Uri WHERE {0} = @Value", fieldName);

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("Value", fieldValue);
                    cmd.Prepare();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            uri = new UriModel
                            {
                                Id = Int32.Parse(reader["Id"].ToString()),
                                Original = reader["Original"].ToString(),
                                Alias = reader["Alias"].ToString()
                            };
                        }
                    }
                }
                conn.Close();
            }

            return uri;
        }
        
    }
    
    /**
     * 
     *  CREATE TABLE `Uri` (
	        `Id`	INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
	        `Original`	TEXT NOT NULL UNIQUE,
	        `Alias`	TEXT NOT NULL UNIQUE
        );
     *
     */
}
