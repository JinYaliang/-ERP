Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports LFERP.DataSetting
Imports LFERP.Library.ProductionPieceWorkGroup
Imports LFERP.Library.ProductionPiecePersonnel

Imports LFERP.Library.ProductionPiecePayWGMain
Imports LFERP.Library.ProductionPiecePayWGSub


Public Class frmProductionPiecePayGroupLoad

    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim Export_Mark As New ArrayList


    Sub CreateTable()
        '創建類型表
        With ds.Tables.Add("Target")
            .Columns.Add("編號", GetType(String))
            .Columns.Add("組別編號", GetType(String))
            .Columns.Add("工號", GetType(String))
            .Columns.Add("工時比例", GetType(String))
            .Columns.Add("年月", GetType(String))
        End With

        GridControl1.DataSource = ds.Tables("Target")
    End Sub
    ''' <summary>
    ''' 對 ds 表處理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function PieceWGRate() As Boolean

        Dim i As Integer
        Dim RowsCount As Integer

        RowsCount = ds1.Tables(0).Rows.Count

        If RowsCount > 0 Then

            ds.Tables("Target").Clear()

            For i = 1 To RowsCount - 1
                Dim row As DataRow
                row = ds.Tables("Target").NewRow
                row("編號") = i
                row("組別編號") = Trim(ds1.Tables(0).Rows(i)(0).ToString)
                row("工號") = Trim(ds1.Tables(0).Rows(i)(1).ToString)
                row("工時比例") = Trim(ds1.Tables(0).Rows(i)(2).ToString)
                row("年月") = Trim(ds1.Tables(0).Rows(i)(3).ToString)
                ds.Tables("Target").Rows.Add(row)
            Next

        End If

    End Function


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

            PieceWGRate()
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
    ''' 檢查人員是否已存在，計件人員名單中
    ''' </summary>
    ''' <param name="_Per_NO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Check_Piece_PerNO(ByVal _Per_NO As String) As Boolean
        Dim pc As New ProductionPiecePersonnelControl
        Dim pl As New List(Of ProductionPiecePersonnelInfo)
        pl = pc.ProductionPiecePersonnel_GetList(Nothing, _Per_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing)

        If pl.Count > 0 Then
            Check_Piece_PerNO = True
        Else
            Check_Piece_PerNO = False
        End If
    End Function

    Function Check_yyyyMM(ByVal yyyyMM As String) As Boolean
        Check_yyyyMM = True
        Try
            If Len(Format(CDate(yyyyMM), "yyyy/MM")) = 7 Then
            Else
                Check_yyyyMM = False
            End If
        Catch ex As Exception
            Check_yyyyMM = False
        End Try
    End Function
    '



    Function SaveData() As Boolean
        SaveData = True

        Dim MsgTextBoxStr As String = ""
        Dim OK_Bz As String = ""
        Dim Export_Excel As String = ""
        Dim Export_ExcelStr As String = ""
        Dim k As Integer

        If ds.Tables("Target").Rows.Count <= 0 Then
            MsgBox("請載入數據!")
            Exit Function
        End If

        ProgressBar1.Visible = True
        ProgressBar1.Maximum = ds.Tables("Target").Rows.Count

        Export_Mark.Clear()

        For k = 0 To ds.Tables("Target").Rows.Count - 1
            OK_Bz = ""
            Export_ExcelStr = ""

            If ds.Tables("Target").Rows(k)("組別編號").ToString() = "" Or ds.Tables("Target").Rows(k)("工號").ToString() = "" Or ds.Tables("Target").Rows(k)("工時比例").ToString() = "" Or _
               ds.Tables("Target").Rows(k)("年月").ToString() = "" Then
                MsgTextBoxStr = MsgTextBoxStr & k + 1 & "組別編號,工號,工時比例,年月中一項輸入為空!" & vbCrLf
                Export_ExcelStr = "組別編號,工號,工時比例,年月中一項輸入為空!"
            Else
                If Check_yyyyMM(ds.Tables("Target").Rows(k)("年月").ToString()) = False Then
                    MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & ds.Tables("Target").Rows(k)("年月").ToString() & ",年月格式錯誤" & vbCrLf
                    Export_ExcelStr = "年月格式錯誤"
                Else
                    If Check_G_NO(ds.Tables("Target").Rows(k)("組別編號").ToString()) = False Then
                        MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & ds.Tables("Target").Rows(k)("組別編號").ToString() & ",組別編號不存在" & vbCrLf
                        Export_ExcelStr = "組別編號不存在"
                    Else
                        If Check_Piece_PerNO(ds.Tables("Target").Rows(k)("工號").ToString()) = False Then
                            MsgTextBoxStr = MsgTextBoxStr & k + 1 & "行" & ds.Tables("Target").Rows(k)("工號").ToString() & ",此計件人員名單已辭工或不存在" & vbCrLf
                            Export_ExcelStr = "此計件人員名單已辭工或不存在!"
                        Else
                            Dim strLog As String
                            strLog = Write_PeiceMGRate(k)

                            If strLog = "" Then
                                OK_Bz = "Y"
                            Else
                                MsgTextBoxStr = MsgTextBoxStr & k + 1 & strLog & vbCrLf
                                Export_ExcelStr = "保存失敗"
                            End If
                        End If
                    End If
                End If

                End If

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
            Export_Excle(Export_Excel)
        End If
        ProgressBar1.Visible = False
    End Function

    ''' <summary>
    ''' 保存數據
    ''' </summary>
    ''' <param name="_Row"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Write_PeiceMGRate(ByVal _Row As Integer) As String

        '.Columns.Add("編號", GetType(String))
        '.Columns.Add("組別編號", GetType(String))
        '.Columns.Add("工號", GetType(String))
        '.Columns.Add("工時比例", GetType(String))
        '.Columns.Add("年月", GetType(String))

        Write_PeiceMGRate = ""

        Dim stryyyyMM As String
        Dim strG_NO As String
        Dim strPer_NO As String
        Dim strRate As String

        strG_NO = ds.Tables("Target").Rows(_Row)("組別編號").ToString
        stryyyyMM = Format(CDate(ds.Tables("Target").Rows(_Row)("年月").ToString), "yyyy/MM")

        strPer_NO = ds.Tables("Target").Rows(_Row)("工號").ToString
        strRate = ds.Tables("Target").Rows(_Row)("工時比例").ToString

        Dim gc As New ProductionPiecePayWGMainControl
        Dim gl As New List(Of ProductionPiecePayWGMainInfo)

        gl = gc.ProductionPiecePayWGMain_GetList(Nothing, Nothing, strG_NO, stryyyyMM, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False)

        If gl.Count = 0 Then
            Write_PeiceMGRate = "此組別當月薪金已審核或未匯總"
            Exit Function
        End If

        ''保存子表----------------------------------------------------------
        Dim gcs As New ProductionPiecePayWGSubControl
        Dim gci As New ProductionPiecePayWGSubInfo

        gci.PY_ID = gl(0).PY_ID
        gci.Per_NO = strPer_NO
        gci.PYS_Proportion = strRate

        If gcs.ProductionPiecePayWGSub_UpdateRat(gci) = True Then
        Else
            Write_PeiceMGRate = "保存失敗"
        End If

        ''--------------------------------

    End Function

    ''' <summary>
    ''' 寫入 Excel中
    ''' </summary>
    ''' <param name="_Export_Excel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Export_Excle(ByVal _Export_Excel As String) As Boolean

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



        exsheet.Cells(1, 1) = "組別編號"
        exsheet.Cells(1, 2) = "工號"
        exsheet.Cells(1, 3) = "工時比例"
        exsheet.Cells(1, 4) = "年月"

        For i = 0 To n - 1
            exsheet.Cells(i + 2, 1) = ds.Tables("Target").Rows(arr(i))("組別編號").ToString
            exsheet.Cells(i + 2, 2) = ds.Tables("Target").Rows(arr(i))("工號").ToString
            exsheet.Cells(i + 2, 3) = ds.Tables("Target").Rows(arr(i))("工時比例").ToString
            exsheet.Cells(i + 2, 4) = ds.Tables("Target").Rows(arr(i))("年月").ToString
            exsheet.Cells(i + 2, 8) = Export_Mark(i) '錯誤信息
        Next


        exapp.Visible = True

    End Function




    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        SaveData()
    End Sub

    Private Sub QuitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitButton.Click
        Me.Close()
    End Sub

    Private Sub frmProductionPiecePayGroupLoad_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
    End Sub
End Class