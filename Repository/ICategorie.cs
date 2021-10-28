using System.Collections.Generic;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public interface ICategorie
    {
        Categoria GetCategoria(int idCategoria);

        IEnumerable<Categoria> GetCategorie();

        int CreateCategoria(Categoria categoria);

        bool DeleteCategoria(int idCategoria);

        bool UpdateCategoria(Categoria categoria);



        
    }
}
