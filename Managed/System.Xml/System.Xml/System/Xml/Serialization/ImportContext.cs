using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Xml.Serialization
{
	/// <summary>Describes the context in which a set of schema is bound to .NET Framework code entities.</summary>
	// Token: 0x020002DD RID: 733
	public class ImportContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.ImportContext" /> class for the given code identifiers, with the given type-sharing option.</summary>
		/// <param name="identifiers">The code entities to which the context applies.</param>
		/// <param name="shareTypes">A <see cref="T:System.Boolean" /> value that determines whether custom types are shared among schema.</param>
		// Token: 0x06001B77 RID: 7031 RVA: 0x00098DC7 File Offset: 0x00096FC7
		public ImportContext(CodeIdentifiers identifiers, bool shareTypes)
		{
			this.typeIdentifiers = identifiers;
			this.shareTypes = shareTypes;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00098DDD File Offset: 0x00096FDD
		internal ImportContext()
			: this(null, false)
		{
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x00098DE7 File Offset: 0x00096FE7
		internal SchemaObjectCache Cache
		{
			get
			{
				if (this.cache == null)
				{
					this.cache = new SchemaObjectCache();
				}
				return this.cache;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x00098E02 File Offset: 0x00097002
		internal Hashtable Elements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new Hashtable();
				}
				return this.elements;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x00098E1D File Offset: 0x0009701D
		internal Hashtable Mappings
		{
			get
			{
				if (this.mappings == null)
				{
					this.mappings = new Hashtable();
				}
				return this.mappings;
			}
		}

		/// <summary>Gets a set of code entities to which the context applies.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.CodeIdentifiers" /> that specifies the code entities to which the context applies.</returns>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x00098E38 File Offset: 0x00097038
		public CodeIdentifiers TypeIdentifiers
		{
			get
			{
				if (this.typeIdentifiers == null)
				{
					this.typeIdentifiers = new CodeIdentifiers();
				}
				return this.typeIdentifiers;
			}
		}

		/// <summary>Gets a value that determines whether custom types are shared.</summary>
		/// <returns>true, if custom types are shared among schema; otherwise, false.</returns>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x00098E53 File Offset: 0x00097053
		public bool ShareTypes
		{
			get
			{
				return this.shareTypes;
			}
		}

		/// <summary>Gets a collection of warnings that are generated when importing the code entity descriptions.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains warnings that were generated when importing the code entity descriptions.</returns>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x00098E5B File Offset: 0x0009705B
		public StringCollection Warnings
		{
			get
			{
				return this.Cache.Warnings;
			}
		}

		// Token: 0x040015E9 RID: 5609
		private bool shareTypes;

		// Token: 0x040015EA RID: 5610
		private SchemaObjectCache cache;

		// Token: 0x040015EB RID: 5611
		private Hashtable mappings;

		// Token: 0x040015EC RID: 5612
		private Hashtable elements;

		// Token: 0x040015ED RID: 5613
		private CodeIdentifiers typeIdentifiers;
	}
}
