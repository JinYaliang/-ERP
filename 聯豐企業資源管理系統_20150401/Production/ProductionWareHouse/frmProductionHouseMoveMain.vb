Imports LFERP.Library.ProductionWareMove
Imports LFERP.Library.ProductionWareHouse
Imports LFERP.SystemManager
Imports LFERP.Library.WareHouse.WareHouseController

Public Class frmProductionHouseMoveMain

    '���J�֦��v�����ܮw
    Private Sub frmProductionHouseMoveMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mt As New Library.WareHouse.WareHouseController

        mt.WareHouse_LoadToTreeView(TreeView1, WareSelect(InUserID, "880606"))
        LoadUserPower()

        tv1.CollapseAll()
    End Sub

    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880601")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareMoveOutAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880602")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareMoveEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880603")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareMoveDel.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880604")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareMoveIn.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880605")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareMoveCheck.Enabled = True
        End If

    End Sub

    '�����e�ܮw���ռ��H��
    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        tempValue2 = ""
        Dim wmc As New ProductionWareMoveControl
        Dim wmi As New ProductionWareMoveInfo
        'If e.Node.Level = 1 Then
        If TreeView1.SelectedNode.Level = 1 Then
            'Grid1.DataSource = wmc.ProductionWareMove_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, "2")
            Grid1.DataSource = Nothing
            tempValue2 = TreeView1.SelectedNode.Tag

            tv1.ExpandAll() 'TV1�i�}�Ҧ��ؿ�

            Dim a As New ProductionWareMoveControl
            Dim b As New List(Of ProductionWareMoveInfo)
            Dim c As New List(Of ProductionWareMoveInfo)

            b = a.ProductionWareMove_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, True, Nothing, False)
            c = a.ProductionWareMove_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, True, Nothing, False)
            If b.Count > 0 Then
                tv1.Nodes.Item(1).Nodes.Item(0).Text = "���f�� (" & b.Count & ")"
            Else
                tv1.Nodes.Item(1).Nodes.Item(0).Text = "���f��(0)"
            End If

            If c.Count > 0 Then
                tv1.Nodes.Item(0).Nodes.Item(0).Text = "���f�� (" & c.Count & ")"
            Else
                tv1.Nodes.Item(0).Nodes.Item(0).Text = "���f��(0)"
            End If
            tv1.Select()
        End If
    End Sub

    '�����e�Ҧ��U�ԲӪ��ƫH��
    Private Sub tv1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv1.AfterSelect
        Dim wmc As New ProductionWareMoveControl
        Dim wmi As New ProductionWareMoveInfo
        If tv1.SelectedNode.Level = 0 Then
            If tv1.SelectedNode.Text = "���J����" Then

                Grid1.DataSource = wmc.ProductionWareMove_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, Nothing, Nothing)

                popWareMoveEdit.Visible = False
                popWareMoveDel.Visible = False
                popWareMoveIn.Visible = True
                popWareMoveCheck.Visible = True
                ToolStripSeparator1.Visible = True

            ElseIf tv1.SelectedNode.Text = "�o�X����" Then

                Grid1.DataSource = wmc.ProductionWareMove_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, Nothing)

                popWareMoveEdit.Visible = True
                popWareMoveDel.Visible = True
                popWareMoveIn.Visible = False
                popWareMoveCheck.Visible = False
                ToolStripSeparator1.Visible = False
            End If

        ElseIf tv1.SelectedNode.Level = 1 Then
            If tv1.SelectedNode.Parent.Text = "���J����" Then

                If Mid(tv1.SelectedNode.Text, 1, 3) = "���f��" Then
                    Grid1.DataSource = wmc.ProductionWareMove_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, Nothing, False)
                ElseIf Mid(tv1.SelectedNode.Text, 1, 3) = "�w�f��" Then
                    Grid1.DataSource = wmc.ProductionWareMove_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, Nothing, True)
                End If

                popWareMoveEdit.Visible = False
                popWareMoveDel.Visible = False
                popWareMoveIn.Visible = True
                popWareMoveCheck.Visible = True
                ToolStripSeparator1.Visible = True

            ElseIf tv1.SelectedNode.Parent.Text = "�o�X����" Then

                If Mid(tv1.SelectedNode.Text, 1, 3) = "���f��" Then
                    Grid1.DataSource = wmc.ProductionWareMove_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, False)
                ElseIf Mid(tv1.SelectedNode.Text, 1, 3) = "�w�f��" Then
                    Grid1.DataSource = wmc.ProductionWareMove_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, True)
                End If

                popWareMoveEdit.Visible = True
                popWareMoveDel.Visible = True
                popWareMoveIn.Visible = False
                popWareMoveCheck.Visible = False
                ToolStripSeparator1.Visible = False

            End If
        End If
    End Sub

    Private Sub popWareMoveOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveOutAdd.Click
        On Error Resume Next
        Edit = False
        If TreeView1.SelectedNode.Level = 1 Then
            Dim fr As frmProductionWareMove
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionWareMove Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            fr = New frmProductionWareMove
            MTypeName = "ProductionWareMove"
            tempValue2 = TreeView1.SelectedNode.Tag
            tempValue3 = TreeView1.SelectedNode.Text
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()

        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If
     
    End Sub

    Private Sub popWareMoveEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveEdit.Click
        On Error Resume Next
        Dim osc As New ProductionWareMoveControl
        Dim osilist As New List(Of ProductionWareMoveInfo)

        If GridView1.RowCount = 0 Then Exit Sub

        osilist = osc.ProductionWareMove_GetList(GridView1.GetFocusedRowCellValue("PWM_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, Nothing)
        If osilist.Count > 0 Then

            MsgBox("�L�k�ק�,���ռ���w�T�{�I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        'osilist = osc.ProductionWareMove_GetList(GridView1.GetFocusedRowCellValue("PWM_NO").ToString, Nothing, Nothing, Nothing, "����", Nothing, Nothing, Nothing, True, "1")
        'If osilist.Count > 0 Then

        '    MsgBox("�L�k�ק�,���禬��w�f�֡I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        '    Exit Sub
        'End If

        'If GridView1.GetFocusedRowCellValue("PWM_Type").ToString <> "�o��" Then
        '    MsgBox("�z�����'�o��'�ʽ誺�O���i��ק�R���ާ@�I", MsgBoxStyle.OkOnly)
        '    Exit Sub
        'End If

        Edit = True
    
        Dim fr As frmProductionWareMove
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionWareMove Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionWareMove
        tempValue = GridView1.GetFocusedRowCellValue("PWM_NO").ToString
        MTypeName = "ProductionWareMove"
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popWareMoveDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveDel.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim osc As New ProductionWareMoveControl
        Dim osilist As New List(Of ProductionWareMoveInfo)


        osilist = osc.ProductionWareMove_GetList(GridView1.GetFocusedRowCellValue("PWM_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, Nothing)
        If osilist.Count <> 0 Then

            MsgBox("���ռ���w�T�{�A����R���I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        'osilist = osc.ProductionWareMove_GetList(GridView1.GetFocusedRowCellValue("PWM_NO").ToString, Nothing, Nothing, Nothing, "����", Nothing, Nothing, Nothing, True, "1")
        'If osilist.Count <> 0 Then

        '    MsgBox("�L�k�ק�,���ռ���w�f�֡I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        '    Exit Sub
        'End If


        'If GridView1.GetFocusedRowCellValue("PWM_Type").ToString <> "�o��" Then
        '    MsgBox("�z�����'�o��'�ʽ誺�O���i��ק�R���ާ@�I", MsgBoxStyle.OkOnly)
        '    Exit Sub
        'End If


        Dim StrA As String
        StrA = GridView1.GetFocusedRowCellValue("PWM_NO").ToString
        If MsgBox("�A�T�w�R���ռ��渹��  '" & StrA & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            Dim mt As New ProductionWareMoveControl

            If mt.ProductionWareMove_Delete(StrA, Nothing) = True Then
                MsgBox("�R�����\�I", 64, "����")
                TreeView1_AfterSelect(Nothing, Nothing)
                tv1_AfterSelect(Nothing, Nothing)
            Else
                MsgBox("�R�����ѡI", 64, "����")
            End If

        End If
    End Sub

    Private Sub popWareMoveView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As frmProductionWareMove
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionWareMove Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionWareMove

        tempValue = GridView1.GetFocusedRowCellValue("PWM_NO").ToString
        MTypeName = "PreView"
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popWareMoveIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveIn.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        tempValue = GridView1.GetFocusedRowCellValue("PWM_NO").ToString
        Dim a As New ProductionWareMoveControl
        Dim b As New List(Of ProductionWareMoveInfo)
        b = a.ProductionWareMove_GetList(tempValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
        If b.Count > 0 Then
            MsgBox("����w�f��,�L�k�A���f�I", MsgBoxStyle.OkOnly)
            Exit Sub
        End If

        Dim fr As frmProductionWareMove
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionWareMove Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionWareMove
        MTypeName = "InCheck"
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popWareMoveCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveCheck.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        tempValue = GridView1.GetFocusedRowCellValue("PWM_NO").ToString
        Dim a As New ProductionWareMoveControl
        Dim b As New List(Of ProductionWareMoveInfo)

        b = a.ProductionWareMove_GetList(tempValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing)

        If b.Count > 0 Then
            MsgBox("�f�֤��e�ݭn���T�{���f!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        'If GridView1.GetFocusedRowCellValue("PWM_Type").ToString <> "����" Then
        '    MsgBox("�z�����'����'�ʽ誺�O���i��f�־ާ@�I", MsgBoxStyle.OkOnly)
        '    Exit Sub
        'End If

        Dim fr As frmProductionWareMove
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionWareMove Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionWareMove
        MTypeName = "Check"
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popWareMoveflesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveflesh.Click
        TreeView1_AfterSelect(Nothing, Nothing)
        tv1_AfterSelect(Nothing, Nothing)
    End Sub
End Class