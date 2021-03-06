'Imports LFERP.Library.Production.ProductionOutWard
Imports LFERP.Library.ProductionDPTWareInventory
Imports LFERP.Library.ProductionOutProcess
Imports LFERP.Library.ProductionOWPAcceptance
Imports LFERP.Library.ProductProcess

Public Class FrmProductionOWPAcceptance
    Dim ds As New DataSet
    Dim OldCheck As Boolean
    Dim OldAccCheck As Boolean

    Dim Get_Next_Process_NO As String
    Dim Get_Next_Process_Name As String

    Dim strPM_M_Code As String
    Dim strPM_Type As String


    Private Sub FrmProductionOWPAcceptance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        GluDep.EditValue = "外發部" ''固定為外發部 F101
        GluDep.Enabled = False


        Dim mtd As New LFERP.DataSetting.SuppliersControler
        gluSupplier.Properties.DisplayMember = "S_SupplierName"
        gluSupplier.Properties.ValueMember = "S_Supplier"
        gluSupplier.Properties.DataSource = mtd.GetSuppliersList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "True")

        A_AcceptanceNO_ID.Text = tempValue2
        CaoTypeLabel.Text = MTypeName '

        tempValue2 = ""
        MTypeName = ""

        CreateTables()

        XtraTabPage3.PageVisible = False

        Select Case CaoTypeLabel.Text

            Case "OWPAddEdit"
                '驗收單新增或修改
                getenable(True, False, False)

                A_Action_Edit.Text = ""
                XtraTabPage2.PageVisible = False

                Select Case Edit
                    Case False
                        A_Action_Edit.Text = UserName
                        A_SendDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
                        VerLabel.Text = "1"
                        A_AcceptanceNO_ID.Text = ""
                        Me.Text = "外發驗收--暫收"
                    Case True
                        If LoadData(A_AcceptanceNO_ID.Text) = False Then Exit Sub
                        VerLabel.Text = CStr(CInt(VerLabel.Text) + 1)
                        A_SendDateEdit.Enabled = False
                        Me.Text = "外發驗收--修改" & "[" & A_AcceptanceNO_ID.Text & "]"
                End Select
            Case "OWPCheck"
                '驗收
                XtraTabControl1.SelectedTabPage = XtraTabPage2

                getenable(False, True, False)
                ASend_NOText.Enabled = False
                A_SendDateEdit.Enabled = False
                If LoadData(A_AcceptanceNO_ID.Text) = False Then Exit Sub

                CheckDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
                Me.Text = "外發驗收--審核" & "[" & A_AcceptanceNO_ID.Text & "]"
            Case "OWPAccCheck"  ''不用此處了
                '審核
                XtraTabControl1.SelectedTabPage = XtraTabPage3

                getenable(False, False, True)

                If LoadData(A_AcceptanceNO_ID.Text) = False Then Exit Sub
                AccDateEdit.EditValue = Format(Now, "yyyy/MM/dd")
                Me.Text = "外發驗收--復核" & "[" & A_AcceptanceNO_ID.Text & "]"
            Case "OWPView"
                getenable(False, False, False)
                If LoadData(A_AcceptanceNO_ID.Text) = False Then Exit Sub
                '查看
                GridView1.Columns.Item("A_Remark").OptionsColumn.ReadOnly = True
                GridView1.Columns.Item("A_OK_Qty").OptionsColumn.ReadOnly = True
                GridView1.Columns.Item("A_QQ_Qty").OptionsColumn.ReadOnly = True
                GridView1.Columns.Item("A_TC_Qty").OptionsColumn.ReadOnly = True
                GridView1.Columns.Item("A_QT_Qty").OptionsColumn.ReadOnly = True

                cmdSave.Visible = False
                Me.Text = "外發驗收--查看" & "[" & A_AcceptanceNO_ID.Text & "]"
        End Select

        GridView1.Columns.Item("OPAutoID").Visible = False
        GridView1.Columns.Item("A_NO").Visible = False
        GridView1.Columns.Item("A_PS_NO").Visible = False
        GridView1.Columns.Item("A_PS_NO_Next").Visible = False
        GridView2.Columns.Item("N_PS_NO").Visible = False



        ASend_NOText.Focus()
        ASend_NOText.Select()

    End Sub


    Sub getenable(ByVal a As Boolean, ByVal b As Boolean, ByVal c As Boolean)

        popAdd.Enabled = a
        popDel.Enabled = a
        ' gluSupplier.Enabled = a

        CheckEdit.Enabled = b
        ' CheckDateEdit.Enabled = b
        CheckMemoEdit.Enabled = b

        GridView1.Columns.Item("A_Remark").OptionsColumn.ReadOnly = b
        GridView1.Columns.Item("A_OK_Qty").OptionsColumn.ReadOnly = b
        GridView1.Columns.Item("A_QQ_Qty").OptionsColumn.ReadOnly = b
        GridView1.Columns.Item("A_TC_Qty").OptionsColumn.ReadOnly = b
        GridView1.Columns.Item("A_QT_Qty").OptionsColumn.ReadOnly = b
        Grid2.Enabled = Not b

        AccCheck.Enabled = c
        CBA_AccountCheckType.Enabled = c
        AccDateEdit.Enabled = c
        AccMemoEdit.Enabled = c

    End Sub
    ''' <summary>
    ''' 創建數據表
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("ProductionOWPAcceptance")
            .Columns.Add("OPAutoID", GetType(String))

            .Columns.Add("A_NO", GetType(String))
            .Columns.Add("A_AcceptanceNO", GetType(String))

            .Columns.Add("O_NO", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
            .Columns.Add("A_PS_Name", GetType(String))
            .Columns.Add("A_PS_NO", GetType(String))
            .Columns.Add("A_OW_Do", GetType(String)) ' 加工要求

            .Columns.Add("A_PS_Name_Next", GetType(String))
            .Columns.Add("A_PS_NO_Next", GetType(String))

            .Columns.Add("A_OK_Qty", GetType(String))
            .Columns.Add("A_QQ_Qty", GetType(String))
            .Columns.Add("A_TC_Qty", GetType(String))
            .Columns.Add("A_QT_Qty", GetType(String))
            .Columns.Add("A_Remark", GetType(String)) '
            .Columns.Add("PM_JiYu", GetType(String)) 'PM_JiYu

        End With

        Grid1.DataSource = ds.Tables("ProductionOWPAcceptance")

        '創建刪除數據表
        With ds.Tables.Add("DelProductionOWPAcceptance")
            .Columns.Add("A_NO", GetType(String))
            .Columns.Add("A_AcceptanceNO", GetType(String))
        End With
        ''2012-4-10
        '選擇下一工序編號
        With ds.Tables.Add("ProcessNext")
            .Columns.Add("N_PS_NO", GetType(String))
            .Columns.Add("N_PS_Name", GetType(String))
        End With

        ''2012-04-12加
        Grid2.DataSource = ds.Tables("ProcessNext")
    End Sub
    ''' <summary>
    ''' 添加數據add
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub SaveNew()
        Dim i As Integer
        Dim ac As New ProductionOWPAcceptanceControl
        Dim ai As New ProductionOWPAcceptanceInfo

        A_AcceptanceNO_ID.Text = GetWOPAcceptanceNO() ''獲取驗收單號

        If A_AcceptanceNO_ID.Text = "" Then
            MsgBox("驗收單號獲取失敗，請檢查")
            Exit Sub
        End If

        ai.A_AcceptanceNO = A_AcceptanceNO_ID.Text

        ai.ASend_NO = ASend_NOText.Text
        ai.DPT_ID = "F101"
        ai.A_SendDate = A_SendDateEdit.Text

        ' A_Action_Edit.Text = InUserID
        'ai.A_Action = A_Action_Edit.Text
        ai.A_Action = InUserID
        ai.S_Supplier = gluSupplier.EditValue

        ai.A_Ver = CInt(VerLabel.Text)

        For i = 0 To ds.Tables("ProductionOWPAcceptance").Rows.Count - 1
            ai.OPAutoID = ds.Tables("ProductionOWPAcceptance").Rows(i)("OPAutoID") '外發單中的自動編號ID

            ai.A_NO = GetWOPAcceptancetNum()
            '  ai.A_NO = "A00000001" ''測試

            ai.O_NO = ds.Tables("ProductionOWPAcceptance").Rows(i)("O_NO") ''流水號

            ai.PM_M_Code = ds.Tables("ProductionOWPAcceptance").Rows(i)("PM_M_Code")
            ai.PM_Type = ds.Tables("ProductionOWPAcceptance").Rows(i)("PM_Type")

            ai.PS_NO = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_PS_NO") '發料工序
            ai.PS_NO_Next = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_PS_NO_Next") '收料工序

            If IsDBNull(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_OK_Qty")) Then
                ai.A_OK_Qty = 0
            Else
                ai.A_OK_Qty = Int(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_OK_Qty"))
            End If

            If IsDBNull(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_QQ_Qty")) Then
                ai.A_QQ_Qty = 0
            Else
                ai.A_QQ_Qty = Int(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_QQ_Qty"))
            End If

            If IsDBNull(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_TC_Qty")) Then
                ai.A_TC_Qty = 0
            Else
                ai.A_TC_Qty = Int(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_TC_Qty"))
            End If

            If IsDBNull(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_QT_Qty")) Then
                ai.A_QT_Qty = 0
            Else
                ai.A_QT_Qty = Int(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_QT_Qty"))
            End If

            If IsDBNull(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_Remark")) Then
                ai.A_Remark = Nothing
            Else
                ai.A_Remark = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_Remark")
            End If

            If IsDBNull(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_OW_Do")) Then
                ai.A_OW_Do = Nothing
            Else
                ai.A_OW_Do = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_OW_Do")
            End If


            ai.A_UpdateDate = Format(Now, "yyyy/MM/dd")
            ai.A_Detail = "暫收"
            If ac.ProductionOWPAcceptance_Add(ai) = False Then
                ' MsgBox(ai.A_PS_Name & "工序保存失敗")
                Exit Sub
            End If
        Next

        MsgBox("已保存,單號: " & A_AcceptanceNO_ID.Text & " ")

        Me.Close()
    End Sub
    ''' <summary>
    ''' 修改數據保存
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveEdit()

        Dim i As Integer
        Dim ac As New ProductionOWPAcceptanceControl
        Dim ai As New ProductionOWPAcceptanceInfo

        ai.A_AcceptanceNO = A_AcceptanceNO_ID.Text
        ai.ASend_NO = ASend_NOText.Text
        ai.DPT_ID = "F101"
        ai.A_SendDate = A_SendDateEdit.Text
        ai.A_CheckAction = A_Action_Edit.Text
        ai.S_Supplier = gluSupplier.EditValue
        ai.A_Action = InUserID

        ai.A_Ver = CInt(VerLabel.Text)

        For i = 0 To ds.Tables("ProductionOWPAcceptance").Rows.Count - 1
            '如果只是修改
            ' ai.A_AcceptanceNO = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_AcceptanceNO")
            ai.OPAutoID = ds.Tables("ProductionOWPAcceptance").Rows(i)("OPAutoID") '外發單中的自動編號ID

            ai.O_NO = ds.Tables("ProductionOWPAcceptance").Rows(i)("O_NO")
            ai.PM_M_Code = ds.Tables("ProductionOWPAcceptance").Rows(i)("PM_M_Code")
            ai.PM_Type = ds.Tables("ProductionOWPAcceptance").Rows(i)("PM_Type")

            ai.PS_NO = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_PS_NO") ''發料工序
            ai.PS_NO_Next = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_PS_NO_Next") '收料工序

            If IsDBNull(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_OK_Qty")) Then
                ai.A_OK_Qty = 0
            Else
                ai.A_OK_Qty = Int(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_OK_Qty"))
            End If

            If IsDBNull(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_QQ_Qty")) Then
                ai.A_QQ_Qty = 0
            Else
                ai.A_QQ_Qty = Int(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_QQ_Qty"))
            End If

            If IsDBNull(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_TC_Qty")) Then
                ai.A_TC_Qty = 0
            Else
                ai.A_TC_Qty = Int(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_TC_Qty"))
            End If

            If IsDBNull(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_QT_Qty")) Then
                ai.A_QT_Qty = 0
            Else
                ai.A_QT_Qty = Int(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_QT_Qty"))
            End If

            ai.A_Remark = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_Remark")

            ai.A_OW_Do = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_OW_Do")

            ai.A_UpdateDate = Format(Now, "yyyy/MM/dd")
            ai.A_Detail = "暫收"

            If IsDBNull(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_NO")) Then
                ai.A_NO = GetWOPAcceptancetNum()
                ac.ProductionOWPAcceptance_Add(ai)
            Else
                ai.A_NO = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_NO")
                ac.ProductionOWPAcceptance_Update(ai)
            End If
        Next

        '更新刪除的記錄
        If ds.Tables("DelProductionOWPAcceptance").Rows.Count > 0 Then
            For i = 0 To ds.Tables("DelProductionOWPAcceptance").Rows.Count - 1
                Dim acd As New ProductionOWPAcceptanceControl
                Dim aid As New ProductionOWPAcceptanceInfo
                aid.A_NO = ds.Tables("DelProductionOWPAcceptance").Rows(i)("A_NO")
                acd.ProductionOWPAcceptance_Delete(A_AcceptanceNO_ID.Text, aid.A_NO)
            Next i
        End If

        MsgBox("已修改！,單號: " & A_AcceptanceNO_ID.Text & "")
        Me.Close()
    End Sub
    ''' <summary>
    ''' 驗收作業 (更新部門庫存表)(???)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveCheck()
        '驗收
        Dim i As Integer
        Dim ac As New ProductionOWPAcceptanceControl
        Dim ai As New ProductionOWPAcceptanceInfo

        ai.A_AcceptanceNO = A_AcceptanceNO_ID.Text
        ai.A_Check = CheckEdit.Checked
        ai.A_CheckDate = CheckDateEdit.Text
        ai.A_CheckAction = InUserID
        ai.A_CheckRemark = CheckMemoEdit.Text

        If CheckEdit.Checked = False Then
            ai.A_Detail = "暫收"
        ElseIf CheckEdit.Checked = True Then
            ai.A_Detail = "驗收"
        End If


        ''------------------------------------------更新部門庫存表---------------------
        Dim pdi As List(Of ProductionDPTWareInventoryInfo)
        Dim pdc As New ProductionDPTWareInventoryControl

        Dim pdi1 As List(Of ProductionDPTWareInventoryInfo)
        Dim pdc1 As New ProductionDPTWareInventoryControl

        For i = 0 To ds.Tables("ProductionOWPAcceptance").Rows.Count - 1

            ai.A_NO = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_NO")
            If ac.ProductionOWPAcceptance_UpdateCheck(ai) = True Then
                'MsgBox("審核完成!")
            Else
                MsgBox("審核失敗,請檢查原因!")
                Exit Sub
            End If
            '' ---------------------------2012-5-25-在触發器中加在 ProductionField條報費記錄要一條一條的更新----------------------------------
            '驗收中"其它"數量 加到驗前即外發時的工序中，又要在 報費時數量又要減去，所以在驗收時，"其它"數量不做操作 
            '在取消驗收亦是如此 其它數量在物料收“報費時減” 在“外發驗收中加”  所以對其它數量在驗收整個過程中不加不減

            Dim strQty As Integer '發料工序原有數量
            Dim strQty_Next As Integer '收料工序原有數量

            Dim strQtyAdd As Integer
            Dim A_OK_Qty_, A_QQ_Qty_, A_TC_Qty_, A_QT_Qty_ As Double

            strQtyAdd = 0
            ''更新發料工序庫存表-----------------------------------------------------------------------------------------------------
            pdi = pdc.ProductionDPTWareInventory_GetList("F101", ds.Tables("ProductionOWPAcceptance").Rows(i)("A_PS_NO"), Nothing)

            If pdi.Count = 0 Then
                strQty = 0
            Else
                strQty = pdi(0).WI_Qty '原存有大貨數量
            End If

            Dim di As New ProductionDPTWareInventoryInfo
            di.DPT_ID = "F101"
            di.M_Code = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_PS_NO")

            A_OK_Qty_ = CInt(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_OK_Qty"))
            A_QQ_Qty_ = CInt(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_QQ_Qty"))
            A_TC_Qty_ = CInt(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_TC_Qty"))
            A_QT_Qty_ = CInt(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_QT_Qty"))

            strQtyAdd = A_OK_Qty_ + A_QQ_Qty_ + A_TC_Qty_ + A_QT_Qty_  ''用來扣未交數-----

            If ai.A_Check = True Then
                di.WI_Qty = strQty + A_QQ_Qty_ + A_TC_Qty_
            Else
                di.WI_Qty = strQty - (A_QQ_Qty_ + A_TC_Qty_)
            End If

            pdc.UpdateProductionField_Qty(di)

            ''更新收料工序表，下一工序-----------------------------------------------------------------------
            pdi1 = pdc1.ProductionDPTWareInventory_GetList("F101", ds.Tables("ProductionOWPAcceptance").Rows(i)("A_PS_NO_Next"), Nothing)
            If pdi1.Count = 0 Then
                strQty_Next = 0
            Else
                strQty_Next = pdi1(0).WI_Qty '原存有大貨數量
            End If

            Dim di1 As New ProductionDPTWareInventoryInfo
            di1.DPT_ID = "F101"
            di1.M_Code = ds.Tables("ProductionOWPAcceptance").Rows(i)("A_PS_NO_Next")

            If ai.A_Check = True Then
                di1.WI_Qty = strQty_Next + A_OK_Qty_
            Else
                di1.WI_Qty = strQty_Next - A_OK_Qty_
            End If

            pdc1.UpdateProductionField_Qty(di1)
            '------------------------未交數---------------------------------------------------------------------

            Dim strQty_No As Double
            Dim oili As New List(Of ProductionOutProcessInfo)
            Dim ocli As New ProductionOutProcessControl

            Dim oc As New ProductionOWPAcceptanceControl
            Dim oi As New ProductionOWPAcceptanceInfo

            oili = ocli.ProductionOutProcess_GetList(ds.Tables("ProductionOWPAcceptance").Rows(i)("OPAutoID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If oili.Count <= 0 Then
                strQty_No = 0
            Else
                strQty_No = oili(0).PO_NoSendQty
            End If

            oi.OPAutoID = ds.Tables("ProductionOWPAcceptance").Rows(i)("OPAutoID")
            oi.O_NO = ds.Tables("ProductionOWPAcceptance").Rows(i)("O_NO")

            If ai.A_Check = True Then
                oi.PO_NoSendQty = strQty_No - strQtyAdd
            Else
                oi.PO_NoSendQty = strQty_No + strQtyAdd
            End If

            oc.ProductionOWPAcceptance_NoSendQty(oi)
        Next

        MsgBox("驗收狀態已改變！")
        Me.Close()

    End Sub
    ''' <summary>
    ''' 審核
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveAccCheck()
        '審核
        Dim i As Integer
        For i = 0 To ds.Tables("ProductionOWPAcceptance").Rows.Count - 1
            Dim ac As New ProductionOWPAcceptanceControl
            Dim ai As New ProductionOWPAcceptanceInfo
            ai.A_AcceptanceNO = A_AcceptanceNO_ID.Text
            ai.A_AccCheck = AccCheck.Checked
            ai.A_AccCheckType = CBA_AccountCheckType.Text
            ai.A_AccCheckDate = AccDateEdit.Text
            ai.A_AccCheckAction = InUserID
            ai.A_ACCCheckRemark = AccMemoEdit.Text

            ac.ProductionOWPAcceptance_UpdateAccCheck(ai)
        Next
        MsgBox("審核狀態已改變！")
        Me.Close()
    End Sub

    ''' <summary>   
    ''' '生成新的驗收單號
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetWOPAcceptanceNO() As String
        GetWOPAcceptanceNO = ""

        Dim str1, str2 As String
        Dim ac As New ProductionOWPAcceptanceControl
        Dim ai As New ProductionOWPAcceptanceInfo

        str1 = Mid(Year(Now), 3)
        If CInt(Month(Now)) < 10 Then
            str2 = "0" & Month(Now)
        Else
            str2 = Month(Now)
        End If

        Dim Stra As String
        Stra = Trim(str1 & str2)

        ai = ac.ProductionOWPAcceptance_GetNO(Stra) '' 讀取基數

        If ai Is Nothing Then
            GetWOPAcceptanceNO = "AC" & str1 & str2 & "0001"
        Else
            GetWOPAcceptanceNO = "AC" & str1 & str2 & Mid((CInt(Mid(ai.A_AcceptanceNO, 7)) + 10001), 2)
        End If

    End Function
    ''' <summary>
    '''  '生成新的验收流水号
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetWOPAcceptancetNum() As String
        Dim ac As New ProductionOWPAcceptanceControl
        Dim ai As New ProductionOWPAcceptanceInfo
        Dim Stra As String
        Stra = "A"

        ai = ac.ProductionOWPAcceptance_GetNum(Stra)

        If ai Is Nothing Then
            GetWOPAcceptancetNum = "A" & "00000001"
        Else
            GetWOPAcceptancetNum = "A" & Mid((CInt(Mid(ai.A_NO, 2)) + 100000001), 2)
        End If
    End Function
    ''' <summary>
    ''' 數據檢查
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckData() As Boolean
        CheckData = True

        If ASend_NOText.Text = "" Then
            MsgBox("驗收必須有送貨單號")
            ASend_NOText.Focus()
            CheckData = False
            Exit Function
        End If

        If gluSupplier.Text = "" Then
            MsgBox("请填写供应商!")
            gluSupplier.Focus()
            CheckData = False
            Exit Function
        End If

        If ds.Tables("ProductionOWPAcceptance").Rows.Count = 0 Then
            MsgBox("沒有數據,無法保存!")
            CheckData = False
            Exit Function
        End If

        Dim A_OK_Qty_, A_QQ_Qty_, A_TC_Qty_, A_QT_Qty_ As Double

        For i As Integer = 0 To ds.Tables("ProductionOWPAcceptance").Rows.Count - 1
            A_OK_Qty_ = CInt(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_OK_Qty"))
            A_QQ_Qty_ = CInt(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_QQ_Qty"))
            A_TC_Qty_ = CInt(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_TC_Qty"))
            A_QT_Qty_ = CInt(ds.Tables("ProductionOWPAcceptance").Rows(i)("A_QT_Qty"))

            If A_OK_Qty_ = 0 And A_QQ_Qty_ = 0 And A_TC_Qty_ = 0 And A_QT_Qty_ = 0 Then
                GridView1.FocusedRowHandle = i '移、至錯誤碼行
                MsgBox("請填寫驗收數量", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                CheckData = False
                Exit Function
            End If

            If A_OK_Qty_ < 0 Or A_QQ_Qty_ < 0 Or A_TC_Qty_ < 0 Or A_QT_Qty_ < 0 Then '2012-6-20 不能輸負數
                GridView1.FocusedRowHandle = i '移、至錯誤碼行
                MsgBox("驗收數量不能為負數", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                CheckData = False
                Exit Function
            End If

            Dim opc As New ProductionOutProcessControl
            Dim opiL As List(Of ProductionOutProcessInfo)
            opiL = opc.ProductionOutProcess_GetList(ds.Tables("ProductionOWPAcceptance").Rows(i)("OPAutoID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If opiL.Count <= 0 Then
                CheckData = False
                Exit Function
            End If

            If (CaoTypeLabel.Text = "OWPCheck" And CheckEdit.Checked = True) Or CaoTypeLabel.Text = "OWPAddEdit" Then  ''審核通過后，又對改變審核狀態,就不進行判斷
                ''判斷驗收數不小於未交數
                If (A_OK_Qty_ + A_QQ_Qty_ + A_TC_Qty_ + A_QT_Qty_) <= opiL(0).PO_NoSendQty Then
                Else
                    GridView1.FocusedRowHandle = i '移、至錯誤碼行
                    ' GridView1.FocusedColumn = GridView1.Columns("SPWO_Qty")
                    MsgBox("驗收數量總和大於未交數，請檢查!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                    CheckData = False
                    Exit Function
                End If
            End If
        Next

    End Function
    ''' <summary>
    ''' 載入數據
    ''' </summary>
    ''' <param name="A_AcceptanceNO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadData(ByVal A_AcceptanceNO As String) As Boolean
        '載入驗收單數據
        LoadData = True
        Dim objInfo As New ProductionOWPAcceptanceInfo
        Dim objList As New List(Of ProductionOWPAcceptanceInfo)
        Dim oc As New ProductionOWPAcceptanceControl

        Try
            Dim i As Integer
            Dim Count As Double
            Dim row As DataRow
            ds.Tables("ProductionOWPAcceptance").Rows.Clear()

            objList = oc.ProductionOWPAcceptance_GetList(Nothing, A_AcceptanceNO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            Count = objList.Count
            If Count <= 0 Then
                MsgBox("沒有數據！")
                LoadData = False
                Exit Function
            Else
                VerLabel.Text = CStr(objList(0).A_Ver)
                ASend_NOText.Text = objList(0).ASend_NO
                ' GluDep.EditValue = objList(0).DPT_ID
                A_SendDateEdit.Text = objList(0).A_SendDate
                A_Action_Edit.Text = objList(0).A_ActionName
                gluSupplier.EditValue = objList(0).S_Supplier

                'CheckEdit.Checked = objList(0).A_Check
                CheckDateEdit.Text = objList(0).A_CheckDate
                CheckActionLabel.Text = objList(0).A_CheckActionName
                CheckMemoEdit.Text = objList(0).A_CheckRemark

                'AccCheck.Checked = objList(0).A_AccCheck
                CBA_AccountCheckType.Text = objList(0).A_AccCheckType
                AccDateEdit.Text = objList(0).A_AccCheckDate
                AccLabel.Text = objList(0).A_AccCheckActionName
                AccMemoEdit.Text = objList(0).A_ACCCheckRemark


                If objList(0).A_Check = False Then
                    CheckEdit.Checked = False
                    OldCheck = False
                Else
                    CheckEdit.Checked = True
                    OldCheck = True
                End If

                If objList(i).A_AccCheck = False Then
                    AccCheck.Checked = False
                    OldAccCheck = False
                Else
                    AccCheck.Checked = True
                    OldAccCheck = True
                End If
            End If

            For i = 0 To Count - 1
                '''''tb'''''
                row = ds.Tables("ProductionOWPAcceptance").NewRow

                row("OPAutoID") = objList(i).OPAutoID ''外發單中的自動編號ID

                row("O_NO") = objList(i).O_NO
                row("A_NO") = objList(i).A_NO
                row("A_AcceptanceNO") = objList(i).A_AcceptanceNO
                row("PM_M_Code") = objList(i).PM_M_Code

                row("PM_Type") = objList(i).PM_Type
                row("A_PS_NO") = objList(i).PS_NO
                row("A_PS_Name") = objList(i).A_PS_Name

                row("A_PS_NO_Next") = objList(i).PS_NO_Next
                row("A_PS_Name_Next") = objList(i).A_PS_Name_Next

                row("A_OK_Qty") = objList(i).A_OK_Qty
                row("A_QQ_Qty") = objList(i).A_QQ_Qty
                row("A_TC_Qty") = objList(i).A_TC_Qty
                row("A_QT_Qty") = objList(i).A_QT_Qty

                row("A_Remark") = objList(i).A_Remark
                row("A_OW_Do") = objList(i).A_OW_Do

                row("PM_JiYu") = objList(i).PM_JiYu

                ds.Tables("ProductionOWPAcceptance").Rows.Add(row)

                ''2012-4-10
                Dim row1 As DataRow = ds.Tables("ProcessNext").NewRow
                row1("N_PS_NO") = objList(i).PS_NO
                row1("N_PS_Name") = objList(i).A_PS_Name
                ds.Tables("ProcessNext").Rows.Add(row1)

            Next

        Catch ex As Exception
            LoadData = False
            MsgBox(ex.Message)
        End Try
    End Function


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckData() = False Then Exit Sub

        Select Case CaoTypeLabel.Text
            Case "OWPAddEdit"
                Select Case Edit
                    Case False
                        SaveNew()
                    Case True
                        SaveEdit()
                End Select
            Case "OWPCheck"
                '驗收時
                If OldCheck = False Then
                    If CheckEdit.Checked = False Then
                        MsgBox("請先更改驗收狀態,才能保存!", MsgBoxStyle.OkOnly)
                        Exit Sub
                    End If

                End If

                If OldCheck = True Then
                    If CheckEdit.Checked = True Then
                        MsgBox("請先更改驗收狀態,才能保存!", MsgBoxStyle.OkOnly)
                        Exit Sub
                    End If
                End If
                SaveCheck()
            Case "OWPAccCheck"
                '復核
                SaveAccCheck()
        End Select



    End Sub

    Private Sub popDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "A_NO")

        If DelTemp = "A_NO" Then
        Else
            '在刪除表中增加被刪除的記錄
            Dim row As DataRow = ds.Tables("DelProductionOWPAcceptance").NewRow
            row("A_NO") = DelTemp
            ds.Tables("DelProductionOWPAcceptance").Rows.Add(row)
        End If
        ds.Tables("ProductionOWPAcceptance").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
        If GridView1.RowCount = 0 Then
            gluSupplier.Text = Nothing
        End If
    End Sub
    Private Sub popAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popAdd.Click
        '  FrmProductionOWPAcceptanceSelect.ShowDialog()

        Dim i, n, j As Integer
        Dim YanZeng As Boolean    '驗証,看載入各項未交數是否為0或負數
        Dim myfrm As New FrmProductionOWPAcceptanceSelect

        myfrm.ShowDialog()
        YanZeng = True
        If RefreshT = True Then
            Dim arr(n) As String
            arr = Split(tempValue, ",")
            n = Len(Replace(tempValue, ",", "," & "*")) - Len(tempValue)

            For i = 0 To n

                Dim opc As New ProductionOutProcessControl
                Dim opiL As List(Of ProductionOutProcessInfo)
                opiL = opc.ProductionOutProcess_GetList(arr(i), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                Dim row As DataRow = ds.Tables("ProductionOWPAcceptance").NewRow

                row("OPAutoID") = opiL(0).AutoID
                row("O_NO") = opiL(0).PO_ID

                row("PM_M_Code") = opiL(0).PM_M_Code
                row("PM_Type") = opiL(0).PM_Type

                row("A_PS_NO") = opiL(0).PS_NO   ''上面三項目確定唯一單號
                row("A_PS_Name") = opiL(0).PS_Name

                row("A_OW_Do") = opiL(0).OW_Do
                row("PM_JiYu") = opiL(0).PM_JiYu
                ''---------------------------------------------------------------------------------------------------------------------
                Get_Next_Process_NO = ""
                Get_Next_Process_Name = ""


                ''有外發工序為全檢時，載入本工序 2012-8-27
                If InStr(opiL(0).PS_Name, "全檢", CompareMethod.Text) > 0 Then
                    row("A_PS_NO_Next") = opiL(0).PS_NO
                    row("A_PS_Name_Next") = opiL(0).PS_Name
                Else

                    ''讀取下一工序。若存在顯示下一工序，不存在顯示當前工序
                    Get_Next_Process(opiL(0).PS_NO)

                    If Get_Next_Process_NO <> "" Then
                        row("A_PS_NO_Next") = Get_Next_Process_NO   '收料工序
                        row("A_PS_Name_Next") = Get_Next_Process_Name
                    Else
                        row("A_PS_NO_Next") = opiL(0).PS_NO
                        row("A_PS_Name_Next") = opiL(0).PS_Name
                    End If


                End If

                ''''++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                row("A_OK_Qty") = opiL(0).PO_NoSendQty  ''默認全部OK
                row("A_QQ_Qty") = 0
                row("A_TC_Qty") = 0
                row("A_QT_Qty") = 0
                row("A_Remark") = ""

                For j = 0 To ds.Tables("ProductionOWPAcceptance").Rows.Count - 1
                    If arr(i) = ds.Tables("ProductionOWPAcceptance").Rows(j)("OPAutoID") Then
                        MsgBox("一張單不允許有重復外發工序記錄")
                        Exit Sub
                    End If
                Next
                '  MsgBox(opiL(0).PO_NoSendQty)

                If opiL(0).PO_NoSendQty <= 0 Then
                    YanZeng = False ''此產品外發工序未交數已經為0或負數,無法再驗收
                End If

                Dim strSupplier As String
                strSupplier = gluSupplier.Text

                Dim oi2 As List(Of ProductionOutProcessInfo)
                oi2 = opc.ProductionOutProcess_GetList(Nothing, tempValue5, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                If oi2.Count = 0 Then Exit Sub

                gluSupplier.Text = oi2(0).S_Supplier  '新增的时候填充

                If strSupplier = "" Then
                    ds.Tables("ProductionOWPAcceptance").Rows.Add(row)
                End If

                If strSupplier <> "" Then
                    If strSupplier.Equals(gluSupplier.Text) = True Then
                        ds.Tables("ProductionOWPAcceptance").Rows.Add(row)
                    Else
                        Dim oi1 As List(Of ProductionOutProcessInfo)
                        oi1 = opc.ProductionOutProcess_GetList(Nothing, tempValue5, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                        gluSupplier.Text = oi1(0).S_Supplier
                        MsgBox("同一驗收單號中供應商必須相同！", , "警告")
                        Exit Sub
                    End If
                End If

                GridView1.MoveLast()
            Next

        End If

        tempValue5 = ""
        tempValue = ""
        RefreshT = False

        If YanZeng = False Then
            MsgBox("此產品外發工序未交數已經為0或負數,無法再驗收!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ASend_NOText_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ASend_NOText.KeyDown
        If e.KeyCode = Keys.Tab Or e.KeyCode = Keys.Enter Then
            Grid1.Focus()
        End If

        If e.KeyCode = Keys.A And e.Control Then
            popAdd_Click(Nothing, Nothing)
        End If
    End Sub
    ''2012-04-10 改----------------------------------------------------

    ''' <summary>
    ''' 載入下一工序名稱
    ''' </summary>
    ''' <param name="PS_No1"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Get_Next_Process(ByVal PS_No1 As String) As String
        Get_Next_Process = ""

        Dim PS_Num_Now As String  ''當前工序序號
        Dim PS_Num_Next As String  ''下一工序序號
        Dim Pro_NO_Now As String

        PS_Num_Now = ""
        PS_Num_Next = ""
        Pro_NO_Now = ""


        Dim opc As New ProductionOWPAcceptanceControl
        Dim opiL As List(Of ProductionOWPAcceptanceInfo)

        opiL = opc.ProductionOWPAcceptance_Next_Process(Nothing, PS_No1, Nothing, Nothing, "True")

        If opiL.Count > 0 Then
            PS_Num_Now = opiL(0).PS_Num  ''得到當前工序序號
            Pro_NO_Now = opiL(0).Pro_NO
        Else
            Exit Function
        End If

        PS_Num_Next = Str(Val(PS_Num_Now) + 1) ''下一工序加一


        Dim opc2 As New ProductionOWPAcceptanceControl
        Dim opiL2 As List(Of ProductionOWPAcceptanceInfo)
        Dim Num_Count, i As Integer
        opiL2 = opc2.ProductionOWPAcceptance_Next_Process(Pro_NO_Now, Nothing, Nothing, Nothing, Nothing)
        ''得到當前Pro_NO_Now的，總工序總數，要查詢當前 以下存在的工序
        Num_Count = opiL2.Count

        If Num_Count > 0 And PS_Num_Next > 0 And Num_Count > PS_Num_Next Then
        Else
            Exit Function
        End If


        For i = PS_Num_Next To Num_Count
            '查詢 下一工序序號是否存在
            Dim opc1 As New ProductionOWPAcceptanceControl
            Dim opiL1 As List(Of ProductionOWPAcceptanceInfo)
            opiL1 = opc1.ProductionOWPAcceptance_Next_Process(Pro_NO_Now, Nothing, Nothing, CStr(i), "True")

            If opiL1.Count > 0 Then

                Get_Next_Process_NO = opiL1(0).PS_NO.ToString
                Get_Next_Process_Name = opiL1(0).A_PS_Name.ToString
                Exit Function
            End If
        Next


    End Function



    Function Load_Procses(ByVal PM_M_Code As String, ByVal cbType As String, ByVal gluType As String) As String
        Load_Procses = ""

        ds.Tables("ProcessNext").Clear()

        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)
        pci = pc.ProcessMain_GetList(Nothing, PM_M_Code, cbType, gluType, Nothing, "True")

        If pci.Count > 0 Then
        Else
            Exit Function
        End If

        Dim i As Integer
        For i = 0 To pci.Count - 1
            Dim row As DataRow = ds.Tables("ProcessNext").NewRow
            row("N_PS_NO") = pci(i).PS_NO
            row("N_PS_Name") = pci(i).PS_Name
            ds.Tables("ProcessNext").Rows.Add(row)
        Next

    End Function

    ''---------2012-4-12改-------------------------------------
    Private Sub RepositoryItemPopupContainerEdit1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemPopupContainerEdit1.Enter
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        Dim strA, strB As String

        strA = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString
        strB = GridView1.GetFocusedRowCellValue("PM_Type").ToString

        If strPM_M_Code <> strA Or strPM_Type <> strB Then
            strPM_M_Code = strA
            strPM_Type = strB
            Load_Procses(strA, "生產加工", strB)
        End If
    End Sub

    Private Sub Grid2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid2.DoubleClick
        On Error Resume Next
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        GridView1.SetFocusedRowCellValue(A_PS_NO_Next, GridView2.GetFocusedRowCellValue("N_PS_NO").ToString)
        GridView1.SetFocusedRowCellValue(A_PS_Name_Next, GridView2.GetFocusedRowCellValue("N_PS_Name").ToString)

        PopupContainerControl1.OwnerEdit.ClosePopup()
        '  RepositoryItemPopupContainerEdit1.NullText = ds.Tables("ProcessNext").Rows((GridView2.FocusedRowHandle)).Item("N_PS_Name")

    End Sub

End Class