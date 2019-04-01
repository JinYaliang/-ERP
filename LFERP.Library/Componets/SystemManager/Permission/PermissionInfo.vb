
Namespace LFERP.SystemManager
    Public Class PermissionInfo
        Private _PM_ID As String
        Private _PM_Name As String
        Private _PM_PID As String
        Private _PM_Level As Integer
        Private _PM_Sub As Boolean
        Private _PM_Image As Integer
        Private _PM_SelectImage As Integer
        Private _pmu_value As Boolean
        Private _u_id As String

        Public Sub New()
            _PM_ID = Nothing
            _PM_Name = Nothing
            _PM_PID = Nothing
            _PM_Level = 0
            _PM_Sub = False
            _PM_Image = 0
            _PM_SelectImage = 0
            _pmu_value = False
            _u_id = Nothing
        End Sub

        ''' <summary>
        ''' ����ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PM_ID() As String
            Get
                Return _PM_ID
            End Get
            Set(ByVal value As String)
                _PM_ID = value
            End Set
        End Property

        ''' <summary>
        ''' ���ئW��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PM_Name() As String
            Get
                Return _PM_Name
            End Get
            Set(ByVal value As String)
                _PM_Name = value
            End Set
        End Property

        ''' <summary>
        ''' ���ؤW��ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PM_PID() As String
            Get
                Return _PM_PID
            End Get
            Set(ByVal value As String)
                _PM_PID = value
            End Set
        End Property

        ''' <summary>
        ''' ���ص���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PM_Level() As Integer
            Get
                Return _PM_Level
            End Get
            Set(ByVal value As Integer)
                _PM_Level = value
            End Set
        End Property

        ''' <summary>
        ''' �O�_���l����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PM_Sub() As Boolean
            Get
                Return _PM_Sub
            End Get
            Set(ByVal value As Boolean)
                _PM_Sub = value
            End Set
        End Property

        ''' <summary>
        ''' ����ܮ���ܹϤ��s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PM_Image() As Integer
            Get
                Return _PM_Image
            End Get
            Set(ByVal value As Integer)
                _PM_Image = value
            End Set
        End Property

        ''' <summary>
        ''' �Q��ܮ���ܹϤ��s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PM_SelectImage() As Integer
            Get
                Return _PM_SelectImage
            End Get
            Set(ByVal value As Integer)
                _PM_SelectImage = value
            End Set
        End Property
        ''' <summary>
        ''' �Ҷ��v��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PMU_Value() As Boolean
            Get
                Return _pmu_value
            End Get
            Set(ByVal value As Boolean)
                _pmu_value = value
            End Set
        End Property
        ''' <summary>
        ''' ���u�u��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property U_ID() As String
            Get
                Return _u_id
            End Get
            Set(ByVal value As String)
                _u_id = value
            End Set
        End Property
    End Class
End Namespace
