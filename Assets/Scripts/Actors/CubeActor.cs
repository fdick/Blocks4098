using Code.Configs;

namespace Code.Actors
{
    public class CubeActor
    {
        public CubeActor(CubeConfiguration configuration)
        {
            Configuration = configuration;
        }

        public CubeConfiguration Configuration { get; }
        
    }
}