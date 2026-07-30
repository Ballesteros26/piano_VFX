using System;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000056 RID: 86
	public abstract class RandomizingSettings<TObject>
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00009B62 File Offset: 0x00007D62
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00009B6A File Offset: 0x00007D6A
		public Predicate<TObject> Filter { get; set; }
	}
}
