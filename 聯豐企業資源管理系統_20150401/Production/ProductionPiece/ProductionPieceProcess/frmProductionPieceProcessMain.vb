Imports LFERP.Library.ProductionPieceProcess
Imports LFERP.SystemManager

Public Class frmProductionPieceProcessMain
    Dim ppc As New ProductionPieceProcessControl

    Private Sub frmProductionPieceProcessMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmdRef_Click(Nothing, Nothing)

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160101")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160102")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160103")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160104")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160105")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdPrint.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160106")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then PP_PriceInputToolStripMenuItem.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160107")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then PP_PriceCheckToolStripMenuItem.Enabled = True
        End If
    End Sub
    ''' <summary>
    ''' �����k���桧��s��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' frmProductionPieceProcessMain_Load()
    ''' cmdDel_Click()
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        'Grid.DataSource = ppc.ProductionPieceProcess_GetList1(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -31, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")), Nothing)
        Grid.DataSource = ppc.ProductionPieceProcess_GetList1(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub
    ''' <summary>
    ''' �����k���桧�s�W��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        frmProductionPieceProcess.MdiParent = MDIMain
        frmProductionPieceProcess.WindowState = FormWindowState.Maximized
        frmProductionPieceProcess.Show()
    End Sub
    ''' <summary>
    ''' �����k���桧�ק
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim ppi As List(Of ProductionPieceProcessInfo)
        ppi = ppc.ProductionPieceProcess_GetList(Nothing, GridView1.GetFocusedRowCellValue("PP_ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If ppi(0).PP_Check = True Then      '�P�_�p��u����O�_�w�f��
            MsgBox("���p��u����w�f�֡A�����\�ק�!", 64, "����")
            Exit Sub
        End If

        Dim frmPPPModify As New frmProductionPieceProcess
        frmPPPModify.lblTittle.Text = "�p��u����--�ק�"
        frmPPPModify.MdiParent = MDIMain
        frmPPPModify.WindowState = FormWindowState.Maximized
        frmPPPModify.txtPP_ID.Text = GridView1.GetFocusedRowCellValue("PP_ID").ToString
        frmPPPModify.Show()
    End Sub
    ''' <summary>
    ''' �����k���桧�f�֡�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim frmPPPCheck As New frmProductionPieceProcess
        frmPPPCheck.lblTittle.Text = "�p��u����--�f��"
        frmPPPCheck.MdiParent = MDIMain
        frmPPPCheck.WindowState = FormWindowState.Maximized
        frmPPPCheck.txtPP_ID.Text = GridView1.GetFocusedRowCellValue("PP_ID").ToString
        frmPPPCheck.Show()
    End Sub
    ''' <summary>
    ''' �����k���桧�d�ݡ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim frmPPPView As New frmProductionPieceProcess
        frmPPPView.lblTittle.Text = "�p��u����--�d��"
        frmPPPView.MdiParent = MDIMain
        frmPPPView.WindowState = FormWindowState.Maximized
        frmPPPView.txtPP_ID.Text = GridView1.GetFocusedRowCellValue("PP_ID").ToString
        frmPPPView.Show()
    End Sub
    ''' <summary>
    ''' �����k���桧�R����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim ppi As List(Of ProductionPieceProcessInfo)
        ppi = ppc.ProductionPieceProcess_GetList(Nothing, GridView1.GetFocusedRowCellValue("PP_ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If ppi.Count > 0 Then       '�P�_�ƾڮw���O�_�s�b���p��u���渹
            If ppi(0).PP_Check = False Then      '�P�_�p��u����O�_�w�f��
                If MsgBox("�T�w�n�R���p��u���渹���G" & GridView1.GetFocusedRowCellValue("PP_ID") & " ���O���ܡH", MsgBoxStyle.OkCancel + MsgBoxStyle.Question, "����") = MsgBoxResult.Ok Then
                    If ppc.ProductionPieceProcess_Delete(Nothing, GridView1.GetFocusedRowCellValue("PP_ID")) = True Then
                        MsgBox("�O���R�����\!", 64, "����")
                        cmdRef_Click(Nothing, Nothing)
                    Else
                        MsgBox("�O���R������!", 64, "����")
                    End If
                End If

            Else
                MsgBox("���p��u����w�f�֡A�����\�R��!", 64, "����")
            End If
        Else
            MsgBox("���O���ƾڮw���s�b!", 64, "����")
        End If

    End Sub
    ''' <summary>
    ''' �����k���桧�d�ߡ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        'Dim frmPPPFind As New frmProductionPieceProcessFind

        'frmPPPFind.ShowDialog()
        'If frmPPPFind.isClickbtnFind = True Then
        '    Grid.DataSource = ppc.ProductionPieceProcess_GetList1(tempValue, tempValue2, tempValue3, tempValue4, Nothing, Nothing, Nothing, tempValue5, tempValue6, Nothing)
        '    tempValue = ""
        '    tempValue2 = ""
        '    tempValue3 = ""
        '    tempValue4 = ""
        '    tempValue5 = ""
        '    tempValue6 = ""
        '    tempValue7 = ""
        'End If
        tempValue = "�p��u��"
        ProductionPieceSelectAll.ShowDialog()

        Dim frmPPPFind As New frmProductionPieceProcessFind

        Select Case tempValue
            Case "0" '�۩w�q
                Dim PPS As New LFERP.Library.ProductionPiece_Select.ProductionPiece_SelectControl

                Grid.DataSource = PPS.ProductionPieceProcess_GetListSelect("�p��u��", tempValue2)

            Case "1" '�T�w�Ҧ�
                If tempValue3 = "�u���渹" Then
                    Grid.DataSource = ppc.ProductionPieceProcess_GetList1(tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue6, tempValue4, tempValue5, strInDepID)
                ElseIf tempValue3 = "���~�s��" Then
                    Grid.DataSource = ppc.ProductionPieceProcess_GetList1(Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, tempValue6, tempValue4, tempValue5, strInDepID)
                End If
            Case "2" '�t�O ����
                Grid.DataSource = ppc.ProductionPieceProcess_GetList1(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue4, tempValue5, tempValue2)

        End Select

        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing

        ProductionPieceSelectAll.Dispose()

    End Sub
    ''' <summary>
    ''' �����k���桧�C�L(�u��)��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        On Error Resume Next

        Dim ds As New DataSet

        If GridView1.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet

        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim strCompany As String

        strCompany = Mid(strInDPT_ID, 1, 4)   '��o�n���̩��ݤ��qID,�H��^���q�W�١ALOGO

        ds.Tables.Clear()

        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("PP_ID").ToString
        If ppc.ProductionPieceProcess_GetList(Nothing, strA, Nothing, Nothing, Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing).Count <= 0 Then
            MsgBox("�A�Ҧb�����b���p��u���椤�L�O��!", 64, "����")
            Exit Sub
        End If

        ltc.CollToDataSet(ds, "ProductionPieceProcess", ppc.ProductionPieceProcess_GetList(Nothing, strA, Nothing, Nothing, Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))

        ds.Tables("ProductionPieceProcess").Columns.Add("P_ID", GetType(Integer))
        Dim i As Long

        For i = 0 To ds.Tables("ProductionPieceProcess").Rows.Count - 1
            ds.Tables("ProductionPieceProcess").Rows(i)("P_ID") = i + 1
        Next

        PreviewRPT(ds, "rptProductionPieceProcess", "�p��u����", True, True)

        ltc = Nothing
        ltc1 = Nothing
    End Sub
    ''' <summary>
    ''' �����k���桧�C�L��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdPrintNoPrice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrintNoPrice.Click
        Dim ds As New DataSet

        If GridView1.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet

        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim strCompany As String

        strCompany = Mid(strInDPT_ID, 1, 4)   '��o�n���̩��ݤ��qID,�H��^���q�W�١ALOGO

        ds.Tables.Clear()

        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("PP_ID").ToString

        ltc.CollToDataSet(ds, "ProductionPieceProcess", ppc.ProductionPieceProcess_GetList(Nothing, strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))

        ds.Tables("ProductionPieceProcess").Columns.Add("P_ID", GetType(Integer))
        Dim i As Long

        For i = 0 To ds.Tables("ProductionPieceProcess").Rows.Count - 1
            ds.Tables("ProductionPieceProcess").Rows(i)("P_ID") = i + 1
        Next

        PreviewRPT(ds, "rptProductionPieceProcess1", "�p��u����", True, True)

        ltc = Nothing
        ltc1 = Nothing
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PP_PriceInputToolStripMenuItem.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        'Dim ppi As List(Of ProductionPieceProcessInfo)
        'ppi = ppc.ProductionPieceProcess_GetList(Nothing, GridView1.GetFocusedRowCellValue("PP_ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        'If ppi(0).PP_PriceCheck = True Then      '�P�_�p��u����O�_�w�f��
        '    MsgBox("���p��u������w�f�֡A�����\���J!", 64, "����")
        '    Exit Sub
        'End If

        Dim frmPPPModify As New frmProductionPieceProcess
        frmPPPModify.lblTittle.Text = "�p��u����--������J"
        frmPPPModify.MdiParent = MDIMain
        frmPPPModify.WindowState = FormWindowState.Maximized
        frmPPPModify.txtPP_ID.Text = GridView1.GetFocusedRowCellValue("PP_ID").ToString
        frmPPPModify.Show()
    End Sub

    Private Sub PP_PriceCheckToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PP_PriceCheckToolStripMenuItem.Click
        If GridView1.RowCount <= 0 Then Exit Sub


        'Dim ppi As List(Of ProductionPieceProcessInfo)
        'ppi = ppc.ProductionPieceProcess_GetList(Nothing, GridView1.GetFocusedRowCellValue("PP_ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        'If ppi(0).PP_Check = True Then      '�P�_�p��u����O�_�w�f��
        'Else
        '    MsgBox("���p��u���楼�f��,��������f��!", 64, "����")
        '    Exit Sub
        'End If


        Dim frmPPPCheck As New frmProductionPieceProcess
        frmPPPCheck.lblTittle.Text = "�p��u����--����f��"
        frmPPPCheck.MdiParent = MDIMain
        frmPPPCheck.WindowState = FormWindowState.Maximized
        frmPPPCheck.txtPP_ID.Text = GridView1.GetFocusedRowCellValue("PP_ID").ToString
        frmPPPCheck.Show()
    End Sub

    Private Sub ToolStripNOCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripNOCheck.Click
       
        tempValue = "�C�L���f�֤u��"

        ProductionPieceSelectAll.ShowDialog()

        tempValue = Nothing
        tempValue8 = Nothing
        ProductionPieceSelectAll.Dispose()
    End Sub
End Class