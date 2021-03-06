Imports LFERP.Library.MrpManager.MrpForecastOrder
Imports LFERP.Library.MrpManager.Bom_M
Imports LFERP.Library.MrpManager.MrpWareHouseInfo
Imports LFERP.Library.MrpManager.MrpMaterialCode
Imports LFERP.Library.MrpManager.MrpInfo
Imports LFERP.Library.MrpManager.MrpPurchaseRecord
Imports LFERP.Library.MrpManager.MrpPurchaseOrder
Imports LFERP.Library.MrpManager.MrpMps
Imports LFERP.DataSetting
Public Class MrpReportSelect
#Region "属性"
    Private _intShowPage As Integer = 0
    Public Property intShowPage() As Integer    ''''獲取審核備註
        Get
            Return _intShowPage
        End Get
        Set(ByVal value As Integer)
            _intShowPage = value
        End Set
    End Property
#End Region

#Region "窗体载入"
    Private Sub MrpReportSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        xtcTable.TabPages(intShowPage).PageVisible = True
        xtcTable.SelectedTabPageIndex = intShowPage

        Select Case intShowPage
            Case 0
                Dim MrpCon As New MrpForecastOrderController
                glu_ForecastID_FO.Properties.DisplayMember = "ForecastID"
                glu_ForecastID_FO.Properties.ValueMember = "ForecastID"
                glu_ForecastID_FO.Properties.DataSource = MrpCon.MrpForecastOrder_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                glu_MO_CusterID_FO.Properties.DisplayMember = "MO_CusterName"    'txt
                glu_MO_CusterID_FO.Properties.ValueMember = "MO_CusterID"   'EditValue
                glu_MO_CusterID_FO.Properties.DataSource = MrpCon.CusterGetName(Nothing, Nothing)
                glu_ForecastID_FO.Enabled = True
                Lbl_Title.Text = "MRP預測訂單報表列印"
                Me.Text = "MRP預測訂單報表列印"
            Case 1
                Dim MMICcon As New MrpMaterialCodeController
                glu_M_Code_BOM.Properties.DisplayMember = "M_Code"
                glu_M_Code_BOM.Properties.ValueMember = "M_Code"
                glu_M_Code_BOM.Properties.DataSource = MMICcon.MrpMaterialCode_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                GridLookUpEdit1View.ActiveFilterString = "M_Code not like '1%' and M_Code not like '2%'"
                glu_M_Code_BOM.Enabled = True
                Lbl_Title.Text = "BOM結構報表列印"
                Me.Text = "BOM結構報表列印"

            Case 2
                Dim MWHIcon As New MrpWareHouseInfoController
                glu_Ware_ID_WH.Properties.DisplayMember = "Ware_ID"
                glu_Ware_ID_WH.Properties.ValueMember = "Ware_ID"
                glu_Ware_ID_WH.Properties.DataSource = MWHIcon.MrpWareHouseInfo_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                Dim MPRcon As New MrpInfoController
                gul_MRPID__WH.Properties.DisplayMember = "MRP_ID"    'txt
                gul_MRPID__WH.Properties.ValueMember = "MRP_ID"   'EditValue
                gul_MRPID__WH.Properties.DataSource = MPRcon.MrpInfo_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                glu_Ware_ID_WH.Enabled = True
                Lbl_Title.Text = "MRP庫存記錄報表列印"
                Me.Text = "Mrp預測訂單報表列印"

            Case 3
                Dim MMICcon As New MrpMaterialCodeController
                glu_M_Code_MC.Properties.DisplayMember = "M_Name"
                glu_M_Code_MC.Properties.ValueMember = "M_Code"
                glu_M_Code_MC.Properties.DataSource = MMICcon.MrpMaterialCode_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                Dim sccon As New SuppliersControler
                glu_S_Supplier_MC.Properties.DisplayMember = "S_SupplierName"
                glu_S_Supplier_MC.Properties.ValueMember = "S_Supplier"
                glu_S_Supplier_MC.Properties.DataSource = sccon.Suppliers_GetDataTable(Nothing)

                glu_M_Code_MC.Enabled = True
                Lbl_Title.Text = "MRP物料編碼報表列印"
                Me.Text = "MRP物料編碼報表列印"

            Case 4
                Dim MICon As New MrpInfoController
                glu_MRP_ID_MI.Properties.DisplayMember = "MRP_ID"
                glu_MRP_ID_MI.Properties.ValueMember = "MRP_ID"
                glu_MRP_ID_MI.Properties.DataSource = MICon.MrpInfo_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                glu_MRP_ID_MI.Enabled = True
                Lbl_Title.Text = "MRP需求運算報表列印"
                Me.Text = "MRP需求運算報表列印"

            Case 5

                Dim MPRcon As New MrpPurchaseRecordController
                glu_MrpPurchaseID_PR.Properties.DisplayMember = "MrpPurchaseID"
                glu_MrpPurchaseID_PR.Properties.ValueMember = "MrpPurchaseID"
                glu_MrpPurchaseID_PR.Properties.DataSource = MPRcon.MrpPurchaseRecord_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                Dim MPRInfocon As New MrpInfoController
                glu_Mrp_ID_PR.Properties.DisplayMember = "MRP_ID"
                glu_Mrp_ID_PR.Properties.ValueMember = "MRP_ID"
                glu_Mrp_ID_PR.Properties.DataSource = MPRInfocon.MrpInfo_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                Dim dptCon As New DepartmentControler
                glu_Department_PR.Properties.DisplayMember = "DPT_Name"
                glu_Department_PR.Properties.ValueMember = "DPT_ID"
                glu_Department_PR.Properties.DataSource = dptCon.Department_GetList(Nothing, Nothing, Nothing)

                glu_MrpPurchaseID_PR.Enabled = True
                Lbl_Title.Text = "MRP請購申請報表列印"
                Me.Text = "MRP請購申請報表列印"

            Case 6
                Dim mpoc As New MrpPurchaseOrderController
                glu_PO_PO.Properties.DisplayMember = "PO"
                glu_PO_PO.Properties.ValueMember = "PO"
                glu_PO_PO.Properties.DataSource = mpoc.MrpPurchaseOrder_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                Dim MPRcon As New MrpPurchaseRecordController
                glu_PR_PO.Properties.DisplayMember = "MrpPurchaseID"
                glu_PR_PO.Properties.ValueMember = "MrpPurchaseID"
                glu_PR_PO.Properties.DataSource = MPRcon.MrpPurchaseRecord_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                Dim sccon As New SuppliersControler
                glu_SupplierID_PO.Properties.DisplayMember = "S_SupplierName"
                glu_SupplierID_PO.Properties.ValueMember = "S_Supplier"
                glu_SupplierID_PO.Properties.DataSource = sccon.Suppliers_GetDataTable(Nothing)

                glu_PO_PO.Enabled = True
                Lbl_Title.Text = "MRP採購單報表列印"
                Me.Text = "MRP採購單報表列印"

            Case 7
                Dim MMCon As New MrpMpsController
                glu_MOID_MM.Properties.DisplayMember = "MO"
                glu_MOID_MM.Properties.ValueMember = "MO"
                glu_MOID_MM.Properties.DataSource = MMCon.MrpMps_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                Dim MMICcon As New MrpMaterialCodeController
                glu_M_Code_MM.Properties.DisplayMember = "M_Name"
                glu_M_Code_MM.Properties.ValueMember = "M_Code"
                glu_M_Code_MM.Properties.DataSource = MMICcon.MrpMaterialCode_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                glu_MOID_MM.Enabled = True
                Lbl_Title.Text = "主生產計劃報表列印"
                Me.Text = "主生產計劃報表列印"

        End Select
    End Sub
#End Region

#Region "报表"
    Private Sub PrintRPT(ByVal strDatasetName As String, ByVal list As Object, ByVal strRPTName As String, ByVal strSend As String, ByVal TitleName As String)
        Try
            Dim dss As New DataSet
            Dim ltc1 As New CollectionToDataSet
            If strSend = String.Empty Then
                strSend = InUser
            End If
            ltc1.CollToDataSet(dss, strDatasetName, list)
            PreviewRPT1(dss, strRPTName, strSend, strSend, TitleName, True, True)
            ltc1 = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

#Region "列印"
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Try
            Dim strDatasetName As String = String.Empty
            Dim strRPTName As String = String.Empty
            Dim strTitleName As String = String.Empty
            Dim strSend As String = String.Empty
            Dim strCheck As String = String.Empty

            Select Case intShowPage
                Case 0
                    If cb_Select_FO.SelectedIndex = 2 Then
                        If CheckDate(de_StartDate_FO.EditValue, de_EndDate_FO.EditValue) = False Then
                            Exit Sub
                        End If
                    End If

                    Dim MrpCon As New MrpForecastOrderController
                    strDatasetName = "MrpForecastOrder"
                    strRPTName = "rptMrpForecastOrderAll"
                    strTitleName = "MRP預測訂單信息報表"
                    strSend = String.Empty

                    Select Case cb_Check_FO.SelectedIndex
                        Case 0
                            strCheck = Nothing
                        Case 1
                            strCheck = "True"
                        Case 2
                            strCheck = "False"
                    End Select
                    PrintRPT(strDatasetName, MrpCon.MrpForecastOrder_GetList(glu_ForecastID_FO.EditValue, de_StartDate_FO.EditValue, de_EndDate_FO.EditValue, strCheck, Nothing, glu_MO_CusterID_FO.EditValue, Nothing), strRPTName, strSend, strTitleName)

                Case 1
                    If cb_Select_BOM.SelectedIndex = 2 Then
                        If CheckDate(de_startDate_BOM.EditValue, de_endDate_BOM.EditValue) = False Then
                            Exit Sub
                        End If
                    End If

                    Dim BOCon As New Bom_MController
                    strDatasetName = "Bom_M"
                    strRPTName = "rptBomAll"
                    strTitleName = "BOM結構信息報表"
                    strSend = String.Empty
                    Select Case cb_IsCheck_BOM.SelectedIndex
                        Case 0
                            strCheck = Nothing
                        Case 1
                            strCheck = "True"
                        Case 2
                            strCheck = "False"
                    End Select

                    '  PrintRPT(strDatasetName, BOCon.Bom_M_GetList(glu_M_Code_BOM.EditValue, Nothing, de_startDate_BOM.EditValue, de_endDate_BOM.EditValue, Nothing, Nothing, Nothing, strCheck, Nothing), strRPTName, strSend, strTitleName)
                Case 2
                    If cb_Select_WH.SelectedIndex = 3 Then
                        If CheckDate(de_BeginDate_WH.EditValue, de_EndDate_WH.EditValue) = False Then
                            Exit Sub
                        End If
                    End If
                    Dim MWHIcon As New MrpWareHouseInfoController
                    Dim Check As String = String.Empty
                    strDatasetName = "MrpWareHouseInfo"
                    strRPTName = "rptMrpWareHouseAll"
                    strTitleName = "MRP庫存信息報表"
                    strSend = String.Empty
                    Select Case cb_Ischeck_WH.SelectedIndex
                        Case 0
                            strCheck = Nothing
                        Case 1
                            strCheck = "True"
                        Case 2
                            strCheck = "False"
                    End Select

                    PrintRPT(strDatasetName, MWHIcon.MrpWareHouseInfo_GetList(glu_Ware_ID_WH.EditValue, gul_MRPID__WH.EditValue, strCheck, Nothing, de_BeginDate_WH.EditValue, de_EndDate_WH.EditValue, Nothing), strRPTName, strSend, strTitleName)

                Case 3
                    If cb_Select_MC.SelectedIndex = 3 Then
                        If CheckDate(de_BeginDate_MC.EditValue, de_EndDate_MC.EditValue) = False Then
                            Exit Sub
                        End If
                    End If
                    Dim MMCcon As New MrpMaterialCodeController
                    strDatasetName = "MrpMaterialCode"
                    strRPTName = "rptMrpMaterialCodeAll"
                    strTitleName = "MRP物料編碼信息報表"
                    strSend = String.Empty
                    Select Case cb_CheckBit_MC.SelectedIndex
                        Case 0
                            strCheck = Nothing
                        Case 1
                            strCheck = "True"
                        Case 2
                            strCheck = "False"
                    End Select
                    PrintRPT(strDatasetName, MMCcon.MrpMaterialCode_GetList(glu_M_Code_MC.EditValue, Nothing, strCheck, Nothing, Nothing, Nothing, de_BeginDate_MC.EditValue, de_EndDate_MC.EditValue, glu_S_Supplier_MC.EditValue, Nothing), strRPTName, strSend, strTitleName)
                Case 4

                    If cb_Select_MI.SelectedIndex = 4 Then
                        If CheckDate(de_BeginDate_MI.EditValue, de_EndDate_MI.EditValue) = False Then
                            Exit Sub
                        End If
                    End If

                    Dim micon As New MrpInfoController
                    strDatasetName = "MrpInfo" ''''
                    strRPTName = "rptMrpInfoAll" ''''
                    strTitleName = "MRP運算信息報表"
                    strSend = String.Empty

                    Select Case cb_IsCheck_MI.SelectedIndex
                        Case 0
                            strCheck = Nothing
                        Case 1
                            strCheck = "True"
                        Case 2
                            strCheck = "False"
                    End Select
                    PrintRPT(strDatasetName, micon.MrpInfo_GetList(glu_MRP_ID_MI.EditValue, de_BeginDate_MI.EditValue, Nothing, Nothing, Nothing, de_EndDate_MI.EditValue, glu_MI_CalcType_MI.EditValue, glu_MI_MRPType_MI.EditValue, strCheck, Nothing), strRPTName, strSend, strTitleName)

                Case 5
                    If cb_Select_PR.SelectedIndex = 3 Then
                        If CheckDate(de_BeginDate_PR.EditValue, de_EndDate_PR.EditValue) = False Then
                            Exit Sub
                        End If
                    End If

                    Dim strPurchase As String = String.Empty
                    Dim strUrgent As String = String.Empty

                    Select Case cb_IsPurchase_PR.SelectedIndex
                        Case 0
                            strPurchase = Nothing
                        Case 1
                            strPurchase = "True"
                        Case 2
                            strPurchase = "False"
                    End Select

                    Select Case cb_IsUrgent_PR.SelectedIndex
                        Case 0
                            strUrgent = Nothing
                        Case 1
                            strUrgent = "True"
                        Case 2
                            strUrgent = "False"
                    End Select

                    Select Case cb_IsCheck_PR.SelectedIndex
                        Case 0
                            strCheck = Nothing
                        Case 1
                            strCheck = "True"
                        Case 2
                            strCheck = "False"
                    End Select

                    Dim mprCon As New MrpPurchaseRecordController
                    strDatasetName = "MrpPurchaseRecord"
                    strRPTName = "rptMrpPurchaseRecordAll"
                    strTitleName = "MRP請購信息報表"
                    strSend = String.Empty
                    PrintRPT(strDatasetName, mprCon.MrpPurchaseRecord_GetList(glu_MrpPurchaseID_PR.EditValue, glu_Mrp_ID_PR.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, strCheck, Nothing, Nothing, strUrgent, glu_Department_PR.EditValue, de_BeginDate_PR.EditValue, de_EndDate_PR.EditValue, strPurchase, Nothing), strRPTName, strSend, strTitleName)

                Case 6

                    If cb_Select_PO.SelectedIndex = 3 Then
                        If CheckDate(de_BeginDate_PO.EditValue, de_EndDate_PO.EditValue) = False Then
                            Exit Sub
                        End If
                    End If

                    Dim strReCheckBit As String = String.Empty
                    Dim strUrgency As String = String.Empty
                    Select Case cb_ReCheckBit_PO.SelectedIndex
                        Case 0
                            strReCheckBit = Nothing
                        Case 1
                            strReCheckBit = "True"
                        Case 2
                            strReCheckBit = "False"
                    End Select
                    Select Case cb_IsUrgency_PO.SelectedIndex
                        Case 0
                            strUrgency = Nothing
                        Case 1
                            strUrgency = "True"
                        Case 2
                            strUrgency = "False"
                    End Select
                    Select Case cb_IsCheck_PO.SelectedIndex
                        Case 0
                            strCheck = Nothing
                        Case 1
                            strCheck = "True"
                        Case 2S
                            strCheck = "False"
                    End Select

                    Dim mpoc As New MrpPurchaseOrderController
                    strDatasetName = "MrpPurchaseOrder"
                    strRPTName = "rptMrpPurchaseOrderAll"
                    strTitleName = "MRP採購信息報表"
                    strSend = String.Empty
                    PrintRPT(strDatasetName, mpoc.MrpPurchaseOrder_GetList(glu_PO_PO.EditValue, de_BeginDate_PO.EditValue, Nothing, Nothing, Nothing, Nothing, glu_PR_PO.EditValue, glu_SupplierID_PO.EditValue, de_EndDate_PO.EditValue, strCheck, strUrgency, strReCheckBit, Nothing), strRPTName, strSend, strTitleName)
                Case 7
                    If cb_Select_MM.SelectedIndex = 3 Then
                        If CheckDate(de_StarDate_MM.EditValue, de_EndDate_MM.EditValue) = False Then
                            Exit Sub
                        End If
                    End If
                    Dim MMCon As New MrpMpsController
                    Dim Check As String = String.Empty
                    strDatasetName = "MrpMps"
                    strRPTName = "rptMrpMpsAll"
                    strTitleName = "生產主計劃信息報表"
                    strSend = String.Empty
                    Select Case cb_IsCheck_MM.SelectedIndex
                        Case 0
                            strCheck = Nothing
                        Case 1
                            strCheck = "True"
                        Case 2
                            strCheck = "False"
                    End Select
                    PrintRPT(strDatasetName, MMCon.MrpMps_GetList(glu_MOID_MM.EditValue, glu_M_Code_MM.EditValue, strCheck, de_StarDate_MM.EditValue, de_EndDate_MM.EditValue, Nothing, Nothing), strRPTName, strSend, strTitleName)
            End Select
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
#End Region

#Region "退出"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
#End Region

#Region "控件事件"


    Private Sub cbSelect_Forecast_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_Select_FO.SelectedIndexChanged
        de_EndDate_FO.Enabled = False
        de_StartDate_FO.Enabled = False
        glu_MO_CusterID_FO.Enabled = False
        glu_ForecastID_FO.Enabled = False
        cb_Check_FO.Enabled = False

        de_EndDate_FO.EditValue = Nothing
        de_StartDate_FO.EditValue = Nothing
        glu_MO_CusterID_FO.EditValue = Nothing
        glu_ForecastID_FO.EditValue = Nothing
        cb_Check_FO.SelectedIndex = 0
        Select Case cb_Select_FO.SelectedIndex
            Case 0
                glu_ForecastID_FO.Enabled = True
            Case 1
                glu_MO_CusterID_FO.Enabled = True
            Case 2
                de_StartDate_FO.Enabled = True
                de_EndDate_FO.Enabled = True
                de_StartDate_FO.EditValue = DefaultDate(True)
                de_EndDate_FO.EditValue = DefaultDate(False)
            Case 3
                cb_Check_FO.Enabled = True
        End Select
    End Sub

    Private Sub cbSelect_Bom_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_Select_BOM.SelectedIndexChanged
        glu_M_Code_BOM.Enabled = False
        cb_IsCheck_BOM.Enabled = False
        de_startDate_BOM.Enabled = False
        de_endDate_BOM.Enabled = False

        glu_M_Code_BOM.EditValue = Nothing
        cb_IsCheck_BOM.SelectedIndex = 0
        de_startDate_BOM.EditValue = Nothing
        de_endDate_BOM.EditValue = Nothing

        Select Case cb_Select_BOM.SelectedIndex
            Case 0
                glu_M_Code_BOM.Enabled = True
            Case 1
                cb_IsCheck_BOM.Enabled = True
            Case 2
                de_startDate_BOM.Enabled = True
                de_endDate_BOM.Enabled = True
                de_startDate_BOM.EditValue = DefaultDate(True)
                de_endDate_BOM.EditValue = DefaultDate(False)

        End Select
    End Sub
    Private Sub cbSelect_WareHouse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_Select_WH.SelectedIndexChanged
        glu_Ware_ID_WH.Enabled = False
        gul_MRPID__WH.Enabled = False
        cb_Ischeck_WH.Enabled = False
        de_EndDate_WH.Enabled = False
        de_BeginDate_WH.Enabled = False

        glu_Ware_ID_WH.EditValue = Nothing
        gul_MRPID__WH.EditValue = Nothing
        cb_Ischeck_WH.SelectedIndex = 0
        de_EndDate_WH.EditValue = Nothing
        de_BeginDate_WH.EditValue = Nothing

        Select Case cb_Select_WH.SelectedIndex
            Case 0
                glu_Ware_ID_WH.Enabled = True
            Case 1
                gul_MRPID__WH.Enabled = True
            Case 2
                cb_Ischeck_WH.Enabled = True
            Case 3
                de_BeginDate_WH.Enabled = True
                de_EndDate_WH.Enabled = True
                de_BeginDate_WH.EditValue = DefaultDate(True)
                de_EndDate_WH.EditValue = DefaultDate(False)
        End Select
    End Sub

    Private Sub cbSelect_MateralCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_Select_MC.SelectedIndexChanged

        glu_M_Code_MC.Enabled = False
        glu_S_Supplier_MC.Enabled = False
        cb_CheckBit_MC.Enabled = False
        de_BeginDate_MC.Enabled = False
        de_EndDate_MC.Enabled = False

        glu_M_Code_MC.EditValue = Nothing
        glu_S_Supplier_MC.EditValue = Nothing
        cb_CheckBit_MC.SelectedIndex = 0
        de_BeginDate_MC.EditValue = Nothing
        de_EndDate_MC.EditValue = Nothing

        Select Case cb_Select_MC.SelectedIndex
            Case 0
                glu_M_Code_MC.Enabled = True
            Case 1
                glu_S_Supplier_MC.Enabled = True
            Case 2
                cb_CheckBit_MC.Enabled = True
            Case 3
                de_BeginDate_MC.Enabled = True
                de_EndDate_MC.Enabled = True
                de_BeginDate_MC.EditValue = DefaultDate(True)
                de_EndDate_MC.EditValue = DefaultDate(False)
        End Select

    End Sub
    Private Sub cbSelect_MrpInfo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_Select_MI.SelectedIndexChanged
        glu_MRP_ID_MI.Enabled = False
        glu_MI_CalcType_MI.Enabled = False
        glu_MI_MRPType_MI.Enabled = False
        cb_IsCheck_MI.Enabled = False
        de_BeginDate_MI.Enabled = False
        de_EndDate_MI.Enabled = False

        glu_MRP_ID_MI.EditValue = Nothing
        glu_MI_CalcType_MI.EditValue = Nothing
        glu_MI_MRPType_MI.EditValue = Nothing
        cb_IsCheck_MI.SelectedIndex = 0
        de_BeginDate_MI.EditValue = Nothing
        de_EndDate_MI.EditValue = Nothing

        Select Case cb_Select_MI.SelectedIndex
            Case 0
                glu_MRP_ID_MI.Enabled = True
            Case 1
                glu_MI_CalcType_MI.Enabled = True
            Case 2
                glu_MI_MRPType_MI.Enabled = True
            Case 3
                cb_IsCheck_MI.Enabled = True
            Case 4
                de_BeginDate_MI.Enabled = True
                de_EndDate_MI.Enabled = True
                de_BeginDate_MI.EditValue = DefaultDate(True)
                de_EndDate_MI.EditValue = DefaultDate(False)
        End Select
    End Sub

    Private Sub cbSelect_Record_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_Select_PR.SelectedIndexChanged
        glu_MrpPurchaseID_PR.Enabled = False
        glu_Mrp_ID_PR.Enabled = False
        glu_Department_PR.Enabled = False
        de_BeginDate_PR.Enabled = False
        de_EndDate_PR.Enabled = False
        cb_IsPurchase_PR.Enabled = False
        cb_IsUrgent_PR.Enabled = False
        cb_IsCheck_PR.Enabled = False

        glu_MrpPurchaseID_PR.EditValue = Nothing
        glu_Mrp_ID_PR.EditValue = Nothing
        glu_Department_PR.EditValue = Nothing
        de_BeginDate_PR.EditValue = Nothing
        de_EndDate_PR.EditValue = Nothing
        cb_IsPurchase_PR.SelectedIndex = 0
        cb_IsUrgent_PR.SelectedIndex = 0
        cb_IsCheck_PR.SelectedIndex = 0
        Select Case cb_Select_PR.SelectedIndex
            Case 0
                glu_MrpPurchaseID_PR.Enabled = True
            Case 1
                glu_Mrp_ID_PR.Enabled = True
            Case 2
                glu_Department_PR.Enabled = True
            Case 3
                de_BeginDate_PR.Enabled = True
                de_EndDate_PR.Enabled = True
                de_BeginDate_PR.EditValue = DefaultDate(True)
                de_EndDate_PR.EditValue = DefaultDate(False)
            Case 4
                cb_IsPurchase_PR.Enabled = True
            Case 5
                cb_IsUrgent_PR.Enabled = True
            Case 6
                cb_IsCheck_PR.Enabled = True
        End Select
    End Sub

    Private Sub cbe_Select_PO_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_Select_PO.SelectedIndexChanged
        glu_PO_PO.Enabled = False
        glu_PR_PO.Enabled = False
        glu_SupplierID_PO.Enabled = False
        de_BeginDate_PO.Enabled = False
        de_EndDate_PO.Enabled = False
        cb_IsCheck_PO.Enabled = False
        cb_IsUrgency_PO.Enabled = False
        cb_ReCheckBit_PO.Enabled = False
        glu_PO_PO.EditValue = Nothing
        glu_PR_PO.EditValue = Nothing
        glu_SupplierID_PO.EditValue = Nothing
        de_BeginDate_PO.EditValue = Nothing
        de_EndDate_PO.EditValue = Nothing
        cb_IsCheck_PO.SelectedIndex = 0
        cb_IsUrgency_PO.SelectedIndex = 0
        cb_ReCheckBit_PO.SelectedIndex = 0

        Select Case cb_Select_PO.SelectedIndex
            Case 0
                glu_PO_PO.Enabled = True
            Case 1
                glu_PR_PO.Enabled = True
            Case 2
                glu_SupplierID_PO.Enabled = True
            Case 3
                de_BeginDate_PO.Enabled = True
                de_EndDate_PO.Enabled = True
                de_BeginDate_PO.EditValue = DefaultDate(True)
                de_EndDate_PO.EditValue = DefaultDate(False)
            Case 4
                cb_IsCheck_PO.Enabled = True
            Case 5
                cb_IsUrgency_PO.Enabled = True
            Case 6
                cb_ReCheckBit_PO.Enabled = True
        End Select
    End Sub
#End Region

#Region "日期检查---默认日期"
    Private Function CheckDate(ByVal date1 As Date, ByVal date2 As Date) As Boolean
        Dim boolResult As Boolean = False
        If date1 > date2 Then
            MsgBox("起始日期大於截止日期，請修改！")
        Else
            boolResult = True
        End If
        Return boolResult
    End Function
    Private Function DefaultDate(ByVal Type As Boolean) As Date
        If Type = True Then
            Return Year(Now) & "/" & Month(Now) & "/01"
        Else
            Return Now
        End If
    End Function
#End Region

    Private Sub xtpMrpForecastOrder_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles xtpMrpForecastOrder.Paint

    End Sub

    Private Sub cb_Select_MM_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_Select_MM.SelectedIndexChanged
        glu_MOID_MM.Enabled = False
        glu_M_Code_MM.Enabled = False
        cb_IsCheck_MM.Enabled = False
        de_StarDate_MM.Enabled = False
        de_EndDate_MM.Enabled = False
        glu_MOID_MM.EditValue = Nothing
        glu_M_Code_MM.EditValue = Nothing
        cb_IsCheck_MM.SelectedIndex = 0
        de_StarDate_MM.EditValue = Nothing
        de_EndDate_MM.EditValue = Nothing
        Select Case cb_Select_MM.SelectedIndex
            Case 0
                glu_MOID_MM.Enabled = True
            Case 1
                glu_M_Code_MM.Enabled = True
            Case 2
                cb_IsCheck_MM.Enabled = True
            Case 3
                de_StarDate_MM.Enabled = True
                de_EndDate_MM.Enabled = True
                de_StarDate_MM.EditValue = DefaultDate(True)
                de_EndDate_MM.EditValue = DefaultDate(False)
        End Select

    End Sub


End Class