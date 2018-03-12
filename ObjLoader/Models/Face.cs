using System;
using System.Collections.Generic;

namespace ObjLoader
{
    public class Face
    {
        public FaceReferences[] faceReferences;

        public Face(string[] splittedLine)
        {
            if (splittedLine.Length == 4) //Triangle
            {
                faceReferences = CreateFaceReference(splittedLine, 3);
            }
            else if (splittedLine.Length == 5) //Quad
            {
                faceReferences = CreateFaceReference(splittedLine, 4);
            }
            else
            {
                Console.WriteLine("ERROR: Only Triangles and Quads are supported");
            }
        }

        private FaceReferences[] CreateFaceReference(string[] splittedLine, int countOfPacks)
        {
            faceReferences = new FaceReferences[countOfPacks];
            for (int i = 0; i < countOfPacks; i++)
            {
                faceReferences[i] = new FaceReferences();
                var splittedPack = splittedLine[i + 1].Split('/');

                faceReferences[i].vertexPositionIndex = Convert.ToInt32(splittedPack[0]);
                if (splittedPack.Length == 3)
                {
                    faceReferences[i].vertexPositionIndex = Convert.ToInt32(splittedPack[0]);
                    faceReferences[i].textureCoordinateIndex = Convert.ToInt32(splittedPack[1]);
                    faceReferences[i].normalDirectionIndex = Convert.ToInt32(splittedPack[2]);
                }
                else if (splittedPack.Length == 2)
                {
                    if (splittedPack[i].Contains("//"))
                    {
                        faceReferences[i].normalDirectionIndex = Convert.ToInt32(splittedPack[1]);
                    }
                    else if (splittedPack[i].Contains("/"))
                    {
                        faceReferences[i].textureCoordinateIndex = Convert.ToInt32(splittedPack[1]);
                    }
                }
            }
            return faceReferences;
        }
    }

    public class FaceReferences
    {
        public int vertexPositionIndex;
        public int? textureCoordinateIndex = null;
        public int? normalDirectionIndex = null;
    }
}