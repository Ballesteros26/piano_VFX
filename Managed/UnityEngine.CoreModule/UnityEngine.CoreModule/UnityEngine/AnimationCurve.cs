using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200009B RID: 155
	[NativeHeader("Runtime/Math/AnimationCurve.bindings.h")]
	[RequiredByNativeCode]
	[StructLayout(0)]
	public class AnimationCurve : IEquatable<AnimationCurve>
	{
		// Token: 0x060001EB RID: 491
		[FreeFunction("AnimationCurveBindings::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x060001EC RID: 492
		[FreeFunction("AnimationCurveBindings::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Internal_Create(Keyframe[] keys);

		// Token: 0x060001ED RID: 493
		[FreeFunction("AnimationCurveBindings::Internal_Equals", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern bool Internal_Equals(IntPtr other);

		// Token: 0x060001EE RID: 494 RVA: 0x00004424 File Offset: 0x00002624
		~AnimationCurve()
		{
			AnimationCurve.Internal_Destroy(this.m_Ptr);
		}

		// Token: 0x060001EF RID: 495
		[ThreadSafe]
		[MethodImpl(4096)]
		public extern float Evaluate(float time);

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000445C File Offset: 0x0000265C
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00004474 File Offset: 0x00002674
		public Keyframe[] keys
		{
			get
			{
				return this.GetKeys();
			}
			set
			{
				this.SetKeys(value);
			}
		}

		// Token: 0x060001F2 RID: 498
		[FreeFunction("AnimationCurveBindings::AddKeySmoothTangents", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		public extern int AddKey(float time, float value);

		// Token: 0x060001F3 RID: 499 RVA: 0x00004480 File Offset: 0x00002680
		public int AddKey(Keyframe key)
		{
			return this.AddKey_Internal(key);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00004499 File Offset: 0x00002699
		[NativeMethod("AddKey", IsThreadSafe = true)]
		private int AddKey_Internal(Keyframe key)
		{
			return this.AddKey_Internal_Injected(ref key);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000044A3 File Offset: 0x000026A3
		[FreeFunction("AnimationCurveBindings::MoveKey", HasExplicitThis = true, IsThreadSafe = true)]
		[NativeThrows]
		public int MoveKey(int index, Keyframe key)
		{
			return this.MoveKey_Injected(index, ref key);
		}

		// Token: 0x060001F6 RID: 502
		[FreeFunction("AnimationCurveBindings::RemoveKey", HasExplicitThis = true, IsThreadSafe = true)]
		[NativeThrows]
		[MethodImpl(4096)]
		public extern void RemoveKey(int index);

		// Token: 0x1700003D RID: 61
		public Keyframe this[int index]
		{
			get
			{
				return this.GetKey(index);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001F8 RID: 504
		public extern int length
		{
			[NativeMethod("GetKeyCount", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060001F9 RID: 505
		[FreeFunction("AnimationCurveBindings::SetKeys", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern void SetKeys(Keyframe[] keys);

		// Token: 0x060001FA RID: 506 RVA: 0x000044CC File Offset: 0x000026CC
		[FreeFunction("AnimationCurveBindings::GetKey", HasExplicitThis = true, IsThreadSafe = true)]
		[NativeThrows]
		private Keyframe GetKey(int index)
		{
			Keyframe keyframe;
			this.GetKey_Injected(index, out keyframe);
			return keyframe;
		}

		// Token: 0x060001FB RID: 507
		[FreeFunction("AnimationCurveBindings::GetKeys", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern Keyframe[] GetKeys();

		// Token: 0x060001FC RID: 508
		[NativeThrows]
		[FreeFunction("AnimationCurveBindings::SmoothTangents", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		public extern void SmoothTangents(int index, float weight);

		// Token: 0x060001FD RID: 509 RVA: 0x000044E4 File Offset: 0x000026E4
		public static AnimationCurve Constant(float timeStart, float timeEnd, float value)
		{
			return AnimationCurve.Linear(timeStart, value, timeEnd, value);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00004500 File Offset: 0x00002700
		public static AnimationCurve Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			bool flag = timeStart == timeEnd;
			AnimationCurve animationCurve;
			if (flag)
			{
				Keyframe keyframe = new Keyframe(timeStart, valueStart);
				animationCurve = new AnimationCurve(new Keyframe[] { keyframe });
			}
			else
			{
				float num = (valueEnd - valueStart) / (timeEnd - timeStart);
				Keyframe[] array = new Keyframe[]
				{
					new Keyframe(timeStart, valueStart, 0f, num),
					new Keyframe(timeEnd, valueEnd, num, 0f)
				};
				animationCurve = new AnimationCurve(array);
			}
			return animationCurve;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000457C File Offset: 0x0000277C
		public static AnimationCurve EaseInOut(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			bool flag = timeStart == timeEnd;
			AnimationCurve animationCurve;
			if (flag)
			{
				Keyframe keyframe = new Keyframe(timeStart, valueStart);
				animationCurve = new AnimationCurve(new Keyframe[] { keyframe });
			}
			else
			{
				Keyframe[] array = new Keyframe[]
				{
					new Keyframe(timeStart, valueStart, 0f, 0f),
					new Keyframe(timeEnd, valueEnd, 0f, 0f)
				};
				animationCurve = new AnimationCurve(array);
			}
			return animationCurve;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000200 RID: 512
		// (set) Token: 0x06000201 RID: 513
		public extern WrapMode preWrapMode
		{
			[NativeMethod("GetPreInfinity", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetPreInfinity", IsThreadSafe = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000202 RID: 514
		// (set) Token: 0x06000203 RID: 515
		public extern WrapMode postWrapMode
		{
			[NativeMethod("GetPostInfinity", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetPostInfinity", IsThreadSafe = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000045F3 File Offset: 0x000027F3
		public AnimationCurve(params Keyframe[] keys)
		{
			this.m_Ptr = AnimationCurve.Internal_Create(keys);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00004609 File Offset: 0x00002809
		[RequiredByNativeCode]
		public AnimationCurve()
		{
			this.m_Ptr = AnimationCurve.Internal_Create(null);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00004620 File Offset: 0x00002820
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
					flag2 = !flag4 && this.Equals((AnimationCurve)o);
				}
			}
			return flag2;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00004674 File Offset: 0x00002874
		public bool Equals(AnimationCurve other)
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

		// Token: 0x06000208 RID: 520 RVA: 0x000046CC File Offset: 0x000028CC
		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		// Token: 0x06000209 RID: 521
		[MethodImpl(4096)]
		private extern int AddKey_Internal_Injected(ref Keyframe key);

		// Token: 0x0600020A RID: 522
		[MethodImpl(4096)]
		private extern int MoveKey_Injected(int index, ref Keyframe key);

		// Token: 0x0600020B RID: 523
		[MethodImpl(4096)]
		private extern void GetKey_Injected(int index, out Keyframe ret);

		// Token: 0x040001C1 RID: 449
		internal IntPtr m_Ptr;
	}
}
