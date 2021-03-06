Imports LFERP.Library.ProductProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSamplePace
Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersMain
Imports LFERP.Library.NmetalSampleManager.NmetalSampleCollection
Imports LFERP.Library.NmetalSampleManager.NmetalSampleWareInventory
Imports LFERP.Library.PieceProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleTransaction
Imports LFERP.Library.ProductionController
Imports LFERP.SystemManager
Imports LFERP.Library.NmetalSampleManager.NmetalSamplePacking
Imports LFERP.Library.NmetalSampleManager.NmetalSampleStorage
Imports LFERP.Library.NmetalSampleManager.NmetalSampleSetting
Imports LFERP.DataSetting
Imports LFERP.Library.NmetalSampleManager.NmetalSamplePaceTypeBriName
Imports System.Threading


Public Class frmNmetalSamplePaceInsert

#Region "属性"
    Dim ds As New DataSet
    Dim pmcon As New ProcessMainControl
    Dim spCon As New NmetalSamplePaceControler
    Dim prcon As New NmetalSampleProcessControl
    Dim pncon As New PersonnelControl
    Dim SwCon As New NmetalSampleWareInventoryControler
    Dim pacon As New NmetalSamplePaceControler
    Dim Sccon As New NmetalSampleCollectionControler
    Dim stcon As New NmetalSampleTransactionControler
    Dim pfcon As New ProductionFieldControl
    Dim spkcon As New NmetalSamplePackingController
    Dim stocon As New NmetalSampleStorageController
    Dim dtCon As New DepartmentControler
    Dim nsdwu As New NmetalSampleDepWeightControler

    Private boolSE_Check As Boolean
    Private boolSE_InCheck As Boolean
    Private _EditBooL As Boolean = True
    Private _EditItem As String '属性栏位
    Private _GetEditValue As String
    Private _EditSE_ID As String
    Private _EditDep As String
    Private StrSE_OutCardID As String '借出人员

    Private StrPortName As String  '设置端口号
    Private StrBaudRate As Integer '设置波特率
    Private bitWeighing As Boolean '是否有称

    Dim pmlist As New List(Of PersonnelInfo) '部門分享

    Dim ClickJS As Integer = 0

    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Property EditSE_ID() As String '属性
        Get
            Return _EditSE_ID
        End Get
        Set(ByVal value As String)
            _EditSE_ID = value
        End Set
    End Property

    Property GetEditValue() As String '属性
        Get
            Return _GetEditValue
        End Get
        Set(ByVal value As String)
            _GetEditValue = value
        End Set
    End Property

    Property EditDep() As String '属性
        Get
            Return _EditDep
        End Get
        Set(ByVal value As String)
            _EditDep = value
        End Set
    End Property

    Property EditBooL() As Boolean  '属性
        Get
            Return _EditBooL
        End Get
        Set(ByVal value As Boolean)
            _EditBooL = value
        End Set
    End Property
#End Region

#Region "窗体载入"
    Private Sub frmSamplePaceInsert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        PowerUser()

        Dim msi As New List(Of NmetalSampleSettingInfo)
        Dim msc As New NmetalSampleSettingController
        msi = msc.NmetalSampleSetting_GetList(InUserID)


        pmlist = pncon.FacBriSearch_GetList("V", Nothing, Nothing, Nothing)
        gluSE_InD_ID.Properties.DisplayMember = "DepName"
        gluSE_InD_ID.Properties.ValueMember = "DepID"
        gluSE_InD_ID.Properties.DataSource = pmlist

        'gluType.Properties.ValueMember = "SE_Type"
        'gluType.Properties.DisplayMember = "SE_TypeName"
        ' gluType.Properties.DataSource = spCon.NmetalSamplePaceType_Getlist(Nothing)
        '----------------------------------------------------------------------------------------
        '載入訂單編號
        Dim mtd As New NmetalSampleOrdersMainControler
        'gluSO_ID.Properties.DisplayMember = "SO_ID"
        'gluSO_ID.Properties.ValueMember = "SO_ID"
        gluSO_ID.Properties.DataSource = mtd.NmetalSampleOrdersMain_GetListItem(Nothing, Nothing, Nothing, True)

        If EditItem <> EditEnumType.OUT Then
            Dim mc As New NmetalSampleOrdersMainControler
            'gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
            'gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
            gluPM_M_Code.Properties.DataSource = mc.NmetalSampleOrdersMain_GetList(gluSO_ID.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If

        Select Case EditItem
            Case EditEnumType.OUT
                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.OUT)
                CreateDepNameTable(True)
                CreatePaceType(InUserID)
                gluSE_OutD_ID.Properties.DataSource = ds.Tables("SampleDepName")

                dateAddDate.EditValue = Format(Now, "yyyy/MM/dd")
                gluSE_OutD_ID.EditValue = EditDep
                Panel1.Visible = True
                lblCheckName.Text = InUserID
                lblCheckDate.Text = Format(Now, "yyyy/MM/dd HH:mm:ss")

                txtPK_CodeID.Enabled = False
                txtSealCode_ID.Enabled = False


            Case EditEnumType.INCHECK
                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.INCHECK)
                CreateDepNameTable(False)
                CreatePaceType(Nothing)
                gluSE_OutD_ID.Properties.DataSource = ds.Tables("SampleDepName")

                LoadDataSource()
                gluSO_ID.Enabled = False
                gluPM_M_Code.Enabled = False
                gluM_Code.Enabled = False
                gluType.Enabled = False

                gluSE_InD_ID.Enabled = False
                gluSE_OutD_ID.Enabled = False
                gluOutPS_NO.Enabled = False
                gluInPS_NO.Enabled = False
                cboCodeType.Enabled = False
                CheckEdit1.Visible = True
                Label4.Visible = False
                gluLoan.Visible = False
                gluReason.Enabled = False
                txtTime.Enabled = False
                txtOutWeighing.Enabled = False
                txtSE_Remark.Enabled = False

                txtPK_CodeID.Enabled = True
                txtSealCode_ID.Enabled = True
                cmdDelSub.Enabled = False
                cobSE_BarCodeType.Enabled = False
                txtOutInQty.Enabled = False
                LoadData(GetEditValue)

                Dim sptlist As New List(Of NmetalSamplePaceInfo)
                sptlist = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
                If sptlist.Count > 0 Then
                    If EditItem = EditEnumType.INCHECK And sptlist(0).SE_InCheckBarcode = False Then
                        txtPK_CodeID.Enabled = False
                        txtSealCode_ID.Enabled = False
                    End If
                End If
                colErrorRate.Visible = True


            Case EditEnumType.CHECK

                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.CHECK)
                CreateDepNameTable(False)
                CreatePaceType(Nothing)
                gluSE_OutD_ID.Properties.DataSource = ds.Tables("SampleDepName")
                LoadDataSource()
                gluSO_ID.Enabled = False
                gluPM_M_Code.Enabled = False
                gluM_Code.Enabled = False
                gluType.Enabled = False

                gluSE_InD_ID.Enabled = False
                gluSE_OutD_ID.Enabled = False
                gluOutPS_NO.Enabled = False
                gluInPS_NO.Enabled = False

                txtM_Code.Enabled = False
                txtSE_Remark.Enabled = False

                Panel1.Visible = True
                cmdDelSub.Enabled = False
                gluReason.Enabled = False
                txtTime.Enabled = False
                txtOutWeighing.Enabled = False
                txtPK_CodeID.Enabled = False
                txtSealCode_ID.Enabled = False
                cobSE_BarCodeType.Enabled = False
                txtOutInQty.Enabled = False
                LoadData(GetEditValue)



            Case EditEnumType.EDIT

                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.EDIT)
                CreateDepNameTable(True)
                CreatePaceType(InUserID)
                gluSE_OutD_ID.Properties.DataSource = ds.Tables("SampleDepName")

                LoadDataSource()
                gluSO_ID.Enabled = False
                gluPM_M_Code.Enabled = False
                gluM_Code.Enabled = False
                gluType.Enabled = False

                gluReason.Enabled = False
                txtTime.Enabled = False
                txtOutWeighing.Enabled = False
                Panel1.Visible = True
                txtPK_CodeID.Enabled = False
                txtSealCode_ID.Enabled = False
                cobSE_BarCodeType.Enabled = False
                LoadData(GetEditValue)



            Case EditEnumType.VIEW

                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.VIEW)
                CreateDepNameTable(False)
                CreatePaceType(Nothing)
                gluSE_OutD_ID.Properties.DataSource = ds.Tables("SampleDepName")
                LoadDataSource()
                txtM_Code.Enabled = False
                Panel1.Visible = True
                CheckEdit1.Visible = True
                cmdDelSub.Enabled = False
                LoadData(GetEditValue)
                cmdSave.Visible = False

                gluReason.Enabled = False
                txtTime.Enabled = False
                txtOutWeighing.Enabled = False
                txtPK_CodeID.Enabled = False
                txtSealCode_ID.Enabled = False
        End Select
        Me.Text = Lbl_Title.Text

        Try
            '李超
            Dim reset As New ResetPassWords.SetPassWords
            reset.SetPassWords()
        Catch
        End Try
    End Sub

    
    Sub LoadDataSource()
        Dim splist As New List(Of NmetalSampleProcessInfo)
        splist = prcon.NmetalSampleProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, Nothing, Me.gluM_Code.EditValue, Nothing, True, Nothing)
        'gluOutPS_NO.Properties.ValueMember = "PS_NO"
        'gluOutPS_NO.Properties.DisplayMember = "PS_Name"
        gluOutPS_NO.Properties.DataSource = splist
    End Sub
#End Region

#Region "設置各控件的Enable屬性"
    Private Sub SetControlEnable(ByVal a As Boolean, ByVal b As Boolean)
        Panel3.Enabled = a
        gluSE_OutD_ID.Enabled = a
        gluSE_InD_ID.Enabled = a
        gluOutPS_NO.Enabled = a
        gluInPS_NO.Enabled = a
    End Sub
#End Region


#Region "载入数据"
    Sub LoadData(ByVal StrSE_ID As String)
        Dim som As New List(Of NmetalSamplePaceInfo)
        som = spCon.NmetalSamplePace_Getlist(StrSE_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If som.Count = 0 Then
            Exit Sub
        Else
            gluSO_ID.EditValue = som(0).SO_ID
            txtSS_Edition.Text = som(0).SS_Edition
            gluPM_M_Code.EditValue = som(0).PM_M_Code
            gluM_Code.EditValue = som(0).M_Code
            gluType.EditValue = som(0).SE_Type
            gluSE_OutD_ID.EditValue = som(0).SE_OutD_ID
            gluOutPS_NO.EditValue = som(0).SE_OutPS_NO
            dateAddDate.EditValue = som(0).SE_AddDate
            txtOutInQty.Text = som(0).SE_Qty
            gluSE_InD_ID.EditValue = som(0).SE_InD_ID
            gluInPS_NO.EditValue = som(0).SE_InPS_NO
            LabSE_NO.Text = som(0).SE_ID
            boolSE_Check = som(0).SE_Check
            boolSE_InCheck = som(0).SE_InCheck
            CheckA.Checked = som(0).SE_Check
            CheckEdit1.Checked = som(0).SE_InCheck
            gluLoan.EditValue = som(0).SE_LoanID
            gluReason.EditValue = som(0).SE_BorrowType
            txtSE_Remark.Text = som(0).SE_Remark
            txtMaterialType.Text = som(0).MaterialTypeName
            cobSE_BarCodeType.EditValue = som(0).SE_BarCodeType
            txtTime.Text = som(0).SE_BorrowTime
            txtOutWeighing.Text = som(0).OutWeighing
            txtInWeighing.Text = som(0).InWeighing

            txtOutQty.Text = som(0).OutQtying
            txtInQty.Text = som(0).InQtying


            If Me.EditItem <> EditEnumType.INCHECK Then '显示刷卡人
                If som(0).PK_Code_ID = String.Empty Then
                    txtPK_CodeID.Enabled = False
                    txtSealCode_ID.Enabled = False
                Else
                    txtPK_CodeID.Text = som(0).PK_Code_ID
                    txtSealCode_ID.Text = som(0).SealCode_ID
                End If
            Else
                If som(0).PK_Code_ID = String.Empty Then
                    txtPK_CodeID.Enabled = False
                Else
                    txtM_Code.Enabled = False
                End If
                If som(0).SealCode_ID = String.Empty Then
                    txtSealCode_ID.Enabled = False
                End If
            End If

            If som(0).SE_CheckAction = Nothing Then
                lblCheckName.Text = InUserID
            Else
                lblCheckName.Text = som(0).SE_CheckAction
            End If

            If som(0).SE_CheckDate = Nothing Then
                lblCheckDate.Text = Format(Now, "yyyy/MM/dd HH:mm:ss")
            Else
                lblCheckDate.Text = som(0).SE_CheckDate
            End If
            '------------------------------------------------------
            If Me.EditItem = EditEnumType.VIEW Then '显示刷卡人
                If som(0).SE_OutCardID <> String.Empty Then
                    txtCardID.Text = "发:" + som(0).SE_OutCardID
                Else
                    txtCardID.Text = String.Empty
                End If

                If som(0).SE_InCardID <> String.Empty Then
                    txtCardID.Text = txtCardID.Text + " 收:" + som(0).SE_InCardID
                Else
                    txtCardID.Text = txtCardID.Text + String.Empty
                End If
                If EditBooL = False Then
                    txtSealCode_ID.Text = String.Empty
                End If

                ''异常收料显示
                Dim TempStr As String
                If som(0).SE_OutCardID <> String.Empty Then
                    TempStr = "发:" + som(0).SE_OutYCCardID
                Else
                    TempStr = String.Empty
                End If

                If som(0).SE_InCardID <> String.Empty Then
                    TempStr = TempStr + " 收:" + som(0).SE_InYCCardID
                Else
                    TempStr = TempStr + String.Empty
                End If
                txtYiChangCard.Text = TempStr
                txtYiChangCard.ToolTip = TempStr

            End If
            ''------------------------------------------------------自动条码是否需要确认
            'If som(0).SE_BarCodeType = "自动采集" And EditItem = EditEnumType.INCHECK Then

            'End If

            Dim BoolSE_IsRweight As Boolean = False
            Dim sptlist As New List(Of NmetalSamplePaceInfo)
            sptlist = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
            If sptlist.Count > 0 Then
                BoolSE_IsRweight = sptlist(0).SE_IsRweight  ''入库
            End If
            '-----------------------------------------条码采集数据
            Dim somlist As New List(Of NmetalSamplePaceInfo)
            somlist = spCon.NmetalSamplePaceBarCode_Getlist(Nothing, Nothing, EditSE_ID)
            If somlist.Count = 0 Then
                Exit Sub
            Else
                ds.Tables("SamplePaceBarCode").Clear()
                Dim i As Integer
                For i = 0 To somlist.Count - 1
                    Dim row As DataRow
                    row = ds.Tables("SamplePaceBarCode").NewRow
                    row("AutoID") = somlist(i).AutoID
                    row("Code_ID") = somlist(i).Code_ID
                    row("ClientBarcode") = somlist(i).ClientBarcode

                    If i = 0 Then '李超修改
                        If somlist(i).ClientBarcode <> String.Empty Then
                            cboCodeType.SelectedIndex = 1
                        Else
                            cboCodeType.SelectedIndex = 0
                        End If
                    End If

                    row("SPID") = somlist(i).SPID

                    row("SE_AddDate") = Format(CDate(somlist(i).SE_AddDate), "yyyy/MM/dd HH:mm:ss")
                    row("InCheck") = somlist(i).InCheck
                    If somlist(i).InCheck = False Then
                        row("InCheckDate") = Nothing
                    Else
                        row("InCheckDate") = Format(CDate(somlist(i).InCheckDate), "yyyy/MM/dd HH:mm:ss")
                    End If

                    '2014-07-03
                    row("OutQty") = somlist(i).OutQty
                    row("OutWeight") = somlist(i).OutWeight
                    row("InQty") = somlist(i).InQty
                    row("InWeight") = somlist(i).InWeight


                    '查询出每个条码的重量,记录发出前数量-------------------------
                    Dim coll As New List(Of NmetalSampleCollectionInfo)
                    coll = Sccon.NmetalSampleCollection_Getlist(Nothing, somlist(i).Code_ID, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    If coll.Count > 0 Then
                        row("TWeight") = coll(0).TWeight
                        row("IWeight") = coll(0).IWeight

                        row("OutErrorRate") = FormatNumber((coll(0).TWeight - somlist(i).OutWeight), 3, TriState.True)

                        If somlist(i).InWeight > 0 Then
                            If BoolSE_IsRweight = True Then
                                row("ErrorRate") = FormatNumber(coll(0).IWeight - somlist(i).InWeight, 3, TriState.True)
                            Else
                                row("ErrorRate") = FormatNumber(somlist(i).OutWeight - somlist(i).InWeight, 3, TriState.True)
                            End If
                        Else
                            row("ErrorRate") = 0
                        End If
                    End If
                    '-------------------------------------------------------------
                    ds.Tables("SamplePaceBarCode").Rows.Add(row)
                Next
            End If
            cboCodeType.SelectedIndex = som(0).SE_CodeType
        End If
    End Sub
#End Region

#Region "审核"
    Sub UpdateCheck(ByVal boolMess As Boolean)
        If boolMess Then
            If CheckA.Checked = boolSE_Check Then
                MsgBox("审核状态没有改变，提示！")
                Exit Sub
            End If
        End If

        '-------------------------------------------
        Dim SPPI As New NmetalSamplePaceInfo
        SPPI.SE_ID = LabSE_NO.Text
        SPPI.SE_Check = CheckA.Checked
        SPPI.SE_CheckDate = Format(Now, "yyyy-MM-dd HH:mm:ss")
        SPPI.SE_CheckAction = InUserID

        SPPI.OutDwonRate = Val(txtOutDwonRate.Text)
        SPPI.OutUpRate = Val(txtOutUpRate.Text)

        If spCon.NmetalSamplePace_UpdateCheck(SPPI) = False Then
            MsgBox("添加失敗，请檢查原因！")
            Exit Sub
        End If

        If boolMess Then
            If CheckA.Checked Then
                MsgBox("審核成功!")
            Else
                MsgBox("取消審核成功!")
            End If
            Me.Close()
        End If
    End Sub
#End Region

#Region "收料确认"

    Sub UpdateInCheck()
        Me.ControlBox = False

        If CheckEdit1.Checked = boolSE_InCheck Then
            MsgBox("收料确认状态没有改变，提示！")
            Exit Sub
        End If
        ''只能点击一次-----------------------------
        If ClickJS = 0 Then
            ClickJS = ClickJS + 1
        Else
            Exit Sub
        End If
        ''-----------------------------------------

        Panel7.Visible = True '--显示进度条
        ProgressBar1.Value = 0
        Me.lbl_ProgressBar.Text = "进度显示....(收发确认中)"

        ProgressBar1.Value = 10 '--显示进度值
        Me.lbl_ProgressBar.Text = "进度显示(10%)....(收发确认中)"

        '1-----------------------------------------审核处理
        Dim SPPI As New NmetalSamplePaceInfo
        SPPI.SE_ID = LabSE_NO.Text
        SPPI.SE_InCheck = CheckEdit1.Checked
        SPPI.SE_IncheckAction = InUserID
        SPPI.SE_InCardID = Me.txtCardID.Text
        '+贵金金属加
        SPPI.InQtying = txtInQty.Text
        SPPI.InWeighing = txtInWeighing.Text
        SPPI.SE_InYCCardID = txtYiChangCard.Text
        SPPI.InDwonRate = Val(txtInDwonRate.Text)
        SPPI.InUpRate = Val(txtInUpRate.Text)

        If spCon.NmetalSamplePace_UpdateInCheck(SPPI) = False Then
            MsgBox("添加失敗，请檢查原因！")
            Exit Sub
        End If

        ProgressBar1.Value = 20 '--显示进度条
        Me.lbl_ProgressBar.Text = "进度显示(20%)....(收发确认中)"

        Dim m As Integer
        ''本單節余--------------------------------
        Dim TempOutWeighingEnd As Decimal = 0
        Dim TempInWeighingEnd As Decimal = 0
        Dim TempOutQtyingEnd As Int16 = 0
        Dim TempInQtyingEnd As Int16 = 0
        '2.-----------------------------------------扣账处理---+條碼確認
        Dim sptlist As New List(Of NmetalSamplePaceInfo)
        sptlist = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
        If sptlist.Count > 0 Then
            ProgressBar1.Value = 30 '--显示进度条
            Me.lbl_ProgressBar.Text = "进度显示(30%)....(收发检查数据中)"
            '2.1是否要輸入確認條碼
            If sptlist(0).SE_InCheckBarcode Then
                For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                    With ds.Tables("SamplePaceBarCode")
                        Dim spBInfo As New NmetalSamplePaceInfo
                        spBInfo.InCheck = IIf(IsDBNull(.Rows(m)("InCheck")), False, .Rows(m)("InCheck"))
                        spBInfo.AutoID = IIf(IsDBNull(.Rows(m)("AutoID")), 0, .Rows(m)("AutoID"))
                        spBInfo.InCheckDate = IIf(IsDBNull(.Rows(m)("InCheckDate")), Nothing, .Rows(m)("InCheckDate"))
                        spBInfo.InQty = IIf(IsDBNull(.Rows(m)("InQty")), 0, .Rows(m)("InQty"))
                        spBInfo.InWeight = IIf(IsDBNull(.Rows(m)("InWeight")), 0, .Rows(m)("InWeight"))

                        '查询出每个条码的重量,记录发出前数量-------------------------
                        Dim coll As New List(Of NmetalSampleCollectionInfo)
                        coll = Sccon.NmetalSampleCollection_Getlist(Nothing, .Rows(m)("Code_ID").ToString, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                        If coll.Count > 0 Then
                            spBInfo.OutEndQty = coll(0).Qty
                            spBInfo.OutEndWeight = coll(0).TWeight
                        End If
                        '-------------------------------------------------------------
                        If spCon.NmetalSamplePaceBarCode_Update(spBInfo) = False Then
                            MsgBox("添加失敗，请檢查原因！")
                            Exit Sub
                        End If

                    End With
                Next
            Else
                '改为批量更新
                If spCon.NmetalSamplePaceBarCode_UpdateEndBatch(LabSE_NO.Text, sptlist(0).SE_IsRweight) = False Then
                    MsgBox("更新结余数失败!")
                End If

                ''發料時,只更新一下節余數
                'For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                '    With ds.Tables("SamplePaceBarCode")
                '        Dim spBInfo As New NmetalSamplePaceInfo
                '        '查询出每个条码的重量,记录发出前数量-------------------------
                '        Dim coll As New List(Of NmetalSampleCollectionInfo)
                '        coll = Sccon.NmetalSampleCollection_Getlist(Nothing, .Rows(m)("Code_ID").ToString, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                '        If coll.Count > 0 Then
                '            spBInfo.OutEndQty = coll(0).Qty
                '            spBInfo.OutEndWeight = coll(0).TWeight
                '        End If
                '        spBInfo.AutoID = IIf(IsDBNull(.Rows(m)("AutoID")), 0, .Rows(m)("AutoID"))

                '        If spCon.NmetalSamplePaceBarCode_UpdateEnd(spBInfo) = False Then
                '            MsgBox("添加失敗，请檢查原因！")
                '            Exit Sub
                '        End If
                '    End With
                'Next

            End If

            ProgressBar1.Value = 40 '--显示进度条
            Me.lbl_ProgressBar.Text = "进度显示(30%)....(收发扣账处理中请等待)"
            '3.--------------------------扣账发料-------------------------------------------------------
            If sptlist(0).SE_OutVisible Then
                Dim SwInfo As New NmetalSampleWareInventoryInfo
                Dim SwList As New List(Of NmetalSampleWareInventoryInfo)
                Dim intinQty As Integer = 0
                Dim DoubleWeight As Decimal = 0

                SwList = SwCon.NmetalSampleWareInventory_Getlist(Nothing, Nothing, gluOutPS_NO.EditValue, Nothing, False, gluSE_OutD_ID.EditValue)
                If SwList.Count > 0 Then
                    intinQty = SwList(0).SWI_Qty
                    DoubleWeight = SwList(0).SWI_Weight
                End If

                If CheckEdit1.Checked = True Then
                    SwInfo.SWI_Qty = intinQty - CInt(txtOutInQty.Text)
                    SwInfo.SWI_Weight = DoubleWeight - Val(txtOutWeighing.Text)
                ElseIf CheckEdit1.Checked = False Then
                    SwInfo.SWI_Qty = intinQty + CInt(txtOutInQty.Text)
                    SwInfo.SWI_Weight = DoubleWeight + Val(txtOutWeighing.Text)
                End If

                TempOutQtyingEnd = intinQty  '取得最后節余
                TempOutWeighingEnd = DoubleWeight

                SwInfo.ModifyDate = Format(Now, "yyyy/MM/dd")
                SwInfo.ModifyUserID = InUserID

                SwInfo.D_ID = gluSE_OutD_ID.EditValue
                SwInfo.PS_NO = gluOutPS_NO.EditValue

                If SwCon.NmetalSampleWareInventory_Update(SwInfo) = False Then
                    MsgBox("發料扣賬失敗,請檢查原因!", MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If

                '3.1-----------------------------------------采集條碼修改部門
                If CheckEdit1.Checked = True Then
                    Dim strStatusType As String = String.Empty
                    Dim somlist As New List(Of NmetalSamplePaceInfo)
                    somlist = pacon.NmetalSamplePaceBarCode_Getlist(Nothing, Nothing, LabSE_NO.Text)
                    If somlist.Count = 0 Then
                        Exit Sub
                    Else
                        Dim strOutPS_NO As String = Nothing
                        Dim sptlistB As New List(Of NmetalSamplePaceInfo)
                        sptlistB = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
                        If sptlistB.Count > 0 Then
                            strStatusType = sptlistB(0).StatusType

                            '完工状态,不更新库存工序编号
                            If sptlistB(0).SE_IsComplete = True Then
                                strOutPS_NO = Nothing
                            Else
                                strOutPS_NO = gluOutPS_NO.EditValue
                            End If
                        End If
                        '根具单号修改部门与状态
                        ''For m = 0 To somlist.Count - 1
                        ''    '修改部門
                        ''    If Sccon.NmetalSampleCollection_UpdateG(somlist(m).Code_ID, strStatusType, gluSE_OutD_ID.EditValue, somlist(m).OutWeight, strOutPS_NO) = False Then
                        ''        MessageBox.Show("修改条码状态或部門错误!", "提示")
                        ''        Exit Sub
                        ''    End If
                        ''Next
                        If Sccon.NmetalSampleCollection_UpdateGOut(LabSE_NO.Text, strStatusType, gluSE_OutD_ID.EditValue, strOutPS_NO) = False Then
                            MessageBox.Show("修改条码状态或部門错误!", "提示")
                            Exit Sub
                        End If

                    End If
                End If
            End If
            ProgressBar1.Value = 70 '--显示进度条
            Me.lbl_ProgressBar.Text = "进度显示(70%)....(收发入账处理中请等待)"
            '4---入账收料-------------------------------------------------------------------
            If sptlist(0).SE_InVisible Then
                Dim SwInfo As New NmetalSampleWareInventoryInfo
                Dim SwList As New List(Of NmetalSampleWareInventoryInfo)
                Dim intinQty As Integer = 0
                Dim DoubleWeight As Decimal = 0

                SwList = SwCon.NmetalSampleWareInventory_Getlist(Nothing, Nothing, gluInPS_NO.EditValue, Nothing, False, gluSE_InD_ID.EditValue)
                If SwList.Count > 0 Then
                    intinQty = SwList(0).SWI_Qty
                    DoubleWeight = SwList(0).SWI_Weight
                End If

                If CheckEdit1.Checked = True Then
                    SwInfo.SWI_Qty = intinQty + CInt(txtOutInQty.Text)
                    SwInfo.SWI_Weight = DoubleWeight + Val(txtInWeighing.Text)
                ElseIf CheckEdit1.Checked = False Then
                    SwInfo.SWI_Qty = intinQty - CInt(txtOutInQty.Text)
                    SwInfo.SWI_Weight = DoubleWeight - Val(txtInWeighing.Text)
                End If

                TempInWeighingEnd = intinQty
                TempInQtyingEnd = DoubleWeight

                SwInfo.ModifyDate = Format(Now, "yyyy/MM/dd")
                SwInfo.ModifyUserID = InUserID

                SwInfo.D_ID = gluSE_InD_ID.EditValue
                SwInfo.PS_NO = gluInPS_NO.EditValue

                If SwCon.NmetalSampleWareInventory_Update(SwInfo) = False Then
                    MsgBox("收料入賬失敗,請檢查原因!", MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If

                '4.1-----------------------------------------采集條碼修改部門--與--修改条码状态
                If CheckEdit1.Checked = True Then
                    Dim strStatusType As String = String.Empty
                    Dim strSE_IsRweight As Boolean = False

                    Dim somlist As New List(Of NmetalSamplePaceInfo)
                    somlist = pacon.NmetalSamplePaceBarCode_Getlist(Nothing, Nothing, LabSE_NO.Text)
                    If somlist.Count = 0 Then
                        Exit Sub
                    Else
                        Dim sptlistB As New List(Of NmetalSamplePaceInfo)
                        sptlistB = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
                        If sptlistB.Count > 0 Then
                            strStatusType = sptlistB(0).StatusType
                            strSE_IsRweight = sptlistB(0).SE_IsRweight
                        End If

                        ' ''For m = 0 To somlist.Count - 1
                        ' ''    '修改部門
                        ' ''    If Sccon.NmetalSampleCollection_UpdateG(somlist(m).Code_ID, strStatusType, gluSE_InD_ID.EditValue, somlist(m).InWeight, gluInPS_NO.EditValue) = False Then
                        ' ''        MessageBox.Show("修改条码状态或部門错误!", "提示")
                        ' ''        Exit Sub
                        ' ''    End If

                        ' ''    If strSE_IsRweight = True Then '更新入庫,只有在入库类型时才
                        ' ''        If Sccon.NmetalSampleCollection_UpdateH(somlist(m).Code_ID, somlist(m).InWeight) = False Then
                        ' ''            MessageBox.Show("修改条码状态或部門错误!", "提示")
                        ' ''            Exit Sub
                        ' ''        End If
                        ' ''    End If
                        ' ''Next
                        If strSE_IsRweight = True Then '更新入庫,只有在入库类型时才
                            If Sccon.NmetalSampleCollection_UpdateGIn(LabSE_NO.Text, strStatusType, gluSE_InD_ID.EditValue, gluInPS_NO.EditValue, True) = False Then
                                MessageBox.Show("修改条码状态或部門错误!", "提示")
                                Exit Sub
                            End If
                        Else
                            If Sccon.NmetalSampleCollection_UpdateGIn(LabSE_NO.Text, strStatusType, gluSE_InD_ID.EditValue, gluInPS_NO.EditValue, False) = False Then
                                MessageBox.Show("修改条码状态或部門错误!", "提示")
                                Exit Sub
                            End If
                        End If

                    End If
                End If
            End If
            ProgressBar1.Value = 80 '--显示进度条
            Me.lbl_ProgressBar.Text = "进度显示(80%)....(收发确认中)"
            '4產生新的客戶條碼
            If CheckEdit1.Checked = True Then
                Dim somlist As New List(Of NmetalSamplePaceInfo)
                somlist = pacon.NmetalSamplePaceBarCode_Getlist(Nothing, Nothing, LabSE_NO.Text)
                If somlist.Count = 0 Then
                    Exit Sub
                Else

                    For m = 0 To somlist.Count - 1
                        '4.1產生新的客戶條碼
                        If somlist(m).ClientBarcode <> String.Empty Then
                            Dim SetSO_ID As String = gluSO_ID.EditValue
                            Dim SetPM_M_Code As String = Me.gluPM_M_Code.EditValue
                            Dim SetSS_Edition As String = txtSS_Edition.Text
                            Dim StrStatusType As String = String.Empty

                            Dim objinfo As New NmetalSampleCollectionInfo
                            objinfo.Code_ID = somlist(m).ClientBarcode
                            'objinfo.Qty = somlist(m).InQty
                            '李超修改20140111
                            Dim sptxlist As New List(Of NmetalSamplePaceInfo)
                            sptxlist = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
                            If sptxlist.Count > 0 Then
                                objinfo.StatusType = sptxlist(0).StatusType
                                StrStatusType = sptxlist(0).StatusType

                                '                                If sptxlist(0).SE_OutPSEnabled Then
                                If sptxlist(0).SE_InPSEnabled Then
                                    objinfo.D_ID = gluSE_InD_ID.EditValue
                                    objinfo.TWeight = somlist(m).InWeight
                                    objinfo.IWeight = somlist(m).InWeight  ''转客户条码时的,更新来料重量
                                    objinfo.Qty = somlist(m).InQty
                                Else
                                    objinfo.D_ID = gluSE_OutD_ID.EditValue
                                    objinfo.TWeight = somlist(m).OutWeight
                                    objinfo.IWeight = somlist(m).OutWeight
                                    objinfo.Qty = somlist(m).OutQty
                                End If
                            End If

                            objinfo.Remark = String.Empty
                            objinfo.PM_M_Code = SetPM_M_Code
                            objinfo.AddUserID = InUserID
                            objinfo.AddDate = Format(Now, "yyyy/MM/dd")
                            objinfo.PM_Type = String.Empty
                            objinfo.SO_ID = SetSO_ID
                            objinfo.SP_ID = String.Empty
                            objinfo.SS_Edition = SetSS_Edition

                          


                            objinfo.PS_NO = gluInPS_NO.EditValue

                            objinfo.BarcodeType = "客户条码"
                            If Sccon.NmetalSampleCollection_Add(objinfo) = False Then
                                MessageBox.Show("新增错误!", "提示")
                                Exit Sub
                            End If
                            '厂内条码变为內-->客
                            If Sccon.NmetalSampleCollection_UpdateF(somlist(m).Code_ID, "H", somlist(m).ClientBarcode, 0) = False Then
                                MessageBox.Show("修改客户条码與條碼狀態错误!", "提示")
                                Exit Sub
                            End If

                            If Sccon.NmetalSampleCollection_UpdateA(somlist(m).ClientBarcode, StrStatusType) = False Then
                                MessageBox.Show("修改条码状态错误!", "提示")
                                Exit Sub
                            End If
                        End If

                    Next
                End If


            End If
            ProgressBar1.Value = 90 '--显示进度条
            Me.lbl_ProgressBar.Text = "进度显示(90%)....(收发确认中)"

        End If

        '5更新收料節余------------------------------------------------------------
        Dim SPPIE As New NmetalSamplePaceInfo
        SPPIE.SE_ID = LabSE_NO.Text
        SPPIE.InQtyingEnd = TempInQtyingEnd
        SPPIE.InWeighingEnd = TempInWeighingEnd

        SPPIE.OutQtyingEnd = TempOutQtyingEnd
        SPPIE.OutWeighingEnd = TempOutWeighingEnd

        If spCon.NmetalSamplePace_UpdateInCheckEnd(SPPIE) = False Then
            MsgBox("添加失敗，请檢查原因！")
            Exit Sub
        End If
        '6-------------------------------------------------------------
        '     .裝箱單使用一次以后不可以再使用()
        If txtPK_CodeID.Text <> "" Then SavetxtPK_CodeID()
        '------------------------------------------------------------------
        ProgressBar1.Value = 100 '--显示进度条
        Me.lbl_ProgressBar.Text = "进度显示(100%)....(收发确认完成)"
        Panel7.Visible = False '--显示进度条


        Me.ControlBox = True
        Me.Close()
    End Sub
#End Region



#Region "装箱条码相关"

    Sub SavetxtPK_CodeID()
        Dim strPK_CodeID As String = txtPK_CodeID.Text
        Dim pklist As New List(Of NmetalSamplePackingInfo)
        pklist = spkcon.NmetalSamplePacking_GetList(Nothing, Nothing, Nothing, strPK_CodeID, Nothing, Nothing, Nothing, Nothing)
        If pklist.Count > 0 Then
            If strPK_CodeID <> String.Empty Then
                Dim pkinfo As New NmetalSamplePackingInfo
                pkinfo.Code_ID = strPK_CodeID
                pkinfo.BitNeed = True
                pkinfo.UsePKCount = 1
                If spkcon.NmetalSamplePacking_InUpdateCheck(pkinfo) = False Then
                    MessageBox.Show("修改裝箱状态错误!", "提示")
                    Exit Sub
                End If
            End If
        End If
    End Sub


    Private Sub txtPK_CodeID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPK_CodeID.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim strPK_CodeID As String = txtPK_CodeID.Text
            Dim strSealCode_ID As String = txtSealCode_ID.Text
            Dim strPK_ID As String = String.Empty
            'If strSealCode_ID = String.Empty Then
            '    MessageBox.Show("封条沒有不能为空", "提示")
            '    txtSealCode_ID.Text = String.Empty
            '    Exit Sub
            'End If

            Dim pklist As New List(Of NmetalSamplePackingInfo)
            pklist = spkcon.NmetalSamplePacking_GetList(Nothing, Nothing, Nothing, strPK_CodeID, Nothing, Nothing, Nothing, GetEditValue)
            If pklist.Count > 0 Then
                If pklist(0).CheckBit = False Then
                    MessageBox.Show(strPK_CodeID + "：此装箱条码没有审核", "提示")
                    txtPK_CodeID.Text = String.Empty
                    Exit Sub
                End If
                If pklist(0).BitNeed = True Then
                    MessageBox.Show(strPK_CodeID + "：此装箱条码没有已经使用,不能再使用！", "提示")
                    txtPK_CodeID.Text = String.Empty
                    Exit Sub
                End If
                'If pklist(0).SealCode_ID <> strSealCode_ID Then
                '    MessageBox.Show(strSealCode_ID + "：此封条不属于此单", "提示")
                '    txtSealCode_ID.Text = String.Empty
                '    txtSealCode_ID.Focus()
                '    Exit Sub
                'End If
                strPK_ID = pklist(0).PK_ID
            Else
                MessageBox.Show("条码不存在或者此单没有装箱条码", "提示")
                txtPK_CodeID.Text = String.Empty
                Exit Sub
            End If

            If strPK_ID <> String.Empty Then
                '1.清空
                Dim m As Integer
                For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                    With ds.Tables("SamplePaceBarCode")
                        .Rows(m)("InCheck") = False
                        .Rows(m)("InCheckDate") = Nothing
                        .Rows(m)("InQty") = 0
                        .Rows(m)("InWeight") = 0
                    End With
                Next

                '2'填入確認條碼
                Dim spklist As New List(Of NmetalSamplePackingInfo)
                spklist = spkcon.NmetalSamplePackingSub_GetList(Nothing, strPK_ID, Nothing, Nothing)
                If spklist.Count > 0 Then
                    Dim i As Integer
                    Dim StrCode As String = String.Empty
                    For i = 0 To spklist.Count - 1
                        StrCode = spklist(i).Code_ID
                        Dim strBarCode As String = String.Empty
                        For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                            With ds.Tables("SamplePaceBarCode")
                                strBarCode = IIf(IsDBNull(.Rows(m)("Code_ID")), Nothing, .Rows(m)("Code_ID"))
                                If StrCode = strBarCode Then
                                    .Rows(m)("InCheck") = True
                                    .Rows(m)("InCheckDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
                                    .Rows(m)("InQty") = 1
                                    .Rows(m)("InWeight") = .Rows(m)("OutWeight")
                                    .Rows(m)("ErrorRate") = 0
                                End If
                            End With
                        Next
                    Next
                End If

            End If
        End If
    End Sub
#End Region




#Region "控件事件"
    Private Sub gluSO_ID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluSO_ID.EditValueChanged
        If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.INCHECK Or EditItem = EditEnumType.VIEW Or EditItem = EditEnumType.CHECK Or EditItem = EditEnumType.EDIT Then
            '1.给产品编号填值
            Dim strM As String = gluSO_ID.EditValue
            Dim mc As New NmetalSampleOrdersMainControler
            gluPM_M_Code.Properties.DataSource = mc.NmetalSampleOrdersMain_GetList(strM, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            '2.其他填值
            Dim mtd As New NmetalSampleOrdersMainControler
            Dim somlist As New List(Of NmetalSampleOrdersMainInfo)
            somlist = mtd.NmetalSampleOrdersMain_GetListItem(strM, Nothing, Nothing, True)
            If somlist.Count > 0 Then
                txtSS_Edition.Text = somlist(0).SS_Edition
                gluPM_M_Code.EditValue = somlist(0).PM_M_Code
                txtMaterialType.Text = somlist(0).MaterialTypeName

                '2014-07加
                LabSO_Type.Text = somlist(0).SO_TypeName
                LabSO_Type.Tag = somlist(0).SO_Type

                If LabSO_Type.Tag = "A01" Then
                    txtQty.Enabled = False
                Else
                    txtQty.Enabled = True
                End If

            End If
        End If
    End Sub
    Private Sub gluPM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_M_Code.EditValueChanged
        On Error Resume Next
        'gluM_Code.Properties.DisplayMember = "Type3ID"
        'gluM_Code.Properties.ValueMember = "Type3ID"
        gluM_Code.Properties.DataSource = prcon.NmetalSampleProcessMain_GetList2(Nothing, sender.text)
    End Sub
    Private Sub gluType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluType.EditValueChanged

        If gluType.EditValue = "" Then Exit Sub
        '1.控件可見
        Dim sptlistA As New List(Of NmetalSamplePaceInfo)
        sptlistA = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
        If sptlistA.Count > 0 Then
            If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.EDIT Then
                gluSE_OutD_ID.Enabled = sptlistA(0).SE_OutEnabled
                gluSE_InD_ID.Enabled = sptlistA(0).SE_OutPSEnabled
                gluOutPS_NO.Enabled = sptlistA(0).SE_InEnabled
                gluInPS_NO.Enabled = sptlistA(0).SE_InPSEnabled
                cboCodeType.Enabled = sptlistA(0).SE_BarcodeLink '是否可以条码关联
            End If
        End If
        ''-----------------------------------------------------------

        Dim sptlist As New List(Of NmetalSamplePaceInfo)
        sptlist = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
        If sptlist.Count > 0 Then
            ''類型為“殼”的不能進行開料動作
            '開料主要是將一個條碼變成多個產品 2014-06
            txtQty.Enabled = False
            If sptlist(0).SE_IsKailiao = True Then
                If LabSO_Type.Tag = "A01" Then
                    gluType.EditValue = ""
                    MsgBox("產品非配件類型,不能選擇開料！")
                Else
                    txtQty.Enabled = True
                End If
            End If

            '-第一個發料區可否Enabled
            If sptlist(0).SE_OutVisible Then
                GroupBox2.Enabled = sptlist(0).SE_OutVisible '李超修改
                gluSE_OutD_ID.Properties.DataSource = ds.Tables("SampleDepName")
            Else
                GroupBox2.Enabled = sptlist(0).SE_OutVisible '李超修改
                gluSE_OutD_ID.EditValue = String.Empty
                gluOutPS_NO.EditValue = String.Empty
            End If

            '-第二個收料區可否Enabled
            If sptlist(0).SE_InVisible Then
                GroupBox3.Enabled = sptlist(0).SE_InVisible '李超修改
                If sptlist(0).SE_OutVisible Then
                    gluSE_InD_ID.Properties.DataSource = pmlist
                Else
                    gluSE_InD_ID.Properties.DataSource = ds.Tables("SampleDepName")
                End If
            Else
                gluSE_InD_ID.EditValue = String.Empty
                gluInPS_NO.EditValue = String.Empty
            End If

            GroupBox3.Enabled = sptlist(0).SE_InVisible '李超修改
            ''加载条码表中,显示
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            colOutErrorRate.Visible = False
            colErrorRate.Visible = False
            colTWeight.Visible = False
            colIweight.Visible = False
            If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.EDIT Or EditItem = EditEnumType.CHECK Then
                If sptlist(0).SE_IsRweight = True Then  '类型入库
                    colIweight.Visible = True
                    colErrorRate.Visible = True
                Else
                    colOutErrorRate.Visible = True
                    colTWeight.Visible = True
                End If
                '-------
            ElseIf EditItem = EditEnumType.INCHECK Then
                If sptlist(0).SE_IsRweight = True Then  '类型入库
                    colIweight.Visible = True
                    colErrorRate.Visible = True
                Else
                    colErrorRate.Visible = True
                    colTWeight.Visible = True
                End If
            ElseIf EditItem = EditEnumType.VIEW Then
                If sptlist(0).SE_IsRweight = True Then  '类型入库
                    colIweight.Visible = True
                    colErrorRate.Visible = True
                Else
                    colOutErrorRate.Visible = True
                    colErrorRate.Visible = True
                    colTWeight.Visible = True
                End If
            End If
        Else

            gluSE_InD_ID.EditValue = String.Empty
            gluInPS_NO.EditValue = String.Empty
        End If


        Label4.Visible = False
        gluLoan.Visible = False
        '清空表格数据
        If EditItem = EditEnumType.OUT Then
            ds.Tables("SamplePaceBarCode").Clear() '否清空表内数据
            txtOutInQty.Text = ds.Tables("SamplePaceBarCode").Rows.Count
        End If

    End Sub

    Private Sub cboCodeType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCodeType.EditValueChanged

        Select Case cboCodeType.SelectedIndex
            Case 0
                lblClientBarcode.Visible = False
                txtClientBarcode.Visible = False
                'Me.cmdAutoCode.Enabled = False
                'Me.cmdAutoCode.Text = "可扫描条码输入"
            Case 1
                lblClientBarcode.Visible = True
                txtClientBarcode.Visible = True
                'Me.cmdAutoCode.Enabled = False
                'Me.cmdAutoCode.Text = "可扫描条码输入"

                Select Case EditItem
                    Case EditEnumType.INCHECK
                        lblClientBarcode.Visible = False
                        txtClientBarcode.Visible = False
                        ' Me.cmdAutoCode.Enabled = False
                End Select

            Case 2
                Select Case EditItem
                    Case EditEnumType.OUT
                        lblClientBarcode.Visible = False
                        txtClientBarcode.Visible = False
                        ' Me.cmdAutoCode.Enabled = True
                        ' Me.cmdAutoCode.Text = "自动装箱条码"
                    Case EditEnumType.EDIT
                        lblClientBarcode.Visible = False
                        txtClientBarcode.Visible = False
                        'Me.cmdAutoCode.Enabled = True
                        ' Me.cmdAutoCode.Text = "自动装箱条码"
                    Case EditEnumType.INCHECK
                        lblClientBarcode.Visible = False
                        txtClientBarcode.Visible = False
                        'Me.cmdAutoCode.Enabled = True
                        ' Me.cmdAutoCode.Text = "装箱条码確認"
                End Select
        End Select
    End Sub
#End Region

#Region "新增程序"
    ''' <summary>
    ''' 新增程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataNew() '新增


        '1.主档新增-----------------------------------------------------
        Dim spinfo As New NmetalSamplePaceInfo
        Dim GetSE_IDItem As String
        GetSE_IDItem = GetSE_ID()

        spinfo.SE_ID = GetSE_IDItem '自动流水号
        spinfo.SO_ID = gluSO_ID.EditValue
        spinfo.SS_Edition = Me.txtSS_Edition.Text
        spinfo.PM_M_Code = gluPM_M_Code.EditValue
        spinfo.M_Code = gluM_Code.EditValue
        spinfo.SE_Type = gluType.EditValue
        spinfo.SE_OutD_ID = gluSE_OutD_ID.EditValue
        spinfo.SE_OutPS_NO = Me.gluOutPS_NO.EditValue
        spinfo.SE_AddDate = Me.dateAddDate.EditValue
        spinfo.SE_OutTime = Format(Now, "yyyy/MM/dd")
        spinfo.SE_InTime = Format(Now, "yyyy/MM/dd")
        spinfo.SE_OutInType = "發料"
        spinfo.SE_InD_ID = gluSE_InD_ID.EditValue
        spinfo.SE_InPS_NO = Me.gluInPS_NO.EditValue
        spinfo.SE_Qty = ds.Tables("SamplePaceBarCode").Rows.Count ' txtOutInQty.Text
        spinfo.SE_AddUserID = InUserID
        spinfo.SE_OutCardID = Me.txtCardID.Text

        spinfo.SE_LoanID = gluLoan.EditValue
        spinfo.SE_BorrowType = gluReason.EditValue
        spinfo.SE_BorrowTime = txtTime.EditValue
        spinfo.OutWeighing = Me.txtOutWeighing.EditValue
        spinfo.InWeighing = Me.txtInWeighing.EditValue
        spinfo.SE_Remark = txtSE_Remark.Text
        spinfo.SE_BarCodeType = Me.cobSE_BarCodeType.EditValue
        spinfo.SupplierID = txtSupplierID.Text
        spinfo.SE_CodeType = cboCodeType.SelectedIndex
        '------------------------------------------
        spinfo.OutQtying = Me.txtOutQty.Text
        spinfo.InQtying = Me.txtInQty.Text
        spinfo.SE_OutYCCardID = Me.txtYiChangCard.Text


        If spCon.NmetalSamplePace_Add(spinfo) = False Then
            MsgBox("添加失败!")
            Exit Sub
        End If

        '2.条码新增-----------------------------------------------------
        Dim M As Integer
        For M = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            With ds.Tables("SamplePaceBarCode")
                Dim spBInfo As New NmetalSamplePaceInfo
                Dim strCode_ID As String = IIf(IsDBNull(.Rows(M)("Code_ID")), Nothing, .Rows(M)("Code_ID"))
                spBInfo.SE_ID = GetSE_IDItem
                spBInfo.Code_ID = strCode_ID
                spBInfo.ClientBarcode = IIf(IsDBNull(.Rows(M)("ClientBarcode")), Nothing, .Rows(M)("ClientBarcode"))
                spBInfo.SE_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                spBInfo.SE_AddUserID = InUserID
                '----------------------------------------
                spBInfo.OutQty = IIf(IsDBNull(.Rows(M)("OutQty")), 0, .Rows(M)("OutQty"))
                spBInfo.OutWeight = IIf(IsDBNull(.Rows(M)("OutWeight")), 0, .Rows(M)("OutWeight"))
                spBInfo.InQty = IIf(IsDBNull(.Rows(M)("InQty")), 0, .Rows(M)("InQty"))
                spBInfo.InWeight = IIf(IsDBNull(.Rows(M)("InWeight")), 0, .Rows(M)("InWeight"))

                If spCon.NmetalSamplePaceBarCode_Add(spBInfo) = False Then
                    MsgBox("添加失敗，请檢查原因！")
                    Exit Sub
                End If


            End With
        Next
        LabSE_NO.Text = GetSE_IDItem
        UpdateCheck(False)

        MsgBox("保存成功!")
    End Sub
#End Region

#Region "修改程序"
    ''' <summary>
    '''修改
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataEdit()
        '1.删除条码
        If ds.Tables("DelSamplePaceBarCode").Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ds.Tables("DelSamplePaceBarCode").Rows.Count - 1
                spCon.NmetalSamplePaceBarCode_Delete(ds.Tables("DelSamplePaceBarCode").Rows(j)("AutoID"), Nothing, Nothing) '刪除当前选定的
            Next
        End If
        '1.修改主档
        Dim spinfo As New NmetalSamplePaceInfo
        spinfo.SO_ID = gluSO_ID.EditValue
        spinfo.SS_Edition = Me.txtSS_Edition.Text
        spinfo.PM_M_Code = gluPM_M_Code.EditValue
        spinfo.M_Code = gluM_Code.EditValue
        spinfo.SE_Type = gluType.EditValue
        spinfo.SE_OutD_ID = gluSE_OutD_ID.EditValue
        spinfo.SE_OutPS_NO = Me.gluOutPS_NO.EditValue
        spinfo.SE_AddDate = Me.dateAddDate.EditValue
        spinfo.SE_OutTime = Format(Now, "yyyy/MM/dd")
        spinfo.SE_InTime = Format(Now, "yyyy/MM/dd")
        spinfo.SE_OutInType = "發料"
        spinfo.SE_InD_ID = gluSE_InD_ID.EditValue
        spinfo.SE_InPS_NO = Me.gluInPS_NO.EditValue
        spinfo.SE_Qty = ds.Tables("SamplePaceBarCode").Rows.Count ' txtOutInQty.Text
        spinfo.SE_ID = LabSE_NO.Text
        spinfo.SE_ModifyDate = Format(Now, "yyyy/MM/dd")
        spinfo.SE_ModifyUserID = InUserID
        spinfo.AutoID = GetEditValue
        spinfo.SE_OutCardID = Me.txtCardID.Text
        spinfo.SE_LoanID = gluLoan.EditValue
        spinfo.SE_BorrowType = gluReason.EditValue
        spinfo.SE_BorrowTime = txtTime.EditValue
        spinfo.OutWeighing = Me.txtOutWeighing.EditValue
        spinfo.InWeighing = Me.txtInWeighing.EditValue
        spinfo.SE_Remark = txtSE_Remark.Text
        spinfo.SE_BarCodeType = Me.cobSE_BarCodeType.EditValue
        spinfo.SupplierID = txtSupplierID.Text
        spinfo.SE_CodeType = cboCodeType.SelectedIndex
        '------------------------------------------
        spinfo.OutQtying = Me.txtOutQty.Text
        spinfo.InQtying = Me.txtInQty.Text
        spinfo.SE_OutYCCardID = Me.txtYiChangCard.Text

        If spCon.NmetalSamplePace_Update(spinfo) = False Then
            MsgBox("修改失败!")
            Exit Sub
        End If
        '3.修改条码-----------------------------------------------------
        Dim M As Integer
        For M = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            With ds.Tables("SamplePaceBarCode")

                If IIf(IsDBNull(.Rows(M)("AutoID")), 0, .Rows(M)("AutoID")) = 0 Then
                    Dim spBInfo As New NmetalSamplePaceInfo
                    Dim strCode_ID As String = IIf(IsDBNull(.Rows(M)("Code_ID")), Nothing, .Rows(M)("Code_ID"))
                    spBInfo.SE_ID = LabSE_NO.Text
                    spBInfo.Code_ID = strCode_ID
                    spBInfo.ClientBarcode = IIf(IsDBNull(.Rows(M)("ClientBarcode")), Nothing, .Rows(M)("ClientBarcode"))
                    spBInfo.Qty = 1
                    spBInfo.SE_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                    spBInfo.SE_AddUserID = InUserID
                    '----------------------------------------
                    spBInfo.OutQty = IIf(IsDBNull(.Rows(M)("OutQty")), 0, .Rows(M)("OutQty"))
                    spBInfo.OutWeight = IIf(IsDBNull(.Rows(M)("OutWeight")), 0, .Rows(M)("OutWeight"))
                    spBInfo.InQty = IIf(IsDBNull(.Rows(M)("InQty")), 0, .Rows(M)("InQty"))
                    spBInfo.InWeight = IIf(IsDBNull(.Rows(M)("InWeight")), 0, .Rows(M)("InWeight"))

                    If spCon.NmetalSamplePaceBarCode_Add(spBInfo) = False Then
                        MsgBox("修改失敗，请檢查原因！")
                        Exit Sub
                    End If

                End If
            End With
        Next
        UpdateCheck(False)
        '---------------------------------------------------------------
        MsgBox("修改成功!")
        Me.Close()
    End Sub
#End Region

#Region "按键事件"

    Function CheckLock(ByVal Type As String) As Boolean '盘点收发
        CheckLock = False
        Select Case Type
            Case "Out"

                If gluSE_OutD_ID.Text <> "" Then
                    Dim dtlist As New List(Of DepartmentInfo)
                    dtlist = dtCon.BriName_GetList(gluSE_OutD_ID.EditValue)
                    If dtlist.Count > 0 Then
                        If dtlist(0).CheckLock = True Then
                            MsgBox(gluSE_OutD_ID.Text & ",已盘点锁定,不能操作!")
                            CheckLock = True
                            Exit Function
                        End If
                    End If
                End If
            Case "In"
                If gluSE_InD_ID.Text <> "" Then
                    Dim dtlist1 As New List(Of DepartmentInfo)
                    dtlist1 = dtCon.BriName_GetList(gluSE_InD_ID.EditValue)
                    If dtlist1.Count > 0 Then
                        If dtlist1(0).CheckLock = True Then
                            MsgBox(gluSE_InD_ID.Text & ",已盘点锁定,不能操作!")
                            CheckLock = True
                            Exit Function
                        End If
                    End If
                End If
        End Select
    End Function


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click


        Select Case EditItem
            Case EditEnumType.OUT '发料保存
                If CheckLock("Out") = True Then Exit Sub

                If DataCheckEmpty() = 0 Then
                    Exit Sub
                End If
                DataNew()


                cmdSave.Visible = False
            Case EditEnumType.INCHECK  '确认收料
                If CheckLock("In") = True Then Exit Sub

                If DataCheckEmpty() = 0 Then
                    Exit Sub
                End If
                UpdateInCheck()
            Case EditEnumType.CHECK  '确认收料
                If CheckA.Checked Then
                    If DataCheckEmpty() = 0 Then
                        Exit Sub
                    End If
                End If
                UpdateCheck(True)
            Case EditEnumType.EDIT
                If DataCheckEmpty() = 0 Then
                    Exit Sub
                End If
                DataEdit()
        End Select
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'SetControlEnable(True, True)

        'gluType.EditValue = String.Empty
        'gluSE_OutD_ID.EditValue = String.Empty
        'gluSE_InD_ID.EditValue = String.Empty
        'gluOutPS_NO.EditValue = String.Empty
        'gluInPS_NO.EditValue = String.Empty
        'lblQtyOut.Text = "0"
        'lblQtyIn.Text = "0"
        'ds.Tables("SamplePaceBarCode").Clear()
        'cmdSave.Visible = True
        'cmdAdd.Visible = False
        'cobSE_BarCodeType.SelectedIndex = 0
        'cobSE_BarCodeType.Enabled = True
    End Sub
#End Region

#Region "创建临时表"
    ''' <summary>
    ''' 创建临时表
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("SamplePaceBarCode") '子配件表
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("SPID", GetType(String))
            .Columns.Add("ClientBarcode", GetType(String))
            .Columns.Add("OutQty", GetType(Int32))
            .Columns.Add("OutWeight", GetType(Decimal))
            .Columns.Add("InQty", GetType(Int32))
            .Columns.Add("InWeight", GetType(Decimal))

            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("SE_AddDate", GetType(String))
            .Columns.Add("InCheck", GetType(Boolean))
            .Columns.Add("InCheckDate", GetType(String))

            .Columns.Add("ErrorRate", GetType(Decimal))
            .Columns.Add("TWeight", GetType(Decimal))
            .Columns.Add("OutErrorRate", GetType(Decimal))

            .Columns.Add("IWeight", GetType(Decimal))

        End With

        Grid1.DataSource = ds.Tables("SamplePaceBarCode")
        With ds.Tables.Add("DelSamplePaceBarCode")
            .Columns.Add("AutoID", GetType(Decimal))
        End With

        With ds.Tables.Add("SampleDepName") '子配件表
            .Columns.Add("DepID", GetType(String))
            .Columns.Add("DepName", GetType(String))
        End With

        With ds.Tables.Add("SamplePS") '子配件表
            .Columns.Add("PS_Num", GetType(String))
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
            .Columns.Add("PS_NoName", GetType(String))
        End With


        With ds.Tables.Add("NmetalSamplePaceType")
            .Columns.Add("SE_Type", GetType(String))
            .Columns.Add("SE_TypeName", GetType(String))
        End With
        gluType.Properties.ValueMember = "SE_Type"
        gluType.Properties.DisplayMember = "SE_TypeName"
        gluType.Properties.DataSource = ds.Tables("NmetalSamplePaceType")

    End Sub

    Sub CreatePaceType(ByVal _InUserID As String)

        ds.Tables("NmetalSamplePaceType").Clear()

        Dim pflist As New List(Of NmetalSamplePaceInfo)
        If _InUserID = Nothing Then
            pflist = spCon.NmetalSamplePaceType_GetlistIn(Nothing)
        Else
            '加载只有权限的记录
            Dim pmws As New PermissionModuleWarrantSubController
            Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
            pmwiL = pmws.PermissionModuleWarrantSub_GetList(_InUserID, "860414")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value <> "" Then
                    Dim TempStr As String
                    TempStr = "'" & Replace(pmwiL.Item(0).PMWS_Value, ",", "','") & "'"
                    pflist = spCon.NmetalSamplePaceType_GetlistIn(TempStr)
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        End If
        ''加载数据
        If pflist.Count > 0 Then
            Dim i As Integer = 0
            For i = 0 To pflist.Count - 1
                Dim row As DataRow
                row = ds.Tables("NmetalSamplePaceType").NewRow
                row("SE_Type") = pflist(i).SE_Type
                row("SE_TypeName") = pflist(i).SE_TypeName
                ds.Tables("NmetalSamplePaceType").Rows.Add(row)
            Next
        End If




    End Sub



    Sub CreateDepNameTable(ByVal boolUserID As Boolean)
        Dim strUserID As String = String.Empty
        If boolUserID = True Then
            strUserID = InUserID
        Else
            strUserID = Nothing
        End If

        Dim pflist As New List(Of ProductionFieldControlInfo)
        pflist = pfcon.ProductionFieldControl_GetList1(strUserID, Nothing, "V")
        ds.Tables("SampleDepName").Clear()
        Dim i As Integer = 0
        For i = 0 To pflist.Count - 1
            Dim row As DataRow
            row = ds.Tables("SampleDepName").NewRow
            row("DepID") = pflist(i).ControlDep
            row("DepName") = pflist(i).DepName
            ds.Tables("SampleDepName").Rows.Add(row)
        Next
    End Sub
#End Region

#Region "条码事件"
    Private Sub txtM_Code_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            Select Case EditItem
                Case EditEnumType.OUT
                    BarCode()
                Case EditEnumType.EDIT
                    BarCode()
                Case EditEnumType.INCHECK
                    BarCodeInCheck()
            End Select
        End If
    End Sub

    Sub BarCodePeiJian()
        Dim strSE_OutVisible As String = 0
        '0.驗証重量,--------------------------------------------
        If Val(txtQty.Text) <= 0 Then
            txtQty.Select()
            lblCode.Text = "數量輸入有誤！"
            Exit Sub
        End If

        If Val(txtWeight.Text) <= 0 Then
            lblCode.Text = "重量輸入有誤！"
            Exit Sub
        End If

        If gluSO_ID.Text = "" Then
            lblCode.Text = "請選擇訂單編號！"
            Exit Sub
        End If

        If gluType.Text = "" Then
            lblCode.Text = "請選擇類型！"
            Exit Sub
        End If

        If gluInPS_NO.Text = "" And gluOutPS_NO.Text = "" Then
            lblCode.Text = "請選擇工序！"
            Exit Sub
        End If

        '1.条码以输入-重复-------------------------------
        lblCode.Text = String.Empty
        Dim strM_Code As String = Trim(StrConv(UCase(Me.txtM_Code.Text), vbNarrow))
        Dim i As Integer
        For i = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            If strM_Code = ds.Tables("SamplePaceBarCode").Rows(i)("Code_ID") Then
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                lblCode.Text = "条码重复"
                Exit Sub
            End If
        Next
        '2.条码验证
        Dim sclist As New List(Of NmetalSampleCollectionInfo)
        sclist = Sccon.NmetalSampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If sclist.Count > 0 Then
            '2.1-----------------------------------------------存在客户条码
            If sclist(0).ClientBarcode <> String.Empty Then
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                lblCode.Text = "存在客户条码"
                Exit Sub
            End If
            '2.2-----------------------------------------------订单不存在此条码
            If sclist(0).SO_ID <> gluSO_ID.EditValue Then
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                lblCode.Text = "此订单不存在此条码"
                Exit Sub
            End If
            '配件一個條碼多個數量,多個狀態
        Else
            txtM_Code.Text = String.Empty
            txtM_Code.Focus()
            lblCode.Text = "条码不存在"
            Exit Sub
        End If

        ' ''8.条码是否正在使用中李超20140116加
        ' ''記住要分"殼"與"配件"
        ''Dim somAlist As New List(Of NmetalSamplePaceInfo)
        ''somAlist = pacon.NmetalSamplePaceBarCode_Getlist2(strM_Code, Nothing)
        ''If somAlist.Count > 0 Then
        ''    txtM_Code.Text = String.Empty
        ''    txtM_Code.Focus()
        ''    lblCode.Text = "条码正在使用中,请查明原因"
        ''    Exit Sub
        ''End If

        '9.跳入客户条码输入---------------------------------
        If cboCodeType.SelectedIndex = 1 Then
            Me.txtClientBarcode.Text = String.Empty
            Me.txtClientBarcode.Focus()
            Exit Sub
        End If

        '加入表中------------------------------------------------------------------------
        Dim row As DataRow
        row = ds.Tables("SamplePaceBarCode").NewRow
        row("Code_ID") = strM_Code
        row("SPID") = String.Empty

        If strSE_OutVisible = True Then
            row("OutQty") = txtQty.Text
            row("OutWeight") = txtWeight.Text
        Else
            row("InQty") = txtQty.Text
            row("InWeight") = txtWeight.Text
        End If

        row("InCheck") = False
        row("SE_AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
        ds.Tables("SamplePaceBarCode").Rows.Add(row)
        '加入表中------------------------------------------------------------------------

        txtM_Code.Text = ""
        txtWeight.Text = 0

        SumWeight()

        If ds.Tables("SamplePaceBarCode").Rows.Count = 1 Then
            SetControlEnable(False, True)
        End If

        '11.条码输入总数量处理-------------------------------
        If ds.Tables("SamplePaceBarCode").Rows.Count > 0 Then
            txtOutInQty.Text = ds.Tables("SamplePaceBarCode").Rows.Count
            cobSE_BarCodeType.Enabled = False
        Else
            cobSE_BarCodeType.Enabled = True
        End If
    End Sub

    Private Sub txtClientBarcode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtClientBarcode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Select Case EditItem
                Case EditEnumType.OUT
                    If cobSE_BarCodeType.SelectedIndex = 0 Then
                        SetClientBarcode()
                    Else
                        ' SetClientBarcodeAuto()
                    End If
                Case EditEnumType.EDIT
                    If cobSE_BarCodeType.SelectedIndex = 0 Then
                        SetClientBarcode()
                    Else
                        '  SetClientBarcodeAuto()
                    End If
            End Select
        End If
    End Sub

    ''' <summary>
    '''     客户条码自动方式
    ''' </summary>
    Sub SetClientBarcodeAuto()
        '1.第二个客户条码输入是否重复
        Dim strClientBarcode As String = Trim(UCase(Me.txtClientBarcode.Text))
        Dim N As Integer
        For N = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            With ds.Tables("SamplePaceBarCode")
                Dim strClientBarcodeA As String = IIf(IsDBNull(.Rows(N)("ClientBarcode")), String.Empty, .Rows(N)("ClientBarcode"))
                If strClientBarcode = strClientBarcodeA Then
                    txtClientBarcode.Text = String.Empty
                    txtClientBarcode.Focus()
                    lblCode.Text = "条码重复"
                    Exit Sub
                End If
            End With
        Next
        ''此客戶條碼<采集表>存在
        Dim sclcom As New NmetalSampleCollectionControler
        If sclcom.NmetalSampleCollection_GetID(UCase(Me.txtClientBarcode.Text)) = True Then
            lblCode.Text = "客戶条码重复"
            txtClientBarcode.Text = String.Empty
            txtClientBarcode.Focus()
            MessageBox.Show(UCase(Me.txtClientBarcode.Text) + "此客戶條碼<采集表>存在!", "提示")
            Exit Sub
        End If

        Dim m As Integer
        For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            With ds.Tables("SamplePaceBarCode")
                Dim strCode_ID As String = IIf(IsDBNull(.Rows(m)("ClientBarcode")), String.Empty, .Rows(m)("ClientBarcode"))
                If strCode_ID = String.Empty Then
                    .Rows(m)("ClientBarcode") = strClientBarcode
                    txtClientBarcode.Text = String.Empty
                    txtClientBarcode.Focus()
                    Exit For
                End If
            End With
        Next
    End Sub
    ''' <summary>
    '''     客户条码
    ''' </summary>
    Sub SetClientBarcode()
        If txtM_Code.Text = String.Empty Then
            txtM_Code.Focus()
            Exit Sub
        End If

        If Val(txtWeight.Text) <= 0 Then
            lblCode.Text = "重量輸入有誤！"
            Exit Sub
        End If


        '第二个客户条码输入是否重复
        Dim strCode As String = Trim(StrConv(UCase(Me.txtM_Code.Text), vbNarrow))
        Dim strClientBarcode As String = Trim(StrConv(UCase(Me.txtClientBarcode.Text), vbNarrow))
        Dim N As Integer
        For N = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            Dim strClientBarcodeA As String = IIf(IsDBNull(ds.Tables("SamplePaceBarCode").Rows(N)("ClientBarcode")), String.Empty, ds.Tables("SamplePaceBarCode").Rows(N)("ClientBarcode"))
            If strClientBarcode = strClientBarcodeA Then
                txtClientBarcode.Text = String.Empty
                txtClientBarcode.Focus()
                lblCode.Text = "客戶条码重复"
                Exit Sub
            End If
            '內部條碼確認問題'20140414修改
            Dim strCodeA As String = IIf(IsDBNull(ds.Tables("SamplePaceBarCode").Rows(N)("Code_ID")), String.Empty, ds.Tables("SamplePaceBarCode").Rows(N)("Code_ID"))
            If strCode = strCodeA Then
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                lblCode.Text = "內部条码重复"
                Exit Sub
            End If
        Next

        '此客戶條碼<采集表>存在
        Dim sclcom As New NmetalSampleCollectionControler
        If sclcom.NmetalSampleCollection_GetID(UCase(Me.txtClientBarcode.Text)) = True Then
            lblCode.Text = "客戶条码重复"
            txtClientBarcode.Text = String.Empty
            txtClientBarcode.Focus()
            MessageBox.Show(UCase(Me.txtClientBarcode.Text) + "此客戶條碼<采集表>存在!", "提示")
            Exit Sub
        End If

        If strCode = String.Empty Then
            txtM_Code.Focus()
            lblCode.Text = "客戶条码重复"
        End If
        '-------------------------------------------------------
        '查询出当前,真实库存
        Dim doubleTWeight As Decimal = 0 '记录当前,条码重量,内部条码库存
        Dim dblErrorRate As Decimal = 0

        Dim sclist As New List(Of NmetalSampleCollectionInfo)
        sclist = Sccon.NmetalSampleCollection_Getlist(Nothing, strCode, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If sclist.Count > 0 Then
            doubleTWeight = sclist(0).TWeight

            Dim saplist As New List(Of NmetalSamplePaceInfo)
            saplist = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
            If saplist.Count > 0 Then
                If saplist(0).SE_OutVisible = True Then  '当发出时
                    'dblErrorRate = FormatNumber((sclist(0).TWeight - Val(txtWeight.Text)) / sclist(0).TWeight * 100, 3, TriState.True)
                    'If dblErrorRate < Val(txtOutDwonRate.Text) Or dblErrorRate > Val(Me.txtOutUpRate.Text) Then
                    '    lblCode.Text = "误差率太大!"
                    'End If
                    dblErrorRate = FormatNumber(sclist(0).TWeight - Val(txtWeight.Text), 3, TriState.True)
                    If dblErrorRate < Val(txtOutDwonRate.Text) Or dblErrorRate > Val(Me.txtOutUpRate.Text) Then
                        lblCode.Text = "误差太大!"
                    End If
                End If
            End If
        Else
            txtM_Code.Text = String.Empty
            txtM_Code.Focus()
            lblCode.Text = "条码不存在"
            Exit Sub
        End If



        '-------------------------------------------------------------------------
        Dim row As DataRow
        row = ds.Tables("SamplePaceBarCode").NewRow
        row("Code_ID") = strCode
        row("ClientBarcode") = strClientBarcode
        row("SPID") = String.Empty

        row("TWeight") = doubleTWeight
        row("OutErrorRate") = dblErrorRate

        row("OutQty") = txtQty.Text
        row("OutWeight") = txtWeight.Text

        row("InCheck") = False
        row("SE_AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
        ds.Tables("SamplePaceBarCode").Rows.Add(row)
        txtClientBarcode.Text = String.Empty

        '4.条码总数量---------------------------------------
        If ds.Tables("SamplePaceBarCode").Rows.Count = 1 Then
            SetControlEnable(False, True)
        End If

        txtOutInQty.Text = ds.Tables("SamplePaceBarCode").Rows.Count

        SumWeight()

        '5.跳入客户条码输入---------------------------------
        If cboCodeType.SelectedIndex = 1 Then
            Me.txtM_Code.Text = String.Empty
            Me.txtM_Code.Focus()
        End If
    End Sub
    ''' <summary>
    ''' 2014-09-17
    ''' 張偉
    ''' 選擇性穩重
    ''' </summary>
    ''' <remarks></remarks>
    Sub SelectWeight()
        Dim prcIn As New List(Of NmetalSampleProcessInfo)
        Dim prcOut As New List(Of NmetalSampleProcessInfo)
        Dim nspic As New NmetalSampleProcessControl
        prcIn = nspic.NmetalSampleProcessSub_GetList(Nothing, gluInPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing)      '收料工序
        prcOut = nspic.NmetalSampleProcessSub_GetList(Nothing, gluOutPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing)    '發料工序
        If prcIn.Count > 0 And prcOut.Count > 0 Then
            If (prcOut(0).PS_IsAutoWeight = False Or prcIn(0).PS_IsCompleteProcess = True) Then

            Else
                Dim nscl As New List(Of NmetalSampleCollectionInfo)
                nscl = Sccon.NmetalSampleCollection_Getlist(Nothing, txtM_Code.Text, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                txtWeight.Text = nscl(0).TWeight
            End If
        End If
    End Sub

    Sub BarCode()
        Dim strSE_OutVisible As String = 0
        Dim dblErrorRate As Decimal = 0

        '2014-09-12   張偉    完工時自動帶出重量
        If gluType.EditValue = "" Then
            Exit Sub
        End If
        Dim sptlist As New List(Of NmetalSamplePaceInfo)
        sptlist = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
        If sptlist.Count > 0 Then
            If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.EDIT Then

                If sptlist(0).SE_IsCompletionWeight Then      '根據NmetalSampelePaceType表中各類型是否穩重
                    Dim nscl As New List(Of NmetalSampleCollectionInfo)
                    nscl = Sccon.NmetalSampleCollection_Getlist(Nothing, txtM_Code.Text, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    txtWeight.Text = nscl(0).TWeight
                End If
            End If
        End If

        ''2014-09-16     張偉
        'If gluType.EditValue = "P02" Then
        '    Dim nspi As New List(Of NmetalSampleProcessInfo)
        '    Dim nspi1 As New List(Of NmetalSampleProcessInfo)
        '    Dim nspic As New NmetalSampleProcessControl
        '    nspi = nspic.NmetalSampleProcessSub_GetList(Nothing, gluInPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing)      '收料工序
        '    nspi1 = nspic.NmetalSampleProcessSub_GetList(Nothing, gluOutPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing)    '發料工序
        '    If nspi.Count > 0 Or nspi1.Count > 0 Then

        '        If EditItem = EditEnumType.OUT Then

        '            If nspi(0).PS_BarCodeBit Or nspi1(0).PS_BarCodeBit Then

        '                Dim nscl As New List(Of NmetalSampleCollectionInfo)

        '                nscl = Sccon.NmetalSampleCollection_Getlist(Nothing, txtM_Code.Text, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        '                txtWeight.Text = nscl(0).TWeight

        '            End If
        '        End If
        '    End If
        'End If

        ''2014-09-17     張偉
        'If gluType.EditValue = "P02" Then
        '    Dim prcIn As New List(Of NmetalSampleProcessInfo)
        '    Dim prcOut As New List(Of NmetalSampleProcessInfo)
        '    Dim nspic As New NmetalSampleProcessControl
        '    prcIn = nspic.NmetalSampleProcessSub_GetList(Nothing, gluInPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing)      '收料工序
        '    prcOut = nspic.NmetalSampleProcessSub_GetList(Nothing, gluOutPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing)    '發料工序
        '    If prcIn.Count > 0 And prcOut.Count > 0 Then

        '        If EditItem = EditEnumType.OUT Then
        '            If (prcOut(0).PS_IsAutoWeight = False Or prcIn(0).PS_IsCompleteProcess = True) Then

        '            Else
        '                Dim nscl As New List(Of NmetalSampleCollectionInfo)
        '                nscl = Sccon.NmetalSampleCollection_Getlist(Nothing, txtM_Code.Text, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '                txtWeight.Text = nscl(0).TWeight
        '            End If
        '        End If
        '    End If
        'End If


        'Dim doubleWeight As Double


        ''2014-09-17     張偉
        'If gluType.EditValue = "P02" And EditItem = EditEnumType.OUT Then

        '    doubleWeight = SelectWeight()
        'Else
        '    doubleWeight = Val(txtWeight.Text)
        'End If
        If gluType.EditValue = "P02" And EditItem = EditEnumType.OUT Then


            SelectWeight()
        End If

        '0.驗証重量,--------------------------------------------
        If Val(txtWeight.Text) <= 0 Then
            lblCode.Text = "重量輸入有誤！"
            Exit Sub
        End If

        If gluSO_ID.Text = "" Then
            lblCode.Text = "請選擇訂單編號！"
            Exit Sub
        End If

        If gluType.Text = "" Then
            lblCode.Text = "請選擇類型！"
            Exit Sub
        End If

        If gluInPS_NO.Text = "" And gluOutPS_NO.Text = "" Then
            lblCode.Text = "請選擇工序！"
            Exit Sub
        End If

        '1.条码以输入-重复-------------------------------
        lblCode.Text = String.Empty
        Dim strM_Code As String = Trim(StrConv(UCase(Me.txtM_Code.Text), vbNarrow))
        Dim i As Integer
        For i = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            If strM_Code = ds.Tables("SamplePaceBarCode").Rows(i)("Code_ID") Then
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                lblCode.Text = "条码重复"
                Exit Sub
            End If
        Next

        '2.条码验证
        Dim doubleTWeight As Decimal '记录当前,条码重量
        Dim doubleIWeight As Decimal
        Dim sclist As New List(Of NmetalSampleCollectionInfo)
        sclist = Sccon.NmetalSampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If sclist.Count > 0 Then
            doubleTWeight = sclist(0).TWeight
            doubleIWeight = sclist(0).IWeight
            '2.1-----------------------------------------------存在客户条码
            If sclist(0).ClientBarcode <> String.Empty Then
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                lblCode.Text = "存在客户条码"
                Exit Sub
            End If
            '2.2-----------------------------------------------订单不存在此条码
            If sclist(0).SO_ID <> gluSO_ID.EditValue Then
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                lblCode.Text = "此订单不存在此条码"
                Exit Sub
            End If
            '2.3-----------------------------------------------条码是否可能再收发
            If sclist(0).StatusType <> String.Empty Then
                Dim stlist As New List(Of NmetalSampleTransactionInfo)
                stlist = stcon.NmetalSampleTransactionType_GetList(sclist(0).StatusType, Nothing)
                'If (stlist(0).IsTransferred = False) And (gluType.EditValue <> "P07") Then
                If (stlist(0).IsTransferred = False) Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    lblCode.Text = "此条码已经" + stlist(0).StatusTypeName
                    Exit Sub
                End If
            End If

            '2.6-----------------------------------------------采集表部门不存在"
            Dim saplist As New List(Of NmetalSamplePaceInfo)
            saplist = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
            If saplist.Count > 0 Then
                strSE_OutVisible = saplist(0).SE_OutVisible
                If saplist(0).SE_OutVisible Then
                    If sclist(0).D_ID <> gluSE_OutD_ID.EditValue Then
                        txtM_Code.Text = String.Empty
                        txtM_Code.Focus()
                        lblCode.Text = "采集表部门不存在此条码"
                        Exit Sub
                    End If
                End If
                '2.4-----------------------------------------------開料此條碼已经處理
                If saplist(0).SE_StatusTypeIsNull = True Then
                    If sclist(0).StatusType <> String.Empty Then
                        txtM_Code.Text = String.Empty
                        txtM_Code.Focus()
                        lblCode.Text = "此條碼狀態為" + sclist(0).StatusTypeName
                        Exit Sub
                    End If
                End If

                If saplist(0).SE_OutVisible = True Then  '当发出时
                    dblErrorRate = FormatNumber(sclist(0).TWeight - Val(txtWeight.Text), 3, TriState.True)
                    If dblErrorRate < Val(txtOutDwonRate.Text) Or dblErrorRate > Val(Me.txtOutUpRate.Text) Then
                        lblCode.Text = "误差太大!"
                    End If
                End If

                'InWeight
                If saplist(0).SE_IsRweight = True Then  '类型入库
                    dblErrorRate = FormatNumber(sclist(0).IWeight - Val(txtWeight.Text), 3, TriState.True)
                    If dblErrorRate < Val(txtInDwonRate.Text) Or dblErrorRate > Val(Me.txtInUpRate.Text) Then
                        lblCode.Text = "误差太大!"
                    End If
                End If

            End If
        Else
            txtM_Code.Text = String.Empty
            txtM_Code.Focus()
            lblCode.Text = "条码不存在"
            Exit Sub
        End If

        '8.条码是否正在使用中李超20140116加
        '記住要分"殼"與"配件"
        Dim somAlist As New List(Of NmetalSamplePaceInfo)
        somAlist = pacon.NmetalSamplePaceBarCode_Getlist2(strM_Code, Nothing)
        If somAlist.Count > 0 Then
            txtM_Code.Text = String.Empty
            txtM_Code.Focus()
            lblCode.Text = "条码正在使用中,请查明原因"
            Exit Sub
        End If

        '9.跳入客户条码输入---------------------------------
        If cboCodeType.SelectedIndex = 1 Then
            Me.txtClientBarcode.Text = String.Empty
            Me.txtClientBarcode.Focus()
            Exit Sub
        End If

        '加入表中------------------------------------------------------------------------
        Dim row As DataRow
        row = ds.Tables("SamplePaceBarCode").NewRow
        row("Code_ID") = strM_Code
        row("SPID") = String.Empty
        If strSE_OutVisible = True Then
            row("OutQty") = txtQty.Text
            row("OutWeight") = txtWeight.Text
            row("SE_AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
            row("OutErrorRate") = dblErrorRate
        Else
            row("InQty") = txtQty.Text
            row("InWeight") = txtWeight.Text
            row("InCheckDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
            row("ErrorRate") = dblErrorRate
        End If

        row("TWeight") = doubleTWeight
        row("IWeight") = doubleIWeight
        row("InCheck") = False

        ds.Tables("SamplePaceBarCode").Rows.Add(row)
        '加入表中------------------------------------------------------------------------
        txtM_Code.Text = ""
        txtWeight.Text = 0

        SumWeight()

        If ds.Tables("SamplePaceBarCode").Rows.Count = 1 Then
            SetControlEnable(False, True)
        End If

        '11.条码输入总数量处理-------------------------------
        If ds.Tables("SamplePaceBarCode").Rows.Count > 0 Then
            txtOutInQty.Text = ds.Tables("SamplePaceBarCode").Rows.Count
            cobSE_BarCodeType.Enabled = False
        Else
            cobSE_BarCodeType.Enabled = True
        End If
    End Sub

    Sub SumWeight()
        Dim i As Integer
        '合計中所有-------------------------------
        Dim TempOutWeight As Decimal = 0
        Dim TempInWeight As Decimal = 0

        Dim TempOutQty As Integer = 0
        Dim TempInQty As Integer = 0
        If ds.Tables("SamplePaceBarCode").Rows.Count <= 0 Then
            Me.txtOutWeighing.Text = 0
            Me.txtInWeighing.Text = 0
            Me.txtOutQty.Text = 0
            Me.txtInQty.Text = 0
        Else
            With ds.Tables("SamplePaceBarCode")
                For i = 0 To .Rows.Count - 1
                    TempOutWeight = TempOutWeight + IIf(IsDBNull(.Rows(i)("OutWeight")), 0, .Rows(i)("OutWeight"))
                    TempInWeight = TempInWeight + IIf(IsDBNull(.Rows(i)("InWeight")), 0, .Rows(i)("InWeight"))

                    TempOutQty = TempOutQty + IIf(IsDBNull(.Rows(i)("OutQty")), 0, .Rows(i)("OutQty"))
                    TempInQty = TempInQty + IIf(IsDBNull(.Rows(i)("InQty")), 0, .Rows(i)("InQty"))
                Next
            End With

            Me.txtOutWeighing.Text = TempOutWeight
            Me.txtInWeighing.Text = TempInWeight
            Me.txtOutQty.Text = TempOutQty
            Me.txtInQty.Text = TempInQty
            '----------------------------------
        End If
    End Sub

    '确认条码
    Sub BarCodeInCheck()
        '1.不需要輸入確認條碼"-------------------------
        Dim sptlist As New List(Of NmetalSamplePaceInfo)
        sptlist = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
        If sptlist.Count > 0 Then
            If EditItem = EditEnumType.INCHECK And sptlist(0).SE_InCheckBarcode = False Then
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                lblCode.Text = "不需要輸入確認條碼"
                Exit Sub
            End If
        End If

        ''2014-09-16   張偉
        'Dim nspi1 As New List(Of NmetalSampleProcessInfo)
        'Dim nspic As New NmetalSampleProcessControl
        'nspi1 = nspic.NmetalSampleProcessSub_GetList(Nothing, gluOutPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing)    '發料工序
        'If nspi1.Count > 0 Then

        '    If EditItem = EditEnumType.INCHECK Then

        '        If nspi1(0).PS_BarCodeBit Then

        '            Dim nscl As New List(Of NmetalSampleCollectionInfo)

        '            nscl = Sccon.NmetalSampleCollection_Getlist(Nothing, txtM_Code.Text, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        '            txtWeight.Text = nscl(0).TWeight

        '        End If
        '    End If
        'End If

        ''2014-09-17   張偉
        'Dim prcOut As New List(Of NmetalSampleProcessInfo)
        'Dim prcIn As New List(Of NmetalSampleProcessInfo)
        'Dim nspic As New NmetalSampleProcessControl
        'prcIn = nspic.NmetalSampleProcessSub_GetList(Nothing, gluInPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing)      '收料工序
        'prcOut = nspic.NmetalSampleProcessSub_GetList(Nothing, gluOutPS_NO.EditValue, Nothing, Nothing, Nothing, Nothing)    '發料工序
        'If prcIn.Count > 0 And prcOut.Count > 0 Then
        '    If EditItem = EditEnumType.INCHECK Then
        '        If (prcOut(0).PS_IsAutoWeight = False Or prcIn(0).PS_IsCompleteProcess = True) Then

        '        Else
        '            Dim nscl As New List(Of NmetalSampleCollectionInfo)
        '            nscl = Sccon.NmetalSampleCollection_Getlist(Nothing, txtM_Code.Text, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '            txtWeight.Text = nscl(0).TWeight
        '        End If
        '    End If
        'End If

        '2014-09-17     張偉
        If gluType.EditValue = "P02" And EditItem = EditEnumType.INCHECK Then
            SelectWeight()
        End If

        If Val(txtWeight.Text) <= 0 Then
            lblCode.Text = "重量輸入有誤！"
            Exit Sub
        End If

        Dim boolBarcode As Boolean = False
        Dim M As Integer
        Dim strBarCode As String
        Dim strClientBarcode As String
        Dim strCode As String = Trim(StrConv(UCase(Me.txtM_Code.Text), vbNarrow))
        lblCode.Text = String.Empty
        For M = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            With ds.Tables("SamplePaceBarCode")
                strClientBarcode = IIf(IsDBNull(.Rows(M)("ClientBarcode")), String.Empty, .Rows(M)("ClientBarcode"))
                If strClientBarcode <> String.Empty Then
                    If strCode = strClientBarcode Then
                        .Rows(M)("InCheck") = True
                        .Rows(M)("InCheckDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
                        .Rows(M)("InQty") = txtQty.Text
                        .Rows(M)("InWeight") = txtWeight.Text

                        txtM_Code.Text = String.Empty
                        txtM_Code.Focus()
                        boolBarcode = True

                    End If
                Else
                    strBarCode = IIf(IsDBNull(.Rows(M)("Code_ID")), Nothing, .Rows(M)("Code_ID"))
                    If strCode = strBarCode Then
                        .Rows(M)("InCheck") = True
                        .Rows(M)("InCheckDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
                        .Rows(M)("InQty") = txtQty.Text
                        .Rows(M)("InWeight") = txtWeight.Text

                        txtM_Code.Text = String.Empty
                        txtM_Code.Focus()
                        boolBarcode = True

                    End If
                End If
                '----------------------------------------------------------------------------------------------------
                If .Rows(M)("OutWeight") <> 0 Then
                    .Rows(M)("ErrorRate") = FormatNumber(.Rows(M)("OutWeight") - .Rows(M)("InWeight"), 3, TriState.True)
                Else
                    .Rows(M)("ErrorRate") = 0
                End If

            End With
        Next

        If boolBarcode = False Then
            lblCode.Text = "确认条码不存在列表"
            txtM_Code.Text = String.Empty
            txtM_Code.Focus()
            Exit Sub
        End If

        SumWeight() '匯總

    End Sub
#End Region

#Region "表格删除事件"
    Private Sub cmdDelSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelSub.Click
        If cboCodeType.SelectedIndex = 2 Then
            For m As Integer = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                With ds.Tables("SamplePaceBarCode")
                    Dim row As DataRow = ds.Tables("DelSamplePaceBarCode").NewRow
                    row("AutoID") = ds.Tables("SamplePaceBarCode").Rows(m)("AutoID")
                    ds.Tables("DelSamplePaceBarCode").Rows.Add(row)
                End With
            Next
            ds.Tables("SamplePaceBarCode").Clear()
            cboCodeType.Enabled = True
        Else
            If GridView9.RowCount = 0 Then Exit Sub
            Dim DelTemp As String
            DelTemp = GridView9.GetRowCellDisplayText(GridView9.GetSelectedRows(0), "AutoID")
            If DelTemp <> String.Empty Then
                Dim row As DataRow = ds.Tables("DelSamplePaceBarCode").NewRow
                row("AutoID") = ds.Tables("SamplePaceBarCode").Rows(GridView9.FocusedRowHandle)("AutoID")
                ds.Tables("DelSamplePaceBarCode").Rows.Add(row)
            End If
            ds.Tables("SamplePaceBarCode").Rows.RemoveAt(GridView9.GetSelectedRows(0))
        End If

        If ds.Tables("SamplePaceBarCode").Rows.Count > 0 Then
            txtOutInQty.Text = ds.Tables("SamplePaceBarCode").Rows.Count
            cobSE_BarCodeType.Enabled = False
            'gluType.Enabled = False
        Else
            cobSE_BarCodeType.Enabled = True
            'gluType.Enabled = True
        End If

        If ds.Tables("SamplePaceBarCode").Rows.Count = 0 Then
            SetControlEnable(True, True)
        End If

        SumWeight()

    End Sub
#End Region

#Region "是否為空"

    ''' <summary>
    ''' 保存数据前处理
    ''' </summary>
    ''' <remarks></remarks>
    Function DataCheckEmpty() As Integer


        If gluSO_ID.Text = String.Empty Then
            MsgBox("订单编号不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluSO_ID.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If
        If gluPM_M_Code.Text = String.Empty Then
            MsgBox("产品编号不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluPM_M_Code.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If gluM_Code.EditValue = String.Empty Or gluM_Code.Text = String.Empty Then
            MsgBox("產品類別不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluM_Code.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If txtSS_Edition.Text = String.Empty Then
            MsgBox("版本号不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            txtSS_Edition.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If gluType.Text = String.Empty Then
            MsgBox("类型不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluType.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If


        If gluPM_M_Code.Text <> gluM_Code.Text Then
            MsgBox("产品编号与产同类型不同！", MsgBoxStyle.Information, "溫馨提示")
            gluM_Code.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If
        '----------------------------------------------------------------------
        '判断当前部门是否允许此类型,双重判断
        Dim BriC As New NmetalSamplePaceTypeBriNameControl
        If gluSE_OutD_ID.EditValue <> "" Then
            If BriC.NmetalSamplePaceTypeBriName_GetList(gluSE_OutD_ID.EditValue, gluType.EditValue).Count <= 0 Then
                MsgBox(gluSE_OutD_ID.Text & "无[" & gluType.Text & "]的权限！", MsgBoxStyle.Information, "溫馨提示")
                gluSE_OutD_ID.Focus()
                DataCheckEmpty = 0
                Exit Function
            End If
        End If

        If gluSE_InD_ID.EditValue <> "" Then
            If BriC.NmetalSamplePaceTypeBriName_GetList(gluSE_InD_ID.EditValue, gluType.EditValue).Count <= 0 Then
                MsgBox(gluSE_InD_ID.Text & "无[" & gluType.Text & "]的权限！", MsgBoxStyle.Information, "溫馨提示")
                gluSE_OutD_ID.Focus()
                DataCheckEmpty = 0
                Exit Function
            End If
        End If

        '---------------------------------------------------------------------------

        '確認收發料工序是否屬生這個產品的
        If gluOutPS_NO.EditValue <> String.Empty Then
            Dim Tsplist As New List(Of NmetalSampleProcessInfo)
            Tsplist = prcon.NmetalSampleProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, Nothing, Me.gluM_Code.EditValue, Nothing, True, gluOutPS_NO.EditValue)
            If Tsplist.Count <= 0 Then
                MsgBox("此發料工序不屬于這個產品編號！", MsgBoxStyle.Information, "溫馨提示")
                gluOutPS_NO.Focus()
                DataCheckEmpty = 0
                Exit Function
            End If
        End If

        If gluInPS_NO.EditValue <> String.Empty Then
            Dim TspAlist As New List(Of NmetalSampleProcessInfo)
            TspAlist = prcon.NmetalSampleProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, Nothing, Me.gluM_Code.EditValue, Nothing, True, gluInPS_NO.EditValue)
            If TspAlist.Count <= 0 Then
                MsgBox("此收料工序不屬于這個產品編號！", MsgBoxStyle.Information, "溫馨提示")
                gluInPS_NO.Focus()
                DataCheckEmpty = 0
                Exit Function
            End If
        End If


        '权限处理---------------------------------------------------
        If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.EDIT Then
            Dim BoolPermission As Boolean = False
            Dim pmws As New PermissionModuleWarrantSubController
            Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860414")
            If pmwiL.Count > 0 Then
                If pmwiL.Item(0).PMWS_Value <> "" Then
                    Dim i, n As Integer
                    Dim arr(n) As String

                    arr = Split(pmwiL.Item(0).PMWS_Value, ",")
                    n = Len(Replace(pmwiL.Item(0).PMWS_Value, ",", "," & "*")) - Len(pmwiL.Item(0).PMWS_Value)

                    For i = 0 To n
                        If gluType.EditValue = Trim(arr(i)) Then
                            BoolPermission = True
                        End If
                    Next
                End If
            End If

            If BoolPermission = False Then
                MessageBox.Show("此用戶沒有:" + gluType.Text + "的權限", "权限提示")
                gluType.EditValue = String.Empty
                Exit Function
            End If

        End If

        Dim sptlistA As New List(Of NmetalSamplePaceInfo)
        sptlistA = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
        If sptlistA.Count > 0 Then
            If sptlistA(0).SE_OutVisible Then
                If gluSE_OutD_ID.EditValue = String.Empty Then
                    MsgBox("发料部门不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                    gluSE_OutD_ID.Focus()
                    DataCheckEmpty = 0
                    Exit Function
                End If

                If gluOutPS_NO.EditValue = String.Empty Then
                    MsgBox("发料工序不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                    gluOutPS_NO.Focus()
                    DataCheckEmpty = 0
                    Exit Function
                End If
                '显示最新误差率------------------------------------------------
                Dim dtlist As New List(Of DepartmentInfo)
                dtlist = dtCon.BriName_GetList(gluSE_OutD_ID.EditValue)
                If dtlist.Count > 0 Then
                    txtOutDwonRate.Text = dtlist(0).OutDwonRate
                    txtOutUpRate.Text = dtlist(0).OutUpRate
                Else
                    DataCheckEmpty = 0
                    MsgBox("请设置[" & gluSE_OutD_ID.Text & "]部门误差率！", MsgBoxStyle.Information, "溫馨提示")
                    Exit Function
                End If
            End If

            If sptlistA(0).SE_InVisible Then
                If gluSE_InD_ID.EditValue = String.Empty Then
                    MsgBox("收料部门不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                    gluSE_InD_ID.Focus()
                    DataCheckEmpty = 0
                    Exit Function
                End If

                If gluInPS_NO.EditValue = String.Empty Then
                    MsgBox("收料工序不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                    gluInPS_NO.Focus()
                    DataCheckEmpty = 0
                    Exit Function
                End If
                '-----------------------------------------------------------------------------------
                Dim dtlist As New List(Of DepartmentInfo)
                dtlist = dtCon.BriName_GetList(gluSE_InD_ID.EditValue)
                If dtlist.Count > 0 Then
                    txtInDwonRate.Text = dtlist(0).InDwonRate
                    txtInUpRate.Text = dtlist(0).InUpRate
                Else
                    DataCheckEmpty = 0
                    MsgBox("请设置[" & gluSE_InD_ID.Text & "]部门误差率！", MsgBoxStyle.Information, "溫馨提示")
                    Exit Function
                End If
            End If

            If sptlistA(0).SE_OutVisible And sptlistA(0).SE_InVisible Then
                If (gluSE_OutD_ID.EditValue = gluSE_InD_ID.EditValue) And (gluOutPS_NO.EditValue = gluInPS_NO.EditValue) Then
                    MsgBox("部門與工序同時相同,请重新输入！", MsgBoxStyle.Information, "溫馨提示")
                    gluInPS_NO.EditValue = String.Empty
                    gluInPS_NO.Focus()
                    DataCheckEmpty = 0
                    Exit Function
                End If
            End If

        End If

        If ds.Tables("SamplePaceBarCode").Rows.Count = 0 Then
            MsgBox("子檔不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            DataCheckEmpty = 0
            Exit Function
        End If

        ''是否刷卡
        If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.INCHECK Or EditItem = EditEnumType.EDIT Then
            If txtCardID.EditValue = String.Empty Then
                MsgBox("需要刷卡！", MsgBoxStyle.Information, "溫馨提示")
                txtCardID.Focus()
                DataCheckEmpty = 0
                Exit Function
            End If
        End If

        '确认收料是输入条码
        Dim m As Integer

        If EditItem = EditEnumType.INCHECK Then
            Dim sptlistZ As New List(Of NmetalSamplePaceInfo)
            sptlistZ = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
            If sptlistZ.Count > 0 Then
                '3.1是否要輸入確認條碼
                If sptlistZ(0).SE_InCheckBarcode Then
                    Dim strBarCode = True
                    For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                        With ds.Tables("SamplePaceBarCode")
                            strBarCode = IIf(IsDBNull(.Rows(m)("InCheck")), False, .Rows(m)("InCheck"))
                            If strBarCode = False Then
                                MsgBox("条码没输入,请输入收料条码！", MsgBoxStyle.Information, "溫馨提示")
                                Grid1.Focus()
                                GridView9.FocusedRowHandle = m
                                DataCheckEmpty = 0
                                Exit Function
                            End If

                        End With
                    Next
                End If
            End If

        End If


        '确认收料是输入条码
        Dim IntClientBarCode As Integer = 0
        Dim IntTableRowCount As Integer = ds.Tables("SamplePaceBarCode").Rows.Count
        For m = 0 To IntTableRowCount - 1
            With ds.Tables("SamplePaceBarCode")
                Dim strBarCode As String = IIf(IsDBNull(.Rows(m)("Code_ID")), String.Empty, .Rows(m)("Code_ID"))
                Dim strClientBarcode As String = IIf(IsDBNull(.Rows(m)("ClientBarcode")), String.Empty, .Rows(m)("ClientBarcode"))
                '1.--------------------------------------------------------条码验证
                Dim sclist As New List(Of NmetalSampleCollectionInfo)
                sclist = Sccon.NmetalSampleCollection_Getlist(Nothing, strBarCode, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If sclist.Count > 0 Then
                    '1.1---------------------------------------------------条码是否存在采集表里面,是否存在客户条码
                    If sclist(0).ClientBarcode <> String.Empty Then
                        MsgBox("存在客户条码！", MsgBoxStyle.Information, "溫馨提示")
                        Grid1.Focus()
                        GridView9.FocusedRowHandle = m
                        DataCheckEmpty = 0
                        Exit Function
                    End If
                    '1.2----------------------------------------------------此订单不存在此条码
                    If gluSO_ID.EditValue <> sclist(0).SO_ID Then
                        MsgBox("此订单不存在此条码！", MsgBoxStyle.Information, "溫馨提示")
                        Grid1.Focus()
                        GridView9.FocusedRowHandle = m
                        DataCheckEmpty = 0
                        Exit Function
                    End If
                    '1.3----------------------------------------------------采集表部门,不存在此条码
                    Dim splist As New List(Of NmetalSamplePaceInfo)
                    splist = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
                    If splist.Count > 0 Then
                        If splist(0).SE_OutVisible Then
                            If gluSE_OutD_ID.EditValue <> sclist(0).D_ID Then
                                MsgBox("集表部门不存在此条码！", MsgBoxStyle.Information, "溫馨提示")
                                Grid1.Focus()
                                GridView9.FocusedRowHandle = m
                                DataCheckEmpty = 0
                                Exit Function
                            End If
                        End If

                        '1.5--------------------------------------------------開料此條碼已经處理
                        If splist(0).SE_StatusTypeIsNull = True Then
                            If sclist(0).StatusType <> String.Empty Then
                                Grid1.Focus()
                                GridView9.FocusedRowHandle = m
                                lblCode.Text = "此條碼狀態為" + sclist(0).StatusTypeName
                                DataCheckEmpty = 0
                                Exit Function
                            End If
                        End If
                    End If
                    '1.4----------------------------------------------------此条码已经使用
                    If sclist(0).StatusType <> String.Empty Then
                        Dim stlist As New List(Of NmetalSampleTransactionInfo)
                        stlist = stcon.NmetalSampleTransactionType_GetList(sclist(0).StatusType, Nothing)
                        If (stlist(0).IsTransferred = False) Then
                            Grid1.Focus()
                            GridView9.FocusedRowHandle = m
                            lblCode.Text = "此条码已经" + stlist(0).StatusTypeName
                            DataCheckEmpty = 0
                            Exit Function
                        End If
                    End If
                    '1.6-------------------------------------------在发料时,检查当前,条码库存损耗
                    If txtYiChangCard.Text = "" Then
                        If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.EDIT Then
                            Dim dblErrorRate As Decimal = 0
                            If splist(0).SE_OutVisible = True Then  '当发出时
                                'dblErrorRate = FormatNumber((sclist(0).TWeight - .Rows(m)("OutWeight")) / sclist(0).TWeight * 100, 3, TriState.True)
                                '改误差范围
                                dblErrorRate = FormatNumber((sclist(0).TWeight - .Rows(m)("OutWeight")), 3, TriState.True)
                                If dblErrorRate < Val(txtOutDwonRate.Text) Or dblErrorRate > Val(Me.txtOutUpRate.Text) Then
                                    DataCheckEmpty = 0
                                    MsgBox("发料重量与系统重量误差太大,請檢查原因!" & vbCrLf & strBarCode, MsgBoxStyle.Information, "提示")
                                    Exit Function
                                End If
                            End If

                            If splist(0).SE_IsRweight = True Then  '出入,入库时也要,要与来料重量进行比较
                                dblErrorRate = FormatNumber((sclist(0).IWeight - .Rows(m)("InWeight")), 3, TriState.True)
                                If dblErrorRate < Val(txtInDwonRate.Text) Or dblErrorRate > Val(Me.txtInUpRate.Text) Then
                                    DataCheckEmpty = 0
                                    MsgBox("收料重量与系统重量误差太大,請檢查原因!" & vbCrLf & strBarCode, MsgBoxStyle.Information, "提示")
                                    Exit Function
                                End If
                            End If
                        ElseIf EditItem = EditEnumType.INCHECK Then '审核
                            If splist(0).SE_InCheckBarcode Then
                                '改误差范围
                                Dim dblErrorRate As Decimal = 0
                                dblErrorRate = FormatNumber((.Rows(m)("OutWeight") - .Rows(m)("InWeight")), 3, TriState.True)
                                If dblErrorRate < Val(txtInDwonRate.Text) Or dblErrorRate > Val(Me.txtInUpRate.Text) Then
                                    DataCheckEmpty = 0
                                    MsgBox("发料重量与收料重量的误差太大,請檢查原因!" & vbCrLf & strBarCode, MsgBoxStyle.Information, "提示")
                                    Exit Function
                                End If
                            End If
                        End If
                    End If
                    '-------------------------------------------------------------
                Else
                    MsgBox("条码不存在！", MsgBoxStyle.Information, "溫馨提示")
                    Grid1.Focus()
                    GridView9.FocusedRowHandle = m
                    DataCheckEmpty = 0
                    Exit Function
                End If


                '2.此客戶條碼<采集表>存在---------------------------
                If strClientBarcode <> String.Empty Then
                    '2.1此客戶條碼<采集表>存在!
                    If Sccon.NmetalSampleCollection_GetID(UCase(strClientBarcode)) = True Then
                        MessageBox.Show(UCase(strClientBarcode) + "此客戶條碼<采集表>存在!", "提示")
                        Grid1.Focus()
                        GridView9.FocusedRowHandle = m
                        DataCheckEmpty = 0
                        Exit Function
                    End If
                    '2.2客户条码是否全部有
                    IntClientBarCode = IntClientBarCode + 1
                End If

                '4.条码是否正在使用中李超20140116加
                Dim somAlist As New List(Of NmetalSamplePaceInfo)
                somAlist = pacon.NmetalSamplePaceBarCode_Getlist2(strBarCode, LabSE_NO.Text)
                If somAlist.Count > 0 Then
                    Grid1.Focus()
                    GridView9.FocusedRowHandle = m
                    lblCode.Text = "条码正在使用中,请查明原因"
                    Exit Function
                End If

            End With
        Next

        '1.输入客户条码时一定要
        If (IntTableRowCount - IntClientBarCode = 0) Or IntClientBarCode = 0 Then
        Else
            MsgBox("有" + CStr(IntTableRowCount - IntClientBarCode) + "条客户条码没有输入！", MsgBoxStyle.Information, "溫馨提示")
            Exit Function
        End If

        '------------------------------------------------------------扣账库存查询---李超20131217修正
        Dim sptlist As New List(Of NmetalSamplePaceInfo)
        sptlist = spCon.NmetalSamplePaceType_Getlist(gluType.EditValue)
        If sptlist.Count > 0 Then
            If sptlist(0).SE_OutVisible Then
                Dim SwInfo As New NmetalSampleWareInventoryInfo
                Dim SwList As New List(Of NmetalSampleWareInventoryInfo)
                Dim intinQty As Integer = 0
                Dim doubWeitht As Decimal = 0
                SwList = SwCon.NmetalSampleWareInventory_Getlist(Nothing, Nothing, gluOutPS_NO.EditValue, Nothing, False, gluSE_OutD_ID.EditValue)
                If SwList.Count > 0 Then
                    intinQty = SwList(0).SWI_Qty
                    doubWeitht = SwList(0).SWI_Weight
                End If
                If intinQty < CInt(txtOutInQty.Text) Then
                    MsgBox("发料時庫存数量小于發料數量！", MsgBoxStyle.Information, "溫馨提示")
                    DataCheckEmpty = 0
                    Exit Function
                End If

                'If doubWeitht < Val(lblWightOut.Text) Then
                '    MsgBox("发料時庫存重量小于發料量！", MsgBoxStyle.Information, "溫馨提示")
                '    DataCheckEmpty = 0
                '    Exit Function
                'End If
            End If
        End If

        DataCheckEmpty = 1
    End Function
#End Region

#Region "控件事件"
    Private Sub gluOutPS_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluOutPS_NO.EditValueChanged
        If gluOutPS_NO.EditValue <> String.Empty Then
            Dim SwList As New List(Of NmetalSampleWareInventoryInfo)
            Dim intinQty As Integer = 0
            SwList = SwCon.NmetalSampleWareInventory_Getlist(Nothing, Nothing, gluOutPS_NO.EditValue, Nothing, False, gluSE_OutD_ID.EditValue)

            If SwList.Count > 0 Then
                lblQtyOut.Text = SwList(0).SWI_Qty
                lblWightOut.Text = SwList(0).SWI_Weight
            Else
                lblQtyOut.Text = 0
                lblWightOut.Text = 0
            End If
        End If
    End Sub

    Private Sub gluInPS_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluInPS_NO.EditValueChanged
        If gluInPS_NO.EditValue <> String.Empty Then
            Dim SwList As New List(Of NmetalSampleWareInventoryInfo)
            Dim intinQty As Integer = 0
            SwList = SwCon.NmetalSampleWareInventory_Getlist(Nothing, Nothing, gluInPS_NO.EditValue, Nothing, False, gluSE_InD_ID.EditValue)

            If SwList.Count > 0 Then
                lblQtyIn.Text = SwList(0).SWI_Qty
                lblWightIn.Text = SwList(0).SWI_Weight
            Else
                lblQtyIn.Text = 0
                lblWightIn.Text = 0
            End If
        End If
    End Sub

    Private Sub gluSE_OutD_ID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluSE_OutD_ID.EditValueChanged
        txtOutDwonRate.Text = 0
        txtOutUpRate.Text = 0

        If gluSE_OutD_ID.EditValue <> String.Empty Then
            Dim SwList As New List(Of NmetalSampleWareInventoryInfo)
            Dim intinQty As Integer = 0
            SwList = SwCon.NmetalSampleWareInventory_Getlist(Nothing, Nothing, gluOutPS_NO.EditValue, Nothing, False, gluSE_OutD_ID.EditValue)
            If SwList.Count > 0 Then
                lblQtyOut.Text = SwList(0).SWI_Qty
                lblWightOut.Text = SwList(0).SWI_Weight
            Else
                lblQtyOut.Text = 0
                lblWightOut.Text = 0
            End If

            Dim dtlist As New List(Of DepartmentInfo)
            dtlist = dtCon.BriName_GetList(gluSE_OutD_ID.EditValue)
            If dtlist.Count > 0 Then
                txtOutDwonRate.Text = dtlist(0).OutDwonRate
                txtOutUpRate.Text = dtlist(0).OutUpRate
            End If
        End If
    End Sub

    Private Sub gluSE_InD_ID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluSE_InD_ID.EditValueChanged
        txtInDwonRate.Text = 0
        txtInUpRate.Text = 0

        If gluSE_InD_ID.EditValue <> String.Empty Then

            Dim SwList As New List(Of NmetalSampleWareInventoryInfo)
            Dim intinQty As Integer = 0
            SwList = SwCon.NmetalSampleWareInventory_Getlist(Nothing, Nothing, gluInPS_NO.EditValue, Nothing, False, gluSE_InD_ID.EditValue)

            If SwList.Count > 0 Then
                lblQtyIn.Text = SwList(0).SWI_Qty
                lblWightIn.Text = SwList(0).SWI_Weight
            Else
                lblQtyIn.Text = 0
                lblWightIn.Text = 0
            End If

            Dim dtlist As New List(Of DepartmentInfo)
            dtlist = dtCon.BriName_GetList(gluSE_InD_ID.EditValue)

            If dtlist.Count > 0 Then
                txtInDwonRate.Text = dtlist(0).InDwonRate
                txtInUpRate.Text = dtlist(0).InUpRate
            End If
        End If
    End Sub

    Private Sub gluM_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluM_Code.EditValueChanged

        If gluM_Code.EditValue <> String.Empty Then
            Dim splist As New List(Of NmetalSampleProcessInfo)
            splist = prcon.NmetalSampleProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, Nothing, Me.gluM_Code.EditValue, Nothing, True, Nothing)
            'gluOutPS_NO.Properties.ValueMember = "PS_NO"
            'gluOutPS_NO.Properties.DisplayMember = "PS_Name"
            gluOutPS_NO.Properties.DataSource = splist
            'gluInPS_NO.Properties.ValueMember = "PS_NO"
            'gluInPS_NO.Properties.DisplayMember = "PS_Name"
            gluInPS_NO.Properties.DataSource = splist
            SetPSDataSource()
        End If

    End Sub

    Private Sub txtSealCode_ID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSealCode_ID.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPK_CodeID.Focus()
        End If
    End Sub

    Private Sub gluSE_OutD_ID_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluSE_OutD_ID.Leave
        SetPSDataSource()
    End Sub

    Private Sub SetPSDataSource()
        '--------------------------------------------處理工序有數量的帶出
        If gluPM_M_Code.EditValue <> String.Empty And Me.gluM_Code.EditValue <> String.Empty And gluSE_OutD_ID.EditValue <> String.Empty Then
            Dim splist As New List(Of NmetalSampleProcessInfo)
            splist = prcon.NmetalSampleProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, Nothing, Me.gluM_Code.EditValue, Nothing, Nothing, Nothing)
            If splist.Count > 0 Then
                Dim i As Integer = 0

                ds.Tables("SamplePS").Clear()
                For i = 0 To splist.Count - 1
                    Dim SwsList As New List(Of NmetalSampleWareInventoryInfo)
                    SwsList = SwCon.NmetalSampleWareInventory_Getlist(Nothing, Nothing, splist(i).PS_NO, Nothing, False, gluSE_OutD_ID.EditValue)
                    If SwsList.Count > 0 Then
                        If SwsList(0).SWI_Qty > 0 Then ''李超修改
                            Dim row As DataRow
                            row = ds.Tables("SamplePS").NewRow
                            row("PS_Num") = splist(i).PS_Num
                            row("PS_NO") = splist(i).PS_NO
                            row("PS_Name") = splist(i).PS_Name
                            row("PS_NoName") = splist(i).PS_NoName
                            ds.Tables("SamplePS").Rows.Add(row)
                        End If
                    End If
                Next
                gluOutPS_NO.Properties.DataSource = ds.Tables("SamplePS")
            End If
        End If
    End Sub


    Private Sub txtWeight_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWeight.KeyDown
        If e.KeyCode = Keys.Enter Then
            Select Case EditItem
                Case EditEnumType.OUT
                    BarCode()
                Case EditEnumType.EDIT
                    BarCode()
                Case EditEnumType.INCHECK
                    BarCodeInCheck()
            End Select
        End If
    End Sub


    Private Sub GridView9_CustomDrawCell(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridView9.CustomDrawCell
        On Error Resume Next
        If e.Column.FieldName = "OutErrorRate" Then
            If Val(e.CellValue) < Val(txtOutDwonRate.Text) Or Val(e.CellValue) > Val(Me.txtOutUpRate.Text) Then
                e.Appearance.BackColor = Color.Red
            End If
        End If

        If e.Column.FieldName = "ErrorRate" Then
            If Val(e.CellValue) < Val(Me.txtInDwonRate.Text) Or Val(e.CellValue) > Val(Me.txtInUpRate.Text) Then
                e.Appearance.BackColor = Color.Red
            End If
        End If

    End Sub


#End Region

#Region "自动流水单号"
    Function GetSE_ID() As String
        Dim oc As New NmetalSamplePaceControler
        Dim oi As New NmetalSamplePaceInfo
        Dim StrSE As String
        StrSE = "SE" & Format(Now, "yyMM")
        oi = oc.NmetalSamplePace_GetID(StrSE)
        If oi Is Nothing Then
            GetSE_ID = "SE" + Format(Now, "yyMM") + "0001"
        Else
            GetSE_ID = "SE" + Format(Now, "yyMM") + Mid(CStr(CInt(Mid(oi.SE_ID, 7)) + 1000000001), 6)
        End If
    End Function
#End Region

#Region "刷卡事件"
    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        txtCardID.Text = ReadCard() '讀取卡號李超
        If txtCardID.Text <> "" Then
            'chkFP_OutOK.Checked = True
        End If
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        'Select Case InUserID
        '    Case "12022419"
        '        txtCardID.Text = "12022419"
        '    Case "14021002"
        '        txtCardID.Text = "14021002"
        'End Select

        '2014-08-22     Mark
        Dim frm As New frmNmetalSampleException
        frm.EditItem = EditEnumType.ELSEONE
        frm.ShowDialog()

        txtCardID.Text = tempValue
        tempValue = ""
    End Sub
#End Region

#Region "特殊卡"

    ''' <summary>
    ''' 在超标,不可保存时用
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub ButtonYichang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonYichang.Click
        'Dim TempCardID As String
        'TempCardID = ReadCard1()
        'If InUserID = "14021002" Then
        '    TempCardID = "14021002"
        'ElseIf InUserID = "13021010" Then
        '    TempCardID = "13021010"
        'End If
        ' ''--------------------
        'Dim bc As New NmetalSamplePaceTypeBriNameControl
        'Dim bL As New List(Of NmetalSamplePaceTypeBriNameInfo)
        'bL = bc.NmetalSampleExceptionUser_GetList(TempCardID)
        'If bL.Count > 0 Then
        '    txtYiChangCard.Text = bL(0).PE_User & "-" & bL(0).PE_Name
        'Else
        '    txtYiChangCard.Text = ""
        'End If

        '2014-08-22     Mark
        Dim frm As New frmNmetalSampleException
        frm.EditItem = EditEnumType.ELSETWO
        frm.ShowDialog()

        txtYiChangCard.Text = tempValue
        tempValue = ""
    End Sub

#End Region

#Region "稱重"
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        txtWeight.Enabled = False
        Timer1.Enabled = True

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "8601")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                txtWeight.Enabled = True
                Timer1.Enabled = False
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        txtWeight.Text = WeightTime
    End Sub

    'Private Sub ButtonWeight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonWeight.Click
    '    frmWightComm.Show()
    'End Sub
#End Region


End Class