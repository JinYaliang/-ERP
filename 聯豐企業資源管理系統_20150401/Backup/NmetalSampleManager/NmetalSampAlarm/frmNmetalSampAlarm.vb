Imports LFERP.Library.NmetalSampleManager.NmetalSampleAlarm
Imports LFERP.SystemManager
Public Class frmNmetalSampAlarm
#Region "属性"
    Dim frmB As frmNmetalSampAlarmAdd
    Dim sacon As New NmetalSampleAlarmController
#End Region

#Region "窗体载入"
    Private Sub frmSampAlarm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        cmdRef_Click(Nothing, Nothing)
    End Sub
#End Region

#Region "刷新"
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        Grid1.DataSource = sacon.NmetalSampleAlarm_GetList(Nothing)
    End Sub
#End Region

#Region "设置权限"
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891501")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891502")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891503")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891504")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdCheck.Enabled = True
        End If

    End Sub
#End Region

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub
        Dim strSE_ID As String = GridView1.GetFocusedRowCellValue("SE_ID").ToString
        If strSE_ID = String.Empty Or strSE_ID = Nothing Then
            Exit Sub
        End If
        Dim sblist As New List(Of NmetalSampleAlarmInfo)
        sblist = sacon.NmetalSampleAlarm_GetList(strSE_ID)
        If sblist.Count > 0 Then
            If sblist(0).ProcessResult <> String.Empty Then
                MsgBox(strSE_ID + ":此单已经处理,不能新增！", MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
            If sblist(0).CheckBit = True Then
                MsgBox(strSE_ID + ":此单已经处理确认,不能再处理！", MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
        End If

        frmB = New frmNmetalSampAlarmAdd
        frmB.EditItem = EditEnumType.ADD
        frmB.GetSE_ID = strSE_ID
        frmB.Show()
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub
        Dim strSE_ID As String = GridView1.GetFocusedRowCellValue("SE_ID").ToString
        If strSE_ID = String.Empty Or strSE_ID = Nothing Then
            Exit Sub
        End If
        Dim sblist As New List(Of NmetalSampleAlarmInfo)
        sblist = sacon.NmetalSampleAlarm_GetList(strSE_ID)
        If sblist.Count > 0 Then
            If sblist(0).ProcessResult = String.Empty Then
                MsgBox(strSE_ID + ":此单还没有处理！", MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
            If sblist(0).CheckBit = True Then
                MsgBox(strSE_ID + ":此单已经处理确认,不能修改！", MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
        End If

        frmB = New frmNmetalSampAlarmAdd
        frmB.EditItem = EditEnumType.EDIT
        frmB.GetSE_ID = strSE_ID
        frmB.Show()
    End Sub

#Region "對Grid中的審核日期和運算類型設置顯示格式"
    Private Sub GridView1_CustomColumnDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles GridView1.CustomColumnDisplayText
        Try
            '----------------當訂單開始日期為空時，則不顯示----------------
            If e.Column.FieldName = "SE_InTime" Then
                If e.Value = Nothing Then
                    e.DisplayText = ""
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "GridView1_CustomColumnDisplayText方法出錯")
        End Try
    End Sub
#End Region
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim strSE_ID As String = GridView1.GetFocusedRowCellValue("SE_ID").ToString
        If strSE_ID = String.Empty Or strSE_ID = Nothing Then
            Exit Sub
        End If

        Dim sblist As New List(Of NmetalSampleAlarmInfo)
        sblist = sacon.NmetalSampleAlarm_GetList(strSE_ID)
        If sblist.Count > 0 Then
            If sblist(0).ProcessResult = String.Empty Then
                MsgBox(strSE_ID + ":此单还没有处理！", MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
            If sblist(0).CheckBit = True Then
                MsgBox(strSE_ID + ":此单已经处理确认,不能审核！", MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
        End If

        frmB = New frmNmetalSampAlarmAdd
        frmB.EditItem = EditEnumType.CHECK
        frmB.GetSE_ID = strSE_ID
        frmB.Show()
    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim strSE_ID As String = GridView1.GetFocusedRowCellValue("SE_ID").ToString
        If strSE_ID = String.Empty Or strSE_ID = Nothing Then
            Exit Sub
        End If

        Dim sblist As New List(Of NmetalSampleAlarmInfo)
        sblist = sacon.NmetalSampleAlarm_GetList(strSE_ID)
        If sblist.Count > 0 Then
            If sblist(0).ProcessResult = String.Empty Then
                MsgBox(strSE_ID + ":此单还没有处理！", MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
            If sblist(0).CheckBit = True Then
                MsgBox(strSE_ID + ":此单已经处理确认,不能删除！", MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
        End If

        If MsgBox(strSE_ID + ":确认删除此信息处理记录吗？", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.Yes Then
            If sacon.NmetalSampleAlarm_Delete(Nothing, strSE_ID) = False Then
                MsgBox("刪除失敗", MsgBoxStyle.Information, "提示")
            End If
            MsgBox("刪除成功", MsgBoxStyle.Information, "提示")
        Else
            MsgBox("刪除失敗", MsgBoxStyle.Information, "提示")
        End If
    End Sub

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

    Private Sub cmdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim strSE_ID As String = GridView1.GetFocusedRowCellValue("SE_ID").ToString
        If strSE_ID = String.Empty Or strSE_ID = Nothing Then
            Exit Sub
        End If
        frmB = New frmNmetalSampAlarmAdd
        frmB.EditItem = EditEnumType.VIEW
        frmB.GetSE_ID = strSE_ID
        frmB.Show()
    End Sub

End Class