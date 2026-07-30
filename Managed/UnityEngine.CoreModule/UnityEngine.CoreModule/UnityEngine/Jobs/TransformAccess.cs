using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Jobs
{
	// Token: 0x0200021E RID: 542
	[NativeHeader("Runtime/Transform/ScriptBindings/TransformAccess.bindings.h")]
	public struct TransformAccess
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00026D30 File Offset: 0x00024F30
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x00026D4C File Offset: 0x00024F4C
		public Vector3 position
		{
			get
			{
				Vector3 vector;
				TransformAccess.GetPosition(ref this, out vector);
				return vector;
			}
			set
			{
				TransformAccess.SetPosition(ref this, ref value);
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x00026D58 File Offset: 0x00024F58
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x00026D74 File Offset: 0x00024F74
		public Quaternion rotation
		{
			get
			{
				Quaternion quaternion;
				TransformAccess.GetRotation(ref this, out quaternion);
				return quaternion;
			}
			set
			{
				TransformAccess.SetRotation(ref this, ref value);
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00026D80 File Offset: 0x00024F80
		// (set) Token: 0x06001801 RID: 6145 RVA: 0x00026D9C File Offset: 0x00024F9C
		public Vector3 localPosition
		{
			get
			{
				Vector3 vector;
				TransformAccess.GetLocalPosition(ref this, out vector);
				return vector;
			}
			set
			{
				TransformAccess.SetLocalPosition(ref this, ref value);
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00026DA8 File Offset: 0x00024FA8
		// (set) Token: 0x06001803 RID: 6147 RVA: 0x00026DC4 File Offset: 0x00024FC4
		public Quaternion localRotation
		{
			get
			{
				Quaternion quaternion;
				TransformAccess.GetLocalRotation(ref this, out quaternion);
				return quaternion;
			}
			set
			{
				TransformAccess.SetLocalRotation(ref this, ref value);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00026DD0 File Offset: 0x00024FD0
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x00026DEC File Offset: 0x00024FEC
		public Vector3 localScale
		{
			get
			{
				Vector3 vector;
				TransformAccess.GetLocalScale(ref this, out vector);
				return vector;
			}
			set
			{
				TransformAccess.SetLocalScale(ref this, ref value);
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00026DF8 File Offset: 0x00024FF8
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				TransformAccess.GetLocalToWorldMatrix(ref this, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x00026E14 File Offset: 0x00025014
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				TransformAccess.GetWorldToLocalMatrix(ref this, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x06001808 RID: 6152
		[NativeMethod(Name = "TransformAccessBindings::GetPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void GetPosition(ref TransformAccess access, out Vector3 p);

		// Token: 0x06001809 RID: 6153
		[NativeMethod(Name = "TransformAccessBindings::SetPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void SetPosition(ref TransformAccess access, ref Vector3 p);

		// Token: 0x0600180A RID: 6154
		[NativeMethod(Name = "TransformAccessBindings::GetRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void GetRotation(ref TransformAccess access, out Quaternion r);

		// Token: 0x0600180B RID: 6155
		[NativeMethod(Name = "TransformAccessBindings::SetRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void SetRotation(ref TransformAccess access, ref Quaternion r);

		// Token: 0x0600180C RID: 6156
		[NativeMethod(Name = "TransformAccessBindings::GetLocalPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void GetLocalPosition(ref TransformAccess access, out Vector3 p);

		// Token: 0x0600180D RID: 6157
		[NativeMethod(Name = "TransformAccessBindings::SetLocalPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void SetLocalPosition(ref TransformAccess access, ref Vector3 p);

		// Token: 0x0600180E RID: 6158
		[NativeMethod(Name = "TransformAccessBindings::GetLocalRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void GetLocalRotation(ref TransformAccess access, out Quaternion r);

		// Token: 0x0600180F RID: 6159
		[NativeMethod(Name = "TransformAccessBindings::SetLocalRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void SetLocalRotation(ref TransformAccess access, ref Quaternion r);

		// Token: 0x06001810 RID: 6160
		[NativeMethod(Name = "TransformAccessBindings::GetLocalScale", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void GetLocalScale(ref TransformAccess access, out Vector3 r);

		// Token: 0x06001811 RID: 6161
		[NativeMethod(Name = "TransformAccessBindings::SetLocalScale", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void SetLocalScale(ref TransformAccess access, ref Vector3 r);

		// Token: 0x06001812 RID: 6162
		[NativeMethod(Name = "TransformAccessBindings::GetLocalToWorldMatrix", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void GetLocalToWorldMatrix(ref TransformAccess access, out Matrix4x4 m);

		// Token: 0x06001813 RID: 6163
		[NativeMethod(Name = "TransformAccessBindings::GetWorldToLocalMatrix", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void GetWorldToLocalMatrix(ref TransformAccess access, out Matrix4x4 m);

		// Token: 0x0400075F RID: 1887
		private IntPtr hierarchy;

		// Token: 0x04000760 RID: 1888
		private int index;
	}
}
