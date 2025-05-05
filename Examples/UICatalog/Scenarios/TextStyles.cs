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

        int row = 1;
        // create labels that have the various Text Styles
        foreach (TextStyle textStyle in Enum.GetValues (typeof (TextStyle)))
        {
            app.Add (
                new Label
                {
                    Y = row,
                    X = Pos.Center (),
                    Text = $"{textStyle} Text Style",
                    ColorScheme = new ()
                    {
                        Normal = new (app.ColorScheme.Normal, textStyle),
                    }
                });

            row++;
        }

        // create labels that combine two Text Styles 
        foreach (TextStyle textStyle in Enum.GetValues (typeof (TextStyle)))
        {
            if (textStyle == TextStyle.None)
            {
                // skip because we displayed in the first loop
                continue;
            }

            foreach (TextStyle otherTextStyle in Enum.GetValues (typeof (TextStyle)))
            {
                if (textStyle == otherTextStyle || otherTextStyle == TextStyle.None)
                {
                    // skip because combining none is the same as the first loop
                    continue;
                }

                app.Add (
                new Label
                {
                    Y = row,
                    X = Pos.Center (),
                    Text = $"{textStyle} Text Style Combined with {otherTextStyle}",
                    ColorScheme = new ()
                    {
                        // using or "|" to combine Text Styles
                        Normal = new (app.ColorScheme.Normal, textStyle | otherTextStyle),
                    }
                });

                row++;
            }
        }

        Application.Run (app);
        app.Dispose ();
        Application.Shutdown ();
    }
}
