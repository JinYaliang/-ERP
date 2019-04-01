Imports LFERP.Library.Production.Datasetting
Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.ProductionDPTWareInventory
Imports LFERP.Library.ProductionField
Imports LFERP.Library.Production.ProductionFieldDaySummary
Imports LFERP.Library.WareHouse
Imports LFERP.Library.Purchase.SharePurchase
Imports LFERP.Library.Production.ProductionAffair

Public Class frmProductionFieldCode1
    Dim ds As New DataSet
    Dim mc As New ProductionDataSettingControl
    Dim mpc As New ProductController
    Dim ppc As New ProcessMainControl

    Private Sub frmProductionFieldCode1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()      '�եγЫت�L�{
        dteFP_Date.EditValue = Now
    End Sub

    ''' <summary>
    ''' �Ыت�
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�ե�
    ''' frmProductionFieldCode1_Load()
    Sub CreateTable()
        With ds.Tables.Add("Production")
            .Columns.Add("Pro_Type", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))

            .Columns.Add("PS_Name1", GetType(String))
            .Columns.Add("FP_OutType", GetType(String))
            .Columns.Add("FP_Qty", GetType(Integer))
            .Columns.Add("FP_Weight", GetType(Single))
            .Columns.Add("WI_Qty", GetType(Integer))

            .Columns.Add("PO_NO", GetType(String))
            .Columns.Add("FP_Remark", GetType(String))
        End With
        Grid.DataSource = ds.Tables("Production")


    End Sub

    ''' <summary>
    ''' �ͦ����o�渹
    ''' </summary>
    ''' <returns></returns>
    ''' ����ƳQ�H�U�L�{�ե�
    ''' btnOK_Click()
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
    ''' <summary>
    ''' �ͦ��y����
    ''' </summary>
    ''' <returns></returns>
    ''' ����ƳQ�H�U�L�{�ե�
    ''' btnOK_Click()
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
    ''' <summary>
    ''' �����k����"�K�["
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsAdd.Click
        Dim row As DataRow
        row = ds.Tables("Production").NewRow

        row("Pro_Type") = rcboPro_type.Items.Item(0)
        row("FP_OutType") = rcboFP_OutType.Items.Item(0)
        row("FP_Qty") = 0
        row("FP_Weight") = 0
        row("WI_Qty") = 0

        row("PO_NO") = ""
        row("FP_Remark") = ""

        ds.Tables("Production").Rows.Add(row)

        rgluPM_M_Code.DisplayMember = "PM_M_Code"
        rgluPM_M_Code.ValueMember = "PM_M_Code"
        If mc.ProductionUser_GetList(txtDepID_Out.Tag, Nothing).Count > 0 Then            '�P�_�O�_���v������,��ָ��J�ƾ�
            rgluPM_M_Code.DataSource = mc.ProductionUser_GetList(txtDepID_Out.Tag, Nothing)
        Else
            rgluPM_M_Code.DataSource = ppc.ProcessMain_GetList3(Nothing, Nothing)
        End If

    End Sub
    ''' <summary>
    ''' �����k��"�R��"���,�R���襤��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsDelete.Click
        If GridView1.RowCount = 0 Then Exit Sub

        ds.Tables("Production").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))

    End Sub
    ''' <summary>
    ''' ���~�s�����ܮ�,���s���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgluPM_M_Code_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgluPM_M_Code.EditValueChanged
        ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("PM_Type") = ""
        ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("PS_Name") = ""
        ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("PS_Name1") = ""
        ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("FP_Qty") = 0
        ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("FP_Weight") = 0
        ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("WI_Qty") = 0
        gridPS_NO.DataSource = Nothing
        GridView1.Focus()
    End Sub
    ''' <summary>
    ''' �����������s,�h�X����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' �����������,�ǭȨ�D���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' ���L�{�Q�H�U�L�{�ե�:
    ''' gridPM_Type_View_KeyDown()
    Private Sub gridPM_Type_View_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridPM_Type_View.Click
        On Error Resume Next

        If gridPM_Type_View.RowCount > 0 Then
            GridView1.SetFocusedRowCellValue(PM_Type, gridPM_Type_View.GetFocusedRowCellValue("Type3ID"))       '�ǭ�
            PopupContainerControl1.OwnerEdit.ClosePopup()
            GridView1.Focus()
        End If
        '���s���
        ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("PS_Name") = ""
        ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("PS_Name1") = ""
        ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("FP_Qty") = 0
        ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("FP_Weight") = 0
        ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("WI_Qty") = 0
    End Sub
    ''' <summary>
    ''' �����u�Ǫ��,�ǭȨ�D���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' ���L�{�Q�H�U�L�{�ե�:
    ''' gridPS_NO_View_KeyDown()
    Private Sub gridPS_NO_View_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridPS_NO_View.Click
        On Error Resume Next
        If gridPS_NO_View.RowCount > 0 Then
            Dim i%
            '�P�_�D��椤�O�_�w�s�b�ۦP�u��
            For i = 0 To ds.Tables("Production").Rows.Count - 1
                '�u�Ǭ��ťB�渹�P�J�I��۵��ɤ��i��P�_
                If IsDBNull(ds.Tables("Production").Rows(i)("PS_NO")) = False And i <> GridView1.FocusedRowHandle Then
                    If ds.Tables("Production").Rows(i)("PS_NO") = gridPS_NO_View.GetFocusedRowCellValue("PS_NO") Then
                        PopupContainerControl2.OwnerEdit.ClosePopup()
                        MsgBox("�u�Ǥw�s�b,����K�[�ۦP���u��!", 64, "����")
                        Exit Sub
                    End If
                End If
            Next

            '��ܤu�ǻP��u�Ǭ۵��ɤ����s���
            If gridPS_NO_View.GetFocusedRowCellValue("PS_NO") <> GridView1.GetFocusedRowCellValue("PS_NO") Then
                GridView1.SetFocusedRowCellValue(FP_Qty, 0)
                GridView1.SetFocusedRowCellValue(FP_Weight, 0)
                GridView1.SetFocusedRowCellValue(PS_Name, gridPS_NO_View.GetFocusedRowCellValue("PS_Name"))
                GridView1.SetFocusedRowCellValue(PS_Name1, gridPS_NO_View.GetFocusedRowCellValue("PS_Name"))
                GridView1.SetFocusedRowCellValue(PS_NO, gridPS_NO_View.GetFocusedRowCellValue("PS_NO"))
            End If
            PopupContainerControl2.OwnerEdit.ClosePopup()
            GridView1.Focus()

            '�d���e�u�Ǫ������w�s��,�ýᵹ���E�ƶq
            Dim fdc As New ProductionDPTWareInventoryControl
            Dim fdi As List(Of ProductionDPTWareInventoryInfo)

            fdi = fdc.ProductionDPTWareInventory_GetList(txtDepID_Out.Tag, GridView1.GetFocusedRowCellValue("PS_NO"), Nothing)

            If fdi.Count > 0 Then
                ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("WI_Qty") = fdi(0).WI_Qty
            Else
                ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("WI_Qty") = 0
            End If
        End If
    End Sub
    ''' <summary>
    ''' ��o�X�ƶq���ܮ�,���q�]��ۧ���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rcaeFP_Qty_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcaeFP_Qty.EditValueChanged
        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        pci = pc.ProcessSub_GetList(Nothing, ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("PS_NO").ToString, Nothing, Nothing, Nothing, Nothing)
        If pci.Count = 0 Then
            ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("FP_Weight") = 0
        Else
            Dim AllWeight, strWeight, strG As Single

            strWeight = pci(0).PS_Weight  '�J/��  �歫
            strG = strWeight * CInt(sender.text)
            AllWeight = strG / 1000  '��e�ƶq�����q(KG)
            ds.Tables("Production").Rows(GridView1.FocusedRowHandle)("FP_Weight") = Format(AllWeight, "0.00") '(��Ƭ����p��)
        End If
    End Sub
    ''' <summary>
    ''' �����T�w���s,�K�[�ƾ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim i, j%

        For i = 0 To ds.Tables("Production").Rows.Count - 1
            If IsDBNull(ds.Tables("Production").Rows(i)("PS_NO")) = True Then
                GridView1.FocusedRowHandle = i
                MsgBox("�п�J�o�Ƥu��!", 64, "����")
                Exit Sub
            End If

            If ds.Tables("Production").Rows(i)("FP_Qty") <= 0 Then
                GridView1.FocusedRowHandle = i
                MsgBox("�o�X�ƶq����p�󵥩�0,�Э��s��J!", 64, "����")
                Exit Sub
            End If

            If ds.Tables("Production").Rows(i)("FP_Qty") > ds.Tables("Production").Rows(i)("WI_Qty") Then
                GridView1.FocusedRowHandle = i
                MsgBox("��e���E�ƶq�p��o�X�ƶq,����o�X!", 64, "����")
                Exit Sub
            End If

            If ds.Tables("Production").Rows(i)("PO_NO") = "" Then
                GridView1.FocusedRowHandle = i
                MsgBox("�п�JPO�渹!", 64, "����")
                Exit Sub
            End If

            Dim psi As List(Of LFERP.Library.ProductionSchedule.ProductionScheduleInfo)
            Dim psc As New LFERP.Library.ProductionSchedule.ProductionScheduleControl

            psi = psc.ProductionSchedule_GetList(Nothing, ds.Tables("Production").Rows(i)("Pro_Type"), Nothing, ds.Tables("Production").Rows(i)("PM_M_Code").ToString, ds.Tables("Production").Rows(i)("PM_Type").ToString, (Format(CDate(dteFP_Date.Text), "yyyy/MM/dd")), Format(CDate(dteFP_Date.Text), "yyyy/MM/dd"), Nothing)
            If psi.Count = 0 Then
                GridView1.FocusedRowHandle = i
                MsgBox("��e�u�ǥͲ������s�b��w������Ͳ��p���A�Х��K�[�Ͳ��p���I", 64, "����")
                Exit Sub
            End If
        Next

        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl

        For j = 0 To ds.Tables("Production").Rows.Count - 1
            pi.FP_NO = GetNO()
            pi.FP_Num = GetNum()
            pi.Pro_Type = ds.Tables("Production").Rows(j)("Pro_Type")
            pi.PM_M_Code = ds.Tables("Production").Rows(j)("PM_M_Code").ToString
            pi.PM_Type = ds.Tables("Production").Rows(j)("PM_Type").ToString

            pi.Pro_Type1 = ds.Tables("Production").Rows(j)("Pro_Type")
            pi.PM_M_Code1 = ds.Tables("Production").Rows(j)("PM_M_Code").ToString
            pi.PM_Type1 = ds.Tables("Production").Rows(j)("PM_Type").ToString
            pi.Pro_NO = ds.Tables("Production").Rows(j)("PS_NO").ToString
            pi.Pro_NO1 = ds.Tables("Production").Rows(j)("PS_NO").ToString

            pi.FP_Qty = ds.Tables("Production").Rows(j)("FP_Qty")
            pi.FP_Weight = ds.Tables("Production").Rows(j)("FP_Weight")
            pi.FP_Date = dteFP_Date.EditValue
            pi.FP_Detail = "PT13"
            pi.FP_OutDep = txtDepID_Out.Tag

            pi.FP_InDep = txtDepID.Tag
            pi.FP_Remark = ds.Tables("Production").Rows(j)("PO_NO").ToString & vbCrLf & ds.Tables("Production").Rows(j)("FP_Remark").ToString
            pi.FP_OutAction = InUserID
            pi.FP_OutType = ds.Tables("Production").Rows(j)("FP_OutType")
            pi.FP_OutOK = True

            pi.FP_SendType = "���`"


            If pc.ProductionField_Add(pi) = False Then
                GridView1.FocusedRowHandle = j
                If MsgBox("��e�O���O�s����,�O�_�n�~��O�s�U�@���O��?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "����") = MsgBoxResult.No Then
                    Exit Sub
                End If
            Else                 '�K�[���\�A�����ܧ�w�s�ƾ�
                Dim pai As New ProductionAffairInfo
                Dim pac As New ProductionAffairControl
                Dim pdi As List(Of ProductionDPTWareInventoryInfo)
                Dim pdc As New ProductionDPTWareInventoryControl
                Dim pdsi As List(Of ProductionFieldDaySummaryInfo)
                Dim pdsc As New ProductionFieldDaySummaryControl

                Dim strQty, strReQty As Integer  '�o�X�������l�ƫH��

                Dim strShouLiao, strJiaCun, strQuCun, strFaLiao, strCunHuo, strFanXiuIn, strFanXiuOut, strLiuBan, strSunHuai, strDiuShi, strBuNiang, strCunCang, strQuCang, strChuHuo, strWaiFaIn, strWaiFaOut, strAccIn, strAccOut, strRePairOut, strZuheOut As Integer '�o�X����

                pdi = pdc.ProductionDPTWareInventory_GetList(txtDepID_Out.Tag, pi.Pro_NO, Nothing)

                If pdi.Count = 0 Then
                    strQty = 0
                    strReQty = 0
                Else
                    strQty = pdi(0).WI_Qty
                    strReQty = pdi(0).WI_ReQty
                End If

                pdsi = pdsc.ProductionFieldDaySummary_GetList(Nothing, Nothing, Nothing, pi.Pro_NO, txtDepID_Out.Tag, Nothing, dteFP_Date.Text, dteFP_Date.Text)
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

                '------------------------------------------------------���J�����H��

                'Dim wii As List(Of WareInventory.WareInventoryInfo)
                'Dim wic As New WareInventory.WareInventoryMTController

                'Dim Qty3 As Single

                'wii = wic.WareInventory_GetList3(Transfer(pi.Pro_NO), txtDepID.Tag)

                'If wii.Count = 0 Then
                '    Qty3 = 0
                'Else
                '    Qty3 = wii(0).WI_Qty
                'End If

                'Dim spc As New SharePurchaseController
                'Dim spi As New SharePurchaseInfo

                'spi.M_Code = Transfer(pi.Pro_NO) '�N�u�ǽs����Ƭ����ƽs�X�H��
                'spi.WH_ID = txtDepID.Tag

                'spi.WI_Qty = Qty3 + CSng(pi.FP_Qty) '�T�{�o��

                'spc.UpdateWareInventory_WIQty2(spi)  '���Ʀb��e�ܮw���ܧ�

                Dim pic As New ProductInventoryController
                Dim pii As List(Of ProductInventoryInfo)
                Dim pii1 As New ProductInventoryInfo
                Dim strM_Code As String

                strM_Code = Transfer(pi.Pro_NO)

                pii = pic.ProductInventory_GetList(ds.Tables("Production").Rows(j)("PM_M_Code").ToString, strM_Code, txtDepID.Tag, Nothing)

                pii1.PM_M_Code = ds.Tables("Production").Rows(j)("PM_M_Code").ToString
                pii1.M_Code = strM_Code
                pii1.WH_ID = txtDepID.Tag

                If pii.Count > 0 Then
                    pii1.PI_Qty = pii(0).PI_Qty + CInt(ds.Tables("Production").Rows(j)("FP_Qty"))
                Else
                    pii1.PI_Qty = CInt(ds.Tables("Production").Rows(j)("FP_Qty"))
                End If
                pic.ProductInventory_Update(pii1)

                '----------------------------------------------------------------------------------------------------

                pai.FP_Detail = "PT13"
                pai.FP_NO = pi.FP_NO
                pai.FP_Type = "����"
                pai.FP_InAction = InUserID
                pai.FP_InCheck = True
                pai.FP_InCheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                pai.CardID = ""

                pai.FP_OutDep = txtDepID_Out.Tag
                pai.Pro_NO = pi.Pro_NO
                pai.WI_Qty = strQty - CInt(pi.FP_Qty)
                pai.WI_ReQty = strReQty


                pai.Pro_Type = pi.Pro_Type
                pai.PM_M_Code = pi.PM_M_Code
                pai.PM_Type = pi.PM_Type


                pai.ShouLiao = strShouLiao
                pai.JiaCun = strJiaCun
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
                pai.ChuHuo = strChuHuo + CInt(pi.FP_Qty)
                pai.WaiFaIn = strWaiFaIn
                pai.WaiFaOut = strWaiFaOut
                pai.AccIn = strAccIn
                pai.AccOut = strAccOut
                pai.RePairOut = strRePairOut
                pai.ZuheOut = strZuheOut
                pai.PM_Date = Format(Now, "yyyy/MM/dd")

                If pac.UpdateProductionCheck_Qty(pai) = True Then
                    Dim pfi As New ProductionFieldInfo
                    Dim pfc As New ProductionFieldControl

                    pfi.FP_NO = pi.FP_NO
                    pfi.FP_Check = True

                    pfi.FP_CheckAction = InUserID
                    pfi.FP_CheckRemark = ""

                    pfc.ProductionField_UpdateCheck(pfi)
                End If

            End If
        Next
        MsgBox("�O�s���\!", 64, "����")
        Me.Close()
    End Sub
    Function Transfer(ByVal PS_NO As String) As String  '�N�u�ǽs����Ƭ����~�u���H��������l�s�X(���ƽs�X,���ƦW��)
        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        pci = pc.ProcessSub_GetList(Nothing, PS_NO, Nothing, Nothing, Nothing, Nothing)
        If pci.Count = 0 Then
            MsgBox("��e���s�b���u�ǽs�X,�нT�{��J���T!")
            Transfer = Nothing
            Exit Function
        Else
            Transfer = pci(0).M_Code    '���������ƽs�X�H��
        End If

    End Function
    ''' <summary>
    ''' ������������o�J�I��,������歫�s�[���ƾ�,�קK��������`�O�O���̫�@���[�����ƾ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rpcePM_Type_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles rpcePM_Type.Enter
        If GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "PM_M_Code").ToString = "" Then   '���~�s�����Ů�,�������]����
            gridPM_Type.DataSource = Nothing
        Else
            gridPM_Type.DataSource = ppc.ProcessMain_GetList2(GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Pro_Type").ToString, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "PM_M_Code").ToString)
        End If
    End Sub
    ''' <summary>
    ''' ��o�X�u�Ǳ�����o�J�I��,�u�Ǫ�歫�s�[���ƾ�,�קK�u�Ǫ���`�O�O���̫�@���[�����ƾ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rpcePS_Name_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles rpcePS_Name.Enter
        '�P�_�u�ǬO�_���v������
        If mc.ProductionIssue_GetList(txtDepID_Out.Tag, txtDepID_Out.Tag, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Pro_Type").ToString, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "PM_M_Code").ToString, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "PM_Type").ToString, Nothing).Count > 0 Then
            gridPS_NO.DataSource = mc.ProductionIssue_GetList(txtDepID_Out.Tag, txtDepID_Out.Tag, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Pro_Type").ToString, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "PM_M_Code").ToString, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "PM_Type").ToString, Nothing)
        Else
            '�P�_�u�ǬO�_�s�b
            If ppc.ProcessMain_GetList(Nothing, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "PM_M_Code").ToString, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Pro_Type").ToString, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "PM_Type").ToString, Nothing, True).Count > 0 Then
                gridPS_NO.DataSource = ppc.ProcessMain_GetList(Nothing, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "PM_M_Code").ToString, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Pro_Type").ToString, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "PM_Type").ToString, Nothing, True)
            Else
                gridPS_NO.DataSource = Nothing
            End If
        End If
    End Sub
    ''' <summary>
    ''' ���U�Ů����,��ܤU�Ե��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgluPM_M_Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rgluPM_M_Code.KeyDown, rpcePM_Type.KeyDown, rpcePS_Name.KeyDown, rcboPro_type.KeyDown, rcboFP_OutType.KeyDown, RepositoryItemMemoExEdit1.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub
    ''' <summary>
    ''' ������椤���U�^�����,�ե�������檺�����ƥ�,��{�^����ǭ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gridPM_Type_View_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridPM_Type_View.KeyDown
        If e.KeyCode = Keys.Enter Then
            gridPM_Type_View_Click(Nothing, Nothing)
        End If
    End Sub
    ''' <summary>
    ''' �u�Ǫ�椤���U�^�����,�եΤu�Ǫ�檺�����ƥ�,��{�^����ǭ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gridPS_NO_View_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridPS_NO_View.KeyDown
        If e.KeyCode = Keys.Enter Then
            gridPS_NO_View_Click(Nothing, Nothing)
        End If
    End Sub
End Class