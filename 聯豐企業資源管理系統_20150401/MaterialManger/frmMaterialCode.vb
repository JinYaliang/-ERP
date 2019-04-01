Imports System.IO
Imports System.Windows.Forms
Imports LFERP.Library.MaterialParam
Imports LFERP.SystemManager
Imports LFERP.Library.Material
Imports LFERP.Library
Imports LFERP.DataSetting
Imports LFERP.TwainMdl

Public Class frmMaterialCode
    Private fs As FileStream
    Dim mtc As New Material.MaterialTypeController
    Dim mc As New MaterialController
    Dim mtsCurrency As New CurrencyControler
    Dim mtsUnit As New UnitController
    Dim mtd As New SuppliersControler
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()

    End Sub
    '@ 2012/1/7 �ק�A��popTypeID������ܪ������O�W�١ALabel24������ܪ������O���W�١A�������OID�O�s�bpopTypeID.Tag
    Private Sub frmMaterialCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        rdoAccountCheck.SelectedIndex = 1
        CreateTables()

        lueCurrency.Properties.DataSource = mtsCurrency.GetCurrencyList(Nothing)
        lueUnit.Properties.DataSource = mtsUnit.GetUnitList(Nothing)
        gluSupplier.Properties.DisplayMember = "S_Supplier"
        gluSupplier.Properties.ValueMember = "S_Supplier"
        '   gluSupplier.Properties.DataSource = mtd.GetSupplierList(Nothing, Nothing, ErpUser.SupplierType)
        gluSupplier.Properties.DataSource = mtd.GetSuppliersList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "True")

        Label28.Text = tempCode
        Label27.Text = tempValue5
        txtCode.Text = Label27.Text
        Label29.Text = tempValue

        tempValue5 = ""
        tempCode = ""

        'mtc.LoadNodes(tv1, ErpUser.MaterialType)
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100113")
        If pmwiL.Count > 0 Then
            If pmwiL(0).PMWS_Value.ToString <> "" Then
                mtc.LoadNodes(tv1, pmwiL(0).PMWS_Value.ToString)    '�u��ܦ��~��
            Else
                mtc.LoadNodes(tv1, ErpUser.MaterialType)
            End If
        Else
            mtc.LoadNodes(tv1, ErpUser.MaterialType)
        End If

        Select Case Label28.Text

            Case "AddOrEdit"
                If Edit = False Then
                    Me.Text = "���ƽs�X-�s�W"
                    popTypeID.Enabled = True
                    txtSaveKuCun.Text = "0"
                    txtWeight.Text = "0"
                    If Label29.Text = "�_��s�W" Then
                        popTypeID.Enabled = True
                        'LoadData(txtCode.Text)
                        txtName.Text = tempValue2
                        txtGauge.Text = tempValue3
                        lueUnit.EditValue = tempValue4
                        popTypeID.Tag = tempValue6 '@ 2012/1/7 �ק�

                    End If
                ElseIf Edit = True Then
                    Me.Text = "���ƽs�X-�ק�"
                    popTypeID.Enabled = True
                    LoadData(Label27.Text)
                End If

            Case "Check"
                Me.Text = "���ƽs�X-�f��"
                LoadData(Label27.Text)
                Panel2.Enabled = False
                Grid.Enabled = False
                cmdOpenPhoto.Enabled = False
                cmdDelPhoto.Enabled = False
                Panel3.Enabled = False
            Case "OnlyPreView"
                Me.Text = "���ƽs�X-�d��"
                LoadData(Label27.Text)
                cmdSave.Visible = False
                Panel3.Enabled = False
                txtPrice.Visible = False
            Case "Photo"
                Me.Text = "���ƽs�X-���J�Ϥ�"
                LoadData(Label27.Text)
                'Panel3.Enabled = False
                'Panel2.Enabled = False
                txtName.Enabled = False
                txtCode.Enabled = False
                txtGauge.Enabled = False
                lueUnit.Enabled = True
                txtMaker.Enabled = False
                txtRemark.Enabled = False
                popTypeID.Enabled = True
                'popCodeSubAdd.Enabled = False
                'popCodeSubDel.Enabled = False
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                cmdOpenPhoto.Enabled = True
                cmdDelPhoto.Enabled = True
                cmdSave.Visible = True
                gluSupplier.Enabled = False
                txtSupplierNo.Enabled = False
                lueCurrency.Enabled = False
                txtSaveKuCun.Enabled = False
                txtBlocCode.Enabled = True
                cmdGauge.Enabled = False
                lueUnit.Enabled = False

            Case "PurchaseView"
                Me.Text = "���ƽs�X-�d��"
                LoadData(Label27.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                cmdSave.Visible = False
                Panel3.Enabled = False
                txtPrice.Visible = False
            Case "QuotationView"
                Me.Text = "���ƽs�X-�d��"
                LoadData(Label27.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                cmdSave.Visible = False
                Panel3.Enabled = False
                txtPrice.Visible = False
            Case "AuditEdit"
                Me.Text = "���ƽs�X-�f�p���ק�"
                LoadData(Label27.Text)
                popTypeID.Enabled = False
        End Select

        'If Edit = False Then
        '    Me.Text = "���ƽs�X-�s�W"
        '    popTypeID.Enabled = True
        '    txtSaveKuCun.Text = "0"
        '    txtWeight.Text = "0"
        '    If tempValue = "�_��s�W" Then
        '        popTypeID.Enabled = True
        '        LoadData(txtCode.Text)

        '    End If
        'Else
        '    Me.Text = "���ƽs�X-�ק�"
        '    popTypeID.Enabled = True
        '    LoadData(txtCode.Text)

        'End If

        'If tempCode = "AddOrEdit" Then
        '    rdoAccountCheck.Enabled = False
        '    rdoIsEnabled.Enabled = False
        'End If

        'If tempCode = "Check" Then
        '    Panel2.Enabled = False
        '    Grid.Enabled = False
        '    cmdOpenPhoto.Enabled = False
        '    cmdDelPhoto.Enabled = False
        '    Panel3.Enabled = False
        'End If
        'If tempCode = "OnlyPreView" Then
        '    Me.Text = "���ƽs�X-�d��"
        '    LoadData(txtCode.Text)
        '    cmdSave.Visible = False
        '    Panel3.Enabled = False
        '    txtPrice.Visible = False
        'End If
        Label25.Visible = False
        Label26.Visible = False
        txtWeight.Visible = False
        cbWeight.Visible = False
        tempValue = ""
        tempValue2 = ""
        tempValue3 = ""
        tempValue4 = ""
        tempValue6 = ""
    End Sub

    Private Sub popCodeSubAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popCodeSubAdd.Click

        On Error Resume Next
        tempCode = ""
        Dim fr As frmBiaMai
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmBiaMai Then
                'fr.Activate()
                fr.Close()
                Exit Sub
            End If
        Next
        fr = New frmBiaMai
        ' fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.ShowIcon = False
        fr.ShowInTaskbar = False
        fr.cmdAddSub.Visible = True
        fr.cmdCopy.Visible = False
        fr.cmdNew.Visible = False
        fr.ShowDialog()

        '�W�[�O��
        If tempCode = txtCode.Text Then
            MsgBox("���i�H��ܥD���ƽs�X!", , "����")
            Exit Sub
        Else
            AddRow(tempCode)
        End If
        tempCode = ""

    End Sub

    Sub AddRow(ByVal strCode As String)
        If strCode = "" Then
        Else
            Dim objInfo As New MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)
            Dim row As DataRow
            row = CodeSubData.Tables("CodeSub").NewRow
            'CodeSubData.Tables("CodeSub").NewRow()
            row("AutoID") = "0"
            row("M_CodeSub") = objInfo.M_Code
            row("M_Qty") = 0
            row("M_Name") = objInfo.M_Name
            row("M_Unit") = objInfo.M_Unit
            row("M_Gauge") = objInfo.M_Gauge
            CodeSubData.Tables("CodeSub").Rows.Add(row)
            GridView1.MoveLast()
        End If
    End Sub
    Sub UpdateCodeSub(ByVal strCode As String)
        '��s�l�t��
        Dim i As Integer
        For i = 0 To CodeSubData.Tables("CodeSub").Rows.Count - 1
            Dim objInfo As New MaterialSubInfo
            If CodeSubData.Tables("CodeSub").Rows(i)("AutoID") = "0" Then
                '�s�O��
                objInfo.M_Code = strCode
                objInfo.M_CodeSub = CodeSubData.Tables("CodeSub").Rows(i)("M_CodeSub")
                objInfo.M_Qty = CodeSubData.Tables("CodeSub").Rows(i)("M_Qty")
                mc.MaterialCodeSub_Add(objInfo)
            Else
                '�ק�O��
                objInfo.M_Code = strCode
                objInfo.M_CodeSub = CodeSubData.Tables("CodeSub").Rows(i)("M_CodeSub")
                objInfo.M_Qty = CodeSubData.Tables("CodeSub").Rows(i)("M_Qty")
                'mc.MaterialCodeSub_Add(objInfo)
                mc.MaterialCodeSub_Update(objInfo)
            End If
        Next
        '�R���O��
        For i = 0 To CodeSubData.Tables("DelData").Rows.Count - 1
            Dim objInfo As New MaterialSubInfo
            objInfo.M_Code = strCode
            objInfo.M_CodeSub = CodeSubData.Tables("DelData").Rows(i)("M_CodeSub")
            mc.MaterialCodeSub_Delete(objInfo.M_Code, objInfo.M_CodeSub)
        Next

    End Sub

    Sub CreateTables()
        '�l���Ƽƾ�
        CodeSubData.Tables.Clear()
        '�Ыؼƾڪ�
        With CodeSubData.Tables.Add("CodeSub")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_CodeSub", GetType(String))
            .Columns.Add("M_Qty", GetType(Integer))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
        End With
        '�ЫاR���ƾڪ�
        With CodeSubData.Tables.Add("DelData")
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_CodeSub", GetType(String))
        End With
        '�j�w���
        Grid.DataSource = CodeSubData.Tables("CodeSub")
    End Sub
    Sub LoadDataToTable(ByVal tList As List(Of MaterialInfo))
        Dim i As Integer
        Dim row As DataRow
        For i = 0 To tList.Count - 1
            row = CodeSubData.Tables("CodeSub").NewRow
            row("AutoID") = "1"
            'row("M_Code") = txtCode.Text
            row("M_CodeSub") = tList(i).M_Code
            row("M_Qty") = tList(i).M_Qty
            row("M_Name") = tList(i).M_Name
            row("M_Unit") = tList(i).M_Unit
            row("M_Gauge") = tList(i).M_Gauge
            CodeSubData.Tables("CodeSub").Rows.Add(row)
        Next


    End Sub

    Private Sub cmdOpenPhoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenPhoto.Click
        Dim ofd As New OpenFileDialog
        Dim i As Integer
        ofd.Filter = "�Ϥ����(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp"
        ofd.ShowDialog()
        If ofd.FileName.ToString = "" Then Exit Sub
        fs = New IO.FileStream(ofd.FileName.ToString, IO.FileMode.Open, IO.FileAccess.Read)
        pPhoto.Image = Image.FromFile(ofd.FileName.ToString)

        Select Case CInt(ofd.OpenFile.Length / 1024)
            Case Is < 80 : i = 0
            Case Is > 80 < 100 : i = 100
            Case Is > 100 < 150 : i = 85
            Case Is > 300 : i = 65
        End Select

        Dim ci As New CompressImage
        If i = 0 Then
        Else
            ci.GetJPEG(pPhoto.Image, pPhoto, i)
        End If

    End Sub
  
    Private Sub cmdGauge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGauge.Click
        If popTypeID.Text = "" Then
            MsgBox("�S����ܪ������O,�����J!", , "����")
            Exit Sub

        End If
        frmMaterialGauge.strGauge = txtGauge.Text
        frmMaterialGauge.Label2.Text = popTypeID.Tag '@ 2012/1/7 �ק�
        frmMaterialGauge.ShowDialog()

        If frmMaterialGauge.strGauge = "����" Then
        Else
            Me.txtGauge.Text = frmMaterialGauge.strGauge
        End If


    End Sub

    Private Sub cmdDelPhoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelPhoto.Click
        pPhoto.Image = Nothing

    End Sub
  
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Mid(popTypeID.Tag.ToString, 1, 5) = "20024" Then '@ 2012/1/7 �ק�
            '***�P�_�Τ�O�֦��S�������v��
            Dim pmws2 As New PermissionModuleWarrantSubController
            Dim pmwiL2 As List(Of PermissionModuleWarrantSubInfo)

            pmwiL2 = pmws2.PermissionModuleWarrantSub_GetList(InUserID, "100115")
            If pmwiL2.Item(0).PMWS_Value = "�_" Then
                ' If pmwiL.Item(0).PMWS_Value = "�O" Then popMaterialMainAdd.Enabled = True
                MsgBox("�A�S���S�����������v��,�Э��s��ܪ������O")

                Exit Sub
            End If

            '************

        End If

        If popTypeID.EditValue.ToString = "" Then
            MsgBox("�S���إߪ��ƽs�X�A�нT�{!", 64, "����")
            popTypeID.Focus()
            Exit Sub
        End If

        '@ 2012/11/21 �K�[
        If Val(txtSaveKuCun.Text) < 0 Then
            MsgBox("�w���w�s����p��s!", 64, "����")
            XtraTabControl1.SelectedTabPage = XtraTabPage3
            txtSaveKuCun.Focus()
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To CodeSubData.Tables("CodeSub").Rows.Count - 1
            If CodeSubData.Tables("CodeSub").Rows(i)("M_Qty") = 0 Or _
                Len(CodeSubData.Tables("CodeSub").Rows(i)("M_Qty")) = 0 Then
                MsgBox("�l���Ʀ��ƶq��0,�Эץ�!", , "����")
                Exit Sub
            Else
            End If

        Next


        '***�P�_�Τ�O�֦��s�W10�A20�A�٬O30���v��
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100113")
        If pmwiL.Count > 0 Then
            ' If pmwiL.Item(0).PMWS_Value = "�O" Then popMaterialMainAdd.Enabled = True
            If InStr(pmwiL.Item(0).PMWS_Value, Mid(popTypeID.Tag, 1, 2)) = 0 Then '@ 2012/1/7 �ק�
                MsgBox("�A�S�����������s�W�έק��v��")
                Exit Sub
            End If

        End If

        If CStr(txtBlocCode.Text) <> "" Then
            If Len(txtBlocCode.Text) <> 17 Then
                MsgBox("���νs�X��J���~�A���νs�X�u��O17��!", 64, "����")
                txtBlocCode.Focus()
                Exit Sub
            End If
        End If
        '************
        Select Case Label28.Text

            Case "AddOrEdit"
                If Edit = False Then '�s�W

                    If Label29.Text = "�_��s�W" Then
                        If txtName.Text = tempValue2 And txtGauge.Text = tempValue3 And lueUnit.EditValue = tempValue4 Then
                            MsgBox("���ƦW��,�W��,��줤���������ܰʤ~��إ߷s���ơI")
                            Exit Sub
                        Else
                            SaveDataNew()
                        End If
                    ElseIf Label29.Text = "�s�W" Then

                        SaveDataNew()

                    End If

                Else         '�ק�
                    SaveDataEdit()

                End If
            Case "Photo"
                DesignPhoto()
            Case "AuditEdit"
                SaveDataEdit()
        End Select

    End Sub

    Sub SaveDataNew()
        '�O�s�s�W���
        Dim objInfo As New MaterialInfo
        '�p�G�����~���h�Ѥ�ʽs��
        If Mid(popTypeID.Tag, 1, 2) = "40" Then '@ 2012/1/7 �ק�

        Else
            txtCode.Text = mc.MaterialCode_GetID(popTypeID.Tag) '@ 2012/1/7 �ק�
        End If


        objInfo.M_Code = txtCode.Text
        objInfo.M_Name = txtName.Text
        objInfo.M_AddDate = CStr(Format(Now, "yyyy-MM-dd hh:mm:ss"))
        objInfo.M_EditDate = Nothing
        objInfo.M_Gauge = txtGauge.Text
        objInfo.Type3ID = popTypeID.Tag '@ 2012/1/7 �ק�
        objInfo.M_Unit = lueUnit.EditValue
        objInfo.M_Currency = lueCurrency.EditValue
        objInfo.M_Maker = txtMaker.Text
        objInfo.M_Supplier = gluSupplier.EditValue
        objInfo.M_SupplierNo = txtSupplierNo.EditValue

        objInfo.M_Weight = txtWeight.Text
        objInfo.M_WeightUnit = cbWeight.EditValue

        If Len(txtSaveKuCun.Text) = 0 Then
            objInfo.M_SaveKuCun = "0"
        Else
            objInfo.M_SaveKuCun = txtSaveKuCun.Text
        End If

        If Len(txtPrice.Text) = 0 Then
            objInfo.M_Price = 0
        Else
            objInfo.M_Price = CSng(txtPrice.Text)
        End If

        objInfo.BlocCode = CStr(txtBlocCode.Text)       '@ 2012/6/2 �K�[ ���νs�X

        objInfo.M_Remark = txtRemark.Text
        objInfo.InUser = InUserID

        If rodIsSub.SelectedIndex = 0 Then
            objInfo.M_IsSub = True
        Else
            objInfo.M_IsSub = False
        End If
        If rdoAccountCheck.SelectedIndex = 0 Then
            objInfo.M_AccountCheck = True
        Else
            objInfo.M_AccountCheck = False
        End If

        If rdoIsEnabled.SelectedIndex = 1 Then
            objInfo.M_IsEnabled = False
        Else
            objInfo.M_IsEnabled = True
        End If
      


        '�O�s�Ϥ�
        'If pPhoto.Image Is Nothing Then
        '    objInfo.M_Photo = Nothing
        'Else
        '    Dim photoData(fs.Length) As Byte
        '    fs.Read(photoData, 0, Int(fs.Length))
        '    objInfo.M_Photo = photoData
        'End If


        ''�O�s�Ϥ�
        'If pPhoto.Image Is Nothing Then
        '    objInfo.M_Photo = Nothing
        'Else
        '    If fs Is Nothing Then
        '        '���[�J�Ϥ���,�O�s���󤤪��Ϥ�
        '        objInfo.M_Photo = Nothing
        '        Dim output As New MemoryStream
        '        Dim iImage As Drawing.Image = Nothing
        '        Dim mem As New MemoryStream
        '        Dim myBitmap As New Bitmap(Me.pPhoto.Image)
        '        myBitmap.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg)
        '        Dim Buf(CInt(mem.Length - 1)) As Byte

        '        mem.Position = 0
        '        mem.Read(Buf, 0, CInt(mem.Length))
        '        mem.Close()
        '        objInfo.M_Photo = Buf

        '    Else
        '        Dim photoData(fs.Length) As Byte
        '        fs.Read(photoData, 0, Int(fs.Length))
        '        fs.Close()
        '        objInfo.M_Photo = photoData
        '    End If

        'End If
        objInfo.M_Photo = ImageToByte(pPhoto.Image)
        objInfo.M_CheckRemark = txtM_CheckRemark.Text.Trim    '@ 2013/3/26 �K�[

        If mc.MaterialCode_Add(objInfo) = True Then
            '�O�s�l����
            UpdateCodeSub(txtCode.Text)

            'If Mid(txtCode.Text, 1, 2) = "20" Then
            '    objInfo.M_Code = txtCode.Text
            '    objInfo.M_Price = 0
            '    objInfo.M_Currency = lueCurrency.EditValue
            '    objInfo.M_ChangeDate = Format(Now, "yyyy/MM/dd")

            '    mc.MaterialPrice_Add(objInfo) '�p�G�O������ �h�b������K�[�O��
            'End If

            'MsgBox("�O�s���\", , "����")
            Me.Close()
        Else
            MsgBox("�O�s����,���ˬd���~!", , "����")
        End If


    End Sub

    Sub MaterialPriceNew(ByVal strCode As String)

    End Sub

    Sub SaveDataEdit()
        '�O�s�ק�ƾ�
        Dim objInfo As New MaterialInfo
        objInfo.M_Code = Label27.Text
        objInfo.M_Name = txtName.Text
        objInfo.M_EditDate = CStr(Format(Now, "yyyy-MM-dd hh:mm:ss"))
        objInfo.M_AddDate = Nothing
        objInfo.M_Gauge = txtGauge.Text
        objInfo.Type3ID = popTypeID.Tag '@ 2012/1/7 �ק�
        objInfo.M_Unit = lueUnit.EditValue
        objInfo.M_Currency = lueCurrency.EditValue
        objInfo.M_Maker = txtMaker.Text
        objInfo.M_Supplier = gluSupplier.EditValue
        objInfo.M_SupplierNo = txtSupplierNo.EditValue

        objInfo.M_Weight = txtWeight.Text
        objInfo.M_WeightUnit = cbWeight.EditValue

        If Len(txtSaveKuCun.Text) = 0 Then
            objInfo.M_SaveKuCun = "0"
        Else
            objInfo.M_SaveKuCun = txtSaveKuCun.Text
        End If

        objInfo.BlocCode = CStr(txtBlocCode.Text)       '@ 2012/6/2 �K�[ ���νs�X

        If Len(txtPrice.Text) = 0 Then
            objInfo.M_Price = 0
        Else
            objInfo.M_Price = CSng(txtPrice.Text)
        End If
        objInfo.M_Remark = txtRemark.Text
        objInfo.InUser = InUserID
        If rodIsSub.SelectedIndex = 0 Then
            objInfo.M_IsSub = True
        Else
            objInfo.M_IsSub = False
        End If
        If rdoAccountCheck.SelectedIndex = 0 Then
            objInfo.M_AccountCheck = True
        Else
            objInfo.M_AccountCheck = False
        End If

        If rdoIsEnabled.SelectedIndex = 1 Then
            objInfo.M_IsEnabled = False
        Else
            objInfo.M_IsEnabled = True
        End If

        '�O�s�Ϥ�
        'If pPhoto.Image Is Nothing Then
        '    objInfo.M_Photo = Nothing
        'Else
        '    If fs Is Nothing Then
        '        '���[�J�Ϥ���,�O�s���󤤪��Ϥ�
        '        objInfo.M_Photo = Nothing
        '        Dim output As New MemoryStream
        '        Dim iImage As Drawing.Image = Nothing
        '        Dim mem As New MemoryStream
        '        Dim myBitmap As New Bitmap(Me.pPhoto.Image)
        '        myBitmap.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg)
        '        Dim Buf(CInt(mem.Length - 1)) As Byte

        '        mem.Position = 0
        '        mem.Read(Buf, 0, CInt(mem.Length))
        '        mem.Close()
        '        objInfo.M_Photo = Buf

        '    Else
        '        Dim photoData(fs.Length) As Byte
        '        fs.Read(photoData, 0, Int(fs.Length))
        '        fs.Close()
        '        objInfo.M_Photo = photoData
        '    End If

        'End If
        objInfo.M_Photo = ImageToByte(pPhoto.Image)
        objInfo.M_CheckRemark = txtM_CheckRemark.Text.Trim    '@ 2013/3/26 �K�[

        If mc.MaterialCode_Update(objInfo) = True Then
            '�O�s�l����
            UpdateCodeSub(txtCode.Text)
            'MsgBox("�ק令�\", , "����")
            Me.Close()
        Else
            MsgBox("�O�s����,���ˬd���~!", , "����")
        End If

    End Sub

    Sub DesignPhoto()
        Dim objInfo As New MaterialInfo
        objInfo.M_Code = Label27.Text
        objInfo.M_Name = txtName.Text
        objInfo.M_EditDate = CStr(Format(Now, "yyyy-MM-dd hh:mm:ss"))
        objInfo.M_AddDate = Nothing
        objInfo.M_Gauge = txtGauge.Text
        objInfo.Type3ID = popTypeID.Tag '@ 2012/1/7 �ק�
        objInfo.M_Unit = lueUnit.EditValue
        objInfo.M_Currency = lueCurrency.EditValue
        objInfo.M_Maker = txtMaker.Text
        objInfo.M_Supplier = gluSupplier.EditValue
        objInfo.M_SupplierNo = txtSupplierNo.EditValue

        objInfo.M_Weight = txtWeight.Text
        objInfo.M_WeightUnit = cbWeight.EditValue

        If Len(txtSaveKuCun.Text) = 0 Then
            objInfo.M_SaveKuCun = "0"
        Else
            objInfo.M_SaveKuCun = txtSaveKuCun.Text
        End If

        objInfo.BlocCode = CStr(txtBlocCode.Text)       '@ 2012/6/2 �K�[ ���νs�X

        If Len(txtPrice.Text) = 0 Then
            objInfo.M_Price = 0
        Else
            objInfo.M_Price = CSng(txtPrice.Text)
        End If
        objInfo.M_Remark = txtRemark.Text
        objInfo.InUser = InUserID
        If rodIsSub.SelectedIndex = 0 Then
            objInfo.M_IsSub = True
        Else
            objInfo.M_IsSub = False
        End If
        If rdoAccountCheck.SelectedIndex = 0 Then
            objInfo.M_AccountCheck = True
        Else
            objInfo.M_AccountCheck = False
        End If
        If rdoIsEnabled.SelectedIndex = 0 Then
            objInfo.M_IsEnabled = True
        Else
            objInfo.M_IsEnabled = False
        End If

        objInfo.M_Photo = ImageToByte(pPhoto.Image)
        objInfo.M_CheckRemark = txtM_CheckRemark.Text.Trim    '@ 2013/3/26 �K�[

        If mc.MaterialCode_Update(objInfo) = True Then
            UpdateCodeSub(Label27.Text)
            MsgBox("�O�s���\�I")
            Me.Close()
        Else
            MsgBox("�O�s���ѡA���ˬd��]�I")
        End If
    End Sub

    Sub LoadData(ByVal strCode As String)
        '�ɤJ�ƾ�
        On Error Resume Next
        Dim objInfo As New MaterialInfo
        objInfo = mc.MaterialCode_Get(strCode)
        txtCode.Text = objInfo.M_Code
        txtName.Text = objInfo.M_Name
        'objInfo.M_EditDate = CStr(Format(Now, "yyyy-MM-dd hh:mm:ss"))
        'objInfo.M_EditDate = Nothing
        txtGauge.Text = objInfo.M_Gauge
        popTypeID.Tag = objInfo.Type3ID '@ 2012/1/7 �ק�
        popTypeID.EditValue = objInfo.Type3Name '@ 2012/1/7 �ק�
        lueUnit.EditValue = objInfo.M_Unit
        lueCurrency.EditValue = objInfo.M_Currency
        txtMaker.Text = objInfo.M_Maker
        txtRemark.Text = objInfo.M_Remark
        'InUser=objInfo.InUser  
        txtPrice.Text = objInfo.M_Price
        Label17.Text = objInfo.M_AddDate
        Label18.Text = objInfo.M_EditDate
        Label19.Text = objInfo.InUser
        gluSupplier.EditValue = objInfo.M_Supplier
        txtSupplierNo.EditValue = objInfo.M_SupplierNo
        txtSaveKuCun.Text = objInfo.M_SaveKuCun
        txtBlocCode.Text = objInfo.BlocCode     '@ 2012/6/2 �K�[ ��ܶ��νs�X

        txtWeight.Text = objInfo.M_Weight
        cbWeight.EditValue = objInfo.M_WeightUnit

        If objInfo.M_IsSub = True Then
            rodIsSub.SelectedIndex = 0
        Else
            rodIsSub.SelectedIndex = 1
        End If
        If objInfo.M_AccountCheck = True Then
            rdoAccountCheck.SelectedIndex = 0
        Else
            rdoAccountCheck.SelectedIndex = 1
        End If
        If objInfo.M_IsEnabled = True Then
            rdoIsEnabled.SelectedIndex = 0
        Else
            rdoIsEnabled.SelectedIndex = 1
        End If
        '�ɤJ�Ϥ�
        'If objInfo.M_Photo Is Nothing Then
        'Else
        '    Dim stmphoto As New MemoryStream(objInfo.M_Photo)
        '    pPhoto.Image = Image.FromStream(stmphoto)
        'End If
        pPhoto.Image = ByteToImage(objInfo.M_Photo)
        txtM_CheckRemark.Text = objInfo.M_CheckRemark     '@ 2013/3/26 �K�[


        If objInfo.M_supplier = Nothing Then
        Else
            gluSupplier.Text = objInfo.M_supplier
            GridLookUpEdit1View.StartIncrementalSearch(objInfo.M_supplier)
        End If

        ''�ɤJ�������OTypeID1+TypeID2+TypeID3
        Dim tc As New MaterialTypeController
        Dim ti As New MaterialTypeInfo
        Dim ti1 As New MaterialTypeInfo1
        Dim ti2 As New MaterialTypeInfo2
        Dim ti3 As New MaterialTypeInfo3
        'ti = tc.MaterialTypeGet(Mid(popTypeID.Text, 1, 2))
        ti1 = tc.MaterialType1_Get(Mid(popTypeID.Tag, 1, 5)) '@ 2012/1/7 �ק�
        ti2 = tc.MaterialType2_Get(Mid(popTypeID.Tag, 1, 8)) '@ 2012/1/7 �ק�
        ti3 = tc.MaterialType3_Get(Mid(popTypeID.Tag, 1, 11)) '@ 2012/1/7 �ק�


        Label24.Text = ti1.Type1Name & "+" & ti2.Type2Name & "+" & ti3.Type3Name
        'cmdExit.Text = ti.MaterialTypeName
        'cmdSave.Text = Mid(popTypeID.Text, 1, 2)


        '�ɤJ�l����
        LoadDataToTable(mc.MaterialCodeSub_GetList(strCode))

    End Sub

    Private Sub tv1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tv1.MouseDoubleClick
        If tv1.SelectedNode.Level = 3 Then
            popTypeID.Tag = tv1.SelectedNode.Tag '@ 2012/1/7 �ק�
            popTypeID.EditValue = tv1.SelectedNode.Text '@ 2012/1/7 �ק�
            PopupContainerControl1.OwnerEdit.ClosePopup()

        End If
    End Sub

    Private Sub popTypeID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popTypeID.EditValueChanged
        On Error Resume Next

        If Edit = False Then
            If Mid(popTypeID.Tag, 1, 2) = "40" Then '@ 2012/1/7 �ק�
                txtCode.Enabled = True
                txtCode.Text = ""
            Else
                txtCode.Enabled = False
                txtCode.Text = ""
            End If
            'txtName.Text = tv1.SelectedNode.Text

        End If

        ''�ɤJ�������OTypeID1+TypeID2+TypeID3
        Dim tc As New MaterialTypeController
        Dim ti As New MaterialTypeInfo
        Dim ti1 As New MaterialTypeInfo1
        Dim ti2 As New MaterialTypeInfo2
        Dim ti3 As New MaterialTypeInfo3
        ti = tc.MaterialType_Get(Mid(popTypeID.Tag, 1, 2)) '@ 2012/1/7 �ק�
        ti1 = tc.MaterialType1_Get(Mid(popTypeID.Tag, 1, 5)) '@ 2012/1/7 �ק�
        ti2 = tc.MaterialType2_Get(Mid(popTypeID.Tag, 1, 8)) '@ 2012/1/7 �ק�
        ti3 = tc.MaterialType3_Get(Mid(popTypeID.Tag, 1, 11)) '@ 2012/1/7 �ק�
        Label24.Text = ti.MaterialTypeName & "+" & ti1.Type1Name & "+" & ti2.Type2Name & "+" & ti3.Type3Name
    End Sub

    Private Sub popCodeSubDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popCodeSubDel.Click
        If CodeSubData.Tables("CodeSub").Rows.Count = 0 Then
            Exit Sub
        End If
        If GridView1.RowCount = 0 Then Exit Sub
        'DataRow.Delete()
        Dim DelTemp As String
        DelTemp = CodeSubData.Tables("CodeSub").Rows(GridView1.FocusedRowHandle)("AutoID")
        If DelTemp = "0" Then
        Else
            '�b�R�����W�[�Q�R�����O��
            Dim row As DataRow = CodeSubData.Tables("DelData").NewRow
            'row("AutoID") = DelTemp
            row("M_Code") = txtCode.Text
            row("M_CodeSub") = CodeSubData.Tables("CodeSub").Rows(GridView1.FocusedRowHandle)("M_CodeSub")
            CodeSubData.Tables("DelData").Rows.Add(row)
        End If
        CodeSubData.Tables("CodeSub").Rows.RemoveAt(GridView1.FocusedRowHandle)

    End Sub

  

  
 
    'Private Sub txtCode_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.EditValueChanged
    '    '�ˬd�O�_�w�s�b���s��
    '    If mc.MaterialCode_Get(txtCode.Text) Is Nothing Then
    '    Else
    '        MsgBox("���p�׽s���w�s�b�A�Э��s��J�s�s��", , "����")
    '        txtCode.Focus()
    '        Exit Sub


    '    End If
    'End Sub
    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If pPhoto.Image Is Nothing Then
            MsgBox("�L�Ϥ��H��,����ɥX�I")
            Exit Sub
        End If

        Dim tempName As String
        Dim SaveFileDialog1 As New SaveFileDialog
        SaveFileDialog1.InitialDirectory = "c:\"
        SaveFileDialog1.Filter = "txt files (*.jpg)|*.jpg | (*.bmp)|*.bmp | (*.png)|*.png "
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            tempName = SaveFileDialog1.FileName
        Else
            Exit Sub
        End If

        pPhoto.Image.Save(tempName)

        MsgBox("�Ϥ��w�O�s!")
    End Sub
End Class