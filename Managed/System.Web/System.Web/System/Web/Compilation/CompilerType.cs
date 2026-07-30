using System;
using System.CodeDom.Compiler;
using Unity;

namespace System.Web.Compilation
{
	/// <summary>Represents the compiler settings used within the ASP.NET build environment to generate and compile source code from a virtual path. This class cannot be inherited.</summary>
	// Token: 0x0200064D RID: 1613
	public sealed class CompilerType
	{
		// Token: 0x06004560 RID: 17760 RVA: 0x000BDF5F File Offset: 0x000BC15F
		internal CompilerType(Type type, CompilerParameters parameters)
		{
			this.type = type;
			this.parameters = parameters;
		}

		/// <summary>Determines whether the specified object represents the same code provider and compiler settings as the current instance of <see cref="T:System.Web.Compilation.CompilerType" />.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Web.Compilation.CompilerType" /> object and its value is the same as this instance; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current instance of <see cref="T:System.Web.Compilation.CompilerType" />.</param>
		// Token: 0x06004561 RID: 17761 RVA: 0x000BDF78 File Offset: 0x000BC178
		public override bool Equals(object o)
		{
			if (!(o is CompilerType))
			{
				return false;
			}
			CompilerType compilerType = (CompilerType)o;
			return compilerType.type == this.type && compilerType.parameters == this.parameters;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code for the current instance of <see cref="T:System.Web.Compilation.CompilerType" />, suitable for use in hashing algorithms and data structures, such as a hash table.</returns>
		// Token: 0x06004562 RID: 17762 RVA: 0x000BDFB9 File Offset: 0x000BC1B9
		public override int GetHashCode()
		{
			return (this.type.GetHashCode() << 6) ^ this.parameters.GetHashCode();
		}

		/// <summary>Gets a <see cref="T:System.Type" /> for the configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation.</summary>
		/// <returns>A read-only <see cref="T:System.Type" /> that represents the configured code provider type.</returns>
		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x000BDFD4 File Offset: 0x000BC1D4
		public Type CodeDomProviderType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the settings and options used to compile source code into an assembly.</summary>
		/// <returns>A read-only <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that represents the settings and options of the code compiler.</returns>
		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x06004564 RID: 17764 RVA: 0x000BDFDC File Offset: 0x000BC1DC
		public CompilerParameters CompilerParameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal CompilerType()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040024E4 RID: 9444
		private Type type;

		// Token: 0x040024E5 RID: 9445
		private CompilerParameters parameters;
	}
}
