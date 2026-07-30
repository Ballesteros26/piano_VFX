using System;
using System.Globalization;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001E RID: 30
	public class LdapException : Exception
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000078A3 File Offset: 0x00005AA3
		public virtual string LdapErrorMessage
		{
			get
			{
				if (this.serverMessage != null && this.serverMessage.Length == 0)
				{
					return null;
				}
				return this.serverMessage;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000078C2 File Offset: 0x00005AC2
		public virtual Exception Cause
		{
			get
			{
				return this.rootException;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000078CA File Offset: 0x00005ACA
		public virtual int ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000078D2 File Offset: 0x00005AD2
		public virtual string MatchedDN
		{
			get
			{
				return this.matchedDN;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000078DA File Offset: 0x00005ADA
		public override string Message
		{
			get
			{
				return this.resultCodeToString();
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000078E2 File Offset: 0x00005AE2
		public LdapException()
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000078EA File Offset: 0x00005AEA
		public LdapException(string messageOrKey, int resultCode, string serverMsg)
			: this(messageOrKey, null, resultCode, serverMsg, null, null)
		{
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000078F8 File Offset: 0x00005AF8
		public LdapException(string messageOrKey, object[] arguments, int resultCode, string serverMsg)
			: this(messageOrKey, arguments, resultCode, serverMsg, null, null)
		{
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007907 File Offset: 0x00005B07
		public LdapException(string messageOrKey, int resultCode, string serverMsg, Exception rootException)
			: this(messageOrKey, null, resultCode, serverMsg, null, rootException)
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007916 File Offset: 0x00005B16
		public LdapException(string messageOrKey, object[] arguments, int resultCode, string serverMsg, Exception rootException)
			: this(messageOrKey, arguments, resultCode, serverMsg, null, rootException)
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007926 File Offset: 0x00005B26
		public LdapException(string messageOrKey, int resultCode, string serverMsg, string matchedDN)
			: this(messageOrKey, null, resultCode, serverMsg, matchedDN, null)
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007935 File Offset: 0x00005B35
		public LdapException(string messageOrKey, object[] arguments, int resultCode, string serverMsg, string matchedDN)
			: this(messageOrKey, arguments, resultCode, serverMsg, matchedDN, null)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007945 File Offset: 0x00005B45
		internal LdapException(string messageOrKey, object[] arguments, int resultCode, string serverMsg, string matchedDN, Exception rootException)
		{
			this.messageOrKey = messageOrKey;
			this.arguments = arguments;
			this.resultCode = resultCode;
			this.rootException = rootException;
			this.matchedDN = matchedDN;
			this.serverMessage = serverMsg;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000797A File Offset: 0x00005B7A
		public virtual string resultCodeToString()
		{
			return ResourcesHandler.getResultString(this.resultCode);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007987 File Offset: 0x00005B87
		public static string resultCodeToString(int code)
		{
			return ResourcesHandler.getResultString(code);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000798F File Offset: 0x00005B8F
		public virtual string resultCodeToString(CultureInfo locale)
		{
			return ResourcesHandler.getResultString(this.resultCode, locale);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000799D File Offset: 0x00005B9D
		public static string resultCodeToString(int code, CultureInfo locale)
		{
			return ResourcesHandler.getResultString(code, locale);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000079A6 File Offset: 0x00005BA6
		public override string ToString()
		{
			return this.getExceptionString("LdapException");
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000079B4 File Offset: 0x00005BB4
		internal virtual string getExceptionString(string exception)
		{
			string text = ResourcesHandler.getMessage("TOSTRING", new object[]
			{
				exception,
				base.Message,
				this.resultCode,
				this.resultCodeToString()
			});
			if (text.ToUpper().Equals("TOSTRING".ToUpper()))
			{
				text = string.Concat(new object[]
				{
					exception,
					": (",
					this.resultCode,
					") ",
					this.resultCodeToString()
				});
			}
			if (this.serverMessage != null && this.serverMessage.Length != 0)
			{
				string text2 = ResourcesHandler.getMessage("SERVER_MSG", new object[] { exception, this.serverMessage });
				if (text2.ToUpper().Equals("SERVER_MSG".ToUpper()))
				{
					text2 = exception + ": Server Message: " + this.serverMessage;
				}
				text = text + "\n" + text2;
			}
			if (this.matchedDN != null)
			{
				string text2 = ResourcesHandler.getMessage("MATCHED_DN", new object[] { exception, this.matchedDN });
				if (text2.ToUpper().Equals("MATCHED_DN".ToUpper()))
				{
					text2 = exception + ": Matched DN: " + this.matchedDN;
				}
				text = text + "\n" + text2;
			}
			if (this.rootException != null)
			{
				text = text + "\n" + this.rootException.ToString();
			}
			return text;
		}

		// Token: 0x040000B7 RID: 183
		private int resultCode;

		// Token: 0x040000B8 RID: 184
		private string messageOrKey;

		// Token: 0x040000B9 RID: 185
		private object[] arguments;

		// Token: 0x040000BA RID: 186
		private string matchedDN;

		// Token: 0x040000BB RID: 187
		private Exception rootException;

		// Token: 0x040000BC RID: 188
		private string serverMessage;

		// Token: 0x040000BD RID: 189
		public const int SUCCESS = 0;

		// Token: 0x040000BE RID: 190
		public const int OPERATIONS_ERROR = 1;

		// Token: 0x040000BF RID: 191
		public const int PROTOCOL_ERROR = 2;

		// Token: 0x040000C0 RID: 192
		public const int TIME_LIMIT_EXCEEDED = 3;

		// Token: 0x040000C1 RID: 193
		public const int SIZE_LIMIT_EXCEEDED = 4;

		// Token: 0x040000C2 RID: 194
		public const int COMPARE_FALSE = 5;

		// Token: 0x040000C3 RID: 195
		public const int COMPARE_TRUE = 6;

		// Token: 0x040000C4 RID: 196
		public const int AUTH_METHOD_NOT_SUPPORTED = 7;

		// Token: 0x040000C5 RID: 197
		public const int STRONG_AUTH_REQUIRED = 8;

		// Token: 0x040000C6 RID: 198
		public const int Ldap_PARTIAL_RESULTS = 9;

		// Token: 0x040000C7 RID: 199
		public const int REFERRAL = 10;

		// Token: 0x040000C8 RID: 200
		public const int ADMIN_LIMIT_EXCEEDED = 11;

		// Token: 0x040000C9 RID: 201
		public const int UNAVAILABLE_CRITICAL_EXTENSION = 12;

		// Token: 0x040000CA RID: 202
		public const int CONFIDENTIALITY_REQUIRED = 13;

		// Token: 0x040000CB RID: 203
		public const int SASL_BIND_IN_PROGRESS = 14;

		// Token: 0x040000CC RID: 204
		public const int NO_SUCH_ATTRIBUTE = 16;

		// Token: 0x040000CD RID: 205
		public const int UNDEFINED_ATTRIBUTE_TYPE = 17;

		// Token: 0x040000CE RID: 206
		public const int INAPPROPRIATE_MATCHING = 18;

		// Token: 0x040000CF RID: 207
		public const int CONSTRAINT_VIOLATION = 19;

		// Token: 0x040000D0 RID: 208
		public const int ATTRIBUTE_OR_VALUE_EXISTS = 20;

		// Token: 0x040000D1 RID: 209
		public const int INVALID_ATTRIBUTE_SYNTAX = 21;

		// Token: 0x040000D2 RID: 210
		public const int NO_SUCH_OBJECT = 32;

		// Token: 0x040000D3 RID: 211
		public const int ALIAS_PROBLEM = 33;

		// Token: 0x040000D4 RID: 212
		public const int INVALID_DN_SYNTAX = 34;

		// Token: 0x040000D5 RID: 213
		public const int IS_LEAF = 35;

		// Token: 0x040000D6 RID: 214
		public const int ALIAS_DEREFERENCING_PROBLEM = 36;

		// Token: 0x040000D7 RID: 215
		public const int INAPPROPRIATE_AUTHENTICATION = 48;

		// Token: 0x040000D8 RID: 216
		public const int INVALID_CREDENTIALS = 49;

		// Token: 0x040000D9 RID: 217
		public const int INSUFFICIENT_ACCESS_RIGHTS = 50;

		// Token: 0x040000DA RID: 218
		public const int BUSY = 51;

		// Token: 0x040000DB RID: 219
		public const int UNAVAILABLE = 52;

		// Token: 0x040000DC RID: 220
		public const int UNWILLING_TO_PERFORM = 53;

		// Token: 0x040000DD RID: 221
		public const int LOOP_DETECT = 54;

		// Token: 0x040000DE RID: 222
		public const int NAMING_VIOLATION = 64;

		// Token: 0x040000DF RID: 223
		public const int OBJECT_CLASS_VIOLATION = 65;

		// Token: 0x040000E0 RID: 224
		public const int NOT_ALLOWED_ON_NONLEAF = 66;

		// Token: 0x040000E1 RID: 225
		public const int NOT_ALLOWED_ON_RDN = 67;

		// Token: 0x040000E2 RID: 226
		public const int ENTRY_ALREADY_EXISTS = 68;

		// Token: 0x040000E3 RID: 227
		public const int OBJECT_CLASS_MODS_PROHIBITED = 69;

		// Token: 0x040000E4 RID: 228
		public const int AFFECTS_MULTIPLE_DSAS = 71;

		// Token: 0x040000E5 RID: 229
		public const int OTHER = 80;

		// Token: 0x040000E6 RID: 230
		public const int SERVER_DOWN = 81;

		// Token: 0x040000E7 RID: 231
		public const int LOCAL_ERROR = 82;

		// Token: 0x040000E8 RID: 232
		public const int ENCODING_ERROR = 83;

		// Token: 0x040000E9 RID: 233
		public const int DECODING_ERROR = 84;

		// Token: 0x040000EA RID: 234
		public const int Ldap_TIMEOUT = 85;

		// Token: 0x040000EB RID: 235
		public const int AUTH_UNKNOWN = 86;

		// Token: 0x040000EC RID: 236
		public const int FILTER_ERROR = 87;

		// Token: 0x040000ED RID: 237
		public const int USER_CANCELLED = 88;

		// Token: 0x040000EE RID: 238
		public const int NO_MEMORY = 90;

		// Token: 0x040000EF RID: 239
		public const int CONNECT_ERROR = 91;

		// Token: 0x040000F0 RID: 240
		public const int Ldap_NOT_SUPPORTED = 92;

		// Token: 0x040000F1 RID: 241
		public const int CONTROL_NOT_FOUND = 93;

		// Token: 0x040000F2 RID: 242
		public const int NO_RESULTS_RETURNED = 94;

		// Token: 0x040000F3 RID: 243
		public const int MORE_RESULTS_TO_RETURN = 95;

		// Token: 0x040000F4 RID: 244
		public const int CLIENT_LOOP = 96;

		// Token: 0x040000F5 RID: 245
		public const int REFERRAL_LIMIT_EXCEEDED = 97;

		// Token: 0x040000F6 RID: 246
		public const int INVALID_RESPONSE = 100;

		// Token: 0x040000F7 RID: 247
		public const int AMBIGUOUS_RESPONSE = 101;

		// Token: 0x040000F8 RID: 248
		public const int TLS_NOT_SUPPORTED = 112;

		// Token: 0x040000F9 RID: 249
		public const int SSL_HANDSHAKE_FAILED = 113;

		// Token: 0x040000FA RID: 250
		public const int SSL_PROVIDER_NOT_FOUND = 114;
	}
}
