Imports LFERP.DataSetting
Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.ProductionPayPersonnel

Imports System.Data.SqlClient
Imports LFERP.Library.ProductionPiecePersonnelDay

Imports LFERP.Library.ProductionPieceWorkGroup
Imports LFERP.Library.ProductionPiecePersonnel


Public Class ProductionPieceSelectAll

    Dim ds As New DataSet
    Dim dsP As New DataSet
    Dim TempA, TempB As String
    Private Sub ProductionPieceSelectAll_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LabPerClass.Visible = False
        cboPer_Class.Visible = False


        GluG_NO.Visible = False
        Dim StrA As String = ""
        Dim StrB As String = ""
        Dim StrC As String = ""

        TempA = tempValue
        TempB = tempValue8
        tempValue = Nothing
        tempValue8 = Nothing
        Me.Text = TempA

        InputTextEdit.Select()
        StateLookUp.Visible = False
        Date_YYMM.Visible = False
        XtraTabControl1.SelectedTabPageIndex = 0
        StartDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
        EndDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
        Date_YYMM.EditValue = Format(Now, "yyyy�~MM��")

        ''------------------------------------------------------------
        CreateTable()
        Load_Fac()
        load_Stata()
        load_Pro_Type()
        load_Bool()
        ''---------------------------------------------------------------

        BZLabel.Text = ""  '�D�n�w�� �~���d�߮�,�~�몺��J
        LabelControl3.Text = ""


        ''���G�Ʋ� TempA �n�q�{���奻�榡
        Select Case TempA
            Case "�p��u��" ''-----------------------------------------------1�u������,���~�s��,�t��W��
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�u���渹")
                ComboType.Properties.Items.Add("���~�s��")
                ComboType.Text = "�u���渹"

                StateLookUp.Visible = True ''�f��

                LabelControlDate.Text = "�إߤ��(&E):"
                StartDateEdit.EditValue = DateAdd(DateInterval.Day, -30, CDate(Format(Now, "yyyy/MM/dd")))
                EndDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
                InputTextEdit.Text = TempB

                ''----------------------------------------------------------------
                StrA = "�u���渹,�u������,���~�s��,�t��W��,����,�f�֧_,�إߤ��,�f�֮ɶ�"
                StrB = "��r,��r,��r,��r,��r,����,���,���"
                StrC = "PP_ID,Pro_Type,PM_M_Code,PM_Type,DPT_ID,PP_Check,AddDate,PP_CheckDate"

            Case "�էO�W��" ''---------------------------------------------------2
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�էO�s��")
                ComboType.Properties.Items.Add("�էO�W��")
                ComboType.Properties.Items.Add("�t�d�H")
                ComboType.Text = "�էO�s��"
                LabelControlDate.Text = "�إߤ��(&E):"
                StartDateEdit.EditValue = Nothing
                EndDateEdit.EditValue = Nothing
                'StartDateEdit.Visible = False
                'EndDateEdit.Visible = False
                'LabelControlDate.Visible = False
                'LabelControl4.Visible = False
                'LabelControl5.Visible = False
                PanelControl2.Visible = False
                PanelControl1.Top = PanelControl1.Top - 30

                ''----------------------------------------------------------------
                StrA = "�էO�s��,�էO�W��,�t�d�H,����,�إߤ��"
                StrB = "��r,��r,��r,��r,���"
                StrC = "G_NO,G_Name,G_Manager,DepID,G_Date"

            Case "���u�W��" ''---------------------------------------------------3
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�t���s��")
                ComboType.Properties.Items.Add("���u�m�W")
                ComboType.Properties.Items.Add("�էO�s��")
                ComboType.Properties.Items.Add("�~������")
                ComboType.Text = "�t���s��"
                LabelControlDate.Text = "�إߤ��(&E):"
                StartDateEdit.EditValue = Nothing
                EndDateEdit.EditValue = Nothing
                'StartDateEdit.Visible = False
                'EndDateEdit.Visible = False
                'LabelControlDate.Visible = False
                'LabelControl4.Visible = False
                'LabelControl5.Visible = False
                PanelControl2.Visible = False
                PanelControl1.Top = PanelControl1.Top - 30

                ''----------------------------------------------------------------
                StrA = "���u�s��,���u�m�W,�էO�s��,�~������,����,�إߤ��,��u�_,�Z��"
                StrB = "��r,��r,��r,��r,��r,���,����,��r"
                StrC = "Per_NO,Per_Name,G_NO,Per_PayType,DepID,Per_Date,Per_Resign,KQClass"

                '2013-8-9
                LabPerClass.Visible = True
                cboPer_Class.Visible = True
                cboPer_Class.Properties.Items.Clear()
                cboPer_Class.Text = "����"
                cboPer_Class.Properties.Items.Add("����")
                cboPer_Class.Properties.Items.Add("�կZ")
                cboPer_Class.Properties.Items.Add("�]�Z")

            Case "�ӤH�p��έp" ''------------------------------------------------------------------------------4
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�t���s��")
                ComboType.Properties.Items.Add("���~�s��")
                ComboType.Text = "�t���s��"
                LabelControlDate.Text = "�p����(&E):"
                StartDateEdit.EditValue = Format(Now, "yyyy/MM") & "/01"
                EndDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
                InputTextEdit.Text = TempB
                PanelControl1.Top = PanelControl1.Top - 30

                ''----------------------------------------------------------------
                StrA = "�p��渹,���u�s��,�u������,���~�s��,�t��W��,����,�p����"
                StrB = "��r,��r,��r,��r,��r,��r,���"
                StrC = "PP_NO,Per_NO,Pro_Type,PM_M_Code,PM_Type,DepID,PP_Date"

            Case "�ӤH�p��έp�C�L", "�ӤH�p��έp�C�L���`" ''---------------------------------------------------4.1
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�t���s��")
                ComboType.Text = "�t���s��"
                LabelControlDate.Text = "�p����(&E):"
                StartDateEdit.EditValue = Format(Now, "yyyy/MM") & "/01"
                EndDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
                InputTextEdit.Text = TempB

                Up_PrintXY()

            Case "�էO�p��έp" ''-------------------------------------------------------------------------------5
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�էO�s��")
                ComboType.Properties.Items.Add("���~�s��")
                ComboType.Text = "�էO�s��"
                LabelControlDate.Text = "�p����(&E):"
                StartDateEdit.EditValue = Format(Now, "yyyy/MM") & "/01"
                EndDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
                InputTextEdit.Text = TempB
                PanelControl1.Top = PanelControl1.Top - 30

                ''----------------------------------------------------------------
                StrA = "�p��渹,�էO�s��,�u������,���~�s��,�t��W��,����,�p����"
                StrB = "��r,��r,��r,��r,��r,��r,���"
                StrC = "GP_NO,G_NO,Pro_Type,PM_M_Code,PM_Type,DepID,PP_Date"

            Case "�էO�p��έp�C�L", "�էO�p��έp�C�L���`" ''---------------------------------------------------5.1
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�էO�s��")
                ComboType.Text = "�էO�s��"
                LabelControlDate.Text = "�p����(&E):"
                StartDateEdit.EditValue = Format(Now, "yyyy/MM") & "/01"
                EndDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
                InputTextEdit.Text = TempB

                Up_PrintXY()

            Case "�ӤH�p�ɲέp" ''--------------------------------------------------------------------------------6
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�t���s��")
                ComboType.Text = "�t���s��"
                LabelControlDate.Text = "�p�ɤ��(&E):"
                StartDateEdit.EditValue = Format(Now, "yyyy/MM") & "/01"
                EndDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
                InputTextEdit.Text = TempB
                PanelControl1.Top = PanelControl1.Top - 30

                ''----------------------------------------------------------------
                StrA = "�p�ɳ渹,�t���s��,����,�p�ɤ��"
                StrB = "��r,��r,��r,���"
                StrC = "PT_NO,Per_NO,DepID,PT_Date"


            Case "�ӤH�p�ɲέp�C�L", "�ӤH�p�ɲέp�C�L���`", "�ӤH�p��έp�C�L���`�M��" ''------------------------------------------------------6.1
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�t���s��")
                ComboType.Text = "�t���s��"
                LabelControlDate.Text = "�p�ɤ��(&E):"
                StartDateEdit.EditValue = Format(Now, "yyyy/MM") & "/01"
                EndDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
                InputTextEdit.Text = TempB

                If TempA <> "�ӤH�p��έp�C�L���`�M��" Then
                    Up_PrintXY()
                Else
                    LabPerClass.Visible = True
                    cboPer_Class.Visible = True
                    LabPerClass.Text = "���A(&E)"
                    cboPer_Class.Properties.Items.Clear()
                    cboPer_Class.Text = "����"
                    cboPer_Class.Properties.Items.Add("����")
                    cboPer_Class.Properties.Items.Add("�b��")
                    cboPer_Class.Properties.Items.Add("��u")
                End If

            Case "�էO�p�ɲέp" ''----------------------------------------------------------------------------------------7
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�էO�s��")
                ComboType.Text = "�էO�s��"
                LabelControlDate.Text = "�p�ɤ��(&E):"
                StartDateEdit.EditValue = Format(Now, "yyyy/MM") & "/01"
                EndDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
                InputTextEdit.Text = TempB
                PanelControl1.Top = PanelControl1.Top - 30

                ''----------------------------------------------------------------
                StrA = "�p�ɳ渹,�էO�s��,����,�p�ɤ��"
                StrB = "��r,��r,��r,���"
                StrC = "GT_NO,G_NO,DepID,GT_Date"

            Case "�էO�p�ɲέp�C�L", "�էO�p�ɲέp�C�L���`", "�էO�p��έp�C�L���`�M��" ''----------------------------------------------------------7.1
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�էO�s��")
                ComboType.Text = "�էO�s��"
                LabelControlDate.Text = "�p�ɤ��(&E):"
                StartDateEdit.EditValue = Format(Now, "yyyy/MM") & "/01"
                EndDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
                InputTextEdit.Text = TempB

                If TempA <> "�էO�p��έp�C�L���`�M��" Then
                    Up_PrintXY()
                End If


            Case "�ӤH�p���~��" ''--------------------------------------------------------------------------------------8
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�t���s��")
                ComboType.Text = "�t���s��"
                InputTextEdit.Text = TempB

                StateLookUp.Visible = True
                Date_YYMM.Visible = True
                'StartDateEdit.Visible = False
                'EndDateEdit.Visible = False
                'LabelControl4.Visible = False
                'LabelControl5.Visible = False
                PanelControl3.Visible = False
                'Date_YYMM.Left = LabelControlDate.Left + 40
                LabelControlDate.Text = "�~��(&E):"
                BZLabel.Text = "�~��"

                LabPerClass.Visible = True
                cboPer_Class.Visible = True

                ComboBoxReg.Visible = True
                LabReg.Visible = True
                Me.ComboBoxReg.Text = "�b¾"


                ''-----------------
                cboPer_Class.Text = "����"
                cboPer_Class.Properties.Items.Clear()
                cboPer_Class.Properties.Items.Add("����")
                cboPer_Class.Properties.Items.Add("�կZ")
                cboPer_Class.Properties.Items.Add("�]�Z")

                ''----------------------------------------------------------------
                StrA = "�t���s��,���u�m�W,���`���,����,�f��,���`���,�f�֤��,�Z��"
                StrB = "��r,��r,�~��,��r,����,���,���,��r"
                StrC = "Per_NO,Per_Name,PL_YYMM,DepID,PL_Check,PL_AddDate,PL_CheckDate,Per_Class"


            Case "�ӤH�p���~���C�L" ''-----------------------------------------------------------------------------------8.1
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�t���s��")
                ComboType.Text = "�t���s��"
                InputTextEdit.Text = TempB

                StateLookUp.Visible = True
                Date_YYMM.Visible = True
                PanelControl3.Visible = False
                'StartDateEdit.Visible = False
                'EndDateEdit.Visible = False
                'LabelControl4.Visible = False
                'LabelControl5.Visible = False
                'LabelControlDate.Top = LabelControlDate.Top - 50
                'Date_YYMM.Left = LabelControlDate.Left + 40
                'Date_YYMM.Top = LabelControlDate.Top - 2
                LabelControlDate.Text = "�~��(&E):"
                BZLabel.Text = "�~��"

                LabPerClass.Visible = True
                cboPer_Class.Visible = True

                ComboBoxReg.Visible = True
                LabReg.Visible = True
                Me.ComboBoxReg.Text = "�b¾"


                cboPer_Class.Text = "����"
                cboPer_Class.Properties.Items.Clear()
                cboPer_Class.Properties.Items.Add("����")
                cboPer_Class.Properties.Items.Add("�կZ")
                cboPer_Class.Properties.Items.Add("�]�Z")

                '  Up_PrintXY()

            Case "�էO�p���~��" ''--------------------------------------------------------------------------------------9
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�էO�s��")
                ComboType.Text = "�էO�s��"
                StateLookUp.Visible = True
                Date_YYMM.Visible = True
                PanelControl3.Visible = False
                'StartDateEdit.Visible = False
                'EndDateEdit.Visible = False
                'LabelControl4.Visible = False
                'LabelControl5.Visible = False
                'Date_YYMM.Left = LabelControlDate.Left + 40
                'Date_YYMM.Top = LabelControlDate.Top - 2
                LabelControlDate.Text = "�~��(&E):"
                BZLabel.Text = "�~��"

                ''----------------------------------------------------------------
                StrA = "�էO�s��,���`���,����,�f��,���`���,�f�֤��"
                StrB = "��r,�~��,��r,����,���,���"
                StrC = "G_NO,PY_YYMM,DepID,PY_Check,PY_AddDate,PY_CheckDate"

            Case "�էO�p���~���C�L" ''-----------------------------------------------------------------------------------9.1
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�էO�s��")
                ComboType.Text = "�էO�s��"
                InputTextEdit.Text = TempB

                StateLookUp.Visible = True
                Date_YYMM.Visible = True
                PanelControl3.Visible = True
                'StartDateEdit.Visible = False
                'EndDateEdit.Visible = False
                'LabelControl4.Visible = False
                'LabelControl5.Visible = False
                'LabelControlDate.Top = LabelControlDate.Top - 50
                'Date_YYMM.Left = LabelControlDate.Left + 40
                'Date_YYMM.Top = LabelControlDate.Top - 2
                LabelControlDate.Text = "�~��(&E):"
                BZLabel.Text = "�~��"

                PanelControl3.Visible = False

                If strInUserRank = "�Ͳ���" Then
                    lueFacID.EditValue = strInFacIDFull
                    lueDepID.EditValue = Nothing
                ElseIf strInUserRank = "�޲z" Then
                    lueFacID.EditValue = Nothing
                    lueDepID.EditValue = Nothing
                ElseIf strInUserRank = "�έp" Then
                    lueFacID.EditValue = strInFacIDFull
                    lueDepID.EditValue = strInDepIDFull
                End If


                OKButton.Text = "�C�L(&P)"
                'Up_PrintXY()

            Case "�ӤH�p���~�����`" ''--------------------------------------------------------------------------------------10
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�t���s��")
                ComboType.Text = "�t���s��"
                StateLookUp.Visible = True
                Date_YYMM.Visible = True
                PanelControl3.Visible = False
                'StartDateEdit.Visible = False
                'EndDateEdit.Visible = False
                'LabelControl4.Visible = False
                'LabelControl5.Visible = False
                'Date_YYMM.Left = LabelControlDate.Left + 40
                'Date_YYMM.Top = LabelControlDate.Top - 2
                LabelControlDate.Text = "�~��(&E):"
                BZLabel.Text = "�~��"

            Case "�ӤH�p���~�����`�C�L" ''----------------------------------------------------------------------------------10.1
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�t���s��")
                ComboType.Text = "�t���s��"
                InputTextEdit.Text = TempB

                StateLookUp.Visible = True
                Date_YYMM.Visible = True
                PanelControl3.Visible = False
                'StartDateEdit.Visible = False
                'EndDateEdit.Visible = False
                'LabelControl4.Visible = False
                'LabelControl5.Visible = False
                'LabelControlDate.Top = LabelControlDate.Top - 50
                'Date_YYMM.Left = LabelControlDate.Left + 40
                'Date_YYMM.Top = LabelControlDate.Top - 2
                LabelControlDate.Text = "�~��(&E):"
                BZLabel.Text = "�~��"

                Up_PrintXY()

            Case "�էO�~���վ�"
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("��X�էO�s��")
                ComboType.Properties.Items.Add("��J�էO�s��")
                ComboType.Text = "��X�էO�s��"
                InputTextEdit.Text = TempB

                StateLookUp.Visible = True
                Date_YYMM.Visible = True
                PanelControl3.Visible = False

                LabelControlDate.Text = "�~��(&E):"
                BZLabel.Text = "�~��"

                Me.RadioButton2.Text = "��X�t�O����(&T)�G"

                ''----------------------------------------------------------------
                StrA = "��X�էO�s��,��X����,��J�էO�s��,��J����,�ާ@���,�վ���,�f��,�f�֤��"
                StrB = "��r,��r,��r,��r,���,�~��,����,���"
                StrC = "OUT_G_NO,OUT_DepID,IN_G_NO,IN_DepID,AddDate,Ad_YYMM,Ad_Check,Ad_CheckDate"
                ''-------------------------------------------------------------------------------------------------------
            Case "�ӤH��p����`�C�L"
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�t���s��")
                ComboType.Text = "�t���s��"
                InputTextEdit.Text = TempB

                StateLookUp.Visible = True
                Date_YYMM.Visible = True
                PanelControl3.Visible = False

                RadioButton2.Visible = False
                FacLabel.Visible = False
                DepLabel.Visible = False
                lueFacID.Visible = False
                lueDepID.Visible = False

                LabelControl1.Visible = False
                StateLookUp.Visible = False


                LabelControlDate.Text = "�~��(&E):"
                BZLabel.Text = "�~��"

            Case "�էO�p�����`�C�L"
                GluG_NO.Visible = True
                InputTextEdit.Visible = False
                GluG_NO.Left = InputTextEdit.Left
                GluG_NO.Width = InputTextEdit.Width

                Dim pc1 As New ProductionPieceWorkGroupControl
                GluG_NO.Properties.DisplayMember = "G_NOName"
                GluG_NO.Properties.ValueMember = "G_NO"
                GluG_NO.Properties.DataSource = pc1.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�էO�s��")
                ComboType.Text = "�էO�s��"
                InputTextEdit.Text = TempB

                StateLookUp.Visible = True
                Date_YYMM.Visible = True
                PanelControl3.Visible = True
                StartDateEdit.Visible = True
                LabelControlDate.Text = "�p����:"
                StartDateEdit.Left = LabelControlDate.Left + 1
                EndDateEdit.Visible = False

                LabelControl1.Visible = False
                StateLookUp.Visible = False

                RadioButton2.Visible = False
                FacLabel.Visible = False
                DepLabel.Visible = False
                lueFacID.Visible = False
                lueDepID.Visible = False

                LabelControl4.Visible = False
                LabelControl5.Visible = False
                EndDateEdit.Visible = False
                Date_YYMM.Visible = False


            Case "�էO�p��ӤH���ӦC�L"

                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�t���s��")
                ComboType.Text = "�t���s��"
                InputTextEdit.Text = TempB

                StateLookUp.Visible = True
                Date_YYMM.Visible = True
                PanelControl3.Visible = False

                RadioButton2.Visible = False
                FacLabel.Visible = False
                DepLabel.Visible = False
                lueFacID.Visible = False
                lueDepID.Visible = False


                LabelControl1.Visible = False
                StateLookUp.Visible = False

                LabelControlDate.Text = "�~��(&E):"
                BZLabel.Text = "�~��"

            Case "�C�L���f�֤u��"

                RadioButton1.Checked = False
                RadioButton1.Enabled = False
                RadioButton2.Checked = True

                ComboType.Enabled = False
                InputTextEdit.Enabled = False
              
                LabelControlDate.Text = "�_�l���(&E):"
                StartDateEdit.EditValue = DateAdd(DateInterval.Day, -30, CDate(Format(Now, "yyyy/MM/dd")))
                EndDateEdit.EditValue = Format(Now, "yyyy/MM/dd")

                PanelControl1.Top = PanelControl1.Top - 30
            Case "�ӤH��p����`�����C�L"
                RadioButton1.Enabled = False
                RadioButton1.Checked = False
                RadioButton2.Checked = True

                ComboType.Enabled = False
                InputTextEdit.Enabled = False
                PanelControl1.Visible = True

                LabelControlDate.Text = "�p����:"
                StartDateEdit.Left = LabelControlDate.Left + 1
                EndDateEdit.Visible = False
                LabelControl4.Visible = False
                LabelControl5.Visible = False
                EndDateEdit.Visible = False
                Date_YYMM.Visible = False

            Case "�էO�W����`�C�L"
                ComboType.Properties.Items.Clear()
                ComboType.Properties.Items.Add("�էO�s��")
                ComboType.Text = "�էO�s��"
                InputTextEdit.Text = TempB

                StateLookUp.Visible = True
                Date_YYMM.Visible = True
                PanelControl3.Visible = True
                LabelControlDate.Text = "�~��(&E):"
                BZLabel.Text = "�~��"

                PanelControl3.Visible = False
                If strInUserRank = "�Ͳ���" Then
                    lueFacID.EditValue = strInFacIDFull
                    lueDepID.EditValue = Nothing
                ElseIf strInUserRank = "�޲z" Then
                    lueFacID.EditValue = Nothing
                    lueDepID.EditValue = Nothing
                ElseIf strInUserRank = "�έp" Then
                    lueFacID.EditValue = strInFacIDFull
                    lueDepID.EditValue = strInDepIDFull
                End If
                OKButton.Text = "�C�L(&P)"
        End Select


        ''---------------------------------------------------------------------------
        If Val(Format(Now, "dd")) < 10 Then
            StartDateEdit.EditValue = Format(DateAdd(DateInterval.Month, -1, CDate(Format(Now, "yyyy/MM/dd"))), "yyyy/MM") & "/01"
            EndDateEdit.EditValue = DateAdd(DateInterval.Day, -1, CDate(Format(Now, "yyyy/MM") & "/01"))
            Date_YYMM.EditValue = Format(DateAdd(DateInterval.Month, -1, CDate(Format(Now, "yyyy/MM/dd"))), "yyyy�~MM��")
        Else
            StartDateEdit.EditValue = Format(Now, "yyyy/MM") & "/01"
            EndDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
            Date_YYMM.EditValue = Format(Now, "yyyy�~MM��")
        End If

        ''---------------------------------------------------------------------------

        Dim StrAarray As Array = Split(StrA, ",")
        Dim StrBarray As Array = Split(StrB, ",")
        Dim StrCarray As Array = Split(StrC, ",")

        Dim Row As DataRow
        For i As Integer = 0 To UBound(StrAarray)
            Row = ds.Tables("SelectTb").NewRow
            Row("AutoID") = i
            Row("Category") = StrAarray(i)
            Row("Type") = StrBarray(i)
            Row("FieldName") = StrCarray(i)
            ds.Tables("SelectTb").Rows.Add(Row)
        Next i

        GridView1_Click(Nothing, Nothing)
        ComboBoxEdit3.Width = AddTextEdit.Width

        ''�C�L�����Ѧ۩w�q�d��
        If InStr(TempA, "�C�L", CompareMethod.Text) > 0 Then
            Me.Label1.Text = "�C�L�Ҷ�"
            XtraTabPage2.PageVisible = False
        End If

        If StateLookUp.Visible = False Then
            LabelControl1.Visible = False
        End If
    End Sub


    Sub Up_PrintXY() ''�b�C�L�� 
        RadioButton2.Checked = False
        RadioButton2.Visible = False
        RadioButton1.Checked = True

        DepLabel.Enabled = True
        FacLabel.Enabled = True
        lueFacID.Enabled = True
        lueDepID.Enabled = True

        If strInUserRank = "�Ͳ���" Then
            lueFacID.EditValue = strInFacIDFull
            lueDepID.EditValue = Nothing
        ElseIf strInUserRank = "�޲z" Then
            lueFacID.EditValue = Nothing
            lueDepID.EditValue = Nothing
        ElseIf strInUserRank = "�έp" Then
            lueFacID.EditValue = strInFacIDFull
            lueDepID.EditValue = strInDepIDFull
        End If
        

        OKButton.Text = "�C�L(&P)"
        If StateLookUp.Visible = False Then
            PanelControl1.Top = InputTextEdit.Top + 6
        Else
            PanelControl1.Top = StateLookUp.Top + 6
        End If
        PanelControl2.Top = lueDepID.Top + lueDepID.Height + 9
        PanelControl2.Left = DepLabel.Left
        'FacLabel.Top = lueFacID.Top + 3
        'lueDepID.Top = lueFacID.Top + lueFacID.Height + 12
        'DepLabel.Top = lueDepID.Top + 3
        'Date_YYMM.Left = LabelControlDate.Left + 40
        'Date_YYMM.Top = lueDepID.Top + lueDepID.Height + 12
        'LabelControlDate.Top = Date_YYMM.Top + 3
        'StartDateEdit.Top = lueDepID.Top + lueDepID.Height + 12
        'EndDateEdit.Top = StartDateEdit.Top
        'LabelControl4.Top = LabelControlDate.Top
        'LabelControl5.Top = LabelControlDate.Top
    End Sub

    ''' <summary>
    ''' �T�w�d�߸��J�t�O
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_Fac()
        ' �[���t�O�W��() 
        Dim fc As New FacControler
        Dim fcl As New List(Of FacInfo)
        Dim i As Integer

        fcl = fc.GetFacList(strInFacID, Nothing)

        If fcl.Count <= 0 Then Exit Sub

        ds.Tables("TFac").Clear()

        If strInFacID = Nothing Then
            Dim row As DataRow
            row = ds.Tables("TFac").NewRow
            row("FacName") = "����"
            row("FacID") = "*"
            row("FacIDName") = "* ����"
            ds.Tables("TFac").Rows.Add(row)
        End If

        For i = 0 To fcl.Count - 1
            Dim row1 As DataRow
            row1 = ds.Tables("TFac").NewRow
            row1("FacName") = fcl(i).FacName
            row1("FacID") = fcl(i).FacID
            row1("FacIDName") = fcl(i).FacID & " " & fcl(i).FacName
            ds.Tables("TFac").Rows.Add(row1)
        Next
    End Sub
    ''' <summary>
    ''' �T�w�d�߸��J����
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_Dep()
        ' �[���t�O�W��()
        Dim dc As New DepartmentControler
        Dim dil As New List(Of DepartmentInfo)
        Dim i As Integer

        If lueFacID.EditValue = "*" Then
            dil = dc.BriName_GetList(strInDepID, Nothing, Nothing)
        Else
            dil = dc.BriName_GetList(strInDepID, Nothing, lueFacID.EditValue)
        End If
        If dil.Count <= 0 Then Exit Sub

        ds.Tables("TDep").Clear()

        If strInDepID = Nothing Then
            Dim row As DataRow
            row = ds.Tables("TDep").NewRow
            row("DepName") = "����"
            row("DepID") = "*"
            row("DepIDName") = "* ����"
            ds.Tables("TDep").Rows.Add(row)
        End If

        For i = 0 To dil.Count - 1
            Dim row1 As DataRow
            row1 = ds.Tables("TDep").NewRow
            row1("DepName") = dil(i).DepName
            row1("DepID") = dil(i).DepID
            row1("FacName") = dil(i).FacName
            row1("DepIDName") = dil(i).DepID & " " & dil(i).DepName
            row1("DepIDNameFacName") = dil(i).DepID & " " & dil(i).DepName & "" & dil(i).FacName
            ds.Tables("TDep").Rows.Add(row1)
        Next

    End Sub

    ''' <summary>
    ''' �۩w�q�d�߸��J�t�O
    ''' </summary>
    ''' <remarks></remarks>
    Sub Load_Dep1()
        ' �[���t�O�W��()
        Dim dc As New DepartmentControler
        Dim dil As New List(Of DepartmentInfo)
        Dim i As Integer

        dil = dc.BriName_GetList(Nothing, Nothing, strInFacID)

        For i = 0 To dil.Count - 1
            Dim row1 As DataRow
            row1 = ds.Tables("TDep1").NewRow
            row1("DepName") = dil(i).DepName
            row1("DepID") = dil(i).DepID
            row1("DepIDName") = dil(i).DepID & " " & dil(i).DepName
            row1("FacName") = dil(i).FacName
            ds.Tables("TDep1").Rows.Add(row1)
        Next

    End Sub
    ''' <summary>
    ''' ���J�f�֪��A
    ''' </summary>
    ''' <remarks></remarks>
    Sub load_Stata()
        Dim row As DataRow
        row = ds.Tables("State").NewRow
        row("StateID") = "Nothing"
        row("StateName") = "����"
        ds.Tables("State").Rows.Add(row)

        Dim row1 As DataRow
        row1 = ds.Tables("State").NewRow
        row1("StateID") = "False"
        row1("StateName") = "���f��"
        ds.Tables("State").Rows.Add(row1)

        Dim row2 As DataRow
        row2 = ds.Tables("State").NewRow
        row2("StateID") = "True"
        row2("StateName") = "�w�f��"
        ds.Tables("State").Rows.Add(row2)
    End Sub
    ''' <summary>
    ''' ���J����
    ''' </summary>
    ''' <remarks></remarks>
    Sub load_Pro_Type()
        Dim row As DataRow
        row = ds.Tables("TPro_Type").NewRow
        row("Pro_Type") = "�Ͳ��[�u"
        ds.Tables("TPro_Type").Rows.Add(row)

        Dim row1 As DataRow
        row1 = ds.Tables("TPro_Type").NewRow
        row1("Pro_Type") = "�˰t�X�f"
        ds.Tables("TPro_Type").Rows.Add(row1)

        Dim row2 As DataRow
        row2 = ds.Tables("TPro_Type").NewRow
        row2("Pro_Type") = "�F���[�u"
        ds.Tables("TPro_Type").Rows.Add(row2)
    End Sub
    ''' <summary>
    ''' �۩w�q�f��
    ''' </summary>
    ''' <remarks></remarks>
    Sub load_Bool()
        Dim row As DataRow
        row = ds.Tables("TBool").NewRow
        row("Bool") = "False"
        ds.Tables("TBool").Rows.Add(row)

        Dim row1 As DataRow
        row1 = ds.Tables("TBool").NewRow
        row1("Bool") = "True"
        ds.Tables("TBool").Rows.Add(row1)
    End Sub
    ''' <summary>
    ''' �ت��c
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTable()
        ds.Tables.Clear()

        With ds.Tables.Add("TFac")
            .Columns.Add("FacName", GetType(String))
            .Columns.Add("FacID", GetType(String))
            .Columns.Add("FacIDName", GetType(String))
        End With

        lueFacID.Properties.DisplayMember = "FacIDName"
        lueFacID.Properties.ValueMember = "FacID"
        lueFacID.Properties.DataSource = ds.Tables("TFac")

        With ds.Tables.Add("TDep")
            .Columns.Add("DepName", GetType(String))
            .Columns.Add("DepID", GetType(String))
            .Columns.Add("FacName", GetType(String))
            .Columns.Add("DepIDName", GetType(String))
            .Columns.Add("DepIDNameFacName", GetType(String))
        End With

        lueDepID.Properties.DisplayMember = "DepIDName"
        lueDepID.Properties.ValueMember = "DepID"
        lueDepID.Properties.DataSource = ds.Tables("TDep")


        'StateLookUp
        With ds.Tables.Add("State")
            .Columns.Add("StateID", GetType(String))
            .Columns.Add("StateName", GetType(String))
        End With

        StateLookUp.Properties.DisplayMember = "StateName"
        StateLookUp.Properties.ValueMember = "StateID"
        StateLookUp.Properties.DataSource = ds.Tables("State")


        With ds.Tables.Add("SelectTb")
            .Columns.Add("AutoID", GetType(Integer))
            .Columns.Add("Category", GetType(String))
            .Columns.Add("Type", GetType(String))
            .Columns.Add("FieldName", GetType(String))
        End With
        GridControl1.DataSource = ds.Tables("SelectTb")

        With ds.Tables.Add("SelectTbDel")
            .Columns.Add("DelConditon", GetType(String))
            .Columns.Add("DelDisplay", GetType(String))
            .Columns.Add("DelContent", GetType(String))
        End With
        GridControl2.DataSource = ds.Tables("SelectTbDel")


        With ds.Tables.Add("TPro_Type")
            .Columns.Add("Pro_Type", GetType(String))
        End With

        With ds.Tables.Add("TDep1")
            .Columns.Add("DepName", GetType(String))
            .Columns.Add("DepID", GetType(String)) '
            .Columns.Add("DepIDName", GetType(String))
            .Columns.Add("FacName", GetType(String))
        End With

        With ds.Tables.Add("TBool")
            .Columns.Add("Bool", GetType(String))
        End With

    End Sub

    Private Sub lueFacID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lueFacID.EditValueChanged
        Load_Dep()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged

        If RadioButton1.Checked = True Then
            ComboType.Enabled = True
            InputTextEdit.Enabled = True

            lueFacID.Enabled = False
            lueDepID.Enabled = False
            RadioButton2.Checked = False
            StateLookUp.Enabled = True
            cboPer_Class.Enabled = False
            ComboBoxReg.Enabled = False
        End If

    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged

        If RadioButton2.Checked = True Then
            ComboType.Enabled = False
            InputTextEdit.Enabled = False
            lueFacID.Enabled = True
            lueDepID.Enabled = True
            RadioButton1.Checked = False
            StateLookUp.Enabled = False
            cboPer_Class.Enabled = True

            ComboBoxReg.Enabled = True

        End If
    End Sub

    Private Sub ExitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitButton.Click
        tempValue = ""
        Me.Close()
    End Sub

    'tempValue �r�q�ǻ��ƾڻ���
    'tempValue 0---�۩w�q,1---�T�w�˦������������ܡ� 2---�T�w�˦��������t�O������
    '�T�w�˦�
    'tempValue2  �T�w�˦�����"���e" InputTextEdit.Text
    'tempValue3  �T�w�˦�����"������" ComboType.Text
    'tempValue4  �}�l�ɶ�  StartDateEdit.EditValue
    'tempValue5  �����ɶ�  EndDateEdit.EditValue
    'tempValue6  �f�֪��A  StateLookUp.EditValue
    '�۩w�q�˦�
    'tempValue2   �d��where


    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click

        If XtraTabControl1.SelectedTabPageIndex = 0 And RadioButton1.Checked = True Then '�T�w

            If InputTextEdit.Visible = False Then
                InputTextEdit.Text = GluG_NO.EditValue
            End If

            If InputTextEdit.Text = "" Then
                MsgBox("�п�J�d�ߤ��e")
                InputTextEdit.Select()
                Exit Sub
            End If
        End If

        If InStr(TempA, "�C�L", CompareMethod.Text) > 0 Then
            ProductionPiece_Print_case(TempA)
            ''----------------------------------------------------------------------------------------------------
        Else
            If XtraTabControl1.SelectedTabPageIndex = 0 Then '�T�w

                If RadioButton1.Checked = True Then ''(��ܼ˦�)
                    tempValue = 1
                    tempValue2 = InputTextEdit.Text
                    tempValue3 = ComboType.Text

                    If BZLabel.Text = "�~��" Then
                        If Date_YYMM.Visible = True Then tempValue4 = Format(CDate(Date_YYMM.EditValue), "yyyy/MM")
                        tempValue5 = Nothing
                        If StateLookUp.Visible = True Then tempValue6 = StateLookUp.EditValue
                    Else
                        If StartDateEdit.Visible = True Then tempValue4 = StartDateEdit.EditValue
                        If EndDateEdit.Visible = True Then tempValue5 = EndDateEdit.EditValue
                    End If
                ElseIf RadioButton2.Checked = True Then ''(�t�O����)

                    If ComboBoxReg.EditValue = "" Or ComboBoxReg.EditValue = "����" Then '�D�n�u�w��ӤH�~���Ҷ�
                        tempValue8 = Nothing
                    ElseIf ComboBoxReg.EditValue = "�b¾" Then
                        tempValue8 = "False"
                    Else
                        tempValue8 = "True"
                    End If

                    If Me.cboPer_Class.EditValue = "" Or Me.cboPer_Class.EditValue = "����" Then
                        tempValue7 = Nothing
                    Else
                        tempValue7 = Me.cboPer_Class.EditValue
                    End If

                    tempValue = 2
                    If lueDepID.EditValue = "*" Then
                        tempValue2 = Nothing
                    Else
                        tempValue2 = lueDepID.EditValue
                    End If

                    If BZLabel.Text = "�~��" Then
                        If Date_YYMM.Visible = True Then tempValue4 = Format(CDate(Date_YYMM.EditValue), "yyyy/MM")
                        tempValue5 = Nothing
                        If StateLookUp.Visible = True Then tempValue6 = StateLookUp.EditValue
                    Else
                        If StartDateEdit.Visible = True Then tempValue4 = StartDateEdit.EditValue
                        If EndDateEdit.Visible = True Then tempValue5 = EndDateEdit.EditValue
                    End If

                End If

            ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then '�۩w�q
                tempValue = "0"
                tempValue2 = ""

                For i As Integer = 0 To ds.Tables("SelectTbDel").Rows.Count - 1
                    tempValue2 = tempValue2 & ds.Tables("SelectTbDel").Rows(i)("DelConditon").ToString.Trim
                Next

                If tempValue2 = "" Then
                    tempValue = ""
                    MsgBox("�п�J�۩w�q�d�߱���!")
                    Exit Sub
                End If

                '�b�d�߮�,�έp�v���u��d�ߥ��������A�Ͳ������u��d�ݥ��Ͳ�����
                If InStr(tempValue2, "DepID", CompareMethod.Text) <= 0 And InStr(tempValue2, "DPT_ID", CompareMethod.Text) <= 0 And strInDepID <> Nothing Then '
                    If TempA = "�p��u��" Then
                        tempValue2 = tempValue2 + " and DPT_ID like '%" + strInDepID + "%'"
                    Else
                        tempValue2 = tempValue2 + " and DepID like '%" + strInDepID + "%'"
                    End If
                End If
            End If

            Me.Close()
        End If

    End Sub



    Function ProductionPiece_Print_case(ByVal _TempA As String) As String 'strInDepID
        ProductionPiece_Print_case = ""

        Dim strCompany As String
        strCompany = Mid(strInDPT_ID, 1, 4)   '��o�n���̩��ݤ��qID,�H��^���q�W�١ALOGO

        dsP.Clear()
        dsP.Tables.Clear()

        Dim _lueDepID As String
        Dim _lueFacID As String

        If lueDepID.EditValue = "*" Then
            _lueDepID = Nothing
        Else
            _lueDepID = lueDepID.EditValue
        End If

        If lueFacID.EditValue = "*" Then
            _lueFacID = Nothing
        Else
            _lueFacID = lueFacID.EditValue
        End If


        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim ltc5 As New CollectionToDataSet


        Select Case _TempA
            Case "�ӤH�p��έp�C�L���`�M��"
                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim PPP As New LFERP.Library.ProductionSumPiecePersonnel.ProductionSumPiecePersonnelControl
                Dim PPPS As New LFERP.Library.ProductionPieceProcess.ProductionPieceProcessControl

                dsP.Tables.Clear()

                'If RadioButton1.Checked = True Then
                '    If PPP.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, InputTextEdit.Text, Nothing, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, Nothing, Nothing, StartDateEdit.EditValue, Nothing, EndDateEdit.EditValue, UserName).Count <= 0 Then
                '        MsgBox("�L���u�p��O��!")
                '        Exit Function
                '    Else
                '        ltc1.CollToDataSet(dsP, "ProductionSumPiecePersonnel", PPP.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, InputTextEdit.Text, Nothing, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, Nothing, Nothing, StartDateEdit.EditValue, Nothing, EndDateEdit.EditValue, UserName))
                '    End If
                'Else
                '    If PPP.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, Nothing, Nothing, StartDateEdit.EditValue, Nothing, EndDateEdit.EditValue, UserName).Count <= 0 Then
                '        MsgBox("�L���u�p��O��!")
                '        Exit Function
                '    Else
                '        ltc1.CollToDataSet(dsP, "ProductionSumPiecePersonnel", PPP.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, Nothing, Nothing, StartDateEdit.EditValue, Nothing, EndDateEdit.EditValue, UserName))
                '    End If
                'End If
                'ltc2.CollToDataSet(dsP, "ProductionPieceProcess", PPPS.ProductionPieceProcess_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
                'ltc3.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))

                Dim da As SqlDataAdapter
                Dim cn As New LFERPDB.LFERPDataBase
                Dim strCn As New SqlConnection(cn.LoadConnStr)
                strCn.Open()

                Dim strSql As String = ""
                Dim wherestr As String = ""

                wherestr = " 1=1"
                If _lueFacID <> Nothing Then
                    wherestr = wherestr + " and FacID='" + _lueFacID + "'"
                End If

                If _lueDepID <> Nothing Then
                    wherestr = wherestr + " and DepID='" + _lueDepID + "'"
                End If

                Dim PP_DateStart As String
                Dim PP_DateEnd As String
                PP_DateStart = Format(CDate(StartDateEdit.EditValue), "yyyy-MM-dd")
                PP_DateEnd = Format(CDate(Me.EndDateEdit.EditValue), "yyyy-MM-dd")

                wherestr = wherestr + " and PP_Date >='" + PP_DateStart + "' and PP_Date <='" + PP_DateEnd + "' "

                Dim selectStr As String = ""
                selectStr = "'" & PP_DateStart & "' as PP_DateStart, '" & PP_DateEnd & "' as PP_DateEnd ,(SELECT Per_Class FROM ProductionPiecePersonnelMothClass WHERE (Per_NO = dbo.ViewSumPiecePersonnelColl1.Per_NO) AND (Per_Date = SUBSTRING(CONVERT(varchar(100),ViewSumPiecePersonnelColl1.PP_Date, 111), 1, 7))) AS Per_Class "
                Dim PrintS As String = ""

                If RadioButton1.Checked = True Then '��ӤH��
                    strSql = "select *, " & selectStr & " from ViewSumPiecePersonnelColl1 where per_no='" & InputTextEdit.Text & "' and " + wherestr
                Else
                    If cboPer_Class.Text = "����" Then
                        strSql = "select *," & selectStr & " from ViewSumPiecePersonnelColl1 where " + wherestr
                        PrintS = "(����)"
                    ElseIf cboPer_Class.Text = "�b��" Then
                        selectStr = "'" & PP_DateStart & "' as PP_DateStart, '" & PP_DateEnd & "' as PP_DateEnd ,(SELECT Per_Class FROM ProductionPiecePersonnelMothClass WHERE (Per_NO = dbo.ViewSumPiecePersonnelColl2.Per_NO) AND (Per_Date = SUBSTRING(CONVERT(varchar(100),ViewSumPiecePersonnelColl2.PP_Date, 111), 1, 7))) AS Per_Class "
                        strSql = "select *," & selectStr & " from ViewSumPiecePersonnelColl2  where " + wherestr + " and Per_Resign=0"
                        PrintS = "(�b��)"
                    ElseIf cboPer_Class.Text = "��u" Then
                        selectStr = "'" & PP_DateStart & "' as PP_DateStart, '" & PP_DateEnd & "' as PP_DateEnd ,(SELECT Per_Class FROM ProductionPiecePersonnelMothClass WHERE (Per_NO = dbo.ViewSumPiecePersonnelColl2.Per_NO) AND (Per_Date = SUBSTRING(CONVERT(varchar(100),ViewSumPiecePersonnelColl2.PP_Date, 111), 1, 7))) AS Per_Class "
                        strSql = "select *," & selectStr & " from ViewSumPiecePersonnelColl2  where " + wherestr + " and Per_Resign=1"
                        PrintS = "(��u)"
                    End If
                End If
              
                da = New SqlDataAdapter(strSql, strCn)
                da.Fill(dsP, "ProductionSumPiecePersonnel")
                strCn.Close()

                ltc1.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))

                PreviewRPTDialog1(dsP, "rptProductionPersonnelSumPieceTimeCollectList1", "�ӤH�p��έp�C�L���`�M��", strInUserRank, PrintS, True, True)


            Case "�էO�p��έp�C�L���`�M��"
                ''++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim PPG As New LFERP.Library.ProductionSumPieceWorkGroup.ProductionSumPieceWorkGroupControl
                Dim PPPS As New LFERP.Library.ProductionPieceProcess.ProductionPieceProcessControl

                dsP.Tables.Clear()

                If RadioButton1.Checked = True Then
                    If PPG.ProductionSumPieceWorkGroup_GetList(Nothing, Nothing, Nothing, InputTextEdit.Text, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, Nothing, Nothing, StartDateEdit.EditValue, Nothing, EndDateEdit.EditValue, UserName).Count <= 0 Then
                        MsgBox("�L�էO�p��O��!")
                        Exit Function
                    Else
                        ltc1.CollToDataSet(dsP, "ProductionSumPieceWorkGroup", PPG.ProductionSumPieceWorkGroup_GetList(Nothing, Nothing, Nothing, InputTextEdit.Text, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, Nothing, Nothing, Format(CDate(StartDateEdit.EditValue), "yyyy/MM/dd"), Nothing, Format(CDate(EndDateEdit.EditValue), "yyyy/MM/dd"), UserName))
                    End If
                Else
                    If PPG.ProductionSumPieceWorkGroup_GetList(Nothing, Nothing, Nothing, Nothing, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, Nothing, Nothing, StartDateEdit.EditValue, Nothing, EndDateEdit.EditValue, UserName).Count <= 0 Then
                        MsgBox("�L�էO�p��O��!")
                        Exit Function
                    Else
                        ltc1.CollToDataSet(dsP, "ProductionSumPieceWorkGroup", PPG.ProductionSumPieceWorkGroup_GetList(Nothing, Nothing, Nothing, Nothing, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, Nothing, Nothing, Format(CDate(StartDateEdit.EditValue), "yyyy/MM/dd"), Nothing, Format(CDate(EndDateEdit.EditValue), "yyyy/MM/dd"), UserName))
                    End If
                End If

                ltc2.CollToDataSet(dsP, "ProductionPieceProcess", PPPS.ProductionPieceProcess_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
                ltc3.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
                PreviewRPTDialog1(dsP, "rptProductionWorkGroupSumPieceTimeCollectList", "�էO�p��έp�C�L���`�M��", strInUserRank, strInUserRank, True, True)

            Case "�ӤH�p��έp�C�L", "�ӤH�p��έp�C�L���`", "�ӤH�p�ɲέp�C�L", "�ӤH�p�ɲέp�C�L���`"
                ''++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim PPP As New LFERP.Library.ProductionSumPiecePersonnel.ProductionSumPiecePersonnelControl
                Dim PPT As New LFERP.Library.ProductionSumTimePersonnel.ProductionSumTimePersonnelControl
                Dim PPPS As New LFERP.Library.ProductionPieceProcess.ProductionPieceProcessControl
                Dim PPPa As New LFERP.Library.ProductionPiecePersonnel.ProductionPiecePersonnelControl
                Dim Pppe As New LFERP.Library.ProductionPiecePersonnel.ProductionPiecePersonnelControl
                Dim PppeL As New List(Of LFERP.Library.ProductionPiecePersonnel.ProductionPiecePersonnelInfo)

                PppeL = Pppe.ProductionPiecePersonnel_GetList(Nothing, InputTextEdit.Text.Trim, Nothing, Nothing, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                If PppeL.Count <= 0 Then
                    MsgBox("�����u���b��e����!")
                    Exit Function
                End If

             
                If PPP.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, InputTextEdit.Text.Trim, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Format(CDate(StartDateEdit.EditValue), "yyyy/MM/dd"), Nothing, Format(CDate(EndDateEdit.EditValue), "yyyy/MM/dd"), UserName).Count <= 0 Then
                    ltc1.CollToDataSet(dsP, "ProductionSumPiecePersonnel", PPP.NothingNew)

                    Dim PPPSL As New List(Of LFERP.Library.ProductionPieceProcess.ProductionPieceProcessInfo)
                    PPPSL = PPPS.ProductionPieceProcess_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                    dsP.Tables("ProductionSumPiecePersonnel").Rows(0)("Per_NO") = InputTextEdit.Text
                    dsP.Tables("ProductionSumPiecePersonnel").Rows(0)("PP_Per_Name") = PppeL(0).Per_Name
                    dsP.Tables("ProductionSumPiecePersonnel").Rows(0)("PP_DateEnd") = Format(CDate(EndDateEdit.EditValue), "yyyy/MM/dd")
                    dsP.Tables("ProductionSumPiecePersonnel").Rows(0)("PP_DateStart") = Format(CDate(StartDateEdit.EditValue), "yyyy/MM/dd")
                    dsP.Tables("ProductionSumPiecePersonnel").Rows(0)("PP_AutoID") = PPPSL(0).AutoID
                    dsP.Tables("ProductionSumPiecePersonnel").Rows(0)("DepID") = ""
                    dsP.Tables("ProductionSumPiecePersonnel").Rows(0)("FacID") = ""

                Else
                    ltc1.CollToDataSet(dsP, "ProductionSumPiecePersonnel", PPP.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, InputTextEdit.Text.Trim, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Format(CDate(StartDateEdit.EditValue), "yyyy/MM/dd"), Nothing, Format(CDate(EndDateEdit.EditValue), "yyyy/MM/dd"), UserName))
                End If


                If PPT.ProductionSumTimePersonnel_GetList(Nothing, InputTextEdit.Text.Trim, Nothing, Nothing, Nothing, Format(CDate(StartDateEdit.EditValue), "yyyy/MM/dd"), Nothing, Format(CDate(EndDateEdit.EditValue), "yyyy/MM/dd"), Nothing, Nothing, Nothing).Count <= 0 Then
                    ltc2.CollToDataSet(dsP, "ProductionSumTimePersonnel", PPT.NothingNew)
                Else
                    ltc2.CollToDataSet(dsP, "ProductionSumTimePersonnel", PPT.ProductionSumTimePersonnel_GetList(Nothing, InputTextEdit.Text.Trim, Nothing, Nothing, Nothing, Format(CDate(StartDateEdit.EditValue), "yyyy/MM/dd"), Nothing, Format(CDate(EndDateEdit.EditValue), "yyyy/MM/dd"), Nothing, Nothing, Nothing))
                End If

                ltc3.CollToDataSet(dsP, "ProductionPieceProcess", PPPS.ProductionPieceProcess_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
                ltc4.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
                ltc5.CollToDataSet(dsP, "ProductionPiecePersonnel", PPPa.ProductionPiecePersonnel_GetList(Nothing, InputTextEdit.Text.Trim, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))


                If _TempA = "�ӤH�p��έp�C�L" Or _TempA = "�ӤH�p�ɲέp�C�L" Then
                    PreviewRPTDialog1(dsP, "rptProductionPersonnelSumPieceTime", "�ӤH�p����L", strInUserRank, strInUserRank, True, True)
                ElseIf _TempA = "�ӤH�p��έp�C�L���`" Or _TempA = "�ӤH�p�ɲέp�C�L���`" Then
                    PreviewRPTDialog1(dsP, "rptProductionPersonnelSumPieceTimeCollect", "�ӤH�p����`���L", strInUserRank, strInUserRank, True, True)
                End If

            Case "�էO�p��έp�C�L", "�էO�p��έp�C�L���`", "�էO�p�ɲέp�C�L", "�էO�p�ɲέp�C�L���`"
                ''++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim PPG As New LFERP.Library.ProductionSumPieceWorkGroup.ProductionSumPieceWorkGroupControl
                Dim PTG As New LFERP.Library.ProductionSumTimeWorkGroup.ProductionSumTimeWorkGroupControl
                Dim PPPS As New LFERP.Library.ProductionPieceProcess.ProductionPieceProcessControl
                Dim PPPa As New LFERP.Library.ProductionPiecePersonnel.ProductionPiecePersonnelControl

                dsP.Tables.Clear()
                If PPG.ProductionSumPieceWorkGroup_GetList(Nothing, Nothing, Nothing, InputTextEdit.Text, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, Nothing, Nothing, Format(CDate(StartDateEdit.EditValue), "yyyy/MM/dd"), Nothing, Format(CDate(EndDateEdit.EditValue), "yyyy/MM/dd"), UserName).Count <= 0 Then
                    ltc1.CollToDataSet(dsP, "ProductionSumPieceWorkGroup", PPG.NothingNew)

                Else
                    ltc1.CollToDataSet(dsP, "ProductionSumPieceWorkGroup", PPG.ProductionSumPieceWorkGroup_GetList(Nothing, Nothing, Nothing, InputTextEdit.Text, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, Nothing, Nothing, Format(CDate(StartDateEdit.EditValue), "yyyy/MM/dd"), Nothing, Format(CDate(EndDateEdit.EditValue), "yyyy/MM/dd"), UserName))
                End If

                If PTG.ProductionSumTimeWorkGroup_GetList(Nothing, Nothing, InputTextEdit.Text, _lueDepID, _lueFacID, StartDateEdit.EditValue, Nothing, EndDateEdit.EditValue, Nothing, Nothing).Count <= 0 Then
                    ltc2.CollToDataSet(dsP, "ProductionSumTimeWorkGroup", PTG.NothingNew)
                Else
                    ltc2.CollToDataSet(dsP, "ProductionSumTimeWorkGroup", PTG.ProductionSumTimeWorkGroup_GetList(Nothing, Nothing, InputTextEdit.Text, _lueDepID, _lueFacID, Format(CDate(StartDateEdit.EditValue), "yyyy/MM/dd"), Nothing, Format(CDate(EndDateEdit.EditValue), "yyyy/MM/dd"), Nothing, Nothing))
                End If

                ltc3.CollToDataSet(dsP, "ProductionPieceProcess", PPPS.ProductionPieceProcess_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
                ltc4.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
                ltc5.CollToDataSet(dsP, "ProductionPiecePersonnel", PPPa.ProductionPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))

                If _TempA = "�էO�p��έp�C�L" Or _TempA = "�էO�p�ɲέp�C�L" Then
                    PreviewRPTDialog1(dsP, "rptProductionWorkGroupSumPieceTime", "�ӥ]�p����L", strInUserRank, strInUserRank, True, True)
                ElseIf _TempA = "�էO�p��έp�C�L���`" Or _TempA = "�էO�p�ɲέp�C�L���`" Then
                    PreviewRPTDialog1(dsP, "rptProductionWorkGroupSumPieceTimeCollect", "�ӥ]�p����`���L", strInUserRank, strInUserRank, True, True)
                End If

            Case "�ӤH�p���~���C�L"
                ''++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                'Dim mcCompany As New LFERP.DataSetting.CompanyControler
                'Dim Ppp As New LFERP.Library.ProductionPiecePayPersonnel.ProductionPiecePayPersonnelContol

                'ltc1.CollToDataSet(ds, "ProductionPiecePayPersonnel", Ppp.ProductionPiecePayPersonnel_GetList(Nothing, InputTextEdit.Text, Nothing, Format(Date_YYMM.EditValue, "yyyy/MM"), _lueDepID, StateLookUp.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _lueFacID))
                'ltc2.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
                'PreviewRPT(ds, "RPTProductionPiecePayPersonnel", "�ӤH�p��u��", True, True)
                Dim strComboBoxReg As String
                Dim strcboPer_Class As String

                If ComboBoxReg.EditValue = "" Or ComboBoxReg.EditValue = "����" Then '�D�n�u�w��ӤH�~���Ҷ�
                    strComboBoxReg = Nothing
                ElseIf ComboBoxReg.EditValue = "�b¾" Then
                    strComboBoxReg = "False"
                Else
                    strComboBoxReg = "True"
                End If

                If Me.cboPer_Class.EditValue = "" Or Me.cboPer_Class.EditValue = "����" Then
                    strcboPer_Class = Nothing
                Else
                    strcboPer_Class = Me.cboPer_Class.EditValue
                End If


                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim Ppp As New LFERP.Library.ProductionPiecePayPersonnel.ProductionPiecePayPersonnelContol

                If RadioButton1.Checked = True Then
                    ltc1.CollToDataSet(dsP, "ProductionPiecePayPersonnel", Ppp.ProductionPiecePayPersonnel_GetList(Nothing, InputTextEdit.Text, Nothing, Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), _lueDepID, StateLookUp.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _lueFacID, Nothing))
                Else
                    ltc1.CollToDataSet(dsP, "ProductionPiecePayPersonnel", Ppp.ProductionPiecePayPersonnel_GetList1(Nothing, Nothing, Nothing, Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), _lueDepID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _lueFacID, strcboPer_Class, strComboBoxReg))
                End If
                ltc2.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
                PreviewRPTDialog(dsP, "RPTProductionPiecePayPersonnel", "�ӤH�p��u��", True, True)

            Case "�ӤH�p���~�����`�C�L"
                ''++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim payp As New LFERP.Library.ProductionPayPersonnel.ProductionPayPersonnelContol
                Dim pppayp As New LFERP.Library.ProductionPiecePayPersonnel.ProductionPiecePayPersonnelContol
                Dim ppwgMain As New LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainControl
                Dim ppwgSub As New LFERP.Library.ProductionPiecePayWGSub.ProductionPiecePayWGSubControl

                If payp.ProductionPieceMeritedPaySum_View(Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), _lueDepID, _lueFacID, InputTextEdit.Text, StateLookUp.EditValue).Count <= 0 Then
                    MsgBox("���w���󤺵L�O���s�b!")
                    Exit Function
                End If

                'If ppwgSub.ProductionPiecePayWGSub_GetList(Nothing, Nothing, InputTextEdit.Text, Nothing).Count <= 0 Or _
                'pppayp.ProductionPiecePayPersonnel_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, StateLookUp.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing).Count <= 0 Then

                '    MsgBox("�L�O���s�b.")
                '    Exit Function
                'End If

                ltc1.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
                ltc2.CollToDataSet(dsP, "ProductionPayPersonnel", payp.ProductionPieceMeritedPaySum_View(Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), _lueDepID, _lueFacID, InputTextEdit.Text, StateLookUp.EditValue))
                ltc3.CollToDataSet(dsP, "ProductionPiecePayPersonnel", pppayp.ProductionPiecePayPersonnel_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, StateLookUp.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))

                ltc4.CollToDataSet(dsP, "ProductionPiecePayWGMain", ppwgMain.ProductionPiecePayWGMain_GetList(Nothing, Nothing, Nothing, Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, StateLookUp.EditValue))
                ltc5.CollToDataSet(dsP, "ProductionPiecePayWGSub", ppwgSub.ProductionPiecePayWGSub_GetList(Nothing, Nothing, InputTextEdit.Text, Nothing))

                PreviewRPTDialog(dsP, "rptProductionPayPersonnel", "�ӤH�p��u��", True, True)

            Case "�էO�p���~���C�L"
                ''++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim WGppM As New LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainControl
                Dim WGppS As New LFERP.Library.ProductionPiecePayWGSub.ProductionPiecePayWGSubControl

                If RadioButton1.Checked = True Then
                    ltc1.CollToDataSet(dsP, "ProductionPiecePayWGMain", WGppM.ProductionPiecePayWGMain_GetList(Nothing, Nothing, InputTextEdit.Text, Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, StateLookUp.EditValue))
                Else
                    ltc1.CollToDataSet(dsP, "ProductionPiecePayWGMain", WGppM.ProductionPiecePayWGMain_GetList(Nothing, Nothing, Nothing, Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), Nothing, Nothing, _lueDepID, _lueFacID, Nothing, Nothing, Nothing))
                End If

                ltc2.CollToDataSet(dsP, "ProductionPiecePayWGSub", WGppS.ProductionPiecePayWGSub_GetList(Nothing, Nothing, Nothing, Nothing))
                ltc3.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
                PreviewRPTDialog(dsP, "rptProductionPiecePayWG", "�էO�p��u��", True, True)


                '2013 -4-1
            Case "�ӤH��p����`�C�L"


                Dim StrFacName As String = ""
                Dim StrDepName As String = ""

                Dim strSql As String
                Dim myConn As New SqlConnection(KaoQinConn)
                Dim da As SqlDataAdapter

                Dim cn As New LFERPDB.LFERPDataBase
                Dim strCn As New SqlConnection(cn.LoadConnStr)

                Dim strStarEndDate As String
                Dim strStat_Date As Date
                Dim strEnd_Date As Date

                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim PPP As New LFERP.Library.ProductionSumPiecePersonnel.ProductionSumPiecePersonnelControl
                Dim PPT As New LFERP.Library.ProductionSumTimePersonnel.ProductionSumTimePersonnelControl


                Dim oc1 As New ProductionPiecePersonnelControl

                Dim ocl1 As New List(Of ProductionPiecePersonnelInfo)
                ocl1 = oc1.ProductionPiecePersonnel_GetList(Nothing, InputTextEdit.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If ocl1.Count <= 0 Then
                    MsgBox("�����u�b�p��H���W�椤���s�b!")
                    Exit Function
                End If

                StrFacName = ocl1(0).Per_FacName
                StrDepName = ocl1(0).Per_DepName

                strStat_Date = Format(CDate(Date_YYMM.EditValue), "yyyy-MM") & "-1"
                strEnd_Date = DateAdd("d", -1, DateAdd("M", 1, strStat_Date))
                strStarEndDate = "BETWEEN '" & strStat_Date & "' AND '" & strEnd_Date & "'"

                myConn.Open()

                dsP.Tables.Clear()
                strSql = "SELECT * FROM  vwCheckin WHERE BadgeID ='" & InputTextEdit.Text & "'  AND " _
                        & " CheckInDate " & strStarEndDate & " ORDER BY BadgeID,CheckInDate"
                da = New SqlDataAdapter(strSql, myConn)
                da.Fill(dsP, "vwCheckin")

                If dsP.Tables("vwCheckin").Rows.Count <= 0 Then
                    MsgBox(Format(CDate(Date_YYMM.EditValue), "yyyy-MM") & " ," & InputTextEdit.Text & "  �Ҷ԰O�����s�b!")
                    Exit Function
                End If

                strSql = "SELECT * FROM  KQDetail WHERE BadgeID ='" & InputTextEdit.Text & "'  AND " _
                        & " KQDate " & strStarEndDate & " ORDER BY BadgeID,KQDate"
                da = New SqlDataAdapter(strSql, myConn)
                da.Fill(dsP, "KQDetail")
                myConn.Close()


                strCn.Open()

                'sum(PT_Total * SampPrice) 

                'strSql = "select pt.per_no ,pt_date ,SUM(pt_total) as pt_total,SUM(pt_total*per_dayprice/8) as pt_sum " & _
                '              "from ProductionSumTimePersonnel pt inner join ProductionPiecePersonnel pp on pt.Per_NO =pp.Per_NO " & _
                '              "group by pt.per_no,pt_date having pt_date " & strStarEndDate & " and pt.per_no='" & InputTextEdit.Text & "'"

                strSql = "select pt.per_no ,pt_date ,SUM(pt_total) as pt_total,sum(PT_Total * SampPrice)  as pt_sum " & _
                             "from ProductionSumTimePersonnel pt inner join ProductionPiecePersonnel pp on pt.Per_NO =pp.Per_NO " & _
                             "group by pt.per_no,pt_date having pt_date " & strStarEndDate & " and pt.per_no='" & InputTextEdit.Text & "'"

                da = New SqlDataAdapter(strSql, strCn)
                da.Fill(dsP, "ProductionSumTimePersonnel")

                strSql = "select per_no,pp_date,SUM(pp_qty) as pp_qty,SUM(pp_qty*pp_price) as pp_sum from ViewSumPiecePersonnel" & _
                             " group by per_no,pp_date having pp_date " & strStarEndDate & " and per_no='" & InputTextEdit.Text & "'"

                da = New SqlDataAdapter(strSql, strCn)
                da.Fill(dsP, "ProductionSumPiecePersonnel")

                strCn.Close()

                ltc3.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strInCompany, Nothing, Nothing, Nothing))
                PreviewRPTDialog1(dsP, "rptProductionPiecePersonnelDaySum", "�ӤH��p����`��", StrFacName, StrDepName, True, True)
                ltc3 = Nothing


            Case "�էO�p�����`�C�L"

              

                Dim strSql As String
                Dim myConn As New SqlConnection(KaoQinConn)
                Dim da As SqlDataAdapter

                Dim cn As New LFERPDB.LFERPDataBase
                Dim strCn As New SqlConnection(cn.LoadConnStr)

                Dim strStat_Date As Date
                Dim strEnd_Date As Date

                Dim strG_NO As String

                strG_NO = InputTextEdit.Text
                strStat_Date = Format(CDate(StartDateEdit.EditValue), "yyyy-MM-dd")
                strEnd_Date = Format(CDate(StartDateEdit.EditValue), "yyyy-MM-dd")

                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim oc As New ProductionPiecePersonnelDayControl
                Dim PPP As New LFERP.Library.ProductionSumPiecePersonnel.ProductionSumPiecePersonnelControl
                Dim PPT As New LFERP.Library.ProductionSumTimePersonnel.ProductionSumTimePersonnelControl


                If oc.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, Nothing, Nothing, strG_NO, Nothing, Nothing, Nothing, strStat_Date, Nothing, Nothing, strEnd_Date, Nothing).Count <= 0 Then
                    MsgBox("���էO" & strStat_Date & "�L�C��H���W��!")
                    Exit Function
                End If

                strStat_Date = Format(CDate(StartDateEdit.EditValue), "yyyy-MM-dd")
                strEnd_Date = Format(CDate(StartDateEdit.EditValue), "yyyy-MM-dd")

                myConn.Open()

                dsP.Tables.Clear()
                strSql = "SELECT * FROM  vwCheckin WHERE CheckInDate ='" & strStat_Date & "'   "
                da = New SqlDataAdapter(strSql, myConn)
                da.Fill(dsP, "vwCheckin")

                strSql = "SELECT * FROM  KQDetail WHERE KQDate ='" & strStat_Date & "' "
                da = New SqlDataAdapter(strSql, myConn)
                da.Fill(dsP, "KQDetail")
                myConn.Close()


                strCn.Open()

                'sum(PT_Total * SampPrice) 

                'strSql = "select SUM (GT_Total ) as GT_Total,SUM(GT_Total*per_dayprice/8) as GT_sum from ProductionSumTimeWorkGroup pt  "
                strSql = "select SUM (GT_Total ) as GT_Total,sum(GT_Total * SampPrice)  as GT_sum from ProductionSumTimeWorkGroup pt  "
                strSql = strSql + "inner join ProductionPiecePersonnel pp on pt.Per_NO =pp.Per_NO  where pt.G_NO ='" + strG_NO + "' and pt.GT_Date ='" + strStat_Date + "' "

                da = New SqlDataAdapter(strSql, strCn)
                da.Fill(dsP, "ProductionSumTimeWG")

                strSql = "select SUM (GP_Qty) as GP_Qty,SUM (GP_Qty * PP_Price* GP_factor  ) as GP_Sum  from ViewSumPieceWorkGroup where GP_Date ='" + strStat_Date + "' and G_NO ='" + strG_NO + "'"

                da = New SqlDataAdapter(strSql, strCn)
                da.Fill(dsP, "ProductionSumPieceWG")

                strSql = "SELECT G_NO,per_no,(select G_Name from ProductionPieceWorkGroup where G_NO =ProductionPiecePersonnelDay.G_NO)  as Per_G_Name FROM ProductionPiecePersonnelDay  where  Per_Date <='" + strEnd_Date + "' and  Per_Date >='" + strStat_Date + "' and G_NO not in ('" + strG_NO + "')"

                da = New SqlDataAdapter(strSql, strCn)
                da.Fill(dsP, "ProductionPiecePersonnelDayOther")

                strCn.Close()

                ltc1.CollToDataSet(dsP, "ProductionPiecePersonnelDay", oc.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, Nothing, Nothing, strG_NO, Nothing, Nothing, Nothing, strStat_Date, Nothing, Nothing, strEnd_Date, Nothing))
                ltc2.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strInCompany, Nothing, Nothing, Nothing))


                PreviewRPTDialog1(dsP, "rptProductionWorkGroupPieceDay", "�էO�p�����`��", Format(CDate(strStat_Date), "yyyy�~MM��dd��"), "", True, True)

                ltc1 = Nothing
                ltc2 = Nothing

                '�էO�p��ӤH���ӦC�L
            Case "�էO�p��ӤH���ӦC�L"




                Dim strSql As String
                Dim myConn As New SqlConnection(KaoQinConn)
                Dim da As SqlDataAdapter

                Dim cn As New LFERPDB.LFERPDataBase
                Dim strCn As New SqlConnection(cn.LoadConnStr)

                Dim strStarEndDate As String
                Dim strStat_Date As Date
                Dim strEnd_Date As Date

                Dim strG_NO As String
                Dim strPerNO As String
                Dim strYYYYMM As String

                strPerNO = InputTextEdit.Text
                strYYYYMM = Format(CDate(Date_YYMM.EditValue), "yyyy-MM")

                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim oc As New ProductionPiecePersonnelDayControl
                Dim PPP As New LFERP.Library.ProductionSumPiecePersonnel.ProductionSumPiecePersonnelControl
                Dim PPT As New LFERP.Library.ProductionSumTimePersonnel.ProductionSumTimePersonnelControl
                Dim oc1 As New ProductionPiecePersonnelControl

                Dim ocl1 As New List(Of ProductionPiecePersonnelInfo)
                ocl1 = oc1.ProductionPiecePersonnel_GetList(Nothing, strPerNO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                If ocl1.Count <= 0 Then
                    MsgBox("�����u�s�����s�b!")
                    Exit Function
                Else
                    strG_NO = ocl1(0).G_NO
                    If strG_NO = "�L" Then
                        MsgBox("�����u���ӤH�p��W��!")
                        Exit Function
                    End If
                End If
                ''--------------------------------------------------------------------------

                strStat_Date = Format(CDate(strYYYYMM), "yyyy-MM") & "-1"
                strEnd_Date = DateAdd("d", -1, DateAdd("M", 1, strStat_Date))
                strStarEndDate = "BETWEEN '" & strStat_Date & "' AND '" & strEnd_Date & "'"

                myConn.Open()

                dsP.Tables.Clear()
                strSql = "SELECT * FROM  vwCheckin WHERE BadgeID ='" & strPerNO & "'  AND " _
                        & " CheckInDate " & strStarEndDate & " ORDER BY BadgeID,CheckInDate"
                da = New SqlDataAdapter(strSql, myConn)
                da.Fill(dsP, "vwCheckin")

                If dsP.Tables("vwCheckin").Rows.Count <= 0 Then
                    MsgBox(strYYYYMM & " ," & strPerNO & "  �Ҷ԰O�����s�b!")
                    Exit Function
                End If

                strSql = "SELECT * FROM  KQDetail WHERE BadgeID ='" & strPerNO & "'  AND " _
                        & " KQDate " & strStarEndDate & " ORDER BY BadgeID,KQDate"
                da = New SqlDataAdapter(strSql, myConn)
                da.Fill(dsP, "KQDetail")
                myConn.Close()

                strCn.Open()
                'sum(PT_Total * SampPrice) 
                'strSql = "select SUM (GT_Total ) as GT_Total,SUM(GT_Total*per_dayprice/8) as GT_sum from ProductionSumTimeWorkGroup pt  "
                strSql = "select SUM (GT_Total ) as GT_Total,sum(GT_Total * SampPrice)  as GT_sum from ProductionSumTimeWorkGroup pt  "
                strSql = strSql + "inner join ProductionPiecePersonnel pp on pt.Per_NO =pp.Per_NO  where pt.G_NO ='" + strG_NO + "' and pt.GT_Date " + strStarEndDate

                da = New SqlDataAdapter(strSql, strCn)
                da.Fill(dsP, "ProductionSumTimeWG")

                strSql = "select SUM (GP_Qty) as GP_Qty,SUM (GP_Qty * PP_Price* GP_factor ) as GP_Sum  from ViewSumPieceWorkGroup where GP_Date " + strStarEndDate + " and G_NO ='" + strG_NO + "'"

                da = New SqlDataAdapter(strSql, strCn)
                da.Fill(dsP, "ProductionSumPieceWG")

                strSql = "SELECT G_NO,per_no,(select G_Name from ProductionPieceWorkGroup where G_NO =ProductionPiecePersonnelDay.G_NO)  as Per_G_Name,CONVERT(varchar(100), Per_Date, 111) AS Per_Date "
                strSql = strSql + "FROM ProductionPiecePersonnelDay  where per_no= '" + strPerNO + "' and Per_Date <='" + strEnd_Date + "' and  Per_Date >='" + strStat_Date + "' and G_NO not in ('" + strG_NO + "')"

                da = New SqlDataAdapter(strSql, strCn)
                da.Fill(dsP, "ProductionPiecePersonnelDayOther")

                strCn.Close()

                ltc1.CollToDataSet(dsP, "ProductionPiecePersonnel", oc1.ProductionPiecePersonnel_GetList(Nothing, strPerNO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
                ltc2.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strInCompany, Nothing, Nothing, Nothing))

                PreviewRPTDialog1(dsP, "rptProductionWorkGroupPiecePersonelDay", "�էO�p����u����`��", Format(CDate(strStat_Date), "yyyy�~MM��"), "", True, True)

            Case "�C�L���f�֤u��"

                Dim ds As New DataSet
                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim ppc As New LFERP.Library.ProductionPieceProcess.ProductionPieceProcessControl

                If ppc.ProductionPieceProcess_GetList2(Nothing, Nothing, Nothing, _lueDepID, Nothing, "True", Format(CDate(StartDateEdit.EditValue), "yyyy-MM-dd"), Format(CDate(EndDateEdit.EditValue), "yyyy-MM-dd")).Count <= 0 Then
                    MsgBox("��e�ɶ��q,��w�����L�u�ǻݼf��!")
                    Exit Function
                End If
                ltc1.CollToDataSet(dsP, "ProductionPieceProcess", ppc.ProductionPieceProcess_GetList2(Nothing, Nothing, Nothing, _lueDepID, Nothing, "True", Format(CDate(StartDateEdit.EditValue), "yyyy-MM-dd"), Format(CDate(EndDateEdit.EditValue), "yyyy-MM-dd")))
                ltc2.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))

                PreviewRPTDialog1(dsP, "rptPieceProcessNOCheck", "���f�֤u���C��", Format(CDate(StartDateEdit.EditValue), "yyyy-MM-dd"), Format(CDate(EndDateEdit.EditValue), "yyyy-MM-dd"), True, True)

                ''�ӤH�p�����`(����)
            Case "�ӤH��p����`�����C�L"

                Dim strSql As String
                Dim myConn As New SqlConnection(KaoQinConn)
                Dim da As SqlDataAdapter

                Dim cn As New LFERPDB.LFERPDataBase
                Dim strCn As New SqlConnection(cn.LoadConnStr)

                Dim strStat_Date As Date
                Dim strEnd_Date As Date

                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim PPP As New LFERP.Library.ProductionSumPiecePersonnel.ProductionSumPiecePersonnelControl
                Dim PPT As New LFERP.Library.ProductionSumTimePersonnel.ProductionSumTimePersonnelControl


                Dim oc1 As New ProductionPiecePersonnelControl
                Dim ocl1 As New List(Of ProductionPiecePersonnelInfo)
                ocl1 = oc1.ProductionPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If ocl1.Count <= 0 Then
                    MsgBox("�p��H���W�椤���s�b!")
                    Exit Function
                End If


                Dim i As Integer
                Dim whereAA As String = ""

                For i = 0 To ocl1.Count - 1
                    If i = 0 Then
                        whereAA = "'" + ocl1(i).Per_NO + "'"
                    Else
                        whereAA = whereAA + "," + "'" + ocl1(i).Per_NO + "'"
                    End If
                Next
                whereAA = " and badgeid in  (" + whereAA + " )"

                strStat_Date = Format(CDate(StartDateEdit.EditValue), "yyyy-MM-dd")
                strEnd_Date = Format(CDate(StartDateEdit.EditValue), "yyyy-MM-dd")

                myConn.Open()

                dsP.Tables.Clear()
                strSql = "SELECT * FROM  vwCheckin WHERE CheckInDate ='" & strStat_Date & "'" + whereAA
                da = New SqlDataAdapter(strSql, myConn)
                da.Fill(dsP, "vwCheckin")

                ' MsgBox(ds.Tables("vwCheckin").Rows.Count)

                strSql = "SELECT * FROM  KQDetail WHERE KQDate ='" & strStat_Date & "' " + whereAA
                da = New SqlDataAdapter(strSql, myConn)
                da.Fill(dsP, "KQDetail")
                myConn.Close()

                ' MsgBox(ds.Tables("KQDetail").Rows.Count)  sum(PT_Total * SampPrice) 

                strCn.Open()

                strSql = "select per_no,SUM(pt_total) as pt_total,sum(PT_Total * SampPrice)  as pt_sum from ViewSumTimePersonnel where pt_date ='" & strStat_Date & "'  group by Per_NO   "
                da = New SqlDataAdapter(strSql, strCn)
                da.Fill(dsP, "ProductionSumTimePersonnel")
                ''--------------------------------------
                strSql = "select per_no,SUM(pp_qty) as pp_qty,SUM(pp_qty*pp_price) as pp_sum from ViewSumPiecePersonnel where pp_date ='" & strStat_Date & "' group by Per_NO "
                da = New SqlDataAdapter(strSql, strCn)
                da.Fill(dsP, "ProductionSumPiecePersonnel")

                strCn.Close()

                ltc1.CollToDataSet(dsP, "ProductionPiecePersonnel", oc1.ProductionPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, _lueDepID, _lueFacID, Nothing, Nothing, Nothing, False, Nothing, Nothing))
                ltc2.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strInCompany, Nothing, Nothing, Nothing))
                PreviewRPTDialog1(dsP, "rptProductionPiecePersonnelDaySumDep", "�ӤH��p����`�����C�L", Format(CDate(StartDateEdit.EditValue), "yyyy�~MM��dd"), "", True, True)


            Case "�էO�W����`�C�L"

                Dim strSql As String
                Dim da As SqlDataAdapter
                Dim cn As New LFERPDB.LFERPDataBase
                Dim strCn As New SqlConnection(cn.LoadConnStr)


                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim ppp As New LFERP.Library.ProductionPiecePersonnel.ProductionPiecePersonnelControl
                Dim pwg As New ProductionPieceWorkGroupControl

                dsP.Tables.Clear()

                Dim strStat_Date, strEnd_Date, strStarEndDate As String
                strStat_Date = Format(CDate(Date_YYMM.EditValue), "yyyy-MM") & "-1"
                strEnd_Date = DateAdd("d", -1, DateAdd("M", 1, strStat_Date))


                If RadioButton1.Checked = True Then
                    ltc1.CollToDataSet(dsP, "ProductionPieceWorkGroup", pwg.ProductionPieceWorkGroup_GetList(InputTextEdit.Text, Nothing, Nothing, _lueDepID, _lueFacID, Nothing, Nothing, Nothing))
                    strStarEndDate = " where Per_Date >= '" & strStat_Date & "' AND Per_Date <= '" & strEnd_Date & "' and G_NO='" + InputTextEdit.Text + "'"
                Else
                    ltc1.CollToDataSet(dsP, "ProductionPieceWorkGroup", pwg.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, _lueDepID, _lueFacID, Nothing, Nothing, Nothing))
                    strStarEndDate = " where Per_Date >= '" & strStat_Date & "' AND Per_Date <= '" & strEnd_Date & "'"
                End If

                strCn.Open()

                strSql = " select distinct G_NO,Per_NO,"
                strSql = strSql + " (select Per_Name  from ViewProductionPiecePersonlWorkGroup where Per_NO =ProductionPiecePersonnelDay.Per_NO ) as Per_Name,"
                strSql = strSql + " (select G_NO   from ViewProductionPiecePersonlWorkGroup where Per_NO =ProductionPiecePersonnelDay.Per_NO ) as G_NO_Old,"
                strSql = strSql + "(select G_Name  from ViewProductionPiecePersonlWorkGroup WHERE Per_NO =ProductionPiecePersonnelDay.Per_NO) AS G_Name_Old, "
                strSql = strSql + "(select DepName from BriName where BriName.DepID=ProductionPiecePersonnelDay.DepID) as Per_DepName,"
                strSql = strSql + "(select FacName from FacName where FacName.FacID=ProductionPiecePersonnelDay.FacID) as Per_FacName,"
                strSql = strSql + "(select Per_Resign from ProductionPiecePersonnel where ProductionPiecePersonnelDay.per_no=ProductionPiecePersonnel.per_no) as Per_Resign "
                strSql = strSql + " from ProductionPiecePersonnelDay " + strStarEndDate + "  order by per_no desc "

                da = New SqlDataAdapter(strSql, strCn)
                da.Fill(dsP, "ProductionPiecePersonnel")

                strCn.Close()

                ' ltc2.CollToDataSet(dsP, "ProductionPiecePersonnel", ppp.ProductionPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing))
                ltc3.CollToDataSet(dsP, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))

                PreviewRPTDialog(dsP, "rptProductionPieceWorkGroupDayColl", "�էO�W��(���`)", True, True)

        End Select

        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing
        ltc4 = Nothing
        ltc5 = Nothing



    End Function
    ''' <summary>
    ''' �ھڤ��P��������J���P�����e
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.Click
        'On Error Resume Next
        Dim StrType As String
        Dim StrCategory As String


        StrType = GridView1.GetFocusedRowCellValue("Type")
        'StrCategory = GridView1.GetFocusedRowCellValue("Category")
        If InStr(GridView1.GetFocusedRowCellValue("Category"), "����", CompareMethod.Text) > 0 Then
            StrCategory = "����"
        Else
            StrCategory = GridView1.GetFocusedRowCellValue("Category")
        End If


        If StrType = "���" Then
            ConditionBoxEdit.Properties.Items.Clear()
            ConditionBoxEdit.Properties.Items.Add("=   ����")
            ConditionBoxEdit.Properties.Items.Add(">=  �j�󵥩�")
            ConditionBoxEdit.Properties.Items.Add("<=  �p�󵥩�")
            ConditionBoxEdit.Text = "=   ����"

            DateEdit1.Left = AddTextEdit.Left
            DateEdit1.Top = AddTextEdit.Top
            DateEdit1.Width = AddTextEdit.Width
            AddTextEdit.Visible = False
            TypeLookUp.Visible = False
            DateEdit1.Visible = True
            TimeEdit1.Visible = False

            DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")

            AddLabel.Text = "Date"

        ElseIf StrType = "�~��" Then
            ConditionBoxEdit.Properties.Items.Clear()
            ConditionBoxEdit.Properties.Items.Add("=   ����")
            ConditionBoxEdit.Properties.Items.Add(">=  �j�󵥩�")
            ConditionBoxEdit.Properties.Items.Add("<=  �p�󵥩�")
            ConditionBoxEdit.Text = "=   ����"

            TimeEdit1.Left = AddTextEdit.Left
            TimeEdit1.Top = AddTextEdit.Top
            TimeEdit1.Width = AddTextEdit.Width
            AddTextEdit.Visible = False
            TypeLookUp.Visible = False
            DateEdit1.Visible = False
            TimeEdit1.Visible = True

            TimeEdit1.EditValue = Format(Now, "yyyy�~MM��")

            AddLabel.Text = "YearMonth"

        ElseIf StrType = "����" Then

            DateEdit1.Visible = False

            ConditionBoxEdit.Properties.Items.Clear()
            ConditionBoxEdit.Properties.Items.Add("=   ����")
            ConditionBoxEdit.Text = "=   ����"

            TypeLookUp.Visible = True
            AddTextEdit.Visible = False
            DateEdit1.Visible = False
            TimeEdit1.Visible = False

            TypeLookUp.Width = AddTextEdit.Width
            TypeLookUp.Top = AddTextEdit.Top
            TypeLookUp.Left = AddTextEdit.Left

            TypeLookUp.Properties.Columns(0).FieldName = "Bool"
            TypeLookUp.Properties.Columns(1).Visible = False
            TypeLookUp.Properties.Columns(2).Visible = False

            TypeLookUp.Properties.Columns(0).Caption = StrCategory

            TypeLookUp.Properties.DisplayMember = "Bool"
            TypeLookUp.Properties.ValueMember = "Bool"
            TypeLookUp.Properties.DataSource = ds.Tables("TBool")

            AddLabel.Text = "Bool"


        ElseIf StrType = "��r" And (StrCategory = "����" Or StrCategory = "�u������" Or StrCategory = "���~�s��" Or StrCategory = "�t��W��") Then

            DateEdit1.Visible = False

            ConditionBoxEdit.Properties.Items.Clear()
            ConditionBoxEdit.Properties.Items.Add("=   ����")
            ConditionBoxEdit.Text = "=   ����"

            TypeLookUp.Visible = True
            AddTextEdit.Visible = False
            DateEdit1.Visible = False
            TimeEdit1.Visible = False

            TypeLookUp.Width = AddTextEdit.Width
            TypeLookUp.Top = AddTextEdit.Top
            TypeLookUp.Left = AddTextEdit.Left

            AddLabel.Text = "Com"
            LoadCom(StrCategory)

        Else
            DateEdit1.Visible = False
            ConditionBoxEdit.Properties.Items.Clear()
            ConditionBoxEdit.Properties.Items.Add("=     ����")
            ConditionBoxEdit.Properties.Items.Add("like  �ҽk")
            ConditionBoxEdit.Text = "=     ����"

            TypeLookUp.Visible = False
            AddTextEdit.Visible = True
            DateEdit1.Visible = False
            TimeEdit1.Visible = False

            AddLabel.Text = "Text"
        End If

        LabelControl3.Text = "(" + GridView1.GetFocusedRowCellValue("Category") + ")"
    End Sub

    ''' <summary>
    ''' ��TypeLookUp���󪺸j�w�ݩ�
    ''' </summary>
    ''' <param name="_StrCategory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadCom(ByVal _StrCategory As String) As String
        Dim pc As New ProcessMainControl

        LoadCom = ""
        Select Case _StrCategory
            Case "����"

                TypeLookUp.Properties.Columns(0).FieldName = "DepID"
                TypeLookUp.Properties.Columns(1).FieldName = "DepName"
                TypeLookUp.Properties.Columns(2).FieldName = "FacName"

                TypeLookUp.Properties.Columns(0).Caption = "�����s��"
                TypeLookUp.Properties.Columns(1).Caption = "�����W��"
                TypeLookUp.Properties.Columns(2).Caption = "�t�O�W��"

                TypeLookUp.Properties.Columns(0).Visible = True
                TypeLookUp.Properties.Columns(1).Visible = True
                TypeLookUp.Properties.Columns(2).Visible = True

                Load_Dep1()

                TypeLookUp.Properties.DisplayMember = "DepIDName"
                TypeLookUp.Properties.ValueMember = "DepID"
                TypeLookUp.Properties.DataSource = ds.Tables("TDep1")

            Case "�u������"
                TypeLookUp.Properties.Columns(0).FieldName = "Pro_Type"
                TypeLookUp.Properties.Columns(1).Visible = False
                TypeLookUp.Properties.Columns(2).Visible = False

                TypeLookUp.Properties.Columns(0).Caption = "�u������"

                TypeLookUp.Properties.DisplayMember = "Pro_Type"
                TypeLookUp.Properties.ValueMember = "Pro_Type"
                TypeLookUp.Properties.DataSource = ds.Tables("TPro_Type")

            Case "���~�s��"

                TypeLookUp.Properties.Columns(0).FieldName = "PM_M_Code"
                TypeLookUp.Properties.Columns(1).FieldName = "PM_JiYu"
                'TypeLookUp.Properties.Columns(1).Visible = False
                TypeLookUp.Properties.Columns(2).Visible = False

                TypeLookUp.Properties.Columns(0).Caption = "���~�s��"
                TypeLookUp.Properties.Columns(1).Caption = strJIYU

                'Dim mc As New ProductController
                TypeLookUp.Properties.DisplayMember = "PM_M_Code"
                TypeLookUp.Properties.ValueMember = "PM_M_Code"
                'TypeLookUp.Properties.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing) 'DelContent
                TypeLookUp.Properties.DataSource = pc.ProcessMain_GetList3(Nothing, Nothing)

            Case "�t��W��"
                Dim strPM_M_CodeLabel As String
                Dim strProTypeLabel As String

                If PM_M_CodeLabel.Text = "" Or PM_M_CodeLabel.Text = "***" Then
                    TypeLookUp.Properties.DataSource = Nothing
                    Exit Function
                End If

                strPM_M_CodeLabel = PM_M_CodeLabel.Text

                If ProTypeLabel.Text = "" Or ProTypeLabel.Text = "***" Then
                    strProTypeLabel = Nothing
                Else
                    strProTypeLabel = ProTypeLabel.Text
                End If

                ''----------------------------------------------------------------------------------------------------------------
                Dim pcc As New ProcessMainControl

                TypeLookUp.Properties.Columns(0).FieldName = "Type3ID"
                TypeLookUp.Properties.Columns(1).Visible = False
                TypeLookUp.Properties.Columns(2).Visible = False

                TypeLookUp.Properties.Columns(0).Caption = "����"

                Dim mc As New ProductController
                TypeLookUp.Properties.DisplayMember = "Type3ID"
                TypeLookUp.Properties.ValueMember = "Type3ID"
                TypeLookUp.Properties.DataSource = pcc.ProcessMain_GetList1(Nothing, strPM_M_CodeLabel, strProTypeLabel, Nothing)

        End Select
    End Function

    ''' <summary>
    ''' �K�[����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddButton.Click

        If AddLabel.Text = "Date" Then
            Dim Row As DataRow
            Row = ds.Tables("SelectTbDel").NewRow

            If ds.Tables("SelectTbDel").Rows.Count = 0 Then
                Row("DelConditon") = GridView1.GetFocusedRowCellValue("FieldName") + "  " + Mid(Trim(ConditionBoxEdit.Text), 1, 3) + " " + "'" + DateEdit1.Text + "'" + " "
                Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ConditionBoxEdit.Text), 3) + " " + "'" + DateEdit1.Text + "'"
            Else
                Row("DelConditon") = " " & Mid(ComboBoxEdit3.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + "  " + Mid(Trim(ConditionBoxEdit.Text), 1, 3) + " " + "'" + DateEdit1.Text + "'" + " "
                Row("DelDisplay") = Mid(Trim(ComboBoxEdit3.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ConditionBoxEdit.Text), 3) + " " + "'" + DateEdit1.Text + "'"
            End If
            ds.Tables("SelectTbDel").Rows.Add(Row)
        End If

        ''-----------------------------------------------------------------------------------------------------------------------------------------

        If AddLabel.Text = "Text" Then
            If AddTextEdit.Text = "" Then
                MsgBox("�ƾڬ���,����K�[!")
                AddTextEdit.Select()
                Exit Sub
            End If

            Dim Row As DataRow
            Row = ds.Tables("SelectTbDel").NewRow

            If ds.Tables("SelectTbDel").Rows.Count = 0 Then
                If Mid(ConditionBoxEdit.Text, 1, 1) = "=" Then
                    Row("DelConditon") = " " + GridView1.GetFocusedRowCellValue("FieldName") + " = " + "'" + Trim(AddTextEdit.Text) + "'" + " "
                Else
                    Row("DelConditon") = " " + GridView1.GetFocusedRowCellValue("FieldName") + " like " + "'%" + Trim(AddTextEdit.Text) + "%'" + " "
                End If
                Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + "  " + Mid(Trim(ConditionBoxEdit.Text), 1, 4) + "  " + "'" + Trim(AddTextEdit.Text) + "'"
            Else
                If Mid(ConditionBoxEdit.Text, 1, 1) = "=" Then
                    Row("DelConditon") = " " & Mid(Trim(ComboBoxEdit3.Text), 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " = " + "'" + Trim(AddTextEdit.Text) + "'" + " "
                Else
                    Row("DelConditon") = " " & Mid(Trim(ComboBoxEdit3.Text), 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " like " + "'%" + Trim(AddTextEdit.Text) + "%'" + " "
                End If
                Row("DelDisplay") = Mid(Trim(ComboBoxEdit3.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + "  " + Mid(Trim(ConditionBoxEdit.Text), 1, 4) + "  " + "'" + Trim(AddTextEdit.Text) + "'"
            End If

            ds.Tables("SelectTbDel").Rows.Add(Row)
            AddTextEdit.Text = ""
        End If
        ''-----------------------------------------------------------------------------------------------------------------------------------------
        If AddLabel.Text = "Com" Then


            If LabelControl3.Text = "(�u������)" Then
                ProTypeLabel.Text = TypeLookUp.EditValue
            End If

            If LabelControl3.Text = "(���~�s��)" Then
                PM_M_CodeLabel.Text = TypeLookUp.EditValue
            End If

            If TypeLookUp.EditValue Is Nothing Then
                MsgBox("�ƾڬ���,����K�[!")
                AddTextEdit.Select()
                Exit Sub
            End If

            Dim Row As DataRow

            Row = ds.Tables("SelectTbDel").NewRow

            If ds.Tables("SelectTbDel").Rows.Count = 0 Then
                Row("DelConditon") = GridView1.GetFocusedRowCellValue("FieldName") + "  " + Mid(Trim(ConditionBoxEdit.Text), 1, 3) + " " + "'" + TypeLookUp.EditValue + "'" + " "
                Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ConditionBoxEdit.Text), 3) + " " + "'" + TypeLookUp.Text + "'"
            Else
                Row("DelConditon") = " " & Mid(ComboBoxEdit3.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + "  " + Mid(Trim(ConditionBoxEdit.Text), 1, 3) + " " + "'" + TypeLookUp.EditValue + "'" + " "
                Row("DelDisplay") = Mid(Trim(ComboBoxEdit3.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ConditionBoxEdit.Text), 3) + " " + "'" + TypeLookUp.Text + "'"
            End If
            ds.Tables("SelectTbDel").Rows.Add(Row)

            TypeLookUp.EditValue = Nothing
        End If

        ''-----------------------------------------------------------------------------------------------------------------------------------------
        If AddLabel.Text = "Bool" Then
            Dim Row As DataRow

            Row = ds.Tables("SelectTbDel").NewRow

            If ds.Tables("SelectTbDel").Rows.Count = 0 Then
                Row("DelConditon") = GridView1.GetFocusedRowCellValue("FieldName") + "  " + Mid(Trim(ConditionBoxEdit.Text), 1, 3) + " " + "'" + TypeLookUp.EditValue + "'" + " "
                Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ConditionBoxEdit.Text), 3) + " " + "'" + TypeLookUp.Text + "'"
            Else
                Row("DelConditon") = " " & Mid(ComboBoxEdit3.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + "  " + Mid(Trim(ConditionBoxEdit.Text), 1, 3) + " " + "'" + TypeLookUp.EditValue + "'" + " "
                Row("DelDisplay") = Mid(Trim(ComboBoxEdit3.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ConditionBoxEdit.Text), 3) + " " + "'" + TypeLookUp.Text + "'"
            End If
            ds.Tables("SelectTbDel").Rows.Add(Row)
        End If

        ''-----------------------------------------------------------------------------------------------------------------------------------------
        If AddLabel.Text = "YearMonth" Then
            Dim Row As DataRow
            Row = ds.Tables("SelectTbDel").NewRow

            If ds.Tables("SelectTbDel").Rows.Count = 0 Then
                Row("DelConditon") = GridView1.GetFocusedRowCellValue("FieldName") + "  " + Mid(Trim(ConditionBoxEdit.Text), 1, 3) + " " + "'" + Format(TimeEdit1.EditValue, "yyyy/MM") + "'" + " "
                Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ConditionBoxEdit.Text), 3) + " " + "'" + Format(TimeEdit1.EditValue, "yyyy/MM") + "'"
            Else
                Row("DelConditon") = " " & Mid(ComboBoxEdit3.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + "  " + Mid(Trim(ConditionBoxEdit.Text), 1, 3) + " " + "'" + Format(TimeEdit1.EditValue, "yyyy/MM") + "'" + " "
                Row("DelDisplay") = Mid(Trim(ComboBoxEdit3.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ConditionBoxEdit.Text), 3) + " " + "'" + Format(TimeEdit1.EditValue, "yyyy/MM") + "'"
            End If
            ds.Tables("SelectTbDel").Rows.Add(Row)
        End If
    End Sub

    Private Sub SimpleButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton6.Click
        If GridView2.RowCount = 0 Then Exit Sub
        Dim i As Integer = ds.Tables("SelectTbDel").Rows.Count - 1

        If InStr(ds.Tables("SelectTbDel").Rows(i)("DelDisplay").ToString, "���~�s��", CompareMethod.Text) > 0 Then
            PM_M_CodeLabel.Text = "***"
        End If

        ds.Tables("SelectTbDel").Rows.RemoveAt(i)
    End Sub


    Private Sub TypeLookUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TypeLookUp.Click
        If (PM_M_CodeLabel.Text = "" Or PM_M_CodeLabel.Text = "***") And LabelControl3.Text = "(�t��W��)" Then
            MsgBox("�Х���ܲ��~�s��!")
        End If
    End Sub

    Private Sub AddTextEdit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles AddTextEdit.KeyDown, TimeEdit1.KeyDown, DateEdit1.KeyDown
        If e.KeyCode = Keys.Enter Then
            AddButton_Click(Nothing, Nothing)
        End If
    End Sub

    'Private Sub TypeLookUp_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TypeLookUp.EditValueChanged
    '    If TypeLookUp.Text.Trim = "" Or TypeLookUp.EditValue = Nothing Then Exit Sub

    '    'AddButton_Click(Nothing, Nothing)

    'End Sub

    Private Sub GridView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView2.KeyDown
        If e.KeyCode = Keys.Delete Then
            SimpleButton6_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub ComboType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboType.KeyDown, ConditionBoxEdit.KeyDown, ComboBoxEdit3.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub

    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            GridView1_Click(Nothing, Nothing)
        End If
    End Sub


    Function CreateTableData(ByVal Ds As DataSet, ByVal TabName As String, ByVal Per_no As String, ByVal Per_Name As String) As Boolean
        CreateTableData = False

        Ds.Tables(TabName).Rows(0)("Per_NO") = Per_no
        Ds.Tables(TabName).Rows(0)("PP_Per_Name") = Per_Name

        Ds.Tables(TabName).Rows(0)("PP_DateEnd") = Format(CDate(EndDateEdit.EditValue), "yyyy/MM/dd")
        Ds.Tables(TabName).Rows(0)("PP_DateStart") = Format(CDate(StartDateEdit.EditValue), "yyyy/MM/dd")


        'Ds.Tables(TabName).Rows(0)("PP_AutoID") = 192
        Ds.Tables(TabName).Rows(0)("DepID") = ""
        Ds.Tables(TabName).Rows(0)("FacID") = ""
        'ai1.PP_DateEnd = Nothing
        'ai1.PP_DateStart = Nothing


        'ai1.PP_NO = Nothing '            *  nvarchar(50)                /�p��渹
        'ai1.Per_NO = Nothing '           *  nvarchar(50)                /���u�s��
        'ai1.G_NO = Nothing '              *  nvarchar(50)                /�էO�s��
        'ai1.DepID = Nothing '             *  nvarchar(50)                /�����s��
        'ai1.FacID = Nothing '             *  nvarchar(50)                /�t�O

        'ai1.Pro_Type = Nothing '          *  nvarchar(50)                /�u������
        'ai1.PM_M_Code = Nothing '         *  nvarchar(50)                /���~�s��
        'ai1.PM_Type = Nothing '          *  nvarchar(50)                /�t��W��

        'ai1.PS_NameS = Nothing '          *  nvarchar(50)                /�p�u�ǦW��

        'ai1.PP_factor = 0 '         *  float                       /�ӥ]�t��
        'ai1.PP_Qty = 0 '            *  int                         /�ƶq 
        'ai1.PP_Date = Nothing '           *  datetime                    /�p����
        'ai1.PP_AddDate = Nothing '        *  datetime                 /�O�����
        'ai1.PP_Action = Nothing  '        *  nvarchar(50)                /�ާ@�H

        'ai1.PP_Remark = Nothing '         *  nvarchar(MAX)               /�ƪ`

        ''�~��r�q
        'ai1.PP_ActionName = Nothing ' �ާ@���W (SystemUser)
        'ai1.PP_Per_Name = Nothing '    ���u�W�m�W(ProductionPiecePersonnel)
        'ai1.PP_DepName = Nothing '    �����W
        'ai1.PP_FacName = Nothing '    �t�O�W
        'ai1.PP_G_Name = Nothing '      �էO�W(ProductionPieceWorkGroup)    ���Ҥ���

        'ai1.PS_Name = Nothing                    '    /�j�u�ǦW��  ProcessSub
        'ai1.PS_NO = Nothing                      '    �j�u�ǽs��   ProductionPieceProcess

        'ai1.PP_AutoID = Nothing ''�u�����y�{
        'ai1.PP_Price = 0  ''�u��

        'ai1.PP_DateEnd = Nothing
        'ai1.PP_DateStart = Nothing

        'ai1.Print_Action = Nothing ''�ѥ��L�� ��


    End Function




End Class