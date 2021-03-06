Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.NmetalSampleManager.NmetalSampleCustomerFeedback
Imports LFERP.Library.NmetalSampleManager.NmetalSamplePlan
Public Class frmNmetalCustomerFeedback
    Dim cc As New NmetalSampleCustomerFeedbackControler()
    ''' <summary>
    ''' 新增事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub addToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next
        Dim fr As frmNmetalCustomerFeedbackAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalCustomerFeedbackAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalCustomerFeedbackAdd
        fr.MdiParent = MDIMain
        'tempValue = "ADD"
        fr.EditItem = "ADD"
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub frmCustomerFeedback_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        Grid1.DataSource = cc.NmetalSampleCustomerFeedback_getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
    End Sub

    ''' <summary>
    '''修改事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub editToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        On Error Resume Next
        If Me.GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        If GridView1.GetFocusedRowCellValue("SC_Confirmation").ToString <> "" Then
            MsgBox("样版确认后不能修改", MsgBoxStyle.OkOnly, "")
            Exit Sub
        End If

        Dim fr As frmNmetalCustomerFeedbackAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalCustomerFeedbackAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalCustomerFeedbackAdd
        fr.MdiParent = MDIMain
        'tempValue2 = GridView1.GetFocusedRowCellValue("AutoID").ToString()
        'tempValue = "Edit"
        fr.EditItem = "Edit"
        fr.EditValue = GridView1.GetFocusedRowCellValue("AutoID").ToString()
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    'Private Sub addnewverToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewVerToolStripMenuItem.Click
    '    On Error Resume Next
    '    If Me.GridView1.RowCount <= 0 Then
    '        Exit Sub
    '    End If

    '    Dim fr As frmCustomerFeedbackAdd
    '    For Each fr In MDIMain.MdiChildren
    '        If TypeOf fr Is frmCustomerFeedbackAdd Then
    '            fr.Activate()
    '            Exit Sub
    '        End If
    '    Next
    '    fr = New frmCustomerFeedbackAdd
    '    fr.MdiParent = MDIMain
    '    tempValue2 = GridView1.GetFocusedRowCellValue("AutoID").ToString
    '    tempValue = "VER"
    '    fr.EditItem = "VER"
    '    fr.WindowState = FormWindowState.Maximized
    '    fr.Show()
    'End Sub
    ''' <summary>
    ''' 更新事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RefToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefToolStripMenuItem1.Click
        Grid1.DataSource = cc.NmetalSampleCustomerFeedback_getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
    End Sub
    ''' <summary>
    ''' 版本确认
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        On Error Resume Next
        If Me.GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        If GridView1.GetFocusedRowCellValue("SC_Confirmation").ToString <> "" Then
            If GridView1.GetFocusedRowCellValue("SC_Confirmation").ToString.Substring(0, 1) <> "C" Then
                MsgBox("样版确认后不能修改", MsgBoxStyle.OkOnly, "")
                Exit Sub
            End If
        End If

        Dim fr As frmNmetalCustomerFeedbackAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalCustomerFeedbackAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next

        fr = New frmNmetalCustomerFeedbackAdd
        fr.MdiParent = MDIMain
        ' tempValue2 = GridView1.GetFocusedRowCellValue("AutoID").ToString
        fr.sendQty = GridView1.GetFocusedRowCellValue("SP_Qty")
        'tempValue = "Comfarmation"
        fr.EditItem = "Comfarmation"
        fr.EditValue = GridView1.GetFocusedRowCellValue("AutoID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub

    ''' <summary>
    ''' 刪除事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        If GridView1.GetFocusedRowCellValue("SC_Confirmation").ToString <> "" Then
            MsgBox("样版确认后不能修改", MsgBoxStyle.OkOnly, "")
            Exit Sub
        End If

        If MsgBox("確定要刪除名称為： '" & GridView1.GetFocusedRowCellValue("SO_ID").ToString & "' 的記錄嗎?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If cc.NmetalSampleCustomerFeedback_Delete(GridView1.GetFocusedRowCellValue("AutoID").ToString) = True Then
                MsgBox("刪除当前記錄信息成功！", 60, "提示")
                Grid1.DataSource = cc.NmetalSampleCustomerFeedback_getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
            Else
                MsgBox("刪除当前选定記錄失敗，请檢查原因！", 60, "提示")
                Exit Sub
            End If
        End If
    End Sub

    ''' <summary>
    ''' 查询事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FindToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindToolStripMenuItem1.Click
        Dim fr As New frmNmetalSampleView
        fr = New frmNmetalSampleView
        fr.lbl_Title.Text = "样办查询--客戶反馈"
        fr.EditItem = "CustomerFeedback"
        fr.ShowDialog()
        If fr.CustomerFeedbackList.Count = 0 Then
            Exit Sub
        Else
            Grid1.DataSource = fr.CustomerFeedbackList
        End If
    End Sub


    ''' <summary>
    ''' 查看事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LookSCtrlfToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LSCtrlfToolStripMenuItem.Click
        On Error Resume Next
        If Me.GridView1.RowCount <= 0 Then
            Exit Sub
        End If
        Dim fr As frmNmetalCustomerFeedbackAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalCustomerFeedbackAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalCustomerFeedbackAdd
        fr.MdiParent = MDIMain
        ' tempValue2 = GridView1.GetFocusedRowCellValue("AutoID").ToString()
        'tempValue = "Look"
        fr.EditItem = "Look"
        fr.EditValue = GridView1.GetFocusedRowCellValue("AutoID").ToString()
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    ''' <summary>
    ''' 列印事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim SPC As New NmetalSamplePlanControler
        '''''''''''''''''''''''''''''''''''''''
        Dim som As New List(Of NmetalSampleCustomerFeedbackinfo)
        Dim a As Integer
        For a = 0 To GridView1.RowCount - 1
            Dim Scfinfo As New NmetalSampleCustomerFeedbackinfo
            Scfinfo.AutoID = GridView1.GetRowCellValue(a, "AutoID")
            Scfinfo.M_Code = GridView1.GetRowCellValue(a, "M_Code")
            Scfinfo.PM_M_Code = GridView1.GetRowCellValue(a, "PM_M_Code")
            Scfinfo.SO_ID = GridView1.GetRowCellValue(a, "SO_ID")
            Scfinfo.M_Name = GridView1.GetRowCellValue(a, "M_Name")
            Scfinfo.SC_Edition = GridView1.GetRowCellValue(a, "SC_Edition")
            Scfinfo.SC_Description = GridView1.GetRowCellValue(a, "SC_Description")
            Scfinfo.SO_OrderQty = GridView1.GetRowCellValue(a, "SO_OrderQty")
            Scfinfo.SP_Qty = GridView1.GetRowCellValue(a, "SP_Qty")
            Scfinfo.SC_customerid = GridView1.GetRowCellValue(a, "SC_customerid")
            som.Add(Scfinfo)
        Next
        ltc.CollToDataSet(dss, "SampleCustomerFeedback", som)
        PreviewRPT1(dss, "rptSampleCustomerFeedback", "寄送资料表", InUser, "栏位", True, True)
        ltc = Nothing
    End Sub
    '设置权限
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890601")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890602")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890603")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890604")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890605")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdPrint.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890606")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdAddFile.Enabled = True
        End If
    End Sub
    ''' <summary>
    ''' 增加附件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub AddFileToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddFile.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim open, update, down, Edit, del, detail As Boolean
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)
        If GridView1.RowCount = 0 Then Exit Sub
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890608")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then update = True
            If pmwiL.Item(0).PMWS_Value = "否" Then update = False
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890609")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then down = True
            If pmwiL.Item(0).PMWS_Value = "否" Then down = False
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890610")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Edit = True
            If pmwiL.Item(0).PMWS_Value = "否" Then Edit = False
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890607")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then del = True
            If pmwiL.Item(0).PMWS_Value = "否" Then del = False
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890611")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then detail = True
            If pmwiL.Item(0).PMWS_Value = "否" Then detail = False
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890612")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then open = True
            If pmwiL.Item(0).PMWS_Value = "否" Then open = False
        End If
        FileShow("8906", GridView1.GetFocusedRowCellValue("AutoID").ToString, open, update, down, Edit, del, detail)
    End Sub
End Class