using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000194 RID: 404
	internal class ListQueryResults<T> : QueryResults<T>
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x00024E3F File Offset: 0x0002303F
		internal ListQueryResults(IList<T> source, int partitionCount, bool useStriping)
		{
			this._source = source;
			this._partitionCount = partitionCount;
			this._useStriping = useStriping;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00024E5C File Offset: 0x0002305C
		internal override void GivePartitionedStream(IPartitionedStreamRecipient<T> recipient)
		{
			PartitionedStream<T, int> partitionedStream = this.GetPartitionedStream();
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal override bool IsIndexible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00024E77 File Offset: 0x00023077
		internal override int ElementsCount
		{
			get
			{
				return this._source.Count;
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00024E84 File Offset: 0x00023084
		internal override T GetElement(int index)
		{
			return this._source[index];
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00024E92 File Offset: 0x00023092
		internal PartitionedStream<T, int> GetPartitionedStream()
		{
			return ExchangeUtilities.PartitionDataSource<T>(this._source, this._partitionCount, this._useStriping);
		}

		// Token: 0x04000694 RID: 1684
		private IList<T> _source;

		// Token: 0x04000695 RID: 1685
		private int _partitionCount;

		// Token: 0x04000696 RID: 1686
		private bool _useStriping;
	}
}
