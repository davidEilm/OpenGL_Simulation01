using System.Collections.Generic;

namespace ObjLoader
{
    public class ObjResult
    {
        public string objectName;

        public VertexPosition[] vertexPositions;
        public TextureCoordinate[] textureCoordinates;
        public NormalDirection[] normalDirections;
        public Face[] faces;

        private List<float> allVertices = new List<float>();
        private List<float> allNormals = new List<float>();
        private List<float> allTextureCoords = new List<float>();

        public float[] GetVertices()
        {
            foreach (Face f in faces)
            {
                foreach (FaceReferences fr in f.faceReferences)
                {
                    allVertices.Add(vertexPositions[fr.vertexPositionIndex - 1].x);
                    allVertices.Add(vertexPositions[fr.vertexPositionIndex - 1].y);
                    allVertices.Add(vertexPositions[fr.vertexPositionIndex - 1].z);
                }
            }
            return allVertices.ToArray();
        }
        public float[] GetNormals()
        {
            foreach (Face f in faces)
            {
                foreach (FaceReferences fr in f.faceReferences)
                {
                    if (fr.normalDirectionIndex == null)
                    {
                        System.Console.WriteLine("WARING: Face has no Normals");
                    }
                    allNormals.Add(normalDirections[(int)fr.normalDirectionIndex - 1].x);
                    allNormals.Add(normalDirections[(int)fr.normalDirectionIndex - 1].y);
                    allNormals.Add(normalDirections[(int)fr.normalDirectionIndex - 1].z);
                }
            }
            return allNormals.ToArray();
        }
        public float[] GetTextureCoords()
        {
            foreach (Face f in faces)
            {
                foreach (FaceReferences fr in f.faceReferences)
                {
                    if (fr.textureCoordinateIndex == null)
                    {
                        System.Console.WriteLine("WARING: Face has no TextureCoords");
                    }
                    allTextureCoords.Add(textureCoordinates[(int)fr.textureCoordinateIndex - 1].x);
                    allTextureCoords.Add(textureCoordinates[(int)fr.textureCoordinateIndex - 1].y);
                }
            }
            return allTextureCoords.ToArray();
        }
    }
}