Imports LFERP.Library.WareHouse.WareChange
Imports LFERP.SystemManager

Public Class frmWareChangeMain

    Dim cc As New WareChangeControl

    Private Sub frmWareChangeMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Grid1.DataSource = cc.WareChange_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
        PowerUser()

    End Sub

    Sub PowerUser()

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "5006101")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popChangeAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "5006102")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popChangeEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "5006103")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popChangeDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "5006104")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popChangeCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "5006105")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popChangeReCheck.Enabled = True
        End If

    End Sub

    Private Sub popChangeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeAdd.Click
        On Error Resume Next

        Edit = False

        Dim fr As frmWareChange
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareChange Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "WareChange"
        fr = New frmWareChange
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
      
    End Sub

    Private Sub popChangeEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeEdit.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub

        Dim ci As List(Of WareChangeInfo)
        ci = cc.WareChange_GetList(GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing, Nothing, Nothing, Nothing)
        If ci.Count = 0 Then Exit Sub
        If ci(0).C_Check = True Then
            MsgBox("����w�f�֤����\�ק�", , "����")
            Exit Sub
        End If
        Edit = True
        Dim fr As frmWareChange
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareChange Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue = "WareChange"
        tempValue2 = GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString
        fr = New frmWareChange
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popChangeDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeDel.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim ci As List(Of WareChangeInfo)
        ci = cc.WareChange_GetList(GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing, Nothing, Nothing, Nothing)
        If ci.Count = 0 Then Exit Sub
        If ci(0).C_Check = True Then
            MsgBox("����w�f�֤����\�R��!", , "����")
            Exit Sub
        End If
        If MsgBox("�T�w�R���渹��" & GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString & "�������O���H", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If cc.WareChange_Delete(GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing) = True Then
                MsgBox("�R�����榨�\!", , "����")

                Grid1.DataSource = cc.WareChange_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
            End If

        End If

    End Sub

    Private Sub popChangeView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As frmWareChange
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareChange Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue = "PreView"
        tempValue2 = GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString
        fr = New frmWareChange
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popChangeCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeCheck.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub

        Dim ci As List(Of WareChangeInfo)
        ci = cc.WareChange_GetList(GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing, Nothing, Nothing, Nothing)
        If ci.Count = 0 Then Exit Sub
        If ci(0).C_Check = True Then
            MsgBox("����w�f�֤����\�A���f��", , "����")
            Exit Sub
        End If
        Dim fr As frmWareChange
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareChange Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue = "Check"
        tempValue2 = GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString
        fr = New frmWareChange
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popChangeRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeRef.Click
        Grid1.DataSource = cc.WareChange_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub popChangeSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeSeek.Click

        Dim Myfrm As New frmWareChangeSelect
        Myfrm.ShowDialog()

        Grid1.DataSource = cc.WareChange_GetList(tempValue2, tempValue4, tempValue5, tempValue, Nothing)

        tempValue = ""
        tempValue2 = ""
        tempValue4 = ""
        tempValue5 = ""

    End Sub
    '�������---�ܮw���w�s��(�ռ�)
    Private Sub popChangePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangePrint.Click
        Dim ds As New DataSet
        If GridView1.RowCount = 0 Then Exit Sub

        Dim ltc1 As New CollectionToDataSet

        Dim Cmc As New LFERP.Library.WareHouse.WareChange.WareChangeControl
     
        ds.Tables.Clear()

        ltc1.CollToDataSet(ds, "WareChange", Cmc.WareChange_GetList(GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing, Nothing, Nothing, Nothing))

        PreviewRPT(ds, "rptWareChange", "����--" & GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, True, True)


        ltc1 = Nothing
    End Sub

    '���[���---�w����ܮw�w�s�ƶq���Ƶ�
    Private Sub popChangeFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeFile.Click

        Dim open, update, down, edit, del, detail As Boolean
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

        If GridView1.RowCount = 0 Then Exit Sub
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "505001")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then update = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then update = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "505002")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then down = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then down = False
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "505003")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then edit = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then edit = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "505004")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then del = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then del = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "505005")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then detail = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then detail = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "505006")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then open = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then open = False
        End If

        FileShow("50061", GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, open, update, down, edit, del, detail)
    End Sub

    Private Sub popChangeReCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeReCheck.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub

        Dim ci As List(Of WareChangeInfo)
        ci = cc.WareChange_GetList(GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing, Nothing, Nothing, Nothing)
        If ci.Count = 0 Then Exit Sub
        If ci(0).C_Check = False Then
            MsgBox("���楼�f�֤����\�_��", , "����")
            Exit Sub
        End If
        Dim fr As frmWareChange
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareChange Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue = "ReCheck"
        tempValue2 = GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString
        fr = New frmWareChange
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
End Class