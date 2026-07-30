using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000A4 RID: 164
	internal class VisualElementPanelActivator
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00012AB8 File Offset: 0x00010CB8
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00012AC0 File Offset: 0x00010CC0
		public bool isActive { get; private set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00012AC9 File Offset: 0x00010CC9
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00012AD1 File Offset: 0x00010CD1
		public bool isDetaching { get; private set; }

		// Token: 0x060004E4 RID: 1252 RVA: 0x00012ADA File Offset: 0x00010CDA
		public VisualElementPanelActivator(IVisualElementPanelActivatable activatable)
		{
			this.m_Activatable = activatable;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00012AEC File Offset: 0x00010CEC
		public void SetActive(bool action)
		{
			bool flag = this.isActive != action;
			if (flag)
			{
				this.isActive = action;
				bool isActive = this.isActive;
				if (isActive)
				{
					this.m_Activatable.element.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnEnter), TrickleDown.NoTrickleDown);
					this.m_Activatable.element.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnLeave), TrickleDown.NoTrickleDown);
					this.SendActivation();
				}
				else
				{
					this.m_Activatable.element.UnregisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnEnter), TrickleDown.NoTrickleDown);
					this.m_Activatable.element.UnregisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnLeave), TrickleDown.NoTrickleDown);
					this.SendDeactivation();
				}
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00012BB0 File Offset: 0x00010DB0
		public void SendActivation()
		{
			bool flag = this.m_Activatable.CanBeActivated();
			if (flag)
			{
				this.m_Activatable.OnPanelActivate();
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00012BDC File Offset: 0x00010DDC
		public void SendDeactivation()
		{
			bool flag = this.m_Activatable.CanBeActivated();
			if (flag)
			{
				this.m_Activatable.OnPanelDeactivate();
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00012C08 File Offset: 0x00010E08
		private void OnEnter(AttachToPanelEvent evt)
		{
			bool isActive = this.isActive;
			if (isActive)
			{
				this.SendActivation();
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00012C2C File Offset: 0x00010E2C
		private void OnLeave(DetachFromPanelEvent evt)
		{
			bool isActive = this.isActive;
			if (isActive)
			{
				this.isDetaching = true;
				try
				{
					this.SendDeactivation();
				}
				finally
				{
					this.isDetaching = false;
				}
			}
		}

		// Token: 0x04000202 RID: 514
		private IVisualElementPanelActivatable m_Activatable;
	}
}
