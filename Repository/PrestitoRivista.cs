using RepositoryBiblioteca.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepositoryBiblioteca.Repository
{
    public class PrestitoRivista : Prestiti<Rivista>
    {
        public override IEnumerable<Prestito<Rivista>> GetPrestiti()
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT SELECT [IdPrestito]
                                      ,[DataInizio]
                                      ,[DataFine]
                                      ,[IdUtente]
                                      ,[IdImpiegatoPrestito]
                                      ,[IdImpiegatoRestituzione]
                                      ,[IdOggetto]
                            FROM [dbo].[Prestiti] p 
                                        join Oggetto o on p.idoggetto = o.idoggetto
                                WHERE o.idtipologiaoggetto =3";

            using var command = new SqlCommand(sql, connection);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                var impiegati = new Impiegati();
                var utente = new Utenti();
                var riviste = new Riviste();

                while (datareader.Read())
                {
                    yield return new Prestito<Rivista>
                    {
                        IdPrestito = Convert.ToInt32(datareader["IdPrestito"]),
                        DataInizio = Convert.ToDateTime(datareader["DataInizio"]),
                        DataFine = Convert.ToDateTime(datareader["DataFine"]),
                        ImpPrestito = impiegati.GetImpiegato(Convert.ToInt32(datareader["IdImpiegatoPrestito"])),
                        ImpRestituzione = impiegati.GetImpiegato(Convert.ToInt32(datareader["IdImpiegatoRestituzione"])),
                        Utente = utente.GetUtente(Convert.ToInt32(datareader["IdUtente"])),
                        Oggetto = riviste.Get(Convert.ToInt32(datareader["IdOggetto"]))
                    };
                }
            }
        }

        public override Prestito<Rivista> GetPrestito(int idPrestito)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"SELECT [IdPrestito]
                                      ,[DataInizio]
                                      ,[DataFine]
                                      ,[IdUtente]
                                      ,[IdImpiegatoPrestito]
                                      ,[IdImpiegatoRestituzione]
                                      ,[IdOggetto]
                                  FROM [dbo].[Prestiti] p 
                                        join Oggetto o on p.idoggetto = o.idoggetto
                                WHERE [IdPrestito]= @idPrestito and o.idtipologiaoggetto =3";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdPrestito", idPrestito);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessun prestito trovato per l'Id richiesto {idPrestito}");

                var impiegati = new Impiegati();
                var utente = new Utenti();
                var rivista = new Riviste();
                datareader.Read();
                {
                    return new Prestito<Rivista>
                    {
                        IdPrestito = Convert.ToInt32(datareader["IdPrestito"]),
                        DataInizio = Convert.ToDateTime(datareader["DataInizio"]),
                        DataFine = Convert.ToDateTime(datareader["DataFine"]),
                        ImpPrestito = impiegati.GetImpiegato(Convert.ToInt32(datareader["IdImpiegatoPrestito"])),
                        ImpRestituzione = impiegati.GetImpiegato(Convert.ToInt32(datareader["IdImpiegatoRestituzione"])),
                        Utente = utente.GetUtente(Convert.ToInt32(datareader["IdUtente"])),
                        Oggetto = rivista.Get(Convert.ToInt32(datareader["IdOggetto"]))
                    };
                }

            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "PrestitoRivista", "GetPrestito");
                return null;
            }
        }
    }
}
