using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tovuti_api.Models;

namespace tovuti_api
{
    interface ITokenValidator
    {

        bool IsValid(string token);
        Token Token { get; set; }
    }
}
