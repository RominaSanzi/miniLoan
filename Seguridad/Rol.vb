Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.Configuration
Imports System.IO

Public Class Rol

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iNombre As String
    Private iDescripcion As String
    Private iDescripcionAmpliada As String
    Private iUsuario As Usuario
    Private iValor As Boolean
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
    Public Property descripcion() As String
        Get
            Return iDescripcion
        End Get
        Set(ByVal Value As String)
            iDescripcion = Value
        End Set
    End Property
    Public Property descripcionAmpliada() As String
        Get
            Return iDescripcionAmpliada
        End Get
        Set(ByVal Value As String)
            iDescripcionAmpliada = Value
        End Set
    End Property
    Public Property usuario() As Usuario
        Get
            Return iUsuario
        End Get
        Set(ByVal Value As Usuario)
            iUsuario = Value
        End Set
    End Property
    Public Property valor() As Boolean
        Get
            Return iValor
        End Get
        Set(ByVal Value As Boolean)
            iValor = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Sub crearRoles()
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataset As DataSet
        Dim i As Integer

        Try
            iConexion = obtenerConexion()

            iDataset = obtenerRoles()
            For i = 0 To iDataset.Tables("Roles").Rows.Count - 1
                iGeneradorSql.agregarColumna("idUsuario")
                iGeneradorSql.agregarColumna("idRol")
                iGeneradorSql.agregarColumna("Valor")
                iGeneradorSql.agregarValue(iUsuario.id)
                iGeneradorSql.agregarValue(iDataset.Tables("Roles").Rows(i).Item("id"))
                iGeneradorSql.agregarValue(FuncionComun.booleanByte(False))
                iGeneradorSql.agregarTabla("rolUsuario")
                iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            Next i

        Catch excepcion As Exception
            Throw New UsuarioNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iDataset = Nothing
        End Try
    End Sub

    Private Function obtenerRoles() As DataSet
        Dim iDataSet As DataSet
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarTabla("rol")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarOrden("nombre asc")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Roles")

            Return iDataSet

        Catch excepcion As Exception
            Throw New UsuarioNoCreadoException(excepcion)
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

    Public Sub eliminarRoles()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iUsuario.id)
            iGeneradorSql.agregarTabla("rolUsuario")
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New UsuarioNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerRolesAutorizados() As String()
        Dim iDataSet As DataSet
        Dim iGeneradorSql As New GeneradorSql()
        Dim i As Integer
        Dim iRolesAutorizados() As String

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarTabla("rol r")
            iGeneradorSql.agregarTabla("rolUsuario ru")
            iGeneradorSql.agregarColumna("r.nombre")
            iGeneradorSql.agregarCondicionWhere("ru.idRol= r.id")
            iGeneradorSql.agregarCondicionWhere("ru.idUsuario=" & iUsuario.id)
            iGeneradorSql.agregarCondicionWhere("ru.valor=" & FuncionComun.booleanByte(True))
            iGeneradorSql.agregarOrden("r.nombre")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "RolUsuario")

            ReDim iRolesAutorizados(iDataSet.Tables("RolUsuario").Rows.Count)

            For i = 1 To iDataSet.Tables("RolUsuario").Rows.Count
                iRolesAutorizados.SetValue(iDataSet.Tables("RolUsuario").Rows(i - 1).Item("nombre"), i)
            Next i

            Return iRolesAutorizados

        Catch excepcion As Exception
            Throw New UsuarioNoEncontradoException(excepcion)
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

    Public Function obtenerRolesHabilitar() As DataSet
        Dim iDataSet As DataSet
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarTabla("rol r")
            iGeneradorSql.agregarTabla("rolUsuario ru")
            iGeneradorSql.agregarColumna("distinct 1 as orden")
            iGeneradorSql.agregarColumna("0 as id")
            iGeneradorSql.agregarColumna("r.titulo as descripcion")
            iGeneradorSql.agregarColumna("r.titulo")
            iGeneradorSql.agregarColumna("'' as descripcionAmpliada")
            iGeneradorSql.agregarColumna("case when ru.valor is null or ru.valor=0 then 0 else 0 end as valor")
            iGeneradorSql.agregarCondicionWhere("ru.idRol=r.id")
            iGeneradorSql.agregarCondicionWhere("ru.idUsuario=" & iUsuario.id)


            iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


            iGeneradorSql.agregarTabla("rol r")
            iGeneradorSql.agregarTabla("rolUsuario ru")
            iGeneradorSql.agregarColumna("2 as orden")
            iGeneradorSql.agregarColumna("ru.id")
            iGeneradorSql.agregarColumna("r.descripcion")
            iGeneradorSql.agregarColumna("r.titulo")
            iGeneradorSql.agregarColumna("r.descripcionAmpliada")
            iGeneradorSql.agregarColumna("case when ru.valor is null or ru.valor=0 then 0 else 1 end as valor")
            iGeneradorSql.agregarCondicionWhere("ru.idRol=r.id")
            iGeneradorSql.agregarCondicionWhere("ru.idUsuario=" & iUsuario.id)

            iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)
            iGeneradorSql.agregarOrden("titulo,orden,descripcion asc")



            iDataSet = iConexion.getDataSet(iGeneradorSql.generarUnion, iGeneradorSql.parametrosSQL, "RolesUsuario")
            Return iDataSet

        Catch excepcion As Exception
            Throw New RolNoEncontradoException(excepcion)
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

    Public Function obtenerRolesHabilitarPorTitulo(ByVal eTitulo As String) As DataSet
        Dim iDataSet As DataSet
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()
            'iGeneradorSql.agregarTabla("rol r")
            'iGeneradorSql.agregarTabla("rolUsuario ru")
            'iGeneradorSql.agregarColumna("distinct 1 as orden")
            'iGeneradorSql.agregarColumna("0 as id")
            'iGeneradorSql.agregarColumna("r.titulo as descripcion")
            'iGeneradorSql.agregarColumna("r.titulo")
            'iGeneradorSql.agregarColumna("'' as descripcionAmpliada")
            'iGeneradorSql.agregarColumna("case when ru.valor is null or ru.valor=0 then 0 else 0 end as valor")
            'iGeneradorSql.agregarCondicionWhere("ru.idRol=r.id")
            'iGeneradorSql.agregarCondicionWhere("ru.idUsuario=" & iUsuario.id)
            'iGeneradorSql.agregarCondicionWhere("r.titulo like '%" & eTitulo & "%'")

            'iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


            iGeneradorSql.agregarTabla("rol r")
            iGeneradorSql.agregarTabla("rolUsuario ru")
            iGeneradorSql.agregarColumna("ru.id")
            iGeneradorSql.agregarColumna("r.descripcion")
            iGeneradorSql.agregarColumna("2 as orden")
            iGeneradorSql.agregarColumna("r.titulo")
            iGeneradorSql.agregarColumna("r.descripcionAmpliada")
            iGeneradorSql.agregarColumna("case when ru.valor is null or ru.valor=0 then 0 else 1 end as valor")
            iGeneradorSql.agregarColumna("r.id as idRol")
            iGeneradorSql.agregarCondicionWhere("ru.idRol=r.id")
            iGeneradorSql.agregarCondicionWhere("ru.idUsuario=" & iUsuario.id)
            iGeneradorSql.agregarCondicionWhere("r.titulo = '" & eTitulo & "'")

            iGeneradorSql.agregarOrden("descripcion asc")



            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "RolesUsuario")
            Return iDataSet

        Catch excepcion As Exception
            Throw New RolNoEncontradoException(excepcion)
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

    Public Function obtenerRolesUsuarioFila() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("0 as id")
            iGeneradorSql.agregarColumna(FuncionComun.sqlConcatenar("ifnull(r.titulo,'')") & " as titulo")

            iGeneradorSql.agregarTabla("rol r")

            iGeneradorSql.agregarGroupBy("titulo asc")
            iGeneradorSql.agregarOrden("titulo asc")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "RolesUsuarios")

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
    End Function


    Public Sub habilitarRol()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarSet("valor=" & FuncionComun.booleanByte(valor))
            iGeneradorSql.agregarCondicionWhere("id=" & iId)
            iGeneradorSql.agregarTabla("RolUsuario")
            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RolNoHabilitadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub habilitarRolPorUsuario()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarSet("valor=" & FuncionComun.booleanByte(valor))
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iUsuario.id)
            iGeneradorSql.agregarCondicionWhere("idRol=" & iId)
            iGeneradorSql.agregarTabla("RolUsuario")
            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RolNoHabilitadoException(excepcion)
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
            If Not IsNothing(iUsuario) Then
                iUsuario.dispose()
            End If
        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub

    Public Function rolHabilitadoConsulta() As String
        Dim iArchivoExcel As StreamWriter
        Dim iGeneradorSql As New GeneradorSql
        Dim iPathArchivo, iPathArchivoZipeado As String
        Dim iDataSet As DataSet
        Dim iDataColumn(2) As DataColumn
        Dim iColumnas(1) As Object
        Dim iDataRow As DataRow
        Dim iLinea As String
        Dim i, j As Integer

        Dim iColeccion As New Collection

        Try

            iPathArchivo = ConfigurationManager.AppSettings("archivosGenerados") & "RolHabilitado" & Format(Now, "ddMMyyyy") & Format(Now, "hhmm") & ".xls"
            iPathArchivoZipeado = ConfigurationManager.AppSettings("archivosGenerados") & "RolHabilitado" & Format(Now, "ddMMyyyy") & Format(Now, "hhmm") & ".zip"

            iConexion = obtenerConexion()

            '*****traigo los roles********
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarTabla("rol")

            iGeneradorSql.agregarOrden("nombre")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Rol")

            '*****traigo los usuarios********
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarTabla("usuario")

            iGeneradorSql.agregarOrden("nombre")

            iDataSet = iConexion.getDataSet(iDataSet, iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Usuario")

            '*****traigo los perfiles que tienen habilitado********
            iGeneradorSql.agregarColumna("ru.idRol")
            iGeneradorSql.agregarColumna("ru.idUsuario")
            iGeneradorSql.agregarColumna("ru.valor")
            iGeneradorSql.agregarColumna("u.nombre")

            iGeneradorSql.agregarTabla("RolUsuario ru")
            iGeneradorSql.agregarTabla("Usuario u")

            iGeneradorSql.agregarCondicionWhere("u.id=ru.idUsuario")

            iDataSet = iConexion.getDataSet(iDataSet, iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "RolUsuario")

            iDataColumn(0) = iDataSet.Tables("RolUsuario").Columns("idRol")
            iDataColumn(1) = iDataSet.Tables("RolUsuario").Columns("idUsuario")
            iDataSet.Tables("RolUsuario").PrimaryKey = iDataColumn

            iArchivoExcel = New StreamWriter(iPathArchivo)

            'Agrego el encabezado
            iLinea = "USUARIO" & vbTab
            For i = 0 To iDataSet.Tables("Rol").Rows.Count - 1
                iLinea &= Trim(iDataSet.Tables("Rol").Rows(i).Item("nombre").ToString.ToUpper) & vbTab
            Next i
            iArchivoExcel.WriteLine(iLinea)

            'Agrego los menus y pongo una x si el perfil lo tiene habilitado
            For i = 0 To iDataSet.Tables("Usuario").Rows.Count - 1
                iLinea = iDataSet.Tables("Usuario").Rows(i).Item("nombre").ToString.ToUpper & vbTab

                For j = 0 To iDataSet.Tables("Rol").Rows.Count - 1
                    iColumnas(0) = iDataSet.Tables("Rol").Rows(j).Item("id")
                    iColumnas(1) = iDataSet.Tables("Usuario").Rows(i).Item("id")
                    iDataRow = iDataSet.Tables("RolUsuario").Rows.Find(iColumnas)
                    If Not IsNothing(iDataRow) Then
                        If iDataRow("valor").ToString = "1" Then
                            iLinea &= "X" & vbTab
                        Else
                            iLinea &= vbTab
                        End If
                    Else
                        iLinea &= vbTab
                    End If
                Next j
                iArchivoExcel.WriteLine(iLinea)
            Next i

            iArchivoExcel.Close()

            iColeccion.Add(iPathArchivo)
            FuncionComun.zipearArchivos(iColeccion, iPathArchivoZipeado)

            Return iPathArchivoZipeado

        Catch exception As Exception
            Throw New RolNoEncontradoException
        Finally
            If Not IsNothing(iArchivoExcel) Then iArchivoExcel.Close()
            iArchivoExcel = Nothing
            If File.Exists(iPathArchivo) Then File.Delete(iPathArchivo)
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

    Public Sub habilitarRolPorDescripcion()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarSet("valor=" & FuncionComun.booleanByte(valor))
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iUsuario.id)
            iGeneradorSql.agregarCondicionWhere("idrol in (select id from rol where nombre='" & iDescripcion & "')", True)
            iGeneradorSql.agregarTabla("RolUsuario")
            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RolNoHabilitadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub deshabilitarRolesPorUsuario()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            '================================================
            'DESHABILITA TODOS LOS REOLES DEL USUARIO SETEADO
            '================================================
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("rolUsuario")
            iGeneradorSql.agregarSet("valor=" & FuncionComun.booleanByte(False))
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & iUsuario.id)
            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New UsuarioNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub copiarRolesUsuario(eRolCopiarVO As RolCopiarVO)
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            For Each iUsuarioLista As Usuario In eRolCopiarVO.usuarios
                '1) DESHABILITAMOS TODOS LOS ROLES PARA LOS USUARIOS QUE VIENEN EN LA LISTA
                usuario = iUsuarioLista
                deshabilitarRolesPorUsuario()

                '2) A ESTOS USUARIOS LES HABILITAMOS LOS ROLES QUE VIENEN EN LA LISTA
                For Each iRol As Rol In eRolCopiarVO.rolesSeleccionados
                    With iRol
                        .accesoDatos = iConexion
                        .usuario = iUsuario
                        .valor = True
                        .habilitarRolPorUsuario()
                        .accesoDatos = Nothing
                    End With
                Next
            Next

        Catch excepcion As Exception
            Throw New RolNoHabilitadoException(excepcion)
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
