Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.Product
Imports LFERP.Library.Production.ProuctionWareOutA

Public Class ProductionWareOutMainA
    ''' <summary>
    '''   �s�W�ƥ�  
    ''' </summary>
    Private Sub popWareOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As ProductionWareOutSubA
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductionWareOutSubA Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "�X�f��"
        tempValue3 = ""
        fr = New ProductionWareOutSubA
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    '''   �ק�ƥ�  
    ''' </summary>
    Private Sub popWareOutEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutEdit.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As List(Of ProductionWareOutAInfo)
        Dim pc As New ProductionWareOutAControl
        pi = pc.ProductionWareOutA_GetList(GridView1.GetFocusedRowCellValue("PWO_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pi(0).PWO_Check = True Then
            MsgBox("���X�f��w�Q�f��,�����\�ק�")
            Exit Sub

        Else

            Edit = True
            Dim fr As ProductionWareOutSubA
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is ProductionWareOutSubA Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue2 = "�X�f��"
            tempValue3 = GridView1.GetFocusedRowCellValue("PWO_NO").ToString
            fr = New ProductionWareOutSubA
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()

        End If
    End Sub
    ''' <summary>
    '''   �R���ƥ�  
    ''' </summary>
    Private Sub popWareOutDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As List(Of ProductionWareOutAInfo)
        Dim pc As New ProductionWareOutAControl
        pi = pc.ProductionWareOutA_GetList(GridView1.GetFocusedRowCellValue("PWO_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pi(0).PWO_Check = True Then
            MsgBox("���X�f��w�Q�f��,�����\�ק�")
            Exit Sub
        Else
            If MsgBox("�T�w�n�R���s����" & GridView1.GetFocusedRowCellValue("PWO_NO").ToString & "���X�f��ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then
                If pc.ProductionWareOutA_Delete(GridView1.GetFocusedRowCellValue("PWO_NO").ToString, Nothing) = True Then
                    MsgBox("�R����e�X�f�榨�\!")
                Else
                    MsgBox("�R������,���ˬd��]!")
                End If
                DateFill(XtraTabControl1.SelectedTabPageIndex)
            End If
        End If
    End Sub
    ''' <summary>
    '''   �d�ݨƥ�  
    ''' </summary>
    Private Sub popWareOutView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutView.Click, popWareInView.Click
        On Error Resume Next
        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                If GridView1.RowCount = 0 Then Exit Sub
            Case 1
                If GridView2.RowCount = 0 Then Exit Sub
        End Select

        Dim fr As ProductionWareOutSubA
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductionWareOutSubA Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "PreView"
        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                tempValue3 = GridView1.GetFocusedRowCellValue("PWO_NO").ToString
            Case 1
                tempValue3 = GridView2.GetFocusedRowCellValue("PWO_NO").ToString
        End Select

        fr = New ProductionWareOutSubA
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    '''   ��s�ƥ�  
    ''' </summary>
    Private Sub popWareOutflesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutflesh.Click, popWareInflesh.Click
        DateFill(XtraTabControl1.SelectedTabPageIndex)
    End Sub
    ''' <summary>
    '''   �d�ߨƥ�  
    ''' </summary>
    Private Sub popWareOutSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutSeek.Click, popWareInSeek.Click
        ProductionWareOutSelect.ShowDialog()
        Dim pc As New ProductionWareOutAControl
        If tempValue <> Nothing Then
            Select Case XtraTabControl1.SelectedTabPageIndex
                Case 0
                    Grid.DataSource = pc.ProductionWareOutA_GetList(tempValue4, Nothing, tempValue3, Nothing, tempValue5, Nothing, Nothing, tempValue6, tempValue7, tempValue2, tempValue8, Nothing, Nothing, Nothing)
                Case 1
                    Grid1.DataSource = pc.ProductionWareOutA_GetList(tempValue4, Nothing, tempValue3, Nothing, tempValue5, Nothing, Nothing, tempValue6, tempValue7, tempValue2, tempValue8, Nothing, Nothing, Nothing)
            End Select
        End If
        ProductionWareOutSelect.Dispose()

        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing
        tempValue7 = Nothing
        tempValue8 = Nothing
    End Sub
    ''' <summary>
    '''   �C�L����ƥ�  
    ''' </summary>
    Private Sub popWareOutReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutReport.Click, popWareInReport.Click
        Dim ds As New DataSet
        If GridView1.RowCount = 0 Then Exit Sub
        Dim Str As String

        Str = GridView1.GetFocusedRowCellValue("PWO_NO").ToString

        Dim ltc As New CollectionToDataSet
        Dim pc As New ProductionWareOutAControl
        Dim pi As New ProductionWareOutAInfo

        ds.Tables.Clear()

        ltc.CollToDataSet(ds, "ProductionWareOut", pc.ProductionWareOutA_GetList(Str, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ds.Tables("ProductionWareOut").Columns.Add("PWO_ID", GetType(Integer))
        Dim i As Long
        For i = 0 To ds.Tables("ProductionWareOut").Rows.Count - 1
            ds.Tables("ProductionWareOut").Rows(i)("PWO_ID") = i + 1
        Next

        PreviewRPT(ds, "ProductionWareOut", "���~�X�w��", True, True)

        ltc = Nothing
    End Sub
    ''' <summary>
    '''   �f�֨ƥ�  
    ''' </summary>
    Private Sub popWareOutCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutCheck.Click
        On Error Resume Next
        Dim pi As List(Of ProductionWareOutAInfo)
        Dim pc As New ProductionWareOutAControl
        pi = pc.ProductionWareOutA_GetList(GridView1.GetFocusedRowCellValue("PWO_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pi(0).PWO_InCheck = True Then
            MsgBox("���X�f��w���f�T�{,�����\���f�֪��A")
            Exit Sub
        End If
        '---------------------------
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As ProductionWareOutSubA
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductionWareOutSubA Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "Check"
        tempValue3 = GridView1.GetFocusedRowCellValue("PWO_NO").ToString
        fr = New ProductionWareOutSubA
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    '''   ����Ұʨƥ�  
    ''' </summary>
    Private Sub ProductionWareOutMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        Dim IntCheck As Integer = 0

        WareInVoid(0, "880709", "Out")

        IntCheck = FunWareCheck(0, "880709")
        If IntCheck > 0 Then
            lblCheck.Text = "(" + CStr(IntCheck) + "��)" + "���f��"
        Else
            lblCheck.Text = String.Empty
        End If

        '-------------------------------------------------

        WareInVoid(1, "880710", "In")
        IntCheck = FunWareCheck(1, "880710")
        If IntCheck > 0 Then
            lblCheck.Text = "(" + CStr(IntCheck) + "��)" + "�����f"
        Else
            lblCheck.Text = String.Empty
        End If

        If GridView1.RowCount = 0 Then
            XtraTabControl1.SelectedTabPage = XtraTabPage2
        End If




        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                DateFill(0)
            Case 1
                DateFill(1)
        End Select

        PowerUser()
    End Sub
    ''' <summary>
    '''   '�]�m�v��
    ''' </summary>
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880701")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880702")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880703")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880704")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880707")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareInInCheck.Enabled = True
        End If
    End Sub
    ''' <summary>
    '''   '�T�{���f�v��
    ''' </summary>
    Private Sub popWareInInCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareInInCheck.Click
        On Error Resume Next
        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                If GridView1.RowCount = 0 Then Exit Sub
            Case 1
                If GridView2.RowCount = 0 Then Exit Sub
        End Select

        Dim pi As List(Of ProductionWareOutAInfo)
        Dim pc As New ProductionWareOutAControl

        Dim boolPwo As Boolean
        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                pi = pc.ProductionWareOutA_GetList(GridView1.GetFocusedRowCellValue("PWO_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                boolPwo = pi(0).PWO_Check
            Case 1
                pi = pc.ProductionWareOutA_GetList(GridView2.GetFocusedRowCellValue("PWO_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                boolPwo = pi(0).PWO_Check
        End Select
        If boolPwo = False Then
            MsgBox("���X�f��S�f��,�����\���f")
            Exit Sub
        End If
        '-------------------------------------------
        Dim fr As ProductionWareOutSubA
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductionWareOutSubA Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue2 = "InCheck"
        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                tempValue3 = GridView1.GetFocusedRowCellValue("PWO_NO").ToString
            Case 1
                tempValue3 = GridView2.GetFocusedRowCellValue("PWO_NO").ToString
        End Select

        fr = New ProductionWareOutSubA
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    '''   ���e�I���ƥ�
    ''' </summary>
    Private Sub XtraTabControl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XtraTabControl1.Click
        DateFill(XtraTabControl1.SelectedTabPageIndex)
    End Sub

#Region "��k�{��"
    Private Sub WareInVoid(ByVal FunInt As Integer, ByVal FunWare As String, ByVal StrGrid As String)
        Dim WareSelect As String = FunWareSelect(FunWare)
        Dim pc As New ProductionWareOutAControl
        Select Case FunInt
            Case 0
                If StrGrid = "Out" Then
                    Grid.DataSource = pc.ProductionWareOutA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, WareSelect, Nothing)
                Else
                    Grid1.DataSource = pc.ProductionWareOutA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, WareSelect, Nothing)
                End If
            Case 1
                If StrGrid = "Out" Then
                    Grid.DataSource = pc.ProductionWareOutA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, WareSelect)
                Else
                    Grid1.DataSource = pc.ProductionWareOutA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, WareSelect)
                End If
        End Select
    End Sub

    Function FunWareCheck(ByVal FunInt As Integer, ByVal FunWare As String) As Integer
        Dim WareSelect As String = FunWareSelect(FunWare)
        Dim pc As New ProductionWareOutAControl
        Select Case FunInt
            Case 0
                FunWareCheck = pc.ProductionWareOutA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "false", Nothing, WareSelect, Nothing).Count
            Case 1
                FunWareCheck = pc.ProductionWareOutA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "false", Nothing, WareSelect).Count
        End Select
    End Function

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
    Private Sub DateFill(ByVal indexA As Integer)
        Select Case indexA
            Case 0
                '-------------------------------------------------

                Dim strWareVale As String = "880709"
                WareInVoid(0, strWareVale, "Out")
                Dim IntCheck As Integer = 0
                IntCheck = FunWareCheck(0, strWareVale)
                If IntCheck > 0 And Me.GridView1.RowCount > 0 Then
                    lblCheck.Text = "(" + CStr(IntCheck) + "��)" + "���f��"
                Else
                    lblCheck.Text = String.Empty
                End If

            Case 1
                '-------------------------------------------------

                Dim strWareVale As String = "880710"
                WareInVoid(1, strWareVale, "In")
                Dim IntCheck As Integer = 0
                IntCheck = FunWareCheck(1, strWareVale)
                If IntCheck > 0 And Me.GridView2.RowCount > 0 Then
                    lblCheck.Text = "(" + CStr(IntCheck) + "��)" + "�����f"
                Else
                    lblCheck.Text = String.Empty
                End If

        End Select
    End Sub
#End Region

End Class