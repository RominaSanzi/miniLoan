Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Web.UI
Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class Pasante
    Inherits Persona

#Region "Variables"
    Private iLegajoEscuela As String = ""
    Private iFechaInicioPasantia As Date
    Private iFechaFinalizacionPasantia As Date
    Private iEscuela As String = ""
    Private iPersona As Persona
    Private iConexion As accesoDatos
#End Region

#Region "Atributos"
    Public Property legajoEscuela As String
        Get
            Return iLegajoEscuela
        End Get
        Set(value As String)
            iLegajoEscuela = value
        End Set
    End Property

    Public Property fechaInicioPasantia As Date
        Get
            Return iFechaInicioPasantia
        End Get
        Set(value As Date)
            iFechaInicioPasantia = value
        End Set
    End Property

    Public Property fechaFinalizacionPasantia As Date
        Get
            Return iFechaFinalizacionPasantia
        End Get
        Set(value As Date)
            iFechaFinalizacionPasantia = value
        End Set
    End Property

    Public Property escuela As String
        Get
            Return iEscuela
        End Get
        Set(value As String)
            iEscuela = value
        End Set
    End Property

    Public Property Persona As Persona
        Get
            Return iPersona
        End Get
        Set(value As Persona)
            iPersona = value
        End Set
    End Property
#End Region

#Region "Modificar"
    Public Overrides Sub modificar()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            ' Primero modificar persona
            MyBase.modificar()

            ' Ahora modificar tabla pasante
            iGeneradorSql.agregarTabla("pasante")
            iGeneradorSql.agregarSet("legajoEscuela =" & FuncionComun.ceroSiEsNothing(legajoEscuela), True)
            iGeneradorSql.agregarSet("fechaInicioPasantia =" & FuncionComun.nuloSiEsNothing(fechaInicioPasantia), True)
            iGeneradorSql.agregarSet("fechaFinalizacionPasantia =" & FuncionComun.nuloSiEsNothing(fechaFinalizacionPasantia), True)
            iGeneradorSql.agregarSet("idEscuela =" & escuela, True)

            iGeneradorSql.agregarCondicionWhere("idPersona = " & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch ex As Exception
            Throw New PersonaNoModificadaException(ex)

        Finally
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub
#End Region

#Region "Crear"
    Public Overrides Sub crear(eValidarNombre As Boolean)
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            ' Primero crear persona
            MyBase.crear(eValidarNombre)

            ' Ahora insertar pasante
            iGeneradorSql.agregarTabla("pasante")
            iGeneradorSql.agregarColumna("legajoEscuela")
            iGeneradorSql.agregarColumna("fechaInicioPasantia")
            iGeneradorSql.agregarColumna("fechaFinalizacionPasantia")
            iGeneradorSql.agregarColumna("idEscuela")
            iGeneradorSql.agregarColumna("idPersona")

            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(legajoEscuela))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(fechaInicioPasantia))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(fechaFinalizacionPasantia))
            iGeneradorSql.agregarValue(escuela)
            iGeneradorSql.agregarValue(id)

            iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch ex As Exception
            Throw New PersonaNoCreadaException(ex)

        Finally
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub
#End Region

#Region "Buscar lista de pasantes"
    Public Function BuscarPasantes() As List(Of Pasante)
        Dim lista As New List(Of Pasante)
        Dim iGeneradorSql As New GeneradorSql()
        Dim DataSet As DataSet = Nothing

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("pe.Documento")
            iGeneradorSql.agregarColumna("pe.Nombre AS NombrePersona")
            iGeneradorSql.agregarColumna("es.Nombre AS Escuela")

            iGeneradorSql.agregarTabla("persona pe")
            iGeneradorSql.agregarTablaConJoin("pasante pa", "pe.Id = pa.idPersona")
            iGeneradorSql.agregarTablaConJoin("escuela es", "pa.idEscuela = es.Id")

            If documento IsNot Nothing Then
                iGeneradorSql.agregarCondicionWhere("pe.Documento = " & documento)
            End If

            If Not String.IsNullOrEmpty(nombre) Then
                iGeneradorSql.agregarCondicionWhere("pe.Nombre LIKE '%" & nombre & "%'")
            End If

            DataSet = iConexion.getDataSet(iGeneradorSql.generarSelect(), iGeneradorSql.parametrosSQL, "DatosPasante")

            If DataSet IsNot Nothing AndAlso DataSet.Tables.Count > 0 Then
                For Each row As DataRow In DataSet.Tables(0).Rows
                    Dim p As New Pasante
                    If Not IsDBNull(row("Documento")) Then p.documento = row("Documento")
                    If Not IsDBNull(row("NombrePersona")) Then p.nombre = row("NombrePersona").ToString()
                    If Not IsDBNull(row("Escuela")) Then p.escuela = row("Escuela").ToString()
                    lista.Add(p)
                Next
            End If

            Return lista

        Catch ex As Exception
            Throw New Exception("Error al buscar pasantes: " & ex.Message)

        Finally
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
        End Try
    End Function
#End Region

#Region "Obtener por Documento (mapeo completo)"
    Public Function ObtenerPorDocumento(doc As String) As Pasante
        Dim iGeneradorSql As New GeneradorSql()
        Dim DataSet As DataSet = Nothing
        Dim pasante As Pasante = Nothing

        Try
            iConexion = obtenerConexion()

            ' SELECT gigante mapeando todo
            iGeneradorSql.agregarColumna("pe.Documento, pe.Nombre, pe.FechaNacimiento, pe.email, pe.idTipoDocumento, pe.id AS IDpersona")
            iGeneradorSql.agregarColumna("pe.Cuil1, pe.Cuil2")
            iGeneradorSql.agregarColumna("se.descripcion AS SexoDescripcion")
            iGeneradorSql.agregarColumna("pa.legajoEscuela, pa.fechaInicioPasantia, pa.fechaFinalizacionPasantia")
            iGeneradorSql.agregarColumna("es.Nombre As EscuelaNombre")
            iGeneradorSql.agregarColumna("Do.Calle, Do.numero, Do.Piso, Do.CodigoPostal, Do.id as IDdomicilio")
            iGeneradorSql.agregarColumna("lo.Descripcion As LocalidadDescripcion")
            iGeneradorSql.agregarColumna("ba.descripcion As BarrioDescripcion")

            iGeneradorSql.agregarTabla("persona pe")
            iGeneradorSql.agregarTablaConJoin("pasante pa", "pe.Id = pa.idPersona")
            iGeneradorSql.agregarTablaConJoin("escuela es", "pa.idEscuela = es.Id")
            iGeneradorSql.agregarTablaConJoin("domicilio Do", "Do.id = pe.idDomicilio")
            iGeneradorSql.agregarTablaConJoin("localidad lo", "lo.id = Do.idLocalidad")
            iGeneradorSql.agregarTablaConJoin("barrio ba", "ba.id = Do.idBarrio")
            iGeneradorSql.agregarTablaConJoin("sexo se", "se.id = pe.idSexo")

            iGeneradorSql.agregarCondicionWhere("pe.Documento = " & doc)

            DataSet = iConexion.getDataSet(iGeneradorSql.generarSelect(), iGeneradorSql.parametrosSQL, "PasanteCompleto")

            If DataSet IsNot Nothing AndAlso DataSet.Tables.Count > 0 AndAlso DataSet.Tables(0).Rows.Count > 0 Then
                Dim row As DataRow = DataSet.Tables(0).Rows(0)
                pasante = New Pasante()

                '==========================
                '  PERSONA
                '==========================
                If Not IsDBNull(row("IDpersona")) Then pasante.id = CLng(row("IDpersona"))
                If Not IsDBNull(row("Documento")) Then pasante.documento = CLng(row("Documento"))
                If Not IsDBNull(row("Nombre")) Then pasante.nombre = row("Nombre").ToString()
                If Not IsDBNull(row("FechaNacimiento")) Then pasante.fechaNacimiento = CDate(row("FechaNacimiento"))
                If Not IsDBNull(row("email")) Then pasante.email = row("email").ToString()
                If Not IsDBNull(row("Cuil1")) Then pasante.cuil1 = row("Cuil1").ToString()
                If Not IsDBNull(row("Cuil2")) Then pasante.cuil2 = row("Cuil2").ToString()

                ' Tipos documento
                If Not IsDBNull(row("idTipoDocumento")) Then
                    Select Case row("idTipoDocumento")
                        Case TipoDocumento.DNI : pasante.tipoDocumento = New DNI
                        Case TipoDocumento.CUIT : pasante.tipoDocumento = New CUIT
                        Case TipoDocumento.CI : pasante.tipoDocumento = New CI
                        Case TipoDocumento.LE : pasante.tipoDocumento = New LE
                        Case TipoDocumento.LC : pasante.tipoDocumento = New LC
                        Case TipoDocumento.PAS : pasante.tipoDocumento = New PAS
                    End Select
                End If

                ' Sexo
                If Not IsDBNull(row("SexoDescripcion")) Then
                    Dim sx As New Sexo()
                    sx.descripcion = row("SexoDescripcion").ToString()
                    pasante.sexo = sx
                End If

                '==========================
                '  DOMICILIO
                '==========================
                Dim dom As New Domicilio()
                If Not IsDBNull(row("IDdomicilio")) Then dom.id = CLng(row("IDdomicilio"))
                If Not IsDBNull(row("Calle")) Then dom.calle = row("Calle").ToString().Replace("%", "'")
                If Not IsDBNull(row("numero")) Then dom.numero = row("numero").ToString()
                If Not IsDBNull(row("Piso")) Then dom.piso = row("Piso").ToString()
                If Not IsDBNull(row("CodigoPostal")) Then dom.codigoPostal = row("CodigoPostal").ToString()

                Dim loc As New Localidad()
                If Not IsDBNull(row("LocalidadDescripcion")) Then loc.descripcion = row("LocalidadDescripcion").ToString()
                dom.localidad = loc

                If Not IsDBNull(row("BarrioDescripcion")) Then dom.barrio = row("BarrioDescripcion").ToString()

                pasante.domicilio = dom

                '==========================
                '  PASANTE
                '==========================
                If Not IsDBNull(row("legajoEscuela")) Then pasante.legajoEscuela = row("legajoEscuela").ToString()
                If Not IsDBNull(row("fechaInicioPasantia")) Then pasante.fechaInicioPasantia = CDate(row("fechaInicioPasantia"))
                If Not IsDBNull(row("fechaFinalizacionPasantia")) Then pasante.fechaFinalizacionPasantia = CDate(row("fechaFinalizacionPasantia"))
                If Not IsDBNull(row("EscuelaNombre")) Then pasante.escuela = row("EscuelaNombre").ToString()
            End If

            Return pasante

        Catch ex As Exception
            Throw New Exception("Error al obtener pasante por documento: " & ex.Message)

        Finally
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
        End Try
    End Function

    Private Sub validarEliminar()
        If id = Nothing Then
            Throw New PersonaNoEliminadaException("La persona a eliminar no existe")
        End If

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("pasante")
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

    Public Overrides Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            MyBase.eliminar()

            validarEliminar()

            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("pasante")
            iGeneradorSql.agregarCondicionWhere("idPersona=" & id)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)


        Catch ex As Exception
            Throw New PersonaNoEliminadaException("Error al eliminar pasante: " & ex.Message)

        Finally
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub
#End Region

End Class
