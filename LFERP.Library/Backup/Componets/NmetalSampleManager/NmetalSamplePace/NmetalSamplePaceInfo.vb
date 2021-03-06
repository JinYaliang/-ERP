Namespace LFERP.Library.NmetalSampleManager.NmetalSamplePace
    Public Class NmetalSamplePaceInfo
        Private _AutoID As String
        Private _SO_ID As String
        Private _SS_Edition As String
        Private _M_Code As String
        Private _PM_M_Code As String
        Private _PS_NO As String
        Private _PS_Name As String
        Private _SE_PaceDescribe As String
        Private _State As String
        Private _SE_AddUserID As String
        Private _SE_AddUserName As String
        Private _SE_AddDate As Date
        Private _SE_ModifyUserID As String
        Private _SE_ModifyDate As Date
        Private _M_Name As String
        '加數量與類型
        Private _SE_Type As String
        Private _SE_TypeName As String
        Private _SE_Qty As Int32

        Private _SE_Check As Boolean
        Private _SE_InCheck As Boolean
        Private _SE_InCheckAction As String
        Private _SE_InCheckActionName As String
        Private _InCheck As Boolean
        Private _InCheckDate As Date

        Private _SE_CheckDate As Date
        Private _SE_CheckAction As String
        Private _SE_CheckActionName As String
        Private _SE_ID As String
        Private _ClientBarcode As String

        Private _SE_OutInType As String '半成品
        Private _SPID As String '半成品
        Private _Code_ID As String
        Private _Qty As Integer

        Private _SE_OutD_ID As String
        Private _SE_OutPS_NO As String
        Private _SE_OutTime As Date
        Private _SE_InD_ID As String
        Private _SE_InPS_NO As String
        Private _SE_InTime As Date
        Private _SE_InD_Dep As String
        Private _SE_OutD_Dep As String
        Private _SE_OutPS_Name As String
        Private _SE_InPS_Name As String
        Private _SE_OutVisible As Boolean
        Private _SE_InVisible As Boolean

        Private _SE_OutEnabled As Boolean
        Private _SE_InEnabled As Boolean
        Private _SE_OutPSEnabled As Boolean
        Private _SE_InPSEnabled As Boolean
        Private _SE_InCheckBarcode As Boolean
        Private _StatusType As String

        Private _SE_OutCardID As String
        Private _SE_InCardID As String
        Private _SE_OutCardIDName As String
        Private _SE_InCardIDName As String
        Private _SE_Remark As String
        Private _SE_LoanID As String
        Private _BarCodeCount As String
        Private _SO_SampleID As String
        Private _SE_BorrowType As String
        Private _MaterialTypeName As String
        Private _PK_Code_ID As String
        Private _SealCode_ID As String
        Private _SE_BarcodeLink As Boolean
        Private _SE_BarCodeType As String
        Private _SE_BarCodeAuto As Boolean
        Private _SE_EditInD_ID As Boolean
        Private _SE_BorrowTime As Integer
        Private _OutWeighing As Decimal
        Private _InWeighing As Decimal
        Private _SE_CodeType As Integer
        Private _SupplierID As String
        Private _ServerIP As String
        Private _DataBaseName As String
        Private _UserID As String
        Private _PassWord As String

        '2014-07-03
        Private _SE_StatusTypeIsNull As String
        Private _OutQty As Integer
        Private _OutWeight As Decimal
        Private _InQty As Integer
        Private _InWeight As Decimal

        Private _InWeighingEnd As Decimal
        Private _OutWeighingEnd As Decimal

        Private _OutQtying As Int16
        Private _InQtying As Int16

        Private _OutQtyingEnd As Int16
        Private _InQtyingEnd As Int16

        Private _SE_IsRweight As Boolean
        Private _SE_IsKailiao As Boolean
        Private _SE_IsComplete As Boolean


        Private _OutEndQty As Int16
        Private _OutEndWeight As Decimal
        Private _OutWightChaYi As Decimal

        Private _SE_OutYCCardID As String ''异常相关
        Private _SE_InYCCardID As String
        Private _OutDwonRate As Decimal
        Private _OutUpRate As Decimal
        Private _InDwonRate As Decimal
        Private _InUpRate As Decimal




        Public Sub New()
            


            _OutQty = 0
            _OutWeight = 0
            _InQty = 0
            _InWeight = 0
            _InWeighingEnd = 0
            _OutWeighingEnd = 0

            _OutQtying = 0
            _InQtying = 0
            _OutQtyingEnd = 0
            _InQtyingEnd = 0
            '------------------------------------------------------------------
            _SE_OutVisible = Nothing
            _SE_InVisible = Nothing

            _SE_OutD_ID = Nothing
            _SE_OutPS_NO = Nothing
            _SE_OutTime = Nothing
            _SE_InD_ID = Nothing
            _SE_InPS_NO = Nothing
            _SE_InTime = Nothing
            _SE_InD_Dep = Nothing
            _SE_OutD_Dep = Nothing
            _SE_OutPS_Name = Nothing
            _SE_InPS_Name = Nothing

            _InCheck = Nothing
            _InCheckDate = Nothing

            _SE_OutCardID = Nothing
            _SE_InCardID = Nothing
            _SE_OutCardIDName = Nothing
            _SE_InCardIDName = Nothing

            _SE_InCheck = Nothing
            _SE_InCheckAction = Nothing
            _SE_InCheckActionName = Nothing

            _AutoID = Nothing
            _SO_ID = Nothing
            _SS_Edition = Nothing
            _M_Code = Nothing
            _PM_M_Code = Nothing
            _PS_NO = Nothing
            _SE_PaceDescribe = Nothing
            _State = Nothing
            _SE_AddUserID = Nothing
            _SE_AddDate = Nothing
            _SE_ModifyUserID = Nothing
            _SE_ModifyDate = Nothing
            _M_Name = Nothing
            _SE_AddUserName = Nothing
            _PS_Name = Nothing


            _SE_Type = Nothing
            _SE_TypeName = Nothing
            _SE_Qty = 0

            _SE_Check = False
            _SE_CheckDate = Nothing
            _SE_CheckAction = Nothing
            _SE_CheckActionName = Nothing
            _SE_ID = Nothing
            _ClientBarcode = Nothing

            _SE_OutInType = Nothing
            _SPID = Nothing
            _Code_ID = Nothing
            _Qty = 0

            _SE_Remark = Nothing
            _SE_LoanID = Nothing
            _BarCodeCount = Nothing

            _SE_OutEnabled = Nothing
            _SE_InEnabled = Nothing
            _SE_OutPSEnabled = Nothing
            _SE_InPSEnabled = Nothing
            _SE_InCheckBarcode = Nothing
            _StatusType = Nothing
            _SO_SampleID = Nothing
            _SE_BorrowType = Nothing
            _MaterialTypeName = Nothing
            _PK_Code_ID = Nothing
            _SealCode_ID = Nothing
            _SE_BarcodeLink = Nothing
            _SE_BarCodeType = Nothing
            _SE_BarCodeAuto = Nothing
            _SE_EditInD_ID = Nothing
            _SE_BorrowTime = Nothing
            _OutWeighing = Nothing
            _InWeighing = Nothing
            _SE_CodeType = Nothing
            _SupplierID = Nothing
            _ServerIP = Nothing
            _DataBaseName = Nothing
            _UserID = Nothing
            _PassWord = Nothing

            _SE_StatusTypeIsNull = False
            _SE_IsRweight = False

            _SE_IsKailiao = False
            _SE_IsComplete = False


            _OutEndQty = 0
            _OutEndWeight = 0
            _OutWightChaYi = 0

            _SE_OutYCCardID = Nothing ''异常相关
            _SE_InYCCardID = Nothing
            _OutDwonRate = 0
            _OutUpRate = 0
            _InDwonRate = 0
            _InUpRate = 0


        End Sub


        Public Property SE_OutYCCardID() As String
            Get
                Return _SE_OutYCCardID
            End Get
            Set(ByVal value As String)
                _SE_OutYCCardID = value
            End Set
        End Property

        Public Property SE_InYCCardID() As String
            Get
                Return _SE_InYCCardID
            End Get
            Set(ByVal value As String)
                _SE_InYCCardID = value
            End Set
        End Property

        Public Property OutDwonRate() As Decimal
            Get
                Return _OutDwonRate
            End Get
            Set(ByVal value As Decimal)
                _OutDwonRate = value
            End Set
        End Property


        Public Property OutUpRate() As Decimal
            Get
                Return _OutUpRate
            End Get
            Set(ByVal value As Decimal)
                _OutUpRate = value
            End Set
        End Property

        Public Property InDwonRate() As Decimal
            Get
                Return _InDwonRate
            End Get
            Set(ByVal value As Decimal)
                _InDwonRate = value
            End Set
        End Property


        Public Property InUpRate() As Decimal
            Get
                Return _InUpRate
            End Get
            Set(ByVal value As Decimal)
                _InUpRate = value
            End Set
        End Property


        ''--------------------------------------------

        Public Property OutWightChaYi() As Decimal
            Get
                Return _OutWightChaYi
            End Get
            Set(ByVal value As Decimal)
                _OutWightChaYi = value
            End Set
        End Property


        Public Property OutEndWeight() As Decimal
            Get
                Return _OutEndWeight
            End Get
            Set(ByVal value As Decimal)
                _OutEndWeight = value
            End Set
        End Property


        Public Property OutEndQty() As Int16
            Get
                Return _OutEndQty
            End Get
            Set(ByVal value As Int16)
                _OutEndQty = value
            End Set
        End Property


        Public Property SE_CodeType() As Integer
            Get
                Return _SE_CodeType
            End Get
            Set(ByVal value As Integer)
                _SE_CodeType = value
            End Set
        End Property

        Public Property InWeighing() As Decimal
            Get
                Return _InWeighing
            End Get
            Set(ByVal value As Decimal)
                _InWeighing = value
            End Set
        End Property
        Public Property OutWeighing() As Decimal
            Get
                Return _OutWeighing
            End Get
            Set(ByVal value As Decimal)
                _OutWeighing = value
            End Set
        End Property
        Public Property SE_BorrowTime() As Integer
            Get
                Return _SE_BorrowTime
            End Get
            Set(ByVal value As Integer)
                _SE_BorrowTime = value
            End Set
        End Property
        Public Property SE_EditInD_ID() As Boolean
            Get
                Return _SE_EditInD_ID
            End Get
            Set(ByVal value As Boolean)
                _SE_EditInD_ID = value
            End Set
        End Property
        Public Property SE_BarCodeAuto() As Boolean
            Get
                Return _SE_BarCodeAuto
            End Get
            Set(ByVal value As Boolean)
                _SE_BarCodeAuto = value
            End Set
        End Property
        Public Property SE_BarCodeType() As String
            Get
                Return _SE_BarCodeType
            End Get
            Set(ByVal value As String)
                _SE_BarCodeType = value
            End Set
        End Property
        Public Property SE_BarcodeLink() As String
            Get
                Return _SE_BarcodeLink
            End Get
            Set(ByVal value As String)
                _SE_BarcodeLink = value
            End Set
        End Property
        Public Property SealCode_ID() As String
            Get
                Return _SealCode_ID
            End Get
            Set(ByVal value As String)
                _SealCode_ID = value
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
        Public Property MaterialTypeName() As String
            Get
                Return _MaterialTypeName
            End Get
            Set(ByVal value As String)
                _MaterialTypeName = value
            End Set
        End Property
        Public Property SE_BorrowType() As String
            Get
                Return _SE_BorrowType
            End Get
            Set(ByVal value As String)
                _SE_BorrowType = value
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
        Public Property StatusType() As String
            Get
                Return _StatusType
            End Get
            Set(ByVal value As String)
                _StatusType = value
            End Set
        End Property
        Public Property SE_InCheckBarcode() As Boolean
            Get
                Return _SE_InCheckBarcode
            End Get
            Set(ByVal value As Boolean)
                _SE_InCheckBarcode = value
            End Set
        End Property

        Public Property SE_OutEnabled() As Boolean
            Get
                Return _SE_OutEnabled
            End Get
            Set(ByVal value As Boolean)
                _SE_OutEnabled = value
            End Set
        End Property
        Public Property SE_InEnabled() As Boolean
            Get
                Return _SE_InEnabled
            End Get
            Set(ByVal value As Boolean)
                _SE_InEnabled = value
            End Set
        End Property
        Public Property SE_InPSEnabled() As Boolean
            Get
                Return _SE_InPSEnabled
            End Get
            Set(ByVal value As Boolean)
                _SE_InPSEnabled = value
            End Set
        End Property
        Public Property SE_OutPSEnabled() As Boolean
            Get
                Return _SE_OutPSEnabled
            End Get
            Set(ByVal value As Boolean)
                _SE_OutPSEnabled = value
            End Set
        End Property
        Public Property BarCodeCount() As String
            Get
                Return _BarCodeCount
            End Get
            Set(ByVal value As String)
                _BarCodeCount = value
            End Set
        End Property

        Public Property SE_Remark() As String
            Get
                Return _SE_Remark
            End Get
            Set(ByVal value As String)
                _SE_Remark = value
            End Set
        End Property
        Public Property SE_LoanID() As String
            Get
                Return _SE_LoanID
            End Get
            Set(ByVal value As String)
                _SE_LoanID = value
            End Set
        End Property

        Public Property SE_OutCardIDName() As String
            Get
                Return _SE_OutCardIDName
            End Get
            Set(ByVal value As String)
                _SE_OutCardIDName = value
            End Set
        End Property
        Public Property SE_InCardIDName() As String
            Get
                Return _SE_InCardIDName
            End Get
            Set(ByVal value As String)
                _SE_InCardIDName = value
            End Set
        End Property

        Public Property SE_OutCardID() As String
            Get
                Return _SE_OutCardID
            End Get
            Set(ByVal value As String)
                _SE_OutCardID = value
            End Set
        End Property

        Public Property SE_InCardID() As String
            Get
                Return _SE_InCardID
            End Get
            Set(ByVal value As String)
                _SE_InCardID = value
            End Set
        End Property

        Public Property InCheck() As Boolean
            Get
                Return _InCheck
            End Get
            Set(ByVal value As Boolean)
                _InCheck = value
            End Set
        End Property

        Public Property InCheckDate() As String
            Get
                Return _InCheckDate
            End Get
            Set(ByVal value As String)
                _InCheckDate = value
            End Set
        End Property

        Public Property SE_OutVisible() As Boolean
            Get
                Return _SE_OutVisible
            End Get
            Set(ByVal value As Boolean)
                _SE_OutVisible = value
            End Set
        End Property
        Public Property SE_InVisible() As Boolean
            Get
                Return _SE_InVisible
            End Get
            Set(ByVal value As Boolean)
                _SE_InVisible = value
            End Set
        End Property


        Public Property SE_InCheck() As Boolean
            Get
                Return _SE_InCheck
            End Get
            Set(ByVal value As Boolean)
                _SE_InCheck = value
            End Set
        End Property
        Public Property SE_IncheckAction() As String
            Get
                Return _SE_InCheckAction
            End Get
            Set(ByVal value As String)
                _SE_InCheckAction = value
            End Set
        End Property
        Public Property SE_InCheckActionName() As String
            Get
                Return _SE_InCheckActionName
            End Get
            Set(ByVal value As String)
                _SE_InCheckActionName = value
            End Set
        End Property

        Public Property SE_InPS_Name() As String
            Get
                Return _SE_InPS_Name
            End Get
            Set(ByVal value As String)
                _SE_InPS_Name = value
            End Set
        End Property
        Public Property SE_OutPS_Name() As String
            Get
                Return _SE_OutPS_Name
            End Get
            Set(ByVal value As String)
                _SE_OutPS_Name = value
            End Set
        End Property


        Public Property SE_OutD_ID() As String
            Get
                Return _SE_OutD_ID
            End Get
            Set(ByVal value As String)
                _SE_OutD_ID = value
            End Set
        End Property
        Public Property SE_OutD_Dep() As String
            Get
                Return _SE_OutD_Dep
            End Get
            Set(ByVal value As String)
                _SE_OutD_Dep = value
            End Set
        End Property

        Public Property SE_OutPS_NO() As String
            Get
                Return _SE_OutPS_NO
            End Get
            Set(ByVal value As String)
                _SE_OutPS_NO = value
            End Set
        End Property
        Public Property SE_OutTime() As Date
            Get
                Return _SE_OutTime
            End Get
            Set(ByVal value As Date)
                _SE_OutTime = value
            End Set
        End Property
        Public Property SE_InD_ID() As String
            Get
                Return _SE_InD_ID
            End Get
            Set(ByVal value As String)
                _SE_InD_ID = value
            End Set
        End Property
        Public Property SE_InD_Dep() As String
            Get
                Return _SE_InD_Dep
            End Get
            Set(ByVal value As String)
                _SE_InD_Dep = value
            End Set
        End Property
        Public Property SE_InPS_NO() As String
            Get
                Return _SE_InPS_NO
            End Get
            Set(ByVal value As String)
                _SE_InPS_NO = value
            End Set
        End Property
        Public Property SE_InTime() As Date
            Get
                Return _SE_InTime
            End Get
            Set(ByVal value As Date)
                _SE_InTime = value
            End Set
        End Property


        Public Property ClientBarcode() As String
            Get
                Return _ClientBarcode
            End Get
            Set(ByVal value As String)
                _ClientBarcode = value
            End Set
        End Property
        Public Property SE_OutInType() As String
            Get
                Return _SE_OutInType
            End Get
            Set(ByVal value As String)
                _SE_OutInType = value
            End Set
        End Property
        Public Property SPID() As String
            Get
                Return _SPID
            End Get
            Set(ByVal value As String)
                _SPID = value
            End Set
        End Property

        Public Property Qty() As String
            Get
                Return _Qty
            End Get
            Set(ByVal value As String)
                _Qty = value
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

        Public Property State() As String
            Get
                Return _State
            End Get
            Set(ByVal value As String)
                _State = value
            End Set
        End Property
        Public Property PS_Name() As String
            Get
                Return _PS_Name
            End Get
            Set(ByVal value As String)
                _PS_Name = value
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
        Public Property SO_ID() As String
            Get
                Return _SO_ID
            End Get
            Set(ByVal value As String)
                _SO_ID = value
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

        Public Property SE_PaceDescribe() As String
            Get
                Return _SE_PaceDescribe
            End Get
            Set(ByVal value As String)
                _SE_PaceDescribe = value
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
        Public Property SE_AddUserID() As String
            Get
                Return _SE_AddUserID
            End Get
            Set(ByVal value As String)
                _SE_AddUserID = value
            End Set
        End Property
        Public Property SE_AddDate() As String
            Get
                Return _SE_AddDate
            End Get
            Set(ByVal value As String)
                _SE_AddDate = value
            End Set
        End Property
        Public Property SE_ModifyUserID() As String
            Get
                Return _SE_ModifyUserID
            End Get
            Set(ByVal value As String)
                _SE_ModifyUserID = value
            End Set
        End Property
        Public Property SE_AddUserName() As String
            Get
                Return _SE_AddUserName
            End Get
            Set(ByVal value As String)
                _SE_AddUserName = value
            End Set
        End Property
        Public Property SE_ModifyDate() As String
            Get
                Return _SE_ModifyDate
            End Get
            Set(ByVal value As String)
                _SE_ModifyDate = value
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

        ''--------------------------

        Public Property SE_Type() As String
            Get
                Return _SE_Type
            End Get
            Set(ByVal value As String)
                _SE_Type = value
            End Set
        End Property

        Public Property SE_TypeName() As String
            Get
                Return _SE_TypeName
            End Get
            Set(ByVal value As String)
                _SE_TypeName = value
            End Set
        End Property

        Public Property SE_Qty() As Int32
            Get
                Return _SE_Qty
            End Get
            Set(ByVal value As Int32)
                _SE_Qty = value
            End Set
        End Property


        Public Property SE_Check() As Boolean
            Get
                Return _SE_Check
            End Get
            Set(ByVal value As Boolean)
                _SE_Check = value
            End Set
        End Property

        Public Property SE_CheckDate() As Date
            Get
                Return _SE_CheckDate
            End Get
            Set(ByVal value As Date)
                _SE_CheckDate = value
            End Set
        End Property

        Public Property SE_CheckAction() As String
            Get
                Return _SE_CheckAction
            End Get
            Set(ByVal value As String)
                _SE_CheckAction = value
            End Set
        End Property

        Public Property SE_CheckActionName() As String
            Get
                Return _SE_CheckActionName
            End Get
            Set(ByVal value As String)
                _SE_CheckActionName = value
            End Set
        End Property

        Public Property SE_ID() As String
            Get
                Return _SE_ID
            End Get
            Set(ByVal value As String)
                _SE_ID = value
            End Set
        End Property

        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(ByVal value As String)
                _SupplierID = value
            End Set
        End Property


        Public Property ServerIP() As String
            Get
                Return _ServerIP
            End Get
            Set(ByVal value As String)
                _ServerIP = value
            End Set
        End Property

        Public Property DataBaseName() As String
            Get
                Return _DataBaseName
            End Get
            Set(ByVal value As String)
                _DataBaseName = value
            End Set
        End Property

        Public Property UserID() As String
            Get
                Return _UserID
            End Get
            Set(ByVal value As String)
                _UserID = value
            End Set
        End Property

        Public Property PassWord() As String
            Get
                Return _PassWord
            End Get
            Set(ByVal value As String)
                _PassWord = value
            End Set
        End Property

        '_SE_StatusTypeIsNull


        Public Property SE_StatusTypeIsNull() As Boolean
            Get
                Return _SE_StatusTypeIsNull
            End Get
            Set(ByVal value As Boolean)
                _SE_StatusTypeIsNull = value
            End Set
        End Property


        Public Property OutQty() As Integer
            Get
                Return _OutQty
            End Get
            Set(ByVal value As Integer)
                _OutQty = value
            End Set
        End Property

        Public Property OutWeight() As Decimal
            Get
                Return _OutWeight
            End Get
            Set(ByVal value As Decimal)
                _OutWeight = value
            End Set
        End Property

        Public Property InQty() As Integer
            Get
                Return _InQty
            End Get
            Set(ByVal value As Integer)
                _InQty = value
            End Set
        End Property


        Public Property InWeight() As Decimal
            Get
                Return _InWeight
            End Get
            Set(ByVal value As Decimal)
                _InWeight = value
            End Set
        End Property


        Public Property InWeighingEnd() As Decimal
            Get
                Return _InWeighingEnd
            End Get
            Set(ByVal value As Decimal)
                _InWeighingEnd = value
            End Set
        End Property

        Public Property OutWeighingEnd() As Decimal
            Get
                Return _OutWeighingEnd
            End Get
            Set(ByVal value As Decimal)
                _OutWeighingEnd = value
            End Set
        End Property


        Public Property OutQtying() As Integer
            Get
                Return _OutQtying
            End Get
            Set(ByVal value As Integer)
                _OutQtying = value
            End Set
        End Property

        Public Property InQtying() As Integer
            Get
                Return _InQtying
            End Get
            Set(ByVal value As Integer)
                _InQtying = value
            End Set
        End Property

        Public Property OutQtyingEnd() As Integer
            Get
                Return _OutQtyingEnd
            End Get
            Set(ByVal value As Integer)
                _OutQtyingEnd = value
            End Set
        End Property

        Public Property InQtyingEnd() As Integer
            Get
                Return _InQtyingEnd
            End Get
            Set(ByVal value As Integer)
                _InQtyingEnd = value
            End Set
        End Property

        ''' <summary>
        ''' 入库是,更新入库重量
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SE_IsRweight() As Boolean
            Get
                Return _SE_IsRweight
            End Get
            Set(ByVal value As Boolean)
                _SE_IsRweight = value
            End Set
        End Property
        ''' <summary>
        ''' 是否開料
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SE_IsKailiao() As Boolean
            Get
                Return _SE_IsKailiao
            End Get
            Set(ByVal value As Boolean)
                _SE_IsKailiao = value
            End Set
        End Property
        ''' <summary>
        ''' 是否完工
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SE_IsComplete() As Boolean
            Get
                Return _SE_IsComplete
            End Get
            Set(ByVal value As Boolean)
                _SE_IsComplete = value
            End Set
        End Property




    End Class
End Namespace



