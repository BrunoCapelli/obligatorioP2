using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    public abstract class Actividad
    {
        private int _id;
        private string _nombre;
        private string descripcion;
        private DateTime _fecha;
        private int _cantidadMaxPersonas;
        private int _edadMinima;
        // private List<Agenda> _agendas = new List<Agenda>();
        private decimal _costo;
        private int _cuposDisponibles;
        private static int s_ultimoId;
    }

    public class ActividadPropia: Actividad 
    {
        private string _responsable;
        private string _lugar;
        private bool _isExterior;
    }

    public class ActividadTerciarizada: Actividad
    {
        private Proveedor _proveedor;
        private bool _isConfirmada;
        private DateTime _fechaConfirmación;
    }



}
