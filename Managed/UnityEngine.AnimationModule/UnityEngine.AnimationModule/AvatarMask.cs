using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/AvatarMask.h")]
	[MovedFrom(true, "UnityEditor.Animations", "UnityEditor", null)]
	public sealed class AvatarMask : Object
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00003D80 File Offset: 0x00001F80
		public AvatarMask()
		{
			AvatarMask.Internal_Create(this);
		}

		// Token: 0x0600023E RID: 574
		[FreeFunction("AnimationBindings::CreateAvatarMask")]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] AvatarMask self);

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00003D94 File Offset: 0x00001F94
		[Obsolete("AvatarMask.humanoidBodyPartCount is deprecated, use AvatarMaskBodyPart.LastBodyPart instead.")]
		public int humanoidBodyPartCount
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x06000240 RID: 576
		[NativeMethod("GetBodyPart")]
		[MethodImpl(4096)]
		public extern bool GetHumanoidBodyPartActive(AvatarMaskBodyPart index);

		// Token: 0x06000241 RID: 577
		[NativeMethod("SetBodyPart")]
		[MethodImpl(4096)]
		public extern void SetHumanoidBodyPartActive(AvatarMaskBodyPart index, bool value);

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000242 RID: 578
		// (set) Token: 0x06000243 RID: 579
		public extern int transformCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public void AddTransformPath(Transform transform)
		{
			this.AddTransformPath(transform, true);
		}

		// Token: 0x06000245 RID: 581
		[MethodImpl(4096)]
		public extern void AddTransformPath([NotNull] Transform transform, [DefaultValue("true")] bool recursive);

		// Token: 0x06000246 RID: 582 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public void RemoveTransformPath(Transform transform)
		{
			this.RemoveTransformPath(transform, true);
		}

		// Token: 0x06000247 RID: 583
		[MethodImpl(4096)]
		public extern void RemoveTransformPath([NotNull] Transform transform, [DefaultValue("true")] bool recursive);

		// Token: 0x06000248 RID: 584
		[MethodImpl(4096)]
		public extern string GetTransformPath(int index);

		// Token: 0x06000249 RID: 585
		[MethodImpl(4096)]
		public extern void SetTransformPath(int index, string path);

		// Token: 0x0600024A RID: 586
		[MethodImpl(4096)]
		private extern float GetTransformWeight(int index);

		// Token: 0x0600024B RID: 587
		[MethodImpl(4096)]
		private extern void SetTransformWeight(int index, float weight);

		// Token: 0x0600024C RID: 588 RVA: 0x00003DC0 File Offset: 0x00001FC0
		public bool GetTransformActive(int index)
		{
			return this.GetTransformWeight(index) > 0.5f;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public void SetTransformActive(int index, bool value)
		{
			this.SetTransformWeight(index, value ? 1f : 0f);
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600024E RID: 590
		internal extern bool hasFeetIK
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00003DFC File Offset: 0x00001FFC
		internal void Copy(AvatarMask other)
		{
			for (AvatarMaskBodyPart avatarMaskBodyPart = AvatarMaskBodyPart.Root; avatarMaskBodyPart < AvatarMaskBodyPart.LastBodyPart; avatarMaskBodyPart++)
			{
				this.SetHumanoidBodyPartActive(avatarMaskBodyPart, other.GetHumanoidBodyPartActive(avatarMaskBodyPart));
			}
			this.transformCount = other.transformCount;
			for (int i = 0; i < other.transformCount; i++)
			{
				this.SetTransformPath(i, other.GetTransformPath(i));
				this.SetTransformActive(i, other.GetTransformActive(i));
			}
		}
	}
}
