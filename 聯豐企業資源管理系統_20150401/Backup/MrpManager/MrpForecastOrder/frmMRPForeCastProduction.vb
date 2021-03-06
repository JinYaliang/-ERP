Imports LFERP.Library.MrpManager.MrpForecastOrder
Imports LFERP.Library.MrpManager.MrpSetting
Imports LFERP.Library.MrpManager.Bom_M
Imports LFERP.Library.MrpManager.MrpMaterialCode
Public Class frmMRPForeCastProduction
    Dim bomCon As New Bom_MController
    Dim mscon As New MrpSettingController
    Dim mfoCon As New MrpForecastOrderController
    Dim mmcon As New MrpMaterialCodeController
    Dim _StrSelect As String
    Private Property StrSelect() As String
        Get
            Return _StrSelect
        End Get
        Set(ByVal value As String)
            _StrSelect = value
        End Set
    End Property
    Private Sub frmMRPForeCastProduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '1.產品控件----------------------------------------------------------------
        GLU_MCode.Properties.DisplayMember = "M_Name"
        GLU_MCode.Properties.ValueMember = "ParentGroup"
        Dim BoInfo As New Bom_MInfo
        Dim MrpInfoList2 As New List(Of Bom_MInfo)
        BoInfo.ParentGroup = "*全部*"
        BoInfo.M_Name = "*全部*"
        BoInfo.M_Gauge = "*全部*"
        BoInfo.M_Unit = "*全部*"
        '   BoInfo.M_Source = "*全部*"
        'MrpInfoList2 = bomCon.Bom_M_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        MrpInfoList2.Insert(0, BoInfo)
        GLU_MCode.Properties.DataSource = MrpInfoList2         '物料信息彈窗賦值

        '2.載入當前查詢的數據------------------------------------------------------
        Dim MrpSet As New MrpSettingInfo
        Dim msList As New List(Of MrpSettingInfo)
        msList = mscon.MrpSetting_GetList(InUserID)
        If msList.Count > 0 Then   '判斷數據來源是否為空
            MrpSet = msList(0)
        Else
            MrpSet.ProductionBeginDate = Year(Now) & "/01/01"
            MrpSet.ProductionEndDate = Year(Now) & "/12/31"
        End If
        If MrpSet.ProductionBeginDate = Nothing Then
            MrpSet.ProductionBeginDate = Year(Now) & "/01/01"
        End If
        If MrpSet.ProductionEndDate = Nothing Then
            MrpSet.ProductionEndDate = Year(Now) & "/12/31"
        End If
        det_StartDate.DateTime = MrpSet.ProductionBeginDate       '加載數據和個人的默認設置
        det_EndDate.DateTime = MrpSet.ProductionEndDate

        LoadSub(Mid(txtSelect.EditValue, 1, 3), MrpSet.ProductionBeginDate, MrpSet.ProductionEndDate, Nothing)
    End Sub

    Private Sub LoadSub(ByVal StrSelect As String, ByVal date1 As Date, ByVal date2 As Date, ByVal M_Code As String)
        Try
            Dim DT_M As New DataTable
            Dim DT_D As New DataTable
            BandedGridView1.Bands.Clear()      '清空BandedGridView1
            BandedGridView1.Columns.Clear()
         
            Select Case StrSelect
                Case "[A]" ':周數"
                    DT_M = mfoCon.GetWeekAllProductionInfo(date1, date2, M_Code)
                    If DT_M.Rows.Count > 0 Then
                        BGridViewDeal(DT_M, 6, BandedGridView1, GridControl1, "周")
                    End If
                Case "[B]" ':月份"
                    DT_M = mfoCon.GetMonthAllProductionInfo(date1, date2, M_Code)
                    If DT_M.Rows.Count > 0 Then
                        BGridViewDeal(DT_M, 6, BandedGridView1, GridControl1, "月")
                    End If
            End Select
        Catch ex As Exception
        End Try
        BGColumnSet(BandedGridView1)
    End Sub

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
                    GC.DataSource = dt
                End If
                BG.Bands(0).Columns.Add(BG.Columns(dt.Columns(i).ColumnName))
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
        '3.只讀與編輯
        For i As Integer = 0 To BG.Columns.Count - 1         '列設置不可編輯與只讀
            BG.Columns(i).OptionsColumn.ReadOnly = True
            BG.Columns(i).OptionsColumn.AllowEdit = False
        Next
        '4.凍結列
        BG.Bands(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        BG.Bands(BG.Bands.Count - 1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
    End Sub
#End Region
    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
       
        '1.產品編碼數擾
        Dim StrMCode As String
        If GLU_MCode.EditValue = "*全部*" Or GLU_MCode.EditValue = String.Empty Then '物料編號條件判斷
            StrMCode = Nothing
        Else
            StrMCode = GLU_MCode.EditValue
        End If
        '2.起止日期
        Dim StratDate As Date = det_StartDate.DateTime '起止時間
        Dim EndDate As Date = det_EndDate.DateTime
        '3.查詢條件
        StrSelect = Mid(txtSelect.EditValue, 1, 3) '查詢條件
        '4.執行查詢
        LoadSub(StrSelect, StratDate, EndDate, StrMCode)    '進行查詢
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If BandedGridView1.RowCount <= 0 Then
            Exit Sub
        End If
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim filePath As String
            filePath = FolderBrowserDialog1.SelectedPath
            filePath += "\YourExcel1.xls"
            BandedGridView1.ExportToXls(filePath)
            Process.Start(filePath)
        End If
    End Sub
End Class