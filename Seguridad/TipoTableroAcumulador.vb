Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.Collections.Generic
Public Class TipoTableroAcumulador

    Inherits Entidad

#Region "Variables"

    Private iConexion As accesoDatos
#End Region

#Region "Metodos"
    Public Sub generarTableros()
        Dim iGeneradorSql As New GeneradorSql

        Try

            iConexion = obtenerConexion()
            'PRIMERO GENERAMOS LOS ACUMULADORES
            iGeneradorSql.agregarCondicionWhere("eFecha=" & Format(Today, "yyyyMMdd"))
            iConexion.ejecutarStoredProcedure("generarTiposTablerosAcumuladores", iGeneradorSql.ParametrosSQLStoredProcedure)

            'LUEGO GENERAMOS LOS TABLEROS DE LOS USUARIOS
            iGeneradorSql.agregarCondicionWhere("eFecha=" & Format(Today, "yyyyMMdd"))
            iConexion.ejecutarStoredProcedure("generarTablerosTodosLosUsuarios", iGeneradorSql.ParametrosSQLStoredProcedure)


        Catch excepcion As Exception
            Throw New RootException(excepcion)
        End Try
    End Sub

    Public Sub generarTablerosDinamicosPorUsuario(ByRef eUsuario As Usuario, eFecha As Date)
        Dim iGeneradorSql As New GeneradorSql

        Try

            iConexion = obtenerConexion()
            'PRIMERO GENERAMOS LOS ACUMULADORES
            iGeneradorSql.agregarCondicionWhere("eFecha=" & Format(eFecha, "yyyyMMdd"))
            iGeneradorSql.agregarCondicionWhere("eIdUsuario=" & eUsuario.id)
            iConexion.ejecutarStoredProcedure("generarTablerosDinamicosPorUsuario", iGeneradorSql.ParametrosSQLStoredProcedure)


        Catch excepcion As Exception
            Throw New RootException(excepcion)
        End Try
    End Sub

    Public Sub generarTablerosEstaticosPorUsuario(ByRef eUsuario As Usuario, eFecha As Date)
        Dim iGeneradorSql As New GeneradorSql

        Try

            iConexion = obtenerConexion()
            'PRIMERO GENERAMOS LOS ACUMULADORES
            iGeneradorSql.agregarCondicionWhere("eFecha=" & Format(eFecha, "yyyyMMdd"))
            iGeneradorSql.agregarCondicionWhere("eIdUsuario=" & eUsuario.id)
            iConexion.ejecutarStoredProcedure("generarTablerosEstaticosPorUsuario", iGeneradorSql.ParametrosSQLStoredProcedure)


        Catch excepcion As Exception
            Throw New RootException(excepcion)
        End Try
    End Sub

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
