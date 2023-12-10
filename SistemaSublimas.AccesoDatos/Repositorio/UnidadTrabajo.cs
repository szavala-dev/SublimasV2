using SistemaSublimas.AccesoDatos.Data;
using SistemaSublimas.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSublimas.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        
        public ITiendaRepositorio Tienda {  get; private set; }
        public UnidadTrabajo(ApplicationDbContext db)  
        {
            _db = db;
            Tienda = new TiendaRepositorio(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
