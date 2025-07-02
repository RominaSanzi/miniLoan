Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.entidades
Imports di.financiera.utils

Public MustInherit Class Menu
    Inherits Entidad

#Region "Enumerado"
    Public Enum EnumTipoSistema
        LOAN = 1
        LOANCOMERCIO = 2
    End Enum
#End Region

#Region "Variables"
    Private iId As Long
    Private iNombre As String
    Private iIdPadre As Long
    Private iNivel As String
    Private Shared iIdRaiz As Long
    Private iPalabraClave As String
    Private iIcono As String
    Private iTooltip As String
    Private iAccion As Accion
    Private iFinal As Boolean

    Private iConexion As accesoDatos
#End Region

#Region "Atributos"
    Public Property nombre() As String
        Get
            Return iNombre
        End Get
        Set(ByVal Value As String)
            iNombre = Value
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
    Public Property idPadre() As Long
        Get
            Return iIdPadre
        End Get
        Set(ByVal Value As Long)
            iIdPadre = Value
        End Set
    End Property
    Public Shared Property idRaiz() As Long
        Get
            Return iIdRaiz
        End Get
        Set(ByVal Value As Long)
            iIdRaiz = Value
        End Set
    End Property
    Public Property nivel() As String
        Get
            Return iNivel
        End Get
        Set(ByVal Value As String)
            iNivel = Value
        End Set
    End Property
    Public Property palabraClave() As String
        Get
            Return iPalabraClave
        End Get
        Set(ByVal Value As String)
            iPalabraClave = Value
        End Set
    End Property
    Public Property icono() As String
        Get
            Return iIcono
        End Get
        Set(ByVal Value As String)
            iIcono = Value
        End Set
    End Property

    Public Property tooltip As String
        Get
            Return iTooltip
        End Get
        Set(value As String)
            iTooltip = value
        End Set
    End Property
    Public Property accion() As Accion
        Get
            Return iAccion
        End Get
        Set(ByVal Value As Accion)
            iAccion = Value
        End Set
    End Property
    Public Property final() As Boolean
        Get
            Return iFinal
        End Get
        Set(ByVal Value As Boolean)
            iFinal = Value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public MustOverride Function isMenuSimple() As Boolean
    Public MustOverride Function isMenuCompuesto() As Boolean
    Public MustOverride Function isHijoDeRaiz() As Boolean

    Public Function obtenerMenu() As Menu
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("menu")

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("Nombre")
            iGeneradorSql.agregarColumna("IdPadre")
            iGeneradorSql.agregarColumna("nivel")
            iGeneradorSql.agregarColumna("icono")
            iGeneradorSql.agregarColumna("idAccion")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id")
                iNombre = iDataReader.Item("Nombre").ToString
                iNivel = iDataReader.Item("nivel").ToString
                iIdPadre = FuncionComun.ceroSiEsVacio(iDataReader.Item("IdPadre").ToString)
                iIcono = iDataReader.Item("icono").ToString

                If Len(iDataReader.Item("idAccion").ToString) > 0 Then
                    iAccion = New Accion
                    iAccion.id = iDataReader.Item("idAccion")
                End If

                iDataReader.Close()

                If Not IsNothing(iAccion) Then
                    iAccion.accesoDatos = iConexion
                    iAccion = iAccion.obtenerAccion
                    iAccion.accesoDatos = Nothing
                End If

                Return Me
            Else
                Throw New MenuNoEncontradoException
            End If

        Catch excepcion As Exception
            Throw New MenuNoEncontradoException(excepcion)
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

    Private Sub validarEliminar()

        If nombre = Nothing Then
            Throw New MenuNoEliminadoException("El menu a eliminar no puede ser nulo")
        End If


        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("idPerfil")
            iGeneradorSql.agregarTabla("perfilMenu")
            iGeneradorSql.agregarCondicionWhere("idMenu=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New MenuNoEliminadoException("No se puede eliminar el menu porque está asignado a un perfil. Primero debe desasignar el menú a todos los perfiles que lo tengan habilitado.")
            End If
        Catch excepcion As Exception
            Throw New MenuNoEliminadoException(excepcion)
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

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            validarEliminar()

            iGeneradorSql.agregarTabla("menu")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New MenuNoEliminadoException(excepcion)
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

    Public Shared Sub crearConStored(ByVal eConexion As accesoDatos, ByVal eScript As String)
        Dim iStored As String

        Try

            iStored = "DROP PROCEDURE IF EXISTS crearMenu;
                       CREATE PROCEDURE crearMenu()
                       BEGIN" & vbNewLine

            iStored &= eScript

            iStored &= vbNewLine & "END;"


            eConexion.ejecutar(iStored)

            eConexion.ejecutarStoredProcedure("crearMenu", Nothing)

        Catch exception As Exception
            Throw New MenuNoCreadoException()
        Finally
        End Try

    End Sub

    Private Sub validarCrear()

        If nombre = Nothing Then
            Throw New MenuNoCreadoException("El nombre no puede ser nulo")
        End If

    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("Menu")

            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("final")
            iGeneradorSql.agregarColumna("IdPadre")
            iGeneradorSql.agregarColumna("IdAccion")
            iGeneradorSql.agregarColumna("icono")
            iGeneradorSql.agregarColumna("idEstado")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(nombre))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(final))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(idPadre))
            If Not IsNothing(accion) Then
                iGeneradorSql.agregarValue(accion.id)
            Else
                iGeneradorSql.agregarValue("null")
            End If
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(icono))
            iGeneradorSql.agregarValue(Estado.ALTA)

            If id <> Nothing Then
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(id))
                iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            Else
                id = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            End If

        Catch excepcion As Exception
            Throw New MenuNoCreadoException(excepcion)
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

        If nombre = Nothing Then
            Throw New MenuNoModificadoException("El nombre no puede ser nulo")
        End If

        If id = Nothing Then
            Throw New MenuNoModificadoException("El menu a modificar no puede ser nulo")
        End If

    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarTabla("Menu")

            iGeneradorSql.agregarSet("nombre=" & FuncionComun.nuloSiEsNothing(nombre))
            iGeneradorSql.agregarSet("final=" & FuncionComun.booleanByte(final))
            iGeneradorSql.agregarSet("IdPadre=" & FuncionComun.nuloSiEsNothing(idPadre))
            iGeneradorSql.agregarSet("icono=" & FuncionComun.nuloSiEsNothing(icono))
            If Not IsNothing(iAccion) Then
                iGeneradorSql.agregarSet("IdAccion=" & FuncionComun.nuloSiEsNothing(iAccion.id))
            Else
                iGeneradorSql.agregarSet("IdAccion=null")
            End If

            iGeneradorSql.agregarCondicionWhere("id = " & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New MenuNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub
    Public Overridable Sub dispose()
        Try
            If Not IsNothing(iAccion) Then
                iAccion.dispose()
            End If

        Catch exception As Exception
            Throw New RootException(exception)
        End Try

    End Sub
#End Region

End Class
