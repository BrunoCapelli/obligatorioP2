using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio {
    public class ActividadTerciarizada : Actividad { //falta agregar interfaz
        private Proveedor _proveedor;
        private bool _isConfirmada;
        private DateTime _fechaConfirmacion;

        #region Propiedades
        public Proveedor Proveedor { get { return _proveedor;} ,set { _proveedor = value; } }

        public bool IsConfirmada { get { return _isConfirmada; } set {  _isConfirmada = value; } }

        public DateTime FechaConfirmacion { get {  return _fechaConfirmacion; } set { _fechaConfirmacion = value; } }
        #endregion

        #region Constructor 
        
        public ActividadTerciarizada(string nombre, string descripcion, DateTime fecha, int cantMaxPer, int edadMinima, decimal costo, int cupos,Proveedor proveedor,bool confirmada, DateTime fechaConfirmacion)
            :base(nombre, descripcion, fecha, cantMaxPer, edadMinima, costo, cupos) { //llamo al constructor de la clase padre
            this._proveedor= proveedor;
            this._isConfirmada= confirmada;
            this._fechaConfirmacion= fechaConfirmacion;
        }

        #endregion

        #region Metodos

        public void Validate() :base() { //como no tengo cosas para validar aca solo llamo en base al validate de la clase padre
            
        }

        #endregion
    }
}
