
Imports LFERP.Library.Material
Imports LFERP.Library

Public Class FrmWareOutFind



    Dim mtc As New Material.MaterialTypeController
    Private Sub FrmWareOutFind_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextEdit1.EditValue = tempValue3
        mtc.LoadNodes(tv1, ErpUser.MaterialType)
        tempValue3 = ""
    End Sub
    Private Sub tv1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv1.AfterSelect
        PopupContainerEdit1.EditValue = tv1.SelectedNode.Tag
        PopupContainerControl1.OwnerEdit.ClosePopup()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If RadioButton1.Checked = True Then                 '�渹
            tempValue = "1"
            tempValue2 = txtLFID.EditValue
        End If

        If RadioButton2.Checked = True Then                 '����
            tempValue = "2"
            tempValue2 = PopupContainerEdit1.EditValue
        End If

        If RadioButton3.Checked = True Then                 '�W��
            tempValue = "3"
            tempValue2 = TextEdit3.EditValue
        End If

        If RadioButton4.Checked = True Then                 '�u��
            tempValue = "4"
            tempValue2 = WOPerID.EditValue
        End If

        If RadioButton5.Checked = True Then                 '����
            tempValue = "5"
            tempValue2 = TextEdit2.EditValue
        End If
        If RadioButton6.Checked = True Then
            tempValue = "6"
            If ComboBoxEdit1.EditValue = "�w�_��" Then
                tempValue2 = True
            Else
                tempValue2 = False
            End If
        End If

        Me.Close()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Dim i%
        If sender.checked = True Then
            i = sender.tag
            Me.Controls.Item(i).Focus()
        End If
    End Sub
End Class