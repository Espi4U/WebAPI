using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels.Requests;
using Xamarin.Forms;

namespace FamilyFinance.Helpers
{
    public static class GlobalHelper
    {
        static GlobalHelper()
        {
            if (!Application.Current.Properties.ContainsKey("personid"))
            {
                SetPersonId(0);
            }
            if (!Application.Current.Properties.ContainsKey("familyid"))
            {
                SetFamilyId(0);
            }
            if (!Application.Current.Properties.ContainsKey("personname"))
            {
                SetPersonName("");
            }
            if (!Application.Current.Properties.ContainsKey("familyname"))
            {
                SetFamilyName("");
            }
            if (!Application.Current.Properties.ContainsKey("role"))
            {
                SetRole("");
            }
            if (!Application.Current.Properties.ContainsKey("token"))
            {
                SetToken("");
            }
        }
        public static int GetPersonId()
        {
            return Convert.ToInt32(Application.Current.Properties["personid"]);
        }

        public static int GetFamilyId()
        {
            return Convert.ToInt32(Application.Current.Properties["familyid"]);
        }

        public static string GetPersonName()
        {
            return Convert.ToString(Application.Current.Properties["personname"]);
        }

        public static string GetFamilyName()
        {
            return Convert.ToString(Application.Current.Properties["familyname"]);
        }

        public static string GetRole()
        {
            return Convert.ToString(Application.Current.Properties["role"]);
        }

        public static string GetToken()
        {
            return Convert.ToString(Application.Current.Properties["token"]);
        }

        public static void SetPersonId(int? id)
        {
            Application.Current.Properties["personid"] = id;
            Application.Current.SavePropertiesAsync();
        }

        public static void SetFamilyId(int? id)
        {
            Application.Current.Properties["familyid"] = id;
            Application.Current.SavePropertiesAsync();
        }

        public static void SetPersonName(string personName)
        {
            Application.Current.Properties["personname"] = personName;
            Application.Current.SavePropertiesAsync();
        }

        public static void SetFamilyName(string familyName)
        {
            Application.Current.Properties["familyname"] = familyName;
            Application.Current.SavePropertiesAsync();
        }
        public static void SetRole(string role)
        {
            Application.Current.Properties["role"] = role;
            Application.Current.SavePropertiesAsync();
        }

        public static void SetToken(string token)
        {
            Application.Current.Properties["token"] = token;
            Application.Current.SavePropertiesAsync();
        }

        public static void Logout()
        {
            Application.Current.Properties["familyid"] = null;
            Application.Current.Properties["personid"] = null;
            Application.Current.Properties["familyname"] = null;
            Application.Current.Properties["personname"] = null;
            Application.Current.Properties["role"] = null;
            Application.Current.Properties["token"] = null;
            Application.Current.SavePropertiesAsync();
        }

        public static BaseRequest GetBaseRequest()
        {
            return new BaseRequest
            {
                FamilyId = GetFamilyId(),
                PersonId = GetPersonId()
            };
        }
    }
}
