using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FamilyFinance.Helpers
{
    public static class GlobalHelper
    {
        public static void WriteToken(string token)
        {
            Application.Current.Properties["token"] = token;
            Application.Current.SavePropertiesAsync();
        }

        public static string GetToken()
        {
            return Application.Current.Properties["token"].ToString();
        }

        public static void WritePersonId(int id)
        {
            Application.Current.Properties["personid"] = id;
            Application.Current.SavePropertiesAsync();
        }

        public static int GetPersonId()
        {
            return Convert.ToInt32(Application.Current.Properties["personid"]);
        }

        public static int GetFamilyId()
        {
            return Convert.ToInt32(Application.Current.Properties["familyid"]);
        }

        public static void SetPersonId(int id)
        {
            Application.Current.Properties["familyid"] = id;
            Application.Current.SavePropertiesAsync();
        }
    }
}
