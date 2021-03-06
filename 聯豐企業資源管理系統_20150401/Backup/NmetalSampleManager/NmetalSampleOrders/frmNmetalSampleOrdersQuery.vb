Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersMain
Imports LFERP.Library.NmetalSampleManager.NmetalSampleWareInventory
Imports System.Threading
Imports LFERP.Library.PieceProcess

Public Class frmNmetalSampleOrdersQuery
#Region "屬性"
    Dim SOS As New NmetalSampleOrdersMainControler
    Dim SWI As New NmetalSampleWareInventoryControler
    Dim pncon As New PersonnelControl

    Private _PM_M_Code As String
    Private strTID As String
    Private strD_ID As String
    Property PM_M_Code() As String '属性
        Get
            Return _PM_M_Code
        End Get
        Set(ByVal value As String)
            _PM_M_Code = value
        End Set
    End Property
#End Region

#Region "加载事件"
    ''' <summary>
    ''' 加載事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSampleOrdersQuery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtPM_M_Code.Text = PM_M_Code

        Dim somlist As New List(Of NmetalSampleOrdersMainInfo)
        somlist = SOS.NmetalSampleOrdersType_GetList(Nothing, "M", "True", Nothing)
        chkMaterialType.DataSource = somlist

        Dim pmlist As New List(Of PersonnelInfo) '部門分享
        pmlist = pncon.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)
        chkD_ID.DataSource = pmlist

        CheckEdit2_CheckedChanged(Nothing, Nothing)
    End Sub
#End Region

#Region "查询"
    ''' <summary>
    ''' 查詢
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Dim strPM_M_Code As String = String.Empty
        Dim strSO_SampleID As String = String.Empty
        If CheckEdit3.Checked = True Then
            strSO_SampleID = Nothing
            If txtPM_M_Code.Text = String.Empty Then
                MsgBox("請選擇产品编号！", 60, "提示")
                Me.txtPM_M_Code.Focus()
                Exit Sub
            End If
            strPM_M_Code = txtPM_M_Code.Text
        End If

        If CheckEdit4.Checked = True Then
            strPM_M_Code = Nothing
            If txtSO_SampleID.Text = String.Empty Then
                MsgBox("样办单号不能为空！", 60, "提示")
                Me.txtSO_SampleID.Focus()
                Exit Sub
            End If
            strSO_SampleID = txtSO_SampleID.Text
        End If

        If gluMaterialType.Text = String.Empty Then
            MsgBox("請選擇材料！", 60, "提示")
            Me.gluMaterialType.Focus()
            Exit Sub
        End If

        If gluD_ID.Text = String.Empty Then
            MsgBox("請選擇部门！", 60, "提示")
            Me.gluD_ID.Focus()
            Exit Sub
        End If

        Dim swlist As New List(Of NmetalSampleWareInventoryInfo)
        If CheckEdit2.Checked Then
            swlist = SWI.NmetalSampleOrdersMainInvent_GetList(strPM_M_Code, strTID, strSO_SampleID, strD_ID)
        Else
            swlist = SWI.NmetalSampleOrdersMainInventA_GetList(strPM_M_Code, strTID, strSO_SampleID, strD_ID)
        End If

        If swlist.Count > 0 Then
            GridControl1.DataSource = swlist
        Else
            MsgBox("没有数据显示！", 60, "提示")
            GridControl1.DataSource = Nothing
            Exit Sub
        End If
    End Sub
#End Region

#Region "導出Excel"
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
#End Region

#Region "控件返回值"
    Private Sub gluMaterialType_QueryResultValue(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.QueryResultValueEventArgs) Handles gluMaterialType.QueryResultValue
        Dim str As String = String.Empty
        strTID = String.Empty
        For i As Integer = 0 To chkMaterialType.ItemCount - 1
            If chkMaterialType.GetItemChecked(i) = True Then
                str += chkMaterialType.GetItemText(i) + ","
                strTID += "'" + chkMaterialType.GetItemValue(i) + "',"

            End If
        Next
        If str.Length > 1 Then
            str = str.Remove(str.LastIndexOf(","), 1)
            strTID = strTID.Remove(strTID.LastIndexOf(","), 1)
        End If
        e.Value = str

    End Sub

    Private Sub gluD_ID_QueryResultValue(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.QueryResultValueEventArgs) Handles gluD_ID.QueryResultValue
        Dim str As String = String.Empty
        strD_ID = String.Empty
        For i As Integer = 0 To chkD_ID.ItemCount - 1
            If chkD_ID.GetItemChecked(i) = True Then
                str += chkD_ID.GetItemText(i) + ","
                strD_ID += "'" + chkD_ID.GetItemValue(i) + "',"

            End If
        Next
        If str.Length > 1 Then
            str = str.Remove(str.LastIndexOf(","), 1)
            strD_ID = strD_ID.Remove(strD_ID.LastIndexOf(","), 1)
        End If
        e.Value = str
    End Sub
#End Region

#Region "控件全选"
    Private Sub CheckEdit1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit1.CheckedChanged
        For i As Integer = 0 To chkMaterialType.ItemCount - 1
            If CheckEdit1.Checked Then
                chkMaterialType.SetItemChecked(i, True)
            Else
                chkMaterialType.SetItemChecked(i, False)
            End If
        Next
    End Sub
#End Region

    Private Sub CheckEdit2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit2.CheckedChanged
        If CheckEdit2.Checked Then
            GridColumn3.Visible = True
            GridControl1.DataSource = Nothing
        Else
            GridColumn3.Visible = False
            GridControl1.DataSource = Nothing
        End If
    End Sub
    Private Sub CheckEdit3_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit3.Click, CheckEdit4.Click
        Select Case sender.Name
            Case "CheckEdit3"
                txtPM_M_Code.Enabled = True
                txtSO_SampleID.Enabled = False
                CheckEdit4.EditValue = False
            Case "CheckEdit4"
                txtPM_M_Code.Enabled = False
                txtSO_SampleID.Enabled = True
                CheckEdit3.EditValue = False
        End Select
    End Sub

    Private Sub CheckEdit5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit5.CheckedChanged
        For i As Integer = 0 To Me.chkD_ID.ItemCount - 1
            If CheckEdit5.Checked Then
                chkD_ID.SetItemChecked(i, True)
            Else
                chkD_ID.SetItemChecked(i, False)
            End If
        Next
    End Sub
End Class