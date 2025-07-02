Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.seguridad
Imports di.financiera.utils
Imports System.Collections.Generic

Public Class RolCopiarVO

    Inherits Entidad

#Region "Variables"
    Private iRolesSeleccionados As List(Of Rol)
    Private iUsuarios As List(Of Usuario)
#End Region

#Region "Atributos"
    Public Property rolesSeleccionados As List(Of Rol)
        Get
            Return iRolesSeleccionados
        End Get
        Set(value As List(Of Rol))
            iRolesSeleccionados = value
        End Set
    End Property

    Public Property usuarios As List(Of Usuario)
        Get
            Return iUsuarios
        End Get
        Set(value As List(Of Usuario))
            iUsuarios = value
        End Set
    End Property

#End Region

End Class
