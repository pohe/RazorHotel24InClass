namespace RazorHotel24InClass.Services
{
    public static class Secret
    {
        private static string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        //Dette er en test af secret...
        public static string ConnectionString
        {
            get { return _connectionString; }

        }
    }
    
}
