using System;

namespace System.Runtime.Remoting
{
	// Token: 0x02000750 RID: 1872
	internal class ClientIdentity : Identity
	{
		// Token: 0x06004D50 RID: 19792 RVA: 0x00116D97 File Offset: 0x00114F97
		public ClientIdentity(string objectUri, ObjRef objRef)
			: base(objectUri)
		{
			this._objRef = objRef;
			this._envoySink = ((this._objRef.EnvoyInfo != null) ? this._objRef.EnvoyInfo.EnvoySinks : null);
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06004D51 RID: 19793 RVA: 0x00116DCD File Offset: 0x00114FCD
		// (set) Token: 0x06004D52 RID: 19794 RVA: 0x00116DDF File Offset: 0x00114FDF
		public MarshalByRefObject ClientProxy
		{
			get
			{
				return (MarshalByRefObject)this._proxyReference.Target;
			}
			set
			{
				this._proxyReference = new WeakReference(value);
			}
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x00116DED File Offset: 0x00114FED
		public override ObjRef CreateObjRef(Type requestedType)
		{
			return this._objRef;
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06004D54 RID: 19796 RVA: 0x00116DF5 File Offset: 0x00114FF5
		public string TargetUri
		{
			get
			{
				return this._objRef.URI;
			}
		}

		// Token: 0x04002998 RID: 10648
		private WeakReference _proxyReference;
	}
}
