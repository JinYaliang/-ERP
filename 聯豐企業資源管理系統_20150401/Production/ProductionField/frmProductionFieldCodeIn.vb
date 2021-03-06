Imports LFERP.Library.ProductionField
Imports LFERP.DataSetting
Imports LFERP.Library.Product
Imports LFERP.Library.ProductionWareHouse
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.ProductionFieldType
Imports LFERP.Library.PieceProcess
Imports LFERP.Library.ProductionKaiLiao
Imports LFERP.Library.ProductionDPTWareInventory
Imports LFERP.Library.WareHouse
Imports LFERP.Library.Purchase.SharePurchase
Imports LFERP.Library.Production.ProductionFieldDaySummary
Imports System.Threading
Imports LFERP.Library.Production.ProductionAffair
Imports LFERP.Library.ProductionMaterial
Imports LFERP.Library.Production.Datasetting

Imports LFERP.SystemManager
Public Class frmProductionFieldCodeIn

    Dim pc As New LFERP.Library.PieceProcess.PersonnelControl
    Dim OldCheck As Boolean
    Dim ds As New DataSet
    Dim upi As List(Of UserPowerInfo)
    Dim upc As New UserPowerControl
    Dim mc As New ProductionDataSettingControl

    Dim sngKaiLiao As Single
    Dim strM_Code As String

    Dim AutoSchedule As Boolean = False
    Dim KaoliaoShow As Boolean = False
    Dim LBJC_Check As Boolean = False

    Sub LoadPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880413")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then AutoSchedule = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880414")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then KaoliaoShow = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880416")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then LBJC_Check = True
        End If

    End Sub


    'Sub LoadProductNo()
    '    Dim mc As New ProductController
    '    PM_M_Code.Properties.DisplayMember = "PM_M_Code"
    '    PM_M_Code.Properties.ValueMember = "PM_M_Code"
    '    PM_M_Code.Properties.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

    'End Sub

    '@ 2012/2/22 添加
    '加載成品編號數據
    '此過程被以下過程調用：
    'frmProductionFieldCodeIn_Load()
    'LoadData()
    Sub LoadPM_M_Code()
        Dim mi As List(Of ProductionDataSettingInfo)
        mi = mc.ProductionUser_GetList(gluDep.EditValue, Nothing)

        ds.Tables("PM_M_Code").Clear()

        If mi.Count > 0 Then    '判斷是否有權限限制
            Dim row As DataRow
            Dim j As Integer
            For j = 0 To mi.Count - 1
                row = ds.Tables("PM_M_Code").NewRow
                row("PM_M_Code") = mi(j).PM_M_Code '
                row("PM_JiYu") = mi(j).PM_JiYu  'PM_JiYu
                ds.Tables("PM_M_Code").Rows.Add(row)
            Next
        Else
            Dim row As DataRow
            Dim j As Integer
            'Dim mpi As List(Of ProductInfo)
            'Dim mpc As New ProductController

            'mpi = mpc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            Dim mpi As List(Of ProcessMainInfo)
            Dim mpc As New ProcessMainControl

            mpi = mpc.ProcessMain_GetList3(Nothing, Nothing)

            If mpi.Count > 0 Then
                For j = 0 To mpi.Count - 1
                    row = ds.Tables("PM_M_Code").NewRow
                    row("PM_M_Code") = mpi(j).PM_M_Code
                    row("PM_JiYu") = mpi(j).PM_JiYu
                    ds.Tables("PM_M_Code").Rows.Add(row)
                Next
            End If
        End If

    End Sub

    Sub LoadProductionDetail()
        Dim mc As New ProductionFieldTypeControl
        gluDetail.Properties.DisplayMember = "PT_Type"
        gluDetail.Properties.ValueMember = "PT_NO"
        gluDetail.Properties.DataSource = mc.ProductionFieldType_GetList(Nothing, Nothing, "收料")
    End Sub

    Sub LoadDepartment()
        gluDep.Properties.DataSource = pc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)  '原始部門
        gluDep.Properties.DisplayMember = "DepName"
        gluDep.Properties.ValueMember = "DepID"

        gluChangeDep.Properties.DataSource = pc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)  '變更部門
        gluChangeDep.Properties.DisplayMember = "DepName"
        gluChangeDep.Properties.ValueMember = "DepID"
    End Sub

    '@ 2012/2/22 添加
    '加載工序編號數據
    '此過程被以下過程調用：
    'frmProductionFieldCodeIn_Load()
    'gluType_EditValueChanged()
    'LoadData()
    Sub LoadOutPS_Name()
        If gluDep.EditValue = "" Then Exit Sub

        Dim mi As List(Of ProductionDataSettingInfo)
        Dim row As DataRow
        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        mi = mc.ProductionIssue_GetList(gluDep.EditValue, gluChangeDep.EditValue, cbType.Text, PM_M_Code.Text, gluType.Text, Nothing)
        ds.Tables("Process").Clear()

        If mi.Count > 0 Then        '判斷是否有權限限制
            Dim i%
            For i = 0 To mi.Count - 1
                pci = pc.ProcessSub_GetList(Nothing, mi(i).Pro_NO, Nothing, Nothing, Nothing, True)
                If pci.Count > 0 Then
                    row = ds.Tables("Process").NewRow
                    row("PS_NO") = mi(i).Pro_NO
                    row("PS_Name") = pci(0).PS_Name

                    ds.Tables("Process").Rows.Add(row)
                End If
            Next

        Else

            pci = pc.ProcessMain_GetList(Nothing, PM_M_Code.EditValue, cbType.EditValue, gluType.EditValue, Nothing, True)
            If pci.Count = 0 Then Exit Sub
            Dim i As Integer
            For i = 0 To pci.Count - 1
                row = ds.Tables("Process").NewRow
                row("PS_NO") = pci(i).PS_NO
                row("PS_Name") = pci(i).PS_Name

                ds.Tables("Process").Rows.Add(row)
            Next
        End If
    End Sub

    Private Sub frmProductionFieldCodeIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i%
        LoadPower()

        '@ 2012/5/14 添加 判斷是否需要刷卡
        If strRefCard = "是" Then
        Else
            SimpleButton1.Visible = False
            TextEdit1.Visible = False
            SimpleButton2.Visible = False
        End If
        CreateTable()
       
        'LoadProductNo()
        LoadPM_M_Code()     '@2012/2/22 修改 產品編號調用
        LoadProductionDetail()
        LoadDepartment()

        Label22.Text = tempValue4
        Label23.Text = tempValue
        Label5.Text = tempValue3
        Label6.Text = tempValue2

        tempValue = ""
        tempValue2 = ""
        tempValue3 = ""
        tempValue4 = ""

        Label30.Text = tempValue5  '當前操作員所在部門
        tempValue5 = ""



        txtNO.Enabled = False
        gluChangeDep.Enabled = False
        gluDep.Enabled = False
        PM_M_Code.Select()

        ''重置新刷卡機

        'Try
        Dim reset As New ResetPassWords.SetPassWords
        reset.SetPassWords()
        'Catch
        'End Try

        Select Case Label23.Text

            Case "CodeIn"
                If Edit = False Then


                    Dim lockobj As New Object()

                    '需要鎖定的代碼塊 
                    SyncLock lockobj
                        Thread.Sleep(Int(Rnd() * 400) + 100)

                        txtNO.Text = GetNO()
                    End SyncLock

                    gluDetail.EditValue = Label6.Text  '屬性

                    If gluDetail.EditValue = "PT14" Then
                        gluDep.EditValue = "G101"
                        gluDep.Enabled = False
                        gluChangeDep.EditValue = "F101"
                        gluChangeDep.Enabled = False
                    ElseIf gluDetail.EditValue = "PT03" Then

                        Label16.Text = tempValue10
                        Label20.Text = tempValue9
                        Label24.Text = tempCode
                        Label34.Text = tempValue11

                        tempValue11 = ""
                        tempValue10 = ""
                        tempValue9 = ""
                        tempCode = Nothing

                        LoadKaiLiao(Label5.Text)
                        '------------------------------------------------
                        'gludep.EditValue =‘用戶對應的生產倉庫
                        '------------------------------------------------
                        If tempValue7 = "1" Then
                            txtQty.Text = sngKaiLiao
                            txtQty.Enabled = False
                            txtWeight.Enabled = False
                            '2013-9-13

                            PanelKaiLiao.Visible = KaoliaoShow
                            txtQty.Enabled = KaoliaoShow

                            ''開料不良判斷
                            'txtKailiaoBuliang.Text = 0
                            txtC_NO.Text = Label5.Text

                            ' MsgBox(KaoliaoShow)

                        ElseIf tempValue7 = "2" Then
                            txtQty.Text = tempValue6
                            txtWeight.Text = tempValue8 '導入重量
                            txtQty.Enabled = False
                            txtWeight.Enabled = False

                            txtKailiaoBuliang.Text = 0
                            txtC_NO.Text = Label5.Text

                            PanelKaiLiao.Visible = False
                            txtQty.Enabled = False
                        End If

                        gluChangeDep.EditValue = Label30.Text

                    ElseIf gluDetail.EditValue = "PT04" Then  '2013-6-3
                        gluDep.EditValue = Label30.Text
                        gluDep.Enabled = False
                        gluChangeDep.EditValue = Label30.Text
                        gluChangeDep.Enabled = False

                    End If

                    upi = upc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

                    cbType.EditValue = upi(0).UserType
                    cbType.Enabled = False

                    DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")
                    Label9.Text = Format(Now, "HH:mm:ss")

                Else

                    LoadData(Label22.Text)
                    If gluDetail.EditValue = "PT03" Then
                        txtQty.Enabled = False
                        txtWeight.Enabled = False
                    End If
                    M_Code.Enabled = True
                    Me.Text = "修改--" & Label22.Text
                End If

                LoadPM_M_Code()

                If gluDetail.EditValue = "PT04" Then
                    txtQty.Enabled = True
                End If

            Case "PreView"

                LoadData(Label22.Text)
                cmdSave.Visible = False
                M_Code.Enabled = False
                Me.Text = "查看--" & Label22.Text

            Case "InCheck"
                LoadData(Label22.Text)
                CheckEdit1.Enabled = True

                For i = 0 To XtraTabPage1.Controls.Count - 1
                    XtraTabPage1.Controls(i).Enabled = False
                Next

                Me.Text = "確認--" & Label22.Text
        End Select

        gluDetail.Enabled = False
        tempValue6 = ""
        tempValue8 = ""

        LoadOutPS_Name()        '@ 2012/2/22 添加 工序編號調用


        If gluDetail.EditValue = "PT04" Then
            PanelLB.Visible = True
        Else
            PanelLB.Visible = False
        End If
    End Sub

    Sub CreateTable()

        With ds.Tables.Add("ProductType")
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluType.Properties.ValueMember = "PM_Type"
        gluType.Properties.DisplayMember = "PM_Type"
        gluType.Properties.DataSource = ds.Tables("ProductType")

        With ds.Tables.Add("Process")
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
        End With
        M_Code.Properties.ValueMember = "PS_NO"
        M_Code.Properties.DisplayMember = "PS_Name"
        M_Code.Properties.DataSource = ds.Tables("Process")

        '@ 2012/2/22 添加
        '創建成品編號表
        With ds.Tables.Add("PM_M_Code")
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_JiYu", GetType(String))
        End With

        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = ds.Tables("PM_M_Code")

    End Sub

    Function LoadKaiLiao(ByVal C_NO As String) As Boolean
        LoadKaiLiao = True

        Dim pki As List(Of ProductionKaiLiaoInfo)
        Dim pkc As New ProductionKaiLiaoControl
        pki = pkc.ProductionKaiLiao_GetList(C_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing)
        If pki.Count = 0 Then Exit Function

        gluDep.EditValue = pki(0).WH_ID  '倉庫編號
        gluChangeDep.EditValue = "" '用戶所在部門編號

        cbType.EditValue = pki(0).Pro_Type
        PM_M_Code.EditValue = pki(0).PM_M_Code
        gluType.EditValue = pki(0).PM_Type
        sngKaiLiao = pki(0).C_Weight
        Label6.Text = "PT03"  '開料
        strM_Code = pki(0).M_Code

        txtKailiaoBuliang.Text = pki(0).BadQty

    End Function

    Function LoadData(ByVal FP_NO As String) As Boolean
        LoadData = True
        LoadPM_M_Code()      '@ 2012/2/22 添加 成品編號調用

        Dim pi As List(Of ProductionFieldInfo)
        Dim pc As New ProductionFieldControl

        pi = pc.ProductionField_GetList(FP_NO, Nothing, "收料", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "2", Nothing, Nothing)

        If pi.Count = 0 Then
            MsgBox("沒有數據")
            LoadData = False
            Exit Function
        Else

            txtNO.Text = pi(0).FP_NO
            Label7.Text = pi(0).FP_Num

            gluDep.EditValue = pi(0).FP_InDep
            gluChangeDep.EditValue = pi(0).FP_OutDep

            cbType.EditValue = pi(0).Pro_Type
            PM_M_Code.EditValue = pi(0).PM_M_Code
            gluType.EditValue = pi(0).PM_Type
            DateEdit1.EditValue = Format(pi(0).FP_Date, "yyyy/MM/dd")
            Label9.Text = Format(pi(0).FP_Date, "HH:mm:ss")

            Label6.Text = pi(0).FP_Type  '收發類型



            gluDetail.EditValue = pi(0).FP_Detail
            LoadOutPS_Name()        '@ 2012/2/22 添加 工序編號調用

            M_Code.EditValue = pi(0).Pro_NO

            txtQty.Text = pi(0).FP_Qty
            txtWeight.Text = pi(0).FP_Weight

            txtRemark.Text = pi(0).FP_Remark
            txtIWNO.Text = pi(0).IW_NO
            TextEdit1.Text = pi(0).CardID

            If pi(0).FP_InCheck = True Then
                CheckEdit1.Checked = True
                OldCheck = True
            Else
                CheckEdit1.Checked = False
                OldCheck = False
            End If

            ''載入收發/開料單
            Dim pki As New List(Of ProductionKaiLiaoInfo)
            Dim pkc As New ProductionKaiLiaoControl
            pki = pkc.ProductionKaiLiaoA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, FP_NO)
            If pki.Count > 0 Then
                'If pi(0).FP_InCheck = False Then
                '    txtKailiaoBuliang.Text = pki(0).C_Weight - Val(txtQty.Text)
                'Else
                '    txtKailiaoBuliang.Text = pki(0).BadQty
                'End If

                txtKailiaoBuliang.Text = pki(0).BadQty
                txtC_NO.Text = pki(0).C_NO
            End If

            LoadLBJC(pi(0).ReturnQty)

        End If
    End Function

    Function GetNO() As String

        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl
        Dim strA As String
        strA = Format(Now, "yyMM")
        pi = pc.ProductionField_GetNO(strA)

        If pi Is Nothing Then
            GetNO = "PF" & strA & "000001"
        Else
            GetNO = "PF" + strA + Mid((CInt(Mid(pi.FP_NO, 7)) + 1000001), 2)
        End If

    End Function

    Function GetNum() As String

        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl
        Dim strA As String
        strA = Format(Now, "yyMM")
        pi = pc.ProductionField_GetNO(strA)

        If pi Is Nothing Then
            GetNum = strA & "000001"
        Else
            GetNum = strA + Mid((CInt(Mid(pi.FP_Num, 5)) + 1000001), 2)
        End If

    End Function

    '@ 2012/1/6 修改判斷不正確時，相應控件獲得焦點
    Sub DataNew() '工藝流程第一步(開料)收入物料

        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl


        'Dim lockobj As New Object()

        ''需要鎖定的代碼塊 
        'SyncLock lockobj
        '    Thread.Sleep(Int(Rnd() * 400) + 100)

        '    txtNO.Text = GetNO()
        'End SyncLock

        pi.FP_NO = txtNO.Text
        pi.FP_Num = GetNum()

        pi.Pro_Type = cbType.EditValue
        pi.PM_M_Code = PM_M_Code.EditValue
        pi.PM_Type = gluType.EditValue

        pi.Pro_NO = M_Code.EditValue
        pi.CardID = TextEdit1.Text

        If txtQty.Text.Trim = "" Then
            MsgBox("數量不能為空!", 64, "提示")
            txtQty.Select()
            Exit Sub
        End If
        If txtWeight.Text.Trim = "" Then
            MsgBox("重量不能為空!", 64, "提示")
            txtWeight.Select()
            Exit Sub
        End If
        If CInt(txtQty.Text) < 0 Then
            MsgBox("數量必須為大於0的整數", 64, "提示")
            txtQty.Select()
            Exit Sub
        End If
        If CInt(txtWeight.Text) < 0 Then
            MsgBox("數量必須為大於0的數", 64, "提示")
            txtWeight.Select()
            Exit Sub
        End If

        'If CInt(txtQty.Text) > CInt(Label11.Text) Then
        '    MsgBox("當前發出數量不能大於當前結餘數!")
        '    Exit Sub
        'End If

        'If CheckIsNumber() = True Then

        'Else
        '    Exit Sub
        'End If
        pi.FP_Qty = txtQty.Text
        pi.FP_Weight = txtWeight.Text

        pi.FP_Date = CDate(DateEdit1.EditValue & " " & Label9.Text)
        pi.FP_Detail = gluDetail.EditValue
        pi.FP_OutDep = gluChangeDep.EditValue '倉庫
        pi.FP_OutAction = InUserID
        pi.FP_InDep = gluDep.EditValue '默認當前用戶所在部門
        pi.FP_Remark = txtRemark.Text

        If gluDetail.EditValue = "PT14" Then
            If Len(txtIWNO.Text.Trim) = 0 Then
                MsgBox("聯豐送回單號不能為空!")
                Exit Sub
            End If
            pi.IW_NO = txtIWNO.Text  '外發聯豐送回單號
        Else
            pi.IW_NO = ""
        End If


        If pc.ProductionField_InAdd(pi) = True Then
            MsgBox("保存成功")


            If gluDetail.EditValue = "PT03" Then
                '因無字段記錄結單狀態，因此只能在新增時添加開料記錄
                '存在一個問題，當收發記錄被刪除時，開料記錄不會刪除
                Dim pii As New ProductionKaiLiaoInfo
                Dim piic As New ProductionKaiLiaoControl

                pii.KL_NO = Label5.Text
                pii.M_Code = strM_Code
                If Label16.Text = "原材料" Then
                    pii.M_Type = "原材料"
                    pii.KL_TheoryWeight = Label20.Text
                    pii.KL_Check = Label24.Text
                Else
                    pii.M_Type = "配件"
                    pii.KL_TheoryWeight = 0

                    ''
                    If PanelKaiLiao.Visible = True Then
                        pii.KL_Check = 0
                    Else
                        pii.KL_Check = Label24.Text
                    End If

                End If

                pii.KL_Qty = txtQty.Text

                pii.KL_ActualWeight = txtWeight.Text
                pii.KL_Action = InUserID
                pii.KL_Date = Format(Now, "yyyy/MM/dd")

                ' 

                piic.KaiLiaoManagement_Add(pii)


                UpdateKailiao(0)

            End If

            SaveDataLBJC()


        Else
            MsgBox("保存失敗,請檢查原因!")
            Exit Sub
        End If

        Me.Close()
    End Sub

    '@ 2012/1/6 修改判斷不正確時，相應控件獲得焦點
    Sub DataEdit()
        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl

        pi.FP_Num = Label7.Text
        pi.PM_M_Code = PM_M_Code.EditValue
        pi.PM_Type = gluType.EditValue
        pi.Pro_NO = M_Code.EditValue

        If txtQty.Text.Trim = "" Then
            MsgBox("數量不能為空!", 64, "提示")
            txtQty.Select()
            Exit Sub
        End If
        If txtWeight.Text.Trim = "" Then
            MsgBox("重量不能為空!", 64, "提示")
            txtWeight.Select()
            Exit Sub
        End If
        If CInt(txtQty.Text) < 0 Then
            MsgBox("數量必須為大於0的整數", 64, "提示")
            txtQty.Select()
            Exit Sub
        End If
        If CInt(txtWeight.Text) < 0 Then
            MsgBox("數量必須為大於0的整數", 64, "提示")
            txtWeight.Select()
            Exit Sub
        End If

        If CheckDataLBJC() = False Then
            Exit Sub
        End If


        'If CInt(txtQty.Text) > CInt(Label11.Text) Then
        '    MsgBox("當前發出數量不能大於當前結餘數!")
        '    Exit Sub
        'End If

        'If CheckIsNumber() = True Then

        'Else
        '    Exit Sub
        'End If
        pi.FP_Qty = txtQty.Text
        pi.FP_Weight = txtWeight.Text

        pi.FP_OutAction = InUserID
        pi.FP_OutDep = gluChangeDep.EditValue
        pi.FP_Date = CDate(DateEdit1.EditValue & " " & Label9.Text)
        pi.FP_Detail = gluDetail.EditValue
        pi.FP_InDep = gluDep.EditValue
        pi.FP_Remark = txtRemark.Text
        pi.CardID = TextEdit1.Text

        If pc.ProductionField_InUpdate(pi) = True Then
            SaveDataLBJC()
            MsgBox("保存成功")
        Else
            MsgBox("保存失敗,請檢查原因!")
        End If
        Me.Close()
    End Sub

    Function GetTypeData() As Boolean
        GetTypeData = True
        '1<<<<<<<<<<<<-----------查出ProductionFieldType表的許可行數
        Dim SetDataVal, SetDataPTF As Integer
        Dim pfcon As New ProductionFieldTypeControl
        Dim pflist As New List(Of ProductionFieldTypeInfo)
        pflist = pfcon.ProductionFieldType_GetList(GluDetail.EditValue, Nothing, Nothing)
        If pflist.Count > 0 Then
            SetDataVal = pflist(0).PT_DataValue
        Else
            SetDataVal = 0
        End If
        '2-----------------------查出ProductionField表的單筆實際行數
        Dim piL As New List(Of ProductionFieldInfo)
        Dim pc As New ProductionFieldControl
        piL = pc.ProductionField_GetList(txtNo.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "2", Nothing, Nothing)
        If piL.Count > 0 Then
            SetDataPTF = piL.Count
        Else
            SetDataPTF = 0
        End If
        '3-------------正常比對單筆行數不能大于實際行數-------------
        If SetDataPTF = SetDataVal Or SetDataVal = 0 Then

        Else
            GetTypeData = False
        End If
        '---------------------------------------------->>>>>>>>>>>>>
    End Function
    Sub UpdateInCheck()
        If GetTypeData() = False Then
            MsgBox("此單已存在重單,請及時與電腦部聯系!")
            Exit Sub
        End If

        'Dim pi As New ProductionFieldInfo
        'Dim pc As New ProductionFieldControl

        'pi.FP_NO = txtNO.Text
        'pi.FP_Type = "收料"
        'If CheckEdit1.Checked = True Then
        '    pi.FP_InCheck = True
        'Else
        '    pi.FP_InCheck = False
        'End If
        'pi.FP_InAction = InUserID
        'pi.FP_InCheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

        'If CheckEdit1.Checked = OldCheck Then
        '    MsgBox("未改變確認狀態,不允許保存!")
        '    Exit Sub
        'End If

        'If pc.ProductionField_UpdateInCheck(pi) = True Then
        '    MsgBox("確認信息發生改變!")
        'Else
        '    MsgBox("保存失敗,請檢查原因!")
        'End If


        ''----------------------------------------------------------
        ''系統新增開料, 加存.此時不減對應倉庫數量
        ''只新增當前物料在當前部門的數量
        'Dim mt As New ProductionDPTWareInventoryControl
        'Dim mm As New ProductionDPTWareInventoryInfo

        'mm.DPT_ID = gluChangeDep.EditValue
        'mm.M_Code = M_Code.EditValue

        'Dim Qty, Qty1 As Single
        'Dim wi As List(Of ProductionDPTWareInventoryInfo)
        'Dim wc As New ProductionDPTWareInventoryControl
        'wi = wc.ProductionDPTWareInventory_GetList(gluChangeDep.EditValue, M_Code.EditValue, Nothing)

        'If wi.Count = 0 Then
        '    Qty = 0
        '    Qty1 = 0
        'Else
        '    Qty = wi(0).WI_Qty
        '    Qty1 = wi(0).WI_ReQty
        'End If

        'If CheckEdit1.Checked = True Then
        '    mm.WI_Qty = Qty + CSng(txtQty.Text)   '確認收料
        'ElseIf CheckEdit1.Checked = False Then
        '    mm.WI_Qty = Qty - CSng(txtQty.Text)  '取消--物料加入
        'End If
        'mm.WI_ReQty = Qty1
        'mt.UpdateProductionField_Qty(mm)  '對應當前部門數量

        ''-----------------
        ''變更當前部門當前工序確認審核后--此收發單號的結餘數量(保存此時此部門庫存數)

        'Dim pi1 As New ProductionFieldInfo
        'pi1.FP_NO = txtNO.Text
        'pi1.FP_OutDep = gluChangeDep.EditValue
        'pi1.Pro_NO = M_Code.EditValue
        'pi1.FP_Type = "收料"
        'pi1.FP_EndQty = mm.WI_Qty
        'pi1.FP_EndReQty = mm.WI_ReQty

        'pc.ProductionField_UpdateEndQty(pi1)
        ''-----------------

        ''----------------------------------------------------------
        ''----------------------------------------------------------對應當天該工序編碼該部門數量匯總信息
        'Dim pdi As List(Of ProductionFieldDaySummaryInfo)
        'Dim pdc As New ProductionFieldDaySummaryControl

        'Dim udi As New ProductionFieldDaySummaryInfo


        'Dim StrType As String  '類型
        'Dim IntQty As Integer  '數量

        'pdi = pdc.ProductionFieldDaySummary_GetList(Nothing, Nothing, Nothing, M_Code.EditValue, gluChangeDep.EditValue, Nothing, DateEdit1.Text, DateEdit1.Text)

        'If gluDetail.EditValue = "PT03" Then

        '    StrType = "收料"
        '    If pdi.Count = 0 Then
        '        IntQty = 0
        '    Else
        '        IntQty = pdi(0).ShouLiao
        '    End If
        '    udi.Pro_Type = cbType.EditValue
        '    udi.PM_M_Code = PM_M_Code.EditValue
        '    udi.PM_Type = gluType.EditValue
        '    udi.Pro_NO = M_Code.EditValue
        '    udi.FP_OutDep = gluChangeDep.EditValue
        '    udi.PM_Date = DateEdit1.EditValue

        '    udi.BuNiang = 0
        '    udi.CunCang = 0
        '    udi.CunHuo = 0
        '    udi.DiuShi = 0
        '    udi.FaLiao = 0
        '    udi.FanXiuIn = 0
        '    udi.FanXiuOut = 0
        '    udi.JiaCun = 0
        '    If CheckEdit1.Checked = True Then
        '        udi.ShouLiao = IntQty + CInt(txtQty.Text)
        '    ElseIf CheckEdit1.Checked = False Then
        '        udi.ShouLiao = IntQty - CInt(txtQty.Text)
        '    End If
        '    udi.QuCang = 0
        '    udi.QuCun = 0
        '    udi.LiuBan = 0
        '    udi.SunHuai = 0
        '    udi.ChuHuo = 0
        '    udi.WaiFaIn = 0
        '    udi.WaiFaOut = 0
        '    udi.AccIn = 0
        '    udi.AccOut = 0
        '    udi.RePairOut = 0
        '    udi.Type = StrType

        '    pdc.UpdateProductionDaySummary_Qty(udi)   '變更收料數量(開料產生)
        'ElseIf gluDetail.EditValue = "PT04" Then
        '    StrType = "加存"
        '    If pdi.Count = 0 Then
        '        IntQty = 0
        '    Else
        '        IntQty = pdi(0).JiaCun
        '    End If
        '    udi.Pro_Type = cbType.EditValue
        '    udi.PM_M_Code = PM_M_Code.EditValue
        '    udi.PM_Type = gluType.EditValue
        '    udi.Pro_NO = M_Code.EditValue
        '    udi.FP_OutDep = gluChangeDep.EditValue
        '    udi.PM_Date = DateEdit1.EditValue

        '    udi.BuNiang = 0
        '    udi.CunCang = 0
        '    udi.CunHuo = 0
        '    udi.DiuShi = 0
        '    udi.FaLiao = 0
        '    udi.FanXiuIn = 0
        '    udi.FanXiuOut = 0
        '    udi.ShouLiao = 0
        '    If CheckEdit1.Checked = True Then
        '        udi.JiaCun = IntQty + CInt(txtQty.Text)
        '    ElseIf CheckEdit1.Checked = False Then
        '        udi.JiaCun = IntQty - CInt(txtQty.Text)
        '    End If
        '    udi.QuCang = 0
        '    udi.QuCun = 0
        '    udi.LiuBan = 0
        '    udi.SunHuai = 0
        '    udi.ChuHuo = 0
        '    udi.WaiFaIn = 0
        '    udi.WaiFaOut = 0
        '    udi.AccIn = 0
        '    udi.AccOut = 0
        '    udi.RePairOut = 0
        '    udi.Type = StrType

        '    pdc.UpdateProductionDaySummary_Qty(udi)   '變更加存數量(加存產生)
        'ElseIf gluDetail.EditValue = "PT14" Then
        '    StrType = "外發收入"
        '    If pdi.Count = 0 Then
        '        IntQty = 0
        '    Else
        '        IntQty = pdi(0).WaiFaIn
        '    End If
        '    udi.Pro_Type = cbType.EditValue
        '    udi.PM_M_Code = PM_M_Code.EditValue
        '    udi.PM_Type = gluType.EditValue
        '    udi.Pro_NO = M_Code.EditValue
        '    udi.FP_OutDep = gluChangeDep.EditValue
        '    udi.PM_Date = DateEdit1.EditValue

        '    udi.BuNiang = 0
        '    udi.CunCang = 0
        '    udi.CunHuo = 0
        '    udi.DiuShi = 0
        '    udi.FaLiao = 0
        '    udi.FanXiuIn = 0
        '    udi.FanXiuOut = 0
        '    udi.ShouLiao = 0
        '    If CheckEdit1.Checked = True Then
        '        udi.WaiFaIn = IntQty + CInt(txtQty.Text)
        '    ElseIf CheckEdit1.Checked = False Then
        '        udi.WaiFaIn = IntQty - CInt(txtQty.Text)
        '    End If
        '    udi.QuCang = 0
        '    udi.QuCun = 0
        '    udi.LiuBan = 0
        '    udi.SunHuai = 0
        '    udi.ChuHuo = 0
        '    udi.JiaCun = 0
        '    udi.WaiFaOut = 0
        '    udi.AccIn = 0
        '    udi.AccOut = 0
        '    udi.RePairOut = 0
        '    udi.Type = StrType

        '    pdc.UpdateProductionDaySummary_Qty(udi)   '變更數量()
        'End If

        'If pdi.Count > 0 Then

        '    udi.Pro_NO = M_Code.EditValue
        '    udi.FP_OutDep = gluDep.EditValue
        '    udi.PM_Date = DateEdit1.Text

        '    pdc.ProductionFieldDaySummary_Delete(udi) '判斷當前工序是否所有數量都為0 Yes刪除此條記錄,NO繼續保留!

        'End If

        Dim pai As New ProductionAffairInfo
        Dim pac As New ProductionAffairControl

        Dim pdi As List(Of ProductionDPTWareInventoryInfo)
        Dim pdc As New ProductionDPTWareInventoryControl

        Dim pdsi As List(Of ProductionFieldDaySummaryInfo)
        Dim pdsc As New ProductionFieldDaySummaryControl

        Dim strQty, strReQty As Integer
        Dim strShouLiao, strJiaCun, strQuCun, strFaLiao, strCunHuo, strFanXiuIn, strFanXiuOut, strLiuBan, strSunHuai, strDiuShi, strBuNiang, strCunCang, strQuCang, strChuHuo, strWaiFaIn, strWaiFaOut, strAccIn, strAccOut, strRePairOut, strZuheOut As Integer

        pdi = pdc.ProductionDPTWareInventory_GetList(gluChangeDep.EditValue, M_Code.EditValue, Nothing)
        If pdi.Count = 0 Then
            strQty = 0
            strReQty = 0
        Else
            strReQty = pdi(0).WI_ReQty
            strQty = pdi(0).WI_Qty
        End If
        pdsi = pdsc.ProductionFieldDaySummary_GetList(Nothing, Nothing, Nothing, M_Code.EditValue, gluChangeDep.EditValue, Nothing, DateEdit1.Text, DateEdit1.Text)
        If pdsi.Count = 0 Then
            strShouLiao = 0
            strJiaCun = 0
            strQuCun = 0
            strFaLiao = 0
            strCunHuo = 0
            strFanXiuIn = 0
            strFanXiuOut = 0
            strLiuBan = 0
            strSunHuai = 0
            strDiuShi = 0
            strBuNiang = 0
            strCunCang = 0
            strQuCang = 0
            strChuHuo = 0
            strWaiFaIn = 0
            strWaiFaOut = 0
            strAccIn = 0
            strAccOut = 0
            strRePairOut = 0
            strZuheOut = 0
        Else
            strShouLiao = pdsi(0).ShouLiao
            strJiaCun = pdsi(0).JiaCun
            strQuCun = pdsi(0).QuCun
            strFaLiao = pdsi(0).FaLiao
            strCunHuo = pdsi(0).CunHuo
            strFanXiuIn = pdsi(0).FanXiuIn
            strFanXiuOut = pdsi(0).FanXiuOut
            strLiuBan = pdsi(0).LiuBan
            strSunHuai = pdsi(0).SunHuai
            strDiuShi = pdsi(0).DiuShi
            strBuNiang = pdsi(0).BuNiang
            strCunCang = pdsi(0).CunCang
            strQuCang = pdsi(0).QuCang
            strChuHuo = pdsi(0).ChuHuo
            strWaiFaIn = pdsi(0).WaiFaIn
            strWaiFaOut = pdsi(0).WaiFaOut
            strAccIn = pdsi(0).AccIn
            strAccOut = pdsi(0).AccOut
            strRePairOut = pdsi(0).RePairOut
            strZuheOut = pdsi(0).ZuheOut

        End If

        If CheckEdit1.Checked = OldCheck Then
            MsgBox("未改變確認狀態,不允許保存!")
            Exit Sub
        End If

        pai.FP_NO = txtNO.Text
        pai.FP_Type = "收料"
        pai.FP_InAction = InUserID
        pai.CardID = TextEdit1.Text

        If CheckEdit1.Checked = True Then
            pai.FP_InCheck = True
        Else
            pai.FP_InCheck = False
        End If

        pai.FP_InCheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        '-------------------------------------------------------------------
        pai.Pro_Type = cbType.EditValue
        pai.PM_M_Code = PM_M_Code.EditValue
        pai.PM_Type = gluType.EditValue
        pai.Pro_Type1 = Nothing
        pai.PM_M_Code1 = Nothing
        pai.PM_Type1 = Nothing
        pai.Pro_NO = M_Code.EditValue
        pai.Pro_NO1 = Nothing
        pai.FP_OutDep = gluChangeDep.EditValue
        pai.FP_InDep = Nothing

        pai.FP_Detail = gluDetail.EditValue
        pai.Type = Nothing

        '------------------------------------------------------變更部門結餘數信息
        pai.WI_Qty = strQty + CInt(txtQty.Text)
        pai.WI_ReQty = strReQty
        pai.WI_Qty1 = 0
        pai.WI_ReQty1 = 0

        '--------------------------------------------------------------------
        If gluDetail.EditValue = "PT03" Then
            pai.ShouLiao = strShouLiao + CInt(txtQty.Text)
            pai.JiaCun = strJiaCun
        ElseIf gluDetail.EditValue = "PT04" Then
            pai.ShouLiao = strShouLiao
            pai.JiaCun = strJiaCun + CInt(txtQty.Text)
        End If
        pai.QuCun = strQuCun
        pai.FaLiao = strFaLiao
        pai.CunHuo = strCunHuo
        pai.FanXiuIn = strFanXiuIn
        pai.FanXiuOut = strFanXiuOut
        pai.LiuBan = strLiuBan
        pai.SunHuai = strSunHuai
        pai.DiuShi = strDiuShi
        pai.BuNiang = strBuNiang
        pai.CunCang = strCunCang
        pai.QuCang = strQuCang
        pai.ChuHuo = strChuHuo
        pai.WaiFaIn = strWaiFaIn
        pai.WaiFaOut = strWaiFaOut
        pai.AccIn = strAccIn
        pai.AccOut = strAccOut
        pai.RePairOut = strRePairOut
        pai.ZuheOut = strZuheOut

        '------------------------------------------存在有收有發情況下
        pai.ShouLiao1 = 0
        pai.JiaCun1 = 0
        pai.QuCun1 = 0
        pai.FaLiao1 = 0
        pai.CunHuo1 = 0
        pai.FanXiuIn1 = 0
        pai.FanXiuOut1 = 0
        pai.LiuBan1 = 0
        pai.SunHuai1 = 0
        pai.DiuShi1 = 0
        pai.BuNiang1 = 0
        pai.CunCang1 = 0
        pai.QuCang1 = 0
        pai.ChuHuo1 = 0
        pai.WaiFaIn1 = 0
        pai.WaiFaOut1 = 0
        pai.AccIn1 = 0
        pai.AccOut1 = 0
        pai.RePairOut1 = 0
        pai.ZuheOut1 = 0

        '------------------------------------------
        pai.PM_Date = DateEdit1.Text

        If pac.UpdateProductionCheck_Qty(pai) = True Then
            MsgBox("確認當前物料收發已完成審核!")
            UpdateCheck()

            UpdateKailiao(Val(txtQty.Text))

        Else
            MsgBox("當前確認操作失敗,請檢查原因!")
            Exit Sub
        End If


        Me.Close()
    End Sub

    Sub UpdateCheck()

        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl

        pi.FP_NO = txtNo.Text
        pi.FP_Check = True

        pi.FP_CheckAction = InUserID
        pi.FP_CheckRemark = ""

        pc.ProductionField_UpdateCheck(pi)

        'If pc.ProductionField_UpdateCheck(pi) = True Then
        '    MsgBox("審核成功")
        'Else
        '    MsgBox("審核成功,請檢查原因!")
        'End If
        'Me.Close()
    End Sub


    Sub UpdateKailiao(ByVal BadQty As Int32)
        If KaoliaoShow = True Then '跟棕開料單
        Else
            Exit Sub
        End If

        Dim pi As New ProductionKaiLiaoInfo
        Dim pc As New ProductionKaiLiaoControl

        pi.C_NO = txtC_NO.Text
        pi.BadQty = BadQty
        pi.FP_NO = txtNO.Text

        If pc.ProductionKaiLiao_BadUpdate(pi) = True Then
        End If

    End Sub

    'Sub ChangeMaterial()

    '    '------------------------------------------------記錄生產開料后實際生產部門的領料記錄(區別于倉庫領料--便於計算實際的損耗狀態信息)
    '    Dim pmi As List(Of ProductionMaterialInfo)
    '    Dim pmi1 As New ProductionMaterialInfo
    '    Dim pmc As New ProductionMaterialControl

    '    pmi = pmc.ProductionMaterial_GetList(cbType.EditValue, PM_M_Code.EditValue, gluType.EditValue, strM_Code)

    '    Dim MaterialQty As Single
    '    If pmi.Count = 0 Then
    '        MaterialQty = 0
    '    Else
    '        MaterialQty = pmi(0).M_Qty
    '    End If

    '    pmi1.Pro_Type = cbType.EditValue
    '    pmi1.PM_M_Code = PM_M_Code.EditValue
    '    pmi1.PM_Type = gluType.EditValue
    '    pmi1.M_Code = strM_Code
    '    pmi1.M_Qty = MaterialQty - CSng(txtQty.Text)

    '    pmc.UpdateProductionMaterialQty(pmi1)   '變更當前實際的領料結餘數

    'End Sub

    'Sub UpdateKaiLiaoCheck()
    '    Dim pki As New ProductionKaiLiaoInfo
    '    Dim pkc As New ProductionKaiLiaoControl

    '    pki.C_NO = Label5.Text
    '    pki.C_ReCheck = CheckEdit1.Checked

    '    pkc.ProductionKaiLiao_ReCheck(pki)

    'End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case Label23.Text
            Case "CodeIn"

                If Edit = False Then
                    Dim fpi As List(Of ProductionFieldInfo)
                    Dim fpc As New ProductionFieldControl

                    fpi = fpc.ProductionField_GetList(txtNO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    If fpi.Count = 0 Then

                        If CheckData() = True Then
                            DataNew()
                        End If
                    Else
                        MsgBox("單號已存在，" & vbCr & "請確定重新生成單號!", 64, "提示")
                        txtNO.Text = GetNO()

                    End If

                    'UpdateKaiLiaoCheck()
                Else
                    DataEdit()
                End If
            Case "InCheck"
                If CheckEdit1.Checked = OldCheck Then
                    MsgBox("請確認信息未發生改變!")
                    Exit Sub
                End If
                UpdateInCheck()

                CheckLBJC()

                'UpdateCheck()
                'UpdateKaiLiaoCheck()
        End Select

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    'Private Sub txtQty_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyUp
    '    If (e.KeyValue > 47 And e.KeyValue < 58) Or (e.KeyValue > 95 And e.KeyValue < 106) Or (e.KeyValue = 8) Or (e.KeyValue = 45) Or (e.KeyValue = 46) Then

    '        Dim pc As New ProcessMainControl
    '        Dim pci As List(Of ProcessMainInfo)

    '        pci = pc.ProcessSub_GetList(Nothing, M_Code.EditValue, Nothing, Nothing, Nothing, Nothing)
    '        If pci.Count = 0 Then Exit Sub

    '        Dim AllWeight, strWeight, strG As Single

    '        strWeight = pci(0).PS_Weight  '克/個  單重
    '        strG = strWeight * txtQty.Text
    '        AllWeight = strG / 1000  '當前數量的重量(KG)
    '        txtWeight.Text = Format(AllWeight, "0.00") '(轉化為兩位小數)

    '    Else
    '        If txtQty.Text <> "" And Not IsNumeric(Trim(txtQty.Text)) Then
    '            'MsgBox("只能輸入整數數字！")
    '            txtQty.Text = Nothing
    '            Exit Sub
    '        End If
    '    End If
    'End Sub


    Private Sub PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged
        On Error Resume Next
        If PM_M_Code.EditValue = "" Or PM_M_Code.EditValue Is Nothing Then Exit Sub

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        ds.Tables("ProductType").Clear()
        ppi = ppc.ProcessMain_GetList2(cbType.EditValue, PM_M_Code.EditValue)
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
        gluType.EditValue = Nothing
    End Sub

    '@ 2012/2/22 修改為調用LoadOutPS_Name過程
    Private Sub gluType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluType.EditValueChanged
        If gluType.EditValue = "" Or gluType.EditValue Is Nothing Then Exit Sub

        LoadOutPS_Name()
        M_Code.EditValue = ds.Tables("Process").Rows(0)("PS_NO").ToString
    End Sub

    Private Sub txtWeight_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWeight.KeyUp
        Dim m As New System.Text.RegularExpressions.Regex("^+?(\d+(\.\d*)?|\.\d+)$")  '顯示整數,正浮點數正則表達式

        If m.IsMatch(txtWeight.Text) = True Then

        Else

            txtWeight.Text = Nothing
            Exit Sub
        End If
    End Sub


    Private Sub M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles M_Code.EditValueChanged
        If M_Code.EditValue = "" Or M_Code.EditValue Is Nothing Then Exit Sub

        Dim fdc As New ProductionDPTWareInventoryControl
        Dim fdi As List(Of ProductionDPTWareInventoryInfo)

        If gluDetail.EditValue = "PT03" Then
            Label11.Text = 0
        Else
            fdi = fdc.ProductionDPTWareInventory_GetList(gluDep.EditValue, M_Code.EditValue, Nothing)
            If fdi.Count = 0 Then
                Label11.Text = 0
            Else
                Label11.Text = fdi(0).WI_Qty
            End If

            'txtQty.Text = Label11.Text
        End If
    End Sub

    Function CheckData() As Boolean
        CheckData = True

        If TextEdit1.Text = "" And TextEdit1.Visible = True Then
            MsgBox("刷卡人信息不能為空！")
            CheckData = False
            Exit Function
        End If
        If gluDep.EditValue = "" Then
            MsgBox("發出部門不能為空！")
            CheckData = False
            Exit Function
        End If
        If gluChangeDep.EditValue = "" Then
            MsgBox("收入部門不能為空！")
            CheckData = False
            Exit Function
        End If
        If Len(txtQty.Text.Trim) = 0 Then
            MsgBox("數量信息不能為空！")
            CheckData = False
            Exit Function
        End If
        If Len(txtWeight.Text.Trim) = 0 Then
            MsgBox("重量信息不能為空！")
            CheckData = False
            Exit Function
        End If
        If M_Code.EditValue = "" Then
            MsgBox("工序信息不能為空！")
            CheckData = False
            Exit Function
        End If

        If AutoSchedule = False Then
            Dim psi As List(Of LFERP.Library.ProductionSchedule.ProductionScheduleInfo)
            Dim psc As New LFERP.Library.ProductionSchedule.ProductionScheduleControl

            psi = psc.ProductionSchedule_GetList(Nothing, cbType.EditValue, Nothing, PM_M_Code.EditValue, gluType.EditValue, DateEdit1.Text, DateEdit1.Text, Nothing)
            If psi.Count = 0 Then
                MsgBox("當前生產部不存在選定日期的生產計劃，請先添加生產計劃！")
                CheckData = False
                Exit Function
            Else
                CheckData = True
            End If
        End If

        '2013-9-13
        '要判斷開料單是否開足夠
        If KaoliaoShow = True Then
            '要判斷當前開料數量不能大於此開料單的總數

            Dim pki As New List(Of ProductionKaiLiaoInfo)
            Dim pkc As New ProductionKaiLiaoControl
            pki = pkc.ProductionKaiLiao_GetList(txtC_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing)
            If pki.Count > 0 Then
                If pki(0).C_Weight - pki(0).BadQty < Val(txtQty.Text) Then
                    MsgBox("當前領料數大於此開料單未交數！")
                    CheckData = False
                    Exit Function
                End If
            End If


            'If Val(txtKailiaoBuliang.Text) < 0 Then
            '    txtQty.Select()
            '    MsgBox("開料不良數不能為負數,請檢查!")
            '    CheckData = False
            'End If

            'If Val(txtQty.Text) + Val(txtKailiaoBuliang.Text) <> sngKaiLiao Then
            '    txtQty.Select()
            '    MsgBox("開料數+開料不良數 不等於 開料單總數,請檢查!")
            '    CheckData = False
            'End If
        End If

        If CheckDataLBJC() = False Then
            CheckData = False
        End If


    End Function

    '@ 2012/1/5修改為用正則表達式判斷輸入的是否為數字
    Private Sub txtQty_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyUp
        If txtQty.Text <> "" Then
            Dim m As New System.Text.RegularExpressions.Regex("^[1-9]+\d*$")
            If m.IsMatch(txtQty.Text) Then
                Dim pc As New ProcessMainControl
                Dim pci As List(Of ProcessMainInfo)

                pci = pc.ProcessSub_GetList(Nothing, M_Code.EditValue, Nothing, Nothing, Nothing, Nothing)
                If pci.Count = 0 Then Exit Sub

                Dim AllWeight, strWeight, strG As Single

                strWeight = pci(0).PS_Weight  '克/個  單重
                strG = strWeight * txtQty.Text
                AllWeight = strG / 1000  '當前數量的重量(KG)
                txtWeight.Text = Format(AllWeight, "0.00") '(轉化為兩位小數)
            Else
                MsgBox("只能輸入正整數！", 64, "提示")
                txtQty.Text = ""
            End If
        End If
    End Sub
    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        TextEdit1.Text = ReadCard()
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Dim frm As New frmNmetalSampleException
        frm.ShowDialog()

        TextEdit1.Text = tempValue
        tempValue = ""
    End Sub
    '2013-9-13
    Private Sub txtKailiaoBuliang_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKailiaoBuliang.KeyUp
        If txtKailiaoBuliang.Text <> "" Then
            Dim m As New System.Text.RegularExpressions.Regex("^[1-9]+\d*$")
            If m.IsMatch(txtKailiaoBuliang.Text) Then
            Else
                txtKailiaoBuliang.Text = Nothing
            End If
        End If
    End Sub

    Private Sub txtQty_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.EditValueChanged
        If Label23.Text = "CodeIn" Then
            ' If Edit = False Then
            ' txtKailiaoBuliang.Text = sngKaiLiao - Val(txtQty.Text)
            'End If
        End If

    End Sub



#Region "留辦/加存相關"
    Private Sub ButtonEditLB_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ButtonEditLB.ButtonClick

        If PM_M_Code.EditValue = "" Then
            MsgBox("請選擇產品編號!")
            PM_M_Code.Select()
            Exit Sub
        End If

        If cbType.EditValue = "" Then
            MsgBox("請選擇工藝類型!")
            cbType.Select()
            Exit Sub
        End If

        If gluType.EditValue = "" Then
            MsgBox("請選擇類型!")
            gluType.Select()
            Exit Sub
        End If

        If M_Code.EditValue = "" Then
            MsgBox("請選擇工序!")
            M_Code.Select()
            Exit Sub
        End If

        tempValue2 = PM_M_Code.EditValue
        tempValue3 = cbType.EditValue
        tempValue4 = gluType.EditValue
        tempValue5 = M_Code.EditValue
        tempValue6 = gluDep.EditValue

        frmProductionFieldLBJC.ShowDialog()
        frmProductionFieldLBJC.Dispose()

        If tempValue7 <> Nothing Then
            ButtonEditLB.EditValue = tempValue7
            LabelNoReturn.Text = tempValue8
            ButtonEditLB.Tag = tempValue9
        End If

        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing
        tempValue7 = Nothing
        tempValue8 = Nothing
        tempValue9 = Nothing
    End Sub

    Function CheckDataLBJC() As Boolean
        CheckDataLBJC = True

        If gluDetail.EditValue = "PT04" Then
        Else
            Exit Function
        End If

        If LBJC_Check = True Or ButtonEditLB.EditValue <> "" Then
            If ButtonEditLB.EditValue <> "" Then
            Else
                ButtonEditLB.Select()
                CheckDataLBJC = False
                MsgBox("留辦單不能為空!")
                Exit Function
            End If
            '-------------------------------------------------------------
            Dim a As New ProductionFieldControl
            Dim b As New List(Of ProductionFieldInfo)
            b = a.ProductionField_GetList1(ButtonEditLB.EditValue, cbType.EditValue, PM_M_Code.EditValue, gluType.EditValue, M_Code.EditValue, Nothing, Nothing, Nothing, "PT06", True, True, Nothing, Nothing, Nothing, Nothing)

            If b.Count <= 0 Then
                CheckDataLBJC = False
                ButtonEditLB.Select()
                MsgBox("加載的留辦單號,與載入信息不匹配!")
                Exit Function
            End If

            LabelNoReturn.Text = b(0).FP_Qty - b(0).ReturnQty
            LabelNoReturn.Tag = b(0).FP_Qty

            If Val(txtQty.Text) > Val(LabelNoReturn.Text) Then
                ButtonEditLB.Select()
                CheckDataLBJC = False
                MsgBox("加存數量大於,留辦單:" & ButtonEditLB.EditValue & "未還數!")
                Exit Function
            End If
            '-------------------------------------------------------------
        End If

    End Function


    Sub SaveDataLBJC()
        If gluDetail.EditValue = "PT04" Then
        Else
            Exit Sub
        End If

        If ButtonEditLB.EditValue <> "" Then
        Else
            Exit Sub
        End If

        Dim LBC As New ProductionFieldControl
        Dim LBi As New ProductionFieldInfo
        LBi.LBFP_NO = ButtonEditLB.EditValue
        LBi.LBQty = LabelNoReturn.Tag
        LBi.JCFP_NO = txtNO.Text
        LBi.JCQty = txtQty.Text
        LBi.JCLBCheck = False

        If LBC.ProductionFieldJCLB_Add(LBi) = True Then
        End If

    End Sub


    Sub CheckLBJC()
        If gluDetail.EditValue = "PT04" Then
        Else
            Exit Sub
        End If

        If ButtonEditLB.EditValue <> "" Then
        Else
            Exit Sub
        End If
        '------------------------------------------------
        Dim LBC1 As New ProductionFieldControl
        Dim LBi1 As New ProductionFieldInfo

        LBi1.LBFP_NO = ButtonEditLB.EditValue
        LBi1.JCFP_NO = txtNO.Text
        LBi1.JCLBCheck = True

        If LBC1.ProductionFieldJCLB_Check(LBi1) = True Then
        End If

    End Sub

    Sub LoadLBJC(ByVal ReturnQty As Integer)
        If gluDetail.EditValue = "PT04" Then
        Else
            Exit Sub
        End If

        Dim ac As New ProductionFieldControl
        Dim bi As New List(Of ProductionFieldInfo)

        bi = ac.ProductionFieldJCLB_GetList(Nothing, txtNO.Text, Nothing)

        If bi.Count <= 0 Then
            Exit Sub
        End If

        ButtonEditLB.EditValue = bi(0).LBFP_NO
        LabelNoReturn.Text = bi(0).LBQty - ReturnQty

    End Sub



#End Region

    Private Sub txtKailiaoBuliang_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKailiaoBuliang.EditValueChanged

    End Sub
End Class