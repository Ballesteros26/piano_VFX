using System;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace System.Data.Design
{
	/// <summary>This class is used to generate a database query method signature, as it will be created by the typed dataset generator.</summary>
	// Token: 0x020000EA RID: 234
	public class MethodSignatureGenerator
	{
		/// <summary>Gets or sets the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> object.</summary>
		/// <returns>The <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> object, which provides code generation and code compilation interfaces for generating code and managing compilation for a single programming language. Code generators can be used to generate code in a particular language, and code compilers can be used to compile code into assemblies.</returns>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public CodeDomProvider CodeProvider
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the type of object that the query will fill.</summary>
		/// <returns>The type of object that the query will fill. The type will be either <see cref="T:System.Data.DataSet" /> or <see cref="T:System.Data.DataTable" />.</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Type ContainerParameterType
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the type name of the typed dataset to be filled.</summary>
		/// <returns>A string representing the type name of the typed dataset to be filled.</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string DataSetClassName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a Boolean value specifying whether the signature generated needs to return a <see cref="T:System.Data.DataTable" /> object.</summary>
		/// <returns>A Boolean value specifying whether the signature generated needs to return a <see cref="T:System.Data.DataTable" /> object: True if the signature generated needs to return a data table, otherwise False.</returns>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool IsGetMethod
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a Boolean value specifying whether the method supports paging.</summary>
		/// <returns>A Boolean value specifying whether the method supports paging: True if the method supports paging; False if it does not.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public bool PagingMethod
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the category of types to use for parameters.</summary>
		/// <returns>A ParameterGenerationOption value specifying the category of types to use for parameters: <see cref="F:System.Data.Design.ParameterGenerationOption.ClrTypes" /> (the default), <see cref="F:System.Data.Design.ParameterGenerationOption.SqlTypes" />, or <see cref="F:System.Data.Design.ParameterGenerationOption.Objects" />.</returns>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public ParameterGenerationOption ParameterOption
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the type name of the typed table to fill.</summary>
		/// <returns>A string indicating the type name of the typed table to fill.</returns>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string TableClassName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns the code member method representing the database query, as generated by the typed dataset generator.</summary>
		/// <returns>The <see cref="T:System.CodeDom.CodeMemberMethod" /> object representing the database query, as generated by the typed dataset generator.</returns>
		// Token: 0x060006B8 RID: 1720 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public CodeMemberMethod GenerateMethod()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a string representing the database query method signature, as generated by the typed dataset generator.</summary>
		/// <returns>A string representing the database query method signature, as generated by the typed dataset generator.</returns>
		// Token: 0x060006B9 RID: 1721 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string GenerateMethodSignature()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a code type declaration containing the Insert, Update, and Delete methods associated with the database query.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object containing the Insert, Update, and Delete methods associated with the database query.</returns>
		// Token: 0x060006BA RID: 1722 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public CodeTypeDeclaration GenerateUpdatingMethods()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the schema of the design table for which the database query method will be generated.</summary>
		/// <param name="designTableContent">A string representation of the table schema.</param>
		// Token: 0x060006BB RID: 1723 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void SetDesignTableContent(string designTableContent)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the database query for which the method signature will be generated.</summary>
		/// <param name="methodSourceContent">A string representation of the database query.</param>
		// Token: 0x060006BC RID: 1724 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void SetMethodSourceContent(string methodSourceContent)
		{
			throw new NotImplementedException();
		}
	}
}
