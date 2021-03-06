Imports LFERP.DataSetting
Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersMain
Imports LFERP.Library.NmetalSampleManager.NmetalSampleCollection
Imports LFERP.Library.NmetalSampleManager.NmetalSamplePace
Imports LFERP.Library.NmetalSampleManager.NmetalSampleStorage

Public Class frmNmetalSampleStorageCodeAdd
#Region "属性和全局变量"
    Private _EditItem As String '属性栏位

    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Private _SSID As Decimal '自动编码

    Property SSID() As Decimal
        Get
            Return _SSID
        End Get
        Set(ByVal value As Decimal)
            _SSID = value
        End Set
    End Property

    Dim ds As New DataSet
    Dim sscon As New NmetalSampleStorageController
    Dim sslcon As New NmetalSampleStorageLogController
    Dim result As Boolean
    Dim spqList As New List(Of NmetalSamplePaceQueryInfo)
    Dim SSList As New List(Of NmetalSampleStorageInfo)
#End Region

#Region "加载"

    Sub Loadsampleorders()
        SSList = sscon.NmetalSampleStorage_GetList(SSID, Nothing, Nothing, Nothing, Nothing)
        If SSList.Count <= 0 Then
            Exit Sub
        End If
        'txtD_ID.EditValue = SSList(0).D_ID
        'txtSS_ShelveID.Text = SSList(0).SS_ShelveID
        'txtSS_StorageLocation.Text = SSList(0).SS_StorageLocation
        'txtCode.Text = SSList(0).Code_ID
        'txtD_ID.EditValue = SSList(0).D_ID
    End Sub

    Private Sub frmSampleStorageCodeAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim pclist As New List(Of LFERP.Library.ProductionController.ProductionFieldControlInfo)
        Dim pminfo As New LFERP.Library.ProductionController.ProductionFieldControlInfo
        Dim fc As New LFERP.Library.ProductionController.ProductionFieldControl
        pclist = fc.ProductionFieldControl_GetList(InUserID, Nothing)
        'Me.txtD_ID.Properties.DataSource = pclist
        'Me.txtD_ID.Properties.DisplayMember = "DepName"
        'Me.txtD_ID.Properties.ValueMember = "ControlDep"

        Dim spqInfo As New NmetalSamplePaceQueryInfo
        spqInfo.Code_ID = "All"
        spqList.Insert(0, spqInfo)
        Select Case EditItem
            Case EditEnumType.EDIT
                Loadsampleorders()
        End Select
    End Sub
#End Region

#Region "保存"

    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub Savebutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ssInfo As New NmetalSampleStorageInfo
        Dim Type As String = String.Empty
        If CheckDateEmpty() = True Then
            Select Case EditItem
                Case EditEnumType.EDIT
                    Type = "Edit"
                    'SaveEdit(Type)
                Case EditEnumType.CHECK
                    Type = "Check"
                    SaveCheck(Type)
            End Select
            If result = True Then
                MsgBox("保存成功！", 60, "提示")
                Me.Close()
            End If
        End If
    End Sub

    'Private Sub SaveLog(ByVal ssinfo As SampleStorageInfo, ByVal Type As String)
    '    Dim sslInfo As New SampleStorageLogInfo
    '    sslInfo.Code_ID = txtCode.Text
    '    sslInfo.CreateUserID = InUserID
    '    sslInfo.D_ID = SSList(0).D_ID
    '    sslInfo.OperateType = Type
    '    sslInfo.SS_ShelveID = txtSS_ShelveIDNew.Text
    '    sslInfo.SO_ID = SSList(0).SO_ID
    '    sslInfo.SS_StorageLocation = txtSS_StorageLocationNew.Text
    '    sslInfo.SO_SampleID = SSList(0).SO_SampleID
    '    sslcon.SampleStorageLog_Add(sslInfo)
    'End Sub

    'Private Sub SaveEdit(ByVal Type As String)
    '    Dim ssinfo As New SampleStorageInfo
    '    ssinfo.AutoID = SSID
    '    ssinfo.ModifyUserID = InUserID
    '    'ssinfo.D_ID = txtD_IDNew.EditValue
    '    ssinfo.Remark = txtRemarkNew.Text
    '    ssinfo.SS_ShelveID = txtSS_ShelveIDNew.Text
    '    'ssinfo.SO_ID = txtSO_ID.Text
    '    ssinfo.SS_StorageLocation = txtSS_StorageLocationNew.Text
    '    'ssinfo.SampleID = txtSampleID.Text
    '    'ssinfo.SE_ID = txtSE_ID.Text
    '    result = sscon.SampleStorage_Update(ssinfo)
    '    SaveLog(ssinfo, Type)
    'End Sub

    Private Sub SaveCheck(ByVal Type As String)
        'Dim ssinfo As New SampleStorageInfo
        'ssinfo.CheckBit = ceCheck.Checked
        'ssinfo.CheckRemark = txtCheckRemark.EditValue
        'ssinfo.CheckUserID = InUserID
        'ssinfo.AutoID = SSID
        'result = sscon.SampleStorage_Check(ssinfo)
        'SaveLog(ssinfo, Type)
    End Sub

    Function CheckDateEmpty() As Boolean
        '--------主檔
        CheckDateEmpty = True
        'If txtD_ID.Text = String.Empty Then
        '    CheckDateEmpty = False
        '    MsgBox("您沒有输入部门编号！", MsgBoxStyle.OkOnly, "提示")
        '    txtD_ID.Focus()
        '    Exit Function
        'End If
        'If txtSS_ShelveID.Text = String.Empty Then
        '    CheckDateEmpty = False
        '    MsgBox("您沒有输入货架编号！", MsgBoxStyle.OkOnly, "提示")
        '    txtSS_ShelveID.Focus()
        '    Exit Function
        'End If
        'If txtSS_StorageLocation.Text = String.Empty Then
        '    CheckDateEmpty = False
        '    MsgBox("您沒有输入区域位置！", MsgBoxStyle.OkOnly, "提示")
        '    txtSS_StorageLocation.Focus()
        '    Exit Function
        'End If
    End Function

#End Region



End Class