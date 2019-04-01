Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.PieceProcessMoSub
Public Class FrmPieceProcessMain
    Dim pc As New PieceProcessMoSubControl
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next
        Dim fr As New FrmPieceImport
        fr.ShowDialog()
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pc As New Library.PieceProcessMoSub.PieceProcessMoSubControl
        Dim piL As List(Of Library.PieceProcessMoSub.PieceProcessMoSubInfo)
        piL = pc.PieceProcessMo_GetList(GridView1.GetFocusedRowCellValue("PM_NO"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If piL(0).PM_Check Then
            MsgBox("���p��u���y�{��w�f�֡A�����\�ק�I")
            Exit Sub
        End If

        Edit = True
        Dim fr As FrmPieceProcessMainSub
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is FrmPieceProcessMainSub Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "�p��u���y�{��"
        tempValue2 = GridView1.GetFocusedRowCellValue("PM_NO").ToString
        fr = New FrmPieceProcessMainSub
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pc As New PieceProcessMoSubControl
        Dim piL As List(Of PieceProcessMoSubInfo)
        piL = pc.PieceProcessMo_GetList(GridView1.GetFocusedRowCellValue("PM_NO"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If piL(0).PM_Check Then
            MsgBox("���p��y�{��w�f�֡A�����\�R���I")
            Exit Sub
        End If
        If MsgBox("�T�w�n�R���渹��:" & GridView1.GetFocusedRowCellValue("PM_NO"), MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        If pc.PieceProcessMo_Del(GridView1.GetFocusedRowCellValue("PM_NO")) Then
            MsgBox("�w�R�����\�I")
        End If
        Grid.DataSource = pc.PieceProcessMo_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub cmdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As FrmPieceProcessMainSub
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is FrmPieceProcessMainSub Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "�d��"
        tempValue2 = GridView1.GetFocusedRowCellValue("PM_NO").ToString
        fr = New FrmPieceProcessMainSub
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub

    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As FrmPieceProcessMainSub
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is FrmPieceProcessMainSub Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "�f��"
        tempValue2 = GridView1.GetFocusedRowCellValue("PM_NO").ToString
        fr = New FrmPieceProcessMainSub
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub

    Private Sub FrmPieceProcessMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Grid.DataSource = pc.PieceProcessMo_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        PowerUser()
    End Sub
    '�]�m�v��
    Sub PowerUser()

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860301")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860302")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860303")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860304")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdCheck.Enabled = True
        End If

    End Sub

    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        Grid.DataSource = pc.PieceProcessMo_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click

    End Sub
End Class