Namespace LFERP.SystemManager.UserLoginLog
    Public Class UserLoginLogInfo
        Private _U_ID As String
        Private _U_Name As String
        Private _UL_InTime As Date
        Private _UL_ExitTime As Date
        Private _UL_Mac As String
        Private _UL_IP As String



        ''test

        Public Sub New()
            _U_ID = Nothing
            _U_Name = Nothing
            _UL_InTime = Nothing
            _UL_ExitTime = Nothing
            _UL_Mac = Nothing
            _UL_IP = Nothing
        End Sub
        ''' <summary>
        ''' �Τ�s��
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
        ''' �Τ�W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property U_Name() As String
            Get
                Return _U_Name
            End Get
            Set(ByVal value As String)
                _U_Name = value
            End Set
        End Property
        ''' <summary>
        ''' �Τ�n�J�ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UL_InTime() As Date
            Get
                Return _UL_InTime
            End Get
            Set(ByVal value As Date)
                _UL_InTime = value
            End Set
        End Property
        '''<summary>
        ''' �Τ�n�X�ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UL_ExitTime() As Date
            Get
                Return _UL_ExitTime
            End Get
            Set(ByVal value As Date)
                _UL_ExitTime = value
            End Set
        End Property
        ''' <summary>
        ''' �Τ�n���q����MAC�a�}
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UL_Mac() As String
            Get
                Return _UL_Mac
            End Get
            Set(ByVal value As String)
                _UL_Mac = value
            End Set
        End Property
        ''' <summary>
        ''' �Τ�n���q����IP�a�}
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UL_IP() As String
            Get
                Return _UL_IP
            End Get
            Set(ByVal value As String)
                _UL_IP = value
            End Set
        End Property
   
    End Class
End Namespace



