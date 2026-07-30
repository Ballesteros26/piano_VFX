using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B2 RID: 178
	internal sealed class VisualTreeUpdater : IDisposable
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x00013F79 File Offset: 0x00012179
		public VisualTreeUpdater(BaseVisualElementPanel panel)
		{
			this.m_Panel = panel;
			this.m_UpdaterArray = new VisualTreeUpdater.UpdaterArray();
			this.SetDefaultUpdaters();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00013F9C File Offset: 0x0001219C
		public void Dispose()
		{
			for (int i = 0; i < 7; i++)
			{
				IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[i];
				visualTreeUpdater.Dispose();
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00013FD0 File Offset: 0x000121D0
		public void UpdateVisualTree()
		{
			for (int i = 0; i < 7; i++)
			{
				IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[i];
				using (visualTreeUpdater.profilerMarker.Auto())
				{
					visualTreeUpdater.Update();
				}
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00014038 File Offset: 0x00012238
		public void UpdateVisualTreePhase(VisualTreeUpdatePhase phase)
		{
			IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[phase];
			using (visualTreeUpdater.profilerMarker.Auto())
			{
				visualTreeUpdater.Update();
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001408C File Offset: 0x0001228C
		public void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			for (int i = 0; i < 7; i++)
			{
				IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[i];
				visualTreeUpdater.OnVersionChanged(ve, versionChangeType);
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000140C2 File Offset: 0x000122C2
		public void SetUpdater(IVisualTreeUpdater updater, VisualTreeUpdatePhase phase)
		{
			IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[phase];
			if (visualTreeUpdater != null)
			{
				visualTreeUpdater.Dispose();
			}
			updater.panel = this.m_Panel;
			this.m_UpdaterArray[phase] = updater;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000140F8 File Offset: 0x000122F8
		public void SetUpdater<T>(VisualTreeUpdatePhase phase) where T : IVisualTreeUpdater, new()
		{
			IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[phase];
			if (visualTreeUpdater != null)
			{
				visualTreeUpdater.Dispose();
			}
			T t = new T();
			t.panel = this.m_Panel;
			T t2 = t;
			this.m_UpdaterArray[phase] = t2;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00014150 File Offset: 0x00012350
		public IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase)
		{
			return this.m_UpdaterArray[phase];
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001416E File Offset: 0x0001236E
		private void SetDefaultUpdaters()
		{
			this.SetUpdater<VisualTreeViewDataUpdater>(VisualTreeUpdatePhase.ViewData);
			this.SetUpdater<VisualTreeBindingsUpdater>(VisualTreeUpdatePhase.Bindings);
			this.SetUpdater<VisualElementAnimationSystem>(VisualTreeUpdatePhase.Animation);
			this.SetUpdater<VisualTreeStyleUpdater>(VisualTreeUpdatePhase.Styles);
			this.SetUpdater<UIRLayoutUpdater>(VisualTreeUpdatePhase.Layout);
			this.SetUpdater<VisualTreeTransformClipUpdater>(VisualTreeUpdatePhase.TransformClip);
			this.SetUpdater<UIRRepaintUpdater>(VisualTreeUpdatePhase.Repaint);
		}

		// Token: 0x0400023C RID: 572
		private BaseVisualElementPanel m_Panel;

		// Token: 0x0400023D RID: 573
		private VisualTreeUpdater.UpdaterArray m_UpdaterArray;

		// Token: 0x020000B3 RID: 179
		private class UpdaterArray
		{
			// Token: 0x06000537 RID: 1335 RVA: 0x000141A9 File Offset: 0x000123A9
			public UpdaterArray()
			{
				this.m_VisualTreeUpdaters = new IVisualTreeUpdater[7];
			}

			// Token: 0x1700012C RID: 300
			public IVisualTreeUpdater this[VisualTreeUpdatePhase phase]
			{
				get
				{
					return this.m_VisualTreeUpdaters[(int)phase];
				}
				set
				{
					this.m_VisualTreeUpdaters[(int)phase] = value;
				}
			}

			// Token: 0x1700012D RID: 301
			public IVisualTreeUpdater this[int index]
			{
				get
				{
					return this.m_VisualTreeUpdaters[index];
				}
				set
				{
					this.m_VisualTreeUpdaters[index] = value;
				}
			}

			// Token: 0x0400023E RID: 574
			private IVisualTreeUpdater[] m_VisualTreeUpdaters;
		}
	}
}
