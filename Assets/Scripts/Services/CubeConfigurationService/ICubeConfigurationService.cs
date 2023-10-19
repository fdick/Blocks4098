using Code.Actors;

namespace Code.Services
{
    public interface ICubeConfigurationService
    {
        public void Configure(CubeView cube, int index);
    }
}