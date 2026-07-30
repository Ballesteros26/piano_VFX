using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000AB RID: 171
	internal sealed class ConstNode : ExpressionNode
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x0002FD0C File Offset: 0x0002DF0C
		internal ConstNode(DataTable table, ValueType type, object constant)
			: this(table, type, constant, true)
		{
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002FD18 File Offset: 0x0002DF18
		internal ConstNode(DataTable table, ValueType type, object constant, bool fParseQuotes)
			: base(table)
		{
			switch (type)
			{
			case ValueType.Null:
				this._val = DBNull.Value;
				return;
			case ValueType.Bool:
				this._val = Convert.ToBoolean(constant, CultureInfo.InvariantCulture);
				return;
			case ValueType.Numeric:
				this._val = this.SmallestNumeric(constant);
				return;
			case ValueType.Str:
				if (fParseQuotes)
				{
					this._val = ((string)constant).Replace("''", "'");
					return;
				}
				this._val = (string)constant;
				return;
			case ValueType.Float:
				this._val = Convert.ToDouble(constant, NumberFormatInfo.InvariantInfo);
				return;
			case ValueType.Decimal:
				this._val = this.SmallestDecimal(constant);
				return;
			case ValueType.Date:
				this._val = DateTime.Parse((string)constant, CultureInfo.InvariantCulture);
				return;
			}
			this._val = constant;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002FDFD File Offset: 0x0002DFFD
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002FE06 File Offset: 0x0002E006
		internal override object Eval()
		{
			return this._val;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002FE0E File Offset: 0x0002E00E
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.Eval();
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002FE0E File Offset: 0x0002E00E
		internal override object Eval(int[] recordNos)
		{
			return this.Eval();
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0000EF2B File Offset: 0x0000D12B
		internal override bool IsConstant()
		{
			return true;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0000EF2B File Offset: 0x0000D12B
		internal override bool IsTableConstant()
		{
			return true;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00005D82 File Offset: 0x00003F82
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002FE18 File Offset: 0x0002E018
		private object SmallestDecimal(object constant)
		{
			if (constant == null)
			{
				return 0.0;
			}
			string text = constant as string;
			if (text != null)
			{
				decimal num;
				if (decimal.TryParse(text, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
				double num2;
				if (double.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out num2))
				{
					return num2;
				}
			}
			else
			{
				IConvertible convertible = constant as IConvertible;
				if (convertible != null)
				{
					try
					{
						return convertible.ToDecimal(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					}
					catch (FormatException ex2)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex2);
					}
					catch (InvalidCastException ex3)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex3);
					}
					catch (OverflowException ex4)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex4);
					}
					try
					{
						return convertible.ToDouble(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex5)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex5);
					}
					catch (FormatException ex6)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex6);
					}
					catch (InvalidCastException ex7)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex7);
					}
					catch (OverflowException ex8)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex8);
					}
					return constant;
				}
			}
			return constant;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002FF50 File Offset: 0x0002E150
		private object SmallestNumeric(object constant)
		{
			if (constant == null)
			{
				return 0;
			}
			string text = constant as string;
			if (text != null)
			{
				int num;
				if (int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
				long num2;
				if (long.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2))
				{
					return num2;
				}
				double num3;
				if (double.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out num3))
				{
					return num3;
				}
			}
			else
			{
				IConvertible convertible = constant as IConvertible;
				if (convertible != null)
				{
					try
					{
						return convertible.ToInt32(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					}
					catch (FormatException ex2)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex2);
					}
					catch (InvalidCastException ex3)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex3);
					}
					catch (OverflowException ex4)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex4);
					}
					try
					{
						return convertible.ToInt64(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex5)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex5);
					}
					catch (FormatException ex6)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex6);
					}
					catch (InvalidCastException ex7)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex7);
					}
					catch (OverflowException ex8)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex8);
					}
					try
					{
						return convertible.ToDouble(NumberFormatInfo.InvariantInfo);
					}
					catch (ArgumentException ex9)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex9);
					}
					catch (FormatException ex10)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex10);
					}
					catch (InvalidCastException ex11)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex11);
					}
					catch (OverflowException ex12)
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex12);
					}
					return constant;
				}
			}
			return constant;
		}

		// Token: 0x040006E6 RID: 1766
		internal readonly object _val;
	}
}
