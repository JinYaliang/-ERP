Namespace LFERP.Library.ProductProcess

    Public Class ProcessTypeInfo
        Private _AutoID As String
        Private _D_ID As String
        Private _NumberID As Integer
        Private _D_Type As String
        Private _D_TypeID As String
        Private _D_Dep As String
        Private _D_ProcessName As String
        Private _D_IsSub As Boolean
        Private _Remarks As String
        Sub New()
            _AutoID = Nothing
            _D_ID = Nothing
            _NumberID = 0
            _D_Type = Nothing
            _D_Dep = Nothing
            _D_TypeID = Nothing
            _D_ProcessName = Nothing
            _D_IsSub = Nothing
            _Remarks = Nothing
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
        ''' ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property NumberID() As Integer
            Get
                Return _NumberID
            End Get
            Set(ByVal value As Integer)
                _NumberID = value
            End Set
        End Property
        ''' <summary>
        ''' ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property D_ID() As String
            Get
                Return _D_ID
            End Get
            Set(ByVal value As String)
                _D_ID = value
            End Set
        End Property
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property D_Type() As String
            Get
                Return _D_Type
            End Get
            Set(ByVal value As String)
                _D_Type = value
            End Set
        End Property
        ''' <summary>
        ''' �����s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property D_TypeID() As String
            Get
                Return _D_TypeID
            End Get
            Set(ByVal value As String)
                _D_TypeID = value
            End Set
        End Property
        ''' <summary>
        ''' 
        ''' ����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property D_Dep() As String
            Get
                Return _D_Dep
            End Get
            Set(ByVal value As String)
                _D_Dep = value
            End Set
        End Property
        ''' <summary>
        ''' �u���W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property D_ProcessName() As String
            Get
                Return _D_ProcessName
            End Get
            Set(ByVal value As String)
                _D_ProcessName = value
            End Set
        End Property
        ''' <summary>
        ''' �O�_�ҥ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property D_IsSub() As Boolean
            Get
                Return _D_IsSub
            End Get
            Set(ByVal value As Boolean)
                _D_IsSub = value
            End Set
        End Property

        ''' <summary>
        ''' �`�N�ƶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Remarks() As String
            Get
                Return _Remarks
            End Get
            Set(ByVal value As String)
                _Remarks = value
            End Set
        End Property

    End Class
End Namespace

