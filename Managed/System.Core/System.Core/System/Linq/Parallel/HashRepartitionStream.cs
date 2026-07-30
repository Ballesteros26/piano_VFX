using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000127 RID: 295
	internal abstract class HashRepartitionStream<TInputOutput, THashKey, TOrderKey> : PartitionedStream<Pair<TInputOutput, THashKey>, TOrderKey>
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0001EDD3 File Offset: 0x0001CFD3
		internal HashRepartitionStream(int partitionsCount, IComparer<TOrderKey> orderKeyComparer, IEqualityComparer<THashKey> hashKeyComparer, IEqualityComparer<TInputOutput> elementComparer)
			: base(partitionsCount, orderKeyComparer, OrdinalIndexState.Shuffled)
		{
			this._keyComparer = hashKeyComparer;
			this._elementComparer = elementComparer;
			this._distributionMod = 503;
			checked
			{
				while (this._distributionMod < partitionsCount)
				{
					this._distributionMod *= 2;
				}
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001EE11 File Offset: 0x0001D011
		internal int GetHashCode(TInputOutput element)
		{
			return (int.MaxValue & ((this._elementComparer == null) ? ((element == null) ? 0 : element.GetHashCode()) : this._elementComparer.GetHashCode(element))) % this._distributionMod;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001EE4E File Offset: 0x0001D04E
		internal int GetHashCode(THashKey key)
		{
			return (int.MaxValue & ((this._keyComparer == null) ? ((key == null) ? 0 : key.GetHashCode()) : this._keyComparer.GetHashCode(key))) % this._distributionMod;
		}

		// Token: 0x040005A0 RID: 1440
		private readonly IEqualityComparer<THashKey> _keyComparer;

		// Token: 0x040005A1 RID: 1441
		private readonly IEqualityComparer<TInputOutput> _elementComparer;

		// Token: 0x040005A2 RID: 1442
		private readonly int _distributionMod;

		// Token: 0x040005A3 RID: 1443
		private const int NULL_ELEMENT_HASH_CODE = 0;

		// Token: 0x040005A4 RID: 1444
		private const int HashCodeMask = 2147483647;
	}
}
