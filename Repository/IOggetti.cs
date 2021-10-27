using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public interface IOggetti<T> where T:Oggetto
    {
        T Get(int idOggetto);
        int Insert(T oggetto);
        bool Delete(int id);
        IEnumerable<T> Get();
        bool Update(T oggetto);
    }
}
