using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace establishment_service.Validate
{
    public class ValidateService
    {
        public bool ValidCNPJ(string cnpj)
        {
            var multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum, rest = 0;
            string digit, tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)

                return false;

            tempCnpj = cnpj.Substring(0, 12);
            sum = 0;

            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];
            }


            rest = (sum % 11);

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();
            tempCnpj = tempCnpj + digit;
            sum = 0;

            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];
            }

            rest = (sum % 11);

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            var valid = cnpj.EndsWith(digit);

            if (valid == false)
                throw new Exception("CNPJ inválido!");

            return valid;
        }

        public bool ValidEmail(string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (!rg.IsMatch(email))
                throw new Exception("Email inválido!");

            return true;
        }
    }
}
