Public Class frmAddAddress

    Public di As DMP.DMPItem

    Public Sub New(ByVal url As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        txtAddress.Text = url
        txtTitle.Text = url

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        di = New DMP.DMPItem(Me.txtAddress.Text, Me.txtTitle.Text)
        Me.DialogResult = DialogResult.OK
    End Sub
End Class