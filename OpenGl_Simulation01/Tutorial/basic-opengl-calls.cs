sealed class GameWindow : OpenTK.GameWindow
{
    public GameWindow()
        // set window resolution, title, and default behaviour
        :base(1280, 720, GraphicsMode.Default, "OpenTK Intro",
        GameWindowFlags.Default, DisplayDevice.Default,
        // ask for an OpenGL 3.0 forward compatible context
        3, 0, GraphicsContextFlags.ForwardCompatible)
    {
        Console.WriteLine("gl version: " + GL.GetString(StringName.Version));
    }
 
    protected override void OnResize(EventArgs e)
    {
        GL.Viewport(0, 0, this.Width, this.Height);
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
        GL.ClearColor(Color4.Purple);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        this.SwapBuffers();
    }
}