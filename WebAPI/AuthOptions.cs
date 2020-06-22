using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI
{
    public class AuthOptions
    {
        public const string ISSUER = "FinanceServer";
        public const string AUDIENCE = "FinanceClient";
        const string KEY = "fin!na563!nv56!dsd!5d";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
