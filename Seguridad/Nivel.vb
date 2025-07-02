Imports System.Collections.Generic
Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades

Public MustInherit Class Nivel

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iDescripcion As String
    Private iPadre As Object

    Private iConexion As accesoDatos
#End Region

#Region "Atributos"
    Public Property descripcion() As String
        Get
            Return iDescripcion
        End Get
        Set(ByVal Value As String)
            iDescripcion = Value
        End Set
    End Property

    Public Property id() As Long
        Get
            Return iId
        End Get
        Set(ByVal Value As Long)
            iId = Value
        End Set
    End Property

    Public Property padre() As Object
        Get
            Return iPadre
        End Get
        Set(ByVal Value As Object)
            iPadre = Value
        End Set
    End Property

#End Region

#Region "Metodos"

    MustOverride Function isSucursal() As Boolean
    MustOverride Function isUnidadDeNegocios() As Boolean
    MustOverride Function isEmpresaGrupo() As Boolean
    MustOverride Function isGrupoEmpresas() As Boolean
    MustOverride Function isPuntoVentaDgi() As Boolean

    'Public Overridable Function obtenerNiveles() As IDataReader
    '    'Se sobreescribe en Sucursal,UnidadDeNegocios,EmpresaGrupo,GrupoEmpresas
    'End Function

    MustOverride Function obtenerNiveles(Optional ByVal ePadre As Nivel = Nothing) As IDataReader
    'Se sobreescribe en Sucursal,UnidadDeNegocios,EmpresaGrupo,GrupoEmpresas

    MustOverride Function obtenerNivelesLista(eNivelOrigen As Nivel) As List(Of Nivel)
    'Se sobreescribe en Sucursal,UnidadDeNegocios,EmpresaGrupo,GrupoEmpresas

    Public Overridable Function obtenerNivel(Optional eObtenerDomicilio As Boolean = True) As Nivel
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataSetSingleton As DataSet
        Dim iDataRow As DataRow
        Try
            'If iId <> Nothing Then
            '    iDataSetSingleton = NivelSingleton.getInstancia(False).dataSet
            '    If iId <> Nothing Then
            '        Dim iBusqueda(0) As Object
            '        iBusqueda(0) = iId
            '        iDataRow = iDataSetSingleton.Tables("Nivel").Rows.Find(iBusqueda)
            '    End If
            '    If Not IsNothing(iDataRow) Then
            '        iId = iDataRow.Item("id").ToString
            '        iDescripcion = FuncionComun.vacioSiEsNulo(iDataRow.Item("descripcion").ToString)

            '        Return Me
            '    End If
            'Else
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarTabla("Nivel")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If descripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion like '" & descripcion & "%'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iDescripcion = FuncionComun.vacioSiEsNulo(iDataReader.Item("descripcion").ToString)

                Return Me
            Else
                Throw New NivelNoEncontradoException()
            End If
            'End If
        Catch Exception As Exception
            Throw New NivelNoEncontradoException(Exception)
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

    Public Shared Function obtenerNivelShared(ByVal eId As Long, ByVal eConexion As accesoDatos, Optional eObtenerDomicilio As Boolean = True) As Object
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try

            iGeneradorSql.agregarTabla("Nivel")

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("idTipoNivel")

            iGeneradorSql.agregarCondicionWhere("id=" & eId)

            iDataReader = eConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Select Case iDataReader.Item("idTipoNivel").ToString
                    Case di.financiera.seguridad.TipoNivel.SUCURSAL
                        Dim iSucursal As New Sucursal()
                        iSucursal.id = iDataReader.Item("id").ToString
                        iDataReader.Close()
                        iSucursal.accesoDatos = eConexion
                        iSucursal = iSucursal.obtenerNivel(eObtenerDomicilio)
                        iSucursal.accesoDatos = Nothing
                        Return iSucursal
                    Case di.financiera.seguridad.TipoNivel.UNIDADDENEGOCIOS
                        Dim iUnidadDeNegocios As New UnidadDeNegocios()
                        iUnidadDeNegocios.id = iDataReader.Item("id").ToString
                        iDataReader.Close()
                        iUnidadDeNegocios.accesoDatos = eConexion
                        iUnidadDeNegocios = iUnidadDeNegocios.obtenerNivel(eObtenerDomicilio)
                        iUnidadDeNegocios.accesoDatos = Nothing
                        Return iUnidadDeNegocios
                    Case di.financiera.seguridad.TipoNivel.EMPRESAGRUPO
                        Dim iEmpresaGrupo As New EmpresaGrupo()
                        iEmpresaGrupo.id = iDataReader.Item("id").ToString
                        iDataReader.Close()
                        iEmpresaGrupo.accesoDatos = eConexion
                        iEmpresaGrupo = iEmpresaGrupo.obtenerNivel(eObtenerDomicilio)
                        iEmpresaGrupo.accesoDatos = Nothing
                        Return iEmpresaGrupo
                    Case di.financiera.seguridad.TipoNivel.GRUPOEMPRESAS
                        Dim iGrupoEmpresas As New GrupoEmpresas()
                        iGrupoEmpresas.id = iDataReader.Item("id").ToString
                        iDataReader.Close()
                        iGrupoEmpresas.accesoDatos = eConexion
                        iGrupoEmpresas = iGrupoEmpresas.obtenerNivel(eObtenerDomicilio)
                        iGrupoEmpresas.accesoDatos = Nothing
                        Return iGrupoEmpresas
                    Case di.financiera.seguridad.TipoNivel.PUNTOVENTADGI
                        Dim iPuntoVentaDgi As New PuntoVentaDgi
                        iPuntoVentaDgi.id = iDataReader.Item("id").ToString
                        iDataReader.Close()
                        iPuntoVentaDgi.accesoDatos = eConexion
                        iPuntoVentaDgi = iPuntoVentaDgi.obtenerNivel(eObtenerDomicilio)
                        iPuntoVentaDgi.accesoDatos = Nothing
                        Return iPuntoVentaDgi
                End Select
            Else
                Throw New NivelNoEncontradoException()
            End If

        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Shared Function obtenerIdsNivelShared(ByVal eId As Long, ByVal eConexion As accesoDatos) As Object
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader
        Try
            iGeneradorSql.agregarTabla("Nivel")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("idTipoNivel")
            iGeneradorSql.agregarColumna("id")

            iGeneradorSql.agregarCondicionWhere("id=" & eId)

            iDataReader = eConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Select Case iDataReader.Item("idTipoNivel").ToString
                    Case di.financiera.seguridad.TipoNivel.SUCURSAL
                        Dim iSucursal As New Sucursal()
                        iSucursal.id = iDataReader.Item("id").ToString
                        iSucursal.descripcion = iDataReader.Item("descripcion").ToString
                        iDataReader.Close()
                        Return iSucursal
                    Case di.financiera.seguridad.TipoNivel.UNIDADDENEGOCIOS
                        Dim iUnidadDeNegocios As New UnidadDeNegocios()
                        iUnidadDeNegocios.id = iDataReader.Item("id").ToString
                        iUnidadDeNegocios.descripcion = iDataReader.Item("descripcion").ToString
                        iDataReader.Close()
                        Return iUnidadDeNegocios
                    Case di.financiera.seguridad.TipoNivel.EMPRESAGRUPO
                        Dim iEmpresaGrupo As New EmpresaGrupo()
                        iEmpresaGrupo.id = iDataReader.Item("id").ToString
                        iEmpresaGrupo.descripcion = iDataReader.Item("descripcion").ToString
                        iDataReader.Close()
                        Return iEmpresaGrupo
                    Case di.financiera.seguridad.TipoNivel.GRUPOEMPRESAS
                        Dim iGrupoEmpresas As New GrupoEmpresas()
                        iGrupoEmpresas.id = iDataReader.Item("id").ToString
                        iGrupoEmpresas.descripcion = iDataReader.Item("descripcion").ToString
                        iDataReader.Close()
                        Return iGrupoEmpresas
                End Select
            Else
                Throw New NivelNoEncontradoException()
            End If

        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
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

            If Not IsNothing(iPadre) Then iPadre.dispose()

        Catch exception As Exception
            Throw New RootException(exception)
        End Try

    End Sub

    Public Overridable Function obtenerSucursalNivel() As String

    End Function

    Public Overridable Sub modificar()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarSet("descripcion='" & descripcion & "'")
            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iGeneradorSql.agregarTabla("nivel")
            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

            NivelSingleton.forzarActualizacion = True

        Catch excepcion As Exception
            Throw New NivelNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Overridable Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("idTipoNivel")
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(descripcion))
            If isEmpresaGrupo() Then
                iGeneradorSql.agregarValue(TipoNivel.EMPRESAGRUPO)
            ElseIf isUnidadDeNegocios() Then
                iGeneradorSql.agregarValue(TipoNivel.UNIDADDENEGOCIOS)
            ElseIf isSucursal() Then
                iGeneradorSql.agregarValue(TipoNivel.SUCURSAL)
            ElseIf isPuntoVentaDgi() Then
                iGeneradorSql.agregarValue(TipoNivel.PUNTOVENTADGI)
            End If
            iGeneradorSql.agregarTabla("nivel")

            If id <> Nothing Then
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarValue(id)
            End If

            iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

            If id = Nothing Then
                obtenerUltimoId()
            End If

            NivelSingleton.forzarActualizacion = True

        Catch excepcion As Exception
            Throw New NivelNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Overridable Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iGeneradorSql.agregarTabla("nivel")
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            NivelSingleton.forzarActualizacion = True

        Catch excepcion As Exception
            Throw New NivelNoEliminadoException(excepcion)
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

            If descripcion = Nothing Then Throw New NivelNoCreadoException("La descripcion no puede ser nula")

            Dim iGeneradorSql As New GeneradorSql
            Dim iDataReader As IDataReader
            Try
                iConexion = obtenerConexion()

                'iGeneradorSql.agregarColumna("id")
                'iGeneradorSql.agregarCondicionWhere("descripcion='" & descripcion & "'")

                'If isEmpresaGrupo() Then
                '    iGeneradorSql.agregarCondicionWhere("idTipoNivel=" & TipoNivel.EMPRESAGRUPO)
                'ElseIf isUnidadDeNegocios() Then
                '    iGeneradorSql.agregarCondicionWhere("idTipoNivel=" & TipoNivel.UNIDADDENEGOCIOS)
                'ElseIf isSucursal() Then
                '    iGeneradorSql.agregarCondicionWhere("idTipoNivel=" & TipoNivel.SUCURSAL)
                'ElseIf isPuntoVentaDgi() Then
                '    iGeneradorSql.agregarCondicionWhere("idTipoNivel=" & TipoNivel.PUNTOVENTADGI)
                'End If

                'iGeneradorSql.agregarTabla("nivel")

                'iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                'If iDataReader.Read Then
                '    If isEmpresaGrupo() Then
                '        Throw New NivelNoCreadoException("La empresa ya existe")
                '    End If
                '    If isUnidadDeNegocios() Then
                '        Throw New NivelNoCreadoException("La unidad de negocios ya existe")
                '    End If
                '    If isSucursal() Then
                '        Throw New NivelNoCreadoException("La sucursal ya existe")
                '    End If
                '    If isPuntoVentaDgi() Then
                '        Throw New NivelNoCreadoException("El punto de venta ya existe")
                '    End If
                'End If
                'iDataReader.Close()

            Catch excepcion As Exception
                Throw New NivelNoEliminadoException(excepcion)
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

        Catch excepcion As ErrorConexionException
            Throw New NivelNoCreadoException(excepcion)
        Finally
            If IsNothing(MyBase.accesoDatos) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub

    Private Sub validarModificar()
        Try

            If descripcion = Nothing Then Throw New NivelNoModificadoException("La descripcion no puede ser nula")

            Dim iGeneradorSql As New GeneradorSql
            Dim iDataReader As IDataReader

            Try
                iConexion = obtenerConexion()

                'iGeneradorSql.agregarColumna("id")
                'iGeneradorSql.agregarCondicionWhere("descripcion ='" & descripcion & "'")
                'iGeneradorSql.agregarCondicionWhere("id <>" & id)
                'iGeneradorSql.agregarTabla("nivel")

                'iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                'If iDataReader.Read Then
                '    If isEmpresaGrupo() Then
                '        Throw New NivelNoCreadoException("La empresa ya existe")
                '    End If
                '    If isUnidadDeNegocios() Then
                '        Throw New NivelNoCreadoException("La unidad de negocios ya existe")
                '    End If
                'End If
                'iDataReader.Close()

            Catch excepcion As Exception
                Throw New NivelNoEliminadoException(excepcion)
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

        Catch excepcion As ErrorConexionException
            Throw New NivelNoModificadoException(excepcion)
        Finally
            If IsNothing(MyBase.accesoDatos) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub

    Private Sub obtenerUltimoId()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("nivel")
            iGeneradorSql.agregarColumna("max(id) as id")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                iId = CLng(iDataReader.Item("id").ToString)
            Else
                Throw New PersonaNoCreadaException
            End If

        Catch excepcion As Exception
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
    End Sub

    Public Function obtenerIdNivelPorDescripcion() As Long
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("descripcion='" & descripcion & "'")

            If isEmpresaGrupo() Then
                iGeneradorSql.agregarCondicionWhere("idTipoNivel=" & TipoNivel.EMPRESAGRUPO)
            ElseIf isUnidadDeNegocios() Then
                iGeneradorSql.agregarCondicionWhere("idTipoNivel=" & TipoNivel.UNIDADDENEGOCIOS)
            ElseIf isSucursal() Then
                iGeneradorSql.agregarCondicionWhere("idTipoNivel=" & TipoNivel.SUCURSAL)
            End If

            iGeneradorSql.agregarTabla("nivel")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Return CLng(iDataReader.Item("id").ToString)
            Else
                Throw New NivelNoEncontradoException()
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New NivelNoEliminadoException(excepcion)
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
    End Function
#End Region

End Class
