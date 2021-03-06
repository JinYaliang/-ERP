Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.Product
Imports LFERP.Library.Production.ProuctionWareOutB
Imports LFERP.Library.MrpManager.MrpForecastOrder
Imports LFERP.Library.MrpManager.MrpSelect
Imports LFERP.Library.MrpManager.Bom_M
Imports LFERP.Library.MrpManager.MrpSetting
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core

Public Class frmMrpForecastOrderMain

#Region "實例與字段"
    '實例與字段
    Dim mrpcon As New MrpForecastOrderController
    Dim mrpecom As New MrpForecastOrderEntryController
    Dim ds As New DataSet
    Dim mrporderList As New List(Of MrpForecastOrderInfo)
#End Region

#Region "頁面的加載"
    '加載主表
    Private Sub frmMrpForcastOrderMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        'Dim MrpSetCon As New MrpSettingController
        'Dim MrpSet As New MrpSettingInfo
        'If MrpSetCon.MrpSetting_GetList(InUserID).Count > 0 Then
        '    MrpSet = MrpSetCon.MrpSetting_GetList(InUserID)(0)
        'Else
        '    MrpSet.forecastBeginDate = Year(Now) & "/01/01"
        'End If

        'GridControl1.DataSource = mrpcon.MrpForecastOrder_GetList(Nothing, MrpSet.forecastBeginDate, Nothing, Nothing, Nothing, Nothing)
        tsmRefresh_Click(Nothing, Nothing)
    End Sub
    '動態加載子表
    Private Sub GridView1_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        SetRightClickMenuEnable()
        If GridView1.FocusedRowHandle < 0 Then
            Grid1.DataSource = Nothing
        Else
            Grid1.DataSource = mrpecom.MrpForecastOrderEntry_GetList(GridView1.GetFocusedRowCellValue("ForecastID"))
        End If
    End Sub
#End Region

#Region "設置右擊菜單項是否可用"
    Private Sub GridControl1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridControl1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            SetRightClickMenuEnable()
        End If
    End Sub

    Private Sub SetRightClickMenuEnable()
        Dim mfoi As New MrpForecastOrderInfo
        Dim mfoiList As New List(Of MrpForecastOrderInfo)
        If GridView1.FocusedRowHandle >= 0 Then
            mfoiList = mrpcon.MrpForecastOrder_GetList(GridView1.GetFocusedRowCellValue("ForecastID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If mfoiList.Count > 0 Then
                mfoi = mfoiList(0)
            Else
                MsgBox(GridView1.GetFocusedRowCellValue("ForecastID") + "的預測單號已被其他用戶刪除", MsgBoxStyle.Information, "提示")
                tsmRefresh_Click(Nothing, Nothing)
                Exit Sub
            End If
        End If
        Try
            Dim c As ToolStripItem
            If GridView1.FocusedRowHandle < 0 Then
                For Each c In cmsMenuStrip.Items
                    If (c.Name = "tsmNew" Or c.Name = "tsmRefresh") Then
                        c.Enabled = True
                    Else
                        c.Enabled = False
                    End If
                Next
            ElseIf mfoi.CheckBit.Equals(True) Then
                For Each c In cmsMenuStrip.Items
                    If (c.Name = "tsmEdit" Or c.Name = "tsmDelete") Then
                        c.Enabled = False
                    Else
                        c.Enabled = True
                    End If
                Next
            Else
                For Each c In cmsMenuStrip.Items
                    c.Enabled = True
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "SetRightClickMenuEnable方法出錯")
        End Try
    End Sub
#End Region

#Region "菜單功能"
    ''' <summary>
    ''' 添加
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmNew.Click
        On Error Resume Next
        Dim fr As frmMrpForecastOrder
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMrpForecastOrder Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmMrpForecastOrder
        fr.Type = EditEnumType.ADD
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' 修改
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmEdit.Click
        On Error Resume Next
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If
        If mrpcon.MrpForecastOrder_GetIScheck(GridView1.GetFocusedRowCellValue("ForecastID").ToString()) = True Then
            MsgBox("審核狀態無法修改！")
            Exit Sub
        End If
        Dim fr As frmMrpForecastOrder
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMrpForecastOrder Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmMrpForecastOrder
        fr.StrForecastID = GridView1.GetFocusedRowCellValue("ForecastID").ToString
        fr.Type = EditEnumType.EDIT
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' 審核
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmCheck.Click
        On Error Resume Next
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If
        Dim fr As frmMrpForecastOrder
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMrpForecastOrder Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmMrpForecastOrder
        fr.StrForecastID = GridView1.GetFocusedRowCellValue("ForecastID").ToString
        fr.Type = EditEnumType.CHECK
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' 查看
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmView.Click
        On Error Resume Next
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If
        Dim fr As frmMrpForecastOrder
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMrpForecastOrder Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmMrpForecastOrder
        fr.StrForecastID = GridView1.GetFocusedRowCellValue("ForecastID").ToString
        fr.Type = EditEnumType.VIEW
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' 刪除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmDelete.Click
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        Dim StrForecastID As String = GridView1.GetFocusedRowCellValue("ForecastID").ToString()
        Dim StrAutoID As String = GridView1.GetFocusedRowCellValue("AutoID").ToString()

        If MsgBox("是否確定刪除預測單號：" + GridView1.GetFocusedRowCellValue("ForecastID").ToString() + "?", vbOKCancel, "提示") = vbOK Then
            If mrpcon.MrpForecastOrder_PreDelete(StrForecastID) = False Then
                Exit Sub
            End If
            If mrpcon.MrpForecastOrder_Delete(StrAutoID, Nothing) = True Then
                Dim result As Boolean
                result = mrpecom.MrpForecastOrderEntry_Delete(Nothing, StrForecastID)
                MsgBox("刪除成功！")
                GridControl1.DataSource = mrpcon.MrpForecastOrder_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            End If
        End If
    End Sub
    ''' <summary>
    ''' 查詢
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFind.Click
        'Dim fr As New frmSelect
        'fr.FormText = "MRP預測訂單"
        'fr.TableName = "MrpForecastOrder"
        'fr.ID = "ForecastID"
        'fr.ShowDialog()
        'Dim sc As New Select_Controller
        'If String.IsNullOrEmpty(tempValue) = False Then
        '    GridControl1.DataSource = sc.MrpForecastOrder_GetList(tempValue)
        'End If

        'Dim fr As New frmMrpSelect
        'fr = New frmMrpSelect
        'fr.EditItem = "MrpForecastOrder"
        'fr.lblinfo.Text = "預測訂單--查詢"
        'fr.lbltip.Text = "請選擇預測單號:"
        'fr.ShowDialog()
        'Select Case tempValue
        '    Case "固定樣式"
        '        GridControl1.DataSource = mrpcon.MrpForecastOrder_GetList(tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '    Case "自定義樣式"
        '        Dim MScon As New MrpSelect_Controller
        '        GridControl1.DataSource = MScon.MrpForecastOrder_Select_GetList("MrpForecastOrder", tempValue2)
        'End Select
    End Sub
    ''' <summary>
    ''' 刷新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmRefresh.Click
        Dim MrpSetCon As New MrpSettingController
        Dim MrpSet As New MrpSettingInfo
        If MrpSetCon.MrpSetting_GetList(InUserID).Count > 0 Then
            MrpSet = MrpSetCon.MrpSetting_GetList(InUserID)(0)
        Else
            MrpSet.forecastBeginDate = Year(Now) & "/01/01"
        End If
        GridControl1.DataSource = mrpcon.MrpForecastOrder_GetList(Nothing, MrpSet.forecastBeginDate, Nothing, Nothing, Nothing, Nothing, MrpSet.forecastDisplayNum)
        Grid1.DataSource = mrpecom.MrpForecastOrderEntry_GetList(GridView1.GetFocusedRowCellValue("ForecastID"))
        If GridView1.RowCount <= 0 Then
            tsmDelete.Enabled = False
            tsmEdit.Enabled = False
            tsmView.Enabled = False
            tsmCheck.Enabled = False
            tsmPaint.Enabled = False
            tsmFind.Enabled = False
        Else
            tsmDelete.Enabled = True
            tsmEdit.Enabled = True
            tsmView.Enabled = True
            tsmCheck.Enabled = True
            tsmPaint.Enabled = True
            tsmFind.Enabled = True
        End If
    End Sub
    '''' <summary>
    '''' 打印
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub tsmPaint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmPaint.Click
    '    Dim dss As New DataSet
    '    Dim ltc1 As New CollectionToDataSet
    '    Dim date1 As Date = "2012/10/06"
    '    Dim date2 As Date = "2014/01/09"
    '    Dim strSend As String = String.Empty
    '    strSend = "預測訂單資料表" + Format(date1, "yyyy/MM/dd") + Format(date2, "yyyy/MM/dd")
    '    '這裡添加 date1 和date2 為條件的查詢函數
    '    ltc1.CollToDataSet(dss, "MrpForecastOrder", mrpcon.MrpForecastOrder_GetList(Nothing, date1, date2, Nothing, Nothing, Nothing))
    '    PreviewRPT1(dss, "rptMrpForecastOrder", "表", strSend, strSend, True, True)
    '    ltc1 = Nothing
    'End Sub

    Private Sub tsm_PlanProduction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If
        Dim fr As frmMrpForecastOrder
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMrpForecastOrder Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmMrpForecastOrder
        fr.StrForecastID = GridView1.GetFocusedRowCellValue("ForecastID").ToString
        fr.Type = EditEnumType.ELSEONE
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

#End Region

#Region "設置權限"
    '設置權限
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "48030101")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                tsmNew.Visible = True
                tsmNew.Enabled = True
            End If

        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "48030102")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                tsmEdit.Visible = True
                tsmEdit.Enabled = True
            End If

        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "48030103") '審核
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                tsmDelete.Visible = True
                tsmDelete.Enabled = True
            End If

        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "48030104") '確認審核
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                tsmCheck.Visible = True
                tsmCheck.Enabled = True
            End If

        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "48030105") '確認審核
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                tsm_ChildCheck.Visible = True
                tsm_ChildCheck.Enabled = True
            End If

        End If
    End Sub
#End Region

    Private Sub frmMrpForecastOrderMain_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        tsmRefresh_Click(Nothing, Nothing)
    End Sub

    Private Sub tsm_MRPOrderFous_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_MRPOrderFous.Click
        Try
            Dim MrpSetCon As New MrpSettingController
            Dim MrpSet As New MrpSettingInfo
            If MrpSetCon.MrpSetting_GetList(InUserID).Count > 0 Then
                MrpSet = MrpSetCon.MrpSetting_GetList(InUserID)(0)
            Else
                MrpSet.forecastBeginDate = Year(Now) & "/01/01"
            End If
            Dim dss As New DataSet
            Dim ltc1 As New CollectionToDataSet
            Dim ltc2 As New CollectionToDataSet
            Dim StrSend As String = String.Empty
            StrSend = InUser
            ltc1.CollToDataSet(dss, "MrpForecastOrder", mrpcon.MrpForecastOrder_GetList(GridView1.GetFocusedRowCellValue("ForecastID"), MrpSet.forecastBeginDate, Nothing, Nothing, Nothing, Nothing, Nothing))
            ltc2.CollToDataSet(dss, "MrpForecastOrderEntry", mrpecom.MrpForecastOrderEntry_GetList(GridView1.GetFocusedRowCellValue("ForecastID")))
            PreviewRPT1(dss, "rptMrpForecastOrderInfo", StrSend, StrSend, "表", True, True)
            ltc1 = Nothing
            ltc2 = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
      
    End Sub

    Private Sub tsm_MRPOrderTotal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_MRPOrderTotal.Click
        On Error Resume Next
        Dim fr As MrpReportSelect
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is MrpReportSelect Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New MrpReportSelect
        fr.intShowPage = 0
        fr.ShowDialog()
        fr.Focus()
    End Sub

   
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'On Error Resume Next
        'Dim fr As frmMRPForeCastProduction
        'For Each fr In MDIMain.MdiChildren
        '    If TypeOf fr Is frmMRPForeCastProduction Then
        '        fr.Activate()
        '        Exit Sub
        '    End If
        'Next
        'fr = New frmMRPForeCastProduction
        'fr.MdiParent = MDIMain
        'fr.WindowState = FormWindowState.Maximized
        'fr.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next

        Dim exapp As New Excel.Application
        Dim exbook As Excel.Workbook
        Dim exsheet As Excel.Worksheet


        exapp = CreateObject("Excel.Application")
        exbook = exapp.Workbooks.Add
        exsheet = exapp.Worksheets(1)
        exsheet.Range("A1:B2").Borders(7).Weight = 2
        exsheet.Range("A1:B2").Borders(8).Weight = 2
        exsheet.Range("A1:B2").Borders(9).Weight = 2
        exsheet.Range("A1:B2").Borders(10).Weight = 2
        exsheet.Range("A1:B2").Borders(11).Weight = 2
        exsheet.Range("A1:B2").Borders(12).Weight = 2

        exbook.Sheets("Sheet1").Name = "1111"
        exbook.Sheets("Sheet2").Name = "2222"
        exapp.Visible = True
    End Sub

    Private Sub cmsExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsExcel.Click, cmsSubExcel.Click
        If sender.owner Is cmsMenuStrip Then
            ConrotlExportExcel(GridControl1)
        ElseIf sender.owner Is cmsSub Then
            ConrotlExportExcel(Grid1)
        End If
    End Sub

    Private Sub tsm_ChildCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_ChildCheck.Click
        On Error Resume Next
        Dim fr As frmMrpForecastOrder
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMrpForecastOrder Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmMrpForecastOrder
        fr.Type = EditEnumType.ELSEONE
        fr.StrForecastID = GridView1.GetFocusedRowCellValue("ForecastID").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
End Class