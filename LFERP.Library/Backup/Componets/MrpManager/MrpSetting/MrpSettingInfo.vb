Namespace LFERP.Library.MrpManager.MrpSetting
    Public Class MrpSettingInfo
        Private _AutoID As Decimal
        Private _U_ID As String
        Private _forecastBeginDate As DateTime
        Private _forecastCheckType As String
        Private _forecasstCancellation As String
        Private _forecastCreateUserID As String
        Private _forecastDisplayNum As Decimal
        Private _forecastBrowserBeginDate As DateTime
        Private _forecastBrowserEndDate As DateTime
        Private _forecastBrowserDisplayNum As Decimal
        Private _bomBeginDate As DateTime
        Private _bomProductType As String
        Private _bomCheckType As String
        Private _bomDisplayType As String
        Private _bomCreateUserID As String
        Private _bomDisplayNum As Decimal
        Private _materialBeginDate As DateTime
        Private _materialCheckType As String
        Private _materialWarehouse As String
        Private _materialCreateUserID As String
        Private _materialDisplayNum As Decimal
        Private _warehouseBeginDate As DateTime
        Private _warehouseCheckType As String
        Private _warehouseCreateUserID As String
        Private _warehouseDisplayNum As Decimal
        Private _mrpInfoBeginDate As DateTime
        Private _mrpInfoCheckType As String
        Private _mrpInfoMrpType As String
        Private _mrpInfoCreateUserID As String
        Private _mrpInfoForecastCancellation As String
        Private _mrpInfoForecastCheckType As String
        Private _mrpInfoDisplayNum As Decimal
        Private _needQtyEnable As Boolean
        Private _inventoryQtyEnable As Boolean
        Private _retreatQtyEnable As Boolean
        Private _inTransitQtyEnable As Boolean
        Private _inspectionEnable As Boolean
        Private _noCollarEnable As Boolean
        Private _relatedQtyEnable As Boolean
        Private _secInvEnable As Boolean
        Private _lowLimitEnable As Boolean
        Private _batFixConomyEnable As Boolean
        Private _purchaseBeginDate As Date
        Private _purchaseCheckType As String
        Private _purchaseCreateUserID As String
        Private _purchaseDisplayNum As Decimal
        Private _purOrderBeginDate As Date
        Private _purOrderCheckType As String
        Private _purOrderReCheckType As String
        Private _purOrderRequestDept As String
        Private _purOrderSupplierID As String
        Private _purOrderCreateUserID As String
        Private _purOrderDisplayNum As Decimal
        Private _createDate As DateTime
        Private _modifyDate As DateTime
        Private _ProductionBeginDate As Date
        Private _ProductionEndDate As Date
        Private _MrpMpsBeginDate As Date
        Private _MrpMpsCheckType As String
        Private _MrpMpsCreateUserID As String
        Private _mrpMpsDisplayNum As Decimal
        Private _mrpMpsBrowserBeginDate As Date
        Private _mrpMpsBrowserEndDate As Date
        Private _mrpMpsBrowserDisplayNum As Decimal
        Public Sub New()
            _AutoID = 0
            _U_ID = Nothing
            _forecastBeginDate = Nothing
            _forecastCheckType = Nothing
            _forecasstCancellation = Nothing
            _forecastCreateUserID = Nothing
            _forecastDisplayNum = 0
            _forecastBrowserBeginDate = Nothing
            _forecastBrowserEndDate = Nothing
            _forecastBrowserDisplayNum = 0
            _bomBeginDate = Nothing
            _bomProductType = Nothing
            _bomCheckType = Nothing
            _bomDisplayType = Nothing
            _bomCreateUserID = Nothing
            _bomDisplayNum = 0
            _materialBeginDate = Nothing
            _materialCheckType = Nothing
            _materialWarehouse = Nothing
            _materialCreateUserID = Nothing
            _materialDisplayNum = 0
            _warehouseBeginDate = Nothing
            _warehouseCheckType = Nothing
            _warehouseCreateUserID = Nothing
            _warehouseDisplayNum = 0
            _mrpInfoBeginDate = Nothing
            _mrpInfoCheckType = Nothing
            _mrpInfoMrpType = Nothing
            _mrpInfoCreateUserID = Nothing
            _mrpInfoForecastCancellation = Nothing
            _mrpInfoForecastCheckType = Nothing
            _mrpInfoDisplayNum = 0
            _needQtyEnable = False
            _inventoryQtyEnable = False
            _retreatQtyEnable = False
            _inTransitQtyEnable = False
            _inspectionEnable = False
            _noCollarEnable = False
            _relatedQtyEnable = False
            _secInvEnable = False
            _lowLimitEnable = False
            _batFixConomyEnable = False
            _purchaseBeginDate = Nothing
            _purchaseCheckType = Nothing
            _purchaseCreateUserID = Nothing
            _purchaseDisplayNum = 0
            _purOrderBeginDate = Nothing
            _purOrderCheckType = Nothing
            _purOrderReCheckType = Nothing
            _purOrderRequestDept = Nothing
            _purOrderSupplierID = Nothing
            _purOrderCreateUserID = Nothing
            _purOrderDisplayNum = 0
            _createDate = Nothing
            _modifyDate = Nothing
            _ProductionBeginDate = Nothing
            _ProductionEndDate = Nothing
            _MrpMpsBeginDate = Nothing
            _MrpMpsCheckType = Nothing
            _MrpMpsCreateUserID = Nothing
            _mrpMpsDisplayNum = 0
            _mrpMpsBrowserBeginDate = Nothing
            _mrpMpsBrowserEndDate = Nothing
            _mrpMpsBrowserDisplayNum = 0
        End Sub
        Public Property mrpMpsBrowserDisplayNum() As Decimal
            Get
                Return _mrpMpsBrowserDisplayNum
            End Get
            Set(ByVal value As Decimal)
                _mrpMpsBrowserDisplayNum = value
            End Set
        End Property
        Public Property mrpMpsBrowserBeginDate() As Date
            Get
                Return _mrpMpsBrowserBeginDate
            End Get
            Set(ByVal value As DateTime)
                _mrpMpsBrowserBeginDate = value
            End Set
        End Property
        Public Property mrpMpsBrowserEndDate() As Date
            Get
                Return _mrpMpsBrowserEndDate
            End Get
            Set(ByVal value As DateTime)
                _mrpMpsBrowserEndDate = value
            End Set
        End Property
        Public Property MrpMpsBeginDate() As Date
            Get
                Return _MrpMpsBeginDate
            End Get
            Set(ByVal value As DateTime)
                _MrpMpsBeginDate = value
            End Set
        End Property
        Public Property MrpMpsCheckType() As String
            Get
                Return _MrpMpsCheckType
            End Get
            Set(ByVal value As String)
                _MrpMpsCheckType = value
            End Set
        End Property
        Public Property MrpMpsCreateUserID() As String
            Get
                Return _MrpMpsCreateUserID
            End Get
            Set(ByVal value As String)
                _MrpMpsCreateUserID = value
            End Set
        End Property
        Public Property MrpMpsDisplayNum() As Decimal
            Get
                Return _mrpMpsDisplayNum
            End Get
            Set(ByVal value As Decimal)
                _mrpMpsDisplayNum = value
            End Set
        End Property
        Public Property AutoID() As Decimal
            Get
                Return _AutoID
            End Get
            Set(ByVal value As Decimal)
                _AutoID = value
            End Set
        End Property
        Public Property U_ID() As String
            Get
                Return _U_ID
            End Get
            Set(ByVal value As String)
                _U_ID = value
            End Set
        End Property
        Public Property forecastBeginDate() As DateTime
            Get
                Return _forecastBeginDate
            End Get
            Set(ByVal value As DateTime)
                _forecastBeginDate = value
            End Set
        End Property
        Public Property forecastCheckType() As String
            Get
                Return _forecastCheckType
            End Get
            Set(ByVal value As String)
                _forecastCheckType = value
            End Set
        End Property
        Public Property forecasstCancellation() As String
            Get
                Return _forecasstCancellation
            End Get
            Set(ByVal value As String)
                _forecasstCancellation = value
            End Set
        End Property
        Public Property forecastCreateUserID() As String
            Get
                Return _forecastCreateUserID
            End Get
            Set(ByVal value As String)
                _forecastCreateUserID = value
            End Set
        End Property
        
        Public Property forecastDisplayNum() As Decimal
            Get
                Return _forecastDisplayNum
            End Get
            Set(ByVal value As Decimal)
                _forecastDisplayNum = value
            End Set
        End Property
        Public Property forecastBrowserBeginDate() As DateTime
            Get
                Return _forecastBrowserBeginDate
            End Get
            Set(ByVal value As DateTime)
                _forecastBrowserBeginDate = value
            End Set
        End Property
        Public Property forecastBrowserEndDate() As DateTime
            Get
                Return _forecastBrowserEndDate
            End Get
            Set(ByVal value As DateTime)
                _forecastBrowserEndDate = value
            End Set
        End Property
        Public Property forecastBrowserDisplayNum() As Decimal
            Get
                Return _forecastBrowserDisplayNum
            End Get
            Set(ByVal value As Decimal)
                _forecastBrowserDisplayNum = value
            End Set
        End Property
        Public Property bomBeginDate() As DateTime
            Get
                Return _bomBeginDate
            End Get
            Set(ByVal value As DateTime)
                _bomBeginDate = value
            End Set
        End Property
        Public Property bomProductType() As String
            Get
                Return _bomProductType
            End Get
            Set(ByVal value As String)
                _bomProductType = value
            End Set
        End Property
        Public Property bomCheckType() As String
            Get
                Return _bomCheckType
            End Get
            Set(ByVal value As String)
                _bomCheckType = value
            End Set
        End Property
        Public Property bomDisplayType() As String
            Get
                Return _bomDisplayType
            End Get
            Set(ByVal value As String)
                _bomDisplayType = value
            End Set
        End Property
        Public Property bomCreateUserID() As String
            Get
                Return _bomCreateUserID
            End Get
            Set(ByVal value As String)
                _bomCreateUserID = value
            End Set
        End Property
        Public Property bomDisplayNum() As Decimal
            Get
                Return _bomDisplayNum
            End Get
            Set(ByVal value As Decimal)
                _bomDisplayNum = value
            End Set
        End Property
        Public Property materialBeginDate() As DateTime
            Get
                Return _materialBeginDate
            End Get
            Set(ByVal value As DateTime)
                _materialBeginDate = value
            End Set
        End Property
        Public Property materialCheckType() As String
            Get
                Return _materialCheckType
            End Get
            Set(ByVal value As String)
                _materialCheckType = value
            End Set
        End Property
        Public Property materialWarehouse() As String
            Get
                Return _materialWarehouse
            End Get
            Set(ByVal value As String)
                _materialWarehouse = value
            End Set
        End Property
        Public Property materialCreateUserID() As String
            Get
                Return _materialCreateUserID
            End Get
            Set(ByVal value As String)
                _materialCreateUserID = value
            End Set
        End Property
        Public Property materialDisplayNum() As Decimal
            Get
                Return _materialDisplayNum
            End Get
            Set(ByVal value As Decimal)
                _materialDisplayNum = value
            End Set
        End Property
        Public Property warehouseBeginDate() As DateTime
            Get
                Return _warehouseBeginDate
            End Get
            Set(ByVal value As DateTime)
                _warehouseBeginDate = value
            End Set
        End Property
        Public Property warehouseCheckType() As String
            Get
                Return _warehouseCheckType
            End Get
            Set(ByVal value As String)
                _warehouseCheckType = value
            End Set
        End Property
        Public Property warehouseCreateUserID() As String
            Get
                Return _warehouseCreateUserID
            End Get
            Set(ByVal value As String)
                _warehouseCreateUserID = value
            End Set
        End Property
        Public Property warehouseDisplayNum() As Decimal
            Get
                Return _warehouseDisplayNum
            End Get
            Set(ByVal value As Decimal)
                _warehouseDisplayNum = value
            End Set
        End Property
        Public Property mrpInfoBeginDate() As DateTime
            Get
                Return _mrpInfoBeginDate
            End Get
            Set(ByVal value As DateTime)
                _mrpInfoBeginDate = value
            End Set
        End Property
        Public Property mrpInfoCheckType() As String
            Get
                Return _mrpInfoCheckType
            End Get
            Set(ByVal value As String)
                _mrpInfoCheckType = value
            End Set
        End Property
        Public Property mrpInfoMrpType() As String
            Get
                Return _mrpInfoMrpType
            End Get
            Set(ByVal value As String)
                _mrpInfoMrpType = value
            End Set
        End Property
        Public Property mrpInfoCreateUserID() As String
            Get
                Return _mrpInfoCreateUserID
            End Get
            Set(ByVal value As String)
                _mrpInfoCreateUserID = value
            End Set
        End Property
        Public Property mrpInfoForecastCancellation() As String
            Get
                Return _mrpInfoForecastCancellation
            End Get
            Set(ByVal value As String)
                _mrpInfoForecastCancellation = value
            End Set
        End Property
        Public Property mrpInfoForecastCheckType() As String
            Get
                Return _mrpInfoForecastCheckType
            End Get
            Set(ByVal value As String)
                _mrpInfoForecastCheckType = value
            End Set
        End Property
        Public Property mrpInfoDisplayNum() As Decimal
            Get
                Return _mrpInfoDisplayNum
            End Get
            Set(ByVal value As Decimal)
                _mrpInfoDisplayNum = value
            End Set
        End Property
        Public Property needQtyEnable() As Boolean
            Get
                Return _needQtyEnable
            End Get
            Set(ByVal value As Boolean)
                _needQtyEnable = value
            End Set
        End Property
        Public Property inventoryQtyEnable() As Boolean
            Get
                Return _inventoryQtyEnable
            End Get
            Set(ByVal value As Boolean)
                _inventoryQtyEnable = value
            End Set
        End Property
        Public Property retreatQtyEnable() As Boolean
            Get
                Return _retreatQtyEnable
            End Get
            Set(ByVal value As Boolean)
                _retreatQtyEnable = value
            End Set
        End Property
        Public Property inTransitQtyEnable() As Boolean
            Get
                Return _inTransitQtyEnable
            End Get
            Set(ByVal value As Boolean)
                _inTransitQtyEnable = value
            End Set
        End Property
        Public Property inspectionEnable() As Boolean
            Get
                Return _inspectionEnable
            End Get
            Set(ByVal value As Boolean)
                _inspectionEnable = value
            End Set
        End Property
        Public Property noCollarEnable() As Boolean
            Get
                Return _noCollarEnable
            End Get
            Set(ByVal value As Boolean)
                _noCollarEnable = value
            End Set
        End Property

        Public Property relatedQtyEnable() As Boolean
            Get
                Return _relatedQtyEnable
            End Get
            Set(ByVal value As Boolean)
                _relatedQtyEnable = value
            End Set
        End Property
        Public Property secInvEnable() As Boolean
            Get
                Return _secInvEnable
            End Get
            Set(ByVal value As Boolean)
                _secInvEnable = value
            End Set
        End Property
        Public Property lowLimitEnable() As Boolean
            Get
                Return _lowLimitEnable
            End Get
            Set(ByVal value As Boolean)
                _lowLimitEnable = value
            End Set
        End Property
        Public Property batFixConomyEnable() As Boolean
            Get
                Return _batFixConomyEnable
            End Get
            Set(ByVal value As Boolean)
                _batFixConomyEnable = value
            End Set
        End Property
        Public Property purchaseBeginDate() As Date
            Get
                Return _purchaseBeginDate
            End Get
            Set(ByVal value As Date)
                _purchaseBeginDate = value
            End Set
        End Property
        Public Property purchaseCheckType() As String
            Get
                Return _purchaseCheckType
            End Get
            Set(ByVal value As String)
                _purchaseCheckType = value
            End Set
        End Property
        Public Property purchaseCreateUserID() As String
            Get
                Return _purchaseCreateUserID
            End Get
            Set(ByVal value As String)
                _purchaseCreateUserID = value
            End Set
        End Property
        Public Property purchaseDisplayNum() As Decimal
            Get
                Return _purchaseDisplayNum
            End Get
            Set(ByVal value As Decimal)
                _purchaseDisplayNum = value
            End Set
        End Property
        Public Property purOrderBeginDate() As Date
            Get
                Return _purOrderBeginDate
            End Get
            Set(ByVal value As Date)
                _purOrderBeginDate = value
            End Set
        End Property
        Public Property purOrderCheckType() As String
            Get
                Return _purOrderCheckType
            End Get
            Set(ByVal value As String)
                _purOrderCheckType = value
            End Set
        End Property
        Public Property purOrderReCheckType() As String
            Get
                Return _purOrderReCheckType
            End Get
            Set(ByVal value As String)
                _purOrderReCheckType = value
            End Set
        End Property
        Public Property purOrderRequestDept() As String
            Get
                Return _purOrderRequestDept
            End Get
            Set(ByVal value As String)
                _purOrderRequestDept = value
            End Set
        End Property
        Public Property purOrderSupplierID() As String
            Get
                Return _purOrderSupplierID
            End Get
            Set(ByVal value As String)
                _purOrderSupplierID = value
            End Set
        End Property
        Public Property purOrderCreateUserID() As String
            Get
                Return _purOrderCreateUserID
            End Get
            Set(ByVal value As String)
                _purOrderCreateUserID = value
            End Set
        End Property
        Public Property purOrderDisplayNum() As Decimal
            Get
                Return _purOrderDisplayNum
            End Get
            Set(ByVal value As Decimal)
                _purOrderDisplayNum = value
            End Set
        End Property
        Public Property createDate() As DateTime
            Get
                Return _createDate
            End Get
            Set(ByVal value As DateTime)
                _createDate = value
            End Set
        End Property
        Public Property modifyDate() As DateTime
            Get
                Return _modifyDate
            End Get
            Set(ByVal value As DateTime)
                _modifyDate = value
            End Set
        End Property
        Public Property ProductionBeginDate() As Date
            Get
                Return _ProductionBeginDate
            End Get
            Set(ByVal value As Date)
                _ProductionBeginDate = value
            End Set
        End Property
        Public Property ProductionEndDate() As Date
            Get
                Return _ProductionEndDate
            End Get
            Set(ByVal value As Date)
                _ProductionEndDate = value
            End Set
        End Property



    End Class

End Namespace