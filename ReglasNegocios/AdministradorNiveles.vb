Imports di.financiera.Datos
Imports di.financiera.excepciones
Imports di.financiera.seguridad
Imports di.financiera.entidades
Imports System.Collections.Generic

Public Class AdministradorNiveles

#Region "Punto venta dgi"

    Public Function obtenerNivelesDataSet(ByVal ePuntoVentaDgi As PuntoVentaDgi) As DataSet
        Try
            Return ePuntoVentaDgi.obtenerNivelesDataSet()
        Catch RootException As RootException
            Throw RootException
        Catch exception As exception
            Throw New RootException(exception)
        End Try
    End Function

#End Region

#Region "Nivel"

    Public Function obtenerNivel(ByVal eNivel As Nivel, Optional eObtenerDomicilio As Boolean = True) As Nivel
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos()
            Return obtenerNivel(iAccesoDatos, eNivel, eObtenerDomicilio)

        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerNivel(ByVal eAccesoDatos As accesoDatos, ByVal eNivel As Nivel, Optional eObtenerDomicilio As Boolean = True) As Nivel
        Try
            eNivel.accesoDatos = eAccesoDatos
            Return eNivel.obtenerNivel(eObtenerDomicilio)
        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        Finally
            eNivel.accesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerNiveles(ByVal eNivel As Nivel, Optional ByVal ePadre As Nivel = Nothing) As IDataReader
        Try
            Return eNivel.obtenerNiveles(ePadre)
        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        Finally
        End Try
    End Function

    Public Function obtenerNivelesOperativo(ByVal ePuntoVentaDgi As PuntoVentaDgi, Optional ByVal ePadre As Nivel = Nothing) As IDataReader
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            Return obtenerNivelesOperativo(iAccesoDatos, ePuntoVentaDgi, ePadre)
        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        Finally
        End Try
    End Function

    Public Function obtenerNivelesOperativo(eAccesoDatos As accesoDatos, ByVal ePuntoVentaDgi As PuntoVentaDgi, Optional ByVal ePadre As Nivel = Nothing) As IDataReader
        Try
            Return ePuntoVentaDgi.obtenerNivelesOperativo(ePadre)
        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerNivelesLista(eAccesoDatos As accesoDatos, ByVal eNivel As Nivel, eNivelOrigen As Nivel) As List(Of Nivel)
        Try
            eNivel.accesoDatos = eAccesoDatos
            Return eNivel.obtenerNivelesLista(eNivelOrigen)
        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerIdsNivelShared(ByVal eId As Long) As Nivel
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            Return Nivel.obtenerIdsNivelShared(eId, iAccesoDatos)

        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    'Public Sub obtenerNivelesCobranza(ByVal eAccesoDatos As accesoDatos, ByVal eNivelCobranzaVO As NivelCobranzaVO)
    '    Dim iIdsSucursal As String
    '    Dim i As Integer

    '    Try

    '        With eNivelCobranzaVO
    '            For i = 0 To .entidades.Count - 1
    '                If TypeOf (.entidades.Item(i)) Is Comercio Then
    '                    .idsComercio += CType(.entidades.Item(i), Comercio).id & ","
    '                ElseIf TypeOf (.entidades.Item(i)) Is Sucursal Then
    '                    .idsSucursal += CType(.entidades.Item(i), Sucursal).id & ","
    '                Else
    '                    iIdsSucursal = .idsSucursal
    '                    If iIdsSucursal <> Nothing Then iIdsSucursal = Left(iIdsSucursal, iIdsSucursal.Length - 1)
    '                    iIdsSucursal = obtenerSucursalesNivel(eAccesoDatos, .entidades.Item(i), iIdsSucursal)
    '                    If iIdsSucursal <> Nothing Then .idsSucursal += iIdsSucursal & ","
    '                End If
    '            Next

    '            If .idsComercio <> Nothing Then .idsComercio = Left(.idsComercio, .idsComercio.Length - 1)
    '            If .idsSucursal <> Nothing Then .idsSucursal = Left(.idsSucursal, .idsSucursal.Length - 1)

    '        End With


    '    Catch excepcion As Exception
    '        Throw New NivelNoEncontradoException(excepcion)
    '    Finally
    '    End Try
    'End Sub

    Public Function obtenerIdNivelPorDescripcion(ByVal eAccesoDatos As accesoDatos, ByVal eNivel As Nivel) As Long

        Try
            eNivel.accesoDatos = eAccesoDatos
            Return eNivel.obtenerIdNivelPorDescripcion()
        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        Finally
            eNivel.accesoDatos = Nothing
        End Try
    End Function
#End Region

#Region "Sucursal"

    Public Sub crearSucursal(ByVal eSucursal As Sucursal)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            crearSucursal(iAccesoDatos, eSucursal)
            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New SucursalNoModificadaException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearSucursal(ByVal eAccesoDatos As accesoDatos, ByVal eSucursal As Sucursal)
        Dim iParametro As New Parametro
        Dim iAdministradorParametros As New AdministradorParametros

        Try
            If eSucursal.codigo = "0" Then
                iParametro = New Parametro
                iParametro.descripcion = "codigoSucursal"
                iParametro = iAdministradorParametros.obtenerParametroIncremental(eAccesoDatos, iParametro, eSucursal.padre)
                eSucursal.codigo = iParametro.valor
                iParametro = Nothing
            End If


            eSucursal.accesoDatos = eAccesoDatos
            eSucursal.crear()
            eSucursal.accesoDatos = Nothing

            iAdministradorParametros.crearParametrosDesdeParametroBase(eAccesoDatos, eSucursal)

        Catch exception As Exception
            Throw New SucursalNoCreadaException(exception)
        Finally
            iParametro = Nothing
        End Try
    End Sub

    Public Function obtenerSucursalesGrilla(ByVal eSucursal As Sucursal, ByVal eNivel As Nivel) As DataSet
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            Return eSucursal.obtenerSucursalesGrilla(eNivel)
        Catch exception As Exception
            Throw New SucursalNoEncontradaException(exception)
        Finally
            If Not IsNothing(iAccesoDatos) Then iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Function

    Public Function obtenerSucursalesGrilla(eAccesoDatos As accesoDatos, ByVal eSucursal As Sucursal, ByVal eNivel As Nivel) As DataSet
        Try
            eSucursal.accesoDatos = eAccesoDatos
            Return eSucursal.obtenerSucursalesGrilla(eNivel)
        Catch exception As Exception
            Throw New SucursalNoEncontradaException(exception)
        Finally
            eSucursal.accesoDatos = Nothing
        End Try
    End Function

    Public Sub modificarSucursal(ByVal eSucursal As Sucursal)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eSucursal.accesoDatos = iAccesoDatos
            eSucursal.modificar()
            eSucursal.accesoDatos = Nothing
            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New SucursalNoModificadaException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarSucursal(ByVal eSucursal As Sucursal)
        Dim iAccesoDatos As accesoDatos
        'Dim iAdministradorTablasMantenimientoCajas As New AdministradorTablasMantenimientoCajas
        Dim iAdministradorParametros As New AdministradorParametros
        Dim iParametro As New Parametro
        Dim iUsuario As New seguridad.Usuario
        Dim iAdministradorUsuarios As New AdministradorUsuarios
        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            'elimino los parametros
            iAdministradorParametros.eliminarParametro(iAccesoDatos, iParametro, eSucursal)
            iAdministradorParametros.eliminarParametroOperaciones(iAccesoDatos, iParametro, eSucursal)

            eSucursal.accesoDatos = iAccesoDatos
            eSucursal.eliminar()

            eSucursal.accesoDatos = Nothing
            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New SucursalNoEliminadaException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerSucursales(ByVal eSucursal As Sucursal, Optional ByVal ePadre As Nivel = Nothing, Optional ByVal eObtenerSucursalCargaLegajo As Boolean = False) As IDataReader
        Try
            Return eSucursal.obtenerSucursales(ePadre, eObtenerSucursalCargaLegajo)
        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerSucursalesNivel(ByVal eAccesoDatos As accesoDatos, ByVal eNivel As Nivel, ByVal eIdsSucursal As String) As String
        Dim iSucursal As New Sucursal
        Try
            iSucursal.accesoDatos = eAccesoDatos
            Return iSucursal.obtenerSucursalesNivel(eNivel, eIdsSucursal)
        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        Finally
            iSucursal.accesoDatos = Nothing
            iSucursal = Nothing
        End Try
    End Function

#End Region

#Region "Unidad de Negocios"

    Public Sub crearUnidadDeNegocios(eUnidadDeNegocios As UnidadDeNegocios)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()

            crearUnidadDeNegocios(iAccesoDatos, eUnidadDeNegocios)

            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearUnidadDeNegocios(eAccesoDatos As accesoDatos, eUnidadDeNegocios As UnidadDeNegocios)
        Dim iAdministradorParametros As New AdministradorParametros

        Try

            eUnidadDeNegocios.accesoDatos = eAccesoDatos
            eUnidadDeNegocios.crear()
            eUnidadDeNegocios.accesoDatos = Nothing

            iAdministradorParametros.crearParametrosDesdeParametroBase(eAccesoDatos, eUnidadDeNegocios)

        Catch exception As Exception
            Throw New UnidadDeNegociosNoCreadaException(exception)
        Finally
            eUnidadDeNegocios.accesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerUnidadesDeNegociosGrilla(ByVal eUnidadDeNegocios As UnidadDeNegocios, ByVal eNivel As Nivel) As DataSet
        Try
            Return eUnidadDeNegocios.obtenerUnidadesDeNegociosGrilla(eNivel)
        Catch exception As Exception
            Throw New UnidadDeNegociosNoEncontradaException(exception)
        End Try
    End Function

    Public Sub modificarUnidadDeNegocios(ByVal eUnidadDeNegocios As UnidadDeNegocios)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eUnidadDeNegocios.accesoDatos = iAccesoDatos
            eUnidadDeNegocios.modificar()
            eUnidadDeNegocios.accesoDatos = Nothing
            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New UnidadDeNegociosNoModificadaException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarUnidadDeNegocios(ByVal eUnidadDeNegocios As UnidadDeNegocios)
        Dim iAccesoDatos As accesoDatos
        Dim iAdministradorParametros As New AdministradorParametros
        Dim iParametro As New Parametro

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            'elimino los parametros
            iAdministradorParametros.eliminarParametro(iAccesoDatos, iParametro, eUnidadDeNegocios)
            'elimino los parametros operaciones
            iParametro.nombreTabla = "parametroOperaciones"
            iAdministradorParametros.eliminarParametro(iAccesoDatos, iParametro, eUnidadDeNegocios)
            'elimino los parametros liquidacion
            iParametro.nombreTabla = "parametroLiquidacion"
            iAdministradorParametros.eliminarParametro(iAccesoDatos, iParametro, eUnidadDeNegocios)
            'elimino los parametros facturacion
            iParametro.nombreTabla = "parametroFacturacion"
            iAdministradorParametros.eliminarParametro(iAccesoDatos, iParametro, eUnidadDeNegocios)
            'elimino los parametros estudio
            iParametro.nombreTabla = "parametroEstudio"
            iAdministradorParametros.eliminarParametro(iAccesoDatos, iParametro, eUnidadDeNegocios)
            'elimino los parametros estudio
            iParametro.nombreTabla = "parametrotexto"
            iAdministradorParametros.eliminarParametro(iAccesoDatos, iParametro, eUnidadDeNegocios)

            eUnidadDeNegocios.accesoDatos = iAccesoDatos
            eUnidadDeNegocios.eliminar()
            eUnidadDeNegocios.accesoDatos = Nothing
            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New UnidadDeNegociosNoEliminadaException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub
#End Region

#Region "Empresa Grupo"
    Public Function obtenerEmpresasGrupoGrilla(ByVal eEmpresaGrupo As EmpresaGrupo, ByVal eNivel As Nivel) As DataSet
        Try
            Return eEmpresaGrupo.obtenerEmpresasGrupoGrilla(eNivel)
        Catch exception As Exception
            Throw New EmpresaGrupoNoEncontradaException(exception)
        End Try
    End Function
    Public Sub crearEmpresaGrupo(ByVal eEmpresaGrupo As EmpresaGrupo)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()

            crearEmpresaGrupo(iAccesoDatos, eEmpresaGrupo)

            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearEmpresaGrupo(eAccesoDatos As accesoDatos, ByVal eEmpresaGrupo As EmpresaGrupo)
        Dim iAdministradorParametros As New AdministradorParametros

        Try

            eEmpresaGrupo.accesoDatos = eAccesoDatos
            eEmpresaGrupo.crear()
            eEmpresaGrupo.accesoDatos = Nothing

            iAdministradorParametros.crearParametrosDesdeParametroBase(eAccesoDatos, eEmpresaGrupo)

        Catch exception As Exception
            Throw New EmpresaGrupoNoCreadaException(exception)
        Finally
            iAdministradorParametros = Nothing
        End Try
    End Sub

    Public Sub modificarEmpresaGrupo(ByVal eEmpresaGrupo As EmpresaGrupo)
        Dim iAccesoDatos As accesoDatos
        Try

            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eEmpresaGrupo.accesoDatos = iAccesoDatos
            eEmpresaGrupo.modificar()
            eEmpresaGrupo.accesoDatos = Nothing
            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New EmpresaGrupoNoModificadaException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub eliminarEmpresaGrupo(ByVal eEmpresaGrupo As EmpresaGrupo)
        Dim iAccesoDatos As accesoDatos
        Dim iAdministradorParametros As New AdministradorParametros
        Dim iParametro As New Parametro

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            'elimino los parametros
            iAdministradorParametros.eliminarParametro(iAccesoDatos, iParametro, eEmpresaGrupo)

            eEmpresaGrupo.accesoDatos = iAccesoDatos
            eEmpresaGrupo.eliminar()
            eEmpresaGrupo.accesoDatos = Nothing
            iAccesoDatos.commit()

        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New EmpresaGrupoNoEliminadaException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub
#End Region

#Region "Segmento"

    Public Function obtenerSegmentos(ByVal eSegmento As Segmento) As IDataReader
        Try

            Return eSegmento.obtenerSegmentos()

        Catch exception As Exception
            Throw New SegmentoNoEncontradoException(exception)
        End Try
    End Function

    Public Function obtenerSegmento(ByVal eSegmento As Segmento) As Segmento
        Try

            Return eSegmento.obtenerSegmento()
        Catch exception As Exception
            Throw New NivelNoEncontradoException(exception)
        Finally

        End Try
    End Function

    Public Sub crearSegmento(ByVal eSegmento As Segmento)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            crearSegmento(iAccesoDatos, eSegmento)
            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New SucursalNoModificadaException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub crearSegmento(ByVal eAccesoDatos As accesoDatos, ByVal eSegmento As Segmento)

        Try

            eSegmento.accesoDatos = eAccesoDatos
            eSegmento.crear()

        Catch Exception As Exception
            Throw New SegmentoNoModificadoException(Exception)
        Finally
            eSegmento.accesoDatos = Nothing
        End Try
    End Sub

    Public Function obtenerSegmentoGrilla(ByVal eSegmento As Segmento) As DataSet
        Try
            Return eSegmento.obtenerSegmentoGrilla
        Catch Exception As Exception
            Throw New EntidadfinancieraNoCreadaException(Exception)
        End Try
    End Function

    Public Sub eliminarSegmento(ByVal eSegmento As Segmento)
        Dim iAccesoDatos As accesoDatos

        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()

            eSegmento.accesoDatos = iAccesoDatos
            eSegmento.eliminar()

            eSegmento.accesoDatos = Nothing
            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New SegmentoNoEliminadoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub

    Public Sub modificarSegmento(ByVal eSegmento As Segmento)
        Dim iAccesoDatos As accesoDatos
        Try
            iAccesoDatos = New accesoDatos
            iAccesoDatos.beginTransaction()
            eSegmento.accesoDatos = iAccesoDatos
            eSegmento.modificar()
            eSegmento.accesoDatos = Nothing
            iAccesoDatos.commit()
        Catch exception As Exception
            iAccesoDatos.rollback()
            Throw New SegmentoNoModificadoException(exception)
        Finally
            iAccesoDatos.cerrar()
            iAccesoDatos = Nothing
        End Try
    End Sub
#End Region

End Class
