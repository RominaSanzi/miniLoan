Imports di.financiera.excepciones
Imports System.Collections.Generic
Imports System.Text
Imports di.financiera.seguridad
Imports di.financiera.entidades
Imports System.Configuration

Public Class TokenServicio

#Region "Variables"
    Private Shared iTokensServicio As List(Of TokenServicioVO)
#End Region

#Region "Atributos"

    Public Shared Property tokensServicio As List(Of TokenServicioVO)
        Get
            Return iTokensServicio
        End Get
        Set(value As List(Of TokenServicioVO))
            iTokensServicio = value
        End Set
    End Property

#End Region

    Public Shared Function obtenerTokenServicio(ByVal eTokenServicioVO As TokenServicioVO) As TokenServicioVO
        Dim iTokenServicioVO As TokenServicioVO

        If IsNothing(iTokensServicio) Then iTokensServicio = New List(Of TokenServicioVO)

        Try
            iTokenServicioVO = iTokensServicio.Find(Function(TokenServicioVO) TokenServicioVO.token = eTokenServicioVO.token)

            If Not IsNothing(iTokenServicioVO) Then
                If iTokenServicioVO.fechaVencimiento < Now Then
                    Throw New RootException("El token ya no es válido")
                End If
            End If

            If IsNothing(iTokenServicioVO) Then
                Throw New RootException("El token provisto no es válido")
            Else
                Return iTokenServicioVO
            End If

        Catch ex As Exception
            Throw New RootException(ex)
        End Try

    End Function

    Public Shared Function crearTokenServicio(ByVal eTokenServicioVO As TokenServicioVO) As TokenServicioVO
        Dim iTokenServicioVO As TokenServicioVO

        If IsNothing(iTokensServicio) Then iTokensServicio = New List(Of TokenServicioVO)

        iTokenServicioVO = iTokensServicio.Find(Function(TokenServicioVO) TokenServicioVO.idUsuario = eTokenServicioVO.idUsuario)

        If Not IsNothing(iTokenServicioVO) Then
            iTokensServicio.Remove(iTokenServicioVO)
        End If

        eTokenServicioVO.palabraClave = generarPalabraClave(5, 10)
        Dim iEncriptador As New Encriptador(eTokenServicioVO.palabraClave)

        eTokenServicioVO.token = iEncriptador.encriptar(eTokenServicioVO.login & "*|*" & eTokenServicioVO.password)

        If ConfigurationManager.AppSettings("duracionTokenServicio") <> Nothing Then
            eTokenServicioVO.fechaVencimiento = Now.AddMinutes(FuncionComun.ceroSiEsVacio(ConfigurationManager.AppSettings("duracionTokenServicio")))
        Else
            eTokenServicioVO.fechaVencimiento = Now.AddHours(1)
        End If

        FuncionComun.loguearTokenServicios("Token:" & eTokenServicioVO.token & ", Clave:" & eTokenServicioVO.palabraClave & ", Login:" & eTokenServicioVO.login & "*|*" & eTokenServicioVO.password & ", VTO;" & eTokenServicioVO.fechaVencimiento & ", idUsuario:" & eTokenServicioVO.idUsuario)

        iTokensServicio.Add(eTokenServicioVO)

        Return eTokenServicioVO

    End Function

    Private Shared Function generarPalabraClave(eLongitudMinima As Integer, eLongitudMaxima As Integer) As String
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Static r As New Random
        Dim chactersInString As Integer = r.Next(eLongitudMinima, eLongitudMaxima)
        Dim sb As New StringBuilder
        For i As Integer = 1 To chactersInString
            Dim idx As Integer = r.Next(0, s.Length)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function


End Class
