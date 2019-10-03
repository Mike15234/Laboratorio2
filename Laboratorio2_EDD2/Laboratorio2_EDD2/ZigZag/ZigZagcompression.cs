using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Laboratorio2_EDD2.Helpers;

namespace Laboratorio2_EDD2.ZigZag
{
    public class ZigZagcompression
    {
        public string ZigZag(string textoCompleto, int llave)
        {
            int zz = 0;
            int fila = 0;
            int columna = 0;
            int P1 = 2 * (llave) - 1;
            int V1 = 2;
            int V2 = 1;
            int Lmax = 0;

            while (Lmax<textoCompleto.Length && P1 <textoCompleto.Length)
            {
                Lmax = P1 * V1 - V2;
                V1 += 1;
                V2 += 1;
            }

            if (P1 > Lmax)
            {
                Lmax = P1;
            }
            string[,] zigzag = new string[llave, Lmax];
            for (int i = 0; i < Lmax; i++)
            {
                if (i<textoCompleto.Length)
                {
                    zigzag[fila, columna] = textoCompleto.Substring(i, 1);
                }
                else
                {
                    zigzag[fila, columna] = "$";
                }
               
                columna = columna + 1;
                if (fila== llave-1)
                {
                    if (zz == 0)
                    {
                        zz = 1;
                    }
                    else
                    {
                        zz = 0;
                        fila = 0;
                    }
                }
                if (zz == 1 && fila ==0)
                {
                    zz = 0;
                }
                if (zz == 0)
                {
                    fila += 1;
                }
                else
                {
                    fila -= 1;
                }
            }
            ///// escribe el cifrado
            string cifrado = string.Empty;
            for (var i = 0; i < llave; i++)
            {
                for (var j = 0; j < Lmax; j++)
                {
                    cifrado += zigzag[i, j];
                }
            }
            return cifrado;
        }

        public string Decifrarzigzag(string textoCifrado, int llave)
        {
            string[,] zigzagDecifrar= new string[llave, textoCifrado.Length];

            int m = ((textoCifrado.Length-3)/((2*llave)-2));
            string nivel1 = textoCifrado.Substring(0, m);
            string medios = textoCifrado.Substring((m + 1), (textoCifrado.Length - (m - 1) - 1));
            string ultimoNivel = textoCifrado.Substring(textoCifrado.Length-(m - 1),textoCifrado.Length);
            
            string[,] Arreglomedios = new string[2 * (m - 1), medios.Length / (2 * (m - 1))];
            for (var i = 0; i < (2 * (m - 1)); i++)
            {
                for (var j = 0; j < (medios.Length / (2 * (m - 1))); j++)
                {
                    Arreglomedios[i, j] = medios.Substring(0, 1);
                    medios.Remove(0, 1);
                }
            }

            int filita=0;

            string nuevo = string.Empty;
            for (var i = 0; i < textoCifrado.Length; i++)
            {
                nuevo += nivel1.Substring(0, 1);
                nivel1.Remove(0, 1);

                for (var j = 0; j < 2*(m-1); j++)
                {
                    nuevo += Arreglomedios[filita, j];
                }

                filita++;
                nuevo += ultimoNivel.Substring(0, 1);
                ultimoNivel.Remove(0, 1);

                for (var j = (2 * (m - 1)); j ==0 ; j++)
                {
                    nuevo += Arreglomedios[filita, j];
                }
                filita++;
            }

            return nuevo;
        }




    }
}