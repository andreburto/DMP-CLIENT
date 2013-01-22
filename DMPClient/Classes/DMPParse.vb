Imports System
Imports System.Collections
Imports System.IO
Imports System.Text
Imports System.Text.Encoding
Imports Microsoft

Public Class DMPParse

    Private vals() As ArrayList

    Public Sub New()
    End Sub

    Public Sub New(ByVal str As String)
        ParseString(str)
    End Sub

    Public Sub ParseString(ByVal str As String)
        str = str.Replace(vbCrLf, vbLf)
        Dim lines() As String = str.Split(vbLf)

        For Each line As String In lines
            If line.Length <= 1 Then Continue For
            Dim parts() As String = line.Split(vbTab)
            If parts.Length = 3 Then

            End If
        Next
    End Sub

End Class
