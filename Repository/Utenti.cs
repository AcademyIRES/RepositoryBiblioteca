using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;
using System.Data.SqlClient;

namespace RepositoryBiblioteca.Repository
{
    public class Utenti : IUtenti
    {
        public int CreateUtente(Utente utente)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @" INSERT INTO [dbo].[Utenti]
                               ([Nome]
                               ,[Cognome]
                               ,[NumTelefono])
                         VALUES
                               (@Nome)
                               ,(@Cognome)
                               ,(@NumTelefono); 
                                SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", utente.Nome);
                command.Parameters.AddWithValue("@Cognome", utente.Cognome);
                command.Parameters.AddWithValue("@NumTelefono", utente.NumTelefono);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Utente", "CreateUtente");
                return -1;
            }
        }

        public bool DeleteUtente(int idUtente)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @" UPDATE [dbo].[Utenti]
                               SET Cancellato = 1;
                             WHERE [idUtente] = @IdUtente";
                using var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;               
               
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Utente", "UpdateUtente");
                return false;
            }

        }

        public Utente GetUtente(int idUtente)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @"SELECT [IdUtente]
                                  ,[Nome]
                                  ,[Cognome]
                                  ,[NumTelefono]
                                  ,[Cancellato]
                              FROM [dbo].[Utenti]
                   WHERE [IdUtente] = @IdUtente";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdAutore", idUtente);               
                
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessuna categoria trovata per l'Id richiesto {idUtente}");

                datareader.Read();
                {
                    return new Utente
                    {
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        NumTelefono = datareader["NumTelefono"].ToString(),
                        Id = Convert.ToInt32(datareader["IdUtente"]),

                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Utente", "GetUtente");
                return null;
            }
        }

        public IEnumerable<Utente> GetUtenti()
        {
            using var connection = new SqlConnection(Connection.GetConnection());
            connection.Open();
            var sql = @"SELECT [IdUtente]
                                  ,[Nome]
                                  ,[Cognome]
                                  ,[NumTelefono]
                                  ,[Cancellato]
                              FROM [dbo].[Utenti]
                   WHERE [IdUtente] = @IdUtente";
            using var command = new SqlCommand(sql, connection);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    yield return new Utente
                    {
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        NumTelefono = datareader["NumTelefono"].ToString(),
                        Id = Convert.ToInt32(datareader["IdUtente"]),
                    };
                }
            }
        }

        public bool UpdateUtente(Utente utente)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @" UPDATE [dbo].[Utenti]
                             SET [Nome] = @Nome
                                  ,[Cognome] = @Cognome
                                  ,[NumTelefono] = @NumTelefono
                                  ,[Cancellato] = @Cancellato
                             WHERE [idUtente] = @IdUtente";
                using var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;

            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Utente", "UpdateUtente");
                return false;
            }

        }
    }
}
