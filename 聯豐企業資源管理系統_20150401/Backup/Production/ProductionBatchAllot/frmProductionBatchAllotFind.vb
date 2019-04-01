Imports LFERP.DataSetting

Public Class frmProductionBatchAllotFind
    Dim fac As New FacControler
    Public isClickbtnFind As Boolean = False

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub frmProductionBatchAllotFind_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gluFacID.Properties.DisplayMember = "FacName"
        gluFacID.Properties.ValueMember = "FacID"
        gluFacID.Properties.DataSource = fac.GetFacList(Nothing, Nothing)

    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If txtPBA_ID.Text.Trim = "" Then                          '���u�渹
            tempValue = Nothing
        Else
            tempValue = txtPBA_ID.Text.Trim
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

        If gluFacID.EditValue = "" Then               '�t�O�W��
            tempValue5 = Nothing
        Else
            tempValue5 = gluFacID.EditValue
        End If

        If cboPBA_Check.Text = "����" Then               '�f��
            tempValue6 = Nothing
        ElseIf cboPBA_Check.Text = "�w�f��" Then
            tempValue6 = "True"
        ElseIf cboPBA_Check.Text = "���f��" Then
            tempValue6 = "False"
        End If

        If dtePBA_AddDateBegin.Text = "" Then                   '�}�l�إߤ��
            tempValue7 = Nothing
        Else
            tempValue7 = dtePBA_AddDateBegin.Text
        End If

        If dtePBA_AddDateEnd.Text = "" Then                      '�����إߤ��
            tempValue8 = Nothing
        Else
            tempValue8 = dtePBA_AddDateEnd.Text
        End If

        isClickbtnFind = True
        Me.Close()
    End Sub
End Class