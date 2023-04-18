using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio {

    public class ActividadPropia : Actividad { //falta agregar interfaz
        private string _responsable;
        private string _lugar;
        private bool _isExterior;

        #region Propiedades
        public string Responsable { get { return _responsable; } set { _responsable = value; } }
            
        public string Lugar { get { return _lugar; } set { _lugar = value; } }

        public bool IsExterior { get { return _isExterior; } set { _isExterior = value; } }
        #endregion

        #region Constructor

        public ActividadPropia(string nombre, string descripcion, DateTime fecha, int cantMaxPer, int edadMinima, decimal costo, int cupos,string responsable,string lugar,bool exterior)
            :base(nombre, descripcion, fecha, cantMaxPer, edadMinima, costo, cupos) { //el base es para que se llame al constructor de la clase padre
            _responsable = responsable;
            _lugar = lugar;
            _isExterior = exterior;
        }

        #endregion

        #region Metodos
            
        public void Validate() :base() { //el base() aca es para que se llame al Validate de la clase padre primero
            if (_responsable == "") {
                throw new Exception("El nobre del responsable no puede ser vacio");
            }
        }

        #endregion
    }
}
