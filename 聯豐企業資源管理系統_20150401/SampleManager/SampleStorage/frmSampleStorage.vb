Imports LFERP.DataSetting
Imports LFERP.SystemManager
Imports LFERP.Library.SampleManager.SampleStorage
Public Class frmSampleStorage
#Region "属性"
    Dim sscon As New SampleStorageController
    Dim ssList As New List(Of SampleStorageInfo)
    Dim sslcon As New SampleStorageLogController
    Dim strDID As String = String.Empty
#End Region

#Region "页面加载"
    Private Sub frmSampleStorage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '1.权限设定
        PowerUser()
        '2.控件数据绑定
        Dim pclist As New List(Of LFERP.Library.ProductionController.ProductionFieldControlInfo)
        Dim pminfo As New LFERP.Library.ProductionController.ProductionFieldControlInfo
        pminfo.DepName = "全部"
        pminfo.ControlDep = "All"
        Dim fc As New LFERP.Library.ProductionController.ProductionFieldControl
        pclist = fc.ProductionFieldControl_GetList(InUserID, Nothing)
        pclist.Insert(0, pminfo)
        Me.GridControl.DataSource = pclist
        '3.刷新
        tsmRefash_Click(Nothing, Nothing)
    End Sub

#End Region

#Region "权限设定"
    ' 设置权限
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891601")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                tsmAdd.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891602")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                tsmEdit.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891603")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                tsmDelete.Enabled = True
            End If
        End If
    End Sub
#End Region

#Region "菜单功能"
    Private Sub tsmAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmAdd.Click
        strDID = GridView2.GetFocusedRowCellValue("ControlDep")
        If strDID = "All" Then
            MsgBox("请选择对应的部门", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If
        On Error Resume Next
        Dim fr As frmSampleStorageAdd
        If strDID = String.Empty Then
            Exit Sub
        End If
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleStorageAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleStorageAdd
        fr.EditItem = EditEnumType.ADD '新增
        fr.DID = strDID
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub

    Private Sub tsmEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmEdit.Click
        If ViewMain.RowCount <= 0 Then
            Exit Sub
        End If
        If ViewMain.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        On Error Resume Next
        Dim fr As frmSampleStorageSub
        fr = New frmSampleStorageSub
        fr.SSID = ViewMain.GetFocusedRowCellValue("AutoID")
        fr.EditItem = EditEnumType.EDIT
        fr.ShowDialog()
    End Sub

    Private Sub tsmDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmDelete.Click
        If ViewMain.RowCount <= 0 Then
            Exit Sub
        End If
        If ViewMain.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        'If ViewMain.GetFocusedRowCellValue("CheckBit") = True Then
        '    MsgBox("审核后不能删除", MsgBoxStyle.OkOnly, "提示")
        '    Exit Sub
        'End If
        If MsgBox("确定要删除此记录吗？", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim m_AutoID As Int32 = ViewMain.GetFocusedRowCellValue("AutoID")
            Dim m_list As List(Of SampleStorageInfo)
            Dim sslInfo As New SampleStorageLogInfo
            m_list = sscon.SampleStorage_GetList(m_AutoID, Nothing, Nothing, Nothing, Nothing)
            If m_list.Count > 0 Then
                sslInfo.Code_ID = m_list(0).Code_ID
                sslInfo.CreateUserID = InUserID
                sslInfo.D_ID = m_list(0).D_ID
                sslInfo.OperateType = "Delete"
                sslInfo.SS_ShelveID = m_list(0).SS_ShelveID
                sslInfo.SO_ID = m_list(0).SO_ID
                sslInfo.SS_StorageLocation = m_list(0).SS_StorageLocation
                If sscon.SampleStorage_Delete(m_AutoID, Nothing) = True Then
                    sslcon.SampleStorageLog_Add(sslInfo)
                    MsgBox("删除成功！", 60, "提示")
                    tsmRefash_Click(Nothing, Nothing)
                Else
                    MsgBox("删除失败，请检查原因！", 60, "提示")
                    Exit Sub
                End If
            Else
                MsgBox("删除失败，此记录已删除！", 60, "提示")
            End If
        End If
    End Sub

    Private Sub tsmRefash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmRefash.Click
        strDID = GridView2.GetFocusedRowCellValue("ControlDep")
        If strDID = "All" Then
            ssList = sscon.SampleStorage_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
        Else
            ssList = sscon.SampleStorage_GetList(Nothing, strDID, Nothing, Nothing, Nothing)
        End If
        If ssList.Count <= 0 Then
            GridMain.DataSource = Nothing
        Else
            GridMain.DataSource = ssList
        End If
    End Sub

    Private Sub tsmView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmView.Click
        If ViewMain.RowCount <= 0 Then
            Exit Sub
        End If
        If ViewMain.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        On Error Resume Next
        Dim fr As frmSampleStorageSub
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleStorageSub Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleStorageSub
        fr.SSID = ViewMain.GetFocusedRowCellValue("AutoID")
        fr.EditItem = EditEnumType.VIEW
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub

    Private Sub tsmCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmCheck.Click
        If ViewMain.RowCount <= 0 Then
            Exit Sub
        End If
        If ViewMain.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        On Error Resume Next
        Dim fr As frmSampleStorageSub
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleStorageSub Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleStorageSub
        fr.SSID = ViewMain.GetFocusedRowCellValue("AutoID")
        fr.EditItem = EditEnumType.CHECK
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub

    Private Sub tsmPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmPrint.Click
        If ViewMain.RowCount = 0 Then Exit Sub
        If ViewMain.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim strAutoID As String
        strAutoID = ViewMain.GetFocusedRowCellValue("AutoID").ToString
        Dim strCode As String = ViewMain.GetFocusedRowCellValue("Code_ID")
        If strAutoID = String.Empty Then
            Exit Sub
        End If
        ssList = sscon.SampleStorage_GetList(strAutoID, Nothing, Nothing, Nothing, Nothing)
        If ssList.Count <= 0 Then
            MsgBox("没有数据，请检查！", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        Dim sslCon As New SampleStorageLogController
        Dim sslList As New List(Of SampleStorageLogInfo)
        sslList = sslCon.SampleStorageLog_GetList(Nothing, Nothing, strCode, Nothing)
        ltc.CollToDataSet(dss, "SampleStorage", ssList)
        ltc1.CollToDataSet(dss, "SampleStorageLog", sslList)
        Dim StrSend As String = InUserID
        PreviewRPT1(dss, "rptSampleStorageInfo", "货架信息表", StrSend, StrSend, True, True)
        ltc = Nothing
    End Sub

    Private Sub tsmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmExcel.Click
        If ViewMain.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "导出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(GridMain, saveFileDialog.FileName) Then
                MsgBox("已成功导出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If
    End Sub

    Private Sub tsmPrintTotal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmPrintTotal.Click
        If ViewMain.RowCount = 0 Then Exit Sub
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        strDID = GridView2.GetFocusedRowCellValue("ControlDep")
        If strDID = "All" Then
            ssList = sscon.SampleStorage_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
        Else
            ssList = sscon.SampleStorage_GetList(Nothing, strDID, Nothing, Nothing, Nothing)
        End If
        If ssList.Count <= 0 Then
            MsgBox("没有数据，请检查！", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        ltc.CollToDataSet(dss, "SampleStorage", ssList)
        Dim StrSend As String = InUserID
        PreviewRPT1(dss, "rptSampleStorageTotal", "货架信息总表", StrSend, StrSend, True, True)
        ltc = Nothing
    End Sub
#End Region

#Region "关联事件"
    Private Sub GridView2_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        tsmRefash_Click(Nothing, Nothing)
        ViewMain_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub ViewMain_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ViewMain.FocusedRowChanged
        Dim sslCon As New SampleStorageLogController
        Dim strCode As String = ViewMain.GetFocusedRowCellValue("Code_ID")
        If strCode = String.Empty Then
            GridChild.DataSource = Nothing
        Else
            GridChild.DataSource = sslCon.SampleStorageLog_GetList(Nothing, Nothing, strCode, Nothing)
        End If
    End Sub
#End Region

    'Private Sub tsmAddCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If ViewMain.RowCount <= 0 Then
    '        Exit Sub
    '    End If
    '    If ViewMain.FocusedRowHandle < 0 Then
    '        Exit Sub
    '    End If
    '    On Error Resume Next
    '    Dim fr As frmSampleStorageCodeAdd
    '    fr = New frmSampleStorageCodeAdd
    '    fr.SSID = ViewMain.GetFocusedRowCellValue("AutoID")
    '    fr.EditItem = EditEnumType.EDIT
    '    fr.ShowDialog()
    'End Sub
End Class