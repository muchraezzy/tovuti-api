using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tovuti_api.Models;

namespace tovuti_api
{
    public class TokenValidator : ITokenValidator
    {
        public static double DefaultSecondsUntilTokenExpires = 1800;

        private readonly ApplicationDbContext _DbContext;
        public TokenValidator(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public bool IsValid(string tokentext)
        {
            var token = _DbContext.Tokens.SingleOrDefault(t => t.Text == tokentext);
            if (token != null && !IsExpired(token))
            {
                token.LastTimeUsed = DateTime.Now;
                _DbContext.SaveChanges();
                return true;
            }
            return false;
        }

        internal bool IsExpired(Token token)
        {
            var span = DateTime.Now - token.LastTimeUsed;
            return span.TotalSeconds > DefaultSecondsUntilTokenExpires;
        }
        public Token Token { get; set; }
    }
}