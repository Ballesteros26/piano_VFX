using System;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x0200009A RID: 154
	public sealed class SplittedLengthedObject<TObject> where TObject : ILengthedObject
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00011197 File Offset: 0x0000F397
		internal SplittedLengthedObject(TObject leftPart, TObject rightPart)
		{
			this.LeftPart = leftPart;
			this.RightPart = rightPart;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000337 RID: 823 RVA: 0x000111AD File Offset: 0x0000F3AD
		public TObject LeftPart { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000338 RID: 824 RVA: 0x000111B5 File Offset: 0x0000F3B5
		public TObject RightPart { get; }
	}
}
