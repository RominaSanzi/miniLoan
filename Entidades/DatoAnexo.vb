Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.entidades
Imports di.financiera.utils
Imports di.financiera.seguridad
Imports System.IO
Imports System.Configuration
Imports System.Collections.Generic

Public Class DatoAnexo
    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iValor As String
    Private iTipoEntidad As TipoEntidad
    Private iTipoDatoAnexo As TipoDatoAnexo
    Private iEntidad As Object
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
    Public Property tipoDatoAnexo() As TipoDatoAnexo
        Get
            Return iTipoDatoAnexo
        End Get
        Set(ByVal Value As TipoDatoAnexo)
            iTipoDatoAnexo = Value
        End Set
    End Property
    Public Property valor() As String
        Get
            Return iValor
        End Get
        Set(ByVal Value As String)
            iValor = Value
        End Set
    End Property
    Public Property tipoEntidad() As TipoEntidad
        Get
            Return iTipoEntidad
        End Get
        Set(ByVal Value As TipoEntidad)
            iTipoEntidad = Value
        End Set
    End Property
    Public Property entidad() As Object
        Get
            Return iEntidad
        End Get
        Set(ByVal Value As Object)
            iEntidad = Value
        End Set
    End Property

#End Region

#Region "Metodos"
    Private Sub validarCrear(eValidarExistencia)

        Try

            If IsNothing(tipoDatoAnexo) OrElse tipoDatoAnexo.id = Nothing Then
                Throw New DatoAnexoNoCreadoException("El tipo de dato anexo no puede ser nulo")
            End If

            'If valor = Nothing Then
            '    Throw New DatoAnexoNoCreadoException("El valor no puede ser nulo")
            'End If

            If IsNothing(iTipoEntidad) Then
                Throw New DatoAnexoNoCreadoException("El tipo de entidad no puede ser nulo")
            End If

            If IsNothing(entidad) Then
                Throw New DatoAnexoNoCreadoException("La entidad no puede ser nula")
            End If
            If eValidarExistencia Then
                Dim iGeneradorSql As New GeneradorSql
                Dim iDataReader As IDataReader

                Try
                    iConexion = obtenerConexion()

                    iGeneradorSql.agregarColumna("id")
                    iGeneradorSql.agregarTabla("datoanexo")
                    iGeneradorSql.agregarCondicionWhere("idTipoDatoAnexo=" & FuncionComun.nuloSiEsNothing(tipoDatoAnexo.id))
                    iGeneradorSql.agregarCondicionWhere("valor=" & FuncionComun.nuloSiEsNothing(valor))
                    iGeneradorSql.agregarCondicionWhere("idTipoEntidad=" & FuncionComun.nuloSiEsNothing(iTipoEntidad.id))
                    iGeneradorSql.agregarCondicionWhere("idEntidad=" & FuncionComun.nuloSiEsNothing(iEntidad.id))

                    iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
                    If iDataReader.Read Then
                        Throw New PersonaNoCreadaException("El anexo ya existe para la entidad")
                    End If
                    iDataReader.Close()

                Catch excepcion As ErrorConexionException
                    Throw New PersonaNoCreadaException(excepcion)
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
            End If
        Catch exception As Exception
            Throw New DatoAnexoNoCreadoException(exception)
        End Try
    End Sub

    Public Sub crear(Optional eValidarExistencia As Boolean = True)
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear(eValidarExistencia)

            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("datoAnexo")

            iGeneradorSql.agregarColumna("idTipoDatoAnexo")
            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarColumna("idTipoEntidad")
            iGeneradorSql.agregarColumna("idEntidad")


            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(tipoDatoAnexo.id))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(valor))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iTipoEntidad.id))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iEntidad.id))


            id = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New DatoAnexoNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub crearMasivo(eDatosAnexos As List(Of DatoAnexo), eEntidad As Object)
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("datoAnexo")

            iGeneradorSql.agregarColumna("idTipoDatoAnexo")
            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarColumna("idTipoEntidad")
            iGeneradorSql.agregarColumna("idEntidad")

            For Each iDatoAnexo As DatoAnexo In eDatosAnexos
                With iDatoAnexo
                    .entidad = eEntidad
                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(.tipoDatoAnexo.id), True)
                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(.valor), True)
                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(.tipoEntidad.id), True)
                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(.entidad.id), True)

                    iGeneradorSql.agregarValuesAgrupados(iGeneradorSql.generarValues)
                End With
            Next

            iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New DatoAnexoNoCreadoException(excepcion)
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
            If IsNothing(tipoDatoAnexo) OrElse tipoDatoAnexo.id = Nothing Then
                Throw New DatoAnexoNoModificadoException("El tipo de dato anexo no puede ser nulo")
            End If
            If valor = Nothing Then
                Throw New DatoAnexoNoModificadoException("El valor no puede ser nulo")
            End If

            If IsNothing(iTipoEntidad) Then
                Throw New DatoAnexoNoModificadoException("El tipo de entidad no puede ser nulo")
            End If

            If IsNothing(entidad) Then
                Throw New DatoAnexoNoModificadoException("La entidad no puede ser nula")
            End If

        Catch excepcion As Exception
            Throw New DatoAnexoNoModificadoException(excepcion)
        End Try
    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarSet("idTipoDatoAnexo=" & FuncionComun.nuloSiEsNothing(tipoDatoAnexo.id))
            iGeneradorSql.agregarSet("valor=" & FuncionComun.nuloSiEsNothing(valor))
            iGeneradorSql.agregarSet("idTipoEntidad=" & FuncionComun.nuloSiEsNothing(iTipoEntidad.id))
            iGeneradorSql.agregarSet("idEntidad=" & FuncionComun.nuloSiEsNothing(iEntidad.id))

            iGeneradorSql.agregarTabla("datoAnexo")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New DatoAnexoNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    'Public Function obtenerDatoAnexo() As DatoAnexo
    '    Dim iDataReader As IDataReader
    '    Dim iGeneradorSql As New GeneradorSql

    '    Try
    '        iConexion = obtenerConexion()

    '        iGeneradorSql.agregarColumna("id")
    '        iGeneradorSql.agregarColumna("idTipoDatoAnexo")
    '        iGeneradorSql.agregarColumna("valor")
    '        iGeneradorSql.agregarColumna("idTipoEntidad")
    '        iGeneradorSql.agregarColumna("idEntidad")

    '        iGeneradorSql.agregarTabla("datoAnexo")

    '        If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))
    '        If Not IsNothing(tipoDatoAnexo) AndAlso tipoDatoAnexo.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("idTipoDatoAnexo=" & FuncionComun.nuloSiEsNothing(tipoDatoAnexo.id))
    '        If Not IsNothing(tipoEntidad) AndAlso tipoEntidad.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("idTipoEntidad=" & FuncionComun.nuloSiEsNothing(tipoEntidad.id))
    '        If Not IsNothing(entidad) AndAlso entidad.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("idEntidad=" & FuncionComun.nuloSiEsNothing(entidad.id))

    '        iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

    '        If iDataReader.Read Then
    '            iId = FuncionComun.nothingSiEsNulo(iDataReader.Item("Id"))
    '            iTipoDatoAnexo = New TipoDatoAnexo
    '            iTipoDatoAnexo.id = iDataReader.Item("idtipodatoanexo")
    '            iValor = FuncionComun.nothingSiEsNulo(iDataReader.Item("valor"))
    '            iTipoEntidad = New TipoEntidad
    '            iTipoEntidad.id = FuncionComun.nothingSiEsNulo(iDataReader.Item("idTipoEntidad"))
    '            Select Case iTipoEntidad.id
    '                Case TipoEntidad.SOLICITUD
    '                    iEntidad = New Solicitud
    '                Case TipoEntidad.CLIENTE
    '                    iEntidad = New Cliente
    '                Case TipoEntidad.CARTERA
    '                    iEntidad = New Cartera
    '                Case TipoEntidad.PRODUCTOCOMPRADO
    '                    iEntidad = New ProductoComprado
    '            End Select
    '            iEntidad.id = FuncionComun.nothingSiEsNulo(iDataReader.Item("idEntidad"))

    '            Return Me
    '        Else
    '            Throw New DatoAnexoNoEncontradoException()
    '        End If

    '    Catch excepcion As Exception
    '        Throw New DatoAnexoNoEncontradoException(excepcion)
    '    Finally
    '        If (IsNothing(MyBase.accesoDatos)) Then
    '            iConexion.cerrar()
    '            iConexion = Nothing
    '        End If
    '        If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
    '        iDataReader = Nothing
    '        iGeneradorSql.destructor()
    '        iGeneradorSql = Nothing
    '    End Try

    'End Function

    Public Function obtenerDatosAnexosPorEntidad() As List(Of DatoAnexo)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataSet As DataSet
        Dim iListaDatosAnexos As New List(Of DatoAnexo)
        Dim iDatoAnexo As DatoAnexo

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("datoAnexo.id")
            iGeneradorSql.agregarColumna("datoAnexo.idTipoDatoAnexo")
            iGeneradorSql.agregarColumna("datoAnexo.valor")
            iGeneradorSql.agregarColumna("datoAnexo.idTipoEntidad")
            iGeneradorSql.agregarColumna("datoAnexo.idEntidad")
            iGeneradorSql.agregarColumna("tipodatoAnexo.requeridoEnCarga")
            iGeneradorSql.agregarColumna("tipodatoAnexo.nombre")
            iGeneradorSql.agregarColumna("tipodatoAnexo.idTipoDato")
            iGeneradorSql.agregarColumna("tipodatoAnexo.datoObligatorio")

            iGeneradorSql.agregarTabla("datoAnexo inner join tipoDatoAnexo on tipoDatoAnexo.id=datoAnexo.idTipoDatoAnexo")

            If Not IsNothing(iTipoEntidad) Then iGeneradorSql.agregarCondicionWhere("datoAnexo.idTipoEntidad=" & iTipoEntidad.id)
            If Not IsNothing(iEntidad) Then iGeneradorSql.agregarCondicionWhere("datoAnexo.idEntidad=" & iEntidad.id)

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "datosAnexos")

            For Each iDataRow As DataRow In iDataSet.Tables("datosAnexos").Rows
                iDatoAnexo = New DatoAnexo
                With iDatoAnexo
                    .id = iDataRow.Item("id")
                    .tipoDatoAnexo = New TipoDatoAnexo
                    .tipoDatoAnexo.id = iDataRow.Item("idTipoDatoAnexo")
                    .tipoDatoAnexo.nombre = iDataRow.Item("nombre")
                    .tipoDatoAnexo.requeridoEnCarga = New Boolean?
                    .tipoDatoAnexo.requeridoEnCarga = FuncionComun.byteBoolean(iDataRow.Item("requeridoEnCarga"))
                    Select Case iDataRow.Item("idTipoDato")
                        Case di.financiera.entidades.TipoDato.DATODATE
                            .tipoDatoAnexo.tipoDato = New DatoDate
                        Case di.financiera.entidades.TipoDato.DATODOUBLE
                            .tipoDatoAnexo.tipoDato = New DatoDouble
                        Case di.financiera.entidades.TipoDato.DATOINTEGER
                            .tipoDatoAnexo.tipoDato = New DatoInteger
                        Case di.financiera.entidades.TipoDato.DATOLONG
                            .tipoDatoAnexo.tipoDato = New DatoLong
                        Case di.financiera.entidades.TipoDato.DATOSTRING
                            .tipoDatoAnexo.tipoDato = New DatoString
                    End Select
                    .tipoDatoAnexo.tipoDato.id = iDataRow.Item("idTipoDato")
                    .tipoDatoAnexo.datoObligatorio = FuncionComun.byteBoolean(iDataRow.Item("datoObligatorio"))

                    .valor = iDataRow.Item("valor").ToString
                    .tipoEntidad = New TipoEntidad
                    .tipoEntidad.id = iDataRow.Item("idTipoEntidad")
                End With

                iListaDatosAnexos.Add(iDatoAnexo)
                iDatoAnexo = Nothing
            Next

            Return iListaDatosAnexos

        Catch excepcion As Exception
            Throw New SucursalClienteNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iDataSet = Nothing
            iListaDatosAnexos = Nothing
        End Try

    End Function

    Public Function obtenerDatosAnexos() As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("tipoDatoAnexo.id as idTipoDatoAnexo")
            iGeneradorSql.agregarColumna("tipoDatoAnexo.nombre")
            iGeneradorSql.agregarColumna("tipoDatoAnexo.idTipoEntidad")
            iGeneradorSql.agregarTabla("tipoDatoAnexo")
            iGeneradorSql.agregarOrden("tipoDatoAnexo.nombre")

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New DatoAnexoNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("datoAnexo")

            If id <> Nothing OrElse (Not IsNothing(iEntidad) AndAlso Not IsNothing(iTipoEntidad)) Then
                If id <> Nothing Then
                    iGeneradorSql.agregarCondicionWhere("id=" & id)
                ElseIf Not IsNothing(iEntidad) AndAlso Not IsNothing(iTipoEntidad) Then
                    iGeneradorSql.agregarCondicionWhere("idTipoEntidad=" & iTipoEntidad.id)
                    iGeneradorSql.agregarCondicionWhere("idEntidad=" & iEntidad.id)
                End If

                iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)
            End If

        Catch excepcion As Exception
            Throw New DatoAnexoNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerDatoAnexoDataSet() As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataSet As DataSet

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("datoAnexo")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))
            If Not IsNothing(tipoDatoAnexo) AndAlso tipoDatoAnexo.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("idTipoDatoAnexo=" & FuncionComun.nuloSiEsNothing(tipoDatoAnexo.id))
            If Not IsNothing(tipoEntidad) AndAlso tipoEntidad.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("idTipoEntidad=" & FuncionComun.nuloSiEsNothing(tipoEntidad.id))
            If Not IsNothing(entidad) AndAlso entidad.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("idEntidad=" & FuncionComun.nuloSiEsNothing(entidad.id))

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "DatoAnexo")

        Catch excepcion As Exception
            Throw New DatoAnexoNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iDataSet = Nothing
        End Try
    End Function

    Public Sub dispose()
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
