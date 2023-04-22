﻿using System;
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

        public void Validate() { //como no tengo cosas para validar aca solo llamo en base al validate de la clase padre
            base.Validate();
        }

        public override void AgregarAgenda(UsuarioHuesped huesped) {
            if (_isConfirmada == true) {
                int descuentoFijo = this.Proveedor.DescuentoFijo;
                decimal costoFinal = this.Costo;
                if (descuentoFijo != 0) {
                    costoFinal = this.Costo - this.Costo * 1 / descuentoFijo;
                }
                EstadoAgenda estadoAgenda = new EstadoAgenda();
                if (costoFinal == 0) {
                    estadoAgenda = EstadoAgenda.CONFIRMADA;
                }
                else {
                    estadoAgenda = EstadoAgenda.PENDIENTE_PAGO;

                }
                Agenda agenda = new Agenda(huesped, estadoAgenda,costoFinal);
                return ("Datos formateados");
            }
            else {
                throw new Exception("La actividad no esta confirmada");
            }
        }
        #endregion
    }
}
