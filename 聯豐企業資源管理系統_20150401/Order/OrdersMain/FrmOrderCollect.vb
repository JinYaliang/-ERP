Imports LFERP.Library.Orders

Public Class FrmOrderCollect

    Dim ds As New DataSet

    Sub CreateTablesMain()
        ds.Tables.Clear()
        With ds.Tables.Add("OM_Custer")
            .Columns.Add("OM_CusterID", GetType(String))
        End With
        custeridEdit.Properties.DataSource = ds.Tables("OM_Custer")
        custeridEdit.Properties.DisplayMember = "OM_CusterID"
        custeridEdit.Properties.ValueMember = "OM_CusterID"
        LoadDataMain()

    End Sub

    Sub LoadDataMain()
        Dim oc As New OrdersMainController
        Dim ci As New List(Of OrdersMainInfo)

        ci = oc.OrdersMainAndSub_GetCusterID()

        If ci.Count = 0 Then
            Exit Sub
        End If
        Dim i As Integer = 0

        Dim row As DataRow
        row = ds.Tables("OM_Custer").NewRow
        row("OM_CusterID") = "����"
        ds.Tables("OM_Custer").Rows.Add(row)
        '--------------------------------------

        For i = 0 To ci.Count - 1
            row = ds.Tables("OM_Custer").NewRow
            row("OM_CusterID") = ci(i).OM_CusterID
            ds.Tables("OM_Custer").Rows.Add(row)
        Next

    End Sub

    Private Sub orderEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles orderEdit.Click
        If orderEdit.Checked = False Then
            If sendEdit.Checked = True Then
                sendEdit.Checked = False
            End If
        End If
    End Sub

    Private Sub sendEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendEdit.Click
        If sendEdit.Checked = False Then
            If orderEdit.Checked = True Then
                orderEdit.Checked = False
            End If
        End If
    End Sub

    Private Sub FrmOrderCollect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTablesMain()

        dateEdit1.EditValue = Format(Now, "yyyy/MM/" & "01")
        dateEdit2.EditValue = Format(Now, "yyyy/MM/dd")
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        Dim ds As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim oc As New OrdersMainController
        Dim custerid, podate1, podate2, senddate1, senddate2 As String
        Dim PrintDate As String = ""

        custerid = Nothing
        podate1 = Nothing
        podate2 = Nothing
        senddate1 = Nothing
        senddate2 = Nothing


        If dateEdit1.Text <> "" Or dateEdit1.Text <> "" Then
            If orderEdit.Checked = False And sendEdit.Checked = False Then
                MsgBox("�п�ܤ������!")
                Exit Sub
            End If
        End If
        '------------------------------------------------------------------
        If custeridEdit.EditValue = "����" Then
            custerid = Nothing
        Else
            custerid = IIf(custeridEdit.Text = "", Nothing, custeridEdit.EditValue)
        End If

        If orderEdit.Checked = True Then
            podate1 = IIf(dateEdit1.Text = "", Nothing, dateEdit1.Text)
            podate2 = IIf(dateEdit2.Text = "", Nothing, dateEdit2.Text)
            PrintDate = "�U����:" & podate1 & "-" & podate2
        End If

        If sendEdit.Checked = True Then
            senddate1 = IIf(dateEdit1.Text = "", Nothing, dateEdit1.Text)
            senddate2 = IIf(dateEdit2.Text = "", Nothing, dateEdit2.Text)
            PrintDate = "��f���:" & senddate1 & "-" & senddate2
        End If

        Dim strvel As String = ""
        If ComboBoxType.Text = "����" Then
            strvel = Nothing
        ElseIf ComboBoxType.Text = "���槹" Then
            strvel = ">"
        ElseIf ComboBoxType.Text = "�w�槹" Then
            strvel = "<="
        End If

        If oc.OrdersMainAndSub_GetList1(custerid, podate1, podate2, senddate1, senddate2, strvel).Count > 0 Then
        Else
            MsgBox("�S���ŦX�n�D���ƾڡI", 64, "����")
            Exit Sub
        End If

        ltc.CollToDataSet(ds, "OrdersMain", oc.OrdersMainAndSub_GetList1(custerid, podate1, podate2, senddate1, senddate2, strvel))
        PreviewRPTDialog1(ds, "rptOrdersMainAndSub", "�i�M��---�����f�i�ת�", PrintDate, "", True, True)
        ltc = Nothing
    End Sub

    Private Sub dateEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dateEdit1.EditValueChanged

    End Sub
End Class