using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using Unity;

namespace System.Data.Design
{
	/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> class.</summary>
	// Token: 0x020000EC RID: 236
	public sealed class TypedDataSetGenerator
	{
		// Token: 0x060006BD RID: 1725 RVA: 0x00002352 File Offset: 0x00000552
		private TypedDataSetGenerator()
		{
		}

		/// <summary>Gets or sets the collection of assemblies referenced in a typed dataset.</summary>
		/// <returns>A collection containing all referenced assemblies in the dataset.</returns>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static ICollection<Assembly> ReferencedAssemblies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on an existing <see cref="T:System.Data.DataSet" />. </summary>
		/// <returns>A strongly typed <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The source <see cref="T:System.Data.DataSet" /> that specifies the metadata for the typed <see cref="T:System.Data.DataSet" />.</param>
		/// <param name="codeNamespace">The namespace that provides the target namespace for the typed <see cref="T:System.Data.DataSet" />.</param>
		/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
		// Token: 0x060006BF RID: 1727 RVA: 0x0000A567 File Offset: 0x00008767
		public static string Generate(DataSet dataSet, CodeNamespace codeNamespace, CodeDomProvider codeProvider)
		{
			TypedDataSetGenerator.Generate(dataSet, codeNamespace, codeProvider.CreateGenerator());
			return null;
		}

		/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
		/// <returns>A strongly typed <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
		/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
		/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
		/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
		// Token: 0x060006C0 RID: 1728 RVA: 0x0000A577 File Offset: 0x00008777
		public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider)
		{
			DataSet dataSet = new DataSet();
			dataSet.ReadXmlSchema(inputFileContent);
			TypedDataSetGenerator.Generate(dataSet, mainNamespace, codeProvider.CreateGenerator());
			return null;
		}

		/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
		/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
		/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
		/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
		/// <param name="codeProvider">The language specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
		/// <param name="customDBProviders">A HashTable that maps connections to specific providers in the typed dataset.</param>
		// Token: 0x060006C1 RID: 1729 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
		/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
		/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
		/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
		/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
		/// <param name="specifiedFactory">The <see cref="T:System.Data.Common.DbProviderFactory" /> to use to override the provider contained in the <paramref name="inputFileContent" />.</param>
		// Token: 0x060006C2 RID: 1730 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, DbProviderFactory specifiedFactory)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
		/// <returns>A strongly typed <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
		/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
		/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
		/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
		/// <param name="option">The <see cref="T:System.Data.Design.TypedDataSetGenerator.GenerateOption" /> that determines what (if any) additional components and methods to create when generating a typed dataset.</param>
		// Token: 0x060006C3 RID: 1731 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, TypedDataSetGenerator.GenerateOption option)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
		/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
		/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
		/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
		/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
		/// <param name="customDBProviders">A HashTable that maps connections to specific providers in the typed dataset.</param>
		/// <param name="option">The <see cref="T:System.Data.Design.TypedDataSetGenerator.GenerateOption" /> that determines what (if any) additional components and methods to create when generating a typed dataset.</param>
		// Token: 0x060006C4 RID: 1732 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders, TypedDataSetGenerator.GenerateOption option)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the first provider name found in the provided input file.</summary>
		/// <returns>A string that represents the specific provider for this <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
		// Token: 0x060006C5 RID: 1733 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static string GetProviderName(string inputFileContent)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the provider name for the <paramref name="tableName" /> in the input file.</summary>
		/// <returns>A string that represents the provider name for the specific table passed in to the <paramref name="tableName" /> parameter.</returns>
		/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
		/// <param name="tableName">A string that represents the name of the table to return the provider name from.</param>
		// Token: 0x060006C6 RID: 1734 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static string GetProviderName(string inputFileContent, string tableName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
		/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
		/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
		/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
		/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
		/// <param name="customDBProviders">A HashTable that maps connections to specific providers in the typed dataset.</param>
		/// <param name="option">The <see cref="T:System.Data.Design.TypedDataSetGenerator.GenerateOption" /> that determines what (if any) additional components and methods to create when generating a typed dataset.</param>
		/// <param name="dataSetNamespace">A string that contains the namespace of the generated dataset.</param>
		// Token: 0x060006C7 RID: 1735 RVA: 0x00009519 File Offset: 0x00007719
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders, TypedDataSetGenerator.GenerateOption option, string dataSetNamespace)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
		/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
		/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
		/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
		/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
		/// <param name="customDBProviders">A HashTable that maps connections to specific providers in the typed dataset.</param>
		/// <param name="option">The <see cref="T:System.Data.Design.TypedDataSetGenerator.GenerateOption" /> that determines what (if any) additional components and methods to create when generating a typed dataset.</param>
		/// <param name="dataSetNamespace">A string that contains the namespace of the generated dataset.</param>
		/// <param name="basePath">A string that represents the path to the schema based on the relative path of the dataset input file.</param>
		// Token: 0x060006C8 RID: 1736 RVA: 0x00009519 File Offset: 0x00007719
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders, TypedDataSetGenerator.GenerateOption option, string dataSetNamespace, string basePath)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
		/// <returns>A strongly typed <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
		/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
		/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
		/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
		/// <param name="option">The <see cref="T:System.Data.Design.TypedDataSetGenerator.GenerateOption" /> that determines what (if any) additional components and methods to create when generating a typed dataset.</param>
		/// <param name="dataSetNamespace">A string that contains the namespace of the generated dataset.</param>
		// Token: 0x060006C9 RID: 1737 RVA: 0x0000970B File Offset: 0x0000790B
		public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, TypedDataSetGenerator.GenerateOption option, string dataSetNamespace)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" /> based on the provided input file.</summary>
		/// <returns>A strongly typed <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="inputFileContent">A string that represents the XML schema to base the <see cref="T:System.Data.DataSet" /> on.</param>
		/// <param name="compileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to contain the generated code.</param>
		/// <param name="mainNamespace">The <see cref="T:System.CodeDom.CodeNamespace" /> that contains the generated dataset.</param>
		/// <param name="codeProvider">The language-specific <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> to use to generate the dataset.</param>
		/// <param name="option">The <see cref="T:System.Data.Design.TypedDataSetGenerator.GenerateOption" /> that determines what (if any) additional components and methods to create when generating a typed dataset.</param>
		/// <param name="dataSetNamespace">A string that contains the namespace of the generated dataset.</param>
		/// <param name="basePath">A string that represents the path to the schema based on the relative path of the dataset input file.</param>
		// Token: 0x060006CA RID: 1738 RVA: 0x0000970B File Offset: 0x0000790B
		public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, TypedDataSetGenerator.GenerateOption option, string dataSetNamespace, string basePath)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Provides the <see cref="T:System.Data.Design.TypedDataSetGenerator" />with information for creating typed datasets that support LINQ to DataSet and hierarchical update.</summary>
		// Token: 0x020000ED RID: 237
		[Flags]
		public enum GenerateOption
		{
			/// <summary>Generates typed datasets that are compatible with typed datasets generated in versions of Visual Studio earlier than Visual Studio 2008.</summary>
			// Token: 0x04000160 RID: 352
			None = 0,
			/// <summary>Generates typed datasets that have a TableAdapterManager and associated methods for enabling hierarchical update.</summary>
			// Token: 0x04000161 RID: 353
			HierarchicalUpdate = 1,
			/// <summary>Generates typed datasets that have data tables that inherit from <see cref="T:System.Data.TypedTableBase`1" /> in order to enable the ability to perform LINQ queries on data tables.</summary>
			// Token: 0x04000162 RID: 354
			LinqOverTypedDatasets = 2
		}
	}
}
