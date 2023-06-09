﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    public abstract class Actividad: IComparable<Actividad>, IValidate
    {
        private int _id;
        private string _nombre;
        private string _descripcion;
        private DateTime _fecha;
        private int _cantidadMaxPersonas;
        private int _edadMinima;
        protected List<Agenda> _agendas = new List<Agenda>();
        private decimal _costoFinal = 0;
        private static int s_ultimoId = 0;


        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }

        public string Nombre { get { return _nombre; } set { _nombre = value; } }

        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }

        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }

        public int CantidadMaxPersonas { get { return _cantidadMaxPersonas; } set { _cantidadMaxPersonas = value; } }

        public int EdadMinina { get { return _edadMinima; } set { _edadMinima = value; } }

        public decimal Costo { get { return _costoFinal; } set { _costoFinal = value; } }

        public List<Agenda> Agendas { get { return this._agendas; } } 
        #endregion

        #region Constructor
        public Actividad(string nombre, string descripcion, DateTime fecha, int cantMaxPer, int edadMinima, decimal costo) { 
            s_ultimoId += 1;
            _id = s_ultimoId;
            _nombre = nombre;
            _descripcion = descripcion;
            _fecha = fecha;
            _cantidadMaxPersonas = cantMaxPer;
            _edadMinima = edadMinima;
            _costoFinal = costo;
        }
        #endregion



        #region Metodos
        public void Validate() {
            if (this._nombre == "") {
                throw new Exception("El nombre no puede ser vacio");

            } else { //aca entra si el nombre esta bien
                if (this._descripcion == "") {
                    throw new Exception("La descripcion no puede ser vacia");
                } else { //aca entra si el nombre y la descripcion estan bien
                    if (this._nombre.Length > 25) {
                        throw new Exception("El nombre debe tener hasta 25 caracteres");
                    }
                }
            }
        }


        public abstract void AgregarAgenda(UsuarioHuesped huesped);

        public int CompareTo(Actividad act) {
            return _costoFinal.CompareTo(act._costoFinal);
        }

        public bool hayCupos() {
            int resultado = _cantidadMaxPersonas - _agendas.Count;
            return resultado > 0;
        }

        public bool HuespedEnAgenda(UsuarioHuesped huesped) {
            bool esta = false;
            int i = 0;
            while (i < _agendas.Count && esta == false) {
                if (_agendas[i].Huesped == huesped) {
                    esta = true;
                }
                i++;
            }

            return esta;
        }

        
        public virtual string GetProveedor() {
            string proveedor = null;
            return proveedor;
        }


        public abstract string TipoActividad();

        public virtual string GetLugar()
        {
            return "A definir";
        }

        
        #endregion
       
    }




}
