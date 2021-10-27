using RepositoryBiblioteca.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RepositoryBiblioteca.Repository
{
    public class Autori : IAutori
    {
        public int CreateAutore(Autore autore)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @"INSERT INTO [dbo].[Autore]
                                       ([Nome]
                                       ,[Cognome]
                                       ,[Nazionalità])
                                 VALUES (@Nome),
                                        (@Cognome),
                                        (@Nazionalità); SELECT SCOPE_IDENTITY()";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", autore.Nome); 
                command.Parameters.AddWithValue("@Cognome", autore.Cognome);
                command.Parameters.AddWithValue("@Nazionalità",autore.Nazionalità);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Autori", "CreateAutore");
                return -1;
            }
        }

        public bool DeleteAutore(int idAutore)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @"UPDATE [dbo].[Autore]
                                     SET Cancellato = 1
                                   WHERE [IdAutore] = @IdAutore";
                using var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Autori", "DeleteAutore");
                return false;
            };
        }

        public Autore GetAutore(int idAutore)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @"SELECT [IdAutore]
                                      ,[Nome]
                                      ,[Cognome]
                                      ,[Nazionalità]
                                      ,[Cancellato]
                                  FROM [dbo].[Autore]
                              WHERE [IdAutore] = @IdAutore";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdAutore", idAutore);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessun autore trovato per l'Id richiesto {idAutore}");

                datareader.Read();
                {
                    return new Autore
                    {
                        Id = Convert.ToInt32(datareader["IdAutore"]),
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        Nazionalità= datareader["Nazionalità"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Autori", "GetAutore");
                return null;
            }
        }

        public IEnumerable<Autore> GetAutori()
        {
            using var connection = new SqlConnection(Connection.GetConnection());
            connection.Open();
            var sql = @"SELECT [IdAutore]
                              ,[Nome]
                              ,[Cognome]
                              ,[Nazionalità]
                              ,[Cancellato]
                          FROM [dbo].[Autore]";
            using var command = new SqlCommand(sql, connection);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    yield return new Autore
                    {
                        Id = Convert.ToInt32(datareader["IdAutore"]),
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                    };
                }
            }
        }

        public bool UpdateAutore(Autore autore)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @"UPDATE [dbo].[Autore]
                           SET [Nome] = @Nome
                              ,[Cognome] = @Cognome
                              ,[Nazionalità] = @Nazionalità
                              ,[Cancellato] = @Cancellato
                             WHERE [IdAutore] = @IdAutore";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Cancellato", autore.Cancellato);
                command.Parameters.AddWithValue("@Nome", autore.Nome);
                command.Parameters.AddWithValue("@IdAutore", autore.Id);
                command.Parameters.AddWithValue("@Cognome", autore.Cognome);
                command.Parameters.AddWithValue("@Nazionalità", autore.Nazionalità);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Autori", "UpdateAutore");
                return false;
            }
        }
    }
}
   