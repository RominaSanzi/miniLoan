Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports di.financiera.datos

Public Class GeneradorSql
    Private Enum EnumColecciones
        TABLAS = 1
        COMUMNAS = 2
        WHERES = 3
        TABLASESPECIALES = 4
        JOIN = 5
        GROUPBY = 6
        LIMIT = 7
        HAVING = 8
        ORDER = 9
        COMUMNASESPECIALES = 10
        VALUES = 11
        VALUESAGRUPADOS = 12
        SETS = 13
    End Enum

#Region "Variables"
    Private iColumnas As Collection
    Private iTablas As Collection
    Private iTablaPrincipal As String = ""
    Private iCondicionesJoin As Collection
    Private iCondicionesWhere As Collection
    Private iCondicionesWhereConOr As Collection
    Private iSet As Collection
    Private iSelect As Collection
    Private iOrden As Collection
    Private iValues As Collection
    Private iValuesAgrupados As Collection
    Private iGroupBy As Collection
    Private iLimit As Collection
    Private iHaving As Collection
    Private iBloquearTabla As Boolean
    Private iParametrosSQL As List(Of ParametroSQL)
    Private iParametrosSQLStoredProcedure As List(Of ParametrosSQLStoredProcedureVO)
    Private iIndiceParametro As Integer
    Dim iBaseDeDatos As String
#End Region

#Region "Atributos"
    Public Property parametrosSQL() As List(Of ParametroSQL)
        Get
            Return iParametrosSQL
        End Get
        Set(ByVal Value As List(Of ParametroSQL))
            iParametrosSQL = Value
        End Set
    End Property

    Public Property bloquearTabla As Boolean
        Get
            Return iBloquearTabla
        End Get
        Set(value As Boolean)
            iBloquearTabla = value
        End Set
    End Property

    Public Property ParametrosSQLStoredProcedure() As List(Of ParametrosSQLStoredProcedureVO)
        Get
            Return iParametrosSQLStoredProcedure
        End Get
        Set(ByVal Value As List(Of ParametrosSQLStoredProcedureVO))
            iParametrosSQLStoredProcedure = Value
        End Set
    End Property
#End Region

#Region "Constructores"
    Public Sub New()
        'creamos las collections
        iColumnas = New Collection()
        iTablas = New Collection()
        iCondicionesJoin = New Collection
        iCondicionesWhere = New Collection
        iCondicionesWhereConOr = New Collection
        iSet = New Collection()
        iSelect = New Collection()
        iValues = New Collection()
        iValuesAgrupados = New Collection()
        iOrden = New Collection()
        iLimit = New Collection()
        iGroupBy = New Collection
        iHaving = New Collection
        iParametrosSQL = New List(Of ParametroSQL)
        iParametrosSQLStoredProcedure = New List(Of ParametrosSQLStoredProcedureVO)
        iBaseDeDatos = ConfigurationManager.AppSettings("baseDeDatos")
    End Sub
#End Region

#Region "Metodos"

    Public Sub agregarColumna(ByVal eColumna As String)
        iColumnas.Add(eColumna)
    End Sub

    Public Sub agregarTabla(ByVal eTabla As String)
        iTablas.Add(eTabla.ToLower)
    End Sub
    Public Sub agregarTablaPrincipal(ByVal eTabla As String)
        iTablaPrincipal = eTabla
    End Sub

    Public Sub agregarTablaConJoin(ByVal eTabla As String, ByVal eCondicion As String, Optional ByVal esLeft As Boolean = False)
        If esLeft Then
            iCondicionesJoin.Add(" LEFT JOIN " & eTabla & " ON " & eCondicion)
        Else
            iCondicionesJoin.Add(" INNER JOIN " & eTabla & " ON " & eCondicion)
        End If
    End Sub

    Public Sub agregarOrden(ByVal eOrden As String)
        iOrden.Add(eOrden)
    End Sub

    Public Sub agregarGroupBy(ByVal eGroupBy As String)
        iGroupBy.Add(eGroupBy)
    End Sub

    Public Sub agregarValue(ByVal eValue As String, Optional eSinGuardarParametros As Boolean = False)
        Dim iParametroSQL As New ParametroSQL

        Try

            iIndiceParametro += 1
            If Not eSinGuardarParametros Then
                With iParametroSQL
                    .valor = eValue
                    .nombre = iColumnas.Item(iValues.Count + 1) & iIndiceParametro
                    .nombreCampo = iColumnas.Item(iValues.Count + 1).ToString
                End With
                iParametrosSQL.Add(iParametroSQL)

                iValues.Add(IIf((iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14"), "@", "?") & iColumnas.Item(iValues.Count + 1) & iIndiceParametro)
            Else
                iValues.Add(eValue)
            End If
        Catch exception As Exception
        End Try

    End Sub

    Public Sub agregarValueEncriptado(ByVal eValue As String)
        Dim iParametroSQL As New ParametroSQL

        Try

            iIndiceParametro += 1

            With iParametroSQL
                .valor = FuncionComun.sqlMD5(eValue)
                .nombre = iColumnas.Item(iValues.Count + 1) & iIndiceParametro
                .nombreCampo = iColumnas.Item(iValues.Count + 1).ToString
            End With
            iParametrosSQL.Add(iParametroSQL)

            iValues.Add(IIf((iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14"), "@", "?") & iColumnas.Item(iValues.Count + 1) & iIndiceParametro)

        Catch exception As Exception
        End Try

    End Sub

    Public Sub agregarSet(ByVal eSet As String, Optional eSinGuardarParametros As Boolean = False)
        Dim iDerecha, iIzquierda As String
        Dim iParametroSQL As New ParametroSQL
        Dim iParametroSQLStoreProcedure As New ParametrosSQLStoredProcedureVO

        Try

            iIndiceParametro += 1

            If Not eSinGuardarParametros AndAlso eSet <> Nothing AndAlso eSet.IndexOf("=") > 0 Then
                iDerecha = Trim(Right(eSet, eSet.Length - eSet.IndexOf("=") - 1))
                iIzquierda = Trim(Left(eSet, eSet.IndexOf("=")))

                With iParametroSQL
                    .valor = iDerecha
                    .nombre = iIzquierda & iIndiceParametro
                    .nombreCampo = iIzquierda
                End With
                iParametrosSQL.Add(iParametroSQL)

                iParametroSQLStoreProcedure = New ParametrosSQLStoredProcedureVO
                With iParametroSQLStoreProcedure
                    .nombreParametro = "up" & iIzquierda.Replace(".", "")
                    .valor = iDerecha
                End With
                iParametrosSQLStoredProcedure.Add(iParametroSQLStoreProcedure)

                iSet.Add(iIzquierda & "=" & IIf((iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14"), "@", "?") & iIzquierda & iIndiceParametro)
            ElseIf eSet <> Nothing Then
                iSet.Add(eSet)
            End If

        Catch exception As Exception
        End Try

    End Sub

    Public Sub agregarSetEncriptado(ByVal eSet As String, Optional eSinGuardarParametros As Boolean = False)
        Dim iDerecha, iIzquierda As String
        Dim iParametroSQL As New ParametroSQL

        Try

            iIndiceParametro += 1

            If Not eSinGuardarParametros AndAlso eSet <> Nothing AndAlso eSet.IndexOf("=") > 0 Then
                iDerecha = Right(eSet, eSet.Length - eSet.IndexOf("=") - 1)
                iIzquierda = Trim(Left(eSet, eSet.IndexOf("=")))

                With iParametroSQL
                    .valor = FuncionComun.sqlMD5(iDerecha)
                    .nombre = iIzquierda & iIndiceParametro
                    .nombreCampo = iIzquierda
                End With
                iParametrosSQL.Add(iParametroSQL)

                iSet.Add(iIzquierda & "=" & IIf((iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14"), "@", "?") & iIzquierda & iIndiceParametro)

            ElseIf eSet <> Nothing Then
                iSet.Add(eSet)
            End If

        Catch exception As Exception
        End Try

    End Sub

    Public Sub agregarLimit(ByVal eLimit As String)
        iLimit.Add(eLimit)
    End Sub

    Public Sub agregarSelect(ByVal eSelect As String)
        iSelect.Add(eSelect)
    End Sub

    Public Sub agregarValuesAgrupados(ByVal eSelect As String)
        iValuesAgrupados.Add(eSelect)
    End Sub
    Public Sub agregarHaving(ByVal eHaving As String)
        iHaving.Add(eHaving)
    End Sub

    Public Function generarWhereConOr() As String
        Dim iConsulta As String

        'agregamos las condiciones del where
        If (iCondicionesWhereConOr.Count > 0) Then
            iConsulta = iterarColeccionWhereConOr(iCondicionesWhereConOr)
        End If

        vaciarColeccionesWhereOr()

        Return iConsulta

    End Function

    Public Function generarWhere() As String
        Dim iConsulta As String

        'agregamos las condiciones del where
        If (iCondicionesWhere.Count > 0) Then
            iConsulta = iterarColeccionWhere(iCondicionesWhere)
        End If

        vaciarColeccionesWhere()

        Return iConsulta

    End Function

    Public Function generarSelect() As String
        Dim iConsulta As String
        'agregamos las columnas
        iConsulta = "SELECT "
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'agregamos el limit
            If (iLimit.Count > 0) Then
                iConsulta = iConsulta & " TOP " & iLimit.Item(1) & " "
            End If
        End If

        iConsulta &= iterarColeccion(iColumnas, EnumColecciones.COMUMNASESPECIALES, ",")

        'agregamos las tablas
        If iTablaPrincipal.Length > 0 Then
            iConsulta = iConsulta & " FROM " & iTablaPrincipal
            'agrego el bloqueo sobre la tabla principal
            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") AndAlso bloquearTabla Then
                iConsulta = iConsulta & " WITH (UPDLOCK) "
            End If
            'AGREGARMOS JOINS 
            iConsulta &= iterarColeccion(iCondicionesJoin, EnumColecciones.JOIN, "")
            If iTablas.Count > 0 Then
                iConsulta = iConsulta & ", " & iterarColeccion(iTablas, EnumColecciones.TABLAS, ",")
            End If
        Else
            iConsulta = iConsulta & " FROM " & iterarColeccion(iTablas, EnumColecciones.TABLASESPECIALES, ",")
            'AGREGARMOS JOINS 
            iConsulta &= iterarColeccion(iCondicionesJoin, EnumColecciones.JOIN, "")
        End If

        'agregamos las condiciones del where
        If (iCondicionesWhere.Count > 0) Then
            iConsulta = iConsulta & " WHERE " & iterarColeccionWhere(iCondicionesWhere)
        End If

        'agregamos las columnas del GROUP BY
        If (iGroupBy.Count > 0) Then
            iConsulta = iConsulta & " GROUP BY " & iterarColeccion(iGroupBy, EnumColecciones.GROUPBY, ",")
        End If

        'agregamos el having
        If (iHaving.Count > 0) Then
            iConsulta = iConsulta & " HAVING " & iterarColeccionWhere(iHaving)
        End If

        'agregamos las columnas del order by
        If (iOrden.Count > 0) Then
            iConsulta = iConsulta & " ORDER BY " & iterarColeccion(iOrden, EnumColecciones.ORDER, ",")
        End If

        If iBaseDeDatos = "MYSQL" Then
            'agregamos el limit
            If (iLimit.Count > 0) Then
                iConsulta = iConsulta & " LIMIT " & iLimit.Item(1)
            End If
        End If

        If iBaseDeDatos = "MYSQL" AndAlso iBloquearTabla Then
            'agregamos FOR UPDATE
            iConsulta = iConsulta & " FOR UPDATE "
        End If

        vaciarColecciones()

        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            iConsulta = Regex.Replace(iConsulta, "ifnull\(", "isnull(", RegexOptions.IgnoreCase)
            iConsulta = Regex.Replace(iConsulta, "length\(", "len(", RegexOptions.IgnoreCase)
            iConsulta = Regex.Replace(iConsulta, "now\(\)", "getdate()", RegexOptions.IgnoreCase)

            'iConsulta = iConsulta.Replace("ifnull(", "isnull(")
            'iConsulta = iConsulta.Replace("length(", "len(")
            'iConsulta = iConsulta.Replace("now()", "getdate()")
        Else

            iConsulta = Regex.Replace(iConsulta, "isnull\(", "ifnull(", RegexOptions.IgnoreCase)
            iConsulta = Regex.Replace(iConsulta, "len\(", "length(", RegexOptions.IgnoreCase)
            iConsulta = Regex.Replace(iConsulta, "getdate\(\)", "now()", RegexOptions.IgnoreCase)
        End If

        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
            Case "URUGUAY"
            Case "PARAGUAY"
                iConsulta = iConsulta.Replace("$", "Gs")
            Case "COLOMBIA"
        End Select

        Return iConsulta

    End Function

    Public Function generarInsert() As String
        Dim iConsulta As String

        'agregamos la tabla
        iConsulta = "INSERT INTO " & iterarColeccion(iTablas, EnumColecciones.TABLAS, ",")

        'agregamos las columnas
        iConsulta = iConsulta & " (" & iterarColeccion(iColumnas, EnumColecciones.COMUMNAS, ",") & ")"

        'agregamos los values
        If iValues.Count > 0 Then iConsulta = iConsulta & " VALUES " & " (" & iterarColeccion(iValues, EnumColecciones.VALUES, ",") & ")"

        'agregamos los values agrupados
        If iValuesAgrupados.Count > 0 Then iConsulta = iConsulta & " VALUES " & iterarColeccion(iValuesAgrupados, EnumColecciones.VALUESAGRUPADOS, ",")

        vaciarColecciones()
        'Finalmente devolvemos la consulta
        Return iConsulta

    End Function

    Public Function generarValues() As String
        Dim iConsulta As String
        'agregamos los values
        iConsulta = "(" & iterarColeccion(iValues, EnumColecciones.VALUES, ",") & ")"

        vaciarColeccionValues()
        'Finalmente devolvemos la consulta
        Return iConsulta

    End Function

    Public Function generarInsertConSelect() As String
        Dim iConsulta As String

        'agregamos la tabla
        iConsulta = "INSERT INTO " & iterarColeccion(iTablas, EnumColecciones.TABLAS, ",")

        'agregamos las columnas
        iConsulta = iConsulta & " (" & iterarColeccion(iColumnas, EnumColecciones.COMUMNAS, ",") & ")"

        'agregamos los values
        iConsulta = iConsulta & " " & iterarColeccion(iValues, EnumColecciones.VALUES, ",")

        vaciarColecciones()
        'Finalmente devolvemos la consulta
        Return iConsulta

    End Function

    Public Function generarUpdate() As String
        Dim iConsulta As String

        'agregamos las tablas
        If iTablaPrincipal.Length > 0 Then
            iConsulta = " UPDATE " & iTablaPrincipal
            'AGREGARMOS JOINS 
            iConsulta &= iterarColeccion(iCondicionesJoin, EnumColecciones.JOIN, "")
            If iTablas.Count > 0 Then
                iConsulta = iConsulta & ", " & iterarColeccion(iTablas, EnumColecciones.TABLAS, ",")
            End If
        Else
            iConsulta = " UPDATE " & iterarColeccion(iTablas, EnumColecciones.TABLAS, ",")
            'AGREGARMOS JOINS 
            iConsulta &= iterarColeccion(iCondicionesJoin, EnumColecciones.JOIN, "")
        End If

        'agregamos el set
        iConsulta = iConsulta & " SET " & iterarColeccion(iSet, EnumColecciones.SETS, ",")

        'agregamos la condición
        If (iCondicionesWhere.Count > 0) Then
            iConsulta = iConsulta & " WHERE " & iterarColeccionWhere(iCondicionesWhere)
        End If

        'Finalmente devolvemos la consulta
        vaciarColecciones()

        Return iConsulta

    End Function

    Public Function generarUnion() As String
        Dim iConsulta As String
        Dim i As Integer

        iConsulta = ""

        'agregamos la tabla
        If iSelect.Count > 1 Then
            For i = 1 To iSelect.Count
                If i = iSelect.Count Then
                    iConsulta = iConsulta & "(" & iSelect.Item(i) & ")"
                Else
                    iConsulta = iConsulta & "(" & iSelect.Item(i) & ")" & " union "
                End If
            Next i
        ElseIf iSelect.Count = 1 Then
            iConsulta = iSelect.Item(1)
        End If

        'agregamos las columnas del order by
        If (iOrden.Count > 0) Then
            iConsulta = iConsulta & " ORDER BY " & iterarColeccion(iOrden, EnumColecciones.ORDER, ",")
        End If

        vaciarColecciones()
        While iSelect.Count > 0
            iSelect.Remove(1)
        End While
        'Finalmente devolvemos la consulta
        Return iConsulta

    End Function

    Public Sub agregarCondicionWhereConOr(ByVal eCondicionWhereConOr As String, Optional eSinGuardarParametros As Boolean = False)
        Dim iDerecha, iIzquierda As String
        Dim iParametroSQL As New ParametroSQL
        Dim iParametroSQLStoredProcedure As New ParametrosSQLStoredProcedureVO
        Dim iCaracterEncontrado As String

        Try

            iIndiceParametro += 1

            If eCondicionWhereConOr.ToLower.IndexOf(" and ") > 0 Then
                eSinGuardarParametros = True
            ElseIf eCondicionWhereConOr.ToLower.IndexOf(" or ") > 0 Then
                eSinGuardarParametros = True
            ElseIf eCondicionWhereConOr.IndexOf(">=") > 0 Then
                iCaracterEncontrado = ">="
            ElseIf eCondicionWhereConOr.IndexOf("<=") > 0 Then
                iCaracterEncontrado = "<="
            ElseIf eCondicionWhereConOr.IndexOf("<>") > 0 Then
                iCaracterEncontrado = "<>"
            ElseIf eCondicionWhereConOr.IndexOf(">") > 0 Then
                iCaracterEncontrado = ">"
            ElseIf eCondicionWhereConOr.IndexOf("<") > 0 Then
                iCaracterEncontrado = "<"
            ElseIf eCondicionWhereConOr.IndexOf("=") > 0 Then
                iCaracterEncontrado = "="
            End If

            If Not eSinGuardarParametros AndAlso eCondicionWhereConOr <> Nothing AndAlso Len(iCaracterEncontrado) > 0 Then
                iDerecha = Trim(Right(eCondicionWhereConOr, eCondicionWhereConOr.Length - eCondicionWhereConOr.IndexOf(iCaracterEncontrado) - Len(iCaracterEncontrado)))
                If InStr(iDerecha, ".") <= 0 OrElse InStr(iDerecha, "'") > 0 Then
                    iIzquierda = Trim(Left(eCondicionWhereConOr, eCondicionWhereConOr.IndexOf(iCaracterEncontrado)))
                    With iParametroSQL
                        .valor = iDerecha
                        .nombre = "condicion" & iIndiceParametro
                        .nombreCampo = iIzquierda
                    End With
                    iParametrosSQL.Add(iParametroSQL)

                    iParametroSQLStoredProcedure = New ParametrosSQLStoredProcedureVO
                    With iParametroSQLStoredProcedure
                        .nombreParametro = iIzquierda.Replace(".", "")
                        .valor = iDerecha
                    End With
                    iParametrosSQLStoredProcedure.Add(iParametroSQLStoredProcedure)

                    iCondicionesWhereConOr.Add(iIzquierda & iCaracterEncontrado & IIf((iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14"), "@", "?") & "condicion" & iIndiceParametro)
                Else
                    iCondicionesWhereConOr.Add(eCondicionWhereConOr)
                End If

            ElseIf Not eSinGuardarParametros AndAlso eCondicionWhereConOr.IndexOf("like") > 0 Then

                iIzquierda = Trim(Left(eCondicionWhereConOr, eCondicionWhereConOr.IndexOf("like")))
                iDerecha = Trim(Right(eCondicionWhereConOr, eCondicionWhereConOr.Length - eCondicionWhereConOr.IndexOf("'")))
                With iParametroSQL
                    .valor = iDerecha
                    .nombre = "condicion" & iIndiceParametro
                    .nombreCampo = iIzquierda
                End With
                iParametrosSQL.Add(iParametroSQL)

                iCondicionesWhereConOr.Add(iIzquierda & " like " & IIf((iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14"), "@", "?") & "condicion" & iIndiceParametro)

            ElseIf eCondicionWhereConOr <> Nothing Then
                iCondicionesWhereConOr.Add(eCondicionWhereConOr)
            End If

        Catch exception As Exception
        End Try
    End Sub

    Public Sub agregarCondicionWhere(ByVal eCondicionWhere As String, Optional eSinGuardarParametros As Boolean = False)
        Dim iDerecha, iIzquierda As String
        Dim iParametroSQL As New ParametroSQL
        Dim iParametroSQLStoredProcedure As New ParametrosSQLStoredProcedureVO
        Dim iCaracterEncontrado As String

        Try

            iIndiceParametro += 1

            If eCondicionWhere.ToLower.IndexOf(" and ") > 0 Then
                eSinGuardarParametros = True
            ElseIf eCondicionWhere.ToLower.IndexOf(" or ") > 0 Then
                eSinGuardarParametros = True
            ElseIf eCondicionWhere.IndexOf(">=") > 0 Then
                iCaracterEncontrado = ">="
            ElseIf eCondicionWhere.IndexOf("<=") > 0 Then
                iCaracterEncontrado = "<="
            ElseIf eCondicionWhere.IndexOf("<>") > 0 Then
                iCaracterEncontrado = "<>"
            ElseIf eCondicionWhere.IndexOf(">") > 0 Then
                iCaracterEncontrado = ">"
            ElseIf eCondicionWhere.IndexOf("<") > 0 Then
                iCaracterEncontrado = "<"
            ElseIf eCondicionWhere.IndexOf("=") > 0 Then
                iCaracterEncontrado = "="
            End If

            If Not eSinGuardarParametros AndAlso eCondicionWhere <> Nothing AndAlso Len(iCaracterEncontrado) > 0 Then
                iDerecha = Trim(Right(eCondicionWhere, eCondicionWhere.Length - eCondicionWhere.IndexOf(iCaracterEncontrado) - Len(iCaracterEncontrado)))
                If InStr(iDerecha, ".") <= 0 OrElse InStr(iDerecha, "'") > 0 Then
                    iIzquierda = Trim(Left(eCondicionWhere, eCondicionWhere.IndexOf(iCaracterEncontrado)))
                    With iParametroSQL
                        .valor = iDerecha
                        .nombre = "condicion" & iIndiceParametro
                        .nombreCampo = iIzquierda
                    End With
                    iParametrosSQL.Add(iParametroSQL)

                    iParametroSQLStoredProcedure = New ParametrosSQLStoredProcedureVO
                    With iParametroSQLStoredProcedure
                        .nombreParametro = iIzquierda.Replace(".", "")
                        .valor = iDerecha
                    End With
                    iParametrosSQLStoredProcedure.Add(iParametroSQLStoredProcedure)
                    iCondicionesWhere.Add(iIzquierda & iCaracterEncontrado & IIf((iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14"), "@", "?") & "condicion" & iIndiceParametro)
                Else
                    iCondicionesWhere.Add(eCondicionWhere)
                End If

            ElseIf Not eSinGuardarParametros AndAlso eCondicionWhere.IndexOf("like") > 0 Then
                iIzquierda = Trim(Left(eCondicionWhere, eCondicionWhere.IndexOf("like")))
                iDerecha = Trim(Right(eCondicionWhere, eCondicionWhere.Length - eCondicionWhere.IndexOf("'")))
                With iParametroSQL
                    .valor = iDerecha
                    .nombre = "condicion" & iIndiceParametro
                    .nombreCampo = iIzquierda
                End With
                iParametrosSQL.Add(iParametroSQL)

                iCondicionesWhere.Add(iIzquierda & " like " & IIf((iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14"), "@", "?") & "condicion" & iIndiceParametro)

            ElseIf eCondicionWhere <> Nothing Then
                iCondicionesWhere.Add(eCondicionWhere)
            End If

        Catch exception As Exception
        End Try

    End Sub

    Public Sub agregarCondicionWhereEncriptado(ByVal eCondicionWhere As String, Optional eSinGuardarParametros As Boolean = False)
        Dim iDerecha, iIzquierda As String
        Dim iParametroSQL As New ParametroSQL
        Dim iCaracterEncontrado As String

        Try
            iIndiceParametro += 1

            If eCondicionWhere.ToLower.IndexOf(" and ") > 0 Then
                eSinGuardarParametros = True
            ElseIf eCondicionWhere.ToLower.IndexOf(" or ") > 0 Then
                eSinGuardarParametros = True
            ElseIf eCondicionWhere.IndexOf(">=") > 0 Then
                iCaracterEncontrado = ">="
            ElseIf eCondicionWhere.IndexOf("<=") > 0 Then
                iCaracterEncontrado = "<="
            ElseIf eCondicionWhere.IndexOf("<>") > 0 Then
                iCaracterEncontrado = "<>"
            ElseIf eCondicionWhere.IndexOf(">") > 0 Then
                iCaracterEncontrado = ">"
            ElseIf eCondicionWhere.IndexOf("<") > 0 Then
                iCaracterEncontrado = "<"
            ElseIf eCondicionWhere.IndexOf("=") > 0 Then
                iCaracterEncontrado = "="
            End If

            If Not eSinGuardarParametros AndAlso eCondicionWhere <> Nothing AndAlso Len(iCaracterEncontrado) > 0 Then
                iDerecha = Trim(Right(eCondicionWhere, eCondicionWhere.Length - eCondicionWhere.IndexOf(iCaracterEncontrado) - Len(iCaracterEncontrado)))
                If InStr(iDerecha, ".") <= 0 OrElse InStr(iDerecha, "'") > 0 Then
                    iIzquierda = Trim(Left(eCondicionWhere, eCondicionWhere.IndexOf(iCaracterEncontrado)))
                    With iParametroSQL
                        .valor = FuncionComun.sqlMD5(iDerecha)
                        .nombre = "condicion" & iIndiceParametro
                        .nombreCampo = iIzquierda
                    End With
                    iParametrosSQL.Add(iParametroSQL)

                    iCondicionesWhere.Add(iIzquierda & iCaracterEncontrado & IIf((iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14"), "@", "?") & "condicion" & iIndiceParametro)
                Else
                    iCondicionesWhere.Add(eCondicionWhere)
                End If

            ElseIf Not eSinGuardarParametros AndAlso eCondicionWhere.IndexOf("like") > 0 Then
                iIzquierda = Trim(Left(eCondicionWhere, eCondicionWhere.IndexOf("like")))
                iDerecha = Trim(Right(eCondicionWhere, eCondicionWhere.Length - eCondicionWhere.IndexOf("'")))
                With iParametroSQL
                    .valor = FuncionComun.sqlMD5(iDerecha)
                    .nombre = "condicion" & iIndiceParametro
                    .nombreCampo = iIzquierda
                End With
                iParametrosSQL.Add(iParametroSQL)

                iCondicionesWhere.Add(iIzquierda & " like " & IIf((iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14"), "@", "?") & "condicion" & iIndiceParametro)

            ElseIf eCondicionWhere <> Nothing Then
                iCondicionesWhere.Add(eCondicionWhere)
            End If

        Catch exception As Exception
        End Try

    End Sub

    Public Function generarUnion(ByVal eUnionAll As Boolean) As String
        Dim iConsulta As String
        Dim i As Integer

        iConsulta = ""

        'agregamos la tabla
        For i = 1 To iSelect.Count
            If i = iSelect.Count Then
                iConsulta = iConsulta & "(" & iSelect.Item(i) & ")"
            Else
                iConsulta = iConsulta & "(" & iSelect.Item(i) & ")" & " union all "
            End If
        Next i

        'agregamos las columnas del order by
        If (iOrden.Count > 0) Then
            iConsulta = iConsulta & " ORDER BY " & iterarColeccion(iOrden, EnumColecciones.ORDER, ",")
        End If

        vaciarColecciones()
        While iSelect.Count > 0
            iSelect.Remove(1)
        End While
        'Finalmente devolvemos la consulta
        Return iConsulta

    End Function

    Public Function generarDelete() As String
        Dim iConsulta As String

        'agregamos la tabla
        iConsulta = "DELETE FROM " & iterarColeccion(iTablas, EnumColecciones.TABLAS, ",")

        'agregamos la condición
        If (iCondicionesWhere.Count > 0) Then
            iConsulta = iConsulta & " WHERE " & iterarColeccionWhere(iCondicionesWhere)
        End If

        vaciarColecciones()
        'Finalmente devolvemos la consulta
        Return iConsulta

    End Function

    Private Function iterarColeccion(ByVal eColeccion As Collection, eConcepto As EnumColecciones, ByVal eSeparador As String) As String
        Dim iCadena As String = ""
        Dim i As Integer

        'agregamos las columnas
        For i = 1 To eColeccion.Count

            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") AndAlso eConcepto = EnumColecciones.COMUMNASESPECIALES AndAlso iGroupBy.Count > 0 Then
                Dim iColumna As String = eColeccion.Item(i)
                If Not ((iColumna.Length > 3 AndAlso iColumna.ToLower().Substring(0, 4) = "max(") OrElse iColumna.ToLower.Contains("min(") OrElse (Not iColumna.ToLower.Contains("case ") AndAlso iColumna.ToLower.Contains("max(")) OrElse iColumna.ToLower.Contains("sum(") OrElse iColumna.ToLower.Contains("count(") OrElse (Not iColumna.ToLower.Contains("case ") AndAlso iColumna.ToLower.Contains("null ")) OrElse iColumna.ToLower.Contains("group_concat_d")) Then
                    If iColumna.ToLower.Contains(" as ") Then
                        iColumna = "min(" & Left(iColumna, iColumna.ToLower.LastIndexOf(" as ")) & ") " & Right(iColumna, iColumna.Length - iColumna.ToLower.LastIndexOf(" as "))
                    Else
                        If iColumna.ToLower.Contains(".") Then
                            iColumna = "min(" & iColumna & ") as " & Right(iColumna, iColumna.Length - iColumna.ToLower.LastIndexOf(".") - 1)
                        Else
                            iColumna = "min(" & iColumna & ") as " & iColumna
                        End If
                    End If
                    If i = eColeccion.Count Then
                        iCadena = iCadena + iColumna
                    Else
                        iCadena = iCadena + iColumna + eSeparador
                    End If
                Else
                    If i = eColeccion.Count Then
                        iCadena = iCadena + eColeccion.Item(i)
                    Else
                        iCadena = iCadena + eColeccion.Item(i) + eSeparador
                    End If
                End If
            Else
                If i = eColeccion.Count Then
                    iCadena = iCadena + eColeccion.Item(i)
                Else
                    iCadena = iCadena + eColeccion.Item(i) + eSeparador
                End If
            End If

            If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") AndAlso eConcepto = EnumColecciones.TABLASESPECIALES AndAlso i = 1 AndAlso iBloquearTabla AndAlso iTablaPrincipal.Length = 0 Then
                If eColeccion.Count > 1 Then
                    iCadena = Left(iCadena, iCadena.Length - 1) + "  WITH (UPDLOCK) , "
                Else
                    iCadena = iCadena + "  WITH (UPDLOCK) "
                End If
            End If

        Next

        'Reemplazo las palabras reservadas para SQL SERVER
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            iCadena = Regex.Replace(iCadena, "as plan ", "as [plan] ", RegexOptions.IgnoreCase)
            iCadena = Regex.Replace(iCadena, "as plan\)", "as [plan])", RegexOptions.IgnoreCase)
            iCadena = Regex.Replace(iCadena, "as plan,", "as [plan],", RegexOptions.IgnoreCase)
        End If

        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
            Case "URUGUAY"
            Case "PARAGUAY"
                iCadena = iCadena.Replace("$", "Gs")
            Case "COLOMBIA"
        End Select

        Return iCadena

    End Function

    Private Function iterarColeccionWhere(ByVal eColeccion As Collection) As String
        Dim iCadena As String = ""
        Dim iNewItem As String = ""

        'agregamos las columnas
        For Each iNewItem In eColeccion
            iCadena = iCadena & "(" & iNewItem
            'si no es el ultimo item agregamos los parentesis
            If (iNewItem <> CType(eColeccion.Item(eColeccion.Count), String)) Then
                iCadena = iCadena + ") AND "
            Else
                iCadena = iCadena & ")"
            End If
        Next

        Return iCadena
    End Function

    Private Function iterarColeccionWhereConOr(ByVal eColeccion As Collection) As String
        Dim iCadena As String = ""
        Dim iNewItem As String = ""

        'agregamos las columnas
        For Each iNewItem In eColeccion
            iCadena = iCadena & "(" & iNewItem
            'si no es el ultimo item agregamos los parentesis
            If (iNewItem <> CType(eColeccion.Item(eColeccion.Count), String)) Then
                iCadena = iCadena + ") Or "
            Else
                iCadena = iCadena & ")"
            End If
        Next

        Return iCadena
    End Function

    Public Sub destructor()
        'destruimos las listas
        iColumnas = Nothing
        iTablas = Nothing
        iCondicionesWhere = Nothing
        iOrden = Nothing
        iLimit = Nothing
        iSelect = Nothing
        iGroupBy = Nothing
        iHaving = Nothing
        iParametrosSQL = Nothing
    End Sub

    Private Sub vaciarColecciones()

        While iColumnas.Count > 0
            iColumnas.Remove(1)
        End While

        While iLimit.Count > 0
            iLimit.Remove(1)
        End While

        While iCondicionesJoin.Count > 0
            iCondicionesJoin.Remove(1)
        End While

        While iCondicionesWhere.Count > 0
            iCondicionesWhere.Remove(1)
        End While

        While iGroupBy.Count > 0
            iGroupBy.Remove(1)
        End While

        While iOrden.Count > 0
            iOrden.Remove(1)
        End While

        While iSet.Count > 0
            iSet.Remove(1)
        End While

        While iTablas.Count > 0
            iTablas.Remove(1)
        End While

        While iValues.Count > 0
            iValues.Remove(1)
        End While

        While iValuesAgrupados.Count > 0
            iValuesAgrupados.Remove(1)
        End While

        While iHaving.Count > 0
            iHaving.Remove(1)
        End While

        iTablaPrincipal = ""

    End Sub

    Private Sub vaciarColeccionesWhereOr()

        While iCondicionesWhereConOr.Count > 0
            iCondicionesWhereConOr.Remove(1)
        End While

    End Sub

    Private Sub vaciarColeccionValues()

        While iValues.Count > 0
            iValues.Remove(1)
        End While

    End Sub

    Private Sub vaciarColeccionesWhere()

        While iCondicionesWhere.Count > 0
            iCondicionesWhere.Remove(1)
        End While

    End Sub

    Public Function generarSelectSecuencia(ByVal eSecuencia As String) As String

        'Esta funcion retorna un string para obtener el proximo id a otorgar
        Return "select nextval (" & "'""" & eSecuencia & """'" & ") as id"

    End Function

#End Region

End Class
