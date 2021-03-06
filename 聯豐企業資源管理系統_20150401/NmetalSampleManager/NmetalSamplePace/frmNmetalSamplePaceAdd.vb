Imports System
Imports System.IO
Imports System.Windows.Forms
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Product
Imports LFERP.Library.ProductionField
Imports LFERP.Library.NmetalSampleManager.NmetalSampleProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSamplePace
Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersMain
Imports LFERP.Library.NmetalSampleManager.NmetalSampleProcessMain
Imports LFERP.Library.NmetalSampleManager.NmetalSampleWareInventory
Imports LFERP.Library.NmetalSampleManager.NmetalSampleCollection

Public Class frmNmetalSamplePaceAdd

#Region "屬性"
    Dim ds As New DataSet
    Dim LoadBZ As String
    Dim SamplePace As New NmetalSamplePaceControler
    Dim Spcon As New NmetalSampleProcessControl

    Private _EditItem As String '属性栏位
    Private _SO_IDVal As String
    Private _SE_IDA As String
    Private _SS_EditionVal As String
    Private _PM_M_CodeVal As String
    Private TempCode As String
    Private _SO_IDA As String

    Property SE_IDA() As String '属性
        Get
            Return _SE_IDA
        End Get
        Set(ByVal value As String)
            _SE_IDA = value
        End Set
    End Property
    Property SO_IDA() As String '属性
        Get
            Return _SO_IDA
        End Get
        Set(ByVal value As String)
            _SO_IDA = value
        End Set
    End Property

    Property SO_IDVal() As String '属性
        Get
            Return _SO_IDVal
        End Get
        Set(ByVal value As String)
            _SO_IDVal = value
        End Set
    End Property
    Property PM_M_CodeVal() As String '属性
        Get
            Return _PM_M_CodeVal
        End Get
        Set(ByVal value As String)
            _PM_M_CodeVal = value
        End Set
    End Property
    Property SS_EditionVal() As String '属性
        Get
            Return _SS_EditionVal
        End Get
        Set(ByVal value As String)
            _SS_EditionVal = value
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

#Region "按鍵事件"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' 数据保存時發生
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If DataCheckEmpty() = 0 Then
            Exit Sub
        End If

        Select Case EditItem
            Case "Add"
                DataNew()
            Case "AddPlan"
                DataNew()
            Case "Edit"
                DataEdit()
            Case "Check"
                UpdateCheck()
        End Select
    End Sub
#End Region

#Region "窗體載入事件"
    Private Sub frmSamplePace_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()

        RepositoryItemLookUpEdit1.DataSource = SamplePace.NmetalSamplePaceType_Getlist(Nothing)
        RepositoryItemLookUpEdit1.ValueMember = "SE_Type"
        RepositoryItemLookUpEdit1.DisplayMember = "SE_TypeName"
        '載入訂單編號
        Dim mtd As New NmetalSampleOrdersMainControler
        gluSO_ID.Properties.DisplayMember = "SO_ID"
        gluSO_ID.Properties.ValueMember = "SO_ID"
        gluSO_ID.Properties.DataSource = mtd.NmetalSampleOrdersMain_GetListItem(Nothing, Nothing, Nothing, True)

        If EditItem <> "Add" Then
            Dim mc As New NmetalSampleOrdersMainControler
            gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
            gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
            gluPM_M_Code.Properties.DataSource = mc.NmetalSampleOrdersMain_GetList(SO_IDVal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If

        Select Case EditItem
            Case "Add"
                Me.dateAddDate.EditValue = Format(Now, "yyyy/MM/dd")
            Case "AddPlan"
                Me.dateAddDate.EditValue = Format(Now, "yyyy/MM/dd")
                gluSO_ID.EditValue = SO_IDVal
                gluPM_M_Code.EditValue = PM_M_CodeVal
            Case "Edit"
                txtSS_Edition.Enabled = False
                gluPM_M_Code.Enabled = False
                gluM_Code.Enabled = False
                gluSO_ID.Enabled = False

                LoadData(SE_IDA)
            Case "Look"
                'gluSO_ID.Enabled = False
                'txtSS_Edition.Enabled = False
                'gluPM_M_Code.Enabled = False
                '' gluM_Code.Enabled = False
                'dateAddDate.Enabled = False
                cmdSave.Visible = False
                LoadData(SE_IDA)
            Case "Check"
                txtSS_Edition.Enabled = False
                gluPM_M_Code.Enabled = False
                gluM_Code.Enabled = False
                gluSO_ID.Enabled = False
                Panel1.Visible = True
                tsmNew.Enabled = False
                tsmDelete.Enabled = False
                LoadData(SE_IDA)
        End Select
        ' TreeList1.ExpandAll()
    End Sub
#End Region

#Region "创建临时表"
    ''' <summary>
    ''' 创建临时表
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("SamplePace")
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("PS_Name", GetType(String))
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("SE_PaceDescribe", GetType(String))
            .Columns.Add("State", GetType(String))
            .Columns.Add("SE_Type", GetType(String))
            .Columns.Add("SE_Qty", GetType(Int32))
            .Columns.Add("SE_AddDate", GetType(Date))
            .Columns.Add("SPID", GetType(String))
            .Columns.Add("SE_OutInType", GetType(String))
            .Columns.Add("SE_EndQty", GetType(Int32))
        End With
        gridSampleSace.DataSource = ds.Tables("SamplePace")

        With ds.Tables.Add("DelSamplePace")
            .Columns.Add("AutoID", GetType(Decimal))
        End With

        '创建刪除数据表
        With ds.Tables.Add("DelData")
            .Columns.Add("PS_NO", GetType(String))
        End With

        With ds.Tables.Add("ProductSub") '子配件表
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_PID", GetType(String))
            .Columns.Add("M_KEY", GetType(String))
        End With


        With ds.Tables.Add("SamplePaceBarCode") '子配件表
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("SPID", GetType(String))
            .Columns.Add("ClientBarcode", GetType(String))
            .Columns.Add("Qty", GetType(Int32))
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("SE_AddDate", GetType(String))
        End With
        Grid1.DataSource = ds.Tables("SamplePaceBarCode")

        With ds.Tables.Add("DelSamplePaceBarCode")
            .Columns.Add("AutoID", GetType(Decimal))
        End With

        '綁定表格
        '  Me.TreeList1.DataSource = ds.Tables("ProductSub")
    End Sub
#End Region

#Region "載入數據"


    ''' <summary>
    ''' 載入数据
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadData(ByVal StrSO_ID As String, ByVal StrSS_Edition As String) '返回数据
        Dim som As New List(Of NmetalSamplePaceInfo)
        som = SamplePace.NmetalSamplePace_Getlist(Nothing, StrSO_ID, StrSS_Edition, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If som.Count = 0 Then
            Exit Sub
        Else
            gluSO_ID.EditValue = som(0).SO_ID
            txtSS_Edition.Text = som(0).SS_Edition
            gluPM_M_Code.EditValue = som(0).PM_M_Code

            gluPM_M_Code_EditValueChanged(Nothing, Nothing)
            gluM_Code.EditValue = som(0).M_Name
            gluM_Code.Text = som(0).M_Code
            dateAddDate.Text = som(0).SE_AddDate

            LabSE_NO.Text = som(0).SE_ID
            CheckA.Checked = som(0).SE_Check
            LabAction.Text = som(0).SE_CheckActionName
            LabCheckDate.Text = som(0).SE_CheckDate

            Dim i As Integer
            ds.Tables("SamplePace").Clear()
            For i = 0 To som.Count - 1
                Dim row As DataRow
                row = ds.Tables("SamplePace").NewRow
                row("AutoID") = som(i).AutoID
                row("PS_Name") = som(i).PS_Name
                row("PS_NO") = som(i).PS_NO
                row("SE_PaceDescribe") = som(i).SE_PaceDescribe
                row("State") = som(i).State
                row("SE_Type") = som(i).SE_Type
                row("SE_Qty") = som(i).SE_Qty
                row("SE_AddDate") = som(i).SE_AddDate
                '----------------------------------------
                Dim SwCon As New NmetalSampleWareInventoryControler
                Dim SwList As New List(Of NmetalSampleWareInventoryInfo)
                Dim intinQty As Integer = 0
                SwList = SwCon.NmetalSampleWareInventory_Getlist(gluPM_M_Code.EditValue, gluM_Code.EditValue, som(i).PS_NO, Nothing, False, Nothing)
                If SwList.Count > 0 Then
                    intinQty = SwList(0).SWI_Qty
                    row("SE_EndQty") = intinQty
                End If

                ds.Tables("SamplePace").Rows.Add(row)
            Next
        End If
    End Sub


    Sub LoadData(ByVal StrSE_ID As String) '返回数据
        Dim i As Integer
        Dim som As New List(Of NmetalSamplePaceInfo)
        som = SamplePace.NmetalSamplePace_Getlist1(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, StrSE_ID, Nothing)
        If som.Count = 0 Then
            Exit Sub
        Else
            gluSO_ID.EditValue = som(0).SO_ID
            txtSS_Edition.Text = som(0).SS_Edition
            gluPM_M_Code.EditValue = som(0).PM_M_Code

            gluPM_M_Code_EditValueChanged(Nothing, Nothing)
            gluM_Code.EditValue = som(0).M_Name
            gluM_Code.Text = som(0).M_Code
            dateAddDate.Text = som(0).SE_AddDate

            LabSE_NO.Text = som(0).SE_ID
            CheckA.Checked = som(0).SE_Check
            LabAction.Text = LabAction.Text & som(0).SE_CheckActionName
            LabCheckDate.Text = LabCheckDate.Text & som(0).SE_CheckDate
            '-----------------------------------------
            ds.Tables("SamplePace").Clear()
            For i = 0 To som.Count - 1
                Dim row As DataRow
                row = ds.Tables("SamplePace").NewRow
                row("AutoID") = som(i).AutoID
                row("PS_Name") = som(i).PS_Name
                row("PS_NO") = som(i).PS_NO
                row("SE_PaceDescribe") = som(i).SE_PaceDescribe
                row("State") = som(i).State
                row("SE_Type") = som(i).SE_Type
                row("SE_Qty") = som(i).SE_Qty
                row("SE_AddDate") = som(i).SE_AddDate
                row("SPID") = som(i).SPID
                row("SE_OutInType") = som(i).SE_OutInType
                '----------------------------------------
                Dim SwCon As New NmetalSampleWareInventoryControler
                Dim SwList As New List(Of NmetalSampleWareInventoryInfo)
                Dim intinQty As Integer = 0
                SwList = SwCon.NmetalSampleWareInventory_Getlist(gluPM_M_Code.EditValue, gluM_Code.EditValue, som(i).PS_NO, Nothing, False, Nothing)
                If SwList.Count > 0 Then
                    intinQty = SwList(0).SWI_Qty
                    row("SE_EndQty") = intinQty
                End If
                '----------------------------------------
                ds.Tables("SamplePace").Rows.Add(row)
            Next

        End If
        '-----------------------------------------條碼採集表 
        Dim somlist As New List(Of NmetalSamplePaceInfo)
        somlist = SamplePace.NmetalSamplePaceBarCode_Getlist(Nothing, Nothing, StrSE_ID)
        If somlist.Count = 0 Then
            Exit Sub
        Else
            ds.Tables("SamplePaceBarCode").Clear()
            For i = 0 To somlist.Count - 1
                Dim row As DataRow
                row = ds.Tables("SamplePaceBarCode").NewRow
                row("AutoID") = somlist(i).AutoID
                row("Code_ID") = somlist(i).Code_ID
                row("ClientBarcode") = somlist(i).ClientBarcode
                row("SPID") = somlist(i).SPID
                row("Qty") = somlist(i).Qty
                row("SE_AddDate") = somlist(i).SE_AddDate

                ds.Tables("SamplePaceBarCode").Rows.Add(row)
            Next
        End If
    End Sub
#End Region

#Region "新增程序"
    ''' <summary>
    ''' 新增程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataNew() '新增
        Dim SPPI As New NmetalSamplePaceInfo
        SPPI.M_Code = gluM_Code.EditValue
        SPPI.PM_M_Code = gluPM_M_Code.EditValue
        SPPI.SS_Edition = txtSS_Edition.Text
        SPPI.SO_ID = gluSO_ID.EditValue
        SPPI.SE_AddUserID = InUserID

        LabSE_NO.Text = GetSE_ID()
        SPPI.SE_ID = LabSE_NO.Text
        If Len(LabSE_NO.Text) <= 0 Then
            MsgBox("獲取流水號失敗!")
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To ds.Tables("SamplePace").Rows.Count - 1
            With ds.Tables("SamplePace")
                Dim strPB As String = IIf(IsDBNull(.Rows(i)("SPID")), Nothing, .Rows(i)("SPID"))

                SPPI.PS_NO = IIf(IsDBNull(.Rows(i)("PS_NO")), Nothing, .Rows(i)("PS_NO"))
                SPPI.PS_Name = IIf(IsDBNull(.Rows(i)("PS_Name")), Nothing, .Rows(i)("PS_Name"))
                SPPI.SE_PaceDescribe = IIf(IsDBNull(.Rows(i)("SE_PaceDescribe")), Nothing, .Rows(i)("SE_PaceDescribe"))
                SPPI.State = IIf(IsDBNull(.Rows(i)("State")), Nothing, .Rows(i)("State"))

                SPPI.SE_Type = IIf(IsDBNull(.Rows(i)("SE_Type")), Nothing, .Rows(i)("SE_Type"))
                SPPI.SE_Qty = IIf(IsDBNull(.Rows(i)("SE_Qty")), Nothing, .Rows(i)("SE_Qty"))
                SPPI.SE_OutInType = IIf(IsDBNull(.Rows(i)("SE_OutInType")), Nothing, .Rows(i)("SE_OutInType"))
                SPPI.SE_AddDate = Format(.Rows(i)("SE_AddDate"), "yyyy-MM-dd")
                Dim StrSPID As String = String.Empty

                StrSPID = GetSPID() '自動流水號
                SPPI.SPID = StrSPID
                If IIf(IsDBNull(.Rows(i)("PS_NO")), String.Empty, .Rows(i)("PS_NO")) <> String.Empty Then
                    If SamplePace.NmetalSamplePace_Add(SPPI) = False Then
                        MsgBox("添加失敗，请檢查原因！")
                        Exit Sub
                    Else

                        '----------------------------條碼採集

                        Dim M As Integer
                        For M = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                            With ds.Tables("SamplePaceBarCode")
                                Dim stSPID As String = IIf(IsDBNull(.Rows(M)("SPID")), Nothing, .Rows(M)("SPID"))
                                If strPB = stSPID Then
                                    Dim spBInfo As New NmetalSamplePaceInfo
                                    spBInfo.SE_ID = LabSE_NO.Text
                                    spBInfo.SPID = StrSPID
                                    spBInfo.Code_ID = IIf(IsDBNull(.Rows(M)("Code_ID")), Nothing, .Rows(M)("Code_ID"))

                                    spBInfo.ClientBarcode = IIf(IsDBNull(.Rows(M)("ClientBarcode")), Nothing, .Rows(M)("ClientBarcode"))

                                    spBInfo.Qty = 1
                                    spBInfo.SE_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                                    spBInfo.SE_AddUserID = InUserID
                                    If SamplePace.NmetalSamplePaceBarCode_Add(spBInfo) = False Then
                                        MsgBox("添加失敗，请檢查原因！")
                                        Exit Sub
                                    End If
                                End If
                            End With
                        Next
                        '----------------------------

                        Dim SampleMain As New NmetalSampleOrdersMainControler
                        Dim SampleMainInfo As New NmetalSampleOrdersMainInfo
                        SampleMainInfo.SO_ID = gluSO_ID.Text
                        SampleMainInfo.SO_State = "F.样办进度"
                        SampleMain.NmetalSampleOrdersMain_UpdateState(SampleMainInfo)

                    End If
                End If
            End With
        Next

        MsgBox("保存成功!")
        Me.Close()
    End Sub
#End Region

#Region "修改程序"
    ''' <summary>
    '''修改
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataEdit()
        '---------------------------删除子表处理
        If ds.Tables("DelSamplePace").Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ds.Tables("DelSamplePace").Rows.Count - 1
                SamplePace.NmetalSamplePace_Delete(ds.Tables("DelSamplePace").Rows(j)("AutoID")) '刪除当前选定的
            Next
        End If

        If ds.Tables("DelSamplePaceBarCode").Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ds.Tables("DelSamplePaceBarCode").Rows.Count - 1
                SamplePace.NmetalSamplePaceBarCode_Delete(ds.Tables("DelSamplePaceBarCode").Rows(j)("AutoID"), Nothing, Nothing) '刪除当前选定的
            Next
        End If
        '''''''''''''''''''''''''''主表删除
        Dim SPPI As New NmetalSamplePaceInfo
        SPPI.M_Code = gluM_Code.EditValue
        SPPI.PM_M_Code = gluPM_M_Code.EditValue
        SPPI.SS_Edition = txtSS_Edition.Text
        SPPI.SO_ID = gluSO_ID.EditValue
        SPPI.SE_ModifyDate = Format(Now, "yyyy/MM/dd")
        SPPI.SE_ModifyUserID = InUserID
        SPPI.SE_ID = LabSE_NO.Text

        Dim i As Integer
        For i = 0 To ds.Tables("SamplePace").Rows.Count - 1
            With ds.Tables("SamplePace")
                Dim strPB As String = IIf(IsDBNull(.Rows(i)("SPID")), Nothing, .Rows(i)("SPID"))

                SPPI.PS_NO = IIf(IsDBNull(.Rows(i)("PS_NO")), Nothing, .Rows(i)("PS_NO"))
                SPPI.PS_Name = IIf(IsDBNull(.Rows(i)("PS_Name")), Nothing, .Rows(i)("PS_Name"))
                SPPI.SE_PaceDescribe = IIf(IsDBNull(.Rows(i)("SE_PaceDescribe")), Nothing, .Rows(i)("SE_PaceDescribe"))
                SPPI.State = IIf(IsDBNull(.Rows(i)("State")), Nothing, .Rows(i)("State"))
                SPPI.SE_Type = IIf(IsDBNull(.Rows(i)("SE_Type")), Nothing, .Rows(i)("SE_Type"))
                SPPI.SE_Qty = IIf(IsDBNull(.Rows(i)("SE_Qty")), Nothing, .Rows(i)("SE_Qty"))
                SPPI.SE_AddDate = Format(.Rows(i)("SE_AddDate"), "yyyy-MM-dd")
                SPPI.SPID = IIf(IsDBNull(.Rows(i)("SPID")), Nothing, .Rows(i)("SPID"))
                SPPI.SE_OutInType = IIf(IsDBNull(.Rows(i)("SE_OutInType")), Nothing, .Rows(i)("SE_OutInType"))

                Dim StrSPID As String = IIf(IsDBNull(.Rows(i)("SPID")), Nothing, .Rows(i)("SPID"))

                If IIf(IsDBNull(.Rows(i)("PS_NO")), String.Empty, .Rows(i)("PS_NO")) <> String.Empty Then
                    If IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID")) > 0 Then
                        '主表修改处理
                        SPPI.AutoID = IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID"))
                        If SamplePace.NmetalSamplePace_Update(SPPI) = False Then
                            MsgBox("修改失敗，请檢查原因！")
                            Exit Sub
                        Else
                            '----------------------------條碼採集子表
                            'StrSPID = GetSPID()
                            SPPI.SPID = StrSPID
                            Dim M As Integer
                            For M = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                                With ds.Tables("SamplePaceBarCode")
                                    Dim stSPID As String = IIf(IsDBNull(.Rows(M)("SPID")), Nothing, .Rows(M)("SPID"))
                                    If strPB = stSPID And IIf(IsDBNull(.Rows(M)("AutoID")), 0, .Rows(M)("AutoID")) = 0 Then
                                        Dim spBInfo As New NmetalSamplePaceInfo
                                        spBInfo.SE_ID = LabSE_NO.Text
                                        spBInfo.SPID = StrSPID
                                        spBInfo.Code_ID = IIf(IsDBNull(.Rows(M)("Code_ID")), Nothing, .Rows(M)("Code_ID"))
                                        spBInfo.ClientBarcode = IIf(IsDBNull(.Rows(M)("ClientBarcode")), Nothing, .Rows(M)("ClientBarcode"))
                                        spBInfo.Qty = 1
                                        spBInfo.SE_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                                        spBInfo.SE_AddUserID = InUserID
                                        If SamplePace.NmetalSamplePaceBarCode_Add(spBInfo) = False Then
                                            MsgBox("添加失敗，请檢查原因！")
                                            Exit Sub
                                        End If
                                    End If
                                End With
                            Next
                            '----------------------------------
                        End If
                    Else
                        '主表修改新增处理
                        StrSPID = GetSPID()
                        SPPI.SPID = StrSPID
                        If SamplePace.NmetalSamplePace_Add(SPPI) = False Then
                            MsgBox("修改失敗，请檢查原因！")
                            Exit Sub
                        Else
                            '----------------------------條碼採集子表
                            Dim M As Integer
                            For M = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                                With ds.Tables("SamplePaceBarCode")
                                    Dim stSPID As String = IIf(IsDBNull(.Rows(M)("SPID")), Nothing, .Rows(M)("SPID"))
                                    If strPB = stSPID Then
                                        Dim spBInfo As New NmetalSamplePaceInfo
                                        spBInfo.SE_ID = LabSE_NO.Text
                                        spBInfo.SPID = StrSPID
                                        spBInfo.Code_ID = IIf(IsDBNull(.Rows(M)("Code_ID")), Nothing, .Rows(M)("Code_ID"))
                                        spBInfo.ClientBarcode = IIf(IsDBNull(.Rows(M)("ClientBarcode")), Nothing, .Rows(M)("ClientBarcode"))
                                        spBInfo.Qty = 1
                                        spBInfo.SE_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                                        spBInfo.SE_AddUserID = InUserID
                                        If SamplePace.NmetalSamplePaceBarCode_Add(spBInfo) = False Then
                                            MsgBox("添加失敗，请檢查原因！")
                                            Exit Sub
                                        End If
                                    End If
                                End With
                            Next
                            '----------------------------------
                        End If
                    End If
                End If
            End With
        Next

        MsgBox("保存成功!")
        Me.Close()
    End Sub
#End Region

#Region "是否為空"
    ''' <summary>
    ''' 保存数据前处理
    ''' </summary>
    ''' <remarks></remarks>

    Function DataCheckEmpty() As Integer
        If gluM_Code.EditValue = String.Empty Then
            MsgBox("產品類別不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluM_Code.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If
        If gluPM_M_Code.Text = String.Empty Then
            MsgBox("产品编号不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluPM_M_Code.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If txtSS_Edition.Text = String.Empty Then
            MsgBox("版本号不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            txtSS_Edition.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If gluSO_ID.Text = String.Empty Then
            MsgBox("订单编号不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluSO_ID.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If ds.Tables("SamplePace").Rows.Count = 0 Then
            MsgBox("子檔不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            DataCheckEmpty = 0
            Exit Function
        End If

        Dim i As Integer
        For i = 0 To ds.Tables("SamplePace").Rows.Count - 1
            Dim intQty = ds.Tables("SamplePace").Rows(i)("SE_Qty")
            If intQty <= 0 Then
                MsgBox("數量不能小于等于零！", 64, "提示")
                gridSampleSace.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If IsDBNull(ds.Tables("SamplePace").Rows(i)("SE_Type")) Then
                MsgBox("類型不能為空！", 64, "提示")
                gridSampleSace.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If

            If IsDBNull(ds.Tables("SamplePace").Rows(i)("SE_AddDate")) Then
                MsgBox("日期不能為空！", 64, "提示")
                gridSampleSace.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If IsDBNull(ds.Tables("SamplePace").Rows(i)("PS_NO")) Then
                MsgBox("工艺编号不能为空，请输入工艺编号！", 64, "提示")
                gridSampleSace.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If

            If IsDBNull(ds.Tables("SamplePace").Rows(i)("SE_OutInType")) Then
                MsgBox("收發類型不能為空，请输入收發類型！", 64, "提示")
                gridSampleSace.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If

            If IsDBNull(ds.Tables("SamplePace").Rows(i)("SE_PaceDescribe")) Then
                MsgBox("工艺描述不能为空，请输入工艺描述！", 64, "提示")
                gridSampleSace.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            '------------------------------------

            '------------------------------------
            Dim strSE_OutInType As String = ds.Tables("SamplePace").Rows(i)("SE_OutInType")
            Dim strPS_NO As String = ds.Tables("SamplePace").Rows(i)("PS_NO")
            Dim intSE_Qty As Integer = ds.Tables("SamplePace").Rows(i)("SE_Qty")


            If strSE_OutInType = "發料" Then
                Dim SwCon As New NmetalSampleWareInventoryControler
                Dim SwList As New List(Of NmetalSampleWareInventoryInfo)
                Dim intinQty As Integer = 0
                SwList = SwCon.NmetalSampleWareInventory_Getlist(Me.gluPM_M_Code.EditValue, gluM_Code.EditValue, strPS_NO, Nothing, False, Nothing)
                If SwList.Count > 0 Then
                    intinQty = SwList(0).SWI_Qty
                End If
                If intSE_Qty > intinQty Then
                    MsgBox("收發類型為發料時庫存小于發料數量！", 64, "錯誤")
                    gridSampleSace.Focus()
                    GridView1.FocusedRowHandle = i
                    DataCheckEmpty = 0
                    Exit Function
                End If
            End If
        Next

        For i = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            If EditItem = "Check" Then
                Dim strM_Code As String = ds.Tables("SamplePaceBarCode").Rows(i)("Code_ID")
                If SetBarCodeCollection(strM_Code, False) = False Then
                    Exit Function
                End If
            End If
        Next

        DataCheckEmpty = 1
    End Function
#End Region

#Region "表格新增事件"
    Private Sub tsmNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmNew.Click
        If gluPM_M_Code.EditValue = "" Then
            MsgBox("請選擇產編號!", 60, "提示")
            gluPM_M_Code.Select()
            Exit Sub
        End If

        If gluM_Code.EditValue = "" Then
            MsgBox("請選擇產品類型!", 60, "提示")
            gluM_Code.Select()
            Exit Sub
        End If

        tempValue = gluPM_M_Code.EditValue
        tempValue2 = gluM_Code.EditValue
        frmNmetalSamplePaceLoad.ShowDialog()
        '-------------------------------------
        Dim i, n As Integer
        Dim arr(n) As String
        arr = Split(tempValue3, ",")
        n = Len(Replace(tempValue3, ",", "," & "*")) - Len(tempValue3)
        For i = 0 To n
            If arr(i) = "" Then
                Exit Sub
            End If
            AddRow(arr(i))
        Next
        frmNmetalSamplePaceLoad.Dispose()
    End Sub

    Sub AddRow(ByVal strPS_NO As String)
        Dim PS As New NmetalSampleProcessControl
        Dim PL As New List(Of NmetalSampleProcessInfo)
        PL = PS.NmetalSampleProcessSub_GetList(Nothing, strPS_NO, Nothing, Nothing, Nothing, True)
        If PL.Count > 0 Then
            Dim row As DataRow
            row = ds.Tables("SamplePace").NewRow
            row("PS_Name") = PL(0).PS_Name
            row("PS_NO") = strPS_NO
            row("SE_PaceDescribe") = Nothing
            row("SE_Type") = "P01"
            row("SE_Qty") = 0
            row("SE_AddDate") = Format(Now, "yyyy-MM-dd")
            row("State") = "生產中"
            '----------------------------------------
            Dim SwCon As New NmetalSampleWareInventoryControler
            Dim SwList As New List(Of NmetalSampleWareInventoryInfo)
            Dim intinQty As Integer = 0
            SwList = SwCon.NmetalSampleWareInventory_Getlist(gluPM_M_Code.EditValue, gluM_Code.EditValue, strPS_NO, Nothing, False, Nothing)
            If SwList.Count > 0 Then
                intinQty = SwList(0).SWI_Qty
                row("SE_EndQty") = intinQty
            End If
            '----------------------------------------

            row("SPID") = Mid(CStr(GetSPID_DS() + 100001), 2)

            ds.Tables("SamplePace").Rows.Add(row)
            GridView2.MoveLast()
        End If
    End Sub


    Private Sub tsmDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmDelete.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(GridView1.GetSelectedRows(0), "AutoID")

        If DelTemp <> String.Empty Then
            Dim row As DataRow = ds.Tables("DelSamplePace").NewRow
            row("AutoID") = ds.Tables("SamplePace").Rows(GridView1.FocusedRowHandle)("AutoID")
            ds.Tables("DelSamplePace").Rows.Add(row)
        End If
        ds.Tables("SamplePace").Rows.RemoveAt(GridView1.GetSelectedRows(0))
        '--------------------------------------------
    End Sub


#End Region

#Region "控件事件"

    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim strSPID As String = String.Empty
        strSPID = GridView1.GetFocusedRowCellValue("SPID").ToString()
        Me.GridView9.ActiveFilterString = "[SPID] = '" & strSPID & "'"
    End Sub


    Function GetSPID_DS() As Integer
        Dim strPB_ID As Integer = 0
        If ds.Tables("SamplePace").Rows.Count = 0 Then

            GetSPID_DS = 1
        Else
            Dim i As Integer
            For i = 0 To ds.Tables("SamplePace").Rows.Count - 1
                Dim strTA As String = String.Empty
                If Len(ds.Tables("SamplePace").Rows(i)("SPID")) > 6 Then
                    strTA = Mid(ds.Tables("SamplePace").Rows(i)("SPID"), 7)
                Else
                    strTA = ds.Tables("SamplePace").Rows(i)("SPID")
                End If

                If strPB_ID < CInt(strTA) Then
                    strPB_ID = CInt(strTA)
                End If
            Next
        End If
        GetSPID_DS = strPB_ID
    End Function

    Private Sub gluPM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_M_Code.EditValueChanged
        On Error Resume Next
        gluM_Code.Properties.DisplayMember = "Type3ID"
        gluM_Code.Properties.ValueMember = "Type3ID"
        gluM_Code.Properties.DataSource = Spcon.NmetalSampleProcessMain_GetList2(Nothing, sender.text)
    End Sub
    ''' <summary>
    ''' 样办单号離开事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluSO_ID_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gluSO_ID.EditValueChanged
        If EditItem = "Add" Or EditItem = "AddPlan" Then
            If EditItem = "Edit" Or EditItem = "Add" Then
                txtSS_Edition.Text = GridView7.GetFocusedRowCellValue("SS_Edition")
            End If
            Dim strM As String

            If EditItem = "AddPlan" Then
                strM = gluSO_ID.EditValue
            Else
                strM = GridView7.GetFocusedRowCellValue("SO_ID")
            End If

            Dim mc As New NmetalSampleOrdersMainControler
            gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
            gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
            gluPM_M_Code.Properties.DataSource = mc.NmetalSampleOrdersMain_GetList(strM, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If

    End Sub
#End Region

#Region "自動流水"
    Function GetSE_ID() As String
        Dim oc As New NmetalSamplePaceControler
        Dim oi As New NmetalSamplePaceInfo
        Dim StrSE As String
        StrSE = "SE" & Format(Now, "yyMM")
        oi = oc.NmetalSamplePace_GetID(StrSE)
        If oi Is Nothing Then
            GetSE_ID = "SE" + Format(Now, "yyMM") + "0001"
        Else
            GetSE_ID = "SE" + Format(Now, "yyMM") + Mid(CInt(Mid(oi.SE_ID, 9)) + 1000000001, 7)
        End If
    End Function

    Function GetSPID() As String
        Dim oc As New NmetalSamplePaceControler
        Dim oi As New NmetalSamplePaceInfo
        Dim StrSE As String
        StrSE = "SP" & Format(Now, "yyMMdd")
        oi = oc.NmetalSamplePace_GetIDA(StrSE)
        If oi Is Nothing Then
            GetSPID = "SP" + Format(Now, "yyMMdd") + "0001"
        Else
            GetSPID = "SP" + Format(Now, "yyMMdd") + Mid(CInt(Mid(oi.SPID, 9)) + 100000000001, 9)
        End If
    End Function
#End Region

#Region "審核"
    Sub UpdateCheck()
        Dim SPPI As New NmetalSamplePaceInfo
        SPPI.SE_ID = LabSE_NO.Text
        SPPI.SE_Check = CheckA.Checked
        SPPI.SE_CheckDate = Format(Now, "yyyy-MM-dd HH:mm:ss")
        SPPI.SE_CheckAction = InUserID
        If SamplePace.NmetalSamplePace_UpdateCheck(SPPI) = False Then
            MsgBox("添加失敗，请檢查原因！")
            Exit Sub
        End If
        MsgBox("審核成功!")
        '----------------收發料扣賬處理----------------------------------------
        Dim i As Integer
        Dim strSE_OutInType As String = String.Empty
        Dim strPS_NO As String = String.Empty
        For i = 0 To ds.Tables("SamplePace").Rows.Count - 1
            strSE_OutInType = ds.Tables("SamplePace").Rows(i)("SE_OutInType")
            strPS_NO = ds.Tables("SamplePace").Rows(i)("PS_NO")
            Dim SwCon As New NmetalSampleWareInventoryControler
            Dim SwInfo As New NmetalSampleWareInventoryInfo

            Dim Sccon As New NmetalSampleCollectionControler

            Dim SwList As New List(Of NmetalSampleWareInventoryInfo)
            SwInfo.PM_M_Code = gluPM_M_Code.EditValue
            SwInfo.M_Code = Me.gluM_Code.EditValue
            SwInfo.PS_NO = ds.Tables("SamplePace").Rows(i)("PS_NO")

            SwInfo.ModifyUserID = InUserID
            SwInfo.ModifyDate = Format(Now, "yyyy/MM/dd")
            SwInfo.AddUserID = InUserID
            SwInfo.AddDate = Format(Now, "yyyy/MM/dd")
            Dim strSPID = ds.Tables("SamplePace").Rows(i)("SPID")

            '1---------------------------處理訂單編號與產品編號是否存在-------------------
            Dim somlistSet As New List(Of NmetalSamplePaceInfo)
            somlistSet = SamplePace.NmetalSamplePaceBarCode_Getlist(strSPID, Nothing, Nothing)
            If somlistSet.Count = 0 Then
                Exit Sub
            Else
                Dim m As Integer
                For m = 0 To somlistSet.Count - 1
                    '1.1
                    If SetBarCodeCollection(somlistSet(m).Code_ID, True) = False Then
                        Exit Sub
                    End If
                    '1.2產生新的客戶條碼
                    If somlistSet(m).ClientBarcode <> String.Empty Then
                        Dim SetSO_ID As String = gluSO_ID.EditValue
                        Dim SetPM_M_Code As String = Me.gluPM_M_Code.EditValue
                        Dim SetSS_Edition As String = txtSS_Edition.Text
                        Dim sclcom As New NmetalSampleCollectionControler
                        Dim objinfo As New NmetalSampleCollectionInfo


                        objinfo.Code_ID = somlistSet(m).ClientBarcode
                        objinfo.Qty = 1
                        objinfo.StatusType = ""
                        objinfo.Remark = ""
                        objinfo.PM_M_Code = SetPM_M_Code
                        objinfo.AddUserID = InUserID
                        objinfo.AddDate = Format(Now, "yyyy/MM/dd")
                        objinfo.PM_Type = String.Empty
                        objinfo.SO_ID = SetSO_ID
                        objinfo.SP_ID = String.Empty
                        objinfo.SS_Edition = SetSS_Edition
                        objinfo.BarcodeType = "[c].客户条码"
                        If sclcom.NmetalSampleCollection_Add(objinfo) = False Then
                            MessageBox.Show("新增错误!", "提示")
                            Exit Sub
                        End If

                    End If
                Next
            End If

            '2-----------------------------------------------------------收料庫存
            If strSE_OutInType = "發料" Then
                Dim intinQty As Integer = 0
                SwList = SwCon.NmetalSampleWareInventory_Getlist(Me.gluPM_M_Code.EditValue, gluM_Code.EditValue, strPS_NO, Nothing, False, Nothing)
                If SwList.Count > 0 Then
                    intinQty = SwList(0).SWI_Qty
                End If

                If CheckA.Checked = True Then
                    SwInfo.SWI_Qty = intinQty - CInt(ds.Tables("SamplePace").Rows(i)("SE_Qty"))
                ElseIf CheckA.Checked = False Then
                    SwInfo.SWI_Qty = intinQty + CInt(ds.Tables("SamplePace").Rows(i)("SE_Qty"))
                End If

                If SwCon.NmetalSampleWareInventory_Update(SwInfo) = False Then
                    MsgBox("發料扣賬失敗,請檢查原因!", MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If

                '3---------------------------采集條碼修改部門-------------------
                Dim somlist As New List(Of NmetalSamplePaceInfo)
                somlist = SamplePace.NmetalSamplePaceBarCode_Getlist(strSPID, Nothing, Nothing)
                If somlist.Count = 0 Then
                    Exit Sub
                Else
                    Dim m As Integer
                    For m = 0 To somlist.Count - 1

                        If Sccon.NmetalSampleCollection_UpdateC(somlist(m).Code_ID, "") = False Then
                            MsgBox("生產部門修改錯誤,請檢查原因!", MsgBoxStyle.Information, "提示")
                            Exit Sub
                        End If
                    Next
                End If

            End If
            '4-----------------------------------------------------------收料庫存
            If strSE_OutInType = "收料" Then
                Dim intinQty As Integer = 0
                SwList = SwCon.NmetalSampleWareInventory_Getlist(Me.gluPM_M_Code.EditValue, gluM_Code.EditValue, strPS_NO, Nothing, False, Nothing)
                If SwList.Count > 0 Then
                    intinQty = SwList(0).SWI_Qty
                End If

                If CheckA.Checked = True Then
                    SwInfo.SWI_Qty = intinQty + CInt(ds.Tables("SamplePace").Rows(i)("SE_Qty"))
                ElseIf CheckA.Checked = False Then
                    SwInfo.SWI_Qty = intinQty - CInt(ds.Tables("SamplePace").Rows(i)("SE_Qty"))
                End If

                If SwCon.NmetalSampleWareInventory_Update(SwInfo) = False Then
                    MsgBox("收料扣賬失敗,請檢查原因!", MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If

                '5-----------------------------------------采集條碼修改部門
                Dim somlist As New List(Of NmetalSamplePaceInfo)
                somlist = SamplePace.NmetalSamplePaceBarCode_Getlist(strSPID, Nothing, Nothing)
                If somlist.Count = 0 Then
                    Exit Sub
                Else
                    Dim m As Integer
                    For m = 0 To somlist.Count - 1
                        Dim spinfo As New List(Of NmetalSampleProcessInfo)
                        spinfo = Spcon.NmetalSampleProcessSub_GetList(Nothing, strPS_NO, Nothing, Nothing, Nothing, Nothing)
                        If spinfo.Count > 0 Then
                            Dim StrD_ID As String = spinfo(0).D_ID
                            If Sccon.NmetalSampleCollection_UpdateC(somlist(m).Code_ID, StrD_ID) = False Then
                                MsgBox("生產部門修改錯誤,請檢查原因!", MsgBoxStyle.Information, "提示")
                                Exit Sub
                            End If
                        End If
                    Next
                End If

            End If
        Next

        Me.Close()
    End Sub
#End Region

#Region "條碼事件"
    Private Sub txtM_Code_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyDown
        If GridView1.RowCount = 0 Then Exit Sub
        If e.KeyCode = Keys.Enter Then
            '1.條碼重複
            Dim strM_Code As String
            Dim i As Integer
            strM_Code = UCase(Me.txtM_Code.Text)
            For i = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                If strM_Code = ds.Tables("SamplePaceBarCode").Rows(i)("Code_ID") Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    lblCode.Text = "条码重复"
                    Exit Sub
                End If
            Next
            '--------------------------------------------------------------------------
            If SetBarCodeCollection(strM_Code, False) = False Then
                Exit Sub
            End If

            '--------------------------------------------------------------------------
            If Mid(cboCodeType.EditValue, 1, 3) <> "[C]" Then
            Else
                '2、客户条码是否关连
                Dim sccon As New NmetalSampleCollectionControler
                Dim sclist As New List(Of NmetalSampleCollectionInfo)
                sclist = sccon.NmetalSampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If sclist.Count > 0 Then
                    If sclist(0).ClientBarcode <> String.Empty Then
                        MessageBox.Show(sclist(0).Code_ID + "此編號條碼采集表中存在客户条码关连", "提示")
                        txtM_Code.Text = String.Empty
                        txtM_Code.Focus()
                        Exit Sub
                    End If
                End If

                Me.TextEdit1.Focus()
                Exit Sub
            End If

            Dim strCode As String
            Dim strQty As String
            Dim intIn As Integer
            Dim StrText As String
            StrText = txtM_Code.Text
            intIn = InStr(StrText, ",", CompareMethod.Text)
            If StrText = "" Then Exit Sub

            If intIn <= 0 Then
                strCode = StrText
                strQty = "1"
            Else
                strCode = Mid(StrText, 1, intIn - 1)
                strQty = Mid(StrText, intIn + 1, Len(StrText) - intIn)
            End If
            '----------------------------------------
            Dim row As DataRow
            row = ds.Tables("SamplePaceBarCode").NewRow
            row("Code_ID") = StrConv(UCase(strCode), vbNarrow)
            row("SPID") = GridView1.GetFocusedRowCellValue("SPID").ToString
            row("Qty") = 1
            row("SE_AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
            ds.Tables("SamplePaceBarCode").Rows.Add(row)
            txtM_Code.Text = ""
            '----------------------------------------
            Dim m As Integer = 0
            For i = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                If GridView1.GetFocusedRowCellValue("SPID") = ds.Tables("SamplePaceBarCode").Rows(i)("SPID") Then
                    m = m + 1
                End If
            Next
            GridView1.SetFocusedRowCellValue(Me.SE_Qty, m)
        End If
    End Sub

    Function SetBarCodeCollection(ByVal SetBarCode As String, ByVal SetBoolUpdate As Boolean) As Boolean
        Dim sclcom As New NmetalSampleCollectionControler
        Dim scllist As New List(Of NmetalSampleCollectionInfo)
        scllist = sclcom.NmetalSampleCollection_Getlist(Nothing, SetBarCode, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If scllist.Count > 0 Then
            '1------------如果產品編號為空or訂單編號為空----------
            Dim SetSO_ID As String = gluSO_ID.EditValue
            Dim SetPM_M_Code As String = Me.gluPM_M_Code.EditValue
            Dim SetSS_Edition As String = txtSS_Edition.Text
            If scllist(0).PM_M_Code = String.Empty Or scllist(0).SO_ID = String.Empty Then
                '-----------------------------------------------------------
                If SetBoolUpdate Then
                    Dim objinfo As New NmetalSampleCollectionInfo
                    objinfo.Remark = ""
                    objinfo.PM_M_Code = SetPM_M_Code
                    objinfo.AddUserID = InUserID
                    objinfo.AddDate = Format(Now, "yyyy/MM/dd")
                    objinfo.ModifyUserID = InUserID
                    objinfo.ModifyDate = Format(Now, "yyyy/MM/dd")
                    objinfo.Code_ID = SetBarCode
                    objinfo.PM_Type = String.Empty
                    objinfo.SO_ID = SetSO_ID
                    objinfo.SS_Edition = SetSS_Edition
                    objinfo.SP_ID = String.Empty

                    If sclcom.NmetalSampleCollection_Update(objinfo) = False Then
                        MessageBox.Show("修改错误!", "提示")
                        SetBarCodeCollection = False
                        Exit Function
                    End If
                End If
                '-----------------------------------------------------------
            Else
                Dim sccon As New NmetalSampleCollectionControler
                Dim sclist As New List(Of NmetalSampleCollectionInfo)
                sclist = sclcom.NmetalSampleCollection_Getlist(Nothing, SetBarCode, Nothing, SetSO_ID, Nothing, Nothing, False, Nothing, SetPM_M_Code, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If sclist.Count > 0 Then
                Else
                    MessageBox.Show(SetBarCode + "此編號條碼采集表中的訂單編號與產品編號不相同!", "提示")
                    SetBarCodeCollection = False
                    Exit Function
                End If
            End If
        Else
            MessageBox.Show(SetBarCode + "此編號條碼采集表不存在!", "提示")
            SetBarCodeCollection = False
            txtM_Code.Text = String.Empty
            txtM_Code.Focus()
            Exit Function
        End If
        SetBarCodeCollection = True
    End Function



    Private Sub cmdDelSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelSub.Click
        If GridView9.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView9.GetRowCellDisplayText(GridView9.GetSelectedRows(0), "AutoID")

        If DelTemp <> String.Empty Then
            Dim row As DataRow = ds.Tables("DelSamplePaceBarCode").NewRow
            row("AutoID") = ds.Tables("SamplePaceBarCode").Rows(GridView9.FocusedRowHandle)("AutoID")
            ds.Tables("DelSamplePaceBarCode").Rows.Add(row)
        End If
        ds.Tables("SamplePaceBarCode").Rows.RemoveAt(GridView9.GetSelectedRows(0))

        Dim m As Integer = 0
        Dim i As Integer = 0
        For i = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            If GridView1.GetFocusedRowCellValue("SPID") = ds.Tables("SamplePaceBarCode").Rows(i)("SPID") Then
                m = m + 1
            End If
        Next
        GridView1.SetFocusedRowCellValue(Me.SE_Qty, m)
    End Sub


    Private Sub cmdAddFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddFile.Click
        If GridView1.RowCount = 0 Then Exit Sub
        OpenFileDialog1.InitialDirectory = "c:\"
        OpenFileDialog1.Filter = "txt files (*.txt))|*.txt;"
        OpenFileDialog1.FilterIndex = 2
        OpenFileDialog1.RestoreDirectory = True

        Dim PathStr As String

        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ds.Tables("SamplePaceBarCode").Clear()
            PathStr = OpenFileDialog1.FileName

            Dim str As IO.StreamReader = New IO.StreamReader(PathStr, System.Text.Encoding.Default)
            Do Until str.EndOfStream
                Dim StrTemp As String
                Dim InstrA, i As Integer
                Dim strM_Code As String
                Dim boolCheck As Boolean = False
                StrTemp = str.ReadLine()
                InstrA = InStr(StrTemp, ",", CompareMethod.Text)

                If InstrA > 0 Then

                    Dim row As DataRow
                    row = ds.Tables("SamplePaceBarCode").NewRow
                    row("Code_ID") = UCase(Mid(StrTemp, 1, InstrA - 1))
                    row("SPID") = GridView1.GetFocusedRowCellValue("SPID").ToString
                    row("Qty") = 1
                    row("SE_AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
                    '去掉重複
                    strM_Code = UCase(Mid(StrTemp, 1, InstrA - 1))
                    For i = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                        If strM_Code = ds.Tables("SamplePaceBarCode").Rows(i)("Code_ID") Then
                            lblCode.Text = "條碼重複"
                            boolCheck = True
                        End If
                    Next


                    lblCode.Text = String.Empty
                    If boolCheck = False Then
                        ds.Tables("SamplePaceBarCode").Rows.Add(row)
                    End If
                End If
            Loop

            Dim n As Integer
            Dim m As Integer = 0
            For n = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                If GridView1.GetFocusedRowCellValue("SPID") = ds.Tables("SamplePaceBarCode").Rows(n)("SPID") Then
                    m = m + 1
                End If
            Next
            GridView1.SetFocusedRowCellValue(Me.SE_Qty, m)

            str.Close()
        End If
    End Sub

    Private Sub cboMRPType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCodeType.EditValueChanged
        Select Case Mid(cboCodeType.EditValue, 1, 3)
            Case "[A]"
                Label2.Visible = False
                TextEdit1.Visible = False
            Case "[B]"
                Label2.Visible = False
                TextEdit1.Visible = False
            Case "[C]"
                Label2.Visible = True
                TextEdit1.Visible = True
        End Select
    End Sub
    ''' <summary>
    '''     客戶條碼
    ''' </summary>
    Private Sub TextEdit1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextEdit1.KeyDown
        If GridView1.RowCount = 0 Then Exit Sub
        If e.KeyCode = Keys.Enter Then
            If txtM_Code.Text = String.Empty Then
                txtM_Code.Focus()
            End If
            '第一个条码输入是否重复
            Dim strM_Code As String = UCase(Me.txtM_Code.Text)
            Dim i As Integer
            For i = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                If strM_Code = ds.Tables("SamplePaceBarCode").Rows(i)("Code_ID") Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    lblCode.Text = "条码重复"
                    Exit Sub
                End If
            Next

            '第二个客户条码输入是否重复
            Dim strClientBarcode As String = UCase(Me.TextEdit1.Text)
            Dim N As Integer
            For N = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                If strClientBarcode = ds.Tables("SamplePaceBarCode").Rows(N)("ClientBarcode") Then
                    TextEdit1.Text = String.Empty
                    TextEdit1.Focus()
                    lblCode.Text = "条码重复"
                    Exit Sub
                End If
            Next

            '此客戶條碼<采集表>存在
            Dim sclcom As New NmetalSampleCollectionControler
            If sclcom.NmetalSampleCollection_GetID(UCase(Me.TextEdit1.Text)) = True Then
                lblCode.Text = "客戶条码重复"
                TextEdit1.Text = ""
                TextEdit1.Focus()
                MessageBox.Show(UCase(Me.TextEdit1.Text) + "此客戶條碼<采集表>存在!", "提示")
                Exit Sub
            End If

            Dim strCode As String
            Dim strQty As String
            Dim intIn As Integer
            Dim StrText As String
            Dim strCtext As String

            StrText = txtM_Code.Text
            strCtext = Me.TextEdit1.Text

            If TextEdit1.Text = String.Empty Then
                TextEdit1.Focus()
                Exit Sub
            End If

            intIn = InStr(StrText, ",", CompareMethod.Text)
            If StrText = "" Then Exit Sub
            If intIn <= 0 Then
                strCode = StrText
                strQty = "1"
            Else
                strCode = Mid(StrText, 1, intIn - 1)
                strQty = Mid(StrText, intIn + 1, Len(StrText) - intIn)
            End If

            Dim row As DataRow
            row = ds.Tables("SamplePaceBarCode").NewRow
            row("Code_ID") = StrConv(UCase(strCode), vbNarrow)
            row("ClientBarcode") = StrConv(UCase(strCtext), vbNarrow)
            row("SPID") = GridView1.GetFocusedRowCellValue("SPID").ToString
            row("Qty") = 1
            row("SE_AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
            ds.Tables("SamplePaceBarCode").Rows.Add(row)
            txtM_Code.Text = ""
            '----------------------------------------
            Dim m As Integer = 0
            For i = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                If GridView1.GetFocusedRowCellValue("SPID") = ds.Tables("SamplePaceBarCode").Rows(i)("SPID") Then
                    m = m + 1
                End If
            Next
            GridView1.SetFocusedRowCellValue(Me.SE_Qty, m)

            '----------------------------------------清空控件
            txtM_Code.Text = String.Empty
            TextEdit1.Text = String.Empty
            txtM_Code.Focus()

        End If
    End Sub
#End Region

    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。

    End Sub
End Class