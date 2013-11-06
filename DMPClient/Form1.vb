Imports DMPClient.DMP

Public Class Form1

    Private dl As DMP.DMPList
    Private dc As DMP.DMPCommands
    Public dmps As Collection
    Private Start As String = "Addresses go here..."
    Private Title As String = "DMP CLIENT"

    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        dl.WriteXml(dmps)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckField()
        dl = New DMP.DMPList("addresses.xml")
        If dl.CheckForXml = True Then
            dmps = dl.ReadXml
            For Each current As DMPItem In dmps
                cmbAddresses.Items.Add(current.url)
            Next
        End If
        cmbAddresses.Text = Start
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If cmbAddresses.Text.Length > 0 Then
            'cmbAddresses.Items.Add(cmbAddresses.Text)
            Dim address As frmAddAddress = New frmAddAddress(cmbAddresses.Text)
            address.ShowDialog()
            If address.DialogResult = DialogResult.OK Then
                dmps.Add(address.di, address.di.url)
                cmbAddresses.Items.Add(address.di.url)
            End If
            address.Close()
            address.Dispose()
        Else
            ErrorMsg.Show("Enter an address...")
        End If
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If cmbAddresses.Text.Length > 0 Then
            dmps.Remove(cmbAddresses.Text)
            cmbAddresses.Items.RemoveAt(cmbAddresses.SelectedIndex)
            cmbAddresses.Text = Start
            Me.Text = Me.Title
        Else
            ErrorMsg.Show("Nothing to erase...")
        End If
    End Sub

    Private Sub btnReboot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReboot.Click
        Me.UnactivateButtons(False)
        dc = New DMP.DMPCommands(cmbAddresses.Text, txtUser.Text, txtPass.Text)
        dc.Reboot()
        Me.UnactivateButtons(True)
    End Sub

    Private Sub btnGetAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAddress.Click
        Me.UnactivateButtons(False)
        dc = New DMP.DMPCommands(cmbAddresses.Text, txtUser.Text, txtPass.Text)
        txtAddress.Text = dc.GetWebPage
        Me.UnactivateButtons(True)
    End Sub

    Private Sub btnSetAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetAddress.Click
        If txtAddress.Text.Length = 0 Then
            ErrorMsg.Show("There's no address to set...")
            Exit Sub
        End If
        Me.UnactivateButtons(False)
        dc = New DMP.DMPCommands(cmbAddresses.Text, txtUser.Text, txtPass.Text)
        If dc.SetWebPage(txtAddress.Text) = True Then dc.SaveMib() Else ErrorMsg.Show("Could not update address...")
        Me.UnactivateButtons(True)
    End Sub

    Private Sub btnShowip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowip.Click
        Me.UnactivateButtons(False)
        dc = New DMP.DMPCommands(cmbAddresses.Text, txtUser.Text, txtPass.Text)
        dc.ShowIP(60)
        Me.UnactivateButtons(True)
    End Sub

    Private Sub CheckField()
        If cmbAddresses.Text.Length > 0 And txtUser.Text.Length > 0 And txtPass.Text.Length > 0 Then
            UnactivateButtons(True)
        Else
            UnactivateButtons(False)
        End If
    End Sub

    Private Sub UnactivateButtons(ByVal yesno As Boolean)
        btnReboot.Enabled = yesno
        btnShowip.Enabled = yesno
        btnSetAddress.Enabled = yesno
        btnGetAddress.Enabled = yesno
        btnSetAll.Enabled = yesno
    End Sub

    Private Sub txtUser_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUser.TextChanged
        CheckField()
    End Sub

    Private Sub txtPass_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPass.TextChanged
        CheckField()
    End Sub

    Private Sub cmbAddresses_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAddresses.SelectedIndexChanged
        UpdateTitle()
        CheckField()
    End Sub

    Private Sub cmbAddresses_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAddresses.SelectedValueChanged
        UpdateTitle()
        CheckField()
    End Sub

    Private Sub cmbAddresses_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAddresses.TextChanged
        CheckField()
    End Sub

    Private Sub UpdateTitle()
        If dmps.Contains(cmbAddresses.Text) Then
            Dim item As DMPItem = dmps.Item(cmbAddresses.Text)
            Me.Text = Me.Title & " - " & item.title
        Else
            Me.Text = Me.Title
        End If
    End Sub

    Private Sub btnSetAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetAll.Click
        If txtAddress.Text.Length = 0 Then
            ErrorMsg.Show("There's no address to set...")
            Exit Sub
        End If
        Dim problems As ArrayList = New ArrayList
        Me.UnactivateButtons(False)
        For Each item As DMPItem In dmps
            Me.Text = Me.Title & " - changing " & item.url
            dc = New DMP.DMPCommands(item.url, txtUser.Text, txtPass.Text)
            If dc.SetWebPage(txtAddress.Text) = True Then dc.SaveMib() Else problems.Add(item.url)
            Threading.Thread.Sleep(500)
        Next
        Me.UnactivateButtons(True)
        If problems.Count > 0 Then
            Dim p As String = "Could not set: "
            For Each i As String In problems
                p &= vbCrLf & i.ToString
            Next
            ErrorMsg.Show(p)
        End If
    End Sub
End Class
