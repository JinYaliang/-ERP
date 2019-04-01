Imports LFERP.Library.KnifeWare
Public Class frmKnifeGroupAdd

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If txtG_Name.Text = "" Then
            MsgBox("�էO�W�٤��ର��!", 64, "����")
            Exit Sub
        End If

        If txtWH.Tag = "" Then
            MsgBox("�п�ܭܮw!", 64, "����")
            Exit Sub
        End If

        ''-----------------------------------------------------------
        Dim kiL As New List(Of KnifeGroupInfo)
        Dim kcL As New KnifeGroupControl
        kiL = kcL.KnifeGroup_GetList(txtWH.Tag & Me.txtG_Name.Text, Nothing, txtWH.Tag)
        If kiL.Count <= 0 Then
        Else
            MsgBox("��e�ܮw:" & txtWH.Text & "   �w�s���էO,���ˬd�I", 64, "����")
            Exit Sub
        End If
        ''-----------------------------------------------------------
        Dim ki As New KnifeGroupInfo
        Dim kc As New KnifeGroupControl
        ki.WH_ID = txtWH.Tag
        ki.G_Name = Me.txtG_Name.Text
        ki.G_NO = txtWH.Tag & Me.txtG_Name.Text
        ki.Remark = Format(Now, "yyyy/MM/dd HH:mm:ss" & ";" & InUserID & "�ާ@")

        If kc.KnifeGroup_Add(ki) = True Then
            MsgBox("�O�s���\!", 64, "����")
            Me.Close()
        Else
            MsgBox("�O�s����,���ˬd�I", 64, "����")
        End If

    End Sub

    Private Sub frmKnifeGroupAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtWH.Enabled = False
        txtWH.Tag = tempValue
        txtWH.Text = tempValue2

        tempValue = Nothing
        tempValue2 = Nothing

        txtG_Name.Select()

    End Sub
End Class