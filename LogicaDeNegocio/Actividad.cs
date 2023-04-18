using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    public abstract class Actividad  //falta agregar interfaz
    {
        private int _id;
        private string _nombre;
        private string _descripcion;
        private DateTime _fecha;
        private int _cantidadMaxPersonas;
        private int _edadMinima;
        private List<Agenda> _agendas = new List<Agenda>();
        private decimal _costo = 0;
        private int _cuposDisponibles;
        private static int s_ultimoId;


        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }

        public string Nombre { get { return _nombre; } set { _nombre = value; } }

        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }

        public DateTime Fecha { get { return _fecha; } set { fecha = value; } }

        public int CantidadMaxPersonas { get { return _cantidadMaxPersonas; } set { _cantidadMaxPersonas = value; } }

        public int EdadMinina { get { return _edadMinima; } set { _edadMinima = value; } }

        public decimal Costo { get { return _costo; } set { _costo = value; } }

        public int CuposDisponibles { get { return _cuposDisponibles; } set { _cuposDisponibles = value; } }

        public List<Agenda> Agendas { get { return this._agendas; } } //aca solo puse el get porque el set es agregar un elemento a la lista, esta implementado mas abajo
        #endregion

        #region Constructor
        public Actividad(string nombre, string descripcion, DateTime fecha, int cantMaxPer, int edadMinima, decimal costo, int cupos) { //no intentar llamar a este metodo para un objeto Actividad, dara error por ser abstracto
            s_ultimoId += 1
            _id = s_ultimoId
            _nombre = nombre;
            _descripcion = descripcion;
            _fecha = fecha;
            _cantidadMaxPersonas = cantMaxPer;
            _edadMinima = edadMinima;
            _costo = costo;
            _cuposDisponibles = cupos;
        }
        #endregion



        #region Metodos
        public void Validate() {
            if (this._nombre == "") {
                throw new Exception("El nombre no puede ser vacio");

            } else {
                if (this._descripcion == "") {
                    throw new Exception("La descripcion no puede ser vacia");
                } else {
                    if (this._nombre.Length > 25) {
                        throw new Exception("El nombre debe tener hasta 25 caracteres");
                    }
                }
            }
        }


        public void AgregarAgenda(Agenda agenda) {
            this._agendas.Add(agenda);
        }

        #endregion


    }




}
