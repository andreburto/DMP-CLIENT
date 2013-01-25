Imports System
Imports System.IO

Module DMP

    Sub Main()
        Dim dc As New DMPConnect()
        Dim dp As New DMPParse()

        dc.SetCredentials("", "")
        dc.AddParam("p", "init.*")

        Dim vals As String = dc.GetParam("192.168.45.20")

        If vals.Length = 0 Then
            Console.Out.WriteLine("No values")
            End
        End If

        dp.ParseString(vals)

        Dim dv As DMPValue = dp.GetValue("init.TVZILLA_URL")

        Console.Out.WriteLine(dv.key + " = " + dv.value)
    End Sub

End Module
