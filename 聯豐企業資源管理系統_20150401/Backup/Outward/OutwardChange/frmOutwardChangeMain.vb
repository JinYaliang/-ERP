Imports LFERP.Library.Outward

Public Class frmOutwardChangeMain

    Dim occ As New OutwardChangeControl

    Private Sub frmOutwardChangeMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Grid1.DataSource = occ.OutwardChange_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, False)
    End Sub
    '�]�m�Τᦹ�Ҷ��v��
    Sub LoadPowerUser()


    End Sub

    Private Sub popChangeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As frmOutwardChange
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmOutwardChange Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "����"
        fr = New frmOutwardChange
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub
    '�ק�
    Private Sub popChangeEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeEdit.Click
        On Error Resume Next
        Edit = True
        If GridView1.RowCount = 0 Then Exit Sub
        Dim ci1 As List(Of OutwardChangeInfo)
        ci1 = occ.OutwardChange_GetList(GridView1.GetFocusedRowCellValue("OC_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing)
        If ci1(0).OC_Check = True Then
            MsgBox("������w�f�֡A�����\�ק�")
            Exit Sub
        Else
            Dim fr As frmOutwardChange
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmOutwardChange Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue = "����"
            tempValue3 = GridView1.GetFocusedRowCellValue("OC_NO").ToString
            fr = New frmOutwardChange
            fr.MdiParent = MDIMain
            fr.Show()
        End If
    End Sub
    '�R��
    Private Sub popChangeDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim ci1 As List(Of OutwardChangeInfo)
        ci1 = occ.OutwardChange_GetList(GridView1.GetFocusedRowCellValue("OC_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing)
        If ci1(0).OC_Check = True Then
            MsgBox("������w�f��,�����\�R���I")
            Exit Sub
        Else
            If MsgBox("�T�w�R���渹��" & GridView1.GetFocusedRowCellValue("OC_NO").ToString & "�������O���H", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If occ.OutwardChange_Delete(GridView1.GetFocusedRowCellValue("OC_NO").ToString, Nothing) = True Then

                    '*----------------�Y�A�Ψ���ɫh�ݧR�������椤���ɫH��---------------------*

                    '*---------------------------------------------------------------------------*

                    MsgBox("�R�����\�I")
                    Grid1.DataSource = occ.OutwardChange_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, False)
                Else
                    MsgBox("�R�����ѡA���ˬd��]�I")
                    Exit Sub
                End If
            End If
        End If
    End Sub
    '�d��
    Private Sub popChangeView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmOutwardChange
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmOutwardChange Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "PreView"
        tempValue3 = GridView1.GetFocusedRowCellValue("OC_NO").ToString
        fr = New frmOutwardChange
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub
    '�f��
    Private Sub popChangeCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeCheck.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmOutwardChange
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmOutwardChange Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "Check"
        tempValue3 = GridView1.GetFocusedRowCellValue("OC_NO").ToString
        fr = New frmOutwardChange
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub
    '��s
    Private Sub popChangeRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeRef.Click
        Grid1.DataSource = occ.OutwardChange_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, False)
    End Sub
    '�d��
    Private Sub popChangeSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeSeek.Click
        Dim myfrm As New frmOutwardChangeSelect
        myfrm.ShowDialog()

        Grid1.DataSource = occ.OutwardChange_GetList(tempValue, tempValue2, tempValue3, tempValue4, tempValue5, tempValue6)

        tempValue = ""
        tempValue2 = ""
        tempValue3 = ""
        tempValue4 = ""
        tempValue5 = ""
        tempValue6 = ""

    End Sub
    '��e����@����
    Private Sub popChangePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangePrint.Click

    End Sub
    '���J����
    Private Sub popChangeFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeFile.Click

    End Sub
End Class