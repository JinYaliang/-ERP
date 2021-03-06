Imports LFERP.Library.WareHouse
Imports LFERP.Library.WareHouse.WareOut
Imports LFERP.Library.Material
Imports LFERP.SystemManager.SystemUser
Imports LFERP.DataSetting
Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop



Public Class WareInventoryMothCollSelect
    Dim rptType As String
    Dim ds As New DataSet

    'Sub Createtable()
    '    With ds.Tables.Add("Type3IDName")
    '        .Columns.Add("Type3ID", GetType(String))
    '        .Columns.Add("Type3Name", GetType(String))
    '    End With

    '    Type3IDGridLookUp.Properties.ValueMember = "Type3ID"
    '    Type3IDGridLookUp.Properties.DisplayMember = "Type3Name"
    '    Type3IDGridLookUp.Properties.DataSource = ds.Tables("Type3IDName")


    '    Dim mc As New MaterialTypeController
    '    Dim ml As New List(Of MaterialTypeInfo3)

    '    ml = mc.MaterialType3_GetList(Nothing, Nothing, Nothing)

    '    Dim i As Integer
    '    If ml.Count > 0 Then
    '    Else
    '        Exit Sub
    '    End If

    '    Dim row As DataRow
    '    row = ds.Tables("Type3IDName").NewRow
    '    row("Type3ID") = "*"
    '    row("Type3Name") = "全部"
    '    ds.Tables("Type3IDName").Rows.Add(row)

    '    For i = 0 To ml.Count - 1
    '        Dim row1 As DataRow
    '        row1 = ds.Tables("Type3IDName").NewRow
    '        row1("Type3ID") = ml(i).Type3ID
    '        row1("Type3Name") = ml(i).Type3Name
    '        ds.Tables(0).Rows.Add(row1)
    '    Next

    '    Type3IDGridLookUp.EditValue = "*"

    'End Sub

    Private Sub WareInventoryMothCollSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtWH.Text = Nothing
        txtWH.Tag = Nothing

        txtWH.Text = tempValue5
        txtWH.Tag = tempValue4

        'txtWH.Enabled = False
        tempValue4 = Nothing
        tempValue5 = Nothing

        YearDate.EditValue = Format(Now, "yyyy年MM月")

        'Createtable()
        Dim mt As New WareInventory.WareInventoryMTController

        mt.LoadNodes(Tv1, txtWH.Tag)


    End Sub




    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If Me.txtWH.EditValue Is Nothing Then
            MsgBox("請選擇倉庫！")
            Exit Sub
        End If

        ''Dim strStat_Date, strEnd_Date As String
        '' ''-----------------------------------------------------------------------------|
        ''Dim intInputMonth As Integer '这是你输入的月份                                '|
        ''intInputMonth = Val(Format(CDate(YearDate.EditValue), "MM"))                 '| 

        ''Dim dt As New DateTime(DateTime.Today.Year, intInputMonth, 1)                 '|
        ' ''计算该月份的天数
        ''Dim days As Integer = dt.AddMonths(1).DayOfYear - dt.DayOfYear                '|
        ''strStat_Date = (dt.AddDays(0).ToString("yyyy/MM/dd"))                         '|
        ''strEnd_Date = (dt.AddDays(days - 1).ToString("yyyy/MM/dd"))                   '|
        '' ''-----------------------------------------------------------------------------|

        ''Dim Nextdate As String
        ''If Format(CDate(YearDate.EditValue), "yyyy-MM") = Format(Now, "yyyy-MM") Then
        ''    Nextdate = Format(Now, "yyyy-MM")
        ''Else
        ''    Nextdate = DateAdd(DateInterval.Month, 1, CDate(YearDate.EditValue))
        ''End If

        Dim strStat_Date, strEnd_Date, Nextdate As String
        ''-----------------------------------------------------------------------------|
        Dim intInputMonth, intInputYear As Integer '这是你输入的月份                                '|
        intInputMonth = Val(Format(CDate(YearDate.EditValue), "MM"))                 '| 
        intInputYear = Val(Format(CDate(YearDate.EditValue), "yyyy"))

        Dim dt As New DateTime(intInputYear, intInputMonth, 1)                 '|
        '计算该月份的天数
        Dim days As Integer = dt.AddMonths(1).DayOfYear - dt.DayOfYear

        If days <= 0 Or days > 31 Then
            days = 31
        End If
        '|
        strStat_Date = (dt.AddDays(0).ToString("yyyy/MM/dd")) & " 00:00:00"                        '|
        'strEnd_Date = (dt.AddDays(days - 1).ToString("yyyy/MM/dd"))                   '|
        ''-----------------------------------------------------------------------------|

        ' Dim Nextdate As String
        If Format(CDate(YearDate.EditValue), "yyyy/MM") = Format(Now, "yyyy/MM") Then
            Nextdate = Format(Now, "yyyy/MM")
            strEnd_Date = Format(Now, "yyyy/MM/dd") & " 23:59:00"  '
        Else
            Nextdate = Format(DateAdd(DateInterval.Month, 1, CDate(YearDate.EditValue)), "yyyy/MM")
            strEnd_Date = (dt.AddDays(days - 1).ToString("yyyy/MM/dd")) & " 23:59:00"  '
        End If

        Dim strType1ID As String
        Dim strType2ID As String
        Dim strType3ID As String

        strType1ID = Nothing
        strType2ID = Nothing
        strType3ID = Nothing

        If PopupContainerEdit1.Tag = "" Then

        ElseIf Len(PopupContainerEdit1.Tag) = 5 Then
            strType1ID = PopupContainerEdit1.Tag
        ElseIf Len(PopupContainerEdit1.Tag) = 8 Then
            strType2ID = PopupContainerEdit1.Tag
        ElseIf Len(PopupContainerEdit1.Tag) = 11 Then
            strType3ID = PopupContainerEdit1.Tag
        End If



        Dim ds As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet

        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim strCompany As String
        strCompany = Mid(strInDPT_ID, 1, 4)   '獲得登錄者所屬公司ID,以返回公司名稱，LOGO

        Dim WC As New WareInventory.WareInventoryMTController
        If WC.WareInventoryMothColl(txtWH.Tag, strType1ID, strType2ID, strType3ID, strStat_Date, strEnd_Date, Nextdate).Count <= 0 Then
            MsgBox("無數據,請檢查!")
            Exit Sub
        End If

        ds.Tables.Clear()

        ltc1.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(ds, "WareInventory", WC.WareInventoryMothColl(txtWH.Tag, strType1ID, strType2ID, strType3ID, strStat_Date, strEnd_Date, Nextdate))
        PreviewRPT1(ds, "rptWareInventoryMothColl", "庫存月報表", strStat_Date & "至" & strEnd_Date, Format(CDate(YearDate.EditValue), "yyyy年MM月"), True, True)

        ltc = Nothing
        ltc2 = Nothing

        Me.Close()


    End Sub

    Private Sub txtWH_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWH.ButtonClick
        'frmWareHouseSelect.blnSelectParentNode = True
        'frmWareHouseSelect.SelectWareID = ""
        'frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        'frmWareHouseSelect.Left = MDIMain.tvModule.Width + 8 + Me.txtWH.Left + Me.Left
        'frmWareHouseSelect.Top = Me.txtWH.Top + Me.txtWH.Height + Me.Top + 92
        'tempValue3 = "500401" '不為
        'frmWareHouseSelect.ShowDialog()
        'If frmWareHouseSelect.SelectWareID = "" Then
        'Else
        '    txtWH.Text = frmWareHouseSelect.SelectWareName
        '    txtWH.EditValue = frmWareHouseSelect.SelectWareID
        '    '      txtWH.Enabled = False
        'End If

        frmWareHouseSelect.SelectWareID = ""
        tempValue3 = "500401"
        tempValue2 = "500401"
        frmWareHouseSelect.SelectWareID = ""
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = Me.Left + txtWH.Left + 2
        frmWareHouseSelect.Top = Me.Top + txtWH.Top + txtWH.Height + 21
        frmWareHouseSelect.ShowDialog()

        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            'txtWH.Text = frmWareHouseSelect.SelectWareID
            'If txtWH.Text = "" Then
            txtWH.Text = frmWareHouseSelect.SelectWareName
            txtWH.Tag = frmWareHouseSelect.SelectWareID
            'txtWH.Enabled = False
            '    Else
            '    txtWH.Text = txtWH.Text + "," + frmWareHouseSelect.SelectWareName
            '    txtWH.Tag = txtWH.Tag + "," + frmWareHouseSelect.SelectWareID
            'End If
        End If

        frmWareHouseSelect.Dispose()

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        ExpotExcel()
    End Sub

    Sub ExpotExcel()
        If Me.txtWH.EditValue Is Nothing Then
            MsgBox("請選擇倉庫！")
            Exit Sub
        End If

        Dim strStat_Date, strEnd_Date, Nextdate As String
        ''-----------------------------------------------------------------------------|
        Dim intInputMonth As Integer '这是你输入的月份                                '|
        Dim intInputYear As Integer

        intInputMonth = Val(Format(CDate(YearDate.EditValue), "MM"))                 '| 
        intInputYear = Val(Format(CDate(YearDate.EditValue), "yyyy"))

        Dim dt As New DateTime(intInputYear, intInputMonth, 1)                 '|
        '计算该月份的天数
        Dim days As Integer = dt.AddMonths(1).DayOfYear - dt.DayOfYear

        If days <= 0 Or days > 31 Then
            days = 31
        End If
        '|
        strStat_Date = (dt.AddDays(0).ToString("yyyy/MM/dd")) & " 00:00:00"                        '|
        'strEnd_Date = (dt.AddDays(days - 1).ToString("yyyy/MM/dd"))                   '|
        ''-----------------------------------------------------------------------------|

        ' Dim Nextdate As String
        If Format(CDate(YearDate.EditValue), "yyyy/MM") = Format(Now, "yyyy/MM") Then
            Nextdate = Format(Now, "yyyy/MM")
            strEnd_Date = Format(Now, "yyyy/MM/dd") & " 23:59:00"  '
        Else
            Nextdate = Format(DateAdd(DateInterval.Month, 1, CDate(YearDate.EditValue)), "yyyy/MM")
            strEnd_Date = (dt.AddDays(days - 1).ToString("yyyy/MM/dd")) & " 23:59:00"  '
        End If


        ''If Val(Format(CDate(YearDate.EditValue), "MM")) = 1 Then
        ''    strStat_Date = Val(Format(CDate(YearDate.EditValue), "yyyy")) - 1 & "/12/1"
        ''    strEnd_Date = Val(Format(CDate(YearDate.EditValue), "yyyy")) - 1 & "/12/31"
        ''    Nextdate = Format(CDate(YearDate.EditValue), "yyyy-MM")

        ''Else
        ''    '-----------------------------------------------------------------------------|
        ''    ''-----------------------------------------------------------------------------|
        ''    Dim intInputMonth As Integer '                              '|
        ''    intInputMonth = Val(Format(CDate(YearDate.EditValue), "MM"))                 '| 

        ''    Dim dt As New DateTime(DateTime.Today.Year, intInputMonth, 1)                 '|
        ''    '
        ''    Dim days As Integer = dt.AddMonths(1).DayOfYear - dt.DayOfYear

        ''    If intInputMonth = 12 Then
        ''        strStat_Date = (dt.AddDays(0).ToString("yyyy/MM/dd"))                         '|
        ''        strEnd_Date = Val(Format(CDate(YearDate.EditValue), "yyyy")) & "/12/31"
        ''        Nextdate = Val(Format(CDate(YearDate.EditValue), "yyyy")) + 1 & "/1"
        ''    Else

        ''        strStat_Date = (dt.AddDays(0).ToString("yyyy/MM/dd"))                         '|
        ''        strEnd_Date = (dt.AddDays(days - 1).ToString("yyyy/MM/dd"))
        ''        Nextdate = Format(Now, "yyyy-MM")
        ''    End If

        ''End If

        Dim strType1ID As String
        Dim strType2ID As String
        Dim strType3ID As String

        strType1ID = Nothing
        strType2ID = Nothing
        strType3ID = Nothing

        If PopupContainerEdit1.Tag = "" Then

        ElseIf Len(PopupContainerEdit1.Tag) = 5 Then
            strType1ID = PopupContainerEdit1.Tag
        ElseIf Len(PopupContainerEdit1.Tag) = 8 Then
            strType2ID = PopupContainerEdit1.Tag
        ElseIf Len(PopupContainerEdit1.Tag) = 11 Then
            strType3ID = PopupContainerEdit1.Tag
        End If


        '  MsgBox(strType1ID & "-" & strType2ID & "-" & strType3ID)
        '---------------------------------------------------------------------------------

        Dim CountJS As Integer
        Dim WC As New WareInventory.WareInventoryMTController
        Dim WL As New List(Of WareInventory.WareInventoryInfo)

        ''  MsgBox(strStat_Date & strEnd_Date & Nextdate)

        WL = WC.WareInventoryMothColl(txtWH.Tag, strType1ID, strType2ID, strType3ID, strStat_Date, strEnd_Date, Nextdate)

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
        exbook = exapp.Workbooks.Open(Application.StartupPath & "\ModuleFile\module1.xls")
        exsheet = exbook.Worksheets(1)


        ProgressBar1.Visible = True
        ProgressBar1.Maximum = CountJS

        exbook.ActiveSheet.Range(exsheet.Cells(1, 1), exsheet.Cells(1, 9)).Merge(Type.Missing)
        exbook.ActiveSheet.Range(exsheet.Cells(1, 1), exsheet.Cells(1, 9)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        exsheet.Cells(1, 1) = strStat_Date & "至" & strEnd_Date & "倉庫庫存匯總表"

        exsheet.Cells(2 + i, 1) = "倉庫編號"
        exsheet.Cells(2 + i, 2) = "物料編碼"
        exsheet.Cells(2 + i, 3) = "物料名稱"
        exsheet.Cells(2 + i, 4) = "規格"
        exsheet.Cells(2 + i, 5) = "單位"
        exsheet.Cells(2 + i, 6) = "期初"
        exsheet.Cells(2 + i, 7) = "總收入"
        exsheet.Cells(2 + i, 8) = "總發出"
        exsheet.Cells(2 + i, 9) = "期末"
        exsheet.Cells(2 + i, 10) = "時實節余"


        For i = 0 To CountJS - 1
            exsheet.Cells(3 + i, 1) = WL(i).WH_Name & "-" & WL(i).WH_SName
            exsheet.Cells(3 + i, 2) = WL(i).M_Code
            exsheet.Cells(3 + i, 3) = WL(i).M_Name
            exsheet.Cells(3 + i, 4) = WL(i).M_Gauge
            exsheet.Cells(3 + i, 5) = WL(i).M_Unit
            exsheet.Cells(3 + i, 6) = WL(i).QTYStart
            exsheet.Cells(3 + i, 7) = WL(i).SumIN
            exsheet.Cells(3 + i, 8) = WL(i).SumOut
            exsheet.Cells(3 + i, 9) = WL(i).QTYEnd
            exsheet.Cells(3 + i, 10) = WL(i).WI_Qty

            ProgressBar1.Value = i
        Next

        exbook.ActiveSheet.range(exsheet.Cells(2, 1), exsheet.Cells(2 + CountJS, 10)).borders(1).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(2, 1), exsheet.Cells(2 + CountJS, 10)).borders(2).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(2, 1), exsheet.Cells(2 + CountJS, 10)).borders(3).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(2, 1), exsheet.Cells(2 + CountJS, 10)).borders(4).linestyle = 1


        ProgressBar1.Visible = False

        Dim tempName As String

        SaveFileDialog1.InitialDirectory = "c:\"
        SaveFileDialog1.Filter = "txt files (*.xls)|*.xls"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tempName = SaveFileDialog1.FileName
            exsheet.SaveAs(tempName)
        End If

        exapp.Quit()
        exsheet = Nothing
        exbook = Nothing
        exapp = Nothing

        MsgBox("導出成功!")
    End Sub

    Private Sub txtWH_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWH.EditValueChanged

    End Sub

    Private Sub Tv1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Tv1.AfterSelect
        If Len(Tv1.SelectedNode.Tag) > 2 Then
        Else
            Exit Sub
        End If

        PopupContainerEdit1.Tag = Tv1.SelectedNode.Tag
        PopupContainerEdit1.Text = Tv1.SelectedNode.Text
        PopupContainerControl1.OwnerEdit.ClosePopup()
    End Sub
End Class