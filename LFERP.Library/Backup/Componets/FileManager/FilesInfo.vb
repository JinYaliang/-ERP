Namespace LFERP.FileManager
    Public Class FilesInfo
        Private _f_no As String
        Private _f_name As String
        Private _f_detail As String
        Private _f_adddate As String
        Private _f_oldname As String
        Private _f_newname As String
        Private _f_filetype As String
        Private _f_filepath As String
        Private _f_remark As String
        Private _f_action As String
        Private _f_typeid As String
        Private _fb_type As String
        Private _fb_typeno As String
        Public Sub New()
            _f_no = Nothing
            _f_name = Nothing
            _f_detail = Nothing
            _f_adddate = Nothing
            _f_newname = Nothing
            _f_oldname = Nothing
            _f_filetype = Nothing
            _f_filepath = Nothing
            _f_remark = Nothing
            _f_action = Nothing
            _f_typeid = Nothing
            _fb_type = Nothing
            _fb_typeno = Nothing

        End Sub

        ''' <summary>
        ''' ���s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property F_No() As String
            Get
                Return _f_no
            End Get
            Set(ByVal value As String)
                _f_no = value
            End Set
        End Property
        ''' <summary>
        ''' ���W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property F_Name() As String
            Get
                Return _f_name
            End Get
            Set(ByVal value As String)
                _f_name = value
            End Set
        End Property
        ''' <summary>
        ''' ��󤺮e
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property F_Detail() As String
            Get
                Return _f_detail
            End Get
            Set(ByVal value As String)
                _f_detail = value
            End Set
        End Property
        ''' <summary>
        ''' �إߤ��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property F_AddDate() As String
            Get
                Return _f_adddate
            End Get
            Set(ByVal value As String)
                _f_adddate = value
            End Set
        End Property
        ''' <summary>
        ''' ��l�W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property F_OldName() As String
            Get
                Return _f_oldname
            End Get
            Set(ByVal value As String)
                _f_oldname = value
            End Set
        End Property
        ''' <summary>
        ''' �s�W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property F_NewName() As String
            Get
                Return _f_newname
            End Get
            Set(ByVal value As String)
                _f_newname = value
            End Set
        End Property
        ''' <summary>
        ''' ���Z��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property F_FileType() As String
            Get
                Return _f_filetype
            End Get
            Set(ByVal value As String)
                _f_filetype = value
            End Set
        End Property
        ''' <summary>
        ''' �s��a�}
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property F_FilePath() As String
            Get
                Return _f_filepath
            End Get
            Set(ByVal value As String)
                _f_filepath = value
            End Set
        End Property
        ''' <summary>
        ''' �Ƶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property F_Remark() As String
            Get
                Return _f_remark
            End Get
            Set(ByVal value As String)
                _f_remark = value
            End Set
        End Property
        ''' <summary>
        ''' �ާ@��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property F_Action() As String
            Get
                Return _f_action
            End Get
            Set(ByVal value As String)
                _f_action = value
            End Set
        End Property
        ''' <summary>
        ''' ������
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property F_TypeID() As String
            Get
                Return _f_typeid
            End Get
            Set(ByVal value As String)
                _f_typeid = value
            End Set
        End Property
        ''' <summary>
        ''' �Ҷ��s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property FB_Type() As String
            Get
                Return _fb_type
            End Get
            Set(ByVal value As String)
                _fb_type = value
            End Set
        End Property
        ''' <summary>
        ''' �Ҷ������O���s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property FB_TypeNo() As String
            Get
                Return _fb_typeno
            End Get
            Set(ByVal value As String)
                _fb_typeno = value
            End Set
        End Property















    End Class
End Namespace

