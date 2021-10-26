using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public interface IAutore
    {
        Autore GetAutore(int idAutore);

        IEnumerable<Autore> GetAutori();

        int CreateAutore(Autore autore);

        bool UpdateAutore(Autore autore);

        bool DeleteAutore(int idAutore);
    }
}
