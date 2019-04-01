Namespace LFERP.Library.ProductionSumTimePersonnel



    Public Class ProductionSumTimePersonnelInfo
        Private _PT_NO As String '             *  nvarchar(50)                /�p�ɳ渹
        Private _Per_NO As String '            *  nvarchar(50)                /���u�s��
        Private _G_NO As String '              *  nvarchar(50)                /�էO�s��
        Private _DepID As String '             *  nvarchar(50)                /�����s��
        Private _FacID As String '             *  nvarchar(50)                /�t�O

        Private _PT_BeginTime As String '       *  nvarchar(50)                /�p�ɶ}�l�ɶ�
        Private _PT_EndTime As String '         *  nvarchar(50)                /�p�ɵ����ɶ�
        Private _PT_Total As Double '         *  float                       /�`�p�ɶ�    
        Private _PT_Date As String '           *  datetime                    /�إߤ��
        Private _PT_Action As String '        *  nvarchar(50)                /�ާ@�H

        Private _PT_Remark As String '         *  nvarchar(MAX)               /�ƪ`

        '�~��r�q
        Private _PT_ActionName As String '  �ާ@���W 
        Private _PT_Per_Name As String '    ���u�W�m�W(ProductionPiecePersonnel)
        Private _PT_DepName As String '     �����W
        Private _PT_FacName As String '     �t�O�W
        Private _PT_G_Name As String '      �էO�W(ProductionPieceWorkGroup)

        Private _PT_DateEnd As String  ''���L�ζ}�l�ɶ�
        Private _PT_DateStart As String
        Private _Print_Action As String

        Private _PP_NO As String

        Private _SampID As String
        Private _SampPrice As Double
        Private _SampName As String '2014-07-24

        Sub New()

            _PT_DateEnd = Nothing
            _PT_DateStart = Nothing
            _Print_Action = Nothing

            _PT_NO = Nothing
            _Per_NO = Nothing
            _G_NO = Nothing
            _DepID = Nothing
            _FacID = Nothing

            _PT_BeginTime = Nothing
            _PT_EndTime = Nothing
            _PT_Total = 0
            _PT_Date = Nothing
            _PT_Action = Nothing

            _PT_Remark = Nothing

            ''�~��
            _PT_ActionName = Nothing
            _PT_Per_Name = Nothing
            _PT_DepName = Nothing
            _PT_FacName = Nothing
            _PT_G_Name = Nothing


            _PP_NO = Nothing

            _SampPrice = 0
            _SampID = Nothing
            _SampName = Nothing

        End Sub

        Public Property SampPrice() As Double
            Get
                Return _SampPrice
            End Get
            Set(ByVal value As Double)
                _SampPrice = value
            End Set
        End Property

        Public Property SampID() As String
            Get
                Return _SampID
            End Get
            Set(ByVal value As String)
                _SampID = value
            End Set
        End Property

        Public Property SampName() As String
            Get
                Return _SampName
            End Get
            Set(ByVal value As String)
                _SampName = value
            End Set
        End Property


        Public Property PP_NO() As String
            Get
                Return _PP_NO
            End Get
            Set(ByVal value As String)
                _PP_NO = value
            End Set
        End Property

        ''' <summary>
        ''' ���L�H�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Print_Action() As String
            Get
                Return _Print_Action
            End Get
            Set(ByVal value As String)
                _Print_Action = value
            End Set
        End Property
        ''' <summary>
        ''' ���L�����ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_DateStart() As String
            Get
                Return _PT_DateStart
            End Get
            Set(ByVal value As String)
                _PT_DateStart = value
            End Set
        End Property
        ''' <summary>
        ''' ���L�ζ}�l�ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_DateEnd() As String
            Get
                Return _PT_DateEnd
            End Get
            Set(ByVal value As String)
                _PT_DateEnd = value
            End Set
        End Property
        ''' <summary>
        ''' �p�ɳ渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_NO() As String
            Get
                Return _PT_NO
            End Get
            Set(ByVal value As String)
                _PT_NO = value
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
        ''' �p�ɶ}�l�ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_BeginTime() As String
            Get
                Return _PT_BeginTime
            End Get
            Set(ByVal value As String)
                _PT_BeginTime = value
            End Set
        End Property
        ''' <summary>
        ''' �p�ɵ����ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_EndTime() As String
            Get
                Return _PT_EndTime
            End Get
            Set(ByVal value As String)
                _PT_EndTime = value
            End Set
        End Property

        ''' <summary>
        ''' �`�p�ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_Total() As Double
            Get
                Return _PT_Total
            End Get
            Set(ByVal value As Double)
                _PT_Total = value
            End Set
        End Property

        ''' <summary>
        ''' �إߤ��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_Date() As String
            Get
                Return _PT_Date
            End Get
            Set(ByVal value As String)
                _PT_Date = value
            End Set
        End Property
        ''' <summary>
        ''' �ާ@�H
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_Action() As String
            Get
                Return _PT_Action
            End Get
            Set(ByVal value As String)
                _PT_Action = value
            End Set
        End Property

        ''' <summary>
        ''' �ƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_Remark() As String
            Get
                Return _PT_Remark
            End Get
            Set(ByVal value As String)
                _PT_Remark = value
            End Set
        End Property
        ''' <summary>
        ''' �ާ@���W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_ActionName() As String
            Get
                Return _PT_ActionName
            End Get
            Set(ByVal value As String)
                _PT_ActionName = value
            End Set
        End Property
        ''' <summary>
        ''' ���u�W�m�W(ProductionPiecePersonnel)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_Per_Name() As String
            Get
                Return _PT_Per_Name
            End Get
            Set(ByVal value As String)
                _PT_Per_Name = value
            End Set
        End Property
        ''' <summary>
        ''' �����W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_DepName() As String
            Get
                Return _PT_DepName
            End Get
            Set(ByVal value As String)
                _PT_DepName = value
            End Set
        End Property
        ''' <summary>
        ''' �t�O�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_FacName() As String
            Get
                Return _PT_FacName
            End Get
            Set(ByVal value As String)
                _PT_FacName = value
            End Set
        End Property
        ''' <summary>
        '''  �էO�W(ProductionPieceWorkGroup)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PT_G_Name() As String
            Get
                Return _PT_G_Name
            End Get
            Set(ByVal value As String)
                _PT_G_Name = value
            End Set
        End Property
    End Class
End Namespace