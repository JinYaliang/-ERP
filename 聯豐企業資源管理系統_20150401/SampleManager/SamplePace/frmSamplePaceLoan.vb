Imports LFERP.Library.SampleManager.SamplePace
Imports LFERP.Library.SampleManager.SampleOrdersSub
Imports LFERP.Library.PieceProcess
Imports System.Threading
Public Class frmSamplePaceLoan
#Region "屬性"
    Dim ds As New DataSet
    Dim pncon As New PersonnelControl
    Private _SO_IDItem As String
    Private _Code_IDItem As String
    Private _StartDate As String
    Private _EndtDate As String
    Private _D_ID As String
    Property SO_IDItem() As String '属性
        Get
            Return _SO_IDItem
        End Get
        Set(ByVal value As String)
            _SO_IDItem = value
        End Set
    End Property
    Property Code_IDItem() As String '属性
        Get
            Return _Code_IDItem
        End Get
        Set(ByVal value As String)
            _Code_IDItem = value
        End Set
    End Property
    Property StartDate() As String '属性
        Get
            Return _StartDate
        End Get
        Set(ByVal value As String)
            _StartDate = value
        End Set
    End Property
    Property EndtDate() As String '属性
        Get
            Return _EndtDate
        End Get
        Set(ByVal value As String)
            _EndtDate = value
        End Set
    End Property
    Property D_ID() As String '属性
        Get
            Return _D_ID
        End Get
        Set(ByVal value As String)
            _D_ID = value
        End Set
    End Property
#End Region
    ''' <summary>
    ''' 加載事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSamplePaceLoan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTables()
        Dim pclist As New List(Of LFERP.Library.ProductionController.ProductionFieldControlInfo)
        Dim pminfo As New LFERP.Library.ProductionController.ProductionFieldControlInfo
        pminfo.DepName = "全部"
        pminfo.ControlDep = "All"

        Dim fc As New LFERP.Library.ProductionController.ProductionFieldControl
        gluSE_OutD_ID.Properties.DisplayMember = "DepName"
        gluSE_OutD_ID.Properties.ValueMember = "ControlDep"
        pclist = fc.ProductionFieldControl_GetList(InUserID, Nothing)
        pclist.Insert(0, pminfo)
        gluSE_OutD_ID.Properties.DataSource = pclist 'fc.ProductionFieldControl_GetList(InUserID, Nothing)

        gluSE_OutD_ID.EditValue = D_ID
        dteStartDate.EditValue = CDate(Format(DateAdd("M", -1, Now()), "yyyy/MM/dd"))
        dteEndDate.Text = Format(Now, "yyyy/MM/dd")
    End Sub
    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("SampLoan") '子配件表
            .Columns.Add("SE_ID", GetType(String))
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("Qty", GetType(String))
            .Columns.Add("SE_Type", GetType(String))
        End With
        GridControl2.DataSource = ds.Tables("SampLoan")
    End Sub

    ''' <summary>
    ''' 查詢
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Dim strDep As String = Nothing
        If gluSE_OutD_ID.EditValue = "All" Then
            strDep = Nothing
        Else
            strDep = gluSE_OutD_ID.EditValue
        End If

        Dim spCon As New SamplePaceControler
        GridControl1.DataSource = spCon.SamplePace_Getlist2(Nothing, Nothing, Nothing, Nothing, Nothing, dteStartDate.EditValue, dteEndDate.EditValue, True, Nothing, strDep, Nothing, True, True)


        If GridView2.RowCount > 0 Then
            ds.Tables("SampLoan").Clear()
            Dim i As Integer
            For i = 0 To GridView2.RowCount - 1
                Dim strSE_ID As String = GridView2.GetRowCellValue(i, Me.SE_ID)
                Dim spblist As List(Of SamplePaceInfo)
                spblist = spCon.SamplePaceBarCode_Getlist(Nothing, Nothing, strSE_ID)
                Dim m As Integer
                For m = 0 To spblist.Count - 1
                    Dim row As DataRow
                    row = ds.Tables("SampLoan").NewRow
                    row("SE_ID") = spblist(m).SE_ID
                    row("Code_ID") = spblist(m).Code_ID
                    row("Qty") = spblist(m).Qty
                    row("SE_Type") = spblist(m).SE_Type
                    ds.Tables("SampLoan").Rows.Add(row)
                Next
            Next
        Else
            'GridControl2.DataSource = Nothing
        End If
    End Sub
    ''' <summary>
    ''' 列印
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        'On Error Resume Next
        'If GetParameter() Then
        '    Dim ds As New DataSet
        '    If GridView2.RowCount = 0 Then Exit Sub
        '    Dim ltc As New CollectionToDataSet
        '    Dim SPQC As New SamplePaceQueryController
        '    ds.Tables.Clear()
        '    ltc.CollToDataSet(ds, "SamplePaceQuery", SPQC.SamplePaceQuery_Getlist(SO_IDItem, Nothing, Code_IDItem, Nothing, StartDate, EndtDate, chkSE_InCheck.Checked))
        '    PreviewRPT(ds, "rptSamplePaceQuery", "SamplePaceQuery", True, True)
        '    ltc = Nothing
        'End If
    End Sub
    ''' <summary>
    ''' 導出Excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        If GridView2.RowCount > 0 Then
            If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim filePath As String
                filePath = FolderBrowserDialog1.SelectedPath
                filePath += "\YourExcel1.xls"
                GridView2.ExportToXls(filePath)
                Process.Start(filePath)
            End If
        End If
    End Sub

    Private Sub GridView2_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        Dim spCon As New SamplePaceControler
        Dim strSE_ID As String = GridView2.GetFocusedRowCellValue("SE_ID")
        Me.GridView1.ActiveFilterString = "[SE_ID]='" + strSE_ID + "'"
    End Sub

    Private Sub cmdExcelB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelB.Click
        If GridView1.RowCount > 0 Then
            If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim filePath As String
                filePath = FolderBrowserDialog1.SelectedPath
                filePath += "\YourExcel2.xls"
                GridView1.ExportToXls(filePath)
                Process.Start(filePath)
            End If
        End If
    End Sub
End Class