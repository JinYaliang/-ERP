Imports LFERP.DataSetting
Imports LFERP.Library.ProductionField
Imports LFERP.Library.Production.ProductionFieldDaySummary
Imports LFERP.Library.ProductionDPTWareInventory


Public Class frmProductionDayReport

    Dim upc As New UserPowerControl
    Dim pc As New LFERP.Library.PieceProcess.PersonnelControl

    Sub LoadDepartment()   '�����H��

        GluDep.Properties.DataSource = pc.FacBriSearch_GetList(strInFacID, Nothing, strInDepID, Nothing) '��l����
        GluDep.Properties.DisplayMember = "DepName"
        GluDep.Properties.ValueMember = "DepID"

    End Sub

    Private Sub frmProductionDayReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadDepartment()

        DateEdit1.Text = Format(Now, "yyyy/MM/dd")
        DateEdit2.Text = Format(Now, "yyyy/MM/dd")

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If GluDep.EditValue = "" Then
            MsgBox("�п�J�ݭn�d�ߪ�����!")
            Exit Sub
        End If
        If DateEdit1.Text = "" Or DateEdit2.Text = "" Then
            MsgBox("�п�J�ݭn�d�ߪ�����d��!")
            Exit Sub
        End If



        Dim ds As New DataSet

        Dim pdc As New ProductionFieldDaySummaryControl
        Dim pddc As New ProductionDPTWareInventoryControl

        '---------------------------------------------------------------
        Dim pdi As New ProductionFieldDaySummaryInfo

        pdi.Str1 = DateEdit1.Text
        pdi.Str2 = DateEdit2.Text           '�{���x�s����q�O��!

        pdc.Temp3_Add(pdi)

        '---------------------------------------------------------------

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet

        ds.Tables.Clear()


        If pdc.ProductionFieldDaySummary_GetList1(Nothing, Nothing, Nothing, Nothing, GluDep.EditValue, Nothing, DateEdit1.Text, DateEdit2.Text).Count = 0 Then
            MsgBox("��e�����b������d�򤺵L���Ƶo�X�O��!")
            Exit Sub
        End If

        ltc.CollToDataSet(ds, "ProductionFieldDaySummary", pdc.ProductionFieldDaySummary_GetList1(Nothing, Nothing, Nothing, Nothing, GluDep.EditValue, Nothing, DateEdit1.Text, DateEdit2.Text))
        ' ltc1.CollToDataSet(ds, "ProductionDPTWareInventory", pddc.ProductionDPTWareInventory_GetList(Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(ds, "Temp3", pdc.Temp3_GetList(Nothing, Nothing))

        PreviewRPT(ds, "rptProductionDayNumber", "�����C�ѥͲ��O��", True, True)

        ltc = Nothing
        ltc1 = Nothing

        Me.Close()

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
End Class