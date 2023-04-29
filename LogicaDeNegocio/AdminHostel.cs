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


        public Proveedor BuscarProveedor(string nombre) {
            Proveedor prov = null;
            bool existe = false;
            int i = 0;

            while (i < _proveedores.Count && existe == false) { //lo hago con un while para que no recorra innecesariamente
                if (_proveedores[i].NombreProveedor == nombre) {
                    existe = true;
                    prov = _proveedores[i];
                }
                i++;
            }

            return prov;
        }

        public void AltaProveedor(string nombre, string telefonoProveedor, string direccionProveedor, int descuentoFijo)
        {
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

        public Actividad BuscarActividad(string nombre, DateTime fecha)
        { //busco la actividad para validar cuando hago el alta
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

        public void AltaActividadPropia(string nombre, string descripcion, string fechaIn, int cantMaxPer, int edadMinima, decimal costo, int cupos, string responsable, string lugar, bool exterior)
        { //aca se va a llamar a los metodos correspondientes para la subclase ActividadPropia
            DateTime fecha = new DateTime();
            DateTime.TryParse(fechaIn, out fecha);

            ActividadPropia actividadPropia = new ActividadPropia(nombre, descripcion, fecha, cantMaxPer, edadMinima, costo, cupos, responsable, lugar, exterior);
            try {
                actividadPropia.Validate();
                Actividad act = BuscarActividad(nombre, fecha); //lo hago con actvidad porque reviso si existe una actividad (no importa el tipo) con los mismos datos "clave"
                if (act == null) {
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

        public void AltaActividadTerciarizada(string nombre, string descripcion, string fechaIn, int cantMaxPer, int edadMinima, decimal costo, int cupos, string nombreProveedor, bool confirmada, string fechaConfirmacionIn)
        {//aca se va a llamar a los metodos correspondientes para la subclase ActividadTerciarizada
         //antes de todo buscamos al proveedor

            DateTime fecha = new DateTime();
            DateTime.TryParse(fechaIn, out fecha);

            DateTime fechaConfirmacion = new DateTime();
            DateTime.TryParse(fechaConfirmacionIn, out fechaConfirmacion);

            Proveedor prov = BuscarProveedor(nombreProveedor);
            if (prov != null) { //si no hay proveedor no puedo crear la actividad terciarizada por la agregacion que esta en UML
                //aca le paso el prov que encontre
                ActividadTerciarizada actividadTerciarizada = new ActividadTerciarizada(nombre, descripcion, fecha, cantMaxPer, edadMinima, costo, cupos, prov, confirmada, fechaConfirmacion);
                try {
                    actividadTerciarizada.Validate();
                    Actividad act = BuscarActividad(nombre, fecha); //lo hago con actvidad porque reviso si existe una actividad (no importa el tipo) con los mismos datos "clave"
                    if (act == null) {

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

        public string ListarActividades()
        {
            string resultado = null;
            resultado = "Actividades: \n";
            foreach (ActividadTerciarizada act in _actividades.OfType<ActividadTerciarizada>())
            {
                resultado += $"\n ID: {act.Id}" +
                    $"\n Nombre: {act.Nombre}" +
                    $"\n Descripcion: {act.Descripcion} " +
                    $"\n Fecha: {act.Fecha} " +
                    $"\n Cant. max. personas: {act.CantidadMaxPersonas} " +
                    $"\n Edad min para realizarla: {act.EdadMinina} \n";
            }

            foreach (ActividadPropia act in _actividades.OfType<ActividadPropia>())
            {
                resultado += $"\n ID: {act.Id}" +
                    $"\n Nombre: {act.Nombre}" +
                    $"\n Descripcion: {act.Descripcion} " +
                    $"\n Fecha: {act.Fecha} " +
                    $"\n Cant. max. personas: {act.CantidadMaxPersonas} " +
                    $"\n Edad min para realizarla: {act.EdadMinina} \n";
            }

            return resultado;
        }

        public string ListarProveedores()
        {
            ListaProveedoresOrdenada();
            string resultado = "Proveedores";

            foreach (Proveedor prov in _proveedores)
            {
                resultado += $" \n Nombre : {prov.NombreProveedor} " +
                    $"\n Telefono: {prov.TelefonoProveedor} " +
                    $"\n Direccion: {prov.DireccionProveedor} " +
                    $"\n Descuento: {prov.DescuentoFijo}\n";
            }
            return resultado;
        }

        public void AltaHuesped(UsuarioHuesped userHuesped)
        {
            try
            {
                userHuesped.Validate();
                if (BuscarHuesped(userHuesped.Nombre, userHuesped.TipoDoc) == null)
                {
                    _usuarios.Add(userHuesped);
                }

            } catch
            {
                throw;
            }

        }

        public List<Proveedor> ListaProveedoresOrdenada()
        {
            _proveedores.Sort();
            return _proveedores;
        }

        public void EstablecerDescuento(string nombreProveedor, int descuento)
        {
            Proveedor prov = BuscarProveedor(nombreProveedor);
            if (prov != null)
            {

                prov.DescuentoFijo = descuento;
            }
            else
            {
                throw new Exception("No se encontro el proveedor");
            }
        }

        public string ListarActividadesFiltradas(decimal costo, DateTime fechaDesde, DateTime fechaHasta)
        {

            try
            {
                if (costo >= 0 && fechaDesde > DateTime.MinValue && fechaHasta > DateTime.MinValue) // Valido el costo y las fechas ingresadas
                {
                    this._actividades.Sort(); // Ordena por costo
                    string resultado = "Actividades \n";
                    foreach (Actividad act in _actividades)
                    {
                        if (act.Fecha >= fechaDesde && act.Fecha <= fechaHasta && act.Costo >= costo)
                        {
                            resultado += $" Actividad \n ID: {act.Id}" +
                                $" \n Descripcion: {act.Descripcion} " +
                                $"\n Fecha: {act.Fecha} " +
                                $"\n Cant. max. personas: {act.CantidadMaxPersonas} " +
                                $"\n Edad min para realizarla: {act.EdadMinina}";
                        }
                    }

                    return resultado;
                }
                else
                {
                    throw new Exception("Los datos ingesados no son validos");
                }
            } catch
            {
                throw;
            }






        }
        #endregion
        #region Constructor
    
        public AdminHostel()
        {
            // Precarga de Proveedores
            AltaProveedor("DreamWorks S.R.L.", "23048549", "Suarez 3380 Apto 304", 10);
            AltaProveedor("Estela Umpierrez S.A.", "33459678", "Lima 2456", 7);
            AltaProveedor("TravelFun", "29152020", "Misiones 1140", 9);
            AltaProveedor("Rekreation S.A.", "29162019", "Bacacay 1211", 11);
            AltaProveedor("Alonso & Umpierrez", "24051920", "18 de Julio 1956 Apto 4", 10);
            AltaProveedor("Electric Blue", "26018945", "Cooper 678", 5);
            AltaProveedor("Lúdica S.A.", "26142967", "Dublin 560", 4);
            AltaProveedor("Gimenez S.R.L.", "29001010", "Andes 1190", 7);
            AltaProveedor("", "22041120", "Agraciada 2512 Apto. 1", 8);
            AltaProveedor("Norberto Molina", "22001189", "Paraguay 2100", 9);

            // Precarga de Actividades Propias
            AltaActividadPropia("Yoga", "Masajes, belleza, relax", "01/11/2023", 25, 18, 10, 25, "Juan Orozco", "Spa", false);
            AltaActividadPropia("Baila con Ritmo", "Clase de baile de salsa y bachata", "05/07/2023", 20, 16, 15, 15, "Pedro Rodríguez", "Salon principal", false);
            AltaActividadPropia("Sabores del Caribe", "Comida caribeña ", "15/08/2023", 25, 18, 50, 20, "Laura Fernández", "Segunda planta", false);
            AltaActividadPropia("Spa Relax", "Día de relajación en el spa del hotel con masajes, tratamientos de belleza", "25/09/2023", 10, 21, 100, 8, "Ana María Torres", "Spa del hotel", false);
            AltaActividadPropia("Arte y Pintura", "Clase de pintura para principiantes", "10/10/2023", 15, 12, 20, 12, "Sofía Gómez", "Terraza del hotel", true);
            AltaActividadPropia("Cantando con Amigos", "Noche de karaoke", "20/11/2023", 30, 18, 10, 25, "Juan García", "Bar del hotel", false);
            AltaActividadPropia("Sabores del Mundo", "Clase de cocina", "15/01/2023", 12, 16,30, 10, "Ana López", "Cocina del hotel", false);
            AltaActividadPropia("Fiesta de Disfraces", "Fiesta de disfraces con música en vivo", "25/02/2023", 50, 21, 25, 40, "María García", "Patio de eventos", true);
            AltaActividadPropia("Gimnasia Acuática", "Gimnasia acuática en la piscina", "20/05/2023", 15, 16, 5, 12, "José Martínez", "Piscina del hotel", true);
            AltaActividadPropia("Tour Histórico", "Tour guiado por los lugares históricos más importantes de la ciudad", "05/03/2023", 20, 16, 25, 15, "Alejandro Gómez", "Recepción del hotel", false);

            // Precarga de Actividades Terciarizadas
            AltaActividadTerciarizada("Paseo en caballo", "Paseo en caballo por el jardin del hotel", "15/02/2023", 25, 16, 12, 65, "Alonso & Umpierrez", true, "")

        }

        





        #endregion
    }
}
