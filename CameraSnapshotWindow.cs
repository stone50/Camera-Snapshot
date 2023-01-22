using System.IO;
using UnityEngine;
using UnityEditor;

public class CameraSnapshotWindow : EditorWindow
{
    public Camera camera = null;
    public int width = 1920;
    public int height = 1080;
    public CameraSnapshot.BitDepth depth = CameraSnapshot.BitDepth.ZERO;
    public RenderTextureFormat renderTexFormat = RenderTextureFormat.ARGB32;
    public TextureFormat texFormat = TextureFormat.ARGB32;
    public string filepath = "";
    public string filename = "Snapshot";

    [MenuItem("Tools/Camera Snapshot")]
    static void Init()
    {
        CameraSnapshotWindow window = EditorWindow.GetWindow<CameraSnapshotWindow>();
        window.minSize = new Vector2(300f, 500f);
        window.Show();
    }

    void OnEnable()
    {
        filepath = Application.dataPath;
    }

    void OnGUI()
    {
        camera = EditorGUILayout.ObjectField("Camera", camera, typeof(Camera), true) as Camera;
        EditorGUILayout.Separator();
        width = Mathf.Clamp(EditorGUILayout.IntField("Width", width), 0, SystemInfo.maxTextureSize);
        height = Mathf.Clamp(EditorGUILayout.IntField("Height", height), 0, SystemInfo.maxTextureSize);
        depth = (CameraSnapshot.BitDepth)EditorGUILayout.EnumPopup("Depth", depth);
        EditorGUILayout.Separator();
        renderTexFormat = (RenderTextureFormat)EditorGUILayout.EnumPopup("Render Texture Format", renderTexFormat);
        texFormat = (TextureFormat)EditorGUILayout.EnumPopup("Texture Format", texFormat);
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Location");
        string originalFilepath = filepath;
        filepath = EditorGUILayout.TextField(filepath);
        if (GUILayout.Button("Choose"))
        {
            filepath = EditorUtility.OpenFolderPanel("Snapshot Location", filepath, "");
        }
        if (!Directory.Exists(filepath))
        {
            filepath = originalFilepath;
        }
        EditorGUILayout.Separator();
        filename = EditorGUILayout.TextField("Name", filename);
        EditorGUILayout.Separator();
        if (GUILayout.Button("Snap"))
        {
            CameraSnapshot.camera = camera;
            CameraSnapshot.width = width;
            CameraSnapshot.height = height;
            CameraSnapshot.depth = depth;
            CameraSnapshot.renderTexFormat = renderTexFormat;
            CameraSnapshot.texFormat = texFormat;
            CameraSnapshot.filepath = filepath;
            CameraSnapshot.filename = filename;
            CameraSnapshot.Snap();
        }
    }
}