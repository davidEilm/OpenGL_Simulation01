sealed class GameWindow : OpenTK.GameWindow
{
    public GameWindow()
        // set window resolution, title, and default behaviour
        :base(1280, 720, GraphicsMode.Default, "OpenTK Intro",
        GameWindowFlags.Default, DisplayDevice.Default,
        // ask for an OpenGL 3.0 forward compatible context
        3, 0, GraphicsContextFlags.ForwardCompatible)
    {
        
    }

    protected override void OnResize(EventArgs e)
    {
        // this is called when the window resizes
    }

    protected override void OnLoad(EventArgs e)
    {
        // this is called when the window starts running
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        // this is called every frame, put game logic here
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        // this is called every frame, put game rendering here
    }
}

class Program
{
    static void Main(string[] args)
    {
        // run game with 60 frames per second
        new GameWindow().Run(60);
    }
}