Public Class ProductionPiecePersonnelDate

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
        tempValue = Nothing
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Per_DateEdit.EditValue = Format(Now, "yyyy/MM/dd") Then
            MsgBox("����ɤJ��Ѫ����u�H��.")
        Else
            tempValue = "OK"
            tempValue2 = Per_DateEdit.EditValue
            Me.Close()
        End If
    End Sub

    Private Sub ProductionPiecePersonnelDate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Per_DateEdit.Text = DateAdd(DateInterval.Day, -1, CDate(Format(Now, "yyyy/MM/dd")))  ''�o��e�@�}���
    End Sub
End Class