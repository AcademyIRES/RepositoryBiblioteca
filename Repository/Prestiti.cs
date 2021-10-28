using RepositoryBiblioteca.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RepositoryBiblioteca.Repository
{
    public abstract class Prestiti<T> : IPrestiti<T> where T : Oggetto
    {


        public bool UpdatePrestito(Prestito<T> prestito)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE UPDATE [dbo].[Prestiti]
                           SET [DataInizio] = @DataInizio
                              ,[DataFine] = @DataFine
                              ,[IdUtente] = @IdUtente
                              ,[IdImpiegatoPrestito] = @IdImpiegatoPrestito
                              ,[IdImpiegatoRestituzione] =  @IdImpiegatoRestituzione
                              ,[IdOggetto] = @IdOggetto
                      WHERE [IdOggetto] = @IdOggetto
                            AND [IdPrestito]=@IdPrestito";
                using var command = new SqlCommand(sql, connection);
                
                command.Parameters.AddWithValue("@DataInizio", prestito.DataInizio);
                command.Parameters.AddWithValue("@DataFine", prestito.DataFine);
                command.Parameters.AddWithValue("@IdUtente", prestito.Utente.Id);
                command.Parameters.AddWithValue("@IdImpiegatoPrestito", prestito.ImpPrestito.Id);
                command.Parameters.AddWithValue("@IdImpiegatoRestituzione", prestito.ImpRestituzione.Id);
                
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Prestiti", "Update");
                return false;
            }
        }
        public abstract IEnumerable<Prestito<T>> GetPrestiti();
        public abstract Prestito<T> GetPrestito(int idPrestito);
    }
}
