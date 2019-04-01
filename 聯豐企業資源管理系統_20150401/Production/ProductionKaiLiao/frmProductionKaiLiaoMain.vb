Imports LFERP.Library.ProductionKaiLiao
Imports LFERP.SystemManager
Imports LFERP.Library.WareHouse.WareOut
Imports LFERP.DataSetting
Public Class frmProductionKaiLiaoMain
    Dim pkc As New ProductionKaiLiaoControl
    Dim strDPT As String

    Private Sub frmProductionKaiLiaoMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ui As List(Of UserPowerInfo)
        Dim uc As New UserPowerControl
        ui = uc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

        If ui.Count = 0 Then
            Exit Sub
        Else
            If ui(0).UserRank = "�޲z" Then
                strDPT = Nothing
            Else
                strDPT = Mid(ui(0).DepID, 1, 1)
            End If
        End If
        ' Grid.DataSource = pkc.ProductionKaiLiao_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strDPT)
        Grid.DataSource = pkc.ProductionKaiLiaoA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strDPT, Nothing, Nothing, Nothing, Nothing)
        PowerUser()
    End Sub
    '�]�m�v��
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880201")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsProductionAdd.Enabled = True
                cmsProductionAddA.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880202")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsProductionEdit.Enabled = True
                cmsProductionEditA.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880203")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsProductionDel.Enabled = True
                cmsProductionDelA.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880204")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsProductionCheck.Enabled = True
                cmsProductionCheckA.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880205")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsProductionPrint.Enabled = True
                cmsProductionPrintA.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880207")
        Me.Grid.ContextMenuStrip = Me.cmsProductionKaiLiao
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                Me.Grid.ContextMenuStrip = Me.cmsProductionKaiLiaoA
                Me.C_Number.Visible = True
            Else
                Me.Grid.ContextMenuStrip = Me.cmsProductionKaiLiao
                Me.C_Number.Visible = False
            End If
        End If
    End Sub

    '�s�W�}�Ƴ�
    Private Sub cmsProductionAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionAdd.Click
        On Error Resume Next
        Edit = False

        Dim fr As frmProductionKaiLiao
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionKaiLiao Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "�}�Ƴ�"
        fr = New frmProductionKaiLiao
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    '�ק�}�Ƴ�
    Private Sub cmsProductionEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionEdit.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As List(Of ProductionKaiLiaoInfo)
        pi = pkc.ProductionKaiLiao_GetList(GridView1.GetFocusedRowCellValue("C_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pi(0).C_Check = True Then
            MsgBox("���}�Ƴ�w�Q�f��,�����\�ק�")
            Exit Sub
        Else
            Edit = True
            Dim fr As frmProductionKaiLiao
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionKaiLiao Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue2 = "�}�Ƴ�"
            tempValue3 = GridView1.GetFocusedRowCellValue("C_NO").ToString
            fr = New frmProductionKaiLiao
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
    End Sub

    '�R���}�Ƴ�
    Private Sub cmsProductionDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As List(Of ProductionKaiLiaoInfo)
        pi = pkc.ProductionKaiLiao_GetList(GridView1.GetFocusedRowCellValue("C_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pi(0).C_Check = True Then
            MsgBox("���}�Ƴ�w�Q�f��,�����\�R��")
            Exit Sub
        Else
            If MsgBox("�T�w�n�R���s����" & GridView1.GetFocusedRowCellValue("C_NO").ToString & "���}�Ƴ�ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then
                If pkc.ProductionKaiLiao_Delete(GridView1.GetFocusedRowCellValue("C_NO").ToString, Nothing) = True Then
                    MsgBox("�R����e�}�Ƴ榨�\!")
                Else
                    MsgBox("�R������,���ˬd��]!")
                End If
                Grid.DataSource = pkc.ProductionKaiLiao_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strDPT)
            End If
        End If
    End Sub

    '�d��
    Private Sub cmsProductionSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionSelect.Click
        Dim frm As New frmProductionKaiLiaoSelect
        frm.ShowDialog()

        If tempValue = "�����d�߫��s" Then
            Grid.DataSource = pkc.ProductionKaiLiao_GetList(tempValue2, tempValue3, tempValue6, Nothing, tempValue7, tempValue4, tempValue5, tempValue8, tempValue9, Nothing, strDPT)
        End If
        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing
        tempValue7 = Nothing
        tempValue8 = Nothing
        tempValue9 = Nothing

    End Sub

    '�d�ݷ�e�}�Ƴ�
    Private Sub cmsProductionView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmProductionKaiLiao
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionKaiLiao Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "PreView"
        tempValue3 = GridView1.GetFocusedRowCellValue("C_NO").ToString
        fr = New frmProductionKaiLiao
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    '��s�ާ@
    Private Sub cmsProductionRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionRef.Click
        Grid.DataSource = pkc.ProductionKaiLiaoA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strDPT, Nothing, Nothing, Nothing, Nothing)
    End Sub

    '�f�ַ�e�}�Ƴ�
    Private Sub cmsProductionCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionCheck.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim pi As List(Of ProductionKaiLiaoInfo)
        pi = pkc.ProductionKaiLiao_GetList(GridView1.GetFocusedRowCellValue("C_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pi.Count = 0 Then Exit Sub

        If pi(0).C_Check = True Then

            Dim wic As New WareOutController
            Dim wi As List(Of WareOutInfo)

            wi = wic.WareOut_ProductionGetList(Nothing, GridView1.GetFocusedRowCellValue("C_NO").ToString)

            If wi.Count = 0 Then
                Dim fr As frmProductionKaiLiao
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmProductionKaiLiao Then
                        fr.Activate()
                        Exit Sub
                    End If
                Next
                tempValue2 = "Check"
                tempValue3 = GridView1.GetFocusedRowCellValue("C_NO").ToString
                fr = New frmProductionKaiLiao
                fr.MdiParent = MDIMain
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            Else
                MsgBox("�b�ܮw�w�s�b����Ƴ渹�O��,�����\�����I")
                Exit Sub
            End If

        Else

            Dim fr As frmProductionKaiLiao
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionKaiLiao Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue2 = "Check"
            tempValue3 = GridView1.GetFocusedRowCellValue("C_NO").ToString
            fr = New frmProductionKaiLiao
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If

    End Sub

    '�C�L��e�}�Ƴ�
    Private Sub cmsProductionPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionPrint.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim ds As New DataSet
        Dim ltc As New CollectionToDataSet

        ds.Tables.Clear()
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("C_NO").ToString
        Dim pki As List(Of ProductionKaiLiaoInfo)

        pki = pkc.ProductionKaiLiao_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pki.Count = 0 Then
            MsgBox("��e�L���}�Ƴ�O��!")
            Exit Sub
        End If

        ltc.CollToDataSet(ds, "ProductionKaiLiao", pkc.ProductionKaiLiao_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))

        If pki(0).C_Check = True Then
            PreviewRPT(ds, "rptProductionKaiLiao", "�}�Ƴ�", True, True)
        Else
            PreviewRPT(ds, "rptProductionKaiLiao", "�}�Ƴ�", True, False)

        End If
        ltc = Nothing
    End Sub

    '�D�n�w��q�ܮw��^���ƦZ�����⨫�O���H���]�@���Φh���O���H�����ܵ��欰��!�^
    Private Sub cmsOutPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsOutPrint.Click
        If GridView1.RowCount = 0 Then Exit Sub

        tempValue = GridView1.GetFocusedRowCellValue("C_NO").ToString
        Dim frm As New frmOutPrint
        frm.ShowDialog()
    End Sub

    Private Sub cmsProductionAddA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionAddA.Click
        On Error Resume Next
        Edit = False

        Dim fr As frmProductionKaiLiaoA
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionKaiLiaoA Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "�}�Ƴ�"
        fr = New frmProductionKaiLiaoA
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmsProductionViewA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionViewA.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmProductionKaiLiaoA
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionKaiLiaoA Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "PreView"
        tempValue3 = GridView1.GetFocusedRowCellValue("C_Number").ToString
        fr = New frmProductionKaiLiaoA
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmsProductionCheckA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionCheckA.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim pi As List(Of ProductionKaiLiaoInfo)
        pi = pkc.ProductionKaiLiaoA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, GridView1.GetFocusedRowCellValue("C_Number").ToString, Nothing, Nothing, Nothing)

        If pi.Count = 0 Then Exit Sub

        If pi(0).C_Check = True Then
            Dim wic As New WareOutController
            Dim wi As List(Of WareOutInfo)
            wi = wic.WareOut_ProductionGetList(Nothing, GridView1.GetFocusedRowCellValue("C_NO").ToString)

            If wi.Count = 0 Then
                Dim fr As frmProductionKaiLiaoA
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmProductionKaiLiaoA Then
                        fr.Activate()
                        Exit Sub
                    End If
                Next
                tempValue2 = "Check"
                tempValue3 = GridView1.GetFocusedRowCellValue("C_Number").ToString
                fr = New frmProductionKaiLiaoA
                fr.MdiParent = MDIMain
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            Else
                MsgBox("�b�ܮw�w�s�b����Ƴ渹�O��,�����\�����I")
                Exit Sub
            End If
        Else

            Dim fr As frmProductionKaiLiaoA
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionKaiLiaoA Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue2 = "Check"
            tempValue3 = GridView1.GetFocusedRowCellValue("C_Number").ToString
            fr = New frmProductionKaiLiaoA
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
    End Sub

    Private Sub cmsProductionRefA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionRefA.Click
        Grid.DataSource = pkc.ProductionKaiLiaoA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strDPT, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub cmsProductionDelA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionDelA.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As List(Of ProductionKaiLiaoInfo)
        pi = pkc.ProductionKaiLiaoA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, GridView1.GetFocusedRowCellValue("C_Number").ToString, Nothing, Nothing, Nothing)

        If pi(0).C_Check = True Then
            MsgBox("���}�Ƴ�w�Q�f��,�����\�R��")
            Exit Sub
        Else
            If MsgBox("�T�w�n�R���s����" & GridView1.GetFocusedRowCellValue("C_Number").ToString & "���}�Ƴ渹�ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then
                If pkc.ProductionKaiLiaoA_Delete(GridView1.GetFocusedRowCellValue("C_Number").ToString, Nothing) = True Then
                    MsgBox("�R����e�}�Ƴ榨�\!")
                Else
                    MsgBox("�R������,���ˬd��]!")
                End If
                Grid.DataSource = pkc.ProductionKaiLiaoA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strDPT, Nothing, Nothing, Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub cmsProductionEditA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionEditA.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As List(Of ProductionKaiLiaoInfo)
        pi = pkc.ProductionKaiLiaoA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, GridView1.GetFocusedRowCellValue("C_Number").ToString, Nothing, Nothing, Nothing)
        If pi(0).C_Check = True Then
            MsgBox("���}�Ƴ�w�Q�f��,�����\�ק�")
            Exit Sub
        Else
            Edit = True
            Dim fr As frmProductionKaiLiaoA
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionKaiLiaoA Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue2 = "�}�Ƴ�"
            tempValue3 = GridView1.GetFocusedRowCellValue("C_Number").ToString
            fr = New frmProductionKaiLiaoA
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
    End Sub

    Private Sub cmsProductionPrintA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionPrintA.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim ds As New DataSet
        Dim ltc As New CollectionToDataSet

        ds.Tables.Clear()
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("C_NO").ToString
        Dim pki As List(Of ProductionKaiLiaoInfo)

        pki = pkc.ProductionKaiLiao_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pki.Count = 0 Then
            MsgBox("��e�L���}�Ƴ�O��!")
            Exit Sub
        End If

        ltc.CollToDataSet(ds, "ProductionKaiLiao", pkc.ProductionKaiLiao_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))

        If pki(0).C_Check = True Then
            PreviewRPT(ds, "rptProductionKaiLiao", "�}�Ƴ�", True, True)
        Else
            PreviewRPT(ds, "rptProductionKaiLiao", "�}�Ƴ�", True, False)

        End If

        ltc = Nothing
    End Sub

    Private Sub cmsProductionSelectA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionSelectA.Click
        Dim frm As New frmProductionKaiLiaoSelect
        frm.ShowDialog()

        If tempValue = "�����d�߫��s" Then
            Grid.DataSource = pkc.ProductionKaiLiao_GetList(tempValue2, tempValue3, tempValue6, Nothing, tempValue7, tempValue4, tempValue5, tempValue8, tempValue9, Nothing, strDPT)
        End If
        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing
        tempValue7 = Nothing
        tempValue8 = Nothing
        tempValue9 = Nothing
    End Sub

    Private Sub cmsOutPrintA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsOutPrintA.Click
        If GridView1.RowCount = 0 Then Exit Sub

        tempValue = GridView1.GetFocusedRowCellValue("C_NO").ToString
        Dim frm As New frmOutPrint
        frm.ShowDialog()
    End Sub
End Class