using System;
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
            Data.Instancia.LecturaArchivo(filePath, fileName, llave, "");
            return View();
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
            if (Path.GetExtension(filename) == ".huff" || Path.GetExtension(filename) == ".txt" || Path.GetExtension(filename) == ".LZW")
            {
                string fullpath = Path.Combine(Server.MapPath("~/Uploads"), filename);
                return File(fullpath, "LZW/huff");

            }
            else
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }
        }
        
        //VISTA ERROR
        public ActionResult error()
        {
            return View();
        }
        
        //CESAR
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
                Data.Instancia.LecturaArchivo(filePath, fileName, 0,llave);

            }
            else
            {

                return View("error"); 

            }
            
            return View();
        }
        public ActionResult CesarDescifrado()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CesarDescifrado(HttpPostedFileBase file, string llave)
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
                Data.Instancia.LecturaArchivo(filePath, fileName, 1, llave);

            }
            else
            {

                return View("error");

            }

            return View();
        }

        //ESPIRAL
        public ActionResult CifrarEspirarl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CifrarEspiral(HttpPostedFileBase file, int m, string llenado)
        {
            SubirArchivo recibirkey = new SubirArchivo();
            int verificarM;
            recibirkey.m = m;
            var fileName = Path.GetFileName(file.FileName);
            file.SaveAs(Server.MapPath(@"~\Uploads\" + fileName));
            string filePath = string.Empty;

            if (llenado == "horizontal")
            {
                verificarM = 0;
            }
            else if(llenado == "Vertical")
            {
                verificarM = 1;
            }
            else
            {
                //mmm escriba bien xfa
                return View("error");
            }
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
            
            return View();
        }


        // GET: IngresoData/Details/5
        public ActionResult Details(int id)
        {
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
