Imports di.financiera.excepciones
Imports di.financiera.entidades
Imports di.financiera.utils
Imports di.financiera.datos
Imports System.IO
Imports System.Collections.Generic
Imports System.Configuration

Public Class Perfil

    Inherits Entidad

#Region "Constantes"
    Public Const PERFILUSUARIOCAJEROMIGRACION = 1
    Public Const PERFILCOMERCIO = 21
#End Region

#Region "Variables"
    Private iId As Long
    Private iNombre As String
    Private iMenu As Menu
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
    Public Property nombre() As String
        Get
            Return iNombre
        End Get
        Set(ByVal Value As String)
            iNombre = Value
        End Set
    End Property
    Public Property menu() As Menu
        Get
            Return iMenu
        End Get
        Set(ByVal Value As Menu)
            iMenu = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerPerfil() As Perfil
        Dim iDataReader As IDataReader

        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarTabla("perfil")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If nombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("nombre=" & FuncionComun.nuloSiEsNothing(iNombre))


            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iNombre = iDataReader.Item("nombre").ToString
                Return Me
            Else
                Throw New PerfilNoEncontradoException()
            End If
        Catch excepcion As Exception
            Throw New PerfilNoEncontradoException(excepcion)
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


    Public Function obtenerPerfiles() As IDataReader
        Dim iGeneradorSql As New GeneradorSql()
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("Nombre")
            iGeneradorSql.agregarTabla("Perfil")
            iGeneradorSql.agregarOrden("nombre asc")

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New PerfilNoEncontradoException()
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Private Sub validarCrear()

        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarTabla("Perfil")
            iGeneradorSql.agregarCondicionWhere("nombre='" & nombre & "'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New PerfilNoCreadoException("El perfil ya existe")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New PerfilNoCreadoException(excepcion)
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

    Public Sub crear()

        Dim iGeneradorSql As New GeneradorSql()
        Try
            iConexion = obtenerConexion()
            validarCrear()

            iGeneradorSql.agregarTabla("perfil")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(nombre))

            iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

            obtenerUltimoId()
        Catch exception As Exception
            Throw New PerfilNoCreadoException()
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

        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarTabla("perfil")
            iGeneradorSql.agregarColumna("max(id) as id")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                iId = CLng(iDataReader.Item("id").ToString)
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
    End Sub

    Public Sub asignarPerfiles()
        Dim iGeneradorSql As New GeneradorSql()
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("perfilMenu")
            iGeneradorSql.agregarColumna("idPerfil")
            iGeneradorSql.agregarColumna("idMenu")

            iGeneradorSql.agregarValue(id)
            iGeneradorSql.agregarValue(menu.id)

            iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)


        Catch exception As Exception
            Throw New PerfilNoCreadoException()
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub asignarPerfilesLista(ByVal eLista As List(Of Long))
        Dim iGeneradorSql As New GeneradorSql()
        Dim iQuery As String = ""
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("perfilMenu")
            iGeneradorSql.agregarColumna("idPerfil")
            iGeneradorSql.agregarColumna("idMenu")

            For Each iIdMenu As Long In eLista
                iQuery &= "(" & id & "," & iIdMenu & "),"
            Next
            iQuery = iQuery.Substring(1, iQuery.Length - 3)
            iGeneradorSql.agregarValue(iQuery, True)

            iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)


        Catch exception As Exception
            Throw New PerfilNoCreadoException()
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub modificar(ByVal ePerfilesSeleccionados As String, ByVal ePerfilesNoSeleccionados As String, ByVal ePerfilesSeleccionadosColeccion As Collection, ByVal eFinal As Boolean)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader
        Dim i As Integer
        Dim iStringMenus As String

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("perfilMenu p")
            iGeneradorSql.agregarTabla("Menu m")
            iGeneradorSql.agregarCondicionWhere("p.idmenu=m.id")
            iGeneradorSql.agregarCondicionWhere("m.final=" & FuncionComun.booleanByte(eFinal))
            iGeneradorSql.agregarColumna("idMenu")
            iGeneradorSql.agregarCondicionWhere("p.idperfil=" & FuncionComun.nuloSiEsNothing(id))

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            While iDataReader.Read
                If iStringMenus <> Nothing Then iStringMenus = iStringMenus & ","
                iStringMenus = iStringMenus & iDataReader.Item("idmenu").ToString
            End While
            iDataReader.Close()

            If iStringMenus <> Nothing Then
                iGeneradorSql.agregarTabla("perfilmenu")
                iGeneradorSql.agregarCondicionWhere("idmenu in(" & iStringMenus & ")")
                iGeneradorSql.agregarCondicionWhere("idPerfil=" & FuncionComun.nuloSiEsNothing(id))

                iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)
            End If
            iStringMenus = Nothing

            If ePerfilesNoSeleccionados <> Nothing Then
                iGeneradorSql.agregarTabla("perfilMenu p")
                iGeneradorSql.agregarTabla("Menu m")
                iGeneradorSql.agregarCondicionWhere("p.idmenu=m.id")
                iGeneradorSql.agregarCondicionWhere("m.final=" & FuncionComun.booleanByte(True))
                iGeneradorSql.agregarCondicionWhere("m.idpadre in(" & ePerfilesNoSeleccionados & ")")
                iGeneradorSql.agregarColumna("idMenu")
                iGeneradorSql.agregarCondicionWhere("p.idperfil=" & FuncionComun.nuloSiEsNothing(id))

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                While iDataReader.Read
                    If iStringMenus <> Nothing Then iStringMenus = iStringMenus & ","
                    iStringMenus = iStringMenus & iDataReader.Item("idmenu").ToString
                End While
                iDataReader.Close()

                If iStringMenus <> Nothing Then
                    iGeneradorSql.agregarTabla("perfilMenu")
                    iGeneradorSql.agregarCondicionWhere("idmenu in(" & iStringMenus & ")")
                    iGeneradorSql.agregarCondicionWhere("idPerfil=" & FuncionComun.nuloSiEsNothing(id))

                    iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

                End If
            End If

            For i = 1 To ePerfilesSeleccionadosColeccion.Count

                iGeneradorSql.agregarTabla("perfilMenu")
                iGeneradorSql.agregarColumna("idPerfil")
                iGeneradorSql.agregarColumna("idMenu")

                iGeneradorSql.agregarValue(id)
                iGeneradorSql.agregarValue(CLng(ePerfilesSeleccionadosColeccion.Item(i)))

                iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            Next i

            'actualizo el nombre del perfil
            iGeneradorSql.agregarTabla("perfil")
            iGeneradorSql.agregarSet("nombre=" & FuncionComun.nuloSiEsNothing(iNombre))
            iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New PerfilNoModificadoException(exception)
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

        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("idPerfil")
            iGeneradorSql.agregarTabla("Usuario")
            iGeneradorSql.agregarCondicionWhere("idPerfil=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New PerfilNoEliminadoException("El perfil esta siendo utilizado por un usuario")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("idPerfil")
            iGeneradorSql.agregarTabla("IntranetArchivoPerfiles")
            iGeneradorSql.agregarCondicionWhere("idPerfil=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New PerfilNoEliminadoException("El perfil esta siendo utilizado por una intranet Archivo")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("idPerfil")
            iGeneradorSql.agregarTabla("IntranetCarteleraPerfiles")
            iGeneradorSql.agregarCondicionWhere("idPerfil=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New PerfilNoEliminadoException("El perfil esta siendo utilizado por una intranet cartelera")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New PerfilNoEliminadoException(excepcion)
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

        Dim iGeneradorSql As New GeneradorSql()
        Try
            iConexion = obtenerConexion()
            validarEliminar()

            iGeneradorSql.agregarTabla("PerfilMenu")
            iGeneradorSql.agregarCondicionWhere("idPerfil=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iGeneradorSql.agregarTabla("Perfil")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New PerfilNoEliminadoException()
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function perfilHabilitadoConsulta() As String
        Dim iArchivoExcel As StreamWriter
        Dim iGeneradorSql As New GeneradorSql
        Dim iPathArchivo As String
        Dim iDataSet As DataSet
        Dim iDataColumn(2) As DataColumn
        Dim iColumnas(1) As Object
        Dim iDataRow As DataRow
        Dim iLinea As String
        Dim i, j As Integer

        Try

            iPathArchivo = ConfigurationManager.AppSettings("archivosGenerados") & "PerfilHabilitado" & Format(Now, "ddMMyyyy") & Format(Now, "hhmm") & ".xls"

            iConexion = obtenerConexion()

            '*****traigo los menús********
            iGeneradorSql.agregarColumna("m.id")
            iGeneradorSql.agregarColumna("m.nombre")
            iGeneradorSql.agregarColumna("mm.nombre as nombrePadre")
            iGeneradorSql.agregarTabla("menu m")
            iGeneradorSql.agregarTabla("menu mm")
            iGeneradorSql.agregarCondicionWhere("m.idPadre=mm.id")

            iGeneradorSql.agregarOrden("mm.nombre")
            iGeneradorSql.agregarOrden("m.nombre")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Menu")

            '*****traigo los perfiles********
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")

            iGeneradorSql.agregarTabla("perfil")

            iGeneradorSql.agregarOrden("nombre ASC")

            iDataSet = iConexion.getDataSet(iDataSet, iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Perfil")

            '*****traigo los perfiles que tienen habilitado********
            iGeneradorSql.agregarColumna("idPerfil")
            iGeneradorSql.agregarColumna("idMenu")

            iGeneradorSql.agregarTabla("perfilMenu")

            iDataSet = iConexion.getDataSet(iDataSet, iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "PerfilMenu")

            iDataColumn(0) = iDataSet.Tables("PerfilMenu").Columns("idPerfil")
            iDataColumn(1) = iDataSet.Tables("PerfilMenu").Columns("idMenu")
            iDataSet.Tables("PerfilMenu").PrimaryKey = iDataColumn

            iArchivoExcel = New StreamWriter(iPathArchivo)

            'Agrego el encabezado
            iLinea = "MENU PADRE" & vbTab & "MENU" & vbTab
            For i = 0 To iDataSet.Tables("Perfil").Rows.Count - 1
                iLinea &= Trim(iDataSet.Tables("Perfil").Rows(i).Item("nombre")) & vbTab
            Next i
            iArchivoExcel.WriteLine(iLinea)

            'Agrego los menus y pongo una x si el perfil lo tiene habilitado
            For i = 0 To iDataSet.Tables("Menu").Rows.Count - 1
                iLinea = iDataSet.Tables("Menu").Rows(i).Item("nombrePadre").ToString.ToUpper & vbTab
                iLinea &= iDataSet.Tables("Menu").Rows(i).Item("nombre").ToString.ToUpper & vbTab

                For j = 0 To iDataSet.Tables("Perfil").Rows.Count - 1
                    iColumnas(0) = iDataSet.Tables("Perfil").Rows(j).Item("id")
                    iColumnas(1) = iDataSet.Tables("Menu").Rows(i).Item("id")
                    iDataRow = iDataSet.Tables("PerfilMenu").Rows.Find(iColumnas)
                    If Not IsNothing(iDataRow) Then
                        iLinea &= "X" & vbTab
                    Else
                        iLinea &= vbTab
                    End If
                Next j
                iArchivoExcel.WriteLine(iLinea)
            Next i

            Return iPathArchivo

        Catch exception As Exception
            Throw New MenuNoEncontradoException
        Finally
            If Not IsNothing(iArchivoExcel) Then iArchivoExcel.Close()
            iArchivoExcel = Nothing
            iDataSet = Nothing
            iDataColumn = Nothing
            iColumnas = Nothing
            iDataRow = Nothing
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerPerfilesGrilla() As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("Nombre")
            iGeneradorSql.agregarTabla("Perfil")
            iGeneradorSql.agregarOrden("Nombre")
            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "perfiles")

        Catch exception As Exception
            Throw New PerfilNoEncontradoException
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Function

    Public Function obtenerArbolPerfil(ByVal ePerfiles As List(Of Perfil), Optional eEsAsignadoPerfil As Boolean = False, Optional eMenu As Menu = Nothing) As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataset As DataSet
        Dim iDataColumn(1) As DataColumn
        Dim iIdsPerfiles As String = ""

        Try

            iConexion = obtenerConexion()

            If Not IsNothing(ePerfiles) AndAlso ePerfiles.Count > 0 Then
                For Each iPerfil As Perfil In ePerfiles
                    iIdsPerfiles &= iPerfil.id & ","
                Next
                iIdsPerfiles = Left(iIdsPerfiles, iIdsPerfiles.Length - 1)
            End If

            'generamos un union para primero obtener el elemento raiz

            iGeneradorSql.agregarColumna("m.id")
            iGeneradorSql.agregarColumna("m.nombre")
            iGeneradorSql.agregarColumna("a.paginaasociada")
            iGeneradorSql.agregarColumna("a.esSubMenu")
            iGeneradorSql.agregarColumna("a.grabaLog")
            iGeneradorSql.agregarColumna("m.final")
            iGeneradorSql.agregarColumna("a.id as idAccion")
            iGeneradorSql.agregarColumna("a.nombre as nombreAccion")
            iGeneradorSql.agregarColumna("m.idTipoSistema")
            iGeneradorSql.agregarColumna("m.idPadre")

            'SI VIENE EL PARÁMETRO DE MENÚ INSTANCIADO, BUSCAMOS TODOS LOS HIJOS DE ESE PADRE
            If Not IsNothing(eMenu) AndAlso eMenu.id <> Nothing Then
                iGeneradorSql.agregarCondicionWhere("m.id=" & eMenu.id)
            Else
                iGeneradorSql.agregarCondicionWhere("m.idpadre is null")
            End If

            If Len(iIdsPerfiles) > 0 Then
                iGeneradorSql.agregarTablaPrincipal("menu m")
                iGeneradorSql.agregarTablaConJoin("perfilmenu pm", "m.id=pm.idmenu and (pm.idPerfil in (" & iIdsPerfiles & "))", Not eEsAsignadoPerfil)
                iGeneradorSql.agregarColumna("case when pm.idmenu is null then 0 else 1 end as asignado")
                iGeneradorSql.agregarGroupBy("id")
            Else
                iGeneradorSql.agregarTabla("menu m")
                iGeneradorSql.agregarColumna("0 as asignado")
            End If

            iGeneradorSql.agregarTablaConJoin("accion a", "m.idaccion=a.id", True)
            iGeneradorSql.agregarOrden("m.nombre")

            iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)

            'luego el resto de los nodos

            iGeneradorSql.agregarColumna("m.id")
            iGeneradorSql.agregarColumna("m.nombre")
            iGeneradorSql.agregarColumna("a.paginaasociada")
            iGeneradorSql.agregarColumna("a.esSubMenu")
            iGeneradorSql.agregarColumna("a.grabaLog")
            iGeneradorSql.agregarColumna("m.final")
            iGeneradorSql.agregarColumna("a.id as idAccion")
            iGeneradorSql.agregarColumna("a.nombre as nombreAccion")
            iGeneradorSql.agregarColumna("m.idTipoSistema")
            iGeneradorSql.agregarColumna("m.idPadre")

            'SI VIENE EL PARÁMETRO DE MENÚ INSTANCIADO, BUSCAMOS TODOS LOS HIJOS DE ESE PADRE
            'If Not IsNothing(eMenu) AndAlso eMenu.id <> Nothing Then
            '    iGeneradorSql.agregarCondicionWhere("m.idPadre=" & eMenu.id)
            'Else
            iGeneradorSql.agregarCondicionWhere("m.idpadre is not null")
            'End If

            If Len(iIdsPerfiles) > 0 Then
                iGeneradorSql.agregarTablaPrincipal("menu m")
                iGeneradorSql.agregarTablaConJoin("perfilmenu pm", "m.id=pm.idmenu and (pm.idPerfil in (" & iIdsPerfiles & "))", Not eEsAsignadoPerfil)
                iGeneradorSql.agregarColumna("case when pm.idmenu is null then 0 else 1 end as asignado")
                iGeneradorSql.agregarGroupBy("id")
            Else
                iGeneradorSql.agregarTabla("menu m")
                iGeneradorSql.agregarColumna("0 as asignado")
            End If

            iGeneradorSql.agregarTablaConJoin("accion a", "m.idaccion=a.id", True)

            iGeneradorSql.agregarOrden("m.nombre")

            iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)

            iDataset = iConexion.getDataSet(iGeneradorSql.generarUnion, iGeneradorSql.parametrosSQL, "ArbolPerfil")

            Return iDataset

        Catch exception As Exception
            Throw New MenuNoEncontradoException
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Sub vaciar()

        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("PerfilMenu")
            iGeneradorSql.agregarCondicionWhere("idPerfil=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New PerfilNoEliminadoException
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

#Region "Menu"

    Public Function obtenerMenu(ByVal ePerfiles As List(Of Perfil), eTipoSistema As Menu.EnumTipoSistema) As Menu
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("icono")
            iGeneradorSql.agregarTabla("menu")
            iGeneradorSql.agregarCondicionWhere("idpadre is null") ' Encuentro al padre
            iGeneradorSql.agregarCondicionWhere("idTipoSistema =" & eTipoSistema)


            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            'armo una coleccion con cada uno de los menu
            If iDataReader.Read Then
                Dim iMenuCompuesto As New MenuCompuesto()
                iMenuCompuesto.id = iDataReader.Item("id").ToString
                iMenuCompuesto.nombre = iDataReader.Item("nombre").ToString
                iMenuCompuesto.icono = iDataReader.Item("icono").ToString
                iMenuCompuesto.idRaiz = iDataReader.Item("id").ToString
                iDataReader.Close()
                Return armarMenu(ePerfiles, iMenuCompuesto)
            Else
                Throw New MenuNoEncontradoException()
            End If
        Catch excepcion As Exception
            Throw New MenuNoEncontradoException(excepcion)
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

    Public Function obtenerMenuResponsivo(ByVal ePerfiles As List(Of Perfil)) As Menu
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataset As DataSet
        Dim iDatarowRaiz As DataRow
        Try

            iDataset = obtenerArbolMenu(ePerfiles)

            If iDataset.Tables("menu").Rows.Count > 0 Then

                'armo una coleccion con cada uno de los menu
                iDatarowRaiz = iDataset.Tables("menu").Rows(0)

                Dim iMenuCompuesto As New MenuCompuesto()
                iMenuCompuesto.id = iDatarowRaiz.Item("idMenu").ToString
                iMenuCompuesto.nombre = iDatarowRaiz.Item("nombreMenu").ToString
                iMenuCompuesto.idRaiz = iDatarowRaiz.Item("idMenu").ToString
                Return armarMenuResponsivo(iMenuCompuesto, iDataset)
            Else
                Throw New MenuNoEncontradoException()
            End If
        Catch excepcion As Exception
            Throw New MenuNoEncontradoException(excepcion)
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

    'ArmarMenu es una funcion recursiva que toma un menu compuesto y completa todos sus hijos 
    'si alguno de ellos es compuesto se llama recursivamente de manera de recorrer todo el arbol 

    Private Function armarMenu(ByVal ePerfiles As List(Of Perfil), ByVal iMenuCompuesto As MenuCompuesto) As Menu
        'Esta funcion buscas todos los hijos del menu y devuelve el menu, es una funcion recursiva
        Dim iDataSet As DataSet
        Dim iDataTable As DataTable

        Dim iGeneradorSql As New GeneradorSql()
        Dim i As Integer
        Dim iIdsPerfiles As String = ""

        Try

            iConexion = obtenerConexion()

            For Each iPerfil As Perfil In ePerfiles
                iIdsPerfiles &= iPerfil.id & ","
            Next
            iIdsPerfiles = Left(iIdsPerfiles, iIdsPerfiles.Length - 1)


            'Busco todos los hijos del menu compuesto de mi perfil
            'Se deberia armar con el generador la query
            'select a.id, a.nombre, a.idPadre, a.idAccion, a.final, b.nombre, b.paginaasociada from menu as a, accion as b, menuperfil as c where a.id = c.idmenu and a.idaccion += b.id and c.idperfil = id

            'Query que saca todos los menu de un perfil
            iGeneradorSql.agregarColumna("distinct b.id as idMenu")

            iGeneradorSql.agregarColumna("b.nombre as nombreMenu")
            iGeneradorSql.agregarColumna("b.idpadre as idPadre")
            iGeneradorSql.agregarColumna("b.final")
            iGeneradorSql.agregarColumna("b.nivel")
            iGeneradorSql.agregarColumna("c.nombre as idNombreAccion")
            iGeneradorSql.agregarColumna("c.paginaAsociada")
            iGeneradorSql.agregarColumna("b.icono")

            iGeneradorSql.agregarTablaPrincipal("perfilmenu a")
            iGeneradorSql.agregarTablaConJoin("menu b", "a.idMenu = b.id")
            iGeneradorSql.agregarTablaConJoin("accion c", "b.idAccion = c.id")

            iGeneradorSql.agregarCondicionWhere("a.idPerfil in(" & iIdsPerfiles & ")")
            iGeneradorSql.agregarCondicionWhere("b.idPadre = " & iMenuCompuesto.id)

            iGeneradorSql.agregarOrden("b.final DESC")
            iGeneradorSql.agregarOrden("b.Nombre ASC")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "menu")

            iDataTable = iDataSet.Tables("menu")
            'leo todos los elementos de menu que son mis hijos
            For i = 0 To iDataTable.Rows.Count - 1

                If iDataTable.Rows(i).Item("final") = 0 Then
                    'Es un menu compuesto 
                    Dim menuCompuesto As New MenuCompuesto()
                    menuCompuesto.id = iDataTable.Rows(i).Item("idMenu")
                    menuCompuesto.nombre = iDataTable.Rows(i).Item("nombreMenu")
                    menuCompuesto.nivel = iDataTable.Rows(i).Item("nivel")
                    menuCompuesto.idPadre = iDataTable.Rows(i).Item("idPadre")
                    menuCompuesto.icono = iDataTable.Rows(i).Item("icono").ToString

                    iMenuCompuesto.menues.Add(armarMenu(ePerfiles, menuCompuesto))
                Else
                    'Es una hoja
                    Dim iMenuSimple As New MenuSimple()
                    Dim iAccion As New Accion()
                    iAccion.nombre = iDataTable.Rows(i).Item("idNombreAccion")
                    iAccion.paginaAsociada = iDataTable.Rows(i).Item("paginaAsociada")
                    iMenuSimple.id = iDataTable.Rows(i).Item("idMenu")
                    iMenuSimple.nivel = iDataTable.Rows(i).Item("nivel")
                    iMenuSimple.idPadre = iDataTable.Rows(i).Item("idPadre")
                    iMenuSimple.nombre = iDataTable.Rows(i).Item("nombreMenu")
                    iMenuSimple.accion = iAccion
                    iMenuSimple.icono = iDataTable.Rows(i).Item("icono").ToString

                    iMenuCompuesto.menues.Add(iMenuSimple)
                End If
            Next i
            Return iMenuCompuesto
        Catch excepcion As Exception
            Throw New MenuNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iDataSet = Nothing
            iDataTable = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Private Function armarMenuResponsivo(ByVal iMenuCompuesto As MenuCompuesto, ByVal eDataset As DataSet) As Menu
        'Esta funcion buscas todos los hijos del menu y devuelve el menu, es una funcion recursiva
        Dim iDataTable As DataTable
        Dim iDataRowsHijos() As DataRow

        Try
            iConexion = obtenerConexion()
            'Busco todos los hijos del menu compuesto de mi perfil

            iDataTable = eDataset.Tables("menu")

            iDataRowsHijos = iDataTable.Select("idPadre=" & iMenuCompuesto.id, "final desc, nombreMenu ASC")

            'leo todos los elementos de menu que son mis hijos
            For Each iDataRow As DataRow In iDataRowsHijos

                If iDataRow.Item("final") = 0 Then
                    'Es un menu compuesto 
                    Dim menuCompuesto As New MenuCompuesto()
                    menuCompuesto.id = iDataRow.Item("idMenu").ToString()
                    menuCompuesto.nombre = iDataRow.Item("nombreMenu").ToString()
                    menuCompuesto.nivel = iDataRow.Item("nivel").ToString()
                    menuCompuesto.idPadre = iDataRow.Item("idPadre").ToString()
                    menuCompuesto.palabraClave = iDataRow.Item("palabraClave").ToString()
                    menuCompuesto.icono = iDataRow.Item("icono").ToString()
                    menuCompuesto.tooltip = iDataRow.Item("tooltip").ToString()

                    iMenuCompuesto.menues.Add(armarMenuResponsivo(menuCompuesto, eDataset))

                Else
                    'Es una hoja
                    Dim iMenuSimple As New MenuSimple()
                    Dim iAccion As New Accion()
                    iAccion.nombre = iDataRow.Item("idNombreAccion").ToString()
                    iAccion.paginaAsociada = iDataRow.Item("paginaAsociada").ToString()
                    iMenuSimple.id = iDataRow.Item("idMenu").ToString()
                    iMenuSimple.nivel = iDataRow.Item("nivel").ToString()
                    iMenuSimple.idPadre = iDataRow.Item("idPadre").ToString()
                    iMenuSimple.nombre = iDataRow.Item("nombreMenu").ToString()
                    iMenuSimple.palabraClave = iDataRow.Item("palabraClave").ToString()
                    iMenuSimple.icono = iDataRow.Item("icono").ToString()
                    iMenuSimple.tooltip = iDataRow.Item("tooltip").ToString()

                    iMenuSimple.accion = iAccion

                    iMenuCompuesto.menues.Add(iMenuSimple)
                End If
            Next

            Return iMenuCompuesto

        Catch excepcion As Exception
            Throw New MenuNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iDataTable = Nothing
        End Try
    End Function

    Private Function obtenerArbolMenu(ByVal ePerfiles As List(Of Perfil)) As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataset As DataSet
        Dim iDataColumn(1) As DataColumn
        Dim iIdsPerfiles As String

        Try
            iConexion = obtenerConexion()

            For Each iPerfil As Perfil In ePerfiles
                iIdsPerfiles &= iPerfil.id & ","
            Next
            iIdsPerfiles = Left(iIdsPerfiles, iIdsPerfiles.Length - 1)

            iGeneradorSql.agregarColumna("b.id as idMenu")
            iGeneradorSql.agregarColumna("b.nombre as nombreMenu")
            iGeneradorSql.agregarColumna("b.idpadre as idPadre")
            iGeneradorSql.agregarColumna("b.final")
            iGeneradorSql.agregarColumna("b.nivel")
            iGeneradorSql.agregarColumna("c.nombre as idNombreAccion")
            iGeneradorSql.agregarColumna("c.paginaAsociada")
            iGeneradorSql.agregarColumna("b.palabraClave")
            iGeneradorSql.agregarColumna("b.icono")
            iGeneradorSql.agregarColumna("d.tooltip")

            iGeneradorSql.agregarTabla("perfilmenu a")
            iGeneradorSql.agregarTabla("menu b left JOIN accion c ON (b.idAccion = c.id) left JOIN menutooltip d ON (b.id= d.idMenu)")

            iGeneradorSql.agregarCondicionWhere("a.idMenu = b.id")
            iGeneradorSql.agregarCondicionWhere("a.idPerfil in (" & iIdsPerfiles & ")")
            iGeneradorSql.agregarCondicionWhere("c.id IS NULL OR c.esSubMenu = " & FuncionComun.booleanByte(True))

            iGeneradorSql.agregarCondicionWhere("b.idpadre is null")
            iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)

            iGeneradorSql.agregarColumna("b.id as idMenu")
            iGeneradorSql.agregarColumna("b.nombre as nombreMenu")
            iGeneradorSql.agregarColumna("b.idpadre as idPadre")
            iGeneradorSql.agregarColumna("b.final")
            iGeneradorSql.agregarColumna("b.nivel")
            iGeneradorSql.agregarColumna("c.nombre as idNombreAccion")
            iGeneradorSql.agregarColumna("c.paginaAsociada")
            iGeneradorSql.agregarColumna("b.palabraClave")
            iGeneradorSql.agregarColumna("b.icono")
            iGeneradorSql.agregarColumna("d.tooltip")

            iGeneradorSql.agregarTabla("perfilmenu a")
            iGeneradorSql.agregarTabla("menu b left JOIN accion c ON (b.idAccion = c.id) left JOIN menutooltip d ON (b.id= d.idMenu)")

            iGeneradorSql.agregarCondicionWhere("a.idMenu = b.id")
            iGeneradorSql.agregarCondicionWhere("a.idPerfil in (" & iIdsPerfiles & ")")
            iGeneradorSql.agregarCondicionWhere("c.id IS NULL OR c.esSubMenu = " & FuncionComun.booleanByte(True))

            iGeneradorSql.agregarOrden("b.Nombre ASC")

            iGeneradorSql.agregarCondicionWhere("b.idpadre is not null")
            iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)

            iDataset = iConexion.getDataSet(iGeneradorSql.generarUnion, iGeneradorSql.parametrosSQL, "menu")

            Return iDataset

        Catch exception As Exception
            Throw New MenuNoEncontradoException
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function
#End Region


    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iMenu) Then
                iMenu.dispose()
            End If
        Catch exception As Exception
            Throw New RootException(exception)
        End Try

    End Sub

#End Region

End Class
