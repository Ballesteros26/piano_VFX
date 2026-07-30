using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004E RID: 78
	public class BaseEventData : AbstractEventData
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x0001646C File Offset: 0x0001466C
		public BaseEventData(EventSystem eventSystem)
		{
			this.m_EventSystem = eventSystem;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001647B File Offset: 0x0001467B
		public BaseInputModule currentInputModule
		{
			get
			{
				return this.m_EventSystem.currentInputModule;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00016488 File Offset: 0x00014688
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00016495 File Offset: 0x00014695
		public GameObject selectedObject
		{
			get
			{
				return this.m_EventSystem.currentSelectedGameObject;
			}
			set
			{
				this.m_EventSystem.SetSelectedGameObject(value, this);
			}
		}

		// Token: 0x0400019E RID: 414
		private readonly EventSystem m_EventSystem;
	}
}
