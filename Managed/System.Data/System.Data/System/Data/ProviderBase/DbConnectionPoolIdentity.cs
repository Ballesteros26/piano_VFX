using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000313 RID: 787
	[Serializable]
	internal sealed class DbConnectionPoolIdentity
	{
		// Token: 0x060022FE RID: 8958 RVA: 0x000A2B5D File Offset: 0x000A0D5D
		internal static DbConnectionPoolIdentity GetCurrent()
		{
			return DbConnectionPoolIdentity.GetCurrentManaged();
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000A2B64 File Offset: 0x000A0D64
		private DbConnectionPoolIdentity(string sidString, bool isRestricted, bool isNetwork)
		{
			this._sidString = sidString;
			this._isRestricted = isRestricted;
			this._isNetwork = isNetwork;
			this._hashCode = ((sidString == null) ? 0 : sidString.GetHashCode());
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x000A2B93 File Offset: 0x000A0D93
		internal bool IsRestricted
		{
			get
			{
				return this._isRestricted;
			}
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000A2B9C File Offset: 0x000A0D9C
		public override bool Equals(object value)
		{
			bool flag = this == DbConnectionPoolIdentity.NoIdentity || this == value;
			if (!flag && value != null)
			{
				DbConnectionPoolIdentity dbConnectionPoolIdentity = (DbConnectionPoolIdentity)value;
				flag = this._sidString == dbConnectionPoolIdentity._sidString && this._isRestricted == dbConnectionPoolIdentity._isRestricted && this._isNetwork == dbConnectionPoolIdentity._isNetwork;
			}
			return flag;
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x000A2BFA File Offset: 0x000A0DFA
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x000A2C04 File Offset: 0x000A0E04
		internal static DbConnectionPoolIdentity GetCurrentManaged()
		{
			string text = ((!string.IsNullOrWhiteSpace(Environment.UserDomainName)) ? (Environment.UserDomainName + "\\") : "") + Environment.UserName;
			bool flag = false;
			bool flag2 = false;
			return new DbConnectionPoolIdentity(text, flag2, flag);
		}

		// Token: 0x0400172D RID: 5933
		public static readonly DbConnectionPoolIdentity NoIdentity = new DbConnectionPoolIdentity(string.Empty, false, true);

		// Token: 0x0400172E RID: 5934
		private readonly string _sidString;

		// Token: 0x0400172F RID: 5935
		private readonly bool _isRestricted;

		// Token: 0x04001730 RID: 5936
		private readonly bool _isNetwork;

		// Token: 0x04001731 RID: 5937
		private readonly int _hashCode;
	}
}
