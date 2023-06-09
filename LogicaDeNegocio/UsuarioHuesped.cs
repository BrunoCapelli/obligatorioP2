﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LogicaDeNegocio;


namespace LogicaDeNegocio 
{
    public class UsuarioHuesped : Usuario, IValidate {
        private TipoDocumento _tipoDoc;
        private string _nroDocumento;
        private string _habitacion;
        private DateTime _fechaNacimiento;
        private int _nivel;

       
        #region Propiedades
        public TipoDocumento TipoDoc {
            get { return _tipoDoc; }
            set { _tipoDoc = value; }
        }

        public string NroDocumento
        {
            get { return _nroDocumento; }
            set { _nroDocumento = value; }
        }

       
        public string Habitacion
        {
            get { return _habitacion; }
            set { _habitacion = value; }
        }
        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }

        public int NivelFidelizacion
        {
            get { return _nivel; }
            set { _nivel = value; }
        }
        #endregion

        #region Constructor
        public UsuarioHuesped(string Email, string Password, string Nombre, string Apellido, TipoDocumento tipoDoc, string NroDocumento, DateTime FechaNacimiento, string Habitacion, int Nivel): base(Email, Password, Nombre, Apellido)
        {
            _tipoDoc = tipoDoc;
            _nroDocumento = NroDocumento;
            _fechaNacimiento = FechaNacimiento;
            _habitacion = Habitacion;
            _nivel = Nivel;
        }
        #endregion

        #region Metodos

        public void Validate()
            
        {
            try
            {
                base.Validate(); 

                // Validar tipo de documento
                if(_tipoDoc != TipoDocumento.CI && _tipoDoc != TipoDocumento.PASAPORTE && _tipoDoc != TipoDocumento.OTROS)
                {
                    throw new Exception("Tipo de documento no valido.");
                }

                // Validar que el campo Habitacion no sea vacio
                if (_habitacion.Length < 0)
                {
                    throw new Exception("El campo habitacion no puede estar vacío.");
                }

                if (_nroDocumento.Length == 8)
                { //esto es por si me pasan una cedula de menos de 8 caracteres

                    int digitoVerificador = 0;
                    bool correcto = Int32.TryParse(_nroDocumento, out int nroCedula);
                    if (correcto == true)
                    { //esto es por si me pasan un string no numerico
                        int[] chequeo = { 8, 1, 2, 3, 4, 7, 6 };
                        int[] cedulaArray = new int[8];
                        for (int i = 0; i <= 7; i++)
                        {
                            cedulaArray[i] = (int)Char.GetNumericValue(_nroDocumento[i]);
                        }

                        for (int i = 0; i <= 6; i++)
                        {
                            digitoVerificador += chequeo[i] * cedulaArray[i];
                        }
                        if (digitoVerificador % 10 != cedulaArray[7])
                        { //esto es por si la cedula no es valida
                            throw new Exception("El documento ingresado no es valido.");
                        }
                    }
                    else
                    {
                        throw new Exception("El documento ingresado no es valido.");
                    }

                }
                else
                {
                    if (_tipoDoc == TipoDocumento.CI)
                    {
                        throw new Exception("El documento ingresado no es valido.");
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public int ObtenerEdad()
        {
            
            int edad = DateTime.Now.Year - FechaNacimiento.Year;

            return edad;
        }

        public override string VerificarRol()
        {
            return "Huesped";
        }

        #endregion
    }
}
