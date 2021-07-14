using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanXe.Helpers
{
    public class ValidateHelper
    {
        public static bool ValidateUsername(string username)
        {
            if (string.IsNullOrEmpty(username)) return false;

            // Check
            foreach (var c in username)
            {
                if (!char.IsLetterOrDigit(c)) return false;
            }

            return true;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return false;

            // Check
            if (!phone.StartsWith("0")) return false;
            foreach (var c in phone)
            {
                if (!char.IsDigit(c)) return false;
            }

            return true;
        }

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            // Check
            return true;
        }

        public static bool IsValidAdress(string adress)
        {
            if (string.IsNullOrEmpty(adress)) return false;

            // Check
            return true;
        }

        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;

            // Check
            foreach (var c in password)
            {
                if (!char.IsLetterOrDigit(c)) return false;
            }

            return true;
        }
    }
}