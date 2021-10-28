using RepositoryBiblioteca.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepositoryBiblioteca.Repository
{
    public class Impiegati : IImpiegati
    {
        public int CreateImpiegato(Impiegato impiegato)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @" INSERT INTO [dbo].[Impiegati]
                                                ([Nome])
                                               ,([Cognome])
                                     VALUES
                                               (@Nome)
                                              ,(@Cognome)
                                SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", impiegato.Nome);
                command.Parameters.AddWithValue("@Cognome", impiegato.Cognome);
                
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Impiegati", "CreateImpiegato");
                return -1;
            }
        }

        public bool DeleteImpiegato(int idImpiegato)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Impiegati]
                                           SET Cancellato = 1
                                 WHERE [IdImpiegato] = @IdImpiegato";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdImpiegato", idImpiegato);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Impiegati", "DeleteImpiegato");
                return false;
            }
        }

        public Impiegato GetImpiegato(int idImpiegato)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"SELECT [IdImpiegato]
                                ,[Nome]
                                ,[Cognome]
                                ,[Cancellato]
                            FROM [dbo].[Impiegati]
                   WHERE [IdImpiegato] = @IdImpiegato";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdImpiegato", idImpiegato);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessun impiegato trovato per l'Id richiesto {idImpiegato}");

                datareader.Read();
                {
                    return new Impiegato
                    {
                       Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                        Id = Convert.ToInt32(datareader["IdImpiegato"]),
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Impiegati", "GetImpiegato");
                return null;
            }
        }

        public IEnumerable<Impiegato> GetImpiegato()
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT    [IdImpiegato] 
                                 ,[Nome]
                                 ,[Cognome]
                                 ,[Cancellato]
                                FROM [dbo].[Impiegati]
                                        ";
            using var command = new SqlCommand(sql, connection);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    yield return new Impiegato
                    {
                        Id = Convert.ToInt32(datareader["IdImpiegato"]),
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),

                    };
                }
            }
        }

        public bool UpdateImpiegato(Impiegato impiegato)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Impiegati]
                               SET [Nome] = @Nome
                                  ,[Cognome] = @Cognome
                                  ,[Cancellato] = @Cancellato
                             WHERE [idImpiegato] = @IdImpiegato";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Cancellato", impiegato.Cancellato);
                command.Parameters.AddWithValue("@Nome", impiegato.Nome);
                command.Parameters.AddWithValue("@Cognome", impiegato.Cognome);
                command.Parameters.AddWithValue("@IdImpiegato", impiegato.Id);
                command.ExecuteNonQuery();
                return true;

            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Impiegati", "UpdateImpiegato");
                return false;
            }

        }
    }
}
   

