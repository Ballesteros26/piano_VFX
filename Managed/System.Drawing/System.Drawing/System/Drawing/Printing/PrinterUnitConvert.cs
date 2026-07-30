using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies a series of conversion methods that are useful when interoperating with the Win32 printing API. This class cannot be inherited.</summary>
	// Token: 0x020000C2 RID: 194
	public sealed class PrinterUnitConvert
	{
		// Token: 0x06000A8B RID: 2699 RVA: 0x00002050 File Offset: 0x00000250
		private PrinterUnitConvert()
		{
		}

		/// <summary>Converts a double-precision floating-point number from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
		/// <returns>A double-precision floating-point number that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.Point" /> being converted. </param>
		/// <param name="fromUnit">The unit to convert from. </param>
		/// <param name="toUnit">The unit to convert to. </param>
		// Token: 0x06000A8C RID: 2700 RVA: 0x00016CF8 File Offset: 0x00014EF8
		public static double Convert(double value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			double num = PrinterUnitConvert.UnitsPerDisplay(fromUnit);
			double num2 = PrinterUnitConvert.UnitsPerDisplay(toUnit);
			return value * num2 / num;
		}

		/// <summary>Converts a 32-bit signed integer from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
		/// <returns>A 32-bit signed integer that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
		/// <param name="value">The value being converted. </param>
		/// <param name="fromUnit">The unit to convert from. </param>
		/// <param name="toUnit">The unit to convert to. </param>
		// Token: 0x06000A8D RID: 2701 RVA: 0x00016D18 File Offset: 0x00014F18
		public static int Convert(int value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return (int)Math.Round(PrinterUnitConvert.Convert((double)value, fromUnit, toUnit));
		}

		/// <summary>Converts a <see cref="T:System.Drawing.Point" /> from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.Point" /> being converted. </param>
		/// <param name="fromUnit">The unit to convert from. </param>
		/// <param name="toUnit">The unit to convert to. </param>
		// Token: 0x06000A8E RID: 2702 RVA: 0x00016D29 File Offset: 0x00014F29
		public static Point Convert(Point value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return new Point(PrinterUnitConvert.Convert(value.X, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Y, fromUnit, toUnit));
		}

		/// <summary>Converts a <see cref="T:System.Drawing.Size" /> from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.Size" /> being converted. </param>
		/// <param name="fromUnit">The unit to convert from. </param>
		/// <param name="toUnit">The unit to convert to. </param>
		// Token: 0x06000A8F RID: 2703 RVA: 0x00016D4C File Offset: 0x00014F4C
		public static Size Convert(Size value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return new Size(PrinterUnitConvert.Convert(value.Width, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Height, fromUnit, toUnit));
		}

		/// <summary>Converts a <see cref="T:System.Drawing.Rectangle" /> from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.Rectangle" /> being converted. </param>
		/// <param name="fromUnit">The unit to convert from. </param>
		/// <param name="toUnit">The unit to convert to. </param>
		// Token: 0x06000A90 RID: 2704 RVA: 0x00016D6F File Offset: 0x00014F6F
		public static Rectangle Convert(Rectangle value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return new Rectangle(PrinterUnitConvert.Convert(value.X, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Y, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Width, fromUnit, toUnit), PrinterUnitConvert.Convert(value.Height, fromUnit, toUnit));
		}

		/// <summary>Converts a <see cref="T:System.Drawing.Printing.Margins" /> from one <see cref="T:System.Drawing.Printing.PrinterUnit" /> type to another <see cref="T:System.Drawing.Printing.PrinterUnit" /> type.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.Margins" /> that represents the converted <see cref="T:System.Drawing.Printing.PrinterUnit" />.</returns>
		/// <param name="value">The <see cref="T:System.Drawing.Printing.Margins" /> being converted. </param>
		/// <param name="fromUnit">The unit to convert from. </param>
		/// <param name="toUnit">The unit to convert to. </param>
		// Token: 0x06000A91 RID: 2705 RVA: 0x00016DB0 File Offset: 0x00014FB0
		public static Margins Convert(Margins value, PrinterUnit fromUnit, PrinterUnit toUnit)
		{
			return new Margins
			{
				DoubleLeft = PrinterUnitConvert.Convert(value.DoubleLeft, fromUnit, toUnit),
				DoubleRight = PrinterUnitConvert.Convert(value.DoubleRight, fromUnit, toUnit),
				DoubleTop = PrinterUnitConvert.Convert(value.DoubleTop, fromUnit, toUnit),
				DoubleBottom = PrinterUnitConvert.Convert(value.DoubleBottom, fromUnit, toUnit)
			};
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00016E10 File Offset: 0x00015010
		private static double UnitsPerDisplay(PrinterUnit unit)
		{
			double num;
			switch (unit)
			{
			case PrinterUnit.Display:
				num = 1.0;
				break;
			case PrinterUnit.ThousandthsOfAnInch:
				num = 10.0;
				break;
			case PrinterUnit.HundredthsOfAMillimeter:
				num = 25.4;
				break;
			case PrinterUnit.TenthsOfAMillimeter:
				num = 2.54;
				break;
			default:
				num = 1.0;
				break;
			}
			return num;
		}
	}
}
