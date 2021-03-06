Imports LFERP.Library.ProductionController
Imports LFERP.Library.NmetalSampleManager.NmetalSampInventoryWeightCheck

Public Class frmNmetalSampInventoryWeightCheckMain

    Dim pncon As New NmetalSampInventoryWeightCheckMainControler
    Dim nswc As New NmetalSampInventoryWeightCheckSubControler
    ''' <summary>
    ''' 窗体初始化加载
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNmetalSampInventoryWeightCheckMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim fc As New ProductionFieldControl
        GridControl2.DataSource = fc.ProductionFieldControl_GetList1(InUserID, Nothing, "V")
    End Sub
    ''' <summary>
    ''' 部门行变更时
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView2_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        If GridView2.SelectedRowsCount = 0 Then
            Exit Sub
        End If
        Dim DepID As String = GridView2.GetFocusedRowCellValue("ControlDep").ToString
        Grid.DataSource = pncon.NmetalSampInventoryWeightCheckMain_GetList(Nothing, Nothing, DepID, Nothing, Nothing, Nothing, Nothing)
    End Sub
    ''' <summary>
    ''' 日期格式显示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_CustomColumnDisplayText(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles GridView1.CustomColumnDisplayText
        If e.Column.Name = "colCH_Date" Or e.Column.Name = "colCheckDate" Then
            If e.Value = Nothing Then
                e.DisplayText = ""
            End If
        End If
    End Sub
    ''' <summary>
    ''' 新增
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripAdd.Click
        On Error Resume Next
        Dim fr As frmNmetalSampInventoryWeightCheck
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSampInventoryWeightCheck Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSampInventoryWeightCheck
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.ADD
        fr.EditDep = GridView2.GetFocusedRowCellValue("ControlDep").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' 修改
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripModife_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripModife.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim CH_NO As String = GridView1.GetFocusedRowCellValue("CH_NO").ToString

        If pncon.NmetalSampInventoryWeightCheckMain_GetList(Nothing, CH_NO, Nothing, Nothing, Nothing, Nothing, "True").Count > 0 Then

            MsgBox("审核后不能修改！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If

        Dim fr As frmNmetalSampInventoryWeightCheck
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSampInventoryWeightCheck Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSampInventoryWeightCheck
        fr.EditItem = EditEnumType.EDIT
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.CNumb = GridView1.GetFocusedRowCellValue("CH_NO").ToString
        fr.Show()
    End Sub
    ''' <summary>
    ''' 删除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDelete.Click
        If GridView1.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        If GridView1.RowCount = 0 Then
            Exit Sub
        End If
        Dim AutoID As String = GridView1.GetFocusedRowCellValue("AutoID").ToString
        Dim CH_NO As String = GridView1.GetFocusedRowCellValue("CH_NO").ToString
        If pncon.NmetalSampInventoryWeightCheckMain_GetList(Nothing, CH_NO, Nothing, Nothing, Nothing, Nothing, "True").Count > 0 Then
            MsgBox("审核后不能删除！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If

        If MsgBox("确定要删除单号为： '" & CH_NO & "' 的记录吗?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If pncon.NmetalSampInventoryWeightCheckMain_Delete(AutoID, CH_NO) = False Then
                MsgBox("删除当前记录失败，请检查原因！", 60, "提示")
                Exit Sub
            Else
                If nswc.NmetalSampInventoryWeightCheckSub_Delete(Nothing, CH_NO) = False Then
                    MsgBox("子表删除当前记录失败，请检查原因！", 60, "提示")
                    Exit Sub
                End If
            End If
            MsgBox("刪除成功！", 60, "提示")
        End If
    End Sub
    ''' <summary>
    ''' 审核
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripCheck.Click
        On Error Resume Next
        Dim CH_NO As String = GridView1.GetFocusedRowCellValue("CH_NO").ToString
        If pncon.NmetalSampInventoryWeightCheckMain_GetList(Nothing, CH_NO, Nothing, Nothing, Nothing, Nothing, "True").Count > 0 Then
            MsgBox("审核后不能再审核！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If
        Dim fr As frmNmetalSampInventoryWeightCheck
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSampInventoryWeightCheck Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSampInventoryWeightCheck
        fr.EditItem = EditEnumType.CHECK
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.CNumb = GridView1.GetFocusedRowCellValue("CH_NO").ToString
        fr.Show()
    End Sub
    ''' <summary>
    ''' 查看
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then
            Exit Sub
        End If
        Dim fr As frmNmetalSampInventoryWeightCheck
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSampInventoryWeightCheck Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSampInventoryWeightCheck
        fr.EditItem = EditEnumType.VIEW
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.CNumb = GridView1.GetFocusedRowCellValue("CH_NO").ToString
        fr.Show()
    End Sub

    Private Sub tsm_yulan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_yulan.Click
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim strSO_No As String
        strSO_No = GridView1.GetFocusedRowCellValue("CH_NO").ToString

        ltc.CollToDataSet(dss, "NmetalSampInventoryWeightCheckMain", pncon.NmetalSampInventoryWeightCheckMain_GetList(Nothing, strSO_No, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(dss, "NmetalSampInventoryWeightCheckSub", nswc.NmetalSampInventoryWeightCheckSub_GetList(Nothing, strSO_No, Nothing))


        PreviewRPT(dss, "rtpWeightCheck", "订单资料表", True, True)
        ltc = Nothing
        ltc2 = Nothing
    End Sub
    ''' <summary>
    ''' 盘点作业查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsm_Query_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_Query.Click
        Dim aif As frmNmetalSampInventoryWeightView
        aif = New frmNmetalSampInventoryWeightView
        aif.Text = "盘点查询"
        aif.EditItem = "NmetalSampInventoryWeightCheck"
        aif.StrD_ID = GridView2.GetFocusedRowCellValue("ControlDep").ToString           '部门编号
        aif.StrD_IDItem = GridView2.GetFocusedRowCellValue("DepName").ToString          '部门名称
        aif.ShowDialog()
        If aif.nsiwcmi.Count = 0 Then
            Grid.DataSource = Nothing
            Exit Sub
        Else
            Grid.DataSource = aif.nsiwcmi
        End If
    End Sub
End Class