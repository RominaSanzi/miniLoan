Imports di.financiera.datos
Imports di.financiera.utils
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.seguridad
Imports System.Collections.Generic
Public Class NivelSingleton

#Region "Variables"

    Private Shared iFechaHora As Date
    Private Shared iListaNiveles As List(Of Nivel)
    Private Shared iDataSet As DataSet
    Private Shared iForzarActualizacion As Boolean

#End Region


#Region "Atributos"
    Public Property fechaHora() As Date
        Get
            Return iFechaHora
        End Get
        Set(ByVal Value As Date)
            iFechaHora = Value
        End Set
    End Property

    Public Property listaNiveles() As List(Of Nivel)
        Get
            Return iListaNiveles
        End Get
        Set(ByVal Value As List(Of Nivel))
            iListaNiveles = Value
        End Set
    End Property

    Public Shared Property forzarActualizacion() As Boolean
        Get
            Return iForzarActualizacion
        End Get
        Set(ByVal Value As Boolean)
            iForzarActualizacion = Value
        End Set
    End Property

    Public Property dataSet As DataSet
        Get
            Return iDataSet
        End Get
        Set(value As DataSet)
            iDataSet = value
        End Set
    End Property

#End Region

    Private Shared iInstancia As NivelSingleton
    Private Shared iMutex As New System.Threading.Mutex()

    Public Shared Function getInstancia(eActualizar As Boolean) As NivelSingleton
        Try
            iMutex.WaitOne()
            If iInstancia Is Nothing OrElse DateDiff(DateInterval.Hour, iFechaHora, Now) > 24 OrElse eActualizar OrElse iForzarActualizacion Then
                iInstancia = New NivelSingleton
            End If

            Return iInstancia
        Catch exception As Exception
            'Agarramos las excepcines para que no se pare la cola de tareas por un error
        Finally
            iMutex.ReleaseMutex()
        End Try
    End Function

    Public Sub New()
        Try
            obtenerNiveles()

        Catch exception As Exception
            'Agarramos las excepcines para que no se pare la cola de tareas por un error
        End Try
    End Sub


    Private Shared Sub obtenerNiveles()
        Dim iGeneradorSql As New GeneradorSql
        Dim iConexion As accesoDatos
        Dim iDataSetNiveles As DataSet
        Dim iNivel As Nivel
        Try
            iForzarActualizacion = False

            iConexion = New accesoDatos

            iListaNiveles = New List(Of Nivel)

            iGeneradorSql.agregarColumna("s.id")
            iGeneradorSql.agregarColumna("s.codigo")
            iGeneradorSql.agregarColumna("s.idDomicilio")
            iGeneradorSql.agregarColumna("s.idEstado")
            iGeneradorSql.agregarColumna("s.idUnidadDeNegocios")
            iGeneradorSql.agregarColumna("s.tipoSucursal")
            iGeneradorSql.agregarColumna("s.codigoTarjeta")
            iGeneradorSql.agregarColumna("n.descripcion as nivelDescripcion")
            iGeneradorSql.agregarColumna("dom.id as idDomicilio")
            iGeneradorSql.agregarColumna("dom.calle")
            iGeneradorSql.agregarColumna("dom.numero")
            iGeneradorSql.agregarColumna("dom.piso")
            iGeneradorSql.agregarColumna("dom.entre")
            iGeneradorSql.agregarColumna("dom.idbarrio")
            iGeneradorSql.agregarColumna("dom.codigoPostal")
            iGeneradorSql.agregarColumna("dom.planoFilcar")
            iGeneradorSql.agregarColumna("dom.coordenadaFilcar")
            iGeneradorSql.agregarColumna("dom.idLocalidad")
            iGeneradorSql.agregarColumna("dom.LocalidadAnterior")
            iGeneradorSql.agregarColumna("dom.telefonoCodigoArea")
            iGeneradorSql.agregarColumna("dom.telefonoCaracteristica")
            iGeneradorSql.agregarColumna("dom.telefonoNumero")
            iGeneradorSql.agregarColumna("dom.telefono")
            iGeneradorSql.agregarColumna("dom.TelefonoReferenciaCodigoArea")
            iGeneradorSql.agregarColumna("dom.TelefonoReferenciaCaracteristica")
            iGeneradorSql.agregarColumna("dom.TelefonoReferenciaNumero")
            iGeneradorSql.agregarColumna("dom.TelefonoReferencia")
            iGeneradorSql.agregarColumna("dom.TelefonoCelularCodigoArea")
            iGeneradorSql.agregarColumna("dom.TelefonoCelularCaracteristica")
            iGeneradorSql.agregarColumna("dom.TelefonoCelularNumero")
            iGeneradorSql.agregarColumna("dom.TelefonoCelular")
            iGeneradorSql.agregarColumna("dom.idcalleBase")
            iGeneradorSql.agregarColumna("dom.departamento")
            iGeneradorSql.agregarColumna("dom.manzana")
            iGeneradorSql.agregarColumna("dom.edificio")
            iGeneradorSql.agregarColumna("dom.monoblock")
            iGeneradorSql.agregarColumna("dom.escaleras")
            iGeneradorSql.agregarColumna("dom.pasillo")
            iGeneradorSql.agregarColumna("dom.casa")
            iGeneradorSql.agregarColumna("dom.lote")
            iGeneradorSql.agregarColumna("dom.idVivienda")

            iGeneradorSql.agregarTabla("Nivel n")
            iGeneradorSql.agregarTabla("sucursal s")
            iGeneradorSql.agregarTabla("domicilio dom")
            iGeneradorSql.agregarCondicionWhere("n.id=s.id")
            iGeneradorSql.agregarCondicionWhere("dom.id=s.iddomicilio")


            iDataSetNiveles = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Sucursal")

            For Each iDataRow As DataRow In iDataSetNiveles.Tables("Sucursal").Rows
                iNivel = New Sucursal
                With CType(iNivel, Sucursal)
                    .id = iDataRow.Item("id").ToString
                    .codigo = iDataRow.Item("codigo").ToString
                    .descripcion = iDataRow.Item("nivelDescripcion").ToString
                    .domicilio = New Domicilio
                    .domicilio.id = iDataRow.Item("idDomicilio").ToString
                    .padre = New UnidadDeNegocios
                    .padre.id = iDataRow.Item("idUnidadDeNegocios").ToString
                    .estado = IIf(iDataRow.Item("idEstado").ToString = Estado.ALTA, New Alta, New Baja)
                    .tipoSucursal = iDataRow.Item("tipoSucursal")
                    .codigoTarjeta = iDataRow.Item("codigoTarjeta").ToString
                    With .domicilio
                        .calle = FuncionComun.vacioSiEsNulo(iDataRow.Item("calle").ToString.Replace("%", "'"))
                        .numero = FuncionComun.vacioSiEsNulo(iDataRow.Item("numero").ToString)
                    End With
                End With

                iListaNiveles.Add(iNivel)
                iNivel = Nothing
            Next

            iGeneradorSql.agregarColumna("u.id")
            iGeneradorSql.agregarColumna("u.idDomicilio")
            iGeneradorSql.agregarColumna("u.idEmpresaGrupo")
            iGeneradorSql.agregarColumna("u.idTipoIva")
            iGeneradorSql.agregarColumna("u.numeroCuit")
            iGeneradorSql.agregarColumna("u.numeroIB")
            iGeneradorSql.agregarColumna("n.descripcion as nivelDescripcion")
            iGeneradorSql.agregarColumna("dom.id as idDomicilio")
            iGeneradorSql.agregarColumna("dom.calle")
            iGeneradorSql.agregarColumna("dom.numero")
            iGeneradorSql.agregarColumna("dom.piso")
            iGeneradorSql.agregarColumna("dom.entre")
            iGeneradorSql.agregarColumna("dom.idbarrio")
            iGeneradorSql.agregarColumna("dom.codigoPostal")
            iGeneradorSql.agregarColumna("dom.planoFilcar")
            iGeneradorSql.agregarColumna("dom.coordenadaFilcar")
            iGeneradorSql.agregarColumna("dom.idLocalidad")
            iGeneradorSql.agregarColumna("dom.LocalidadAnterior")
            iGeneradorSql.agregarColumna("dom.telefonoCodigoArea")
            iGeneradorSql.agregarColumna("dom.telefonoCaracteristica")
            iGeneradorSql.agregarColumna("dom.telefonoNumero")
            iGeneradorSql.agregarColumna("dom.telefono")
            iGeneradorSql.agregarColumna("dom.TelefonoReferenciaCodigoArea")
            iGeneradorSql.agregarColumna("dom.TelefonoReferenciaCaracteristica")
            iGeneradorSql.agregarColumna("dom.TelefonoReferenciaNumero")
            iGeneradorSql.agregarColumna("dom.TelefonoReferencia")
            iGeneradorSql.agregarColumna("dom.TelefonoCelularCodigoArea")
            iGeneradorSql.agregarColumna("dom.TelefonoCelularCaracteristica")
            iGeneradorSql.agregarColumna("dom.TelefonoCelularNumero")
            iGeneradorSql.agregarColumna("dom.TelefonoCelular")
            iGeneradorSql.agregarColumna("dom.idcalleBase")
            iGeneradorSql.agregarColumna("dom.departamento")
            iGeneradorSql.agregarColumna("dom.manzana")
            iGeneradorSql.agregarColumna("dom.edificio")
            iGeneradorSql.agregarColumna("dom.monoblock")
            iGeneradorSql.agregarColumna("dom.escaleras")
            iGeneradorSql.agregarColumna("dom.pasillo")
            iGeneradorSql.agregarColumna("dom.casa")
            iGeneradorSql.agregarColumna("dom.lote")
            iGeneradorSql.agregarColumna("dom.idVivienda")

            iGeneradorSql.agregarTabla("Nivel n")
            iGeneradorSql.agregarTabla("UnidadDeNegocios u")
            iGeneradorSql.agregarTabla("domicilio dom")
            iGeneradorSql.agregarCondicionWhere("n.id=u.id")
            iGeneradorSql.agregarCondicionWhere("dom.id=u.iddomicilio")


            iDataSetNiveles = iConexion.getDataSet(iDataSetNiveles, iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "UnidadDeNegocios")


            For Each iDataRow As DataRow In iDataSetNiveles.Tables("UnidadDeNegocios").Rows
                iNivel = New UnidadDeNegocios
                With CType(iNivel, UnidadDeNegocios)
                    .id = iDataRow.Item("id").ToString
                    .descripcion = iDataRow.Item("nivelDescripcion").ToString
                    .padre = New EmpresaGrupo()
                    .padre.id = iDataRow.Item("idEmpresaGrupo").ToString
                    .domicilio = New Domicilio()
                    .domicilio.id = iDataRow.Item("idDomicilio").ToString
                    .numeroIb = iDataRow.Item("numeroIB").ToString
                    .numeroCuit = FuncionComun.vacioSiEsNulo(iDataRow.Item("numeroCuit"))

                    With .domicilio
                        .calle = FuncionComun.vacioSiEsNulo(iDataRow.Item("calle").ToString.Replace("%", "'"))
                        .numero = FuncionComun.vacioSiEsNulo(iDataRow.Item("numero").ToString)
                    End With
                End With

                iListaNiveles.Add(iNivel)
                iNivel = Nothing
            Next

            iGeneradorSql.agregarColumna("e.id")
            iGeneradorSql.agregarColumna("e.numeroCuit1")
            iGeneradorSql.agregarColumna("e.numeroCuit")
            iGeneradorSql.agregarColumna("e.numeroCuit2")
            iGeneradorSql.agregarColumna("e.idDomicilio")
            iGeneradorSql.agregarColumna("e.idGrupoEmpresas")
            iGeneradorSql.agregarColumna("e.numeroIB")
            iGeneradorSql.agregarColumna("n.descripcion as nivelDescripcion")
            iGeneradorSql.agregarColumna("dom.id as idDomicilio")
            iGeneradorSql.agregarColumna("dom.calle")
            iGeneradorSql.agregarColumna("dom.numero")
            iGeneradorSql.agregarColumna("dom.piso")
            iGeneradorSql.agregarColumna("dom.entre")
            iGeneradorSql.agregarColumna("dom.idbarrio")
            iGeneradorSql.agregarColumna("dom.codigoPostal")
            iGeneradorSql.agregarColumna("dom.planoFilcar")
            iGeneradorSql.agregarColumna("dom.coordenadaFilcar")
            iGeneradorSql.agregarColumna("dom.idLocalidad")
            iGeneradorSql.agregarColumna("dom.LocalidadAnterior")
            iGeneradorSql.agregarColumna("dom.telefonoCodigoArea")
            iGeneradorSql.agregarColumna("dom.telefonoCaracteristica")
            iGeneradorSql.agregarColumna("dom.telefonoNumero")
            iGeneradorSql.agregarColumna("dom.telefono")
            iGeneradorSql.agregarColumna("dom.TelefonoReferenciaCodigoArea")
            iGeneradorSql.agregarColumna("dom.TelefonoReferenciaCaracteristica")
            iGeneradorSql.agregarColumna("dom.TelefonoReferenciaNumero")
            iGeneradorSql.agregarColumna("dom.TelefonoReferencia")
            iGeneradorSql.agregarColumna("dom.TelefonoCelularCodigoArea")
            iGeneradorSql.agregarColumna("dom.TelefonoCelularCaracteristica")
            iGeneradorSql.agregarColumna("dom.TelefonoCelularNumero")
            iGeneradorSql.agregarColumna("dom.TelefonoCelular")
            iGeneradorSql.agregarColumna("dom.idcalleBase")
            iGeneradorSql.agregarColumna("dom.departamento")
            iGeneradorSql.agregarColumna("dom.manzana")
            iGeneradorSql.agregarColumna("dom.edificio")
            iGeneradorSql.agregarColumna("dom.monoblock")
            iGeneradorSql.agregarColumna("dom.escaleras")
            iGeneradorSql.agregarColumna("dom.pasillo")
            iGeneradorSql.agregarColumna("dom.casa")
            iGeneradorSql.agregarColumna("dom.lote")
            iGeneradorSql.agregarColumna("dom.idVivienda")

            iGeneradorSql.agregarTabla("Nivel n")
            iGeneradorSql.agregarTabla("EmpresaGrupo e")
            iGeneradorSql.agregarTabla("domicilio dom")
            iGeneradorSql.agregarCondicionWhere("n.id=e.id")
            iGeneradorSql.agregarCondicionWhere("dom.id=e.iddomicilio")

            iDataSetNiveles = iConexion.getDataSet(iDataSetNiveles, iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "EmpresaGrupo")

            For Each iDataRow As DataRow In iDataSetNiveles.Tables("EmpresaGrupo").Rows
                iNivel = New EmpresaGrupo
                With CType(iNivel, EmpresaGrupo)
                    .id = iDataRow.Item("id").ToString
                    .descripcion = iDataRow.Item("nivelDescripcion").ToString
                    .numeroCuit1 = FuncionComun.ceroSiEsVacio(iDataRow.Item("numeroCuit1").ToString())
                    .numeroCuit = FuncionComun.ceroSiEsVacio(iDataRow.Item("numeroCuit").ToString())
                    .numeroCuit2 = FuncionComun.ceroSiEsVacio(iDataRow.Item("numeroCuit2").ToString())
                    .numeroIB = iDataRow.Item("numeroIB").ToString()
                    .domicilio = New Domicilio
                    .domicilio.id = iDataRow.Item("idDomicilio").ToString()
                    .padre = New GrupoEmpresas()
                    .padre.id = iDataRow.Item("idGrupoEmpresas").ToString()

                    With .domicilio
                        .calle = FuncionComun.vacioSiEsNulo(iDataRow.Item("calle").ToString.Replace("%", "'"))
                        .numero = FuncionComun.vacioSiEsNulo(iDataRow.Item("numero").ToString)
                    End With
                End With

                iListaNiveles.Add(iNivel)
                iNivel = Nothing
            Next


            iGeneradorSql.agregarColumna("g.id")
            iGeneradorSql.agregarColumna("g.idDomicilio")
            iGeneradorSql.agregarColumna("n.descripcion as nivelDescripcion")
            iGeneradorSql.agregarColumna("dom.id as idDomicilio")
            iGeneradorSql.agregarColumna("dom.calle")
            iGeneradorSql.agregarColumna("dom.numero")
            iGeneradorSql.agregarColumna("dom.piso")
            iGeneradorSql.agregarColumna("dom.entre")
            iGeneradorSql.agregarColumna("dom.idbarrio")
            iGeneradorSql.agregarColumna("dom.codigoPostal")
            iGeneradorSql.agregarColumna("dom.planoFilcar")
            iGeneradorSql.agregarColumna("dom.coordenadaFilcar")
            iGeneradorSql.agregarColumna("dom.idLocalidad")
            iGeneradorSql.agregarColumna("dom.LocalidadAnterior")
            iGeneradorSql.agregarColumna("dom.telefonoCodigoArea")
            iGeneradorSql.agregarColumna("dom.telefonoCaracteristica")
            iGeneradorSql.agregarColumna("dom.telefonoNumero")
            iGeneradorSql.agregarColumna("dom.telefono")
            iGeneradorSql.agregarColumna("dom.TelefonoReferenciaCodigoArea")
            iGeneradorSql.agregarColumna("dom.TelefonoReferenciaCaracteristica")
            iGeneradorSql.agregarColumna("dom.TelefonoReferenciaNumero")
            iGeneradorSql.agregarColumna("dom.TelefonoReferencia")
            iGeneradorSql.agregarColumna("dom.TelefonoCelularCodigoArea")
            iGeneradorSql.agregarColumna("dom.TelefonoCelularCaracteristica")
            iGeneradorSql.agregarColumna("dom.TelefonoCelularNumero")
            iGeneradorSql.agregarColumna("dom.TelefonoCelular")
            iGeneradorSql.agregarColumna("dom.idcalleBase")
            iGeneradorSql.agregarColumna("dom.departamento")
            iGeneradorSql.agregarColumna("dom.manzana")
            iGeneradorSql.agregarColumna("dom.edificio")
            iGeneradorSql.agregarColumna("dom.monoblock")
            iGeneradorSql.agregarColumna("dom.escaleras")
            iGeneradorSql.agregarColumna("dom.pasillo")
            iGeneradorSql.agregarColumna("dom.casa")
            iGeneradorSql.agregarColumna("dom.lote")
            iGeneradorSql.agregarColumna("dom.idVivienda")

            iGeneradorSql.agregarTabla("grupoEmpresas g")
            iGeneradorSql.agregarTabla("Nivel n")
            iGeneradorSql.agregarTabla("domicilio dom")
            iGeneradorSql.agregarCondicionWhere("n.id=g.id")
            iGeneradorSql.agregarCondicionWhere("dom.id=g.iddomicilio")

            iDataSetNiveles = iConexion.getDataSet(iDataSetNiveles, iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "grupoEmpresas")

            For Each iDataRow As DataRow In iDataSetNiveles.Tables("grupoEmpresas").Rows
                iNivel = New GrupoEmpresas
                With CType(iNivel, GrupoEmpresas)
                    .id = iDataRow.Item("id").ToString
                    .descripcion = iDataRow.Item("nivelDescripcion").ToString
                    .domicilio = New Domicilio
                    .domicilio.id = iDataRow.Item("idDomicilio").ToString()
                    With .domicilio
                        .calle = FuncionComun.vacioSiEsNulo(iDataRow.Item("calle").ToString.Replace("%", "'"))
                        .numero = FuncionComun.vacioSiEsNulo(iDataRow.Item("numero").ToString)
                    End With

                End With

                iListaNiveles.Add(iNivel)
                iNivel = Nothing
            Next


            iGeneradorSql.agregarColumna("s.id")
            iGeneradorSql.agregarColumna(FuncionComun.sqlRellenarAIzquierda("s.codigo", "4", "0") & " as codigo")
            iGeneradorSql.agregarColumna(FuncionComun.sqlConcatenar(FuncionComun.sqlRellenarAIzquierda("s.codigo", 4, "0") & " ,'  - ',n.descripcion") & " as descripcion")
            iGeneradorSql.agregarColumna("n.descripcion as solodescripcion")
            iGeneradorSql.agregarColumna("s.tipoSucursal")
            iGeneradorSql.agregarColumna("s.idUnidadDeNegocios")
            iGeneradorSql.agregarColumna("u.idEmpresaGrupo")
            iGeneradorSql.agregarColumna("eg.idGrupoEmpresas")
            iGeneradorSql.agregarColumna("n.idtipoNivel")

            iGeneradorSql.agregarTabla("sucursal s")
            iGeneradorSql.agregarTabla("nivel n")
            iGeneradorSql.agregarTabla("UnidadDeNegocios u")
            iGeneradorSql.agregarTabla("empresaGrupo eg")

            iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=eg.id")
            iGeneradorSql.agregarCondicionWhere("s.idUnidadDeNegocios=u.id")
            iGeneradorSql.agregarCondicionWhere("s.id=n.id")
            iGeneradorSql.agregarCondicionWhere("s.idEstado=" & Estado.ALTA)


            iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


            iGeneradorSql.agregarColumna("u.id")
            iGeneradorSql.agregarColumna("'' as codigo")
            iGeneradorSql.agregarColumna("n.descripcion")
            iGeneradorSql.agregarColumna("n.descripcion as solodescripcion")
            iGeneradorSql.agregarColumna("0 as tipoSucursal")
            iGeneradorSql.agregarColumna("u.id as  idUnidadDeNegocios")
            iGeneradorSql.agregarColumna("u.idEmpresaGrupo")
            iGeneradorSql.agregarColumna("eg.idGrupoEmpresas")
            iGeneradorSql.agregarColumna("n.idtipoNivel")

            iGeneradorSql.agregarTabla("unidadDeNegocios u")
            iGeneradorSql.agregarTabla("empresaGrupo eg")
            iGeneradorSql.agregarTabla("nivel n")
            iGeneradorSql.agregarCondicionWhere("u.id=n.id")
            iGeneradorSql.agregarCondicionWhere("u.idEmpresaGrupo=eg.id")

            iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)

            iGeneradorSql.agregarColumna("e.id")
            iGeneradorSql.agregarColumna("'' as codigo")
            iGeneradorSql.agregarColumna("n.descripcion")
            iGeneradorSql.agregarColumna("n.descripcion as solodescripcion")
            iGeneradorSql.agregarColumna("0 as tipoSucursal")
            iGeneradorSql.agregarColumna("0 as idUnidadDeNegocios")
            iGeneradorSql.agregarColumna("e.id as idEmpresaGrupo")
            iGeneradorSql.agregarColumna("e.idGrupoEmpresas")
            iGeneradorSql.agregarColumna("n.idtipoNivel")

            iGeneradorSql.agregarTabla("empresaGrupo e")
            iGeneradorSql.agregarTabla("nivel n")
            iGeneradorSql.agregarCondicionWhere("e.id=n.id")

            iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)

            iGeneradorSql.agregarColumna("g.id")
            iGeneradorSql.agregarColumna("'' as codigo")
            iGeneradorSql.agregarColumna("n.descripcion")
            iGeneradorSql.agregarColumna("n.descripcion as solodescripcion")
            iGeneradorSql.agregarColumna("0 as tipoSucursal")
            iGeneradorSql.agregarColumna("0 as idUnidadDeNegocios")
            iGeneradorSql.agregarColumna("0 as idEmpresaGrupo")
            iGeneradorSql.agregarColumna("g.id as idGrupoEmpresas")
            iGeneradorSql.agregarColumna("n.idtipoNivel")

            iGeneradorSql.agregarTabla("grupoEmpresas g")
            iGeneradorSql.agregarTabla("nivel n")
            iGeneradorSql.agregarCondicionWhere("g.id=n.id")

            iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)
            iGeneradorSql.agregarOrden("descripcion asc")
            iDataSet = iConexion.getDataSet(iGeneradorSql.generarUnion, iGeneradorSql.parametrosSQL, "Niveles")

            iFechaHora = Now

        Catch excepcion As Exception
            Throw New NivelNoEncontradoException(excepcion)
        Finally
            iConexion.cerrar()
            iConexion = Nothing
            iGeneradorSql.destructor()
            iDataSetNiveles = Nothing
            iGeneradorSql = Nothing
        End Try
    End Sub

End Class
