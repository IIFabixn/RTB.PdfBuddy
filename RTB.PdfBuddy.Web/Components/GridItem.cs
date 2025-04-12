using System;
using Microsoft.AspNetCore.Components;

namespace RTB.PdfBuddy.Web.Components;

public class GridItem : ComponentBase, IDisposable
{
    [CascadingParameter] public Grid Parent {get;set;} = default!;
    [Parameter] public int Column {get;set;} = 1;
    [Parameter] public int ColumnSpan {get;set;} = 1;
    [Parameter] public int Row {get;set;} = 1;
    [Parameter] public int RowSpan {get;set;} = 1;
    [Parameter] public RenderFragment ChildContent {get;set;} = default!;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object?> CapturesAttributes {get;set;} = new();

    public void Dispose()
    {
        Parent.UnregisterItem(this);
    }

    protected override void OnParametersSet()
    {
        Parent.RegisterItem(this);
    }
}
