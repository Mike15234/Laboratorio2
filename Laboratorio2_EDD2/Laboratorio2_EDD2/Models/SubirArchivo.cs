using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio2_EDD2.Models
{
    public class SubirArchivo
    {
        public int llave { get; set; }
        public string clave { get; set; }

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
