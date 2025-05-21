using System;
using RTB.BlazorUI.Services.Theme.Themes;

namespace RTB.PdfBuddy.Web.Theme;

public interface IBuddyTheme : ITheme
{
    string Primary { get; }
    string Secondary { get; }

    string Background { get; }

    string PrimaryText { get; }
    string TextOnPrimary { get; }
}
