Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.Security.Cryptography

Public Class Pasante : Inherits Persona
    Private iNumeroPiola As Integer

    Private iConexion As accesoDatos

    Public Property numeroPiola As Integer
        Get
            Return iNumeroPiola
        End Get
        Set(value As Integer)
            iNumeroPiola = value
        End Set
    End Property

    Public Overrides Sub crear(eValidarNombre As Boolean)
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            MyBase.crear(eValidarNombre)

            iGeneradorSql.agregarColumna("numeroPiola")

            iGeneradorSql.agregarTabla("pasante")

            iGeneradorSql.agregarValue(numeroPiola)

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
