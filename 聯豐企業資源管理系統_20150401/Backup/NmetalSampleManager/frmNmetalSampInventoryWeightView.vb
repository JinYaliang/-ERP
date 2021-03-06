Imports LFERP.Library.PieceProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleCollection
Imports LFERP.Library.NmetalSampleManager.NmetalSampInventoryWeightCheck
Imports LFERP.Library.NmetalSampleManager.NmetalSampleReWaste


Public Class frmNmetalSampInventoryWeightView
    Dim pncon As New PersonnelControl
    Dim pmlist As New List(Of PersonnelInfo)
    Dim nsdwc As New NmetalSampleDepWeightCheckControler
    Public nsdwci As New List(Of NmetalSampleDepWeightCheckInfo)
    Dim nsiwcmc As New NmetalSampInventoryWeightCheckMainControler
    Public nsiwcmi As New List(Of NmetalSampInventoryWeightCheckMainInfo)
    Dim nsrwc As New NmetalSampleReWasteControler
    Public nsrci As New List(Of NmetalSampleReWasteInfo)
    Private _EditItem As String          '属性栏位
    Private _SP_IDItem As String         '单号
    Private _StrD_IDItem As String       '部门ID
    Private _CheckItem As String         '审核类型
    Private _StratDateITem As String
    Private _EndDateITem As String
    Private _StrD_ID As String           '部门ID

    Property SP_IDItem() As String       '单号
        Get
            Return _SP_IDItem
        End Get
        Set(ByVal value As String)
            _SP_IDItem = value
        End Set
    End Property
    Property CheckItem() As String       '审核类型属性
        Get
            Return _CheckItem
        End Get
        Set(ByVal value As String)
            _CheckItem = value
        End Set
    End Property
    Property StrD_ID() As String         '部门ID
        Get
            Return _StrD_ID
        End Get
        Set(ByVal value As String)
            _StrD_ID = value
        End Set
    End Property
    Property StrD_IDItem() As String    '部门名称
        Get
            Return _StrD_IDItem
        End Get
        Set(ByVal value As String)
            _StrD_IDItem = value
        End Set
    End Property
    Property EditItem() As String       '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
    Property StratDateITem() As String  '起始日期
        Get
            Return _StratDateITem
        End Get
        Set(ByVal value As String)
            _StratDateITem = value
        End Set
    End Property
    Property EndDateITem() As String    '结束日期
        Get
            Return _EndDateITem
        End Get
        Set(ByVal value As String)
            _EndDateITem = value
        End Set
    End Property
    ''' <summary>
    ''' 窗体加载
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNmetalSampInventoryWeightView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select Case EditItem
            Case "NmetalSampInventoryWeightCheck"
                Me.Text = "盘点作业"
                lbl_Title.Text = "盘点作业查询"
                txt_DepName.Text = StrD_IDItem
                Exit Select
            Case "NmetalSampleDepWeightCheck"
                Me.Text = "部门库存"
                lbl_Title.Text = "部门库存查询"
                txt_DepName.Text = StrD_IDItem
                Exit Select
            Case "NmetalSampleReWaste"
                Me.Text = "废料/尾料处理"
                lbl_Title.Text = "废料/尾料处理查询"
                txt_DepName.Text = StrD_IDItem
                Exit Select
        End Select
        dateStratDate.Text = Format(DateAdd("m", -1, Now()), "yyyy/MM/dd")
        dateEndDate.Text = Format(Now, "yyyy/MM/dd")
    End Sub
    ''' <summary>
    ''' 查询按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If txt_OrderNO.Text <> String.Empty Then
            SP_IDItem = txt_OrderNO.Text
        End If
        If txt_DepName.Text <> String.Empty Then
            StrD_IDItem = StrD_ID
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

        If dateEndDate.Text < dateStratDate.Text Then
            MsgBox("结束日期不能小于起始日期,请重新选择!", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        Select Case cbeCheck.Text
            Case "ALL"
                CheckItem = Nothing
            Case "审核"
                CheckItem = True
            Case "未审核"
                CheckItem = False
        End Select
        Select Case EditItem
            Case "NmetalSampInventoryWeightCheck"
                nsiwcmi = nsiwcmc.NmetalSampInventoryWeightCheckMain_GetList(Nothing, SP_IDItem, StrD_IDItem, Nothing, StratDateITem, EndDateITem, CheckItem)
            Case "NmetalSampleDepWeightCheck"
                nsdwci = nsdwc.NmetalSampleDepWeightCheck_GetList(Nothing, SP_IDItem, Nothing, StrD_IDItem, Nothing, StratDateITem, EndDateITem, CheckItem)
            Case "NmetalSampleReWaste"
                nsrci = nsrwc.NmetalSampleReWaste_GetList(Nothing, SP_IDItem, StrD_IDItem, Nothing, Nothing, CheckItem, StratDateITem, EndDateITem)
        End Select
        Me.Close()
    End Sub
    ''' <summary>
    ''' 取消按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
End Class