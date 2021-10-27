using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;


namespace RepositoryBiblioteca.Repository
{
    public interface IPrestiti<T> where T: Oggetto
    {
        int CreatePrestito(Prestito<T> prestito);
        Prestito<T> GetPrestito(int idPrestito);
        IEnumerable<Prestito<T>> GetPrestiti();
        bool UpdatePrestito(Prestito<T> prestito);
    }
}
