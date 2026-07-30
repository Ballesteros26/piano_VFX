using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200012D RID: 301
	public abstract class CommandEventBase<T> : EventBase<T>, ICommandEvent where T : CommandEventBase<T>, new()
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00022C44 File Offset: 0x00020E44
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x00022C83 File Offset: 0x00020E83
		public string commandName
		{
			get
			{
				bool flag = this.m_CommandName == null && base.imguiEvent != null;
				string text;
				if (flag)
				{
					text = base.imguiEvent.commandName;
				}
				else
				{
					text = this.m_CommandName;
				}
				return text;
			}
			protected set
			{
				this.m_CommandName = value;
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00022C8D File Offset: 0x00020E8D
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00022C9E File Offset: 0x00020E9E
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
			this.commandName = null;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00022CB4 File Offset: 0x00020EB4
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			return pooled;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00022CDC File Offset: 0x00020EDC
		public static T GetPooled(string commandName)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.commandName = commandName;
			return pooled;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00022D02 File Offset: 0x00020F02
		protected CommandEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x040003E1 RID: 993
		private string m_CommandName;
	}
}
