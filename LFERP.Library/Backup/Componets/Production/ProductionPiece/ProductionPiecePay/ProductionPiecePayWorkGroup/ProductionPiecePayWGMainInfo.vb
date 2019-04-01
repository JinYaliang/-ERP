Namespace LFERP.Library.ProductionPiecePayWGMain

    Public Class ProductionPiecePayWGMainInfo
        Private _PY_ID As String     '�渹
        Private _G_NO As String     '�էO�s��
        Private _PY_YYMM As String     '�~��
        Private _PY_CompleteSum As Double     '�����`���B
        Private _PY_UseSum As Double     '�ӥ��`���B
        Private _PY_TimeAllSum As Double     '�p���`���B
        Private _PY_PieceAllSum As Double     '�p���`���B
        Private _PY_CompensateSum As Double     '���ɪ��B
        Private _PY_SubtractSum As Double     '�������B
        Private _PY_BonusSum As Double     '����
        Private _PY_Remark As String     '�ƪ`
        Private _PY_Check As Boolean      '�f��
        Private _PY_CheckUserID As String     '�f�ֽs��
        Private _PY_CheckDate As String     '�f�֤��
        Private _PY_AddUserID As String     '�K�[�H(�ާ@�H)�s��
        Private _PY_AddDate As String     '�K�[���
        Private _PY_ModifyUserID As String     '�ק�H
        Private _PY_ModifyDate As String     '�ק���
        Private _DepID As String     '����
        Private _FacID As String     '�t�O

        Private _PY_CheckUserName As String     '�f�֤H�m�W
        Private _PY_ModifyUserName As String     '�ק�H�m�W
        Private _PY_DepName As String     '�����W��
        Private _PY_FacName As String     '�t�O�W��

        Private _AutoID As String
        Private _PY_TimeSum As Double     '�p�ɪ��B
        Private _PY_PieceSum As Double     '�p����B

        Private _PY_G_Name As String '�էO�s��

        Private _PWGtotal_P As Double '�d�߲έp�X���w�էO�Y�@����p���`�B
        Private _PWGtotal_T As Double '�d�߲έp�X���w�էO�Y�@����p���`�B

        Private _PY_AddUserName As String

        Private _G_NO_OUTSum As Double '              --  �էO��X
        Private _G_NO_InSum As Double '              --  �էO��J   


        Sub New()
            _PY_AddUserName = Nothing

            _PWGtotal_P = 0
            _PWGtotal_T = 0

            _PY_ID = Nothing
            _G_NO = Nothing
            _PY_YYMM = Nothing
            _PY_CompleteSum = 0
            _PY_UseSum = 0
            _PY_TimeSum = 0
            _PY_PieceSum = 0
            _PY_CompensateSum = 0
            _PY_SubtractSum = 0
            _PY_BonusSum = 0
            _PY_Remark = Nothing
            _PY_Check = Nothing
            _PY_CheckUserID = Nothing
            _PY_CheckDate = Nothing
            _PY_AddUserID = Nothing
            _PY_AddDate = Nothing
            _PY_ModifyUserID = Nothing
            _PY_ModifyDate = Nothing
            _DepID = Nothing
            _FacID = Nothing

            _PY_CheckUserName = Nothing
            _PY_ModifyUserName = Nothing
            _PY_DepName = Nothing
            _PY_FacName = Nothing

            _AutoID = Nothing
            _PY_G_Name = Nothing

            _G_NO_OUTSum = 0
            _G_NO_InSum = 0

        End Sub

        ''' <summary>
        ''' ��X�էO���B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_NO_OUTSum() As Double
            Get
                Return _G_NO_OUTSum
            End Get
            Set(ByVal value As Double)
                _G_NO_OUTSum = value
            End Set
        End Property

        ''' <summary>
        ''' ��J�էO���B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_NO_InSum() As Double
            Get
                Return _G_NO_InSum
            End Get
            Set(ByVal value As Double)
                _G_NO_InSum = value
            End Set
        End Property


        Public Property PY_AddUserName() As String
            Get
                Return _PY_AddUserName
            End Get
            Set(ByVal value As String)
                _PY_AddUserName = value
            End Set
        End Property

        ''' <summary>
        ''' �d�߲έp�X���w�էO�Y�@����p���`�B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PWGtotal_P() As Double
            Get
                Return _PWGtotal_P
            End Get
            Set(ByVal value As Double)
                _PWGtotal_P = value
            End Set
        End Property
        ''' <summary>
        ''' �d�߲έp�X���w�էO�Y�@����p���`�B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PWGtotal_T() As Double
            Get
                Return _PWGtotal_T
            End Get
            Set(ByVal value As Double)
                _PWGtotal_T = value
            End Set
        End Property


        ''' <summary>
        ''' �էO�s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_G_Name() As String
            Get
                Return _PY_G_Name
            End Get
            Set(ByVal value As String)
                _PY_G_Name = value
            End Set
        End Property
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
        ''' �渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_ID() As String
            Get
                Return _PY_ID
            End Get
            Set(ByVal value As String)
                _PY_ID = value
            End Set
        End Property


        ''' <summary>
        ''' �էO�s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_NO() As String
            Get
                Return _G_NO
            End Get
            Set(ByVal value As String)
                _G_NO = value
            End Set
        End Property


        ''' <summary>
        ''' �~��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_YYMM() As String
            Get
                Return _PY_YYMM
            End Get
            Set(ByVal value As String)
                _PY_YYMM = value
            End Set
        End Property


        ''' <summary>
        ''' �����`���B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_CompleteSum() As Double
            Get
                Return _PY_CompleteSum
            End Get
            Set(ByVal value As Double)
                _PY_CompleteSum = value
            End Set
        End Property


        ''' <summary>
        ''' �ӥ��`���B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_UseSum() As Double
            Get
                Return _PY_UseSum
            End Get
            Set(ByVal value As Double)
                _PY_UseSum = value
            End Set
        End Property


        ''' <summary>
        ''' �p���`���B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_TimeAllSum() As Double
            Get
                Return _PY_TimeAllSum
            End Get
            Set(ByVal value As Double)
                _PY_TimeAllSum = value
            End Set
        End Property


        ''' <summary>
        ''' �p���`���B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_PieceAllSum() As Double
            Get
                Return _PY_PieceAllSum
            End Get
            Set(ByVal value As Double)
                _PY_PieceAllSum = value
            End Set
        End Property

        ''' <summary>
        ''' �p���`���B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_TimeSum() As Double
            Get
                Return _PY_TimeSum
            End Get
            Set(ByVal value As Double)
                _PY_TimeSum = value
            End Set
        End Property


        ''' <summary>
        ''' �p���`���B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_PieceSum() As Double
            Get
                Return _PY_PieceSum
            End Get
            Set(ByVal value As Double)
                _PY_PieceSum = value
            End Set
        End Property


        ''' <summary>
        ''' ���ɪ��B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_CompensateSum() As Double
            Get
                Return _PY_CompensateSum
            End Get
            Set(ByVal value As Double)
                _PY_CompensateSum = value
            End Set
        End Property


        ''' <summary>
        ''' �������B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_SubtractSum() As Double
            Get
                Return _PY_SubtractSum
            End Get
            Set(ByVal value As Double)
                _PY_SubtractSum = value
            End Set
        End Property


        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_BonusSum() As Double
            Get
                Return _PY_BonusSum
            End Get
            Set(ByVal value As Double)
                _PY_BonusSum = value
            End Set
        End Property


        ''' <summary>
        ''' �ƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_Remark() As String
            Get
                Return _PY_Remark
            End Get
            Set(ByVal value As String)
                _PY_Remark = value
            End Set
        End Property


        ''' <summary>
        ''' �f��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_Check() As Boolean
            Get
                Return _PY_Check
            End Get
            Set(ByVal value As Boolean)
                _PY_Check = value
            End Set
        End Property


        ''' <summary>
        ''' �f�ֽs��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_CheckUserID() As String
            Get
                Return _PY_CheckUserID
            End Get
            Set(ByVal value As String)
                _PY_CheckUserID = value
            End Set
        End Property


        ''' <summary>
        ''' �f�֤��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_CheckDate() As String
            Get
                Return _PY_CheckDate
            End Get
            Set(ByVal value As String)
                _PY_CheckDate = value
            End Set
        End Property


        ''' <summary>
        ''' �K�[�H(�ާ@�H)�s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_AddUserID() As String
            Get
                Return _PY_AddUserID
            End Get
            Set(ByVal value As String)
                _PY_AddUserID = value
            End Set
        End Property


        ''' <summary>
        ''' �K�[���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_AddDate() As String
            Get
                Return _PY_AddDate
            End Get
            Set(ByVal value As String)
                _PY_AddDate = value
            End Set
        End Property


        ''' <summary>
        ''' �ק�H
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_ModifyUserID() As String
            Get
                Return _PY_ModifyUserID
            End Get
            Set(ByVal value As String)
                _PY_ModifyUserID = value
            End Set
        End Property


        ''' <summary>
        ''' �ק���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_ModifyDate() As String
            Get
                Return _PY_ModifyDate
            End Get
            Set(ByVal value As String)
                _PY_ModifyDate = value
            End Set
        End Property


        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DepID() As String
            Get
                Return _DepID
            End Get
            Set(ByVal value As String)
                _DepID = value
            End Set
        End Property


        ''' <summary>
        ''' �t�O
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
        ''' �f�֤H�m�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_CheckUserName() As String
            Get
                Return _PY_CheckUserName
            End Get
            Set(ByVal value As String)
                _PY_CheckUserName = value
            End Set
        End Property


        ''' <summary>
        ''' �ק�H�m�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_ModifyUserName() As String
            Get
                Return _PY_ModifyUserName
            End Get
            Set(ByVal value As String)
                _PY_ModifyUserName = value
            End Set
        End Property


        ''' <summary>
        ''' �����W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_DepName() As String
            Get
                Return _PY_DepName
            End Get
            Set(ByVal value As String)
                _PY_DepName = value
            End Set
        End Property


        ''' <summary>
        ''' �t�O�W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_FacName() As String
            Get
                Return _PY_FacName
            End Get
            Set(ByVal value As String)
                _PY_FacName = value
            End Set
        End Property

    End Class
End Namespace