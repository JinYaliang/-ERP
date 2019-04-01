Imports LFERP.Library.Material
Imports System

Public Class frmCodeRelated


    Dim mc As New MaterialController

    Private Sub frmCodeRelated_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label8.Text = "0"
    End Sub

    '�D����
    Private Sub txtMainCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMainCode.LostFocus
        Dim mi As New MaterialInfo
        mi = mc.MaterialCode_Get(txtMainCode.Text)
        If mi Is Nothing Then
            MsgBox("�����ƽs�X���s�b!")
            Label4.Text = Nothing
            Label5.Text = Nothing
            Exit Sub
        Else
            If Mid(mi.Type3ID, 1, 2) = "20" Then

                Label4.Visible = True
                Label5.Visible = True
                Label4.Text = "�W��:" & mi.M_Name
                Label5.Text = "�W��:" & mi.M_Gauge

                Dim mi1 As New MaterialSubInfo
                mi1 = mc.MaterialCodeSub_Get(txtMainCode.Text, Nothing)

                If mi1 Is Nothing Then
                Else
                    txtSubCode.Text = mi1.M_CodeSub
                    txtRatio.Text = mi1.M_Qty
                    Label8.Text = mi1.AutoID
                    Label6.Text = "�W��:" & mc.MaterialCode_Get(mi1.M_CodeSub).M_Name
                    Label7.Text = "�W��:" & mc.MaterialCode_Get(mi1.M_CodeSub).M_Gauge
                End If
            Else
                MsgBox("�п�J���������ƪ����ƽs�X!")
                txtMainCode.Text = Nothing
                txtMainCode.Focus()

            End If

         
        End If
    End Sub
    '�l����
    Private Sub txtSubCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubCode.LostFocus
        Dim mi As New MaterialInfo
        mi = mc.MaterialCode_Get(txtSubCode.Text)
        If mi Is Nothing Then
            MsgBox("�����ƽs�X���s�b,�п�J���T�s�X!")
            Exit Sub
        Else
            Label6.Visible = True
            Label7.Visible = True
            Label6.Text = "�W��:" & mi.M_Name
            Label7.Text = "�W��:" & mi.M_Gauge
        End If
    End Sub

    Private pattern As String = "^[0-9]*$"

    Private Sub txtRatio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRatio.TextChanged
        Dim m As New System.Text.RegularExpressions.Regex(pattern)
        ' �ǰt���h��F��
        If m.IsMatch(txtRatio.Text) = False Then
            ' ��J�����O�Ʀr
            Me.txtRatio.Text = Nothing
            ' textBox���e����
            ' �N���Щw���奻�س̫�
            Me.txtRatio.SelectionStart = Me.txtRatio.Text.Length

        End If

    End Sub

    Sub DataNew()

        Dim objinfo As New MaterialSubInfo

        objinfo.M_Code = txtMainCode.Text
        objinfo.M_CodeSub = txtSubCode.Text
        objinfo.M_Qty = txtRatio.Text

        If mc.MaterialCodeSub_Add(objinfo) = True Then
            MsgBox("�O�s���\!", , "����")
        Else
            MsgBox("�O�s����,���ˬd���~!", , "����")
        End If

    End Sub

    Sub DataEdit()

        Dim objinfo As New MaterialSubInfo

        objinfo.M_Code = txtMainCode.Text
        objinfo.M_CodeSub = txtSubCode.Text
        objinfo.M_Qty = txtRatio.Text

        If mc.MaterialCodeSub_Update(objinfo) = True Then
            MsgBox("�O�s���\!", , "����")
        Else
            MsgBox("�O�s����,���ˬd���~!", , "����")
        End If

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If CInt(Label8.Text) > 0 Then
            DataEdit()
        Else
            DataNew()
        End If
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
End Class