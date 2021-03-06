Imports LFERP.Library.Outward.OutwardAcceptance
Imports LFERP.Library.Outward
Imports LFERP.Library.Purchase.SharePurchase
Public Class FormAcceptance
    Dim ds As New DataSet
    Dim OldCheck As Boolean
    Dim OldAccCheck As Boolean

    Private Sub formAcceptance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtQuoID.Text = tempValue2
        Label5.Text = MTypeName
        tempValue2 = ""
        MTypeName = ""

        Dim mtd As New LFERP.DataSetting.SuppliersControler
        ButtonEdit1.Text = "W0701"
        txtQuoID.Enabled = False
        gluSupplier.Properties.DisplayMember = "S_SupplierName"
        gluSupplier.Properties.ValueMember = "S_Supplier"
        gluSupplier.Properties.DataSource = mtd.GetSuppliersList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "True")

        Dim oc As New OutwardController
        Me.RepositoryItemLookUpEdit1.DataSource = oc.LookUpEdit_Get("外發加工").Tables(0)
        Me.RepositoryItemLookUpEdit1.ValueMember = "OT_NO"
        Me.RepositoryItemLookUpEdit1.DisplayMember = "OT_Name"

        CreateTables()
        txtQuoID.Enabled = False

        Select Case Label5.Text

            Case "OutwardAcceptanceAddEdit"
                '驗收單新增或修改
                getenable(True, False, False)

                Label6.Text = ""
                Select Case Edit
                    Case False
                        Label7.Text = InUserID
                        DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")
                        Label2.Text = "1"
                        txtQuoID.Text = ""
                        Me.Text = "外發驗收--暫收"
                    Case True
                        If LoadData(txtQuoID.Text) = False Then Exit Sub
                        Label2.Text = CStr(CInt(Label2.Text) + 1)
                        DateEdit1.Enabled = False
                        Me.Text = "外發驗收--修改" & "[" & txtQuoID.Text & "]"
                End Select
            Case "OutwardAcceptanceCheck"
                '驗收
                XtraTabControl1.SelectedTabPage = XtraTabPage2

                getenable(False, True, False)
                If LoadData(txtQuoID.Text) = False Then Exit Sub
             
                DateEdit2.EditValue = Format(Now, "yyyy/MM/dd")
                Me.Text = "外發驗收--審核" & "[" & txtQuoID.Text & "]"
            Case "OutwardAcceptanceAccCheck"
                '審核
                XtraTabControl1.SelectedTabPage = XtraTabPage3

                getenable(False, False, True)
                If LoadData(txtQuoID.Text) = False Then Exit Sub
                DateEdit3.EditValue = Format(Now, "yyyy/MM/dd")
                Me.Text = "外發驗收--復核" & "[" & txtQuoID.Text & "]"
            Case "OutwardAcceptanceView"
                getenable(False, False, False)
                If LoadData(txtQuoID.Text) = False Then Exit Sub
                '查看
                AdvBandedGridView1.Columns.Item("A_Qty").OptionsColumn.ReadOnly = True
                AdvBandedGridView1.Columns.Item("A_Remark").OptionsColumn.ReadOnly = True
                Me.Text = "外發驗收--查看" & "[" & txtQuoID.Text & "]"
        End Select

        ButtonEdit1.Enabled = False
        gluSupplier.Enabled = False

    End Sub
    Sub CreateTables()
        '建立驗收表  OutwardAcceptance
        ds.Tables.Clear()
        With ds.Tables.Add("OutwardAcceptance")
            .Columns.Add("A_NO", GetType(String))
            .Columns.Add("A_AcceptanceNO", GetType(String))
            .Columns.Add("WH_ID", GetType(String))
            .Columns.Add("O_NO", GetType(String))
            .Columns.Add("A_SendNO", GetType(String))
            .Columns.Add("A_SendType", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("OS_BatchID", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("OS_ItemType", GetType(String))
            .Columns.Add("A_Qty", GetType(Double))
            .Columns.Add("A_SendDate", GetType(Date))
            .Columns.Add("A_Ver", GetType(String))
            .Columns.Add("S_SupplierNO", GetType(String))
            .Columns.Add("A_Action", GetType(String))
            .Columns.Add("A_Remark", GetType(String))
            .Columns.Add("A_Detail", GetType(String))
            .Columns.Add("A_UpdateDate", GetType(Date))
            .Columns.Add("A_Check", GetType(Boolean))
            .Columns.Add("A_CheckAction", GetType(String))
            .Columns.Add("A_CheckDate", GetType(String))
            .Columns.Add("A_CheckRemark", GetType(String))
            .Columns.Add("A_AccountCheck", GetType(Boolean))
            .Columns.Add("A_AccountCheckAction", GetType(String))
            .Columns.Add("A_AccountCheckDate", GetType(String))
            .Columns.Add("A_AccountCheckRemark", GetType(String))
            .Columns.Add("A_AccountCheckType", GetType(String))

            .Columns.Add("A_QtyType", GetType(String))

        End With

        '創建刪除數據表
        With ds.Tables.Add("DelDataOutwardAcceptance")
            .Columns.Add("A_NO", GetType(String))
        End With

        Grid.DataSource = ds.Tables("OutwardAcceptance")

    End Sub
    Function LoadData(ByVal A_AcceptanceNO As String) As Boolean
        '載入驗收單數據
        LoadData = True
        Dim objInfo As New OutwardAcceptanceInfo
        Dim objList As New List(Of OutwardAcceptanceInfo)
        Dim oc As New OutwardAcceptanceControl
        Try
            objList = oc.OutwardAcceptance_GetList(A_AcceptanceNO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If objList Is Nothing Then
                '沒有數據
                LoadData = False
                Exit Function
            End If
            ds.Tables("OutwardAcceptance").Rows.Clear()
            objList = oc.OutwardAcceptance_GetList(A_AcceptanceNO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Dim i As Integer
            Dim row As DataRow
            For i = 0 To objList.Count - 1
                '''''tb'''''
                row = ds.Tables("OutwardAcceptance").NewRow
                row("O_NO") = objList(i).O_NO
                row("M_Code") = objList(i).M_Code
                row("OS_BatchID") = objList(i).OS_BatchID
                row("PM_M_Code") = objList(i).PM_M_Code
                row("OS_ItemType") = objList(i).OS_ItemType
                row("A_Qty") = objList(i).A_Qty
                row("A_Remark") = objList(i).A_Remark
                row("A_NO") = objList(i).A_NO
                row("M_Name") = objList(i).M_Name
                row("M_Gauge") = objList(i).M_Gauge
                row("A_SendType") = objList(i).A_SendType
                row("A_QtyType") = objList(i).A_QtyType

                ds.Tables("OutwardAcceptance").Rows.Add(row)
                ''''''a''''''
                gluSupplier.Text = objList(i).S_Supplier
                txtName.Text = objList(i).A_SendNO
                ButtonEdit1.Text = objList(i).WH_ID
                DateEdit1.Text = objList(i).A_SendDate
                'CBSendType.Text = objList(i).A_SendType
                Label2.Text = objList(i).A_Ver
                Label7.Text = objList(i).A_Action
                ''''''''b''''''

                DateEdit2.Text = objList(i).A_CheckDate
                Label6.Text = objList(i).A_CheckAction
                If objList(i).A_Check = False Then
                    CheckEdit1.Checked = False
                    OldCheck = False
                Else
                    CheckEdit1.Checked = True
                    OldCheck = True
                End If
                MemoEdit1.Text = objList(i).A_CheckRemark

                ''''''c''''''

                If objList(i).A_AccCheck = False Then
                    CheckEdit2.Checked = False
                    OldAccCheck = False
                Else
                    CheckEdit2.Checked = True
                    OldAccCheck = True
                End If
                CBA_AccountCheckType.Text = objList(i).A_AccCheckType
                MemoEdit2.Text = objList(i).A_AccCheckRemark

                DateEdit3.Text = objList(i).A_CheckDate

                Label3.Text = objList(i).A_AccCheckAction

            Next


        Catch ex As Exception
            LoadData = False
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
    Sub getenable(ByVal a As Boolean, ByVal b As Boolean, ByVal c As Boolean)

        AdvBandedGridView1.Columns.Item("A_Qty").OptionsColumn.ReadOnly = b
        AdvBandedGridView1.Columns.Item("A_Remark").OptionsColumn.ReadOnly = b
        popAdd.Enabled = a
        popDel.Enabled = a
        gluSupplier.Enabled = a
        txtName.Enabled = a
        ButtonEdit1.Enabled = a
        DateEdit1.Enabled = a

        CheckEdit1.Enabled = b
        DateEdit2.Enabled = b
        MemoEdit1.Enabled = b

        CheckEdit2.Enabled = c
        CBA_AccountCheckType.Enabled = c
        DateEdit3.Enabled = c
        MemoEdit2.Enabled = c

    End Sub

    Private Sub popAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popAdd.Click
        Dim i, n As Integer
        Dim YanZeng As Boolean    '驗証,看載入各項未交數是否為0或負數
        Dim myfrm As New FormAcceptanceSelect
        Dim oc As New OutwardController
        Dim oi As New List(Of OutwardInfo)

        myfrm.ShowDialog()
        YanZeng = True
        If RefreshT = True Then
            Dim arr(n) As String
            arr = Split(tempValue, ",")
            n = Len(Replace(tempValue, ",", "," & "*")) - Len(tempValue)

            For i = 0 To n

                Dim opc As New OutwardController
                Dim opiL As List(Of OutwardInfo)
                opiL = opc.OutwardSub_GetList(Nothing, arr(i), Nothing, Nothing, Nothing)

                Dim row As DataRow = ds.Tables("OutwardAcceptance").NewRow
                row("O_NO") = opiL(0).O_NO
                row("M_Code") = opiL(0).M_Code
                row("M_Name") = opiL(0).M_Name
                row("M_Gauge") = opiL(0).M_Gauge
                row("OS_BatchID") = opiL(0).OS_BatchID
                row("PM_M_Code") = opiL(0).PM_M_Code
                row("OS_ItemType") = opiL(0).OS_ItemType
                row("A_SendType") = opiL(0).OP_NO
                'oi = opc.OutwardSub_GetList(tempValue5, arr(i), Nothing, Nothing, Nothing)
                row("A_Qty") = opiL(0).OS_NoSendQty

                If opiL(0).OS_NoSendQty <= 0 Then YanZeng = False

                Dim strSupplier As String
                strSupplier = gluSupplier.Text

                Dim oi2 As List(Of OutwardInfo)
                oi2 = opc.OutwardMain_GetList(tempValue5, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                If oi2.Count = 0 Then Exit Sub

                gluSupplier.Text = oi2(0).S_Supplier  '新增的时候填充

                If strSupplier = "" Then
                    ds.Tables("OutwardAcceptance").Rows.Add(row)
                End If
                If strSupplier <> "" Then
                    If strSupplier.Equals(gluSupplier.Text) = True Then
                        ds.Tables("OutwardAcceptance").Rows.Add(row)
                    Else
                        Dim oi1 As List(Of OutwardInfo)
                        oi1 = opc.OutwardSub_GetList(ds.Tables("OutwardAcceptance").Rows(0)("O_NO"), Nothing, Nothing, Nothing, Nothing)
                        gluSupplier.Text = oi1(0).S_Supplier
                        MsgBox("同一驗收單號中供應商必須相同！", , "警告")
                        Exit Sub
                    End If
                End If


                AdvBandedGridView1.MoveLast()
                'ButtonEdit1.Text = opiL(0).WH_ID '新增的时候填充
                'gluSupplier.Text = opiL(i).S_Supplier  '新增的时候填充
            Next

        End If
        tempValue5 = ""
        tempValue = ""
        RefreshT = False
        If YanZeng = False Then
            MsgBox("某物料未交數已經為0或負數,無法再驗收!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If
        'Dim row As DataRow = ds.Tables("OutwardAcceptance").NewRow
        'ds.Tables("OutwardAcceptance").Rows.Add(row)
    End Sub

    Private Sub popDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popDel.Click

        If AdvBandedGridView1.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = AdvBandedGridView1.GetRowCellDisplayText(ArrayToString(AdvBandedGridView1.GetSelectedRows()), "A_NO")

        If DelTemp = "A_NO" Then
        Else
            '在刪除表中增加被刪除的記錄
            Dim row As DataRow = ds.Tables("DelDataOutwardAcceptance").NewRow
            row("A_NO") = DelTemp
            ds.Tables("DelDataOutwardAcceptance").Rows.Add(row)
        End If
        ds.Tables("OutwardAcceptance").Rows.RemoveAt(CInt(ArrayToString(AdvBandedGridView1.GetSelectedRows())))
        If AdvBandedGridView1.RowCount = 0 Then
            gluSupplier.Text = Nothing
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckData() = False Then Exit Sub

        Select Case Label5.Text
            Case "OutwardAcceptanceAddEdit"
                Select Case Edit
                    Case False
                        SaveNew()
                    Case True
                        SaveEdit()
                End Select
            Case "OutwardAcceptanceCheck"
                '驗收時
                If OldCheck = False Then
                    If CheckEdit1.Checked = False Then
                        MsgBox("請先更改驗收狀態,才能保存!", MsgBoxStyle.OkOnly)
                        Exit Sub
                    End If

                End If

                If OldCheck = True Then
                    If CheckEdit1.Checked = True Then
                        MsgBox("請先更改驗收狀態,才能保存!", MsgBoxStyle.OkOnly)
                        Exit Sub
                    End If
                End If
                SaveCheck()
            Case "OutwardAcceptanceAccCheck"
                '復核
                SaveAccCheck()
        End Select
        'If MTypeName = "OutwardAcceptanceAddEdit" Then
        '    '驗收單新增修改

        '    Select Case Edit

        '        Case False
        '            SaveNew()
        '        Case True
        '            SaveEdit()
        '    End Select
        'End If

        'If MTypeName = "OutwardAcceptanceCheck" Then
        '    '驗收時()
        '    SaveCheck()
        'End If

        'If MTypeName = "OutwardAcceptanceAccCheck" Then
        '    '復核
        '    SaveAccCheck()
        'End If
    End Sub
    Private Sub SaveNew()
        Dim i As Integer
        Dim ac As New OutwardAcceptanceControl
        Dim ai As New OutwardAcceptanceInfo

        txtQuoID.Text = GetAcceptanceNO()

        If txtName.Text = "" Then
            MsgBox("驗收必須有送貨單號！")
            Exit Sub
        End If

        ai.A_AcceptanceNO = txtQuoID.Text
        ai.WH_ID = ButtonEdit1.Text
        ai.A_Ver = Label2.Text
        ai.A_SendDate = DateEdit1.EditValue
        ai.S_Supplier = gluSupplier.EditValue
        ai.A_Action = Label7.Text
        ai.A_SendNO = txtName.Text
     
        For i = 0 To ds.Tables("OutwardAcceptance").Rows.Count - 1

            ai.A_NO = GetAcceptanceA_NO()

            ai.O_NO = ds.Tables("OutwardAcceptance").Rows(i)("O_NO")
            ai.M_Code = ds.Tables("OutwardAcceptance").Rows(i)("M_Code")

            If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("OS_BatchID")) Then
                ai.OS_BatchID = Nothing
            Else
                ai.OS_BatchID = ds.Tables("OutwardAcceptance").Rows(i)("OS_BatchID")
            End If

            If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("PM_M_Code")) Then
                ai.PM_M_Code = Nothing
            Else
                ai.PM_M_Code = ds.Tables("OutwardAcceptance").Rows(i)("PM_M_Code")
            End If

            If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("OS_ItemType")) Then
                ai.OS_ItemType = Nothing
            Else
                ai.OS_ItemType = ds.Tables("OutwardAcceptance").Rows(i)("OS_ItemType")
            End If

            If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_SendType")) Then
                MsgBox("送貨類型不能為空！")
                Exit Sub
            Else
                ai.A_SendType = ds.Tables("OutwardAcceptance").Rows(i)("A_SendType")
            End If
            If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_Qty")) Then
                ai.A_Qty = "0"
            Else
                ai.A_Qty = ds.Tables("OutwardAcceptance").Rows(i)("A_Qty")
            End If
            If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_QtyType")) Then
                MsgBox("送貨數量類型不能為空！")
                Exit Sub
            Else
                ai.A_QtyType = ds.Tables("OutwardAcceptance").Rows(i)("A_QtyType")
            End If

            If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_Remark")) Then
                ai.A_Remark = Nothing
            Else
                ai.A_Remark = ds.Tables("OutwardAcceptance").Rows(i)("A_Remark")
            End If
            ai.A_UpdateDate = Format(Now, "yyyy/MM/dd")
            ai.A_Detail = "暫收"
            ac.OutwardAcceptance_Add(ai)
        Next
        MsgBox("已保存,單號: " & txtQuoID.Text & " ")
        Me.Close()
    End Sub

    Private Sub SaveEdit()

        Dim i As Integer
        Dim ac As New OutwardAcceptanceControl
        Dim ai As New OutwardAcceptanceInfo

        If txtName.Text = "" Then
            MsgBox("驗收必須有送貨單號！")
            Exit Sub
        End If

        ai.A_AcceptanceNO = txtQuoID.Text
        ai.WH_ID = ButtonEdit1.Text
        ai.A_Ver = Label2.Text
        ai.A_SendDate = DateEdit1.Text
        ai.A_SendNO = txtName.Text
        ai.S_Supplier = gluSupplier.EditValue
        ai.A_Action = InUserID


        For i = 0 To ds.Tables("OutwardAcceptance").Rows.Count - 1
            If Not IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_NO")) Then
                '如果只是修改

                ai.O_NO = ds.Tables("OutwardAcceptance").Rows(i)("O_NO")

                ai.A_NO = ds.Tables("OutwardAcceptance").Rows(i)("A_NO")


                ai.M_Code = ds.Tables("OutwardAcceptance").Rows(i)("M_Code")

                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("OS_BatchID")) Then
                    ai.OS_BatchID = Nothing
                Else
                    ai.OS_BatchID = ds.Tables("OutwardAcceptance").Rows(i)("OS_BatchID")
                End If

                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("PM_M_Code")) Then
                    ai.PM_M_Code = Nothing
                Else
                    ai.PM_M_Code = ds.Tables("OutwardAcceptance").Rows(i)("PM_M_Code")
                End If
                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("OS_ItemType")) Then
                    ai.OS_ItemType = Nothing
                Else
                    ai.OS_ItemType = ds.Tables("OutwardAcceptance").Rows(i)("OS_ItemType")
                End If
                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_SendType")) Then
                    MsgBox("送貨類型不能為空！")
                    Exit Sub
                Else
                    ai.A_SendType = ds.Tables("OutwardAcceptance").Rows(i)("A_SendType")
                End If
                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_Qty")) Then
                    ai.A_Qty = "0"
                Else
                    ai.A_Qty = ds.Tables("OutwardAcceptance").Rows(i)("A_Qty")
                End If
                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_QtyType")) Then
                    MsgBox("送貨數量類型不能為空！")
                    Exit Sub
                Else
                    ai.A_QtyType = ds.Tables("OutwardAcceptance").Rows(i)("A_QtyType")
                End If
                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_Remark")) Then
                    ai.A_Remark = Nothing
                Else
                    ai.A_Remark = ds.Tables("OutwardAcceptance").Rows(i)("A_Remark")
                End If
                ai.A_UpdateDate = Now
                ai.A_Detail = "暫收"
                ac.OutwardAcceptance_Update(ai)
            End If


            If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_NO")) Then
                '如果是新增加
                ai.A_NO = GetAcceptanceA_NO()

                ai.O_NO = ds.Tables("OutwardAcceptance").Rows(i)("O_NO")
                ai.M_Code = ds.Tables("OutwardAcceptance").Rows(i)("M_Code")

                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("OS_BatchID")) Then
                    ai.OS_BatchID = Nothing
                Else
                    ai.OS_BatchID = ds.Tables("OutwardAcceptance").Rows(i)("OS_BatchID")
                End If

                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("PM_M_Code")) Then
                    ai.PM_M_Code = Nothing
                Else
                    ai.PM_M_Code = ds.Tables("OutwardAcceptance").Rows(i)("PM_M_Code")
                End If
                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("OS_ItemType")) Then
                    ai.OS_ItemType = Nothing
                Else
                    ai.OS_ItemType = ds.Tables("OutwardAcceptance").Rows(i)("OS_ItemType")
                End If

                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_SendType")) Then
                    MsgBox("送貨類型不能為空！")
                    Exit Sub
                Else
                    ai.A_SendType = ds.Tables("OutwardAcceptance").Rows(i)("A_SendType")
                End If
                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_Qty")) Then
                    ai.A_Qty = "0"
                Else
                    ai.A_Qty = ds.Tables("OutwardAcceptance").Rows(i)("A_Qty")
                End If
                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_QtyType")) Then
                    MsgBox("送貨數量類型不能為空！")
                    Exit Sub
                Else
                    ai.A_QtyType = ds.Tables("OutwardAcceptance").Rows(i)("A_QtyType")
                End If

                If IsDBNull(ds.Tables("OutwardAcceptance").Rows(i)("A_Remark")) Then
                    ai.A_Remark = Nothing
                Else
                    ai.A_Remark = ds.Tables("OutwardAcceptance").Rows(i)("A_Remark")
                End If

                ai.A_UpdateDate = Now
                ai.A_Detail = "暫收"
                ac.OutwardAcceptance_Add(ai)
            End If
        Next

        '更新刪除的記錄
        If ds.Tables("DelDataOutwardAcceptance").Rows.Count > 0 Then
            For i = 0 To ds.Tables("DelDataOutwardAcceptance").Rows.Count - 1
                Dim odc As New OutwardAcceptanceControl
                Dim odi As New OutwardAcceptanceInfo
                odi.A_NO = ds.Tables("DelDataOutwardAcceptance").Rows(i)("A_NO")
                odc.OutwardAcceptance_Delete(txtQuoID.Text, odi.A_NO)
            Next i
        End If

        MsgBox("已修改！,單號: " & txtQuoID.Text & "")
        Me.Close()
    End Sub

    Private Sub SaveCheck()
        '驗收
        Dim i As Integer
        Dim mt As New SharePurchaseController
        Dim mm As New SharePurchaseInfo

        For i = 0 To ds.Tables("OutwardAcceptance").Rows.Count - 1
            Dim ac As New OutwardAcceptanceControl
            Dim ai As New OutwardAcceptanceInfo

            ai.A_AcceptanceNO = txtQuoID.Text
            ai.A_Check = CheckEdit1.Checked
            ai.A_CheckDate = DateEdit2.Text
            ai.A_CheckAction = InUserID
            ai.A_CheckRemark = MemoEdit1.Text

            If CheckEdit1.Checked = False Then
                ai.A_Detail = "暫收"

            ElseIf CheckEdit1.Checked = True Then
                ai.A_Detail = "驗收"

            End If

            ac.OutwardAcceptance_UpdateCheck(ai)

        Next
        Dim j As Integer
        For j = 0 To ds.Tables("OutwardAcceptance").Rows.Count - 1
         
            Dim ac As New OutwardAcceptanceControl
            Dim ai As New OutwardInfo
            '----------------------------------------------------------------------暫時停用倉庫操作(入庫，出庫，扣數等)
            'Dim spc As New SharePurchaseController
            'Dim spi As New SharePurchaseInfo

            'spi.WH_ID = ButtonEdit1.EditValue
            'spi.M_Code = ds.Tables("OutwardAcceptance").Rows(j)("M_Code")

            'Dim Qty As Single
            'Dim wi As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            'Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            'wi = wc.WareInventory_GetSub(ds.Tables("OutwardAcceptance").Rows(j)("M_Code"), ButtonEdit1.EditValue)

            'If wi Is Nothing Then
            '    Qty = 0
            'Else
            '    Qty = wi.WI_Qty
            'End If

            'If CheckEdit1.Checked = False Then
            '    spi.WI_Qty = Qty - CSng(ds.Tables("OutwardAcceptance").Rows(j)("A_Qty"))
            'Else
            '    spi.WI_Qty = Qty + CSng(ds.Tables("OutwardAcceptance").Rows(j)("A_Qty"))
            'End If

            'mt.UpdateWareInventory_WIQty2(spi) '仓库加减

            ai.O_NO = ds.Tables("OutwardAcceptance").Rows(j)("O_NO")
            ai.M_Code = ds.Tables("OutwardAcceptance").Rows(j)("M_Code")
            ai.OS_BatchID = ds.Tables("OutwardAcceptance").Rows(j)("OS_BatchID")

            ac.Outward_UpdateNoSendQty(ai)    '外发单未送貨數量加减



        Next

        MsgBox("驗收狀態已改變！")
        Me.Close()

    End Sub

    Private Sub SaveAccCheck()
        '審核
        Dim i As Integer
        For i = 0 To ds.Tables("OutwardAcceptance").Rows.Count - 1
            Dim ac As New OutwardAcceptanceControl
            Dim ai As New OutwardAcceptanceInfo
            ai.A_AcceptanceNO = txtQuoID.Text
            ai.A_AccCheck = CheckEdit2.Checked
            ai.A_AccCheckType = CBA_AccountCheckType.Text
            ai.A_AccCheckDate = DateEdit3.Text
            ai.A_AccCheckAction = InUserID
            ai.A_AccCheckRemark = MemoEdit2.Text

            ac.OutwardAcceptance_UpdateAccCheck(ai)
        Next
        MsgBox("審核狀態已改變！")
        Me.Close()
    End Sub

    Private Function CheckData() As Boolean
        CheckData = False

        If ButtonEdit1.Text = "" Or DateEdit1.Text = "" Then
            MsgBox("請填寫暫收日期及收貨倉庫")
            Exit Function
        End If
        If gluSupplier.Text = "" Then
            MsgBox("请填写供应商!")
            Exit Function
        End If
        If Label5.Text = "OutwardAcceptanceAddEdit" Then
            If ds.Tables("OutwardAcceptance").Rows.Count = 0 Then
                MsgBox("沒有數據,無法保存!")
                Exit Function
            End If
            For i As Integer = 0 To ds.Tables("OutwardAcceptance").Rows.Count - 1
                If ds.Tables("OutwardAcceptance").Rows(i)("A_Qty") <= 0 Then
                    MsgBox("請填寫驗收數量", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                    Exit Function
                End If
            Next
        End If

        If Label5.Text = "OutwardAcceptanceCheck" Then
            '驗收時
            If DateEdit2.Text = "" Then
                MsgBox("請填寫驗收日期", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                Exit Function
            End If

            If OldCheck = True Then
                If CheckEdit1.Checked = True Then
                    MsgBox("請先更改驗收狀態,才能保存!", MsgBoxStyle.OkOnly)
                    Exit Function
                End If

            End If
            If OldCheck = False Then
                If CheckEdit1.Checked = False Then
                    MsgBox("請先更改驗收狀態,才能保存!", MsgBoxStyle.OkOnly)
                    Exit Function
                End If
            End If
        End If

        If Label5.Text = "OutwardAcceptanceAccCheck" Then
            '審核時

            If CBA_AccountCheckType.Text = "" Then
                MsgBox("請填審核類型", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                Exit Function
            End If

            If DateEdit3.Text = "" Then
                MsgBox("請填寫審核日期", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                Exit Function
            End If

            If OldAccCheck = True Then
                If CheckEdit2.Checked = True Then
                    MsgBox("請先更改審核狀態,才能保存!", MsgBoxStyle.OkOnly)
                    Exit Function
                End If

            End If
            If OldAccCheck = False Then
                If CheckEdit2.Checked = False Then
                    MsgBox("請先更改審核狀態,才能保存!", MsgBoxStyle.OkOnly)
                    Exit Function
                End If
            End If
        End If
        CheckData = True
    End Function

    Function GetAcceptanceNO() As String
        '生成新的驗收單號
        Dim str1, str2 As String
        Dim ac As New OutwardAcceptanceControl
        Dim ai As New OutwardAcceptanceInfo

        str1 = Mid(Year(Now), 3)
        If CInt(Month(Now)) < 10 Then
            str2 = "0" & Month(Now)
        Else
            str2 = Month(Now)
        End If

        Dim Stra As String
        Stra = "AC" & str1 & str2

        ai = ac.OutwardAcceptance_GetNO(Stra)
        If ai Is Nothing Then
            GetAcceptanceNO = "AC" & str1 & str2 & "0001"
        Else
            GetAcceptanceNO = "AC" & str1 & str2 & Mid((CInt(Mid(ai.A_AcceptanceNO, 7)) + 10001), 2)
        End If
    End Function

    Function GetAcceptanceA_NO() As String
        '生成新的验收流水号

        Dim ac As New OutwardAcceptanceControl
        Dim ai As New OutwardAcceptanceInfo
        Dim Stra As String
        Stra = "A"
        ai = ac.OutwardAcceptance_GetNum(Stra)
        If ai Is Nothing Then
            GetAcceptanceA_NO = "A" & "00000001"
        Else
            GetAcceptanceA_NO = "A" & Mid((CInt(Mid(ai.A_NO, 2)) + 100000001), 2)
        End If
    End Function

    Private Sub cmdBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBarCode.Click

        tempValue3 = "外發加工驗收作業"
        tempValue4 = txtQuoID.Text
        Dim myfrm As New frmBarCode
        myfrm.ShowDialog()
    End Sub

    Private Sub AdvBandedGridView1_CellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles AdvBandedGridView1.CellValueChanged

        If e.Column.FieldName = "A_AcceptanceNO" Then

        End If

    End Sub

    Private Sub popBatchAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popBatchAdd.Click
        tempValue5 = ButtonEdit1.EditValue
        frmAcceptanceBatchIDLoad.ShowDialog()

        If tempValue = "" Or tempValue2 = "" Or tempValue3 = "" Then
            Exit Sub
        Else
            AddRow(tempValue, tempValue2, tempValue3, tempValue4)

            tempValue = Nothing
            tempValue2 = Nothing
            tempValue3 = Nothing
            tempValue4 = Nothing
        End If

    End Sub

    Sub AddRow(ByVal O_NO As String, ByVal BatchID As String, ByVal M_Code As String, ByVal StrQty As String)
        Dim row As DataRow
        row = ds.Tables("OutwardAcceptance").NewRow

        If M_Code = "" Then

        Else

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(M_Code)

            row("O_NO") = O_NO
            row("M_Code") = objInfo.M_Code
            row("M_Name") = objInfo.M_Name

            row("M_Gauge") = objInfo.M_Gauge

            row("OS_BatchID") = BatchID

            Dim osc As New Library.Orders.OrdersSubController
            Dim osi As List(Of Library.Orders.OrdersSubInfo)

            osi = osc.OrdersSub_GetList(Nothing, BatchID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            row("PM_M_Code") = osi(0).PM_M_Code

            Dim oi As List(Of OutwardInfo)
            Dim oc As New OutwardController

            oi = oc.OutwardSub_GetList(O_NO, Nothing, M_Code, Nothing, BatchID)

            row("OS_ItemType") = oi(0).OS_ItemType
            row("A_SendType") = oi(0).OP_NO
            row("A_Qty") = StrQty

            ds.Tables("OutwardAcceptance").Rows.Add(row)

            Dim oi2 As List(Of OutwardInfo)
            oi2 = oc.OutwardMain_GetList(O_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            gluSupplier.Text = oi2(0).S_Supplier '導入外發單中供應商編號

            AdvBandedGridView1.MoveLast()
        End If

    End Sub

End Class