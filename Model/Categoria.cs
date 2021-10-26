using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBiblioteca.Model
{
    public class Categoria
    {
        public int IdCategoria { get; set; }

        public string Nome { get; set; }

        public bool Cancellato { get; set; }
    }
}
