using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    public class AdminHostel
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Actividad> _actividades = new List<Actividad>();
        private List<Proveedor> _proveedores = new List<Proveedor>();

        #region Metodos
        // Validar que no exista un usuario con los datos ingresados
        public bool ExisteUsuario(string nroDocumentoHuesped, TipoDocumento tipoDocumentoHuesped)
        {
            bool existe = false;
            foreach (UsuarioHuesped user in _usuarios)
            {
                // Verifico que no exista otro huesped con este numero y tipo de documento
                if (user.NroDocumento == nroDocumentoHuesped && user.TipoDoc == tipoDocumentoHuesped)
                    {
                        existe = true;
                    }
                }
            return existe;
        }
        #endregion

    }
}
