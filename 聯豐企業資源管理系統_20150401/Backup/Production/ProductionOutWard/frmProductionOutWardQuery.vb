Imports LFERP.Library.Production

Public Class frmProductionOutWardQuery

    Dim ds As New DataSet

    Private Sub frmProductionOutWardQuery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strDate As Date
        strDate = CDate(Now.Year & "-" & Now.Month & "-1")

        DateEdit2.Text = Format(Now, "yyyy/MM/dd")
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim pdc As New ProductionFieldDaySummary.ProductionFieldDaySummaryControl
        Dim pdi As List(Of ProductionFieldDaySummary.ProductionFieldDaySummaryInfo)

        pdi = pdc.ProductionFieldDaySummary_GetList(Nothing, Nothing, Nothing, Nothing, "F101", Nothing, DateEdit1.Text, DateEdit2.Text)

        If pdi.Count = 0 Then
            MsgBox("��e����d��,���s�b�~�o�O��!")
            Exit Sub
        Else

            Dim pdi1 As New ProductionFieldDaySummary.ProductionFieldDaySummaryInfo


            pdi1.Str1 = DateEdit1.Text
            pdi1.Str2 = DateEdit2.Text

            pdc.Temp3_Add(pdi1)

            Dim ltc As New CollectionToDataSet
            Dim ltc1 As New CollectionToDataSet

            ds.Tables.Clear()

            ltc.CollToDataSet(ds, "ProductionFiledDaySummay", pdc.ProductionFieldDaySummary_GetList(Nothing, Nothing, Nothing, Nothing, "F101", Nothing, DateEdit1.Text, DateEdit2.Text))
            ltc1.CollToDataSet(ds, "Temp3", pdc.Temp3_GetList(Nothing, Nothing))

            PreviewRPT(ds, "rptProductionOutWardAll", "�~�o���`�H���O��", True, True)

            ltc = Nothing

        End If
        Me.Close()

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

End Class