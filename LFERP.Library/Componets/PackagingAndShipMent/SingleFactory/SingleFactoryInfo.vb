

Namespace LFERP.Library.SingleFactory

    Public Class SingleFactoryInfo

        Private _SF_ID As String
        Private _SF_Address As String
        Private _SF_Date As Date
        Private _SF_Remark As String
        Private _SF_Action As String
        Private _SF_Check As Boolean
        Private _SF_CheckAction As String
        Private _SF_CheckDate As Date
        Private _SF_CheckType As String
        Private _SF_CheckRemark As String

        Private _P_NO As String
        Private _P_Detail As String
        Private _P_Remark As String
        Private _PB_NO As String
        Private _P_Type As String
        Private _OS_BatchID As String
        Private _M_Code As String
        Private _P_Qty As Double
        Private _P_SP As Double
        Private _P_Help As String

        Private _PB_Qty As Double
        Private _PB_SP As Double

        Private _SF_ActionName As String
        Private _SF_CheckActionName As String
        Private _M_Name As String
        Private _M_Gauge As String

        Private _AutoID As Integer
        Private _IndexNO As Integer
        Private _IndexNum As Integer

        Private _ID As String
        Private _IDSub As String

        Sub New()

            _SF_ID = Nothing
            _SF_Address = Nothing
            _SF_Date = Nothing
            _SF_Remark = Nothing
            _SF_Action = Nothing
            _SF_Check = False
            _SF_CheckAction = Nothing
            _SF_CheckType = Nothing
            _SF_CheckDate = Nothing
            _SF_CheckRemark = Nothing

            _P_NO = Nothing
            _P_Detail = Nothing
            _P_Remark = Nothing
            _PB_NO = Nothing
            _P_Type = Nothing
            _OS_BatchID = Nothing
            _M_Code = Nothing
            _P_Qty = 0
            _P_SP = 0
            _P_Help = Nothing

            _PB_Qty = 0
            _PB_SP = 0

            _SF_ActionName = Nothing
            _SF_CheckActionName = Nothing
            _M_Name = Nothing
            _M_Gauge = Nothing

            _IndexNO = 0
            _AutoID = 0
            _IndexNum = 0
            _ID = Nothing
            _IDSub = Nothing

        End Sub
        ''' <summary>
        ''' �X�t�渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SF_ID() As String
            Get
                Return _SF_ID
            End Get
            Set(ByVal value As String)
                _SF_ID = value
            End Set
        End Property
        ''' <summary>
        ''' �X�t�a�I
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SF_Address() As String
            Get
                Return _SF_Address
            End Get
            Set(ByVal value As String)
                _SF_Address = value
            End Set
        End Property
        ''' <summary>
        ''' �X�t���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SF_Date() As Date
            Get
                Return _SF_Date
            End Get
            Set(ByVal value As Date)
                _SF_Date = value
            End Set
        End Property
        ''' <summary>
        ''' �Ƶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SF_Remark() As String
            Get
                Return _SF_Remark
            End Get
            Set(ByVal value As String)
                _SF_Remark = value
            End Set
        End Property
        ''' <summary>
        ''' �ާ@��ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SF_Action() As String
            Get
                Return _SF_Action
            End Get
            Set(ByVal value As String)
                _SF_Action = value
            End Set
        End Property
        ''' <summary>
        ''' �f��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SF_Check() As Boolean
            Get
                Return _SF_Check
            End Get
            Set(ByVal value As Boolean)
                _SF_Check = value
            End Set
        End Property
        ''' <summary>
        ''' �f�֭�ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SF_CheckAction() As String
            Get
                Return _SF_CheckAction
            End Get
            Set(ByVal value As String)
                _SF_CheckAction = value
            End Set
        End Property
        ''' <summary>
        ''' �f�֤��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SF_CheckDate() As Date
            Get
                Return _SF_CheckDate
            End Get
            Set(ByVal value As Date)
                _SF_CheckDate = value
            End Set
        End Property
        ''' <summary>
        ''' �f�ֳƵ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SF_CheckRemark() As String
            Get
                Return _SF_CheckRemark
            End Get
            Set(ByVal value As String)
                _SF_CheckRemark = value
            End Set
        End Property
        ''' <summary>
        ''' �f������
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SF_CheckType() As String
            Get
                Return _SF_CheckType
            End Get
            Set(ByVal value As String)
                _SF_CheckType = value
            End Set
        End Property
        ''' <summary>
        ''' �˽c�s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property P_NO() As String
            Get
                Return _P_NO
            End Get
            Set(ByVal value As String)
                _P_NO = value
            End Set
        End Property
        ''' <summary>
        ''' �˽c���e
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property P_Detail() As String
            Get
                Return _P_Detail
            End Get
            Set(ByVal value As String)
                _P_Detail = value
            End Set
        End Property
        ''' <summary>
        ''' �˽c�Ƶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property P_Remark() As String
            Get
                Return _P_Remark
            End Get
            Set(ByVal value As String)
                _P_Remark = value
            End Set
        End Property
        ''' <summary>
        ''' �˽c���ؽs��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PB_NO() As String
            Get
                Return _PB_NO
            End Get
            Set(ByVal value As String)
                _PB_NO = value
            End Set
        End Property
        ''' <summary>
        ''' �˽c����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property P_Type() As String
            Get
                Return _P_Type
            End Get
            Set(ByVal value As String)
                _P_Type = value
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
        ''' �˽c�ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property P_Qty() As Double
            Get
                Return _P_Qty
            End Get
            Set(ByVal value As Double)
                _P_Qty = value
            End Set
        End Property
        ''' <summary>
        ''' �˽c�h��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property P_SP() As Double
            Get
                Return _P_SP
            End Get
            Set(ByVal value As Double)
                _P_SP = value
            End Set
        End Property
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property P_Help() As String
            Get
                Return _P_Help
            End Get
            Set(ByVal value As String)
                _P_Help = value
            End Set
        End Property
        ''' <summary>
        ''' �˽c���ؼƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PB_Qty() As Double
            Get
                Return _PB_Qty
            End Get
            Set(ByVal value As Double)
                _PB_Qty = value
            End Set
        End Property
        ''' <summary>
        ''' �˽c���ؤh��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PB_SP() As Double
            Get
                Return _PB_SP
            End Get
            Set(ByVal value As Double)
                _PB_SP = value
            End Set
        End Property
        ''' <summary>
        ''' �ާ@��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SF_ActionName() As String
            Get
                Return _SF_ActionName
            End Get
            Set(ByVal value As String)
                _SF_ActionName = value
            End Set
        End Property
        ''' <summary>
        ''' �f�֭�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SF_CheckActionName() As String
            Get
                Return _SF_CheckActionName
            End Get
            Set(ByVal value As String)
                _SF_CheckActionName = value
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

        ''' <summary>
        ''' �۰ʬy����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IndexNO() As Integer
            Get
                Return _IndexNO
            End Get
            Set(ByVal value As Integer)
                _IndexNO = value
            End Set
        End Property
        Public Property AutoID() As Integer
            Get
                Return _AutoID
            End Get
            Set(ByVal value As Integer)
                _AutoID = value
            End Set
        End Property
        Public Property IndexNum() As Integer
            Get
                Return _IndexNum
            End Get
            Set(ByVal value As Integer)
                _IndexNum = value
            End Set
        End Property

        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
            End Set
        End Property
        Public Property IDSub() As String
            Get
                Return _IDSub
            End Get
            Set(ByVal value As String)
                _IDSub = value
            End Set
        End Property
    End Class

End Namespace

