using System;
using System.Collections;

namespace System.Web.UI.Design
{
	/// <summary>Represents an object in a design host such as Visual Studio 2005. This class must be inherited.</summary>
	// Token: 0x02000074 RID: 116
	public abstract class DesignerObject : IServiceProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerObject" /> class.</summary>
		/// <param name="designer">The parent designer.</param>
		/// <param name="name">The name of the object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="designer" /> is null.-or-<paramref name="name" /> is null.</exception>
		// Token: 0x060003A8 RID: 936 RVA: 0x00002364 File Offset: 0x00000564
		[MonoNotSupported("")]
		protected DesignerObject(ControlDesigner designer, string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a service from the design host, as identified by the provided type.</summary>
		/// <returns>The requested service.</returns>
		/// <param name="serviceType">The type of service being requested.</param>
		// Token: 0x060003A9 RID: 937 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		protected object GetService(Type serviceType)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.IServiceProvider.GetService(System.Type)" />.</summary>
		/// <returns>The requested service.</returns>
		/// <param name="serviceType">The type of service being requested.</param>
		// Token: 0x060003AA RID: 938 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		object IServiceProvider.GetService(Type serviceType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the associated designer component.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.ControlDesigner" /> object.</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public ControlDesigner Designer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the object.</summary>
		/// <returns>The name of the object.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the object's properties.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> object containing the object's properties and their values.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public IDictionary Properties
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
