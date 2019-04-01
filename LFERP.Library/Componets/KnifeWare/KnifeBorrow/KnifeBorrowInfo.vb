Namespace LFERP.Library.KnifeWare
    Public Class KnifeBorrowInfo
        Private _AutoID As Int32      '�۰ʽs��ID
        Private _B_Num As String     '�ɤM�y����
        Private _B_NO As String     '�ɤM�渹
        Private _M_Code As String     '�M��s�X
        Private _WH_ID As String     '�ܮw�N��
        Private _BPer_ID As String     '�ɤM�H
        Private _BPer_Name As String     '�ɤM�m�W
        Private _B_Qty As Double      '�ɤM��
        Private _R_Qty As Double     '�٤M��
        Private _B_Date As Date     '�ɤM���
        Private _R_Date As String     '�٤M���
        Private _B_Action As String     '�ާ@��
        Private _B_Remark As String     '�ƪ`
        Private _G_NO As String     '�էO�s��
        Private _B_Type As String     '�o�M�ݩ�
        Private _M_Name As String     '�M��W��---------------
        Private _WH_Name As String     '�ܮw�W��
        Private _WH_PName As String     '���ܮw�W��
        Private _B_ActionName As String     '�ާ@���W��
        Private _G_Name As String     '�էO�W��
        Private _B_EndQty As Double
        Private _M_Gauge As String
        Private _NOReturn As Double
        Private _B_AllEndQty As Double
        Private _Type3ID As String
        Private _Type3Name As String
        Private _M_Unit As String

        '2014-05-28  ���@
        Private _FacName As String
        Private _DepName As String



        Sub New()
            _AutoID = 0
            _B_Num = Nothing
            _B_NO = Nothing
            _M_Code = Nothing
            _WH_ID = Nothing
            _BPer_ID = Nothing
            _BPer_Name = Nothing
            _B_Qty = 0
            _R_Qty = 0
            _B_Date = Nothing
            _R_Date = Nothing
            _B_Action = Nothing
            _B_Remark = Nothing
            _G_NO = Nothing
            _B_Type = Nothing
            _M_Name = Nothing
            _WH_Name = Nothing
            _WH_PName = Nothing
            _B_ActionName = Nothing
            _G_Name = Nothing
            _B_EndQty = 0
            _M_Gauge = Nothing
            _NOReturn = 0
            _B_AllEndQty = 0

            _Type3ID = Nothing
            _Type3Name = Nothing
            _M_Unit = Nothing

            '2014-05-28  ���@
            _FacName = Nothing
            _DepName = Nothing
         
        End Sub

        Public Property M_Unit() As String
            Get
                Return _M_Unit
            End Get
            Set(ByVal value As String)
                _M_Unit = value
            End Set
        End Property

        Public Property Type3ID() As String
            Get
                Return _Type3ID
            End Get
            Set(ByVal value As String)
                _Type3ID = value
            End Set
        End Property

        Public Property Type3Name() As String
            Get
                Return _Type3Name
            End Get
            Set(ByVal value As String)
                _Type3Name = value
            End Set
        End Property


        Public Property B_AllEndQty() As Double
            Get
                Return _B_AllEndQty
            End Get
            Set(ByVal value As Double)
                _B_AllEndQty = value
            End Set
        End Property


        Public Property NOReturn() As Double
            Get
                Return _NOReturn
            End Get
            Set(ByVal value As Double)
                _NOReturn = value
            End Set
        End Property
        ''' <summary>
        ''' �۰ʽs��ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AutoID() As Int32
            Get
                Return _AutoID
            End Get
            Set(ByVal value As Int32)
                _AutoID = value
            End Set
        End Property


        ''' <summary>
        ''' �ɤM�y����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property B_Num() As String
            Get
                Return _B_Num
            End Get
            Set(ByVal value As String)
                _B_Num = value
            End Set
        End Property


        ''' <summary>
        ''' �ɤM�渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property B_NO() As String
            Get
                Return _B_NO
            End Get
            Set(ByVal value As String)
                _B_NO = value
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
        ''' �ɤM�H
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BPer_ID() As String
            Get
                Return _BPer_ID
            End Get
            Set(ByVal value As String)
                _BPer_ID = value
            End Set
        End Property


        ''' <summary>
        ''' �ɤM�m�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BPer_Name() As String
            Get
                Return _BPer_Name
            End Get
            Set(ByVal value As String)
                _BPer_Name = value
            End Set
        End Property


        ''' <summary>
        ''' �ɤM��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property B_Qty() As Double
            Get
                Return _B_Qty
            End Get
            Set(ByVal value As Double)
                _B_Qty = value
            End Set
        End Property


        ''' <summary>
        ''' �٤M��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property R_Qty() As Double
            Get
                Return _R_Qty
            End Get
            Set(ByVal value As Double)
                _R_Qty = value
            End Set
        End Property


        ''' <summary>
        ''' �ɤM���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property B_Date() As Date
            Get
                Return _B_Date
            End Get
            Set(ByVal value As Date)
                _B_Date = value
            End Set
        End Property


        ''' <summary>
        ''' �٤M���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property R_Date() As String
            Get
                Return _R_Date
            End Get
            Set(ByVal value As String)
                _R_Date = value
            End Set
        End Property


        ''' <summary>
        ''' �ާ@��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property B_Action() As String
            Get
                Return _B_Action
            End Get
            Set(ByVal value As String)
                _B_Action = value
            End Set
        End Property


        ''' <summary>
        ''' �ƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property B_Remark() As String
            Get
                Return _B_Remark
            End Get
            Set(ByVal value As String)
                _B_Remark = value
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
        ''' �o�M�ݩ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property B_Type() As String
            Get
                Return _B_Type
            End Get
            Set(ByVal value As String)
                _B_Type = value
            End Set
        End Property


        ''' <summary>
        ''' �M��W��---------------
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property M_Name() As String
            Get
                Return _M_Name
            End Get
            Set(ByVal value As String)
                _M_Name = value
            End Set
        End Property


        ''' <summary>
        ''' �ܮw�W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property WH_Name() As String
            Get
                Return _WH_Name
            End Get
            Set(ByVal value As String)
                _WH_Name = value
            End Set
        End Property


        ''' <summary>
        ''' ���ܮw�W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property WH_PName() As String
            Get
                Return _WH_PName
            End Get
            Set(ByVal value As String)
                _WH_PName = value
            End Set
        End Property


        ''' <summary>
        ''' �ާ@���W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property B_ActionName() As String
            Get
                Return _B_ActionName
            End Get
            Set(ByVal value As String)
                _B_ActionName = value
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

        '
        Public Property B_EndQty() As Double
            Get
                Return _B_EndQty
            End Get
            Set(ByVal value As Double)
                _B_EndQty = value
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

        Public Property FacName() As String
            Get
                Return _FacName
            End Get
            Set(ByVal value As String)
                _FacName = value
            End Set
        End Property

        Public Property DepName() As String
            Get
                Return _DepName
            End Get
            Set(ByVal value As String)
                _DepName = value
            End Set
        End Property
    End Class
End Namespace
