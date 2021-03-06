Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.SampleManager.SampInventoryCheck
Imports LFERP.Library.SampleManager.SampleSend
Imports LFERP.Library.SampleManager.SampleOrdersMain


Public Class frmSampInventoryCheck
    Dim ds As New DataSet
    Dim sicon As New SampInventoryCheckControl

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

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890901")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890902")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890903")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890904")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdAnalysis.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890905")
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
        Me.gridSampInventoryCheck.DataSource = sicon.SampInventoryCheckMain_GetList(Nothing)
    End Sub


    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next
        ' If GridView3.RowCount = 0 Then Exit Sub
        Dim fr As frmSampInventoryCheckAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampInventoryCheckAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampInventoryCheckAdd
        fr.MdiParent = MDIMain
        fr.EditItem = "Add" '入庫
        fr.Lbl_Title.Text = Lbl_Title.Text + "--新增盤點單"
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If GridView3.RowCount = 0 Then Exit Sub
        Dim stlist As New List(Of SampInventoryCheckInfo)
        Dim strCheckNO As String = GridView3.GetFocusedRowCellValue("CheckNO").ToString

        stlist = sicon.SampInventoryCheckMain_GetList(strCheckNO)
        If stlist.Count > 0 Then
            If stlist(0).CCheck Then
                MsgBox("單號" + strCheckNO + "审核后,不能修改", MsgBoxStyle.OkOnly, "提示")
                Exit Sub
            End If
            If stlist(0).AnalysisCheck Then
                MsgBox("單號" + strCheckNO + "已盤點分析,不能修改", MsgBoxStyle.OkOnly, "提示")
                Exit Sub
            End If
        End If

        On Error Resume Next
        Dim fr As frmSampInventoryCheckAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampInventoryCheckAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampInventoryCheckAdd
        fr.MdiParent = MDIMain
        fr.EditItem = "Edit" '出庫
        fr.EditValue = GridView3.GetFocusedRowCellValue("CheckNO").ToString
        fr.Lbl_Title.Text = Lbl_Title.Text + "--修改盤點單"
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        '刪除产品资料
        If GridView3.RowCount = 0 Then Exit Sub
        Dim stlist As New List(Of SampInventoryCheckInfo)
        Dim strCheckNO As String = GridView3.GetFocusedRowCellValue("CheckNO").ToString

        stlist = sicon.SampInventoryCheckMain_GetList(strCheckNO)
        If stlist.Count > 0 Then
            If stlist(0).CCheck Then
                MsgBox("單號" + strCheckNO + "审核后不能刪除", MsgBoxStyle.OkOnly, "提示")
                Exit Sub
            End If
        End If

        If MsgBox("你確定要刪除 " & strCheckNO & " 這個條碼編號嗎?", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.No Then Exit Sub
        If sicon.SampInventoryCheck_Delete(Nothing, strCheckNO) = True Then
            sicon.SampInventoryCheckChaYi_Delete(strCheckNO)
            cmdRef_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub cmdAnalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnalysis.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub

        Dim stlist As New List(Of SampInventoryCheckInfo)
        Dim strCheckNO As String = GridView3.GetFocusedRowCellValue("CheckNO").ToString
        stlist = sicon.SampInventoryCheckMain_GetList(strCheckNO)
        If stlist.Count > 0 Then
            If stlist(0).CCheck Then
                MsgBox("單號" + strCheckNO + "已審核不能盤點分析", MsgBoxStyle.OkOnly, "提示")
                Exit Sub
            End If
        End If


        Dim fr As frmSampInventoryCheckAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampInventoryCheckAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampInventoryCheckAdd
        fr.MdiParent = MDIMain
        fr.EditItem = "Analysis" '報
        fr.EditValue = GridView3.GetFocusedRowCellValue("CheckNO").ToString
        fr.Lbl_Title.Text = Lbl_Title.Text + "--分析盤點單"
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

   
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        If GridView3.RowCount = 0 Then Exit Sub
        Dim stlist As New List(Of SampInventoryCheckInfo)
        Dim strCheckNO As String = GridView3.GetFocusedRowCellValue("CheckNO").ToString
        stlist = sicon.SampInventoryCheckMain_GetList(strCheckNO)
        If stlist.Count > 0 Then
            If stlist(0).AnalysisCheck = False Then
                MsgBox("單號" + strCheckNO + "沒有盤點分析", MsgBoxStyle.OkOnly, "提示")
                Exit Sub
            End If
        End If

        On Error Resume Next
        Dim fr As frmSampInventoryCheckAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampInventoryCheckAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampInventoryCheckAdd
        fr.MdiParent = MDIMain
        fr.Lbl_Title.Text = Lbl_Title.Text + "--審核"
        fr.EditItem = "Check"
        fr.EditValue = GridView3.GetFocusedRowCellValue("CheckNO").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmdLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLook.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub
        Dim fr As frmSampInventoryCheckAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampInventoryCheckAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampInventoryCheckAdd
        fr.MdiParent = MDIMain
        fr.Lbl_Title.Text = Lbl_Title.Text + "--查看"
        fr.EditItem = "Look"
        fr.EditValue = GridView3.GetFocusedRowCellValue("CheckNO").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub


    Private Sub GridView3_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView3.FocusedRowChanged
        If GridView3.RowCount = 0 Then Exit Sub
        Dim strCheckNo As String = GridView3.GetFocusedRowCellValue("CheckNO").ToString()
        Me.gridSampInventoryCheckSub.DataSource = sicon.SampInventoryCheck_GetList(Nothing, strCheckNo, Nothing, Nothing, Nothing, Nothing)
        Me.GridAnalysis.DataSource = sicon.SampInventoryCheckChaYi_GetList(strCheckNo, Nothing)
    End Sub

    Private Sub gridSampInventoryCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridSampInventoryCheck.Click
        GridView3_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub cmdPrintAnalysisA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdPrintAnalysisA.Click
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim strCheckNO As String = GridView3.GetFocusedRowCellValue("CheckNO").ToString

        ltc.CollToDataSet(dss, "SampInventoryCheckChaYi", sicon.SampInventoryCheckChaYi_GetList(strCheckNO, Nothing))

        PreviewRPT(dss, "rptSampInventoryCheckA", "盤點差異常分析", True, True)
        ltc = Nothing
    End Sub
    Private Sub cmdPrintAnalysisB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdPrintAnalysisB.Click
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim strCheckNO As String = GridView3.GetFocusedRowCellValue("CheckNO").ToString

        ltc.CollToDataSet(dss, "SampInventoryCheckChaYi", sicon.SampInventoryCheckChaYi_GetList(strCheckNO, Nothing))

        PreviewRPT(dss, "rptSampInventoryCheckB", "盤點差異常分析", True, True)
        ltc = Nothing
    End Sub
    Private Sub cmdPrintAnalysisC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdPrintAnalysisC.Click
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim strCheckNO As String = GridView3.GetFocusedRowCellValue("CheckNO").ToString

        ltc.CollToDataSet(dss, "SampInventoryCheckChaYi", sicon.SampInventoryCheckChaYi_GetList(strCheckNO, Nothing))

        PreviewRPT(dss, "rptSampInventoryCheckC", "盤點差異常分析", True, True)
        ltc = Nothing
    End Sub
End Class