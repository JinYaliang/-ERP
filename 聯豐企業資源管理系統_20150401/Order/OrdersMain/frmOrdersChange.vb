Imports LFERP.DataSetting
Imports LFERP.Library.Orders
Imports DevExpress.XtraGrid.Columns
Imports LFERP.Library.Product
Imports LFERP.Library.OrdersChange
Imports LFERP.Library.Outwards

Public Class frmOrdersChange

    Dim strType As String


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If labOM_ID.Text = "" Then
            MsgBox("�q��y��������!")
            Exit Sub
        End If

        If strType = "M" Then ''�קﲣ�~�s��
            If GridLookNewCode.EditValue = "" Then
                GridLookNewCode.Focus()
                MsgBox("�s�s�X���ର�šI")
                Exit Sub
            End If

            If txtOldm_code.EditValue = "" Then
                txtOldm_code.Focus()
                MsgBox("�½s�X���ର�šI")
                Exit Sub
            End If


            If GridLookNewCode.EditValue = txtOldm_code.Text Then
                GridLookNewCode.Focus()
                MsgBox("�s�s�X,�P�½s�X����ۦP�I")
                Exit Sub
            End If

            If txtRemark.Text = "" Then
                txtRemark.Select()
                MsgBox("�п�J�ƪ`�H��!")
                Exit Sub
            End If


            '-------------------------------------------------------

            Dim OW As New OutWardsController
            Dim OL As New List(Of OutWardsInfo)
            OL = OW.OutWards_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, labOM_ID.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If OL.Count <= 0 Then
            Else
                MsgBox("���q��y����,�w���e�f�O��!")
                Exit Sub
            End If

            Dim osc As New OrdersSubController
            Dim osl As New List(Of OrdersSubInfo)

            osl = osc.OrdersSub_GetList(labOM_ID.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If osl.Count > 0 Then
                Dim k As Integer

                For k = 0 To osl.Count - 1
                    Dim obomc As New OrdersBomController
                    Dim obocl As New List(Of OrdersBomInfo)
                    obocl = obomc.OrdersBom_GetList(Nothing, osl(k).OS_BatchID, Nothing, Nothing)

                    If obocl.Count > 0 Then
                        MsgBox("���q��y����,�����妸�w�b���ƲM�椤�s�b!")
                        Exit Sub
                    End If
                Next
            End If

            '---------------------------------------------------------------------
        End If


        If strType = "Q" Then ''�ק良���
            If txtNOSendold.Text = "" Then
                txtNOSendold.Focus()
                MsgBox("�ק�e�ƶq���ର�šI")
                Exit Sub
            End If

            If txtNOSendnew.EditValue = "" Or Val(txtNOSendnew.Text) < 0 Then
                txtNOSendnew.Focus()
                MsgBox("�ק�Z���ƶq��J���~�I")
                Exit Sub
            End If


            If Val(txtNOSendold.Text) <= Val(txtNOSendnew.Text) Then
                txtNOSendnew.Focus()
                MsgBox("�ק�Z�ƶq����j�󵥩�ק�e�ƶq�I")
                Exit Sub
            End If

            If txtRrmarkQ.Text = "" Then
                txtRrmarkQ.Select()
                MsgBox("�п�J�ƪ`�H��!")
                Exit Sub
            End If
        End If



        Dim oc As New OrdersChangeControl
        Dim oi As New OrdersChangeInfo

        oi.OM_ID = labOM_ID.Text


        If strType = "M" Then ''�ק良���
            oi.PM_M_CodeNew = GridLookNewCode.EditValue
            oi.PM_M_CodeOld = txtOldm_code.Text
            oi.Remark = txtRemark.Text
        End If

        If strType = "Q" Then ''�ק良���
            oi.PM_M_CodeNew = txtNOSendnew.Text
            oi.PM_M_CodeOld = txtNOSendold.Text
            oi.Remark = txtRrmarkQ.Text
        End If


        oi.OrderDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        oi.ChangeType = strType
        oi.U_ID = InUserID

        If oc.OrdersChange_Add1(oi) = True Then
            MsgBox("�O�s���\�I")
        Else
            MsgBox("�O�s���ѡI")
            Exit Sub
        End If

        Me.Close()


    End Sub

    Private Sub frmOrdersChange_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        labOM_ID.Text = ""


        strType = tempValue3
        If strType = "M" Then
            Dim mc As New ProductController
            GridLookNewCode.Properties.DisplayMember = "PM_M_Code"
            GridLookNewCode.Properties.ValueMember = "PM_M_Code"
            GridLookNewCode.Properties.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            labOM_ID.Text = tempValue
            txtOldm_code.EditValue = tempValue2

            Me.XtraTabPage1.PageVisible = True
            Me.XtraTabPage2.PageVisible = False

        ElseIf strType = "Q" Then
            labOM_ID.Text = tempValue
            txtNOSendold.EditValue = tempValue2
            Me.XtraTabPage2.PageVisible = True
            Me.XtraTabPage1.PageVisible = False
        End If

        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        '----------------------------------------------------


    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

 
End Class