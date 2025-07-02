Imports System.Collections.Generic
Imports di.financiera.excepciones
Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.utils
Imports di.financiera.seguridad

Public Class PuntoVentaDgi : Inherits nivel

#Region "Constantes"
    Public Const PUNTOVENTADGIUSUARIOCAJEROMIGRACION As Integer = 5
    Public Const PUNTOVTADGIACOM As Integer = 12
    Public Const PUNTOVTADGIBCOM As Integer = 11
#End Region

#Region "Variables"
    Private iNumero As Integer
    Private iTipoComprobante As TipoComprobante
    Private iDomcilioFiscal As String
    Private iNumeroEquivalencia As Integer
    Private iNivel As Nivel
    Private iElectronico As Boolean

    Private iConexion As accesoDatos
#End Region

#Region "Atributos"
    Public Property numero() As Integer
        Get
            Return iNumero
        End Get
        Set(ByVal Value As Integer)
            iNumero = Value
        End Set
    End Property
    Public Property tipoComprobante() As TipoComprobante
        Get
            Return iTipoComprobante
        End Get
        Set(ByVal Value As tipoComprobante)
            iTipoComprobante = Value
        End Set
    End Property
    Public Property domcilioFiscal() As String
        Get
            Return iDomcilioFiscal
        End Get
        Set(ByVal Value As String)
            iDomcilioFiscal = Value
        End Set
    End Property
    Public Property numeroEquivalencia() As Integer
        Get
            Return iNumeroEquivalencia
        End Get
        Set(ByVal Value As Integer)
            iNumeroEquivalencia = Value
        End Set
    End Property
    Public Property nivel() As Nivel
        Get
            Return iNivel
        End Get
        Set(ByVal Value As Nivel)
            iNivel = Value
        End Set
    End Property
    Public Property electronico() As Boolean
        Get
            Return iElectronico
        End Get
        Set(ByVal Value As Boolean)
            iElectronico = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Overrides Function isSucursal() As Boolean
        Return False
    End Function

    Public Overrides Function isUnidadDeNegocios() As Boolean
        Return False
    End Function

    Public Overrides Function isEmpresaGrupo() As Boolean
        Return False
    End Function

    Public Overrides Function isGrupoEmpresas() As Boolean
        Return False
    End Function

    Public Overrides Function isPuntoVentaDgi() As Boolean
        Return True
    End Function
    
    Public Overrides Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            MyBase.accesoDatos = iConexion
            MyBase.crear()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("numero")
            iGeneradorSql.agregarColumna("NumeroEquivalencia")
            iGeneradorSql.agregarColumna("idTipoComprobante")
            iGeneradorSql.agregarColumna("domcilioFiscal")
            iGeneradorSql.agregarColumna("idNivel")
            iGeneradorSql.agregarColumna("electronico")

            iGeneradorSql.agregarValue(id)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(numero))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(numeroEquivalencia))
            iGeneradorSql.agregarValue(iTipoComprobante.id)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(domcilioFiscal))
            If IsNothing(iNivel) Then
                iGeneradorSql.agregarValue("NULL")
            Else
                iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iNivel.id))
            End If
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(iElectronico))

            iGeneradorSql.agregarTabla("PuntoVentaDgi")

            iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New SucursalNoCreadaException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing

        End Try
    End Sub

    Public Overrides Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            MyBase.accesoDatos = iConexion
            MyBase.modificar()

            iGeneradorSql.agregarTabla("PuntoVentaDgi")
            iGeneradorSql.agregarSet("numero=" & iNumero)
            iGeneradorSql.agregarSet("NumeroEquivalencia=" & iNumeroEquivalencia)
            iGeneradorSql.agregarSet("idTipoComprobante=" & iTipoComprobante.id)
            iGeneradorSql.agregarSet("domcilioFiscal=" & FuncionComun.nuloSiEsNothing(domcilioFiscal))
            If IsNothing(iNivel) Then
                iGeneradorSql.agregarSet("idNivel=NULL")
            Else
                iGeneradorSql.agregarSet("idNivel=" & FuncionComun.nuloSiEsNothing(iNivel.id))
            End If
            iGeneradorSql.agregarSet("electronico=" & FuncionComun.booleanByte(iElectronico))

            iGeneradorSql.agregarCondicionWhere("id =" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Public Overrides Function obtenerNivelesLista(eNivelOrigen As Nivel) As List(Of Nivel)
        Dim iGeneradorSql As New GeneradorSql()
        Dim iPuntoVentaDgi As PuntoVentaDgi
        Dim iDataSet As DataSet
        Dim iNiveles As List(Of Nivel)

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("PuntoVentaDgi")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Niveles")

            For Each iDataRow As DataRow In iDataSet.Tables("Niveles").Rows
                If IsNothing(iNiveles) Then iNiveles = New List(Of Nivel)

                iPuntoVentaDgi = New PuntoVentaDgi
                iPuntoVentaDgi.id = iDataRow.Item("id")
                iPuntoVentaDgi.accesoDatos = iConexion
                iPuntoVentaDgi = iPuntoVentaDgi.obtenerNivel(False)
                iPuntoVentaDgi.accesoDatos = Nothing
                iPuntoVentaDgi = Nothing

                iNiveles.Add(iPuntoVentaDgi)
            Next

            Return iNiveles

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iPuntoVentaDgi = Nothing
            iDataSet = Nothing
            iNiveles = Nothing
        End Try
    End Function

    Public Overrides Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            validarEliminar()

            MyBase.accesoDatos = iConexion
            MyBase.eliminar()

            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iGeneradorSql.agregarTabla("PuntoVentaDgi")
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New RootException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub validarEliminar()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idPuntoVentaDgi=" & id)
            iGeneradorSql.agregarTabla("comprobante")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New RootException("El punto de venta Dgi tiene comprobantes asociado")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhereConOr("idPuntoVentaDgiInmediatoNotaCredito=" & id)
            iGeneradorSql.agregarCondicionWhereConOr("idPuntoVentaDgiDiferido=" & id)
            iGeneradorSql.agregarCondicionWhere(iGeneradorSql.generarWhereConOr, True)

            iGeneradorSql.agregarTabla("usuario")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New RootException("El punto de venta Dgi tiene usuarios asociado")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhereConOr("idPuntoVentaDgiInmediatoInteresesYGastos=" & id)
            iGeneradorSql.agregarCondicionWhereConOr("idPuntoVentaDgiInmediatoPunitorios=" & id)
            iGeneradorSql.agregarCondicionWhere(iGeneradorSql.generarWhereConOr, True)

            iGeneradorSql.agregarTabla("usuario")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New RootException("El punto de venta Dgi tiene usuarios asociado")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Overrides Function obtenerNivel(Optional eObtenerDomicilio As Boolean = True) As Nivel
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Dim iIdNivel As Long

        Try
            iConexion = obtenerConexion()


            iGeneradorSql.agregarTabla("PuntoVentaDgi")

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("idTipoComprobante")
            iGeneradorSql.agregarColumna("numero")
            iGeneradorSql.agregarColumna("NumeroEquivalencia")
            iGeneradorSql.agregarColumna("domcilioFiscal")
            iGeneradorSql.agregarColumna("idNivel")
            iGeneradorSql.agregarColumna("electronico")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If iNumero <> Nothing Then iGeneradorSql.agregarCondicionWhere("numero=" & iNumero)
            If Not IsNothing(iTipoComprobante) <> Nothing Then iGeneradorSql.agregarCondicionWhere("idTipoComprobante=" & iTipoComprobante.id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                id = iDataReader.Item("id").ToString
                iNumero = iDataReader.Item("numero").ToString
                iNumeroEquivalencia = iDataReader.Item("NumeroEquivalencia").ToString
                iDomcilioFiscal = iDataReader.Item("domcilioFiscal").ToString
                iElectronico = FuncionComun.byteBoolean(iDataReader.Item("electronico"))

                Select Case iDataReader.Item("idTipoComprobante").ToString
                    Case tipoComprobante.FACTURAA
                        iTipoComprobante = New FacturaA
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case tipoComprobante.FACTURAB
                        iTipoComprobante = New FacturaB
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case tipoComprobante.FACTURAAPUNTUAL
                        iTipoComprobante = New FacturaAPuntual
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case tipoComprobante.FACTURABPUNTUAL
                        iTipoComprobante = New FacturaBPuntual
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case tipoComprobante.FACTURABPUNTUALMOSTRADOR
                        iTipoComprobante = New FacturaBPuntualMostrador
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case tipoComprobante.NOTADECREDITOB
                        iTipoComprobante = New NotaDeCreditoB
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case tipoComprobante.NOTADEDEBITOB
                        iTipoComprobante = New NotaDeDebitoB
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case tipoComprobante.NOTADECREDITOA
                        iTipoComprobante = New NotaDeCreditoA
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case tipoComprobante.NOTADEDEBITOA
                        iTipoComprobante = New NotaDeDebitoA
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case tipoComprobante.NOTADECREDITOBMOSTRADOR
                        iTipoComprobante = New NotaDeCreditoBMostrador
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case tipoComprobante.NOTADEDEBITOBMOSTRADOR
                        iTipoComprobante = New NotaDeDebitoBMostrador
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case TipoComprobante.FACTURARGMIPYMES
                        iTipoComprobante = New FacturaRGMiPyMEs
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case TipoComprobante.NOTADECREDITORGMIPYMES
                        iTipoComprobante = New NotaDeCreditoRGMiPyMEs
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                    Case TipoComprobante.NOTADEDEBITORGMIPYMES
                        iTipoComprobante = New NotaDeDebitoRGMiPyMEs
                        iTipoComprobante.id = iDataReader.Item("idTipoComprobante").ToString
                End Select
                iIdNivel = FuncionComun.ceroSiEsNulo(iDataReader.Item("idNivel"))
                padre = Nothing

                iDataReader.Close()

                If iIdNivel <> Nothing Then iNivel = nivel.obtenerNivelShared(iIdNivel, iConexion)

                MyBase.accesoDatos = iConexion
                MyBase.obtenerNivel()

                Return Me
            Else
                Throw New RootException()
            End If

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Overrides Function obtenerNiveles(Optional ByVal ePadre As Nivel = Nothing) As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("distinct p.id")
            iGeneradorSql.agregarColumna("p.numero")
            iGeneradorSql.agregarColumna("p.NumeroEquivalencia")
            iGeneradorSql.agregarColumna("n.descripcion")
            iGeneradorSql.agregarColumna("t.descripcion as tipoComprobante")

            iGeneradorSql.agregarTabla("PuntoVentaDgi p")
            iGeneradorSql.agregarTabla("nivel n")
            iGeneradorSql.agregarTabla("TipoComprobante t")

            iGeneradorSql.agregarCondicionWhere("t.id=p.idTipoComprobante")
            iGeneradorSql.agregarCondicionWhere("n.id=p.id")
            If Not IsNothing(iTipoComprobante) AndAlso iTipoComprobante.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("t.id=" & iTipoComprobante.id)
            If Not IsNothing(iNivel) AndAlso iNivel.id <> Nothing Then
                Dim iNiveles As String
                If iNivel.isGrupoEmpresas Then
                    iNiveles = "select id from GrupoEmpresas where id=" & iNivel.id & " union  select id from empresaGrupo where idGrupoEmpresas=" & iNivel.id & " union select id from unidadDeNegocios where idEmpresaGrupo in (select id from empresaGrupo where idGrupoEmpresas=" & iNivel.id & ") union select id from sucursal where idUnidadDeNegocios in (select id from unidadDeNegocios where idEmpresaGrupo in (select id from empresaGrupo where idGrupoEmpresas=" & iNivel.id & "))"
                    iGeneradorSql.agregarCondicionWhere("p.idNivel in (" & iNiveles & ") or p.idNivel is null", True)
                ElseIf iNivel.isEmpresaGrupo Then
                    iNiveles = "select id from empresaGrupo where id=" & iNivel.id & " union select id from unidadDeNegocios where idEmpresaGrupo=" & iNivel.id & " union select id from sucursal where idUnidadDeNegocios in (select id from unidadDeNegocios where idEmpresaGrupo=" & iNivel.id & ")"
                    iGeneradorSql.agregarCondicionWhere("p.idNivel in (" & iNiveles & ") or p.idNivel is null", True)
                ElseIf iNivel.isUnidadDeNegocios Then
                    iNiveles = "select id from unidadDeNegocios where id=" & iNivel.id & " union select id from sucursal where idUnidadDeNegocios=" & iNivel.id
                    iGeneradorSql.agregarCondicionWhere("p.idNivel in (" & iNiveles & ") or p.idNivel is null", True)
                ElseIf iNivel.isSucursal Then
                    iGeneradorSql.agregarCondicionWhere("p.idNivel=" & iNivel.id & " or p.idNivel is null")
                End If
            End If

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Function obtenerNivelesOperativo(Optional ByVal ePadre As Nivel = Nothing) As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("distinct p.id")
            iGeneradorSql.agregarColumna("p.numero")
            iGeneradorSql.agregarColumna("p.NumeroEquivalencia")
            iGeneradorSql.agregarColumna("n.descripcion")
            iGeneradorSql.agregarColumna("t.descripcion as tipoComprobante")

            iGeneradorSql.agregarTabla("PuntoVentaDgi p")
            iGeneradorSql.agregarTabla("nivel n")
            iGeneradorSql.agregarTabla("TipoComprobante t")

            iGeneradorSql.agregarCondicionWhere("t.id=p.idTipoComprobante")
            iGeneradorSql.agregarCondicionWhere("n.id=p.id")
            If Not IsNothing(iTipoComprobante) AndAlso iTipoComprobante.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("t.id=" & iTipoComprobante.id)
            If Not IsNothing(iNivel) AndAlso iNivel.id <> Nothing Then
                Dim iNiveles As String
                If iNivel.isGrupoEmpresas Then
                ElseIf iNivel.isEmpresaGrupo Then
                    iNiveles = "select id from empresaGrupo where id=" & iNivel.id
                    iGeneradorSql.agregarCondicionWhere("p.idNivel in (" & iNiveles & ") or p.idNivel is null", True)
                ElseIf iNivel.isUnidadDeNegocios Then
                    iNiveles = "select id from unidadDeNegocios where id=" & iNivel.id & " union select id from empresaGrupo where id=" & iNivel.padre.id
                    iGeneradorSql.agregarCondicionWhere("p.idNivel in (" & iNiveles & ") or p.idNivel is null", True)
                ElseIf iNivel.isSucursal Then
                    iNiveles = "select id from sucursal where id=" & iNivel.id & " union select id from unidadDeNegocios where id=" & iNivel.padre.id & " union select id from empresaGrupo where id=" & iNivel.padre.padre.id
                    iGeneradorSql.agregarCondicionWhere("p.idNivel in (" & iNiveles & ") or p.idNivel is null", True)
                End If
            End If

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function
    Public Function obtenerNivelesDataSet() As DataSet
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("p.id")
            iGeneradorSql.agregarColumna("p.numero")
            iGeneradorSql.agregarColumna("p.NumeroEquivalencia")
            iGeneradorSql.agregarColumna("n.descripcion")
            iGeneradorSql.agregarColumna("t.descripcion as tipoComprobante")

            iGeneradorSql.agregarTabla("PuntoVentaDgi p")
            iGeneradorSql.agregarTabla("nivel n")
            iGeneradorSql.agregarTabla("TipoComprobante t")

            iGeneradorSql.agregarCondicionWhere("t.id=p.idTipoComprobante")
            iGeneradorSql.agregarCondicionWhere("n.id=p.id")
            If Not IsNothing(iTipoComprobante) AndAlso iTipoComprobante.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("t.id=" & iTipoComprobante.id)
            If iNumero <> Nothing Then iGeneradorSql.agregarCondicionWhere("p.Numero=" & iNumero)

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "PuntosVentaDgi")

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Overrides Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

            MyBase.dispose()

        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub

#End Region

End Class