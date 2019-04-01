Imports LFERP.Library.ProductionPiecePersonnel
Imports LFERP.Library.ProductionPieceWorkGroup

Public Class ProductionPiecePersonnel
    Dim ds As New DataSet

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ProductionPiecePersonnel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LabAutoID.Text = tempValue2
        CaoTypeLabel.Text = MTypeName

        MTypeName = Nothing
        tempValue2 = Nothing

        Dim pc1 As New ProductionPieceWorkGroupControl
        txtDepID.Properties.DataSource = pc1.DepFac_GetList1(strInDepID, Nothing, Nothing, Nothing)
        txtDepID.Properties.DisplayMember = "UserDep_Fac"
        txtDepID.Properties.ValueMember = "DepID"

        TxtPer_NO.Properties.ReadOnly = False

        CreateTable()
        AddRow()

        Select Case CaoTypeLabel.Text
            Case "PerAdd"
                Me.Text = "���u�W��-�W�["
                LabCaption.Text = "���u�W��-�W�["
            Case "PerEdit"
                If LoadData(LabAutoID.Text) = False Then Exit Sub
                Me.Text = "���u�W��-�ק�" & "[" & LabAutoID.Text & "]"
                LabCaption.Text = "���u�W��-�ק�"
                cmdAdd.Visible = False
                TxtPer_NO.Enabled = False
                TxtPer_NO.Properties.ReadOnly = True

            Case "PerView"
                If LoadData(LabAutoID.Text) = False Then Exit Sub
                '�d��
                luePer_PayType.Enabled = False
                cmdSave.Visible = False
                cmdAdd.Visible = False
                GluG_NO.Enabled = False
                TxtPer_NO.Enabled = False
                txtDepID.Enabled = False
                TxtPer_Name.Enabled = False
                txtPer_DayPrice.Enabled = False
                MemoPer_Remark.Enabled = False
                cboPer_Class.Enabled = False

                Me.Text = "���u�W��-�d��" & "[" & LabAutoID.Text & "]"
                LabCaption.Text = "���u�W��-�d��"
        End Select

        PowerUser()

    End Sub

    Sub PowerUser()
        Dim pmws As New LFERP.SystemManager.PermissionModuleWarrantSubController
        Dim pmwiL As List(Of LFERP.SystemManager.PermissionModuleWarrantSubInfo)

        txtPer_DayPrice.Enabled = True

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160306")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
            Else
                txtPer_DayPrice.Properties.PasswordChar = "*"
                txtPer_DayPrice.Enabled = False
            End If
        End If

    End Sub


    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("Type")
            .Columns.Add("Per_PayType")
            .Columns.Add("Remark")
        End With
        luePer_PayType.Properties.DataSource = ds.Tables("Type")
        luePer_PayType.Properties.DisplayMember = "Per_PayType"
        luePer_PayType.Properties.ValueMember = "Per_PayType"


        ''�W�[�L�էO���u�A�i��ӤH�p��2012-5-8

        With ds.Tables.Add("G_NOName")
            .Columns.Add("G_NO")
            .Columns.Add("G_Name")
        End With
        GluG_NO.Properties.DisplayMember = "G_Name"
        GluG_NO.Properties.ValueMember = "G_NO"
        GluG_NO.Properties.DataSource = ds.Tables("G_NOName")

    End Sub

    Sub AddRow()
        Dim row As DataRow
        ds.Tables("Type").Clear()
        row = ds.Tables("Type").NewRow()
        row("Per_PayType") = "�p��"
        row("Remark") = "�u�ǭp����"
        ds.Tables("Type").Rows.Add(row)
        row = ds.Tables("Type").NewRow()
        row("Per_PayType") = "�p��"
        row("Remark") = "���ɤ�CNC�p����"
        ds.Tables("Type").Rows.Add(row)
        row = ds.Tables("Type").NewRow()
        row("Per_PayType") = "���~"
        row("Remark") = "�ӥ]�դ����~���u"
        ds.Tables("Type").Rows.Add(row)
    End Sub

    ''' <summary>
    ''' ���J�ƾ�
    ''' </summary>
    ''' <param name="Str_AutoID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadData(ByVal Str_AutoID As String) As Boolean
        LoadData = False

        Dim objInfo As New ProductionPiecePersonnelInfo
        Dim objList As New List(Of ProductionPiecePersonnelInfo)
        Dim oc As New ProductionPiecePersonnelControl

        objList = oc.ProductionPiecePersonnel_GetList(Str_AutoID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If objList.Count <= 0 Then
            MsgBox("�S���ƾڡI")
            LoadData = False
            Exit Function
        Else

            txtFacID.Text = objList(0).FacID

            luePer_PayType.Text = objList(0).Per_PayType  ''�~������
            TxtPer_NO.Text = objList(0).Per_NO '���u�s��
            TxtPer_Name.Text = objList(0).Per_Name '���u�m�W


            txtPer_DayPrice.Text = objList(0).Per_DayPrice '  /���~         
            MemoPer_Remark.Text = objList(0).Per_Remark '     /�ƪ`

            txtDepID.EditValue = objList(0).DepID
            GluG_NO.EditValue = objList(0).G_NO  ''�էO�W
            cboPer_Class.Text = objList(0).Per_Class  '�Z��

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

        If txtDepID.EditValue = "" Then
            MsgBox("�п�ܳ���")
            txtDepID.Select()
            CheckData = False
            Exit Function
        End If

        If TxtPer_NO.Text = "" Then
            MsgBox("���u�s�����ର��")
            TxtPer_NO.Focus()
            CheckData = False
            Exit Function   '
        End If

        If TxtPer_Name.Text = "" Then
            MsgBox("���u�m�W���ର��")
            TxtPer_Name.Focus()
            CheckData = False
            Exit Function
        End If

        If GluG_NO.EditValue = "" Then
            MsgBox("���u�էO�H�������")
            GluG_NO.Focus()
            CheckData = False
            Exit Function
        End If

        If cboPer_Class.Text = "" Then
            MsgBox("�Z��H�������")
            cboPer_Class.Focus()
            CheckData = False
            Exit Function
        End If

        If luePer_PayType.Text = "" Then
            MsgBox("�~��������J���~")
            luePer_PayType.Focus()
            CheckData = False
            Exit Function
        End If


        ''�p�G���O�p��ɡA�@�w�n��J�~������ 2012-9-4

        If luePer_PayType.EditValue <> "�p��" Then
            If Val(txtPer_DayPrice.Text) <= 0 Then
                MsgBox("�п�J���~�I")
                luePer_PayType.Focus()
                CheckData = False
                Exit Function
            End If
        End If



        ''�[�P�_���H�u�b�H�����u�s�b�@���O��  2012-6-5
        If CaoTypeLabel.Text = "PerEdit" Then ''�ק�ɤ��P�_�A������ק�s��
        Else
            Dim objL As New List(Of ProductionPiecePersonnelInfo)
            Dim objc As New ProductionPiecePersonnelControl

            objL = objc.ProductionPiecePersonnel_GetList(Nothing, TxtPer_NO.Text.Trim, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If objL.Count > 0 Then
                MsgBox("�����u�s���w�s�b,���ˬd!")
                TxtPer_NO.Focus()
                CheckData = False
                Exit Function
            End If
        End If


    End Function
    ''' <summary>
    ''' �ƾڼW�[
    ''' </summary>
    ''' <param name="S_model"></param>
    ''' <remarks></remarks>
    Private Sub SaveNew(ByVal S_model As String)
        Dim gc As New ProductionPiecePersonnelControl
        Dim gi As New ProductionPiecePersonnelInfo

        gi.Per_PayType = luePer_PayType.Text  ''�~ �� ���O

        gi.DepID = txtDepID.EditValue  ''�����s��
        gi.FacID = txtFacID.Text  '�t�O

        gi.Per_NO = TxtPer_NO.Text.Trim '���u�u��
        gi.Per_Name = TxtPer_Name.Text.Trim '/���u�W��
        gi.G_NO = GluG_NO.EditValue '�էO�էO�s��
        gi.Per_DayPrice = Val(txtPer_DayPrice.Text) '���~
        gi.Per_Remark = MemoPer_Remark.Text '�ƪ`
        gi.Per_Action = InUserID
        gi.Per_Date = Format(Now, "yyyy/MM/dd")
        gi.Per_Class = cboPer_Class.Text

        If S_model = "S" Then
            If gc.ProductionPiecePersonnel_Add(gi) = True Then
                MsgBox("�ƾګO�s���\")
                Me.Close()
            End If
        Else
            If gc.ProductionPiecePersonnel_Add(gi) = False Then
                MsgBox("�ƾګO�s����")
                Me.Close()
            End If
        End If
    End Sub

    ''' <summary>
    ''' �ק�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveEdit()
        Dim gc As New ProductionPiecePersonnelControl
        Dim gi As New ProductionPiecePersonnelInfo

        gi.AutoID = LabAutoID.Text.Trim

        gi.Per_PayType = luePer_PayType.Text ''�~ �� ���O

        gi.DepID = txtDepID.EditValue  ''�����s��
        gi.FacID = txtFacID.Text

        gi.Per_NO = TxtPer_NO.Text.Trim
        gi.Per_Name = TxtPer_Name.Text.Trim
        gi.G_NO = GluG_NO.EditValue '�էO
        gi.Per_DayPrice = Val(txtPer_DayPrice.Text)
        gi.Per_Remark = MemoPer_Remark.Text
        gi.Per_Class = cboPer_Class.Text

        gi.Per_Action = InUserID
        gi.Per_Date = Format(Now, "yyyy/MM/dd")

        If gc.ProductionPiecePersonnel_Update(gi) = True Then
            MsgBox("�ƾګO�s���\")
            Me.Close()
        End If
    End Sub


    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If CheckData() = False Then Exit Sub

        If CaoTypeLabel.Text = "PerAdd" Then '
            Call SaveNew("A")
            'Dim i%
            'For i = 1 To 6
            '    GroupBox1.Controls.Item(i).Text = ""
            'Next


            TxtPer_NO.Text = ""
            TxtPer_Name.Text = ""


            TxtPer_NO.Select()
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckData() = False Then Exit Sub

        Select Case CaoTypeLabel.Text
            Case "PerAdd"
                Call SaveNew("S")
            Case "PerEdit"
                Call SaveEdit()
        End Select
    End Sub

    Private Sub txtDepID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDepID.EditValueChanged
        If txtDepID.EditValue <> "" Then
        Else
            Exit Sub
        End If

        Dim pc1 As New ProductionPieceWorkGroupControl
        Dim piList As New List(Of ProductionPieceWorkGroupInfo)

        piList = pc1.DepFac_GetList1(txtDepID.EditValue, Nothing, Nothing, Nothing)

        If piList.Count <= 0 Then Exit Sub

        txtFacID.Text = piList(0).FacID '�d�ߥX�t�O

        'GluG_NO.Properties.DisplayMember = "G_Name"
        'GluG_NO.Properties.ValueMember = "G_NO"
        'GluG_NO.Properties.DataSource = pc.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, txtDepID.EditValue, Nothing, Nothing, Nothing, Nothing)

        Dim pc As New ProductionPieceWorkGroupControl
        Dim pcL As List(Of ProductionPieceWorkGroupInfo)

        ds.Tables("G_NOName").Clear()

        Dim row1 As DataRow
        row1 = ds.Tables("G_NOName").NewRow
        row1("G_NO") = "�L"
        row1("G_Name") = "�L"
        ds.Tables("G_NOName").Rows.Add(row1)
        pcL = pc.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, txtDepID.EditValue, txtFacID.Text, Nothing, Nothing, Nothing)
        If pcL.Count = 0 Then
        Else
            Dim i As Integer
            For i = 0 To pcL.Count - 1
                Dim row As DataRow
                row = ds.Tables("G_NOName").NewRow
                row("G_NO") = pcL(i).G_NO
                row("G_Name") = pcL(i).G_Name
                ds.Tables("G_NOName").Rows.Add(row)
            Next
        End If

    End Sub

    Private Sub txtDepID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDepID.KeyDown, GluG_NO.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub

    Private Sub TxtPer_NO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPer_NO.KeyDown
        If e.KeyCode = Keys.Enter Then
            TxtPer_Name.Text = GetName(TxtPer_NO.Text.Trim)
            ' luePer_PayType.EditValue = strPayType
            'txtDepID.EditValue = strDepID
            'MsgBox(strDepID)
            If TxtPer_Name.Text <> "" Then
                luePer_PayType.Focus()
                'txtDepID.Focus()
                'txtDepID.ShowPopup()
            End If

        End If
    End Sub


    Private Sub txtPer_DayPrice_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPer_DayPrice.KeyUp
        Dim m As New System.Text.RegularExpressions.Regex("^+?(\d+(\.\d*)?|\.\d+)$")  '��ܾ��,���B�I�ƥ��h��F��

        If m.IsMatch(txtPer_DayPrice.Text) = True Then
        Else
            txtPer_DayPrice.Text = Nothing
            Exit Sub
        End If
    End Sub
End Class