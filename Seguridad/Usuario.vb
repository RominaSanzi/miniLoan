Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.IO
Imports System.Configuration
Imports System.Collections.Generic
Imports Microsoft.VisualBasic.CompilerServices
Imports di.financiera.seguridad

Public Class Usuario

    Inherits Entidad

#Region "Contanstes"
    Public Const JUAN As Integer = 1
#End Region

#Region "Variables"
    Private iId As Long
    Private iNombre As String
    Private iLogin As String
    Private iPassword As String
    Private iPedirCambioPassword As Boolean
    Private iPerfiles As List(Of Perfil)
    Private iNivel As Nivel
    Private iEstado As Estado
    Private iEstadoUsuario As EstadoUsuario
    Private iPaginasAutorizadas() As String
    Private iRolesAutorizados() As String
    Private iMail As String
    Private iAdministrativo As Boolean?
    Private iPuntoVentaDgiDiferido As PuntoVentaDgi
    Private iPuntoVentaDgiInmediatoPunitorios As PuntoVentaDgi
    Private iPuntoVentaDgiInmediatoInteresesYGastos As PuntoVentaDgi
    Private iPuntoVentaDgiInmediatoNotaCredito As PuntoVentaDgi
    Private iUsuarioComercio As Boolean?
    Private iUsuarioFront As Boolean?
    Private iIdComercioDefault As Long
    Private iIdComercioDefaultPromocion As Long
    Private iIdEntidadCuentaCorrienteComercioDefault As Long
    Private iUsuarioAutorizacion As Usuario
    Private iIntentosIngresoPassword As Integer

    '** Validacion de login de usuario **
    Private iLunesHoraLoguinDesde As Date
    Private iLunesHoraLoguinHasta As Date
    Private iMartesHoraLoguinDesde As Date
    Private iMartesHoraLoguinHasta As Date
    Private iMiercolesHoraLoguinDesde As Date
    Private iMiercolesHoraLoguinHasta As Date
    Private iJuevesHoraLoguinDesde As Date
    Private iJuevesHoraLoguinHasta As Date
    Private iViernesHoraLoguinDesde As Date
    Private iViernesHoraLoguinHasta As Date
    Private iSabadoHoraLoguinDesde As Date
    Private iSabadoHoraLoguinHasta As Date
    Private iDomingoHoraLoguinDesde As Date
    Private iDomingoHoraLoguinHasta As Date
    Private iIpAcceso As String
    Private iIpRestringida As String
    Private iMedioAccesoSistema As MedioAccesoSistema
    '***********************************
    Private iFechaUltimoCambioContraseña As Date
    Private iUltimaContraseña1 As String
    Private iUltimaContraseña2 As String
    Private iUltimaContraseña3 As String
    Private iUltimaContraseña4 As String
    Private iCodigoOperadorCentralTelefonica As String
    Private iCodigoColaCentralTelefonica As String
    Private iTipoUsuarioTarea As TipoUsuarioTarea
    Private iTrabajaConAgenda As Boolean?
    Private iSupervisorAgenda As Boolean?
    Private iAutorizaSolicitud As Boolean?
    Private iUsuarioSupervisorAgenda As Usuario
    Private iSucursalPuntos As SucursalPuntos
    Private iSectorAutorizacion As SectorAutorizacion
    Private iIdEstudioDefault As Long
    Private iIdVendedorDefault As Long
    '************************************

    Private iAccesosDirectos As List(Of AccesoDirecto)

    Private iEntidades As List(Of Object)
    Private iComerciosAsociados As List(Of Object)
    Private iControlaRemito As Boolean?
    Private iBandejaWelcome As Boolean?

    Private iIdPoliticaComercial As Long
    Private iIdFiltrosWorkflow As Long

    ' --- USUARIO GENERA CONTRASENIA ---
    Private iGeneraContrasenia As Boolean
    Private iFechaVencimientoGeneraContrasenia As Date
    Private iTokenGeneraContrasenia As String
    Private iEstadoTokenGeneraContrasenia As Estado

    Private iPaisVisualizacion As Pais

    Private iTelefonoCelularCodigoArea As String
    Private iTelefonoCelularCaracteristica As String
    Private iTelefonoCelularNumero As String
    Private iDobleFactor As Boolean
    Private iToken As String
    Private iFechaCaducidadToken As Date

    'Token Firebase Aplicacion
    Private iTokensFirebaseUsuario As List(Of Object)

    ' --- DASHBOARD ---
    Private iDashboard As Dashboard

    Private iFotoPerfil As String


    Private iConexion As accesoDatos
#End Region

#Region "Atributos"
    Public Property idComercioDefaultPromocion() As Long
        Get
            Return iIdComercioDefaultPromocion
        End Get
        Set(ByVal value As Long)
            iIdComercioDefaultPromocion = value
        End Set
    End Property
    Public Property controlaRemito() As Boolean?
        Get
            Return iControlaRemito
        End Get
        Set(ByVal Value As Boolean?)
            iControlaRemito = Value
        End Set
    End Property
    Public Property bandejaWelcome() As Boolean?
        Get
            Return iBandejaWelcome
        End Get
        Set(ByVal Value As Boolean?)
            iBandejaWelcome = Value
        End Set
    End Property
    Public Property id() As Long
        Get
            Return iId
        End Get
        Set(ByVal Value As Long)
            iId = Value
        End Set
    End Property
    Public Property nombre() As String
        Get
            Return iNombre
        End Get
        Set(ByVal Value As String)
            iNombre = Value
        End Set
    End Property
    Public Property nivel() As Nivel
        Get
            Return iNivel
        End Get
        Set(ByVal Value As Nivel)
            iNivel = Value
        End Set
    End Property
    Public Property pedirCambioPassword() As Boolean
        Get
            Return iPedirCambioPassword
        End Get
        Set(ByVal Value As Boolean)
            iPedirCambioPassword = Value
        End Set
    End Property
    Public Property estado() As Estado
        Get
            Return iEstado
        End Get
        Set(ByVal Value As Estado)
            iEstado = Value
        End Set
    End Property
    Public Property estadoUsuario() As EstadoUsuario
        Get
            Return iEstadoUsuario
        End Get
        Set(ByVal Value As EstadoUsuario)
            iEstadoUsuario = Value
        End Set
    End Property
    Public Property perfiles As List(Of Perfil)
        Get
            Return iPerfiles
        End Get
        Set(value As List(Of Perfil))
            iPerfiles = value
        End Set
    End Property
    Public Property login() As String
        Get
            Return iLogin
        End Get
        Set(ByVal Value As String)
            iLogin = Value
        End Set
    End Property
    Public Property password() As String
        Get
            Return iPassword
        End Get
        Set(ByVal Value As String)
            iPassword = Value
        End Set
    End Property
    Public Property paginasAutorizadas() As String()
        Get
            Return iPaginasAutorizadas
        End Get
        Set(ByVal Value As String())
            iPaginasAutorizadas = Value
        End Set
    End Property
    Public Property rolesAutorizados() As String()
        Get
            Return iRolesAutorizados
        End Get
        Set(ByVal Value As String())
            iRolesAutorizados = Value
        End Set
    End Property
    Public Property mail() As String
        Get
            Return iMail
        End Get
        Set(ByVal Value As String)
            iMail = Value
        End Set
    End Property
    Public Property administrativo() As Boolean?
        Get
            Return iAdministrativo
        End Get
        Set(ByVal Value As Boolean?)
            iAdministrativo = Value
        End Set
    End Property
    Public Property puntoVentaDgiDiferido() As PuntoVentaDgi
        Get
            Return iPuntoVentaDgiDiferido
        End Get
        Set(ByVal Value As PuntoVentaDgi)
            iPuntoVentaDgiDiferido = Value
        End Set
    End Property
    Public Property puntoVentaDgiInmediatoPunitorios() As PuntoVentaDgi
        Get
            Return iPuntoVentaDgiInmediatoPunitorios
        End Get
        Set(ByVal Value As PuntoVentaDgi)
            iPuntoVentaDgiInmediatoPunitorios = Value
        End Set
    End Property
    Public Property puntoVentaDgiInmediatoInteresesYGastos() As PuntoVentaDgi
        Get
            Return iPuntoVentaDgiInmediatoInteresesYGastos
        End Get
        Set(ByVal Value As PuntoVentaDgi)
            iPuntoVentaDgiInmediatoInteresesYGastos = Value
        End Set
    End Property
    Public Property puntoVentaDgiInmediatoNotaCredito() As PuntoVentaDgi
        Get
            Return iPuntoVentaDgiInmediatoNotaCredito
        End Get
        Set(ByVal Value As PuntoVentaDgi)
            iPuntoVentaDgiInmediatoNotaCredito = Value
        End Set
    End Property
    Public Property usuarioComercio() As Boolean?
        Get
            Return iUsuarioComercio
        End Get
        Set(ByVal Value As Boolean?)
            iUsuarioComercio = Value
        End Set
    End Property
    Public Property idComercioDefault() As Long
        Get
            Return iIdComercioDefault
        End Get
        Set(ByVal Value As Long)
            iIdComercioDefault = Value
        End Set
    End Property
    Public Property idEntidadCuentaCorrienteComercioDefault() As Long
        Get
            Return iIdEntidadCuentaCorrienteComercioDefault
        End Get
        Set(ByVal Value As Long)
            iIdEntidadCuentaCorrienteComercioDefault = Value
        End Set
    End Property
    Public Property usuarioAutorizacion() As Usuario
        Get
            Return iUsuarioAutorizacion
        End Get
        Set(ByVal Value As Usuario)
            iUsuarioAutorizacion = Value
        End Set
    End Property
    Public Property lunesHoraLoguinDesde() As Date
        Get
            Return iLunesHoraLoguinDesde
        End Get
        Set(ByVal Value As Date)
            iLunesHoraLoguinDesde = Value
        End Set
    End Property
    Public Property lunesHoraLoguinHasta() As Date
        Get
            Return iLunesHoraLoguinHasta
        End Get
        Set(ByVal Value As Date)
            iLunesHoraLoguinHasta = Value
        End Set
    End Property
    Public Property martesHoraLoguinDesde() As Date
        Get
            Return iMartesHoraLoguinDesde
        End Get
        Set(ByVal Value As Date)
            iMartesHoraLoguinDesde = Value
        End Set
    End Property
    Public Property martesHoraLoguinHasta() As Date
        Get
            Return iMartesHoraLoguinHasta
        End Get
        Set(ByVal Value As Date)
            iMartesHoraLoguinHasta = Value
        End Set
    End Property
    Public Property miercolesHoraLoguinDesde() As Date
        Get
            Return iMiercolesHoraLoguinDesde
        End Get
        Set(ByVal Value As Date)
            iMiercolesHoraLoguinDesde = Value
        End Set
    End Property
    Public Property miercolesHoraLoguinHasta() As Date
        Get
            Return iMiercolesHoraLoguinHasta
        End Get
        Set(ByVal Value As Date)
            iMiercolesHoraLoguinHasta = Value
        End Set
    End Property
    Public Property juevesHoraLoguinDesde() As Date
        Get
            Return iJuevesHoraLoguinDesde
        End Get
        Set(ByVal Value As Date)
            iJuevesHoraLoguinDesde = Value
        End Set
    End Property
    Public Property juevesHoraLoguinHasta() As Date
        Get
            Return iJuevesHoraLoguinHasta
        End Get
        Set(ByVal Value As Date)
            iJuevesHoraLoguinHasta = Value
        End Set
    End Property
    Public Property viernesHoraLoguinDesde() As Date
        Get
            Return iViernesHoraLoguinDesde
        End Get
        Set(ByVal Value As Date)
            iViernesHoraLoguinDesde = Value
        End Set
    End Property
    Public Property viernesHoraLoguinHasta() As Date
        Get
            Return iViernesHoraLoguinHasta
        End Get
        Set(ByVal Value As Date)
            iViernesHoraLoguinHasta = Value
        End Set
    End Property
    Public Property sabadoHoraLoguinDesde() As Date
        Get
            Return iSabadoHoraLoguinDesde
        End Get
        Set(ByVal Value As Date)
            iSabadoHoraLoguinDesde = Value
        End Set
    End Property
    Public Property sabadoHoraLoguinHasta() As Date
        Get
            Return iSabadoHoraLoguinHasta
        End Get
        Set(ByVal Value As Date)
            iSabadoHoraLoguinHasta = Value
        End Set
    End Property
    Public Property domingoHoraLoguinDesde() As Date
        Get
            Return iDomingoHoraLoguinDesde
        End Get
        Set(ByVal Value As Date)
            iDomingoHoraLoguinDesde = Value
        End Set
    End Property
    Public Property domingoHoraLoguinHasta() As Date
        Get
            Return iDomingoHoraLoguinHasta
        End Get
        Set(ByVal Value As Date)
            iDomingoHoraLoguinHasta = Value
        End Set
    End Property
    Public Property medioAccesoSistema() As MedioAccesoSistema
        Get
            Return iMedioAccesoSistema
        End Get
        Set(ByVal Value As MedioAccesoSistema)
            iMedioAccesoSistema = Value
        End Set
    End Property
    Public Property ipAcceso() As String
        Get
            Return iIpAcceso
        End Get
        Set(ByVal Value As String)
            iIpAcceso = Value
        End Set
    End Property
    Public Property ipRestringida() As String
        Get
            Return iIpRestringida
        End Get
        Set(ByVal Value As String)
            iIpRestringida = Value
        End Set
    End Property
    Public Property tipoUsuarioTarea() As TipoUsuarioTarea
        Get
            Return iTipoUsuarioTarea
        End Get
        Set(ByVal Value As TipoUsuarioTarea)
            iTipoUsuarioTarea = Value
        End Set
    End Property
    Public Property trabajaConAgenda() As Boolean?
        Get
            Return iTrabajaConAgenda
        End Get
        Set(ByVal Value As Boolean?)
            iTrabajaConAgenda = Value
        End Set
    End Property
    Public Property supervisorAgenda() As Boolean?
        Get
            Return iSupervisorAgenda
        End Get
        Set(ByVal Value As Boolean?)
            iSupervisorAgenda = Value
        End Set
    End Property
    Public Property usuarioSupervisorAgenda() As Usuario
        Get
            Return iUsuarioSupervisorAgenda
        End Get
        Set(ByVal Value As Usuario)
            iUsuarioSupervisorAgenda = Value
        End Set
    End Property
    Public Property autorizaSolicitud() As Boolean?
        Get
            Return iAutorizaSolicitud
        End Get
        Set(ByVal Value As Boolean?)
            iAutorizaSolicitud = Value
        End Set
    End Property
    Public Property codigoOperadorCentralTelefonica() As String
        Get
            Return iCodigoOperadorCentralTelefonica
        End Get
        Set(ByVal Value As String)
            iCodigoOperadorCentralTelefonica = Value
        End Set
    End Property
    Public Property codigoColaCentralTelefonica() As String
        Get
            Return iCodigoColaCentralTelefonica
        End Get
        Set(ByVal Value As String)
            iCodigoColaCentralTelefonica = Value
        End Set
    End Property
    Public Property fechaUltimoCambioContraseña() As Date
        Get
            Return iFechaUltimoCambioContraseña
        End Get
        Set(ByVal Value As Date)
            iFechaUltimoCambioContraseña = Value
        End Set
    End Property
    Public Property ultimaContraseña1() As String
        Get
            Return iUltimaContraseña1
        End Get
        Set(ByVal Value As String)
            iUltimaContraseña1 = Value
        End Set
    End Property
    Public Property ultimaContraseña2() As String
        Get
            Return iUltimaContraseña2
        End Get
        Set(ByVal Value As String)
            iUltimaContraseña2 = Value
        End Set
    End Property
    Public Property ultimaContraseña3() As String
        Get
            Return iUltimaContraseña3
        End Get
        Set(ByVal Value As String)
            iUltimaContraseña3 = Value
        End Set
    End Property
    Public Property ultimaContraseña4() As String
        Get
            Return iUltimaContraseña4
        End Get
        Set(ByVal Value As String)
            iUltimaContraseña4 = Value
        End Set
    End Property
    Public Property sucursalPuntos() As SucursalPuntos
        Get
            Return iSucursalPuntos
        End Get
        Set(ByVal Value As SucursalPuntos)
            iSucursalPuntos = Value
        End Set
    End Property
    Public Property sectorAutorizacion() As SectorAutorizacion
        Get
            Return iSectorAutorizacion
        End Get
        Set(ByVal Value As SectorAutorizacion)
            iSectorAutorizacion = Value
        End Set
    End Property
    Public Property idEstudioDefault() As Long
        Get
            Return iIdEstudioDefault
        End Get
        Set(ByVal Value As Long)
            iIdEstudioDefault = Value
        End Set
    End Property
    Public Property entidades() As List(Of Object)
        Get
            Return iEntidades
        End Get
        Set(ByVal Value As List(Of Object))
            iEntidades = Value
        End Set
    End Property
    Public Property intentosIngresoPassword() As Integer
        Get
            Return iIntentosIngresoPassword
        End Get
        Set(ByVal Value As Integer)
            iIntentosIngresoPassword = Value
        End Set
    End Property
    Public Property idVendedorDefault() As Long
        Get
            Return iIdVendedorDefault
        End Get
        Set(ByVal Value As Long)
            iIdVendedorDefault = Value
        End Set
    End Property
    Public Property comerciosAsociados() As List(Of Object)
        Get
            Return iComerciosAsociados
        End Get
        Set(ByVal Value As List(Of Object))
            iComerciosAsociados = Value
        End Set
    End Property
    Public Property accesosDirectos As List(Of AccesoDirecto)
        Get
            Return iAccesosDirectos
        End Get
        Set(value As List(Of AccesoDirecto))
            iAccesosDirectos = value
        End Set
    End Property
    Public Property idPoliticaComercial() As Long
        Get
            Return iIdPoliticaComercial
        End Get
        Set(ByVal Value As Long)
            iIdPoliticaComercial = Value
        End Set
    End Property
    Public Property idFiltrosWorkflow() As Long
        Get
            Return iIdFiltrosWorkflow
        End Get
        Set(ByVal Value As Long)
            iIdFiltrosWorkflow = Value
        End Set
    End Property
    Public Property generaContrasenia() As Boolean
        Get
            Return iGeneraContrasenia
        End Get
        Set(ByVal Value As Boolean)
            iGeneraContrasenia = Value
        End Set
    End Property
    Public Property fechaVencimientoGeneraContrasenia() As Date
        Get
            Return iFechaVencimientoGeneraContrasenia
        End Get
        Set(ByVal Value As Date)
            iFechaVencimientoGeneraContrasenia = Value
        End Set
    End Property
    Public Property tokenGeneraContrasenia() As String
        Get
            Return iTokenGeneraContrasenia
        End Get
        Set(ByVal Value As String)
            iTokenGeneraContrasenia = Value
        End Set
    End Property
    Public Property estadoTokenGeneraContrasenia() As Estado
        Get
            Return iEstadoTokenGeneraContrasenia
        End Get
        Set(ByVal Value As Estado)
            iEstadoTokenGeneraContrasenia = Value
        End Set
    End Property
    Public Property paisVisualizacion() As Pais
        Get
            Return iPaisVisualizacion
        End Get
        Set(ByVal Value As Pais)
            iPaisVisualizacion = Value
        End Set
    End Property


    Public Property dobleFactor As Boolean
        Get
            Return iDobleFactor
        End Get
        Set(value As Boolean)
            iDobleFactor = value
        End Set
    End Property

    Public Property token As String
        Get
            Return iToken
        End Get
        Set(value As String)
            iToken = value
        End Set
    End Property

    Public Property fechaCaducidadToken As Date
        Get
            Return iFechaCaducidadToken
        End Get
        Set(value As Date)
            iFechaCaducidadToken = value
        End Set
    End Property

    Public Property telefonoCelularCodigoArea As String
        Get
            Return iTelefonoCelularCodigoArea
        End Get
        Set(value As String)
            iTelefonoCelularCodigoArea = value
        End Set
    End Property

    Public Property telefonoCelularCaracteristica As String
        Get
            Return iTelefonoCelularCaracteristica
        End Get
        Set(value As String)
            iTelefonoCelularCaracteristica = value
        End Set
    End Property

    Public Property telefonoCelularNumero As String
        Get
            Return iTelefonoCelularNumero
        End Get
        Set(value As String)
            iTelefonoCelularNumero = value
        End Set
    End Property

    Public Property dashboard As Dashboard
        Get
            Return iDashboard
        End Get
        Set(value As Dashboard)
            iDashboard = value
        End Set
    End Property

    Public Property tokensFirebaseUsuario As List(Of Object)
        Get
            Return iTokensFirebaseUsuario
        End Get
        Set(value As List(Of Object))
            iTokensFirebaseUsuario = value
        End Set
    End Property

    Public Property usuarioFront As Boolean?
        Get
            Return iUsuarioFront
        End Get
        Set(value As Boolean?)
            iUsuarioFront = value
        End Set
    End Property

    Public Property fotoPerfil As String
        Get
            Return iFotoPerfil
        End Get
        Set(value As String)
            iFotoPerfil = value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerUsuario(Optional ByVal eSinUsuarioAutorizacion As Boolean = True) As Usuario
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iIdNivel As Integer
        Dim iRol As New Rol
        Dim iAccesoDirecto As New AccesoDirecto

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("login")
            iGeneradorSql.agregarColumna("password")
            iGeneradorSql.agregarColumna("PedirCambioPassword")
            iGeneradorSql.agregarColumna("idNivel")
            iGeneradorSql.agregarColumna("idEstado")
            iGeneradorSql.agregarColumna("idestadoUsuario")
            iGeneradorSql.agregarColumna("mail")
            iGeneradorSql.agregarColumna("administrativo")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiDiferido")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiInmediatoPunitorios")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiInmediatoInteresesYGastos")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiInmediatoNotaCredito")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionRecibo")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionSolicitud")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionChequera")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionMovimiento")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionLiquidacion")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionResumenTarjeta")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionEtiqueta")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionCarta")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionCupon")
            iGeneradorSql.agregarColumna("usuarioComercio")
            iGeneradorSql.agregarColumna("idComercioDefault")
            iGeneradorSql.agregarColumna("idComercioDefaultPromocion")
            iGeneradorSql.agregarColumna("idEntidadCuentaCorrienteComercioDefault")
            iGeneradorSql.agregarColumna("idCaja")
            iGeneradorSql.agregarColumna("idStand")
            iGeneradorSql.agregarColumna("idUsuarioAutorizacion")
            iGeneradorSql.agregarColumna("LunesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("LunesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("MartesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("MartesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("MiercolesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("MiercolesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("JuevesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("JuevesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("ViernesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("ViernesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("SabadoHoraLoguinDesde")
            iGeneradorSql.agregarColumna("SabadoHoraLoguinHasta")
            iGeneradorSql.agregarColumna("DomingoHoraLoguinDesde")
            iGeneradorSql.agregarColumna("DomingoHoraLoguinHasta")
            iGeneradorSql.agregarColumna("idMedioAccesoSistema")
            iGeneradorSql.agregarColumna("ipAcceso")
            iGeneradorSql.agregarColumna("ipRestringida")
            iGeneradorSql.agregarColumna("idTipoUsuarioTarea")
            iGeneradorSql.agregarColumna("trabajaConAgenda")
            iGeneradorSql.agregarColumna("supervisorAgenda")
            iGeneradorSql.agregarColumna("autorizaSolicitud")
            iGeneradorSql.agregarColumna("idUsuarioSupervisorAgenda")
            iGeneradorSql.agregarColumna("codigoOperadorCentralTelefonica")
            iGeneradorSql.agregarColumna("codigoColaCentralTelefonica")
            iGeneradorSql.agregarColumna("fechaUltimoCambioContrasenia")
            iGeneradorSql.agregarColumna("ultimaContrasenia1")
            iGeneradorSql.agregarColumna("ultimaContrasenia2")
            iGeneradorSql.agregarColumna("ultimaContrasenia3")
            iGeneradorSql.agregarColumna("ultimaContrasenia4")
            iGeneradorSql.agregarColumna("idSucursalPuntos")
            iGeneradorSql.agregarColumna("idSectorAutorizacion")
            iGeneradorSql.agregarColumna("idEstudioDefault")
            iGeneradorSql.agregarColumna("IdVendedorDefault")
            iGeneradorSql.agregarColumna("intentosIngresoPassword")
            iGeneradorSql.agregarColumna("controlaRemito")
            iGeneradorSql.agregarColumna("bandejaWelcome")
            iGeneradorSql.agregarColumna("idpoliticacomercial")
            iGeneradorSql.agregarColumna("idFiltrosWorkflow")
            iGeneradorSql.agregarColumna("generaContrasenia")
            iGeneradorSql.agregarColumna("fechaVencimientoGeneraContrasenia")
            iGeneradorSql.agregarColumna("tokenGeneraContrasenia")
            iGeneradorSql.agregarColumna("idEstadoTokenGeneraContrasenia")
            iGeneradorSql.agregarColumna("idPaisVisualizacion")
            iGeneradorSql.agregarColumna("TelefonoCelularCodigoArea")
            iGeneradorSql.agregarColumna("TelefonoCelularCaracteristica")
            iGeneradorSql.agregarColumna("TelefonoCelularNumero")
            iGeneradorSql.agregarColumna("DobleFactor")
            iGeneradorSql.agregarColumna("Token")
            iGeneradorSql.agregarColumna("FechaCaducidadToken")
            iGeneradorSql.agregarColumna("idDashboard")
            iGeneradorSql.agregarColumna("usuarioFront")

            iGeneradorSql.agregarTabla("usuario")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iNombre = iDataReader.Item("nombre").ToString
                iLogin = iDataReader.Item("login").ToString
                iPassword = iDataReader.Item("password").ToString
                iMail = FuncionComun.vacioSiEsNulo(iDataReader.Item("Mail").ToString)
                iPedirCambioPassword = FuncionComun.byteBoolean(iDataReader.Item("PedirCambioPassword").ToString)
                iAdministrativo = FuncionComun.byteBoolean(iDataReader.Item("administrativo").ToString)
                iEstado = IIf(iDataReader.Item("idEstado").ToString = Estado.ALTA, New Alta, New Baja)
                iEstadoUsuario = New EstadoUsuario
                iEstadoUsuario.id = iDataReader.Item("idEstadoUsuario").ToString
                iUsuarioComercio = FuncionComun.byteBoolean(iDataReader.Item("usuarioComercio").ToString)
                iCodigoOperadorCentralTelefonica = iDataReader.Item("codigoOperadorCentralTelefonica").ToString
                iCodigoColaCentralTelefonica = iDataReader.Item("codigoColaCentralTelefonica").ToString
                iFechaUltimoCambioContraseña = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaUltimoCambioContrasenia").ToString)
                iUltimaContraseña1 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia1").ToString)
                iUltimaContraseña2 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia2").ToString)
                iUltimaContraseña3 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia3").ToString)
                iUltimaContraseña4 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia4").ToString)
                iSupervisorAgenda = FuncionComun.byteBoolean(iDataReader.Item("supervisorAgenda").ToString)
                iAutorizaSolicitud = FuncionComun.byteBoolean(iDataReader.Item("AutorizaSolicitud").ToString)

                If Not IsDBNull(iDataReader.Item("idUsuarioSupervisorAgenda")) Then
                    iUsuarioSupervisorAgenda = New Usuario
                    iUsuarioSupervisorAgenda.id = iDataReader.Item("idUsuarioSupervisorAgenda").ToString
                Else
                    iUsuarioSupervisorAgenda = Nothing
                End If
                iIdNivel = iDataReader.Item("idNivel").ToString

                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiDiferido")) Then
                    iPuntoVentaDgiDiferido = New PuntoVentaDgi
                    iPuntoVentaDgiDiferido.id = iDataReader.Item("idPuntoVentaDgiDiferido").ToString
                Else
                    iPuntoVentaDgiDiferido = Nothing
                End If

                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiInmediatoPunitorios")) Then
                    iPuntoVentaDgiInmediatoPunitorios = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoPunitorios.id = iDataReader.Item("idPuntoVentaDgiInmediatoPunitorios").ToString
                Else
                    iPuntoVentaDgiInmediatoPunitorios = Nothing
                End If

                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiInmediatoInteresesYGastos")) Then
                    iPuntoVentaDgiInmediatoInteresesYGastos = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoInteresesYGastos.id = iDataReader.Item("idPuntoVentaDgiInmediatoInteresesYGastos").ToString
                Else
                    iPuntoVentaDgiInmediatoInteresesYGastos = Nothing
                End If
                If Not IsDBNull(iDataReader.Item("idpuntoVentaDgiInmediatoNotaCredito")) Then
                    iPuntoVentaDgiInmediatoNotaCredito = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoNotaCredito.id = iDataReader.Item("idPuntoVentaDgiInmediatoNotaCredito").ToString
                Else
                    iPuntoVentaDgiInmediatoNotaCredito = Nothing
                End If
                iIdComercioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idComercioDefault").ToString)
                iIdComercioDefaultPromocion = FuncionComun.ceroSiEsVacio(iDataReader.Item("idComercioDefaultPromocion").ToString)
                iIdEntidadCuentaCorrienteComercioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idEntidadCuentaCorrienteComercioDefault").ToString)
                iIdVendedorDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("IdVendedorDefault").ToString)
                If Not IsDBNull(iDataReader.Item("idusuarioAutorizacion")) Then
                    iUsuarioAutorizacion = New Usuario
                    iUsuarioAutorizacion.id = iDataReader.Item("idusuarioAutorizacion").ToString
                End If
                If Not IsDBNull(iDataReader.Item("idSectorAutorizacion")) Then
                    iSectorAutorizacion = New SectorAutorizacion
                    iSectorAutorizacion.id = iDataReader.Item("idSectorAutorizacion").ToString
                End If

                iLunesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("LunesHoraLoguinDesde").ToString)
                iLunesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("LunesHoraLoguinHasta").ToString)
                iMartesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("MartesHoraLoguinDesde").ToString)
                iMartesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("MartesHoraLoguinHasta").ToString)
                iMiercolesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("MiercolesHoraLoguinDesde").ToString)
                iMiercolesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("MiercolesHoraLoguinHasta").ToString)
                iJuevesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("JuevesHoraLoguinDesde").ToString)
                iJuevesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("JuevesHoraLoguinHasta").ToString)
                iViernesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("ViernesHoraLoguinDesde").ToString)
                iViernesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("ViernesHoraLoguinHasta").ToString)
                iSabadoHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("SabadoHoraLoguinDesde").ToString)
                iSabadoHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("SabadoHoraLoguinHasta").ToString)
                iDomingoHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("DomingoHoraLoguinDesde").ToString)
                iDomingoHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("DomingoHoraLoguinHasta").ToString)
                iIpAcceso = iDataReader.Item("ipAcceso").ToString
                iIpRestringida = iDataReader.Item("ipRestringida").ToString
                iMedioAccesoSistema = New MedioAccesoSistema
                iMedioAccesoSistema.id = iDataReader.Item("idMedioAccesoSistema").ToString

                If Not IsDBNull(iDataReader.Item("idTipoUsuarioTarea")) Then
                    iTipoUsuarioTarea = New TipoUsuarioTarea
                    iTipoUsuarioTarea.id = iDataReader.Item("idTipoUsuarioTarea").ToString
                End If

                iTrabajaConAgenda = FuncionComun.byteBoolean(iDataReader.Item("trabajaConAgenda").ToString)

                If Not IsDBNull(iDataReader.Item("idSucursalPuntos")) Then
                    iSucursalPuntos = New SucursalPuntos
                    iSucursalPuntos.id = iDataReader.Item("idSucursalPuntos").ToString
                End If
                iIdEstudioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idEstudioDefault").ToString)
                iIntentosIngresoPassword = FuncionComun.ceroSiEsVacio(iDataReader.Item("intentosIngresoPassword").ToString)
                iControlaRemito = FuncionComun.byteBoolean(iDataReader.Item("ControlaRemito").ToString)
                iBandejaWelcome = FuncionComun.byteBoolean(iDataReader.Item("bandejaWelcome").ToString)
                iIdPoliticaComercial = FuncionComun.nothingSiEsNulo(iDataReader.Item("idpoliticacomercial"))
                iIdFiltrosWorkflow = FuncionComun.nothingSiEsNulo(iDataReader.Item("idFiltrosWorkflow"))
                iGeneraContrasenia = FuncionComun.byteBoolean(iDataReader.Item("generaContrasenia").ToString)
                iFechaVencimientoGeneraContrasenia = FuncionComun.nothingSiEsVacio(iDataReader.Item("fechaVencimientoGeneraContrasenia").ToString)
                iTokenGeneraContrasenia = FuncionComun.vacioSiEsNulo(iDataReader.Item("tokenGeneraContrasenia").ToString)
                If IsDBNull(iDataReader.Item("idEstadoTokenGeneraContrasenia")) Then
                    iEstadoTokenGeneraContrasenia = Nothing
                Else
                    iEstadoTokenGeneraContrasenia = IIf(iDataReader.Item("idEstadoTokenGeneraContrasenia").ToString = Estado.ALTA, New Alta, New Baja)
                End If

                iPaisVisualizacion = New Pais
                iPaisVisualizacion.id = iDataReader.Item("idPaisVisualizacion").ToString

                iTelefonoCelularCodigoArea = iDataReader.Item("TelefonoCelularCodigoArea").ToString
                iTelefonoCelularCaracteristica = iDataReader.Item("TelefonoCelularCaracteristica").ToString
                iTelefonoCelularNumero = iDataReader.Item("TelefonoCelularNumero").ToString

                iDobleFactor = FuncionComun.byteBoolean(iDataReader.Item("DobleFactor").ToString)
                iToken = iDataReader.Item("Token").ToString
                iFechaCaducidadToken = FuncionComun.nothingSiEsVacio(iDataReader.Item("FechaCaducidadToken").ToString)

                If Not IsDBNull(iDataReader.Item("idDashboard")) Then
                    iDashboard = New Dashboard
                    iDashboard.id = iDataReader.Item("idDashboard").ToString
                End If
                iUsuarioFront = FuncionComun.byteBoolean(iDataReader.Item("usuarioFront").ToString)

                iDataReader.Close()

                iPaisVisualizacion.accesoDatos = iConexion
                iPaisVisualizacion = iPaisVisualizacion.obtenerPais
                iPaisVisualizacion.accesoDatos = Nothing

                iMedioAccesoSistema.accesoDatos = iConexion
                iMedioAccesoSistema = iMedioAccesoSistema.obtenerMedioAccesoSistema
                iMedioAccesoSistema.accesoDatos = Nothing

                iEstadoUsuario.accesoDatos = iConexion
                iEstadoUsuario = iEstadoUsuario.obtenerEstadoUsuario
                iEstadoUsuario.accesoDatos = Nothing

                If Not IsNothing(iPuntoVentaDgiDiferido) Then
                    iPuntoVentaDgiDiferido.accesoDatos = iConexion
                    iPuntoVentaDgiDiferido = iPuntoVentaDgiDiferido.obtenerNivel()
                    iPuntoVentaDgiDiferido.accesoDatos = Nothing
                End If
                If Not IsNothing(iPuntoVentaDgiInmediatoPunitorios) Then
                    iPuntoVentaDgiInmediatoPunitorios.accesoDatos = iConexion
                    iPuntoVentaDgiInmediatoPunitorios = iPuntoVentaDgiInmediatoPunitorios.obtenerNivel()
                    iPuntoVentaDgiInmediatoPunitorios.accesoDatos = Nothing
                End If
                If Not IsNothing(iPuntoVentaDgiInmediatoInteresesYGastos) Then
                    iPuntoVentaDgiInmediatoInteresesYGastos.accesoDatos = iConexion
                    iPuntoVentaDgiInmediatoInteresesYGastos = iPuntoVentaDgiInmediatoInteresesYGastos.obtenerNivel()
                    iPuntoVentaDgiInmediatoInteresesYGastos.accesoDatos = Nothing
                End If
                If Not IsNothing(iPuntoVentaDgiInmediatoNotaCredito) Then
                    iPuntoVentaDgiInmediatoNotaCredito.accesoDatos = iConexion
                    iPuntoVentaDgiInmediatoNotaCredito = iPuntoVentaDgiInmediatoNotaCredito.obtenerNivel()
                    iPuntoVentaDgiInmediatoNotaCredito.accesoDatos = Nothing
                End If
                If Not IsNothing(iUsuarioAutorizacion) AndAlso eSinUsuarioAutorizacion Then
                    iUsuarioAutorizacion.accesoDatos = iConexion
                    iUsuarioAutorizacion = iUsuarioAutorizacion.obtenerUsuarioSoloIds(True)
                    iUsuarioAutorizacion.accesoDatos = Nothing
                End If

                If Not IsNothing(iTipoUsuarioTarea) Then
                    iTipoUsuarioTarea.accesoDatos = iConexion
                    iTipoUsuarioTarea = iTipoUsuarioTarea.obtenerTipoUsuarioTarea
                    iTipoUsuarioTarea.accesoDatos = Nothing
                End If
                If Not IsNothing(iUsuarioSupervisorAgenda) Then
                    iUsuarioSupervisorAgenda.accesoDatos = iConexion
                    iUsuarioSupervisorAgenda = iUsuarioSupervisorAgenda.obtenerUsuarioSoloIds(True)
                    iUsuarioSupervisorAgenda.accesoDatos = Nothing
                End If
                If Not IsNothing(iSectorAutorizacion) Then
                    iSectorAutorizacion.accesoDatos = iConexion
                    iSectorAutorizacion = iSectorAutorizacion.obtenerSectorAutorizacion
                    iSectorAutorizacion.accesoDatos = Nothing
                End If

                If Not IsNothing(iDashboard) Then
                    iDashboard.accesoDatos = iConexion
                    iDashboard = iDashboard.obtenerDashboard
                    iDashboard.accesoDatos = Nothing
                End If

                iNivel = Nivel.obtenerNivelShared(iIdNivel, iConexion)

                obtenerPaginasAutorizadas()

                iPerfiles = obtenerUsuarioPerfil()

                iRol.accesoDatos = iConexion
                iRol.usuario = Me
                iRolesAutorizados = iRol.obtenerRolesAutorizados()
                iRol.accesoDatos = Nothing

                iAccesoDirecto.accesoDatos = iConexion
                iAccesosDirectos = iAccesoDirecto.obtenerAccesosDirectosPorUsuario(Me)
                iAccesoDirecto.accesoDatos = Nothing

                If Not IsNothing(iSucursalPuntos) Then
                    iSucursalPuntos.accesoDatos = iConexion
                    iSucursalPuntos = iSucursalPuntos.obtenerSucursalPuntos
                    iSucursalPuntos.accesoDatos = Nothing
                End If

                Return Me
            Else
                Throw New UsuarioNoEncontradoException
            End If

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                If Not IsNothing(iConexion) Then
                    iConexion.cerrar()
                    iConexion = Nothing
                End If
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iRol = Nothing
            iAccesoDirecto = Nothing
        End Try
    End Function

    Public Overridable Function obtenerUsuarioFacebook(ByVal eIdFacebook As Long) As Usuario
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iIdNivel As Integer
        Dim iRol As New Rol
        Dim iAccesoDirecto As New AccesoDirecto

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("u.id")
            iGeneradorSql.agregarColumna("u.nombre")
            iGeneradorSql.agregarColumna("u.login")
            iGeneradorSql.agregarColumna("u.password")
            iGeneradorSql.agregarColumna("u.PedirCambioPassword")
            iGeneradorSql.agregarColumna("u.idNivel")
            iGeneradorSql.agregarColumna("u.idEstado")
            iGeneradorSql.agregarColumna("u.idestadoUsuario")
            iGeneradorSql.agregarColumna("u.mail")
            iGeneradorSql.agregarColumna("u.administrativo")
            iGeneradorSql.agregarColumna("u.idPuntoVentaDgiDiferido")
            iGeneradorSql.agregarColumna("u.idPuntoVentaDgiInmediatoPunitorios")
            iGeneradorSql.agregarColumna("u.idPuntoVentaDgiInmediatoInteresesYGastos")
            iGeneradorSql.agregarColumna("u.idPuntoVentaDgiInmediatoNotaCredito")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionRecibo")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionSolicitud")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionChequera")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionMovimiento")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionLiquidacion")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionResumenTarjeta")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionEtiqueta")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionCarta")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionCupon")
            iGeneradorSql.agregarColumna("u.usuarioComercio")
            iGeneradorSql.agregarColumna("u.idComercioDefault")
            iGeneradorSql.agregarColumna("u.idComercioDefaultPromocion")
            iGeneradorSql.agregarColumna("u.idEntidadCuentaCorrienteComercioDefault")
            iGeneradorSql.agregarColumna("u.idCaja")
            iGeneradorSql.agregarColumna("u.idStand")
            iGeneradorSql.agregarColumna("u.idUsuarioAutorizacion")
            iGeneradorSql.agregarColumna("u.LunesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.LunesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.MartesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.MartesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.MiercolesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.MiercolesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.JuevesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.JuevesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.ViernesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.ViernesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.SabadoHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.SabadoHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.DomingoHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.DomingoHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.idMedioAccesoSistema")
            iGeneradorSql.agregarColumna("u.ipAcceso")
            iGeneradorSql.agregarColumna("u.ipRestringida")
            iGeneradorSql.agregarColumna("u.idTipoUsuarioTarea")
            iGeneradorSql.agregarColumna("u.trabajaConAgenda")
            iGeneradorSql.agregarColumna("u.supervisorAgenda")
            iGeneradorSql.agregarColumna("u.autorizaSolicitud")
            iGeneradorSql.agregarColumna("u.idUsuarioSupervisorAgenda")
            iGeneradorSql.agregarColumna("u.codigoOperadorCentralTelefonica")
            iGeneradorSql.agregarColumna("u.codigoColaCentralTelefonica")
            iGeneradorSql.agregarColumna("u.fechaUltimoCambioContrasenia")
            iGeneradorSql.agregarColumna("u.ultimaContrasenia1")
            iGeneradorSql.agregarColumna("u.ultimaContrasenia2")
            iGeneradorSql.agregarColumna("u.ultimaContrasenia3")
            iGeneradorSql.agregarColumna("u.ultimaContrasenia4")
            iGeneradorSql.agregarColumna("u.idSucursalPuntos")
            iGeneradorSql.agregarColumna("u.idSectorAutorizacion")
            iGeneradorSql.agregarColumna("u.idEstudioDefault")
            iGeneradorSql.agregarColumna("u.IdVendedorDefault")
            iGeneradorSql.agregarColumna("u.intentosIngresoPassword")
            iGeneradorSql.agregarColumna("u.controlaRemito")
            iGeneradorSql.agregarColumna("u.bandejaWelcome")
            iGeneradorSql.agregarColumna("u.idpoliticacomercial")
            iGeneradorSql.agregarColumna("u.idPaisVisualizacion")
            iGeneradorSql.agregarColumna("u.TelefonoCelularCodigoArea")
            iGeneradorSql.agregarColumna("u.TelefonoCelularCaracteristica")
            iGeneradorSql.agregarColumna("u.TelefonoCelularNumero")
            iGeneradorSql.agregarColumna("u.DobleFactor")
            iGeneradorSql.agregarColumna("u.Token")
            iGeneradorSql.agregarColumna("u.FechaCaducidadToken")
            iGeneradorSql.agregarColumna("u.idDashboard")
            iGeneradorSql.agregarColumna("u.usuarioFront")

            iGeneradorSql.agregarTabla("usuario u")
            iGeneradorSql.agregarTabla("UsuarioAutogestion ua")

            iGeneradorSql.agregarCondicionWhere("u.id=ua.id")
            iGeneradorSql.agregarCondicionWhere("ua.idFacebook=" & eIdFacebook)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iNombre = iDataReader.Item("nombre").ToString
                iLogin = iDataReader.Item("login").ToString
                iPassword = iDataReader.Item("password").ToString
                iMail = FuncionComun.vacioSiEsNulo(iDataReader.Item("Mail").ToString)
                iPedirCambioPassword = FuncionComun.byteBoolean(iDataReader.Item("PedirCambioPassword").ToString)
                iAdministrativo = FuncionComun.byteBoolean(iDataReader.Item("administrativo").ToString)
                iEstado = IIf(iDataReader.Item("idEstado").ToString = Estado.ALTA, New Alta, New Baja)
                iEstadoUsuario = New EstadoUsuario
                iEstadoUsuario.id = iDataReader.Item("idEstadoUsuario").ToString
                iUsuarioComercio = FuncionComun.byteBoolean(iDataReader.Item("usuarioComercio").ToString)
                iCodigoOperadorCentralTelefonica = iDataReader.Item("codigoOperadorCentralTelefonica").ToString
                iCodigoColaCentralTelefonica = iDataReader.Item("codigoColaCentralTelefonica").ToString
                iFechaUltimoCambioContraseña = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaUltimoCambioContrasenia").ToString)
                iUltimaContraseña1 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia1").ToString)
                iUltimaContraseña2 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia2").ToString)
                iUltimaContraseña3 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia3").ToString)
                iUltimaContraseña4 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia4").ToString)
                iSupervisorAgenda = FuncionComun.byteBoolean(iDataReader.Item("supervisorAgenda").ToString)
                iAutorizaSolicitud = FuncionComun.byteBoolean(iDataReader.Item("AutorizaSolicitud").ToString)

                If Not IsDBNull(iDataReader.Item("idUsuarioSupervisorAgenda")) Then
                    iUsuarioSupervisorAgenda = New Usuario
                    iUsuarioSupervisorAgenda.id = iDataReader.Item("idUsuarioSupervisorAgenda").ToString
                Else
                    iUsuarioSupervisorAgenda = Nothing
                End If
                iIdNivel = iDataReader.Item("idNivel").ToString

                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiDiferido")) Then
                    iPuntoVentaDgiDiferido = New PuntoVentaDgi
                    iPuntoVentaDgiDiferido.id = iDataReader.Item("idPuntoVentaDgiDiferido").ToString
                Else
                    iPuntoVentaDgiDiferido = Nothing
                End If

                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiInmediatoPunitorios")) Then
                    iPuntoVentaDgiInmediatoPunitorios = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoPunitorios.id = iDataReader.Item("idPuntoVentaDgiInmediatoPunitorios").ToString
                Else
                    iPuntoVentaDgiInmediatoPunitorios = Nothing
                End If

                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiInmediatoInteresesYGastos")) Then
                    iPuntoVentaDgiInmediatoInteresesYGastos = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoInteresesYGastos.id = iDataReader.Item("idPuntoVentaDgiInmediatoInteresesYGastos").ToString
                Else
                    iPuntoVentaDgiInmediatoInteresesYGastos = Nothing
                End If
                If Not IsDBNull(iDataReader.Item("idpuntoVentaDgiInmediatoNotaCredito")) Then
                    iPuntoVentaDgiInmediatoNotaCredito = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoNotaCredito.id = iDataReader.Item("idPuntoVentaDgiInmediatoNotaCredito").ToString
                Else
                    iPuntoVentaDgiInmediatoNotaCredito = Nothing
                End If
                iIdComercioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idComercioDefault").ToString)
                iIdComercioDefaultPromocion = FuncionComun.ceroSiEsVacio(iDataReader.Item("idComercioDefaultPromocion").ToString)
                iIdEntidadCuentaCorrienteComercioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idEntidadCuentaCorrienteComercioDefault").ToString)
                iIdVendedorDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("IdVendedorDefault").ToString)
                If Not IsDBNull(iDataReader.Item("idusuarioAutorizacion")) Then
                    iUsuarioAutorizacion = New Usuario
                    iUsuarioAutorizacion.id = iDataReader.Item("idusuarioAutorizacion").ToString
                End If
                If Not IsDBNull(iDataReader.Item("idSectorAutorizacion")) Then
                    iSectorAutorizacion = New SectorAutorizacion
                    iSectorAutorizacion.id = iDataReader.Item("idSectorAutorizacion").ToString
                End If

                iLunesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("LunesHoraLoguinDesde").ToString)
                iLunesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("LunesHoraLoguinHasta").ToString)
                iMartesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("MartesHoraLoguinDesde").ToString)
                iMartesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("MartesHoraLoguinHasta").ToString)
                iMiercolesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("MiercolesHoraLoguinDesde").ToString)
                iMiercolesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("MiercolesHoraLoguinHasta").ToString)
                iJuevesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("JuevesHoraLoguinDesde").ToString)
                iJuevesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("JuevesHoraLoguinHasta").ToString)
                iViernesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("ViernesHoraLoguinDesde").ToString)
                iViernesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("ViernesHoraLoguinHasta").ToString)
                iSabadoHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("SabadoHoraLoguinDesde").ToString)
                iSabadoHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("SabadoHoraLoguinHasta").ToString)
                iDomingoHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("DomingoHoraLoguinDesde").ToString)
                iDomingoHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("DomingoHoraLoguinHasta").ToString)
                iIpAcceso = iDataReader.Item("ipAcceso").ToString
                iIpRestringida = iDataReader.Item("ipRestringida").ToString
                iMedioAccesoSistema = New MedioAccesoSistema
                iMedioAccesoSistema.id = iDataReader.Item("idMedioAccesoSistema").ToString

                If Not IsDBNull(iDataReader.Item("idTipoUsuarioTarea")) Then
                    iTipoUsuarioTarea = New TipoUsuarioTarea
                    iTipoUsuarioTarea.id = iDataReader.Item("idTipoUsuarioTarea").ToString
                End If

                iTrabajaConAgenda = FuncionComun.byteBoolean(iDataReader.Item("trabajaConAgenda").ToString)

                If Not IsDBNull(iDataReader.Item("idSucursalPuntos")) Then
                    iSucursalPuntos = New SucursalPuntos
                    iSucursalPuntos.id = iDataReader.Item("idSucursalPuntos").ToString
                End If
                iIdEstudioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idEstudioDefault").ToString)
                iIntentosIngresoPassword = FuncionComun.ceroSiEsVacio(iDataReader.Item("intentosIngresoPassword").ToString)
                iControlaRemito = FuncionComun.byteBoolean(iDataReader.Item("ControlaRemito").ToString)
                iBandejaWelcome = FuncionComun.byteBoolean(iDataReader.Item("bandejaWelcome").ToString)
                iIdPoliticaComercial = FuncionComun.nothingSiEsNulo(iDataReader.Item("idpoliticacomercial"))

                iPaisVisualizacion = New Pais
                iPaisVisualizacion.id = iDataReader.Item("idPaisVisualizacion").ToString
                iTelefonoCelularCodigoArea = iDataReader.Item("TelefonoCelularCodigoArea").ToString
                iTelefonoCelularCaracteristica = iDataReader.Item("TelefonoCelularCaracteristica").ToString
                iTelefonoCelularNumero = iDataReader.Item("TelefonoCelularNumero").ToString
                iDobleFactor = FuncionComun.byteBoolean(iDataReader.Item("DobleFactor").ToString)
                iToken = iDataReader.Item("Token").ToString
                iFechaCaducidadToken = FuncionComun.nothingSiEsVacio(iDataReader.Item("FechaCaducidadToken").ToString)

                If Not IsDBNull(iDataReader.Item("idDashboard")) Then
                    iDashboard = New Dashboard
                    iDashboard.id = iDataReader.Item("idDashboard").ToString
                End If
                iUsuarioFront = FuncionComun.byteBoolean(iDataReader.Item("usuarioFront").ToString)

                iDataReader.Close()

                Return Me

            Else
                Throw New UsuarioNoEncontradoException
            End If

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iRol = Nothing
            iAccesoDirecto = Nothing
        End Try
    End Function

    Public Overridable Function obtenerUsuarioGoogle(ByVal eIdGoogle As String) As Usuario
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iIdNivel As Integer
        Dim iRol As New Rol
        Dim iAccesoDirecto As New AccesoDirecto

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("u.id")
            iGeneradorSql.agregarColumna("u.nombre")
            iGeneradorSql.agregarColumna("u.login")
            iGeneradorSql.agregarColumna("u.password")
            iGeneradorSql.agregarColumna("u.PedirCambioPassword")
            iGeneradorSql.agregarColumna("u.idNivel")
            iGeneradorSql.agregarColumna("u.idEstado")
            iGeneradorSql.agregarColumna("u.idestadoUsuario")
            iGeneradorSql.agregarColumna("u.mail")
            iGeneradorSql.agregarColumna("u.administrativo")
            iGeneradorSql.agregarColumna("u.idPuntoVentaDgiDiferido")
            iGeneradorSql.agregarColumna("u.idPuntoVentaDgiInmediatoPunitorios")
            iGeneradorSql.agregarColumna("u.idPuntoVentaDgiInmediatoInteresesYGastos")
            iGeneradorSql.agregarColumna("u.idPuntoVentaDgiInmediatoNotaCredito")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionRecibo")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionSolicitud")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionChequera")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionMovimiento")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionLiquidacion")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionResumenTarjeta")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionEtiqueta")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionCarta")
            iGeneradorSql.agregarColumna("u.idTipoServicioImpresionCupon")
            iGeneradorSql.agregarColumna("u.usuarioComercio")
            iGeneradorSql.agregarColumna("u.idComercioDefault")
            iGeneradorSql.agregarColumna("u.idComercioDefaultPromocion")
            iGeneradorSql.agregarColumna("u.idEntidadCuentaCorrienteComercioDefault")
            iGeneradorSql.agregarColumna("u.idCaja")
            iGeneradorSql.agregarColumna("u.idStand")
            iGeneradorSql.agregarColumna("u.idUsuarioAutorizacion")
            iGeneradorSql.agregarColumna("u.LunesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.LunesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.MartesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.MartesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.MiercolesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.MiercolesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.JuevesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.JuevesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.ViernesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.ViernesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.SabadoHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.SabadoHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.DomingoHoraLoguinDesde")
            iGeneradorSql.agregarColumna("u.DomingoHoraLoguinHasta")
            iGeneradorSql.agregarColumna("u.idMedioAccesoSistema")
            iGeneradorSql.agregarColumna("u.ipAcceso")
            iGeneradorSql.agregarColumna("u.ipRestringida")
            iGeneradorSql.agregarColumna("u.idTipoUsuarioTarea")
            iGeneradorSql.agregarColumna("u.trabajaConAgenda")
            iGeneradorSql.agregarColumna("u.supervisorAgenda")
            iGeneradorSql.agregarColumna("u.autorizaSolicitud")
            iGeneradorSql.agregarColumna("u.idUsuarioSupervisorAgenda")
            iGeneradorSql.agregarColumna("u.codigoOperadorCentralTelefonica")
            iGeneradorSql.agregarColumna("u.codigoColaCentralTelefonica")
            iGeneradorSql.agregarColumna("u.fechaUltimoCambioContrasenia")
            iGeneradorSql.agregarColumna("u.ultimaContrasenia1")
            iGeneradorSql.agregarColumna("u.ultimaContrasenia2")
            iGeneradorSql.agregarColumna("u.ultimaContrasenia3")
            iGeneradorSql.agregarColumna("u.ultimaContrasenia4")
            iGeneradorSql.agregarColumna("u.idSucursalPuntos")
            iGeneradorSql.agregarColumna("u.idSectorAutorizacion")
            iGeneradorSql.agregarColumna("u.idEstudioDefault")
            iGeneradorSql.agregarColumna("u.IdVendedorDefault")
            iGeneradorSql.agregarColumna("u.intentosIngresoPassword")
            iGeneradorSql.agregarColumna("u.controlaRemito")
            iGeneradorSql.agregarColumna("u.bandejaWelcome")
            iGeneradorSql.agregarColumna("u.idpoliticacomercial")
            iGeneradorSql.agregarColumna("u.idPaisVisualizacion")
            iGeneradorSql.agregarColumna("u.TelefonoCelularCodigoArea")
            iGeneradorSql.agregarColumna("u.TelefonoCelularCaracteristica")
            iGeneradorSql.agregarColumna("u.TelefonoCelularNumero")
            iGeneradorSql.agregarColumna("u.DobleFactor")
            iGeneradorSql.agregarColumna("u.Token")
            iGeneradorSql.agregarColumna("u.FechaCaducidadToken")
            iGeneradorSql.agregarColumna("u.idDashboard")
            iGeneradorSql.agregarColumna("u.usuarioFront")

            iGeneradorSql.agregarTabla("usuario u")
            iGeneradorSql.agregarTabla("UsuarioAutogestion ua")

            iGeneradorSql.agregarCondicionWhere("u.id=ua.id")
            iGeneradorSql.agregarCondicionWhere("ua.idGoogle=" & eIdGoogle)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iNombre = iDataReader.Item("nombre").ToString
                iLogin = iDataReader.Item("login").ToString
                iPassword = iDataReader.Item("password").ToString
                iMail = FuncionComun.vacioSiEsNulo(iDataReader.Item("Mail").ToString)
                iPedirCambioPassword = FuncionComun.byteBoolean(iDataReader.Item("PedirCambioPassword").ToString)
                iAdministrativo = FuncionComun.byteBoolean(iDataReader.Item("administrativo").ToString)
                iEstado = IIf(iDataReader.Item("idEstado").ToString = Estado.ALTA, New Alta, New Baja)
                iEstadoUsuario = New EstadoUsuario
                iEstadoUsuario.id = iDataReader.Item("idEstadoUsuario").ToString
                iUsuarioComercio = FuncionComun.byteBoolean(iDataReader.Item("usuarioComercio").ToString)
                iCodigoOperadorCentralTelefonica = iDataReader.Item("codigoOperadorCentralTelefonica").ToString
                iCodigoColaCentralTelefonica = iDataReader.Item("codigoColaCentralTelefonica").ToString
                iFechaUltimoCambioContraseña = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaUltimoCambioContrasenia").ToString)
                iUltimaContraseña1 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia1").ToString)
                iUltimaContraseña2 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia2").ToString)
                iUltimaContraseña3 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia3").ToString)
                iUltimaContraseña4 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia4").ToString)
                iSupervisorAgenda = FuncionComun.byteBoolean(iDataReader.Item("supervisorAgenda").ToString)
                iAutorizaSolicitud = FuncionComun.byteBoolean(iDataReader.Item("AutorizaSolicitud").ToString)

                If Not IsDBNull(iDataReader.Item("idUsuarioSupervisorAgenda")) Then
                    iUsuarioSupervisorAgenda = New Usuario
                    iUsuarioSupervisorAgenda.id = iDataReader.Item("idUsuarioSupervisorAgenda").ToString
                Else
                    iUsuarioSupervisorAgenda = Nothing
                End If
                iIdNivel = iDataReader.Item("idNivel").ToString

                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiDiferido")) Then
                    iPuntoVentaDgiDiferido = New PuntoVentaDgi
                    iPuntoVentaDgiDiferido.id = iDataReader.Item("idPuntoVentaDgiDiferido").ToString
                Else
                    iPuntoVentaDgiDiferido = Nothing
                End If

                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiInmediatoPunitorios")) Then
                    iPuntoVentaDgiInmediatoPunitorios = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoPunitorios.id = iDataReader.Item("idPuntoVentaDgiInmediatoPunitorios").ToString
                Else
                    iPuntoVentaDgiInmediatoPunitorios = Nothing
                End If

                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiInmediatoInteresesYGastos")) Then
                    iPuntoVentaDgiInmediatoInteresesYGastos = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoInteresesYGastos.id = iDataReader.Item("idPuntoVentaDgiInmediatoInteresesYGastos").ToString
                Else
                    iPuntoVentaDgiInmediatoInteresesYGastos = Nothing
                End If
                If Not IsDBNull(iDataReader.Item("idpuntoVentaDgiInmediatoNotaCredito")) Then
                    iPuntoVentaDgiInmediatoNotaCredito = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoNotaCredito.id = iDataReader.Item("idPuntoVentaDgiInmediatoNotaCredito").ToString
                Else
                    iPuntoVentaDgiInmediatoNotaCredito = Nothing
                End If
                iIdComercioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idComercioDefault").ToString)
                iIdComercioDefaultPromocion = FuncionComun.ceroSiEsVacio(iDataReader.Item("idComercioDefaultPromocion").ToString)
                iIdEntidadCuentaCorrienteComercioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idEntidadCuentaCorrienteComercioDefault").ToString)
                iIdVendedorDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("IdVendedorDefault").ToString)
                If Not IsDBNull(iDataReader.Item("idusuarioAutorizacion")) Then
                    iUsuarioAutorizacion = New Usuario
                    iUsuarioAutorizacion.id = iDataReader.Item("idusuarioAutorizacion").ToString
                End If
                If Not IsDBNull(iDataReader.Item("idSectorAutorizacion")) Then
                    iSectorAutorizacion = New SectorAutorizacion
                    iSectorAutorizacion.id = iDataReader.Item("idSectorAutorizacion").ToString
                End If

                iLunesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("LunesHoraLoguinDesde").ToString)
                iLunesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("LunesHoraLoguinHasta").ToString)
                iMartesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("MartesHoraLoguinDesde").ToString)
                iMartesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("MartesHoraLoguinHasta").ToString)
                iMiercolesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("MiercolesHoraLoguinDesde").ToString)
                iMiercolesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("MiercolesHoraLoguinHasta").ToString)
                iJuevesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("JuevesHoraLoguinDesde").ToString)
                iJuevesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("JuevesHoraLoguinHasta").ToString)
                iViernesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("ViernesHoraLoguinDesde").ToString)
                iViernesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("ViernesHoraLoguinHasta").ToString)
                iSabadoHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("SabadoHoraLoguinDesde").ToString)
                iSabadoHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("SabadoHoraLoguinHasta").ToString)
                iDomingoHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("DomingoHoraLoguinDesde").ToString)
                iDomingoHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("DomingoHoraLoguinHasta").ToString)
                iIpAcceso = iDataReader.Item("ipAcceso").ToString
                iIpRestringida = iDataReader.Item("ipRestringida").ToString
                iMedioAccesoSistema = New MedioAccesoSistema
                iMedioAccesoSistema.id = iDataReader.Item("idMedioAccesoSistema").ToString

                If Not IsDBNull(iDataReader.Item("idTipoUsuarioTarea")) Then
                    iTipoUsuarioTarea = New TipoUsuarioTarea
                    iTipoUsuarioTarea.id = iDataReader.Item("idTipoUsuarioTarea").ToString
                End If

                iTrabajaConAgenda = FuncionComun.byteBoolean(iDataReader.Item("trabajaConAgenda").ToString)

                If Not IsDBNull(iDataReader.Item("idSucursalPuntos")) Then
                    iSucursalPuntos = New SucursalPuntos
                    iSucursalPuntos.id = iDataReader.Item("idSucursalPuntos").ToString
                End If
                iIdEstudioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idEstudioDefault").ToString)
                iIntentosIngresoPassword = FuncionComun.ceroSiEsVacio(iDataReader.Item("intentosIngresoPassword").ToString)
                iControlaRemito = FuncionComun.byteBoolean(iDataReader.Item("ControlaRemito").ToString)
                iBandejaWelcome = FuncionComun.byteBoolean(iDataReader.Item("bandejaWelcome").ToString)
                iIdPoliticaComercial = FuncionComun.nothingSiEsNulo(iDataReader.Item("idpoliticacomercial"))

                iPaisVisualizacion = New Pais
                iPaisVisualizacion.id = iDataReader.Item("idPaisVisualizacion").ToString
                iTelefonoCelularCodigoArea = iDataReader.Item("TelefonoCelularCodigoArea").ToString
                iTelefonoCelularCaracteristica = iDataReader.Item("TelefonoCelularCaracteristica").ToString
                iTelefonoCelularNumero = iDataReader.Item("TelefonoCelularNumero").ToString

                iDobleFactor = FuncionComun.byteBoolean(iDataReader.Item("DobleFactor").ToString)
                iToken = iDataReader.Item("Token").ToString
                iFechaCaducidadToken = FuncionComun.nothingSiEsVacio(iDataReader.Item("FechaCaducidadToken").ToString)

                If Not IsDBNull(iDataReader.Item("idDashboard")) Then
                    iDashboard = New Dashboard
                    iDashboard.id = iDataReader.Item("idDashboard").ToString
                End If
                iUsuarioFront = FuncionComun.byteBoolean(iDataReader.Item("usuarioFront").ToString)

                iDataReader.Close()

                Return Me

            Else
                Throw New UsuarioNoEncontradoException
            End If

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iRol = Nothing
            iAccesoDirecto = Nothing
        End Try
    End Function
    Public Overridable Function obtenerUsuarioSoloIds(Optional eObtenerNivel As Boolean = False) As Usuario
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iIdNivel As Integer

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("login")
            iGeneradorSql.agregarColumna("password")
            iGeneradorSql.agregarColumna("PedirCambioPassword")
            iGeneradorSql.agregarColumna("idNivel")
            iGeneradorSql.agregarColumna("idEstado")
            iGeneradorSql.agregarColumna("idestadoUsuario")
            iGeneradorSql.agregarColumna("mail")
            iGeneradorSql.agregarColumna("administrativo")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiDiferido")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiInmediatoPunitorios")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiInmediatoInteresesYGastos")
            iGeneradorSql.agregarColumna("idpuntoVentaDgiInmediatoNotaCredito")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionRecibo")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionSolicitud")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionChequera")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionMovimiento")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionLiquidacion")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionResumenTarjeta")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionEtiqueta")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionCarta")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionCupon")
            iGeneradorSql.agregarColumna("usuarioComercio")
            iGeneradorSql.agregarColumna("idComercioDefault")
            iGeneradorSql.agregarColumna("idEntidadCuentaCorrienteComercioDefault")
            iGeneradorSql.agregarColumna("idCaja")
            iGeneradorSql.agregarColumna("idStand")
            iGeneradorSql.agregarColumna("idUsuarioAutorizacion")
            iGeneradorSql.agregarColumna("LunesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("LunesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("MartesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("MartesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("MiercolesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("MiercolesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("JuevesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("JuevesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("ViernesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("ViernesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("SabadoHoraLoguinDesde")
            iGeneradorSql.agregarColumna("SabadoHoraLoguinHasta")
            iGeneradorSql.agregarColumna("DomingoHoraLoguinDesde")
            iGeneradorSql.agregarColumna("DomingoHoraLoguinHasta")
            iGeneradorSql.agregarColumna("idMedioAccesoSistema")
            iGeneradorSql.agregarColumna("ipAcceso")
            iGeneradorSql.agregarColumna("ipRestringida")
            iGeneradorSql.agregarColumna("idTipoUsuarioTarea")
            iGeneradorSql.agregarColumna("trabajaConAgenda")
            iGeneradorSql.agregarColumna("supervisorAgenda")
            iGeneradorSql.agregarColumna("autorizaSolicitud")
            iGeneradorSql.agregarColumna("idUsuarioSupervisorAgenda")
            iGeneradorSql.agregarColumna("codigoOperadorCentralTelefonica")
            iGeneradorSql.agregarColumna("codigoColaCentralTelefonica")
            iGeneradorSql.agregarColumna("fechaUltimoCambioContrasenia")
            iGeneradorSql.agregarColumna("ultimaContrasenia1")
            iGeneradorSql.agregarColumna("ultimaContrasenia2")
            iGeneradorSql.agregarColumna("ultimaContrasenia3")
            iGeneradorSql.agregarColumna("ultimaContrasenia4")
            iGeneradorSql.agregarColumna("idSucursalPuntos")
            iGeneradorSql.agregarColumna("idSectorAutorizacion")
            iGeneradorSql.agregarColumna("idEstudioDefault")
            iGeneradorSql.agregarColumna("intentosIngresoPassword")
            iGeneradorSql.agregarColumna("idVendedorDefault")
            iGeneradorSql.agregarColumna("controlaRemito")
            iGeneradorSql.agregarColumna("bandejaWelcome")
            iGeneradorSql.agregarColumna("idpoliticacomercial")
            iGeneradorSql.agregarColumna("idFiltrosWorkflow")
            iGeneradorSql.agregarColumna("generaContrasenia")
            iGeneradorSql.agregarColumna("fechaVencimientoGeneraContrasenia")
            iGeneradorSql.agregarColumna("tokenGeneraContrasenia")
            iGeneradorSql.agregarColumna("idEstadoTokenGeneraContrasenia")
            iGeneradorSql.agregarColumna("idPaisVisualizacion")
            iGeneradorSql.agregarColumna("TelefonoCelularCodigoArea")
            iGeneradorSql.agregarColumna("TelefonoCelularCaracteristica")
            iGeneradorSql.agregarColumna("TelefonoCelularNumero")
            iGeneradorSql.agregarColumna("DobleFactor")
            iGeneradorSql.agregarColumna("Token")
            iGeneradorSql.agregarColumna("FechaCaducidadToken")
            iGeneradorSql.agregarColumna("idDashboard")
            iGeneradorSql.agregarColumna("usuarioFront")

            iGeneradorSql.agregarTabla("usuario")
            If iId <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If iLogin <> Nothing Then iGeneradorSql.agregarCondicionWhere("login='" & login & "'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then

                If iPassword <> Nothing AndAlso Not (FuncionComun.obtenerCodigoHashMD5(iPassword) = iDataReader.Item("password").ToString OrElse
                        FuncionComun.obtenerCodigoHashMD5(iPassword.ToLower) = iDataReader.Item("password").ToString OrElse
                        FuncionComun.obtenerCodigoHashMD5(iPassword.ToUpper) = iDataReader.Item("password").ToString OrElse
                        iPassword = iDataReader.Item("password").ToString) Then
                    FuncionComun.loguearLogin(login, False, False, iPassword)

                    Throw New UsuarioClaveErroneaException

                End If

                iId = iDataReader.Item("id").ToString
                iNombre = iDataReader.Item("nombre").ToString
                iLogin = iDataReader.Item("login").ToString
                iPassword = iDataReader.Item("password").ToString
                iIdNivel = iDataReader.Item("idNivel").ToString
                iMail = FuncionComun.vacioSiEsNulo(iDataReader.Item("Mail").ToString)
                iPedirCambioPassword = FuncionComun.byteBoolean(iDataReader.Item("PedirCambioPassword").ToString)
                iAdministrativo = FuncionComun.byteBoolean(iDataReader.Item("administrativo").ToString)
                iEstado = IIf(iDataReader.Item("idEstado").ToString = Estado.ALTA, New Alta, New Baja)
                iEstadoUsuario = New EstadoUsuario
                iEstadoUsuario.id = iDataReader.Item("idEstadoUsuario").ToString
                iUsuarioComercio = FuncionComun.byteBoolean(iDataReader.Item("usuarioComercio").ToString)
                iCodigoOperadorCentralTelefonica = iDataReader.Item("codigoOperadorCentralTelefonica").ToString
                iCodigoColaCentralTelefonica = iDataReader.Item("codigoColaCentralTelefonica").ToString
                iFechaUltimoCambioContraseña = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaUltimoCambioContrasenia").ToString)
                iUltimaContraseña1 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia1").ToString)
                iUltimaContraseña2 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia2").ToString)
                iUltimaContraseña3 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia3").ToString)
                iUltimaContraseña4 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia4").ToString)
                iSupervisorAgenda = FuncionComun.byteBoolean(iDataReader.Item("supervisorAgenda").ToString)
                iAutorizaSolicitud = FuncionComun.byteBoolean(iDataReader.Item("AutorizaSolicitud").ToString)

                If Not IsDBNull(iDataReader.Item("idUsuarioSupervisorAgenda")) Then
                    iUsuarioSupervisorAgenda = New Usuario
                    iUsuarioSupervisorAgenda.id = iDataReader.Item("idUsuarioSupervisorAgenda").ToString
                Else
                    iUsuarioSupervisorAgenda = Nothing
                End If
                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiDiferido")) Then
                    iPuntoVentaDgiDiferido = New PuntoVentaDgi
                    iPuntoVentaDgiDiferido.id = iDataReader.Item("idPuntoVentaDgiDiferido").ToString
                Else
                    iPuntoVentaDgiDiferido = Nothing
                End If
                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiInmediatoPunitorios")) Then
                    iPuntoVentaDgiInmediatoPunitorios = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoPunitorios.id = iDataReader.Item("idPuntoVentaDgiInmediatoPunitorios").ToString
                Else
                    iPuntoVentaDgiInmediatoPunitorios = Nothing
                End If
                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiInmediatoInteresesYGastos")) Then
                    iPuntoVentaDgiInmediatoInteresesYGastos = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoInteresesYGastos.id = iDataReader.Item("idPuntoVentaDgiInmediatoInteresesYGastos").ToString
                Else
                    iPuntoVentaDgiInmediatoInteresesYGastos = Nothing
                End If
                If Not IsDBNull(iDataReader.Item("idpuntoVentaDgiInmediatoNotaCredito")) Then
                    iPuntoVentaDgiInmediatoNotaCredito = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoNotaCredito.id = iDataReader.Item("idpuntoVentaDgiInmediatoNotaCredito").ToString
                Else
                    iPuntoVentaDgiInmediatoNotaCredito = Nothing
                End If
                iIdComercioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idComercioDefault").ToString)
                iIdEntidadCuentaCorrienteComercioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idEntidadCuentaCorrienteComercioDefault").ToString)
                If Not IsDBNull(iDataReader.Item("idusuarioAutorizacion")) Then
                    iUsuarioAutorizacion = New Usuario
                    iUsuarioAutorizacion.id = iDataReader.Item("idusuarioAutorizacion").ToString
                End If
                If Not IsDBNull(iDataReader.Item("idSectorAutorizacion")) Then
                    iSectorAutorizacion = New SectorAutorizacion
                    iSectorAutorizacion.id = iDataReader.Item("idSectorAutorizacion").ToString
                End If
                iLunesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("LunesHoraLoguinDesde").ToString)
                iLunesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("LunesHoraLoguinHasta").ToString)
                iMartesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("MartesHoraLoguinDesde").ToString)
                iMartesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("MartesHoraLoguinHasta").ToString)
                iMiercolesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("MiercolesHoraLoguinDesde").ToString)
                iMiercolesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("MiercolesHoraLoguinHasta").ToString)
                iJuevesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("JuevesHoraLoguinDesde").ToString)
                iJuevesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("JuevesHoraLoguinHasta").ToString)
                iViernesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("ViernesHoraLoguinDesde").ToString)
                iViernesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("ViernesHoraLoguinHasta").ToString)
                iSabadoHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("SabadoHoraLoguinDesde").ToString)
                iSabadoHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("SabadoHoraLoguinHasta").ToString)
                iDomingoHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("DomingoHoraLoguinDesde").ToString)
                iDomingoHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("DomingoHoraLoguinHasta").ToString)
                iIpAcceso = iDataReader.Item("ipAcceso").ToString
                iIpRestringida = iDataReader.Item("ipRestringida").ToString
                iMedioAccesoSistema = New MedioAccesoSistema
                iMedioAccesoSistema.id = iDataReader.Item("idMedioAccesoSistema").ToString
                If Not IsDBNull(iDataReader.Item("idTipoUsuarioTarea")) Then
                    iTipoUsuarioTarea = New TipoUsuarioTarea
                    iTipoUsuarioTarea.id = iDataReader.Item("idTipoUsuarioTarea").ToString
                End If
                iTrabajaConAgenda = FuncionComun.byteBoolean(iDataReader.Item("trabajaConAgenda").ToString)

                If Not IsDBNull(iDataReader.Item("idSucursalPuntos")) Then
                    iSucursalPuntos = New SucursalPuntos
                    iSucursalPuntos.id = iDataReader.Item("idSucursalPuntos").ToString
                End If
                iIdEstudioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idEstudioDefault").ToString)
                iIntentosIngresoPassword = FuncionComun.ceroSiEsVacio(iDataReader.Item("intentosIngresoPassword").ToString)
                iControlaRemito = FuncionComun.byteBoolean(iDataReader.Item("ControlaRemito").ToString)
                iBandejaWelcome = FuncionComun.byteBoolean(iDataReader.Item("bandejaWelcome").ToString)
                iIdPoliticaComercial = FuncionComun.nothingSiEsNulo(iDataReader.Item("idpoliticacomercial"))
                iIdFiltrosWorkflow = FuncionComun.nothingSiEsNulo(iDataReader.Item("idFiltrosWorkflow"))
                iGeneraContrasenia = FuncionComun.byteBoolean(iDataReader.Item("generaContrasenia").ToString)
                iFechaVencimientoGeneraContrasenia = FuncionComun.nothingSiEsVacio(iDataReader.Item("fechaVencimientoGeneraContrasenia").ToString)
                iTokenGeneraContrasenia = FuncionComun.vacioSiEsNulo(iDataReader.Item("tokenGeneraContrasenia").ToString)
                If IsDBNull(iDataReader.Item("idEstadoTokenGeneraContrasenia")) Then
                    iEstadoTokenGeneraContrasenia = Nothing
                Else
                    iEstadoTokenGeneraContrasenia = IIf(iDataReader.Item("idEstadoTokenGeneraContrasenia").ToString = Estado.ALTA, New Alta, New Baja)
                End If

                iPaisVisualizacion = New Pais
                iPaisVisualizacion.id = iDataReader.Item("idPaisVisualizacion").ToString

                iTelefonoCelularCodigoArea = iDataReader.Item("TelefonoCelularCodigoArea").ToString
                iTelefonoCelularCaracteristica = iDataReader.Item("TelefonoCelularCaracteristica").ToString
                iTelefonoCelularNumero = iDataReader.Item("TelefonoCelularNumero").ToString
                iDobleFactor = FuncionComun.byteBoolean(iDataReader.Item("DobleFactor").ToString)
                iToken = iDataReader.Item("Token").ToString
                iFechaCaducidadToken = FuncionComun.nothingSiEsVacio(iDataReader.Item("FechaCaducidadToken").ToString)

                If Not IsDBNull(iDataReader.Item("idDashboard")) Then
                    iDashboard = New Dashboard
                    iDashboard.id = iDataReader.Item("idDashboard").ToString
                End If
                iUsuarioFront = FuncionComun.byteBoolean(iDataReader.Item("usuarioFront").ToString)

                iDataReader.Close()

                If eObtenerNivel Then
                    iNivel = Nivel.obtenerNivelShared(iIdNivel, iConexion)
                    obtenerPaginasAutorizadas()
                End If

                Return Me
            Else
                Throw New UsuarioNoEncontradoException
            End If

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerUsuarioPedirCambioPassword() As Usuario
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("usuario")
            iGeneradorSql.agregarCondicionWhere("login='" & login & "'")
            iGeneradorSql.agregarCondicionWhereEncriptado("password='" & password & "'")
            iGeneradorSql.agregarCondicionWhere("pedirCambioPassword=" & FuncionComun.booleanByte(True))

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                Return Me
            Else
                Return Nothing
            End If


        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Overridable Function obtenerUsuarioPorNombreYPass() As Usuario
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iIdNivel As Integer
        Dim iRol As New Rol
        Dim iAccesoDirecto As New AccesoDirecto

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("login")
            iGeneradorSql.agregarColumna("password")
            iGeneradorSql.agregarColumna("idNivel")
            iGeneradorSql.agregarColumna("idEstado")
            iGeneradorSql.agregarColumna("idestadoUsuario")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiDiferido")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiInmediatoPunitorios")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiInmediatoInteresesYGastos")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiInmediatoNotaCredito")
            iGeneradorSql.agregarColumna("administrativo")
            iGeneradorSql.agregarColumna("pedirCambioPassword")
            iGeneradorSql.agregarColumna("mail")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionRecibo")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionSolicitud")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionChequera")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionMovimiento")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionLiquidacion")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionResumenTarjeta")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionEtiqueta")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionCarta")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionCupon")
            iGeneradorSql.agregarColumna("usuarioComercio")
            iGeneradorSql.agregarColumna("idComercioDefault")
            iGeneradorSql.agregarColumna("idComercioDefaultPromocion")
            iGeneradorSql.agregarColumna("idEntidadCuentaCorrienteComercioDefault")
            iGeneradorSql.agregarColumna("idCaja")
            iGeneradorSql.agregarColumna("idStand")
            iGeneradorSql.agregarColumna("idUsuarioAutorizacion")
            iGeneradorSql.agregarColumna("LunesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("LunesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("MartesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("MartesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("MiercolesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("MiercolesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("JuevesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("JuevesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("ViernesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("ViernesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("SabadoHoraLoguinDesde")
            iGeneradorSql.agregarColumna("SabadoHoraLoguinHasta")
            iGeneradorSql.agregarColumna("DomingoHoraLoguinDesde")
            iGeneradorSql.agregarColumna("DomingoHoraLoguinHasta")
            iGeneradorSql.agregarColumna("idMedioAccesoSistema")
            iGeneradorSql.agregarColumna("ipAcceso")
            iGeneradorSql.agregarColumna("ipRestringida")
            iGeneradorSql.agregarColumna("idTipoUsuarioTarea")
            iGeneradorSql.agregarColumna("trabajaConAgenda")
            iGeneradorSql.agregarColumna("supervisorAgenda")
            iGeneradorSql.agregarColumna("autorizaSolicitud")
            iGeneradorSql.agregarColumna("idUsuarioSupervisorAgenda")
            iGeneradorSql.agregarColumna("codigoOperadorCentralTelefonica")
            iGeneradorSql.agregarColumna("codigoColaCentralTelefonica")
            iGeneradorSql.agregarColumna("fechaUltimoCambioContrasenia")
            iGeneradorSql.agregarColumna("ultimaContrasenia1")
            iGeneradorSql.agregarColumna("ultimaContrasenia2")
            iGeneradorSql.agregarColumna("ultimaContrasenia3")
            iGeneradorSql.agregarColumna("ultimaContrasenia4")
            iGeneradorSql.agregarColumna("idSucursalPuntos")
            iGeneradorSql.agregarColumna("idSectorAutorizacion")
            iGeneradorSql.agregarColumna("idEstudioDefault")
            iGeneradorSql.agregarColumna("intentosIngresoPassword")
            iGeneradorSql.agregarColumna("idVendedorDefault")
            iGeneradorSql.agregarColumna("controlaRemito")
            iGeneradorSql.agregarColumna("bandejaWelcome")
            iGeneradorSql.agregarColumna("idpoliticacomercial")
            iGeneradorSql.agregarColumna("idFiltrosWorkflow")
            iGeneradorSql.agregarColumna("generaContrasenia")
            iGeneradorSql.agregarColumna("fechaVencimientoGeneraContrasenia")
            iGeneradorSql.agregarColumna("tokenGeneraContrasenia")
            iGeneradorSql.agregarColumna("idEstadoTokenGeneraContrasenia")
            iGeneradorSql.agregarColumna("idPaisVisualizacion")
            iGeneradorSql.agregarColumna("TelefonoCelularCodigoArea")
            iGeneradorSql.agregarColumna("TelefonoCelularCaracteristica")
            iGeneradorSql.agregarColumna("TelefonoCelularNumero")
            iGeneradorSql.agregarColumna("DobleFactor")
            iGeneradorSql.agregarColumna("Token")
            iGeneradorSql.agregarColumna("FechaCaducidadToken")
            'iGeneradorSql.agregarColumna("idDashboard")
            iGeneradorSql.agregarColumna("usuarioFront")
            'iGeneradorSql.agregarColumna("fotoPerfil")

            iGeneradorSql.agregarTabla("usuario")
            iGeneradorSql.agregarCondicionWhere("login='" & login & "'")
            iGeneradorSql.agregarCondicionWhere("idEstado=" & Estado.ALTA)
            'iGeneradorSql.agregarCondicionWhere("idestadoUsuario=" & estadoUsuario.HABILITADO)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then

                Select Case iDataReader.Item("idEstadoUsuario").ToString()
                    Case EstadoUsuario.BLOQUEADO
                        FuncionComun.loguearLogin(login, False, True, "")
                        Throw New UsuarioBloqueadoException
                End Select

                If Not (FuncionComun.obtenerCodigoHashMD5(iPassword) = iDataReader.Item("password").ToString OrElse
                        FuncionComun.obtenerCodigoHashMD5(iPassword.ToLower) = iDataReader.Item("password").ToString OrElse
                        FuncionComun.obtenerCodigoHashMD5(iPassword.ToUpper) = iDataReader.Item("password").ToString OrElse
                        iPassword = iDataReader.Item("password").ToString) Then
                    FuncionComun.loguearLogin(login, False, False, iPassword)
                    Throw New UsuarioClaveErroneaException

                End If

                FuncionComun.loguearLogin(login, True, False, "")

                iId = iDataReader.Item("id").ToString
                iNombre = iDataReader.Item("nombre").ToString
                iLogin = iDataReader.Item("login").ToString
                iPassword = iDataReader.Item("password").ToString
                iMail = FuncionComun.vacioSiEsNulo(iDataReader.Item("Mail").ToString)
                iPedirCambioPassword = FuncionComun.byteBoolean(iDataReader.Item("PedirCambioPassword").ToString)
                iAdministrativo = FuncionComun.byteBoolean(iDataReader.Item("administrativo").ToString)
                iEstado = IIf(iDataReader.Item("idEstado").ToString = Estado.ALTA, New Alta, New Baja)
                iEstadoUsuario = New EstadoUsuario
                iEstadoUsuario.id = iDataReader.Item("idEstadoUsuario").ToString
                iUsuarioComercio = FuncionComun.byteBoolean(iDataReader.Item("usuarioComercio").ToString)
                iIdNivel = iDataReader.Item("idNivel").ToString
                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiDiferido")) Then
                    iPuntoVentaDgiDiferido = New PuntoVentaDgi
                    iPuntoVentaDgiDiferido.id = iDataReader.Item("idPuntoVentaDgiDiferido").ToString
                Else
                    iPuntoVentaDgiDiferido = Nothing
                End If
                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiInmediatoPunitorios")) Then
                    iPuntoVentaDgiInmediatoPunitorios = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoPunitorios.id = iDataReader.Item("idPuntoVentaDgiInmediatoPunitorios").ToString
                Else
                    iPuntoVentaDgiInmediatoPunitorios = Nothing
                End If
                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiInmediatoInteresesYGastos")) Then
                    iPuntoVentaDgiInmediatoInteresesYGastos = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoInteresesYGastos.id = iDataReader.Item("idPuntoVentaDgiInmediatoInteresesYGastos").ToString
                Else
                    iPuntoVentaDgiInmediatoInteresesYGastos = Nothing
                End If
                If Not IsDBNull(iDataReader.Item("idPuntoVentaDgiInmediatoNotaCredito")) Then
                    iPuntoVentaDgiInmediatoNotaCredito = New PuntoVentaDgi
                    iPuntoVentaDgiInmediatoNotaCredito.id = iDataReader.Item("idPuntoVentaDgiInmediatoNotaCredito").ToString
                Else
                    iPuntoVentaDgiInmediatoNotaCredito = Nothing
                End If
                If Not IsDBNull(iDataReader.Item("idusuarioAutorizacion")) Then
                    iUsuarioAutorizacion = New Usuario
                    iUsuarioAutorizacion.id = iDataReader.Item("idusuarioAutorizacion").ToString
                End If
                If Not IsDBNull(iDataReader.Item("idSectorAutorizacion")) Then
                    iSectorAutorizacion = New SectorAutorizacion
                    iSectorAutorizacion.id = iDataReader.Item("idSectorAutorizacion").ToString
                End If
                iIdVendedorDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("IdVendedorDefault").ToString)
                iIdComercioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idComercioDefault").ToString)
                iIdComercioDefaultPromocion = FuncionComun.ceroSiEsVacio(iDataReader.Item("idComercioDefaultPromocion").ToString)
                iIdEntidadCuentaCorrienteComercioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("IdEntidadCuentaCorrienteComercioDefault").ToString)
                iLunesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("LunesHoraLoguinDesde").ToString)
                iLunesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("LunesHoraLoguinHasta").ToString)
                iMartesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("MartesHoraLoguinDesde").ToString)
                iMartesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("MartesHoraLoguinHasta").ToString)
                iMiercolesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("MiercolesHoraLoguinDesde").ToString)
                iMiercolesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("MiercolesHoraLoguinHasta").ToString)
                iJuevesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("JuevesHoraLoguinDesde").ToString)
                iJuevesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("JuevesHoraLoguinHasta").ToString)
                iViernesHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("ViernesHoraLoguinDesde").ToString)
                iViernesHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("ViernesHoraLoguinHasta").ToString)
                iSabadoHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("SabadoHoraLoguinDesde").ToString)
                iSabadoHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("SabadoHoraLoguinHasta").ToString)
                iDomingoHoraLoguinDesde = FuncionComun.nothingSiEsVacio(iDataReader.Item("DomingoHoraLoguinDesde").ToString)
                iDomingoHoraLoguinHasta = FuncionComun.nothingSiEsVacio(iDataReader.Item("DomingoHoraLoguinHasta").ToString)
                iIpAcceso = iDataReader.Item("ipAcceso").ToString
                iIpRestringida = iDataReader.Item("ipRestringida").ToString
                iMedioAccesoSistema = New MedioAccesoSistema
                iMedioAccesoSistema.id = iDataReader.Item("idMedioAccesoSistema").ToString
                If Not IsDBNull(iDataReader.Item("idTipoUsuarioTarea")) Then
                    iTipoUsuarioTarea = New TipoUsuarioTarea
                    iTipoUsuarioTarea.id = iDataReader.Item("idTipoUsuarioTarea").ToString
                End If
                iTrabajaConAgenda = FuncionComun.byteBoolean(iDataReader.Item("trabajaConAgenda").ToString)
                iSupervisorAgenda = FuncionComun.byteBoolean(iDataReader.Item("supervisorAgenda").ToString)
                iAutorizaSolicitud = FuncionComun.byteBoolean(iDataReader.Item("AutorizaSolicitud").ToString)
                iFechaUltimoCambioContraseña = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaUltimoCambioContrasenia").ToString)
                iUltimaContraseña1 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia1").ToString)
                iUltimaContraseña2 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia2").ToString)
                iUltimaContraseña3 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia3").ToString)
                iUltimaContraseña4 = FuncionComun.nothingSiEsNulo(iDataReader.Item("ultimaContrasenia4").ToString)
                iCodigoOperadorCentralTelefonica = iDataReader.Item("codigoOperadorCentralTelefonica").ToString
                iCodigoColaCentralTelefonica = iDataReader.Item("codigoColaCentralTelefonica").ToString
                If Not IsDBNull(iDataReader.Item("idUsuarioSupervisorAgenda")) Then
                    iUsuarioSupervisorAgenda = New Usuario
                    iUsuarioSupervisorAgenda.id = iDataReader.Item("idUsuarioSupervisorAgenda").ToString
                Else
                    iUsuarioSupervisorAgenda = Nothing
                End If
                If Not IsDBNull(iDataReader.Item("idSucursalPuntos")) Then
                    iSucursalPuntos = New SucursalPuntos
                    iSucursalPuntos.id = iDataReader.Item("idSucursalPuntos").ToString
                End If
                iIdEstudioDefault = FuncionComun.ceroSiEsVacio(iDataReader.Item("idEstudioDefault").ToString)
                iIntentosIngresoPassword = FuncionComun.ceroSiEsVacio(iDataReader.Item("intentosIngresoPassword").ToString)
                iControlaRemito = FuncionComun.byteBoolean(iDataReader.Item("ControlaRemito").ToString)
                iBandejaWelcome = FuncionComun.byteBoolean(iDataReader.Item("bandejaWelcome").ToString)
                iIdPoliticaComercial = FuncionComun.nothingSiEsNulo(iDataReader.Item("idpoliticacomercial"))
                iIdFiltrosWorkflow = FuncionComun.nothingSiEsNulo(iDataReader.Item("idFiltrosWorkflow"))
                iGeneraContrasenia = FuncionComun.byteBoolean(iDataReader.Item("generaContrasenia").ToString)
                iFechaVencimientoGeneraContrasenia = FuncionComun.nothingSiEsVacio(iDataReader.Item("fechaVencimientoGeneraContrasenia").ToString)
                iTokenGeneraContrasenia = FuncionComun.vacioSiEsNulo(iDataReader.Item("tokenGeneraContrasenia").ToString)
                If IsDBNull(iDataReader.Item("idEstadoTokenGeneraContrasenia")) Then
                    iEstadoTokenGeneraContrasenia = Nothing
                Else
                    iEstadoTokenGeneraContrasenia = IIf(iDataReader.Item("idEstadoTokenGeneraContrasenia").ToString = Estado.ALTA, New Alta, New Baja)
                End If
                iPaisVisualizacion = New Pais
                iPaisVisualizacion.id = iDataReader.Item("idPaisVisualizacion").ToString
                iTelefonoCelularCodigoArea = iDataReader.Item("TelefonoCelularCodigoArea").ToString
                iTelefonoCelularCaracteristica = iDataReader.Item("TelefonoCelularCaracteristica").ToString
                iTelefonoCelularNumero = iDataReader.Item("TelefonoCelularNumero").ToString
                iDobleFactor = FuncionComun.byteBoolean(iDataReader.Item("DobleFactor").ToString)
                iToken = iDataReader.Item("Token").ToString
                iFechaCaducidadToken = FuncionComun.nothingSiEsVacio(iDataReader.Item("FechaCaducidadToken").ToString)


                iUsuarioFront = FuncionComun.byteBoolean(iDataReader.Item("usuarioFront").ToString)

                iDataReader.Close()

                perfiles = obtenerUsuarioPerfil()


                If Not IsNothing(iPuntoVentaDgiDiferido) Then
                    iPuntoVentaDgiDiferido.accesoDatos = iConexion
                    iPuntoVentaDgiDiferido = iPuntoVentaDgiDiferido.obtenerNivel()
                    iPuntoVentaDgiDiferido.accesoDatos = Nothing
                End If
                If Not IsNothing(iSectorAutorizacion) Then
                    iSectorAutorizacion.accesoDatos = iConexion
                    iSectorAutorizacion = iSectorAutorizacion.obtenerSectorAutorizacion
                    iSectorAutorizacion.accesoDatos = Nothing
                End If
                If Not IsNothing(iUsuarioAutorizacion) Then
                    iUsuarioAutorizacion.accesoDatos = iConexion
                    iUsuarioAutorizacion = iUsuarioAutorizacion.obtenerUsuarioSoloIds(True)
                    iUsuarioAutorizacion.accesoDatos = Nothing
                End If
                If Not IsNothing(iTipoUsuarioTarea) Then
                    iTipoUsuarioTarea.accesoDatos = iConexion
                    iTipoUsuarioTarea = iTipoUsuarioTarea.obtenerTipoUsuarioTarea
                    iTipoUsuarioTarea.accesoDatos = Nothing
                End If

                iRol.accesoDatos = iConexion
                iRol.usuario = Me
                iRolesAutorizados = iRol.obtenerRolesAutorizados()
                iRol.accesoDatos = Nothing

                iAccesoDirecto.accesoDatos = iConexion
                iAccesosDirectos = iAccesoDirecto.obtenerAccesosDirectosPorUsuario(Me)
                iAccesoDirecto.accesoDatos = Nothing

                If Not IsNothing(iSucursalPuntos) Then
                    iSucursalPuntos.accesoDatos = iConexion
                    iSucursalPuntos = iSucursalPuntos.obtenerSucursalPuntos
                    iSucursalPuntos.accesoDatos = Nothing
                End If

                iPaisVisualizacion.accesoDatos = iConexion
                iPaisVisualizacion = iPaisVisualizacion.obtenerPais
                iPaisVisualizacion.accesoDatos = Nothing

                Return Me
            Else
                Throw New UsuarioNoEncontradoException
            End If

        Catch excepcion As Exception
            Throw excepcion
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iRol = Nothing
            iAccesoDirecto = Nothing
        End Try
    End Function

    Public Function obtenerUsuarios() As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()
            If Not IsNothing(iNivel) Then
                iGeneradorSql.agregarColumna("u.id")
                iGeneradorSql.agregarColumna("u.nombre")
                iGeneradorSql.agregarColumna("u.login")
                iGeneradorSql.agregarColumna("u.password")
                iGeneradorSql.agregarColumna("u.idNivel")
                iGeneradorSql.agregarColumna("u.idEstado")
                iGeneradorSql.agregarColumna(FuncionComun.sqlConcatenar("u.login,' - ',u.nombre") & " as loginnombre")

                iGeneradorSql.agregarTablaPrincipal("usuario u")
                iGeneradorSql.agregarTablaConJoin("nivel n", "u.idnivel = n.id")
                iGeneradorSql.agregarTablaConJoin("UsuarioPerfil up", "up.idUsuario = u.id")

                If iNivel.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("n.id=" & nivel.id)
                If iNivel.isSucursal Then iGeneradorSql.agregarCondicionWhere("n.idTipoNivel=" & TipoNivel.SUCURSAL)
                If iNivel.isUnidadDeNegocios Then iGeneradorSql.agregarCondicionWhere("n.idTipoNivel=" & TipoNivel.UNIDADDENEGOCIOS)
                If iNivel.isEmpresaGrupo Then iGeneradorSql.agregarCondicionWhere("n.idTipoNivel=" & TipoNivel.EMPRESAGRUPO)
                If iNivel.isGrupoEmpresas Then iGeneradorSql.agregarCondicionWhere("n.idTipoNivel=" & TipoNivel.GRUPOEMPRESAS)
                If Not IsNothing(iPerfiles) Then iGeneradorSql.agregarCondicionWhere("up.idPerfil=" & iPerfiles(0).id)
                If Not IsNothing(iTrabajaConAgenda) Then iGeneradorSql.agregarCondicionWhere("u.trabajaConAgenda=" & FuncionComun.booleanByte(iTrabajaConAgenda.Value))
                If Not IsNothing(iSupervisorAgenda) Then iGeneradorSql.agregarCondicionWhere("u.supervisorAgenda=" & FuncionComun.booleanByte(iSupervisorAgenda.Value))
                If Not IsNothing(iAutorizaSolicitud) Then iGeneradorSql.agregarCondicionWhere("u.autorizaSolicitud=" & FuncionComun.booleanByte(iAutorizaSolicitud.Value))
                If Not IsNothing(iControlaRemito) Then iGeneradorSql.agregarCondicionWhere("u.controlaRemito=" & FuncionComun.booleanByte(controlaRemito.Value))
                If Not IsNothing(iBandejaWelcome) Then iGeneradorSql.agregarCondicionWhere("u.bandejaWelcome=" & FuncionComun.booleanByte(bandejaWelcome.Value))
                iGeneradorSql.agregarCondicionWhere("u.idEstado = " & Estado.ALTA)

                'iGeneradorSql.agregarOrden("u.nombre asc")
                iGeneradorSql.agregarOrden("u.login asc")
            Else
                iGeneradorSql.agregarColumna("u.id")
                iGeneradorSql.agregarColumna("u.nombre")
                iGeneradorSql.agregarColumna("u.login")
                iGeneradorSql.agregarColumna("u.password")
                iGeneradorSql.agregarColumna("u.idNivel")
                iGeneradorSql.agregarColumna("u.idEstado")
                iGeneradorSql.agregarColumna(FuncionComun.sqlConcatenar("u.login,' - ',u.nombre") & " as loginnombre")

                iGeneradorSql.agregarTablaPrincipal("usuario u")
                iGeneradorSql.agregarTablaConJoin("UsuarioPerfil up", "up.idUsuario = u.id")

                If Not IsNothing(iSupervisorAgenda) Then iGeneradorSql.agregarCondicionWhere("u.supervisorAgenda=" & FuncionComun.booleanByte(iSupervisorAgenda.Value))
                If Not IsNothing(iAutorizaSolicitud) Then iGeneradorSql.agregarCondicionWhere("u.autorizaSolicitud=" & FuncionComun.booleanByte(iAutorizaSolicitud.Value))
                If Not IsNothing(iTrabajaConAgenda) Then iGeneradorSql.agregarCondicionWhere("u.trabajaConAgenda=" & FuncionComun.booleanByte(iTrabajaConAgenda.Value))
                If Not IsNothing(iControlaRemito) Then iGeneradorSql.agregarCondicionWhere("u.controlaRemito=" & FuncionComun.booleanByte(controlaRemito.Value))
                If Not IsNothing(iBandejaWelcome) Then iGeneradorSql.agregarCondicionWhere("u.bandejaWelcome=" & FuncionComun.booleanByte(bandejaWelcome.Value))

                If Not IsNothing(iPerfiles) Then iGeneradorSql.agregarCondicionWhere("up.idPerfil=" & iPerfiles(0).id)
                iGeneradorSql.agregarCondicionWhere("idestado=" & Estado.ALTA)

                'iGeneradorSql.agregarOrden("nombre asc")
                iGeneradorSql.agregarOrden("login asc")
            End If

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerUsuarios(ByVal eNivel As Nivel) As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("u.id")
            iGeneradorSql.agregarColumna("u.nombre")
            iGeneradorSql.agregarColumna("u.login")
            iGeneradorSql.agregarColumna("u.password")
            iGeneradorSql.agregarColumna("u.idNivel")
            iGeneradorSql.agregarColumna("u.idEstado")
            iGeneradorSql.agregarColumna(FuncionComun.sqlConcatenar("u.login,' - ',u.nombre") & " as loginnombre")

            iGeneradorSql.agregarTabla("usuario u")

            If Not IsNothing(iEstado) Then iGeneradorSql.agregarCondicionWhere("u.idEstado = " & iEstado.id)
            If Not IsNothing(eNivel) AndAlso eNivel.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("u.idnivel=" & eNivel.id)
            iGeneradorSql.agregarOrden("u.nombre asc")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerUsuariosGrilla(ByVal eUsuariosGrillaVO As UsuariosGrillaVO) As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Dim iNiveles As String
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("u.id")
            iGeneradorSql.agregarColumna("u.nombre")
            iGeneradorSql.agregarColumna("u.login")
            iGeneradorSql.agregarColumna("e.Descripcion as estado")

            iGeneradorSql.agregarTablaPrincipal("usuario u")
            iGeneradorSql.agregarTablaConJoin("usuarioperfil up", "u.id=up.idusuario")
            iGeneradorSql.agregarTablaConJoin("Estado e", "e.id=u.idEstado")

            With eUsuariosGrillaVO
                If Not IsNothing(.perfil) Then iGeneradorSql.agregarCondicionWhere("up.idPerfil=" & .perfil.id, False)

                If Not IsNothing(.nivel) AndAlso .nivel.id <> Nothing Then
                    If .nivel.isGrupoEmpresas Then
                        iNiveles = "select id from GrupoEmpresas where id=" & .nivel.id & " union  select id from empresaGrupo where idGrupoEmpresas=" & .nivel.id & " union select id from unidadDeNegocios where idEmpresaGrupo in (select id from empresaGrupo where idGrupoEmpresas=" & .nivel.id & ") union select id from sucursal where idUnidadDeNegocios in (select id from unidadDeNegocios where idEmpresaGrupo in (select id from empresaGrupo where idGrupoEmpresas=" & .nivel.id & "))"
                        iGeneradorSql.agregarCondicionWhere("u.idNivel in (" & iNiveles & ")", True)
                    ElseIf .nivel.isEmpresaGrupo Then
                        iNiveles = "select id from empresaGrupo where id=" & .nivel.id & " union select id from unidadDeNegocios where idEmpresaGrupo=" & .nivel.id & " union select id from sucursal where idUnidadDeNegocios in (select id from unidadDeNegocios where idEmpresaGrupo=" & .nivel.id & ")"
                        iGeneradorSql.agregarCondicionWhere("u.idNivel in (" & iNiveles & ")", True)
                    ElseIf .nivel.isUnidadDeNegocios Then
                        iNiveles = "select id from unidadDeNegocios where id=" & .nivel.id & " union select id from sucursal where idUnidadDeNegocios=" & .nivel.id
                        iGeneradorSql.agregarCondicionWhere("u.idNivel in (" & iNiveles & ")", True)
                    ElseIf .nivel.isSucursal Then
                        iGeneradorSql.agregarCondicionWhere("u.idNivel=" & .nivel.id)
                    End If
                End If

                If Not IsNothing(.estado) Then iGeneradorSql.agregarCondicionWhere("u.idEstado=" & .estado.id)
                If .nombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("u.nombre like '" & .nombre & "%'")
                If .login <> Nothing Then iGeneradorSql.agregarCondicionWhere("u.login like '" & .login & "%'")
            End With

            iGeneradorSql.agregarOrden("u.nombre asc")
            iGeneradorSql.agregarGroupBy("u.id")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "UsuariosGrilla")

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Private Sub obtenerPaginasAutorizadas()
        Dim iDataSet As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Dim i As Integer

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTablaPrincipal("accion a")
            iGeneradorSql.agregarTablaConJoin("menu m", "a.id=m.idaccion")
            iGeneradorSql.agregarTablaConJoin("perfilMenu pm", "pm.idMenu=m.id")
            iGeneradorSql.agregarTablaConJoin("usuarioperfil up", "up.idPerfil=pm.idPerfil")
            iGeneradorSql.agregarTablaConJoin("usuario u", "u.id=up.idUsuario")

            iGeneradorSql.agregarColumna("Case when m.idTipoSistema = " & Menu.EnumTipoSistema.LOAN & " then " & FuncionComun.sqlCortarTextoEnCantidadCaracteres("a.paginaAsociada", FuncionComun.sqlPosicionCaracter("a.paginaAsociada", ".") & "  + 4", FuncionComun.enumSentido.IZQUIERDA) & " else a.paginaAsociada end as paginaAsociada")
            iGeneradorSql.agregarCondicionWhere("u.id=" & id)

            iGeneradorSql.agregarOrden("a.paginaAsociada")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "PaginaUsuario")

            ReDim iPaginasAutorizadas(iDataSet.Tables("PaginaUsuario").Rows.Count)

            For i = 1 To iDataSet.Tables("PaginaUsuario").Rows.Count
                iPaginasAutorizadas.SetValue(iDataSet.Tables("PaginaUsuario").Rows(i - 1).Item("paginaAsociada").ToString.ToLower, i)
            Next i

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iDataSet = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function puede(ByVal eRol As String) As Boolean
        Try
            Return Array.BinarySearch(iRolesAutorizados, eRol) > 0
        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        End Try
    End Function

    Public Function puedeAcceder(ByVal eNombrePagina As String) As Boolean
        Try
            Return Array.BinarySearch(iPaginasAutorizadas, eNombrePagina.ToLower) > 0
        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        End Try
    End Function

    Private Sub validarCrear()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iMensaje As String

        Try

            iConexion = obtenerConexion()

            If iPassword <> Nothing Then
                If Not esPasswordFuerte(iConexion, iPassword, iMensaje) Then
                    ' Throw New UsuarioNoModificadoException(iMensaje)
                End If
            End If

            iGeneradorSql.agregarColumna("login")
            iGeneradorSql.agregarTabla("Usuario")
            iGeneradorSql.agregarCondicionWhere("login='" & login & "'")
            iGeneradorSql.agregarCondicionWhere("idEstado=" & Estado.ALTA)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoCreadoException("Ya existe un usuario con ese login")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("login")
            iGeneradorSql.agregarTabla("Usuario")

            iGeneradorSql.agregarCondicionWhere("TelefonoCelularCodigoArea=" & FuncionComun.nuloSiEsNothing(iTelefonoCelularCodigoArea))
            iGeneradorSql.agregarCondicionWhere("TelefonoCelularCaracteristica=" & FuncionComun.nuloSiEsNothing(iTelefonoCelularCaracteristica))
            iGeneradorSql.agregarCondicionWhere("TelefonoCelularNumero=" & FuncionComun.nuloSiEsNothing(iTelefonoCelularNumero))
            iGeneradorSql.agregarCondicionWhere("TelefonoCelularCodigoArea is not null")
            iGeneradorSql.agregarCondicionWhere("TelefonoCelularCaracteristica is not null")
            iGeneradorSql.agregarCondicionWhere("TelefonoCelularNumero is not null")


            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoCreadoException("Ya existe un usuario con ese telefono celular")
            End If
            iDataReader.Close()



        Catch excepcion As ErrorConexionException
            Throw New UsuarioNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Overridable Sub crear()
        Dim iGeneradorSql As New GeneradorSql
        Dim iRol As New Rol
        Dim i As Integer

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("Usuario")
            iGeneradorSql.agregarColumna("Nombre")
            iGeneradorSql.agregarColumna("Login")
            iGeneradorSql.agregarColumna("Password")
            iGeneradorSql.agregarColumna("PedirCambioPassword")
            iGeneradorSql.agregarColumna("Administrativo")
            iGeneradorSql.agregarColumna("Mail")
            iGeneradorSql.agregarColumna("idEstado")
            iGeneradorSql.agregarColumna("idestadoUsuario")
            iGeneradorSql.agregarColumna("idNivel")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiDiferido")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiInmediatoPunitorios")
            iGeneradorSql.agregarColumna("idPuntoVentaDgiInmediatoInteresesYGastos")
            iGeneradorSql.agregarColumna("idpuntoVentaDgiInmediatoNotaCredito")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionRecibo")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionSolicitud")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionChequera")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionMovimiento")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionLiquidacion")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionResumenTarjeta")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionEtiqueta")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionCarta")
            iGeneradorSql.agregarColumna("idTipoServicioImpresionCupon")
            iGeneradorSql.agregarColumna("usuarioComercio")
            iGeneradorSql.agregarColumna("idComercioDefault")
            iGeneradorSql.agregarColumna("idComercioDefaultPromocion")
            iGeneradorSql.agregarColumna("idEntidadCuentaCorrienteComercioDefault")
            iGeneradorSql.agregarColumna("idCaja")
            iGeneradorSql.agregarColumna("idStand")
            iGeneradorSql.agregarColumna("idUsuarioAutorizacion")
            iGeneradorSql.agregarColumna("LunesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("LunesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("MartesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("MartesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("MiercolesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("MiercolesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("JuevesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("JuevesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("ViernesHoraLoguinDesde")
            iGeneradorSql.agregarColumna("ViernesHoraLoguinHasta")
            iGeneradorSql.agregarColumna("SabadoHoraLoguinDesde")
            iGeneradorSql.agregarColumna("SabadoHoraLoguinHasta")
            iGeneradorSql.agregarColumna("DomingoHoraLoguinDesde")
            iGeneradorSql.agregarColumna("DomingoHoraLoguinHasta")
            iGeneradorSql.agregarColumna("idMedioAccesoSistema")
            iGeneradorSql.agregarColumna("ipAcceso")
            iGeneradorSql.agregarColumna("ipRestringida")
            iGeneradorSql.agregarColumna("idTipoUsuarioTarea")
            iGeneradorSql.agregarColumna("trabajaConAgenda")
            iGeneradorSql.agregarColumna("supervisorAgenda")
            iGeneradorSql.agregarColumna("autorizaSolicitud")
            iGeneradorSql.agregarColumna("idUsuarioSupervisorAgenda")
            iGeneradorSql.agregarColumna("codigoOperadorCentralTelefonica")
            iGeneradorSql.agregarColumna("codigoColaCentralTelefonica")
            iGeneradorSql.agregarColumna("ultimaContrasenia1")
            iGeneradorSql.agregarColumna("fechaUltimoCambioContrasenia")
            iGeneradorSql.agregarColumna("idSucursalPuntos")
            iGeneradorSql.agregarColumna("idSectorAutorizacion")
            iGeneradorSql.agregarColumna("idEstudioDefault")
            iGeneradorSql.agregarColumna("idVendedorDefault")
            iGeneradorSql.agregarColumna("controlaRemito")
            iGeneradorSql.agregarColumna("bandejaWelcome")
            iGeneradorSql.agregarColumna("idpoliticacomercial")
            iGeneradorSql.agregarColumna("idFiltrosWorkflow")
            iGeneradorSql.agregarColumna("generaContrasenia")
            iGeneradorSql.agregarColumna("fechaVencimientoGeneraContrasenia")
            iGeneradorSql.agregarColumna("tokenGeneraContrasenia")
            iGeneradorSql.agregarColumna("idEstadoTokenGeneraContrasenia")
            iGeneradorSql.agregarColumna("idPaisVisualizacion")
            iGeneradorSql.agregarColumna("TelefonoCelularCodigoArea")
            iGeneradorSql.agregarColumna("TelefonoCelularCaracteristica")
            iGeneradorSql.agregarColumna("TelefonoCelularNumero")
            iGeneradorSql.agregarColumna("DobleFactor")
            iGeneradorSql.agregarColumna("Token")
            iGeneradorSql.agregarColumna("FechaCaducidadToken")
            iGeneradorSql.agregarColumna("idDashboard")
            iGeneradorSql.agregarColumna("usuarioFront")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(nombre))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(login))
            iGeneradorSql.agregarValueEncriptado(password)
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(pedirCambioPassword))
            If administrativo.HasValue Then
                iGeneradorSql.agregarValue(FuncionComun.booleanByte(administrativo))
            Else
                iGeneradorSql.agregarValue("null")
            End If
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(mail))
            iGeneradorSql.agregarValue(IIf(iEstado.isAlta, Estado.ALTA, Estado.BAJA))
            If IsNothing(iEstadoUsuario) Then
                iGeneradorSql.agregarValue("null")
            Else
                iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iEstadoUsuario.id))
            End If
            If IsNothing(iNivel) Then
                iGeneradorSql.agregarValue("null")
            Else
                iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iNivel.id))
            End If
            If IsNothing(puntoVentaDgiDiferido) Then
                iGeneradorSql.agregarValue("null")
            Else
                iGeneradorSql.agregarValue(puntoVentaDgiDiferido.id)
            End If
            If IsNothing(puntoVentaDgiInmediatoPunitorios) Then
                iGeneradorSql.agregarValue("null")
            Else
                iGeneradorSql.agregarValue(puntoVentaDgiInmediatoPunitorios.id)
            End If
            If IsNothing(puntoVentaDgiInmediatoInteresesYGastos) Then
                iGeneradorSql.agregarValue("null")
            Else
                iGeneradorSql.agregarValue(puntoVentaDgiInmediatoInteresesYGastos.id)
            End If
            If IsNothing(puntoVentaDgiInmediatoNotaCredito) Then
                iGeneradorSql.agregarValue("null")
            Else
                iGeneradorSql.agregarValue(puntoVentaDgiInmediatoNotaCredito.id)
            End If
            If iUsuarioComercio.HasValue Then
                iGeneradorSql.agregarValue(FuncionComun.booleanByte(iUsuarioComercio))
            Else
                iGeneradorSql.agregarValue("null")
            End If
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iIdComercioDefault))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iIdComercioDefaultPromocion))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iIdEntidadCuentaCorrienteComercioDefault))
            If Not IsNothing(iUsuarioAutorizacion) Then
                iGeneradorSql.agregarValue(iUsuarioAutorizacion.id)
            Else
                iGeneradorSql.agregarValue("null")
            End If
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iLunesHoraLoguinDesde))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iLunesHoraLoguinHasta))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iMartesHoraLoguinDesde))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iMartesHoraLoguinHasta))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iMiercolesHoraLoguinDesde))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iMiercolesHoraLoguinHasta))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iJuevesHoraLoguinDesde))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iJuevesHoraLoguinHasta))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iViernesHoraLoguinDesde))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iViernesHoraLoguinHasta))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iSabadoHoraLoguinDesde))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iSabadoHoraLoguinHasta))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iDomingoHoraLoguinDesde))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingHora(iDomingoHoraLoguinHasta))
            If Not IsNothing(iMedioAccesoSistema) Then
                iGeneradorSql.agregarValue(iMedioAccesoSistema.id)
            Else
                iGeneradorSql.agregarValue("null")
            End If
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iIpAcceso))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iIpRestringida))
            If Not IsNothing(iTipoUsuarioTarea) Then
                iGeneradorSql.agregarValue(iTipoUsuarioTarea.id)
            Else
                iGeneradorSql.agregarValue("null")
            End If
            If iTrabajaConAgenda.HasValue Then
                iGeneradorSql.agregarValue(FuncionComun.booleanByte(iTrabajaConAgenda))
            Else
                iGeneradorSql.agregarValue("null")
            End If
            If iAutorizaSolicitud.HasValue Then
                iGeneradorSql.agregarValue(FuncionComun.booleanByte(iAutorizaSolicitud))
            Else
                iGeneradorSql.agregarValue("null")
            End If
            If iSupervisorAgenda.HasValue Then
                iGeneradorSql.agregarValue(FuncionComun.booleanByte(iSupervisorAgenda))
            Else
                iGeneradorSql.agregarValue("null")
            End If
            If IsNothing(usuarioSupervisorAgenda) Then
                iGeneradorSql.agregarValue("null")
            Else
                iGeneradorSql.agregarValue(usuarioSupervisorAgenda.id)
            End If
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(codigoOperadorCentralTelefonica))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(codigoColaCentralTelefonica))
            iGeneradorSql.agregarValueEncriptado(iPassword)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(Today))
            If Not IsNothing(iSucursalPuntos) Then
                iGeneradorSql.agregarValue(iSucursalPuntos.id)
            Else
                iGeneradorSql.agregarValue("null")
            End If
            If Not IsNothing(iSectorAutorizacion) Then
                iGeneradorSql.agregarValue(iSectorAutorizacion.id)
            Else
                iGeneradorSql.agregarValue("null")
            End If
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(idEstudioDefault))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(idVendedorDefault))
            If iControlaRemito.HasValue Then
                iGeneradorSql.agregarValue(FuncionComun.booleanByte(iControlaRemito))
            Else
                iGeneradorSql.agregarValue("null")
            End If
            If iBandejaWelcome.HasValue Then
                iGeneradorSql.agregarValue(FuncionComun.booleanByte(iBandejaWelcome))
            Else
                iGeneradorSql.agregarValue("null")
            End If
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iIdPoliticaComercial))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iIdFiltrosWorkflow))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(iGeneraContrasenia))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iFechaVencimientoGeneraContrasenia))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iTokenGeneraContrasenia))
            If Not IsNothing(iEstadoTokenGeneraContrasenia) Then
                iGeneradorSql.agregarValue(IIf(iEstadoTokenGeneraContrasenia.isAlta, Estado.ALTA, Estado.BAJA))
            Else
                iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iEstadoTokenGeneraContrasenia))
            End If
            If Not IsNothing(iPaisVisualizacion) Then
                iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iPaisVisualizacion.id))

            Else
                iGeneradorSql.agregarValue("null")
            End If

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iTelefonoCelularCodigoArea))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iTelefonoCelularCaracteristica))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iTelefonoCelularNumero))

            iGeneradorSql.agregarValue(FuncionComun.booleanByte(iDobleFactor))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iToken))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingFechaHora(iFechaCaducidadToken))
            If Not IsNothing(iDashboard) Then
                iGeneradorSql.agregarValue(iDashboard.id)
            Else
                iGeneradorSql.agregarValue("null")
            End If
            If iUsuarioFront.HasValue Then
                iGeneradorSql.agregarValue(FuncionComun.booleanByte(iUsuarioFront))
            Else
                iGeneradorSql.agregarValue("null")
            End If

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

            iRol.accesoDatos = iConexion
            iRol.usuario = Me
            iRol.crearRoles()
            iRol.accesoDatos = Nothing

            crearUsuarioPerfiles()

            If Not IsNothing(iAccesosDirectos) Then
                For i = 0 To iAccesosDirectos.Count - 1
                    With iAccesosDirectos.Item(i)
                        .usuario = Me
                        .accesoDatos = iConexion
                        .crear()
                        .accesoDatos = Nothing
                    End With
                Next
            End If

            If Not IsNothing(iEntidades) Then
                For i = 0 To iEntidades.Count - 1
                    iGeneradorSql.agregarTabla("UsuarioEntidad")
                    iGeneradorSql.agregarColumna("idUsuario")
                    iGeneradorSql.agregarColumna("idEntidad")
                    iGeneradorSql.agregarColumna("idTipoEntidad")
                    iGeneradorSql.agregarValue(iId)
                    iGeneradorSql.agregarValue(iEntidades.Item(i).id)
                    If TypeOf (iEntidades.Item(i)) Is Nivel Then
                        iGeneradorSql.agregarValue(TipoEntidad.NIVEL)
                    Else
                        iGeneradorSql.agregarValue(TipoEntidad.COMERCIO)
                    End If
                    iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
                Next
            End If

            If Not IsNothing(iComerciosAsociados) Then
                For i = 0 To iComerciosAsociados.Count - 1
                    iGeneradorSql.agregarTabla("UsuarioComerciosAsociados")
                    iGeneradorSql.agregarColumna("idUsuario")
                    iGeneradorSql.agregarColumna("idComercio")
                    iGeneradorSql.agregarValue(iId)
                    iGeneradorSql.agregarValue(iComerciosAsociados.Item(i).id)
                    iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
                Next
            End If

        Catch exception As Exception
            Throw New UsuarioNoCreadoException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing

        End Try
    End Sub

    Private Sub validarModificar()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iMensaje As String

        Try
            iConexion = obtenerConexion()

            If Len(iPassword) > 0 Then
                If Not FuncionComun.esPasswordFuerte(iConexion, iPassword, iMensaje) Then
                    Throw New UsuarioNoModificadoException(iMensaje)
                End If
            End If

            If IsNothing(iPerfiles) AndAlso (iPerfiles.Count) = 0 Then
                Throw New UsuarioNoModificadoException("el usuario debe tener al menos un perfil asignado")
            End If

            iGeneradorSql.agregarColumna("login")
            iGeneradorSql.agregarTabla("Usuario")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not iDataReader.Read Then
                Throw New UsuarioNoModificadoException("El usuario a modificar no existe")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("login")
            iGeneradorSql.agregarTabla("Usuario")

            iGeneradorSql.agregarCondicionWhere("TelefonoCelularCodigoArea=" & FuncionComun.nuloSiEsNothing(iTelefonoCelularCodigoArea))
            iGeneradorSql.agregarCondicionWhere("TelefonoCelularCaracteristica=" & FuncionComun.nuloSiEsNothing(iTelefonoCelularCaracteristica))
            iGeneradorSql.agregarCondicionWhere("TelefonoCelularNumero=" & FuncionComun.nuloSiEsNothing(iTelefonoCelularNumero))
            iGeneradorSql.agregarCondicionWhere("id<>" & id)
            iGeneradorSql.agregarCondicionWhere("TelefonoCelularCodigoArea is not null")
            iGeneradorSql.agregarCondicionWhere("TelefonoCelularCaracteristica is not null")
            iGeneradorSql.agregarCondicionWhere("TelefonoCelularNumero is not null")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoModificadoException("Ya existe un usuario con ese telefono celular")
            End If
            iDataReader.Close()

            If iEstado.isBaja Then
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarTabla("comercio")
                iGeneradorSql.agregarCondicionWhere("idComercializador=" & iId)

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
                If iDataReader.Read Then
                    Throw New UsuarioNoEliminadoException("El usuario esta utilizado en un comercio como comercializador")
                End If
                iDataReader.Close()
            End If

        Catch excepcion As Exception
            Throw excepcion
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub validarModificarId()

        If Not IsNothing(usuarioAutorizacion) AndAlso usuarioAutorizacion.id = id Then
            Throw New UsuarioNoCreadoException("El usuario autorizacion es igual al usuario que se desea modificar")
        End If

        If Not IsNothing(usuarioSupervisorAgenda) AndAlso usuarioSupervisorAgenda.id = id Then
            Throw New UsuarioNoCreadoException("El usuario supervisor agenda es igual al usuario que se desea modificar")
        End If

    End Sub

    Public Function validaCambioDatos(ByVal eUsuario As Usuario) As Boolean
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iIdsListaPerfilesActuales As String

        Try

            For Each iPerfil As Perfil In iPerfiles
                iIdsListaPerfilesActuales &= iPerfil.id & ","
            Next
            iIdsListaPerfilesActuales = Left(iIdsListaPerfilesActuales, iIdsListaPerfilesActuales.Length - 1)

            '/// Valido si los datos se cambiaron o no.///
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTablaPrincipal("Usuario u")
            iGeneradorSql.agregarTablaConJoin("UsuarioPerfil up", "up.idUsuario=u.id")
            iGeneradorSql.agregarColumna("u.id")

            With eUsuario
                iGeneradorSql.agregarCondicionWhereConOr("u.idEstado<>" & .estado.id)
                iGeneradorSql.agregarCondicionWhereConOr("u.Login<>" & FuncionComun.nuloSiEsNothing(.login))
                iGeneradorSql.agregarCondicionWhereConOr("u.idestadoUsuario<>" & .estadoUsuario.id)
                iGeneradorSql.agregarCondicionWhereConOr("up.idPerfil not in(" & iIdsListaPerfilesActuales & ")")

                iGeneradorSql.agregarCondicionWhere(iGeneradorSql.generarWhereConOr)
            End With
            iGeneradorSql.agregarCondicionWhere("u.id= " & eUsuario.id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader.Read

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Overridable Sub modificar()
        Dim iGeneradorSql As New GeneradorSql
        Dim iAccesoDirecto As New AccesoDirecto
        Dim i As Integer

        Try

            iConexion = obtenerConexion()

            validarModificar()

            validarModificarId()

            iGeneradorSql.agregarTabla("Usuario")
            iGeneradorSql.agregarSet("Login=" & FuncionComun.nuloSiEsNothing(login))
            iGeneradorSql.agregarSet("Nombre=" & FuncionComun.nuloSiEsNothing(nombre))
            If password <> Nothing Then iGeneradorSql.agregarSetEncriptado("Password=" & password)
            iGeneradorSql.agregarSet("Mail=" & FuncionComun.nuloSiEsNothing(mail))
            iGeneradorSql.agregarSet("PedirCambioPassword=" & FuncionComun.booleanByte(pedirCambioPassword))
            iGeneradorSql.agregarSet("Administrativo=" & FuncionComun.booleanByte(administrativo.Value))
            iGeneradorSql.agregarSet("idEstado=" & IIf(iEstado.isAlta, Estado.ALTA, Estado.BAJA))
            iGeneradorSql.agregarSet("idNivel=" & FuncionComun.nuloSiEsNothing(iNivel.id))
            iGeneradorSql.agregarSet("idEstadoUsuario=" & FuncionComun.nuloSiEsNothing(estadoUsuario.id))
            If IsNothing(puntoVentaDgiDiferido) Then
                iGeneradorSql.agregarSet("idpuntoVentaDgiDiferido=null")
            Else
                iGeneradorSql.agregarSet("idpuntoVentaDgiDiferido=" & FuncionComun.nuloSiEsNothing(puntoVentaDgiDiferido.id))
            End If
            If IsNothing(puntoVentaDgiInmediatoPunitorios) Then
                iGeneradorSql.agregarSet("idpuntoVentaDgiInmediatoPunitorios=null")
            Else
                iGeneradorSql.agregarSet("idpuntoVentaDgiInmediatoPunitorios=" & FuncionComun.nuloSiEsNothing(puntoVentaDgiInmediatoPunitorios.id))
            End If
            If IsNothing(puntoVentaDgiInmediatoInteresesYGastos) Then
                iGeneradorSql.agregarSet("idpuntoVentaDgiInmediatoInteresesYGastos=null")
            Else
                iGeneradorSql.agregarSet("idpuntoVentaDgiInmediatoInteresesYGastos=" & FuncionComun.nuloSiEsNothing(puntoVentaDgiInmediatoInteresesYGastos.id))
            End If
            If IsNothing(iPuntoVentaDgiInmediatoNotaCredito) Then
                iGeneradorSql.agregarSet("idpuntoVentaDgiInmediatoNotaCredito=null")
            Else
                iGeneradorSql.agregarSet("idpuntoVentaDgiInmediatoNotaCredito=" & FuncionComun.nuloSiEsNothing(iPuntoVentaDgiInmediatoNotaCredito.id))
            End If
            If IsNothing(usuarioAutorizacion) Then
                iGeneradorSql.agregarSet("idusuarioAutorizacion=null")
            Else
                iGeneradorSql.agregarSet("idusuarioAutorizacion=" & FuncionComun.nuloSiEsNothing(usuarioAutorizacion.id))
            End If
            iGeneradorSql.agregarSet("usuarioComercio=" & FuncionComun.booleanByte(iUsuarioComercio.Value))
            iGeneradorSql.agregarSet("idComercioDefault=" & FuncionComun.nuloSiEsNothing(iIdComercioDefault))
            iGeneradorSql.agregarSet("idComercioDefaultPromocion=" & FuncionComun.nuloSiEsNothing(iIdComercioDefaultPromocion))
            iGeneradorSql.agregarSet("idEntidadCuentaCorrienteComercioDefault=" & FuncionComun.nuloSiEsNothing(iIdEntidadCuentaCorrienteComercioDefault))
            iGeneradorSql.agregarSet("lunesHoraLoguinDesde=" & FuncionComun.nuloSiEsNothingHora(iLunesHoraLoguinDesde))
            iGeneradorSql.agregarSet("lunesHoraLoguinHasta=" & FuncionComun.nuloSiEsNothingHora(iLunesHoraLoguinHasta))
            iGeneradorSql.agregarSet("martesHoraLoguinDesde=" & FuncionComun.nuloSiEsNothingHora(iMartesHoraLoguinDesde))
            iGeneradorSql.agregarSet("martesHoraLoguinHasta=" & FuncionComun.nuloSiEsNothingHora(iMartesHoraLoguinHasta))
            iGeneradorSql.agregarSet("miercolesHoraLoguinDesde=" & FuncionComun.nuloSiEsNothingHora(iMiercolesHoraLoguinDesde))
            iGeneradorSql.agregarSet("miercolesHoraLoguinHasta=" & FuncionComun.nuloSiEsNothingHora(iMiercolesHoraLoguinHasta))
            iGeneradorSql.agregarSet("juevesHoraLoguinDesde=" & FuncionComun.nuloSiEsNothingHora(iJuevesHoraLoguinDesde))
            iGeneradorSql.agregarSet("juevesHoraLoguinHasta=" & FuncionComun.nuloSiEsNothingHora(iJuevesHoraLoguinHasta))
            iGeneradorSql.agregarSet("viernesHoraLoguinDesde=" & FuncionComun.nuloSiEsNothingHora(iViernesHoraLoguinDesde))
            iGeneradorSql.agregarSet("viernesHoraLoguinHasta=" & FuncionComun.nuloSiEsNothingHora(iViernesHoraLoguinHasta))
            iGeneradorSql.agregarSet("sabadoHoraLoguinDesde=" & FuncionComun.nuloSiEsNothingHora(iSabadoHoraLoguinDesde))
            iGeneradorSql.agregarSet("sabadoHoraLoguinHasta=" & FuncionComun.nuloSiEsNothingHora(iSabadoHoraLoguinHasta))
            iGeneradorSql.agregarSet("domingoHoraLoguinDesde=" & FuncionComun.nuloSiEsNothingHora(iDomingoHoraLoguinDesde))
            iGeneradorSql.agregarSet("domingoHoraLoguinHasta=" & FuncionComun.nuloSiEsNothingHora(iDomingoHoraLoguinHasta))
            iGeneradorSql.agregarSet("idMedioAccesoSistema=" & iMedioAccesoSistema.id)
            iGeneradorSql.agregarSet("ipAcceso=" & FuncionComun.nuloSiEsNothing(iIpAcceso))
            iGeneradorSql.agregarSet("ipRestringida=" & FuncionComun.nuloSiEsNothing(iIpRestringida))
            If IsNothing(iTipoUsuarioTarea) Then
                iGeneradorSql.agregarSet("idTipoUsuarioTarea=null")
            Else
                iGeneradorSql.agregarSet("idTipoUsuarioTarea=" & FuncionComun.nuloSiEsNothing(iTipoUsuarioTarea.id))
            End If
            iGeneradorSql.agregarSet("trabajaConAgenda=" & FuncionComun.booleanByte(iTrabajaConAgenda.Value))
            If IsNothing(usuarioSupervisorAgenda) Then
                iGeneradorSql.agregarSet("idUsuarioSupervisorAgenda=null")
            Else
                iGeneradorSql.agregarSet("idUsuarioSupervisorAgenda=" & iUsuarioSupervisorAgenda.id)
            End If
            iGeneradorSql.agregarSet("supervisorAgenda=" & FuncionComun.booleanByte(iSupervisorAgenda.Value))
            iGeneradorSql.agregarSet("autorizaSolicitud=" & FuncionComun.booleanByte(iAutorizaSolicitud.Value))
            iGeneradorSql.agregarSet("codigoOperadorCentralTelefonica=" & FuncionComun.nuloSiEsNothing(codigoOperadorCentralTelefonica))
            iGeneradorSql.agregarSet("codigoColaCentralTelefonica=" & FuncionComun.nuloSiEsNothing(codigoColaCentralTelefonica))
            If IsNothing(sucursalPuntos) Then
                iGeneradorSql.agregarSet("idsucursalPuntos=null")
            Else
                iGeneradorSql.agregarSet("idsucursalPuntos=" & FuncionComun.nuloSiEsNothing(sucursalPuntos.id))
            End If
            If IsNothing(sectorAutorizacion) Then
                iGeneradorSql.agregarSet("idsectorAutorizacion=null")
            Else
                iGeneradorSql.agregarSet("idsectorAutorizacion=" & FuncionComun.nuloSiEsNothing(sectorAutorizacion.id))
            End If
            iGeneradorSql.agregarSet("idEstudioDefault=" & FuncionComun.nuloSiEsNothing(iIdEstudioDefault))
            iGeneradorSql.agregarSet("idVendedorDefault=" & FuncionComun.nuloSiEsNothing(iIdVendedorDefault))
            iGeneradorSql.agregarSet("controlaRemito=" & FuncionComun.booleanByte(iControlaRemito.Value))
            iGeneradorSql.agregarSet("bandejaWelcome=" & FuncionComun.booleanByte(iBandejaWelcome.Value))
            iGeneradorSql.agregarSet("idpoliticacomercial=" & FuncionComun.nuloSiEsNothing(iIdPoliticaComercial))
            iGeneradorSql.agregarSet("idFiltrosWorkflow=" & FuncionComun.nuloSiEsNothing(iIdFiltrosWorkflow))
            iGeneradorSql.agregarSet("generaContrasenia=" & FuncionComun.booleanByte(iGeneraContrasenia))
            iGeneradorSql.agregarSet("fechaVencimientoGeneraContrasenia=" & FuncionComun.nuloSiEsNothing(iFechaVencimientoGeneraContrasenia))
            iGeneradorSql.agregarSet("tokenGeneraContrasenia=" & FuncionComun.nuloSiEsNothing(iTokenGeneraContrasenia))

            If IsNothing(estadoTokenGeneraContrasenia) Then
                iGeneradorSql.agregarSet("idEstadoTokenGeneraContrasenia=null")
            Else
                iGeneradorSql.agregarSet("idEstadoTokenGeneraContrasenia=" & IIf(iEstadoTokenGeneraContrasenia.isAlta, Estado.ALTA, Estado.BAJA))
            End If

            iGeneradorSql.agregarSet("idPaisVisualizacion=" & iPaisVisualizacion.id)

            iGeneradorSql.agregarSet("TelefonoCelularCodigoArea=" & FuncionComun.nuloSiEsNothing(iTelefonoCelularCodigoArea))
            iGeneradorSql.agregarSet("TelefonoCelularCaracteristica=" & FuncionComun.nuloSiEsNothing(iTelefonoCelularCaracteristica))
            iGeneradorSql.agregarSet("TelefonoCelularNumero=" & FuncionComun.nuloSiEsNothing(iTelefonoCelularNumero))

            iGeneradorSql.agregarSet("DobleFactor=" & FuncionComun.booleanByte(iDobleFactor))
            iGeneradorSql.agregarSet("Token=" & FuncionComun.nuloSiEsNothing(iToken))
            iGeneradorSql.agregarSet("FechaCaducidadToken=" & FuncionComun.nuloSiEsNothingFechaHora(iFechaCaducidadToken))
            If IsNothing(iDashboard) Then
                iGeneradorSql.agregarSet("idDashboard=null")
            Else
                iGeneradorSql.agregarSet("idDashboard=" & FuncionComun.nuloSiEsNothing(iDashboard.id))
            End If
            iGeneradorSql.agregarSet("usuarioFront=" & FuncionComun.booleanByte(iUsuarioFront.Value))

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

            eliminarUsuarioPerfiles()
            crearUsuarioPerfiles()

            If Not IsNothing(iEntidades) Then
                iGeneradorSql.agregarTabla("UsuarioEntidad")
                iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)
                iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

                For i = 0 To iEntidades.Count - 1
                    iGeneradorSql.agregarTabla("UsuarioEntidad")
                    iGeneradorSql.agregarColumna("idUsuario")
                    iGeneradorSql.agregarColumna("idEntidad")
                    iGeneradorSql.agregarColumna("idTipoEntidad")
                    iGeneradorSql.agregarValue(iId)
                    iGeneradorSql.agregarValue(iEntidades.Item(i).id)
                    If TypeOf (iEntidades.Item(i)) Is Nivel Then
                        iGeneradorSql.agregarValue(TipoEntidad.NIVEL)
                    Else
                        iGeneradorSql.agregarValue(TipoEntidad.COMERCIO)
                    End If
                    iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
                Next
            End If

            If Not IsNothing(iComerciosAsociados) Then
                iGeneradorSql.agregarTabla("UsuarioComerciosAsociados")
                iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)
                iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

                For i = 0 To iComerciosAsociados.Count - 1
                    iGeneradorSql.agregarTabla("UsuarioComerciosAsociados")
                    iGeneradorSql.agregarColumna("idUsuario")
                    iGeneradorSql.agregarColumna("idComercio")
                    iGeneradorSql.agregarValue(iId)
                    iGeneradorSql.agregarValue(iComerciosAsociados.Item(i).id)
                    iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
                Next
            End If

        Catch exception As Exception
            Throw New UsuarioNoModificadoException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub validarModificarAccesosDirectos()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("login")
            iGeneradorSql.agregarTabla("Usuario")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not iDataReader.Read Then
                Throw New UsuarioNoCreadoException("El usuario a modificar no existe")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw excepcion
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub modificarAccesosDirectos()
        Dim iGeneradorSql As New GeneradorSql
        Dim iAccesoDirecto As New AccesoDirecto
        Dim i As Integer

        Try

            iConexion = obtenerConexion()

            validarModificarAccesosDirectos()

            iAccesoDirecto.accesoDatos = iConexion
            iAccesoDirecto.eliminarAccesosDirectosPorUsuario(Me)
            iAccesoDirecto.accesoDatos = Nothing

            If Not IsNothing(iAccesosDirectos) Then
                For i = 0 To iAccesosDirectos.Count - 1
                    With iAccesosDirectos.Item(i)
                        .usuario = Me
                        .accesoDatos = iConexion
                        .crear()
                        .accesoDatos = Nothing
                    End With
                Next
            End If

        Catch exception As Exception
            Throw New UsuarioNoModificadoException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Overridable Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Dim iRol As New Rol
        Dim iAccesoDirecto As New AccesoDirecto
        Dim iLogEntidadUsuario As New LogEntidadUsuario
        Dim iLog As New Log
        Try
            iConexion = obtenerConexion()

            validarEliminar()

            iGeneradorSql.agregarTabla("UsuarioAutogestion")
            iGeneradorSql.agregarCondicionWhere("id=" & iId)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iGeneradorSql.agregarTabla("UsuarioEntidad")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iGeneradorSql.agregarTabla("UsuarioComerciosAsociados")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iGeneradorSql.agregarTabla("logusuario")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iGeneradorSql.agregarTabla("logparametriacomercio")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            With iRol
                .accesoDatos = iConexion
                .usuario = Me
                .eliminarRoles()
                .accesoDatos = Nothing
            End With

            With iLogEntidadUsuario
                .accesoDatos = iConexion
                .usuario = Me
                .eliminarLogsPorUsuario()
                .accesoDatos = Nothing
            End With
            With iLog
                .accesoDatos = iConexion
                .usuario = Me
                .eliminarLogsPorUsuario()
                .accesoDatos = Nothing
            End With

            With iAccesoDirecto
                .accesoDatos = iConexion
                .eliminarAccesosDirectosPorUsuario(Me)
                .accesoDatos = Nothing
            End With

            eliminarUsuarioPerfiles()

            iGeneradorSql.agregarTabla("Usuario")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)


        Catch exception As Exception
            Throw New UsuarioNoEliminadoException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iAccesoDirecto = Nothing
            iRol = Nothing
        End Try
    End Sub

    Private Sub validarEliminar()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("Autorizacion")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por una autorizacion")
            End If
            iDataReader.Close()


            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("impresionweb")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por una impresionweb")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("logentidadusuario")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por un logentidadusuario")
            End If
            iDataReader.Close()


            iGeneradorSql.agregarColumna("idEstudio")
            iGeneradorSql.agregarTabla("estudiooperador")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por un estudio")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("tramite")
            iGeneradorSql.agregarCondicionWhere("idUsuarioCreador=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por un tramite")
            End If
            iDataReader.Close()


            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("tramite")
            iGeneradorSql.agregarCondicionWhere("idUsuarioResponsable=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por un tramite")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("intranetArchivo")
            iGeneradorSql.agregarCondicionWhere("idUsuarioCreador=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por un archivo de intranet")
            End If
            iDataReader.Close()


            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("intranetCartelera")
            iGeneradorSql.agregarCondicionWhere("idUsuarioCreador=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por una cartelera de intranet")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("smsGrupo")
            iGeneradorSql.agregarCondicionWhere("idUsuarioCreador=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por un sms grupo")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("solicitudPendiente")
            iGeneradorSql.agregarCondicionWhere("idUsuarioAutorizador=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por una solicitud pendiente")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("solicitudPendienteArchivo")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por un archivo de solicitud pendiente")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("solicitudPendienteLog")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por un log de solicitud pendiente")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("consultariesgonethtml")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por una consulta riesgonet html")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("consultariesgonet")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por una consulta riesgonet")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("consultasiisa")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por una consulta siisa")
            End If
            iDataReader.Close()


            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("consultaVeraz")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por una consulta veraz")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("usuario")
            iGeneradorSql.agregarCondicionWhere("idUsuarioSupervisorAgenda=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado como supervisor de agenda")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarTabla("historialDomicilio")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado en un historial de domicilio")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarTabla("historiallaboral")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado en un historial laboral")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarTabla("historialpersona")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado en un historial persona")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarTabla("historialreferencia")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado en un historial referencia")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("comercio")
            iGeneradorSql.agregarCondicionWhere("idComercializador=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado en un comercio como comercializador")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("comercio")
            iGeneradorSql.agregarCondicionWhere("idActivador=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado en un comercio como activador")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("ModeloAutogestion")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoEliminadoException("El usuario esta utilizado por un modelo autogestión")
            End If
            iDataReader.Close()


        Catch excepcion As Exception
            Throw New UsuarioNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub validarContraseñaNueva()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iMensaje As String

        Try


            iConexion = obtenerConexion()

            If Not FuncionComun.esPasswordFuerte(iConexion, iPassword, iMensaje) Then
                Throw New UsuarioNoModificadoException(iMensaje)
            End If

            normalizarCantidadRepeticionesContraseniaUsuario(iConexion, id)

            'iGeneradorSql.agregarTabla("usuario")
            'iGeneradorSql.agregarColumna("id")
            'iGeneradorSql.agregarCondicionWhere("id=" & id)
            'iGeneradorSql.agregarCondicionWhere("ultimaContrasenia4=" & FuncionComun.sqlMD5(FuncionComun.nuloSiEsNothing(password)) & " or ultimaContrasenia3=" & FuncionComun.sqlMD5(FuncionComun.nuloSiEsNothing(password)) & " or ultimaContrasenia2=" & FuncionComun.sqlMD5(FuncionComun.nuloSiEsNothing(password)) & " or ultimaContrasenia1=" & FuncionComun.sqlMD5(FuncionComun.nuloSiEsNothing(password)) & "", True)
            'iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & id)
            iGeneradorSql.agregarCondicionWhere("ultimaContrasenia=" & FuncionComun.nuloSiEsNothing(password))
            iGeneradorSql.agregarTabla("usuarioContrasenia")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UsuarioNoModificadoException("verifique la contraseña, ya fue utilizada, no puede volver a utilizarla")
            End If
            iDataReader.Close()
        Catch exception As Exception
            Throw exception
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Shared Sub normalizarCantidadRepeticionesContraseniaUsuario(eAccesoDatos As accesoDatos, eIdUsuario As Long)
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iConexion As accesoDatos
        Dim iCantidadExistente As Integer
        Dim iCantidad As Integer
        Dim i As Integer

        Try

            If IsNothing(eAccesoDatos) Then
                iConexion = New accesoDatos
            Else
                iConexion = eAccesoDatos
            End If

            'Obtengo los paramtros
            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarTabla("Parametro")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & "'CantidadSinRepetirContrasenia'")
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                iCantidad = iDataReader.Item("valor").ToString()
            End If
            iDataReader.Close()

            'Borro las contrasenias que son mayores a las del parametro
            iGeneradorSql.agregarTabla("usuarioContrasenia")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & eIdUsuario)
            iGeneradorSql.agregarCondicionWhere("numero >" & iCantidad)
            iConexion.ejecutar(iGeneradorSql.generarDelete)

            'Verifico si tengo que agregar datos en usuario contrasenia
            iGeneradorSql.agregarColumna("count(id) as cantidad")
            iGeneradorSql.agregarTabla("usuarioContrasenia")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & eIdUsuario)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                iCantidadExistente = iDataReader.Item("cantidad").ToString()
            End If
            iDataReader.Close()

            If iCantidad > iCantidadExistente Then
                iCantidadExistente += 1
                For i = iCantidadExistente To iCantidad
                    iGeneradorSql.agregarTabla("usuarioContrasenia")

                    iGeneradorSql.agregarColumna("idUsuario")
                    iGeneradorSql.agregarColumna("numero")
                    iGeneradorSql.agregarColumna("ultimaContrasenia")

                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(eIdUsuario))
                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(i))
                    iGeneradorSql.agregarValue("null")

                    iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
                Next
            End If

        Catch exception As Exception
        Finally
            If IsNothing(eAccesoDatos) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader.Close()
        End Try
    End Sub


    Public Sub cambiarPassword()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarContraseñaNueva()

            iGeneradorSql.agregarTabla("Usuario")
            iGeneradorSql.agregarSetEncriptado("Password=" & password)
            iGeneradorSql.agregarSet("pedirCambioPassword=" & FuncionComun.booleanByte(pedirCambioPassword))
            iGeneradorSql.agregarSet("idestadoUsuario=" & EstadoUsuario.HABILITADO)
            iGeneradorSql.agregarSet("fechaUltimoCambioContrasenia=" & FuncionComun.nuloSiEsNothing(Format(Now.Date, "yyyy/MM/dd")))
            iGeneradorSql.agregarSet("ultimaContrasenia4=ultimaContrasenia3", True)
            iGeneradorSql.agregarSet("ultimaContrasenia3=ultimaContrasenia2", True)
            iGeneradorSql.agregarSet("ultimaContrasenia2=ultimaContrasenia1", True)
            iGeneradorSql.agregarSetEncriptado("ultimaContrasenia1=" & password)

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

            actualizarUltimasContrasenias()

        Catch exception As Exception
            Throw exception
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub actualizarUltimasContrasenias()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataSet As DataSet
        Dim iUltimaContrasenia As String
        Dim iPrimerContrasenia As Boolean

        Try

            iConexion = obtenerConexion()

            'Obtengo las ultimas contraseñas del usuario
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarColumna("numero")
            iGeneradorSql.agregarColumna("ultimaContrasenia")
            iGeneradorSql.agregarTabla("usuarioContrasenia")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & id)
            iGeneradorSql.agregarOrden("numero")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "usuario")

            'Actualizo las en base a las ultimas contraseñas
            For Each iDataRow As DataRow In iDataSet.Tables("usuario").Rows

                iGeneradorSql.agregarTabla("usuarioContrasenia")
                If Not iPrimerContrasenia Then
                    iGeneradorSql.agregarSet("ultimaContrasenia=" & FuncionComun.nuloSiEsNothing(password))
                    iPrimerContrasenia = True
                Else
                    iGeneradorSql.agregarSet("ultimaContrasenia=" & FuncionComun.nuloSiEsNothing(iUltimaContrasenia))
                End If
                iGeneradorSql.agregarCondicionWhere("id=" & iDataRow.Item("id").ToString())

                iConexion.ejecutar(iGeneradorSql.generarUpdate)

                iUltimaContrasenia = iDataRow.Item("ultimaContrasenia").ToString()

            Next

        Catch exception As Exception
            Throw exception
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iDataSet = Nothing
        End Try
    End Sub


    Public Sub deshabilitarUsuario()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Usuario")
            iGeneradorSql.agregarSet("intentosIngresoPassword=0")
            iGeneradorSql.agregarSet("idestadoUsuario=" & EstadoUsuario.BLOQUEADO)
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw exception
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub bajaUsuario()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Usuario")
            iGeneradorSql.agregarSet("intentosIngresoPassword=0")
            iGeneradorSql.agregarSet("idestado=" & Estado.BAJA)
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw exception
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub vaciarCantidadIntentosIngreso()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()


            iGeneradorSql.agregarTabla("Usuario")
            iGeneradorSql.agregarSet("intentosIngresoPassword=0")
            iGeneradorSql.agregarCondicionWhere("id=" & iId)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw exception
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub sumarCantidadIntentosIngresoPasswordPorLogin()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataSet As DataSet

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Usuario")

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("intentosIngresoPassword")
            iGeneradorSql.agregarCondicionWhere("login=" & FuncionComun.nuloSiEsNothing(iLogin))

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Usuario")

            If iDataSet.Tables("Usuario").Rows.Count > 0 Then

                iId = iDataSet.Tables("Usuario").Rows(0).Item("id")
                iIntentosIngresoPassword = FuncionComun.ceroSiEsVacio(iDataSet.Tables("Usuario").Rows(0).Item("IntentosIngresoPassword").ToString) + 1

                iGeneradorSql.agregarTabla("Usuario")
                iGeneradorSql.agregarSet("intentosIngresoPassword=" & iIntentosIngresoPassword)
                iGeneradorSql.agregarCondicionWhere("id=" & iId)

                iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)
            End If

        Catch exception As Exception
            Throw exception
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iDataSet = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Overridable Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iPuntoVentaDgiInmediatoPunitorios) Then
                iPuntoVentaDgiInmediatoPunitorios.dispose()
            End If
            If Not IsNothing(iPuntoVentaDgiInmediatoInteresesYGastos) Then
                iPuntoVentaDgiInmediatoInteresesYGastos.dispose()
            End If
            If Not IsNothing(iPuntoVentaDgiInmediatoNotaCredito) Then
                iPuntoVentaDgiInmediatoNotaCredito.dispose()
            End If
            If Not IsNothing(iPuntoVentaDgiDiferido) Then
                iPuntoVentaDgiDiferido.dispose()
            End If
            If Not IsNothing(iNivel) Then
                iNivel.dispose()
            End If
            If Not IsNothing(iSectorAutorizacion) Then
                iSectorAutorizacion.dispose()
            End If

        Catch exception As Exception
            Throw New RootException(exception)
        End Try

    End Sub

    Public Function obtenerUsuariosReporte(ByVal eUsuariosReporteVO As UsuariosReporteVO) As String
        Dim iArchivoExcel As StreamWriter
        Dim iGeneradorSql As New GeneradorSql
        Dim iPathArchivo, iPathArchivoZipeado As String
        Dim iDataSet As DataSet
        Dim iLinea, iNiveles As String
        Dim i As Integer

        Dim iColeccion As New Collection

        Try

            iPathArchivo = ConfigurationManager.AppSettings("archivosGenerados") & "UsuariosReporte" & Format(Now, "ddMMyyyy") & Format(Now, "hhmm") & ".xls"
            iPathArchivoZipeado = ConfigurationManager.AppSettings("archivosGenerados") & "UsuariosReporte" & Format(Now, "ddMMyyyy") & Format(Now, "hhmm") & ".zip"

            iConexion = obtenerConexion()

            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("u.id")
            iGeneradorSql.agregarColumna("u.nombre")
            iGeneradorSql.agregarColumna("u.login")
            iGeneradorSql.agregarColumna("e.Descripcion as estado")
            iGeneradorSql.agregarColumna("n.Descripcion as nivel")
            iGeneradorSql.agregarColumna("group_concat(p.nombre) as perfil")

            iGeneradorSql.agregarTablaPrincipal("usuario u")
            iGeneradorSql.agregarTablaConJoin("UsuarioPerfil up", "up.idusuario=u.id", True)
            iGeneradorSql.agregarTablaConJoin("Perfil p", "p.id=up.idPerfil", True)
            iGeneradorSql.agregarTablaConJoin("Estado e", "e.id=u.idEstado")
            iGeneradorSql.agregarTablaConJoin("Nivel n", "n.id=u.idNivel")

            With eUsuariosReporteVO
                If Not IsNothing(.perfil) Then iGeneradorSql.agregarCondicionWhere("up.idPerfil=" & .perfil.id)

                If Not IsNothing(.nivel) AndAlso .nivel.id <> Nothing Then
                    If .nivel.isGrupoEmpresas Then
                        iNiveles = "select id from GrupoEmpresas where id=" & .nivel.id & " union  select id from empresaGrupo where idGrupoEmpresas=" & .nivel.id & " union select id from unidadDeNegocios where idEmpresaGrupo in (select id from empresaGrupo where idGrupoEmpresas=" & .nivel.id & ") union select id from sucursal where idUnidadDeNegocios in (select id from unidadDeNegocios where idEmpresaGrupo in (select id from empresaGrupo where idGrupoEmpresas=" & .nivel.id & "))"
                        iGeneradorSql.agregarCondicionWhere("u.idNivel in (" & iNiveles & ")", True)
                    ElseIf .nivel.isEmpresaGrupo Then
                        iNiveles = "select id from empresaGrupo where id=" & .nivel.id & " union select id from unidadDeNegocios where idEmpresaGrupo=" & .nivel.id & " union select id from sucursal where idUnidadDeNegocios in (select id from unidadDeNegocios where idEmpresaGrupo=" & .nivel.id & ")"
                        iGeneradorSql.agregarCondicionWhere("u.idNivel in (" & iNiveles & ")", True)
                    ElseIf .nivel.isUnidadDeNegocios Then
                        iNiveles = "select id from unidadDeNegocios where id=" & .nivel.id & " union select id from sucursal where idUnidadDeNegocios=" & .nivel.id
                        iGeneradorSql.agregarCondicionWhere("u.idNivel in (" & iNiveles & ")", True)
                    ElseIf .nivel.isSucursal Then
                        iGeneradorSql.agregarCondicionWhere("u.idNivel=" & .nivel.id)
                    End If
                End If
                If Not IsNothing(.estado) Then iGeneradorSql.agregarCondicionWhere("u.idEstado=" & .estado.id)
                If .nombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("u.nombre Like '%" & .nombre & "%'")
                If .login <> Nothing Then iGeneradorSql.agregarCondicionWhere("u.login like '%" & .login & "%'")
            End With

            iGeneradorSql.agregarOrden("u.nombre asc")
            iGeneradorSql.agregarGroupBy("u.id")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "UsuariosReportes")

            iArchivoExcel = New StreamWriter(iPathArchivo)

            'Agrego el encabezado
            iLinea = "LOGIN" & vbTab & "NOMBRE" & vbTab & "ESTADO" & vbTab & "NIVEL" & vbTab & "PERFIL" & vbTab
            iArchivoExcel.WriteLine(iLinea)

            For i = 0 To iDataSet.Tables("UsuariosReportes").Rows.Count - 1
                With iDataSet.Tables("UsuariosReportes").Rows(i)
                    iLinea = Trim(.Item("login").ToString.ToUpper) & vbTab
                    iLinea &= Trim(.Item("nombre").ToString.ToUpper) & vbTab
                    iLinea &= Trim(.Item("estado").ToString.ToUpper) & vbTab
                    iLinea &= Trim(.Item("nivel").ToString.ToUpper) & vbTab
                    iLinea &= Trim(.Item("perfil").ToString.ToUpper) & vbTab
                End With
                iArchivoExcel.WriteLine(iLinea)
            Next i

            iArchivoExcel.Close()

            iColeccion.Add(iPathArchivo)
            FuncionComun.zipearArchivos(iColeccion, iPathArchivoZipeado)

            Return iPathArchivoZipeado

        Catch exception As Exception
            Throw New RolNoEncontradoException
        Finally
            If Not IsNothing(iArchivoExcel) Then iArchivoExcel.Close()
            iArchivoExcel = Nothing
            If File.Exists(iPathArchivo) Then File.Delete(iPathArchivo)
            iDataSet = Nothing
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerUsuariosMail() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("mail")
            iGeneradorSql.agregarTabla("usuario")
            iGeneradorSql.agregarCondicionWhere("idEstado=" & Estado.ALTA)
            iGeneradorSql.agregarCondicionWhere("mail is not null")
            If Not IsNothing(iAutorizaSolicitud) Then iGeneradorSql.agregarCondicionWhere("autorizaSolicitud=" & FuncionComun.booleanByte(iAutorizaSolicitud.Value))

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "UsuariosMail")

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerIdEntidadCuentaCorrientePorUsuarioComercioAsignado(eIdComercio As Long) As Long
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("UsuarioEntidad ue")
            iGeneradorSql.agregarTabla("Usuario u")
            iGeneradorSql.agregarColumna("u.idEntidadCuentaCorrienteComercioDefault")
            iGeneradorSql.agregarCondicionWhere("ue.idUsuario=u.id")
            iGeneradorSql.agregarCondicionWhere("idEntidad=" & eIdComercio)
            iGeneradorSql.agregarCondicionWhere("idTipoEntidad=" & TipoEntidad.COMERCIO)
            iGeneradorSql.agregarCondicionWhere("u.idEntidadCuentaCorrienteComercioDefault is not null")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Return FuncionComun.ceroSiEsNulo(iDataReader.Item("idEntidadCuentaCorrienteComercioDefault"))
            End If

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Sub activarUsuario()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Usuario")

            iGeneradorSql.agregarSet("idEstado=" & Estado.ALTA)
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New UsuarioNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub modificarToken()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Usuario")

            iGeneradorSql.agregarSet("Token=" & FuncionComun.nuloSiEsNothing(iToken))
            iGeneradorSql.agregarSet("FechaCaducidadToken=" & FuncionComun.nuloSiEsNothingFechaHora(iFechaCaducidadToken))

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New UsuarioNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub modificarMailFotoPerfil()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Usuario")

            iGeneradorSql.agregarSet("Mail=" & FuncionComun.nuloSiEsNothing(iMail))
            iGeneradorSql.agregarSet("FotoPerfil=" & FuncionComun.nuloSiEsNothing(iFotoPerfil))

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New UsuarioNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

#Region "Genera Contrasenia"
    Public Sub modificarSoloGeneraContrasenia()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("usuario")
            iGeneradorSql.agregarSet("generaContrasenia=" & FuncionComun.booleanByte(generaContrasenia))
            iGeneradorSql.agregarSet("fechaVencimientoGeneraContrasenia=" & FuncionComun.nuloSiEsNothing(fechaVencimientoGeneraContrasenia))
            iGeneradorSql.agregarSet("tokenGeneraContrasenia=" & FuncionComun.nuloSiEsNothing(tokenGeneraContrasenia))
            iGeneradorSql.agregarSet("idEstadoTokenGeneraContrasenia=" & IIf(iEstadoTokenGeneraContrasenia.isAlta, Estado.ALTA, Estado.BAJA))

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch ex As Exception
            Throw New UsuarioNoModificadoException(ex)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Public Sub cambiarPasswordUsuarioGeneraContrasenia()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarContraseñaNueva()

            iGeneradorSql.agregarTabla("Usuario")

            iGeneradorSql.agregarSet("idEstadoUsuario=" & EstadoUsuario.HABILITADO)
            iGeneradorSql.agregarSetEncriptado("password=" & password)
            iGeneradorSql.agregarSet("pedirCambioPassword=" & FuncionComun.booleanByte(pedirCambioPassword))
            iGeneradorSql.agregarSet("fechaUltimoCambioContrasenia=" & FuncionComun.nuloSiEsNothing(Format(Now.Date, "yyyy/MM/dd")))
            iGeneradorSql.agregarSet("ultimaContrasenia4=ultimaContrasenia3", True)
            iGeneradorSql.agregarSet("ultimaContrasenia3=ultimaContrasenia2", True)
            iGeneradorSql.agregarSet("ultimaContrasenia2=ultimaContrasenia1", True)
            iGeneradorSql.agregarSetEncriptado("ultimaContrasenia1=" & password)

            ' Limpio genera contraseña
            iGeneradorSql.agregarSet("generaContrasenia=" & FuncionComun.booleanByte(False))
            iGeneradorSql.agregarSet("idEstadoTokenGeneraContrasenia=" & Estado.BAJA)
            'iGeneradorSql.agregarSet("fechaVencimientoGeneraContrasenia=NULL", True)
            'iGeneradorSql.agregarSet("tokenGeneraContrasenia=NULL", True)

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

            actualizarUltimasContrasenias()


        Catch ex As Exception
            Throw ex
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Public Shared Function esPasswordFuerte(eAccesoDatos As accesoDatos, ePassword As String, ByRef eMensaje As String) As Boolean
        Dim iGeneradorSql As New GeneradorSql
        Dim iConexion As accesoDatos
        Dim iDataSet As DataSet

        Dim letterCount As Integer = 0
        Dim numbreCount As Integer = 0
        Dim symbolCount As Integer = 0
        Dim lowerCount As Integer = 0
        Dim upperCount As Integer = 0

        Dim iIncluirCaracteresEspecialesContrasenia As Boolean
        Dim iUsarMayusculasMinusculasConstasenia As Boolean
        Dim iIncluirNumerosContrasenia As Boolean
        Dim iLargoMinimoContrasenia As Integer
        Dim iSuperaValidacion As Boolean

        Try

            If IsNothing(eAccesoDatos) Then
                iConexion = New accesoDatos
            Else
                iConexion = eAccesoDatos
            End If

            'Obtengo los paramtros
            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarTabla("Parametro")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & "'LargoMinimoContrasenia'")
            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")
            If iDataSet.Tables("Parametros").Rows.Count > 0 Then
                iLargoMinimoContrasenia = FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametros").Rows.Item(0).Item("valor").ToString())
            End If

            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarTabla("Parametro")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & "'UsarMayusculasMinusculasConstasenia'")
            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")
            If iDataSet.Tables("Parametros").Rows.Count > 0 Then
                iUsarMayusculasMinusculasConstasenia = FuncionComun.byteBoolean(FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametros").Rows.Item(0).Item("valor").ToString()))
            End If

            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarTabla("Parametro")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & "'IncluirNumerosContrasenia'")
            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")
            If iDataSet.Tables("Parametros").Rows.Count > 0 Then
                iIncluirNumerosContrasenia = FuncionComun.byteBoolean(FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametros").Rows.Item(0).Item("valor").ToString()))
            End If

            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarTabla("Parametro")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & "'IncluirCaracteresEspecialesContrasenia'")
            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")
            If iDataSet.Tables("Parametros").Rows.Count > 0 Then
                iIncluirCaracteresEspecialesContrasenia = FuncionComun.byteBoolean(FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametros").Rows.Item(0).Item("valor").ToString()))
            End If

            iSuperaValidacion = True

            For i As Integer = 0 To ePassword.Length - 1
                If [Char].IsLower(ePassword(i)) Then
                    lowerCount += 1
                ElseIf [Char].IsUpper(ePassword(i)) Then
                    upperCount += 1
                ElseIf [Char].IsLetter(ePassword(i)) Then
                    letterCount += 1
                ElseIf [Char].IsNumber(ePassword(i)) Then
                    numbreCount += 1
                Else
                    symbolCount += 1
                End If
            Next

            If iLargoMinimoContrasenia > 0 AndAlso iLargoMinimoContrasenia > ePassword.Length Then iSuperaValidacion = False
            If iSuperaValidacion AndAlso iUsarMayusculasMinusculasConstasenia AndAlso (lowerCount = 0 OrElse upperCount = 0) Then iSuperaValidacion = False
            If iSuperaValidacion AndAlso iIncluirNumerosContrasenia AndAlso numbreCount = 0 Then iSuperaValidacion = False
            If iSuperaValidacion AndAlso iIncluirCaracteresEspecialesContrasenia AndAlso symbolCount = 0 Then iSuperaValidacion = False

            If Not iSuperaValidacion Then
                eMensaje = "<p>La contraseña debe respetar los siguientes lineamientos"
                eMensaje &= "<ul>"

                If iLargoMinimoContrasenia > 0 Then eMensaje &= "<li>Deber contener como minimo " & iLargoMinimoContrasenia & " caracteres " & IIf(iLargoMinimoContrasenia > ePassword.Length, "FALLO", "PASO") & "</li>"
                If iUsarMayusculasMinusculasConstasenia Then eMensaje &= "<li>Deber contener mayusculas y minusculas " & IIf(lowerCount = 0 OrElse upperCount = 0, "FALLO", "PASO") & "</li>"
                If iIncluirNumerosContrasenia Then eMensaje &= "<li>Deber contener por lo menos un numero " & IIf(numbreCount = 0, "FALLO", "PASO") & "</li>"
                If iIncluirCaracteresEspecialesContrasenia Then eMensaje &= "<li>Deber contener por lo menos un simbolo " & IIf(symbolCount = 0, "FALLO", "PASO") & "</li>"

                eMensaje &= "</ul>"
                eMensaje &= "</p>"
            End If

            Return iSuperaValidacion

        Catch exception As Exception
            Return False
        Finally
            If IsNothing(eAccesoDatos) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iDataSet = Nothing
        End Try
    End Function


#End Region

#Region "Perfiles"
    Public Function obtenerUsuarioPerfil() As List(Of Perfil)
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataSet As DataSet
        Dim iPerfil As Perfil
        Dim iListaPerfiles As List(Of Perfil)

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("idPerfil")
            iGeneradorSql.agregarTabla("UsuarioPerfil")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & id, False)

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Perfiles")

            iListaPerfiles = New List(Of Perfil)

            For Each iDataRow As DataRow In iDataSet.Tables("Perfiles").Rows
                iPerfil = New Perfil
                With iPerfil
                    .id = iDataRow.Item("idPerfil")
                    .accesoDatos = iConexion
                    iPerfil = .obtenerPerfil
                    .accesoDatos = Nothing
                End With
                iListaPerfiles.Add(iPerfil)
            Next

            Return iListaPerfiles

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iDataSet = Nothing
            iPerfil = Nothing
            iListaPerfiles = Nothing
        End Try
    End Function

    Public Sub crearUsuarioPerfiles()
        Dim iGeneradorSql As New GeneradorSql
        Dim i As Integer

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId)
            iGeneradorSql.agregarTabla("UsuarioPerfil")
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            For i = 0 To iPerfiles.Count - 1
                iGeneradorSql.agregarColumna("idPerfil")
                iGeneradorSql.agregarColumna("idUsuario")
                iGeneradorSql.agregarValue(iPerfiles(i).id, False)
                iGeneradorSql.agregarValue(iId, False)

                iGeneradorSql.agregarTabla("UsuarioPerfil")

                iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            Next

        Catch excepcion As Exception
            Throw New UsuarioNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub eliminarUsuarioPerfiles()
        Dim iGeneradorSql As New GeneradorSql

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iId, False)
            iGeneradorSql.agregarTabla("UsuarioPerfil")
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New UsuarioNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub
#End Region

#End Region

End Class
