using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000C1 RID: 193
	[UsedByNativeCode(Name = "ExposedReference")]
	[Serializable]
	public struct ExposedReference<T> where T : Object
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x00006E10 File Offset: 0x00005010
		public T Resolve(IExposedPropertyTable resolver)
		{
			bool flag = resolver != null;
			if (flag)
			{
				bool flag2;
				Object referenceValue = resolver.GetReferenceValue(this.exposedName, out flag2);
				bool flag3 = flag2;
				if (flag3)
				{
					return referenceValue as T;
				}
			}
			return this.defaultValue as T;
		}

		// Token: 0x04000235 RID: 565
		[SerializeField]
		public PropertyName exposedName;

		// Token: 0x04000236 RID: 566
		[SerializeField]
		public Object defaultValue;
	}
}
