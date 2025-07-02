Imports System.Collections.Generic
Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class EmpresaGrupo : Inherits Nivel

#Region "Variables"
    Private iNumeroCuit1 As Integer
    Private iNumeroCuit As Long
    Private iNumeroCuit2 As Integer
    Private iNumeroIB As String
    Private iDomicilio As domicilio
    Private iConexion As accesoDatos
#End Region

#Region "Atributos"
    Public Property domicilio() As domicilio
        Get
            Return iDomicilio
        End Get
        Set(ByVal Value As domicilio)
            iDomicilio = Value
        End Set
    End Property

    Public Property numeroCuit() As Integer
        Get
            Return iNumeroCuit
        End Get
        Set(ByVal Value As Integer)
            iNumeroCuit = Value
        End Set
    End Property

    Public Property numeroCuit1() As Integer
        Get
            Return iNumeroCuit1
        End Get
        Set(ByVal Value As Integer)
            iNumeroCuit1 = Value
        End Set
    End Property

    Public Property numeroCuit2() As Integer
        Get
            Return iNumeroCuit2
        End Get
        Set(ByVal Value As Integer)
            iNumeroCuit2 = Value
        End Set
    End Property

    Public Property numeroIB() As String
        Get
            Return iNumeroIB
        End Get
        Set(ByVal Value As String)
            iNumeroIB = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Overrides Function isSucursal() As Boolean
        Return False
    End Function

    Public Overrides Function isUnidadDeNegocios() As Boolean
        Return False
    End Function

    Public Overrides Function isEmpresaGrupo() As Boolean
        Return True
    End Function

    Public Overrides Function isGrupoEmpresas() As Boolean
        Return False
    End Function

    Public Overrides Function isPuntoVentaDgi() As Boolean
        Return False
    End Function

    Public Overrides Function obtenerNivel(Optional eObtenerDomicilio As Boolean = True) As Nivel
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()
        Dim iEmpresaGrupo As EmpresaGrupo

        Try



            If id <> Nothing Then
                iEmpresaGrupo = NivelSingleton.getInstancia(False).listaNiveles.Find(Function(p) p.id = id)

                id = iEmpresaGrupo.id
                descripcion = iEmpresaGrupo.descripcion
                iNumeroCuit1 = iEmpresaGrupo.numeroCuit1
                iNumeroCuit = iEmpresaGrupo.numeroCuit
                iNumeroCuit2 = iEmpresaGrupo.numeroCuit2
                iNumeroIB = iEmpresaGrupo.numeroIB
                iDomicilio = iEmpresaGrupo.domicilio
                padre = iEmpresaGrupo.padre

                padre.accesoDatos = iConexion
                padre.obtenerNivel(eObtenerDomicilio)
                padre.accesoDatos = Nothing

                Return Me
            Else
                iConexion = obtenerConexion()

                iGeneradorSql.agregarColumna("eg.id")
                iGeneradorSql.agregarColumna("eg.numeroCuit1")
                iGeneradorSql.agregarColumna("eg.numeroCuit")
                iGeneradorSql.agregarColumna("eg.numeroCuit2")
                iGeneradorSql.agregarColumna("eg.idDomicilio")
                iGeneradorSql.agregarColumna("eg.idGrupoEmpresas")
                iGeneradorSql.agregarColumna("eg.numeroIB")

                iGeneradorSql.agregarTabla("EmpresaGrupo eg")

                If descripcion <> Nothing Then
                    iGeneradorSql.agregarTabla("nivel n")
                    iGeneradorSql.agregarCondicionWhere("n.id=eg.id")
                    iGeneradorSql.agregarCondicionWhere("n.descripcion=" & FuncionComun.nuloSiEsNothing(descripcion))
                End If

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                If iDataReader.Read Then
                    id = iDataReader.Item("id").ToString
                    iNumeroCuit1 = FuncionComun.ceroSiEsVacio(iDataReader.Item("numeroCuit1").ToString())
                    iNumeroCuit = FuncionComun.ceroSiEsVacio(iDataReader.Item("numeroCuit").ToString())
                    iNumeroCuit2 = FuncionComun.ceroSiEsVacio(iDataReader.Item("numeroCuit2").ToString())
                    iNumeroIB = iDataReader.Item("numeroIB").ToString()
                    iDomicilio = New Domicilio
                    iDomicilio.id = iDataReader.Item("idDomicilio").ToString()
                    padre = New GrupoEmpresas()
                    padre.id = iDataReader.Item("idGrupoEmpresas").ToString()

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
                    Throw New EmpresaGrupoNoEncontradaException()
                End If
            End If



        Catch excepcion As Exception
            Throw New EmpresaGrupoNoEncontradaException(excepcion)
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
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader
        Dim iDataSetSingleton As DataSet
        Dim iDataView As DataView
        Try
            iDataSetSingleton = NivelSingleton.getInstancia(False).dataSet

            If Not IsNothing(iDataSetSingleton) Then
                iDataView = iDataSetSingleton.Tables("Niveles").DefaultView
                If Not IsNothing(eNivel) Then
                    If eNivel.isEmpresaGrupo Then
                        iDataView.RowFilter = "idtipoNivel=" & TipoNivel.EMPRESAGRUPO & " and idGrupoEmpresas=" & eNivel.padre.id
                    ElseIf eNivel.isGrupoEmpresas Then
                        iDataView.RowFilter = "idtipoNivel=" & TipoNivel.EMPRESAGRUPO & " and idGrupoEmpresas=" & eNivel.id
                    Else
                        Throw New EmpresaGrupoNoEncontradaException("El usuario no pertenece a un nivel de grupo empresa")
                    End If
                Else
                    iDataView.RowFilter = "idtipoNivel=" & TipoNivel.EMPRESAGRUPO
                End If
                iDataReader = iDataView.ToTable.CreateDataReader()
            Else

                iConexion = obtenerConexion()

                iGeneradorSql.agregarColumna("e.id")
                iGeneradorSql.agregarColumna("n.descripcion")
                iGeneradorSql.agregarTabla("empresaGrupo e")
                iGeneradorSql.agregarTabla("nivel n")
                iGeneradorSql.agregarCondicionWhere("e.id=n.id")
                If Not IsNothing(eNivel) Then
                    If eNivel.isEmpresaGrupo Then
                        iGeneradorSql.agregarCondicionWhere("e.idGrupoEmpresas=" & eNivel.padre.id)
                    ElseIf eNivel.isGrupoEmpresas Then
                        iGeneradorSql.agregarCondicionWhere("e.idGrupoEmpresas=" & eNivel.id)
                    Else
                        Throw New EmpresaGrupoNoEncontradaException("El usuario no pertenece a un nivel de grupo empresa")
                    End If
                End If
                iGeneradorSql.agregarOrden("n.descripcion asc")

                IDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            End If

            Return IDataReader
        Catch excepcion As Exception
            Throw New EmpresaGrupoNoEncontradaException(excepcion)
        Finally
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Overrides Function obtenerNivelesLista(eNivelOrigen As Nivel) As List(Of Nivel)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iEmpresaGrupo As EmpresaGrupo
        Dim iDataSet As DataSet
        Dim iNiveles As List(Of Nivel)

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("empresaGrupo")

            If eNivelOrigen.isGrupoEmpresas Then
                iGeneradorSql.agregarCondicionWhere("idGrupoEmpresas=" & eNivelOrigen.id)
            ElseIf eNivelOrigen.isEmpresaGrupo Then
                iGeneradorSql.agregarCondicionWhere("id=" & eNivelOrigen.id)
            ElseIf eNivelOrigen.isUnidadDeNegocios Then
                iGeneradorSql.agregarCondicionWhere("id=" & eNivelOrigen.padre.id)
            ElseIf eNivelOrigen.isSucursal Then
                iGeneradorSql.agregarCondicionWhere("id=" & eNivelOrigen.padre.padre.id)
            End If

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Niveles")

            For Each iDataRow As DataRow In iDataSet.Tables("Niveles").Rows
                If IsNothing(iNiveles) Then iNiveles = New List(Of Nivel)

                iEmpresaGrupo = New EmpresaGrupo
                iEmpresaGrupo.id = iDataRow.Item("id")
                iEmpresaGrupo.accesoDatos = iConexion
                iEmpresaGrupo = iEmpresaGrupo.obtenerNivel(False)
                iNiveles.Add(iEmpresaGrupo)
                iEmpresaGrupo.accesoDatos = Nothing
            Next

            Return iNiveles

        Catch excepcion As Exception
            Throw New EmpresaGrupoNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iEmpresaGrupo = Nothing
            iDataSet = Nothing
            iNiveles = Nothing
        End Try
    End Function

    Public Function obtenerEmpresasGrupoGrilla(ByVal eNivel As Nivel) As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataSet As DataSet
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("n.id")
            iGeneradorSql.agregarColumna("n.descripcion")
            iGeneradorSql.agregarColumna("e.numeroIB")
            iGeneradorSql.agregarColumna(FuncionComun.sqlConcatenar("e.numeroCuit1,'-',e.numeroCuit,'-',e.numeroCuit2") & " as numerocuit")
            iGeneradorSql.agregarTabla("nivel n")
            iGeneradorSql.agregarTabla("empresaGrupo e")
            iGeneradorSql.agregarCondicionWhere("n.id=e.id")
            If descripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("n.descripcion like'" & descripcion & "%'")
            iGeneradorSql.agregarCondicionWhere("e.idGrupoEmpresas=" & eNivel.id)

            iGeneradorSql.agregarOrden("n.descripcion ASC")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "EmpresasGrupoGrilla")

            Return iDataSet

        Catch exception As exception
            Throw New EmpresaGrupoNoEncontradaException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Overrides Function obtenerSucursalNivel() As String
        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iStringId As String
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("su.id")
            iGeneradorSql.agregarTabla("sucursal su")
            iGeneradorSql.agregarTabla("UnidadDeNegocios un")
            iGeneradorSql.agregarCondicionWhere("un.id=su.idUnidadDenegocios")
            iGeneradorSql.agregarCondicionWhere("un.idEmpresaGrupo=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            While iDataReader.Read
                iStringId = iStringId & iDataReader.Item("id").ToString & ","
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
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Overrides Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            MyBase.accesoDatos = iConexion
            MyBase.modificar()

            iDomicilio.accesoDatos = iConexion
            iDomicilio.modificar()
            iDomicilio.accesoDatos = Nothing

            iGeneradorSql.agregarTabla("EmpresaGrupo")
            iGeneradorSql.agregarSet("numeroIb=" & FuncionComun.nuloSiEsNothing(iNumeroIb))
            iGeneradorSql.agregarSet("numeroCuit1=" & FuncionComun.nuloSiEsNothing(iNumeroCuit1))
            iGeneradorSql.agregarSet("numeroCuit=" & FuncionComun.nuloSiEsNothing(iNumeroCuit))
            iGeneradorSql.agregarSet("numeroCuit2=" & FuncionComun.nuloSiEsNothing(iNumeroCuit2))
            iGeneradorSql.agregarCondicionWhere("id = " & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New EmpresaFalsaNoModificadaException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

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
            iGeneradorSql.agregarColumna("numeroIb")
            iGeneradorSql.agregarColumna("numeroCuit1")
            iGeneradorSql.agregarColumna("numeroCuit")
            iGeneradorSql.agregarColumna("numeroCuit2")
            iGeneradorSql.agregarColumna("idDomicilio")
            iGeneradorSql.agregarColumna("idGrupoEmpresas")

            iGeneradorSql.agregarValue(id)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(numeroIB))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(numeroCuit1))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(numeroCuit))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(numeroCuit2))
            iGeneradorSql.agregarValue(domicilio.id)
            iGeneradorSql.agregarValue(padre.id)

            iGeneradorSql.agregarTabla("empresaGrupo")

            iConexion.ejecutar(iGeneradorSql.GenerarInsert, iGeneradorSql.parametrosSQL)

        Catch exception As exception
            Throw New EmpresaGrupoNoCreadaException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing

        End Try
    End Sub

    Private Sub validarCrear()
        Try

            If IsNothing(domicilio) Then Throw New EmpresaGrupoNoCreadaException("El domicilio no puede ser nulo")

            If IsNothing(padre) Then Throw New EmpresaGrupoNoCreadaException("El padre no puede ser nulo")

        Catch excepcion As Exception
            Throw New EmpresaGrupoNoCreadaException(excepcion)
        Finally
            If IsNothing(MyBase.accesoDatos) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
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
            iGeneradorSql.agregarTabla("empresaGrupo")
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iDomicilio.accesoDatos = iConexion
            iDomicilio.eliminar()
            iDomicilio.accesoDatos = Nothing


        Catch exception As Exception
            Throw New EmpresaGrupoNoEliminadaException(exception)
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
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idEmpresaGrupo=" & id)
            iGeneradorSql.agregarTabla("unidadDeNegocios")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New EmpresaGrupoNoEliminadaException("La empresa grupo posee unidades de negocios")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idNivel=" & id)
            iGeneradorSql.agregarTabla("usuario")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New EmpresaGrupoNoEliminadaException("La empresa grupo tiene usuarios")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New EmpresaGrupoNoEliminadaException(excepcion)
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
#End Region

End Class