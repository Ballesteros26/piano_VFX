using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000E1 RID: 225
	public sealed class MidiClockSettings
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0001889F File Offset: 0x00016A9F
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x000188A7 File Offset: 0x00016AA7
		public CreateTickGeneratorCallback CreateTickGeneratorCallback
		{
			get
			{
				return this._createTickGeneratorCallback;
			}
			set
			{
				ThrowIfArgument.IsNull("value", value);
				this._createTickGeneratorCallback = value;
			}
		}

		// Token: 0x0400073F RID: 1855
		private CreateTickGeneratorCallback _createTickGeneratorCallback = (TimeSpan interval) => new HighPrecisionTickGenerator(interval);
	}
}
