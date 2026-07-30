using System;
using System.ComponentModel;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002E RID: 46
	[RequiredByNativeCode]
	[NativeType(CodegenOptions.Custom, "MonoSkeletonBone")]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	public struct SkeletonBone
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00003B14 File Offset: 0x00001D14
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00002059 File Offset: 0x00000259
		[EditorBrowsable(1)]
		[Obsolete("transformModified is no longer used and has been deprecated.", true)]
		public int transformModified
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x04000108 RID: 264
		[NativeName("m_Name")]
		public string name;

		// Token: 0x04000109 RID: 265
		[NativeName("m_ParentName")]
		internal string parentName;

		// Token: 0x0400010A RID: 266
		[NativeName("m_Position")]
		public Vector3 position;

		// Token: 0x0400010B RID: 267
		[NativeName("m_Rotation")]
		public Quaternion rotation;

		// Token: 0x0400010C RID: 268
		[NativeName("m_Scale")]
		public Vector3 scale;
	}
}
