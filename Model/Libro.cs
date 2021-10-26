using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBiblioteca.Model
{
    public class Libro : Oggetto
    {
        public Autore Autore { get; set; }
    }
}
