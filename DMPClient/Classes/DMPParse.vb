Imports System
Imports System.Collections
Imports System.IO
Imports System.Text
Imports System.Text.Encoding
Imports Microsoft

Namespace DMP
    Public Class DMPParse

        Private vals As Hashtable = New Hashtable()

        Public Sub New()
        End Sub

        Public Sub New(ByVal str As String)
            ParseString(str)
        End Sub

        Public Function GetValue(ByVal key As String) As DMPValue
            If vals.ContainsKey(key) = False Then Return New DMPValue("No such key", "ERROR", "No value with that key")
            Return vals(key)
        End Function

        Public Sub ParseString(ByVal str As String)
            str = str.Replace(vbCrLf, vbLf)
            Dim lines() As String = str.Split(vbLf)

            For Each line As String In lines
                If line.Length <= 1 Then Continue For
                Dim parts() As String = line.Split(vbTab)
                If parts.Length = 3 Then
                    vals(parts(0)) = New DMPValue(parts(0), parts(1), parts(2))
                End If
            Next
        End Sub

    End Class
End Namespace