﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using Laboratorio2_EDD2.Helpers;
using Laboratorio2_EDD2.Models;

namespace Laboratorio2_EDD2.Controllers
{
    public class CargaDataController : Controller
    {
        // GET: IngresoData
        public ActionResult Index()
        {
            return View();
        }
        //
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
