Imports System
Imports LFERP.SystemManager
Imports LFERP.Library
Imports LFERP.Library.PieceProcess
Public Class FrmPieceTimePersonnel

    Dim pc As New PieceProcess.PersonnelControl
    Dim pi As New PieceProcess.PersonnelInfo


    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Edit = False
        tempValue2 = "員工計時"
        Dim fr As New FrmTimePersonnelSub
        fr.ShowDialog()
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Edit = True
        tempValue2 = "員工計時"
        tempValue = GridView1.GetFocusedRowCellValue("PT_NO")
        Dim fr As New FrmTimePersonnelSub
        fr.ShowDialog()
    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        If MsgBox("確定要刪除廠證編號為:" & GridView1.GetFocusedRowCellValue("PT_NO"), MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        If pc.PieceTimePersonnel_Del(GridView1.GetFocusedRowCellValue("PT_NO")) Then
            MsgBox("已刪除成功！")
        End If
        Grid.DataSource = pc.PieceTimePersonnel_GetList(Nothing, Nothing)
    End Sub

    Private Sub cmdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        tempValue2 = "View"
        tempValue = GridView1.GetFocusedRowCellValue("PT_NO")
        Dim fr As New FrmTimePersonnelSub
        fr.ShowDialog()
    End Sub

    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        Dim fr As New FrmPieceSelect
        fr.ShowDialog()
    End Sub

    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        Grid.DataSource = pc.PieceTimePersonnel_GetList(Nothing, Nothing)
    End Sub

    Private Sub FrmPieceTimePersonnel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        Grid.DataSource = pc.PieceTimePersonnel_GetList(Nothing, Nothing)
    End Sub
    '設置權限
    Sub PowerUser()

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860501")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860502")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860503")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdDel.Enabled = True
        End If


    End Sub
End Class