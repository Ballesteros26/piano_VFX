using System;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	[AttributeUsage(64)]
	public class GUITargetAttribute : Attribute
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000BA9B File Offset: 0x00009C9B
		public GUITargetAttribute()
		{
			this.displayMask = -1;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000BAAC File Offset: 0x00009CAC
		public GUITargetAttribute(int displayIndex)
		{
			this.displayMask = 1 << displayIndex;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000BAC2 File Offset: 0x00009CC2
		public GUITargetAttribute(int displayIndex, int displayIndex1)
		{
			this.displayMask = (1 << displayIndex) | (1 << displayIndex1);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		public GUITargetAttribute(int displayIndex, int displayIndex1, params int[] displayIndexList)
		{
			this.displayMask = (1 << displayIndex) | (1 << displayIndex1);
			for (int i = 0; i < displayIndexList.Length; i++)
			{
				this.displayMask |= 1 << displayIndexList[i];
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000BB30 File Offset: 0x00009D30
		[RequiredByNativeCode]
		private static int GetGUITargetAttrValue(Type klass, string methodName)
		{
			MethodInfo method = klass.GetMethod(methodName, 52);
			bool flag = method != null;
			if (flag)
			{
				object[] customAttributes = method.GetCustomAttributes(true);
				bool flag2 = customAttributes != null;
				if (flag2)
				{
					for (int i = 0; i < customAttributes.Length; i++)
					{
						bool flag3 = customAttributes[i].GetType() != typeof(GUITargetAttribute);
						if (!flag3)
						{
							GUITargetAttribute guitargetAttribute = customAttributes[i] as GUITargetAttribute;
							return guitargetAttribute.displayMask;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x040000DD RID: 221
		internal int displayMask;
	}
}
