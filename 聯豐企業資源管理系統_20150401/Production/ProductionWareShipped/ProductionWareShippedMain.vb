Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.Product
Imports LFERP.Library.Production.ProductionWareShipped

Public Class ProductionWareShippedMain
    Dim strWareValeOut As String = "881009"
    Dim strWareValeIn As String = "881010"
    ''' <summary>
    '''     �s�W�ƥ�
    ''' </summary>
    Private Sub popWareOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As ProductionWareShipped
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductionWareShipped Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New ProductionWareShipped
        fr.EditItem = "Shipped" '�X�f��
        fr.EditValue = ""
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    '''     �ק�ƥ�
    ''' </summary>
    Private Sub popWareOutEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutEdit.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As List(Of ProductionWareShippedInfo)
        Dim pc As New ProductionWareShippedControl
        pi = pc.ProductionWareShipped_GetList(GridView1.GetFocusedRowCellValue("PWS_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pi(0).PWS_Check = True Then
            MsgBox("���X�f��w�Q�f��,�����\�ק�")
            Exit Sub
        Else
            Edit = True
            Dim fr As ProductionWareShipped
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is ProductionWareShipped Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            fr = New ProductionWareShipped
            fr.EditItem = "Shipped"
            fr.EditValue = GridView1.GetFocusedRowCellValue("PWS_NO").ToString
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
    End Sub
    ''' <summary>
    '''     �R���ƥ�
    ''' </summary>
    Private Sub popWareOutDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As List(Of ProductionWareShippedInfo)
        Dim pc As New ProductionWareShippedControl
        pi = pc.ProductionWareShipped_GetList(GridView1.GetFocusedRowCellValue("PWS_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pi(0).PWS_Check = True Then
            MsgBox("���X�f��w�Q�f��,�����\�ק�")
            Exit Sub
        Else
            If MsgBox("�T�w�n�R���s����" & GridView1.GetFocusedRowCellValue("PWS_NO").ToString & "���X�f��ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then
                If pc.ProductionWareShipped_Delete(GridView1.GetFocusedRowCellValue("PWS_NO").ToString, Nothing) = True Then
                    MsgBox("�R����e�X�f�榨�\!")
                Else
                    MsgBox("�R������,���ˬd��]!")
                End If
                DateFill(XtraTabControl1.SelectedTabPageIndex)
            End If
        End If
    End Sub
    ''' <summary>
    '''     �d�ݨƥ�
    ''' </summary>
    Private Sub popWareOutView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutView.Click, popWareInView.Click
        On Error Resume Next
        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                If GridView1.RowCount = 0 Then Exit Sub
            Case 1
                If GridView2.RowCount = 0 Then Exit Sub
        End Select

        Dim fr As ProductionWareShipped
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductionWareShipped Then
                fr.Activate()
                Exit Sub
            End If
        Next

        fr = New ProductionWareShipped
        fr.EditItem = "PreView"
        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                fr.EditValue = GridView1.GetFocusedRowCellValue("PWS_NO").ToString
            Case 1
                fr.EditValue = GridView2.GetFocusedRowCellValue("PWS_NO").ToString
        End Select

        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    '''     ��s�ƥ�
    ''' </summary>
    Private Sub popWareOutflesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutflesh.Click, popWareInflesh.Click
        DateFill(XtraTabControl1.SelectedTabPageIndex)
    End Sub
    ''' <summary>
    '''     �d�ߨƥ�
    ''' </summary>
    Private Sub popWareOutSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutSeek.Click, popWareInSeek.Click
        ProductionWareOutSelect.ShowDialog()
        Dim pc As New ProductionWareShippedControl
        If tempValue <> Nothing Then
            Select Case XtraTabControl1.SelectedTabPageIndex
                Case 0
                    Grid.DataSource = pc.ProductionWareShipped_GetList(tempValue4, Nothing, Nothing, Nothing, tempValue5, Nothing, Nothing, tempValue6, tempValue7, Nothing, tempValue8, Nothing, Nothing, Nothing)
                Case 1
                    Grid1.DataSource = pc.ProductionWareShipped_GetList(tempValue4, Nothing, Nothing, Nothing, tempValue5, Nothing, Nothing, tempValue6, tempValue7, Nothing, tempValue8, Nothing, Nothing, Nothing)
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
    '''     ����ƥ�
    ''' </summary>
    Private Sub popWareOutReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutReport.Click, popWareInReport.Click
        Dim ds As New DataSet
        Dim Str As String
        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                If GridView1.RowCount = 0 Then Exit Sub
                Str = GridView1.GetFocusedRowCellValue("PWS_NO").ToString
            Case 1
                If GridView2.RowCount = 0 Then Exit Sub
                Str = GridView2.GetFocusedRowCellValue("PWS_NO").ToString
        End Select


        Dim ltc As New CollectionToDataSet
        Dim pc As New ProductionWareShippedControl
        Dim pi As New ProductionWareShippedInfo

        ds.Tables.Clear()

        ltc.CollToDataSet(ds, "ProductionWareShipped", pc.ProductionWareShipped_GetList(Str, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ds.Tables("ProductionWareShipped").Columns.Add("PWS_ID", GetType(Integer))
        Dim i As Long
        For i = 0 To ds.Tables("ProductionWareShipped").Rows.Count - 1
            ds.Tables("ProductionWareShipped").Rows(i)("PWS_ID") = i + 1
        Next

        PreviewRPT1(ds, "rptProductionWareShipped", "���~�X�w��", InUser, InUser, True, True)

        ltc = Nothing
    End Sub
    Private Sub popWareInTotalAReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareInTotalAReport.Click, popWareInTotalBReport.Click
        '  If GridView1.RowCount = 0 Then Exit Sub
        'tempValue = GridView1.GetFocusedRowCellValue("C_NO").ToString
        Dim frm As New frmWarePrint
        frm.EditItem = "ProductionWareShipped"
        frm.ShowDialog()
    End Sub
    ''' <summary>
    '''     �f�֨ƥ�
    ''' </summary>
    Private Sub popWareOutCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutCheck.Click
        On Error Resume Next
        Dim pi As List(Of ProductionWareShippedInfo)
        Dim pc As New ProductionWareShippedControl
        pi = pc.ProductionWareShipped_GetList(GridView1.GetFocusedRowCellValue("PWS_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pi(0).PWS_InCheck = True Then
            MsgBox("���X�f��w���f�T�{,�����\���f�֪��A")
            Exit Sub
        End If
        '---------------------------
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As ProductionWareShipped
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductionWareShipped Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New ProductionWareShipped
        fr.EditItem = "Check"
        fr.EditValue = GridView1.GetFocusedRowCellValue("PWS_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    '''     ����Ұʨƥ�
    ''' </summary>
    Private Sub ProductionWareOutMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        Dim IntCheck As Integer = 0

        WareInVoid(0, strWareValeOut, strWareValeIn, "Out")

        IntCheck = FunWareCheck(0, strWareValeOut)
        If IntCheck > 0 Then
            lblCheck.Text = "(" + CStr(IntCheck) + "��)" + "���f��"
        Else
            lblCheck.Text = String.Empty
        End If


        '-------------------------------------------------

        WareInVoid(1, strWareValeOut, strWareValeIn, "In")
        IntCheck = FunWareCheck(1, strWareValeIn)
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
    '''    �]�m�v��
    ''' </summary>
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881001")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881002")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881003")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881004")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareOutCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881007")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popWareInInCheck.Enabled = True
        End If
    End Sub
    ''' <summary>
    '''    �T�{���f
    ''' </summary>
    Private Sub popWareInInCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareInInCheck.Click
        On Error Resume Next
        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                If GridView1.RowCount = 0 Then Exit Sub
            Case 1
                If GridView2.RowCount = 0 Then Exit Sub
        End Select

        Dim pi As List(Of ProductionWareShippedInfo)
        Dim pc As New ProductionWareShippedControl

        Dim boolPwo As Boolean
        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                pi = pc.ProductionWareShipped_GetList(GridView1.GetFocusedRowCellValue("PWS_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                boolPwo = pi(0).PWS_Check
            Case 1
                pi = pc.ProductionWareShipped_GetList(GridView2.GetFocusedRowCellValue("PWS_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                boolPwo = pi(0).PWS_Check
        End Select
        If boolPwo = False Then
            MsgBox("���X�f��S�f��,�����\���f")
            Exit Sub
        End If
        '-------------------------------------------
        Dim fr As ProductionWareShipped
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductionWareShipped Then
                fr.Activate()
                Exit Sub
            End If
        Next

        fr = New ProductionWareShipped
        fr.EditItem = "InCheck"
        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                fr.EditValue = GridView1.GetFocusedRowCellValue("PWS_NO").ToString
            Case 1
                fr.EditValue = GridView2.GetFocusedRowCellValue("PWS_NO").ToString
        End Select
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    '''    ����ƥ�h����
    ''' </summary>
    Private Sub XtraTabControl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XtraTabControl1.Click
        DateFill(XtraTabControl1.SelectedTabPageIndex)
    End Sub

#Region "��k�{��"
    Private Sub WareInVoid(ByVal FunInt As Integer, ByVal FunWareOut As String, ByVal FunWareIn As String, ByVal StrGrid As String)
        Dim StartDate As String
        StartDate = Format(Now, "yyyy/MM") & "/01"

        ''Dim WareSelect As String = FunWareSelect(FunWare)
        ''Dim pc As New ProductionWareShippedControl
        ''Select Case FunInt
        ''    Case 0
        ''        If StrGrid = "Out" Then
        ''            Grid.DataSource = pc.ProductionWareShipped_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, StartDate, Format(Now, "yyyy/MM/dd"), Nothing, Nothing, Nothing, WareSelect, Nothing)
        ''        Else
        ''            Grid1.DataSource = pc.ProductionWareShipped_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, StartDate, Format(Now, "yyyy/MM/dd"), Nothing, Nothing, Nothing, WareSelect, Nothing)
        ''        End If
        ''    Case 1
        ''        If StrGrid = "Out" Then
        ''            Grid.DataSource = pc.ProductionWareShipped_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, StartDate, Format(Now, "yyyy/MM/dd"), Nothing, Nothing, Nothing, Nothing, WareSelect)
        ''        Else
        ''            Grid1.DataSource = pc.ProductionWareShipped_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, StartDate, Format(Now, "yyyy/MM/dd"), Nothing, Nothing, Nothing, Nothing, WareSelect)
        ''        End If
        ''End Select

        Dim WareSelectOut As String = FunWareSelect(FunWareOut)
        Dim WareSelectIn As String = FunWareSelect(FunWareIn)

        Dim pc As New ProductionWareShippedControl
        Grid.DataSource = pc.ProductionWareShipped_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, StartDate, Format(Now, "yyyy/MM/dd"), Nothing, Nothing, Nothing, WareSelectOut, WareSelectIn)
        Grid1.DataSource = pc.ProductionWareShipped_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, StartDate, Format(Now, "yyyy/MM/dd"), Nothing, Nothing, Nothing, WareSelectOut, WareSelectIn)


    End Sub
    Function FunWareCheck(ByVal FunInt As Integer, ByVal FunWare As String) As Integer
        Dim WareSelect As String = FunWareSelect(FunWare)
        Dim pc As New ProductionWareShippedControl
        Select Case FunInt
            Case 0
                FunWareCheck = pc.ProductionWareShipped_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "false", Nothing, WareSelect, Nothing).Count
            Case 1
                FunWareCheck = pc.ProductionWareShipped_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "false", Nothing, WareSelect).Count
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
        'Dim strWareValeOut As String = "881009"
        'Dim strWareValeIn As String = "881010"

        Select Case indexA
            Case 0
                '-------------------------------------------------

                'Dim strWareVale As String = "881009"
                WareInVoid(0, strWareValeOut, strWareValeIn, "Out")
                Dim IntCheck As Integer = 0
                IntCheck = FunWareCheck(0, strWareValeOut)
                If IntCheck > 0 Then
                    lblCheck.Text = "(" + CStr(IntCheck) + "��)" + "���f��"
                Else
                    lblCheck.Text = String.Empty
                End If

            Case 1
                '-------------------------------------------------

                ' Dim strWareVale As String = "881010"
                WareInVoid(1, strWareValeOut, strWareValeIn, "In")
                Dim IntCheck As Integer = 0
                IntCheck = FunWareCheck(1, strWareValeIn)
                If IntCheck > 0 Then
                    lblCheck.Text = "(" + CStr(IntCheck) + "��)" + "�����f"
                Else
                    lblCheck.Text = String.Empty
                End If

        End Select
    End Sub
#End Region


End Class