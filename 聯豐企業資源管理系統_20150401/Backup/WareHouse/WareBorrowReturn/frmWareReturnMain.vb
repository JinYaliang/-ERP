Imports LFERP.Library.WareHouse
Imports LFERP.Library.WareHouse.WareBorrowReturn
Imports LFERP.SystemManager
Imports LFERP.Library.WareHouse.WareSelect

Public Class frmWareReturnMain

    Private Sub frmWareReturnMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mt As New WareHouseController
        Dim mc As New WareOut.WareOutController


        CaptionLabel.Text = "�٤M�@�~"
        'mt.WareHouse_LoadToTreeView(TreeView1, WareSelect(InUserID, "50100201"))

        ''�ȮɩT�w---------------------------------------
        'TreeView1.Nodes.Add("�b���~��").Tag = "W16"
        'TreeView1.Nodes(0).Nodes.Add("����").Tag = "W1609"
        'TreeView1.ExpandAll()
        'TreeView1.SelectedNode = TreeView1.Nodes(0).Nodes(0)
        ''-----------------------------------------------

        Dim a As New LFERP.SystemManager.PermissionModuleWarrantSubController
        Dim b As New List(Of LFERP.SystemManager.PermissionModuleWarrantSubInfo)
        b = a.PermissionModuleWarrantSub_GetList(InUserID, "50100201")

        mt.WareHouse_LoadToTreeViewIn(TreeView1, WareInSelect(InUserID, "50100201"), b.Item(0).PMWS_Value)
        '--------------------------------------------------------


        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "50100202")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripReturnAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "50100203")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripReturnAddR.Enabled = True
        End If

    End Sub

    Sub LoadReturn()
        Grid1.DataSource = Nothing
        Grid1.ContextMenuStrip = WareReturnContext
        GridView2.Columns.Clear()

        Dim StrA As String, StrB As String

        StrA = "�۰ʽs��, �٤M�y����, �٤M�渹, �M��s�X,�W��, �W��,�k�ټƶq,���,�٤M�H,�N�٤H,�ܮw�W��, �٤M���,�ާ@�H"
        StrB = "AutoID, WB_NUM, WB_NO, M_Code, M_Name, M_Gauge, Qty,WB_Action,RR_PerName,WB_Date, WH_Name,WB_Date,WB_ActionName"

        Dim StrAarray As Array = Split(StrA, ",")
        Dim StrBarray As Array = Split(StrB, ",")

        For i As Integer = 0 To UBound(StrAarray)
            Dim Coll As New DevExpress.XtraGrid.Columns.GridColumn
            Coll.Caption = Trim(StrAarray(i))
            Coll.FieldName = Trim(StrBarray(i))
            Coll.OptionsColumn.AllowEdit = False
            Coll.Visible = True
            Coll.VisibleIndex = GridView2.Columns.Count
            GridView2.Columns.Add(Coll)
        Next

    End Sub

    Private Sub ToolStripReturnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripReturnAdd.Click
        On Error Resume Next
        Edit = False
        If TreeView1.SelectedNode.Level = 1 Then
            Dim fr As frmWareReturn
            tempValue = "�٤M��"
            tempValue3 = TreeView1.SelectedNode.Tag
            tempValue4 = TreeView1.SelectedNode.Text
            fr = New frmWareReturn
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If

        '-----------------------------------------------------------
        ''On Error Resume Next
        ''Edit = False
        ''Dim fr As frmWareReturn
        ''tempValue = "�٤M��"
        ''tempValue3 = "W1609"
        ''tempValue4 = "���˭�"
        ''fr = New frmWareReturn
        ''fr.MdiParent = MDIMain
        ''fr.WindowState = FormWindowState.Maximized
        ''fr.Show()
   


    End Sub

    Private Sub ToolStripReturnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripReturnRefresh.Click
        Dim bc As New WareBorrowReturnControl
        Me.Grid1.DataSource = bc.WareBorrowReturn_GetList(Nothing, Nothing, "�٤M", Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, Nothing, Nothing)

    End Sub

    Private Sub ToolStripReturnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripReturnView.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub

        Dim fr As frmWareReturn

        tempValue = "�d��"
        tempValue5 = GridView2.GetFocusedRowCellValue("WB_NO").ToString
        fr = New frmWareReturn
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        If e.Node.Level = 1 Then
            ToolStripReturnRefresh_Click(Nothing, Nothing)
        End If
    End Sub


    Private Sub ToolStripReturnAddR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripReturnAddR.Click
        On Error Resume Next
        Edit = False
        If TreeView1.SelectedNode.Level = 1 Then
            Dim fr As frmWareReturn
            tempValue = "�N�٤M"
            tempValue3 = TreeView1.SelectedNode.Tag
            tempValue4 = TreeView1.SelectedNode.Text
            fr = New frmWareReturn
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If

        ''On Error Resume Next
        ''Edit = False
        ''Dim fr As frmWareReturn
        ''tempValue = "�N�٤M"
        ''tempValue3 = "W1609"
        ''tempValue4 = "���˭�"
        ''fr = New frmWareReturn
        ''fr.MdiParent = MDIMain
        ''fr.WindowState = FormWindowState.Maximized
        ''fr.Show()
 
    End Sub

    Private Sub ToolStripReturnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripReturnFind.Click

        If TreeView1.SelectedNode.Level = 1 Then
        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If

        Dim mc As New WareBorrowReturnControl
        Dim fr As New frmWareSelect
        tempValue = "�٤M�@�~"
        tempValue4 = TreeView1.SelectedNode.Tag
        fr.ShowDialog()

        Select Case tempValue
            Case "1"
                Grid1.DataSource = mc.WareBorrowReturn_GetList(Nothing, tempValue2, "�٤M", Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "2"
                Grid1.DataSource = mc.WareBorrowReturn_GetList(Nothing, Nothing, "�٤M", tempValue2, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "4"
                Grid1.DataSource = mc.WareBorrowReturn_GetList(tempValue2, Nothing, "�٤M", Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "6"
                Dim ws As New WareSelectControl
                Grid1.DataSource = ws.WareBorrowReturn_Getlist("�٤M�@�~", tempValue2)
        End Select
        tempValue = ""
        tempValue2 = ""
    End Sub

    Private Sub ToolStripReturnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripReturnPrint.Click
       
        If GridView2.RowCount = 0 Then Exit Sub

        Dim ds As New DataSet
        Dim strA As String

        Dim ltc As New CollectionToDataSet
        strA = GridView2.GetFocusedRowCellValue("WB_NO").ToString

        Dim bc As New WareBorrowReturnControl
        ltc.CollToDataSet(ds, "WareBorrowReturn", bc.WareBorrowReturn_GetList(Nothing, strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        PreviewRPT(ds, "rptWareReturn", "�٤M��", True, True)
        ltc = Nothing
    End Sub

    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToolStripMenuItem.Click
        If GridView2.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "�ɥXExcel"
        saveFileDialog.Filter = "Excel2003���(*.xls)|*.xls"
        '|Excel2007�ΥH�W���(*.xlsx)|*.xlsx  '��e����2007 �H�ΥH�W�������~�I
        Dim dialogResult__1 As DialogResult = saveFileDialog.ShowDialog(Me)
        If dialogResult__1 = Windows.Forms.DialogResult.OK Then
            GridView2.BestFitColumns()
            Grid1.ExportToExcelOld(saveFileDialog.FileName)
            DevExpress.XtraEditors.XtraMessageBox.Show("�O�s���\�I", "����", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub CopyALLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyALLToolStripMenuItem.Click
        Dim FiledNameStr As String
        FiledNameStr = "WB_NUM,WB_NO,M_Code,M_Name,M_Gauge,Qty,WB_Action,RR_PerName,WH_Name,DPT_Name,WB_Date,WB_ActionName"
        GridViewCopyMulitRow(GridView2, FiledNameStr, "ALL")
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        Dim FiledNameStr As String
        FiledNameStr = "WB_NUM,WB_NO,M_Code,M_Name,M_Gauge,Qty,WB_Action,RR_PerName,WH_Name,DPT_Name,WB_Date,WB_ActionName"
        GridViewCopyMulitRow(GridView2, FiledNameStr, "")
    End Sub
End Class