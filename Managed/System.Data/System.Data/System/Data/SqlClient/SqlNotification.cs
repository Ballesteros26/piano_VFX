using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001BB RID: 443
	internal class SqlNotification : MarshalByRefObject
	{
		// Token: 0x0600149F RID: 5279 RVA: 0x00067AEB File Offset: 0x00065CEB
		internal SqlNotification(SqlNotificationInfo info, SqlNotificationSource source, SqlNotificationType type, string key)
		{
			this._info = info;
			this._source = source;
			this._type = type;
			this._key = key;
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00067B10 File Offset: 0x00065D10
		internal SqlNotificationInfo Info
		{
			get
			{
				return this._info;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x00067B18 File Offset: 0x00065D18
		internal string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00067B20 File Offset: 0x00065D20
		internal SqlNotificationSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00067B28 File Offset: 0x00065D28
		internal SqlNotificationType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000DB6 RID: 3510
		private readonly SqlNotificationInfo _info;

		// Token: 0x04000DB7 RID: 3511
		private readonly SqlNotificationSource _source;

		// Token: 0x04000DB8 RID: 3512
		private readonly SqlNotificationType _type;

		// Token: 0x04000DB9 RID: 3513
		private readonly string _key;
	}
}
