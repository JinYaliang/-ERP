Imports LFERP.Library.Orders
Public Class frmOrdersSubNeedMain
    Dim onc As New OrdersSubNeedController

    Private Sub frmOrdersSubNeedMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmsRef_Click(Nothing, Nothing)
    End Sub

    Private Sub cmsExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsExport.Click
        If GridView1.RowCount = 0 Then Exit Sub
        'ExportGridToXls()

        Dim saveFileDialog As New SaveFileDialog()

        saveFileDialog.Title = "�ɥXExcel"

        saveFileDialog.Filter = "Excel2003���(*.xls)|*.xls"
        '|Excel2007�ΥH�W���(*.xlsx)|*.xlsx  '��e����2007 �H�ΥH�W�������~�I

        Dim dialogResult__1 As DialogResult = saveFileDialog.ShowDialog(Me)

        If dialogResult__1 = Windows.Forms.DialogResult.OK Then

            GridView1.BestFitColumns()

            GridControl1.ExportToExcelOld(saveFileDialog.FileName)

            DevExpress.XtraEditors.XtraMessageBox.Show("�O�s���\�I", "����", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub cmsRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsRef.Click
        Dim oni As List(Of OrdersSubNeedInfo)
        oni = onc.OrdersSubNeed_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, "W0301", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        GridControl1.DataSource = oni
    End Sub

    Private Sub cmsView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsView.Click
        Dim fr As New frmOrdersSubNeed

        fr.txtON_ID.Text = GridView1.GetFocusedRowCellValue(ON_ID)

        fr.lblTittle.Text = "�妸�ݨD��-�d��"
        fr.ShowDialog()
    End Sub

    Private Sub cmsSalesDptCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsSalesDptCheck.Click
        Dim fr As New frmOrdersSubNeed
        Dim oni As List(Of OrdersSubNeedInfo)

        oni = onc.OrdersSubNeed_GetList(Nothing, GridView1.GetFocusedRowCellValue(ON_ID), Nothing, Nothing, Nothing, "W0301", Nothing, Nothing, Nothing, Nothing, Nothing, "True")
        If oni.Count > 0 Then
            MsgBox("���ݨD��w�f�֡A���ݦA�f��!", 64, "����")
            Exit Sub
        End If

        fr.txtON_ID.Text = GridView1.GetFocusedRowCellValue(ON_ID)

        fr.lblTittle.Text = "�妸�ݨD��-��~���f��"
        fr.ShowDialog()
    End Sub

    Private Sub cmsOperationDptCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsOperationDptCheck.Click
        Dim fr As New frmOrdersSubNeed

        fr.txtON_ID.Text = GridView1.GetFocusedRowCellValue(ON_ID)

        fr.lblTittle.Text = "�妸�ݨD��-��B���f��"
        fr.ShowDialog()
    End Sub

    Private Sub cmsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsDelete.Click
        Try
            Dim oni As List(Of OrdersSubNeedInfo)
            oni = onc.OrdersSubNeed_GetList(Nothing, GridView1.GetFocusedRowCellValue(ON_ID), Nothing, Nothing, Nothing, "W0301", Nothing, Nothing, Nothing, Nothing, "True", Nothing)
            If oni.Count > 0 Then
                MsgBox("���ݨD��w�s�b�f�ְO���A����A�R��!", 64, "����")
                Exit Sub
            End If

            If MsgBox("�T�w�n�R���ݨD�渹���G" & GridView1.GetFocusedRowCellValue(ON_ID) & " ���O���ܡH", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "����") = MsgBoxResult.Ok Then
                If onc.OrdersSubNeed_Delete(GridView1.GetFocusedRowCellValue(ON_ID), Nothing) = True Then
                    MsgBox("�ݨD�渹���G" & GridView1.GetFocusedRowCellValue(ON_ID) & " ���O���w���\�R��", 64, "����")
                    cmsRef_Click(Nothing, Nothing)
                Else
                    MsgBox("�R������!", 64, "����")
                End If
            End If
        Catch ex As Exception
            MsgBox("�R������!" & vbCrLf & ex.Message, 64, "����")
        End Try
    End Sub

    Private Sub cmsFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsFind.Click
        Dim fr As New frmOrdersSubNeedFind

        fr.ShowDialog()
        If fr.isClickcmdFind = True Then
            GridControl1.DataSource = onc.OrdersSubNeed_GetList(Nothing, tempValue, tempValue2, tempValue3, tempValue4, "W0301", tempValue5, tempValue6, tempValue9, tempValue10, tempValue7, tempValue8)
        End If

        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing
        tempValue7 = Nothing
        tempValue8 = Nothing
        tempValue9 = Nothing
        tempValue10 = Nothing

    End Sub

    Private Sub cmsPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPrint.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim strON_ID As String
        strON_ID = GridView1.GetFocusedRowCellValue("ON_ID").ToString

        Dim ds As New DataSet
        ds.Tables.Clear()

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim oni As List(Of OrdersSubNeedInfo)

        oni = onc.OrdersSubNeed_GetList(Nothing, strON_ID, Nothing, Nothing, Nothing, "W0301", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If oni.Count = 0 Then Exit Sub

        ltc.CollToDataSet(ds, "OrdersSubNeed", oni)
        ltc1.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strInCompany, Nothing, Nothing, Nothing))

        PreviewRPT(ds, "rptOrdersSubNeed", "�妸�ݨD��", True, True)

        ltc = Nothing
        ltc1 = Nothing
    End Sub
End Class