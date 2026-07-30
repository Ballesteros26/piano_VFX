using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies that the type has a visualizer. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A6A RID: 2666
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	public sealed class DebuggerVisualizerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type name of the visualizer.</summary>
		/// <param name="visualizerTypeName">The fully qualified type name of the visualizer.</param>
		// Token: 0x06006183 RID: 24963 RVA: 0x001400DF File Offset: 0x0013E2DF
		public DebuggerVisualizerAttribute(string visualizerTypeName)
		{
			this.visualizerName = visualizerTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type name of the visualizer and the type name of the visualizer object source.</summary>
		/// <param name="visualizerTypeName">The fully qualified type name of the visualizer.</param>
		/// <param name="visualizerObjectSourceTypeName">The fully qualified type name of the visualizer object source.</param>
		// Token: 0x06006184 RID: 24964 RVA: 0x001400EE File Offset: 0x0013E2EE
		public DebuggerVisualizerAttribute(string visualizerTypeName, string visualizerObjectSourceTypeName)
		{
			this.visualizerName = visualizerTypeName;
			this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type name of the visualizer and the type of the visualizer object source.</summary>
		/// <param name="visualizerTypeName">The fully qualified type name of the visualizer.</param>
		/// <param name="visualizerObjectSource">The type of the visualizer object source.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="visualizerObjectSource" /> is null.</exception>
		// Token: 0x06006185 RID: 24965 RVA: 0x00140104 File Offset: 0x0013E304
		public DebuggerVisualizerAttribute(string visualizerTypeName, Type visualizerObjectSource)
		{
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.visualizerName = visualizerTypeName;
			this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type of the visualizer.</summary>
		/// <param name="visualizer">The type of the visualizer.</param>
		/// <exception cref="T:System.ArgumentNullException">v<paramref name="isualizer" /> is null.</exception>
		// Token: 0x06006186 RID: 24966 RVA: 0x00140133 File Offset: 0x0013E333
		public DebuggerVisualizerAttribute(Type visualizer)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type of the visualizer and the type of the visualizer object source.</summary>
		/// <param name="visualizer">The type of the visualizer.</param>
		/// <param name="visualizerObjectSource">The type of the visualizer object source.</param>
		/// <exception cref="T:System.ArgumentNullException">v<paramref name="isualizer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="visualizerObjectSource" /> is null.</exception>
		// Token: 0x06006187 RID: 24967 RVA: 0x0014015C File Offset: 0x0013E35C
		public DebuggerVisualizerAttribute(Type visualizer, Type visualizerObjectSource)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
			this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type of the visualizer and the type name of the visualizer object source.</summary>
		/// <param name="visualizer">The type of the visualizer.</param>
		/// <param name="visualizerObjectSourceTypeName">The fully qualified type name of the visualizer object source.</param>
		/// <exception cref="T:System.ArgumentNullException">v<paramref name="isualizer" /> is null.</exception>
		// Token: 0x06006188 RID: 24968 RVA: 0x001401AF File Offset: 0x0013E3AF
		public DebuggerVisualizerAttribute(Type visualizer, string visualizerObjectSourceTypeName)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
			this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
		}

		/// <summary>Gets the fully qualified type name of the visualizer object source.</summary>
		/// <returns>The fully qualified type name of the visualizer object source.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06006189 RID: 24969 RVA: 0x001401DE File Offset: 0x0013E3DE
		public string VisualizerObjectSourceTypeName
		{
			get
			{
				return this.visualizerObjectSourceName;
			}
		}

		/// <summary>Gets the fully qualified type name of the visualizer.</summary>
		/// <returns>The fully qualified visualizer type name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x0600618A RID: 24970 RVA: 0x001401E6 File Offset: 0x0013E3E6
		public string VisualizerTypeName
		{
			get
			{
				return this.visualizerName;
			}
		}

		/// <summary>Gets or sets the description of the visualizer.</summary>
		/// <returns>The description of the visualizer.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x0600618B RID: 24971 RVA: 0x001401EE File Offset: 0x0013E3EE
		// (set) Token: 0x0600618C RID: 24972 RVA: 0x001401F6 File Offset: 0x0013E3F6
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets or sets the target type when the attribute is applied at the assembly level.</summary>
		/// <returns>The type that is the target of the visualizer.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value cannot be set because it is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x0600618E RID: 24974 RVA: 0x00140228 File Offset: 0x0013E428
		// (set) Token: 0x0600618D RID: 24973 RVA: 0x001401FF File Offset: 0x0013E3FF
		public Type Target
		{
			get
			{
				return this.target;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.targetName = value.AssemblyQualifiedName;
				this.target = value;
			}
		}

		/// <summary>Gets or sets the fully qualified type name when the attribute is applied at the assembly level.</summary>
		/// <returns>The fully qualified type name of the target type.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06006190 RID: 24976 RVA: 0x00140239 File Offset: 0x0013E439
		// (set) Token: 0x0600618F RID: 24975 RVA: 0x00140230 File Offset: 0x0013E430
		public string TargetTypeName
		{
			get
			{
				return this.targetName;
			}
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x040030B7 RID: 12471
		private string visualizerObjectSourceName;

		// Token: 0x040030B8 RID: 12472
		private string visualizerName;

		// Token: 0x040030B9 RID: 12473
		private string description;

		// Token: 0x040030BA RID: 12474
		private string targetName;

		// Token: 0x040030BB RID: 12475
		private Type target;
	}
}
