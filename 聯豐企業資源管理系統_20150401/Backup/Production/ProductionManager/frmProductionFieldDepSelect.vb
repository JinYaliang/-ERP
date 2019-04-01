Public Class frmProductionFieldDepSelect
    '2014-02-26   ���@
    Dim ds As New DataSet

    Private Sub frmProductionFieldDepSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        LoadDataMain()

        dtStartDate.EditValue = Format(Now, "yyyy/MM/" & "01")
        dtEndDate.EditValue = Format(Now, "yyyy/MM/dd")

    End Sub

    Private Sub CreateTable()
        ds.Tables.Clear()

        With ds.Tables.Add("Dep")
            .Columns.Add("ControlDep", GetType(String))
            .Columns.Add("DepName", GetType(String))

        End With

        custeridEdit.Properties.DataSource = ds.Tables("Dep")
        custeridEdit.Properties.DisplayMember = "DepName"
        custeridEdit.Properties.ValueMember = "ControlDep"

    End Sub
    Private Sub LoadDataMain()

        Dim fc As New LFERP.Library.ProductionController.ProductionFieldControl
        Dim fi As List(Of LFERP.Library.ProductionController.ProductionFieldControlInfo)

        Dim row1 As DataRow
        row1 = ds.Tables("Dep").NewRow
        row1("ControlDep") = "*"
        row1("DepName") = "����"
        ds.Tables("Dep").Rows.Add(row1)

        fi = fc.ProductionFieldControl_GetList(InUserID, Nothing)
        If fi.Count = 0 Then
        Else
            Dim i As Integer
            For i = 0 To fi.Count - 1
                Dim row As DataRow
                row = ds.Tables("Dep").NewRow
                row("ControlDep") = fi(i).ControlDep
                row("DepName") = fi(i).DepName
                ds.Tables("Dep").Rows.Add(row)
            Next
        End If
        custeridEdit.EditValue = "*"
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim custerid As String = Nothing
        Dim dsProductionField As New DataSet
        Dim ltc As New CollectionToDataSet


        If dtStartDate.DateTime > dtEndDate.DateTime Then
            MsgBox("�_�l�ɶ�����j�_�I��ɶ�", 64, "����")
            Exit Sub
        End If

        If custeridEdit.Text <> "����" Then
            custerid = custeridEdit.EditValue
        End If



        Dim oc As New Library.ProductionField.ProductionFieldControl
        Dim orderInfo As New List(Of Library.ProductionField.ProductionFieldInfo)

        orderInfo = oc.ProductionFieldDayDPTData_GetList(Nothing, custerid, dtStartDate.Text, dtEndDate.Text)

        If orderInfo.Count < 0 Then
            MsgBox("�S���ŦX�n�D���ƾڡI", 64, "����")
            Exit Sub
        End If

        ltc.CollToDataSet(dsProductionField, "ProductionFieldDayDPTData", orderInfo)
        PreviewRPTDialog1(dsProductionField, "rptProductionFieldDayDPTData", "�Ͳ��������o�ƾڪ�", Format(CDate(dtStartDate.EditValue), "yyyy�~MM��dd��"), Format(CDate(dtEndDate.EditValue), "yyyy�~MM��dd��"), True, True)
        ltc = Nothing
    End Sub
End Class