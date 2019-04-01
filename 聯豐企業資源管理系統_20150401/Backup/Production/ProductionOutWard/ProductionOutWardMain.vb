Imports LFERP.Library.ProductionField
Imports LFERP.SystemManager
Imports LFERP.DataSetting
Imports LFERP.Library.ProductionController

Public Class ProductionOutWardMain

    Dim pfc As New Library.ProductionField.ProductionFieldControl
    Dim strDPT As String


    Private Sub ProductionOutWardMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Try
        twWare.ExpandAll()

        Dim fci As List(Of ProductionFieldControlInfo)
        Dim fc As New LFERP.Library.ProductionController.ProductionFieldControl
        fci = fc.ProductionFieldControl_GetList(InUserID, Nothing)

        If fci.Count = 0 Then Exit Sub

        strDPT = fci(0).ControlDep  '�ߤ@�~�o�� 

        'Dim a As New Library.ProductionField.ProductionFieldControl
        'Dim b As New List(Of ProductionFieldInfo)
        'Dim c As New List(Of ProductionFieldInfo)

        'Dim strDPT As String

        'Dim ui As List(Of UserPowerInfo)
        'Dim uc As New UserPowerControl
        'ui = uc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

        'If ui.Count = 0 Then
        '    Exit Sub
        'Else
        '    If ui(0).UserRank = "�޲z" Then
        '        strDPT = Nothing
        '    ElseIf ui(0).UserRank = "�Ͳ���" Then

        '        strDPT = Mid(ui(0).DepID, 1, 1)
        '    Else

        '        strDPT = ui(0).DepID
        '    End If

        'End If



        '    b = a.ProductionField_GetList(Nothing, Nothing, "�o��", Nothing, strDPT, Nothing, Nothing, False, Nothing, Nothing, "2")
        '    c = a.ProductionField_GetList(Nothing, Nothing, "����", Nothing, strDPT, Nothing, Nothing, False, Nothing, Nothing, "2")

        '    If b.Count > 0 Then
        '        twWare.Nodes.Item(1).Nodes.Item(0).Text = "���f�� (" & b.Count & ")"
        '    Else
        '        twWare.Nodes.Item(1).Nodes.Item(0).Text = "���f��"
        '    End If

        '    If c.Count > 0 Then
        '        twWare.Nodes.Item(0).Nodes.Item(0).Text = "���f�� (" & c.Count & ")"
        '    Else
        '        twWare.Nodes.Item(0).Nodes.Item(0).Text = "���f��"
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub twWare_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles twWare.AfterSelect
        'Dim strDPT As String

        'Dim ui As List(Of UserPowerInfo)
        'Dim uc As New UserPowerControl
        'ui = uc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

        'If ui.Count = 0 Then
        '    Exit Sub
        'Else
        '    If ui(0).UserRank = "�޲z" Then
        '        strDPT = Nothing
        '    ElseIf ui(0).UserRank = "�Ͳ���" Then

        '        strDPT = Mid(ui(0).DepID, 1, 1)
        '    Else

        '        strDPT = ui(0).DepID
        '    End If

        'End If

        If e.Node.Level = 2 Then
            Select Case Mid(twWare.SelectedNode.Text, 1, 3)

                Case "���f��"
                    If twWare.SelectedNode.Parent.Text = "���J����" Then
                        Pro_NO.Caption = "���Ƥu��"
                        Pro_NO1.Caption = "�o�Ƥu��"
                        Grid.DataSource = pfc.ProductionField_GetList(Nothing, Nothing, "����", Nothing, strDPT, Nothing, Nothing, False, Nothing, Nothing, "2", Nothing, Nothing)

                    End If
                    If twWare.SelectedNode.Parent.Text = "�o�X����" Then
                        Grid.DataSource = pfc.ProductionField_GetList(Nothing, Nothing, "�o��", Nothing, strDPT, Nothing, Nothing, False, Nothing, Nothing, "2", Nothing, Nothing)
                        Pro_NO1.Caption = "���Ƥu��"
                        Pro_NO.Caption = "�o�Ƥu��"
                    End If

                Case "�w�f��"
                    If twWare.SelectedNode.Parent.Text = "���J����" Then
                        Pro_NO.Caption = "���Ƥu��"
                        Pro_NO1.Caption = "�o�Ƥu��"
                        Grid.DataSource = pfc.ProductionField_GetList(Nothing, Nothing, "����", Nothing, strDPT, Nothing, Nothing, True, Nothing, Nothing, "2", Nothing, Nothing)

                    End If
                    If twWare.SelectedNode.Parent.Text = "�o�X����" Then
                        Grid.DataSource = pfc.ProductionField_GetList(Nothing, Nothing, "�o��", Nothing, strDPT, Nothing, Nothing, True, Nothing, Nothing, "2", Nothing, Nothing)
                        Pro_NO1.Caption = "���Ƥu��"
                        Pro_NO.Caption = "�o�Ƥu��"
                    End If

            End Select
        End If
    End Sub

    Private Sub cmsInAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsInAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As frmProductionFieldCodeIn
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionFieldCodeIn Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionFieldCodeIn
        tempValue = "CodeIn"
        tempValue2 = "PT14"
        tempValue5 = strDPT
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmsOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsOutAdd.Click
        On Error Resume Next

        Edit = False

        Dim fr As frmProductionFieldCodeOut
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionFieldCodeOut Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionFieldCodeOut
        tempValue = "CodeHouse"
        tempValue2 = "PT14"
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmsEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsEdit.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub

        Dim pfi1 As List(Of ProductionFieldInfo)
        Dim pfi As List(Of ProductionFieldInfo)

        Dim strType As String
        strType = GridView1.GetFocusedRowCellValue("FP_Type")

        pfi1 = pfc.ProductionField_GetList(GridView1.GetFocusedRowCellValue("FP_NO").ToString, Nothing, "����", Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, "1", Nothing, Nothing)
        If pfi1.Count <> 0 Then
            MsgBox("���ާ@�w�Q�T�{,�����\���!")
            Exit Sub
        End If

        pfi = pfc.ProductionField_GetList(GridView1.GetFocusedRowCellValue("FP_NO").ToString, Nothing, strType, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "1", Nothing, Nothing)
        If pfi.Count = 0 Then Exit Sub

        If pfi(0).FP_Detail = "PT14" And strType = "����" And pfi(0).IW_NO <> "" Then '�}�ƹ���PT03,�[�s����PT04

            Edit = True
            Dim fr As frmProductionFieldCodeIn
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionFieldCodeIn Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            fr = New frmProductionFieldCodeIn
            tempValue = "CodeIn"
            tempValue4 = GridView1.GetFocusedRowCellValue("FP_NO").ToString
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()

        ElseIf pfi(0).FP_Detail = "PT14" And strType = "�o��" Then '�j�f����PT01,��׹���PT02

            If GridView1.GetFocusedRowCellValue("FP_Type").ToString <> "�o��" Then
                MsgBox("�z�����'�o��'�ʽ誺�O���i��ק�R���ާ@�I")
                Exit Sub
            Else

                Dim pfi2 As List(Of ProductionFieldInfo)
                Dim pfc2 As New Library.ProductionField.ProductionFieldControl

                pfi2 = pfc2.ProductionField_GetList(GridView1.GetFocusedRowCellValue("FP_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, "1", Nothing, Nothing)
                If pfi2.Count = 0 Then Exit Sub

                If pfi2.Count = 1 Then
                    Edit = True
                    Dim fr As frmProductionFieldCodeOut
                    For Each fr In MDIMain.MdiChildren
                        If TypeOf fr Is frmProductionFieldCodeOut Then
                            fr.Activate()
                            Exit Sub
                        End If
                    Next
                    fr = New frmProductionFieldCodeOut
                    tempValue = "CodeHouse"
                    tempValue4 = GridView1.GetFocusedRowCellValue("FP_NO").ToString
                    fr.MdiParent = MDIMain
                    fr.WindowState = FormWindowState.Maximized
                    fr.Show()
                Else

                    Edit = True
                    Dim fr As frmProductionFieldCode
                    For Each fr In MDIMain.MdiChildren
                        If TypeOf fr Is frmProductionFieldCode Then
                            fr.Activate()
                            Exit Sub
                        End If
                    Next
                    fr = New frmProductionFieldCode
                    tempValue = "CodeOut"
                    tempValue4 = GridView1.GetFocusedRowCellValue("FP_NO").ToString
                    fr.MdiParent = MDIMain
                    fr.WindowState = FormWindowState.Maximized
                    fr.Show()
                End If
            End If
         
        End If
    End Sub

    Private Sub cmsDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsDel.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim pfi1 As List(Of ProductionFieldInfo)
        pfi1 = pfc.ProductionField_GetList(GridView1.GetFocusedRowCellValue("FP_NO").ToString, Nothing, "����", Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, "1", Nothing, Nothing)
        If pfi1.Count <> 0 Then
            MsgBox("���ާ@�w�Q�T�{,�����\�R��!")
            Exit Sub
        End If

        Dim pfi As List(Of ProductionFieldInfo)

        Dim strType As String
        strType = GridView1.GetFocusedRowCellValue("FP_Type").ToString

        pfi = pfc.ProductionField_GetList(GridView1.GetFocusedRowCellValue("FP_NO").ToString, Nothing, strType, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "1", Nothing, Nothing)
        If pfi.Count = 0 Then Exit Sub

        If pfi(0).FP_Detail = "PT14" And strType = "����" Then

            If MsgBox("�A�T�w�R�����Ʀ��o�渹��  '" & GridView1.GetFocusedRowCellValue("FP_NO").ToString & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If pfc.ProductionField_Delete(GridView1.GetFocusedRowCellValue("FP_NO").ToString, Nothing) = True Then
                    MsgBox("�R�����\!")
                    Grid.DataSource = pfc.ProductionField_GetList(Nothing, Nothing, "����", Nothing, pfi(0).FP_OutDep, Nothing, False, False, Nothing, Nothing, "2", Nothing, Nothing)
                Else
                    MsgBox("�R������,���ˬd��]!")
                End If
            End If

        ElseIf pfi(0).FP_Detail = "PT14" And strType = "�o��" Then
          

            If MsgBox("�A�T�w�R�����Ʀ��o�渹��  '" & GridView1.GetFocusedRowCellValue("FP_NO").ToString & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If pfc.ProductionField_Delete(GridView1.GetFocusedRowCellValue("FP_NO").ToString, Nothing) = True Then
                    MsgBox("�R�����\!")
                    Grid.DataSource = pfc.ProductionField_GetList(Nothing, Nothing, "�o��", Nothing, pfi(0).FP_OutDep, Nothing, False, False, Nothing, Nothing, "2", Nothing, Nothing)
                Else
                    MsgBox("�R������,���ˬd��]!")
                End If
            End If
     
        End If
    End Sub

    Private Sub cmsPreView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPreView.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub

        Dim pfi1 As List(Of ProductionFieldInfo)
        Dim pfi As List(Of ProductionFieldInfo)

        Dim strType As String
        strType = GridView1.GetFocusedRowCellValue("FP_Type")

        pfi = pfc.ProductionField_GetList(GridView1.GetFocusedRowCellValue("FP_NO").ToString, Nothing, strType, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "1", Nothing, Nothing)
        If pfi.Count = 0 Then Exit Sub

        If pfi(0).FP_Detail = "PT14" And strType = "����" And pfi(0).IW_NO <> "" Then '�}�ƹ���PT03,�[�s����PT04

            Dim fr As frmProductionFieldCodeIn
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionFieldCodeIn Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            fr = New frmProductionFieldCodeIn
            tempValue = "PreView"
            tempValue4 = GridView1.GetFocusedRowCellValue("FP_NO").ToString
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()

        ElseIf pfi(0).FP_Detail = "PT14" And strType = "�o��" Then '�j�f����PT01,��׹���PT02

            If GridView1.GetFocusedRowCellValue("FP_Type").ToString <> "�o��" Then
                MsgBox("�z�����'�o��'�ʽ誺�O���i��ק�R���ާ@�I")
                Exit Sub
            Else

                Dim pfi2 As List(Of ProductionFieldInfo)
                Dim pfc2 As New Library.ProductionField.ProductionFieldControl

                pfi2 = pfc2.ProductionField_GetList(GridView1.GetFocusedRowCellValue("FP_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, "1", Nothing, Nothing)
                If pfi2.Count = 0 Then Exit Sub

                If pfi2.Count = 1 Then

                    Dim fr As frmProductionFieldCodeOut
                    For Each fr In MDIMain.MdiChildren
                        If TypeOf fr Is frmProductionFieldCodeOut Then
                            fr.Activate()
                            Exit Sub
                        End If
                    Next
                    fr = New frmProductionFieldCodeOut
                    tempValue = "PreView"
                    tempValue4 = GridView1.GetFocusedRowCellValue("FP_NO").ToString
                    fr.MdiParent = MDIMain
                    fr.WindowState = FormWindowState.Maximized
                    fr.Show()
                Else

                    Dim fr As frmProductionFieldCode
                    For Each fr In MDIMain.MdiChildren
                        If TypeOf fr Is frmProductionFieldCode Then
                            fr.Activate()
                            Exit Sub
                        End If
                    Next
                    fr = New frmProductionFieldCode
                    tempValue = "PreView"
                    tempValue4 = GridView1.GetFocusedRowCellValue("FP_NO").ToString
                    fr.MdiParent = MDIMain
                    fr.WindowState = FormWindowState.Maximized
                    fr.Show()
                End If
            End If

        End If
    End Sub

    Private Sub cmsQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsQuery.Click
        Dim frm As New frmProductionFieldSelect
        frm.ShowDialog()

        Dim upi As List(Of UserPowerInfo)
        Dim upc As New UserPowerControl
        upi = upc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

        'Dim strDep As String

        'If upi.Count = 0 Then
        '    Exit Sub
        'Else
        '    If upi(0).UserRank = "�޲z" Then
        '        strDep = Nothing
        '    ElseIf upi(0).UserRank = "�Ͳ���" Then
        '        strDep = Mid(upi(0).DepID, 1, 1)
        '    Else
        '        strDep = upi(0).DepID
        '    End If

        'End If
        Grid.DataSource = pfc.ProductionField_GetList1(Nothing, tempValue2, tempValue3, tempValue4, Nothing, Nothing, Nothing, strDPT, Nothing, Nothing, Nothing, tempValue5, tempValue6, Nothing, Nothing)

        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing
    End Sub

    Private Sub cmsRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsRef.Click

        If GridView1.RowCount = 0 Then Exit Sub

        Dim strType As String
        strType = GridView1.GetFocusedRowCellValue("FP_Type").ToString

        'Dim upi As List(Of UserPowerInfo)
        'Dim upc As New UserPowerControl
        'upi = upc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

        'Dim strDep As String

        'If upi.Count = 0 Then
        '    Exit Sub
        'Else
        '    If upi(0).UserRank = "�޲z" Then
        '        strDep = Nothing
        '    ElseIf upi(0).UserRank = "�Ͳ���" Then
        '        strDep = Mid(upi(0).DepID, 1, 1)
        '    Else
        '        strDep = upi(0).DepID
        '    End If

        'End If
        Grid.DataSource = pfc.ProductionField_GetList(Nothing, Nothing, strType, Nothing, strDPT, Nothing, False, False, Nothing, Nothing, "2", Nothing, Nothing)
    End Sub

    Private Sub cmsInCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsInCheck.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub

        Dim pfi1 As List(Of ProductionFieldInfo)
        Dim pfi As List(Of ProductionFieldInfo)

        Dim strType As String
        strType = GridView1.GetFocusedRowCellValue("FP_Type")

        pfi = pfc.ProductionField_GetList(GridView1.GetFocusedRowCellValue("FP_NO").ToString, Nothing, strType, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "1", Nothing, Nothing)
        If pfi.Count = 0 Then Exit Sub

        If pfi(0).FP_Detail = "PT14" And strType = "����" And pfi(0).IW_NO <> "" Then '�}�ƹ���PT03,�[�s����PT04

            Dim fr As frmProductionFieldCodeIn
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionFieldCodeIn Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            fr = New frmProductionFieldCodeIn
            tempValue = "InCheck"
            tempValue4 = GridView1.GetFocusedRowCellValue("FP_NO").ToString
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()

        ElseIf pfi(0).FP_Detail = "PT14" And strType = "����" And pfi(0).IW_NO = "" Then

            Dim fr As frmProductionFieldCode
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionFieldCode Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            fr = New frmProductionFieldCode
            tempValue = "InCheck"
            tempValue4 = GridView1.GetFocusedRowCellValue("FP_NO").ToString
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()

        ElseIf pfi(0).FP_Detail = "PT14" And strType = "�o��" Then '�j�f����PT01,��׹���PT02

            If GridView1.GetFocusedRowCellValue("FP_Type").ToString <> "�o��" Then
                MsgBox("�z�����'�o��'�ʽ誺�O���i��ק�R���ާ@�I")
                Exit Sub
            Else

                Dim pfi2 As List(Of ProductionFieldInfo)
                Dim pfc2 As New Library.ProductionField.ProductionFieldControl

                pfi2 = pfc2.ProductionField_GetList(GridView1.GetFocusedRowCellValue("FP_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "1", Nothing, Nothing)
                If pfi2.Count = 0 Then Exit Sub

                If pfi2.Count = 1 Then

                    Dim fr As frmProductionFieldCodeOut
                    For Each fr In MDIMain.MdiChildren
                        If TypeOf fr Is frmProductionFieldCodeOut Then
                            fr.Activate()
                            Exit Sub
                        End If
                    Next
                    fr = New frmProductionFieldCodeOut
                    tempValue = "InCheck"
                    tempValue4 = GridView1.GetFocusedRowCellValue("FP_NO").ToString
                    fr.MdiParent = MDIMain
                    fr.WindowState = FormWindowState.Maximized
                    fr.Show()
                Else

                    Dim fr As frmProductionFieldCode
                    For Each fr In MDIMain.MdiChildren
                        If TypeOf fr Is frmProductionFieldCode Then
                            fr.Activate()
                            Exit Sub
                        End If
                    Next
                    fr = New frmProductionFieldCode
                    tempValue = "InCheck"
                    tempValue4 = GridView1.GetFocusedRowCellValue("FP_NO").ToString
                    fr.MdiParent = MDIMain
                    fr.WindowState = FormWindowState.Maximized
                    fr.Show()
                End If
            End If

        End If
    End Sub

    '�C�L�~�o��
    Private Sub cmsPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPrint.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim ds As New DataSet

        Dim ltc As New CollectionToDataSet
        Dim pfc As New Library.ProductionField.ProductionFieldControl  '���Ʀ��o�H��
        ds.Tables.Clear()

        Dim pfi As List(Of ProductionFieldInfo)

        Dim strType As String
        strType = GridView1.GetFocusedRowCellValue("FP_Type")

        pfi = pfc.ProductionField_GetList(GridView1.GetFocusedRowCellValue("FP_NO").ToString, Nothing, strType, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "1", Nothing, Nothing)

        If pfi(0).FP_Detail = "PT14" And strType = "�o��" Then
            Dim strA, strB, strC As String

            strA = GridView1.GetFocusedRowCellValue("FP_NO").ToString
            strB = "G101"
            strC = "F101"
            If GridView1.GetFocusedRowCellValue("FP_InDep").ToString <> strB And GridView1.GetFocusedRowCellValue("FP_OutDep").ToString <> strC Then
                Exit Sub
            End If
            ltc.CollToDataSet(ds, "ProductionField", pfc.ProductionField_GetList1(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, Nothing, Nothing, Nothing, Nothing))
        End If

        PreviewRPT(ds, "rptProductionOutWard", "�~�o��", True, True)

        ltc = Nothing

    End Sub

    Private Sub cmsSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsSend.Click
        On Error Resume Next

        Edit = False

        Dim fr As frmProductionFieldCode
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionFieldCode Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionFieldCode
        tempValue = "CodeOut"
        tempValue2 = "PT14"
        tempValue5 = strDPT
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    '�C�L��e�~�o���`�H���]�~�o���p�ץH�ΰe�^�H���A�����~�Ϥ��^
    Private Sub cmsPrintAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPrintAll.Click
        Dim frm As New frmProductionOutWardQuery
        frm.ShowDialog()

    End Sub

End Class