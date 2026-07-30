using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200009B RID: 155
	internal static class VisualElementDebugExtensions
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x00012038 File Offset: 0x00010238
		public static string GetDisplayName(this VisualElement ve, bool withHashCode = true)
		{
			bool flag = ve == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				string text2 = ve.GetType().Name;
				bool flag2 = !string.IsNullOrEmpty(ve.name);
				if (flag2)
				{
					text2 = text2 + "#" + ve.name;
				}
				if (withHashCode)
				{
					text2 = text2 + " (" + ve.GetHashCode().ToString("x8") + ")";
				}
				text = text2;
			}
			return text;
		}
	}
}
