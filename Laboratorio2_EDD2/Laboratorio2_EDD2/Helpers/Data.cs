using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Laboratorio2_EDD2.Controllers;
using Laboratorio2_EDD2.ZigZag;
using Laboratorio2_EDD2.Cesar;
using Laboratorio2_EDD2.Espiral;
using Laboratorio2_EDD2.SDES;
using Laboratorio2_EDD2.RSA;

namespace Laboratorio2_EDD2.Helpers
{
    public class Data
    {
        //SINGLETON
        private static Data instancia = null;
        public static Data Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Data();
                }
                return instancia;
            }
        }

        const int bufferLength = 1000000;

        public void LecturaArchivo(string ruta, string nombre, int llave, string clave, int llenado) //LEE EL ARCHIVO
        {
            if ((llave != 0) && (llenado == 100))
            {
                ZigZagcompression zigzagg = new ZigZagcompression();
                using (var stream = new FileStream(ruta, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        var byteBuffer = new byte[bufferLength];
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            byteBuffer = reader.ReadBytes(bufferLength);
                        }
                        string letters = System.Text.Encoding.ASCII.GetString(byteBuffer);
                        string cifrado = zigzagg.Decifrarzigzag(letters, llave); //mandar llave

                        string[] nuevo = ruta.Split('.');
                        nuevo[0] += ".txt";
                        if (!File.Exists(nuevo[0]))
                        {

                            using (var writeStream1 = new FileStream(nuevo[0], FileMode.OpenOrCreate))
                            {
                                using (var writer = new BinaryWriter(writeStream1))
                                {
                                    foreach (var item in cifrado)
                                    {
                                        writer.Write(item);
                                    }
                                    writer.Close();
                                }
                                writeStream1.Close();

                            }
                        }

                    }
                }


            }
            else if ((llave != 0) && (clave == "") && (llenado == 1000))
            {
                ZigZagcompression zigzagg = new ZigZagcompression();
                using (var stream = new FileStream(ruta, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        var byteBuffer = new byte[bufferLength];
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            byteBuffer = reader.ReadBytes(bufferLength);
                        }
                        string letters = System.Text.Encoding.ASCII.GetString(byteBuffer);
                        string cifrado = zigzagg.ZigZag(letters, llave); //mandar llave

                        string[] nuevo = ruta.Split('.');
                        nuevo[0] += "zigzag.cif";
                        if (!File.Exists(nuevo[0]))
                        {

                            using (var writeStream1 = new FileStream(nuevo[0], FileMode.OpenOrCreate))
                            {
                                using (var writer = new BinaryWriter(writeStream1))
                                {
                                    foreach (var item in cifrado)
                                    {
                                        writer.Write(item);
                                    }
                                    writer.Close();
                                }
                                writeStream1.Close();

                            }
                        }

                    }
                }
            }
            //CIFRADO CESAR LLAVE=0
            else if ((clave != "") && (llave == 0))
            {
                CifrarCesar Cesar = new CifrarCesar();
                using (var stream = new FileStream(ruta, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        var byteBuffer = new byte[bufferLength];
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            byteBuffer = reader.ReadBytes(bufferLength);
                        }
                        string letters = System.Text.Encoding.ASCII.GetString(byteBuffer);
                        string cifrado = Cesar.Cifrado(letters, clave); //mandar llave
                        string[] nuevo = ruta.Split('.');
                        nuevo[0] += "cesar.cif";
                        //EN TEORIA ESCRIBE EN BYTES
                        if (!File.Exists(nuevo[0]))
                        {

                            using (var writeStream1 = new FileStream(nuevo[0], FileMode.OpenOrCreate))
                            {
                                using (var writer = new BinaryWriter(writeStream1))
                                {
                                    foreach (var item in cifrado)
                                    {
                                        writer.Write(item);
                                    }
                                    writer.Close();
                                }
                                writeStream1.Close();

                            }
                        }

                    }
                }
            }
            //DESCIFRADO CESAR LLAVE=1
            else if ((clave != "") && (llave == 1))
            {
                CifrarCesar Cesar = new CifrarCesar();
                using (var stream = new FileStream(ruta, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        var byteBuffer = new byte[bufferLength];
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            byteBuffer = reader.ReadBytes(bufferLength);
                        }
                        string letters = System.Text.Encoding.ASCII.GetString(byteBuffer);
                        string cifrado = Cesar.Descifrado(letters, clave); //mandar llave
                        string[] nuevo = ruta.Split('.');
                        nuevo[0] += "cesar.txt";
                        //EN TEORIA ESCRIBE EN BYTES
                        if (!File.Exists(nuevo[0]))
                        {

                            using (var writeStream1 = new FileStream(nuevo[0], FileMode.OpenOrCreate))
                            {
                                using (var writer = new BinaryWriter(writeStream1))
                                {
                                    foreach (var item in cifrado)
                                    {
                                        writer.Write(item);
                                    }
                                    writer.Close();
                                }
                                writeStream1.Close();

                            }
                        }

                    }
                }
            }
            else if (llenado != 0)
            {
                RutaEspiral Espiral = new RutaEspiral();
                using (var stream = new FileStream(ruta, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        var byteBuffer = new byte[bufferLength];
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            byteBuffer = reader.ReadBytes(bufferLength);
                        }
                        string letters = System.Text.Encoding.ASCII.GetString(byteBuffer);
                        string cifrado = Espiral.cifrarEspiral(letters, llave, llenado); //mandar llave
                        string[] nuevo = ruta.Split('.');
                        nuevo[0] += "espiral.cif";
                        if (!File.Exists(nuevo[0]))
                        {

                            using (var writeStream1 = new FileStream(nuevo[0], FileMode.OpenOrCreate))
                            {
                                using (var writer = new BinaryWriter(writeStream1))
                                {
                                    foreach (var item in cifrado)
                                    {
                                        writer.Write(item);
                                    }
                                    writer.Close();
                                }
                                writeStream1.Close();

                            }
                        }

                    }
                }

            }
        }

        public void lecturaSDES(string ruta, int num, string rutaPermutaciones, int confirmacion)
        {
            SDES.SDES sdes = new SDES.SDES();
            string K1 = string.Empty, K2 = string.Empty;
            using (var stream = new FileStream(ruta, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var byteBuffer = new byte[bufferLength];
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        byteBuffer = reader.ReadBytes(bufferLength);
                    }
                    string permutado = sdes.GenerarPermutado(num, rutaPermutaciones);
                    K1 = sdes.LLAVES1(permutado);
                    K2 = sdes.LLAVES2();
                    string[] nuevo = new string[5];
                    byte[] resultado = new byte[byteBuffer.Length];
                    if (confirmacion == 1)
                    {
                        resultado = sdes.Cifrar(byteBuffer, K1, K2);
                        nuevo = ruta.Split('.');
                        nuevo[0] += ".scif";
                    }
                    else
                    {
                        resultado = sdes.Cifrar(byteBuffer, K2, K1);
                        nuevo = ruta.Split('.');
                        nuevo[0] += "descifrado.txt";
                    }

                    if (!File.Exists(nuevo[0]))
                    {

                        using (var writeStream1 = new FileStream(nuevo[0], FileMode.OpenOrCreate))
                        {
                            using (var writer = new BinaryWriter(writeStream1))
                            {
                                foreach (var item in resultado)
                                {
                                    writer.Write(item);
                                }
                                writer.Close();
                            }
                            writeStream1.Close();

                        }
                    }

                }
            }


        }

        public void lecturasRSA(string rutallave,string rutatexto)
        {
            RSAprocess rsa = new RSAprocess();
            using (var stream = new FileStream(rutallave, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var byteBufferKey = new byte[bufferLength];
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        byteBufferKey = reader.ReadBytes(bufferLength);
                    }

                }

            }
            using (var stream = new FileStream(rutatexto, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var byteBufferText = new byte[bufferLength];
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        byteBufferText = reader.ReadBytes(bufferLength);
                    }
                   // rsa.CifrandoRSA(byteBufferKey,byteBufferText);
                }

            }

        }
    }
}
