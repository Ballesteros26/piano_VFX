using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.VFX
{
	// Token: 0x02000014 RID: 20
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/VFX/Public/VisualEffect.h")]
	[NativeHeader("Modules/VFX/Public/ScriptBindings/VisualEffectBindings.h")]
	public class VisualEffect : Behaviour
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009A RID: 154
		// (set) Token: 0x0600009B RID: 155
		public extern bool pause
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009C RID: 156
		// (set) Token: 0x0600009D RID: 157
		public extern float playRate
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009E RID: 158
		// (set) Token: 0x0600009F RID: 159
		public extern uint startSeed
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A0 RID: 160
		// (set) Token: 0x060000A1 RID: 161
		public extern bool resetSeedOnPlay
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A2 RID: 162
		// (set) Token: 0x060000A3 RID: 163
		public extern int initialEventID
		{
			[FreeFunction(Name = "VisualEffectBindings::GetInitialEventID", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "VisualEffectBindings::SetInitialEventID", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A4 RID: 164
		// (set) Token: 0x060000A5 RID: 165
		public extern string initialEventName
		{
			[FreeFunction(Name = "VisualEffectBindings::GetInitialEventName", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "VisualEffectBindings::SetInitialEventName", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A6 RID: 166
		public extern bool culled
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A7 RID: 167
		// (set) Token: 0x060000A8 RID: 168
		public extern VisualEffectAsset visualEffectAsset
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000288C File Offset: 0x00000A8C
		public VFXEventAttribute CreateVFXEventAttribute()
		{
			bool flag = this.visualEffectAsset == null;
			VFXEventAttribute vfxeventAttribute;
			if (flag)
			{
				vfxeventAttribute = null;
			}
			else
			{
				VFXEventAttribute vfxeventAttribute2 = VFXEventAttribute.Internal_InstanciateVFXEventAttribute(this.visualEffectAsset);
				vfxeventAttribute = vfxeventAttribute2;
			}
			return vfxeventAttribute;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000028C0 File Offset: 0x00000AC0
		private void CheckValidVFXEventAttribute(VFXEventAttribute eventAttribute)
		{
			bool flag = eventAttribute != null && eventAttribute.vfxAsset != this.visualEffectAsset;
			if (flag)
			{
				throw new InvalidOperationException("Invalid VFXEventAttribute provided to VisualEffect, has been created with another VisualEffectAsset");
			}
		}

		// Token: 0x060000AB RID: 171
		[FreeFunction(Name = "VisualEffectBindings::SendEventFromScript", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SendEventFromScript(int eventNameID, VFXEventAttribute eventAttribute);

		// Token: 0x060000AC RID: 172 RVA: 0x000028F5 File Offset: 0x00000AF5
		public void SendEvent(int eventNameID, VFXEventAttribute eventAttribute)
		{
			this.CheckValidVFXEventAttribute(eventAttribute);
			this.SendEventFromScript(eventNameID, eventAttribute);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002909 File Offset: 0x00000B09
		public void SendEvent(string eventName, VFXEventAttribute eventAttribute)
		{
			this.SendEvent(Shader.PropertyToID(eventName), eventAttribute);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000291A File Offset: 0x00000B1A
		public void SendEvent(int eventNameID)
		{
			this.SendEventFromScript(eventNameID, null);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002926 File Offset: 0x00000B26
		public void SendEvent(string eventName)
		{
			this.SendEvent(Shader.PropertyToID(eventName), null);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002937 File Offset: 0x00000B37
		public void Play(VFXEventAttribute eventAttribute)
		{
			this.SendEvent(VisualEffectAsset.PlayEventID, eventAttribute);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002947 File Offset: 0x00000B47
		public void Play()
		{
			this.SendEvent(VisualEffectAsset.PlayEventID);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002956 File Offset: 0x00000B56
		public void Stop(VFXEventAttribute eventAttribute)
		{
			this.SendEvent(VisualEffectAsset.StopEventID, eventAttribute);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002966 File Offset: 0x00000B66
		public void Stop()
		{
			this.SendEvent(VisualEffectAsset.StopEventID);
		}

		// Token: 0x060000B4 RID: 180
		[MethodImpl(4096)]
		public extern void Reinit();

		// Token: 0x060000B5 RID: 181
		[MethodImpl(4096)]
		public extern void AdvanceOneFrame();

		// Token: 0x060000B6 RID: 182
		[FreeFunction(Name = "VisualEffectBindings::ResetOverrideFromScript", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void ResetOverride(int nameID);

		// Token: 0x060000B7 RID: 183
		[FreeFunction(Name = "VisualEffectBindings::GetTextureDimensionFromScript", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern TextureDimension GetTextureDimension(int nameID);

		// Token: 0x060000B8 RID: 184
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<bool>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasBool(int nameID);

		// Token: 0x060000B9 RID: 185
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<int>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasInt(int nameID);

		// Token: 0x060000BA RID: 186
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<UInt32>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasUInt(int nameID);

		// Token: 0x060000BB RID: 187
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<float>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasFloat(int nameID);

		// Token: 0x060000BC RID: 188
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector2f>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasVector2(int nameID);

		// Token: 0x060000BD RID: 189
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector3f>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasVector3(int nameID);

		// Token: 0x060000BE RID: 190
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector4f>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasVector4(int nameID);

		// Token: 0x060000BF RID: 191
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasMatrix4x4(int nameID);

		// Token: 0x060000C0 RID: 192
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Texture*>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasTexture(int nameID);

		// Token: 0x060000C1 RID: 193
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<AnimationCurve*>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasAnimationCurve(int nameID);

		// Token: 0x060000C2 RID: 194
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Gradient*>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasGradient(int nameID);

		// Token: 0x060000C3 RID: 195
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Mesh*>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasMesh(int nameID);

		// Token: 0x060000C4 RID: 196
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<bool>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetBool(int nameID, bool b);

		// Token: 0x060000C5 RID: 197
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<int>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetInt(int nameID, int i);

		// Token: 0x060000C6 RID: 198
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<UInt32>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetUInt(int nameID, uint i);

		// Token: 0x060000C7 RID: 199
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<float>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetFloat(int nameID, float f);

		// Token: 0x060000C8 RID: 200 RVA: 0x00002975 File Offset: 0x00000B75
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector2f>", HasExplicitThis = true)]
		public void SetVector2(int nameID, Vector2 v)
		{
			this.SetVector2_Injected(nameID, ref v);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00002980 File Offset: 0x00000B80
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector3f>", HasExplicitThis = true)]
		public void SetVector3(int nameID, Vector3 v)
		{
			this.SetVector3_Injected(nameID, ref v);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000298B File Offset: 0x00000B8B
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector4f>", HasExplicitThis = true)]
		public void SetVector4(int nameID, Vector4 v)
		{
			this.SetVector4_Injected(nameID, ref v);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002996 File Offset: 0x00000B96
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix4x4(int nameID, Matrix4x4 v)
		{
			this.SetMatrix4x4_Injected(nameID, ref v);
		}

		// Token: 0x060000CC RID: 204
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Texture*>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetTexture(int nameID, Texture t);

		// Token: 0x060000CD RID: 205
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<AnimationCurve*>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetAnimationCurve(int nameID, AnimationCurve c);

		// Token: 0x060000CE RID: 206
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Gradient*>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetGradient(int nameID, Gradient g);

		// Token: 0x060000CF RID: 207
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Mesh*>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetMesh(int nameID, Mesh m);

		// Token: 0x060000D0 RID: 208
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<bool>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool GetBool(int nameID);

		// Token: 0x060000D1 RID: 209
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<int>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern int GetInt(int nameID);

		// Token: 0x060000D2 RID: 210
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<UInt32>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern uint GetUInt(int nameID);

		// Token: 0x060000D3 RID: 211
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<float>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern float GetFloat(int nameID);

		// Token: 0x060000D4 RID: 212 RVA: 0x000029A4 File Offset: 0x00000BA4
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector2f>", HasExplicitThis = true)]
		public Vector2 GetVector2(int nameID)
		{
			Vector2 vector;
			this.GetVector2_Injected(nameID, out vector);
			return vector;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000029BC File Offset: 0x00000BBC
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector3f>", HasExplicitThis = true)]
		public Vector3 GetVector3(int nameID)
		{
			Vector3 vector;
			this.GetVector3_Injected(nameID, out vector);
			return vector;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000029D4 File Offset: 0x00000BD4
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector4f>", HasExplicitThis = true)]
		public Vector4 GetVector4(int nameID)
		{
			Vector4 vector;
			this.GetVector4_Injected(nameID, out vector);
			return vector;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000029EC File Offset: 0x00000BEC
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			Matrix4x4 matrix4x;
			this.GetMatrix4x4_Injected(nameID, out matrix4x);
			return matrix4x;
		}

		// Token: 0x060000D8 RID: 216
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Texture*>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern Texture GetTexture(int nameID);

		// Token: 0x060000D9 RID: 217
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Mesh*>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern Mesh GetMesh(int nameID);

		// Token: 0x060000DA RID: 218 RVA: 0x00002A04 File Offset: 0x00000C04
		public Gradient GetGradient(int nameID)
		{
			Gradient gradient = new Gradient();
			this.Internal_GetGradient(nameID, gradient);
			return gradient;
		}

		// Token: 0x060000DB RID: 219
		[FreeFunction(Name = "VisualEffectBindings::Internal_GetGradientFromScript", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_GetGradient(int nameID, Gradient gradient);

		// Token: 0x060000DC RID: 220 RVA: 0x00002A28 File Offset: 0x00000C28
		public AnimationCurve GetAnimationCurve(int nameID)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			this.Internal_GetAnimationCurve(nameID, animationCurve);
			return animationCurve;
		}

		// Token: 0x060000DD RID: 221
		[FreeFunction(Name = "VisualEffectBindings::Internal_GetAnimationCurveFromScript", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_GetAnimationCurve(int nameID, AnimationCurve curve);

		// Token: 0x060000DE RID: 222
		[FreeFunction(Name = "VisualEffectBindings::HasSystemFromScript", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasSystem(int nameID);

		// Token: 0x060000DF RID: 223 RVA: 0x00002A4C File Offset: 0x00000C4C
		[FreeFunction(Name = "VisualEffectBindings::GetParticleSystemInfo", HasExplicitThis = true, ThrowsException = true)]
		public VFXParticleSystemInfo GetParticleSystemInfo(int nameID)
		{
			VFXParticleSystemInfo vfxparticleSystemInfo;
			this.GetParticleSystemInfo_Injected(nameID, out vfxparticleSystemInfo);
			return vfxparticleSystemInfo;
		}

		// Token: 0x060000E0 RID: 224
		[FreeFunction(Name = "VisualEffectBindings::GetSystemNamesFromScript", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetSystemNames([NotNull] List<string> names);

		// Token: 0x060000E1 RID: 225
		[FreeFunction(Name = "VisualEffectBindings::GetParticleSystemNamesFromScript", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetParticleSystemNames([NotNull] List<string> names);

		// Token: 0x060000E2 RID: 226 RVA: 0x00002A63 File Offset: 0x00000C63
		public void ResetOverride(string name)
		{
			this.ResetOverride(Shader.PropertyToID(name));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002A74 File Offset: 0x00000C74
		public bool HasInt(string name)
		{
			return this.HasInt(Shader.PropertyToID(name));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002A94 File Offset: 0x00000C94
		public bool HasUInt(string name)
		{
			return this.HasUInt(Shader.PropertyToID(name));
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public bool HasFloat(string name)
		{
			return this.HasFloat(Shader.PropertyToID(name));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public bool HasVector2(string name)
		{
			return this.HasVector2(Shader.PropertyToID(name));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public bool HasVector3(string name)
		{
			return this.HasVector3(Shader.PropertyToID(name));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00002B14 File Offset: 0x00000D14
		public bool HasVector4(string name)
		{
			return this.HasVector4(Shader.PropertyToID(name));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00002B34 File Offset: 0x00000D34
		public bool HasMatrix4x4(string name)
		{
			return this.HasMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002B54 File Offset: 0x00000D54
		public bool HasTexture(string name)
		{
			return this.HasTexture(Shader.PropertyToID(name));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002B74 File Offset: 0x00000D74
		public TextureDimension GetTextureDimension(string name)
		{
			return this.GetTextureDimension(Shader.PropertyToID(name));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002B94 File Offset: 0x00000D94
		public bool HasAnimationCurve(string name)
		{
			return this.HasAnimationCurve(Shader.PropertyToID(name));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public bool HasGradient(string name)
		{
			return this.HasGradient(Shader.PropertyToID(name));
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public bool HasMesh(string name)
		{
			return this.HasMesh(Shader.PropertyToID(name));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public bool HasBool(string name)
		{
			return this.HasBool(Shader.PropertyToID(name));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00002C12 File Offset: 0x00000E12
		public void SetInt(string name, int i)
		{
			this.SetInt(Shader.PropertyToID(name), i);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002C23 File Offset: 0x00000E23
		public void SetUInt(string name, uint i)
		{
			this.SetUInt(Shader.PropertyToID(name), i);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00002C34 File Offset: 0x00000E34
		public void SetFloat(string name, float f)
		{
			this.SetFloat(Shader.PropertyToID(name), f);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00002C45 File Offset: 0x00000E45
		public void SetVector2(string name, Vector2 v)
		{
			this.SetVector2(Shader.PropertyToID(name), v);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00002C56 File Offset: 0x00000E56
		public void SetVector3(string name, Vector3 v)
		{
			this.SetVector3(Shader.PropertyToID(name), v);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00002C67 File Offset: 0x00000E67
		public void SetVector4(string name, Vector4 v)
		{
			this.SetVector4(Shader.PropertyToID(name), v);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00002C78 File Offset: 0x00000E78
		public void SetMatrix4x4(string name, Matrix4x4 v)
		{
			this.SetMatrix4x4(Shader.PropertyToID(name), v);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00002C89 File Offset: 0x00000E89
		public void SetTexture(string name, Texture t)
		{
			this.SetTexture(Shader.PropertyToID(name), t);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002C9A File Offset: 0x00000E9A
		public void SetAnimationCurve(string name, AnimationCurve c)
		{
			this.SetAnimationCurve(Shader.PropertyToID(name), c);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00002CAB File Offset: 0x00000EAB
		public void SetGradient(string name, Gradient g)
		{
			this.SetGradient(Shader.PropertyToID(name), g);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002CBC File Offset: 0x00000EBC
		public void SetMesh(string name, Mesh m)
		{
			this.SetMesh(Shader.PropertyToID(name), m);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002CCD File Offset: 0x00000ECD
		public void SetBool(string name, bool b)
		{
			this.SetBool(Shader.PropertyToID(name), b);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public int GetInt(string name)
		{
			return this.GetInt(Shader.PropertyToID(name));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002D00 File Offset: 0x00000F00
		public uint GetUInt(string name)
		{
			return this.GetUInt(Shader.PropertyToID(name));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002D20 File Offset: 0x00000F20
		public float GetFloat(string name)
		{
			return this.GetFloat(Shader.PropertyToID(name));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002D40 File Offset: 0x00000F40
		public Vector2 GetVector2(string name)
		{
			return this.GetVector2(Shader.PropertyToID(name));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002D60 File Offset: 0x00000F60
		public Vector3 GetVector3(string name)
		{
			return this.GetVector3(Shader.PropertyToID(name));
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00002D80 File Offset: 0x00000F80
		public Vector4 GetVector4(string name)
		{
			return this.GetVector4(Shader.PropertyToID(name));
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public Matrix4x4 GetMatrix4x4(string name)
		{
			return this.GetMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public Texture GetTexture(string name)
		{
			return this.GetTexture(Shader.PropertyToID(name));
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public Mesh GetMesh(string name)
		{
			return this.GetMesh(Shader.PropertyToID(name));
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00002E00 File Offset: 0x00001000
		public bool GetBool(string name)
		{
			return this.GetBool(Shader.PropertyToID(name));
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002E20 File Offset: 0x00001020
		public AnimationCurve GetAnimationCurve(string name)
		{
			return this.GetAnimationCurve(Shader.PropertyToID(name));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002E40 File Offset: 0x00001040
		public Gradient GetGradient(string name)
		{
			return this.GetGradient(Shader.PropertyToID(name));
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00002E60 File Offset: 0x00001060
		public bool HasSystem(string name)
		{
			return this.HasSystem(Shader.PropertyToID(name));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00002E80 File Offset: 0x00001080
		public VFXParticleSystemInfo GetParticleSystemInfo(string name)
		{
			return this.GetParticleSystemInfo(Shader.PropertyToID(name));
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600010A RID: 266
		public extern int aliveParticleCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600010B RID: 267
		[MethodImpl(4096)]
		public extern void Simulate(float stepDeltaTime, uint stepCount = 1U);

		// Token: 0x0600010D RID: 269
		[MethodImpl(4096)]
		private extern void SetVector2_Injected(int nameID, ref Vector2 v);

		// Token: 0x0600010E RID: 270
		[MethodImpl(4096)]
		private extern void SetVector3_Injected(int nameID, ref Vector3 v);

		// Token: 0x0600010F RID: 271
		[MethodImpl(4096)]
		private extern void SetVector4_Injected(int nameID, ref Vector4 v);

		// Token: 0x06000110 RID: 272
		[MethodImpl(4096)]
		private extern void SetMatrix4x4_Injected(int nameID, ref Matrix4x4 v);

		// Token: 0x06000111 RID: 273
		[MethodImpl(4096)]
		private extern void GetVector2_Injected(int nameID, out Vector2 ret);

		// Token: 0x06000112 RID: 274
		[MethodImpl(4096)]
		private extern void GetVector3_Injected(int nameID, out Vector3 ret);

		// Token: 0x06000113 RID: 275
		[MethodImpl(4096)]
		private extern void GetVector4_Injected(int nameID, out Vector4 ret);

		// Token: 0x06000114 RID: 276
		[MethodImpl(4096)]
		private extern void GetMatrix4x4_Injected(int nameID, out Matrix4x4 ret);

		// Token: 0x06000115 RID: 277
		[MethodImpl(4096)]
		private extern void GetParticleSystemInfo_Injected(int nameID, out VFXParticleSystemInfo ret);
	}
}
