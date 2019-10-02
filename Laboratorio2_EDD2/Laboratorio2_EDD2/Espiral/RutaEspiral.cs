using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio2_EDD2.Espiral
{
    public class RutaEspiral
    {
        public string cifrarEspiral(string texto, int m)
        {
            int n = texto.Length / m;

            string[,] matriz = new string[m, n];
            //llenado horizontal
            for (var i = 0; i<m; i++)
            {
                for (var j = 0; j < n ; j++)
                {
                    matriz[i, j] = texto.Substring(i, 1);
                }
            }

            //llenado vertical
            for (int i = 0; i < m; i++)
            {
                
                for (int j = 0;j < n; j++)
                {
                    matriz[j,i] = texto.Substring(i, 1);
                }
            }

            //Recorrer en espiral


            //escribir
            for (var i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matriz[m, n].ToString();
                }
            }

            return "";
        }
    }
}