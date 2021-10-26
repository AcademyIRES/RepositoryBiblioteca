using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBiblioteca.Model
{
    public class CasaProduzione
    {
        public int IdCasaProduzione { get; set; }
        public string Nome { get; set; }
        public string Nazionalità { get; set; }
        public bool Cancellato { get; set; }
    }
}
