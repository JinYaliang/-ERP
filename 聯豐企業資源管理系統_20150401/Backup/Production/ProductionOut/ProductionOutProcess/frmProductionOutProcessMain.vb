Imports LFERP.Library.ProductionOutProcess
Imports LFERP.Library.ProductionOWPAcceptance
Imports LFERP.SystemManager
Imports LFERP.Library.ProductionDPTWareInventory

Public Class frmProductionOutProcessMain
    Dim poc As New ProductionOutProcessControl

    Private Sub frmProductionOutProcessMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmsRef_Click(Nothing, Nothing)      '�եΨ�s�L�{

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88150101")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88150102")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88150103")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88150104")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88150105")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmsCancelCheck.Enabled = True
        End If

        'RetrocedeToolStripMenu
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88150106")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then RetrocedeToolStripMenu.Enabled = True
        End If

    End Sub

    '�����k���桧��s��
    '���L�{�Q�H�U�L�{�եΡG
    'frmProductionOutProcessMain_Load()
    'cmsDel_Click()
    'cmsCancelCheck_Click
    Private Sub cmsRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsRef.Click
        ''2012-8-27
        If poc.ProductionOutProcess_GetList(Nothing, Nothing, "�~�o�[�u", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 1).Count > 0 Then
            Grid1.DataSource = poc.ProductionOutProcess_GetList(Nothing, Nothing, "�~�o�[�u", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 1)
        Else
            Grid1.DataSource = poc.ProductionOutProcess_GetList(Nothing, Nothing, "�~�o�[�u", Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")), Nothing)
        End If

        Grid1_Click(Nothing, Nothing)   '�եγ���Grid1�ƥ�L�{
    End Sub

    '�����k���桧�s�W��
    Private Sub cmsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsAdd.Click
        Dim frmPOPAdd As New frmProductionOutProcess

        frmPOPAdd.lblTittle.Text = "�~�o�[�u��--�s�W"
        frmPOPAdd.strPO_Type = "�~�o�[�u"
        frmPOPAdd.MdiParent = MDIMain
        frmPOPAdd.WindowState = FormWindowState.Maximized
        frmPOPAdd.Show()
    End Sub

    '�����k���桧�ק
    Private Sub cmsEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsEdit.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim poi As List(Of ProductionOutProcessInfo)
        poi = poc.ProductionOutProcess_GetList(Nothing, GridView1.GetFocusedRowCellValue("PO_ID"), "�~�o�[�u", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If poi(0).PO_Check = True Then      '�P�_�~�o�[�u��O�_�w�f��
            MsgBox("���~�o�[�u��w�f�֡A�����\�ק�!", 64, "����")
            Exit Sub
        End If

        Dim frmPOPModify As New frmProductionOutProcess
        frmPOPModify.lblTittle.Text = "�~�o�[�u��--�ק�"
        frmPOPModify.strPO_Type = "�~�o�[�u"
        frmPOPModify.MdiParent = MDIMain
        frmPOPModify.WindowState = FormWindowState.Maximized
        frmPOPModify.txtPO_ID.Text = GridView1.GetFocusedRowCellValue("PO_ID").ToString
        frmPOPModify.Show()
    End Sub

    '�����k���桧�d�ݡ�
    Private Sub cmsPreView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPreView.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim frmPOPView As New frmProductionOutProcess
        frmPOPView.lblTittle.Text = "�~�o�[�u��--�d��"
        frmPOPView.strPO_Type = "�~�o�[�u"
        frmPOPView.MdiParent = MDIMain
        frmPOPView.WindowState = FormWindowState.Maximized
        frmPOPView.txtPO_ID.Text = GridView1.GetFocusedRowCellValue("PO_ID").ToString
        frmPOPView.Show()
    End Sub

    '�����k���桧�f�֡�
    Private Sub cmsCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmsCheck.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim poi As List(Of ProductionOutProcessInfo)
        poi = poc.ProductionOutProcess_GetList(Nothing, GridView1.GetFocusedRowCellValue("PO_ID"), "�~�o�[�u", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If poi(0).PO_Check = True Then      '�P�_�~�o�[�u��O�_�w�f��
            MsgBox("���~�o�[�u��w�f�֡A���ݦA�f��!", 64, "����")
            Exit Sub
        End If

        Dim frmPOPCheck As New frmProductionOutProcess
        frmPOPCheck.lblTittle.Text = "�~�o�[�u��--�f��"
        frmPOPCheck.strPO_Type = "�~�o�[�u"
        frmPOPCheck.MdiParent = MDIMain
        frmPOPCheck.WindowState = FormWindowState.Maximized
        frmPOPCheck.txtPO_ID.Text = GridView1.GetFocusedRowCellValue("PO_ID").ToString
        frmPOPCheck.Show()
    End Sub

    '�����k���桧�R����
    Private Sub cmsDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsDel.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim poi As List(Of ProductionOutProcessInfo)
        poi = poc.ProductionOutProcess_GetList(Nothing, GridView1.GetFocusedRowCellValue("PO_ID"), "�~�o�[�u", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If poi.Count > 0 Then       '�P�_�ƾڮw���O�_�s�b���~�o�[�u�渹
            If poi(0).PO_Check = False Then      '�P�_�~�o�[�u��O�_�w�f��
                If MsgBox("�T�w�n�R���~�o�[�u�渹���G" & GridView1.GetFocusedRowCellValue("PO_ID") & " ���O���ܡH", MsgBoxStyle.OkCancel + MsgBoxStyle.Question, "����") = MsgBoxResult.Ok Then
                    If poc.ProductionOutProcess_Delete(Nothing, GridView1.GetFocusedRowCellValue("PO_ID")) = True Then
                        MsgBox("�O���R�����\!", 64, "����")
                        cmsRef_Click(Nothing, Nothing)
                    Else
                        MsgBox("�O���R������!", 64, "����")
                    End If
                End If
            Else
                MsgBox("���~�o�[�u��w�f�֡A�����\�R��!", 64, "����")
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
        frmPOPFind.strPo_Type = "�~�o�[�u"
        frmPOPFind.ShowDialog()
        If frmPOPFind.isClickcmdFind = True Then
            Grid1.DataSource = poc.ProductionOutProcess_GetList(Nothing, tempValue, "�~�o�[�u", tempValue2, tempValue3, tempValue4, tempValue5, tempValue6, tempValue7, Nothing)
            tempValue = ""
            tempValue2 = ""
            tempValue3 = ""
            tempValue4 = ""
            tempValue5 = ""
            tempValue6 = ""
            tempValue7 = ""
        End If
    End Sub
    '�����k���桧�C�L��
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
    ''' <summary>
    ''' '�����k���桧�����f�֡�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' @ 2012/7/2 �K�[ �����f��
    Private Sub cmsCancelCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsCancelCheck.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim popi As List(Of ProductionOutProcessInfo)
        '�P�_�ƾڮw���O�_�s�b�~�o�渹
        popi = poc.ProductionOutProcess_GetList(Nothing, GridView1.GetFocusedRowCellValue("PO_ID"), "�~�o�[�u", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If popi.Count <= 0 Then
            MsgBox("�ƾڮw�����s�ӥ~�o�渹(" & GridView1.GetFocusedRowCellValue("PO_ID") & ")�I", 64, "����")
            Exit Sub
        End If

        '�P�_�~�o�[�u��O�_�w�f��
        If popi(0).PO_Check = False Then
            MsgBox("���~�o�[�u�楼�f�֡A��������f��!", 64, "����")
            Exit Sub
        End If

        '�P�_�~�o�[�u��O�_�w���禬�O��
        Dim ac As New ProductionOWPAcceptanceControl
        If ac.ProductionOWPAcceptance_GetList(Nothing, Nothing, GridView1.GetFocusedRowCellValue("PO_ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing).Count > 0 Then
            MsgBox("���~�o��w���禬�O���A��������f��", 64, "����")
            Exit Sub
        End If

        Dim poi As New ProductionOutProcessInfo
        Dim pdc As New ProductionDPTWareInventoryControl

        poi.PO_ID = GridView1.GetFocusedRowCellValue("PO_ID")
        poi.PO_Check = False
        poi.PO_CheckUserID = popi(0).PO_CheckUserID
        poi.PO_CheckDate = popi(0).PO_CheckDate
        poi.PO_CheckRemark = popi(0).PO_CheckRemark

        If poc.ProductionOutProcess_Check(poi) = True Then      '�P�_�f�֫H���O�_��s����
            Dim pdi As List(Of ProductionDPTWareInventoryInfo)
            Dim pdiUpdate As New ProductionDPTWareInventoryInfo
            Dim i%

            '�����f�֮ɡA�ݧ�~�o�ƶq�W�[�^�w�s�ƶq
            For i = 0 To popi.Count - 1
                pdi = pdc.ProductionDPTWareInventory_GetList("F101", popi(i).PS_NO, Nothing)

                If pdi.Count > 0 Then
                    pdiUpdate.M_Code = popi(i).PS_NO
                    pdiUpdate.DPT_ID = "F101"
                    pdiUpdate.WI_Qty = pdi(0).WI_Qty + popi(i).PO_Qty
                    If pdc.UpdateProductionField_Qty(pdiUpdate) = False Then
                        MsgBox("�w�s�ƶq�ܧ󥢱�!", 64, "����")
                        Exit Sub
                    End If
                End If
            Next

            cmsRef_Click(Nothing, Nothing)
            MsgBox("�w�����f��!", 64, "����")
        Else
            MsgBox("�����f�֥���!", 64, "����")
        End If
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
        tempValue2 = "�j�f"


        frmProductionOutRetrocede.ShowDialog()
        frmProductionOutRetrocede.Dispose()

    End Sub
End Class