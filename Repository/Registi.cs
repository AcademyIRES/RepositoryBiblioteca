using System;
using System.Collections.Generic;
using RepositoryBiblioteca.Model;
using System.Data.SqlClient;

namespace RepositoryBiblioteca.Repository
{
    public class Registi : IRegisti
    {
        public int CreateRegista(Regista regista)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"INSERT INTO [dbo].[Regista]
                                       ([Nome]
                                       ,[Cognome]
                                       ,[Nazionalità]
                                      VALUES
                                       (@Nome)
                                       ,(@Cognome)
                                       ,(@Nazionalità)
                                       SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", regista.Nome);
                command.Parameters.AddWithValue("@Cognome", regista.Cognome);
                command.Parameters.AddWithValue("@Nazionalità", regista.Nazionalità);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Regista", "CreateRegista");
                return -1;
            }
        }

        public bool DeleteRegista(int idRegista)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Regista]
                                 SET Cancellato = 1
                                 WHERE [IdRegista] = @IdRegista";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdRegista", idRegista);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Regista", "DeleteRegista");
                return false;
            }
        }

        public Regista GetRegista(int idRegista)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"SELECT [IdRegista]
                                  ,[Nome]
                                  ,[Cognome]
                                  ,[Nazionalità]
                                  ,[Cancellato]
                              FROM [dbo].[Regista]
                   WHERE [IdRegista] = @IdRegista";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdRegista", idRegista);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows) 
                    throw new Exception($"Nessun regista trovato per l'Id richiesto {idRegista}");

                datareader.Read();
                {
                    return new Regista
                    {
                       Id = Convert.ToInt32(datareader["IdRegista"]),
                       Nome = datareader["Nome"].ToString(),
                       Cognome = datareader["Cognome"].ToString(),
                       Nazionalità = datareader["Nazionalità"].ToString(),
                       Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Registi", "GetRegista");
                return null;
            };
        }

        public IEnumerable<Regista> GetRegista()
        {   using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"SELECT [IdRegista]
                                  ,[Nome]
                                  ,[Cognome]
                                  ,[Nazionalità]
                                  ,[Cancellato]
                              FROM [dbo].[Regista]
                                        ";
                using var command = new SqlCommand(sql, connection);
                
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessun regista trovato");
               
                while (datareader.Read())
               
                {
                   yield  return new Regista
                    {
                        Id = Convert.ToInt32(datareader["idRegista"]),
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                    };
                }
            }

        public bool UpdateRegista(Regista regista)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Regista]
                 SET              [Nome] = @Nome
                                  [Cognome] = @Cognome
                                  [Nazionalità] = @Nazionalità
                                  [Cancellato] = @Cancellato
                              FROM [dbo].[Regista]
                      WHERE [IdRegista] = @IdRegista";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Cancellato", regista.Cancellato);
                command.Parameters.AddWithValue("@Nome", regista.Nome);
                command.Parameters.AddWithValue("@Cognome", regista.Cognome);
                command.Parameters.AddWithValue("@Nazionalità", regista.Nazionalità);
                command.Parameters.AddWithValue("@IdRegista", regista.Id);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Registi", "UpdateRegista");
                return false;
            }
        }
    }
}

