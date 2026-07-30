using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200000C RID: 12
	[NativeType(Header = "Modules/VFX/Public/VFXExpressionValues.h")]
	[RequiredByNativeCode]
	[StructLayout(0)]
	public class VFXExpressionValues
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000024AE File Offset: 0x000006AE
		private VFXExpressionValues()
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000024B8 File Offset: 0x000006B8
		[RequiredByNativeCode]
		internal static VFXExpressionValues CreateExpressionValuesWrapper(IntPtr ptr)
		{
			return new VFXExpressionValues
			{
				m_Ptr = ptr
			};
		}

		// Token: 0x06000048 RID: 72
		[NativeThrows]
		[NativeName("GetValueFromScript<bool>")]
		[MethodImpl(4096)]
		public extern bool GetBool(int nameID);

		// Token: 0x06000049 RID: 73
		[NativeName("GetValueFromScript<int>")]
		[NativeThrows]
		[MethodImpl(4096)]
		public extern int GetInt(int nameID);

		// Token: 0x0600004A RID: 74
		[NativeName("GetValueFromScript<UInt32>")]
		[NativeThrows]
		[MethodImpl(4096)]
		public extern uint GetUInt(int nameID);

		// Token: 0x0600004B RID: 75
		[NativeThrows]
		[NativeName("GetValueFromScript<float>")]
		[MethodImpl(4096)]
		public extern float GetFloat(int nameID);

		// Token: 0x0600004C RID: 76 RVA: 0x000024D8 File Offset: 0x000006D8
		[NativeThrows]
		[NativeName("GetValueFromScript<Vector2f>")]
		public Vector2 GetVector2(int nameID)
		{
			Vector2 vector;
			this.GetVector2_Injected(nameID, out vector);
			return vector;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000024F0 File Offset: 0x000006F0
		[NativeName("GetValueFromScript<Vector3f>")]
		[NativeThrows]
		public Vector3 GetVector3(int nameID)
		{
			Vector3 vector;
			this.GetVector3_Injected(nameID, out vector);
			return vector;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002508 File Offset: 0x00000708
		[NativeThrows]
		[NativeName("GetValueFromScript<Vector4f>")]
		public Vector4 GetVector4(int nameID)
		{
			Vector4 vector;
			this.GetVector4_Injected(nameID, out vector);
			return vector;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002520 File Offset: 0x00000720
		[NativeThrows]
		[NativeName("GetValueFromScript<Matrix4x4f>")]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			Matrix4x4 matrix4x;
			this.GetMatrix4x4_Injected(nameID, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000050 RID: 80
		[NativeThrows]
		[NativeName("GetValueFromScript<Texture*>")]
		[MethodImpl(4096)]
		public extern Texture GetTexture(int nameID);

		// Token: 0x06000051 RID: 81
		[NativeThrows]
		[NativeName("GetValueFromScript<Mesh*>")]
		[MethodImpl(4096)]
		public extern Mesh GetMesh(int nameID);

		// Token: 0x06000052 RID: 82 RVA: 0x00002538 File Offset: 0x00000738
		public AnimationCurve GetAnimationCurve(int nameID)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			this.Internal_GetAnimationCurveFromScript(nameID, animationCurve);
			return animationCurve;
		}

		// Token: 0x06000053 RID: 83
		[NativeThrows]
		[MethodImpl(4096)]
		internal extern void Internal_GetAnimationCurveFromScript(int nameID, AnimationCurve curve);

		// Token: 0x06000054 RID: 84 RVA: 0x0000255C File Offset: 0x0000075C
		public Gradient GetGradient(int nameID)
		{
			Gradient gradient = new Gradient();
			this.Internal_GetGradientFromScript(nameID, gradient);
			return gradient;
		}

		// Token: 0x06000055 RID: 85
		[NativeThrows]
		[MethodImpl(4096)]
		internal extern void Internal_GetGradientFromScript(int nameID, Gradient gradient);

		// Token: 0x06000056 RID: 86 RVA: 0x00002580 File Offset: 0x00000780
		public bool GetBool(string name)
		{
			return this.GetBool(Shader.PropertyToID(name));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000025A0 File Offset: 0x000007A0
		public int GetInt(string name)
		{
			return this.GetInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000025C0 File Offset: 0x000007C0
		public uint GetUInt(string name)
		{
			return this.GetUInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000025E0 File Offset: 0x000007E0
		public float GetFloat(string name)
		{
			return this.GetFloat(Shader.PropertyToID(name));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002600 File Offset: 0x00000800
		public Vector2 GetVector2(string name)
		{
			return this.GetVector2(Shader.PropertyToID(name));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002620 File Offset: 0x00000820
		public Vector3 GetVector3(string name)
		{
			return this.GetVector3(Shader.PropertyToID(name));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002640 File Offset: 0x00000840
		public Vector4 GetVector4(string name)
		{
			return this.GetVector4(Shader.PropertyToID(name));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002660 File Offset: 0x00000860
		public Matrix4x4 GetMatrix4x4(string name)
		{
			return this.GetMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002680 File Offset: 0x00000880
		public Texture GetTexture(string name)
		{
			return this.GetTexture(Shader.PropertyToID(name));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000026A0 File Offset: 0x000008A0
		public AnimationCurve GetAnimationCurve(string name)
		{
			return this.GetAnimationCurve(Shader.PropertyToID(name));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000026C0 File Offset: 0x000008C0
		public Gradient GetGradient(string name)
		{
			return this.GetGradient(Shader.PropertyToID(name));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000026E0 File Offset: 0x000008E0
		public Mesh GetMesh(string name)
		{
			return this.GetMesh(Shader.PropertyToID(name));
		}

		// Token: 0x06000062 RID: 98
		[MethodImpl(4096)]
		private extern void GetVector2_Injected(int nameID, out Vector2 ret);

		// Token: 0x06000063 RID: 99
		[MethodImpl(4096)]
		private extern void GetVector3_Injected(int nameID, out Vector3 ret);

		// Token: 0x06000064 RID: 100
		[MethodImpl(4096)]
		private extern void GetVector4_Injected(int nameID, out Vector4 ret);

		// Token: 0x06000065 RID: 101
		[MethodImpl(4096)]
		private extern void GetMatrix4x4_Injected(int nameID, out Matrix4x4 ret);

		// Token: 0x040000CE RID: 206
		internal IntPtr m_Ptr;
	}
}
