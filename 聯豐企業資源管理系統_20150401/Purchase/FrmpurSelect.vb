Imports LFERP.Library.MaterialParam
Imports LFERP.Library.Material
Imports LFERP.Library
Public Class FrmpurSelect
    Dim ds As New DataSet
    Dim mtc As New Material.MaterialTypeController
    Dim TempA As String, TempB As String
    Dim StrA As String, StrB As String, StrC As String
    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
      
        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            If RadioButton1.Checked = True Then
                If ComboBoxEdit1.Text = "���ʳ渹" Or ComboBoxEdit1.Text = "�����渹" Then
                    tempValue = 1
                ElseIf ComboBoxEdit1.Text = "���ƳW��" Or ComboBoxEdit1.Text = "�禬�渹" Then
                    tempValue = 2
                ElseIf ComboBoxEdit1.Text = "���ƦW��" Then
                    tempValue = 3
                ElseIf ComboBoxEdit1.Text = "���ƽs�X" Then
                    tempValue = 4
                End If
                tempValue2 = TextEdit1.Text
            End If

            If RadioButton2.Checked = True Then '�������O
                tempValue = 4
                tempValue2 = PopupContainerEdit1.Text
            End If
            If RadioButton3.Checked = True Then '������
                tempValue = 5
                tempValue2 = gluSupplier.EditValue
            End If
        Else
            tempValue = 6
            tempValue2 = ""
            For i As Integer = 0 To ds.Tables("SelectTbDel").Rows.Count - 1
                tempValue2 = tempValue2 & ds.Tables("SelectTbDel").Rows(i)("DelConditon").ToString.Trim
            Next
        End If

        Me.Close()

    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub

    Private Sub FrmpurSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        TempA = tempValue
        tempValue = ""
        TextEdit1.Select()
        ComboBoxEdit3.SelectedIndex = 0
        XtraTabControl1.SelectedTabPageIndex = 0
        DateEdit1.Text = Format(Now, "yyyy/MM/dd")

        mtc.LoadNodes(Tv1, ErpUser.MaterialType)
        PopupContainerEdit1.Enabled = False

        Dim mtd As New LFERP.DataSetting.SuppliersControler
        gluSupplier.Properties.DisplayMember = "S_SupplierName"
        gluSupplier.Properties.ValueMember = "S_Supplier"
        gluSupplier.Properties.DataSource = mtd.GetSuppliersList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        gluSupplier.Enabled = False


        If TempA = "���ʺ޲z--���Ʊ���" Or TempA = "���ʺ޲z--��}���ʳ�" Then
            ComboBoxEdit1.Properties.Items.Clear()
            ComboBoxEdit1.Properties.Items.Add("���ʳ渹")
            ComboBoxEdit1.Properties.Items.Add("���ƽs�X")
            ComboBoxEdit1.Properties.Items.Add("���ƳW��")
            ComboBoxEdit1.Properties.Items.Add("���ƦW��")
        ElseIf TempA = "�禬�޲z" Then
            ComboBoxEdit1.Properties.Items.Clear()
            ComboBoxEdit1.Properties.Items.Add("���ʳ渹")
            ComboBoxEdit1.Properties.Items.Add("�禬�渹")
            ComboBoxEdit1.Properties.Items.Add("���ƽs�X")
            ComboBoxEdit1.Properties.Items.Add("���ƦW��")
        ElseIf TempA = "�h�f�޲z" Then
            ComboBoxEdit1.Properties.Items.Clear()
            ComboBoxEdit1.Properties.Items.Add("���ʳ渹")
            ComboBoxEdit1.Properties.Items.Add("�h�f�渹")
            ComboBoxEdit1.Properties.Items.Add("���ƽs�X")
            ComboBoxEdit1.Properties.Items.Add("���ƦW��")
        ElseIf TempA = "������޲z-����" Then
            ComboBoxEdit1.Properties.Items.Clear()
            ComboBoxEdit1.Properties.Items.Add("�����渹")
            ComboBoxEdit1.Properties.Items.Add("���ƽs�X")
            ComboBoxEdit1.Properties.Items.Add("���ƦW��")
            ComboBoxEdit1.Properties.Items.Add("���ƳW��")
        End If


        CreateTable()
        LoadTable()
        RadioButton1.Checked = True

    End Sub
    Private Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("SelectTb")
            .Columns.Add("AutoID", GetType(Integer))
            .Columns.Add("Category", GetType(String))
            .Columns.Add("Type", GetType(String))
            .Columns.Add("FieldName", GetType(String))
        End With
        GridControl1.DataSource = ds.Tables("SelectTb")

        With ds.Tables.Add("SelectTbDel")
            .Columns.Add("DelConditon", GetType(String))
            .Columns.Add("DelDisplay", GetType(String))
        End With
        GridControl2.DataSource = ds.Tables("SelectTbDel")
    End Sub
    Private Sub LoadTable()
        Dim Row As DataRow
        On Error Resume Next

        If Mid(TempA, 1, 5).Equals("������޲z") Then
            StrA = "�����渹,���ƽs�X,���ƦW��,�������O,������,�������,�����f��,�|�p�f��"
            StrB = "��r,��r,��r,��r,��r,���,��r,��r"
            StrC = "Quotation.Q_QuoID,MaterialCode.M_Code,MaterialCode.M_Name,Type3ID,Q_Supplier,Q_AddDate,Q_Check,Q_AccCheck"
        End If
        If Mid(TempA, 1, 4).Equals("���ʺ޲z") Then
            StrA = "���ʳ渹,���ƽs�X,���ƦW��,���ƳW��,��������,������,�|�v,���O,�����,���ʤ��,��f���,�_�֤��,�����f��,�|�p�f��,�ާ@��"
            StrB = "��r,��r,��r,��r,��r,��r,��r,��r,��r,���,���,���,��r,��r,��r"
            StrC = "PurchaseMain.PM_NO,PurchaseSub.M_Code,MaterialCode.M_Name,MaterialCode.M_Gauge,MaterialCode.M_Code,PurchaseMain.S_Supplier,PurchaseSub.PS_Tarriff,C_ID,PS_NoSendQty,PM_PurchaseDate,PS_SendDate,PM_AccountCheckDate,PM_Check,PM_AccountCheck,PM_Action"
        End If
        If TempA = "�禬�޲z" Then
            StrA = "�禬�渹,���ʳ渹,�e�f�渹,�妸�s��,���ƽs�X,���ƦW��,���ƳW��,������,�e�f���,�|�p�f��,�I�ڽT�{,�I������"
            StrB = "��r,��r,��r,��r,��r,��r,��r,��r,���,��r,��r,��r"
            StrC = "A_AcceptanceNO,PM_NO,A_SendNO,OS_BatchID,Acceptance.M_Code,MaterialCode.M_Name,MaterialCode.M_Gauge,Acceptance.S_Supplier,A_SendDate,A_AccountCheck,A_PayCheck,A_ToFrom"
        End If
        If TempA = "�h�f�޲z" Then
            StrA = "�h�f�渹,���ʳ渹,�e�f�渹,�妸�s��,���ƽs�X,���ƦW��,���ƳW��,������,�h�f���"
            StrB = "��r,��r,��r,��r,��r,��r,��r,��r,���"
            StrC = "R_RetrocedeNO,PM_NO,A_SendNO,OS_BatchID,Retrocede.M_Code,MaterialCode.M_Name,MaterialCode.M_Gauge,Retrocede.S_Supplier,Retrocede.R_ReturnDate"
        End If
       
        Dim StrAarray As Array = Split(StrA, ",")
        Dim StrBarray As Array = Split(StrB, ",")
        Dim StrCarray As Array = Split(StrC, ",")

        For i As Integer = 0 To UBound(StrAarray)

            Row = ds.Tables("SelectTb").NewRow
            Row("AutoID") = i
            Row("Category") = StrAarray(i)
            Row("Type") = StrBarray(i)
            Row("FieldName") = StrCarray(i)
            ds.Tables("SelectTb").Rows.Add(Row)
        Next i

    End Sub

    Private Sub SimpleButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton7.Click
        Dim fr As New FrmpurSelectCondition
        Dim Row As DataRow
        tempValue = GridView1.GetFocusedRowCellValue("Category")
        fr.ShowDialog()
        TempB = tempValue2
        tempValue2 = ""
        If TempB = "" Then Exit Sub
        Row = ds.Tables("SelectTbDel").NewRow
        If GridView1.GetFocusedRowCellValue("Category") = "�������O" Then
            Row("DelConditon") = "  " & Mid(Trim(ComboBoxEdit3.Text), 1, 3) + "  " + GridView1.GetFocusedRowCellValue("FieldName") + " Like " + "'%" + TempB + "%'" + " "
        ElseIf GridView1.GetFocusedRowCellValue("Category") = "�����" Then
            Row("DelConditon") = "  " & Mid(Trim(ComboBoxEdit3.Text), 1, 3) + "  " + GridView1.GetFocusedRowCellValue("FieldName") + " " + TempB + " "
        Else
            Row("DelConditon") = "  " & Mid(Trim(ComboBoxEdit3.Text), 1, 3) + "  " + GridView1.GetFocusedRowCellValue("FieldName") + " = " + "'" + TempB + "'" + " "
        End If

        If ds.Tables("SelectTbDel").Rows.Count = 0 Then
            Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " = " + "'" + TempB + "'"
        Else
            Row("DelDisplay") = Mid(Trim(ComboBoxEdit3.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " = " + "'" + TempB + "'"
        End If
        ds.Tables("SelectTbDel").Rows.Add(Row)
        TempB = ""
    End Sub

    Private Sub GridView1_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        On Error Resume Next
        
        If GridView1.GetFocusedRowCellValue("Type") = "���" Then
            XtraTabControl2.SelectedTabPageIndex = 1
            XtraTabControl2.TabPages(0).PageEnabled = False
            XtraTabControl2.TabPages(1).PageEnabled = True
            LabelControl3.Text = ""
        Else
            Dim tempC As String = "���ʳ渹,���ƽs�X,���ƦW��,���ƳW��,�����渹,�禬�渹,�h�f�渹,�妸�s��,�e�f�渹"
            Dim Strarray As Array = Split(tempC, ",")
            For i As Integer = 0 To UBound(Strarray)
                If GridView1.GetFocusedRowCellValue("Category").ToString = Strarray(i) Then
                    SimpleButton7.Enabled = False
                    SimpleButton5.Enabled = True
                    TextEdit2.Enabled = True
                    TextEdit2.Focus()
                    Exit For
                Else
                    TextEdit2.Enabled = False
                    SimpleButton5.Enabled = False
                    SimpleButton7.Enabled = True
                End If
            Next

            XtraTabControl2.SelectedTabPageIndex = 0
            XtraTabControl2.TabPages(0).PageEnabled = True
            XtraTabControl2.TabPages(1).PageEnabled = False
            LabelControl3.Text = "(" + GridView1.GetFocusedRowCellValue("Category") + ")"
        End If

    End Sub

    Private Sub SimpleButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton4.Click
        Dim Row As DataRow

        Row = ds.Tables("SelectTbDel").NewRow
        Row("DelConditon") = " " & Mid(ComboBoxEdit3.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + "  " + Mid(Trim(ComboBoxEdit2.Text), 1, 3) + " " + "'" + DateEdit1.Text + "'" + " "
        If ds.Tables("SelectTbDel").Rows.Count = 0 Then
            Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ComboBoxEdit2.Text), 3) + " " + "'" + DateEdit1.Text + "'"
        Else
            Row("DelDisplay") = Mid(Trim(ComboBoxEdit3.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ComboBoxEdit2.Text), 3) + " " + "'" + DateEdit1.Text + "'"
        End If
        ds.Tables("SelectTbDel").Rows.Add(Row)
    End Sub

    Private Sub Tv1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Tv1.AfterSelect
        PopupContainerEdit1.EditValue = Tv1.SelectedNode.Tag
        PopupContainerControl1.OwnerEdit.ClosePopup()
    End Sub


    Private Sub RadioButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.Click
       
        ComboBoxEdit1.Enabled = True
        TextEdit1.Enabled = True
        
        PopupContainerEdit1.Enabled = False
        PopupContainerEdit1.Text = ""
        
        gluSupplier.Enabled = False
        gluSupplier.Text = ""
        ComboBoxEdit1.Focus()
    End Sub

    Private Sub RadioButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.Click

        ComboBoxEdit1.Enabled = False
        TextEdit1.Enabled = False
        TextEdit1.Text = ""
       
        PopupContainerEdit1.Enabled = True
        
        gluSupplier.Enabled = False
        gluSupplier.Text = ""
        PopupContainerEdit1.Focus()
    End Sub

    Private Sub RadioButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.Click
       
        ComboBoxEdit1.Enabled = False
        TextEdit1.Enabled = False
        TextEdit1.Text = ""

        PopupContainerEdit1.Enabled = False
        PopupContainerEdit1.Text = ""
        
        gluSupplier.Enabled = True
        gluSupplier.Focus()
    End Sub

    Private Sub SimpleButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton6.Click
        If GridView2.RowCount = 0 Then Exit Sub

        Dim i As Integer = ds.Tables("SelectTbDel").Rows.Count - 1
        ds.Tables("SelectTbDel").Rows.RemoveAt(i)

    End Sub

    Private Sub SimpleButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton5.Click
        If TextEdit2.Text = "" Then
            MsgBox("�ƾڬ���,����K�[!")
            Exit Sub
        End If
        Dim Row As DataRow
        Row = ds.Tables("SelectTbDel").NewRow
        If GridView1.GetFocusedRowCellValue("Category") = "���ƦW��" Or GridView1.GetFocusedRowCellValue("Category") = "���ƳW��" Or GridView1.GetFocusedRowCellValue("Category") = "��������" Then
            Row("DelConditon") = " " & Mid(Trim(ComboBoxEdit3.Text), 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " like " + "'%" + Trim(TextEdit2.Text) + "%'" + " "
        Else
            Row("DelConditon") = " " & Mid(Trim(ComboBoxEdit3.Text), 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " = " + "'" + Trim(TextEdit2.Text) + "'" + " "
        End If

        If ds.Tables("SelectTbDel").Rows.Count = 0 Then
            Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " = " + "'" + Trim(TextEdit2.Text) + "'"
        Else
            Row("DelDisplay") = Mid(Trim(ComboBoxEdit3.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " = " + "'" + Trim(TextEdit2.Text) + "'"
        End If

        ds.Tables("SelectTbDel").Rows.Add(Row)
        TextEdit2.Text = ""
    End Sub

    Private Sub ComboBoxEdit1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged
        TextEdit1.Focus()
    End Sub

    Private Sub GridControl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridControl1.Click

    End Sub
End Class
