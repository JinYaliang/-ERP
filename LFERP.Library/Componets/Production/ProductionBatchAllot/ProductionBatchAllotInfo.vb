Namespace LFERP.Library.ProductionBatchAllot
    Public Class ProductionBatchAllotInfo
        Private _AutoID As String               '�۰ʽs��
        Private _PBA_ID As String     '���u�渹
        Private _OS_BatchID As String         '�妸�s��
        Private _PBA_AddUserID As String        '�K�[�HID
        Private _PBA_AddUserName As String        '�K�[�H�m�W

        Private _PBA_AddDate As String     '�K�[���
        Private _PBA_ModifyUserID As String     '�ק�HID
        Private _PBA_ModifyUserName As String     '�ק�H�m�W
        Private _PBA_ModifyDate As String     '�ק���
        Private _PBA_Check As Boolean      '�f��

        Private _PBA_CheckUserID As String     '�f�֤HID
        Private _PBA_CheckUserName As String     '�f�֤H�m�W
        Private _PBA_CheckDate As String     '�f�֤��
        Private _FacID As String     '�t�O�s��
        Private _FacName As String     '�t�O�W��

        Private _M_Code As String     '���ƽs�X
        Private _M_Name As String     '���ƦW��
        Private _M_Gauge As String     '���ƳW��
        Private _M_Unit As String     '���Ƴ��
        Private _ON_NeedQty As Double      '�ݨD�q

        Private _PBA_Qty As Double     '�����ƶq
        Private _PBA_Remark As String     '�ƪ`


        Sub New()
            _AutoID = Nothing
            _PBA_ID = Nothing
            _OS_BatchID = Nothing
            _PBA_AddUserID = Nothing
            _PBA_AddUserName = Nothing

            _PBA_AddDate = Nothing
            _PBA_ModifyUserID = Nothing
            _PBA_ModifyUserName = Nothing
            _PBA_ModifyDate = Nothing
            _PBA_Check = False

            _PBA_CheckUserID = Nothing
            _PBA_CheckUserName = Nothing
            _PBA_CheckDate = Nothing
            _FacID = Nothing
            _FacName = Nothing

            _M_Code = Nothing
            _M_Name = Nothing
            _M_Gauge = Nothing
            _M_Unit = Nothing
            _ON_NeedQty = 0

            _PBA_Qty = 0
            _PBA_Remark = Nothing
        End Sub

        ''' <summary>
        ''' �۰ʽs��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AutoID() As String
            Get
                Return _AutoID
            End Get
            Set(ByVal value As String)
                _AutoID = value
            End Set
        End Property
        ''' <summary>
        ''' ���u�渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_ID() As String
            Get
                Return _PBA_ID
            End Get
            Set(ByVal value As String)
                _PBA_ID = value
            End Set
        End Property
        ''' <summary>
        ''' �妸�s��
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
        ''' �K�[�HID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_AddUserID() As String
            Get
                Return _PBA_AddUserID
            End Get
            Set(ByVal value As String)
                _PBA_AddUserID = value
            End Set
        End Property
        ''' <summary>
        ''' �K�[�H�m�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_AddUserName() As String
            Get
                Return _PBA_AddUserName
            End Get
            Set(ByVal value As String)
                _PBA_AddUserName = value
            End Set
        End Property

        ''' <summary>
        ''' �K�[���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_AddDate() As String
            Get
                Return _PBA_AddDate
            End Get
            Set(ByVal value As String)
                _PBA_AddDate = value
            End Set
        End Property
        ''' <summary>
        ''' �ק�HID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_ModifyUserID() As String
            Get
                Return _PBA_ModifyUserID
            End Get
            Set(ByVal value As String)
                _PBA_ModifyUserID = value
            End Set
        End Property
        ''' <summary>
        ''' �ק�H�m�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_ModifyUserName() As String
            Get
                Return _PBA_ModifyUserName
            End Get
            Set(ByVal value As String)
                _PBA_ModifyUserName = value
            End Set
        End Property
        ''' <summary>
        ''' �ק���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_ModifyDate() As String
            Get
                Return _PBA_ModifyDate
            End Get
            Set(ByVal value As String)
                _PBA_ModifyDate = value
            End Set
        End Property
        ''' <summary>
        ''' �f��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_Check() As Boolean
            Get
                Return _PBA_Check
            End Get
            Set(ByVal value As Boolean)
                _PBA_Check = value
            End Set
        End Property

        ''' <summary>
        ''' �f�֤HID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_CheckUserID() As String
            Get
                Return _PBA_CheckUserID
            End Get
            Set(ByVal value As String)
                _PBA_CheckUserID = value
            End Set
        End Property
        ''' <summary>
        ''' �f�֤H�m�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_CheckUserName() As String
            Get
                Return _PBA_CheckUserName
            End Get
            Set(ByVal value As String)
                _PBA_CheckUserName = value
            End Set
        End Property
        ''' <summary>
        ''' �f�֤��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_CheckDate() As String
            Get
                Return _PBA_CheckDate
            End Get
            Set(ByVal value As String)
                _PBA_CheckDate = value
            End Set
        End Property
        ''' <summary>
        ''' �t�O�s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property FacID() As String
            Get
                Return _FacID
            End Get
            Set(ByVal value As String)
                _FacID = value
            End Set
        End Property
        ''' <summary>
        ''' �t�O�W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property FacName() As String
            Get
                Return _FacName
            End Get
            Set(ByVal value As String)
                _FacName = value
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
        ''' ���Ƴ��
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
        ''' �ݨD�q
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ON_NeedQty() As Double
            Get
                Return _ON_NeedQty
            End Get
            Set(ByVal value As Double)
                _ON_NeedQty = value
            End Set
        End Property

        ''' <summary>
        ''' �����ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_Qty() As Double
            Get
                Return _PBA_Qty
            End Get
            Set(ByVal value As Double)
                _PBA_Qty = value
            End Set
        End Property
        ''' <summary>
        ''' �ƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PBA_Remark() As String
            Get
                Return _PBA_Remark
            End Get
            Set(ByVal value As String)
                _PBA_Remark = value
            End Set
        End Property

    End Class
End Namespace

