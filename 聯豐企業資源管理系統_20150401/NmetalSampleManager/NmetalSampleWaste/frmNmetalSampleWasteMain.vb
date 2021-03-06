Imports LFERP.Library.ProductionController
Imports LFERP.Library.NmetalSampleManager.NmetalSampleReWaste


Public Class frmNmetalSampleWasteMain

    Dim pncon As New NmetalSampleReWasteControler


    Private Sub frmNmetalSampleWasteMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim fc As New ProductionFieldControl
        GridControl2.DataSource = fc.ProductionFieldControl_GetList1(InUserID, Nothing, "V")
    End Sub

    Private Sub GridView2_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        If GridView2.SelectedRowsCount = 0 Then
            Exit Sub
        End If
        Dim ReDepID As String
        ReDepID = GridView2.GetFocusedRowCellValue("ControlDep").ToString
        Grid.DataSource = pncon.NmetalSampleReWaste_GetList(Nothing, Nothing, ReDepID, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub
    ''' <summary>
    ''' 新增
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsm_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_Add.Click
        On Error Resume Next
        If GridView2.RowCount <= 0 Then
            Exit Sub
        End If
        Dim fr As New frmNmetalSampleWaste
        fr = New frmNmetalSampleWaste
        fr.EditItem = EditEnumType.ADD
        fr.EditDep = GridView2.GetFocusedRowCellValue("ControlDep").ToString
        fr.ShowDialog()
        fr.Dispose()


    End Sub
    ''' <summary>
    ''' 修改
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsm_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_Update.Click

        If GridView1.FocusedRowHandle < 0 Then
            Exit Sub
        End If

        If GridView1.RowCount = 0 Then Exit Sub
        Dim ReNO As String = GridView1.GetFocusedRowCellValue("ReNO").ToString
        If pncon.NmetalSampleReWaste_GetList(Nothing, ReNO, Nothing, Nothing, Nothing, "True", Nothing, Nothing).Count > 0 Then
            MsgBox("审核后不能修改！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If

        Dim fr As New frmNmetalSampleWaste
        fr = New frmNmetalSampleWaste
        fr.EditItem = EditEnumType.EDIT
        fr.ReNOs = GridView1.GetFocusedRowCellValue("ReNO").ToString
        fr.ShowDialog()
        fr.Dispose()
    End Sub
    ''' <summary>
    ''' 刪除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsm_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_Delete.Click
        If GridView1.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        If GridView1.RowCount = 0 Then
            Exit Sub
        End If
        Dim AutoID As String = GridView1.GetFocusedRowCellValue("AutoID").ToString
        Dim ReNO As String = GridView1.GetFocusedRowCellValue("ReNO").ToString
        If pncon.NmetalSampleReWaste_GetList(Nothing, ReNO, Nothing, Nothing, Nothing, "True", Nothing, Nothing).Count > 0 Then
            MsgBox("审核后不能修改！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If

        If MsgBox("確定要刪除單號為： '" & ReNO & "' 的記錄嗎?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If pncon.NmetalSampleReWaste_Delete(AutoID, ReNO) = True Then
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
    Private Sub tsm_Ceheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_Ceheck.Click
        On Error Resume Next
        Dim ReNO As String = GridView1.GetFocusedRowCellValue("ReNO").ToString
        If pncon.NmetalSampleReWaste_GetList(Nothing, ReNO, Nothing, Nothing, Nothing, "True", Nothing, Nothing).Count > 0 Then
            MsgBox("审核后不能修改！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If


        Dim fr As New frmNmetalSampleWaste
        fr = New frmNmetalSampleWaste
        fr.EditItem = EditEnumType.CHECK
        fr.ReNOs = GridView1.GetFocusedRowCellValue("ReNO").ToString
        fr.ShowDialog()
        fr.Dispose()
    End Sub

    Private Sub tsm_Vieew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_Vieew.Click
        On Error Resume Next
        Dim ReNO As String = GridView1.GetFocusedRowCellValue("ReNO").ToString
        Dim fr As New frmNmetalSampleWaste
        fr = New frmNmetalSampleWaste
        fr.EditItem = EditEnumType.VIEW
        fr.ReNOs = GridView1.GetFocusedRowCellValue("ReNO").ToString
        fr.ShowDialog()
        fr.Dispose()
    End Sub
    ''' <summary>
    ''' 废料查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmi_Query_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmi_Query.Click
        Dim aif As frmNmetalSampInventoryWeightView
        aif = New frmNmetalSampInventoryWeightView
        aif.Text = "废料查询"
        aif.EditItem = "NmetalSampleReWaste"
        aif.StrD_ID = GridView2.GetFocusedRowCellValue("ControlDep").ToString           '部门编号
        aif.StrD_IDItem = GridView2.GetFocusedRowCellValue("DepName").ToString          '部门名称
        aif.ShowDialog()
        If aif.nsrci.Count = 0 Then
            Grid.DataSource = Nothing
            Exit Sub
        Else
            Grid.DataSource = aif.nsrci
        End If
    End Sub
End Class