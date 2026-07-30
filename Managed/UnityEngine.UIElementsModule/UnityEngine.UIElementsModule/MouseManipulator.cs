using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000033 RID: 51
	public abstract class MouseManipulator : Manipulator
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005DAF File Offset: 0x00003FAF
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00005DB7 File Offset: 0x00003FB7
		public List<ManipulatorActivationFilter> activators { get; private set; }

		// Token: 0x0600011F RID: 287 RVA: 0x00005DC0 File Offset: 0x00003FC0
		protected MouseManipulator()
		{
			this.activators = new List<ManipulatorActivationFilter>();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005DD8 File Offset: 0x00003FD8
		protected bool CanStartManipulation(IMouseEvent e)
		{
			foreach (ManipulatorActivationFilter manipulatorActivationFilter in this.activators)
			{
				bool flag = manipulatorActivationFilter.Matches(e);
				if (flag)
				{
					this.m_currentActivator = manipulatorActivationFilter;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005E48 File Offset: 0x00004048
		protected bool CanStopManipulation(IMouseEvent e)
		{
			bool flag = e == null;
			return !flag && e.button == (int)this.m_currentActivator.button;
		}

		// Token: 0x04000081 RID: 129
		private ManipulatorActivationFilter m_currentActivator;
	}
}
