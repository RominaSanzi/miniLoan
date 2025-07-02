Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades

Public Class Accion

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iNombre As String
    Private iPaginaAsociada As String
    Private iGrabaLog As New Boolean?()
    Private iEsSubmenu As Boolean

    Private iConexion As accesoDatos
#End Region

#Region "Atributos"
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
    Public Property paginaAsociada() As String
        Get
            Return iPaginaAsociada
        End Get
        Set(ByVal Value As String)
            iPaginaAsociada = Value
        End Set
    End Property
    Public Property grabaLog() As Boolean?
        Get
            Return iGrabaLog
        End Get
        Set(ByVal Value As Boolean?)
            iGrabaLog = Value
        End Set
    End Property
    Public Property esSubMenu() As Boolean
        Get
            Return iEsSubmenu
        End Get
        Set(ByVal Value As Boolean)
            iEsSubmenu = Value
        End Set
    End Property
#End Region

#Region "Métodos"

    Public Function obtenerAccion() As Accion
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Accion")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("paginaAsociada")


            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id")
                iNombre = iDataReader.Item("Nombre").ToString
                iPaginaAsociada = iDataReader.Item("PaginaAsociada").ToString

                iDataReader.Close()
                Return Me
            Else
                Throw New AccionNoEncontradaException
            End If

        Catch excepcion As Exception
            Throw New AccionNoEncontradaException(excepcion)
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

    Public Function obtenerAcciones() As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("paginaAsociada")

            If Not IsNothing(grabaLog) Then iGeneradorSql.agregarCondicionWhere("grabaLog=" & FuncionComun.booleanByte(grabaLog.Value))

            iGeneradorSql.agregarOrden("nombre asc")

            iGeneradorSql.agregarTabla("accion")

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New AccionNoEncontradaException(excepcion)
        Finally
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Private Sub validarCrear()

        If nombre = Nothing Then
            Throw New AccionNoCreadaException("El nombre no puede ser nulo")
        End If

        If paginaAsociada = Nothing Then
            Throw New AccionNoCreadaException("La pagina asociada no puede ser nula")
        End If

        If paginaAsociada = Nothing Then
            Throw New AccionNoCreadaException("La pagina asociada no puede ser nula")
        End If

    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("accion")

            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("paginaAsociada")
            iGeneradorSql.agregarColumna("grabaLog")
            iGeneradorSql.agregarColumna("esSubMenu")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(nombre))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(paginaAsociada))
            If Not IsNothing(grabaLog) Then
                iGeneradorSql.agregarValue(FuncionComun.booleanByte(grabaLog.Value))
            Else
                iGeneradorSql.agregarValue(FuncionComun.booleanByte(False))
            End If
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(esSubMenu))

            If id <> Nothing Then
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(id))
                iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            Else
                id = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            End If

        Catch excepcion As Exception
            Throw New AccionNoCreadaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Private Sub validarEliminar()

        If nombre = Nothing Then
            Throw New AccionNoEliminadaException("La accion a eliminar no puede ser nula")
        End If


        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("Menu")
            iGeneradorSql.agregarCondicionWhere("idAccion=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New AccionNoEliminadaException("No se puede eliminar la acción porque está relacionada con un menu")
            End If
        Catch excepcion As Exception
            Throw New AccionNoEliminadaException(excepcion)
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

            iGeneradorSql.agregarTabla("accion")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New AccionNoEliminadaException(excepcion)
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

    Private Sub validarModificar()

        If nombre = Nothing Then
            Throw New AccionNoModificadaException("El nombre no puede ser nulo")
        End If

        If paginaAsociada = Nothing Then
            Throw New AccionNoModificadaException("La pagina asociada no puede ser nula")
        End If

        If id = Nothing Then
            Throw New AccionNoModificadaException("La accion a modificar no puede ser nula")
        End If

    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarTabla("accion")

            iGeneradorSql.agregarSet("nombre=" & FuncionComun.nuloSiEsNothing(nombre))
            iGeneradorSql.agregarSet("PaginaAsociada=" & FuncionComun.nuloSiEsNothing(iPaginaAsociada))
            iGeneradorSql.agregarSet("grabaLog=" & FuncionComun.booleanByte(grabaLog.Value))
            iGeneradorSql.agregarSet("esSubMenu=" & FuncionComun.booleanByte(esSubMenu))

            iGeneradorSql.agregarCondicionWhere("id = " & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New AccionNoModificadaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub
    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

        Catch exception As exception
            Throw New RootException(exception)
        End Try

    End Sub

#End Region

End Class
