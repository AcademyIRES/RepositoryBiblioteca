using System.Collections.Generic;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public interface IAutori
    {
        Autore GetAutore(int idAutore);

        IEnumerable<Autore> GetAutori();

        int CreateAutore(Autore autore);

        bool UpdateAutore(Autore autore);

        bool DeleteAutore(int idAutore);

        IEnumerable<Autore> GetAutoriByState(bool deleted);
    }
}
