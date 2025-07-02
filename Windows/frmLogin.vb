Imports di.financiera.seguridad
Imports di.financiera.excepciones
Imports di.financiera.reglasnegocios
Imports di.financiera.entidades
Imports di.financiera.datos
Imports di.financiera.utils

Public Class frmLogin
    Inherits frmBase

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
    Friend WithEvents grpUsuario As System.Windows.Forms.GroupBox
    Friend WithEvents lblContraseña As System.Windows.Forms.Label
    Friend WithEvents imagenes As System.Windows.Forms.ImageList
    Friend WithEvents btnIngresar As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents txtLogin As System.Windows.Forms.TextBox
    Friend WithEvents txtContraseña As System.Windows.Forms.TextBox
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.grpUsuario = New System.Windows.Forms.GroupBox()
        Me.txtContraseña = New System.Windows.Forms.TextBox()
        Me.txtLogin = New System.Windows.Forms.TextBox()
        Me.lblContraseña = New System.Windows.Forms.Label()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.imagenes = New System.Windows.Forms.ImageList(Me.components)
        Me.btnIngresar = New System.Windows.Forms.Button()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpUsuario.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpUsuario
        '
        Me.grpUsuario.Controls.Add(Me.txtContraseña)
        Me.grpUsuario.Controls.Add(Me.txtLogin)
        Me.grpUsuario.Controls.Add(Me.lblContraseña)
        Me.grpUsuario.Controls.Add(Me.lblUsuario)
        Me.grpUsuario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpUsuario.Location = New System.Drawing.Point(8, 8)
        Me.grpUsuario.Name = "grpUsuario"
        Me.grpUsuario.Size = New System.Drawing.Size(200, 80)
        Me.grpUsuario.TabIndex = 1
        Me.grpUsuario.TabStop = False
        Me.grpUsuario.Text = " usuario "
        '
        'txtContraseña
        '
        Me.txtContraseña.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContraseña.Location = New System.Drawing.Point(88, 48)
        Me.txtContraseña.Name = "txtContraseña"
        Me.txtContraseña.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtContraseña.Size = New System.Drawing.Size(100, 21)
        Me.txtContraseña.TabIndex = 3
        '
        'txtLogin
        '
        Me.txtLogin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLogin.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLogin.Location = New System.Drawing.Point(88, 24)
        Me.txtLogin.Name = "txtLogin"
        Me.txtLogin.Size = New System.Drawing.Size(100, 21)
        Me.txtLogin.TabIndex = 2
        '
        'lblContraseña
        '
        Me.lblContraseña.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContraseña.Location = New System.Drawing.Point(8, 48)
        Me.lblContraseña.Name = "lblContraseña"
        Me.lblContraseña.Size = New System.Drawing.Size(67, 16)
        Me.lblContraseña.TabIndex = 90
        Me.lblContraseña.Text = "contraseña"
        '
        'lblUsuario
        '
        Me.lblUsuario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.Location = New System.Drawing.Point(7, 24)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(88, 16)
        Me.lblUsuario.TabIndex = 80
        Me.lblUsuario.Text = "login"
        '
        'imagenes
        '
        Me.imagenes.ImageStream = CType(resources.GetObject("imagenes.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imagenes.TransparentColor = System.Drawing.Color.Transparent
        Me.imagenes.Images.SetKeyName(0, "")
        Me.imagenes.Images.SetKeyName(1, "")
        '
        'btnIngresar
        '
        Me.btnIngresar.ImageIndex = 0
        Me.btnIngresar.ImageList = Me.imagenes
        Me.btnIngresar.Location = New System.Drawing.Point(112, 96)
        Me.btnIngresar.Name = "btnIngresar"
        Me.btnIngresar.Size = New System.Drawing.Size(45, 40)
        Me.btnIngresar.TabIndex = 4
        Me.ToolTip.SetToolTip(Me.btnIngresar, "ingresar")
        '
        'btnSalir
        '
        Me.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSalir.ImageIndex = 1
        Me.btnSalir.ImageList = Me.imagenes
        Me.btnSalir.Location = New System.Drawing.Point(163, 96)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(45, 40)
        Me.btnSalir.TabIndex = 5
        Me.ToolTip.SetToolTip(Me.btnSalir, "salir")
        '
        'frmLogin
        '
        Me.AcceptButton = Me.btnIngresar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.CancelButton = Me.btnSalir
        Me.ClientSize = New System.Drawing.Size(216, 143)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.btnIngresar)
        Me.Controls.Add(Me.grpUsuario)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.grpUsuario.ResumeLayout(False)
        Me.grpUsuario.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Metodos"

    Private Sub validarUsuario()
        Dim iUsuario As di.financiera.seguridad.Usuario
        Dim iAdministradorUsuarios As New AdministradorUsuarios

        Try

            If IsNothing(gUsuario) Then
                iUsuario = New di.financiera.seguridad.Usuario
                iUsuario.login = txtLogin.Text
                iUsuario.password = txtContraseña.Text
                iUsuario = iAdministradorUsuarios.validarUsuario(iUsuario, Nothing)
                If iUsuario.pedirCambioPassword Then
                    MsgBox("Debe modificar la contraseña de su usuario para poder ingresar", MsgBoxStyle.Exclamation, "Usuario no autorizado")
                Else
                    gUsuario = iUsuario
                    DialogResult = DialogResult.OK
                    If Not Modal Then Close()
                End If
            Else
                txtLogin.Focus()
                txtContraseña.Clear()
            End If

        Catch exception As Exception
            MsgBox(Log.obtenerErrorAplicacion(exception, Me.ToString).mensajeUsuario, MsgBoxStyle.Critical, "Error")
            txtContraseña.Clear()
            txtLogin.Focus()
            txtLogin.SelectAll()
        Finally
            iUsuario = Nothing
            iAdministradorUsuarios = Nothing
        End Try
    End Sub

#End Region

#Region "Botones"
    Private Sub btnIngresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIngresar.Click
        'Dim iaccesoDatos As New accesoDatos
        'Dim iAdministradorExternos As New AdministradorExternos
        'Dim iAdministradorSolicitudes As New AdministradorSolicitudes
        'Dim iSolicitudPendiente As New SolicitudPendiente

        'iSolicitudPendiente.id = 215

        'iSolicitudPendiente = iAdministradorSolicitudes.obtenerSolicitudPendiente(iaccesoDatos, iSolicitudPendiente)

        'iAdministradorExternos.notificarRechazoSistemaExterno(iaccesoDatos, iSolicitudPendiente)

        'iAdministradorExternos.notificarCocrecionSistemaExterno(iaccesoDatos, iSolicitudPendiente)



        validarUsuario()
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        End
    End Sub
#End Region

#Region "TextBox"
    Private Sub txtLogin_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLogin.GotFocus
        txtLogin.SelectionStart = 0
        txtLogin.SelectionLength = Len(txtLogin.Text)
        txtLogin.Focus()
    End Sub

    Private Sub txtContraseña_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtContraseña.GotFocus
        txtContraseña.SelectionStart = 0
        txtContraseña.SelectionLength = Len(txtContraseña.Text)
        txtContraseña.Focus()
    End Sub

#End Region

End Class
