﻿using System;

namespace RepositoryBiblioteca.Model
{
    public class Prestito<T> where T : Oggetto
    {
        public int IdPrestito { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public Utente Utente { get; set; }
        public Impiegato ImpPrestito { get; set; }
        public Impiegato ImpRestituzione { get; set; }
        public T Oggetto { get; set; }

    }


    public class Prestito
    {
        public int IdPrestito { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public int Utente { get; set; }
        public int ImpPrestito { get; set; }
        public int ImpRestituzione { get; set; }
        public int IdObj { get; set; }

    }
}
