using UnityEngine;

namespace Mirin
{
    public class BlackPlatePool : PoolBase<Component>
    {
        public Component GetPlate()
        {
            Vector2 randScale = new Vector2(
                Random.Range(2f, 12f), Random.Range(0.2f, 1.2f));
            var plate = GetInstance();
            plate.transform.localScale = randScale;
            return plate;
        }
    }
}