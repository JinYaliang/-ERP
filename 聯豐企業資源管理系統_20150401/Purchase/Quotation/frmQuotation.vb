Imports LFERP.Library.Purchase.Quotation
Imports LFERP.Library.Material
Imports LFERP.SystemManager.SystemUser

Imports LFERP.FileManager
Imports LFERP.SystemManager

Public Class frmQuotation


    Sub AddRow(ByVal strCode As String)
        If strCode = "" Then
        Else

            'Dim i As Integer
            'For i = 0 To ds.Tables("Quotation").Rows.Count - 1
            '    If strCode = ds.Tables("Quotation").Rows(i)("M_Code") Then
            '        MsgBox("�@�i�椣���\�����_���ƽs�X....")
            '        Exit Sub
            '    End If
            'Next

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)

            If objInfo Is Nothing Then Exit Sub

            If objInfo.M_IsEnabled = False Then  '�P�_��e���ƬO�_�i�� 2012-2-20�A���i�Τ����\�����I
                MsgBox("��e���Ƥ��i�ΡA�����\�����I")
                Exit Sub
            End If

            Dim row As DataRow
            row = ds.Tables("Quotation").NewRow
            'CodeSubData.Tables("CodeSub").NewRow()
            row("AutoID") = Nothing
            row("Q_QuoID") = Nothing
            row("M_Code") = objInfo.M_Code
            row("Q_Qty") = 0
            row("Q_Price") = 0
            row("M_Name") = objInfo.M_Name
            row("M_Unit") = objInfo.M_Unit
            row("M_Gauge") = objInfo.M_Gauge
            row("Q_XH") = ds.Tables("Quotation").Rows.Count + 1 '2012-5-16

            ds.Tables("Quotation").Rows.Add(row)
            AdvBandedGridView1.MoveLast()
        End If
    End Sub


    Private Sub frmQuotation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim mtd As New LFERP.DataSetting.SuppliersControler
        gluSupplier.Properties.DisplayMember = "S_SupplierName"
        gluSupplier.Properties.ValueMember = "S_Supplier"
        gluSupplier.Properties.DataSource = mtd.GetSuppliersList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "True")

        Dim mtg As New LFERP.DataSetting.CompanyControler
        gluCompany.Properties.DisplayMember = "CO_ID"
        gluCompany.Properties.ValueMember = "CO_ID"
        gluCompany.Properties.DataSource = mtg.Company_Getlist(Nothing, Nothing, Nothing, Nothing)

        Dim mtsCurrency As New LFERP.DataSetting.CurrencyControler
        lueCurrency.Properties.DataSource = mtsCurrency.GetCurrencyList(Nothing)

        Dim mtstarriff As New LFERP.DataSetting.TarriffController
        TarriffEdit.Properties.DataSource = mtstarriff.TarriffGetList(Nothing)

        CreateTables()
        txtQuoID.Text = tempValue
        checkLabel.Text = tempValue2
        Label30.Text = tempValue3
        tempValue = ""
        tempValue2 = ""
        tempValue3 = ""
        Select Case checkLabel.Text
            Case "���ƽs�X"
                If Edit = True Then
                    Me.Text = "������ק�"

                    If LoadEdit(txtQuoID.Text) = True Then
                    Else
                        tempValue = ""
                    End If
                    getenable(True, False, False)
                    'gluSupplier.Enabled = False
                    dteDate.Enabled = False
                Else
                    Me.Text = "������s�W"
                    txtQuoID.Text = ""
                    Label8.Text = 0
                    dteDate.Text = Format(Now, "yyyy/MM/dd")
                    getenable(True, False, False)
                End If

                XtraTabControl1.SelectedTabPage = XtraTabPage1
                GridView2.OptionsBehavior.AutoSelectAllInEditor = False
                GridView2.OptionsBehavior.Editable = False
                GridView2.OptionsSelection.EnableAppearanceFocusedCell = False

            Case "�_����"
                Me.Text = "������s�W"
                If LoadEdit(txtQuoID.Text) = True Then
                    txtQuoID.Text = ""
                    cboName.Text = ""
                    gluSupplier.Text = ""

                    Label8.Text = 0
                    dteDate.Text = Format(Now, "yyyy/MM/dd")
                End If
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                getenable(True, False, False)

                GridView2.OptionsBehavior.AutoSelectAllInEditor = False
                GridView2.OptionsBehavior.Editable = False
                GridView2.OptionsSelection.EnableAppearanceFocusedCell = False

            Case "�����f��"
                Me.Text = "�����f��"
                LoadEdit(txtQuoID.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage2

                AdvBandedGridView1.OptionsBehavior.AutoSelectAllInEditor = False
                AdvBandedGridView1.OptionsBehavior.Editable = False
                AdvBandedGridView1.OptionsSelection.EnableAppearanceFocusedCell = False
                getenable(False, True, False)
                popQuotationAdd.Visible = False
                popQuotationDel.Visible = False
                ToolStripSeparator1.Visible = False
                popQuotationCode.Visible = False

            Case "�|�p���f��"
                Me.Text = "�|�p���f��"
                LoadEdit(txtQuoID.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage3
                getenable(False, False, True)

                AdvBandedGridView1.OptionsBehavior.AutoSelectAllInEditor = False
                AdvBandedGridView1.OptionsBehavior.Editable = False
                AdvBandedGridView1.OptionsSelection.EnableAppearanceFocusedCell = False
                GridView2.OptionsBehavior.AutoSelectAllInEditor = False
                GridView2.OptionsBehavior.Editable = False
                GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
                popQuotationAdd.Visible = False
                popQuotationDel.Visible = False
                ToolStripSeparator1.Visible = False
                popQuotationCode.Visible = False

            Case "�d��"
                Me.Text = "�d��"
                LoadEdit(txtQuoID.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                getenable(False, False, False)
                txtRemark.Enabled = True

                'AdvBandedGridView1.OptionsBehavior.AutoSelectAllInEditor = False
                'AdvBandedGridView1.OptionsBehavior.Editable = False
                'AdvBandedGridView1.OptionsSelection.EnableAppearanceFocusedCell = False
                GridView2.OptionsBehavior.AutoSelectAllInEditor = False
                GridView2.OptionsBehavior.Editable = False
                GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
                cmdSave.Enabled = False
                popQuotationAdd.Visible = False
                popQuotationDel.Visible = False
                ToolStripSeparator1.Visible = False
                popQuotationCode.Visible = False

        End Select

        '�[����������
        GridFile.AutoGenerateColumns = False
        GridFile.RowHeadersWidth = 15
        Dim dt As New FileController
        GridFile.DataSource = dt.FileBond_GetList("4001", txtQuoID.EditValue, Nothing)
        GridFile.Refresh()

        '@ 2012/4/13 �K�[ �]�m��ܦ渹�C�e�A�ھڼƾڦ�h�ֳ]�m
        If GridView2.RowCount < 10 Then
            GridView2.IndicatorWidth = 28
        ElseIf GridView2.RowCount < 100 Then
            GridView2.IndicatorWidth = 35
        ElseIf GridView2.RowCount < 1000 Then
            GridView2.IndicatorWidth = 42
        Else
            GridView2.IndicatorWidth = 49
        End If

        Me.AdvBandedGridView1.IndicatorWidth = 30
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Sub CreateTables()
        '�l���Ƽƾ�
        ds.Tables.Clear()
        '�Ыؼƾڪ�
        With ds.Tables.Add("Quotation")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("Q_QuoID", GetType(String))

            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))

            .Columns.Add("Q_SupplierNo", GetType(String))
            .Columns.Add("Q_Type", GetType(String))
            .Columns.Add("M_Price", GetType(Double))
            .Columns.Add("Q_Price", GetType(Double))
            .Columns.Add("Q_Qty", GetType(Double))

            .Columns.Add("Q_RetrDate", GetType(String))
            .Columns.Add("Q_Depict", GetType(String))
            .Columns.Add("PN_NO", GetType(String))
            .Columns.Add("Q_Check", GetType(Boolean))
            .Columns.Add("Q_CheckAction", GetType(String))
            .Columns.Add("Q_CheckRemark", GetType(String))
            .Columns.Add("Q_CheckType", GetType(String))

            .Columns.Add("Q_Brand", GetType(String))
            .Columns.Add("Q_Origine", GetType(String))
            .Columns.Add("Q_StandardPack", GetType(Double))
            .Columns.Add("Q_MOQ", GetType(Double))
            .Columns.Add("Q_ProductionCycle", GetType(String))
            .Columns.Add("Q_DeliveredCycle", GetType(String))

            'Currency 2012-5-16
            .Columns.Add("Q_Currency", GetType(String)) ''�ɤJ�̷s���O
            .Columns.Add("Q_XH", GetType(Integer))

        End With
        '�ЫاR���ƾڪ�
        With ds.Tables.Add("DelData")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("Q_QuoID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
        End With
        '�j�w���
        Grid.DataSource = ds.Tables("Quotation")
        GridControl1.DataSource = ds.Tables("Quotation")

    End Sub

    Private Sub popQuotationAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationAdd.Click
        tempCode = ""
        tempValue6 = "�����޲z"
        If Label30.Text = "�j�f�妸" Or Label30.Text = "�t��妸" Then
            frmBOMSelect.XtraTabPage1.PageVisible = True
            frmBOMSelect.XtraTabPage2.PageVisible = True
            frmBOMSelect.XtraTabPage3.PageVisible = True
        Else
            frmBOMSelect.XtraTabPage1.PageVisible = True
            frmBOMSelect.XtraTabPage2.PageVisible = False
            frmBOMSelect.XtraTabPage3.PageVisible = False
        End If
        frmBOMSelect.ShowDialog()
        '�W�[�O��
        If frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then
            If tempCode = "" Then
                Exit Sub
            Else
                AddRow(tempCode)
            End If
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 1 Then
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempValue7, ",")
            n = Len(Replace(tempValue7, ",", "," & "*")) - Len(tempValue7)
            For i = 0 To n

                Dim j As Integer

                For j = 0 To ds.Tables("Quotation").Rows.Count - 1
                    If arr(i) = ds.Tables("Quotation").Rows(j)("M_Code") Then
                        MsgBox("�@�i�椣���\�����_���ƽs�X....")
                        Exit Sub
                    End If
                Next
                If arr(i) = "" Then
                    Exit Sub
                End If
                Dim mc As New LFERP.Library.Material.MaterialController
                Dim objInfo As New LFERP.Library.Material.MaterialInfo
                objInfo = mc.MaterialCode_Get(arr(i))

                If objInfo Is Nothing Then Exit Sub

                If objInfo.M_IsEnabled = False Then  '�P�_��e���ƬO�_�i�� 2012-2-20�A���i�Τ����\�����I
                    MsgBox("��e���Ƥ��i�ΡA�����\�����I")
                    Exit Sub
                End If

                Dim row As DataRow
                row = ds.Tables("Quotation").NewRow
                row("AutoID") = Nothing
                row("Q_QuoID") = Nothing
                row("M_Code") = objInfo.M_Code
                row("Q_Qty") = 0
                row("Q_Price") = 0
                row("M_Name") = objInfo.M_Name
                row("M_Unit") = objInfo.M_Unit
                row("M_Gauge") = objInfo.M_Gauge
                row("Q_XH") = ds.Tables("Quotation").Rows.Count + 1 '2012-5-16

                ds.Tables("Quotation").Rows.Add(row)
                AdvBandedGridView1.MoveLast()
            Next
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 2 Then
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempValue8, ",")
            n = Len(Replace(tempValue8, ",", "," & "*")) - Len(tempValue8)
            For i = 0 To n

                Dim j As Integer

                MsgBox(arr(i))
                MsgBox(ds.Tables("Quotation").Rows(j)("M_Code"))
                For j = 0 To ds.Tables("Quotation").Rows.Count - 1

                    If arr(i) = ds.Tables("Quotation").Rows(j)("M_Code") Then
                        MsgBox("�@�i�椣���\�����_���ƽs�X....")
                        Exit Sub
                    End If
                Next
                If arr(i) = "" Then
                    Exit Sub
                End If
                Dim mc As New LFERP.Library.Material.MaterialController
                Dim objInfo As New LFERP.Library.Material.MaterialInfo
                objInfo = mc.MaterialCode_Get(arr(i))

                If objInfo Is Nothing Then Exit Sub

                If objInfo.M_IsEnabled = False Then  '�P�_��e���ƬO�_�i�� 2012-2-20�A���i�Τ����\�����I
                    MsgBox("��e���Ƥ��i�ΡA�����\�����I")
                    Exit Sub
                End If

                Dim row As DataRow
                row = ds.Tables("Quotation").NewRow
                row("AutoID") = Nothing
                row("Q_QuoID") = Nothing
                row("M_Code") = objInfo.M_Code
                row("Q_Qty") = 0
                row("Q_Price") = 0
                row("M_Name") = objInfo.M_Name
                row("M_Unit") = objInfo.M_Unit
                row("M_Gauge") = objInfo.M_Gauge
                row("Q_XH") = ds.Tables("Quotation").Rows.Count + 1 '2012-5-16

                ds.Tables("Quotation").Rows.Add(row)
                AdvBandedGridView1.MoveLast()
            Next
        End If
        tempValue7 = ""
        tempValue8 = ""
    End Sub


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If TextEdit3.Text <> "" And IsNumeric(TextEdit3.Text) = False Then
            MsgBox("�ײv�����O�Ʀr", MsgBoxStyle.OkOnly)
            Exit Sub
        End If
        If Len(gluCompany.EditValue) = 0 Then
            MsgBox("�п�ܤ��q", MsgBoxStyle.OkOnly)
            Exit Sub
        End If


        If TextEdit4.Text <> "" And IsNumeric(TextEdit4.Text) = False Then
            MsgBox("�Ҩ�O�����O�Ʀr", MsgBoxStyle.OkOnly)
            Exit Sub
        End If



        Select Case checkLabel.Text
            Case "���ƽs�X"
                If Edit = True Then
                    SaveEdit()
                Else
                    SaveDataNew()
                End If
            Case "�_����"
                SaveDataNew()
            Case "�����f��"
                SaveEdit()
            Case "�|�p���f��"
                If ComboBoxEdit1.Text = "�T�{�L�~" And CheckEdit2.Checked = False Then
                    MsgBox("�f�������P�f�ֵ��G����,���ˬd!", MsgBoxStyle.OkOnly)
                    Exit Sub
                End If
                SaveEdit()
        End Select




    End Sub

    Sub loadeditsub(ByVal dlist As List(Of QuotationInfo))
        If dlist Is Nothing Then Exit Sub
        On Error Resume Next
        Dim i As Integer
        Dim row As DataRow

        Dim j As Integer   '2012-5-16  
        Dim Sign As String
        Sign = ""
        For j = 0 To dlist.Count - 1   ''�u�n�@���O�Ǹ����s�b �A�N�� I �Ƨ�    
            If dlist(j).Q_XH = 0 Then
                Sign = "Y"
            End If
        Next




        For i = 0 To dlist.Count - 1

            row = ds.Tables("Quotation").NewRow
            row("AutoID") = dlist(i).AutoID
            row("Q_QuoID") = dlist(i).Q_QuoID
            row("M_Code") = dlist(i).M_Code
            row("M_Name") = dlist(i).M_Name
            row("M_Gauge") = dlist(i).M_Gauge
            row("Q_SupplierNo") = dlist(i).Q_SupplierNo

            row("Q_Type") = dlist(i).Q_Type

            row("Q_RetrDate") = dlist(i).Q_RetrDate
            row("M_Unit") = dlist(i).M_Unit
            row("Q_Qty") = dlist(i).Q_Qty

            ''�Y Q_XH ���s�b ��� �ɤJ������  �H�e���ƾ� �S�����r�q
            If Sign = "Y" Then
                row("Q_XH") = i + 1  '2012-5-16
            Else
                row("Q_XH") = dlist(i).Q_XH  '2012-5-16
            End If

            '@ 2012/4/13 �K�[ �̷s���ʳ��
            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(dlist(i).M_Code)

            If objInfo.M_Price > 0 Then
                row("M_Price") = objInfo.M_Price
            Else
                row("M_Price") = ""
            End If

            ''Q_Currency �̷s���O2012-5-16
            row("Q_Currency") = objInfo.M_Currency

            row("Q_Price") = dlist(i).Q_Price
            row("Q_Depict") = dlist(i).Q_Depict
            row("PN_NO") = dlist(i).PN_NO
            row("Q_Check") = dlist(i).Q_Check                   ' �����f��
            'row("Q_CheckAction") = dlist(i).Q_CheckAction
            'row("Q_CheckDate") = dlist(i).Q_CheckDate
            row("Q_CheckType") = dlist(i).Q_CheckType
            row("Q_CheckRemark") = dlist(i).Q_CheckRemark

            'row("Q_AccCheck") = dlist(i).Q_AccCheck              '�|�p���f��
            'row("Q_AccCheckDate") = dlist(i).Q_AccCheckDate
            'row("Q_AccCheckRemark") = dlist(i).Q_AccCheckRemark
            'row("Q_AccCheckType") = dlist(i).Q_AccCheckType
            'row("Q_AccCheckAction") = dlist(i).Q_AccCheckAction

            row("Q_Brand") = dlist(i).Q_Brand
            row("Q_Origine") = dlist(i).Q_Origine
            row("Q_StandardPack") = dlist(i).Q_StandardPack
            row("Q_MOQ") = dlist(i).Q_MOQ
            row("Q_ProductionCycle") = dlist(i).Q_ProductionCycle
            row("Q_DeliveredCycle") = dlist(i).Q_DeliveredCycle

            ds.Tables("Quotation").Rows.Add(row)
        Next

    End Sub

    '�ɤJ�ק�ƾ�
    Function LoadEdit(ByVal Q_QuoID As String) As Boolean


        LoadEdit = True
        Dim objInfo As New QuotationInfo
        Dim quoc As New QuotationController
        Try
            objInfo = quoc.Quotation_Get(Q_QuoID)
            If objInfo Is Nothing Then
                '�S���ƾ�
                LoadEdit = False
                Exit Function
            End If
            gluCompany.EditValue = objInfo.CO_ID
            Label8.Text = objInfo.Q_Edition
            dteDate.EditValue = objInfo.Q_EditDate
            gluSupplier.EditValue = objInfo.Q_Supplier
            Label30.Text = objInfo.Q_Type
            cboName.EditValue = objInfo.Q_Associate

            lueCurrency.EditValue = objInfo.Q_Currency
            'txtPNNO.Text = objInfo.PN_NO
            txtRemark.EditValue = objInfo.Q_Remark

            Label19.Text = objInfo.Q_CheckActionName        '�����f�֭�
            Label21.Text = objInfo.Q_CheckDate
            'Label22.Text = objInfo.Q_EditDate
            'Label23.Text = objInfo.Q_Action


            CheckEdit2.Checked = objInfo.Q_AccCheck
            ComboBoxEdit1.Text = objInfo.Q_AccCheckType
            MemoEdit2.Text = objInfo.Q_AccCheckRemark
            Label9.Text = objInfo.Q_AccCheckDate

            Label17.Text = objInfo.Q_AccCheckActionName     '�|�p�f�֭�

            TextEdit2.Text = objInfo.Q_DeliveredAddress
            TextEdit3.Text = objInfo.Q_ExchangeRate
            TextEdit1.Text = objInfo.Q_Validity
            TextEdit4.Text = objInfo.Q_MoldExpense
            ComboBoxEdit2.Text = objInfo.Q_PriceType
            TarriffEdit.EditValue = objInfo.Q_Tarriff.ToString
            ComboBoxEdit3.Text = objInfo.Q_ToForm


            ds.Tables("Quotation").Rows.Clear()
            loadeditsub(quoc.Quotation_Getlist(Q_QuoID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
            'LoadOrdersMainToTable(oc.OrdersMain_GetList(OM_No, Nothing, Nothing, Nothing, Nothing, Nothing))
        Catch ex As Exception
            LoadEdit = False
            MsgBox(ex.Message)
        End Try



    End Function


    '�O�s�s�W���
    Sub SaveDataNew()
        On Error Resume Next
        Dim mc As New QuotationController
        txtQuoID.Text = GetQuo_ID() '���o�����渹"

        If Len(txtQuoID.Text) = 0 Then
            MsgBox("����ͦ������渹�A�L�k�O�s�I")
            Exit Sub
        End If

        If Len(gluCompany.EditValue) = 0 Then
            MsgBox("�п�ܤ��q")
            Exit Sub
        End If

        If Len(gluSupplier.EditValue) = 0 Then
            MsgBox("�п�ܨ�����")
            Exit Sub
        End If


        If Len(lueCurrency.EditValue) = 0 Then
            MsgBox("�п�ܹ��O")
            Exit Sub
        End If





        If ds.Tables("Quotation").Rows.Count = 0 Then
            MsgBox("�п�ܪ���")
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To ds.Tables("Quotation").Rows.Count - 1
            Dim objInfo As New QuotationInfo
            objInfo.Q_QuoID = txtQuoID.Text

            objInfo.Q_Type = Label30.Text

            objInfo.Q_Supplier = gluSupplier.EditValue
            objInfo.Q_Currency = lueCurrency.EditValue
            objInfo.Q_AddDate = Format(Now, "yyyy/MM/dd")
            objInfo.Q_EditDate = Format(Now, "yyyy/MM/dd")
            objInfo.Q_Associate = cboName.EditValue
            objInfo.Q_Edition = 0
            objInfo.Q_AccCheck = False
            objInfo.Q_AccCheckAction = ""
            objInfo.Q_AccCheckDate = ""
            objInfo.Q_AccCheckRemark = ""
            objInfo.Q_AccCheckType = ""
            objInfo.Q_Action = InUserID
            objInfo.Q_Remark = txtRemark.EditValue
            objInfo.CO_ID = gluCompany.EditValue
            'objInfo.PN_NO = txtPNNO.EditValue

            objInfo.Q_CheckAction = ""
            objInfo.Q_CheckDate = ""
            'objInfo.M_Name = ds.Tables("Quotation").Rows(i)("M_Name")
            'objInfo.M_Gauge = ds.Tables("Quotation").Rows(i)("M_Gauge")
            'objInfo.M_Unit = ds.Tables("Quotation").Rows(i)("M_Unit")
            objInfo.M_Code = ds.Tables("Quotation").Rows(i)("M_Code")
            objInfo.Q_SupplierNo = ds.Tables("Quotation").Rows(i)("Q_SupplierNo")
            objInfo.PN_NO = ds.Tables("Quotation").Rows(i)("PN_NO")

            objInfo.Q_Price = CDbl(ds.Tables("Quotation").Rows(i)("Q_Price"))
            objInfo.Q_Depict = ds.Tables("Quotation").Rows(i)("Q_Depict")

            objInfo.Q_Qty = CDbl(ds.Tables("Quotation").Rows(i)("Q_Qty"))
            objInfo.Q_RetrDate = ds.Tables("Quotation").Rows(i)("Q_RetrDate")

            objInfo.Q_Check = False
            objInfo.Q_CheckRemark = ds.Tables("Quotation").Rows(i)("Q_CheckRemark")
            objInfo.Q_CheckType = ds.Tables("Quotation").Rows(i)("Q_CheckType")

            objInfo.Q_Brand = ds.Tables("Quotation").Rows(i)("Q_Brand")
            objInfo.Q_Origine = ds.Tables("Quotation").Rows(i)("Q_Origine")
            objInfo.Q_StandardPack = ds.Tables("Quotation").Rows(i)("Q_StandardPack")
            objInfo.Q_MOQ = ds.Tables("Quotation").Rows(i)("Q_MOQ")
            objInfo.Q_ProductionCycle = ds.Tables("Quotation").Rows(i)("Q_ProductionCycle")
            objInfo.Q_DeliveredCycle = ds.Tables("Quotation").Rows(i)("Q_DeliveredCycle")

            objInfo.Q_DeliveredAddress = TextEdit2.EditValue
            objInfo.Q_ExchangeRate = TextEdit3.EditValue
            objInfo.Q_Validity = TextEdit1.EditValue
            objInfo.Q_MoldExpense = TextEdit4.EditValue
            objInfo.Q_PriceType = ComboBoxEdit2.EditValue

            objInfo.Q_Tarriff = TarriffEdit.EditValue
            objInfo.Q_ToForm = ComboBoxEdit3.EditValue

            objInfo.Q_XH = ds.Tables("Quotation").Rows(i)("Q_XH") ' Q_XH 2012-5-16

            mc.Quotation_Add(objInfo)


        Next

        Me.Close()
    End Sub

    Sub SaveEdit()
        On Error Resume Next
        Dim i As Integer

        If ds.Tables("Quotation").Rows.Count = 0 Then
            MsgBox("�п�ܪ���")
            Exit Sub
        End If

        If Len(txtQuoID.Text) = 0 Then
            MsgBox("�����渹����,����O�s")
            Exit Sub
        End If

        If Len(gluCompany.EditValue) = 0 Then
            MsgBox("�п�ܤ��q")
            Exit Sub
        End If

        If Len(gluSupplier.EditValue) = 0 Then
            MsgBox("�п�ܨ�����")
            Exit Sub
        End If


        If Len(lueCurrency.EditValue) = 0 Then
            MsgBox("�п�ܹ��O")
            Exit Sub
        End If



        '��s�R�����O��
        If ds.Tables("DelData").Rows.Count > 0 Then
            For i = 0 To ds.Tables("DelData").Rows.Count - 1
                Dim objInfo2 As New QuotationInfo
                Dim mc As New QuotationController

                If Not IsDBNull(ds.Tables("DelData").Rows(i)("Q_QuoID")) Then

                    'objInfo2.Q_QuoID = txtQuoID.EditValue
                    'objInfo2.M_Code = ds.Tables("DelData").Rows(i)("M_Code")
                    'MsgBox(objInfo2.M_Code)
                    mc.Quotation_Delete(ds.Tables("DelData").Rows(i)("AutoID"), txtQuoID.EditValue.ToString, ds.Tables("DelData").Rows(i)("M_Code"))

                End If
            Next i
        End If



        For i = 0 To ds.Tables("Quotation").Rows.Count - 1
            If IsDBNull(ds.Tables("Quotation").Rows(i)("Q_QuoID")) Then  '�s�W
                Dim objInfo As New QuotationInfo
                Dim mc As New QuotationController

                objInfo.Q_QuoID = txtQuoID.Text

                objInfo.Q_Type = Label30.Text

                objInfo.Q_Supplier = gluSupplier.EditValue
                objInfo.Q_Currency = lueCurrency.EditValue
                objInfo.Q_AddDate = Format(Now, "yyyy/MM/dd")
                objInfo.Q_EditDate = Format(Now, "yyyy/MM/dd")
                objInfo.Q_Associate = cboName.EditValue
                objInfo.Q_Edition = CInt(Label8.Text)
                objInfo.Q_AccCheck = False
                objInfo.Q_AccCheckAction = ""
                objInfo.Q_AccCheckDate = ""
                objInfo.Q_AccCheckRemark = ""
                objInfo.Q_AccCheckType = ""
                objInfo.Q_Action = InUserID
                objInfo.Q_Remark = txtRemark.EditValue
                objInfo.CO_ID = gluCompany.EditValue

                'objInfo.PN_NO = txtPNNO.EditValue

                objInfo.Q_CheckAction = ""
                objInfo.Q_CheckDate = ""
                'objInfo.M_Name = ds.Tables("Quotation").Rows(i)("M_Name")
                'objInfo.M_Gauge = ds.Tables("Quotation").Rows(i)("M_Gauge")
                'objInfo.M_Unit = ds.Tables("Quotation").Rows(i)("M_Unit")
                objInfo.M_Code = ds.Tables("Quotation").Rows(i)("M_Code")
                objInfo.Q_SupplierNo = ds.Tables("Quotation").Rows(i)("Q_SupplierNo")
                objInfo.Q_Price = CDbl(ds.Tables("Quotation").Rows(i)("Q_Price"))
                objInfo.Q_Depict = ds.Tables("Quotation").Rows(i)("Q_Depict")
                objInfo.Q_Qty = CDbl(ds.Tables("Quotation").Rows(i)("Q_Qty"))
                objInfo.Q_RetrDate = ds.Tables("Quotation").Rows(i)("Q_RetrDate")
                objInfo.PN_NO = ds.Tables("Quotation").Rows(i)("PN_NO")
                objInfo.Q_Check = False
                objInfo.Q_CheckRemark = ds.Tables("Quotation").Rows(i)("Q_CheckRemark")
                objInfo.Q_CheckType = ds.Tables("Quotation").Rows(i)("Q_CheckType")


                objInfo.Q_Brand = ds.Tables("Quotation").Rows(i)("Q_Brand")
                objInfo.Q_Origine = ds.Tables("Quotation").Rows(i)("Q_Origine")
                objInfo.Q_StandardPack = ds.Tables("Quotation").Rows(i)("Q_StandardPack")
                objInfo.Q_MOQ = ds.Tables("Quotation").Rows(i)("Q_MOQ")
                objInfo.Q_ProductionCycle = ds.Tables("Quotation").Rows(i)("Q_ProductionCycle")
                objInfo.Q_DeliveredCycle = ds.Tables("Quotation").Rows(i)("Q_DeliveredCycle")

                objInfo.Q_DeliveredAddress = TextEdit2.EditValue
                objInfo.Q_ExchangeRate = TextEdit3.EditValue
                objInfo.Q_Validity = TextEdit1.EditValue
                objInfo.Q_MoldExpense = TextEdit4.EditValue
                objInfo.Q_PriceType = ComboBoxEdit2.EditValue
                objInfo.Q_Tarriff = TarriffEdit.EditValue
                objInfo.Q_ToForm = ComboBoxEdit3.EditValue

                objInfo.Q_XH = ds.Tables("Quotation").Rows(i)("Q_XH") ' Q_XH 2012-5-16

                mc.Quotation_Add(objInfo)
            ElseIf Not IsDBNull(ds.Tables("Quotation").Rows(i)("Q_QuoID")) Then  '�ק�
                Dim objInfo As New QuotationInfo
                Dim mc As New QuotationController

                Select Case checkLabel.Text
                    Case "���ƽs�X"

                        objInfo.Q_QuoID = txtQuoID.Text
                        objInfo.Q_Supplier = gluSupplier.EditValue
                        objInfo.Q_Currency = lueCurrency.EditValue
                        objInfo.Q_EditDate = Format(Now, "yyyy/MM/dd")  '
                        objInfo.Q_Edition = CInt(Label8.Text)  '
                        objInfo.Q_AccCheck = False 'CheckEdit2.Checked  '
                        objInfo.Q_AccCheckAction = "" 'InUserID  '
                        objInfo.Q_AccCheckDate = "" '
                        objInfo.Q_AccCheckRemark = "" '
                        objInfo.Q_AccCheckType = "" '
                        objInfo.Q_Action = InUserID  '
                        objInfo.Q_CheckAction = "" 'InUserID '
                        objInfo.Q_CheckDate = "" 'Label21.Text  '

                        objInfo.Q_Associate = cboName.EditValue
                        objInfo.Q_Type = Label30.Text
                        objInfo.Q_Remark = txtRemark.EditValue
                        objInfo.CO_ID = gluCompany.EditValue

                        objInfo.AutoID = ds.Tables("Quotation").Rows(i)("AutoID")
                        'objInfo.M_Name = ds.Tables("Quotation").Rows(i)("M_Name")
                        'objInfo.M_Gauge = ds.Tables("Quotation").Rows(i)("M_Gauge")
                        'objInfo.M_Unit = ds.Tables("Quotation").Rows(i)("M_Unit")
                        objInfo.M_Code = ds.Tables("Quotation").Rows(i)("M_Code")
                        objInfo.Q_SupplierNo = ds.Tables("Quotation").Rows(i)("Q_SupplierNo")

                        objInfo.Q_Price = CDbl(ds.Tables("Quotation").Rows(i)("Q_Price"))
                        objInfo.Q_Depict = ds.Tables("Quotation").Rows(i)("Q_Depict")
                        objInfo.Q_Qty = CDbl(ds.Tables("Quotation").Rows(i)("Q_Qty"))
                        objInfo.Q_RetrDate = ds.Tables("Quotation").Rows(i)("Q_RetrDate")
                        objInfo.PN_NO = ds.Tables("Quotation").Rows(i)("PN_NO")

                        objInfo.Q_Check = False
                        objInfo.Q_CheckRemark = ds.Tables("Quotation").Rows(i)("Q_CheckRemark")
                        objInfo.Q_CheckType = ds.Tables("Quotation").Rows(i)("Q_CheckType")

                        objInfo.Q_Brand = ds.Tables("Quotation").Rows(i)("Q_Brand")
                        objInfo.Q_Origine = ds.Tables("Quotation").Rows(i)("Q_Origine")
                        objInfo.Q_StandardPack = ds.Tables("Quotation").Rows(i)("Q_StandardPack")
                        objInfo.Q_MOQ = ds.Tables("Quotation").Rows(i)("Q_MOQ")
                        objInfo.Q_ProductionCycle = ds.Tables("Quotation").Rows(i)("Q_ProductionCycle")
                        objInfo.Q_DeliveredCycle = ds.Tables("Quotation").Rows(i)("Q_DeliveredCycle")

                        objInfo.Q_DeliveredAddress = TextEdit2.EditValue
                        objInfo.Q_ExchangeRate = TextEdit3.EditValue
                        objInfo.Q_Validity = TextEdit1.EditValue
                        objInfo.Q_MoldExpense = TextEdit4.EditValue
                        objInfo.Q_PriceType = ComboBoxEdit2.EditValue
                        objInfo.Q_Tarriff = TarriffEdit.EditValue
                        objInfo.Q_ToForm = ComboBoxEdit3.EditValue

                        objInfo.Q_XH = ds.Tables("Quotation").Rows(i)("Q_XH") ' Q_XH 2012-5-16

                        mc.Quotation_Update(objInfo)
                    Case "�����f��"

                        '�[����������

                        'Dim dt As New FileController
                        'GridFile.DataSource = dt.FileBond_GetList("4001", txtQuoID.EditValue, Nothing)
                        If GridFile.RowCount = 0 Then
                            MsgBox("�ӳ�����S������A����f�֡K�K")
                            Exit Sub
                        End If

                        objInfo.AutoID = ds.Tables("Quotation").Rows(i)("AutoID")
                        objInfo.Q_QuoID = txtQuoID.Text
                        objInfo.M_Code = ds.Tables("Quotation").Rows(i)("M_Code")
                        objInfo.Q_CheckAction = InUserID '
                        objInfo.Q_CheckDate = Format(Now, "yyyy/MM/dd") 'Label21.Text  '
                        objInfo.Q_Check = ds.Tables("Quotation").Rows(i)("Q_Check")
                        objInfo.Q_CheckRemark = ds.Tables("Quotation").Rows(i)("Q_CheckRemark")
                        objInfo.Q_CheckType = ds.Tables("Quotation").Rows(i)("Q_CheckType")
                        mc.Quotation_UpdateCheck(objInfo)

                    Case "�|�p���f��"
                        objInfo.Q_QuoID = txtQuoID.Text
                        objInfo.M_Code = ds.Tables("Quotation").Rows(i)("M_Code")
                        objInfo.Q_AccCheck = CheckEdit2.Checked  '

                        If objInfo.Q_AccCheck = True Then
                            objInfo.Q_Edition = CInt(Label8.Text) + 1
                        ElseIf objInfo.Q_AccCheck = False Then
                            objInfo.Q_Edition = CInt(Label8.Text)
                        End If
                        objInfo.Q_AccCheckAction = InUserID  '
                        objInfo.Q_AccCheckDate = Format(Now, "yyyy/MM/dd") '
                        objInfo.Q_AccCheckRemark = MemoEdit2.Text
                        objInfo.Q_AccCheckType = ComboBoxEdit1.Text
                        mc.Quotation_UpdateAccCheck(objInfo)

                End Select

                ''�����f�ֻP�|�p�f�ֳ����\���ʮɡA�N�����ƪ������s�쪫�ƪ��     '  ��b���ʳ�b�����f�֫�A��s�쪫�ƪ��
                'If CheckEdit2.Checked = True And ds.Tables("Quotation").Rows(i)("Q_Check") = True Then
                '    Dim mm As New MaterialController
                '    mm.MaterialCode_UpdatePrice(ds.Tables("Quotation").Rows(i)("M_Code"), CSng(ds.Tables("Quotation").Rows(i)("Q_Price")), lueCurrency.EditValue, gluSupplier.EditValue, ds.Tables("Quotation").Rows(i)("Q_SupplierNo"))
                'End If

            End If
        Next

        Me.Close()
    End Sub

    '�ͦ��s�渹
    Function GetQuo_ID() As String


        Dim QC As New QuotationController
        Dim QI As New QuotationInfo
        Dim tt As String

        tt = "Q" + Format(Now(), "yyMM")

        QI = QC.Quotation_GetID(tt)

        If QI Is Nothing Then
            GetQuo_ID = tt + "00001"
        Else
            GetQuo_ID = tt + Mid((CInt(Mid(QI.Q_QuoID, 6)) + 100001), 2)

        End If


    End Function

    Sub getenable(ByVal a As Boolean, ByVal b As Boolean, ByVal c As Boolean)
        'Grid.Enabled = a
        gluCompany.Enabled = a
        dteDate.Enabled = a
        gluSupplier.Enabled = a
        cboName.Enabled = a
        lueCurrency.Enabled = a
        txtRemark.Enabled = a

        'GridControl1.Enabled = b
        SimpleButton1.Enabled = b
        SimpleButton2.Enabled = b
        CheckEdit2.Enabled = c
        ComboBoxEdit1.Enabled = c
        MemoEdit2.Enabled = c

        ComboBoxEdit2.Enabled = a
        TextEdit2.Enabled = a
        TextEdit1.Enabled = a
        TextEdit3.Enabled = a
        TextEdit4.Enabled = a
        TarriffEdit.Enabled = a
        ComboBoxEdit3.Enabled = a



    End Sub

    Private Sub popQuotationDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationDel.Click
        If ds.Tables("Quotation").Rows.Count <= 0 Then
            Exit Sub
        ElseIf ds.Tables("Quotation").Rows.Count = 1 Then
            MsgBox("����R���̦Z�@���O��!", , "����")
            Exit Sub
        End If
        If MsgBox("�T�w�n�R����e�檫�Ƴ����H���ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then

            Dim DelTemp As String
            DelTemp = AdvBandedGridView1.GetRowCellDisplayText(ArrayToString(AdvBandedGridView1.GetSelectedRows()), "M_Code")
            If DelTemp = "M_Code" Then
            Else
                '�b�R�����W�[�Q�R�����O��
                Dim row As DataRow = ds.Tables("DelData").NewRow
                'row("M_CodeSub") = CodeSubData.Tables("CodeSub").Rows(GridView1.FocusedRowHandle)("M_CodeSub")
                row("AutoID") = ds.Tables("Quotation").Rows(AdvBandedGridView1.FocusedRowHandle)("AutoID")
                row("Q_QuoID") = ds.Tables("Quotation").Rows(AdvBandedGridView1.FocusedRowHandle)("Q_QuoID")
                row("M_Code") = DelTemp
                ds.Tables("DelData").Rows.Add(row)
            End If

            '���N�襤�O���H�U���s����s�@�U�A�A�R���襤�O�� 2012-5-16
            For i As Integer = CInt(AdvBandedGridView1.GetFocusedRowCellValue("Q_XH")) To ds.Tables("Quotation").Rows.Count - 1 Step 1
                ds.Tables("Quotation").Rows(i)("Q_XH") = i
            Next

            ds.Tables("Quotation").Rows.RemoveAt(CInt(ArrayToString(AdvBandedGridView1.GetSelectedRows())))
        End If
    End Sub

    ''' <summary>
    ''' ���������i���ʫ��s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' @2012/4/12 �K�[ ���������e�̷s���ʳ��30%�H�W���X����
    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Dim i As Integer
        Dim mc As New LFERP.Library.Material.MaterialController
        Dim objInfo As New LFERP.Library.Material.MaterialInfo
        
        Dim strCheckRemark As String
        strCheckRemark = ""

        For i = 0 To ds.Tables("Quotation").Rows.Count - 1
            If ds.Tables("Quotation").Rows(i)("Q_Check") = 0 Then
                GridView2.FocusedRowHandle = i
                objInfo = mc.MaterialCode_Get(ds.Tables("Quotation").Rows(i)("M_Code"))

                If objInfo Is Nothing Then Exit Sub

                ds.Tables("Quotation").Rows(i)("Q_Check") = 1
                ds.Tables("Quotation").Rows(i)("Q_CheckType") = "�i����"

                If objInfo.M_Price > 0 Then
                    If objInfo.M_Price * 1.3 < CSng(ds.Tables("Quotation").Rows(i)("Q_Price")) Then
                        If MsgBox("�����Ƴ��������e�̷s���ʳ��30%�H�W(�̷s���ʳ���G" & objInfo.M_Price & "),�O�_���ʦ����ơH", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "����") = MsgBoxResult.No Then
                            ds.Tables("Quotation").Rows(i)("Q_Check") = 0
                            ds.Tables("Quotation").Rows(i)("Q_CheckType") = "���i����"
                        Else
                            ds.Tables("Quotation").Rows(i)("Q_CheckRemark") = "1�G���������e�̷s���ʳ��30%�H�W"
                            strCheckRemark = "1�G���������e�̷s���ʳ��30%�H�W."
                        End If
                    End If
                Else
                    ds.Tables("Quotation").Rows(i)("Q_CheckRemark") = "�Ĥ@������"
                    GoTo CCC
                End If


                ''''' ''Q_Currency �̷s���O2012-5-16----------------------------------------------------
                If ds.Tables("Quotation").Rows(i)("Q_Currency") = lueCurrency.EditValue Then
                Else
                    If MsgBox("�����Ƴ�������O�P�̷s���ƹ��O�G" & objInfo.M_Currency & ",���@�P. �O�_���ʦ����ơH", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "����") = MsgBoxResult.No Then

                        ds.Tables("Quotation").Rows(i)("Q_Check") = 0
                        ds.Tables("Quotation").Rows(i)("Q_CheckType") = "���i����"
                    Else
                        If strCheckRemark = "" Then
                            ds.Tables("Quotation").Rows(i)("Q_CheckRemark") = "1�G�����Ƴ�������O�P�̷s���O�G" & objInfo.M_Currency & ",���@�P."
                        Else
                            ds.Tables("Quotation").Rows(i)("Q_CheckRemark") = strCheckRemark & "  2�G�����Ƴ�������O�P�̷s���O�G" & objInfo.M_Currency & ",���@�P."
                        End If
                    End If
                End If
CCC:
            End If

        Next
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Dim i As Integer
        For i = 0 To ds.Tables("Quotation").Rows.Count - 1
            ds.Tables("Quotation").Rows(i)("Q_Check") = 0
            ds.Tables("Quotation").Rows(i)("Q_CheckType") = "���i����"

        Next
    End Sub



    Private Sub gluSupplier_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluSupplier.EditValueChanged
        On Error Resume Next
        Dim mi As New LFERP.DataSetting.SuppliersControler
        Dim mc As New LFERP.DataSetting.SuppliersInfo
        mc = mi.Suppliers_Get(gluSupplier.EditValue, Nothing)
        cboName.Properties.Items.Clear()
        If Trim(mc.S_Associate) <> "" And IsDBNull(mc.S_Associate) = False Then
            cboName.Properties.Items.Add(mc.S_Associate)
        End If
        If Trim(mc.S_Associate1) <> "" And IsDBNull(mc.S_Associate1) = False Then
            cboName.Properties.Items.Add(mc.S_Associate1)
        End If
        If Trim(mc.S_Associate2) <> "" And IsDBNull(mc.S_Associate2) = False Then
            cboName.Properties.Items.Add(mc.S_Associate2)
        End If
        If cboName.Properties.Items.Count > 0 Then cboName.Text = cboName.Properties.Items(0)

        lueCurrency.EditValue = mc.S_Currency
    End Sub


    Private Sub popFileShowOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popFileShowOpen.Click
        '���}��ܤ��
        Dim dt As New FileController
        If GridFile.Rows.Count = 0 Then Exit Sub
        dt.File_Open(Nothing, Nothing, GridFile.CurrentRow.Cells("F_No").Value.ToString)

    End Sub

    ''' <summary>
    ''' �����O��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SimpleButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton4.Click

        If ds.Tables("Quotation").Rows.Count = 0 Then Exit Sub

        tempValue = ds.Tables("Quotation").Rows(AdvBandedGridView1.FocusedRowHandle)("M_Code")
        tempValue2 = ds.Tables("Quotation").Rows(AdvBandedGridView1.FocusedRowHandle)("M_Name")

        Dim ff As New FrmQuotationTemp
        ff.ShowDialog()

        tempValue = ""
        tempValue2 = ""

    End Sub

    ''' <summary>
    ''' �����O��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SimpleButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton3.Click
        If ds.Tables("Quotation").Rows.Count = 0 Then Exit Sub
        tempValue = ds.Tables("Quotation").Rows(GridView2.FocusedRowHandle)("M_Code")
        tempValue2 = ds.Tables("Quotation").Rows(GridView2.FocusedRowHandle)("M_Name")

        Dim ff As New FrmQuotationTemp
        ff.ShowDialog()

        tempValue = ""
        tempValue2 = ""
    End Sub

    Private Sub popQuotationCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationCode.Click
        On Error Resume Next
        If AdvBandedGridView1.RowCount = 0 Then Exit Sub
        tempCode = "QuotationView"
        Dim fr As frmMaterialCode
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMaterialCode Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmMaterialCode
        fr.MdiParent = MDIMain
        tempValue5 = AdvBandedGridView1.GetFocusedRowCellValue("M_Code").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub


    ''' <summary>
    ''' �i�_���ʴ_��ت��A���� 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' @2012/4/12 �K�[ ���������e�̷s���ʳ��30%�H�W���X����
    Private Sub RepositoryItemCheckEdit1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemCheckEdit1.CheckedChanged
        Dim strCheckRemark As String
        strCheckRemark = ""

        Dim mc As New LFERP.Library.Material.MaterialController
        Dim objInfo As New LFERP.Library.Material.MaterialInfo
        objInfo = mc.MaterialCode_Get(GridView2.GetFocusedRowCellValue("M_Code"))

        If objInfo Is Nothing Then Exit Sub

        If sender.checked = True Then
            ds.Tables("Quotation").Rows(GridView2.FocusedRowHandle)("Q_CheckType") = "�i����"
            If objInfo.M_Price > 0 Then
                If objInfo.M_Price * 1.3 < CSng(GridView2.GetFocusedRowCellValue("Q_Price")) Then
                    If MsgBox("�����Ƴ��������e�̷s���ʳ��30%�H�W(�̷s���ʳ���G" & objInfo.M_Price & "),�O�_���ʦ����ơH", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "����") = MsgBoxResult.No Then
                        sender.checked = False
                        ds.Tables("Quotation").Rows(GridView2.FocusedRowHandle)("Q_CheckType") = "���i����"
                        ds.Tables("Quotation").Rows(GridView2.FocusedRowHandle)("Q_CheckRemark") = ""
                        Exit Sub
                    Else
                        ds.Tables("Quotation").Rows(GridView2.FocusedRowHandle)("Q_CheckRemark") = "1�G���������e�̷s���ʳ��30%�H�W."
                        strCheckRemark = "1�G���������e�̷s���ʳ��30%�H�W."

                    End If
                End If
            Else
                ds.Tables("Quotation").Rows(GridView2.FocusedRowHandle)("Q_CheckRemark") = "�Ĥ@������"
                Exit Sub
            End If


            ''Q_Currency �̷s���O2012-5-16----------------------------------------------------
            If objInfo.M_Currency = lueCurrency.EditValue Then
            Else
                If MsgBox("�����Ƴ�������O�P�̷s���ƹ��O�G(" & objInfo.M_Currency & "),���@�P. �O�_���ʦ����ơH", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "����") = MsgBoxResult.No Then
                    sender.checked = False
                    ds.Tables("Quotation").Rows(GridView2.FocusedRowHandle)("Q_CheckType") = "���i����"
                    ds.Tables("Quotation").Rows(GridView2.FocusedRowHandle)("Q_CheckRemark") = ""
                Else
                    If strCheckRemark = "" Then
                        ds.Tables("Quotation").Rows(GridView2.FocusedRowHandle)("Q_CheckRemark") = "1�G�����Ƴ������O�P�̷s���O�G(" & objInfo.M_Currency & "),���@�P."
                    Else
                        ds.Tables("Quotation").Rows(GridView2.FocusedRowHandle)("Q_CheckRemark") = strCheckRemark & "  2�G�����Ƴ������O�P�̷s���O�G(" & objInfo.M_Currency & "),���@�P."
                    End If
                End If
            End If


        Else
            ds.Tables("Quotation").Rows(GridView2.FocusedRowHandle)("Q_CheckType") = "���i����"
        End If
    End Sub

    ''' <summary>
    ''' ��ƬO�_�L�~�_��ت��A����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' @2012/4/12 �K�[ ���������e�̷s���ʳ��30%�H�W���X����
    Private Sub CheckEdit2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit2.CheckedChanged
        If CheckEdit2.Checked = True Then
            Dim i As Integer
            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            
            For i = 0 To ds.Tables("Quotation").Rows.Count - 1
                objInfo = mc.MaterialCode_Get(ds.Tables("Quotation").Rows(i)("M_Code"))

                If objInfo Is Nothing Then Exit Sub

                If ds.Tables("Quotation").Rows(i)("Q_Check") = True Then
                    If objInfo.M_Price > 0 Then
                        If objInfo.M_Price * 1.3 < CSng(ds.Tables("Quotation").Rows(i)("Q_Price")) Then
                            MsgBox("��" & i + 1 & "�檫�� " & ds.Tables("Quotation").Rows(i)("M_Name") & " ���������e�̷s���ʳ��30%�H�W" & _
                            vbCrLf & "�̷s���ʳ���G" & objInfo.M_Price & vbCrLf & "��e��������G" & ds.Tables("Quotation").Rows(i)("Q_Price"), MsgBoxStyle.Information, "����")
                        End If


                        '�[������------------------2012-5-17---------
                        If objInfo.M_Currency = lueCurrency.EditValue Then
                        Else
                            MsgBox("��" & i + 1 & "�檫�� " & ds.Tables("Quotation").Rows(i)("M_Name") & "���O�P��e�̷s���O���@�P." & _
                             vbCrLf & "�̷s�ϥι��O���G" & objInfo.M_Currency & vbCrLf & "��e��������O���G" & lueCurrency.EditValue, MsgBoxStyle.Information, "����")
                        End If
                        ''------------------------------------------------------

                    End If
                End If
            Next
        End If
    End Sub

    '@ 2012/4/13 �K�[ ��ܦ渹
    Private Sub GridView2_CustomDrawRowIndicator(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs) Handles GridView2.CustomDrawRowIndicator
        If e.Info.IsRowIndicator And e.RowHandle >= 0 Then '�Ĥ@�椣���
            e.Info.DisplayText = (e.RowHandle + 1).ToString()
        End If
    End Sub


    ''--------2012-5-16--------------------------------------------
    Private Sub popQuotationInt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popQuotationInt.Click
        If ds.Tables("Quotation").Rows.Count > 0 Then
        Else
            Exit Sub
        End If

        tempCode = ""
        tempValue6 = "�����޲z"
        If Label30.Text = "�j�f�妸" Or Label30.Text = "�t��妸" Then
            frmBOMSelect.XtraTabPage1.PageVisible = True
            frmBOMSelect.XtraTabPage2.PageVisible = True
            frmBOMSelect.XtraTabPage3.PageVisible = True
        Else
            frmBOMSelect.XtraTabPage1.PageVisible = True
            frmBOMSelect.XtraTabPage2.PageVisible = False
            frmBOMSelect.XtraTabPage3.PageVisible = False
        End If
        frmBOMSelect.ShowDialog()
        '�W�[�O��
        If frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then
            If tempCode = "" Then
                Exit Sub
            Else
                ' AddRow(tempCode)
                If Int_DateSet(CInt(AdvBandedGridView1.GetFocusedRowCellValue("Q_XH")), tempCode) Then
                End If

            End If
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 1 Then
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempValue7, ",")
            n = Len(Replace(tempValue7, ",", "," & "*")) - Len(tempValue7)
            For i = 0 To n

                Dim j As Integer

                If ds.Tables("Quotation").Rows.Count > 0 Then
                Else
                    GoTo BBB
                End If
                For j = 0 To ds.Tables("Quotation").Rows.Count - 1
                    If arr(i) = ds.Tables("Quotation").Rows(j)("M_Code") Then
                        MsgBox("�@�i�椣���\�����_���ƽs�X....")
                        Exit Sub
                    End If
                Next
                If arr(i) = "" Then
                    Exit Sub
                End If
BBB:
                If Int_DateSet(CInt(AdvBandedGridView1.GetFocusedRowCellValue("Q_XH")), arr(i)) Then
                End If

            Next
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 2 Then
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempValue8, ",")
            n = Len(Replace(tempValue8, ",", "," & "*")) - Len(tempValue8)
            For i = 0 To n

                Dim j As Integer

                'MsgBox(arr(i))
                ' MsgBox(ds.Tables("Quotation").Rows(j)("M_Code"))
                If ds.Tables("Quotation").Rows.Count > 0 Then
                Else
                    GoTo AAA
                End If

                For j = 0 To ds.Tables("Quotation").Rows.Count - 1

                    If arr(i) = ds.Tables("Quotation").Rows(j)("M_Code") Then
                        MsgBox("�@�i�椣���\�����_���ƽs�X....")
                        Exit Sub
                    End If
                Next
AAA:

                If arr(i) = "" Then
                    Exit Sub
                End If

                If Int_DateSet(CInt(AdvBandedGridView1.GetFocusedRowCellValue("Q_XH")), arr(i)) Then
                End If

            Next
        End If
        tempValue7 = ""
        tempValue8 = ""

    End Sub



    Function Int_DateSet(ByVal GetFocuseRow As Integer, ByVal strCode As String) As Boolean   ''�ѼƬ��襤�����
        Int_DateSet = False

        If ds.Tables("Quotation").Rows.Count <= 0 Or GetFocuseRow <= 0 Then
            Exit Function
        End If
        Dim mc As New LFERP.Library.Material.MaterialController
        Dim objInfo As New LFERP.Library.Material.MaterialInfo
        objInfo = mc.MaterialCode_Get(strCode)

        If objInfo Is Nothing Then Exit Function

        If objInfo.M_IsEnabled = False Then  '�P�_��e���ƬO�_�i�� 2012-2-20�A���i�Τ����\�����I
            MsgBox("��e���Ƥ��i�ΡA�����\�����I")
            Exit Function
        End If
        ''----------------------------���[�@�ťզ�A�H�ѩ��U��------------------------------------------------------------
        Dim row As DataRow
        row = ds.Tables("Quotation").NewRow

        row("Q_XH") = ds.Tables("Quotation").Rows(ds.Tables("Quotation").Rows.Count - 1)("Q_XH") + 1

        row("AutoID") = Nothing
        row("Q_QuoID") = Nothing
        row("M_Code") = Nothing
        row("Q_Qty") = 0
        row("Q_Price") = 0
        row("M_Name") = Nothing
        row("M_Unit") = Nothing
        row("M_Gauge") = Nothing
        ds.Tables("Quotation").Rows.Add(row)

        ''���d�X����---------------��U����------------------------------------------------------------------

        Int_DateSet = True

        For i As Integer = ds.Tables("Quotation").Rows.Count - 1 To GetFocuseRow Step -1

            '18
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("AutoID")) Then '' Columns.Add("AutoID", GetType(String)) 
                ds.Tables("Quotation").Rows(i)("AutoID") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("AutoID") = ds.Tables("Quotation").Rows(i - 1)("AutoID")
            End If

            '17
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_QuoID")) Then '.Columns.Add("Q_QuoID", GetType(String)) '2
                ds.Tables("Quotation").Rows(i)("Q_QuoID") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_QuoID") = ds.Tables("Quotation").Rows(i - 1)("Q_QuoID")
            End If

            '1
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("M_Code")) Then  '.Columns.Add("M_Code", GetType(String))
                ds.Tables("Quotation").Rows(i)("M_Code") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("M_Code") = ds.Tables("Quotation").Rows(i - 1)("M_Code")
            End If

            '4
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_Qty")) Then '.Columns.Add("Q_Qty", GetType(Double))
                ' ds.Tables("Quotation").Rows(i)("Q_Qty") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_Qty") = ds.Tables("Quotation").Rows(i - 1)("Q_Qty")
            End If

            ''6-----------------------------------------------------------------------------------------------------
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_Price")) Then ' '.Columns.Add("Q_Price", GetType(Double))
                ' ds.Tables("Quotation").Rows(i)("Q_Price") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_Price") = ds.Tables("Quotation").Rows(i - 1)("Q_Price")
            End If

            '2
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("M_Name")) Then '.Columns.Add("M_Name", GetType(String))
                ds.Tables("Quotation").Rows(i)("M_Name") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("M_Name") = ds.Tables("Quotation").Rows(i - 1)("M_Name")
            End If

            '5
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("M_Unit")) Then  '.Columns.Add("M_Unit", GetType(String))
                ds.Tables("Quotation").Rows(i)("M_Unit") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("M_Unit") = ds.Tables("Quotation").Rows(i - 1)("M_Unit")
            End If
            '3
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("M_Gauge")) Then '.Columns.Add("M_Gauge", GetType(String))
                ds.Tables("Quotation").Rows(i)("M_Gauge") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("M_Gauge") = ds.Tables("Quotation").Rows(i - 1)("M_Gauge")
            End If

            ''�䥦 �U���r�q-----------------------------------
            '7
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("PN_NO")) Then  ''�ӽг渹  '.Columns.Add("PN_NO", GetType(String))
                ds.Tables("Quotation").Rows(i)("PN_NO") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("PN_NO") = ds.Tables("Quotation").Rows(i - 1)("PN_NO")
            End If

            '8
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_Depict")) Then  ''�Ƶ� '.Columns.Add("Q_Depict", GetType(String))
                ds.Tables("Quotation").Rows(i)("Q_Depict") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_Depict") = ds.Tables("Quotation").Rows(i - 1)("Q_Depict")
            End If

            '9
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_SupplierNo")) Then  ''�����ӽs�� '.Columns.Add("Q_SupplierNo", GetType(String))
                ds.Tables("Quotation").Rows(i)("Q_SupplierNo") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_SupplierNo") = ds.Tables("Quotation").Rows(i - 1)("Q_SupplierNo")
            End If

            '10
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_RetrDate")) Then  ''��f���� '.Columns.Add("Q_RetrDate", GetType(String))
                ds.Tables("Quotation").Rows(i)("Q_RetrDate") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_RetrDate") = ds.Tables("Quotation").Rows(i - 1)("Q_RetrDate")
            End If

            '11
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_Brand")) Then  ''�~�P '.Columns.Add("Q_Brand", GetType(String))
                ds.Tables("Quotation").Rows(i)("Q_Brand") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_Brand") = ds.Tables("Quotation").Rows(i - 1)("Q_Brand")
            End If

            '12
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_Origine")) Then  ''���a '.Columns.Add("Q_Origine", GetType(String))
                ds.Tables("Quotation").Rows(i)("Q_Origine") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_Origine") = ds.Tables("Quotation").Rows(i - 1)("Q_Origine")
            End If

            '13
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_StandardPack")) Then  ''Q_StandardPack  '.Columns.Add("Q_StandardPack", GetType(Double))
                ' ds.Tables("Quotation").Rows(i)("Q_StandardPack") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_StandardPack") = ds.Tables("Quotation").Rows(i - 1)("Q_StandardPack")
            End If

            '14
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_MOQ")) Then  ''�̤p�q�ʶq '.Columns.Add("Q_MOQ", GetType(Double))
                ' ds.Tables("Quotation").Rows(i)("Q_MOQ") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_MOQ") = ds.Tables("Quotation").Rows(i - 1)("Q_MOQ")
            End If

            '15
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_ProductionCycle")) Then  ''�Ͳ��P�� '.Columns.Add("Q_ProductionCycle", GetType(String))
                ds.Tables("Quotation").Rows(i)("Q_ProductionCycle") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_ProductionCycle") = ds.Tables("Quotation").Rows(i - 1)("Q_ProductionCycle")
            End If

            '16
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_DeliveredCycle")) Then  ''�e�f�P��  '.Columns.Add("Q_DeliveredCycle", GetType(String))
                ds.Tables("Quotation").Rows(i)("Q_DeliveredCycle") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_DeliveredCycle") = ds.Tables("Quotation").Rows(i - 1)("Q_DeliveredCycle")
            End If




            '19

            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_Currency")) Then   '.Columns.Add("Q_Currency", GetType(String)) ''�ɤJ�̷s���O
                ds.Tables("Quotation").Rows(i)("Q_Currency") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_Currency") = ds.Tables("Quotation").Rows(i - 1)("Q_Currency")
            End If

            '20

            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("M_Price")) Then   '.Columns.Add("M_Price", GetType(Double))
                'ds.Tables("Quotation").Rows(i)("M_Price") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("M_Price") = ds.Tables("Quotation").Rows(i - 1)("M_Price")
            End If

            '21
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_Check")) Then    '.Columns.Add("Q_Check", GetType(Boolean))
                ds.Tables("Quotation").Rows(i)("Q_Check") = False
            Else
                ds.Tables("Quotation").Rows(i)("Q_Check") = ds.Tables("Quotation").Rows(i - 1)("Q_Check")
            End If

            '22
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_CheckRemark")) Then    '.Columns.Add("Q_CheckRemark", GetType(String))
                ds.Tables("Quotation").Rows(i)("Q_CheckRemark") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_CheckRemark") = ds.Tables("Quotation").Rows(i - 1)("Q_CheckRemark")
            End If

            '23
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_CheckType")) Then      '.Columns.Add("Q_CheckType", GetType(String))
                ds.Tables("Quotation").Rows(i)("Q_CheckType") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_CheckType") = ds.Tables("Quotation").Rows(i - 1)("Q_CheckType")
            End If

            '24
            If IsDBNull(ds.Tables("Quotation").Rows(i - 1)("Q_Type")) Then        '.Columns.Add("Q_Type", GetType(String))    ''����  �j�f  �妸
                ds.Tables("Quotation").Rows(i)("Q_Type") = Nothing
            Else
                ds.Tables("Quotation").Rows(i)("Q_Type") = ds.Tables("Quotation").Rows(i - 1)("Q_Type")
            End If


            '.Columns.Add("Q_CheckAction", GetType(String))
            '.Columns.Add("Q_XH", GetType(Integer))



        Next
        ''----------�d�ߥX���O�� ���J�������W-----------------------------------------------------------------------------------------
        Dim j As Integer = GetFocuseRow - 1

        ds.Tables("Quotation").Rows(j)("AutoID") = Nothing
        ds.Tables("Quotation").Rows(j)("Q_QuoID") = Nothing
        ds.Tables("Quotation").Rows(j)("M_Code") = objInfo.M_Code
        ds.Tables("Quotation").Rows(j)("Q_Qty") = 0

        ds.Tables("Quotation").Rows(j)("Q_Price") = 0
        ds.Tables("Quotation").Rows(j)("M_Name") = objInfo.M_Name
        ds.Tables("Quotation").Rows(j)("M_Unit") = objInfo.M_Unit
        ds.Tables("Quotation").Rows(j)("M_Gauge") = objInfo.M_Gauge

        '-------------------------------------------------------------

        ds.Tables("Quotation").Rows(j)("Q_SupplierNo") = Nothing
        ds.Tables("Quotation").Rows(j)("Q_Type") = Nothing
        ds.Tables("Quotation").Rows(j)("M_Price") = 0
        ds.Tables("Quotation").Rows(j)("Q_RetrDate") = Nothing
        ds.Tables("Quotation").Rows(j)("Q_Depict") = Nothing


        ds.Tables("Quotation").Rows(j)("PN_NO") = Nothing
        ds.Tables("Quotation").Rows(j)("Q_Check") = False
        ds.Tables("Quotation").Rows(j)("Q_CheckRemark") = Nothing
        ds.Tables("Quotation").Rows(j)("Q_CheckType") = Nothing
        ds.Tables("Quotation").Rows(j)("Q_Brand") = Nothing

        ds.Tables("Quotation").Rows(j)("Q_Origine") = Nothing
        ds.Tables("Quotation").Rows(j)("Q_StandardPack") = 0
        ds.Tables("Quotation").Rows(j)("Q_MOQ") = 0
        ds.Tables("Quotation").Rows(j)("Q_ProductionCycle") = Nothing
        ds.Tables("Quotation").Rows(j)("Q_DeliveredCycle") = Nothing

        ds.Tables("Quotation").Rows(j)("Q_Currency") = Nothing


        'Currency 2012-5-16
        '.Columns.Add("Q_CheckAction", GetType(String))
        '.Columns.Add("Q_XH", GetType(Integer))

    End Function








    Private Sub Grid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grid.Click

    End Sub

    Private Sub AdvBandedGridView1_CustomDrawRowIndicator(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs) Handles AdvBandedGridView1.CustomDrawRowIndicator
        '@ 2012/4/13 �K�[ ��ܦ渹
        If e.Info.IsRowIndicator And e.RowHandle >= 0 Then '�Ĥ@�椣���
            e.Info.DisplayText = (e.RowHandle + 1).ToString()

        End If

    End Sub
End Class