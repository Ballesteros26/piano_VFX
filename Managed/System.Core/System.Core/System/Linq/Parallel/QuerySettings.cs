using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001A4 RID: 420
	internal struct QuerySettings
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00025A22 File Offset: 0x00023C22
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x00025A2A File Offset: 0x00023C2A
		internal CancellationState CancellationState
		{
			get
			{
				return this._cancellationState;
			}
			set
			{
				this._cancellationState = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00025A33 File Offset: 0x00023C33
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x00025A3B File Offset: 0x00023C3B
		internal TaskScheduler TaskScheduler
		{
			get
			{
				return this._taskScheduler;
			}
			set
			{
				this._taskScheduler = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00025A44 File Offset: 0x00023C44
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00025A4C File Offset: 0x00023C4C
		internal int? DegreeOfParallelism
		{
			get
			{
				return this._degreeOfParallelism;
			}
			set
			{
				this._degreeOfParallelism = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00025A55 File Offset: 0x00023C55
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x00025A5D File Offset: 0x00023C5D
		internal ParallelExecutionMode? ExecutionMode
		{
			get
			{
				return this._executionMode;
			}
			set
			{
				this._executionMode = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00025A66 File Offset: 0x00023C66
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00025A6E File Offset: 0x00023C6E
		internal ParallelMergeOptions? MergeOptions
		{
			get
			{
				return this._mergeOptions;
			}
			set
			{
				this._mergeOptions = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00025A77 File Offset: 0x00023C77
		internal int QueryId
		{
			get
			{
				return this._queryId;
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00025A7F File Offset: 0x00023C7F
		internal QuerySettings(TaskScheduler taskScheduler, int? degreeOfParallelism, CancellationToken externalCancellationToken, ParallelExecutionMode? executionMode, ParallelMergeOptions? mergeOptions)
		{
			this._taskScheduler = taskScheduler;
			this._degreeOfParallelism = degreeOfParallelism;
			this._cancellationState = new CancellationState(externalCancellationToken);
			this._executionMode = executionMode;
			this._mergeOptions = mergeOptions;
			this._queryId = -1;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00025AB4 File Offset: 0x00023CB4
		internal QuerySettings Merge(QuerySettings settings2)
		{
			if (this.TaskScheduler != null && settings2.TaskScheduler != null)
			{
				throw new InvalidOperationException("The WithTaskScheduler operator may be used at most once in a query.");
			}
			if (this.DegreeOfParallelism != null && settings2.DegreeOfParallelism != null)
			{
				throw new InvalidOperationException("The WithDegreeOfParallelism operator may be used at most once in a query.");
			}
			if (this.CancellationState.ExternalCancellationToken.CanBeCanceled && settings2.CancellationState.ExternalCancellationToken.CanBeCanceled)
			{
				throw new InvalidOperationException("The WithCancellation operator may by used at most once in a query.");
			}
			if (this.ExecutionMode != null && settings2.ExecutionMode != null)
			{
				throw new InvalidOperationException("The WithExecutionMode operator may be used at most once in a query.");
			}
			if (this.MergeOptions != null && settings2.MergeOptions != null)
			{
				throw new InvalidOperationException("The WithMergeOptions operator may be used at most once in a query.");
			}
			TaskScheduler taskScheduler = ((this.TaskScheduler == null) ? settings2.TaskScheduler : this.TaskScheduler);
			int? num = ((this.DegreeOfParallelism != null) ? this.DegreeOfParallelism : settings2.DegreeOfParallelism);
			CancellationToken cancellationToken = (this.CancellationState.ExternalCancellationToken.CanBeCanceled ? this.CancellationState.ExternalCancellationToken : settings2.CancellationState.ExternalCancellationToken);
			ParallelExecutionMode? parallelExecutionMode = ((this.ExecutionMode != null) ? this.ExecutionMode : settings2.ExecutionMode);
			ParallelMergeOptions? parallelMergeOptions = ((this.MergeOptions != null) ? this.MergeOptions : settings2.MergeOptions);
			return new QuerySettings(taskScheduler, num, cancellationToken, parallelExecutionMode, parallelMergeOptions);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00025C46 File Offset: 0x00023E46
		internal QuerySettings WithPerExecutionSettings()
		{
			return this.WithPerExecutionSettings(new CancellationTokenSource(), new Shared<bool>(false));
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00025C5C File Offset: 0x00023E5C
		internal QuerySettings WithPerExecutionSettings(CancellationTokenSource topLevelCancellationTokenSource, Shared<bool> topLevelDisposedFlag)
		{
			QuerySettings querySettings = new QuerySettings(this.TaskScheduler, this.DegreeOfParallelism, this.CancellationState.ExternalCancellationToken, this.ExecutionMode, this.MergeOptions);
			querySettings.CancellationState.InternalCancellationTokenSource = topLevelCancellationTokenSource;
			querySettings.CancellationState.MergedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(querySettings.CancellationState.InternalCancellationTokenSource.Token, querySettings.CancellationState.ExternalCancellationToken);
			querySettings.CancellationState.TopLevelDisposedFlag = topLevelDisposedFlag;
			querySettings._queryId = PlinqEtwProvider.NextQueryId();
			return querySettings;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00025CE8 File Offset: 0x00023EE8
		internal QuerySettings WithDefaults()
		{
			QuerySettings querySettings = this;
			if (querySettings.TaskScheduler == null)
			{
				querySettings.TaskScheduler = TaskScheduler.Default;
			}
			if (querySettings.DegreeOfParallelism == null)
			{
				querySettings.DegreeOfParallelism = new int?(Scheduling.GetDefaultDegreeOfParallelism());
			}
			if (querySettings.ExecutionMode == null)
			{
				querySettings.ExecutionMode = new ParallelExecutionMode?(ParallelExecutionMode.Default);
			}
			if (querySettings.MergeOptions == null)
			{
				querySettings.MergeOptions = new ParallelMergeOptions?(ParallelMergeOptions.Default);
			}
			if (querySettings.MergeOptions == ParallelMergeOptions.Default)
			{
				querySettings.MergeOptions = new ParallelMergeOptions?(ParallelMergeOptions.AutoBuffered);
			}
			return querySettings;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00025DA0 File Offset: 0x00023FA0
		internal static QuerySettings Empty
		{
			get
			{
				return new QuerySettings(null, null, default(CancellationToken), null, null);
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00025DD7 File Offset: 0x00023FD7
		public void CleanStateAtQueryEnd()
		{
			this._cancellationState.MergedCancellationTokenSource.Dispose();
		}

		// Token: 0x040006C1 RID: 1729
		private TaskScheduler _taskScheduler;

		// Token: 0x040006C2 RID: 1730
		private int? _degreeOfParallelism;

		// Token: 0x040006C3 RID: 1731
		private CancellationState _cancellationState;

		// Token: 0x040006C4 RID: 1732
		private ParallelExecutionMode? _executionMode;

		// Token: 0x040006C5 RID: 1733
		private ParallelMergeOptions? _mergeOptions;

		// Token: 0x040006C6 RID: 1734
		private int _queryId;
	}
}
