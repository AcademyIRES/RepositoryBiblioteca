using System;
using System.Collections.Generic;
using RepositoryBiblioteca.Model;
using System.Data.SqlClient;


namespace RepositoryBiblioteca.Repository
{
    public interface IPrestiti<T> where T: Oggetto
    {

        public int CreatePrestito(Prestito<T> prestito)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @" INSERT INTO [dbo].[Prestiti]
                                       ([DataInizio]
                                       ,[DataFine]
                                       ,[IdUtente]
                                       ,[IdImpiegatoPrestito]
                                       ,[IdImpiegatoRestituzione]
                                       ,[IdOggetto])
                                 VALUES
                                       ([DataInizio]
                                       ,[DataFine]
                                       ,[IdUtente]
                                       ,[IdImpiegatoPrestito]
                                       ,[IdImpiegatoRestituzione]
                                       ,[IdOggetto])
                                 SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@DataInizio", prestito.DataInizio);
                command.Parameters.AddWithValue("@DataFine", prestito.DataFine);
                command.Parameters.AddWithValue("@IdUtente", prestito.Utente.Id);
                command.Parameters.AddWithValue("@IdImpiegatoPrestito", prestito.ImpPrestito.Id);
                command.Parameters.AddWithValue("@IdImpiegatoRestituzione", prestito.ImpRestituzione.Id);
                command.Parameters.AddWithValue("@IdOggetto", prestito.Oggetto.IdOggetto);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Prestiti", "Create");
                return -1;
            }
        }
        Prestito<T> GetPrestito(int idPrestito);
        IEnumerable<Prestito<T>> GetPrestiti();
        bool UpdatePrestito(Prestito<T> prestito);
    }
}
