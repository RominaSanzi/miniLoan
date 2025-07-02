Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class MenuSimple : Inherits Menu

#Region "Variables"


#End Region

#Region "Atributos"

#End Region

#Region "Metodos"
    Public Overrides Function isMenuSimple() As Boolean
        Return True
    End Function

    Public Overrides Function isMenuCompuesto() As Boolean
        Return False
    End Function

    Public Overrides Function isHijoDeRaiz() As Boolean
        Return idPadre = idRaiz
    End Function

#End Region

End Class
