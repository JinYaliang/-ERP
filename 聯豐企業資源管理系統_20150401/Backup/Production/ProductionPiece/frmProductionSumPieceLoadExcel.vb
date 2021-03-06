Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core
Imports System.Data.OleDb
Imports System.Data.SqlClient

Imports LFERP.Library.ProductionPieceProcess
Imports LFERP.Library.ProductionPiecePersonnel
Imports LFERP.Library.ProductionSumPiecePersonnel
Imports LFERP.Library.ProductionPieceWorkGroup
Imports LFERP.Library.ProductionPiecePersonnelMothClass

Imports LFERP.Library.ProductionSumPieceWorkGroup
Imports LFERP.Library.ProductionSumTimePersonnel
Imports LFERP.Library.ProductionSumTimeWorkGroup


Public Class frmProductionSumPieceLoadExcel
    Dim ds1 As New DataSet
    Dim ds As New DataSet
    Dim LoadType As String '組別計件  or 個人計件
    Dim Export_Mark As New ArrayList

    Private Sub frmProductionSumPieceLoadExcel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadType = tempValue2
        tempValue2 = Nothing
        CreateTable(LoadType)
    End Sub



    '員工編號	姓名	班制	廠別	部門	部門編號	產品編號	配件名稱	小工序名稱	數量	計件日期	備注
    Private Sub ButtonLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLoad.Click
        OpenFileDialog1.InitialDirectory = "c:\"
        OpenFileDialog1.Filter = "txt files (*.xls;*.xlsx))|*.xls; *.xlsx"
        OpenFileDialog1.FilterIndex = 2
        OpenFileDialog1.RestoreDirectory = True

        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ds1.Clear()

            PathTextBox.Text = OpenFileDialog1.FileName
            ''讀取 sheet 工作表 (讀取第一個工作表)
            Dim _Sheet As String
            _Sheet = GetExcelSheetNames(PathTextBox.Text)(0).ToString

            If _Sheet = "" Then
                MsgBox("無工作表單存在，請檢查!")
                Exit Sub
            End If

            GetExcelToDataTableBySheet(OpenFileDialog1.FileName, _Sheet)

            PieceSumPersonel(LoadType)
        End If
    End Sub

    Public Function GetExcelSheetNames(ByVal excelFile As String) As [String]()
        Dim objConn As OleDbConnection = Nothing
        Dim dt As System.Data.DataTable = Nothing

        Try

            Dim strConn As String = ""
            If RadioButton1.Checked = True Then
                strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + excelFile + ";Extended Properties='Excel 8.0; HDR=NO; IMEX=1'"    ''''; //此连接只能操作Excel2007之前(.xls)文件
            ElseIf RadioButton2.Checked = True Then
                strConn = "Provider=Microsoft.Ace.OleDb.12.0;" & "data source=" + excelFile & ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'"
            End If
            '此连接可以操作.xls与.xlsx文件
            objConn = New OleDbConnection(strConn)
            objConn.Open()
            dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            If dt Is Nothing Then
                Return Nothing
            End If
            Dim excelSheets As [String]() = New [String](dt.Rows.Count - 1) {}
            Dim i As Integer = 0
            For Each row As DataRow In dt.Rows
                excelSheets(i) = row("TABLE_NAME").ToString()
                i += 1
            Next

            Return excelSheets
        Catch
            Return Nothing
        Finally
            If objConn IsNot Nothing Then
                objConn.Close()
                objConn.Dispose()
            End If
            If dt IsNot Nothing Then
                dt.Dispose()
            End If
        End Try
    End Function
    Public Function GetExcelToDataTableBySheet(ByVal FileFullPath As String, ByVal SheetName As String) As DataTable
        Dim strConn As String = ""
        If RadioButton1.Checked = True Then
            strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + FileFullPath + ";Extended Properties='Excel 8.0; HDR=NO; IMEX=1'"    ''; //此连接只能操作Excel2007之前(.xls)文件
        ElseIf RadioButton2.Checked = True Then
            strConn = "Provider=Microsoft.Ace.OleDb.12.0;" & "data source=" + FileFullPath & ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'"
        End If
        '此连接可以操作.xls与.xlsx文件
        Dim conn As New OleDbConnection(strConn)
        conn.Open()

        ds1.Clear()
        ds1.Tables.Clear()

        Dim odda As New OleDbDataAdapter(String.Format("SELECT * FROM [{0}]", SheetName), conn)
        '("select * from [Sheet1$]", conn);
        odda.Fill(ds1, SheetName)
        conn.Close()

        Return ds1.Tables(0)
    End Function

    Function CreateTable(ByVal _LoadType As String) As Boolean
        Dim strA As String = ""
        If _LoadType = "個人計件" Then
            strA = "廠証編號,姓名,班制,部門編號,廠別編號,產品編號,配件名稱,小工序名稱,數量,計件日期,備注"
            Me.Text = "個人計件數據導入"
            Label2.Text = "個人計件數據導入"
        End If

        If _LoadType = "組別計件" Then
            strA = "組別編號,組別名稱,部門編號,廠別編號,產品編號,配件名稱,小工序名稱,數量,承包系數,計件日期,備注"
            Me.Text = "組別計件數據導入"
            Label2.Text = "組別計件數據導入"
        End If

        If _LoadType = "個人計時" Then
            strA = "廠証編號,姓名,部門編號,廠別編號,開始時間,結束時間,合計時長,記錄日期,備注"
            Me.Text = "個人計時數據導入"
            Label2.Text = "個人計時數據導入"
        End If

        If _LoadType = "組別計時" Then
            strA = " 廠証編號,姓名,組別,組別編號,部門編號,廠別編號,開始時間,結束時間,合計時長,記錄日期,備注"
            Me.Text = "組別計時數據導入"
            Label2.Text = "組別計時數據導入"
        End If


        '創建類型表
        With ds.Tables.Add("Target")
            Dim StrAarray As Array = Split(strA, ",")
            For i As Integer = 0 To UBound(StrAarray)
                .Columns.Add(Trim(StrAarray(i)), GetType(String))
            Next
        End With

        Grid1.DataSource = ds.Tables("Target")
    End Function

    Function PieceSumPersonel(ByVal _LoadType As String) As Boolean

        Dim strA As String = ""
        If _LoadType = "個人計件" Then
            strA = "廠証編號,姓名,班制,部門編號,廠別編號,產品編號,配件名稱,小工序名稱,數量,計件日期,備注"
        End If

        If _LoadType = "組別計件" Then
            strA = "組別編號,組別名稱,部門編號,廠別編號,產品編號,配件名稱,小工序名稱,數量,承包系數,計件日期,備注"
        End If

        If _LoadType = "個人計時" Then
            strA = "廠証編號,姓名,部門編號,廠別編號,開始時間,結束時間,合計時長,記錄日期,備注"
        End If

        If _LoadType = "組別計時" Then
            strA = "廠証編號,姓名,組別,組別編號,部門編號,廠別編號,開始時間,結束時間,合計時長,記錄日期,備注"
        End If


        Dim i, j As Integer
        Dim RowsCount As Integer

        RowsCount = ds1.Tables(0).Rows.Count

        If RowsCount > 0 Then
            ds.Tables("Target").Clear()

            For i = 1 To RowsCount - 1
                Dim row As DataRow
                row = ds.Tables("Target").NewRow

                Dim StrAarray As Array = Split(strA, ",")
                For k As Integer = 0 To UBound(StrAarray)
                    row(k) = Trim(ds1.Tables(0).Rows(i)(k).ToString)
                Next

                ds.Tables("Target").Rows.Add(row)
            Next

            '固定  MG1020-M31-1 為 凸台
            '      MG1056-M7-4  為 凸臺
            If _LoadType = "個人計件" Or _LoadType = "組別計件" Then
                For j = 0 To RowsCount - 2
                    If Trim(ds.Tables("Target").Rows(j)("產品編號")) = "MG1020-M31-1" And ds.Tables("Target").Rows(j)("配件名稱") = "凸臺" Then
                        ds.Tables("Target").Rows(j)("配件名稱") = "凸台"
                    End If

                    If Trim(ds.Tables("Target").Rows(j)("產品編號")) = "MG1056-M7-4" And Trim(ds.Tables("Target").Rows(j)("配件名稱")) = "凸台" Then
                        ds.Tables("Target").Rows(j)("配件名稱") = "凸臺"
                    End If
                Next
            End If

        End If

    End Function

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        'ChcekDateA

        If LoadType = "個人計件" Then
            SaveDataSumPer()
        End If

        If LoadType = "組別計件" Then
            SaveDataSumWG()
        End If

        If LoadType = "個人計時" Then
            SaveDataSumPerTime()
        End If

        If LoadType = "組別計時" Then
            SaveDataSumWGTime()
        End If

    End Sub
#Region "導入數據檢查"
    ''' <summary>
    ''' 組別計時用
    ''' </summary>
    ''' <param name="_Per_NO"></param>
    ''' <param name="_G_NO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Check_Piece_PerNO_GNO(ByVal _Per_NO As String, ByVal _G_NO As String) As Boolean
        Dim pc As New ProductionPiecePersonnelControl
        Dim pl As New List(Of ProductionPiecePersonnelInfo)
        pl = pc.ProductionPiecePersonnel_GetList(Nothing, _Per_NO, Nothing, _G_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pl.Count <= 0 Then
            Check_Piece_PerNO_GNO = False
        Else
            Check_Piece_PerNO_GNO = True
        End If
    End Function
    ''' <summary>
    ''' 檢查人員是否已存在，計件人員名單中
    ''' </summary>
    ''' <param name="_Per_NO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Check_Piece_PerNO(ByVal _Per_NO As String) As Boolean
        Dim pc As New ProductionPiecePersonnelControl
        Dim pl As New List(Of ProductionPiecePersonnelInfo)
        pl = pc.ProductionPiecePersonnel_GetList(Nothing, _Per_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pl.Count <= 0 Then
            Check_Piece_PerNO = False
        Else
            Check_Piece_PerNO = True
        End If
    End Function


    ''' <summary>
    ''' 檢查組別編號是否存在
    ''' </summary>
    ''' <param name="_G_NO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Check_G_NO(ByVal _G_NO As String) As Boolean

        Dim ppwg As New ProductionPieceWorkGroupControl
        Dim ppwGL As New List(Of ProductionPieceWorkGroupInfo)

        ppwGL = ppwg.ProductionPieceWorkGroup_GetList(_G_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If ppwGL.Count <= 0 Then
            Check_G_NO = False
        Else
            Check_G_NO = True
        End If

    End Function


    ''' <summary>
    ''' 檢查當前數據是否已鎖定
    ''' </summary>
    ''' <param name="DepID"></param>
    ''' <param name="strDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckSumLock(ByVal DepID As String, ByVal strDate As String) As Boolean
        CheckSumLock = True

        Dim pcA As New LFERP.Library.ProductionSumLock.ProductionSumLockControl
        Dim plA As New List(Of LFERP.Library.ProductionSumLock.ProductionSumLockInfo)
        plA = pcA.ProductionSumLock_GetList(Nothing, Nothing, DepID, Format(CDate(strDate), "yyyy/MM"))

        If plA.Count > 0 Then
            If plA(0).LockCheck = True Then
                CheckSumLock = False
            End If
        End If
    End Function

    ''' <summary>
    ''' 查詢出計件工藝 是否存在，或記錄已存在
    ''' </summary>
    ''' <param name="cboPro_Type"></param>
    ''' <param name="gluPM_M_Code"></param>
    ''' <param name="gluPM_Type"></param>
    ''' <param name="DepIDA"></param>
    ''' <param name="PP_N_Name"></param>
    ''' <param name="_Date"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Check_PerProcess(ByVal cboPro_Type As String, ByVal gluPM_M_Code As String, ByVal gluPM_Type As String, ByVal DepIDA As String, ByVal PP_N_Name As String, ByVal _Date As String, ByVal _Per_NOA As String) As String
        Dim pcc As New ProductionPieceProcessControl
        Dim pci As List(Of ProductionPieceProcessInfo)

        Check_PerProcess = ""

        pci = pcc.ProductionPieceProcess_GetList(Nothing, Nothing, cboPro_Type, gluPM_M_Code, gluPM_Type, Nothing, PP_N_Name, DepIDA, True, True, Nothing, Nothing)
        If pci.Count > 0 Then

            If Format(CDate(_Date), "yyyy/MM/dd") >= Format(pci(0).PP_BeginUseDate, "yyyy/MM/dd") And Format(CDate(_Date), "yyyy/MM/dd") <= Format(pci(0).PP_EndUseDate, "yyyy/MM/dd") Then
                ' If CDate(_Date) >= pci(0).PP_BeginUseDate And CDate(_Date) <= pci(0).PP_EndUseDate Then
                ''前天工序已參與計件錄入
                Dim pct As New ProductionSumPiecePersonnelControl
                If pct.ProductionSumPiecePersonnel_GetList(pci(0).AutoID, Nothing, _Per_NOA, Nothing, DepIDA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _Date, Nothing, _Date, Nothing).Count > 0 Then
                    Check_PerProcess = "計件數據中已存在此記錄"
                Else
                    Check_PerProcess = Trim("A" & pci(0).AutoID)
                End If
            Else
                Check_PerProcess = "當工序在計件工藝中,已超有效期"
            End If
        Else
            Check_PerProcess = "當前工序在計件工藝中,不存在或未審核"
        End If

    End Function

    ''' <summary>
    ''' 查詢出計件工藝 是否存在，或記錄已存在
    ''' </summary>
    ''' <param name="cboPro_Type"></param>
    ''' <param name="gluPM_M_Code"></param>
    ''' <param name="gluPM_Type"></param>
    ''' <param name="DepIDA"></param>
    ''' <param name="PP_N_Name"></param>
    ''' <param name="_Date"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Check_WGProcess(ByVal cboPro_Type As String, ByVal gluPM_M_Code As String, ByVal gluPM_Type As String, ByVal DepIDA As String, ByVal PP_N_Name As String, ByVal _Date As String, ByVal _G_NOA As String) As String
        Dim pcc As New ProductionPieceProcessControl
        Dim pci As List(Of ProductionPieceProcessInfo)

        Check_WGProcess = ""

        pci = pcc.ProductionPieceProcess_GetList(Nothing, Nothing, cboPro_Type, gluPM_M_Code, gluPM_Type, Nothing, PP_N_Name, DepIDA, True, True, Nothing, Nothing)
        If pci.Count > 0 Then

            If Format(CDate(_Date), "yyyy/MM/dd") >= Format(pci(0).PP_BeginUseDate, "yyyy/MM/dd") And Format(CDate(_Date), "yyyy/MM/dd") <= Format(pci(0).PP_EndUseDate, "yyyy/MM/dd") Then
                ''前天工序已參與計件錄入
                Dim oc As New ProductionSumPieceWorkGroupControl

                Dim pct As New ProductionSumPiecePersonnelControl
                If oc.ProductionSumPieceWorkGroup_GetList(pci(0).AutoID, Nothing, Nothing, _G_NOA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _Date, Nothing, _Date, Nothing).Count > 0 Then
                    Check_WGProcess = "計件數據中已存在此記錄"
                Else
                    Check_WGProcess = Trim("A" & pci(0).AutoID)
                End If
            Else
                Check_WGProcess = "當工序在計件工藝中,已超有效期"
            End If
        Else
            Check_WGProcess = "當前工序在計件工藝中,不存在或未審核"
        End If

    End Function

    ''' <summary>
    ''' 檢查是否為時間格式
    ''' </summary>
    ''' <param name="_Date"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChcekDateA(ByVal _Date As String) As Boolean
        Try
            Dim aa As Date
            aa = CDate(_Date)
            ChcekDateA = True
        Catch ex As Exception
            ChcekDateA = False
        End Try
    End Function

    ''' <summary>
    ''' 檢查日期+開始+結束時間
    ''' </summary>
    ''' <param name="_Date"></param>
    ''' <param name="_Stime"></param>
    ''' <param name="_Etime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChcekDateB(ByVal _Date As String, ByVal _Stime As String, ByVal _Etime As String) As Boolean
        ChcekDateB = True

        Try

            Dim aa As Date
            aa = CDate(_Date)


            Dim strDateEdit_S, strDateEdit_E As DateTime
            strDateEdit_S = DateTime.Parse(_Stime)  ''要加入的時間         要求保存的時間段
            strDateEdit_E = DateTime.Parse(_Etime)

            If Len(_Stime) <> 5 Or Len(_Etime) <> 5 Then
                ChcekDateB = False
            End If

        Catch ex As Exception
            ChcekDateB = False
        End Try
    End Function

    ''' <summary>
    ''' 檢查同一人在同一時間段不能有兩項作業
    ''' </summary>
    ''' <param name="txtPer_NO"></param>
    ''' <param name="PT_DateEdit"></param>
    ''' <param name="StartTimeEdit"></param>
    ''' <param name="EndTimeEdit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CHECK_TimePer(ByVal txtPer_NO As String, ByVal PT_DateEdit As String, ByVal StartTimeEdit As String, ByVal EndTimeEdit As String) As String
        Dim pil As New List(Of ProductionSumTimePersonnelInfo)
        Dim pc As New ProductionSumTimePersonnelControl
        Dim i As Integer

        Dim strDateEnd, strDateStart As DateTime
        Dim strDateEdit_S, strDateEdit_E As DateTime

        Dim CheckHourS1, CheckHourS2 As TimeSpan
        Dim CheckHourE1, CheckHourE2 As TimeSpan

        CHECK_TimePer = ""

        pil = pc.ProductionSumTimePersonnel_GetList(Nothing, txtPer_NO, Nothing, Nothing, Nothing, PT_DateEdit, Nothing, PT_DateEdit, Nothing, Nothing, Nothing)

        If pil.Count > 0 Then
        Else
            Exit Function
        End If

        For i = 0 To pil.Count - 1
            ''核查時間段----------------
            If pil(i).PT_BeginTime = Nothing Or pil(i).PT_EndTime = Nothing Then
                Exit Function
            End If

            strDateEnd = DateTime.Parse(pil(i).PT_EndTime)  ''記件結束時間              讀取數據庫中的時間段
            strDateStart = DateTime.Parse(pil(i).PT_BeginTime) ''記件開始時間

            strDateEdit_S = DateTime.Parse(StartTimeEdit)  ''要加入的時間         要求保存的時間段
            strDateEdit_E = DateTime.Parse(EndTimeEdit)

            ''-----------------------------------------------------------
            CheckHourS1 = strDateEdit_S - strDateStart   ''>0
            CheckHourS2 = strDateEdit_S - strDateEnd     ''<0

            If CheckHourS1.TotalHours >= 0 And CheckHourS2.TotalHours <= 0 Then
                CHECK_TimePer = "在遷定時間段中，此人員已參與作！"
                Exit Function
            End If
            ''-+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            CheckHourE1 = strDateEdit_E - strDateStart   ''>0
            CheckHourE2 = strDateEdit_E - strDateEnd     ''<0

            If CheckHourE1.TotalHours >= 0 And CheckHourE2.TotalHours <= 0 Then
                CHECK_TimePer = "在遷定時間段中，此人員已參與作！"
                Exit Function
            End If
        Next
    End Function

    ''' <summary>
    ''' 檢查線別計時數據
    ''' </summary>
    ''' <param name="txtPer_NO"></param>
    ''' <param name="GT_DateEdit"></param>
    ''' <param name="StartTimeEdit"></param>
    ''' <param name="EndTimeEdit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CHECK_TimeWG(ByVal txtPer_NO As String, ByVal GT_DateEdit As String, ByVal StartTimeEdit As String, ByVal EndTimeEdit As String) As String
        Dim pil As New List(Of ProductionSumTimeWorkGroupInfo)
        Dim pc As New ProductionSumTimeWorkGroupControl
        Dim i As Integer

        Dim strDateEnd, strDateStart As DateTime
        Dim strDateEdit_S, strDateEdit_E As DateTime

        Dim CheckHourS1, CheckHourS2 As TimeSpan
        Dim CheckHourE1, CheckHourE2 As TimeSpan

        CHECK_TimeWG = ""

        pil = pc.ProductionSumTimeWorkGroup_GetList(Nothing, txtPer_NO, Nothing, Nothing, Nothing, GT_DateEdit, Nothing, GT_DateEdit, Nothing, Nothing)


        If pil.Count > 0 Then
        Else
            Exit Function
        End If

        For i = 0 To pil.Count - 1
            ''核查時間段----------------
            If pil(i).GT_BeginTime = Nothing Or pil(i).GT_EndTime = Nothing Then
                Exit Function
            End If

            strDateEnd = DateTime.Parse(pil(i).GT_EndTime)  ''記件結束時間              讀取數據庫中的時間段
            strDateStart = DateTime.Parse(pil(i).GT_BeginTime) ''記件開始時間

            strDateEdit_S = DateTime.Parse(StartTimeEdit)  ''要加入的時間         要求保存的時間段
            strDateEdit_E = DateTime.Parse(EndTimeEdit)

            ''-----------------------------------------------------------
            CheckHourS1 = strDateEdit_S - strDateStart   ''>0
            CheckHourS2 = strDateEdit_S - strDateEnd     ''<0

            If CheckHourS1.TotalHours >= 0 And CheckHourS2.TotalHours <= 0 Then
                CHECK_TimeWG = "在遷定時間段中，此人員已參與作！"
            End If
            ''-+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            CheckHourE1 = strDateEdit_E - strDateStart   ''>0
            CheckHourE2 = strDateEdit_E - strDateEnd     ''<0

            If CheckHourE1.TotalHours >= 0 And CheckHourE2.TotalHours <= 0 Then
                CHECK_TimeWG = "在遷定時間段中，此人員已參與作！"
            End If
        Next
    End Function

#End Region


#Region "個人計件數據導入"

    Function SaveDataSumPer() As Boolean '個人計件數據
        SaveDataSumPer = True

        Dim MsgTextBoxStr As String = ""
        Dim OK_Bz As String = ""
        Dim Export_Excel As String = ""
        Dim Export_ExcelStr As String = ""
        Dim k As Integer

        If ds.Tables("Target").Rows.Count <= 0 Then
            MsgBox("請載入數據!")
            Exit Function
        End If

        ''先判斷時間是否有誤碼-------------------------------------------------------
        Dim i As Integer
        For i = 0 To ds.Tables("Target").Rows.Count - 1
            If ChcekDateA(ds.Tables("Target").Rows(i)("計件日期").ToString()) = False Then
                GridView1.FocusedRowHandle = i '移、至錯誤碼行
                MsgBox("第" & Str(i + 1) & "行,時間輸入有誤！")
                Exit Function
            End If
        Next
        ''-------------------------------------------------------


        Dim _Per_no, _per_class, _depid, _facid, _PM_M_Code, _PM_Type, _PP_N_Name, _Date, _Qty, _Remark As String
        Dim MsgStrA As String

        ProgressBar1.Visible = True
        ProgressBar1.Maximum = ds.Tables("Target").Rows.Count

        Export_Mark.Clear()

        For k = 0 To ds.Tables("Target").Rows.Count - 1
            OK_Bz = ""
            Export_ExcelStr = ""
            MsgStrA = ""

            'strA = "廠証編號,姓名,班制,廠別編號,部門編號,產品編號,配件名稱,小工序名稱,數量,計件日期,備注"
            _Per_no = ds.Tables("Target").Rows(k)("廠証編號").ToString()
            _per_class = ds.Tables("Target").Rows(k)("班制").ToString()
            _depid = ds.Tables("Target").Rows(k)("部門編號").ToString()
            _facid = ds.Tables("Target").Rows(k)("廠別編號").ToString()

            _PM_M_Code = ds.Tables("Target").Rows(k)("產品編號").ToString()
            _PM_Type = ds.Tables("Target").Rows(k)("配件名稱").ToString()
            _PP_N_Name = ds.Tables("Target").Rows(k)("小工序名稱").ToString()
            _Date = ds.Tables("Target").Rows(k)("計件日期").ToString()
            _Qty = ds.Tables("Target").Rows(k)("數量").ToString()

            _Remark = ds.Tables("Target").Rows(k)("備注").ToString()

            If _Per_no = "" Or _per_class = "" Or _depid = "" Or _PM_M_Code = "" Or _PM_Type = "" Or _PP_N_Name = "" Or _Qty = "" Or _Date = "" Or _facid = "" Then
                MsgTextBoxStr = MsgTextBoxStr & k + 1 & "廠証編號,班制,部門編號,廠別編號,產品編號,配件名稱,小工序名稱,數量,計件日期中一項輸入為空!" & vbCrLf
                Export_ExcelStr = "廠証編號,班制,部門編號,廠別編號,產品編號,配件名稱,小工序名稱,數量,計件日期中一項輸入為空!"
            Else
                If ChcekDateA(_Date) = False Then
                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "時間格式有誤碼!" & vbCrLf
                    Export_ExcelStr = "時間格式有誤碼!"
                Else
                    If Check_Piece_PerNO(_Per_no) = False Then
                        MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & _Per_no & ", 員工名單中不存在" & vbCrLf
                        Export_ExcelStr = "員工名單中不存在"
                    Else
                        If CheckSumLock(_depid, _Date) = False Then

                            MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & _depid & ",當月部門數據已鎖定" & vbCrLf
                            Export_ExcelStr = "當月部門數據已鎖定"
                        Else
                            MsgStrA = Check_PerProcess("生產加工", _PM_M_Code, _PM_Type, _depid, _PP_N_Name, _Date, _Per_no)

                            If Mid(MsgStrA, 1, 1) <> "A" Then
                                MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & MsgStrA & vbCrLf
                                Export_ExcelStr = MsgStrA
                            Else
                                ''寫入數據-----------------------------------------------------
                                Dim gc As New ProductionSumPiecePersonnelControl
                                Dim gi As New ProductionSumPiecePersonnelInfo

                                gi.Per_NO = _Per_no '員工工號

                                gi.Pro_Type = "生產加工"
                                gi.PM_M_Code = _PM_M_Code
                                gi.PM_Type = _PM_Type
                                gi.PP_Qty = Val(_Qty)   ''

                                gi.PP_Date = _Date
                                gi.PP_Action = InUserID

                                Dim StrPP_NO_LS As String
                                StrPP_NO_LS = ProductionSumPiecePersonnelNO()

                                If StrPP_NO_LS <> "" Then
                                Else
                                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "流水號獲取失敗"
                                    Export_ExcelStr = "流水號獲取失敗"
                                    GoTo AAA
                                End If

                                gi.PP_NO = StrPP_NO_LS  ''要先取得編號

                                Dim pcc As New ProductionPieceProcessControl
                                Dim pci As List(Of ProductionPieceProcessInfo)

                                pci = pcc.ProductionPieceProcess_GetList(Mid(MsgStrA, 2, Len(MsgStrA) - 1), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                                If pci.Count <= 0 Then
                                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "計件工藝流程載入失敗"
                                    Export_ExcelStr = "計件工藝流程載入失敗"
                                    GoTo AAA
                                End If

                                gi.PS_NO = pci(0).PS_NO.ToString  ''大工序編號
                                gi.PS_NameS = pci(0).PP_N_Name.ToString '小工序名稱

                                gi.PP_Price = pci(0).PP_Price.ToString   ''工價
                                ''autoID 也存一下
                                gi.PP_AutoID = pci(0).AutoID.ToString
                                gi.PP_Remark = _Remark

                                gi.DepID = _depid    ''部門編號
                                gi.FacID = _facid    '廠別 

                                If Find_Per_Class(_Per_no, _Date) = "" Then
                                    Add_Per_Class(_Per_no, _per_class, _Date)
                                Else
                                    Update_Per_Class(_Per_no, _per_class, _Date)
                                End If

                                If gc.ProductionSumPiecePersonnel_Add(gi) = True Then
                                    OK_Bz = "Y"
                                Else
                                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "保存失敗"
                                    Export_ExcelStr = "保存失敗"
                                End If
AAA:
                            End If  '產品工藝
                        End If '當月鎖定
                    End If '員工名單
                End If '檢查時間格式
            End If '檢查是否為空

            If OK_Bz = "" Then
                Export_Excel = Export_Excel + Str(k) + ","
                Export_Mark.Add(Export_ExcelStr)
            End If

            ProgressBar1.Value = k
        Next
        If Export_Excel = "" Then
            MsgBox("數據全部保存成功!")
        Else
            MsgBox(MsgTextBoxStr)
            Export_Excel_SumPerNO(Export_Excel)
        End If
        ProgressBar1.Visible = False
    End Function

    Function ProductionSumPiecePersonnelNO() As String

        ProductionSumPiecePersonnelNO = ""

        Dim str1, str2 As String
        Dim gc1 As New ProductionSumPiecePersonnelControl
        Dim gi1 As New ProductionSumPiecePersonnelInfo

        str1 = Mid(Year(Now), 3)
        If CInt(Month(Now)) < 10 Then
            str2 = "0" & Month(Now)
        Else
            str2 = Month(Now)
        End If

        Dim Stra As String
        Stra = Trim(str1 & str2)

        gi1 = gc1.ProductionSumPiecePersonnel_GetNO(Stra) '' 讀取基數

        If gi1 Is Nothing Then
            ProductionSumPiecePersonnelNO = "PP" & str1 & str2 & "0000001"
        Else
            ProductionSumPiecePersonnelNO = "PP" & str1 & str2 & Mid((CInt(Mid(gi1.PP_NO, 7)) + 10000001), 2)
        End If
    End Function

    Function Update_Per_Class(ByVal UPer_NO As String, ByVal UPer_Class As String, ByVal UDate As String) As Boolean  '保存班制信息至
        Dim Pec As New ProductionPiecePersonnelMothClassControl
        Dim pei As New ProductionPiecePersonnelMothClassInfo

        pei.Per_NO = UPer_NO
        pei.Per_Class = UPer_Class
        pei.Per_Date = Format(CDate(UDate), "yyyy/MM")

        Pec.ProductionPiecePersonnelMothClass_Update(pei)

    End Function

    Function Find_Per_Class(ByVal FPer_NO As String, ByVal FDate As String) As String   '查詢
        Find_Per_Class = ""

        Dim Pec As New ProductionPiecePersonnelMothClassControl
        Dim pel As New List(Of ProductionPiecePersonnelMothClassInfo)

        pel = Pec.ProductionPiecePersonnelMothClass_GetList(FPer_NO, Format(CDate(FDate), "yyyy/MM"), Nothing)

        If pel.Count <= 0 Then
            Find_Per_Class = ""
        Else
            Find_Per_Class = pel(0).Per_Class
        End If
    End Function

    Function Add_Per_Class(ByVal APer_NO As String, ByVal APer_Class As String, ByVal ADate As String) As Boolean  '保存班制信息至
        Dim Pec As New ProductionPiecePersonnelMothClassControl
        Dim pei As New ProductionPiecePersonnelMothClassInfo

        pei.Per_NO = APer_NO
        pei.Per_Class = APer_Class
        pei.Per_Date = Format(CDate(ADate), "yyyy/MM")

        Pec.ProductionPiecePersonnelMothClass_Add(pei)

    End Function

    ''' <summary>
    ''' 寫入 Excel中
    ''' </summary>
    ''' <param name="_Export_Excel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Export_Excel_SumPerNO(ByVal _Export_Excel As String) As Boolean

        If _Export_Excel = "" Then
            Exit Function
        End If
        '

        Dim i, n As Integer
        Dim arr(n) As String
        arr = Split(_Export_Excel, ",")
        n = Len(Replace(_Export_Excel, ",", "," & "*")) - Len(_Export_Excel)


        Dim exapp As New Excel.Application
        Dim exbook As Excel.Workbook
        Dim exsheet As Excel.Worksheet

        exapp = CreateObject("Excel.Application")
        exbook = exapp.Workbooks.Add
        exsheet = exapp.Worksheets(1)

        '廠証編號,姓名,班制,廠別編號,部門編號,產品編號,配件名稱,小工序名稱,數量,計件日期,備注
        exsheet.Cells(1, 1) = "'廠証編號"
        exsheet.Cells(1, 2) = "'姓名"
        exsheet.Cells(1, 3) = "'班制"
        exsheet.Cells(1, 4) = "'廠別編號"
        exsheet.Cells(1, 5) = "'部門編號"
        exsheet.Cells(1, 6) = "'產品編號"
        exsheet.Cells(1, 7) = "'配件名稱"

        exsheet.Cells(1, 8) = "'小工序名稱"
        exsheet.Cells(1, 9) = "'數量"
        exsheet.Cells(1, 10) = "'計件日期"
        exsheet.Cells(1, 11) = "'備注"


        For i = 0 To n - 1
            exsheet.Cells(i + 2, 1) = "'" & ds.Tables("Target").Rows(arr(i))("廠証編號").ToString
            exsheet.Cells(i + 2, 2) = "'" & ds.Tables("Target").Rows(arr(i))("姓名").ToString
            exsheet.Cells(i + 2, 3) = "'" & ds.Tables("Target").Rows(arr(i))("班制").ToString
            exsheet.Cells(i + 2, 4) = "'" & ds.Tables("Target").Rows(arr(i))("廠別編號").ToString
            exsheet.Cells(i + 2, 5) = "'" & ds.Tables("Target").Rows(arr(i))("部門編號").ToString
            exsheet.Cells(i + 2, 6) = "'" & ds.Tables("Target").Rows(arr(i))("產品編號").ToString
            exsheet.Cells(i + 2, 7) = "'" & ds.Tables("Target").Rows(arr(i))("配件名稱").ToString

            exsheet.Cells(i + 2, 8) = "'" & ds.Tables("Target").Rows(arr(i))("小工序名稱").ToString
            exsheet.Cells(i + 2, 9) = "'" & ds.Tables("Target").Rows(arr(i))("數量").ToString
            exsheet.Cells(i + 2, 10) = "'" & ds.Tables("Target").Rows(arr(i))("計件日期").ToString
            exsheet.Cells(i + 2, 11) = "'" & ds.Tables("Target").Rows(arr(i))("備注").ToString

            exsheet.Cells(i + 2, 12) = arr(i) & "----" & Export_Mark(i) '錯誤信息
        Next


        exapp.Visible = True

    End Function

#End Region

#Region "組別計件導入"

    Function SaveDataSumWG() As Boolean '個人計件數據
        SaveDataSumWG = True

        Dim MsgTextBoxStr As String = ""
        Dim OK_Bz As String = ""
        Dim Export_Excel As String = ""
        Dim Export_ExcelStr As String = ""
        Dim k As Integer

        If ds.Tables("Target").Rows.Count <= 0 Then
            MsgBox("請載入數據!")
            Exit Function
        End If

        ''先判斷時間是否有誤碼-------------------------------------------------------
        Dim i As Integer
        For i = 0 To ds.Tables("Target").Rows.Count - 1
            If ChcekDateA(ds.Tables("Target").Rows(i)("計件日期").ToString()) = False Then
                GridView1.FocusedRowHandle = i '移、至錯誤碼行
                MsgBox("第" & Str(i + 1) & "行,時間輸入有誤！")
                Exit Function
            End If
        Next
        ''-------------------------------------------------------


        Dim _G_NO, _G_Name, _depid, _facid, _PM_M_Code, _PM_Type, _PP_N_Name, _Date, _Qty, _Remark, _GP_factor As String
        Dim MsgStrA As String

        ProgressBar1.Visible = True
        ProgressBar1.Maximum = ds.Tables("Target").Rows.Count

        Export_Mark.Clear()

        For k = 0 To ds.Tables("Target").Rows.Count - 1
            OK_Bz = ""
            Export_ExcelStr = ""
            MsgStrA = ""

            ' strA = "組別編號,組別名稱,部門編號,廠別編號,產品編號,配件名稱,小工序名稱,數量,計件日期,備注"
            _G_NO = ds.Tables("Target").Rows(k)("組別編號").ToString()
            _G_Name = ds.Tables("Target").Rows(k)("組別名稱").ToString()
            _depid = ds.Tables("Target").Rows(k)("部門編號").ToString()
            _facid = ds.Tables("Target").Rows(k)("廠別編號").ToString()

            _PM_M_Code = ds.Tables("Target").Rows(k)("產品編號").ToString()
            _PM_Type = ds.Tables("Target").Rows(k)("配件名稱").ToString()
            _PP_N_Name = ds.Tables("Target").Rows(k)("小工序名稱").ToString()
            _Date = ds.Tables("Target").Rows(k)("計件日期").ToString()
            _Qty = ds.Tables("Target").Rows(k)("數量").ToString()

            _GP_factor = ds.Tables("Target").Rows(k)("承包系數").ToString()

            _Remark = ds.Tables("Target").Rows(k)("備注").ToString()

            If _G_NO = "" Or _G_Name = "" Or _depid = "" Or _PM_M_Code = "" Or _PM_Type = "" Or _PP_N_Name = "" Or _Qty = "" Or _Date = "" Or _facid = "" Or _GP_factor = "" Then
                MsgTextBoxStr = MsgTextBoxStr & k + 1 & "組別編號,組別名稱,部門編號,廠別編號,產品編號,配件名稱,小工序名稱,數量,計件日期,承包系數中一項輸入為空!" & vbCrLf
                Export_ExcelStr = "組別編號,組別名稱,部門編號,廠別編號,產品編號,配件名稱,小工序名稱,數量,計件日期,承包系數中一項輸入為空!"
            Else
                If ChcekDateA(_Date) = False Then
                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "時間格式有誤碼!" & vbCrLf
                    Export_ExcelStr = "時間格式有誤碼!"
                Else
                    If Check_G_NO(_G_NO) = False Then
                        MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & _G_NO & ", 組別編號不存在" & vbCrLf
                        Export_ExcelStr = "組別編號不存在"
                    Else
                        If CheckSumLock(_depid, _Date) = False Then

                            MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & _depid & ",當月部門數據已鎖定" & vbCrLf
                            Export_ExcelStr = "當月部門數據已鎖定"
                        Else
                            MsgStrA = Check_WGProcess("生產加工", _PM_M_Code, _PM_Type, _depid, _PP_N_Name, _Date, _G_NO)

                            If Mid(MsgStrA, 1, 1) <> "A" Then
                                MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & MsgStrA & vbCrLf
                                Export_ExcelStr = MsgStrA
                            Else
                                Dim gc As New ProductionSumPieceWorkGroupControl
                                Dim gi As New ProductionSumPieceWorkGroupInfo
                                Dim StrGP_NO_LS As String

                                gi.G_NO = _G_NO '組別

                                gi.Pro_Type = "生產加工"
                                gi.PM_M_Code = _PM_M_Code
                                gi.PM_Type = _PM_Type
                                gi.GP_Qty = Val(_Qty)   ''

                                gi.GP_Date = _Date
                                gi.GP_Action = InUserID

                                StrGP_NO_LS = ProductionSumPieceWorkGroupNO()

                                If StrGP_NO_LS <> "" Then
                                Else
                                    'MsgBox("流水號獲取失敗，請重試!")
                                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "流水號獲取失敗"
                                    Export_ExcelStr = "流水號獲取失敗"
                                    GoTo AA
                                End If

                                gi.GP_NO = StrGP_NO_LS  ''要先取得編號
                                ''-----------------------------------------------
                                Dim pc As New ProductionPieceWorkGroupControl
                                Dim pcil As New List(Of ProductionPieceWorkGroupInfo)
                                pcil = pc.ProductionPieceWorkGroup_GetList(_G_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                                gi.DepID = pcil(0).DepID
                                gi.FacID = pcil(0).FacID
                                ''根據AutoID查找----------------------------------------------------------------------------------

                                Dim pcc As New ProductionPieceProcessControl
                                Dim pci As List(Of ProductionPieceProcessInfo)

                                pci = pcc.ProductionPieceProcess_GetList(Mid(MsgStrA, 2, Len(MsgStrA) - 1), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                                If pci.Count <= 0 Then
                                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "計件工藝流程載入失敗"
                                    Export_ExcelStr = "計件工藝流程載入失敗"
                                    GoTo AA
                                End If

                                gi.PS_NO = pci(0).PS_NO.ToString  ''大工序編號
                                gi.PS_NameS = pci(0).PP_N_Name.ToString '小工序名稱

                                gi.GP_Price = pci(0).PP_Price.ToString   ''工價
                                ''autoID 也存一下
                                gi.PP_AutoID = Mid(MsgStrA, 2, Len(MsgStrA) - 1)
                                'gi.DepID = pci(0).DPT_ID ''部門

                                gi.GP_Remark = _Remark

                                gi.GP_factor = Val(_GP_factor)
                                ''---------------------------------------------------------------------------------------------------------
                                If gc.ProductionSumPieceWorkGroup_Add(gi) = True Then
                                    OK_Bz = "Y"
                                Else
                                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "保存失敗"
                                    Export_ExcelStr = "保存失敗"
                                End If
AA:

                            End If  '產品工藝
                        End If '當月鎖定
                    End If '員工名單
                End If '檢查時間格式
            End If '檢查是否為空

            If OK_Bz = "" Then
                Export_Excel = Export_Excel + Str(k) + ","
                Export_Mark.Add(Export_ExcelStr)
            End If

            ProgressBar1.Value = k
        Next
        If Export_Excel = "" Then
            MsgBox("數據全部保存成功!")
        Else
            MsgBox(MsgTextBoxStr)
            Export_Excel_SumWG(Export_Excel)
        End If
        ProgressBar1.Visible = False
    End Function

    Function ProductionSumPieceWorkGroupNO() As String

        ProductionSumPieceWorkGroupNO = ""

        Dim str1, str2 As String
        Dim gc1 As New ProductionSumPieceWorkGroupControl
        Dim gi1 As New ProductionSumPieceWorkGroupInfo

        str1 = Mid(Year(Now), 3)
        If CInt(Month(Now)) < 10 Then
            str2 = "0" & Month(Now)
        Else
            str2 = Month(Now)
        End If

        Dim Stra As String
        Stra = Trim(str1 & str2)

        gi1 = gc1.ProductionSumPieceWorkGroup_GetNO(Stra) '' 讀取基數

        If gi1 Is Nothing Then
            ProductionSumPieceWorkGroupNO = "GP" & str1 & str2 & "0000001"
        Else
            ProductionSumPieceWorkGroupNO = "GP" & str1 & str2 & Mid((CInt(Mid(gi1.GP_NO, 7)) + 10000001), 2)
        End If
    End Function


    ''' <summary>
    ''' 寫入 Excel中
    ''' </summary>
    ''' <param name="_Export_Excel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Export_Excel_SumWG(ByVal _Export_Excel As String) As Boolean

        If _Export_Excel = "" Then
            Exit Function
        End If
        '
        Dim i, n As Integer
        Dim arr(n) As String
        arr = Split(_Export_Excel, ",")
        n = Len(Replace(_Export_Excel, ",", "," & "*")) - Len(_Export_Excel)

        Dim exapp As New Excel.Application
        Dim exbook As Excel.Workbook
        Dim exsheet As Excel.Worksheet

        exapp = CreateObject("Excel.Application")
        exbook = exapp.Workbooks.Add
        exsheet = exapp.Worksheets(1)

        'strA = "組別編號,組別名稱,部門編號,廠別編號,產品編號,配件名稱,小工序名稱,數量,承包系數,計件日期,備注"
        exsheet.Cells(1, 1) = "'組別編號"
        exsheet.Cells(1, 2) = "'組別名稱"
        exsheet.Cells(1, 3) = "'部門編號"
        exsheet.Cells(1, 4) = "'廠別編號"

        exsheet.Cells(1, 5) = "'產品編號"
        exsheet.Cells(1, 6) = "'配件名稱"
        exsheet.Cells(1, 7) = "'小工序名稱"
        exsheet.Cells(1, 8) = "'數量"

        exsheet.Cells(1, 9) = "'承包系數"
        exsheet.Cells(1, 10) = "'計件日期"
        exsheet.Cells(1, 11) = "'備注"

        For i = 0 To n - 1
            exsheet.Cells(i + 2, 1) = "'" & ds.Tables("Target").Rows(arr(i))("組別編號").ToString
            exsheet.Cells(i + 2, 2) = "'" & ds.Tables("Target").Rows(arr(i))("組別名稱").ToString
            exsheet.Cells(i + 2, 3) = "'" & ds.Tables("Target").Rows(arr(i))("部門編號").ToString
            exsheet.Cells(i + 2, 4) = "'" & ds.Tables("Target").Rows(arr(i))("廠別編號").ToString

            exsheet.Cells(i + 2, 5) = "'" & ds.Tables("Target").Rows(arr(i))("產品編號").ToString
            exsheet.Cells(i + 2, 6) = "'" & ds.Tables("Target").Rows(arr(i))("配件名稱").ToString
            exsheet.Cells(i + 2, 7) = "'" & ds.Tables("Target").Rows(arr(i))("小工序名稱").ToString

            exsheet.Cells(i + 2, 8) = "'" & ds.Tables("Target").Rows(arr(i))("數量").ToString
            exsheet.Cells(i + 2, 9) = "'" & ds.Tables("Target").Rows(arr(i))("承包系數").ToString
            exsheet.Cells(i + 2, 10) = "'" & ds.Tables("Target").Rows(arr(i))("計件日期").ToString
            exsheet.Cells(i + 2, 11) = "'" & ds.Tables("Target").Rows(arr(i))("備注").ToString

            exsheet.Cells(i + 2, 12) = arr(i) & "----" & Export_Mark(i) '錯誤信息
        Next

        exapp.Visible = True

    End Function

#End Region

#Region "個人計時導入"

    Function SaveDataSumPerTime() As Boolean '個人計時數據
        SaveDataSumPerTime = True

        Dim MsgTextBoxStr As String = ""
        Dim OK_Bz As String = ""
        Dim Export_Excel As String = ""
        Dim Export_ExcelStr As String = ""
        Dim k As Integer

        If ds.Tables("Target").Rows.Count <= 0 Then
            MsgBox("請載入數據!")
            Exit Function
        End If

        ''先判斷時間是否有誤碼-------------------------------------------------------
        Dim i As Integer
        For i = 0 To ds.Tables("Target").Rows.Count - 1
            If ChcekDateB(ds.Tables("Target").Rows(i)("記錄日期").ToString(), ds.Tables("Target").Rows(i)("開始時間").ToString(), ds.Tables("Target").Rows(i)("結束時間").ToString()) = False Then
                GridView1.FocusedRowHandle = i '移、至錯誤碼行
                MsgBox("第" & Str(i + 1) & "行,時間輸入有誤！")
                Exit Function
            End If
        Next
        ''-------------------------------------------------------

        Dim _Per_no, _depid, _facid, _STime, _ETime, _TatalTime, _Date, _Remark As String
        Dim MsgStrA As String

        ProgressBar1.Visible = True
        ProgressBar1.Maximum = ds.Tables("Target").Rows.Count

        Export_Mark.Clear()

        For k = 0 To ds.Tables("Target").Rows.Count - 1
            OK_Bz = ""
            Export_ExcelStr = ""
            MsgStrA = ""

            'strA = "廠証編號,姓名,部門,廠別,開始時間,結束時間,合計時長,記錄日期,備注"
            _Per_no = ds.Tables("Target").Rows(k)("廠証編號").ToString()
            _depid = ds.Tables("Target").Rows(k)("部門編號").ToString()
            _facid = ds.Tables("Target").Rows(k)("廠別編號").ToString()

            _STime = ds.Tables("Target").Rows(k)("開始時間").ToString()
            _ETime = ds.Tables("Target").Rows(k)("結束時間").ToString()
            _TatalTime = ds.Tables("Target").Rows(k)("合計時長").ToString()
            _Date = ds.Tables("Target").Rows(k)("記錄日期").ToString()

            _Remark = ds.Tables("Target").Rows(k)("備注").ToString()

            If _Per_no = "" Or _depid = "" Or _STime = "" Or _ETime = "" Or _TatalTime = "" Or _Date = "" Or _facid = "" Then
                MsgTextBoxStr = MsgTextBoxStr & k + 1 & "廠証編號,部門編號,廠別編號,開始時間,結束時間,合計時長,記錄日期中一項輸入為空!" & vbCrLf
                Export_ExcelStr = "廠証編號,部門編號,廠別編號,開始時間,結束時間,合計時長,記錄日期中一項輸入為空！"
            Else
                If ChcekDateB(_Date, _STime, _ETime) = False Then
                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "時間格式有誤碼!" & vbCrLf
                    Export_ExcelStr = "時間格式有誤碼!"
                Else
                    If Check_Piece_PerNO(_Per_no) = False Then
                        MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & _Per_no & ", 員工名單中不存在" & vbCrLf
                        Export_ExcelStr = "員工名單中不存在"
                    Else
                        If CheckSumLock(_depid, _Date) = False Then
                            MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & _depid & ",當月部門數據已鎖定" & vbCrLf
                            Export_ExcelStr = "當月部門數據已鎖定"
                        Else
                            MsgStrA = CHECK_TimePer(_Per_no, _Date, _STime, _ETime)

                            If MsgStrA <> "" Then
                                MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & MsgStrA & vbCrLf
                                Export_ExcelStr = MsgStrA
                            Else
                                Dim gc As New ProductionSumTimePersonnelControl

                                Dim gi As New ProductionSumTimePersonnelInfo

                                Dim LabPT_NO As String
                                LabPT_NO = GetProductionSumTimePersonnelNO()

                                If LabPT_NO <> "" Then
                                Else
                                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "流水號獲取失敗"
                                    Export_ExcelStr = "流水號獲取失敗"
                                    GoTo AAAA
                                End If

                                gi.PT_NO = LabPT_NO  ''要先取得編號
                                gi.PT_Date = _Date  ''統計時間

                                gi.Per_NO = _Per_no '員工工號
                                gi.PT_BeginTime = _STime
                                gi.PT_EndTime = _ETime

                                gi.PT_Total = FormatNumber(Val(_TatalTime), 2, TriState.True) '合計
                                gi.PT_Remark = _Remark
                                gi.PT_Action = InUserID

                                gi.DepID = _depid    ''部門編號
                                gi.FacID = _facid   '廠別 

                                If gc.ProductionSumTimePersonnel_Add(gi) = True Then
                                    OK_Bz = "Y"
                                Else
                                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "保存失敗"
                                    Export_ExcelStr = "保存失敗"
                                End If
AAAA:
                            End If  '檢查時間段重復
                        End If '當月鎖定
                    End If '員工名單
                End If '檢查時間格式
            End If '檢查是否為空

            If OK_Bz = "" Then
                Export_Excel = Export_Excel + Str(k) + ","
                Export_Mark.Add(Export_ExcelStr)
            End If

            ProgressBar1.Value = k
        Next
        If Export_Excel = "" Then
            MsgBox("數據全部保存成功!")
        Else
            MsgBox(MsgTextBoxStr)
            Export_Excel_SumPerTime(Export_Excel)
        End If
        ProgressBar1.Visible = False
    End Function

    ''' <summary>
    ''' 獲取流水號基數
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetProductionSumTimePersonnelNO() As String
        GetProductionSumTimePersonnelNO = ""

        Dim str1, str2 As String
        Dim gc1 As New ProductionSumTimePersonnelControl
        Dim gi1 As New ProductionSumTimePersonnelInfo

        str1 = Mid(Year(Now), 3)
        If CInt(Month(Now)) < 10 Then
            str2 = "0" & Month(Now)
        Else
            str2 = Month(Now)
        End If

        Dim Stra As String
        Stra = Trim(str1 & str2)

        gi1 = gc1.ProductionSumTimePersonnel_GetNO(Stra) '' 讀取基數

        If gi1 Is Nothing Then
            GetProductionSumTimePersonnelNO = "PT" & str1 & str2 & "0000001"
        Else
            GetProductionSumTimePersonnelNO = "PT" & str1 & str2 & Mid((CInt(Mid(gi1.PT_NO, 7)) + 10000001), 2)
        End If


    End Function

    ''' <summary>
    ''' 寫入 Excel中
    ''' </summary>
    ''' <param name="_Export_Excel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Export_Excel_SumPerTime(ByVal _Export_Excel As String) As Boolean



        If _Export_Excel = "" Then
            Exit Function
        End If
        '
        Dim i, n As Integer
        Dim arr(n) As String
        arr = Split(_Export_Excel, ",")
        n = Len(Replace(_Export_Excel, ",", "," & "*")) - Len(_Export_Excel)

        Dim exapp As New Excel.Application
        Dim exbook As Excel.Workbook
        Dim exsheet As Excel.Worksheet

        exapp = CreateObject("Excel.Application")
        exbook = exapp.Workbooks.Add
        exsheet = exapp.Worksheets(1)

        'strA = "廠証編號,姓名,部門編號,廠別編號,開始時間,結束時間,合計時長,記錄日期,備注"
        exsheet.Cells(1, 1) = "'廠証編號"
        exsheet.Cells(1, 2) = "'姓名"
        exsheet.Cells(1, 3) = "'部門編號"
        exsheet.Cells(1, 4) = "'廠別編號"

        exsheet.Cells(1, 5) = "'開始時間"
        exsheet.Cells(1, 6) = "'結束時間"
        exsheet.Cells(1, 7) = "'合計時長"
        exsheet.Cells(1, 8) = "'記錄日期"

        exsheet.Cells(1, 9) = "'備注"

        For i = 0 To n - 1
            exsheet.Cells(i + 2, 1) = "'" & ds.Tables("Target").Rows(arr(i))("廠証編號").ToString
            exsheet.Cells(i + 2, 2) = "'" & ds.Tables("Target").Rows(arr(i))("姓名").ToString
            exsheet.Cells(i + 2, 3) = "'" & ds.Tables("Target").Rows(arr(i))("部門編號").ToString
            exsheet.Cells(i + 2, 4) = "'" & ds.Tables("Target").Rows(arr(i))("廠別編號").ToString

            exsheet.Cells(i + 2, 5) = "'" & ds.Tables("Target").Rows(arr(i))("開始時間").ToString
            exsheet.Cells(i + 2, 6) = "'" & ds.Tables("Target").Rows(arr(i))("結束時間").ToString
            exsheet.Cells(i + 2, 7) = "'" & ds.Tables("Target").Rows(arr(i))("合計時長").ToString

            exsheet.Cells(i + 2, 8) = "'" & ds.Tables("Target").Rows(arr(i))("記錄日期").ToString
            exsheet.Cells(i + 2, 9) = "'" & ds.Tables("Target").Rows(arr(i))("備注").ToString

            exsheet.Cells(i + 2, 10) = arr(i) & "----" & Export_Mark(i) '錯誤信息
        Next

        exapp.Visible = True

    End Function


#End Region

#Region "組別計時導入"
    Function SaveDataSumWGTime() As Boolean '組別計時數據
        SaveDataSumWGTime = True

        Dim MsgTextBoxStr As String = ""
        Dim OK_Bz As String = ""
        Dim Export_Excel As String = ""
        Dim Export_ExcelStr As String = ""
        Dim k As Integer

        If ds.Tables("Target").Rows.Count <= 0 Then
            MsgBox("請載入數據!")
            Exit Function
        End If

        ''先判斷時間是否有誤碼-------------------------------------------------------
        Dim i As Integer
        For i = 0 To ds.Tables("Target").Rows.Count - 1
            If ChcekDateB(ds.Tables("Target").Rows(i)("記錄日期").ToString(), ds.Tables("Target").Rows(i)("開始時間").ToString(), ds.Tables("Target").Rows(i)("結束時間").ToString()) = False Then
                GridView1.FocusedRowHandle = i '移、至錯誤碼行
                MsgBox("第" & Str(i + 1) & "行,時間輸入有誤！")
                Exit Function
            End If
        Next
        ''-------------------------------------------------------

        Dim _Per_no, _G_NO, _depid, _facid, _STime, _ETime, _TatalTime, _Date, _Remark As String
        'strA = " 廠証編號,姓名,組別,組別編號,部門,廠別,開始時間,結束時間,合計時長,記錄日期,備注"

        Dim MsgStrA As String

        ProgressBar1.Visible = True
        ProgressBar1.Maximum = ds.Tables("Target").Rows.Count

        Export_Mark.Clear()

        For k = 0 To ds.Tables("Target").Rows.Count - 1
            OK_Bz = ""
            Export_ExcelStr = ""
            MsgStrA = ""

            'strA = " 廠証編號,姓名,組別,組別編號,部門,廠別,開始時間,結束時間,合計時長,記錄日期,備注"
            _Per_no = ds.Tables("Target").Rows(k)("廠証編號").ToString()
            _G_NO = ds.Tables("Target").Rows(k)("組別編號").ToString()

            _depid = ds.Tables("Target").Rows(k)("部門編號").ToString()
            _facid = ds.Tables("Target").Rows(k)("廠別編號").ToString()

            _STime = ds.Tables("Target").Rows(k)("開始時間").ToString()
            _ETime = ds.Tables("Target").Rows(k)("結束時間").ToString()
            _TatalTime = ds.Tables("Target").Rows(k)("合計時長").ToString()
            _Date = ds.Tables("Target").Rows(k)("記錄日期").ToString()

            _Remark = ds.Tables("Target").Rows(k)("備注").ToString()

            If _G_NO = "" Or _Per_no = "" Or _depid = "" Or _STime = "" Or _ETime = "" Or _TatalTime = "" Or _Date = "" Or _facid = "" Then
                MsgTextBoxStr = MsgTextBoxStr & k + 1 & "組別編號,廠証編號,部門編號,廠別編號,開始時間,結束時間,合計時長,記錄日期中一項輸入為空!" & vbCrLf
                Export_ExcelStr = "組別編號,廠証編號,部門編號,廠別編號,開始時間,結束時間,合計時長,記錄日期中一項輸入為空！"
            Else
                If ChcekDateB(_Date, _STime, _ETime) = False Then
                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "時間格式有誤碼!" & vbCrLf
                    Export_ExcelStr = "時間格式有誤碼!"
                Else
                    If Check_Piece_PerNO_GNO(_Per_no, _G_NO) = False Then
                        MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & _Per_no & ", 此員工不在當前組別中" & vbCrLf
                        Export_ExcelStr = "此員工不在當前組別中"
                    Else
                        If CheckSumLock(_depid, _Date) = False Then
                            MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & _depid & ",當月部門數據已鎖定" & vbCrLf
                            Export_ExcelStr = "當月部門數據已鎖定"
                        Else
                            MsgStrA = CHECK_TimeWG(_Per_no, _Date, _STime, _ETime)

                            If MsgStrA <> "" Then
                                MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & MsgStrA & vbCrLf
                                Export_ExcelStr = MsgStrA
                            Else
                                Dim gc As New ProductionSumTimeWorkGroupControl
                                Dim gi As New ProductionSumTimeWorkGroupInfo

                                Dim LabGT_NO As String
                                LabGT_NO = GetProductionSumTimeWorkGroupNO()

                                If LabGT_NO <> "" Then
                                Else
                                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "流水號獲取失敗"
                                    Export_ExcelStr = "流水號獲取失敗"
                                    GoTo AAAAA
                                End If

                                gi.GT_NO = LabGT_NO  ''要先取得編號
                                gi.GT_Date = _Date  ''統計時間

                                gi.Per_NO = _Per_no '員工工號
                                gi.GT_BeginTime = _STime
                                gi.GT_EndTime = _ETime

                                gi.G_NO = _G_NO ''組別
                                gi.GT_Total = FormatNumber(Val(_TatalTime), 2, TriState.True) '合計
                                gi.GT_Remark = _Remark '備注

                                gi.GT_Action = InUserID

                                Dim pc As New ProductionPieceWorkGroupControl
                                Dim pcil As New List(Of ProductionPieceWorkGroupInfo)
                                pcil = pc.ProductionPieceWorkGroup_GetList(_G_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                                If pcil.Count <= 0 Then
                                    Exit Function
                                End If
                                gi.DepID = pcil(0).DepID
                                gi.FacID = pcil(0).FacID

                                If gc.ProductionSumTimeWorkGroup_Add(gi) = True Then
                                    OK_Bz = "Y"
                                Else
                                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "保存失敗"
                                    Export_ExcelStr = "保存失敗"
                                End If
AAAAA:
                            End If  '檢查時間段重復
                        End If '當月鎖定
                    End If '員工名單
                End If '檢查時間格式
            End If '檢查是否為空

            If OK_Bz = "" Then
                Export_Excel = Export_Excel + Str(k) + ","
                Export_Mark.Add(Export_ExcelStr)
            End If

            ProgressBar1.Value = k
        Next
        If Export_Excel = "" Then
            MsgBox("數據全部保存成功!")
        Else
            MsgBox(MsgTextBoxStr)
            Export_Excel_SumWGTime(Export_Excel)
        End If
        ProgressBar1.Visible = False
    End Function

    ''' <summary>
    ''' 獲取流水號基數
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetProductionSumTimeWorkGroupNO() As String
        GetProductionSumTimeWorkGroupNO = ""

        Dim str1, str2 As String
        Dim gc1 As New ProductionSumTimeWorkGroupControl
        Dim gi1 As New ProductionSumTimeWorkGroupInfo

        str1 = Mid(Year(Now), 3)
        If CInt(Month(Now)) < 10 Then
            str2 = "0" & Month(Now)
        Else
            str2 = Month(Now)
        End If

        Dim Stra As String
        Stra = Trim(str1 & str2)

        gi1 = gc1.ProductionSumTimeWorkGroup_GetNO(Stra) '' 讀取基數

        If gi1 Is Nothing Then
            GetProductionSumTimeWorkGroupNO = "GT" & str1 & str2 & "0000001"
        Else
            GetProductionSumTimeWorkGroupNO = "GT" & str1 & str2 & Mid((CInt(Mid(gi1.GT_NO, 7)) + 10000001), 2)
        End If

    End Function

    ''' <summary>
    ''' 寫入 Excel中
    ''' </summary>
    ''' <param name="_Export_Excel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Export_Excel_SumWGTime(ByVal _Export_Excel As String) As Boolean

        If _Export_Excel = "" Then
            Exit Function
        End If
        '
        Dim i, n As Integer
        Dim arr(n) As String
        arr = Split(_Export_Excel, ",")
        n = Len(Replace(_Export_Excel, ",", "," & "*")) - Len(_Export_Excel)

        Dim exapp As New Excel.Application
        Dim exbook As Excel.Workbook
        Dim exsheet As Excel.Worksheet

        exapp = CreateObject("Excel.Application")
        exbook = exapp.Workbooks.Add
        exsheet = exapp.Worksheets(1)

        'strA = " 廠証編號,姓名,組別,組別編號,部門,廠別,開始時間,結束時間,合計時長,記錄日期,備注"
        exsheet.Cells(1, 1) = "'廠証編號"
        exsheet.Cells(1, 2) = "'姓名"
        exsheet.Cells(1, 3) = "'組別"
        exsheet.Cells(1, 4) = "'組別編號"

        exsheet.Cells(1, 5) = "'部門編號"
        exsheet.Cells(1, 6) = "'廠別編號"

        exsheet.Cells(1, 7) = "'開始時間"
        exsheet.Cells(1, 8) = "'結束時間"
        exsheet.Cells(1, 9) = "'合計時長"
        exsheet.Cells(1, 10) = "'記錄日期"

        exsheet.Cells(1, 11) = "'備注"

        For i = 0 To n - 1
            exsheet.Cells(i + 2, 1) = "'" & ds.Tables("Target").Rows(arr(i))("廠証編號").ToString
            exsheet.Cells(i + 2, 2) = "'" & ds.Tables("Target").Rows(arr(i))("姓名").ToString

            exsheet.Cells(i + 2, 3) = "'" & ds.Tables("Target").Rows(arr(i))("組別").ToString
            exsheet.Cells(i + 2, 4) = "'" & ds.Tables("Target").Rows(arr(i))("組別編號").ToString

            exsheet.Cells(i + 2, 5) = "'" & ds.Tables("Target").Rows(arr(i))("部門編號").ToString
            exsheet.Cells(i + 2, 6) = "'" & ds.Tables("Target").Rows(arr(i))("廠別編號").ToString

            exsheet.Cells(i + 2, 7) = "'" & ds.Tables("Target").Rows(arr(i))("開始時間").ToString
            exsheet.Cells(i + 2, 8) = "'" & ds.Tables("Target").Rows(arr(i))("結束時間").ToString
            exsheet.Cells(i + 2, 9) = "'" & ds.Tables("Target").Rows(arr(i))("合計時長").ToString

            exsheet.Cells(i + 2, 10) = "'" & ds.Tables("Target").Rows(arr(i))("記錄日期").ToString
            exsheet.Cells(i + 2, 11) = "'" & ds.Tables("Target").Rows(arr(i))("備注").ToString

            exsheet.Cells(i + 2, 12) = arr(i) & "----" & Export_Mark(i) '錯誤信息
        Next

        exapp.Visible = True

    End Function

#End Region

    Private Sub QuitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitButton.Click
        Me.Close()
    End Sub
End Class