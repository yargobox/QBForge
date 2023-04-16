using System;

namespace QBForge.Provider
{
    [Flags]
    public enum ReadabilityLevels
    {
        Default = 0,

        Low = AvoidAsKeyword | AvoidSpaces,
        Middle = LineBreaks | Indentation | AvoidQuotedLabels,
        High = LineBreaks | Indentation | AvoidQuotedLabels | AvoidQuotedIdentifiers,

        LineBreaks = 1,
        Indentation = 2,
        AvoidQuotedIdentifiers = 4,
        AvoidQuotedLabels = 8,
        AvoidAsKeyword = 16,
        AvoidSpaces = 32
    }
}