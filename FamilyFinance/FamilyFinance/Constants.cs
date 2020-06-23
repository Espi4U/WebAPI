using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyFinance
{
    public static class Constants
    {
        public static string NameLatinPattern = "^[a-zA-Z][a-zA-Z0-9]{3,30}$";
        public static string NameLatinAndCyrylicPattern = @"^[a-zA-Zа-яєїіґА-ЯЄЇІҐ\s][a-zа-яєїіґA-ZА-ЯЄЇІҐ0-9\s]{3,30}$";
        public static string EmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public static string PositiveDigitsPattern = @"^\d*[1-9]\d*$";
    }
}
