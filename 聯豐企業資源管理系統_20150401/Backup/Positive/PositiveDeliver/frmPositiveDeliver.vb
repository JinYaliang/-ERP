Imports LFERP.Library.Positive
Imports LFERP.DataSetting

Public Class frmPositiveDeliver

#Region "�w�s�Ѽ�"
    Private m_DoCtrl As New PositiveDeliverController
    Private m_DsListInfo As New List(Of PositiveDeliverInfo)

    Private m_PoCtrl As New PositiveOrdersController
    Private m_PsListInfo As New List(Of PositiveOrdersInfo)


    Private m_DataSet As New DataSet                '�ƾڶ�

    Private m_EditItemDescrip As String             '�y�z

    Public Property EditItemDescrip() As String
        Get
            Return m_EditItemDescrip
        End Get
        Set(ByVal value As String)
            m_EditItemDescrip = value
        End Set
    End Property


    Private m_DOMID As String                       '�e�f�s��

    Public Property DOMID() As String
        Get
            Return m_DOMID
        End Get
        Set(ByVal value As String)
            m_DOMID = value
        End Set
    End Property


    Private m_BolFalg As Boolean

    Public Property BolFalg() As String
        Get
            Return m_BolFalg
        End Get
        Set(ByVal value As String)
            m_BolFalg = value
        End Set
    End Property

#End Region

#Region "�Ы��{�ɪ�"
    Private Sub CreateTables()

        m_DataSet.Tables.Clear()
        With m_DataSet.Tables.Add("PositiveDeliver")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("P_OM_Num", GetType(String))
            .Columns.Add("P_OM_ID", GetType(String))
            .Columns.Add("P_OM_CusterPO", GetType(String))
            .Columns.Add("P_OM_CusterNo", GetType(String))
            .Columns.Add("P_M_Code", GetType(String))
            .Columns.Add("PartNumber", GetType(String))
            .Columns.Add("Qty", GetType(Integer))
            .Columns.Add("OrderQty", GetType(String))
            .Columns.Add("NoSendQty", GetType(String))
            .Columns.Add("D_NO", GetType(String))
            .Columns.Add("D_OM_Num", GetType(String))
            .Columns.Add("D_AddAction", GetType(String))
            .Columns.Add("D_AddDate", GetType(String))
            .Columns.Add("D_Remark", GetType(String))
            .Columns.Add("D_Check", GetType(String))
            .Columns.Add("D_CheckDate", GetType(String))
            .Columns.Add("D_CheckRemark", GetType(String))
            .Columns.Add("D_OMCheckRemark", GetType(String))  ''

            .Columns.Add("D_TypeID", GetType(String))  ''D_TypeID
        End With
        dgPositiveDeliver.DataSource = m_DataSet.Tables("PositiveDeliver")

        '�ЫاR���ƾڪ�
        With m_DataSet.Tables.Add("DelData")
            .Columns.Add("AutoID", GetType(String))
        End With

    End Sub
#End Region

#Region "�ͦ��s�q�渹"
    ''' <summary>
    ''' �ͦ��s�q�渹
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDOI_No() As String

        Dim m_DsInfo As New PositiveDeliverInfo

        m_DsInfo = m_DoCtrl.PositiveDeliver_GetNO("DOI" + Now.ToString("yyMM"), Nothing)

        If m_DsInfo Is Nothing Then
            Return "DOI" & Now.ToString("yy") & Now.ToString("MM") & "0001"
        Else
            Return "DOI" & Now.ToString("yy") & Now.ToString("MM") & Mid((CInt(Mid(m_DsInfo.D_NO, 6)) + 10001), 2)
        End If

    End Function

#End Region

#Region "�[���Ȥ�N��"
    Private Sub LoadgluCuster()
        Dim cusCtrl As New CusterControler
        Dim cusInfo As New List(Of CusterInfo)

        gluCuster.Properties.DisplayMember = "C_CusterID"
        gluCuster.Properties.ValueMember = "C_CusterID"
        gluCuster.Properties.DataSource = cusCtrl.GetCusterList(Nothing, Nothing, Nothing)

    End Sub

    ''' <summary>
    ''' �[���Ȥ�H��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluCuster_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluCuster.EditValueChanged
        Dim cusCtrl As New CusterControler
        Dim cusInfo As New List(Of CusterInfo)

        cusInfo = cusCtrl.GetCusterList(gluCuster.EditValue, Nothing, Nothing)

        If cusInfo.Count <= 0 Then
            Exit Sub
        End If

        txtCusterName.Text = cusInfo(0).C_ChsName
        txtAddr.Text = cusInfo(0).C_Adder1
     
    End Sub

#End Region

#Region "�[���e�f����"
    Private Sub LoadPositiveDeliverType()
        ResLookUp.DisplayMember = "D_TypeName"
        ResLookUp.ValueMember = "D_TypeID"
        ResLookUp.DataSource = m_DoCtrl.PositiveDeliver_GetListType(Nothing, Nothing)
    End Sub
   
#End Region

#Region "�k����ﶵ"
    ''' <summary>
    ''' �s�W
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popPositiveDeliverAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPositiveDeliverAdd.Click
        If gluCuster.EditValue = Nothing Then
            MsgBox("�п�ܫȤ�N��", 64, "����")
            Exit Sub
        End If

        Dim frmSelect As New frmPositiveDeliverSelect
        frmSelect.CusterNo = gluCuster.EditValue
        frmSelect.ShowDialog()

        If frmPositiveDeliverSelect.ListTemp Is Nothing Then
            Exit Sub
        End If

        If frmPositiveDeliverSelect.ListTemp.Count > 0 Then

            gluCuster.Enabled = False

            For nIndex As Integer = 0 To frmPositiveDeliverSelect.ListTemp.Count - 1
                Dim row As DataRow

                row = m_DataSet.Tables("PositiveDeliver").NewRow

                'm_PsListInfo = m_PoCtrl.PositiveOrders_GetList(Nothing, frmPositiveDeliverSelect.ListTemp(nIndex).ToString(), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                row("P_OM_CusterNo") = m_PsListInfo(0).P_OM_CusterNo

                row("P_OM_CusterPO") = m_PsListInfo(0).P_OM_CusterPO

                row("P_OM_Num") = m_PsListInfo(0).P_OM_Num

                row("P_M_Code") = m_PsListInfo(0).P_M_Code

                row("PartNumber") = m_PsListInfo(0).PartNumber

                row("OrderQty") = m_PsListInfo(0).Qty

                row("NoSendQty") = m_PsListInfo(0).NoSendQty

                m_DataSet.Tables("PositiveDeliver").Rows.Add(row)

            Next
        End If



    End Sub
    ''' <summary>
    ''' �R��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popPositiveDeliverDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPositiveDeliverDel.Click
        If dgvPositiveDeliver.RowCount = 0 Then Exit Sub
        Dim delTemp As String
        Try
            delTemp = dgvPositiveDeliver.GetRowCellDisplayText(ArrayToString(dgvPositiveDeliver.GetSelectedRows()), "AutoID")

            If delTemp = "AutoID" Then
            Else
                '�b�R�����W�[�Q�R�����O��
                If delTemp <> String.Empty Then
                    Dim row As DataRow = m_DataSet.Tables("DelData").NewRow
                    row("AutoID") = delTemp
                    m_DataSet.Tables("DelData").Rows.Add(row)
                End If
               
            End If
            m_DataSet.Tables("PositiveDeliver").Rows.RemoveAt(CInt(ArrayToString(dgvPositiveDeliver.GetSelectedRows())))

            If dgvPositiveDeliver.RowCount <= 0 Then
                gluCuster.Enabled = True
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "��l�[���ƾ�"
    Private Sub frmPositiveDeliver_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateTables()
        LoadgluCuster()
        LoadPositiveDeliverType()


        Select Case m_EditItemDescrip
            Case "Add"
                txtD_NO.Text = GetDOI_No()
                txtD_AddAction.Text = UserName
                dtPositiveDeliverStart.Text = Now.ToString("yyyy/MM/dd")
                XtraTabPage2.PageVisible = False
                Me.Text = "�e�f-�s�W"
            Case "Modify"
                LoadModifyData()
                pnlCheck.Enabled = False
                XtraTabPage2.PageEnabled = False
                gluCuster.Enabled = False
                Me.Text = "�e�f-�ק�"
            Case "Check"
                LoadModifyData()
                GroupBox1.Enabled = False
                popOrder.Enabled = False
                dgvPositiveDeliver.OptionsBehavior.Editable = False

                lblCheckDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                Me.Text = "�e�f-�f��"
                XtraTabControl1.SelectedTabPage = XtraTabPage2
            Case "View"
                LoadModifyData()
                GroupBox1.Enabled = False
                popOrder.Enabled = False
                cmdSave.Enabled = False
                pnlCheck.Enabled = False
                dgvPositiveDeliver.OptionsBehavior.Editable = False
                Me.Text = "�e�f-�d��"
        End Select

    End Sub
#End Region

#Region "�[���ק�ƾ�"
    Private Function LoadModifyData() As Boolean
        LoadModifyData = False

        txtD_NO.Text = m_DOMID

        m_DsListInfo.Clear()

        'm_DsListInfo = m_DoCtrl.PositiveDeliver_GetList(txtD_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing)

        If m_DsListInfo Is Nothing Then
            Exit Function
        End If

        If m_DsListInfo.Count <= 0 Then
            Exit Function
        End If

        ''�[���f�ּƾ�
        gluCuster.EditValue = m_DsListInfo(0).P_OM_CusterNo
        txtD_AddAction.Text = m_DsListInfo(0).ActionName
        dtPositiveDeliverStart.Text = m_DsListInfo(0).D_AddDate
        txtD_OMRemark.Text = m_DsListInfo(0).D_OMCheckRemark
        chkCheck.Checked = m_DsListInfo(0).D_Check

        If m_DsListInfo(0).CheckActionName = String.Empty Then
            lblCheckAction.Text = UserName
        Else
            lblCheckAction.Text = m_DsListInfo(0).CheckActionName
        End If
       
        lblCheckDateTime.Text = m_DsListInfo(0).D_CheckDate
        txtCheckRemark.Text = m_DsListInfo(0).D_CheckRemark




        Dim nIndex As Integer
        For nIndex = 0 To m_DsListInfo.Count - 1
            Dim row As DataRow
            row = m_DataSet.Tables("PositiveDeliver").NewRow

            row("AutoID") = m_DsListInfo(nIndex).AutoID

            row("P_OM_CusterNo") = m_DsListInfo(nIndex).P_OM_CusterNo

            row("P_OM_CusterPO") = m_DsListInfo(nIndex).P_OM_CusterPO

            row("P_OM_Num") = m_DsListInfo(nIndex).P_OM_Num

            row("P_M_Code") = m_DsListInfo(nIndex).P_M_Code

            row("PartNumber") = m_DsListInfo(nIndex).PartNumber

            row("Qty") = m_DsListInfo(nIndex).Qty

            row("OrderQty") = m_DsListInfo(nIndex).OrderQty

            row("NoSendQty") = m_DsListInfo(nIndex).NoSendQty

            row("D_TypeID") = m_DsListInfo(nIndex).D_TypeID

            row("D_Check") = m_DsListInfo(nIndex).D_Check

            row("D_Remark") = m_DsListInfo(nIndex).D_Remark

            m_DataSet.Tables("PositiveDeliver").Rows.Add(row)
        Next

        LoadModifyData = True
    End Function
#End Region

#Region "��������{��"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
#End Region

#Region "�O�s����ƾ�"
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case m_EditItemDescrip
            Case "Add"
                If CheckData() Then
                    SaveData("Add", "�s�W")
                End If
            Case "Modify"
                If CheckData() Then
                    SaveData("Modify", "�ק�")
                End If
            Case "Check"
                If CheckUpdateData() Then
                    UpdateCheck()
                End If
        End Select
    End Sub

    Private Function CheckUpdateData() As Boolean
        CheckUpdateData = False

        If m_BolFalg = chkCheck.Checked Then
            MsgBox("�Ч��ܼf�֪��A�I")
            Exit Function
        End If


        m_DsListInfo.Clear()

        'm_DsListInfo = m_DoCtrl.PositiveDeliver_GetList(txtD_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing)

        If chkCheck.Checked Then
            If m_DsListInfo(0).D_Check Then
                MsgBox("�ӱi��w�g�f��,�Э��s�T�{���A!", 64, "����")
                Exit Function
            End If
        Else
            If Not m_DsListInfo(0).D_Check Then
                MsgBox("�ӱi��w�g�h��,�Э��s�T�{���A!", 64, "����")
                Exit Function
            End If
        End If

        'm_DsListInfo.Clear()

        'm_DsListInfo = m_DoCtrl.PositiveDeliver_GetList(txtD_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing)

        If m_DsListInfo Is Nothing Then
            MsgBox("�ӱi�e�f��w�g���s�b", 64, "����")
            Exit Function
        End If

        If m_DsListInfo.Count <= 0 Then
            MsgBox("�ӱi�e�f��w�g���s�b", 64, "����")
            Exit Function
        End If

        Dim nIndexI As Integer
        Dim nIndexJ As Integer


        Try
            If m_DsListInfo.Count <> m_DataSet.Tables("PositiveDeliver").Rows.Count Then
                MsgBox("�ӱi�e�f��w�o���ܤ�,�Э��s���J", 64, "����")
                Exit Function
            End If

            For nIndexI = 0 To m_DataSet.Tables("PositiveDeliver").Rows.Count - 1

                Dim bFlag As Boolean = False

                For nIndexJ = 0 To m_DsListInfo.Count - 1
                    If m_DataSet.Tables("PositiveDeliver").Rows(nIndexI)("AutoID").ToString() = m_DsListInfo(nIndexJ).AutoID Then
                        bFlag = True
                        Exit For
                    End If
                Next

                If Not bFlag Then
                    MsgBox("�ӱi�e�f��w�o���ܤ�,�Э��s���J", 64, "����")
                    Exit Function
                End If
            Next

            For nIndexI = 0 To m_DataSet.Tables("PositiveDeliver").Rows.Count - 1
                For nIndexJ = 0 To m_DsListInfo.Count - 1
                    If m_DataSet.Tables("PositiveDeliver").Rows(nIndexI)("AutoID").ToString() = m_DsListInfo(nIndexJ).AutoID Then
                        If CInt(m_DsListInfo(nIndexJ).Qty) <> CInt(m_DataSet.Tables("PositiveDeliver").Rows(nIndexI)("Qty").ToString()) Then
                            MsgBox("�ӱi�e�f��w�o���ܤ�,�Э��s���J", 64, "����")
                            Exit Function
                        End If
                    End If
                Next

            Next

        Catch ex As Exception
            MsgBox(ex.Message.ToString(), 64, "����")
            Exit Function
        End Try

        CheckUpdateData = True
    End Function
    ''' <summary>
    ''' �f��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateCheck()

       

        Dim m_DsInfo As New PositiveDeliverInfo

        m_DsInfo.D_NO = txtD_NO.Text

        m_DsInfo.D_Check = chkCheck.Checked

        If chkCheck.Checked Then

            m_DsInfo.D_CheckAction = InUserID

            m_DsInfo.D_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

            m_DsInfo.D_CheckRemark = txtCheckRemark.Text
        Else

            m_DsInfo.D_CheckAction = String.Empty

            m_DsInfo.D_CheckDate = Nothing

            m_DsInfo.D_CheckRemark = String.Empty
        End If
        




        If m_DoCtrl.PositiveDeliver_Check(m_DsInfo) Then
            If UpdateNoSendQty() Then
                MsgBox("�f�֪��A�w����!", 64, "����")
            End If

        Else
            MsgBox("�f�֥���,���ˬd��]!", 64, "����")
        End If

        Me.Close()
    End Sub

    ''' <summary>
    ''' ����ƶq
    ''' </summary>
    ''' <remarks></remarks>
    Private Function UpdateNoSendQty() As Boolean

        UpdateNoSendQty = False

        m_PsListInfo.Clear()

        m_DsListInfo.Clear()

        Dim dNoSendQty As Integer

        Dim nIndex As Integer

        Try
            For nIndex = 0 To m_DataSet.Tables("PositiveDeliver").Rows.Count - 1
                '   m_PsListInfo = m_PoCtrl.PositiveOrders_GetList(Nothing, m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("P_OM_Num").ToString(), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                If chkCheck.Checked Then
                 
                    'If CDbl(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("Qty").ToString()) > CDbl(m_PsListInfo(0).NoSendQty) Then
                    '    MsgBox("�e�f�ƶq����j�󥼥�ƶq,�Э��s�[���ƾ�!", 64, "����")
                    '    Exit Function
                    'End If
                    'dNoSendQty = CInt(m_PsListInfo(0).NoSendQty) - CInt(m_DsListInfo(0).Qty)
                    dNoSendQty = CInt(m_PsListInfo(0).NoSendQty) - CInt(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("Qty"))
                Else
                    'dNoSendQty = CInt(m_PsListInfo(0).NoSendQty) - CInt(m_DsListInfo(0).Qty)
                    dNoSendQty = CInt(m_PsListInfo(0).NoSendQty) + CInt(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("Qty"))
                End If

                m_PoCtrl.PositiveOrders_UpdateNoSendQty(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("P_OM_Num"), dNoSendQty)
            Next
        Catch ex As Exception
            MsgBox(ex.Message.ToString(), 64, "����")
            Exit Function
        End Try

        UpdateNoSendQty = True
    End Function

    ''' <summary>
    ''' �O�s�ƾ�
    ''' </summary>
    ''' <param name="strTemp"></param>
    ''' <param name="strDescreption"></param>
    ''' <remarks></remarks>
    Private Sub SaveData(ByVal strTemp As String, ByVal strDescreption As String)
        Dim m_DsInfo As New PositiveDeliverInfo

        Select Case strTemp
            Case "Add"
                m_DsInfo.D_NO = GetDOI_No()
            Case "Modify"
                m_DsInfo.D_NO = txtD_NO.Text
        End Select

        m_DsInfo.D_AddAction = InUserID

        m_DsInfo.D_OMCheckRemark = txtD_OMRemark.Text

        m_DsInfo.D_AddDate = Format(CDate(dtPositiveDeliverStart.Text), "yyyy/MM/dd HH:mm:ss")

        m_DsInfo.D_OMCheckRemark = txtD_OMRemark.Text

        Dim nIndex As Integer

        For nIndex = 0 To m_DataSet.Tables("PositiveDeliver").Rows.Count - 1



            m_DsInfo.P_OM_Num = m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("P_OM_Num")

            m_DsInfo.D_TypeID = m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("D_TypeID")

            m_DsInfo.PartNumber = m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("PartNumber")

            m_DsInfo.Qty = m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("Qty")

            m_DsInfo.NoSendQty = m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("NoSendQty")


            If IsDBNull(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("D_Remark")) Then
                m_DsInfo.D_Remark = String.Empty
            Else
                m_DsInfo.D_Remark = m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("D_Remark")
            End If



            Select Case strTemp
                Case "Add"

                    If Not (m_DoCtrl.PositiveDeliver_Add(m_DsInfo)) Then
                        MsgBox("����" & strDescreption & "�����e�f�q��!", 64, "����")
                        Exit Sub
                    End If
                Case "Modify"

                   
                    If m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("AutoID").ToString() = String.Empty Then

                        If Not (m_DoCtrl.PositiveDeliver_Add(m_DsInfo)) Then
                            MsgBox("����" & strDescreption & "�����e�f�q��!", 64, "����")
                            Exit Sub
                        End If
                    Else

                        m_DsInfo.AutoID = m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("AutoID")

                        m_DsInfo.D_OM_Num = m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("AutoID")
                        If Not (m_DoCtrl.PositiveDeliver_Update(m_DsInfo)) Then
                            MsgBox("����" & strDescreption & "�����e�f�q��!", 64, "����")
                            Exit Sub
                        End If
                    End If

            End Select


        Next

        If m_DataSet.Tables("DelData").Rows.Count > 0 Then
            For i As Integer = 0 To m_DataSet.Tables("DelData").Rows.Count - 1
                m_DoCtrl.PositiveDeliver_Delete(m_DataSet.Tables("DelData").Rows(i)("AutoID"))
            Next i
        End If

        MsgBox("����" & strDescreption & "�����e�f�q��!", 64, "����")
        Me.Close()

    End Sub
#End Region

#Region "�ˬd�ƾ�"
    Private Function CheckData() As Boolean
        CheckData = False


        If m_EditItemDescrip = "Modify" Then
            m_DsListInfo.Clear()

            'm_DsListInfo = m_DoCtrl.PositiveDeliver_GetList(txtD_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing)

            If m_DsListInfo Is Nothing Then
                MsgBox("�Ӱe�f��w�g���s�b", 64, "����")
                Exit Function
            End If

            If m_DsListInfo.Count <= 0 Then
                MsgBox("�Ӱe�f��w�g���s�b", 64, "����")
                Exit Function
            End If

            If m_DsListInfo(0).D_Check Then
                MsgBox("�Ӱe�f��w�g�f�֤����\�ק�", 64, "����")
                Exit Function
            End If

        End If

        If m_DataSet.Tables("PositiveDeliver").Rows.Count <= 0 Then
            MsgBox("�e�f���~���ର��!", 64, "����")
            Exit Function
        End If

        If gluCuster.EditValue = "" Or Len(gluCuster.EditValue) > 50 Then
            MsgBox("�Ȥ�N�����ର�ũΪ̶W�X�F����!", 64, "����")
            gluCuster.Focus()
            Exit Function
        End If

        If txtD_AddAction.Text = "" Or Len(txtD_AddAction.Text) > 20 Then
            MsgBox("�e�f�H���ର�ũΪ̶W�X�F����!", 64, "����")
            txtD_AddAction.Focus()
            Exit Function
        End If


        Try
            Dim nIndex As Integer


            For nIndex = 0 To m_DataSet.Tables("PositiveDeliver").Rows.Count - 1

                If IsDBNull(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("Qty")) Then
                    MsgBox("�e�f�ƶq���ର��!", 64, "����")
                    Exit Function
                End If

                If IsDBNull(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("OrderQty")) Then
                    MsgBox("�q��ƶq���ର��!", 64, "����")
                    Exit Function
                End If

                If IsDBNull(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("NoSendQty")) Then
                    MsgBox("����ƶq���ର��!", 64, "����")
                    Exit Function
                End If

                If Not CheckQty(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("Qty").ToString(), "�e�f�ƶq") Then
                    Exit Function
                End If

                If Not CheckQty(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("OrderQty").ToString(), "�q��ƶq") Then
                    Exit Function
                End If

                If Not CheckQty(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("NoSendQty").ToString(), "����ƶq") Then
                    Exit Function
                End If


                If m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("D_TypeID").ToString() = "" Or Len(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("D_TypeID").ToString()) > 10 Then
                    MsgBox("�e�f�������ର�ũΪ̶W�X�F����!", 64, "����")
                    Exit Function
                End If



                m_PsListInfo.Clear()

                'm_PsListInfo = m_PoCtrl.PositiveOrders_GetList(Nothing, m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("P_OM_Num").ToString(), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)


                If m_PsListInfo.Count <= 0 Then
                    MsgBox("�ӱi�q�檺���~�w�g���s�b,�Э��s�[��!", 64, "����")
                    Exit Function
                End If

                If CDbl(m_DataSet.Tables("PositiveDeliver").Rows(nIndex)("Qty").ToString()) > CDbl(m_PsListInfo(0).NoSendQty) Then
                    MsgBox("�e�f�ƶq����j�󥼥�ƶq!", 64, "����")
                    Exit Function
                End If

            Next

            Dim nIndex_I As Integer
            Dim nIndex_J As Integer

            If m_DataSet.Tables("PositiveDeliver").Rows.Count > 0 Then
                '�_�w�d���k
                For nIndex_I = 0 To m_DataSet.Tables("PositiveDeliver").Rows.Count - 1
                    For nIndex_J = 1 + nIndex_I To m_DataSet.Tables("PositiveDeliver").Rows.Count - 1
                        If m_DataSet.Tables("PositiveDeliver").Rows(nIndex_J)("P_OM_CusterNo").ToString() <> m_DataSet.Tables("PositiveDeliver").Rows(nIndex_I)("P_OM_CusterNo").ToString() Then
                            MsgBox("�e�f�楲�����P�@�ӫȤ�!", 64, "����")
                            Exit Function
                        End If
                    Next
                Next
            End If

            If m_DataSet.Tables("PositiveDeliver").Rows.Count > 0 Then
                '�_�w�d���k
                For nIndex_I = 0 To m_DataSet.Tables("PositiveDeliver").Rows.Count - 1
                    For nIndex_J = 1 + nIndex_I To m_DataSet.Tables("PositiveDeliver").Rows.Count - 1
                        If m_DataSet.Tables("PositiveDeliver").Rows(nIndex_J)("P_OM_Num").ToString() = m_DataSet.Tables("PositiveDeliver").Rows(nIndex_I)("P_OM_Num").ToString() Then
                            If m_DataSet.Tables("PositiveDeliver").Rows(nIndex_J)("D_TypeID").ToString() = m_DataSet.Tables("PositiveDeliver").Rows(nIndex_I)("D_TypeID").ToString() Then
                                MsgBox("�P�@�i�q��y��������X�{�ۦP���e�f����!", 64, "����")
                                Exit Function
                            End If
                        End If
                    Next
                Next
            End If





            For nIndex_I = 0 To m_DataSet.Tables("PositiveDeliver").Rows.Count - 1

                m_PsListInfo.Clear()

                'm_PsListInfo = m_PoCtrl.PositiveOrders_GetList(Nothing, m_DataSet.Tables("PositiveDeliver").Rows(nIndex_I)("P_OM_Num").ToString(), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                Dim nSumNoSendQty As Integer

                nSumNoSendQty = 0

                nSumNoSendQty = nSumNoSendQty + CInt(m_DataSet.Tables("PositiveDeliver").Rows(nIndex_I)("Qty").ToString())

                For nIndex_J = 1 + nIndex_I To m_DataSet.Tables("PositiveDeliver").Rows.Count - 1
                    If m_DataSet.Tables("PositiveDeliver").Rows(nIndex_J)("P_OM_Num").ToString() = m_DataSet.Tables("PositiveDeliver").Rows(nIndex_I)("P_OM_Num").ToString() Then
                        nSumNoSendQty = nSumNoSendQty + CInt(m_DataSet.Tables("PositiveDeliver").Rows(nIndex_J)("Qty").ToString())
                    End If
                Next

                If nSumNoSendQty > CInt(m_PsListInfo(0).NoSendQty) Then
                    MsgBox("�e�f�`�ƶq����j�󥼥�ƶq!", 64, "����")
                    Exit Function
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString(), 64, "����")
            Exit Function
        End Try

        

        CheckData = True
    End Function

    ''' <summary>
    ''' �֬d�ƾ�
    ''' </summary>
    ''' <param name="strParam"></param>
    ''' <param name="strName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckQty(ByVal strParam As String, ByVal strName As String) As Boolean

        CheckQty = False

        Try
            If strParam <> String.Empty Then
                If CDbl(strParam) <= 0 Then
                    MsgBox(strName & "����p�󵥩�s!", 64, "����")
                    Exit Function
                End If
            Else
                MsgBox(strName & "���ର��!", 64, "����")
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(strName & "�W�X�d��!", 64, "����")
            Exit Function
        End Try

        CheckQty = True

    End Function
#End Region

   

End Class