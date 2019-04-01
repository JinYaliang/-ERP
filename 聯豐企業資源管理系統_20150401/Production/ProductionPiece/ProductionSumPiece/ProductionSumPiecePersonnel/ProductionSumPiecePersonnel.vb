Imports LFERP.Library.ProductionPieceProcess
Imports LFERP.Library.ProductionPiecePersonnel
Imports LFERP.Library.ProductionSumPiecePersonnel
Imports LFERP.Library.ProductionPiecePersonnelDay

Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Production.Datasetting
Imports LFERP.DataSetting
Imports LFERP.Library.ProductionPiecePersonnelMothClass


Imports LFERP.Library.ProductionSumTimePersonnel
Imports LFERP.Library.ProductionSumTimeSetting


Public Class ProductionSumPiecePersonnel
    Dim StrTypeLabel As String '���� 
    Dim StrPP_NO As String

    Dim Load_OK As String ''�T�wLoad�ƥ�O�_�w���J����
    Dim ds As New DataSet

    Private Sub ProductionSumPiecePersonnel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Load_OK = ""

        Clr_Text()

        ButtSing.Enabled = True
        cmdAdd.Visible = True
        GLookUpEditPS_NO.Enabled = True
        cmdSave.Visible = True
        cmdAdd.Visible = True



        StrPP_NO = tempValue2 '�s��
        StrTypeLabel = MTypeName  '�ާ@����

        tempValue2 = Nothing
        MTypeName = Nothing
        tempValue4 = Nothing
        tempValue3 = Nothing

        CreateTable()

        PP_DateEdit.EditValue = Format(Now, "yyyy/MM/dd")

        '----------------------------------------------------------------------
        GridLookSampType.Properties.ValueMember = "SampID"
        GridLookSampType.Properties.DisplayMember = "SampName"
        Dim Psam As New LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeControl

        If strInUserRank = "�έp" Then
            GridLookSampType.Properties.DataSource = Psam.ProductionPiecePaySampType_GetList(Nothing, Nothing, True, strInDepIDFull)
        Else
            GridLookSampType.Properties.DataSource = Psam.ProductionPiecePaySampType_GetList(Nothing, Nothing, True, Nothing)
        End If

        'GridLookSampType.EditValue = "Z"

        '----------------------------------------------------------------------


        'Dim ptc As New ProductionPiecePersonnelDayControl   ''���J ���u�s��---�m�W---���� �ӤH�p��[�J�L�էO��
        'txtPer_NO.Properties.DataSource = ptc.ProductionPiecePersonnelDay_GetList1("�L", Nothing, Nothing, strInDepID, Nothing, PP_DateEdit.EditValue, "False", PP_DateEdit.EditValue, "=")
        'txtPer_NO.Properties.DisplayMember = "Per_Name"
        'txtPer_NO.Properties.ValueMember = "Per_NO"

        If StrTypeLabel <> "PPView" Then
            Dim pc As New ProcessMainControl
            Dim pdsi As List(Of ProductionDataSettingInfo)
            Dim pdsc As New ProductionDataSettingControl

            gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
            gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
            gluPM_M_Code.Properties.DataSource = pc.ProcessMain_GetList3(Nothing, Nothing)

            If strInUserRank = "�έp" Then
                pdsi = pdsc.ProductionUser_GetList(strInDepIDFull, Nothing)
                If pdsi.Count > 0 Then
                    gluPM_M_Code.Properties.DataSource = pdsc.ProductionUser_GetList(strInDepIDFull, Nothing)
                End If
            End If
        End If

        '
        Select Case StrTypeLabel
            Case "PPAdd"
                Me.Text = "���u�p��--�W�["
                Me.LabCaption.Text = "���u�p��--�W�["
            Case "PPEdit"
                If LoadData(StrPP_NO) = False Then Exit Sub
                Me.Text = "���u�p��--�ק�" & "[" & StrPP_NO & "]"
                Me.LabCaption.Text = "���u�p��--�ק�"
                cmdAdd.Visible = False
                ButtSing.Enabled = False   ''�ק�u����

            Case "PPView"

                If LoadData(StrPP_NO) = False Then Exit Sub
                '�d��
                cmdSave.Visible = False
                cmdAdd.Visible = False
                ButtSing.Enabled = False

                Me.LabCaption.Text = "���u�p��--�d��"
                Me.Text = "���u�p��--�d��" & "[" & StrPP_NO & "]"
        End Select

        ButtSing.Text = "���"
        Me.Width = ButtSing.Location.X + ButtSing.Width + 10
        Me.Left = Me.Left + Grid1.Width / 2

        If StrTypeLabel = "PPEdit" Then
            txtPP_Qty.Select()
        Else
            txtPer_NO.Select()
            txtPer_NO.Focus()
        End If

        Load_OK = "OK"

        Me.GLookUpEditPS_NO.Properties.PopupFormWidth = 700

    End Sub

    Sub CreateTable()
        ds.Tables.Clear()
        '�p��u����()
        With ds.Tables.Add("PieceProcess")
            .Columns.Add("GoIn", GetType(Boolean))
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))

            .Columns.Add("PP_N_Name", GetType(String))
            .Columns.Add("DPT_ID", GetType(String))
            .Columns.Add("DPT_Name", GetType(String))
            .Columns.Add("DPT_PName", GetType(String))
            .Columns.Add("PP_Price", GetType(Single))
            .Columns.Add("PP_BeginUse", GetType(Boolean))
            'PP_BeginUseDate
            .Columns.Add("PP_BeginUseDate", GetType(Date))
            .Columns.Add("PP_EndUseDate", GetType(Date))

            .Columns.Add("PP_ID", GetType(String))
        End With

        ds.Tables("PieceProcess").Clear()

        Grid1.DataSource = ds.Tables("PieceProcess")

        GLookUpEditPS_NO.Properties.DisplayMember = "PP_N_Name"
        GLookUpEditPS_NO.Properties.ValueMember = "AutoID"
        GLookUpEditPS_NO.Properties.DataSource = ds.Tables("PieceProcess")


        '�t��W�٪�
        With ds.Tables.Add("ProductType")
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
        End With

        gluPM_Type.Properties.DisplayMember = "PM_Type"
        gluPM_Type.Properties.ValueMember = "PM_Type"
        gluPM_Type.Properties.DataSource = ds.Tables("ProductType")

        ''2012-5-11
        With ds.Tables.Add("Per_NONameT")
            .Columns.Add("Per_NO", GetType(String))
            .Columns.Add("Per_Name", GetType(String))
            .Columns.Add("DepID", GetType(String))
            .Columns.Add("FacID", GetType(String))
            .Columns.Add("Per_DepName", GetType(String))
            .Columns.Add("Per_FacName", GetType(String)) '
            .Columns.Add("Per_NOName", GetType(String))
        End With
        '
        txtPer_NO.Properties.DisplayMember = "Per_NOName"
        txtPer_NO.Properties.ValueMember = "Per_NO"
        txtPer_NO.Properties.DataSource = ds.Tables("Per_NONameT")

    End Sub


    ''' <summary>
    ''' ���J��Ѥ��b�էO�M�էO�s�������L�����ӤH�p��H���W�� 2012/6/4 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Load_Table_New() As String
        Load_Table_New = ""

        Dim i As Integer
        Dim ptil As New List(Of ProductionPiecePersonnelDayInfo)
        Dim ptc As New ProductionPiecePersonnelDayControl
        ptil = ptc.ProductionPiecePersonnelTimeDay_GetList(PP_DateEdit.EditValue, strInDepID)

        ds.Tables("Per_NONameT").Clear()

        If ptil.Count > 0 Then
            For i = 0 To ptil.Count - 1
                Dim row As DataRow
                row = ds.Tables("Per_NONameT").NewRow

                row("Per_NO") = ptil(i).Per_NO
                row("Per_Name") = ptil(i).Per_Name

                row("DepID") = ptil(i).DepID
                row("FacID") = ptil(i).FacID

                row("Per_DepName") = ptil(i).Per_DepName
                row("Per_FacName") = ptil(i).Per_FacName

                row("Per_NOName") = ptil(i).Per_NO & "  " & ptil(i).Per_Name

                ds.Tables("Per_NONameT").Rows.Add(row)
            Next
        End If

        'Dim i, count1 As Integer
        'Dim j, count2 As Integer

        'ds.Tables("Per_NONameT").Clear()

        'Dim ptil As New List(Of ProductionPiecePersonnelInfo)
        'Dim ptc As New ProductionPiecePersonnelControl   '

        'ptil = ptc.ProductionPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing, "False", Nothing)
        'count1 = ptil.Count
        'If count1 > 0 Then
        '    For i = 0 To count1 - 1

        '        Dim ppil As New List(Of ProductionPiecePersonnelDayInfo)
        '        Dim ppc As New ProductionPiecePersonnelDayControl   '

        '        ppil = ppc.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, ptil(i).Per_NO, Nothing, Nothing, strInDepID, Nothing, Nothing, PP_DateEdit.EditValue, Nothing, "False", PP_DateEdit.EditValue)
        '        count2 = ppil.Count

        '        If count2 <= 0 Or ptil(i).G_NO = "�L" Then
        '            Dim row As DataRow
        '            row = ds.Tables("Per_NONameT").NewRow

        '            row("Per_NO") = ptil(i).Per_NO
        '            row("Per_Name") = ptil(i).Per_Name

        '            row("DepID") = ptil(i).DepID
        '            row("FacID") = ptil(i).FacID

        '            row("Per_DepName") = ptil(i).Per_DepName
        '            row("Per_FacName") = ptil(i).Per_FacName

        '            row("Per_NOName") = ptil(i).Per_NO & "  " & ptil(i).Per_Name

        '            ds.Tables("Per_NONameT").Rows.Add(row)
        '        End If
        '    Next
        'End If


    End Function


    ''' <summary>
    ''' ��������''2012-5-11
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Load_Table() As String
        Load_Table = ""

        Dim i, count1 As Integer
        Dim j, count2 As Integer

        ds.Tables("Per_NONameT").Clear()

        Dim ptil As New List(Of ProductionPiecePersonnelInfo)  ''���J�򥻪������L���O��
        Dim ptc As New ProductionPiecePersonnelControl   '

        ptil = ptc.ProductionPiecePersonnel_GetList1("�L", Nothing, Nothing, strInDepID, Nothing, Nothing, "False", Nothing, "=")
        count1 = ptil.Count
        If count1 > 0 Then
            For i = 0 To count1 - 1
                Dim row As DataRow
                row = ds.Tables("Per_NONameT").NewRow

                row("Per_NO") = ptil(i).Per_NO
                row("Per_Name") = ptil(i).Per_Name

                row("DepID") = ptil(i).DepID
                row("FacID") = ptil(i).FacID

                row("Per_DepName") = ptil(i).Per_DepName
                row("Per_FacName") = ptil(i).Per_FacName

                ds.Tables("Per_NONameT").Rows.Add(row)
            Next
        End If

        ''---------------------------------------------------------------------------------------------
        '�q�C����[�J

        Dim ppil As New List(Of ProductionPiecePersonnelDayInfo)  ''���J�򥻪������L���O��
        Dim ppc As New ProductionPiecePersonnelDayControl   '

        ppil = ppc.ProductionPiecePersonnelDay_GetList1(Nothing, Nothing, Nothing, strInDepID, Nothing, PP_DateEdit.EditValue, "False", PP_DateEdit.EditValue, Nothing)
        count2 = ppil.Count
        If count2 > 0 Then
            For j = 0 To count2 - 1
                Dim row As DataRow
                row = ds.Tables("Per_NONameT").NewRow

                row("Per_NO") = ppil(j).Per_NO
                row("Per_Name") = ppil(j).Per_Name

                row("DepID") = ppil(j).DepID
                row("FacID") = ppil(j).FacID

                row("Per_DepName") = ppil(j).Per_DepName
                row("Per_FacName") = ppil(j).Per_FacName

                ds.Tables("Per_NONameT").Rows.Add(row)
                ' GridView2.MoveLast()
            Next
        End If

    End Function

    Function LoadData(ByVal StrPP_NO As String) As Boolean
        LoadData = False

        Dim objInfo As New ProductionSumPiecePersonnelInfo
        Dim objList As New List(Of ProductionSumPiecePersonnelInfo)
        Dim oc As New ProductionSumPiecePersonnelControl

        objList = oc.ProductionSumPiecePersonnel_GetList(Nothing, StrPP_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If objList.Count <= 0 Then
            MsgBox("�S���ƾڡI")
            LoadData = False
            Exit Function
        Else
            PP_DateEdit.EditValue = objList(0).PP_Date

            If StrTypeLabel = "PPView" Then
                gluPM_M_Code.Enabled = False
                gluPM_M_Code.Properties.NullText = objList(0).PM_M_Code.ToString  '���~�s��

                gluPM_Type.Enabled = False
                gluPM_Type.Properties.NullText = objList(0).PM_Type.ToString    '����

                GLookUpEditPS_NO.Enabled = False
                GLookUpEditPS_NO.Properties.NullText = objList(0).PS_NameS
            Else

                gluPM_M_Code.EditValue = objList(0).PM_M_Code.ToString  '���~�s��
                gluPM_Type.EditValue = objList(0).PM_Type.ToString    '����
                GLookUpEditPS_NO.EditValue = objList(0).PP_AutoID
            End If

            cboPro_Type.Text = objList(0).Pro_Type.ToString  '�u������ 
            txtPP_Qty.Text = objList(0).PP_Qty.ToString

            MemoPP_Remark.Text = objList(0).PP_Remark '�ƪ`

            labDepID.Text = objList(0).DepID  ' ����        ��Ū���A�H���ھ� ���u�s���s�J
            labFacID.Text = objList(0).FacID  ' �t�O  

            cboPer_Class.EditValue = objList(0).Per_Class



            '-------------�b�ק��--�p�d�G�O���������A�N���J���F�A�ҥH�[�@���O��--------------------------------------------------------------
            Dim j As Integer
            Dim strBz As String

            strBz = ""
            If ds.Tables("Per_NONameT").Rows.Count > 0 Then
                For j = 0 To ds.Tables("Per_NONameT").Rows.Count - 1
                    If objList(0).Per_NO = ds.Tables("Per_NONameT").Rows(j)("Per_NO") Then
                        strBz = "Y"
                    End If
                Next
            End If

            If strBz = "" Then
                Dim row As DataRow
                row = ds.Tables("Per_NONameT").NewRow

                row("Per_NO") = objList(0).Per_NO
                row("Per_Name") = objList(0).PP_Per_Name
                row("Per_NOName") = objList(0).Per_NO & "  " & objList(0).PP_Per_Name

                row("DepID") = objList(0).DepID
                row("FacID") = objList(0).FacID

                row("Per_DepName") = objList(0).PP_DepName
                row("Per_FacName") = objList(0).PP_FacName

                ds.Tables("Per_NONameT").Rows.Add(row)
            End If

            txtPer_NO.EditValue = objList(0).Per_NO
            '---------------------------------------------------------------------------------------------------
            LoadData = True
        End If


        LoadDataPT(StrPP_NO)


    End Function



    Private Function CheckData() As Boolean
        CheckData = True

        If txtPer_NO.EditValue = "" Then
            MsgBox("�п�ܭ��u�s��")
            txtPer_NO.Select()
            CheckData = False
            Exit Function
        End If

        If labDepID.Text = "labFacID" Or labDepID.Text = "" Then
            MsgBox("���u���ݳ�������,�Э��s��ܭ��u�s��!")
            txtPer_NO.Select()
            CheckData = False
            Exit Function
        End If


        If gluPM_M_Code.EditValue = "" Then
            MsgBox("�п�J���~�s���A���ˬd")
            gluPM_M_Code.Focus()
            CheckData = False
            Exit Function
        End If

        If cboPer_Class.EditValue = "" Then
            MsgBox("�п�J���u�Z��H���A���ˬd")
            cboPer_Class.Focus()
            CheckData = False
            Exit Function
        End If

        If gluPM_Type.EditValue = "" Then
            MsgBox("�п�J�t��W�١A���ˬd")
            gluPM_Type.Focus()
            CheckData = False
            Exit Function
        End If

        If Val(txtPP_Qty.Text) <= 0 Then
            MsgBox("�п�J�ƶq�A���ˬd")
            txtPP_Qty.Focus()
            CheckData = False
            Exit Function
        End If

        Dim i, n As Integer

        If ButtSing.Text = "���" Then
            If GLookUpEditPS_NO.EditValue = "" Then
                MsgBox("�п�ܤu�ǦW�١A���ˬd")
                GLookUpEditPS_NO.Focus()
                CheckData = False
                Exit Function
            End If

            ''--------------------------------------------------------------
            Dim pcc As New ProductionPieceProcessControl
            Dim pci As New List(Of ProductionPieceProcessInfo)

            pci = pcc.ProductionPieceProcess_GetList(GLookUpEditPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If pci(0).DPT_ID <> strInDepIDFull Then
                MsgBox("���������s�b���p��u��," & "  ���ˬd!")
                GLookUpEditPS_NO.Focus()
                CheckData = False
                Exit Function
            End If
            ''----------------------------------------------------------------

        Else
            If ds.Tables("PieceProcess").Rows.Count >= 1 Then
            Else
                CheckData = False
                MsgBox("�����~�t�󥼫إ߭p��u���y�{,���ˬd!")
                Exit Function
            End If

            For i = 0 To ds.Tables("PieceProcess").Rows.Count - 1
                If ds.Tables("PieceProcess").Rows(i)("GoIn") = True Then

                    ''--------------------------------------------------------------
                    Dim pcc As New ProductionPieceProcessControl
                    Dim pci As List(Of ProductionPieceProcessInfo)

                    pci = pcc.ProductionPieceProcess_GetList(ds.Tables("PieceProcess").Rows(i)("AutoID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    If pci(0).DPT_ID <> strInDepIDFull Then
                        MsgBox("���������s�b�p��u��," & ds.Tables("PieceProcess").Rows(i)("PP_N_Name") & "  ���ˬd!")

                        CheckData = False
                        Exit Function
                    End If
                    ''--------------------------------------------------------------

                    n = n + 1
                End If
            Next

            If n > 0 Then
            Else
                CheckData = False
                MsgBox("�A���[�J�u�ǫH���A���ˬd")
                Exit Function
            End If
        End If


        ''------------------------------------------------------------------------

        If cboPer_Class.EditValue = "" Then
            MsgBox("�Z��H�����ର��!")
            cboPer_Class.Select()
        End If

        Dim TempStr As String
        TempStr = Find_Per_Class()

        If TempStr <> "" Then
            If cboPer_Class.EditValue <> TempStr Then
                If MsgBox("�����u���s�b���P�Z��H��,�T�w�O�_�O�s�H", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Else
                    CheckData = False
                    Exit Function
                End If
            End If
        End If


        Dim pcA As New LFERP.Library.ProductionSumLock.ProductionSumLockControl
        Dim plA As New List(Of LFERP.Library.ProductionSumLock.ProductionSumLockInfo)
        plA = pcA.ProductionSumLock_GetList(Nothing, Nothing, labDepID.Text, Format(CDate(PP_DateEdit.EditValue), "yyyy/MM"))

        If plA.Count > 0 Then
            If plA(0).LockCheck = True Then
                MsgBox("��e�����O���w��w������J!")
                CheckData = False
                Exit Function
            End If
        End If



    End Function

    ''' <summary>
    ''' ���O���O�s
    ''' </summary>
    ''' <param name="S_model"></param>
    ''' <remarks></remarks>
    Private Sub SaveNewOne(ByVal S_model As String)
        Dim gc As New ProductionSumPiecePersonnelControl
        Dim gi As New ProductionSumPiecePersonnelInfo
        Dim StrPP_NO_LS As String

        gi.Per_NO = txtPer_NO.EditValue.Trim '���u�u��

        gi.Pro_Type = cboPro_Type.Text
        gi.PM_M_Code = gluPM_M_Code.EditValue
        gi.PM_Type = gluPM_Type.EditValue
        gi.PP_Qty = Val(txtPP_Qty.Text)   ''

        gi.PP_Date = PP_DateEdit.Text
        gi.PP_Action = InUserID

        StrPP_NO_LS = ProductionSumPiecePersonnelNO()

        If StrPP_NO_LS <> "" Then
        Else
            MsgBox("�y����������ѡA�Э���!")
        End If

        gi.PP_NO = StrPP_NO_LS  ''�n�����o�s��

        ''�ھ�AutoID�d��

        If GLookUpEditPS_NO.EditValue <> "" Then
        Else
            MsgBox("�п�ܤu��")
        End If
        Dim pcc As New ProductionPieceProcessControl
        Dim pci As List(Of ProductionPieceProcessInfo)

        pci = pcc.ProductionPieceProcess_GetList(GLookUpEditPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pci.Count <= 0 Then
            MsgBox("�p��u���y�{���J����,���ˬd")
            Exit Sub
        End If

        gi.PS_NO = pci(0).PS_NO.ToString  ''�j�u�ǽs��
        gi.PS_NameS = pci(0).PP_N_Name.ToString '�p�u�ǦW��

        gi.PP_Price = pci(0).PP_Price.ToString   ''�u��
        ''autoID �]�s�@�U
        gi.PP_AutoID = GLookUpEditPS_NO.EditValue
        gi.PP_Remark = MemoPP_Remark.Text

        '' ''�����A�t�O �ھڭ��u�s�� �q�p��H���򥻫H�����d�ߥX�s�A �������H���u���������u��
        ''Dim ptc As New ProductionPiecePersonnelControl
        ''Dim pti As New List(Of ProductionPiecePersonnelInfo)
        ''pti = ptc.ProductionPiecePersonnel_GetList(Nothing, txtPer_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "False", Nothing)
        ''If pti.Count <= 0 Then
        ''    Exit Sub
        ''End If
        ''gi.DepID = pti(0).DepID   ''�����s��
        ''gi.FacID = pti(0).FacID   '�t�O 

        gi.DepID = labDepID.Text    ''�����s��
        gi.FacID = labFacID.Text    '�t�O 

        If Find_Per_Class() = "" Then
            Add_Per_Class()
        Else
            Update_Per_Class()
        End If


        If SaveNewPT(StrPP_NO_LS) = False Then
            MsgBox("�p�ɫO�s����!")
            Exit Sub
        End If


        If S_model = "S" Then
            If gc.ProductionSumPiecePersonnel_Add(gi) = True Then
                MsgBox("�ƾګO�s���\")
                Me.Close()
            End If
        Else
            If gc.ProductionSumPiecePersonnel_Add(gi) = False Then
                MsgBox("�ƾګO�s����")
                Me.Close()
            Else
                ' Clr_Text()
                Clr_TextNo_Per_NO()
            End If
        End If

    End Sub
    ''' <summary>
    ''' �h��O���O�s
    ''' </summary>
    ''' <param name="S_model"></param>
    ''' <remarks></remarks>
    Private Sub SaveNewTwo(ByVal S_model As String)
        Dim gc As New ProductionSumPiecePersonnelControl
        Dim gi As New ProductionSumPiecePersonnelInfo

        Dim i As Integer

        Dim StrPP_NO_LS As String

        gi.Per_NO = txtPer_NO.EditValue.Trim '���u�u��

        gi.Pro_Type = cboPro_Type.Text
        gi.PM_M_Code = gluPM_M_Code.EditValue
        gi.PM_Type = gluPM_Type.EditValue
        gi.PP_Qty = Val(txtPP_Qty.Text)   ''

        gi.PP_Date = PP_DateEdit.Text
        gi.PP_Action = InUserID
        gi.PP_Remark = MemoPP_Remark.Text

        If ds.Tables("PieceProcess").Rows.Count >= 1 Then
        Else
            MsgBox("�u�ǦW�٥����T���,���ˬd!")
            Exit Sub
        End If

        '' ''�����A�t�O �ھڭ��u�s�� �q�p��H���򥻫H�����d�ߥX�s�A �������H���u���������u��
        ''Dim ptc As New ProductionPiecePersonnelControl
        ''Dim pti As New List(Of ProductionPiecePersonnelInfo)
        ''pti = ptc.ProductionPiecePersonnel_GetList(Nothing, txtPer_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "False", Nothing)
        ''If pti.Count <= 0 Then
        ''    Exit Sub
        ''End If
        ''gi.DepID = pti(0).DepID   ''�����s��
        ''gi.FacID = pti(0).FacID   '�t�O 

        gi.DepID = labDepID.Text    ''�����s��
        gi.FacID = labFacID.Text    '�t�O 

        gi.DepID = strInDepIDFull
        gi.FacID = strInFacIDFull

        For i = 0 To ds.Tables("PieceProcess").Rows.Count - 1
            If ds.Tables("PieceProcess").Rows(i)("GoIn") = True Then

                StrPP_NO_LS = ProductionSumPiecePersonnelNO()
                If StrPP_NO_LS <> "" Then
                Else
                    MsgBox("�y����������ѡA�Э���!")
                End If
                gi.PP_NO = StrPP_NO_LS  ''�n�����o�s��

                gi.PS_NO = ds.Tables("PieceProcess").Rows(i)("PS_NO")  ''�j�u�ǽs��
                gi.PS_NameS = ds.Tables("PieceProcess").Rows(i)("PP_N_Name") '�p�u�ǦW��

                gi.PP_Price = ds.Tables("PieceProcess").Rows(i)("PP_Price").ToString  ''�u��
                gi.PP_AutoID = ds.Tables("PieceProcess").Rows(i)("AutoID") ''�u��autoID �]�s�@�U

                If gc.ProductionSumPiecePersonnel_Add(gi) = False Then
                    MsgBox("�ƾګO�s����,���ˬd")
                    Exit Sub
                End If
            End If
        Next

        If Find_Per_Class() = "" Then
            Add_Per_Class()
        Else
            Update_Per_Class()
        End If


        SaveNewPT(StrPP_NO_LS)

        If S_model = "S" Then
            MsgBox("�ƾګO�s���\")
            Me.Close()
        Else
            ' Clr_Text()
            Clr_TextNo_Per_NO()
            ds.Tables("PieceProcess").Clear()
        End If


    End Sub
    Sub Clr_TextNo_Per_NO()
        ' txtPer_NO.EditValue = Nothing
        gluPM_M_Code.EditValue = Nothing
        gluPM_Type.EditValue = Nothing
        ' txtPP_Qty.Text = ""
        MemoPP_Remark.Text = ""
        GLookUpEditPS_NO.EditValue = Nothing

        'labDepID.Text = ""
        'labFacID.Text = ""
        gluPM_M_Code.Focus()

        StartTimeEdit.Text = "00:00"
        EndTimeEdit.Text = "00:00"
        txtTotal.Text = ""
        MemoPT_Remark.Text = ""
        LabPT_NO.Text = ""
    End Sub
    Sub Clr_Text()
        txtPer_NO.EditValue = Nothing
        gluPM_M_Code.EditValue = Nothing
        gluPM_Type.EditValue = Nothing
        txtPP_Qty.Text = ""
        MemoPP_Remark.Text = ""
        GLookUpEditPS_NO.EditValue = Nothing

        labDepID.Text = ""
        labFacID.Text = ""
        txtPer_NO.Focus()

        StartTimeEdit.Text = "00:00"
        EndTimeEdit.Text = "00:00"
        txtTotal.Text = ""
        MemoPT_Remark.Text = ""
        LabPT_NO.Text = ""

    End Sub


    Function ProductionSumPiecePersonnelNO() As String

        ProductionSumPiecePersonnelNO = ""

        Dim str1, str2 As String
        Dim gc1 As New ProductionSumPiecePersonnelControl
        Dim gi1 As New ProductionSumPiecePersonnelInfo

        str1 = Mid(Year(Now), 3)
        If CInt(Month(Now)) < 10 Then
            str2 = "0" & Month(Now)
        Else
            str2 = Month(Now)
        End If

        Dim Stra As String
        Stra = Trim(str1 & str2)

        gi1 = gc1.ProductionSumPiecePersonnel_GetNO(Stra) '' Ū�����

        If gi1 Is Nothing Then
            ProductionSumPiecePersonnelNO = "PP" & str1 & str2 & "0000001"
        Else
            ProductionSumPiecePersonnelNO = "PP" & str1 & str2 & Mid((CInt(Mid(gi1.PP_NO, 7)) + 10000001), 2)
        End If
    End Function

    ''' <summary>
    ''' �ק�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveEdit()
        Dim gc As New ProductionSumPiecePersonnelControl
        Dim gi As New ProductionSumPiecePersonnelInfo

        gi.Per_NO = txtPer_NO.EditValue.Trim '���u�u��

        gi.Pro_Type = cboPro_Type.Text
        gi.PM_M_Code = gluPM_M_Code.EditValue
        gi.PM_Type = gluPM_Type.EditValue
        gi.PP_Qty = Val(txtPP_Qty.Text)   ''

        gi.PP_Date = PP_DateEdit.Text
        gi.PP_Action = InUserID

        'StrPP_NO = StrPP_NO

        gi.PP_Remark = MemoPP_Remark.Text

        If StrPP_NO <> "" Then
        Else
            MsgBox("�y����������ѡA�Э���!")
        End If

        gi.PP_NO = StrPP_NO  ''�n�����o�s��

        ' ''�����A�t�O �ھڭ��u�s�� �q�p��H���򥻫H�����d�ߥX�s�A �������H���u���������u��
        'Dim ptc As New ProductionPiecePersonnelControl
        'Dim pti As New List(Of ProductionPiecePersonnelInfo)
        'pti = ptc.ProductionPiecePersonnel_GetList(Nothing, txtPer_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "False", Nothing)
        'If pti.Count <= 0 Then
        '    Exit Sub
        'End If
        'gi.DepID = pti(0).DepID   ''�����s��
        'gi.FacID = pti(0).FacID   '�t�O 

        gi.DepID = labDepID.Text    ''�����s��
        gi.FacID = labFacID.Text    '�t�O 



        ''�ھ�AutoID�d��------------------------------------------

        If GLookUpEditPS_NO.EditValue <> "" Then
        Else
            MsgBox("�п�ܤu��")
        End If
        Dim pcc As New ProductionPieceProcessControl
        Dim pci As List(Of ProductionPieceProcessInfo)

        pci = pcc.ProductionPieceProcess_GetList(GLookUpEditPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pci.Count <= 0 Then
            MsgBox("�p��u���y�{���J����,���ˬd")
            Exit Sub
        End If

        gi.PS_NO = pci(0).PS_NO.ToString  ''�j�u�ǽs��
        gi.PS_NameS = pci(0).PP_N_Name.ToString '�p�u�ǦW��
        gi.PP_Price = pci(0).PP_Price  ''�u��
        ''autoID �]�s�@�U
        gi.PP_AutoID = GLookUpEditPS_NO.EditValue
        ' gi.DepID = pci(0).DPT_ID  ''����
        ''-------------------------------------------------------------------

        If Find_Per_Class() = "" Then
            Add_Per_Class()
        Else
            Update_Per_Class()
        End If

        If gc.ProductionSumPiecePersonnel_Update(gi) = True Then
            MsgBox("�ƾګO�s���\")
            Me.Close()
        End If

    End Sub


    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub gluPM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_M_Code.EditValueChanged
        If StrTypeLabel = "PPView" Then Exit Sub

        If gluPM_M_Code.EditValue <> "" Then
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
                'gluPM_Type.Text = ""
            End If
        End If
    End Sub

    Private Sub gluPM_Type_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_Type.EditValueChanged
        If StrTypeLabel = "PPView" Then Exit Sub

        '���J �p��u����
        If gluPM_Type.EditValue <> "" Then

            Dim pcc As New ProductionPieceProcessControl
            Dim pci As List(Of ProductionPieceProcessInfo)

            pci = pcc.ProductionPieceProcess_GetList(Nothing, Nothing, cboPro_Type.Text, gluPM_M_Code.EditValue, gluPM_Type.EditValue, Nothing, Nothing, strInDepID, True, True, Nothing, Nothing)
            If pci.Count = 0 Then
                ds.Tables("PieceProcess").Clear()
            Else
                ds.Tables("PieceProcess").Clear()

                Dim i As Integer
                For i = 0 To pci.Count - 1


                    Dim row As DataRow
                    row = ds.Tables("PieceProcess").NewRow
                    'MsgBox(pci(i).PP_EndUseDate)
                    If PP_DateEdit.EditValue >= pci(i).PP_BeginUseDate And PP_DateEdit.EditValue <= pci(i).PP_EndUseDate Then

                        row("GoIn") = False
                        row("AutoID") = pci(i).AutoID
                        row("DPT_Name") = pci(i).DPT_Name
                        row("PS_Name") = pci(i).PS_Name
                        row("PS_NO") = pci(i).PS_NO

                        row("PP_N_Name") = pci(i).PP_N_Name
                        row("PP_Price") = pci(i).PP_Price
                        row("PP_BeginUse") = pci(i).PP_BeginUse

                        row("DPT_PName") = pci(i).DPT_PName
                        row("DPT_ID") = pci(i).DPT_ID
                        row("DPT_Name") = pci(i).DPT_Name

                        row("PP_BeginUseDate") = pci(i).PP_BeginUseDate
                        row("PP_EndUseDate") = pci(i).PP_EndUseDate

                        row("PP_ID") = pci(i).PP_ID

                        ds.Tables("PieceProcess").Rows.Add(row)
                    End If
                Next
            End If


            'GridLookUpEdit1.Properties.PopupFormWidth = 800

        End If
    End Sub

    Private Sub ButtSing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtSing.Click
        If ButtSing.Text = "���" Then
            ButtSing.Text = "�h��"
            GLookUpEditPS_NO.Enabled = False
            Me.Width = ButtSing.Location.X + ButtSing.Width + Grid1.Width + 10
        Else
            GLookUpEditPS_NO.Enabled = True
            ButtSing.Text = "���"
            Me.Width = ButtSing.Location.X + ButtSing.Width + 10
        End If
    End Sub


    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If StrTypeLabel = "PPAdd" Then
            If CheckData() = False Then Exit Sub

            If CHECK_Time() = False Then Exit Sub


            If ButtSing.Text = "���" Then
                SaveNewOne("A")
            Else
                SaveNewTwo("A")
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If StrTypeLabel = "PPAdd" Then
            If CheckData() = False Then Exit Sub

            If CHECK_Time() = False Then Exit Sub

            If ButtSing.Text = "���" Then
                SaveNewOne("S")
            Else
                SaveNewTwo("S")
            End If
        End If

        If StrTypeLabel = "PPEdit" Then
            If CheckData() = False Then Exit Sub

            If CHECK_Time() = False Then Exit Sub
            SaveEditPT()

            SaveEdit()
        End If

    End Sub

    Private Sub txtPer_NO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPer_NO.KeyDown, gluPM_M_Code.KeyDown, gluPM_Type.KeyDown, GLookUpEditPS_NO.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub

    Private Sub PP_DateEdit_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PP_DateEdit.EditValueChanged
        'Dim ptc As New ProductionPiecePersonnelDayControl   ''���J ���u�s��---�m�W---����
        'txtPer_NO.Properties.DataSource = ptc.ProductionPiecePersonnelDay_GetList1("�L", Nothing, Nothing, strInDepID, Nothing, PP_DateEdit.EditValue, "False", PP_DateEdit.EditValue, "=")
        'txtPer_NO.Properties.DisplayMember = "Per_Name"
        'txtPer_NO.Properties.ValueMember = "Per_NO"
        If PP_DateEdit.EditValue Is Nothing Then
        Else
            Load_Table_New()
        End If

        If Load_OK = "OK" Then
            cboPer_Class.EditValue = Load_Per_Class()
        End If


    End Sub



    Private Sub txtPP_Qty_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPP_Qty.KeyUp
        Dim m As New System.Text.RegularExpressions.Regex("^\+?[1-9][0-9]*$")  '��ܾ��,���B�I�ƥ��h��F��

        If m.IsMatch(txtPP_Qty.Text) = True Then
        Else
            txtPP_Qty.Text = Nothing
            Exit Sub
        End If
    End Sub

    Private Sub SimpLoadPer_NO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpLoadPer_NO.Click
        frmProductionLoadPiecePersonnel.StartPosition = FormStartPosition.Manual
        frmProductionLoadPiecePersonnel.Left = Me.Left + txtPer_NO.Left + 3
        frmProductionLoadPiecePersonnel.Top = Me.Top + txtPer_NO.Top + txtPer_NO.Height + txtPer_NO.Height
        frmProductionLoadPiecePersonnel.Width = txtPer_NO.Width + SimpLoadPer_NO.Width + 3
        frmProductionLoadPiecePersonnel.TextEditPerNO.Focus()

        tempValue2 = strInFacIDFull  ''�q�{���J���������A�U�@�O�t�O�v�����Τ�n���ɡA�N���i��X�{ �N���O�����������u�[�J�������@�~�F
        tempValue3 = strInDepIDFull

        frmProductionLoadPiecePersonnel.ShowDialog()


        If tempValue Is Nothing Then
        Else
            Dim i, j As Integer
            Dim ptil As New List(Of ProductionPiecePersonnelInfo)  '���J���w���u
            Dim ptc As New ProductionPiecePersonnelControl   '

            For j = 0 To ds.Tables("Per_NONameT").Rows.Count - 1
                If ds.Tables("Per_NONameT").Rows(j)("Per_NO").ToString = tempValue Then
                    MsgBox("�ӭ��u�w�s�b�C���A�i�H�������!", 64, "����")
                    txtPer_NO.EditValue = tempValue
                    txtPer_NO.Focus()
                    Exit Sub
                End If
            Next


            ptil = ptc.ProductionPiecePersonnel_GetList(Nothing, tempValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "False", Nothing, Nothing)

            If ptil.Count > 0 Then
                For i = 0 To ptil.Count - 1
                    Dim row As DataRow
                    row = ds.Tables("Per_NONameT").NewRow

                    row("Per_NO") = ptil(i).Per_NO
                    row("Per_Name") = ptil(i).Per_Name

                    row("Per_NOName") = ptil(i).Per_NO & Space(2) & ptil(i).Per_Name

                    ''�o��t�a�O�W��
                    Dim fc As New FacControler
                    Dim fl As New List(Of FacInfo)
                    fl = fc.GetFacList(tempValue2, Nothing)
                    row("FacID") = tempValue2
                    row("Per_FacName") = fl(0).FacName

                    ''�o�쳡���W��
                    Dim dc As New DepartmentControler
                    Dim dl As New List(Of DepartmentInfo)
                    dl = dc.BriName_GetList(tempValue3, Nothing, tempValue2)
                    row("DepID") = tempValue3
                    row("Per_DepName") = dl(0).DepName

                    ds.Tables("Per_NONameT").Rows.Add(row)
                Next

                txtPer_NO.EditValue = ptil(0).Per_NO
                labFacID.Text = tempValue2
                labDepID.Text = tempValue3

            Else
                txtPer_NO.EditValue = Nothing
                labFacID.Text = Nothing
                labDepID.Text = Nothing
            End If

        End If


        tempValue3 = Nothing
        tempValue2 = Nothing
        tempValue = Nothing

        frmProductionLoadPiecePersonnel.Dispose()

    End Sub

    Private Sub txtPer_NO_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPer_NO.LostFocus
        On Error Resume Next

        If Load_OK = "OK" Then
            labDepID.Text = GridLookUpEdit1View.GetFocusedRowCellValue("DepID").ToString
            labFacID.Text = GridLookUpEdit1View.GetFocusedRowCellValue("FacID").ToString
        End If
    End Sub


    Private Sub txtPer_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPer_NO.EditValueChanged



        On Error Resume Next

        If Load_OK = "OK" Then
            labDepID.Text = GridLookUpEdit1View.GetFocusedRowCellValue("DepID").ToString
            labFacID.Text = GridLookUpEdit1View.GetFocusedRowCellValue("FacID").ToString
            cboPer_Class.EditValue = Load_Per_Class()
        End If
    End Sub


    Function Add_Per_Class() As Boolean  '�O�s�Z��H����
        Dim Pec As New ProductionPiecePersonnelMothClassControl
        Dim pei As New ProductionPiecePersonnelMothClassInfo

        pei.Per_NO = txtPer_NO.EditValue
        pei.Per_Class = cboPer_Class.EditValue
        pei.Per_Date = Format(CDate(PP_DateEdit.EditValue), "yyyy/MM")

        Pec.ProductionPiecePersonnelMothClass_Add(pei)

    End Function

    Function Update_Per_Class() As Boolean  '�O�s�Z��H����
        Dim Pec As New ProductionPiecePersonnelMothClassControl
        Dim pei As New ProductionPiecePersonnelMothClassInfo

        pei.Per_NO = txtPer_NO.EditValue
        pei.Per_Class = cboPer_Class.EditValue
        pei.Per_Date = Format(CDate(PP_DateEdit.EditValue), "yyyy/MM")

        Pec.ProductionPiecePersonnelMothClass_Update(pei)

    End Function

    Function Find_Per_Class() As String   '�d��
        Find_Per_Class = ""

        Dim Pec As New ProductionPiecePersonnelMothClassControl
        Dim pel As New List(Of ProductionPiecePersonnelMothClassInfo)

        pel = Pec.ProductionPiecePersonnelMothClass_GetList(txtPer_NO.EditValue, Format(CDate(PP_DateEdit.EditValue), "yyyy/MM"), Nothing)

        If pel.Count <= 0 Then
            Find_Per_Class = ""
        Else
            Find_Per_Class = pel(0).Per_Class
        End If
    End Function




    Function Load_Per_Class() As String   ''���J�Z��
        ''Load_Per_Class = ""

        ''If Me.txtPer_NO.EditValue Is Nothing Then
        ''    Exit Function
        ''End If

        ''Dim TempStr As String
        ''TempStr = Find_Per_Class()

        ''If TempStr = "" Then
        ''    Dim ptil As New List(Of ProductionPiecePersonnelInfo)  '���J���w���u
        ''    Dim ptc As New ProductionPiecePersonnelControl
        ''    ptil = ptc.ProductionPiecePersonnel_GetList(Nothing, Me.txtPer_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        ''    If ptil.Count > 0 Then
        ''        Load_Per_Class = ptil(0).Per_Class
        ''    End If
        ''Else
        ''    Load_Per_Class = TempStr
        ''End If

        ''�C��Z���,�q�򥻤��d�ߥX��2013-5-20
        Load_Per_Class = ""

        If Me.txtPer_NO.EditValue Is Nothing Then
            Exit Function
        End If

        Dim TempStr As String = ""

        Dim ptil As New List(Of ProductionPiecePersonnelInfo)  '���J���w���u
        Dim ptc As New ProductionPiecePersonnelControl
        ptil = ptc.ProductionPiecePersonnel_GetList(Nothing, Me.txtPer_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If ptil.Count > 0 Then
            TempStr = ptil(0).Per_Class
        End If

        If TempStr = "" Then
            TempStr = Find_Per_Class()
        End If

        Load_Per_Class = TempStr
     
    End Function

    ''------------------------------------------------------------------------------------------------------------------------------
    ''���u�p�ɫO�s
    ''------------------------------------------------------------------------------------------------------------------------------

    Function SaveNewPT(ByVal StrPP_NOAA As String) As Boolean

        SaveNewPT = True

        If Val(txtTotal.Text) > 0 Then
        Else
            Exit Function
        End If

        Dim gc As New ProductionSumTimePersonnelControl
        Dim gi As New ProductionSumTimePersonnelInfo

        Dim LsLabPT_NO As String

        LsLabPT_NO = GetProductionSumTimePersonnelNO()

        If LsLabPT_NO <> "" Then
        Else
            MsgBox("�y����������ѡA�Э���!")
            Exit Function
        End If

        gi.PT_NO = LsLabPT_NO  ''�n�����o�s��
        gi.PT_Date = PP_DateEdit.EditValue  ''�έp�ɶ�

        gi.Per_NO = txtPer_NO.EditValue.Trim '���u�u��
        gi.PT_BeginTime = StartTimeEdit.Text
        gi.PT_EndTime = EndTimeEdit.Text

        gi.PT_Total = Val(txtTotal.Text) '�X�p
        gi.PT_Remark = MemoPT_Remark.Text '�ƪ`
        gi.PT_Action = InUserID


        gi.DepID = labDepID.Text    ''�����s��
        gi.FacID = labFacID.Text    '�t�O 

        gi.PP_NO = StrPP_NOAA

        ''���o�˿�,����---------------------------------------------------------------------------
        Dim PsaC As New LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeControl
        Dim PsaL As New List(Of LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeInfo)
        PsaL = PsaC.ProductionPiecePaySampType_GetList(GridLookSampType.EditValue, Nothing, True, Nothing)
        gi.SampID = GridLookSampType.EditValue
        gi.SampPrice = PsaL(0).SampPrice
        '-----------------------------------------------------------------------------------------

        If gc.ProductionSumTimePersonnel_Add(gi) = True Then
        Else
            SaveNewPT = False
        End If
    End Function

    Private Sub SaveEditPT()

        If Val(txtTotal.Text) > 0 Or LabPT_NO.Text <> "" Then
        Else
            Exit Sub
        End If

        Dim gc As New ProductionSumTimePersonnelControl
        Dim gi As New ProductionSumTimePersonnelInfo

        gi.PT_NO = LabPT_NO.Text
        gi.PT_Date = PP_DateEdit.Text  ''�έp�ɶ�

        gi.Per_NO = txtPer_NO.EditValue.Trim '���u�u��
        gi.PT_BeginTime = StartTimeEdit.Text
        gi.PT_EndTime = EndTimeEdit.Text

        gi.PT_Total = Val(txtTotal.Text) '�X�p
        gi.PT_Remark = MemoPT_Remark.Text '�ƪ`
        gi.PT_Action = InUserID


        gi.DepID = labDepID.Text    ''�����s��
        gi.FacID = labFacID.Text    '�t�O 


        ''���o�˿�,����---------------------------------------------------------------------------
        Dim PsaC As New LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeControl
        Dim PsaL As New List(Of LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeInfo)
        PsaL = PsaC.ProductionPiecePaySampType_GetList(GridLookSampType.EditValue, Nothing, True, Nothing)
        gi.SampID = GridLookSampType.EditValue
        gi.SampPrice = PsaL(0).SampPrice
        '-----------------------------------------------------------------------------------------

        If LabPT_NO.Text = "" Then
            gi.PP_NO = StrPP_NO
            Dim LsLabPT_NO As String

            LsLabPT_NO = GetProductionSumTimePersonnelNO()

            If LsLabPT_NO <> "" Then
            Else
                MsgBox("�y����������ѡA�Э���!")
                Exit Sub
            End If

            gi.PT_NO = LsLabPT_NO

            If gc.ProductionSumTimePersonnel_Add(gi) = True Then
            End If
        Else
            If gc.ProductionSumTimePersonnel_Update(gi) = True Then
            End If
        End If

    End Sub

    ''' <summary>
    ''' �ھڽs�����J�ƾ�
    ''' </summary>
    ''' <param name="Str_PP_NO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadDataPT(ByVal Str_PP_NO As String) As Boolean
        LoadDataPT = False

        Dim objInfo As New ProductionSumTimePersonnelInfo
        Dim objList As New List(Of ProductionSumTimePersonnelInfo)
        Dim oc As New ProductionSumTimePersonnelControl

        objList = oc.ProductionSumTimePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Str_PP_NO)

        If objList.Count <= 0 Then
            LoadDataPT = False
            Exit Function
        Else

            StartTimeEdit.Text = objList(0).PT_BeginTime
            EndTimeEdit.Text = objList(0).PT_EndTime  '

            txtTotal.Text = objList(0).PT_Total.ToString

            MemoPT_Remark.EditValue = objList(0).PT_Remark   '�ƪ`

            LabPT_NO.Text = objList(0).PT_NO

            GridLookSampType.EditValue = objList(0).SampID

            LoadDataPT = True
        End If

    End Function

    ''' <summary>
    ''' ����y�������
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetProductionSumTimePersonnelNO() As String
        GetProductionSumTimePersonnelNO = ""

        Dim str1, str2 As String
        Dim gc1 As New ProductionSumTimePersonnelControl
        Dim gi1 As New ProductionSumTimePersonnelInfo

        str1 = Mid(Year(Now), 3)
        If CInt(Month(Now)) < 10 Then
            str2 = "0" & Month(Now)
        Else
            str2 = Month(Now)
        End If

        Dim Stra As String
        Stra = Trim(str1 & str2)

        gi1 = gc1.ProductionSumTimePersonnel_GetNO(Stra) '' Ū�����

        If gi1 Is Nothing Then
            GetProductionSumTimePersonnelNO = "PT" & str1 & str2 & "0000001"
        Else
            GetProductionSumTimePersonnelNO = "PT" & str1 & str2 & Mid((CInt(Mid(gi1.PT_NO, 7)) + 10000001), 2)
        End If


    End Function

    '�D��Ӯɶ����ƭȡM�ഫ������������(�^

    Function QJSumDateValue(ByVal Date1 As Date, ByVal Date2 As Date) As Single

        ''-------------------------------------------
        ''�s��k
        Dim strDate1 As DateTime
        Dim strDate2 As DateTime
        Dim CheckHours As TimeSpan

        strDate1 = DateTime.Parse(StartTimeEdit.Text)
        strDate2 = DateTime.Parse(EndTimeEdit.Text)

        CheckHours = strDate2 - strDate1
        QJSumDateValue = Format(CheckHours.TotalHours, "0.00")

        If QJSumDateValue < 0 Then
            QJSumDateValue = QJSumDateValue + 24
        End If


    End Function

    ''' <summary>
    ''' �d�ߥX�]�mOK���ɪ�
    ''' </summary>
    ''' <param name="Stime"></param>
    ''' <param name="Etime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Get_Tatal_Time(ByVal Stime As String, ByVal Etime As String) As Double


        Dim pc As New ProductionSumTimeSettingControl
        Dim pil As New List(Of ProductionSumTimeSettingInfo)

        pil = pc.ProductionSumTimeSetting_GetList(Nothing, Stime, Etime)

        If pil.Count > 0 Then
            Get_Tatal_Time = pil(0).TotalTime
        End If


    End Function

    Function CHECK_Time() As Boolean

        CHECK_Time = True

        If Val(txtTotal.Text) > 0 Or LabPT_NO.Text <> "" Then
        Else
            Exit Function
        End If

        ''  MsgBox(Val(txtTotal.Text) & "--" & LabPT_NO.Text)

        If GridLookSampType.Text = "" Then
            MsgBox("�п������")
            GridLookSampType.Select()
            CHECK_Time = False
            Exit Function
        End If


        ''-------------------------------------------------------------------
        If (StartTimeEdit.Text <> "00:00" Or EndTimeEdit.Text <> "00:00") And Val(Me.txtTotal.Text) = 0 Then
            CHECK_Time = False
            EndTimeEdit.Focus()
            MsgBox("�ɶ��q��J���~!")
            Exit Function
        End If
        ''-------------------------------------------------------------------

        Dim pil As New List(Of ProductionSumTimePersonnelInfo)
        Dim pc As New ProductionSumTimePersonnelControl
        Dim i As Integer

        Dim strDateEnd, strDateStart As DateTime
        Dim strDateEdit_S, strDateEdit_E As DateTime

        Dim CheckHourS1, CheckHourS2 As TimeSpan
        Dim CheckHourE1, CheckHourE2 As TimeSpan


        ''�ק�   �M �W�[ �ާ@�ɡA�ק�b�d�߼ƾڮw�ɤ��]�t���O��,  �W�[�ɬd�ߩҦ� �ŦX����

        If StrTypeLabel = "PPEdit" Then '
            pil = pc.ProductionSumTimePersonnel_GetList(LabPT_NO.Text, txtPer_NO.EditValue, Nothing, Nothing, Nothing, PP_DateEdit.EditValue, Nothing, PP_DateEdit.EditValue, "in", Nothing, Nothing)
        Else
            pil = pc.ProductionSumTimePersonnel_GetList(Nothing, txtPer_NO.EditValue, Nothing, Nothing, Nothing, PP_DateEdit.EditValue, Nothing, PP_DateEdit.EditValue, Nothing, Nothing, Nothing)
        End If


        If pil.Count > 0 Then
        Else
            Exit Function
        End If

        For i = 0 To pil.Count - 1
            ''�֬d�ɶ��q----------------
            If pil(i).PT_BeginTime = Nothing Or pil(i).PT_EndTime = Nothing Then
                CHECK_Time = False
                Exit Function
            End If

            strDateEnd = DateTime.Parse(pil(i).PT_EndTime)  ''�O�󵲧��ɶ�              Ū���ƾڮw�����ɶ��q
            strDateStart = DateTime.Parse(pil(i).PT_BeginTime) ''�O��}�l�ɶ�

            strDateEdit_S = DateTime.Parse(StartTimeEdit.Text)  ''�n�[�J���ɶ�         �n�D�O�s���ɶ��q
            strDateEdit_E = DateTime.Parse(EndTimeEdit.Text)

            ''-----------------------------------------------------------
            CheckHourS1 = strDateEdit_S - strDateStart   ''>0
            CheckHourS2 = strDateEdit_S - strDateEnd     ''<0

            If CheckHourS1.TotalHours >= 0 And CheckHourS2.TotalHours <= 0 Then
                CHECK_Time = False
                StartTimeEdit.Focus()
                MsgBox("�b�E�w�ɶ��q���A���H���w�ѻP�@�I")
                Exit Function
            End If
            ''-+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            CheckHourE1 = strDateEdit_E - strDateStart   ''>0
            CheckHourE2 = strDateEdit_E - strDateEnd     ''<0

            If CheckHourE1.TotalHours >= 0 And CheckHourE2.TotalHours <= 0 Then
                CHECK_Time = False
                EndTimeEdit.Focus()
                MsgBox("�b�E�w�ɶ��q���A���H���w�ѻP�@�I")
                Exit Function
            End If
        Next
    End Function

    Private Sub EndTimeEdit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles EndTimeEdit.LostFocus
        On Error Resume Next
        Dim date1 As Date

        date1 = CDate(EndTimeEdit.Text)

        If Err.Number > 0 Then
            EndTimeEdit.Focus()
            EndTimeEdit.SelectAll()
            Exit Sub
            ' MsgBox("�A��J���ɶ��榡���~,�Э��s��J!")
        End If

        If Load_OK = "OK" And StrTypeLabel <> "PPEdit" Then
            '  txtTotal.Text = QJSumDateValue(CDate(StartTimeEdit.Text), CDate(EndTimeEdit.Text)) ''�p���`�p�ɶ�
            Dim TempDoub As Double
            TempDoub = Get_Tatal_Time(StartTimeEdit.Text, EndTimeEdit.Text)

            If TempDoub = 0 Then
                txtTotal.Text = QJSumDateValue(CDate(StartTimeEdit.Text), CDate(EndTimeEdit.Text)) ''�p���`�p�ɶ�
            Else
                txtTotal.Text = TempDoub
            End If
        End If

    End Sub

    Private Sub txtTotal_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotal.GotFocus
        On Error Resume Next
        If Load_OK = "OK" And StrTypeLabel <> "PPEdit" Then
            'txtTotal.Text = QJSumDateValue(CDate(StartTimeEdit.Text), CDate(EndTimeEdit.Text)) ''�p���`�p�ɶ�
            Dim TempDoub As Double
            TempDoub = Get_Tatal_Time(StartTimeEdit.Text, EndTimeEdit.Text)

            If TempDoub = 0 Then
                txtTotal.Text = QJSumDateValue(CDate(StartTimeEdit.Text), CDate(EndTimeEdit.Text)) ''�p���`�p�ɶ�
            Else
                txtTotal.Text = TempDoub
            End If
        End If
    End Sub



    Private Sub EndTimeEdit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles EndTimeEdit.GotFocus
        EndTimeEdit.SelectAll()
    End Sub

    Private Sub StartTimeEdit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles StartTimeEdit.GotFocus
        StartTimeEdit.SelectAll()
    End Sub

    Private Sub StartTimeEdit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles StartTimeEdit.LostFocus
        On Error Resume Next
        Dim date1 As Date

        date1 = CDate(StartTimeEdit.Text)

        If Err.Number > 0 Then
            StartTimeEdit.Focus()
            StartTimeEdit.SelectAll()
            ' MsgBox("�A��J���ɶ��榡���~,�Э��s��J!")
        End If
    End Sub

    Private Sub AddPSButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddPSButton.Click
        If gluPM_Type.EditValue = "" Or gluPM_M_Code.EditValue = "" Then
            MsgBox("�п�ܤu���~�s���������I")
            Exit Sub
        End If

        Dim frmPPPModify As New frmProductionPieceProcess
        frmPPPModify.lblTittle.Text = "�p��u����--���J"
        '  frmPPPModify.MdiParent = MDIMain
        ' frmPPPModify.WindowState = FormWindowState.Maximized
        tempValue = cboPro_Type.Text
        tempValue2 = gluPM_M_Code.EditValue
        tempValue3 = gluPM_Type.EditValue

        ' Me.Text = gluPM_M_Code.EditValue & "----" & gluPM_Type.EditValue

        frmPPPModify.ShowInTaskbar = False
        frmPPPModify.MaximizeBox = False
        frmPPPModify.MinimizeBox = False
        frmPPPModify.StartPosition = FormStartPosition.CenterScreen
        'frmPPPModify.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        frmPPPModify.ShowDialog()
        frmPPPModify.Dispose()

        gluPM_Type_EditValueChanged(Nothing, Nothing)

    End Sub

    Private Sub GridLookSampType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridLookSampType.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub
End Class