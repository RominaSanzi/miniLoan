Imports di.financiera.entidades
Imports di.financiera.utils
Imports di.financiera.reglasnegocios
Imports di.financiera.tareas
Imports di.financiera.seguridad

Module modMain

    Sub Main()
        Dim iFrmBarraTareasProcesosAutomaticos As frmBarraTareasProcesosAutomaticos
        Dim iFrmPrincipal As frmPrincipal
        Dim iFrmLogin As frmLogin
        Dim iMensaje As String
        Dim iOpcion As String
        Dim iOpcion2 As String
        Dim iOpcion3 As String

        Try

            gIngresoModMain = True

            If My.Application.CommandLineArgs.Count > 0 Then

                iOpcion = My.Application.CommandLineArgs.Item(0).ToString

                If My.Application.CommandLineArgs.Count >= 2 Then iOpcion2 = My.Application.CommandLineArgs.Item(1).ToString
                If My.Application.CommandLineArgs.Count >= 3 Then iOpcion3 = My.Application.CommandLineArgs.Item(2).ToString

                FuncionComun.validarVersionSistema(Nothing, "Proceso windows")

                Select Case My.Application.CommandLineArgs.Item(0).ToString
                    Case "/?"
                        iMensaje = "Opciones:" & vbNewLine
                        iMensaje &= "/pa" & Space(10) & "Proceso Automatico" & vbNewLine
                        Console.WriteLine(iMensaje)
                        MsgBox(iMensaje, MsgBoxStyle.Information, "Parametros")

                    Case "/pa"
                        iFrmBarraTareasProcesosAutomaticos = New frmBarraTareasProcesosAutomaticos(FuncionComun.enumProcesoAutomatico.PROCESOAUTOMATICO)
                        Application.Run(iFrmBarraTareasProcesosAutomaticos)

                    Case Else
                        MsgBox("Parametro no contemplado. Ejecute /? para conocer las opciones.", MsgBoxStyle.Critical, "Error")
                End Select
            Else

                iFrmLogin = New frmLogin
                If iFrmLogin.ShowDialog() = DialogResult.OK Then
                    iFrmPrincipal = New frmPrincipal
                    Application.Run(iFrmPrincipal)
                Else
                    End
                End If

            End If

        Catch exception As Exception
            MsgBox(Log.obtenerErrorAplicacion(exception, "MODMAIN").mensajeUsuario, MsgBoxStyle.Critical, "Error")
        Finally
            iFrmBarraTareasProcesosAutomaticos = Nothing
            iFrmPrincipal = Nothing
            iFrmLogin = Nothing
        End Try
    End Sub

End Module
