Imports LFERP.Library.NmetalSampleManager.NmetalSamplePace
Imports DevExpress.XtraEditors
Imports DevExpress.XtraPrinting

Public Class Test
    Dim pmlist As New List(Of NmetalSamplePaceInfo)
    Dim pncon As New NmetalSamplePaceControler
    Dim DT_M As New DataTable
    Private _SO_ID As String
    Property SO_ID() As String '属性
        Get
            Return _SO_ID
        End Get
        Set(ByVal value As String)
            _SO_ID = value
        End Set
    End Property
    Private Sub Test_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadSub(SO_ID)
    End Sub
    Private Sub LoadSub(ByVal SO_ID As String)
        BandedGridView1.Bands.Clear()      '清空BandedGridView1
        BandedGridView1.Columns.Clear()

        DT_M = pncon.NmetalSamplePace_GetlistDepWeight(SO_ID)
        If DT_M.Rows.Count > 0 Then
            BGridViewDeal(DT_M, 6, BandedGridView1, GridControl1)
        End If
        BGColumnSet(BandedGridView1)
    End Sub
    Private Sub BGridViewDeal(ByVal dt As DataTable, ByVal begionNum As Integer, ByVal BG As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, ByVal GC As DevExpress.XtraGrid.GridControl)
        '1.求和與修改列標題
        Dim i As Integer = 0
        For i = 0 To dt.Columns.Count - 1
            If i >= 0 And i <= begionNum - 3 Then
                If i = 0 Then
                    BG.Bands.AddBand("條碼收發明細表")

                    GC.DataSource = dt
                End If
                BG.Bands(0).Columns.Add(BG.Columns(dt.Columns(i).ColumnName))
            End If
        Next
    End Sub
    Private Sub BGColumnSet(ByVal BG As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
        If BG.Columns.Count < 1 Then
            Exit Sub
        End If
        If BG.Bands.Count < 1 Then
            Exit Sub
        End If
        Dim memoEdit As New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
        memoEdit.ShowIcon = False

        '1.列的屬性設置
        BG.Columns("SO_SampleID").Width = 130
        BG.Columns("SO_SampleID").Caption = "樣辦單號"

        BG.Columns("Code_ID").Width = 130
        BG.Columns("Code_ID").Caption = "條碼編號"

        BG.Columns("Code_ID").SummaryItem.FieldName = "Code_ID"
        BG.Columns("Code_ID").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        BG.Columns("Code_ID").SummaryItem.DisplayFormat = "合计：{0}"



        BG.Columns("IWeight").Width = 100
        BG.Columns("IWeight").Caption = "來料重量"

        BG.Columns("IWeight").SummaryItem.FieldName = "IWeight"
        BG.Columns("IWeight").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        BG.Columns("IWeight").SummaryItem.DisplayFormat = "{0}"



        BG.Columns("TWeight").Width = 100
        BG.Columns("TWeight").Caption = "當前重量"
        BG.Columns("TWeight").SummaryItem.FieldName = "TWeight"
        BG.Columns("TWeight").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        BG.Columns("TWeight").SummaryItem.DisplayFormat = "{0}"


        BG.Columns("理論損耗").SummaryItem.FieldName = "理論損耗"
        BG.Columns("理論損耗").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        BG.Columns("理論損耗").SummaryItem.DisplayFormat = "{0}"


        '2.只讀與編輯
        For i As Integer = 0 To BG.Columns.Count - 1         '列設置不可編輯與只讀
            BG.Columns(i).OptionsColumn.ReadOnly = True
            BG.Columns(i).OptionsColumn.AllowEdit = False

        Next
        BandedGridView1.BestFitColumns()
    End Sub
    ''' <summary>
    ''' 導出Excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsm_Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_Excel.Click
        If BandedGridView1.RowCount = 0 Then
            Exit Sub
        End If
        Dim title As String = "條碼收發明細表"
        Dim sfd As SaveFileDialog = New SaveFileDialog
        sfd.Title = "導出文件"
        sfd.Filter = "Excel文件(*.xls)|*.xls"
        sfd.FileName = title + DateTime.Now.ToString("yyyyMMddhhmmss")
        Dim dialogResult As DialogResult = sfd.ShowDialog(Me)

        If dialogResult = dialogResult.OK Then
            BandedGridView1.BestFitColumns()
            GridControl1.ExportToXls(sfd.FileName)
            XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class