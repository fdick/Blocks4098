using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Services
{
    public interface IPoolable
    {
        public void OnReturnToPool();
    }
}
