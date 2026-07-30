using System;

namespace System.Threading.Tasks
{
	/// <summary>Stores options that configure the operation of methods on the <see cref="T:System.Threading.Tasks.Parallel" /> class.</summary>
	// Token: 0x020004D5 RID: 1237
	public class ParallelOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.ParallelOptions" /> class.</summary>
		// Token: 0x06003965 RID: 14693 RVA: 0x000CF31E File Offset: 0x000CD51E
		public ParallelOptions()
		{
			this.m_scheduler = TaskScheduler.Default;
			this.m_maxDegreeOfParallelism = -1;
			this.m_cancellationToken = CancellationToken.None;
		}

		/// <summary>Gets or sets the <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with this <see cref="T:System.Threading.Tasks.ParallelOptions" /> instance. Setting this property to null indicates that the current scheduler should be used.</summary>
		/// <returns>The task scheduler that is associated with this instance.</returns>
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06003966 RID: 14694 RVA: 0x000CF343 File Offset: 0x000CD543
		// (set) Token: 0x06003967 RID: 14695 RVA: 0x000CF34B File Offset: 0x000CD54B
		public TaskScheduler TaskScheduler
		{
			get
			{
				return this.m_scheduler;
			}
			set
			{
				this.m_scheduler = value;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003968 RID: 14696 RVA: 0x000CF354 File Offset: 0x000CD554
		internal TaskScheduler EffectiveTaskScheduler
		{
			get
			{
				if (this.m_scheduler == null)
				{
					return TaskScheduler.Current;
				}
				return this.m_scheduler;
			}
		}

		/// <summary>Gets or sets the maximum degree of parallelism enabled by this ParallelOptions instance.</summary>
		/// <returns>An integer that represents the maximum degree of parallelism.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when this <see cref="P:System.Threading.Tasks.ParallelOptions.MaxDegreeOfParallelism" /> is set to 0 or some value less than -1.</exception>
		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06003969 RID: 14697 RVA: 0x000CF36A File Offset: 0x000CD56A
		// (set) Token: 0x0600396A RID: 14698 RVA: 0x000CF372 File Offset: 0x000CD572
		public int MaxDegreeOfParallelism
		{
			get
			{
				return this.m_maxDegreeOfParallelism;
			}
			set
			{
				if (value == 0 || value < -1)
				{
					throw new ArgumentOutOfRangeException("MaxDegreeOfParallelism");
				}
				this.m_maxDegreeOfParallelism = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Threading.CancellationToken" /> associated with this <see cref="T:System.Threading.Tasks.ParallelOptions" /> instance.</summary>
		/// <returns>The token that is associated with this instance.</returns>
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x0600396B RID: 14699 RVA: 0x000CF38D File Offset: 0x000CD58D
		// (set) Token: 0x0600396C RID: 14700 RVA: 0x000CF395 File Offset: 0x000CD595
		public CancellationToken CancellationToken
		{
			get
			{
				return this.m_cancellationToken;
			}
			set
			{
				this.m_cancellationToken = value;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x0600396D RID: 14701 RVA: 0x000CF3A0 File Offset: 0x000CD5A0
		internal int EffectiveMaxConcurrencyLevel
		{
			get
			{
				int num = this.MaxDegreeOfParallelism;
				int maximumConcurrencyLevel = this.EffectiveTaskScheduler.MaximumConcurrencyLevel;
				if (maximumConcurrencyLevel > 0 && maximumConcurrencyLevel != 2147483647)
				{
					num = ((num == -1) ? maximumConcurrencyLevel : Math.Min(maximumConcurrencyLevel, num));
				}
				return num;
			}
		}

		// Token: 0x04001DF8 RID: 7672
		private TaskScheduler m_scheduler;

		// Token: 0x04001DF9 RID: 7673
		private int m_maxDegreeOfParallelism;

		// Token: 0x04001DFA RID: 7674
		private CancellationToken m_cancellationToken;
	}
}
