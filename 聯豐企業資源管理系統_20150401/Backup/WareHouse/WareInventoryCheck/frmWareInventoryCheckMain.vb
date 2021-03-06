Imports System.IO
Imports LFERP.FileManager
Imports LFERP.SystemManager
Imports LFERP.Library.WareHouse.WareInventoryCheck
Imports LFERP.Library.WareHouse.WareInventory
Imports LFERP.Library.WareHouse.WareHouseController
Imports LFERP.Library.WareHouse.WareChange

Public Class frmWareInventoryCheckMain

#Region "属性"
    Dim ds As New DataSet
    Dim WICON As New WareInventoryCheckController
    'Dim TempDepotNO As String = "W0301"
    'Dim TempWIC_NO As String = "W100100001"
#End Region

#Region "窗体载入"
    Private Sub frmWareInventoryCheckMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser() '权限设置"
        Dim mt As New Library.WareHouse.WareHouseController
        mt.WareHouse_LoadToTreeView(TreeView1, WareSelect(InUserID, "500601"))
        CreateTables()
    End Sub
#End Region

#Region "权限设置"
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500602")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdInData.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500603")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdTable.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500604")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdCheck.Enabled = True
        End If
    End Sub
#End Region

#Region "树选择事件"
    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        If e.Node.Level = 1 Then
            Grid1.DataSource = WICON.WareInventoryCheck_GetList(Nothing, TreeView1.SelectedNode.Tag)
            Grid2.DataSource = Nothing
            Grid3.DataSource = Nothing
        Else
            Grid1.DataSource = Nothing
            Grid2.DataSource = Nothing
            Grid3.DataSource = Nothing
        End If
    End Sub
#End Region

#Region "取得流水号"
    Public Function GetNO() As String
        Dim wqi As New WareInventoryCheckInfo
        'Dim wqc As New WareInventoryCheckController
        Dim str As String
        str = CStr(Format(Now, "yyMM"))
        wqi = WICON.WareInventoryCheck_GetNO(str)
        If wqi Is Nothing Then
            GetNO = "W" & str & "00001"
        Else
            GetNO = "W" & str & Mid((CInt(Mid(wqi.WIC_NO, 6)) + 100001), 2)
        End If
    End Function
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

#Region "表格事件"
    '當前盤點單號下盤點物料記錄
    Private Sub Grid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid1.Click
        If GridView2.RowCount = 0 Then Exit Sub
        Dim strNO, strWHID As String
        strNO = GridView2.GetFocusedRowCellValue("WIC_NO").ToString
        strWHID = GridView2.GetFocusedRowCellValue("DepotNO").ToString

        'Dim wic As New WareInventoryCheckController
        If WICON.WareInventoryCheckSub_GetList(strNO, strWHID, Nothing).Count = 0 Then
            Grid2.DataSource = Nothing
        Else
            Grid2.DataSource = Nothing
            Grid2.DataSource = WICON.WareInventoryCheckSub_GetList(strNO, strWHID, Nothing)
        End If
        If WICON.WareInventoryCheckProcess_GetList(strNO, strWHID, Nothing).Count = 0 Then
            Grid3.DataSource = Nothing
        Else
            Grid2.DataSource = Nothing
            Grid3.DataSource = WICON.WareInventoryCheckProcess_GetList(strNO, strWHID, Nothing)
        End If
    End Sub
#End Region

#Region "A:載入盤點數據"
    Private Sub cmdInData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInData.Click
        'OpenTextFile.Filter = "txt files (*.txt))|*.txt;"
        'OpenTextFile.FilterIndex = 1
        'OpenTextFile.RestoreDirectory = True

        'Dim StrFileNamePath As String = String.Empty
        'Dim StrWIC_NO As String = String.Empty
        'If OpenTextFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '    '1.生成自动流水号
        '    StrWIC_NO = GetNO()
        '    '2.載入盤點數據<<<<寫入WareInventoryCheckSub 表
        '    'Dim wicc As New WareInventoryCheckController
        '    StrFileNamePath = OpenTextFile.FileName
        '    Dim file As New System.IO.StreamReader(StrFileNamePath)
        '    Dim oneLine As String = file.ReadLine()
        '    Try
        '        While (oneLine <> String.Empty)
        '            Dim wicsi As New WareInventoryCheckInfo
        '            Dim wc As New WareInventoryMTController
        '            Dim wiList As New List(Of WareInventoryInfo)
        '            '判斷盤點選定的倉庫號是否與盤點機中倉庫號對應
        '            '如果相同則導入物料信息,不同則退出
        '            If Mid(oneLine.ToString, 19, 5) = TreeView1.SelectedNode.Tag Then
        '                wicsi.WIC_NO = StrWIC_NO
        '                wicsi.M_Code = Mid(oneLine.ToString, 1, 17)
        '                wicsi.DepotNO = Mid(oneLine.ToString, 19, 5)
        '                wicsi.WIC_NewQty = Mid(oneLine.ToString, 25)
        '                '查找原倉庫中此物料的庫存
        '                wiList = wc.WareInventory_GetMaterial(Nothing, Mid(oneLine.ToString, 19, 5), "'" & Mid(oneLine.ToString, 1, 17) & "'")
        '                If wiList.Count = 0 Then
        '                    wicsi.WIC_OldQty = "0"
        '                Else
        '                    wicsi.WIC_OldQty = wiList(0).WI_Qty
        '                End If
        '                wicsi.WIC_Difference = CSng(Mid(oneLine.ToString, 25)) - CSng(wicsi.WIC_OldQty)   '差異數 ---轉化為單精度浮點型 2011/1/7
        '                WICON.WareInventoryCheckSub_Add(wicsi)
        '                oneLine = file.ReadLine()
        '            Else
        '                MsgBox("當前選定倉庫與盤點機中倉庫不對應!")
        '                Exit Sub
        '            End If
        '        End While
        '        file.Close()
        '    Catch ex As Exception
        '        MsgBox("文件格式不正确!")
        '        Exit Sub
        '    End Try

        '    '3.生成主档数据<<<<寫入WareInventoryCheck 表
        '    Dim wici As New WareInventoryCheckInfo
        '    wici.WIC_NO = StrWIC_NO
        '    wici.WIC_Date = Format(Now, "yyyy/MM/dd HH:mm:ss")
        '    wici.DepotNO = TreeView1.SelectedNode.Tag
        '    wici.WIC_Remark = ""
        '    wici.WIC_Action = InUserID
        '    WICON.WareInventoryCheck_Add(wici)
        '    Grid1.DataSource = WICON.WareInventoryCheck_GetList(Nothing, TreeView1.SelectedNode.Tag) '刷新主表数据

        '    '4.刷新盘点数据
        '    Grid1_Click(Nothing, Nothing)
        '    XtraTabControl1.SelectedTabPage = XtraTabPage1
        '    MsgBox("盘点单" + StrWIC_NO + "生成成功!")
        'End If
        OpenTextFile.Filter = "txt files (*.txt))|*.txt;"
        OpenTextFile.FilterIndex = 1
        OpenTextFile.RestoreDirectory = True

        Dim StrFileNamePath As String = String.Empty
        Dim StrWIC_NO As String = String.Empty
        If OpenTextFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            '1.生成自?流水?
            StrWIC_NO = GetNO()
            '2.載入盤點數據<<<<寫入WareInventoryCheckSub 表
            'Dim wicc As New WareInventoryCheckController
            StrFileNamePath = OpenTextFile.FileName
            Dim file As New System.IO.StreamReader(StrFileNamePath)
            Dim oneLine As String = file.ReadLine()
            Try
                While (oneLine <> String.Empty)
                    Dim wicsi As New WareInventoryCheckInfo
                    Dim wc As New WareInventoryMTController
                    Dim wiList As New List(Of WareInventoryInfo)
                    '判斷盤點選定的倉庫號是否與盤點機中倉庫號對應
                    '如果相同則導入物料信息,不同則退出

                    Dim InstrA As Integer
                    InstrA = InStr(oneLine, ",", CompareMethod.Text)

                    If InstrA > 0 Then
                        wicsi.WIC_NO = StrWIC_NO
                        wicsi.M_Code = UCase(Mid(oneLine, 1, InstrA - 1))
                        wicsi.DepotNO = TreeView1.SelectedNode.Tag
                        wicsi.WIC_NewQty = UCase(Mid(oneLine, InstrA + 1))

                        '查找原倉庫中此物料的庫存
                        wiList = wc.WareInventory_GetMaterial(Nothing, TreeView1.SelectedNode.Tag, "'" & UCase(Mid(oneLine, 1, InstrA - 1)) & "'")
                        If wiList.Count = 0 Then
                            wicsi.WIC_OldQty = "0"
                        Else
                            wicsi.WIC_OldQty = wiList(0).WI_Qty
                        End If
                        wicsi.WIC_Difference = CSng(UCase(Mid(oneLine, InstrA + 1))) - CSng(wicsi.WIC_OldQty)   '差異數 ---轉化為單精度浮點型 2011/1/7
                        WICON.WareInventoryCheckSub_Add(wicsi)
                        oneLine = file.ReadLine()
                    End If
                End While

                file.Close()
            Catch ex As Exception
                MsgBox("文件格式不正确!")
                Exit Sub
            End Try

            '3.生成主??据<<<<寫入WareInventoryCheck 表
            Dim wici As New WareInventoryCheckInfo
            wici.WIC_NO = StrWIC_NO
            wici.WIC_Date = Format(Now, "yyyy/MM/dd HH:mm:ss")
            wici.DepotNO = TreeView1.SelectedNode.Tag
            wici.WIC_Remark = ""
            wici.WIC_Action = InUserID
            WICON.WareInventoryCheck_Add(wici)
            Grid1.DataSource = WICON.WareInventoryCheck_GetList(Nothing, TreeView1.SelectedNode.Tag) '刷新主表?据

            '4.刷新???据
            Grid1_Click(Nothing, Nothing)
            XtraTabControl1.SelectedTabPage = XtraTabPage1
            MsgBox("盤點單" + StrWIC_NO + "生成成功!")
        End If
    End Sub
#End Region

#Region "B:生成待處理表"
    Private Sub cmdTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTable.Click
        Dim wicList As New List(Of WareInventoryCheckInfo)
        Dim strNO = GridView2.GetFocusedRowCellValue("WIC_NO").ToString
        wicList = WICON.WareInventoryCheck_GetList(strNO, Nothing)
        If wicList.Count > 0 Then
            If wicList(0).WIC_Check = True Then
                MsgBox("已審核,不能再生成处理表！", MsgBoxStyle.OkOnly)
                Exit Sub
            End If
        End If

        If GridView1.RowCount = 0 Then Exit Sub
        cmdTable.Enabled = False
        '<<<<<<<<<<<<<<<<從表WareInventorySub 中導入盤盈盤虧數據
        'Dim wicc As New WareInventoryCheckController
        Dim strWHID As String = GridView2.GetFocusedRowCellValue("DepotNO").ToString
        Dim wicsiList As New List(Of WareInventoryCheckInfo)
        wicsiList = WICON.WareInventoryCheckSub_GetList(strNO, strWHID, Nothing)

        Dim i As Integer
        For i = 0 To wicsiList.Count - 1
            Dim wicpi As New WareInventoryCheckInfo
            Dim aList As New List(Of WareInventoryCheckInfo)
            aList = WICON.WareInventoryCheckSub_SumWICNewQty(wicsiList(i).WIC_NO, wicsiList(i).DepotNO, wicsiList(i).M_Code)
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
            WICON.WareInventoryCheckProcess_Add(wicpi)
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
            bList = WICON.WareInventoryCheckSub_GetList(strNO, strWHID, wiiList(n).M_Code)
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
                WICON.WareInventoryCheckProcess_Add(wicpi2)
            End If
B:      Next
        MsgBox("已生成相應待處理表!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        Grid1_Click(Nothing, Nothing)
        XtraTabControl1.SelectedTabPage = XtraTabPage2
    End Sub
#End Region

#Region "C:盤點單審核"
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        '0.是否审核
        'Dim wic As New WareInventoryCheckController
        Dim wicList As New List(Of WareInventoryCheckInfo)
        Dim strNO = GridView2.GetFocusedRowCellValue("WIC_NO").ToString
        wicList = WICON.WareInventoryCheck_GetList(strNO, Nothing)
        If wicList.Count > 0 Then
            If wicList(0).WIC_Check = True Then
                MsgBox("已審核,不能再审核！", MsgBoxStyle.OkOnly)
                Exit Sub
            End If
        End If

        If MsgBox("審核後將覆蓋原有庫存數, 且原有庫存無法再恢復,是否確定?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '1.修改盘点差库存
            cmdCheck.Enabled = False
            Dim wicc As New WareInventoryCheckController
            Dim wiciList As New List(Of WareInventoryCheckInfo)

            Dim strGetChangeNO As String = GetChangeNO()
            Dim strWHID As String = GridView2.GetFocusedRowCellValue("DepotNO").ToString
            wiciList = wicc.WareInventoryCheckProcess_GetList(strNO, strWHID, Nothing)
            Dim i As Integer
            If wiciList.Count > 0 Then
                For i = 0 To wiciList.Count - 1
                    Dim wici As New WareInventoryCheckInfo
                    wici.WIC_NO = wiciList(i).WIC_NO
                    wici.WIC_Check = True
                    wici.WIC_CheckAction = InUserID
                    wici.WIC_CheckType = "無誤"
                    wici.WIC_CheckRemark = strGetChangeNO + "更改单.完成審核,庫存數量變更"
                    wici.M_Code = wiciList(i).M_Code
                    wici.DepotNO = wiciList(i).DepotNO
                    wici.WIC_NewQty = wiciList(i).WIC_NewQty
                    wicc.WareInventoryCheck_Check(wici)
                Next
                MsgBox("已審核！", MsgBoxStyle.OkOnly)
            Else
                Dim wici As New WareInventoryCheckInfo
                wici.WIC_NO = strNO
                wici.WIC_Check = True
                wici.WIC_CheckAction = InUserID
                wici.WIC_CheckType = "無誤"
                wici.WIC_CheckRemark = "完成審核,不存在差异"
                wicc.WareInventoryCheck_UpdateCheck(wici)
                MsgBox("已審核不存在差异！", MsgBoxStyle.OkOnly)
            End If

            Dim wic As New WareInventoryCheckController
            Grid1.DataSource = wic.WareInventoryCheck_GetList(Nothing, TreeView1.SelectedNode.Tag)

            '2.新增更改单
            Dim wi As New WareChangeInfo
            Dim wicon As New WareChangeControl

            wi.C_ChangeNO = strGetChangeNO
            wi.WH_ID = strWHID
            wi.C_Date = Format(Now, "yyyy/MM/dd HH:mm:ss")
            wi.C_Action = InUserID
            wi.C_Remark = strNO + ":盘点单转入"
            wi.C_Check = True
            wi.C_CheckAction = InUserID
            wi.C_ReCheck = True
            wi.C_ReCheckAction = InUserID
            If wiciList.Count <= 0 Then
                Exit Sub
            End If
            For i = 0 To wiciList.Count - 1
                wi.M_Code = wiciList(i).M_Code
                wi.WI_Qty = wiciList(i).WIC_OldQty
                wi.C_Qty = wiciList(i).WIC_NewQty
                wicon.WareChange_Insert(wi)
            Next
            MsgBox("已保存,更改單號: " & strGetChangeNO & "! ")

        End If
    End Sub
#End Region

#Region "自动流水号"
    ''' <summary>
    ''' 自動獲得更改單單號
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetChangeNO() As String
        Dim str As String
        str = CStr(Format(Now, "yyMM"))
        Dim ai As New WareChangeInfo
        Dim ac As New WareChangeControl

        ai = ac.WareChange_GetNO(str)
        If ai Is Nothing Then
            GetChangeNO = "C" & str & "00001"
        Else
            GetChangeNO = "C" & str & Mid((CInt(Mid(ai.C_ChangeNO, 6)) + 100001), 2)
        End If
    End Function
#End Region

End Class