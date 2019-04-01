Imports LFERP.Library.ProductionPieceWorkGroup

Public Class frmProductionPieceWorkGroup

    Private Sub ProductionPieceWorkGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TxtG_NO.Text = tempValue2
        CaoTypeLabel.Text = MTypeName


        Dim pc As New ProductionPieceWorkGroupControl
        txtDepID.Properties.DataSource = pc.DepFac_GetList1(strInDepID, Nothing, Nothing, Nothing)
        txtDepID.Properties.DisplayMember = "UserDep_Fac"
        txtDepID.Properties.ValueMember = "DepID"

        MTypeName = Nothing
        tempValue2 = Nothing

        labFacID.Text = ""

        Select Case CaoTypeLabel.Text
            Case "WGAdd"
                Me.Text = "�էO--�W�["
                lblTittle.Text = "�էO--�W�["
                txtDepID.Enabled = True
            Case "WGEdit"
                If LoadData(TxtG_NO.Text) = False Then Exit Sub

                Me.Text = "�էO--�ק�" & "[" & TxtG_NO.Text & "]"
                lblTittle.Text = "�էO--�ק�"
                cmdAdd.Visible = False

            Case "WGView"

                If LoadData(TxtG_NO.Text) = False Then Exit Sub
                '�d��
                cmdSave.Visible = False
                cmdAdd.Visible = False
                Me.Text = "�էO--�d��" & "[" & TxtG_NO.Text & "]"
                lblTittle.Text = "�էO--�d��"
        End Select

        TxtG_NO.Enabled = False

        'txtFacID.Enabled = False
        'txtDepID.Enabled = False


    End Sub
    ''' <summary>
    ''' �[���ƾ�
    ''' </summary>
    ''' <param name="Str_G_no"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadData(ByVal Str_G_no As String) As Boolean
        LoadData = False

        Dim objInfo As New ProductionPieceWorkGroupInfo
        Dim objList As New List(Of ProductionPieceWorkGroupInfo)
        Dim oc As New ProductionPieceWorkGroupControl

        objList = oc.ProductionPieceWorkGroup_GetList(Str_G_no, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If objList.Count <= 0 Then
            MsgBox("�ƾڮw�����s�b���ƾڡI", 64, "����")
            LoadData = False
            Exit Function
        Else
            txtDepID.EditValue = objList(0).DepID
            labFacID.Text = objList(0).FacID
            txtG_Name.Text = objList(0).G_Name ''�էO�W��
            txtG_Manager.Text = objList(0).G_Manager  ''�էO�t�d�H
            MemoG_Remark.Text = objList(0).G_Remark

            LoadData = True

        End If

    End Function
    ''' <summary>
    ''' �ˬd�ƾڿ�J���T
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckData() As Boolean
        CheckData = True

        If txtDepID.EditValue = "" Then
            MsgBox("�п�ܳ���!", 64, "����")
            txtDepID.Select()
            CheckData = False
            Exit Function
        End If

        If txtG_Name.Text = "" Then
            MsgBox("�ж�g�էO�W��!", 64, "����")
            txtG_Name.Focus()
            CheckData = False
            Exit Function   '
        End If

        If txtG_Manager.Text = "" Then
            MsgBox("�ж�g�էO�t�d�H!", 64, "����")
            txtG_Manager.Focus()
            CheckData = False
            Exit Function
        End If

    End Function
    ''' <summary>
    ''' �s�W�ƾ�
    ''' </summary>
    ''' <param name="S_model"></param>
    ''' <remarks></remarks>
    Private Sub SaveNew(ByVal S_model As String)

        Dim gc As New ProductionPieceWorkGroupControl
        Dim gi As New ProductionPieceWorkGroupInfo
        Dim pgi As List(Of ProductionPieceWorkGroupInfo)

        pgi = gc.ProductionPieceWorkGroup_GetList(Nothing, txtG_Name.Text.Trim, Nothing, txtDepID.EditValue, labFacID.Text, Nothing, Nothing, Nothing)
        '�P�_�էO�W�٬O�_�w�s�b
        If pgi.Count > 0 Then
            MsgBox("�էO�W�٤w�s�b�A�Э��s��J!", 64, "����")
            txtG_Name.Focus()
            txtG_Name.SelectAll()
            Exit Sub
        End If

        TxtG_NO.Text = GetWorkGroupNO() ''����禬�渹

        If TxtG_NO.Text = "" Then
            MsgBox("�էO�渹�s�W���ѡA���ˬd", 64, "����")
            Exit Sub
        End If

        gi.G_NO = TxtG_NO.Text.Trim  ''�էO�s��
        gi.G_Name = txtG_Name.Text.Trim  ''�էO�W��

        gi.DepID = txtDepID.EditValue  ''�����s��
        gi.FacID = labFacID.Text

        gi.G_Manager = txtG_Manager.Text.Trim

        gi.G_Remark = MemoG_Remark.Text
        gi.G_Action = InUserID
        gi.G_Date = Format(Now, "yyyy/MM/dd")

        If S_model = "S" Then
            If gc.ProductionPieceWorkGroup_Add(gi) = True Then
                MsgBox("�ƾڷs�W���\", 64, "����")
                Me.Close()
            End If
        Else
            If gc.ProductionPieceWorkGroup_Add(gi) = False Then
                MsgBox("�ƾڷs�W����", 64, "����")
            Else
                Clr_Text()
            End If
        End If
    End Sub
    ''' <summary>
    ''' ��O�s���h�X�ɡA�ܶq�A�ƾڮزM��
    ''' </summary>
    ''' <remarks></remarks>
    Sub Clr_Text()
        TxtG_NO.Text = ""
        txtG_Name.Text = ""
        txtG_Manager.Text = ""
        MemoG_Remark.Text = ""
    End Sub

    ''' <summary>
    ''' ����y����Ƹ�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetWorkGroupNO() As String

        Dim gc As New ProductionPieceWorkGroupControl
        Dim gi As New ProductionPieceWorkGroupInfo

        gi = gc.ProductionPieceWorkGroup_GetNO(txtDepID.EditValue) '' Ū�����

        If gi Is Nothing Then
            GetWorkGroupNO = txtDepID.EditValue & "001"
        Else
            GetWorkGroupNO = txtDepID.EditValue & Mid((CInt(Microsoft.VisualBasic.Right(gi.G_NO, 3)) + 1001), 2)
        End If
    End Function
    ''' <summary>
    ''' �ק�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveEdit()
        Dim gc As New ProductionPieceWorkGroupControl
        Dim gi As New ProductionPieceWorkGroupInfo
        Dim pgi As List(Of ProductionPieceWorkGroupInfo)

        pgi = gc.ProductionPieceWorkGroup_GetList(Nothing, txtG_Name.Text.Trim, Nothing, txtDepID.EditValue, labFacID.Text, Nothing, Nothing, Nothing)
        '�P�_�էO�W�٬O�_�w�s�b
        If pgi.Count > 0 Then
            MsgBox("�էO�W�٤w�s�b�A�Э��s��J!", 64, "����")
            txtG_Name.Focus()
            txtG_Name.SelectAll()
            Exit Sub
        End If

        If TxtG_NO.Text = "" Then
            MsgBox("�էO�渹�ק異�ѡA���ˬd")
            Exit Sub
        End If

        gi.G_NO = TxtG_NO.Text.Trim  ''�էO�s��
        gi.G_Name = txtG_Name.Text.Trim  ''�էO�W��

        gi.DepID = txtDepID.EditValue   ''�����s��
        gi.FacID = labFacID.Text    ''�t�O

        gi.G_Manager = txtG_Manager.Text.Trim

        gi.G_Remark = MemoG_Remark.Text
        gi.G_Action = InUserID
        gi.G_Date = Format(Now, "yyyy/MM/dd")


        If gc.ProductionPieceWorkGroup_Update(gi) = True Then
            MsgBox("�ƾڭק令�\")
            Me.Close()
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If CheckData() = False Then Exit Sub

        Select Case CaoTypeLabel.Text
            Case "WGAdd"
                Call SaveNew("S")
            Case "WGEdit"
                Call SaveEdit()
        End Select
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If CheckData() = False Then Exit Sub

        If CaoTypeLabel.Text = "WGAdd" Then
            Call SaveNew("A")
            txtG_Name.Select()
        End If
    End Sub

    Private Sub txtDepID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDepID.EditValueChanged
        If txtDepID.EditValue <> "" Then
            Dim pc As New ProductionPieceWorkGroupControl
            Dim piList As New List(Of ProductionPieceWorkGroupInfo)

            piList = pc.DepFac_GetList1(txtDepID.EditValue, Nothing, Nothing, Nothing)

            If piList.Count > 0 Then
            Else
                Exit Sub
            End If

            labFacID.Text = piList(0).FacID
        End If
    End Sub
End Class