using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000A9 RID: 169
	public class DebugUIHandlerValue : DebugUIHandlerWidget
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x0001022B File Offset: 0x0000E42B
		protected override void OnEnable()
		{
			this.m_Timer = 0f;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00010238 File Offset: 0x0000E438
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Value>();
			this.nameLabel.text = this.m_Field.displayName;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00010263 File Offset: 0x0000E463
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00010288 File Offset: 0x0000E488
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000102AC File Offset: 0x0000E4AC
		private void Update()
		{
			if (this.m_Timer >= this.m_Field.refreshRate)
			{
				this.valueLabel.text = this.m_Field.GetValue().ToString();
				this.m_Timer -= this.m_Field.refreshRate;
			}
			this.m_Timer += Time.deltaTime;
		}

		// Token: 0x04000226 RID: 550
		public Text nameLabel;

		// Token: 0x04000227 RID: 551
		public Text valueLabel;

		// Token: 0x04000228 RID: 552
		private DebugUI.Value m_Field;

		// Token: 0x04000229 RID: 553
		private float m_Timer;
	}
}
