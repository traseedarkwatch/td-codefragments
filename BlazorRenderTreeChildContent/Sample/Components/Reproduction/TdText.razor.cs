using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Sample.Components.Reproduction;

public partial class TdText
{
    /// <summary>
    /// The alignment of the text control
    /// </summary>
    [Parameter]
    public Align? Align { get; set; }

    /// <summary>
    /// The class to apply
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// The href if this is a link
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Show the text as a link
    /// </summary>
    [Parameter]
    public bool IsShowLink { get; set; }

    /// <summary>
    /// The typo for non-mobile versions
    /// </summary>
    [Parameter]
    public Typo Typo { get; set; } = Typo.body1;

    /// <summary>
    /// Show the text as a hyperlink
    /// </summary>
    private bool _isShowLink => IsShowLink && !string.IsNullOrWhiteSpace(Href);

    /// <summary>
    /// The mobile typo
    /// </summary>
    private Typo _typoMobile => Typo switch
    {
        Typo.h1 => Typo.h4,
        Typo.h2 => Typo.h5,
        Typo.h3 => Typo.h6,
        _ => Typo
    };

    /// <summary>
    /// Renders the text component
    /// </summary>
    /// <param name="typo">The typo to use</param>
    /// <returns>The component</returns>
    private RenderFragment RenderContent(Typo typo) => builder =>
    {
        if (Align.HasValue && _isShowLink)
        {
            builder.OpenComponent<MudText>(0);
            builder.AddAttribute(1, "Align", Align.Value);
            builder.AddContent(2, RenderTextOrComponent(typo));
            builder.CloseComponent();
        }
        else
        {
            builder.AddContent(1, RenderTextOrComponent(typo));
        }
    };

    /// <summary>
    /// Renders a text or link component
    /// </summary>
    /// <param name="typo">The typo to use</param>
    /// <returns>The component</returns>
    private RenderFragment RenderTextOrComponent(Typo typo) => builder =>
    {
        var componentType = _isShowLink ? typeof(MudLink) : typeof(MudText);
        builder.OpenComponent(0, componentType);

        if (!string.IsNullOrWhiteSpace(Class))
        {
            builder.AddAttribute(1, "Class", Class);
        }

        if (_isShowLink)
        {
            builder.AddAttribute(2, "Href", Href);
            builder.AddAttribute(3, "Typo", typo);
        }
        else
        {
            if (Align.HasValue)
            {
                builder.AddAttribute(2, "Align", Align);
            }

            builder.AddAttribute(3, "Typo", typo);
        }

        builder.AddContent(4, ChildContent);
        builder.CloseComponent();
    };
}
