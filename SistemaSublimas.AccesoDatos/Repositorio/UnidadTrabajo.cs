﻿using SistemaSublimas.AccesoDatos.Data;
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

        public ICategoriaRepositorio Categoria { get; private set; }

        public IMarcaRepositorio Marca { get; private set; }

        public IProductoRepositorio Producto { get; private set; }

        public IUsuarioAplicacionRepositorio UsuarioAplicacion { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)  
        {
            _db = db;
            Tienda = new TiendaRepositorio(_db);
            Categoria = new CategoriaRepositorio(_db);
            Marca = new MarcaRepositorio(_db);
            Producto = new ProductoRepositorio(_db);
            UsuarioAplicacion = new UsuarioAplicacionRepositorio(_db);
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
