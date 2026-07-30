using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000126 RID: 294
	public abstract class MouseCaptureEventBase<T> : PointerCaptureEventBase<T>, IMouseCaptureEvent where T : MouseCaptureEventBase<T>, new()
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00022A4F File Offset: 0x00020C4F
		public new IEventHandler relatedTarget
		{
			get
			{
				return base.relatedTarget;
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00022A58 File Offset: 0x00020C58
		public static T GetPooled(IEventHandler target, IEventHandler relatedTarget)
		{
			return PointerCaptureEventBase<T>.GetPooled(target, relatedTarget, 0);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00022A74 File Offset: 0x00020C74
		protected override void Init()
		{
			base.Init();
		}
	}
}
