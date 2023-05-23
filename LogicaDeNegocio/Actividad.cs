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
    public abstract class Actividad: IValidate, IComparable<Actividad>
    {
        private int _id;
        private string _nombre;
        private string _descripcion;
        private DateTime _fecha;
        private int _cantidadMaxPersonas;
        private int _edadMinima;
        protected List<Agenda> _agendas = new List<Agenda>();
        private decimal _costoFinal = 0;
        private int _cuposDisponibles; //sacar esto y calcularlo cada vez que damos el alta con cantidad maxima de personas
        private static int s_ultimoId = 0;


        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }

        public string Nombre { get { return _nombre; } set { _nombre = value; } }

        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }

        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }

        public int CantidadMaxPersonas { get { return _cantidadMaxPersonas; } set { _cantidadMaxPersonas = value; } }

        public int EdadMinina { get { return _edadMinima; } set { _edadMinima = value; } }

        public decimal Costo { get { return _costoFinal; } set { _costoFinal = value; } }

        public int CuposDisponibles { get { return _cuposDisponibles; } set { _cuposDisponibles = value; } }

        public List<Agenda> Agendas { get { return this._agendas; } } //aca solo puse el get porque el set es agregar un elemento a la lista, esta implementado mas abajo
        #endregion

        #region Constructor
        public Actividad(string nombre, string descripcion, DateTime fecha, int cantMaxPer, int edadMinima, decimal costo, int cupos) { //no intentar llamar a este metodo para un objeto Actividad, dara error por ser abstracto
            s_ultimoId += 1;
            _id = s_ultimoId;
            _nombre = nombre;
            _descripcion = descripcion;
            _fecha = fecha;
            _cantidadMaxPersonas = cantMaxPer;
            _edadMinima = edadMinima;
            _costoFinal = costo;
            _cuposDisponibles = cupos;
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


        public virtual string AgregarAgenda(UsuarioHuesped huesped) { //el comportamiento es distinto en cada subclase
            return "";
           
        }

        public int CompareTo(Actividad act)
        {
            return _costoFinal.CompareTo(act._costoFinal);
        }

        #endregion


    }




}
