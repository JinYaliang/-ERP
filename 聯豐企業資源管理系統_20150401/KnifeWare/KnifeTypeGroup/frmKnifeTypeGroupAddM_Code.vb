Imports LFERP.Library.Material
Imports LFERP.Library

Public Class frmKnifeTypeGroupAddM_Code

    Private Sub tv1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tv1.MouseDoubleClick
        '�����ܪ����O�s��
        If tv1.SelectedNode.Level = 3 Then
            popTypeID.Tag = tv1.SelectedNode.Tag
            popTypeID.EditValue = tv1.SelectedNode.Text
            PopupContainerControl1.OwnerEdit.ClosePopup()


            Dim kc As New LFERP.Library.KnifeWare.KnifeTypeGroupControl
            GridLookM_Code.Properties.DataSource = kc.KnifeMaterialCode_GetList(Nothing, Nothing, tv1.SelectedNode.Tag)

        End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frmKnifeTypeGroupAddM_Code_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LabKnifeID.Text = tempValue
        tempValue = Nothing

        Dim mtc As New Material.MaterialTypeController
        mtc.LoadNodes(tv1, "60")    '�u��ܦ��~��

        GridLookM_Code.Properties.DisplayMember = "M_Gauge"
        GridLookM_Code.Properties.ValueMember = "M_Code"

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If popTypeID.Text = "" Then
            MsgBox("�п�����O�I", 64, "����")
            GridLookM_Code.Select()
            Exit Sub
        End If
        '----------------------------------------
        If GridLookM_Code.Text = "" Then
            MsgBox("�п�ܪ��ƳW��I", 64, "����")
            GridLookM_Code.Select()
            Exit Sub
        End If
        ''--------------------------------------
        Dim kc As New LFERP.Library.KnifeWare.KnifeTypeGroupControl
        If kc.KnifeMaterialCode_UpdateType(GridLookM_Code.EditValue, LabKnifeID.Text) = True Then
            MsgBox("�O�s���\!", 64, "����")
        Else
            MsgBox("��s����,���ˬd!", 64, "����")
            Exit Sub
        End If

        Me.Close()

    End Sub
End Class