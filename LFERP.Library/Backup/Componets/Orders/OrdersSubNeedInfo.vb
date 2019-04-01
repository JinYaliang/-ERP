Namespace LFERP.Library.Orders
    Public Class OrdersSubNeedInfo
        Private _ON_ID As String   '�ݨD�渹
        Private _OS_BatchID As String  '�妸�s��
        Private _OS_Qty As String   '�妸�ƶq
        Private _M_Code As String '���ƽs�X
        Private _M_Name As String    '���ƦW��

        Private _M_Gauge As String   '���ƳW��
        Private _M_Unit As String   '���
        Private _M_Weight As String    '�ζq
        Private _ON_NeedQty As String  '�z�׻ݨD��
        Private _M_CodeType As String '�t������

        Private _WI_Qty As String    '��e�w�s��
        Private _ON_PrepareQty As String  '�i�ƮƼ�
        Private _ON_NeedPurchaseQty As String   '�ݱ��ʼ�
        Private _ON_ReallyPurchaseQty As String '��ڱ��ʼ�
        Private _ON_PurchaseReturnQty As String    '���ʤw�^��

        Private _ON_TakeQty As String   '�w�����
        Private _ON_Remark As String  '�ƪ`  
        Private _ON_SalesDptCheck As Boolean   '��~���f��
        Private _ON_OperationDptCheck As Boolean   '��B���f��
        Private _AutoID As String   '�۰ʽs��

        Private _ON_PurchaseState As String   '���ʪ��A
        Private _ON_AddUser As String   '�K�[�H
        Private _ON_AddDate As String   '�K�[���
        Private _ON_ModifyUser As String   '�ק�H
        Private _ON_ModifyDate As String   '�ק���

        Private _ON_SalesDptCheckUser As String   '��~���f�֤H
        Private _ON_SalesDptCheckDate As String   '��~���f�֤��
        Private _ON_OperationDptCheckUser As String   '��B���f�֤H
        Private _ON_OperationDptCheckDate As String   '��B���f�֤��
        Private _WI_UsableQty As String    '�i�ήw�s��



        Public Sub New()
            _ON_ID = Nothing
            _OS_BatchID = Nothing
            _OS_Qty = Nothing
            _M_Code = Nothing
            _M_Name = Nothing

            _M_Gauge = Nothing
            _M_Unit = Nothing
            _M_Weight = Nothing
            _ON_NeedQty = Nothing
            _M_CodeType = Nothing

            _WI_Qty = Nothing
            _ON_PrepareQty = Nothing
            _ON_NeedPurchaseQty = Nothing
            _ON_ReallyPurchaseQty = Nothing
            _ON_PurchaseReturnQty = Nothing

            _ON_TakeQty = Nothing
            _ON_Remark = Nothing
            _ON_SalesDptCheck = Nothing
            _ON_OperationDptCheck = Nothing
            _AutoID = Nothing

            _ON_PurchaseState = Nothing
            _ON_AddUser = Nothing
            _ON_AddDate = Nothing
            _ON_ModifyUser = Nothing
            _ON_ModifyDate = Nothing

            _ON_SalesDptCheckUser = Nothing
            _ON_SalesDptCheckDate = Nothing
            _ON_OperationDptCheckUser = Nothing
            _ON_OperationDptCheckDate = Nothing
            _WI_UsableQty = Nothing
        End Sub

        Public Property ON_ID() As String
            Get
                Return _ON_ID
            End Get
            Set(ByVal value As String)
                _ON_ID = value
            End Set
        End Property
        Public Property OS_BatchID() As String
            Get
                Return _OS_BatchID
            End Get
            Set(ByVal value As String)
                _OS_BatchID = value
            End Set
        End Property
        Public Property OS_Qty() As String
            Get
                Return _OS_Qty
            End Get
            Set(ByVal value As String)
                _OS_Qty = value
            End Set
        End Property
        Public Property M_Code() As String
            Get
                Return _M_Code
            End Get
            Set(ByVal value As String)
                _M_Code = value
            End Set
        End Property
        Public Property M_Name() As String
            Get
                Return _M_Name
            End Get
            Set(ByVal value As String)
                _M_Name = value
            End Set
        End Property

        Public Property M_Gauge() As String
            Get
                Return _M_Gauge
            End Get
            Set(ByVal value As String)
                _M_Gauge = value
            End Set
        End Property
        Public Property M_Unit() As String
            Get
                Return _M_Unit
            End Get
            Set(ByVal value As String)
                _M_Unit = value
            End Set
        End Property
        Public Property M_Weight() As String
            Get
                Return _M_Weight
            End Get
            Set(ByVal value As String)
                _M_Weight = value
            End Set
        End Property
        Public Property ON_NeedQty() As String
            Get
                Return _ON_NeedQty
            End Get
            Set(ByVal value As String)
                _ON_NeedQty = value
            End Set
        End Property
        Public Property M_CodeType() As String
            Get
                Return _M_CodeType
            End Get
            Set(ByVal value As String)
                _M_CodeType = value
            End Set
        End Property

        Public Property WI_Qty() As String
            Get
                Return _WI_Qty
            End Get
            Set(ByVal value As String)
                _WI_Qty = value
            End Set
        End Property
        Public Property ON_PrepareQty() As String
            Get
                Return _ON_PrepareQty
            End Get
            Set(ByVal value As String)
                _ON_PrepareQty = value
            End Set
        End Property
        Public Property ON_NeedPurchaseQty() As String
            Get
                Return _ON_NeedPurchaseQty
            End Get
            Set(ByVal value As String)
                _ON_NeedPurchaseQty = value
            End Set
        End Property
        Public Property ON_ReallyPurchaseQty() As String
            Get
                Return _ON_ReallyPurchaseQty
            End Get
            Set(ByVal value As String)
                _ON_ReallyPurchaseQty = value
            End Set
        End Property
        Public Property ON_PurchaseReturnQty() As String
            Get
                Return _ON_PurchaseReturnQty
            End Get
            Set(ByVal value As String)
                _ON_PurchaseReturnQty = value
            End Set
        End Property

        Public Property ON_TakeQty() As String
            Get
                Return _ON_TakeQty
            End Get
            Set(ByVal value As String)
                _ON_TakeQty = value
            End Set
        End Property
        Public Property ON_Remark() As String
            Get
                Return _ON_Remark
            End Get
            Set(ByVal value As String)
                _ON_Remark = value
            End Set
        End Property
        Public Property ON_SalesDptCheck() As Boolean
            Get
                Return _ON_SalesDptCheck
            End Get
            Set(ByVal value As Boolean)
                _ON_SalesDptCheck = value
            End Set
        End Property
        Public Property ON_OperationDptCheck() As Boolean
            Get
                Return _ON_OperationDptCheck
            End Get
            Set(ByVal value As Boolean)
                _ON_OperationDptCheck = value
            End Set
        End Property
        Public Property AutoID() As String
            Get
                Return _AutoID
            End Get
            Set(ByVal value As String)
                _AutoID = value
            End Set
        End Property

        Public Property ON_PurchaseState() As String
            Get
                Return _ON_PurchaseState
            End Get
            Set(ByVal value As String)
                _ON_PurchaseState = value
            End Set
        End Property
        Public Property ON_AddUser() As String
            Get
                Return _ON_AddUser
            End Get
            Set(ByVal value As String)
                _ON_AddUser = value
            End Set
        End Property
        Public Property ON_AddDate() As String
            Get
                Return _ON_AddDate
            End Get
            Set(ByVal value As String)
                _ON_AddDate = value
            End Set
        End Property
        Public Property ON_ModifyUser() As String
            Get
                Return _ON_ModifyUser
            End Get
            Set(ByVal value As String)
                _ON_ModifyUser = value
            End Set
        End Property
        Public Property ON_ModifyDate() As String
            Get
                Return _ON_ModifyDate
            End Get
            Set(ByVal value As String)
                _ON_ModifyDate = value
            End Set
        End Property

        Public Property ON_SalesDptCheckUser() As String
            Get
                Return _ON_SalesDptCheckUser
            End Get
            Set(ByVal value As String)
                _ON_SalesDptCheckUser = value
            End Set
        End Property
        Public Property ON_SalesDptCheckDate() As String
            Get
                Return _ON_SalesDptCheckDate
            End Get
            Set(ByVal value As String)
                _ON_SalesDptCheckDate = value
            End Set
        End Property
        Public Property ON_OperationDptCheckUser() As String
            Get
                Return _ON_OperationDptCheckUser
            End Get
            Set(ByVal value As String)
                _ON_OperationDptCheckUser = value
            End Set
        End Property
        Public Property ON_OperationDptCheckDate() As String
            Get
                Return _ON_OperationDptCheckDate
            End Get
            Set(ByVal value As String)
                _ON_OperationDptCheckDate = value
            End Set
        End Property
        Public Property WI_UsableQty() As String
            Get
                Return _WI_UsableQty
            End Get
            Set(ByVal value As String)
                _WI_UsableQty = value
            End Set
        End Property
    End Class
End Namespace

