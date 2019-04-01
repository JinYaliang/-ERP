Imports LFERP.DataSetting
Imports LFERP.Library.Positive
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Product

Public Class frmPositiveOrders

#Region "�w�s���Ѽ�"

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


    Private m_POMID As String                       '�q��s��

    Public Property POMID() As String
        Get
            Return m_POMID
        End Get
        Set(ByVal value As String)
            m_POMID = value
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

#Region "��������{��"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
#End Region

#Region "�ͦ��s�q�渹"
    ''' <summary>
    ''' �ͦ��s�q�渹
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPOI_No() As String

        Dim m_PsInfo As New PositiveOrdersInfo

        m_PsInfo = m_PoCtrl.PositiveOrders_GetNO("POI" + Now.ToString("yyMM"), Nothing)

        If m_PsInfo Is Nothing Then
            Return "POI" & Now.ToString("yy") & Now.ToString("MM") & "0001"
        Else
            Return "POI" & Now.ToString("yy") & Now.ToString("MM") & Mid((CInt(Mid(m_PsInfo.P_OM_ID, 6)) + 10001), 2)
        End If

    End Function
#End Region

#Region "�ͦ��q��y����"
    ''' <summary>
    ''' �ͦ��q��y����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PON_Num() As String

        Dim m_PsInfo As New PositiveOrdersInfo

        m_PsInfo = m_PoCtrl.PositiveOrders_GetNO(Nothing, "PON" + Now.ToString("yyMM"))

        If m_PsInfo Is Nothing Then
            Return "PON" & Now.ToString("yy") & Now.ToString("MM") & "0001"
        Else
            Return "PON" & Now.ToString("yy") & Now.ToString("MM") & Mid((CInt(Mid(m_PsInfo.P_OM_Num, 6)) + 10001), 2)
        End If

    End Function

#End Region

#Region "�Ы��{�ɪ�"
    Private Sub CreateTables()

        m_DataSet.Tables.Clear()
        With m_DataSet.Tables.Add("PositiveOrders")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("P_OM_Num", GetType(String))
            .Columns.Add("P_OM_ID", GetType(String))
            .Columns.Add("P_OM_CusterPO", GetType(String))
            .Columns.Add("P_OM_CusterNo", GetType(String))
            .Columns.Add("P_M_Code", GetType(String))
            .Columns.Add("PartNumber", GetType(String))
            .Columns.Add("Qty", GetType(Integer))
            .Columns.Add("NoSendQty", GetType(String))
            .Columns.Add("P_OM_AddDate", GetType(String))
            .Columns.Add("P_OM_AddAction", GetType(String))
            .Columns.Add("P_OM_Check", GetType(String))
            .Columns.Add("P_OM_CheckAction", GetType(String))
            .Columns.Add("P_OM_CheckDate", GetType(String))
            .Columns.Add("P_SalesPrice", GetType(String))
            .Columns.Add("P_ProductPrice", GetType(String))
            .Columns.Add("P_Remark", GetType(String))
            .Columns.Add("P_CheckRemark", GetType(String))
        End With
        dgPositiveOrders.DataSource = m_DataSet.Tables("PositiveOrders")

        '�ЫاR���ƾڪ�
        With m_DataSet.Tables.Add("DelData")
            .Columns.Add("P_OM_Num", GetType(String))
        End With

    End Sub
#End Region

#Region "�[���ק�ƾ�"
    Private Function LoadModifyData() As Boolean
        LoadModifyData = False

        txtP_OM_ID.Text = m_POMID

        m_PsListInfo.Clear()

        '  m_PsListInfo = m_PoCtrl.PositiveOrders_GetList(txtP_OM_ID.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If m_PsListInfo Is Nothing Then
            Exit Function
        End If

        If m_PsListInfo.Count <= 0 Then
            Exit Function
        End If


        gluCuster.EditValue = m_PsListInfo(0).P_OM_CusterNo
        txtP_OM_AddAction.Text = m_PsListInfo(0).ActionName
        txtP_OM_CusterPO.Text = m_PsListInfo(0).P_OM_CusterPO
        dtOrdersStart.Text = m_PsListInfo(0).P_OM_AddDate
        txtP_OMRemark.Text = m_PsListInfo(0).P_OMRemark
        dtOrderComplate.Text = m_PsListInfo(0).P_OMComplateDate
        '�[���f�ּƾ�

        chkCheck.Checked = m_PsListInfo(0).P_OM_Check
        lblCheckDateTime.Text = m_PsListInfo(0).P_OM_CheckDate

        If m_PsListInfo(0).CheckActionName = String.Empty Then
            lblCheckAction.Text = UserName
        Else
            lblCheckAction.Text = m_PsListInfo(0).CheckActionName
        End If

        txtCheckRemark.Text = m_PsListInfo(0).P_CheckRemark


        Dim nIndex As Integer
        For nIndex = 0 To m_PsListInfo.Count - 1
            Dim row As DataRow
            row = m_DataSet.Tables("PositiveOrders").NewRow

            row("P_OM_Num") = m_PsListInfo(nIndex).P_OM_Num

            row("P_M_Code") = m_PsListInfo(nIndex).P_M_Code

            row("PartNumber") = m_PsListInfo(nIndex).PartNumber

            row("Qty") = m_PsListInfo(nIndex).Qty

            row("NoSendQty") = m_PsListInfo(nIndex).NoSendQty

            row("P_SalesPrice") = m_PsListInfo(nIndex).P_SalesPrice

            row("P_ProductPrice") = m_PsListInfo(nIndex).P_ProductPrice

            row("P_Remark") = m_PsListInfo(nIndex).P_Remark

            m_DataSet.Tables("PositiveOrders").Rows.Add(row)
        Next

        LoadModifyData = True
    End Function
#End Region

#Region "��l�ƥ[���ƾ�"
    Private Sub frmPositiveOrders_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CreateTables()
        LoadgluCuster()

        r_PM_M_Code.VisibleIndex = 0
        r_PM_M_Code.Width = 100


        Dim pcMainCtrl As New ProductController
        rGrid.DisplayMember = "PM_M_Code"
        rGrid.ValueMember = "PM_M_Code"
        rGrid.DataSource = pcMainCtrl.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)


        Select Case m_EditItemDescrip
            Case "Add"
                txtP_OM_ID.Text = GetPOI_No()
                txtP_OM_AddAction.Text = UserName
                dtOrdersStart.Text = Now.ToString("yyyy/MM/dd")
                dtOrderComplate.Text = Now.ToString("yyyy/MM/dd")
                XtraTabPage2.PageVisible = False
                Me.Text = "�q��-�s�W"
            Case "Modify"
                LoadModifyData()
                pnlCheck.Enabled = False
                XtraTabPage2.PageEnabled = False
                Me.Text = "�q��-�ק�"
            Case "Check"
                LoadModifyData()
                GroupBox1.Enabled = False
                popOrder.Enabled = False
                dgvPositiveOrders.OptionsBehavior.Editable = False

                lblCheckDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                Me.Text = "�q��-�f��"
                XtraTabControl1.SelectedTabPage = XtraTabPage2
            Case "View"
                LoadModifyData()
                GroupBox1.Enabled = False
                popOrder.Enabled = False
                cmdSave.Enabled = False
                pnlCheck.Enabled = False
                dgvPositiveOrders.OptionsBehavior.Editable = False
                dgvPositiveOrders.Columns("NoSendQty").Visible = True
                dgvPositiveOrders.Columns("NoSendQty").VisibleIndex = 3
                Me.Text = "�q��-�d��"
            Case "Price"
                LoadModifyData()
                pnlCheck.Enabled = False
                XtraTabPage2.PageEnabled = False
                InvalidPrice()
                Me.Text = "�q��-�ק���"
        End Select
    End Sub

    Private Sub InvalidPrice()
        dgvPositiveOrders.Columns("P_SalesPrice").Visible = True
        dgvPositiveOrders.Columns("P_SalesPrice").VisibleIndex = 3
        dgvPositiveOrders.Columns("P_ProductPrice").Visible = True
        dgvPositiveOrders.Columns("P_ProductPrice").VisibleIndex = 4
        GroupBox1.Enabled = False
        popOrder.Enabled = False
        pnlCheck.Enabled = False


        dgvPositiveOrders.Columns("P_M_Code").OptionsColumn.AllowEdit = False
        dgvPositiveOrders.Columns("PartNumber").OptionsColumn.AllowEdit = False
        dgvPositiveOrders.Columns("Qty").OptionsColumn.AllowEdit = False
        dgvPositiveOrders.Columns("P_Remark").OptionsColumn.AllowEdit = False

        popOrder.Enabled = True
        popPositiveOrdersAdd.Enabled = False
    End Sub

#End Region

#Region "�[���Ȥ�N��"
    Private Sub LoadgluCuster()
        Dim cusCtrl As New CusterControler

        gluCuster.Properties.DisplayMember = "C_CusterID"
        gluCuster.Properties.ValueMember = "C_CusterID"
        gluCuster.Properties.DataSource = cusCtrl.GetCusterList(Nothing, Nothing, Nothing)

    End Sub
   
#End Region

#Region "�ˬd�ƾ�"
    Private Function CheckData() As Boolean
        CheckData = False

        If m_DataSet.Tables("PositiveOrders").Rows.Count <= 0 Then
            MsgBox("�q�沣�~���ର��!", 64, "����")
            Exit Function
        End If

        If m_EditItemDescrip = "Modify" Then
            Dim nIndexModify As Integer

            m_DsListInfo.Clear()

            m_PsListInfo.Clear()


            '  m_PsListInfo = m_PoCtrl.PositiveOrders_GetList(txtP_OM_ID.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If m_PsListInfo Is Nothing Then
                MsgBox("�ӭq��w�g���s�b", 64, "����")
                Exit Function
            End If

            If m_PsListInfo.Count <= 0 Then
                MsgBox("�ӭq��w�g���s�b", 64, "����")
                Exit Function
            End If

            If m_PsListInfo(0).P_OM_Check Then
                MsgBox("�ӭq��w�g�f�֤����\�ק�", 64, "����")
                Exit Function
            End If

            For nIndexModify = 0 To m_DataSet.Tables("PositiveOrders").Rows.Count - 1

                If m_DataSet.Tables("PositiveOrders").Rows(nIndexModify)("P_OM_Num").ToString() = String.Empty Then
                    Continue For
                End If

                ' m_DsListInfo = m_DoCtrl.PositiveDeliver_GetList(Nothing, m_DataSet.Tables("PositiveOrders").Rows(nIndexModify)("P_OM_Num"), Nothing, Nothing, Nothing, Nothing)

                If m_DsListInfo.Count > 0 Then
                    MsgBox("�ӭq��w���e�f�O�������\�ק�!", 64, "����")
                    Exit Function
                End If
            Next
        End If


        Dim nIndex_I As Integer
        Dim nIndex_J As Integer

        If m_DataSet.Tables("PositiveOrders").Rows.Count > 1 Then
            '�_�w�d���k
            For nIndex_I = 0 To m_DataSet.Tables("PositiveOrders").Rows.Count - 1
                For nIndex_J = 1 + nIndex_I To m_DataSet.Tables("PositiveOrders").Rows.Count - 1
                    If m_DataSet.Tables("PositiveOrders").Rows(nIndex_J)("P_M_Code").ToString() = m_DataSet.Tables("PositiveOrders").Rows(nIndex_I)("P_M_Code").ToString() Then
                        If m_DataSet.Tables("PositiveOrders").Rows(nIndex_J)("PartNumber").ToString() = m_DataSet.Tables("PositiveOrders").Rows(nIndex_I)("PartNumber").ToString() Then
                            MsgBox("�P�@�i�椣��X�{�ۦP�����~!", 64, "����")
                            Exit Function
                        End If
                    End If
                Next
            Next
        End If



        If gluCuster.EditValue = "" Or Len(gluCuster.EditValue) > 50 Then
            MsgBox("�Ȥ�N�����ର�ũΪ̶W�X�F����!", 64, "����")
            gluCuster.Focus()
            Exit Function
        End If

        If txtP_OM_AddAction.Text = "" Or Len(txtP_OM_AddAction.Text) > 20 Then
            MsgBox("�u�����ର�ũΪ̶W�X�F����!", 64, "����")
            txtP_OM_AddAction.Focus()
            Exit Function
        End If

        If txtP_OM_CusterPO.Text = "" Or Len(txtP_OM_CusterPO.Text) > 50 Then
            MsgBox("�Ȥ�PO���ର�ũΪ̶W�X�F����!", 64, "����")
            txtP_OM_CusterPO.Focus()
            Exit Function
        End If

        If CDate(dtOrdersStart.Text) > CDate(dtOrderComplate.Text) Then
            MsgBox("�U��������j���f���!", 64, "����")
            Exit Function
        End If

        Try
            Dim nIndex As Integer
            For nIndex = 0 To m_DataSet.Tables("PositiveOrders").Rows.Count - 1


                If IsDBNull(m_DataSet.Tables("PositiveOrders").Rows(nIndex)("Qty")) Then
                    MsgBox("�q��ƶq���ର��!", 64, "����")
                    Exit Function
                End If



                If IsDBNull(m_DataSet.Tables("PositiveOrders").Rows(nIndex)("PartNumber")) Then
                    MsgBox("�Ƹ����ର��!", 64, "����")
                    Exit Function
                End If

                If Not CheckQty(m_DataSet.Tables("PositiveOrders").Rows(nIndex)("Qty").ToString(), "�q��ƶq") Then
                    Exit Function
                End If

                If m_EditItemDescrip = "Price" Then

                    If IsDBNull(m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_SalesPrice")) Then
                        MsgBox("�P�������ର��!", 64, "����")
                        Exit Function
                    End If

                    If IsDBNull(m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_ProductPrice")) Then
                        MsgBox("���������ର��!", 64, "����")
                        Exit Function
                    End If

                    If Not CheckQty(m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_SalesPrice").ToString(), "�P����") Then
                        Exit Function
                    End If

                    If Not CheckQty(m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_ProductPrice").ToString(), "������") Then
                        Exit Function
                    End If
                End If


                If m_DataSet.Tables("PositiveOrders").Rows(nIndex)("PartNumber").ToString() = "" Or Len(m_DataSet.Tables("PositiveOrders").Rows(nIndex)("PartNumber").ToString()) > 50 Then
                    MsgBox("�Ƹ����ର�ũΪ̶W�X�F����!", 64, "����")
                    Exit Function
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.ToString(), 64, "����")
            Exit Function
        End Try

        CheckData = True
    End Function

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

#Region "�O�s�ƾ�"
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
                UpdateCheck()
            Case "Price"
                If CheckData() Then
                    SavePrice()
                End If
        End Select
    End Sub

    ''' <summary>
    ''' �O�s���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SavePrice()
        Dim m_PsInfo As New PositiveOrdersInfo

        m_PsInfo.P_OM_ID = txtP_OM_ID.Text

        Dim nIndex As Integer

        For nIndex = 0 To m_DataSet.Tables("PositiveOrders").Rows.Count - 1
            m_PsInfo.P_OM_Num = m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_OM_Num")

            m_PsInfo.P_SalesPrice = m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_SalesPrice")

            m_PsInfo.P_ProductPrice = m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_ProductPrice")

            If Not (m_PoCtrl.PositiveOrders_UpdatePrice(m_PsInfo)) Then
                MsgBox("�����q������異��!", 64, "����")
                Exit Sub
            End If
        Next


     

        MsgBox("����ק令�\", 64, "����")
        Me.Close()
    End Sub


    ''' <summary>
    ''' �f�ּƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateCheck()

        If m_BolFalg = chkCheck.Checked Then
            MsgBox("�Ч��ܼf�֪��A�I")
            Exit Sub
        End If

        Dim m_PsInfo As New PositiveOrdersInfo

        m_PsInfo.P_OM_ID = txtP_OM_ID.Text

        m_PsInfo.P_OM_Check = chkCheck.Checked

        If chkCheck.Checked Then
            m_PsInfo.P_OM_CheckAction = InUserID

            m_PsInfo.P_OM_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

            m_PsInfo.P_Remark = txtCheckRemark.Text
        Else
            m_PsInfo.P_OM_CheckAction = String.Empty

            m_PsInfo.P_OM_CheckDate = Nothing

            m_PsInfo.P_Remark = String.Empty

        End If
       

        If m_PoCtrl.PositiveOrders_Check(m_PsInfo) Then
            MsgBox("�f�֪��A�w����!", 64, "����")
        Else
            MsgBox("�f�֥���,���ˬd��]!", 64, "����")
        End If

        Me.Close()
    End Sub

    ''' <summary>
    ''' �O�s�ƾ�
    ''' </summary>
    ''' <param name="strTemp"></param>
    ''' <remarks></remarks>
    Private Sub SaveData(ByVal strTemp As String, ByVal strDescreption As String)
        Dim m_PsInfo As New PositiveOrdersInfo

        Select Case strTemp
            Case "Add"
                m_PsInfo.P_OM_ID = GetPOI_No()
            Case "Modify"
                m_PsInfo.P_OM_ID = txtP_OM_ID.Text
        End Select

        m_PsInfo.P_OM_CusterNo = gluCuster.EditValue

        m_PsInfo.P_OM_AddAction = InUserID

        m_PsInfo.P_OM_CusterPO = txtP_OM_CusterPO.Text

        m_PsInfo.P_OMRemark = txtP_OMRemark.Text

        m_PsInfo.P_OMComplateDate = dtOrderComplate.Text

        m_PsInfo.P_OM_AddDate = dtOrdersStart.Text

        Dim nIndex As Integer

        For nIndex = 0 To m_DataSet.Tables("PositiveOrders").Rows.Count - 1

            m_PsInfo.P_M_Code = m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_M_Code")

            m_PsInfo.PartNumber = m_DataSet.Tables("PositiveOrders").Rows(nIndex)("PartNumber")

            m_PsInfo.Qty = m_DataSet.Tables("PositiveOrders").Rows(nIndex)("Qty")

            m_PsInfo.NoSendQty = m_DataSet.Tables("PositiveOrders").Rows(nIndex)("Qty")

            If IsDBNull(m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_Remark")) Then
                m_PsInfo.P_Remark = String.Empty
            Else
                m_PsInfo.P_Remark = m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_Remark")
            End If



            Select Case strTemp
                Case "Add"
                    m_PsInfo.P_OM_Num = PON_Num()
                    If Not (m_PoCtrl.PositiveOrders_Add(m_PsInfo)) Then
                        MsgBox("����" & strDescreption & "�����q��!", 64, "����")
                        Exit Sub
                    End If
                Case "Modify"

                    If m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_OM_Num").ToString() = String.Empty Then

                        m_PsInfo.P_OM_Num = PON_Num()
                        If Not (m_PoCtrl.PositiveOrders_Add(m_PsInfo)) Then
                            MsgBox("����" & strDescreption & "�����q��!", 64, "����")
                            Exit Sub
                        End If
                    Else

                        m_PsInfo.P_OM_Num = m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_OM_Num")
                        If Not (m_PoCtrl.PositiveOrders_Update(m_PsInfo)) Then
                            MsgBox("����" & strDescreption & "�����q��!", 64, "����")
                            Exit Sub
                        End If
                    End If
            End Select


        Next

        If m_DataSet.Tables("DelData").Rows.Count > 0 Then
            For i As Integer = 0 To m_DataSet.Tables("DelData").Rows.Count - 1
                m_PoCtrl.PositiveOrders_DeleteByPONum(m_DataSet.Tables("DelData").Rows(i)("P_OM_Num"))
            Next i
        End If


        MsgBox("����" & strDescreption & "�����q��!", 64, "����")
        Me.Close()

    End Sub

#End Region

#Region "�k����ﶵ"
    ''' <summary>
    ''' �s�W
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popPositiveOrdersAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPositiveOrdersAdd.Click
        Dim row As DataRow = m_DataSet.Tables("PositiveOrders").NewRow()
        row("Qty") = 0
        row("NoSendQty") = 0
        row("P_SalesPrice") = 0
        row("P_ProductPrice") = 0
        m_DataSet.Tables("PositiveOrders").Rows.Add(row)
        dgvPositiveOrders.MoveLast()
    End Sub
    ''' <summary>
    ''' �R��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popPositiveOrdersDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPositiveOrdersDel.Click
        If dgvPositiveOrders.RowCount = 0 Then Exit Sub
        Dim delTemp As String
        Try
            delTemp = dgvPositiveOrders.GetRowCellDisplayText(ArrayToString(dgvPositiveOrders.GetSelectedRows()), "P_OM_Num")

            If delTemp = "P_OM_Num" Then
            Else
                '�b�R�����W�[�Q�R�����O��
                Dim row As DataRow = m_DataSet.Tables("DelData").NewRow
                row("P_OM_Num") = delTemp
                m_DataSet.Tables("DelData").Rows.Add(row)
            End If
            m_DataSet.Tables("PositiveOrders").Rows.RemoveAt(CInt(ArrayToString(dgvPositiveOrders.GetSelectedRows())))
        Catch ex As Exception

        End Try

    End Sub

#End Region



End Class