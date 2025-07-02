Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.IO
Imports System.Configuration

Public Class Veraz : Inherits Deudor

#Region "Variables"
    Private iFinanciera As String
    Private iImporte As Double
    Private iConexion As accesoDatos

#End Region

#Region "Atributos"
    Public Property financiera() As String
        Get
            Return iFinanciera
        End Get
        Set(ByVal Value As String)
            iFinanciera = Value
        End Set
    End Property
    Public Property importe() As Double
        Get
            Return iImporte
        End Get
        Set(ByVal Value As Double)
            iImporte = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Overrides Function isVeraz() As Boolean
        Return True
    End Function

    Public Overrides Function isCamara() As Boolean
        Return False
    End Function

    Public Overrides Function obtenerDeudor(Optional ByVal eCodigoFinancieraNoBuscar As String = Nothing) As Deudor
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("v.id")
            iGeneradorSql.agregarColumna("v.documento")
            iGeneradorSql.agregarColumna("v.nombre")
            iGeneradorSql.agregarColumna("ev.descripcion as financiera")
            iGeneradorSql.agregarColumna("v.importe")
            iGeneradorSql.agregarTabla("veraz v")
            iGeneradorSql.agregarTabla("entidadVeraz ev")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("v.id=" & id)
            If documento <> Nothing Then iGeneradorSql.agregarCondicionWhere("v.documento=" & documento)
            If nombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("v.nombre like '" & nombre & "%'")
            iGeneradorSql.agregarCondicionWhere("v.codigo=ev.codigoVeraz")

            iGeneradorSql.agregarLimit("1")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                documento = iDataReader.Item("documento").ToString
                nombre = iDataReader.Item("nombre").ToString
                iFinanciera = iDataReader.Item("financiera").ToString
                iImporte = iDataReader.Item("importe").ToString
                Return Me
            Else
                Throw New DeudorNoEncontradoException
            End If
        Catch DeudorNoEncontradoException As DeudorNoEncontradoException
            Throw DeudorNoEncontradoException
        Catch excepcion As Exception
            Throw New DeudorNoEncontradoException(excepcion)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Overrides Function obtenerDeudores() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("v.id")
            iGeneradorSql.agregarColumna("v.documento")
            iGeneradorSql.agregarColumna("v.nombre")
            iGeneradorSql.agregarColumna("ev.descripcion as financiera")
            iGeneradorSql.agregarColumna("v.importe")
            iGeneradorSql.agregarTabla("veraz v")
            iGeneradorSql.agregarTabla("entidadVeraz ev")

            If documento <> Nothing Then iGeneradorSql.agregarCondicionWhere("v.documento=" & documento)
            If nombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("v.nombre like '" & nombre & "%'")
            iGeneradorSql.agregarCondicionWhere("v.codigo=ev.codigoVeraz")

            iGeneradorSql.agregarLimit("1000")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Veraz")

        Catch excepcion As ErrorConexionException
            Throw New DeudorNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Overrides Function obtenerDeudorTarjeta(ByVal eDocumento As Long) As Boolean
        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("veraz")
            iGeneradorSql.agregarCondicionWhere("documento=" & eDocumento)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Return True
            Else
                Return False
            End If

        Catch excepcion As ErrorConexionException
            Throw New DeudorNoEncontradoException(excepcion)
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

    Public Function insertarVeraz(ByVal ePathArchivoVerazZipeado As String, ByVal eEliminarTodo As Boolean) As String
        Dim iPathArchivoVerazGenerado As String = ConfigurationManager.AppSettings("archivosGenerados") & "Veraz.txt"
        Dim iPathArchivoVerazRecibido As String = ConfigurationManager.AppSettings("archivosRecibidos") & "Veraz.txt"
        Dim iEntidadesVeraz As String
        Dim iContadorVeraz As Long
        Dim iResultado As String
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader
        Dim iContadorInsertados As Long
        Dim iFechaInicio As Date
        Dim iFechaFin As Date

        Try
            iConexion = obtenerConexion()

            'Deszipear el archivo
            FuncionComun.UnZip(ePathArchivoVerazZipeado)

            If eEliminarTodo Then
                'Elimino la info con drop
                iConexion.ejecutar("drop table veraz")
                iConexion.ejecutar("CREATE TABLE veraz ( " &
                                   " Id int(11) NOT NULL auto_increment, " &
                                   " Documento decimal(11,0) NOT NULL default '0'," &
                                   " Nombre varchar(25) default NULL," &
                                   " Codigo varchar(6)," &
                                   " Importe decimal(8,2) default NULL," &
                                   " PRIMARY KEY  (Id)," &
                                   " KEY indexDocumento(documento)," &
                                   " KEY indexNombre(nombre)," &
                                   " KEY indexCodigo(codigo)," &
                                   " ) ENGINE=InnoDB;")
            Else
                'Elimino la info con delete
                iGeneradorSql.agregarColumna("codigoVeraz")
                iGeneradorSql.agregarTabla("entidadVeraz")
                iGeneradorSql.agregarCondicionWhere("eliminarInformacion=" & FuncionComun.booleanByte(True))
                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
                While iDataReader.Read
                    iEntidadesVeraz = iEntidadesVeraz & "'" & iDataReader.Item("codigoVeraz") & "',"
                End While
                iDataReader.Close()

                iGeneradorSql.agregarTabla("Veraz")
                iGeneradorSql.agregarCondicionWhere("codigo in(" & Left(iEntidadesVeraz, iEntidadesVeraz.Length - 1) & ")")
                iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)
            End If

            'Inicio la insercion de las financieras
            iFechaInicio = Now

            'Armo el archivo para hacer el load infile
            iContadorVeraz = armarArchivoVeraz()

            'Ejecuto el load
            iConexion.ejecutarLoad("LOAD DATA INFILE '" & iPathArchivoVerazGenerado.Replace("\", "/") & "'" & _
                            " INTO TABLE veraz" & _
                            " FIELDS TERMINATED BY ';'" & _
                            " LINES TERMINATED BY '\r\n'" & _
                            " (Nombre,Documento,Importe,Codigo)")

            'Cuento los registros insertados
            iGeneradorSql.agregarColumna("count(id) as insertados")
            iGeneradorSql.agregarTabla("veraz")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                iContadorInsertados = CLng(iDataReader.Item("insertados").ToString)
            Else
                iContadorInsertados = 0
            End If
            iDataReader.Close()

            iFechaFin = Now

            iResultado = "VERAZ" & vbNewLine
            iResultado = iResultado & "-----------" & vbNewLine
            iResultado = iResultado & "INICIO DEL PROCESO:" & iFechaInicio & vbNewLine
            iResultado = iResultado & "FIN DEL PROCESO:" & iFechaFin & vbNewLine
            iResultado = iResultado & "TOTAL DE REGISTROS PROCESADOS:" & iContadorVeraz & vbNewLine
            iResultado = iResultado & "TOTAL DE REGISTROS INSERTADOS:" & iContadorInsertados & vbNewLine
            iResultado = iResultado & "TOTAL DE REGISTROS RECHAZADOS:" & iContadorVeraz - iContadorInsertados & vbNewLine & vbNewLine

            'Informo las entidades veraz que no estan en la tabla 
            iEntidadesVeraz = Nothing
            iGeneradorSql.agregarColumna("codigoVeraz")
            iGeneradorSql.agregarTabla("entidadVeraz")
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            While iDataReader.Read
                iEntidadesVeraz = iEntidadesVeraz & "'" & iDataReader.Item("codigoVeraz") & "',"
            End While
            iDataReader.Close()

            iGeneradorSql.agregarColumna("distinct codigo")
            iGeneradorSql.agregarTabla("Veraz")
            iGeneradorSql.agregarCondicionWhere("codigo not in(" & Left(iEntidadesVeraz, iEntidadesVeraz.Length - 1) & ")")
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            iEntidadesVeraz = Nothing
            While iDataReader.Read
                iEntidadesVeraz = iEntidadesVeraz & "'" & iDataReader.Item("codigo") & "',"
            End While
            iDataReader.Close()

            If Len(iEntidadesVeraz) > 0 Then
                iResultado = iResultado & "SE INCORPORARON LAS SIGUIENTE ENTIDADES VERAZ:" & Left(iEntidadesVeraz, iEntidadesVeraz.Length - 1).Replace("'", "") & vbNewLine & vbNewLine
                iResultado = iResultado & "***NO OLVIDE INCORPORARLAS AL SISTEMA, DE LO CONTRARIO SE REGISTRARAN ERRORES DE INTEGRIDAD DE DATOS***" & vbNewLine
            Else
                iResultado = iResultado & "NO HUBO INCORPORACION DE ENTIDADES VERAZ EN ESTE PERIODO" & vbNewLine
            End If

            'Elimino todos los archivos utilizados
            If File.Exists(iPathArchivoVerazGenerado) Then File.Delete(iPathArchivoVerazGenerado)
            If File.Exists(iPathArchivoVerazRecibido) Then File.Delete(iPathArchivoVerazRecibido)
            If File.Exists(ePathArchivoVerazZipeado) Then File.Delete(ePathArchivoVerazZipeado)

            Return iResultado

        Catch exception As exception
            Throw New DeudorNoInsertadoException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql = Nothing
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
        End Try

    End Function

    Private Function armarArchivoVeraz() As Long
        Dim iArchivoEntrada As StreamReader
        Dim iArchivoSalida As StreamWriter
        Dim iPathArchivoVeraz As String = ConfigurationManager.AppSettings("archivosRecibidos") & "Veraz.txt"
        Dim iRegistroEntrada As String
        Dim iRegistroSalida As String
        Dim iContador As Long


        Try
            iArchivoEntrada = New StreamReader(iPathArchivoVeraz, Text.Encoding.ASCII)
            iArchivoSalida = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "Veraz.txt")

            iContador = 0

            'Registro de cabecera
            iRegistroEntrada = iArchivoEntrada.ReadLine

            While Not iArchivoEntrada.Peek = -1

                iRegistroEntrada = iArchivoEntrada.ReadLine
                'nombre
                iRegistroSalida = Trim(iRegistroEntrada.Substring(8, 72)) & ";"
                'documento
                iRegistroSalida = iRegistroSalida & Trim(iRegistroEntrada.Substring(81, 11)) & ";"
                'importe
                iRegistroSalida = iRegistroSalida & iRegistroEntrada.Substring(307, 11) & ";"
                'codigo
                iRegistroSalida = iRegistroSalida & Trim(iRegistroEntrada.Substring(262, 6))

                If iArchivoEntrada.Peek <> -1 Then iArchivoSalida.WriteLine(iRegistroSalida)

                iContador = iContador + 1

            End While

            Return iContador

        Catch exception As exception
            Throw New DeudorNoInsertadoException(exception)
        Finally
            If Not IsNothing(iArchivoEntrada) Then iArchivoEntrada.Close()
            If Not IsNothing(iArchivoSalida) Then iArchivoSalida.Close()
            iArchivoEntrada = Nothing
            iArchivoSalida = Nothing
        End Try
    End Function

    Public Overrides Function obtenerDeudoresFinanciera(ByVal eDocumento As Long) As String
        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("v.id")
            iGeneradorSql.agregarColumna("v.documento")
            iGeneradorSql.agregarColumna("v.nombre")
            iGeneradorSql.agregarColumna("ev.descripcion as financiera")
            iGeneradorSql.agregarColumna("v.importe")
            iGeneradorSql.agregarTabla("veraz v")
            iGeneradorSql.agregarTabla("entidadVeraz ev")
            iGeneradorSql.agregarCondicionWhere("v.documento=" & eDocumento)
            iGeneradorSql.agregarCondicionWhere("v.codigo=ev.codigoVeraz")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Return "Financiera: " & FuncionComun.vacioSiEsNulo(iDataReader.Item("financiera").ToString) & "Importe:" & FuncionComun.ceroSiEsNulo(iDataReader.Item("importe").ToString)
            Else
                Return ""
            End If

        Catch excepcion As Exception
            Throw New DeudorNoEncontradoException(excepcion)
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

#End Region

End Class
