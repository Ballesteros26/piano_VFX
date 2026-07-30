using System;

namespace System.Web.UI
{
	/// <summary>Specifies the HTML tags that can be passed to an <see cref="T:System.Web.UI.HtmlTextWriter" /> or <see cref="T:System.Web.UI.Html32TextWriter" /> object output stream.</summary>
	// Token: 0x02000165 RID: 357
	public enum HtmlTextWriterTag
	{
		/// <summary>The string passed as an HTML tag is not recognized. </summary>
		// Token: 0x040012B0 RID: 4784
		Unknown,
		/// <summary>The HTML a element. </summary>
		// Token: 0x040012B1 RID: 4785
		A,
		/// <summary>The HTML acronym element. </summary>
		// Token: 0x040012B2 RID: 4786
		Acronym,
		/// <summary>The HTML address element. </summary>
		// Token: 0x040012B3 RID: 4787
		Address,
		/// <summary>The HTML area element. </summary>
		// Token: 0x040012B4 RID: 4788
		Area,
		/// <summary>The HTML b element. </summary>
		// Token: 0x040012B5 RID: 4789
		B,
		/// <summary>The HTML base element. </summary>
		// Token: 0x040012B6 RID: 4790
		Base,
		/// <summary>The HTML basefont element. </summary>
		// Token: 0x040012B7 RID: 4791
		Basefont,
		/// <summary>The HTML bdo element. </summary>
		// Token: 0x040012B8 RID: 4792
		Bdo,
		/// <summary>The HTML bgsound element. </summary>
		// Token: 0x040012B9 RID: 4793
		Bgsound,
		/// <summary>The HTML big element. </summary>
		// Token: 0x040012BA RID: 4794
		Big,
		/// <summary>The HTML blockquote element. </summary>
		// Token: 0x040012BB RID: 4795
		Blockquote,
		/// <summary>The HTML body element. </summary>
		// Token: 0x040012BC RID: 4796
		Body,
		/// <summary>The HTML br element. </summary>
		// Token: 0x040012BD RID: 4797
		Br,
		/// <summary>The HTML button element. </summary>
		// Token: 0x040012BE RID: 4798
		Button,
		/// <summary>The HTML caption element. </summary>
		// Token: 0x040012BF RID: 4799
		Caption,
		/// <summary>The HTML center element. </summary>
		// Token: 0x040012C0 RID: 4800
		Center,
		/// <summary>The HTML cite element. </summary>
		// Token: 0x040012C1 RID: 4801
		Cite,
		/// <summary>The HTML code element. </summary>
		// Token: 0x040012C2 RID: 4802
		Code,
		/// <summary>The HTML col element. </summary>
		// Token: 0x040012C3 RID: 4803
		Col,
		/// <summary>The HTML colgroup element. </summary>
		// Token: 0x040012C4 RID: 4804
		Colgroup,
		/// <summary>The HTML dd element. </summary>
		// Token: 0x040012C5 RID: 4805
		Dd,
		/// <summary>The HTML del element. </summary>
		// Token: 0x040012C6 RID: 4806
		Del,
		/// <summary>The HTML dfn element. </summary>
		// Token: 0x040012C7 RID: 4807
		Dfn,
		/// <summary>The HTML dir element. </summary>
		// Token: 0x040012C8 RID: 4808
		Dir,
		/// <summary>The HTML div element. </summary>
		// Token: 0x040012C9 RID: 4809
		Div,
		/// <summary>The HTML dl element. </summary>
		// Token: 0x040012CA RID: 4810
		Dl,
		/// <summary>The HTML dt element. </summary>
		// Token: 0x040012CB RID: 4811
		Dt,
		/// <summary>The HTML em element. </summary>
		// Token: 0x040012CC RID: 4812
		Em,
		/// <summary>The HTML embed element. </summary>
		// Token: 0x040012CD RID: 4813
		Embed,
		/// <summary>The HTML fieldset element. </summary>
		// Token: 0x040012CE RID: 4814
		Fieldset,
		/// <summary>The HTML font element. </summary>
		// Token: 0x040012CF RID: 4815
		Font,
		/// <summary>The HTML form element. </summary>
		// Token: 0x040012D0 RID: 4816
		Form,
		/// <summary>The HTML frame element. </summary>
		// Token: 0x040012D1 RID: 4817
		Frame,
		/// <summary>The HTML frameset element. </summary>
		// Token: 0x040012D2 RID: 4818
		Frameset,
		/// <summary>The HTML H1 element. </summary>
		// Token: 0x040012D3 RID: 4819
		H1,
		/// <summary>The HTML H2 element. </summary>
		// Token: 0x040012D4 RID: 4820
		H2,
		/// <summary>The HTML H3 element. </summary>
		// Token: 0x040012D5 RID: 4821
		H3,
		/// <summary>The HTML H4 element. </summary>
		// Token: 0x040012D6 RID: 4822
		H4,
		/// <summary>The HTML H5 element. </summary>
		// Token: 0x040012D7 RID: 4823
		H5,
		/// <summary>The HTML H6 element. </summary>
		// Token: 0x040012D8 RID: 4824
		H6,
		/// <summary>The HTML head element. </summary>
		// Token: 0x040012D9 RID: 4825
		Head,
		/// <summary>The HTML hr element. </summary>
		// Token: 0x040012DA RID: 4826
		Hr,
		/// <summary>The HTML html element. </summary>
		// Token: 0x040012DB RID: 4827
		Html,
		/// <summary>The HTML i element. </summary>
		// Token: 0x040012DC RID: 4828
		I,
		/// <summary>The HTML iframe element. </summary>
		// Token: 0x040012DD RID: 4829
		Iframe,
		/// <summary>The HTML img element. </summary>
		// Token: 0x040012DE RID: 4830
		Img,
		/// <summary>The HTML input element. </summary>
		// Token: 0x040012DF RID: 4831
		Input,
		/// <summary>The HTML ins element. </summary>
		// Token: 0x040012E0 RID: 4832
		Ins,
		/// <summary>The HTML isindex element. </summary>
		// Token: 0x040012E1 RID: 4833
		Isindex,
		/// <summary>The HTML kbd element. </summary>
		// Token: 0x040012E2 RID: 4834
		Kbd,
		/// <summary>The HTML label element. </summary>
		// Token: 0x040012E3 RID: 4835
		Label,
		/// <summary>The HTML legend element. </summary>
		// Token: 0x040012E4 RID: 4836
		Legend,
		/// <summary>The HTML li element. </summary>
		// Token: 0x040012E5 RID: 4837
		Li,
		/// <summary>The HTML link element. </summary>
		// Token: 0x040012E6 RID: 4838
		Link,
		/// <summary>The HTML map element. </summary>
		// Token: 0x040012E7 RID: 4839
		Map,
		/// <summary>The HTML marquee element. </summary>
		// Token: 0x040012E8 RID: 4840
		Marquee,
		/// <summary>The HTML menu element. </summary>
		// Token: 0x040012E9 RID: 4841
		Menu,
		/// <summary>The HTML meta element. </summary>
		// Token: 0x040012EA RID: 4842
		Meta,
		/// <summary>The HTML nobr element. </summary>
		// Token: 0x040012EB RID: 4843
		Nobr,
		/// <summary>The HTML noframes element. </summary>
		// Token: 0x040012EC RID: 4844
		Noframes,
		/// <summary>The HTML noscript element. </summary>
		// Token: 0x040012ED RID: 4845
		Noscript,
		/// <summary>The HTML object element. </summary>
		// Token: 0x040012EE RID: 4846
		Object,
		/// <summary>The HTML ol element. </summary>
		// Token: 0x040012EF RID: 4847
		Ol,
		/// <summary>The HTML option element. </summary>
		// Token: 0x040012F0 RID: 4848
		Option,
		/// <summary>The HTML p element. </summary>
		// Token: 0x040012F1 RID: 4849
		P,
		/// <summary>The HTML param element. </summary>
		// Token: 0x040012F2 RID: 4850
		Param,
		/// <summary>The HTML pre element. </summary>
		// Token: 0x040012F3 RID: 4851
		Pre,
		/// <summary>The HTML q element. </summary>
		// Token: 0x040012F4 RID: 4852
		Q,
		/// <summary>The DHTML rt element, which specifies text for the ruby element. </summary>
		// Token: 0x040012F5 RID: 4853
		Rt,
		/// <summary>The DHTML ruby element. </summary>
		// Token: 0x040012F6 RID: 4854
		Ruby,
		/// <summary>The HTML s element. </summary>
		// Token: 0x040012F7 RID: 4855
		S,
		/// <summary>The HTML samp element. </summary>
		// Token: 0x040012F8 RID: 4856
		Samp,
		/// <summary>The HTML script element. </summary>
		// Token: 0x040012F9 RID: 4857
		Script,
		/// <summary>The HTML select element. </summary>
		// Token: 0x040012FA RID: 4858
		Select,
		/// <summary>The HTML small element. </summary>
		// Token: 0x040012FB RID: 4859
		Small,
		/// <summary>The HTML span element. </summary>
		// Token: 0x040012FC RID: 4860
		Span,
		/// <summary>The HTML strike element. </summary>
		// Token: 0x040012FD RID: 4861
		Strike,
		/// <summary>The HTML strong element. </summary>
		// Token: 0x040012FE RID: 4862
		Strong,
		/// <summary>The HTML style element. </summary>
		// Token: 0x040012FF RID: 4863
		Style,
		/// <summary>The HTML sub element. </summary>
		// Token: 0x04001300 RID: 4864
		Sub,
		/// <summary>The HTML sup element. </summary>
		// Token: 0x04001301 RID: 4865
		Sup,
		/// <summary>The HTML table element. </summary>
		// Token: 0x04001302 RID: 4866
		Table,
		/// <summary>The HTML tbody element. </summary>
		// Token: 0x04001303 RID: 4867
		Tbody,
		/// <summary>The HTML td element. </summary>
		// Token: 0x04001304 RID: 4868
		Td,
		/// <summary>The HTML textarea element. </summary>
		// Token: 0x04001305 RID: 4869
		Textarea,
		/// <summary>The HTML tfoot element. </summary>
		// Token: 0x04001306 RID: 4870
		Tfoot,
		/// <summary>The HTML th element. </summary>
		// Token: 0x04001307 RID: 4871
		Th,
		/// <summary>The HTML thead element. </summary>
		// Token: 0x04001308 RID: 4872
		Thead,
		/// <summary>The HTML title element. </summary>
		// Token: 0x04001309 RID: 4873
		Title,
		/// <summary>The HTML tr element. </summary>
		// Token: 0x0400130A RID: 4874
		Tr,
		/// <summary>The HTML tt element. </summary>
		// Token: 0x0400130B RID: 4875
		Tt,
		/// <summary>The HTML u element. </summary>
		// Token: 0x0400130C RID: 4876
		U,
		/// <summary>The HTML ul element. </summary>
		// Token: 0x0400130D RID: 4877
		Ul,
		/// <summary>The HTML var element. </summary>
		// Token: 0x0400130E RID: 4878
		Var,
		/// <summary>The HTML wbr element. </summary>
		// Token: 0x0400130F RID: 4879
		Wbr,
		/// <summary>The HTML xml element. </summary>
		// Token: 0x04001310 RID: 4880
		Xml
	}
}
