using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RepositoryBiblioteca
{
   public static class LogError
    {
        public static void Write(string errore, string classe, string metodo)
        {
            var log = $"{DateTime.Now} - {classe} - {metodo} - {errore} {Environment.NewLine}";
            File.AppendAllText("error.log", log);
        }

    }

}
