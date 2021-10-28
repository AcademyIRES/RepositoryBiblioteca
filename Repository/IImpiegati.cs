
using System.Collections.Generic;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public interface IImpiegati
    {
         Impiegato GetImpiegato(int idImpiegato);
        IEnumerable<Impiegato> GetImpiegato();
        int CreateImpiegato(Impiegato impiegato);
        bool UpdateImpiegato(Impiegato impiegato);                 
        bool DeleteImpiegato(int idImpiegato);
    }             
}
