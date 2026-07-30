using System;
using System.Diagnostics;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B5 RID: 181
	internal abstract class BaseVisualTreeUpdater : IVisualTreeUpdater, IDisposable
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000541 RID: 1345 RVA: 0x00014204 File Offset: 0x00012404
		// (remove) Token: 0x06000542 RID: 1346 RVA: 0x0001423C File Offset: 0x0001243C
		[field: DebuggerBrowsable(0)]
		public event Action<BaseVisualElementPanel> panelChanged;

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00014274 File Offset: 0x00012474
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0001428C File Offset: 0x0001248C
		public BaseVisualElementPanel panel
		{
			get
			{
				return this.m_Panel;
			}
			set
			{
				this.m_Panel = value;
				bool flag = this.panelChanged != null;
				if (flag)
				{
					this.panelChanged.Invoke(value);
				}
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000142BC File Offset: 0x000124BC
		public VisualElement visualTree
		{
			get
			{
				return this.panel.visualTree;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000546 RID: 1350
		public abstract ProfilerMarker profilerMarker { get; }

		// Token: 0x06000547 RID: 1351 RVA: 0x000142D9 File Offset: 0x000124D9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000062F3 File Offset: 0x000044F3
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000549 RID: 1353
		public abstract void Update();

		// Token: 0x0600054A RID: 1354
		public abstract void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType);

		// Token: 0x04000240 RID: 576
		private BaseVisualElementPanel m_Panel;
	}
}
