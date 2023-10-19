using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Views
{
    public class Bootstraper : MonoBehaviour
    {
        private void Awake()
        {
            //load setting and other data
            SceneManager.LoadScene(1);
        }
    }
}
