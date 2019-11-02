﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio2_EDD2.Models
{
    public class SubirArchivo
    {
        public int llave { get; set; }
        public string clave { get; set; }
        public int m { get; set; }
        public string llenado { get; set; }
        public int P { get; set; }
        public int Q { get; set; }


        public void SubirArchivos(string ruta, HttpPostedFileBase file)
        {
            try
            {
                file.SaveAs(ruta);
            }
            catch (Exception) { }
        }
    }
}
