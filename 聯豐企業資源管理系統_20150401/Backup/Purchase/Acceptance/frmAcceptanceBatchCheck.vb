Imports LFERP.Library.Purchase.Acceptance
Imports LFERP.Library.Purchase.Retrocede

Public Class frmAcceptanceBatchCheck
    Dim ac As New AcceptanceController
    Dim ds As New DataSet
    Dim strSupplier, strDateBegin, strDateEnd As String

    ''' <summary>
    ''' �����f�֫��s�A�f�֩Ҧ��禬��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheck.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim i As Integer

        '�禬���q�f��
        If Me.lblTittle.Text = "�禬���q�f��" Then
            For i = 0 To GridView1.RowCount - 1
                Dim ai As New AcceptanceInfo
                ai.A_NO = GridView1.GetRowCellValue(i, "A_NO")
                ai.A_AccountCheck = True
                ai.A_AccountCheckType = "�T�{�L�~"
                ai.A_AccountCheckDate = Format(Now, "yyyy/MM/dd")
                ai.A_AccountCheckAction = InUserID
                ai.A_AccountCheckRemark = ""

                ac.Acceptance_UpdateAccountCheck(ai)
            Next
            MsgBox("�f�֧���!", 64, "����")

            '�禬���q�I�ڽT�{
        ElseIf Me.lblTittle.Text = "�禬���q�I�ڽT�{" Then
            For i = 0 To GridView1.RowCount - 1
                Dim ai As New AcceptanceInfo
                ai.A_NO = GridView1.GetRowCellValue(i, "A_NO")
                ai.A_PayCheck = True
                ai.A_PayCheckDate = Format(Now, "yyyy/MM/dd")
                ai.A_PayCheckAction = InUserID
                ai.A_PayCheckRemark = ""

                Dim rc As New RetrocedeController
                Dim riList As New List(Of RetrocedeInfo)

                riList = rc.Retrocede_GetList(Nothing, GridView1.GetRowCellValue(i, "A_AcceptanceNO"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If riList.Count > 0 Then
                    '�p�G���h�f�O�������p�U
                    ai.A_Detail = "�h�f���B�z"
                Else
                    '�p�G�S���h�f�O�������p�U
                    ai.A_Detail = "�w����"
                End If
                ac.Acceptance_UpdatePay(ai)
            Next
            MsgBox("�I�ڽT�{�����I", 64, "����")

        End If
        Me.Close()
    End Sub
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("Acceptance")
            .Columns.Add("A_NO")
            .Columns.Add("A_AcceptanceNO")
            .Columns.Add("PM_NO")
            .Columns.Add("M_Code")
            .Columns.Add("M_Name")
            .Columns.Add("A_SendNO")
            .Columns.Add("A_Qty")
            .Columns.Add("A_SendDate")
            .Columns.Add("S_SupplierName")
            .Columns.Add("Status")
        End With
        Grid.DataSource = ds.Tables("Acceptance")
    End Sub

    ''' <summary>
    ''' �[������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmAcceptanceBatchCheck_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mtd As New LFERP.DataSetting.SuppliersControler
        '�[�������� 
        gluSupplier.Properties.DataSource = mtd.GetSuppliersList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "True")
        gluSupplier.Properties.DisplayMember = "S_SupplierName"
        gluSupplier.Properties.ValueMember = "S_Supplier"

        If Me.lblTittle.Text = "�禬���q�f��" Then

        ElseIf Me.lblTittle.Text = "�禬���q�I�ڽT�{" Then
            Me.Text = "�禬���q�I�ڽT�{"
            btnCheck.Text = "�T�{(&O)"
            CreateTable()
        End If
    End Sub
    ''' <summary>
    ''' �����d�߫��s�A�d���禬��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click

        If gluSupplier.Text.Trim = "" And dteA_SendDateBegin.Text.Trim = "" And dteA_SendDateEnd.Text.Trim = "" Then
            MsgBox("�п�J�z�����!", 64, "����")
            gluSupplier.Focus()
            Exit Sub
        End If

        If dteA_SendDateBegin.DateTime > dteA_SendDateEnd.DateTime And dteA_SendDateEnd.Text.Trim <> "" Then
            MsgBox("��J�e�f������Ǧ��~�A�Э��s��J!", 64, "����")
            dteA_SendDateBegin.Focus()
            Exit Sub
        End If

        If gluSupplier.Text.Trim = "" Then
            strSupplier = Nothing
        Else
            strSupplier = gluSupplier.EditValue
        End If

        If dteA_SendDateBegin.Text.Trim = "" Then
            strDateBegin = Nothing
        Else
            strDateBegin = dteA_SendDateBegin.Text.Trim
        End If

        If dteA_SendDateEnd.Text.Trim = "" Then
            strDateEnd = Nothing
        Else
            strDateEnd = dteA_SendDateEnd.Text.Trim
        End If
        If Me.lblTittle.Text = "�禬���q�f��" Then
            Grid.DataSource = ac.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, "True", "False", Nothing, Nothing, strSupplier, Nothing, strDateBegin, strDateEnd)
        ElseIf Me.lblTittle.Text = "�禬���q�I�ڽT�{" Then
            Dim ai As List(Of AcceptanceInfo)
            Dim row As DataRow
            Dim i%
            ai = ac.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, "True", "True", Nothing, Nothing, strSupplier, "False", strDateBegin, strDateEnd)

            If ai.Count <= 0 Then
                Grid.DataSource = Nothing
                Exit Sub
            End If

            For i = 0 To ai.Count - 1
                row = ds.Tables("Acceptance").NewRow
                row("A_NO") = ai(i).A_NO
                row("A_AcceptanceNO") = ai(i).A_AcceptanceNO
                row("PM_NO") = ai(i).PM_NO
                row("M_Code") = ai(i).M_Code
                row("M_Name") = ai(i).M_Name
                row("A_SendNO") = ai(i).A_SendNO
                row("A_Qty") = ai(i).A_Qty
                row("A_SendDate") = ai(i).A_SendDate
                row("S_SupplierName") = ai(i).S_SupplierName

                '�P�_�O�_���h�f�O��
                Dim rc As New RetrocedeController
                Dim riList As New List(Of RetrocedeInfo)
                riList = rc.Retrocede_GetList(Nothing, ai(i).A_AcceptanceNO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, ai(i).M_Code, Nothing, Nothing, Nothing)
                If riList.Count > 0 Then
                    row("Status") = "�s�b�h�f�O��"
                End If
                ds.Tables("Acceptance").Rows.Add(row)
            Next
        End If

    End Sub
    ''' <summary>
    ''' �����������s�A�h�X����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' �����k��R�����A�R���P�襤���ۦP���Ҧ��禬��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsDelete.Click
        Dim i, j%
        Dim strA_AcceptanceNO As String
        strA_AcceptanceNO = GridView1.GetFocusedRowCellValue("A_AcceptanceNO")     '�O���襤�����禬�渹
        For j = 0 To GridView1.RowCount - 1        '�⦸For�`����{�R���P�襤���ۦP���Ҧ��禬��
            For i = 0 To GridView1.RowCount - 1
                If strA_AcceptanceNO = GridView1.GetRowCellValue(i, "A_AcceptanceNO") Then      '�P�_�O�_���P�襤���ۦP���禬�渹
                    GridView1.DeleteRow(i)      '�R����
                End If
            Next
        Next
    End Sub
    ''' <summary>
    ''' �����k��d�ݵ��A�d�ݿ襤�禬��H��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsView.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmAcceptance

        tempValue2 = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        MTypeName = "AcceptanceView"

        fr = New frmAcceptance
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' �����ӦW�ٿ�J�ؤ����Ů���u�X�U�Ե��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluSupplier_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gluSupplier.KeyDown
        If e.KeyCode = Keys.Space Then
            gluSupplier.ShowPopup()
        End If
    End Sub
End Class