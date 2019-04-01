Imports LFERP.Library.ProductionPiecePersonnel
Imports LFERP.Library.ProductionSumTimePersonnel
Imports LFERP.Library.ProductionPiecePersonnelDay
Imports LFERP.DataSetting
Imports System.Math

Imports LFERP.Library.ProductionSumTimeSetting


Public Class ProductionSumTimePersonnel

    Dim ds As New DataSet
    ' Dim StrLabDepID As String
    Dim Load_OK As String ''�T�wLoad�ƥ�O�_�w���J����

    Private Sub ProductionSumTimePersonnel_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ' Me.Dispose()
        Me.Close()
    End Sub

    Private Sub ProductionSumTimePersonnel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Load_OK = ""

        Clr_Text() ''�M���ƾ�

        LabPT_NO.Text = tempValue2
        CaoTypeLabel.Text = MTypeName
        'StrLabDepID = tempValue4

        tempValue2 = Nothing
        MTypeName = Nothing
        tempValue4 = Nothing
        tempValue3 = Nothing

        txtPer_NO.Enabled = True
        cmdAdd.Visible = True
        cmdSave.Visible = True

        CreatTable()

        PT_DateEdit.EditValue = Format(Now, "yyyy/MM/dd")

        'Dim ptc As New ProductionPiecePersonnelDayControl   ''���J ���u�s��---�m�W---���� �[�J�L�էO���u
        'txtPer_NO.Properties.DataSource = ptc.ProductionPiecePersonnelDay_GetList1("�L", Nothing, Nothing, strInDepID, Nothing, PT_DateEdit.EditValue, "False", PT_DateEdit.EditValue, "=")
        'txtPer_NO.Properties.DisplayMember = "Per_Name"
        'txtPer_NO.Properties.ValueMember = "Per_NO"

        '----------------------------------------------------------------------
        GridLookSampType.Properties.ValueMember = "SampID"
        GridLookSampType.Properties.DisplayMember = "SampName"
        Dim Psam As New LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeControl

        If strInUserRank = "�έp" Then
            GridLookSampType.Properties.DataSource = Psam.ProductionPiecePaySampType_GetList(Nothing, Nothing, True, strInDepIDFull)
        Else
            GridLookSampType.Properties.DataSource = Psam.ProductionPiecePaySampType_GetList(Nothing, Nothing, True, Nothing)
        End If

        GridLookSampType.EditValue = "Z"

        '----------------------------------------------------------------------


        Select Case CaoTypeLabel.Text
            Case "PTAdd"
                Me.Text = "���u�p��--�W�["
                Me.LabCaption.Text = "���u�p��--�W�["
            Case "PTEdit"
                If LoadData(LabPT_NO.Text) = False Then Exit Sub
                Me.Text = "���u�p��--�ק�" & "[" & LabPT_NO.Text & "]"
                Me.LabCaption.Text = "���u�p��--�ק�"
                cmdAdd.Visible = False

            Case "PTView"

                If LoadData(LabPT_NO.Text) = False Then Exit Sub
                '�d��
                cmdSave.Visible = False
                cmdAdd.Visible = False
                PT_DateEdit.Enabled = False
                txtPer_NO.Enabled = False

                'StartTimeEdit.Enabled = False
                'EndTimeEdit.Enabled = False
                'txtTotal.Enabled = False
                'MemoPT_Remark.Enabled = False

                Me.Text = "���u�p��--�d��" & "[" & LabPT_NO.Text & "]"
                Me.LabCaption.Text = "���u�p��--�d��"
        End Select

        txtPer_NO.Select()
        txtPer_NO.Focus()

        Load_OK = "OK"

    End Sub

    Sub CreatTable()
        ''2012-5-11
        With ds.Tables.Add("Per_NONameT")
            .Columns.Add("Per_NO", GetType(String))
            .Columns.Add("Per_Name", GetType(String))
            .Columns.Add("DepID", GetType(String))
            .Columns.Add("FacID", GetType(String))
            .Columns.Add("Per_DepName", GetType(String))
            .Columns.Add("Per_FacName", GetType(String))
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
        ptil = ptc.ProductionPiecePersonnelTimeDay_GetList(PT_DateEdit.EditValue, strInDepID)

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
        'Dim count2 As Integer

        'ds.Tables("Per_NONameT").Clear()

        'Dim ptil As New List(Of ProductionPiecePersonnelInfo)
        'Dim ptc As New ProductionPiecePersonnelControl   '

        'ptil = ptc.ProductionPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing, "False", Nothing)
        'count1 = ptil.Count
        'If count1 > 0 Then
        '    For i = 0 To count1 - 1

        '        Dim ppil As New List(Of ProductionPiecePersonnelDayInfo)
        '        Dim ppc As New ProductionPiecePersonnelDayControl   '

        '        ppil = ppc.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, ptil(i).Per_NO, Nothing, Nothing, strInDepID, Nothing, Nothing, PT_DateEdit.EditValue, Nothing, "False", PT_DateEdit.EditValue)
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
    '''  ��������   ''2012-5-11
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

        ppil = ppc.ProductionPiecePersonnelDay_GetList1(Nothing, Nothing, Nothing, strInDepID, Nothing, PT_DateEdit.EditValue, "False", PT_DateEdit.EditValue, Nothing)
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
    ''' <summary>
    ''' �ھڽs�����J�ƾ�
    ''' </summary>
    ''' <param name="Str_PT_NO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadData(ByVal Str_PT_NO As String) As Boolean
        LoadData = False

        Dim objInfo As New ProductionSumTimePersonnelInfo
        Dim objList As New List(Of ProductionSumTimePersonnelInfo)
        Dim oc As New ProductionSumTimePersonnelControl

        objList = oc.ProductionSumTimePersonnel_GetList(Str_PT_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If objList.Count <= 0 Then
            MsgBox("�S���ƾڡI")
            LoadData = False
            Exit Function
        Else
            PT_DateEdit.EditValue = objList(0).PT_Date ''�ɶ�
            StartTimeEdit.Text = objList(0).PT_BeginTime
            EndTimeEdit.Text = objList(0).PT_EndTime  '

            txtTotal.Text = objList(0).PT_Total.ToString

            MemoPT_Remark.EditValue = objList(0).PT_Remark   '�ƪ`

            labDepID.Text = objList(0).DepID  ' ����        ��Ū���A�H���ھ� ���u�s���s�J
            labFacID.Text = objList(0).FacID  ' �t�O  

            GridLookSampType.EditValue = objList(0).SampID


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
                row("Per_Name") = objList(0).PT_Per_Name

                row("Per_NOName") = objList(0).Per_NO & "  " & objList(0).PT_Per_Name

                row("DepID") = objList(0).DepID
                row("FacID") = objList(0).FacID

                row("Per_DepName") = objList(0).PT_DepName
                row("Per_FacName") = objList(0).PT_FacName

                ds.Tables("Per_NONameT").Rows.Add(row)
            End If

            txtPer_NO.EditValue = objList(0).Per_NO

            LoadData = True
        End If

    End Function

    ''' <summary>
    ''' �ˬd�ƾڿ�J���T
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

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
        

        If Val(txtTotal.Text) > 0 Then
        Else
            MsgBox("�}�l�ε����ɶ���J���~�A���ˬd")
            StartTimeEdit.Focus()
            StartTimeEdit.SelectAll()
            CheckData = False
            Exit Function
        End If


        If GridLookSampType.Text = "" Then
            MsgBox("�п������")
            GridLookSampType.Select()
            CheckData = False
            Exit Function
        End If


        On Error Resume Next

        Dim date1 As Date
        Dim date2 As Date

        date1 = CDate(StartTimeEdit.Text)


        If Err.Number > 0 Then
            StartTimeEdit.Focus()
            StartTimeEdit.SelectAll()
            MsgBox("�A��J���ɶ��榡���~,���ˬd!")
            CheckData = False
            Exit Function
        End If

        date2 = CDate(EndTimeEdit.Text)

        If Err.Number > 0 Then
            EndTimeEdit.Focus()
            EndTimeEdit.SelectAll()
            MsgBox("�A��J���ɶ��榡���~,���ˬd!")
            CheckData = False
            Exit Function
        End If


        'If DateDiff("n", CDate(StartTimeEdit.Text), CDate(EndTimeEdit.Text)) > 0 Then
        'Else
        '    MsgBox("�}�l�ε����ɶ���J���~�A���ˬd")
        '    CheckData = False
        '    If Val(txtTotal.Text) < 0 Then
        '        StartTimeEdit.Focus()
        '        StartTimeEdit.SelectAll()
        '    Else
        '        EndTimeEdit.Focus()
        '        EndTimeEdit.SelectAll()
        '    End If
        '    Exit Function
        'End If



        If CSng(txtTotal.Text) = QJSumDateValue(CDate(StartTimeEdit.Text), CDate(EndTimeEdit.Text)) Then ''�p���`�p�ɶ� �P�_�̦Z  ��J���X�p�ɶ��P�p�⪺�O�_�@�� 
        Else
            If MsgBox("�X�p�ɶ��p�⦳�~�A�A�T�w�n�~��O�s�ܡH", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Else
                CheckData = False
                Exit Function
            End If
        End If

        Dim pcA As New LFERP.Library.ProductionSumLock.ProductionSumLockControl
        Dim plA As New List(Of LFERP.Library.ProductionSumLock.ProductionSumLockInfo)
        plA = pcA.ProductionSumLock_GetList(Nothing, Nothing, labDepID.Text, Format(CDate(PT_DateEdit.EditValue), "yyyy/MM"))

        If plA.Count > 0 Then
            If plA(0).LockCheck = True Then
                MsgBox("��e�����O���w��w������J!")
                CheckData = False
                Exit Function
            End If
        End If

    End Function


    ''' <summary>
    ''' �d�� �Y�@���u�A��ѡA�b�S�w�ɶ��q�A����ѻP�ⶵ�حp�ɤu�@ 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CHECK_Time() As Boolean
        Dim pil As New List(Of ProductionSumTimePersonnelInfo)
        Dim pc As New ProductionSumTimePersonnelControl
        Dim i As Integer

        Dim strDateEnd, strDateStart As DateTime
        Dim strDateEdit_S, strDateEdit_E As DateTime

        Dim CheckHourS1, CheckHourS2 As TimeSpan
        Dim CheckHourE1, CheckHourE2 As TimeSpan

        CHECK_Time = True


        ''�ק�   �M �W�[ �ާ@�ɡA�ק�b�d�߼ƾڮw�ɤ��]�t���O��,  �W�[�ɬd�ߩҦ� �ŦX����

        If CaoTypeLabel.Text = "PTEdit" Then
            pil = pc.ProductionSumTimePersonnel_GetList(LabPT_NO.Text, txtPer_NO.EditValue, Nothing, Nothing, Nothing, PT_DateEdit.EditValue, Nothing, PT_DateEdit.EditValue, "in", Nothing, Nothing)
        Else
            pil = pc.ProductionSumTimePersonnel_GetList(Nothing, txtPer_NO.EditValue, Nothing, Nothing, Nothing, PT_DateEdit.EditValue, Nothing, PT_DateEdit.EditValue, Nothing, Nothing, Nothing)
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

    ''' <summary>
    ''' �ƾڼW�[
    ''' </summary>
    ''' <param name="S_model"></param>
    ''' <remarks></remarks>
    Private Sub SaveNew(ByVal S_model As String)
        Dim gc As New ProductionSumTimePersonnelControl
        Dim gi As New ProductionSumTimePersonnelInfo

        LabPT_NO.Text = GetProductionSumTimePersonnelNO()

        If LabPT_NO.Text <> "" Then
        Else
            MsgBox("�y����������ѡA�Э���!")
        End If

        gi.PT_NO = LabPT_NO.Text  ''�n�����o�s��
        gi.PT_Date = PT_DateEdit.EditValue  ''�έp�ɶ�
        
        gi.Per_NO = txtPer_NO.EditValue.Trim '���u�u��
        gi.PT_BeginTime = StartTimeEdit.Text
        gi.PT_EndTime = EndTimeEdit.Text

        gi.PT_Total = Val(txtTotal.Text) '�X�p
        gi.PT_Remark = MemoPT_Remark.Text '�ƪ`
        gi.PT_Action = InUserID

        ''���o�˿�,����---------------------------------------------------------------------------
        Dim PsaC As New LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeControl
        Dim PsaL As New List(Of LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeInfo)
        PsaL = PsaC.ProductionPiecePaySampType_GetList(GridLookSampType.EditValue, Nothing, True, Nothing)
        gi.SampID = GridLookSampType.EditValue
        gi.SampPrice = PsaL(0).SampPrice
        '-----------------------------------------------------------------------------------------

        ' ''�����A�t�O �ھڭ��u�s�� �q�p��H���򥻫H�����d�ߥX�s�A
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


        If S_model = "S" Then
            If gc.ProductionSumTimePersonnel_Add(gi) = True Then
                MsgBox("�ƾګO�s���\")
                'Me.Dispose()
                Me.Close()
            End If
        Else
            If gc.ProductionSumTimePersonnel_Add(gi) = False Then
                MsgBox("�ƾګO�s����")
                'Me.Dispose()
                Me.Close()
            Else
                Clr_Text()
            End If
        End If



    End Sub
    ''' <summary>
    ''' �M�żƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub Clr_Text()
        txtPer_NO.EditValue = Nothing
        StartTimeEdit.Text = Nothing
        EndTimeEdit.Text = Nothing
        txtTotal.Text = ""
        MemoPT_Remark.Text = ""
        LabPT_NO.Text = ""

        StartTimeEdit.Text = "00:00"
        EndTimeEdit.Text = "00:00"

        labDepID.Text = ""
        labFacID.Text = ""

        txtPer_NO.Focus()
    End Sub

    ''' <summary>
    ''' �ק�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveEdit()
        Dim gc As New ProductionSumTimePersonnelControl
        Dim gi As New ProductionSumTimePersonnelInfo

        gi.PT_NO = LabPT_NO.Text
        gi.PT_Date = PT_DateEdit.Text  ''�έp�ɶ�

        gi.Per_NO = txtPer_NO.EditValue.Trim '���u�u��
        gi.PT_BeginTime = StartTimeEdit.Text
        gi.PT_EndTime = EndTimeEdit.Text

        gi.PT_Total = Val(txtTotal.Text) '�X�p
        gi.PT_Remark = MemoPT_Remark.Text '�ƪ`
        gi.PT_Action = InUserID

        ''���o�˿�,����---------------------------------------------------------------------------
        Dim PsaC As New LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeControl
        Dim PsaL As New List(Of LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeInfo)
        PsaL = PsaC.ProductionPiecePaySampType_GetList(GridLookSampType.EditValue, Nothing, True, Nothing)
        gi.SampID = GridLookSampType.EditValue
        gi.SampPrice = PsaL(0).SampPrice
        '-----------------------------------------------------------------------------------------


        ' ''�����A�t�O �ھڭ��u�s�� �q�p��H���C����d�ߥX�s�A
        'Dim ptc As New ProductionPiecePersonnelDayControl
        'Dim pti As New List(Of ProductionPiecePersonnelDayInfo)
        'pti = ptc.ProductionPiecePersonnelDay_GetList(Nothing, txtPer_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "False", Nothing, Nothing)
        'If pti.Count <= 0 Then
        '    Exit Sub
        'End If
        'gi.DepID = pti(0).DepID   ''�����s��
        'gi.FacID = pti(0).FacID   '�t�O 

        gi.DepID = labDepID.Text    ''�����s��
        gi.FacID = labFacID.Text    '�t�O 


        If gc.ProductionSumTimePersonnel_Update(gi) = True Then
            MsgBox("�ƾګO�s���\")
            'Me.Dispose()
            Me.Close()
        End If
    End Sub
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
        'Dim i, X, Y, z As Long
        'Dim l As Single

        'i = Math.Abs(DateDiff("n", Date1, Date2))
        ''i = DateDiff("n", Date1, Date2)
        'l = Math.Round(i / 60, 1)
        'Y = 0

        'For X = 1 To Len(l)
        '    If Mid(l, X, 1) = "." Then
        '        Y = X
        '        Exit For
        '    End If
        'Next
        'If Y = 0 Then
        '    QJSumDateValue = l
        'Else
        '    z = i - (CLng(Mid(l, 1, Y - 1) * 60))
        '    Select Case z
        '        Case 1 To 14
        '            QJSumDateValue = CLng(Mid(l, 1, Y - 1))
        '        Case 15 To 44
        '            QJSumDateValue = CLng(Mid(l, 1, Y - 1)) + 0.5
        '        Case 45 To 59
        '            QJSumDateValue = CLng(Mid(l, 1, Y - 1)) + 1
        '        Case Else
        '            QJSumDateValue = Y
        '    End Select
        'End If


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


    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If CheckData() = False Then Exit Sub
        If CHECK_Time() = False Then Exit Sub '�P�_�ɶ��q

        If CaoTypeLabel.Text = "PTAdd" Then
            Call SaveNew("A")
            txtPer_NO.Select()
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckData() = False Then Exit Sub '
        If CHECK_Time() = False Then Exit Sub '�P�_�ɶ��q

        Select Case CaoTypeLabel.Text
            Case "PTAdd"
                Call SaveNew("S")
            Case "PTEdit"
                Call SaveEdit()
        End Select
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        ' Me.Dispose()
        Me.Close()
    End Sub


    Private Sub txtTotal_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotal.GotFocus
        On Error Resume Next
        If Load_OK = "OK" Then
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

        If Load_OK = "OK" Then
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


    Private Sub txtPer_NO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPer_NO.KeyDown
        If e.KeyCode = Keys.Space Then
            txtPer_NO.ShowPopup()
        End If
    End Sub

    Private Sub PT_DateEdit_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PT_DateEdit.EditValueChanged
        'Dim ptc As New ProductionPiecePersonnelDayControl   ''���J ���u�s��---�m�W---����
        'txtPer_NO.Properties.DataSource = ptc.ProductionPiecePersonnelDay_GetList1("�L", Nothing, Nothing, strInDepID, Nothing, PT_DateEdit.EditValue, "False", PT_DateEdit.EditValue, "=")
        'txtPer_NO.Properties.DisplayMember = "Per_Name"
        'txtPer_NO.Properties.ValueMember = "Per_NO"

        If PT_DateEdit.EditValue Is Nothing Then
        Else
            Load_Table_New()
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

            Dim i As Integer
            Dim ptil As New List(Of ProductionPiecePersonnelInfo)  '���J���w���u
            Dim ptc As New ProductionPiecePersonnelControl   '

            ptil = ptc.ProductionPiecePersonnel_GetList(Nothing, tempValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "False", Nothing, Nothing)

            If ptil.Count > 0 Then
                For i = 0 To ptil.Count - 1
                    Dim row As DataRow
                    row = ds.Tables("Per_NONameT").NewRow

                    row("Per_NO") = ptil(i).Per_NO
                    row("Per_Name") = ptil(i).Per_Name

                    row("Per_NOName") = ptil(i).Per_NO & "  " & ptil(i).Per_Name

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

    Private Sub txtPer_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPer_NO.EditValueChanged
        On Error Resume Next
        If Load_OK = "OK" Then
            labDepID.Text = GridLookUpEdit1View.GetFocusedRowCellValue("DepID").ToString
            labFacID.Text = GridLookUpEdit1View.GetFocusedRowCellValue("FacID").ToString
        End If
    End Sub

    Private Sub SimpLoadPer_NO_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles SimpLoadPer_NO.LostFocus
        On Error Resume Next
        If Load_OK = "OK" Then
            labDepID.Text = GridLookUpEdit1View.GetFocusedRowCellValue("DepID").ToString
            labFacID.Text = GridLookUpEdit1View.GetFocusedRowCellValue("FacID").ToString
        End If
    End Sub

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

    Private Sub GridLookSampType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridLookSampType.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub
End Class