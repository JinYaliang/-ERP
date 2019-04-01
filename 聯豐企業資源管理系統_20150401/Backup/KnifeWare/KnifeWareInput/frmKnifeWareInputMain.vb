Imports LFERP.Library.WareHouse
Imports LFERP.Library.WareHouse.WareInput
Imports LFERP.SystemManager
Imports LFERP.FileManager
'Imports Microsoft.Office.Core
'Imports Microsoft.Office.Interop
Imports LFERP.Library.WareHouse.WareSelect
Imports LFERP.Library.KnifeWare
Imports LFERP.DataSetting


Public Class frmKnifeWareInputMain
    Private LableText As String

    Private m_BolReport As Boolean = False    '����P�O����
    Private Sub KnifeWareInputMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadUserPower()

        'Dim mt As New WareHouseController
        'Dim mc As New KnifeWareInputContraller
        'mt.WareHouse_LoadToTreeView(twWare, WareSelect(InUserID, "510105"))

        KnifeWareTreeView = twWare
        KnifeWareBarManager = BarManager1
        KnifeWareLoad(ImageList1, "510105")
        'Grid1.DataSource = mc.KnifeWareInput_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510101")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareInputAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510102")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareInputEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510103")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareInputDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510104")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareInputCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510106")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareInputReCheck.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510112") '�s�W�ݳB�z
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareInputAddKnife.Enabled = True
        End If
    End Sub

    Private Sub popKnifeWareInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareInputAdd.Click, popWareInputView.Click, popWareInputCheck.Click, popWareInputReCheck.Click, popwareinputflesh.Click, popWareInputAddKnife.Click, popWareInputPrint.Click, popWareInputPrintColl.Click
        On Error Resume Next
        Dim mc As New KnifeWareInputContraller
        Select Case sender.Name
            Case "popWareInputAdd" '-----------------------------�M��J�w��
                Edit = False
                If twWare.SelectedNode Is Nothing Then
                    MsgBox("�п�ܥ��T���ܮw!", 64, "����")
                    Exit Sub
                Else
                    Dim fr As frmKnifeWareInput
                    For Each fr In MDIMain.MdiChildren
                        If TypeOf fr Is frmKnifeWareInput Then
                            fr.Activate()
                            Exit Sub
                        End If
                    Next
                    fr = New frmKnifeWareInput
                    fr.EditItem = "popWareInputAdd"
                    fr.NodeTag = twWare.SelectedNode.Tag
                    fr.NodeText = twWare.SelectedNode.Text
                    fr.MdiParent = MDIMain
                    fr.WindowState = FormWindowState.Maximized
                    fr.Show()
                End If
            Case "popWareInputAddKnife" '-----------------------------�M��J�w��
                Edit = False
                If twWare.SelectedNode Is Nothing Then
                    MsgBox("�п�ܥ��T���ܮw!", 64, "����")
                    Exit Sub
                Else
                    Dim fr As frmKnifeWareInput
                    For Each fr In MDIMain.MdiChildren
                        If TypeOf fr Is frmKnifeWareInput Then
                            fr.Activate()
                            Exit Sub
                        End If
                    Next
                    fr = New frmKnifeWareInput
                    fr.EditItem = "popWareInputAddKnife"
                    fr.NodeTag = twWare.SelectedNode.Tag
                    fr.NodeText = twWare.SelectedNode.Text
                    fr.MdiParent = MDIMain
                    fr.WindowState = FormWindowState.Maximized
                    fr.Show()
                End If
            Case "popWareInputEdit"

            Case "popWareInputDel"

            Case "popWareInputView" '-----------------------------�d��
                If GridView2.RowCount = 0 Then Exit Sub
                Dim fr As frmKnifeWareInput
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmKnifeWareInput Then
                        fr.Activate()
                        Exit Sub
                    End If
                Next
                fr = New frmKnifeWareInput
                fr.MdiParent = MDIMain
                fr.EditItem = "popWareInputView"
                fr.EditID = GridView2.GetFocusedRowCellValue("WIP_ID").ToString
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            Case "popWareInputCheck" '---------------------------�����f��
                If GridView2.RowCount = 0 Then Exit Sub
                Dim mcc As New KnifeWareInputContraller
                Dim mi As New KnifeWareInputInfo
                mi = mcc.KnifeWareInput_GetNUM(GridView2.GetFocusedRowCellValue("WIP_NUM").ToString)

                If mi.WIP_ReCheck = True Then
                    MsgBox("�ӤJ�w��w�_��,�����\�����f��!", 64, "����")
                    Exit Sub
                Else
                    If mi.WIP_Check = False Then
                        Dim fr As frmKnifeWareInput
                        For Each fr In MDIMain.MdiChildren
                            If TypeOf fr Is frmKnifeWareInput Then
                                fr.Activate()
                                Exit Sub
                            End If
                        Next
                        fr = New frmKnifeWareInput
                        fr.MdiParent = MDIMain
                        fr.EditItem = "popWareInputCheck"
                        fr.EditID = GridView2.GetFocusedRowCellValue("WIP_ID").ToString
                        fr.WindowState = FormWindowState.Maximized
                        fr.Show()
                    Else
                        MsgBox("�ӳ�ثe�w�f��,�����\�����f��!", 64, "����")
                        Exit Sub
                    End If
                End If
            Case "popWareInputReCheck" '-----------------------------�_��
                If GridView2.RowCount = 0 Then Exit Sub
                Dim mcc As New KnifeWareInputContraller
                Dim mi As New KnifeWareInputInfo
                mi = mcc.KnifeWareInput_GetNUM(GridView2.GetFocusedRowCellValue("WIP_NUM").ToString)
                If mi.WIP_Check = True Then
                    Dim fr As frmKnifeWareInput
                    For Each fr In MDIMain.MdiChildren
                        If TypeOf fr Is frmKnifeWareInput Then
                            fr.Activate()
                            Exit Sub
                        End If
                    Next
                    fr = New frmKnifeWareInput
                    fr.MdiParent = MDIMain
                    fr.EditItem = "popWareInputReCheck"
                    fr.EditID = GridView2.GetFocusedRowCellValue("WIP_ID").ToString
                    fr.WindowState = FormWindowState.Maximized
                    fr.Show()
                Else
                    MsgBox("�ӤJ�w���٥��Q�f��!�Х��T�{�f��!", 64, "����")
                    Exit Sub
                End If
            Case "popWareInputPrint"
                If GridView2.RowCount <= 0 Then Exit Sub
                Dim ds As New DataSet

                Dim ltc As New CollectionToDataSet
                Dim ltc1 As New CollectionToDataSet
                Dim ltc2 As New CollectionToDataSet

                Dim pmc As New LFERP.Library.KnifeWare.KnifeWareInputContraller
                Dim suc As New SystemUser.SystemUserController
                Dim wh As New WareHouseController
                Dim uc2 As New DepartmentControler

                ds.Tables.Clear()
                Dim strA As String
                strA = GridView2.GetFocusedRowCellValue("WIP_ID").ToString

                ltc.CollToDataSet(ds, "KnifeWareInput", pmc.KnifeWareInput_Getlist(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
                ltc1.CollToDataSet(ds, "WareHouse", wh.WareHouse_GetList(Nothing))
                ltc2.CollToDataSet(ds, "Department", uc2.Department_GetList(Nothing, Nothing, Nothing))
                PreviewRPT(ds, "rptKnifeWareInput1", "�J�w��", True, True)

                ltc = Nothing
                ltc1 = Nothing
                ltc2 = Nothing

            Case "popWareInputPrintColl"

                If GridView2.RowCount <= 0 Then Exit Sub

                Dim ds As New DataSet
                Dim ltc As New CollectionToDataSet


                'Dim strSO_No As String        --Old
                'strSO_No = GridView2.GetFocusedRowCellValue("WIP_ID").ToString    --Old
                '*********************2013-11-8 ���@�s�W**********************
                Dim strMonthly As String
                Dim strDtAll As Date
                Dim lstExport As List(Of KnifeWareInputInfo)
                Dim frmKnWareInput As frmKnifeWareInputExport = New frmKnifeWareInputExport
                frmKnWareInput.ShowDialog()


                If frmKnifeWareInputExport.DTReport = "" And Not frmKnifeWareInputExport.BolReport Then Exit Sub
                strMonthly = frmKnifeWareInputExport.DTReport
                strDtAll = Convert.ToDateTime(frmKnifeWareInputExport.DTReportAll)

                If Not frmKnifeWareInputExport.BolReport Then
                    lstExport = mc.KnifeWareInputExportReport_GetList(twWare.SelectedNode.Tag, strMonthly, DateAdd(DateInterval.Day, -7, CDate(Format(strDtAll, "yyyy/MM/dd"))))
                    If (lstExport Is Nothing) Or (lstExport.Count = 0) Then
                        MessageBox.Show("�ɥX�ƾڬ���")
                        Exit Sub
                    End If
                    ltc.CollToDataSet(ds, "KnifeWareInput", lstExport)

                    If (ds.Tables.Count < 0 Or ds.Tables.Count = 0) Then
                        MessageBox.Show("�ɥX�ƾڬ���")
                        Exit Sub
                    End If
                    Grid1.DataSource = lstExport
                    'Grid1.DataSource = mc.KnifeWareInputExportReport_GetList(twWare.SelectedNode.Tag, strMonthly, DateAdd(DateInterval.Day, -9, CDate(Format(strDtAll, "yyyy/MM/dd"))))
                    'lstExport = mc.KnifeWareInputExportReport_GetList(twWare.SelectedNode.Tag, strMonthly, DateAdd(DateInterval.Day, -9, CDate(Format(strDtAll, "yyyy/MM/dd"))))
                    'ltc.CollToDataSet(ds, "KnifeWareInput", lstExport)
                    m_BolReport = True
                Else
                    lstExport = mc.KnifeWareInputExportReport_GetList(twWare.SelectedNode.Tag, strMonthly, DateAdd(DateInterval.Day, -7, CDate(Format(strDtAll, "yyyy/MM/dd"))))

                    If lstExport.Count = 0 Then
                        MessageBox.Show("�ɥX�ƾڬ���")
                        Exit Sub
                    End If

                    Grid1.DataSource = lstExport
                    'Grid1.DataSource = mc.KnifeWareInputExportReport_GetList(twWare.SelectedNode.Tag, strMonthly, DateAdd(DateInterval.Day, -9, CDate(Format(strDtAll, "yyyy/MM/dd"))))
                    m_BolReport = True
                End If





                If (frmKnifeWareInputExport.BolReport) Then
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

                    Exit Sub

                End If

                PreviewRPT(ds, "rptKnifeWareInput", "�M��J�w��", True, True)
                ltc = Nothing


                '**************************************************************
            Case "popwareinputflesh"
                Grid1.DataSource = mc.KnifeWareInput_Getlist(Nothing, Nothing, Nothing, Nothing, twWare.SelectedNode.Tag, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), ">", Nothing, Nothing)
        End Select
    End Sub

    Private Sub twWare_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles twWare.AfterSelect
        Dim mc As New KnifeWareInputContraller
        If e.Node.Level = 0 Then
            If (m_BolReport) Then
                m_BolReport = False
                Exit Sub
            End If
            Grid1.DataSource = mc.KnifeWareInput_Getlist(Nothing, Nothing, Nothing, Nothing, twWare.SelectedNode.Tag, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), ">", Nothing, Nothing)
        End If
    End Sub

    Private Sub frmKnifeWareInputMain_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        KnifeWareTreeView = twWare
        KnifeWareBarManager = BarManager1
        KnifeWareLoad(ImageList1, "510105")
    End Sub

    Private Sub popWareInputSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareInputSeek.Click
        Dim fr As New frmKnifeSelect
        Dim mc As New LFERP.Library.KnifeWare.KnifeWareInputContraller
        fr.ComboBoxEdit1.Properties.Items.Add("�J�w�渹")
        tempValue = "�M��J�w"
        Try
            tempValue4 = twWare.SelectedNode.Tag
        Catch ex As Exception
        End Try
        fr.ShowDialog()
        If RefreshT = True Then
            Dim ws As New LFERP.Library.KnifeWare.KnifeWareSelectController
            Select Case tempValue
                Case 1
                    Grid1.DataSource = mc.KnifeWareInput_Getlist(tempValue2, Nothing, Nothing, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing)
                Case 2
                    Grid1.DataSource = mc.KnifeWareInput_Getlist(Nothing, Nothing, tempValue2, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing)
                Case 3
                    Grid1.DataSource = mc.KnifeWareInput_Getlist(Nothing, Nothing, Nothing, tempValue2, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing)
                Case 4
                    Grid1.DataSource = mc.KnifeWareInput_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                Case 5
                    Grid1.DataSource = mc.KnifeWareInput_Getlist(Nothing, Nothing, Nothing, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, tempValue2)
                Case 6
                    Grid1.DataSource = ws.WareInput_Getlist("�M��J�w", tempValue2)
            End Select
            tempValue = ""
            tempValue2 = ""
            tempValue4 = ""
            RefreshT = False
        End If
    End Sub

 
    Private Sub ExportStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportStripMenuItem.Click

    End Sub
End Class
