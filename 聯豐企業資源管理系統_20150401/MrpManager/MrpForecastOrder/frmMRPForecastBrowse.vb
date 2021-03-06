Imports LFERP.Library.MrpManager.MrpForecastOrder
Imports LFERP.Library.MrpManager.MrpSetting
Imports LFERP.Library.MrpManager.Bom_M
Imports LFERP.Library.MrpManager.MrpMaterialCode
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core

Public Class frmMrpForecastBrowse

#Region "字段與實例"
    Dim bomCon As New Bom_MController
    Dim mscon As New MrpSettingController
    Dim mfoCon As New MrpForecastOrderController
    Dim mmcon As New MrpMaterialCodeController
    Dim DT_M As New DataTable
    Dim DT_D As New DataTable

    Dim _StrSelect As String
    Dim _StrSource As String
    Private Property StrSource() As String
        Get
            Return _StrSource
        End Get
        Set(ByVal value As String)
            _StrSource = value
        End Set
    End Property
    Private Property StrSelect() As String
        Get
            Return _StrSelect
        End Get
        Set(ByVal value As String)
            _StrSelect = value
        End Set
    End Property
#End Region

#Region "頁面加載"
    Private Sub frmMRPForecastBrowse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '1.客戶控件----------------------------------------------------------------
        txtMO_CusterID.Properties.DisplayMember = "MO_CusterName"    'txt
        txtMO_CusterID.Properties.ValueMember = "MO_CusterID"   'EditValue
        Dim MrpInfo As New MrpForecastOrderInfo
        Dim MrpInfoList As New List(Of MrpForecastOrderInfo)
        MrpInfo.MO_CusterID = "*全部*"
        MrpInfo.MO_CusterName = "*全部*"
        MrpInfoList = mfoCon.CusterGetName(Nothing, Nothing)
        MrpInfoList.Insert(0, MrpInfo)
        txtMO_CusterID.Properties.DataSource = MrpInfoList   '客戶編號控件賦值

        '2.來源碼控件----------------------------------------------------------------
        gueSource.Properties.DisplayMember = "MC_Source"    'txt
        gueSource.Properties.ValueMember = "MC_SourceID"   'EditValue
        Dim MrpcInfo As New MrpMaterialCodeInfo
        Dim MrpList As New List(Of MrpMaterialCodeInfo)
        MrpList = mmcon.MrpSource_GetList(Nothing, Nothing)
        gueSource.Text = "*全部*"
        gueSource.EditValue = "ALL"
        MrpcInfo.MC_SourceID = "ALL"
        MrpcInfo.MC_Source = "*全部*"
        MrpList.Insert(0, MrpcInfo)
        gueSource.Properties.DataSource = MrpList      '來源碼控件賦值

        '3.產品控件----------------------------------------------------------------
        GLU_MCode.Properties.DisplayMember = "M_Name"
        GLU_MCode.Properties.ValueMember = "ParentGroup"
        Dim BoInfo As New Bom_MInfo
        Dim MrpInfoList2 As New List(Of Bom_MInfo)
        BoInfo.ParentGroup = "*全部*"
        BoInfo.M_Name = "*全部*"
        BoInfo.M_Gauge = "*全部*"
        BoInfo.M_Unit = "*全部*"
        ' BoInfo.M_Source = "*全部*"
        'MrpInfoList2 = bomCon.Bom_M_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        MrpInfoList2.Insert(0, BoInfo)
        GLU_MCode.Properties.DataSource = MrpInfoList2         '物料信息彈窗賦值

        '4.載入用戶的默認設置-----------
        Dim MrpSet As New MrpSettingInfo
        Dim msList As New List(Of MrpSettingInfo)
        msList = mscon.MrpSetting_GetList(InUserID)
        If msList.Count > 0 Then   '判斷數據來源是否為空
            MrpSet = msList(0)
        Else
            MrpSet.forecastBrowserBeginDate = Year(Now) & "/01/01"
            MrpSet.forecastBrowserEndDate = Year(Now) & "/12/31"
        End If
        If MrpSet.forecastBrowserBeginDate = Nothing Then
            MrpSet.forecastBrowserBeginDate = Year(Now) & "/01/01"
        End If
        If MrpSet.forecastBrowserEndDate = Nothing Then
            MrpSet.forecastBrowserEndDate = Year(Now) & "/12/31"
        End If
        det_StartDate.DateTime = MrpSet.forecastBrowserBeginDate       '加載數據和個人的默認設置
        det_EndDate.DateTime = MrpSet.forecastBrowserEndDate

        'LoadSub(Mid(txtSelect.EditValue, 1, 3), MrpSet.forecastBrowserBeginDate, MrpSet.forecastBrowserEndDate, Nothing, Nothing, Nothing)

    End Sub
#End Region

#Region "查詢按鈕"
    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        '1.客戶數據
        Dim StrCusterID As String
        If txtMO_CusterID.Text = String.Empty Or txtMO_CusterID.Text = "*全部*" Then '客戶條件判斷
            StrCusterID = Nothing
        Else
            StrCusterID = txtMO_CusterID.EditValue
        End If
        '2.來源碼數據
        If gueSource.EditValue = "ALL" Or gueSource.EditValue = String.Empty Then '來源碼條件判斷
            StrSource = Nothing
        Else
            StrSource = gueSource.EditValue
        End If
        '3.產品編碼數擾
        Dim StrMCode As String
        If GLU_MCode.EditValue = "*全部*" Or GLU_MCode.EditValue = String.Empty Then '物料編號條件判斷
            StrMCode = Nothing
        Else
            StrMCode = GLU_MCode.EditValue
        End If
        '4.起止日期
        Dim StratDate As Date = det_StartDate.DateTime '起止時間
        Dim EndDate As Date = det_EndDate.DateTime
        '5.查詢條件
        StrSelect = Mid(txtSelect.EditValue, 1, 3) '查詢條件
        '6.執行查詢
        LoadSub(StrSelect, StratDate, EndDate, StrCusterID, StrMCode, StrSource)    '進行查詢
    End Sub
#End Region

#Region "Excel導出"
#Region "同時導出2個Excel"
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If BandedGridView1.RowCount < 1 Then
            MsgBox("上表沒有可導出的數據", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        Dim sfd As New SaveFileDialog
        sfd.DefaultExt = ".xls"
        sfd.Filter = "Excel Files|*.xls|All Files|*.*"
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            GridControl1.ExportToXls(sfd.FileName)
            MsgBox("已成功導出", MsgBoxStyle.Information, "提示")
        End If
        If BandedGridView2.RowCount < 1 Then
            MsgBox("下表沒有可導出的數據", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        Dim sfd1 As New SaveFileDialog
        sfd1.DefaultExt = ".xls"
        sfd1.Filter = "Excel Files|*.xls|All Files|*.*"
        If sfd1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            GridControl2.ExportToXls(sfd1.FileName)
            MsgBox("已成功導出", MsgBoxStyle.Information, "提示")
        End If
    End Sub
#End Region

#Region " 菜單欄—主表Excel導出"
    Private Sub cms_ToExcelChild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cms_ToExcelChildA.Click, cms_ToExcelChildB.Click
        Select Case sender.name
            Case "cms_ToExcelChildA"
                If BandedGridView1.RowCount < 1 Then
                    MsgBox("沒有可導出的數據", MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If
                Dim sfd As New SaveFileDialog
                sfd.DefaultExt = ".xls"
                sfd.Filter = "Excel Files|*.xls|All Files|*.*"
                If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    GridControl1.ExportToXls(sfd.FileName)
                    MsgBox("已成功導出", MsgBoxStyle.Information, "提示")
                End If
            Case "cms_ToExcelChildB"
                If BandedGridView2.RowCount < 1 Then
                    MsgBox("沒有可導出的數據", MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If
                Dim sfd As New SaveFileDialog
                sfd.DefaultExt = ".xls"
                sfd.Filter = "Excel Files|*.xls|All Files|*.*"
                If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    GridControl2.ExportToXls(sfd.FileName)
                    MsgBox("已成功導出", MsgBoxStyle.Information, "提示")
                End If
        End Select
    End Sub
#End Region

#End Region

#Region "加載方法"
    Private Sub LoadSub(ByVal StrSelect As String, ByVal date1 As Date, ByVal date2 As Date, ByVal CusterID As String, ByVal M_Code As String, ByVal Source As String)
        Try

            BandedGridView1.Bands.Clear()      '清空BandedGridView1
            BandedGridView1.Columns.Clear()
            BandedGridView2.Bands.Clear()   '清空BandedGridView2
            BandedGridView2.Columns.Clear()
            Select Case StrSelect
                Case "[A]" ':周數"
                    DT_M = mfoCon.GetWeekAllInfo(date1, date2, CusterID, M_Code, Source)

                    If DT_M.Rows.Count > 0 Then
                        BGridViewDeal(DT_M, 6, BandedGridView1, GridControl1, "周")
                    End If

                    DT_D = mfoCon.GetWeekAllInfoChild(date1, date2, CusterID, M_Code, Source)
                    If DT_D.Rows.Count > 0 Then
                        BGridViewDeal(DT_D, 7, BandedGridView2, GridControl2, "周")
                    End If
                Case "[B]" ':月份"
                    DT_M = mfoCon.GetMonthAllInfo(date1, date2, CusterID, M_Code, Source)
                    If DT_M.Rows.Count > 0 Then
                        BGridViewDeal(DT_M, 6, BandedGridView1, GridControl1, "月")
                    End If

                    DT_D = mfoCon.GetMonthAllInfoChild(date1, date2, CusterID, M_Code, Source) '子表的數據源
                    If DT_D.Rows.Count > 0 Then
                        BGridViewDeal(DT_D, 7, BandedGridView2, GridControl2, "月")
                    End If
                Case "[C]" ':周數【客戶】"
                    DT_M = mfoCon.GetWeekInfo(date1, date2, CusterID, M_Code, Source) '主表的數據源
                    If DT_M.Rows.Count > 0 Then
                        BGridViewDeal(DT_M, 8, BandedGridView1, GridControl1, "周")
                    End If

                    DT_D = mfoCon.GetWeekInfoChild(date1, date2, CusterID, M_Code, Source) '子表的數據源
                    If DT_D.Rows.Count > 0 Then
                        BGridViewDeal(DT_D, 9, BandedGridView2, GridControl2, "周")
                    End If

                Case "[D]" ':月份【客戶】"
                    DT_M = mfoCon.GetMonthInfo(date1, date2, CusterID, M_Code, Source)  '主表的數據源
                    If DT_M.Rows.Count > 0 Then
                        BGridViewDeal(DT_M, 8, BandedGridView1, GridControl1, "月")
                    End If

                    DT_D = mfoCon.GetMonthInfoChild(date1, date2, CusterID, M_Code, Source) '子表的數據源
                    If DT_D.Rows.Count > 0 Then
                        BGridViewDeal(DT_D, 9, BandedGridView2, GridControl2, "月")
                    End If
            End Select
        Catch ex As Exception
        End Try
        BGColumnSet(BandedGridView1)
        BGColumnSet(BandedGridView2)
    End Sub
#End Region

#Region "Table 處理"
    Private Sub TableDeal(ByVal dt As DataTable, ByVal begionNum As Integer, ByVal StrType As String)
        Dim col As New DataColumn
        col.ColumnName = "總計"
        dt.Columns.Add(col)
        Dim TotalSum As Double = 0

        For i As Integer = 0 To dt.Rows.Count - 1
            For j As Integer = begionNum To dt.Columns.Count - 2 'table中每一行中數量求和
                If i = 0 Then
                    dt.Columns(j).ColumnName = Mid((dt.Columns(j).ColumnName), 1, 4) + "年" + Mid((dt.Columns(j).ColumnName), 5, 2) + StrType
                End If

                If IsDBNull(dt.Rows(i)(j)) = False Then
                    TotalSum = TotalSum + dt.Rows(i)(j)
                End If
            Next
            dt.Rows(i)(col) = TotalSum
            TotalSum = 0
        Next
    End Sub
#End Region

#Region "BGridView 賦值 處理 "
    Private Sub BGridViewDeal(ByVal dt As DataTable, ByVal begionNum As Integer, ByVal BG As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, ByVal GC As DevExpress.XtraGrid.GridControl, ByVal StrType As String)
        '1.求和與修改列標題
        TableDeal(dt, begionNum, StrType)

        Dim i As Integer = 0
        Dim intA As Integer = 0
        Dim strYearA As String = String.Empty
        Dim strYearB As String = String.Empty
        For i = 0 To dt.Columns.Count - 1
            If i >= 0 And i <= begionNum - 3 Then
                If i = 0 Then
                    BG.Bands.AddBand("產品信息")
                    If StrSelect = "[C]" Or StrSelect = "[D]" Then
                        BG.Bands.AddBand("客戶信息")
                    End If
                    GC.DataSource = dt
                End If
                BG.Bands(0).Columns.Add(BG.Columns(dt.Columns(i).ColumnName))
            End If

            If StrSelect = "[C]" Or StrSelect = "[D]" Then
                If i >= begionNum - 3 And i <= begionNum - 1 Then
                    BG.Bands(1).Columns.Add(BG.Columns(dt.Columns(i).ColumnName))
                End If
            End If

            If i >= begionNum And i <= dt.Columns.Count - 2 Then
                strYearA = Mid(dt.Columns(i).ColumnName, 1, 5)
                If strYearA <> strYearB Then
                    BG.Bands.AddBand(Mid(dt.Columns(i).ColumnName, 1, 5))
                    strYearB = Mid(dt.Columns(i).ColumnName, 1, 5)
                    Dim NumBands As Integer = BG.Bands.Count - 1
                    intA = 0
                    If StrType = "周" Then
                        BG.Bands(NumBands).Children.AddBand(GetDateCrossStr(dt.Columns(i).ColumnName))
                    Else
                        BG.Bands(NumBands).Children.AddBand(Mid(dt.Columns(i).ColumnName, 6, 3))
                    End If
                    BG.Bands(NumBands).Children(intA).Columns.Add(BG.Columns(dt.Columns(i).ColumnName))
                    BG.Columns(dt.Columns(i).ColumnName).Width = 100
                Else
                    intA = intA + 1
                    strYearB = Mid(dt.Columns(i).ColumnName, 1, 5)
                    Dim NumBands As Integer = BG.Bands.Count - 1
                    If StrType = "周" Then
                        BG.Bands(NumBands).Children.AddBand(GetDateCrossStr(dt.Columns(i).ColumnName))
                    Else
                        BG.Bands(NumBands).Children.AddBand(Mid(dt.Columns(i).ColumnName, 6, 3))
                    End If
                    BG.Bands(NumBands).Children(intA).Columns.Add(BG.Columns(dt.Columns(i).ColumnName))
                    BG.Columns(dt.Columns(i).ColumnName).Width = 100
                End If
            End If

        Next

    End Sub
#End Region

#Region "BandedGridView的樣式設置"
    Private Sub BGColumnSet(ByVal BG As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
        If BG.Columns.Count < 1 Then
            Exit Sub
        End If
        If BG.Bands.Count < 1 Then
            Exit Sub
        End If
        Dim memoEdit As New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
        memoEdit.ShowIcon = False
        '1.添加總計列
        BG.Bands.AddBand("總計")      '添加總計列()
        BG.Bands(BG.Bands.Count - 1).Children.AddBand("數量")
        BG.Bands(BG.Bands.Count - 1).Children(0).Columns.Add(BG.Columns(BG.Columns.Count - 1))
        '2.列的屬性設置
        BG.Columns("M_ID").Width = 130
        BG.Columns("M_ID").Caption = "產品編碼"
        BG.Columns("M_Name").Width = 130
        BG.Columns("M_Name").Caption = "名稱"
        If BG.Name = BandedGridView2.Name Then    '特定列的位置指定
            BG.Columns("M_Code").Width = 130
            BG.Columns("M_Code").Caption = "物料編碼"
        End If
        BG.Columns("M_Gauge").OptionsColumn.AllowEdit = True
        BG.Columns("M_Gauge").ColumnEdit = memoEdit
        BG.Columns("M_Gauge").Width = 50
        BG.Columns("M_Gauge").Caption = "規格"
        BG.Columns("M_Source").OptionsColumn.AllowEdit = True
        BG.Columns("M_Source").ColumnEdit = memoEdit
        BG.Columns("M_Source").Width = 130
        BG.Columns("M_Source").Caption = "來源碼"
        BG.Columns("M_SourceID").Visible = False
        BG.Columns("M_Unit").Caption = "單位"

        If StrSelect = "[C]" Or StrSelect = "[D]" Then
            BG.Columns("MO_CusterName").Caption = "客戶名稱"
            BG.Columns("MO_CusterID").Caption = "客戶編碼"
        End If
        '3.只讀與編輯
        For i As Integer = 0 To BG.Columns.Count - 1         '列設置不可編輯與只讀
            BG.Columns(i).OptionsColumn.ReadOnly = True
            BG.Columns(i).OptionsColumn.AllowEdit = False
        Next
        '4.凍結列
        BG.Bands(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        If BG.Bands(1).Caption = "客戶信息" Then
            BG.Bands(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        End If
        BG.Bands(BG.Bands.Count - 1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
    End Sub
#End Region

#Region "子表的動態過濾"
    Private Sub BandedGridView1_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles BandedGridView1.FocusedRowChanged
        Me.BandedGridView2.ActiveFilterString = "[M_ID] = '" & BandedGridView1.GetFocusedRowCellValue("M_ID") & "'"
    End Sub
#End Region

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        On Error Resume Next

        Dim exapp As New Excel.Application
        Dim exbook As Excel.Workbook
        Dim exsheet As Excel.Worksheet
        exapp = CreateObject("Excel.Application")
        exbook = exapp.Workbooks.Add
        exapp.Visible = True

        Dim i As Integer
        For i = 3 To DT_M.Rows.Count - 1
            exapp.Sheets.Add()
        Next

        Dim m As Integer
        For i = 0 To DT_M.Rows.Count - 1
            m = i + 1
            exsheet = exapp.Worksheets(m)
            exsheet.Columns("A:A").ColumnWidth = 11.38
            exsheet.Columns("B:B").ColumnWidth = 12.75
            exsheet.Columns("C:C").ColumnWidth = 9.5
            exsheet.Columns("D:D").ColumnWidth = 9.88
            exsheet.Columns("E:E").ColumnWidth = 9.88
            exsheet.Columns("F:F").ColumnWidth = 10.38

            exsheet.Rows("9:10").RowHeight = 33
            exsheet.Rows("13:13").RowHeight = 33
            exsheet.Rows("14:17").RowHeight = 33
            exsheet.Rows("18:19").RowHeight = 30.75
            exsheet.Rows("21:22").RowHeight = 53.25

            exsheet.Range("D6:F6").Borders(7).Weight = 2
            exsheet.Range("D6:F6").Borders(8).Weight = 2
            exsheet.Range("D6:F6").Borders(9).Weight = 2
            exsheet.Range("D6:F6").Borders(10).Weight = 2
            exsheet.Range("D6:F6").Borders(11).Weight = 2
            exsheet.Range("D6:F6").Borders(12).Weight = 2

            '1.第一個表格
            exsheet.Range("A1:B2").Borders(7).Weight = 2
            exsheet.Range("A1:B2").Borders(8).Weight = 2
            exsheet.Range("A1:B2").Borders(9).Weight = 2
            exsheet.Range("A1:B2").Borders(10).Weight = 2
            exsheet.Range("A1:B2").Borders(11).Weight = 2
            exsheet.Range("A1:B2").Borders(12).Weight = 2
            exsheet.Range("A1").Value = "項目:"
            exsheet.Range("A2").Value = "預測數更新日期："
            exsheet.Range("B1").Value = DT_M.Rows(i)("M_Name").ToString()
            exsheet.Range("B2").Value = Format(Now, "yyyy/MM/dd")
            '2.第二個表格
            exsheet.Range("D1:E3").Borders(7).Weight = 2
            exsheet.Range("D1:E3").Borders(8).Weight = 2
            exsheet.Range("D1:E3").Borders(9).Weight = 2
            exsheet.Range("D1:E3").Borders(10).Weight = 2
            exsheet.Range("D1:E3").Borders(11).Weight = 2
            exsheet.Range("D1:E3").Borders(12).Weight = 2


            exsheet.Range("D1").Value = "开料良率:"
            exsheet.Range("D2").Value = "WIP良率:"
            exsheet.Range("D3").Value = "包裝良率:"
            exsheet.Range("E1").Value = "95%"
            exsheet.Range("E2").Value = "92%"
            exsheet.Range("E3").Value = "92%"
            '3.第三個表格
            exsheet.Range("G1:H1").Borders(7).Weight = 2
            exsheet.Range("G1:H1").Borders(8).Weight = 2
            exsheet.Range("G1:H1").Borders(9).Weight = 2
            exsheet.Range("G1:H1").Borders(10).Weight = 2
            exsheet.Range("G1:H1").Borders(11).Weight = 2
            exsheet.Range("G1:H1").Borders(12).Weight = 2
            exsheet.Range("G1").Value = "整体良率:"
            exsheet.Range("H1").Value = "80%"

            '4.第四個表格
            exsheet.Range("A7:F22").Borders(7).Weight = 2
            exsheet.Range("A7:F22").Borders(8).Weight = 2
            exsheet.Range("A7:F22").Borders(9).Weight = 2
            exsheet.Range("A7:F22").Borders(10).Weight = 2
            exsheet.Range("A7:F22").Borders(11).Weight = 2
            exsheet.Range("A7:F22").Borders(12).Weight = 2

            '4.1合半單元格
            exsheet.Range("A7:A10").MergeCells = True
            exsheet.Range("A12:A13").MergeCells = True
            exsheet.Range("A14:A20").MergeCells = True
            exsheet.Range("A21:C21").MergeCells = True
            exsheet.Range("A22:C22").MergeCells = True

            exsheet.Range("B7:C8").MergeCells = True
            exsheet.Range("B9:C9").MergeCells = True
            exsheet.Range("B10:C10").MergeCells = True
            exsheet.Range("B11:C11").MergeCells = True
            exsheet.Range("B12:C12").MergeCells = True
            exsheet.Range("B13:C13").MergeCells = True
            exsheet.Range("B14:C14").MergeCells = True
            exsheet.Range("B15:C15").MergeCells = True
            exsheet.Range("B16:C16").MergeCells = True
            exsheet.Range("B17:C17").MergeCells = True
            exsheet.Range("B18:C18").MergeCells = True
            exsheet.Range("B19:C19").MergeCells = True
            exsheet.Range("B20:C20").MergeCells = True

            exsheet.Range("D7:E7").MergeCells = True
            exsheet.Range("D8:E8").MergeCells = True
            exsheet.Range("D10:E10").MergeCells = True
            exsheet.Range("D12:E12").MergeCells = True
            exsheet.Range("D13:E13").MergeCells = True
            exsheet.Range("D21:F21").MergeCells = True
            exsheet.Range("D22:F22").MergeCells = True

            exsheet.Range("F7:F10").MergeCells = True
            exsheet.Range("F12:F13").MergeCells = True
            exsheet.Range("F14:F20").MergeCells = True
            '4.2填值
            exsheet.Range("A7").Value = "半成品"
            exsheet.Range("A12").Value = "成品"
            exsheet.Range("A14").Value = "原材料 （公斤）"
            exsheet.Range("A21").Value = "现有總匯成品+半成品+原材料）"
            exsheet.Range("A22").Value = "现有總匯成品+半成品+原材料+未回材料）"

            exsheet.Range("B7").Value = "電鍍廠"
            exsheet.Range("B9").Value = "WIP總和"
            exsheet.Range("B10").Value = "包裝"
            exsheet.Range("B11").Value = "个别半成品总和"
            exsheet.Range("B12").Value = "OQC／倉庫"
            exsheet.Range("B13").Value = "VMI"
            exsheet.Range("B14").Value = "產線未開料"
            exsheet.Range("B15").Value = "庫存"
            exsheet.Range("B16").Value = "未回數"
            exsheet.Range("B17").Value = "每公斤可開（PCS"
            exsheet.Range("B18").Value = "现有料可做货（PCS)"
            exsheet.Range("B19").Value = "未回数可做料（PCS)"
            exsheet.Range("B20").Value = "总可做货数"

            exsheet.Range("C7").Value = "不良品"
            exsheet.Range("C8").Value = "良品"
            exsheet.Range("F6").Value = "獨立總和："
            '5色彩
            exsheet.Range("B2").Interior.ColorIndex = 50
            exsheet.Range("A7:C22").Interior.ColorIndex = 6
            exsheet.Range("E1:E3").Interior.ColorIndex = 44
            exsheet.Range("H1").Interior.ColorIndex = 44
            exsheet.Range("D12:E17").Interior.ColorIndex = 44
            exsheet.Range("D7:E10").Interior.ColorIndex = 44
            exsheet.Range("D21:F21").Interior.ColorIndex = 6
            exsheet.Range("D22:F22").Interior.ColorIndex = 55
            'exsheet.Cells(1)(2).Interior.ColorIndex = 55
            exbook.Sheets(m).Name = DT_M.Rows(i)("M_ID").ToString()
            '第二部門---------------------------------------------------------------------------------------------
            'For i = 0 To DT_M.Columns.Count - 1
            '    exsheet.Range(1, 2).Value = DT_M.Columns.Item(i).Caption
            'Next

        Next



    End Sub
End Class