using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000113 RID: 275
	public sealed class ChunkType
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x0001C9B1 File Offset: 0x0001ABB1
		public ChunkType(Type type, string id)
		{
			this.Type = type;
			this.Id = id;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001C9C7 File Offset: 0x0001ABC7
		public Type Type { get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001C9CF File Offset: 0x0001ABCF
		public string Id { get; }
	}
}
