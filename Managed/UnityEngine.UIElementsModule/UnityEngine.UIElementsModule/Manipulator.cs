using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000030 RID: 48
	public abstract class Manipulator : IManipulator
	{
		// Token: 0x06000112 RID: 274
		protected abstract void RegisterCallbacksOnTarget();

		// Token: 0x06000113 RID: 275
		protected abstract void UnregisterCallbacksFromTarget();

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005C48 File Offset: 0x00003E48
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00005C60 File Offset: 0x00003E60
		public VisualElement target
		{
			get
			{
				return this.m_Target;
			}
			set
			{
				bool flag = this.target != null;
				if (flag)
				{
					this.UnregisterCallbacksFromTarget();
				}
				this.m_Target = value;
				bool flag2 = this.target != null;
				if (flag2)
				{
					this.RegisterCallbacksOnTarget();
				}
			}
		}

		// Token: 0x04000079 RID: 121
		private VisualElement m_Target;
	}
}
