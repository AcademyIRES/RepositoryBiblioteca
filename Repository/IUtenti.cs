using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public interface IUtenti
    {
        Utente GetUtente(int idUtente);
        IEnumerable<Utente> GetUtenti();
        int CreateUtente(Utente utente);
        bool UpdateUtente(Utente utente);
        bool DeleteUtente(int idUtente);

    }
}
