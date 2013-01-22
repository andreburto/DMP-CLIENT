Imports System
Imports System.Collections
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports System.Text.Encoding
Imports System.Text.StringBuilder
Imports System.Web
Imports System.Xml
Imports Microsoft

Public Class DMPConnect

    Private params As Hashtable
    Private userid As String = ""
    Private passwd As String = ""
    Private count = 0

    Sub New()
        params = New Hashtable()

        ' MAGIC -- overrides certificate check errors and allows a connection.
        ServicePointManager.ServerCertificateValidationCallback = (Function(sender, certificate, chain, sslPolicyErrors) True)
    End Sub

    Public Sub AddParam(ByVal key As String, ByVal val As String)
        If params.ContainsKey(key) Then params.Remove(key)
        params.Add(key, val)
    End Sub

    Public Sub ClearParams()
        params.Clear()
    End Sub

    Public Sub DelParam(ByVal key As String)
        If params.ContainsKey(key) Then params.Remove(key)
    End Sub

    Public Function GetParam(ByVal address As String) As String
        Return HttpGet(BuildUrl(address, "get_param"))
    End Function

    Public Sub SetCredentials(ByVal id As String, ByVal pw As String)
        Me.userid = id
        Me.passwd = pw
    End Sub

    Public Function SetParam(ByVal address As String) As String
        Return HttpGet(BuildUrl(address, "set_param"))
    End Function

    Private Function BuildUrl(ByVal url As String, ByVal func As String) As String
        Dim theUrl As String = "https://" + url + ":7777/" + func + "?"
        Dim temp As ArrayList = New ArrayList

        For Each k As String In params.Keys
            temp.Add(k + "=" + Web.HttpUtility.UrlEncode(params(k).ToString()))
        Next

        theUrl += String.Join("&", temp.ToArray(GetType(String)))
        Windows.Forms.MessageBox.Show(theUrl.ToString)
        Return theUrl.ToString
    End Function

    Private Function HttpGet(ByVal url As String) As String
        Dim getit As HttpWebRequest = WebRequest.Create(url)
        Dim results As String = ""


        getit.PreAuthenticate = True
        getit.Credentials = New NetworkCredential(userid, passwd)
        getit.Method = "GET"
        getit.UserAgent = "DMP CLIENT"

        Try
            Dim resp As WebResponse = getit.GetResponse()
            Dim sr As StreamReader = New StreamReader(resp.GetResponseStream())
            results = sr.ReadToEnd
            resp.Close()
        Catch x As Exception
            If count < 2 Then
                results = HttpGet(url)
            Else
                results = "ERROR: " + x.Message
            End If
        End Try

        Return results
    End Function

End Class
