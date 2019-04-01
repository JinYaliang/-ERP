Imports LFERP.Library.Purchase.Acceptance
Imports LFERP.Library.Purchase.Purchase
Imports LFERP.DataSetting
Imports LFERP.SystemManager

Public Class frmAcceptanceGather
    Dim isPrint As Boolean
    Dim isExport As Boolean

    ''' <summary>
    ''' �����������s�A�h�X����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' �[������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmAcceptanceGather_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mtd As New LFERP.DataSetting.SuppliersControler
        '�[�������� 
        gluS_Supplier.Properties.DataSource = mtd.GetSuppliersList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "True")
        gluS_Supplier.Properties.DisplayMember = "S_SupplierName"
        gluS_Supplier.Properties.ValueMember = "S_Supplier"
    End Sub
    ''' <summary>
    ''' �����T�w���s�A���`�O��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim ds As New DataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ac As New AcceptanceController
        Dim strA_AccountCheck, strA_PayCheck As String
        isPrint = False
        isExport = False

        If cboA_AccountCheck.Text = "����" Then
            strA_AccountCheck = Nothing
        ElseIf cboA_AccountCheck.Text = "�w�f��" Then
            strA_AccountCheck = "True"
        ElseIf cboA_AccountCheck.Text = "���f��" Then
            strA_AccountCheck = "False"
        End If

        If cboA_PayCheck.Text = "����" Then
            strA_PayCheck = Nothing
        ElseIf cboA_PayCheck.Text = "�w�T�{" Then
            strA_PayCheck = "True"
        ElseIf cboA_PayCheck.Text = "���T�{" Then
            strA_PayCheck = "False"
        End If

        If ac.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, "True", strA_AccountCheck, Nothing, Nothing, gluS_Supplier.EditValue, strA_PayCheck, dteA_SendDateBegin.EditValue, dteA_SendDateEnd.EditValue).Count <= 0 Then
            MsgBox("�����`����L���`�O���I", 64, "����")
            gluS_Supplier.Focus()
            Exit Sub
        End If

        '�P�_�O�_�����L�A�ɥX�v��
        If gluS_Supplier.Text.Trim <> "" Then     '�����Ӭ��ŮɡA�����\���L�A�ɥX
            If gluS_Supplier.EditValue = "S0177" Or gluS_Supplier.EditValue = "S0558" Or gluS_Supplier.EditValue = "S0861" Then    '�u�����w�������Ӥ~�i�H���L�A�ɥX
                Dim pmws As New PermissionModuleWarrantSubController
                Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400319")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "�O" Then
                        isPrint = True
                        isExport = True
                    End If
                End If
            End If
        End If

        ds.Tables.Clear()

        ltc1.CollToDataSet(ds, "Acceptance", ac.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, "True", strA_AccountCheck, Nothing, Nothing, gluS_Supplier.EditValue, strA_PayCheck, dteA_SendDateBegin.EditValue, dteA_SendDateEnd.EditValue))

        PreviewRPT1(ds, "rptAcceptance1", "�禬�O�����`��", dteA_SendDateBegin.Text, dteA_SendDateEnd.Text, isPrint, isExport)
        ltc1 = Nothing

        Me.Close()
    End Sub
    ''' <summary>
    ''' �����ӦW�ٿ�J�ؤ����Ů���u�X�U�Ե��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluS_Supplier_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gluS_Supplier.KeyDown
        If e.KeyCode = Keys.Space Then
            gluS_Supplier.ShowPopup()
        End If
    End Sub
End Class