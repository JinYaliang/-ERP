Namespace LFERP.DataSetting

    Public Class SuppliersTypeInfo
        Private _s_typeid As String
        Private _s_typename As String
        Private _s_typepid As String
        Private _s_typeremark As String
        Public Sub New()
            _s_typeid = Nothing
            _s_typename = Nothing
            _s_typepid = Nothing
            _s_typeremark = Nothing
        End Sub


        ''' <summary>
        ''' �����������N��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property S_TypeID() As String
            Get
                Return _s_typeid
            End Get
            Set(ByVal value As String)
                _s_typeid = value
            End Set
        End Property


        ''' <summary>
        ''' �����ӦW��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property S_TypeName() As String
            Get
                Return _s_typename
            End Get
            Set(ByVal value As String)
                _s_typename = value
            End Set
        End Property


        ''' <summary>
        ''' ����ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property S_TypePID() As String
            Get
                Return _s_typepid
            End Get
            Set(ByVal value As String)
                _s_typepid = value
            End Set
        End Property

        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property S_TypeRemark()
            Get
                Return _s_typeremark
            End Get
            Set(ByVal value)
                _s_typeremark = value
            End Set
        End Property
    End Class
End Namespace
