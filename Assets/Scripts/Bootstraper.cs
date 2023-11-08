using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Views
{
    public class Bootstraper : MonoBehaviour
    {
        private const string APP_KEY = "";
        private void Awake()
        {
            //init ironSource
            IronSource.Agent.validateIntegration();
            IronSource.Agent.init(APP_KEY);
            
            //load setting and other data
            SceneManager.LoadScene(1);
        }
    }
}
