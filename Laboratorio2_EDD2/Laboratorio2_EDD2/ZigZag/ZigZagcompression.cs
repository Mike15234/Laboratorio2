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
            string valor = string.Empty;
            string[] Validos = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", ".", "'", "/", "-", "Ü", " " ,"!","$"};
            foreach (char a in textoCompleto.ToArray())
            {
                if (!textoCompleto.ToArray().Contains(a))
                {
                    valor = a.ToString();
                    break;
                }
            }
            int zz = 0;
            int fila = 0;
            int columna = 0;
            string[,] zigzag = new string[llave, textoCompleto.Length];

            string caracter=string.Empty;
            for (int i = 0; i < textoCompleto.Length; i++)
            {
                if (textoCompleto.Substring(i,1)==null)
                {
                    caracter= valor;
                }
                else
                {
                    caracter = textoCompleto.Substring(i, 1);
                }
                ////
                zigzag[fila, columna] = textoCompleto.Substring(i, 1);
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
            /////
            string cifrado = string.Empty;
            for (var i = 0; i < llave; i++)
            {
                for (var j = 0; j < textoCompleto.Length; j++)
                {
                    cifrado += zigzag[i, j];
                }
            }
            return cifrado;
        }

    }
}