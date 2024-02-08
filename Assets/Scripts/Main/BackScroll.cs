using UnityEngine;
using Cysharp.Threading.Tasks;

public class BackScroll : MonoBehaviour
{
    Renderer rend;

    [SerializeField] Vector2 speed;

    public async UniTask SetSpeedAsync(float time)
    {
        float t = 0f;
        while(t < time)
        {
            speed = new Vector2(0, t / 4f);
            t += Time.deltaTime;
            await UniTask.Yield();
        }
        speed = new Vector2(0, 0);
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * speed.x, 1);
        float y = Mathf.Repeat(Time.time * speed.y, 1);
        Vector2 offset = new Vector2(x, y);

        rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}