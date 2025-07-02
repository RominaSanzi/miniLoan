Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.IO
Imports System.Configuration
Imports System.Collections.Generic
Imports di.financiera.seguridad

Public Class UsuarioAutogestion

    Inherits Usuario

#Region "Variables"
    Private iIdPersona As Long
    Private iCodigoRecuperoContrasenia As String
    '=================================
    'Datos de facebook
    '=================================
    Private iIdFacebook As Long
    Private iEmailFacebook As String
    Private iUrlFotoPerfilFacebook As String
    Private iNombreFacebook As String
    Private iPaginaDireccionar As String
    Private iCBUValidado As Boolean
    Private iIdentidadValidada As Boolean
    Private iCelularValidado As Boolean
    Private iMailValidado As Boolean
    '=================================
    'Datos de google
    '=================================
    Private iIdGoogle As String
    Private iNombreGoogle As String
    Private iEmailGoogle As String

    Private iConexion As accesoDatos
#End Region

#Region "Atributos"
    Public Property idPersona() As Long
        Get
            Return iIdPersona
        End Get
        Set(ByVal Value As Long)
            iIdPersona = Value
        End Set
    End Property
    Public Property nombreFacebook() As String
        Get
            Return iNombreFacebook
        End Get
        Set(ByVal Value As String)
            iNombreFacebook = Value
        End Set
    End Property
    Public Property idFacebook() As Long
        Get
            Return iIdFacebook
        End Get
        Set(ByVal Value As Long)
            iIdFacebook = Value
        End Set
    End Property
    Public Property emailFacebook() As String
        Get
            Return iEmailFacebook
        End Get
        Set(ByVal Value As String)
            iEmailFacebook = Value
        End Set
    End Property
    Public Property urlFotoPerfilFacebook() As String
        Get
            Return iUrlFotoPerfilFacebook
        End Get
        Set(ByVal Value As String)
            iUrlFotoPerfilFacebook = Value
        End Set
    End Property
    Public Property codigoRecuperoContrasenia() As String
        Get
            Return iCodigoRecuperoContrasenia
        End Get
        Set(ByVal Value As String)
            iCodigoRecuperoContrasenia = Value
        End Set
    End Property

    Public Property paginaDireccionar() As String
        Get
            Return iPaginaDireccionar
        End Get
        Set(ByVal Value As String)
            iPaginaDireccionar = Value
        End Set
    End Property
    Public Property CBUValidado() As Boolean
        Get
            Return iCBUValidado
        End Get
        Set(ByVal Value As Boolean)
            iCBUValidado = Value
        End Set
    End Property
    Public Property identidadValidada() As Boolean
        Get
            Return iIdentidadValidada
        End Get
        Set(ByVal Value As Boolean)
            iIdentidadValidada = Value
        End Set
    End Property
    Public Property celularValidado() As Boolean
        Get
            Return iCelularValidado
        End Get
        Set(ByVal Value As Boolean)
            iCelularValidado = Value
        End Set
    End Property

    Public Property idGoogle As String
        Get
            Return iIdGoogle
        End Get
        Set(value As String)
            iIdGoogle = value
        End Set
    End Property

    Public Property nombreGoogle As String
        Get
            Return iNombreGoogle
        End Get
        Set(value As String)
            iNombreGoogle = value
        End Set
    End Property

    Public Property emailGoogle As String
        Get
            Return iEmailGoogle
        End Get
        Set(value As String)
            iEmailGoogle = value
        End Set
    End Property

    Public Property mailValidado As Boolean
        Get
            Return iMailValidado
        End Get
        Set(value As Boolean)
            iMailValidado = value
        End Set
    End Property
#End Region

#Region "Metodos"

    Private Sub validarCrear()

        Try

            If idPersona = Nothing Then
                Throw New UsuarioNoCreadoException("La persona no puede ser nula")
            End If

        Catch excepcion As Exception
            Throw New UsuarioNoCreadoException(excepcion)
        End Try
    End Sub

    Public Overrides Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            MyBase.accesoDatos = iConexion
            MyBase.crear()

            iGeneradorSql.agregarTabla("UsuarioAutogestion")

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombreFacebook")
            iGeneradorSql.agregarColumna("idFacebook")
            iGeneradorSql.agregarColumna("emailFacebook")
            iGeneradorSql.agregarColumna("idPersona")
            iGeneradorSql.agregarColumna("CodigoRecuperoContrasenia")
            iGeneradorSql.agregarColumna("UrlFotoPerfilFacebook")
            iGeneradorSql.agregarColumna("paginaDireccionar")
            iGeneradorSql.agregarColumna("CBUValidado")
            iGeneradorSql.agregarColumna("IdentidadValidada")
            iGeneradorSql.agregarColumna("CelularValidado")
            iGeneradorSql.agregarColumna("idGoogle")
            iGeneradorSql.agregarColumna("nombreGoogle")
            iGeneradorSql.agregarColumna("emailGoogle")
            iGeneradorSql.agregarColumna("mailValidado")


            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(id))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(nombreFacebook))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(idFacebook))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(emailFacebook))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(idPersona))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(codigoRecuperoContrasenia))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(urlFotoPerfilFacebook))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(paginaDireccionar))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(CBUValidado))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(identidadValidada))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(celularValidado))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(idGoogle))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(nombreGoogle))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(emailGoogle))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(iMailValidado))

            iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

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

    Private Sub validarModificar()
        Try


        Catch excepcion As Exception
            Throw New UsuarioNoModificadoException(excepcion)
        End Try
    End Sub

    Public Overrides Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarModificar()

            MyBase.accesoDatos = iConexion
            MyBase.modificar()

            iGeneradorSql.agregarSet("nombreFacebook=" & FuncionComun.nuloSiEsNothing(nombreFacebook))
            iGeneradorSql.agregarSet("idFacebook=" & FuncionComun.nuloSiEsNothing(idFacebook))
            iGeneradorSql.agregarSet("emailFacebook=" & FuncionComun.nuloSiEsNothing(emailFacebook))
            iGeneradorSql.agregarSet("CodigoRecuperoContrasenia=" & FuncionComun.nuloSiEsNothing(codigoRecuperoContrasenia))
            iGeneradorSql.agregarSet("UrlFotoPerfilFacebook=" & FuncionComun.nuloSiEsNothing(urlFotoPerfilFacebook))
            iGeneradorSql.agregarSet("paginaDireccionar=" & FuncionComun.nuloSiEsNothing(paginaDireccionar))
            iGeneradorSql.agregarSet("CBUValidado=" & FuncionComun.booleanByte(CBUValidado))
            iGeneradorSql.agregarSet("identidadValidada=" & FuncionComun.booleanByte(identidadValidada))
            iGeneradorSql.agregarSet("celularValidado=" & FuncionComun.booleanByte(celularValidado))
            iGeneradorSql.agregarSet("idGoogle=" & FuncionComun.nuloSiEsNothing(idGoogle))
            iGeneradorSql.agregarSet("nombreGoogle=" & FuncionComun.nuloSiEsNothing(nombreGoogle))
            iGeneradorSql.agregarSet("emailGoogle=" & FuncionComun.nuloSiEsNothing(emailGoogle))
            iGeneradorSql.agregarSet("mailValidado=" & FuncionComun.booleanByte(iMailValidado))


            iGeneradorSql.agregarTabla("UsuarioAutogestion")
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

    Public Sub modificarValidacionMail()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarSet("ua.mailValidado=" & FuncionComun.booleanByte(iMailValidado))

            iGeneradorSql.agregarTablaPrincipal("UsuarioAutogestion ua ")
            iGeneradorSql.agregarTablaConJoin("Usuario u", "ua.id=u.id")

            iGeneradorSql.agregarCondicionWhere("u.login=" & FuncionComun.nuloSiEsNothing(mail))

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

    Public Overrides Function obtenerUsuarioPorNombreYPass() As Usuario
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            MyBase.accesoDatos = iConexion
            MyBase.obtenerUsuarioPorNombreYPass()

            iGeneradorSql.agregarColumna("nombreFacebook")
            iGeneradorSql.agregarColumna("idFacebook")
            iGeneradorSql.agregarColumna("emailFacebook")
            iGeneradorSql.agregarColumna("idPersona")
            iGeneradorSql.agregarColumna("CodigoRecuperoContrasenia")
            iGeneradorSql.agregarColumna("UrlFotoPerfilFacebook")
            iGeneradorSql.agregarColumna("paginaDireccionar")
            iGeneradorSql.agregarColumna("CBUValidado")
            iGeneradorSql.agregarColumna("IdentidadValidada")
            iGeneradorSql.agregarColumna("CelularValidado")
            iGeneradorSql.agregarColumna("idGoogle")
            iGeneradorSql.agregarColumna("nombreGoogle")
            iGeneradorSql.agregarColumna("emailGoogle")
            iGeneradorSql.agregarColumna("mailValidado")


            iGeneradorSql.agregarTabla("UsuarioAutogestion")

            iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iNombreFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("NombreFacebook"))
                iIdFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("Idfacebook"))
                iEmailFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("EmailFacebook"))
                iIdPersona = FuncionComun.nothingSiEsNulo(iDataReader.Item("idPersona"))

                iCodigoRecuperoContrasenia = FuncionComun.nothingSiEsNulo(iDataReader.Item("CodigoRecuperoContrasenia"))
                iUrlFotoPerfilFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("UrlFotoPerfilFacebook"))
                iPaginaDireccionar = FuncionComun.nothingSiEsNulo(iDataReader.Item("paginaDireccionar"))
                iCBUValidado = FuncionComun.byteBoolean(iDataReader.Item("CBUValidado"))
                iIdentidadValidada = FuncionComun.byteBoolean(iDataReader.Item("identidadValidada"))
                iCelularValidado = FuncionComun.byteBoolean(iDataReader.Item("celularValidado"))

                iIdGoogle = FuncionComun.nothingSiEsNulo(iDataReader.Item("idGoogle"))
                iNombreGoogle = FuncionComun.nothingSiEsNulo(iDataReader.Item("nombreGoogle"))
                iEmailGoogle = FuncionComun.nothingSiEsNulo(iDataReader.Item("emailGoogle"))
                iMailValidado = FuncionComun.byteBoolean(iDataReader.Item("mailValidado"))

                iDataReader.Close()

                Return Me
            Else
                Throw New UsuarioNoEncontradoException()
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

    Public Overrides Function obtenerUsuarioSoloIds(Optional eObtenerNivel As Boolean = False) As Usuario
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            MyBase.accesoDatos = iConexion
            MyBase.obtenerUsuarioSoloIds(eObtenerNivel:=eObtenerNivel)

            iGeneradorSql.agregarColumna("nombreFacebook")
            iGeneradorSql.agregarColumna("idFacebook")
            iGeneradorSql.agregarColumna("emailFacebook")
            iGeneradorSql.agregarColumna("idPersona")
            iGeneradorSql.agregarColumna("CodigoRecuperoContrasenia")
            iGeneradorSql.agregarColumna("UrlFotoPerfilFacebook")

            iGeneradorSql.agregarColumna("paginaDireccionar")
            iGeneradorSql.agregarColumna("CBUValidado")
            iGeneradorSql.agregarColumna("IdentidadValidada")
            iGeneradorSql.agregarColumna("CelularValidado")
            iGeneradorSql.agregarColumna("idGoogle")
            iGeneradorSql.agregarColumna("nombreGoogle")
            iGeneradorSql.agregarColumna("emailGoogle")
            iGeneradorSql.agregarColumna("mailValidado")

            iGeneradorSql.agregarTabla("UsuarioAutogestion")

            iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iNombreFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("nombreFacebook"))
                iIdFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("Idfacebook"))
                iEmailFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("EmailFacebook"))
                iIdPersona = FuncionComun.nothingSiEsNulo(iDataReader.Item("idPersona"))
                iCodigoRecuperoContrasenia = FuncionComun.nothingSiEsNulo(iDataReader.Item("CodigoRecuperoContrasenia"))
                iUrlFotoPerfilFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("UrlFotoPerfilFacebook"))

                iPaginaDireccionar = FuncionComun.nothingSiEsNulo(iDataReader.Item("paginaDireccionar"))
                iCBUValidado = FuncionComun.byteBoolean(iDataReader.Item("CBUValidado"))
                iIdentidadValidada = FuncionComun.byteBoolean(iDataReader.Item("identidadValidada"))
                iCelularValidado = FuncionComun.byteBoolean(iDataReader.Item("celularValidado"))

                iIdGoogle = FuncionComun.nothingSiEsNulo(iDataReader.Item("idGoogle"))
                iNombreGoogle = FuncionComun.nothingSiEsNulo(iDataReader.Item("nombreGoogle"))
                iEmailGoogle = FuncionComun.nothingSiEsNulo(iDataReader.Item("emailGoogle"))
                iMailValidado = FuncionComun.byteBoolean(iDataReader.Item("mailValidado"))

                iDataReader.Close()

                Return Me
            Else
                Throw New UsuarioNoEncontradoException()
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

    Public Overrides Function obtenerUsuarioFacebook(ByVal eIdFacebook As Long) As Usuario
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            MyBase.accesoDatos = iConexion
            MyBase.obtenerUsuarioFacebook(eIdFacebook)

            iGeneradorSql.agregarColumna("nombreFacebook")
            iGeneradorSql.agregarColumna("idFacebook")
            iGeneradorSql.agregarColumna("emailFacebook")
            iGeneradorSql.agregarColumna("idPersona")
            iGeneradorSql.agregarColumna("CodigoRecuperoContrasenia")
            iGeneradorSql.agregarColumna("UrlFotoPerfilFacebook")

            iGeneradorSql.agregarColumna("paginaDireccionar")
            iGeneradorSql.agregarColumna("CBUValidado")
            iGeneradorSql.agregarColumna("IdentidadValidada")
            iGeneradorSql.agregarColumna("CelularValidado")
            iGeneradorSql.agregarColumna("idGoogle")
            iGeneradorSql.agregarColumna("nombreGoogle")
            iGeneradorSql.agregarColumna("emailGoogle")
            iGeneradorSql.agregarColumna("mailValidado")

            iGeneradorSql.agregarTabla("UsuarioAutogestion")

            iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iNombreFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("nombreFacebook"))
                iIdFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("Idfacebook"))
                iEmailFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("EmailFacebook"))
                iIdPersona = FuncionComun.nothingSiEsNulo(iDataReader.Item("idPersona"))
                iCodigoRecuperoContrasenia = FuncionComun.nothingSiEsNulo(iDataReader.Item("CodigoRecuperoContrasenia"))
                iUrlFotoPerfilFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("UrlFotoPerfilFacebook"))
                iPaginaDireccionar = FuncionComun.nothingSiEsNulo(iDataReader.Item("paginaDireccionar"))
                iCBUValidado = FuncionComun.byteBoolean(iDataReader.Item("CBUValidado"))
                iIdentidadValidada = FuncionComun.byteBoolean(iDataReader.Item("identidadValidada"))
                iCelularValidado = FuncionComun.byteBoolean(iDataReader.Item("celularValidado"))
                iIdGoogle = FuncionComun.nothingSiEsNulo(iDataReader.Item("idGoogle"))
                iNombreGoogle = FuncionComun.nothingSiEsNulo(iDataReader.Item("nombreGoogle"))
                iEmailGoogle = FuncionComun.nothingSiEsNulo(iDataReader.Item("emailGoogle"))
                iMailValidado = FuncionComun.byteBoolean(iDataReader.Item("mailValidado"))

                iDataReader.Close()

                Return Me
            Else
                Throw New UsuarioNoEncontradoException()
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

    Public Overrides Function obtenerUsuarioGoogle(ByVal eIdGoogle As String) As Usuario
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            MyBase.accesoDatos = iConexion
            MyBase.obtenerUsuarioGoogle(eIdGoogle)

            iGeneradorSql.agregarColumna("nombreFacebook")
            iGeneradorSql.agregarColumna("idFacebook")
            iGeneradorSql.agregarColumna("emailFacebook")
            iGeneradorSql.agregarColumna("idPersona")
            iGeneradorSql.agregarColumna("CodigoRecuperoContrasenia")
            iGeneradorSql.agregarColumna("UrlFotoPerfilFacebook")

            iGeneradorSql.agregarColumna("paginaDireccionar")
            iGeneradorSql.agregarColumna("CBUValidado")
            iGeneradorSql.agregarColumna("IdentidadValidada")
            iGeneradorSql.agregarColumna("CelularValidado")
            iGeneradorSql.agregarColumna("idGoogle")
            iGeneradorSql.agregarColumna("nombreGoogle")
            iGeneradorSql.agregarColumna("emailGoogle")
            iGeneradorSql.agregarColumna("mailValidado")

            iGeneradorSql.agregarTabla("UsuarioAutogestion")

            iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iNombreFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("nombreFacebook"))
                iIdFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("Idfacebook"))
                iEmailFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("EmailFacebook"))
                iIdPersona = FuncionComun.nothingSiEsNulo(iDataReader.Item("idPersona"))
                iCodigoRecuperoContrasenia = FuncionComun.nothingSiEsNulo(iDataReader.Item("CodigoRecuperoContrasenia"))
                iUrlFotoPerfilFacebook = FuncionComun.nothingSiEsNulo(iDataReader.Item("UrlFotoPerfilFacebook"))
                iPaginaDireccionar = FuncionComun.nothingSiEsNulo(iDataReader.Item("paginaDireccionar"))
                iCBUValidado = FuncionComun.byteBoolean(iDataReader.Item("CBUValidado"))
                iIdentidadValidada = FuncionComun.byteBoolean(iDataReader.Item("identidadValidada"))
                iCelularValidado = FuncionComun.byteBoolean(iDataReader.Item("celularValidado"))
                iIdGoogle = FuncionComun.nothingSiEsNulo(iDataReader.Item("idGoogle"))
                iNombreGoogle = FuncionComun.nothingSiEsNulo(iDataReader.Item("nombreGoogle"))
                iEmailGoogle = FuncionComun.nothingSiEsNulo(iDataReader.Item("emailGoogle"))
                iMailValidado = FuncionComun.byteBoolean(iDataReader.Item("mailValidado"))

                iDataReader.Close()

                Return Me
            Else
                Throw New UsuarioNoEncontradoException()
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

    Public Overrides Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Try

            iConexion = obtenerConexion()

            MyBase.accesoDatos = iConexion
            MyBase.eliminar()

            iGeneradorSql.agregarTabla("UsuarioAutogestion")
            iGeneradorSql.agregarCondicionWhere("id=" & id)
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


    Private Sub validarmodificarPaginaDireccionar()

        Try

            If id = Nothing Then
                Throw New UsuarioNoModificadoException("El identificador del usuario no puede ser nulo")
            End If

        Catch excepcion As Exception
            Throw New UsuarioNoModificadoException(excepcion)
        End Try
    End Sub

    Public Sub modificarPaginaDireccionar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarmodificarPaginaDireccionar()

            iGeneradorSql.agregarSet("paginaDireccionar=" & FuncionComun.nuloSiEsNothing(paginaDireccionar))

            iGeneradorSql.agregarTabla("UsuarioAutogestion")
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

    Private Sub validarmodificarCelularValidado()

        Try

            If id = Nothing Then
                Throw New UsuarioNoModificadoException("El identificador del usuario no puede ser nulo")
            End If

        Catch excepcion As Exception
            Throw New UsuarioNoModificadoException(excepcion)
        End Try
    End Sub

    Public Sub modificarCelularValidado()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarmodificarCelularValidado()

            iGeneradorSql.agregarSet("celularValidado=" & FuncionComun.booleanByte(celularValidado))

            iGeneradorSql.agregarTabla("UsuarioAutogestion")
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

    Private Sub validarmodificarCBUValidado()
        Try
            If id = Nothing Then
                Throw New UsuarioNoModificadoException("El identificador del usuario no puede ser nulo")
            End If

        Catch excepcion As Exception
            Throw New UsuarioNoModificadoException(excepcion)
        End Try
    End Sub

    Public Sub modificarCBUValidado()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarmodificarCBUValidado()

            iGeneradorSql.agregarSet("CBUValidado=" & FuncionComun.booleanByte(CBUValidado))

            iGeneradorSql.agregarTabla("UsuarioAutogestion")
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

    Private Sub validarmodificarIdentidadValidada()

        Try

            If id = Nothing Then
                Throw New UsuarioNoModificadoException("El identificador del usuario no puede ser nulo")
            End If

        Catch excepcion As Exception
            Throw New UsuarioNoModificadoException(excepcion)
        End Try
    End Sub

    Public Sub modificarIdentidadValidada()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarmodificarIdentidadValidada()

            iGeneradorSql.agregarSet("IdentidadValidada=" & FuncionComun.booleanByte(identidadValidada))

            iGeneradorSql.agregarTabla("UsuarioAutogestion")
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

#End Region

End Class