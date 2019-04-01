Imports LFERP.Library.ProductionReturn
Imports LFERP.SystemManager
Imports LFERP.DataSetting
Imports LFERP.Library.ProductionSelect

Public Class frmProductionReturnMain
#Region "�ݩ�"
    Dim prc As New ProductionReturnControl
    Dim uc As New UserPowerControl
#End Region


    ''' <summary>
    '''  ����Ұʸ��J�ƥ�  
    ''' </summary>
    Private Sub frmProductionReturnMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim uci As List(Of UserPowerInfo)
        uci = uc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)
        If uci.Count = 0 Then
            MsgBox("���Τ�W���b�Ͳ��Ҷ���,�Х��K�[!")
            Exit Sub
        Else

        End If
        'Grid1.DataSource = prc.ProductionReturn_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        ReturnRef_Click(Nothing, Nothing)

        PowerUser()
    End Sub

    ''' <summary>
    '''  �]�m�v��  
    ''' </summary>
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880901")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ReturnAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880902")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ReturnEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880903")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ReturnDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880904")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ReturnCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880905")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ReturnPrint.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880908")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripIncheck.Enabled = True
        End If

        '

    End Sub

    ''' <summary>
    '''  �s�W�ɰh�f�ƥ�  
    ''' </summary>
    Private Sub ReturnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As frmProductionReturn
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionReturn Then
                fr.Activate()
                Exit Sub
            End If
        Next

        fr = New frmProductionReturn
        fr.EditItem = "ReturnADD"
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    ''' <summary>
    '''  �ɰh�f�ק�ƥ�  
    ''' </summary>
    Private Sub ReturnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnEdit.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("AR_NO").ToString
        Dim pri As List(Of ProductionReturnInfo)
        pri = prc.ProductionReturn_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pri(0).AR_Check = True Then
            MsgBox("����w�f��,�����\�ק�!")
            Exit Sub
        Else
            Edit = True
            Dim fr As frmProductionReturn
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionReturn Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            fr = New frmProductionReturn
            '' fr.EditItem = pri(0).AR_Type
            fr.EditItem = "ReturnADD"
            fr.EditValue = strA
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
    End Sub

    ''' <summary>
    '''  �s�W�ɧR���ƥ�  
    ''' </summary>
    Private Sub ReturnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("AR_NO").ToString
        Dim pri As List(Of ProductionReturnInfo)
        pri = prc.ProductionReturn_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pri(0).AR_Check = True Then
            MsgBox("����w�f��,�����\�R��!")
            Exit Sub
        Else
            If MsgBox("�T�w�n�R���s����" & strA & "��ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then
                If prc.ProductionReturn_Delete(Nothing, strA) = True Then
                Else
                    MsgBox("�R������,���ˬd��]!")
                End If

                Grid1.DataSource = prc.ProductionReturn_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            End If
        End If
    End Sub

    ''' <summary>
    '''  �s�W�ɬd�ݨƥ�  
    ''' </summary>
    Private Sub ReturnPreView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnPreView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmProductionReturn
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionReturn Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionReturn
        fr.EditItem = "PreView"
        fr.EditValue = GridView1.GetFocusedRowCellValue("AR_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    ''' <summary>
    '''  �f�֨ƥ�  
    ''' </summary>
    Private Sub ReturnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnCheck.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmProductionReturn
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionReturn Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionReturn
        fr.EditItem = "Check"
        fr.EditValue = GridView1.GetFocusedRowCellValue("AR_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    ''' <summary>
    '''  '�d�߾ާ@--
    ''' </summary>
    Private Sub ReturnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnQuery.Click
        Dim fr As New ProductionRetrocedeSelect
        tempValue = "�Ͳ��ɰh�f��"    '�d������
        '�P�_��e�ާ@���Ҧb����
        '�Ĥ@�Ͳ���---�Ĥ@�Ͳ���
        '�ĤG�Ͳ���---�ĤG�Ͳ���
        '�ĤT�Ͳ���---�ĤT�Ͳ���...
        tempValue2 = ""   '�o�X�ܮw�H��---��e�ϥΤH�����w�ܮw�H��

        fr.ShowDialog()

        Dim prc As New ProductionReturnControl
        Dim prc1 As New ProductionSelectControl

        Select Case tempValue

            Case "1"  '�T�w�Ҷ��d��

                Grid1.DataSource = prc.ProductionReturn_GetList(tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            Case "2"   '�۩w�q�h����ܬd��

                Grid1.DataSource = prc1.ProductionSelect_Return_Getlist("�Ͳ��ɰh�f��", tempValue2)

        End Select

        tempValue = ""
        tempValue2 = ""
    End Sub

    ''' <summary>
    '''  '��s�ƥ�ާ@--
    ''' </summary>
    Private Sub ReturnRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnRef.Click
        Dim InWare As String = FunWareSelect("880907")
        Dim OutWare As String = FunWareSelect("880906")

        Grid1.DataSource = prc.ProductionReturn_GetList1(Nothing, Nothing, Nothing, OutWare, InWare, Nothing, Nothing, Nothing, Nothing, Nothing)
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
    '''   '�ɥX�����������渹(���J�̾�) ---����
    ''' </summary>
    Private Sub ReturnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnPrint.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim ds As New DataSet
        Dim ltc As New CollectionToDataSet

        Dim prc As New ProductionReturnControl
        ds.Tables.Clear()

        Dim strA, strB As String

        strA = GridView1.GetFocusedRowCellValue("AR_NO").ToString
        strB = GridView1.GetFocusedRowCellValue("AR_Type").ToString

        ltc.CollToDataSet(ds, "ProductionReturn", prc.ProductionReturn_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))


        PreviewRPT(ds, "rptProductionReturn", "�����Ͳ�" & strB & "��", True, True)

        ltc = Nothing
        'Me.Close()

    End Sub

    Private Sub ToolStripIncheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripIncheck.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("AR_NO").ToString
        Dim pri As List(Of ProductionReturnInfo)
        pri = prc.ProductionReturn_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pri(0).AR_Check = False Then
            MsgBox("���楼�f��,�����\���ƽT�{!")
            Exit Sub
        End If

        If pri(0).AR_InCheck = True Then
            MsgBox("����w���ƽT�{,�����\���ƽT�{!")
            Exit Sub
        End If

        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmProductionReturn
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionReturn Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionReturn
        fr.EditItem = "InCheck"
        fr.EditValue = GridView1.GetFocusedRowCellValue("AR_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub
End Class