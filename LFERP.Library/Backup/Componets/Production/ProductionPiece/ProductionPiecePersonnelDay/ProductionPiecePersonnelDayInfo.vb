Namespace LFERP.Library.ProductionPiecePersonnelDay

    Public Class ProductionPiecePersonnelDayInfo
        Private _AutoID As String
        Private _Per_NO As String '              *  nvarchar(50)                  /���u�u��
        Private _Per_Name As String '            *  nvarchar(50)                  /���u�W��
        Private _G_NO As String '             *  nvarchar(50)                  /�էO�s��
        Private _DepID As String '             *  nvarchar(50)                  /�����s��
        Private _FacID As String '             *  nvarchar(50)                  /�t�O
        Private _Per_PayType As String '        *  nvarchar(50)                  /�~������
        Private _Per_Date As String      '      *  datetime                      /�إߤ��
        Private _Per_Action As String ''       *  nvarchar(50)                  /�ާ@�H
        Private _Per_Remark As String       ' *  nvarchar(MAX)                 /�ƪ`
        Private _Per_Resign As Boolean         ' *  bit                           /�O�_�w��u
        Private _Per_DayPrice As Double '���~

        Private _Per_ActionName As String '  �H���򥻫H���H�W
        Private _Per_G_Name As String '      �էO�W��'
        Private _Per_DepName As String '  �����W��
        Private _Per_FacName As String '�t�O�W��

        '
        Private _Now_Date As String  ''���s���u�W��ɪ����
        Private _Load_Date As String '�n�D�ɤJ�����w���

        Private _Per_Num As String

        Private _Per_NOName As String  '�u�� �m�W

        Private _Per_Class As String    '�Z��

        Sub New()
            _Per_NO = Nothing
            _Per_Name = Nothing
            _G_NO = Nothing
            _DepID = Nothing
            _FacID = Nothing

            _Per_PayType = Nothing
            _Per_Date = Nothing
            _Per_Action = Nothing
            _Per_Remark = Nothing

            _Per_Resign = False
            _Per_ActionName = Nothing
            _Per_G_Name = Nothing
            _Per_DepName = Nothing

            _Per_FacName = Nothing
            _Per_DayPrice = 0
            _AutoID = Nothing

            _Per_Num = Nothing
            _Per_NOName = Nothing

            _Per_Class = Nothing

        End Sub
        ''' <summary>
        ''' �u��-�m�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_NOName() As String
            Get
                Return _Per_NOName
            End Get
            Set(ByVal value As String)
                _Per_NOName = value
            End Set
        End Property

        ''' <summary>
        ''' �P�@��ɤJ�����u�۽s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_Num() As String
            Get
                Return _Per_Num
            End Get
            Set(ByVal value As String)
                _Per_Num = value
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
        ''' <summary>
        ''' ���u�s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_NO() As String
            Get
                Return _Per_NO
            End Get
            Set(ByVal value As String)
                _Per_NO = value
            End Set
        End Property

        ''' <summary>
        ''' ���u�W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_Name() As String
            Get
                Return _Per_Name
            End Get
            Set(ByVal value As String)
                _Per_Name = value
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
        ''' �^������
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_PayType() As String
            Get
                Return _Per_PayType
            End Get
            Set(ByVal value As String)
                _Per_PayType = value
            End Set
        End Property

        ''' <summary>
        ''' �� �߮ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_Date() As String
            Get
                Return _Per_Date
            End Get
            Set(ByVal value As String)
                _Per_Date = value
            End Set
        End Property
        ''' <summary>
        ''' �ާ@�����X
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_Action() As String
            Get
                Return _Per_Action
            End Get
            Set(ByVal value As String)
                _Per_Action = value
            End Set
        End Property
        ''' <summary>
        ''' �ƪ`�H��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_Remark() As String
            Get
                Return _Per_Remark
            End Get
            Set(ByVal value As String)
                _Per_Remark = value
            End Set
        End Property
        ''' <summary>
        ''' �O�_�w��u
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_Resign() As Boolean
            Get
                Return _Per_Resign
            End Get
            Set(ByVal value As Boolean)
                _Per_Resign = value
            End Set
        End Property
        '--------------------�s���r�q

        ''' <summary>
        '''    'Per_ActionName  �H���򥻫H���H�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_ActionName() As String
            Get
                Return _Per_ActionName
            End Get
            Set(ByVal value As String)
                _Per_ActionName = value
            End Set
        End Property

        ''' <summary>
        '''Per_G_Name      �էO�W��'
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_G_Name() As String
            Get
                Return _Per_G_Name
            End Get
            Set(ByVal value As String)
                _Per_G_Name = value
            End Set
        End Property

        ''' <summary>
        '''     'Per_DPT_ID_Name  �����W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_DepName() As String
            Get
                Return _Per_DepName
            End Get
            Set(ByVal value As String)
                _Per_DepName = value
            End Set
        End Property
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_FacName() As String
            Get
                Return _Per_FacName
            End Get
            Set(ByVal value As String)
                _Per_FacName = value
            End Set
        End Property
        ''' <summary>
        ''' ���~
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_DayPrice() As Double
            Get
                Return _Per_DayPrice
            End Get
            Set(ByVal value As Double)
                _Per_DayPrice = value
            End Set
        End Property

        'Private _Now_Date As String  ''���s���u�W��ɪ����
        'Private _Load_Date As String '�n�D�ɤJ�����w���
        ''' <summary>
        ''' �n�D�ɤJ���u�H�����ɶ��A�@�묰��e���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Now_Date() As String
            Get
                Return _Now_Date
            End Get
            Set(ByVal value As String)
                _Now_Date = value
            End Set
        End Property

        ''' <summary>
        ''' �Q�ɤJ�����u�H������A�@�묰�e�@�u�@��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Load_Date() As String
            Get
                Return _Load_Date
            End Get
            Set(ByVal value As String)
                _Load_Date = value
            End Set
        End Property
        ''' <summary>
        ''' �Z��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_Class() As String
            Get
                Return _Per_Class
            End Get
            Set(ByVal value As String)
                _Per_Class = value
            End Set
        End Property

    End Class
End Namespace