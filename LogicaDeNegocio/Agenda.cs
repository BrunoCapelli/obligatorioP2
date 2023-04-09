using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    internal class Agenda
    {
        private UsuarioHuesped _huesped;
        private Actividad _actividad;
        private EstadoAgenda _estadoAgenda;
        private DateTime _fechaCreacionAgenda;
        private DateTime _fechaActividad;
        private decimal costoFinal;
    }
}
