Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.Security.Cryptography

Public Class Pasante
    Inherits Persona

#Region "Variables"

    Private iID As Long
    Private iPersona As Persona
    Private iLegajoEscuela As String = ""
    Private iFechaInicioPasantia As Date
    Private iFechaFinalizacionPasantia As Date
    Private iEscuela As String = ""
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

    Public Overrides Sub crear(eValidarNombre As Boolean)
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            MyBase.crear(eValidarNombre)

            iGeneradorSql.agregarColumna("legajoEscuela")
            iGeneradorSql.agregarColumna("fechaInicioPasantia")
            iGeneradorSql.agregarColumna("fechaFinalizacionPasantia")
            iGeneradorSql.agregarColumna("idEscuela")
            iGeneradorSql.agregarColumna("idPersona")

            iGeneradorSql.agregarTabla("pasante")

            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(legajoEscuela))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(fechaInicioPasantia))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(fechaFinalizacionPasantia))
            iGeneradorSql.agregarValue(escuela)
            iGeneradorSql.agregarValue(id)



            If id <> Nothing Then
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarValue(id)
                iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            Else
                id = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
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

End Class
