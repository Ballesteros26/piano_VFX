using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000154 RID: 340
	internal class EnumCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x06000A6C RID: 2668 RVA: 0x00015528 File Offset: 0x00013728
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			TypeConverter converter = TypeDescriptor.GetConverter(value);
			Enum[] array;
			if (converter.CanConvertTo(typeof(Enum[])))
			{
				array = (Enum[])converter.ConvertTo(value, typeof(Enum[]));
			}
			else
			{
				array = new Enum[] { (Enum)value };
			}
			CodeExpression codeExpression = null;
			foreach (Enum @enum in array)
			{
				CodeExpression enumExpression = this.GetEnumExpression(@enum);
				if (codeExpression == null)
				{
					codeExpression = enumExpression;
				}
				else
				{
					codeExpression = new CodeBinaryOperatorExpression(codeExpression, CodeBinaryOperatorType.BitwiseOr, enumExpression);
				}
			}
			return codeExpression;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000155D4 File Offset: 0x000137D4
		private CodeExpression GetEnumExpression(Enum e)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(e);
			if (converter != null && converter.CanConvertTo(typeof(string)))
			{
				return new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(e.GetType().FullName), (string)converter.ConvertTo(e, typeof(string)));
			}
			return null;
		}
	}
}
