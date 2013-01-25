Imports System.Windows.Forms

Module ErrorMsg
    Public Sub Show(ByVal msg As String)
        MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    End Sub
End Module
