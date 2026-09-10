using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public interface IObservablePool
    {
        event Action<int> OnCountOfCreatedObjectsChanged;
        event Action<int> OnCountOfActiveObjectsChanged;
        event Action<int> OnTotalSpawnedObjectsCountChanged;

        int CountOfCreatedObjects { get; }
        int CountOfActiveObjects { get; }
        int TotalSpawnedObjectsCount { get; }
    }
}
