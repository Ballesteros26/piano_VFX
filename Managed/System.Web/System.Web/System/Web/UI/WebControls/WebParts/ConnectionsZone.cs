using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides a user interface (UI) that enables users to form connections between <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> and other server controls that reside in <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zones. </summary>
	// Token: 0x020007A9 RID: 1961
	[Designer("System.Web.UI.Design.WebControls.WebParts.ConnectionsZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class ConnectionsZone : ToolZone
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> class. </summary>
		// Token: 0x06004EFF RID: 20223 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ConnectionsZone()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to cancel the process of establishing a connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to cancel the process of connecting two controls.</returns>
		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06004F00 RID: 20224 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb CancelVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to close the connection user interface (UI) created by the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that allows an end user to close the connection UI.</returns>
		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x06004F01 RID: 20225 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb CloseVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the text displayed as the title of a subsection of the connection user interface (UI) created by a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control.</summary>
		/// <returns>A string containing the title for the Configure Connections section. The default title is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x06004F02 RID: 20226 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F03 RID: 20227 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ConfigureConnectionTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object used to open the configuration view in the connection user interface (UI).</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> used to open the Configure Connections section in the connection UI.</returns>
		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x06004F04 RID: 20228 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb ConfigureVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the instructional text displayed in the section of the connection user interface (UI) where users select a consumer connection point that the provider will connect to.</summary>
		/// <returns>A string containing the instructions for creating a consumer connection to the provider. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x06004F05 RID: 20229 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F06 RID: 20230 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ConnectToConsumerInstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text of a hyperlink that users click to open a view in which they can choose a consumer control for a connection.</summary>
		/// <returns>A string displayed as the text of a hyperlink that opens the view where users select consumers. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x06004F07 RID: 20231 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F08 RID: 20232 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ConnectToConsumerText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the title text of the section in the connection user interface (UI) in which users can select a specific consumer to connect with.</summary>
		/// <returns>A string serving as the title of the section where users select consumers. The default title is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x06004F09 RID: 20233 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F0A RID: 20234 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ConnectToConsumerTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the instructional text displayed in the section of the connection user interface (UI) where users select a provider connection point that the consumer will connect to.</summary>
		/// <returns>A string containing the instructions on creating a provider connection to the consumer. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x06004F0B RID: 20235 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F0C RID: 20236 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ConnectToProviderInstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text of a hyperlink that users click to open a view in which they can choose a provider control for a connection.</summary>
		/// <returns>A string displayed as the text of a hyperlink that opens the view where users select providers. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x06004F0D RID: 20237 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F0E RID: 20238 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ConnectToProviderText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the title text of the section in the connection user interface (UI) in which users can select a specific provider to connect with.</summary>
		/// <returns>A string serving as the title of the section where users select providers. The default title is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x06004F0F RID: 20239 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F10 RID: 20240 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ConnectToProviderTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls to establish a connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables two controls to establish a connection.</returns>
		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x06004F11 RID: 20241 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb ConnectVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the instructional text shown in the consumers section of the connection user interface (UI), when a connection already exists.</summary>
		/// <returns>A string serving as the instructional text for consumers participating in a connection. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x06004F12 RID: 20242 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F13 RID: 20243 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ConsumersInstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the title shown above the consumers section of the connection user interface (UI), when a connection already exists.</summary>
		/// <returns>A string serving as the title text for consumers participating in a connection. The default title is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x06004F14 RID: 20244 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F15 RID: 20245 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ConsumersTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables a user to disconnect two connected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that disconnects two connected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</returns>
		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x06004F16 RID: 20246 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb DisconnectVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> is currently displayed; otherwise, false. The default value is false.</returns>
		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x000CB634 File Offset: 0x000C9834
		protected override bool Display
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets a text message that is displayed in an empty <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control if there are insufficient controls on a Web page to establish a connection. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the message for an empty zone. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F19 RID: 20249 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override string EmptyZoneText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text of a message displayed in the connection user interface (UI) when there is an error or warning on an existing connection.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the text of the message. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x06004F1A RID: 20250 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F1B RID: 20251 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ExistingConnectionErrorMessage
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text shown in the section of the connection user interface (UI) that precedes the named provider from which a consumer will retrieve data.</summary>
		/// <returns>A string that prefaces the named provider that the consumer will get data from. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F1D RID: 20253 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string GetFromText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text shown in the section of the connection user interface (UI) that precedes the named consumer that will receive data from a provider.</summary>
		/// <returns>A string that prefaces the named consumer in the connection. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x06004F1E RID: 20254 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F1F RID: 20255 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string GetText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the header text that appears at the top of the connection user interface (UI) created by the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control.</summary>
		/// <returns>A string that contains the header text for the connection UI. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x06004F20 RID: 20256 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F21 RID: 20257 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override string HeaderText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or set the text that is used for general instructions about the selected control in the section of the connection user interface (UI) for managing existing connections.</summary>
		/// <returns>A string that contains the instruction text for the connection UI. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x06004F22 RID: 20258 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F23 RID: 20259 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override string InstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text that is used for a general description of the action that can be performed on the consumer or provider control, within the connection user interface (UI) for managing existing connections.</summary>
		/// <returns>A string that contains the instruction title for the connection UI. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x06004F24 RID: 20260 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F25 RID: 20261 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string InstructionTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text of a message displayed in the connection user interface (UI) when there is an error or warning on a new connection that a user tries to create.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the text of the message. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x06004F26 RID: 20262 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F27 RID: 20263 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string NewConnectionErrorMessage
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the instruction text that appears in the body of the connection user interface (UI) when a Web Parts control has no existing connection.</summary>
		/// <returns>A string that contains the text message for the case where there is no existing connection. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x06004F28 RID: 20264 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F29 RID: 20265 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string NoExistingConnectionInstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the title text that appears in the body of the connection user interface (UI) when a Web Parts control has no existing connection.</summary>
		/// <returns>A string that contains the title text for the case where there is no existing connection. The default text is a culture-specific string supplied by the .NET Framework. </returns>
		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x06004F2A RID: 20266 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F2B RID: 20267 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string NoExistingConnectionTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the type of border that frames the server controls contained in a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> that determines the type of border that frames the controls contained in the connections zone.</returns>
		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x06004F2C RID: 20268 RVA: 0x000CB650 File Offset: 0x000C9850
		// (set) Token: 0x06004F2D RID: 20269 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override PartChromeType PartChromeType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return PartChromeType.Default;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the instructional text shown in the providers section of the connection user interface (UI), when a connection already exists.</summary>
		/// <returns>A string serving as the instructional text for providers participating in a connection. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x06004F2E RID: 20270 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F2F RID: 20271 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ProvidersInstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the title shown above the providers section of the connection user interface (UI), when a connection already exists.</summary>
		/// <returns>A string serving as the title text for providers participating in a connection. The default title is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x06004F30 RID: 20272 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F31 RID: 20273 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ProvidersTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text shown in the section of the connection user interface (UI) that precedes the named provider that will send data to a consumer.</summary>
		/// <returns>A string that prefaces the named provider in the connection. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x06004F32 RID: 20274 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F33 RID: 20275 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string SendText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text shown in the section of the connection user interface (UI) that precedes the named consumer to which a provider will send data.</summary>
		/// <returns>A string that prefaces the named consumer that the provider will send data to. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x06004F34 RID: 20276 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F35 RID: 20277 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string SendToText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the currently selected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control to connect to.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</returns>
		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x06004F36 RID: 20278 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected WebPart WebPartToConnect
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Closes the connection user interface (UI) created by the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control.</summary>
		// Token: 0x06004F37 RID: 20279 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void Close()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.DisplayModeChanged" /> event.</summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeEventArgs" /> that contains the event data.</param>
		// Token: 0x06004F38 RID: 20280 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void OnDisplayModeChanged(object sender, WebPartDisplayModeEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> event.</summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that identifies the sender of the event.</param>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventArgs" /> that contains the event data.</param>
		// Token: 0x06004F39 RID: 20281 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void OnSelectedWebPartChanged(object sender, WebPartEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises an event for the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control when the form that contains it posts back to the server. </summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that contains the argument data for the event.</param>
		// Token: 0x06004F3A RID: 20282 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void RaisePostBackEvent(string eventArgument)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sends the content of a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control's body area to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to the Web page.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the control's body content.</param>
		// Token: 0x06004F3B RID: 20283 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void RenderBody(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders the zone-level verbs for a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the content of the verbs to render in a connections zone.</param>
		// Token: 0x06004F3C RID: 20284 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void RenderVerbs(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
