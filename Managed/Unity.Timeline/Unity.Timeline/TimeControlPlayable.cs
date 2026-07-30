using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000037 RID: 55
	public class TimeControlPlayable : PlayableBehaviour
	{
		// Token: 0x06000283 RID: 643 RVA: 0x00008E28 File Offset: 0x00007028
		public static ScriptPlayable<TimeControlPlayable> Create(PlayableGraph graph, ITimeControl timeControl)
		{
			if (timeControl == null)
			{
				return ScriptPlayable<TimeControlPlayable>.Null;
			}
			ScriptPlayable<TimeControlPlayable> scriptPlayable = ScriptPlayable<TimeControlPlayable>.Create(graph, 0);
			scriptPlayable.GetBehaviour().Initialize(timeControl);
			return scriptPlayable;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008E54 File Offset: 0x00007054
		public void Initialize(ITimeControl timeControl)
		{
			this.m_timeControl = timeControl;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00008E5D File Offset: 0x0000705D
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (this.m_timeControl != null)
			{
				this.m_timeControl.SetTime(playable.GetTime<Playable>());
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00008E78 File Offset: 0x00007078
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (this.m_timeControl == null)
			{
				return;
			}
			if (!this.m_started)
			{
				this.m_timeControl.OnControlTimeStart();
				this.m_started = true;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00008E9D File Offset: 0x0000709D
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.m_timeControl == null)
			{
				return;
			}
			if (this.m_started)
			{
				this.m_timeControl.OnControlTimeStop();
				this.m_started = false;
			}
		}

		// Token: 0x040000DE RID: 222
		private ITimeControl m_timeControl;

		// Token: 0x040000DF RID: 223
		private bool m_started;
	}
}
