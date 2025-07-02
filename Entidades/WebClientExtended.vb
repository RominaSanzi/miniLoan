Imports System.Net

Public Class WebClientExtended
    Inherits WebClient
    Public Property Timeout() As Integer
        Get
            Return m_Timeout
        End Get
        Set(value As Integer)
            m_Timeout = value
        End Set
    End Property
    Private m_Timeout As Integer

    Protected Overrides Function GetWebRequest(address As Uri) As WebRequest
        Dim request = MyBase.GetWebRequest(address)
        request.Timeout = Timeout
        Return request
    End Function
End Class
