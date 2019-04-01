Namespace LFERP.Library.ProductionPieceWorkGroup




    Public Class ProductionPieceWorkGroupInfo
        Private _G_NO As String              ' *  nvarchar(50)              /�էO�s��
        Private _G_Name As String            ' *  nvarchar(50)              /�էO�W��
        Private _G_Manager As String '        ' *  nvarchar(50)              /�էO�t�d�H
        Private _DepID As String            ' *  nvarchar(50)              /�����s��
        Private _FacID As String           ' *  nvarchar(50)              /�t�O
        Private _G_Date As Date           ' *  datetime                  /�إߤ��
        Private _G_Action As String          ' *  nvarchar(50)              /�ާ@�H
        Private _G_Remark As String         ' *  nvarchar(MAX)             /�ƪ`
        ''�s���F�~���r�q
        Private _G_ActionName As String '     �ާ@�H���m�W
        Private _G_DepName As String '    �����W��
        Private _G_FacName As String '   �t�O�W��

        ''�Q�ΥΤ�s���o�쳡���s��
        Private _UserName As String
        Private _UserID As String
        Private _UserRank As String
        Private _UserDep_Fac As String '�����s��-�t�O�W-

        Private _G_NOName As String '�էO�s���A�էO�W���p�_��


        Sub New()
            _G_NO = Nothing
            _G_Name = Nothing
            _G_Manager = Nothing
            _G_DepName = Nothing
            _G_FacName = Nothing '
            _G_Date = Nothing
            _G_Action = Nothing
            _G_Remark = Nothing

            _UserName = Nothing
            _UserID = Nothing
            _UserRank = Nothing
            _UserDep_Fac = Nothing
            _G_NOName = Nothing
        End Sub
        ''' <summary>
        ''' �էO�s��  �էO�W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_NOName() As String
            Get
                Return _G_NOName
            End Get
            Set(ByVal value As String)
                _G_NOName = value
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
        ''' �էO�W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_Name() As String
            Get
                Return _G_Name
            End Get
            Set(ByVal value As String)
                _G_Name = value
            End Set
        End Property
        ''' <summary>
        ''' �էO�t�d�H
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_Manager() As String
            Get
                Return _G_Manager
            End Get
            Set(ByVal value As String)
                _G_Manager = value
            End Set
        End Property
        ''' <summary>
        ''' �����s��
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
        ''' �إߤ��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_Date() As Date
            Get
                Return _G_Date
            End Get
            Set(ByVal value As Date)
                _G_Date = value
            End Set
        End Property
        ''' <summary>
        ''' �ާ@�H
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_Action() As String
            Get
                Return _G_Action
            End Get
            Set(ByVal value As String)
                _G_Action = value
            End Set
        End Property
        ''' <summary>
        ''' �ƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_Remark() As String
            Get
                Return _G_Remark
            End Get
            Set(ByVal value As String)
                _G_Remark = value
            End Set
        End Property

        ''�s���F�~���r�q
        'Private _G_ActionName As String '     �ާ@�H���m�W
        'Private _G_DPT_ID_Name As String '    �����W��
        'Private _G_DPT_PID_Name As String '   �t�O�W��
        ''' <summary>
        ''' �ާ@�H���m�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_ActionName() As String
            Get
                Return _G_ActionName
            End Get
            Set(ByVal value As String)
                _G_ActionName = value
            End Set
        End Property
        ''' <summary>
        ''' �����W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_DepName() As String
            Get
                Return _G_DepName
            End Get
            Set(ByVal value As String)
                _G_DepName = value
            End Set
        End Property
        ''' <summary>
        ''' �t�O�W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_FacName() As String
            Get
                Return _G_FacName
            End Get
            Set(ByVal value As String)
                _G_FacName = value
            End Set
        End Property


        ''�Q�ΥΤ�s���o�쳡���s��
        'Private _UserName As String
        ''' <summary>
        ''' �Τ�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UserName() As String
            Get
                Return _UserName
            End Get
            Set(ByVal value As String)
                _UserName = value
            End Set
        End Property
        'Private _UserID As String
        ''' <summary>
        ''' �Τ�s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UserID() As String
            Get
                Return _UserID
            End Get
            Set(ByVal value As String)
                _UserID = value
            End Set
        End Property
        'Private _UserRank As String
        ''' <summary>
        ''' �Τ��v��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UserRank() As String
            Get
                Return _UserRank
            End Get
            Set(ByVal value As String)
                _UserRank = value
            End Set
        End Property
        'Private _UserDep_Fac As String '�����s��-�t�O�W-
        ''' <summary>
        ''' �����s��-�t�O�W-�p
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UserDep_Fac() As String
            Get
                Return _UserDep_Fac
            End Get
            Set(ByVal value As String)
                _UserDep_Fac = value
            End Set
        End Property

    End Class
End Namespace