using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines identifiers used to determine whether a code generator supports certain types of code elements.</summary>
	// Token: 0x020007B1 RID: 1969
	[Flags]
	public enum GeneratorSupport
	{
		/// <summary>Indicates the generator supports arrays of arrays.</summary>
		// Token: 0x04002E5A RID: 11866
		ArraysOfArrays = 1,
		/// <summary>Indicates the generator supports a program entry point method designation. This is used when building executables.</summary>
		// Token: 0x04002E5B RID: 11867
		EntryPointMethod = 2,
		/// <summary>Indicates the generator supports goto statements.</summary>
		// Token: 0x04002E5C RID: 11868
		GotoStatements = 4,
		/// <summary>Indicates the generator supports referencing multidimensional arrays. Currently, the CodeDom cannot be used to instantiate multidimensional arrays.</summary>
		// Token: 0x04002E5D RID: 11869
		MultidimensionalArrays = 8,
		/// <summary>Indicates the generator supports static constructors.</summary>
		// Token: 0x04002E5E RID: 11870
		StaticConstructors = 16,
		/// <summary>Indicates the generator supports try...catch statements.</summary>
		// Token: 0x04002E5F RID: 11871
		TryCatchStatements = 32,
		/// <summary>Indicates the generator supports return type attribute declarations.</summary>
		// Token: 0x04002E60 RID: 11872
		ReturnTypeAttributes = 64,
		/// <summary>Indicates the generator supports value type declarations.</summary>
		// Token: 0x04002E61 RID: 11873
		DeclareValueTypes = 128,
		/// <summary>Indicates the generator supports enumeration declarations.</summary>
		// Token: 0x04002E62 RID: 11874
		DeclareEnums = 256,
		/// <summary>Indicates the generator supports delegate declarations.</summary>
		// Token: 0x04002E63 RID: 11875
		DeclareDelegates = 512,
		/// <summary>Indicates the generator supports interface declarations.</summary>
		// Token: 0x04002E64 RID: 11876
		DeclareInterfaces = 1024,
		/// <summary>Indicates the generator supports event declarations.</summary>
		// Token: 0x04002E65 RID: 11877
		DeclareEvents = 2048,
		/// <summary>Indicates the generator supports assembly attributes.</summary>
		// Token: 0x04002E66 RID: 11878
		AssemblyAttributes = 4096,
		/// <summary>Indicates the generator supports parameter attributes.</summary>
		// Token: 0x04002E67 RID: 11879
		ParameterAttributes = 8192,
		/// <summary>Indicates the generator supports reference and out parameters.</summary>
		// Token: 0x04002E68 RID: 11880
		ReferenceParameters = 16384,
		/// <summary>Indicates the generator supports chained constructor arguments.</summary>
		// Token: 0x04002E69 RID: 11881
		ChainedConstructorArguments = 32768,
		/// <summary>Indicates the generator supports the declaration of nested types.</summary>
		// Token: 0x04002E6A RID: 11882
		NestedTypes = 65536,
		/// <summary>Indicates the generator supports the declaration of members that implement multiple interfaces.</summary>
		// Token: 0x04002E6B RID: 11883
		MultipleInterfaceMembers = 131072,
		/// <summary>Indicates the generator supports public static members.</summary>
		// Token: 0x04002E6C RID: 11884
		PublicStaticMembers = 262144,
		/// <summary>Indicates the generator supports complex expressions.</summary>
		// Token: 0x04002E6D RID: 11885
		ComplexExpressions = 524288,
		/// <summary>Indicates the generator supports compilation with Win32 resources.</summary>
		// Token: 0x04002E6E RID: 11886
		Win32Resources = 1048576,
		/// <summary>Indicates the generator supports compilation with .NET Framework resources. These can be default resources compiled directly into an assembly, or resources referenced in a satellite assembly.</summary>
		// Token: 0x04002E6F RID: 11887
		Resources = 2097152,
		/// <summary>Indicates the generator supports partial type declarations.</summary>
		// Token: 0x04002E70 RID: 11888
		PartialTypes = 4194304,
		/// <summary>Indicates the generator supports generic type references.</summary>
		// Token: 0x04002E71 RID: 11889
		GenericTypeReference = 8388608,
		/// <summary>Indicates the generator supports generic type declarations.</summary>
		// Token: 0x04002E72 RID: 11890
		GenericTypeDeclaration = 16777216,
		/// <summary>Indicates the generator supports the declaration of indexer properties.</summary>
		// Token: 0x04002E73 RID: 11891
		DeclareIndexerProperties = 33554432
	}
}
