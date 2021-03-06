Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.SampleManager.SampleDivert
Imports LFERP.Library.SampleManager.SampleSetting

Public Class frmSampleDivert
#Region "属性"
    Dim sdcon As New SampleDivertControl
#End Region

#Region "载入事件"
    Private Sub SampleDivert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        cmdRef_Click(Nothing, Nothing)
    End Sub
#End Region

#Region "新增事件"
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next
        Dim fr As frmSampleDivertAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleDivertAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleDivertAdd
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.ADD
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "修改事件"
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim strSD_ID As String = GridView1.GetFocusedRowCellValue("SD_ID").ToString
        If strSD_ID = String.Empty Or strSD_ID = Nothing Then
            Exit Sub
        End If

        Dim sblist As New List(Of SampleDivertInfo)
        sblist = sdcon.SampleDivert_Getlist(Nothing, strSD_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If sblist.Count > 0 Then
            If sblist(0).SD_Check = True Then
                MsgBox(strSD_ID + ":此单已经审核,不能修改！", MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
        End If

        Dim fr As frmSampleDivertAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleDivertAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleDivertAdd
        fr.GetSD_ID = GridView1.GetFocusedRowCellValue("SD_ID").ToString
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.EDIT
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "查看事件"
    Private Sub cmdLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmSampleDivertAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleDivertAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleDivertAdd
        fr.GetSD_ID = GridView1.GetFocusedRowCellValue("SD_ID").ToString
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.VIEW
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "审核事件"
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim strSD_ID As String = GridView1.GetFocusedRowCellValue("SD_ID").ToString
        If strSD_ID = String.Empty Or strSD_ID = Nothing Then
            Exit Sub
        End If
        Dim sblist As New List(Of SampleDivertInfo)
        sblist = sdcon.SampleDivert_Getlist(Nothing, strSD_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If sblist.Count > 0 Then
            If sblist(0).SD_Check = True Then
                MsgBox(strSD_ID + ":此单已经审核,不能审核！", MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
        End If


        Dim fr As frmSampleDivertAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleDivertAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleDivertAdd
        fr.GetSD_ID = GridView1.GetFocusedRowCellValue("SD_ID").ToString
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.CHECK
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "删除事件"
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim strSD_ID As String = GridView1.GetFocusedRowCellValue("SD_ID").ToString
        If strSD_ID = String.Empty Or strSD_ID = Nothing Then
            Exit Sub
        End If
        Dim sblist As New List(Of SampleDivertInfo)
        sblist = sdcon.SampleDivert_Getlist(Nothing, strSD_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If sblist.Count > 0 Then
            If sblist(0).SD_Check = True Then
                MsgBox(strSD_ID + ":此单已经审核,不能删除！", MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
        End If

        '2.是否正式删除
        If MsgBox("确认删除此:" + strSD_ID + "单号", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.Yes Then
            If sdcon.SampleDivert_Delete(Nothing, strSD_ID) = False Then
                MsgBox("刪除失敗", MsgBoxStyle.Information, "提示")
            Else
                MsgBox("刪除成功", MsgBoxStyle.Information, "提示")
                cmdRef_Click(Nothing, Nothing)
            End If
        End If
    End Sub
#End Region

#Region "设置权限"
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891401")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891402")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891403")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891404")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdCheck.Enabled = True
        End If
    End Sub
#End Region

#Region "转Excel"
    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "導出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(Grid1, saveFileDialog.FileName) Then
                MsgBox("已成功導出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If
    End Sub
#End Region

#Region "刷新"
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        Dim msi As New List(Of SampleSettingInfo)
        Dim msc As New SampleSettingController

        Dim StrCheck As String = Nothing
        Dim StrUser As String = Nothing

        msi = msc.SampleSetting_GetList(InUserID)
        If msi.Count > 0 Then
            '1.審核類型
            Select Case msi(0).SampleDivertCheck
                Case "0,1"
                    StrCheck = Nothing
                Case "1"
                    StrCheck = "True"
                Case "0"
                    StrCheck = "False"
            End Select

            '1.用戶選擇
            If msi(0).SampleDivertCreateUserID = "All" Then
                StrUser = Nothing
            Else
                StrUser = msi(0).SampleDivertCreateUserID
            End If
            Grid1.DataSource = sdcon.SampleDivert_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, StrCheck, msi(0).SampleDivertBeginDate, Nothing, StrUser)
        Else
            Grid1.DataSource = sdcon.SampleDivert_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If

    End Sub
#End Region

End Class