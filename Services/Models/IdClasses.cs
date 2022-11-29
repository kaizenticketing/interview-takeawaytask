namespace Ticketing.Services;

// NOTE: these are part of the data api now - eg. quotas use these to indicate dimensions of quotas AND entitlements refer to them
// so these CANNOT be changed
public static class IdClasses
{
	// common
	public const string BroadcastBatch = "brdba";

	// organisations
	public const string Organisation = "organ";
	public const string Country = "cntry";
	public const string Channel = "chanl";
	public const string MethodOfPayment = "mop";
	public const string PaymentProviderConfig = "pprov";
	public const string MethodOfDelivery = "mod";
	public const string EntryMedia = "emedi";
	public const string Workstation = "works";
	public const string User = "user";
	public const string Entitlement = "entlm";
	public const string TicketTemplate = "ttmpl";
	public const string DocumentBatch = "docu";
	public const string TicketTemplateSet = "ttset";
	public const string TaxBand = "tax";
	public const string NotificationTemplate = "nttmp";

	// manifests
	public const string VenueConfiguration = "vncnf";
	public const string VenueConfigurationPart = "mpart";
	public const string Segment = "seg";
	public const string SegmentConfiguration = "segcfg";
	public const string Hold = "hold";
	public const string Gate = "gate";
	public const string Turnstile = "turn";
	public const string Entrance = "entr";
	public const string Stair = "stai";
	public const string Aisle = "aisl";
	public const string Note = "note";

	// catalog
	public const string PriceType = "prtyp";
	public const string PriceCategory = "prcat";
	public const string Attraction = "atrct";
	public const string Venue = "venue";
	public const string Brand = "brand";
	public const string Tour = "tour";
	public const string ProductCategory = "pdcat";
	public const string Product = "prdct";
	public const string ProductPossibleCriteriaGroup = "ppcg";
	public const string PackageComponentGroup = "pkcgp";
	public const string PackageComponent = "pkcmp";
	public const string InventorySearchGroup = "pkisg";
	public const string Price = "price";
	public const string Offer = "offer";
	public const string Adjustment = "adjst";
	public const string DeliveryOption = "delop";
	public const string PaymentOption = "payop";
	public const string Onsale = "onsal";
	public const string GatesOpen = "gatop";

	// inventory
	public const string InventorySet = "inset";
	public const string Inventory = "inv";
	public const string InventorySearch = "invsr";

	// carts
	public const string Cart = "cart";
	public const string CartItem = "ctitm";
	public const string CartItemSpecification = "ispec";

	// customers
	public const string Account = "accnt"; // fixed
	public const string Profile = "prfle";
	public const string AppliedRestriction = "arst";
	public const string RestrictionReason = "rrsn";
	public const string AppliedTag = "atag";
	public const string Login = "login";
	public const string Address = "addss";
	public const string CustomerTagType = "ctagt";
	public const string Consent = "cnsen";
	public const string StoredInstrument = "stins";

	// orders
	public const string Order = "order";
	public const string Delivery = "dlvry";
	public const string OrderLine = "oline";
	public const string AppliedAdjustment = "apadj";
	public const string Payment = "paymt";
	public const string Transaction = "tx";
	public const string LedgerEntry = "ledgr";

	// access
	public const string AccessToken = "actok";
	public const string EntryCode = "ecode";
	public const string ScanHistory = "scan";

	// surveys
	public const string Survey = "survy";
	public const string SurveyResponse = "sursp";

	// quotas
	public const string Quota = "quota";
	public const string QuotaLimit = "qlimt";
	public const string QuotaGroup = "qugrp";

	// promotions
	public const string Campaign = "cmpgn";
	public const string PromotionCode = "promo";

	// batches
	public const string Batch = "batch";
	public const string BatchEntry = "betry";

	// reporting
	public const string DocumentTemplate = "dctmp";

	// integrations
	public const string IntegrationConfiguration = "incfg";
}
