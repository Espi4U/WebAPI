using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FamilyFinance.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidName(string name)
        {
            var nameWithoutSpaces = name.Trim(' ');
            if (string.IsNullOrWhiteSpace(nameWithoutSpaces))
            {
                return false;
            }
            else if(Regex.IsMatch(name, @"^[\p{L}\p{M}' \.\-]+$"))
            {
                return true;
            }
            return false;
        }
    }
}
