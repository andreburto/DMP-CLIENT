Imports System
Imports System.Collections
Imports System.IO
Imports System.Security
Imports System.Xml

Namespace DMP
    Public Class DMPList

        Private fn As String = ""
        Public addreses As Collection = New Collection()

        Public Sub New(ByVal filename As String)
            fn = filename
        End Sub

        Public Function CheckForXml() As Boolean
            If File.Exists(FullFilePath()) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function FullFilePath() As String
            Dim filePath As String = Environment.CurrentDirectory + "\" + fn
            Return filePath
        End Function

        Public Function WriteXml(ByVal col As Collection) As Boolean
            Dim theFile As String = FullFilePath()
            Dim xml As String = "<addresses>" + vbCrLf

            For Each item As DMPItem In col
                xml = xml + " <address>" + vbCrLf
                xml = xml + "  <url>" + item.url + "</url>" + vbCrLf
                xml = xml + "  <title>" + item.title + "</title>" + vbCrLf
                xml = xml + " </address>" + vbCrLf
            Next

            xml = xml + "</addresses>" + vbCrLf

            Try
                Dim write As New StreamWriter(theFile, False, System.Text.UTF8Encoding.UTF8)
                write.Write(xml)
                write.Close()
                Return True
            Catch ex As Exception
                ErrorMsg.Show("Could not write XML")
                Return False
            End Try
        End Function

        Public Function ReadXml() As Collection
            Dim theFile As String = FullFilePath()

            Try
                Dim xmlFile As New XmlDocument
                xmlFile.Load(theFile)

                For Each n As XmlNode In xmlFile.SelectNodes("/addresses/address")
                    addreses.Add(New DMPItem(n.Item("url").InnerText, n.Item("title").InnerText), n.Item("url").InnerText)
                Next
            Catch ex As Exception
                Me.addreses.Clear()
            End Try

            Return Me.addreses
        End Function

    End Class

    Public Class DMPItem
        Public url As String
        Public title As String
        Public Sub New(ByVal u As String, ByVal t As String)
            Me.url = SecurityElement.Escape(u)
            Me.title = SecurityElement.Escape(t)
        End Sub
    End Class
End Namespace