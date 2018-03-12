using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ObjLoader
{
    public static class ObjLoaderController
    {
        private static string currentLine;
        private static string[] splittedLine;
        private static List<VertexPosition> tempVertexPositions; //TODO: Maybe try out with linkedList
        private static List<TextureCoordinate> tempTextureCoordinates;
        private static List<NormalDirection> tempNormalDirections;
        private static List<Face> tempFaces;


        public static ObjResult Load(Stream fileStream)
        {
            ObjResult objResult = new ObjResult();
            StreamReader sr = new StreamReader(fileStream);
            tempVertexPositions = new List<VertexPosition>();
            tempTextureCoordinates = new List<TextureCoordinate>();
            tempNormalDirections = new List<NormalDirection>();
            tempFaces = new List<Face>();

            while (!sr.EndOfStream)
            {
                currentLine = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(currentLine) && currentLine[0] != '#')
                {
                    splittedLine = currentLine.Split(' ');

                    //Vertex
                    if (splittedLine[0] == "v")
                    {
                        tempVertexPositions.Add(new VertexPosition(
                        float.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture),
                        float.Parse(splittedLine[2], System.Globalization.CultureInfo.InvariantCulture),
                        float.Parse(splittedLine[3], System.Globalization.CultureInfo.InvariantCulture)));
                    }
                    //Texture
                    else if (splittedLine[0] == "vt")
                    {
                        tempTextureCoordinates.Add(new TextureCoordinate(
                        float.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture),
                        float.Parse(splittedLine[2], System.Globalization.CultureInfo.InvariantCulture)));
                    }
                    //Normal
                    else if (splittedLine[0] == "vn")
                    {
                        tempNormalDirections.Add(new NormalDirection(
                        float.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture),
                        float.Parse(splittedLine[2], System.Globalization.CultureInfo.InvariantCulture),
                        float.Parse(splittedLine[3], System.Globalization.CultureInfo.InvariantCulture)));
                    }
                    //Face
                    else if (splittedLine[0] == "f")
                    {
                        tempFaces.Add(new Face(splittedLine));
                    }
                    //Face
                    else if (splittedLine[0] == "o")
                    {
                        objResult.objectName = splittedLine[1];
                    }
                }
            }
            objResult.vertexPositions = tempVertexPositions.ToArray();
            objResult.textureCoordinates = tempTextureCoordinates.ToArray();
            objResult.normalDirections = tempNormalDirections.ToArray();
            objResult.faces = tempFaces.ToArray();

           

            //Only for testing purposes
            /*
            foreach (VertexPosition v in objResult.vertexPositions)
            {
                Console.Write("v ");
                Console.WriteLine(v.x + " " + v.y + " " + v.z);
            }
            foreach (TextureCoordinate t in objResult.textureCoordinates)
            {
                Console.Write("vt ");
                Console.WriteLine(t.x + " " + t.y);
            }
            foreach (NormalDirection n in objResult.normalDirections)
            {
                Console.Write("vn ");
                Console.WriteLine(n.x + " " + n.y + " " + n.z);
            }
            foreach (Face f in objResult.faces)
            {
                Console.Write("f ");
                Console.Write(f.faceReferences[0].vertexPositionIndex + "/");
                Console.Write(f.faceReferences[0].textureCoordinateIndex + "/");
                Console.Write(f.faceReferences[0].normalDirectionIndex + " ");
                Console.Write(f.faceReferences[1].vertexPositionIndex + "/");
                Console.Write(f.faceReferences[1].textureCoordinateIndex + "/");
                Console.Write(f.faceReferences[1].normalDirectionIndex + " ");
                Console.Write(f.faceReferences[2].vertexPositionIndex + "/");
                Console.Write(f.faceReferences[2].textureCoordinateIndex + "/");
                Console.Write(f.faceReferences[2].normalDirectionIndex + " ");
                Console.Write(f.faceReferences[3].vertexPositionIndex + "/");
                Console.Write(f.faceReferences[3].textureCoordinateIndex + "/");
                Console.Write(f.faceReferences[3].normalDirectionIndex);
                Console.WriteLine();
            }
            */
            return objResult;
        }
    }
}
