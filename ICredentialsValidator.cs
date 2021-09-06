using tovuti_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tovuti_api
{
   public interface ICredentialsValidator
    {
        bool IsValid(Credentials creds);
    }
}
