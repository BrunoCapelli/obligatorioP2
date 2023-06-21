using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaDeNegocio
{
    public class AdminHostel
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Actividad> _actividades = new List<Actividad>();
        private List<Proveedor> _proveedores = new List<Proveedor>();
        private static AdminHostel s_instance;

        #region Singleton
        public static AdminHostel GetInstancia
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new AdminHostel();
                }
                return s_instance;
            }
        }
        #endregion

        #region Metodos
        

        public UsuarioHuesped BuscarHuesped(string nroDocumentoHuesped, TipoDocumento tipoDocumentoHuesped) 
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

        public Usuario BuscarPorEmail(string userEmail)
        {
            Usuario user = null;
            Usuario userAux = null;
            bool existe = false;
            int i = 0;

            while (i < _usuarios.Count && existe == false)
            {
                userAux = _usuarios[i];
                if (userAux != null)
                {
                    if (userAux.Email == userEmail)
                    {
                        user = userAux;
                        existe = true;
                    }
                }
                i++;
            }


            return user;
        }

        public UsuarioHuesped BuscarHuespedPorEmail(string userEmail) {
            UsuarioHuesped huesped = null;
            Usuario usuario = null;
            bool existe = false;
            int i = 0;

            while (i < _usuarios.Count && existe == false) {
                usuario = _usuarios[i];
                if (usuario != null) {
                    if (usuario.Email == userEmail) {
                        huesped = usuario as UsuarioHuesped;
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

            while (i < _proveedores.Count && existe == false) { 
                if (_proveedores[i].NombreProveedor.ToUpper() == nombre.ToUpper()) {
                    existe = true;
                    prov = _proveedores[i];
                }
                i++;
            }

            return prov;
        }

        public Agenda BuscarAgenda(string nombreActividad, DateTime fechaActividad)
        {
            Agenda agenda = null;
            bool existe = false;
            int i = 0;

            while (i < _actividades.Count)
            { 
                if (_actividades[i].Nombre.ToUpper() == nombreActividad.ToUpper() && _actividades[i].Fecha == fechaActividad)
                { 
                    agenda = _actividades[i].Agendas[0];
                    existe = true;
                }
                i++;
            }

            return agenda;
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
        { 
            Actividad act = null;
            bool existe = false;
            int i = 0;

            while (i < _actividades.Count && existe == false) {
                if (_actividades[i].Nombre == nombre && _actividades[i].Fecha == fecha) {
                    existe = true;
                    act = _actividades[i];
                }
                i++;
            }

            return act;

        }

        public Actividad BuscarActividad(int id) 
        { 
            Actividad act = null;
            bool existe = false;
            int i = 0;

            while (i < _actividades.Count && existe == false) { 
                if (_actividades[i].Id == id) {
                    existe = true;
                    act = _actividades[i];
                }
                i++;
            }

            return act;

        }

       

        public void AltaActividadPropia(string nombre, string descripcion, string fechaIn, int cantMaxPer, int edadMinima, decimal costo, string responsable, string lugar, bool exterior)
        { 
            DateTime fecha = new DateTime();
            DateTime.TryParse(fechaIn, out fecha);

            ActividadPropia actividadPropia = new ActividadPropia(nombre, descripcion, fecha, cantMaxPer, edadMinima, costo, responsable, lugar, exterior);
            try {
                actividadPropia.Validate();
                Actividad act = BuscarActividad(nombre, fecha); 
                if (act == null) {
                    _actividades.Add(actividadPropia);
                } else {
                    throw new Exception("Ya existe una Actividad con este nombre"); 
                }
            } catch {
                throw; //aca paso a Program la excepcion que dio validate de actividadPropia
            }
        }

        

        public void AltaActividadTerciarizada(string nombre, string descripcion, string fechaIn, int cantMaxPer, int edadMinima, decimal costo, string nombreProveedor, bool confirmada, string fechaConfirmacionIn)
        {

            DateTime fecha = new DateTime();
            DateTime.TryParse(fechaIn, out fecha);

            DateTime fechaConfirmacion = new DateTime();
            DateTime.TryParse(fechaConfirmacionIn, out fechaConfirmacion);

            Proveedor prov = BuscarProveedor(nombreProveedor);

            if (prov != null) { //si no hay proveedor no puedo crear la actividad terciarizada por la agregacion que esta en UML
                //aca le paso el prov que encontre
                ActividadTerciarizada actividadTerciarizada = new ActividadTerciarizada(nombre, descripcion, fecha, cantMaxPer, edadMinima, costo, prov, confirmada, fechaConfirmacion);
                try {
                    actividadTerciarizada.Validate();
                    Actividad act = BuscarActividad(nombre, fecha); 
                    if (act == null) {

                        _actividades.Add(actividadTerciarizada); 
                    }
                    else {
                        throw new Exception("Ya existe una Actividad con este nombre");
                    }
                }
                catch {
                    throw; 
                }
            } else {
                throw new Exception("No existe un proveedor con ese nombre");
            }
        }

        public void AltaAgenda(string userDoc, TipoDocumento tipoDoc, string nomActividad, DateTime fechaAct) 
        {

            UsuarioHuesped userH = BuscarHuesped(userDoc, tipoDoc);

            Actividad act = BuscarActividad(nomActividad, fechaAct);

            if(userH.ObtenerEdad() > 18)
            {
               act.AgregarAgenda(userH);

            }
            else {
                throw  new Exception("El usuario es menor de edad");
            }
        }

        public List<Actividad> ListarActividades()
        { 
            return _actividades;
        }

        public List<Actividad> ListarActividadesPorFecha()
        {
           _actividades.Sort();
            return _actividades;
        }

        public List<Proveedor> ListarProveedores()
        {
            List<Proveedor> listaProv = new List<Proveedor>();
            foreach(Proveedor p in _proveedores)
            {
                listaProv.Add(p);
            }
            return listaProv;
        }

        public void AltaHuesped(string Email, string Password, string Nombre, string Apellido, TipoDocumento tipoDoc, string NroDocumento, DateTime FechaNacimiento, string Habitacion, int Nivel)
        {
            UsuarioHuesped userHuesped = new UsuarioHuesped(Email, Password, Nombre, Apellido, tipoDoc, NroDocumento, FechaNacimiento, Habitacion, Nivel);
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

        public List<Agenda> ListarAgendas()
        {
            List<Agenda> agendasAux = new List<Agenda>();
            foreach(Actividad a in _actividades)
            {
                foreach(Agenda ag in a.Agendas)
                {
                    agendasAux.Add(ag);
                }
            }
            return agendasAux;
        }

        public void precargaDatos()
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

            AltaActividadPropia("Yoga", "Masajes, belleza, relax", "01/11/2023", 25, 18, 10, "Juan Orozco", "Spa", false);
            AltaActividadPropia("Baila con Ritmo", "Clase de baile de salsa y bachata", "05/07/2023", 20, 16, 15, "Pedro Rodríguez", "Salon principal", false);
            AltaActividadPropia("Sabores del Caribe", "Comida caribeña ", "15/08/2023", 25, 18, 50, "Laura Fernández", "Segunda planta", false);
            AltaActividadPropia("Spa Relax", "Día de relajación en el spa del hotel con masajes, tratamientos de belleza", "25/09/2023", 10, 21, 100, "Ana María Torres", "Spa del hotel", false);
            AltaActividadPropia("Arte y Pintura", "Clase de pintura para principiantes", "10/10/2023", 15, 12, 20, "Sofía Gómez", "Terraza del hotel", true);
            AltaActividadPropia("Cantando con Amigos", "Noche de karaoke", "20/11/2023", 30, 18, 10, "Juan García", "Bar del hotel", false);
            AltaActividadPropia("Sabores del Mundo", "Clase de cocina", "15/01/2023", 12, 16, 30, "Ana López", "Cocina del hotel", false);
            AltaActividadPropia("Fiesta de Disfraces", "Fiesta de disfraces con música en vivo", "25/02/2023", 50, 21, 0, "María García", "Patio de eventos", true);
            AltaActividadPropia("Gimnasia Acuática", "Gimnasia acuática en la piscina", "20/05/2023", 15, 16, 5, "José Martínez", "Piscina del hotel", true);
            AltaActividadPropia("Tour Histórico", "Tour guiado por los lugares históricos más importantes de la ciudad", "05/03/2023", 20, 16, 25, "Alejandro Gómez", "Recepción del hotel", false);


            // Precarga de Actividades Terciarizadas
            AltaActividadTerciarizada("Paseo en caballo", "Paseo en caballo por el jardin del hotel", "15/02/2023", 25, 16, 12, "DreamWorks S.R.L.", true, "01/11/2023");
            AltaActividadTerciarizada("Senderismo", "Excursión de senderismo por las montañas", "05/03/2023", 15, 18, 25, "DreamWorks S.R.L.", false, "30/06/2023");
            AltaActividadTerciarizada("Parapente", "Experiencia de vuelo en parapente con guía profesional", "15/04/2023", 10, 21, 80, "DreamWorks S.R.L.", false, "30/11/2023");

            AltaActividadTerciarizada("Buceo", "Inmersión de buceo en los arrecifes", "20/05/2023", 8, 18, 100, "Estela Umpierrez S.A.", false, "30/09/2023");
            AltaActividadTerciarizada("Paintball", "Juego de paintball en campo de batalla", "10/06/2023", 20, 16, 20, "Estela Umpierrez S.A.", false, "30/12/2023");
            AltaActividadTerciarizada("Buggy", "Experiencia de manejo de buggy todo terreno", "15/07/2023", 10, 21, 60, "Estela Umpierrez S.A.", false, "31/10/2023");

            AltaActividadTerciarizada("Kitesurf", "Clase de kitesurf con equipo incluido", "20/08/2023", 6, 18, 150, "TravelFun", false, "31/12/2023");
            AltaActividadTerciarizada("Escalada en Roca", "Excursión de escalada en roca", "15/09/2023", 12, 16, 40, "TravelFun", false, "31/03/2023");
            AltaActividadTerciarizada("Rafting", "Excursión de rafting por el río", "20/10/2023", 8, 18, 70, "TravelFun", false, "30/04/2023");

            AltaActividadTerciarizada("Observación de Estrellas", "Noche de observación de estrellas con telescopios", "15/11/2023", 20, 12, 5, "Rekreation S.A.", false, "28/02/2023");
            AltaActividadTerciarizada("Globo Aerostático", "Experiencia de vuelo en globo aerostático", "20/12/2023", 4, 21, 200, "Rekreation S.A.", false, "30/06/2023");
            AltaActividadTerciarizada("Taller de cocina", "Aprende a cocinar platos típicos de la región", "10/03/2023", 20, 18, 25, "Rekreation S.A.", true, "15/03/2023");

            AltaActividadTerciarizada("Tour en bicicleta", "Recorre los mejores paisajes en bicicleta", "20/09/2023", 15, 14, 10, "Alonso & Umpierrez", true, "25/04/2023");
            AltaActividadTerciarizada("Clases de yoga", "Despierta tu cuerpo y mente con yoga", "05/05/2023", 30, 16, 8, "Alonso & Umpierrez", true, "10/05/2023");
            AltaActividadTerciarizada("Karaoke night", "Diviértete cantando tus canciones favoritas en el bar del hotel", "14/06/2023", 50, 18, 5, "Alonso & Umpierrez", false, "19/06/2023");

            AltaActividadTerciarizada("Sesión de masajes", "Relájate con una sesión de masajes en el spa del hotel", "25/07/2023", 10, 18, 50, "Electric Blue", true, "30/07/2023");
            AltaActividadTerciarizada("Torneo de ping pong", "Demuestra tus habilidades en este torneo de ping pong", "10/08/2023", 16, 12, 25, "Electric Blue", true, "15/08/2023");
            AltaActividadTerciarizada("Safari fotográfico", "Explora la fauna y flora de la región en un safari fotográfico", "20/09/2023", 12, 16, 80, "Electric Blue", true, "25/09/2023");





            // Precarga de Huesped

            DateTime fechaNac = new DateTime();
            string fecha = "17/03/1980";
            DateTime.TryParse(fecha, out fechaNac);

            AltaHuesped("igdiaz@hotmail.com", "brunocapelli", "Ignacio", "Diaz", TipoDocumento.CI, "52722947", fechaNac, "B02", 2);
            AltaHuesped("jherrera@internet.com", "jherrera", "Jose", "Herrera", TipoDocumento.CI, "19122605", fechaNac, "B03", 3);
            AltaHuesped("jlopez@internet.com", "jlopez1234", "Jorge", "Lopez", TipoDocumento.PASAPORTE, "512260", fechaNac, "B04", 1);

            // Precarga Agendas

            
            DateTime.TryParse("25/02/2023", out DateTime fechaAge);
            AltaAgenda("52722947", TipoDocumento.CI, "Fiesta de Disfraces", fechaAge);
                        
            DateTime.TryParse("10/08/2023", out DateTime fechaAge2);
            AltaAgenda("52722947", TipoDocumento.CI, "Torneo de ping pong", fechaAge2);
                        
            DateTime.TryParse("01/11/2023", out DateTime FechaAgeP);
            AltaAgenda("52722947", TipoDocumento.CI, "Yoga", FechaAgeP);
                        
            DateTime.TryParse("25/02/2023", out DateTime fechaAgendaP2);
            AltaAgenda("19122605", TipoDocumento.CI, "Fiesta de Disfraces", fechaAgendaP2);

            DateTime.TryParse("20/09/2023", out DateTime fechaX);
            AltaAgenda("19122605", TipoDocumento.CI, "Tour en bicicleta", fechaX );


            // Precarga de Operador


            DateTime.TryParse("17/05/2020", out DateTime fechaAlta);
            UsuarioOperador userOperador1 = new UsuarioOperador("nicoherrera@hostel.com", "nicololo","Nico", "Herrera", fechaAlta);
            _usuarios.Add(userOperador1);


        }

        #endregion
        #region Constructor
    
        private AdminHostel()
        {
            precargaDatos();

        }







        #endregion
    }
}
