Public Class frmSamplePackingQuery

#Region "���J����"
    Private Sub frmOutPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtVersions.Text = "A.�]�˽c"
        txtID.Text = tempValue
        tempValue = ""
    End Sub
#End Region

#Region "����ƥ�"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        '������
        If Me.txtVersions.Text = "" Then
            tempValue2 = Nothing
        Else
            tempValue2 = txtVersions.Text
        End If
        '�]��
        If Me.txtID.Text = "" Then
            tempValue3 = Nothing
        Else
            tempValue3 = txtID.Text
        End If
        '���X
        If Me.txtCode.Text = "" Then
            tempValue4 = Nothing
        Else
            tempValue4 = txtCode.Text
        End If

        Me.Close()
    End Sub
#End Region

#Region "��ܱ���"
    Private Sub txtVersions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVersions.SelectedIndexChanged
        Select Case Mid(txtVersions.Text, 1, 1)
            Case "A"
                Me.lblID.Text = "�]�˳渹(&L)�G"
                Me.lblCode.Text = "�]�˱��X(&Y)�G"
                Me.lblID.Enabled = True
                Me.lblCode.Enabled = True
                Me.txtID.Enabled = True
                Me.txtCode.Enabled = True
            Case "B"
                Me.lblID.Text = "���c�渹(&L)�G"
                Me.lblCode.Text = "���c���X(&Y)�G"
                Me.lblID.Enabled = True
                Me.lblCode.Enabled = True
                Me.txtID.Enabled = True
                Me.txtCode.Enabled = True
            Case "C"
                Me.lblCode.Text = "���~���X(&Y)�G"
                Me.lblID.Text = "���c�渹(&L)�G"
                Me.lblID.Enabled = False
                Me.lblCode.Enabled = True
                Me.txtID.Enabled = False
                Me.txtCode.Enabled = True
        End Select
    End Sub
#End Region

End Class