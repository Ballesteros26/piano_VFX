using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	internal class VisualElementAsset : IUxmlAttributes, ISerializationCallbackReceiver
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00039558 File Offset: 0x00037758
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x00039570 File Offset: 0x00037770
		public int id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x0003957C File Offset: 0x0003777C
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x00039594 File Offset: 0x00037794
		public int orderInDocument
		{
			get
			{
				return this.m_OrderInDocument;
			}
			set
			{
				this.m_OrderInDocument = value;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x000395A0 File Offset: 0x000377A0
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x000395B8 File Offset: 0x000377B8
		public int parentId
		{
			get
			{
				return this.m_ParentId;
			}
			set
			{
				this.m_ParentId = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x000395C4 File Offset: 0x000377C4
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x000395DC File Offset: 0x000377DC
		public int ruleIndex
		{
			get
			{
				return this.m_RuleIndex;
			}
			set
			{
				this.m_RuleIndex = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x000395E8 File Offset: 0x000377E8
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x00039600 File Offset: 0x00037800
		public string fullTypeName
		{
			get
			{
				return this.m_FullTypeName;
			}
			set
			{
				this.m_FullTypeName = value;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0003960C File Offset: 0x0003780C
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x00039624 File Offset: 0x00037824
		public string[] classes
		{
			get
			{
				return this.m_Classes;
			}
			set
			{
				this.m_Classes = value;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00039630 File Offset: 0x00037830
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x0003965A File Offset: 0x0003785A
		public List<string> stylesheetPaths
		{
			get
			{
				List<string> list;
				if ((list = this.m_StylesheetPaths) == null)
				{
					list = (this.m_StylesheetPaths = new List<string>());
				}
				return list;
			}
			set
			{
				this.m_StylesheetPaths = value;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00039664 File Offset: 0x00037864
		public bool hasStylesheetPaths
		{
			get
			{
				return this.m_StylesheetPaths != null;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x00039670 File Offset: 0x00037870
		// (set) Token: 0x06000FCF RID: 4047 RVA: 0x0003969A File Offset: 0x0003789A
		public List<StyleSheet> stylesheets
		{
			get
			{
				List<StyleSheet> list;
				if ((list = this.m_Stylesheets) == null)
				{
					list = (this.m_Stylesheets = new List<StyleSheet>());
				}
				return list;
			}
			set
			{
				this.m_Stylesheets = value;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x000396A4 File Offset: 0x000378A4
		public bool hasStylesheets
		{
			get
			{
				return this.m_Stylesheets != null;
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000396AF File Offset: 0x000378AF
		public VisualElementAsset(string fullTypeName)
		{
			this.m_FullTypeName = fullTypeName;
			this.m_Name = string.Empty;
			this.m_Text = string.Empty;
			this.m_PickingMode = PickingMode.Position;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x000062F3 File Offset: 0x000044F3
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x000396E0 File Offset: 0x000378E0
		public void OnAfterDeserialize()
		{
			bool flag = !string.IsNullOrEmpty(this.m_Name) && !this.m_Properties.Contains("name");
			if (flag)
			{
				this.AddProperty("name", this.m_Name);
			}
			bool flag2 = !string.IsNullOrEmpty(this.m_Text) && !this.m_Properties.Contains("text");
			if (flag2)
			{
				this.AddProperty("text", this.m_Text);
			}
			bool flag3 = this.m_PickingMode != PickingMode.Position && !this.m_Properties.Contains("picking-mode") && !this.m_Properties.Contains("pickingMode");
			if (flag3)
			{
				this.AddProperty("picking-mode", this.m_PickingMode.ToString());
			}
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x000397B7 File Offset: 0x000379B7
		public void AddProperty(string propertyName, string propertyValue)
		{
			this.SetOrAddProperty(propertyName, propertyValue);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000397C4 File Offset: 0x000379C4
		private void SetOrAddProperty(string propertyName, string propertyValue)
		{
			bool flag = this.m_Properties == null;
			if (flag)
			{
				this.m_Properties = new List<string>();
			}
			for (int i = 0; i < this.m_Properties.Count - 1; i += 2)
			{
				bool flag2 = this.m_Properties[i] == propertyName;
				if (flag2)
				{
					this.m_Properties[i + 1] = propertyValue;
					return;
				}
			}
			this.m_Properties.Add(propertyName);
			this.m_Properties.Add(propertyValue);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0003984C File Offset: 0x00037A4C
		public bool TryGetAttributeValue(string propertyName, out string value)
		{
			bool flag = this.m_Properties == null;
			bool flag2;
			if (flag)
			{
				value = null;
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.m_Properties.Count - 1; i += 2)
				{
					bool flag3 = this.m_Properties[i] == propertyName;
					if (flag3)
					{
						value = this.m_Properties[i + 1];
						return true;
					}
				}
				value = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x04000682 RID: 1666
		[SerializeField]
		private string m_Name;

		// Token: 0x04000683 RID: 1667
		[SerializeField]
		private int m_Id;

		// Token: 0x04000684 RID: 1668
		[SerializeField]
		private int m_OrderInDocument;

		// Token: 0x04000685 RID: 1669
		[SerializeField]
		private int m_ParentId;

		// Token: 0x04000686 RID: 1670
		[SerializeField]
		private int m_RuleIndex;

		// Token: 0x04000687 RID: 1671
		[SerializeField]
		private string m_Text;

		// Token: 0x04000688 RID: 1672
		[SerializeField]
		private PickingMode m_PickingMode;

		// Token: 0x04000689 RID: 1673
		[SerializeField]
		private string m_FullTypeName;

		// Token: 0x0400068A RID: 1674
		[SerializeField]
		private string[] m_Classes;

		// Token: 0x0400068B RID: 1675
		[SerializeField]
		private List<string> m_StylesheetPaths;

		// Token: 0x0400068C RID: 1676
		[SerializeField]
		private List<StyleSheet> m_Stylesheets;

		// Token: 0x0400068D RID: 1677
		[SerializeField]
		private List<string> m_Properties;
	}
}
