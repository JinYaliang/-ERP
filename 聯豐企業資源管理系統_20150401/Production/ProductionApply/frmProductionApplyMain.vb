Imports LFERP.Library.ProductionApply
Imports LFERP.SystemManager

Public Class frmProductionApplyMain
    Dim pac As New ProductionApplyControl

    Private Sub frmProductionApplyMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmdRef_Click(Nothing, Nothing)

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881301")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881302")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881303")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881404")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdCheck.Enabled = True
        End If
    
    End Sub

    '�����k���桧��s��
    '���L�{�Q�H�U�L�{�ե�
    'frmProductionApplyMain_Load()
    'cmdDel_Click()
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        Grid.DataSource = pac.ProductionApply_GetList1()
    End Sub

    '�����k���桧�s�W��
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        frmProductionApply.MdiParent = MDIMain
        frmProductionApply.WindowState = FormWindowState.Maximized
        frmProductionApply.Show()
    End Sub

    '�����k���桧�ק
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim pai As List(Of ProductionApplyInfo)
        pai = pac.ProductionApply_GetList(GridView1.GetFocusedRowCellValue("PA_ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pai(0).PA_Check = True Then      '�P�_���ʳ�O�_�w�f��
            MsgBox("�����ʳ�w�f�֡A�����\�ק�", 64, "����")
            Exit Sub
        End If

        Dim frmPAModify As New frmProductionApply
        frmPAModify.lblTitle.Text = "���ʳ�--�ק�"
        frmPAModify.MdiParent = MDIMain
        frmPAModify.WindowState = FormWindowState.Maximized
        frmPAModify.txtPA_ID.Text = GridView1.GetFocusedRowCellValue("PA_ID").ToString
        frmPAModify.Show()
    End Sub

    '�����k���桧�R����
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim pai As List(Of ProductionApplyInfo)
        pai = pac.ProductionApply_GetList(GridView1.GetFocusedRowCellValue("PA_ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pai.Count > 0 Then       '�P�_�ƾڮw���O�_�s�b�����ʳ渹
            If pai(0).PA_Check = False Then      '�P�_���ʳ�O�_�w�f��
                If MsgBox("�T�w�n�R�����ʳ渹���G" & GridView1.GetFocusedRowCellValue("PA_ID") & " ���O���ܡH", MsgBoxStyle.OkCancel + MsgBoxStyle.Question, "����") = MsgBoxResult.Ok Then
                    If pac.ProductionApply_Delete(Nothing, GridView1.GetFocusedRowCellValue("PA_ID")) = True Then
                        MsgBox("�O���R�����\!", 64, "����")
                        cmdRef_Click(Nothing, Nothing)
                    Else
                        MsgBox("�O���R������!", 64, "����")
                    End If
                End If
            Else
                MsgBox("�����ʳ�w�f�֡A�����\�R��!", 64, "����")
            End If
        Else
            MsgBox("���O���ƾڮw���s�b!", 64, "����")
        End If

    End Sub

    '�����k���桧�d�ݡ�
    Private Sub cmdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        If GridView1.RowCount = 0 Then Exit Sub
      
        Dim frmPAView As New frmProductionApply
        frmPAView.lblTitle.Text = "���ʳ�--�d��"
        frmPAView.MdiParent = MDIMain
        frmPAView.WindowState = FormWindowState.Maximized
        frmPAView.txtPA_ID.Text = GridView1.GetFocusedRowCellValue("PA_ID").ToString
        frmPAView.Show()
    End Sub

    '�����k���桧�f�֡�
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim pai As List(Of ProductionApplyInfo)
        pai = pac.ProductionApply_GetList(GridView1.GetFocusedRowCellValue("PA_ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pai(0).PA_Check = True Then      '�P�_���ʳ�O�_�w�f��
            MsgBox("�����ʳ�w�f�֡A���ݦA�f��!", 64, "����")
            Exit Sub
        End If

        Dim frmPACheck As New frmProductionApply
        frmPACheck.lblTitle.Text = "���ʳ�--�f��"
        frmPACheck.MdiParent = MDIMain
        frmPACheck.WindowState = FormWindowState.Maximized
        frmPACheck.txtPA_ID.Text = GridView1.GetFocusedRowCellValue("PA_ID").ToString
        frmPACheck.Show()
    End Sub

    '�����k���桧�d�ߡ�
    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        Dim frm As New frmProductionApplyFind
        frm.ShowDialog()
        If frm.isClickcmdOK = True Then     '�u�������d�߫��s�A�åB��J�L�~�����p�U�A�~����d��
            Grid.DataSource = pac.ProductionApply_GetList(tempValue, tempValue2, tempValue3, tempValue4, tempValue5, tempValue6, tempValue7, tempValue8)
        End If
        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing
        tempValue7 = Nothing
        tempValue8 = Nothing
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim strNO As String
        strNO = GridView1.GetFocusedRowCellValue("PA_ID").ToString

        Dim ds As New DataSet
        ds.Tables.Clear()

        Dim ltc As New CollectionToDataSet

        Dim pai As List(Of ProductionApplyInfo)

        pai = pac.ProductionApply_GetList(strNO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pai.Count = 0 Then Exit Sub

        ltc.CollToDataSet(ds, "ProductionApply", pac.ProductionApply_GetList(strNO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))

        If pai(0).PA_Check = True Then
            PreviewRPT(ds, "rptProductionApply", "���ʳ�", True, True)
        Else
            PreviewRPT(ds, "rptProductionApply", "���ʳ�", True, False)

        End If

        ltc = Nothing
    End Sub
End Class