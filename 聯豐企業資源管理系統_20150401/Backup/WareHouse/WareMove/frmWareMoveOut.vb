Imports LFERP.Library.WareHouse.WareMove
Imports LFERP.Library.WareHouse.WareInventory
Imports LFERP.Library.Purchase.SharePurchase
Imports LFERP.Library.WareHouse
Imports LFERP.Library.Purchase.Acceptance


Public Class frmWareMoveOut
    Dim ds As New DataSet
    Dim TorF As Boolean
    Dim oldCheck As Boolean
    Public isBarCode As Boolean '�P�_�O�_�O���X���y

    Private Sub frmWareMoveOut_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTables()
        Label4.Text = MTypeName
        MTypeName = Nothing

        If isBarCode = True Then
            Label24.Visible = True
            txtM_Code.Visible = True
            LabelControl1.Visible = True
            popWareMoveBarAdd.Visible = False
            txtM_Code.Focus()
        End If

        Select Case Label4.Text

            Case "WareMoveAddEdit"
                '�s�W�ק��
                Select Case Edit
                    Case False
                        OutWH.Text = tempValue2
                        If tempValue2 = "" Then
                            MsgBox("�Х���ܬ������ܧO")
                            Exit Sub
                        End If
                        txtWH.Enabled = True
                        DateEdit1.Enabled = False
                        DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")
                        Label1.Text = "�ռ���--�s�W"
                    Case True
                        If LoadData(tempValue) = False Then Exit Sub
                        Label1.Text = "�ռ���--�ק�"
                        '  Label1.Text = tempValue
                End Select
            Case "AcceptanceMoveOut"
                OutWH.Text = "W0102"
                txtWH.Enabled = True
                DateEdit1.Enabled = False
                DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")
                LoadAcceptanceData(tempValue2)
                tempValue2 = Nothing

            Case "WareMoveIn"
                '���ƽT�{��
                Label1.Text = "�ռ���--���ƽT�{"

                If LoadData(tempValue) = False Then Exit Sub
                GridView2.Columns.Item("MV_Qty").OptionsColumn.ReadOnly = True
                GridView2.Columns.Item("MV_Property").OptionsColumn.ReadOnly = True
                GridView2.Columns.Item("MV_Remark").OptionsColumn.ReadOnly = True
                popWareMoveOutAdd.Enabled = False
                popWareMoveOutDel.Enabled = False
                CheckEdit3.Enabled = True
            Case "WareMoveView"
                '�d�ݮ�
                If LoadData(tempValue) = False Then Exit Sub
                GridView2.Columns.Item("MV_Qty").OptionsColumn.ReadOnly = True
                GridView2.Columns.Item("MV_Property").OptionsColumn.ReadOnly = True
                GridView2.Columns.Item("MV_Remark").OptionsColumn.ReadOnly = True
                popWareMoveOutAdd.Enabled = False
                popWareMoveOutDel.Enabled = False
                cmdSave.Enabled = False
                Label1.Text = "�ռ���--�d��"
            Case "WareMoveCheck"
                '�f�֮�
                If LoadData(tempValue) = False Then Exit Sub
                'CheckEdit2.Enabled = True
                'MemoEdit1.Enabled = True

                If CheckEdit2.Checked = True Then
                    TorF = True
                Else
                    TorF = False
                End If


                Panel1.Enabled = True
                GridView2.Columns.Item("MV_Qty").OptionsColumn.ReadOnly = True
                GridView2.Columns.Item("MV_Property").OptionsColumn.ReadOnly = True
                GridView2.Columns.Item("MV_Remark").OptionsColumn.ReadOnly = True
                popWareMoveOutAdd.Enabled = False
                popWareMoveOutDel.Enabled = False

                Label1.Text = "�ռ���--�f��"
        End Select

        tempValue = ""

    End Sub

    Sub CreateTables()
        '�إ��禬��  Acceptance
        ds.Tables.Clear()
        With ds.Tables.Add("WareMove")
            .Columns.Add("MV_NO", GetType(String))
            .Columns.Add("MV_Number", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("MV_Qty", GetType(String))
            .Columns.Add("MV_Date", GetType(String))
            .Columns.Add("OS_BatchID", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("DepotNO", GetType(String))
            .Columns.Add("MV_ChangeDepotNO", GetType(String))
            .Columns.Add("MV_InOrOut", GetType(String))
            .Columns.Add("MV_Remark", GetType(String))
            .Columns.Add("MV_Ack", GetType(String))
            .Columns.Add("MV_OutAction", GetType(String))
            .Columns.Add("MV_InAction", GetType(String))
            .Columns.Add("MV_Property", GetType(String))
            .Columns.Add("MV_Check", GetType(String))
            .Columns.Add("MV_CheckAction", GetType(String))
            .Columns.Add("MV_CheckType", GetType(String))
            .Columns.Add("MV_ChcekRemark", GetType(String))
            .Columns.Add("MV_InActionName", GetType(Boolean))
            .Columns.Add("MV_OutActionName", GetType(String))
            .Columns.Add("MV_CheckActionName", GetType(String))

            .Columns.Add("WI_Qty", GetType(Double))
            .Columns.Add("WI_SafeQty", GetType(Double))
        End With


        '�ЫاR���ƾڪ�
        With ds.Tables.Add("DelDataWareMove")
            .Columns.Add("MV_Number", GetType(String))
        End With

        GridControl1.DataSource = ds.Tables("WareMove")

    End Sub

    Private Sub popWareMoveOutAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popWareMoveOutAdd.Click
        tempCode = ""
        tempValue5 = OutWH.Text
        tempValue6 = "�ܮw�޲z"
        frmBOMSelect.ShowDialog()
        '�W�[�O��
        If tempCode = "" Then

            Exit Sub
        Else
            AddRow(tempCode, 0)
        End If
    End Sub

    Sub AddRow(ByVal strCode As String, ByVal Qty As Double)
        If strCode = "" Then
        Else
            Dim i As Integer
            For i = 0 To ds.Tables("WareMove").Rows.Count - 1
                If strCode = ds.Tables("WareMove").Rows(i)("M_Code") Then
                    If isBarCode = True Then
                        ds.Tables("WareMove").Rows(i)("MV_Qty") = ds.Tables("WareMove").Rows(i)("MV_Qty") + 1
                        GridView2.FocusedRowHandle = i
                        Exit Sub
                    Else
                        MsgBox("�@�i�椣���\�����_���ƽs�X....")
                        Exit Sub
                    End If
                End If
            Next
            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)
            If objInfo Is Nothing Then
                MsgBox("���s�b�����ƽs�X�H��!")
                Exit Sub
            End If
            Dim row As DataRow
            row = ds.Tables("WareMove").NewRow
            'CodeSubData.Tables("CodeSub").NewRow()

            row("M_Code") = objInfo.M_Code
            row("MV_Qty") = Qty
            row("M_Name") = objInfo.M_Name
            row("M_Unit") = objInfo.M_Unit
            row("M_Gauge") = objInfo.M_Gauge
            '     row("OS_BatchID") = objInfo.m
            row("MV_Property") = "�����ಾ"

            '@2013/1/28 �K�[ ��ܮw�s
            Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            wi1 = wc1.WareInventory_GetSub(strCode, OutWH.Text)

            If wi1 Is Nothing Then
                row("WI_SafeQty") = 0
                row("WI_Qty") = 0
            Else
                row("WI_SafeQty") = wi1.WI_SafeQty
                row("WI_Qty") = wi1.WI_Qty
            End If

            ds.Tables("WareMove").Rows.Add(row)
            GridView2.MoveLast()


        End If
    End Sub

    Sub LoadAcceptanceData(ByVal AcceptanceNO As String)
        Dim objList As New List(Of AcceptanceInfo)
        Dim oc As New AcceptanceController
        objList = oc.Acceptance_GetList(Nothing, AcceptanceNO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If objList.Count = 0 Then
            Exit Sub
        Else
            Dim i As Integer
            For i = 0 To objList.Count - 1
                Dim row As DataRow
                row = ds.Tables("WareMove").NewRow

                row("M_Code") = objList(i).M_Code
                row("M_Name") = objList(i).M_Name
                row("M_Gauge") = objList(i).M_Gauge
                row("M_Unit") = objList(i).M_Unit
                row("MV_Qty") = objList(i).A_Qty
                row("OS_BatchID") = objList(i).OS_BatchID
                row("PM_M_Code") = objList(i).PM_M_Code
                row("MV_Property") = "�����ಾ"

                ds.Tables("WareMove").Rows.Add(row)
                GridView2.MoveLast()
            Next
        End If


    End Sub

    Private Sub txtWH_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWH.ButtonClick

        frmWareHouseSelect.SelectWareID = ""
        tempValue3 = "500306"
        tempCode = "���ƭܮw"
        frmWareHouseSelect.ShowDialog()

        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            txtWH.Text = frmWareHouseSelect.SelectWareID
        End If

    End Sub

    Function GetMV_NO() As String
        ' ''�ͦ��s���ռ��渹
        'Dim str1, str2 As String
        'Dim ac As New WareMoveController
        'Dim ai As List(Of WareMoveInfo)
        'str1 = Mid(Year(Now), 3)
        'If CInt(Month(Now)) < 10 Then
        '    str2 = "0" & Month(Now)
        'Else
        '    str2 = Month(Now)
        'End If

        'Dim Stra As String
        'Stra = "MV" & str1 & str2

        'ai = ac.WareMove_GetList(Stra, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "2")
        'If ai.Count = 0 Then
        '    GetMV_NO = "MV" & str1 & str2 & "00001"
        'Else
        '    GetMV_NO = "MV" & str1 & str2 & Mid((CInt(Mid(ai.Item(0).MV_NO, 7)) + 100001), 2)
        'End If

        Dim str1 As String
        Dim ac As New WareMoveController
        Dim ai As New List(Of WareMoveInfo)
        str1 = Format(Now, "yyMM")

        Dim Stra As String
        Stra = "MV" & str1

        ai = ac.WareMove_GetNO(Stra)
        If ai.Count = 0 Then
            GetMV_NO = "MV" & str1 & "00001"
        Else
            GetMV_NO = "MV" & str1 & Mid((CInt(Mid(ai.Item(0).MV_NO, 7)) + 100001), 2)
        End If


    End Function

    Function GetMV_Number()
        '�ͦ��s���ռ��s��
        'Dim str1, str2 As String
        'Dim ac As New WareMoveController
        'Dim ai As List(Of WareMoveInfo)
        'str1 = Mid(Year(Now), 3)
        'If CInt(Month(Now)) < 10 Then
        '    str2 = "0" & Month(Now)
        'Else
        '    str2 = Month(Now)
        'End If

        'Dim Stra As String
        'Stra = str1 & str2

        'ai = ac.WareMove_GetList(Stra, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "2")
        'If ai.Count = 0 Then
        '    GetMV_Number = str1 & str2 & "00001"
        'Else
        '    GetMV_Number = str1 & str2 & Mid((CInt(Mid(ai.Item(0).MV_Number, 5)) + 100001), 2)  
        'End If

        Dim Stra As String
        Dim ac As New WareMoveController
        Dim ai As New List(Of WareMoveInfo)
        
        Stra = Format(Now, "yyMM")

        ai = ac.WareMove_GetNumber(Stra)
        If ai.Count = 0 Then
            GetMV_Number = Stra & "00001"
        Else
            GetMV_Number = Stra & Mid((CInt(Mid(ai.Item(0).MV_Number, 5)) + 100001), 2)
        End If


    End Function


    Sub SaveNew()
        Dim i As Integer
        Dim MV_NO As String
        MV_NO = GetMV_NO()

        For i = 0 To ds.Tables("WareMove").Rows.Count - 1
            Dim MV_Number As String
            MV_Number = GetMV_Number()

            Dim ac As New WareMoveController
            Dim ai As New WareMoveInfo

            ai.MV_NO = MV_NO
            ai.MV_Number = MV_Number
            ai.M_Code = ds.Tables("WareMove").Rows(i)("M_Code")
            ai.M_Name = ds.Tables("WareMove").Rows(i)("M_Name")
            ai.M_Gauge = ds.Tables("WareMove").Rows(i)("M_Gauge")
            ai.M_Unit = ds.Tables("WareMove").Rows(i)("M_Unit")

            If IsDBNull(ds.Tables("WareMove").Rows(i)("MV_Qty")) Then
                MsgBox("�ƶq���ର��,�п�J�ƶq!")
                Exit Sub
            Else
                ai.MV_Qty = CDbl(ds.Tables("WareMove").Rows(i)("MV_Qty"))
            End If

            ai.MV_Date = DateEdit1.Text


            If IsDBNull(ds.Tables("WareMove").Rows(i)("OS_BatchID")) Then
                ai.OS_BatchID = Nothing
            Else
                ai.OS_BatchID = ds.Tables("WareMove").Rows(i)("OS_BatchID")
            End If

            If IsDBNull(ds.Tables("WareMove").Rows(i)("PM_M_Code")) Then
                ai.PM_M_Code = Nothing
            Else
                ai.PM_M_Code = ds.Tables("WareMove").Rows(i)("PM_M_Code")
            End If

            ai.DepotNO = OutWH.Text
            ai.MV_ChangeDepotNO = txtWH.Text

            If IsDBNull(ds.Tables("WareMove").Rows(i)("MV_Remark")) Then
                ai.MV_Remark = Nothing
            Else
                ai.MV_Remark = ds.Tables("WareMove").Rows(i)("MV_Remark")
            End If

            ai.MV_Property = ds.Tables("WareMove").Rows(i)("MV_Property")
            ai.MV_OutAction = InUserID

            ac.WareMove_Add(ai)
        Next
        MsgBox("�w�O�s,�渹: " & MV_NO & " ")
        Me.Close()

    End Sub

    Sub SaveUpdate()
        Dim i As Integer


        For i = 0 To ds.Tables("WareMove").Rows.Count - 1
            If Not IsDBNull(ds.Tables("WareMove").Rows(i)("MV_Number")) Then
                '�p�G���O�ק�


                Dim ac As New WareMoveController
                Dim ai As New WareMoveInfo


                ai.MV_Number = ds.Tables("WareMove").Rows(i)("MV_Number")

                If IsDBNull(ds.Tables("WareMove").Rows(i)("MV_Qty")) Then
                    MsgBox("�ƶq���ର��,�п�J�ƶq!")
                    Exit Sub
                Else
                    ai.MV_Qty = CDbl(ds.Tables("WareMove").Rows(i)("MV_Qty"))
                End If

                ai.MV_ChangeDepotNO = txtWH.Text

                If IsDBNull(ds.Tables("WareMove").Rows(i)("MV_Remark")) Then
                    ai.MV_Remark = Nothing
                Else
                    ai.MV_Remark = ds.Tables("WareMove").Rows(i)("MV_Remark")
                End If

                ai.MV_Property = ds.Tables("WareMove").Rows(i)("MV_Property")
                ai.MV_OutAction = InUserID

                ac.WareMove_Update(ai)

            End If

            If IsDBNull(ds.Tables("WareMove").Rows(i)("MV_Number")) Then
                '�p�G���O�s�W
                Dim MV_Number As String
                MV_Number = GetMV_Number()
                Dim ac As New WareMoveController
                Dim ai As New WareMoveInfo

                ai.MV_NO = txtQuoID.Text
                ai.MV_Number = MV_Number
                ai.M_Code = ds.Tables("WareMove").Rows(i)("M_Code")
                ai.M_Name = ds.Tables("WareMove").Rows(i)("M_Name")
                ai.M_Gauge = ds.Tables("WareMove").Rows(i)("M_Gauge")
                ai.M_Unit = ds.Tables("WareMove").Rows(i)("M_Unit")

                If IsDBNull(ds.Tables("WareMove").Rows(i)("MV_Qty")) Then
                    MsgBox("�ƶq���ର��,�п�J�ƶq!")
                    Exit Sub
                Else
                    ai.MV_Qty = CDbl(ds.Tables("WareMove").Rows(i)("MV_Qty"))
                End If

                ai.MV_Date = DateEdit1.Text


                If IsDBNull(ds.Tables("WareMove").Rows(i)("OS_BatchID")) Then
                    ai.OS_BatchID = Nothing
                Else
                    ai.OS_BatchID = ds.Tables("WareMove").Rows(i)("OS_BatchID")
                End If

                If IsDBNull(ds.Tables("WareMove").Rows(i)("PM_M_Code")) Then
                    ai.PM_M_Code = Nothing
                Else
                    ai.PM_M_Code = ds.Tables("WareMove").Rows(i)("PM_M_Code")
                End If

                ai.DepotNO = OutWH.Text
                ai.MV_ChangeDepotNO = txtWH.Text

                If IsDBNull(ds.Tables("WareMove").Rows(i)("MV_Remark")) Then
                    ai.MV_Remark = Nothing
                Else
                    ai.MV_Remark = ds.Tables("WareMove").Rows(i)("MV_Remark")
                End If
                ai.MV_Property = ds.Tables("WareMove").Rows(i)("MV_Property")
                ai.MV_OutAction = InUserID
                ac.WareMove_Add(ai)
            End If
        Next
        '��s�R�����O��
        If ds.Tables("DelDataWareMove").Rows.Count > 0 Then
            For i = 0 To ds.Tables("DelDataWareMove").Rows.Count - 1
                Dim odc As New WareMoveController
                Dim odi As New WareMoveInfo
                odi.MV_Number = ds.Tables("DelDataWareMove").Rows(i)("MV_Number")
                odc.WareMove_DelNumber(odi)
            Next i
        End If

        MsgBox("�w�ק�,�渹: " & txtQuoID.Text & " ")
        Me.Close()

    End Sub

    Sub SaveCheck()

        Dim a As New WareMoveController
        Dim b As New WareMoveInfo
        b.MV_NO = txtQuoID.Text
        If CheckEdit2.Checked = True Then
            b.MV_Check = True
        Else
            b.MV_Check = False
        End If

        b.MV_AckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        b.MV_CheckRemark = MemoEdit1.Text
        b.MV_CheckAction = InUserID
        a.WareMove_UpdateCheck(b)

        Dim i As Integer
        For i = 0 To ds.Tables("WareMove").Rows.Count - 1

            Dim wic As New SharePurchaseController
            Dim wiiIn As New SharePurchaseInfo
            Dim wiiOut As New SharePurchaseInfo

            wiiIn.M_Code = ds.Tables("WareMove").Rows(i)("M_Code").ToString
            wiiIn.WH_ID = txtWH.Text


            If TorF = True And CheckEdit2.Checked = False Then
                wiiIn.WI_Qty = "-" & CDbl(ds.Tables("WareMove").Rows(i)("MV_Qty"))
                wiiOut.WI_Qty = CDbl(ds.Tables("WareMove").Rows(i)("MV_Qty"))
            End If

            wiiOut.M_Code = ds.Tables("WareMove").Rows(i)("M_Code").ToString
            wiiOut.WH_ID = OutWH.Text
            If TorF = False And CheckEdit2.Checked = True Then
                wiiIn.WI_Qty = CDbl(ds.Tables("WareMove").Rows(i)("MV_Qty"))
                wiiOut.WI_Qty = "-" & CDbl(ds.Tables("WareMove").Rows(i)("MV_Qty"))
            End If

            wic.UpdateWareInventory_WIQty(wiiIn)
            wic.UpdateWareInventory_WIQty(wiiOut)

            '-----------------------------------------------------------------------------------------------
            Dim wi1 As List(Of WareInventoryInfo)
            Dim wi2 As List(Of WareInventoryInfo)
            Dim wiic As New WareInventoryMTController  '�ܮw�w�s
            Dim Qty1, Qty2 As Double
            '--------------------------------------------------------------
            wi1 = wiic.WareInventory_GetList3(ds.Tables("WareMove").Rows(i)("M_Code"), OutWH.Text, "True")  '�o�X�ܮw
            If wi1.Count = 0 Then
                Qty1 = 0
            Else
                Qty1 = wi1(0).WI_Qty '��e�ܮw�ƶq              
            End If

            Dim wmi1 As New WareMoveInfo
            wmi1.MV_NO = txtQuoID.Text
            wmi1.DepotNO = OutWH.Text
            wmi1.MV_ChangeDepotNO = txtWH.Text
            wmi1.M_Code = ds.Tables("WareMove").Rows(i)("M_Code")
            wmi1.MV_EndQty = Qty1
            a.WareMove_UpdateOutEndQty(wmi1)

            '--------------------------------------------------------------
            wi2 = wiic.WareInventory_GetList3(ds.Tables("WareMove").Rows(i)("M_Code"), txtWH.Text, "True") '���J�ܮw
            If wi2.Count = 0 Then
                Qty2 = 0
            Else
                Qty2 = wi2(0).WI_Qty '��e�ܮw�ƶq
            End If

            Dim wmi2 As New WareMoveInfo

            wmi2.MV_NO = txtQuoID.Text
            wmi2.DepotNO = txtWH.Text
            wmi2.MV_ChangeDepotNO = OutWH.Text
            wmi2.M_Code = ds.Tables("WareMove").Rows(i)("M_Code")
            wmi2.MV_EndQty = Qty2

            a.WareMove_UpdateInEndQty(wmi2)

            '-----------------------------------------------------------------------------------------------

        Next


        MsgBox("�f�֪��A�w����!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        Me.Close()





    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Select Case Label4.Text

            Case "WareMoveAddEdit"
                '�s�W�ק��
                Select Case Edit
                    Case False
                        If CheckSave() = True Then
                            SaveNew()
                        End If
                    Case True
                        If CheckSave() = True Then
                            SaveUpdate()
                        End If
                End Select

            Case "AcceptanceMoveOut"
                SaveNew()

            Case "WareMoveIn"


                '2013-7-22
                If CheckPO() = False Then
                    Exit Sub
                End If


                If CheckSave() = True Then
                    SaveIn()
                End If
            Case "WareMoveCheck"
                '�f�֪��A���ܪ����p�U
                If TorF = True And CheckEdit2.Checked = True Then
                    Exit Sub
                    Me.Close()
                End If

                If TorF = False And CheckEdit2.Checked = False Then
                    Exit Sub
                    Me.Close()
                End If


             


                SaveCheck()
            Case ""

        End Select

    End Sub

    ''' <summary>
    ''' 2013-7-22 �d�ߥX �w�ռ��禬��
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckPO() As Boolean
        CheckPO = True
        Dim K As Integer

        For K = 0 To ds.Tables("WareMove").Rows.Count - 1
            If IsDBNull(ds.Tables("WareMove").Rows(K)("MV_Remark")) Then
            Else
                Dim mvc As New WareMoveController
                Dim mvl As New List(Of WareMoveInfo)

                mvl = mvc.WareMove_GetListRemark(ds.Tables("WareMove").Rows(K)("M_Code").ToString, ds.Tables("WareMove").Rows(K)("MV_Remark").ToString, txtWH.Text)
                If mvl.Count > 0 Then
                    If MsgBox(ds.Tables("WareMove").Rows(K)("MV_Remark") & "[" & ds.Tables("WareMove").Rows(K)("M_Name") & "]" & "���禬��w�ռ�,�O�_�~��?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Else
                        CheckPO = False
                        Exit Function
                    End If
                End If
            End If
        Next

    End Function



    Sub SaveIn()
        '���f�T�{
        'Dim i As Integer
        'For i = 0 To ds.Tables("WareMove").Rows.Count - 1
        '    Dim wmc As New WareMoveController
        '    Dim wmi As New WareMoveInfo
        '    wmi.MV_Number = ds.Tables("WareMove").Rows(i)("MV_Number").ToString
        '    wmi.MV_InAction = InUserID
        '    If CheckEdit3.Checked = True Then
        '        wmi.MV_Ack = True
        '    Else
        '        wmi.MV_Ack = False
        '    End If
        '    wmc.WareMove_UpdateAck(wmi)


        '    Dim wic As New SharePurchaseController
        '    Dim wiiIn As New SharePurchaseInfo
        '    Dim wiiOut As New SharePurchaseInfo

        '    If TorF = True And CheckEdit3.Checked = False Then
        '        wiiIn.WI_Qty = "-" & ds.Tables("WareMove").Rows(i)("MV_Qty").ToString
        '        wiiOut.WI_Qty = ds.Tables("WareMove").Rows(i)("MV_Qty").ToString
        '    End If
        '    If TorF = False And CheckEdit3.Checked = True Then
        '        wiiIn.WI_Qty = ds.Tables("WareMove").Rows(i)("MV_Qty").ToString
        '        wiiOut.WI_Qty = "-" & ds.Tables("WareMove").Rows(i)("MV_Qty").ToString
        '    End If

        '    wiiIn.M_Code = ds.Tables("WareMove").Rows(i)("M_Code").ToString
        '    wiiOut.M_Code = ds.Tables("WareMove").Rows(i)("M_Code").ToString
        '    wiiIn.WH_ID = txtWH.Text
        '    wiiOut.WH_ID = OutWH.Text
        '    wic.UpdateWareInventory_WIQty(wiiIn)
        '    wic.UpdateWareInventory_WIQty(wiiOut)

        'Next

        'MsgBox("���ƽT�{���A�w����", MsgBoxStyle.OkOnly)
        'Me.Close()


        Dim wmc As New WareMoveController
        Dim wmi As New WareMoveInfo
        wmi.MV_NO = txtQuoID.Text
        wmi.MV_InAction = InUserID

        If CheckEdit3.Checked = oldCheck Then
            MsgBox("�Х��ܧ�T�{���A!")
            Exit Sub
        End If
        If CheckEdit3.Checked = True Then
            wmi.MV_Ack = True
        Else
            wmi.MV_Ack = False
        End If
        wmc.WareMove_UpdateAck(wmi)

        MsgBox("���ƽT�{���A�w����", MsgBoxStyle.OkOnly)
        Me.Close()

    End Sub

    Function CheckSave() As Boolean
        CheckSave = True

        If ds.Tables("WareMove").Rows.Count = 0 Then
            MsgBox("�S���ƾ�,�L�k�O�s!")
            CheckSave = False
            Exit Function
        End If

        If txtWH.Text = "" Then
            MsgBox("�����ܮw���ର��!", MsgBoxStyle.OkOnly)
            CheckSave = False
            Exit Function
        End If

        If OutWH.Text = "" Then
            MsgBox("�o�ƭܮw���ର��!", MsgBoxStyle.OkOnly)
            CheckSave = False
            Exit Function
        End If

        If txtWH.Text = OutWH.Text Then
            MsgBox("�����M�o�X����O�P�@�ӭܧO!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            CheckSave = False
            Exit Function
        End If

        If DateEdit1.Text = "" Then
            MsgBox("������ର��!", MsgBoxStyle.OkOnly)
            CheckSave = False
            Exit Function
        End If


        Dim i As Integer
        For i = 0 To ds.Tables("WareMove").Rows.Count - 1

            If CDbl(ds.Tables("WareMove").Rows(i)("MV_Qty").ToString) = 0 Then
                MsgBox("�ռ��ƶq���ର0�I", MsgBoxStyle.OkOnly)
                CheckSave = False
            End If

            If ds.Tables("WareMove").Rows(i)("MV_Qty").ToString < 0 Then
                MsgBox("�ռ��ƶq���ର�t�ơI", MsgBoxStyle.OkOnly)
                CheckSave = False
            End If

            On Error GoTo A
            Dim wic As New WareInventoryMTController
            Dim wii As New WareInventoryInfo
            wii = wic.WareInventory_GetSub(ds.Tables("WareMove").Rows(i)("M_Code").ToString, OutWH.Text)

            Dim Qty As Double
            If wii Is Nothing Then
                MsgBox("��e�o�X�ܮw���s�b�����ƫH��")
                CheckSave = False
                Exit Function
            Else
                Qty = wii.WI_Qty
            End If

            If Qty < CDbl(ds.Tables("WareMove").Rows(i)("MV_Qty").ToString) Then
A:              MsgBox("�o�X�ܨS�����w�s�ήw�s����!")
                CheckSave = False
                Exit Function

            End If

            If ds.Tables("WareMove").Rows(i)("MV_Property").ToString = "" Then
                MsgBox("�ж�g�ռ������I", MsgBoxStyle.OkOnly)
                CheckSave = False
            End If

        Next
    End Function

    ''' <summary>
    ''' ���J�ռ���ƾ�
    ''' </summary>
    ''' <param name="MV_NO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadData(ByVal MV_NO As String) As Boolean


        LoadData = True
        Dim objList As New List(Of WareMoveInfo)
        Dim oc As New WareMoveController
        Try
            objList = oc.WareMove_GetList(MV_NO, Nothing, Nothing, Nothing, Nothing, "����", Nothing, Nothing, "2")
            If objList.Count = 0 Then
                MsgBox("�S���ƾ�")
                LoadData = False
                Exit Function
            End If

            txtQuoID.Text = objList(0).MV_NO
            txtWH.Text = objList(0).DepotNO
            OutWH.Text = objList(0).MV_ChangeDepotNO
            DateEdit1.Text = objList(0).MV_Date

            ds.Tables("WareMove").Rows.Clear()
            '           objList = oc.Acceptance_GetList(Nothing, A_AcceptanceNO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Dim i As Integer
            Dim row As DataRow
            For i = 0 To objList.Count - 1
                row = ds.Tables("WareMove").NewRow
                row("MV_Number") = objList(i).MV_Number
                row("M_Code") = objList(i).M_Code
                row("M_Unit") = objList(i).M_Unit
                row("M_Gauge") = objList(i).M_Gauge
                row("M_Name") = objList(i).M_Name


                row("OS_BatchID") = objList(i).OS_BatchID
                row("PM_M_Code") = objList(i).PM_M_Code

                row("MV_Qty") = objList(i).MV_Qty
                row("MV_Property") = objList(i).MV_Property

                row("MV_Remark") = objList(i).MV_Remark

                '@2013/1/28 �K�[ ��ܮw�s
                Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi1 = wc1.WareInventory_GetSub(objList(i).M_Code, OutWH.Text)

                If wi1 Is Nothing Then
                    row("WI_SafeQty") = 0
                    row("WI_Qty") = 0
                Else
                    row("WI_SafeQty") = wi1.WI_SafeQty
                    row("WI_Qty") = wi1.WI_Qty
                End If

                ds.Tables("WareMove").Rows.Add(row)

                If objList(i).MV_Ack = False Then
                    CheckEdit3.Checked = False
                    oldCheck = False
                Else
                    CheckEdit3.Checked = True
                    oldCheck = True
                End If

                If objList(i).MV_Check = False Then
                    CheckEdit2.Checked = False
                Else
                    CheckEdit2.Checked = True
                End If



                Label3.Text = objList(i).MV_CheckAction
                MemoEdit1.Text = objList(i).MV_CheckRemark
            

            Next


        Catch ex As Exception
            LoadData = False
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub popWareMoveOutDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popWareMoveOutDel.Click

        If GridView2.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView2.GetRowCellDisplayText(ArrayToString(GridView2.GetSelectedRows()), "MV_Number")
        If DelTemp = "MV_Number" Then
        Else
            '�b�R�����W�[�Q�R�����O��
            Dim row As DataRow = ds.Tables("DelDataWareMove").NewRow
            row("MV_Number") = DelTemp
            ds.Tables("DelDataWareMove").Rows.Add(row)
        End If
        ds.Tables("WareMove").Rows.RemoveAt(CInt(ArrayToString(GridView2.GetSelectedRows())))
    End Sub

    Private Sub cmdBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBarCode.Click
        '
        ' PrintBar() �[�p�׽s��
        Dim i, m As Integer
        For i = 0 To ds.Tables("WareMove").Rows.Count - 1
            Dim str1, str2, str3, str4, str5, str6, str7 As String

            str1 = ds.Tables("WareMove").Rows(i)("M_Code")
            str2 = ds.Tables("WareMove").Rows(i)("M_Name")
            str3 = ds.Tables("WareMove").Rows(i)("PM_M_Code")
            str4 = ds.Tables("WareMove").Rows(i)("M_Unit")
            str5 = ds.Tables("WareMove").Rows(i)("MV_Qty")
            str6 = Format(DateEdit1.DateTime, "Short Date")
            str7 = ds.Tables("WareMove").Rows(i)("OS_BatchID")


            Dim whc As New WareHouseController
            Dim whiL As New List(Of WareHouseInfo)
            whiL = whc.WareHouse_GetList(Nothing)
            For m = 0 To whiL.Count - 1
                If whiL.Item(m).WH_ID = OutWH.Text Then
                    If whiL.Item(m).PrintBulk = "�j" Then
                        PrintBar(str1, str2, str3, str4, str5, str6, str7)
                    End If

                    If whiL.Item(m).PrintBulk = "�p" Then
                        PrintBar2(str1, str2)
                    End If
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub popWareMoveBarAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popWareMoveBarAdd.Click
        FrmBarCodeInput.ds.Tables.Clear()
        FrmBarCodeInput.ShowDialog()
        If FrmBarCodeInput.ds.Tables("BarCode").Rows.Count <> 0 Then
            Dim i As Integer
            For i = 0 To FrmBarCodeInput.ds.Tables("BarCode").Rows.Count - 1
                AddRow(FrmBarCodeInput.ds.Tables("BarCode").Rows(i)("M_Code"), FrmBarCodeInput.ds.Tables("BarCode").Rows(i)("WIP_Qty"))
            Next

        End If
    End Sub

    Private Sub popWareMoveAccAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveAccAdd.Click
        Dim i, n As Integer
        Dim myfrm As New frmRetrocedeSelect
        tempValue4 = "�ռ��@�~"
        myfrm.ShowDialog()
        If RefreshT = True Then
            Dim arr(n) As String
            arr = Split(tempValue, ",")
            n = Len(Replace(tempValue, ",", "," & "*")) - Len(tempValue)


            For i = 0 To n

                Dim pst As New Library.Purchase.Acceptance.AcceptanceController
                Dim psi As New Library.Purchase.Acceptance.AcceptanceInfo

                psi = pst.Acceptance_GetSub(arr(i))

                'cmdExit.Text = tempValue

                Dim row As DataRow = ds.Tables("WareMove").NewRow

                row("M_Code") = psi.M_Code
                row("M_Unit") = psi.M_Unit
                row("M_Name") = psi.M_Name
                row("M_Gauge") = psi.M_Gauge
                row("OS_BatchID") = psi.OS_BatchID
                row("PM_M_Code") = psi.PM_M_Code
                row("MV_Qty") = psi.A_Qty
                row("MV_Property") = "�����ಾ"

                ds.Tables("WareMove").Rows.Add(row)
                GridView1.MoveLast()

            Next
        End If
        tempValue = ""
        RefreshT = False
    End Sub

    Private Sub popWareMoveCenterAcc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveCenterAcc.Click
        Dim i, n As Integer
        Dim myfrm As New frmRetrocedeSelect
        tempValue4 = "�ռ��@�~-�����禬"
        myfrm.ShowDialog()
        If RefreshT = True Then
            Dim arr(n) As String
            arr = Split(tempValue, ",")
            n = Len(Replace(tempValue, ",", "," & "*")) - Len(tempValue)

            For i = 0 To n

                Dim wic As New LFERP.Library.WareHouse.WareInput.WareInputContraller
                Dim wii As New LFERP.Library.WareHouse.WareInput.WareInputInfo

                wii = wic.WareInput_GetNUM(arr(i))

                Dim j As Integer
                For j = 0 To ds.Tables("WareMove").Rows.Count - 1
                    If wii.M_Code = ds.Tables("WareMove").Rows(j)("M_Code") Then
                        MsgBox("�@�i�椣���\�����_���ƽs�X....")
                        Exit Sub
                    End If
                Next

                Dim row As DataRow = ds.Tables("WareMove").NewRow

                row("M_Code") = wii.M_Code
                row("M_Unit") = wii.M_Unit
                row("M_Name") = wii.M_Name
                row("M_Gauge") = wii.M_Gauge
                row("OS_BatchID") = wii.OS_BatchID

                'row("PM_M_Code") = wii.PM_M_Code
                row("MV_Qty") = wii.WIP_Qty
                row("MV_Property") = "�����ಾ"
                row("MV_Remark") = Mid(wii.WIP_Remark, 1, 12) '�O����e�J�w�渹�������ƪ����ʳ渹�O��



                '@2013/1/28 �K�[ ��ܮw�s
                Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi1 = wc1.WareInventory_GetSub(wii.M_Code, OutWH.Text)

                If wi1 Is Nothing Then
                    row("WI_SafeQty") = 0
                    row("WI_Qty") = 0
                Else
                    row("WI_SafeQty") = wi1.WI_SafeQty
                    row("WI_Qty") = wi1.WI_Qty
                End If

                ds.Tables("WareMove").Rows.Add(row)
                GridView1.MoveLast()

            Next
        End If
        tempValue = ""
        RefreshT = False
    End Sub

    '@ 2013/1/25 �K�[ ���U�^����եβK�[�ƾڦ�ƥ�,���X���y�����y�X�����X�۱a�^�����
    Private Sub txtM_Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            AddRow(txtM_Code.Text.Trim, 1)
            txtM_Code.Text = ""
        End If
    End Sub

    '@ 2013/1/25 �K�[
    Private Sub txtM_Code_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyUp
        If GridView2.RowCount <= 0 Then Exit Sub

        If e.KeyCode = Keys.Home Then       '���UHome��A�J�I��ƶq�椸����o�J�I
            GridView2.Focus()
            GridView2.FocusedColumn = GridView2.Columns("MV_Qty")
        End If

        If e.KeyCode = Keys.PageUp Then    '���UPageUp��A�J�I��ƶq�[�@
            ds.Tables("WareMove").Rows(GridView2.FocusedRowHandle)("MV_Qty") = ds.Tables("WareMove").Rows(GridView2.FocusedRowHandle)("MV_Qty") + 1
        End If

        If e.KeyCode = Keys.PageDown Then   '���UPageDown��A�J�I��ƶq��@
            ds.Tables("WareMove").Rows(GridView2.FocusedRowHandle)("MV_Qty") = ds.Tables("WareMove").Rows(GridView2.FocusedRowHandle)("MV_Qty") - 1
        End If

        If e.KeyCode = Keys.Up Then    '���U�V�W��V��A�J�I��W���@��A�åB�J�I���b�ƶq�椸�椤
            GridView2.FocusedRowHandle = GridView2.FocusedRowHandle - 1
            GridView2.Focus()
            GridView2.FocusedColumn = GridView2.Columns("MV_Qty")
        End If

        If e.KeyCode = Keys.Down Then   '���U�V�U��V��A�J�I��U���@��A�åB�J�I���b�ƶq�椸�椤
            GridView2.FocusedRowHandle = GridView2.FocusedRowHandle + 1
            GridView2.Focus()
            GridView2.FocusedColumn = GridView2.Columns("MV_Qty")
        End If
    End Sub

    '@ 2013/1/25 �K�[
    Private Sub GridView2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView2.KeyUp
        If e.KeyCode = Keys.Enter Then
            txtM_Code.Focus()
        End If
    End Sub

    Private Sub Label20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label20.Click
        MsgBox("�渹:" & GetMV_NO() & "�y����:" & GetMV_Number())
    End Sub
End Class