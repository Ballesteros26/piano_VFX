using System;
using System.Resources;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000047 RID: 71
	public class ExceptionMessages : ResourceManager
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0000D95A File Offset: 0x0000BB5A
		public object[][] getContents()
		{
			return ExceptionMessages.contents;
		}

		// Token: 0x040001AD RID: 429
		[CLSCompliant(false)]
		public const string TOSTRING = "TOSTRING";

		// Token: 0x040001AE RID: 430
		public const string SERVER_MSG = "SERVER_MSG";

		// Token: 0x040001AF RID: 431
		public const string MATCHED_DN = "MATCHED_DN";

		// Token: 0x040001B0 RID: 432
		public const string FAILED_REFERRAL = "FAILED_REFERRAL";

		// Token: 0x040001B1 RID: 433
		public const string REFERRAL_ITEM = "REFERRAL_ITEM";

		// Token: 0x040001B2 RID: 434
		public const string CONNECTION_ERROR = "CONNECTION_ERROR";

		// Token: 0x040001B3 RID: 435
		public const string CONNECTION_IMPOSSIBLE = "CONNECTION_IMPOSSIBLE";

		// Token: 0x040001B4 RID: 436
		public const string CONNECTION_WAIT = "CONNECTION_WAIT";

		// Token: 0x040001B5 RID: 437
		public const string CONNECTION_FINALIZED = "CONNECTION_FINALIZED";

		// Token: 0x040001B6 RID: 438
		public const string CONNECTION_CLOSED = "CONNECTION_CLOSED";

		// Token: 0x040001B7 RID: 439
		public const string CONNECTION_READER = "CONNECTION_READER";

		// Token: 0x040001B8 RID: 440
		public const string DUP_ERROR = "DUP_ERROR";

		// Token: 0x040001B9 RID: 441
		public const string REFERRAL_ERROR = "REFERRAL_ERROR";

		// Token: 0x040001BA RID: 442
		public const string REFERRAL_LOCAL = "REFERRAL_LOCAL";

		// Token: 0x040001BB RID: 443
		public const string REFERENCE_ERROR = "REFERENCE_ERROR";

		// Token: 0x040001BC RID: 444
		public const string REFERRAL_SEND = "REFERRAL_SEND";

		// Token: 0x040001BD RID: 445
		public const string REFERENCE_NOFOLLOW = "REFERENCE_NOFOLLOW";

		// Token: 0x040001BE RID: 446
		public const string REFERRAL_BIND = "REFERRAL_BIND";

		// Token: 0x040001BF RID: 447
		public const string REFERRAL_BIND_MATCH = "REFERRAL_BIND_MATCH";

		// Token: 0x040001C0 RID: 448
		public const string NO_DUP_REQUEST = "NO_DUP_REQUEST";

		// Token: 0x040001C1 RID: 449
		public const string SERVER_CONNECT_ERROR = "SERVER_CONNECT_ERROR";

		// Token: 0x040001C2 RID: 450
		public const string NO_SUP_PROPERTY = "NO_SUP_PROPERTY";

		// Token: 0x040001C3 RID: 451
		public const string ENTRY_PARAM_ERROR = "ENTRY_PARAM_ERROR";

		// Token: 0x040001C4 RID: 452
		public const string DN_PARAM_ERROR = "DN_PARAM_ERROR";

		// Token: 0x040001C5 RID: 453
		public const string RDN_PARAM_ERROR = "RDN_PARAM_ERROR";

		// Token: 0x040001C6 RID: 454
		public const string OP_PARAM_ERROR = "OP_PARAM_ERROR";

		// Token: 0x040001C7 RID: 455
		public const string PARAM_ERROR = "PARAM_ERROR";

		// Token: 0x040001C8 RID: 456
		public const string DECODING_ERROR = "DECODING_ERROR";

		// Token: 0x040001C9 RID: 457
		public const string ENCODING_ERROR = "ENCODING_ERROR";

		// Token: 0x040001CA RID: 458
		public const string IO_EXCEPTION = "IO_EXCEPTION";

		// Token: 0x040001CB RID: 459
		public const string INVALID_ESCAPE = "INVALID_ESCAPE";

		// Token: 0x040001CC RID: 460
		public const string SHORT_ESCAPE = "SHORT_ESCAPE";

		// Token: 0x040001CD RID: 461
		public const string INVALID_CHAR_IN_FILTER = "INVALID_CHAR_IN_FILTER";

		// Token: 0x040001CE RID: 462
		public const string INVALID_CHAR_IN_DESCR = "INVALID_CHAR_IN_DESCR";

		// Token: 0x040001CF RID: 463
		public const string INVALID_ESC_IN_DESCR = "INVALID_ESC_IN_DESCR";

		// Token: 0x040001D0 RID: 464
		public const string UNEXPECTED_END = "UNEXPECTED_END";

		// Token: 0x040001D1 RID: 465
		public const string MISSING_LEFT_PAREN = "MISSING_LEFT_PAREN";

		// Token: 0x040001D2 RID: 466
		public const string MISSING_RIGHT_PAREN = "MISSING_RIGHT_PAREN";

		// Token: 0x040001D3 RID: 467
		public const string EXPECTING_RIGHT_PAREN = "EXPECTING_RIGHT_PAREN";

		// Token: 0x040001D4 RID: 468
		public const string EXPECTING_LEFT_PAREN = "EXPECTING_LEFT_PAREN";

		// Token: 0x040001D5 RID: 469
		public const string NO_OPTION = "NO_OPTION";

		// Token: 0x040001D6 RID: 470
		public const string INVALID_FILTER_COMPARISON = "INVALID_FILTER_COMPARISON";

		// Token: 0x040001D7 RID: 471
		public const string NO_MATCHING_RULE = "NO_MATCHING_RULE";

		// Token: 0x040001D8 RID: 472
		public const string NO_ATTRIBUTE_NAME = "NO_ATTRIBUTE_NAME";

		// Token: 0x040001D9 RID: 473
		public const string NO_DN_NOR_MATCHING_RULE = "NO_DN_NOR_MATCHING_RULE";

		// Token: 0x040001DA RID: 474
		public const string NOT_AN_ATTRIBUTE = "NOT_AN_ATTRIBUTE";

		// Token: 0x040001DB RID: 475
		public const string UNEQUAL_LENGTHS = "UNEQUAL_LENGTHS";

		// Token: 0x040001DC RID: 476
		public const string IMPROPER_REFERRAL = "IMPROPER_REFERRAL";

		// Token: 0x040001DD RID: 477
		public const string NOT_IMPLEMENTED = "NOT_IMPLEMENTED";

		// Token: 0x040001DE RID: 478
		public const string NO_MEMORY = "NO_MEMORY";

		// Token: 0x040001DF RID: 479
		public const string SERVER_SHUTDOWN_REQ = "SERVER_SHUTDOWN_REQ";

		// Token: 0x040001E0 RID: 480
		public const string INVALID_ADDRESS = "INVALID_ADDRESS";

		// Token: 0x040001E1 RID: 481
		public const string UNKNOWN_RESULT = "UNKNOWN_RESULT";

		// Token: 0x040001E2 RID: 482
		public const string OUTSTANDING_OPERATIONS = "OUTSTANDING_OPERATIONS";

		// Token: 0x040001E3 RID: 483
		public const string WRONG_FACTORY = "WRONG_FACTORY";

		// Token: 0x040001E4 RID: 484
		public const string NO_TLS_FACTORY = "NO_TLS_FACTORY";

		// Token: 0x040001E5 RID: 485
		public const string NO_STARTTLS = "NO_STARTTLS";

		// Token: 0x040001E6 RID: 486
		public const string STOPTLS_ERROR = "STOPTLS_ERROR";

		// Token: 0x040001E7 RID: 487
		public const string MULTIPLE_SCHEMA = "MULTIPLE_SCHEMA";

		// Token: 0x040001E8 RID: 488
		public const string NO_SCHEMA = "NO_SCHEMA";

		// Token: 0x040001E9 RID: 489
		public const string READ_MULTIPLE = "READ_MULTIPLE";

		// Token: 0x040001EA RID: 490
		public const string CANNOT_BIND = "CANNOT_BIND";

		// Token: 0x040001EB RID: 491
		public const string SSL_PROVIDER_MISSING = "SSL_PROVIDER_MISSING";

		// Token: 0x040001EC RID: 492
		internal static readonly object[][] contents = new object[][]
		{
			new object[] { "TOSTRING", "{0}: {1} ({2}) {3}" },
			new object[] { "SERVER_MSG", "{0}: Server Message: {1}" },
			new object[] { "MATCHED_DN", "{0}: Matched DN: {1}" },
			new object[] { "FAILED_REFERRAL", "{0}: Failed Referral: {1}" },
			new object[] { "REFERRAL_ITEM", "{0}: Referral: {1}" },
			new object[] { "CONNECTION_ERROR", "Unable to connect to server {0}:{1}" },
			new object[] { "CONNECTION_IMPOSSIBLE", "Unable to reconnect to server, application has never called connect()" },
			new object[] { "CONNECTION_WAIT", "Connection lost waiting for results from {0}:{1}" },
			new object[] { "CONNECTION_FINALIZED", "Connection closed by the application finalizing the object" },
			new object[] { "CONNECTION_CLOSED", "Connection closed by the application disconnecting" },
			new object[] { "CONNECTION_READER", "Reader thread terminated" },
			new object[] { "DUP_ERROR", "RfcLdapMessage: Cannot duplicate message built from the input stream" },
			new object[] { "REFERENCE_ERROR", "Error attempting to follow a search continuation reference" },
			new object[] { "REFERRAL_ERROR", "Error attempting to follow a referral" },
			new object[] { "REFERRAL_LOCAL", "LdapSearchResults.{0}(): No entry found & request is not complete" },
			new object[] { "REFERRAL_SEND", "Error sending request to referred server" },
			new object[] { "REFERENCE_NOFOLLOW", "Search result reference received, and referral following is off" },
			new object[] { "REFERRAL_BIND", "LdapBind.bind() function returned null" },
			new object[] { "REFERRAL_BIND_MATCH", "Could not match LdapBind.bind() connection with Server Referral URL list" },
			new object[] { "NO_DUP_REQUEST", "Cannot duplicate message to follow referral for {0} request, not allowed" },
			new object[] { "SERVER_CONNECT_ERROR", "Error connecting to server {0} while attempting to follow a referral" },
			new object[] { "NO_SUP_PROPERTY", "Requested property is not supported." },
			new object[] { "ENTRY_PARAM_ERROR", "Invalid Entry parameter" },
			new object[] { "DN_PARAM_ERROR", "Invalid DN parameter" },
			new object[] { "RDN_PARAM_ERROR", "Invalid DN or RDN parameter" },
			new object[] { "OP_PARAM_ERROR", "Invalid extended operation parameter, no OID specified" },
			new object[] { "PARAM_ERROR", "Invalid parameter" },
			new object[] { "DECODING_ERROR", "Error Decoding responseValue" },
			new object[] { "ENCODING_ERROR", "Encoding Error" },
			new object[] { "IO_EXCEPTION", "I/O Exception on host {0}, port {1}" },
			new object[] { "INVALID_ESCAPE", "Invalid value in escape sequence \"{0}\"" },
			new object[] { "SHORT_ESCAPE", "Incomplete escape sequence" },
			new object[] { "UNEXPECTED_END", "Unexpected end of filter" },
			new object[] { "MISSING_LEFT_PAREN", "Unmatched parentheses, left parenthesis missing" },
			new object[] { "NO_OPTION", "Semicolon present, but no option specified" },
			new object[] { "MISSING_RIGHT_PAREN", "Unmatched parentheses, right parenthesis missing" },
			new object[] { "EXPECTING_RIGHT_PAREN", "Expecting right parenthesis, found \"{0}\"" },
			new object[] { "EXPECTING_LEFT_PAREN", "Expecting left parenthesis, found \"{0}\"" },
			new object[] { "NO_ATTRIBUTE_NAME", "Missing attribute description" },
			new object[] { "NO_DN_NOR_MATCHING_RULE", "DN and matching rule not specified" },
			new object[] { "NO_MATCHING_RULE", "Missing matching rule" },
			new object[] { "INVALID_FILTER_COMPARISON", "Invalid comparison operator" },
			new object[] { "INVALID_CHAR_IN_FILTER", "The invalid character \"{0}\" needs to be escaped as \"{1}\"" },
			new object[] { "INVALID_ESC_IN_DESCR", "Escape sequence not allowed in attribute description" },
			new object[] { "INVALID_CHAR_IN_DESCR", "Invalid character \"{0}\" in attribute description" },
			new object[] { "NOT_AN_ATTRIBUTE", "Schema element is not an LdapAttributeSchema object" },
			new object[] { "UNEQUAL_LENGTHS", "Length of attribute Name array does not equal length of Flags array" },
			new object[] { "IMPROPER_REFERRAL", "Referral not supported for command {0}" },
			new object[] { "NOT_IMPLEMENTED", "Method LdapConnection.startTLS not implemented" },
			new object[] { "NO_MEMORY", "All results could not be stored in memory, sort failed" },
			new object[] { "SERVER_SHUTDOWN_REQ", "Received unsolicited notification from server {0}:{1} to shutdown" },
			new object[] { "INVALID_ADDRESS", "Invalid syntax for address with port; {0}" },
			new object[] { "UNKNOWN_RESULT", "Unknown Ldap result code {0}" },
			new object[] { "OUTSTANDING_OPERATIONS", "Cannot start or stop TLS because outstanding Ldap operations exist on this connection" },
			new object[] { "WRONG_FACTORY", "StartTLS cannot use the set socket factory because it does not implement LdapTLSSocketFactory" },
			new object[] { "NO_TLS_FACTORY", "StartTLS failed because no LdapTLSSocketFactory has been set for this Connection" },
			new object[] { "NO_STARTTLS", "An attempt to stopTLS on a connection where startTLS had not been called" },
			new object[] { "STOPTLS_ERROR", "Error stopping TLS: Error getting input & output streams from the original socket" },
			new object[] { "MULTIPLE_SCHEMA", "Multiple schema found when reading the subschemaSubentry for {0}" },
			new object[] { "NO_SCHEMA", "No schema found when reading the subschemaSubentry for {0}" },
			new object[] { "READ_MULTIPLE", "Read response is ambiguous, multiple entries returned" },
			new object[] { "CANNOT_BIND", "Cannot bind. Use PoolManager.getBoundConnection()" }
		};
	}
}
