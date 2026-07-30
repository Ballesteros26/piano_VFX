using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001E7 RID: 487
	internal sealed class SqlStatistics
	{
		// Token: 0x06001677 RID: 5751 RVA: 0x0006F8E5 File Offset: 0x0006DAE5
		internal static SqlStatistics StartTimer(SqlStatistics statistics)
		{
			if (statistics != null && !statistics.RequestExecutionTimer())
			{
				statistics = null;
			}
			return statistics;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0006F8F6 File Offset: 0x0006DAF6
		internal static void StopTimer(SqlStatistics statistics)
		{
			if (statistics != null)
			{
				statistics.ReleaseAndUpdateExecutionTimer();
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0006F901 File Offset: 0x0006DB01
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x0006F909 File Offset: 0x0006DB09
		internal bool WaitForDoneAfterRow
		{
			get
			{
				return this._waitForDoneAfterRow;
			}
			set
			{
				this._waitForDoneAfterRow = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0006F912 File Offset: 0x0006DB12
		internal bool WaitForReply
		{
			get
			{
				return this._waitForReply;
			}
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00005C14 File Offset: 0x00003E14
		internal SqlStatistics()
		{
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0006F91A File Offset: 0x0006DB1A
		internal void ContinueOnNewConnection()
		{
			this._startExecutionTimestamp = 0L;
			this._startFetchTimestamp = 0L;
			this._waitForDoneAfterRow = false;
			this._waitForReply = false;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0006F93C File Offset: 0x0006DB3C
		internal IDictionary GetDictionary()
		{
			return new SqlStatistics.StatisticsDictionary(18)
			{
				{ "BuffersReceived", this._buffersReceived },
				{ "BuffersSent", this._buffersSent },
				{ "BytesReceived", this._bytesReceived },
				{ "BytesSent", this._bytesSent },
				{ "CursorOpens", this._cursorOpens },
				{ "IduCount", this._iduCount },
				{ "IduRows", this._iduRows },
				{ "PreparedExecs", this._preparedExecs },
				{ "Prepares", this._prepares },
				{ "SelectCount", this._selectCount },
				{ "SelectRows", this._selectRows },
				{ "ServerRoundtrips", this._serverRoundtrips },
				{ "SumResultSets", this._sumResultSets },
				{ "Transactions", this._transactions },
				{ "UnpreparedExecs", this._unpreparedExecs },
				{
					"ConnectionTime",
					ADP.TimerToMilliseconds(this._connectionTime)
				},
				{
					"ExecutionTime",
					ADP.TimerToMilliseconds(this._executionTime)
				},
				{
					"NetworkServerTime",
					ADP.TimerToMilliseconds(this._networkServerTime)
				}
			};
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0006FAEB File Offset: 0x0006DCEB
		internal bool RequestExecutionTimer()
		{
			if (this._startExecutionTimestamp == 0L)
			{
				ADP.TimerCurrent(out this._startExecutionTimestamp);
				return true;
			}
			return false;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0006FB03 File Offset: 0x0006DD03
		internal void RequestNetworkServerTimer()
		{
			if (this._startNetworkServerTimestamp == 0L)
			{
				ADP.TimerCurrent(out this._startNetworkServerTimestamp);
			}
			this._waitForReply = true;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0006FB1F File Offset: 0x0006DD1F
		internal void ReleaseAndUpdateExecutionTimer()
		{
			if (this._startExecutionTimestamp > 0L)
			{
				this._executionTime += ADP.TimerCurrent() - this._startExecutionTimestamp;
				this._startExecutionTimestamp = 0L;
			}
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0006FB4C File Offset: 0x0006DD4C
		internal void ReleaseAndUpdateNetworkServerTimer()
		{
			if (this._waitForReply && this._startNetworkServerTimestamp > 0L)
			{
				this._networkServerTime += ADP.TimerCurrent() - this._startNetworkServerTimestamp;
				this._startNetworkServerTimestamp = 0L;
			}
			this._waitForReply = false;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0006FB88 File Offset: 0x0006DD88
		internal void Reset()
		{
			this._buffersReceived = 0L;
			this._buffersSent = 0L;
			this._bytesReceived = 0L;
			this._bytesSent = 0L;
			this._connectionTime = 0L;
			this._cursorOpens = 0L;
			this._executionTime = 0L;
			this._iduCount = 0L;
			this._iduRows = 0L;
			this._networkServerTime = 0L;
			this._preparedExecs = 0L;
			this._prepares = 0L;
			this._selectCount = 0L;
			this._selectRows = 0L;
			this._serverRoundtrips = 0L;
			this._sumResultSets = 0L;
			this._transactions = 0L;
			this._unpreparedExecs = 0L;
			this._waitForDoneAfterRow = false;
			this._waitForReply = false;
			this._startExecutionTimestamp = 0L;
			this._startNetworkServerTimestamp = 0L;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0006FC43 File Offset: 0x0006DE43
		internal void SafeAdd(ref long value, long summand)
		{
			if (9223372036854775807L - value > summand)
			{
				value += summand;
				return;
			}
			value = long.MaxValue;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0006FC66 File Offset: 0x0006DE66
		internal long SafeIncrement(ref long value)
		{
			if (value < 9223372036854775807L)
			{
				value += 1L;
			}
			return value;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0006FC7E File Offset: 0x0006DE7E
		internal void UpdateStatistics()
		{
			if (this._closeTimestamp >= this._openTimestamp)
			{
				this.SafeAdd(ref this._connectionTime, this._closeTimestamp - this._openTimestamp);
				return;
			}
			this._connectionTime = long.MaxValue;
		}

		// Token: 0x04000EDF RID: 3807
		internal long _closeTimestamp;

		// Token: 0x04000EE0 RID: 3808
		internal long _openTimestamp;

		// Token: 0x04000EE1 RID: 3809
		internal long _startExecutionTimestamp;

		// Token: 0x04000EE2 RID: 3810
		internal long _startFetchTimestamp;

		// Token: 0x04000EE3 RID: 3811
		internal long _startNetworkServerTimestamp;

		// Token: 0x04000EE4 RID: 3812
		internal long _buffersReceived;

		// Token: 0x04000EE5 RID: 3813
		internal long _buffersSent;

		// Token: 0x04000EE6 RID: 3814
		internal long _bytesReceived;

		// Token: 0x04000EE7 RID: 3815
		internal long _bytesSent;

		// Token: 0x04000EE8 RID: 3816
		internal long _connectionTime;

		// Token: 0x04000EE9 RID: 3817
		internal long _cursorOpens;

		// Token: 0x04000EEA RID: 3818
		internal long _executionTime;

		// Token: 0x04000EEB RID: 3819
		internal long _iduCount;

		// Token: 0x04000EEC RID: 3820
		internal long _iduRows;

		// Token: 0x04000EED RID: 3821
		internal long _networkServerTime;

		// Token: 0x04000EEE RID: 3822
		internal long _preparedExecs;

		// Token: 0x04000EEF RID: 3823
		internal long _prepares;

		// Token: 0x04000EF0 RID: 3824
		internal long _selectCount;

		// Token: 0x04000EF1 RID: 3825
		internal long _selectRows;

		// Token: 0x04000EF2 RID: 3826
		internal long _serverRoundtrips;

		// Token: 0x04000EF3 RID: 3827
		internal long _sumResultSets;

		// Token: 0x04000EF4 RID: 3828
		internal long _transactions;

		// Token: 0x04000EF5 RID: 3829
		internal long _unpreparedExecs;

		// Token: 0x04000EF6 RID: 3830
		private bool _waitForDoneAfterRow;

		// Token: 0x04000EF7 RID: 3831
		private bool _waitForReply;

		// Token: 0x020001E8 RID: 488
		private sealed class StatisticsDictionary : Dictionary<object, object>, IDictionary, ICollection, IEnumerable
		{
			// Token: 0x06001687 RID: 5767 RVA: 0x0006FCB7 File Offset: 0x0006DEB7
			public StatisticsDictionary(int capacity)
				: base(capacity)
			{
			}

			// Token: 0x17000457 RID: 1111
			// (get) Token: 0x06001688 RID: 5768 RVA: 0x0006FCC0 File Offset: 0x0006DEC0
			ICollection IDictionary.Keys
			{
				get
				{
					SqlStatistics.StatisticsDictionary.Collection collection;
					if ((collection = this._keys) == null)
					{
						collection = (this._keys = new SqlStatistics.StatisticsDictionary.Collection(this, base.Keys));
					}
					return collection;
				}
			}

			// Token: 0x17000458 RID: 1112
			// (get) Token: 0x06001689 RID: 5769 RVA: 0x0006FCEC File Offset: 0x0006DEEC
			ICollection IDictionary.Values
			{
				get
				{
					SqlStatistics.StatisticsDictionary.Collection collection;
					if ((collection = this._values) == null)
					{
						collection = (this._values = new SqlStatistics.StatisticsDictionary.Collection(this, base.Values));
					}
					return collection;
				}
			}

			// Token: 0x0600168A RID: 5770 RVA: 0x0006FD18 File Offset: 0x0006DF18
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IDictionary)this).GetEnumerator();
			}

			// Token: 0x0600168B RID: 5771 RVA: 0x0006FD20 File Offset: 0x0006DF20
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.ValidateCopyToArguments(array, arrayIndex);
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					DictionaryEntry dictionaryEntry = new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
					array.SetValue(dictionaryEntry, arrayIndex++);
				}
			}

			// Token: 0x0600168C RID: 5772 RVA: 0x0006FD98 File Offset: 0x0006DF98
			private void CopyKeys(Array array, int arrayIndex)
			{
				this.ValidateCopyToArguments(array, arrayIndex);
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					array.SetValue(keyValuePair.Key, arrayIndex++);
				}
			}

			// Token: 0x0600168D RID: 5773 RVA: 0x0006FDFC File Offset: 0x0006DFFC
			private void CopyValues(Array array, int arrayIndex)
			{
				this.ValidateCopyToArguments(array, arrayIndex);
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					array.SetValue(keyValuePair.Value, arrayIndex++);
				}
			}

			// Token: 0x0600168E RID: 5774 RVA: 0x0006FE60 File Offset: 0x0006E060
			private void ValidateCopyToArguments(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < base.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
			}

			// Token: 0x04000EF8 RID: 3832
			private SqlStatistics.StatisticsDictionary.Collection _keys;

			// Token: 0x04000EF9 RID: 3833
			private SqlStatistics.StatisticsDictionary.Collection _values;

			// Token: 0x020001E9 RID: 489
			private sealed class Collection : ICollection, IEnumerable
			{
				// Token: 0x0600168F RID: 5775 RVA: 0x0006FEBE File Offset: 0x0006E0BE
				public Collection(SqlStatistics.StatisticsDictionary dictionary, ICollection collection)
				{
					this._dictionary = dictionary;
					this._collection = collection;
				}

				// Token: 0x17000459 RID: 1113
				// (get) Token: 0x06001690 RID: 5776 RVA: 0x0006FED4 File Offset: 0x0006E0D4
				int ICollection.Count
				{
					get
					{
						return this._collection.Count;
					}
				}

				// Token: 0x1700045A RID: 1114
				// (get) Token: 0x06001691 RID: 5777 RVA: 0x0006FEE1 File Offset: 0x0006E0E1
				bool ICollection.IsSynchronized
				{
					get
					{
						return this._collection.IsSynchronized;
					}
				}

				// Token: 0x1700045B RID: 1115
				// (get) Token: 0x06001692 RID: 5778 RVA: 0x0006FEEE File Offset: 0x0006E0EE
				object ICollection.SyncRoot
				{
					get
					{
						return this._collection.SyncRoot;
					}
				}

				// Token: 0x06001693 RID: 5779 RVA: 0x0006FEFB File Offset: 0x0006E0FB
				void ICollection.CopyTo(Array array, int arrayIndex)
				{
					if (this._collection is Dictionary<object, object>.KeyCollection)
					{
						this._dictionary.CopyKeys(array, arrayIndex);
						return;
					}
					this._dictionary.CopyValues(array, arrayIndex);
				}

				// Token: 0x06001694 RID: 5780 RVA: 0x0006FF25 File Offset: 0x0006E125
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this._collection.GetEnumerator();
				}

				// Token: 0x04000EFA RID: 3834
				private readonly SqlStatistics.StatisticsDictionary _dictionary;

				// Token: 0x04000EFB RID: 3835
				private readonly ICollection _collection;
			}
		}
	}
}
