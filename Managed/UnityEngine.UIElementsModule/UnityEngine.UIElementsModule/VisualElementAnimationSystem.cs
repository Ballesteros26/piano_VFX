using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020000A7 RID: 167
	internal class VisualElementAnimationSystem : BaseVisualTreeUpdater
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x00013038 File Offset: 0x00011238
		private long CurrentTimeMs()
		{
			return Panel.TimeSinceStartupMs();
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001304F File Offset: 0x0001124F
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualElementAnimationSystem.s_ProfilerMarker;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00013056 File Offset: 0x00011256
		public void UnregisterAnimation(IValueAnimationUpdate anim)
		{
			this.m_Animations.Remove(anim);
			this.m_IterationListDirty = true;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00013070 File Offset: 0x00011270
		public void UnregisterAnimations(List<IValueAnimationUpdate> anims)
		{
			foreach (IValueAnimationUpdate valueAnimationUpdate in anims)
			{
				this.m_Animations.Remove(valueAnimationUpdate);
			}
			this.m_IterationListDirty = true;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000130D0 File Offset: 0x000112D0
		public void RegisterAnimation(IValueAnimationUpdate anim)
		{
			this.m_Animations.Add(anim);
			this.m_HasNewAnimations = true;
			this.m_IterationListDirty = true;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000130F0 File Offset: 0x000112F0
		public void RegisterAnimations(List<IValueAnimationUpdate> anims)
		{
			foreach (IValueAnimationUpdate valueAnimationUpdate in anims)
			{
				this.m_Animations.Add(valueAnimationUpdate);
			}
			this.m_HasNewAnimations = true;
			this.m_IterationListDirty = true;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00013158 File Offset: 0x00011358
		public override void Update()
		{
			long num = Panel.TimeSinceStartupMs();
			bool iterationListDirty = this.m_IterationListDirty;
			if (iterationListDirty)
			{
				this.m_IterationList = Enumerable.ToList<IValueAnimationUpdate>(this.m_Animations);
				this.m_IterationListDirty = false;
			}
			bool flag = this.m_HasNewAnimations || this.lastUpdate != num;
			if (flag)
			{
				foreach (IValueAnimationUpdate valueAnimationUpdate in this.m_IterationList)
				{
					valueAnimationUpdate.Tick(num);
				}
				this.m_HasNewAnimations = false;
				this.lastUpdate = num;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000062F3 File Offset: 0x000044F3
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
		}

		// Token: 0x04000207 RID: 519
		private HashSet<IValueAnimationUpdate> m_Animations = new HashSet<IValueAnimationUpdate>();

		// Token: 0x04000208 RID: 520
		private List<IValueAnimationUpdate> m_IterationList = new List<IValueAnimationUpdate>();

		// Token: 0x04000209 RID: 521
		private bool m_HasNewAnimations = false;

		// Token: 0x0400020A RID: 522
		private bool m_IterationListDirty = false;

		// Token: 0x0400020B RID: 523
		private static readonly string s_Description = "Animation Update";

		// Token: 0x0400020C RID: 524
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualElementAnimationSystem.s_Description);

		// Token: 0x0400020D RID: 525
		private long lastUpdate;
	}
}
