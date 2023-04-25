using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    public class AdminHostel
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Actividad> _actividades = new List<Actividad>();
        private List<Proveedor> _proveedores = new List<Proveedor>();

        #region Metodos
        // Validar que no exista un usuario con los datos ingresados
        public UsuarioHuesped BuscarHuesped(string nroDocumentoHuesped, TipoDocumento tipoDocumentoHuesped) //mejor que un existe es usar un Buscar
        {
            UsuarioHuesped huesped = null;
            UsuarioHuesped huespedAux = null;
            bool existe = false;
            int i = 0;

            while (i < _usuarios.Count && existe == false) {
                huespedAux = _usuarios[i] as UsuarioHuesped;
                if (huespedAux != null) {
                    if (huespedAux.NroDocumento == nroDocumentoHuesped && huespedAux.TipoDoc == tipoDocumentoHuesped) {
                        huesped = huespedAux;
                        existe = true;
                    }
                }
                i++;
            }

            
            return huesped;
        }
        #endregion

        public Proveedor BuscarProveedor(string nombre) {
            Proveedor prov = null;
            bool existe = false;
            int i = 0;

            while ( i < _proveedores.Count && existe ==false) { //lo hago con un while para que no recorra innecesariamente
                if (_proveedores[i].NombreProveedor == nombre) {
                    existe = true;
                    prov = _proveedores[i];
                }
                i++;
            }
            
            return prov;
        }

        public void AltaProveedor(string nombre,string telefonoProveedor,string direccionProveedor,int descuentoFijo) {
            Proveedor prov = new Proveedor(nombre, telefonoProveedor, direccionProveedor, descuentoFijo);
               
            try {
                prov.Validate(); //primero lo valido, si da error ya catchea la excepcion y no continua la ejecucion
            
                if (BuscarProveedor(nombre) == null) { //busco el proveedor, si da null es que no existe y puedo agregarlo
                    _proveedores.Add(prov);
                } else {  //si no da null arrojo excepcion
                    throw new Exception("Ya existe un proveedor con este nombre");
                }
            }
            catch {
                throw; //aca paso a Program la excepcion que dio validate  de Proveedor
            }

        }

        public Actividad BuscarActividad(string nombre,DateTime fecha) { //busco la actividad para validar cuando hago el alta
            Actividad act = null;
            bool existe = false;
            int i = 0;

            while (i < _actividades.Count && existe == false) { //lo hago con un while para que no recorra innecesariamente
                if (_actividades[i].Nombre == nombre && _actividades[i].Fecha == fecha) {
                    existe = true;
                    act = _actividades[i];
                }
                i++;
            }

            return act;

        }

        //revisar si la validacion de existencia es correcta

        public void AltaActividadPropia(string nombre, string descripcion, DateTime fecha, int cantMaxPer, int edadMinima, decimal costo, int cupos, string responsable, string lugar, bool exterior) { //aca se va a llamar a los metodos correspondientes para la subclase ActividadPropia
            ActividadPropia actividadPropia = new ActividadPropia(nombre,descripcion,fecha,cantMaxPer,edadMinima,costo,cupos,responsable,lugar,exterior);
            try {
                actividadPropia.Validate();
                Actividad act = BuscarActividad(nombre, fecha); //lo hago con actvidad porque reviso si existe una actividad (no importa el tipo) con los mismos datos "clave"
                if (act != null) {
                    _actividades.Add(actividadPropia);
                } else {
                    throw new Exception("Ya existe una Actividad con este nombre"); //puedo hacer el add porque es clase hija
                }
            } catch {
                throw; //aca paso a Program la excepcion que dio validate de actividadPropia
            }
        }

        //revisar si la validacion de existencia es correcta

        //

        public void AltaActividadTerciarizada(string nombre, string descripcion, DateTime fecha, int cantMaxPer, int edadMinima, decimal costo, int cupos, string nombreProveedor, bool confirmada, DateTime fechaConfirmacion) {//aca se va a llamar a los metodos correspondientes para la subclase ActividadTerciarizada
            //antes de todo buscamos al proveedor
            Proveedor prov = BuscarProveedor(nombreProveedor);
            if (prov != null) { //si no hay proveedor no puedo crear la actividad terciarizada por la agregacion que esta en UML
                //aca le paso el prov que encontre
                ActividadTerciarizada actividadTerciarizada = new ActividadTerciarizada(nombre, descripcion, fecha, cantMaxPer, edadMinima, costo, cupos, prov, confirmada, fechaConfirmacion);
                try {
                        actividadTerciarizada.Validate();
                        Actividad act = BuscarActividad(nombre, fecha); //lo hago con actvidad porque reviso si existe una actividad (no importa el tipo) con los mismos datos "clave"
                        if (act != null) {

                            _actividades.Add(actividadTerciarizada); //puedo hacer el add porque es clase hija
                        }
                        else {
                        throw new Exception("Ya existe una Actividad con este nombre");
                        }
                 }
                catch {
                    throw; //aca paso a Program la excepcion que dio validate de actividadPropia
                }
            } else {
                throw new Exception("No existe un proveedor con ese nombre");
            }
        }

        public void altaAgenda() {

        }

    }
}
