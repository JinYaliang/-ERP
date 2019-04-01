Imports LFERP.Library.WareHouse.WareBorrowReturn
Imports LFERP.Library.WareHouse
Imports LFERP.Library.Purchase.SharePurchase
Imports LFERP.Library.Shared
Imports LFERP.SystemManager

Public Class frmWareReturn
    Dim ds As New DataSet
    Dim strWHID As String
    Dim strDPTID As String
    Dim isShowLCD As Boolean = False  '�O�_�bLCD�p�̹��W��ܥX�w�H��

    '@ 2013/6/11 �K�[
    Private Sub frmWareReturn_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If isShowLCD = False Then Exit Sub

        ApiDisplay.display_wellcome()   'LCD��ܫ̫�_����l�ɭ�
        ApiDisplay.com_exit() '�_�}COM�s��
        isOpenCOM = False   'COM���}�аO��False
    End Sub

    Private Sub frmWareReturn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTables()
        LoadUserPower()

        LabMsg.Text = tempValue
        strWHID = tempValue3
        txtWH.EditValue = tempValue4
        txtWBID.Text = tempValue5

        tempValue4 = Nothing
        tempValue3 = Nothing
        tempValue = Nothing
        tempValue5 = Nothing

        ButtonEdit2.Enabled = False
        cmdSave.Enabled = False


        Select Case LabMsg.Text
            Case "�٤M��"
                If Edit = False Then
                    DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")
                    Label3.Visible = False
                    TxtReReturn.Visible = False
                    LabReReturn.Visible = False
                    Me.Text = "�٤M��-�s�W"

                    '���m�s��d��
                    Dim reset As New ResetPassWords.SetPassWords
                    reset.SetPassWords()
                    txtWBID.Enabled = False
                End If

            Case "�N�٤M"
                If Edit = False Then
                    DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")
                    Label3.Visible = True
                    TxtReReturn.Visible = True
                    LabReReturn.Visible = True

                    Me.Text = "�N�٤M-�s�W"
                    TextEdit1.Enabled = True

                    '���m�s��d��
                    Dim reset As New ResetPassWords.SetPassWords
                    reset.SetPassWords()
                    txtWBID.Enabled = False
                End If

            Case "�d��"
                Me.Text = "�٤M��-�d��"
                NoReturnQty.Visible = False
                popWareWB.Enabled = False
                cmdSave.Visible = False

                loadedit(txtWBID.Text)

        End Select

 

    End Sub

    '@ 2013/6/11 �K�[ �v������
    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        '�bLCD�p��ܫ̤W��ܫH��
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "50100204")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then isShowLCD = True
        End If
    End Sub

    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("Return")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            '
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("Qty", GetType(Double))
            .Columns.Add("NoReturnQty", GetType(Double))
            .Columns.Add("Remark", GetType(String))
        End With

        Grid.DataSource = ds.Tables("Return")
    End Sub



    Sub loadedit(ByVal WR_ID As String)
        ds.Tables("Return").Clear()

        Dim wbc As New WareBorrowReturnControl
        Dim objInfo As New List(Of WareBorrowReturnInfo)

        Dim row As DataRow
        Try
            objInfo = wbc.WareBorrowReturn_GetList(Nothing, WR_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If objInfo.Count <= 0 Then
                MsgBox("�L�ƾڸ��J�I")
                Exit Sub
            End If

            txtWH.EditValue = objInfo(0).WH_ID
            ButtonEdit2.EditValue = objInfo(0).DPT_Name

            TextEdit1.Text = objInfo(0).WB_PerID
            Label4.Text = objInfo(0).WB_PerName

            DateEdit1.EditValue = objInfo(0).WB_Date

            txtWBID.Text = objInfo(0).WB_NO

            Dim i As Integer

            For i = 0 To objInfo.Count - 1
                row = ds.Tables("Return").NewRow
                row("AutoID") = objInfo(i).AutoID
                row("M_Code") = objInfo(i).M_Code
                row("M_Name") = objInfo(i).M_Name
                row("M_Gauge") = objInfo(i).M_Gauge
                row("M_Unit") = objInfo(i).M_Unit

                row("Qty") = objInfo(i).Qty
                row("Remark") = objInfo(i).Remark

                ds.Tables("Return").Rows.Add(row)
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Sub SaveDataNew()
        Dim wbc As New WareBorrowReturnControl
        Dim wbi As New WareBorrowReturnInfo

        txtWBID.EditValue = GetWR_ID()

        If Len(txtWBID.EditValue) = 0 Then
            MsgBox("����ͦ��渹�A�L�k�O�s�I")
            Exit Sub
        End If

        Dim j As New Integer
        For j = 0 To ds.Tables("Return").Rows.Count - 1
            wbi.WB_NO = txtWBID.EditValue
            ''-------------------------------------------
            Dim strWR_NUM As String
            strWR_NUM = GetWR_NUM()
            wbi.WB_NUM = strWR_NUM
            ''-------------------------------------------

            wbi.WB_Type = "�٤M"

            wbi.M_Code = ds.Tables("Return").Rows(j)("M_Code").ToString
            wbi.Qty = ds.Tables("Return").Rows(j)("Qty")

            wbi.DPT_ID = strDPTID
            wbi.WB_PerID = TextEdit1.Text
            wbi.WB_PerName = Label4.Text

            wbi.WH_ID = strWHID
            wbi.WB_Action = InUserID
            wbi.WB_Date = Format(Now, "yyyy/MM/dd")

            wbi.RR_PerID = TxtReReturn.Text
            wbi.RR_PerName = LabReReturn.Text

            wbi.Remark = ds.Tables("Return").Rows(j)("Remark").ToString

            If wbc.WareBorrowReturn_Add(wbi) = True Then
                '�H���ɥ��ټҦ������ƶq
                If UpdateNOSend(ds.Tables("Return").Rows(j)("M_Code").ToString, TextEdit1.Text, ds.Tables("Return").Rows(j)("Qty"), strWR_NUM) = True Then
                    ''�W�[���\�Z������e��ܭܮw�A�w�s
                    Dim mt As New SharePurchaseController
                    Dim mm As New SharePurchaseInfo
                    mm.WH_ID = strWHID
                    mm.M_Code = ds.Tables("Return").Rows(j)("M_Code")

                    Dim Qty As Single
                    Dim wi As New LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                    wi = wc.WareInventory_GetSub(ds.Tables("Return").Rows(j)("M_Code"), strWHID)

                    If wi Is Nothing Then
                        Qty = 0
                    Else
                        Qty = wi.WI_Qty
                    End If

                    mm.WI_Qty = Qty + CSng(ds.Tables("Return").Rows(j)("Qty"))

                    mt.UpdateWareInventory_WIQty2(mm)
                End If
            Else
                MsgBox("�����O�s����!")
                Exit Sub
            End If
        Next

        MsgBox("�O�s���\!")

    End Sub

    ''�H���ɥ��ټҦ������ƶq
    Function UpdateNOSend(ByVal strM_Code As String, ByVal Per_ID As String, ByVal Tatal_Count As Double, ByVal strWR_NUMA As String) As Boolean
        UpdateNOSend = True

        Dim wbt As New WareBorrowReturnControl
        Dim wbl As New List(Of WareBorrowReturnInfo)

        Try

            wbl = wbt.WareBorrowReturn_GetList(Nothing, Nothing, "�ɤM", strM_Code, 100, Nothing, Per_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If wbl.Count <= 0 Then
                UpdateNOSend = False
                MsgBox("�ƶq�������~�I")
                Exit Function
            End If

            ' WBR_NO �O���U �٪��M�O�������ӭɤM��
            Dim strWBR_NO As String = ""
            Dim i As Integer
            Dim TempDouble As Double '�n�������`��
            TempDouble = Tatal_Count

            For i = 0 To wbl.Count - 1
                Dim NODouble As Double '
                Dim WBR_NODouble As Double '�������� �ƶq

                If TempDouble > 0 Then
                    If TempDouble > wbl(i).NO_ReQty Then
                        NODouble = 0
                        TempDouble = TempDouble - wbl(i).NO_ReQty
                        WBR_NODouble = wbl(i).NO_ReQty
                    Else
                        NODouble = wbl(i).NO_ReQty - TempDouble  '���椤����Ƥj�� �٤M�`��
                        WBR_NODouble = TempDouble
                        TempDouble = 0
                    End If

                    '�渹-------------------
                    If wbl(i).WBR_NO = "" Then
                        strWBR_NO = strWR_NUMA
                    Else
                        strWBR_NO = wbl(i).WBR_NO & "," & strWR_NUMA
                    End If
                    '�ƶq-------------------
                    Dim WBR_QtyStr As String = ""
                    If wbl(i).WBR_QtyStr = "" Then
                        WBR_QtyStr = Str(WBR_NODouble)
                    Else
                        WBR_QtyStr = wbl(i).WBR_QtyStr & "," & Str(WBR_NODouble)
                    End If

                    ''��s����ƾ�-----------------------------------
                    Dim wt As New WareBorrowReturnControl
                    Dim wi As New WareBorrowReturnInfo

                    wi.WB_NUM = wbl(i).WB_NUM
                    wi.NO_ReQty = NODouble
                    wi.WBR_NO = strWBR_NO
                    wi.WBR_QtyStr = WBR_QtyStr


                    If wt.WareBorrowReturn_UpdateNO_ReQty(wi) = True Then
                    Else
                        UpdateNOSend = False
                        MsgBox("�����ƾګO�s����,���ˬd!")
                    End If


                    '-�bWareBorrowReturnSub�l���O���b�ɤM��P�٤M�����-----
                    Dim wtt As New WareBorrowReturnControl
                    Dim wii As New WareBorrowReturnInfo

                    wii.WB_NUM = wbl(i).WB_NUM
                    wii.WR_NUM = strWR_NUMA
                    wii.Qty = Str(WBR_NODouble)

                    If wtt.WareBorrowReturnSub_Add(wii) = True Then
                    Else
                        UpdateNOSend = False
                        MsgBox("�����ƾګO�s����,���ˬd!")
                    End If


                End If
                ''------------------------------------------------
            Next

        Catch ex As Exception
            UpdateNOSend = False
            MsgBox(ex.Message)
        End Try
    End Function




    Function CheckSave() As Boolean
        CheckSave = True

        If LabMsg.Text = "�N�٤M" Then
            If TextEdit1.Text = "" Then
                MsgBox("�٤M�H���ର��!")
                CheckSave = False
                Exit Function
            End If


            If TxtReReturn.Text = "" Then
                MsgBox("�N�٤M�H���ର��!")
                CheckSave = False
                Exit Function
            End If
        End If

        If ButtonEdit2.EditValue = "" Then
            strDPTID = ""
            MsgBox("�٤M�H�������ର��!")

            If LabMsg.Text = "�N�٤M" Then

            End If
        End If



        If txtWH.EditValue = "" Then
            MsgBox("�ܮw�s�����ର��!")
            CheckSave = False
            Exit Function
        End If

        If TextEdit1.Text = "" Then
            MsgBox("�L��d�H��!")
            CheckSave = False
            Exit Function
        End If


        If ds.Tables("Return").Rows.Count <= 0 Then
            MsgBox("�L�٤M�H��!")
            CheckSave = False
            Exit Function
        End If

        Dim i As Integer
        For i = 0 To ds.Tables("Return").Rows.Count - 1
            Dim BroDoub As Double
            Dim rcA As New WareBorrowReturnControl
            Dim RcAl As New List(Of WareBorrowReturnInfo)

            RcAl = rcA.WareBorrowReturn_Sum("�ɤM", ds.Tables("Return").Rows(i)("M_Code").ToString, TextEdit1.Text, Nothing)

            If RcAl.Count <= 0 Then
                BroDoub = 0
            Else
                BroDoub = RcAl(0).SumNO_ReQty
            End If

            If BroDoub < ds.Tables("Return").Rows(i)("Qty") Then
                MsgBox("[" & ds.Tables("Return").Rows(i)("M_Name").ToString & "] ,�٤M�ƶq�j���٤M�ƶq,����O�s!")
                CheckSave = False
                Exit Function
            End If
        Next

    End Function


    Function GetWR_ID() As String
        '�ͦ��spm
        Dim pm As New WareBorrowReturnControl
        Dim pi As New WareBorrowReturnInfo
        Dim ndate As String
        ndate = "WR" + Format(Now(), "yyMM")
        pi = pm.WareBorrowReturn_GetID(ndate)
        If pi Is Nothing Then
            GetWR_ID = ndate + "00001"
        Else
            GetWR_ID = ndate + Mid((CInt(Mid(pi.WB_NO, 7)) + 100001), 2)
        End If
    End Function

    Function GetWR_NUM() As String
        '�ͦ��spS
        Dim pm1 As New WareBorrowReturnControl
        Dim pi1 As New WareBorrowReturnInfo
        Dim ndate As String
        ndate = "WR"
        pi1 = pm1.WareBorrowReturn_GetNUM(ndate)
        If pi1 Is Nothing Then
            GetWR_NUM = "WR000000001"
        Else
            GetWR_NUM = "WR" & Mid((CInt(Mid(pi1.WB_NUM, 3)) + 1000000001), 2)
        End If
    End Function

    Private Sub sk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sk.Click

        If LabMsg.Text = "�N�٤M" Then
            If TextEdit1.Text = "" Then
                MsgBox("�п�J�٤M�H�Z,�A[��d]!")
                TextEdit1.Select()
                Exit Sub
            End If

            Get_PerNo() '���J�٤M�H �H��

            Dim RRstr As String
            RRstr = ReadCard()

            If RRstr <> "" Then
                If InStr(RRstr, "-", CompareMethod.Text) > 0 Then
                    TxtReReturn.Text = Mid(RRstr, 1, InStr(RRstr, "-", CompareMethod.Text) - 1)
                    LabReReturn.Text = Mid(RRstr, InStr(RRstr, "-", CompareMethod.Text) + 1, Len(RRstr) - InStr(RRstr, "-", CompareMethod.Text))
                End If
            End If

            cmdSave.Text = "�T�w"
            cmdSave.Enabled = True
            TextEdit1.Select()
            ' TextEdit1.Text = "00000001"
        Else

            TextEdit1.Text = ReadCard1() 'Ū���d��
            'TextEdit1.Text = "00000001"
            Dim wulc As New WhiteUserListController
            Dim wuliL As New List(Of WhiteuserListInfo)
            wuliL = wulc.WhiteUserList_GetList(TextEdit1.Text, Mid(strWHID, 1, 3))
            If wuliL.Count = 0 Then
                MsgBox("����d�ΫD�k�d��")
                Exit Sub
            Else
                cmdSave.Text = "�T�w"
                cmdSave.Enabled = True
            End If

            Label4.Text = wuliL.Item(0).W_UserName
            strDPTID = wuliL.Item(0).DPT_ID
            ButtonEdit2.Text = wuliL.Item(0).DPT_Name
            '-------------------------------------------------------------------------
            ButtonEdit2.Enabled = False
            TextEdit1.Enabled = False
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckSave() = True Then
            SaveDataNew()
            cmdSave.Enabled = False
            cmdSave.Visible = False
            PrintButton.Visible = True
        End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub popWareReturnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareReturnAdd.Click
        If TextEdit1.Text = "" Then
            MsgBox("�٤M�H�H�����ର��!")
            TextEdit1.Focus()
            Exit Sub
        End If

        tempValue2 = TextEdit1.Text

        Dim fr As New frmWareReturnLoad
        fr.ShowDialog()

        If tempValue = "Y" Then
        Else
            Exit Sub
        End If

        Dim K As Integer
        For K = 0 To fr.DSA.Tables("ReturnLoad").Rows.Count - 1

        

            If fr.DSA.Tables("ReturnLoad").Rows(K)("GOIN") = True Then

                Dim _M_Unit As String = ""
                Dim _M_Name As String = ""
                Dim _M_Gauge As String = ""
                Dim _Qty As String = ""

                ''�[�P�_�[�J�F���_�s�X--------------------------------
                If ds.Tables("Return").Rows.Count > 0 Then
                    Dim X As Integer
                    For X = 0 To ds.Tables("Return").Rows.Count - 1
                        If fr.DSA.Tables("ReturnLoad").Rows(K)("M_Code").ToString = ds.Tables("Return").Rows(X)("M_Code").ToString Then
                            MsgBox(ds.Tables("Return").Rows(X)("M_Code").ToString & ",�M��s�X�w�[�J!")
                            GoTo AA
                        End If
                    Next
                End If


               

                ''-------------------------------------------------------------------
                Dim row As DataRow
                row = ds.Tables("Return").NewRow
                row("M_Code") = fr.DSA.Tables("ReturnLoad").Rows(K)("M_Code").ToString
                row("M_Name") = fr.DSA.Tables("ReturnLoad").Rows(K)("M_Name").ToString

                row("M_Gauge") = fr.DSA.Tables("ReturnLoad").Rows(K)("M_Gauge").ToString
                row("M_Unit") = fr.DSA.Tables("ReturnLoad").Rows(K)("M_Unit").ToString
                row("Qty") = fr.DSA.Tables("ReturnLoad").Rows(K)("Qty")
                row("NoReturnQty") = fr.DSA.Tables("ReturnLoad").Rows(K)("Qty")
                ds.Tables("Return").Rows.Add(row)

                _M_Unit = fr.DSA.Tables("ReturnLoad").Rows(K)("M_Unit").ToString
                _M_Name = fr.DSA.Tables("ReturnLoad").Rows(K)("M_Name").ToString
                _M_Gauge = fr.DSA.Tables("ReturnLoad").Rows(K)("M_Gauge").ToString
                _Qty = fr.DSA.Tables("ReturnLoad").Rows(K)("Qty").ToString


                ''-------------------------------------------------------------------
AA:
                GridView1.FocusedRowHandle = K

                '@ 2013/6/11 �K�[ �s�W�����ƫH���bLCD��ܫ̤W���
                If isShowLCD = True Then   '������v���ɤ~�i���
                    LoadPingMU("�ټ˿�M", "�W�١G" & _M_Name, "�W��G" & _M_Gauge, "�ƶq�G" & _Qty & _M_Unit)
                End If

            End If
        Next

        fr.Dispose()
    End Sub


    Private Sub TextEdit1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextEdit1.KeyDown

        If LabMsg.Text = "�N�٤M" Then
            If e.KeyCode = Keys.Enter Then
                Get_PerNo()
            End If
        End If
    End Sub

    Sub Get_PerNo()
        Dim wulc As New WhiteUserListController
        Dim wuliL As New List(Of WhiteuserListInfo)
        wuliL = wulc.WhiteUserList_GetList(TextEdit1.Text, Mid(strWHID, 1, 3))

        If wuliL.Count > 0 Then '�զW�椤�d�ߤ���ɡA�d�̦߳Z�@���O��
            Label4.Text = wuliL.Item(0).W_UserName
            strDPTID = wuliL.Item(0).DPT_ID
            ButtonEdit2.Text = wuliL.Item(0).DPT_Name
        Else
            Dim wbt1 As New WareBorrowReturnControl
            Dim wbl1 As New List(Of WareBorrowReturnInfo)

            wbl1 = wbt1.WareBorrowReturn_GetList(Nothing, Nothing, "�ɤM", Nothing, Nothing, Nothing, TextEdit1.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Dim dCount As Double
            dCount = wbl1.Count

            If dCount > 0 Then
                Label4.Text = wbl1(dCount - 1).WB_PerName
                strDPTID = wbl1(dCount - 1).DPT_ID
                ButtonEdit2.Text = wbl1(dCount - 1).DPT_Name
            End If
        End If
    End Sub


    Private Sub PrintButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintButton.Click

        Dim ds As New DataSet
        Dim strA As String

        Dim ltc As New CollectionToDataSet
        strA = txtWBID.Text

        Dim bc As New WareBorrowReturnControl
        ltc.CollToDataSet(ds, "WareBorrowReturn", bc.WareBorrowReturn_GetList(Nothing, strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        PreviewRPT(ds, "rptWareReturn", "�٤M��", True, True)
        ltc = Nothing
    End Sub

    Private Sub popWareOutDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popWareOutDel.Click
        If ds.Tables("Return").Rows.Count = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "M_Code")
        ds.Tables("Return").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    '@ 2013/6/11 �K�[ �J�I����ܮɡALCD�̹���ܵJ�I�檫�ƫH��
    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If isShowLCD = True Then   '������v���ɤ~�i���
            LoadPingMU("�ټ˿�M", "�W�١G" & ds.Tables("Return").Rows(GridView1.FocusedRowHandle)("M_Name"), "�W��G" & ds.Tables("Return").Rows(GridView1.FocusedRowHandle)("M_Gauge"), "�ƶq�G" & ds.Tables("Return").Rows(GridView1.FocusedRowHandle)("Qty") & ds.Tables("Return").Rows(GridView1.FocusedRowHandle)("M_Unit"))
        End If
    End Sub

    '@ 2013/6/11 �ƶq�ק�ɡALCD�̹����s��ܪ��ƫH��
    Private Sub RepositoryItemCalcEdit1_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemCalcEdit1.EditValueChanged
        If isShowLCD = True Then   '������v���ɤ~�i���
            LoadPingMU("�ټ˿�M", "�W�١G" & ds.Tables("Return").Rows(GridView1.FocusedRowHandle)("M_Name"), "�W��G" & ds.Tables("Return").Rows(GridView1.FocusedRowHandle)("M_Gauge"), "�ƶq�G" & CDbl(sender.text) & ds.Tables("Return").Rows(GridView1.FocusedRowHandle)("M_Unit"))
        End If
    End Sub
End Class