using System;
using System.Data.ProviderBase;

namespace System.Data.SqlClient
{
	// Token: 0x020001D7 RID: 471
	internal sealed class SqlReferenceCollection : DbReferenceCollection
	{
		// Token: 0x06001627 RID: 5671 RVA: 0x0006EA06 File Offset: 0x0006CC06
		public override void Add(object value, int tag)
		{
			base.AddItem(value, tag);
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0006EA10 File Offset: 0x0006CC10
		internal void Deactivate()
		{
			base.Notify(0);
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0006EA1C File Offset: 0x0006CC1C
		internal SqlDataReader FindLiveReader(SqlCommand command)
		{
			if (command == null)
			{
				return base.FindItem<SqlDataReader>(1, (SqlDataReader dataReader) => !dataReader.IsClosed);
			}
			return base.FindItem<SqlDataReader>(1, (SqlDataReader dataReader) => !dataReader.IsClosed && command == dataReader.Command);
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0006EA78 File Offset: 0x0006CC78
		internal SqlCommand FindLiveCommand(TdsParserStateObject stateObj)
		{
			return base.FindItem<SqlCommand>(2, (SqlCommand command) => command.StateObject == stateObj);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0006EAA8 File Offset: 0x0006CCA8
		protected override void NotifyItem(int message, int tag, object value)
		{
			if (tag == 1)
			{
				SqlDataReader sqlDataReader = (SqlDataReader)value;
				if (!sqlDataReader.IsClosed)
				{
					sqlDataReader.CloseReaderFromConnection();
					return;
				}
			}
			else
			{
				if (tag == 2)
				{
					((SqlCommand)value).OnConnectionClosed();
					return;
				}
				if (tag == 3)
				{
					((SqlBulkCopy)value).OnConnectionClosed();
				}
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0006EAEE File Offset: 0x0006CCEE
		public override void Remove(object value)
		{
			base.RemoveItem(value);
		}

		// Token: 0x04000EBD RID: 3773
		internal const int DataReaderTag = 1;

		// Token: 0x04000EBE RID: 3774
		internal const int CommandTag = 2;

		// Token: 0x04000EBF RID: 3775
		internal const int BulkCopyTag = 3;
	}
}
