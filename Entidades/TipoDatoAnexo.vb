Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.Collections.ObjectModel
Imports System.Collections.Generic
Public Class TipoDatoAnexo
    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iNombre As String
    Private iTipoDato As TipoDato
    Private iTipoEntidad As TipoEntidad
    Private iDatoObligatorio As Boolean
    Private iRequeridoEnCarga As Boolean?

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

    Public Property nombre As String
        Get
            Return iNombre
        End Get
        Set(value As String)
            iNombre = value
        End Set
    End Property

    Public Property tipoDato As TipoDato
        Get
            Return iTipoDato
        End Get
        Set(value As TipoDato)
            iTipoDato = value
        End Set
    End Property

    Public Property tipoEntidad As TipoEntidad
        Get
            Return iTipoEntidad
        End Get
        Set(value As TipoEntidad)
            iTipoEntidad = value
        End Set
    End Property

    Public Property datoObligatorio As Boolean
        Get
            Return iDatoObligatorio
        End Get
        Set(value As Boolean)
            iDatoObligatorio = value
        End Set
    End Property

    Public Property requeridoEnCarga As Boolean?
        Get
            Return iRequeridoEnCarga
        End Get
        Set(value As Boolean?)
            iRequeridoEnCarga = value
        End Set
    End Property

#End Region

#Region "Metodos"
    Private Sub validarCrear()

        Try

            If nombre = Nothing Then
                Throw New TipoDatoAnexoNoCreadoException("El codigo no puede ser nulo")
            End If
            If IsNothing(tipoDato) Then
                Throw New TipoDatoAnexoNoCreadoException("El tipo de dato no puede ser nulo")
            End If
            If IsNothing(tipoEntidad) Then
                Throw New TipoDatoAnexoNoCreadoException("El tipo de entidad no puede ser nulo")
            End If
        Catch excepcion As Exception
            Throw New TipoDatoAnexoNoCreadoException(excepcion)
        End Try
    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()


            validarCrear()

            iGeneradorSql.agregarTabla("TipoDatoAnexo")

            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("idTipoDato")
            iGeneradorSql.agregarColumna("idTipoEntidad")
            iGeneradorSql.agregarColumna("datoObligatorio")
            iGeneradorSql.agregarColumna("requeridoEnCarga")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(nombre))
            If tipoDato.isDatoDate Then iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(TipoDato.DATODATE))
            If tipoDato.isDatoDouble Then iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(TipoDato.DATODOUBLE))
            If tipoDato.isDatoInteger Then iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(TipoDato.DATOINTEGER))
            If tipoDato.isDatoLong Then iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(TipoDato.DATOLONG))
            If tipoDato.isDatoString Then iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(TipoDato.DATOSTRING))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(tipoEntidad.id))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(iDatoObligatorio))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(iRequeridoEnCarga.Value))

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New TipoDatoAnexoNoCreadoException(excepcion)
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

            If id = Nothing Then
                Throw New TipoDatoAnexoNoModificadoException("El tipo de dato a modificar no puede ser nulo")
            End If
            If nombre = Nothing Then
                Throw New TipoDatoAnexoNoModificadoException("El codigo no puede ser nulo")
            End If
            If IsNothing(tipoDato) Then
                Throw New TipoDatoAnexoNoModificadoException("El tipo de dato no puede ser nulo")
            End If
            If IsNothing(tipoEntidad) Then
                Throw New TipoDatoAnexoNoModificadoException("El tipo de entidad no puede ser nulo")
            End If

        Catch excepcion As Exception
            Throw New TipoDatoAnexoNoModificadoException(excepcion)
        End Try
    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarSet("nombre=" & FuncionComun.nuloSiEsNothing(nombre))
            If tipoDato.isDatoDate Then iGeneradorSql.agregarSet("idTipoDato=" & FuncionComun.nuloSiEsNothing(TipoDato.DATODATE))
            If tipoDato.isDatoDouble Then iGeneradorSql.agregarSet("idTipoDato=" & FuncionComun.nuloSiEsNothing(TipoDato.DATODOUBLE))
            If tipoDato.isDatoInteger Then iGeneradorSql.agregarSet("idTipoDato=" & FuncionComun.nuloSiEsNothing(TipoDato.DATOINTEGER))
            If tipoDato.isDatoLong Then iGeneradorSql.agregarSet("idTipoDato=" & FuncionComun.nuloSiEsNothing(TipoDato.DATOLONG))
            If tipoDato.isDatoString Then iGeneradorSql.agregarSet("idTipoDato=" & FuncionComun.nuloSiEsNothing(TipoDato.DATOSTRING))
            iGeneradorSql.agregarSet("datoObligatorio=" & FuncionComun.booleanByte(iDatoObligatorio))
            iGeneradorSql.agregarSet("idTipoEntidad=" & FuncionComun.nuloSiEsNothing(tipoEntidad.id))
            iGeneradorSql.agregarSet("requeridoEnCarga=" & FuncionComun.booleanByte(iRequeridoEnCarga.Value))

            iGeneradorSql.agregarTabla("TipoDatoAnexo")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            iConexion.rollback()
            Throw New TipoDatoAnexoNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerTiposDatosAnexos() As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("TipoDatoAnexo.id")
            iGeneradorSql.agregarColumna("TipoDatoAnexo.nombre")
            iGeneradorSql.agregarColumna("TipoDatoAnexo.idTipoDato")
            iGeneradorSql.agregarColumna("TipoDatoAnexo.idTipoEntidad")
            iGeneradorSql.agregarColumna("TipoEntidad.descripcion as tipoentidad")
            iGeneradorSql.agregarTabla("TipoDatoAnexo inner join tipoEntidad on tipoEntidad.id=tipoDatoAnexo.idTipoEntidad")
            iGeneradorSql.agregarOrden("TipoDatoAnexo.nombre")

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New TipoDatoAnexoNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerTiposDatosAnexosGrilla() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("tipoDatoAnexo.id")
            iGeneradorSql.agregarColumna("tipoDatoAnexo.nombre")
            iGeneradorSql.agregarColumna("TipoDato.descripcion as tipoDato")
            iGeneradorSql.agregarColumna("tipoDatoAnexo.idtipoDato")
            iGeneradorSql.agregarColumna("TipoDatoAnexo.idTipoEntidad")
            iGeneradorSql.agregarColumna("TipoEntidad.descripcion as tipoentidad")
            iGeneradorSql.agregarTabla("TipoDatoAnexo inner join tipoDato on tipoDato.id=tipoDatoAnexo.idTipoDato inner join tipoEntidad on tipoEntidad.id=tipoDatoAnexo.idTipoEntidad")
            iGeneradorSql.agregarOrden("tipoDatoAnexo.nombre")

            If nombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("tipoDatoAnexo.nombre like '" & nombre & "%'")
            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "TipoDatoAnexo")

        Catch excepcion As Exception
            Throw New TipoDatoAnexoNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Private Sub validarEliminar()

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("DatoAnexo")
            iGeneradorSql.agregarCondicionWhere("idTipoDatoAnexo=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New TipoDatoAnexoNoEliminadoException("El tipo de dato anexo esta siendo utilizado")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New TipoDatoAnexoNoEliminadoException(excepcion)
        End Try
    End Sub

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            validarEliminar()

            iGeneradorSql.agregarTabla("TipoDatoAnexo")
            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New TipoDatoAnexoNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerTipoDatoAnexo() As TipoDatoAnexo
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("idTipoDato")
            iGeneradorSql.agregarColumna("idTipoEntidad")
            iGeneradorSql.agregarColumna("datoObligatorio")
            iGeneradorSql.agregarColumna("requeridoEnCarga")
            iGeneradorSql.agregarTabla("TipoDatoAnexo")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))
            If nombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("nombre=" & FuncionComun.nuloSiEsNothing(nombre))
            If Not IsNothing(tipoEntidad) AndAlso tipoEntidad.id > 0 Then iGeneradorSql.agregarCondicionWhere("idTipoEntidad=" & FuncionComun.nuloSiEsNothing(tipoEntidad.id))

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = FuncionComun.nothingSiEsNulo(iDataReader.Item("Id"))
                iNombre = FuncionComun.nothingSiEsNulo(iDataReader.Item("Nombre"))
                iTipoEntidad = New TipoEntidad
                iTipoEntidad.id = FuncionComun.nothingSiEsNulo(iDataReader.Item("IdTipoEntidad"))

                Select Case iDataReader.Item("idTipoDato")
                    Case di.financiera.entidades.TipoDato.DATODATE
                        iTipoDato = New DatoDate
                    Case di.financiera.entidades.TipoDato.DATODOUBLE
                        iTipoDato = New DatoDouble
                    Case di.financiera.entidades.TipoDato.DATOINTEGER
                        iTipoDato = New DatoInteger
                    Case di.financiera.entidades.TipoDato.DATOLONG
                        iTipoDato = New DatoLong
                    Case di.financiera.entidades.TipoDato.DATOSTRING
                        iTipoDato = New DatoString
                End Select
                iDatoObligatorio = FuncionComun.byteBoolean(iDataReader.Item("DatoObligatorio").ToString)
                iRequeridoEnCarga = FuncionComun.byteBoolean(iDataReader.Item("RequeridoEnCarga").ToString)
                iDataReader.Close()

                iTipoEntidad.accesoDatos = iConexion
                iTipoEntidad = iTipoEntidad.obtenerTipoEntidad
                iTipoEntidad.accesoDatos = Nothing

                Return Me
            Else
                Throw New TipoDatoAnexoNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New TipoDatoAnexoNoEncontradoException(excepcion)
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

    Public Function obtenerTipoDatoAnexos(Optional eEsObligatorio As Boolean = False, Optional eEsRequeridoEnCarga As Boolean = False) As List(Of TipoDatoAnexo)
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataSet As DataSet
        Dim iTipoDatoAnexo As TipoDatoAnexo
        Dim iListaTipoDatosAnexo As List(Of TipoDatoAnexo)
        Dim i As Integer

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("TipoDatoAnexo")
            iGeneradorSql.agregarColumna("id")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))
            If nombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("nombre=" & FuncionComun.nuloSiEsNothing(nombre))
            If Not IsNothing(tipoEntidad) AndAlso tipoEntidad.id > 0 Then iGeneradorSql.agregarCondicionWhere("idTipoEntidad=" & FuncionComun.nuloSiEsNothing(tipoEntidad.id))
            If eEsObligatorio Then iGeneradorSql.agregarCondicionWhere("datoObligatorio=" & FuncionComun.booleanByte(True))
            If eEsRequeridoEnCarga Then iGeneradorSql.agregarCondicionWhere("requeridoencarga=" & FuncionComun.booleanByte(True))

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "TipoDatoAnexos")

            iListaTipoDatosAnexo = New List(Of TipoDatoAnexo)

            For i = 0 To iDataSet.Tables("TipoDatoAnexos").Rows.Count - 1
                iTipoDatoAnexo = New TipoDatoAnexo
                iTipoDatoAnexo.id = iDataSet.Tables("TipoDatoAnexos").Rows(i).Item("id")
                iTipoDatoAnexo.accesoDatos = iConexion
                iTipoDatoAnexo = iTipoDatoAnexo.obtenerTipoDatoAnexo()
                iTipoDatoAnexo.accesoDatos = Nothing
                iListaTipoDatosAnexo.Add(iTipoDatoAnexo)
            Next

            Return iListaTipoDatosAnexo

        Catch excepcion As Exception
            Throw New TipoDatoAnexoNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iDataSet = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iListaTipoDatosAnexo = Nothing
            iTipoDatoAnexo = Nothing
        End Try
    End Function

    Public Function obtenerTiposDatosAnexosPorEntidad() As IDataReader
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("idTipoDato")
            iGeneradorSql.agregarColumna("idTipoEntidad")
            iGeneradorSql.agregarColumna("datoObligatorio")
            iGeneradorSql.agregarColumna("requeridoEnCarga")
            iGeneradorSql.agregarTabla("TipoDatoAnexo")

            If tipoEntidad.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("idtipoentidad=" & FuncionComun.nuloSiEsNothing(tipoEntidad.id))
            If Not IsNothing(requeridoEnCarga) Then iGeneradorSql.agregarCondicionWhere("requeridoEnCarga=" & FuncionComun.nuloSiEsNothing(requeridoEnCarga.Value))

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New TipoDatoAnexoNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Overridable Sub dispose()
        Try

            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

        Catch exception As Exception
            Throw New RootException(exception)
        End Try

    End Sub



#End Region

End Class