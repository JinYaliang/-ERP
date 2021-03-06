Imports LFERP.DataSetting
Imports LFERP.Library.SampleManager.SampleCustomerFeedback

Public Class frmCustomerFeedbackAdd
#Region "属性"
    Dim cc As New SampleCustomerFeedbackControler
    Dim ds As New DataSet
    Dim cco As New CusterControler
    Private _sendQty As Integer  '属性栏位
    Private _EditItem As String
    Private _EditValue As String
    Property EditValue() As String
        Get
            Return _EditValue
        End Get
        Set(ByVal value As String)
            _EditValue = value
        End Set
    End Property
    Property sendQty() As Integer '属性
        Get
            Return _sendQty
        End Get
        Set(ByVal value As Integer)
            _sendQty = value
        End Set
    End Property
    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
#End Region

#Region "窗体载入事件"
    Private Sub frmCustomerFeedbackAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTables()
        Select Case EditItem
            Case "ADD"
                det_date.EditValue = Format(Now, "yyyy/MM/dd")
                gue_Customerid.Properties.DisplayMember = "C_ChsName"
                gue_Customerid.Properties.ValueMember = "C_CusterID"
                gue_Customerid.Properties.DataSource = cco.GetCusterList(Nothing, Nothing, Nothing)
                GridView2.Columns("SC_Confirmation").Visible = False
            Case "Edit"
                LoadData(EditValue)
                gue_Customerid.Enabled = False
                GridView2.Columns("SC_Confirmation").Visible = False
                Label2.Text = "修改客戶反馈意见"
                Me.Text = Label2.Text
            Case "Look"
                LoadData(EditValue)
                gue_Customerid.Enabled = False
                det_date.Enabled = False
                Grid1.Enabled = False
                SaveButton.Visible = False
                Label2.Text = "样版客戶反馈查看"
                Me.Text = Label2.Text
            Case "Comfarmation"
                LoadData(EditValue)
                gue_Customerid.Enabled = False
                GridView2.Columns("SC_Confirmation").Visible = True
                GridView2.Columns("SC_ConfirmationQty").Visible = True
                Label2.Text = "样版确认"
                Me.Text = Label2.Text
            Case Else
                Exit Select
        End Select
    End Sub
#End Region

#Region "创建临时表"
    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("SampleCustomerFeedback")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("SO_ID", GetType(String))
            .Columns.Add("SC_Edition", GetType(String))
            .Columns.Add("SC_CustomerID", GetType(String))
            .Columns.Add("SC_Description", GetType(String))
            .Columns.Add("SC_Picture", GetType(String))
            .Columns.Add("SC_Process", GetType(String))
            .Columns.Add("SC_Confirmation", GetType(String))
            .Columns.Add("SC_ConfirmationDate", GetType(Date))
            .Columns.Add("SC_ConfirmationQty", GetType(Integer))
            .Columns.Add("SC_AdduserID", GetType(String))
            .Columns.Add("SC_Adddate", GetType(Date))
            .Columns.Add("SC_ModifyUserID", GetType(String))
            .Columns.Add("SC_ModifyDate", GetType(Date))
            .Columns.Add("PM_M_Code", GetType(String)) '
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("SO_Gauge", GetType(String))
            .Columns.Add("flag", GetType(String))
            .Columns.Add("SO_CusterID", GetType(String))
            .Columns.Add("SO_CusterPO", GetType(String))
            .Columns.Add("SC_SendNo", GetType(String))
            .Columns.Add("SC_Remark", GetType(String))
        End With
        Grid1.DataSource = ds.Tables("SampleCustomerFeedback")

        With ds.Tables.Add("SampleCustomerFeedbackDel")
            .Columns.Add("AutoID", GetType(String))
        End With
    End Sub
#End Region

#Region "数据载入"
    Sub LoadData(ByVal str As String)
        Dim cl, cl1 As New List(Of SampleCustomerFeedbackinfo)
        cl = cc.SampleCustomerFeedback_getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, str, False)
        If cl.Count <= 0 Then
            MsgBox("无記錄存在")
            Me.Close()
        End If

        Dim i As Integer
        For i = 0 To cl.Count - 1
            Dim row As DataRow
            row = ds.Tables("SampleCustomerFeedback").NewRow

            row("AutoID") = cl(i).AutoID
            row("SO_ID") = cl(i).SO_ID
            ''''''''''''''''''''''''''''''''''''''''''
            gue_Customerid.EditValue = cl(i).SC_customerid
            gue_Customerid.Text = cl(i).SO_CusterNo
            gue_Customerid.Properties.NullText = cl(i).SO_CusterNo
            ''''''''''''''''''''''''''''''''''''''''''
            row("SC_Edition") = cl(i).SC_Edition
            row("SC_CustomerID") = cl(i).SC_customerid
            row("SC_Description") = cl(i).SC_Description
            row("SC_Picture") = cl(i).SC_Picture
            row("SC_Process") = cl(i).SC_Process
            row("SC_Confirmation") = cl(i).SC_Confirmation

            If cl(i).SC_Confirmationdate.ToString <> "" Then
                row("SC_ConfirmationDate") = cl(i).SC_Confirmationdate
            End If

            row("SC_ConfirmationQty") = cl(i).SC_ConfirmationQty
            row("SC_AdduserID") = cl(i).SC_AdduserID
            row("SC_ConfirmationQty") = sendQty

            row("SC_Adddate") = cl(i).SC_Adddate
            det_date.Text = cl(i).SC_Adddate
            row("SC_ModifyUserID") = cl(i).SC_ModifyuserID
            row("SC_ModifyDate") = cl(i).SC_Modifydate
            row("PM_M_Code") = cl(i).PM_M_Code
            row("M_Code") = cl(i).M_Code
            row("SO_Gauge") = cl(i).SO_Gauge
            row("SC_SendNo") = cl(i).SC_SendNo
            row("M_Name") = cl(i).M_Name
            ds.Tables("SampleCustomerFeedback").Rows.Add(row)
        Next
    End Sub
#End Region

#Region "查询订单寄送数据"
    ''' <summary>
    '''   查询订单数据  
    ''' </summary>
    Sub loadSampleOrder(ByVal str As String)
        Dim cl As New List(Of SampleCustomerFeedbackinfo)
        cl = cc.getSampleOrdersinfo(str)
        If cl.Count <= 0 Then
            MsgBox("无記錄存在")
            Exit Sub
        End If

        Dim i As Integer
        ds.Tables("SampleCustomerFeedback").Clear()
        For i = 0 To cl.Count - 1
            Dim row As DataRow
            row = ds.Tables("SampleCustomerFeedback").NewRow
            row("SO_CusterID") = cl(i).SO_CusterID
            row("SO_CusterPO") = cl(i).SO_CusterPO
            row("PM_M_Code") = cl(i).PM_M_Code
            row("M_Code") = cl(i).M_Code
            row("SO_Gauge") = cl(i).SO_Gauge
            row("SC_Edition") = cl(i).SC_Edition
            ds.Tables("SampleCustomerFeedback").Rows.Add(row)
        Next
    End Sub
    ''' <summary>
    '''   查询寄送数据  
    ''' </summary>
    Sub loadSampleSendNo()
        Dim cl As New List(Of SampleCustomerFeedbackinfo)
        cl = cc.SampleSend_GetList(Nothing)
        Dim i As Integer
        cmb_sendNo.Items.Clear()
        For i = 0 To cl.Count - 1
            Dim row As DataRow
            row = ds.Tables("SampleCustomerFeedback").NewRow
            row("PM_M_Code") = cl(i).PM_M_Code
            row("M_Code") = cl(i).M_Code
            row("SC_Edition") = cl(i).SC_Edition
            ds.Tables("SampleCustomerFeedback").Rows.Add(row)
        Next
    End Sub
#End Region

#Region "数据检查"
    Function CheckDate() As Boolean
        If gue_Customerid.Text = "" Then
            MsgBox("你沒有选择订单编号", MsgBoxStyle.OkOnly, "提示")
            Return False
        End If
        If det_date.Text = "" Then
            MsgBox("你沒有选择日期", MsgBoxStyle.OkOnly, "提示")
            Return False
        End If
        Dim i As Int16
        For i = 0 To ds.Tables("SampleCustomerFeedback").Rows.Count - 1
            If ds.Tables("SampleCustomerFeedback").Rows(i)("PM_M_Code").ToString = "" Then
                MsgBox("你沒有选择产品", MsgBoxStyle.OkOnly, "提示")
                Return False
            End If
            If ds.Tables("SampleCustomerFeedback").Rows(i)("SC_Description").ToString = "" Then
                MsgBox("你沒有输入反馈意见", MsgBoxStyle.OkOnly, "提示")
                Return False
            End If
            If ds.Tables("SampleCustomerFeedback").Rows(i)("SC_Edition").ToString = "" Then
                MsgBox("你沒有输入版本", MsgBoxStyle.OkOnly, "提示")
                Return False
            End If
        Next
        Return True
    End Function
#End Region

#Region "数据保存"
    Private Sub Savedate()
        Dim ci As New SampleCustomerFeedbackinfo
        Dim j As Integer
        Dim b As Boolean
        ci.SO_CusterID = gue_Customerid.EditValue
        ci.SC_Adddate = det_date.Text

        With ds.Tables("SampleCustomerFeedback")
            For j = 0 To .Rows.Count - 1
                ci.SC_Process = .Rows(j)("sc_process").ToString
                ci.SC_Picture = .Rows(j)("sc_picture").ToString
                ci.SC_Description = .Rows(j)("sc_description").ToString
                ci.PM_M_Code = .Rows(j)("PM_M_Code").ToString
                ci.M_Code = .Rows(j)("M_Code").ToString
                ci.SC_SendNo = .Rows(j)("SC_SendNo").ToString
                ci.AutoID = IIf(IsDBNull(.Rows(j)("AutoID")), 0, .Rows(j)("AutoID"))
                ci.SC_Confirmation = .Rows(j)("SC_Confirmation").ToString
                ci.SC_Edition = .Rows(j)("SC_Edition").ToString
                ci.SO_ID = .Rows(j)("SO_ID").ToString
                ci.SC_Remark = .Rows(j)("SC_Remark").ToString
                ci.SC_ConfirmationQty = IIf(IsDBNull(.Rows(j)("SC_ConfirmationQty")), 0, .Rows(j)("SC_ConfirmationQty"))
                ci.SC_Modifydate = Format(Now, "yyyy/MM/dd")
                ci.SC_Confirmationdate = Format(Now, "yyyy/MM/dd")
                ci.SC_Adddate = Format(Now, "yyyy/MM/dd")
                ci.SC_AdduserID = InUserID
                ci.SC_ModifyuserID = InUserID
                '-----------------------------
                Select Case EditItem
                    Case "ADD"
                        b = cc.SampleCustomerFeedback_Add(ci)
                    Case "Comfarmation"
                        If ci.SC_Confirmation <> "" Then
                            Select Case ci.SC_Confirmation.Substring(0, 1)
                                Case "A" 'A.样版结单
                                    b = cc.SampleCustomerFeedback_Update_Comfarmation(ci)
                                Case "B" 'B.增加新版本
                                    b = cc.SampleCustomerFeedback_Update_Comfarmation(ci)
                                    AddNewVer()
                                Case "C" 'C.送货未完成
                                    b = cc.SampleCustomerFeedback_Update_Comfarmation(ci)
                            End Select
                        End If
                    Case "Edit"
                        b = cc.SampleCustomerFeedback_Update(ci)
                End Select
                '-----------------------------
                If b = False Then
                    MsgBox("部分保存失敗！", MsgBoxStyle.OkOnly, "提示")
                    Exit Sub
                End If
            Next
        End With
        MsgBox("保存成功！", 60, "提示")
        Me.Close()
    End Sub
#End Region

#Region "新增版本"
    ''' <summary>
    '''  新增版本   
    ''' </summary>
    Private Sub AddNewVer()
        Dim ci As New SampleCustomerFeedbackinfo
        Dim j As Integer
        ci.SO_ID = GridView2.GetFocusedRowCellValue("SO_ID").ToString()
        ci.SC_Adddate = det_date.Text
        For j = 0 To ds.Tables("SampleCustomerFeedback").Rows.Count - 1
            ci.PM_M_Code = ds.Tables("SampleCustomerFeedback").Rows(j)("PM_M_Code")
            ci.SC_Remark = ds.Tables("SampleCustomerFeedback").Rows(j)("SC_Remark").ToString
            If cc.SampleOrdersSub_Addedition(ci.SO_ID, ci.PM_M_Code, ci.SC_Remark) = False Then
                MsgBox("保存失敗！")
                Exit Sub
            End If
        Next
    End Sub
#End Region

#Region "子表菜单事件"
    Private Sub DelDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelSub.Click
        'ds.Tables("SampleCustomerFeedback").Rows.RemoveAt(GridView2.FocusedRowHandle)
        If GridView2.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView2.GetRowCellDisplayText(GridView2.GetSelectedRows(0), "AutoID")
        If DelTemp <> String.Empty Then
            Dim row As DataRow = ds.Tables("SampleCustomerFeedbackDel").NewRow
            row("AutoID") = ds.Tables("SampleCustomerFeedback").Rows(GridView2.FocusedRowHandle)("AutoID")
            ds.Tables("SampleCustomerFeedbackDel").Rows.Add(row)
        End If
        ds.Tables("SampleCustomerFeedback").Rows.RemoveAt(GridView2.GetSelectedRows(0))
    End Sub
    '右鍵添加
    Private Sub AddToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddSub.Click
        If gue_Customerid.Text <> String.Empty Then
            '--------------------------------------------
            Dim StrCustom As String = gue_Customerid.EditValue
            Dim scfcon As New SampleCustomerFeedbackControler
            Dim objinfo As New List(Of SampleCustomerFeedbackinfo)
            objinfo = scfcon.SampleSend_GetList(StrCustom)
            If objinfo.Count <= 0 Then
                MsgBox("无記錄存在")
                Exit Sub
            End If

            '------------------------------------
            Dim fr As New frmCustomerFeedbackSelect
            fr = New frmCustomerFeedbackSelect
            fr.EditItem = StrCustom
            fr.ShowDialog()

            Dim m As Integer
            For m = 0 To fr.SampleList.Count - 1
                Dim BoolEdit As Boolean
                BoolEdit = True
                Dim x As Integer
                For x = 0 To ds.Tables("SampleCustomerFeedback").Rows.Count - 1
                    If fr.SampleList.Item(m).SS_Edition = ds.Tables("SampleCustomerFeedback").Rows(x)("SC_Edition").ToString Then
                        BoolEdit = False
                    End If
                Next

                If BoolEdit = True Then
                    Dim row As DataRow = ds.Tables("SampleCustomerFeedback").NewRow
                    row("SC_Edition") = fr.SampleList.Item(m).SC_Edition
                    row("PM_M_Code") = fr.SampleList.Item(m).PM_M_Code
                    row("M_Code") = fr.SampleList.Item(m).M_Code
                    row("M_Name") = fr.SampleList.Item(m).M_Name
                    row("SO_ID") = fr.SampleList.Item(m).SO_ID
                    row("SC_SendNo") = fr.SampleList.Item(m).SC_SendNo
                    ds.Tables("SampleCustomerFeedback").Rows.Add(row)
                End If
            Next
        Else
            MsgBox("请先选择客戶名称", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If
    End Sub

    Private Sub cmb_sendNo_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_sendNo.EditValueChanged
        Dim row As DataRow
        row = ds.Tables("SampleCustomerFeedback").NewRow
        ds.Tables("SampleCustomerFeedback").Rows.Add(row)

        Dim cl As New List(Of SampleCustomerFeedbackinfo)
        Dim i As Integer
        For i = 0 To cl.Count - 1
        Next
    End Sub
#End Region

#Region "按键事件"
    '保存
    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        Select Case EditItem
            Case "ADD"
                If CheckDate() = False Then
                    Exit Sub
                Else
                    Savedate()
                End If
            Case "Edit"
                If CheckDate() = False Then
                    Exit Sub
                Else
                    Savedate()
                End If
            Case "Comfarmation"
                If CheckDate() = False Then
                    Exit Sub
                Else
                    Savedate()
                End If
        End Select
    End Sub
    Private Sub cmb_Confirmation_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Confirmation.EditValueChanged
        If sender.text.ToString.Substring(0, 1) = "B" Then
            Dim fm As New frmCustomerFeedbackRemark
            Dim verstr As String = ""
            fm.ShowDialog()
            Dim row As DataRow = ds.Tables("SampleCustomerFeedback").Rows(GridView2.FocusedRowHandle)
            row("SC_Remark") = fm.VERRemark
        End If
    End Sub
    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Me.Close()
    End Sub
#End Region

End Class