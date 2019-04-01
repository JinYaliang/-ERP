Imports LFERP.Library.Purchase.Change
Imports LFERP.SystemManager
Imports LFERP.FileManager

Public Class frmChangeMain

    Dim ci As New ChangeInfo
    Dim cc As New ChangeControl

    Private Sub frmChangeMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Grid1.DataSource = cc.Change_GetList(Nothing, Nothing, Nothing, False)
        LoadUserPower()
    End Sub
    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400501")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                popChangeAdd.Enabled = True
                'popChangePurchaseAdd.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400502")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popChangeEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400503")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popChangeDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400504")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popChangeCheck.Enabled = True
        End If
    End Sub
    ''' <summary>
    ''' �ȧ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popChangeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeAdd.Click
        On Error Resume Next
        Edit = False
     
        tempValue = "����"
        tempValue4 = "��良���"
        tempValue3 = ""
        'Dim myfrm As New frmChange
        'myfrm.ShowDialog()
        CheckForm(frmChange, frmChange.Name)

    End Sub
    ''' <summary>
    ''' �ק�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popChangeEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeEdit.Click
        On Error Resume Next
        Edit = True
        If GridView1.RowCount = 0 Then Exit Sub
        Dim ci1 As List(Of ChangeInfo)
        ci1 = cc.Change_GetList(GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing, Nothing, Nothing)
        If ci1(0).C_Check = True Then
            MsgBox("������w�f�֡A�����\�ק�")
            Exit Sub
        Else

            tempValue = "����"
            tempValue3 = GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString
            tempValue4 = GridView1.GetFocusedRowCellValue("C_Type").ToString
            Dim myfrm As New frmChange
            myfrm.MdiParent = MDIMain
            myfrm.WindowState = FormWindowState.Maximized
            myfrm.Show()

        End If

    End Sub
    ''' <summary>
    ''' �R��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popChangeDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim ci1 As List(Of ChangeInfo)
        ci1 = cc.Change_GetList(GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing, Nothing, Nothing)
        If ci1(0).C_Check = True Then
            MsgBox("������w�f�֡C�����\�R���I")
            Exit Sub
        Else
            If MsgBox("�T�w�R���渹��" & GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString & "�������O���H", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                If cc.Change_Del(ci1(0).C_ChangeNO, Nothing) = True Then
                    '****�R������
                    Dim dt As New FileController
                    Dim ldt As New List(Of FilesInfo)
                    Dim ii As Integer
                    ldt = dt.FileBond_GetList("4005", GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing)
                    If ldt.Count > 0 Then
                        For ii = 0 To ldt.Count - 1
                            dt.File_Delete("4005", GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, ldt(ii).F_No)
                        Next
                    End If
                    '******

                    MsgBox("�R�����\�I")
                    Grid1.DataSource = cc.Change_GetList(Nothing, Nothing, Nothing, False)
                Else
                    MsgBox("�R�����ѡA���ˬd��]�I")
                    Exit Sub
                End If
            End If

        End If
    End Sub
    ''' <summary>
    ''' �d��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popChangeView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeView.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub

       
        tempValue = "�d��"
        tempValue3 = GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString
        Dim myfrm As New frmChange
        myfrm.MdiParent = MDIMain
        myfrm.WindowState = FormWindowState.Maximized
        myfrm.Show()
        
    End Sub
    ''' <summary>
    ''' �f��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popChangeCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeCheck.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub
        Dim ci1 As List(Of ChangeInfo)

        Dim dt As New FileController
        Dim di As List(Of FilesInfo)

        di = dt.FileBond_GetList("4005", GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing)
        If di.Count = 0 Then
            MsgBox("������S������,�����\�f��,�и��J���ɡI")
            Exit Sub
        Else
            ci1 = cc.Change_GetList(GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing, Nothing, Nothing)
            If ci1(0).C_Check = True Then
                MsgBox("������w�f�֡A�����\�A�ާ@�I")
                Exit Sub
            Else
                tempValue = "�f��"
                tempValue3 = GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString
                tempValue4 = GridView1.GetFocusedRowCellValue("C_Type").ToString
                Dim myfrm As New frmChange
                myfrm.MdiParent = MDIMain
                myfrm.WindowState = FormWindowState.Maximized
                myfrm.Show()
            End If
        End If
      

    End Sub
    ''' <summary>
    ''' ��s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popChangeRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeRef.Click
        Grid1.DataSource = cc.Change_GetList(Nothing, Nothing, Nothing, False)
    End Sub
    ''' <summary>
    ''' ���[���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popChangeFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeFile.Click
        Dim open, update, down, edit, del, detail As Boolean
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

        If GridView1.RowCount = 0 Then Exit Sub
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400505")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then update = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then update = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400506")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then down = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then down = False
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400507")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then edit = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then edit = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400508")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then del = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then del = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400509")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then detail = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then detail = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400510")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then open = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then open = False
        End If

        FileShow("4005", GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, open, update, down, edit, del, detail)
    End Sub
    ''' <summary>
    ''' �d��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popChangeSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangeSeek.Click
        Dim myFrm As New frmChangeSelect
        myFrm.ShowDialog()
        Select Case tempValue
            Case "1"
                Grid1.DataSource = cc.Change_GetList(Nothing, tempValue2, Nothing, Nothing)
            Case "2"
                Grid1.DataSource = cc.Change_GetList(Nothing, Nothing, tempValue2, Nothing)
            Case "3"
                Grid1.DataSource = cc.Change_GetList(tempValue2, Nothing, Nothing, Nothing)
            Case "4"
                If tempValue2 = "�w�f��" Then
                    Grid1.DataSource = cc.Change_GetList(Nothing, Nothing, Nothing, True)
                ElseIf tempValue2 = "���f��" Then
                    Grid1.DataSource = cc.Change_GetList(Nothing, Nothing, Nothing, False)
                End If
        End Select
        tempValue = ""
        tempValue2 = ""
    End Sub
    '�o�e����---�d��
    Private Sub popMsgView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMsgView.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As frmMessageSent
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMessageSent Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue = "�s�W"                                                              '�Z�O��L�Ҷ��եΫH���������e���ѼƳ����s�W�A
        tempValue3 = "���ʳ�" & GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString & "�d��"      ' ����W
        tempValue4 = GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString               ' ���ID
        tempValue5 = "4005"                                                         '   �Ҷ�ID
        tempValue6 = "�d��"                                                         '�o�e����

        fr = New frmMessageSent
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    '�o�e����---�f��
    Private Sub popMsgCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMsgCheck.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub

        Dim ci1 As List(Of ChangeInfo)
        ci1 = cc.Change_GetList(GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing, Nothing, Nothing)
        If ci1(0).C_Check = True Then
            MsgBox("������w�f�֡A�����\�A�ާ@�I")
            Exit Sub
        Else
            Dim fr As frmMessageSent
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmMessageSent Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            tempValue = "�s�W"                                                              '�Z�O��L�Ҷ��եΫH���������e���ѼƳ����s�W�A
            tempValue3 = "���ʳ�" & GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString & "�����f��"     ' ����W
            tempValue4 = GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString               ' ���ID
            tempValue5 = "4005"                                                         '   �Ҷ�ID
            tempValue6 = "�����f��"                                                         '�o�e����


            fr = New frmMessageSent
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
    End Sub
    '�C�L�������
    Private Sub popChangePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangePrint.Click
        Dim ds As New DataSet
        If GridView1.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        'Dim ltc2 As New CollectionToDataSet
        'Dim ltc3 As New CollectionToDataSet
        'Dim ltc4 As New CollectionToDataSet

        Dim Mcc As New LFERP.Library.Material.MaterialController
        Dim Cmc As New LFERP.Library.Purchase.Change.ChangeControl
        Dim suc As New LFERP.SystemManager.SystemUser.SystemUserController
        Dim sc As New LFERP.DataSetting.SuppliersControler

        ds.Tables.Clear()

        ltc.CollToDataSet(ds, "MaterialCode", Mcc.MaterialCode_GetList(Nothing, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Change", Cmc.Change_GetList(GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, Nothing, Nothing, Nothing))
        'ltc2.CollToDataSet(ds, "SystemUser", suc.SystemUser_GetList(Nothing, Nothing, Nothing))
        'ltc4.CollToDataSet(ds, "Suppliers", sc.GetSuppliersList(Nothing, Nothing, Nothing, Nothing, Nothing))

        PreviewRPT(ds, "rptChangeMain", " ����--" & GridView1.GetFocusedRowCellValue("C_ChangeNO").ToString, True, True)

        ltc = Nothing
        ltc1 = Nothing
        'ltc4 = Nothing
        'ltc2 = Nothing
        'ltc3 = Nothing

    End Sub

    '�����ʳ椤���ʼƶq
    Private Sub popChangePurchaseAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popChangePurchaseAdd.Click
        On Error Resume Next
        Edit = False

        tempValue = "����"
        tempValue4 = "�����ʼ�"
        tempValue3 = ""
        'Dim myfrm As New frmChange
        'myfrm.MdiParent = MDIMain
        'myfrm.WindowState = FormWindowState.Maximized
        'myfrm.Show()
        CheckForm(frmChange, frmChange.Name)
    End Sub
End Class