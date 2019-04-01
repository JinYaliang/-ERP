Imports LFERP.Library.ProductionOutProcess
Imports LFERP.Library.ProductionOWPAcceptance
Imports LFERP.SystemManager

Public Class frmProductionOutReturnMain
    Dim poc As New ProductionOutProcessControl

    Private Sub frmProductionOutProcessMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmsRef_Click(Nothing, Nothing)      '�եΨ�s�L�{

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88150301")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88150302")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88150303")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88150304")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88150305")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then RetrocedeToolStripMenu.Enabled = True
        End If

    End Sub

    '�����k���桧��s��
    Private Sub cmsRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsRef.Click
        Grid1.DataSource = poc.ProductionOutProcess_GetList(Nothing, Nothing, "�~�o���", Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")), Nothing)
        Grid1_Click(Nothing, Nothing)
    End Sub

    '�����k���桧�s�W��
    Private Sub cmsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsAdd.Click
        Dim frmPOPAdd As New frmProductionOutProcess

        frmPOPAdd.lblTittle.Text = "�~�o��׳�--�s�W"
        frmPOPAdd.strPO_Type = "�~�o���"
        frmPOPAdd.MdiParent = MDIMain
        frmPOPAdd.WindowState = FormWindowState.Maximized
        frmPOPAdd.Show()
    End Sub

    '�����k���桧�ק
    Private Sub cmsEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsEdit.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim poi As List(Of ProductionOutProcessInfo)
        poi = poc.ProductionOutProcess_GetList(Nothing, GridView1.GetFocusedRowCellValue("PO_ID"), "�~�o���", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If poi(0).PO_Check = True Then      '�P�_�~�o��׳�O�_�w�f��
            MsgBox("���~�o��׳�w�f�֡A�����\�ק�!", 64, "����")
            Exit Sub
        End If

        Dim frmPOPModify As New frmProductionOutProcess
        frmPOPModify.lblTittle.Text = "�~�o��׳�--�ק�"
        frmPOPModify.strPO_Type = "�~�o���"
        frmPOPModify.MdiParent = MDIMain
        frmPOPModify.WindowState = FormWindowState.Maximized
        frmPOPModify.txtPO_ID.Text = GridView1.GetFocusedRowCellValue("PO_ID").ToString
        frmPOPModify.Show()
    End Sub

    '�����k���桧�d�ݡ�
    Private Sub cmsPreView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPreView.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim frmPOPView As New frmProductionOutProcess
        frmPOPView.lblTittle.Text = "�~�o��׳�--�d��"
        frmPOPView.strPO_Type = "�~�o���"
        frmPOPView.MdiParent = MDIMain
        frmPOPView.WindowState = FormWindowState.Maximized
        frmPOPView.txtPO_ID.Text = GridView1.GetFocusedRowCellValue("PO_ID").ToString
        frmPOPView.Show()
    End Sub

    '�����k���桧�f�֡�
    Private Sub cmsCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmsCheck.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim poi As List(Of ProductionOutProcessInfo)
        poi = poc.ProductionOutProcess_GetList(Nothing, GridView1.GetFocusedRowCellValue("PO_ID"), "�~�o���", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If poi(0).PO_Check = True Then      '�P�_�~�o��׳�O�_�w�f��
            MsgBox("���~�o��׳�w�f�֡A���ݦA�f��!", 64, "����")
            Exit Sub
        End If

        Dim frmPOPCheck As New frmProductionOutProcess
        frmPOPCheck.lblTittle.Text = "�~�o��׳�--�f��"
        frmPOPCheck.strPO_Type = "�~�o���"
        frmPOPCheck.MdiParent = MDIMain
        frmPOPCheck.WindowState = FormWindowState.Maximized
        frmPOPCheck.txtPO_ID.Text = GridView1.GetFocusedRowCellValue("PO_ID").ToString
        frmPOPCheck.Show()
    End Sub

    '�����k���桧�R����
    Private Sub cmsDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsDel.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim poi As List(Of ProductionOutProcessInfo)
        poi = poc.ProductionOutProcess_GetList(Nothing, GridView1.GetFocusedRowCellValue("PO_ID"), "�~�o���", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If poi.Count > 0 Then       '�P�_�ƾڮw���O�_�s�b�����ʳ渹
            If poi(0).PO_Check = False Then      '�P�_�~�o��׳�O�_�w�f��
                If MsgBox("�T�w�n�R���~�o��׳渹���G" & GridView1.GetFocusedRowCellValue("PO_ID") & " ���O���ܡH", MsgBoxStyle.OkCancel + MsgBoxStyle.Question, "����") = MsgBoxResult.Ok Then
                    If poc.ProductionOutProcess_Delete(Nothing, GridView1.GetFocusedRowCellValue("PO_ID")) = True Then
                        MsgBox("�O���R�����\!", 64, "����")
                        cmsRef_Click(Nothing, Nothing)
                    Else
                        MsgBox("�O���R������!", 64, "����")
                    End If
                End If
            Else
                MsgBox("���~�o��׳�w�f�֡A�����\�R��!", 64, "����")
            End If
        Else
            MsgBox("���O���ƾڮw���s�b!", 64, "����")
        End If
    End Sub

    '����Grid1,�bGrid2����ܿ襤�~�o�檺�禬�H��
    '���L�{�Q�H�U�L�{�եΡG
    'cmsRef_Click()
    'Grid1_KeyUp()
    Private Sub Grid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid1.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim ac As New ProductionOWPAcceptanceControl
        Grid2.DataSource = ac.ProductionOWPAcceptance_GetList(Nothing, Nothing, GridView1.GetFocusedRowCellValue("PO_ID"), Nothing, Nothing, Nothing, GridView1.GetFocusedRowCellValue("PS_NO"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)


        Dim acc As New ProductionOutProcessControl
        Grid3.DataSource = acc.ProductionOutRetrocede_GetList(Nothing, GridView1.GetFocusedRowCellValue("AutoID"))

    End Sub

    '���V�U�ΦV�W��V��ɡA�եγ���Grid1�ƥ�L�{,�bGrid2����ܿ襤�~�o�檺�禬�H��
    Private Sub Grid1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grid1.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            Grid1_Click(Nothing, Nothing)
        End If
    End Sub

    '����Grid2���k���� ���d�ݡ��A��ܬd�ݵ���A�d���禬��H��
    Private Sub popViewAcceptance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popViewAcceptance.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As FrmProductionOWPAcceptance

        MTypeName = "OWPView"
        fr = New FrmProductionOWPAcceptance
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        tempValue2 = GridView2.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        fr.Show()
    End Sub

    '�����k���桧�d�ߡ�
    Private Sub cmsQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsQuery.Click

        Dim frmPOPFind As New frmProductionOutProcessFind
        frmPOPFind.strPo_Type = "�~�o���"
        frmPOPFind.ShowDialog()
        If frmPOPFind.isClickcmdFind = True Then
            Grid1.DataSource = poc.ProductionOutProcess_GetList(Nothing, tempValue, "�~�o���", tempValue2, tempValue3, tempValue4, tempValue5, tempValue6, tempValue7, Nothing)
            tempValue = ""
            tempValue2 = ""
            tempValue3 = ""
            tempValue4 = ""
            tempValue5 = ""
            tempValue6 = ""
            tempValue7 = ""
        End If
    End Sub

    Private Sub cmsPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPrint.Click
        Dim ds As New DataSet

        If GridView1.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet

        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim suc As New LFERP.SystemManager.SystemUser.SystemUserController
        Dim sui As List(Of LFERP.SystemManager.SystemUser.SystemUserInfo)
        Dim strCompany As String

        sui = suc.SystemUser_GetList(InUserID, Nothing, Nothing)
        strCompany = Mid(sui(0).DPT_ID, 1, 4)

        ds.Tables.Clear()

        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("PO_ID").ToString

        ltc.CollToDataSet(ds, "ProductionOutProcess", poc.ProductionOutProcess_GetList(Nothing, strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))

        ds.Tables("ProductionOutProcess").Columns.Add("P_ID", GetType(Integer))
        Dim i As Long

        For i = 0 To ds.Tables("ProductionOutProcess").Rows.Count - 1
            ds.Tables("ProductionOutProcess").Rows(i)("P_ID") = i + 1
        Next

        PreviewRPT(ds, "rptProductionOutProcess1", "�~�o�[�u��", True, True)

        ltc = Nothing
        ltc1 = Nothing

    End Sub

    Private Sub RetrocedeToolStripMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetrocedeToolStripMenu.Click
        '�P�_�~�o�[�u��O�_�w���禬�O��
        Dim ac As New ProductionOWPAcceptanceControl
        Dim al As New List(Of ProductionOWPAcceptanceInfo)



        al = ac.ProductionOWPAcceptance_GetList(GridView1.GetFocusedRowCellValue("AutoID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If al.Count = 0 Then
        Else
            MsgBox("���~�o��w���禬�O��,������ƶq!", 64, "����")
            Exit Sub
        End If

        tempValue = GridView1.GetFocusedRowCellValue("AutoID")
        tempValue2 = "���"


        frmProductionOutRetrocede.ShowDialog()
        frmProductionOutRetrocede.Dispose()
    End Sub
End Class