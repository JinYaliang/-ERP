Imports LFERP.Library.WareHouse.WareMove
Imports LFERP.Library.WareHouse
Imports LFERP.SystemManager
Imports LFERP.Library.WareHouse.WareSelect
Imports LFERP.Library.KnifeWare
'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Core

Public Class frmKnifeWareMoveMain
    Private LableText As String
    Private Sub frmKnifeWareMoveMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mt As New WareHouseController
        'mt.WareHouse_LoadToTreeView(TreeView1, WareSelect(InUserID, "510306"))
        KnifeWareTreeView = TreeView1
        KnifeWareBarManager = BarManager1
        KnifeWareLoad(ImageList1, "510306")
        'isBarCode = False
        LoadUserPower()
        'tv1.CollapseAll()
    End Sub

    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510301")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510302")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510303")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510304")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdIn.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510305")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510307")
        If pmwiL.Count > 0 Then
            '  If pmwiL.Item(0).PMWS_Value = "�O" Then isBarCode = True
        End If

        '2014-02-28   ���@  �ק�f���v���Ƶ�
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510311")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdRemarkEdit.Enabled = True : cmdRemarkEdit.Visible = True
        End If



        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510310")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdAddKnife.Enabled = True
        End If

    End Sub
    Private Sub popKnifeWareMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click, cmdView.Click, cmdSeek.Click, cmdPrint.Click, cmdLook.Click, cmdIn.Click, cmdflesh.Click, cmdEdit.Click, cmdDel.Click, cmdCopyAll.Click, cmdCopy.Click, cmdCheck.Click, cmdAddKnife.Click, cmdRemarkEdit.Click
        Select Case sender.Name
            Case "cmdAdd"
                On Error Resume Next
                If TreeView1.SelectedNode Is Nothing Then
                    MsgBox("�п�ܥ��T���ܮw!", 64, "����")
                    Exit Sub
                Else
                    Dim fr As frmKnifeWareMoveOut
                    For Each fr In MDIMain.MdiChildren
                        If TypeOf fr Is frmKnifeWareMoveOut Then
                            fr.Activate()
                            Exit Sub
                        End If
                    Next
                    tempValue2 = TreeView1.SelectedNode.Tag
                    tempValue3 = TreeView1.SelectedNode.Text

                    fr = New frmKnifeWareMoveOut
                    fr.EditItem = "KnifeWareMoveAddNew"
                    'fr.isBarCode = isBarCode
                    fr.MdiParent = MDIMain
                    fr.WindowState = FormWindowState.Maximized
                    fr.Show()
                End If
            Case "cmdAddKnife"
                On Error Resume Next
                If TreeView1.SelectedNode Is Nothing Then
                    MsgBox("�п�ܥ��T���ܮw!", 64, "����")
                    Exit Sub
                Else
                    Dim fr As frmKnifeWareMoveOut
                    For Each fr In MDIMain.MdiChildren
                        If TypeOf fr Is frmKnifeWareMoveOut Then
                            fr.Activate()
                            Exit Sub
                        End If
                    Next

                    tempValue2 = TreeView1.SelectedNode.Tag
                    tempValue3 = TreeView1.SelectedNode.Text

                    fr = New frmKnifeWareMoveOut
                    fr.EditItem = "KnifeWareMoveAddOld"
                    'fr.isBarCode = isBarCode
                    fr.MdiParent = MDIMain
                    fr.WindowState = FormWindowState.Maximized
                    fr.Show()
                End If
            Case "cmdEdit" '-------------------------------------------------------------�ק�
                On Error Resume Next
                Dim osc As New KnifeWareMoveController
                Dim osilist As New List(Of KnifeWareMoveInfo)
                osilist = osc.KnifeWareMove_GetList(GridView1.GetFocusedRowCellValue("MV_NO").ToString, Nothing, Nothing, Nothing, Nothing, "����", True, Nothing, "1", Nothing)
                If osilist.Count <> 0 Then
                    MsgBox("�L�k�ק�,���ռ���w�T�{�I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "����")
                    Exit Sub
                End If
                osilist = osc.KnifeWareMove_GetList(GridView1.GetFocusedRowCellValue("MV_NO").ToString, Nothing, Nothing, Nothing, Nothing, "����", Nothing, True, "1", Nothing)
                If osilist.Count <> 0 Then
                    MsgBox("�L�k�ק�,���禬��w�f�֡I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "����")
                    Exit Sub
                End If
                If GridView1.GetFocusedRowCellValue("MV_InOrOut").ToString <> "�o��" Then
                    MsgBox("�z�����'�o��'�ʽ誺�O���i��ק�R���ާ@�I", MsgBoxStyle.OkOnly, "����")
                    Exit Sub
                End If
                tempValue = GridView1.GetFocusedRowCellValue("MV_NO").ToString

                Dim fr As frmKnifeWareMoveOut
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmKnifeWareMoveOut Then
                        fr.Activate()
                        Exit Sub
                    End If
                Next
                fr = New frmKnifeWareMoveOut
                fr.EditItem = "KnifeWareMoveEdit"
                fr.KnifeType = GridView1.GetFocusedRowCellValue("KnifeType").ToString
                fr.EditID = GridView1.GetFocusedRowCellValue("MV_NO").ToString
                fr.MdiParent = MDIMain
                ' fr.isBarCode = isBarCode
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            Case "cmdDel" '-----------------------------------------------------------------------�R��
                On Error Resume Next
                Dim osc As New LFERP.Library.KnifeWare.KnifeWareMoveController
                Dim osilist As New List(Of LFERP.Library.KnifeWare.KnifeWareMoveInfo)
                osilist = osc.KnifeWareMove_GetList(GridView1.GetFocusedRowCellValue("MV_NO").ToString, Nothing, Nothing, Nothing, Nothing, "����", True, Nothing, "1", Nothing)
                If osilist.Count <> 0 Then
                    MsgBox("�L�k�R��,���ռ���w�T�{�I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "����")
                    Exit Sub
                End If
                osilist = osc.KnifeWareMove_GetList(GridView1.GetFocusedRowCellValue("MV_NO").ToString, Nothing, Nothing, Nothing, Nothing, "����", Nothing, True, "1", Nothing)
                If osilist.Count <> 0 Then
                    MsgBox("�L�k�R��,���ռ���w�f�֡I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "����")
                    Exit Sub
                End If
                If GridView1.GetFocusedRowCellValue("MV_InOrOut").ToString <> "�o��" Then
                    MsgBox("�z�����'�o��'�ʽ誺�O���i��ק�R���ާ@�I", MsgBoxStyle.OkOnly, "����")
                    Exit Sub
                End If
                Dim StrA As String
                StrA = GridView1.GetFocusedRowCellValue("MV_NO").ToString
                If MsgBox("�A�T�w�R���ռ��渹��  '" & StrA & "'  ���O����?", MsgBoxStyle.YesNo, "����") = MsgBoxResult.Yes Then
                    Dim mc As New KnifeWareMoveInfo
                    Dim mt As New KnifeWareMoveController
                    mc.MV_NO = StrA
                    If mt.KnifeWareMove_DelNO(mc) = True Then
                        Grid1.DataSource = mt.KnifeWareMove_GetList(Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, Nothing, "2", Nothing)
                    Else
                        MsgBox("�R������", 64, "����")
                    End If
                End If
            Case "cmdView" '-------------------------------------------------------------------�d��
                On Error Resume Next
                'tempValue = GridView1.GetFocusedRowCellValue("MV_NO").ToString
                ' MTypeName = "WareMoveView"
                Dim fr As frmKnifeWareMoveOut
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmKnifeWareMoveOut Then
                        fr.Activate()
                        Exit Sub
                    End If
                Next

                fr = New frmKnifeWareMoveOut
                fr.EditItem = "KnifeWareMoveView"
                fr.EditID = GridView1.GetFocusedRowCellValue("MV_NO").ToString
                fr.MdiParent = MDIMain
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            Case "cmdLook"
            Case "cmdIn"
                On Error Resume Next
                If GridView1.RowCount = 0 Then Exit Sub

                '2013
                If CheckWHID(GridView1.GetFocusedRowCellValue("DepotNO").ToString) = False Then
                    MsgBox("���Τ�L���ܮw�����v��")
                    Exit Sub
                End If

                ' tempValue = GridView1.GetFocusedRowCellValue("MV_NO").ToString
                Dim a As New KnifeWareMoveController
                Dim b As Integer = a.KnifeWareMove_GetList(GridView1.GetFocusedRowCellValue("MV_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, "1", Nothing).Count

                If b > 0 Then
                    MsgBox("����w�f��,�L�k�A���f�I", MsgBoxStyle.OkOnly, "����")
                    Exit Sub
                End If

                If GridView1.GetFocusedRowCellValue("MV_InOrOut").ToString <> "����" Then
                    MsgBox("�z�����'����'�ʽ誺�O�����f�T�{�ާ@�I", MsgBoxStyle.OkOnly, "����")
                    Exit Sub
                End If
                ' MTypeName = "WareMoveIn"
                Dim fr As frmKnifeWareMoveOut
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmKnifeWareMoveOut Then
                        fr.Activate()
                        Exit Sub
                    End If
                Next
                fr = New frmKnifeWareMoveOut
                fr.EditItem = "KnifeWareMoveIn"
                fr.EditID = GridView1.GetFocusedRowCellValue("MV_NO").ToString
                fr.MdiParent = MDIMain
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            Case "cmdCheck"
                '�f��
                On Error Resume Next
                If GridView1.RowCount = 0 Then Exit Sub

                '2013
                If CheckWHID(GridView1.GetFocusedRowCellValue("DepotNO").ToString) = False Then
                    MsgBox("���Τ�L���ܮw�����v��")
                    Exit Sub
                End If


                'tempValue = GridView1.GetFocusedRowCellValue("MV_NO").ToString
                Dim a As New KnifeWareMoveController
                Dim b As New List(Of KnifeWareMoveInfo)
                '      b = a.WareMove_GetList(tempValue, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, "2")
                b = a.KnifeWareMove_GetList(GridView1.GetFocusedRowCellValue("MV_NO").ToString, Nothing, Nothing, Nothing, Nothing, "����", True, Nothing, "1", Nothing)
                Dim c As New List(Of KnifeWareMoveInfo)
                c = a.KnifeWareMove_GetList(GridView1.GetFocusedRowCellValue("MV_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, "1", Nothing)
                If c.Count > 0 Then
                    MsgBox("����w�f��,�����\�A���ާ@�I", MsgBoxStyle.OkOnly, "����")
                    Exit Sub
                End If

                If b.Count = 0 Then
                    MsgBox("�f�֤��e�ݭn�����f!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "����")
                    Exit Sub
                End If

                If GridView1.GetFocusedRowCellValue("MV_InOrOut").ToString <> "����" Then
                    MsgBox("�z�����'����'�ʽ誺�O���i��f�־ާ@�I", MsgBoxStyle.OkOnly, "����")
                    Exit Sub
                End If
                '     Edit = True
                Dim fr As frmKnifeWareMoveOut
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmKnifeWareMoveOut Then
                        fr.Activate()
                        Exit Sub
                    End If
                Next
                fr = New frmKnifeWareMoveOut
                fr.EditItem = "KnifeWareMoveCheck"
                fr.EditID = GridView1.GetFocusedRowCellValue("MV_NO").ToString
                fr.MdiParent = MDIMain
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            Case "cmdflesh"
            Case "cmdSeek"
                On Error Resume Next
                Dim wmc As New LFERP.Library.KnifeWare.KnifeWareMoveController
                Dim fr As New frmKnifeSelect
                fr.ComboBoxEdit1.Properties.Items.Add("�ռ��渹")
                tempValue = "�M��ռ�"
                tempValue4 = TreeView1.SelectedNode.Tag
                fr.ShowDialog()
                If RefreshT = True Then
                    Select Case tempValue
                        Case 1
                            Grid1.DataSource = wmc.KnifeWareMove_GetList(tempValue2, Nothing, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                        Case 2
                            Grid1.DataSource = wmc.KnifeWareMove_GetList(Nothing, tempValue2, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                        Case 3
                            Grid1.DataSource = wmc.KnifeWareMove_GetList(Nothing, Nothing, tempValue2, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                        Case 4
                            Grid1.DataSource = wmc.KnifeWareMove_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                        Case 5
                            Grid1.DataSource = wmc.KnifeWareMove_GetList(Nothing, Nothing, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2)
                        Case 6
                            Dim ws As New KnifeWareSelectController
                            Grid1.DataSource = ws.WareMove_Getlist("�M��ռ�", tempValue2)
                            RefreshT = False
                    End Select
                    tempValue = ""
                    tempValue2 = ""
                    tempValue4 = ""
                End If
            Case "cmdPrint"
                Dim dss As New DataSet
                Dim ltc As New CollectionToDataSet
                Dim mc As New KnifeWareMoveController

                'Dim strSO_No As String
                'strSO_No = GridView1.GetFocusedRowCellValue("WIP_ID").ToString

                ltc.CollToDataSet(dss, "KnifeWareMove", mc.KnifeWareMove_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))

                PreviewRPT(dss, "rptKnifeWareMove", "�M��ռ���", True, True)
                ltc = Nothing
            Case "cmdCopy"
            Case "cmdCopyAll"
            Case "cmdRemarkEdit"         '2014-02-28   ���@
                On Error Resume Next


                Dim fr As frmKnifeWareMoveOut
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmKnifeWareMoveOut Then
                        fr.Activate()
                        Exit Sub
                    End If
                Next
                fr = New frmKnifeWareMoveOut
                fr.EditItem = "KnifeWareMoveRemarkEdit"
                fr.KnifeType = GridView1.GetFocusedRowCellValue("KnifeType").ToString
                fr.EditID = GridView1.GetFocusedRowCellValue("MV_NO").ToString
                fr.MdiParent = MDIMain

                fr.WindowState = FormWindowState.Maximized
                fr.Show()

        End Select
    End Sub



    Function CheckWHID(ByVal strSelectWHID As String) As Boolean
        CheckWHID = False

        Dim strID As String
        ''-------------------------
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510309")
        If pmwiL.Count > 0 Then
            strID = pmwiL.Item(0).PMWS_Value
        Else
            Exit Function
        End If
        ''-------------------------
        Dim arrWHID() As String
        arrWHID = strID.Split(",")
        Dim i As Integer
        For i = 0 To arrWHID.Length - 1
            If strSelectWHID = arrWHID(i) Then
                CheckWHID = True
                Exit Function
            Else

            End If
        Next
    End Function

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

        tempValue2 = ""
        Dim wmc As New KnifeWareMoveController
        Dim wmi As New KnifeWareMoveInfo
        If e.Node.Level = 0 Then
            Grid1.DataSource = wmc.KnifeWareMove_GetList1(Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, Nothing, "2", DateAdd(DateInterval.Day, -30, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
            tempValue2 = TreeView1.SelectedNode.Tag

            tv1.ExpandAll() 'TV1�i�}�Ҧ��ؿ�

            Dim a As New KnifeWareMoveController
            Dim b, c As Integer
            'Dim b As New List(Of KnifeWareMoveInfo)
            'Dim c As New List(Of KnifeWareMoveInfo)

            b = a.KnifeWareMove_GetList(Nothing, Nothing, Nothing, tempValue2, Nothing, "�o��", Nothing, False, "2", Nothing).Count
            c = a.KnifeWareMove_GetList(Nothing, Nothing, Nothing, tempValue2, Nothing, "����", Nothing, False, "2", Nothing).Count
            If b > 0 Then
                tv1.Nodes.Item(1).Nodes.Item(0).Text = "���f�� (" & b & ")"
                tv1.Nodes.Item(1).Nodes.Item(0).ForeColor = Color.Green
            Else

                tv1.Nodes.Item(1).Nodes.Item(0).Text = "���f��"
                tv1.Nodes.Item(1).Nodes.Item(0).ForeColor = Color.Black

            End If
            If c > 0 Then
                tv1.Nodes.Item(0).Nodes.Item(0).Text = "���f�� (" & c & ")"
                tv1.Nodes.Item(0).Nodes.Item(0).ForeColor = Color.Green
            Else

                tv1.Nodes.Item(0).Nodes.Item(0).Text = "���f��"
                tv1.Nodes.Item(0).Nodes.Item(0).ForeColor = Color.Black

            End If
        End If
        tv1.Nodes.Item(1).Text = "�o�X����"

    End Sub

    Private Sub tv1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv1.AfterSelect
        Dim wmc As New KnifeWareMoveController
        Dim wmi As New KnifeWareMoveInfo
        If e.Node.Level = 1 Then
            Select Case Mid(tv1.SelectedNode.Text, 1, 3)
                Case "���f��"
                    If tv1.SelectedNode.Parent.Text = "���J����" Then
                        Grid1.DataSource = wmc.KnifeWareMove_GetList1(Nothing, Nothing, Nothing, tempValue2, Nothing, "����", Nothing, False, "2", Nothing, Nothing)
                    End If
                    If tv1.SelectedNode.Parent.Text = "�o�X����" Then
                        Grid1.DataSource = wmc.KnifeWareMove_GetList1(Nothing, Nothing, Nothing, tempValue2, Nothing, "�o��", Nothing, False, "2", Nothing, Nothing)
                    End If
                Case "�w�f��"
                    If tv1.SelectedNode.Parent.Text = "���J����" Then
                        Grid1.DataSource = wmc.KnifeWareMove_GetList1(Nothing, Nothing, Nothing, tempValue2, Nothing, "����", Nothing, True, "2", DateAdd(DateInterval.Day, -30, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
                    End If
                    If tv1.SelectedNode.Parent.Text = "�o�X����" Then
                        Grid1.DataSource = wmc.KnifeWareMove_GetList1(Nothing, Nothing, Nothing, tempValue2, Nothing, "�o��", Nothing, True, "2", DateAdd(DateInterval.Day, -30, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
                    End If
            End Select
            '    Label1.Text = Mid(tv1.SelectedNode.Text, 1, 3) & tv1.SelectedNode.Parent.Text
            ' Grid1.DataSource = wmc.WareMove_GetList("2")
        End If
    End Sub

    'Private Sub frmKnifeWareMoveMain_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    '    'KnifeWareTreeView = TreeView1
    '    'KnifeWareBarManager = BarManager1
    '    'KnifeWareLoad(ImageList1, "510306")
    '    'tv1.ExpandAll()
    'End Sub

    Private Sub TreeView1_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        Dim str2 As String = TreeView1.SelectedNode.Tag
    End Sub

    Private Sub ToolStripMoveRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMoveRecord.Click
        frmBrrowList.ReportTypeID = "KnifeWareMove"
        frmBrrowList.ReportTypeName = "�M��ռ��O��"
        frmBrrowList.ShowDialog()
        frmBrrowList.Dispose()
    End Sub
End Class