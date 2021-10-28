using System.Collections.Generic;
using RepositoryBiblioteca.Model;

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
