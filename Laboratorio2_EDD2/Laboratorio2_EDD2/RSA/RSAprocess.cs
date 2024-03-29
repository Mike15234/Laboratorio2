﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Laboratorio2_EDD2.RSA;
namespace Laboratorio2_EDD2.RSA
{
    public class RSAprocess
    {
        public void GenerandoLlaves(int p, int q,string path)
        {
            //Generamos d y e y N
            VerificacionNumeros numeros = new VerificacionNumeros();
            int N = p * q;
            int fi = (p - 1) * (q - 1);
            int e, d;
            e = 1;
            d = 1;

            //DIVISORES DE PHI
            int[] divisoresfi = new int[fi];
            int cont = 0;
            for (int i = 2; i < fi; i++)
            {
                if (fi % i == 0)
                {
                    divisoresfi[cont] = i;
                    cont++;
                }
            }

            //DIVISORES N
            //N ES EL MODULAR
            int[] divisoresN = new int[N];
            cont = 0;
            for (int i = 2; i < N; i++)
            {
                if (N % i == 0)
                {
                    divisoresN[cont] = i;
                    cont++;
                }
            }

            cont = 0;
            int[] fifi = new int[fi];
            bool sirve = true;

            for (int i = 0; i < fi; i++)
            {
                for (int j = 0; j < fi; j++)
                {
                    if ((i == divisoresfi[j]) || (i == divisoresN[j]))
                    {
                        sirve = false;//no se utiliza si son factores en comun
                    }
                }
                if (sirve)
                {
                    fifi[cont] = i;//agarra numeros que no tienen en comun 
                    cont++;
                }
                sirve = true;
            }

            int aux = 0;
            for (int i = 0; i < cont; i++)
            {
                if ( numeros.coprimo(fifi[i], fi) && fifi[i] > 10)

                {
                    aux++;
                    if (aux == 2)
                    {
                        e = fifi[i];
                        break;
                    }
                }
            }

            bool listo = false;
            while (!listo)
            {
                if (((e * d) % fi) == 1)
                {
                    listo = true;
                }
                else
                {
                    d++;
                }
            }
            
            string privada = d.ToString() + ","+ N.ToString() ;
            string publica = e.ToString() + "," + N.ToString();
            byte[] bytesprivada = System.Text.Encoding.ASCII.GetBytes(privada);
            byte[] bytespublica = System.Text.Encoding.ASCII.GetBytes(publica);
            

            string pathprivada = path + "llaveprivada.txt";
            string pathpublica = path + "llavepublica.txt";
            
            using (var writeStream1 = new FileStream(pathprivada, FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(writeStream1))
                    {
                        foreach (var item in bytesprivada)
                        {
                            writer.Write(item);
                        }
                        writer.Close();
                    }
                    writeStream1.Close();

            }
            using (var writeStream1 = new FileStream(pathpublica, FileMode.OpenOrCreate))
            {
                using (var writer = new BinaryWriter(writeStream1))
                {
                    foreach (var item in bytespublica)
                    {
                        writer.Write(item);
                    }
                    writer.Close();
                }
                writeStream1.Close();

            }

        }

        public int CifrandoRSACantByte(int cifrado)
        {
            return cifrado / 256;
        }


        public int CifrandoRSA(int numCifrar,int e, int N)
        {
            int Cifrado = 1;

            numCifrar = numCifrar % N;
            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    Cifrado = (Cifrado * numCifrar) % N;

                }
                //  e = e / 2;
                e = e >> 1;
                numCifrar = (numCifrar * numCifrar) % N;
            }

            Cifrado = Cifrado % N;
            return Cifrado;
        }


        public int DescifrandoRSA(int numDescifrar, int e, int N)
        {
            int Cifrado = 1;

            numDescifrar = numDescifrar % N;
            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    Cifrado = (Cifrado * numDescifrar) % N;

                }
                //  e = e / 2;
                e = e >> 1;
                numDescifrar = (numDescifrar * numDescifrar) % N;
            }

            Cifrado = Cifrado % N;
            return Cifrado;
        }

    }
}