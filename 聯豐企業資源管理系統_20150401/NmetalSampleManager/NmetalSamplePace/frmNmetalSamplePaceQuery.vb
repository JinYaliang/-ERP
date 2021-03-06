Imports LFERP.Library.NmetalSampleManager.NmetalSamplePace
Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersMain
Imports System.Threading
Public Class frmNmetalSamplePaceQuery
#Region "屬性"

    Private _SO_IDItem As String
    Private _Code_IDItem As String
    Private _StartDate As String
    Private _EndtDate As String
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

#End Region
    ''' <summary>
    ''' 加載事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSamplePaceQuery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim SOS As New NmetalSampleOrdersMainControler
        gueSO_ID.Properties.DisplayMember = "SO_ID"
        gueSO_ID.Properties.ValueMember = "SO_ID"
        gueSO_ID.Properties.DataSource = SOS.NmetalSampleOrdersMain_GetListItem(Nothing, Nothing, Nothing, True)
        dteStartDate.EditValue = CDate(Format(DateAdd("M", -1, Now()), "yyyy/MM/dd"))
        dteEndDate.Text = Format(Now, "yyyy/MM/dd")
    End Sub
    ''' <summary>
    ''' 點擊獲得條碼信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView3.Click
        Dim SPBC As New NmetalSamplePaceQueryController
        gueCode_ID.Properties.DisplayMember = "Code_ID"
        gueCode_ID.Properties.ValueMember = "Code_ID"
        Dim objInfo As New NmetalSamplePaceQueryInfo
        objInfo.Code_ID = "全部"
        Dim FeatureList As New List(Of NmetalSamplePaceQueryInfo)
        FeatureList = SPBC.NmetalSamplePaceBarCode_GetList(GridView3.GetFocusedRowCellValue("Code_ID"))
        FeatureList.Insert(0, objInfo)
        gueCode_ID.Properties.DataSource = FeatureList
    End Sub
    ''' <summary>
    ''' 查詢
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Dim SPQC As New NmetalSamplePaceQueryController
        If GetParameter() Then
            GridControl1.DataSource = SPQC.NmetalSamplePaceQuery_Getlist(SO_IDItem, Nothing, Code_IDItem, Nothing, StartDate, EndtDate, chkSE_InCheck.Checked)
        End If
    End Sub
    ''' <summary>
    ''' 列印
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        On Error Resume Next
        If GetParameter() Then
            Dim ds As New DataSet
            If GridView2.RowCount = 0 Then Exit Sub
            Dim ltc As New CollectionToDataSet
            Dim SPQC As New NmetalSamplePaceQueryController
            ds.Tables.Clear()
            ltc.CollToDataSet(ds, "SamplePaceQuery", SPQC.NmetalSamplePaceQuery_Getlist(SO_IDItem, Nothing, Code_IDItem, Nothing, StartDate, EndtDate, chkSE_InCheck.Checked))
            PreviewRPT(ds, "rptSamplePaceQuery", "SamplePaceQuery", True, True)
            ltc = Nothing
        End If
    End Sub
    ''' <summary>
    ''' 導出Excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click

        If GridView2.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "導出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(GridControl1, saveFileDialog.FileName) Then
                MsgBox("已成功導出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If

    End Sub
    Public Function GetParameter() As Boolean
        Dim flag As Boolean = True
        If gueSO_ID.Text = String.Empty Then
            MsgBox("請選擇訂單編號！", 60, "提示")
            flag = False
            Exit Function
        End If
        SO_IDItem = gueSO_ID.Text
        Code_IDItem = gueCode_ID.Text

        If dteStartDate.Text = "" Then
            StartDate = Nothing
        Else
            StartDate = Format(CDate(dteStartDate.Text), "yyyy-MM-dd") & " 00:00:00"
        End If

        If dteStartDate.Text = "" Then
            EndtDate = Nothing
        Else
            EndtDate = Format(CDate(dteEndDate.Text), "yyyy-MM-dd") & " 23:59:59"
        End If





        Select Case gueCode_ID.Text
            Case "全部"
                Code_IDItem = Nothing
            Case ""
                MsgBox("請選擇條碼！", 60, "提示")
                flag = False
                Exit Function
        End Select

        Return flag
    End Function
End Class