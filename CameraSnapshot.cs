using System.IO;
using UnityEngine;
using UnityEditor;

public static class CameraSnapshot
{
    public enum BitDepth
    {
        ZERO = 0,
        SIXTEEN = 16,
        TWENTY_FOUR = 24,
        THIRTY_TWO = 32
    }

    public static Camera camera;
    public static int width;
    public static int height;
    public static BitDepth depth;
    public static RenderTextureFormat renderTexFormat;
    public static TextureFormat texFormat;
    public static string filepath;
    public static string filename;

    public static void Snap()
    {
        camera.targetTexture = new RenderTexture(width, height, (int)depth, renderTexFormat);

        camera.Render();

        Texture2D texture = new Texture2D(width, height, texFormat, false);
        RenderTexture.active = camera.targetTexture;
        texture.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        texture.Apply();

        RenderTexture.active = null;
        camera.targetTexture = null;

        File.WriteAllBytes(Path.Combine(filepath, $"{filename}.png"), texture.EncodeToPNG());

        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }
}
