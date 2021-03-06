Imports LFERP.Library.Production.ProuctionWareOutB
Imports LFERP.DataSetting
Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Production.Datasetting
Imports LFERP.Library.ProductionSchedule


Public Class ProductionWareOutSubB
#Region "屬性"
    Dim upi As List(Of UserPowerInfo)
    Dim upc As New UserPowerControl

    Dim pic As New ProductInventoryController
    Dim mc As New ProductionDataSettingControl
    Dim ds As New DataSet
    Private strWHINID As String = String.Empty
    Private strWHoutID As String = String.Empty
    Private strM_CodeIN As String = String.Empty
    Private strM_CodeOut As String = String.Empty

    Private oldCheck, oldInCheck As Boolean
    Private _EditItem As String  '属性栏位
    Private _EditValue As String '属性栏位

    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Property EditValue() As String '属性
        Get
            Return _EditValue
        End Get
        Set(ByVal value As String)
            _EditValue = value
        End Set
    End Property

#End Region

#Region "窗體載入"
    Private Sub ProductionWareOutSubB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        LoadPM_M_Code()
        '生產工藝
        upi = upc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)
        If upi.Count > 0 Then
            cbTypeIN.EditValue = upi(0).UserType
            cbTypeOut.EditValue = upi(0).UserType
        End If

        Select Case EditItem
            Case "Add"
                txtP_ActionName.Text = InUserID
                DateAddDate.EditValue = Format(Now, "yyyy/MM/dd")
            Case "Edit"
                LoadData(EditValue)
            Case "View"
                LoadData(EditValue)
                Panel1.Visible = True
                CheckEdit1.Visible = True
                cmdSave.Visible = False
            Case "Check"
                LoadData(EditValue)
                Panel1.Visible = True
                GroupBox1.Enabled = False
                GroupBox2.Enabled = False
                GroupBox3.Enabled = False
            Case "CheckIn"
                LoadData(EditValue)
                CheckEdit1.Visible = True
                GroupBox1.Enabled = False
                GroupBox2.Enabled = False
                GroupBox3.Enabled = False
        End Select
    End Sub
#End Region

#Region "創建臨時表"
    Sub CreateTable()
        '創建產品編號表
        With ds.Tables.Add("PM_M_Code")
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_JiYu", GetType(String))
        End With

        PM_M_CodeIN.Properties.ValueMember = "PM_M_Code"
        PM_M_CodeIN.Properties.DisplayMember = "PM_M_Code"
        PM_M_CodeIN.Properties.DataSource = ds.Tables("PM_M_Code")

        PM_M_CodeOut.Properties.ValueMember = "PM_M_Code"
        PM_M_CodeOut.Properties.DisplayMember = "PM_M_Code"
        PM_M_CodeOut.Properties.DataSource = ds.Tables("PM_M_Code")
        '創建類型表
        With ds.Tables.Add("ProductType")
            .Columns.Add("PM_Type", GetType(String))
        End With
        '創建類型表
        With ds.Tables.Add("ProductType1")
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluTypeIN.Properties.ValueMember = "PM_Type"
        gluTypeIN.Properties.DisplayMember = "PM_Type"
        gluTypeIN.Properties.DataSource = ds.Tables("ProductType")

        gluTypeOut.Properties.ValueMember = "PM_Type"
        gluTypeOut.Properties.DisplayMember = "PM_Type"
        gluTypeOut.Properties.DataSource = ds.Tables("ProductType1")
    End Sub

#End Region

#Region "加載控件數據"
    Sub LoadPM_M_Code()
        Dim mi As New List(Of ProductionDataSettingInfo)
        mi = mc.ProductionUser_GetList(txtWHIN.Tag, Nothing)
        ds.Tables("PM_M_Code").Clear()

        If mi.Count > 0 Then    '判斷是否有權限限制
            Dim row As DataRow
            Dim j As Integer
            For j = 0 To mi.Count - 1
                row = ds.Tables("PM_M_Code").NewRow
                row("PM_M_Code") = mi(j).PM_M_Code
                row("PM_JiYu") = mi(j).PM_JiYu '
                ds.Tables("PM_M_Code").Rows.Add(row)
            Next
        Else
            Dim row As DataRow
            Dim j As Integer

            Dim mpi As List(Of ProcessMainInfo)
            Dim mpc As New ProcessMainControl
            mpi = mpc.ProcessMain_GetList3(Nothing, Nothing)

            If mpi.Count > 0 Then
                For j = 0 To mpi.Count - 1
                    row = ds.Tables("PM_M_Code").NewRow
                    row("PM_M_Code") = mpi(j).PM_M_Code
                    row("PM_JiYu") = mpi(j).PM_JiYu '
                    ds.Tables("PM_M_Code").Rows.Add(row)
                Next
            End If
        End If
    End Sub
#End Region

#Region "控件載入事件"
    '通過產品編號得到相應的類型信息
    Private Sub PM_M_CodeIN_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_CodeIN.EditValueChanged
        On Error Resume Next
        If PM_M_CodeIN.EditValue = "" Or PM_M_CodeIN.EditValue Is Nothing Then Exit Sub
        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        ds.Tables("ProductType").Clear()
        ppi = ppc.ProcessMain_GetList2(cbTypeIN.EditValue, PM_M_CodeIN.EditValue)
        If ppi.Count = 0 Then
        Else

            Dim i As Integer
            For i = 0 To ppi.Count - 1
                Dim row As DataRow
                row = ds.Tables("ProductType").NewRow
                row("PM_Type") = ppi(i).Type3ID
                ds.Tables("ProductType").Rows.Add(row)
            Next
        End If

        gluTypeIN.EditValue = ds.Tables("ProductType").Rows(0)("PM_Type").ToString

    End Sub
    '通過產品編號得到相應的類型信息
    Private Sub PM_M_CodeOut_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_CodeOut.EditValueChanged
        On Error Resume Next
        If PM_M_CodeOut.EditValue = "" Or PM_M_CodeOut.EditValue Is Nothing Then Exit Sub
        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        ds.Tables("ProductType1").Clear()
        ppi = ppc.ProcessMain_GetList2(cbTypeOut.EditValue, PM_M_CodeOut.EditValue)
        If ppi.Count = 0 Then
        Else

            Dim i As Integer
            For i = 0 To ppi.Count - 1
                Dim row1 As DataRow
                row1 = ds.Tables("ProductType1").NewRow
                row1("PM_Type") = ppi(i).Type3ID          '---默認情況下,發料類型與收料類型相同
                ds.Tables("ProductType1").Rows.Add(row1)
            Next
        End If
        gluTypeOut.EditValue = ds.Tables("ProductType1").Rows(0)("PM_Type").ToString
    End Sub
    '數字控件處理
    Private Sub txtQty_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyUp
        If (e.KeyValue > 47 And e.KeyValue < 58) Or (e.KeyValue > 95 And e.KeyValue < 106) Or (e.KeyValue = 8) Or (e.KeyValue = 45) Or (e.KeyValue = 46) Then
        Else
            'MsgBox("只能輸入整數數字！")
            txtQty.Text = Nothing
            Exit Sub
        End If
    End Sub
#End Region

#Region "返回數據"
    Public Function LoadData(ByVal P_NO As String) As Boolean
        LoadData = True

        Dim pi As List(Of ProductionWareOutBInfo)
        Dim pc As New ProductionWareOutBControl
        pi = pc.ProductionWareOutB_GetList(P_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Try
            If pi.Count = 0 Then
                MsgBox("沒有數據")
                LoadData = False
                Exit Function
            Else
                Me.txtNo.Text = pi(0).P_NO
                '發貨
                strWHINID = pi(0).P_WareIN
                txtWHIN.Tag = pi(0).P_WareIN
                txtWHIN.EditValue = pi(0).P_WareINName
                PM_M_CodeIN.EditValue = pi(0).PM_M_CodeIN

                cbTypeIN.EditValue = pi(0).Pro_TypeIN
                gluTypeIN.EditValue = pi(0).PM_TypeIN
                strM_CodeIN = pi(0).M_CodeIN
                lblEndINQty.Text = pi(0).P_EndINQty
                '收貨
                strWHoutID = pi(0).P_WareOut
                txtWHOut.Tag = pi(0).P_WareOut
                txtWHOut.EditValue = pi(0).P_WareOutName
                cbTypeOut.EditValue = pi(0).Pro_TypeOut
                PM_M_CodeOut.EditValue = pi(0).PM_M_CodeOut
                gluTypeOut.EditValue = pi(0).PM_TypeOut

                strM_CodeOut = pi(0).M_CodeOut
                lblEndOutQty.Text = pi(0).P_EndOutQty

                '審核部份
                CheckEdit2.Checked = pi(0).P_Check
                oldCheck = pi(0).P_Check
                CheckEdit1.Checked = pi(0).P_InCheck
                oldInCheck = pi(0).P_InCheck

                Label14.Text = pi(0).P_CheckActionName
                Label17.Text = pi(0).P_CheckDate
                Me.txtP_Remark.Text = pi(0).P_Remark
                Me.txtQty.Text = pi(0).P_Qty
                DateAddDate.Text = pi(0).P_AddDate

                txtP_ActionName.Text = pi(0).P_ActionName
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
#End Region

#Region "自動流水號"
    Public Function GetNO() As String
        Dim pi As New ProductionWareOutBInfo
        Dim pc As New ProductionWareOutBControl
        Dim strA As String
        strA = Format(Now, "yyMM")
        pi = pc.ProductionWareOutB_GetNO(strA)

        If pi Is Nothing Then
            GetNO = "PO" & strA & "0001"
        Else
            GetNO = "PO" + strA + Mid((CInt(Mid(pi.P_NO, 8)) + 10001), 2)
        End If
    End Function
#End Region

#Region "按鍵事件"
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckData() = False Then
            Exit Sub
        End If

        Select Case EditItem
            Case "Add"
                DataNew()
            Case "Edit"
                DataEdit()
            Case "Check"
                UpdateCheck()
            Case "CheckIn"
                UpdateCheckIn()
        End Select

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

#End Region

#Region "數據檢查"
    Function CheckData() As Boolean
        CheckData = True
        If txtWHIN.EditValue = String.Empty Then
            MsgBox("發料倉庫不能为空,请输入！")
            txtWHIN.Focus()
            CheckData = False
            Exit Function
        End If
        If cbTypeIN.EditValue = String.Empty Then
            MsgBox("發料工藝類型不能为空,请输入！")
            cbTypeIN.Focus()
            CheckData = False
            Exit Function
        End If
        If PM_M_CodeIN.EditValue = String.Empty Then
            MsgBox("發料產品編號不能为空,请输入！")
            PM_M_CodeIN.Focus()
            CheckData = False
            Exit Function
        End If
        If gluTypeIN.EditValue = String.Empty Then
            MsgBox("發料類型不能为空,请输入！")
            gluTypeIN.Focus()
            CheckData = False
            Exit Function
        End If

        If txtQty.EditValue = String.Empty Then
            MsgBox("發料數量不能为空,请输入！")
            txtQty.Focus()
            CheckData = False
            Exit Function
        End If

        If CInt(txtQty.EditValue) <= 0 Then
            MsgBox("發料數量不能小于等于0为空,请输入！")
            txtQty.Focus()
            CheckData = False
            Exit Function
        End If

        '---------------------------------------------------------------
        If txtWHOut.EditValue = String.Empty Then
            MsgBox("收料倉庫不能为空,请输入！")
            txtWHOut.Focus()
            CheckData = False
            Exit Function
        End If

        If cbTypeOut.EditValue = String.Empty Then
            MsgBox("收料工藝類型不能为空,请输入！")
            cbTypeOut.Focus()
            CheckData = False
            Exit Function
        End If
        If PM_M_CodeOut.EditValue = String.Empty Then
            MsgBox("收料產品編號不能为空,请输入！")
            PM_M_CodeOut.Focus()
            CheckData = False
            Exit Function
        End If
        If gluTypeOut.EditValue = String.Empty Then
            MsgBox("發料類型不能为空,请输入！")
            gluTypeOut.Focus()
            CheckData = False
            Exit Function
        End If

        If txtWHIN.EditValue = txtWHOut.EditValue Then
            MsgBox("收料倉庫與發料倉庫不能相同,请输入！")
            txtWHOut.Focus()
            CheckData = False
            Exit Function
        End If
        If cbTypeIN.EditValue = cbTypeOut.EditValue Then
            MsgBox("收料產品工藝與發料產品工藝不能相同,请输入！")
            cbTypeOut.Focus()
            CheckData = False
            Exit Function
        End If
        '---------------------------------------------------------發貨倉庫存
        Dim PL As New List(Of ProcessMainInfo)
        Dim PC As New ProcessMainControl
        PL = PC.ProcessMain_GetList(Nothing, PM_M_CodeIN.EditValue, cbTypeIN.EditValue, gluTypeIN.EditValue, Nothing, Nothing)
        Dim intQty As Integer = 0
        If PL.Count > 0 Then
            Dim pcw As New ProductInventoryController
            Dim pcl As New List(Of ProductInventoryInfo)

            pcl = pcw.ProductInventory_GetList(PM_M_CodeIN.EditValue, PL(0).M_Code, Me.txtWHIN.Tag, Nothing)
            If pcl.Count <= 0 Then
                intQty = 0
            Else
                intQty = pcl(0).PI_Qty
            End If
        Else
            MsgBox("發貨產品沒有建工藝,请输入！")
            PM_M_CodeIN.Focus()
            CheckData = False
        End If


        If EditItem = "Check" And CheckEdit2.Checked = False Then
        Else

            If CInt(txtQty.EditValue) > intQty Then
                MsgBox("倉庫不夠！")
                txtQty.Focus()
                CheckData = False
            End If
        End If


        '---------------------------------------------------------收貨倉是否建工藝
        Dim PLlist As New List(Of ProcessMainInfo)
        Dim PCcon As New ProcessMainControl
        PLlist = PCcon.ProcessMain_GetList(Nothing, PM_M_CodeOut.EditValue, cbTypeOut.EditValue, gluTypeOut.EditValue, Nothing, Nothing)
        If PLlist.Count = 0 Then
            MsgBox("收貨產品沒有建工藝,请输入！")
            PM_M_CodeOut.Focus()
            CheckData = False
        End If


        '2013-10-23-----------------------------------------------------------------------
        Dim strFac As String
        Dim fcon As New FacControler
        Dim flist As New List(Of FacInfo)
        flist = fcon.GetFacListA(Nothing, Nothing, Me.txtWHIN.Tag)

        If flist.Count <= 0 Then
            Exit Function
        Else
            strFac = flist(0).FacID
        End If

        Dim pscA As New ProductionScheduleControl
        Dim psiA As New List(Of ProductionScheduleInfo)

        psiA = pscA.ProductionSchedule_GetList1(Nothing, cbTypeIN.EditValue, PM_M_CodeIN.EditValue, PL(0).M_Code, strFac, Nothing, DateAddDate.EditValue, DateAddDate.EditValue, gluTypeIN.EditValue)
        If psiA.Count = 0 Then
            MsgBox("產品:" & PM_M_CodeIN.EditValue & "  配件:" & gluTypeIN.EditValue & "在" & DateAddDate.EditValue & "無生產計，請添加生產計划!")
            CheckData = False
            Exit Function
        End If
        '----------------------------------------------------------------------------------------------



    End Function
#End Region

#Region "新增修改"
    ''' <summary>
    '''     新增
    ''' </summary>
    Sub DataNew()
        Dim pi As New ProductionWareOutBInfo
        Dim pc As New ProductionWareOutBControl

        txtNo.Text = GetNO()

        pi.P_NO = txtNo.Text
        pi.P_WareIN = strWHINID
        pi.PM_M_CodeIN = me.PM_M_CodeIN.EditValue
        pi.PM_TypeIN = gluTypeIN.EditValue
        pi.Pro_TypeIN = cbTypeIN.EditValue
        pi.M_CodeIN = strM_CodeIN

        pi.P_WareOut = strWHoutID
        pi.PM_M_CodeOut = Me.PM_M_CodeOut.EditValue
        pi.PM_TypeOut = gluTypeOut.EditValue
        pi.Pro_TypeOut = cbTypeOut.EditValue
        pi.M_CodeOut = strM_CodeOut

        pi.P_Qty = CInt(Me.txtQty.Text)

        pi.P_Action = InUserID
        pi.P_AddDate = Me.DateAddDate.Text
        pi.P_Remark = Me.txtP_Remark.Text

        If pc.ProductionWareOutB_Add(pi) = False Then
            Exit Sub
            MsgBox("新增失敗！")
        Else
            MsgBox("已保存,單號: " & txtNo.Text & " ")
        End If
        Me.Close()
    End Sub
    ''' <summary>
    '''     修改
    ''' </summary>
    Sub DataEdit()
        Dim pi As New ProductionWareOutBInfo
        Dim pc As New ProductionWareOutBControl

        pi.P_NO = txtNo.Text
        pi.P_WareIN = strWHINID
        pi.PM_M_CodeIN = Me.PM_M_CodeIN.EditValue
        pi.PM_TypeIN = gluTypeIN.EditValue
        pi.Pro_TypeIN = cbTypeIN.EditValue
        pi.M_CodeIN = strM_CodeIN

        pi.P_WareOut = strWHoutID
        pi.PM_M_CodeOut = Me.PM_M_CodeOut.EditValue
        pi.PM_TypeOut = gluTypeOut.EditValue
        pi.Pro_TypeOut = cbTypeOut.EditValue
        pi.M_CodeOut = strM_CodeIN
        pi.M_CodeOut = strM_CodeOut

        pi.P_Qty = CInt(Me.txtQty.Text)
        pi.P_Remark = Me.txtP_Remark.Text

        pi.P_ModifyAction = InUserID
        pi.P_ModifyDate = Format(Now, "yyyy/MM/dd")

        If pc.ProductionWareOutB_Update(pi) = False Then
            Exit Sub
            MsgBox("修改失敗！")
        Else
            MsgBox("已修改,單號: " & txtNo.Text & " ")
        End If
        Me.Close()
    End Sub
    ''' <summary>
    '''     審核
    ''' </summary>
    Sub UpdateCheck()
        Dim pi As New ProductionWareOutBInfo
        Dim pc As New ProductionWareOutBControl

        If oldCheck = CheckEdit2.Checked Then
            MsgBox("審核狀態未改變，請更改狀態後再保存……")
            Exit Sub
        End If

        pi.P_NO = Me.txtNo.Text
        pi.P_Check = CheckEdit2.Checked
        pi.P_CheckAction = InUserID
        pi.P_CheckDate = Format(Now, "yyyy/MM/dd")
        pi.P_CheckRemark = String.Empty

        If pc.ProductionWareOutB_UpdateCheck(pi) = True Then
            MsgBox("審核狀態已改變!")
        Else
            MsgBox("審核失敗,請檢查原因!")
            Exit Sub
        End If
        Me.Close()
    End Sub
    ''' <summary>
    '''     收貨確認
    ''' </summary>
    Sub UpdateCheckIn()
        Dim pi As New ProductionWareOutBInfo
        Dim pc As New ProductionWareOutBControl
        If oldInCheck = CheckEdit1.Checked Then
            MsgBox("收料確認狀態未改變，請更改狀態後再保存……")
            Exit Sub
        End If

        pi.P_NO = Me.txtNo.Text
        pi.P_InCheck = CheckEdit1.Checked
        pi.P_InCheckAction = InUserID
        pi.P_InCheckDate = Format(Now, "yyyy/MM/dd")

        If pc.ProductionWareOutB_UpdateInCheck(pi) = True Then
            MsgBox("收貨確認狀態已改變!")
        Else
            MsgBox("收貨確認失敗,請檢查原因!")
            Exit Sub
        End If

        '----------------出貨扣賬處理----------------------------------------
        Dim ininfo As New ProductInventoryInfo
        ininfo.WH_ID = strWHINID
        Dim inList As New List(Of ProductInventoryInfo)
        Dim intinQty As Integer = 0
        inList = pic.ProductInventory_GetList(Me.PM_M_CodeIN.EditValue, strM_CodeIN, strWHINID, Nothing)
        If inList.Count > 0 Then
            intinQty = inList(0).PI_Qty
        End If

        If CheckEdit1.Checked = True Then
            ininfo.PI_Qty = intinQty - CInt(Me.txtQty.Text)
        ElseIf CheckEdit1.Checked = False Then
            ininfo.PI_Qty = intinQty + CInt(Me.txtQty.Text)
        End If

        ininfo.PM_M_Code = Me.PM_M_CodeIN.EditValue
        ininfo.M_Code = strM_CodeIN

        If pic.ProductInventory_Update(ininfo) = False Then
            MsgBox("出貨扣賬失敗,請檢查原因!", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        ''----------------收貨扣賬處理----------------------------------------
        Dim outinfo As New ProductInventoryInfo
        outinfo.WH_ID = strWHoutID
        Dim outList As New List(Of ProductInventoryInfo)
        Dim intOutQty As Integer = 0
        outList = pic.ProductInventory_GetList(Me.PM_M_CodeOut.EditValue, strM_CodeOut, strWHoutID, Nothing)
        If outList.Count > 0 Then
            intOutQty = outList(0).PI_Qty
        End If

        If CheckEdit1.Checked = True Then
            outinfo.PI_Qty = intOutQty + CInt(Me.txtQty.Text)
        ElseIf CheckEdit1.Checked = False Then
            outinfo.PI_Qty = intOutQty - CInt(Me.txtQty.Text)
        End If

        outinfo.PM_M_Code = Me.PM_M_CodeOut.EditValue
        outinfo.M_Code = strM_CodeOut

        If pic.ProductInventory_Update(outinfo) = False Then
            MsgBox("收貨扣賬失敗,請檢查原因!", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        '------------------修改結餘數--------------------------------------------
        Dim pwinfo As New ProductionWareOutBInfo
        Dim pccon As New ProductionWareOutBControl

        pwinfo.P_NO = Me.txtNo.Text
        pwinfo.P_EndINQty = ininfo.PI_Qty
        pwinfo.P_EndOutQty = outinfo.PI_Qty
        If pccon.ProductionWareOutB_UpdateEndQty(pwinfo) = False Then
            MsgBox("結餘數量保存錯誤,請檢查原因!")
            Exit Sub
        End If

        ''------2013-10-23----------------------------------------------------------
        '生產計划
        Dim Qty As Int32
        If CheckEdit1.Checked = True Then
            Qty = CInt(Me.txtQty.Text)
        ElseIf CheckEdit1.Checked = False Then
            Qty = -CInt(Me.txtQty.Text)
        End If
        UpdateSchedule(strWHINID, cbTypeIN.EditValue, PM_M_CodeIN.EditValue, strM_CodeIN, gluTypeIN.EditValue, DateAddDate.EditValue, Qty)



        Me.Close()
    End Sub
#End Region

#Region "控件載入"
    Private Sub txtWHIN_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWHIN.ButtonClick
        tempCode = "生產倉庫"
        frmWareHouseSelect.SelectWareID = ""
        tempValue2 = "880705"
        tempValue3 = "880705"
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = MDIMain.Left + MDIMain.tvModule.Width + Me.Left + txtWHIN.Left + 25
        frmWareHouseSelect.Top = MDIMain.Top + Me.Top + txtWHIN.Top + txtWHIN.Height + 170
        frmWareHouseSelect.ShowDialog()
        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            strWHINID = frmWareHouseSelect.SelectWareID
            txtWHIN.Text = frmWareHouseSelect.SelectWareName
            txtWHIN.Tag = frmWareHouseSelect.SelectWareID

            LoadPM_M_Code()
        End If
    End Sub

    Private Sub txtWHOut_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWHOut.ButtonClick
        tempCode = "生產倉庫"
        frmWareHouseSelect.SelectWareID = ""
        tempValue2 = "880708"
        tempValue3 = "880708"
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = GroupBox2.Location.X + MDIMain.Left + MDIMain.tvModule.Width + Me.Left + txtWHOut.Left + 25
        frmWareHouseSelect.Top = GroupBox2.Location.Y + MDIMain.Top + Me.Top + txtWHOut.Top + txtWHOut.Height + 160
        frmWareHouseSelect.ShowDialog()
        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            strWHoutID = frmWareHouseSelect.SelectWareID
            txtWHOut.Text = frmWareHouseSelect.SelectWareName
            txtWHOut.Tag = frmWareHouseSelect.SelectWareID
        End If
    End Sub

    Private Sub gluTypeIN_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluTypeIN.EditValueChanged
        If gluTypeIN.EditValue = "" Or gluTypeIN.EditValue Is Nothing Then Exit Sub

        ''得到生產倉配件編號
        Dim PL As New List(Of ProcessMainInfo)
        Dim PC As New ProcessMainControl
        PL = PC.ProcessMain_GetList(Nothing, PM_M_CodeIN.EditValue, cbTypeIN.EditValue, gluTypeIN.EditValue, Nothing, Nothing)

        If PL.Count > 0 Then
            strM_CodeIN = PL(0).M_Code
            Dim pcw As New ProductInventoryController
            Dim pcl As New List(Of ProductInventoryInfo)

            pcl = pcw.ProductInventory_GetList(PM_M_CodeIN.EditValue, PL(0).M_Code, Me.txtWHIN.Tag, Nothing)
            If pcl.Count <= 0 Then
                lblQtyIn.Text = 0
            Else
                lblQtyIn.Text = pcl(0).PI_Qty
            End If
        Else
            strM_CodeIN = ""
            lblQtyIn.Text = 0
        End If
    End Sub
    Private Sub gluTypeOut_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluTypeOut.EditValueChanged
        If gluTypeOut.EditValue = "" Or gluTypeOut.EditValue Is Nothing Then Exit Sub

        Dim PL As New List(Of ProcessMainInfo)
        Dim PC As New ProcessMainControl
        PL = PC.ProcessMain_GetList(Nothing, PM_M_CodeOut.EditValue, cbTypeOut.EditValue, gluTypeOut.EditValue, Nothing, Nothing)

        If PL.Count > 0 Then
            strM_CodeOut = PL(0).M_Code
            Dim pcw As New ProductInventoryController
            Dim pcl As New List(Of ProductInventoryInfo)

            'MsgBox(PM_M_CodeOut.EditValue + "|" + PL(0).M_Code + "|" + Me.txtWHOut.Tag)

            pcl = pcw.ProductInventory_GetList(PM_M_CodeOut.EditValue, PL(0).M_Code, Me.txtWHOut.Tag, Nothing)
            If pcl.Count <= 0 Then
                lblQtyOut.Text = 0
            Else
                lblQtyOut.Text = pcl(0).PI_Qty
            End If
        Else
            strM_CodeOut = ""
            lblQtyOut.Text = 0
        End If
    End Sub
#End Region



    Sub UpdateSchedule(ByVal WH_ID As String, ByVal cbType As String, ByVal PM_M_Code As String, ByVal M_Code As String, ByVal PM_Type As String, ByVal FDate As String, ByVal PWO_Qty As Int32)

        '根據倉庫得到生產部
        Dim strFac As String
        Dim fcon As New FacControler
        Dim flist As New List(Of FacInfo)
        flist = fcon.GetFacListA(Nothing, Nothing, WH_ID)

        If flist.Count <= 0 Then
            Exit Sub
        Else
            strFac = flist(0).FacID
        End If
        '--------------------------------------------------------------------
        Dim psi As New ProductionScheduleInfo
        Dim psc As New ProductionScheduleControl
        Dim psi1 As New List(Of ProductionScheduleInfo)

        psi1 = psc.ProductionSchedule_GetList1(Nothing, cbType, PM_M_Code, M_Code, strFac, Nothing, FDate, FDate, PM_Type)

        If psi1.Count = 0 Then
        Else
            Dim m As Double
            m = psi1(0).PS_ActualNumber

            psi.PS_NO = psi1(0).PS_NO '得到當前單號
            psi.Pro_Type = cbType
            psi.PM_M_Code = psi1(0).PM_M_Code
            psi.PM_Type = psi1(0).PM_Type
            psi.PS_Dep = strFac
            psi.PS_Date = Format(CDate(FDate), "yyyy-MM-dd")
            psi.PS_ActualNumber = m + PWO_Qty

            psc.ProductionSchedule_UpdateActualNumber(psi)
        End If

    End Sub


End Class