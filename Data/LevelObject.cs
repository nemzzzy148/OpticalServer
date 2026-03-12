namespace OpticalServer.Data
{
    public class LevelObject
    {
        public class LevelData
        {
            // level config
            public string? name;
            public float size = 1f;
            public float bgR = 0.09433959f, bgG = 0.09433959f, bgB = 0.09433959f, bgA = 1;

            //objects

            public List<Light>? lights;

            public List<Receiver>? receivers;

            public List<MeshObject>? meshObjects;

            public List<Text>? texts;
        }
        public class Text
        {
            public float size = 36f;
            public float r = 1, g = 1, b = 1, a = 1;
            public float x, y;
            public string text = "Lux";
        }
        public class Light
        {
            public float r = 1, g = 0.6117804f, b = 0, a = 0.0627451f;
            public float x, y;
        }
        public class Receiver
        {
            public bool light = true;
            public float x, y;
        }
        public class MeshObject
        {
            public bool mirror = false;
            public bool collision = true;
            public bool drawable = false;
            public float r = 0.490566f, g = 0.490566f, b = 0.490566f, a = 1;
            public List<float>? vertices;
            public List<int>? triangles;

        }
    }
}
