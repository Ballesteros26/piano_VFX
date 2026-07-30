using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200019A RID: 410
	public static class ControlUtilities
	{
		// Token: 0x060009EC RID: 2540 RVA: 0x00021DB2 File Offset: 0x0001FFB2
		public static ControlName GetControlName(this ControlChangeEvent controlChangeEvent)
		{
			ThrowIfArgument.IsNull("controlChangeEvent", controlChangeEvent);
			return ControlUtilities.GetControlName(controlChangeEvent.ControlNumber);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00021DCA File Offset: 0x0001FFCA
		public static SevenBitNumber AsSevenBitNumber(this ControlName controlName)
		{
			ThrowIfArgument.IsInvalidEnumValue<ControlName>("controlName", controlName);
			return (SevenBitNumber)((byte)controlName);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00021DDD File Offset: 0x0001FFDD
		public static ControlChangeEvent GetControlChangeEvent(this ControlName controlName, SevenBitNumber controlValue, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<ControlName>("controlName", controlName);
			return new ControlChangeEvent(controlName.AsSevenBitNumber(), controlValue)
			{
				Channel = channel
			};
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00021E00 File Offset: 0x00020000
		private static ControlName GetControlName(SevenBitNumber controlNumber)
		{
			byte b = controlNumber;
			if (!Enum.IsDefined(typeof(ControlName), b))
			{
				return ControlName.Undefined;
			}
			return (ControlName)b;
		}
	}
}
