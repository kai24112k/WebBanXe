using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanXe.Helpers.Facebook
{
    public class FacebookHelper
    {
        /// <summary>
        /// Lấy link đăng nhập
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetLinkLogin(string uri)
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = FacebookSetting.IDApp,
                client_secret = FacebookSetting.SecretKey,
                redirect_uri = uri,
                response_type = "code",
                scope = "email",
            });

            return loginUrl.AbsoluteUri;
        }

        /// <summary>
        /// Phân tích facebook call back
        /// </summary>
        /// <param name="code"></param>
        public static FacebookUserModel GetFacebookCallback(string uri, string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }

            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = FacebookSetting.IDApp,
                client_secret = FacebookSetting.SecretKey,
                redirect_uri = uri,
                code = code
            });
            
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=name,id,email");

                return new FacebookUserModel
                {
                    Name = me.name,
                    Email = me.email
                };
            }

            return null;
        }
    } 
}