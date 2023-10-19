using UnityEngine;

namespace Code.Factories
{
    public interface IBasketFactory
    {
        public BasketView CreateMap();
    }
}