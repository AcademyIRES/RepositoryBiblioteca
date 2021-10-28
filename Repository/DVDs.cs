using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;
using System.Data.SqlClient;

namespace RepositoryBiblioteca.Repository
{
    public class DVDs : IOggetti<DVD>
    {
        public bool Delete(int id)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Oggetti]
                                SET Cancellato = 1
                 WHERE [IdOggetto] = @IdOggetto
                        AND [IdTipologiaOggetto] = 2";
                using var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "DVDs", "Delete");
                return false;
            }
        }

        public DVD Get(int idOggetto)
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
                                  ,[Idregista]
                                  ,[IdTipologiaOggetto]
                                  ,[Cancellato]
                              FROM [dbo].[Oggetti]
                        WHERE [IdOggetto] = @IdOggetto
                        AND [IdTipologiaOggeto] =2";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdOggetto", idOggetto);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessun DVD trovato per l'Id richiesto {idOggetto}");

                var registi = new Registi();
                var casaProduzione = new CaseProduzione();
                var categorie = new Categorie();


                datareader.Read();
                {
                    return new DVD
                    {
                        IdOggetto = Convert.ToInt32(datareader["idOggetto"]),
                        CasaProduzione = casaProduzione.GetCasaProduzione(Convert.ToInt32(datareader["idCasaProduzione"])),
                        Categoria = categorie.GetCategoria(Convert.ToInt32(datareader["idCategoria"])),
                        Regista = registi.GetRegista(Convert.ToInt32(datareader["IdRegista"])),
                        DataUscita = Convert.ToDateTime(datareader["DataUscita"]),
                        Titolo = datareader["Titolo"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"])


                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "DVDs", "Get");
                return null;
            }
        }

        public IEnumerable<DVD> Get()
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [IdOggetto]
                                ,[Titolo]
                                ,[IdCategoria]
                                ,[IdCasaProduzione]
                                ,[DataUscita]
                                ,[IdRegista]
                                ,[IdTipologiaOggetto]
                            FROM [dbo].[Oggetti]
                            WHERE [IdTipologiaOggetto]=2";
            using var command = new SqlCommand(sql, connection);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {

                var casaProduzione = new CaseProduzione();
                var categorie = new Categorie();
                var registi = new Registi();
                while (datareader.Read())
                {
                    yield return new DVD
                    {
                        IdOggetto = Convert.ToInt32(datareader["idOggetto"]),
                        Regista = registi.GetRegista(Convert.ToInt32(datareader["IdRegista"])),
                        CasaProduzione = casaProduzione.GetCasaProduzione(Convert.ToInt32(datareader["IdCasaProduzione"])),
                        Categoria = categorie.GetCategoria(Convert.ToInt32(datareader["idCategoria"])),
                        DataUscita = Convert.ToDateTime(datareader["DataUscita"]),
                        Titolo = datareader["Titolo"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"])
                    };
                }
            }
        }

        public int Insert(DVD dischi)
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
                            ,[Idregista]
                            ,[IdTipologiaOggetto]
                             VALUES
                                (
                                 [Titolo]=@Titolo
                                ,[IdCategoria] = @IdCategoria
                                ,[IdCasaProduzione] =@IdCasaProduzione
                                ,[DataUscita] =@DataUscita
                                ,[Idregista] = @IdRegista
                                ,[IdTipologiaOggetto] =2
                                 );
                                 SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                var categorie = new Categorie();
                var casaProduzione = new CaseProduzione();
                command.Parameters.AddWithValue("@Titolo", dischi.Titolo);
                command.Parameters.AddWithValue("@IdCategoria", dischi.Categoria.IdCategoria);
                command.Parameters.AddWithValue("@IdCasaProduzione", dischi.CasaProduzione.IdCasaProduzione);
                command.Parameters.AddWithValue("@DataUscita", dischi.DataUscita);
                command.Parameters.AddWithValue("@IdRegista", dischi.Regista.Id);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "DVDs", "Insert");
                return -1;
            }
        }

        public bool Update(DVD dischi)
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
                                  ,[IdTipologiaOggetto] = @IdTipologiaOggetto
            \                     ,[IdRegista] = @IdRegista
                                  ,[Cancellato] = @Cancellato
                      WHERE [IdOggetto] = @IdOggetto
                            AND [IdTipologiaOggetto]=2";
                using var command = new SqlCommand(sql, connection);
                var categorie = new Categorie();
                var casaProduzione = new CaseProduzione();
                command.Parameters.AddWithValue("@Titolo", dischi.Titolo);
                command.Parameters.AddWithValue("@IdCategoria", dischi.Categoria.IdCategoria);
                command.Parameters.AddWithValue("@IdCasaProduzione", dischi.CasaProduzione.IdCasaProduzione);
                command.Parameters.AddWithValue("@DataUscita", dischi.DataUscita);
                command.Parameters.AddWithValue("@Cancellato", dischi.Cancellato);
                command.Parameters.AddWithValue("@IdRegista", dischi.Regista.Id);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "DVDs", "Update");
                return false;
            }
        }
    }
}
