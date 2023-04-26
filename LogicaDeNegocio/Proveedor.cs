using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    public class Proveedor: IComparable<Proveedor>, IValidate 
    {
        private string _nombreProveedor;
        private string _telefonoProveedor;
        private string _direccionProveedor;
        private int _descuentoFijo;

        #region Propiedades
        public string NombreProveedor { get { return _nombreProveedor;} set { _nombreProveedor = value; } }

        public string TelefonoProveedor { get { return _telefonoProveedor; } set { _telefonoProveedor = value; } }

        public string DireccionProveedor { get { return _direccionProveedor; } set { _direccionProveedor = value; } }

        public int DescuentoFijo { get { return _descuentoFijo; } set { _descuentoFijo = value; } }
        #endregion

        #region Constructor
        public Proveedor(string nombreProveedor,string telefonoProveedor,string direccionProveedor,int descuentoFijo) {
            this._nombreProveedor=nombreProveedor;
            this._telefonoProveedor=telefonoProveedor;
            this._direccionProveedor=direccionProveedor;
            this._descuentoFijo=descuentoFijo;
        }

        #endregion

        #region Metodos
        public void Validate() 
        {
            if (this._telefonoProveedor == "") {
                throw new Exception("El telefono no puede ser vacio");
            } else if (this._direccionProveedor == "") {
                throw new Exception("La direccion no puede ser vacia");
            }

        }

        public int CompareTo(Proveedor prov)
        {
            return _nombreProveedor.CompareTo(prov.NombreProveedor);
        }
        #endregion

    }


}
