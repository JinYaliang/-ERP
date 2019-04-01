Imports System
Imports LFERP.SystemManager
Imports LFERP.Library
Imports LFERP.Library.PieceProcess

Public Class FrmPieceMoWorkGroup
    Dim pc As New PieceProcess.WorkGroupControl
    Dim pi As New PieceProcess.WorkGroupInfo

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Edit = False
        tempValue2 = "�էO���J"
        Dim fr As New FrmWorkGroupSub
        fr.ShowDialog()
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Edit = True
        tempValue2 = "�էO���J"
        tempValue = GridView1.GetFocusedRowCellValue("Wg_NO")
        Dim fr As New FrmWorkGroupSub
        fr.ShowDialog()
    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        If MsgBox("�T�w�n�R���էO�s����:" & GridView1.GetFocusedRowCellValue("Wg_NO"), MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        If pc.PieceMoWorkGroup_Del(GridView1.GetFocusedRowCellValue("Wg_NO")) Then
            MsgBox("�w�R�����\�I")
        End If
        Grid.DataSource = pc.PieceMoWorkGroup_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
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
        tempValue = GridView1.GetFocusedRowCellValue("Wg_NO")
        Dim fr As New FrmWorkGroupSub
        fr.ShowDialog()
    End Sub

    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        Dim fr As New FrmPieceSelect
        fr.ShowDialog()

    End Sub

    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        Grid.DataSource = pc.PieceMoWorkGroup_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub FrmPieceMoWorkGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Grid.DataSource = pc.PieceMoWorkGroup_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '�]�m�v��
        PowerUser()
    End Sub
End Class