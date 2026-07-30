using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides a custom loader to ASP.NET so that an application can provide its own implementation of the hosting environment.</summary>
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class CustomLoaderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.CustomLoaderAttribute" /> class.</summary>
		// Token: 0x060000AA RID: 170 RVA: 0x00002E4A File Offset: 0x0000104A
		public CustomLoaderAttribute(Type customLoaderType)
		{
		}

		/// <summary>Gets the type of the custom loader.</summary>
		/// <returns>The type of the custom loader.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002E4C File Offset: 0x0000104C
		public Type CustomLoaderType
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
