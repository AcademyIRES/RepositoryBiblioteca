using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;
using System.Data.SqlClient;

namespace RepositoryBiblioteca.Repository
{
    public interface IRegisti
    {
        Regista GetRegista(int idRegista);
        IEnumerable<Regista> GetRegista();
        int CreateRegista(Regista regista);
        bool UpdateRegista(Regista regista);
        bool DeleteRegista(int idRegista);
    }
}
