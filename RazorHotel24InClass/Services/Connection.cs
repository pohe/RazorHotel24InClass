using Microsoft.AspNetCore.DataProtection;

namespace RazorHotel24InClass.Services
{
    public class Connection
    {
        protected String connectionString = Secret.ConnectionString;
    }
}
