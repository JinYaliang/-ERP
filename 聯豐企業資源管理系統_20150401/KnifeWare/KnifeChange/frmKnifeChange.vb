Imports LFERP.Library.KnifeWare
Public Class frmKnifeChange
    Dim ds As New DataSet
    Dim strType As String

    Private Sub frmKnifeChange_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtCH_Num.Text = tempValue2
        strType = tempValue

        tempValue2 = Nothing
        tempValue = Nothing

        Dim cc As New KnifeChangeControl
        GridLookChangeType.Properties.ValueMember = "ChangeID"
        GridLookChangeType.Properties.DisplayMember = "ChangeName"
        GridLookChangeType.Properties.DataSource = cc.KnifeChangeType_GetList(Nothing, Nothing)

        GridLookChangeType.Enabled = False
        GridLookChangeType.EditValue = strType
        DateC_Date.EditValue = Format(Now, "yyyy/MM/dd HH:mm:ss")

        CreateTables()

        Select Case strType
            Case "WT1", "WT2", "KT3", "KT4", "KT5"
                txtCH_Num.Text = ""
                GridLookChangeType.Enabled = False
                GridLookChangeType.EditValue = strType

                Me.Text = "����" & "[" & GridLookChangeType.Text & "]"
                LabMsg.Text = "����" & "-" & GridLookChangeType.Text

                DateC_Date.EditValue = Format(Now, "yyyy/MM/dd HH:mm:ss")

                If strType = "WT1" Or strType = "WT2" Then
                    XtraTabControl1.TabPages(0).PageVisible = False
                    XtraTabControl1.TabPages(1).PageVisible = False
                    XtraTabControl1.TabPages(2).PageVisible = True
                    XtraTabControl1.TabPages(3).PageVisible = False
                    B_Num.Visible = False
                ElseIf strType = "KT3" Then '���u��M
                    XtraTabControl1.TabPages(0).PageVisible = True
                    XtraTabControl1.TabPages(1).PageVisible = False
                    XtraTabControl1.TabPages(2).PageVisible = False
                    XtraTabControl1.TabPages(3).PageVisible = False
                    B_Num.Visible = True
                ElseIf strType = "KT4" Then '���u�٤M
                    XtraTabControl1.TabPages(0).PageVisible = True
                    XtraTabControl1.TabPages(1).PageVisible = True
                    XtraTabControl1.TabPages(2).PageVisible = False
                    XtraTabControl1.TabPages(3).PageVisible = False
                ElseIf strType = "KT5" Then '����W�[
                    XtraTabControl1.TabPages(0).PageVisible = False
                    XtraTabControl1.TabPages(1).PageVisible = False
                    XtraTabControl1.TabPages(2).PageVisible = False
                    XtraTabControl1.TabPages(3).PageVisible = True
                    B_Num.Visible = False
                End If

            Case "View"

                Me.Text = "����-�d��"
                LabMsg.Text = "����-�d��"

                cmdSave.Visible = False
                LoadData(txtCH_Num.Text)
                ''����J��������
                If GridLookChangeType.EditValue = "WT1" Or GridLookChangeType.EditValue = "WT2" Or GridLookChangeType.EditValue = "KT5" Then
                    B_Num.Visible = False
                Else
                    B_Num.Visible = True
                End If
        End Select

        popWareOutAdd.Visible = False
        ToolStripBorrowReturn.Visible = False
        '------------------------------------------
        txtWH.Select()
        ButtonLoadBnum2.Left = ButtonLoadBnum1.Left
        ButtonLoadBnum2.Top = ButtonLoadBnum1.Top

        ButtonM_CodeA.Left = ButtonLoadBnum1.Left
        ButtonM_CodeA.Top = ButtonLoadBnum1.Top
        '------------------------------------------


        ' Me.TextBrrow.Text = "KF000000001"

    End Sub
    ''' <summary>
    ''' �ت��c
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("KnifeChange")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("B_Num", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("CBegin_Qty", GetType(Int32))
            .Columns.Add("CEnd_Qty", GetType(Int32))
            .Columns.Add("CReMark", GetType(String))

            .Columns.Add("BQty", GetType(Int32)) '���u��M��
            .Columns.Add("RQty", GetType(Int32)) '���u�٤M�� 
            .Columns.Add("NOReturn", GetType(Int32)) '���ټ�

        End With

        Grid1.DataSource = ds.Tables("KnifeChange")

    End Sub

    ''' <summary>
    ''' �P�_�ƾ�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckData() As Boolean
        CheckData = True

        If txtWH.Text = "" Then
            MsgBox("�ܮw�s�����ର��!", 64, "����")
            CheckData = False
            Exit Function
        End If

        If ds.Tables("KnifeChange").Rows.Count <= 0 Then
            MsgBox("�L�O���O�s!", 64, "����")
            CheckData = False
            Exit Function
        End If

        Dim i As Integer
        For i = 0 To ds.Tables("KnifeChange").Rows.Count - 1

            If ds.Tables("KnifeChange").Rows(i)("M_Code").ToString.Trim = "" Then
                MsgBox("���ƽs�X���ର��", 64, "����")
                CheckData = False
                Exit Function
            End If
            If ds.Tables("KnifeChange").Rows(i)("M_Name").ToString.Trim = "" Then
                MsgBox("���ƦW�٤��ର��", 64, "����")
                CheckData = False
                Exit Function
            End If
            If ds.Tables("KnifeChange").Rows(i)("M_Gauge").ToString.Trim = "" Then
                MsgBox("���ƳW�椣�ର��", 64, "����")
                CheckData = False
                Exit Function
            End If

            '----------------------
            If ds.Tables("KnifeChange").Rows(i)("CBegin_Qty") = ds.Tables("KnifeChange").Rows(i)("CEnd_Qty") Then
                CheckData = False
                MsgBox("���e�ƶq�P���Z�ƶq�ۦP,���ˬd!", 64, "����")
                Exit Function
            End If


            Dim M_Code1 As String
            Dim NowWareInvent As Double 'Ū���O�s�ɪ��̦Z�w�s
            M_Code1 = ds.Tables("KnifeChange").Rows(i)("M_Code").ToString
            '----------------------
            ' If strType = "WT1" Or strType = "WT2" Then
            Select Case strType
                Case "WT1", "WT2"
                    Dim wi As LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
                    Dim wc As New LFERP.Library.KnifeWare.KnifeWareInventorySubControl
                    wi = wc.KnifeWareInventorySub_GetList(M_Code1, txtWH.Tag)
                    If wi Is Nothing Then
                        NowWareInvent = 0
                    Else
                        If strType = "WT1" Then  '�ݳB�z
                            NowWareInvent = wi.WI_SReQty
                        ElseIf strType = "WT2" Then '�s�M
                            NowWareInvent = wi.WI_SQty
                        End If
                    End If

                    If NowWareInvent = ds.Tables("KnifeChange").Rows(i)("CBegin_Qty") Then
                    Else
                        CheckData = False
                        MsgBox("��e���ƽs�X�w�s�w�ܧ�,���ˬd!", 64, "����")
                        Exit Function
                    End If

                Case "KT3" '���u��M
                    'B_Num
                    Dim strB_Num As String
                    strB_Num = ds.Tables("KnifeChange").Rows(i)("B_Num").ToString

                    Dim bl As New List(Of KnifeBorrowInfo)
                    Dim bc As New KnifeBorrowControl
                    bl = bc.KnifeBorrow_GetList(strB_Num, Nothing, Nothing, txtWH.Tag, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                    If bl.Count = 1 Then
                    Else
                        CheckData = False
                        MsgBox(strB_Num & "��M��w���s�b,���ˬd�I", 64, "����")
                        Exit Function
                    End If

                    If ds.Tables("KnifeChange").Rows(i)("CBegin_Qty") <> bl(0).B_Qty Then
                        CheckData = False
                        MsgBox("��M�Ʀ��ܧ�,���ˬd!  �̷s��M�Ƭ�" & bl(0).B_Qty, 64, "����")
                        Exit Function
                    End If

                    If ds.Tables("KnifeChange").Rows(i)("CEnd_Qty") < bl(0).R_Qty Then
                        CheckData = False
                        MsgBox("�վ�Z�ƶq����p��w�٤M��!" & "��e�٤M�Ƭ�:" & bl(0).R_Qty, 64, "����")
                        Exit Function
                    End If

                    Dim wi As LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
                    Dim wc As New LFERP.Library.KnifeWare.KnifeWareInventorySubControl
                    wi = wc.KnifeWareInventorySub_GetList(M_Code1, txtWH.Tag)

                    If wi.WI_SQty - (ds.Tables("KnifeChange").Rows(i)("CEnd_Qty") - ds.Tables("KnifeChange").Rows(i)("CBegin_Qty")) < 0 Then
                        CheckData = False
                        MsgBox("��e�w�s����������e�t����!" & "��e�w�s�Ƭ�:" & wi.WI_SQty, 64, "����")
                        Exit Function
                    End If

                Case "KT4" '���u�٤M
                    'B_Num
                    Dim strB_Num As String
                    strB_Num = ds.Tables("KnifeChange").Rows(i)("B_Num").ToString

                    Dim bl As New List(Of KnifeBorrowInfo)
                    Dim bc As New KnifeBorrowControl
                    bl = bc.KnifeBorrow_GetList(strB_Num, Nothing, Nothing, txtWH.Tag, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                    If bl.Count = 1 Then
                    Else
                        CheckData = False
                        MsgBox(strB_Num & "��M��w���s�b,���ˬd�I", 64, "����")
                        Exit Function
                    End If

                    If ds.Tables("KnifeChange").Rows(i)("CBegin_Qty") <> bl(0).R_Qty Then
                        CheckData = False
                        MsgBox("�٤M�Ʀ��ܧ�,���ˬd!  �̷s�٤M�Ƭ�" & bl(0).R_Qty, 64, "����")
                        Exit Function
                    End If

                    If ds.Tables("KnifeChange").Rows(i)("CEnd_Qty") > bl(0).B_Qty Then
                        CheckData = False
                        MsgBox("�վ�Z�ƶq����j��,�w��M��!", 64, "����")
                        Exit Function
                    End If

                    Dim wi As LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
                    Dim wc As New LFERP.Library.KnifeWare.KnifeWareInventorySubControl
                    wi = wc.KnifeWareInventorySub_GetList(M_Code1, txtWH.Tag)

                    If (ds.Tables("KnifeChange").Rows(i)("CEnd_Qty") - ds.Tables("KnifeChange").Rows(i)("CBegin_Qty")) + wi.WI_SQty < 0 Then
                        CheckData = False
                        MsgBox("��e�w�s����������e�t����!" & "��e�w�s�Ƭ�:" & wi.WI_SQty, 64, "����")
                        Exit Function
                    End If

                Case "KT5" '����W�[
            End Select
        Next

    End Function


    Private Sub txtWH_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWH.ButtonClick
        If ds.Tables("KnifeChange").Rows.Count > 0 Then
            MsgBox("������ܮw,��e�O�s�ƾڤ��w�s�b�O��!", 64, "����")
            Exit Sub
        End If

        frmWareHouseSelect.SelectWareID = ""
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = Me.Left + txtWH.Left + 2
        frmWareHouseSelect.Top = Me.Top + txtWH.Top + txtWH.Height + 21 + GroupBox1.Height
        frmWareHouseSelect.SelectWareID = ""
        tempValue3 = "510706"
        frmWareHouseSelect.ShowDialog()
        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            txtWH.Tag = frmWareHouseSelect.SelectWareID
            txtWH.Text = frmWareHouseSelect.SelectWareName
        End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckData() = False Then
            Exit Sub
        End If

        If strType = "KT5" Then
            SaveDataNewBorrow()
        Else
            SaveData()
        End If



    End Sub
    ''' <summary>
    ''' ����y��
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function KnifeChange_GetNUM() As String
        '�ͦ��spm
        Dim pm As New KnifeChangeControl
        Dim pi As New KnifeChangeInfo
        Dim ndate As String
        ndate = "KV" + Format(Now(), "yyMM")
        pi = pm.KnifeChange_GetNUM(ndate)
        If pi Is Nothing Then
            KnifeChange_GetNUM = ndate + "00001"
        Else
            KnifeChange_GetNUM = ndate + Mid((CInt(Mid(pi.CH_Num, 7)) + 100001), 2)
        End If
    End Function
    Function SaveData() As String
        SaveData = ""

        txtCH_Num.Text = KnifeChange_GetNUM()
        If Len(txtCH_Num.Text) <= 0 Then
            MsgBox("����y��������!")
            Exit Function
        End If
        '---------------------------------------
        Dim k As Integer
        For k = 0 To ds.Tables("KnifeChange").Rows.Count - 1
            ''�W�[�@�����O��
            Dim cc As New KnifeChangeControl
            Dim ci As New KnifeChangeInfo

            ci.CH_Num = txtCH_Num.Text

            ci.M_Code = ds.Tables("KnifeChange").Rows(k)("M_Code").ToString
            ci.WH_ID = txtWH.Tag

            ci.CBegin_Qty = ds.Tables("KnifeChange").Rows(k)("CBegin_Qty")
            ci.CEnd_Qty = ds.Tables("KnifeChange").Rows(k)("CEnd_Qty")
            ci.CKType = GridLookChangeType.EditValue

            ci.CReMark = ds.Tables("KnifeChange").Rows(k)("CReMark").ToString
            ci.C_Action = InUserID
            ci.C_Date = Format(CDate(DateC_Date.EditValue), "yyyy/MM/dd HH:mm:ss")
            ci.CReMark = "�Q�ʽվ�--" & ds.Tables("KnifeChange").Rows(k)("CReMark")


            If IsDBNull(ds.Tables("KnifeChange").Rows(k)("B_Num")) = True Then
            Else
                ci.BR_NO = ds.Tables("KnifeChange").Rows(k)("B_Num").ToString
            End If

            If cc.KnifeChange_Add(ci) = True Then

                Select Case strType
                    Case "WT1", "WT2"  '�վ�w�s
                        If UpdateQty(ds.Tables("KnifeChange").Rows(k)("M_Code").ToString, ds.Tables("KnifeChange").Rows(k)("CEnd_Qty"), strType) = True Then
                        Else
                            MsgBox(ds.Tables("KnifeChange").Rows(k)("M_Code").ToString & "�w�s�վ㥢��!", 64, "����")
                            Exit Function
                        End If
                    Case "KT3", "KT4" '���u��M��  '���u�٤M
                        ''�����u��M�椤����M�� 
                        ''�A��s�w�s
                        Dim strB_Num As String
                        strB_Num = ds.Tables("KnifeChange").Rows(k)("B_Num").ToString

                        Dim bl As New List(Of KnifeBorrowInfo)
                        Dim bc As New KnifeBorrowControl
                        bl = bc.KnifeBorrow_GetList(strB_Num, Nothing, Nothing, txtWH.Tag, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                        ''----------------------------------------------------------------------------------
                        Dim BCC As New KnifeBorrowControl
                        Dim BII As New KnifeBorrowInfo
                        BII.B_Num = strB_Num
                        If strType = "KT3" Then
                            BII.R_Qty = bl(0).R_Qty
                            BII.B_Qty = ds.Tables("KnifeChange").Rows(k)("CEnd_Qty")
                            BII.NOReturn = ds.Tables("KnifeChange").Rows(k)("CEnd_Qty") - bl(0).R_Qty
                        ElseIf strType = "KT4" Then
                            BII.R_Qty = ds.Tables("KnifeChange").Rows(k)("CEnd_Qty")
                            BII.B_Qty = bl(0).B_Qty
                            BII.NOReturn = bl(0).B_Qty - ds.Tables("KnifeChange").Rows(k)("CEnd_Qty")
                        End If

                        If BCC.KnifeBorrow_UpdateChangeBRQty(BII) = True Then
                            '�����w�s
                            'Dim WareCha As Int32
                            'WareCha = ds.Tables("KnifeChange").Rows(k)("CEnd_Qty") - ds.Tables("KnifeChange").Rows(k)("CBegin_Qty")

                            'If UpdateQty(ds.Tables("KnifeChange").Rows(k)("M_Code").ToString, WareCha, strType) = True Then
                            'Else
                            '    MsgBox(strB_Num & "�w�s�վ㥢��!")
                            '    Exit Function
                            'End If
                        End If

                    Case "KT5" '����W�[
                        '�n�P�_����ܮw
                        '��M�W��
                End Select
            End If
        Next
        MsgBox("�O�s���\!", 64, "����")
        Me.Close()



    End Function
    ''' <summary>
    ''' ��s�w�s
    ''' </summary>
    ''' <param name="_M_code"></param>
    ''' <param name="_CEnd_Qty"></param>
    ''' <param name="_strType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateQty(ByVal _M_code As String, ByVal _CEnd_Qty As Int32, ByVal _strType As String) As Boolean

        UpdateQty = True

        Dim dblWI_All As Double
        Dim dbWI_SReQty As Double
        Dim dblWI_SQty As Double

        Dim wi As New LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
        Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
        wi = wc.WareInventory_GetSub(_M_code, txtWH.Tag)

        If wi Is Nothing Then
            dblWI_All = 0
        Else
            dblWI_All = wi.WI_Qty
        End If
        '---------------------�w�s�l��------------------------------------------------------------
        Dim wiinfo As New LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
        Dim wcco As New LFERP.Library.KnifeWare.KnifeWareInventorySubControl

        wiinfo = wcco.KnifeWareInventorySub_GetList(_M_code, txtWH.Tag)
        If wiinfo Is Nothing Then
            dbWI_SReQty = 0
            dblWI_SQty = 0
        Else
            dblWI_SQty = wiinfo.WI_SQty
            dbWI_SReQty = wiinfo.WI_SReQty
        End If
        '---------------------'---------------------'---------------------'---------------------
        Dim wifo As New LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
        Select Case _strType
            Case "WT2" ''''�s�M  '������אּ�h�ּƶq
                wifo.WI_SQty = _CEnd_Qty
                wifo.WI_SReQty = dbWI_SReQty
                wifo.WI_All = dblWI_All + (_CEnd_Qty - dblWI_SQty)
            Case "WT1" '�ݳB�z
                wifo.WI_SQty = dblWI_SQty
                wifo.WI_SReQty = _CEnd_Qty
                wifo.WI_All = dblWI_All + (_CEnd_Qty - dbWI_SReQty)
            Case "KT3" ''''�s�M     ��h�ּ�
                wifo.WI_SQty = dblWI_SQty - _CEnd_Qty
                wifo.WI_SReQty = dbWI_SReQty
                wifo.WI_All = dblWI_All - _CEnd_Qty
            Case "KT4" '�٤M
                wifo.WI_SQty = dblWI_SQty + _CEnd_Qty
                wifo.WI_SReQty = dbWI_SReQty
                wifo.WI_All = dblWI_All + _CEnd_Qty
        End Select


        wifo.M_Code = _M_code
        wifo.WH_ID = txtWH.Tag
        If wcco.KnifeWareInventorySub_Update(wifo) = False Then
            UpdateQty = False
        End If

    End Function


    ''' <summary>
    ''' ���J�ƾ�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadData(ByVal CH_NumText As String) As Boolean
        Dim cC As New KnifeChangeControl
        Dim cL As New List(Of KnifeChangeInfo)
        cL = cC.KnifeChange_GetList(CH_NumText, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If cL.Count <= 0 Then
            MsgBox("�L�ƾګO�s!", 64, "����")
            Exit Function
        End If

        txtWH.Tag = cL(0).WH_ID
        txtWH.Text = cL(0).WH_Name
        GridLookChangeType.EditValue = cL(0).CKType
        DateC_Date.EditValue = cL(0).C_Date
        '-------------------------------------------------------------------
        Dim K As Integer
        For K = 0 To cL.Count - 1
            Dim row As DataRow
            row = ds.Tables("KnifeChange").NewRow

            row("AutoID") = cL(K).AutoID
            row("M_Code") = cL(K).M_Code
            row("M_Name") = cL(K).M_Name
            row("M_Gauge") = cL(K).M_Gauge

            row("CBegin_Qty") = cL(K).CBegin_Qty
            row("CEnd_Qty") = cL(K).CEnd_Qty
            row("CReMark") = cL(K).CReMark

            row("B_Num") = cL(K).BR_NO
            ds.Tables("KnifeChange").Rows.Add(row)
        Next

        '------------------------------------------------------------------
    End Function

    Private Sub popWareOutDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutDel.Click
        If ds.Tables("KnifeChange").Rows.Count = 0 Then Exit Sub

        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "AutoID")
        ds.Tables("KnifeChange").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))

    End Sub

    Private Sub ToolStripBorrowReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripBorrowReturn.Click

        If txtWH.Text = "" Then
            txtWH.Select()
            MsgBox("�п�ܭܮw�s��!", 64, "����")
            Exit Sub
        End If
        tempValue2 = GridLookChangeType.EditValue
        tempValue = txtWH.Tag
        frmKnifeChangeLoad.ShowDialog()
        frmKnifeChangeLoad.Dispose()

    End Sub


#Region "�˦�1"
    Private Sub ButtonLoadBnum1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLoadBnum1.Click
        '��M�y����------------------

        If txtWH.Text = "" Then
            txtWH.Select()
            MsgBox("�п�ܭܮw�s��!", 64, "����")
            Exit Sub
        End If

        If TextBrrow.Text = "" Then
            TextBrrow.Select()
            MsgBox("��M�椣�ର��!", 64, "����")
            Exit Sub
        End If
        AddKnifeNO(TextBrrow.Text, txtWH.Tag)
    End Sub

    Sub AddKnifeNO(ByVal TextBrrow As String, ByVal txtWHTag As String)
        ' ds.Tables("KnifeChangeLoad").Clear()

        Dim cb As New List(Of KnifeBorrowInfo)
        Dim cc As New KnifeBorrowControl
        cb = cc.KnifeBorrow_GetList(TextBrrow, Nothing, Nothing, txtWHTag, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If cb.Count = 1 Then
        Else
            MsgBox("����M���J���~!", 64, "����")
            Exit Sub
        End If
        ''-------------------------------------------------------------------------------

        Dim j As Integer
        For j = 0 To ds.Tables("KnifeChange").Rows.Count - 1
            If TextBrrow = ds.Tables("KnifeChange").Rows(j)("B_Num").ToString Then
                MsgBox("�P�@�i���椤�����\�s�b�ۦP����M��I", 64, "����")
                GridView1.FocusedRowHandle = j
                Exit Sub
            End If
        Next
        ''-------------------------------------------------------------------------------
        Dim row As DataRow
        row = ds.Tables("KnifeChange").NewRow

        row("B_Num") = TextBrrow
        row("M_Code") = cb(0).M_Code
        row("M_Name") = cb(0).M_Name
        row("M_Gauge") = cb(0).M_Gauge

        row("BQty") = cb(0).B_Qty '���u��M��
        row("RQty") = cb(0).R_Qty '���u�٤M�� 
        row("NOReturn") = cb(0).NOReturn '���ټ�

        '��-----------------�l--------------------------------

        If strType = "KT3" Then  '�ݳB�z
            row("CBegin_Qty") = cb(0).B_Qty
        ElseIf strType = "KT4" Then '�s�M
            row("CBegin_Qty") = cb(0).R_Qty
        End If
        row("CEnd_Qty") = 0
        ''------------------------------------------------------
        ds.Tables("KnifeChange").Rows.Add(row)

    End Sub
    '��M�y����:
    Private Sub TextBrrow_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBrrow.KeyDown
        If e.KeyCode = Keys.Enter Then
            ButtonLoadBnum1_Click(Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "�w�s���"

    Private Sub ButtonM_CodeA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonM_CodeA.Click
        '�վ�ܮw��
        If txtWH.Text = "" Then
            txtWH.Select()
            MsgBox("�п�ܭܮw�s��!", 64, "����")
            Exit Sub
        End If

        If TxtM_CodeA.Text = "" Then
            TxtM_CodeA.Select()
            MsgBox("�M��s�X����!", 64, "����")
            Exit Sub
        End If

        AddRow(TxtM_CodeA.Text, 0)
    End Sub

    Private Sub TxtM_CodeA_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtM_CodeA.KeyDown
        If e.KeyCode = Keys.Enter Then
            ButtonM_CodeA_Click(Nothing, Nothing)
        End If
    End Sub

    'Private Sub popWareOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutAdd.Click

    '    If txtWH.Text = "" Then
    '        txtWH.Select()
    '        MsgBox("�п�ܭܮw�s��!", 64, "����")
    '        Exit Sub
    '    End If


    '    tempCode = ""
    '    tempValue5 = txtWH.Tag
    '    tempValue6 = "Knife"
    '    frmBOMSelect.ShowDialog()

    '    If frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then
    '        '�W�[�O��
    '        If tempCode = "" Then
    '            Exit Sub
    '        Else
    '            AddRow(tempCode, 0)
    '        End If
    '    End If


    '    tempValue7 = ""
    '    tempValue8 = ""
    'End Sub

    Sub AddRow(ByVal strCode As String, ByVal strQty As Single)
        Dim row As DataRow
        row = ds.Tables("KnifeChange").NewRow
        If strCode = "" Then
        Else
            Dim j As Integer
            For j = 0 To ds.Tables("KnifeChange").Rows.Count - 1
                If strCode = ds.Tables("KnifeChange").Rows(j)("M_Code") Then
                    MsgBox("���Ƥw�s�b,�P�@�i���椤�����\�s�b�ۦP���M��s�X�I", 64, "����")
                    GridView1.FocusedRowHandle = j
                    Exit Sub
                End If
            Next

            Dim wi6 As LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
            Dim wc6 As New LFERP.Library.KnifeWare.KnifeWareInventorySubControl
            wi6 = wc6.KnifeWareInventorySub_GetList(strCode, txtWH.Tag)

            If wi6 Is Nothing Then
                MsgBox("��e�ܮw�L���M��H���I", 64, "����")
                Exit Sub
            End If

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)

            row("M_Code") = objInfo.M_Code
            row("M_Name") = objInfo.M_Name
            row("M_Gauge") = objInfo.M_Gauge
            '��-----------------�l--------------------------------

            If strType = "WT1" Then  '�ݳB�z
                row("CBegin_Qty") = wi6.WI_SReQty
            ElseIf strType = "WT2" Then '�s�M
                row("CBegin_Qty") = wi6.WI_SQty
            End If
            row("CEnd_Qty") = 0
            ''------------------------------------------------------
            ds.Tables("KnifeChange").Rows.Add(row)
        End If
        GridView1.MoveLast()
    End Sub

#End Region




#Region "�˦�2"
    Private Sub ButtonLoadBnum2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLoadBnum2.Click
        If txtPerNO2.Text = "" Then
            txtPerNO2.Select()
            MsgBox("�ɤM�u�����ର��!", 64, "����")
            Exit Sub
        End If

        If txtM_Code.Text = "" Then
            txtM_Code.Select()
            MsgBox("�M��s�����ର��!", 64, "����")
            Exit Sub
        End If

        If Val(txtQty.Text) = 0 Then
            txtQty.Select()
            MsgBox("�ƶq���ର0!", 64, "����")
            Exit Sub
        End If

        If txtWH.Text = "" Then
            txtWH.Select()
            MsgBox("�п�ܭܮw�s��!", 64, "����")
            Exit Sub
        End If

        ' ds.Tables("KnifeChange").Clear()


        Dim kcc As New KnifeReturnControl
        Dim kll As New List(Of KnifeReturnInfo)
        kll = kcc.KnifeBorrow_NOReturnGroupGetList(Me.txtPerNO2.Text, Me.txtM_Code.Text, txtWH.Tag, Nothing, Nothing)

        If kll.Count <> 1 Then
            MsgBox("��e�H���L���M��ɤM�H���I", 64, "����")
            Exit Sub
        Else
            If kll(0).SumB_QTY - kll(0).SumR_QTY < Val(txtQty.Text) Then
                MsgBox("�����Ƥ���", 64, "����")
                Exit Sub
            End If
        End If

        Dim kc As New KnifeBorrowControl
        Dim kl As New List(Of KnifeBorrowInfo)
        kl = kc.KnifeBorrowDedu_GetList(Me.txtM_Code.Text, txtWH.Tag, txtPerNO2.Text, Nothing)
        If kl.Count <= 0 Then
            MsgBox("�L�ƾڦs�b!", 64, "����")
            Exit Sub
        End If

        Dim RQty As Int32
        RQty = Val(txtQty.Text)

        'Dim j As Integer

        Dim i As Integer
        Dim TempDouble As Double
        TempDouble = RQty '�����٪��ƶq

        For i = 0 To kl.Count - 1

            ''-------------------------------------------------------------------------------
            Dim TextBrrow As String = kl(i).B_Num
            Dim j As Integer
            For j = 0 To ds.Tables("KnifeChange").Rows.Count - 1
                If TextBrrow = ds.Tables("KnifeChange").Rows(j)("B_Num").ToString Then
                    MsgBox("�P�@�i���椤�����\�s�b�ۦP����M��I", 64, "����")
                    GridView1.FocusedRowHandle = j
                    Exit Sub
                End If
            Next
            ''-------------------------------------------------------------------------------


            Dim doubleNOReturn As Double '���٤M��
            doubleNOReturn = kl(i).B_Qty - kl(i).R_Qty

            Dim NODouble As Double '
            Dim KnifeReturnDouble As Double '����w�ټ�
            Dim DoubleDeafautCEndQty As Double ''���Z����ϭ�

            If TempDouble > 0 Then
                If TempDouble > doubleNOReturn Then '�k�ټ� �j�� ���ټ�
                    NODouble = 0
                    TempDouble = TempDouble - doubleNOReturn
                    KnifeReturnDouble = kl(i).B_Qty
                    DoubleDeafautCEndQty = kl(i).B_Qty
                Else
                    NODouble = doubleNOReturn - TempDouble
                    KnifeReturnDouble = TempDouble + kl(i).R_Qty '�����k�ټƥ[�W�w�k�ټ�  '�W�U��椣��洫
                    DoubleDeafautCEndQty = kl(i).R_Qty + TempDouble ''�w�ټ�+�i�ټ�
                    TempDouble = 0

                End If

                Dim row As DataRow
                row = ds.Tables("KnifeChange").NewRow
                row("B_Num") = kl(i).B_Num
                row("M_Code") = kl(i).M_Code
                row("M_Name") = kl(i).M_Name
                row("M_Gauge") = kl(i).M_Gauge
                row("CBegin_Qty") = kl(i).R_Qty
                row("CEnd_Qty") = DoubleDeafautCEndQty
                ''-------------------------------------

                row("BQty") = kl(i).B_Qty '���u��M��
                row("RQty") = kl(i).R_Qty '���u�٤M�� 
                row("NOReturn") = kl(i).NOReturn '���ټ�

                ds.Tables("KnifeChange").Rows.Add(row)
            Else
                Exit For
            End If
        Next



    End Sub

    Function ReduceKnifeBrrow(ByVal M_Code As String, ByVal WH_ID As String, ByVal BPer_ID As String, ByVal RQty As Double, ByVal WR_Num As String) As Boolean
        ReduceKnifeBrrow = True

        Dim kc As New KnifeBorrowControl
        Dim kl As New List(Of KnifeBorrowInfo)
        kl = kc.KnifeBorrowDedu_GetList(M_Code, WH_ID, BPer_ID, Nothing)

        If kl.Count <= 0 Then
            ReduceKnifeBrrow = False
            Exit Function
        End If

        Dim i As Integer
        Dim TempDouble As Double
        TempDouble = RQty '�����٪��ƶq

        For i = 0 To kl.Count - 1
            Dim doubleNOReturn As Double '���٤M��
            doubleNOReturn = kl(i).B_Qty - kl(i).R_Qty

            Dim NODouble As Double '
            Dim KnifeReturnDouble As Double '����w�ټ�

            If TempDouble > 0 Then
                If TempDouble > doubleNOReturn Then '�k�ټ� �j�� ���ټ�
                    NODouble = 0
                    TempDouble = TempDouble - doubleNOReturn
                    KnifeReturnDouble = kl(i).B_Qty
                Else
                    NODouble = doubleNOReturn - TempDouble
                    KnifeReturnDouble = TempDouble + kl(i).R_Qty '�����k�ټƥ[�W�w�k�ټ�  '�W�U��椣��洫
                    TempDouble = 0
                End If

                ''��s����ƾ�----------------------------------------------------------------
                Dim wt As New KnifeBorrowControl
                Dim wi As New KnifeBorrowInfo

                wi.B_Num = kl(i).B_Num
                wi.NOReturn = NODouble
                wi.R_Qty = KnifeReturnDouble
                wi.R_Date = Format(Now, "yyyy/MM/dd HH:mm:ss")

                If wt.KnifeBorrow_UpdateRQty(wi) = True Then
                Else
                    ReduceKnifeBrrow = False
                    MsgBox("���ƼƾګO�s����,���ˬd!", 64, "����")
                    Exit Function
                End If
                '��s���A
                Dim wtt As New KnifeReturnControl
                If wtt.KnifeReturnDedu_Update(WR_Num, "True") = False Then
                    ReduceKnifeBrrow = False
                    MsgBox("�٤M�檬�A�O�s����,���ˬd!", 64, "����")
                    Exit Function
                End If
            Else
                Exit For
            End If
        Next

    End Function
#End Region

    ''------------
#Region "����W�["
    Private Sub ButtonChangeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonChangeAdd.Click

        If TxtM_Code4.Text = "" Then
            TxtM_Code4.Select()
            MsgBox("�M��s�����ର��!", 64, "����")
            Exit Sub
        End If

        If txtPerNO4.Text = "" Then
            txtPerNO4.Select()
            MsgBox("�u�����ର��!", 64, "����")
            Exit Sub
        End If

        If txtWH.Text = "" Then
            txtWH.Select()
            MsgBox("�п�ܭܮw�s��!", 64, "����")
            Exit Sub
        End If

        ds.Tables("KnifeChange").Clear()
        AddRow4(TxtM_Code4.Text)

    End Sub

    Sub AddRow4(ByVal strCode As String)
        Dim row As DataRow
        row = ds.Tables("KnifeChange").NewRow

        If strCode = "" Then
        Else
            Dim j As Integer
            For j = 0 To ds.Tables("KnifeChange").Rows.Count - 1
                If strCode = ds.Tables("KnifeChange").Rows(j)("M_Code") Then
                    MsgBox("���Ƥw�s�b,�P�@�i���椤�����\�s�b�ۦP���M��s�X�I", 64, "����")
                    GridView1.FocusedRowHandle = j
                    Exit Sub
                End If
            Next

            Dim wi6 As LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
            Dim wc6 As New LFERP.Library.KnifeWare.KnifeWareInventorySubControl
            wi6 = wc6.KnifeWareInventorySub_GetList(strCode, txtWH.Tag)

            If wi6 Is Nothing Then
                MsgBox("��e�ܮw�L���M��H���I", 64, "����")
                Exit Sub
            End If

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)

            row("M_Code") = objInfo.M_Code
            row("M_Name") = objInfo.M_Name
            row("M_Gauge") = objInfo.M_Gauge
            row("CBegin_Qty") = 0
            row("CEnd_Qty") = 0
            ''------------------------------------------------------
            ds.Tables("KnifeChange").Rows.Add(row)
        End If
        GridView1.MoveLast()
    End Sub

    ''' <summary>
    ''' �W�[�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub SaveDataNewBorrow()
        Dim wbc As New KnifeBorrowControl
        Dim wbi As New KnifeBorrowInfo
        Dim txtB_NOEditValue As String

        txtB_NOEditValue = GetB_ID()

        If Len(txtB_NOEditValue) = 0 Then
            MsgBox("����ͦ��渹�A�L�k�O�s�I", 64, "����")
            Exit Sub
        End If

        ''����y����----------------------
        Dim txtCH_Num As String
        txtCH_Num = KnifeChange_GetNUM()
        If Len(txtCH_Num) <= 0 Then
            MsgBox("����y��������!", 64, "����")
            Exit Sub
        End If
        '-----------------------------------
        Dim j As New Integer
        wbi.WH_ID = txtWH.Tag
        wbi.BPer_ID = txtPerNO4.Text
        wbi.BPer_Name = ""
        wbi.B_NO = txtB_NOEditValue
        wbi.B_Date = Format(CDate(DateC_Date.EditValue), "yyyy/MM/dd")
        wbi.B_Action = InUserID
        wbi.B_Type = "�s�M"

        For j = 0 To ds.Tables("KnifeChange").Rows.Count - 1
            wbi.B_Num = GetB_NUM()
            Dim Ls As String
            Ls = wbi.B_Num

            wbi.M_Code = ds.Tables("KnifeChange").Rows(j)("M_Code").ToString
            wbi.B_Qty = ds.Tables("KnifeChange").Rows(j)("CEnd_Qty")
            wbi.NOReturn = ds.Tables("KnifeChange").Rows(j)("CEnd_Qty")
            wbi.B_Remark = "�t�Φ۰ʼW�[:" & ds.Tables("KnifeChange").Rows(j)("CReMark").ToString

            Dim wi As LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
            Dim wc As New LFERP.Library.KnifeWare.KnifeWareInventorySubControl
            Dim intEndQty As Int32 = 0
            Dim intAllEndQty As Int32 = 0

            wi = wc.KnifeWareInventorySub_GetList(ds.Tables("KnifeChange").Rows(j)("M_Code").ToString, txtWH.Tag)
            If wi Is Nothing Then
            Else
                intEndQty = wi.WI_SQty
                intAllEndQty = wi.WI_Qty
            End If

            wbi.B_EndQty = intEndQty - ds.Tables("KnifeChange").Rows(j)("CEnd_Qty")
            wbi.B_AllEndQty = intAllEndQty - ds.Tables("KnifeChange").Rows(j)("CEnd_Qty")

            If wbc.KnifeBorrow_Add(wbi) = True Then
                ''�W�[����O��
                If SaveDataChange(txtCH_Num, j, Ls) = True Then
                Else
                    MsgBox("�����O�s����!", 64, "����")
                    Exit Sub
                End If
            End If
        Next
        MsgBox("�O�s���\!", 64, "����")
        Me.Close()
    End Sub
    ''' <summary>
    ''' �W�[����O��
    ''' </summary>
    ''' <param name="_txtCH_Num"></param>
    ''' <param name="k"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveDataChange(ByVal _txtCH_Num As String, ByVal k As Integer, ByVal _BR_NO As String) As Boolean

        SaveDataChange = True

        ''�W�[�@�����O��
        Dim cc As New KnifeChangeControl
        Dim ci As New KnifeChangeInfo

        ci.CH_Num = _txtCH_Num

        '2014-01-08
        ci.BR_NO = _BR_NO

        ci.M_Code = ds.Tables("KnifeChange").Rows(k)("M_Code").ToString
        ci.WH_ID = txtWH.Tag

        ci.CBegin_Qty = ds.Tables("KnifeChange").Rows(k)("CBegin_Qty")
        ci.CEnd_Qty = ds.Tables("KnifeChange").Rows(k)("CEnd_Qty")
        ci.CKType = GridLookChangeType.EditValue

        ci.CReMark = ds.Tables("KnifeChange").Rows(k)("CReMark").ToString
        ci.C_Action = InUserID
        ci.C_Date = Format(CDate(DateC_Date.EditValue), "yyyy/MM/dd HH:mm:ss")
        ci.CReMark = "�Q�ʽվ�--" & ds.Tables("KnifeChange").Rows(k)("CReMark")

        If cc.KnifeChange_Add(ci) = True Then
        Else
            SaveDataChange = True
        End If

    End Function

    Function GetB_ID() As String
        '�ͦ��spm
        Dim pm As New KnifeBorrowControl
        Dim pi As New KnifeBorrowInfo
        Dim ndate As String
        ndate = "KF" + Format(Now(), "yyMM")
        pi = pm.KnifeBorrow_GetID(ndate)
        If pi Is Nothing Then
            GetB_ID = ndate + "00001"
        Else
            GetB_ID = ndate + Mid((CInt(Mid(pi.B_NO, 7)) + 100001), 2)
        End If
    End Function

    Function GetB_NUM() As String
        '�ͦ��spS
        Dim pm1 As New KnifeBorrowControl
        Dim pi1 As New KnifeBorrowInfo
        Dim ndate As String
        ndate = "KF"
        pi1 = pm1.KnifeBorrow_GetNUM(ndate)
        If pi1 Is Nothing Then
            GetB_NUM = "KF000000001"
        Else
            GetB_NUM = "KF" & Mid((CInt(Mid(pi1.B_Num, 3)) + 1000000001), 2)
        End If
    End Function
#End Region

End Class