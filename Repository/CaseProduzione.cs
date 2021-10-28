using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;
using System.Data.SqlClient;


namespace RepositoryBiblioteca.Repository
{
    public class CaseProduzione : ICaseProduzione
    {
        public int CreateCasaProduzione(CasaProduzione casaproduzione)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"INSERT INTO [dbo].[CasaProduzione]
                                           ([Nome]
                                           ,[Nazionalità]
                                        VALUES
                                            (@Nome)
                                           ,(@Nazionalità);
                                    SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", casaproduzione.Nome);
                command.Parameters.AddWithValue("@Nazionalità", casaproduzione.Nazionalità);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "CaseProduzione", "CreateCasaProduzione");
                return -1;
            }
        }

        public bool DeleteCasaProduzione(int idCasaProduzione)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[CasaProduzione]
                                     SET Cancellato = 1
                                     WHERE [IdCasaProduzione] = @IdCasaProduzione";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdCasaProduzione",idCasaProduzione);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "CaseProduzione", "DeleteCasaProduzione");
                return false;
            }
        }

        public CasaProduzione GetCasaProduzione(int idCasaProduzione)
        {

            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"SELECT [IdCasaProduzione]
                                  ,[Nome]
                                  ,[Nazionalità]
                                  ,[Cancellato]
                              FROM [dbo].[CasaProduzione]
                   WHERE [IdCasaProduzione] = @IdCasaProduzione";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdCasaProduzione", idCasaProduzione);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessuna casa di produzione trovata per l'Id richiesto {idCasaProduzione}");

                datareader.Read();
                {
                    return new CasaProduzione
                    {
                        IdCasaProduzione = Convert.ToInt32(datareader["IdCasaProduzione"]),
                        Nome = datareader["Nome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "CaseProduzione", "GetCasaProduzione");
                return null;
            }


        }

        public IEnumerable<CasaProduzione> GetCasaProduzione()
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [IdCasaProduzione]
                                  ,[Nome]
                                  ,[Nazionalità]
                                  ,[Cancellato]
                              FROM [dbo].[CasaProduzione]
                                        ";
            using var command = new SqlCommand(sql, connection);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    yield return new CasaProduzione
                    {
                        IdCasaProduzione = Convert.ToInt32(datareader["IdCasaProduzione"]),
                        Nome = datareader["Nome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                    };
                }
            }

        }

        public IEnumerable<CasaProduzione> GetCasaProduzione(bool deleted)
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [IdCasaProduzione]
                                  ,[Nome]
                                  ,[Nazionalità]
                                  ,[Cancellato]
                              FROM [dbo].[CasaProduzione]
                                WHERE Cancellato = @Cancellato
                                        ";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Cancellato",deleted);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    yield return new CasaProduzione
                    {
                        IdCasaProduzione = Convert.ToInt32(datareader["IdCasaProduzione"]),
                        Nome = datareader["Nome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                    };
                }
            }

        }

        public bool UpdateCasaProduzione(CasaProduzione casaproduzione)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[CasaProduzione]
                               SET [Nome] = @Nome,
                                [Nazionalità] = @Nazionalità,
                                [Cancellato] = @Cancellato,
                            WHERE [IdCasaProduzione] = @IdCasaProduzione";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Cancellato", casaproduzione.Cancellato);
                command.Parameters.AddWithValue("@Nome", casaproduzione.Nome);
                command.Parameters.AddWithValue("@IdCasaProduzione", casaproduzione.IdCasaProduzione);
                command.Parameters.AddWithValue("@Nazionalità", casaproduzione.Nazionalità);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "CaseProduzione", "UpdateCasaProduzione");
                return false;
            }
        }
    }
}