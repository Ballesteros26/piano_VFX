using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000060 RID: 96
	public sealed class PropertySheetFactory
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000E46F File Offset: 0x0000C66F
		public PropertySheetFactory()
		{
			this.m_Sheets = new Dictionary<Shader, PropertySheet>();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000E484 File Offset: 0x0000C684
		[Obsolete("Use PropertySheet.Get(Shader) with a direct reference to the Shader instead.")]
		public PropertySheet Get(string shaderName)
		{
			Shader shader = Shader.Find(shaderName);
			if (shader == null)
			{
				throw new ArgumentException(string.Format("Invalid shader ({0})", shaderName));
			}
			return this.Get(shader);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		public PropertySheet Get(Shader shader)
		{
			if (shader == null)
			{
				throw new ArgumentException(string.Format("Invalid shader ({0})", shader));
			}
			PropertySheet propertySheet;
			if (this.m_Sheets.TryGetValue(shader, out propertySheet))
			{
				return propertySheet;
			}
			string name = shader.name;
			propertySheet = new PropertySheet(new Material(shader)
			{
				name = string.Format("PostProcess - {0}", name.Substring(name.LastIndexOf('/') + 1)),
				hideFlags = HideFlags.DontSave
			});
			this.m_Sheets.Add(shader, propertySheet);
			return propertySheet;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000E540 File Offset: 0x0000C740
		public void Release()
		{
			foreach (PropertySheet propertySheet in this.m_Sheets.Values)
			{
				propertySheet.Release();
			}
			this.m_Sheets.Clear();
		}

		// Token: 0x040001A0 RID: 416
		private readonly Dictionary<Shader, PropertySheet> m_Sheets;
	}
}
