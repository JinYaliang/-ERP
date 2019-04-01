Imports System
Imports LFERP.SystemManager
Imports LFERP.Library
Imports LFERP.Library.PieceProcess
Public Class FrmPieceMoPersonnel
    Dim pc As New PieceProcess.PersonnelControl
    Dim pi As New PieceProcess.PersonnelInfo
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Edit = False
        tempValue2 = "���u���J"
        Dim fr As New FrmPersonnelSub
        fr.ShowDialog()
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Edit = True
        tempValue2 = "���u���J"
        tempValue = GridView1.GetFocusedRowCellValue("Per_NO")
        Dim fr As New FrmPersonnelSub
        fr.ShowDialog()
    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        If MsgBox("�T�w�n�R���t�ҽs����:" & GridView1.GetFocusedRowCellValue("Per_NO"), MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        If pc.PieceMoPersonnel_Del(GridView1.GetFocusedRowCellValue("Per_NO"), Nothing) Then
            MsgBox("�w�R�����\�I")
        End If
        Grid.DataSource = pc.PieceMoPersonnel_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub FrmPieceMoPersonnel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        Grid.DataSource = pc.PieceMoPersonnel_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub
    '�]�m�v��
    Sub PowerUser()

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860201")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860202")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860203")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdDel.Enabled = True
        End If
      

    End Sub
    Private Sub cmdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        tempValue2 = "View"
        tempValue = GridView1.GetFocusedRowCellValue("Per_NO")
        Dim fr As New FrmPersonnelSub
        fr.ShowDialog()
    End Sub

    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        tempValue = "�p��W��-�H��"
        Dim fr As New FrmPieceSelect
        fr.ShowDialog()

    End Sub

    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        Grid.DataSource = pc.PieceMoPersonnel_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub ccmdCiGong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ccmdCiGong.Click
        If MsgBox("�A�T��O�n�B�z�o�ӭ��u��?", vbYesNo + vbQuestion, "��u�B�z") = vbNo Then
        Else
            pi.Per_NO = GridView1.GetFocusedRowCellValue("Per_NO")
            If pc.PieceMoPersonnel_GetList(GridView1.GetFocusedRowCellValue("Per_NO"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)(0).Per_Dismiss = False Then
                pi.Per_Dismiss = True
            Else
                pi.Per_Dismiss = False
            End If
            If pc.PieceMoPersonnel2_Update(pi) = False Then
                MsgBox("���u��u�B�z�X��!")
                Exit Sub
            End If
        End If
    End Sub
End Class