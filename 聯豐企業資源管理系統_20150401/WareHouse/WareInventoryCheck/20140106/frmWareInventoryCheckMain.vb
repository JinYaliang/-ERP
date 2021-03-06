Imports System.IO
Imports LFERP.FileManager
Imports LFERP.SystemManager
Imports LFERP.Library.WareHouse.WareInventoryCheck
Imports LFERP.Library.WareHouse.WareInventory
Imports LFERP.Library.WareHouse.WareHouseController

Public Class frmWareInventoryCheckMain
#Region "属性"
    Dim ds As New DataSet
    'Dim TempDepotNO As String = "W0301"
    'Dim TempWIC_NO As String = "W100100001"
#End Region

#Region "窗体载入"
    Private Sub frmWareInventoryCheckMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mt As New Library.WareHouse.WareHouseController
        mt.WareHouse_LoadToTreeView(TreeView1, WareSelect(InUserID, "500601"))
        CreateTables()
    End Sub
#End Region

#Region "树选择事件"
    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim wic As New WareInventoryCheckController
        Grid1.DataSource = wic.WareInventoryCheck_GetList(Nothing, TreeView1.SelectedNode.Tag)
        Grid2.DataSource = Nothing
        Grid3.DataSource = Nothing
    End Sub
#End Region

#Region "取得流水号"
    Public Function GetNO() As String
        Dim wqi As New WareInventoryCheckInfo
        Dim wqc As New WareInventoryCheckController
        Dim str As String
        str = CStr(Format(Now, "yyMM"))
        wqi = wqc.WareInventoryCheck_GetNO(str)
        If wqi Is Nothing Then
            GetNO = "W" & str & "00001"
        Else
            GetNO = "W" & str & Mid((CInt(Mid(wqi.WIC_NO, 6)) + 100001), 2)
        End If
    End Function
#End Region

#Region "載入盤點信息"
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        On Error GoTo A
        '  CreateTables()
        Dim wicc As New WareInventoryCheckController
        Dim wici As New WareInventoryCheckInfo
        '<<<<寫入WareInventoryCheck 表
        'wici.WIC_NO = TempWIC_NO

        wici.WIC_NO = GetNO()

        wici.WIC_Date = Format(Now, "Short date")
        wici.DepotNO = TreeView1.SelectedNode.Tag
        wici.WIC_Remark = ""
        wici.WIC_Action = InUserID
        wicc.WareInventoryCheck_Add(wici)

        Grid1.DataSource = wicc.WareInventoryCheck_GetList(Nothing, TreeView1.SelectedNode.Tag)
        ''>>>>>>>>>>>>>>
        'Dim strFileName As String
        'strFileName = Application.StartupPath & "\Collect.txt"
        ''<<<<寫入WareInventoryCheckSub 表
        'Dim file As New System.IO.StreamReader(strFileName)
        'Dim oneLine As String
        'oneLine = file.ReadLine()
        'While (oneLine <> " ")
        '    Dim wicsi As New WareInventoryCheckInfo
        '    Dim wc As New WareInventoryMTController
        '    Dim wiList As New List(Of WareInventoryInfo)
        '    wicsi.WIC_NO = TempWIC_NO
        '    'wicsi.WIC_NO = GridView1.GetFocusedRowCellValue("WIC_NO").ToString
        '    wicsi.M_Code = Mid(oneLine.ToString, 1, 17)
        '    wicsi.DepotNO = Mid(oneLine.ToString, 19, 5)
        '    wicsi.WIC_NewQty = Mid(oneLine.ToString, 25)
        '    '查找原倉庫中此物料的庫存
        '    wiList = wc.WareInventory_GetMaterial(Nothing, Mid(oneLine.ToString, 19, 5), "'" & Mid(oneLine.ToString, 1, 17) & "'")
        '    If wiList.Count = 0 Then
        '        wicsi.WIC_OldQty = "0"
        '    Else
        '        wicsi.WIC_OldQty = wiList.Item(0).WI_Qty
        '    End If
        '    wicsi.WIC_Difference = CSng(Mid(oneLine.ToString, 25)) - CSng(wicsi.WIC_OldQty)   '差異數 ---轉化為單精度浮點型 2011/1/7
        '    wicc.WareInventoryCheckSub_Add(wicsi)
        '    'ListBox1.Items.Add(Mid(oneLine.ToString, 19, 5) & "_" & Mid(oneLine.ToString, 1, 17) & "_" & Mid(oneLine.ToString, 25) & "_" & TempWIC_NO)
        '    oneLine = file.ReadLine()
        'End While
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>
A:      Me.Text = Me.Text
    End Sub
#End Region

#Region "创建临时表"
    Sub CreateTables()
        ds.Tables.Clear()
        '創建數據表
        With ds.Tables.Add("WareInventoryCheckProcess")
            .Columns.Add("WIC_NO", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("DepotNO", GetType(String))
            .Columns.Add("WIC_NewQty", GetType(String))
            .Columns.Add("WIC_OldQty", GetType(String))
            .Columns.Add("WIC_Difference", GetType(String))
            .Columns.Add("WIC_Process", GetType(String))
            .Columns.Add("WIC_Type", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
        End With
        '綁定表格
        'DataGridView1.DataSource = ds.Tables("WareInventoryCheckProcess")
        Grid3.DataSource = ds.Tables("WareInventoryCheckProcess")
        Grid2.DataSource = ds.Tables("WareInventoryCheckProcess")
    End Sub
#End Region

#Region "生成待處理表"
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Button2.Enabled = False
        '<<<<<<<<<<<<<<<<從表WareInventorySub 中導入盤盈盤虧數據
        Dim wicc As New WareInventoryCheckController
        Dim wicsiList As New List(Of WareInventoryCheckInfo)

        Dim strNO, strWHID As String
        strNO = GridView2.GetFocusedRowCellValue("WIC_NO").ToString
        strWHID = GridView2.GetFocusedRowCellValue("DepotNO").ToString

        wicsiList = wicc.WareInventoryCheckSub_GetList(strNO, strWHID, Nothing)
        'wicsiList = wicc.WareInventoryCheckSub_GetList(TempWIC_NO, TempDepotNO, Nothing)
        Dim i As Integer
        For i = 0 To wicsiList.Count - 1
            Dim wicpi As New WareInventoryCheckInfo
            Dim aList As New List(Of WareInventoryCheckInfo)

            aList = wicc.WareInventoryCheckSub_SumWICNewQty(wicsiList(i).WIC_NO, wicsiList(i).DepotNO, wicsiList(i).M_Code)



            wicpi.WIC_NO = wicsiList(i).WIC_NO
            wicpi.M_Code = wicsiList(i).M_Code
            wicpi.DepotNO = wicsiList(i).DepotNO
            wicpi.WIC_OldQty = wicsiList(i).WIC_OldQty


            If i <> wicsiList.Count - 1 Then
                '如果這不是最後一條記錄
                If wicsiList(i).M_Code = wicsiList(i + 1).M_Code Then
                    GoTo A
                Else
                    wicpi.WIC_NewQty = aList(0).WIC_NewQty
                    wicpi.WIC_Difference = aList(0).WIC_NewQty - wicsiList(i).WIC_OldQty

                End If
            End If

            If i = wicsiList.Count - 1 Then
                '如果這是最後一條記錄
                wicpi.WIC_NewQty = aList(0).WIC_NewQty
                wicpi.WIC_Difference = aList(0).WIC_NewQty - wicsiList(i).WIC_OldQty
            End If



            If wicpi.WIC_NewQty - wicpi.WIC_OldQty > 0 Then
                wicpi.WIC_Type = "盤盈"
            End If

            If wicpi.WIC_NewQty - wicpi.WIC_OldQty < 0 Then
                wicpi.WIC_Type = "盤虧"
            End If


            If wicpi.WIC_NewQty - wicpi.WIC_OldQty = 0 Then
                '盤點後某物料總數和庫存相等, 則不插入待處理表
                GoTo A
            End If

            wicc.WareInventoryCheckProcess_Add(wicpi)

A:      Next
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        '<<<<<<<<<<<<查找倉庫庫存,如倉庫有, 盤點表沒有,則加入待處理表
        Dim wimc As New WareInventoryMTController
        Dim wiiList As New List(Of WareInventoryInfo)
        Dim n As Integer
        'wiiList = wimc.WareInventory_GetMaterial(Nothing, TempDepotNO, Nothing)
        wiiList = wimc.WareInventory_GetList(Nothing, strWHID)

        For n = 0 To wiiList.Count - 1
            If wiiList.Item(n).WI_Qty = 0 Then GoTo B
            Dim bList As New List(Of WareInventoryCheckInfo)
            bList = wicc.WareInventoryCheckSub_GetList(strNO, strWHID, wiiList(n).M_Code)
            'bList = wicc.WareInventoryCheckSub_GetList(TempWIC_NO, TempDepotNO, wiiList.Item(n).M_Code)
            If bList.Count = 0 Then
                Dim wicpi2 As New WareInventoryCheckInfo
                wicpi2.WIC_NO = strNO
                'wicpi2.WIC_NO = TempWIC_NO
                wicpi2.M_Code = wiiList(n).M_Code
                wicpi2.DepotNO = strWHID
                wicpi2.WIC_NewQty = "0"
                wicpi2.WIC_OldQty = wiiList(n).WI_Qty
                wicpi2.WIC_Difference = 0 - wicpi2.WIC_OldQty
                wicpi2.WIC_Type = "盤虧"
                wicc.WareInventoryCheckProcess_Add(wicpi2)
            End If
B:      Next
        MsgBox("已生成相應待處理表!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        'Dim row As DataRow
        'row = ds.Tables("WareInventoryCheckProcess").NewRow
        'row("WIC_NO") = "Test01"
        'row("M_Code") = Mid(oneLine.ToString, 1, 17)
        'row("DepotNO") = Mid(oneLine.ToString, 19, 5)
        'row("WIC_NewQty") = Mid(oneLine.ToString, 25)
        'row("WIC_OldQty") = "0"
        'row("WIC_Difference") = Mid(oneLine.ToString, 25)
        'row("WIC_Process") = "未處理"
        'row("WIC_Type") = "盤盈"
        'ds.Tables("WareInventoryCheckProcess").Rows.Add(row)
    End Sub
#End Region

#Region "盤點單審核"
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If MsgBox("審核後將覆蓋原有庫存數, 且原有庫存無法再恢復,是否確定?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Button3.Enabled = False
            Dim wicc As New WareInventoryCheckController
            Dim wiciList As New List(Of WareInventoryCheckInfo)

            Dim strNO, strWHID As String
            strNO = GridView2.GetFocusedRowCellValue("WIC_NO").ToString
            strWHID = GridView2.GetFocusedRowCellValue("DepotNO").ToString
            wiciList = wicc.WareInventoryCheckProcess_GetList(strNO, strWHID, Nothing)

            'wiciList = wicc.WareInventoryCheckProcess_GetList(TempWIC_NO, TempDepotNO, Nothing)
            Dim i As Integer
            For i = 0 To wiciList.Count - 1
                Dim wici As New WareInventoryCheckInfo
                wici.WIC_NO = wiciList(i).WIC_NO
                wici.WIC_Check = True
                wici.WIC_CheckAction = InUserID
                wici.WIC_CheckType = "無誤"
                wici.WIC_CheckRemark = "完成審核,庫存數量變更"
                wici.M_Code = wiciList(i).M_Code
                wici.DepotNO = wiciList(i).DepotNO
                wici.WIC_NewQty = wiciList(i).WIC_NewQty
                wicc.WareInventoryCheck_Check(wici)
            Next
            MsgBox("已審核！", MsgBoxStyle.OkOnly)

            Dim wic As New WareInventoryCheckController
            Grid1.DataSource = wic.WareInventoryCheck_GetList(Nothing, TreeView1.SelectedNode.Tag)
        End If
    End Sub
#End Region

#Region "載入待處理表數據"
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim wicc As New WareInventoryCheckController
        Dim wiciList As List(Of WareInventoryCheckInfo)

        Dim strNO, strWHID As String
        strNO = GridView2.GetFocusedRowCellValue("WIC_NO").ToString
        strWHID = GridView2.GetFocusedRowCellValue("DepotNO").ToString

        wiciList = wicc.WareInventoryCheckProcess_GetList(strNO, strWHID, Nothing)
        'wiciList = wicc.WareInventoryCheckProcess_GetList(TempWIC_NO, TempDepotNO, Nothing)
        Dim i As Integer
        For i = 0 To wiciList.Count - 1

            Dim row As DataRow
            row = ds.Tables("WareInventoryCheckProcess").NewRow

            row("WIC_NO") = strNO
            'row("WIC_NO") = TempWIC_NO
            row("M_Code") = wiciList(i).M_Code
            'row("DepotNO") = TempDepotNO
            row("DepotNO") = strWHID
            row("WIC_NewQty") = wiciList(i).WIC_NewQty
            row("WIC_OldQty") = wiciList(i).WIC_OldQty
            row("WIC_Difference") = wiciList(i).WIC_Difference
            row("WIC_Process") = wiciList(i).WIC_Process
            row("WIC_Type") = wiciList(i).WIC_Type

            Dim mc As New LFERP.Library.Material.MaterialController

            row("M_Name") = mc.MaterialCode_Get(wiciList(i).M_Code).M_Name
            row("M_Gauge") = mc.MaterialCode_Get(wiciList(i).M_Code).M_Gauge

            ds.Tables("WareInventoryCheckProcess").Rows.Add(row)
        Next
        'DataGridView1.DataSource = ds.Tables("WareInventoryCheckProcess")
        Grid2.DataSource = ds.Tables("WareInventoryCheckProcess")
        Grid3.DataSource = ds.Tables("WareInventoryCheckProcess")
    End Sub
#End Region

#Region "没有起作用的按键"
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'On Error GoTo A
        'Dim strFileName As String
        'strFileName = Application.StartupPath & "\Collect.txt"

        'Dim file As New System.IO.StreamReader(strFileName)
        'Dim oneLine As String
        'oneLine = file.ReadLine()
        'While (oneLine <> " ")
        '    ListBox1.Items.Add(oneLine.ToString)
        '    oneLine = file.ReadLine()


        'End While

        'Dim fi As New FileInfo("我的電腦\行動裝置\Storage Card\Collect.txt ")
        'MessageBox.Show(fi.Length)
        'If fi.Length > 1 Then
        '    fi.Delete()
        '    Dim sw As StreamWriter = New StreamWriter( "我的電腦\行動裝置\Storage Card\Collect.txt ", True)
        '    sw.Close()
        'End If

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>

A:      Me.Text = Me.Text
    End Sub
#End Region

#Region "載入盤點數據"
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        '>>>>>>>>>>>>>>
        Dim wicc As New WareInventoryCheckController
        Dim wici As New WareInventoryCheckInfo
        Dim strFileName As String
        strFileName = Application.StartupPath & "\Collect.txt"

        '<<<<寫入WareInventoryCheckSub 表
        Dim file As New System.IO.StreamReader(strFileName)
        Dim oneLine As String
        oneLine = file.ReadLine()

        Try
            While (oneLine <> " ")
                Dim wicsi As New WareInventoryCheckInfo
                Dim wc As New WareInventoryMTController
                Dim wiList As New List(Of WareInventoryInfo)

                '判斷盤點選定的倉庫號是否與盤點機中倉庫號對應
                '如果相同則導入物料信息,不同則退出

                If Mid(oneLine.ToString, 19, 5) = TreeView1.SelectedNode.Tag Then

                    wicsi.WIC_NO = GridView2.GetFocusedRowCellValue("WIC_NO").ToString
                    wicsi.M_Code = Mid(oneLine.ToString, 1, 17)
                    wicsi.DepotNO = Mid(oneLine.ToString, 19, 5)
                    wicsi.WIC_NewQty = Mid(oneLine.ToString, 25)

                    '查找原倉庫中此物料的庫存
                    wiList = wc.WareInventory_GetMaterial(Nothing, Mid(oneLine.ToString, 19, 5), "'" & Mid(oneLine.ToString, 1, 17) & "'")
                    If wiList.Count = 0 Then
                        wicsi.WIC_OldQty = "0"
                    Else
                        wicsi.WIC_OldQty = wiList(0).WI_Qty
                    End If

                    wicsi.WIC_Difference = CSng(Mid(oneLine.ToString, 25)) - CSng(wicsi.WIC_OldQty)   '差異數 ---轉化為單精度浮點型 2011/1/7

                    wicc.WareInventoryCheckSub_Add(wicsi)

                    'ListBox1.Items.Add(Mid(oneLine.ToString, 19, 5) & "_" & Mid(oneLine.ToString, 1, 17) & "_" & Mid(oneLine.ToString, 25) & "_" & TempWIC_NO)
                    oneLine = file.ReadLine()
                Else
                    MsgBox("當前選定倉庫與盤點機中倉庫不對應!")
                    Exit Sub
                End If
            End While
            file.Close()
        Catch ex As Exception
        End Try
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    End Sub
#End Region

#Region "表格事件"
    '當前盤點單號下盤點物料記錄
    Private Sub Grid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid1.Click
        If GridView2.RowCount = 0 Then Exit Sub

        Dim strNO, strWHID As String

        strNO = GridView2.GetFocusedRowCellValue("WIC_NO").ToString
        strWHID = GridView2.GetFocusedRowCellValue("DepotNO").ToString

        Dim wic As New WareInventoryCheckController
        If wic.WareInventoryCheckSub_GetList(strNO, strWHID, Nothing).Count = 0 Then
            Grid2.DataSource = Nothing
        Else
            Grid2.DataSource = Nothing
            Grid2.DataSource = wic.WareInventoryCheckSub_GetList(strNO, strWHID, Nothing)
        End If
        If wic.WareInventoryCheckProcess_GetList(strNO, strWHID, Nothing).Count = 0 Then
            Grid3.DataSource = Nothing
        Else
            Grid2.DataSource = Nothing
            Grid3.DataSource = wic.WareInventoryCheckProcess_GetList(strNO, strWHID, Nothing)
        End If
    End Sub
#End Region

End Class