Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.seguridad
Imports di.financiera.utils

Public MustInherit Class Persona
    Inherits Entidad

#Region "Variables"

    Private iId As Long
    Private iTipoDocumento As TipoDocumento
    Private iDocumento As Nullable(Of Long)
    Private iNombre As String = ""
    Private iFechaNacimiento As Date
    Private iFechaAlta As Date
    Private iCuil1 As Integer
    Private iCuil2 As Integer
    Private iDomicilio As Domicilio
    Private iEstado As Estado
    Private iSexo As Sexo
    Private iEmail As String
    Private iDatosAnexos As List(Of DatoAnexo)

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

    Public Property documento As Nullable(Of Long)
        Get
            Return iDocumento
        End Get
        Set(value As Nullable(Of Long))
            iDocumento = value
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
    Public Property fechaNacimiento() As Date
        Get
            Return iFechaNacimiento
        End Get
        Set(ByVal Value As Date)
            iFechaNacimiento = Value
        End Set
    End Property
    Public Property cuil1() As Integer
        Get
            Return iCuil1
        End Get
        Set(ByVal Value As Integer)
            iCuil1 = Value
        End Set
    End Property
    Public Property cuil2() As Integer
        Get
            Return iCuil2
        End Get
        Set(ByVal Value As Integer)
            iCuil2 = Value
        End Set
    End Property
    Public Property tipoDocumento() As TipoDocumento
        Get
            Return iTipoDocumento
        End Get
        Set(ByVal Value As TipoDocumento)
            iTipoDocumento = Value
        End Set
    End Property
    Public Property domicilio() As Domicilio
        Get
            Return iDomicilio
        End Get
        Set(ByVal Value As Domicilio)
            iDomicilio = Value
        End Set
    End Property
    Public Property estado() As Estado
        Get
            Return iEstado
        End Get
        Set(ByVal Value As Estado)
            iEstado = Value
        End Set
    End Property
    Public Property sexo() As Sexo
        Get
            Return iSexo
        End Get
        Set(ByVal Value As Sexo)
            iSexo = Value
        End Set
    End Property
    Public Property datosAnexos As List(Of DatoAnexo)
        Get
            Return iDatosAnexos
        End Get
        Set(value As List(Of DatoAnexo))
            iDatosAnexos = value
        End Set
    End Property
    Public Property fechaAlta As Date
        Get
            Return iFechaAlta
        End Get
        Set(value As Date)
            iFechaAlta = value
        End Set
    End Property
    Public Property email As String
        Get
            Return iEmail
        End Get
        Set(value As String)
            iEmail = value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Function obtenerIdPersona() As Long
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("persona")
            If documento <> Nothing Then iGeneradorSql.agregarCondicionWhere("documento=" & documento)
            If Not IsNothing(tipoDocumento) Then iGeneradorSql.agregarCondicionWhere("idTipoDocumento=" & tipoDocumento.id)
            If Not IsNothing(iSexo) Then iGeneradorSql.agregarCondicionWhere("idSexo=" & sexo.id)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Return iDataReader.Item("id").ToString
            Else
                Throw New PersonaNoEncontradaException
            End If

        Catch excepcion As Exception
            Throw New PersonaNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try
    End Function
    Public Function obtenerNumerosDocumento() As String
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iMensaje As String
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("persona")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("documento")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarCondicionWhere("documento=" & documento)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            While iDataReader.Read
                iMensaje &= iDataReader.Item("documento").ToString & "-" & iDataReader.Item("nombre").ToString
            End While
            Return iMensaje

        Catch PersonaNoEncontradaException As PersonaNoEncontradaException
            Throw PersonaNoEncontradaException
        Catch excepcion As Exception
            Throw New PersonaNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try
    End Function

    Public Overridable Function obtenerPersona(Optional ByVal eNivel As Nivel = Nothing) As Persona
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDatoAnexo As New DatoAnexo
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("idTipoDocumento")
            iGeneradorSql.agregarColumna("documento")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("fechaNacimiento")
            iGeneradorSql.agregarColumna("FechaAlta")
            iGeneradorSql.agregarColumna("cuil1")
            iGeneradorSql.agregarColumna("cuil2")
            iGeneradorSql.agregarColumna("idDomicilio")
            iGeneradorSql.agregarColumna("idEstado")
            iGeneradorSql.agregarColumna("idSexo")
            iGeneradorSql.agregarColumna("email")

            iGeneradorSql.agregarTabla("persona")

            If iId <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If iDocumento <> Nothing Then iGeneradorSql.agregarCondicionWhere("documento=" & documento)
            If iCuil1 <> Nothing Then iGeneradorSql.agregarCondicionWhere("cuil1=" & cuil1)
            If iCuil2 <> Nothing Then iGeneradorSql.agregarCondicionWhere("cuil2=" & cuil2)
            If Not IsNothing(sexo) Then iGeneradorSql.agregarCondicionWhere("idSexo=" & sexo.id)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iDocumento = iDataReader.Item("documento").ToString
                iNombre = FuncionComun.vacioSiEsNulo(iDataReader.Item("nombre").ToString)
                iFechaNacimiento = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaNacimiento").ToString)
                iFechaAlta = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaAlta").ToString)
                iCuil1 = FuncionComun.ceroSiEsNulo(iDataReader.Item("cuil1").ToString)
                iCuil2 = FuncionComun.ceroSiEsNulo(iDataReader.Item("cuil2").ToString)
                Select Case iDataReader.Item("idTipoDocumento").ToString
                    Case TipoDocumento.DNI
                        iTipoDocumento = New DNI
                    Case TipoDocumento.CUIT
                        iTipoDocumento = New CUIT
                    Case TipoDocumento.CI
                        iTipoDocumento = New CI
                    Case TipoDocumento.LE
                        iTipoDocumento = New LE
                    Case TipoDocumento.LC
                        iTipoDocumento = New LC
                    Case TipoDocumento.PAS
                        iTipoDocumento = New PAS
                End Select
                iDomicilio = New Domicilio
                iDomicilio.id = iDataReader.Item("idDomicilio").ToString
                iEstado = IIf(iDataReader.Item("idEstado").ToString = Estado.ALTA, New Alta, New Baja)
                iSexo = New Sexo
                iSexo.id = IIf(IsDBNull(iDataReader.Item("idSexo")), Sexo.GENERICO, iDataReader.Item("idSexo"))

                iDataReader.Close()

                iDomicilio.accesoDatos = iConexion
                iDomicilio = iDomicilio.obtenerDomicilio
                iDomicilio.accesoDatos = Nothing

                iSexo.accesoDatos = iConexion
                iSexo = iSexo.obtenerSexo
                iSexo.accesoDatos = Nothing

                Return Me
            Else
                Throw New PersonaNoEncontradaException
            End If

        Catch PersonaNoEncontradaException As PersonaNoEncontradaException
            Throw PersonaNoEncontradaException
        Catch excepcion As Exception
            Throw New PersonaNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try
    End Function
    Public Overridable Function obtenerPersonaSoloIds(Optional ByVal eNivel As Nivel = Nothing, Optional ByVal eObtieneDatosBancarios As Boolean = True) As Persona
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("idTipoDocumento")
            iGeneradorSql.agregarColumna("documento")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("fechaNacimiento")
            iGeneradorSql.agregarColumna("FechaAlta")
            iGeneradorSql.agregarColumna("cuil1")
            iGeneradorSql.agregarColumna("cuil2")
            iGeneradorSql.agregarColumna("idDomicilio")
            iGeneradorSql.agregarColumna("idEstado")
            iGeneradorSql.agregarColumna("idSexo")
            iGeneradorSql.agregarColumna("email")

            iGeneradorSql.agregarTabla("persona")

            If iId <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If Not IsNothing(iSexo) AndAlso iSexo.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("idSexo=" & iSexo.id)
            If iDocumento <> Nothing Then iGeneradorSql.agregarCondicionWhere("documento=" & documento)
            If iCuil1 <> Nothing Then iGeneradorSql.agregarCondicionWhere("cuil1=" & cuil1)
            If iCuil2 <> Nothing Then iGeneradorSql.agregarCondicionWhere("cuil2=" & cuil2)
            If Not IsNothing(sexo) Then iGeneradorSql.agregarCondicionWhere("idSexo=" & sexo.id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iDocumento = iDataReader.Item("documento").ToString
                iNombre = FuncionComun.vacioSiEsNulo(iDataReader.Item("nombre").ToString)
                iFechaNacimiento = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaNacimiento").ToString)
                iFechaAlta = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaAlta").ToString)
                iCuil1 = FuncionComun.ceroSiEsNulo(iDataReader.Item("cuil1").ToString)
                iCuil2 = FuncionComun.ceroSiEsNulo(iDataReader.Item("cuil2").ToString)
                Select Case iDataReader.Item("idTipoDocumento").ToString
                    Case TipoDocumento.DNI
                        iTipoDocumento = New DNI
                    Case TipoDocumento.CUIT
                        iTipoDocumento = New CUIT
                    Case TipoDocumento.CI
                        iTipoDocumento = New CI
                    Case TipoDocumento.LE
                        iTipoDocumento = New LE
                    Case TipoDocumento.LC
                        iTipoDocumento = New LC
                    Case TipoDocumento.PAS
                        iTipoDocumento = New PAS
                End Select
                iDomicilio = New Domicilio
                iDomicilio.id = iDataReader.Item("idDomicilio").ToString
                iEstado = IIf(iDataReader.Item("idEstado").ToString = Estado.ALTA, New Alta, New Baja)
                iSexo = New Sexo
                iSexo.id = iDataReader.Item("idSexo").ToString
                iEmail = FuncionComun.vacioSiEsNulo(iDataReader.Item("email").ToString)

                iDataReader.Close()

                Return Me
            Else
                Throw New PersonaNoEncontradaException
            End If

        Catch PersonaNoEncontradaException As PersonaNoEncontradaException
            Throw PersonaNoEncontradaException
        Catch excepcion As Exception
            Throw New PersonaNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try
    End Function

    Private Sub validarCrear(ByVal eValidarNombre As Boolean)
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iParametro As New Parametro

        Try

            iConexion = obtenerConexion()

            If documento = Nothing Then
                Throw New PersonaNoCreadaException("El documento no puede ser nulo")
            End If

            If iTipoDocumento Is Nothing Then
                Throw New PersonaNoCreadaException("El tipo de documento no puede ser nulo")
            End If

            If iSexo Is Nothing Then
                Throw New PersonaNoCreadaException("El sexo no puede ser nulo")
            End If

            If eValidarNombre AndAlso iNombre <> Nothing Then
                If Not FuncionComun.validarNombre(iNombre) Then Throw New PersonaNoCreadaException("El nombre y/o apellido no pueden ser nulo/s")
            End If

            If eValidarNombre Then
                iGeneradorSql.agregarColumna("documento")
                iGeneradorSql.agregarColumna("idTipoDocumento")
                iGeneradorSql.agregarTabla("persona")
                iGeneradorSql.agregarCondicionWhere("documento=" & documento)
                iGeneradorSql.agregarCondicionWhere("idtipoDocumento=" & iTipoDocumento.id)
                iGeneradorSql.agregarCondicionWhere("idSexo=" & sexo.id)
                iGeneradorSql.agregarCondicionWhere("idEstado=" & Estado.ALTA)

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
                If iDataReader.Read Then
                    Throw New PersonaNoCreadaException("El documento ya existe")
                End If
                iDataReader.Close()
            End If
        Catch excepcion As ErrorConexionException
            Throw New PersonaNoCreadaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try
    End Sub

    Public Overridable Sub crear(ByVal eValidarNombre As Boolean)
        Dim iGeneradorSql As New GeneradorSql
        Dim iComillas As String = Chr(34)
        Dim i As Integer

        Try
            iConexion = obtenerConexion()

            validarCrear(eValidarNombre)

            iDomicilio.accesoDatos = iConexion
            iDomicilio.crear()
            iDomicilio.accesoDatos = Nothing

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("idTipoDocumento")
            iGeneradorSql.agregarColumna("documento")
            iGeneradorSql.agregarColumna("nombre")
            iGeneradorSql.agregarColumna("fechaNacimiento")
            iGeneradorSql.agregarColumna("FechaAlta")
            iGeneradorSql.agregarColumna("cuil1")
            iGeneradorSql.agregarColumna("cuil2")
            iGeneradorSql.agregarColumna("idDomicilio")
            iGeneradorSql.agregarColumna("idEstado")
            iGeneradorSql.agregarColumna("idSexo")
            iGeneradorSql.agregarColumna("email")

            iGeneradorSql.agregarTabla("persona")

            iGeneradorSql.agregarValue(tipoDocumento.id)
            iGeneradorSql.agregarValue(documento)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(Trim(nombre)))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(fechaNacimiento))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(fechaAlta))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(cuil1))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(cuil2))
            iGeneradorSql.agregarValue(domicilio.id)
            iGeneradorSql.agregarValue(IIf(estado.isAlta, Estado.ALTA, Estado.BAJA))
            iGeneradorSql.agregarValue(sexo.id)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(Trim(email)))

            If id <> Nothing Then
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarValue(id)
                iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            Else
                iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            End If

        Catch excepcion As Exception
            Throw New PersonaNoCreadaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub

    Private Sub validarModificar()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iParametro As New Parametro

        Try
            iConexion = obtenerConexion()

            If id = Nothing Then
                Throw New PersonaNoModificadaException("La persona a modificar no existe")
            End If

            If documento = Nothing OrElse documento = 0 Then
                Throw New PersonaNoModificadaException("El documento no puede ser nulo")
            End If

            If iSexo Is Nothing Then
                Throw New PersonaNoModificadaException("El sexo no puede ser nulo")
            End If

            'If iNombre <> Nothing Then
            '    If Not FuncionComun.validarNombre(iNombre) Then Throw New PersonaNoModificadaException("El nombre de la persona debe tener el formato Apellido/s,Nombre/s")
            'End If

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("persona")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not iDataReader.Read Then
                Throw New PersonaNoModificadaException("La persona a modificar no existe")
            End If

            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("persona")
            iGeneradorSql.agregarCondicionWhere("IdTipoDocumento=" & tipoDocumento.id)
            iGeneradorSql.agregarCondicionWhere("documento=" & documento)
            iGeneradorSql.agregarCondicionWhere("idSexo=" & sexo.id)
            iGeneradorSql.agregarCondicionWhere("idEstado=" & Estado.ALTA)
            iGeneradorSql.agregarCondicionWhere("id<>" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New PersonaNoModificadaException("No se puede modificar la parsona porque ya existe")
            End If

        Catch excepcion As Exception
            Throw New PersonaNoModificadaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try
    End Sub
    Public Overridable Sub modificar()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDatoAnexo As New DatoAnexo

        Try

            validarModificar()

            ' Modificar domicilio
            iDomicilio.accesoDatos = iConexion
            iDomicilio.modificar()
            iDomicilio.accesoDatos = Nothing

            iConexion = obtenerConexion()

            ' UPDATE persona
            iGeneradorSql.agregarTabla("persona")
            iGeneradorSql.agregarSet("nombre='" & FuncionComun.vacioSiEsNothing(Trim(nombre)) & "'")
            iGeneradorSql.agregarSet("fechaNacimiento=" & FuncionComun.nuloSiEsNothing(fechaNacimiento))
            iGeneradorSql.agregarSet("fechaAlta=" & FuncionComun.nuloSiEsNothing(fechaAlta))
            iGeneradorSql.agregarSet("cuil1=" & FuncionComun.nuloSiEsNothing(cuil1))
            iGeneradorSql.agregarSet("cuil2=" & FuncionComun.nuloSiEsNothing(cuil2))

            If iTipoDocumento.isCI Then iGeneradorSql.agregarSet("idTipoDocumento=" & TipoDocumento.CI)
            If iTipoDocumento.isDNI Then iGeneradorSql.agregarSet("idTipoDocumento=" & TipoDocumento.DNI)
            If iTipoDocumento.isLC Then iGeneradorSql.agregarSet("idTipoDocumento=" & TipoDocumento.LC)
            If iTipoDocumento.isLE Then iGeneradorSql.agregarSet("idTipoDocumento=" & TipoDocumento.LE)
            If iTipoDocumento.isPAS Then iGeneradorSql.agregarSet("idTipoDocumento=" & TipoDocumento.PAS)

            iGeneradorSql.agregarSet("idDomicilio=" & iDomicilio.id)
            iGeneradorSql.agregarSet("idSexo=" & iSexo.id)
            iGeneradorSql.agregarSet("idEstado=" & IIf(iEstado.isAlta, Estado.ALTA, Estado.BAJA))
            iGeneradorSql.agregarSet("email='" & FuncionComun.vacioSiEsNothing(Trim(email)) & "'")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

            ' DATOS ANEXOS
            If Not IsNothing(datosAnexos) Then

                With iDatoAnexo
                    .entidad = Me
                    .tipoEntidad = New TipoEntidad With {.id = TipoEntidad.CLIENTE}
                    .accesoDatos = iConexion
                    .eliminar()
                    .accesoDatos = Nothing
                End With

                For Each iDato In datosAnexos
                    iDato.accesoDatos = iConexion
                    iDato.entidad = Me
                    iDato.crear()
                    iDato.accesoDatos = Nothing
                Next

            End If

        Catch excepcion As Exception
            Throw New PersonaNoModificadaException(excepcion)

        Finally
            ' CORRECCIÓN: cerrar solo si existe
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

        End Try
    End Sub

    Private Sub validarEliminar()

        If id = Nothing Then
            Throw New PersonaNoEliminadaException("La persona a eliminar no existe")
        End If

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("persona")
            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not iDataReader.Read Then
                Throw New PersonaNoEliminadaException("La persona a eliminar no existe")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New PersonaNoEliminadaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try
    End Sub

    Public Overridable Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            validarEliminar()

            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("persona")
            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

            iDomicilio.accesoDatos = iConexion
            iDomicilio.eliminar()
            iDomicilio.accesoDatos = Nothing

        Catch excepcion As DomicilioNoEliminadoException
            Throw New PersonaNoEliminadaException(excepcion)
        Catch excepcion As ErrorConexionException
            Throw New PersonaNoEliminadaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub
    Private Sub validarBaja()

        If id = Nothing Then
            Throw New PersonaNoEliminadaException("La persona que desea dar de Baja no existe")
        End If

    End Sub
    Public Overridable Sub baja()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarBaja()

            iGeneradorSql.agregarTabla("persona")
            iGeneradorSql.agregarSet("idEstado=" & Estado.BAJA)
            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As ErrorConexionException
            Throw New PersonaNoEliminadaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub
#End Region
End Class