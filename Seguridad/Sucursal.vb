Imports System.Collections.Generic
Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades

Public Class Sucursal : Inherits Nivel

#Region "Enumerados"
    Public Enum EnumTipoSucursal
        TODO = 1
        WEB = 2
        AUTOGESTION = 3
    End Enum
#End Region

#Region "Constantes"
    Public Const SUCURSALCASACENTR = 52
#End Region

#Region "Variables"
    Private iCodigo As String
    Private iDomicilio As domicilio
    Private iEstado As estado
    Private iConexion As accesoDatos
    Private iTipoSucursal As EnumTipoSucursal
    Private iCodigoTarjeta As String
#End Region

#Region "Atributos"
    Public Property codigo() As String
        Get
            Return iCodigo
        End Get
        Set(ByVal Value As String)
            iCodigo = Value
        End Set
    End Property
    Public Property domicilio() As domicilio
        Get
            Return iDomicilio
        End Get
        Set(ByVal Value As domicilio)
            iDomicilio = Value
        End Set
    End Property
    Public Property estado() As estado
        Get
            Return iEstado
        End Get
        Set(ByVal Value As estado)
            iEstado = Value
        End Set
    End Property
    Public Property tipoSucursal() As EnumTipoSucursal
        Get
            Return iTipoSucursal
        End Get
        Set(ByVal Value As EnumTipoSucursal)
            iTipoSucursal = Value
        End Set
    End Property
    Public Property codigoTarjeta As String
        Get
            Return iCodigoTarjeta
        End Get
        Set(value As String)
            iCodigoTarjeta = value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Overrides Function isSucursal() As Boolean
        Return True
    End Function

    Public Overrides Function isUnidadDeNegocios() As Boolean
        Return False
    End Function

    Public Overrides Function isEmpresaGrupo() As Boolean
        Return False
    End Function

    Public Overrides Function isGrupoEmpresas() As Boolean
        Return False
    End Function

    Public Overrides Function isPuntoVentaDgi() As Boolean
        Return False
    End Function

    Public Overrides Function obtenerNivel(Optional eObtenerDomicilio As Boolean = True) As Nivel
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iSucursal As Sucursal

        Try

            If id <> Nothing Then
                iSucursal = NivelSingleton.getInstancia(False).listaNiveles.Find(Function(p) p.id = id)

                id = iSucursal.id
                descripcion = iSucursal.descripcion
                iCodigo = iSucursal.codigo
                iDomicilio = iSucursal.domicilio
                padre = iSucursal.padre
                iEstado = iSucursal.estado
                iTipoSucursal = iSucursal.tipoSucursal
                iCodigoTarjeta = iSucursal.codigoTarjeta

                padre.accesoDatos = iConexion
                padre.obtenerNivel(eObtenerDomicilio)
                padre.accesoDatos = Nothing

                Return Me
            Else
                iConexion = obtenerConexion()

                iGeneradorSql.agregarColumna("s.id")
                iGeneradorSql.agregarColumna("s.codigo")
                iGeneradorSql.agregarColumna("s.idDomicilio")
                iGeneradorSql.agregarColumna("s.idEstado")
                iGeneradorSql.agregarColumna("s.idUnidadDeNegocios")
                iGeneradorSql.agregarColumna("s.tipoSucursal")
                iGeneradorSql.agregarColumna("s.codigoTarjeta")

                iGeneradorSql.agregarTabla("sucursal s")
                If codigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("s.codigo=" & codigo)
                If descripcion <> Nothing Then
                    iGeneradorSql.agregarTabla("nivel n")
                    iGeneradorSql.agregarCondicionWhere("n.id=s.id")
                    iGeneradorSql.agregarCondicionWhere("n.descripcion=" & FuncionComun.nuloSiEsNothing(descripcion))
                End If
                If Not IsNothing(padre) Then
                    iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=" & CType(padre, UnidadDeNegocios).id)
                End If
                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                If iDataReader.Read Then

                    id = iDataReader.Item("id").ToString
                    iCodigo = iDataReader.Item("codigo").ToString
                    iDomicilio = New Domicilio
                    iDomicilio.id = iDataReader.Item("idDomicilio").ToString
                    padre = New UnidadDeNegocios
                    padre.id = iDataReader.Item("idUnidadDeNegocios").ToString
                    iEstado = IIf(iDataReader.Item("idEstado").ToString = Estado.ALTA, New Alta, New Baja)
                    iTipoSucursal = iDataReader.Item("tipoSucursal")
                    iCodigoTarjeta = iDataReader.Item("codigoTarjeta").ToString
                    iDataReader.Close()

                    MyBase.accesoDatos = iConexion
                    MyBase.obtenerNivel()

                    If eObtenerDomicilio Then
                        iDomicilio.accesoDatos = iConexion
                        iDomicilio = iDomicilio.obtenerDomicilio
                        iDomicilio.accesoDatos = Nothing
                    End If

                    padre.accesoDatos = iConexion
                    padre.obtenerNivel(eObtenerDomicilio)
                    padre.accesoDatos = Nothing

                    Return Me
                Else
                    Throw New SucursalNoEncontradaException
                End If
            End If

        Catch excepcion As Exception
            Throw New SucursalNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) AndAlso Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Overrides Function obtenerNiveles(Optional ByVal eNivel As Nivel = Nothing) As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iDataSetSingleton As DataSet
        Dim iDataView As DataView
        Dim iFiltro As String
        Try
            iDataSetSingleton = NivelSingleton.getInstancia(False).dataSet

            If Not IsNothing(iDataSetSingleton) Then

                iDataView = iDataSetSingleton.Tables("Niveles").DefaultView
                iFiltro = "idtipoNivel=" & TipoNivel.SUCURSAL

                If Not IsNothing(eNivel) Then
                    If eNivel.isSucursal Then
                        iFiltro &= " and idUnidadDeNegocios=" & eNivel.padre.id
                    ElseIf eNivel.isUnidadDeNegocios Then
                        iFiltro &= " and idUnidadDeNegocios=" & eNivel.id
                    ElseIf eNivel.isEmpresaGrupo Then
                        iFiltro &= " and idEmpresaGrupo=" & eNivel.id
                    ElseIf eNivel.isGrupoEmpresas Then
                        iFiltro &= " and idGrupoEmpresas=" & eNivel.id
                    End If
                End If

                iDataView.RowFilter = iFiltro
                iDataReader = iDataView.ToTable.CreateDataReader()

            Else

                iConexion = obtenerConexion()

                iGeneradorSql.agregarColumna("s.id")
                iGeneradorSql.agregarColumna(FuncionComun.sqlRellenarAIzquierda("s.codigo", "4", "0") & " as codigo")
                iGeneradorSql.agregarColumna(FuncionComun.sqlConcatenar(FuncionComun.sqlRellenarAIzquierda("s.codigo", 4, "0") & " ,'  - ',n.descripcion") & " as descripcion")
                iGeneradorSql.agregarColumna("n.descripcion as solodescripcion")
                iGeneradorSql.agregarColumna("s.tipoSucursal")

                iGeneradorSql.agregarTabla("sucursal s")
                iGeneradorSql.agregarTabla("nivel n")
                If descripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion like '" & descripcion & "%'")

                iGeneradorSql.agregarCondicionWhere("s.id=n.id")
                iGeneradorSql.agregarCondicionWhere("s.idEstado=" & Estado.ALTA)
                If Not IsNothing(eNivel) Then
                    If eNivel.isSucursal Then
                        iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=" & eNivel.padre.id)
                    ElseIf eNivel.isUnidadDeNegocios Then
                        iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=" & eNivel.id)
                    ElseIf eNivel.isEmpresaGrupo Then
                        iGeneradorSql.agregarTabla("UnidadDeNegocios u")
                        iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=u.id")
                        iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=" & eNivel.id)
                    ElseIf eNivel.isGrupoEmpresas Then
                        iGeneradorSql.agregarTabla("UnidadDeNegocios u")
                        iGeneradorSql.agregarTabla("empresaGrupo eg")
                        iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=u.id")
                        iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=eg.id")
                        iGeneradorSql.agregarCondicionWhere("eg.idGrupoEmpresas=" & eNivel.id)
                    End If
                End If
                iGeneradorSql.agregarOrden("codigo asc")

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            End If

            Return iDataReader

        Catch excepcion As Exception
            Throw New SucursalNoEncontradaException(excepcion)
        Finally
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Overrides Function obtenerNivelesLista(eNivelOrigen As Nivel) As List(Of Nivel)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iSucursal As Sucursal
        Dim iDataSet As DataSet
        Dim iNiveles As List(Of Nivel)

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("s.id")
            iGeneradorSql.agregarTabla("Sucursal s")
            iGeneradorSql.agregarTabla("EmpresaGrupo eg")
            iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=eg.id")
            iGeneradorSql.agregarTabla("unidaddenegocios u")
            iGeneradorSql.agregarCondicionWhere("u.id=s.idunidaddenegocios")

            If eNivelOrigen.isGrupoEmpresas Then
                iGeneradorSql.agregarCondicionWhere("eg.idGrupoEmpresas=" & eNivelOrigen.padre.padre.padre.id)
            ElseIf eNivelOrigen.isEmpresaGrupo Then
                iGeneradorSql.agregarCondicionWhere("eg.id=" & eNivelOrigen.padre.padre.id)
            ElseIf eNivelOrigen.isUnidadDeNegocios Then
                iGeneradorSql.agregarCondicionWhere("u.id=" & eNivelOrigen.padre.id)
            ElseIf eNivelOrigen.isSucursal Then
                iGeneradorSql.agregarCondicionWhere("s.id=" & eNivelOrigen.id)
            End If

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Niveles")


            For Each iDataRow As DataRow In iDataSet.Tables("Niveles").Rows
                If IsNothing(iNiveles) Then iNiveles = New List(Of Nivel)

                iSucursal = New Sucursal
                iSucursal.id = iDataRow.Item("id")
                iSucursal.accesoDatos = iConexion
                iSucursal = iSucursal.obtenerNivel(False)
                iSucursal.accesoDatos = Nothing
                iNiveles.Add(iSucursal)
                iSucursal = Nothing
            Next

            Return iNiveles

        Catch excepcion As Exception
            Throw New SucursalNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iSucursal = Nothing
            iDataSet = Nothing
            iNiveles = Nothing
        End Try
    End Function

    Public Overrides Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

            If Not IsNothing(iDomicilio) Then iDomicilio.dispose()

            MyBase.dispose()

        Catch exception As exception
            Throw New RootException(exception)
        End Try

    End Sub

    Public Function obtenerIdSucursal() As Integer
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iIdSucursal As Integer

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("sucursal")
            iGeneradorSql.agregarCondicionWhere("codigo=" & codigo)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            While iDataReader.Read
                iIdSucursal = FuncionComun.ceroSiEsVacio(iDataReader.Item("id").ToString)
            End While

            Return iIdSucursal

        Catch excepcion As Exception
            Throw New SucursalNoEncontradaException(excepcion)
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
    Public Overrides Function obtenerSucursalNivel() As String
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iStringId As String = ""
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("sucursal")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            While iDataReader.Read
                iStringId = iDataReader.Item("id").ToString & ","
            End While

            Return iStringId

        Catch excepcion As Exception
            Throw New SucursalNoEncontradaException(excepcion)
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

    Public Overrides Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            MyBase.accesoDatos = iConexion
            MyBase.crear()

            iDomicilio.accesoDatos = iConexion
            iDomicilio.crear()
            iDomicilio.accesoDatos = Nothing

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("idEstado")
            iGeneradorSql.agregarColumna("idDomicilio")
            iGeneradorSql.agregarColumna("idUnidadDeNegocios")
            iGeneradorSql.agregarColumna("tipoSucursal")

            iGeneradorSql.agregarValue(id)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(codigo))
            iGeneradorSql.agregarValue(estado.ALTA)
            iGeneradorSql.agregarValue(iDomicilio.id)
            iGeneradorSql.agregarValue(padre.id)
            iGeneradorSql.agregarValue(iTipoSucursal)

            iGeneradorSql.agregarTabla("sucursal")

            iConexion.ejecutar(iGeneradorSql.GenerarInsert, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New SucursalNoCreadaException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing

        End Try
    End Sub

    Private Sub validarCrear()
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try

            If iCodigo = Nothing Then Throw New SucursalNoCreadaException("El código no puede ser nulo")
            If descripcion = Nothing Then Throw New SucursalNoCreadaException("La descripción no puede ser nula")
            iConexion = obtenerConexion()
            iGeneradorSql.agregarTabla("Sucursal")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarCondicionWhere("codigo=" & iCodigo)
            iGeneradorSql.agregarCondicionWhere("idunidaddenegocios=" & padre.id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New SucursalNoCreadaException("El código de sucursal ya existe en la base de datos")
            End If

            iDataReader.Close()

        Catch excepcion As ErrorConexionException
            Throw New SucursalNoCreadaException(excepcion)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            If IsNothing(MyBase.accesoDatos) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Public Overrides Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            MyBase.accesoDatos = iConexion
            MyBase.modificar()

            iDomicilio.accesoDatos = iConexion
            iDomicilio.modificar()
            iDomicilio.accesoDatos = Nothing

            iGeneradorSql.agregarTabla("Sucursal")
            iGeneradorSql.agregarSet("codigo=" & FuncionComun.nuloSiEsNothing(iCodigo))
            iGeneradorSql.agregarSet("idUnidadDeNegocios=" & padre.id)
            iGeneradorSql.agregarSet("tiposucursal=" & FuncionComun.ceroSiEsNulo(iTipoSucursal))
            iGeneradorSql.agregarSet("idEstado=" & IIf(iEstado.isAlta, iEstado.ALTA, iEstado.BAJA))

            iGeneradorSql.agregarCondicionWhere("id = " & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New SucursalNoModificadaException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Overrides Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            validarEliminar()

            MyBase.accesoDatos = iConexion
            MyBase.eliminar()

            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iGeneradorSql.agregarTabla("sucursal")
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iDomicilio.accesoDatos = iConexion
            iDomicilio.eliminar()
            iDomicilio.accesoDatos = Nothing

        Catch exception As Exception
            Throw New SucursalNoEliminadaException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub validarEliminar()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idSucursal=" & id)
            iGeneradorSql.agregarTabla("comprobante")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New SucursalNoEliminadaException("La sucursal tiene comprobantes")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idSucursal=" & id)
            iGeneradorSql.agregarTabla("comercio")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New SucursalNoEliminadaException("La sucursal tiene comercio")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idSucursal=" & id)
            iGeneradorSql.agregarTabla("recibo")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New SucursalNoEliminadaException("La sucursal tiene recibos")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idSucursal=" & id)
            iGeneradorSql.agregarTabla("persona")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New SucursalNoEliminadaException("La sucursal tiene clientes o garantes")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idSucursal=" & id)
            iGeneradorSql.agregarTabla("solicitud")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New SucursalNoEliminadaException("La sucursal tiene solicitudes")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idSucursalCarga=" & id)
            iGeneradorSql.agregarTabla("solicitud")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New SucursalNoEliminadaException("La sucursal tiene solicitudes")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idSucursal=" & id)
            iGeneradorSql.agregarTabla("liquidacion")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New SucursalNoEliminadaException("La sucursal tiene liquidaciones")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idNivel=" & id)
            iGeneradorSql.agregarTabla("usuario")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New SucursalNoEliminadaException("La sucursal tiene usuarios")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idSucursal=" & id)
            iGeneradorSql.agregarTabla("autorizacion")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New SucursalNoEliminadaException("La sucursal tiene autorizaciones")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New SucursalNoEncontradaException(excepcion)
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
    End Sub

    Public Function obtenerSucursalesGrilla(ByVal eNivel As Nivel) As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataSet As DataSet

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("s.id")
            iGeneradorSql.agregarColumna("n.descripcion as unidadDeNegocios")
            iGeneradorSql.agregarColumna("s.codigo")
            iGeneradorSql.agregarColumna("n2.descripcion as sucursal")
            iGeneradorSql.agregarTabla("sucursal s")
            iGeneradorSql.agregarTabla("nivel n")
            iGeneradorSql.agregarTabla("nivel n2")
            iGeneradorSql.agregarCondicionWhere("n.id=s.idunidaddeNegocios")
            iGeneradorSql.agregarCondicionWhere("n2.id=s.id")

            If codigo <> 0 Then iGeneradorSql.agregarCondicionWhere("s.codigo=" & codigo)
            If tipoSucursal <> 0 Then
                Select Case tipoSucursal
                    Case EnumTipoSucursal.AUTOGESTION
                        iGeneradorSql.agregarCondicionWhere("s.tipoSucursal=" & EnumTipoSucursal.AUTOGESTION & " or s.tipoSucursal=" & EnumTipoSucursal.TODO)
                    Case EnumTipoSucursal.WEB
                        iGeneradorSql.agregarCondicionWhere("s.tipoSucursal=" & EnumTipoSucursal.WEB & " or s.tipoSucursal=" & EnumTipoSucursal.TODO)
                End Select
            End If
            If descripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("n2.descripcion like '" & descripcion & "%'")
            If Not IsNothing(iEstado) Then iGeneradorSql.agregarCondicionWhere("s.idEstado=" & iEstado.id)

            If Not IsNothing(eNivel) Then
                If eNivel.isUnidadDeNegocios Then
                    iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=" & eNivel.id)
                ElseIf eNivel.isEmpresaGrupo Then
                    iGeneradorSql.agregarTabla("unidaddenegocios un")
                    iGeneradorSql.agregarCondicionWhere("un.id=s.idunidaddenegocios")
                    iGeneradorSql.agregarCondicionWhere("un.idEmpresaGrupo=" & eNivel.id)
                ElseIf eNivel.isGrupoEmpresas Then
                    iGeneradorSql.agregarTabla("EmpresaGrupo eg")
                    iGeneradorSql.agregarTabla("unidaddenegocios un")
                    iGeneradorSql.agregarCondicionWhere("un.id=s.idunidaddenegocios")
                    iGeneradorSql.agregarCondicionWhere("eg.id=un.idEmpresaGrupo")
                    iGeneradorSql.agregarCondicionWhere("eg.idGrupoEmpresas=" & eNivel.id)
                End If
            End If

            iGeneradorSql.agregarOrden("s.codigo ASC")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "SucursalGrilla")

            Return iDataSet

        Catch exception As Exception
            Throw New SucursalNoEncontradaException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerSucursales(Optional ByVal eNivel As Nivel = Nothing, Optional ByVal eObtenerSucursalCargaLegajo As Boolean = False) As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna(FuncionComun.sqlRellenarAIzquierda("s.codigo", "4", "0") & " as codigo")
            iGeneradorSql.agregarColumna(FuncionComun.sqlConcatenar("nu.descripcion,' - '," & FuncionComun.sqlRellenarAIzquierda("s.codigo", 4, "0") & ",'  - ',n.descripcion") & " as descripcion")
            iGeneradorSql.agregarColumna("s.id")
            iGeneradorSql.agregarColumna("n.descripcion as descripcionOrden2")
            iGeneradorSql.agregarColumna("nu.descripcion as descripcionOrden1")
            iGeneradorSql.agregarTabla("sucursal s")
            iGeneradorSql.agregarTabla("nivel nu")
            iGeneradorSql.agregarTabla("nivel n")
            iGeneradorSql.agregarCondicionWhere("s.id=n.id")
            iGeneradorSql.agregarCondicionWhere("s.idunidaddenegocios=nu.id")
            iGeneradorSql.agregarCondicionWhere("s.idEstado=" & Estado.ALTA)
            If eObtenerSucursalCargaLegajo Then
                iGeneradorSql.agregarCondicionWhere("s.cargaLegajo= " & FuncionComun.booleanByte(True))
            End If
            If Not IsNothing(eNivel) Then
                If eNivel.isSucursal Then
                    iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=" & eNivel.padre.id)
                ElseIf eNivel.isUnidadDeNegocios Then
                    iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=" & eNivel.id)
                ElseIf eNivel.isEmpresaGrupo Then
                    iGeneradorSql.agregarTabla("UnidadDeNegocios u")
                    iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=u.id")
                    iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=" & eNivel.id)
                ElseIf eNivel.isGrupoEmpresas Then
                    iGeneradorSql.agregarTabla("UnidadDeNegocios u")
                    iGeneradorSql.agregarTabla("empresaGrupo eg")
                    iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=u.id")
                    iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=eg.id")
                    iGeneradorSql.agregarCondicionWhere("eg.idGrupoEmpresas=" & eNivel.id)
                End If
            End If
            iGeneradorSql.agregarOrden("descripcionOrden1, descripcionOrden2 asc")

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New SucursalNoEncontradaException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerSucursalesNivel(ByVal eNivel As Nivel, ByVal eIdsSucursal As String) As String
        Dim iGeneradorSql As New GeneradorSql
        Dim iIdsSucursal As String
        Dim iDataSet As DataSet
        Dim i As Integer

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("s.id")
            iGeneradorSql.agregarTabla("sucursal s")

            If eIdsSucursal <> Nothing Then iGeneradorSql.agregarCondicionWhere("s.id not in (" & eIdsSucursal & ")")
            iGeneradorSql.agregarCondicionWhere("s.idEstado=" & Estado.ALTA)
            If Not IsNothing(eNivel) Then
                If eNivel.isSucursal Then
                    iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=" & eNivel.padre.id)
                ElseIf eNivel.isUnidadDeNegocios Then
                    iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=" & eNivel.id)
                ElseIf eNivel.isEmpresaGrupo Then
                    iGeneradorSql.agregarTabla("UnidadDeNegocios u")
                    iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=u.id")
                    iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=" & eNivel.id)
                ElseIf eNivel.isGrupoEmpresas Then
                    iGeneradorSql.agregarTabla("UnidadDeNegocios u")
                    iGeneradorSql.agregarTabla("empresaGrupo eg")
                    iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=u.id")
                    iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=eg.id")
                    iGeneradorSql.agregarCondicionWhere("eg.idGrupoEmpresas=" & eNivel.id)
                End If
            End If

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Sucursales")

            If iDataSet.Tables("Sucursales").Rows.Count > 0 Then
                For i = 0 To iDataSet.Tables("Sucursales").Rows.Count - 1
                    iIdsSucursal += iDataSet.Tables("Sucursales").Rows(i).Item("id") & ","
                Next
                If iIdsSucursal <> Nothing Then iIdsSucursal = Left(iIdsSucursal, iIdsSucursal.Length - 1)
            End If

            Return iIdsSucursal

        Catch excepcion As Exception
            Throw New SucursalNoEncontradaException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iDataSet = Nothing
        End Try

    End Function
    
#End Region

End Class