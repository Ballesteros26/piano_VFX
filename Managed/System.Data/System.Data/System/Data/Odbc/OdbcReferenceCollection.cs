using System;
using System.Data.ProviderBase;

namespace System.Data.Odbc
{
	// Token: 0x020002AB RID: 683
	internal sealed class OdbcReferenceCollection : DbReferenceCollection
	{
		// Token: 0x06001D4E RID: 7502 RVA: 0x0006EA06 File Offset: 0x0006CC06
		public override void Add(object value, int tag)
		{
			base.AddItem(value, tag);
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x00091206 File Offset: 0x0008F406
		protected override void NotifyItem(int message, int tag, object value)
		{
			if (message != 0)
			{
				if (message == 1 && 1 == tag)
				{
					((OdbcCommand)value).RecoverFromConnection();
					return;
				}
			}
			else if (1 == tag)
			{
				((OdbcCommand)value).CloseFromConnection();
			}
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0006EAEE File Offset: 0x0006CCEE
		public override void Remove(object value)
		{
			base.RemoveItem(value);
		}

		// Token: 0x04001582 RID: 5506
		internal const int Closing = 0;

		// Token: 0x04001583 RID: 5507
		internal const int Recover = 1;

		// Token: 0x04001584 RID: 5508
		internal const int CommandTag = 1;
	}
}
