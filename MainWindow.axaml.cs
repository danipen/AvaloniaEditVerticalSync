using System;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using AvaloniaEdit;

namespace AvaloniaEditVerticalSync
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            mLeftEditor = new TextEditor();
            GridSplitter splitter = new GridSplitter();
            mRightEditor = new TextEditor();

            SetupEditor(mLeftEditor);
            SetupEditor(mRightEditor);

            mLeftEditor.TextArea.TextView.ScrollOffsetChanged += LeftScroll_OffsetChanged;
            mRightEditor.TextArea.TextView.ScrollOffsetChanged += RightScroll_OffsetChanged;

            StringBuilder leftContent = new StringBuilder();
            for (int i = 0; i < 300; i++)
            {
                leftContent.AppendLine("09 Oct 2021   cs:269   main   daniel.penalba@unity3d.com     true");
            }

            mLeftEditor.Text = leftContent.ToString();

            StringBuilder rightContent = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                rightContent.AppendLine("using System;");
                rightContent.AppendLine("using System.Collections;");
                rightContent.AppendLine("");
            }

            mRightEditor.Text = rightContent.ToString();

            Grid grid = new Grid();

            grid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(0.5, GridUnitType.Star)));
            grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
            grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

            Grid.SetColumn(mLeftEditor, 0);
            Grid.SetColumn(splitter, 1);
            Grid.SetColumn(mRightEditor, 2);

            grid.Children.Add(mLeftEditor);
            grid.Children.Add(splitter);
            grid.Children.Add(mRightEditor);

            this.Content = grid;
        }

        private void LeftScroll_OffsetChanged(object? sender, EventArgs e)
        {
            ILogicalScrollable scrollInfo = mRightEditor.TextArea.TextView;

            scrollInfo.Offset = new Vector(
                scrollInfo.Offset.X,
                mLeftEditor.TextArea.TextView.ScrollOffset.Y);
        }

        private void RightScroll_OffsetChanged(object? sender, EventArgs e)
        {
            ILogicalScrollable scrollInfo = mLeftEditor.TextArea.TextView;

            scrollInfo.Offset = new Vector(
                scrollInfo.Offset.X,
                mRightEditor.TextArea.TextView.ScrollOffset.Y);
        }

        private void SetupEditor(TextEditor editor)
        {
            editor.ShowLineNumbers = true;
            editor.VerticalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Hidden;
            editor.HorizontalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Hidden;
            editor.FontFamily = new Avalonia.Media.FontFamily("Consolas,.SF NS Mono,monospace");
            editor.FontSize = 12;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        TextEditor mLeftEditor;
        TextEditor mRightEditor;
    }
}