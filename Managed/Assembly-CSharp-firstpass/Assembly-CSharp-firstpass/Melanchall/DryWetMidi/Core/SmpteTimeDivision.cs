using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000195 RID: 405
	public sealed class SmpteTimeDivision : TimeDivision
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x00021B9E File Offset: 0x0001FD9E
		public SmpteTimeDivision(SmpteFormat format, byte resolution)
		{
			ThrowIfArgument.IsInvalidEnumValue<SmpteFormat>("format", format);
			this.Format = format;
			this.Resolution = resolution;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00021BBF File Offset: 0x0001FDBF
		public SmpteFormat Format { get; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00021BC7 File Offset: 0x0001FDC7
		public byte Resolution { get; }

		// Token: 0x060009D5 RID: 2517 RVA: 0x00021BCF File Offset: 0x0001FDCF
		public static bool operator ==(SmpteTimeDivision timeDivision1, SmpteTimeDivision timeDivision2)
		{
			return timeDivision1 == timeDivision2 || (timeDivision1 != null && timeDivision2 != null && timeDivision1.Format == timeDivision2.Format && timeDivision1.Resolution == timeDivision2.Resolution);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00021BFD File Offset: 0x0001FDFD
		public static bool operator !=(SmpteTimeDivision timeDivision1, SmpteTimeDivision timeDivision2)
		{
			return !(timeDivision1 == timeDivision2);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00021C09 File Offset: 0x0001FE09
		internal override short ToInt16()
		{
			return (short)(-(short)DataTypesUtilities.Combine((byte)this.Format, this.Resolution));
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00021C1E File Offset: 0x0001FE1E
		public override TimeDivision Clone()
		{
			return new SmpteTimeDivision(this.Format, this.Resolution);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00021C31 File Offset: 0x0001FE31
		public override string ToString()
		{
			return string.Format("{0} frames / sec, {1} subdivisions / frame", this.Format, this.Resolution);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00021C53 File Offset: 0x0001FE53
		public override bool Equals(object obj)
		{
			return this == obj as SmpteTimeDivision;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00021C64 File Offset: 0x0001FE64
		public override int GetHashCode()
		{
			return (17 * 23 + this.Format.GetHashCode()) * 23 + this.Resolution.GetHashCode();
		}
	}
}
