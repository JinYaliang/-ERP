Imports LFERP.Library.WareHouse.WareQCSend
Imports LFERP.Library.WareHouse.WareQCRecover
Imports LFERP.SystemManager

Public Class frmWareQCRecoverMain

    Private Sub frmWareQCRecoverMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim wqc As New WareQCRecoverController
        Grid1.DataSource = wqc.WareQCRecover_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        LoadPowerUser()
    End Sub
    Sub LoadPowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "50090201")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then WareQCRecoverAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "50090202")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then WareQCRecoverEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "50090203")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then WareQCRecoverDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "50090204")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then WareQCRecoverCheck.Enabled = True
        End If

    End Sub
    Private Sub WareQCRecoverAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WareQCRecoverAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As frmWareQCRecover
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareQCRecover Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "�����k��"
        fr = New frmWareQCRecover
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub WareQCRecoverEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WareQCRecoverEdit.Click
        On Error Resume Next
        Dim qi As List(Of WareQCRecoverInfo)
        Dim qc As New WareQCRecoverController
        qi = qc.WareQCRecover_GetList(GridView1.GetFocusedRowCellValue("WQR_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing)

        If qi(0).WQR_Check = True Then
            MsgBox("�������k�ٳ�w�Q�f�֡A�����\�ק�I")
            Exit Sub
        Else
            Edit = True
            Dim fr As frmWareQCRecover
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmWareQCRecover Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue = "�����k��"
            tempValue2 = GridView1.GetFocusedRowCellValue("WQR_NO").ToString
            fr = New frmWareQCRecover
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
    End Sub

    Private Sub WareQCRecoverDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WareQCRecoverDel.Click
        On Error Resume Next
        Dim qi As List(Of WareQCRecoverInfo)
        Dim qc As New WareQCRecoverController
        qi = qc.WareQCRecover_GetList(GridView1.GetFocusedRowCellValue("WQR_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing)
        If qi(0).WQR_Check = True Then
            MsgBox("�������k�ٳ�w�Q�f�֡A�����\�R���I")
            Exit Sub
        Else
            If MsgBox("�T�w�R���渹��" & GridView1.GetFocusedRowCellValue("WQR_NO").ToString & "�������O���H", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If qc.WareQCRecover_Delete(GridView1.GetFocusedRowCellValue("WQR_NO").ToString, Nothing, Nothing) = True Then
                    MsgBox("�R�����\�I")
                    Grid1.DataSource = qc.WareQCRecover_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                Else
                    MsgBox("�R�����ѡA���ˬd��]�I")
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub WareQCRecoverRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WareQCRecoverRef.Click
        Dim wqc As New WareQCRecoverController
        Grid1.DataSource = wqc.WareQCRecover_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub WareQCRecoverView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WareQCRecoverView.Click
        On Error Resume Next
        Dim fr As frmWareQCRecover
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareQCRecover Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "PreView"
        tempValue2 = GridView1.GetFocusedRowCellValue("WQR_NO").ToString
        fr = New frmWareQCRecover
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub WareQCRecoverQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WareQCRecoverQuery.Click
        Dim wqc As New WareQCRecoverController
        Dim frm As New frmWareQCRecoverSelect
        frm.ShowDialog()
        Select Case tempValue
            Case "1"
                Grid1.DataSource = wqc.WareQCRecover_GetList(tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "2"
                Grid1.DataSource = wqc.WareQCRecover_GetList(Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing)
            Case "3"
                Grid1.DataSource = wqc.WareQCRecover_GetList(Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing)
            Case "4"
                Grid1.DataSource = wqc.WareQCRecover_GetList(Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing)
            Case "5"
                Grid1.DataSource = wqc.WareQCRecover_GetList(Nothing, Nothing, Nothing, Nothing, tempValue2, tempValue3)
        End Select
        tempValue = ""
        tempValue2 = ""
        tempValue3 = ""
    End Sub

    Private Sub WareQCRecoverCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WareQCRecoverCheck.Click
        On Error Resume Next
        Dim fr As frmWareQCRecover
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareQCRecover Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "Check"
        tempValue2 = GridView1.GetFocusedRowCellValue("WQR_NO").ToString
        fr = New frmWareQCRecover
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub Grid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grid1.Click

    End Sub
End Class