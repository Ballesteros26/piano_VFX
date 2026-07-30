using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200016C RID: 364
	[NativeHeader("Runtime/Export/Math/Gradient.bindings.h")]
	[RequiredByNativeCode]
	[StructLayout(0)]
	public class Gradient : IEquatable<Gradient>
	{
		// Token: 0x06001071 RID: 4209
		[FreeFunction(Name = "Gradient_Bindings::Init", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Init();

		// Token: 0x06001072 RID: 4210
		[FreeFunction(Name = "Gradient_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x06001073 RID: 4211
		[FreeFunction("Gradient_Bindings::Internal_Equals", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern bool Internal_Equals(IntPtr other);

		// Token: 0x06001074 RID: 4212 RVA: 0x00017DDB File Offset: 0x00015FDB
		[RequiredByNativeCode]
		public Gradient()
		{
			this.m_Ptr = Gradient.Init();
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00017DF0 File Offset: 0x00015FF0
		~Gradient()
		{
			this.Cleanup();
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00017E20 File Offset: 0x00016020
		[FreeFunction(Name = "Gradient_Bindings::Evaluate", IsThreadSafe = true, HasExplicitThis = true)]
		public Color Evaluate(float time)
		{
			Color color;
			this.Evaluate_Injected(time, out color);
			return color;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001077 RID: 4215
		// (set) Token: 0x06001078 RID: 4216
		public extern GradientColorKey[] colorKeys
		{
			[FreeFunction("Gradient_Bindings::GetColorKeys", IsThreadSafe = true, HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction("Gradient_Bindings::SetColorKeys", IsThreadSafe = true, HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001079 RID: 4217
		// (set) Token: 0x0600107A RID: 4218
		public extern GradientAlphaKey[] alphaKeys
		{
			[FreeFunction("Gradient_Bindings::GetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction("Gradient_Bindings::SetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600107B RID: 4219
		// (set) Token: 0x0600107C RID: 4220
		public extern GradientMode mode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600107D RID: 4221
		[FreeFunction(Name = "Gradient_Bindings::SetKeys", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys);

		// Token: 0x0600107E RID: 4222 RVA: 0x00017E38 File Offset: 0x00016038
		public override bool Equals(object o)
		{
			bool flag = o == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == o;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = o.GetType() != base.GetType();
					flag2 = !flag4 && this.Equals((Gradient)o);
				}
			}
			return flag2;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00017E8C File Offset: 0x0001608C
		public bool Equals(Gradient other)
		{
			bool flag = other == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == other;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = this.m_Ptr.Equals(other.m_Ptr);
					flag2 = flag4 || this.Internal_Equals(other.m_Ptr);
				}
			}
			return flag2;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00017EE4 File Offset: 0x000160E4
		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		// Token: 0x06001081 RID: 4225
		[MethodImpl(4096)]
		private extern void Evaluate_Injected(float time, out Color ret);

		// Token: 0x040005BA RID: 1466
		internal IntPtr m_Ptr;
	}
}
