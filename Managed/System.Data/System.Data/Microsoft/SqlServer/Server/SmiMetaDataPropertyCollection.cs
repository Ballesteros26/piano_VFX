using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003A2 RID: 930
	internal class SmiMetaDataPropertyCollection
	{
		// Token: 0x06002BC9 RID: 11209 RVA: 0x000C0810 File Offset: 0x000BEA10
		private static SmiMetaDataPropertyCollection CreateEmptyInstance()
		{
			SmiMetaDataPropertyCollection smiMetaDataPropertyCollection = new SmiMetaDataPropertyCollection();
			smiMetaDataPropertyCollection.SetReadOnly();
			return smiMetaDataPropertyCollection;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000C0820 File Offset: 0x000BEA20
		internal SmiMetaDataPropertyCollection()
		{
			this._properties = new SmiMetaDataProperty[3];
			this._isReadOnly = false;
			this._properties[0] = SmiMetaDataPropertyCollection.s_emptyDefaultFields;
			this._properties[1] = SmiMetaDataPropertyCollection.s_emptySortOrder;
			this._properties[2] = SmiMetaDataPropertyCollection.s_emptyUniqueKey;
		}

		// Token: 0x17000743 RID: 1859
		internal SmiMetaDataProperty this[SmiPropertySelector key]
		{
			get
			{
				return this._properties[(int)key];
			}
			set
			{
				if (value == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
				}
				this.EnsureWritable();
				this._properties[(int)key] = value;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x000C0893 File Offset: 0x000BEA93
		internal bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000C089B File Offset: 0x000BEA9B
		internal void SetReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000C08A4 File Offset: 0x000BEAA4
		private void EnsureWritable()
		{
			if (this.IsReadOnly)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
		}

		// Token: 0x04001AAE RID: 6830
		private const int SelectorCount = 3;

		// Token: 0x04001AAF RID: 6831
		private SmiMetaDataProperty[] _properties;

		// Token: 0x04001AB0 RID: 6832
		private bool _isReadOnly;

		// Token: 0x04001AB1 RID: 6833
		private static readonly SmiDefaultFieldsProperty s_emptyDefaultFields = new SmiDefaultFieldsProperty(new List<bool>());

		// Token: 0x04001AB2 RID: 6834
		private static readonly SmiOrderProperty s_emptySortOrder = new SmiOrderProperty(new List<SmiOrderProperty.SmiColumnOrder>());

		// Token: 0x04001AB3 RID: 6835
		private static readonly SmiUniqueKeyProperty s_emptyUniqueKey = new SmiUniqueKeyProperty(new List<bool>());

		// Token: 0x04001AB4 RID: 6836
		internal static readonly SmiMetaDataPropertyCollection EmptyInstance = SmiMetaDataPropertyCollection.CreateEmptyInstance();
	}
}
