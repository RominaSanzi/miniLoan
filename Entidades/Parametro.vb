Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades
Imports di.financiera.seguridad
Imports System.Collections.Generic
Imports System

Public Class Parametro

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iDescripcion As String
    Private iValor As Object
    Private iTipoDato As TipoDato
    Private iSingleton As Boolean
    Private iNombreTabla As String
    Private iTipoParametro As TipoParametro
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
    Public Property valor() As Object
        Get
            Return iValor
        End Get
        Set(ByVal Value As Object)
            iValor = Value
        End Set
    End Property
    Public Property tipoDato() As TipoDato
        Get
            Return iTipoDato
        End Get
        Set(ByVal Value As TipoDato)
            iTipoDato = Value
        End Set
    End Property

    Public Property singleton As Boolean
        Get
            Return iSingleton
        End Get
        Set(value As Boolean)
            iSingleton = value
        End Set
    End Property

    Public Property nombreTabla As String
        Get
            Return iNombreTabla
        End Get
        Set(value As String)
            iNombreTabla = value
        End Set
    End Property

    Public Property tipoParametro As TipoParametro
        Get
            Return iTipoParametro
        End Get
        Set(value As TipoParametro)
            iTipoParametro = value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerParametroPorJerarquia(ByVal eNivel As Nivel, Optional eValorVacio As String = "") As Parametro
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataSet, iDataSetSingleton As DataSet
        Dim iDataRow As DataRow
        Try

            'busca un parametro por jerarquia, validando que el parámetro obtenido sea distinto de vacio
            Dim iBusqueda(1) As Object

            iDataSetSingleton = ParametroSingleton.getInstancia(False).dataSet

            If Not IsNothing(iDataSetSingleton) Then
                Dim iNivelSingleton As Nivel
                iNivelSingleton = eNivel
                While (IsNothing(iDataRow) OrElse (Not IsNothing(iDataRow) AndAlso iDataRow("valor") = eValorVacio)) AndAlso (Not IsNothing(iNivelSingleton))
                    iBusqueda(0) = iDescripcion
                    iBusqueda(1) = iNivelSingleton.id

                    iDataRow = iDataSetSingleton.Tables("Parametros").Rows.Find(iBusqueda)

                    iNivelSingleton = iNivelSingleton.padre
                End While
                If Not IsNothing(iDataRow) Then
                    iId = iDataRow("id")
                    Select Case iDataRow.Item("IdTipoDato").ToString
                        Case di.financiera.entidades.TipoDato.DATODATE
                            iTipoDato = New DatoDate()
                            iValor = FuncionComun.nothingSiEsVacio(iDataRow.Item("valor").ToString)
                        Case di.financiera.entidades.TipoDato.DATODOUBLE
                            iTipoDato = New DatoDouble()
                            iValor = FuncionComun.ceroSiEsVacio(iDataRow.Item("valor").ToString)
                        Case di.financiera.entidades.TipoDato.DATOINTEGER
                            iTipoDato = New DatoInteger()
                            iValor = FuncionComun.ceroSiEsVacio(FuncionComun.ceroSiEsVacio(iDataRow.Item("valor").ToString))
                        Case di.financiera.entidades.TipoDato.DATOLONG
                            iTipoDato = New DatoLong()
                            iValor = FuncionComun.ceroSiEsVacio(iDataRow.Item("valor").ToString)
                        Case di.financiera.entidades.TipoDato.DATOSTRING
                            iTipoDato = New DatoString()
                            iValor = CStr(iDataRow.Item("valor").ToString)
                    End Select
                    Return Me
                End If
            End If
            iConexion = obtenerConexion()

            While (IsNothing(iDataSet) OrElse iDataSet.Tables("Parametro").Rows.Count <= 0 OrElse iDataSet.Tables("Parametro").Rows(0).Item("valor") = eValorVacio) AndAlso (Not IsNothing(eNivel))

                iGeneradorSql.agregarTabla(IIf(iNombreTabla <> Nothing, iNombreTabla, "parametro"))
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarColumna("valor")
                iGeneradorSql.agregarColumna("idTipoDato")
                iGeneradorSql.agregarColumna("idTipoParametro")
                iGeneradorSql.agregarCondicionWhere("descripcion='" & iDescripcion & "'")
                iGeneradorSql.agregarCondicionWhere("idNivel=" & eNivel.id)
                iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametro")

                eNivel = eNivel.padre

            End While

            If iDataSet.Tables("Parametro").Rows.Count > 0 Then
                iId = iDataSet.Tables("Parametro").Rows.Item(0).Item("id")
                'iTipoParametro = New TipoParametro
                'iTipoParametro.id = iDataSet.Tables("Parametro").Rows.Item(0).Item("idTipoParametro")
                Select Case iDataSet.Tables("Parametro").Rows.Item(0).Item("IdTipoDato").ToString
                    Case di.financiera.entidades.TipoDato.DATODATE
                        iTipoDato = New DatoDate()
                        iValor = FuncionComun.nothingSiEsVacio(iDataSet.Tables("Parametro").Rows.Item(0).Item("valor").ToString)
                    Case di.financiera.entidades.TipoDato.DATODOUBLE
                        iTipoDato = New DatoDouble()
                        iValor = FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametro").Rows.Item(0).Item("valor").ToString)
                    Case di.financiera.entidades.TipoDato.DATOINTEGER
                        iTipoDato = New DatoInteger()
                        iValor = FuncionComun.ceroSiEsVacio(FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametro").Rows.Item(0).Item("valor").ToString))
                    Case di.financiera.entidades.TipoDato.DATOLONG
                        iTipoDato = New DatoLong()
                        iValor = FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametro").Rows.Item(0).Item("valor").ToString)
                    Case di.financiera.entidades.TipoDato.DATOSTRING
                        iTipoDato = New DatoString()
                        iValor = CStr(iDataSet.Tables("Parametro").Rows.Item(0).Item("valor").ToString)
                End Select
                Return Me
            Else
                Throw New ParametroNoEncontradoException("No se encotro el parametro " & iDescripcion & " en la base de datos")
            End If

        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) AndAlso Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iDataSet = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerParametro(ByVal eNivel As Nivel, Optional ByVal eIncremental As Boolean = False) As Parametro
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataSet, iDataSetSingleton As DataSet
        Dim iDataRow As DataRow
        Try

            'primero busco si el parametro esta en memoria
            Dim iBusqueda(1) As Object

            iDataSetSingleton = ParametroSingleton.getInstancia(False).dataSet

            If Not IsNothing(iDataSetSingleton) Then
                Dim iNivelSingleton As Nivel
                iNivelSingleton = eNivel
                While IsNothing(iDataRow) And (Not IsNothing(iNivelSingleton))
                    iBusqueda(0) = iDescripcion
                    iBusqueda(1) = iNivelSingleton.id

                    iDataRow = iDataSetSingleton.Tables("parametro").Rows.Find(iBusqueda)

                    iNivelSingleton = iNivelSingleton.padre
                End While
                If Not IsNothing(iDataRow) Then
                    iId = iDataRow("id")
                    Select Case iDataRow.Item("IdTipoDato").ToString
                        Case di.financiera.entidades.TipoDato.DATODATE
                            iTipoDato = New DatoDate()
                            iValor = FuncionComun.nothingSiEsVacio(iDataRow.Item("valor").ToString)
                        Case di.financiera.entidades.TipoDato.DATODOUBLE
                            iTipoDato = New DatoDouble()
                            iValor = FuncionComun.ceroSiEsVacio(iDataRow.Item("valor").ToString)
                        Case di.financiera.entidades.TipoDato.DATOINTEGER
                            iTipoDato = New DatoInteger()
                            iValor = FuncionComun.ceroSiEsVacio(FuncionComun.ceroSiEsVacio(iDataRow.Item("valor").ToString))
                        Case di.financiera.entidades.TipoDato.DATOLONG
                            iTipoDato = New DatoLong()
                            iValor = FuncionComun.ceroSiEsVacio(iDataRow.Item("valor").ToString)
                        Case di.financiera.entidades.TipoDato.DATOSTRING
                            iTipoDato = New DatoString()
                            iValor = CStr(iDataRow.Item("valor").ToString)
                    End Select
                    Return Me
                End If
            End If

            iConexion = obtenerConexion()

            While (IsNothing(iDataSet) OrElse iDataSet.Tables("parametro").Rows.Count <= 0)

                iGeneradorSql.agregarTabla(IIf(iNombreTabla <> Nothing, iNombreTabla, "parametro"))
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarColumna("valor")
                iGeneradorSql.agregarColumna("idTipoDato")
                iGeneradorSql.agregarColumna("idTipoParametro")
                iGeneradorSql.agregarCondicionWhere("descripcion='" & iDescripcion & "'")
                'iGeneradorSql.agregarCondicionWhere("idNivel=" & eNivel.id)
                iGeneradorSql.bloquearTabla = eIncremental
                iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "parametro")

                'eNivel = eNivel.padre

            End While

            If iDataSet.Tables("parametro").Rows.Count > 0 Then
                iId = iDataSet.Tables("parametro").Rows.Item(0).Item("id")
                iTipoParametro = New TipoParametro
                iTipoParametro.id = iDataSet.Tables("parametro").Rows.Item(0).Item("idTipoParametro")
                Select Case iDataSet.Tables("parametro").Rows.Item(0).Item("IdTipoDato").ToString
                    Case di.financiera.entidades.TipoDato.DATODATE
                        iTipoDato = New DatoDate()
                        iValor = FuncionComun.nothingSiEsVacio(iDataSet.Tables("parametro").Rows.Item(0).Item("valor").ToString)
                    Case di.financiera.entidades.TipoDato.DATODOUBLE
                        iTipoDato = New DatoDouble()
                        iValor = FuncionComun.ceroSiEsVacio(iDataSet.Tables("parametro").Rows.Item(0).Item("valor").ToString)
                    Case di.financiera.entidades.TipoDato.DATOINTEGER
                        iTipoDato = New DatoInteger()
                        iValor = FuncionComun.ceroSiEsVacio(FuncionComun.ceroSiEsVacio(iDataSet.Tables("parametro").Rows.Item(0).Item("valor").ToString))
                    Case di.financiera.entidades.TipoDato.DATOLONG
                        iTipoDato = New DatoLong()
                        iValor = FuncionComun.ceroSiEsVacio(iDataSet.Tables("parametro").Rows.Item(0).Item("valor").ToString)
                    Case di.financiera.entidades.TipoDato.DATOSTRING
                        iTipoDato = New DatoString()
                        iValor = CStr(iDataSet.Tables("parametro").Rows.Item(0).Item("valor").ToString)
                End Select
                Return Me
            Else
                Throw New ParametroNoEncontradoException("No se encotro el parametro " & iDescripcion & " en la base de datos") 'ERROR
            End If

        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception) 'ERROR
        Finally
            If (IsNothing(MyBase.accesoDatos)) AndAlso Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iDataSet = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerParametroIncremental(ByVal eNivel As Nivel, Optional ByVal eCreaNuevaConexion As Boolean = True) As Parametro
        Dim iGeneradorSql As New GeneradorSql()
        Dim iParametro As Parametro

        Try

            If eCreaNuevaConexion Then
                accesoDatos = New accesoDatos
            End If

            iConexion = obtenerConexion()

            iParametro = obtenerParametro(eNivel, True)

            iGeneradorSql.agregarTabla(IIf(iNombreTabla <> Nothing, iNombreTabla, "parametro"))
            iGeneradorSql.agregarSet("valor=" & iParametro.valor + 1)
            iGeneradorSql.agregarCondicionWhere("id=" & iParametro.id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

            Return iParametro

        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        Finally

            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If eCreaNuevaConexion Then
                If Not IsNothing(accesoDatos) Then
                    accesoDatos.cerrar()
                    accesoDatos = Nothing
                End If
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iParametro = Nothing
        End Try
    End Function

    Private Sub validarActualizar(ByVal eNivel As Nivel)

        If iDescripcion = Nothing Then
            Throw New ParametroNoActualizadoException("La descripcion no puede ser nula")
        End If

        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader
        Try
            iGeneradorSql.agregarTabla(IIf(iNombreTabla <> Nothing, iNombreTabla, "parametro"))
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("singleton")
            iGeneradorSql.agregarCondicionWhere("descripcion='" & iDescripcion & "'")
            iGeneradorSql.agregarCondicionWhere("idNivel=" & eNivel.id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If Not iDataReader.Read Then
                Throw New ParametroNoActualizadoException("No se encontro el parametro " & iDescripcion.ToUpper & " para ser modificado")
            Else
                If FuncionComun.byteBoolean(iDataReader.Item("singleton")) Then
                    ParametroSingleton.forzarActualizacion = True
                End If
            End If
            iDataReader.Close()


        Catch excepcion As Exception
            Throw New ParametroNoActualizadoException(excepcion)
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

    End Sub

    Public Sub actualizar(ByVal eNivel As Nivel)
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarActualizar(eNivel)

            iGeneradorSql.agregarSet("valor='" & valor & "'")
            iGeneradorSql.agregarTabla(IIf(iNombreTabla <> Nothing, iNombreTabla, "parametro"))
            iGeneradorSql.agregarCondicionWhere("descripcion='" & descripcion & "'")
            iGeneradorSql.agregarCondicionWhere("idNivel=" & eNivel.id)

            If IsNothing(iConexion) Then iConexion = obtenerConexion()

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New ParametroNoActualizadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerParametros(ByVal eNivel As Nivel) As DataSet
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataSet As DataSet

        Try
            iConexion = obtenerConexion()

            While (IsNothing(iDataSet) OrElse iDataSet.Tables("Parametro").Rows.Count <= 0) And (Not IsNothing(eNivel))
                iGeneradorSql.agregarTabla("parametro")
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarColumna("valor")
                iGeneradorSql.agregarColumna("idTipoDato")
                iGeneradorSql.agregarColumna("idTipoParametro")
                iGeneradorSql.agregarCondicionWhere("descripcion='" & iDescripcion & "'")
                iGeneradorSql.agregarCondicionWhere("idNivel=" & eNivel.id)

                iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametro")

                eNivel = eNivel.padre
            End While

            Return iDataSet

        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iDataSet = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Private Sub validarCrear()

        If descripcion = Nothing Then
            Throw New ParametroNoCreadoException("La descripción no puede ser nula")
        End If
        If IsNothing(tipoDato) Then
            Throw New ParametroNoCreadoException("El tipo de dato no puede ser nulo")
        End If

    End Sub

    Public Sub crear(ByVal eNivel As Nivel)
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla(IIf(iNombreTabla <> Nothing, iNombreTabla, "parametro"))
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarColumna("idTipoDato")
            iGeneradorSql.agregarColumna("idNivel")
            iGeneradorSql.agregarColumna("singleton")
            iGeneradorSql.agregarColumna("idTipoParametro")

            iGeneradorSql.agregarValue("'" & descripcion & "'")
            iGeneradorSql.agregarValue("'" & valor & "'")
            If tipoDato.isDatoDate Then iGeneradorSql.agregarValue(TipoDato.DATODATE)
            If tipoDato.isDatoDouble Then iGeneradorSql.agregarValue(TipoDato.DATODOUBLE)
            If tipoDato.isDatoInteger Then iGeneradorSql.agregarValue(TipoDato.DATOINTEGER)
            If tipoDato.isDatoLong Then iGeneradorSql.agregarValue(TipoDato.DATOLONG)
            If tipoDato.isDatoString Then iGeneradorSql.agregarValue(TipoDato.DATOSTRING)
            iGeneradorSql.agregarValue(eNivel.id)
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(iSingleton))
            iGeneradorSql.agregarValue(iTipoParametro.id)

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

            If iSingleton Then ParametroSingleton.forzarActualizacion = True

        Catch excepcion As Exception
            Throw New ParametroNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub crearDesdeParametroBase(ByVal eNivel As Nivel)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataSet As DataSet

        Dim iSelect As String

        Try
            iConexion = obtenerConexion()
            '==================================================================
            'PRIMERO OBTENEMOS LAS TABLAS A COMPLETAR DE PARÁMETROS
            '==================================================================
            iGeneradorSql.agregarTabla("parametroBase")
            iGeneradorSql.agregarColumna("distinct parametroBase.tabla")
            If eNivel.isEmpresaGrupo() Then
                iGeneradorSql.agregarCondicionWhere("parametrobase.idTipoNivel=" & TipoNivel.EMPRESAGRUPO, True)
            ElseIf eNivel.isUnidadDeNegocios() Then
                iGeneradorSql.agregarCondicionWhere("parametrobase.idTipoNivel=" & TipoNivel.UNIDADDENEGOCIOS, True)
            ElseIf eNivel.isSucursal() Then
                iGeneradorSql.agregarCondicionWhere("parametrobase.idTipoNivel=" & TipoNivel.SUCURSAL, True)
            ElseIf eNivel.isPuntoVentaDgi() Then
                iGeneradorSql.agregarCondicionWhere("parametrobase.idTipoNivel=" & TipoNivel.PUNTOVENTADGI, True)
            End If

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Tablas")

            '==================================================================
            'CREAMOS EL INSERT POR CADA TABLA
            '==================================================================
            For Each iDataRow As DataRow In iDataSet.Tables("Tablas").Rows
                iGeneradorSql.agregarTabla("parametroBase")
                iGeneradorSql.agregarColumna("parametroBase.descripcion")
                iGeneradorSql.agregarColumna("parametrobase.valorDefecto")
                iGeneradorSql.agregarColumna("parametrobase.idTipoDato")
                iGeneradorSql.agregarColumna(eNivel.id)
                iGeneradorSql.agregarColumna("parametrobase.singleton")
                iGeneradorSql.agregarColumna("parametrobase.idTipoParametro")

                iGeneradorSql.agregarCondicionWhere("parametrobase.tabla=" & FuncionComun.nuloSiEsNothing(iDataRow.Item("tabla").ToString), True)
                If eNivel.isEmpresaGrupo() Then
                    iGeneradorSql.agregarCondicionWhere("parametrobase.idTipoNivel=" & TipoNivel.EMPRESAGRUPO, True)
                ElseIf eNivel.isUnidadDeNegocios() Then
                    iGeneradorSql.agregarCondicionWhere("parametrobase.idTipoNivel=" & TipoNivel.UNIDADDENEGOCIOS, True)
                ElseIf eNivel.isSucursal() Then
                    iGeneradorSql.agregarCondicionWhere("parametrobase.idTipoNivel=" & TipoNivel.SUCURSAL, True)
                ElseIf eNivel.isPuntoVentaDgi() Then
                    iGeneradorSql.agregarCondicionWhere("parametrobase.idTipoNivel=" & TipoNivel.PUNTOVENTADGI, True)
                End If

                iSelect = iGeneradorSql.generarSelect

                iGeneradorSql.agregarTabla(iDataRow.Item("tabla").ToString)
                iGeneradorSql.agregarColumna("descripcion")
                iGeneradorSql.agregarColumna("valor")
                iGeneradorSql.agregarColumna("idTipoDato")
                iGeneradorSql.agregarColumna("idNivel")
                iGeneradorSql.agregarColumna("singleton")
                iGeneradorSql.agregarColumna("idTipoParametro")

                iConexion.ejecutarInsertSinParametros(iGeneradorSql.generarInsert() & " " & iSelect)
            Next

            ParametroSingleton.forzarActualizacion = True

        Catch excepcion As Exception
            Throw New ParametroNoCreadoException(excepcion)
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

    Public Sub crearParametrosDesdeParametrosBase(ByVal eParametro As Parametro, ByVal eNivel As Nivel)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataSet As DataSet
        Dim iIdNivel As Long
        Dim iSelect As String

        Try
            iConexion = obtenerConexion()
            '==================================================================
            'PRIMERO OBTENEMOS LAS TABLAS A COMPLETAR DE PARÁMETROS
            '==================================================================
            iGeneradorSql.agregarTabla("parametroBase")
            iGeneradorSql.agregarColumna("parametroBase.tabla")
            iGeneradorSql.agregarColumna("parametroBase.idTipoNivel")
            iGeneradorSql.agregarColumna("parametroBase.descripcion")
            iGeneradorSql.agregarColumna("parametrobase.valorDefecto")
            iGeneradorSql.agregarColumna("parametrobase.idTipoDato")
            iGeneradorSql.agregarColumna("parametrobase.singleton")
            iGeneradorSql.agregarColumna("parametrobase.idTipoParametro")

            iGeneradorSql.agregarCondicionWhere("parametrobase.descripcion=" & FuncionComun.nuloSiEsNothing(eParametro.descripcion))

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Tablas")

            '==================================================================
            'CREAMOS EL INSERT POR CADA TABLA
            '==================================================================
            For Each iDataRow As DataRow In iDataSet.Tables("Tablas").Rows

                Select Case iDataRow.Item("idTipoNivel")
                    Case TipoNivel.PUNTOVENTADGI
                        If eNivel.isPuntoVentaDgi Then
                            iIdNivel = eNivel.id
                        Else
                            Throw New ParametroNoCreadoException("No se pudo insertar el parametro punto de venta dgi desde parametro base")
                        End If
                    Case TipoNivel.SUCURSAL
                        If eNivel.isSucursal Then
                            iIdNivel = eNivel.id
                        ElseIf eNivel.isUnidadDeNegocios Then
                            iIdNivel = eNivel.padre.id
                        ElseIf eNivel.isEmpresaGrupo Then
                            iIdNivel = eNivel.padre.padre.id
                        ElseIf eNivel.isGrupoEmpresas Then
                            iIdNivel = eNivel.padre.padre.padre.id
                        Else
                            Throw New ParametroNoCreadoException("No se pudo insertar el parametro sucursal desde parametro base")
                        End If
                    Case TipoNivel.UNIDADDENEGOCIOS
                        If eNivel.isUnidadDeNegocios Then
                            iIdNivel = eNivel.id
                        ElseIf eNivel.isEmpresaGrupo Then
                            iIdNivel = eNivel.padre.id
                        ElseIf eNivel.isGrupoEmpresas Then
                            iIdNivel = eNivel.padre.padre.id
                        Else
                            Throw New ParametroNoCreadoException("No se pudo insertar el parametro unidad de negocio desde parametro base")
                        End If
                    Case TipoNivel.EMPRESAGRUPO
                        If eNivel.isEmpresaGrupo Then
                            iIdNivel = eNivel.id
                        ElseIf eNivel.isGrupoEmpresas Then
                            iIdNivel = eNivel.padre.id
                        Else
                            Throw New ParametroNoCreadoException("No se pudo insertar el parametro empresa grupo desde parametro base")
                        End If
                    Case TipoNivel.GRUPOEMPRESAS
                        If eNivel.isGrupoEmpresas Then
                            iIdNivel = eNivel.id
                        Else
                            Throw New ParametroNoCreadoException("No se pudo insertar el parametro grupo empresas desde parametro base")
                        End If

                End Select

                If validarCrearDesdeParametrosBase(eParametro.descripcion, iDataRow.Item("tabla"), iIdNivel) Then
                    iGeneradorSql.agregarTabla(iDataRow.Item("tabla"))

                    iGeneradorSql.agregarColumna("descripcion")
                    iGeneradorSql.agregarColumna("valor")
                    iGeneradorSql.agregarColumna("idTipoDato")
                    iGeneradorSql.agregarColumna("idNivel")
                    iGeneradorSql.agregarColumna("singleton")
                    iGeneradorSql.agregarColumna("idTipoParametro")

                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(eParametro.descripcion))
                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(eParametro.valor))
                    iGeneradorSql.agregarValue(iDataRow.Item("idTipoDato"))
                    iGeneradorSql.agregarValue(iIdNivel)


                    iGeneradorSql.agregarValue(iDataRow.Item("singleton"))
                    iGeneradorSql.agregarValue(iDataRow.Item("idTipoParametro"))

                    iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

                End If
            Next

            ParametroSingleton.forzarActualizacion = True

        Catch excepcion As Exception
            Throw New ParametroNoCreadoException(excepcion)
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

    Public Function validarCrearDesdeParametrosBase(ByVal eDescripcion As String, ByVal eNombreTabla As String, ByVal eIdNivel As Long) As Boolean
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDatareader As IDataReader
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla(eNombreTabla)
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("descripcion='" & iDescripcion & "'")
            iGeneradorSql.agregarCondicionWhere("idNivel=" & eIdNivel)

            iDatareader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDatareader.Read

        Catch excepcion As Exception
            Throw New ParametroNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDatareader) AndAlso Not iDatareader.IsClosed Then iDatareader.Close()
            iDatareader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function


    Public Sub eliminar(ByVal eNivel As Nivel)
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla(IIf(iNombreTabla <> Nothing, iNombreTabla, "parametro"))
            If iDescripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion='" & descripcion & "'")
            iGeneradorSql.agregarCondicionWhere("idNivel=" & eNivel.id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iGeneradorSql.agregarTabla(IIf(iNombreTabla <> Nothing, iNombreTabla, "parametroOperaciones"))
            If iDescripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion='" & descripcion & "'")
            iGeneradorSql.agregarCondicionWhere("idNivel=" & eNivel.id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iGeneradorSql.agregarTabla(IIf(iNombreTabla <> Nothing, iNombreTabla, "parametroLiquidacion"))
            If iDescripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion='" & descripcion & "'")
            iGeneradorSql.agregarCondicionWhere("idNivel=" & eNivel.id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iGeneradorSql.agregarTabla(IIf(iNombreTabla <> Nothing, iNombreTabla, "parametroFacturacion"))
            If iDescripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion='" & descripcion & "'")
            iGeneradorSql.agregarCondicionWhere("idNivel=" & eNivel.id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            ParametroSingleton.forzarActualizacion = True

        Catch excepcion As Exception
            Throw New ParametroNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub eliminarParametroOperaciones(ByVal eNivel As Nivel)
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla(IIf(iNombreTabla <> Nothing, iNombreTabla, "parametroOperaciones"))
            If iDescripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion='" & descripcion & "'")
            iGeneradorSql.agregarCondicionWhere("idNivel=" & eNivel.id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)
            ParametroSingleton.forzarActualizacion = True

        Catch excepcion As Exception
            Throw New ParametroNoEliminadoException(excepcion)
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

        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub

    Public Sub actualizarFechaProceso(ByVal eNivel As Nivel)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataSet As DataSet
        Dim i As Integer

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("s.id")
            iGeneradorSql.agregarTabla("Sucursal s")
            If eNivel.isSucursal Then
                iGeneradorSql.agregarCondicionWhere("s.id=" & eNivel.id)
            ElseIf eNivel.isUnidadDeNegocios Then
                iGeneradorSql.agregarCondicionWhere("s.idUnidadDenegocios=" & eNivel.id)
            ElseIf eNivel.isEmpresaGrupo Then
                iGeneradorSql.agregarTabla("UnidadDeNegocios un")
                iGeneradorSql.agregarCondicionWhere("un.id=s.idUnidadDenegocios")
                iGeneradorSql.agregarCondicionWhere("un.idEmpresaGrupo=" & eNivel.id)
            ElseIf eNivel.isGrupoEmpresas Then
                iGeneradorSql.agregarTabla("UnidadDeNegocios un")
                iGeneradorSql.agregarCondicionWhere("un.id=s.idUnidadDenegocios")
                iGeneradorSql.agregarTabla("empresaGrupo eg")
                iGeneradorSql.agregarCondicionWhere("eg.id=un.idEmpresaGrupo")
                iGeneradorSql.agregarCondicionWhere("eg.idgrupoEmpresas=" & eNivel.id)
            End If

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Sucursales")

            For i = 0 To iDataSet.Tables("Sucursales").Rows.Count - 1
                Dim iSucursal As New Sucursal()
                iSucursal.id = iDataSet.Tables("Sucursales").Rows(i).Item("id")
                actualizar(iSucursal)
                iSucursal = Nothing
            Next

            If eNivel.isGrupoEmpresas Then
                actualizar(eNivel)
            End If

        Catch exception As Exception
            Throw New ParametroNoActualizadoException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Public Function obtenerParametroDiasDeVencimeinto(ByVal eNivel As Nivel, ByVal eFechaProceso As Date) As Date
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataSet As DataSet
        Dim iFecha As Date
        Dim iNivel As Nivel = eNivel
        Try
            iConexion = obtenerConexion()

            While (IsNothing(iDataSet) OrElse iDataSet.Tables("Parametro").Rows.Count <= 0) And (Not IsNothing(iNivel))

                iGeneradorSql.agregarTabla("parametro")

                iGeneradorSql.agregarColumna("valor as valor")
                iGeneradorSql.agregarCondicionWhere("descripcion='" & iDescripcion & "'", True)
                iGeneradorSql.agregarCondicionWhere("valor>=" & eFechaProceso.Day, True)
                iGeneradorSql.agregarCondicionWhere("idNivel=" & iNivel.id)
                iGeneradorSql.agregarOrden("valor ASC")
                iGeneradorSql.agregarLimit("1")

                iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametro")

                iNivel = iNivel.padre

            End While

            If iDataSet.Tables("Parametro").Rows.Count > 0 Then
                iValor = CLng(FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametro").Rows.Item(0).Item("valor").ToString))
                If (iValor > DateTime.DaysInMonth(Today.Year, Today.Month)) Then
                    iValor = DateTime.DaysInMonth(Today.Year, Today.Month)
                End If
                iFecha = CDate(iValor & "/" & eFechaProceso.Month & "/" & eFechaProceso.Year)
            Else
                iDataSet = Nothing
                iNivel = eNivel
                While (IsNothing(iDataSet) OrElse iDataSet.Tables("Parametro").Rows.Count <= 0) And (Not IsNothing(iNivel))

                    iGeneradorSql.agregarTabla("parametro")

                    iGeneradorSql.agregarColumna("valor as valor")
                    iGeneradorSql.agregarCondicionWhere("descripcion='" & iDescripcion & "'", True)
                    iGeneradorSql.agregarCondicionWhere("idNivel=" & iNivel.id)
                    iGeneradorSql.agregarOrden("valor ASC")
                    iGeneradorSql.agregarLimit("1")

                    iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametro")

                    iNivel = iNivel.padre
                End While
                If iDataSet.Tables("Parametro").Rows.Count > 0 Then
                    iValor = CLng(FuncionComun.ceroSiEsVacio(iDataSet.Tables("Parametro").Rows.Item(0).Item("valor").ToString))
                    If (iValor > DateTime.DaysInMonth(eFechaProceso.AddMonths(1).Year, eFechaProceso.AddMonths(1).Month)) Then
                        iValor = DateTime.DaysInMonth(eFechaProceso.AddMonths(1).Year, eFechaProceso.AddMonths(1).Month)
                    End If
                    iFecha = CDate(iValor & "/" & eFechaProceso.AddMonths(1).Month & "/" & eFechaProceso.AddMonths(1).Year)
                Else
                    iFecha = eFechaProceso
                End If
            End If
            Return iFecha

        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iDataSet = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Sub obtenerParametrosNivel(ByRef eParametroVO As ParametroVO)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataSet As DataSet
        Dim iParametro As Parametro
        Dim i As Integer

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("parametro")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarCondicionWhere("idNivel=" & eParametroVO.nivel.id)
            If Not IsNothing(eParametroVO.tipoParametro) AndAlso eParametroVO.tipoParametro.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("idTipoParametro=" & eParametroVO.tipoParametro.id)

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")

            eParametroVO.parametro = New List(Of Parametro)

            For i = 0 To iDataSet.Tables("Parametros").Rows.Count - 1

                iParametro = New Parametro
                iParametro.descripcion = iDataSet.Tables("Parametros").Rows(i).Item("descripcion")

                Try
                    iParametro.accesoDatos = iConexion
                    iParametro = iParametro.obtenerParametro(eParametroVO.nivel)
                    eParametroVO.parametro.Add(iParametro)
                Catch ex As Exception
                    eParametroVO.nivel = eParametroVO.nivel
                End Try
                iParametro.accesoDatos = Nothing
                iParametro = Nothing

            Next

            iGeneradorSql.agregarTabla("parametroOperaciones")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarCondicionWhere("idNivel=" & eParametroVO.nivel.id)

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")
            For i = 0 To iDataSet.Tables("Parametros").Rows.Count - 1
                iParametro = New Parametro
                iParametro.descripcion = iDataSet.Tables("Parametros").Rows(i).Item("descripcion")
                iParametro.nombreTabla = "parametroOperaciones"

                Try
                    iParametro.accesoDatos = iConexion
                    iParametro = iParametro.obtenerParametro(eParametroVO.nivel)
                    eParametroVO.parametro.Add(iParametro)
                Catch ex As Exception
                    eParametroVO.nivel = eParametroVO.nivel
                End Try
                iParametro.accesoDatos = Nothing
                iParametro = Nothing
            Next

            iGeneradorSql.agregarTabla("parametroTexto")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarCondicionWhere("idNivel=" & eParametroVO.nivel.id)

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")
            For i = 0 To iDataSet.Tables("Parametros").Rows.Count - 1
                iParametro = New Parametro
                iParametro.descripcion = iDataSet.Tables("Parametros").Rows(i).Item("descripcion")
                iParametro.nombreTabla = "parametroTexto"

                Try
                    iParametro.accesoDatos = iConexion
                    iParametro = iParametro.obtenerParametro(eParametroVO.nivel)
                    eParametroVO.parametro.Add(iParametro)
                Catch ex As Exception
                    eParametroVO.nivel = eParametroVO.nivel
                End Try
                iParametro.accesoDatos = Nothing
                iParametro = Nothing
            Next

            iGeneradorSql.agregarTabla("parametroFacturacion")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarCondicionWhere("idNivel=" & eParametroVO.nivel.id)

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")
            For i = 0 To iDataSet.Tables("Parametros").Rows.Count - 1
                iParametro = New Parametro
                iParametro.descripcion = iDataSet.Tables("Parametros").Rows(i).Item("descripcion")
                iParametro.nombreTabla = "parametroFacturacion"

                Try
                    iParametro.accesoDatos = iConexion
                    iParametro = iParametro.obtenerParametro(eParametroVO.nivel)
                    eParametroVO.parametro.Add(iParametro)
                Catch ex As Exception
                    eParametroVO.nivel = eParametroVO.nivel
                End Try
                iParametro.accesoDatos = Nothing
                iParametro = Nothing
            Next
            iGeneradorSql.agregarTabla("parametroLiquidacion")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarCondicionWhere("idNivel=" & eParametroVO.nivel.id)

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Parametros")
            For i = 0 To iDataSet.Tables("Parametros").Rows.Count - 1
                iParametro = New Parametro
                iParametro.descripcion = iDataSet.Tables("Parametros").Rows(i).Item("descripcion")
                iParametro.nombreTabla = "parametroLiquidacion"

                Try
                    iParametro.accesoDatos = iConexion
                    iParametro = iParametro.obtenerParametro(eParametroVO.nivel)
                    eParametroVO.parametro.Add(iParametro)
                Catch ex As Exception
                    eParametroVO.nivel = eParametroVO.nivel
                End Try
                iParametro.accesoDatos = Nothing
                iParametro = Nothing
            Next

        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
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

    Public Sub New()
        'iTipoParametro = New TipoParametro
        'iTipoParametro.id = TipoParametro.GENERICO
    End Sub

#End Region

End Class