Imports System.Data.SqlClient
Imports EncodeMy

Module mblShareData
    ''' <summary>
    ''' �N�Ϥ��ഫ���G�i��y�A�i�s�JSQL��image�r�q
    ''' </summary>
    ''' <param name="Image">�Ϥ�,�pPictureBox.image</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ImageToByte(ByVal Image As System.Drawing.Image) As Byte()
        '�N�Ϥ��ഫ���G�i��
        Dim fs As IO.FileStream
        fs = Nothing
        If Image Is Nothing Then
            ImageToByte = Nothing
        Else
            If fs Is Nothing Then
                Dim output As New IO.MemoryStream
                Dim iImage As Drawing.Image = Nothing
                Dim mem As New IO.MemoryStream
                Dim myBitmap As New Bitmap(Image)
                myBitmap.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim Buf(CInt(mem.Length - 1)) As Byte

                mem.Position = 0
                mem.Read(Buf, 0, CInt(mem.Length))
                mem.Close()
                ImageToByte = Buf
            Else
                Dim photoData(fs.Length) As Byte
                fs.Read(photoData, 0, Int(fs.Length))
                fs.Close()
                ImageToByte = photoData
            End If

        End If
    End Function
    ''' <summary>
    ''' �NSQL��image�r�q�ഫ���Ϥ�
    ''' </summary>
    ''' <param name="ImageByte">�G�i��y</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ByteToImage(ByVal ImageByte As Byte()) As System.Drawing.Image
        '�N�G�i��Ϥ��ഫ���ϫ�
        If ImageByte Is Nothing Then
            ByteToImage = Nothing
        Else
            Dim stmphoto As New IO.MemoryStream(ImageByte)
            ByteToImage = Image.FromStream(stmphoto)
        End If
    End Function

    '�̿�ܪ����ثإ߹�����
    Public Sub SetSelectTypeGrid(ByVal M_Code As String, ByVal SelectType As String, ByVal GridView As System.Windows.Forms.DataGridView)
        If M_Code = Nothing Or M_Code = "" Then Exit Sub
        Select Case SelectType
            Case "���~��Ʀ@��"
                Dim mcProduct As New LFERP.Library.Product.ProductController
                GridView.DataSource = mcProduct.Product_GetListCodeShare(M_Code)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 15
                AddColumns("PM_M_Code", "���~�s��", "PM_M_Code", 80, GridView)
                AddColumns("PM_CusterID", "�Ȥ�N��", "PM_CusterID", 80, GridView)
                AddColumns("PM_CusterNO", "�Ȥ�s��", "PM_CusterNO", 80, GridView)
                AddColumns("PM_JiYu", strJIYU, "PM_JiYu", 60, GridView)
                AddColumns("PM_Rank", "����", "PM_Rank", 60, GridView)
                AddColumns("PM_AddDate", "�إߤ��", "PM_AddDate", 120, GridView)
                AddColumns("PM_EditDate", "�̦Z�ק���", "PM_EditDate", 120, GridView)
                AddColumns("PM_Action", "�ާ@��", "PM_Action", 80, GridView)
            Case "�妸�@��"
                Dim mcOrdersBom As New LFERP.Library.Orders.OrdersBomController
                GridView.DataSource = mcOrdersBom.OrdersBom_GetList(M_Code, Nothing, Nothing, Nothing)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 15
                AddColumns("OS_BatchID", "�妸�s��", "OS_BatchID", 100, GridView)
                AddColumns("PM_M_Code", "���~�s��", "PM_M_Code", 80, GridView)
                AddColumns("OS_AddDate", "�إߤ��", "OS_AddDate", 120, GridView)
                AddColumns("OS_EditDate", "�ק���", "OS_EditDate", 120, GridView)
                AddColumns("OS_Check", "�妸�f��", "OS_Check", 80, GridView)

            Case "������"
                Dim mcQuotation As New LFERP.Library.Purchase.Quotation.QuotationController
                GridView.DataSource = mcQuotation.Quotation_GetlistTop10(Nothing, M_Code, Nothing, True, True)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 15
                AddColumns("M_Code", "���ƽs�X", "M_Code", 120, GridView)
                AddColumns("Q_QuoID", "������", "Q_QuoID", 80, GridView)
                AddColumns("Q_SupplierName", "������", "Q_SupplierName", 80, GridView)
                AddColumns("Q_SupplierNo", "�����ӽs��", "Q_SupplierNo", 110, GridView)
                'AddColumns("Q_Price", "���", "Q_Price", 80, GridView)
                'AddColumns("Q_Currency", "���O", "Q_Currency", 80, GridView)
                AddColumns("Q_Depict", "�ƪ`", "Q_Depict", 80, GridView)
                AddColumns("Q_ActionName", "������", "Q_ActionName", 80, GridView)
                AddColumns("Q_Check", "�����f��", "Q_Check", 80, GridView)
                AddColumns("Q_AccCheck", "�|�p�f��", "Q_AccCheck", 80, GridView)


            Case "���ʳ�"
                Dim mcPurchase As New LFERP.Library.Purchase.Purchase.PurchaseMainController
                GridView.DataSource = mcPurchase.PurchaseMain_Getlist(Nothing, Nothing, Nothing, M_Code, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 1, True)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 15
                AddColumns("M_Code", "���ƽs�X", "M_Code", 120, GridView)
                AddColumns("PM_NO", "���ʳ渹", "PM_NO", 80, GridView)
                AddColumns("S_SupplierName", "������", "S_SupplierName", 80, GridView)
                AddColumns("S_SupplierNO", "�����ӽs��", "S_SupplierNO", 110, GridView)
                AddColumns("PS_Qty", "�ƶq", "PS_Qty", 80, GridView)
                'AddColumns("PS_Price", "���", "PS_Price", 80, GridView)
                AddColumns("PS_SendDate", "��f���", "PS_SendDate", 80, GridView)
                AddColumns("PM_ActionName", "���ʭ�", "PM_ActionName", 80, GridView)
                AddColumns("PM_Check", "�����f��", "PM_Check", 80, GridView)
                AddColumns("PM_AccountCheck", "�|�p�f��", "PM_AccountCheck", 80, GridView)

            Case "���ʳ�(���槹)"
                Dim mcPurchase As New LFERP.Library.Purchase.Purchase.PurchaseMainController
                GridView.DataSource = mcPurchase.PurchaseMain_Getlist(Nothing, Nothing, Nothing, M_Code, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, True)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 15
                AddColumns("M_Code", "���ƽs�X", "M_Code", 120, GridView)
                AddColumns("PM_NO", "���ʳ渹", "PM_NO", 80, GridView)
                AddColumns("S_SupplierName", "������", "S_SupplierName", 80, GridView)
                AddColumns("S_SupplierNO", "�����ӽs��", "S_SupplierNO", 110, GridView)
                AddColumns("PS_Qty", "�ƶq", "PS_Qty", 80, GridView)
                AddColumns("PS_NoSendQty", "���ʦ^�ƶq", "PS_NoSendQty", 110, GridView)
                AddColumns("PS_SendDate", "��f���", "PS_SendDate", 80, GridView)
                AddColumns("PM_ActionName", "���ʭ�", "PM_ActionName", 80, GridView)
                AddColumns("PM_Check", "�����f��", "PM_Check", 80, GridView)

            Case "�w�s"
                Dim mcInventory As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                Dim strCode As String
                strCode = "'" & M_Code & "'"
                GridView.DataSource = mcInventory.WareInventory_GetMaterial(Nothing, Nothing, strCode)

                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 15

                AddColumns("M_Code", "���ƽs�X", "M_Code", 120, GridView)
                AddColumns("WI_Qty", "�w�s�ƶq", "WI_Qty", 120, GridView)
                'AddColumns("WH_ID", "�ܮw�s��", "WH_ID", 120, GridView)
                AddColumns("WH_Name", "�D�ܮw�W��", "WH_Name", 120, GridView)
                AddColumns("WH_SName", "�l�ܮw�W��", "WH_SName", 120, GridView)
            Case "�e�f��"
                Dim acc As New LFERP.Library.Purchase.Acceptance.AcceptanceController
                GridView.DataSource = acc.Acceptance_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, M_Code, Nothing, Nothing, Nothing, Nothing, Nothing)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 15

                AddColumns("PM_NO", "���ʳ渹", "PM_NO", 120, GridView)
                AddColumns("A_AcceptanceNO", "�禬�渹", "A_AcceptanceNO", 120, GridView)
                AddColumns("A_Qty", "�禬�ƶq", "A_Qty", 120, GridView)
                AddColumns("A_SendNO", "�e�f�渹", "A_SendNO", 120, GridView)
            Case "���ƫ~����X"
                Dim wqc As New LFERP.Library.Purchase.WareQuality.WareQualityController
                GridView.DataSource = wqc.WareQuality_GetList(Nothing, M_Code, Nothing, Nothing, Nothing, Nothing, Nothing)

                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 15

                AddColumns("WQ_Code", "�渹�H��", "WQ_Code", 100, GridView)
                AddColumns("WQ_Description", "��q�y�z", "WQ_Description", 150, GridView)
                AddColumns("PS_Opinion", "�լd���G", "PS_Opinion", 150, GridView)
                AddColumns("ACC_Opinion", "�f�p�N��", "ACC_Opinion", 150, GridView)


        End Select


    End Sub
    Public Sub SetSelectBatchGrid(ByVal BatchID As String, ByVal M_Code As String, ByVal SelectType As String, ByVal GridView As System.Windows.Forms.DataGridView)
        If BatchID = "" Or M_Code = "" Then Exit Sub '------�Ҧ����d�߱���(���F�w�s)���o�K�[�@�妸�d�ߡI�I�I
        Select Case SelectType
            Case "���ʫH��"
                Dim mcPurchase As New LFERP.Library.Purchase.Purchase.PurchaseMainController
                GridView.DataSource = mcPurchase.PurchaseSub_GetBatchList(Nothing, M_Code, BatchID)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 5
                AddColumns("M_Code", "���ƽs�X", "M_Code", 120, GridView)
                AddColumns("OS_BatchID", "�妸�s��", "OS_BatchID", 120, GridView)
                AddColumns("PM_NO", "���ʳ渹", "PM_NO", 120, GridView)
                AddColumns("PS_QTY", "���ʼƶq", "PS_QTY", 100, GridView)
                AddColumns("PS_NoSendQty", "���ʥ����", "PS_NoSendQty", 100, GridView)
                AddColumns("PM_Check", "�f��", "PM_Check", 100, GridView)
            Case "�����禬�H��"
                Dim mcPurchaseAcc As New LFERP.Library.Purchase.Acceptance.AcceptanceController
                GridView.DataSource = mcPurchaseAcc.Acceptance_GetList(Nothing, Nothing, Nothing, BatchID, Nothing, Nothing, Nothing, M_Code, Nothing, Nothing, Nothing, Nothing, Nothing)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 5
                AddColumns("M_Code", "���ƽs�X", "M_Code", 120, GridView)
                AddColumns("OS_BatchID", "�妸�s��", "OS_BatchID", 120, GridView)
                AddColumns("A_AcceptanceNO", "�禬�渹", "A_AcceptanceNO", 120, GridView)
                AddColumns("A_Qty", "�禬�ƶq", "A_Qty", 100, GridView)
                AddColumns("A_Check", "�f��", "A_Check", 80, GridView)
            Case "���ʰh�f�H��"
                Dim mcPurchaseRet As New LFERP.Library.Purchase.Retrocede.RetrocedeController
                GridView.DataSource = mcPurchaseRet.Retrocede_GetList(Nothing, Nothing, Nothing, BatchID, Nothing, Nothing, Nothing, Nothing, M_Code, Nothing, Nothing, Nothing)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 5
                AddColumns("M_Code", "���ƽs�X", "M_Code", 120, GridView)
                AddColumns("OS_BatchID", "�妸�s��", "OS_BatchID", 120, GridView)
                AddColumns("R_RetrocedeNO", "�h�f�渹", "R_RetrocedeNO", 80, GridView)
                AddColumns("R_Qty", "�h�f��", "R_Qty", 80, GridView)
                AddColumns("R_Check", "�h�f�f��", "R_Check", 80, GridView)
            Case "�~�o�[�u�H��"
                Dim mcOutward As New LFERP.Library.Outward.OutwardController
                GridView.DataSource = mcOutward.OutwardSub_GetList(Nothing, Nothing, M_Code, Nothing, BatchID)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 5
                AddColumns("M_Code", "���ƽs�X", "M_Code", 120, GridView)
                AddColumns("OS_BatchID", "�妸�s��", "OS_BatchID", 120, GridView)
                AddColumns("O_NO", "�~�o�渹", "O_NO", 100, GridView)
                AddColumns("OS_Qty", "�~�o�ƶq", "OS_Qty", 80, GridView)
                AddColumns("OS_NoSendQty", "�~�o�����", "OS_NoSendQty", 120, GridView)
                AddColumns("O_Check", "�f��", "O_Check", 80, GridView)
                AddColumns("OS_ItemType", "��������", "OS_ItemType", 80, GridView)

            Case "�~�o�禬�H��"
                Dim mcOutwardAcc As New LFERP.Library.Outward.OutwardAcceptance.OutwardAcceptanceControl
                GridView.DataSource = mcOutwardAcc.OutwardAcceptance_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, M_Code, Nothing, BatchID, Nothing, Nothing)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 5
                AddColumns("M_Code", "���ƽs�X", "M_Code", 120, GridView)
                AddColumns("OS_BatchID", "�妸�s��", "OS_BatchID", 120, GridView)
                AddColumns("A_AcceptanceNO", "�禬�渹", "A_AcceptanceNO", 100, GridView)
                AddColumns("A_Qty", "��f�ƶq", "A_Qty", 80, GridView)
                AddColumns("A_Check", "�f��", "A_Check", 80, GridView)

            Case "�~�o�h�f�H��"
                Dim mcOutwardRet As New LFERP.Library.Outward.OutwardRetrocedeControl
                GridView.DataSource = mcOutwardRet.OutwardRetrocede_GetList(Nothing, Nothing, Nothing, Nothing, M_Code, BatchID)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 5
                AddColumns("M_Code", "���ƽs�X", "M_Code", 120, GridView)
                AddColumns("OS_BatchID", "�妸�s��", "OS_BatchID", 120, GridView)
                AddColumns("R_RetrocedeNO", "�h�f�渹", "R_RetrocedeNO", 80, GridView)
                AddColumns("R_Qty", "�h�f��", "R_Qty", 80, GridView)
                AddColumns("R_Check", "�h�f�f��", "R_Check", 80, GridView)

            Case "�w�s�H��"
                Dim mcInventory As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                Dim strCode As String
                strCode = "'" & M_Code & "'"
                GridView.DataSource = mcInventory.WareInventory_GetMaterial(Nothing, Nothing, strCode)

                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 5

                AddColumns("M_Code", "���ƽs�X", "M_Code", 120, GridView)
                AddColumns("WI_Qty", "�w�s�ƶq", "WI_Qty", 120, GridView)
                AddColumns("WH_ID", "�ܮw�s��", "WH_ID", 120, GridView)
            Case "�Ͳ����p"

            Case "�����Ӵ_�^�H��"
                Dim mcSupplier As New LFERP.Library.Purchase.Purchase.PurchaseSubsControl
                GridView.DataSource = mcSupplier.PurchaseSubs_GetList(Nothing, M_Code, BatchID)
                GridView.Columns.Clear()
                GridView.RowHeadersWidth = 5
                AddColumns("M_Code", "���ƽs�X", "M_Code", 120, GridView)
                AddColumns("OS_BatchID", "�妸�s��", "OS_BatchID", 80, GridView)
                AddColumns("PM_NO", "���ʳ渹", "PM_NO", 100, GridView)
                AddColumns("PSs_Date", "�_�^���", "PSs_Date", 120, GridView)
                AddColumns("PSs_Remark", "�_�^�Ƶ�", "PSs_Remark", 120, GridView)
        End Select
    End Sub
    Sub AddColumns(ByVal ColumnsName As String, ByVal HeadTxt As String, ByVal FieldName As String, _
    ByVal ColumnsWith As Integer, ByVal Grid As System.Windows.Forms.DataGridView)
        Dim objCol As New System.Windows.Forms.DataGridViewTextBoxColumn
        objCol.Name = ColumnsName
        objCol.HeaderText = HeadTxt
        objCol.DataPropertyName = FieldName
        objCol.Width = ColumnsWith
        Grid.Columns.AddRange(objCol)

    End Sub


    Public Function ArrayToString(ByVal arr As Integer()) As String
        '�ഫArray������String ,�b�ϥ�DataGrid�ɷ|�Ψ�
        Dim s As String = ""
        If arr Is Nothing Then
            s = "Empty..."
        Else
            Dim i As Integer
            For Each i In arr
                s += (IIf(s = "", "", ";")) + i.ToString()
            Next
            s += "."
        End If
        Return s
    End Function

    ''' <summary>
    ''' �p����j�p
    ''' </summary>
    ''' <param name="FileLength"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFileLength(ByVal FileLength As Long) As String
        If Math.Round(FileLength / (1024 * 1024 * 1024), 2) >= 0.98 Then
            GetFileLength = CStr(Math.Round(FileLength / (1024 * 1024 * 1024), 2)) + "GB"
        ElseIf Math.Round(FileLength / (1024 * 1024), 2) >= 0.98 Then
            GetFileLength = CStr(Math.Round(FileLength / (1024 * 1024), 2)) + "MB"
        Else
            GetFileLength = CStr(Math.Round((FileLength / 1024), 2)) + "KB"
        End If
    End Function

    Public Function KaoQinConn() As String
        KaoQinConn = ""
        If Mid(strInDPT_ID, 1, 4) = "1001" Then
            '----�p�צҶ�  
            KaoQinConn = "Data Source=dataserver;Initial Catalog=KaoQin;user id=sa;password=lpflpf"
        ElseIf Mid(strInDPT_ID, 1, 4) = "1002" Then
            '----�̨ȦҶ� 
            KaoQinConn = "Data Source=192.168.5.11;Initial Catalog=KaoQin;user id=sa;password=lpflpf"
        ElseIf Mid(strInDPT_ID, 1, 4) = "1003" Then
            '----�˧J�Ҷ�
            KaoQinConn = "Data Source=192.168.20.5;Initial Catalog=KaoQin;user id=mguser;password=wkpass"
        ElseIf Mid(strInDPT_ID, 1, 4) = "1004" Then
            '----�̨ȤG�t�Ҷ� 
            KaoQinConn = "Data Source=192.168.30.9;Initial Catalog=KaoQin;user id=sa;password=lpflpf"
        End If
    End Function

    Public Function GetName(ByVal strCardID As String) As String
        GetName = ""
        'Dim strConn As String = ""
        Dim myCommand As SqlCommand
        Dim strSql As String

        Dim myConn As New SqlConnection(KaoQinConn)

        myConn.Open()
        strSql = "select * from EmployeeFull where BadgeID='" & strCardID & "'"
        myCommand = New SqlCommand(strSql, myConn)
        Dim rdr As SqlDataReader = myCommand.ExecuteReader
        If rdr.HasRows = True Then      '�P�_�O�_�s�b�ŦX���󪺰O��
            Do While rdr.Read()
                GetName = rdr("name")
                strDepID = rdr("bri_NO")
                strPayType = rdr("Pay_Name")
            Loop
        Else
            GetName = ""
        End If
        myConn.Close()
    End Function

    Public Function ReadCard() As String


        ReadComm = Val(GetIni("CommSet", "ER900"))
        If ReadComm = 0 Then
            ReadComm = 1
        End If

        Dim portptr As IntPtr = ReadWriteCard.readwriteDll.OpenCommPort(ReadComm, 9600)
        Dim port As Integer = Int32.Parse(portptr.ToString())
        Dim isclock As Boolean



        If port <> -1 AndAlso port <> 0 Then
            isclock = ReadWriteCard.readwriteDll.CallClock(portptr, Int32.Parse(0))
            If isclock Then


                Dim temp As New ReadWriteCard.info
                temp.CardNo = New Byte(16) {}
                temp.CardName = New Byte(16) {}
                temp.Money = 0
                temp.Times = 0
                temp.Ver = 0
                Try
                    Dim suc As Boolean = ReadWriteCard.readwriteDll.ReadICCard(portptr, temp.CardNo, temp.CardName, temp.Money, temp.Times, temp.Ver)

                    If suc Then
                        If Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 9, 1) = "2" Then

                            ' ReadCard = Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 8) & "-" & GetName(Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 8))
                            'GBTOBIG5(System.Text.Encoding.GetEncoding("Gb2312").GetString(name1))
                            ReadCard = Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 8) & "-" & GBTOBIG5(System.Text.Encoding.GetEncoding("Gb2312").GetString(temp.CardName))
                        Else
                            ' ReadCard = Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 7) & "-" & GetName(Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 7))
                            ReadCard = Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 7) & "-" & GBTOBIG5(System.Text.Encoding.GetEncoding("Gb2312").GetString(temp.CardName))

                        End If
                    Else
                        MessageBox.Show("�L�k�����u�d�Ψ�d�����s���I")
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                ReadWriteCard.readwriteDll.CloseCommPort(ReadComm)
            Else
                MessageBox.Show("�p������!")
                ReadCard = ""
            End If
        ElseIf port = 0 Then
            MessageBox.Show("�L�k���}�ݤf!")
        ElseIf port = -1 Then

            MessageBox.Show("�ݤf�L�ĩΥ��b�ϥ�!")

        End If

    End Function

    Public Function ReadCard1() As String

        ReadComm = Val(GetIni("CommSet", "ER900"))
        If ReadComm = 0 Then
            ReadComm = 1
        End If

        Dim portptr As IntPtr = ReadWriteCard.readwriteDll.OpenCommPort(ReadComm, 9600)
        Dim port As Integer = Int32.Parse(portptr.ToString())
        Dim isclock As Boolean



        If port <> -1 AndAlso port <> 0 Then
            isclock = ReadWriteCard.readwriteDll.CallClock(portptr, Int32.Parse(0))
            If isclock Then


                Dim temp As New ReadWriteCard.info
                temp.CardNo = New Byte(16) {}
                temp.CardName = New Byte(16) {}
                temp.Money = 0
                temp.Times = 0
                temp.Ver = 0
                Try
                    Dim suc As Boolean = ReadWriteCard.readwriteDll.ReadICCard(portptr, temp.CardNo, temp.CardName, temp.Money, temp.Times, temp.Ver)

                    If suc Then
                        If Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 9, 1) = "2" Then

                            ' ReadCard = Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 8) & "-" & GetName(Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 8))
                            'GBTOBIG5(System.Text.Encoding.GetEncoding("Gb2312").GetString(name1))
                            ReadCard1 = Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 8)
                        Else
                            ' ReadCard = Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 7) & "-" & GetName(Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 7))
                            ReadCard1 = Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 7)

                        End If
                    Else
                        MessageBox.Show("�L�k�����u�d�Ψ�d�����s���I")
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                ReadWriteCard.readwriteDll.CloseCommPort(ReadComm)
            Else
                MessageBox.Show("�p������!")
                ReadCard1 = ""
            End If
        ElseIf port = 0 Then
            MessageBox.Show("�L�k���}�ݤf!")
        ElseIf port = -1 Then

            MessageBox.Show("�ݤf�L�ĩΥ��b�ϥ�!")

        End If

    End Function
    Function GBTOBIG5(ByVal GBCode As String) As String
        '��X�c����²��
        Dim gb As New EncodeRobert
        GBTOBIG5 = gb.SCTCConvert(ConvertType.Simplified, ConvertType.Traditional, GBCode)
    End Function

    Function BIG5TOGBA(ByVal GBCode As String) As String
        '��X�c����²��
        Dim gb As New EncodeRobert
        BIG5TOGBA = gb.SCTCConvert(ConvertType.Traditional, ConvertType.Simplified, GBCode)
    End Function



    ''Ū���ҶԫH��-------------------------
    Public Function LoadKQSumMonth(ByVal strPerNO As String, ByVal _Date_YYMM As String) As String

        On Error Resume Next

        'Dim strConn As String
        Dim myCommand As SqlCommand
        Dim strSql As String

        LoadKQSumMonth = ""
        'strConn = ""

        DoubleNormalDays = 0
        DoubleExtraHours = 0
        DoubleWeekTime = 0

        'If Mid(strInDPT_ID, 1, 4) = "1001" Then
        '    '----�p�צҶ�  
        '    strConn = "Data Source=dataserver;Initial Catalog=KaoQin;user id=sa;password=lpflpf"
        'ElseIf Mid(strInDPT_ID, 1, 4) = "1002" Then
        '    '----�̨ȦҶ� 
        '    strConn = "Data Source=192.168.5.11;Initial Catalog=KaoQin;user id=sa;password=lpflpf"
        'ElseIf Mid(strInDPT_ID, 1, 4) = "1003" Then
        '    '----�˧J�Ҷ�
        '    strConn = "Data Source=192.168.20.5;Initial Catalog=KaoQin;user id=mguser;password=wkpass"
        'ElseIf Mid(strInDPT_ID, 1, 4) = "1004" Then
        '    '----�̨ȤG�t�Ҷ� 
        '    strConn = "Data Source=192.168.30.9;Initial Catalog=KaoQin;user id=sa;password=lpflpf"
        'End If

        Dim myConn As New SqlConnection(KaoQinConn)
        myConn.Open()

        'strSql = "SELECT NormalDays,ExtraHours,(WeekTime-QJHours-SWHours) as WeekTime FROM KQSumMonth " _
        '  & " WHERE BadgeID='" & strPerNO & "' AND YM='" & _Date_YYMM & "'"

        '�̨ȻP�p�ת����P2013-5-14

        'strSql = "SELECT NormalDays,(ExtraHours-QJHours-SWHours) as ExtraHours,WeekTime FROM KQSumMonth " _
        '           & " WHERE BadgeID='" & strPerNO & "' AND YM='" & _Date_YYMM & "'"

        strSql = "SELECT NormalDays,(ExtraHours-QJHours-SWHours) as ExtraHours,(WeekTime+JRTime) as WeekTime FROM KQSumMonth " _
                   & " WHERE BadgeID='" & strPerNO & "' AND YM='" & _Date_YYMM & "'"


        'MsgBox(strSql)
        myCommand = New SqlCommand(strSql, myConn)
        Dim rdr As SqlDataReader = myCommand.ExecuteReader
        If rdr.HasRows = True Then      '�P�_�O�_�s�b�ŦX���󪺰O��
            Do While rdr.Read()

                If rdr("NormalDays") Is DBNull.Value = True Then '�W�Z�Ѽ�
                    DoubleNormalDays = 0
                Else
                    DoubleNormalDays = rdr("NormalDays")
                End If

                If rdr("ExtraHours") Is DBNull.Value = True Then '���ɥ[�Z
                    DoubleExtraHours = 0
                Else
                    DoubleExtraHours = rdr("ExtraHours")
                End If

                If rdr("WeekTime") Is DBNull.Value = True Then '����[�Z
                    DoubleWeekTime = 0
                Else
                    DoubleWeekTime = rdr("WeekTime")
                End If

            Loop
        Else
            LoadKQSumMonth = ""
        End If
        myConn.Close()
    End Function


    ''' <summary>
    ''' �_�� GridView �襤���h����
    ''' </summary>
    ''' <param name="GView"></param>
    ''' <param name="FiledName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GridViewCopyMulitRow(ByVal GView As DevExpress.XtraGrid.Views.Grid.GridView, ByVal FiledName As String, ByVal Type As String) As String
        GridViewCopyMulitRow = ""

        Dim n, i As Integer
        If Type = "ALL" Then
            n = GView.RowCount - 1
        End If

        Dim arr(n) As Integer

        If Type = "ALL" Then
            If GView.RowCount <= 0 Then
                Exit Function
            End If


            Dim j1 As Integer
            For j1 = 0 To GView.RowCount - 1
                arr(j1) = j1
            Next
        Else
            arr = GView.GetSelectedRows()
        End If




        n = arr.Length
        If n > 0 Then
        Else
            Exit Function
        End If
        ''--------------------------------------------------------------------------------------
        Dim m, j As Integer
        Dim arr1(n) As String
        arr1 = Split(FiledName, ",")
        m = arr1.Length
        If m > 0 Then
        Else
            Exit Function
        End If
        ''--------------------------------------------------------------------------------------

        Dim LsStr As String = ""
        Dim k As Integer

        For k = 0 To m - 1
            LsStr = LsStr + GView.Columns(arr1(k)).Caption.ToString & Chr(9)
        Next

        LsStr = LsStr + vbCrLf
        ''--------------------------------------------------------------------------------------


        For i = 0 To n - 1
            For j = 0 To m - 1
                'M_Gauge
                If arr1(j) = "M_Gauge" Then
                    LsStr = LsStr + Replace(GView.GetRowCellValue(arr(i), arr1(j)).ToString, vbCrLf, "") & Chr(9)
                Else
                    If GView.GetRowCellValue(arr(i), arr1(j)) Is DBNull.Value Then
                        LsStr = LsStr + "" & Chr(9)
                    Else
                        '   MsgBox(GView.GetRowCellValue(arr(i), arr1(j)))
                        LsStr = LsStr + CStr(GView.GetRowCellValue(arr(i), arr1(j))) & Chr(9)
                    End If
                End If


            Next
            LsStr = LsStr & vbCrLf
        Next

        Clipboard.SetText(LsStr)
    End Function

End Module
