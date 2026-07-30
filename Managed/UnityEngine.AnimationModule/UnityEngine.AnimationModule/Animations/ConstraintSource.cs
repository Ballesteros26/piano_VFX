using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000060 RID: 96
	[NativeType(CodegenOptions = CodegenOptions.Custom, Header = "Modules/Animation/Constraints/ConstraintSource.h", IntermediateScriptingStructName = "MonoConstraintSource")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[UsedByNativeCode]
	[Serializable]
	public struct ConstraintSource
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00007AB8 File Offset: 0x00005CB8
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public Transform sourceTransform
		{
			get
			{
				return this.m_SourceTransform;
			}
			set
			{
				this.m_SourceTransform = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00007ADC File Offset: 0x00005CDC
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00007AF4 File Offset: 0x00005CF4
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				this.m_Weight = value;
			}
		}

		// Token: 0x04000180 RID: 384
		[NativeName("sourceTransform")]
		private Transform m_SourceTransform;

		// Token: 0x04000181 RID: 385
		[NativeName("weight")]
		private float m_Weight;
	}
}
