Imports LFERP.Library.NmetalSampleManager.NmetalSampleAlarm
Public Class frmNmetalSampAlarmAdd
    Private _EditItem As String '属性栏位
    Private _GetSE_ID As String
    Private boolCheck As Boolean
    Dim ds As New DataSet

    Dim sacon As New NmetalSampleAlarmController
    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
    Property GetSE_ID() As String '属性
        Get
            Return _GetSE_ID
        End Get
        Set(ByVal value As String)
            _GetSE_ID = value
        End Set
    End Property

    Private Sub frmSampAlarmAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select Case EditItem
            Case EditEnumType.ADD
                CreateTable("Othen")
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.ADD)
                Me.Text = lblTitle.Text
                Me.gbCheck.Visible = False
                LoadData(GetSE_ID)
            Case EditEnumType.EDIT
                CreateTable("Othen")
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.EDIT)
                Me.Text = lblTitle.Text
                Me.gbCheck.Visible = False
                LoadData(GetSE_ID)
            Case EditEnumType.VIEW
                CreateTable("ALL")
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.VIEW)
                Me.Text = lblTitle.Text
                Me.txtProcessResult.Enabled = False
                Me.gluD_ID.Enabled = False
                Me.txtDutyUserID.Enabled = False
                Me.txtOccurReason.Enabled = False
                Me.gbCheck.Visible = True
                LoadData(GetSE_ID)
            Case EditEnumType.CHECK
                CreateTable("ALL")
                Me.lblTitle.Text = Me.Text + "处理"
                Me.Text = lblTitle.Text
                Me.txtProcessResult.Enabled = False
                Me.gbCheck.Visible = True
                Me.gluD_ID.Enabled = False
                Me.txtDutyUserID.Enabled = False
                Me.txtOccurReason.Enabled = False
                LoadData(GetSE_ID)
        End Select
    End Sub

    Sub CreateTable(ByVal DelAll As String)
        Select Case DelAll
            Case "ALL"
                Dim pclist As New List(Of LFERP.Library.ProductionController.ProductionFieldControlInfo)
                Dim pminfo As New LFERP.Library.ProductionController.ProductionFieldControlInfo
                Dim fc As New LFERP.Library.ProductionController.ProductionFieldControl
                pclist = fc.ProductionFieldControl_GetList(Nothing, Nothing)
                Me.gluD_ID.Properties.DataSource = pclist
            Case "Othen"
                ds.Tables.Clear()
                With ds.Tables.Add("SampAlarmDep") '子配件表
                    .Columns.Add("ControlDep", GetType(String))
                    .Columns.Add("DepName", GetType(String))
                End With
                Me.gluD_ID.Properties.DataSource = ds.Tables("SampAlarmDep")
        End Select
    End Sub

    Private Sub LoadData(ByVal GetSD_ID As String)
        Dim salist As List(Of NmetalSampleAlarmInfo)
        salist = sacon.NmetalSampleAlarm_GetList(GetSD_ID)
        If salist.Count > 0 Then
            Me.txt_SO_SampleID.Text = salist(0).SO_SampleID
            Me.txtPM_M_Code.Text = salist(0).PM_M_Code
            Me.txtProcessResult.Text = salist(0).ProcessResult
            Me.txtSE_AddDate.Text = salist(0).SE_AddDate
            Me.txtSE_AddUserName.Text = salist(0).SE_AddUserName
            Me.txtSE_ID.Text = salist(0).SE_ID
            Me.txtSE_InCardID.Text = salist(0).SE_InCardID
            Me.txtSE_InD_Dep.Text = salist(0).SE_InD_Dep
            Me.txtSE_OutCardID.Text = salist(0).SE_OutCardID
            Me.txtSE_OutD_Dep.Text = salist(0).SE_OutD_Dep
            Me.txtSE_Qty.Text = salist(0).SE_Qty
            Me.txtOccurReason.Text = salist(0).OccurReason
            Me.txtDutyUserID.Text = salist(0).DutyUserID

            If EditItem = EditEnumType.ADD Or EditItem = EditEnumType.EDIT Then
                If salist(0).SE_OutD_ID <> String.Empty Then
                    Dim row As DataRow
                    row = ds.Tables("SampAlarmDep").NewRow
                    row("ControlDep") = salist(0).SE_OutD_ID
                    row("DepName") = salist(0).SE_OutD_Dep
                    ds.Tables("SampAlarmDep").Rows.Add(row)
                End If

                If salist(0).SE_InD_ID <> String.Empty Then
                    Dim row As DataRow
                    row = ds.Tables("SampAlarmDep").NewRow
                    row("ControlDep") = salist(0).SE_InD_ID
                    row("DepName") = salist(0).SE_InD_Dep
                    ds.Tables("SampAlarmDep").Rows.Add(row)
                End If
            End If

            Me.gluD_ID.Text = salist(0).D_ID
            Me.ce_IsCheck.Checked = salist(0).CheckBit
            Me.LabelControl1.Text = salist(0).CheckActionName
            Me.txtOccurAddress.Text = salist(0).OccurAddress
            Me.txtRemark.Text = salist(0).Remark

            If salist(0).CheckDate = Nothing Then
                Me.LabelControl2.Text = Format(Now, "yyyy/MM/dd")
            Else
                Me.LabelControl2.Text = salist(0).CheckDate
            End If
            boolCheck = salist(0).CheckBit
        End If
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Me.Close()
    End Sub

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        If Me.gluD_ID.EditValue = String.Empty Then
            MsgBox("责任部门不能为空", MsgBoxStyle.Information, "提示")
            Me.gluD_ID.Focus()
            Exit Sub
        End If
        If Me.txtDutyUserID.EditValue = String.Empty Then
            MsgBox("责任人不能为空", MsgBoxStyle.Information, "提示")
            Me.txtDutyUserID.Focus()
            Exit Sub
        End If
        If Me.txtOccurAddress.Text = String.Empty Then
            MsgBox("发生地点不能为空", MsgBoxStyle.Information, "提示")
            Me.txtOccurAddress.Focus()
            Exit Sub
        End If
        If Me.txtOccurReason.Text = String.Empty Then
            MsgBox("发生原因不能为空", MsgBoxStyle.Information, "提示")
            Me.txtOccurReason.Focus()
            Exit Sub
        End If
        If Me.txtProcessResult.Text = String.Empty Then
            MsgBox("解决对策不能为空", MsgBoxStyle.Information, "提示")
            Me.txtProcessResult.Focus()
            Exit Sub
        End If

        Select Case EditItem
            Case EditEnumType.ADD
                NewData()
            Case EditEnumType.EDIT
                EditData()
            Case EditEnumType.CHECK
                If boolCheck = Me.ce_IsCheck.Checked Then
                    MsgBox("处理状态没有改变！", MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If
                CheckData()
        End Select
    End Sub

    Sub NewData()
        Dim sainfo As New NmetalSampleAlarmInfo
        sainfo.SE_ID = Me.txtSE_ID.Text                     '收发单号
        sainfo.ProcessResult = Me.txtProcessResult.Text     '解决对策
        sainfo.AddUserID = InUserID                         '新增用户
        sainfo.AddDate = Format(Now, "yyyy/MM/dd")          '新增日期

        sainfo.D_ID = gluD_ID.EditValue                     '责任部门
        sainfo.DutyUserID = Me.txtDutyUserID.Text           '责任人
        sainfo.OccurReason = txtOccurReason.Text            '发生原因
        sainfo.OccurAddress = Me.txtOccurAddress.Text       '发生地点
        sainfo.Remark = Me.txtRemark.Text                   '备注
        If sacon.NmetalSampleAlarm_Add(sainfo) = False Then
            MsgBox("注意:信息新增处理失败！", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        MsgBox("新增信息处理完成", MsgBoxStyle.Information, "提示")
        Me.Close()
    End Sub

    Sub EditData()
        Dim sainfo As New NmetalSampleAlarmInfo
        sainfo.SE_ID = Me.txtSE_ID.Text                     '收发单号
        sainfo.ProcessResult = Me.txtProcessResult.Text     '解决对策
        sainfo.ModifyUserID = InUserID                      '修改用户
        sainfo.ModifyDate = Format(Now, "yyyy/MM/dd")       '修改日期

        sainfo.D_ID = gluD_ID.EditValue                     '责任部门
        sainfo.DutyUserID = Me.txtDutyUserID.Text           '责任人
        sainfo.OccurReason = txtOccurReason.Text            '发生原因
        sainfo.OccurAddress = Me.txtOccurAddress.Text       '发生地点
        sainfo.Remark = Me.txtRemark.Text                   '备注
        If sacon.NmetalSampleAlarm_Update(sainfo) = False Then
            MsgBox("注意:信息修改处理失败！", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        MsgBox("修改信息处理完成", MsgBoxStyle.Information, "提示")
        Me.Close()
    End Sub

    Sub CheckData()
        Dim sainfo As New NmetalSampleAlarmInfo
        sainfo.SE_ID = Me.txtSE_ID.Text                     '收发单号
        sainfo.CheckAction = InUserID                       '审核人
        sainfo.CheckBit = Me.ce_IsCheck.Checked             '审核
        sainfo.CheckRemark = String.Empty                   '审核备注
        sainfo.CheckDate = Format(Now, "yyyy/MM/dd")        '审核日期

        If sacon.NmetalSampleAlarm_Check(sainfo) = False Then
            MsgBox("注意:信息修改处理失败！", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        If ce_IsCheck.Checked Then
            MsgBox("信息处理完成", MsgBoxStyle.Information, "提示")
        Else
            MsgBox("取消处理完成", MsgBoxStyle.Information, "提示")
        End If
        Me.Close()
    End Sub

End Class