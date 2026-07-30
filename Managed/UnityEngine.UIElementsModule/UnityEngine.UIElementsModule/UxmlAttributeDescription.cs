using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E3 RID: 483
	public abstract class UxmlAttributeDescription
	{
		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003790D File Offset: 0x00035B0D
		protected UxmlAttributeDescription()
		{
			this.use = UxmlAttributeDescription.Use.Optional;
			this.restriction = null;
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00037927 File Offset: 0x00035B27
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x0003792F File Offset: 0x00035B2F
		public string name { get; set; }

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00037938 File Offset: 0x00035B38
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x00037950 File Offset: 0x00035B50
		public IEnumerable<string> obsoleteNames
		{
			get
			{
				return this.m_ObsoleteNames;
			}
			set
			{
				this.m_ObsoleteNames = Enumerable.ToArray<string>(value);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x0003795F File Offset: 0x00035B5F
		// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x00037967 File Offset: 0x00035B67
		public string type { get; protected set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00037970 File Offset: 0x00035B70
		// (set) Token: 0x06000EFA RID: 3834 RVA: 0x00037978 File Offset: 0x00035B78
		public string typeNamespace { get; protected set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000EFB RID: 3835
		public abstract string defaultValueAsString { get; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00037981 File Offset: 0x00035B81
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x00037989 File Offset: 0x00035B89
		public UxmlAttributeDescription.Use use { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00037992 File Offset: 0x00035B92
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x0003799A File Offset: 0x00035B9A
		public UxmlTypeRestriction restriction { get; set; }

		// Token: 0x06000F00 RID: 3840 RVA: 0x000379A4 File Offset: 0x00035BA4
		internal bool TryGetValueFromBagAsString(IUxmlAttributes bag, CreationContext cc, out string value)
		{
			bool flag = this.name == null && (this.m_ObsoleteNames == null || this.m_ObsoleteNames.Length == 0);
			bool flag2;
			if (flag)
			{
				Debug.LogError("Attribute description has no name.");
				value = null;
				flag2 = false;
			}
			else
			{
				string text;
				bag.TryGetAttributeValue("name", out text);
				bool flag3 = !string.IsNullOrEmpty(text) && cc.attributeOverrides != null;
				if (flag3)
				{
					for (int i = 0; i < cc.attributeOverrides.Count; i++)
					{
						bool flag4 = cc.attributeOverrides[i].m_ElementName != text;
						if (!flag4)
						{
							bool flag5 = cc.attributeOverrides[i].m_AttributeName != this.name;
							if (flag5)
							{
								bool flag6 = this.m_ObsoleteNames != null;
								if (!flag6)
								{
									goto IL_0147;
								}
								bool flag7 = false;
								for (int j = 0; j < this.m_ObsoleteNames.Length; j++)
								{
									bool flag8 = cc.attributeOverrides[i].m_AttributeName == this.m_ObsoleteNames[j];
									if (flag8)
									{
										flag7 = true;
										break;
									}
								}
								bool flag9 = !flag7;
								if (flag9)
								{
									goto IL_0147;
								}
							}
							value = cc.attributeOverrides[i].m_Value;
							return true;
						}
						IL_0147:;
					}
				}
				bool flag10 = this.name == null;
				if (flag10)
				{
					for (int k = 0; k < this.m_ObsoleteNames.Length; k++)
					{
						bool flag11 = bag.TryGetAttributeValue(this.m_ObsoleteNames[k], out value);
						if (flag11)
						{
							bool flag12 = cc.visualTreeAsset != null;
							if (flag12)
							{
							}
							return true;
						}
					}
					value = null;
					flag2 = false;
				}
				else
				{
					bool flag13 = !bag.TryGetAttributeValue(this.name, out value);
					if (flag13)
					{
						bool flag14 = this.m_ObsoleteNames != null;
						if (flag14)
						{
							for (int l = 0; l < this.m_ObsoleteNames.Length; l++)
							{
								bool flag15 = bag.TryGetAttributeValue(this.m_ObsoleteNames[l], out value);
								if (flag15)
								{
									bool flag16 = cc.visualTreeAsset != null;
									if (flag16)
									{
									}
									return true;
								}
							}
						}
						value = null;
						flag2 = false;
					}
					else
					{
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00037C08 File Offset: 0x00035E08
		protected bool TryGetValueFromBag<T>(IUxmlAttributes bag, CreationContext cc, Func<string, T, T> converterFunc, T defaultValue, ref T value)
		{
			string text;
			bool flag = this.TryGetValueFromBagAsString(bag, cc, out text);
			bool flag3;
			if (flag)
			{
				bool flag2 = converterFunc != null;
				if (flag2)
				{
					value = converterFunc.Invoke(text, defaultValue);
				}
				else
				{
					value = defaultValue;
				}
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00037C58 File Offset: 0x00035E58
		protected T GetValueFromBag<T>(IUxmlAttributes bag, CreationContext cc, Func<string, T, T> converterFunc, T defaultValue)
		{
			bool flag = converterFunc == null;
			if (flag)
			{
				throw new ArgumentNullException("converterFunc");
			}
			string text;
			bool flag2 = this.TryGetValueFromBagAsString(bag, cc, out text);
			T t;
			if (flag2)
			{
				t = converterFunc.Invoke(text, defaultValue);
			}
			else
			{
				t = defaultValue;
			}
			return t;
		}

		// Token: 0x0400061C RID: 1564
		protected const string xmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x0400061E RID: 1566
		private string[] m_ObsoleteNames;

		// Token: 0x020001E4 RID: 484
		public enum Use
		{
			// Token: 0x04000624 RID: 1572
			None,
			// Token: 0x04000625 RID: 1573
			Optional,
			// Token: 0x04000626 RID: 1574
			Prohibited,
			// Token: 0x04000627 RID: 1575
			Required
		}
	}
}
