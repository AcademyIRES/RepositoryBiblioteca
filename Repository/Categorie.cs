using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public class Categorie : ICategorie
    {

        public int CreateCategoria(Categoria categoria)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @"INSERT INTO [dbo].[Categorie] ([Nome])
                 VALUES
                      (@Nome); SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", categoria.Nome);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch(Exception error)
            {
                LogError.Write(error.Message, "Categoria", "CreateCategoria");
                return -1;
            }
        }
        

        public bool DeleteCategoria(int idCategoria)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @"UPDATE [dbo].[Categorie]
                 SET Cancellato = 1
                 WHERE [IdCategoria] = @IdCategoria";
                using var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Categoria", "DeleteCategoria");
                return false;
            }
        }

        public Categoria GetCategoria(int idCategoria)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @"SELECT [IdCategoria]
                      ,[Nome]
                      ,[Cancellato]
                   FROM [dbo].[Categorie]
                   WHERE [IdCategoria] = @IdCategoria";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdCategoria", idCategoria);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows) 
                    throw new Exception($"Nessuna categoria trovata per l'Id richiesto {idCategoria}");

                datareader.Read();
                {
                    return new Categoria
                    {
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                        IdCategoria = Convert.ToInt32(datareader["IdCategoria"]),
                        Nome = datareader["Nome"].ToString()
                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Categoria", "GetCategoria");
                return null;
            }
        }

        public IEnumerable<Categoria> GetCategorie()
        {
            using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @"SELECT [IdCategoria]
                      ,[Nome]
                      ,[Cancellato]
                  FROM [dbo].[Categorie]";
                using var command = new SqlCommand(sql, connection);
                using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    yield return new Categoria
                    {
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                        IdCategoria = Convert.ToInt32(datareader["IdCategoria"]),
                        Nome = datareader["Nome"].ToString()
                    };
                }
            }
            
        }

        public bool UpdateCategoria(Categoria categoria)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnection());
                connection.Open();
                var sql = @"UPDATE [dbo].[Categorie]
                 SET Cancellato = @Cancellato,
                           Nome = @Nome
                      WHERE [IdCategoria] = @IdCategoria";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Cancellato", categoria.Cancellato);
                command.Parameters.AddWithValue("@Nome", categoria.Nome);
                command.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Categoria", "UpdateCategoria");
                return false;
            }
        }
    }
}
