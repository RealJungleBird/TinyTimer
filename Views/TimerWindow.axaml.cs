using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using TinyTimer.ViewModels;

namespace TinyTimer;

public partial class TimerWindow : Window
{
    private readonly TimerWindowViewModel _viewModel;
    
    public TimerWindow()
    {
        InitializeComponent();
        _viewModel = new TimerWindowViewModel();
        this.DataContext = _viewModel;
        
        // Subscribe to PointerPressed events for all borders
        LeftBorder.PointerPressed += (s, e) => BeginResize(WindowEdge.West, e);
        RightBorder.PointerPressed += (s, e) => BeginResize(WindowEdge.East, e);
        TopBorder.PointerPressed += (s, e) => BeginResize(WindowEdge.North, e);
        BottomBorder.PointerPressed += (s, e) => BeginResize(WindowEdge.South, e);
        TopLeftCorner.PointerPressed += (s, e) => BeginResize(WindowEdge.NorthWest, e);
        TopRightCorner.PointerPressed += (s, e) => BeginResize(WindowEdge.NorthEast, e);
        BottomLeftCorner.PointerPressed += (s, e) => BeginResize(WindowEdge.SouthWest, e);
        BottomRightCorner.PointerPressed += (s, e) => BeginResize(WindowEdge.SouthEast, e);
        
        _viewModel.InitializeTimer();
        //_viewModel.StartTimer();
    }

    private void BeginResize(WindowEdge edge, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            BeginResizeDrag(edge, e);
            e.Handled = true;
        }
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        BeginMoveDrag(e);
    }

    protected override void OnPointerEntered(PointerEventArgs e)
    {
        base.OnPointerEntered(e);
        ResizeBorder.BorderBrush = Brushes.DodgerBlue;
    }
    
    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        ResizeBorder.BorderBrush = Brushes.Transparent;
    }
}