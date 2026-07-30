using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000047 RID: 71
	public abstract class PointerManipulator : MouseManipulator
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00007624 File Offset: 0x00005824
		protected bool CanStartManipulation(IPointerEvent e)
		{
			foreach (ManipulatorActivationFilter manipulatorActivationFilter in base.activators)
			{
				bool flag = manipulatorActivationFilter.Matches(e);
				if (flag)
				{
					this.m_CurrentPointerId = e.pointerId;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007698 File Offset: 0x00005898
		protected bool CanStopManipulation(IPointerEvent e)
		{
			bool flag = e == null;
			return !flag && e.pointerId == this.m_CurrentPointerId;
		}

		// Token: 0x040000D4 RID: 212
		private int m_CurrentPointerId;
	}
}
