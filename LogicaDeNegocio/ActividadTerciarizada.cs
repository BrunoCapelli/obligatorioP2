using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio {
    public class ActividadTerciarizada : Actividad {
        private Proveedor _proveedor;
        private bool _isConfirmada;
        private DateTime _fechaConfirmacion;
    }
}
