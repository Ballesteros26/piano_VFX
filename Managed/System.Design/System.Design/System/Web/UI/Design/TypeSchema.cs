using System;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Represents the structure, or schema, of an object type.</summary>
	// Token: 0x020000AD RID: 173
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public sealed class TypeSchema : IDataSourceSchema
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.TypeSchema" /> class using the provided <see cref="T:System.Type" /> object.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that describes an object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x0600052F RID: 1327 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public TypeSchema(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an array of schema descriptors for views into a data source.</summary>
		/// <returns>An array of instances of <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" />, or of instances of a class that implements the <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> interface.</returns>
		// Token: 0x06000530 RID: 1328 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public IDataSourceViewSchema[] GetViews()
		{
			throw new NotImplementedException();
		}
	}
}
