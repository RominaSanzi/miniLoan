Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.IO
Imports System.Configuration

Public Class Camara : Inherits Deudor

#Region "constante financiera"
    Public Const FINANCIERA = "XXXXX"
#End Region

#Region "Variables"
    Private iFinancieras As Collection
    Private iMas As Boolean
    Private iConexion As accesoDatos
#End Region

#Region "Atributos"
    Public Property financieras() As Collection
        Get
            Return iFinancieras
        End Get
        Set(ByVal Value As Collection)
            iFinancieras = Value
        End Set
    End Property
    Public Property mas() As Boolean
        Get
            Return iMas
        End Get
        Set(ByVal Value As Boolean)
            iMas = Value
        End Set
    End Property

#End Region

#Region "Metodos"

    Public Overrides Function isVeraz() As Boolean
        Return False
    End Function

    Public Overrides Function isCamara() As Boolean
        Return True
    End Function

    Public Overrides Function obtenerDeudor(Optional ByVal eCodigoFinancieraNoBuscar As String = Nothing) As Deudor
        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("c.id")
            iGeneradorSql.agregarColumna("c.documento")
            iGeneradorSql.agregarColumna("c.nombre")
            iGeneradorSql.agregarColumna("f.descripcion as financiera")
            iGeneradorSql.agregarColumna("f1.descripcion as financiera1")
            iGeneradorSql.agregarColumna("f2.descripcion as financiera2")
            iGeneradorSql.agregarColumna("c.financiera as codigofinanciera")
            iGeneradorSql.agregarColumna("c.financiera1 as codigofinanciera1")
            iGeneradorSql.agregarColumna("c.financiera2 as codigofinanciera2")
            iGeneradorSql.agregarColumna("c.mas")
            iGeneradorSql.agregarTabla("financiera f ")
            iGeneradorSql.agregarTabla("camara c " & _
                                       "left join financiera f1 on (f1.codigo = c.financiera1) " & _
                                       "left join financiera f2 on (f2.codigo = c.financiera2)")
            iGeneradorSql.agregarCondicionWhere("f.codigo = c.financiera")

            If Not id = Nothing Then iGeneradorSql.agregarCondicionWhere("c.id=" & id)
            If Not documento = Nothing Then iGeneradorSql.agregarCondicionWhere("c.documento=" & documento)
            If Not nombre = Nothing Then iGeneradorSql.agregarCondicionWhere("c.nombre like '" & nombre & "%'")


            iGeneradorSql.agregarLimit("1")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then

                id = iDataReader.Item("id").ToString
                documento = iDataReader.Item("documento").ToString
                nombre = iDataReader.Item("nombre").ToString
                financieras = New Collection

                If iDataReader.Item("codigofinanciera").ToString <> eCodigoFinancieraNoBuscar Then financieras.Add(iDataReader.Item("financiera").ToString)
                If iDataReader.Item("codigofinanciera1").ToString <> eCodigoFinancieraNoBuscar AndAlso Not IsDBNull(iDataReader.Item("financiera1")) Then financieras.Add(iDataReader.Item("financiera1").ToString)
                If iDataReader.Item("codigofinanciera2").ToString <> eCodigoFinancieraNoBuscar AndAlso Not IsDBNull(iDataReader.Item("financiera2")) Then financieras.Add(iDataReader.Item("financiera2").ToString)
                If Not IsDBNull(iDataReader.Item("mas")) Then iMas = IIf(iDataReader.Item("mas").ToString = "+", True, False)
                If financieras.Count = 0 Then
                    Throw New DeudorNoEncontradoException
                End If
                Return Me
            Else
                Throw New DeudorNoEncontradoException
            End If

        Catch excepcion As Exception
            Throw excepcion
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
            iGeneradorSql.agregarColumna("c.id")
            iGeneradorSql.agregarColumna("c.documento")
            iGeneradorSql.agregarColumna("c.nombre")
            iGeneradorSql.agregarColumna("f.descripcion as financiera")
            iGeneradorSql.agregarColumna("f1.descripcion as financiera1")
            iGeneradorSql.agregarColumna("f2.descripcion as financiera2")
            iGeneradorSql.agregarColumna("c.mas")
            iGeneradorSql.agregarTabla("financiera f")
            iGeneradorSql.agregarTabla("camara c " & _
                                       "left join financiera f1 on (f1.codigo = c.financiera1) " & _
                                       "left join financiera f2 on (f2.codigo = c.financiera2)")
            iGeneradorSql.agregarCondicionWhere("f.codigo = c.financiera")
            If documento <> Nothing Then iGeneradorSql.agregarCondicionWhere("c.documento=" & documento)
            If nombre <> Nothing Then iGeneradorSql.agregarCondicionWhere("c.nombre like '" & nombre & "%'")

            iGeneradorSql.agregarLimit("1000")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Camara")

        Catch excepcion As Exception
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
            iGeneradorSql.agregarColumna("documento")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("financiera")
            iGeneradorSql.agregarColumna("financiera1")
            iGeneradorSql.agregarColumna("financiera2")
            iGeneradorSql.agregarColumna("mas")
            iGeneradorSql.agregarTabla("camara")
            iGeneradorSql.agregarCondicionWhere("documento=" & eDocumento)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Return True
            Else
                Return False
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
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Function insertarCamara(ByVal ePathArchivoCamaraZipeado As String) As String
        Dim iPathArchivoFinancierasGenerado As String = ConfigurationManager.AppSettings("archivosGenerados") & "Financ.seq"
        Dim iPathArchivoCamaraGenerado As String = ConfigurationManager.AppSettings("archivosGenerados") & "Morout.seq"
        Dim iPathArchivoFinancierasRecibido As String = ConfigurationManager.AppSettings("archivosRecibidos") & "Financ.seq"
        Dim iPathArchivoCamaraRecibido As String = ConfigurationManager.AppSettings("archivosRecibidos") & "Morout.seq"
        Dim iContadorFinancieras As Long
        Dim iContadorCamara As Long
        Dim iResultado As String
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader
        Dim iContadorInsertados As Long
        Dim iFechaInicio As Date
        Dim iFechaFin As Date

        Try
            iConexion = obtenerConexion()

            'Deszipear el archivo
            FuncionComun.UnZip(ePathArchivoCamaraZipeado)

            'Elimino la info que tenian las tablas
            'Elimino las tablas y las vuelvo a crear
            iConexion.ejecutar("drop table camara")
            iConexion.ejecutar("CREATE TABLE camara (" &
                               " Id int(11) NOT NULL auto_increment," &
                               " Documento decimal(10,0) NOT NULL default '0'," &
                               " Nombre varchar(25) default NULL," &
                               " Financiera char(3) default NULL," &
                               " Financiera1 char(3) default NULL," &
                               " Financiera2 char(3) default NULL," &
                               " Mas char(1) default NULL," &
                               " PRIMARY KEY  (Id)," &
                               " KEY indexFinanciera (Financiera)," &
                               " KEY indexFinanciera1 (Financiera1)," &
                               " KEY indexFinanciera2 (Financiera2)," &
                               " KEY indexcamaraNombre (Nombre)," &
                               " KEY indexcamaraDucumento (Documento)" &
                               " )  ENGINE=InnoDB;")

            iConexion.ejecutar("drop table financiera")
            iConexion.ejecutar("CREATE TABLE financiera (" &
                               " Descripcion varchar(50) default NULL," &
                               " Codigo char(3) NOT NULL default ''," &
                               " PRIMARY KEY(Codigo)" &
                               " ) ENGINE=InnoDB;")

            iConexion.ejecutar("ALTER TABLE Camara ADD CONSTRAINT CFinanciera " & _
                               " FOREIGN KEY Camara(Financiera)" & _
                               " REFERENCES Financiera(Codigo);")

            iConexion.ejecutar("ALTER TABLE Camara ADD CONSTRAINT CFinanciera1 " & _
                               " FOREIGN KEY Camara(Financiera1)" & _
                               " REFERENCES Financiera(Codigo);")

            iConexion.ejecutar("ALTER TABLE Camara ADD CONSTRAINT CFinanciera2 " & _
                               " FOREIGN KEY Camara(Financiera2)" & _
                               " REFERENCES Financiera(Codigo);")

            'Inicio la insercion de las financieras
            iFechaInicio = Now

            'Armo el archivo para hacer el load infile
            iContadorFinancieras = armarArchivoFinancieras()

            'Ejecuto el load
            iConexion.ejecutarLoad("LOAD DATA INFILE '" & iPathArchivoFinancierasGenerado.Replace("\", "/") & "'" & _
                            " INTO TABLE financiera" & _
                            " FIELDS TERMINATED BY ','" & _
                            " LINES TERMINATED BY '\r\n'" & _
                            " (Codigo,Descripcion)")

            'Cuento los registros insertados
            iGeneradorSql.agregarColumna("count(codigo) as insertados")
            iGeneradorSql.agregarTabla("financiera")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                iContadorInsertados = CLng(iDataReader.Item("insertados").ToString)
            Else
                iContadorInsertados = 0
            End If
            iDataReader.Close()

            iFechaFin = Now

            iResultado = "FINANCIERAS" & vbNewLine
            iResultado = iResultado & "-----------" & vbNewLine
            iResultado = iResultado & "INICIO DEL PROCESO:" & iFechaInicio & vbNewLine
            iResultado = iResultado & "FIN DEL PROCESO:" & iFechaFin & vbNewLine
            iResultado = iResultado & "TOTAL DE REGISTROS PROCESADOS:" & iContadorFinancieras & vbNewLine
            iResultado = iResultado & "TOTAL DE REGISTROS INSERTADOS:" & iContadorInsertados & vbNewLine
            iResultado = iResultado & "TOTAL DE REGISTROS RECHAZADOS:" & iContadorFinancieras - iContadorInsertados & vbNewLine & vbNewLine

            iResultado = iResultado & StrDup(50, "-") & vbNewLine & vbNewLine


            'Ahora inserto los de Camara
            iFechaInicio = Now

            iContadorCamara = armarArchivoCamara()

            'Ejecuto el load
            iConexion.ejecutarLoad("LOAD DATA INFILE '" & iPathArchivoCamaraGenerado.Replace("\", "/") & "'" & _
                               " INTO TABLE camara" & _
                               " FIELDS TERMINATED BY ','" & _
                               " LINES TERMINATED BY '\r\n'" & _
                               " (Financiera,Financiera1,Financiera2,Mas,Nombre,Documento)")

            'Cuento los registros insertados
            iGeneradorSql.agregarColumna("count(id) as insertados")
            iGeneradorSql.agregarTabla("camara")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                iContadorInsertados = CLng(iDataReader.Item("insertados").ToString)
            Else
                iContadorInsertados = 0
            End If
            iDataReader.Close()

            iFechaFin = Now

            iResultado = iResultado & "CAMARA" & vbNewLine
            iResultado = iResultado & "------" & vbNewLine
            iResultado = iResultado & "INICIO DEL PROCESO:" & iFechaInicio & vbNewLine
            iResultado = iResultado & "FIN DEL PROCESO:" & iFechaFin & vbNewLine
            iResultado = iResultado & "TOTAL DE REGISTROS PROCESADOS:" & iContadorCamara & vbNewLine
            iResultado = iResultado & "TOTAL DE REGISTROS INSERTADOS:" & iContadorInsertados & vbNewLine
            iResultado = iResultado & "TOTAL DE REGISTROS RECHAZADOS:" & iContadorCamara - iContadorInsertados & vbNewLine

            'Elimino todos los archivos utilizados
            If File.Exists(iPathArchivoFinancierasGenerado) Then File.Delete(iPathArchivoFinancierasGenerado)
            If File.Exists(iPathArchivoCamaraGenerado) Then File.Delete(iPathArchivoCamaraGenerado)
            If File.Exists(iPathArchivoFinancierasRecibido) Then File.Delete(iPathArchivoFinancierasRecibido)
            If File.Exists(iPathArchivoCamaraRecibido) Then File.Delete(iPathArchivoCamaraRecibido)
            If File.Exists(ePathArchivoCamaraZipeado) Then File.Delete(ePathArchivoCamaraZipeado)

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

    Private Function armarArchivoCamara() As Long
        Dim iArchivoEntrada As StreamReader
        Dim iArchivoSalida As StreamWriter
        Dim iPathArchivoCamara As String = ConfigurationManager.AppSettings("archivosRecibidos") & "Morout.seq"
        Dim iRegistroEntrada As String
        Dim iRegistroSalida As String
        Dim iContador As Long

        Try
            iArchivoEntrada = New StreamReader(iPathArchivoCamara, Text.Encoding.ASCII)
            iArchivoSalida = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "Morout.seq")

            iContador = 0

            While Not iArchivoEntrada.Peek = -1

                iRegistroEntrada = iArchivoEntrada.ReadLine
                'financiera 1
                iRegistroSalida = iRegistroEntrada.Substring(0, 2).Replace(" ", "").PadLeft(2, "0") & ","
                'financiera 2
                iRegistroSalida = iRegistroSalida & iRegistroEntrada.Substring(3, 2).Replace(" ", "").PadLeft(2, "0").Replace("00", "\N") & ","
                'financiera 3
                iRegistroSalida = iRegistroSalida & iRegistroEntrada.Substring(6, 2).Replace(" ", "").PadLeft(2, "0").Replace("00", "\N") & ","
                'financiera +
                iRegistroSalida = iRegistroSalida & iRegistroEntrada.Substring(9, 1) & ","
                'nombre(11 - 37)
                iRegistroSalida = Trim(iRegistroSalida & iRegistroEntrada.Substring(10, 27)) & ","
                'documento(38 - 45)
                iRegistroSalida = iRegistroSalida & iRegistroEntrada.Substring(37, 8).PadRight(8, "00000000")

                iArchivoSalida.WriteLine(iRegistroSalida)

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

    Private Function armarArchivoFinancieras() As Long
        Dim iArchivoEntrada As StreamReader
        Dim iArchivoSalida As StreamWriter
        Dim iPathArchivoFinanciera As String = ConfigurationManager.AppSettings("archivosRecibidos") & "Financ.seq"
        Dim iRegistroEntrada As String
        Dim iRegistroSalida As String
        Dim iContador As Long

        Try
            iArchivoEntrada = New StreamReader(iPathArchivoFinanciera, Text.Encoding.ASCII)
            iArchivoSalida = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "Financ.seq")

            iContador = 0

            While Not iArchivoEntrada.Peek = -1

                iRegistroEntrada = iArchivoEntrada.ReadLine
                'financiera 
                iRegistroSalida = iRegistroEntrada.Substring(0, 2) & ","
                'descripcion
                iRegistroSalida = iRegistroSalida & Trim(iRegistroEntrada.Substring(2, 27))

                iArchivoSalida.WriteLine(iRegistroSalida)

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
        Dim iFinancieras As String
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("documento")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("f.descripcion as financiera")
            iGeneradorSql.agregarColumna("f2.descripcion as financiera1")
            iGeneradorSql.agregarColumna("f3.descripcion as financiera2")
            iGeneradorSql.agregarColumna("mas")
            iGeneradorSql.agregarTabla("camara c left join financiera f on(f.codigo=c.financiera) left join financiera f2 on(f2.codigo=c.financiera1) left join financiera f3 on(f3.codigo=c.financiera2) ")

            iGeneradorSql.agregarCondicionWhere("documento=" & eDocumento)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iFinancieras = "Financieras:" & FuncionComun.vacioSiEsNulo(iDataReader.Item("financiera").ToString)
                If Not IsDBNull(iDataReader.Item("financiera1")) Then iFinancieras = iFinancieras & ", " & FuncionComun.vacioSiEsNulo(iDataReader.Item("financiera1").ToString)
                If Not IsDBNull(iDataReader.Item("financiera2")) Then iFinancieras = iFinancieras & ", " & FuncionComun.vacioSiEsNulo(iDataReader.Item("financiera2").ToString)
                If Not IsDBNull(iDataReader.Item("mas")) OrElse iDataReader.Item("mas").ToString <> "" Then iFinancieras = iFinancieras & ", " & FuncionComun.vacioSiEsNulo(iDataReader.Item("mas").ToString)
                Return iFinancieras
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
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

        Catch exception As exception
            Throw New RootException(exception)
        End Try

    End Sub

#End Region

End Class
