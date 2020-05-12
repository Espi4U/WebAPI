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
            SetPersonId(null);
            SetFamilyId(null);
            SetPersonName(null);
            SetFamilyName(null);
            SetRole(null);
        }

        public static int GetPersonId()
        {
            return Convert.ToInt32(Application.Current.Properties["personid"]);
            //return 1;
        }

        public static int GetFamilyId()
        {
            return Convert.ToInt32(Application.Current.Properties["familyid"]);
            //return 1;
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

        public static void Logout()
        {
            Application.Current.Properties["familyid"] = null;
            Application.Current.Properties["personid"] = null;
            Application.Current.Properties["familyname"] = null;
            Application.Current.Properties["personname"] = null;
            Application.Current.Properties["role"] = null;
            Application.Current.SavePropertiesAsync();
        }

        public static BaseRequest GetBaseRequest()
        {
            //return new BaseRequest
            //{
            //    FamilyId = GetFamilyId(),
            //    PersonId = GetPersonId()
            //};
            return new BaseRequest
            {
                FamilyId = 1,
                PersonId = 1
            };
        }
    }
}
