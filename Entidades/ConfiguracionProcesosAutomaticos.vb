Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades
Imports di.financiera.seguridad
Imports System.Collections.Generic

Public Class ConfiguracionProcesosAutomaticos
    Inherits Entidad
#Region "EnumNivelFiltro"
    Public Enum enumTipoFiltroEntidad
        GRUPOEMPRESA = 1
        EMPRESAGRUPO = 2
        UNIDADNEGOCIOS = 3
        SUCURSAL = 4
        COMERCIO = 5
    End Enum

#End Region

#Region "Variables"
    Private iId As Long
    Private iDescripcion As String
    Private iCodigo As String
    Private iEstado As Estado
    Private iEntidades As List(Of Object)
    Private iProcesoCambioEstadoEnProceso As Boolean
    Private iCantidadDiasEnProcesoVencida As Integer
    Private iProcesoCambioEstadoConcretada As Boolean
    Private iCantidadDiasConcretadaVencida As Integer
    Private iVencimientoCuotaConcretadaVencida As Integer
    Private iProcesoCantidadConsultas As Boolean
    Private iCantidadConsultas As Integer
    Private iCantidadDiasAEvaluar As Integer
    Private iTipoFiltroEntidadConsultas As enumTipoFiltroEntidad
    Private iProcesoLiquidacionAutomaticaPorGrupoComercial As Boolean
    Private iGrupoComercial As GrupoComercial
    Private iDiaSemana As Dia
    Private iCantidadDiasCierreLiquidacion As Integer
    Private iCantidadDiasPagoLiquidacion As Integer

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
    Public Property codigo() As String
        Get
            Return iCodigo
        End Get
        Set(ByVal Value As String)
            iCodigo = Value
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
    Public Property entidades() As List(Of Object)
        Get
            Return iEntidades
        End Get
        Set(ByVal Value As List(Of Object))
            iEntidades = Value
        End Set
    End Property
    Public Property cantidadDiasEnProcesoVencida() As Integer
        Get
            Return iCantidadDiasEnProcesoVencida
        End Get
        Set(value As Integer)
            iCantidadDiasEnProcesoVencida = value
        End Set
    End Property
    Public Property cantidadDiasConcretadaVencida() As Integer
        Get
            Return iCantidadDiasConcretadaVencida
        End Get
        Set(value As Integer)
            iCantidadDiasConcretadaVencida = value
        End Set
    End Property
    Public Property vencimientoCuotaConcretadaVencida() As Integer
        Get
            Return iVencimientoCuotaConcretadaVencida
        End Get
        Set(value As Integer)
            iVencimientoCuotaConcretadaVencida = value
        End Set
    End Property

    Public Property cantidadConsultas() As Integer
        Get
            Return iCantidadConsultas
        End Get
        Set(value As Integer)
            iCantidadConsultas = value
        End Set
    End Property
    Public Property cantidadDiasAEvaluar() As Integer
        Get
            Return iCantidadDiasAEvaluar
        End Get
        Set(value As Integer)
            iCantidadDiasAEvaluar = value
        End Set
    End Property
    Public Property procesoCambioEstadoEnProceso() As Boolean
        Get
            Return iProcesoCambioEstadoEnProceso
        End Get
        Set(value As Boolean)
            iProcesoCambioEstadoEnProceso = value
        End Set
    End Property
    Public Property procesoCambioEstadoConcretada() As Boolean
        Get
            Return iProcesoCambioEstadoConcretada
        End Get
        Set(value As Boolean)
            iProcesoCambioEstadoConcretada = value
        End Set
    End Property
    Public Property procesoCantidadConsultas() As Boolean
        Get
            Return iProcesoCantidadConsultas
        End Get
        Set(value As Boolean)
            iProcesoCantidadConsultas = value
        End Set
    End Property
    Public Property tipoFiltroEntidadConsultas() As enumTipoFiltroEntidad
        Get
            Return iTipoFiltroEntidadConsultas
        End Get
        Set(value As enumTipoFiltroEntidad)
            iTipoFiltroEntidadConsultas = value
        End Set
    End Property

    Public Property procesoLiquidacionAutomaticaPorGrupoComercial() As Boolean
        Get
            Return iProcesoLiquidacionAutomaticaPorGrupoComercial
        End Get
        Set(value As Boolean)
            iProcesoLiquidacionAutomaticaPorGrupoComercial = value
        End Set
    End Property

    Public Property grupoComercial() As GrupoComercial
        Get
            Return iGrupoComercial
        End Get
        Set(value As GrupoComercial)
            iGrupoComercial = value
        End Set
    End Property

    Public Property diaSemana() As Dia
        Get
            Return iDiaSemana
        End Get
        Set(value As Dia)
            iDiaSemana = value
        End Set
    End Property

    Public Property cantidadDiasCierreLiquidacion() As Integer
        Get
            Return iCantidadDiasCierreLiquidacion
        End Get
        Set(value As Integer)
            iCantidadDiasCierreLiquidacion = value
        End Set
    End Property

    Public Property cantidadDiasPagoLiquidacion() As Integer
        Get
            Return iCantidadDiasPagoLiquidacion
        End Get
        Set(value As Integer)
            iCantidadDiasPagoLiquidacion = value
        End Set
    End Property

#End Region

#Region "Metodos"

    Private Sub validarCrear()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Try
            If descripcion = Nothing Then
                Throw New RootException("La descripcion no puede ser nula")
            End If
            If codigo = Nothing Then
                Throw New RootException("El codigo no puede ser nulo")
            End If

            If IsNothing(estado) Then
                Throw New RootException("El estado no puede ser nulo")
            End If

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("ConfiguracionProcesosAutomaticos")
            iGeneradorSql.agregarCondicionWhere("codigo='" & iCodigo & "'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New RootException("El codigo de Configuracion de proceso automaticos ya existe")
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
        Dim i As Integer
        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("ConfiguracionProcesosAutomaticos")

            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("idestado")
            iGeneradorSql.agregarColumna("procesoCambioEstadoEnProceso")
            iGeneradorSql.agregarColumna("cantidadDiasEnProcesoVencida")
            iGeneradorSql.agregarColumna("procesoCambioEstadoConcretada")
            iGeneradorSql.agregarColumna("cantidadDiasConcretadaVencida")
            iGeneradorSql.agregarColumna("vencimientoCuotaConcretadaVencida")
            iGeneradorSql.agregarColumna("procesoCantidadConsultas")
            iGeneradorSql.agregarColumna("cantidadConsultas")
            iGeneradorSql.agregarColumna("cantidadDiasAEvaluar")
            iGeneradorSql.agregarColumna("TipoFiltroEntidadConsultas")
            iGeneradorSql.agregarColumna("procesoLiquidacionAutomaticaPorGrupoComercial")
            iGeneradorSql.agregarColumna("idgrupoComercial")
            iGeneradorSql.agregarColumna("iddiaSemana")
            iGeneradorSql.agregarColumna("cantidadDiasCierreLiquidacion")
            iGeneradorSql.agregarColumna("cantidadDiasPagoLiquidacion")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(descripcion))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(codigo))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(estado.id))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(procesoCambioEstadoEnProceso))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(cantidadDiasEnProcesoVencida))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(procesoCambioEstadoConcretada))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(cantidadDiasConcretadaVencida))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(vencimientoCuotaConcretadaVencida))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(procesoCantidadConsultas))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(cantidadConsultas))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(cantidadDiasAEvaluar))
            iGeneradorSql.agregarValue(iTipoFiltroEntidadConsultas)
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(procesoLiquidacionAutomaticaPorGrupoComercial))
            If Not IsNothing(grupoComercial) Then
                iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(grupoComercial.id))
            Else
                iGeneradorSql.agregarValue("NULL")
            End If
            If Not IsNothing(diaSemana) Then
                iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(diaSemana.id))
            Else
                iGeneradorSql.agregarValue("NULL")
            End If
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(cantidadDiasCierreLiquidacion))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(cantidadDiasPagoLiquidacion))

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

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
        Try

            If descripcion = Nothing Then
                Throw New RootException("La descripcion no puede ser nula")
            End If
            If codigo = Nothing Then
                Throw New RootException("El codigo no puede ser nulo")
            End If
            If IsNothing(estado) Then
                Throw New RootException("El estado no puede ser nulo")
            End If

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        End Try
    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql
        Dim i As Integer
        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarSet("descripcion=" & FuncionComun.nuloSiEsNothing(descripcion))
            iGeneradorSql.agregarSet("codigo=" & FuncionComun.nuloSiEsNothing(codigo))
            iGeneradorSql.agregarSet("idestado=" & FuncionComun.nuloSiEsNothing(estado.id))

            iGeneradorSql.agregarSet("procesoCambioEstadoEnProceso=" & FuncionComun.booleanByte(procesoCambioEstadoEnProceso))
            iGeneradorSql.agregarSet("procesoCambioEstadoConcretada=" & FuncionComun.booleanByte(procesoCambioEstadoConcretada))
            iGeneradorSql.agregarSet("procesoCantidadConsultas=" & FuncionComun.booleanByte(procesoCantidadConsultas))
            iGeneradorSql.agregarSet("procesoLiquidacionAutomaticaPorGrupoComercial=" & FuncionComun.booleanByte(procesoLiquidacionAutomaticaPorGrupoComercial))

            iGeneradorSql.agregarSet("cantidadDiasEnProcesoVencida=" & FuncionComun.nuloSiEsNothing(cantidadDiasEnProcesoVencida))
            iGeneradorSql.agregarSet("cantidadDiasConcretadaVencida=" & FuncionComun.nuloSiEsNothing(cantidadDiasConcretadaVencida))
            iGeneradorSql.agregarSet("vencimientoCuotaConcretadaVencida=" & FuncionComun.nuloSiEsNothing(vencimientoCuotaConcretadaVencida))
            iGeneradorSql.agregarSet("cantidadConsultas=" & FuncionComun.nuloSiEsNothing(cantidadConsultas))
            iGeneradorSql.agregarSet("cantidadDiasAEvaluar=" & FuncionComun.nuloSiEsNothing(cantidadDiasAEvaluar))
            iGeneradorSql.agregarSet("TipoFiltroEntidadConsultas=" & iTipoFiltroEntidadConsultas)
            If Not IsNothing(grupoComercial) Then
                iGeneradorSql.agregarSet("idgrupoComercial=" & FuncionComun.nuloSiEsNothing(grupoComercial.id))
            End If
            If Not IsNothing(diaSemana) Then
                iGeneradorSql.agregarSet("iddiaSemana=" & FuncionComun.nuloSiEsNothing(diaSemana.id))
            End If
            iGeneradorSql.agregarSet("cantidadDiasCierreLiquidacion=" & FuncionComun.ceroSiEsNothing(cantidadDiasCierreLiquidacion))
            iGeneradorSql.agregarSet("cantidadDiasPagoLiquidacion=" & FuncionComun.ceroSiEsNothing(cantidadDiasPagoLiquidacion))

            iGeneradorSql.agregarTabla("ConfiguracionProcesosAutomaticos")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

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

    Public Function obtenerConfiguracionProcesosAutomaticos(Optional eObtenerEntidades As Boolean = True) As ConfiguracionProcesosAutomaticos
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("idestado")
            iGeneradorSql.agregarColumna("procesoCambioEstadoEnProceso")
            iGeneradorSql.agregarColumna("cantidadDiasEnProcesoVencida")
            iGeneradorSql.agregarColumna("procesoCambioEstadoConcretada")
            iGeneradorSql.agregarColumna("cantidadDiasConcretadaVencida")
            iGeneradorSql.agregarColumna("vencimientoCuotaConcretadaVencida")
            iGeneradorSql.agregarColumna("procesoCantidadConsultas")
            iGeneradorSql.agregarColumna("cantidadConsultas")
            iGeneradorSql.agregarColumna("cantidadDiasAEvaluar")
            iGeneradorSql.agregarColumna("TipoFiltroEntidadConsultas")
            iGeneradorSql.agregarColumna("procesoLiquidacionAutomaticaPorGrupoComercial")
            iGeneradorSql.agregarColumna("idgrupoComercial")
            iGeneradorSql.agregarColumna("iddiaSemana")
            iGeneradorSql.agregarColumna("cantidadDiasCierreLiquidacion")
            iGeneradorSql.agregarColumna("cantidadDiasPagoLiquidacion")

            iGeneradorSql.agregarTabla("ConfiguracionProcesosAutomaticos")

            iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))


            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = FuncionComun.nothingSiEsNulo(iDataReader.Item("Id"))
                iDescripcion = FuncionComun.nothingSiEsNulo(iDataReader.Item("Descripcion"))
                iCodigo = FuncionComun.nothingSiEsNulo(iDataReader.Item("Codigo"))
                iEstado = IIf(iDataReader.Item("idEstado").ToString = Estado.ALTA, New Alta, New Baja)

                iProcesoCambioEstadoEnProceso = FuncionComun.byteBoolean(iDataReader.Item("procesoCambioEstadoEnProceso"))
                iProcesoCambioEstadoConcretada = FuncionComun.byteBoolean(iDataReader.Item("procesoCambioEstadoConcretada"))
                iProcesoCantidadConsultas = FuncionComun.byteBoolean(iDataReader.Item("ProcesoCantidadConsultas"))
                iProcesoLiquidacionAutomaticaPorGrupoComercial = FuncionComun.byteBoolean(iDataReader.Item("procesoLiquidacionAutomaticaPorGrupoComercial"))

                iCantidadDiasEnProcesoVencida = FuncionComun.nothingSiEsNulo(iDataReader.Item("cantidadDiasEnProcesoVencida"))
                iCantidadDiasConcretadaVencida = FuncionComun.nothingSiEsNulo(iDataReader.Item("cantidadDiasConcretadaVencida"))
                iVencimientoCuotaConcretadaVencida = FuncionComun.nothingSiEsNulo(iDataReader.Item("vencimientoCuotaConcretadaVencida"))
                iCantidadConsultas = FuncionComun.nothingSiEsNulo(iDataReader.Item("cantidadConsultas"))
                iCantidadDiasAEvaluar = FuncionComun.nothingSiEsNulo(iDataReader.Item("cantidadDiasAEvaluar"))
                iTipoFiltroEntidadConsultas = iDataReader.Item("TipoFiltroEntidadConsultas").ToString
                If Not IsDBNull(iDataReader.Item("idgrupoComercial")) Then
                    iGrupoComercial = New GrupoComercial
                    iGrupoComercial.id = iDataReader.Item("idgrupoComercial")
                End If
                If Not IsDBNull(iDataReader.Item("iddiaSemana")) Then
                    iDiaSemana = New Dia
                    iDiaSemana.id = iDataReader.Item("iddiaSemana")
                End If
                iCantidadDiasCierreLiquidacion = FuncionComun.nothingSiEsNulo(iDataReader.Item("cantidadDiasCierreLiquidacion"))
                iCantidadDiasPagoLiquidacion = FuncionComun.nothingSiEsNulo(iDataReader.Item("cantidadDiasPagoLiquidacion"))

                iDataReader.Close()

                If Not IsNothing(iGrupoComercial) Then
                    iGrupoComercial.accesoDatos = iConexion
                    iGrupoComercial.obtenerGrupoComercial()
                    iGrupoComercial.accesoDatos = Nothing
                End If

                If Not IsNothing(iDiaSemana) Then
                    iDiaSemana.accesoDatos = iConexion
                    iDiaSemana.obtenerDia()
                    iDiaSemana.accesoDatos = Nothing
                End If

                Return Me
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

    End Function

    Public Function obtenerConfiguracionProcesosAutomaticosSoloIds() As ConfiguracionProcesosAutomaticos
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("idestado")
            iGeneradorSql.agregarColumna("procesoCambioEstadoEnProceso")
            iGeneradorSql.agregarColumna("cantidadDiasEnProcesoVencida")
            iGeneradorSql.agregarColumna("procesoCambioEstadoConcretada")
            iGeneradorSql.agregarColumna("cantidadDiasConcretadaVencida")
            iGeneradorSql.agregarColumna("vencimientoCuotaConcretadaVencida")
            iGeneradorSql.agregarColumna("procesoCantidadConsultas")
            iGeneradorSql.agregarColumna("cantidadConsultas")
            iGeneradorSql.agregarColumna("cantidadDiasAEvaluar")
            iGeneradorSql.agregarColumna("TipoFiltroEntidadConsultas")
            iGeneradorSql.agregarColumna("procesoLiquidacionAutomaticaPorGrupoComercial")
            iGeneradorSql.agregarColumna("idgrupoComercial")
            iGeneradorSql.agregarColumna("iddiaSemana")
            iGeneradorSql.agregarColumna("cantidadDiasCierreLiquidacion")
            iGeneradorSql.agregarColumna("cantidadDiasPagoLiquidacion")

            iGeneradorSql.agregarTabla("ConfiguracionProcesosAutomaticos")

            If iId <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))
            If iCodigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("Codigo=" & iCodigo)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = FuncionComun.nothingSiEsNulo(iDataReader.Item("Id"))
                iDescripcion = FuncionComun.nothingSiEsNulo(iDataReader.Item("Descripcion"))
                iCodigo = FuncionComun.nothingSiEsNulo(iDataReader.Item("Codigo"))
                iEstado = IIf(iDataReader.Item("idEstado").ToString = Estado.ALTA, New Alta, New Baja)

                iProcesoCambioEstadoEnProceso = FuncionComun.byteBoolean(iDataReader.Item("procesoCambioEstadoEnProceso"))
                iProcesoCambioEstadoConcretada = FuncionComun.byteBoolean(iDataReader.Item("procesoCambioEstadoEnProceso"))
                iProcesoCantidadConsultas = FuncionComun.byteBoolean(iDataReader.Item("ProcesoCantidadConsultas"))
                iProcesoLiquidacionAutomaticaPorGrupoComercial = FuncionComun.byteBoolean(iDataReader.Item("procesoLiquidacionAutomaticaPorGrupoComercial"))

                iCantidadDiasEnProcesoVencida = FuncionComun.nothingSiEsNulo(iDataReader.Item("cantidadDiasEnProcesoVencida"))
                iCantidadDiasConcretadaVencida = FuncionComun.nothingSiEsNulo(iDataReader.Item("cantidadDiasConcretadaVencida"))
                iVencimientoCuotaConcretadaVencida = FuncionComun.nothingSiEsNulo(iDataReader.Item("vencimientoCuotaConcretadaVencida"))
                iCantidadConsultas = FuncionComun.nothingSiEsNulo(iDataReader.Item("cantidadConsultas"))
                iCantidadDiasAEvaluar = FuncionComun.nothingSiEsNulo(iDataReader.Item("cantidadDiasAEvaluar"))
                iTipoFiltroEntidadConsultas = iDataReader.Item("TipoFiltroEntidadConsultas").ToString
                If Not IsDBNull(iDataReader.Item("idgrupoComercial")) Then
                    iGrupoComercial = New GrupoComercial
                    iGrupoComercial.id = iDataReader.Item("idgrupoComercial")
                End If
                If Not IsDBNull(iDataReader.Item("iddiaSemana")) Then
                    iDiaSemana = New Dia
                    iDiaSemana.id = iDataReader.Item("iddiaSemana")
                End If
                iCantidadDiasCierreLiquidacion = FuncionComun.nothingSiEsNulo(iDataReader.Item("cantidadDiasCierreLiquidacion"))
                iCantidadDiasPagoLiquidacion = FuncionComun.nothingSiEsNulo(iDataReader.Item("cantidadDiasPagoLiquidacion"))

                iDataReader.Close()

                If Not IsNothing(iGrupoComercial) Then
                    iGrupoComercial.accesoDatos = iConexion
                    iGrupoComercial.obtenerGrupoComercial()
                    iGrupoComercial.accesoDatos = Nothing
                End If

                If Not IsNothing(iDiaSemana) Then
                    iDiaSemana.accesoDatos = iConexion
                    iDiaSemana.obtenerDia()
                    iDiaSemana.accesoDatos = Nothing
                End If

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
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Function obtenerConfiguracionProcesosAutomaticosParaProceso() As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataSet As New DataSet

        Try
            iConexion = obtenerConexion()



            iGeneradorSql.agregarColumna("c.id")
            iGeneradorSql.agregarColumna("c.codigo")
            iGeneradorSql.agregarColumna("c.descripcion")
            iGeneradorSql.agregarColumna("e.descripcion as estado")

            iGeneradorSql.agregarTabla("ConfiguracionProcesosAutomaticos c")
            iGeneradorSql.agregarTabla("Estado e")
            iGeneradorSql.agregarCondicionWhere("c.idEstado=e.id")

            iGeneradorSql.agregarCondicionWhere("c.idEstado=" & Estado.ALTA)

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "ConfiguracionProcesosAutomaticos")

            Return iDataSet

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iDataSet = Nothing
        End Try
    End Function


    Private Sub validarEliminar()
        If id = Nothing Then
            Throw New RootException("El id no puede ser nulo.")
        End If
    End Sub

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()
            validarEliminar()


            iGeneradorSql.agregarTabla("ConfiguracionProcesosAutomaticosEntidad")
            iGeneradorSql.agregarCondicionWhere("idConfiguracionProcesosAutomaticos=" & id)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iGeneradorSql.agregarTabla("ConfiguracionProcesosAutomaticos")
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
#End Region

End Class