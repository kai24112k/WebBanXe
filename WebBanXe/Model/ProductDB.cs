using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebBanXe.Model
{
    public partial class PRODUCT
    {
        public string ToURL()
        {
            string stFormD = NameProduct.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');

            var newName = (sb.ToString().Normalize(NormalizationForm.FormD)) + "-" + IdProduct;
            newName = newName.Replace(" ", "-");
            newName = newName.Replace("--", "-");
            return newName;
        }

        public static int? GetIDFromURL(string url)
        {
            try {
                var split = url.Split('-').ToList();
                return int.Parse(split.Last());
            }
            catch
            {
                return null;
            }
        }
    }
}