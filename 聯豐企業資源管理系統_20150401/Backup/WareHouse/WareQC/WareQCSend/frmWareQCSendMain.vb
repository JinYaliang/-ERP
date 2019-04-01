Imports LFERP.Library.WareHouse.WareQCSend
Imports LFERP.SystemManager

Public Class frmWareQCSendMain

    Private Sub frmWareQCSendMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim wqc As New WareQCSendController
        Grid1.DataSource = wqc.WareQCSend_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        LoadPowerUser()
    End Sub
    Sub LoadPowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "50090101")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then QCSendAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "50090102")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then QCSendEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "50090103")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then QCSendDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "50090104")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then QCSendCheck.Enabled = True
        End If
    End Sub

    Private Sub QCSendAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QCSendAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As frmWareQCSend
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareQCSend Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "���˵o�X"
        fr = New frmWareQCSend
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub

    Private Sub QCSendEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QCSendEdit.Click
        On Error Resume Next
        Dim qi As List(Of WareQCSendInfo)
        Dim qc As New WareQCSendController
        qi = qc.WareQCSend_GetList(GridView1.GetFocusedRowCellValue("WQS_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If qi(0).WQS_Check = True Then
            MsgBox("�����˵o�X��w�Q�f�֡A�����\�ק�I")
            Exit Sub
        Else
            Edit = True
            Dim fr As frmWareQCSend
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmWareQCSend Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue = "���˵o�X"
            tempValue2 = GridView1.GetFocusedRowCellValue("WQS_NO").ToString
            fr = New frmWareQCSend
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
       
    End Sub

    Private Sub QCSendDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QCSendDel.Click
        On Error Resume Next
        Dim qi As List(Of WareQCSendInfo)
        Dim qc As New WareQCSendController
        qi = qc.WareQCSend_GetList(GridView1.GetFocusedRowCellValue("WQS_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If qi(0).WQS_Check = True Then
            MsgBox("�����˵o�X��w�Q�f�֡A�����\�R���I")
            Exit Sub
        Else
            If MsgBox("�T�w�R���渹��" & GridView1.GetFocusedRowCellValue("WQS_NO").ToString & "�������O���H", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If qc.WareQCSend_Delete(GridView1.GetFocusedRowCellValue("WQS_NO").ToString, Nothing, Nothing) = True Then
                    MsgBox("�R�����\�I", , "����")
                    Grid1.DataSource = qc.WareQCSend_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                Else
                    MsgBox("�R�����ѡA���ˬd��]�I", , "����")
                    Exit Sub
                End If
            End If

        End If
    End Sub

    Private Sub QCSendQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QCSendQuery.Click
        Dim wqc As New WareQCSendController
        Dim frm As New frmWareQCSendSelect
        frm.ShowDialog()

        Select Case tempValue

            Case "1"
                Grid1.DataSource = wqc.WareQCSend_GetList(tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            Case "2"
                Grid1.DataSource = wqc.WareQCSend_GetList(Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "3"
                Grid1.DataSource = wqc.WareQCSend_GetList(Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing)
            Case "4"
                Grid1.DataSource = wqc.WareQCSend_GetList(Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing)
            Case "5"
                Grid1.DataSource = wqc.WareQCSend_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, tempValue3)
        End Select
        tempValue = ""
        tempValue2 = ""
        tempValue3 = ""
    End Sub

    Private Sub QCSendView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QCSendView.Click
        On Error Resume Next
        Dim fr As frmWareQCSend
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareQCSend Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "PreView"
        tempValue2 = GridView1.GetFocusedRowCellValue("WQS_NO").ToString
        fr = New frmWareQCSend
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub QCSendCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QCSendCheck.Click
        On Error Resume Next
        Dim fr As frmWareQCSend
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareQCSend Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "Check"
        tempValue2 = GridView1.GetFocusedRowCellValue("WQS_NO").ToString
        fr = New frmWareQCSend
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub QCSendRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QCSendRef.Click
        Dim wqc As New WareQCSendController
        Grid1.DataSource = wqc.WareQCSend_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub
End Class