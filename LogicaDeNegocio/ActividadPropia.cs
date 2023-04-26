using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogicaDeNegocio {

    public class ActividadPropia : Actividad, IValidate { 
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
            
        public void Validate() { //el base() aca es para que se llame al Validate de la clase padre primero
            base.Validate();
            if (_responsable == "") {
                throw new Exception("El nobre del responsable no puede ser vacio");
            }
        }

        public override string AgregarAgenda(UsuarioHuesped huesped) {
            string resultado = "";
            //base.AgregarAgenda(huesped, estadoAgenda);
            int nivel = huesped.NivelFidelizacion;
            decimal costoFinal = obtenerCostoFinal(nivel);
            EstadoAgenda estadoAgenda = new EstadoAgenda();
            if (costoFinal == 0) {
                estadoAgenda = EstadoAgenda.CONFIRMADA;
            }
            else {
                estadoAgenda = EstadoAgenda.PENDIENTE_PAGO;

            }


            Agenda agenda = new Agenda(huesped, estadoAgenda, costoFinal);
            this._agendas.Add(agenda);

            resultado = "Huesped: \n" + "Nombre: " + huesped.Nombre + "\nApellido: "
                   + huesped.Apellido + "\nActividad: " + "\nNombre Actividad: " + this.Nombre + "\nFecha: "
                   + this.Fecha.ToString() + "\nLugar: " + this.Lugar;
            if (costoFinal == 0) {
                resultado += "\nCosto: Actividad gratuita";
            }
            else {
                resultado += "\nCosto: " + agenda.CostoFinal.ToString();
            }
            resultado += "\nEstado agenda: " + agenda.EstadoAgenda.ToString();

            return resultado;
        }

        public decimal obtenerCostoFinal(int nivel) {
            decimal costoFinal = 0;
            switch (nivel) {
                case 1:
                    costoFinal = this.Costo ;
                    break;
                case 2:
                    costoFinal = this.Costo * (decimal)0.9;
                    break;
                case 3:
                    costoFinal = this.Costo * (decimal)0.85;
                    break;
                case 4:
                    costoFinal = this.Costo * (decimal)0.8;
                    break;
                default: break;
            }
            return costoFinal;
        }
        #endregion
    }
}
