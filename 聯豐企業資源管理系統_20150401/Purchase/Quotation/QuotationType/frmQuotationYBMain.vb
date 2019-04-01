Imports LFERP.Library.Purchase
Imports LFERP.SystemManager
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core
Imports LFERP.FileManager

Public Class frmQuotationYBMain

    Private Sub frmQuotationYBMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mc As New Quotation.QuotationController

        '***�P�_�Τ�O�֦��S�������v��
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        Dim tesuvalue As String

        tesuvalue = "�_"
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100115")
        If pmwiL.Item(0).PMWS_Value = "�O" Then
            tesuvalue = "�O"
        End If

        'MsgBox(DateAdd(DateInterval.Month, -3, CDate(Format(Now, "yyyy/MM/dd"))))

        '************
        Grid1.DataSource = mc.Quotation_Getlist(Nothing, Nothing, Nothing, Nothing, "�˿�", DateAdd(DateInterval.Month, -3, CDate(Format(Now, "yyyy/MM/dd"))), ">", Nothing, False, False, tesuvalue)
        LoadUserPower()
        Me.Text = Label1.Text
    End Sub
    Sub LoadUserPower()

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010201")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popQuotationNew.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010202")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popQuotationEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010203")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popQuotationDel.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010204")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popQuotationCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010205")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popQuotationAccCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010206")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popQuotationSend.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010207")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popQuotationCopy.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010208")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popQuotationPrint.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010209")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popQuotationPrint2.Enabled = True
        End If


    End Sub

    Private Sub popQuotationNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationNew.Click
        On Error Resume Next


        Edit = False
        Dim fr As frmQuotation
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmQuotation Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "���ƽs�X"
        tempValue3 = "�˿�"
        fr = New frmQuotation
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popQuotationSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationSend.Click
        'On Error Resume Next
        Dim ds As New DataSet
        If GridView2.RowCount = 0 Then Exit Sub
        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim mcSupplier As New LFERP.DataSetting.SuppliersControler
        Dim mcQuotation As New LFERP.Library.Purchase.Quotation.QuotationController
        Dim mcUnit As New LFERP.DataSetting.UnitController
        ds.Tables.Clear()
        ltc.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(Nothing, GridView2.GetFocusedRowCellValue("CO_ID").ToString, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Suppliers", mcSupplier.GetSuppliersList(GridView2.GetFocusedRowCellValue("Q_Supplier").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(ds, "Quotation", mcQuotation.Quotation_Getlist(GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc3.CollToDataSet(ds, "Unit", mcUnit.GetUnitList(Nothing))
        ds.Tables("Quotation").Columns.Add("Q_ID", GetType(Integer))
        Dim i As Integer
        Dim ii As Integer
        Dim exapp As New Excel.Application   '�w�q�@��excel��H
        Dim exbook As Excel.Workbook     '�w�q�@��excel����
        Dim exsheet As Excel.Worksheet   '�w�q�@��excel�u�@��

        i = 0
        ii = 0
        exapp = CreateObject("Excel.Application")   '�ͦ��@��excel��H
        exbook = exapp.Workbooks.Open(Application.StartupPath & "\ModuleFile\Quotation.xls")
        exsheet = exbook.Worksheets(1)


        exsheet.Cells(1, 1) = ds.Tables("Company").Rows(0)("CO_ChsName")
        exsheet.Cells(2, 1) = ds.Tables("Company").Rows(0)("CO_EngName")
        exsheet.Cells(3, 1) = ds.Tables("Company").Rows(0)("CO_ChsAddress")
        exsheet.Cells(4, 1) = "TEL:" & ds.Tables("Company").Rows(0)("CO_ChsTel") & "     FAX:" & ds.Tables("Company").Rows(0)("CO_ChsFax")
        exsheet.Cells(6, 2) = ds.Tables("Suppliers").Rows(0)("S_SupplierName")
        exsheet.Cells(7, 2) = ds.Tables("Suppliers").Rows(0)("S_Address")
        exsheet.Cells(8, 2) = ds.Tables("Suppliers").Rows(0)("S_Associate")
        exsheet.Cells(9, 2) = ds.Tables("Suppliers").Rows(0)("S_Tel")
        exsheet.Cells(10, 2) = ds.Tables("Suppliers").Rows(0)("S_Fax")

        exsheet.Cells(6, 16) = ds.Tables("Quotation").Rows(0)("Q_QuoID")
        exsheet.Cells(7, 16) = ds.Tables("Quotation").Rows(0)("Q_AddDate")
        exsheet.Cells(8, 16) = ds.Tables("Quotation").Rows(0)("Q_ActionName")


        'exsheet.Cells(8, 1) = exsheet.Range("A8").Value.ToString() & "                " & ds.Tables("Quotation").Rows(0)("Q_ActionName")
        'exsheet.Cells(9, 1) = exsheet.Range("A9").Value.ToString() & "            " & ds.Tables("Suppliers").Rows(0)("S_SupplierName")
        'exsheet.Cells(10, 1) = exsheet.Range("A10").Value.ToString() & "    " & ds.Tables("Suppliers").Rows(0)("S_Associate")

        For i = 0 To ds.Tables("Quotation").Rows.Count - 1
            'ds.Tables("Quotation").Rows(i)("Q_ID") = i + 1

            If i = 10 Then Exit For
            ii = i + 14
            exsheet.Cells(ii, 2) = ds.Tables("Quotation").Rows(i)("M_Code")
            exsheet.Cells(ii, 3) = ds.Tables("Quotation").Rows(i)("Q_SupplierNo")

            exsheet.Cells(ii, 4) = ds.Tables("Quotation").Rows(i)("M_Name")
            exsheet.Cells(ii, 5) = ds.Tables("Quotation").Rows(i)("M_Gauge")
            exsheet.Cells(ii, 6) = ds.Tables("Quotation").Rows(i)("Q_Brand")
            exsheet.Cells(ii, 7) = ds.Tables("Quotation").Rows(i)("Q_OrIgine")
            exsheet.Cells(ii, 8) = ds.Tables("Quotation").Rows(i)("M_Unit")

            exsheet.Cells(ii, 9) = ds.Tables("Quotation").Rows(i)("Q_Currency")
            If ds.Tables("Quotation").Rows(i)("Q_Price") = 0 Then
                exsheet.Cells(ii, 10) = ""
            Else
                exsheet.Cells(ii, 10) = ds.Tables("Quotation").Rows(i)("Q_Price")

            End If
            'exsheet.Cells(ii, 10) = ds.Tables("Quotation").Rows(i)("Q_Price")
            exsheet.Cells(ii, 11) = ds.Tables("Quotation").Rows(i)("Q_Qty")
            exsheet.Cells(ii, 12) = ds.Tables("Quotation").Rows(i)("Q_StandardPack")
            exsheet.Cells(ii, 13) = ds.Tables("Quotation").Rows(i)("Q_MOQ")
            exsheet.Cells(ii, 14) = ds.Tables("Quotation").Rows(i)("Q_ProductionCycle")
            exsheet.Cells(ii, 15) = ds.Tables("Quotation").Rows(i)("Q_DeliveredCycle")
            exsheet.Cells(ii, 16) = ds.Tables("Quotation").Rows(i)("Q_Depict")

        Next
        Dim tempName As String
        tempName = Application.StartupPath & "\TempFile\" & GridView2.GetFocusedRowCellValue("Q_QuoID").ToString & ".xls"
        exsheet.SaveAs(tempName)
        exapp.Quit()
        exsheet = Nothing
        exbook = Nothing
        exapp = Nothing


        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing

        Dim Sup As New List(Of LFERP.DataSetting.SuppliersInfo)
        Sup = mcSupplier.GetSuppliersList(GridView2.GetFocusedRowCellValue("Q_Supplier").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Dim strBody As String

        strBody = Sup(0).S_SupplierName & ":" & Chr(13) & " �A�n!" & Chr(13) & "    �o�O�����q���߻���!" & Chr(13) & "�кɧִ_�^�A����!"

        'Dim ReportDoc As New ReportDocument
        'tempName = Application.StartupPath & "\TempFile\" & GridView2.GetFocusedRowCellValue("Q_QuoID").ToString & ".xls"
        'ExportToPDF(ds, "rptQuotation", ReportDoc, tempName)

        SendEmail(" �߻���", Sup(0).S_Email, strBody, tempName)

    End Sub

    Private Sub popQuotationEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationEdit.Click
        On Error Resume Next
        If GridView2.RowCount = 0 Then Exit Sub


        Dim qc As New Quotation.QuotationController
        Dim qi As New Quotation.QuotationInfo
        qi = qc.Quotation_Get(GridView2.GetFocusedRowCellValue("Q_QuoID").ToString)
        ' If qi.Q_CheckAction = Nothing And qi.Q_AccCheckAction = Nothing Then
        If (IsDBNull(qi.Q_AccCheck) = True And IsDBNull(qi.Q_Check) = True) Or (qi.Q_AccCheck = False And qi.Q_Check = False) Then

            Edit = True
            Dim fr As frmQuotation
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmQuotation Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue2 = "���ƽs�X"

            fr = New frmQuotation
            fr.MdiParent = MDIMain
            tempValue = GridView2.GetFocusedRowCellValue("Q_QuoID").ToString

            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("����w�Q�f�֡A�����\�ק�")
            Exit Sub
        End If


    End Sub

    Private Sub popQuotationDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationDel.Click

        If GridView2.RowCount = 0 Then Exit Sub

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

        Dim qc As New Quotation.QuotationController
        Dim qi As New Quotation.QuotationInfo
        qi = qc.Quotation_Get(GridView2.GetFocusedRowCellValue("Q_QuoID").ToString)

        If (IsDBNull(qi.Q_AccCheck) = True And IsDBNull(qi.Q_Check) = True) Or (qi.Q_AccCheck = False And qi.Q_Check = False) Then
            If MsgBox("�T�w�n�R���s����" & GridView2.GetFocusedRowCellValue("Q_QuoID").ToString & "���Ҧ�������ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then

                If qc.Quotation_Delete(Nothing, GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, Nothing) = True Then

                    '****�R������
                    Dim dt As New FileController
                    Dim ldt As New List(Of FilesInfo)
                    Dim ii As Integer
                    ldt = dt.FileBond_GetList("4001", GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, Nothing)
                    If ldt.Count > 0 Then
                        For ii = 0 To ldt.Count - 1
                            dt.File_Delete("4001", GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, ldt(ii).F_No)
                        Next
                    End If
                    '******
                    MsgBox("�R�����\.....")
                End If


                Grid1.DataSource = qc.Quotation_Getlist(Nothing, Nothing, Nothing, Nothing, "�˿�", DateAdd(DateInterval.Month, -3, CDate(Format(Now, "yyyy/MM/dd"))), ">", Nothing, Nothing, Nothing, tesuvalue)
            End If
        Else
            MsgBox("����w�Q�f�֡A�����\�R��")
            Exit Sub
        End If
    End Sub

    Private Sub popQuotationRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationRef.Click
        Dim qc As New Quotation.QuotationController
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
        Grid1.DataSource = qc.Quotation_Getlist(Nothing, Nothing, Nothing, Nothing, "�˿�", DateAdd(DateInterval.Month, -3, CDate(Format(Now, "yyyy/MM/dd"))), ">", Nothing, Nothing, Nothing, tesuvalue)
    End Sub

    Private Sub popQuotationCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationCheck.Click
        On Error Resume Next
        If GridView2.RowCount = 0 Then Exit Sub


        Dim qc As New Quotation.QuotationController
        Dim qi As New Quotation.QuotationInfo

        '�Z�O�U�L���ʳ檺�A�L�צ��S�f�ֳ������\�ާ@�ӳ�����
        Dim mc As New Purchase.PurchaseMainController
        Dim dlist As List(Of Purchase.PurchaseMainInfo)
        dlist = mc.PurchaseMain_Getlist(Nothing, Nothing, GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If dlist.Count <> 0 Then
            MsgBox("����w�U�L���ʳ�A�����\�ާ@")
            Exit Sub
        End If

        qi = qc.Quotation_Get(GridView2.GetFocusedRowCellValue("Q_QuoID").ToString)
        If IsDBNull(qi.Q_AccCheck) = True Or qi.Q_AccCheck = False Then

            Dim fr As frmQuotation
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmQuotation Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue2 = "�����f��"

            fr = New frmQuotation
            fr.MdiParent = MDIMain
            tempValue = GridView2.GetFocusedRowCellValue("Q_QuoID").ToString

            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("����w�Q�|�p���f�֡A�����\�ާ@")
            Exit Sub
        End If

    End Sub

    Private Sub popQuotationAccCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationAccCheck.Click
        On Error Resume Next
        If GridView2.RowCount = 0 Then Exit Sub


        Dim qc As New Quotation.QuotationController
        Dim qi As New List(Of Quotation.QuotationInfo)
        Dim QQcheck As New Boolean
        Dim i As Integer

        '�Z�O�U�L���ʳ檺�A�L�צ��S�f�ֳ������\�ާ@�ӳ�����
        Dim mc As New Purchase.PurchaseMainController
        Dim dlist As List(Of Purchase.PurchaseMainInfo)
        dlist = mc.PurchaseMain_Getlist(Nothing, Nothing, GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If dlist.Count <> 0 Then
            MsgBox("����w�U�L���ʳ�A�����\�ާ@")
            Exit Sub
        End If


        QQcheck = False
        qi = qc.Quotation_Getlist(GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        For i = 0 To qi.Count - 1
            If qi(i).Q_Check = True Then
                QQcheck = True
            End If
        Next

        If QQcheck = True Then
            Dim fr As frmQuotation
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmQuotation Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue2 = "�|�p���f��"

            fr = New frmQuotation
            fr.MdiParent = MDIMain
            tempValue = GridView2.GetFocusedRowCellValue("Q_QuoID").ToString

            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("���楼�g�L�����f�֡A�����\�ާ@")
            Exit Sub
        End If

    End Sub

    Private Sub popQuotationSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationSeek.Click
        Dim qc As New Quotation.QuotationController
        Dim fra As New frmQuotationseek
        fra.ShowDialog()

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

        Select Case tempValue
            Case "1"
                Grid1.DataSource = qc.Quotation_Getlist(tempValue2, Nothing, Nothing, Nothing, "�˿�", Nothing, Nothing, Nothing, Nothing, Nothing, tesuvalue)
            Case "2"
                Grid1.DataSource = qc.Quotation_Getlist(Nothing, tempValue2, Nothing, Nothing, "�˿�", Nothing, Nothing, Nothing, Nothing, Nothing, tesuvalue)
            Case "3"
                Grid1.DataSource = qc.Quotation_Getlist(Nothing, Nothing, Nothing, Nothing, "�˿�", Nothing, Nothing, tempValue2, Nothing, Nothing, tesuvalue)

            Case "4"
                If tempValue2 = "�w�f��" Then
                    Grid1.DataSource = qc.Quotation_Getlist(Nothing, Nothing, Nothing, Nothing, "�˿�", Nothing, Nothing, Nothing, True, Nothing, tesuvalue)
                ElseIf tempValue2 = "���f��" Then
                    Grid1.DataSource = qc.Quotation_Getlist(Nothing, Nothing, Nothing, Nothing, "�˿�", Nothing, Nothing, Nothing, False, Nothing, tesuvalue)
                End If
            Case "5"
                If tempValue2 = "�w�f��" Then
                    Grid1.DataSource = qc.Quotation_Getlist(Nothing, Nothing, Nothing, Nothing, "�˿�", Nothing, Nothing, Nothing, Nothing, True, tesuvalue)
                ElseIf tempValue2 = "���f��" Then
                    Grid1.DataSource = qc.Quotation_Getlist(Nothing, Nothing, Nothing, Nothing, "�˿�", Nothing, Nothing, Nothing, Nothing, False, tesuvalue)
                End If
            Case "6"
                Grid1.DataSource = qc.Quotation_Getlist(Nothing, tempValue2, Nothing, Nothing, "�˿�", Nothing, Nothing, Nothing, Nothing, Nothing, tesuvalue)

            Case "7"
                Grid1.DataSource = qc.Quotation_Getlist(Nothing, Nothing, tempValue2, Nothing, "�˿�", Nothing, Nothing, Nothing, Nothing, Nothing, tesuvalue)

        End Select

        tempValue = ""
        tempValue2 = ""


    End Sub

    Private Sub popQuotationFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationFile.Click

        Dim qc As New Quotation.QuotationController
        Dim qi As New Quotation.QuotationInfo
        qi = qc.Quotation_Get(GridView2.GetFocusedRowCellValue("Q_QuoID").ToString)

        If (IsDBNull(qi.Q_AccCheck) = True And IsDBNull(qi.Q_Check) = True) Or (qi.Q_AccCheck = False And qi.Q_Check = False) Then

            Dim open As Boolean
            Dim update As Boolean
            Dim down As Boolean
            Dim edit As Boolean
            Dim del As Boolean
            Dim detail As Boolean

            Dim pmws As New PermissionModuleWarrantSubController
            Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

            If GridView2.RowCount = 0 Then Exit Sub
            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010110")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value = "�O" Then update = True Else update = False
            End If

            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010111")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value = "�O" Then down = True Else down = False
            End If


            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010112")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value = "�O" Then edit = True Else edit = False
            End If

            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010113")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value = "�O" Then del = True Else del = False
            End If

            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010114")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value = "�O" Then detail = True Else detail = False
            End If

            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010115")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value = "�O" Then open = True Else open = False

            End If
            FileShow("4001", GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, open, update, down, edit, del, detail)
        Else

            MsgBox("����w�Q�f�֡A�����\�ާ@")
            Exit Sub

        End If

    End Sub

    Private Sub popQuotationPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popQuotationPrint.Click

        Dim ds As New DataSet
        If GridView2.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim mcSupplier As New LFERP.DataSetting.SuppliersControler
        Dim mcQuotation As New LFERP.Library.Purchase.Quotation.QuotationController
        Dim mcUnit As New LFERP.DataSetting.UnitController
        Dim mctarriff As New LFERP.DataSetting.TarriffController
        ds.Tables.Clear()
        ltc.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(Nothing, GridView2.GetFocusedRowCellValue("CO_ID").ToString, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Suppliers", mcSupplier.GetSuppliersList(GridView2.GetFocusedRowCellValue("Q_Supplier").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(ds, "Quotation", mcQuotation.Quotation_Getlist(GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc3.CollToDataSet(ds, "Unit", mcUnit.GetUnitList(Nothing))
        ltc4.CollToDataSet(ds, "Tarriff", mctarriff.TarriffGetList(Nothing))
        ds.Tables("Quotation").Columns.Add("Q_ID", GetType(Integer))
        Dim i As Long

        For i = 0 To ds.Tables("Quotation").Rows.Count - 1
            ds.Tables("Quotation").Rows(i)("Q_ID") = i + 1
        Next
        '   MsgBox(GridView2.GetFocusedRowCellValue("Q_QuoID").Value.ToString)
        PreviewRPT(ds, "rptQuotation", " ������--" & GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, True, True)
        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing
    End Sub

    Private Sub popQuotationCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationCopy.Click
        On Error Resume Next
        If GridView2.RowCount = 0 Then Exit Sub

        'Dim qc As New Quotation.QuotationController
        'Dim qi As New Quotation.QuotationInfo
        'qi = qc.Quotation_Get(GridView2.GetFocusedRowCellValue("Q_QuoID").ToString)
        'If qi.Q_CheckAction = Nothing And qi.Q_AccCheckAction = Nothing Then

        Edit = True
        Dim fr As frmQuotation
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmQuotation Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue2 = "�_����"

        fr = New frmQuotation
        fr.MdiParent = MDIMain
        tempValue = GridView2.GetFocusedRowCellValue("Q_QuoID").ToString

        fr.WindowState = FormWindowState.Maximized
        fr.Show()
        'Else
        'MsgBox("����w�Q�f�֡A�����\�_��")
        'Exit Sub
        'End If
    End Sub

    Private Sub QuotationRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuotationRead.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub

        Dim fr As frmQuotation
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmQuotation Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue2 = "�d��"
        fr = New frmQuotation
        fr.MdiParent = MDIMain
        tempValue = GridView2.GetFocusedRowCellValue("Q_QuoID").ToString

        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub

    Private Sub PopMsglook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PopMsglook.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub

        Dim fr As frmMessageSent
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMessageSent Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue = "�s�W"
        tempValue3 = "������" & GridView2.GetFocusedRowCellValue("Q_QuoID").ToString & "�d��"     ' ����W
        tempValue4 = GridView2.GetFocusedRowCellValue("Q_QuoID").ToString                ' ���ID
        tempValue5 = "4001"
        tempValue6 = "�d��"


        fr = New frmMessageSent
        fr.MdiParent = MDIMain

        fr.WindowState = FormWindowState.Maximized
        fr.Show()



    End Sub

    Private Sub PopMsgCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PopMsgCheck.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub


        Dim qc As New Quotation.QuotationController
        Dim qi As New Quotation.QuotationInfo
        qi = qc.Quotation_Get(GridView2.GetFocusedRowCellValue("Q_QuoID").ToString)
        If IsDBNull(qi.Q_AccCheck) = True Or qi.Q_AccCheck = False Then
            Dim fr As frmMessageSent
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmMessageSent Then
                    fr.Activate()
                    Exit Sub
                End If
            Next


            tempValue = "�s�W"
            tempValue3 = "������" & GridView2.GetFocusedRowCellValue("Q_QuoID").ToString & "�����f��"     ' ����W
            tempValue4 = GridView2.GetFocusedRowCellValue("Q_QuoID").ToString                ' ���ID
            tempValue5 = "4001"
            tempValue6 = "�����f��"


            fr = New frmMessageSent
            fr.MdiParent = MDIMain

            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("����w�Q�|�p���f�֡A�����\�ާ@")
            Exit Sub
        End If
    End Sub

    Private Sub PopMsgAccCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PopMsgAccCheck.Click
        On Error Resume Next

        If GridView2.RowCount = 0 Then Exit Sub

        Dim qc As New Quotation.QuotationController
        Dim qi As New List(Of Quotation.QuotationInfo)
        Dim QQcheck As New Boolean
        Dim i As Integer
        QQcheck = False
        qi = qc.Quotation_Getlist(GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        For i = 0 To qi.Count - 1
            If qi(i).Q_Check = True Then
                QQcheck = True
            End If
        Next

        If QQcheck = True Then

            Dim fr As frmMessageSent
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmMessageSent Then
                    fr.Activate()
                    Exit Sub
                End If
            Next

            tempValue = "�s�W"
            tempValue3 = "������" & GridView2.GetFocusedRowCellValue("Q_QuoID").ToString & "�|�p�f��"      ' ����W
            tempValue4 = GridView2.GetFocusedRowCellValue("Q_QuoID").ToString                ' ���ID
            tempValue5 = "4001"
            tempValue6 = "�|�p���f��"


            fr = New frmMessageSent
            fr.MdiParent = MDIMain

            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        Else
            MsgBox("���楼�g�L�����f�֡A�����\�ާ@")
            Exit Sub
        End If

    End Sub

    Private Sub popQuotationPrint2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationPrint2.Click
        Dim ds As New DataSet
        If GridView2.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim mcSupplier As New LFERP.DataSetting.SuppliersControler
        Dim mcQuotation As New LFERP.Library.Purchase.Quotation.QuotationController
        Dim mcUnit As New LFERP.DataSetting.UnitController
        Dim mctarriff As New LFERP.DataSetting.TarriffController
        ds.Tables.Clear()
        ltc.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(Nothing, GridView2.GetFocusedRowCellValue("CO_ID").ToString, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Suppliers", mcSupplier.GetSuppliersList(GridView2.GetFocusedRowCellValue("Q_Supplier").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(ds, "Quotation", mcQuotation.Quotation_Getlist(GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc3.CollToDataSet(ds, "Unit", mcUnit.GetUnitList(Nothing))
        ltc4.CollToDataSet(ds, "Tarriff", mctarriff.TarriffGetList(Nothing))
        ds.Tables("Quotation").Columns.Add("Q_ID", GetType(Integer))
        Dim i As Long

        For i = 0 To ds.Tables("Quotation").Rows.Count - 1
            ds.Tables("Quotation").Rows(i)("Q_ID") = i + 1
        Next
        '   MsgBox(GridView2.GetFocusedRowCellValue("Q_QuoID").Value.ToString)
        PreviewRPT(ds, "rptQuotation2", " ������--" & GridView2.GetFocusedRowCellValue("Q_QuoID").ToString, True, True)
        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing
    End Sub
End Class