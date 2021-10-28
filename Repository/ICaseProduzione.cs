using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;
using System.Data.SqlClient;

namespace RepositoryBiblioteca.Repository
{
    public interface ICaseProduzione
    {
        CasaProduzione GetCasaProduzione(int idCasaProduzione);
        IEnumerable<CasaProduzione> GetCasaProduzione();
        int CreateCasaProduzione(CasaProduzione casaproduzione);
        bool UpdateCasaProduzione(CasaProduzione casaproduzione);
        IEnumerable<CasaProduzione> GetCasaProduzione(bool deleted);
    }
}
