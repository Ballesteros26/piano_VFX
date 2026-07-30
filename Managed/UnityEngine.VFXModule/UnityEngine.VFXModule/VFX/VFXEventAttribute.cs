using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200000B RID: 11
	[NativeType(Header = "Modules/VFX/Public/VFXEventAttribute.h")]
	[RequiredByNativeCode]
	[StructLayout(0)]
	public sealed class VFXEventAttribute : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private VFXEventAttribute(IntPtr ptr, bool owner, VisualEffectAsset vfxAsset)
		{
			this.m_Ptr = ptr;
			this.m_Owner = owner;
			this.m_VfxAsset = vfxAsset;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206F File Offset: 0x0000026F
		private VFXEventAttribute()
			: this(IntPtr.Zero, false, null)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002080 File Offset: 0x00000280
		public VFXEventAttribute(VFXEventAttribute original)
		{
			bool flag = original == null;
			if (flag)
			{
				throw new ArgumentNullException("VFXEventAttribute expect a non null attribute");
			}
			this.m_Ptr = VFXEventAttribute.Internal_Create();
			this.m_VfxAsset = original.m_VfxAsset;
			this.Internal_InitFromEventAttribute(original);
		}

		// Token: 0x06000004 RID: 4
		[MethodImpl(4096)]
		internal static extern IntPtr Internal_Create();

		// Token: 0x06000005 RID: 5 RVA: 0x000020C8 File Offset: 0x000002C8
		internal static VFXEventAttribute Internal_InstanciateVFXEventAttribute(VisualEffectAsset vfxAsset)
		{
			VFXEventAttribute vfxeventAttribute = new VFXEventAttribute(VFXEventAttribute.Internal_Create(), true, vfxAsset);
			vfxeventAttribute.Internal_InitFromAsset(vfxAsset);
			return vfxeventAttribute;
		}

		// Token: 0x06000006 RID: 6
		[MethodImpl(4096)]
		internal extern void Internal_InitFromAsset(VisualEffectAsset vfxAsset);

		// Token: 0x06000007 RID: 7
		[MethodImpl(4096)]
		internal extern void Internal_InitFromEventAttribute(VFXEventAttribute vfxEventAttribute);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020F0 File Offset: 0x000002F0
		internal VisualEffectAsset vfxAsset
		{
			get
			{
				return this.m_VfxAsset;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002108 File Offset: 0x00000308
		private void Release()
		{
			bool flag = this.m_Owner && this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				VFXEventAttribute.Internal_Destroy(this.m_Ptr);
			}
			this.m_Ptr = IntPtr.Zero;
			this.m_VfxAsset = null;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002158 File Offset: 0x00000358
		~VFXEventAttribute()
		{
			this.Release();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002188 File Offset: 0x00000388
		public void Dispose()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600000C RID: 12
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		internal static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x0600000D RID: 13
		[NativeName("HasValueFromScript<bool>")]
		[MethodImpl(4096)]
		public extern bool HasBool(int nameID);

		// Token: 0x0600000E RID: 14
		[NativeName("HasValueFromScript<int>")]
		[MethodImpl(4096)]
		public extern bool HasInt(int nameID);

		// Token: 0x0600000F RID: 15
		[NativeName("HasValueFromScript<UInt32>")]
		[MethodImpl(4096)]
		public extern bool HasUint(int nameID);

		// Token: 0x06000010 RID: 16
		[NativeName("HasValueFromScript<float>")]
		[MethodImpl(4096)]
		public extern bool HasFloat(int nameID);

		// Token: 0x06000011 RID: 17
		[NativeName("HasValueFromScript<Vector2f>")]
		[MethodImpl(4096)]
		public extern bool HasVector2(int nameID);

		// Token: 0x06000012 RID: 18
		[NativeName("HasValueFromScript<Vector3f>")]
		[MethodImpl(4096)]
		public extern bool HasVector3(int nameID);

		// Token: 0x06000013 RID: 19
		[NativeName("HasValueFromScript<Vector4f>")]
		[MethodImpl(4096)]
		public extern bool HasVector4(int nameID);

		// Token: 0x06000014 RID: 20
		[NativeName("HasValueFromScript<Matrix4x4f>")]
		[MethodImpl(4096)]
		public extern bool HasMatrix4x4(int nameID);

		// Token: 0x06000015 RID: 21
		[NativeName("SetValueFromScript<bool>")]
		[MethodImpl(4096)]
		public extern void SetBool(int nameID, bool b);

		// Token: 0x06000016 RID: 22
		[NativeName("SetValueFromScript<int>")]
		[MethodImpl(4096)]
		public extern void SetInt(int nameID, int i);

		// Token: 0x06000017 RID: 23
		[NativeName("SetValueFromScript<UInt32>")]
		[MethodImpl(4096)]
		public extern void SetUint(int nameID, uint i);

		// Token: 0x06000018 RID: 24
		[NativeName("SetValueFromScript<float>")]
		[MethodImpl(4096)]
		public extern void SetFloat(int nameID, float f);

		// Token: 0x06000019 RID: 25 RVA: 0x00002199 File Offset: 0x00000399
		[NativeName("SetValueFromScript<Vector2f>")]
		public void SetVector2(int nameID, Vector2 v)
		{
			this.SetVector2_Injected(nameID, ref v);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021A4 File Offset: 0x000003A4
		[NativeName("SetValueFromScript<Vector3f>")]
		public void SetVector3(int nameID, Vector3 v)
		{
			this.SetVector3_Injected(nameID, ref v);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021AF File Offset: 0x000003AF
		[NativeName("SetValueFromScript<Vector4f>")]
		public void SetVector4(int nameID, Vector4 v)
		{
			this.SetVector4_Injected(nameID, ref v);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000021BA File Offset: 0x000003BA
		[NativeName("SetValueFromScript<Matrix4x4f>")]
		public void SetMatrix4x4(int nameID, Matrix4x4 v)
		{
			this.SetMatrix4x4_Injected(nameID, ref v);
		}

		// Token: 0x0600001D RID: 29
		[NativeName("GetValueFromScript<bool>")]
		[MethodImpl(4096)]
		public extern bool GetBool(int nameID);

		// Token: 0x0600001E RID: 30
		[NativeName("GetValueFromScript<int>")]
		[MethodImpl(4096)]
		public extern int GetInt(int nameID);

		// Token: 0x0600001F RID: 31
		[NativeName("GetValueFromScript<UInt32>")]
		[MethodImpl(4096)]
		public extern uint GetUint(int nameID);

		// Token: 0x06000020 RID: 32
		[NativeName("GetValueFromScript<float>")]
		[MethodImpl(4096)]
		public extern float GetFloat(int nameID);

		// Token: 0x06000021 RID: 33 RVA: 0x000021C8 File Offset: 0x000003C8
		[NativeName("GetValueFromScript<Vector2f>")]
		public Vector2 GetVector2(int nameID)
		{
			Vector2 vector;
			this.GetVector2_Injected(nameID, out vector);
			return vector;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000021E0 File Offset: 0x000003E0
		[NativeName("GetValueFromScript<Vector3f>")]
		public Vector3 GetVector3(int nameID)
		{
			Vector3 vector;
			this.GetVector3_Injected(nameID, out vector);
			return vector;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000021F8 File Offset: 0x000003F8
		[NativeName("GetValueFromScript<Vector4f>")]
		public Vector4 GetVector4(int nameID)
		{
			Vector4 vector;
			this.GetVector4_Injected(nameID, out vector);
			return vector;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002210 File Offset: 0x00000410
		[NativeName("GetValueFromScript<Matrix4x4f>")]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			Matrix4x4 matrix4x;
			this.GetMatrix4x4_Injected(nameID, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002228 File Offset: 0x00000428
		public bool HasBool(string name)
		{
			return this.HasBool(Shader.PropertyToID(name));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002248 File Offset: 0x00000448
		public bool HasInt(string name)
		{
			return this.HasInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002268 File Offset: 0x00000468
		public bool HasUint(string name)
		{
			return this.HasUint(Shader.PropertyToID(name));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002288 File Offset: 0x00000488
		public bool HasFloat(string name)
		{
			return this.HasFloat(Shader.PropertyToID(name));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000022A8 File Offset: 0x000004A8
		public bool HasVector2(string name)
		{
			return this.HasVector2(Shader.PropertyToID(name));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000022C8 File Offset: 0x000004C8
		public bool HasVector3(string name)
		{
			return this.HasVector3(Shader.PropertyToID(name));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000022E8 File Offset: 0x000004E8
		public bool HasVector4(string name)
		{
			return this.HasVector4(Shader.PropertyToID(name));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002308 File Offset: 0x00000508
		public bool HasMatrix4x4(string name)
		{
			return this.HasMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002326 File Offset: 0x00000526
		public void SetBool(string name, bool b)
		{
			this.SetBool(Shader.PropertyToID(name), b);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002337 File Offset: 0x00000537
		public void SetInt(string name, int i)
		{
			this.SetInt(Shader.PropertyToID(name), i);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002348 File Offset: 0x00000548
		public void SetUint(string name, uint i)
		{
			this.SetUint(Shader.PropertyToID(name), i);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002359 File Offset: 0x00000559
		public void SetFloat(string name, float f)
		{
			this.SetFloat(Shader.PropertyToID(name), f);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000236A File Offset: 0x0000056A
		public void SetVector2(string name, Vector2 v)
		{
			this.SetVector2(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000237B File Offset: 0x0000057B
		public void SetVector3(string name, Vector3 v)
		{
			this.SetVector3(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000238C File Offset: 0x0000058C
		public void SetVector4(string name, Vector4 v)
		{
			this.SetVector4(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000239D File Offset: 0x0000059D
		public void SetMatrix4x4(string name, Matrix4x4 v)
		{
			this.SetMatrix4x4(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000023B0 File Offset: 0x000005B0
		public bool GetBool(string name)
		{
			return this.GetBool(Shader.PropertyToID(name));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000023D0 File Offset: 0x000005D0
		public int GetInt(string name)
		{
			return this.GetInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000023F0 File Offset: 0x000005F0
		public uint GetUint(string name)
		{
			return this.GetUint(Shader.PropertyToID(name));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002410 File Offset: 0x00000610
		public float GetFloat(string name)
		{
			return this.GetFloat(Shader.PropertyToID(name));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002430 File Offset: 0x00000630
		public Vector2 GetVector2(string name)
		{
			return this.GetVector2(Shader.PropertyToID(name));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002450 File Offset: 0x00000650
		public Vector3 GetVector3(string name)
		{
			return this.GetVector3(Shader.PropertyToID(name));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002470 File Offset: 0x00000670
		public Vector4 GetVector4(string name)
		{
			return this.GetVector4(Shader.PropertyToID(name));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002490 File Offset: 0x00000690
		public Matrix4x4 GetMatrix4x4(string name)
		{
			return this.GetMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x0600003D RID: 61
		[MethodImpl(4096)]
		public extern void CopyValuesFrom([NotNull] VFXEventAttribute eventAttibute);

		// Token: 0x0600003E RID: 62
		[MethodImpl(4096)]
		private extern void SetVector2_Injected(int nameID, ref Vector2 v);

		// Token: 0x0600003F RID: 63
		[MethodImpl(4096)]
		private extern void SetVector3_Injected(int nameID, ref Vector3 v);

		// Token: 0x06000040 RID: 64
		[MethodImpl(4096)]
		private extern void SetVector4_Injected(int nameID, ref Vector4 v);

		// Token: 0x06000041 RID: 65
		[MethodImpl(4096)]
		private extern void SetMatrix4x4_Injected(int nameID, ref Matrix4x4 v);

		// Token: 0x06000042 RID: 66
		[MethodImpl(4096)]
		private extern void GetVector2_Injected(int nameID, out Vector2 ret);

		// Token: 0x06000043 RID: 67
		[MethodImpl(4096)]
		private extern void GetVector3_Injected(int nameID, out Vector3 ret);

		// Token: 0x06000044 RID: 68
		[MethodImpl(4096)]
		private extern void GetVector4_Injected(int nameID, out Vector4 ret);

		// Token: 0x06000045 RID: 69
		[MethodImpl(4096)]
		private extern void GetMatrix4x4_Injected(int nameID, out Matrix4x4 ret);

		// Token: 0x040000CB RID: 203
		private IntPtr m_Ptr;

		// Token: 0x040000CC RID: 204
		private bool m_Owner;

		// Token: 0x040000CD RID: 205
		private VisualEffectAsset m_VfxAsset;
	}
}
