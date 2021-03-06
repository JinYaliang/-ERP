Imports LFERP.Library.WareHouse
Imports LFERP.Library.WareHouse.WareOut
Imports LFERP.Library.Material
Imports LFERP.SystemManager.SystemUser
Imports LFERP.DataSetting
Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop

Imports LFERP.Library.WareHouseDetail

Public Class frmWareHouseDetailMothPrice

    Dim strTypeID As String

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ButtonExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExport.Click
        If txtWH.EditValue = "" Then
            MsgBox("請選擇倉庫！")
            txtWH.Select()
            Exit Sub
        End If


        If strTypeID = "500408" Then ''倉庫存貨明細
            WareInventory_GetListMothPriceExport()
        End If


        If strTypeID = "500409" Then ''倉庫出入庫明細
            WareHouseDetail_GetList3()
        End If

    End Sub

    Sub WareInventory_GetListMothPriceExport()

        Dim strWH_IDA As String = ""
        If Me.txtWH.Tag <> "" Then
            If InStr(txtWH.Tag, ",", CompareMethod.Text) > 0 Then
                strWH_IDA = "('" & Replace(txtWH.Tag, ",", "','") & "')"
            Else
                strWH_IDA = "('" & txtWH.Tag & "')"
            End If
        End If

        '----------------------------------------------------------------------
        Dim strType As String = ""
        Dim strWareDate As String = Nothing
        If Format(Now, "yyyyMM") = Format(CDate(Me.YearDate.EditValue), "yyyyMM") Then
            strType = "N"
            strWareDate = Nothing
        Else
            strType = "M"
            strWareDate = Format(Now, "yyyy-MM") & "-01"
        End If
        '----------------------------------------------------------------------

        Dim CountJS As Integer
        Dim WC As New WareInventory.WareInventoryMTController
        Dim WL As New List(Of WareInventory.WareInventoryInfo)

        WL = WC.WareInventory_GetListMothPrice(strWH_IDA, strWareDate, strType)

        CountJS = WL.Count

        If WL.Count <= 0 Then
            MsgBox("無月匯總數據,請檢查!")
            Exit Sub
        End If

        Dim i As Integer
        Dim exapp As New Excel.Application   '定義一個excel對象
        Dim exbook As Excel.Workbook     '定義一個excel活頁
        Dim exsheet As Excel.Worksheet   '定義一個excel工作區

        exapp = CreateObject("Excel.Application")   '生成一個excel對象
        exbook = exapp.Workbooks.Open(Application.StartupPath & "\ModuleFile\moduleMonth.xls")
        exsheet = exbook.Worksheets(1)


        ProgressBar1.Visible = True
        ProgressBar1.Maximum = CountJS

        exbook.ActiveSheet.Range(exsheet.Cells(1, 1), exsheet.Cells(1, 11)).Merge(Type.Missing)

        exsheet.Cells(1, 1) = Format(CDate(YearDate.EditValue), "yyyy年MM月") & "存貨價值明細"

        exsheet.Cells(2 + i, 1) = "倉庫"
        exsheet.Cells(2 + i, 2) = "大類"
        exsheet.Cells(2 + i, 3) = "中類"
        exsheet.Cells(2 + i, 4) = "小類"
        exsheet.Cells(2 + i, 5) = "物料編碼"
        exsheet.Cells(2 + i, 6) = "物料名稱"
        exsheet.Cells(2 + i, 7) = "規格"
        exsheet.Cells(2 + i, 8) = "數量"
        exsheet.Cells(2 + i, 9) = "單位"
        exsheet.Cells(2 + i, 10) = "價格"
        exsheet.Cells(2 + i, 11) = "幣別"

        For i = 0 To CountJS - 1
            exsheet.Cells(3 + i, 1) = WL(i).WH_SName & "-" & WL(i).WH_Name
            exsheet.Cells(3 + i, 2) = WL(i).Type1Name
            exsheet.Cells(3 + i, 3) = WL(i).Type2Name
            exsheet.Cells(3 + i, 4) = WL(i).Type3Name
            exsheet.Cells(3 + i, 5) = WL(i).M_Code

            exsheet.Cells(3 + i, 6) = WL(i).M_Name
            exsheet.Cells(3 + i, 7) = WL(i).M_Gauge
            exsheet.Cells(3 + i, 8) = WL(i).WI_Qty
            exsheet.Cells(3 + i, 9) = WL(i).M_Unit

            exsheet.Cells(3 + i, 10) = WL(i).M_Price
            exsheet.Cells(3 + i, 11) = WL(i).M_Currency

            ProgressBar1.Value = i
        Next

        exbook.ActiveSheet.range(exsheet.Cells(2, 1), exsheet.Cells(2 + CountJS, 11)).borders(1).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(2, 1), exsheet.Cells(2 + CountJS, 11)).borders(2).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(2, 1), exsheet.Cells(2 + CountJS, 11)).borders(3).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(2, 1), exsheet.Cells(2 + CountJS, 11)).borders(4).linestyle = 1


        ProgressBar1.Visible = False

        Dim tempName As String

        SaveFileDialog1.InitialDirectory = "c:\"
        SaveFileDialog1.Filter = "txt files (*.xls)|*.xls"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tempName = SaveFileDialog1.FileName
            exsheet.SaveAs(tempName)
        Else
            Exit Sub
        End If

        exapp.Quit()
        exsheet = Nothing
        exbook = Nothing
        exapp = Nothing

        MsgBox("導出成功!")
    End Sub


    Sub WareHouseDetail_GetList3()
        Dim strStat_Date, strEnd_Date As String
        ''-----------------------------------------------------------------------------|
        Dim intInputMonth As Integer '这是你输入的月份                                '|
        intInputMonth = Val(Format(CDate(YearDate.EditValue), "MM"))                 '| 

        Dim dt As New DateTime(DateTime.Today.Year, intInputMonth, 1)                 '|
        '计算该月份的天数
        Dim days As Integer = dt.AddMonths(1).DayOfYear - dt.DayOfYear                '|
        strStat_Date = (dt.AddDays(0).ToString("yyyy/MM/dd"))                         '|
        strEnd_Date = (dt.AddDays(days - 1).ToString("yyyy/MM/dd"))                   '|
        ''-----------------------------------------------------------------------------|
        Dim strWH_IDA As String = ""
        If Me.txtWH.Tag <> "" Then
            If InStr(txtWH.Tag, ",", CompareMethod.Text) > 0 Then
                strWH_IDA = "('" & Replace(txtWH.Tag, ",", "','") & "')"
            Else
                strWH_IDA = "('" & txtWH.Tag & "')"
            End If
        End If
        '----------------------------------------------------------------------
        Dim CountJS As Integer
        Dim WC As New WareHouseDetailControl
        Dim WL As New List(Of WareHouseDetailInfo)

        WL = WC.WareHouseDetail_GetList3(strWH_IDA, Nothing, strStat_Date, strEnd_Date)

        CountJS = WL.Count

        If WL.Count <= 0 Then
            MsgBox("無月匯總數據,請檢查!")
            Exit Sub
        End If

        Dim i As Integer
        Dim exapp As New Excel.Application   '定義一個excel對象
        Dim exbook As Excel.Workbook     '定義一個excel活頁
        Dim exsheet As Excel.Worksheet   '定義一個excel工作區

        exapp = CreateObject("Excel.Application")   '生成一個excel對象
        exbook = exapp.Workbooks.Open(Application.StartupPath & "\ModuleFile\moduleMonth.xls")
        exsheet = exbook.Worksheets(1)


        ProgressBar1.Visible = True
        ProgressBar1.Maximum = CountJS

        exbook.ActiveSheet.Range(exsheet.Cells(1, 1), exsheet.Cells(1, 12)).Merge(Type.Missing)

        exsheet.Cells(2 + i, 1) = "倉庫"
        exsheet.Cells(2 + i, 2) = "大類"
        exsheet.Cells(2 + i, 3) = "中類"
        exsheet.Cells(2 + i, 4) = "小類"
        exsheet.Cells(2 + i, 5) = "物料編碼"

        exsheet.Cells(2 + i, 6) = "物料名稱"
        exsheet.Cells(2 + i, 7) = "規格"
        exsheet.Cells(2 + i, 8) = "單號"
        exsheet.Cells(2 + i, 9) = "數量"
        exsheet.Cells(2 + i, 10) = "操作時間"

        exsheet.Cells(2 + i, 11) = "出/入庫"
        exsheet.Cells(2 + i, 12) = "類型"

        For i = 0 To CountJS - 1
            exsheet.Cells(3 + i, 1) = WL(i).WH_Name 'WL(i).WH_UpName '& "-" &   'WL(i).WH_SName & "-" & WL(i).WH_Name
            exsheet.Cells(3 + i, 2) = WL(i).Type1Name
            exsheet.Cells(3 + i, 3) = WL(i).Type2Name
            exsheet.Cells(3 + i, 4) = WL(i).Type3Name
            exsheet.Cells(3 + i, 5) = WL(i).M_Code

            exsheet.Cells(3 + i, 6) = WL(i).M_Name
            exsheet.Cells(3 + i, 7) = WL(i).M_Gauge
            exsheet.Cells(3 + i, 8) = WL(i).ID
            exsheet.Cells(3 + i, 9) = WL(i).Qty

            exsheet.Cells(3 + i, 10) = Format(CDate(WL(i).strDate), "yyyy/MM/dd HH:mm:ss")

            exsheet.Cells(3 + i, 11) = WL(i).WType
            exsheet.Cells(3 + i, 12) = WL(i).SType

            ProgressBar1.Value = i
        Next

        exbook.ActiveSheet.range(exsheet.Cells(2, 1), exsheet.Cells(2 + CountJS, 12)).borders(1).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(2, 1), exsheet.Cells(2 + CountJS, 12)).borders(2).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(2, 1), exsheet.Cells(2 + CountJS, 12)).borders(3).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(2, 1), exsheet.Cells(2 + CountJS, 12)).borders(4).linestyle = 1


        ProgressBar1.Visible = False

        Dim tempName As String

        SaveFileDialog1.InitialDirectory = "c:\"
        SaveFileDialog1.Filter = "txt files (*.xls)|*.xls"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tempName = SaveFileDialog1.FileName
            exsheet.SaveAs(tempName)
        Else
            Exit Sub
        End If

        exapp.Quit()
        exsheet = Nothing
        exbook = Nothing
        exapp = Nothing

        MsgBox("導出成功!")
    End Sub




    Private Sub txtWH_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWH.ButtonClick
        frmWareHouseSelect.SelectWareID = ""
        tempValue3 = "500401"  ''導入此權限,允許的倉庫
        tempValue2 = strTypeID

        frmWareHouseSelect.SelectWareID = ""
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = Me.Left + txtWH.Left + 2
        frmWareHouseSelect.Top = Me.Top + txtWH.Top + txtWH.Height + 21
        frmWareHouseSelect.ShowDialog()

        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            txtWH.Text = frmWareHouseSelect.SelectWareName
            txtWH.Tag = frmWareHouseSelect.SelectWareID
        End If

        frmWareHouseSelect.Dispose()
    End Sub

    Private Sub frmWareHouseDetailMothPrice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        strTypeID = tempValue
        tempValue = Nothing

        YearDate.EditValue = Format(Now, "yyyy年MM月")


        If strTypeID = "500408" Then ''倉庫存貨明細
            Me.Text = "倉庫存貨價值明細"
            Label2.Text = "倉庫存貨價值明細"
        End If


        If strTypeID = "500409" Then ''倉庫出入庫明細
            Me.Text = "月份出入庫明細"
            Label2.Text = "月份出入庫明細"
        End If

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        'Dim strWH_IDA As String = ""
        'If Me.txtWH.Tag <> "" Then
        '    If InStr(txtWH.Tag, ",", CompareMethod.Text) > 0 Then
        '        strWH_IDA = "('" & Replace(txtWH.Tag, ",", "','") & "')"
        '    Else
        '        strWH_IDA = "('" & txtWH.Tag & "')"
        '    End If
        'End If

        'MsgBox(strWH_IDA)
    End Sub


End Class