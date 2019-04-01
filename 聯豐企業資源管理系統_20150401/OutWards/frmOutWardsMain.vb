Imports LFERP.Library.OutWards
Imports LFERP.SystemManager

Public Class frmOutWardsMain

    Dim owc As New OutWardsController
    Dim isLFUser As Boolean

    ''' <summary>
    ''' �����k���桧�s�W��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsAdd.Click
        Dim fr As New frmOutWards
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' �����k���桧��s��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' ���L�{�Q�H�U�L�{�ե�
    ''' frmOutWardsMain_Load()
    ''' cmsDel_Click()
    Private Sub cmsRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsRef.Click
        GridControl1.DataSource = owc.OutWards_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -30, CDate(Format(Now, "yyyy/MM/dd"))), Nothing)
    End Sub
    ''' <summary>
    ''' ����[��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmOutWardsMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "800201")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsAdd.Enabled = True
                cmsOutWardsAdd.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "800202")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsEdit.Enabled = True
                cmsOutWardsEdit.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "800203")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsDel.Enabled = True
                cmsOutWardsDelete.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "800204")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsCheck.Enabled = True
                cmsOutWardsCheck.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "800207")  '@ 2013/3/26 �K�[
        If pmwiL.Count > 0 Then    '�p�שοi�M����
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                GridControl1.ContextMenuStrip = cmsTrip
                cmsRef_Click(Nothing, Nothing)        '�եΨ�s�L�{
                isLFUser = True
            Else '�̨ȥ�
                GridControl1.ContextMenuStrip = cmsMG
                OM_CusterID.FieldName = "OC_CustomerID"
                OM_CusterNO.FieldName = "OC_CustomerNO"
                OM_CusterPO.FieldName = "OC_CustomerPO"
                cmsOutWardsRef_Click(Nothing, Nothing)        '�եΨ�s�L�{
                isLFUser = False
            End If
        Else
            GridControl1.ContextMenuStrip = cmsMG
            OM_CusterID.FieldName = "OC_CustomerID"
            OM_CusterNO.FieldName = "OC_CustomerNO"
            OM_CusterPO.FieldName = "OC_CustomerPO"
            cmsOutWardsRef_Click(Nothing, Nothing)        '�եΨ�s�L�{
            isLFUser = False
        End If

    End Sub
    ''' <summary>
    ''' �����k���桧�ק
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmsEdit.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim owi As List(Of OutWardsInfo)

        owi = owc.OutWards_GetList(GridView1.GetFocusedRowCellValue("OW_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If owi.Count > 0 Then
            If owi(0).OW_Check = False Then
                Dim fr As New frmOutWards
                fr.lblTittle.Text = "�e�f--�ק�"
                fr.txtOW_NO.Text = GridView1.GetFocusedRowCellValue("OW_NO").ToString
                fr.MdiParent = MDIMain
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            Else
                MsgBox("���e�f��w�f�֡A�����\�ק�I", 64, "����")
            End If
        Else
            MsgBox("�ƾڮw�����s�b���e�f�渹�A���ˬd�O�_�w�Q�R���I", 64, "����")
        End If
    End Sub
    ''' <summary>
    ''' �����k���桧�d�ݡ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsPreView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmsPreView.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim fr As New frmOutWards
        fr.lblTittle.Text = "�e�f--�d��"
        fr.txtOW_NO.Text = GridView1.GetFocusedRowCellValue("OW_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' �����k���桧�f�֡�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsCheck.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim fr As New frmOutWards
        fr.lblTittle.Text = "�e�f--�f��"
        fr.txtOW_NO.Text = GridView1.GetFocusedRowCellValue("OW_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' �����k���桧�R����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsDel.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim owi As List(Of OutWardsInfo)

        owi = owc.OutWards_GetList(GridView1.GetFocusedRowCellValue("OW_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If owi.Count > 0 Then
            If owi(0).OW_Check = False Then
                If MsgBox("�T�w�n�R���e�f�渹���G" & GridView1.GetFocusedRowCellValue("OW_NO").ToString & "  ���Ҧ��O���ܡH", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "����") = MsgBoxResult.Ok Then
                    If owc.OutWards_Delete(Nothing, GridView1.GetFocusedRowCellValue("OW_NO").ToString) = True Then
                        MsgBox("�O���R�����\�I", 64, "����")
                        cmsRef_Click(Nothing, Nothing)             '�եΨ�s�L�{
                    Else
                        MsgBox("�O���R�����ѡI", 64, "����")
                    End If
                End If
            Else
                MsgBox("���e�f��w�f�֡A�����\�R���I", 64, "����")
            End If
        Else
            MsgBox("�ƾڮw�����s�b���e�f�渹�A���ˬd�O�_�w�Q�R���I", 64, "����")
        End If
    End Sub
    ''' <summary>
    ''' �����k���桧�d�ߡ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsQuery.Click
        Dim fr As New frmOutWardsFind

        fr.ShowDialog()

        If fr.isClickbtnOK = True Then        '�u�������d�ߵ��餤���d�߫��s�~�i��d��
            GridControl1.DataSource = owc.OutWards_GetList(tempValue, tempValue2, tempValue3, tempValue4, tempValue5, Nothing, Nothing, tempValue6, Nothing, tempValue7, tempValue8, tempValue9)
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

    Private Sub cmsPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPrint.Click, cmsDaoJuPrint.Click, cmsOutWardsPrint.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim strNO As String
        Dim ds As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim owi As List(Of OutWardsInfo)
        Dim strReportName As String

        strNO = GridView1.GetFocusedRowCellValue("OW_NO").ToString

        ds.Tables.Clear()
        If isLFUser = True Then   '�p�ץ�
            owi = owc.OutWards_GetList(strNO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            strReportName = "rptOutWards"
        Else
            owi = owc.OutWards_GetList1(strNO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            strReportName = "rptOutWards2"
        End If

        If owi.Count = 0 Then Exit Sub

        ltc.CollToDataSet(ds, "OutWards", owi)

        If owi(0).OW_Check = True Then
            If sender.name = "cmsPrint" Or sender.name = "cmsOutWardsPrint" Then
                PreviewRPT(ds, strReportName, "�e�f��", True, True)
            ElseIf sender.name = "cmsDaoJuPrint" Then
                PreviewRPT1(ds, "rptOutWards1", "�e�f��", UserName, "", True, True)
            End If
        Else
            If sender.name = "cmsPrint" Or sender.name = "cmsOutWardsPrint" Then
                PreviewRPT(ds, strReportName, "�e�f��", False, False)
            ElseIf sender.name = "cmsDaoJuPrint" Then
                PreviewRPT1(ds, "rptOutWards1", "�e�f��", UserName, "", False, False)
            End If

        End If

        ltc = Nothing
    End Sub

    Private Sub cmsOutWardsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsOutWardsAdd.Click
        Dim fr As New frmOutWardsT
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub

    Private Sub cmsOutWardsEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsOutWardsEdit.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim owi As List(Of OutWardsInfo)

        owi = owc.OutWards_GetList(GridView1.GetFocusedRowCellValue("OW_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If owi.Count > 0 Then
            If owi(0).OW_Check = False Then
                Dim fr As New frmOutWardsT
                fr.lblTittle.Text = "�e�f--�ק�"
                fr.txtOW_NO.Text = GridView1.GetFocusedRowCellValue("OW_NO").ToString
                fr.MdiParent = MDIMain
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            Else
                MsgBox("���e�f��w�f�֡A�����\�ק�I", 64, "����")
            End If
        Else
            MsgBox("�ƾڮw�����s�b���e�f�渹�A���ˬd�O�_�w�Q�R���I", 64, "����")
        End If
    End Sub

    Private Sub cmsOutWardsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsOutWardsDelete.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim owi As List(Of OutWardsInfo)

        owi = owc.OutWards_GetList(GridView1.GetFocusedRowCellValue("OW_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If owi.Count > 0 Then
            If owi(0).OW_Check = False Then
                If MsgBox("�T�w�n�R���e�f�渹���G" & GridView1.GetFocusedRowCellValue("OW_NO").ToString & "  ���Ҧ��O���ܡH", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "����") = MsgBoxResult.Ok Then
                    If owc.OutWards_Delete(Nothing, GridView1.GetFocusedRowCellValue("OW_NO").ToString) = True Then
                        MsgBox("�O���R�����\�I", 64, "����")
                        cmsOutWardsRef_Click(Nothing, Nothing)             '�եΨ�s�L�{
                    Else
                        MsgBox("�O���R�����ѡI", 64, "����")
                    End If
                End If
            Else
                MsgBox("���e�f��w�f�֡A�����\�R���I", 64, "����")
            End If
        Else
            MsgBox("�ƾڮw�����s�b���e�f�渹�A���ˬd�O�_�w�Q�R���I", 64, "����")
        End If
    End Sub

    Private Sub cmsOutWardsCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsOutWardsCheck.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim fr As New frmOutWardsT
        fr.lblTittle.Text = "�e�f--�f��"
        fr.txtOW_NO.Text = GridView1.GetFocusedRowCellValue("OW_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmsOutWardsView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsOutWardsView.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim fr As New frmOutWardsT
        fr.lblTittle.Text = "�e�f--�d��"
        fr.txtOW_NO.Text = GridView1.GetFocusedRowCellValue("OW_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmsOutWardsFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsOutWardsFind.Click
        Dim fr As New frmOutWardsFind
        tempValue10 = "MG"
        fr.ShowDialog()

        If fr.isClickbtnOK = True Then        '�u�������d�ߵ��餤���d�߫��s�~�i��d��
            GridControl1.DataSource = owc.OutWards_GetList1(tempValue, tempValue2, tempValue3, tempValue4, tempValue5, Nothing, Nothing, tempValue6, Nothing, tempValue7, Nothing, tempValue8, tempValue9)
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

    Private Sub cmsOutWardsRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsOutWardsRef.Click
        GridControl1.DataSource = owc.OutWards_GetList1(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -30, CDate(Format(Now, "yyyy/MM/dd"))), Nothing)
    End Sub


    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim FiledNameStr As String
        FiledNameStr = "OW_NO,OS_BatchID,OM_ID,OM_CusterID,OM_CusterNO,OM_CusterPO,PM_M_Code,M_Name,M_Gauge,M_Unit,PM_Type,OW_Qty,OS_Sprace,OW_Sprace,OW_Date,OW_Check"
        GridViewCopyMulitRow(GridView1, FiledNameStr, "ALL")
    End Sub


    Private Sub ToolStripSonHuoColl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSonHuoColl.Click
        frmOutWardsColl.ShowDialog()
        frmOutWardsColl.Dispose()
    End Sub

    Private Sub ToolStripPOColl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPOColl.Click
        FrmOrderCollect.ShowDialog()
        FrmOrderCollect.Dispose()
    End Sub
End Class