Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.SampleManager.SampleProcess
Imports LFERP.Library.ProductionField
Imports LFERP.Library.SampleManager.SamplePace
Imports LFERP.Library.SampleManager.SampleProcessMain
Imports LFERP.Library.SampleManager.SampleSetting

Public Class frmSampleProcess
#Region "属性"
    Dim ds As New DataSet
#End Region

#Region "新增"
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next
        'Edit = False
        Dim fr As frmSampleProcessAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleProcessAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        'tempValue = "产品资料工艺流程单"
        tempValue3 = "生產加工"
        fr = New frmSampleProcessAdd

        fr.EditItem = EditEnumType.ADD '新增
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub
#End Region

#Region "修改"
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pc As New SampleProcessControl
        Dim piL As List(Of SampleProcessInfo)
        piL = pc.SampleProcessMain_GetList1(GridView1.GetFocusedRowCellValue("Pro_NO"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If piL(0).Pro_Check Then
            MsgBox("此工艺流程单已审核或復核，不允許修改！", 0, "提示")
            Exit Sub
        End If

        'Edit = True
        Dim fr As frmSampleProcessAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleProcessAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        'tempValue = "产品资料工艺流程单"
        tempValue2 = GridView1.GetFocusedRowCellValue("Pro_NO").ToString
        fr = New frmSampleProcessAdd

        fr.EditItem = EditEnumType.EDIT '修改
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub
#End Region

#Region "删除"
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pc As New SampleProcessControl
        Dim piL As New List(Of SampleProcessInfo)
        piL = pc.SampleProcessMain_GetList1(GridView1.GetFocusedRowCellValue("Pro_NO"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If piL(0).Pro_Check Then
            MsgBox("此工艺流程单已审核或復核，不允許刪除！", 0, "提示")
            Exit Sub
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''
        Dim SPE As New SamplePaceControler
        Dim som As New List(Of SamplePaceInfo)
        Dim StrPM_M_Code, StrM_Code As String
        StrPM_M_Code = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString
        StrM_Code = GridView1.GetFocusedRowCellValue("M_Code").ToString
        som = SPE.SamplePace_Getlist(Nothing, Nothing, Nothing, StrM_Code, StrPM_M_Code, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If som.Count > 0 Then
            MsgBox("存在样办进度无法刪除", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''
        If MsgBox("確定要刪除单号為:" & GridView1.GetFocusedRowCellValue("Pro_NO"), MsgBoxStyle.YesNo, "提示") = MsgBoxResult.No Then Exit Sub

        If pc.SampleProcessMain_Delete(GridView1.GetFocusedRowCellValue("Pro_NO")) Then '刪除主表
            If pc.SampleProcessSub_Delete(GridView1.GetFocusedRowCellValue("Pro_NO"), Nothing) Then ' 刪除子表
                MsgBox("已刪除成功！", , "提示")
            End If
        End If
        cmdRef_Click(Nothing, Nothing)
        SampleProcessSub.DataSource = pc.SampleProcessSub_GetList(GridView1.GetFocusedRowCellValue("Pro_NO"), Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub
#End Region

#Region "查看"
    Private Sub cmdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As frmSampleProcessAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleProcessAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        'tempValue = "View"
        tempValue2 = GridView1.GetFocusedRowCellValue("Pro_NO").ToString
        fr = New frmSampleProcessAdd
        fr.EditItem = EditEnumType.VIEW  '查看
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub
#End Region

#Region "审核"
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As frmSampleProcessAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleProcessAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        'tempValue = "Check"
        tempValue2 = GridView1.GetFocusedRowCellValue("Pro_NO").ToString
        fr = New frmSampleProcessAdd
        fr.EditItem = EditEnumType.CHECK  '审核
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub
#End Region

#Region "复制"
    Private Sub cmdCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopy.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As frmSampleProcessAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleProcessAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        'tempValue = "Copy"
        tempValue2 = GridView1.GetFocusedRowCellValue("Pro_NO").ToString
        fr = New frmSampleProcessAdd
        fr.EditItem = EditEnumType.COPY  '复制
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub
#End Region

#Region "窗体载入"
    Private Sub frmSampleProcess_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        cmdRef_Click(Nothing, Nothing)
    End Sub
#End Region

    'Private Function LoadSubData(ByVal PsL As List(Of SampleProcessInfo)) As Boolean
    '    If PsL Is Nothing Then Exit Function
    '    Dim Row As DataRow

    '    For i As Integer = 0 To PsL.Count - 1
    '        Row = ds.Tables("ProcessSub").NewRow
    '        Row("PS_NO") = PsL(i).PS_NO
    '        Row("PS_Num") = PsL(i).PS_Num
    '        Row("D_Name") = PsL(i).D_ID
    '        Row("PS_Type") = PsL(i).PS_Type
    '        Row("PS_Name") = PsL(i).PS_Name
    '        Row("PS_Remark") = PsL(i).PS_Remark
    '        Row("Pro_NO") = PsL(i).Pro_NO
    '        Row("PS_Check") = PsL(i).PS_Check

    '        Row("PS_Enable") = PsL(i).PS_Enable
    '        Row("PS_OutPut") = PsL(i).PS_OutPut
    '        Row("PS_Weight") = PsL(i).PS_Weight
    '        ds.Tables("ProcessSub").Rows.Add(Row)
    '    Next
    'End Function

#Region "刷新"
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        ''相關參數設置
        Dim pc As New SampleProcessControl
        Dim msi As New List(Of SampleSettingInfo)
        Dim msc As New SampleSettingController
        Dim StrCheck As String = Nothing
        Dim StrUser As String = Nothing

        msi = msc.SampleSetting_GetList(InUserID)
        If msi.Count > 0 Then
            '1.審核類型
            Select Case msi(0).SampleProcessCheck
                Case "0,1"
                    StrCheck = Nothing
                Case "1"
                    StrCheck = "True"
                Case "0"
                    StrCheck = "False"
            End Select
            '1.用戶選擇
            If msi(0).SampleOrdersCreateUserID = "All" Then
                StrUser = Nothing
            Else
                StrUser = msi(0).SampleOrdersCreateUserID
            End If

            GridSampleProcess.DataSource = pc.SampleProcessMain_GetList1(Nothing, Nothing, "生產加工", Nothing, msi(0).SampleProcessBeginDate, Nothing, StrUser, StrCheck)
        Else
            GridSampleProcess.DataSource = pc.SampleProcessMain_GetList1(Nothing, Nothing, "生產加工", Nothing, Nothing, Nothing, Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "表格事件"
    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        ProcessDetail()
    End Sub
    Sub ProcessDetail()
        Dim pc As New SampleProcessMainControler
        Dim StrPro_No As String = GridView1.GetFocusedRowCellValue("Pro_NO")

        If StrPro_No <> String.Empty Then
            SampleProcessSub.DataSource = pc.SampleProcessSub_GetItem(StrPro_No, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "查询"
    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        Dim fr As New frmSampleView
        fr = New frmSampleView
        fr.lbl_Title.Text = "样办查询--工艺"
        fr.EditItem = "SampleProcess"
        fr.ShowDialog()
        If fr.SampleProcessList.Count = 0 Then
            GridSampleProcess.DataSource = Nothing
            Exit Sub
        Else
            GridSampleProcess.DataSource = fr.SampleProcessList
        End If
    End Sub
#End Region

#Region "列印"
    '自定义列印
    Private Sub cmdPrintCustom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrintCustom.Click
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim ltm As New CollectionToDataSet
        Dim pc As New SampleProcessMainControler
        Dim SP As New SampleProcessControl
        Dim strSO_No As String = GridView1.GetFocusedRowCellValue("Pro_NO").ToString

        ltc.CollToDataSet(dss, "SampleProcessMain", SP.SampleProcessMain_GetList1(strSO_No, Nothing, "生產加工", Nothing, Nothing, Nothing, Nothing, Nothing))
        ltm.CollToDataSet(dss, "SampleProcessSub", pc.SampleProcessSub_GetReport(strSO_No, Nothing, Nothing, Nothing, Nothing, Nothing))
        PreviewRPT(dss, "rptSampleProcess", "工艺资料表", True, True)
        ltc = Nothing
        ltm = Nothing
    End Sub
    '全部列印
    Private Sub cmdPrintAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrintAll.Click
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim ltm As New CollectionToDataSet
        Dim pc As New SampleProcessMainControler
        Dim SP As New SampleProcessControl
        Dim strSO_No As String = GridView1.GetFocusedRowCellValue("Pro_NO").ToString

        ltc.CollToDataSet(dss, "SampleProcessMain", SP.SampleProcessMain_GetList1(strSO_No, Nothing, "生產加工", Nothing, Nothing, Nothing, Nothing, Nothing))
        ltm.CollToDataSet(dss, "SampleProcessSub", pc.SampleProcessSub_GetItem(strSO_No, Nothing, Nothing, Nothing, Nothing, Nothing))

        PreviewRPT(dss, "rptSampleProcess", "工艺资料表", True, True)

        ltc = Nothing
        ltm = Nothing
    End Sub
    '启用列印
    Private Sub cmdPrintUsed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrintUsed.Click
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim ltm As New CollectionToDataSet
        Dim pc As New SampleProcessMainControler
        Dim SP As New SampleProcessControl
        Dim strSO_No As String = GridView1.GetFocusedRowCellValue("Pro_NO").ToString

        ltc.CollToDataSet(dss, "SampleProcessMain", SP.SampleProcessMain_GetList1(strSO_No, Nothing, "生產加工", Nothing, Nothing, Nothing, Nothing, Nothing))
        ltm.CollToDataSet(dss, "SampleProcessSub", pc.SampleProcessSub_GetItem(strSO_No, Nothing, Nothing, Nothing, Nothing, True))

        PreviewRPT(dss, "rptSampleProcess", "工艺资料表", True, True)

        ltc = Nothing
        ltm = Nothing
    End Sub
#End Region

#Region "权限设定"
    '设置权限
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890201")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890202")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890203")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890204")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890205")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdPrintCustom.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890212")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdAddFile.Enabled = True
        End If

        '2014-02-15
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890214")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.ModifePS_BarCodeBit.Enabled = True
        End If

    End Sub
    Private Sub cmdAddFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddFile.Click
        '調用此产品资料的文件
        If GridView1.RowCount = 0 Then Exit Sub
        Dim open, update, down, Edit, del, detail As Boolean

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

        If GridView1.RowCount = 0 Then Exit Sub
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890207")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then update = True
            If pmwiL.Item(0).PMWS_Value = "否" Then update = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890208")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then down = True
            If pmwiL.Item(0).PMWS_Value = "否" Then down = False
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890209")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Edit = True
            If pmwiL.Item(0).PMWS_Value = "否" Then Edit = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890206")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then del = True
            If pmwiL.Item(0).PMWS_Value = "否" Then del = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890210")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then detail = True
            If pmwiL.Item(0).PMWS_Value = "否" Then detail = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890211")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then open = True
            If pmwiL.Item(0).PMWS_Value = "否" Then open = False
        End If
        FileShow("8902", GridView2.GetFocusedRowCellValue("PS_NO").ToString, open, update, down, Edit, del, detail)
    End Sub

    Private Sub cmdFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFile.Click
        '調用此产品资料的文件
        If GridView1.RowCount = 0 Then Exit Sub
        Dim open, update, down, Edit, del, detail As Boolean

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)
        If GridView1.RowCount = 0 Then Exit Sub
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890207")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then update = True
            If pmwiL.Item(0).PMWS_Value = "否" Then update = False
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890208")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then down = True
            If pmwiL.Item(0).PMWS_Value = "否" Then down = False
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890209")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Edit = True
            If pmwiL.Item(0).PMWS_Value = "否" Then Edit = False
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890206")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then del = True
            If pmwiL.Item(0).PMWS_Value = "否" Then del = False
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890210")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then detail = True
            If pmwiL.Item(0).PMWS_Value = "否" Then detail = False
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890211")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then open = True
            If pmwiL.Item(0).PMWS_Value = "否" Then open = False
        End If
        FileShow("8902", GridView2.GetFocusedRowCellValue("Pro_NO").ToString, open, update, down, Edit, del, detail)
    End Sub
#End Region

#Region "表数据转Excel"
    Private Sub cmdExcelB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelB.Click
        If GridView2.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "導出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(SampleProcessSub, saveFileDialog.FileName) Then
                MsgBox("已成功導出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If
    End Sub

    Private Sub cmdExcelA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelA.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "導出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(GridSampleProcess, saveFileDialog.FileName) Then
                MsgBox("已成功導出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If
    End Sub
#End Region

    Private Sub ModifePS_BarCodeBit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModifePS_BarCodeBit.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        ''Dim pc As New SampleProcessControl
        ''Dim piL As List(Of SampleProcessInfo)
        ''piL = pc.SampleProcessMain_GetList1(GridView1.GetFocusedRowCellValue("Pro_NO"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        ''If piL(0).Pro_Check Then
        ''    MsgBox("此工艺流程单已审核或復核，不允許修改自動收發！", 0, "提示")
        ''    Exit Sub
        ''End If

        Dim fr As frmSampleProcessAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleProcessAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        'tempValue = "产品资料工艺流程单"
        tempValue2 = GridView1.GetFocusedRowCellValue("Pro_NO").ToString
        fr = New frmSampleProcessAdd

        fr.EditItem = EditEnumType.ELSEONE  '修改
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub



    Private Sub colChkPS_ShowReportBit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles colChkPS_ShowReportBit.CheckedChanged
        '1.确认是否有数据
        If GridView1.RowCount = 0 Then
            Exit Sub
        End If
        '2.初值设定
        Dim spcon As New SampleProcessControl
        Dim strPS_NO As String = String.Empty
        strPS_NO = GridView2.GetFocusedRowCellValue("PS_NO")
        If strPS_NO = String.Empty Then
            Exit Sub
        End If
        '3.修改显示
        If spcon.SampleProcessSub_ShowReport(strPS_NO) = False Then
            MsgBox("修改报表显示错误！", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
    End Sub
End Class