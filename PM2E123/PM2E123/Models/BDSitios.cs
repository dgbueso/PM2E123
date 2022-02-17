using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PM2E123.Models
{
    public class BDSitios
    {
        readonly SQLiteAsyncConnection db;

        //constructor de la clase DataBaseSQLite
        public BDSitios(string pathdb)
        {
            db = new SQLiteAsyncConnection(pathdb);
            db.CreateTableAsync<Sitios>().Wait();
        }

        //Operaciones crud de sqlite
        //Read List way
        public Task<List<Sitios>> ObtenerListaSitios()
        {
            return db.Table<Sitios>().ToListAsync();
        }

        //read one by one 
        public Task<Sitios> ObtenerSitios(int pcodigo)
        {
            return db.Table<Sitios>()
                .Where(i => i.codigo == pcodigo)
                .FirstOrDefaultAsync();
        }

        //Create o update personas
        public Task<int> GrabarSitios(Sitios sitio)
        {
            if (sitio.codigo != 0)
            {
                return db.UpdateAsync(sitio);
            }
            else
            {
                return db.InsertAsync(sitio);
            }

        }



        //delete
        public Task<int> EliminarSitios(Sitios localizacion)
        {
            return db.DeleteAsync(localizacion);
        }


    }
}

