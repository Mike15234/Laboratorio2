using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio2_EDD2.RSA
{
    public class VerificacionNumeros
    {
        public bool esPrimo(int numero)
        {
            int divisor = 2;
            int resto = 0;
            while (divisor < numero)
            {
                resto = numero % divisor;
                if (resto == 0)
                {
                    return false;
                }
                divisor = divisor + 1;
            }
            return true;
        }

        public int div(int a, int b)
        {
            var t = 0;
            while (b != 0)
            {
                t = a;
                a = b;
                b = t % b;
            }
            return a;
        }

        public bool coprimo(int a, int b)
        {
            return div(a, b) == 1;
        }
    }
}
