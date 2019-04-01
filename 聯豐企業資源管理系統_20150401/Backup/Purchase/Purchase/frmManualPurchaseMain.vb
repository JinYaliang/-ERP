Imports LFERP.FileManager
Imports LFERP.Library.Purchase
Imports LFERP.Library.Purchase.Acceptance
Imports LFERP.Library.Purchase.Retrocede
Imports LFERP.SystemManager
Imports LFERP.Library.Purchase.purselect
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core

Public Class frmManualPurchaseMain

    Private Sub frmManualPurchaseMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mc As New Purchase.PurchaseMainController
        'Grid.AutoGenerateColumns = False

        '***�P�_�Τ�O�֦��S�������v��
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        Dim tesuvalue As String
        tesuvalue = "�_"
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100115")
        If pmwiL.Item(0).PMWS_Value = "�O" Then
            tesuvalue = "�O"
        End If

        '************


        Dim pmwil1 As List(Of PermissionModuleWarrantSubInfo)
        Dim strTemp As String = ""
        pmwil1 = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400817")
        strTemp = pmwil1.Item(0).PMWS_Value
        Select Case strTemp

            Case "�D���ʤH��"
                Grid1.DataSource = Nothing
                PS_Price.Visible = False
                PS_Price.VisibleIndex = -1

            Case "���ʭ�"
                Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, InUserID, "��}���ʳ�", Nothing, Nothing, False, tesuvalue)
            Case "���ʥD��"
                Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", False, Nothing, Nothing, tesuvalue)
            Case "�|�p��"
                Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", True, False, Nothing, tesuvalue)
            Case "�޲z��"
                Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)
        End Select
        LoadUserPower1()
        Label1.Text = "���ʺ޲z--��}���ʳ�"

        Me.Text = Label1.Text

    End Sub
    Sub LoadUserPower1()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400801")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPurchaseNew.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400802")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPurchaseEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400803")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPurchaseDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400804")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPurchaseCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400805")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPurchaseAccCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400806")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPurchaseSend.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400807")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPurchasePrint.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400808")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPurchasePrintEng.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400809")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then PurchaseRead.Enabled = True
        End If
    End Sub
    Private Sub popPurchaseNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPurchaseNew.Click
        On Error Resume Next

        Edit = False
        Dim fr As frmPurchase
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmPurchase Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "���ʳ�"
        tempValue3 = "��}���ʳ�"
        fr = New frmPurchase
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popPurchaseEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPurchaseEdit.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub

        Dim pc As New Purchase.PurchaseMainController
        Dim pi As New Purchase.PurchaseMainInfo
        pi = pc.PurchaseMain_Get(GridView2.GetFocusedRowCellValue("PM_NO").ToString)

        If (IsDBNull(pi.PM_AccountCheck) = True And IsDBNull(pi.PM_Check) = True) Or (pi.PM_AccountCheck = False And pi.PM_Check = False) Then
            If GridView1.RowCount = 0 And GridView3.RowCount = 0 Then
                Edit = True

                Dim fr As frmPurchase
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmPurchase Then
                        fr.Activate()
                        Exit Sub
                    End If
                Next

                tempValue2 = "���ʳ�"
                tempValue3 = "��}���ʳ�"
                tempValue = GridView2.GetFocusedRowCellValue("PM_NO").ToString
                fr = New frmPurchase
                fr.MdiParent = MDIMain
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            Else
                MsgBox("�����ʳ�w���e�f/�h�f�O���A����ק�")
                Exit Sub
            End If
        Else
            MsgBox("����w�Q�f�֡A�����\�ק�")
            Exit Sub
        End If
    End Sub

    Private Sub popPurchaseDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPurchaseDel.Click
        If GridView2.RowCount = 0 Then Exit Sub

        Dim pc As New Purchase.PurchaseMainController
        Dim pi As New Purchase.PurchaseMainInfo

        '***�P�_�Τ�O�֦��S�������v��
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        Dim tesuvalue As String
        tesuvalue = "�_"
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100115")
        If pmwiL.Item(0).PMWS_Value = "�O" Then
            tesuvalue = "�O"
        End If

        '************

        pi = pc.PurchaseMain_Get(GridView2.GetFocusedRowCellValue("PM_NO").ToString)
        'If pi.PM_AccountCheckAction = Nothing And pi.PM_CheckAction = Nothing Then
        If (IsDBNull(pi.PM_AccountCheck) = True And IsDBNull(pi.PM_Check) = True) Or (pi.PM_AccountCheck = False And pi.PM_Check = False) Then

            If GridView4.RowCount = 0 Then
                If MsgBox("�T�w�n�R���s����" & GridView2.GetFocusedRowCellValue("PM_NO").ToString & "���Ҧ����ʳ�ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then

                    If pc.PurchaseMain_Delete(GridView2.GetFocusedRowCellValue("PM_NO").ToString) = True Then
                        If pc.PurchaseSub_Delete(GridView2.GetFocusedRowCellValue("PM_NO").ToString, Nothing) = True Then

                            '****�R������
                            Dim dt As New FileController
                            Dim ldt As New List(Of FilesInfo)
                            Dim ii As Integer
                            ldt = dt.FileBond_GetList("4008", GridView2.GetFocusedRowCellValue("PM_NO").ToString, Nothing)
                            If ldt.Count > 0 Then
                                For ii = 0 To ldt.Count - 1
                                    dt.File_Delete("4008", GridView2.GetFocusedRowCellValue("PM_NO").ToString, ldt(ii).F_No)
                                Next
                            End If
                            '******
                            MsgBox("�R�����\.....")

                        End If
                    End If
                    Grid1.DataSource = pc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, Nothing, False, tesuvalue)
                End If
            Else

                MsgBox("�����ʳ�w���e�f�O���A�Х��T�{�R���e�f��O���I", 60, "����")
                Exit Sub
            End If
        Else
            MsgBox("����w�Q�f�֡A�����\�R��")
            Exit Sub
        End If
    End Sub

    Private Sub PurchaseRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseRead.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub

        Dim fr As frmPurchase
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmPurchase Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue2 = "�d��"
        tempValue3 = "��}���ʳ�"
        tempValue = GridView2.GetFocusedRowCellValue("PM_NO").ToString
        fr = New frmPurchase
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popPurchaseAccCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPurchaseAccCheck.Click
        On Error Resume Next
        If GridView2.RowCount = 0 Then Exit Sub

        Dim pc As New Purchase.PurchaseMainController
        Dim pi As New Purchase.PurchaseMainInfo
        pi = pc.PurchaseMain_Get(GridView2.GetFocusedRowCellValue("PM_NO").ToString)
        If pi.PM_Check = True Then

            Dim fr As frmPurchase
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmPurchase Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            tempValue2 = "�|�p���f��"
            tempValue = GridView2.GetFocusedRowCellValue("PM_NO").ToString

            fr = New frmPurchase
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("���楼�g�L�����f�֡A�����\�ާ@")
            Exit Sub
        End If
    End Sub

    Private Sub popPurchaseCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPurchaseCheck.Click
        On Error Resume Next
        If GridView2.RowCount = 0 Then Exit Sub

        Dim pc As New Purchase.PurchaseMainController
        Dim pi As New Purchase.PurchaseMainInfo
        pi = pc.PurchaseMain_Get(GridView2.GetFocusedRowCellValue("PM_NO").ToString)
        If pi.PM_AccountCheck = False Or IsDBNull(pi.PM_AccountCheck) = True Then

            If GridView4.RowCount = 0 Then

                Dim fr As frmPurchase
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmPurchase Then

                        fr.Activate()
                        Exit Sub
                    End If
                Next
                tempValue2 = "�����f��"
                tempValue3 = "��}���ʳ�"
                fr = New frmPurchase
                fr.MdiParent = MDIMain
                tempValue = GridView2.GetFocusedRowCellValue("PM_NO").ToString

                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            Else
                MsgBox("�����ʳ�w���e�f�O���A�Х��T�{�R���e�f��O���I", 60, "����")
                Exit Sub
            End If


        Else
            MsgBox("����w�Q�|�p���f�֡A�����\�ާ@")
            Exit Sub
        End If
    End Sub

    Private Sub popPurchaseRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPurchaseRef.Click
        Dim mc As New Purchase.PurchaseMainController
        '***�P�_�Τ�O�֦��S�������v��
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        Dim tesuvalue As String
        tesuvalue = "�_"
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100115")
        If pmwiL.Item(0).PMWS_Value = "�O" Then
            tesuvalue = "�O"
        End If

        '************
        
        Dim pmwil1 As List(Of PermissionModuleWarrantSubInfo)
        Dim strTemp As String = ""
        pmwil1 = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400817")
        strTemp = pmwil1.Item(0).PMWS_Value
        Select Case strTemp

            Case "�D���ʤH��"
                Grid1.DataSource = Nothing
                PS_Price.Visible = False
                PS_Price.VisibleIndex = -1

            Case "���ʭ�"
                Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, InUserID, "��}���ʳ�", Nothing, Nothing, False, tesuvalue)
            Case "���ʥD��"
                Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", False, Nothing, Nothing, tesuvalue)
            Case "�|�p��"
                Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", True, False, Nothing, tesuvalue)
            Case "�޲z��"
                Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)
        End Select


    End Sub

    Private Sub popPurchaseSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPurchaseSeek.Click
        'Dim mc As New Purchase.PurchaseMainController
        'Dim frm As New frmPurchaseSelect
        'frm.ShowDialog()

        ''***�P�_�Τ�O�֦��S�������v��
        'Dim pmws As New PermissionModuleWarrantSubController
        'Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        'Dim tesuvalue As String
        'tesuvalue = "�_"
        'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100115")
        'If pmwiL.Item(0).PMWS_Value = "�O" Then
        '    tesuvalue = "�O"
        'End If

        ''************


        'Select Case tempValue

        '    Case "1"
        '        Grid1.DataSource = mc.PurchaseMain_Getlist(tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)
        '    Case "2"
        '        Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)
        '    Case "3"
        '        Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)

        '    Case "4"
        '        If tempValue2 = "�w�f��" Then
        '            Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", True, Nothing, Nothing, tesuvalue)
        '        ElseIf tempValue2 = "���f��" Then
        '            Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", False, Nothing, Nothing, tesuvalue)
        '        End If
        '    Case "5"
        '        If tempValue2 = "�w�f��" Then
        '            Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, True, Nothing, tesuvalue)
        '        ElseIf tempValue2 = "���f��" Then
        '            Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, False, Nothing, tesuvalue)
        '        End If

        '    Case "6"
        '        Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)
        '    Case "7"
        '        Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)
        '    Case "8"
        '        Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)
        '    Case "9"
        '        Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)
        'End Select

        'tempValue = ""
        'tempValue2 = ""

        Dim mc As New Purchase.PurchaseMainController
        tempValue = Me.Text

        Dim frm As New FrmpurSelect
        frm.ShowDialog()

        '***�P�_�Τ�O�֦��S�������v��
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        Dim tesuvalue As String
        tesuvalue = "�_"
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100115")
        If pmwiL.Item(0).PMWS_Value = "�O" Then
            tesuvalue = "�O"
        End If

        '************
        Dim pc As New PurSelectControl

        Select Case tempValue

            Case "1" '���ʳ渹
                Grid1.DataSource = mc.PurchaseMain_Getlist(tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, "����", Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)
            Case "2" '���ƳW��
                Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, "����", Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)
            Case "3" '���ƦW��
                Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, "����", Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)

            Case "4" '���ƽs�X �������� 
                Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, "����", Nothing, Nothing, Nothing, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)

            Case "5" '������
                Grid1.DataSource = mc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "����", Nothing, Nothing, tempValue2, Nothing, "��}���ʳ�", Nothing, Nothing, Nothing, tesuvalue)

            Case "6" '�۩w�q�d��

                Grid1.DataSource = pc.PurchaseMain_Getlist("���ʺ޲z-��}���ʳ�", tempValue2)

        End Select


        tempValue = ""
        tempValue2 = ""

    End Sub

    Private Sub popPurchaseSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPurchaseSend.Click
        Dim ds As New DataSet
        If GridView2.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim mcSupplier As New LFERP.DataSetting.SuppliersControler
        Dim mcPurchase As New LFERP.Library.Purchase.Purchase.PurchaseMainController
        Dim mcsysuser As New LFERP.SystemManager.SystemUser.SystemUserController
        ds.Tables.Clear()

        ltc.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(Nothing, GridView2.GetFocusedRowCellValue("CO_ID").ToString, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Suppliers", mcSupplier.GetSuppliersList(GridView2.GetFocusedRowCellValue("S_Supplier").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(ds, "PurchaseMain", mcPurchase.PurchaseMain_Getlist(GridView2.GetFocusedRowCellValue("PM_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc3.CollToDataSet(ds, "SystemUser", mcsysuser.SystemUser_GetList(Nothing, Nothing, Nothing))
        ds.Tables("PurchaseMain").Columns.Add("P_ID", GetType(Integer))
        Dim i As Long

        For i = 0 To ds.Tables("PurchaseMain").Rows.Count - 1
            ds.Tables("PurchaseMain").Rows(i)("P_ID") = i + 1
        Next
        '   MsgBox(Grid.CurrentRow.Cells("Q_QuoID").Value.ToString)
        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing

        Dim Sup As New List(Of LFERP.DataSetting.SuppliersInfo)
        Sup = mcSupplier.GetSuppliersList(GridView2.GetFocusedRowCellValue("S_Supplier").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Dim strBody As String
        Dim tempName As String

        strBody = Sup(0).S_SupplierName & ":" & Chr(13) & " �A�n!" & Chr(13) & "    �o�O�����q�����ʳ�!" & Chr(13) & "�кɧִ_�^�A����!"

        Dim ReportDoc As New ReportDocument
        tempName = Application.StartupPath & "\TempFile\" & GridView2.GetFocusedRowCellValue("PM_NO").ToString & ".pdf"
        ExportToPDF(ds, "rptPurchaseChs", ReportDoc, tempName)

        SendEmail(" ���ʳ�", Sup(0).S_Email, strBody, tempName)
    End Sub

    Private Sub popPurchasePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPurchasePrint.Click
        Dim ds As New DataSet
        If GridView2.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim ltc5 As New CollectionToDataSet
        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim mcSupplier As New LFERP.DataSetting.SuppliersControler
        Dim mcPurchase As New LFERP.Library.Purchase.Purchase.PurchaseMainController
        Dim mcsysuser As New LFERP.SystemManager.SystemUser.SystemUserController
        Dim mctarriff As New LFERP.DataSetting.TarriffController
        Dim mcunit As New LFERP.DataSetting.UnitController

        ds.Tables.Clear()
        ltc.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(Nothing, GridView2.GetFocusedRowCellValue("CO_ID").ToString, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Suppliers", mcSupplier.GetSuppliersList(GridView2.GetFocusedRowCellValue("S_Supplier").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(ds, "PurchaseMain", mcPurchase.PurchaseMain_Getlist(GridView2.GetFocusedRowCellValue("PM_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc3.CollToDataSet(ds, "SystemUser", mcsysuser.SystemUser_GetList(Nothing, Nothing, Nothing))
        ltc4.CollToDataSet(ds, "Tarriff", mctarriff.TarriffGetList(Nothing))
        ltc5.CollToDataSet(ds, "Unit", mcunit.GetUnitList(Nothing))
        ds.Tables("PurchaseMain").Columns.Add("P_ID", GetType(Integer))
        Dim i As Long

        For i = 0 To ds.Tables("PurchaseMain").Rows.Count - 1
            ds.Tables("PurchaseMain").Rows(i)("P_ID") = i + 1
        Next
        '   MsgBox(Grid.CurrentRow.Cells("Q_QuoID").Value.ToString)
        PreviewRPT(ds, "rptPurchaseChs", " ���ʳ�--" & GridView2.GetFocusedRowCellValue("PM_NO").ToString, True, True)
        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing
        ltc4 = Nothing
    End Sub

    Private Sub popPurchasePrintEng_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPurchasePrintEng.Click
        Dim ds As New DataSet
        If GridView2.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim mcSupplier As New LFERP.DataSetting.SuppliersControler
        Dim mcPurchase As New LFERP.Library.Purchase.Purchase.PurchaseMainController
        Dim mcsysuser As New LFERP.SystemManager.SystemUser.SystemUserController
        Dim mctarriff As New LFERP.DataSetting.TarriffController
        ds.Tables.Clear()
        ltc.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(Nothing, GridView2.GetFocusedRowCellValue("CO_ID").ToString, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Suppliers", mcSupplier.GetSuppliersList(GridView2.GetFocusedRowCellValue("S_Supplier").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(ds, "PurchaseMain", mcPurchase.PurchaseMain_Getlist(GridView2.GetFocusedRowCellValue("PM_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc3.CollToDataSet(ds, "SystemUser", mcsysuser.SystemUser_GetList(Nothing, Nothing, Nothing))
        ltc4.CollToDataSet(ds, "Tarriff", mctarriff.TarriffGetList(Nothing))

        ds.Tables("PurchaseMain").Columns.Add("P_ID", GetType(Integer))
        Dim i As Long

        For i = 0 To ds.Tables("PurchaseMain").Rows.Count - 1
            ds.Tables("PurchaseMain").Rows(i)("P_ID") = i + 1
        Next
        '   MsgBox(GridView2.GetFocusedRowCellValue("Q_QuoID").Value.ToString)
        PreviewRPT(ds, "rptPurchaseEng", " ���ʳ�--" & GridView2.GetFocusedRowCellValue("PM_NO").ToString, True, True)
        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing
        ltc4 = Nothing
    End Sub

    Private Sub popPurchaseFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPurchaseFile.Click
        If GridView2.RowCount = 0 Then Exit Sub

        Dim pc As New Purchase.PurchaseMainController
        Dim pi As New Purchase.PurchaseMainInfo
        pi = pc.PurchaseMain_Get(GridView2.GetFocusedRowCellValue("PM_NO").ToString)

        If pi.PM_Check = True Then
            MsgBox("�����ʳ�w�Q�f��,�����\�ާ@!")
            Exit Sub
        Else
            Dim open As Boolean
            Dim update As Boolean
            Dim down As Boolean
            Dim edit As Boolean
            Dim del As Boolean
            Dim detail As Boolean

            Dim pmws As New PermissionModuleWarrantSubController
            Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

            If GridView2.RowCount = 0 Then Exit Sub
            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400810")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value = "�O" Then update = True Else update = False
            End If

            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400811")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value = "�O" Then down = True Else down = False
            End If

            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400812")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value = "�O" Then edit = True Else edit = False
            End If

            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400813")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value = "�O" Then del = True Else del = False
            End If

            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400814")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value = "�O" Then detail = True Else detail = False
            End If

            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400815")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value = "�O" Then open = True Else open = False
            End If

            FileShow("4008", GridView2.GetFocusedRowCellValue("PM_NO").ToString, open, update, down, edit, del, detail)
        End If

    End Sub

   
    Private Sub PopMsgLookPur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PopMsgLookPur.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub

        Dim fr As frmMessageSent
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMessageSent Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue = "�s�W"                                                              '�Z�O��L�Ҷ��եΫH���������e���ѼƳ����s�W�A
        tempValue3 = "���ʳ�" & GridView2.GetFocusedRowCellValue("PM_NO").ToString & "�d��"      ' ����W
        tempValue4 = GridView2.GetFocusedRowCellValue("PM_NO").ToString               ' ���ID
        tempValue5 = "4008"                                                         '   �Ҷ�ID
        tempValue6 = "�d��"                                                         '�o�e����

        fr = New frmMessageSent
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub PopMsgCheckPur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PopMsgCheckPur.Click
        On Error Resume Next
        If GridView2.RowCount = 0 Then Exit Sub

        Dim pc As New Purchase.PurchaseMainController
        Dim pi As New Purchase.PurchaseMainInfo
        pi = pc.PurchaseMain_Get(GridView2.GetFocusedRowCellValue("PM_NO").ToString)
        If pi.PM_AccountCheck = False Or IsDBNull(pi.PM_AccountCheck) = True Then

            Dim fr As frmMessageSent
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmMessageSent Then
                    fr.Activate()
                    Exit Sub
                End If
            Next


            tempValue = "�s�W"                                                              '�Z�O��L�Ҷ��եΫH���������e���ѼƳ����s�W�A
            tempValue3 = "���ʳ�" & GridView2.GetFocusedRowCellValue("PM_NO").ToString & "�����f��"     ' ����W
            tempValue4 = GridView2.GetFocusedRowCellValue("PM_NO").ToString               ' ���ID
            tempValue5 = "4008"                                                         '   �Ҷ�ID
            tempValue6 = "�����f��"                                                         '�o�e����


            fr = New frmMessageSent
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("����w�Q�|�p���f�֡A�����\�ާ@")
            Exit Sub
        End If
    End Sub

    Private Sub PopMsgAccCheckPur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PopMsgAccCheckPur.Click
        On Error Resume Next
        If GridView2.RowCount = 0 Then Exit Sub

        Dim pc As New Purchase.PurchaseMainController
        Dim pi As New Purchase.PurchaseMainInfo
        pi = pc.PurchaseMain_Get(GridView2.GetFocusedRowCellValue("PM_NO").ToString)
        If pi.PM_Check = True Then

            Dim fr As frmMessageSent
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmMessageSent Then
                    fr.Activate()
                    Exit Sub
                End If
            Next


            tempValue = "�s�W"                                                              '�Z�O��L�Ҷ��եΫH���������e���ѼƳ����s�W�A
            tempValue3 = "���ʳ�" & GridView2.GetFocusedRowCellValue("PM_NO").ToString & "�|�p�f��"      ' ����W
            tempValue4 = GridView2.GetFocusedRowCellValue("PM_NO").ToString               ' ���ID
            tempValue5 = "4008"                                                         '   �Ҷ�ID
            tempValue6 = "�|�p���f��"                                                         '�o�e����




            fr = New frmMessageSent
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("���楼�g�L�����f�֡A�����\�ާ@")
            Exit Sub
        End If
    End Sub
    Private Sub Grid1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Grid1.MouseUp
        If GridView2.RowCount = 0 Then Exit Sub

        Dim strA, strB As String
        strA = GridView2.GetFocusedRowCellValue("PM_NO").ToString()
        strB = GridView2.GetFocusedRowCellValue("M_Code").ToString()
        Dim ac As New AcceptanceController
        GridControl3.DataSource = ac.Acceptance_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strB, Nothing, Nothing, Nothing, Nothing, Nothing)

        Dim rc As New RetrocedeController
        '  GridControl2.DataSource = rc.Retrocede_GetList(strA, Nothing, Nothing, Nothing, True, Nothing, strB, Nothing, Nothing, Nothing)
        GridControl4.DataSource = rc.Retrocede_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, strB, Nothing, Nothing, Nothing)

        Dim cc As New Change.ChangeControl
        GridControl5.DataSource = cc.Change_GetList(Nothing, strA, strB, Nothing)
    End Sub

    Private Sub popPurchaseExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPurchaseExport.Click
        'On Error Resume Next
        'If GridView2.RowCount = 0 Then Exit Sub
        'If MsgBox("�A�T�w�n�ɥX��e�Ҧ����Ʊ��ʪ��H����?", MsgBoxStyle.YesNo, "����") = MsgBoxResult.No Then Exit Sub

        'Dim exapp As New Excel.Application
        'Dim exbook As Excel.Workbook
        'Dim exsheet As Excel.Worksheet

        'Dim i As Integer = 0, ii As Integer = 0

        'exapp = CreateObject("Excel.Application")
        'exbook = exapp.Workbooks.Add
        'exsheet = exapp.Worksheets(1)

        'exsheet.Cells(1, 1) = "���ʳ渹"
        'exsheet.Cells(1, 2) = "�����ӦW"
        'exsheet.Cells(1, 3) = "���ƽs�X"
        'exsheet.Cells(1, 4) = "�������O"
        'exsheet.Cells(1, 5) = "�W��"
        'exsheet.Cells(1, 6) = "�W��"
        'exsheet.Cells(1, 7) = "�妸"
        'exsheet.Cells(1, 8) = "�����ӽs��"
        'exsheet.Cells(1, 9) = "���"
        'exsheet.Cells(1, 10) = "���O"
        'exsheet.Cells(1, 11) = "�ƶq"
        'exsheet.Cells(1, 12) = "���"
        'exsheet.Cells(1, 13) = "�����"
        'exsheet.Cells(1, 14) = "���ʤ��"
        'exsheet.Cells(1, 15) = "�����f��"
        'exsheet.Cells(1, 16) = "�|�p�f��"
        'exsheet.Cells(1, 17) = "�|�p�f������"
        'exsheet.Cells(1, 18) = "�|�p���f�֭�"
        'exsheet.Cells(1, 19) = "�ާ@��"

        'For i = 0 To GridView2.RowCount - 1
        '    ii = i + 2
        '    exsheet.Cells(ii, 1) = GridView2.GetRowCellValue(i, "PM_NO")
        '    exsheet.Cells(ii, 2) = GridView2.GetRowCellValue(i, "S_SupplierName")
        '    exsheet.Cells(ii, 3) = "$" & GridView2.GetRowCellValue(i, "M_Code") & "$"
        '    exsheet.Cells(ii, 4) = GridView2.GetRowCellValue(i, "Type3Name")
        '    exsheet.Cells(ii, 5) = GridView2.GetRowCellValue(i, "M_Name")
        '    exsheet.Cells(ii, 6) = GridView2.GetRowCellValue(i, "M_Gauge")
        '    exsheet.Cells(ii, 7) = GridView2.GetRowCellValue(i, "OS_BatchID")
        '    exsheet.Cells(ii, 8) = GridView2.GetRowCellValue(i, "S_SupplierNo")
        '    exsheet.Cells(ii, 9) = GridView2.GetRowCellValue(i, "PS_Price")
        '    exsheet.Cells(ii, 10) = GridView2.GetRowCellValue(i, "C_ID")
        '    exsheet.Cells(ii, 11) = GridView2.GetRowCellValue(i, "PS_QTY")
        '    exsheet.Cells(ii, 12) = GridView2.GetRowCellValue(i, "M_Unit")
        '    exsheet.Cells(ii, 13) = GridView2.GetRowCellValue(i, "PS_NoSendQty")
        '    exsheet.Cells(ii, 14) = GridView2.GetRowCellValue(i, "PM_PurchaseDate")
        '    exsheet.Cells(ii, 15) = GridView2.GetRowCellValue(i, "PM_Check")
        '    exsheet.Cells(ii, 16) = GridView2.GetRowCellValue(i, "PM_AccountCheck")
        '    exsheet.Cells(ii, 17) = GridView2.GetRowCellValue(i, "PM_AccountCheckType")
        '    exsheet.Cells(ii, 18) = GridView2.GetRowCellValue(i, "PM_AccCheckActionName")
        '    exsheet.Cells(ii, 19) = GridView2.GetRowCellValue(i, "PM_ActionName")
        'Next
        'exapp.Visible = True


        '2012/9/25  ---------------------------------------------------------

        If GridView2.RowCount = 0 Then Exit Sub
        ' ''ExportGridToXls()

        ''Dim saveFileDialog As New SaveFileDialog()

        ''saveFileDialog.Title = "�ɥXExcel"

        ''saveFileDialog.Filter = "Excel2003���(*.xls)|*.xls"
        ' ''|Excel2007�ΥH�W���(*.xlsx)|*.xlsx  '��e����2007 �H�ΥH�W�������~�I

        ''Dim dialogResult__1 As DialogResult = saveFileDialog.ShowDialog(Me)

        ''If dialogResult__1 = Windows.Forms.DialogResult.OK Then

        ''    'Grid1.ExportToXls(saveFileDialog.FileName)
        ''    GridView2.BestFitColumns()

        ''    Grid1.ExportToExcelOld(saveFileDialog.FileName)

        ''    DevExpress.XtraEditors.XtraMessageBox.Show("�O�s���\�I", "����", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ''End If
        '--------------------------------------------------------------------

        Dim i, j As Integer
        Dim exapp As New Excel.Application   '�w�q�@��excel��H
        Dim exbook As Excel.Workbook     '�w�q�@��excel����
        Dim exsheet As Excel.Worksheet   '�w�q�@��excel�u�@��

        exapp = CreateObject("Excel.Application")   '�ͦ��@��excel��H
        exbook = exapp.Workbooks.Open(Application.StartupPath & "\ModuleFile\Module.xls")
        exsheet = exbook.Worksheets(1)

        exsheet.Cells(1, 1) = "���ʳ渹"
        exsheet.Cells(1, 2) = "���ʤ��"
        exsheet.Cells(1, 3) = "�����ӽs��"
        exsheet.Cells(1, 4) = "�����ӦW��"
        exsheet.Cells(1, 5) = "���ƽs�X"

        exsheet.Cells(1, 6) = "�j��"
        exsheet.Cells(1, 7) = "����"


        exsheet.Cells(1, 8) = "�p��"
        exsheet.Cells(1, 9) = "�W��"
        exsheet.Cells(1, 10) = "�W��"
        exsheet.Cells(1, 11) = "���"
        exsheet.Cells(1, 12) = "�ƶq"

        exsheet.Cells(1, 13) = "���"
        exsheet.Cells(1, 14) = "���B"

        exsheet.Cells(1, 15) = "���O"
        exsheet.Cells(1, 16) = "�I�ڤ覡"
        exsheet.Cells(1, 17) = "�ާ@��"

        exsheet.Cells(1, 18) = "�����"
        exsheet.Cells(1, 19) = "�����f��"
        exsheet.Cells(1, 20) = "�|�p�f��"
        exsheet.Cells(1, 21) = "�|�p�f������"
        exsheet.Cells(1, 22) = "�|�p���f�֭�"

        exsheet.Cells(1, 23) = "�妸"

        exsheet.Cells(1, 24) = "���B(���)"
        exsheet.Cells(1, 25) = "�ƪ`"
        'PM_Remark

        Dim ii As Integer

        For i = 0 To GridView2.RowCount - 1
            ii = i + 2
            exsheet.Cells(ii, 1) = GridView2.GetRowCellValue(i, "PM_NO")
            exsheet.Cells(ii, 2) = GridView2.GetRowCellValue(i, "PM_PurchaseDate")
            exsheet.Cells(ii, 3) = GridView2.GetRowCellValue(i, "S_Supplier") 'S_SupplierNo
            exsheet.Cells(ii, 4) = GridView2.GetRowCellValue(i, "S_SupplierName")
            exsheet.Cells(ii, 5) = GridView2.GetRowCellValue(i, "M_Code")

            exsheet.Cells(ii, 6) = GridView2.GetRowCellValue(i, "Type1Name")
            exsheet.Cells(ii, 7) = GridView2.GetRowCellValue(i, "Type2Name")

            exsheet.Cells(ii, 8) = GridView2.GetRowCellValue(i, "Type3Name")
            exsheet.Cells(ii, 9) = GridView2.GetRowCellValue(i, "M_Name")
            exsheet.Cells(ii, 10) = GridView2.GetRowCellValue(i, "M_Gauge")
            exsheet.Cells(ii, 11) = GridView2.GetRowCellValue(i, "M_Unit")
            exsheet.Cells(ii, 12) = GridView2.GetRowCellValue(i, "PS_QTY")

            exsheet.Cells(ii, 13) = GridView2.GetRowCellValue(i, "PS_Price")
            exsheet.Cells(ii, 14) = Val(GridView2.GetRowCellValue(i, "PS_Price")) * Val(GridView2.GetRowCellValue(i, "PS_QTY"))
            exsheet.Cells(ii, 15) = GridView2.GetRowCellValue(i, "C_ID")
            exsheet.Cells(ii, 16) = GridView2.GetRowCellValue(i, "PM_ToFrom")
            exsheet.Cells(ii, 17) = GridView2.GetRowCellValue(i, "PM_ActionName")

            exsheet.Cells(ii, 18) = GridView2.GetRowCellValue(i, "PS_NoSendQty")
            exsheet.Cells(ii, 19) = GridView2.GetRowCellValue(i, "PM_Check")
            exsheet.Cells(ii, 20) = GridView2.GetRowCellValue(i, "PM_AccountCheck")
            exsheet.Cells(ii, 21) = GridView2.GetRowCellValue(i, "PM_AccountCheckType")
            exsheet.Cells(ii, 22) = GridView2.GetRowCellValue(i, "PM_AccCheckActionName")

            exsheet.Cells(ii, 23) = GridView2.GetRowCellValue(i, "OS_BatchID")

            exsheet.Cells(ii, 24) = Format(Val(GridView2.GetRowCellValue(i, "PS_Price")) * Val(GridView2.GetRowCellValue(i, "PS_QTY")) * Val(GridView2.GetRowCellValue(i, "HKDRate")), "0.0000")
            exsheet.Cells(ii, 25) = GridView2.GetRowCellValue(i, "PM_Remark")

            exsheet.Rows(ii).RowHeight = 18

        Next

        exbook.ActiveSheet.range(exsheet.Cells(1, 1), exsheet.Cells(GridView2.RowCount + 1, 25)).borders(1).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(1, 1), exsheet.Cells(GridView2.RowCount + 1, 25)).borders(2).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(1, 1), exsheet.Cells(GridView2.RowCount + 1, 25)).borders(3).linestyle = 1
        exbook.ActiveSheet.range(exsheet.Cells(1, 1), exsheet.Cells(GridView2.RowCount + 1, 25)).borders(4).linestyle = 1

        Dim tempName As String
        SaveFileDialog1.InitialDirectory = "c:\"
        SaveFileDialog1.Filter = "txt files (*.xls)|*.xls"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tempName = SaveFileDialog1.FileName
        Else
            'tempName = Application.StartupPath & Format(Now, "yyyyMMdd") & ".xls"
            Exit Sub
        End If

        exsheet.SaveAs(tempName)
        exapp.Quit()
        exsheet = Nothing
        exbook = Nothing
        exapp = Nothing

        MsgBox("�ɥX���\! ��" & tempName)



    End Sub
End Class