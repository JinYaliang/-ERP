Imports LFERP.Library.Material

Public Class frmPrice

    Private Sub frmPrice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DateEdit1.Text = Format(Now, "yyyy/MM/dd")
    End Sub

    '�O�s���w����d�򤺪�����H��(�N��ɳ���ɤJ���MaterialPrice��)

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Dim mi As List(Of MaterialInfo)
        Dim mc As New MaterialController

        mi = mc.MaterialCode_GetList(Nothing, Nothing, Nothing, "20", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)


        Dim i As Integer
        If mi.Count = 0 Then Exit Sub

        mc.MaterialPrice_Delete(Nothing)

        Dim mi1 As New MaterialInfo

        For i = 0 To mi.Count - 1

            mi1.M_Code = mi(i).M_Code
            mi1.M_Currency = mi(i).M_Currency
            mi1.M_Price = mi(i).M_Price
            mi1.M_ChangeDate = DateEdit1.Text

            mc.MaterialPrice_Add(mi1)


        Next
        MsgBox("��s���Ƴ���H�������I")

        Me.Close()

    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub
End Class