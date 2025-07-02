Imports System.Collections.Generic
Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class GrupoEmpresas : Inherits Nivel

#Region "Constantes"
    Public Const GRUPOEMPRESAS = 1
#End Region

#Region "Variables"
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
#End Region

#Region "Metodos"

    Public Overrides Function isSucursal() As Boolean
        Return False
    End Function

    Public Overrides Function isUnidadDeNegocios() As Boolean
        Return False
    End Function

    Public Overrides Function isEmpresaGrupo() As Boolean
        Return False
    End Function

    Public Overrides Function isGrupoEmpresas() As Boolean
        Return True
    End Function

    Public Overrides Function isPuntoVentaDgi() As Boolean
        Return False
    End Function

    Public Overrides Function obtenerNivel(Optional eObtenerDomicilio As Boolean = True) As Nivel
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iGrupoEmpresas As GrupoEmpresas

        Try


            If id <> Nothing Then
                iGrupoEmpresas = NivelSingleton.getInstancia(False).listaNiveles.Find(Function(p) p.id = id)
                id = iGrupoEmpresas.id
                descripcion = iGrupoEmpresas.descripcion
                iDomicilio = iGrupoEmpresas.domicilio
                padre = Nothing

                Return Me
            Else
                iConexion = obtenerConexion()

                iGeneradorSql.agregarColumna("idDomicilio")
                iGeneradorSql.agregarTabla("grupoEmpresas")
                iGeneradorSql.agregarCondicionWhere("id=" & id)

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                If iDataReader.Read Then
                    iDomicilio = New Domicilio
                    iDomicilio.id = iDataReader.Item("idDomicilio").ToString
                    iDataReader.Close()

                    MyBase.accesoDatos = iConexion
                    MyBase.obtenerNivel()

                    If eObtenerDomicilio Then
                        iDomicilio.accesoDatos = iConexion
                        iDomicilio = iDomicilio.obtenerDomicilio
                        iDomicilio.accesoDatos = Nothing
                    End If

                    padre = Nothing

                    Return Me
                Else
                    Throw New GrupoEmpresasNoEncontradoException
                End If
            End If


        Catch exception As Exception
            Throw New GrupoEmpresasNoEncontradoException(exception)
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

    Public Overrides Function obtenerNiveles(Optional ByVal ePadre As Nivel = Nothing) As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iDataSetSingleton As DataSet
        Dim iDataView As DataView
        Try


            iDataSetSingleton = NivelSingleton.getInstancia(False).dataSet

            If Not IsNothing(iDataSetSingleton) Then
                iDataView = iDataSetSingleton.Tables("Niveles").DefaultView
                iDataView.RowFilter = "idtipoNivel=" & TipoNivel.GRUPOEMPRESAS
                iDataReader = iDataView.ToTable.CreateDataReader()
            Else
                iConexion = obtenerConexion()

                iGeneradorSql.agregarColumna("g.id")
                iGeneradorSql.agregarColumna("n.descripcion")
                iGeneradorSql.agregarTabla("grupoEmpresas g")
                iGeneradorSql.agregarTabla("nivel n")
                iGeneradorSql.agregarCondicionWhere("g.id=n.id")

                iGeneradorSql.agregarOrden("n.descripcion asc")

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            End If

            Return iDataReader

        Catch excepcion As Exception
            Throw New GrupoEmpresasNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Overrides Function obtenerNivelesLista(eNivelOrigen As Nivel) As List(Of Nivel)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iGrupoEmpresas As GrupoEmpresas
        Dim iDataSet As DataSet
        Dim iNiveles As List(Of Nivel)

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("GrupoEmpresas")

            If eNivelOrigen.isGrupoEmpresas Then
                iGeneradorSql.agregarCondicionWhere("id=" & eNivelOrigen.id)
            ElseIf eNivelOrigen.isEmpresaGrupo Then
                iGeneradorSql.agregarCondicionWhere("id=" & eNivelOrigen.padre.id)
            ElseIf eNivelOrigen.isUnidadDeNegocios Then
                iGeneradorSql.agregarCondicionWhere("id=" & eNivelOrigen.padre.padre.id)
            ElseIf eNivelOrigen.isSucursal Then
                iGeneradorSql.agregarCondicionWhere("id=" & eNivelOrigen.padre.padre.padre.id)
            End If

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Niveles")

            For Each iDataRow As DataRow In iDataSet.Tables("Niveles").Rows
                If IsNothing(iNiveles) Then iNiveles = New List(Of Nivel)
                iGrupoEmpresas = New GrupoEmpresas
                iGrupoEmpresas.id = iDataRow.Item("id")
                iGrupoEmpresas.accesoDatos = iConexion
                iGrupoEmpresas = iGrupoEmpresas.obtenerNivel(False)
                iGrupoEmpresas.accesoDatos = Nothing
                iNiveles.Add(iGrupoEmpresas)
                iGrupoEmpresas = Nothing
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
            iGrupoEmpresas = Nothing
            iDataSet = Nothing
            iNiveles = Nothing
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
            iGeneradorSql.agregarTabla("empresaGrupo eg")
            iGeneradorSql.agregarCondicionWhere("eg.id=un.idEmpresaGrupo")
            iGeneradorSql.agregarCondicionWhere("eg.idgrupoEmpresas=" & id)

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
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Function
    Public Overrides Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

            MyBase.dispose()

        Catch exception As exception
            Throw New RootException(exception)
        End Try

    End Sub
#End Region

End Class