using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio2_EDD2.Cesar
{
    public class CifrarCesar
    {
        public string Cifrado(string Texto, string clave)
        {
            string[] Validos = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N","O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string[] Cesar = new string[Validos.Length];
            char[] arreglo = clave.ToCharArray();
            char[] Comparar = Texto.ToUpper().ToCharArray();
            for (int i = 0; i < arreglo.Length; i++)
            {
                if (!Validos.Contains(arreglo[i].ToString()))
                {

                }

            }
            for (var i = 0; i < clave.Length; i++)
            {
                Cesar[i] = arreglo[i].ToString().ToUpper();
            }

            int contador = clave.Length;
            for (var i = 0; i < Validos.Length; i++)
            {
                if (!Cesar.Contains(Validos[i]))
                {
                    Cesar[contador] = Validos[i];
                    contador++;
                }
            }

            string nuevo = string.Empty;

            for (var i = 0; i < Comparar.Length; i++)
            {
                for (int j = 0; j < Validos.Length; j++)
                {
                    if (!Cesar.Contains(Comparar[i].ToString()))
                    {
                        nuevo += Comparar[i];
                        break;
                    }
                    
                    if (Comparar[i].ToString()==Validos[j])
                    {
                        nuevo+=Cesar[j].ToString();
                        break;
                    }

                }
            }

            return nuevo;

        }

        public string Descifrado(string Texto, string clave)
        {
            string[] Validos = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string[] Cesar = new string[Validos.Length];
            char[] arreglo = clave.ToCharArray();
            char[] Comparar = Texto.ToUpper().ToCharArray();
            for (var i = 0; i < clave.Length; i++)
            {
                Cesar[i] = arreglo[i].ToString().ToUpper();
            }

            int contador = clave.Length;
            for (var i = 0; i < Validos.Length; i++)
            {
                if (!Cesar.Contains(Validos[i]))
                {
                    Cesar[contador] = Validos[i];
                    contador++;
                }
            }

            string nuevo = string.Empty;

            for (var i = 0; i < Comparar.Length; i++)
            {
                for (int j = 0; j < Validos.Length; j++)
                {
                    if (!Cesar.Contains(Comparar[i].ToString()))
                    {
                        nuevo += Comparar[i];
                        break;
                    }

                    if (Comparar[i].ToString() == Cesar[j])
                    {
                        nuevo += Validos[j].ToString();
                        break;
                    }

                }
            }

            return nuevo;

        }
    }
}