using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x0200001E RID: 30
	internal class FileRefHandler : ResXDataNodeHandler
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003AB3 File Offset: 0x00001CB3
		public FileRefHandler(ResXFileRef fileRef)
		{
			this.resXFileRef = fileRef;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003AC2 File Offset: 0x00001CC2
		public override object GetValue(ITypeResolutionService typeResolver)
		{
			return this.GetValue();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003AC2 File Offset: 0x00001CC2
		public override object GetValue(AssemblyName[] assemblyNames)
		{
			return this.GetValue();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003ACC File Offset: 0x00001CCC
		public override string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			Type type = base.ResolveType(this.resXFileRef.TypeName, typeResolver);
			if (type == null)
			{
				return this.resXFileRef.TypeName;
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003B08 File Offset: 0x00001D08
		public override string GetValueTypeName(AssemblyName[] assemblyNames)
		{
			Type type = base.ResolveType(this.resXFileRef.TypeName, assemblyNames);
			if (type == null)
			{
				return this.resXFileRef.TypeName;
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003B44 File Offset: 0x00001D44
		private object GetValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(ResXFileRef));
			object obj;
			try
			{
				obj = converter.ConvertFromInvariantString(this.resXFileRef.ToString());
			}
			catch (ArgumentNullException ex)
			{
				if (ex.ParamName == "type")
				{
					throw new TypeLoadException("Could not find type", ex);
				}
				throw ex;
			}
			return obj;
		}

		// Token: 0x04000D66 RID: 3430
		private ResXFileRef resXFileRef;
	}
}
