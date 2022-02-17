using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace PM2E123.Models
{
    public class Sitios
    {
        [PrimaryKey, AutoIncrement]
        public int codigo { get; set; }

        public double latitud { get; set; }
        public double longitud { get; set; }


        public string direccion { get; set; }

        public string base64 { get; set; }
    }
}
