Imports LFERP.Library.ProductionOutProcess
Imports LFERP.Library.ProductionOWPAcceptance

Public Class frmProductionOutRetrocede


    Private Sub frmProductionOutRetrocede_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LabAutoID.Text = tempValue
        If tempValue2 = "�j�f" Then
            lblTittle.Text = "����(�j�f)"
        Else
            lblTittle.Text = "����(���)"
        End If

        tempValue = Nothing
        tempValue2 = Nothing

        LoadData()

        txtN_Qty.Select()
        txtN_Qty.Focus()
    End Sub


    Sub LoadData()
        Dim poi As List(Of ProductionOutProcessInfo)
        Dim poc As New ProductionOutProcessControl
        poi = poc.ProductionOutProcess_GetList(Me.LabAutoID.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '�P�_�ƾڮw���O�_�s�b���~�o�[�u��(�~�o�[�u�榳�i��Q�H�R����)
        If poi.Count <> 1 Then
            Exit Sub
        End If
        Me.txtPM_Code.Text = poi(0).PM_M_Code
        Me.txtPM_Type.Text = poi(0).PM_Type

        Me.txtPSName.Text = poi(0).PS_Name
        Me.txtOW_DO.Text = poi(0).OW_Do

        Me.txtSuppName.Text = poi(0).S_SupplierName
        Me.txtR_Qty.Text = poi(0).PO_Qty

        Me.LabAutoID.Text = poi(0).AutoID
        Me.txtPO_ID.Text = poi(0).PO_ID

    End Sub

    Sub SaveData()

        Dim ac As New ProductionOWPAcceptanceControl
        Dim al As New List(Of ProductionOWPAcceptanceInfo)
        al = ac.ProductionOWPAcceptance_GetList(Me.LabAutoID.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If al.Count = 0 Then
        Else
            MsgBox("���~�o��w���禬�O��,������ƶq!", 64, "����")
            Exit Sub
        End If

        Dim pcc As New ProductionOutProcessControl
        Dim pii As New ProductionOutProcessInfo

        pii.RecordNO = Me.LabAutoID.Text
        pii.R_Qty = Val(Me.txtR_Qty.Text)
        pii.N_Qty = Val(Me.txtN_Qty.Text)

        pii.R_Action = InUserID
        pii.R_Date = Format(Now, "yyyy-MM-dd HH:mm:ss")
        pii.R_Remark = txtPO_Remark.Text
        If pcc.ProductionOutRetrocede_Add(pii) = True Then
            MsgBox("�O�s���\!")
            Me.Close()
        Else
            MsgBox("�O�s����!")
        End If


    End Sub



    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        SaveData()
    End Sub
End Class