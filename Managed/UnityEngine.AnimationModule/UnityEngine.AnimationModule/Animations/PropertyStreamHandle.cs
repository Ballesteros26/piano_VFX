using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000057 RID: 87
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h")]
	public struct PropertyStreamHandle
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x0000671C File Offset: 0x0000491C
		public bool IsValid(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00006738 File Offset: 0x00004938
		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasHandleIndex && this.hasBindType;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000676C File Offset: 0x0000496C
		private bool createdByNative
		{
			get
			{
				return this.animatorBindingsVersion > 0U;
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00006788 File Offset: 0x00004988
		private bool IsSameVersionAsStream(ref AnimationStream stream)
		{
			return this.animatorBindingsVersion == stream.animatorBindingsVersion;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000067A8 File Offset: 0x000049A8
		private bool hasHandleIndex
		{
			get
			{
				return this.handleIndex != -1;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x000067C8 File Offset: 0x000049C8
		private bool hasValueArrayIndex
		{
			get
			{
				return this.valueArrayIndex != -1;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x000067E8 File Offset: 0x000049E8
		private bool hasBindType
		{
			get
			{
				return this.bindType != 0;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00006810 File Offset: 0x00004A10
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x00006803 File Offset: 0x00004A03
		internal uint animatorBindingsVersion
		{
			get
			{
				return this.m_AnimatorBindingsVersion;
			}
			private set
			{
				this.m_AnimatorBindingsVersion = value;
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00006828 File Offset: 0x00004A28
		public void Resolve(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00006834 File Offset: 0x00004A34
		public bool IsResolved(AnimationStream stream)
		{
			return this.IsResolvedInternal(ref stream);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00006850 File Offset: 0x00004A50
		private bool IsResolvedInternal(ref AnimationStream stream)
		{
			return this.IsValidInternal(ref stream) && this.IsSameVersionAsStream(ref stream) && this.hasValueArrayIndex;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00006880 File Offset: 0x00004A80
		private void CheckIsValidAndResolve(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = this.IsResolvedInternal(ref stream);
			if (!flag)
			{
				bool flag2 = !this.createdByNative || !this.hasHandleIndex || !this.hasBindType;
				if (flag2)
				{
					throw new InvalidOperationException("The PropertyStreamHandle is invalid. Please use proper function to create the handle.");
				}
				bool flag3 = !this.IsSameVersionAsStream(ref stream) || (this.hasHandleIndex && !this.hasValueArrayIndex);
				if (flag3)
				{
					this.ResolveInternal(ref stream);
				}
				bool flag4 = this.hasHandleIndex && !this.hasValueArrayIndex;
				if (flag4)
				{
					throw new InvalidOperationException("The PropertyStreamHandle cannot be resolved.");
				}
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00006920 File Offset: 0x00004B20
		public float GetFloat(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 5;
			if (flag)
			{
				throw new InvalidOperationException("GetValue type doesn't match PropertyStreamHandle bound type.");
			}
			return this.GetFloatInternal(ref stream);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00006960 File Offset: 0x00004B60
		public void SetFloat(AnimationStream stream, float value)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 5;
			if (flag)
			{
				throw new InvalidOperationException("SetValue type doesn't match PropertyStreamHandle bound type.");
			}
			this.SetFloatInternal(ref stream, value);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000699C File Offset: 0x00004B9C
		public int GetInt(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 10 && this.bindType != 11 && this.bindType != 9;
			if (flag)
			{
				throw new InvalidOperationException("GetValue type doesn't match PropertyStreamHandle bound type.");
			}
			return this.GetIntInternal(ref stream);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000069F4 File Offset: 0x00004BF4
		public void SetInt(AnimationStream stream, int value)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 10 && this.bindType != 11 && this.bindType != 9;
			if (flag)
			{
				throw new InvalidOperationException("SetValue type doesn't match PropertyStreamHandle bound type.");
			}
			this.SetIntInternal(ref stream, value);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00006A48 File Offset: 0x00004C48
		public bool GetBool(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 6 && this.bindType != 7;
			if (flag)
			{
				throw new InvalidOperationException("GetValue type doesn't match PropertyStreamHandle bound type.");
			}
			return this.GetBoolInternal(ref stream);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00006A94 File Offset: 0x00004C94
		public void SetBool(AnimationStream stream, bool value)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 6 && this.bindType != 7;
			if (flag)
			{
				throw new InvalidOperationException("SetValue type doesn't match PropertyStreamHandle bound type.");
			}
			this.SetBoolInternal(ref stream, value);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00006ADC File Offset: 0x00004CDC
		public bool GetReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetReadMaskInternal(ref stream);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00006AFF File Offset: 0x00004CFF
		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		private void ResolveInternal(ref AnimationStream stream)
		{
			PropertyStreamHandle.ResolveInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00006B08 File Offset: 0x00004D08
		[NativeMethod(Name = "GetFloat", IsThreadSafe = true)]
		private float GetFloatInternal(ref AnimationStream stream)
		{
			return PropertyStreamHandle.GetFloatInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00006B11 File Offset: 0x00004D11
		[NativeMethod(Name = "SetFloat", IsThreadSafe = true)]
		private void SetFloatInternal(ref AnimationStream stream, float value)
		{
			PropertyStreamHandle.SetFloatInternal_Injected(ref this, ref stream, value);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00006B1B File Offset: 0x00004D1B
		[NativeMethod(Name = "GetInt", IsThreadSafe = true)]
		private int GetIntInternal(ref AnimationStream stream)
		{
			return PropertyStreamHandle.GetIntInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00006B24 File Offset: 0x00004D24
		[NativeMethod(Name = "SetInt", IsThreadSafe = true)]
		private void SetIntInternal(ref AnimationStream stream, int value)
		{
			PropertyStreamHandle.SetIntInternal_Injected(ref this, ref stream, value);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00006B2E File Offset: 0x00004D2E
		[NativeMethod(Name = "GetBool", IsThreadSafe = true)]
		private bool GetBoolInternal(ref AnimationStream stream)
		{
			return PropertyStreamHandle.GetBoolInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00006B37 File Offset: 0x00004D37
		[NativeMethod(Name = "SetBool", IsThreadSafe = true)]
		private void SetBoolInternal(ref AnimationStream stream, bool value)
		{
			PropertyStreamHandle.SetBoolInternal_Injected(ref this, ref stream, value);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00006B41 File Offset: 0x00004D41
		[NativeMethod(Name = "GetReadMask", IsThreadSafe = true)]
		private bool GetReadMaskInternal(ref AnimationStream stream)
		{
			return PropertyStreamHandle.GetReadMaskInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000488 RID: 1160
		[MethodImpl(4096)]
		private static extern void ResolveInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x06000489 RID: 1161
		[MethodImpl(4096)]
		private static extern float GetFloatInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x0600048A RID: 1162
		[MethodImpl(4096)]
		private static extern void SetFloatInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream, float value);

		// Token: 0x0600048B RID: 1163
		[MethodImpl(4096)]
		private static extern int GetIntInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x0600048C RID: 1164
		[MethodImpl(4096)]
		private static extern void SetIntInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream, int value);

		// Token: 0x0600048D RID: 1165
		[MethodImpl(4096)]
		private static extern bool GetBoolInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x0600048E RID: 1166
		[MethodImpl(4096)]
		private static extern void SetBoolInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream, bool value);

		// Token: 0x0600048F RID: 1167
		[MethodImpl(4096)]
		private static extern bool GetReadMaskInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x0400016D RID: 365
		private uint m_AnimatorBindingsVersion;

		// Token: 0x0400016E RID: 366
		private int handleIndex;

		// Token: 0x0400016F RID: 367
		private int valueArrayIndex;

		// Token: 0x04000170 RID: 368
		private int bindType;
	}
}
