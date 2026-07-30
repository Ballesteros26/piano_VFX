using System;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	/// <summary>Represents an item in the XQuery 1.0 and XPath 2.0 Data Model.</summary>
	// Token: 0x020002B7 RID: 695
	public abstract class XPathItem
	{
		/// <summary>When overridden in a derived class, gets a value indicating whether the item represents an XPath node or an atomic value.</summary>
		/// <returns>true if the item represents an XPath node; false if the item represents an atomic value.</returns>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600195B RID: 6491
		public abstract bool IsNode { get; }

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.Schema.XmlSchemaType" /> for the item.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaType" /> for the item.</returns>
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600195C RID: 6492
		public abstract XmlSchemaType XmlType { get; }

		/// <summary>When overridden in a derived class, gets the string value of the item.</summary>
		/// <returns>The string value of the item.</returns>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600195D RID: 6493
		public abstract string Value { get; }

		/// <summary>When overridden in a derived class, gets the current item as a boxed object of the most appropriate .NET Framework 2.0 type according to its schema type.</summary>
		/// <returns>The current item as a boxed object of the most appropriate .NET Framework type.</returns>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600195E RID: 6494
		public abstract object TypedValue { get; }

		/// <summary>When overridden in a derived class, gets the .NET Framework 2.0 type of the item.</summary>
		/// <returns>The .NET Framework type of the item. The default value is <see cref="T:System.String" />.</returns>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600195F RID: 6495
		public abstract Type ValueType { get; }

		/// <summary>When overridden in a derived class, gets the item's value as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The item's value as a <see cref="T:System.Boolean" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.Boolean" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Boolean" /> is not valid.</exception>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001960 RID: 6496
		public abstract bool ValueAsBoolean { get; }

		/// <summary>When overridden in a derived class, gets the item's value as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The item's value as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.DateTime" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.DateTime" /> is not valid.</exception>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001961 RID: 6497
		public abstract DateTime ValueAsDateTime { get; }

		/// <summary>When overridden in a derived class, gets the item's value as a <see cref="T:System.Double" />.</summary>
		/// <returns>The item's value as a <see cref="T:System.Double" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.Double" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Double" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001962 RID: 6498
		public abstract double ValueAsDouble { get; }

		/// <summary>When overridden in a derived class, gets the item's value as an <see cref="T:System.Int32" />.</summary>
		/// <returns>The item's value as an <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.Int32" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int32" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001963 RID: 6499
		public abstract int ValueAsInt { get; }

		/// <summary>When overridden in a derived class, gets the item's value as an <see cref="T:System.Int64" />.</summary>
		/// <returns>The item's value as an <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.Int64" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int64" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001964 RID: 6500
		public abstract long ValueAsLong { get; }

		/// <summary>Returns the item's value as the specified type.</summary>
		/// <returns>The value of the item as the type requested.</returns>
		/// <param name="returnType">The type to return the item value as.</param>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x06001965 RID: 6501 RVA: 0x00090BA8 File Offset: 0x0008EDA8
		public virtual object ValueAs(Type returnType)
		{
			return this.ValueAs(returnType, null);
		}

		/// <summary>When overridden in a derived class, returns the item's value as the type specified using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <returns>The value of the item as the type requested.</returns>
		/// <param name="returnType">The type to return the item's value as.</param>
		/// <param name="nsResolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x06001966 RID: 6502
		public abstract object ValueAs(Type returnType, IXmlNamespaceResolver nsResolver);
	}
}
