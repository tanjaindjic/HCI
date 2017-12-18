using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Projekat.Validacija
{
    class OznakaValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string err = "*".PadLeft(1);
            string err2 = "!".PadLeft(1);
            try
            {
                var oznaka = value as string;
                if (String.IsNullOrWhiteSpace(oznaka))
                {
                    return new ValidationResult(false, err);
                }
                Regex r = new Regex(@"^[šŠđĐčČćĆžŽa-zA-Z0-9_' ']+$");
                if (!r.IsMatch(oznaka))
                {
                    // validation failed
                
                    return new ValidationResult(false, err2);
                }
                return new ValidationResult(true,null);
            }
            catch 
            {

                return new ValidationResult(false, "Greska!");
            } 
        }
    }
}
