Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.PieceProcess
Imports LFERP.Library.ProductionPieceProcess
Imports LFERP.DataSetting
Imports LFERP.Library.Production.Datasetting

Public Class frmProductionPieceProcess
    Dim ds As New DataSet
    Dim ppc As New ProductionPieceProcessControl
    Dim fc As New FacControler
    Dim isSameRow As Boolean '�P�_�O�_���ۦP���O����

    Dim StrPM_M_Code As String
    Dim StrPM_Type As String
    Dim StrPro_Type As String

    Private Sub frmProductionPieceProcess_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        RepositoryItemCalcEdit1.DisplayFormat.FormatString = "###0.0#####"
        RepositoryItemCalcEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        RepositoryItemCalcEdit1.Mask.EditMask = "###0.0#####"
        RepositoryItemCalcEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        RepositoryItemCalcEdit1.Mask.UseMaskAsDisplayFormat = True


        StrPro_Type = tempValue
        StrPM_M_Code = tempValue2
        StrPM_Type = tempValue3


        'Dim mc As New ProductController
        Dim pdsi As List(Of ProductionDataSettingInfo)
        Dim pdsc As New ProductionDataSettingControl
        Dim pc As New ProcessMainControl

        CreateTable()   '�եγЫت�L�{

        gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
        gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
        gluPM_M_Code.Properties.DataSource = pc.ProcessMain_GetList3(Nothing, Nothing)

        If strInUserRank = "�έp" Then
            pdsi = pdsc.ProductionUser_GetList(strInDepIDFull, Nothing)
            If pdsi.Count > 0 Then
                gluPM_M_Code.Properties.DataSource = pdsc.ProductionUser_GetList(strInDepIDFull, Nothing)
            End If
        End If

        Select Case lblTittle.Text
            Case "�p��u����--�s�W"
                txtPP_ActionUser.Text = UserName
                dtePP_ActionDate.DateTime = Format(Now, "yyyy/MM/dd")
                txtPP_ID.Text = GetID()

                PP_PriceCheck.Visible = False
                PP_FactorCheck.Visible = False

            Case "�p��u����--�ק�"
                LoadData()      '�եΥ[���ƾڹL�{
                cboPro_Type.Enabled = False
                gluPM_M_Code.Enabled = False
                gluPM_Type.Enabled = False

                PP_PriceCheck.Visible = False
                PP_FactorCheck.Visible = False
            Case "�p��u����--�f��"
                LoadData()      '�եΥ[���ƾڹL�{
                GroupBox1.Enabled = False
                cboPro_Type.Properties.ReadOnly = True
                gluPM_M_Code.Properties.ReadOnly = True
                gluPM_Type.Properties.ReadOnly = True
                txtPP_ActionUser.Properties.ReadOnly = True
                dtePP_ActionDate.Properties.ReadOnly = True
                txtPP_Remark.Properties.ReadOnly = True
                PP_N_Name.OptionsColumn.ReadOnly = True
                DPT_PName.OptionsColumn.ReadOnly = True
                DPT_Name.OptionsColumn.ReadOnly = True
                PP_Price.OptionsColumn.ReadOnly = True
                PP_Factor.OptionsColumn.ReadOnly = True
                PP_Type.OptionsColumn.ReadOnly = True
                PP_Explain.OptionsColumn.ReadOnly = True
                PP_BeginUseDate.OptionsColumn.ReadOnly = True
                PP_EndUseDate.OptionsColumn.ReadOnly = True
                PP_BeginUse.OptionsColumn.ReadOnly = False
                chkPP_Check.Enabled = True
                btnAllUse.Enabled = True
                lblPP_CheckUserName.Visible = True
                lblPP_CheckDate.Visible = True
                lblCheckName.Visible = True
                lblCheckDate.Visible = True
                lblPP_CheckUserName.Text = UserName
                lblPP_CheckDate.Text = Format(Now, "yyyy/MM/dd")
                btnAllUse.Visible = True
                chkPP_Check.Visible = True
                PP_BeginUse.Visible = True
                Grid.ContextMenuStrip.Enabled = False
                btnOK.Enabled = False

                PP_PriceCheck.Visible = False
                PP_FactorCheck.Visible = False

                PP_Price.OptionsColumn.ReadOnly = True
                PP_Factor.OptionsColumn.ReadOnly = True


            Case "�p��u����--�d��"
                LoadData()      '�եΥ[���ƾڹL�{
                btnOK.Visible = False
                btnAllUse.Visible = True
                chkPP_Check.Visible = True
                PP_BeginUse.Visible = True
                Grid.ContextMenuStrip.Enabled = False

                cboPro_Type.Enabled = False

            Case "�p��u����--������J"
                LoadData()      '�եΥ[���ƾڹL�{
                cboPro_Type.Properties.ReadOnly = True
                gluPM_M_Code.Properties.ReadOnly = True
                gluPM_Type.Properties.ReadOnly = True
                txtPP_ActionUser.Properties.ReadOnly = True
                dtePP_ActionDate.Properties.ReadOnly = True
                txtPP_Remark.Properties.ReadOnly = True
                PP_N_Name.OptionsColumn.ReadOnly = True
                DPT_PName.OptionsColumn.ReadOnly = True
                DPT_Name.OptionsColumn.ReadOnly = True

                PP_Type.OptionsColumn.ReadOnly = True
                PP_Explain.OptionsColumn.ReadOnly = True
                PP_BeginUseDate.OptionsColumn.ReadOnly = True
                PP_EndUseDate.OptionsColumn.ReadOnly = True
                PP_BeginUse.OptionsColumn.ReadOnly = True
                chkPP_Check.Enabled = True
                btnAllUse.Enabled = True
                lblPP_CheckUserName.Visible = True
                lblPP_CheckDate.Visible = True
                lblCheckName.Visible = True
                lblCheckDate.Visible = True
                btnAllUse.Visible = True
                chkPP_Check.Visible = True
                PP_BeginUse.Visible = True
                Grid.ContextMenuStrip.Enabled = False

                PP_Price.OptionsColumn.ReadOnly = False
                PP_Factor.OptionsColumn.ReadOnly = False

                PP_PriceCheck.OptionsColumn.ReadOnly = True
                PP_FactorCheck.OptionsColumn.ReadOnly = True

                btnOK.Enabled = True

                btnAllUse.Visible = False
                chkPP_Check.Visible = False
                Panel1.Visible = False

            Case "�p��u����--����f��"
                LoadData()      '�եΥ[���ƾڹL�{
                cboPro_Type.Properties.ReadOnly = True
                gluPM_M_Code.Properties.ReadOnly = True
                gluPM_Type.Properties.ReadOnly = True
                txtPP_ActionUser.Properties.ReadOnly = True
                dtePP_ActionDate.Properties.ReadOnly = True
                txtPP_Remark.Properties.ReadOnly = True
                PP_N_Name.OptionsColumn.ReadOnly = True
                DPT_PName.OptionsColumn.ReadOnly = True
                DPT_Name.OptionsColumn.ReadOnly = True

                PP_Type.OptionsColumn.ReadOnly = True
                PP_Explain.OptionsColumn.ReadOnly = True
                PP_BeginUseDate.OptionsColumn.ReadOnly = True
                PP_EndUseDate.OptionsColumn.ReadOnly = True
                PP_BeginUse.OptionsColumn.ReadOnly = True
                chkPP_Check.Enabled = True
                btnAllUse.Enabled = True
                lblPP_CheckUserName.Visible = True
                lblPP_CheckDate.Visible = True
                lblCheckName.Visible = True
                lblCheckDate.Visible = True
                btnAllUse.Visible = True
                chkPP_Check.Visible = True
                PP_BeginUse.Visible = True
                Grid.ContextMenuStrip.Enabled = False

                PP_Price.OptionsColumn.ReadOnly = True
                PP_Factor.OptionsColumn.ReadOnly = True

                PP_PriceCheck.OptionsColumn.ReadOnly = False
                PP_FactorCheck.OptionsColumn.ReadOnly = False

                LabelControl9.Visible = True
                LabPP_PriceCheckUserID.Visible = True
                LabelControl7.Visible = True
                LabPP_PriceCheckDate.Visible = True


                btnAllUse.Visible = False
                chkPP_Check.Visible = False
                Panel1.Visible = False
            Case "�p��u����--���J"  '2013-3-19


                LoadData()      '�եΥ[���ƾڹL�{
                cboPro_Type.Properties.ReadOnly = True
                gluPM_M_Code.Properties.ReadOnly = True
                gluPM_Type.Properties.ReadOnly = True
                txtPP_ActionUser.Properties.ReadOnly = True
                dtePP_ActionDate.Properties.ReadOnly = True
                txtPP_Remark.Properties.ReadOnly = True
                PP_PriceCheck.Visible = False
                PP_FactorCheck.Visible = False

                btnAllUse.Visible = False
                chkPP_Check.Visible = False
                Panel1.Visible = False
        End Select

        LoadPS_NO()


    End Sub
    ''' <summary>
    ''' �Ыت�
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTable()
        ds.Tables.Clear()

        '�p��u����
        With ds.Tables.Add("PieceProcess")
            .Columns.Add("AutoID", GetType(Integer))
            .Columns.Add("PP_Num", GetType(Integer))
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
            .Columns.Add("PP_N_Name", GetType(String))

            .Columns.Add("DPT_PName", GetType(String))
            .Columns.Add("DPT_ID", GetType(String))
            .Columns.Add("DPT_Name", GetType(String))
            .Columns.Add("PP_Price", GetType(Single))
            .Columns.Add("PP_Factor", GetType(Single))

            .Columns.Add("PP_Type", GetType(String))
            .Columns.Add("PP_Explain", GetType(String))
            .Columns.Add("PP_BeginUseDate", GetType(Date))
            .Columns.Add("PP_EndUseDate", GetType(Date))
            .Columns.Add("RemnantDays", GetType(Integer))

            .Columns.Add("PP_BeginUse", GetType(Boolean))


            .Columns.Add("PP_PriceCheck", GetType(Boolean))
            .Columns.Add("PP_FactorCheck", GetType(Boolean))

        End With

        Grid.DataSource = ds.Tables("PieceProcess")

        '�t��W�٪�
        With ds.Tables.Add("ProductType")
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluPM_Type.Properties.DisplayMember = "PM_Type"
        gluPM_Type.Properties.ValueMember = "PM_Type"
        gluPM_Type.Properties.DataSource = ds.Tables("ProductType")

        '�ЫاR���H����A�Ω�ק�ɧR���ƾڥ�
        With ds.Tables.Add("DelPieceProcess")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("PP_ID", GetType(String))
        End With

    End Sub

    ''' <summary>
    ''' �۰ʭp��u���渹
    ''' </summary>
    ''' <returns></returns>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' AddData()
    ''' frmProductionPieceProcess_Load()
    Function GetID() As String
        Dim str As String

        str = "PP" & CStr(Format(Now, "yyMM"))

        Dim ppi As List(Of ProductionPieceProcessInfo)
        ppi = ppc.ProductionPieceProcess_GetList(Nothing, str, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If ppi.Count <= 0 Then
            GetID = str & "0001"
        Else
            GetID = str & Mid((CInt(Mid(ppi(0).PP_ID, 7)) + 10001), 2)
        End If
    End Function

    ''' <summary>
    ''' �[���ƾ�
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' frmProductionPieceProcess_Load()
    Sub LoadData()
        'On Error Resume Next

        Dim ppi As New List(Of ProductionPieceProcessInfo)
        Dim ts As TimeSpan
        'Dim usc As New UserPowerControl
        'Dim usi As List(Of UserPowerInfo)

        'usi = usc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

        If Me.lblTittle.Text = "�p��u����--���J" Then
            ppi = ppc.ProductionPieceProcess_GetList(Nothing, Nothing, StrPro_Type, StrPM_M_Code, StrPM_Type, Nothing, Nothing, strInDepID, True, Nothing, Nothing, Nothing)
        Else
            ppi = ppc.ProductionPieceProcess_GetList(Nothing, txtPP_ID.Text.Trim, Nothing, Nothing, Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing)
        End If


        If ppi.Count > 0 Then
            txtPP_ID.Text = ppi(0).PP_ID ''Ū���y����

            cboPro_Type.Text = ppi(0).Pro_Type
            gluPM_M_Code.EditValue = ppi(0).PM_M_Code
            gluPM_Type.Text = ppi(0).PM_Type
            txtPP_ActionUser.Text = ppi(0).PP_ActionUser
            dtePP_ActionDate.EditValue = ppi(0).PP_ActionDate

            txtPP_Remark.Text = ppi(0).PP_Remark
            chkPP_Check.EditValue = ppi(0).PP_Check

            Me.LabPP_PriceCheckDate.Text = ppi(0).PP_PriceCheckDate
            Me.LabPP_PriceCheckUserID.Text = ppi(0).PP_PriceCheckUserName

            Me.LabelCheckID.Text = ppi(0).PP_CheckUserID
            Me.lblPP_CheckDate.Text = ppi(0).PP_CheckDate

            If ppi(0).PP_Check = True Then
                btnAllUse.Text = "�������ҥ�(&A)"
                lblPP_CheckUserName.Visible = True
                lblPP_CheckDate.Visible = True
                lblCheckName.Visible = True
                lblCheckDate.Visible = True
                lblPP_CheckUserName.Text = ppi(0).PP_CheckUserName
                lblPP_CheckDate.Text = ppi(0).PP_CheckDate
            End If
            Dim row As DataRow
            Dim i%

            ds.Tables("PieceProcess").Clear()
            For i = 0 To ppi.Count - 1
                row = ds.Tables("PieceProcess").NewRow()

                row("AutoID") = ppi(i).AutoID
                row("PP_Num") = ppi(i).PP_Num
                row("PS_NO") = ppi(i).PS_NO
                row("PS_Name") = ppi(i).PS_Name
                row("PP_N_Name") = ppi(i).PP_N_Name

                '�[���t�O�W��
                RepositoryItemLookUpEdit1.DisplayMember = "FacName"
                RepositoryItemLookUpEdit1.ValueMember = "FacID"
                RepositoryItemLookUpEdit1.DataSource = fc.GetFacList(strInFacID, Nothing)

                row("DPT_PName") = Mid(ppi(i).DPT_ID, 1, 1)
                row("DPT_ID") = ppi(i).DPT_ID
                row("DPT_Name") = ppi(i).DPT_Name
                row("PP_Price") = ppi(i).PP_Price
                row("PP_Factor") = ppi(i).PP_Factor

                row("PP_Type") = ppi(i).PP_Type
                row("PP_Explain") = ppi(i).PP_Explain
                row("PP_BeginUseDate") = ppi(i).PP_BeginUseDate
                row("PP_EndUseDate") = ppi(i).PP_EndUseDate


                row("PP_PriceCheck") = ppi(i).PP_PriceCheck
                row("PP_FactorCheck") = ppi(i).PP_FactorCheck

                '�p��ѧE�Ѽ�
                If Now > ppi(i).PP_BeginUseDate Then    '�P�_��e����O�_�j��ҥΤ��
                    ts = ppi(i).PP_EndUseDate - CDate(Format(Now, "yyyy/MM/dd"))    '�ѧE�ѼƬ����������h��e���
                Else
                    ts = ppi(i).PP_EndUseDate - ppi(i).PP_BeginUseDate              '�ѧE�ѼƬ����������h�ҥΤ��
                End If

                row("RemnantDays") = ts.Days + 1    '�ѧE�Ѽƥ]�A����I����A�]���ݥ[1
                row("PP_BeginUse") = ppi(i).PP_BeginUse

                ds.Tables("PieceProcess").Rows.Add(row)
            Next
        Else
            If Me.lblTittle.Text = "�p��u����--���J" Then
                Dim ppi1 As New List(Of ProductionPieceProcessInfo)

                ppi1 = ppc.ProductionPieceProcess_GetList(Nothing, Nothing, StrPro_Type, StrPM_M_Code, StrPM_Type, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing)

                If ppi1.Count > 0 Then
                    txtPP_ID.Text = ppi1(0).PP_ID ''Ū���y����

                    cboPro_Type.Text = ppi1(0).Pro_Type
                    gluPM_M_Code.EditValue = ppi1(0).PM_M_Code
                    gluPM_Type.Text = ppi1(0).PM_Type
                    txtPP_ActionUser.Text = ppi1(0).PP_ActionUser
                    dtePP_ActionDate.EditValue = ppi1(0).PP_ActionDate

                    txtPP_Remark.Text = ppi1(0).PP_Remark
                    chkPP_Check.EditValue = ppi1(0).PP_Check

                    Me.LabPP_PriceCheckDate.Text = ppi1(0).PP_PriceCheckDate
                    Me.LabPP_PriceCheckUserID.Text = ppi1(0).PP_PriceCheckUserName

                    Me.LabelCheckID.Text = ppi1(0).PP_CheckUserID
                    Me.lblPP_CheckDate.Text = ppi1(0).PP_CheckDate
                Else
                    MsgBox("�����~,�L�w�f�֪��u���I")
                    Me.Close()
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' �O�s�K�[�ƾ�
    ''' </summary>
    ''' <returns></returns>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' btnOK_Click()
    Function AddData() As Boolean
        Dim ppi As New ProductionPieceProcessInfo
        Dim i%

        Dim pi As List(Of ProductionPieceProcessInfo)
        pi = ppc.ProductionPieceProcess_GetList(Nothing, txtPP_ID.Text.Trim, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        '�P�_�p��u���渹�O�_�w�s�b
        If pi.Count > 0 Then
            MsgBox("�p��u���渹�w�s�b�A�ݭ��s�ͦ��A�нT�w���s�ͦ��p��u���渹!", 64, "����")
            txtPP_ID.Text = GetID()     '���s�ͦ��p��u���渹
            MsgBox("�p��u���渹�w���s�ͦ��A�нT�w�O�s�H��!", 64, "����")
        End If

        ppi.PP_ID = txtPP_ID.Text
        ppi.Pro_Type = cboPro_Type.Text
        ppi.PM_M_Code = gluPM_M_Code.EditValue
        ppi.PM_Type = gluPM_Type.Text
        ppi.PP_ActionUser = txtPP_ActionUser.Text

        ppi.PP_ActionDate = dtePP_ActionDate.EditValue
        ppi.PP_Remark = txtPP_Remark.Text

        For i = 0 To ds.Tables("PieceProcess").Rows.Count - 1
            ppi.PP_Num = ds.Tables("PieceProcess").Rows(i)("PP_Num")
            ppi.PS_NO = ds.Tables("PieceProcess").Rows(i)("PS_NO")
            ppi.PP_N_Name = ds.Tables("PieceProcess").Rows(i)("PP_N_Name")
            ppi.DPT_ID = ds.Tables("PieceProcess").Rows(i)("DPT_ID")
            ppi.PP_Price = ds.Tables("PieceProcess").Rows(i)("PP_Price")

            ppi.PP_Factor = ds.Tables("PieceProcess").Rows(i)("PP_Factor")
            ppi.PP_Explain = ds.Tables("PieceProcess").Rows(i)("PP_Explain")
            ppi.PP_Type = ds.Tables("PieceProcess").Rows(i)("PP_Type")
            ppi.PP_BeginUseDate = ds.Tables("PieceProcess").Rows(i)("PP_BeginUseDate")
            ppi.PP_EndUseDate = ds.Tables("PieceProcess").Rows(i)("PP_EndUseDate")

            ppi.AddUserID = InUserID
            ppi.AddDate = Format(Now, "yyyy/MM/dd")

            Try
                If ppc.ProductionPieceProcess_Add(ppi) = True Then
                    AddData = True
                End If
            Catch ex As Exception
                If MsgBox("���Ǭ��G" & ds.Tables("PieceProcess").Rows(i)("PP_Num") & " �O���K�[����,�O�_�n�~��K�[��᪺�O���H", MsgBoxStyle.YesNo + MsgBoxStyle.Information, "����") = MsgBoxResult.No Then
                    AddData = False
                    Exit Function
                End If
            End Try
        Next
    End Function
    ''' <summary>
    ''' �O�s�ק�ƾ�
    ''' </summary>
    ''' <returns></returns>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' btnOK_Click()
    Function EditData() As Boolean
        Dim ppi As New ProductionPieceProcessInfo
        Dim i, j As Integer

        '�P�_�R�����O�_���ݭn�R�����H���A���h�R���ƾڮw�������H��
        If ds.Tables("DelPieceProcess").Rows.Count > 0 Then
            For j = 0 To ds.Tables("DelPieceProcess").Rows.Count - 1
                ppc.ProductionPieceProcess_Delete(ds.Tables("DelPieceProcess").Rows(i)("AutoID"), Nothing)
            Next
        End If

        ppi.PP_ID = txtPP_ID.Text
        ppi.Pro_Type = cboPro_Type.Text
        ppi.PM_M_Code = gluPM_M_Code.EditValue
        ppi.PM_Type = gluPM_Type.Text
        ppi.PP_ActionUser = txtPP_ActionUser.Text

        ppi.PP_ActionDate = dtePP_ActionDate.EditValue
        ppi.PP_Remark = txtPP_Remark.Text
        For i = 0 To ds.Tables("PieceProcess").Rows.Count - 1
            '�P�_�O�_���s�W�ƾ�,AutoID���ŮɡA�h���s�W�ƾ�
            If ds.Tables("PieceProcess").Rows(i)("AutoID") Is DBNull.Value Then

                ppi.PP_Num = ds.Tables("PieceProcess").Rows(i)("PP_Num")
                ppi.PS_NO = ds.Tables("PieceProcess").Rows(i)("PS_NO")
                ppi.PP_N_Name = ds.Tables("PieceProcess").Rows(i)("PP_N_Name")
                ppi.DPT_ID = ds.Tables("PieceProcess").Rows(i)("DPT_ID")
                ppi.PP_Price = ds.Tables("PieceProcess").Rows(i)("PP_Price")

                ppi.PP_Factor = ds.Tables("PieceProcess").Rows(i)("PP_Factor")
                ppi.PP_Explain = ds.Tables("PieceProcess").Rows(i)("PP_Explain")
                ppi.PP_Type = ds.Tables("PieceProcess").Rows(i)("PP_Type")
                ppi.PP_BeginUseDate = ds.Tables("PieceProcess").Rows(i)("PP_BeginUseDate")
                ppi.PP_EndUseDate = ds.Tables("PieceProcess").Rows(i)("PP_EndUseDate")

                ppi.AddUserID = InUserID
                ppi.AddDate = Format(Now, "yyyy/MM/dd")

                If ppc.ProductionPieceProcess_Add(ppi) = True Then
                    EditData = True
                Else
                    If MsgBox("���Ǭ��G" & ds.Tables("PieceProcess").Rows(i)("PP_Num") & " �s�W�O���K�[����,�O�_�n�~��O�s��᪺�O���H", MsgBoxStyle.YesNo + MsgBoxStyle.Information, "����") = MsgBoxResult.No Then
                        EditData = False
                        Exit Function
                    End If
                End If
            Else
                ppi.AutoID = ds.Tables("PieceProcess").Rows(i)("AutoID")
                ppi.PP_Num = ds.Tables("PieceProcess").Rows(i)("PP_Num")
                ppi.PS_NO = ds.Tables("PieceProcess").Rows(i)("PS_NO")
                ppi.PP_N_Name = ds.Tables("PieceProcess").Rows(i)("PP_N_Name")
                ppi.DPT_ID = ds.Tables("PieceProcess").Rows(i)("DPT_ID")

                ppi.PP_Price = ds.Tables("PieceProcess").Rows(i)("PP_Price")
                ppi.PP_Factor = ds.Tables("PieceProcess").Rows(i)("PP_Factor")
                ppi.PP_Explain = ds.Tables("PieceProcess").Rows(i)("PP_Explain")
                ppi.PP_Type = ds.Tables("PieceProcess").Rows(i)("PP_Type")
                ppi.PP_BeginUseDate = ds.Tables("PieceProcess").Rows(i)("PP_BeginUseDate")

                ppi.PP_EndUseDate = ds.Tables("PieceProcess").Rows(i)("PP_EndUseDate")
                ppi.ModifyUserID = InUserID
                ppi.ModifyDate = Format(Now, "yyyy/MM/dd")

                If ppc.ProductionPieceProcess_Update(ppi) = True Then
                    EditData = True
                Else
                    If MsgBox("���Ǭ��G" & ds.Tables("PieceProcess").Rows(i)("PP_Num") & " �O���ק異��,�O�_�n�~��ק��᪺�O���H", MsgBoxStyle.YesNo + MsgBoxStyle.Information, "����") = MsgBoxResult.No Then
                        EditData = False
                        Exit Function
                    End If
                End If
            End If
        Next
    End Function
    ''' <summary>
    ''' ���~�s���ȧ���,�bgluPM_Type���[���������t��W��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' ���L�{�Q�H�U�L�{�ե�:
    ''' cboPro_Type_SelectedIndexChanged()
    Private Sub gluPM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_M_Code.EditValueChanged
        On Error Resume Next

        Dim pcc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        pci = pcc.ProcessMain_GetList1(Nothing, gluPM_M_Code.EditValue, cboPro_Type.Text, Nothing)

        If pci.Count = 0 Then
            ds.Tables("ProductType").Clear()
            ds.Tables("PieceProcess").Clear()
        Else
            ds.Tables("ProductType").Clear()
            Dim i As Integer
            For i = 0 To pci.Count - 1
                Dim row As DataRow
                row = ds.Tables("ProductType").NewRow
                row("M_Code") = pci(i).M_Code
                row("PM_Type") = pci(i).Type3ID

                ds.Tables("ProductType").Rows.Add(row)
            Next
            gluPM_Type.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' �t��W�٧��ܮɡA�[�������ƾڨ�p��u����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluPM_Type_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_Type.EditValueChanged
        If lblTittle.Text = "�p��u����--�s�W" Then
            If cboPro_Type.Text.Trim = "" Or gluPM_M_Code.Text.Trim = "" Or gluPM_Type.Text.Trim = "" Then
                ds.Tables("PieceProcess").Clear()
            Else
                If ppc.ProductionPieceProcess_GetList(Nothing, Nothing, cboPro_Type.Text, gluPM_M_Code.EditValue, gluPM_Type.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing).Count > 0 Then
                    MsgBox("�Ӳ��~�t��u�Ǥu���w�s�b�I", 64, "����")
                    Exit Sub
                End If

                LoadPS_NO()

                Dim pc As New ProcessMainControl
                Dim pci As List(Of ProcessMainInfo)
                pci = pc.ProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, cboPro_Type.Text, gluPM_Type.EditValue, Nothing, Nothing)

                If pci.Count > 0 Then
                    ds.Tables("PieceProcess").Clear()
                    Dim i%
                    Dim ts As TimeSpan

                    For i = 0 To pci.Count - 1
                        Dim row As DataRow
                        row = ds.Tables("PieceProcess").NewRow()

                        row("PP_Num") = pci(i).PS_Num
                        row("PS_NO") = pci(i).PS_NO
                        row("PS_Name") = pci(i).PS_Name
                        row("PP_N_Name") = pci(i).PS_Name
                        row("PP_Price") = 0

                        row("PP_Factor") = 1
                        row("DPT_PName") = ""
                        row("DPT_ID") = ""
                        row("DPT_Name") = ""
                        row("PP_Explain") = ""

                        row("PP_Type") = RepositoryItemComboBox1.Items(0)
                        row("PP_BeginUseDate") = Format(Now, "yyyy/MM/dd")
                        row("PP_EndUseDate") = Date.Parse(Now.AddMonths(1).ToString("yyyy/MM") & "/01").AddDays(-1) '��e�몺�̫�@��

                        ts = Date.Parse(Now.AddMonths(1).ToString("yyyy/MM") & "/01").AddDays(-1) - CDate(Format(Now, "yyyy/MM/dd"))
                        row("RemnantDays") = ts.Days + 1

                        '�[���t�O�W��
                        RepositoryItemLookUpEdit1.DisplayMember = "FacName"
                        RepositoryItemLookUpEdit1.ValueMember = "FacID"
                        RepositoryItemLookUpEdit1.DataSource = fc.GetFacList(strInFacID, Nothing)

                        ds.Tables("PieceProcess").Rows.Add(row)
                    Next
                End If
            End If
        End If

    End Sub
    ''' <summary>
    ''' ���������������s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' �����W����o�J�I�ɡA�[�������ƾ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemPopupContainerEdit1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemPopupContainerEdit1.Enter
        Dim dc As New DepartmentControler
        '��t�O�W�٬��ŮɡA���[���ƾ�
        If IsDBNull(GridView2.GetFocusedRowCellValue("DPT_PName")) Then
            Grid1.DataSource = Nothing
        Else
            ' Grid1.DataSource = dc.BriName_GetList(Nothing, Nothing, ds.Tables("PieceProcess").Rows(GridView2.FocusedRowHandle)("DPT_PName"))
            If strInUserRank = "�έp" Then
                Grid1.DataSource = dc.BriName_GetList(strInDepID, Nothing, ds.Tables("PieceProcess").Rows(GridView2.FocusedRowHandle)("DPT_PName"))
            Else
                Grid1.DataSource = dc.BriName_GetList(Nothing, Nothing, ds.Tables("PieceProcess").Rows(GridView2.FocusedRowHandle)("DPT_PName"))
            End If
        End If
    End Sub
    ''' <summary>
    ''' �����k���桧�s�W���A�ƨ�̫�@��A�b�̫�s�W�@��ƾ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuAdd.Click

        Dim row As DataRow
        row = ds.Tables("PieceProcess").NewRow

        If GridView2.RowCount <= 0 And Me.lblTittle.Text = "�p��u����--���J" Then

            'Dim row As DataRow

            'row = ds.Tables("PieceProcess").NewRow

            row("PP_Num") = 1
            row("PS_NO") = Nothing
            row("PS_Name") = Nothing
            row("PP_N_Name") = Nothing
            row("PP_Price") = 0

            row("PP_Factor") = 1
            row("DPT_PName") = Nothing
            row("DPT_ID") = Nothing
            row("DPT_Name") = Nothing
            row("PP_Explain") = ""

            row("PP_Type") = "�j�f"
            row("PP_BeginUseDate") = Format(Now, "yyyy/MM/dd")
            row("PP_EndUseDate") = Format(Now, "yyyy/MM/dd")
            row("RemnantDays") = 0

            row("PP_BeginUse") = True

            RepositoryItemLookUpEdit1.DisplayMember = "FacName"
            RepositoryItemLookUpEdit1.ValueMember = "FacID"
            RepositoryItemLookUpEdit1.DataSource = fc.GetFacList(strInFacID, Nothing)

            ds.Tables("PieceProcess").Rows.Add(row)
        Else
            'Dim row As DataRow

            'row = ds.Tables("PieceProcess").NewRow

            row("PP_Num") = ds.Tables("PieceProcess").Rows(ds.Tables("PieceProcess").Rows.Count - 1)("PP_Num") + 1
            row("PS_NO") = ds.Tables("PieceProcess").Rows(ds.Tables("PieceProcess").Rows.Count - 1)("PS_NO")
            row("PS_Name") = ds.Tables("PieceProcess").Rows(ds.Tables("PieceProcess").Rows.Count - 1)("PS_Name")
            row("PP_N_Name") = ds.Tables("PieceProcess").Rows(ds.Tables("PieceProcess").Rows.Count - 1)("PP_N_Name")
            row("PP_Price") = 0

            row("PP_Factor") = 1
            row("DPT_PName") = ds.Tables("PieceProcess").Rows(ds.Tables("PieceProcess").Rows.Count - 1)("DPT_PName")
            row("DPT_ID") = ds.Tables("PieceProcess").Rows(ds.Tables("PieceProcess").Rows.Count - 1)("DPT_ID")
            row("DPT_Name") = ds.Tables("PieceProcess").Rows(ds.Tables("PieceProcess").Rows.Count - 1)("DPT_Name")
            row("PP_Explain") = ""

            row("PP_Type") = RepositoryItemComboBox1.Items(0)
            row("PP_BeginUseDate") = ds.Tables("PieceProcess").Rows(ds.Tables("PieceProcess").Rows.Count - 1)("PP_BeginUseDate")
            row("PP_EndUseDate") = ds.Tables("PieceProcess").Rows(ds.Tables("PieceProcess").Rows.Count - 1)("PP_EndUseDate")
            row("RemnantDays") = ds.Tables("PieceProcess").Rows(ds.Tables("PieceProcess").Rows.Count - 1)("RemnantDays")


            If Me.lblTittle.Text = "�p��u����--���J" Then
                row("PP_BeginUse") = True
            Else
                row("PP_BeginUse") = False
            End If

            RepositoryItemLookUpEdit1.DisplayMember = "FacName"
            RepositoryItemLookUpEdit1.ValueMember = "FacID"
            RepositoryItemLookUpEdit1.DataSource = fc.GetFacList(strInFacID, Nothing)

            ds.Tables("PieceProcess").Rows.Add(row)
        End If
    End Sub
    ''' <summary>
    ''' �����k���桧���J���A�ƨ��e�J�I��A�b��e��U�ѷs�W�@��ƾ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuInsert.Click
        If GridView2.RowCount <= 0 Then Exit Sub

        Dim row As DataRow
        Dim i%

        row = ds.Tables("PieceProcess").NewRow

        row("PP_Num") = GridView2.GetFocusedRowCellValue("PP_Num") + 1
        row("PS_NO") = GridView2.GetFocusedRowCellValue("PS_NO")
        row("PS_Name") = GridView2.GetFocusedRowCellValue("PS_Name")
        row("PP_N_Name") = GridView2.GetFocusedRowCellValue("PP_N_Name")
        row("PP_Price") = 0

        row("PP_Factor") = 1
        row("DPT_PName") = GridView2.GetFocusedRowCellValue("DPT_PName")
        row("DPT_ID") = GridView2.GetFocusedRowCellValue("DPT_ID")
        row("DPT_Name") = GridView2.GetFocusedRowCellValue("DPT_Name")
        row("PP_Explain") = ""

        row("PP_Type") = RepositoryItemComboBox1.Items(0)
        row("PP_BeginUseDate") = GridView2.GetFocusedRowCellValue("PP_BeginUseDate")
        row("PP_EndUseDate") = GridView2.GetFocusedRowCellValue("PP_EndUseDate")
        row("RemnantDays") = GridView2.GetFocusedRowCellValue("RemnantDays")

        If Me.lblTittle.Text = "�p��u����--���J" Then
            row("PP_BeginUse") = True
        Else
            row("PP_BeginUse") = False
        End If

        '�[���t�O�W��
        RepositoryItemLookUpEdit1.DisplayMember = "FacName"
        RepositoryItemLookUpEdit1.ValueMember = "FacID"
        RepositoryItemLookUpEdit1.DataSource = fc.GetFacList(strInFacID, Nothing)

        ds.Tables("PieceProcess").Rows.InsertAt(row, GridView2.FocusedRowHandle + 1)    '���e��U�贡�J�@��ƾ�

        '�ק隸�ǡA�q�J�I��}�l�ק�
        For i = GridView2.GetFocusedRowCellValue("PP_Num") To ds.Tables("PieceProcess").Rows.Count - 1
            ds.Tables("PieceProcess").Rows(i)("PP_Num") = i + 1
        Next
    End Sub
    ''' <summary>
    ''' �����k���桧�R�����A�R���襤���ƾڦ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuDel.Click
        On Error Resume Next

        Dim i%

        If GridView2.RowCount = 0 Then Exit Sub
        ''----------------------------------------------------------------------------------------

        If Me.lblTittle.Text = "�p��u����--���J" Then
            If IsDBNull(GridView2.GetFocusedRowCellValue("AutoID")) = True Then
                ds.Tables("PieceProcess").Rows.RemoveAt(CInt(ArrayToString(GridView2.GetSelectedRows())))
                ''-----------------------------------------------------------------------
                '�ק隸�ǡA�q�J�I��}�l�ק�
                For i = GridView2.GetFocusedRowCellValue("PP_Num") - 1 To ds.Tables("PieceProcess").Rows.Count
                    ds.Tables("PieceProcess").Rows(i - 1)("PP_Num") = i
                Next
            Else
                MsgBox("����R���w�O�s���p��u��!")
            End If
            Exit Sub
        End If
        ''----------------------------------------------------------------------------------------

        '�P�_AutoID�O�_���šA�Y�����šA���襤�檺AutoID�K�[��R���H����
        If IsDBNull(GridView2.GetFocusedRowCellValue("AutoID")) Then
        Else
            '�b�R�����W�[�Q�R�����O���A�H�K�T�w�ɧR���ƾڮw���O��
            Dim row As DataRow = ds.Tables("DelPieceProcess").NewRow

            row("AutoID") = ds.Tables("PieceProcess").Rows(GridView2.FocusedRowHandle)("AutoID")

            ds.Tables("DelPieceProcess").Rows.Add(row)
        End If

        ''---------------------------------------------------------------------------------
        'If IsDBNull(GridView2.GetFocusedRowCellValue("PP_Num")) Then
        'Else
        '    If CSng(GridView2.GetFocusedRowCellValue("PP_Num")) > 0 Then
        '        ds.Tables("PieceProcess").Rows.RemoveAt(CSng(GridView2.GetFocusedRowCellValue("PP_Num")) - 1)
        '    End If
        'End If
        ds.Tables("PieceProcess").Rows.RemoveAt(CInt(ArrayToString(GridView2.GetSelectedRows())))
        ''-----------------------------------------------------------------------

        '�ק隸�ǡA�q�J�I��}�l�ק�
        For i = GridView2.GetFocusedRowCellValue("PP_Num") - 1 To ds.Tables("PieceProcess").Rows.Count
            ds.Tables("PieceProcess").Rows(i - 1)("PP_Num") = i
        Next
    End Sub
    ''' <summary>
    ''' ����GridView3�A�ⳡ���W�٩M�s���Ǩ�GridView2��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView3.Click
        On Error Resume Next
        '��f�֮ɡA���ާ@�L��
        If GridView3.RowCount > 0 And lblTittle.Text <> "�p��u����--�f��" Then
            GridView2.SetFocusedRowCellValue(DPT_Name, GridView3.GetFocusedRowCellValue("DepName"))
            GridView2.SetFocusedRowCellValue(DPT_ID, GridView3.GetFocusedRowCellValue("DepID"))
            PopupContainerControl1.OwnerEdit.ClosePopup()
            GridView2.Focus()
        End If
    End Sub
    ''' <summary>
    ''' ��t�O�W�٧��ܮɡA�����W�٩M�s������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemLookUpEdit1_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemLookUpEdit1.EditValueChanged
        ds.Tables("PieceProcess").Rows(GridView2.FocusedRowHandle)("DPT_Name") = ""
        ds.Tables("PieceProcess").Rows(GridView2.FocusedRowHandle)("DPT_ID") = ""
        'GridView2.SetFocusedRowCellValue(DPT_Name, Nothing)
        'GridView2.SetFocusedRowCellValue(DPT_ID, Nothing)
    End Sub
    ''' <summary>
    ''' �u�������ȧ��ܮ�,�bgluPM_Type���[���������t��W��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPro_Type_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPro_Type.EditValueChanged
        gluPM_M_Code_EditValueChanged(Nothing, Nothing)     '�եβ��~�s���ȧ��ܹL�{
    End Sub
    ''' <summary>
    ''' �������T�w�����s�A�O�s�ƾ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim ppi As New ProductionPieceProcessInfo
        Dim j, i, k%
        isSameRow = False

        If gluPM_M_Code.Text = "" Then
            MsgBox("���~�s�����ର�šA�п�ܲ��~�s��!", 64, "����")
            gluPM_M_Code.Focus()
            Exit Sub
        ElseIf gluPM_Type.Text = "" Then
            MsgBox("�t��W�٤��ର�šA�п�ܰt��W��!", 64, "����")
            gluPM_Type.Focus()
            Exit Sub
        ElseIf txtPP_ActionUser.Text = "" Then
            MsgBox("�ާ@�H���ର�šA�п�J�ާ@�H!", 64, "����")
            txtPP_ActionUser.Focus()
            Exit Sub
        ElseIf dtePP_ActionDate.Text = "" Then
            MsgBox("�إߤ�����ର�šA�п�J�إߤ��!", 64, "����")
            dtePP_ActionDate.Focus()
            Exit Sub
        End If

        For i = 0 To GridView2.RowCount - 1
            GridView2.FocusedRowHandle = i
            If ds.Tables("PieceProcess").Rows(i)("PP_N_Name") = "" Then
                MsgBox("�p�u�ǦW�٤��ର�šA�п�J�p�u�ǦW��!", 64, "����")
                Exit Sub
            End If

            If Me.lblTittle.Text <> "�p��u����--���J" Then  '�K�[�u���ɤ��P�_
                If ds.Tables("PieceProcess").Rows(i)("PP_Price") < 0 Then
                    MsgBox("�u������p�󵥩�0�A�Э��s��J�u��!", 64, "����")
                    Exit Sub
                End If
            End If


            If ds.Tables("PieceProcess").Rows(i)("PP_BeginUseDate") Is DBNull.Value Then
                MsgBox("�ҥΤ�����ର�šA�п�J�ҥΤ��!", 64, "����")
                Exit Sub
            End If
            If ds.Tables("PieceProcess").Rows(i)("PP_EndUseDate") Is DBNull.Value Then
                MsgBox("����������ର�šA�п�J�������!", 64, "����")
                Exit Sub
            End If
            RepositoryItemCalcEdit1_Leave(Nothing, Nothing)     '�եΤu�����h�J�I�L�{
            If isSameRow = True Then    '�P�_�O�_�s�b�ۦP�O��
                Exit Sub
            End If
            If ds.Tables("PieceProcess").Rows(i)("PP_BeginUseDate") > ds.Tables("PieceProcess").Rows(i)("PP_EndUseDate") Then
                MsgBox("��J������~�A�ҥΤ������j�󵲧����!", 64, "����")
                Exit Sub
            End If
            If lblTittle.Text = "�p��u����--�f��" Or lblTittle.Text = "�p��u����--���J" Then
                For k = 0 To ds.Tables("PieceProcess").Rows.Count - 1
                    If i <> k Then
                        If ds.Tables("PieceProcess").Rows(i)("PS_NO") = ds.Tables("PieceProcess").Rows(k)("PS_NO") _
                            And ds.Tables("PieceProcess").Rows(i)("PP_N_Name") = ds.Tables("PieceProcess").Rows(k)("PP_N_Name") _
                            And ds.Tables("PieceProcess").Rows(i)("DPT_ID") = ds.Tables("PieceProcess").Rows(k)("DPT_ID") _
                            And ds.Tables("PieceProcess").Rows(i)("PP_BeginUse") = True _
                            And ds.Tables("PieceProcess").Rows(k)("PP_BeginUse") = True Then
                            MsgBox("�P�@�i�椤�u�ǬۦP�B�����]�ۦP���O������P�ɱҥΡI", 64, "����")
                            Exit Sub
                        End If
                    End If
                Next
            End If

        Next


        If lblTittle.Text = "�p��u����--�s�W" Then
            If AddData() = True Then
                MsgBox("�O���K�[����!", 64, "����")
                Me.Close()
            End If
        ElseIf lblTittle.Text = "�p��u����--�ק�" Then
            If EditData() = True Then

                MsgBox("�O���ק粒��!", 64, "����")
                Me.Close()
            End If
        ElseIf lblTittle.Text = "�p��u����--�f��" Then
            ppi.PP_Check = chkPP_Check.EditValue
            ppi.PP_CheckUserID = InUserID
            ppi.PP_CheckDate = Now
            For j = 0 To ds.Tables("PieceProcess").Rows.Count - 1
                GridView2.FocusedRowHandle = j
                ppi.AutoID = ds.Tables("PieceProcess").Rows(j)("AutoID")
                ppi.PP_BeginUse = ds.Tables("PieceProcess").Rows(j)("PP_BeginUse")
                If ppc.ProductionPieceProcess_Check(ppi) = False Then
                    If MsgBox("���Ǭ��G" & ds.Tables("PieceProcess").Rows(j)("PP_Num") & " ���O���f�֥���,�O�_�n�~��f�֨�᪺�O���H", MsgBoxStyle.YesNo + MsgBoxStyle.Information, "����") = MsgBoxResult.No Then
                        Exit Sub
                    End If
                End If
            Next
            MsgBox("�O���f�֧���!", 64, "����")
            Me.Close()

        ElseIf lblTittle.Text = "�p��u����--������J" Then
            If EditData() = True Then
                MsgBox("�O���ק粒��!", 64, "����")
                Me.Close()
            End If
        ElseIf lblTittle.Text = "�p��u����--����f��" Then

            ppi.PP_PriceCheckDate = Now
            ppi.PP_PriceCheckUserID = InUserID
            For j = 0 To ds.Tables("PieceProcess").Rows.Count - 1
                GridView2.FocusedRowHandle = j
                ppi.AutoID = ds.Tables("PieceProcess").Rows(j)("AutoID")
                ppi.PP_PriceCheck = ds.Tables("PieceProcess").Rows(j)("PP_PriceCheck")
                ppi.PP_FactorCheck = ds.Tables("PieceProcess").Rows(j)("PP_FactorCheck")

                ppi.PP_Price = ds.Tables("PieceProcess").Rows(j)("PP_Price")
                ppi.PP_Factor = ds.Tables("PieceProcess").Rows(j)("PP_Factor")

                If ppc.ProductionPieceProcessPrice_Check1(ppi) = False Then
                    If MsgBox("���Ǭ��G" & ds.Tables("PieceProcess").Rows(j)("PP_Num") & " ������f�֥���,�O�_�n�~��f�֨�᪺�O���H", MsgBoxStyle.YesNo + MsgBoxStyle.Information, "����") = MsgBoxResult.No Then
                        Exit Sub
                    End If
                End If
            Next
            MsgBox("����f�֧���!", 64, "����")
            Me.Close()



        ElseIf Me.lblTittle.Text = "�p��u����--���J" Then
            If EditDataRecord() = True Then
                MsgBox("�O�s���\!")
                Me.Close()
            End If

        End If
    End Sub
    ''' <summary>
    ''' ��������ȧ��ܮɡA�ѧE�ѼƤ]��ۧ���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemDateEdit2_DateTimeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemDateEdit2.DateTimeChanged
        If ds.Tables("PieceProcess").Rows(GridView2.FocusedRowHandle)("PP_BeginUseDate") Is DBNull.Value Then Exit Sub

        Dim ts As TimeSpan
        '�P�_��e����O�_�j��ҥΤ��
        If Now > CDate(GridView2.GetFocusedRowCellValue("PP_BeginUseDate")) Then
            ts = CDate(sender.Text) - CDate(Format(Now, "yyyy/MM/dd"))      '�ѧE�ѼƬ����������h��e���
        Else
            ts = CDate(sender.Text) - CDate(GridView2.GetFocusedRowCellValue("PP_BeginUseDate"))    '�ѧE�ѼƬ����������h�ҥΤ��
        End If

        ds.Tables("PieceProcess").Rows(GridView2.FocusedRowHandle)("RemnantDays") = ts.Days + 1     '�ѧE�Ѽƥ]�A����I����A�]���ݥ[1
    End Sub
    ''' <summary>
    ''' �ҥΤ���ȧ��ܮɡA�ѧE�ѼƤ]��ۧ���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemDateEdit1_DateTimeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemDateEdit1.DateTimeChanged
        If ds.Tables("PieceProcess").Rows(GridView2.FocusedRowHandle)("PP_EndUseDate") Is DBNull.Value Then Exit Sub

        Dim ts As TimeSpan
        '�P�_��e����O�_�j��ҥΤ��
        If Now > CDate(sender.Text) Then
            ts = CDate(GridView2.GetFocusedRowCellValue("PP_EndUseDate")) - CDate(Format(Now, "yyyy/MM/dd"))    '�ѧE�ѼƬ����������h��e���
        Else
            ts = CDate(GridView2.GetFocusedRowCellValue("PP_EndUseDate")) - CDate(sender.Text)      '�ѧE�ѼƬ����������h�ҥΤ��
        End If
        ds.Tables("PieceProcess").Rows(GridView2.FocusedRowHandle)("RemnantDays") = ts.Days + 1     '�ѧE�Ѽƥ]�A����I����A�]���ݥ[1
    End Sub
    ''' <summary>
    ''' �u�����󥢥h�J�I�ɡA�P�_�O�_���ۦP�u�ǡA�ۦP�������ۦP�u��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemCalcEdit1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemCalcEdit1.Leave
        Dim i%
        isSameRow = False   '��l���ܶq��False
        For i = 0 To ds.Tables("PieceProcess").Rows.Count - 1
            If GridView2.GetFocusedRowCellValue("PP_Num") <> ds.Tables("PieceProcess").Rows(i)("PP_Num") Then
                If GridView2.GetFocusedRowCellValue("PS_NO") = ds.Tables("PieceProcess").Rows(i)("PS_NO") _
                    And GridView2.GetFocusedRowCellValue("PP_N_Name") = ds.Tables("PieceProcess").Rows(i)("PP_N_Name") _
                    And GridView2.GetFocusedRowCellValue("DPT_ID") = ds.Tables("PieceProcess").Rows(i)("DPT_ID") _
                    And GridView2.GetFocusedRowCellValue("PP_Price") = ds.Tables("PieceProcess").Rows(i)("PP_Price") Then
                    MsgBox("�P�@�i�椤�ۦP���u�ǡA�ۦP���������঳�ۦP���u��!", 64, "����")
                    ds.Tables("PieceProcess").Rows(GridView2.FocusedRowHandle)("PP_Price") = 0
                    isSameRow = True    '�s�b�ۦP�O��
                End If
            End If
        Next
    End Sub
    ''' <summary>
    ''' �����������ҥΡ����s�A�ҥΩҦ��u��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAllUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllUse.Click
        Dim i%
        If btnAllUse.Text = "�����ҥ�(&A)" Then
            For i = 0 To ds.Tables("PieceProcess").Rows.Count - 1
                ds.Tables("PieceProcess").Rows(i)("PP_BeginUse") = True
            Next
            btnAllUse.Text = "�������ҥ�(&A)"
        ElseIf btnAllUse.Text = "�������ҥ�(&A)" Then
            For i = 0 To ds.Tables("PieceProcess").Rows.Count - 1
                ds.Tables("PieceProcess").Rows(i)("PP_BeginUse") = False
            Next
            btnAllUse.Text = "�����ҥ�(&A)"
        End If
    End Sub
    ''' <summary>
    ''' ���Ů�����ܤU�ԦC��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluPM_M_Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gluPM_M_Code.KeyDown, cboPro_Type.KeyDown, gluPM_Type.KeyDown, RepositoryItemComboBox1.KeyDown, RepositoryItemPopupContainerEdit1.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.showpopup()
        End If
    End Sub

    Private Sub chkPP_Check_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPP_Check.CheckedChanged
        If lblTittle.Text = "�p��u����--�f��" Then
            btnOK.Enabled = Not btnOK.Enabled
        End If
    End Sub


    Private Sub GridView2_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView2.RowCellStyle
        Dim strA, strB As Boolean
        On Error Resume Next

        If lblTittle.Text = "�p��u����--����f��" Or lblTittle.Text = "�p��u����--�f��" Then

            If lblTittle.Text = "�p��u����--����f��" Then
                strA = GridView2.GetFocusedRowCellValue("PP_PriceCheck")
                strB = GridView2.GetFocusedRowCellValue("PP_FactorCheck")

                If strA = True Then
                    PP_Price.OptionsColumn.ReadOnly = False
                Else
                    PP_Price.OptionsColumn.ReadOnly = True
                End If

                If strB = True Then
                    PP_Factor.OptionsColumn.ReadOnly = False
                Else
                    PP_Factor.OptionsColumn.ReadOnly = True
                End If
            End If
        Else

            strA = GridView2.GetFocusedRowCellValue("PP_PriceCheck")
            strB = GridView2.GetFocusedRowCellValue("PP_FactorCheck")

            If strA = True Then
                PP_Price.OptionsColumn.ReadOnly = True
                'PP_PriceCheck.OptionsColumn.ReadOnly = True
            Else
                PP_Price.OptionsColumn.ReadOnly = False
                'PP_PriceCheck.OptionsColumn.ReadOnly = False
            End If

            If strB = True Then
                PP_Factor.OptionsColumn.ReadOnly = True
                'PP_FactorCheck.OptionsColumn.ReadOnly = True
            Else
                PP_Factor.OptionsColumn.ReadOnly = False
                'PP_FactorCheck.OptionsColumn.ReadOnly = False 
            End If

            If strA = True Or strB = True Then
                PS_NO.OptionsColumn.ReadOnly = True
                DPT_PName.OptionsColumn.ReadOnly = True
                DPT_Name.OptionsColumn.ReadOnly = True
                'PP_N_Name.OptionsColumn.ReadOnly = True

                PP_Type.OptionsColumn.ReadOnly = True
                PP_Explain.OptionsColumn.ReadOnly = True
                'PP_BeginUseDate.OptionsColumn.ReadOnly = True
                'PP_EndUseDate.OptionsColumn.ReadOnly = True
                PP_BeginUse.OptionsColumn.ReadOnly = True
            Else
                PS_NO.OptionsColumn.ReadOnly = False
                DPT_PName.OptionsColumn.ReadOnly = False
                DPT_Name.OptionsColumn.ReadOnly = False
                PP_N_Name.OptionsColumn.ReadOnly = False


                PP_Type.OptionsColumn.ReadOnly = False
                PP_Explain.OptionsColumn.ReadOnly = False
                'PP_BeginUseDate.OptionsColumn.ReadOnly = False
                'PP_EndUseDate.OptionsColumn.ReadOnly = False
                PP_BeginUse.OptionsColumn.ReadOnly = False
            End If


        End If



    End Sub
    ''' <summary>
    ''' �έp�ק�O���b�W�[�ɭn�ק�]�f�֪��A�^
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function EditDataRecord() As Boolean
        Dim ppi As New ProductionPieceProcessInfo
        Dim i As Integer


        ppi.PP_ID = txtPP_ID.Text
        ppi.Pro_Type = cboPro_Type.Text
        ppi.PM_M_Code = gluPM_M_Code.EditValue
        ppi.PM_Type = gluPM_Type.Text
        ppi.PP_ActionUser = txtPP_ActionUser.Text

        ppi.PP_ActionDate = dtePP_ActionDate.EditValue
        ppi.PP_Remark = txtPP_Remark.Text
        For i = 0 To ds.Tables("PieceProcess").Rows.Count - 1
            '�P�_�O�_���s�W�ƾ�,AutoID���ŮɡA�h���s�W�ƾ�
            If ds.Tables("PieceProcess").Rows(i)("AutoID") Is DBNull.Value Then

                ppi.PP_Num = ds.Tables("PieceProcess").Rows(i)("PP_Num")
                ppi.PS_NO = ds.Tables("PieceProcess").Rows(i)("PS_NO")
                ppi.PP_N_Name = ds.Tables("PieceProcess").Rows(i)("PP_N_Name")
                ppi.DPT_ID = ds.Tables("PieceProcess").Rows(i)("DPT_ID")
                ppi.PP_Price = ds.Tables("PieceProcess").Rows(i)("PP_Price")

                ppi.PP_Factor = ds.Tables("PieceProcess").Rows(i)("PP_Factor")
                ppi.PP_Explain = ds.Tables("PieceProcess").Rows(i)("PP_Explain")
                ppi.PP_Type = ds.Tables("PieceProcess").Rows(i)("PP_Type")
                ppi.PP_BeginUseDate = ds.Tables("PieceProcess").Rows(i)("PP_BeginUseDate")
                ppi.PP_EndUseDate = ds.Tables("PieceProcess").Rows(i)("PP_EndUseDate")

                ppi.AddUserID = InUserID
                ppi.AddDate = Format(Now, "yyyy/MM/dd")

                ppi.PP_Check = True
                ppi.PP_BeginUse = True

                ''---------------20130327---------------------------------------------
                If Me.LabelCheckID.Text = "" Then
                Else
                    ppi.PP_CheckUserID = Me.LabelCheckID.Text
                End If

                If lblPP_CheckDate.Text <> "" Then
                    ppi.PP_CheckDate = Format(CDate(lblPP_CheckDate.Text), "yyyy-MM-dd")
                End If
                ''---------------------------------------------------------------------

                If ppc.ProductionPieceProcess_Add1(ppi) = True Then
                    EditDataRecord = True
                Else
                    If MsgBox("���Ǭ��G" & ds.Tables("PieceProcess").Rows(i)("PP_Num") & " �s�W�O���K�[����,�O�_�n�~��O�s��᪺�O���H", MsgBoxStyle.YesNo + MsgBoxStyle.Information, "����") = MsgBoxResult.No Then
                        EditDataRecord = False
                        Exit Function
                    End If
                End If
            Else
                ppi.AutoID = ds.Tables("PieceProcess").Rows(i)("AutoID")
                ppi.PP_Num = ds.Tables("PieceProcess").Rows(i)("PP_Num")
                ppi.PS_NO = ds.Tables("PieceProcess").Rows(i)("PS_NO")
                ppi.PP_N_Name = ds.Tables("PieceProcess").Rows(i)("PP_N_Name")
                ppi.DPT_ID = ds.Tables("PieceProcess").Rows(i)("DPT_ID")

                ppi.PP_Price = ds.Tables("PieceProcess").Rows(i)("PP_Price")
                ppi.PP_Factor = ds.Tables("PieceProcess").Rows(i)("PP_Factor")
                ppi.PP_Explain = ds.Tables("PieceProcess").Rows(i)("PP_Explain")
                ppi.PP_Type = ds.Tables("PieceProcess").Rows(i)("PP_Type")
                ppi.PP_BeginUseDate = ds.Tables("PieceProcess").Rows(i)("PP_BeginUseDate")

                ppi.PP_EndUseDate = ds.Tables("PieceProcess").Rows(i)("PP_EndUseDate")
                ppi.ModifyUserID = InUserID
                ppi.ModifyDate = Format(Now, "yyyy/MM/dd")

                If ppc.ProductionPieceProcess_Update(ppi) = True Then
                    EditDataRecord = True
                Else
                    If MsgBox("���Ǭ��G" & ds.Tables("PieceProcess").Rows(i)("PP_Num") & " �O���ק異��,�O�_�n�~��ק��᪺�O���H", MsgBoxStyle.YesNo + MsgBoxStyle.Information, "����") = MsgBoxResult.No Then
                        EditDataRecord = False
                        Exit Function
                    End If
                End If
            End If
        Next
    End Function

    Function LoadPS_NO() As Boolean '2013-3-19

        If cboPro_Type.Text.Trim = "" Or gluPM_M_Code.Text.Trim = "" Or gluPM_Type.Text.Trim = "" Then
            Exit Function
        End If

        Dim pc As New ProcessMainControl

        RepositoryItemLookUpEdit5.DisplayMember = "PS_Name"
        RepositoryItemLookUpEdit5.ValueMember = "PS_NO"
        RepositoryItemLookUpEdit5.DataSource = pc.ProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, cboPro_Type.Text, gluPM_Type.EditValue, Nothing, Nothing)
    End Function



    Private Sub RepositoryItemCalcEdit1_Spin(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles RepositoryItemCalcEdit1.Spin
        e.Handled = True

    End Sub

    Private Sub RepositoryItemCalcEdit2_Spin(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles RepositoryItemCalcEdit2.Spin
        e.Handled = True
    End Sub

End Class