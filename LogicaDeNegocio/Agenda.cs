﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    public class Agenda: IComparable<Agenda>, IValidate
    {
        private UsuarioHuesped _huesped;
        private EstadoAgenda _estadoAgenda;
        private DateTime _fechaCreacionAgenda;
        private decimal _costoFinal;

        #region Propiedaes
        public UsuarioHuesped Huesped { get { return _huesped; } set { _huesped = value; } }
        public EstadoAgenda EstadoAgenda { get { return _estadoAgenda;} set { _estadoAgenda = value; } }
        public DateTime FechaCreacionAgenda { get { return _fechaCreacionAgenda;} set { _fechaCreacionAgenda = value; } }
        public decimal CostoFinal { get { return _costoFinal; } set { _costoFinal = value; } }
        #endregion

        #region Constructor
        public Agenda(UsuarioHuesped huesped,EstadoAgenda estadoAgenda,decimal costoFinal) { 
            _huesped = huesped;
            _estadoAgenda= estadoAgenda;
            _fechaCreacionAgenda= DateTime.Now;
            _costoFinal = costoFinal;
        }

        public int CompareTo(Agenda ag)
        {
            return _fechaCreacionAgenda.CompareTo(ag.FechaCreacionAgenda);
        }


        #endregion

        #region Metodos
        public void Confirmar() {
            _estadoAgenda = EstadoAgenda.CONFIRMADA;
        }

        public void Validate()
        {
            if(_huesped == null)
            {
                throw new Exception("El usuario no es valido");
            }
            if(_costoFinal < 0)
            {
                throw new Exception("El costo no es valido");
            }
        }
        #endregion

    }


}
