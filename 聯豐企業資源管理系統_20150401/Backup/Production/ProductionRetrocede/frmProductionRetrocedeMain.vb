Imports LFERP.Library.ProductionRetrocede
Imports LFERP.SystemManager
Imports LFERP.Library.ProductionSelect


Public Class frmProductionRetrocedeMain
    Dim prc As New ProductionRetrocedeControl
    ''' <summary>
    ''' ����Ұʨƥ�
    ''' </summary>
    Private Sub frmProductionRetrocedeMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '        Grid1.DataSource = prc.ProductionRetrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        RetrocedeRef_Click(Nothing, Nothing)
        UserPower()
    End Sub
    Function FunWareSelect(ByVal FunWare As String) As String
        Dim a As New LFERP.SystemManager.PermissionModuleWarrantSubController
        Dim b As New List(Of LFERP.SystemManager.PermissionModuleWarrantSubInfo)
        b = a.PermissionModuleWarrantSub_GetList(InUserID, FunWare)
        Dim i, n As Integer
        Dim arr(n) As String

        Dim WareSelect As String = ""
        If b.Count > 0 Then
            arr = Split(b.Item(0).PMWS_Value, ",")
            n = Len(Replace(b.Item(0).PMWS_Value, ",", "," & "*")) - Len(b.Item(0).PMWS_Value)
            For i = 0 To n
                If i = 0 Then
                    WareSelect = "'" & arr(i) & "'"
                Else
                    WareSelect = WareSelect & ",'" & arr(i) & "'"
                End If
            Next
        End If
        FunWareSelect = WareSelect
    End Function

    ''' <summary>
    ''' �]�m�Τ��v��
    ''' </summary>
    Sub UserPower()  '�]�m�Τ��v��
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880801")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then RetrocedeAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880802")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then RetrocedeEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880803")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then RetrocedeDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880804")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then RetrocedeCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880805")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then RetrocedePrint.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880808")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripIncheck.Enabled = True
        End If




    End Sub
    ''' <summary>
    ''' �˰t�h�Ʒs�W
    ''' </summary>
    Private Sub RetrocedeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetrocedeAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As frmProductionRetrocede
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionRetrocede Then
                fr.Activate()
                Exit Sub
            End If
        Next

        fr = New frmProductionRetrocede
        fr.EditItem = "ADD"
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' �˰t�h�ƭק�
    ''' </summary>
    Private Sub RetrocedeEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetrocedeEdit.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("R_NO").ToString
        Dim pri As List(Of ProductionRetrocedeInfo)
        pri = prc.ProductionRetrocede_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pri(0).R_Check = True Then
            MsgBox("����w�Q�f��,�����\�ק�!")
            Exit Sub
        Else
            '-----------------------------------------------------------------------------------
            '�p�G���h�f��s�b���e�f�O�����ܡA�]�����\�ק�   2012/8/23
            Dim ptc As New LFERP.Library.ProductionReturn.ProductionReturnControl
            Dim pti As List(Of LFERP.Library.ProductionReturn.ProductionReturnInfo)
            pti = ptc.ProductionReturn_GetList(Nothing, strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If pti.Count > 0 Then
                MsgBox("��e�h�f�渹�s�b���e�f�O���A�����\�ק�I")
                Exit Sub
            End If
            '-----------------------------------------------------------------------------------
            Edit = True
            Dim fr As frmProductionRetrocede
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionRetrocede Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            fr = New frmProductionRetrocede
            fr.EditItem = "ADD"
            fr.EditValue = strA
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
    End Sub
    ''' <summary>
    ''' �Ͳ��ɰh�ƧR��
    ''' </summary>
    Private Sub RetrocedeDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetrocedeDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("R_NO").ToString
        Dim pri As List(Of ProductionRetrocedeInfo)
        pri = prc.ProductionRetrocede_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pri(0).R_Check = True Then
            MsgBox("����w�Q�f��,�����\�R��!")
            Exit Sub
        Else
            If MsgBox("�T�w�n�R���s����" & strA & "��ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then
                If prc.ProductionRetrocede_Delete(Nothing, strA) = True Then
                Else
                    MsgBox("�R������,���ˬd��]!")
                End If

                Grid1.DataSource = prc.ProductionRetrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            End If
        End If
    End Sub
    ''' <summary>
    ''' �Ͳ��ɰh�Ƭd��
    ''' </summary>
    Private Sub RetrocedePreView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetrocedePreView.Click
        On Error Resume Next

        Dim fr As frmProductionRetrocede
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionRetrocede Then
                fr.Activate()
                Exit Sub
            End If
        Next

        fr = New frmProductionRetrocede
        fr.EditItem = "PreView"
        fr.EditValue = GridView1.GetFocusedRowCellValue("R_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' �Ͳ��ɰh�Ƭd��
    ''' </summary>
    Private Sub RetrocedeQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetrocedeQuery.Click
        Dim prc As New ProductionRetrocedeControl
        Dim prc1 As New ProductionSelectControl

        Dim fr As New ProductionRetrocedeSelect
        tempValue = "�˰t�h�f��"    '�d������
        '�P�_��e�ާ@���Ҧb����
        '�Ĥ@�˰t��---�Ĥ@�˰t��
        '�ĤG�˰t��---�ĤG�˰t��
        '�ĤT�˰t��---�ĤT�˰t��...
        tempValue2 = ""   '�o�X�ܮw�H��---��e�ϥΤH�����w�ܮw�H��

        fr.ShowDialog()

        Select Case tempValue
            Case "1"  '�T�w�Ҷ��d��
                Grid1.DataSource = prc.ProductionRetrocede_GetList(tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "2"   '�۩w�q�h����ܬd��
                Grid1.DataSource = prc1.ProductionSelect_Retrocede_Getlist("�˰t�h�f��", tempValue2)
        End Select

        tempValue = ""
        tempValue2 = ""
    End Sub
    ''' <summary>
    ''' �Ͳ��ɰh�ƨ�s
    ''' </summary>
    Private Sub RetrocedeRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetrocedeRef.Click

        Dim InWare As String = FunWareSelect("880807")
        Dim OutWare As String = FunWareSelect("880806")

        Grid1.DataSource = prc.ProductionRetrocede_GetList1(Nothing, Nothing, Nothing, OutWare, InWare, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub
    ''' <summary>
    '''  '�C�L�������h�f/�ɰh�f���(�����������̾�)--�]�p����
    ''' </summary>
    Private Sub RetrocedePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetrocedePrint.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim ds As New DataSet
        Dim ltc As New CollectionToDataSet

        Dim prc As New ProductionRetrocedeControl
        ds.Tables.Clear()

        Dim strA, strB As String

        strA = GridView1.GetFocusedRowCellValue("R_NO").ToString
        strB = GridView1.GetFocusedRowCellValue("R_Type").ToString

        ltc.CollToDataSet(ds, "ProductionRetrocede", prc.ProductionRetrocede_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))

        PreviewRPT1(ds, "rptProductionRetrocede", "�Ͳ�" & strB & "��", InUser, "", True, True)

        ltc = Nothing
        'Me.Close()
    End Sub
    ''' <summary>
    ''' �Ͳ��ɰh�Ƽf��
    ''' </summary>
    Private Sub RetrocedeCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetrocedeCheck.Click
        On Error Resume Next

        '-----------------------------------------------------------------------------------
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("R_NO").ToString
        '�p�G���h�f��s�b���e�f�O�����ܡA�]�����\�ק�   2012/8/23
        Dim ptc As New LFERP.Library.ProductionReturn.ProductionReturnControl
        Dim pti As List(Of LFERP.Library.ProductionReturn.ProductionReturnInfo)
        pti = ptc.ProductionReturn_GetList(Nothing, strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pti.Count > 0 Then
            MsgBox("��e�h�f�渹�s�b���e�f�O���A�����\�f�־ާ@�I")
            Exit Sub
        End If
        '-----------------------------------------------------------------------------------

        Dim fr As frmProductionRetrocede
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionRetrocede Then
                fr.Activate()
                Exit Sub
            End If
        Next

        fr = New frmProductionRetrocede
        fr.EditItem = "Check"
        fr.EditValue = GridView1.GetFocusedRowCellValue("R_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' �l��a�X�d�߸��
    ''' </summary>
    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If GridView1.RowCount = 0 Then Exit Sub
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("R_NO").ToString
        Dim ptc As New LFERP.Library.ProductionReturn.ProductionReturnControl
        GridControl1.DataSource = ptc.ProductionReturn_GetList(Nothing, strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub RetrocedeTotalPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RetrocedeTotalPrint.Click
        '  If GridView1.RowCount = 0 Then Exit Sub
        'tempValue = GridView1.GetFocusedRowCellValue("C_NO").ToString
        Dim frm As New frmWarePrint
        frm.EditItem = "ProductionRetrocede"
        frm.ShowDialog()
    End Sub

    Private Sub ToolStripIncheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripIncheck.Click

        '-----------------------------------------------------------------------------------
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("R_NO").ToString
        '�p�G���h�f��s�b���e�f�O�����ܡA�]�����\�ק�   2012/8/23
        Dim ptc As New LFERP.Library.ProductionRetrocede.ProductionRetrocedeControl
        Dim pti As List(Of LFERP.Library.ProductionRetrocede.ProductionRetrocedeInfo)
        pti = ptc.ProductionRetrocede_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pti.Count <= 0 Then
            MsgBox("�O�����s�b�I")
            Exit Sub
        End If

        If pti(0).R_InCheck = True Then
            MsgBox("�w���ƽT�{����A���ƾާ@")
            Exit Sub
        End If

        If pti(0).R_Check = False Then
            MsgBox("���楼�f��,���ব�ƾާ@")
            Exit Sub
        End If

        '-----------------------------------------------------------------------------------
        On Error Resume Next
        Dim fr As frmProductionRetrocede
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionRetrocede Then
                fr.Activate()
                Exit Sub
            End If
        Next

        fr = New frmProductionRetrocede
        fr.EditItem = "InCheck"
        fr.EditValue = GridView1.GetFocusedRowCellValue("R_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
End Class