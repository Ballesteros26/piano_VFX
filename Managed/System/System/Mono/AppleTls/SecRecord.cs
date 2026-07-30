using System;
using Mono.Net;

namespace Mono.AppleTls
{
	// Token: 0x020000BB RID: 187
	internal class SecRecord : IDisposable
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x0000E460 File Offset: 0x0000C660
		static SecRecord()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/Security.framework/Security", 0);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				SecRecord.SecClassKey = CFObject.GetIntPtr(intPtr, "kSecClass");
			}
			finally
			{
				CFObject.dlclose(intPtr);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		internal CFMutableDictionary QueryDict
		{
			get
			{
				return this._queryDict;
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		internal void SetValue(IntPtr key, IntPtr value)
		{
			this._queryDict.SetValue(key, value);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		public SecRecord(SecKind secKind)
		{
			IntPtr intPtr = SecClass.FromSecKind(secKind);
			this._queryDict = CFMutableDictionary.Create();
			this._queryDict.SetValue(SecRecord.SecClassKey, intPtr);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000E502 File Offset: 0x0000C702
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000E511 File Offset: 0x0000C711
		protected virtual void Dispose(bool disposing)
		{
			if (this._queryDict != null && disposing)
			{
				this._queryDict.Dispose();
				this._queryDict = null;
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000E530 File Offset: 0x0000C730
		~SecRecord()
		{
			this.Dispose(false);
		}

		// Token: 0x04000ADB RID: 2779
		internal static readonly IntPtr SecClassKey;

		// Token: 0x04000ADC RID: 2780
		private CFMutableDictionary _queryDict;
	}
}
