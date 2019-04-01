Imports LFERP.Library.ProductionPieceWorkGroup
Imports LFERP.Library.ProductionPiecePersonnelDay
Imports LFERP.Library.ProductionPiecePersonnel
Imports LFERP.Library.ProductionSumTimeWorkGroup
Imports System.Math
Imports LFERP.Library.ProductionSumTimeSetting

Public Class ProductionSumTimeWorkGroup
    'Dim StrDepID As String
    Dim Load_OK As String ''�T�wLoad�ƥ�O�_�w���J����

    Private Sub ProductionSumTimeWorkGroup_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ' Me.Dispose()
        Me.Close()
    End Sub

    Private Sub ProductionSumTimeWorkGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Clr_Text()
        Load_OK = ""

        LabGT_NO.Text = tempValue2
        CaoTypeLabel.Text = MTypeName
        'StrDepID = tempValue4

        tempValue2 = Nothing
        MTypeName = Nothing
        tempValue4 = Nothing

        cmdSave.Visible = True
        cmdAdd.Visible = True
        GT_DateEdit.Enabled = True
        txtPer_NO.Enabled = True

        GT_DateEdit.EditValue = Format(Now, "yyyy/MM/dd")

        'Dim ptc As New ProductionPiecePersonnelDayControl   ''���J ���u�s��---�m�W---���� ���էO <> �L
        'txtPer_NO.Properties.DataSource = ptc.ProductionPiecePersonnelDay_GetList1("�L", Nothing, Nothing, strInDepID, Nothing, GT_DateEdit.EditValue, "False", GT_DateEdit.EditValue, "<>")
        'txtPer_NO.Properties.DisplayMember = "Per_Name"
        'txtPer_NO.Properties.ValueMember = "Per_NO"

        ''2012-6-5 �אּ ����էO�s���A�A��ܭ��u�s��
        Dim pc As New ProductionPieceWorkGroupControl
        GluG_NO.Properties.DisplayMember = "G_NOName"
        GluG_NO.Properties.ValueMember = "G_NO"
        GluG_NO.Properties.DataSource = pc.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing)



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
            Case "GTAdd"
                Me.Text = "�էO�p��--�W�["
                LabCaption.Text = "�էO�p��--�W�["
            Case "GTEdit"
                If LoadData(LabGT_NO.Text) = False Then Exit Sub
                Me.Text = "�էO�p��--�ק�" & "[" & LabGT_NO.Text & "]"
                LabCaption.Text = "�էO�p��--�ק�"
                cmdAdd.Visible = False

            Case "GTView"

                If LoadData(LabGT_NO.Text) = False Then Exit Sub
                '�d��
                cmdSave.Visible = False
                cmdAdd.Visible = False
                GT_DateEdit.Enabled = False
                txtPer_NO.Enabled = False


                LabCaption.Text = "�էO�p��--�d��"
                Me.Text = "�էO�p��--�d��" & "[" & LabGT_NO.Text & "]"
        End Select

        GT_DateEdit.Focus()
        Load_OK = "OK"

    End Sub

    ''' <summary>
    ''' �ھڽs�����J�ƾ�
    ''' </summary>
    ''' <param name="Str_GT_NO "></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadData(ByVal Str_GT_NO As String) As Boolean
        LoadData = False

        Dim objInfo As New ProductionSumTimeWorkGroupInfo
        Dim objList As New List(Of ProductionSumTimeWorkGroupInfo)
        Dim oc As New ProductionSumTimeWorkGroupControl

        objList = oc.ProductionSumTimeWorkGroup_GetList(Str_GT_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If objList.Count <= 0 Then
            MsgBox("�S���ƾڡI")
            LoadData = False
            Exit Function
        Else
            GT_DateEdit.EditValue = objList(0).GT_Date ''�ɶ�


            StartTimeEdit.Text = objList(0).GT_BeginTime
            EndTimeEdit.Text = objList(0).GT_EndTime  '

            txtTotal.Text = objList(0).GT_Total.ToString

            MemoGT_Remark.EditValue = objList(0).GT_Remark   '�ƪ`

            'labDepID.Text = objList(0).DepID    ''�����s��
            'labFacID.Text = objList(0).FacID    '�t�O

            GluG_NO.EditValue = objList(0).G_NO  '�����էO�A�A��H�� 
            txtPer_NO.EditValue = objList(0).Per_NO


            GridLookSampType.EditValue = objList(0).SampID

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
        End If '


        If GluG_NO.EditValue = "" Then
            MsgBox("�п�ܲէO�H��")
            GluG_NO.Select()
            CheckData = False
            Exit Function
        End If '


        If GridLookSampType.Text = "" Then
            MsgBox("�п������")
            GridLookSampType.Select()
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


        ''2012-12-25
        Dim pc As New ProductionPieceWorkGroupControl
        Dim pcil As New List(Of ProductionPieceWorkGroupInfo)
        pcil = pc.ProductionPieceWorkGroup_GetList(GluG_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pcil.Count <= 0 Then
        Else

            Dim pcA As New LFERP.Library.ProductionSumLock.ProductionSumLockControl
            Dim plA As New List(Of LFERP.Library.ProductionSumLock.ProductionSumLockInfo)
            plA = pcA.ProductionSumLock_GetList(Nothing, Nothing, pcil(0).DepID, Format(CDate(GT_DateEdit.EditValue), "yyyy/MM"))

            If plA.Count > 0 Then
                If plA(0).LockCheck = True Then
                    MsgBox("��e�����O���w��w������J!")
                    CheckData = False
                    Exit Function
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' �d�� �Y�@���u�A��ѡA�b�S�w�ɶ��q�A����ѻP�ⶵ�حp�ɤu�@ 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CHECK_Time() As Boolean
        Dim pil As New List(Of ProductionSumTimeWorkGroupInfo)
        Dim pc As New ProductionSumTimeWorkGroupControl
        Dim i As Integer

        Dim strDateEnd, strDateStart As DateTime
        Dim strDateEdit_S, strDateEdit_E As DateTime

        Dim CheckHourS1, CheckHourS2 As TimeSpan
        Dim CheckHourE1, CheckHourE2 As TimeSpan

        CHECK_Time = True
        If CaoTypeLabel.Text = "GTEdit" Then
            pil = pc.ProductionSumTimeWorkGroup_GetList(LabGT_NO.Text, txtPer_NO.EditValue, Nothing, Nothing, Nothing, GT_DateEdit.EditValue, Nothing, GT_DateEdit.EditValue, "in", Nothing)
        Else
            pil = pc.ProductionSumTimeWorkGroup_GetList(Nothing, txtPer_NO.EditValue, Nothing, Nothing, Nothing, GT_DateEdit.EditValue, Nothing, GT_DateEdit.EditValue, Nothing, Nothing)
        End If

        If pil.Count > 0 Then
        Else
            Exit Function
        End If

        For i = 0 To pil.Count - 1
            ''�֬d�ɶ��q----------------
            If pil(i).GT_BeginTime = Nothing Or pil(i).GT_EndTime = Nothing Then
                CHECK_Time = False
                Exit Function
            End If

            strDateEnd = DateTime.Parse(pil(i).GT_EndTime)  ''�O�󵲧��ɶ�              Ū���ƾڮw�����ɶ��q
            strDateStart = DateTime.Parse(pil(i).GT_BeginTime) ''�O��}�l�ɶ�

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


    '�D��Ӯɶ����ƭȡM�ഫ������������(�^

    Function QJSumDateValue(ByVal Date1 As Date, ByVal Date2 As Date) As Single
        'Dim i, X, Y, z As Long
        'Dim l As Single

        'i = Math.Abs(DateDiff("n", Date1, Date2))
        '' i = DateDiff("n", Date1, Date2)
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

        ''------------------------------

        Dim strDate1 As DateTime
        Dim strDate2 As DateTime
        Dim CheckHours As TimeSpan

        strDate1 = DateTime.Parse(StartTimeEdit.Text)
        strDate2 = DateTime.Parse(EndTimeEdit.Text)

        CheckHours = strDate2 - strDate1
        ' QJSumDateValue = Math.Abs(CSng(Format(CheckHours.TotalHours, "0.00")))
        QJSumDateValue = Format(CheckHours.TotalHours, "0.00")

        If QJSumDateValue < 0 Then
            QJSumDateValue = QJSumDateValue + 24
        End If
    End Function


    ''' <summary>
    ''' �ƾڼW�[
    ''' </summary>
    ''' <param name="S_model"></param>
    ''' <remarks></remarks>
    Private Sub SaveNew(ByVal S_model As String)
        Dim gc As New ProductionSumTimeWorkGroupControl
        Dim gi As New ProductionSumTimeWorkGroupInfo

        LabGT_NO.Text = GetProductionSumTimeWorkGroupNO()

        If LabGT_NO.Text <> "" Then
        Else
            MsgBox("�y����������ѡA�Э���!")
        End If

        gi.GT_NO = LabGT_NO.Text  ''�n�����o�s��
        gi.GT_Date = GT_DateEdit.EditValue  ''�έp�ɶ�

        gi.Per_NO = txtPer_NO.EditValue.Trim '���u�u��
        gi.GT_BeginTime = StartTimeEdit.Text
        gi.GT_EndTime = EndTimeEdit.Text

        gi.G_NO = GluG_NO.EditValue ''�էO
        gi.GT_Total = Val(txtTotal.Text) '�X�p
        gi.GT_Remark = MemoGT_Remark.Text '�ƪ`

        gi.GT_Action = InUserID

        ''���o�˿�,����---------------------------------------------------------------------------
        Dim PsaC As New LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeControl
        Dim PsaL As New List(Of LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeInfo)
        PsaL = PsaC.ProductionPiecePaySampType_GetList(GridLookSampType.EditValue, Nothing, True, Nothing)
        gi.SampID = GridLookSampType.EditValue
        gi.SampPrice = PsaL(0).SampPrice
        '-----------------------------------------------------------------------------------------

        '' ''�����A�t�O �ھڭ��u�s�� �q�p��H���򥻫H�����d�ߥX�s�A
        ''Dim ptc As New ProductionPiecePersonnelControl
        ''Dim pti As New List(Of ProductionPiecePersonnelInfo)
        ''pti = ptc.ProductionPiecePersonnel_GetList(Nothing, txtPer_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "False", Nothing)
        ''If pti.Count <= 0 Then
        ''    Exit Sub
        ''End If
        ''gi.DepID = pti(0).DepID   ''�����s��
        ''gi.FacID = pti(0).FacID   '�t�O 

        '�ھڲէO �d�ߥX�n�O�s������/�P�t�O
        Dim pc As New ProductionPieceWorkGroupControl
        Dim pcil As New List(Of ProductionPieceWorkGroupInfo)
        pcil = pc.ProductionPieceWorkGroup_GetList(GluG_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pcil.Count <= 0 Then
            Exit Sub
        End If
        gi.DepID = pcil(0).DepID
        gi.FacID = pcil(0).FacID


        If S_model = "S" Then
            If gc.ProductionSumTimeWorkGroup_Add(gi) = True Then
                MsgBox("�ƾګO�s���\")
                'Me.Dispose()
                Me.Close()
            End If
        Else
            If gc.ProductionSumTimeWorkGroup_Add(gi) = False Then
                MsgBox("�ƾګO�s����")
                ' Me.Dispose()
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

        GluG_NO.EditValue = Nothing
        ' GluG_NO.Properties.DataSource = Nothing
        txtPer_NO.EditValue = Nothing
        StartTimeEdit.Text = Nothing
        EndTimeEdit.Text = Nothing
        txtTotal.Text = ""
        MemoGT_Remark.Text = ""

        StartTimeEdit.Text = "00:00"
        EndTimeEdit.Text = "00:00"

        labDepID.Text = ""
        labFacID.Text = ""

        GluG_NO.Focus()
    End Sub



    ''' <summary>
    ''' �ק�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveEdit()
        Dim gc As New ProductionSumTimeWorkGroupControl
        Dim gi As New ProductionSumTimeWorkGroupInfo

        gi.GT_NO = LabGT_NO.Text
        gi.GT_Date = GT_DateEdit.Text  ''�έp�ɶ�


        gi.Per_NO = txtPer_NO.EditValue.Trim '���u�u��
        gi.GT_BeginTime = StartTimeEdit.Text
        gi.GT_EndTime = EndTimeEdit.Text
        gi.GT_Total = Val(txtTotal.Text) '�X�p

        gi.G_NO = GluG_NO.EditValue ''�էO
        gi.GT_Remark = MemoGT_Remark.Text '�ƪ`
        gi.GT_Action = InUserID


        ''���o�˿�,����---------------------------------------------------------------------------
        Dim PsaC As New LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeControl
        Dim PsaL As New List(Of LFERP.Library.ProductionPiecePaySampType.ProductionPiecePaySampTypeInfo)
        PsaL = PsaC.ProductionPiecePaySampType_GetList(GridLookSampType.EditValue, Nothing, True, Nothing)
        gi.SampID = GridLookSampType.EditValue
        gi.SampPrice = PsaL(0).SampPrice
        '-----------------------------------------------------------------------------------------

        '' ''�����A�t�O �ھڭ��u�s�� �q�p��H���򥻫H�����d�ߥX�s�A
        ''Dim ptc As New ProductionPiecePersonnelControl
        ''Dim pti As New List(Of ProductionPiecePersonnelInfo)
        ''pti = ptc.ProductionPiecePersonnel_GetList(Nothing, txtPer_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "False", Nothing)
        ''If pti.Count <= 0 Then
        ''    Exit Sub
        ''End If
        ''gi.DepID = pti(0).DepID   ''�����s��
        ''gi.FacID = pti(0).FacID   '�t�O 

        '�ھڲէO �d�ߥX�n�O�s������/�P�t�O
        Dim pc As New ProductionPieceWorkGroupControl
        Dim pcil As New List(Of ProductionPieceWorkGroupInfo)
        pcil = pc.ProductionPieceWorkGroup_GetList(GluG_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pcil.Count <= 0 Then
            Exit Sub
        End If
        gi.DepID = pcil(0).DepID
        gi.FacID = pcil(0).FacID


        If gc.ProductionSumTimeWorkGroup_Update(gi) = True Then
            MsgBox("�ƾګO�s���\")
            '  Me.Dispose()
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' ����y�������
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetProductionSumTimeWorkGroupNO() As String
        GetProductionSumTimeWorkGroupNO = ""

        Dim str1, str2 As String
        Dim gc1 As New ProductionSumTimeWorkGroupControl
        Dim gi1 As New ProductionSumTimeWorkGroupInfo

        str1 = Mid(Year(Now), 3)
        If CInt(Month(Now)) < 10 Then
            str2 = "0" & Month(Now)
        Else
            str2 = Month(Now)
        End If

        Dim Stra As String
        Stra = Trim(str1 & str2)

        gi1 = gc1.ProductionSumTimeWorkGroup_GetNO(Stra) '' Ū�����

        If gi1 Is Nothing Then
            GetProductionSumTimeWorkGroupNO = "GT" & str1 & str2 & "0000001"
        Else
            GetProductionSumTimeWorkGroupNO = "GT" & str1 & str2 & Mid((CInt(Mid(gi1.GT_NO, 7)) + 10000001), 2)
        End If

    End Function

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        'Me.Dispose()
        Me.Close()
    End Sub
    ''' <summary>
    ''' �K�[
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If CheckData() = False Then Exit Sub
        If CHECK_Time() = False Then Exit Sub

        If CaoTypeLabel.Text = "GTAdd" Then
            Call SaveNew("A")
            GluG_NO.Select()
        End If
    End Sub
    ''' <summary>
    ''' �O�s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckData() = False Then Exit Sub
        If CHECK_Time() = False Then Exit Sub


        Select Case CaoTypeLabel.Text
            Case "GTAdd"
                Call SaveNew("S")
            Case "GTEdit"
                Call SaveEdit()
        End Select
    End Sub

    Private Sub txtTotal_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotal.GotFocus
        On Error Resume Next
        If Load_OK = "OK" Then
            'txtTotal.Text = QJSumDateValue(CDate(StartTimeEdit.Text), CDate(EndTimeEdit.Text)) ''�p���`�p�ɶ�
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

    Private Sub txtPer_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPer_NO.EditValueChanged
        'If txtPer_NO.EditValue <> "" Then

        '    If Load_OK = "OK" Then
        '        labDepID.Text = GridLookUpEdit1View.GetFocusedRowCellValue("DepID").ToString
        '        labFacID.Text = GridLookUpEdit1View.GetFocusedRowCellValue("FacID").ToString
        '    End If

        '    ''���J�էO�H��
        '    Dim pc As New ProductionPiecePersonnelDayControl

        '    GluG_NO.Properties.DisplayMember = "Per_G_Name"
        '    GluG_NO.Properties.ValueMember = "G_NO"
        '    GluG_NO.Properties.DataSource = pc.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, txtPer_NO.EditValue, Nothing, Nothing, labDepID.Text, Nothing, Nothing, GT_DateEdit.EditValue, Nothing, "False", GT_DateEdit.EditValue)
        'End If
    End Sub


    Private Sub GluG_NO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GluG_NO.KeyDown, txtPer_NO.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub

    Private Sub GT_DateEdit_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GT_DateEdit.EditValueChanged
        'Dim ptc As New ProductionPiecePersonnelDayControl   ''���J ���u�s��---�m�W---����
        'txtPer_NO.Properties.DataSource = ptc.ProductionPiecePersonnelDay_GetList1("�L", Nothing, Nothing, strInDepID, Nothing, GT_DateEdit.EditValue, "False", GT_DateEdit.EditValue, "<>")
        'txtPer_NO.Properties.DisplayMember = "Per_Name"
        'txtPer_NO.Properties.ValueMember = "Per_NO"
    End Sub

    Private Sub GluG_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GluG_NO.EditValueChanged
        If GluG_NO.EditValue Is Nothing Then Exit Sub
        Dim ptc As New ProductionPiecePersonnelDayControl   ''���J ���u�s��---�m�W---���� ���էO <> �L
        txtPer_NO.Properties.DataSource = ptc.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, Nothing, Nothing, GluG_NO.EditValue, Nothing, Nothing, Nothing, GT_DateEdit.EditValue, Nothing, "False", GT_DateEdit.EditValue, Nothing)
        txtPer_NO.Properties.DisplayMember = "Per_NOName"
        txtPer_NO.Properties.ValueMember = "Per_NO"
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