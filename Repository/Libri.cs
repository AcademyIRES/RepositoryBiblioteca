using RepositoryBiblioteca.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RepositoryBiblioteca.Repository
{
    public class Libri : IOggetti<Libro>
    {
        public bool Delete(int idLibro)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Oggetti]
                                SET Cancellato = 1
                 WHERE [IdOggetto] = @IdOggetto
                        AND [IdTipologiaOggetto] = 1";
                using var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Libri", "Delete");
                return false;
            }
        }

        public IEnumerable<Libro> Get()
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [IdOggetto]
                                ,[Titolo]
                                ,[IdCategoria]
                                ,[IdCasaProduzione]
                                ,[DataUscita]
                                ,[IdAutore]
                                ,[IdTipologiaOggetto]
                                ,[Cancellato]
                            FROM [dbo].[Oggetti]
                             WHERE [IdTipologiaOggetto]=1";
            using var command = new SqlCommand(sql, connection);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                var autori = new Autori();
                var casaProduzione = new CaseProduzione();
                var categorie = new Categorie();
                while (datareader.Read())
                {
                    yield return new Libro
                    {
                        IdOggetto = Convert.ToInt32(datareader["idOggetto"]),
                        Autore = autori.GetAutore(Convert.ToInt32(datareader["IdAutore"])),
                        CasaProduzione = casaProduzione.GetCasaProduzione(Convert.ToInt32(datareader["IdCasaProduzione"])),
                        Categoria = categorie.GetCategoria(Convert.ToInt32(datareader["idCategoria"])),
                        DataUscita = Convert.ToDateTime(datareader["DataUscita"]),
                        Titolo = datareader["Titolo"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"])
                    };
                }
            }
        } 

        public Libro Get(int idOggetto)

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
                                  ,[IdAutore]
                                  ,[IdTipologiaOggetto]
                                  ,[Cancellato]
                              FROM [dbo].[Oggetti]
                        WHERE [IdOggetto] = @IdOggetto
                        AND [IdTipologiaOggeto] =1";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdOggetto", idOggetto);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessun libro trovato per l'Id richiesto {idOggetto}");

                var autori = new Autori();
                var casaProduzione = new CaseProduzione();
                var categorie = new Categorie();
             

                datareader.Read();
                {
                    return new Libro
                    {
                        IdOggetto = Convert.ToInt32(datareader["idOggetto"]),
                        Autore = autori.GetAutore(Convert.ToInt32(datareader["IdAutore"])),
                        CasaProduzione = casaProduzione.GetCasaProduzione(Convert.ToInt32(datareader["IdCasaProduzione"])),
                        Categoria = categorie.GetCategoria(Convert.ToInt32(datareader["idCategoria"])),
                        DataUscita = Convert.ToDateTime(datareader["DataUscita"]),
                        Titolo = datareader["Titolo"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"])
                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Libri", "Get");
                return null;
            }
        }

        public int Insert(Libro libro)
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
                            ,[IdAutore]
                            ,[IdTipologiaOggetto]
                             VALUES
                                (
                                 [Titolo]=@Titolo
                                ,[IdCategoria] = @IdCategoria
                                ,[IdCasaProduzione] =@IdCasaProduzione
                                ,[DataUscita] =@DataUscita
                                ,[IdAutore] = @IdAutore
                                ,[IdTipologiaOggetto] =1
                                 );
                                 SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                var categorie = new Categorie();
                var autori = new Autori();
                var casaProduzione = new CaseProduzione();
                command.Parameters.AddWithValue("@Titolo", libro.Titolo);
                command.Parameters.AddWithValue("@IdCategoria", libro.Categoria.IdCategoria);
                command.Parameters.AddWithValue("@IdCasaProduzione", libro.CasaProduzione.IdCasaProduzione);
                command.Parameters.AddWithValue("@DataUscita", libro.DataUscita);
                command.Parameters.AddWithValue("@IdAutore", libro.Autore.Id);
                command.Parameters.AddWithValue("@Cancellato", libro.Cancellato);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Libri", "Insert");
                return -1;
            }
        }

        public bool Update(Libro libro)
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
                                  ,[IdAutore] = @IdAutore
                                  ,[IdTipologiaOggetto] = @IdTipologiaOggetto
                                  ,[Cancellato] = @Cancellato
                      WHERE [IdOggetti] = @IdOggetti
                            AND [IdTipologiaOggetto]=1";
                using var command = new SqlCommand(sql, connection);
                var categorie = new Categorie();
                var autori = new Autori();
                var casaProduzione = new CaseProduzione();
                command.Parameters.AddWithValue("@Titolo", libro.Titolo);
                command.Parameters.AddWithValue("@IdCategoria", libro.Categoria.IdCategoria);
                command.Parameters.AddWithValue("@IdCasaProduzione", libro.CasaProduzione.IdCasaProduzione);
                command.Parameters.AddWithValue("@DataUscita", libro.DataUscita);
                command.Parameters.AddWithValue("@IdAutore", libro.Autore.Id);
                command.Parameters.AddWithValue("@Cancellato", libro.Cancellato);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Libri", "Update");
                return false;
            }
        }
    }
    
}
