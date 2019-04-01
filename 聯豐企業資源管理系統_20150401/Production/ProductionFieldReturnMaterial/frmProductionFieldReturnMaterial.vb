Imports LFERP.Library.ProductionField
Imports LFERP.DataSetting
Imports LFERP.SystemManager
Imports LFERP.Library.Product
Imports LFERP.Library.WareHouse
Imports LFERP.Library.ProductionWareHouse
Imports LFERP.Library.ProductionSchedule
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.ProductionFieldType
Imports LFERP.Library.PieceProcess
Imports LFERP.Library.ProductionKaiLiao
Imports LFERP.Library.ProductionDPTWareInventory
Imports LFERP.Library
Imports LFERP.Library.Purchase.SharePurchase

Imports System.Threading

Imports LFERP.Library.Production.Datasetting
Imports LFERP.Library.Production.ProductionFieldReturnMaterial
Imports LFERP.Library.Production.ProductionFieldDaySummary

Public Class frmProductionFieldReturnMaterial

#Region "�w�q"
    Dim ds As New DataSet

    Private _EditItem As String
    Private _EditValue As String
    Private _EditID As String
    Public Property EditItem() As String
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
    Public Property EditValue() As String
        Get
            Return _EditValue
        End Get
        Set(ByVal value As String)
            _EditValue = value
        End Set
    End Property
    Public Property EditID() As String
        Get
            Return _EditID
        End Get
        Set(ByVal value As String)
            _EditID = value
        End Set
    End Property

    Dim uc As New SystemUser.SystemUserController
    Dim upi As New List(Of UserPowerInfo)
    Dim upc As New UserPowerControl
    Dim mc As New ProductionDataSettingControl

    Dim OldIncheck As Boolean

#End Region

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frmProductionFieldReturnMaterial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        CreateTable()
        LoadPM_M_Code()
        LoadProductionDetail()
        XtraTabControl1.SelectedTabPage = Me.XtraTabPage1


        Select Case EditItem
            Case "Add"
                upi = upc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)
                If upi.Count = 0 Then MsgBox("�Τ��v���]�m���~!") : Exit Sub
                cbType.EditValue = upi(0).UserType
                cbType.Enabled = False
                XtraTabPage2.PageVisible = False


                DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")

            Case "Update"
                cbType.Enabled = False
                DateEdit1.Enabled = False
                XtraTabPage2.PageVisible = False
                LoadData(EditValue)

            Case "Check"
                XtraTabPage2.PageVisible = True
                XtraTabControl1.SelectedTabPage = Me.XtraTabPage2

                LoadData(EditValue)
            Case "InCheck"
                XtraTabPage1.PageVisible = True
                CheckEdit1.Visible = True
                CheckEdit1.Enabled = True
                Me.GroupBox1.Enabled = False
                Me.GroupBox2.Enabled = False
                Me.GroupBox3.Enabled = False
                LoadData(EditValue)

            Case "View"
                Me.GroupBox1.Enabled = False
                Me.GroupBox2.Enabled = False
                Me.GroupBox3.Enabled = False
                XtraTabPage1.PageVisible = True
                cmdSave.Visible = False
                LoadData(EditValue)

        End Select


    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Select Case EditItem
            Case "Add"
                If CheckData() = False Then
                    Exit Sub
                End If

                SaveDataNew()

            Case "Update"
                If CheckData() = False Then
                    Exit Sub
                End If

                SaveEdit()

            Case "Check"
                If CheckData() = False Then
                    Exit Sub
                End If

                SaveCheck()

            Case "InCheck"

                If OldIncheck = CheckEdit1.Checked Then
                    MsgBox("�����ܦ��ƪ��A!")
                    Exit Sub
                End If

                If CheckData() = False Then
                    Exit Sub
                End If

                SaveInCheck()

        End Select
    End Sub

#Region "���Ƽf�־ާ@"

    Sub SaveInCheck()
        Dim rc As New ProductionFieldReturnMaterialControl
        Dim ri As New ProductionFieldReturnMaterialInfo

        ri.R_NO = txtNo.Text
        ri.RInCheck = CheckEdit1.Checked
        ri.RIncheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        ri.RIncheckAction = InUserID
        ri.FP_NO = GetNO()
        ri.FP_Num = GetNum()
        ri.TuiLiao = UpdateDaySummary()
        If rc.ProductionFieldReturnMaterial_InCheck(ri) = True Then
        Else
            MsgBox("���ƫO�s���ѡI")
            Exit Sub
        End If
        '��u�Ǽ�--------------------------------------------
        Dim fdc As New ProductionDPTWareInventoryControl
        Dim fdi As List(Of ProductionDPTWareInventoryInfo)
        fdi = fdc.ProductionDPTWareInventory_GetList(GluDep.EditValue, GluEdit1.EditValue, Nothing)
        Dim PS_Qty As Int32
        Dim PS_RQty As Int32
        Dim IntWI_Qty As Int32
        If fdi.Count <= 0 Then
            IntWI_Qty = 0
            PS_RQty = 0
        Else
            IntWI_Qty = fdi(0).WI_Qty
            PS_RQty = fdi(0).WI_ReQty
        End If

        PS_Qty = IntWI_Qty - Val(txtQty.Text) '-'�bcheckData�����P�_

        Dim pfcA As New ProductionDPTWareInventoryControl
        Dim pfiA As New ProductionDPTWareInventoryInfo
        pfiA.M_Code = GluEdit1.EditValue
        pfiA.DPT_ID = GluDep.EditValue
        pfiA.WI_Qty = PS_Qty
        pfiA.WI_ReQty = PS_RQty
        If pfcA.UpdateProductionField_Qty(pfiA) = True Then
        End If

        '----�[�w�s-----------------------------------------------------
        Dim mt As New SharePurchaseController
        Dim mm As New SharePurchaseInfo
        mm.WH_ID = ButtonEditA.Tag
        mm.M_Code = PM_M_Code.EditValue

        Dim wi As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
        Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
        wi = wc.WareInventory_GetSub(PM_M_Code.EditValue, ButtonEditA.Tag)
        Dim Qty As Double

        If wi Is Nothing Then
            Qty = 0
        Else
            Qty = wi.WI_Qty
        End If

        mm.WI_Qty = Qty + Val(txtQty.Text)
        mt.UpdateWareInventory_WIQty2(mm)
        '---------------------------------------------------------
        MsgBox("���Ʀ��\�I")

        Me.Close()
    End Sub

    Function UpdateDaySummary() As Int16
        Dim pc As New ProductionFieldDaySummaryControl
        Dim pl As New List(Of ProductionFieldDaySummaryInfo)
        pl = pc.ProductionFieldDaySummary_GetList(Nothing, Nothing, Nothing, GluEdit1.EditValue, GluDep.EditValue, Nothing, Format(CDate(DateEdit1.EditValue), "yyyy-MM-dd"), Format(CDate(DateEdit1.EditValue), "yyyy-MM-dd"))

        Dim IntTuiLiao As Int32


        If pl.Count <= 0 Then
            IntTuiLiao = 0
        Else
            IntTuiLiao = pl(0).TuiLiao
        End If
        UpdateDaySummary = Val(txtQty.Text) + IntTuiLiao

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

#End Region


#Region "�ާ@�ƾڪ�"

    Function CheckData() As Boolean
        CheckData = True

        If PM_M_Code.Text = "" Then
            MsgBox("�п�ܲ��~�s��!")
            PM_M_Code.Select()
            CheckData = False
            Exit Function
        End If

        If cbType.EditValue = "" Then
            MsgBox("�u��������������!")
            cbType.Select()
            CheckData = False
            Exit Function
        End If

        If gluType.EditValue = "" Then
            MsgBox("�������ର��!")
            gluType.Select()
            CheckData = False
            Exit Function
        End If

        If GluDep.Text = "" Then
            MsgBox("�п�ܰh�Ƴ���!")
            GluDep.Select()
            CheckData = False
            Exit Function
        End If
        '
        If GluEdit1.Text = "" Then
            MsgBox("�п�ܰh�Ƥu��!")
            GluEdit1.Select()
            CheckData = False
            Exit Function
        End If

        If ButtonEditA.Tag = "" Then
            MsgBox("�п�ܦ��ƭ�!")
            ButtonEditA.Select()
            CheckData = False
            Exit Function
        End If


        If Val(txtQty.Text) <= 0 Then
            MsgBox("�ƶq����p�󵥩�0!")
            txtQty.Select()
            CheckData = False
            Exit Function
        End If


        ''�ˬd��e�u�Ǯw�s�O�_����
        If CheckEdit2.Checked = False And EditItem = "Check" Then
        Else
            Dim fdc As New ProductionDPTWareInventoryControl
            Dim fdi As List(Of ProductionDPTWareInventoryInfo)

            fdi = fdc.ProductionDPTWareInventory_GetList(GluDep.EditValue, GluEdit1.EditValue, Nothing)

            If fdi.Count = 0 Then
                MsgBox("��e�o�X�����u�Ǽƶq����,�����\�O�s�I")
                CheckData = False
                Exit Function
            Else
                If fdi(0).WI_Qty < Val(txtQty.Text) Then
                    MsgBox("��e�o�X�����u�Ǽƶq�p��h�ƼơI")
                    CheckData = False
                    Exit Function
                End If
            End If
        End If

    End Function

    Function GetR_NO() As String
        '�ͦ��spm
        Dim pm As New ProductionFieldReturnMaterialControl
        Dim pi As New ProductionFieldReturnMaterialInfo
        Dim ndate As String
        ndate = "RC" + Format(Now(), "yyMM")
        pi = pm.ProductionFieldReturnMaterial_GetNO(ndate)
        If pi Is Nothing Then
            GetR_NO = ndate + "00001"
        Else
            GetR_NO = ndate + Mid((CInt(Mid(pi.R_NO, 7)) + 100001), 2)
        End If
    End Function

    Sub SaveDataNew() '�W�[�ƾ�
        Dim pc As New ProductionFieldReturnMaterialControl
        Dim pi As New ProductionFieldReturnMaterialInfo
        txtNo.Text = GetR_NO()
        pi.R_NO = txtNo.Text

        pi.Pro_Type = cbType.EditValue
        pi.PM_M_Code = PM_M_Code.EditValue
        pi.PM_Type = gluType.EditValue
        pi.PS_NO = GluEdit1.EditValue
        pi.DepID = GluDep.EditValue

        pi.RQty = Val(txtQty.Text)
        pi.RWeight = 0
        pi.WH_ID = ButtonEditA.Tag
        pi.M_Code = PM_M_Code.EditValue
        pi.RDate = Format(CDate(DateEdit1.EditValue), "yyyy/MM/dd") & " " & Format(Now, "HH:mm:ss")

        pi.RAction = InUserID
        pi.RRemark = txtRemark.Text

        If pc.ProductionFieldReturnMaterial_Add(pi) = True Then
        Else
            MsgBox("�O�s���ѡI")
            Exit Sub
        End If

        MsgBox("�O�s���\�I")
        Me.Close()
    End Sub


    Sub SaveEdit() '�W�[�ƾ�
        Dim pc As New ProductionFieldReturnMaterialControl
        Dim pi As New ProductionFieldReturnMaterialInfo
        pi.R_NO = txtNo.Text

        pi.Pro_Type = cbType.EditValue
        pi.PM_M_Code = PM_M_Code.EditValue
        pi.PM_Type = gluType.EditValue
        pi.PS_NO = GluEdit1.EditValue
        pi.DepID = GluDep.EditValue

        pi.RQty = Val(txtQty.Text)
        pi.RWeight = 0
        pi.WH_ID = ButtonEditA.Tag
        pi.M_Code = PM_M_Code.EditValue
        pi.RDate = Format(CDate(DateEdit1.EditValue), "yyyy/MM/dd") & " " & Format(Now, "HH:mm:ss")

        pi.RAction = InUserID
        pi.RRemark = txtRemark.Text

        If pc.ProductionFieldReturnMaterial_Update(pi) = True Then
        Else
            MsgBox("�O�s���ѡI")
            Exit Sub
        End If

        MsgBox("�O�s���\�I")
        Me.Close()
    End Sub


    Sub SaveCheck()
        Dim pc As New ProductionFieldReturnMaterialControl
        Dim pi As New ProductionFieldReturnMaterialInfo
        pi.R_NO = txtNo.Text

        pi.RCheck = CheckEdit2.Checked
        pi.RCheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        pi.RCheckAction = InUserID
        pi.RCheckReamrk = txtCheckremark.Text

        If pc.ProductionFieldReturnMaterial_Check(pi) = True Then
        Else
            MsgBox("�O�s����,���ˬd!")
            Exit Sub
        End If

        If CheckEdit2.Checked = True Then
            MsgBox("�f�֦��\!")
        Else
            MsgBox("�����f�֦��\!")
        End If

        Me.Close()
    End Sub

    Sub LoadData(ByVal strR_NO As String)
        Dim pc As New ProductionFieldReturnMaterialControl
        Dim pl As New List(Of ProductionFieldReturnMaterialInfo)

        pl = pc.ProductionFieldReturnMaterial_GetList(strR_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pl.Count <= 0 Then
            MsgBox("�L�ƾڦs�b!")
            Exit Sub
        End If

        txtNo.Text = pl(0).R_NO
        cbType.EditValue = pl(0).Pro_Type
        PM_M_Code.EditValue = pl(0).PM_M_Code
        gluType.EditValue = pl(0).PM_Type

        GluDep.EditValue = pl(0).DepID

        GluEdit1.EditValue = pl(0).PS_NO
        txtQty.Text = pl(0).RQty

        ButtonEditA.Tag = pl(0).WH_ID
        ButtonEditA.Text = pl(0).WH_Name

        'MsgBox(pl(0).RDate)
        DateEdit1.EditValue = Format(CDate(pl(0).RDate), "yyyy/MM/dd")

        LabAction.Text = pl(0).RActionName
        txtRemark.Text = pl(0).RRemark

        '---------------------------------------
        CheckEdit2.Checked = pl(0).RCheck
        Label14.Text = pl(0).RCheckActionName
        txtCheckremark.Text = pl(0).RCheckReamrk

        CheckEdit1.Checked = pl(0).RInCheck
        OldIncheck = pl(0).RInCheck

    End Sub
#End Region


#Region "���J����H��"

    Sub LoadPM_M_Code()
        Dim mi As New List(Of ProductionDataSettingInfo)
        mi = mc.ProductionUser_GetList(GluDep.EditValue, Nothing)

        ds.Tables("PM_M_Code").Clear()

        If mi.Count > 0 Then    '�P�_�O�_���v������
            Dim row As DataRow
            Dim j As Integer
            For j = 0 To mi.Count - 1
                row = ds.Tables("PM_M_Code").NewRow
                row("PM_M_Code") = mi(j).PM_M_Code
                row("PM_JiYu") = mi(j).PM_JiYu '
                ds.Tables("PM_M_Code").Rows.Add(row)
            Next
        Else
            Dim row As DataRow
            Dim j As Integer
            'Dim mpi As List(Of ProductInfo)
            'Dim mpc As New ProductController

            Dim mpi As List(Of ProcessMainInfo)
            Dim mpc As New ProcessMainControl
            mpi = mpc.ProcessMain_GetList3(Nothing, Nothing)

            'mpi = mpc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If mpi.Count > 0 Then
                For j = 0 To mpi.Count - 1
                    row = ds.Tables("PM_M_Code").NewRow
                    row("PM_M_Code") = mpi(j).PM_M_Code
                    row("PM_JiYu") = mpi(j).PM_JiYu '
                    ds.Tables("PM_M_Code").Rows.Add(row)
                Next
            End If

        End If
    End Sub
    Sub CreateTable()
        With ds.Tables.Add("PM_M_Code")
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_JiYu", GetType(String))
        End With
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = ds.Tables("PM_M_Code")

        '�Ы�������
        With ds.Tables.Add("ProductType")
            .Columns.Add("PM_Type", GetType(String))
        End With

        gluType.Properties.ValueMember = "PM_Type"
        gluType.Properties.DisplayMember = "PM_Type"
        gluType.Properties.DataSource = ds.Tables("ProductType")


        '�Ыصo�Ƥu�ǽs����
        With ds.Tables.Add("Process")
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
        End With
        GluEdit1.Properties.ValueMember = "PS_NO"
        GluEdit1.Properties.DisplayMember = "PS_Name"
        GluEdit1.Properties.DataSource = ds.Tables("Process")


        Dim fc As New LFERP.Library.ProductionController.ProductionFieldControl
        GluDep.Properties.DataSource = fc.ProductionFieldControl_GetList(InUserID, Nothing)
        Me.GluDep.Properties.DisplayMember = "DepName"
        Me.GluDep.Properties.ValueMember = "ControlDep"

    End Sub

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

                '-------------------------------------------------
                Dim row1 As DataRow
                row1 = ds.Tables("ProductType1").NewRow
                row1("PM_Type") = ppi(i).Type3ID          '---�q�{���p�U,�o�������P���������ۦP
                ds.Tables("ProductType1").Rows.Add(row1)
                '-------------------------------------------------
            Next
        End If

        gluType.EditValue = ds.Tables("ProductType").Rows(0)("PM_Type").ToString


    End Sub

    Private Sub gluType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluType.EditValueChanged
        If gluType.EditValue = "" Or gluType.EditValue Is Nothing Then Exit Sub

        LoadOutPs_Name()
        GluEdit1.EditValue = ds.Tables("Process").Rows(0)("PS_NO").ToString

    End Sub
    Sub LoadOutPs_Name()
        Dim mi As List(Of ProductionDataSettingInfo)
        Dim row As DataRow
        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        mi = mc.ProductionIssue_GetList(GluDep.EditValue, GluDep.EditValue, cbType.Text, PM_M_Code.EditValue, gluType.EditValue, Nothing)
        ds.Tables("Process").Clear()

        If mi.Count > 0 Then        '�P�_�O�_���v������
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
            Dim i As Integer
            For i = 0 To pci.Count - 1
                row = ds.Tables("Process").NewRow
                row("PS_NO") = pci(i).PS_NO
                row("PS_Name") = pci(i).PS_Name

                ds.Tables("Process").Rows.Add(row)
            Next
        End If
    End Sub
    Sub LoadProductionDetail() ' �ݩ�
        Dim mc As New ProductionFieldTypeControl
        GluDetail.Properties.DisplayMember = "PT_Type"
        GluDetail.Properties.ValueMember = "PT_NO"
        GluDetail.Properties.DataSource = mc.ProductionFieldType_GetList(Nothing, Nothing, "�o��/����")
        GluDetail.EditValue = "PT21"
    End Sub

    Private Sub GluEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GluEdit1.EditValueChanged
        If GluEdit1.EditValue = "" Or GluEdit1.EditValue Is Nothing Then Exit Sub

        Dim fdc As New ProductionDPTWareInventoryControl
        Dim fdi As List(Of ProductionDPTWareInventoryInfo)


        fdi = fdc.ProductionDPTWareInventory_GetList(GluDep.EditValue, GluEdit1.EditValue, Nothing)
        If fdi.Count = 0 Then
            Label32.Text = 0
        Else
            Label29.Text = "���l�ơG"
            Label32.Text = fdi(0).WI_Qty
        End If
    End Sub

#End Region



    Private Sub ButtonEditA_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ButtonEditA.ButtonClick
        tempCode = "�Ͳ��ܮw"
        frmWareHouseSelect.SelectWareID = ""
        tempValue2 = "8802006"
        tempValue3 = "8802006"
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = MDIMain.Left + MDIMain.tvModule.Width + Me.Left + ButtonEditA.Left + 15
        frmWareHouseSelect.Top = MDIMain.Top + Me.Top + ButtonEditA.Top + ButtonEditA.Height + 140 + GroupBox2.Top
        frmWareHouseSelect.ShowDialog()
        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            ButtonEditA.Tag = frmWareHouseSelect.SelectWareID
            ButtonEditA.Text = frmWareHouseSelect.SelectWareName
        End If
    End Sub

    Private Sub GluDep_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GluDep.EditValueChanged
        GluEdit1_EditValueChanged(Nothing, Nothing)
    End Sub

    Private Sub txtQty_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyUp
        'Dim m As New System.Text.RegularExpressions.Regex("^+?(\d+(\.\d*)?|\.\d+)$")  '��ܾ��,���B�I�ƥ��h��F��
        Dim m As New System.Text.RegularExpressions.Regex("^\+?[1-9][0-9]*$")  '��ܾ��,���B�I�ƥ��h��F��

        If m.IsMatch(txtQty.Text) = True Then
        Else
            txtQty.Text = Nothing
            Exit Sub
        End If
    End Sub
End Class