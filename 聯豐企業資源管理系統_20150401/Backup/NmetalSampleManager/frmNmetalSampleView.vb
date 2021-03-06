Imports System
Imports System.IO
Imports System.Windows.Forms
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Product
Imports LFERP.Library.ProductionField
Imports LFERP.DataSetting
Imports LFERP.Library.NmetalSampleManager.NmetalSampleProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleCustomerFeedback
Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersMain
Imports LFERP.Library.NmetalSampleManager.NmetalSamplePace
Imports LFERP.Library.NmetalSampleManager.NmetalSamplePlan
Imports LFERP.Library.NmetalSampleManager.NmetalSampleProcessMain
Imports LFERP.Library.NmetalSampleManager.NmetalSampleSend
Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersSub

Public Class frmNmetalSampleView

    Dim ds As New DataSet
    Public SamplePlanList As New List(Of NmetalSamplePlanInfo)
    Public SampleSendList As New List(Of NmetalSampleSendInfo)
    Public SamplePaceList As New List(Of NmetalSamplePaceInfo)
    Public SampleOrdersList As New List(Of NmetalSampleOrdersMainInfo)
    Public CustomerFeedbackList As New List(Of NmetalSampleCustomerFeedbackinfo)
    Public SampleProcessList As New List(Of NmetalSampleProcessInfo)

    Private _SP_IDItem As String
    Private _SO_IDItem As String
    Private _PM_M_CodeITem As String
    Private _StratDateITem As String
    Private _EndDateITem As String
    Private _EditItem As String
    Private _CusterIDItem As String
    Private _CheckItem As String
    Private _M_CodeITem As String

    Property M_CodeITem() As String '属性
        Get
            Return _M_CodeITem
        End Get
        Set(ByVal value As String)
            _M_CodeITem = value
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
    Property SP_IDItem() As String '属性
        Get
            Return _SP_IDItem
        End Get
        Set(ByVal value As String)
            _SP_IDItem = value
        End Set
    End Property
    Property SO_IDItem() As String '属性
        Get
            Return _SO_IDItem
        End Get
        Set(ByVal value As String)
            _SO_IDItem = value
        End Set
    End Property
    Property StratDateITem() As String '属性
        Get
            Return _StratDateITem
        End Get
        Set(ByVal value As String)
            _StratDateITem = value
        End Set
    End Property
    Property EndDateITem() As String '属性
        Get
            Return _EndDateITem
        End Get
        Set(ByVal value As String)
            _EndDateITem = value
        End Set
    End Property
    Property PM_M_CodeITem() As String '属性
        Get
            Return _PM_M_CodeITem
        End Get
        Set(ByVal value As String)
            _PM_M_CodeITem = value
        End Set
    End Property
    Property CusterIDItem() As String '属性
        Get
            Return _CusterIDItem
        End Get
        Set(ByVal value As String)
            _CusterIDItem = value
        End Set
    End Property
    Property CheckItem() As String '属性
        Get
            Return _CheckItem
        End Get
        Set(ByVal value As String)
            _CheckItem = value
        End Set
    End Property

    Private Sub frmSamplePlanAddItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        _SP_IDItem = Nothing
        _SO_IDItem = Nothing
        _PM_M_CodeITem = Nothing
        _StratDateITem = Nothing
        _EndDateITem = Nothing
        _CusterIDItem = Nothing
        _CheckItem = Nothing
        '第一個
        Select Case EditItem
            Case "SamplePlan"
                lbl_StratDate.Text = "      开始(&S):"
                lbl_EndDate.Text = "      结束(&E):"
                lbl_CusterID.Enabled = False
                gluCuster.Enabled = False
                cbeCheck.Enabled = False
                lbl_DateType.Enabled = False
                cboDateType.Enabled = False
                Dim mtd As New NmetalSamplePlanControler
                glu_ID.Properties.DisplayMember = "SP_ID"
                glu_ID.Properties.ValueMember = "SP_ID"
                glu_ID.Properties.DataSource = mtd.NmetalSamplePlan_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing)
            Case "SampleSend"
                Me.cboDateType.Properties.Items.AddRange(New Object() {"新增日期", "寄送日期"})
                lbl_StratDate.Text = "寄送开始日(&S):"
                lbl_EndDate.Text = "寄送结束日(&E):"
                Dim mtd As New NmetalSampleSendControler
                Dim SSI As New List(Of NmetalSampleSendInfo)
                SSI = mtd.NmetalSampleSend_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing)
                glu_ID.Properties.DisplayMember = "SP_ID"
                glu_ID.Properties.ValueMember = "SP_ID"
                glu_ID.Properties.DataSource = SSI
                Dim C_U As New CusterControler
                gluCuster.Properties.DisplayMember = "C_ChsName"
                gluCuster.Properties.ValueMember = "C_CusterID"
                gluCuster.Properties.DataSource = C_U.GetCusterList(Nothing, Nothing, Nothing)
            Case "SamplePace"
                lbl_StratDate.Text = "新增开始日(&S):"
                lbl_EndDate.Text = "新增结束日(&E):"
                lbl_DateType.Enabled = False
                cboDateType.Enabled = False
                chk_ID.Enabled = False
                glu_ID.Enabled = False
                lbl_CusterID.Enabled = False
                gluCuster.Enabled = False
                cbeCheck.Enabled = False
            Case "SampleOrders"
                Me.cboDateType.Properties.Items.AddRange(New Object() {"新增日期", "PO日期", "交货日期"})
                chk_ID.Enabled = False
                glu_ID.Enabled = False
                Dim C_U As New CusterControler
                gluCuster.Properties.DisplayMember = "C_ChsName"
                gluCuster.Properties.ValueMember = "C_CusterID"
                gluCuster.Properties.DataSource = C_U.GetCusterList(Nothing, Nothing, Nothing)
            Case "SampleProcess"
                gluSO_ID.Enabled = False
                lbl_DateType.Enabled = False
                cboDateType.Enabled = False
                chk_ID.Enabled = False
                glu_ID.Enabled = False
                lbl_CusterID.Enabled = False
                gluCuster.Enabled = False
                cbeCheck.Enabled = False
                dateStratDate.Enabled = False
                dateEndDate.Enabled = False
            Case "CustomerFeedback"
                lbl_DateType.Enabled = False
                cboDateType.Enabled = False
                cbeCheck.Enabled = False
                glu_ID.Enabled = False
                chk_ID.Enabled = False
                lbl_Check.Enabled = False
                Dim C_U As New CusterControler
                gluCuster.Properties.DisplayMember = "C_ChsName"
                gluCuster.Properties.ValueMember = "C_CusterID"
                gluCuster.Properties.DataSource = C_U.GetCusterList(Nothing, Nothing, Nothing)
        End Select
        '第二個
        Dim SOS As New NmetalSampleOrdersSubControler
        gluSO_ID.Properties.DisplayMember = "SO_ID"
        gluSO_ID.Properties.ValueMember = "SO_ID"
        gluSO_ID.Properties.DataSource = SOS.NmetalSampleOrdersSub_GetList(Nothing, Nothing)
        '第三個
        Dim SOM As New NmetalSampleOrdersMainControler
        gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
        gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
        gluPM_M_Code.Properties.DataSource = SOM.NmetalSampleOrdersMain_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)


        'gluPM_M_Code.Properties.DataSource = SOM.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        '日期初始化'''''''''''''''''''''''''''''''''''
        dateStratDate.Text = Format(DateAdd("m", -1, Now()), "yyyy/MM/dd")
        dateEndDate.Text = Format(Now, "yyyy/MM/dd")
        ''''''''''''''''''''''''''''''''''''''''''''''''
        TreeList1.ExpandAll()
    End Sub
    ''' <summary>
    ''' 创建临时表
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("ProductSub") '子配件表
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_PID", GetType(String))
            .Columns.Add("M_KEY", GetType(String))
        End With
        '綁定表格
        Me.TreeList1.DataSource = ds.Tables("ProductSub")
    End Sub
    ''' <summary>
    ''' 查询处理程序
    ''' </summary>\\\\\\\\\\\\\\\\\\\
    ''' <param name="sender"></param>\
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If gluSO_ID.Text <> String.Empty Then
            SO_IDItem = gluSO_ID.Text
        End If
        If gluPM_M_Code.Text <> String.Empty Then
            PM_M_CodeITem = gluPM_M_Code.Text
        End If
        If gluM_Code.Tag <> String.Empty Then
            M_CodeITem = gluM_Code.Tag
        End If
        If gluCuster.Text <> String.Empty Then
            CusterIDItem = gluCuster.Text
        End If
        If glu_ID.Text <> String.Empty Then
            SP_IDItem = glu_ID.Text
        End If

        If dateStratDate.Text = String.Empty Then
            MsgBox("开始日期不能为空,请输入", MsgBoxStyle.Information, "提示")
            Exit Sub
            dateStratDate.Focus()
        Else
            StratDateITem = dateStratDate.Text
        End If

        If dateEndDate.Text = String.Empty Then
            MsgBox("结束日期不能为空,请输入", MsgBoxStyle.Information, "提示")
            Exit Sub
            dateEndDate.Focus()
        Else
            EndDateITem = dateEndDate.Text
        End If

        Select Case cbeCheck.Text
            Case "ALL"
                CheckItem = Nothing
            Case "审核"
                CheckItem = True
            Case "沒审核"
                CheckItem = False
        End Select

        Select Case EditItem
            Case "SamplePlan"
                Dim SamplePlan As New NmetalSamplePlanControler
                SamplePlanList = SamplePlan.NmetalSamplePlan_Getlist(SP_IDItem, SO_IDItem, PM_M_CodeITem, StratDateITem, EndDateITem, Nothing, Nothing, False, Nothing, Nothing, Nothing)
            Case "SampleSend"
                Dim SampleSend As New NmetalSampleSendControler
                If cboDateType.Text = "新增日期" Then
                    SampleSendList = SampleSend.NmetalSampleSend_Getlist(SP_IDItem, SO_IDItem, Nothing, M_CodeITem, CusterIDItem, PM_M_CodeITem, CheckItem, StratDateITem, EndDateITem, Nothing, Nothing, False, Nothing)
                End If
                If cboDateType.Text = "寄送日期" Then
                    SampleSendList = SampleSend.NmetalSampleSend_Getlist(SP_IDItem, SO_IDItem, Nothing, M_CodeITem, CusterIDItem, PM_M_CodeITem, CheckItem, Nothing, Nothing, StratDateITem, EndDateITem, False, Nothing)
                End If
            Case "SamplePace"
                Dim SamplePace As New NmetalSamplePaceControler
                SamplePaceList = SamplePace.NmetalSamplePace_Getlist(Nothing, SO_IDItem, Nothing, M_CodeITem, PM_M_CodeITem, StratDateITem, EndDateITem, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "SampleOrders"
                Dim SampleOrders As New NmetalSampleOrdersMainControler
                Select Case cboDateType.Text
                    Case "新增日期"
                        SampleOrdersList = SampleOrders.NmetalSampleOrdersMain_GetList(SO_IDItem, CusterIDItem, PM_M_CodeITem, M_CodeITem, CheckItem, StratDateITem, EndDateITem, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    Case "PO日期"
                        SampleOrdersList = SampleOrders.NmetalSampleOrdersMain_GetList(SO_IDItem, CusterIDItem, PM_M_CodeITem, M_CodeITem, CheckItem, Nothing, Nothing, StratDateITem, EndDateITem, Nothing, Nothing, Nothing, Nothing)
                    Case "交货日期"
                        SampleOrdersList = SampleOrders.NmetalSampleOrdersMain_GetList(SO_IDItem, CusterIDItem, PM_M_CodeITem, M_CodeITem, CheckItem, Nothing, Nothing, Nothing, Nothing, StratDateITem, EndDateITem, Nothing, Nothing)
                End Select
            Case "SampleProcess"
                Dim SPC As New NmetalSampleProcessControl
                SampleProcessList = SPC.NmetalSampleProcessMain_GetList1(Nothing, PM_M_CodeITem, "生產加工", M_CodeITem, Nothing, Nothing, Nothing, Nothing)
            Case "CustomerFeedback"
                Dim CustomerFeedback As New NmetalSampleCustomerFeedbackControler
                CustomerFeedbackList = CustomerFeedback.NmetalSampleCustomerFeedback_getlist(SO_IDItem, Nothing, CusterIDItem, Nothing, Nothing, Nothing, Nothing, PM_M_CodeITem, M_CodeITem, Nothing, False)
        End Select
        Me.Close()
    End Sub

    Private Sub SubRowAdd(ByVal pbil As List(Of ProductBomInfo))
        Dim pc As New ProductController
        Dim piL As List(Of ProductInfo)

        piL = pc.Product_GetList(gluPM_M_Code.Text.ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        ds.Tables("ProductSub").Clear()
        Dim row As DataRow
        row = ds.Tables("ProductSub").NewRow
        row("M_Name") = gluPM_M_Code.Text.ToString
        row("M_PID") = "0~"
        If pbil.Count > 0 Then
            row("M_Code") = piL(0).PM_M_Code
            row("M_KEY") = pbil(0).PM_PID
        Else
            row("M_Code") = ""
            row("M_KEY") = ""
        End If

        ds.Tables("ProductSub").Rows.Add(row)
        For i As Integer = 0 To pbil.Count - 1
            row = ds.Tables("ProductSub").NewRow
            row("M_Name") = pbil(i).M_Name
            row("M_Code") = pbil(i).M_Code
            row("M_PID") = pbil(i).PM_PID
            row("M_KEY") = pbil(i).PM_Key
            ds.Tables("ProductSub").Rows.Add(row)
        Next
        Me.TreeList1.ExpandAll()
    End Sub

    Private Sub TreeList1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeList1.MouseDoubleClick
        gluM_Code.Text = TreeList1.FocusedNode.GetValue("M_Name")
        gluM_Code.Tag = TreeList1.FocusedNode.GetValue("M_Code")
        PopupContainerControl1.OwnerEdit.ClosePopup()
    End Sub
    Private Sub gluM_Code_ButtonPressed(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles gluM_Code.ButtonPressed
        Dim pbc As New ProductBomController
        Dim pbiL As List(Of ProductBomInfo)
        pbiL = pbc.ProductBom_GetList(gluPM_M_Code.Text.ToString, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pbiL Is Nothing Then
            MsgBox("产品配件为空！")
        End If
        SubRowAdd(pbiL)

    End Sub
    Private Sub gluPM_M_Code_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_M_Code.Leave
        On Error Resume Next
        Dim pc As New ProductController
        Dim piL As List(Of ProductInfo)
        piL = pc.Product_GetList(gluPM_M_Code.Text.ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Dim pbc As New ProductBomController
        Dim pbiL As List(Of ProductBomInfo)
        pbiL = pbc.ProductBom_GetList(gluPM_M_Code.Text.ToString, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pbiL Is Nothing Then
            MsgBox("产品配件为空！")
        End If
        SubRowAdd(pbiL)
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()

        Select Case EditItem
            Case "SamplePlan"
                SampleSendList = Nothing
                SamplePaceList = Nothing
                SampleOrdersList = Nothing
                CustomerFeedbackList = Nothing
                SampleProcessList = Nothing
            Case "SampleSend"
                SamplePlanList = Nothing
                SamplePaceList = Nothing
                SampleOrdersList = Nothing
                CustomerFeedbackList = Nothing
                SampleProcessList = Nothing
            Case "SamplePace"
                SamplePlanList = Nothing
                SampleSendList = Nothing
                SampleOrdersList = Nothing
                CustomerFeedbackList = Nothing
                SampleProcessList = Nothing
            Case "SampleOrders"
                SamplePlanList = Nothing
                SampleSendList = Nothing
                SamplePaceList = Nothing
                CustomerFeedbackList = Nothing
                SampleProcessList = Nothing
            Case "SampleProcess"
                SamplePlanList = Nothing
                SampleSendList = Nothing
                SamplePaceList = Nothing
                SampleOrdersList = Nothing
                CustomerFeedbackList = Nothing
            Case "CustomerFeedback"
                SamplePlanList = Nothing
                SampleSendList = Nothing
                SamplePaceList = Nothing
                SampleOrdersList = Nothing
                SampleProcessList = Nothing
        End Select
    End Sub
End Class