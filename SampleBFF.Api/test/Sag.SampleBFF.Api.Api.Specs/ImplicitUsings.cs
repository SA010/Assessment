#pragma warning disable S1128 // Unused "using" should be removed
global using FluentAssertions;
global using Microsoft.Extensions.Logging;
global using Moq;
global using TechTalk.SpecFlow;
global using Xunit;
global using Xunit.Abstractions;
using System.Diagnostics.CodeAnalysis;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: SuppressMessage("Minor Code Smell", "S2325", Justification = "Static not required in test project")]
[assembly: SuppressMessage("Minor Code Smell", "S4261", Justification = "Async suffix not required in test project")]
