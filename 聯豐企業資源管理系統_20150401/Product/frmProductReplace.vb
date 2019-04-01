Imports LFERP.Library.Product

Public Class frmProductReplace

    Dim ds As New DataSet

    Private Sub frmProductReplace_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cmdReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReplace.Click

        If Len(TextEdit1.Text) = 0 Then
            MsgBox("�߸����ର��,�п�J�߸��I", 64, "����")
            TextEdit1.Focus()
            Exit Sub
        ElseIf Len(TextEdit2.Text) = 0 Then
            MsgBox("�߸����ର��,�п�J�߸��I", 64, "����")
            TextEdit2.Focus()
            Exit Sub
        End If

        Dim pbi As List(Of ProductBomInfo)
        Dim pbc As New ProductBomController
        pbi = pbc.ProductBom_GetList(TextEdit1.Text, Nothing, Nothing, Nothing, Nothing, Nothing) '�o����w�߸����t����
        If pbi.Count = 0 Then
            MsgBox("���߸����s�b�A�Э��s��J�I", 64, "����")
            TextEdit1.Focus()
            TextEdit1.SelectAll()
            Exit Sub
        End If

        Dim mtcBomSub As New ProductController
        If mtcBomSub.Product_GetList(TextEdit2.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing).Count = 0 Then
            MsgBox("���߸����s�b,�����\��s�t��즹�߸���!", 64, "����")
            TextEdit2.Focus()
            TextEdit2.SelectAll()
            Exit Sub
        End If
        If MsgBox("�A�T�w�n��s " & TextEdit2.Text & " �o�Ӳ��~��Ƥ��t���ƶ�?", MsgBoxStyle.YesNo, "����") = MsgBoxResult.No Then Exit Sub

        Dim mtcbomsub1 As New ProductBomController
        mtcbomsub1.ProductBom_Delete(TextEdit2.Text, Nothing) '���R���ݭn��s���߸����t��

        Dim mti As List(Of ProductBomInfo)
        Dim mtc As New ProductBomController

        Dim mti1 As New ProductBomInfo

        mti = mtc.ProductBom_GetList(TextEdit1.Text, Nothing, Nothing, Nothing, Nothing, Nothing) '�o����w�߸����t����

        If mti.Count = 0 Then
            Exit Sub
        Else
            Dim i As Integer
            For i = 0 To mti.Count - 1

                mti1.PM_M_Code = TextEdit2.EditValue
                mti1.PM_ID = mti(i).PM_ID
                mti1.M_Code = mti(i).M_Code
                mti1.M_CodePID = mti(i).M_CodePID
                mti1.PM_Qty = mti(i).PM_Qty
                mti1.PM_MakeRemark = mti(i).PM_MakeRemark
                mti1.PM_Make = mti(i).PM_Make
                mti1.PM_Check = mti(i).PM_Check
                mti1.M_Supplier = mti(i).M_Supplier
                mti1.M_SupplierNo = mti(i).M_SupplierNo
                mti1.PM_Key = mti(i).PM_Key
                mti1.PM_PID = mti(i).PM_PID
                mti1.PM_LVL = mti(i).PM_LVL
                mti1.M_CodeMouldNO = mti(i).M_CodeMouldNO
                mti1.M_CodeType = mti(i).M_CodeType

                mtc.ProductBom_Add(mti1)

            Next
            MsgBox("��s�t��H�������I", 64, "����")
        End If
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    'Private Sub TextEdit2_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextEdit2.Enter
    '    If Trim(TextEdit1.Text) <> "" Then
    '        Dim pbi As List(Of ProductBomInfo)
    '        Dim pbc As New ProductBomController
    '        pbi = pbc.ProductBom_GetList(TextEdit1.Text, Nothing, Nothing, Nothing, Nothing, Nothing) '�o����w�߸����t����
    '        If pbi.Count = 0 Then
    '            MsgBox("���߸����s�b�A�Э��s��J�I", 64, "����")
    '            TextEdit1.SelectAll()
    '            Exit Sub
    '        End If
    '    End If
    'End Sub

End Class