Imports System
Imports System.Collections
Imports System.IO
Imports System.Xml

Namespace DMP
    Public Class DMPList

        Private fn As String = ""

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

            For Each item As String In col
                xml = xml + "  <address>" + item + "</address>" + vbCrLf
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

        Public Function ReadXml() As Array
            Dim theFile As String = FullFilePath()
            Dim list As New ArrayList

            Try
                Dim xmlFile As New XmlDocument
                xmlFile.Load(theFile)

                For Each n As XmlNode In xmlFile.SelectNodes("/addresses/address")
                    list.Add(n.InnerText)
                Next
            Catch ex As Exception
                list.Clear()
            End Try

            Return list.ToArray
        End Function

    End Class
End Namespace