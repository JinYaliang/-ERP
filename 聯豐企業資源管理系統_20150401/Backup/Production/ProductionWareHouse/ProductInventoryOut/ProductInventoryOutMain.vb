Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.Product
Imports LFERP.Library.Production.ProductionWareShipped
Imports LFERP.Library.Production.ProductInventoryOut


Public Class ProductInventoryOutMain
    Dim strWareValeOut As String = "881009"
    Dim strWareValeIn As String = "881010"
#Region "�s�W�ƥ�"
    ''' <summary>
    '''     �s�W�ƥ�
    ''' </summary>
    Private Sub popWareOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As ProductInventoryOut
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductInventoryOut Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New ProductInventoryOut
        fr.EditItem = "Shipped" '�X�f��
        fr.EditValue = ""
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "�ק�"
    ''' <summary>
    '''     �ק�ƥ�
    ''' </summary>
    Private Sub popWareOutEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutEdit.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As List(Of ProductInventoryOutInfo)
        Dim pc As New ProductInventoryOutControl
        pi = pc.ProductInventoryOut_GetList(Nothing, GridView1.GetFocusedRowCellValue("PO_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pi(0).PO_Check = True Then
            MsgBox("���X�f��w�Q�f��,�����\�ק�")
            Exit Sub
        Else
            Edit = True
            Dim fr As ProductInventoryOut
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is ProductInventoryOut Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            fr = New ProductInventoryOut
            fr.EditItem = "ShippedUpdate"
            fr.EditValue = GridView1.GetFocusedRowCellValue("PO_NO").ToString
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
    End Sub
#End Region

#Region "�R��"
    ''' <summary>
    '''     �R���ƥ�
    ''' </summary>
    Private Sub popWareOutDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As List(Of ProductInventoryOutInfo)
        Dim pc As New ProductInventoryOutControl
        pi = pc.ProductInventoryOut_GetList(GridView1.GetFocusedRowCellValue("AutoID").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pi(0).PO_Check = True Then
            MsgBox("���X�f��w�Q�f��,�����\�R��")
            Exit Sub
        Else
            If MsgBox("�T�w�n�R�������X�f�O���ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then
                If pc.ProductInventoryOut_Delete(GridView1.GetFocusedRowCellValue("AutoID").ToString) = True Then
                    MsgBox("�R����e�X�f�榨�\!")
                Else
                    MsgBox("�R������,���ˬd��]!")
                End If
                DateFill(XtraTabControl1.SelectedTabPageIndex)
            End If
        End If
    End Sub
#End Region

#Region "�d��"
    ''' <summary>
    '''     �d�ݨƥ�
    ''' </summary>
    Private Sub popWareOutView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutView.Click, popWareInView.Click
        On Error Resume Next

        '-------------------------------------------------------
        '2013-12-5�s�W
        If GridView1.RowCount = 0 Then Exit Sub
        '-------------------------------------------------------
        Dim fr As ProductInventoryOut
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductInventoryOut Then
                fr.Activate()
                Exit Sub
            End If
        Next

        fr = New ProductInventoryOut
        fr.EditItem = "PreView"
        '-----------------------------------------------------------------
        '2013-12-5�s�W
        fr.EditValue = GridView1.GetFocusedRowCellValue("PO_NO").ToString
        '-----------------------------------------------------------------
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "��s"
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
        'ProductionWareOutSelect.ShowDialog()
        'Dim pc As New ProductionWareShippedControl
        'If tempValue <> Nothing Then
        '    Grid.DataSource = pc.ProductionWareShipped_GetList(tempValue4, Nothing, tempValue3, Nothing, tempValue5, Nothing, Nothing, tempValue6, tempValue7, tempValue2, tempValue8, Nothing, Nothing, Nothing)
        'End If
        'ProductionWareOutSelect.Dispose()
        'tempValue = Nothing
        'tempValue2 = Nothing
        'tempValue3 = Nothing
        'tempValue4 = Nothing
        'tempValue5 = Nothing
        'tempValue6 = Nothing
        'tempValue7 = Nothing
        'tempValue8 = Nothing
    End Sub
#End Region

#Region "����"
    ''' <summary>
    '''     ����ƥ�
    ''' </summary>
    Private Sub popWareOutReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutReport.Click, popWareInReport.Click
        'Dim ds As New DataSet
        'Dim Str As String = String.Empty

        'If GridView1.RowCount = 0 Then Exit Sub
        'Str = GridView1.GetFocusedRowCellValue("PO_NO").ToString

        'Dim ltc As New CollectionToDataSet
        'Dim pc As New ProductionWareShippedControl
        'Dim pi As New ProductionWareShippedInfo

        'ds.Tables.Clear()

        'ltc.CollToDataSet(ds, "ProductionWareShipped", pc.ProductionWareShipped_GetList(Str, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        'ds.Tables("ProductionWareShipped").Columns.Add("PWS_ID", GetType(Integer))
        'Dim i As Long
        'For i = 0 To ds.Tables("ProductionWareShipped").Rows.Count - 1
        '    ds.Tables("ProductionWareShipped").Rows(i)("PWS_ID") = i + 1
        'Next

        'PreviewRPT1(ds, "rptProductionWareShipped", "���~�X�w��", InUser, InUser, True, True)

        'ltc = Nothing
    End Sub
#End Region

#Region "�f��"
    ''' <summary>
    '''     �f�֨ƥ�
    ''' </summary>
    Private Sub popWareOutCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutCheck.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub

        Dim pi As List(Of ProductInventoryOutInfo)
        Dim pc As New ProductInventoryOutControl
        pi = pc.ProductInventoryOut_GetList(Nothing, GridView1.GetFocusedRowCellValue("PO_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)


        '-------------------------------------------------------------
        '2013-12-5
        If pi(0).PO_Check = True Then
            MsgBox("���X�f��w�g�f��,�����\���f�֪��A")
            Exit Sub
        End If
        '-------------------------------------------------------------

        '---------------------------
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As ProductInventoryOut
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductInventoryOut Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New ProductInventoryOut
        fr.EditItem = "Check"
        fr.EditValue = GridView1.GetFocusedRowCellValue("PO_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region


#Region "�����l��"
    ''' <summary>
    '''     ����Ұʨƥ�
    ''' </summary>
    Private Sub ProductionWareOutMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        Dim IntCheck As Integer = 0

        WareInVoid(0, strWareValeOut, strWareValeIn, "Out")

        IntCheck = FunWareCheck(0, "881009")
        If IntCheck > 0 Then
            lblCheck.Text = "(" + CStr(IntCheck) + "��)" + "���f��"
        Else
            lblCheck.Text = String.Empty
        End If



        Select Case XtraTabControl1.SelectedTabPageIndex
            Case 0
                DateFill(0)
            Case 1
                DateFill(1)
        End Select

        PowerUser()
    End Sub
#End Region

#Region "�]�m�v��"
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
#End Region

#Region "����ƥ�"
    ''' <summary>
    '''    ����ƥ�h����
    ''' </summary>
    Private Sub XtraTabControl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XtraTabControl1.Click
        DateFill(XtraTabControl1.SelectedTabPageIndex)
    End Sub
#End Region

#Region "��k�{��"
    Private Sub WareInVoid(ByVal FunInt As Integer, ByVal FunWareOut As String, ByVal FunWareIn As String, ByVal StrGrid As String)
        Dim StartDate As String
        StartDate = Format(Now, "yyyy/MM") & "/01"


        Dim WareSelectIn As String = FunWareSelect(FunWareIn)
        Dim WareSelectOut As String = FunWareSelect(FunWareOut)


        '-----------------------------------------------------------------------------------------
        ''�s�W2013-12-5
        Dim pc As New ProductInventoryOutControl
        Grid.DataSource = pc.ProductInventoryOut_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, WareSelectOut, StartDate, Format(Now, "yyyy/MM/dd"), Nothing)

        '-----------------------------------------------------------------------------------------


    End Sub
    Function FunWareCheck(ByVal FunInt As Integer, ByVal FunWare As String) As Integer
        Dim pi As List(Of ProductInventoryOutInfo)
        Dim pc As New ProductInventoryOutControl
        Dim WareSelect As String = FunWareSelect(FunWare)
        pi = pc.ProductInventoryOut_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, WareSelect, Nothing, Nothing, "false")
        FunWareCheck = pi.Count

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

#Region "������`"
    Private Sub popWareInTotalAReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareInTotalAReport.Click, popWareInTotalBReport.Click

        'Dim frm As New frmWarePrint
        'frm.EditItem = "ProductionWareShipped"
        'frm.ShowDialog()
    End Sub
#End Region

#Region "�۰ʨ�s"
    Private Sub ProductInventoryOutMain_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        popWareOutflesh_Click(Nothing, Nothing)
    End Sub
#End Region

End Class