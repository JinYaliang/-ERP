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

        '2014-05-13 姚駿
        Private _CVSName As String
        Private _CreateDate As String
        Private _UploadDate As String
        Private _Flag As Boolean

        Private _OperationUserID As String
        Private _SP_LC As String
        Private _SP_BigBox As String

        Private _FTPServer As String
        Private _IPAddress As String
        Private _FTPuser As String
        Private _FTPPassWord As String
        Private _FTPRemoteDir As String
        Private _FTPPort As String

        Private _FTPFileName As String
        Private _SP_CardID As String
        '------袁毅龙-----20140721
        Private _SendBoxSupplyNum1 As String
        Private _SendBoxSupplyNum2 As String
        Private _SenBoxSupplyNum As String
        Private _SO_CusterPO As String
        Private _SO_CusterNo As String
        '  ---------------

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

            '2014-05-13   姚駿
            _CVSName = Nothing
            _CreateDate = Nothing
            _UploadDate = Nothing
            _Flag = Nothing

            _SP_LC = Nothing
            _SP_BigBox = Nothing


            _FTPServer = Nothing
            _IPAddress = Nothing
            _FTPuser = Nothing
            _FTPPort = Nothing
            _FTPPassWord = Nothing
            _FTPRemoteDir = Nothing
            _OperationUserID = Nothing
            _FTPFileName = Nothing
            _SP_CardID = Nothing
            '-------------袁毅龍20140721
            _SendBoxSupplyNum1 = Nothing
            _SendBoxSupplyNum2 = Nothing
            _SenBoxSupplyNum = Nothing
            _SO_CusterPO = Nothing
            _SO_CusterNo = Nothing
            ' -----------------------------
        End Sub
        Public Property SO_CusterNo() As String
            Get
                Return _SO_CusterNo
            End Get
            Set(ByVal value As String)
                _SO_CusterNo = value
            End Set
        End Property

        Public Property SP_CardID() As String
            Get
                Return _SP_CardID
            End Get
            Set(ByVal value As String)
                _SP_CardID = value
            End Set
        End Property
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

        '2014-05-13 姚駿
        Public Property CVSName() As String
            Get
                Return _CVSName
            End Get
            Set(ByVal value As String)
                _CVSName = value
            End Set
        End Property

        Public Property CreateDate() As String
            Get
                Return _CreateDate
            End Get
            Set(ByVal value As String)
                _CreateDate = value
            End Set
        End Property

        Public Property UploadDate() As String
            Get
                Return _UploadDate
            End Get
            Set(ByVal value As String)
                _UploadDate = value
            End Set
        End Property

        Public Property Flag() As String
            Get
                Return _Flag
            End Get
            Set(ByVal value As String)
                _Flag = value
            End Set
        End Property

        Public Property SP_LC() As String
            Get
                Return _SP_LC
            End Get
            Set(ByVal value As String)
                _SP_LC = value
            End Set
        End Property

        Public Property SP_BigBox() As String
            Get
                Return _SP_BigBox
            End Get
            Set(ByVal value As String)
                _SP_BigBox = value
            End Set
        End Property

        Public Property FTPServer() As String
            Get
                Return _FTPServer
            End Get
            Set(ByVal value As String)
                _FTPServer = value
            End Set
        End Property



        Public Property IPAddress() As String
            Get
                Return _IPAddress
            End Get
            Set(ByVal value As String)
                _IPAddress = value
            End Set
        End Property

        Public Property FTPuser() As String
            Get
                Return _FTPuser
            End Get
            Set(ByVal value As String)
                _FTPuser = value
            End Set
        End Property

        Public Property FTPPassWord() As String
            Get
                Return _FTPPassWord
            End Get
            Set(ByVal value As String)
                _FTPPassWord = value
            End Set
        End Property

        Public Property FTPRemoteDir() As String
            Get
                Return _FTPRemoteDir
            End Get
            Set(ByVal value As String)
                _FTPRemoteDir = value
            End Set
        End Property

        Public Property FTPPort() As String
            Get
                Return _FTPPort
            End Get
            Set(ByVal value As String)
                _FTPPort = value
            End Set
        End Property


        Public Property OperationUserID() As String
            Get
                Return _OperationUserID
            End Get
            Set(ByVal value As String)
                _OperationUserID = value
            End Set
        End Property

        Public Property FTPFileName() As String
            Get
                Return _FTPFileName
            End Get
            Set(ByVal value As String)
                _FTPFileName = value
            End Set
        End Property

        Public Property SendBoxSupplyNum1() As String
            Get
                Return _SendBoxSupplyNum1
            End Get
            Set(ByVal value As String)
                _SendBoxSupplyNum1 = value
            End Set
        End Property

        Public Property SendBoxSupplyNum2() As String
            Get
                Return _SendBoxSupplyNum2
            End Get
            Set(ByVal value As String)
                _SendBoxSupplyNum2 = value
            End Set
        End Property
        Public Property SenBoxSupplyNum() As String
            Get
                Return _SenBoxSupplyNum
            End Get
            Set(ByVal value As String)
                _SenBoxSupplyNum = value
            End Set
        End Property
        Public Property SO_CusterPO() As String
            Get
                Return _SO_CusterPO
            End Get
            Set(ByVal value As String)
                _SO_CusterPO = value
            End Set
        End Property

    End Class
End Namespace