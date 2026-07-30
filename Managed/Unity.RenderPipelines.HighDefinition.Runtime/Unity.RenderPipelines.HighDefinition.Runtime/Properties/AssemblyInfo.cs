using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

[assembly: AssemblyVersion("0.0.0.0")]
[assembly: InternalsVisibleTo("Unity.RenderPipelines.HighDefinition.Editor")]
[assembly: InternalsVisibleTo("Unity.RenderPipelines.HighDefinition.Editor.Tests")]
[assembly: InternalsVisibleTo("Unity.RenderPipelines.HighDefinition.Runtime.Tests")]
[assembly: InternalsVisibleTo("HDRP_TestRunner")]
[assembly: InternalsVisibleTo("Unity.RenderPipelines.HighDefinition-Tests.Runtime")]
[assembly: InternalsVisibleTo("Unity.RenderPipelines.HighDefinition-Tests.Editor")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
