using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLastText
{
    public class Provetochka
    {
        public static string Proverka(string newpyt)
        {
            string proverka = Path.GetExtension(newpyt);
            return proverka;
        }
    }
}
