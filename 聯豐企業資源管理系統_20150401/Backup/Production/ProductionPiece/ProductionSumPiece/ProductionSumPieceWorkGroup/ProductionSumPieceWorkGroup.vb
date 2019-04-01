Imports LFERP.Library.ProductionPieceProcess
Imports LFERP.Library.ProductionPiecePersonnel
Imports LFERP.Library.ProductionSumPieceWorkGroup
Imports LFERP.Library.ProductionPieceWorkGroup

Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Production.Datasetting

Public Class ProductionSumPieceWorkGroup

    Dim StrTypeLabel As String '���� 
    Dim StrGP_NO As String

    Dim Load_OK As String ''�T�wLoad�ƥ�O�_�w���J����
    Dim ds As New DataSet


    Dim StrDepID, StrFacID, StrG_NO As String

    Private Sub ProductionSumPieceWorkGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Load_OK = ""
        Clr_Text()

        ButtSing.Enabled = True
        cmdAdd.Visible = True
        GLookUpEditPS_NO.Enabled = True
        cmdSave.Visible = True
        cmdAdd.Visible = True

        StrGP_NO = tempValue2 '�s��
        StrTypeLabel = MTypeName  '�ާ@����

        tempValue2 = Nothing
        MTypeName = Nothing
        tempValue4 = Nothing
        tempValue3 = Nothing

        CreateTable()

        If StrTypeLabel <> "GPView" Then
            Dim pc1 As New ProductionPieceWorkGroupControl
            GluG_NO.Properties.DisplayMember = "G_NOName"
            GluG_NO.Properties.ValueMember = "G_NO"
            GluG_NO.Properties.DataSource = pc1.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing)
        End If

        'Dim mc As New ProductController
        'gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
        'gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
        'gluPM_M_Code.Properties.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

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

        PP_DateEdit.EditValue = Format(Now, "yyyy/MM/dd")

        Select Case StrTypeLabel
            Case "GPAdd"
                Me.Text = "�էO�p��--�W�["
            Case "GPEdit"
                If LoadData(StrGP_NO) = False Then Exit Sub
                Me.Text = "�էO�p��--�ק�" & "[" & StrGP_NO & "]"
                cmdAdd.Visible = False
                ButtSing.Enabled = False   ''�ק�u����

            Case "GPView"

                If LoadData(StrGP_NO) = False Then Exit Sub
                '�d��
                cmdSave.Visible = False
                cmdAdd.Visible = False
                ButtSing.Enabled = False

                Me.Text = "�էO�p��--�d��" & "[" & StrGP_NO & "]"
        End Select

        ButtSing.Text = "���"
        Me.Width = ButtSing.Location.X + ButtSing.Width + 12

        Me.Left = Me.Left + Grid1.Width / 2

        
        If StrTypeLabel = "GPEdit" Then
            txtPP_Qty.Select()
        Else
            GluG_NO.Select()
            GluG_NO.Focus()
        End If


        Me.GLookUpEditPS_NO.Properties.PopupFormWidth = 700

        Load_OK = "OK"
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
            .Columns.Add("PP_Factor", GetType(Single))
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

    End Sub

    Function LoadData(ByVal StrGP_NO As String) As Boolean
        LoadData = False

        Dim objInfo As New ProductionSumPieceWorkGroupInfo
        Dim objList As New List(Of ProductionSumPieceWorkGroupInfo)
        Dim oc As New ProductionSumPieceWorkGroupControl

        objList = oc.ProductionSumPieceWorkGroup_GetList(Nothing, StrGP_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If objList.Count <= 0 Then
            MsgBox("�S���ƾڡI")
            LoadData = False
            Exit Function
        Else
            PP_DateEdit.EditValue = objList(0).GP_Date
            If StrTypeLabel = "GPView" Then
                GluG_NO.Enabled = False
                GluG_NO.Properties.NullText = objList(0).GP_G_Name

                gluPM_M_Code.Enabled = False
                gluPM_M_Code.Properties.NullText = objList(0).PM_M_Code.ToString  '���~�s��

                gluPM_Type.Enabled = False
                gluPM_Type.Properties.NullText = objList(0).PM_Type.ToString    '����

                GLookUpEditPS_NO.Enabled = False
                GLookUpEditPS_NO.Properties.NullText = objList(0).PS_NameS

                PerButton.Enabled = False

            Else
                GluG_NO.EditValue = objList(0).G_NO
                gluPM_M_Code.EditValue = objList(0).PM_M_Code.ToString  '���~�s��
                gluPM_Type.EditValue = objList(0).PM_Type.ToString    '����
                GLookUpEditPS_NO.EditValue = objList(0).PP_AutoID
            End If

            StrDepID = objList(0).DepID.ToString  '�λP�ɤJ�C��էO�A�H���W�� 
            StrFacID = objList(0).FacID.ToString
            StrG_NO = objList(0).G_NO.ToString


            cboPro_Type.Text = objList(0).Pro_Type.ToString  '�u������ 
            txtPP_Qty.Text = objList(0).GP_Qty.ToString


            MemoPP_Remark.Text = objList(0).GP_Remark  '�ƪ`
            txtGP_factor.Text = objList(0).GP_factor

            LoadData = True
        End If
    End Function

    Private Function CheckData() As Boolean
        CheckData = True

        If GluG_NO.EditValue = "" Then
            MsgBox("�п�ܲէO�s��")
            GluG_NO.Select()
            CheckData = False
            Exit Function
        End If

        If gluPM_M_Code.EditValue = "" Then
            MsgBox("�п�J���~�s���A���ˬd")
            gluPM_M_Code.Focus()
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


        Dim Check_DepID As String
        Dim pc As New ProductionPieceWorkGroupControl
        Dim pcil As New List(Of ProductionPieceWorkGroupInfo)
        pcil = pc.ProductionPieceWorkGroup_GetList(GluG_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Check_DepID = pcil(0).DepID
  

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
            If pci(0).DPT_ID <> Check_DepID Then
                MsgBox("���էO�Ҧb�������s�b���u��," & "  ���ˬd!")
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
                    If pci(0).DPT_ID <> Check_DepID Then
                        MsgBox("���էO�Ҧb�������s�b���u��," & ds.Tables("PieceProcess").Rows(i)("PP_N_Name") & "  ���ˬd!")
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


        Dim pcA As New LFERP.Library.ProductionSumLock.ProductionSumLockControl
        Dim plA As New List(Of LFERP.Library.ProductionSumLock.ProductionSumLockInfo)
        plA = pcA.ProductionSumLock_GetList(Nothing, Nothing, Check_DepID, Format(CDate(PP_DateEdit.EditValue), "yyyy/MM"))

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
        Dim gc As New ProductionSumPieceWorkGroupControl
        Dim gi As New ProductionSumPieceWorkGroupInfo
        Dim StrGP_NO_LS As String

        gi.G_NO = GluG_NO.EditValue.Trim '�էO

        gi.Pro_Type = cboPro_Type.Text
        gi.PM_M_Code = gluPM_M_Code.EditValue
        gi.PM_Type = gluPM_Type.EditValue
        gi.GP_Qty = Val(txtPP_Qty.Text)   ''

        gi.GP_Date = PP_DateEdit.Text
        gi.GP_Action = InUserID

        StrGP_NO_LS = ProductionSumPieceWorkGroupNO()

        If StrGP_NO_LS <> "" Then
        Else
            MsgBox("�y����������ѡA�Э���!")
        End If

        gi.GP_NO = StrGP_NO_LS  ''�n�����o�s��
        ''-----------------------------------------------
        Dim pc As New ProductionPieceWorkGroupControl
        Dim pcil As New List(Of ProductionPieceWorkGroupInfo)
        pcil = pc.ProductionPieceWorkGroup_GetList(GluG_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pcil.Count <= 0 Then
            Exit Sub
        End If

        gi.DepID = pcil(0).DepID
        gi.FacID = pcil(0).FacID
        ''�ھ�AutoID�d��----------------------------------------------------------------------------------

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

        gi.GP_Price = pci(0).PP_Price.ToString   ''�u��
        ''autoID �]�s�@�U
        gi.PP_AutoID = GLookUpEditPS_NO.EditValue
        'gi.DepID = pci(0).DPT_ID ''����

        gi.GP_Remark = MemoPP_Remark.Text

        gi.GP_factor = Val(txtGP_factor.Text)
        ''---------------------------------------------------------------------------------------------------------
        If S_model = "S" Then
            If gc.ProductionSumPieceWorkGroup_Add(gi) = True Then
                MsgBox("�ƾګO�s���\")
                Me.Close()
            End If
        Else
            If gc.ProductionSumPieceWorkGroup_Add(gi) = False Then
                MsgBox("�ƾګO�s����")
                Me.Close()
            Else
                ' Clr_Text()
                Clr_TextNoG_NO()
            End If
        End If

    End Sub
    ''' <summary>
    ''' �h��O���O�s
    ''' </summary>
    ''' <param name="S_model"></param>
    ''' <remarks></remarks>
    Private Sub SaveNewTwo(ByVal S_model As String)
        Dim gc As New ProductionSumPieceWorkGroupControl
        Dim gi As New ProductionSumPieceWorkGroupInfo

        Dim i As Integer

        Dim StrPP_NO_LS As String

        gi.G_NO = GluG_NO.EditValue.Trim '���u�u��

        gi.Pro_Type = cboPro_Type.Text
        gi.PM_M_Code = gluPM_M_Code.EditValue
        gi.PM_Type = gluPM_Type.EditValue
        gi.GP_Qty = Val(txtPP_Qty.Text)   ''

        gi.GP_Date = PP_DateEdit.Text
        gi.GP_Action = InUserID
        gi.GP_Remark = MemoPP_Remark.Text

        ' gi.GP_factor = Val(txtGP_factor.Text)

        ''''----------------------------------------------------------------------------------------------------
        Dim pc As New ProductionPieceWorkGroupControl
        Dim pcil As New List(Of ProductionPieceWorkGroupInfo)
        pcil = pc.ProductionPieceWorkGroup_GetList(GluG_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pcil.Count <= 0 Then
            Exit Sub
        End If

        gi.DepID = pcil(0).DepID
        gi.FacID = pcil(0).FacID

        ''''----------------------------------------------------------------------------------------------------
        If ds.Tables("PieceProcess").Rows.Count >= 1 Then
        Else
            MsgBox("�u�ǦW�٥����T���,���ˬd!")
            Exit Sub
        End If

        For i = 0 To ds.Tables("PieceProcess").Rows.Count - 1
            If ds.Tables("PieceProcess").Rows(i)("GoIn") = True Then

                StrPP_NO_LS = ProductionSumPieceWorkGroupNO()
                If StrPP_NO_LS <> "" Then
                Else
                    MsgBox("�y����������ѡA�Э���!")
                End If
                gi.GP_NO = StrPP_NO_LS  ''�n�����o�s��


                gi.PS_NO = ds.Tables("PieceProcess").Rows(i)("PS_NO")  ''�j�u�ǽs��
                gi.PS_NameS = ds.Tables("PieceProcess").Rows(i)("PP_N_Name") '�p�u�ǦW��

                gi.GP_Price = ds.Tables("PieceProcess").Rows(i)("PP_Price").ToString  ''�u��
                gi.PP_AutoID = ds.Tables("PieceProcess").Rows(i)("AutoID") ''�u��autoID �]�s�@�U

                gi.GP_factor = ds.Tables("PieceProcess").Rows(i)("PP_Factor")


                If gc.ProductionSumPieceWorkGroup_Add(gi) = False Then
                    MsgBox("�ƾګO�s����,���ˬd")
                    Exit Sub
                End If
            End If
        Next

        If S_model = "S" Then
            MsgBox("�ƾګO�s���\")
            Me.Close()
        Else
            ' Clr_Text()
            Clr_TextNoG_NO()
            ds.Tables("PieceProcess").Clear()
        End If


    End Sub


    Sub Clr_TextNoG_NO()
        'GluG_NO.EditValue = Nothing
        gluPM_M_Code.EditValue = Nothing
        gluPM_Type.EditValue = Nothing
        'txtPP_Qty.Text = ""
        MemoPP_Remark.Text = ""
        GLookUpEditPS_NO.EditValue = Nothing
        txtGP_factor.Text = ""

        gluPM_M_Code.Select()
    End Sub

    Sub Clr_Text()
        GluG_NO.EditValue = Nothing
        gluPM_M_Code.EditValue = Nothing
        gluPM_Type.EditValue = Nothing
        txtPP_Qty.Text = ""
        MemoPP_Remark.Text = ""
        GLookUpEditPS_NO.EditValue = Nothing
        txtGP_factor.Text = ""

        GluG_NO.Select()
    End Sub


    Function ProductionSumPieceWorkGroupNO() As String

        ProductionSumPieceWorkGroupNO = ""

        Dim str1, str2 As String
        Dim gc1 As New ProductionSumPieceWorkGroupControl
        Dim gi1 As New ProductionSumPieceWorkGroupInfo

        str1 = Mid(Year(Now), 3)
        If CInt(Month(Now)) < 10 Then
            str2 = "0" & Month(Now)
        Else
            str2 = Month(Now)
        End If

        Dim Stra As String
        Stra = Trim(str1 & str2)

        gi1 = gc1.ProductionSumPieceWorkGroup_GetNO(Stra) '' Ū�����

        If gi1 Is Nothing Then
            ProductionSumPieceWorkGroupNO = "GP" & str1 & str2 & "0000001"
        Else
            ProductionSumPieceWorkGroupNO = "GP" & str1 & str2 & Mid((CInt(Mid(gi1.GP_NO, 7)) + 10000001), 2)
        End If
    End Function

    ''' <summary>
    ''' �ק�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveEdit()
        Dim gc As New ProductionSumPieceWorkGroupControl
        Dim gi As New ProductionSumPieceWorkGroupInfo

        gi.G_NO = GluG_NO.EditValue.Trim '���u�u��

        gi.Pro_Type = cboPro_Type.Text
        gi.PM_M_Code = gluPM_M_Code.EditValue
        gi.PM_Type = gluPM_Type.EditValue
        gi.GP_Qty = Val(txtPP_Qty.Text)   ''

        gi.GP_Date = PP_DateEdit.Text
        gi.GP_Action = InUserID

        'StrPP_NO = StrPP_NO
        gi.GP_factor = Val(txtGP_factor.Text)

        gi.GP_Remark = MemoPP_Remark.Text

        If StrGP_NO <> "" Then
        Else
            MsgBox("�y����������ѡA�Э���!")
        End If

        gi.GP_NO = StrGP_NO  ''�n�����o�s��

        ''---------------------------------------------------------------------------------------------------------------------------
        Dim pc As New ProductionPieceWorkGroupControl
        Dim pcil As New List(Of ProductionPieceWorkGroupInfo)
        pcil = pc.ProductionPieceWorkGroup_GetList(GluG_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pcil.Count <= 0 Then
            Exit Sub
        End If
        gi.DepID = pcil(0).DepID
        gi.FacID = pcil(0).FacID
        ''�ھ�AutoID�d��-------------------------------------------------------------------------------------------------

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

        gi.GP_Price = pci(0).PP_Price  ''�u��
        ''autoID �]�s�@�U
        gi.PP_AutoID = GLookUpEditPS_NO.EditValue
        ' gi.DepID = pci(0).DPT_ID ''����

        If gc.ProductionSumPieceWorkGroup_Update(gi) = True Then
            MsgBox("�ƾګO�s���\")
            Me.Close()
        End If

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub


    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If StrTypeLabel = "GPAdd" Then
            If CheckData() = False Then Exit Sub

            If ButtSing.Text = "���" Then
                SaveNewOne("A")
            Else
                SaveNewTwo("A")
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If StrTypeLabel = "GPAdd" Then
            If CheckData() = False Then Exit Sub

            If ButtSing.Text = "���" Then
                SaveNewOne("S")
            Else
                SaveNewTwo("S")
            End If
        End If

        If StrTypeLabel = "GPEdit" Then

            If CheckData() = False Then Exit Sub

            SaveEdit()
        End If
    End Sub



    Private Sub txtPP_Qty_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim m As New System.Text.RegularExpressions.Regex("^\+?[1-9][0-9]*$")  '��ܾ��,���B�I�ƥ��h��F��

        If m.IsMatch(txtPP_Qty.Text) = True Then
        Else
            txtPP_Qty.Text = Nothing
            Exit Sub
        End If
    End Sub

    Private Sub txtGP_factor_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim m As New System.Text.RegularExpressions.Regex("^+?(\d+(\.\d*)?|\.\d+)$")  '��ܾ��,���B�I�ƥ��h��F��

        If m.IsMatch(txtGP_factor.Text) = True Then
        Else
            txtGP_factor.Text = Nothing
            Exit Sub
        End If
    End Sub

    Private Sub gluPM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_M_Code.EditValueChanged
        If StrTypeLabel = "GPView" Then Exit Sub

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
        If StrTypeLabel = "GPView" Then Exit Sub

        '���J �p��u����
        If gluPM_Type.EditValue Is Nothing Then Exit Sub

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
                    row("PP_Factor") = pci(i).PP_Factor

                    ds.Tables("PieceProcess").Rows.Add(row)
                End If
            Next
        End If

    End Sub



    Private Sub GluG_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GluG_NO.EditValueChanged
        On Error Resume Next
        If Load_OK = "OK" Then
            StrDepID = GridView5.GetFocusedRowCellValue("DepID").ToString
            StrFacID = GridView5.GetFocusedRowCellValue("FacID").ToString
            StrG_NO = GluG_NO.EditValue
        End If
    End Sub


    Private Sub ButtSing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtSing.Click
        If ButtSing.Text = "���" Then
            ButtSing.Text = "�h��"
            GLookUpEditPS_NO.Enabled = False
            Me.Width = ButtSing.Location.X + ButtSing.Width + Grid1.Width + 12
        Else
            GLookUpEditPS_NO.Enabled = True
            ButtSing.Text = "���"
            Me.Width = ButtSing.Location.X + ButtSing.Width + 12
        End If
    End Sub



    Private Sub PerButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PerButton.Click
        If StrG_NO <> "" Then

            tempValue2 = StrFacID
            tempValue3 = StrDepID
            tempValue4 = StrG_NO

            ProductionPiecePersonnelDay.ShowDialog()
            ProductionPiecePersonnelDay.Dispose()

            tempValue2 = Nothing
            tempValue3 = Nothing
            tempValue4 = Nothing

        Else
            MsgBox("�п�J�էO�s��!")
        End If
    End Sub
    ''' <summary>
    ''' �� �Ů���u�X
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GluG_NO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GluG_NO.KeyDown, gluPM_M_Code.KeyDown, gluPM_Type.KeyDown, GLookUpEditPS_NO.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub

    Private Sub GLookUpEditPS_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUpEditPS_NO.EditValueChanged
        If Load_OK <> "OK" Then Exit Sub
        If GLookUpEditPS_NO.EditValue Is Nothing Then Exit Sub

        Dim pcc1 As New ProductionPieceProcessControl
        Dim pci1 As List(Of ProductionPieceProcessInfo)

        pci1 = pcc1.ProductionPieceProcess_GetList(GLookUpEditPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pci1.Count <= 0 Then
            Exit Sub
        End If

        txtGP_factor.Text = pci1(0).PP_Factor.ToString
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

    Private Sub ButtonUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUpdate.Click
        If GLookUpEditPS_NO.EditValue Is Nothing Then Exit Sub

        Dim pcc1 As New ProductionPieceProcessControl
        Dim pci1 As List(Of ProductionPieceProcessInfo)

        pci1 = pcc1.ProductionPieceProcess_GetList(GLookUpEditPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pci1.Count <= 0 Then
            Exit Sub
        End If

        txtGP_factor.Text = pci1(0).PP_Factor.ToString
    End Sub
End Class