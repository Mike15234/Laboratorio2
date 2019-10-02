using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Laboratorio2_EDD2.Controllers;
using Laboratorio2_EDD2.ZigZag;
using Laboratorio2_EDD2.Cesar;
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
        public void LecturaArchivo(string ruta, string nombre, int llave,string clave) //LEE EL ARCHIVO
        {
            if ((llave != 0)&&(clave==""))
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
                        //EN TEORIA ESCRIBE EN BYTES
                        if (!File.Exists(ruta))
                        {

                            using (var writeStream1 = new FileStream(ruta, FileMode.OpenOrCreate))
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
            //CIFRADO LLAVE=0
            else if ((clave != "")&&(llave==0))
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
            //DESCIFRADO LLAVE=1
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
        }
    }
}