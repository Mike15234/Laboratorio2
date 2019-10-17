﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Laboratorio2_EDD2.SDES
{
    public class SDES
    {
        string[,] So = new string[4, 4];
        string[,] Si = new string[4, 4];
        byte[] IP,EP,P10,P8,P4;
        string K1 = string.Empty,K2=string.Empty;
        string[] LLave1,LLave2 = new string[8];

        public void llenarMatrices()
        {

            So[0, 0] = "01";So[0, 1] = "00";So[0, 2] = "11";So[0, 3] = "10";
            So[1, 0] = "11";So[1, 1] = "10";So[1, 2] = "01";So[1, 3] = "00";
            So[2, 0] = "00";So[2, 1] = "10";So[2, 2] = "01";So[2, 3] = "11";
            So[3, 0] = "11";So[3, 1] = "01";So[3, 2] = "11";So[3, 3] = "10";

            Si[0, 0] = "00"; Si[0, 1] = "01"; Si[0, 2] = "10"; Si[0, 3] = "11";
            Si[1, 0] = "10"; Si[1, 1] = "00"; Si[1, 2] = "01"; Si[1, 3] = "11";
            Si[2, 0] = "11"; Si[2, 1] = "00"; Si[2, 2] = "01"; Si[2, 3] = "00";
            Si[3, 0] = "10"; Si[3, 1] = "01"; Si[3, 2] = "00"; Si[3, 3] = "11";

        }

        public void GenerarLlaves(int Numero)
        {
            const int bufferLength = 1000000000;
            string binario = Convert.ToString(Numero, 2).PadLeft(10, '0');

            var byteBuffer = new byte[bufferLength];

            //Hay que cambiar la ruta para las permutaciones
            string ruta = "C:/ Users / migue / Desktop / GIT_ED / Laboratorio2 / Laboratorio2_EDD2 / Laboratorio2_EDD2 / SDES/Permutaciones.txt";
            using (var stream = new FileStream(ruta, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        byteBuffer = reader.ReadBytes(bufferLength);
                    }
                }
            }
            string Permutaciones = System.Text.Encoding.ASCII.GetString(byteBuffer);

            string[] texto = Permutaciones.Split('/');

            IP = new byte[texto[0].Length];

            for (var i = 0; i < texto[0].Length; i++)
            {
                IP[i] = Convert.ToByte(texto[0].Substring(i, 1));
            }
            EP = new byte[8];
            for (var i = 0; i < texto[1].Length; i++)
            {
                EP[i] = Convert.ToByte(texto[1].Substring(i, 1));
            }

            P10 = new byte[10];
            for (var i = 0; i < texto[2].Length; i++)
            {
                P10[i] = Convert.ToByte(texto[2].Substring(i, 1));
            }

            P8 = new byte[8];
            for (var i = 0; i < texto[3].Length; i++)
            {
                P8[i] = Convert.ToByte(texto[3].Substring(i, 1));
            }
            P4 = new byte[4];
            for (var i = 0; i < texto[4].Length; i++)
            {
                P4[i] = Convert.ToByte(texto[4].Substring(i, 1));
            }

            string permutado = string.Empty;
            for (int i = 0; i < P10.Length; i++)
            {
                permutado += binario.ElementAt(Convert.ToInt32(P10[i]));
            }

            string B1 = permutado.Substring(0, (permutado.Length / 2));
            string B2 = permutado.Substring((permutado.Length / 2),permutado.Length );
            B1 += B1.Substring(0, 1);
            B1.Remove(0,1);
            B2 += B2.Substring(0, 1);
            B2.Remove(0,1);
            permutado = B1 + B2;
            
            for (int i = 0; i < P8.Length; i++)
            {
                K1 += permutado.ElementAt(Convert.ToInt32(P8[i]));
            }

            B1 = K1.Substring(0, (K1.Length / 2));
            B2 = K1.Substring((K1.Length / 2), K1.Length);
            B1 += B1.Substring(0, 2);
            B1.Remove(0,2);
            B2 += B2.Substring(0, 2);
            B2.Remove(0,2);
            permutado = B1 + B2;
            
            for (int i = 0; i < P8.Length; i++)
            {
                K2 += permutado.ElementAt(Convert.ToInt32(P8[i]));
            }
        }


        public string Cifrar(byte[] textoCompleto,)//tespers
        {
            string textoCifrado = string.Empty;
            for (var i = 0; i < textoCompleto.Length; i++)
            {
                string binario = Convert.ToString(textoCompleto[i], 2).PadLeft(8, '0');
                string permutado = string.Empty;
                for (var j = 0; j < IP.Length; j++)
                {
                    permutado += binario.ElementAt(Convert.ToInt32(IP[i]));
                }
                string IP1 = permutado.Substring(0, (permutado.Length / 2));
                string IP2 = permutado.Substring((permutado.Length / 2), permutado.Length);
                string nuevop = string.Empty;
                string XOR = string.Empty;
                for (var j = 0; j < EP.Length; j++)
                {
                    nuevop += IP2.ElementAt(Convert.ToInt32(EP[i]));///SABER
                }

                LLave1 = K1.Split();
                for (var j = 0; j < EP.Length; j++)
                {
                    if (EP[i].ToString() == LLave1[i])
                    {
                        XOR += "0";
                    }
                    else
                    {
                        XOR += "1";
                    }
                }
                string P1, P2;
                P1 = XOR.Substring(0, (XOR.Length / 2));
                P2 = XOR.Substring((XOR.Length / 2), XOR.Length);
                int fila = 0, columna = 0;
                fila = Convert.ToInt32((P1.ElementAt(0).ToString() + P1.ElementAt(3).ToString()), 2);
                columna = Convert.ToInt32((P1.ElementAt(1).ToString() + P1.ElementAt(2).ToString()), 2);
                string Bloque1 = So[fila, columna];
                fila = Convert.ToInt32((P2.ElementAt(0).ToString() + P2.ElementAt(3).ToString()), 2);
                columna = Convert.ToInt32((P2.ElementAt(1).ToString() + P2.ElementAt(2).ToString()), 2);

                string Bloque2 = Si[fila, columna];
                string Total = Bloque1 + Bloque2;

                for (var j = 0; j < P4.Length; j++)
                {
                    nuevop += Total.ElementAt(Convert.ToInt32(P4[i]));///SABER
                }

                XOR = string.Empty;
                for (var j = 0; j < EP.Length; j++)
                {
                    if (IP1[i].ToString() == nuevop[i].ToString())
                    {
                        XOR += "0";
                    }
                    else
                    {
                        XOR += "1";
                    }
                }
                string swap = IP2 + XOR;
                string Parte2 = XOR;
                for (var j = 0; j < EP.Length; j++)
                {
                    nuevop += XOR.ElementAt(Convert.ToInt32(EP[i]));///SABER
                }

                XOR = string.Empty;
                for (var j = 0; j < EP.Length; j++)
                {
                    if (LLave2[i].ToString() == nuevop[i].ToString())
                    {
                        XOR += "0";
                    }
                    else
                    {
                        XOR += "1";
                    }
                }

                P1 = XOR.Substring(0, (XOR.Length / 2));
                P2 = XOR.Substring((XOR.Length / 2), XOR.Length);
                fila = 0; columna = 0;
                fila = Convert.ToInt32((P1.ElementAt(0).ToString() + P1.ElementAt(3).ToString()), 2);
                columna = Convert.ToInt32((P1.ElementAt(1).ToString() + P1.ElementAt(2).ToString()), 2);
                Bloque1 = So[fila, columna];
                fila = Convert.ToInt32((P2.ElementAt(0).ToString() + P2.ElementAt(3).ToString()), 2);
                columna = Convert.ToInt32((P2.ElementAt(1).ToString() + P2.ElementAt(2).ToString()), 2);
                Bloque2 = Si[fila, columna];
                Total = Bloque1 + Bloque2;

                for (var j = 0; j < P4.Length; j++)
                {
                    nuevop += Total.ElementAt(Convert.ToInt32(P4[i]));///SABER
                }

                XOR = string.Empty;
                for (var j = 0; j < EP.Length; j++)
                {
                    if (IP2[i].ToString() == nuevop[i].ToString())
                    {
                        XOR += "0";
                    }
                    else
                    {
                        XOR += "1";
                    }
                }

                Parte2 = XOR+Parte2;
                byte[] IPI = new byte[IP.Length];//IP inversa
                string Ordenada = "01234567";
                    for (var j = 0; j < IP.Length; j++)
                {
                    IPI[j]= IP[Convert.ToInt32(Ordenada[j].ToString())];///SABER
                }

                string Rbinario = string.Empty;
                for (var j = 0; j < Parte2.Length; j++)
                {
                    Rbinario = Parte2.ElementAt(IPI[j]);
                }
                

            }
            return textoCifrado;
        }

    }
}