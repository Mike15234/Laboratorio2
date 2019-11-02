using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using Laboratorio2_EDD2.Helpers;
using Laboratorio2_EDD2.Models;
using Laboratorio2_EDD2.SDES;
using Laboratorio2_EDD2.RSA;

namespace Laboratorio2_EDD2.Controllers
{
    public class CargaDataController : Controller
    {
        // GET: IngresoData
        public ActionResult Index()
        {
            return View();
        }

        //GENERAR LLAVES RSA
        public ActionResult GenerarLlaver()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerarLlaver(int p, int q)
        {
            RSAprocess llaves = new RSAprocess();
            SubirArchivo obtencionLllaves = new SubirArchivo();
            obtencionLllaves.P = p;
            obtencionLllaves.Q = q;
            string key=(Server.MapPath(@"~\LlavesGeneradas\"));
            llaves.GenerandoLlaves(p, q,key);

            return View();
        }


        //RSA CIFRADO
        public ActionResult RSACifrar()
        {

            return View();
        }

        [HttpPost]
        public ActionResult RSACifrar( HttpPostedFileBase text, HttpPostedFileBase key)
        {
            SubirArchivo recibirkey = new SubirArchivo();

            var fileName1 = Path.GetFileName(text.FileName);//Nombre del archivo a cargar
            var fileName2 = Path.GetFileName(key.FileName);
            text.SaveAs(Server.MapPath(@"~\Uploads\" + fileName1));//Guardado del archivo en la ruta física 
            key.SaveAs(Server.MapPath(@"~\LlavesSubidas\" + fileName2));//se guarda en LlavesSubidas carpeta del proyecto
            string filePathText = string.Empty;
            string filePathKey = string.Empty;
            if (text != null)
            {
                string NuevaRutaText = "";
                string NuevaRutaKey = "";
                string path = Server.MapPath("~/Uploads");
                string pathKey = Server.MapPath("~/LlavesSubidas");
                string[] Direccion = path.Split('\\');
                string[] Direction = pathKey.Split('\\');
                //PARA EL ARCHIVO QUE SE VA A  CIFRAR
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //PARA LA RUTA DE LA LLAVE QUE SE USA PARA CIFRAR
                if (!Directory.Exists(pathKey))
                {
                    Directory.CreateDirectory(pathKey);
                }
                //TEXTO
                for (var i = 0; i < Direccion.Length; i++)
                {
                    NuevaRutaKey += Direccion[i] + "/";
                }
                //PARA LA LLAVE
                for (var i = 0; i < Direction.Length; i++)
                {
                    NuevaRutaText+=Direction[i] + "/";
                }
                filePathText = NuevaRutaText + Path.GetFileName(key.FileName);
                filePathKey = NuevaRutaKey + Path.GetFileName(text.FileName);
            }
            Data.Instancia.lecturasRSA(filePathText, filePathKey,1);
            return View();
        }

        //RSA DESCIFRADO
        public ActionResult RSADescifrar()
        {

            return View();
        }

        [HttpPost]
        public ActionResult RSADescifrar(HttpPostedFileBase text, HttpPostedFileBase key)
        {
            SubirArchivo recibirkey = new SubirArchivo();

            var fileName1 = Path.GetFileName(text.FileName);//Nombre del archivo a cargar
            var fileName2 = Path.GetFileName(key.FileName);
            text.SaveAs(Server.MapPath(@"~\Uploads\" + fileName1));//Guardado del archivo en la ruta física 
            key.SaveAs(Server.MapPath(@"~\LlavesSubidas\" + fileName2));//se guarda en LlavesSubidas carpeta del proyecto
            string filePathText = string.Empty;
            string filePathKey = string.Empty;
            if (text != null)
            {
                string NuevaRutaText = "";
                string NuevaRutaKey = "";
                string path = Server.MapPath("~/Uploads");
                string pathKey = Server.MapPath("~/LlavesSubidas");
                string[] Direccion = path.Split('\\');
                string[] Direction = pathKey.Split('\\');
                //PARA EL ARCHIVO QUE SE VA A  CIFRAR
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //PARA LA RUTA DE LA LLAVE QUE SE USA PARA CIFRAR
                if (!Directory.Exists(pathKey))
                {
                    Directory.CreateDirectory(pathKey);
                }
                //TEXTO
                for (var i = 0; i < Direccion.Length; i++)
                {
                    NuevaRutaKey += Direccion[i] + "/";
                }
                //PARA LA LLAVE
                for (var i = 0; i < Direction.Length; i++)
                {
                    NuevaRutaText += Direction[i] + "/";
                }
                filePathText = NuevaRutaText + Path.GetFileName(key.FileName);
                filePathKey = NuevaRutaKey + Path.GetFileName(text.FileName);
            }
            Data.Instancia.lecturasRSA(filePathText, filePathKey, 2);
            return View();
        }

        //SDES
        public ActionResult SubirSDES()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubirSDES(HttpPostedFileBase file, int llave)
        {

            SubirArchivo recibirkey = new SubirArchivo();
            recibirkey.llave = llave;
            if (llave>1023)
            {
                return View("error");
            }
            var fileName = Path.GetFileName(file.FileName);
            file.SaveAs(Server.MapPath(@"~\Uploads\" + fileName));
            string filePath = string.Empty;
            if (file != null)
            {
                string NuevaRuta = "";
                string path = Server.MapPath("~/Uploads");
                string[] Direccion = path.Split('\\');
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                for (var i = 0; i < Direccion.Length; i++)
                {
                    NuevaRuta += Direccion[i] + "/";
                }
                filePath = NuevaRuta + Path.GetFileName(file.FileName);
            }

            string permutaciones = Server.MapPath(@"~\SDES\Permutaciones.txt");
            string[] Direccion1 = permutaciones.Split('\\');
            string rutaPer = string.Empty;
            
            for (var i = 0; i < Direccion1.Length; i++)
            {
                rutaPer += Direccion1[i] + "/";
            }
            rutaPer.Remove((rutaPer.Length-2), 1);
            Data.Instancia.lecturaSDES(filePath, llave,permutaciones,1);
            return View();
        }

        //SDES
        public ActionResult DescifrarSDES()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DescifrarSDES(HttpPostedFileBase file, int llave)
        {

            SubirArchivo recibirkey = new SubirArchivo();
            recibirkey.llave = llave;
            var fileName = Path.GetFileName(file.FileName);
            file.SaveAs(Server.MapPath(@"~\Uploads\" + fileName));
            string filePath = string.Empty;
            if (file != null)
            {
                string NuevaRuta = "";
                string path = Server.MapPath("~/Uploads");
                string[] Direccion = path.Split('\\');
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                for (var i = 0; i < Direccion.Length; i++)
                {
                    NuevaRuta += Direccion[i] + "/";
                }
                filePath = NuevaRuta + Path.GetFileName(file.FileName);
            }

            string permutaciones = Server.MapPath(@"~\SDES\Permutaciones.txt");
            string[] Direccion1 = permutaciones.Split('\\');
            string rutaPer = string.Empty;

            for (var i = 0; i < Direccion1.Length; i++)
            {
                rutaPer += Direccion1[i] + "/";
            }
            rutaPer.Remove((rutaPer.Length - 2), 1);
            Data.Instancia.lecturaSDES(filePath, llave, permutaciones, 0);
            return View();
        }



        public ActionResult SubirArchivo()
        {

            return View();
        }
    
        //ZIGZAG
        [HttpPost]
        public ActionResult SubirArchivo(HttpPostedFileBase file, int llave)
        {
            SubirArchivo recibirkey = new SubirArchivo();
            recibirkey.llave = llave;
            var fileName = Path.GetFileName(file.FileName);//Nombre del archivo a cargar
            file.SaveAs(Server.MapPath(@"~\Uploads\" + fileName));//Guardado del archivo en la ruta física 
            string filePath = string.Empty;
            if (file != null)
            {
                string NuevaRuta = "";
                string path = Server.MapPath("~/Uploads");
                string[] Direccion = path.Split('\\');
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                for (var i = 0; i < Direccion.Length; i++)
                {
                    NuevaRuta += Direccion[i] + "/";
                }
                filePath = NuevaRuta + Path.GetFileName(file.FileName);
            }
            Data.Instancia.LecturaArchivo(filePath, fileName, llave, "",1000);
            return View();
        }

        //DECIFRADO ZIGZAG
        public ActionResult DecifrarZigZag()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DecifrarZigZag(HttpPostedFileBase file, int llave)
        {
            var allowedExtensions = new string[] { ".cif" };
            string extension = Path.GetExtension(file.FileName);
            if (allowedExtensions.Contains(extension))
            {
                SubirArchivo recibirkey = new SubirArchivo();
                recibirkey.llave = llave;
                var fileName = Path.GetFileName(file.FileName);//Nombre del archivo a cargar
                file.SaveAs(Server.MapPath(@"~\Uploads\" + fileName));//Guardado del archivo en la ruta física 
                string filePath = string.Empty;
                if (file != null)
                {
                    string NuevaRuta = "";
                    string path = Server.MapPath("~/Uploads");
                    string[] Direccion = path.Split('\\');
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    for (var i = 0; i < Direccion.Length; i++)
                    {
                        NuevaRuta += Direccion[i] + "/";
                    }
                    filePath = NuevaRuta + Path.GetFileName(file.FileName);
                }
                Data.Instancia.LecturaArchivo(filePath, fileName, llave, "", 100);
                return View();
            }
            else
            {
                return View("error");
            }
        }


        //DOWNLOAD
        public ActionResult Donwload()
        {
            string path = Server.MapPath("~/Uploads/");
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] files = dirInfo.GetFiles("*.*");
            List<string> lst = new List<string>(files.Length);
            foreach (var item in files)
            {
                lst.Add(item.Name);
            }
            return View(lst);
        }
        public ActionResult DownloadFile(string filename)
        {

            var path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
            var bytesDoc = System.IO.File.ReadAllBytes(path + filename);
            return File(bytesDoc, System.Net.Mime.MediaTypeNames.Application.Octet, filename);  
        }
        
        //VISTA ERROR
        public ActionResult error()
        {
            return View();
        }

        //CESAR CIFRADO
        public ActionResult Cesar()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Cesar(HttpPostedFileBase file, string llave)
        {
            SubirArchivo recibirkey = new SubirArchivo();

            recibirkey.clave = llave;
            char[] verificar = llave.ToCharArray();
            bool repetido = false;
            for (var i = 0; i < llave.Length; i++)
            {
                for (var j = 0; j < llave.Length; j++)
                {
                    if (i != j)
                    {
                        if (verificar[i] == verificar[j])
                        {
                            repetido = true;
                        }
                    }
                }
            }

            if (!repetido)
            {
                var fileName = Path.GetFileName(file.FileName);//Nombre del archivo a cargar
                file.SaveAs(Server.MapPath(@"~\Uploads\" + fileName));
                string filePath = string.Empty;
                if (file != null)
                {
                    string NuevaRuta = "";
                    string path = Server.MapPath("~/Uploads");
                    string[] Direccion = path.Split('\\');
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    for (var i = 0; i < Direccion.Length; i++)
                    {
                        NuevaRuta += Direccion[i] + "/";
                    }
                    filePath = NuevaRuta + Path.GetFileName(file.FileName);
                }
                Data.Instancia.LecturaArchivo(filePath, fileName, 0,llave,0);

            }
            else
            {

                return View("error"); 

            }
            
            return View();
        }
        //DECIFRADO CESAR
        public ActionResult CesarDescifrado()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CesarDescifrado(HttpPostedFileBase file, string llave)
        {
            var allowedExtensions = new string[] { ".cif" };
            string extension = Path.GetExtension(file.FileName);
            if (allowedExtensions.Contains(extension))
            {
                SubirArchivo recibirkey = new SubirArchivo();
                recibirkey.clave = llave;
                char[] verificar = llave.ToCharArray();
                bool repetido = false;
                for (var i = 0; i < llave.Length; i++)
                {
                    for (var j = 0; j < llave.Length; j++)
                    {
                        if (i != j)
                        {
                            if (verificar[i] == verificar[j])
                            {
                                repetido = true;
                            }
                        }
                    }
                }

                if (!repetido)
                {
                    var fileName = Path.GetFileName(file.FileName);//Nombre del archivo a cargar
                    file.SaveAs(Server.MapPath(@"~\Uploads\" + fileName));//Guardado del archivo en la ruta física 
                    string filePath = string.Empty;
                    if (file != null)
                    {
                        string NuevaRuta = "";
                        string path = Server.MapPath("~/Uploads");
                        string[] Direccion = path.Split('\\');
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        for (var i = 0; i < Direccion.Length; i++)
                        {
                            NuevaRuta += Direccion[i] + "/";
                        }
                        filePath = NuevaRuta + Path.GetFileName(file.FileName);
                    }
                    Data.Instancia.LecturaArchivo(filePath, fileName, 1, llave, 0);

                }
                else
                {

                    return View("error");

                }

                return View();
            }
            else
            {
                return View("error");
            }
        }
        //ESPIRAL CIFRADO
        public ActionResult CifrarEspiral()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CifrarEspiral(HttpPostedFileBase file, int m, string llenado)
        {
            SubirArchivo recibirkey = new SubirArchivo();
            int verificarM;
            recibirkey.m = m;
            recibirkey.llenado = llenado;
            var fileName = Path.GetFileName(file.FileName);
            file.SaveAs(Server.MapPath(@"~\Uploads\" + fileName));
            string filePath = string.Empty;

            if (llenado == "H")
            {
                verificarM = 1;
            }
            else if(llenado == "V")
            {
                verificarM = 2;
            }
            else
            {
                return View("error");
            } //mmm escriba bien xfa

            if (file != null)
            {
                string NuevaRuta = "";
                string path = Server.MapPath("~/Uploads");
                string[] Direccion = path.Split('\\');
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                for (var i = 0; i < Direccion.Length; i++)
                {
                    NuevaRuta += Direccion[i] + "/";
                }
                filePath = NuevaRuta + Path.GetFileName(file.FileName);
            }


            Data.Instancia.LecturaArchivo(filePath, fileName, m, "",verificarM);
            return View();
        }


        

        // GET: IngresoData/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngresoData/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: IngresoData/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IngresoData/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: IngresoData/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IngresoData/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
