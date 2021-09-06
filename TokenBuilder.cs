using tovuti_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Web;


namespace tovuti_api
{
    public class TokenBuilder : ITokenBuilder
    {
        public static int TokenSize = 100;
        private readonly ApplicationDbContext _DbContext;

        public TokenBuilder(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public string Build(Credentials creds)
        {
            if (!new CredentialsValidator(_DbContext).IsValid(creds))
            {
                throw new AuthenticationException();
            }
            var token = BuildSecureToken(TokenSize);
            var user = _DbContext.Users.SingleOrDefault(u => u.username.Equals(creds.Username, StringComparison.CurrentCultureIgnoreCase));
            if (user == null)
                user = _DbContext.Users.SingleOrDefault(u => u.username.Equals(creds.Username, StringComparison.CurrentCultureIgnoreCase));

            try
            {
                _DbContext.Tokens.Add(new Token { tid= 0 ,Text = token, uid = user.id, CreateDate = DateTime.Now, LastTimeUsed = DateTime.Now });
                _DbContext.SaveChanges();
            }
            catch (Exception Ex)
            {

            }
            return token;
        }

        private string BuildSecureToken(int length)
        {
            var buffer = new byte[length];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetNonZeroBytes(buffer);
            }
            return Convert.ToBase64String(buffer);
        }
    }
}