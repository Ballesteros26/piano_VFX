using System;
using System.Collections;
using System.IO;

namespace System.Resources
{
	/// <summary>Gathers all items that represent an XML resource (.resx) file into a single object.</summary>
	// Token: 0x02000010 RID: 16
	public class ResXResourceSet : ResourceSet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceSet" /> class using the system default <see cref="T:System.Resources.ResXResourceReader" /> to read resources from the specified stream.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> of resources to be read. The stream should refer to an existing resource file. </param>
		// Token: 0x06000051 RID: 81 RVA: 0x00003184 File Offset: 0x00001384
		public ResXResourceSet(Stream stream)
		{
			this.Reader = new ResXResourceReader(stream);
			this.Table = new Hashtable();
			this.ReadResources();
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Resources.ResXResourceSet" /> class using the system default <see cref="T:System.Resources.ResXResourceReader" /> that opens and reads resources from the specified file.</summary>
		/// <param name="fileName">The name of the file to read resources from. </param>
		// Token: 0x06000052 RID: 82 RVA: 0x000031AC File Offset: 0x000013AC
		public ResXResourceSet(string fileName)
		{
			this.Reader = new ResXResourceReader(fileName);
			this.Table = new Hashtable();
			this.ReadResources();
		}

		/// <summary>Returns the preferred resource reader class for this kind of <see cref="T:System.Resources.ResXResourceSet" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the preferred resource reader for this kind of <see cref="T:System.Resources.ResXResourceSet" />.</returns>
		// Token: 0x06000053 RID: 83 RVA: 0x000031D4 File Offset: 0x000013D4
		public override Type GetDefaultReader()
		{
			return typeof(ResXResourceReader);
		}

		/// <summary>Returns the preferred resource writer class for this kind of <see cref="T:System.Resources.ResXResourceSet" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the preferred resource writer for this kind of <see cref="T:System.Resources.ResXResourceSet" />.</returns>
		// Token: 0x06000054 RID: 84 RVA: 0x000031E0 File Offset: 0x000013E0
		public override Type GetDefaultWriter()
		{
			return typeof(ResXResourceWriter);
		}
	}
}
