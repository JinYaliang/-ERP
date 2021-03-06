Namespace LFERP.Library.SampleManager.SampleSend

    ''' <summary>
    ''' 样办寄送表
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SampleSendInfo
        Private _SP_ID As String
        Private _SO_ID As String
        Private _SS_Edition As String
        Private _SP_Qty As Integer
        Private _SP_CusterID As String
        Private _SP_SendDate As Date
        Private _M_Code As String
        Private _PM_M_Code As String
        Private _PS_NO As String
        Private _SP_AddUserID As String
        Private _SP_AddUserName As String
        Private _SP_AddDate As Date
        Private _SP_ModifyUserID As String
        Private _SP_ModifyDate As Date
        Private _M_Name As String
        Private _AutoID As String
        Private _PM_M_Name As String
        Private _CO_ID As String

        Private _SP_CheckUserID As String
        Private _SP_CheckUserName As String
        Private _SP_Check As Boolean
        Private _SP_CheckDate As Date
        Private _SP_CheckRemark As String

        Private _SP_InCheckUserID As String
        Private _SP_InCheckUserName As String
        Private _SP_InCheck As Boolean
        Private _SP_InCheckDate As Date
        Private _SP_InCheckRemark As String

        Private _SP_Remark As String
        Private _C_ChsName As String
        Private _SP_ExpDeliveryID As String '快递单号
        Private _SP_ExpCompany As String '快递公司
        Private _SO_SampleID As String
        Private _PK_Code_ID As String

        Public Sub New()
            _SP_ExpDeliveryID = Nothing
            _SP_ExpCompany = Nothing
            _SP_ID = Nothing
            _SO_ID = Nothing
            _SS_Edition = Nothing
            _SP_Qty = Nothing
            _SP_CusterID = Nothing
            _SP_SendDate = Nothing
            _M_Code = Nothing
            _PM_M_Code = Nothing
            _PS_NO = Nothing
            _SP_AddUserID = Nothing
            _SP_AddUserName = Nothing
            _SP_AddDate = Nothing
            _SP_ModifyUserID = Nothing
            _SP_ModifyDate = Nothing
            _M_Name = Nothing
            _AutoID = Nothing
            _PM_M_Name = Nothing
            _CO_ID = Nothing

            _SP_CheckUserID = Nothing
            _SP_CheckUserName = Nothing
            _SP_Check = False
            _SP_CheckDate = Nothing
            _SP_CheckRemark = Nothing

            _SP_InCheckUserID = Nothing
            _SP_InCheckUserName = Nothing
            _SP_InCheck = False
            _SP_InCheckDate = Nothing
            _SP_InCheckRemark = Nothing

            _SP_Remark = Nothing
            _C_ChsName = Nothing
            _SO_SampleID = Nothing
            _PK_Code_ID = Nothing
        End Sub
        Public Property PK_Code_ID() As String
            Get
                Return _PK_Code_ID
            End Get
            Set(ByVal value As String)
                _PK_Code_ID = value
            End Set
        End Property
        Public Property SO_SampleID() As String
            Get
                Return _SO_SampleID
            End Get
            Set(ByVal value As String)
                _SO_SampleID = value
            End Set
        End Property

        Public Property SP_ExpDeliveryID() As String
            Get
                Return _SP_ExpDeliveryID
            End Get
            Set(ByVal value As String)
                _SP_ExpDeliveryID = value
            End Set
        End Property
        Public Property SP_ExpCompany() As String
            Get
                Return _SP_ExpCompany
            End Get
            Set(ByVal value As String)
                _SP_ExpCompany = value
            End Set
        End Property

        Public Property C_ChsName() As String
            Get
                Return _C_ChsName
            End Get
            Set(ByVal value As String)
                _C_ChsName = value
            End Set
        End Property
        Public Property SP_Remark() As String
            Get
                Return _SP_Remark
            End Get
            Set(ByVal value As String)
                _SP_Remark = value
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
        Public Property PM_M_Name() As String
            Get
                Return _PM_M_Name
            End Get
            Set(ByVal value As String)
                _PM_M_Name = value
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
        Public Property CO_ID() As String
            Get
                Return _CO_ID
            End Get
            Set(ByVal value As String)
                _CO_ID = value
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

        Public Property SP_Qty() As Integer
            Get
                Return _SP_Qty
            End Get
            Set(ByVal value As Integer)
                _SP_Qty = value
            End Set
        End Property
        Public Property SP_CusterID() As String
            Get
                Return _SP_CusterID
            End Get
            Set(ByVal value As String)
                _SP_CusterID = value
            End Set
        End Property
        Public Property SP_SendDate() As String
            Get
                Return _SP_SendDate
            End Get
            Set(ByVal value As String)
                _SP_SendDate = value
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
        Public Property SP_AddUserID() As String
            Get
                Return _SP_AddUserID
            End Get
            Set(ByVal value As String)
                _SP_AddUserID = value
            End Set
        End Property
        Public Property SP_AddDate() As String
            Get
                Return _SP_AddDate
            End Get
            Set(ByVal value As String)
                _SP_AddDate = value
            End Set
        End Property
        Public Property SP_ModifyUserID() As String
            Get
                Return _SP_ModifyUserID
            End Get
            Set(ByVal value As String)
                _SP_ModifyUserID = value
            End Set
        End Property
        Public Property SP_AddUserName() As String
            Get
                Return _SP_AddUserName
            End Get
            Set(ByVal value As String)
                _SP_AddUserName = value
            End Set
        End Property
        Public Property SP_ModifyDate() As String
            Get
                Return _SP_ModifyDate
            End Get
            Set(ByVal value As String)
                _SP_ModifyDate = value
            End Set
        End Property
        Public Property SP_CheckRemark() As String
            Get
                Return _SP_CheckRemark
            End Get
            Set(ByVal value As String)
                _SP_CheckRemark = value
            End Set
        End Property
        Public Property SP_CheckDate() As Date
            Get
                Return _SP_CheckDate
            End Get
            Set(ByVal value As Date)
                _SP_CheckDate = value
            End Set
        End Property
        Public Property SP_CheckUserID() As String
            Get
                Return _SP_CheckUserID
            End Get
            Set(ByVal value As String)
                _SP_CheckUserID = value
            End Set
        End Property
        Public Property SP_CheckUserName() As String
            Get
                Return _SP_CheckUserName
            End Get
            Set(ByVal value As String)
                _SP_CheckUserName = value
            End Set
        End Property

        Public Property SP_Check() As Boolean
            Get
                Return _SP_Check
            End Get
            Set(ByVal value As Boolean)
                _SP_Check = value
            End Set
        End Property

        Public Property SP_InCheckRemark() As String
            Get
                Return _SP_InCheckRemark
            End Get
            Set(ByVal value As String)
                _SP_InCheckRemark = value
            End Set
        End Property
        Public Property SP_InCheckDate() As Date
            Get
                Return _SP_InCheckDate
            End Get
            Set(ByVal value As Date)
                _SP_InCheckDate = value
            End Set
        End Property
        Public Property SP_InCheckUserID() As String
            Get
                Return _SP_InCheckUserID
            End Get
            Set(ByVal value As String)
                _SP_InCheckUserID = value
            End Set
        End Property
        Public Property SP_InCheckUserName() As String
            Get
                Return _SP_InCheckUserName
            End Get
            Set(ByVal value As String)
                _SP_InCheckUserName = value
            End Set
        End Property

        Public Property SP_InCheck() As Boolean
            Get
                Return _SP_InCheck
            End Get
            Set(ByVal value As Boolean)
                _SP_InCheck = value
            End Set
        End Property


        Public Property PS_NO() As String
            Get
                Return _PS_NO
            End Get
            Set(ByVal value As String)
                _PS_NO = value
            End Set
        End Property
    End Class




End Namespace