using SistemaSublimas.AccesoDatos.Data;
using SistemaSublimas.AccesoDatos.Repositorio.IRepositorio;
using SistemaSublimas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSublimas.AccesoDatos.Repositorio
{
    public class TiendaRepositorio : Repositorio<Tienda>, ITiendaRepositorio
    {
        private readonly ApplicationDbContext _db;
         

        public TiendaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Tienda tienda)
        {
            var tiendaBD = _db.Tiendas.FirstOrDefault(b => b.Id == tienda.Id);
            if (tiendaBD != null) 
            {
                tiendaBD.Nombre = tienda.Nombre;
                tiendaBD.Descripcion = tienda.Descripcion;  
                tiendaBD.Estado = tienda.Estado;
                _db.SaveChanges();
            }
        }
    }
}
