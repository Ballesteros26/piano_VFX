using System;
using System.Threading;

namespace System.Runtime.InteropServices
{
	/// <summary>Tracks outstanding handles and forces a garbage collection when the specified threshold is reached.</summary>
	// Token: 0x0200035F RID: 863
	public sealed class HandleCollector
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleCollector" /> class using a name and a threshold at which to begin handle collection. </summary>
		/// <param name="name">A name for the collector. This parameter allows you to name collectors that track handle types separately.</param>
		/// <param name="initialThreshold">A value that specifies the point at which collections should begin.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="initialThreshold" /> parameter is less than 0.</exception>
		// Token: 0x06001ABB RID: 6843 RVA: 0x0006BBD8 File Offset: 0x00069DD8
		public HandleCollector(string name, int initialThreshold)
			: this(name, initialThreshold, int.MaxValue)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleCollector" /> class using a name, a threshold at which to begin handle collection, and a threshold at which handle collection must occur. </summary>
		/// <param name="name">A name for the collector.  This parameter allows you to name collectors that track handle types separately.</param>
		/// <param name="initialThreshold">A value that specifies the point at which collections should begin.</param>
		/// <param name="maximumThreshold">A value that specifies the point at which collections must occur. This should be set to the maximum number of available handles.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="initialThreshold" /> parameter is less than 0.-or-The <paramref name="maximumThreshold" /> parameter is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="maximumThreshold" /> parameter is less than the <paramref name="initialThreshold" /> parameter.</exception>
		// Token: 0x06001ABC RID: 6844 RVA: 0x0006BBE8 File Offset: 0x00069DE8
		public HandleCollector(string name, int initialThreshold, int maximumThreshold)
		{
			if (initialThreshold < 0)
			{
				throw new ArgumentOutOfRangeException("initialThreshold", global::SR.GetString("Non-negative number required."));
			}
			if (maximumThreshold < 0)
			{
				throw new ArgumentOutOfRangeException("maximumThreshold", global::SR.GetString("Non-negative number required."));
			}
			if (initialThreshold > maximumThreshold)
			{
				throw new ArgumentException(global::SR.GetString("maximumThreshold cannot be less than initialThreshold."));
			}
			if (name != null)
			{
				this.name = name;
			}
			else
			{
				this.name = string.Empty;
			}
			this.initialThreshold = initialThreshold;
			this.maximumThreshold = maximumThreshold;
			this.threshold = initialThreshold;
			this.handleCount = 0;
		}

		/// <summary>Gets the number of handles collected.</summary>
		/// <returns>The number of handles collected.</returns>
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0006BC80 File Offset: 0x00069E80
		public int Count
		{
			get
			{
				return this.handleCount;
			}
		}

		/// <summary>Gets a value that specifies the point at which collections should begin.</summary>
		/// <returns>A value that specifies the point at which collections should begin.</returns>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0006BC88 File Offset: 0x00069E88
		public int InitialThreshold
		{
			get
			{
				return this.initialThreshold;
			}
		}

		/// <summary>Gets a value that specifies the point at which collections must occur.</summary>
		/// <returns>A value that specifies the point at which collections must occur.</returns>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x0006BC90 File Offset: 0x00069E90
		public int MaximumThreshold
		{
			get
			{
				return this.maximumThreshold;
			}
		}

		/// <summary>Gets the name of a <see cref="T:System.Runtime.InteropServices.HandleCollector" /> object.</summary>
		/// <returns>This <see cref="P:System.Runtime.InteropServices.HandleCollector.Name" /> property allows you to name collectors that track handle types separately.</returns>
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x0006BC98 File Offset: 0x00069E98
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Increments the current handle count.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Runtime.InteropServices.HandleCollector.Count" /> property is less than 0.</exception>
		// Token: 0x06001AC1 RID: 6849 RVA: 0x0006BCA0 File Offset: 0x00069EA0
		public void Add()
		{
			int num = -1;
			Interlocked.Increment(ref this.handleCount);
			if (this.handleCount < 0)
			{
				throw new InvalidOperationException(global::SR.GetString("Handle collector count overflows or underflows."));
			}
			if (this.handleCount > this.threshold)
			{
				lock (this)
				{
					this.threshold = this.handleCount + this.handleCount / 10;
					num = this.gc_gen;
					if (this.gc_gen < 2)
					{
						this.gc_gen++;
					}
				}
			}
			if (num >= 0 && (num == 0 || this.gc_counts[num] == GC.CollectionCount(num)))
			{
				GC.Collect(num);
				Thread.Sleep(10 * num);
			}
			for (int i = 1; i < 3; i++)
			{
				this.gc_counts[i] = GC.CollectionCount(i);
			}
		}

		/// <summary>Decrements the current handle count.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Runtime.InteropServices.HandleCollector.Count" /> property is less than 0.</exception>
		// Token: 0x06001AC2 RID: 6850 RVA: 0x0006BD80 File Offset: 0x00069F80
		public void Remove()
		{
			Interlocked.Decrement(ref this.handleCount);
			if (this.handleCount < 0)
			{
				throw new InvalidOperationException(global::SR.GetString("Handle collector count overflows or underflows."));
			}
			int num = this.handleCount + this.handleCount / 10;
			if (num < this.threshold - this.threshold / 10)
			{
				lock (this)
				{
					if (num > this.initialThreshold)
					{
						this.threshold = num;
					}
					else
					{
						this.threshold = this.initialThreshold;
					}
					this.gc_gen = 0;
				}
			}
			for (int i = 1; i < 3; i++)
			{
				this.gc_counts[i] = GC.CollectionCount(i);
			}
		}

		// Token: 0x04001854 RID: 6228
		private const int deltaPercent = 10;

		// Token: 0x04001855 RID: 6229
		private string name;

		// Token: 0x04001856 RID: 6230
		private int initialThreshold;

		// Token: 0x04001857 RID: 6231
		private int maximumThreshold;

		// Token: 0x04001858 RID: 6232
		private int threshold;

		// Token: 0x04001859 RID: 6233
		private int handleCount;

		// Token: 0x0400185A RID: 6234
		private int[] gc_counts = new int[3];

		// Token: 0x0400185B RID: 6235
		private int gc_gen;
	}
}
