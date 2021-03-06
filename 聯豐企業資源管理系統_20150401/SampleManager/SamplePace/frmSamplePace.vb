Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.SampleManager.SamplePace
Imports LFERP.Library.SampleManager.SampleSend
Imports LFERP.Library.SampleManager.SampleOrdersMain


Public Class frmSamplePace
#Region "屬性"
    Dim ds As New DataSet
    Dim SPC As New SamplePaceControler
#End Region

#Region "窗體載入"
    Private Sub frmSamplePlan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmdRef_Click(Nothing, Nothing)
        PowerUser()
    End Sub
#End Region

#Region "设置权限"
    '设置权限
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890401")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890402")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890403")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890404")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdCheck.Enabled = True Else cmdCheck.Enabled = False
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890405")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdPrint.Enabled = True
        End If
    End Sub
#End Region

#Region "修改事件"
    ''' <summary>
    '''修改事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub


        Dim StrSE_ID As String = GridView3.GetFocusedRowCellValue("SE_ID").ToString

        If SPC.SamplePace_Getlist1(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, StrSE_ID, "True").Count > 0 Then
            MsgBox("不能修改,此單已審核!")
            Exit Sub
        End If


        Dim fr As frmSamplePaceAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePaceAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSamplePaceAdd
        fr.MdiParent = MDIMain
        fr.lbl_Title.Text = "样办进度--修改"
        fr.EditItem = "Edit"
        'fr.SO_IDVal = GridView3.GetFocusedRowCellValue("SO_ID").ToString
        'fr.SS_EditionVal = GridView3.GetFocusedRowCellValue("SS_Edition").ToString
        fr.SE_IDA = GridView3.GetFocusedRowCellValue("SE_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region
    ''' <summary>
    ''' 新增事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next
        Dim fr As frmSamplePaceAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePaceAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSamplePaceAdd
        fr.MdiParent = MDIMain
        fr.lbl_Title.Text = "样办进度--新增"
        fr.EditItem = "Add"
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' 刪除事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        ''刪除产品资料
        'If GridView3.RowCount = 0 Then Exit Sub

        'Dim SPE As New SampleSendControler
        'Dim som As New List(Of SampleSendInfo)
        'Dim StrSO_ID As String = GridView3.GetFocusedRowCellValue("SO_ID").ToString
        'Dim strSS_Edition As String = GridView3.GetFocusedRowCellValue("SS_Edition").ToString
        'som = SPE.SampleSend_Getlist(Nothing, StrSO_ID, strSS_Edition, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False)
        'If som.Count > 0 Then
        '    MsgBox("存在样办寄送资料无法刪除", MsgBoxStyle.Information, "提示")
        '    Exit Sub
        'End If

        'If MsgBox("你確定要刪除 " & StrSO_ID & " 這個样办进度资料嗎?", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.No Then Exit Sub


        'If SPC.SamplePace_Delete(StrSO_ID, strSS_Edition) = True Then
        '    Dim SampleMain As New SampleOrdersMainControler
        '    Dim SampleMainInfo As New SampleOrdersMainInfo
        '    SampleMainInfo.SO_ID = IIf(IsDBNull(StrSO_ID), Nothing, StrSO_ID)
        '    SampleMainInfo.SO_State = "E.样办排期"
        '    SampleMain.SampleOrdersMain_UpdateState(SampleMainInfo)

        '    gridSamplePace.DataSource = SPC.SamplePace_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
        'End If

        '刪除产品资料
        If GridView3.RowCount = 0 Then Exit Sub

        Dim SPE As New SampleSendControler
        Dim som As New List(Of SampleSendInfo)
        Dim StrSO_ID As String = GridView3.GetFocusedRowCellValue("SO_ID").ToString
        Dim strSS_Edition As String = GridView3.GetFocusedRowCellValue("SS_Edition").ToString
        Dim StrSE_ID As String = GridView3.GetFocusedRowCellValue("SE_ID").ToString

        If SPC.SamplePace_Getlist1(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, StrSE_ID, "True").Count > 0 Then
            MsgBox("不能刪除,此單已審核!")
            Exit Sub
        End If

        som = SPE.SampleSend_Getlist(Nothing, StrSO_ID, strSS_Edition, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing)
        If som.Count > 0 Then
            MsgBox("存在样办寄送资料无法刪除", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        If MsgBox("你確定要刪除 " & StrSE_ID & " 此單號碼?", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.No Then Exit Sub

        If SPC.SamplePace_DeleteSE_ID(StrSE_ID) = True Then
            If SPC.SamplePace_Getlist1(Nothing, StrSO_ID, strSS_Edition, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing).Count <= 0 Then
                Dim SampleMain As New SampleOrdersMainControler
                Dim SampleMainInfo As New SampleOrdersMainInfo
                SampleMainInfo.SO_ID = IIf(IsDBNull(StrSO_ID), Nothing, StrSO_ID)
                SampleMainInfo.SO_State = "E.样办排期"
                SampleMain.SampleOrdersMain_UpdateState(SampleMainInfo)
            End If
        End If

        cmdRef_Click(Nothing, Nothing)
    End Sub
    ''' <summary>
    ''' 查找事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        Dim fr As New frmSampleView
        fr = New frmSampleView
        fr.lbl_Title.Text = "样办查询--进度"
        fr.EditItem = "SamplePace"
        fr.ShowDialog()
        If fr.SamplePaceList.Count = 0 Then
            Exit Sub
        Else
            gridSamplePace.DataSource = fr.SamplePaceList
        End If
    End Sub

    ''' <summary>
    ''' 刷新事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        Me.gridSamplePace.DataSource = SPC.SamplePace_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        'LoadDate()
    End Sub
    ''' <summary>
    ''' 查看事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLook.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub
        Dim fr As frmSamplePaceAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePaceAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSamplePaceAdd
        fr.MdiParent = MDIMain
        fr.lbl_Title.Text = "样办进度--查看"
        fr.EditItem = "Look"
        'fr.SO_IDVal = GridView3.GetFocusedRowCellValue("SO_ID").ToString
        'fr.SS_EditionVal = GridView3.GetFocusedRowCellValue("SS_Edition").ToString
        fr.SE_IDA = GridView3.GetFocusedRowCellValue("SE_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim strSO_ID As String = GridView3.GetFocusedRowCellValue("SO_ID").ToString
        Dim strSS_Edition As String = GridView3.GetFocusedRowCellValue("SS_Edition").ToString

        ltc.CollToDataSet(dss, "SamplePace", SPC.SamplePace_Getlist(Nothing, strSO_ID, strSS_Edition, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))

        PreviewRPT(dss, "rptSamplePace", "样办进度资料表", True, True)
        ltc = Nothing
        ' Me.Close()
    End Sub
    '2013-9-9
    ''' <summary>
    ''' 審核
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub
        Dim fr As frmSamplePaceAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePaceAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSamplePaceAdd
        fr.MdiParent = MDIMain
        fr.lbl_Title.Text = "样办进度--审核"
        fr.EditItem = "Check"
        'fr.SO_IDVal = GridView3.GetFocusedRowCellValue("SO_ID").ToString
        'fr.SS_EditionVal = GridView3.GetFocusedRowCellValue("SS_Edition").ToString
        fr.SE_IDA = GridView3.GetFocusedRowCellValue("SE_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub GridView3_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView3.FocusedRowChanged
        Me.Grid1.DataSource = SPC.SamplePaceBarCode_Getlist(GridView3.GetFocusedRowCellValue("SPID").ToString, Nothing, Nothing)
    End Sub

    Private Sub gridSamplePace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridSamplePace.Click
        Me.Grid1.DataSource = SPC.SamplePaceBarCode_Getlist(GridView3.GetFocusedRowCellValue("SPID").ToString, Nothing, Nothing)
    End Sub
End Class