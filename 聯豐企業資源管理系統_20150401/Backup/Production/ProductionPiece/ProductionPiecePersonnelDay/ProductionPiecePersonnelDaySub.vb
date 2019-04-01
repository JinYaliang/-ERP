Imports LFERP.Library.ProductionPieceWorkGroup
Imports LFERP.Library.ProductionPiecePersonnel
Imports LFERP.Library.ProductionPiecePersonnelDay


Public Class ProductionPiecePersonnelDaySub
    Dim ds As New DataSet

    ''' <summary>
    ''' ���J�ƾڪ��c
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateTables()
        ds.Tables.Clear()

        With ds.Tables.Add("ProductionPiecePersonnelDay")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("Per_NO", GetType(String))
            .Columns.Add("Per_Name", GetType(String))
            .Columns.Add("G_NO", GetType(String))
            .Columns.Add("Per_G_Name", GetType(String))

            .Columns.Add("Per_PayType", GetType(String))
            .Columns.Add("Per_DayPrice", GetType(String))
            .Columns.Add("Per_Remark", GetType(String))
            .Columns.Add("Per_Class", GetType(String))

        End With

        With ds.Tables.Add("ProductionPiecePersonnelDayDelete")
            .Columns.Add("Per_NO", GetType(String))
            .Columns.Add("G_NO", GetType(String))
            .Columns.Add("AutoID", GetType(String))
        End With

        Grid1.DataSource = ds.Tables("ProductionPiecePersonnelDay")

        'RepositoryItemComboBox1.Items.Clear()
        'RepositoryItemComboBox1.Items.Add("�p��-�u�ǭp����")
        'RepositoryItemComboBox1.Items.Add("�p��-���ɤ�CNC�p����")
        'RepositoryItemComboBox1.Items.Add("���~-�ӥ]�դ����~���u")

        'Dim gc As New ProductionPieceWorkGroupControl
        'RepositoryItemLookUpEdit2.DisplayMember = "G_Name"
        'RepositoryItemLookUpEdit2.ValueMember = "G_NO"
        'RepositoryItemLookUpEdit2.DataSource = gc.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)


        With ds.Tables.Add("Type")
            .Columns.Add("Per_PayType")
            .Columns.Add("Remark")
        End With
        RepositoryItemLookUpEdit3.DataSource = ds.Tables("Type")
        RepositoryItemLookUpEdit3.DisplayMember = "Per_PayType"
        RepositoryItemLookUpEdit3.ValueMember = "Per_PayType"

        AddRow()


        '' ''�W�[�L�էO���u�A�i��ӤH�p��2012-5-8
        With ds.Tables.Add("G_NOName")
            .Columns.Add("G_NO")
            .Columns.Add("G_Name")
        End With
        RepositoryItemLookUpEdit2.DisplayMember = "G_Name"
        RepositoryItemLookUpEdit2.ValueMember = "G_NO"
        RepositoryItemLookUpEdit2.DataSource = ds.Tables("G_NOName")

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

    Private Sub ProductionPiecePersonnelDaySub_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        labAutoID.Text = tempValue2
        CaoTypeLabel.Text = MTypeName '

        tempValue2 = ""
        MTypeName = ""

        Dim pc As New ProductionPieceWorkGroupControl
        txtDepID.Properties.DataSource = pc.DepFac_GetList1(strInDepID, Nothing, Nothing, Nothing)
        txtDepID.Properties.DisplayMember = "UserDep_Fac"
        txtDepID.Properties.ValueMember = "DepID"

        Per_DateEdit.Text = Format(Now, "yyyy/MM/dd")

        CreateTables()

        ' Per_DateEdit.Enabled = False

        Select Case CaoTypeLabel.Text
            Case "PD_add"
                LabCaption.Text = "�p��-�W�[-�C��W��"

            Case "PD_edit"
                If LoadData(labAutoID.Text) = False Then Exit Sub
                LabCaption.Text = "�p��-�ק�-�C��W��"
                'GridView2.Columns.Item("Per_NO").OptionsColumn.ReadOnly = True
                'GridView2.Columns.Item("Per_Name").OptionsColumn.ReadOnly = True
                'GridView2.Columns.Item("G_NO").OptionsColumn.ReadOnly = True
                txtDepID.Enabled = False
                Per_DateEdit.Enabled = False

            Case "PD_view"
                If LoadData(labAutoID.Text) = False Then Exit Sub
                LabCaption.Text = "�p��-�d��-�C��W��"
                POPProductionPiecePersonneDay.Enabled = False
                cmdSave.Visible = False
                txtDepID.Enabled = False
        End Select


        'popAdd.Enabled = False

    End Sub

    Private Function CheckData() As Boolean
        CheckData = True

        If txtDepID.EditValue = "" Then
            MsgBox("�п�ܳ���")
            txtDepID.Select()
            CheckData = False
            Exit Function
        End If


        If Per_DateEdit.Text = "" Then
            MsgBox("���u�H���إ߮ɶ����ର��")
            Per_DateEdit.Focus()
            CheckData = False
            Exit Function
        End If

        If ds.Tables("ProductionPiecePersonnelDay").Rows.Count > 0 Then
        Else
            MsgBox("�Х����J�ƾ�")
            Exit Function
        End If

        Dim j As Integer

        ''�P�_�P�@���P�@�էO�������O��
        For j = 0 To ds.Tables("ProductionPiecePersonnelDay").Rows.Count - 1

            If ds.Tables("ProductionPiecePersonnelDay").Rows(j)("G_NO") Is DBNull.Value Then
                GridView2.FocusedRowHandle = j '���B�ܿ��~�X��
                MsgBox("�էO���ର��")
                CheckData = False
                Exit Function
            End If

            If ds.Tables("ProductionPiecePersonnelDay").Rows(j)("Per_NO") Is DBNull.Value Then
                GridView2.FocusedRowHandle = j '���B�ܿ��~�X��
                MsgBox("���u�s�����ର��")
                CheckData = False
                Exit Function
            End If

            If ds.Tables("ProductionPiecePersonnelDay").Rows(j)("Per_Name") Is DBNull.Value Then
                GridView2.FocusedRowHandle = j '���B�ܿ��~�X��
                MsgBox("���u�m�W���ର��")
                CheckData = False
                Exit Function
            End If

            

            If ds.Tables("ProductionPiecePersonnelDay").Rows(j)("AutoID") Is DBNull.Value Then  ''�YAutoID �s�b
                ''�P�_�@�U��Ѫ��W��-�P�@��
                Dim StrG_NO As String
                Dim StrPer_NO As String

                StrG_NO = ds.Tables("ProductionPiecePersonnelDay").Rows(j)("G_NO")
                StrPer_NO = ds.Tables("ProductionPiecePersonnelDay").Rows(j)("Per_NO")

                Dim plist As List(Of ProductionPiecePersonnelDayInfo)
                Dim pc As New ProductionPiecePersonnelDayControl
                plist = pc.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, StrPer_NO, Nothing, StrG_NO, txtDepID.EditValue, labFacID.Text, Nothing, Per_DateEdit.Text, Nothing, "False", Per_DateEdit.EditValue, Nothing)

                If plist.Count > 0 Then
                    GridView2.FocusedRowHandle = j '���B�ܿ��~�X��
                    MsgBox("���u�s����:" & StrPer_NO & "�w�[�J�A���ˬd.")
                    CheckData = False
                    Exit Function
                End If
            End If
        Next
        ''----------------------------------------------------------------------------------

        Dim i1, j1 As Integer

        For j1 = 0 To ds.Tables("ProductionPiecePersonnelDay").Rows.Count - 1
            For i1 = j1 + 1 To ds.Tables("ProductionPiecePersonnelDay").Rows.Count - 1
                If ds.Tables("ProductionPiecePersonnelDay").Rows(i1)("Per_NO") = ds.Tables("ProductionPiecePersonnelDay").Rows(j1)("Per_NO") And ds.Tables("ProductionPiecePersonnelDay").Rows(i1)("G_NO") = ds.Tables("ProductionPiecePersonnelDay").Rows(j1)("G_NO") Then
                    GridView2.FocusedRowHandle = j1 '���B�ܿ��~�X��
                    MsgBox("���u�s����:" & ds.Tables("ProductionPiecePersonnelDay").Rows(i1)("Per_NO") & "�w�[�J�A���ˬd.")
                    CheckData = False
                    Exit Function
                End If
            Next
        Next

    End Function

    Function Get_ProductionPiecePersonnelDayNO() As String

        Get_ProductionPiecePersonnelDayNO = ""

        Dim Str1, Str2, Stra As String
        Dim gc1 As New ProductionPiecePersonnelDayControl
        Dim gi1 As New ProductionPiecePersonnelDayInfo

        str1 = Format(Now, "yyMMdd")
        str2 = txtDepID.EditValue

        stra = Trim(str1 + str2)

        gi1 = gc1.ProductionPiecePersonnelDay_GetNO(Stra) '' Ū�����

        If gi1 Is Nothing Then
            Get_ProductionPiecePersonnelDayNO = Stra & "0001"
        Else
            Get_ProductionPiecePersonnelDayNO = Stra & Mid((CInt(Microsoft.VisualBasic.Right(gi1.Per_Num, 4)) + 10001), 2)
        End If
    End Function

    ''' <summary>
    ''' �O�s�w�n�ɤJ���ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveNew()

        Dim i As Integer

        Dim pi As New ProductionPiecePersonnelDayInfo
        Dim pc As New ProductionPiecePersonnelDayControl

        pi.Per_Date = Per_DateEdit.Text
        pi.Per_Action = InUserID

        pi.DepID = txtDepID.EditValue
        pi.FacID = labFacID.Text

        TextNum.Text = Get_ProductionPiecePersonnelDayNO()

        If TextNum.Text = "" Then
            MsgBox("����y��������,���ˬd")
            Exit Sub
        End If

        pi.Per_Num = TextNum.Text.Trim

        For i = 0 To ds.Tables("ProductionPiecePersonnelDay").Rows.Count - 1

            pi.Per_NO = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_NO")
            pi.Per_Name = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_Name")
            pi.G_NO = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("G_NO")

            If ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_PayType") Is DBNull.Value Then
                pi.Per_PayType = Nothing
            Else
                pi.Per_PayType = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_PayType")
            End If

            pi.Per_DayPrice = CDbl(ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_DayPrice"))

            If ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_Remark") Is DBNull.Value Then
                pi.Per_Remark = Nothing
            Else
                pi.Per_Remark = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_Remark")
            End If

            pi.Per_Class = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_Class")

            If pc.ProductionPiecePersonnelDay_Add(pi) = False Then
                MsgBox("�ƾګO�s����")
                Exit Sub
            End If
        Next

        MsgBox("�ƾګO�s���\")
        Me.Close()

    End Sub
    ''' <summary>
    ''' �ƾڭק�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveEdit()

        Dim i As Integer
        Dim pi As New ProductionPiecePersonnelDayInfo
        Dim pc As New ProductionPiecePersonnelDayControl

        pi.Per_Date = Per_DateEdit.Text
        pi.Per_Action = InUserID

        pi.DepID = txtDepID.EditValue
        pi.FacID = labFacID.Text

        If TextNum.Text = "" Then
            MsgBox("����y��������,���ˬd")
            Exit Sub
        End If

        pi.Per_Num = TextNum.Text.Trim

        For i = 0 To ds.Tables("ProductionPiecePersonnelDay").Rows.Count - 1

            pi.Per_NO = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_NO")
            pi.Per_Name = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_Name")
            pi.G_NO = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("G_NO")

            If ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_PayType") Is DBNull.Value Then
                pi.Per_PayType = Nothing
            Else
                pi.Per_PayType = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_PayType")
            End If

            pi.Per_DayPrice = CDbl(ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_DayPrice"))

            If ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_Remark") Is DBNull.Value Then
                pi.Per_Remark = Nothing
            Else
                pi.Per_Remark = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_Remark")
            End If

            pi.Per_Class = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("Per_Class")

            If ds.Tables("ProductionPiecePersonnelDay").Rows(i)("AutoID") Is DBNull.Value Then
                If pc.ProductionPiecePersonnelDay_Add(pi) = False Then
                    MsgBox("�ƾګO�s����")
                    Exit Sub
                End If
            Else
                pi.AutoID = ds.Tables("ProductionPiecePersonnelDay").Rows(i)("AutoID")

                If pc.ProductionPiecePersonnelDay_Update(pi) = False Then
                    MsgBox("�ƾګO�s����")
                    Exit Sub
                End If
            End If

        Next


        Dim pcd As New ProductionPiecePersonnelDayControl

        Dim j As Integer
        If ds.Tables("ProductionPiecePersonnelDayDelete").Rows.Count > 0 Then 'AutoID
            For j = 0 To ds.Tables("ProductionPiecePersonnelDayDelete").Rows.Count - 1
                If ds.Tables("ProductionPiecePersonnelDayDelete").Rows(j)("AutoID") Is DBNull.Value Then
                Else
                    pcd.ProductionPiecePersonnelDay_Delete(ds.Tables("ProductionPiecePersonnelDayDelete").Rows(j)("AutoID"))
                End If
            Next
        End If


        MsgBox("�ƾګO�s���\")
        Me.Close()
    End Sub


    Function LoadData(ByVal Per_Num As String) As Boolean
        Dim i As Integer

        Dim objInfo As New ProductionPiecePersonnelDayInfo
        Dim objList As New List(Of ProductionPiecePersonnelDayInfo)
        Dim oc As New ProductionPiecePersonnelDayControl
        objList = oc.ProductionPiecePersonnelDay_GetList(Nothing, Per_Num, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        LoadData = False

        If objList.Count > 0 Then
        Else
            MsgBox("�S���ƾ�.")
            Exit Function
        End If

        Per_DateEdit.Text = objList(0).Per_Date
        txtDepID.EditValue = objList(0).DepID
        labFacID.Text = objList(0).FacID

        TextNum.Text = objList(0).Per_Num

        For i = 0 To objList.Count - 1

            Dim row1 As DataRow = ds.Tables("ProductionPiecePersonnelDay").NewRow
            row1("AutoID") = objList(i).AutoID

            row1("Per_NO") = objList(i).Per_NO
            row1("Per_Name") = objList(i).Per_Name
            row1("Per_G_Name") = objList(i).Per_G_Name
            row1("G_NO") = objList(i).G_NO

            row1("Per_PayType") = objList(i).Per_PayType
            row1("Per_DayPrice") = objList(i).Per_DayPrice
            row1("Per_Remark") = objList(i).Per_Remark
            row1("Per_Class") = objList(i).Per_Class

            ds.Tables("ProductionPiecePersonnelDay").Rows.Add(row1)

            LoadData = True
        Next


    End Function



    Private Sub AddRowD()
        Dim i, n, j As Integer
        Dim arr(n) As String

        arr = Split(tempValue2, ",")
        n = Len(Replace(tempValue2, ",", "," & "*")) - Len(tempValue2)

        If arr(i) = "" Then
            Exit Sub
        End If

        For i = 0 To n   ''�̦Z�@��NEXT
            Dim objList As New List(Of ProductionPiecePersonnelDayInfo)
            Dim oc As New ProductionPiecePersonnelDayControl

            objList = oc.ProductionPiecePersonnelDay_GetList(arr(i), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            ' ds.Tables("ProductionPiecePersonnelDay").Clear()

            If objList.Count <= 0 Then
                MsgBox("�S���ƾڡI")
                Exit Sub
            Else

                Dim row1 As DataRow = ds.Tables("ProductionPiecePersonnelDay").NewRow

                'row1("AutoID") = objList(0).AutoID  ''���s�JProductionPiecePersonnelDay �A�u���ק�μW�[�ϥ�

                row1("Per_NO") = objList(0).Per_NO
                row1("Per_Name") = objList(0).Per_Name

                row1("Per_G_Name") = objList(0).Per_G_Name
                row1("G_NO") = objList(0).G_NO

                For j = 0 To ds.Tables("ProductionPiecePersonnelDay").Rows.Count - 1
                    If objList(0).Per_NO = ds.Tables("ProductionPiecePersonnelDay").Rows(j)("Per_NO") And row1("G_NO") = ds.Tables("ProductionPiecePersonnelDay").Rows(j)("G_NO") Then
                        MsgBox("���u�s����:" & objList(0).Per_NO & "  �w�[�J " & objList(0).Per_G_Name & ".���ˬd.")
                        GoTo AAA
                    End If
                Next

                row1("Per_PayType") = objList(0).Per_PayType
                row1("Per_DayPrice") = objList(0).Per_DayPrice

                row1("Per_Remark") = ""
                row1("Per_Class") = objList(0).Per_Class
                ds.Tables("ProductionPiecePersonnelDay").Rows.Add(row1)

AAA:
                GridView2.MoveLast()
            End If

        Next

    End Sub


    Private Sub AddRowB()

        Dim i, n, j As Integer
        Dim arr(n) As String

        arr = Split(tempValue2, ",")
        n = Len(Replace(tempValue2, ",", "," & "*")) - Len(tempValue2)

        If arr(i) = "" Then
            Exit Sub
        End If

        For i = 0 To n   ''�̦Z�@��NEXT
            Dim objList As New List(Of ProductionPiecePersonnelInfo)
            Dim oc As New ProductionPiecePersonnelControl

            objList = oc.ProductionPiecePersonnel_GetList(arr(i), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            'ds.Tables("ProductionPiecePersonnelDay").Clear()

            If objList.Count <= 0 Then
                MsgBox("�S���ƾڡI")
                Exit Sub
            Else

                Dim row1 As DataRow = ds.Tables("ProductionPiecePersonnelDay").NewRow

                'row1("AutoID") = objList(0).AutoID  ''���s�JProductionPiecePersonnelDay �A�u���ק�μW�[�ϥ�

                row1("Per_NO") = objList(0).Per_NO
                row1("Per_Name") = objList(0).Per_Name

                row1("Per_G_Name") = objList(0).Per_G_Name
                row1("G_NO") = objList(0).G_NO

                '  MsgBox(ds.Tables("ProductionPiecePersonnelDay").Rows.Count)

                For j = 0 To ds.Tables("ProductionPiecePersonnelDay").Rows.Count - 1
                    If objList(0).Per_NO = ds.Tables("ProductionPiecePersonnelDay").Rows(j)("Per_NO") And row1("G_NO") = ds.Tables("ProductionPiecePersonnelDay").Rows(j)("G_NO") Then
                        MsgBox("���u�s����:" & objList(0).Per_NO & "  �w�[�J " & objList(0).Per_G_Name & ".���ˬd.")
                        GoTo AAA
                    End If
                Next

                row1("Per_PayType") = objList(0).Per_PayType
                row1("Per_DayPrice") = objList(0).Per_DayPrice
                row1("Per_Remark") = ""
                row1("Per_Class") = objList(0).Per_Class
                ds.Tables("ProductionPiecePersonnelDay").Rows.Add(row1)

AAA:
                GridView2.MoveLast()
            End If

        Next
    End Sub


    Private Sub popLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popLoad.Click

        tempValue = Nothing
        tempValue2 = Nothing

        If txtDepID.EditValue <> "" Then
        Else
            MsgBox("�п�ܳ����W��")
            Exit Sub
        End If

        tempValue3 = txtDepID.EditValue
        tempValue4 = labFacID.Text
        ProductionPiecePersonnelDaylLoad.xteatab.TabIndex = 0

        ProductionPiecePersonnelDaylLoad.ShowDialog()

        '�W�[�O��
        If tempValue = "" Then
            Exit Sub
        Else
            If tempValue = "B" Then
                AddRowB()
            Else
                AddRowD()
            End If
            tempValue2 = Nothing
            tempValue = Nothing
        End If
    End Sub

    Private Sub txtDepID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDepID.EditValueChanged
        If txtDepID.EditValue <> "" Then
            Dim pc1 As New ProductionPieceWorkGroupControl
            Dim piList As New List(Of ProductionPieceWorkGroupInfo)

            piList = pc1.DepFac_GetList1(txtDepID.EditValue, Nothing, Nothing, Nothing)

            If piList.Count > 0 Then
            Else
                Exit Sub
            End If

            labFacID.Text = piList(0).FacID

            ''���J�էO�H��
            ' RepositoryItemLookUpEdit2.DataSource = pc.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, txtDepID.EditValue, labFacID.Text, Nothing, Nothing, Nothing)
            Dim pc As New ProductionPieceWorkGroupControl
            Dim pcL As List(Of ProductionPieceWorkGroupInfo)

            ds.Tables("G_NOName").Clear()

            Dim row1 As DataRow
            row1 = ds.Tables("G_NOName").NewRow
            row1("G_NO") = "�L"
            row1("G_Name") = "�L"
            ds.Tables("G_NOName").Rows.Add(row1)
            pcL = pc.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, txtDepID.EditValue, labFacID.Text, Nothing, Nothing, Nothing)
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


        End If
    End Sub

    Private Sub popAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popAdd.Click
        If txtDepID.EditValue <> "" Then
        Else
            MsgBox("�п�ܳ����W��")
            Exit Sub
        End If

        Dim row1 As DataRow = ds.Tables("ProductionPiecePersonnelDay").NewRow


        row1("Per_PayType") = "�p��"
        row1("Per_DayPrice") = 0
        row1("Per_Remark") = ""
        row1("Per_Class") = RepositoryItemComboBox2.Items.Item(0).ToString

        ds.Tables("ProductionPiecePersonnelDay").Rows.Add(row1)

        GridView2.MoveLast()
    End Sub

    Private Sub popDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popDel.Click
        If GridView2.RowCount = 0 Then Exit Sub

        Dim DelTemp As String
        DelTemp = GridView2.GetRowCellDisplayText(ArrayToString(GridView2.GetSelectedRows()), "AutoID")

        If DelTemp = "AutoID" Then
        Else
            '�b�R�����W�[�Q�R�����O��
            Dim row As DataRow = ds.Tables("ProductionPiecePersonnelDayDelete").NewRow
            row("AutoID") = DelTemp

            ds.Tables("ProductionPiecePersonnelDayDelete").Rows.Add(row)
            ds.Tables("ProductionPiecePersonnelDay").Rows.RemoveAt(CInt(ArrayToString(GridView2.GetSelectedRows())))

        End If

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case CaoTypeLabel.Text

            Case "PD_add"
                If CheckData() = False Then Exit Sub
                SaveNew()

            Case "PD_edit"
                If CheckData() = False Then Exit Sub
                SaveEdit()
        End Select

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
End Class