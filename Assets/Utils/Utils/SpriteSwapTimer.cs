using UnityEngine;

public class SpriteSwapTimer : MonoBehaviour
{
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] float timerSeconds;

    private float _timer;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > timerSeconds)
        {
            _timer = 0;
            SwapSprite();
        }
    }

    void SwapSprite()
    {
        if(spriteRenderer == null)
            return;
        if(spriteRenderer.sprite == sprite1)
        {
            spriteRenderer.sprite = sprite2;
            return;
        }
        spriteRenderer.sprite = sprite1;
    }


}
