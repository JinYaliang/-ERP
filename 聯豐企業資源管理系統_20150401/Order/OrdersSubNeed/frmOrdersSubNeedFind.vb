Public Class frmOrdersSubNeedFind
    Public isClickcmdFind As Boolean

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        If txtON_ID.Text.Trim = "" Then                          '�ݨD�渹
            tempValue = Nothing
        Else
            tempValue = txtON_ID.Text.Trim
        End If

        If txtOS_BatchID.Text.Trim = "" Then                 '�妸�s��
            tempValue2 = Nothing
        Else
            tempValue2 = txtOS_BatchID.Text.Trim
        End If

        If txtM_Code.Text.Trim = "" Then                        '���ƽs�X
            tempValue3 = Nothing
        Else
            tempValue3 = txtM_Code.Text.Trim
        End If

        If txtM_Name.Text.Trim = "" Then                       '���ƦW��
            tempValue4 = Nothing
        Else
            tempValue4 = txtM_Name.Text.Trim
        End If

        If cboM_CodeType.Text = "����" Then               '�t������
            tempValue5 = Nothing
        Else
            tempValue5 = cboM_CodeType.Text.Trim
        End If

        If cboON_PurchaseState.Text = "����" Then               '���ʪ��A
            tempValue6 = Nothing
        Else
            tempValue6 = cboON_PurchaseState.Text.Trim
        End If

        If cboON_SalesDptCheck.Text = "����" Then               '��~���f��
            tempValue7 = Nothing
        ElseIf cboON_SalesDptCheck.Text = "�w�f��" Then
            tempValue7 = "True"
        ElseIf cboON_SalesDptCheck.Text = "���f��" Then
            tempValue7 = "False"
        End If

        If cboON_OperationDptCheck.Text = "����" Then               '��B���f��
            tempValue8 = Nothing
        ElseIf cboON_OperationDptCheck.Text = "�w�f��" Then
            tempValue8 = "True"
        ElseIf cboON_OperationDptCheck.Text = "���f��" Then
            tempValue8 = "False"
        End If

        If dteON_AddDateBegin.Text = "" Then                   '�}�l�إߤ��
            tempValue9 = Nothing
        Else
            tempValue9 = dteON_AddDateBegin.Text
        End If

        If dteON_AddDateEnd.Text = "" Then                      '�����إߤ��
            tempValue10 = Nothing
        Else
            tempValue10 = dteON_AddDateEnd.Text
        End If

        isClickcmdFind = True
        Me.Close()
    End Sub

    Private Sub frmOrdersSubNeedFind_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isClickcmdFind = False
    End Sub
End Class