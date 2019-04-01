Namespace LFERP.Library.KnifeWare
    Public Class KnifeChangeInfo
        Private _AutoID As String     '�۰ʽs��ID
        Private _CH_Num As String     '����y����
        Private _BR_NO As String     '�w�����(�٤M�O����)
        Private _M_Code As String     '�M��s�X
        Private _WH_ID As String     '�ܮw�N��
        Private _CBegin_Qty As Integer     '�ק�e�ƶq
        Private _CEnd_Qty As Integer     '�ק�Z�ƶq
        Private _CKType As String     '��������()
        Private _CReMark As String     '�ƪ`
        Private _C_Action As String     '�ާ@��
        Private _C_Date As String     '�ާ@��

        Private _WH_Name As String
        Private _WH_PName As String
        Private _C_ActionName As String

        Private _M_Name As String
        Private _M_Gauge As String

        Private _ChangeID As String
        Private _ChangeName As String

        Private _CKTypeName As String     '��������()

        Private _BRPer_ID As String
        Private _BRPer_Name As String



        Sub New()
            _AutoID = Nothing
            _CH_Num = Nothing
            _BR_NO = Nothing
            _M_Code = Nothing
            _WH_ID = Nothing
            _CBegin_Qty = 0
            _CEnd_Qty = 0
            _CKType = Nothing
            _CReMark = Nothing
            _C_Action = Nothing
            _C_Date = Nothing

            _C_ActionName = Nothing
            _WH_Name = Nothing
            _WH_PName = Nothing

            _M_Name = Nothing
            _M_Gauge = Nothing

            _ChangeID = Nothing
            _ChangeName = Nothing
            _CKTypeName = Nothing
            _BRPer_ID = Nothing
            _BRPer_Name = Nothing
        End Sub '

        Public Property BRPer_ID() As String
            Get
                Return _BRPer_ID
            End Get
            Set(ByVal value As String)
                _BRPer_ID = value
            End Set
        End Property


        Public Property BRPer_Name() As String
            Get
                Return _BRPer_Name
            End Get
            Set(ByVal value As String)
                _BRPer_Name = value
            End Set
        End Property


        Public Property CKTypeName() As String
            Get
                Return _CKTypeName
            End Get
            Set(ByVal value As String)
                _CKTypeName = value
            End Set
        End Property

        Public Property ChangeID() As String
            Get
                Return _ChangeID
            End Get
            Set(ByVal value As String)
                _ChangeID = value
            End Set
        End Property

        Public Property ChangeName() As String
            Get
                Return _ChangeName
            End Get
            Set(ByVal value As String)
                _ChangeName = value
            End Set
        End Property

        ''' <summary>
        ''' �۰ʽs��ID
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
        ''' ����y����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CH_Num() As String
            Get
                Return _CH_Num
            End Get
            Set(ByVal value As String)
                _CH_Num = value
            End Set
        End Property


        ''' <summary>
        ''' �w�����(�٤M�O����)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BR_NO() As String
            Get
                Return _BR_NO
            End Get
            Set(ByVal value As String)
                _BR_NO = value
            End Set
        End Property


        ''' <summary>
        ''' �M��s�X
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property M_Code() As String
            Get
                Return _M_Code
            End Get
            Set(ByVal value As String)
                _M_Code = value
            End Set
        End Property


        ''' <summary>
        ''' �ܮw�N��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property WH_ID() As String
            Get
                Return _WH_ID
            End Get
            Set(ByVal value As String)
                _WH_ID = value
            End Set
        End Property


        ''' <summary>
        ''' �ק�e�ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CBegin_Qty() As Integer
            Get
                Return _CBegin_Qty
            End Get
            Set(ByVal value As Integer)
                _CBegin_Qty = value
            End Set
        End Property


        ''' <summary>
        ''' �ק�Z�ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CEnd_Qty() As Integer
            Get
                Return _CEnd_Qty
            End Get
            Set(ByVal value As Integer)
                _CEnd_Qty = value
            End Set
        End Property


        ''' <summary>
        ''' ��������()
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CKType() As String
            Get
                Return _CKType
            End Get
            Set(ByVal value As String)
                _CKType = value
            End Set
        End Property


        ''' <summary>
        ''' �ƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CReMark() As String
            Get
                Return _CReMark
            End Get
            Set(ByVal value As String)
                _CReMark = value
            End Set
        End Property


        ''' <summary>
        ''' �ާ@��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_Action() As String
            Get
                Return _C_Action
            End Get
            Set(ByVal value As String)
                _C_Action = value
            End Set
        End Property


        ''' <summary>
        ''' �ާ@��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_Date() As String
            Get
                Return _C_Date
            End Get
            Set(ByVal value As String)
                _C_Date = value
            End Set
        End Property

        Public Property C_ActionName() As String
            Get
                Return _C_ActionName
            End Get
            Set(ByVal value As String)
                _C_ActionName = value
            End Set
        End Property

        Public Property WH_Name() As String
            Get
                Return _WH_Name
            End Get
            Set(ByVal value As String)
                _WH_Name = value
            End Set
        End Property

        Public Property WH_PName() As String
            Get
                Return _WH_PName
            End Get
            Set(ByVal value As String)
                _WH_PName = value
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

        Public Property M_Gauge() As String
            Get
                Return _M_Gauge
            End Get
            Set(ByVal value As String)
                _M_Gauge = value
            End Set
        End Property

    End Class
End Namespace