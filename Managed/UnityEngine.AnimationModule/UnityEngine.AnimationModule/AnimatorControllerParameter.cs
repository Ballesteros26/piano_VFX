using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001F RID: 31
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerParameter.bindings.h")]
	[NativeHeader("Modules/Animation/AnimatorControllerParameter.h")]
	[NativeAsStruct]
	[UsedByNativeCode]
	[NativeType(CodegenOptions.Custom, "MonoAnimatorControllerParameter")]
	[StructLayout(0)]
	public class AnimatorControllerParameter
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00003618 File Offset: 0x00001818
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00003630 File Offset: 0x00001830
		public int nameHash
		{
			get
			{
				return Animator.StringToHash(this.m_Name);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00003650 File Offset: 0x00001850
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00003668 File Offset: 0x00001868
		public AnimatorControllerParameterType type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00003674 File Offset: 0x00001874
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000368C File Offset: 0x0000188C
		public float defaultFloat
		{
			get
			{
				return this.m_DefaultFloat;
			}
			set
			{
				this.m_DefaultFloat = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00003698 File Offset: 0x00001898
		// (set) Token: 0x060001DB RID: 475 RVA: 0x000036B0 File Offset: 0x000018B0
		public int defaultInt
		{
			get
			{
				return this.m_DefaultInt;
			}
			set
			{
				this.m_DefaultInt = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000036BC File Offset: 0x000018BC
		// (set) Token: 0x060001DD RID: 477 RVA: 0x000036D4 File Offset: 0x000018D4
		public bool defaultBool
		{
			get
			{
				return this.m_DefaultBool;
			}
			set
			{
				this.m_DefaultBool = value;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000036E0 File Offset: 0x000018E0
		public override bool Equals(object o)
		{
			AnimatorControllerParameter animatorControllerParameter = o as AnimatorControllerParameter;
			return animatorControllerParameter != null && this.m_Name == animatorControllerParameter.m_Name && this.m_Type == animatorControllerParameter.m_Type && this.m_DefaultFloat == animatorControllerParameter.m_DefaultFloat && this.m_DefaultInt == animatorControllerParameter.m_DefaultInt && this.m_DefaultBool == animatorControllerParameter.m_DefaultBool;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000374C File Offset: 0x0000194C
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x04000067 RID: 103
		internal string m_Name = "";

		// Token: 0x04000068 RID: 104
		internal AnimatorControllerParameterType m_Type;

		// Token: 0x04000069 RID: 105
		internal float m_DefaultFloat;

		// Token: 0x0400006A RID: 106
		internal int m_DefaultInt;

		// Token: 0x0400006B RID: 107
		internal bool m_DefaultBool;
	}
}
