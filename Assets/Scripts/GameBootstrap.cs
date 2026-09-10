using UnityEngine;

namespace Assets.Scripts
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private RainPool _rainPool;
        [SerializeField] private BombPool _bombsPool;
        [SerializeField] private SpawnedObjectsUI _rainUI;
        [SerializeField] private SpawnedObjectsUI _bombsUI;

        private void Awake()
        {
            _rainUI.Initialize(_rainPool);
            _bombsUI.Initialize(_bombsPool);
        }
    }
}
