using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Mouse_DOPDOP : MonoBehaviour
{
    public static Cursor_Mouse_DOPDOP Instance { get; private set; }

    [SerializeField] private Sprite normalCursorSprite;
    [SerializeField] private Sprite clickCursorSprite;
    [SerializeField] private Vector2Int cursorSize = new Vector2Int(32, 32);
    private Texture2D normalCursorTexture;
    private Texture2D clickCursorTexture;
    private Vector2 cursorHotspot;

    private void Awake()
    {
        // Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    void Start()
    {
        normalCursorTexture = ResizeTexture(normalCursorSprite.texture, cursorSize.x, cursorSize.y);
        clickCursorTexture = ResizeTexture(clickCursorSprite.texture, cursorSize.x, cursorSize.y);
        cursorHotspot = new Vector2(normalCursorTexture.width / 2, normalCursorTexture.height / 2);

        UnityEngine.Cursor.SetCursor(normalCursorTexture, cursorHotspot, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Cursor.SetCursor(clickCursorTexture, cursorHotspot, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            UnityEngine.Cursor.SetCursor(normalCursorTexture, cursorHotspot, CursorMode.Auto);
        }
    }

    private void OnDisable()
    {
        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // ฟังก์ชันสำหรับปรับขนาด Texture2D
    private Texture2D ResizeTexture(Texture2D source, int newWidth, int newHeight)
    {
        RenderTexture rt = RenderTexture.GetTemporary(
            newWidth,
            newHeight,
            0,
            RenderTextureFormat.ARGB32,
            RenderTextureReadWrite.Default
        );

        Graphics.Blit(source, rt);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D result = new Texture2D(newWidth, newHeight);
        result.ReadPixels(new Rect(0, 0, newWidth, newHeight), 0, 0);
        result.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        return result;
    }
}
