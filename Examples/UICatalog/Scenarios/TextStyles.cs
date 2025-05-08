using Terminal.Gui;

namespace UICatalog.Scenarios;

[ScenarioMetadata ("Text Styles Demo", "Demos and tests the TextStyles of the Attributes class.")]
[ScenarioCategory ("Text and Formatting")]
public class TextStylesDemo : Scenario
{
    public override void Main ()
    {
        Application.Init ();

        var app = new Window
        {
            Title = GetQuitKeyAndName (),
        };

        var label = new Label { X = 0, Y = 0 };
        app.Add (label);

        var demoView = new TextStylesDemoView
        {
            Id = "demoView",
            X = 2,
            Y = Pos.Bottom (label) + 1,
            Width = Dim.Fill (),
            Height = Dim.Fill ()
        };

        app.Add (demoView);

        Application.Run (app);
        app.Dispose ();
        Application.Shutdown ();
    }
}

public class TextStylesDemoView : View
{
    public TextStylesDemoView ()
    {
        CanFocus = true;
        BorderStyle = LineStyle.Heavy;
        Arrangement = ViewArrangement.Resizable;
        Initialized += OnInitialized;
        HorizontalScrollBar.AutoShow = true;
        VerticalScrollBar.AutoShow = true;
    }

    private void OnInitialized (object sender, EventArgs e)
    {
        SetContentSize (new (80, 50));

        IReadOnlyCollection<TextStyle> textStylesList = Enum.GetValues (typeof (TextStyle))
            .Cast<TextStyle> ()
            .ToList ();

        List<int> displayedTextStylesList = [];
        int row = 1;

        // create labels that have the various Text Styles
        foreach (TextStyle textStyle in textStylesList)
        {
            Add (
                new Label
                {
                    Y = row,
                    X = Pos.Center (),
                    Text = $"{textStyle} Text Style",
                    ColorScheme = new ()
                    {
                        Normal = new (ColorScheme.Normal, textStyle),
                    }
                });

            // add to displayed
            displayedTextStylesList.Add ((int)textStyle);

            row++;
        }

        // Bold and Faint are mutually exclusive, add to the list so they are not displayed;
        displayedTextStylesList.Add ((int)(TextStyle.Bold | TextStyle.Faint));

        // create labels that combine two Text Styles 
        foreach (TextStyle textStyle in textStylesList)
        {
            foreach (TextStyle otherTextStyle in textStylesList)
            {
                // using or "|" to combine Text Styles
                TextStyle combinedTextStyle = textStyle | otherTextStyle;

                // If we have already displayed then skip
                if (displayedTextStylesList.Contains ((int)combinedTextStyle))
                {
                    continue;
                }

                Add (
                new Label
                {
                    Y = row,
                    X = Pos.Center (),
                    Text = $"{textStyle} Text Style Combined with {otherTextStyle}",
                    ColorScheme = new ()
                    {
                        Normal = new (ColorScheme.Normal, combinedTextStyle),
                    }
                });

                // add to displayed
                displayedTextStylesList.Add ((int)combinedTextStyle);

                row++;
            }
        }

        // Bold and Faint are mutually exclusive, add all the other TextStyle combinations to the list so they are not displayed;
        textStylesList
            .ToList()
            .ForEach (ts => displayedTextStylesList.Add ((int)(TextStyle.Bold | TextStyle.Faint | ts)));

        // create labels that combine three Text Styles 
        foreach (TextStyle firstTS in textStylesList)
        {
            foreach (TextStyle secondTS in textStylesList)
            {
                foreach (TextStyle thirdTS in textStylesList)
                {

                    // using or "|" to combine Text Styles
                    TextStyle combinedTextStyle = firstTS | secondTS | thirdTS;

                    // If we have already displayed then skip
                    if (displayedTextStylesList.Contains ((int)combinedTextStyle))
                    {
                        continue;
                    }

                    Add (
                    new Label
                    {
                        Y = row,
                        X = Pos.Center (),
                        Text = $"{firstTS} Text Style Combined with {secondTS} & {thirdTS}",
                        ColorScheme = new ()
                        {
                            Normal = new (ColorScheme.Normal, combinedTextStyle),
                        }
                    });

                    // add to displayed
                    displayedTextStylesList.Add ((int)combinedTextStyle);

                    row++;
                }
            }
        }
    }

    protected override bool OnMouseEvent (MouseEventArgs mouseEvent)
    {
        if (mouseEvent.Flags == MouseFlags.WheeledDown)
        {
            ScrollVertical (1);
            return mouseEvent.Handled = true;
        }

        if (mouseEvent.Flags == MouseFlags.WheeledUp)
        {
            ScrollVertical (-1);
            return mouseEvent.Handled = true;
        }

        if (mouseEvent.Flags == MouseFlags.WheeledRight)
        {
            ScrollHorizontal (1);
            return mouseEvent.Handled = true;
        }

        if (mouseEvent.Flags == MouseFlags.WheeledLeft)
        {
            ScrollHorizontal (-1);
            return mouseEvent.Handled = true;
        }

        return false;
    }
}
