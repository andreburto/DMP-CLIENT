Public Class DMPValue

    Public key As String = ""
    Public type As String = ""
    Public value As String = ""

    Public Sub New()
    End Sub

    Public Sub New(ByVal k As String, ByVal t As String, ByVal v As String)
        Me.key = k
        Me.type = t
        Me.value = v
    End Sub

    Public Overrides Function ToString() As String
        Return "Name: " + Me.key + ", Type: " + Me.type + ", Value: " + Me.value
    End Function

End Class
