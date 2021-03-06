Imports LFERP.Library.ProductionController
Imports LFERP.Library.NmetalSampleManager.NmetalSampleCollection
Imports LFERP.SystemManager

Public Class frmNmetalSampleDepWeightChecks
    Dim nsd As List(Of NmetalSampleDepWeightCheckInfo)
    Dim pncon As New NmetalSampleDepWeightCheckControler

    Private Sub frmNmetalSampleDepWeightChecks_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        Dim fc As New ProductionFieldControl
        GridControl2.DataSource = fc.ProductionFieldControl_GetList1(InUserID, Nothing, "V")
    End Sub
    ''' <summary>
    ''' 设置权限
    ''' </summary>
    ''' <remarks></remarks>
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860901")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                Me.tsm_Insert.Enabled = True
            End If
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860902")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                Me.tsm_update.Enabled = True
            End If
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860903")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                Me.tsm_del.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860904")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                Me.tsm_check.Enabled = True
            End If
        End If

    End Sub
    Private Sub GridView2_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        If GridView2.SelectedRowsCount = 0 Then
            Exit Sub
        End If
        Dim strA As String
        strA = GridView2.GetFocusedRowCellValue("ControlDep").ToString
        Grid.DataSource = pncon.NmetalSampleDepWeightCheck_GetList(Nothing, Nothing, Nothing, strA, Nothing, Nothing, Nothing, Nothing)
    End Sub
    ''' <summary>
    ''' 日期格式的转换
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_CustomColumnDisplayText(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles GridView1.CustomColumnDisplayText
        If e.Column.Name = "gclAddDate" Or e.Column.Name = "gclCheckDate" Or e.Column.Name = "gclModifyDate" Then
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
    Private Sub tsm_Insert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_Insert.Click
        On Error Resume Next
        If GridView2.RowCount <= 0 Then Exit Sub

        tempValue = GridView2.GetFocusedRowCellValue("DepName").ToString
        tempValue2 = GridView2.GetFocusedRowCellValue("ControlDep").ToString

        Dim fr As frmNmetalSampleDepWeightCheck
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSampleDepWeightCheck Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSampleDepWeightCheck
        fr.EditItem = EditEnumType.ADD
        fr.MdiParent = MDIMain
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
    Private Sub tsm_update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_update.Click
        On Error Resume Next
        If GridView1.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        If GridView1.RowCount = 0 Then Exit Sub

        Dim ChangeNO As String = GridView1.GetFocusedRowCellValue("ChangeNO").ToString

        If pncon.NmetalSampleDepWeightCheck_GetList(Nothing, ChangeNO, Nothing, Nothing, Nothing, Nothing, Nothing, "True").Count > 0 Then
            MsgBox("审核后不能修改！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If

        Dim fr As frmNmetalSampleDepWeightCheck
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSampleDepWeightCheck Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSampleDepWeightCheck
        fr.EditItem = EditEnumType.EDIT
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.AutoNo = GridView1.GetFocusedRowCellValue("ChangeNO").ToString
        fr.Show()
    End Sub
    ''' <summary>
    ''' 刪除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsm_del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_del.Click
        If GridView1.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        If GridView1.RowCount = 0 Then
            Exit Sub
        End If
        Dim strChangeNO As String = GridView1.GetFocusedRowCellValue("ChangeNO").ToString
        Dim AutoID As String = GridView1.GetFocusedRowCellValue("AutoID").ToString

        If pncon.NmetalSampleDepWeightCheck_GetList(Nothing, strChangeNO, Nothing, Nothing, Nothing, Nothing, Nothing, "True").Count > 0 Then
            MsgBox("审核后不能刪除！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If

        If MsgBox("確定要刪除單號為： '" & strChangeNO & "' 的記錄嗎?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If pncon.NmetalSampleDepWeightCheck_Delete(AutoID, Nothing) = True Then
                MsgBox("刪除当前記錄成功！", 60, "提示")
            Else
                MsgBox("刪除当前記錄失敗，请檢查原因！", 60, "提示")
                Exit Sub
            End If
        End If
    End Sub
    ''' <summary>
    ''' 審核
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsm_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_check.Click
        On Error Resume Next
        Dim ChangeNO As String = GridView1.GetFocusedRowCellValue("ChangeNO").ToString
        If pncon.NmetalSampleDepWeightCheck_GetList(Nothing, ChangeNO, Nothing, Nothing, Nothing, Nothing, Nothing, "True").Count > 0 Then
            MsgBox("审核后不能再審核！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If
        Dim fr As frmNmetalSampleDepWeightCheck
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSampleDepWeightCheck Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSampleDepWeightCheck
        fr.EditItem = EditEnumType.CHECK
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.AutoNo = GridView1.GetFocusedRowCellValue("ChangeNO").ToString
        fr.Show()
    End Sub
    ''' <summary>
    ''' 查看
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmi_view_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_view.Click
        On Error Resume Next
        If GridView1.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As frmNmetalSampleDepWeightCheck
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSampleDepWeightCheck Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSampleDepWeightCheck
        fr.EditItem = EditEnumType.VIEW
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.AutoNo = GridView1.GetFocusedRowCellValue("ChangeNO").ToString
        fr.Show()
    End Sub

    Private Sub tsm_testButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_testButton.Click
        On Error Resume Next
        Dim fr As frmNmetalSampInventoryWeightCheckMain
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSampInventoryWeightCheckMain Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSampInventoryWeightCheckMain
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    ''' <summary>
    ''' 盘点更改单查询
    ''' 2014-08-04
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmQuery.Click
        Dim aif As frmNmetalSampInventoryWeightView
        aif = New frmNmetalSampInventoryWeightView
        aif.Text = "盘点查询"
        aif.EditItem = "NmetalSampleDepWeightCheck"
        aif.StrD_ID = GridView2.GetFocusedRowCellValue("ControlDep").ToString           '部门编号
        aif.StrD_IDItem = GridView2.GetFocusedRowCellValue("DepName").ToString          '部门名称
        aif.ShowDialog()
        If aif.nsdwci.Count = 0 Then
            Grid.DataSource = Nothing
            Exit Sub
        Else
            Grid.DataSource = aif.nsdwci
        End If
    End Sub
End Class