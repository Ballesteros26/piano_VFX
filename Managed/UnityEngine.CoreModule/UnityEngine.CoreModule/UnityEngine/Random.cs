using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200018D RID: 397
	[NativeHeader("Runtime/Export/Random/Random.bindings.h")]
	public sealed class Random
	{
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060012AC RID: 4780
		// (set) Token: 0x060012AD RID: 4781
		[Obsolete("Deprecated. Use InitState() function or Random.state property instead.")]
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		public static extern int seed
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060012AE RID: 4782
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		[NativeMethod("SetSeed")]
		[MethodImpl(4096)]
		public static extern void InitState(int seed);

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0001EB38 File Offset: 0x0001CD38
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x0001EB4D File Offset: 0x0001CD4D
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		public static Random.State state
		{
			get
			{
				Random.State state;
				Random.get_state_Injected(out state);
				return state;
			}
			set
			{
				Random.set_state_Injected(ref value);
			}
		}

		// Token: 0x060012B1 RID: 4785
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern float Range(float min, float max);

		// Token: 0x060012B2 RID: 4786 RVA: 0x0001EB58 File Offset: 0x0001CD58
		public static int Range(int min, int max)
		{
			return Random.RandomRangeInt(min, max);
		}

		// Token: 0x060012B3 RID: 4787
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern int RandomRangeInt(int min, int max);

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060012B4 RID: 4788
		public static extern float value
		{
			[FreeFunction]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0001EB74 File Offset: 0x0001CD74
		public static Vector3 insideUnitSphere
		{
			[FreeFunction]
			get
			{
				Vector3 vector;
				Random.get_insideUnitSphere_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x060012B6 RID: 4790
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern void GetRandomUnitCircle(out Vector2 output);

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x0001EB8C File Offset: 0x0001CD8C
		public static Vector2 insideUnitCircle
		{
			get
			{
				Vector2 vector;
				Random.GetRandomUnitCircle(out vector);
				return vector;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x0001EBA8 File Offset: 0x0001CDA8
		public static Vector3 onUnitSphere
		{
			[FreeFunction]
			get
			{
				Vector3 vector;
				Random.get_onUnitSphere_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x0001EBC0 File Offset: 0x0001CDC0
		public static Quaternion rotation
		{
			[FreeFunction]
			get
			{
				Quaternion quaternion;
				Random.get_rotation_Injected(out quaternion);
				return quaternion;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x0001EBD8 File Offset: 0x0001CDD8
		public static Quaternion rotationUniform
		{
			[FreeFunction]
			get
			{
				Quaternion quaternion;
				Random.get_rotationUniform_Injected(out quaternion);
				return quaternion;
			}
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0001EBF0 File Offset: 0x0001CDF0
		[Obsolete("Use Random.Range instead")]
		public static float RandomRange(float min, float max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0001EC0C File Offset: 0x0001CE0C
		[Obsolete("Use Random.Range instead")]
		public static int RandomRange(int min, int max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0001EC28 File Offset: 0x0001CE28
		public static Color ColorHSV()
		{
			return Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f, 1f, 1f);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0001EC68 File Offset: 0x0001CE68
		public static Color ColorHSV(float hueMin, float hueMax)
		{
			return Random.ColorHSV(hueMin, hueMax, 0f, 1f, 0f, 1f, 1f, 1f);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0001ECA0 File Offset: 0x0001CEA0
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
		{
			return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, 0f, 1f, 1f, 1f);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0001ECD0 File Offset: 0x0001CED0
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
		{
			return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, 1f, 1f);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0001ECFC File Offset: 0x0001CEFC
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
		{
			float num = Mathf.Lerp(hueMin, hueMax, Random.value);
			float num2 = Mathf.Lerp(saturationMin, saturationMax, Random.value);
			float num3 = Mathf.Lerp(valueMin, valueMax, Random.value);
			Color color = Color.HSVToRGB(num, num2, num3, true);
			color.a = Mathf.Lerp(alphaMin, alphaMax, Random.value);
			return color;
		}

		// Token: 0x060012C3 RID: 4803
		[MethodImpl(4096)]
		private static extern void get_state_Injected(out Random.State ret);

		// Token: 0x060012C4 RID: 4804
		[MethodImpl(4096)]
		private static extern void set_state_Injected(ref Random.State value);

		// Token: 0x060012C5 RID: 4805
		[MethodImpl(4096)]
		private static extern void get_insideUnitSphere_Injected(out Vector3 ret);

		// Token: 0x060012C6 RID: 4806
		[MethodImpl(4096)]
		private static extern void get_onUnitSphere_Injected(out Vector3 ret);

		// Token: 0x060012C7 RID: 4807
		[MethodImpl(4096)]
		private static extern void get_rotation_Injected(out Quaternion ret);

		// Token: 0x060012C8 RID: 4808
		[MethodImpl(4096)]
		private static extern void get_rotationUniform_Injected(out Quaternion ret);

		// Token: 0x0200018E RID: 398
		[Serializable]
		public struct State
		{
			// Token: 0x04000630 RID: 1584
			[SerializeField]
			private int s0;

			// Token: 0x04000631 RID: 1585
			[SerializeField]
			private int s1;

			// Token: 0x04000632 RID: 1586
			[SerializeField]
			private int s2;

			// Token: 0x04000633 RID: 1587
			[SerializeField]
			private int s3;
		}
	}
}
