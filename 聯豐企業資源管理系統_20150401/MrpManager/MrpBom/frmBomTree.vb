Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql
Imports Microsoft.Office.Interop
Imports DevExpress.XtraPrinting
Imports LFERP.Library.MrpManager.Bom_D
Imports LFERP.Library.MrpManager.Bom_M

Public Class frmBomTree
#Region "屬性字段"
    Private _EditItem As String '属性栏位
    Dim Bodcon As New Bom_DController
    Dim BOCon As New Bom_MController
    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
#End Region

#Region "窗體載入事件"
    Private Sub frmBomTree_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'gluM_Code.Properties.DataSource = BOCon.Bom_M_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        gluM_Code.Properties.DisplayMember = "ParentGroup"
        gluM_Code.Properties.ValueMember = "ParentGroup"

        gluM_Code.EditValue = EditItem
        If EditItem <> String.Empty Then
            Dim boinfo As New List(Of Bom_MInfo)
            'boinfo = BOCon.Bom_M_GetList(EditItem, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If boinfo.Count > 0 Then
                txtM_Name.EditValue = boinfo(0).M_Name
                txtM_Gauge.EditValue = boinfo(0).M_Gauge
                ' txtSource.EditValue = boinfo(0).M_Source
            End If
            btnExpand_Click(Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "展開"
    Private Sub btnExpand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpand.Click
        If gluM_Code.EditValue = Nothing And String.IsNullOrEmpty(EditItem) = True Then
            MsgBox("請選擇所要展開的物料", MsgBoxStyle.Information, "提示")
            Exit Sub
        ElseIf CDec(caeQty.EditValue).Equals(0) Then
            MsgBox("數量不能為0", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        Try
            TreeList1.Nodes.Clear()
            TreeList1.DataSource = Bodcon.MRP_GetSingleBomTree(gluM_Code.EditValue, CDec(caeQty.EditValue), False)
            TreeList1.ExpandAll()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        
    End Sub
#End Region

#Region "按鍵事件"
    Private Sub gluM_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluM_Code.EditValueChanged
        If gluM_Code.EditValue <> Nothing Then
            txtM_Name.EditValue = GridView4.GetFocusedRowCellValue("M_Name")
            txtM_Gauge.EditValue = GridView4.GetFocusedRowCellValue("M_Gauge")
            txtSource.EditValue = GridView4.GetFocusedRowCellValue("M_Source")
            btnExpand.Focus()
        End If
    End Sub
#End Region

#Region "Excel"
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim filePath As String
        Dim fd As New FolderBrowserDialog
        If TreeList1.Nodes.Count < 1 Then
            MsgBox("沒有可導出的資料", MsgBoxStyle.Information, "提示")
        Else
            If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                filePath = fd.SelectedPath
                filePath += "\" + txtM_Name.EditValue + ".xls"
                TreeList1.ExportToXls(filePath)
                MsgBox("導出成功" + filePath, MsgBoxStyle.Information, "提示")
            End If
        End If
    End Sub
#End Region

#Region "數據值驗證"
    Private Sub caeQty_EditValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles caeQty.EditValueChanging
        If (CDec(e.NewValue) > 999999999 Or CDec(e.NewValue) < 0) Then
            e.Cancel = True
        End If
    End Sub

#End Region

End Class