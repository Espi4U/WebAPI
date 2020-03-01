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
        public const string ISSUER = "FamilyFinanceWEBAPI"; // издатель токена
        public const string AUDIENCE = "FamilyFinanceDroid"; // потребитель токена
        const string KEY = "sHa4kk8GAl64pC1";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
