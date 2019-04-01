Imports LFERP.Library.WareHouse.WareInventory
Imports LFERP.Library.Material

Public Class frmWareNumRpt
    Dim strWHID As String '�ܮw�N�� �եέܮw��
    Dim mtc As New MaterialTypeController

    Private Sub dteEndDate_DateTimeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dteEndDate.DateTimeChanged
        dteBeginDate.DateTime = Format(dteEndDate.DateTime, "yyyy/MM")
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmWareNumRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'mtc.LoadNodes(tv1, ErpUser.MaterialType)
        dteEndDate.Text = Format(Now, "yyyy/MM/dd")
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim ds As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet

        Dim wic As New WareInventoryMTController
        Dim mcCompany As New LFERP.DataSetting.CompanyControler

        ds.Tables.Clear()

        If bteWH_ID.EditValue = "" Then
            MsgBox("�ܮw�W�٤��ର�šA�п�J�ܮw�W��!", 64, "����")
            bteWH_ID.Focus()
            Exit Sub
        End If
        If dteEndDate.Text = "" Then
            MsgBox("���`������ର�šA�п�J���`���", 64, "����")
            dteEndDate.Focus()
            Exit Sub
        End If

        If wic.WareNum_GetList(bteM_Code.Text, strWHID, dteBeginDate.Text, dteEndDate.Text).Count = 0 Then
            MsgBox("���o�s���`���Ƭ���,�Э��s�]�m���`����!")
            Exit Sub
        End If

        ltc.CollToDataSet(ds, "WareNum", wic.WareNum_GetList(bteM_Code.Text, strWHID, dteBeginDate.Text, dteEndDate.Text))
        ltc1.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strInCompany, Nothing, Nothing, Nothing))

        PreviewRPT(ds, "rptWareNum", "���o�s���`��", True, True)
        ltc = Nothing
        ltc1 = Nothing
        Me.Close()
    End Sub

    Private Sub bteWH_ID_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles bteWH_ID.ButtonClick
        tempValue3 = "50081201"

        frmWareHouseSelect.SelectWareID = ""
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = Me.Left + bteWH_ID.Left + 7
        frmWareHouseSelect.Top = Me.Top + bteWH_ID.Top + bteWH_ID.Height + 31
        frmWareHouseSelect.ShowDialog()
        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            bteWH_ID.Text = frmWareHouseSelect.SelectWareUpName & "-" & frmWareHouseSelect.SelectWareName
            strWHID = frmWareHouseSelect.SelectWareID
        End If
    End Sub

    Private Sub bteM_Code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles bteM_Code.ButtonClick
        tempCode = ""
        frmBOMSelect.XtraTabPage2.PageVisible = False
        frmBOMSelect.XtraTabPage3.PageVisible = False
        frmBOMSelect.ShowDialog()
        bteM_Code.Text = tempCode
        tempCode = ""
    End Sub

End Class