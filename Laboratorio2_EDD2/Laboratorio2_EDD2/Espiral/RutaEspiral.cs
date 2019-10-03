using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio2_EDD2.Espiral
{
    public class RutaEspiral
    {
        public string cifrarEspiral(string texto, int m,int recorrido)
        {
            int n = texto.Length / m;
            string[,] matriz = new string[m, n];
            int aux = 1;
            if (recorrido==2)
            {
                for ( var fila = 0; fila < m; fila++)
                {
                    for (var columna = 0; columna < n; columna++)
                    {
                        matriz[fila, columna] = texto.Substring(fila, 1);
                    }
                }

            }//llenado horizontal
            else
            {
                for (int fila = 0; fila < m; fila++)
                {

                    for (int co = 0; co < n; co++)
                    {
                        matriz[co, fila] = texto.Substring(fila, 1);
                    }
                }
            }//llenado vertical
            int inicio = 0;
            int limitefila = m;
            int limitecolumna = n;
            string cifrado=string.Empty;
            int j, i=0;
            //Recorrer en espiral
            while (aux <= texto.Length)
            {
                for (j = inicio; j < m; j++)
                {
                    cifrado+=matriz[i,j];
                    aux++;
                }
                for (i = inicio + 1; i < limitefila; i++)
                {
                    cifrado+=matriz[i, j - 1];
                    aux++;
                }
                for (j = limitecolumna - 1; j > inicio; j--)
                {
                    cifrado += matriz[i - 1, j - 1];
                    aux++;
                }
                for (i = limitefila - 1; i > inicio + 1; i--)
                {
                    cifrado+=matriz[i - 1,j];
                    aux++;
                }
                inicio++;
                limitecolumna--;
                limitefila--;
            }

            

            return cifrado;
        }
    }
}