
Public Class frmDeliveryScan

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        tempValue3 = TextEdit1.Text
        If tempValue3 = "" Then
            MsgBox("�нT�{�s�b���˽c�s���I")
            Exit Sub
        End If
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
End Class