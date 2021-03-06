Imports LFERP.SystemManager.SystemUser
Imports LFERP.Library.NmetalSampleManager.NmetalSampleSetting
Imports LFERP.Library.PieceProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleTransaction
Imports LFERP.SystemManager

Public Class frmNmetalSampleSetting
    Dim pncon As New PersonnelControl
    Dim stcon As New NmetalSampleTransactionControler
    Private Sub SetGridLookUpDataSource(ByVal glu As DevExpress.XtraEditors.GridLookUpEdit, ByVal ds As List(Of SystemUserInfo))
        glu.Properties.DataSource = ds
        glu.Properties.DisplayMember = "U_Name"
        glu.Properties.ValueMember = "U_ID"
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ssi As New NmetalSampleSettingInfo
        Dim ssc As New NmetalSampleSettingController
        Dim ssili As New List(Of NmetalSampleSettingInfo)
        ssi.U_ID = InUserID
        '----------保存用戶信息-----------------------------------
        ssi.SampleOrdersCreateUserID = gluSampleOrdersUserID.EditValue
        ssi.SampleProcessCreateUserID = gluSampleProcessUserID.EditValue
        ssi.SamplePaceCreateUserID = gluSamplePaceUserID.EditValue
        ssi.SampleSendCreateUserID = gluSampleSendUserID.EditValue
        ssi.SampleCustomerFeedbackCreateUserID = gluSampleCustomerFeedbackUserID.EditValue
        ssi.SampleCollectionCreateUserID = gluSampleCollectionUserID.EditValue
        ssi.SampleCollectionD_ID = gluD_ID.EditValue
        ssi.SampleCollectionStatusType = gluStatusType.EditValue

        ssi.SampleTransactionCreateUserID = gluSampleTransactionUserID.EditValue
        ssi.SampleWareInventoryCreateUserID = gluSampleWareInventoryUserID.EditValue
        ssi.SamplePackingCreateUserID = gluSamplePackingUserID.EditValue
        ssi.SamplePlanCreateUserID = gluSamplePlanUserID.EditValue
        ssi.SampleDivertCreateUserID = gluSampleDivertUserID.EditValue

        '----------保存日期信息-----------------------------------
        ssi.SampleOrdersBeginDate = dteSampleOrdersBeginDate.EditValue
        ssi.SampleProcessBeginDate = dteSampleProcessBeginDate.EditValue
        ssi.SamplePaceBeginDate = dteSamplePaceBeginDate.EditValue
        ssi.SampleSendBeginDate = dteSampleSendBeginDate.EditValue
        ssi.SampleCustomerFeedbackBeginDate = dteSampleCustomerFeedbackBeginDate.EditValue
        ssi.SampleCollectionBeginDate = dteSampleCollectionBeginDate.EditValue
        ssi.SampleTransactionBeginDate = dteSampleTransactionBeginDate.EditValue
        ssi.SampleWareInventoryBeginDate = dteSampleWareInventoryBeginDate.EditValue
        ssi.SamplePackingBeginDate = dteSamplePackingBeginDate.EditValue
        ssi.SamplePlanBeginDate = dteSamplePlanBeginDate.EditValue
        ssi.SampleDivertBeginDate = dteSampleDivertBeginDate.EditValue

        '----------保存審核信息-----------------------------------
        ssi.SampleOrdersCheck = GetComboBoxEditValue(cboSampleOrdersCheck)
        ssi.SampleProcessCheck = GetComboBoxEditValue(cboSampleProcessCheck)
        ssi.SamplePaceCheck = GetComboBoxEditValue(cboSamplePaceCheck)
        ssi.SampleSendCheck = GetComboBoxEditValue(cboSampleSendCheck)
        ssi.SampleCustomerFeedbackCheck = GetComboBoxEditValue(cboSampleCustomerFeedbackCheck)
        ssi.SampleCollectionCheck = GetComboBoxEditValue(cboSampleCollectionCheck)
        ssi.SampleTransactionCheck = GetComboBoxEditValue(cboSampleTransactionCheck)
        ssi.SampleWareInventoryCheck = GetComboBoxEditValue(cboSampleWareInventoryCheck)
        ssi.SamplePackingCheck = GetComboBoxEditValue(cboSamplePackingCheck)
        ssi.SamplePlanCheck = GetComboBoxEditValue(cboSamplePlanCheck)
        ssi.SampleDivertCheck = GetComboBoxEditValue(cboSampleDivertCheck)
        '------------------------------------------------------------------
        ssili = ssc.NmetalSampleSetting_GetList(InUserID)
        If ssili.Count > 0 Then
            If ssc.NmetalSampleSetting_Update(ssi) Then
                MsgBox("樣板參數設置修改成功", MsgBoxStyle.Information, "提示")
                Me.Close()
            Else
                MsgBox("樣板參數設置修改失敗", MsgBoxStyle.Information, "提示")
                Me.Close()
            End If
        Else
            If ssc.NmetalSampleSetting_Add(ssi) Then
                MsgBox("樣板參數設置添加成功", MsgBoxStyle.Information, "提示")
                Me.Close()
            Else
                MsgBox("樣板參數設置添加失敗", MsgBoxStyle.Information, "提示")
                Me.Close()
            End If
        End If
    End Sub
    Private Sub SetComboBoxEdit(ByVal cbo As DevExpress.XtraEditors.ComboBoxEdit, ByVal value As String)
        If value.Contains("0") And value.Contains("1") Then
            cbo.SelectedIndex = 0
        ElseIf value.Contains("1") Then
            cbo.SelectedIndex = 1
        ElseIf value.Contains("0") Then
            cbo.SelectedIndex = 2
        ElseIf value.Contains("2") Then
            cbo.SelectedIndex = 3
        End If
    End Sub
    Private Function GetComboBoxEditValue(ByVal cbo As DevExpress.XtraEditors.ComboBoxEdit) As String
        If cbo.SelectedIndex = 0 Then
            Return "0,1"
        ElseIf cbo.SelectedIndex = 1 Then
            Return "1"
        ElseIf cbo.SelectedIndex = 2 Then
            Return "0"
        ElseIf cbo.SelectedIndex = 3 Then
            Return "2"
        Else
            Return "false"
        End If
    End Function

    Private Sub SampleSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '1.權限設置
        Dim pccon As New PermissionController
        Dim pcinfo As New PermissionInfo
        pcinfo = pccon.PermissionModuleUser_Get("8901", InUserID)
        If pcinfo.PMU_Value = False Then '订單
            XtraTabPage1.PageVisible = False
        End If
        pcinfo = pccon.PermissionModuleUser_Get("8902", InUserID)
        If pcinfo.PMU_Value = False Then '工藝
            XtraTabPage2.PageVisible = False
        End If
        pcinfo = pccon.PermissionModuleUser_Get("8903", InUserID)
        If pcinfo.PMU_Value = False Then '排程
            XtraTabPage3.PageVisible = False
        End If
        pcinfo = pccon.PermissionModuleUser_Get("8904", InUserID)
        If pcinfo.PMU_Value = False Then '收发
            XtraTabPage4.PageVisible = False
        End If
        pcinfo = pccon.PermissionModuleUser_Get("8905", InUserID)
        If pcinfo.PMU_Value = False Then '寄送
            XtraTabPage5.PageVisible = False
        End If
        pcinfo = pccon.PermissionModuleUser_Get("8906", InUserID)
        If pcinfo.PMU_Value = False Then '客戶反饋
            XtraTabPage6.PageVisible = False
        End If
        pcinfo = pccon.PermissionModuleUser_Get("8907", InUserID)
        If pcinfo.PMU_Value = False Then '條碼一览
            XtraTabPage7.PageVisible = False
        End If
        pcinfo = pccon.PermissionModuleUser_Get("8908", InUserID)
        If pcinfo.PMU_Value = False Then '成品狀態
            XtraTabPage8.PageVisible = False
        End If
        pcinfo = pccon.PermissionModuleUser_Get("8909", InUserID)
        If pcinfo.PMU_Value = False Then '盤點
            XtraTabPage9.PageVisible = False
        End If
        pcinfo = pccon.PermissionModuleUser_Get("8910", InUserID)
        If pcinfo.PMU_Value = False Then '裝箱一览
            XtraTabPage10.PageVisible = False
        End If
        pcinfo = pccon.PermissionModuleUser_Get("8914", InUserID)
        If pcinfo.PMU_Value = False Then '条码转移
            XtraTabPage11.PageVisible = False
        End If


        Dim pmlist As New List(Of PersonnelInfo)
        Dim pminfo As New PersonnelInfo
        pminfo.DepID = "All"
        pminfo.DepName = "全部"
        pmlist = pncon.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)
        gluD_ID.Properties.DisplayMember = "DepName"
        gluD_ID.Properties.ValueMember = "DepID"
        gluD_ID.Properties.DataSource = pmlist
        pmlist.Insert(0, pminfo)
        '---------------------------------------------------------------------
        Dim stlist As New List(Of NmetalSampleTransactionInfo)
        Dim stinfo As New NmetalSampleTransactionInfo
        stinfo.StatusType = "All"
        stinfo.StatusTypeName = "全部"
        stlist = stcon.NmetalSampleTransactionType_GetList(Nothing, Nothing)
        gluStatusType.Properties.DisplayMember = "StatusTypeName"
        gluStatusType.Properties.ValueMember = "StatusType"
        gluStatusType.Properties.DataSource = stlist
        stlist.Insert(0, stinfo)

        Dim msuli As New List(Of SystemUserInfo)
        Dim msui As New SystemUserInfo
        '----------填入所有用戶-----------------------------------
        msui.U_ID = "All"
        msui.U_Name = "全部"
        Dim msuc As New SystemUserController
        msuli = msuc.SystemUser_GetList(Nothing, Nothing, Nothing)
        msuli.Insert(0, msui)

        SetGridLookUpDataSource(gluSampleOrdersUserID, msuli)
        SetGridLookUpDataSource(gluSampleProcessUserID, msuli)
        SetGridLookUpDataSource(gluSamplePaceUserID, msuli)
        SetGridLookUpDataSource(gluSampleSendUserID, msuli)
        SetGridLookUpDataSource(gluSampleCustomerFeedbackUserID, msuli)
        SetGridLookUpDataSource(gluSampleCollectionUserID, msuli)
        SetGridLookUpDataSource(gluSampleTransactionUserID, msuli)
        SetGridLookUpDataSource(gluSampleWareInventoryUserID, msuli)
        SetGridLookUpDataSource(gluSamplePackingUserID, msuli)
        SetGridLookUpDataSource(gluSamplePlanUserID, msuli)
        SetGridLookUpDataSource(gluSampleDivertUserID, msuli)
        '---------------------------------------------------------------------------
        Dim ssli As New List(Of NmetalSampleSettingInfo)
        Dim ssc As New NmetalSampleSettingController
        ssli = ssc.NmetalSampleSetting_GetList(InUserID)
        If ssli.Count > 0 Then
            '-------------填入初始用戶信息---------------------------------------------
            gluSampleOrdersUserID.EditValue = ssli(0).SampleOrdersCreateUserID
            gluSampleProcessUserID.EditValue = ssli(0).SampleProcessCreateUserID
            gluSamplePaceUserID.EditValue = ssli(0).SamplePaceCreateUserID
            gluSampleSendUserID.EditValue = ssli(0).SampleSendCreateUserID
            gluSampleCustomerFeedbackUserID.EditValue = ssli(0).SampleCustomerFeedbackCreateUserID
            gluSampleCollectionUserID.EditValue = ssli(0).SampleCollectionCreateUserID
            gluSampleTransactionUserID.EditValue = ssli(0).SampleTransactionCreateUserID
            gluSampleWareInventoryUserID.EditValue = ssli(0).SampleWareInventoryCreateUserID
            gluSamplePackingUserID.EditValue = ssli(0).SamplePackingCreateUserID
            gluSamplePlanUserID.EditValue = ssli(0).SamplePlanCreateUserID
            gluSampleDivertUserID.EditValue = ssli(0).SampleDivertCreateUserID

            gluD_ID.EditValue = ssli(0).SampleCollectionD_ID
            gluStatusType.EditValue = ssli(0).SampleCollectionStatusType

            '-------------填入初始日期信息---------------------------------------------
            dteSampleOrdersBeginDate.EditValue = Format(ssli(0).SampleOrdersBeginDate, "yyyy/MM/dd")
            dteSampleProcessBeginDate.EditValue = Format(ssli(0).SampleProcessBeginDate, "yyyy/MM/dd")
            dteSampleSendBeginDate.EditValue = Format(ssli(0).SampleSendBeginDate, "yyyy/MM/dd")
            dteSamplePaceBeginDate.EditValue = Format(ssli(0).SamplePaceBeginDate, "yyyy/MM/dd")
            dteSampleCustomerFeedbackBeginDate.EditValue = Format(ssli(0).SampleCustomerFeedbackBeginDate, "yyyy/MM/dd")
            dteSampleCollectionBeginDate.EditValue = Format(ssli(0).SampleCollectionBeginDate, "yyyy/MM/dd")
            dteSampleTransactionBeginDate.EditValue = Format(ssli(0).SampleTransactionBeginDate, "yyyy/MM/dd")
            dteSampleWareInventoryBeginDate.EditValue = Format(ssli(0).SampleWareInventoryBeginDate, "yyyy/MM/dd")
            dteSamplePackingBeginDate.EditValue = Format(ssli(0).SamplePackingBeginDate, "yyyy/MM/dd")
            dteSamplePlanBeginDate.EditValue = Format(ssli(0).SamplePlanBeginDate, "yyyy/MM/dd")
            dteSampleDivertBeginDate.EditValue = Format(ssli(0).SampleDivertBeginDate, "yyyy/MM/dd")
            '-------------填入初始審核信息--------------------------------------------------------
            SetComboBoxEdit(cboSampleOrdersCheck, ssli(0).SampleOrdersCheck)
            SetComboBoxEdit(cboSampleProcessCheck, ssli(0).SampleProcessCheck)
            SetComboBoxEdit(cboSamplePaceCheck, ssli(0).SamplePaceCheck)
            SetComboBoxEdit(cboSampleSendCheck, ssli(0).SampleSendCheck)
            SetComboBoxEdit(cboSampleCustomerFeedbackCheck, ssli(0).SampleCustomerFeedbackCheck)
            SetComboBoxEdit(cboSampleCollectionCheck, ssli(0).SampleCollectionCheck)
            SetComboBoxEdit(cboSampleTransactionCheck, ssli(0).SampleTransactionCheck)
            SetComboBoxEdit(cboSampleWareInventoryCheck, ssli(0).SampleWareInventoryCheck)
            SetComboBoxEdit(cboSamplePackingCheck, ssli(0).SamplePackingCheck)
            SetComboBoxEdit(cboSamplePlanCheck, ssli(0).SamplePlanCheck)
            SetComboBoxEdit(cboSampleDivertCheck, ssli(0).SampleDivertCheck)
        Else
            btnReset_Click(Nothing, Nothing)
        End If

    End Sub
    ''' <summary>
    ''' 重置參數
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Dim beginDate As Date = Format(CDate("2013/01/01"), "yyyy/MM/dd")
        '------------重置創建用戶----------------------------------------------
        gluSampleOrdersUserID.EditValue = "All"
        gluSampleProcessUserID.EditValue = "All"
        gluSamplePaceUserID.EditValue = "All"
        gluSampleSendUserID.EditValue = "All"
        gluSampleCustomerFeedbackUserID.EditValue = "All"
        gluSampleCollectionUserID.EditValue = "All"
        gluSampleTransactionUserID.EditValue = "All"
        gluSampleWareInventoryUserID.EditValue = "All"
        gluSamplePackingUserID.EditValue = "All"
        gluSamplePlanUserID.EditValue = "All"
        gluSampleDivertUserID.EditValue = "All"
        gluStatusType.EditValue = "All"
        gluD_ID.EditValue = "All"
        '-------------重置創建日期--------------------------------------------
        dteSampleOrdersBeginDate.EditValue = beginDate
        dteSampleProcessBeginDate.EditValue = beginDate
        dteSamplePaceBeginDate.EditValue = beginDate
        dteSampleSendBeginDate.EditValue = beginDate
        dteSampleCustomerFeedbackBeginDate.EditValue = beginDate
        dteSampleCollectionBeginDate.EditValue = beginDate
        dteSampleTransactionBeginDate.EditValue = beginDate
        dteSampleWareInventoryBeginDate.EditValue = beginDate
        dteSamplePackingBeginDate.EditValue = beginDate
        dteSamplePlanBeginDate.EditValue = beginDate
        dteSampleDivertBeginDate.EditValue = beginDate
        '--------------重置審核狀態-------------------------------------------
        cboSampleOrdersCheck.SelectedIndex = 0
        cboSampleProcessCheck.SelectedIndex = 0
        cboSamplePaceCheck.SelectedIndex = 0
        cboSampleSendCheck.SelectedIndex = 0
        cboSampleCustomerFeedbackCheck.SelectedIndex = 0
        cboSampleCollectionCheck.SelectedIndex = 0
        cboSampleTransactionCheck.SelectedIndex = 0
        cboSampleWareInventoryCheck.SelectedIndex = 0
        cboSamplePackingCheck.SelectedIndex = 0
        cboSamplePlanCheck.SelectedIndex = 0
        cboSampleDivertCheck.SelectedIndex = 0
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class