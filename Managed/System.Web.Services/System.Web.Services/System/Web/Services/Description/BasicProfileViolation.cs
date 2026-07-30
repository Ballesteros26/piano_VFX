using System;
using System.Collections.Specialized;
using System.Text;
using Unity;

namespace System.Web.Services.Description
{
	/// <summary>Represents a WSDL violation of the WSI Basic Profile version 1.1.</summary>
	// Token: 0x0200013A RID: 314
	public class BasicProfileViolation
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x00043174 File Offset: 0x00041374
		internal BasicProfileViolation(string normativeStatement)
			: this(normativeStatement, null)
		{
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00043180 File Offset: 0x00041380
		internal BasicProfileViolation(string normativeStatement, string element)
		{
			this.claims = WsiProfiles.BasicProfile1_1;
			base..ctor();
			this.normativeStatement = normativeStatement;
			int num = normativeStatement.IndexOf(',');
			if (num >= 0)
			{
				normativeStatement = normativeStatement.Substring(0, num);
			}
			this.details = Res.GetString("HelpGeneratorServiceConformance" + normativeStatement);
			this.recommendation = Res.GetString("HelpGeneratorServiceConformance" + normativeStatement + "_r");
			if (element != null)
			{
				this.Elements.Add(element);
			}
			if (this.normativeStatement == "Rxxxx")
			{
				this.normativeStatement = Res.GetString("Rxxxx");
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.WsiProfiles" /> object that specifies whether the Web service declares that it conforms to the WSI Basic Profile version 1.1.</summary>
		/// <returns>A <see cref="T:System.Web.Services.WsiProfiles" /> object that specifies whether the Web service declares that it conforms to the WSI Basic Profile version 1.1.</returns>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0004321B File Offset: 0x0004141B
		public WsiProfiles Claims
		{
			get
			{
				return this.claims;
			}
		}

		/// <summary>Gets a <see cref="T:System.String" /> that provides a detailed description of the WSDL violation of the Basic Profile.</summary>
		/// <returns>A <see cref="T:System.String" /> that provides a detailed description of the WSDL violation of the Basic Profile.</returns>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00043223 File Offset: 0x00041423
		public string Details
		{
			get
			{
				if (this.details == null)
				{
					return string.Empty;
				}
				return this.details;
			}
		}

		/// <summary>Represents WSDL elements that do not comply with the WSI Basic Profile version 1.1 specification.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the WSDL elements that do not comply with the WSI Basic Profile version 1.1 specification.</returns>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00043239 File Offset: 0x00041439
		public StringCollection Elements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new StringCollection();
				}
				return this.elements;
			}
		}

		/// <summary>Gets the identifier for the WSDL violation of the Basic Profile version 1.1 specification. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the identifier (For example, R2038) for the WSDL violation of the Basic Profile version 1.1 specification. </returns>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00043254 File Offset: 0x00041454
		public string NormativeStatement
		{
			get
			{
				return this.normativeStatement;
			}
		}

		/// <summary>Gets a <see cref="T:System.String" /> object that describes the WSDL violation of the Basic Profile version 1.1 specification.</summary>
		/// <returns>The <see cref="T:System.String" /> object that describes the WSDL violation of the Basic Profile version 1.1 specification.</returns>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0004325C File Offset: 0x0004145C
		public string Recommendation
		{
			get
			{
				return this.recommendation;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> that comprises information from <see cref="P:System.Web.Services.Description.BasicProfileViolation.NormativeStatement" />, <see cref="P:System.Web.Services.Description.BasicProfileViolation.Details" />, and <see cref="P:System.Web.Services.Description.BasicProfileViolation.Elements" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that comprises information from <see cref="P:System.Web.Services.Description.BasicProfileViolation.NormativeStatement" />, <see cref="P:System.Web.Services.Description.BasicProfileViolation.Details" />, and <see cref="P:System.Web.Services.Description.BasicProfileViolation.Elements" />.</returns>
		// Token: 0x0600099F RID: 2463 RVA: 0x00043264 File Offset: 0x00041464
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.normativeStatement);
			stringBuilder.Append(": ");
			stringBuilder.Append(this.Details);
			foreach (string text in this.Elements)
			{
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("  -  ");
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00003846 File Offset: 0x00001A46
		internal BasicProfileViolation()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400058F RID: 1423
		private WsiProfiles claims;

		// Token: 0x04000590 RID: 1424
		private string normativeStatement;

		// Token: 0x04000591 RID: 1425
		private string details;

		// Token: 0x04000592 RID: 1426
		private string recommendation;

		// Token: 0x04000593 RID: 1427
		private StringCollection elements;
	}
}
