Imports LFERP.Library.KnifeWare

Imports LFERP.SystemManager

Public Class frmKnifeTypeGroupMain

    Private Sub frmKnifeTypeGroupMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LabKnifeID.Text = ""
        LoadUserPower()
        ToolStripMenuItemRefresh_Click(Nothing, Nothing)

    End Sub



    Private Sub ToolStripMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemDelete.Click
        ' TreeList1.FocusedNode.Item("���OID").ToString()
        If TreeKnifeType.Nodes.Count <= 0 Then
            Exit Sub
        End If

        If TreeKnifeType.FocusedNode.Nodes.Count <= 0 Then
        Else
            MsgBox("����R��,�����D�̤p��!", 64, "����")
            Exit Sub
        End If

        Dim strA As String = Me.TreeKnifeType.FocusedNode.Item("KnifeID").ToString()
        Dim StrB As String = Me.TreeKnifeType.FocusedNode.Item("KnifePID").ToString()

        Dim kc As New KnifeTypeGroupControl
        Dim kl As New List(Of KnifeTypeGroupInfo)
        kl = kc.KnifeMaterialCode_GetList(Nothing, strA, Nothing)
        If kl.Count <= 0 Then
        Else
            MsgBox("����R��,���������s�b�M��s�X!", 64, "����")
            Exit Sub
        End If
        ''----------------------------------------------------------
        If MsgBox("�A�T�w�n�R�� " & Me.TreeKnifeType.FocusedNode.Item("KnifeType").ToString() & " �o�����O��?", MsgBoxStyle.YesNo, "����") = MsgBoxResult.No Then Exit Sub

        If kc.KnifeTypeGroup_Delete(strA) = True Then
            MsgBox("�R�����\!", 64, "����")
            ToolStripMenuItemRefresh_Click(Nothing, Nothing)
        Else
            MsgBox("�R������!", 64, "����")
        End If
    End Sub

    Private Sub ToolStripMenuItemRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemRefresh.Click
        Dim kc As New KnifeTypeGroupControl
        TreeKnifeType.DataSource = kc.KnifeTypeGroup_GetList(Nothing, Nothing, Nothing, Nothing)
        TreeKnifeType.ExpandAll()

    End Sub

    Private Sub ToolStripMenuItemUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemUpdate.Click
        If TreeKnifeType.Nodes.Count <= 0 Then
            Exit Sub
        End If

        tempValue = Me.TreeKnifeType.FocusedNode.Item("KnifeID").ToString()
        tempValue4 = "Update"

        frmKnifeTypeGroupAdd.ShowDialog()
        frmKnifeTypeGroupAdd.Dispose()

        ToolStripMenuItemRefresh_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItemAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemAdd.Click
        If TreeKnifeType.Nodes.Count <= 0 Then
            Exit Sub
        End If

        tempValue = Me.TreeKnifeType.FocusedNode.Item("KnifeID").ToString()
        tempValue4 = "Add"

        frmKnifeTypeGroupAdd.ShowDialog()
        frmKnifeTypeGroupAdd.Dispose()

        ToolStripMenuItemRefresh_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItemAddNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemAddNext.Click
        If TreeKnifeType.Nodes.Count <= 0 Then
            Exit Sub
        End If

        tempValue = Me.TreeKnifeType.FocusedNode.Item("KnifeID").ToString()
        tempValue4 = "AddNext"

        frmKnifeTypeGroupAdd.ShowDialog()
        frmKnifeTypeGroupAdd.Dispose()

        ToolStripMenuItemRefresh_Click(Nothing, Nothing)
    End Sub

    Private Sub TreeKnifeType_FocusedNodeChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTreeList.FocusedNodeChangedEventArgs) Handles TreeKnifeType.FocusedNodeChanged
        If TreeKnifeType.Nodes.Count <= 0 Then
            Exit Sub
        End If
        If TreeKnifeType.FocusedNode.Nodes.Count <= 0 And Len(Me.TreeKnifeType.FocusedNode.Item("KnifeID").ToString()) < 6 Then
            ToolStripMenuItemAddNext.Visible = True
        Else
            ToolStripMenuItemAddNext.Visible = False
        End If

        Dim strA, strB, strC As String
        strA = Me.TreeKnifeType.FocusedNode.Item("KnifeID").ToString()
        strB = Me.TreeKnifeType.FocusedNode.Item("KnifePIDName").ToString()
        strC = Me.TreeKnifeType.FocusedNode.Item("KnifeType").ToString()

        If Len(strA) = 6 Then
            LabKnifeID.Text = strA
            LabMsg.Text = "�M��W�����" & "[" & strB & "-" & strC & "]"

            'Grid1  �d�ߥX����M��W��
            Dim kc As New KnifeTypeGroupControl
            Grid1.DataSource = kc.KnifeMaterialCode_GetList(Nothing, Trim(LabKnifeID.Text), Nothing)
        End If

    End Sub

    Private Sub ToolStripAddM_Code_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripAddM_Code.Click

        If TreeKnifeType.FocusedNode.Nodes.Count <= 0 Then
        Else
            MsgBox("�u���̤p���O,�[�J����!", 64, "����")
            Exit Sub
        End If

        tempValue = Me.TreeKnifeType.FocusedNode.Item("KnifeID").ToString()

        frmKnifeTypeGroupAddM_Code.ShowDialog()
        frmKnifeTypeGroupAddM_Code.Dispose()

        TreeKnifeType_FocusedNodeChanged(Nothing, Nothing)

    End Sub

    Private Sub ToolStripDeteleM_Code_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDeteleM_Code.Click

        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        Dim StrA As String
        StrA = Me.GridView1.GetFocusedRowCellValue("M_Code").ToString()

        If MsgBox("�A�T�w�n�R�� " & StrA & " �o�ӤM���?", MsgBoxStyle.YesNo, "����") = MsgBoxResult.No Then Exit Sub

        Dim kc As New KnifeTypeGroupControl
        If kc.KnifeMaterialCode_UpdateType(StrA, Nothing) = True Then
            MsgBox("�R�����\!", 64, "����")
            TreeKnifeType_FocusedNodeChanged(Nothing, Nothing)
        Else
            MsgBox("�R������!", 64, "����")
        End If

    End Sub


    Sub LoadUserPower()

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510913")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripAddM_Code.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510914")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripDeteleM_Code.Enabled = True
        End If

        ''------------------------------------------------------------------

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510915")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripMenuItemAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510916")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripMenuItemAddNext.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510917")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripMenuItemUpdate.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510918")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripMenuItemDelete.Enabled = True
        End If
    End Sub




End Class