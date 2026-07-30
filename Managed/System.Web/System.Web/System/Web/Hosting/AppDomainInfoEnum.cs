using System;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides access to an application domain.</summary>
	// Token: 0x0200075F RID: 1887
	public class AppDomainInfoEnum : IAppDomainInfoEnum
	{
		// Token: 0x06004D12 RID: 19730 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal AppDomainInfoEnum()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the number of application domains.</summary>
		/// <returns>The number of application domains.</returns>
		// Token: 0x06004D13 RID: 19731 RVA: 0x000CB20C File Offset: 0x000C940C
		public int Count()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Gets an <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</summary>
		/// <returns>An <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</returns>
		// Token: 0x06004D14 RID: 19732 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IAppDomainInfo GetData()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Moves to the next <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</summary>
		/// <returns>Returns true if a new interface is available; otherwise returns false.</returns>
		// Token: 0x06004D15 RID: 19733 RVA: 0x000CB228 File Offset: 0x000C9428
		public bool MoveNext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Initializes the <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</summary>
		// Token: 0x06004D16 RID: 19734 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Reset()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
