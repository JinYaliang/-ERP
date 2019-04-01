Imports LFERP.Library.Purchase.Acceptance
Imports LFERP.Library.Purchase.Purchase
Imports LFERP.Library.Purchase.Retrocede
Imports LFERP.DataSetting
Imports LFERP.SystemManager
Imports LFERP.Library.Purchase.purselect
Public Class frmAcceptanceMain

    Private Sub popAcceptanceAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popAcceptanceAdd.Click

        On Error Resume Next
        Edit = False
        MTypeName = "AcceptanceAddEdit"
        'Dim fr As frmAcceptance
        'For Each fr In MDIMain.MdiChildren
        '    If TypeOf fr Is frmAcceptance Then
        '        fr.Activate()
        '        Exit Sub
        '    End If
        'Next
        'fr = New frmAcceptance
        'fr.MdiParent = MDIMain
        'fr.WindowState = FormWindowState.Maximized
        'fr.Show()
        CheckForm(frmAcceptance, frmAcceptance.Name)
        'Dim myfrm As New frmAcceptance
        'myfrm.ShowDialog()
    End Sub

    Private Sub frmAcceptanceMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mc As New AcceptanceController
       
        Grid1.DataSource = mc.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, "�w����", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)


        LoadUserPower()
    End Sub
    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400301")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popAcceptanceAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400302")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popAcceptanceEdit.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400303")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popAcceptanceDel.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400305")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popAcceptanceCheck.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400306")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popAcceptanceAccCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400314")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popAcceptancePayCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400315")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popAcceptanceDetail.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400316")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popBatchCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400317")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popAcceptanceGether.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400318")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popBatchPayCheck.Enabled = True
        End If

        '--------------------------------------------���ƽռ��v��(���Y���o���v���P��ۦP)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500301")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareMove.Enabled = True
        End If
    End Sub
    Private Sub popAcceptanceRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popAcceptanceRef.Click
        Dim mc As New AcceptanceController
       
        Grid1.DataSource = mc.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, "�w����", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub popAcceptanceDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popAcceptanceDel.Click
        Dim osc As New AcceptanceController
        Dim osilist As New List(Of AcceptanceInfo)
        osilist = osc.Acceptance_GetList(Nothing, GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If osilist.Count <> 0 Then

            MsgBox("�L�k�R��,���禬��w�禬�I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If
        Dim rc As New RetrocedeController
        Dim riList As New List(Of RetrocedeInfo)
        riList = rc.Retrocede_GetList(Nothing, GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If riList.Count <> 0 Then
            MsgBox("�L�k�R��,���禬��w���h�f�I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        osilist = osc.Acceptance_GetList(Nothing, GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing)
        If osilist.Count <> 0 Then

            MsgBox("�L�k�R��,���禬��w�I�ڡI", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If


        Dim StrA As String
        StrA = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        If MsgBox("�A�T�w�R���禬�渹��  '" & StrA & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            Dim mc As New AcceptanceInfo
            Dim mt As New AcceptanceController
            mc.A_AcceptanceNO = StrA
            If mt.Acceptance_NO_Delete(mc) = True Then

                Grid1.DataSource = mt.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Else
                MsgBox("�R������")
            End If

        End If
    End Sub

    Private Sub popAcceptanceEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popAcceptanceEdit.Click
        On Error Resume Next
        Dim osc As New AcceptanceController
        Dim osilist As New List(Of AcceptanceInfo)
        osilist = osc.Acceptance_GetList(Nothing, GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Dim rc As New RetrocedeController
        Dim riList As New List(Of RetrocedeInfo)
        riList = rc.Retrocede_GetList(Nothing, GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)


        If riList.Count <> 0 Then
            MsgBox("�L�k�R��,���禬��w���h�f�I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If


        If osilist.Count <> 0 Then

            MsgBox("�L�k�ק�,���禬��w�禬�I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        osilist = osc.Acceptance_GetList(Nothing, GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If osilist.Count <> 0 Then

            MsgBox("�L�k�ק�,���禬��|�p���w�f�֡I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        osilist = osc.Acceptance_GetList(Nothing, GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing)
        If osilist.Count <> 0 Then

            MsgBox("�L�k�ק�,���禬��w�I�ڡI", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        tempValue2 = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        MTypeName = "AcceptanceAddEdit"
        Edit = True

        Dim fr As frmAcceptance
        'For Each fr In MDIMain.MdiChildren
        '    If TypeOf fr Is frmAcceptance Then
        '        fr.Activate()
        '        Exit Sub
        '    End If
        'Next
        fr = New frmAcceptance
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
        'CheckForm(frmAcceptance, frmAcceptance.Name)
    End Sub

    Private Sub popAcceptanceCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popAcceptanceCheck.Click
        On Error Resume Next
        Dim osc As New AcceptanceController
        Dim osilist As New List(Of AcceptanceInfo)


        osilist = osc.Acceptance_GetList(Nothing, GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If osilist.Count <> 0 Then

            MsgBox("�L�k�ק�,���禬��|�p���w�f�֡I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        osilist = osc.Acceptance_GetList(Nothing, GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing)
        If osilist.Count <> 0 Then

            MsgBox("�L�k�ק�,���禬��w�I�ڡI", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If




        tempValue2 = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        MTypeName = "AcceptanceCheck"


        Dim fr As frmAcceptance
        'For Each fr In MDIMain.MdiChildren
        '    If TypeOf fr Is frmAcceptance Then
        '        fr.Activate()
        '        Exit Sub
        '    End If
        'Next
        fr = New frmAcceptance
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
        'Dim myfrm As New frmAcceptance
        'myfrm.ShowDialog()
        'CheckForm(frmAcceptance, frmAcceptance.Name)
    End Sub

    Private Sub popAcceptanceAccCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popAcceptanceAccCheck.Click
        On Error Resume Next
        Dim osc As New AcceptanceController
        Dim osilist As New List(Of AcceptanceInfo)



        Dim ac As New AcceptanceController
        Dim ai As New AcceptanceInfo

        tempValue2 = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        ai = ac.Acceptance_Get(tempValue2)
        If ai.A_Check = False Then
            MsgBox("�������禬,�|�p���~��f��!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        osilist = osc.Acceptance_GetList(Nothing, GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing)
        If osilist.Count <> 0 Then

            MsgBox("�L�k�ק�,���禬��w�I�ڡI", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        MTypeName = "AcceptanceAccountCheck"

        Dim fr As frmAcceptance
        'For Each fr In MDIMain.MdiChildren
        '    If TypeOf fr Is frmAcceptance Then
        '        fr.Activate()
        '        Exit Sub
        '    End If
        'Next
        fr = New frmAcceptance
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popAcceptanceSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popAcceptanceSeek.Click

        tempValue = "�禬�޲z"
        Dim fr As New FrmpurSelect
        FrmpurSelect.ShowDialog()
        Dim mc As New AcceptanceController
        Dim pc As New PurSelectControl
        Select Case tempValue
            Case "1" '���ʳ渹
                Grid1.DataSource = mc.Acceptance_GetList(tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            Case "2" '�禬�渹
                Grid1.DataSource = mc.Acceptance_GetList(Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            Case "3" '���ƦW��
                Grid1.DataSource = mc.Acceptance_GetList(Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            Case "4" '���ƽs�X,
                Grid1.DataSource = mc.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing)

                'Case "5"  '�������O
            Case "5" '������
                Grid1.DataSource = mc.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing)

            Case "6"

                Grid1.DataSource = pc.Acceptance_GetList("�禬�޲z", tempValue2)
        End Select
        tempValue = ""
        tempValue2 = ""

        'Dim mc As New AcceptanceController
        'Dim myfrm As New frmA_AcceptanceSelect2
        'myfrm.ShowDialog()
        'Select Case tempValue
        '    Case "1"
        '        Grid1.DataSource = mc.Acceptance_GetList(Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        '    Case "2"
        '        Grid1.DataSource = mc.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing)
        '    Case "3"
        '        Grid1.DataSource = mc.Acceptance_GetList(tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '    Case "4"
        '        Grid1.DataSource = mc.Acceptance_GetList(Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '    Case "5"
        '        Grid1.DataSource = mc.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing)
        '    Case "6"
        '        Grid1.DataSource = mc.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing)

        'End Select

        'tempValue = ""
        'tempValue2 = ""

        'tempValue = "�禬�޲z"

        'Dim fr As New FrmpurSelect
        'FrmpurSelect.ShowDialog()
        ''Dim fr As New FrmpurSelect
        'Dim pc As New PurSelectControl

        'Grid1.DataSource = pc.Acceptance_GetList("�禬�޲z", tempValue2)
        'tempValue2 = ""

    End Sub

    Private Sub popAcceptanceFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popAcceptanceFile.Click
        '�եΦ��q��y���������
        '  If Grid.Rows.Count = 0 Then Exit Sub

        Dim open, update, down, edit, del, detail As Boolean
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

        '  If Grid.Rows.Count = 0 Then Exit Sub
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400308")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then update = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then update = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400309")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then down = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then down = False
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400310")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then edit = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then edit = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400311")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then del = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then del = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400312")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then detail = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then detail = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400313")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then open = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then open = False
        End If

        FileShow("4001", GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString, open, update, down, edit, del, detail)
    End Sub

    Private Sub popAcceptancePayCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popAcceptancePayCheck.Click
        On Error Resume Next
        tempValue2 = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        Dim ac As New AcceptanceController
        Dim ai As New AcceptanceInfo
        ai = ac.Acceptance_Get(tempValue2)
        If ai.A_AccountCheck = False Then
            MsgBox("��ڥ����|�p���f�֫�~�వ�I�ڽT�{!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        MTypeName = "AcceptancePayCheck"
        '     Edit = True

        Dim fr As frmAcceptance
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmAcceptance Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmAcceptance
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popAcceptanceView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popAcceptanceView.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmAcceptance
        'For Each fr In MDIMain.MdiChildren
        '    If TypeOf fr Is frmAcceptance Then
        '        fr.Activate()
        '        Exit Sub
        '    End If
        'Next


        tempValue2 = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        MTypeName = "AcceptanceView"



        fr = New frmAcceptance
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popAcceptancePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popAcceptancePrint.Click
        '   On Error Resume Next
        Dim ds As New DataSet
        If GridView1.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim ltc5 As New CollectionToDataSet

        Dim pmc As New PurchaseMainController
        Dim ac As New AcceptanceController
        Dim uc As New UnitController
        Dim suc As New SystemUser.SystemUserController
        Dim cc As New CompanyControler
        '    Dim omc As New OrdersMainController
        ds.Tables.Clear()
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("PM_NO").ToString

        ltc.CollToDataSet(ds, "PurchaseMain", pmc.PurchaseMain_Getlist(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Acceptance", ac.Acceptance_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(ds, "Unit", uc.GetUnitList(Nothing))

        ltc3.CollToDataSet(ds, "SystemUser", suc.SystemUser_GetList(Nothing, Nothing, Nothing))

        ltc4.CollToDataSet(ds, "Company", cc.Company_Getlist(Nothing, Nothing, Nothing, Nothing))
        ' ltc5.CollToDataSet(ds, "OrdersMain", omc.OrdersMain_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Grid.CurrentRow.Cells("OM_ID").Value.ToString))



        PreviewRPT(ds, "rptAcceptance", "�e�f�ԲӪ�", True, False)
        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing
        ltc4 = Nothing
        '     ltc5 = Nothing
    End Sub

    Private Sub popDetail1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popDetail1.Click

        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        Dim ac2 As New AcceptanceController
        Dim aciList As New List(Of AcceptanceInfo)
        Dim aci2 As New AcceptanceInfo
        aciList = ac2.Acceptance_GetList(Nothing, strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '  Label1.Text = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        If aciList.Item(0).A_PayCheck = False Then
            MsgBox("�����O'�I�ڽT�{'����ڤ~��վ㬰'�w����'", MsgBoxStyle.OkOnly)
            Exit Sub
        End If
        If MsgBox("�T�w�n�N���A�վ㬰 '�w����' ��?", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then
            Dim ac As New AcceptanceController
            Dim ai As New AcceptanceInfo
            ai.A_AcceptanceNO = strA
            ai.A_Detail = "�w����"
            ac.Acceptance_UpdateDetail(ai)
            MsgBox("���A�w���", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub popDetail2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popDetail2.Click
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        If MsgBox("�T�w�n�N���A�վ㬰 '�h�f���B�z' ��?", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then
            Dim ac As New AcceptanceController
            Dim ai As New AcceptanceInfo
            ai.A_AcceptanceNO = strA
            ai.A_Detail = "�h�f���B�z"
            ac.Acceptance_UpdateDetail(ai)
            MsgBox("���A�w���", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub Grid1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Grid1.MouseUp
        If GridView1.RowCount = 0 Then Exit Sub

        Dim strA, strB As String
        strA = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString()
        strB = GridView1.GetFocusedRowCellValue("M_Code").ToString()
        Dim rc As New RetrocedeController
        GridControl2.DataSource = rc.Retrocede_GetList(Nothing, strA, Nothing, Nothing, Nothing, Nothing, True, Nothing, strB, Nothing, Nothing, Nothing)
    End Sub

    '�����q�禬�椤�K�[�ռ���
    Private Sub popWareMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMove.Click

        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim ac As New AcceptanceController
        Dim ai As New AcceptanceInfo
        ai = ac.Acceptance_Get(GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString)
        If ai.A_Check = False Then
            MsgBox("�禬�楲����fQC�f�֫�~��o�ռ���!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If
        Dim fr As frmWareMoveOut
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmWareMoveOut Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue2 = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        MTypeName = "AcceptanceMoveOut"

        fr = New frmWareMoveOut
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    '@ 2012/6/14 �K�[ ��q�f�֥\��
    Private Sub popBatchCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popBatchCheck.Click
        Dim fr As New frmAcceptanceBatchCheck
        Dim ac As New AcceptanceController
        fr.lblTittle.Text = "�禬���q�f��"
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    '@ 2012/6/14 �K�[ �禬�O�����`�\��
    Private Sub popAcceptanceGether_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popAcceptanceGether.Click
        Dim fr As New frmAcceptanceGather
        fr.ShowDialog()
    End Sub
    '@ 2012/6/18 �K�[ ��q�I�ڽT�{
    Private Sub popBatchPayCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popBatchPayCheck.Click
        Dim fr As New frmAcceptanceBatchCheck
        Dim ac As New AcceptanceController
        fr.lblTittle.Text = "�禬���q�I�ڽT�{"
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
End Class