Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades

Public Class SectorAutorizacion
    Inherits Entidad

#Region "Constantes"
    Public Const CIAC As Integer = 3
#End Region

#Region "Variables"
    Private iId As Long
    Private iCodigo As Integer
    Private iDescripcion As String
    Private iEstado As Estado
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

    Public Property descripcion() As String
        Get
            Return iDescripcion
        End Get
        Set(ByVal Value As String)
            iDescripcion = Value
        End Set
    End Property

    Public Property codigo() As Integer
        Get
            Return iCodigo
        End Get
        Set(ByVal Value As Integer)
            iCodigo = Value
        End Set
    End Property

    Public Property estado() As Estado
        Get
            Return iEstado
        End Get
        Set(ByVal Value As estado)
            iEstado = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerSectorAutorizacion() As SectorAutorizacion
        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql()
        Dim iFuncionComun As New FuncionComun()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("idEstado")

            iGeneradorSql.agregarTabla("SectorAutorizacion")

            if id <> nothing then iGeneradorSql.agregarCondicionWhere("id=" & id)
            if codigo <> nothing then iGeneradorSql.agregarCondicionWhere("codigo=" & codigo)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iCodigo = iDataReader.Item("codigo").ToString
                iDescripcion = iDataReader.Item("descripcion").ToString
                iEstado = IIf(iDataReader.Item("idEstado").ToString = Estado.ALTA, New Alta, New Baja)

                Return Me
            Else
                Throw New RootException()
            End If

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Function obtenerSectoresAutorizacion(Optional ByVal eEstado As Estado = Nothing) As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("idEstado")
            iGeneradorSql.agregarTabla("SectorAutorizacion")

            If Not IsNothing(eEstado) Then iGeneradorSql.agregarCondicionWhere("idEstado=" & eEstado.id)
            If iCodigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigo=" & FuncionComun.nuloSiEsNothing(iCodigo))

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Function obtenerSectoresAutorizacionGrilla() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("idEstado")

            iGeneradorSql.agregarTabla("SectorAutorizacion")

            If (codigo <> Nothing) Then iGeneradorSql.agregarCondicionWhere("codigo=" & codigo)
            If (descripcion <> Nothing OrElse descripcion <> "") Then iGeneradorSql.agregarCondicionWhere("descripcion like '%" & descripcion & "%'")

            iGeneradorSql.agregarOrden("codigo asc")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "SectorAutorizacion")

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Private Sub validarCrear()

        If codigo = Nothing Then
            Throw New RootException("El código no puede ser nulo")
        End If

        If iDescripcion = Nothing Then
            Throw New RootException("La descripción no puede ser nula")
        End If

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("SectorAutorizacion")
            iGeneradorSql.agregarCondicionWhere("codigo=" & iCodigo)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New RootException("el sector autorización ya existe en la base de datos")
            End If

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

    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("SectorAutorizacion")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("idEstado")

            iGeneradorSql.agregarValue(codigo)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(descripcion))
            iGeneradorSql.agregarValue(estado.ALTA)

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

            obtenerUltimoId()

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub obtenerUltimoId()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarTabla("SectorAutorizacion")
            iGeneradorSql.agregarColumna("max(id) as id")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                iId = CLng(iDataReader.Item("id").ToString)
            Else
                Throw New RootException
            End If

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
    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarTabla("SectorAutorizacion")
            iGeneradorSql.agregarSet("codigo=" & codigo)
            iGeneradorSql.agregarSet("idEstado=" & iEstado.id)
            iGeneradorSql.agregarSet("descripcion=" & FuncionComun.nuloSiEsNothing(descripcion))
            iGeneradorSql.agregarCondicionWhere("id = " & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RootException(excepcion)
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
        If id = Nothing Then
            Throw New RootException("El sector autorización es inexistente")
        End If

        If codigo = Nothing Then
            Throw New RootException("El código no puede ser nulo")
        End If

        If iDescripcion = Nothing Then
            Throw New RootException("La descripción no puede ser nula")
        End If

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("SectorAutorizacion")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not iDataReader.Read Then
                Throw New RootException("El sector autorización es inexistente")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("SectorAutorizacion")
            iGeneradorSql.agregarCondicionWhere("id<>" & id)
            iGeneradorSql.agregarCondicionWhere("Codigo=" & iCodigo)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New RootException("Ya existe un sector autorización con el código ingresado")
            End If
            iDataReader.Close()

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

    End Sub

    Private Sub validarEliminar()

        If id = Nothing Then
            Throw New RootException("El sector autorización es inexistente")
        End If

        Dim iConexion As accesoDatos = obtenerConexion()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader

        Try
            iGeneradorSql.agregarColumna("idSectorAutorizacion")
            iGeneradorSql.agregarTabla("usuario")
            iGeneradorSql.agregarCondicionWhere("idSectorAutorizacion=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New RootException("No se puede eliminar el sector autorización porque se encuentra relacionado con un usuario")
            End If
            iDataReader.Close()

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

    End Sub

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarEliminar()

            iGeneradorSql.agregarTabla("SectorAutorizacion")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RootException(excepcion)
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