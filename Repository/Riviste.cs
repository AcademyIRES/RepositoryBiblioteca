using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;
using System.Data.SqlClient;

namespace RepositoryBiblioteca.Repository
{
    public class Riviste : IOggetti<Rivista>
    {
        public bool Delete(int idRivista)
        {

            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Oggetti]
                                         SET Cancellato = 1
                                WHERE [IdOggetto] = @IdOggetto
                                AND [IdTipologiaOggetto] = 3";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdOggetto", idRivista);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Riviste", "Delete");
                return false;
            }
        }

        public Rivista Get(int idOggetto)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"SELECT [IdOggetto]
                                  ,[Titolo]
                                  ,[IdCategoria]
                                  ,[IdCasaProduzione]
                                  ,[DataUscita]
                                  ,[IdTipologiaOggetto]
                                  ,[Cancellato]
                              FROM [dbo].[Oggetti]
                        WHERE [IdOggetto] = @IdOggetto
                        AND [IdTipologiaOggeto] =3";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdOggetto", idOggetto);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessuna rivista trovata per l'Id richiesto {idOggetto}");

               
                var casaProduzione = new CaseProduzione();
                var categorie = new Categorie();


                datareader.Read();
                {
                    return new Rivista
                    {
                        IdOggetto= Convert.ToInt32(datareader["idOggetto"]),
                        CasaProduzione = casaProduzione.GetCasaProduzione(Convert.ToInt32(datareader["idCasaProduzione"])),
                        Categoria = categorie.GetCategoria(Convert.ToInt32(datareader["idCategoria"])),
                        DataUscita = Convert.ToDateTime(datareader["DataUscita"]),
                        Titolo = datareader["Titolo"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"])

                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Riviste", "Get");
                return null;
            }
        }

        public IEnumerable<Rivista> Get()
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [IdOggetto]
                                ,[Titolo]
                                ,[IdCategoria]
                                ,[IdCasaProduzione]
                                ,[DataUscita]
                                ,[Cancellato]
                                ,[IdTipologiaOggetto]
                            FROM [dbo].[Oggetti]
                            WHERE [IdTipologiaOggetto]=3";
            using var command = new SqlCommand(sql, connection);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                
                var casaProduzione = new CaseProduzione();
                var categorie = new Categorie();
                while (datareader.Read())
                {
                    yield return new Rivista
                    {
                        IdOggetto = Convert.ToInt32(datareader["idOggetto"]),                       
                        CasaProduzione = casaProduzione.GetCasaProduzione(Convert.ToInt32(datareader["IdCasaProduzione"])),
                        Categoria = categorie.GetCategoria(Convert.ToInt32(datareader["idCategoria"])),
                        DataUscita = Convert.ToDateTime(datareader["DataUscita"]),
                        Titolo = datareader["Titolo"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"])
                    };
                }
            }
        }

        public int Insert(Rivista rivista)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @" INSERT INTO [dbo].[Oggetti]
                            (
                            [Titolo]
                            ,[IdCategoria]
                            ,[IdCasaProduzione]
                            ,[DataUscita]
                            ,[IdTipologiaOggetto]
                             VALUES
                                (
                                 [Titolo]=@Titolo
                                ,[IdCategoria] = @IdCategoria
                                ,[IdCasaProduzione] =@IdCasaProduzione
                                ,[DataUscita] =@DataUscita
                                ,[IdTipologiaOggetto] =3
                                 );
                                 SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                var categorie = new Categorie();
                var casaProduzione = new CaseProduzione();
                command.Parameters.AddWithValue("@Titolo", rivista.Titolo);
                command.Parameters.AddWithValue("@IdCategoria", rivista.Categoria.IdCategoria);
                command.Parameters.AddWithValue("@IdCasaProduzione", rivista.CasaProduzione.IdCasaProduzione);
                command.Parameters.AddWithValue("@DataUscita", rivista.DataUscita);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Riviste", "Insert");
                return -1;
            }
        }

        public bool Update(Rivista rivista)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Oggetti]
                               SET [Titolo] = @Titolo
                                  ,[IdCategoria] = @IdCategoria
                                  ,[IdCasaProduzione] = @IdCasaProduzione
                                  ,[DataUscita] = @DataUscita
                                  ,[Cancellato] = @Cancellato
                      WHERE [IdOggetto] = @IdOggetto
                            AND [IdTipologiaOggetto]=3";
                using var command = new SqlCommand(sql, connection);
                var categorie = new Categorie();
                var casaProduzione = new CaseProduzione();
                command.Parameters.AddWithValue("@IdOggetto", rivista.IdOggetto);
                command.Parameters.AddWithValue("@Titolo", rivista.Titolo);
                command.Parameters.AddWithValue("@IdCategoria", rivista.Categoria.IdCategoria);
                command.Parameters.AddWithValue("@IdCasaProduzione", rivista.CasaProduzione.IdCasaProduzione);
                command.Parameters.AddWithValue("@DataUscita", rivista.DataUscita);
                command.Parameters.AddWithValue("@Cancellato", rivista.Cancellato);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Rivista", "Update");
                return false;
            }
        }
    }
    
}
