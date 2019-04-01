Imports EncodeMy
Imports LFERP.Library.KnifeWare
Imports LFERP.Library.WareHouse
Imports LFERP.SystemManager

Public Class frmKnifeBorrow
    Dim ds As New DataSet
    Public strWHID As String
    Public strWH_Name As String

    Dim LoadStr As String
    Dim LabStr As String
    Dim strAutoID As String
    Dim JS As Integer = 0

    '���u�u���ݩ�
    Private m_StrBNo As String
    Public Property StrBNo() As String
        Get
            Return m_StrBNo
        End Get
        Set(ByVal value As String)
            m_StrBNo = value
        End Set
    End Property

    ''�ܮwID 
    'Private m_StrWHID As String
    'Public Property WHID() As String
    '    Get
    '        Return m_StrWHID
    '    End Get
    '    Set(ByVal value As String)
    '        m_StrWHID = value
    '    End Set
    'End Property


    Private Sub frmKnifeBorrow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTables()
        cmdSave.Enabled = False

        ' strAutoID = tempValue

        txtWH.Tag = strWHID
        txtWH.Text = strWH_Name

        LabType.Text = tempValue3
        txtB_NO.Text = tempValue4
        LoadStr = tempValue5

        tempValue3 = Nothing
        tempValue = Nothing
        tempValue2 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing
        tempValue7 = Nothing

        LabStr = LabType.Text
        Select Case LabStr
            Case "AddNew", "AddOld"
                ''���J�W��
                '********************************************************2013-11-14 ���@����*************
                'Dim knl As New List(Of KnifeWhiteUserInfo)
                'Dim knc As New KnifeWhiteUserController
                'knl = knc.WhiteUser_GetList(strAutoID, Nothing, Nothing, Nothing, Nothing, False)
                'LabSX.Text = knl(0).WMax
                'txtWH.Tag = knl(0).WH_ID
                'txtWH.Text = knl(0).WH_Name
                'LabPerNO.Text = knl(0).W_UserID
                'labBPer_Name.Text = knl(0).W_UserName

                'strWHID = knl(0).WH_ID
                '*****************************************************************************************
                ''-----------------------------------------------------------------

                If LabStr = "AddNew" Then
                    txtB_Type.Text = "�s�M"
                    Me.Text = "�ɤM�n�O-�s�M"
                    LabMsg.Text = "�ɤM�n�O-�s�M"
                Else
                    txtB_Type.Text = "�ݳB�z"
                    Me.Text = "�ɤM�n�O-�ݳB�z"
                    LabMsg.Text = "�ɤM�n�O-�ݳB�z"
                End If

                DateB_Date.Enabled = False
                DateB_Date.EditValue = Format(Now, "yyyy/MM/dd")

                If LoadStr <> "" Then  ''���J�襤��
                    AddRow(LoadStr, 1)
                End If

                Dim reset As New ResetPassWords.SetPassWords
                reset.SetPassWords()

            Case "View"
                cmdSave.Visible = False
                LoadData()

        End Select
 


    End Sub



    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("KnifeBorrow")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("B_Num", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))

            .Columns.Add("B_Qty", GetType(Int32))
            .Columns.Add("B_Remark", GetType(String))
            .Columns.Add("B_EndQty", GetType(Int32))
            .Columns.Add("B_AllEndQty", GetType(Int32))

            '�d�ߥX�o�ӤH�A�Ҧ����M�����ټ�
            .Columns.Add("B_AllNoReturn", GetType(Int32))

        End With

        Grid1.DataSource = ds.Tables("KnifeBorrow")

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub popWareOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutAdd.Click
        tempCode = ""
        tempValue5 = strWHID
        tempValue6 = "Knife"
        tempValue7 = "�ɤM�X�w�@�~"
        frmKnifeBOMSelect.ShowDialog()

        If frmKnifeBOMSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then
            '�W�[�O��
            If tempCode = "" Then
                Exit Sub
            Else
                AddRow(tempCode, 0)
            End If
        End If
        tempValue7 = ""
        tempValue8 = ""
    End Sub

    Sub AddRow(ByVal strCode As String, ByVal strQty As Single)
        Dim row As DataRow
        row = ds.Tables("KnifeBorrow").NewRow

        If strCode = "" Then
        Else
            Dim j As Integer
            For j = 0 To ds.Tables("KnifeBorrow").Rows.Count - 1
                If strCode = ds.Tables("KnifeBorrow").Rows(j)("M_Code") Then
                    MsgBox("���Ƥw�s�b�A�P�@�i�ɤM�椤�����\�s�b�ۦP���M��s�X�I", 64, "����")
                    GridView1.FocusedRowHandle = j
                    Exit Sub
                End If
            Next

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)

            'If objInfo.M_IsEnabled = False Then
            '    MsgBox("��e�M�㤣�i�ΡA�нT�{�����ƫH���I", 64, "����")
            '    Exit Sub
            'End If

            row("M_Code") = objInfo.M_Code
            row("M_Name") = objInfo.M_Name
            row("M_Gauge") = objInfo.M_Gauge
            '��-----------------�l--------------------------------
            Dim wi6 As LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
            Dim wc6 As New LFERP.Library.KnifeWare.KnifeWareInventorySubControl
            wi6 = wc6.KnifeWareInventorySub_GetList(strCode, strWHID)
            If wi6 Is Nothing Then
                row("B_EndQty") = 0
            Else
                If LabStr = "AddNew" Then
                    row("B_EndQty") = wi6.WI_SQty
                ElseIf LabStr = "AddOld" Then
                    row("B_EndQty") = wi6.WI_SReQty
                End If
            End If

            row("B_Qty") = 0

            '---------------------------------------------

            row("B_AllNoReturn") = GetNORerurn(txtBPer_ID.Text, strCode, strWHID)



            ''------------------------------------------------------
            ds.Tables("KnifeBorrow").Rows.Add(row)
            ''@ 2013/6/11 �K�[ �s�W�����ƫH���bLCD��ܫ̤W���
            'If isShowLCD = True Then
            '    LoadPingMU("�ɼ˿�M", "�W�١G" & ds.Tables("Borrow").Rows(GridView1.FocusedRowHandle)("M_Name"), "�W��G" & ds.Tables("Borrow").Rows(GridView1.FocusedRowHandle)("M_Gauge"), "�ƶq�G" & ds.Tables("Borrow").Rows(GridView1.FocusedRowHandle)("Qty") & ds.Tables("Borrow").Rows(GridView1.FocusedRowHandle)("M_Unit"))
            'End If
        End If
        GridView1.MoveLast()
    End Sub

    Private Sub sk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sk.Click

        'Me.txtBPer_ID.Text = ReadCard1()
        'Dim bPerId As String = "10020433"

        '************************************************************************************
        '*********************************2013-11-15 ���@�s�W�ק�****************************
        Dim bPerId As String = ReadCard1()
        If bPerId = "" Then Exit Sub

        'Dim strWHRemark As String
        'Dim wc As New WareHouseController
        'Dim wl As New List(Of WareHouseInfo)
        'wl = wc.WareHouse_Get(strWHID)
        'strWHRemark = wl(0).WH_Remark  '�p�G�O�]�Z��

        Dim knl As New List(Of KnifeWhiteUserInfo)
        Dim knc As New KnifeWhiteUserController

        If GetNightWareHouse(strWHID) = True Then
            knl = knc.WhiteUser_NightGetList(strWHID, bPerId)
        Else
            knl = knc.WhiteUser_GetListAll(Nothing, bPerId, strWHID, Nothing, Nothing, False)
        End If

        If knl Is Nothing Then
            LoadPingMU("�D�k�d���I", "", "", "")
            MsgBox(" �d�߹�H����Ҥ�!", 64, "����")
            Exit Sub
        End If

        If knl.Count = 0 Then
            LoadPingMU("�D�k�d���I", "", "", "")
            MsgBox(bPerId & " �L�b[" & strWHID & "]��M���v��!", 64, "����")
            Exit Sub
        End If

        Try
            Me.txtBPer_ID.Text = bPerId
            LabSX.Text = knl(0).WMax
            'txtWH.Tag = knl(0).WH_ID
            ' txtWH.Text = knl(0).WH_Name
            LabPerNO.Text = knl(0).W_UserID
            labBPer_Name.Text = knl(0).W_UserName

            ' strWHID = knl(0).WH_ID

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Exit Sub
        End Try
        '************************************************************************************

        ''�[�զW��
        If Me.txtBPer_ID.Text <> "" Then
            cmdSave.Enabled = True
            sk.Enabled = False
        End If
        ''��ܫ�
        Dim k As Integer

        If ds.Tables("KnifeBorrow").Rows.Count > 0 Then
            For k = 0 To ds.Tables("KnifeBorrow").Rows.Count - 1
                ds.Tables("KnifeBorrow").Rows(k)("B_AllNoReturn") = GetNORerurn(txtBPer_ID.Text, ds.Tables("KnifeBorrow").Rows(k)("M_Code").ToString, strWHID)
            Next
        End If

    End Sub

    Private Sub popWareOutDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutDel.Click
        If ds.Tables("KnifeBorrow").Rows.Count = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "M_Code")
        ds.Tables("KnifeBorrow").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckSave() = True Then
            If JS > 0 Then
                Exit Sub
            End If
            JS = JS + 1

            SaveDataNew()
        End If
    End Sub

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
    ''' <summary>
    ''' �ˬd�ƾ�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckSave() As Boolean
        CheckSave = True

        If txtBPer_ID.Text = "" Then
            MsgBox("�L��d�H��,���ˬd!", 64, "����")
            CheckSave = False
            Exit Function
        End If


        If LabPerNO.Text = txtBPer_ID.Text Then
        Else
            MsgBox("��d�H�P��ܾܭɤM�H�H�����P,���ˬd!", 64, "����")
            CheckSave = False
            Exit Function
        End If

        If ds.Tables("KnifeBorrow").Rows.Count <= 0 Then
            MsgBox("�L�ƾګO�s!", 64, "����")
            CheckSave = False
            Exit Function
        End If

        '�O�_���\�h���O��2013-11-17
        If GetPMWS_Value("510504") = False Then
            If ds.Tables("KnifeBorrow").Rows.Count > 1 Then
                MsgBox("�����\�h��ƾ�!", 64, "����")
                CheckSave = False
                Exit Function
            End If
        End If

        Dim BorrowTatal As Int32 = 0

        ''---------------------------------------------------------------------------------------------
        Dim j As Integer
        For j = 0 To ds.Tables("KnifeBorrow").Rows.Count - 1
            If ds.Tables("KnifeBorrow").Rows(j)("M_Code").ToString.Trim = "" Then
                MsgBox("�M��s�X���ର��", 64, "����")
                CheckSave = False
                Exit Function
            End If
            If ds.Tables("KnifeBorrow").Rows(j)("M_Name").ToString.Trim = "" Then
                MsgBox("�M��W�٤��ର��", 64, "����")
                CheckSave = False
                Exit Function
            End If
            If ds.Tables("KnifeBorrow").Rows(j)("M_Gauge").ToString.Trim = "" Then
                MsgBox("�M��W�椣�ର��", 64, "����")
                CheckSave = False
                Exit Function
            End If
            If ds.Tables("KnifeBorrow").Rows(j)("B_Qty") Is DBNull.Value Then
                MsgBox("�ƶq���ର��!", 64, "����")
                CheckSave = False
                Exit Function
            End If

            If ds.Tables("KnifeBorrow").Rows(j)("B_Qty") <= 0 Then
                MsgBox("�ƶq���൥��0!", 64, "����")
                CheckSave = False
                Exit Function
            End If
            '--------------------------------------------------------�P�_�O�_���D�ɮw�s
            Dim wic As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            Dim wii As New LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            wii = wic.WareInventory_GetSub(ds.Tables("KnifeBorrow").Rows(j)("M_Code").ToString, strWHID)
            Dim Qty As Double = 0
            If wii Is Nothing Then
                MsgBox("��e�o�X�ܮw���s�b�����ƫH��", 64, "����")
                GridView1.FocusedRowHandle = j
                CheckSave = False
                Exit Function
            Else
                Qty = wii.WI_Qty
            End If

            If Qty < CDbl(ds.Tables("KnifeBorrow").Rows(j)("B_Qty").ToString) Then
                MsgBox("�o�X�ܮw�s����!", 64, "����")
                GridView1.FocusedRowHandle = j
                CheckSave = False
                Exit Function
            End If

            '--------------------------------------------------------�P�_�O�_���l�ɮw�s
            Dim wi5 As LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
            Dim wc5 As New LFERP.Library.KnifeWare.KnifeWareInventorySubControl
            wi5 = wc5.KnifeWareInventorySub_GetList(ds.Tables("KnifeBorrow").Rows(j)("M_Code"), strWHID)
            If wi5 Is Nothing Then
                MsgBox("��e�o�X�ܮw�M���ݩʭܮw���s�b�����ƫH��", 64, "����")
                GridView1.FocusedRowHandle = j
                CheckSave = False
                Exit Function
            Else
                Select Case LabStr
                    Case "AddNew" ''''�s�M
                        If wi5.WI_SQty < CDbl(ds.Tables("KnifeBorrow").Rows(j)("B_Qty").ToString) Then
                            MsgBox("�o�X�s�M�S�����w�s�ήw�s����!", 64, "����")
                            GridView1.FocusedRowHandle = j
                            CheckSave = False
                            Exit Function
                        End If

                        ds.Tables("KnifeBorrow").Rows(j)("B_EndQty") = wi5.WI_SQty

                    Case "AddOld" '�ݳB�z
                        If wi5.WI_SReQty < CDbl(ds.Tables("KnifeBorrow").Rows(j)("B_Qty").ToString) Then
                            MsgBox("�o�X�ݳB�z�S�����w�s�ήw�s����!", 64, "����")
                            GridView1.FocusedRowHandle = j
                            CheckSave = False
                            Exit Function
                        End If

                        ds.Tables("KnifeBorrow").Rows(j)("B_EndQty") = wi5.WI_SReQty

                End Select
            End If

            ds.Tables("KnifeBorrow").Rows(j)("B_AllEndQty") = Qty

            BorrowTatal = BorrowTatal + ds.Tables("KnifeBorrow").Rows(j)("B_Qty")
        Next

        ' '' ''�O��n�P�_�w�s,�γ]�w���W��


        '' ''KnifeBorrow_NOReturnGetList
        ' ''Dim kc As New KnifeReturnControl
        ' ''Dim kl As New List(Of KnifeReturnInfo)
        ' ''kl = kc.KnifeBorrow_NOReturnGetList(txtBPer_ID.Text, Nothing, Nothing)

        ' ''Dim knl As New List(Of KnifeWhiteUserInfo)
        ' ''Dim knc As New KnifeWhiteUserController
        ' ''knl = knc.WhiteUser_GetList(strAutoID, Nothing, Nothing, Nothing, Nothing, False)

        ' ''If BorrowTatal + (kl(0).SumB_QTY - kl(0).SumR_QTY) > knl(0).WMax Then
        ' ''    MsgBox(labBPer_Name.Text & " �����u��M�ƶq,�w�W�W����!" & "���٤M�Ƭ�:" & kl(0).SumB_QTY - kl(0).SumR_QTY, 64, "����")
        ' ''    CheckSave = False
        ' ''    Exit Function
        ' ''End If

        ''�O��n�P�_�w�s,�γ]�w���W��

        '********************************************************************************************
        '************************************2013-11-15 ���@�ק�*************************************
        'KnifeBorrow_NOReturnGetList
        Dim kc As New KnifeReturnControl
        Dim kl As New List(Of KnifeReturnInfo)
        kl = kc.KnifeBorrow_NOReturnGetList(txtBPer_ID.Text, Nothing, Nothing)

        If kl.Count = 0 Then
            MsgBox("���k�ٮw�s�L�O��!", 64, "����")
            CheckSave = False
            Exit Function
        End If

        Dim knl As New List(Of KnifeWhiteUserInfo)
        Dim knc As New KnifeWhiteUserController

        If GetNightWareHouse(strWHID) = True Then
            knl = knc.WhiteUser_NightGetList(strWHID, txtBPer_ID.Text)
        Else
            knl = knc.WhiteUser_GetListAll(Nothing, txtBPer_ID.Text, strWHID, Nothing, Nothing, False)
        End If

        'Dim knl As New List(Of KnifeWhiteUserInfo)
        'Dim knc As New KnifeWhiteUserController
        'knl = knc.WhiteUser_GetListAll(Nothing, txtBPer_ID.Text, strWHID, Nothing, Nothing, False)

        If knl.Count = 0 Then
            MsgBox("�զW�椣�s�b!", 64, "����")
            CheckSave = False
            Exit Function
        End If

        If BorrowTatal + (kl(0).SumB_QTY - kl(0).SumR_QTY) > knl(0).WMax Then
            MsgBox(labBPer_Name.Text & " �����u��M�ƶq,�w�W�W����!" & "���٤M�Ƭ�:" & kl(0).SumB_QTY - kl(0).SumR_QTY, 64, "����")
            CheckSave = False
            Exit Function
        End If
        '***************************************************************************************************************************


    End Function
    ''' <summary>
    ''' �W�[�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub SaveDataNew()
        Dim wbc As New KnifeBorrowControl
        Dim wbi As New KnifeBorrowInfo

        txtB_NO.EditValue = GetB_ID()

        If Len(txtB_NO.EditValue) = 0 Then
            MsgBox("����ͦ��渹�A�L�k�O�s�I", 64, "����")
            Exit Sub
        End If

        '----------------------------------------------------------------------------------
        '�P�_�渹�O�_�s�b
        Dim wbcA As New KnifeBorrowControl
        Dim wbL As New List(Of KnifeBorrowInfo)

        wbL = wbcA.KnifeBorrow_GetList(Nothing, txtB_NO.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If wbL.Count > 0 Then
            MsgBox("����w�s�b,�Э��s�O�s�I", 64, "����")
            Exit Sub
        End If
        '----------------------------------------------------------------------------------


        Dim j As New Integer
        wbi.WH_ID = strWHID
        wbi.BPer_ID = Me.txtBPer_ID.EditValue
        wbi.BPer_Name = Me.labBPer_Name.Text
        wbi.B_NO = txtB_NO.EditValue
        wbi.B_Date = Format(CDate(DateB_Date.EditValue), "yyyy-MM-dd") & " " & Format(Now, "HH:mm:ss")
        wbi.B_Action = InUserID
        wbi.B_Type = txtB_Type.Text

        For j = 0 To ds.Tables("KnifeBorrow").Rows.Count - 1
            wbi.B_Num = GetB_NUM()
            wbi.M_Code = ds.Tables("KnifeBorrow").Rows(j)("M_Code").ToString
            wbi.B_Qty = ds.Tables("KnifeBorrow").Rows(j)("B_Qty")
            wbi.NOReturn = ds.Tables("KnifeBorrow").Rows(j)("B_Qty")

            wbi.B_Remark = ds.Tables("KnifeBorrow").Rows(j)("B_Remark").ToString
            wbi.B_EndQty = ds.Tables("KnifeBorrow").Rows(j)("B_EndQty") - ds.Tables("KnifeBorrow").Rows(j)("B_Qty")
            wbi.B_AllEndQty = ds.Tables("KnifeBorrow").Rows(j)("B_AllEndQty") - ds.Tables("KnifeBorrow").Rows(j)("B_Qty")

            If wbc.KnifeBorrow_Add(wbi) = True Then
                ''�n����
                If UpdateQty(ds.Tables("KnifeBorrow").Rows(j)("M_Code").ToString, ds.Tables("KnifeBorrow").Rows(j)("B_Qty")) = False Then
                    MsgBox(wbi.M_Code & " ���ƥ���,���ˬd!", 64, "����")
                    Exit Sub
                End If
            Else
                MsgBox("�����O�s����!", 64, "����")
                Exit Sub
            End If


            Dim str1 As String = ds.Tables("KnifeBorrow").Rows(j)("M_Name").ToString & " " & ds.Tables("KnifeBorrow").Rows(j)("M_Gauge").ToString
            Dim str2 As String = "�ƶq:" & ds.Tables("KnifeBorrow").Rows(j)("B_Qty").ToString
            Dim str3 As String = "���A:�w�O�s ������:" & CStr(ds.Tables("KnifeBorrow").Rows(j)("B_AllNoReturn") + ds.Tables("KnifeBorrow").Rows(j)("B_Qty"))
            Dim str4 As String = "�ɤM�H:" & labBPer_Name.Text


            LoadPingMU(str1, str2, str3, str4)

        Next




        MsgBox("�O�s���\!", 64, "����")
        Me.Close()
    End Sub

    Function GetNORerurn(ByVal sBPer_ID As String, ByVal sM_code As String, ByVal sWH_ID As String) As Int32
        Dim kc As New KnifeReturnControl
        Dim kl As New List(Of KnifeReturnInfo)
        kl = kc.KnifeBorrow_NOReturnGetList(sBPer_ID, sM_code, sWH_ID)
        If kl.Count = 0 Then
            GetNORerurn = 0
        Else
            GetNORerurn = kl(0).SumNOReturn
        End If
    End Function


    Function UpdateQty(ByVal _M_code As String, ByVal _B_Qty As String) As Boolean

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

        wiinfo = wcco.KnifeWareInventorySub_GetList(_M_code, strWHID)
        If wiinfo Is Nothing Then
            dbWI_SReQty = 0
            dblWI_SQty = 0
        Else
            dblWI_SQty = wiinfo.WI_SQty
            dbWI_SReQty = wiinfo.WI_SReQty
        End If
        '---------------------'---------------------'---------------------'---------------------
        Dim wifo As New LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
        Select Case LabStr
            Case "AddNew" ''''�s�M
                wifo.WI_SQty = (dblWI_SQty - _B_Qty)
                wifo.WI_SReQty = dbWI_SReQty
            Case "AddOld" '�ݳB�z
                wifo.WI_SQty = dblWI_SQty
                wifo.WI_SReQty = (dbWI_SReQty - _B_Qty)
        End Select

        wifo.WI_All = dblWI_All - _B_Qty
        wifo.M_Code = _M_code
        wifo.WH_ID = txtWH.Tag
        If wcco.KnifeWareInventorySub_Update(wifo) = False Then
            UpdateQty = False
        End If
    End Function

    ''' <summary>
    ''' ���J�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadData()
        Dim bc As New KnifeBorrowControl
        Dim objInfo As New List(Of KnifeBorrowInfo)

        objInfo = bc.KnifeBorrow_GetList(Nothing, txtB_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If objInfo.Count <= 0 Then
            MsgBox("�L�ƾڦs�b!", 64, "����")
            Exit Sub
        End If
        ''-----------------------------------------------------------------
        txtWH.Tag = objInfo(0).WH_ID
        txtWH.Text = objInfo(0).WH_Name
        DateB_Date.Text = objInfo(0).B_Date
        txtBPer_ID.Text = objInfo(0).BPer_ID
        labBPer_Name.Text = objInfo(0).BPer_Name

        txtB_Type.Text = objInfo(0).B_Type
        ''------------------------------------------------------------------
        Dim row As DataRow

        Dim i As Integer
        For i = 0 To objInfo.Count - 1
            row = ds.Tables("KnifeBorrow").NewRow
            row("B_Num") = objInfo(i).AutoID
            row("M_Code") = objInfo(i).M_Code
            row("M_Name") = objInfo(i).M_Name
            row("M_Gauge") = objInfo(i).M_Gauge

            row("B_Qty") = objInfo(i).B_Qty
            row("B_EndQty") = objInfo(i).B_EndQty
            row("B_Remark") = objInfo(i).B_Remark
            ds.Tables("KnifeBorrow").Rows.Add(row)
        Next
    End Sub




    Private Sub ButtonTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTest.Click
        LoadPingMU("test", "test", "test", "test")
    End Sub
End Class