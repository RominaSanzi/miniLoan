Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades

Public Class Domicilio

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iCalle As String = ""
    Private iNumero As String = ""
    Private iPiso As String = ""
    Private iBarrio As String = ""
    Private iCodigoPostal As String = ""
    Private iLocalidad As Localidad

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
    Public Property calle() As String
        Get
            Return iCalle
        End Get
        Set(ByVal Value As String)
            iCalle = Value
        End Set
    End Property
    Public Property numero() As String
        Get
            Return iNumero
        End Get
        Set(ByVal Value As String)
            iNumero = Value
        End Set
    End Property
    Public Property codigoPostal() As String
        Get
            Return iCodigoPostal
        End Get
        Set(ByVal Value As String)
            iCodigoPostal = Value
        End Set
    End Property

    Public Property localidad As Localidad
        Get
            Return iLocalidad
        End Get
        Set(value As Localidad)
            iLocalidad = value
        End Set
    End Property

    Public Property piso() As String
        Get
            Return iPiso
        End Get
        Set(ByVal Value As String)
            iPiso = Value
        End Set
    End Property

    Public Property barrio() As String
        Get
            Return iBarrio
        End Get
        Set(ByVal Value As String)
            iBarrio = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerDomicilio() As Domicilio
        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("calle")
            iGeneradorSql.agregarColumna("numero")
            iGeneradorSql.agregarColumna("codigoPostal")
            iGeneradorSql.agregarColumna("idLocalidad")

            iGeneradorSql.agregarTabla("domicilio")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iCalle = FuncionComun.vacioSiEsNulo(iDataReader.Item("calle").ToString.Replace("%", "'"))
                iNumero = FuncionComun.vacioSiEsNulo(iDataReader.Item("numero").ToString)
                iCodigoPostal = FuncionComun.vacioSiEsNulo(iDataReader.Item("codigoPostal").ToString)
                iLocalidad = New Localidad

                iDataReader.Close()

                iLocalidad.accesoDatos = iConexion
                iLocalidad = iLocalidad.obtenerLocalidad
                iLocalidad.accesoDatos = Nothing

                Return Me
            Else
                Throw New DomicilioNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New DomicilioNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try

    End Function

    Public Function obtenerDomicilioSoloIds() As Domicilio
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("calle")
            iGeneradorSql.agregarColumna("numero")
            iGeneradorSql.agregarColumna("codigoPostal")
            iGeneradorSql.agregarColumna("idLocalidad")

            iGeneradorSql.agregarTabla("domicilio")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iCalle = FuncionComun.vacioSiEsNulo(iDataReader.Item("calle").ToString.Replace("%", "'"))
                iNumero = FuncionComun.vacioSiEsNulo(iDataReader.Item("numero").ToString)
                iCodigoPostal = FuncionComun.vacioSiEsNulo(iDataReader.Item("codigoPostal").ToString)
                iLocalidad = New Localidad

                iDataReader.Close()

                iLocalidad.accesoDatos = iConexion
                iLocalidad = iLocalidad.obtenerLocalidad
                iLocalidad.accesoDatos = Nothing

                Return Me
            Else
                Throw New DomicilioNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New DomicilioNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try

    End Function
    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql()
        Try

            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("domicilio")

            iGeneradorSql.agregarColumna("calle")
            iGeneradorSql.agregarColumna("numero")
            iGeneradorSql.agregarColumna("codigoPostal")
            iGeneradorSql.agregarColumna("idLocalidad")
            iGeneradorSql.agregarColumna("idBarrio")

            If calle <> Nothing Then
                iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(Trim(calle.Replace("'", "%"))))
            Else
                iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(calle))
            End If
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(numero))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(Trim(codigoPostal)))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(localidad.id))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(barrio))

            If iId <> Nothing Then
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarValue(iId)

                iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            Else
                iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            End If

        Catch excepcion As Exception
            Throw New DomicilioNoCreadoException(excepcion)
        Finally
            'If (IsNothing(MyBase.accesoDatos)) Then
            '    iConexion.cerrar()
            '    iConexion = Nothing
            'End If
        End Try

    End Sub

    Private Sub validarCrear()
        Return
    End Sub

    Private Sub validarEliminar()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader

        Try
            iConexion = obtenerConexion()
            If id = Nothing OrElse id = 0 Then
                Throw New DomicilioNoEliminadoException("El domicilio es inexistente")
            End If

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("domicilio")
            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read = False Then
                Throw New DomicilioNoEliminadoException("El domicilio es inexistente")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("persona")
            iGeneradorSql.agregarCondicionWhere("iddomicilio=" & id)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New DomicilioNoEliminadoException("No se puede eliminar el domicilio porque está relacionado con una persona")
            End If

        Catch excepcion As ErrorConexionException
            Throw New DomicilioNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try

    End Sub

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            validarEliminar()

            iGeneradorSql.agregarTabla("domicilio")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New DomicilioNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Private Sub validarModificar()

        If id = Nothing OrElse id = 0 Then
            Throw New DomicilioNoModificadoException("El domicilio es inexistente")
        End If
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("domicilio")
            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not (iDataReader.Read) Then
                Throw New DomicilioNoModificadoException("El domicilio es inexistente")
            End If
        Catch excepcion As Exception
            Throw New DomicilioNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try

    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            validarModificar()

            If calle <> Nothing Then
                iGeneradorSql.agregarSet("calle=" & FuncionComun.nuloSiEsNothing(Trim(calle.Replace("'", "%"))))
            Else
                iGeneradorSql.agregarSet("calle=" & FuncionComun.nuloSiEsNothing(calle))
            End If
            iGeneradorSql.agregarSet("numero=" & FuncionComun.nuloSiEsNothing(numero))
            iGeneradorSql.agregarSet("codigoPostal=" & FuncionComun.nuloSiEsNothing(Trim(codigoPostal)))
            iGeneradorSql.agregarTabla("domicilio")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New DomicilioNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Public Sub modificarCalles(ByVal eCambiar As String)
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("domicilio")
            iGeneradorSql.agregarSet("calle= replace (calle,'" & eCambiar & "','" & calle & "')")
            iGeneradorSql.agregarCondicionWhere("calle like '%" & eCambiar & "%'")

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New DomicilioNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub
    Public Function celularExistenteParaOtraPersona(eDocumento As Long, ByVal eTelefono As String) As Boolean
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iTelefonoConCero, iTelefonoSinCero As String

        Try

            iConexion = obtenerConexion()

            If Left(eTelefono, 1) = 0 Then
                iTelefonoConCero = eTelefono
                iTelefonoSinCero = CLng(eTelefono)
            Else
                iTelefonoConCero = "0" + eTelefono
                iTelefonoSinCero = eTelefono
            End If

            iGeneradorSql.agregarColumna("d.id")

            iGeneradorSql.agregarTabla("domicilio d inner join persona p on p.idDomicilio=d.id")

            iGeneradorSql.agregarCondicionWhereConOr(FuncionComun.sqlConcatenar("d.TelefonoCelularcodigoarea,d.TelefonoCelularcaracteristica,d.TelefonoCelularnumero") & "='" & iTelefonoConCero & "'")
            iGeneradorSql.agregarCondicionWhereConOr(FuncionComun.sqlConcatenar("d.Telefonoreferenciacodigoarea,d.Telefonoreferenciacaracteristica,d.Telefonoreferencianumero") & "='" & iTelefonoConCero & "'")
            iGeneradorSql.agregarCondicionWhereConOr(FuncionComun.sqlConcatenar("d.Telefonocodigoarea,d.Telefonocaracteristica,d.Telefononumero") & "='" & iTelefonoConCero & "'")
            iGeneradorSql.agregarCondicionWhereConOr(FuncionComun.sqlConcatenar("d.TelefonoCelularcodigoarea,d.TelefonoCelularcaracteristica,d.TelefonoCelularnumero") & "='" & iTelefonoSinCero & "'")
            iGeneradorSql.agregarCondicionWhereConOr(FuncionComun.sqlConcatenar("d.Telefonoreferenciacodigoarea,d.Telefonoreferenciacaracteristica,d.Telefonoreferencianumero") & "='" & iTelefonoSinCero & "'")
            iGeneradorSql.agregarCondicionWhereConOr(FuncionComun.sqlConcatenar("d.Telefonocodigoarea,d.Telefonocaracteristica,d.Telefononumero") & "='" & iTelefonoSinCero & "'")
            iGeneradorSql.agregarCondicionWhere(iGeneradorSql.generarWhereConOr)
            iGeneradorSql.agregarCondicionWhere("p.documento<>" & eDocumento)
            iGeneradorSql.agregarCondicionWhere("p.idEstado=" & Estado.ALTA)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader.Read

        Catch excepcion As Exception
            Throw New DomicilioNoModificadoException(excepcion)
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
    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub
#End Region
End Class
