using System;


namespace RepositoryBiblioteca.Model
{
    public class Oggetto
    {
        public int IdOggetto { get; set; }
        public string Titolo { get; set; }
        public DateTime DataUscita { get; set; }
        public Categoria Categoria { get; set; }
        public CasaProduzione CasaProduzione { get; set; }
        public bool Cancellato { get; set; }
    }
}
