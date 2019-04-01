Imports LFERP.Library.Product
Imports LFERP.Library.Orders

Public Class frmOrderCustomer
    Public strAutoID As String
    Dim ds As New DataSet
    Dim occ As New OrderCustomerController

    Private Sub frmOrderCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mc As New ProductController

        CreateTables()

        gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
        gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
        gluPM_M_Code.Properties.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        LoadOC_CustomerID()

        If lblTittle.Text = "�Ȥ�q����-�s�W" Then

        ElseIf lblTittle.Text = "�Ȥ�q����-�ק�" Then
            LoadData()
            Me.Text = "�Ȥ�q����-�ק�"
        ElseIf lblTittle.Text = "�Ȥ�q����-�d��" Then
            btnSave.Enabled = False
            GroupBox1.Enabled = False
            LoadData()
            Me.Text = "�Ȥ�q����-�d��"
        End If
    End Sub

    Sub CreateTables()

        ds.Tables.Clear()

        With ds.Tables.Add("ProductM_Code")
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
        End With
        gluM_Code.Properties.DisplayMember = "M_Name"
        gluM_Code.Properties.ValueMember = "M_Code"
        gluM_Code.Properties.DataSource = ds.Tables("ProductM_Code")

    End Sub

    Sub LoadData()
        Dim oci As List(Of OrderCustomerInfo)

        oci = occ.OrderCustomer_GetList(strAutoID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If oci.Count > 0 Then
            txtOC_ID.Text = oci(0).OC_ID
            gluPM_M_Code.EditValue = oci(0).PM_M_Code
            gluM_Code.EditValue = oci(0).M_Code
            cboOC_CustomerID.Text = oci(0).OC_CustomerID
            txtOC_CustomerPO.Text = oci(0).OC_CustomerPO

            txtOC_CustomerNo.Text = oci(0).OC_CustomerNo
            txtOC_Qty.Text = oci(0).OC_Qty
            txtOC_Spare.Text = oci(0).OC_Spare
            dteOC_PODate.Text = oci(0).OC_PODate
            txtOC_Index.Text = oci(0).OC_Index

            txtOC_Remark.Text = oci(0).OC_Remark
        End If
    End Sub

    Sub LoadOC_CustomerID()
        Dim oci As List(Of OrderCustomerInfo)
        Dim i As Integer

        oci = occ.OrderCustomer_GetCustomerID

        If oci.Count > 0 Then
            For i = 0 To oci.Count - 1
                cboOC_CustomerID.Properties.Items.Add(oci(i).OC_CustomerID)
            Next
        End If
    End Sub

    Private Sub gluPM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_M_Code.EditValueChanged
        Dim pbc As New ProductBomController
        Dim pbi As List(Of ProductBomInfo)
        Dim row3 As DataRow
        Dim i%

        gluM_Code.Text = ""
        If IsDBNull(gluPM_M_Code.EditValue) = False And gluPM_M_Code.EditValue <> "" Then
            ds.Tables("ProductM_Code").Clear()

            row3 = ds.Tables("ProductM_Code").NewRow
            row3("M_Code") = gluPM_M_Code.EditValue
            row3("M_Name") = gluPM_M_Code.EditValue

            ds.Tables("ProductM_Code").Rows.Add(row3)

            pbi = pbc.Prod_Mounting_New_GetList(gluPM_M_Code.EditValue, Nothing, Nothing, 0, Nothing, Nothing, Nothing)

            If pbi.Count > 0 Then
                For i = 0 To pbi.Count - 1
                    Dim row1 As DataRow
                    row1 = ds.Tables("ProductM_Code").NewRow
                    row1("M_Code") = pbi(i).M_Code
                    row1("M_Name") = pbi(i).M_Name

                    ds.Tables("ProductM_Code").Rows.Add(row1)
                Next
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim oci As New OrderCustomerInfo
        Dim ociGet As List(Of OrderCustomerInfo)

        If txtOC_ID.Text.Trim = "" Then
            MsgBox("�п�J�q��s��!", 64, "����")
            txtOC_ID.Focus()
            Exit Sub
        End If
        If gluPM_M_Code.Text.Trim = "" Then
            MsgBox("�п�J���~�s��!", 64, "����")
            gluPM_M_Code.Focus()
            Exit Sub
        End If
        If gluM_Code.Text.Trim = "" Then
            MsgBox("�п�J�t��W��!", 64, "����")
            gluM_Code.Focus()
            Exit Sub
        End If
        If cboOC_CustomerID.Text.Trim = "" Then
            MsgBox("�п�J�Ȥ�N��!", 64, "����")
            cboOC_CustomerID.Focus()
            Exit Sub
        End If
        If txtOC_CustomerPO.Text.Trim = "" Then
            MsgBox("�п�J�Ȥ�PO!", 64, "����")
            txtOC_CustomerPO.Focus()
            Exit Sub
        End If
        If txtOC_CustomerNo.Text.Trim = "" Then
            MsgBox("�п�J�Ȥ�s��!", 64, "����")
            txtOC_CustomerNo.Focus()
            Exit Sub
        End If
        If txtOC_Qty.Text.Trim = "" Then
            MsgBox("�п�J�q��ƶq!", 64, "����")
            txtOC_Qty.Focus()
            Exit Sub
        End If
        If txtOC_Spare.Text.Trim = "" Then
            MsgBox("�п�J�h��!", 64, "����")
            txtOC_Spare.Focus()
            Exit Sub
        End If
        If dteOC_PODate.Text = "" Then
            MsgBox("�п�JPO���!", 64, "����")
            dteOC_PODate.Focus()
            Exit Sub
        End If
        If txtOC_Index.Text.Trim = "" Then
            MsgBox("�п�J�y����!", 64, "����")
            txtOC_Index.Focus()
            Exit Sub
        End If

        If lblTittle.Text = "�Ȥ�q����-�s�W" Then
            ociGet = occ.OrderCustomer_GetList(Nothing, txtOC_ID.Text.Trim, gluPM_M_Code.EditValue, gluM_Code.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If ociGet.Count > 0 Then
                If MsgBox("�w�s�b�@���q��渹�A���~�s���M�t��W�ٳ��ۦP���O���A�O�_�n�~��K�[�ӰO���H", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "����") = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
        End If

        oci.OC_ID = txtOC_ID.Text.Trim
        oci.PM_M_Code = gluPM_M_Code.EditValue
        oci.M_Code = gluM_Code.EditValue
        oci.OC_CustomerID = cboOC_CustomerID.Text
        oci.OC_CustomerPO = txtOC_CustomerPO.Text.Trim

        oci.OC_CustomerNo = txtOC_CustomerNo.Text.Trim
        oci.OC_Qty = txtOC_Qty.Text.Trim
        oci.OC_NoSendQty = txtOC_Qty.Text
        oci.OC_Spare = txtOC_Spare.Text
        oci.OC_PODate = Format(dteOC_PODate.DateTime, "yyyy/MM/dd")

        oci.OC_Index = txtOC_Index.Text.Trim
        oci.OC_Remark = txtOC_Remark.Text.Trim

        If lblTittle.Text = "�Ȥ�q����-�s�W" Then
            oci.OC_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
            oci.OC_AddUser = UserName

            If occ.OrderCustomer_Add(oci) = True Then
                MsgBox("�q��s�W����!", 64, "����")
                Me.Close()
            End If
        ElseIf lblTittle.Text = "�Ȥ�q����-�ק�" Then
            oci.AutoID = strAutoID
            oci.OC_ModifyDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
            oci.OC_ModifyUser = UserName

            If occ.OrderCustomer_Update(oci) = True Then
                MsgBox("�q��ק粒��!", 64, "����")
                Me.Close()
            End If
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    '�����Ů���A��ܤU�Ե��
    Private Sub gluPM_M_Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gluPM_M_Code.KeyDown, gluM_Code.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub
End Class