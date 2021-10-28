namespace RepositoryBiblioteca.Model

{
    public class Creatore
    { 
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Nazionalità { get; set; }
        public bool Cancellato { get; set; }
    }
}
