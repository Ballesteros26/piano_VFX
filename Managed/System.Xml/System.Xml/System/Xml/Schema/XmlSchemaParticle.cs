using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Abstract class for that is the base class for all particle types (e.g. <see cref="T:System.Xml.Schema.XmlSchemaAny" />).</summary>
	// Token: 0x02000477 RID: 1143
	public abstract class XmlSchemaParticle : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the number as a string value. The minimum number of times the particle can occur.</summary>
		/// <returns>The number as a string value. String.Empty indicates that MinOccurs is equal to the default value. The default is a null reference.</returns>
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x00107AD6 File Offset: 0x00105CD6
		// (set) Token: 0x06002CE8 RID: 11496 RVA: 0x00107AF0 File Offset: 0x00105CF0
		[XmlAttribute("minOccurs")]
		public string MinOccursString
		{
			get
			{
				if ((this.flags & XmlSchemaParticle.Occurs.Min) != XmlSchemaParticle.Occurs.None)
				{
					return XmlConvert.ToString(this.minOccurs);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.minOccurs = 1m;
					this.flags &= ~XmlSchemaParticle.Occurs.Min;
					return;
				}
				this.minOccurs = XmlConvert.ToInteger(value);
				if (this.minOccurs < 0m)
				{
					throw new XmlSchemaException("The value for the 'minOccurs' attribute must be xsd:nonNegativeInteger.", string.Empty);
				}
				this.flags |= XmlSchemaParticle.Occurs.Min;
			}
		}

		/// <summary>Gets or sets the number as a string value. Maximum number of times the particle can occur.</summary>
		/// <returns>The number as a string value. String.Empty indicates that MaxOccurs is equal to the default value. The default is a null reference.</returns>
		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x00107B57 File Offset: 0x00105D57
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x00107B90 File Offset: 0x00105D90
		[XmlAttribute("maxOccurs")]
		public string MaxOccursString
		{
			get
			{
				if ((this.flags & XmlSchemaParticle.Occurs.Max) == XmlSchemaParticle.Occurs.None)
				{
					return null;
				}
				if (!(this.maxOccurs == 79228162514264337593543950335m))
				{
					return XmlConvert.ToString(this.maxOccurs);
				}
				return "unbounded";
			}
			set
			{
				if (value == null)
				{
					this.maxOccurs = 1m;
					this.flags &= ~XmlSchemaParticle.Occurs.Max;
					return;
				}
				if (value == "unbounded")
				{
					this.maxOccurs = decimal.MaxValue;
				}
				else
				{
					this.maxOccurs = XmlConvert.ToInteger(value);
					if (this.maxOccurs < 0m)
					{
						throw new XmlSchemaException("The value for the 'maxOccurs' attribute must be xsd:nonNegativeInteger or 'unbounded'.", string.Empty);
					}
					if (this.maxOccurs == 0m && (this.flags & XmlSchemaParticle.Occurs.Min) == XmlSchemaParticle.Occurs.None)
					{
						this.minOccurs = 0m;
					}
				}
				this.flags |= XmlSchemaParticle.Occurs.Max;
			}
		}

		/// <summary>Gets or sets the minimum number of times the particle can occur.</summary>
		/// <returns>The minimum number of times the particle can occur. The default is 1.</returns>
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x00107C3E File Offset: 0x00105E3E
		// (set) Token: 0x06002CEC RID: 11500 RVA: 0x00107C48 File Offset: 0x00105E48
		[XmlIgnore]
		public decimal MinOccurs
		{
			get
			{
				return this.minOccurs;
			}
			set
			{
				if (value < 0m || value != decimal.Truncate(value))
				{
					throw new XmlSchemaException("The value for the 'minOccurs' attribute must be xsd:nonNegativeInteger.", string.Empty);
				}
				this.minOccurs = value;
				this.flags |= XmlSchemaParticle.Occurs.Min;
			}
		}

		/// <summary>Gets or sets the maximum number of times the particle can occur.</summary>
		/// <returns>The maximum number of times the particle can occur. The default is 1.</returns>
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x00107C95 File Offset: 0x00105E95
		// (set) Token: 0x06002CEE RID: 11502 RVA: 0x00107CA0 File Offset: 0x00105EA0
		[XmlIgnore]
		public decimal MaxOccurs
		{
			get
			{
				return this.maxOccurs;
			}
			set
			{
				if (value < 0m || value != decimal.Truncate(value))
				{
					throw new XmlSchemaException("The value for the 'maxOccurs' attribute must be xsd:nonNegativeInteger or 'unbounded'.", string.Empty);
				}
				this.maxOccurs = value;
				if (this.maxOccurs == 0m && (this.flags & XmlSchemaParticle.Occurs.Min) == XmlSchemaParticle.Occurs.None)
				{
					this.minOccurs = 0m;
				}
				this.flags |= XmlSchemaParticle.Occurs.Max;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x00107D15 File Offset: 0x00105F15
		internal virtual bool IsEmpty
		{
			get
			{
				return this.maxOccurs == 0m;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x00107D27 File Offset: 0x00105F27
		internal bool IsMultipleOccurrence
		{
			get
			{
				return this.maxOccurs > 1m;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x00003065 File Offset: 0x00001265
		internal virtual string NameString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x00107D3C File Offset: 0x00105F3C
		internal XmlQualifiedName GetQualifiedName()
		{
			XmlSchemaElement xmlSchemaElement = this as XmlSchemaElement;
			if (xmlSchemaElement != null)
			{
				return xmlSchemaElement.QualifiedName;
			}
			XmlSchemaAny xmlSchemaAny = this as XmlSchemaAny;
			if (xmlSchemaAny != null)
			{
				string text = xmlSchemaAny.Namespace;
				if (text != null)
				{
					text = text.Trim();
				}
				else
				{
					text = string.Empty;
				}
				return new XmlQualifiedName("*", (text.Length == 0) ? "##any" : text);
			}
			return XmlQualifiedName.Empty;
		}

		// Token: 0x04001DF2 RID: 7666
		private decimal minOccurs = 1m;

		// Token: 0x04001DF3 RID: 7667
		private decimal maxOccurs = 1m;

		// Token: 0x04001DF4 RID: 7668
		private XmlSchemaParticle.Occurs flags;

		// Token: 0x04001DF5 RID: 7669
		internal static readonly XmlSchemaParticle Empty = new XmlSchemaParticle.EmptyParticle();

		// Token: 0x02000478 RID: 1144
		[Flags]
		private enum Occurs
		{
			// Token: 0x04001DF7 RID: 7671
			None = 0,
			// Token: 0x04001DF8 RID: 7672
			Min = 1,
			// Token: 0x04001DF9 RID: 7673
			Max = 2
		}

		// Token: 0x02000479 RID: 1145
		private class EmptyParticle : XmlSchemaParticle
		{
			// Token: 0x170009D3 RID: 2515
			// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x00003242 File Offset: 0x00001442
			internal override bool IsEmpty
			{
				get
				{
					return true;
				}
			}
		}
	}
}
