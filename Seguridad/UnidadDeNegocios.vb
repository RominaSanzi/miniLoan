Imports System.Collections.Generic
Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class UnidadDeNegocios : Inherits Nivel

#Region "Constantes"
    Public Const UNIDADCENTRAL As Integer = 3
#End Region

#Region "Variables"
    Private iDomicilio As domicilio
    Private iNumeroCuit As String
    Private iNumeroIb As String
    Private iConexion As accesoDatos
#End Region

#Region "Atributos"
    Public Property domicilio() As Domicilio
        Get
            Return iDomicilio
        End Get
        Set(ByVal Value As Domicilio)
            iDomicilio = Value
        End Set
    End Property
    Public Property numeroCuit() As String
        Get
            Return iNumeroCuit
        End Get
        Set(ByVal Value As String)
            iNumeroCuit = Value
        End Set
    End Property
    Public Property numeroIb() As String
        Get
            Return iNumeroIb
        End Get
        Set(ByVal Value As String)
            iNumeroIb = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Overrides Function isSucursal() As Boolean
        Return False
    End Function

    Public Overrides Function isUnidadDeNegocios() As Boolean
        Return True
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
        Dim iUnidadDeNegocios As UnidadDeNegocios
        Try

            If id <> Nothing Then
                iUnidadDeNegocios = NivelSingleton.getInstancia(False).listaNiveles.Find(Function(p) p.id = id)

                id = iUnidadDeNegocios.id
                descripcion = iUnidadDeNegocios.descripcion
                padre = iUnidadDeNegocios.padre
                iDomicilio = iUnidadDeNegocios.domicilio
                iNumeroIb = iUnidadDeNegocios.numeroIb
                iNumeroCuit = iUnidadDeNegocios.numeroCuit

                padre.accesoDatos = iConexion
                padre.obtenerNivel(eObtenerDomicilio)
                padre.accesoDatos = Nothing

                Return Me
            Else
                iConexion = obtenerConexion()

                iGeneradorSql.agregarColumna("idDomicilio")
                iGeneradorSql.agregarColumna("idEmpresaGrupo")
                iGeneradorSql.agregarColumna("idTipoIva")
                iGeneradorSql.agregarColumna("numeroCuit")
                iGeneradorSql.agregarColumna("numeroIB")
                iGeneradorSql.agregarTabla("UnidadDeNegocios")
                iGeneradorSql.agregarCondicionWhere("id=" & id)

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                If iDataReader.Read Then
                    padre = New EmpresaGrupo()
                    padre.id = iDataReader.Item("idEmpresaGrupo").ToString
                    iDomicilio = New Domicilio()
                    iDomicilio.id = iDataReader.Item("idDomicilio").ToString
                    iNumeroIb = iDataReader.Item("numeroIB").ToString
                    iNumeroCuit = FuncionComun.vacioSiEsNulo(iDataReader.Item("numeroCuit"))

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
                    Throw New UnidadDeNegociosNoEncontradaException()
                End If
            End If





        Catch excepcion As Exception
            Throw New UnidadDeNegociosNoEncontradaException(excepcion)
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
        Dim iFiltro As String
        Try
            iDataSetSingleton = NivelSingleton.getInstancia(False).dataSet

            If Not IsNothing(iDataSetSingleton) Then
                iDataView = iDataSetSingleton.Tables("Niveles").DefaultView
                iFiltro = "idtipoNivel=" & TipoNivel.UNIDADDENEGOCIOS
                If Not IsNothing(eNivel) Then
                    If eNivel.isUnidadDeNegocios Then
                        iFiltro &= " and idEmpresaGrupo=" & eNivel.padre.id
                    ElseIf eNivel.isEmpresaGrupo Then
                        iFiltro &= " and idEmpresaGrupo=" & eNivel.id
                    Else
                        Throw New UnidadDeNegociosNoEncontradaException("El usuario no pertenece a un nivel de empresa grupo")
                    End If
                End If
                If descripcion <> Nothing Then iFiltro &= " and descripcion like '" & descripcion & "%'"

                iDataView.RowFilter = iFiltro
                iDataReader = iDataView.ToTable.CreateDataReader()

            Else
                iConexion = obtenerConexion()

                iGeneradorSql.agregarColumna("u.id")
                iGeneradorSql.agregarColumna("n.descripcion")
                iGeneradorSql.agregarTabla("unidadDeNegocios u")
                iGeneradorSql.agregarTabla("nivel n")
                iGeneradorSql.agregarCondicionWhere("u.id=n.id")
                If Not IsNothing(eNivel) Then
                    If eNivel.isUnidadDeNegocios Then
                        iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=" & eNivel.padre.id)
                    ElseIf eNivel.isEmpresaGrupo Then
                        iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=" & eNivel.id)
                    Else
                        Throw New UnidadDeNegociosNoEncontradaException("El usuario no pertenece a un nivel de empresa grupo")
                    End If
                End If
                If descripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("n.descripcion like '" & descripcion & "%'")

                iGeneradorSql.agregarOrden("n.descripcion asc")

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            End If

            Return iDataReader

        Catch excepcion As Exception
            Throw New UnidadDeNegociosNoEncontradaException(excepcion)
        Finally
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Overrides Function obtenerNivelesLista(eNivelOrigen As Nivel) As List(Of Nivel)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iUnidadDeNegocios As UnidadDeNegocios
        Dim iDataSet As DataSet
        Dim iNiveles As List(Of Nivel)

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("u.id")
            iGeneradorSql.agregarTabla("UnidadDeNegocios u")
            iGeneradorSql.agregarTabla("EmpresaGrupo eg")
            iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=eg.id")

            If eNivelOrigen.isGrupoEmpresas Then
                iGeneradorSql.agregarCondicionWhere("eg.idGrupoEmpresas=" & eNivelOrigen.id)
            ElseIf eNivelOrigen.isEmpresaGrupo Then
                iGeneradorSql.agregarCondicionWhere("eg.id=" & eNivelOrigen.id)
            ElseIf eNivelOrigen.isUnidadDeNegocios Then
                iGeneradorSql.agregarCondicionWhere("u.id=" & eNivelOrigen.id)
            ElseIf eNivelOrigen.isSucursal Then
                iGeneradorSql.agregarCondicionWhere("u.id=" & eNivelOrigen.padre.id)
            End If

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Niveles")

            For Each iDataRow As DataRow In iDataSet.Tables("Niveles").Rows
                If IsNothing(iNiveles) Then iNiveles = New List(Of Nivel)

                iUnidadDeNegocios = New UnidadDeNegocios
                iUnidadDeNegocios.id = iDataRow.Item("id")
                iUnidadDeNegocios.accesoDatos = iConexion
                iUnidadDeNegocios = iUnidadDeNegocios.obtenerNivel(False)
                iUnidadDeNegocios.accesoDatos = Nothing
                iNiveles.Add(iUnidadDeNegocios)
                iUnidadDeNegocios = Nothing
            Next

            Return iNiveles

        Catch excepcion As Exception
            Throw New UnidadDeNegociosNoModificadaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iUnidadDeNegocios = Nothing
            iDataSet = Nothing
            iNiveles = Nothing
        End Try
    End Function

    Public Overrides Function obtenerSucursalNivel() As String
        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql()
        Dim iStringId As String
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("sucursal")
            iGeneradorSql.agregarCondicionWhere("idunidaddenegocios=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            While iDataReader.Read
                iStringId = iStringId & iDataReader.Item("id").ToString & ","
            End While

            Return iStringId
        Catch excepcion As Exception
            Throw New UnidadDeNegociosNoModificadaException(excepcion)
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

    Public Overrides Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            MyBase.accesoDatos = iConexion
            MyBase.modificar()

            iDomicilio.accesoDatos = iConexion
            iDomicilio.modificar()
            iDomicilio.accesoDatos = Nothing

            iGeneradorSql.agregarTabla("UnidadDeNegocios")
            iGeneradorSql.agregarSet("numeroIb=" & FuncionComun.nuloSiEsNothing(iNumeroIb))
            iGeneradorSql.agregarSet("numeroCuit=" & FuncionComun.nuloSiEsNothing(iNumeroCuit))
            iGeneradorSql.agregarSet("idEmpresaGrupo=" & padre.id)

            iGeneradorSql.agregarCondicionWhere("id = " & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New UnidadDeNegociosNoModificadaException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerUnidadesDeNegociosGrilla(ByVal eNivel As Nivel) As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataSet As DataSet
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("n.id")
            iGeneradorSql.agregarColumna("n.descripcion")
            iGeneradorSql.agregarColumna("u.numeroIB")
            iGeneradorSql.agregarColumna("u.numeroCuit")
            iGeneradorSql.agregarTabla("nivel n")
            iGeneradorSql.agregarTabla("unidadDeNegocios u")
            iGeneradorSql.agregarCondicionWhere("n.id=u.id")
            If descripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("n.descripcion like'" & descripcion & "%'")

            If eNivel.isEmpresaGrupo Then
                iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=" & eNivel.id)
            ElseIf eNivel.isGrupoEmpresas Then
                iGeneradorSql.agregarTabla("EmpresaGrupo eg")
                iGeneradorSql.agregarCondicionWhere("eg.id=u.idEmpresaGrupo")
                iGeneradorSql.agregarCondicionWhere("eg.idGrupoEmpresas=" & eNivel.id)
            End If
            iGeneradorSql.agregarOrden("n.descripcion ASC")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "UnidadesDeNegociosGrilla")

            Return iDataSet

        Catch exception As exception
            Throw New UnidadDeNegociosNoEncontradaException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Overrides Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            validarEliminar()

            MyBase.accesoDatos = iConexion
            MyBase.eliminar()

            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iGeneradorSql.agregarTabla("unidadDeNegocios")
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iDomicilio.accesoDatos = iConexion
            iDomicilio.eliminar()
            iDomicilio.accesoDatos = Nothing

        Catch exception As exception
            Throw New SucursalNoEliminadaException(exception)
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
        Dim iDataReader As iDataReader
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idUnidadDeNegocios=" & id)
            iGeneradorSql.agregarTabla("sucursal")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UnidadDeNegociosNoEliminadaException("La unidad de negocios posee sucursales")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idNivel=" & id)
            iGeneradorSql.agregarTabla("usuario")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New UnidadDeNegociosNoEliminadaException("La unidad de negocios tiene usuarios")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New UnidadDeNegociosNoEliminadaException(excepcion)
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
            iGeneradorSql.agregarColumna("numeroCuit")
            iGeneradorSql.agregarColumna("idDomicilio")
            iGeneradorSql.agregarColumna("idEmpresaGrupo")
            iGeneradorSql.agregarColumna("idTipoIva")

            iGeneradorSql.agregarValue(id)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(numeroIb))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(numeroCuit))
            iGeneradorSql.agregarValue(domicilio.id)
            iGeneradorSql.agregarValue(padre.id)

            iGeneradorSql.agregarTabla("unidadDeNegocios")
            iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New UnidadDeNegociosNoCreadaException(exception)
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

            If IsNothing(domicilio) Then Throw New UnidadDeNegociosNoCreadaException("El domicilio no puede ser nulo")

            If IsNothing(padre) Then Throw New UnidadDeNegociosNoCreadaException("El padre no puede ser nulo")

        Catch excepcion As Exception
            Throw New UnidadDeNegociosNoCreadaException(excepcion)
        Finally
            If IsNothing(MyBase.accesoDatos) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
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