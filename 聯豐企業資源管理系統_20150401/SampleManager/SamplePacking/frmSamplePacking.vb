Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.SampleManager.SamplePacking
Imports LFERP.Library.SampleManager.SamplePace

Public Class frmSamplePacking
#Region "屬性"
    Dim spcon As New SamplePackingController
    Private saCon As New SamplePaceControler
#End Region

#Region "窗體載入"
    Private Sub frmSamplePacking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        Grid1.DataSource = spcon.SamplePacking_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub
#End Region

#Region "設置權限"


    '设置权限
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891001")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891002")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891003")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "891004")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdCheck.Enabled = True
        End If
    End Sub
#End Region

#Region "新增"
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next
        Dim fr As frmSamplePackingAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePackingAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSamplePackingAdd
        fr.MdiParent = MDIMain
        fr.Lbl_Title.Text = "裝箱管理--新增"
        fr.EditItem = "Add"

        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "修改"
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        On Error Resume Next
        Dim strPK_ID As String = GridView1.GetFocusedRowCellValue("PK_ID").ToString
        If strPK_ID = String.Empty Or strPK_ID = Nothing Then
            Exit Sub
        End If

        Dim SSI As New List(Of SamplePackingInfo)
        SSI = spcon.SamplePacking_GetList(Nothing, strPK_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If SSI(0).CheckBit = True Then
            MsgBox("已经审核无法修改")
            Exit Sub
        End If

        Dim fr As frmSamplePackingAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePackingAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSamplePackingAdd
        fr.MdiParent = MDIMain
        fr.Lbl_Title.Text = "裝箱管理--修改"
        fr.EditItem = "Edit"
        fr.EditValue = strPK_ID
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "刪除"
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        Try
            '1.審核不能刪除
            Dim strPK_ID As String = GridView1.GetFocusedRowCellValue("PK_ID").ToString
            If strPK_ID = String.Empty Or strPK_ID = Nothing Then
                Exit Sub
            End If

            Dim SSI As New List(Of SamplePackingInfo)
            SSI = spcon.SamplePacking_GetList(Nothing, strPK_ID.ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If SSI(0).CheckBit = True Then
                MsgBox("已经审核无法刪除")
                Exit Sub
            End If

            '2.收發單號已經確認不能刪除
            Dim strAutoID As String = GridView1.GetFocusedRowCellValue("SE_ID").ToString
            If strAutoID <> String.Empty Then
                Dim splist As List(Of SamplePaceInfo)
                splist = saCon.SamplePace_Getlist(strAutoID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If splist.Count > 0 Then
                    If splist(0).SE_InCheck Then
                        MessageBox.Show(splist(0).SE_ID + "此單已經確認,裝箱單號不能刪除!", "提示")
                        Exit Sub
                    End If

                    '3.修改收發單號的裝箱單號
                    Dim spkInfo As New SamplePaceInfo
                    spkInfo.AutoID = strAutoID
                    spkInfo.PK_Code_ID = String.Empty
                    spkInfo.SealCode_ID = String.Empty
                    If saCon.SamplePacePk_Update(spkInfo) = False Then
                        MsgBox("修改裝箱單號失敗，请檢查原因！")
                        Exit Sub
                    End If
                End If
            End If
            '4.正常刪除
            If MsgBox("你確定要刪除 " & strPK_ID & " 裝箱單號嗎?", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.No Then Exit Sub

            If spcon.SamplePacking_Delete(Nothing, strPK_ID) = True Then
                spcon.SamplePackingSub_Delete(Nothing, strPK_ID, Nothing)
                spcon.SamplePackingSubB_Delete(Nothing, strPK_ID, Nothing)
                Grid1.DataSource = spcon.SamplePacking_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            End If
        Catch

        End Try
    End Sub
#End Region

#Region "查看"

    Private Sub cmdLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLook.Click
        On Error Resume Next
        Dim fr As frmSamplePackingAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePackingAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSamplePackingAdd
        fr.MdiParent = MDIMain
        fr.Lbl_Title.Text = "裝箱管理--查看"
        fr.EditItem = "Look"
        fr.EditValue = GridView1.GetFocusedRowCellValue("PK_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "審核"
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        On Error Resume Next

        Dim fr As frmSamplePackingAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePackingAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSamplePackingAdd
        fr.MdiParent = MDIMain
        fr.Lbl_Title.Text = "裝箱管理--審核"
        fr.EditItem = "Check"
        fr.EditValue = GridView1.GetFocusedRowCellValue("PK_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "表格事件"
    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If GridView1.RowCount = 0 Then
            Exit Sub
        End If
        Try
            Grid2.DataSource = spcon.SamplePackingSub_GetList(Nothing, GridView1.GetFocusedRowCellValue("PK_ID").ToString, Nothing, Nothing)
        Catch
        End Try
    End Sub

    Private Sub Grid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grid1.Click
        If GridView1.RowCount = 0 Then
            Exit Sub
        End If
        Try
            Grid2.DataSource = spcon.SamplePackingSub_GetList(Nothing, GridView1.GetFocusedRowCellValue("PK_ID").ToString, Nothing, Nothing)
        Catch
        End Try
    End Sub

#End Region

#Region "刷新"
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        If GridView1.RowCount = 0 Then
            Exit Sub
        End If
        Grid1.DataSource = spcon.SamplePacking_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub
#End Region

#Region "查詢"
    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        frmSamplePackingQuery.ShowDialog()
        Select Case Mid(tempValue2, 1, 1)
            Case "A"
                Grid1.DataSource = spcon.SamplePacking_GetList(Nothing, tempValue3, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing)
            Case "B"
                Dim conlist As New List(Of SamplePackingInfo)
                conlist = spcon.SamplePackingSub_GetList(Nothing, Nothing, tempValue4, tempValue3)
                If conlist.Count > 0 Then
                    Grid1.DataSource = spcon.SamplePacking_GetList(Nothing, conlist(0).PK_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                End If
            Case "C"
                Dim conlist As New List(Of SamplePackingInfo)
                conlist = spcon.SamplePackingSubB_GetList(Nothing, Nothing, tempValue4, Nothing)
                If conlist.Count > 0 Then
                    Dim csublist As New List(Of SamplePackingInfo)
                    csublist = spcon.SamplePackingSub_GetList(Nothing, Nothing, Nothing, conlist(0).PB_ID)
                    If csublist.Count > 0 Then
                        Grid1.DataSource = spcon.SamplePacking_GetList(Nothing, csublist(0).PK_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    End If
                End If
        End Select
        frmSamplePackingQuery.Dispose()
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
    End Sub
#End Region

#Region "列印事件"
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim strPKID As String = GridView1.GetFocusedRowCellValue("PK_ID").ToString
        Dim ds As New DataSet

        Dim ltc1 As New CollectionToDataSet

        Dim ltc2 As New CollectionToDataSet

        Dim ltc3 As New CollectionToDataSet

        ltc1.CollToDataSet(ds, "SamplePacking", spcon.SamplePacking_GetList(Nothing, strPKID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(ds, "SamplePackingSub", spcon.SamplePackingSub_GetList(Nothing, strPKID, Nothing, Nothing))
        ltc3.CollToDataSet(ds, "SamplePackingSubB", spcon.SamplePackingSubB_GetList(Nothing, strPKID, Nothing, Nothing))

        PreviewRPT(ds, "rptSamPackingAll", "裝箱資料表", True, True)

        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing

        ' Me.Close()
    End Sub
#End Region

    Private Sub cmdPrintAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrintAll.Click
        frmSamplePackingPrint.ShowDialog()
        frmSamplePackingPrint.Dispose()
    End Sub

    Private Sub ExcelXToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelXToolStripMenuItem.Click
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
End Class