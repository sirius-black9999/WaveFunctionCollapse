using System.Numerics;

namespace VolcanoSim
{
    public enum GenerationPhase
    {
        Geology,
        Tectonics,
        Exposure,
        Airflow,
        Percipitation,
        Growth,
        Evolution,
        Erosion
    }

    public class VolcanoMapDef
    {
        private Vector2 MapSize;

        public VolcanoMapDef(int width, int height)
        {
            MapSize = new Vector2(width, height);
        }

        public Vector4 GetPixel(Vector2 pos) => new Vector4(pos.X / 4096f, pos.Y / 4096f, 0.5f, 1);
    }
}

//phases:
//1: volcanism/earthquakes/landslides (7?)
//2: sun exposure (1)
//3: wind direction (1)
//3.5: ash deposition
//4: moisture cycle (2,3)
//5: vegetation (4,2)
//6: creature niches/evolution (4,2,3)
//7: Mass wasting (5,4,3)
