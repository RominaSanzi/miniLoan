Imports System.Net.Mail
Imports di.financiera.entidades

Public Class frmBase
    Private Sub frmBase_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not gIngresoModMain AndAlso Not Debugger.IsAttached Then

            Dim iVersion As New Version
            Try

                iVersion = iVersion.obtenerVersion()
                iVersion.enviarEmail(iVersion, "Problemas version windows", "Los procesos de windows no estan configurados para iniciar del ModMain, por favor cambie la configuracion")

            Catch exception As Exception
                FuncionComun.loguearProcesoVersion(Format(Now, "dd/MM/yyyy HH:mm:ss") & vbTab & "PROBLEMAS VERSION WINDOWS" & vbTab & "LOS PROCESOS DE WINDOWS NO ESTAN CONFIGURADOS PARA INICIAR DEL MODMAIN, POR FAVOR CAMBIE LA CONFIGURACION" & vbTab)
            Finally
                iVersion = Nothing
                MsgBox("Los procesos de windows no estan configurados para iniciar del ModMain, por favor cambie la configuracion", MsgBoxStyle.Exclamation, "Mod main")
                Me.Close()
            End Try

        End If
    End Sub
End Class