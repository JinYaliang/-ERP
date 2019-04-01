Imports LFERP.DataSetting
Imports LFERP.Library.Product
Imports LFERP.Library.Orders

Public Class frmOutWardsFind
    Public isClickbtnOK As Boolean     '�O���O�_�����F�d�߫��s

    Private Sub frmOutWardsFind_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        isClickbtnOK = False

        If Me.Text = "�妸���--�d��" Then
            LabelControl1.Text = "�q��s��(&I)�G"
        End If

        '���J�Ȥ�N��
        If tempValue10 = "MG" Then
            C_CusterID.FieldName = "OC_CustomerID"
            C_EngName.Visible = False
            C_ChsName.Visible = False
            Dim occ As New OrderCustomerController
            gluOM_CusterID.Properties.DisplayMember = "OC_CustomerID"
            gluOM_CusterID.Properties.ValueMember = "OC_CustomerID"
            gluOM_CusterID.Properties.DataSource = occ.OrderCustomer_GetCustomerID
        Else
            Dim mtd As New CusterControler
            gluOM_CusterID.Properties.DisplayMember = "C_CusterID"
            gluOM_CusterID.Properties.ValueMember = "C_CusterID"
            gluOM_CusterID.Properties.DataSource = mtd.GetCusterList(Nothing, Nothing, Nothing)
        End If


        '���J���~�s��
        Dim mc As New ProductController
        gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
        gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
        gluPM_M_Code.Properties.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        tempValue10 = ""
    End Sub

    '�����d�߫��s
    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        isClickbtnOK = True
        If txtOW_NO.Text.Trim = "" Then                      '�e�f�渹
            tempValue = Nothing
        Else
            tempValue = txtOW_NO.Text.Trim
        End If

        tempValue3 = gluOM_CusterID.EditValue         '�Ȥ�N��

        If txtOS_BatchID.Text.Trim = "" Then                 '�妸�s��
            tempValue2 = Nothing
        Else
            tempValue2 = txtOS_BatchID.Text.Trim
        End If

        If txtOM_CusterNO.Text.Trim = "" Then             '�Ȥ�s��
            tempValue4 = Nothing
        Else
            tempValue4 = txtOM_CusterNO.Text.Trim
        End If

        If txtOM_CusterPO.Text.Trim = "" Then             '�Ȥ�PO NO.
            tempValue5 = Nothing
        Else
            tempValue5 = txtOM_CusterPO.Text.Trim
        End If

        tempValue6 = gluPM_M_Code.EditValue           '���~�s��

        If cboOW_Check.Text = "����" Then               '�f�֪��A
            tempValue7 = Nothing
        ElseIf cboOW_Check.Text = "�w�f��" Then
            tempValue7 = True
        ElseIf cboOW_Check.Text = "���f��" Then
            tempValue7 = False
        End If

        If dteOW_DateBegin.Text = "" Then                   '�e�f�}�l���
            tempValue8 = Nothing
        Else
            tempValue8 = dteOW_DateBegin.Text
        End If

        If dteOW_DateEnd.Text = "" Then                      '�e�f�������
            tempValue9 = Nothing
        Else
            tempValue9 = dteOW_DateEnd.Text
        End If

        Me.Close()
    End Sub

    '�����������s�A��������
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    '�����Ů���A��ܤU�Ե��
    Private Sub gluOM_CusterID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gluOM_CusterID.KeyDown, gluPM_M_Code.KeyDown, cboOW_Check.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub
End Class