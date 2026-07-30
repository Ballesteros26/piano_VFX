using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004D RID: 77
	public abstract class AbstractEventData
	{
		// Token: 0x060004E2 RID: 1250 RVA: 0x00016452 File Offset: 0x00014652
		public virtual void Reset()
		{
			this.m_Used = false;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001645B File Offset: 0x0001465B
		public virtual void Use()
		{
			this.m_Used = true;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00016464 File Offset: 0x00014664
		public virtual bool used
		{
			get
			{
				return this.m_Used;
			}
		}

		// Token: 0x0400019D RID: 413
		protected bool m_Used;
	}
}
