Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.SampleManager.SampleSend
Imports LFERP.Library.SampleManager.SampleCustomerFeedback
Imports LFERP.Library.SampleManager.SampleOrdersMain
Imports LFERP.Library.SampleManager.SampleOrdersSub
Imports LFERP.Library.SampleManager.SampleOrders
Imports LFERP.Library.SampleManager.SampleTransaction
Imports LFERP.Library.SampleManager.SampleSetting

Public Class frmSampleSend
#Region "屬性"
    Dim ds As New DataSet
    Dim SampleSend As New SampleSendControler
    Dim sscon As New SampleSendCodeControler
    Dim ssfcon As New SampleSendShipFilesControl
#End Region

#Region "窗體載入"
    Private Sub frmSamplePlan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        cmdRef_Click(Nothing, Nothing)
        'createtable()
    End Sub
#End Region

#Region "设置权限"
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890501")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                Me.cmdAdd.Enabled = True
                Me.cmdShipFile.Enabled = True
                Me.cmdShipFileAdd.Enabled = True
            End If
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890502")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                Me.cmdShipFileEdit.Enabled = True
                Me.cmdEdit.Enabled = True
            End If
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890503")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                Me.cmdShipDel.Enabled = True
                Me.cmdDel.Enabled = True
            End If
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890504")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890505")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdPrint.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890506")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdFile.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890515")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdInCheck.Enabled = True
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
        '1.表格是否有值
        If GridView3.RowCount = 0 Then Exit Sub
        '2.是否审查
        Dim SampleSend As New SampleSendControler
        Dim SSI As New List(Of SampleSendInfo)
        SSI = SampleSend.SampleSend_Getlist(GridView3.GetFocusedRowCellValue("SP_ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing)
        If SSI(0).SP_Check = True Then
            MsgBox("已经审核无法修改")
            Exit Sub
        End If
        '2.是否以打开
        Dim fr As frmSampleSendAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleSendAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleSendAdd
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.EDIT
        fr.txtSP_ID.Text = GridView3.GetFocusedRowCellValue("SP_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "新增事件"
    ''' <summary>
    ''' 新增事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next
        Dim fr As frmSampleSendAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleSendAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleSendAdd
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.ADD
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "刪除事件"


    ''' <summary>
    ''' 刪除事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        '刪除产品资料
        If GridView3.RowCount = 0 Then
            Exit Sub
        End If
        Dim SampleSend As New SampleSendControler
        Dim SSI As New List(Of SampleSendInfo)
        SSI = SampleSend.SampleSend_Getlist(GridView3.GetFocusedRowCellValue("SP_ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing)
        If SSI(0).SP_Check = True Then
            MsgBox("已经审核无法刪除")
            Exit Sub
        End If

        '''''''''''''''''''''''''''''''''
        Dim SPE As New SampleCustomerFeedbackControler
        Dim som As New List(Of SampleCustomerFeedbackinfo)
        Dim StrSO_ID As String = GridView3.GetFocusedRowCellValue("SO_ID").ToString
        som = SPE.SampleCustomerFeedback_getlist(StrSO_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False)
        If som.Count > 0 Then
            MsgBox("存在样办客戶反馈资料无法刪除", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        ''''''''''''''''''''''''''''''''''
        If MsgBox("你確定要刪除 " & GridView3.GetFocusedRowCellValue("SP_ID").ToString & " 這個样办寄送资料嗎?", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.No Then Exit Sub
        If SampleSend.SampleSend_Delete(GridView3.GetFocusedRowCellValue("SP_ID").ToString) = True Then

            sscon.SampleSendCode_Delete(GridView3.GetFocusedRowCellValue("SP_ID"), Nothing) '刪除子表

            Dim SampleMain As New SampleOrdersMainControler
            Dim SampleMainInfo As New SampleOrdersMainInfo
            SampleMainInfo.SO_ID = IIf(IsDBNull(StrSO_ID), Nothing, StrSO_ID)
            SampleMainInfo.SO_State = "F.样办进度"
            SampleMain.SampleOrdersMain_UpdateState(SampleMainInfo)

            cmdRef_Click(Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "查询事件"
    ''' <summary>
    ''' 查询事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdQurey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQurey.Click
        Dim fr As New frmSampleView
        fr = New frmSampleView
        fr.lbl_Title.Text = "样办查询--寄送"
        fr.EditItem = "SampleSend"
        fr.chk_ID.Text = "寄送单号(&F)"
        fr.ShowDialog()
        If fr.SampleSendList.Count = 0 Then
            gridSampleSend.DataSource = Nothing
            Exit Sub
        Else
            gridSampleSend.DataSource = fr.SampleSendList
        End If
    End Sub
#End Region

#Region "审核事件"
    ''' <summary>
    '''  审核事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub
        ''-------------------检查条码数量-------------
        Dim StrSO_ID As String = GridView3.GetFocusedRowCellValue("SO_ID").ToString
        Dim StrSS_Edition As String = GridView3.GetFocusedRowCellValue("SS_Edition").ToString

        '--------------------------------------------
        Dim solist As List(Of SampleOrdersSubInfo)
        Dim socon As New SampleOrdersSubControler

        solist = socon.SampleOrdersSub_GetList(StrSO_ID, StrSS_Edition)
        If solist.Count > 0 Then
            If solist(0).SO_Closed Then
                MsgBox("訂單已結案,無法審核操作")
                Exit Sub
            End If
        End If

        Dim fr As frmSampleSendAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleSendAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleSendAdd
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.CHECK
        fr.txtSP_ID.Text = GridView3.GetFocusedRowCellValue("SP_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "子表數據填充"
    ''' <summary>
    ''' 子表数据
    ''' </summary>
    ''' <remarks></remarks>
    Sub GridControl1show()
        Try
            GridControl1.DataSource = Nothing
            If GridView3.RowCount = 0 Then Exit Sub
            If GridView3.GetFocusedRowCellValue("SP_ID").ToString IsNot Nothing Then
                Dim StrSP_ID As String = GridView3.GetFocusedRowCellValue("SP_ID").ToString
                Dim StrSO_ID As String = GridView3.GetFocusedRowCellValue("SO_ID").ToString
                Dim StrSS_Edition As String = GridView3.GetFocusedRowCellValue("SS_Edition").ToString
                GridControl1.DataSource = sscon.SampleSendCode_Getlist(StrSP_ID, StrSO_ID, StrSS_Edition, Nothing, Nothing, Nothing)
                GridControl2.DataSource = ssfcon.SampleSendShipFiles_GetList(StrSP_ID, Nothing, Nothing)
            End If
        Catch
        End Try
    End Sub
    Private Sub GridView3_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView3.FocusedRowChanged
        GridControl1show()
    End Sub

    Private Sub gridSampleSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridSampleSend.Click
        GridView3_FocusedRowChanged(Nothing, Nothing)
    End Sub
#End Region

#Region "刷新事件"
    ''' <summary>
    ''' 刷新事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click

        Dim msi As New List(Of SampleSettingInfo)
        Dim msc As New SampleSettingController
        Dim StrCheck As String = Nothing
        Dim StrUser As String = Nothing

        msi = msc.SampleSetting_GetList(InUserID)
        If msi.Count > 0 Then
            '1.審核類型
            Select Case msi(0).SampleSendCheck
                Case "0,1"
                    StrCheck = Nothing
                Case "1"
                    StrCheck = "True"
                Case "0"
                    StrCheck = "False"
            End Select

            '1.用戶選擇
            If msi(0).SampleSendCreateUserID = "All" Then
                StrUser = Nothing
            Else
                StrUser = msi(0).SampleSendCreateUserID
            End If

            gridSampleSend.DataSource = SampleSend.SampleSend_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, StrCheck, Nothing, Nothing, msi(0).SampleSendBeginDate, Nothing, True, StrUser)
        Else
            gridSampleSend.DataSource = SampleSend.SampleSend_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing)
        End If
    End Sub

#End Region

#Region "查看事件"
    ''' <summary>
    ''' 查看事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLook.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub
        Dim fr As frmSampleSendAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleSendAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleSendAdd
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.VIEW
        fr.cmdSave.Visible = False
        fr.txtSP_ID.Text = GridView3.GetFocusedRowCellValue("SP_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "列印"
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim strSP_ID As String = GridView3.GetFocusedRowCellValue("SP_ID").ToString

        ltc.CollToDataSet(dss, "SampleSend", SampleSend.SampleSend_Getlist(strSP_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing))

        PreviewRPT(dss, "rptSampleSend", "寄送资料表", True, True)
        ltc = Nothing
        ' Me.Close()
    End Sub
#End Region

#Region "附件上傳"
    Private Sub cmdFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFile.Click
        '調用此产品资料的文件
        If GridView3.RowCount = 0 Then Exit Sub
        Dim open, update, down, Edit, del, detail As Boolean

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

        If GridView3.RowCount = 0 Then Exit Sub
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890507")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then update = True
            If pmwiL.Item(0).PMWS_Value = "否" Then update = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890508")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then down = True
            If pmwiL.Item(0).PMWS_Value = "否" Then down = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890509")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Edit = True
            If pmwiL.Item(0).PMWS_Value = "否" Then Edit = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890506")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then del = True
            If pmwiL.Item(0).PMWS_Value = "否" Then del = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890510")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then detail = True
            If pmwiL.Item(0).PMWS_Value = "否" Then detail = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890511")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then open = True
            If pmwiL.Item(0).PMWS_Value = "否" Then open = False
        End If

        FileShow("8905", GridView3.GetFocusedRowCellValue("SP_ID").ToString, open, update, down, Edit, del, detail)
    End Sub
#End Region


#Region "對Grid中的審核日期和運算類型設置顯示格式"
    Private Sub GridView1_CustomColumnDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles GridView1.CustomColumnDisplayText
        Try
            If e.Column.FieldName = "AddDate" Then
                If e.Value <> String.Empty Then
                    e.DisplayText = Format(CDate(e.Value), "yyyy-MM-dd HH:mm:ss")
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "GridView1_CustomColumnDisplayText方法出錯")
        End Try
    End Sub
#End Region

    Private Sub cmdInCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInCheck.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub
        Dim fr As frmSampleSendAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleSendAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleSendAdd
        fr.MdiParent = MDIMain

        fr.EditItem = EditEnumType.INCHECK
        fr.txtSP_ID.Text = GridView3.GetFocusedRowCellValue("SP_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmdExcelA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelA.Click
        If GridView3.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "導出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(gridSampleSend, saveFileDialog.FileName) Then
                MsgBox("已成功導出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If
    End Sub

    Private Sub cmdExcelB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelB.Click
        If GridView1.RowCount < 1 Then
            MsgBox("沒有可導出的數據", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        Dim sfd1 As New SaveFileDialog
        sfd1.DefaultExt = ".xls"
        sfd1.Filter = "Excel Files|*.xls|All Files|*.*"
        If sfd1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            GridView1.ExportToXls(sfd1.FileName)
            MsgBox("已成功導出到：" + sfd1.FileName, MsgBoxStyle.Information, "提示")
        End If

    End Sub

    Private Sub cmdShipFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipFile.Click, cmdShipFileAdd.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub
        Dim fr As New frmSampleSendShipFile
        fr = New frmSampleSendShipFile
        fr.EditItem = EditEnumType.ADD
        fr.EditSP_ID = GridView3.GetFocusedRowCellValue("SP_ID").ToString
        fr.EditPM_M_Code = GridView3.GetFocusedRowCellValue("PM_M_Code").ToString
        fr.EditSP_Qty = GridView3.GetFocusedRowCellValue("SP_Qty").ToString
        fr.ShowDialog()
    End Sub

    Private Sub cmdShipFileEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipFileEdit.Click
        On Error Resume Next
        If GridView5.RowCount = 0 Then Exit Sub
        Dim fr As New frmSampleSendShipFile
        fr = New frmSampleSendShipFile
        fr.EditItem = EditEnumType.EDIT
        fr.EditAutoID = GridView5.GetFocusedRowCellValue("AutoID").ToString
        fr.EditSP_ID = GridView3.GetFocusedRowCellValue("SP_ID").ToString
        fr.EditSP_Qty = GridView3.GetFocusedRowCellValue("SP_Qty").ToString
        fr.ShowDialog()
    End Sub

    Private Sub cmdShipFileLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipFileLook.Click
        On Error Resume Next
        If GridView5.RowCount = 0 Then Exit Sub
        Dim fr As New frmSampleSendShipFile
        fr = New frmSampleSendShipFile
        fr.EditItem = EditEnumType.VIEW
        fr.EditAutoID = GridView5.GetFocusedRowCellValue("AutoID").ToString
        fr.EditSP_ID = GridView3.GetFocusedRowCellValue("SP_ID").ToString
        fr.ShowDialog()
    End Sub
    Private Sub cmdShipDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipDel.Click
        Try
            If GridView5.RowCount = 0 Then Exit Sub
            If MsgBox("你確定要刪除 " & GridView5.GetFocusedRowCellValue("SP_ID").ToString & " 此标签资料?", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.No Then Exit Sub
            If ssfcon.SampleSendShipFiles_Delete(GridView5.GetFocusedRowCellValue("AutoID").ToString) = True Then
                MsgBox("删除成功", MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
        Catch
        End Try

    End Sub
    Private Sub cmdShipFilePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipFilePrintA.Click
        Dim dss As New DataSet
        Dim ltc1 As New CollectionToDataSet

        ltc1.CollToDataSet(dss, "SampleSendShipFiles", ssfcon.SampleSendShipFiles_GetList(Nothing, Nothing, Nothing))
        PreviewRPT(dss, "rptSampleSendShipFiles", "出货标签", True, True)
        ltc1 = Nothing

    End Sub
    Private Sub cmdShipFileCsv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipFileCsv.Click
        If GridView5.RowCount < 1 Then
            MsgBox("沒有可導出的數據", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        Dim sfd1 As New SaveFileDialog
        sfd1.DefaultExt = ".csv"
        sfd1.Filter = "CSV (Comma delimiter) (*.csv)|*.csv"
        sfd1.Title = "Save As CSV File"
        If sfd1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim strSP_ID As String = GridView3.GetFocusedRowCellValue("SP_ID").ToString()
            WriteToCSV(ssfcon.SampleSendShipFiles_DTGetList(strSP_ID, Nothing, Nothing), sfd1.FileName)
            MsgBox("已成功導出到：" + sfd1.FileName, MsgBoxStyle.Information, "提示")
        End If

    End Sub

    Public Function WriteToCSV(ByVal dataTable As DataTable, ByVal filePath As String) As Boolean
        Dim fileStream As System.IO.FileStream
        ' Dim streamReader As System.IO.StreamReader
        Dim streamWriter As System.IO.StreamWriter
        Dim i, j As Integer
        Dim strRow As String
        Dim strRowA As String
        Try
            If (System.IO.File.Exists(filePath)) Then
                System.IO.File.Delete(filePath)
            End If

            fileStream = New System.IO.FileStream(filePath, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write)

            If Not dataTable Is Nothing Then
                streamWriter = New System.IO.StreamWriter(fileStream, System.Text.Encoding.Default)
                strRowA = ""
                For i = 0 To dataTable.Columns.Count - 1
                    If i >= 2 And i <= 8 Then
                        Dim strCaption As String = String.Empty
                        Select Case dataTable.Columns(i).Caption
                            Case "SP_Qty"
                                strCaption = "Number"
                            Case "BoxID"
                                strCaption = "Box ID"
                            Case "QPN"
                                strCaption = "QPN"
                            Case "PartName"
                                strCaption = "Part Name"
                            Case "Code_ID"
                                strCaption = "SN"
                            Case "CO_ID"
                                strCaption = "Vender Name"
                            Case "LC"
                                strCaption = "L/C"
                        End Select
                        strRowA = strRowA + strCaption
                        strRowA += ","
                    End If
                Next
                streamWriter.WriteLine(strRowA)

                For i = 0 To dataTable.Rows.Count - 1
                    strRow = ""
                    For j = 0 To dataTable.Columns.Count - 1
                        If j >= 2 And j <= 8 Then
                            strRow = strRow + dataTable.Rows(i).Item(j).ToString()
                            strRow += ","
                        End If
                    Next
                    streamWriter.WriteLine(strRow)
                Next

                streamWriter.Close()
            End If
            fileStream.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub cmdShipFilePrintB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipFilePrintB.Click
        Dim dss As New DataSet
        Dim ltc1 As New CollectionToDataSet
        Dim strSP_ID As String = String.Empty
        strSP_ID = GridView3.GetFocusedRowCellValue("SP_ID").ToString
        ltc1.CollToDataSet(dss, "SampleSend", ssfcon.SampleSendShipFiles_GetList(Nothing, Nothing, Nothing))
        PreviewRPT(dss, "rptSampleSendShipFilesA", "出货标签", True, True)
        ltc1 = Nothing
    End Sub

End Class