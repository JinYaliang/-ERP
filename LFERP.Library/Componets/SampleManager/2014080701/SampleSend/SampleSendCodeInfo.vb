Namespace LFERP.Library.SampleManager.SampleSend
    ''' <summary>
    ''' 样办条码
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SampleSendCodeInfo
        Private _SP_ID As String
        Private _SO_ID As String
        Private _SS_Edition As String
        Private _PM_M_Code As String
        Private _Code_ID As String
        Private _Code_Qty As Integer

        Private _AddUserID As String
        Private _AddUserName As String
        Private _AddDate As Date
        Private _ModifyUserID As String
        Private _ModifyDate As Date
        Private _AutoID As String
        Private _CodeType As String
        Public Sub New()
            _SP_ID = Nothing
            _SO_ID = Nothing
            _SS_Edition = Nothing
            _PM_M_Code = Nothing
            _Code_ID = Nothing
            _Code_Qty = Nothing

            _AddUserID = Nothing
            _AddDate = Nothing
            _ModifyUserID = Nothing
            _ModifyDate = Nothing
            _AutoID = Nothing
            _CodeType = Nothing
        End Sub
        Public Property CodeType() As String
            Get
                Return _CodeType
            End Get
            Set(ByVal value As String)
                _CodeType = value
            End Set
        End Property
        Public Property SP_ID() As String
            Get
                Return _SP_ID
            End Get
            Set(ByVal value As String)
                _SP_ID = value
            End Set
        End Property
        Public Property PM_M_Code() As String
            Get
                Return _PM_M_Code
            End Get
            Set(ByVal value As String)
                _PM_M_Code = value
            End Set
        End Property

        Public Property SO_ID() As String
            Get
                Return _SO_ID
            End Get
            Set(ByVal value As String)
                _SO_ID = value
            End Set
        End Property
        Public Property Code_ID() As String
            Get
                Return _Code_ID
            End Get
            Set(ByVal value As String)
                _Code_ID = value
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

        Public Property Code_Qty() As Integer
            Get
                Return _Code_Qty
            End Get
            Set(ByVal value As Integer)
                _Code_Qty = value
            End Set
        End Property

        Public Property SS_Edition() As String
            Get
                Return _SS_Edition
            End Get
            Set(ByVal value As String)
                _SS_Edition = value
            End Set
        End Property
        Public Property AddUserID() As String
            Get
                Return _AddUserID
            End Get
            Set(ByVal value As String)
                _AddUserID = value
            End Set
        End Property
        Public Property AddDate() As String
            Get
                Return _AddDate
            End Get
            Set(ByVal value As String)
                _AddDate = value
            End Set
        End Property
        Public Property ModifyUserID() As String
            Get
                Return _ModifyUserID
            End Get
            Set(ByVal value As String)
                _ModifyUserID = value
            End Set
        End Property
        Public Property ModifyDate() As String
            Get
                Return _ModifyDate
            End Get
            Set(ByVal value As String)
                _ModifyDate = value
            End Set
        End Property
    End Class

End Namespace