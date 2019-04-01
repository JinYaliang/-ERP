Imports LFERP.Library.WareHouse
Imports LFERP.Library.WareHouse.WareOut
Imports LFERP.SystemManager
Imports LFERP.DataSetting
Imports LFERP.Library.Shared
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core
Imports LFERP.Library.WareHouse.WareSelect
Imports DevExpress.XtraGrid.Columns



Public Class frmWareOutMain

    Private Sub frmWareOutMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim mt As New WareHouseController
        Dim mc As New WareOut.WareOutController

        mt.WareHouse_LoadToTreeView(TreeView1, WareSelect(InUserID, "500205"))
        LoadUserPower()

        popWareOutEdit.Visible = False
        popWareOutDel.Visible = False
        popWareOutCheck.Visible = False

    End Sub

    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500201")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500202")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500203")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500204")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500206")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutReCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500207")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutAdd_Process.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500208")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then WareoutcollToolStripMenuItem.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500209")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutAddBarCode.Enabled = True
        End If

        ''�ק�ƪ`
        ModifyRemarkTool.Visible = False
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500210")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ModifyRemarkTool.Visible = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500215")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripMenuItem1.Visible = True Else ToolStripMenuItem1.Visible = False
        End If

        'ToolStripMenuItemPD
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500216")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripMenuItemPD.Visible = True
        End If


    End Sub

    Private Sub popWareOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutAdd.Click
        On Error Resume Next
        Edit = False
        If TreeView1.SelectedNode.Level = 1 Then
            Dim fr As frmWareOut
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmWareOut Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue = "�X�w��"
            tempValue3 = TreeView1.SelectedNode.Tag
            tempValue4 = TreeView1.SelectedNode.Text
            fr = New frmWareOut
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If
     
    End Sub

    Private Sub popWareOutEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutEdit.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub


        Dim mc As New WareOut.WareOutController
        Dim mi As New WareOut.WareOutInfo
        mi = mc.WareOut_GetNUM(GridView2.GetFocusedRowCellValue("WO_NUM").ToString)

        If IsDBNull(mi.WO_Check) = True Or mi.WO_Check = False Then
            Edit = True
            Dim fr As frmWareOut
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmWareOut Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            tempValue = "�X�w��"
            tempValue2 = GridView2.GetFocusedRowCellValue("WO_ID").ToString
            fr = New frmWareOut
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
            'Dim myfrm As New FrmWareOut
            'myfrm.ShowDialog()
        Else
            MsgBox("����w�Q�f�֡A�����\�ק�")
            Exit Sub

        End If
    End Sub

    Private Sub popWareOutDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutDel.Click
        On Error Resume Next
        If GridView2.RowCount = 0 Then Exit Sub
        Dim mc As New WareOut.WareOutController
        Dim mi As New WareOut.WareOutInfo
        mi = mc.WareOut_GetNUM(GridView2.GetFocusedRowCellValue("WO_NUM").ToString)
        If mi.WO_Check = False Then

            Dim strDate, strDate1 As Date
            strDate = Format(Now, "yyyy/MM")
            strDate1 = Format(mi.WO_AddDate, "yyyy/MM")

            If strDate = strDate1 Then
                If MsgBox("�T�w�R���渹��" & GridView2.GetFocusedRowCellValue("WO_ID").ToString & "�������O���H", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    If mc.WareOut_Delete(Nothing, GridView2.GetFocusedRowCellValue("WO_ID").ToString) = True Then
                        MsgBox("�R�����\")
                        Grid1.DataSource = mc.WareOut_Getlist5(Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag.ToString, Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), ">", Nothing, Nothing, Nothing)
                    End If
                End If
            Else
                MsgBox("���O���X�w��,��e�����\�R��!")
                Exit Sub
            End If

        Else
            MsgBox("�ӳ�w�f�֡A�����\�R��")
        End If

    End Sub

    Private Sub popWareOutCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutCheck.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub
        Dim mc As New WareOut.WareOutController
        Dim mi As New WareOut.WareOutInfo
        mi = mc.WareOut_GetNUM(GridView2.GetFocusedRowCellValue("WO_NUM").ToString)
        If mi.WO_ReCheck = True Then
            MsgBox("���X�w��w�_��,�����\�����f��!")
            Exit Sub
        Else
            If mi.WO_Check = False Then
                MsgBox("���X�w���٥��f��,�����\�����f��!")
                Exit Sub
            Else
                Dim fr As frmWareOut
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmWareOut Then
                        fr.Activate()
                        Exit Sub
                    End If
                Next

                tempValue = "�f��"
                tempValue2 = GridView2.GetFocusedRowCellValue("WO_ID").ToString
                fr = New frmWareOut
                fr.MdiParent = MDIMain
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            End If

        End If

    End Sub

    Private Sub popwareOutflesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popwareOutflesh.Click
        Dim mc As New WareOut.WareOutController

        Grid1.DataSource = mc.WareOut_Getlist5(Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag.ToString, Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), ">", Nothing, Nothing, Nothing)
    End Sub

    Private Sub popWareOutSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutSeek.Click
        Dim mc As New WareOut.WareOutController

        Dim fr As New frmWareSelect
        tempValue = Label1.Text
        tempValue4 = TreeView1.SelectedNode.Tag
        fr.ShowDialog()
    
        Select Case tempValue
            Case "1"
                Grid1.DataSource = mc.WareOut_Getlist5(tempValue2, Nothing, Nothing, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "2"
                Grid1.DataSource = mc.WareOut_Getlist5(Nothing, Nothing, tempValue2, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "3"
                Grid1.DataSource = mc.WareOut_Getlist5(Nothing, Nothing, tempValue2, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "4"
                Grid1.DataSource = mc.WareOut_Getlist5(Nothing, Nothing, Nothing, tempValue2, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "5"
                Grid1.DataSource = mc.WareOut_Getlist5(Nothing, Nothing, tempValue2, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "6"
                Dim ws As New WareSelectControl
                Grid1.DataSource = ws.WareOut_Getlist("�X�w�@�~", tempValue2)

        End Select
        tempValue = ""
        tempValue2 = ""
        tempValue4 = ""
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim mc As New WareOut.WareOutController
        If e.Node.Level = 1 Then


            ''***�P�_�Τ�O�֦����ܧO���v��
            'Dim pmws As New PermissionModuleWarrantSubController
            'Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

            'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500205")
            'If pmwiL.Count > 0 Then
            '    If InStr(pmwiL.Item(0).PMWS_Value, Mid(TreeView1.SelectedNode.Tag, 1, 3)) = 0 Then
            '        MsgBox("�A�S�����������s�W�έק��v��")
            '        Exit Sub
            '    End If

            'End If
            ''************

            Grid1.DataSource = mc.WareOut_Getlist5(Nothing, Nothing, Nothing, Nothing, TreeView1.SelectedNode.Tag, Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), ">", Nothing, Nothing, Nothing)


        End If
    End Sub


    Private Sub popwareOutView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popwareOutView.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub

        Dim fr As frmWareOut
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareOut Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue = "�d��"
        tempValue2 = GridView2.GetFocusedRowCellValue("WO_ID").ToString
        fr = New frmWareOut
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popWareOutPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popWareOutPrint.Click
        '   On Error Resume Next

        Dim ds As New DataSet
        If GridView2.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim ltc5 As New CollectionToDataSet


        Dim pmc As New WareOutController
        Dim uc As New UnitController
        Dim suc As New SystemUser.SystemUserController
        Dim wh As New WareHouseController

        Dim pmc2 As New WhiteUserListController
        Dim uc2 As New DepartmentControler

        '    Dim omc As New OrdersMainController
        ds.Tables.Clear()
        Dim strA, strB As String

        Dim wc As New WareHouseController
        Dim wiL As New List(Of WareHouseInfo)
        wiL = wc.WareHouse_Get(TreeView1.SelectedNode.Tag)

        If wiL(0).NeedCheck = False Then

            strA = GridView2.GetFocusedRowCellValue("WO_ID").ToString

            ltc.CollToDataSet(ds, "WareOut", pmc.WareOut_Getlist5(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
            ltc2.CollToDataSet(ds, "SystemUser", suc.SystemUser_GetList(Nothing, Nothing, Nothing))
            ltc3.CollToDataSet(ds, "WareHouse", wh.WareHouse_GetList(Nothing))
            ltc5.CollToDataSet(ds, "Department", uc2.Department_GetList(Nothing, Nothing, Nothing))

            Dim poi As List(Of WareOutInfo)

            poi = pmc.WareOut_Getlist5(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If Mid(poi(0).WH_ID, 1, 3) = "W05" Then
                PreviewRPT(ds, "rptWareOutNoCard1", "�X�w��", True, True)
            Else
                PreviewRPT(ds, "rptWareOutNoCard", "�X�w��", True, True)
            End If


            ltc = Nothing
            ltc2 = Nothing
            ltc3 = Nothing
            ltc5 = Nothing
        Else

            strA = GridView2.GetFocusedRowCellValue("WO_ID").ToString
            strB = GridView2.GetFocusedRowCellValue("WO_PerID").ToString

            ltc.CollToDataSet(ds, "WareOut", pmc.WareOut_Getlist5(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
            ltc1.CollToDataSet(ds, "Unit", uc.GetUnitList(Nothing))
            ltc2.CollToDataSet(ds, "SystemUser", suc.SystemUser_GetList(Nothing, Nothing, Nothing))
            ltc3.CollToDataSet(ds, "WareHouse", wh.WareHouse_GetList(Nothing))

            ltc4.CollToDataSet(ds, "WhiteUserList", pmc2.WhiteUserList_GetList(strB, Nothing))
            ltc5.CollToDataSet(ds, "Department", uc2.Department_GetList(Nothing, Nothing, Nothing))

            Dim poi As List(Of WareOutInfo)

            poi = pmc.WareOut_Getlist5(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            'If poi(0).WH_ID = "W0401" Then
            If GetKnifeWareHouseReport(poi(0).WH_ID) = True Then '�M��ܳ���
                If poi(0).WO_Check = False Then
                    PreviewRPT(ds, "rptWareOut1", "�X�w��", False, False)
                Else
                    PreviewRPT(ds, "rptWareOut1", "�X�w��", True, False)
                End If

            Else
                If poi(0).WO_Check = False Then
                    PreviewRPT(ds, "rptWareOut", "�X�w��", False, False)
                Else
                    PreviewRPT(ds, "rptWareOut", "�X�w��", True, False)
                End If

            End If
            ltc = Nothing
            ltc1 = Nothing
            ltc2 = Nothing
            ltc3 = Nothing
            ltc4 = Nothing
            ltc5 = Nothing
        End If



    End Sub

    Private Sub ExportlStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportlStripMenuItem.Click
        'On Error Resume Next
        'If GridView2.RowCount = 0 Then Exit Sub

        'If MsgBox("�A�T�w�n��e�Ҧ�����ƶ�?", MsgBoxStyle.YesNo, "����") = MsgBoxResult.No Then Exit Sub

        'Dim exapp As New Excel.Application
        'Dim exbook As Excel.Workbook
        'Dim exsheet As Excel.Worksheet

        'Dim i As Integer = 0, ii As Integer = 0

        'exapp = CreateObject("Excel.Application")
        'exbook = exapp.Workbooks.Add
        'exsheet = exapp.Worksheets(1)

        'exsheet.Cells(1, 1) = "�X�w�渹"
        'exsheet.Cells(1, 2) = "���ƽs�X"
        'exsheet.Cells(1, 3) = "���ƦW��"
        'exsheet.Cells(1, 4) = "�W��"
        'exsheet.Cells(1, 5) = "�X�w�ƶq"
        'exsheet.Cells(1, 6) = "���"
        'exsheet.Cells(1, 7) = "�妸"
        'exsheet.Cells(1, 8) = "�Ƶ�"
        'exsheet.Cells(1, 9) = "��ƤH"
        'exsheet.Cells(1, 10) = "��Ƴ���"
        'exsheet.Cells(1, 11) = "�ާ@��"
        'exsheet.Cells(1, 12) = "�f��"
        'exsheet.Cells(1, 13) = "�X�w���"
        'exsheet.Cells(1, 14) = "�f�ֳƵ�"
        'exsheet.Cells(1, 15) = "��ƤH�u��"
        'exsheet.Cells(1, 16) = "�ӻ�渹"

        'For i = 0 To GridView2.RowCount - 1
        '    ii = i + 2


        '    exsheet.Cells(ii, 1) = GridView2.GetRowCellValue(i, "WO_ID")
        '    exsheet.Cells(ii, 2) = "$" & GridView2.GetRowCellValue(i, "M_Code") & "$"
        '    exsheet.Cells(ii, 3) = GridView2.GetRowCellValue(i, "M_Name")
        '    exsheet.Cells(ii, 4) = GridView2.GetRowCellValue(i, "M_Gauge")
        '    exsheet.Cells(ii, 5) = GridView2.GetRowCellValue(i, "WO_Qty")
        '    exsheet.Cells(ii, 6) = GridView2.GetRowCellValue(i, "M_Unit")
        '    exsheet.Cells(ii, 7) = GridView2.GetRowCellValue(i, "OS_BatchID")
        '    exsheet.Cells(ii, 8) = GridView2.GetRowCellValue(i, "WO_Remark")
        '    exsheet.Cells(ii, 9) = GridView2.GetRowCellValue(i, "WO_PerName")
        '    exsheet.Cells(ii, 10) = GridView2.GetRowCellValue(i, "DPT_Name")
        '    exsheet.Cells(ii, 11) = GridView2.GetRowCellValue(i, "WO_ActionName")
        '    exsheet.Cells(ii, 12) = GridView2.GetRowCellValue(i, "WO_Check")
        '    exsheet.Cells(ii, 13) = CDate(GridView2.GetRowCellValue(i, "WO_AddDate"))
        '    exsheet.Cells(ii, 14) = GridView2.GetRowCellValue(i, "WO_CheckRemark")
        '    exsheet.Cells(ii, 15) = GridView2.GetRowCellValue(i, "WO_PerID")
        '    exsheet.Cells(ii, 16) = GridView2.GetRowCellValue(i, "AP_NO")

        'Next
        'exapp.Visible = True



        'MsgBox("�ɥX���\!")
        If GridView2.RowCount = 0 Then Exit Sub
        'ExportGridToXls()

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

#Region "�ɥX�ƾڦ�EXCEL"
    Public Sub ExportGridToXls()

        '�ɥX��ƨ�xls

        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")

        If fileName <> "" Then

            ExportTo(New DevExpress.XtraExport.ExportXlsProvider(fileName))

            OpenFile(fileName)

        End If

    End Sub
    Private Sub ExportTo(ByVal provider As DevExpress.XtraExport.IExportProvider)

        Dim currentCursor As Cursor = Windows.Forms.Cursor.Current



        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Dim link As DevExpress.XtraGrid.Export.BaseExportLink = GridView2.CreateExportLink(provider)

        CType(link, DevExpress.XtraGrid.Export.GridViewExportLink).ExpandAll = False

        AddHandler link.Progress, New DevExpress.XtraGrid.Export.ProgressEventHandler(AddressOf Export_Progress)

        link.ExportTo(True)

        provider.Dispose()

        RemoveHandler link.Progress, New DevExpress.XtraGrid.Export.ProgressEventHandler(AddressOf Export_Progress)

        Windows.Forms.Cursor.Current = currentCursor

        'ToolStripProgressBar1

    End Sub
    Private Sub Export_Progress(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Export.ProgressEventArgs)

        If e.Phase = DevExpress.XtraGrid.Export.ExportPhase.Link Then

            'ToolStripProgressBar1.Value = e.Position

            'Me.Update()

        End If

    End Sub
    Private Sub OpenFile(ByVal fileName As String)

        If MessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Try

                Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()

                process.StartInfo.FileName = fileName

                process.StartInfo.Verb = "Open"

                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal

                process.Start()

            Catch



                MessageBox.Show(Me, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

        End If



    End Sub
    Private Function ShowSaveFileDialog(ByVal title As String, ByVal filter As String) As String

        Dim dlg As SaveFileDialog = New SaveFileDialog()

        Dim name As String = Application.ProductName

        Dim n As Integer = name.LastIndexOf(".") + 1

        If n > 0 Then



            name = name.Substring(n, name.Length - n)

        End If

        dlg.Title = "Export To " + title

        dlg.FileName = name

        dlg.Filter = filter

        If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then



            Return dlg.FileName

        End If

        Return ""

    End Function
#End Region

    '���[���H��
    Private Sub popwareOutLoadFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popwareOutLoadFile.Click
        Dim open, update, down, edit, del, detail As Boolean
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

        If GridView2.RowCount = 0 Then Exit Sub
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "505001")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then update = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then update = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "505002")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then down = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then down = False
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "505003")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then edit = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then edit = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "505004")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then del = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then del = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "505005")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then detail = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then detail = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "505006")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then open = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then open = False
        End If

        FileShow("5002", GridView2.GetFocusedRowCellValue("WO_ID").ToString, open, update, down, edit, del, detail)
    End Sub

    Private Sub popWareOutReCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutReCheck.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub

        Dim mc As New WareOut.WareOutController
        Dim mi As New WareOut.WareOutInfo
        mi = mc.WareOut_GetNUM(GridView2.GetFocusedRowCellValue("WO_NUM").ToString)

        If mi.WO_Check = True Then

            Dim fr As frmWareOut
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmWareOut Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            tempValue = "�_��"
            tempValue2 = GridView2.GetFocusedRowCellValue("WO_ID").ToString
            fr = New frmWareOut
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()

        Else
            MsgBox("���楼�Q�f�֡A�Х��f�ַ�e�X�w��!")
            Exit Sub

        End If
    End Sub

    Private Sub popWareOutAdd_Process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutAdd_Process.Click

        Edit = False
        If TreeView1.SelectedNode.Level = 1 Then
            Dim fr As frmWareOut
          
            tempValue = "�X�w��"
            tempValue3 = TreeView1.SelectedNode.Tag
            tempValue4 = TreeView1.SelectedNode.Text
            fr = New frmWareOut
            fr.isProcess = True
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        '2012-11-19MAO
        Dim FiledNameStr As String
        FiledNameStr = "WO_ID,M_Code,M_Name,M_Gauge,WO_Qty,M_Unit,WO_AddDate,AP_NO,WO_PerName,WO_ActionName,WO_PerID"
        GridViewCopyMulitRow(GridView2, FiledNameStr, "")
    End Sub

    Private Sub ToolStripMenuItemALL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemALL.Click
        Dim FiledNameStr As String
        FiledNameStr = "WO_ID,M_Code,M_Name,M_Gauge,WO_Qty,M_Unit,WO_AddDate,AP_NO,WO_PerName,WO_ActionName,WO_PerID"
        GridViewCopyMulitRow(GridView2, FiledNameStr, "ALL")
    End Sub

    Private Sub WareoutcollToolStripMenuItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WareoutcollToolStripMenuItem.Click
        tempValue3 = "500208"
        tempValue4 = "�X�w����`�O��"
        frmWareInventoryHaltRpt.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click

        If GridView2.RowCount <= 0 Then
            Exit Sub
        End If


        Dim i As Integer
        Dim exapp As New Excel.Application   '�w�q�@��excel��H
        Dim exbook As Excel.Workbook     '�w�q�@��excel����
        Dim exsheet As Excel.Worksheet   '�w�q�@��excel�u�@��

        exapp = CreateObject("Excel.Application")   '�ͦ��@��excel��H
        exbook = exapp.Workbooks.Open(Application.StartupPath & "\ModuleFile\module.xls")
        exsheet = exbook.Worksheets(1)

        exsheet.Cells(1, 1) = "�X�w�渹"
        exsheet.Cells(1, 2) = "���ƽs�X"
        exsheet.Cells(1, 3) = "���ƦW��"
        exsheet.Cells(1, 4) = "�W��"
        exsheet.Cells(1, 5) = "�X�w�ƶq"
        exsheet.Cells(1, 6) = "���"

        exsheet.Cells(1, 7) = "��ƤH�u��"
        exsheet.Cells(1, 8) = "��ƤH"
        exsheet.Cells(1, 9) = "��Ƽt�O"
        exsheet.Cells(1, 10) = "��Ƴ���"
        exsheet.Cells(1, 11) = "�ާ@��"
        exsheet.Cells(1, 12) = "�X�w���"
        exsheet.Cells(1, 13) = "�ӻ�渹"

        exsheet.Cells(1, 14) = "���O"
        exsheet.Cells(1, 15) = "���"
        exsheet.Cells(1, 16) = "���B(�I���)"

        Dim ii As Integer

        For i = 0 To GridView2.RowCount - 1
            ii = i + 2

            exsheet.Cells(ii, 1) = GridView2.GetRowCellValue(i, "WO_ID")
            exsheet.Cells(ii, 2) = GridView2.GetRowCellValue(i, "M_Code")
            exsheet.Cells(ii, 3) = GridView2.GetRowCellValue(i, "M_Name")

            exsheet.Cells(ii, 4) = GridView2.GetRowCellValue(i, "M_Gauge")

            exsheet.Cells(ii, 5) = GridView2.GetRowCellValue(i, "WO_Qty")
            exsheet.Cells(ii, 6) = GridView2.GetRowCellValue(i, "M_Unit")

            exsheet.Cells(ii, 7) = GridView2.GetRowCellValue(i, "WO_PerID")
            exsheet.Cells(ii, 8) = GridView2.GetRowCellValue(i, "WO_PerName")

            Dim lsStr As String
            lsStr = Trim(GridView2.GetRowCellValue(i, "DPT_Name"))
            exsheet.Cells(ii, 9) = Mid(lsStr, 1, InStr(lsStr, "-", CompareMethod.Text) - 1)

            exsheet.Cells(ii, 10) = Mid(lsStr, InStr(lsStr, "-", CompareMethod.Text) + 1, Len(lsStr) - InStr(lsStr, "-", CompareMethod.Text))

            exsheet.Cells(ii, 11) = GridView2.GetRowCellValue(i, "WO_ActionName")
            exsheet.Cells(ii, 12) = CDate(GridView2.GetRowCellValue(i, "WO_AddDate"))
            exsheet.Cells(ii, 13) = GridView2.GetRowCellValue(i, "AP_NO")

            exsheet.Cells(ii, 14) = GridView2.GetRowCellValue(i, "M_Currency")
            exsheet.Cells(ii, 15) = GridView2.GetRowCellValue(i, "M_Price")
            exsheet.Cells(ii, 16) = Format(Val(GridView2.GetRowCellValue(i, "HKDRate")) * Val(GridView2.GetRowCellValue(i, "M_Price")) * Val(GridView2.GetRowCellValue(i, "WO_Qty")), "0.0000")

        Next

        Dim tempName As String
        SaveFileDialog1.InitialDirectory = "c:\"
        SaveFileDialog1.Filter = "txt files (*.xls)|*.xls"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tempName = SaveFileDialog1.FileName
        Else
            Exit Sub
        End If

        exsheet.SaveAs(tempName)
        exapp.Quit()
        exsheet = Nothing
        exbook = Nothing
        exapp = Nothing

        MsgBox("�ɥX���\! ��" & tempName)
    End Sub

    Private Sub popWareOutAddBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutAddBarCode.Click
        Edit = False
        If TreeView1.SelectedNode.Level = 1 Then
            Dim fr As New frmWareOut

            tempValue = "�X�w��"
            tempValue3 = TreeView1.SelectedNode.Tag
            tempValue4 = TreeView1.SelectedNode.Text

            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.isBarCode = True
            fr.Show()
        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If
    End Sub

    Private Sub ModifyRemarkTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModifyRemarkTool.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub


        Dim mc As New WareOut.WareOutController
        Dim mi As New WareOut.WareOutInfo
        mi = mc.WareOut_GetNUM(GridView2.GetFocusedRowCellValue("WO_NUM").ToString)
        Dim fr As frmWareOut
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareOut Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue = "�ק�ƪ`"
        tempValue2 = GridView2.GetFocusedRowCellValue("WO_ID").ToString
        fr = New frmWareOut
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub


    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemPD.Click
        Edit = False
        If TreeView1.SelectedNode.Level = 1 Then
            Dim fr As New frmWareOut

            tempValue = "�X�w��"
            tempValue3 = TreeView1.SelectedNode.Tag
            tempValue4 = TreeView1.SelectedNode.Text

            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.isBarCode = True
            fr.strPdMachine = True
            fr.Show()
        Else
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub
        End If
    End Sub




    Private Function GetKnifeWareHouseReport(ByVal StrWHID As String) As Boolean
        Dim strWHRemark As String
        GetKnifeWareHouseReport = False

        Dim wc As New WareHouseController
        Dim wl As New List(Of WareHouseInfo)
        wl = wc.WareHouse_Get(StrWHID)
        strWHRemark = wl(0).WH_Remark  '�p�G�O�]�Z��

        If strWHRemark = "KnifeReports" Or strWHRemark = "�M��ܳ���" Then
            GetKnifeWareHouseReport = True
        End If
    End Function


End Class