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
            var key = Convert.ToDouble(llave);
            var mm = Math.Ceiling(((2 * key) - 3 + textoCifrado.Length) / ((2 * key) - 2));
            var m = Convert.ToInt32(mm);

            string[] medios = new string[llave - 2];
            var posicion = m;
            string[,] Arreglomedios = new string[llave - 2, 2 * (m - 1)];

            string nivel1 = textoCifrado.Substring(0, m);

            for (int i = 0; i < llave - 2; i++)
            {
                medios[i] = Convert.ToString(textoCifrado.Substring((posicion), (2 * (m - 1)))).PadRight(2 * (m - 1), '%');
                posicion += (2 * (m - 1));
                var charArray = medios[i].ToCharArray();
                for (int j = 0; j < 2 * (m - 1); j++)
                {
                    Arreglomedios[i, j] = Convert.ToString(charArray[j]);
                }

            }

            string ultimoNivel = textoCifrado.Substring((textoCifrado.Length - m +1), m-1);
            int filita=0; 
            var nuevo = string.Empty;

            var contador = 0;
            var k = 0;

            while(contador < textoCifrado.Length)
            {
                if (nuevo.Length == textoCifrado.Length)
                {
                    break;
                }
                if (nivel1.Substring(k, 1) == "$")
                {
                    
                }else
                {
                    nuevo += nivel1.Substring(k, 1);
                }
                contador++;
                for (var j = 0; j < llave - 2; j++)
                {
                    if (nuevo.Length == textoCifrado.Length)
                    {
                        break;
                    }
                    if (nivel1.Substring(k, 1) != "$")
                    {
                        nuevo += Arreglomedios[j, filita];
                    }
                    contador++;
                    
                }
                filita++;
                if (nuevo.Length == textoCifrado.Length)
                {
                    break;
                }
                if (nivel1.Substring(k, 1) != "$")
                {
                    nuevo += ultimoNivel.Substring(k, 1);
                }
                
                
                contador++;
                for (int j = llave - 3; j >= 0; j--)
                {
                    if (nuevo.Length == textoCifrado.Length)
                    {
                        break;
                    }
                    if (nivel1.Substring(k, 1) != "$")
                    {
                        nuevo += Arreglomedios[j, filita];
                    }
                    contador++;
                    
                }//hay que rellenar lo demas de la matriz
                filita++;
                k++;
            }
            
            return nuevo;
           

        }




    }
}