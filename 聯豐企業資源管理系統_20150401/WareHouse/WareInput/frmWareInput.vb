Imports LFERP.Library.WareHouse.WareInput
Imports LFERP.Library
Imports LFERP.Library.Purchase.SharePurchase
Imports LFERP.FileManager
Imports LFERP.SystemManager
Imports LFERP.Library.WareHouse
Imports System.Threading
Imports LFERP.Library.Shared
Imports LFERP.Library.Product
Imports LFERP.Library.ProductionKaiLiaoReturn

Public Class frmWareInput
    Dim ds As New DataSet
    Dim OldCheck As Boolean
    Dim strWHID As String
    Dim strDPTID As String
    Public isBarCode As Boolean '�P�_�O�_�O���X���y
    Dim strPM_M_Code As String


    Dim MsgBZ As String = ""

    Private Sub frmWareInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LabelMsg.Text = ""

        ''ToolStripBatchLoad �[�v��
        ''---------��q�s�W2013-5-11---------------------------------------------------------------
        ToolStripBatchLoad.Visible = False
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500111")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripBatchLoad.Visible = True
        End If
        ''------------------------------------------------------------------------

        Label3.Text = tempValue
        txtWIPID.EditValue = tempValue2
        tempValue = ""
        tempValue2 = ""

        CreateTables()
        txtWIPID.Enabled = False
        DateEdit1.Enabled = False

        If isBarCode = True Then
            Label24.Visible = True
            txtM_Code.Visible = True
            LabelControl1.Visible = True
            popWareInputBarAdd.Visible = False
            popWareInputBatchAdd.Visible = False
            txtM_Code.Select()
        End If

        '���m�s��d��
        Dim reset As New ResetPassWords.SetPassWords
        reset.SetPassWords()

        Select Case Label3.Text
            Case "�J�w��"
                If Edit = True Then
                    Me.Text = "�J�w��ק�"
                    loadedit(txtWIPID.EditValue)
                ElseIf Edit = False Then
                    Me.Text = "�J�w��s�W"
                    txtWIPID.EditValue = ""
                    DateEdit1.DateTime = Now
                    cbType.EditValue = "���`"
                    strWHID = tempValue3
                    txtWH.EditValue = tempValue4

                    CheckEdit1.Checked = True  '�q�{���w�f�֪��p(�K�[�ɪ����O�s--�f��)

                    tempValue3 = ""
                    tempValue4 = ""

                    WareInvent.VisibleIndex = 6
                End If

                XtraTabControl1.SelectedTabPage = XtraTabPage1
                'getenable(True, False)

            Case "�����f��"
                loadedit(txtWIPID.EditValue)
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                getenable(False, True)
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

            Case "�d��"
                loadedit(txtWIPID.EditValue)
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                getenable(False, False)
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
                cmdSave.Enabled = False
            Case "�_��"
                loadedit(txtWIPID.EditValue)
                XtraTabControl1.SelectedTabPage = XtraTabPage3
                getenable(False, True)
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            Case "�ק�ƪ`"

                loadedit(txtWIPID.EditValue)

                ButtonEdit2.Enabled = False
                cbType.Enabled = False
                OS_BatchID.OptionsColumn.ReadOnly = True
                WIP_Qty.OptionsColumn.ReadOnly = True

                XtraTabPage1.PageVisible = True
                XtraTabPage2.PageVisible = False
                XtraTabPage3.PageVisible = False
                XtraTabPage4.PageVisible = False


        End Select

        '�[����������
        GridFile.AutoGenerateColumns = False
        GridFile.RowHeadersWidth = 15
        Dim dt As New FileController
        GridFile.DataSource = dt.FileBond_GetList("5001", txtWIPID.EditValue, Nothing)
        GridFile.Refresh()

        XtraTabPage2.PageVisible = False  '���A�ݭn�f�ְO���H��


        CheckNeedCheckInput()

    End Sub

    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("WareInput")
            .Columns.Add("WIP_NUM", GetType(String))
            .Columns.Add("WIP_ID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("WIP_Qty", GetType(Double))
            .Columns.Add("OS_BatchID", GetType(String))
            .Columns.Add("WIP_Remark", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            'WareInvent
            .Columns.Add("WareInvent", GetType(Double))

            '
            .Columns.Add("RC_AutoID", GetType(String)) '�h�Ƴ�y����

        End With

        With ds.Tables.Add("DelDate")
            .Columns.Add("WIP_NUM", GetType(String))
            .Columns.Add("WIP_ID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
        End With

        Grid.DataSource = ds.Tables("WareInput")
    End Sub

    Sub AddRow(ByVal strCode As String, ByVal OS_BatchID As String)

        Dim row As DataRow
        row = ds.Tables("WareInput").NewRow

        If strCode = "" Then

        Else

            Dim i As Integer

            For i = 0 To ds.Tables("WareInput").Rows.Count - 1
                If strCode = ds.Tables("WareInput").Rows(i)("M_Code") Then
                    If isBarCode = True Then
                        ds.Tables("WareInput").Rows(i)("WIP_Qty") = ds.Tables("WareInput").Rows(i)("WIP_Qty") + 1
                        GridView1.FocusedRowHandle = i
                        Exit Sub
                    Else
                        If MsgBZ = "" Then
                            MsgBZ = "Y"
                            MsgBox("�@�i�椣���\�����_���ƽs�X....")
                        End If

                        Exit Sub
                    End If
                End If
            Next
            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)

            If objInfo Is Nothing Then
                MsgBox("��e���Ƥ��s�b�A�нT�{�����ƫH���I", 64, "����")
                Exit Sub
            End If

            If objInfo.M_IsEnabled = False Then  '�P�_��e���ƬO�_�i�� 2012-2-20�A���i�Τ����\�����I
                MsgBox("��e���Ƥ��i�ΡA�����\�J�w�I")
                Exit Sub
            End If

            ' row = ds.Tables("WareInput").NewRow
            'CodeSubData.Tables("CodeSub").NewRow()
            row("WIP_NUM") = Nothing
            row("WIP_ID") = Nothing


            row("M_Code") = objInfo.M_Code

            row("M_Name") = objInfo.M_Name

            row("PM_M_Code") = strPM_M_Code

            Dim unit As New LFERP.DataSetting.UnitController
            Dim unitinfo As List(Of LFERP.DataSetting.UnitInfo)

            unitinfo = unit.GetUnitList(objInfo.M_Unit)
            If unitinfo.Count > 0 Then
                row("M_Unit") = unitinfo(0).U_Name
            Else
                row("M_Unit") = ""
            End If

            '2013-11-01
            If GetWH_Remark(strWHID) = "NG" Then
                row("M_Unit") = "KG"
            End If


            row("M_Gauge") = objInfo.M_Gauge
            row("OS_BatchID") = OS_BatchID


            row("WIP_Qty") = 1

            ''�J�w����ܷ�e�w�s2013-2-27

            ''--------------------------------------------------------------------------
            If strWHID = "W1101" Then
                Dim pic As New ProductInventoryController
                Dim piiGet As List(Of ProductInventoryInfo)
                piiGet = pic.ProductInventory_GetList(strPM_M_Code, strCode, strWHID, Nothing)

                If piiGet.Count <= 0 Then
                    row("WareInvent") = 0
                Else
                    row("WareInvent") = piiGet(0).PI_Qty
                End If
            Else
                Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi1 = wc1.WareInventory_GetSub(strCode, strWHID)

                If wi1 Is Nothing Then
                    row("WareInvent") = 0
                Else
                    row("WareInvent") = wi1.WI_Qty
                End If
            End If

            ''--------------------------------------------------------------------------

            ds.Tables("WareInput").Rows.Add(row)
        End If
        GridView1.MoveLast()
    End Sub

    Function GetWH_Remark(ByVal strWH_ID As String) As String
        GetWH_Remark = ""
        Dim mt As New WareHouseController
        Dim mtl As New List(Of WareHouseInfo)
        mtl = mt.WareHouse_Get(strWH_ID)

        If mtl.Count > 0 Then
            GetWH_Remark = mtl(0).WH_Remark
        End If


    End Function


    Function AddRowPD(ByVal strCode As String, ByVal Qty As String) As String

        AddRowPD = ""

        Dim row As DataRow
        row = ds.Tables("WareInput").NewRow

        If strCode = "" Then

        Else

            Dim i As Integer

            For i = 0 To ds.Tables("WareInput").Rows.Count - 1
                If strCode = ds.Tables("WareInput").Rows(i)("M_Code") Then
                    ds.Tables("WareInput").Rows(i)("WIP_Qty") = ds.Tables("WareInput").Rows(i)("WIP_Qty") + Val(Qty)
                    GridView1.FocusedRowHandle = i
                    Exit Function
                End If
            Next

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)

            If objInfo Is Nothing Then
                AddRowPD = AddRowPD & strCode & "��e���Ƥ��s�b�A�нT�{�����ƫH���I" + vbCrLf
                Exit Function
            End If

            If objInfo.M_IsEnabled = False Then  '�P�_��e���ƬO�_�i�� 2012-2-20�A���i�Τ����\�����I
                AddRowPD = AddRowPD & strCode & "��e���Ƥ��i�ΡA�����\�J�w�I" + vbCrLf
                Exit Function
            End If

            ' row = ds.Tables("WareInput").NewRow
            'CodeSubData.Tables("CodeSub").NewRow()
            row("WIP_NUM") = Nothing
            row("WIP_ID") = Nothing


            row("M_Code") = objInfo.M_Code

            row("M_Name") = objInfo.M_Name

            row("PM_M_Code") = strPM_M_Code

            Dim unit As New LFERP.DataSetting.UnitController
            Dim unitinfo As List(Of LFERP.DataSetting.UnitInfo)

            unitinfo = unit.GetUnitList(objInfo.M_Unit)
            If unitinfo.Count > 0 Then
                row("M_Unit") = unitinfo(0).U_Name
            Else
                row("M_Unit") = ""
            End If


            row("M_Gauge") = objInfo.M_Gauge
            row("OS_BatchID") = ""


            row("WIP_Qty") = Qty

            ''�J�w����ܷ�e�w�s2013-2-27

            ''--------------------------------------------------------------------------
            If strWHID = "W1101" Then
                Dim pic As New ProductInventoryController
                Dim piiGet As List(Of ProductInventoryInfo)
                piiGet = pic.ProductInventory_GetList(strPM_M_Code, strCode, strWHID, Nothing)

                If piiGet.Count <= 0 Then
                    row("WareInvent") = 0
                Else
                    row("WareInvent") = piiGet(0).PI_Qty
                End If
            Else
                Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi1 = wc1.WareInventory_GetSub(strCode, strWHID)

                If wi1 Is Nothing Then
                    row("WareInvent") = 0
                Else
                    row("WareInvent") = wi1.WI_Qty
                End If
            End If

            ''--------------------------------------------------------------------------

            ds.Tables("WareInput").Rows.Add(row)
        End If
        GridView1.MoveLast()
    End Function

    Sub getenable(ByVal a As Boolean, ByVal b As Boolean)

        txtWH.Enabled = a
        ButtonEdit2.Enabled = a
        DateEdit1.Enabled = a
        CheckEdit1.Enabled = b
        CheckRemark.Enabled = b
    End Sub

    Sub loadedit(ByVal WIP_ID As String)
        ds.Tables("WareInput").Clear()

        Dim objInfo As List(Of WareInputInfo)
        Dim pc As New WareInputContraller
        Dim i As Integer
        Dim row As DataRow
        Try
            objInfo = pc.WareInput_Getlist(WIP_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If objInfo Is Nothing Then
                '�S���ƾ�
                Exit Sub
            End If

            cbType.EditValue = objInfo(0).WIP_Type
            strWHID = objInfo(0).WH_ID

            txtWH.EditValue = objInfo(0).WH_Name
            DateEdit1.EditValue = Format(objInfo(0).WIP_AddDate, "yyyy/MM/dd")

            strDPTID = objInfo(0).DPT_ID
            ButtonEdit2.EditValue = objInfo(0).DPT_Name

            CheckEdit1.Checked = objInfo(0).WIP_Check
            CheckDate.Text = objInfo(0).WIP_CheckDate
            CheckAction.Text = objInfo(0).WIP_CheckActionName
            CheckRemark.Text = objInfo(0).WIP_CheckRemark
            OldCheck = objInfo(0).WIP_Check

            CheckEdit2.Checked = objInfo(0).WIP_ReCheck
            RecheckDate.Text = objInfo(0).WIP_ReCheckDate
            RecheckAction.Text = objInfo(0).WIP_ReCheckAction
            RecheckRemark.Text = objInfo(0).WIP_ReCheckRemark
            txtCardID.Text = objInfo(0).CardID

            For i = 0 To objInfo.Count - 1
                row = ds.Tables("WareInput").NewRow
                row("WIP_NUM") = objInfo(i).WIP_NUM
                row("WIP_ID") = objInfo(i).WIP_ID
                row("M_Code") = objInfo(i).M_Code
                row("M_Name") = objInfo(i).M_Name
                row("M_Gauge") = objInfo(i).M_Gauge
                row("M_Unit") = objInfo(i).M_Unit
                row("WIP_Qty") = objInfo(i).WIP_Qty
                row("OS_BatchID") = objInfo(i).OS_BatchID
                row("WIP_Remark") = objInfo(i).WIP_Remark

                ds.Tables("WareInput").Rows.Add(row)
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Function GetWI_ID() As String
        '�ͦ��spm
        Dim pm As New WareInputContraller
        Dim pi As New WareInputInfo
        Dim ndate As String
        ndate = "WI" + Format(Now(), "yyMM")
        pi = pm.WareInput_GetID(ndate)
        If pi Is Nothing Then
            GetWI_ID = ndate + "00001"
        Else
            GetWI_ID = ndate + Mid((CInt(Mid(pi.WIP_ID, 7)) + 100001), 2)
        End If



    End Function

    Function GetWI_NUM() As String
        '�ͦ��spS
        Dim pm As New WareInputContraller
        Dim pi As New WareInputInfo
        pi = pm.WareInput_GetNUM(Nothing)
        If pi Is Nothing Then
            GetWI_NUM = "I000000001"
        Else
            GetWI_NUM = "I" & Mid((CInt(Mid(pi.WIP_NUM, 2)) + 1000000001), 2)
        End If

    End Function

    Private Sub ButtonEdit1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWH.ButtonClick
        'frmWareHouseSelect.SelectWareID = ""
        'tempValue3 = "500105"
        'tempCode = "���ƭܮw"
        'frmWareHouseSelect.ShowDialog()

        'If frmWareHouseSelect.SelectWareID = "" Then


        'Else

        '    txtWH.Text = frmWareHouseSelect.SelectWareName
        '    strWHID = frmWareHouseSelect.SelectWareID

        'End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub popWareInputAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareInputAdd.Click
        tempCode = ""
        tempValue5 = strWHID
        tempValue6 = "�ܮw�޲z"
        frmBOMSelect.ShowDialog()
        If frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then
            '�W�[�O��
            If tempCode = "" Then
                Exit Sub
            Else
                AddRow(tempCode, "")
            End If
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 1 Then
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempValue7, ",")
            n = Len(Replace(tempValue7, ",", "," & "*")) - Len(tempValue7)
            For i = 0 To n
                If arr(i) = "" Then
                    Exit Sub
                End If
                AddRow(arr(i), tempValue2)
            Next
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 2 Then
            Dim i, n As Integer
            Dim arr(n) As String
            strPM_M_Code = tempValue3

            arr = Split(tempValue8, ",")
            n = Len(Replace(tempValue8, ",", "," & "*")) - Len(tempValue8)
            For i = 0 To n
                If arr(i) = "" Then
                    Exit Sub
                End If
                AddRow(arr(i), "")
            Next
        End If
        tempValue7 = ""
        tempValue8 = ""
        tempValue2 = ""
        tempValue3 = ""
    End Sub

    Private Sub popWareInputDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareInputDel.Click
        If ds.Tables("WareInput").Rows.Count = 0 Then Exit Sub


        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "M_Code")

        If DelTemp = "M_Code" Then
        Else
            '�b�R�����W�[�Q�R�����O��
            Dim row As DataRow = ds.Tables("DelDate").NewRow
            'row("M_CodeSub") = CodeSubData.Tables("CodeSub").Rows(GridView1.FocusedRowHandle)("M_CodeSub")
            row("WIP_NUM") = ds.Tables("WareInput").Rows(GridView1.FocusedRowHandle)("WIP_NUM")
            row("WIP_ID") = ds.Tables("WareInput").Rows(GridView1.FocusedRowHandle)("WIP_ID")
            row("M_Code") = DelTemp
            ds.Tables("DelDate").Rows.Add(row)
        End If
        ds.Tables("WareInput").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case Label3.Text
            Case "�J�w��"
                If Edit = False Then

                    If CheckSave() = True Then
                        SaveDataNew()
                        cmdPrint.Visible = True
                        cmdSave.Enabled = False
                    End If
                ElseIf Edit = True Then
                    SaveDataEdit()
                End If

            Case "�����f��"
                    UpdateCheck()  '�����f��

            Case "�_��"
                UpdateReCheck() '�_��

            Case "�ק�ƪ`" '2013-3-5
                SaveRemark()
        End Select

    End Sub

    Sub SaveDataNew()

            Dim mc As New WareInputContraller
            Dim mi As New WareInputInfo

            Dim lockobj As New Object()

            '�ݭn��w���N�X�� 
            SyncLock lockobj

                Thread.Sleep(Int(Rnd() * 500) + 100)
                txtWIPID.EditValue = GetWI_ID()

            End SyncLock

            If Len(txtWIPID.EditValue) = 0 Then
                MsgBox("����ͦ��渹�A�L�k�O�s�I")
                Exit Sub
            End If


            mi.WIP_ID = txtWIPID.EditValue
            mi.WIP_Type = cbType.EditValue
            mi.WH_ID = strWHID
            mi.WIP_AddDate = Format(DateEdit1.EditValue, "yyyy/MM/dd")
            mi.WIP_Action = InUserID

            'mi.DPT_ID = ButtonEdit2.EditValue

            mi.DPT_ID = strDPTID

            mi.WIP_Check = CheckEdit1.Checked
            mi.WIP_CheckAction = InUserID
            mi.WIP_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
            mi.WIP_CheckRemark = CheckRemark.Text
        mi.CardID = txtCardID.Text

        Dim m As Integer

        For m = 0 To ds.Tables("WareInput").Rows.Count - 1
            If IsDBNull(ds.Tables("WareInput").Rows(m)("WIP_Qty")) Or ds.Tables("WareInput").Rows(m)("WIP_Qty") = 0 Then
                MsgBox("�J�w�ƶq���ର�ũ�0!")
                Me.Close()
                Exit Sub
            End If
        Next

        Dim i As Integer
        For i = 0 To ds.Tables("WareInput").Rows.Count - 1

            mi.WIP_NUM = GetWI_NUM()
            mi.M_Code = ds.Tables("WareInput").Rows(i)("M_Code")

            mi.OS_BatchID = ds.Tables("WareInput").Rows(i)("OS_BatchID")

            mi.WIP_Qty = CDbl(ds.Tables("WareInput").Rows(i)("WIP_Qty"))

            If IsDBNull(ds.Tables("WareInput").Rows(i)("WIP_Remark")) Then
                mi.WIP_Remark = Nothing
            Else
                mi.WIP_Remark = ds.Tables("WareInput").Rows(i)("WIP_Remark")
            End If

            '�h�Ƴ渹-------------------------------------
            If IsDBNull(ds.Tables("WareInput").Rows(i)("RC_AutoID")) Then
                mi.RC_AutoID = Nothing
            Else
                mi.RC_AutoID = ds.Tables("WareInput").Rows(i)("RC_AutoID")
            End If
            mi.RC_NO = txtRC_NO.Text

            mc.WareInput_Add1(mi)

        Next

        If CheckEdit1.Checked = True Then
            Dim j As Integer
            Dim Qty As Double
            Dim dblWIP_EndQty As Double

            For j = 0 To ds.Tables("WareInput").Rows.Count - 1
                If strWHID = "W1101" Then
                    Dim pic As New ProductInventoryController
                    Dim piiSet As New ProductInventoryInfo
                    piiSet.WH_ID = strWHID
                    piiSet.PM_M_Code = ds.Tables("WareInput").Rows(j)("PM_M_Code")
                    piiSet.M_Code = ds.Tables("WareInput").Rows(j)("M_Code")

                    Dim piiGet As List(Of ProductInventoryInfo)
                    piiGet = pic.ProductInventory_GetList(ds.Tables("WareInput").Rows(j)("PM_M_Code"), ds.Tables("WareInput").Rows(j)("M_Code"), strWHID, Nothing)

                    If piiGet.Count <= 0 Then
                        Qty = 0
                    Else
                        Qty = piiGet(0).PI_Qty
                    End If

                    piiSet.PI_Qty = Qty + CDbl(ds.Tables("WareInput").Rows(j)("WIP_Qty"))

                    dblWIP_EndQty = piiSet.PI_Qty

                    pic.ProductInventory_Update(piiSet)
                Else
                    Dim mt As New SharePurchaseController
                    Dim mm As New SharePurchaseInfo
                    mm.WH_ID = strWHID
                    mm.M_Code = ds.Tables("WareInput").Rows(j)("M_Code")

                    Dim wi As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                    wi = wc.WareInventory_GetSub(ds.Tables("WareInput").Rows(j)("M_Code"), strWHID)

                    If wi Is Nothing Then
                        Qty = 0
                    Else
                        Qty = wi.WI_Qty
                    End If

                    mm.WI_Qty = Qty + CDbl(ds.Tables("WareInput").Rows(j)("WIP_Qty"))

                    ' dblWIP_EndQty = mm.WI_Qty = Qty
                    dblWIP_EndQty = mm.WI_Qty 
                    mt.UpdateWareInventory_WIQty2(mm)

                End If

                '------------------------�ܧ��e���l��----------------------------
                Dim info As New WareInputInfo
                info.WIP_ID = txtWIPID.EditValue
                info.WH_ID = strWHID
                info.M_Code = ds.Tables("WareInput").Rows(j)("M_Code")
                info.WIP_EndQty = dblWIP_EndQty  '��e�ܮw���l��

                mc.WareInput_UpdateEndQty(info)
                '----------------------------------------------------
            Next


            ''�h�Ƴ��,2013-11-27----------------------------------------------------
            If cbType.EditValue = "�Ͳ��h��" Then
                Dim j1 As Integer
                For j1 = 0 To ds.Tables("WareInput").Rows.Count - 1
                    Dim Rci As New ProductionKaiLiaoReturnInfo
                    Dim Rcc As New ProductionKaiLiaoReturnControl
                    Rci.AutoID = ds.Tables("WareInput").Rows(j1)("RC_AutoID")
                    Rci.R_ActQty = ds.Tables("WareInput").Rows(j1)("WIP_Qty")
                    Rcc.ProductionKaiLiaoReturn_UpdateR_ActQty(Rci)
                Next
            End If

        End If

        'Me.Close()
    End Sub

    Sub SaveDataEdit()
        On Error Resume Next

        If Len(txtWH.EditValue) = 0 Then
            MsgBox("�п�ܭܮw")
            Exit Sub
        End If
        If Len(ButtonEdit2.EditValue) = 0 Then
            MsgBox("�п�ܳ���")
            Exit Sub
        End If

        If ds.Tables("WareInput").Rows.Count = 0 Then
            MsgBox("�п�ܪ���")
            Exit Sub
        End If

        If Len(txtWIPID.EditValue) = 0 Then
            MsgBox("�渹���šA�L�k�O�s�I")
            Exit Sub
        End If

        '��s�R�����O��
        If ds.Tables("DelDate").Rows.Count > 0 Then
            Dim ii As Integer
            For ii = 0 To ds.Tables("DelDate").Rows.Count - 1

                Dim mc2 As New WareInputContraller
                If Not IsDBNull(ds.Tables("DelDate").Rows(ii)("WIP_NUM")) Then
                    mc2.WareInput_Delete(ds.Tables("DelDate").Rows(ii)("WIP_NUM"), Nothing)
                End If
            Next ii
        End If

        Dim mc As New WareInputContraller
        Dim mi As New WareInputInfo

        mi.WIP_ID = txtWIPID.EditValue
        mi.WIP_Type = cbType.EditValue
        mi.WH_ID = strWHID
        mi.WIP_AddDate = DateEdit1.EditValue
        mi.WIP_Action = InUserID

        'mi.DPT_ID = ButtonEdit2.EditValue
        mi.DPT_ID = strDPTID


        mi.WIP_Check = CheckEdit1.Checked
        mi.WIP_CheckAction = InUserID
        mi.WIP_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        mi.WIP_CheckRemark = CheckRemark.Text

        Dim i As Integer
        For i = 0 To ds.Tables("WareInput").Rows.Count - 1
            If IsDBNull(ds.Tables("WareInput").Rows(i)("WIP_NUM")) Then   '�s�W
                mi.WIP_NUM = GetWI_NUM()
                mi.M_Code = ds.Tables("WareInput").Rows(i)("M_Code")
                'mi.M_Name = ds.Tables("WareInput").Rows(i)("M_Name")
                'mi.M_Gauge = ds.Tables("WareInput").Rows(i)("M_Gauge")
                'mi.M_Unit = ds.Tables("WareInput").Rows(i)("M_Unit")
                mi.OS_BatchID = ds.Tables("WareInput").Rows(i)("OS_BatchID")
                mi.WIP_Qty = CDbl(ds.Tables("WareInput").Rows(i)("WIP_Qty"))
                mi.WIP_Remark = ds.Tables("WareInput").Rows(i)("WIP_Remark")

                mc.WareInput_Add(mi)
            ElseIf Not IsDBNull(ds.Tables("WareInput").Rows(i)("WIP_NUM")) Then ' �ק�
                mi.WIP_NUM = ds.Tables("WareInput").Rows(i)("WIP_NUM")
                mi.M_Code = ds.Tables("WareInput").Rows(i)("M_Code")
                'mi.M_Name = ds.Tables("WareInput").Rows(i)("M_Name")
                'mi.M_Gauge = ds.Tables("WareInput").Rows(i)("M_Gauge")
                'mi.M_Unit = ds.Tables("WareInput").Rows(i)("M_Unit")
                mi.OS_BatchID = ds.Tables("WareInput").Rows(i)("OS_BatchID")
                mi.WIP_Qty = CDbl(ds.Tables("WareInput").Rows(i)("WIP_Qty"))
                mi.WIP_Remark = ds.Tables("WareInput").Rows(i)("WIP_Remark")
                mi.WIP_EditDate = Format(Now, "yyyy/MM/dd")

                mc.WareInput_Update(mi)
            End If
        Next
        If OldCheck = True Then
            MsgBox("����w�f��!")
        Else
            If CheckEdit1.Checked = True Then
                Dim mt As New SharePurchaseController
                Dim mm As New SharePurchaseInfo
                Dim j As Integer


                mm.WH_ID = strWHID
                For j = 0 To ds.Tables("WareInput").Rows.Count - 1

                    mm.M_Code = ds.Tables("WareInput").Rows(j)("M_Code")

                    Dim Qty As Double
                    Dim wi As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                    wi = wc.WareInventory_GetSub(ds.Tables("WareInput").Rows(j)("M_Code"), strWHID)


                    If wi Is Nothing Then
                        Qty = 0
                    Else
                        Qty = wi.WI_Qty
                    End If

                    mm.WI_Qty = Qty + CDbl(ds.Tables("WareInput").Rows(j)("WIP_Qty"))

                    mt.UpdateWareInventory_WIQty2(mm)

                Next
            End If
        End If
      

        'Me.Close()
    End Sub

    Sub UpdateCheck()

        Dim mc As New WareInputContraller
        Dim mi As New WareInputInfo
        mi.WIP_ID = txtWIPID.EditValue
        mi.WIP_Check = CheckEdit1.Checked
        mi.WIP_CheckAction = InUserID
        mi.WIP_CheckDate = Format(Now, "yyyy/MM/dd")
        mi.WIP_CheckRemark = CheckRemark.Text
        If OldCheck = CheckEdit1.Checked Then
            MsgBox("�����f�֪��A�����ܡA�Ч�窱�A��A�O�s�K�K")
            Exit Sub
        End If
        If mc.WareInput_UpdateCheck(mi) = False Then
            MsgBox("�����f�֥���")
            Exit Sub
        End If


        Dim mt As New SharePurchaseController
        Dim mm As New SharePurchaseInfo
        Dim i As Integer


        mm.WH_ID = strWHID
        For i = 0 To ds.Tables("WareInput").Rows.Count - 1

            mm.M_Code = ds.Tables("WareInput").Rows(i)("M_Code")

            Dim Qty As Single
            Dim wi As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            wi = wc.WareInventory_GetSub(ds.Tables("WareInput").Rows(i)("M_Code"), strWHID)

            If wi Is Nothing Then
                Qty = 0
            Else
                Qty = wi.WI_Qty
            End If

            'If CheckEdit1.Checked = True Then
            '    mm.WI_Qty = Qty + CSng(ds.Tables("WareInput").Rows(i)("WIP_Qty"))
            'ElseIf CheckEdit1.Checked = False Then
            '    mm.WI_Qty = Qty - CSng(ds.Tables("WareInput").Rows(i)("WIP_Qty"))
            'End If
            If CheckEdit1.Checked = False Then
                mm.WI_Qty = Qty - CDbl(ds.Tables("WareInput").Rows(i)("WIP_Qty"))
            End If
            'mt.UpdateWareInventory_WIQty(mm)
            mt.UpdateWareInventory_WIQty2(mm)

        Next

        Me.Close()

    End Sub

    Sub UpdateReCheck()

        Dim mc As New WareInputContraller
        Dim mi As New WareInputInfo

        mi.WIP_ID = txtWIPID.EditValue
        mi.WIP_ReCheck = CheckEdit2.Checked
        mi.WIP_ReCheckAction = InUserID
        mi.WIP_ReCheckDate = Format(Now, "yyyy/MM/dd")
        mi.WIP_ReCheckRemark = CheckRemark.Text

        If mc.WareInput_UpdateReCheck(mi) = False Then
            MsgBox("�f�֥���")
            Exit Sub
        End If
        Me.Close()
    End Sub

    Private Sub popWareInputBarAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareInputBarAdd.Click
        'AddRow("")
        'GridView1.Columns("M_Code").OptionsColumn.ReadOnly = False
        FrmBarCodeInput.ds.Tables.Clear()
        FrmBarCodeInput.ShowDialog()
        If FrmBarCodeInput.ds.Tables("BarCode").Rows.Count <> 0 Then
            Dim i As Integer
            For i = 0 To FrmBarCodeInput.ds.Tables("BarCode").Rows.Count - 1
                AddRow(FrmBarCodeInput.ds.Tables("BarCode").Rows(i)("M_Code"), FrmBarCodeInput.ds.Tables("BarCode").Rows(i)("WIP_Qty"))
            Next

        End If
    End Sub

    'Private Sub MCodeText_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MCodeText.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        e.Handled = True

    '        GridView1.FocusedColumn = GridView1.Columns("WIP_Qty")

    '    End If
    'End Sub

    'Private Sub QtyItemCalcEdit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles QtyItemCalcEdit.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        e.Handled = True
    '        MsgBox(GridView1.GetFocusedRowCellValue("M_Code").ToString)
    '        SendKeys.Send("{TAB}")
    '    End If
    'End Sub

    'Private Sub RemarkItemMemoExEdit1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RemarkItemMemoExEdit1.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        AddRow("", 0)
    '        GridView1.FocusedColumn = GridView1.Columns("M_Code")
    '    End If
    'End Sub


    'Private Sub GridView1_FocusedColumnChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles GridView1.FocusedColumnChanged
    '    On Error Resume Next
    '    If e.PrevFocusedColumn.FieldName = "M_Code" Then
    '        'MsgBox(ds.Tables("WareInput").Rows(GridView1.FocusedRowHandle)("M_Code").ToString)
    '        celloadmsg(ds.Tables("WareInput").Rows(GridView1.FocusedRowHandle)("M_Code").ToString)
    '    End If
    'End Sub

    Private Sub cmdBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBarCode.Click
        'tempValue3 = "�J�w�@�~"
        'tempValue4 = tempValue2
        'Dim myfrm As New frmBarCode
        'myfrm.ShowDialog()

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        tempValue3 = "�J�w�@�~"
        tempValue4 = tempValue2

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500109")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                tempValue3 = "�i�M���J�w�@�~"
                tempValue4 = txtWIPID.Text
            Else
                tempValue3 = "�J�w�@�~"
            End If
            popWareInputAdd.Enabled = True 'txtWIPID
        End If


        Dim myfrm As New frmBarCode
        myfrm.ShowDialog()
        myfrm.Dispose()

    End Sub

    Private Sub txtWH_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWH.EditValueChanged

    End Sub

    Private Sub ButtonEdit2_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ButtonEdit2.ButtonClick
        ''frmDepartmentSelect.DptID = ""
        ''frmDepartmentSelect.DptName = ""


        ''frmDepartmentSelect.ShowDialog()

        ''If frmDepartmentSelect.DptID = "" Then

        ''Else

        ''    ButtonEdit2.Text = frmDepartmentSelect.DptName
        ''    strDPTID = frmDepartmentSelect.DptID
        ''    'ButtonEdit2.Tag = frmDepartmentSelect.DptID

        ''End If

        '2013-1-7
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500107")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value <> "" Then
                frmDepartmentSelect1.DptID = ""
                frmDepartmentSelect1.DptName = ""
                tempValue = pmwiL.Item(0).PMWS_Value

                frmDepartmentSelect1.ShowDialog()
                If frmDepartmentSelect1.DptID = "" Then
                Else
                    ButtonEdit2.Text = frmDepartmentSelect1.DptName
                    strDPTID = frmDepartmentSelect1.DptID
                End If

                Exit Sub

            End If
        End If

        frmDepartmentSelect.DptID = ""
        frmDepartmentSelect.DptName = ""
        frmDepartmentSelect.ShowDialog()

        If frmDepartmentSelect.DptID = "" Then
        Else
            ButtonEdit2.Text = frmDepartmentSelect.DptName
            strDPTID = frmDepartmentSelect.DptID
            'ButtonEdit2.Tag = frmDepartmentSelect.DptID
        End If


    End Sub

    Private Sub ButtonEdit2_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEdit2.EditValueChanged

    End Sub
    '���}���ɤ��H��
    Private Sub popFileShowOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popFileShowOpen.Click
        Dim dt As New FileController
        If GridFile.Rows.Count = 0 Then Exit Sub
        dt.File_Open(Nothing, Nothing, GridFile.CurrentRow.Cells("F_No").Value.ToString)
    End Sub

    Private Sub popWareInputBatchAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareInputBatchAdd.Click
        'tempValue4 = strWHID
        frmLoadingBatchID.ShowDialog()

        If tempValue = "" Or tempValue2 = "" Then
            Exit Sub
        Else
            AddRow1(tempValue, tempValue2, tempValue3)

            tempValue = ""
            tempValue2 = ""
            tempValue3 = Nothing

        End If
    End Sub
    Sub AddRow1(ByVal strBatchID As String, ByVal strCode As String, ByVal strQty As Double)

        Dim row As DataRow
        row = ds.Tables("WareInput").NewRow

        If strCode = "" Then

        Else

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)

            If objInfo.M_IsEnabled = False Then  '�P�_��e���ƬO�_�i�� 2012-2-20�A���i�Τ����\�����I
                MsgBox("��e���Ƥ��i�ΡA�����\�J�w�I")
                Exit Sub
            End If

            row("WIP_NUM") = Nothing
            row("WIP_ID") = Nothing
            row("M_Code") = objInfo.M_Code
            row("M_Name") = objInfo.M_Name

            Dim unit As New LFERP.DataSetting.UnitController
            Dim unitinfo As List(Of LFERP.DataSetting.UnitInfo)

            unitinfo = unit.GetUnitList(objInfo.M_Unit)

            If unitinfo.Count > 0 Then
                row("M_Unit") = unitinfo(0).U_Name
            Else
                row("M_Unit") = ""
            End If

            row("M_Gauge") = objInfo.M_Gauge
            row("OS_BatchID") = strBatchID
            row("WIP_Qty") = strQty

            ds.Tables("WareInput").Rows.Add(row)
        End If
        GridView1.MoveLast()
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Dim ds1 As New DataSet

        If GridView1.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet

        Dim pmc As New WareInputContraller
        Dim suc As New SystemUser.SystemUserController
        Dim wh As New WareHouseController
        Dim uc2 As New DataSetting.DepartmentControler

        ds1.Tables.Clear()
        Dim strA As String
        strA = txtWIPID.Text

        ltc.CollToDataSet(ds1, "WareInput", pmc.WareInput_Getlist(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc1.CollToDataSet(ds1, "WareHouse", wh.WareHouse_GetList(Nothing))
        ltc2.CollToDataSet(ds1, "Department", uc2.Department_GetList(Nothing, Nothing, Nothing))

        Dim mi As List(Of WareInputInfo)
        mi = pmc.WareInput_Getlist(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If mi.Count = 0 Then Exit Sub

        If mi(0).WIP_Check = False Then
            PreviewRPT(ds1, "rptWareInput", "�J�w��", False, False)
        Else
            PreviewRPT(ds1, "rptWareInput", "�J�w��", True, False)
        End If

        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing

        Me.Close()
        'cmdPrint.Visible = False

    End Sub

    Function CheckSave() As Boolean
        'CheckSave = True
        Dim isNegative As Boolean
        Dim isPositive As Boolean
        Dim wulc As New WhiteUserListController
        Dim wuliL As New List(Of WhiteuserListInfo)
        Dim i As Integer
        Dim strCardID As String

        isNegative = False
        isPositive = False

        If Len(txtWH.EditValue) = 0 Then
            MsgBox("�п�ܭܮw")
            CheckSave = False
            Exit Function
        End If
        If Len(ButtonEdit2.EditValue) = 0 Then
            MsgBox("�п�ܳ���")
            CheckSave = False
            Exit Function
        End If
        If ds.Tables("WareInput").Rows.Count = 0 Then
            MsgBox("�п�ܪ���")
            CheckSave = False
            Exit Function
        End If
        '�d�߬O�_�������ܮw������
        Dim mw As New WareInventory.WareInventoryMTController
        Dim mwi As List(Of WareInventory.WareInventoryInfo)
        Dim m As Integer
        For m = 0 To ds.Tables("WareInput").Rows.Count - 1

            mwi = mw.WareInventory_GetList3(ds.Tables("WareInput").Rows(m)("M_Code"), strWHID, "True")

            If mwi.Count = 0 Then
                If CDbl(ds.Tables("WareInput").Rows(m)("WIP_Qty")) < 0 Then
                    MsgBox("��e���Ʀb���ܮw���إ�, ���ɤ����\�J�w���t��!")
                    CheckSave = False
                    Exit Function
                ElseIf CDbl(ds.Tables("WareInput").Rows(m)("WIP_Qty")) = 0 Then
                    MsgBox("�J�w�ƶq���ର�s!")
                    CheckSave = False
                    Exit Function
                ElseIf CDbl(ds.Tables("WareInput").Rows(m)("WIP_Qty")) > 0 Then
                    CheckSave = True
                End If
            Else
                If mwi(0).WI_Qty = 0 Then
                    If CDbl(ds.Tables("WareInput").Rows(m)("WIP_Qty")) < 0 Then
                        MsgBox("��e���Ʈw�s��0, ���ɤ����\�J�w���t��!")
                        CheckSave = False
                        Exit Function
                    Else
                        CheckSave = True
                    End If
                ElseIf CDbl(ds.Tables("WareInput").Rows(m)("WIP_Qty")) = 0 Then
                    MsgBox("�J�w�ƶq���ର�s!")
                    CheckSave = False
                    Exit Function
                ElseIf mwi(0).WI_Qty > 0 And mwi(0).WI_Qty + ds.Tables("WareInput").Rows(m)("WIP_Qty") < 0 Then
                    MsgBox("����R�b�ƶq�j���e�ܮw�w�s��,�Ю֬d!")
                    CheckSave = False
                    Exit Function
                ElseIf mwi(0).WI_Qty < 0 Then

                    If mwi(0).WI_Qty + CDbl(ds.Tables("WareInput").Rows(m)("WIP_Qty")) >= 0 Then
                        CheckSave = True
                    Else
                        MsgBox("��e����" & ds.Tables("WareInput").Rows(m)("M_Code") & "�w�s�ܧ�Z���t��,���ˬd��]!")
                        CheckSave = False
                        Exit Function
                    End If

                Else
                    CheckSave = True
                End If
            End If

            If CDbl(ds.Tables("WareInput").Rows(m)("WIP_Qty")) < 0 Then
                isNegative = True
            ElseIf CDbl(ds.Tables("WareInput").Rows(m)("WIP_Qty")) > 0 Then
                isPositive = True
            End If
        Next

        If isPositive = True And isNegative = True Then
            MsgBox("�P�@�i�J�w�椤�J�w�ƶq�����\�P�ɦs�b���ƩM�t��!", 64, "����")
            CheckSave = False
        ElseIf isNegative = True Then
            If txtCardID.Text = "" Then
                MsgBox("��J�ƶq���t�Ʈɻݭn��d�A�Ш�d!", 64, "����")
                CheckSave = False
                Exit Function
            End If

            '@2013/1/30 �K�[ �u�����w�H���~�i�H�b�J�w�ƶq���t�Ʈɨ�d-----
            strCardID = Microsoft.VisualBasic.Left(txtCardID.Text, InStr(txtCardID.Text, "-") - 1)
            wuliL = wulc.WhiteUserList_GetList(strCardID, Nothing)

            If wuliL.Count > 0 Then
                For i = 0 To wuliL.Count - 1
                    If wuliL(i).W_Remark = "�R�P�d" Then
                        CheckSave = True
                        Exit Function
                    End If
                Next
            End If

            MsgBox("�Ө�d�H���s�b�J�w�ƶq���t�Ʈɪ���d�v���A����J�t��!")
            txtCardID.Text = ""
            CheckSave = False
            '--------------------------------------------------------------------------------------------------

        ElseIf isPositive = True Then
            If CheckNeedCheckInput() = False Then  '�w��i�M��,�]�m�J�w�@�w�n��d

                If txtCardID.Text <> "" Then
                    If MsgBox("��J�ƶq�����Ʈɤ��ݭn��d�A�O�_�M�Ũ�d�H�H���A�}�O�s�ƾڡH", MsgBoxStyle.YesNo + MsgBoxStyle.Information, "����") = MsgBoxResult.Yes Then
                        txtCardID.Text = ""
                        CheckSave = True
                    Else
                        CheckSave = False
                    End If
                End If
            Else
                If txtCardID.Text <> "" Then
                    CheckSave = True
                Else
                    MsgBox("�L��d�H��,���ˬd!")
                    CheckSave = False
                End If
            End If

        End If



        ''�h�Ƴ��,2013-11-27----------------------------------------------------
        If cbType.EditValue = "�Ͳ��h��" Then
            Dim j1 As Integer
            For j1 = 0 To ds.Tables("WareInput").Rows.Count - 1
                If IsDBNull(ds.Tables("WareInput").Rows(j1)("RC_AutoID")) Then
                    MsgBox("�Ͳ��h�Ƴ��J���~!")
                    txtRC_NO.Select()
                    CheckSave = False
                End If
            Next
        End If


    End Function

    Private Sub btnRefCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefCard.Click

        If CheckNeedCheckInput() = True Then '�w��i�M��,�]�m�J�w�@�w�n��d
            txtCardID.Text = ReadCard()
            Exit Sub
        End If


        Dim i%
        Dim isNegative As Boolean
        isNegative = False

        If ds.Tables("WareInput").Rows.Count <= 0 Then
            MsgBox("�J�w���Ƥ��ର�šA�åB�u���J�w�ƶq���t�Ʈɤ~�ݭn��d!", 64, "����")
            Exit Sub
        End If

        For i = 0 To ds.Tables("WareInput").Rows.Count - 1
            If CSng(ds.Tables("WareInput").Rows(i)("WIP_Qty")) < 0 Then
                isNegative = True
            Else
                isNegative = False
                Exit For
            End If
        Next
        If isNegative = True Then
            txtCardID.Text = ReadCard()
        Else
            MsgBox("���J�w�椣�ݭn��d�A�u���J�w�ƶq���t�Ʈɤ~�ݭn��d!", 64, "����")
        End If
    End Sub

    '@ 2013/1/25 �K�[ ���U�^����եβK�[�ƾڦ�ƥ�,���X���y�����y�X�����X�۱a�^�����
    Private Sub txtM_Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    AddRow(txtM_Code.Text.Trim, "")
        '    txtM_Code.Text = ""
        'End If

        If e.KeyCode = Keys.Enter Then
            Dim strCode As String
            Dim strQty As String
            Dim intIn As Integer
            Dim StrText As String

            StrText = txtM_Code.Text

            intIn = InStr(StrText, ",", CompareMethod.Text)
            If intIn <= 0 Then
                strCode = StrText
                strQty = "1"
            Else
                strCode = Mid(StrText, 1, intIn - 1)
                strQty = Mid(StrText, intIn + 1, Len(StrText) - intIn)
            End If


            LabelMsg.Text = LabelMsg.Text + AddRowPD(strCode, strQty)

            txtM_Code.Text = ""
        End If

    End Sub

    '@ 2013/1/25 �K�[
    Private Sub txtM_Code_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyUp
        If GridView1.RowCount <= 0 Then Exit Sub

        If e.KeyCode = Keys.Home Then       '���UHome��A�J�I��ƶq�椸����o�J�I
            GridView1.Focus()
            GridView1.FocusedColumn = GridView1.Columns("WIP_Qty")
        End If

        If e.KeyCode = Keys.PageUp Then    '���UPageUp��A�J�I��ƶq�[�@
            ds.Tables("WareInput").Rows(GridView1.FocusedRowHandle)("WIP_Qty") = ds.Tables("WareInput").Rows(GridView1.FocusedRowHandle)("WIP_Qty") + 1
        End If

        If e.KeyCode = Keys.PageDown Then   '���UPageDown��A�J�I��ƶq��@
            ds.Tables("WareInput").Rows(GridView1.FocusedRowHandle)("WIP_Qty") = ds.Tables("WareInput").Rows(GridView1.FocusedRowHandle)("WIP_Qty") - 1
        End If

        If e.KeyCode = Keys.Up Then    '���U�V�W��V��A�J�I��W���@��A�åB�J�I���b�ƶq�椸�椤
            GridView1.FocusedRowHandle = GridView1.FocusedRowHandle - 1
            GridView1.Focus()
            GridView1.FocusedColumn = GridView1.Columns("WIP_Qty")
        End If

        If e.KeyCode = Keys.Down Then   '���U�V�U��V��A�J�I��U���@��A�åB�J�I���b�ƶq�椸�椤
            GridView1.FocusedRowHandle = GridView1.FocusedRowHandle + 1
            GridView1.Focus()
            GridView1.FocusedColumn = GridView1.Columns("WIP_Qty")
        End If
    End Sub

    '@ 2013/1/25 �K�[
    Private Sub GridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            txtM_Code.Focus()
        End If
    End Sub

    Private Sub txtM_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtM_Code.EditValueChanged

    End Sub

    Private Sub txtM_Code_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtM_Code.Leave
        If LabelMsg.Text <> "" Then
            MsgBox(LabelMsg.Text)
        End If
    End Sub


    ''2013-3-5�ק�ƪ`
    Sub SaveRemark()
        Dim mcc As New WareInputContraller
        Dim mii As New WareInputInfo

        mii.WIP_ID = txtWIPID.EditValue
       
        Dim i As Integer
        For i = 0 To ds.Tables("WareInput").Rows.Count - 1
            mii.WIP_NUM = ds.Tables("WareInput").Rows(i)("WIP_NUM")
            mii.WIP_Remark = ds.Tables("WareInput").Rows(i)("WIP_Remark")
            If mcc.WareInput_UpdateRemark(mii) = True Then
            Else
                Exit Sub
            End If
        Next

        MsgBox("�O�s���\!")
        Me.Close()
    End Sub


    '2013-5-11
    Private Sub ToolStripBatchLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripBatchLoad.Click
        tempCode = ""
        tempValue12 = "B"
        '--------------------------------
        tempValue5 = strWHID
        tempValue6 = "�ܮw�޲z"

        frmBOMSelect.ShowDialog()


        If tempValue = Nothing Or tempValue = "" Then
            Exit Sub
        End If

        Dim i, n As Integer
        Dim arr(n) As String
        arr = Split(tempValue, ",")
        n = Len(Replace(tempValue, ",", "," & "*")) - Len(tempValue)
        For i = 0 To n
            If arr(i) = "" Then
                Exit Sub
            End If
            AddRow(arr(i), "")
        Next

        tempValue = Nothing
        tempValue12 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing

    End Sub

    ''' <summary>
    ''' �ˬd���w�ܮw�b�J�ɬO�_�ݭn��d
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckNeedCheckInput() As Boolean
        CheckNeedCheckInput = True

        Dim wc As New WareHouseController
        Dim wiL As New List(Of WareHouseInfo)
        wiL = wc.WareHouse_Get(strWHID)
        If wiL.Item(0).NeedCheckInput = False Then
            CheckNeedCheckInput = False
            btnRefCard.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default
        Else
            CheckNeedCheckInput = True
            btnRefCard.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        End If
    End Function


    Private Sub txtM_Code_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtM_Code.KeyPress

    End Sub

    Private Sub cbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbType.SelectedIndexChanged
        If cbType.EditValue = "�Ͳ��h��" Then
            Label16.Visible = True
            txtRC_NO.Visible = True
            txtRC_NO.Select()
        Else
            Label16.Visible = False
            txtRC_NO.Visible = False
        End If
    End Sub

    Private Sub txtRC_NO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRC_NO.KeyDown
        If e.KeyCode = Keys.Enter Then
            ds.Tables("WareInput").Clear()

            If txtRC_NO.Text = "" Then
                MsgBox("�п�J�Ͳ��h�Ƴ�!")
                txtRC_NO.Select()
                Exit Sub
            End If


            Dim Prc As New ProductionKaiLiaoReturnControl
            Dim prl As New List(Of ProductionKaiLiaoReturnInfo)
            prl = Prc.ProductionKaiLiaoReturn_GetList(Nothing, txtRC_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, "True", Nothing, Nothing, Nothing, Nothing, Nothing)

            If prl.Count <= 0 Then
                MsgBox("���h�Ƴ椣�s�b,�Υ��f��!")
                txtRC_NO.Select()
                Exit Sub
            End If

            If prl(0).R_ActQty > 0 Then
                MsgBox("���h�Ƴ�w�h��,����A�i��h�ƾާ@!")
                txtRC_NO.Select()
                Exit Sub
            End If

            popWareInput.Enabled = False
            ' WIP_Qty.OptionsColumn.ReadOnly = True
            txtRC_NO.Enabled = False
            cbType.Enabled = False

            Dim j As Integer

            For j = 0 To prl.Count - 1
                AddRowR_NO(prl(j).M_Code, prl(j).AutoID, prl(j).RQty)
            Next
        End If
    End Sub
    Sub AddRowR_NO(ByVal strCode As String, ByVal RC_AutoID As String, ByVal Qty As Double)

        Dim row As DataRow
        row = ds.Tables("WareInput").NewRow
        If strCode = "" Then
        Else
            Dim i As Integer
            For i = 0 To ds.Tables("WareInput").Rows.Count - 1
                If strCode = ds.Tables("WareInput").Rows(i)("M_Code") Then
                    MsgBox("�@�i�椣���\�����_���ƽs�X....")
                    Exit Sub
                End If
            Next

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)
            If objInfo Is Nothing Then
                MsgBox("��e���Ƥ��s�b�A�нT�{�����ƫH���I", 64, "����")
                Exit Sub
            End If

            If objInfo.M_IsEnabled = False Then  '�P�_��e���ƬO�_�i�� 2012-2-20�A���i�Τ����\�����I
                MsgBox("��e���Ƥ��i�ΡA�����\�J�w�I")
                Exit Sub
            End If

            row("RC_AutoID") = RC_AutoID
            row("WIP_NUM") = Nothing
            row("WIP_ID") = Nothing

            row("M_Code") = objInfo.M_Code
            row("M_Name") = objInfo.M_Name
            row("PM_M_Code") = ""

            Dim unit As New LFERP.DataSetting.UnitController
            Dim unitinfo As List(Of LFERP.DataSetting.UnitInfo)
            unitinfo = unit.GetUnitList(objInfo.M_Unit)
            If unitinfo.Count > 0 Then
                row("M_Unit") = unitinfo(0).U_Name
            Else
                row("M_Unit") = ""
            End If

            row("M_Gauge") = objInfo.M_Gauge
            row("OS_BatchID") = ""
            row("WIP_Qty") = Qty
            ''�J�w����ܷ�e�w�s2013-2-27
            Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            wi1 = wc1.WareInventory_GetSub(strCode, strWHID)

            If wi1 Is Nothing Then
                row("WareInvent") = 0
            Else
                row("WareInvent") = wi1.WI_Qty
            End If
            ''--------------------------------------------------------------------------
            ds.Tables("WareInput").Rows.Add(row)
        End If
        GridView1.MoveLast()
    End Sub


End Class