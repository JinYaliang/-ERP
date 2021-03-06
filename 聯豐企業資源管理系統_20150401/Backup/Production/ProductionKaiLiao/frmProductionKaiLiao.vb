Imports LFERP.Library.ProductionKaiLiao
Imports LFERP.Library.ProductionSchedule
Imports LFERP.DataSetting
Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.WareHouse
Imports LFERP.Library.ProductionWareInventory

Public Class frmProductionKaiLiao

    Dim ds As New DataSet
    Dim dc As New DepartmentControler

    Dim pc As New LFERP.Library.PieceProcess.PersonnelControl
    Dim strWHID As String

    Dim pfc As New LFERP.Library.ProductionController.ProductionFieldControl
    Dim pic As New ProductInventoryController

    Sub LoadProductNo()
        Dim mc As New ProcessMainControl
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = mc.ProcessMain_GetList3(Nothing, Nothing)
    End Sub

    Private Sub frmProductionKaiLiao_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i%

        Label8.Text = tempValue2
        Label9.Text = tempValue3
        tempValue2 = ""
        tempValue3 = ""


        txtCNO.Enabled = False

        LoadProductNo()
        GluDep.Properties.DataSource = pc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)
        GluDep.Properties.DisplayMember = "DepName"
        GluDep.Properties.ValueMember = "DepID"

        CreateTable()

        Select Case Label8.Text
            Case "開料單"
                If Edit = False Then
                    Me.Text = "開料單--新增"

                    Dim ui As List(Of UserPowerInfo)
                    Dim uc As New UserPowerControl
                    ui = uc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

                    If ui.Count = 0 Then
                        Exit Sub
                    Else
                        GluDep.EditValue = ui(0).DepID
                    End If
                    CBType.EditValue = "生產加工"
                Else
                    LoadData(Label9.Text)
                    Me.Text = "開料單--修改" & Label9.Text
                End If
                XtraTabControl1.SelectedTabPage = XtraTabPage1
            Case "PreView"
                Me.Text = "開料單--查看" & Label9.Text
                LoadData(Label9.Text)
                cmdSave.Visible = False
                XtraTabControl1.SelectedTabPage = XtraTabPage1
            Case "Check"
                Me.Text = "開料單--審核" & Label9.Text
                LoadData(Label9.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                '@ 2012/11/21 添加-------------
                For i = 0 To XtraTabPage1.Controls.Count - 2
                    XtraTabPage1.Controls(i).Enabled = False
                Next
                GridView1.OptionsBehavior.Editable = False
                Grid.ContextMenuStrip.Enabled = False
                '-----------------------------------------
        End Select
    End Sub

    Sub CreateTable()
        ds.Tables.Clear()

        With ds.Tables.Add("KaiLiao")

            .Columns.Add("IndexNo", GetType(String))
            .Columns.Add("C_NO", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("C_Qty", GetType(Integer))
            .Columns.Add("C_Weight", GetType(Double))  '重量
            .Columns.Add("C_Cishu", GetType(Integer))
            .Columns.Add("C_Type", GetType(String))

        End With
        Grid.DataSource = ds.Tables("KaiLiao")

        With ds.Tables.Add("DelKaiLiao")
            .Columns.Add("IndexNO", GetType(String))
        End With

        With ds.Tables.Add("ProductType")
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluType.Properties.DisplayMember = "PM_Type"
        gluType.Properties.ValueMember = "PM_Type"
        gluType.Properties.DataSource = ds.Tables("ProductType")
      
    End Sub

    Public Function LoadData(ByVal C_NO As String) As Boolean
        LoadData = True

        ds.Tables("KaiLiao").Clear()

        Dim pi As List(Of ProductionKaiLiaoInfo)
        Dim pc As New ProductionKaiLiaoControl

        pi = pc.ProductionKaiLiao_GetList(C_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Try
            If pi.Count = 0 Then
                MsgBox("沒有數據")
                LoadData = False
                Exit Function
            Else

                txtCNO.Text = pi(0).C_NO
                txtWH.EditValue = pi(0).WH_PName & "-" & pi(0).WH_Name  '顯示名稱
                strWHID = pi(0).WH_ID  '實際倉庫編號
                CBType.EditValue = pi(0).Pro_Type
                PM_M_Code.EditValue = pi(0).PM_M_Code
                gluType.EditValue = pi(0).PM_Type

                GluDep.EditValue = pi(0).PS_Dep

                txtAction.Text = pi(0).PS_Action
                txtRemark.Text = pi(0).C_Remark

                If pi(0).C_Check = False Then
                    CheckEdit1.Checked = False
                Else
                    CheckEdit1.Checked = True
                End If
                txtCheckAction.Text = pi(0).C_CheckAction
                txtCheckRemark.Text = pi(0).C_CheckRemark

                Dim i As Integer
                Dim row As DataRow
                For i = 0 To pi.Count - 1

                    row = ds.Tables("KaiLiao").NewRow

                    row("IndexNo") = pi(i).IndexNo
                    row("M_Code") = pi(i).M_Code
                    row("M_Name") = pi(i).M_Name
                    row("M_Gauge") = pi(i).M_Gauge
                    row("M_Unit") = pi(i).M_Unit
                    row("C_Qty") = pi(i).C_Qty
                    row("C_Weight") = pi(i).C_Weight
                    row("C_Cishu") = pi(i).C_Cishu
                    row("C_Type") = pi(i).C_Type

                    ds.Tables("KaiLiao").Rows.Add(row)
                Next

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Public Function GetNO() As String
        Dim pi As New ProductionKaiLiaoInfo
        Dim pc As New ProductionKaiLiaoControl
        Dim strA As String
        strA = Format(Now, "yyMM")
        pi = pc.ProductionKaiLiao_GetNO(strA)

        If pi Is Nothing Then
            GetNO = "C" & strA & "001"
        Else
            GetNO = "C" + strA + Mid((CInt(Mid(pi.C_NO, 6)) + 1001), 2)
        End If
    End Function

    Sub DataNew()
        Dim pi As New ProductionKaiLiaoInfo
        Dim pc As New ProductionKaiLiaoControl

        txtCNO.Text = GetNO()
        pi.C_NO = txtCNO.Text
        pi.WH_ID = strWHID
        pi.Pro_Type = CBType.EditValue
        pi.PM_M_Code = PM_M_Code.Text
        pi.PM_Type = gluType.EditValue
        pi.PS_Dep = GluDep.EditValue
        pi.PS_Action = txtAction.Text
        pi.C_Remark = txtRemark.Text
        pi.C_Action = InUserID
        pi.C_AddDate = Format(Now, "yyyy/MM/dd")

        Dim i As Integer

        For i = 0 To ds.Tables("KaiLiao").Rows.Count - 1

            pi.M_Code = ds.Tables("KaiLiao").Rows(i)("M_Code")

            If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Qty")) Then
                pi.C_Qty = 0
            Else
                pi.C_Qty = ds.Tables("KaiLiao").Rows(i)("C_Qty")
            End If
            If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Weight")) Then
                pi.C_Weight = 0
            Else
                pi.C_Weight = ds.Tables("KaiLiao").Rows(i)("C_Weight")
            End If
            If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Cishu")) Then
                pi.C_Cishu = 0
            Else
                pi.C_Cishu = ds.Tables("KaiLiao").Rows(i)("C_Cishu")
            End If
            If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Type")) Then
                pi.C_Type = Nothing
            Else
                pi.C_Type = ds.Tables("KaiLiao").Rows(i)("C_Type")
            End If
            pc.ProductionKaiLiao_Add(pi)
        Next

        MsgBox("已保存,單號: " & txtCNO.Text & " ")
        Me.Close()

    End Sub

    Sub DataEdit()

        Dim i As Integer

        '更新刪除的記錄
        If ds.Tables("DelKaiLiao").Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ds.Tables("DelKaiLiao").Rows.Count - 1

                Dim odc As New ProductionKaiLiaoControl

                If Not IsDBNull(ds.Tables("DelKaiLiao").Rows(j)("IndexNo")) Then
                    odc.ProductionKaiLiao_Delete(Nothing, ds.Tables("DelKaiLiao").Rows(j)("IndexNo"))
                End If
            Next j
        End If

        For i = 0 To ds.Tables("KaiLiao").Rows.Count - 1

            If Not IsDBNull(ds.Tables("KaiLiao").Rows(i)("IndexNo")) Then

                Dim pi As New ProductionKaiLiaoInfo
                Dim pc As New ProductionKaiLiaoControl

                pi.C_NO = txtCNO.Text
                pi.WH_ID = strWHID
                pi.Pro_Type = CBType.EditValue
                pi.PM_M_Code = PM_M_Code.Text
                pi.PM_Type = gluType.EditValue
                pi.PS_Dep = GluDep.EditValue
                pi.PS_Action = txtAction.Text
                pi.C_Remark = txtRemark.Text
                pi.C_Action = InUserID
                pi.C_AddDate = Format(Now, "yyyy/MM/dd")


                pi.M_Code = ds.Tables("KaiLiao").Rows(i)("M_Code")


                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Qty")) Then
                    pi.C_Qty = 0
                Else
                    pi.C_Qty = ds.Tables("KaiLiao").Rows(i)("C_Qty")
                End If
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Weight")) Then
                    pi.C_Weight = 0
                Else
                    pi.C_Weight = ds.Tables("KaiLiao").Rows(i)("C_Weight")
                End If
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Cishu")) Then
                    pi.C_Cishu = 0
                Else
                    pi.C_Cishu = ds.Tables("KaiLiao").Rows(i)("C_Cishu")
                End If
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Type")) Then
                    pi.C_Type = Nothing
                Else
                    pi.C_Type = ds.Tables("KaiLiao").Rows(i)("C_Type")
                End If
                pc.ProductionKaiLiao_Update(pi)

            ElseIf IsDBNull(ds.Tables("KaiLiao").Rows(i)("IndexNo")) Then

                Dim pi As New ProductionKaiLiaoInfo
                Dim pc As New ProductionKaiLiaoControl

                pi.C_NO = txtCNO.Text
                pi.Pro_Type = CBType.EditValue
                pi.PM_M_Code = PM_M_Code.Text
                pi.PM_Type = gluType.EditValue
                pi.PS_Dep = GluDep.EditValue
                pi.PS_Action = txtAction.Text
                pi.C_Remark = txtRemark.Text
                pi.C_Action = InUserID
                pi.C_AddDate = Format(Now, "yyyy/MM/dd")

                pi.M_Code = ds.Tables("KaiLiao").Rows(i)("M_Code")

                'If IsDBNull(ds.Tables("KaiLiao").Rows(i)("M_Name")) Then
                '    pi.M_Name = Nothing
                'Else
                '    pi.M_Name = ds.Tables("KaiLiao").Rows(i)("M_Name")
                'End If
                'If IsDBNull(ds.Tables("KaiLiao").Rows(i)("M_Gauge")) Then
                '    pi.M_Gauge = Nothing
                'Else
                '    pi.M_Gauge = ds.Tables("KaiLiao").Rows(i)("M_Gauge")
                'End If

                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Qty")) Then
                    pi.C_Qty = 0
                Else
                    pi.C_Qty = ds.Tables("KaiLiao").Rows(i)("C_Qty")
                End If
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Weight")) Then
                    pi.C_Weight = 0
                Else
                    pi.C_Weight = ds.Tables("KaiLiao").Rows(i)("C_Weight")
                End If
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Cishu")) Then
                    pi.C_Cishu = 0
                Else
                    pi.C_Cishu = ds.Tables("KaiLiao").Rows(i)("C_Cishu")
                End If
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Type")) Then
                    pi.C_Type = Nothing
                Else
                    pi.C_Type = ds.Tables("KaiLiao").Rows(i)("C_Type")
                End If
                pc.ProductionKaiLiao_Add(pi)

            End If
        Next

        MsgBox("已保存,單號: " & txtCNO.Text & " ")
        Me.Close()
    End Sub

    Sub UpdateCheck()
        Dim pi As New ProductionKaiLiaoInfo
        Dim pc As New ProductionKaiLiaoControl

        pi.C_NO = txtCNO.Text
        pi.C_Check = CheckEdit1.Checked
        pi.C_CheckAction = InUserID
        pi.C_CheckRemark = txtCheckRemark.Text

        If pc.ProductionKaiLiao_UpdateCheck(pi) = True Then
            MsgBox("審核狀態已改變!")
        Else
            MsgBox("審核失敗,請檢查原因!")
        End If
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case Label8.Text
            Case "開料單"
                If Edit = False Then
                    If CheckData() = True Then
                        DataNew()
                    End If

                Else
                    If CheckData() = True Then
                        DataEdit()
                    End If

                End If
            Case "Check"
                If CheckData() = True Then
                    UpdateCheck()
                End If

        End Select

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmsCodeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsCodeAdd.Click
        Dim pii As List(Of ProductInventoryInfo)

        If GridView1.RowCount = 1 Then
            MsgBox("當前每張開料單只允許開一種物料!")
            Exit Sub
        End If

        tempCode = ""
        tempValue5 = strWHID
        tempValue6 = "開料管理"
        tempValue9 = PM_M_Code.Text

        frmBOMSelect.XtraTabPage1.PageVisible = True
        frmBOMSelect.XtraTabPage2.PageVisible = False  '不顯示批次信息
        frmBOMSelect.XtraTabPage3.PageVisible = True

        frmBOMSelect.ShowDialog()
        '增加記錄
        If frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then
            If tempCode = "" Then
                Exit Sub
            Else
                AddRow(tempCode)
            End If
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 1 Then  '批次
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempValue7, ",")
            n = Len(Replace(tempValue7, ",", "," & "*")) - Len(tempValue7)
            For i = 0 To n

                Dim j As Integer

                If GridView1.RowCount = 1 Then
                    MsgBox("當前每張開料單只允許開一種物料!")
                    Exit Sub
                End If

                For j = 0 To ds.Tables("KaiLiao").Rows.Count - 1
                    If arr(i) = ds.Tables("KaiLiao").Rows(j)("M_Code") Then
                        MsgBox("一張單不允許有重復物料編碼....")
                        Exit Sub
                    End If
                Next
                If arr(i) = "" Then
                    Exit Sub
                End If
                Dim mc As New LFERP.Library.Material.MaterialController
                Dim objInfo As New LFERP.Library.Material.MaterialInfo
                objInfo = mc.MaterialCode_Get(arr(i))

                Dim row As DataRow
                row = ds.Tables("KaiLiao").NewRow

                row("M_Code") = objInfo.M_Code
                row("M_Name") = objInfo.M_Name
                row("M_Unit") = objInfo.M_Unit
                row("M_Gauge") = objInfo.M_Gauge

                If CBType.Text = "裝配出貨" Then      '@ 2012/11/23 修改

                    pii = pic.ProductInventory_GetList(Nothing, arr(i), strWHID, Nothing)

                    If pii.Count > 0 Then
                        row("C_Weight") = pii(0).PI_Qty
                    Else
                        row("C_Weight") = 0
                    End If
                Else
                    '@ 2012/10/15 修改
                    Dim wi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
                    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController

                    wi = wc.WareInventory_GetList3(arr(i), strWHID, "True")
                    If wi.Count > 0 Then
                        row("C_Weight") = wi(0).WI_Qty
                    Else
                        row("C_Weight") = 0
                    End If

                End If
                row("C_Qty") = 0


                'If objInfo.M_Unit = "公斤" Or objInfo.M_Unit = "千克" Or objInfo.M_Unit = "KG" Or objInfo.M_Unit = "克" Or objInfo.M_Unit = "毫克" Or objInfo.M_Unit = "克拉" Or objInfo.M_Unit = "吨" Or objInfo.M_Unit = "斤" Or objInfo.M_Unit = "磅" Then
                '    Dim wi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
                '    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController

                '    wi = wc.WareInventory_GetList3(arr(i), strWHID)
                '    If wi.Count = 0 Then
                '        row("C_Weight") = 0
                '        row("C_Qty") = 0
                '    Else
                '        row("C_Weight") = wi(0).WI_Qty
                '        row("C_Qty") = 0
                '    End If
                'Else

                '    Dim wi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
                '    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController

                '    wi = wc.WareInventory_GetList3(arr(i), strWHID)
                '    If wi.Count = 0 Then
                '        row("C_Weight") = 0
                '        row("C_Qty") = 0
                '    Else
                '        row("C_Weight") = 0
                '        row("C_Qty") = wi(0).WI_Qty
                '    End If

                'End If

                row("C_Cishu") = 1
                row("C_Type") = "正批"
                ds.Tables("KaiLiao").Rows.Add(row)
                GridView1.MoveLast()
            Next

        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 2 Then   '聯豐編號
            Dim i, n As Integer
            Dim arr(n) As String
            'MsgBox(tempValue8)
            arr = Split(tempValue8, ",")
            n = Len(Replace(tempValue8, ",", "," & "*")) - Len(tempValue8)
            For i = 0 To n

                Dim j As Integer

                If GridView1.RowCount = 1 Then
                    MsgBox("當前每張開料單只允許開一種物料!")
                    Exit Sub
                End If

                For j = 0 To ds.Tables("KaiLiao").Rows.Count - 1
                    If arr(i) = ds.Tables("KaiLiao").Rows(j)("M_Code") Then
                        MsgBox("一張單不允許有重復物料編碼....")
                        Exit Sub
                    End If
                Next
                If arr(i) = "" Then
                    Exit Sub
                End If
                Dim mc As New LFERP.Library.Material.MaterialController
                Dim objInfo As New LFERP.Library.Material.MaterialInfo
                objInfo = mc.MaterialCode_Get(arr(i))
                Dim row As DataRow
                row = ds.Tables("KaiLiao").NewRow

                row("M_Code") = objInfo.M_Code
                row("M_Name") = objInfo.M_Name
                row("M_Unit") = objInfo.M_Unit
                row("M_Gauge") = objInfo.M_Gauge

                If CBType.Text = "裝配出貨" Then      '@ 2012/11/23 修改

                    pii = pic.ProductInventory_GetList(Nothing, arr(i), strWHID, Nothing)

                    If pii.Count > 0 Then
                        row("C_Weight") = pii(0).PI_Qty
                    Else
                        row("C_Weight") = 0
                    End If
                Else
                    '@ 2012/10/15 修改
                    Dim wi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
                    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController

                    wi = wc.WareInventory_GetList3(arr(i), strWHID, "True")
                    If wi.Count > 0 Then
                        row("C_Weight") = wi(0).WI_Qty
                    Else
                        row("C_Weight") = 0
                    End If
                End If

                row("C_Qty") = 0


                'If objInfo.M_Unit = "公斤" Or objInfo.M_Unit = "千克" Or objInfo.M_Unit = "KG" Or objInfo.M_Unit = "克" Or objInfo.M_Unit = "毫克" Or objInfo.M_Unit = "克拉" Or objInfo.M_Unit = "吨" Or objInfo.M_Unit = "斤" Or objInfo.M_Unit = "磅" Then
                '    Dim wi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
                '    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController

                '    wi = wc.WareInventory_GetList3(arr(i), strWHID)
                '    If wi.Count = 0 Then
                '        row("C_Weight") = 0
                '        row("C_Qty") = 0
                '    Else
                '        row("C_Weight") = wi(0).WI_Qty
                '        row("C_Qty") = 0
                '    End If
                'Else

                '    Dim wi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
                '    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController

                '    wi = wc.WareInventory_GetList3(arr(i), strWHID)
                '    If wi.Count = 0 Then
                '        row("C_Weight") = 0
                '        row("C_Qty") = 0
                '    Else
                '        row("C_Weight") = 0
                '        row("C_Qty") = wi(0).WI_Qty
                '    End If

                'End If


                row("C_Cishu") = 1
                row("C_Type") = "正批"
                ds.Tables("KaiLiao").Rows.Add(row)
                GridView1.MoveLast()
            Next
        End If
        tempValue2 = ""
        tempValue7 = ""
        tempValue8 = ""
        tempValue3 = ""
    End Sub

    Sub AddRow(ByVal strCode As String)
        Dim pii As List(Of ProductInventoryInfo)

        If strCode = "" Then
        Else

            If GridView1.RowCount = 1 Then
                MsgBox("當前每張開料單只允許開一種物料!")
                Exit Sub
            End If

            Dim i As Integer

            For i = 0 To ds.Tables("KaiLiao").Rows.Count - 1

                If strCode = ds.Tables("KaiLiao").Rows(i)("M_Code") Then
                    MsgBox("一張單不允許有重復物料編碼....")
                    Exit Sub
                End If
            Next
            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)
            Dim row As DataRow
            row = ds.Tables("KaiLiao").NewRow

            row("M_Code") = objInfo.M_Code

            row("M_Name") = objInfo.M_Name
            row("M_Unit") = objInfo.M_Unit
            row("M_Gauge") = objInfo.M_Gauge

            If CBType.Text = "裝配出貨" Then      '@ 2012/11/23 修改

                pii = pic.ProductInventory_GetList(Nothing, strCode, strWHID, Nothing)

                If pii.Count > 0 Then
                    row("C_Weight") = pii(0).PI_Qty
                Else
                    row("C_Weight") = 0
                End If
            Else
                '@ 2012/10/15 修改
                Dim wi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
                Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController

                wi = wc.WareInventory_GetList3(strCode, strWHID, "True")
                If wi.Count > 0 Then
                    row("C_Weight") = wi(0).WI_Qty
                Else
                    row("C_Weight") = 0
                End If
            End If

            row("C_Qty") = 0


            'If objInfo.M_Unit = "公斤" Or objInfo.M_Unit = "千克" Or objInfo.M_Unit = "KG" Or objInfo.M_Unit = "克" Or objInfo.M_Unit = "毫克" Or objInfo.M_Unit = "克拉" Or objInfo.M_Unit = "吨" Or objInfo.M_Unit = "斤" Or objInfo.M_Unit = "磅" Then
            '    Dim wi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
            '    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController

            '    wi = wc.WareInventory_GetList3(strCode, strWHID)
            '    If wi.Count = 0 Then
            '        row("C_Weight") = 0
            '        row("C_Qty") = 0
            '    Else
            '        row("C_Weight") = wi(0).WI_Qty
            '        row("C_Qty") = 0
            '    End If
            'Else

            '    Dim wi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
            '    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController

            '    wi = wc.WareInventory_GetList3(strCode, strWHID)
            '    If wi.Count = 0 Then
            '        row("C_Weight") = 0
            '        row("C_Qty") = 0
            '    Else
            '        row("C_Weight") = 0
            '        row("C_Qty") = wi(0).WI_Qty
            '    End If

            'End If


            'row("C_Qty") = 0
            'row("C_Weight") = 0
            row("C_Cishu") = 1
            row("C_Type") = "正批"
            ds.Tables("KaiLiao").Rows.Add(row)

            GridView1.MoveLast()
        End If
    End Sub
    Private Sub cmsCodeDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsCodeDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "IndexNo")

        If DelTemp = "IndexNo" Then
        Else
            '在刪除表中增加被刪除的記錄
            Dim row As DataRow = ds.Tables("DelKaiLiao").NewRow
            row("IndexNo") = DelTemp
            ds.Tables("DelKaiLiao").Rows.Add(row)
        End If
        ds.Tables("KaiLiao").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    'Private Sub CBType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBType.SelectedIndexChanged
    '    Dim pcc As New ProcessMainControl
    '    Dim pci As List(Of ProcessMainInfo)

    '    pci = pcc.ProcessMain_GetList(Nothing, Nothing, CBType.EditValue, Nothing)
    '    If pci.Count = 0 Then Exit Sub
    '    Dim i As Integer
    '    For i = 0 To pci.Count - 1
    '        Dim row As DataRow
    '        row = ds.Tables("Product").NewRow()
    '        row("PM_M_Code") = pci(i).PM_M_Code
    '        ds.Tables("Product").Rows.Add(row)
    '    Next
    'End Sub

    Private Sub PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged

        On Error Resume Next

        Dim pcc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        ds.Tables("ProductType").Clear()

        pci = pcc.ProcessMain_GetList1(Nothing, PM_M_Code.EditValue, CBType.EditValue, Nothing)
        If pci.Count = 0 Then Exit Sub

        Dim i As Integer
        For i = 0 To pci.Count - 1
            Dim row As DataRow
            row = ds.Tables("ProductType").NewRow
            row("PM_Type") = pci(i).Type3ID

            ds.Tables("ProductType").Rows.Add(row)
        Next

        Dim j As Integer
        For j = 0 To pci.Count - 1
            If PM_M_Code.EditValue = pci(j).Type3ID Then  '2013-8-9 若存在成品編號,先默認成品
                gluType.EditValue = pci(j).Type3ID
            End If
        Next






    End Sub

    Private Sub txtWH_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWH.ButtonClick
        tempValue3 = "880206"
        frmWareHouseSelect.ShowDialog()

        If frmWareHouseSelect.SelectWareID <> "" Then
            strWHID = frmWareHouseSelect.SelectWareID

            Dim wi As List(Of WareHouseInfo)
            Dim wc As New WareHouseController

            Dim strWHID1 As String
            strWHID1 = Mid(strWHID, 1, 3)
            wi = wc.WareHouse_Get(strWHID1)

            txtWH.EditValue = wi(0).WH_Name & "-" & frmWareHouseSelect.SelectWareName
        Else
            Exit Sub
        End If
       
    End Sub

    Function CheckData() As Boolean
        CheckData = True
        Dim wi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
        Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
        Dim pii As List(Of ProductInventoryInfo)

        Dim i As Integer

        For i = 0 To ds.Tables("KaiLiao").Rows.Count - 1

            Dim strQty As Double

            If CBType.Text = "裝配出貨" Then      '@ 2012/11/23 修改

                pii = pic.ProductInventory_GetList(Nothing, ds.Tables("KaiLiao").Rows(i)("M_Code"), strWHID, Nothing)

                If pii.Count > 0 Then
                    strQty = pii(0).PI_Qty
                Else
                    CheckData = False
                    strQty = 0
                    MsgBox("當前倉庫不存在此物料！")
                    Exit Function
                End If
            Else
                wi = wc.WareInventory_GetList3(ds.Tables("KaiLiao").Rows(i)("M_Code"), strWHID, "True")
                If wi.Count = 0 Then
                    CheckData = False
                    strQty = 0
                    MsgBox("當前倉庫不存在此物料！")
                    Exit Function
                Else
                    strQty = wi(0).WI_Qty
                End If
            End If

            'Dim mc As New LFERP.Library.Material.MaterialController
            'Dim objInfo As New LFERP.Library.Material.MaterialInfo
            'objInfo = mc.MaterialCode_Get(ds.Tables("KaiLiao").Rows(i)("M_Code"))

            'If ds.Tables("KaiLiao").Rows(i)("C_Weight") > strQty Then
            '    MsgBox("當前申領物料數大於當前倉庫庫存！")
            '    CheckData = False
            '    Exit Function
            'Else
            '    CheckData = True
            'End If

            'If objInfo.M_Unit = "公斤" Or objInfo.M_Unit = "千克" Or objInfo.M_Unit = "KG" Or objInfo.M_Unit = "克" Or objInfo.M_Unit = "毫克" Or objInfo.M_Unit = "克拉" Or objInfo.M_Unit = "吨" Or objInfo.M_Unit = "斤" Or objInfo.M_Unit = "磅" Then
            '    If ds.Tables("KaiLiao").Rows(i)("C_Weight") > strQty Then
            '        MsgBox("當前申領物料數大於當前倉庫庫存！")
            '        CheckData = False
            '        Exit Function
            '    Else
            '        CheckData = True
            '    End If
            'Else
            '    If ds.Tables("KaiLiao").Rows(i)("C_Qty") > strQty Then
            '        MsgBox("當前申領物料數大於當前倉庫庫存！")
            '        CheckData = False
            '        Exit Function
            '    Else
            '        CheckData = True
            '    End If
            'End If


        Next


    End Function
    '@ 2012/1/5 添加當控件內容發生改變，且PM_M_Code控件內容不爲空時，加載相應的內容到gluType控件
    '些過程調用以下過程：
    'PM_M_Code_EditValueChanged()
    Private Sub CBType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBType.SelectedIndexChanged
        If PM_M_Code.Text <> "" Then
            PM_M_Code_EditValueChanged(Nothing, Nothing)
        End If
    End Sub
End Class