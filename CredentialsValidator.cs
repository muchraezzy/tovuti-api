using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using tovuti_api.Models;
using System.Linq;

namespace tovuti_api
{
    public class CredentialsValidator : ICredentialsValidator
    {
        private readonly ApplicationDbContext _DbContext;

        public CredentialsValidator(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public bool IsValid(Credentials creds)
        {
            // Check for valid creds here
            // I compare using hashes only for example purposes

            var user = _DbContext.Users.Where(u => u.username.ToLower() == creds.Username.ToLower()).FirstOrDefault();
            return user != null && Hash.Compare(creds.Password, user.pass, Hash.DefaultHashType, Hash.DefaultEncoding);


        }
    }
}