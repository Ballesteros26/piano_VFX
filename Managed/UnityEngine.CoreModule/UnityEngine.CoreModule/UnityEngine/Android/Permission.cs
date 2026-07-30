using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Android
{
	// Token: 0x020003EA RID: 1002
	[NativeHeader("Runtime/Export/Android/AndroidPermissions.bindings.h")]
	[UsedByNativeCode]
	public struct Permission
	{
		// Token: 0x060022C9 RID: 8905
		[StaticAccessor("PermissionsBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		public static extern bool HasUserAuthorizedPermission(string permission);

		// Token: 0x060022CA RID: 8906
		[StaticAccessor("PermissionsBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		public static extern void RequestUserPermission(string permission);

		// Token: 0x04000D0A RID: 3338
		public const string Camera = "android.permission.CAMERA";

		// Token: 0x04000D0B RID: 3339
		public const string Microphone = "android.permission.RECORD_AUDIO";

		// Token: 0x04000D0C RID: 3340
		public const string FineLocation = "android.permission.ACCESS_FINE_LOCATION";

		// Token: 0x04000D0D RID: 3341
		public const string CoarseLocation = "android.permission.ACCESS_COARSE_LOCATION";

		// Token: 0x04000D0E RID: 3342
		public const string ExternalStorageRead = "android.permission.READ_EXTERNAL_STORAGE";

		// Token: 0x04000D0F RID: 3343
		public const string ExternalStorageWrite = "android.permission.WRITE_EXTERNAL_STORAGE";
	}
}
