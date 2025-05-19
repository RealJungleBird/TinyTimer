using System;
using System.Reflection.Metadata;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.VisualTree;
using TinyTimer.ViewModels;

namespace TinyTimer;

public partial class TimerWindow : Window
{
    private bool _isResizing;
    private Point _dragStartPosition;
    private WindowEdge _activeEdge = WindowEdge.None;
    private PixelRect _initialBounds;
    
    public TimerWindow()
    {
        InitializeComponent();
        var viewmodel = new TimerWindowViewModel();
        this.DataContext = viewmodel;
        
        MinWidth = 100;
        MinHeight = 50;
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);
        if (!_isResizing)
        {
            var position = e.GetPosition(this);
            _activeEdge = GetActiveEdge(position);
            UpdateCursor(_activeEdge);
        }
        else if (_isResizing)
        {
            HandleResize(e);
        }
    }

    // private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    // {
    //     BeginMoveDrag(e);
    // }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        var position = e.GetPosition(this);
        _activeEdge = GetActiveEdge(position);

        if (_activeEdge != WindowEdge.None)
        {
            _isResizing = true;
            _dragStartPosition = position;
            _initialBounds = new PixelRect(
                (int)Position.X,
                (int)Position.Y,
                (int)Bounds.Width,
                (int)Bounds.Height
            );
        }
        else
        {
            BeginMoveDrag(e);
        }
    }

    private void HandleResize(PointerEventArgs e)
    {
        var currentPosition = e.GetPosition(this);
        var delta = currentPosition - _dragStartPosition;

        // HighDPI support
        var scaling = this.GetVisualRoot()?.RenderScaling ?? 1.0;
        var scaledDelta = /*scaling * */delta;
        
        double newWidth = _initialBounds.Width;
        double newHeight = _initialBounds.Height;
        double newX = _initialBounds.X;
        double newY = _initialBounds.Y;

        switch (_activeEdge)
        {
            case WindowEdge.West:   // Left
                newWidth = Math.Max(MinWidth, _initialBounds.Width - scaledDelta.X);
                newX = _initialBounds.X + (_initialBounds.Width - newWidth);
                break;
            case WindowEdge.East:   // Right
                newWidth = Math.Max(MinWidth, _initialBounds.Width + scaledDelta.X);
                break;
            case WindowEdge.North:  // Top
                newHeight = Math.Max(MinHeight, _initialBounds.Height - scaledDelta.Y);
                newY = _initialBounds.Y + (_initialBounds.Height - newHeight);
                break;
            case WindowEdge.South:  // Bottom
                newHeight = Math.Max(MinHeight, _initialBounds.Height + scaledDelta.Y);
                break;
            
            case WindowEdge.NorthWest:  // Top-Left
                newWidth = Math.Max(MinWidth, _initialBounds.Width - scaledDelta.X);
                newX = _initialBounds.X + (_initialBounds.Width - newWidth);
                newHeight = Math.Max(MinHeight, _initialBounds.Height - scaledDelta.Y);
                newY = _initialBounds.Y + (_initialBounds.Height - newHeight);
                break;
            case WindowEdge.NorthEast:  // Top-Right
                newWidth = Math.Max(MinWidth, _initialBounds.Width + scaledDelta.X);
                newHeight = Math.Max(MinHeight, _initialBounds.Height - scaledDelta.Y);
                newY = _initialBounds.Y + (_initialBounds.Height - newHeight);
                break;
            case WindowEdge.SouthWest:  // Bottom-Left
                newWidth = Math.Max(MinWidth, _initialBounds.Width - scaledDelta.X);
                newX = _initialBounds.X + (_initialBounds.Width - newWidth);
                newHeight = Math.Max(MinHeight, _initialBounds.Height - scaledDelta.Y);
                break;
            case WindowEdge.SouthEast:  // Bottom-Right
                newWidth = Math.Max(MinWidth, _initialBounds.Width + scaledDelta.X);
                newHeight = Math.Max(MinHeight, _initialBounds.Height + scaledDelta.Y);
                break;
        }
        
        this.BeginInit();
        try
        {
            // Apply changes
            var newPosition = new PixelPoint((int)newX, (int)newY);
            if (newPosition != Position)
            {
                Position = newPosition;
            }

            if (Math.Abs(Width - newWidth) > 0.1)
            {
                Width = newWidth;
            }

            if (Math.Abs(Height - newHeight) > 0.1)
            {
                Height = newHeight;
            }
        }
        finally
        {
            this.EndInit();
        }
        
        // Position = new PixelPoint((int)newX, (int)newY);
        // Width = newWidth;
        // Height = newHeight;
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        _isResizing = false;
        _activeEdge = WindowEdge.None;
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
    
    private enum WindowEdge
    {
        None,
        West,
        East,
        North,
        South,
        NorthWest,
        NorthEast,
        SouthWest,
        SouthEast
    }
    private const int EdgeSize = 5;
    
    private WindowEdge GetActiveEdge(Point position)
    {
        var isLeft = position.X <= EdgeSize;
        var isRight = position.X >= Bounds.Width - EdgeSize;
        var isTop = position.Y <= EdgeSize;
        var isBottom = position.Y >= Bounds.Height - EdgeSize;

        return (isLeft, isRight, isTop, isBottom) switch
        {
            (true, false, true, false) => WindowEdge.NorthWest,
            (false, true, true, false) => WindowEdge.NorthEast,
            (true, false, false, true) => WindowEdge.SouthWest,
            (false, true, false, true) => WindowEdge.SouthEast,
            (true, _, _, _) => WindowEdge.West,
            (_, true, _, _) => WindowEdge.East,
            (_, _, true, _) => WindowEdge.North,
            (_, _, _, true) => WindowEdge.South,
            _ => WindowEdge.None
        };
    }

    private void UpdateCursor(WindowEdge edge)
    {
        Cursor = edge switch
        {
            WindowEdge.West or WindowEdge.East => new Cursor(StandardCursorType.SizeWestEast),
            WindowEdge.North or WindowEdge.South => new Cursor(StandardCursorType.SizeNorthSouth),
            WindowEdge.NorthWest or WindowEdge.SouthEast => new Cursor(StandardCursorType.TopLeftCorner),
            WindowEdge.NorthEast or WindowEdge.SouthWest => new Cursor(StandardCursorType.TopRightCorner),
            _ => Cursor.Default
        };
    }
}