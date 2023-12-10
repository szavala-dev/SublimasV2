using SistemaSublimas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSublimas.AccesoDatos.Repositorio.IRepositorio
{
    public interface ITiendaRepositorio : IRepositorio<Tienda>
    {
        void Actualizar(Tienda tienda);
    }
}
