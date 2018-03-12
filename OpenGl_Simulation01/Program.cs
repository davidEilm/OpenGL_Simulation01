using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using ObjLoader.Loader.Loaders;
using System.IO;
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.Data.Elements;
using System.Runtime.InteropServices;
using ObjLoader;
using System.Diagnostics;

namespace OpenGl_Simulation01
{
    class Program
    {
        private static GameWindow window;
        private static int HEIGHT = 720;
        private static int WIDTH = 1280;

        private static FileStream fileStream;
        private static ObjResult objResult;

        private static float[] allVertices;
        private static float[] allNormals;
        private static float[] allTextureCoords;

        private static int VBO;
        private static int VAO;
        private static int normalBuffer;
        private static int textureBuffer;

        private static float rotationAngle = 0;

        static void Main(string[] args)
        {
            window = new GameWindow(WIDTH, HEIGHT, GraphicsMode.Default, "Simulation01", GameWindowFlags.FixedWindow);
            Start(window);
        }
        private static void Start(GameWindow window)
        {
            // ### EventBindings
            window.Load += Window_Load;
            window.UpdateFrame += Window_UpdateFrame;
            window.RenderFrame += Window_RenderFrame;

            window.Run();
        }

        private static void Window_Load(object sender, EventArgs e)
        {
            // ### Basic Initialisations
            Console.WriteLine("Loaded Window");
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Enable(EnableCap.DepthTest);

            // ### Create Viewport and Perspective
            GL.Viewport(0, 0, WIDTH, HEIGHT);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 m4 = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)WIDTH / HEIGHT, 1.0f, 1000.0f); //The (float) is very important!!!
            GL.LoadMatrix(ref m4);
            GL.MatrixMode(MatrixMode.Modelview);

            // ### Lighting
            //GL.Enable(EnableCap.Lighting);
            //GL.Light(LightName.Light0, LightParameter.Position, new Vector4(0.0f, 4.0f, -2f, 1));
            //GL.Light(LightName.Light0, LightParameter.SpotExponent, 0.01f);
            //GL.Enable(EnableCap.Light0);

            // ### Load OBJ
            fileStream = new FileStream("ViolettBerry05.obj", FileMode.Open);
            objResult = ObjLoaderController.Load(fileStream);
            fileStream.Close();
            allVertices = objResult.GetVertices();
            allNormals = objResult.GetNormals();
            allTextureCoords = objResult.GetTextureCoords();
            Console.WriteLine("Count of Faces: " + objResult.faces.Length);
            Console.WriteLine("Count of Face-Vertices: " + allVertices.Length);
            Console.WriteLine("Count of Face-Normals: " + allNormals.Length);
            Console.WriteLine("Count of Face-TextureCoords: " + allNormals.Length);

            GL.EnableClientState(ArrayCap.VertexArray);

            //Set up Vertex-Array-Object (VAO)
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            //Set up Vertex Buffer (Position Buffer)
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Marshal.SizeOf<float>() * allVertices.Length), allVertices, BufferUsageHint.StaticDraw);
            //Add to VAO
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

            //Set up Normal Buffer
            normalBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, normalBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Marshal.SizeOf<float>() * allNormals.Length), allNormals, BufferUsageHint.StaticDraw);
            //Add to VAO
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);

            //Set up Texture Buffer
            normalBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, normalBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Marshal.SizeOf<float>() * allNormals.Length), allNormals, BufferUsageHint.StaticDraw);
            //Add to VAO
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindVertexArray(0); //Damit sichern wir, dass sich nachfolgende Aufrufe von glVertexAttribPointer oder glBindBuffer nicht auf unser VAO auswirken.
            //...
        }

        private static void Window_UpdateFrame(object sender, FrameEventArgs e)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // ### InitialTransformations
            GL.Translate(0.0f, -0.375f, -10.0f);
            GL.Rotate(rotationAngle, 0.0f, 1.0f, 0.0f);

            rotationAngle += 1;
            if (rotationAngle > 360) //Clamp
            {
                rotationAngle -= 360;
            }
            //rotationAngle = rotationAngle * (float)e.Time * 50;
        }

        private static void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.BindVertexArray(VAO);
            GL.DrawArrays(PrimitiveType.Points, 0, allVertices.Length);
            GL.BindVertexArray(0);

            window.SwapBuffers();
        }
    }
}
