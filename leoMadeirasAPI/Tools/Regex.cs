using System.Text.RegularExpressions;

namespace leoMadeirasAPI.RegexTools
{
    public class RegexValidator
    {
        public bool ValidarSenha(string password)
        {
            var pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[$*&@#])(?:([0-9a-zA-Z$*&@#])(?!\1)).{14,}$";
            Regex rx = new Regex(pattern);

            var valida = rx.Match(password);

            if (valida.Success)
            {
                return true;
            }
            return false;
        }

    }
}