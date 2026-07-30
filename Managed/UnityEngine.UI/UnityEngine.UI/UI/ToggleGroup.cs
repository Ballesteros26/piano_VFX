using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200003B RID: 59
	[AddComponentMenu("UI/Toggle Group", 32)]
	[DisallowMultipleComponent]
	public class ToggleGroup : UIBehaviour
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00014C01 File Offset: 0x00012E01
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00014C09 File Offset: 0x00012E09
		public bool allowSwitchOff
		{
			get
			{
				return this.m_AllowSwitchOff;
			}
			set
			{
				this.m_AllowSwitchOff = value;
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00014C12 File Offset: 0x00012E12
		protected ToggleGroup()
		{
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00014C25 File Offset: 0x00012E25
		protected override void Start()
		{
			this.EnsureValidState();
			base.Start();
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00014C33 File Offset: 0x00012E33
		protected override void OnEnable()
		{
			this.EnsureValidState();
			base.OnEnable();
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00014C41 File Offset: 0x00012E41
		private void ValidateToggleIsInGroup(Toggle toggle)
		{
			if (toggle == null || !this.m_Toggles.Contains(toggle))
			{
				throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", new object[] { toggle, this }));
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00014C78 File Offset: 0x00012E78
		public void NotifyToggleOn(Toggle toggle, bool sendCallback = true)
		{
			this.ValidateToggleIsInGroup(toggle);
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				if (!(this.m_Toggles[i] == toggle))
				{
					if (sendCallback)
					{
						this.m_Toggles[i].isOn = false;
					}
					else
					{
						this.m_Toggles[i].SetIsOnWithoutNotify(false);
					}
				}
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00014CDF File Offset: 0x00012EDF
		public void UnregisterToggle(Toggle toggle)
		{
			if (this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Remove(toggle);
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00014CFC File Offset: 0x00012EFC
		public void RegisterToggle(Toggle toggle)
		{
			if (!this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Add(toggle);
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00014D18 File Offset: 0x00012F18
		public void EnsureValidState()
		{
			if (!this.allowSwitchOff && !this.AnyTogglesOn() && this.m_Toggles.Count != 0)
			{
				this.m_Toggles[0].isOn = true;
				this.NotifyToggleOn(this.m_Toggles[0], true);
			}
			IEnumerable<Toggle> enumerable = this.ActiveToggles();
			if (enumerable.Count<Toggle>() > 1)
			{
				Toggle firstActiveToggle = this.GetFirstActiveToggle();
				foreach (Toggle toggle in enumerable)
				{
					if (!(toggle == firstActiveToggle))
					{
						toggle.isOn = false;
					}
				}
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00014DC4 File Offset: 0x00012FC4
		public bool AnyTogglesOn()
		{
			return this.m_Toggles.Find((Toggle x) => x.isOn) != null;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00014DF6 File Offset: 0x00012FF6
		public IEnumerable<Toggle> ActiveToggles()
		{
			return this.m_Toggles.Where((Toggle x) => x.isOn);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00014E24 File Offset: 0x00013024
		public Toggle GetFirstActiveToggle()
		{
			IEnumerable<Toggle> enumerable = this.ActiveToggles();
			if (enumerable.Count<Toggle>() <= 0)
			{
				return null;
			}
			return enumerable.First<Toggle>();
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00014E4C File Offset: 0x0001304C
		public void SetAllTogglesOff(bool sendCallback = true)
		{
			bool allowSwitchOff = this.m_AllowSwitchOff;
			this.m_AllowSwitchOff = true;
			if (sendCallback)
			{
				for (int i = 0; i < this.m_Toggles.Count; i++)
				{
					this.m_Toggles[i].isOn = false;
				}
			}
			else
			{
				for (int j = 0; j < this.m_Toggles.Count; j++)
				{
					this.m_Toggles[j].SetIsOnWithoutNotify(false);
				}
			}
			this.m_AllowSwitchOff = allowSwitchOff;
		}

		// Token: 0x0400016D RID: 365
		[SerializeField]
		private bool m_AllowSwitchOff;

		// Token: 0x0400016E RID: 366
		protected List<Toggle> m_Toggles = new List<Toggle>();
	}
}
