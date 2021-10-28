namespace RepositoryBiblioteca
{
    public static class Connection
    {
        private static string ConnectionString = @"Server=UMANA-1-MULTI\SQLEXPRESS01; Database= Biblioteca;  Trusted_Connection=True;";
        public static string GetConnectionString() => ConnectionString;
    }
}
