using System;
using System.Collections.Generic;
using System.Text;
using leoMadeirasAPI.RegexTools;

namespace leoMadeirasAPI.Tools
{
    public class GeradorDeSenha
    {
        public GeradorDeSenha()
        {
        }
        public static string GerarSenha()
        {
            int tamanhoSenha = 15;
            string maiusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZÇ";
            string minusculas = maiusculas.ToLower();
            string especiais = "!@#$%&";
            string numeros = "1234567890";
            List<string> lista = new List<string>() { maiusculas, numeros, minusculas, especiais };
            StringBuilder res = new StringBuilder();
            Random rd = new Random();

            while (res.Length < tamanhoSenha)
            {
                var stringChoice = lista[rd.Next(0, 4)];
                var CharChoice = stringChoice[rd.Next(stringChoice.Length)];

                if (!res.ToString().Contains(CharChoice))
                {
                    res.Append(CharChoice);
                }
            }

            var senha = res.ToString();

            var reg = new RegexValidator();

            var verifica = reg.ValidarSenha(senha);

            while (!verifica)
            {
                senha = GerarSenha();
            }

            return senha;
        }
    }
}