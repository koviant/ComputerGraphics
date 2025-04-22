using System;
using AppKit;
using CoreGraphics;

namespace ComputerGraphics.Mac.Views;

public class DebugView : NSView
{
    private readonly NSTextField _textField;
    private readonly NSSwitch _switch;
    
    private const int On = (int)NSCellStateValue.On;
    private const int Off = (int)NSCellStateValue.Off;

    public event Action OnSwitchStateChanged;

    public DebugView(CGRect frame) : base(frame)
    {
        WantsLayer = true;

        _textField = new NSTextField(new CGRect(0, Frame.Height - 30, 300, 24))
        {
            Editable = false,
            Bezeled = false,
            DrawsBackground = false,
            Selectable = false,
            Font = NSFont.SystemFontOfSize(18),
            TextColor = NSColor.Black,
        };

        var switchTitle = new NSTextField(new CGRect(0, Frame.Height - 60, 300, 24))
        {
            Editable = false,
            Bezeled = false,
            DrawsBackground = false,
            Selectable = false,
            Font = NSFont.SystemFontOfSize(18),
            TextColor = NSColor.Black,
            StringValue = "Change projection:"
        };

        _switch = new NSSwitch
        {
            State = On,
            Frame = new CGRect(0, Frame.Height - 90, 40, 24),
        };

        _switch.Activated += (_, _) => SwitchState = _switch.State == On;

        AddSubview(_textField);
        AddSubview(switchTitle);
        AddSubview(_switch);
    }

    public string OriginText
    {
        get => _textField.StringValue;
        set => _textField.StringValue = value;
    }

    public bool SwitchState
    {
        get => _switch.State == On;
        set
        {
            _switch.State = value ? On : Off;
            OnSwitchStateChanged?.Invoke();
        }
    }
}