Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.SampleManager.SampleTransaction
Imports LFERP.Library.SampleManager.SampleSend
Imports LFERP.Library.SampleManager.SampleOrdersMain

Public Class frmSampleTransaction
    Dim ds As New DataSet
    Dim stCon As New SampleTransactionControler
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmSamplePlan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmdRef_Click(Nothing, Nothing)
        PowerUser()
    End Sub
    '设置权限
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890801")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890802")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890803")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890804")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdCheck.Enabled = True
        End If
    End Sub
    ''' <summary>
    ''' 刷新事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        Me.gridSampleTransaction.DataSource = stCon.SampleTransactionMain_GetList(Nothing)
    End Sub


    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next
        ' If GridView3.RowCount = 0 Then Exit Sub
        Dim fr As frmSampleTransactionAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleTransactionAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleTransactionAdd
        fr.MdiParent = MDIMain
        fr.EditItem = "Add" '入庫
        fr.Lbl_Title.Text = Lbl_Title.Text + "--新增"
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub
        Dim stlist As New List(Of SampleTransactionInfo)
        Dim strTR_ID As String = GridView3.GetFocusedRowCellValue("TR_ID").ToString
        stlist = stCon.SampleTransaction_Getlist(Nothing, Nothing, Nothing, strTR_ID, Nothing, Nothing, Nothing)
        If stlist.Count > 0 Then
            If stlist(0).CheckBit Then
                MsgBox(strTR_ID + "資料已審核,無法修改", MsgBoxStyle.OkOnly, "提示")
                Exit Sub
            End If
        End If
        '-------------------------------------------------------------
        Dim fr As frmSampleTransactionAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleTransactionAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleTransactionAdd
        fr.MdiParent = MDIMain
        fr.EditItem = "Edit" '報
        fr.Lbl_Title.Text = Lbl_Title.Text + "--修改"
        fr.gluStatusType.EditValue = GridView3.GetFocusedRowCellValue("StatusType").ToString
        fr.EditValue = GridView3.GetFocusedRowCellValue("TR_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub
        Dim stlist As New List(Of SampleTransactionInfo)
        Dim strTR_ID As String = GridView3.GetFocusedRowCellValue("TR_ID").ToString
        stlist = stCon.SampleTransaction_Getlist(Nothing, Nothing, Nothing, strTR_ID, Nothing, Nothing, Nothing)
        If stlist.Count > 0 Then
            If stlist(0).CheckBit Then
                MsgBox(strTR_ID + "資料已審核", MsgBoxStyle.OkOnly, "提示")
                Exit Sub
            End If
        End If

        Dim fr As frmSampleTransactionAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleTransactionAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleTransactionAdd
        fr.MdiParent = MDIMain
        fr.Lbl_Title.Text = Lbl_Title.Text + "--審核"
        fr.gluStatusType.EditValue = GridView3.GetFocusedRowCellValue("StatusType").ToString
        fr.EditType = GridView3.GetFocusedRowCellValue("StatusType").ToString
        fr.EditItem = "Check"
        fr.EditValue = GridView3.GetFocusedRowCellValue("TR_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmdLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLook.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub
        Dim fr As frmSampleTransactionAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleTransactionAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleTransactionAdd
        fr.MdiParent = MDIMain
        fr.Lbl_Title.Text = Lbl_Title.Text + "--查看"
        fr.gluStatusType.EditValue = GridView3.GetFocusedRowCellValue("StatusType").ToString
        fr.EditType = GridView3.GetFocusedRowCellValue("StatusType").ToString
        fr.EditItem = "Look"
        fr.EditValue = GridView3.GetFocusedRowCellValue("TR_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        '刪除产品资料
        If GridView3.RowCount = 0 Then Exit Sub
        Dim stlist As New List(Of SampleTransactionInfo)
        Dim strTR_ID As String = GridView3.GetFocusedRowCellValue("TR_ID").ToString
        stlist = stCon.SampleTransaction_Getlist(Nothing, Nothing, Nothing, strTR_ID, Nothing, Nothing, Nothing)
        If stlist.Count > 0 Then
            If stlist(0).CheckBit Then
                MsgBox("审核后不能修改", MsgBoxStyle.OkOnly, "提示")
                Exit Sub
            End If
        End If
        '------------------------------------
        If MsgBox("你確定要刪除 " & strTR_ID & " 這個條碼編號嗎?", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.No Then Exit Sub
        If stCon.SampleTransaction_Delete(Nothing, GridView3.GetFocusedRowCellValue("TR_ID").ToString) = True Then
            cmdRef_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub GridView3_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView3.FocusedRowChanged
        If GridView3.RowCount = 0 Then Exit Sub
        Dim strTR_ID As String = GridView3.GetFocusedRowCellValue("TR_ID").ToString()
        Me.gridSampleTransactionSub.DataSource = stCon.SampleTransaction_Getlist(Nothing, Nothing, Nothing, strTR_ID, Nothing, Nothing, Nothing)
    End Sub
    Private Sub SampleTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridSampleTransaction.Click
        GridView3_FocusedRowChanged(Nothing, Nothing)
    End Sub
End Class