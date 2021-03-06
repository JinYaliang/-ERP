
Imports LFERP.Library.Production.ProductionDetailMainMonthSum
Imports LFERP.Library.Production.ProductionFieldDaySummaryInput
Imports LFERP.Library.Production.ProductionFieldDaySummaryCreat
Imports System.Data.SqlClient

Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess

Imports LFERP.Library.ProductionSchedule


Public Class frmProductionDetailMainMonthSum

    Dim Ex_Count As Integer
    Dim Ex_ID(50) As String
    Dim Ex_Type_Name(50) As String
    Dim Ex_CO_ID(50) As String
    Dim Ex_Sum_Content(50) As String

    Dim ds As New DataSet


    Function CreatTabel(ByVal _tablename As String) As Boolean

        Dim CreatTableStr As String

        CreatTabel = True

        Dim fc As New ProductionFieldDaySummaryCreatControl
        Dim fl As New List(Of ProductionFieldDaySummaryCreatInfo)
        fl = fc.ProductionFieldDaySummaryTempTable_GetList(_tablename)

        If fl.Count <= 0 Then
            CreatTableStr = "create table " + _tablename + "( AutoID  decimal(18, 0) IDENTITY(1,1) , PM_M_Code nvarchar(50) ,PM_Type nvarchar(50), FiledDate nvarchar(10))"

            Dim fcc As New ProductionFieldDaySummaryCreatControl

            If fcc.ProductionFieldDaySummaryTempAddUpdate(CreatTableStr) = True Then

            Else
                CreatTabel = False
                MsgBox(_tablename & "建表失敗!")
                Exit Function
            End If
        Else
            CreatTabel = False
        End If


    End Function
    Function ProductionFieldDaySummaryTempTableAlter(ByVal TableName1 As String) As Boolean
        ProductionFieldDaySummaryTempTableAlter = True

        Dim AddUpudatWeStr As String = ""
        Dim AddUpudateStr As String = ""

        Dim i, j As Integer

        For i = 1 To 31
            AddUpudateStr = ""

            For j = 1 To Ex_Count
                If Find_Coll(TableName1, Trim(Ex_ID(j)) & Format(i, "00")) = True Then
                    AddUpudateStr = AddUpudateStr + " " + Trim(Ex_ID(j)) & Format(i, "00") + " float ,"
                End If
            Next


            If Len(AddUpudateStr) > 0 Then
                ''執行 修改表結構
                AddUpudateStr = Mid(AddUpudateStr, 1, Len(AddUpudateStr) - 1)
                Dim aoc As New ProductionFieldDaySummaryCreatControl

                If aoc.ProductionFieldDaySummaryTempAlter(TableName1, AddUpudateStr) = True Then
                Else
                    ' MsgBox(AddUpudateStr & "保存失敗!")
                    ProductionFieldDaySummaryTempTableAlter = False
                    Exit Function
                End If
            End If
        Next

    End Function

    ''' <summary>
    '''  查詢 表中是否存在此字段
    ''' </summary>
    ''' <param name="TableName"></param>
    ''' <param name="FiledName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Find_Coll(ByVal TableName As String, ByVal FiledName As String) As Boolean
        Dim coc As New ProductionFieldDaySummaryCreatControl
        Dim col As New List(Of ProductionFieldDaySummaryCreatInfo)
        col = coc.ProductionFieldDaySummaryTempColl_GetList(TableName, FiledName)

        If col.Count > 0 Then
            Find_Coll = False
        Else
            Find_Coll = True
        End If

    End Function

    ''' <summary>
    ''' 載入 每道 工序需要統計的類型
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadProductionFieldDaySummaryInputType() As Boolean
        LoadProductionFieldDaySummaryInputType = True

        Dim ptc As New ProductionFieldDaySummaryInputControl
        Dim ptl As New List(Of ProductionFieldDaySummaryInputInfo)
        Dim j As Integer

        ptl = ptc.ProductionFieldDaySummaryInputType_GetList(Nothing, Nothing, Nothing, "MonthSum")

        Ex_Count = ptl.Count

        If Ex_Count <= 0 Then
            LoadProductionFieldDaySummaryInputType = False
            Exit Function
        End If

        For j = 1 To Ex_Count
            Ex_ID(j) = ptl(j - 1).ID
            Ex_Type_Name(j) = ptl(j - 1).TypeName
            Ex_CO_ID(j) = ptl(j - 1).CO_ID
            Ex_Sum_Content(j) = ptl(j - 1).Sum_Content
        Next


    End Function

    Private Sub frmProductionDetailMainMonthSum_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If LoadProductionFieldDaySummaryInputType() = False Then
            MsgBox("統計的工序類型為無記錄!")
        End If

        CrTable()

        Date_YYMM.EditValue = Format(Now, "yyyy年MM月")

        If LoadProductionFieldDaySummaryInputType() = False Then
            MsgBox("統計的工序類型為無記錄!")
        End If
    End Sub



    ''' <summary>
    ''' 統計所有已進入生產工藝流程的記錄
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ProcessMainPM_M_Code() As Boolean
        Dim j As Integer
        Dim strPM_M_Code As String
        Dim strPM_Type As String

        If PM_M_Code.EditValue = "全部" Then
            strPM_M_Code = Nothing
        Else
            strPM_M_Code = PM_M_Code.EditValue
        End If

        If gluType.EditValue = "全部" Then
            strPM_Type = Nothing
        Else
            strPM_Type = gluType.EditValue
        End If



        Dim mpi As List(Of ProductionFieldDaySummaryCreatInfo)
        Dim mpc As New ProductionFieldDaySummaryCreatControl
        mpi = mpc.ProductionFieldDaySummarySF_ProcessMainGetList1("生產加工", strPM_M_Code, strPM_Type)

        ''建表--------------------------------------------------------------------------------------
        PMProgressBar.Maximum = mpi.Count
        If mpi.Count > 0 Then
            For j = 0 To mpi.Count - 1
                SumLoadData(Date_YYMM.EditValue, mpi(j).PM_M_Code, mpi(j).PM_Type)
                PMProgressBar.Value = j
            Next
        End If

        MsgBox("導入完畢！")

    End Function



    ''' <summary>
    ''' 取得,,指定日期數量  生產完工  裝配倉  成品倉 出貨 統計出數據 
    ''' </summary>
    ''' <param name="_MonthDay"></param>
    ''' <param name="_PM_M_Code"></param>
    ''' <param name="_PM_Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SumLoadData(ByVal _MonthDay As String, ByVal _PM_M_Code As String, ByVal _PM_Type As String) As Boolean

        Dim L, k As Integer

        Dim strStat_Date, strEnd_Date As String
        ''------------------------------------------------------------------------------
        Dim intInputMonth As Integer '这是你输入的月份                                '|
        intInputMonth = Val(Format(CDate(_MonthDay), "MM"))                            '| 

        Dim dt As New DateTime(DateTime.Today.Year, intInputMonth, 1)                 '|

        '计算该月份的天数
        Dim days As Integer = dt.AddMonths(1).DayOfYear - dt.DayOfYear                '|
        If days <= 0 Or days > 31 Then
            days = 31
        End If
        strStat_Date = (dt.AddDays(0).ToString("yyyy/MM/dd"))                         '|
        strEnd_Date = (dt.AddDays(days - 1).ToString("yyyy/MM/dd"))                   '|
        ''------------------------------------------------------------------------------
        Dim _TableName As String = "ProductionDetailMainMonthSum"

        DataProgressBar.Maximum = days


        Dim AddStr, AddValue As String
        Dim FCM As New ProductionDetailMainMonthSumControl

        If FCM.ProductionDetailMainMonthSum_GetList(_PM_M_Code, _PM_Type, Format(CDate(_MonthDay), "yyyy-MM")).Count > 0 Then
        Else
            AddStr = "(PM_M_Code,PM_Type,FiledDate)"
            AddValue = "(" & "'" & _PM_M_Code & "'" & "," & "'" & _PM_Type & "'" & "," & "'" & Format(CDate(_MonthDay), "yyyy-MM") & "'" & ")"

            Dim LSstr As String

            LSstr = "insert " + _TableName + "  " & AddStr & " values " & AddValue
            Dim fcc As New ProductionFieldDaySummaryCreatControl

            If fcc.ProductionFieldDaySummaryTempAddUpdate(LSstr) = True Then
            Else
                MsgBox("保存失敗!")
                Exit Function
            End If
        End If


        For L = 1 To days '天數-----------
            Dim UpDateStr As String = ""
            Dim Strdate, StrdateS, StrdateE, Strdate1, StrdateS1, StrdateE1 As String
            '註：_PM_M_Code ,_PM_Type  StrdateS1 StrdateE1 這幾個定義的字段不能 隨便修改 （因寫入數據庫）


            Strdate = Format(DateAdd(DateInterval.Day, L - 1, CDate(strStat_Date)), "yyyy/MM/dd")
            Strdate1 = Format(DateAdd(DateInterval.Day, L - 1, CDate(strStat_Date)), "yyyy-MM-dd")

            StrdateS = Strdate + " 00:00:00" : StrdateE = Strdate + " 23:59:00"
            StrdateS1 = Strdate1 + " 00:00:00" : StrdateE1 = Strdate1 + " 23:59:00"

            For k = 1 To Ex_Count  ''取得,具體工序,指定日期數量  生產完工  裝配倉  成品倉 出貨

                Dim pcqc As New ProductionDetailMainMonthSumControl
                Dim pcql As New List(Of ProductionDetailMainMonthSumInfo)
                pcql = pcqc.ProductionDetailMainMonthSum_Qty(_PM_M_Code, _PM_Type, StrdateS1, StrdateE1, Trim(Ex_ID(k)))

                If pcql.Count <= 0 Then
                    UpDateStr = UpDateStr + Trim(Ex_ID(k)) & Trim(CStr(Format(L, "00"))) + "=" + Trim("'" + Str(0) + "'") + ","
                Else
                    UpDateStr = UpDateStr + Trim(Ex_ID(k)) & Trim(CStr(Format(L, "00"))) + "=" + Trim("'" + Str(pcql(0).Qty) + "'") + ","
                End If



            Next


            UpDateStr = Mid(UpDateStr, 1, Len(UpDateStr) - 1)

            UpDateStr = "Update  " + _TableName + " set " + UpDateStr + " where PM_M_Code='" + _PM_M_Code + "' and PM_Type='" + _PM_Type + "' and FiledDate='" + Format(CDate(_MonthDay), "yyyy-MM") + "'"


            Dim fcc As New ProductionFieldDaySummaryCreatControl
            If fcc.ProductionFieldDaySummaryTempAddUpdate(UpDateStr) = True Then
            Else
                MsgBox("保存失敗!")
                SumLoadData = False
                Exit Function
            End If

            DataProgressBar.Value = L

        Next

    End Function





    Private Sub CreatTableButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreatTableButton.Click

        ''列出所的以進入生產工藝流程的所有產品---------------------

        Dim ErrBz As String = ""
        Dim _TableNameLS As String
        _TableNameLS = "ProductionDetailMainMonthSum"

        If CreatTabel(_TableNameLS) = True Then

            If ProductionFieldDaySummaryTempTableAlter(_TableNameLS) = True Then ''修改表結構

            Else
                MsgBox("修改表結構失敗!")
                Exit Sub
            End If
        Else
            MsgBox("表結構已存在！")
            Exit Sub
        End If

        ''載入數據--------------------------------------------------
    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 載入產品編號 (進入生產工藝的）
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadPM_M_Code()
        ''------------------------------------------------
        'Dim row As DataRow
        'Dim j As Integer
        'Dim mpi As List(Of ProductionFieldDaySummaryCreatInfo)
        'Dim mpc As New ProductionFieldDaySummaryCreatControl

        'mpi = mpc.ProductionFieldDaySummarySF_ProcessMainGetList(Nothing)

        'row = ds.Tables("PM_M_Code").NewRow
        'row("PM_M_Code") = "全部"
        'row("PM_JiYu") = "全部"
        'ds.Tables("PM_M_Code").Rows.Add(row)

        'If mpi.Count > 0 Then
        '    For j = 0 To mpi.Count - 1
        '        row = ds.Tables("PM_M_Code").NewRow
        '        row("PM_M_Code") = mpi(j).PM_M_Code
        '        row("PM_JiYu") = mpi(j).PM_JiYu
        '        ds.Tables("PM_M_Code").Rows.Add(row)
        '    Next
        'End If

        Dim row As DataRow
        Dim j As Integer

        Dim mpi As List(Of ProductionScheduleInfo)
        Dim mpc As New ProductionScheduleControl

        mpi = mpc.ProductionSchedule_GetList4("生產加工")

        row = ds.Tables("PM_M_Code").NewRow
        row("PM_M_Code") = "全部"
        row("PM_JiYu") = "全部"
        ds.Tables("PM_M_Code").Rows.Add(row)

        If mpi.Count > 0 Then
            For j = 0 To mpi.Count - 1
                row = ds.Tables("PM_M_Code").NewRow
                row("PM_M_Code") = mpi(j).PM_M_Code
                row("PM_JiYu") = mpi(j).PM_JiYu
                ds.Tables("PM_M_Code").Rows.Add(row)
            Next
        End If


    End Sub


    Sub CrTable()
        With ds.Tables.Add("PM_M_Code")
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_JiYu", GetType(String))
        End With

        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = ds.Tables("PM_M_Code")

        With ds.Tables.Add("TPM_Type")
            .Columns.Add("PM_Type", GetType(String))
        End With

        gluType.Properties.DisplayMember = "PM_Type"
        gluType.Properties.ValueMember = "PM_Type"
        gluType.Properties.DataSource = ds.Tables("TPM_Type")




       
        LoadPM_M_Code()
    End Sub

    Private Sub PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged
        If PM_M_Code.EditValue Is Nothing Or PM_M_Code.EditValue = "" Then
            Exit Sub
        End If

        ds.Tables("TPM_Type").Clear()

        Dim strPM_M_Code As String

        If PM_M_Code.EditValue = "全部" Then
            strPM_M_Code = Nothing
        Else
            strPM_M_Code = PM_M_Code.EditValue
        End If


        Dim pcc As New ProcessMainControl
        Dim pcl As New List(Of ProcessMainInfo)

        pcl = pcc.ProcessMain_GetList1(Nothing, PM_M_Code.EditValue, "生產加工", Nothing)

        Dim row As DataRow
        Dim j As Integer

        row = ds.Tables("TPM_Type").NewRow
        row("PM_Type") = "全部"
        ds.Tables("TPM_Type").Rows.Add(row)

        If pcl.Count > 0 Then
            For j = 0 To pcl.Count - 1
                row = ds.Tables("TPM_Type").NewRow
                row("PM_Type") = pcl(j).Type3ID
                ds.Tables("TPM_Type").Rows.Add(row)
            Next
        End If

    End Sub

    Private Sub CollectButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollectButton.Click

        If PM_M_Code.EditValue Is Nothing Or PM_M_Code.EditValue = "" Then
            MsgBox("請選擇產品編號!")
            Exit Sub
        End If

        If gluType.EditValue Is Nothing Or gluType.EditValue = "" Then
            MsgBox("請選類型!")
            Exit Sub
        End If

        ProcessMainPM_M_Code()
    End Sub
End Class