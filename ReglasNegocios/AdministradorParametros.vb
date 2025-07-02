Imports di.financiera.reglasnegocios
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.seguridad
Imports di.financiera.datos
Imports System.Collections.Generic

Public Class AdministradorParametros

#Region "Parametros"

    Public Sub obtenerParametrosNivel(ByRef eParametroVO As ParametroVO)
        Dim iAccesoDatos As accesoDatos
        Dim iParametro As New Parametro
        Dim iDataSet As DataSet
        Dim iDataTable As DataTable
        Dim iDataRow As DataRow
        Dim i As Integer

        Try

            iAccesoDatos = New accesoDatos

            With iParametro
                .accesoDatos = iAccesoDatos
                .obtenerParametrosNivel(eParametroVO)
            End With
            iParametro = Nothing

            iParametro = eParametroVO.parametro.Find(Function(Parametro) Parametro.descripcion = "diasDeVencimiento")
            If Not IsNothing(iParametro) Then

                eParametroVO.dataSet = New DataSet
                iDataTable = New DataTable
                iDataTable.Columns.Add(New DataColumn("Id", GetType(Long)))
                iDataTable.Columns.Add(New DataColumn("Valor", GetType(String)))
                iDataTable.Columns.Add(New DataColumn("idTipoDato", GetType(Long)))

                iDataSet = obtenerParametros(iAccesoDatos, iParametro, eParametroVO.nivel)

                For i = 0 To iDataSet.Tables("Parametro").Rows.Count - 1
                    iDataRow = iDataTable.NewRow
                    iDataRow.Item("Id") = iDataSet.Tables("Parametro").Rows(i).Item("Id")
                    iDataRow.Item("Valor") = iDataSet.Tables("Parametro").Rows(i).Item("Valor")
                    iDataRow.Item("idTipoDato") = iDataSet.Tables("Parametro").Rows(i).Item("idTipoDato")

                    iDataTable.Rows.Add(iDataRow)
                Next

                iDataTable.TableName = "diasDeVencimiento"

                eParametroVO.dataSet.Tables.Add(iDataTable)
                iDataTable = Nothing
                iDataSet = Nothing
            End If
            iParametro = Nothing

            iParametro = eParametroVO.parametro.Find(Function(Parametro) Parametro.descripcion = "ValorCupones")
            If Not IsNothing(iParametro) Then

                If IsNothing(eParametroVO.dataSet) Then eParametroVO.dataSet = New DataSet
                iDataTable = New DataTable
                iDataTable.Columns.Add(New DataColumn("Id", GetType(Long)))
                iDataTable.Columns.Add(New DataColumn("Valor", GetType(String)))
                iDataTable.Columns.Add(New DataColumn("idTipoDato", GetType(Long)))

                iDataSet = obtenerParametros(iAccesoDatos, iParametro, eParametroVO.nivel)

                For i = 0 To iDataSet.Tables("Parametro").Rows.Count - 1
                    iDataRow = iDataTable.NewRow
                    iDataRow.Item("Id") = iDataSet.Tables("Parametro").Rows(i).Item("Id")
                    iDataRow.Item("Valor") = iDataSet.Tables("Parametro").Rows(i).Item("Valor")
                    iDataRow.Item("idTipoDato") = iDataSet.Tables("Parametro").Rows(i).Item("idTipoDato")

                    iDataTable.Rows.Add(iDataRow)
                Next

                iDataTable.TableName = "ValorCupones"

                eParametroVO.dataSet.Tables.Add(iDataTable)
                iDataTable = Nothing
                iDataSet = Nothing
            End If
            iParametro = Nothing

            iParametro = eParametroVO.parametro.Find(Function(Parametro) Parametro.descripcion = "PromocionCumplimientoCliente")
            If Not IsNothing(iParametro) Then

                If IsNothing(eParametroVO.dataSet) Then eParametroVO.dataSet = New DataSet
                iDataTable = New DataTable
                iDataTable.Columns.Add(New DataColumn("Id", GetType(Long)))
                iDataTable.Columns.Add(New DataColumn("Valor", GetType(String)))
                iDataTable.Columns.Add(New DataColumn("idTipoDato", GetType(Long)))

                iDataSet = obtenerParametros(iAccesoDatos, iParametro, eParametroVO.nivel)

                For i = 0 To iDataSet.Tables("Parametro").Rows.Count - 1
                    iDataRow = iDataTable.NewRow
                    iDataRow.Item("Id") = iDataSet.Tables("Parametro").Rows(i).Item("Id")
                    iDataRow.Item("Valor") = iDataSet.Tables("Parametro").Rows(i).Item("Valor")
                    iDataRow.Item("idTipoDato") = iDataSet.Tables("Parametro").Rows(i).Item("idTipoDato")

                    iDataTable.Rows.Add(iDataRow)
                Next

                iDataTable.TableName = "PromocionCumplimientoCliente"

                eParametroVO.dataSet.Tables.Add(iDataTable)
                iDataTable = Nothing
                iDataSet = Nothing
            End If

            iParametro = Nothing

            iParametro = eParametroVO.parametro.Find(Function(Parametro) Parametro.descripcion = "MedioPagoBiba")
            If Not IsNothing(iParametro) Then

                If IsNothing(eParametroVO.dataSet) Then eParametroVO.dataSet = New DataSet
                iDataTable = New DataTable
                iDataTable.Columns.Add(New DataColumn("Id", GetType(Long)))
                iDataTable.Columns.Add(New DataColumn("Valor", GetType(String)))
                iDataTable.Columns.Add(New DataColumn("idTipoDato", GetType(Long)))

                iDataSet = obtenerParametros(iAccesoDatos, iParametro, eParametroVO.nivel)

                For i = 0 To iDataSet.Tables("Parametro").Rows.Count - 1
                    iDataRow = iDataTable.NewRow
                    iDataRow.Item("Id") = iDataSet.Tables("Parametro").Rows(i).Item("Id")
                    iDataRow.Item("Valor") = iDataSet.Tables("Parametro").Rows(i).Item("Valor")
                    iDataRow.Item("idTipoDato") = iDataSet.Tables("Parametro").Rows(i).Item("idTipoDato")

                    iDataTable.Rows.Add(iDataRow)
                Next

                iDataTable.TableName = "MedioPagoBiba"

                eParametroVO.dataSet.Tables.Add(iDataTable)
                iDataTable = Nothing
                iDataSet = Nothing
            End If

            iParametro = Nothing

            iParametro = eParametroVO.parametro.Find(Function(Parametro) Parametro.descripcion = "MedioPagoCobranzaSupernova")
            If Not IsNothing(iParametro) Then

                If IsNothing(eParametroVO.dataSet) Then eParametroVO.dataSet = New DataSet
                iDataTable = New DataTable
                iDataTable.Columns.Add(New DataColumn("Id", GetType(Long)))
                iDataTable.Columns.Add(New DataColumn("Valor", GetType(String)))
                iDataTable.Columns.Add(New DataColumn("idTipoDato", GetType(Long)))

                iDataSet = obtenerParametros(iAccesoDatos, iParametro, eParametroVO.nivel)

                For i = 0 To iDataSet.Tables("Parametro").Rows.Count - 1
                    iDataRow = iDataTable.NewRow
                    iDataRow.Item("Id") = iDataSet.Tables("Parametro").Rows(i).Item("Id")
                    iDataRow.Item("Valor") = iDataSet.Tables("Parametro").Rows(i).Item("Valor")
                    iDataRow.Item("idTipoDato") = iDataSet.Tables("Parametro").Rows(i).Item("idTipoDato")

                    iDataTable.Rows.Add(iDataRow)
                Next

                iDataTable.TableName = "MedioPagoCobranzaSupernova"

                eParametroVO.dataSet.Tables.Add(iDataTable)
                iDataTable = Nothing
                iDataSet = Nothing
            End If

            iParametro = Nothing

            iParametro = eParametroVO.parametro.Find(Function(Parametro) Parametro.descripcion = "MedioPagoSucursales")
            If Not IsNothing(iParametro) Then

                If IsNothing(eParametroVO.dataSet) Then eParametroVO.dataSet = New DataSet
                iDataTable = New DataTable
                iDataTable.Columns.Add(New DataColumn("Id", GetType(Long)))
                iDataTable.Columns.Add(New DataColumn("Valor", GetType(String)))
                iDataTable.Columns.Add(New DataColumn("idTipoDato", GetType(Long)))

                iDataSet = obtenerParametros(iAccesoDatos, iParametro, eParametroVO.nivel)

                For i = 0 To iDataSet.Tables("Parametro").Rows.Count - 1
                    iDataRow = iDataTable.NewRow
                    iDataRow.Item("Id") = iDataSet.Tables("Parametro").Rows(i).Item("Id")
                    iDataRow.Item("Valor") = iDataSet.Tables("Parametro").Rows(i).Item("Valor")
                    iDataRow.Item("idTipoDato") = iDataSet.Tables("Parametro").Rows(i).Item("idTipoDato")

                    iDataTable.Rows.Add(iDataRow)
                Next

                iDataTable.TableName = "MedioPagoSucursales"

                eParametroVO.dataSet.Tables.Add(iDataTable)
                iDataTable = Nothing
                iDataSet = Nothing
            End If

            iParametro = Nothing

        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
            iParametro = Nothing
            iDataSet = Nothing
        End Try
    End Sub

    Public Sub actualizarParametrosNivel(ByVal eParametroVO As ParametroVO)
        Dim iAccesoDatos As accesoDatos

        Try

            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()

            actualizarParametrosNivel(iAccesoDatos, eParametroVO)

            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw exception
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub actualizarParametrosNivelLista(ByVal eListaParametroVO As List(Of ParametroVO))
        Dim iAccesoDatos As accesoDatos

        Try

            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()

            For Each iParametroVO As ParametroVO In eListaParametroVO
                actualizarParametrosNivel(iAccesoDatos, iParametroVO)
            Next

            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw exception
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Private Sub actualizarParametrosNivel(ByVal eAccesoDatos As accesoDatos, eParametro As Parametro, eNivel As Nivel)
        Dim iAdministradorParametros As New AdministradorParametros()
        Try
            actualizarParametro(eAccesoDatos, eParametro, eNivel)
        Catch ex As Exception
            eParametro.tipoDato = New DatoString
            crearParametro(eAccesoDatos, eParametro, eNivel)
        End Try
    End Sub

    Public Sub actualizarParametrosNivel(ByVal eAccesoDatos As accesoDatos, ByVal eParametroVO As ParametroVO)
        Dim i As Integer

        Try
            With eParametroVO

                'Elimino los parametros
                If Not IsNothing(.parametroEliminar) Then
                    For i = 0 To .parametroEliminar.Count - 1
                        eliminarParametro(eAccesoDatos, CType(.parametroEliminar.Item(i), Parametro), .nivel)
                    Next
                End If

                'Creo los parametros
                If Not IsNothing(.parametroCrear) Then
                    For i = 0 To .parametroCrear.Count - 1
                        crearParametro(eAccesoDatos, CType(.parametroCrear.Item(i), Parametro), .nivel)
                    Next
                End If

                'Modifico los parametros
                If Not IsNothing(.parametro) Then
                    For i = 0 To .parametro.Count - 1
                        actualizarParametrosNivel(eAccesoDatos, CType(.parametro.Item(i), Parametro), .nivel)
                    Next
                End If

            End With

        Catch ParametroNoActualizadoException As ParametroNoActualizadoException
            Throw ParametroNoActualizadoException
        Catch ParametroNoEliminadoException As ParametroNoEliminadoException
            Throw ParametroNoEliminadoException
        Catch ParametroNoEncontradoException As ParametroNoEncontradoException
            Throw ParametroNoEncontradoException
        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        End Try
    End Sub

    Public Function obtenerParametro(ByVal eParametro As Parametro, ByVal eNivel As Nivel) As Parametro
        Try
            Return eParametro.obtenerParametro(eNivel)
        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerParametroDiasDeVencimeinto(ByVal eAccesoDatos As accesoDatos, ByVal eParametro As Parametro, ByVal eNivel As Nivel, ByVal eFechaProceso As Date) As Date
        Try
            eParametro.accesoDatos = eAccesoDatos
            Return eParametro.obtenerParametroDiasDeVencimeinto(eNivel, eFechaProceso)
        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerParametro(ByVal eAccesoDatos As accesoDatos, ByVal eParametro As Parametro, ByVal eNivel As Nivel) As Parametro
        Try
            eParametro.accesoDatos = eAccesoDatos
            Return obtenerParametro(eParametro, eNivel)
        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerParametroIncremental(ByVal eParametro As Parametro, ByVal eNivel As Nivel, Optional ByVal eCreaNuevaConexion As Boolean = True) As Parametro
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos

            Return obtenerParametroIncremental(iAccesoDatos, eParametro, eNivel, eCreaNuevaConexion)

        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerParametroIncremental(ByVal eAccesoDatos As accesoDatos, ByVal eParametro As Parametro, ByVal eNivel As Nivel, Optional ByVal eCreaNuevaConexion As Boolean = True) As Parametro
        Try
            eParametro.accesoDatos = eAccesoDatos
            Return eParametro.obtenerParametroIncremental(eNivel, eCreaNuevaConexion)
        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        End Try
    End Function

    'Public Function obtenerParametroNumeroCredito(ByVal eAccesoDatos As accesoDatos, ByVal eComercio As Comercio) As Long
    '    Dim iParametro As New Parametro
    'Dim iParametroComercio As ParametroComercio
    '    Try
    '        iParametro.accesoDatos = eAccesoDatos
    '        iParametro.descripcion = "nivelParametroNumeroCredito"
    '        iParametro = obtenerParametro(eAccesoDatos, iParametro, eComercio.sucursal)
    '        iParametro.accesoDatos = Nothing
    '        Select Case CInt(iParametro.valor)
    '            Case FuncionComun.enumNivelParametroNumeroCredito.GRUPOEMPRESA
    '                iParametro.accesoDatos = eAccesoDatos
    '                iParametro.descripcion = "NumeroCredito"
    '                iParametro.nombreTabla = "parametroOperaciones"
    '                Return iParametro.obtenerParametroIncremental(eComercio.sucursal.padre.padre.padre).valor
    '            Case FuncionComun.enumNivelParametroNumeroCredito.EMPRESAGRUPO
    '                iParametro.accesoDatos = eAccesoDatos
    '                iParametro.descripcion = "NumeroCredito"
    '                iParametro.nombreTabla = "parametroOperaciones"
    '                Return iParametro.obtenerParametroIncremental(eComercio.sucursal.padre.padre).valor
    '            Case FuncionComun.enumNivelParametroNumeroCredito.UNIDADDENEGOCIOS
    '                iParametro.accesoDatos = eAccesoDatos
    '                iParametro.descripcion = "NumeroCredito"
    '                iParametro.nombreTabla = "parametroOperaciones"
    '                Return iParametro.obtenerParametroIncremental(eComercio.sucursal.padre).valor
    '            Case FuncionComun.enumNivelParametroNumeroCredito.SUCURSAL
    '                iParametro.accesoDatos = eAccesoDatos
    '                iParametro.descripcion = "NumeroCredito"
    '                iParametro.nombreTabla = "parametroOperaciones"
    '                Return iParametro.obtenerParametroIncremental(eComercio.sucursal).valor
    '        '    Case FuncionComun.enumNivelParametroNumeroCredito.COMERCIO
    '        '        iParametroComercio = New ParametroComercio
    '        '        iParametroComercio.accesoDatos = eAccesoDatos
    '        '        iParametroComercio.descripcion = "NumeroCredito"
    '        '        Return iParametroComercio.obtenerParametroComercioIncremental(eComercio).valor
    '        'End Select
    '    Catch exception As Exception
    '        Throw New ParametroNoEncontradoException(exception)
    '    End Try
    'End Function

    Public Sub actualizarParametro(ByVal eAccesoDatos As accesoDatos, ByVal eParametro As Parametro, ByVal eNivel As Nivel)
        Try
            eParametro.accesoDatos = eAccesoDatos
            eParametro.actualizar(eNivel)
        Catch exception As Exception
            Throw New ParametroNoActualizadoException(exception)
        Finally
            eParametro.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub actualizarParametro(ByVal eParametro As Parametro, ByVal eNivel As Nivel)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            actualizarParametro(iAccesoDatos, eParametro, eNivel)
        Catch exception As Exception
            Throw New ParametroNoActualizadoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerParametros(ByVal eParametro As Parametro, ByVal eNivel As Nivel) As DataSet
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            Return eParametro.obtenerParametros(eNivel)
        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerParametros(ByVal eAccesoDatos As accesoDatos, ByVal eParametro As Parametro, ByVal eNivel As Nivel) As DataSet
        Try
            eParametro.accesoDatos = eAccesoDatos
            Return eParametro.obtenerParametros(eNivel)
        Catch exception As Exception
            Throw New ParametroNoEncontradoException(exception)
        End Try
    End Function

    Public Sub crearParametro(ByVal eParametro As Parametro, ByVal eNivel As Nivel)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            eParametro.accesoDatos = iAccesoDatos
            eParametro.crear(eNivel)
        Catch exception As Exception
            Throw New ParametroNoCreadoException(exception)
        Finally
            eParametro.accesoDatos = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearParametro(ByVal eAccesoDatos As accesoDatos, ByVal eParametro As Parametro, ByVal eNivel As Nivel)
        Try
            eParametro.accesoDatos = eAccesoDatos
            eParametro.crear(eNivel)
        Catch exception As Exception
            Throw New ParametroNoCreadoException(exception)
        Finally
            eParametro.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarParametro(ByVal eParametro As Parametro, ByVal eNivel As Nivel)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            eParametro.accesoDatos = iAccesoDatos
            eParametro.eliminar(eNivel)
        Catch exception As Exception
            Throw New ParametroNoEliminadoException(exception)
        Finally
            eParametro.accesoDatos = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarParametroOperaciones(ByVal eAccesoDatos As accesoDatos, ByVal eParametro As Parametro, ByVal eNivel As Nivel)
        Try
            eParametro.accesoDatos = eAccesoDatos
            eParametro.eliminarParametroOperaciones(eNivel)
        Catch exception As Exception
            Throw New ParametroNoEliminadoException(exception)
        Finally
            eParametro.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarParametroOperaciones(ByVal eParametro As Parametro, ByVal eNivel As Nivel)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            eParametro.accesoDatos = iAccesoDatos
            eParametro.eliminarParametroOperaciones(eNivel)
        Catch exception As Exception
            Throw New ParametroNoEliminadoException(exception)
        Finally
            eParametro.accesoDatos = Nothing
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarParametro(ByVal eAccesoDatos As accesoDatos, ByVal eParametro As Parametro, ByVal eNivel As Nivel)
        Try
            eParametro.accesoDatos = eAccesoDatos
            eParametro.eliminar(eNivel)
        Catch exception As Exception
            Throw New ParametroNoEliminadoException(exception)
        Finally
            eParametro.accesoDatos = Nothing
        End Try
    End Sub

    Public Sub actualizarFechaProceso(ByVal eParametro As Parametro, ByVal eNivel As Nivel)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eParametro.accesoDatos = iAccesoDatos
            eParametro.actualizarFechaProceso(eNivel)
            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New ParametroNoActualizadoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearParametrosDesdeParametroBase(ByVal eAccesoDatos As accesoDatos, ByVal eNivel As Nivel)
        Dim iParametro As New Parametro
        Try
            iParametro.accesoDatos = eAccesoDatos
            iParametro.crearDesdeParametroBase(eNivel)
        Catch exception As Exception
            Throw New ParametroNoCreadoException(exception)
        Finally
            iParametro = Nothing
        End Try
    End Sub

#End Region

End Class
