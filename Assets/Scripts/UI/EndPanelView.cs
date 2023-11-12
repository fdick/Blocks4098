using UnityEngine;
using UnityEngine.UI;

namespace Code.Views
{
    public class EndPanelView : MonoBehaviour
    {
       [field: SerializeField] public Button RestartBtn { get; private set; }
       [field: SerializeField] public Button ContinueBtn { get; private set; }
    }
}
