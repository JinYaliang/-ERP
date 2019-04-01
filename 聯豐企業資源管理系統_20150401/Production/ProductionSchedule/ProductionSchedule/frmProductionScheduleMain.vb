Imports LFERP.Library.ProductionSchedule
Imports LFERP.SystemManager
Imports LFERP.DataSetting

Public Class frmProductionScheduleMain

    Dim psc As New ProductionScheduleControl

    Private Sub frmProductionScheduleMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim strID As String
        'strID = InUserID

        'Dim upi As List(Of UserPowerInfo)
        'Dim upc As New UserPowerControl

        'upi = upc.UserPower_GetList(strID, Nothing, Nothing, Nothing)
        'If upi.Count = 0 Then
        '    Exit Sub
        'Else

        '    If upi(0).UserRank = "�޲z" Then '�޲z�v���i�H�ݨ�Ҧ����Ͳ��p��
        '        Grid.DataSource = psc.ProductionSchedule_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '    Else   '�Ͳ���--�έp���u��ݨ����Ͳ����o�Ͳ��p��(�q�{�u�����줽�Ǥ���ݨ�)
        '        Grid.DataSource = psc.ProductionSchedule_GetList(Nothing, Nothing, Mid(upi(0).DepID, 1, 1), Nothing, Nothing, Nothing, Nothing, Nothing)
        '    End If

        'End If

        '@ 2012/8/10 �קאּ�եΨ�s�L�{
        cmsProductionRef_Click(Nothing, Nothing)

        PowerUser()
    End Sub

    Sub PowerUser() '�]�m�v��
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880101")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsProductionAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880102")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsProductionEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880103")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsProductionDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880104")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsProductionCheck.Enabled = True
        End If
    End Sub

    '�s�W�Ͳ��p��
    Private Sub cmsProductionAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionAdd.Click
        On Error Resume Next
        Edit = False

        Dim fr As frmProductionSchedule
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionSchedule Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "�Ͳ��p��"
        fr = New frmProductionSchedule
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub

    '�ק���w�Ͳ��p��
    Private Sub cmsProductionEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionEdit.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As List(Of ProductionScheduleInfo)
        pi = psc.ProductionSchedule_GetList(GridView1.GetFocusedRowCellValue("PS_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pi(0).PS_Check = True Then
            MsgBox("���Ͳ��p���w�Q�f��,�����\�ק�")
            Exit Sub
        Else
            Edit = True
            Dim fr As frmProductionSchedule
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionSchedule Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue2 = "�Ͳ��p��"
            tempValue3 = GridView1.GetFocusedRowCellValue("PS_NO").ToString
            fr = New frmProductionSchedule
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
    End Sub

    '�R�����w�Ͳ��p��
    Private Sub cmsProductionDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As List(Of ProductionScheduleInfo)
        pi = psc.ProductionSchedule_GetList(GridView1.GetFocusedRowCellValue("PS_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pi(0).PS_Check = True Then
            MsgBox("���Ͳ��p���w�Q�f��,�����\�R��")
            Exit Sub
        Else
            If MsgBox("�T�w�n�R���s����" & GridView1.GetFocusedRowCellValue("PS_NO").ToString & "���Ͳ��p���ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then
                If psc.ProductionSchedule_Delete(GridView1.GetFocusedRowCellValue("PS_NO").ToString, Nothing) = True Then
                    MsgBox("�R����e�Ͳ��p�����\!")

                    '@ 2012/8/10 �קאּ�եΨ�s�L�{
                    cmsProductionRef_Click(Nothing, Nothing)
                Else
                    MsgBox("�R������,���ˬd��]!")
                End If

                'Grid.DataSource = psc.ProductionSchedule_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                'Dim strID As String
                'strID = InUserID

                'Dim upi As List(Of UserPowerInfo)
                'Dim upc As New UserPowerControl

                'upi = upc.UserPower_GetList(strID, Nothing, Nothing, Nothing)
                'If upi.Count = 0 Then
                '    Exit Sub
                'Else

                '    'If upi(0).UserRank = "�Ͳ���" Then
                '    If upi(0).UserRank = "�޲z" Then
                '        Grid.DataSource = psc.ProductionSchedule_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                '    Else
                '        Grid.DataSource = psc.ProductionSchedule_GetList(Nothing, Nothing, Mid(upi(0).DepID, 1, 1), Nothing, Nothing, Nothing, Nothing, Nothing)
                '    End If

                'End If

            End If
        End If
    End Sub

    '�d�ߥͲ��p��
    Private Sub cmsProductionSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionSelect.Click
        On Error Resume Next

        Dim frm As New frmProductionScheduleFind
        frm.ShowDialog()

        If tempValue8 = Nothing Then Exit Sub

        'Dim upi As List(Of UserPowerInfo)
        'Dim upc As New UserPowerControl

        'upi = upc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)
        'If upi.Count = 0 Then
        '    Exit Sub
        'Else
        '    'If upi(0).UserRank = "�Ͳ���" Then
        '    If upi(0).UserRank = "�޲z" Then
        '        Grid.DataSource = psc.ProductionSchedule_GetList(tempValue, tempValue2, tempValue7, tempValue3, tempValue4, tempValue5, tempValue6, Nothing)
        '    Else
        '        Grid.DataSource = psc.ProductionSchedule_GetList(tempValue, tempValue2, Mid(upi(0).DepID, 1, 1), tempValue3, tempValue4, tempValue5, tempValue6, Nothing)
        '    End If
        'End If

        '@ 2012/8/10 �ק�
        Grid.DataSource = psc.ProductionSchedule_GetList(tempValue, tempValue2, tempValue7, tempValue3, tempValue4, tempValue5, tempValue6, Nothing)

        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing
        tempValue7 = Nothing
    End Sub

    '�d�ݿ�w�Ͳ��p��
    Private Sub cmsProductionView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
       
        Dim fr As frmProductionSchedule
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionSchedule Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "PreView"
        tempValue3 = GridView1.GetFocusedRowCellValue("PS_NO").ToString
        fr = New frmProductionSchedule
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub

    '��s�ާ@
    '���L�{�Q�H�U�L�{�եΡG
    'frmProductionScheduleMain_Load()
    'cmsProductionDel_Click()
    Private Sub cmsProductionRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionRef.Click
        'Grid.DataSource = psc.ProductionSchedule_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)


        'Dim strID As String
        'strID = InUserID

        'Dim upi As List(Of UserPowerInfo)
        'Dim upc As New UserPowerControl

        'upi = upc.UserPower_GetList(strID, Nothing, Nothing, Nothing)
        'If upi.Count = 0 Then
        '    Exit Sub
        'Else

        ' If upi(0).UserRank = "�Ͳ���" Then

        '@ 2012/8/10 �ק�
        If strInUserRank = "�޲z" Then         '�޲z�v���i�H�ݨ�Ҧ����Ͳ��p��
            Grid.DataSource = psc.ProductionSchedule_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), DateAdd(DateInterval.Day, 7, CDate(Format(Now, "yyyy/MM/dd"))), Nothing)
        Else     '�Ͳ���--�έp���u��ݨ����Ͳ����o�Ͳ��p��(�q�{�u�����줽�Ǥ���ݨ�)
            Grid.DataSource = psc.ProductionSchedule_GetList(Nothing, Nothing, strInFacID, Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), DateAdd(DateInterval.Day, 7, CDate(Format(Now, "yyyy/MM/dd"))), Nothing)
        End If

        'End If

    End Sub

    '�f�ֿ�w�Ͳ��p��--�H�K��I
    Private Sub cmsProductionCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionCheck.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As frmProductionSchedule
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionSchedule Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "Check"
        tempValue3 = GridView1.GetFocusedRowCellValue("PS_NO").ToString
        fr = New frmProductionSchedule
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        On Error Resume Next

        Dim frm As New frmProductionSchedule1
        frm.ShowDialog()
    End Sub

    '���ܭ���ƨϥΪ��p
    Private Sub cmsProductionMaterial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionMaterial.Click
        Dim frm As New frmProductionMaterial
        frm.ShowDialog()

    End Sub

End Class