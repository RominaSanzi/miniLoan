Imports di.financiera.entidades
Imports di.financiera.reglasnegocios
Imports di.financiera.seguridad
Imports di.financiera.utils
Public Class frmPrincipal
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
	Friend WithEvents mnuPrincipal As System.Windows.Forms.MainMenu
	Friend WithEvents mnuArchivo As System.Windows.Forms.MenuItem
	Friend WithEvents mnuArchivoCerrarSesion As System.Windows.Forms.MenuItem
	Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
	Friend WithEvents mnuArchivoSalir As System.Windows.Forms.MenuItem
	Friend WithEvents imgMenu As System.Windows.Forms.ImageList
	Friend WithEvents StatusBar As System.Windows.Forms.StatusBar
	Friend WithEvents PanelUsuario As System.Windows.Forms.StatusBarPanel
	Friend WithEvents PanelFecha As System.Windows.Forms.StatusBarPanel
	Friend WithEvents mnuInsercion As System.Windows.Forms.MenuItem
	Friend WithEvents mnuEnvioInterface As MenuItem
	Friend WithEvents mnuProcesoAutomatico As MenuItem
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrincipal))
		Me.mnuPrincipal = New System.Windows.Forms.MainMenu(Me.components)
		Me.mnuArchivo = New System.Windows.Forms.MenuItem()
		Me.mnuArchivoCerrarSesion = New System.Windows.Forms.MenuItem()
		Me.MenuItem2 = New System.Windows.Forms.MenuItem()
		Me.mnuArchivoSalir = New System.Windows.Forms.MenuItem()
		Me.mnuInsercion = New System.Windows.Forms.MenuItem()
		Me.mnuProcesoAutomatico = New System.Windows.Forms.MenuItem()
		Me.imgMenu = New System.Windows.Forms.ImageList(Me.components)
		Me.StatusBar = New System.Windows.Forms.StatusBar()
		Me.PanelUsuario = New System.Windows.Forms.StatusBarPanel()
		Me.PanelFecha = New System.Windows.Forms.StatusBarPanel()
		CType(Me.PanelUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.PanelFecha, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'mnuPrincipal
		'
		Me.mnuPrincipal.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuArchivo, Me.mnuInsercion})
		'
		'mnuArchivo
		'
		Me.mnuArchivo.Index = 0
		Me.mnuArchivo.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuArchivoCerrarSesion, Me.MenuItem2, Me.mnuArchivoSalir})
		Me.mnuArchivo.Text = "&Archivo"
		'
		'mnuArchivoCerrarSesion
		'
		Me.mnuArchivoCerrarSesion.Index = 0
		Me.mnuArchivoCerrarSesion.Text = "Cerra sesión usuario"
		'
		'MenuItem2
		'
		Me.MenuItem2.Index = 1
		Me.MenuItem2.Text = "-"
		'
		'mnuArchivoSalir
		'
		Me.mnuArchivoSalir.Index = 2
		Me.mnuArchivoSalir.Text = "&Salir"
		'
		'mnuInsercion
		'
		Me.mnuInsercion.Index = 1
		Me.mnuInsercion.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuProcesoAutomatico})
		Me.mnuInsercion.Text = "Insercion"
		'
		'mnuProcesoAutomatico
		'
		Me.mnuProcesoAutomatico.Index = 0
		Me.mnuProcesoAutomatico.Text = "Proceso Automatico"
		'
		'imgMenu
		'
		Me.imgMenu.ImageStream = CType(resources.GetObject("imgMenu.ImageStream"), System.Windows.Forms.ImageListStreamer)
		Me.imgMenu.TransparentColor = System.Drawing.Color.Transparent
		Me.imgMenu.Images.SetKeyName(0, "")
		Me.imgMenu.Images.SetKeyName(1, "")
		Me.imgMenu.Images.SetKeyName(2, "")
		Me.imgMenu.Images.SetKeyName(3, "")
		'
		'StatusBar
		'
		Me.StatusBar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.StatusBar.Location = New System.Drawing.Point(0, 777)
		Me.StatusBar.Margin = New System.Windows.Forms.Padding(4)
		Me.StatusBar.Name = "StatusBar"
		Me.StatusBar.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.PanelUsuario, Me.PanelFecha})
		Me.StatusBar.ShowPanels = True
		Me.StatusBar.Size = New System.Drawing.Size(1505, 119)
		Me.StatusBar.SizingGrip = False
		Me.StatusBar.TabIndex = 1
		'
		'PanelUsuario
		'
		Me.PanelUsuario.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
		Me.PanelUsuario.Icon = CType(resources.GetObject("PanelUsuario.Icon"), System.Drawing.Icon)
		Me.PanelUsuario.Name = "PanelUsuario"
		Me.PanelUsuario.Text = "Usuario: Juan"
		Me.PanelUsuario.Width = 1274
		'
		'PanelFecha
		'
		Me.PanelFecha.Alignment = System.Windows.Forms.HorizontalAlignment.Center
		Me.PanelFecha.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
		Me.PanelFecha.Icon = CType(resources.GetObject("PanelFecha.Icon"), System.Drawing.Icon)
		Me.PanelFecha.Name = "PanelFecha"
		Me.PanelFecha.Text = "Martes, 17 de Octubre de 2006"
		Me.PanelFecha.Width = 231
		'
		'frmPrincipal
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
		Me.BackColor = System.Drawing.Color.Gray
		Me.ClientSize = New System.Drawing.Size(1505, 896)
		Me.Controls.Add(Me.StatusBar)
		Me.IsMdiContainer = True
		Me.Margin = New System.Windows.Forms.Padding(5)
		Me.Menu = Me.mnuPrincipal
		Me.Name = "frmPrincipal"
		Me.Text = ".:: Procesos de sistemas ::."
		CType(Me.PanelUsuario, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.PanelFecha, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

#End Region

#Region "Formulario"
	Private Sub frmPrincipal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
		mostrar()
	End Sub
#End Region

#Region "Metodos"

	Private Sub mostrar()

		Try
			mostrarPaneles()
		Catch exception As Exception
			MsgBox(Log.obtenerErrorAplicacion(exception, Me.ToString).mensajeUsuario, MsgBoxStyle.Critical, "Error")
		End Try
	End Sub

	Private Sub mostrarPaneles()

		Try
			PanelUsuario.Text = "Usuario: " & gUsuario.nombre
			PanelFecha.Text = Format(Today, "dddd, dd' de 'MMMM' de 'yyyy")

		Catch exception As Exception
			MsgBox(Log.obtenerErrorAplicacion(exception, Me.ToString).mensajeUsuario, MsgBoxStyle.Critical, "Error")
		End Try
	End Sub

#End Region

#Region "Menu"

	Private Sub mnuArchivoSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuArchivoSalir.Click
		End
	End Sub

	Private Sub mnuArchivoCerrarSesion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuArchivoCerrarSesion.Click
		gUsuario = Nothing
		Dim iThread As New System.Threading.Thread(AddressOf Main)
		iThread.Start()
		Application.Exit()
	End Sub

	Private Sub mnuProcesoAutomatico_Click(sender As Object, e As EventArgs) Handles mnuProcesoAutomatico.Click
		Dim ifrmMigrarTablasResponsive As New frmProcesoAutomatico
		ifrmMigrarTablasResponsive.MdiParent = Me
		ifrmMigrarTablasResponsive.Show()
	End Sub

#End Region

End Class
