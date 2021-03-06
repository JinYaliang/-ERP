Imports LFERP.Library.NmetalSampleManager.NmetalSamplePace
Imports LFERP.SystemManager
Imports LFERP.Library.ProductionController
Imports LFERP.Library.NmetalSampleManager.NmetalSampleCollection
Imports LFERP.Library.NmetalSampleManager.NmetalSampleSetting


Public Class frmNmetalSamplePaceMain

#Region "属性"
    Dim spCon As New NmetalSamplePaceControler
    Dim strDPT As String
    Dim StrCheck As String = Nothing
    Dim StrUser As String = Nothing
    Dim StrBeginDate As String = Nothing
#End Region

#Region "窗体载入"
    Private Sub frmSamplePaceMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim fc As New LFERP.Library.ProductionController.ProductionFieldControl
        GridControl2.DataSource = fc.ProductionFieldControl_GetList(InUserID, Nothing)
        twWare.ExpandAll()
        PowerUser()
        ShowDateValue()

    End Sub
#End Region
    Sub ShowDateValue()
        Dim msi As New List(Of NmetalSampleSettingInfo)
        Dim msc As New NmetalSampleSettingController
        msi = msc.NmetalSampleSetting_GetList(InUserID)
        If msi.Count > 0 Then
            '1.審核類型
            Select Case msi(0).SamplePaceCheck
                Case "0,1"
                    StrCheck = Nothing
                Case "1"
                    StrCheck = "True"
                Case "0"
                    StrCheck = "False"
            End Select

            '1.用戶選擇
            If msi(0).SamplePaceCreateUserID = "All" Then
                StrUser = Nothing
            Else
                StrUser = msi(0).SamplePaceCreateUserID
            End If
            StrBeginDate = msi(0).SamplePaceBeginDate
        Else
            StrCheck = Nothing
            StrUser = Nothing
            StrBeginDate = Nothing
        End If
    End Sub

#Region "表格事件"
    Private Sub GridControl2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridControl2.KeyDown
        If e.KeyCode = Keys.Enter Then
            GridControl2_MouseUp(Nothing, Nothing)
        End If
    End Sub
    Private Sub GridControl2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridControl2.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            GridControl2_MouseUp(Nothing, Nothing)
        End If
    End Sub
    '指定當前部門物料收發信息記錄
    Private Sub GridControl2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridControl2.MouseUp

        If GridView2.RowCount = 0 Then Exit Sub

        ShowDateValue()
        strDPT = GridView2.GetFocusedRowCellValue("ControlDep").ToString
        twWare.ExpandAll()
        Try
            twWare.SelectedNode = twWare.Nodes(0)
            Dim a As New NmetalSamplePaceControler
            Dim b As New List(Of NmetalSamplePaceInfo)
            Dim c As New List(Of NmetalSamplePaceInfo)

            ' b = a.ProductionField_GetList(Nothing, Nothing, "發料", Nothing, strDPT, Nothing, Nothing, False, Nothing, Nothing, "2", Nothing, Nothing)
            ' c = a.ProductionField_GetList(Nothing, Nothing, "收料", Nothing, strDPT, Nothing, Nothing, False, Nothing, Nothing, "2", Nothing, Nothing)

            b = a.NmetalSamplePace_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, StrBeginDate, Nothing, True, Nothing, strDPT, Nothing, StrCheck, False, StrUser)
            c = a.NmetalSamplePace_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, StrBeginDate, Nothing, True, Nothing, Nothing, strDPT, StrCheck, False, StrUser)

            If b.Count > 0 Then
                twWare.Nodes.Item(1).Nodes.Item(0).Text = "未審核 (" & b.Count & ")"
                twWare.Nodes.Item(1).Nodes.Item(0).ForeColor = Color.Red
            Else
                twWare.Nodes.Item(1).Nodes.Item(0).Text = "未審核"
                twWare.Nodes.Item(1).Nodes.Item(0).ForeColor = Color.Black
            End If

            If c.Count > 0 Then
                twWare.Nodes.Item(0).Nodes.Item(0).Text = "未審核 (" & c.Count & ")"
                twWare.Nodes.Item(0).Nodes.Item(0).ForeColor = Color.Red
            Else
                twWare.Nodes.Item(0).Nodes.Item(0).Text = "未審核"
                twWare.Nodes.Item(0).Nodes.Item(0).ForeColor = Color.Black
            End If

            Grid.DataSource = Nothing
        Catch ex As Exception
        End Try
    End Sub


    Private Sub twWare_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles twWare.AfterSelect
        If e.Node.Level = 1 Then
            ShowDateValue()
            ShowData(Mid(twWare.SelectedNode.Text, 1, 3), Nothing)
        End If
    End Sub

    Sub ShowData(ByVal CheckState As String, ByVal strFP_OutType As String)
        Select Case CheckState
            Case "未審核"
                If twWare.SelectedNode.Parent.Text = "收入項目" Then
                    Grid.DataSource = spCon.NmetalSamplePace_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, StrBeginDate, Nothing, True, Nothing, Nothing, strDPT, StrCheck, False, StrUser)
                    SealCode_ID.Visible = False
                End If
                If twWare.SelectedNode.Parent.Text = "發出項目" Then
                    Grid.DataSource = spCon.NmetalSamplePace_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, StrBeginDate, Nothing, True, Nothing, strDPT, Nothing, StrCheck, False, StrUser)
                    SealCode_ID.Visible = True
                End If
            Case "已審核"
                If twWare.SelectedNode.Parent.Text = "收入項目" Then
                    Grid.DataSource = spCon.NmetalSamplePace_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, StrBeginDate, Nothing, True, Nothing, Nothing, strDPT, StrCheck, True, StrUser)
                    SealCode_ID.Visible = False
                End If
                If twWare.SelectedNode.Parent.Text = "發出項目" Then
                    Grid.DataSource = spCon.NmetalSamplePace_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, StrBeginDate, Nothing, True, Nothing, strDPT, Nothing, StrCheck, True, StrUser)
                    SealCode_ID.Visible = True
                End If
        End Select
    End Sub
#End Region

#Region "权限设置"
    '设置权限
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890401")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdOut.Enabled = True
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
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890405")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdPrint.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890406")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdIn.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890410")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdPacking.Enabled = True
        End If
    End Sub
#End Region

#Region "发料事件"
    '發料事件'
    Private Sub cmdOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOut.Click
        On Error Resume Next
        Dim fr As frmNmetalSamplePaceInsert
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSamplePaceInsert Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSamplePaceInsert
        fr.MdiParent = MDIMain
        'fr.Lbl_Title.Text = "样办收发--发料"

        fr.EditItem = EditEnumType.OUT
        'fr.EditItem = "Out"
        fr.EditDep = GridView2.GetFocusedRowCellValue("ControlDep").ToString

        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "修改事件"
    '修改事件
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim splist As List(Of NmetalSamplePaceInfo)
        Dim strAutoID As String = GridView1.GetFocusedRowCellValue("AutoID").ToString
        If strAutoID = String.Empty Or strAutoID = Nothing Then
            Exit Sub
        End If

        splist = spCon.NmetalSamplePace_Getlist(strAutoID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If splist.Count > 0 Then
            If splist(0).SE_Check Then
                MsgBox("此操作已被审核,不允許刪除!")
                Exit Sub
            End If
            If splist(0).SE_InCheck Then
                MsgBox("此操作已被确认,不允許刪除!")
                Exit Sub
            End If
        End If
        '--------------------------------------------
        On Error Resume Next
        Dim fr As frmNmetalSamplePaceInsert
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSamplePaceInsert Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSamplePaceInsert
        fr.MdiParent = MDIMain
        'fr.Lbl_Title.Text = "样办收发--修改"
        fr.EditItem = EditEnumType.EDIT
        'fr.EditItem = "Edit"
        fr.GetEditValue = GridView1.GetFocusedRowCellValue("AutoID").ToString
        fr.EditSE_ID = GridView1.GetFocusedRowCellValue("SE_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "删除事件"
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim splist As List(Of NmetalSamplePaceInfo)
        Dim strAutoID As String = GridView1.GetFocusedRowCellValue("AutoID").ToString
        If strAutoID = String.Empty Or strAutoID = Nothing Then
            Exit Sub
        End If

        splist = spCon.NmetalSamplePace_Getlist(strAutoID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If splist.Count > 0 Then
            If splist(0).SE_Check Then
                MsgBox("此操作已被审核,不允許刪除!")
                Exit Sub
            End If
            If splist(0).SE_InCheck Then
                MsgBox("此操作已被确认,不允許刪除!")
                Exit Sub
            End If

            If MsgBox("你確定刪除單號為  '" & GridView1.GetFocusedRowCellValue("SE_ID").ToString & "'  的記錄嗎?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                '主档删除
                If spCon.NmetalSamplePace_DeleteSE_ID(GridView1.GetFocusedRowCellValue("SE_ID").ToString) = True Then
                    MsgBox("刪除成功!")
                Else
                    MsgBox("刪除失敗,請檢查原因!")
                    Exit Sub
                End If
                '子档删除
                If spCon.NmetalSamplePaceBarCode_Delete(Nothing, GridView1.GetFocusedRowCellValue("SE_ID").ToString, Nothing) = False Then
                    MsgBox("刪除失敗,請檢查原因!")
                    Exit Sub
                End If
                '保存删除记录
                Dim spinfo As New NmetalSamplePaceInfo
                spinfo.SE_ID = GridView1.GetFocusedRowCellValue("SE_ID").ToString
                spinfo.SE_AddUserID = InUserID
                spinfo.AutoID = strAutoID
                If spCon.NmetalSamplePace_AddDelLogin(spinfo) = False Then
                    MsgBox("刪除记录保存,請檢查原因!")
                    Exit Sub
                End If
                Grid.DataSource = spCon.NmetalSamplePace_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, strDPT, False, Nothing, Nothing)
            End If

        End If
    End Sub
#End Region

#Region "审核事件"
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim splist As List(Of NmetalSamplePaceInfo)
        Dim strAutoID As String = GridView1.GetFocusedRowCellValue("AutoID").ToString
        If strAutoID = String.Empty Or strAutoID = Nothing Then
            Exit Sub
        End If

        splist = spCon.NmetalSamplePace_Getlist(strAutoID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If splist.Count > 0 Then
            'If splist(0).SE_Check Then
            '    MsgBox("此操作已被审核,不允許审核!")
            '    Exit Sub
            'End If
            If splist(0).SE_InCheck Then
                MsgBox("此操作已被确认,不允許审核!")
                Exit Sub
            End If
        End If

        On Error Resume Next
        Dim fr As frmNmetalSamplePaceInsert
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSamplePaceInsert Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSamplePaceInsert
        fr.MdiParent = MDIMain
        'fr.Lbl_Title.Text = "样办收发--发料审核"
        'fr.EditItem = "Check"
        fr.EditItem = EditEnumType.CHECK
        fr.GetEditValue = GridView1.GetFocusedRowCellValue("AutoID").ToString
        fr.EditSE_ID = GridView1.GetFocusedRowCellValue("SE_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "确认收料"
    '收料事件'
    Private Sub cmdIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIn.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim splist As List(Of NmetalSamplePaceInfo)
        Dim strAutoID As String = GridView1.GetFocusedRowCellValue("AutoID").ToString
        If strAutoID = String.Empty Or strAutoID = Nothing Then
            Exit Sub
        End If

        splist = spCon.NmetalSamplePace_Getlist(strAutoID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If splist.Count > 0 Then
            If splist(0).SE_OutD_ID = strDPT And splist(0).SE_InD_ID <> String.Empty And splist(0).SE_OutD_ID <> splist(0).SE_InD_ID Then
                MsgBox("不能確認本部門的資料,不允許确认!")
                Exit Sub
            End If

            If splist(0).SE_Check = False Then
                MsgBox("此操作没有审核,不允許确认!")
                Exit Sub
            End If
            If splist(0).SE_InCheck Then
                MsgBox("此操作已被确认,不允許确认!")
                Exit Sub
            End If

        End If

        '------------------------------------------------------------
        On Error Resume Next
        Dim fr As frmNmetalSamplePaceInsert
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSamplePaceInsert Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSamplePaceInsert
        fr.MdiParent = MDIMain
        'fr.Lbl_Title.Text = "样办收发--收料确认"
        'fr.EditItem = "In"
        fr.EditItem = EditEnumType.INCHECK
        fr.GetEditValue = GridView1.GetFocusedRowCellValue("AutoID").ToString
        fr.EditSE_ID = GridView1.GetFocusedRowCellValue("SE_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "查看事件"
    Private Sub cmdLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLook.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmNmetalSamplePaceInsert
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSamplePaceInsert Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSamplePaceInsert
        fr.MdiParent = MDIMain
        'fr.Lbl_Title.Text = "样办收发--查看"
        'fr.EditItem = "Look"
        fr.EditItem = EditEnumType.VIEW
        fr.GetEditValue = GridView1.GetFocusedRowCellValue("AutoID").ToString
        fr.EditSE_ID = GridView1.GetFocusedRowCellValue("SE_ID").ToString
        fr.EditBooL = SealCode_ID.Visible
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "条码查询"
    Private Sub cmdCodeQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCodeQuery.Click
        On Error Resume Next

        Dim fr As frmNmetalSamplePaceQuery
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSamplePaceQuery Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSamplePaceQuery
        fr.MdiParent = MDIMain

        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

    '#Region "样办查询"
    '    Private Sub cmdQuery_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
    '        Dim fr As New frmSampleView
    '        fr = New frmSampleView
    '        fr.lbl_Title.Text = "样办查询--进度"
    '        fr.EditItem = "SamplePace"
    '        fr.ShowDialog()
    '        If fr.SamplePaceList.Count = 0 Then
    '            Exit Sub
    '        Else
    '            Grid.DataSource = fr.SamplePaceList
    '        End If
    '    End Sub
    '#End Region

#Region "刷新事件"
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        GridControl2_MouseUp(Nothing, Nothing)

    End Sub
#End Region

#Region "對Grid中的審核日期和運算類型設置顯示格式"
    Private Sub GridView1_CustomColumnDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles GridView1.CustomColumnDisplayText
        Try
            '----------------當訂單開始日期為空時，則不顯示----------------
            If e.Column.FieldName = "SE_InTime" Then
                If e.Value = Nothing Then e.DisplayText = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "GridView1_CustomColumnDisplayText方法出錯")
        End Try
    End Sub
#End Region

    Private Sub cmdDep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDep.Click
        On Error Resume Next

        Dim fr As frmNmetalSamplePaceLoan
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmNmetalSamplePaceLoan Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmNmetalSamplePaceLoan
        fr.D_ID = GridView2.GetFocusedRowCellValue("ControlDep").ToString
        fr.MdiParent = MDIMain

        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Dim fr As New frmNmetalSamplePaceLoad
        fr = New frmNmetalSamplePaceLoad
        fr.ShowDialog()

        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim sacon As New NmetalSampleCollectionControler
        Dim sclist As New List(Of NmetalSampleCollectionInfo)
        sclist = sacon.NmetalSampleCollection_Getlist("Z", Nothing, Nothing, tempValue2, tempValue3, Nothing, False, Nothing, tempValue4, Nothing, tempValue, Nothing, Nothing, Nothing, Nothing)

        If sclist.Count <= 0 Then
            MsgBox("没有数据可列印!")
            Exit Sub
        End If
        ltc.CollToDataSet(dss, "SampleCollection", sclist)

        PreviewRPT(dss, "NmetalColleciton_Select", "部门在制清单", True, True)
        'PreviewRPT(dss, "rptSampleCollection", "部门在制清单", True, True)

        ltc = Nothing

        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
    End Sub

    Private Sub cmdExcelA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelA.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "導出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(Grid, saveFileDialog.FileName) Then
                MsgBox("已成功導出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If
    End Sub

    Private Sub cmdPacking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPacking.Click
        If GridView1.RowCount = 0 Then
            MessageBox.Show("沒有數據可裝箱!", "提示")
            Exit Sub
        End If

        Dim strD_ID As String = GridView1.GetFocusedRowCellValue("SE_OutD_ID").ToString
        Dim strAutoID As String = GridView1.GetFocusedRowCellValue("AutoID").ToString
        Dim splist As List(Of NmetalSamplePaceInfo)
        splist = spCon.NmetalSamplePace_Getlist(strAutoID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If splist.Count > 0 Then
            If splist(0).SE_InCheck Then
                MessageBox.Show("此單已經確認,不能裝箱!", "提示")
                Exit Sub
            End If
            If splist(0).PK_Code_ID <> String.Empty Then
                MessageBox.Show("此單已經裝箱,不能再裝箱!", "提示")
                Exit Sub
            End If
        End If

        If strD_ID = String.Empty Then
            MessageBox.Show("沒有裝箱部門,不能裝箱!", "提示")
            Exit Sub
        End If

        If strDPT <> strD_ID Then
            MessageBox.Show("選擇的部門與發料部門不同,不能裝箱!", "提示")
            Exit Sub
        End If

        Dim frPk As New frmNmetalSamplePacePK
        frPk.D_ID = strD_ID
        frPk.AutoID = strAutoID
        frPk.PM_M_Code = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString '產品編號
        frPk.Qty = GridView1.GetFocusedRowCellValue("SE_Qty").ToString '裝箱數量
        frPk.SE_ID = GridView1.GetFocusedRowCellValue("SE_ID").ToString '收發單號
        frPk.SE_TypeName = GridView1.GetFocusedRowCellValue("SE_TypeName").ToString '收發單號

        frPk.Show()
    End Sub
End Class




