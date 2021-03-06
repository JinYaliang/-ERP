Imports LFERP.Library.Material
Imports LFERP.Library.Purchase.WareQuality

Public Class frmWareQualityAdd
    Dim strDPTID As String      '記錄部門編號

    '單擊反饋部門文本框上的按鈕時，顯示部門選擇模塊
    Private Sub bteWQ_Dpt_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles bteWQ_Dpt.ButtonClick
        frmDepartmentSelect.DptID = ""
        frmDepartmentSelect.DptName = ""
        frmDepartmentSelect.StartPosition = FormStartPosition.Manual        '把模塊顯示模式設置為手動模式
        '設置模塊的顯示位置
        frmDepartmentSelect.Left = MDIMain.tvModule.Width + bteWQ_Dpt.Left + 18 + MDIMain.Left
        frmDepartmentSelect.Top = bteWQ_Dpt.Top + GroupBox2.Top + bteWQ_Dpt.Height + 82 + MDIMain.Top

        frmDepartmentSelect.ShowDialog()        '顯示部門選擇模塊

        If frmDepartmentSelect.DptID <> "" Then     '判斷是否已選擇部門
            strDPTID = frmDepartmentSelect.DptID        '保存部門ID
            bteWQ_Dpt.Text = frmDepartmentSelect.UpDptName & "-" & frmDepartmentSelect.DptName      '顯示部門名稱
        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    '單擊物料編號文本框時，顯示物料選擇模塊
    Private Sub bteM_Code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles bteM_Code.ButtonClick
        tempCode = ""       '清空全局變量，用於保本物料編號
        frmBOMSelect.ShowDialog()       '顯示物料選擇模塊
        If tempCode <> "" Then      '判斷是否已選擇物料
            bteM_Code.EditValue = tempCode      '顯示物料編號
            Dim mc As New MaterialController
            Dim mi As New MaterialInfo
            mi = mc.MaterialCode_Get(tempCode)      '根據物料編號，返回相關物料信息
            txtM_Name.EditValue = mi.M_Name     '顯示物料名稱
            txtM_Gauge.EditValue = mi.M_Gauge       '顯示物料規格
            lblM_Unit.Text = mi.M_Unit      '顯示物料單位
        End If
    End Sub

    '單擊保存按鈕，保存數據
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Trim(txtWQ_Code.Text) = "" Then
            MsgBox("反饋單編號不能為空，請輸入反饋編單號!", 64, "提示")
            txtWQ_Code.Focus()
            Exit Sub
        ElseIf Trim(bteM_Code.EditValue) = "" Then
            MsgBox("物料編號不能為空，請輸入物料單號!", 64, "提示")
            bteM_Code.Focus()
            Exit Sub
        ElseIf Trim(bteWQ_Dpt.EditValue) = "" Then
            MsgBox("反饋部門不能為空，請輸入反饋部門!", 64, "提示")
            bteWQ_Dpt.Focus()
            Exit Sub
            'ElseIf Trim(txtWO_ID.Text) = "" Then
            '    MsgBox("物料發出單號不能為空，請輸入物料發出單號!", 64, "提示")
            '    txtWO_ID.Focus()
            '    Exit Sub
        ElseIf Trim(txtWO_Qty.Text) = "" Then
            MsgBox("物料發出數量不能為空，請輸入物料發出數量!", 64, "提示")
            txtWO_Qty.Focus()
            Exit Sub
            'ElseIf Trim(txtWQ_Description.Text) = "" Then
            '    MsgBox("品質問題描述不能為空，請輸入品質問題描述!", 64, "提示")
            '    txtWQ_Description.Focus()
            '    Exit Sub
            'ElseIf Trim(txtPS_Opinion.Text) = "" Then
            '    MsgBox("採購部意見不能為空，請輸入採購部意見!", 64, "提示")
            '    txtPS_Opinion.Focus()
            '    Exit Sub
            'ElseIf Trim(txtACC_Opinion.Text) = "" Then
            '    MsgBox("審計部意見不能為空，請輸入審計部意見!", 64, "提示")
            '    txtACC_Opinion.Focus()
            '    Exit Sub
        ElseIf IsNumeric(Trim(txtWO_Qty.Text)) = False Then
            MsgBox("物料發出數量輸入有誤，請重新輸入物料發出數量!", 64, "提示")
            txtWO_Qty.Focus()
            txtWO_Qty.SelectAll()
            Exit Sub
        ElseIf CSng(Trim(txtWO_Qty.Text)) < 0 Then
            MsgBox("物料發出數量不能小於0，請重新輸入物料發出數量!", 64, "提示")
            txtWO_Qty.Focus()
            txtWO_Qty.SelectAll()
            Exit Sub
        End If

        Dim wqc As New WareQualityController
        If lblTitle.Text = "物料品質反饋單-添加" Then
            Dim wqi As List(Of WareQualityInfo)
            wqi = wqc.WareQuality_GetList(Trim(txtWQ_Code.Text), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            '判斷反饋單編號是否已存在
            If wqi.Count > 0 Then
                MsgBox("反饋單編號已存在，需重新生成反饋單編號，" & vbCr & "請確定重新生成反饋單編號!", 64, "提示")
                txtWQ_Code.Text = GetManual_NO()        '存在，則調用GetManual_NO()過程，重新生成反饋單編號
                MsgBox("反饋單編號已重新生成，請保存!", 64, "提示")
            Else
                '調用添加物料品質反饋單函數
                If AddWareQuality() = True Then
                    MsgBox("數據添加成功!", 64, "提示")
                    '調用frmWareQualityMain.popWQRef_Click()過程，刷新frmWareQualityMain.Grid，以顯示剛添加的記錄
                    frmWareQualityMain.popWQRef_Click(Nothing, Nothing)
                    Me.Close()
                Else
                    MsgBox("數據添加失敗，請查找原因!", 64, "提示")
                End If
            End If

        ElseIf lblTitle.Text = "物料品質反饋單-修改" Then
            '調用修改物料品質反饋單函數
            If EidtWareQuality() = True Then
                MsgBox("數據修改成功!", 64, "提示")
                '調用frmWareQualityMain.popWQRef_Click()過程，刷新frmWareQualityMain.Grid，以顯示剛修改的記錄
                frmWareQualityMain.popWQRef_Click(Nothing, Nothing)
                Me.Close()
            Else
                MsgBox("數據修改失敗，請查找原因!", 64, "提示")
            End If
        End If
    End Sub

    '添加物料品質反饋單函數
    '此函數被以下過程調用：
    'cmdSave_Click()
    Function AddWareQuality() As Boolean
        Dim wqi As New WareQualityInfo
        Dim wqc As New WareQualityController

        wqi.WQ_Code = Trim(txtWQ_Code.Text)
        wqi.M_Code = Trim(bteM_Code.EditValue)
        wqi.M_Name = Trim(txtM_Name.Text)
        wqi.M_Gauge = Trim(txtM_Gauge.Text)
        wqi.WQ_Dpt = strDPTID
        wqi.WO_ID = Trim(txtWO_ID.Text)
        wqi.WO_Qty = Trim(txtWO_Qty.Text)
        wqi.M_Unit = lblM_unit.text
        wqi.WQ_UserName = Trim(txtWQ_UserName.Text)
        wqi.AddDate = Format(Now, "yyyy/MM/dd")
        wqi.WQ_Description = Trim(txtWQ_Description.Text)
        wqi.PS_Opinion = Trim(txtPS_Opinion.Text)
        wqi.PS_UserName = Trim(txtPS_UserName.Text)
        wqi.ACC_Opinion = Trim(txtACC_Opinion.Text)
        wqi.AddUser = UserName
        wqi.CostAcc_Opinion = txtCostAcc_Opinion.Text.Trim
        wqi.PD_Opinion = txtPD_Opinion.Text.Trim

        If wqc.WareQuality_Add(wqi) = True Then
            AddWareQuality = True       '添加成功，返回True
        Else
            AddWareQuality = False      '添加失敗，返回False
        End If
    End Function

    '修改物料品質反饋單函數
    '此函數被以下過程調用：
    'cmdSave_Click()
    Function EidtWareQuality() As Boolean
        Dim wqi As New WareQualityInfo
        Dim wqc As New WareQualityController

        wqi.WQ_Code = txtWQ_Code.Text
        wqi.M_Code = Trim(bteM_Code.EditValue)
        wqi.M_Name = Trim(txtM_Name.Text)
        wqi.M_Gauge = Trim(txtM_Gauge.Text)
        wqi.WQ_Dpt = strDPTID
        wqi.WO_ID = Trim(txtWO_ID.Text)
        wqi.WO_Qty = Trim(txtWO_Qty.Text)
        wqi.M_Unit = lblM_Unit.Text
        wqi.WQ_UserName = Trim(txtWQ_UserName.Text)
        wqi.WQ_Description = Trim(txtWQ_Description.Text)
        wqi.PS_Opinion = Trim(txtPS_Opinion.Text)
        wqi.PS_UserName = Trim(txtPS_UserName.Text)
        wqi.ACC_Opinion = Trim(txtACC_Opinion.Text)
        wqi.ModifyUser = UserName
        wqi.ModifyDate = Format(Now, "yyyy/MM/dd")
        wqi.CostAcc_Opinion = txtCostAcc_Opinion.Text.Trim
        wqi.PD_Opinion = txtPD_Opinion.Text.Trim

        If wqc.WareQuality_Update(wqi) = True Then
            EidtWareQuality = True      '修改成功，返回True
        Else
            EidtWareQuality = False     '修改失敗，返回False
        End If

    End Function

    '加載物料品質反饋單信息過程
    '此過程被以下過程調用：
    'frmWareQualityAdd_Load()
    Sub LoadData()
        Dim wqi As List(Of WareQualityInfo)
        Dim wqc As New WareQualityController
        '根據反饋單編號，返回反饋單信息
        wqi = wqc.WareQuality_GetList(txtWQ_Code.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If wqi.Count > 0 Then       '判斷是否存在返回信息
            bteM_Code.EditValue = wqi(0).M_Code
            strDPTID = wqi(0).WQ_Dpt
            txtM_Name.Text = wqi(0).M_Name
            txtM_Gauge.Text = wqi(0).M_Gauge
            bteWQ_Dpt.EditValue = wqi(0).AP_ApplyDptName
            txtWO_ID.Text = wqi(0).WO_ID
            txtWO_Qty.Text = wqi(0).WO_Qty
            lblM_Unit.Text = wqi(0).M_Unit
            txtWQ_UserName.Text = wqi(0).WQ_UserName
            dteAddDate.EditValue = wqi(0).AddDate
            txtWQ_Description.Text = wqi(0).WQ_Description
            txtPS_Opinion.Text = wqi(0).PS_Opinion
            txtPS_UserName.Text = wqi(0).PS_UserName
            txtACC_Opinion.Text = wqi(0).ACC_Opinion
            txtCostAcc_Opinion.Text = wqi(0).CostAcc_Opinion
            txtPD_Opinion.Text = wqi(0).PD_Opinion

        End If
    End Sub

    '自動生成反饋單編號過程
    '此過程被以下過程調用：
    'frmWareQualityAdd_Load
    Function GetManual_NO() As String
        Dim wqi As List(Of WareQualityInfo)
        Dim wqc As New WareQualityController
        Dim ndate As String
        '反饋單號共10位，以FH+年月組成反饋單編號的前6位，后4位為流水號
        ndate = "FH" + Format(Now, "yyMM")
        '返回與當前年月一致的反饋單編號，返回記錄第一條，總是最新添加記錄
        wqi = wqc.WareQuality_GetList(ndate, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If wqi.Count <= 0 Then      '判斷是否存在返回信息
            'If wqi Is Nothing Then
            GetManual_NO = ndate + "0001"       '不存在則眾1開始編號
        Else
            GetManual_NO = ndate + Mid((CInt(Mid(wqi(0).WQ_Code, 7)) + 10001), 2)       '存在則在最後一個編號上加1
        End If
    End Function

    Private Sub frmWareQualityAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dteAddDate.EditValue = Format(Now, "yyyy/MM/dd")

        If lblTitle.Text = "物料品質反饋單-添加" Then
            txtWQ_Code.Text = GetManual_NO()        '調用自動生成反饋單編號過程，并顯示於文本框中
            'txtPS_UserName.Text = UserName
        ElseIf lblTitle.Text = "物料品質反饋單-修改" Then
            LoadData()
            dteAddDate.Enabled = False      '添加日期不能修改
        ElseIf lblTitle.Text = "物料品質反饋單-查看" Then
            LoadData()
            cmdSave.Enabled = False     '保存按鈕不能使用
        End If

    End Sub
End Class