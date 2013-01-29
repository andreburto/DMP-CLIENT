Namespace DMP
    Public Class DMPCommands
        Inherits DMPConnect

        Private dmp_url As String = ""

        Public Sub New(ByVal u As String, ByVal id As String, ByVal pw As String)
            Me.SetCredentials(id, pw)
            Me.dmp_url = u
        End Sub

        Public Function GetWebPage() As String
            Dim url As String = ""
            Me.AddParam("p", "init.TVZILLA_URL")
            Dim dp As New DMPParse(Me.GetParam())
            Return dp.GetValue("init.TVZILLA_URL").value
        End Function

        Public Function SetWebPage(ByVal url As String) As Boolean
            Me.AddParam("init.BROWSER_CMD", url)
            Me.AddParam("init.TVZILLA_URL", url)
            Me.AddParam("init.STARTUP_URL", url)
            Return Me.SetParam()
        End Function

        Public Function SaveMib() As Boolean
            Me.AddParam("mib.save", "1")
            Dim res As String = Me.SetParam()
            Return Me.SetParam()
        End Function

        Public Function ShowIP(ByVal secs As Integer) As Boolean
            Dim ms As String = (secs * 1000).ToString
            Me.AddParam("irasrv.key_SHOW_IP", "mng start showip " + ms)
            Return Me.SetParam()
        End Function

        Public Function Reboot() As Boolean
            Me.AddParam("mng.reboot", "1")
            Return Me.SetParam()
        End Function

        Private Overloads Function GetParam() As String
            Dim res As String = Me.GetParam(Me.dmp_url)
            Me.ClearParams()
            Return res
        End Function

        Private Overloads Function SetParam() As Boolean
            Dim res As String = Me.SetParam(Me.dmp_url)
            Me.ClearParams()
            If res.Contains("ERROR") Then Return False Else Return True
        End Function

    End Class
End Namespace