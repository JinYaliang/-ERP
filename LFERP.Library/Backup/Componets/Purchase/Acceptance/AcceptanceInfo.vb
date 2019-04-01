Namespace LFERP.Library.Purchase.Acceptance
    Public Class AcceptanceInfo
        Private _DepotNO As String
        Private _PM_NO As String
        Private _A_AcceptanceNO As String
        Private _A_NO As String
        Private _A_SendNO As String
        Private _M_Code As String
        Private _M_Unit As String
        Private _M_Name As String
        Private _M_Gauge As String
        Private _OS_BatchID As String
        Private _PM_M_Code As String

        Private _A_Ver As String
        Private _A_Qty As String
        Private _A_SendDate As String
        Private _A_SendType As String
        Private _S_Supplier As String
        Private _A_Action As String
        Private _A_Remark As String
        Private _A_Detail As String
        Private _A_UpdateDate As String
        Private _A_Check As Boolean
        Private _A_CheckAction As String
        Private _A_CheckDate As String
        Private _A_CheckType As String
        Private _A_CheckRemark As String
        Private _A_AccountCheck As Boolean
        Private _A_AccountCheckAction As String
        Private _A_AccountCheckDate As String
        Private _A_AccountCheckType As String
        Private _A_AccountCheckRemark As String
        Private _DPT_ID As String
        Private _U_ID As String
        Private _S_SupplierNO As String

        Private _A_PayCheck As Boolean
        Private _A_PayCheckDate As String
        Private _A_PayCheckAction As String
        Private _A_PayCheckRemark As String

        Private _PN_NO As String
        Private _A_ToFrom As String

        Private _S_SupplierName As String
        Private _WQC_Status As String
        Private _WQC_Action As String

        Private _A_Date1 As String
        Private _A_Date2 As String
        Private _M_Price As String
        Private _M_Currency As String

        Public Sub New()
            _DepotNO = Nothing
            _PM_NO = Nothing
            _A_AcceptanceNO = Nothing
            _A_NO = Nothing
            _A_SendNO = Nothing
            _M_Code = Nothing
            _M_Unit = Nothing
            _M_Name = Nothing
            _M_Gauge = Nothing
            _OS_BatchID = Nothing
            _PM_M_Code = Nothing
    
            _A_Ver = Nothing
            _A_Qty = Nothing
            _A_SendDate = Nothing

            _A_SendType = Nothing

            _S_Supplier = Nothing

            _A_Action = Nothing

            _A_Remark = Nothing

            _A_Detail = Nothing

            _A_UpdateDate = Nothing

            _A_Check = False

            _A_CheckAction = Nothing

            _A_CheckDate = Nothing

            _A_CheckType = Nothing

            _A_CheckRemark = Nothing

            _A_AccountCheck = False

            _A_AccountCheckAction = Nothing

            _A_AccountCheckDate = Nothing

            _A_AccountCheckType = Nothing
            _A_AccountCheckRemark = Nothing

            _DPT_ID = Nothing
            _U_ID = Nothing
            _S_SupplierNO = Nothing


            _A_PayCheck = False
            _A_PayCheckDate = Nothing
            _A_PayCheckAction = Nothing
            _A_PayCheckRemark = Nothing

            _PN_NO = Nothing
            _A_ToFrom = Nothing

            _S_SupplierName = Nothing
            _WQC_Status = Nothing
            _WQC_Action = Nothing

            _A_Date1 = Nothing
            _A_Date2 = Nothing

        End Sub
        ''' <summary>
        ''' �ܮw�N��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DepotNO() As String
            Get
                Return _DepotNO
            End Get
            Set(ByVal value As String)
                _DepotNO = value
            End Set
        End Property

        ''' <summary>
        ''' ���ʳ渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PM_NO() As String
            Get
                Return _PM_NO
            End Get
            Set(ByVal value As String)
                _PM_NO = value
            End Set
        End Property

        ''' <summary>
        '''�禬�渹 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_AcceptanceNO() As String
            Get
                Return _A_AcceptanceNO
            End Get
            Set(ByVal value As String)
                _A_AcceptanceNO = value
            End Set
        End Property

        ''' <summary>
        ''' �禬�y����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_NO() As String
            Get
                Return _A_NO
            End Get
            Set(ByVal value As String)
                _A_NO = value
            End Set
        End Property

        ''' <summary>
        ''' �e�f�渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_SendNO() As String
            Get
                Return _A_SendNO
            End Get
            Set(ByVal value As String)
                _A_SendNO = value
            End Set
        End Property

        ''' <summary>
        ''' ���ƽs�X
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property M_Code() As String
            Get
                Return _M_Code
            End Get
            Set(ByVal value As String)
                _M_Code = value
            End Set
        End Property


        ''' <summary>
        ''' ���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property M_Unit() As String
            Get
                Return _M_Unit
            End Get
            Set(ByVal value As String)
                _M_Unit = value
            End Set
        End Property


        ''' <summary>
        ''' ���ƦW��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property M_Name() As String
            Get
                Return _M_Name
            End Get
            Set(ByVal value As String)
                _M_Name = value
            End Set
        End Property


        ''' <summary>
        ''' ���ƳW��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property M_Gauge() As String
            Get
                Return _M_Gauge
            End Get
            Set(ByVal value As String)
                _M_Gauge = value
            End Set
        End Property


        ''' <summary>
        ''' �妸
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property OS_BatchID() As String
            Get
                Return _OS_BatchID
            End Get
            Set(ByVal value As String)
                _OS_BatchID = value
            End Set
        End Property


        ''' <summary>
        ''' �p�׽s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PM_M_Code() As String
            Get
                Return _PM_M_Code
            End Get
            Set(ByVal value As String)
                _PM_M_Code = value
            End Set
        End Property




        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_Ver() As String
            Get
                Return _A_Ver
            End Get
            Set(ByVal value As String)
                _A_Ver = value
            End Set
        End Property


        ''' <summary>
        ''' �e�f�ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_Qty() As String
            Get
                Return _A_Qty
            End Get
            Set(ByVal value As String)
                _A_Qty = value
            End Set
        End Property



        ''' <summary>
        ''' �e�f���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_SendDate() As String
            Get
                Return _A_SendDate
            End Get
            Set(ByVal value As String)
                _A_SendDate = value
            End Set
        End Property









        ''' <summary>
        ''' �e�f����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_SendType() As String
            Get
                Return _A_SendType
            End Get
            Set(ByVal value As String)
                _A_SendType = value
            End Set
        End Property

        ''' <summary>
        ''' ����������
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property S_Supplier() As String
            Get
                Return _S_Supplier
            End Get
            Set(ByVal value As String)
                _S_Supplier = value
            End Set
        End Property



        ''' <summary>
        ''' �ާ@��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_Action() As String
            Get
                Return _A_Action
            End Get
            Set(ByVal value As String)
                _A_Action = value
            End Set
        End Property


        ''' <summary>
        ''' �ƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_Remark() As String
            Get
                Return _A_Remark
            End Get
            Set(ByVal value As String)
                _A_Remark = value
            End Set
        End Property


        ''' <summary>
        ''' ���A
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_Detail() As String
            Get
                Return _A_Detail
            End Get
            Set(ByVal value As String)
                _A_Detail = value
            End Set
        End Property



        ''' <summary>
        ''' �ק���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_UpdateDate() As String
            Get
                Return _A_UpdateDate
            End Get
            Set(ByVal value As String)
                _A_UpdateDate = value
            End Set
        End Property



        ''' <summary>
        ''' �f��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_Check() As Boolean
            Get
                Return _A_Check
            End Get
            Set(ByVal value As Boolean)
                _A_Check = value
            End Set
        End Property



        ''' <summary>
        ''' �f�֭�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_CheckAction() As String
            Get
                Return _A_CheckAction
            End Get
            Set(ByVal value As String)
                _A_CheckAction = value
            End Set
        End Property



        ''' <summary>
        ''' �f�֤��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_CheckDate() As String
            Get
                Return _A_CheckDate
            End Get
            Set(ByVal value As String)
                _A_CheckDate = value
            End Set
        End Property



        ''' <summary>
        ''' �f������
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_CheckType() As String
            Get
                Return _A_CheckType
            End Get
            Set(ByVal value As String)
                _A_CheckType = value
            End Set
        End Property



        ''' <summary>
        ''' �f�ֳƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_CheckRemark() As String
            Get
                Return _A_CheckRemark
            End Get
            Set(ByVal value As String)
                _A_CheckRemark = value
            End Set
        End Property



        ''' <summary>
        ''' �|�p���f��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_AccountCheck() As Boolean
            Get
                Return _A_AccountCheck
            End Get
            Set(ByVal value As Boolean)
                _A_AccountCheck = value
            End Set
        End Property


        ''' <summary>
        ''' �|�p���f�֭�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_AccountCheckAction() As String
            Get
                Return _A_AccountCheckAction
            End Get
            Set(ByVal value As String)
                _A_AccountCheckAction = value
            End Set
        End Property



        ''' <summary>
        ''' �|�p���f�֤��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_AccountCheckDate() As String
            Get
                Return _A_AccountCheckDate
            End Get
            Set(ByVal value As String)
                _A_AccountCheckDate = value
            End Set
        End Property


        ''' <summary>
        ''' �|�p���f������
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_AccountCheckType() As String
            Get
                Return _A_AccountCheckType
            End Get
            Set(ByVal value As String)
                _A_AccountCheckType = value
            End Set
        End Property



        ''' <summary>
        ''' �|�p���f�ֳƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_AccountCheckRemark() As String
            Get
                Return _A_AccountCheckRemark
            End Get
            Set(ByVal value As String)
                _A_AccountCheckRemark = value
            End Set
        End Property


        ''' <summary>
        ''' �ӻⳡ��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DPT_ID() As String
            Get
                Return _DPT_ID
            End Get
            Set(ByVal value As String)
                _DPT_ID = value
            End Set
        End Property

        ''' <summary>
        ''' �ӻ�H��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property U_ID() As String
            Get
                Return _U_ID
            End Get
            Set(ByVal value As String)
                _U_ID = value
            End Set
        End Property


        ''' <summary>
        ''' �����ӽs��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property S_SupplierNO() As String
            Get
                Return _S_SupplierNO
            End Get
            Set(ByVal value As String)
                _S_SupplierNO = value
            End Set
        End Property


        ''' <summary>
        ''' �I�ڼf��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_PayCheck() As Boolean
            Get
                Return _A_PayCheck
            End Get
            Set(ByVal value As Boolean)

                _A_PayCheck = value
            End Set
        End Property


        ''' <summary>
        ''' �I�ڤ��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_PayCheckDate() As String
            Get
                Return _A_PayCheckDate
            End Get
            Set(ByVal value As String)
                _A_PayCheckDate = value
            End Set
        End Property

        ''' <summary>
        ''' �I�ڤH
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_PayCheckAction() As String
            Get
                Return _A_PayCheckAction
            End Get
            Set(ByVal value As String)
                _A_PayCheckAction = value
            End Set
        End Property



        ''' <summary>
        ''' �I�ڳƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_PayCheckRemark() As String
            Get
                Return _A_PayCheckRemark
            End Get
            Set(ByVal value As String)
                _A_PayCheckRemark = value
            End Set
        End Property


        ''' <summary>
        ''' ���ʳ渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PN_NO() As String
            Get
                Return _PN_NO
            End Get
            Set(ByVal value As String)
                _PN_NO = value
            End Set
        End Property

        ''' <summary>
        ''' ���ʳ�I�ڤ覡
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_ToFrom() As String
            Get
                Return _A_ToFrom
            End Get
            Set(ByVal value As String)
                _A_ToFrom = value
            End Set
        End Property


        Public Property S_SupplierName() As String
            Get
                Return _S_SupplierName
            End Get
            Set(ByVal value As String)
                _S_SupplierName = value
            End Set
        End Property

        Public Property WQC_Status() As String
            Get
                Return _WQC_Status
            End Get
            Set(ByVal value As String)
                _WQC_Status = value
            End Set
        End Property
        Public Property WQC_Action() As String
            Get
                Return _WQC_Action
            End Get
            Set(ByVal value As String)
                _WQC_Action = value
            End Set
        End Property

        Public Property A_Date1() As String
            Get
                Return _A_Date1
            End Get
            Set(ByVal value As String)
                _A_Date1 = value
            End Set
        End Property
        Public Property A_Date2() As String
            Get
                Return _A_Date2
            End Get
            Set(ByVal value As String)
                _A_Date2 = value
            End Set
        End Property
        '���
        Public Property M_Price() As String
            Get
                Return _M_Price
            End Get
            Set(ByVal value As String)
                _M_Price = value
            End Set
        End Property
        '���O
        Public Property M_Currency() As String
            Get
                Return _M_Currency
            End Get
            Set(ByVal value As String)
                _M_Currency = value
            End Set
        End Property
    End Class
End Namespace
