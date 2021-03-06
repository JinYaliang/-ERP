Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.MrpManager.MrpMaterialCode
Imports LFERP.Library.MrpManager.MrpWareHouseInfo
Imports LFERP.Library.MrpManager.Bom_M
Imports LFERP.Library.MrpManager.MrpForecastOrder
Imports LFERP.Library.MrpManager.MrpSelect
Imports LFERP.Library.MrpManager.MrpSelection
Imports LFERP.Library.MrpManager.MrpInfo
Public Class frmMrpSelect
    Dim ds As New DataSet
    Dim MWHIEcon As New MrpWareHouseInfoEntryController
    Private _EditItem As String
    Property EditItem() As String '屬性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
    ''' <summary>
    ''' 載入信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMrpSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select Case EditItem '區分哪個畫面的查詢操作
            Case "MrpMaterialCode"
                Dim MMCcon As New MrpMaterialCodeController
                gueID.Properties.DisplayMember = "M_Code"
                gueID.Properties.ValueMember = "M_Code"
                GridView3.Columns(1).Visible = True
                gueID.Properties.DataSource = MMCcon.MrpMaterialCode_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "MrpWareHouseInfo"     '庫存記錄則載入單號,通過單號查詢
                Dim MWHIcon As New MrpWareHouseInfoController
                GridView3.Columns(0).Caption = "單號"
                GridView3.Columns(0).FieldName = "Ware_ID"
                '-------------------------------------------
                gueID.Properties.DisplayMember = "Ware_ID"
                gueID.Properties.ValueMember = "Ware_ID"
                gueID.Properties.DataSource = MWHIcon.MrpWareHouseInfo_GetList(Nothing, Nothing, Nothing, Nothing)
            Case "MrpForecastOrder"      '預測訂單則載入預測單號,通過預測單號查詢
                Dim MFOcon As New MrpForecastOrderController
                GridView3.Columns(0).Caption = "預測單號"
                GridView3.Columns(0).FieldName = "ForecastID"
                '-------------------------------------------
                gueID.Properties.DisplayMember = "ForecastID"
                gueID.Properties.ValueMember = "ForecastID"
                gueID.Properties.DataSource = MFOcon.MrpForecastOrder_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "MrpBOM_M"                 '----
                Dim BMcon As New Bom_MController
                GridView3.Columns(0).Caption = "組件編號"
                GridView3.Columns(0).FieldName = "ParentGroup"
                '-------------------------------------------
                gueID.Properties.DisplayMember = "ParentGroup"
                gueID.Properties.ValueMember = "ParentGroup"
                GridView3.Columns(1).Visible = True
                gueID.Properties.DataSource = BMcon.Bom_M_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "MrpInfo"                  '----
                Dim MIcon As New MrpInfoController
                GridView3.Columns(0).Caption = "運算編號"
                GridView3.Columns(0).FieldName = "MRP_ID"
                '-------------------------------------------
                gueID.Properties.DisplayMember = "MRP_ID"
                gueID.Properties.ValueMember = "MRP_ID"
                gueID.Properties.DataSource = MIcon.MrpInfo_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
            Case Else
        End Select
        CreateTable()
        LoadTable()
    End Sub
    ''' <summary>
    ''' 創建類別表--把每個模塊查詢需要用到的字段載入
    ''' 創建條件篩選表--用戶選擇的條件展示,並以此拼接查詢條件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("SelectTb")
            .Columns.Add("AutoID", GetType(Integer))
            .Columns.Add("Category", GetType(String))
            .Columns.Add("Type", GetType(String))
            .Columns.Add("FieldName", GetType(String))
        End With
        GridControl1.DataSource = ds.Tables("SelectTb")
        With ds.Tables.Add("SelectTbDel")
            .Columns.Add("DelConditon", GetType(String))
            .Columns.Add("DelDisplay", GetType(String))
        End With
        GridControl2.DataSource = ds.Tables("SelectTbDel")
    End Sub
    ''' <summary>
    ''' 往表中載入信息
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTable()
        Dim Row As DataRow
        On Error Resume Next
        Dim perCont As New MrpSelection_Controller
        Dim perList As New List(Of MrpSelectionInfo)
        perList = perCont.MrpSelection_GetList(EditItem, Nothing, Nothing, Nothing, Nothing)
        If perList.Count > 0 Then
            For i As Integer = 0 To perList.Count - 1
                Row = ds.Tables("SelectTb").NewRow
                Row("AutoID") = i
                Row("Category") = perList(i).Category
                Row("Type") = perList(i).Type
                Row("FieldName") = perList(i).FieldName
                ds.Tables("SelectTb").Rows.Add(Row)
            Next i
        End If
    End Sub
    ''' <summary>
    ''' 通過類型不同自動條件選擇
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_FocusedRowChanged_1(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        On Error Resume Next
        If GridView1.GetFocusedRowCellValue("Type") = "日期" Then
            XtraTabControl2.SelectedTabPageIndex = 1
            XtraTabControl2.TabPages(0).PageEnabled = False
            XtraTabControl2.TabPages(1).PageEnabled = True
            XtraTabControl2.TabPages(2).PageEnabled = False
            XtraTabControl2.TabPages(3).PageEnabled = False
        ElseIf GridView1.GetFocusedRowCellValue("Type") = "數字" Then
            XtraTabControl2.SelectedTabPageIndex = 2
            XtraTabControl2.TabPages(0).PageEnabled = False
            XtraTabControl2.TabPages(1).PageEnabled = False
            XtraTabControl2.TabPages(2).PageEnabled = True
            XtraTabControl2.TabPages(3).PageEnabled = False
        ElseIf GridView1.GetFocusedRowCellValue("Type") = "文字" Then
            XtraTabControl2.SelectedTabPageIndex = 0
            XtraTabControl2.TabPages(0).PageEnabled = True
            XtraTabControl2.TabPages(1).PageEnabled = False
            XtraTabControl2.TabPages(2).PageEnabled = False
            XtraTabControl2.TabPages(3).PageEnabled = False
        Else
            XtraTabControl2.SelectedTabPageIndex = 3
            XtraTabControl2.TabPages(0).PageEnabled = False
            XtraTabControl2.TabPages(1).PageEnabled = False
            XtraTabControl2.TabPages(2).PageEnabled = False
            XtraTabControl2.TabPages(3).PageEnabled = True
        End If
    End Sub
    ''' <summary>
    ''' 刪除條件表篩選條件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView2.RowCount = 0 Then Exit Sub
        Dim i As Integer = ds.Tables("SelectTbDel").Rows.Count - 1
        ds.Tables("SelectTbDel").Rows.RemoveAt(i)
    End Sub
    ''' <summary>
    ''' 查詢按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
        If XtraTabControl1.SelectedTabPageIndex = 0 Then   '固定樣式查詢
            If gueID.Text <> String.Empty Then
                tempValue = "固定樣式"
                tempValue2 = gueID.Text   '查詢條件--選擇的單號\預測單號....
            Else
                MsgBox("未做任何選擇，請選擇！", MsgBoxStyle.Information, "提示")
                Return
            End If
        Else   '自定義樣式查詢
            If ds.Tables("SelectTbDel").Rows.Count = 0 Then
                MsgBox("選擇篩選條件！")
                Return
            End If
            tempValue = "自定義樣式"
            tempValue2 = ""
            For i As Integer = 0 To ds.Tables("SelectTbDel").Rows.Count - 1
                tempValue2 = tempValue2 & ds.Tables("SelectTbDel").Rows(i)("DelConditon").ToString   '查詢條件--拼接篩選表中的條件
            Next
        End If
        Me.Close()
    End Sub
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdCancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancle.Click
        tempValue = String.Empty
        Me.Close()
    End Sub
    ''' <summary>
    ''' 添加文字類型條件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdAdd_Char_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd_Char.Click
        If TextBox1.Text = String.Empty Then
            MsgBox("請輸入要查詢的條件！")
            Return
        Else
            Dim Row As DataRow
            Row = ds.Tables("SelectTbDel").NewRow
            Row("DelConditon") = " " & Mid(cboCondition.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " like " + "'%" + TextBox1.Text + "%'" + " "
            If ds.Tables("SelectTbDel").Rows.Count = 0 Then
                Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " Like " + "'" + TextBox1.Text + "'"
            Else
                Row("DelDisplay") = Mid(Trim(cboCondition.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " Like " + "'" + TextBox1.Text + "'"
            End If
            ds.Tables("SelectTbDel").Rows.Add(Row)
        End If
    End Sub
    ''' <summary>
    ''' 添加日期類型條件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdAdd_Date_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd_Date.Click
        Dim Row As DataRow
        Row = ds.Tables("SelectTbDel").NewRow
        Row("DelConditon") = " " & Mid(cboCondition.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + "  " + Mid(Trim(ComboBoxEdit2.Text), 1, 3) + " " + "'" + DateEdit1.Text + "'" + " "
        If ds.Tables("SelectTbDel").Rows.Count = 0 Then
            Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ComboBoxEdit2.Text), 3) + " " + "'" + DateEdit1.Text + "'"
        Else
            Row("DelDisplay") = Mid(Trim(cboCondition.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ComboBoxEdit2.Text), 3) + " " + "'" + DateEdit1.Text + "'"
        End If
        ds.Tables("SelectTbDel").Rows.Add(Row)
    End Sub
    ''' <summary>
    ''' 添加數字類型條件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdAdd_Number_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd_Number.Click
        Dim Row As DataRow
        Row = ds.Tables("SelectTbDel").NewRow
        Row("DelConditon") = " " & Mid(cboCondition.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + "  " + Mid(Trim(cboLogic.Text), 1, 3) + " " + "'" + txtQty.Text.Trim + "'" + " "
        If ds.Tables("SelectTbDel").Rows.Count = 0 Then
            Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(cboLogic.Text), 3) + " " + "'" + txtQty.Text.Trim + "'"
        Else
            Row("DelDisplay") = Mid(Trim(cboCondition.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(cboLogic.Text), 3) + " " + "'" + txtQty.Text.Trim + "'"
        End If
        ds.Tables("SelectTbDel").Rows.Add(Row)
    End Sub
    ''' <summary>
    ''' 添加其他類型條件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdAdd_Other_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd_Other.Click
        Dim Row As DataRow
        Row = ds.Tables("SelectTbDel").NewRow
        If ComboBoxEdit1.Text = "是" Then
            Row("DelConditon") = " " & Mid(cboCondition.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " = " + "'True'"
            If ds.Tables("SelectTbDel").Rows.Count = 0 Then
                Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " 等於 " + "'" + ComboBoxEdit1.Text + "'"
            Else
                Row("DelDisplay") = Mid(Trim(cboCondition.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " 等於 " + "'" + ComboBoxEdit1.Text + "'"
            End If
            ds.Tables("SelectTbDel").Rows.Add(Row)
        Else
            Row("DelConditon") = " " & Mid(cboCondition.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " != " + "'True'"
            If ds.Tables("SelectTbDel").Rows.Count = 0 Then
                Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " 等於 " + "'" + ComboBoxEdit1.Text + "'"
            Else
                Row("DelDisplay") = Mid(Trim(cboCondition.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " 等於 " + "'" + ComboBoxEdit1.Text + "'"
            End If
            ds.Tables("SelectTbDel").Rows.Add(Row)
        End If
    End Sub
End Class