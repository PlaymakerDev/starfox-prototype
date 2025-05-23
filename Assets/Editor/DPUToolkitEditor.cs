using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
public class DPUToolkitEditor : EditorWindow
{
    private Texture2D banner;

    [MenuItem("Tools/TPQI DPU Toolkit/Open Panel")]
    public static void ShowWindow()
    {
        GetWindow<DPUToolkitEditor>("DPU Toolkit");
    }

    void OnEnable()
    {
        banner = Resources.Load<Texture2D>("banner");
    }

    void OnGUI()
{
    GUILayout.Space(10);

    if (banner != null)
    {
        GUILayout.Label(banner, GUILayout.Height(100));
    }

    EditorGUILayout.HelpBox("รบกวนเพิ่ม Tag ดังต่อไปนี้: \"bullet\", \"enemy\"", MessageType.Warning);

    GUILayout.Space(10);

    GUILayout.BeginHorizontal();
    GUILayout.FlexibleSpace();
    GUILayout.BeginVertical(GUILayout.Width(250)); // ปรับความกว้างปุ่มให้เหมาะสม

    if (GUILayout.Button("▶ Create Player", GUILayout.ExpandWidth(true)))
        CreatePlayer();

    if (GUILayout.Button("▶ Create Spawner", GUILayout.ExpandWidth(true)))
        CreateSpawner();

    if (GUILayout.Button("▶ Create Bullet", GUILayout.ExpandWidth(true)))
        CreateBullet();

    if (GUILayout.Button("▶ Create Enemy", GUILayout.ExpandWidth(true)))
        CreateEnemy();

    if (GUILayout.Button("▶ Create Floor", GUILayout.ExpandWidth(true)))
        CreateFloor();

    if (GUILayout.Button("▶ Create MainLogic", GUILayout.ExpandWidth(true)))
        CreateMainLogic();

    GUILayout.Space(10);
    EditorGUILayout.LabelField("UI Setup", EditorStyles.boldLabel);

    if (GUILayout.Button("📱 Create UI GamePlay", GUILayout.ExpandWidth(true)))
        CreateUIGameplay();

    if (GUILayout.Button("🎬 Added Title Scene", GUILayout.ExpandWidth(true)))
        CreateTitleSceneUI();

    GUILayout.Space(10);
    EditorGUILayout.LabelField("Prefab Tools", EditorStyles.boldLabel);

    if (GUILayout.Button("💾 Make Game Object to Prefab", GUILayout.ExpandWidth(true)))
        MakeSelectedObjectPrefab();

    GUILayout.EndVertical();
    GUILayout.FlexibleSpace();
    GUILayout.EndHorizontal();
}


    // ====== Static Utility Methods ======

    [MenuItem("Tools/TPQI DPU Toolkit/Added Title Scene")]
    public static void CreateTitleSceneUI()
    {
        // สร้าง EventSystem ถ้ายังไม่มี
        if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem", typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.EventSystems.StandaloneInputModule));
        }

        // Create Canvas
        GameObject canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<TitleClass>();

        CanvasScaler scaler = canvasGO.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(800, 600);

        // Create Button
        GameObject buttonGO = new GameObject("StartButton", typeof(RectTransform), typeof(Button), typeof(Image));
        buttonGO.transform.SetParent(canvasGO.transform, false);

        RectTransform rt = buttonGO.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0f);
        rt.anchorMax = new Vector2(0.5f, 0f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = new Vector2(0, 200);
        rt.sizeDelta = new Vector2(200, 60);

        // ตั้งค่าภาพปุ่มให้รับ Raycast ได้
        Image img = buttonGO.GetComponent<Image>();
        img.color = Color.white; // สีพื้นหลังมองเห็นง่าย
        img.raycastTarget = true;

        // Button Text
        GameObject textGO = new GameObject("Text", typeof(Text));
        textGO.transform.SetParent(buttonGO.transform, false);
        Text text = textGO.GetComponent<Text>();
        text.text = "Start Game";
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;

        RectTransform textRT = text.GetComponent<RectTransform>();
        textRT.anchorMin = Vector2.zero;
        textRT.anchorMax = Vector2.one;
        textRT.offsetMin = Vector2.zero;
        textRT.offsetMax = Vector2.zero;
    }

    [MenuItem("Tools/TPQI DPU Toolkit/Create UI GamePlay")]
    public static void CreateUIGameplay()
    {
        // สร้าง EventSystem ถ้ายังไม่มี
        if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem", typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.EventSystems.StandaloneInputModule));
        }

        // Create Canvas
        GameObject canvasGO = new GameObject("GameplayUI", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasGO.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(800, 600);

        Font font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        // ===== Create HP Text (Top Left) =====
        GameObject hpGO = new GameObject("HPText", typeof(RectTransform), typeof(Text));
        hpGO.transform.SetParent(canvasGO.transform, false);
        RectTransform hpRT = hpGO.GetComponent<RectTransform>();
        hpRT.anchorMin = new Vector2(0f, 1f);
        hpRT.anchorMax = new Vector2(0f, 1f);
        hpRT.pivot = new Vector2(0f, 1f);
        hpRT.anchoredPosition = new Vector2(10, -10);
        hpRT.sizeDelta = new Vector2(200, 50);
        Text hpText = hpGO.GetComponent<Text>();
        hpText.font = font;
        hpText.fontSize = 24;
        hpText.color = Color.red;
        hpText.alignment = TextAnchor.UpperLeft;
        hpText.text = "HP: ";

        // ===== Create Timer Text (Top Center) =====
        GameObject timerGO = new GameObject("TimerText", typeof(RectTransform), typeof(Text));
        timerGO.transform.SetParent(canvasGO.transform, false);
        RectTransform timerRT = timerGO.GetComponent<RectTransform>();
        timerRT.anchorMin = new Vector2(0.5f, 1f);
        timerRT.anchorMax = new Vector2(0.5f, 1f);
        timerRT.pivot = new Vector2(0.5f, 1f);
        timerRT.anchoredPosition = new Vector2(0, -10);
        timerRT.sizeDelta = new Vector2(200, 50);
        Text timerText = timerGO.GetComponent<Text>();
        timerText.font = font;
        timerText.fontSize = 24;
        timerText.color = Color.white;
        timerText.alignment = TextAnchor.UpperCenter;
        timerText.text = "Time: ";

        // ===== Create Score Text (Top Right) =====
        GameObject scoreGO = new GameObject("ScoreText", typeof(RectTransform), typeof(Text));
        scoreGO.transform.SetParent(canvasGO.transform, false);
        RectTransform scoreRT = scoreGO.GetComponent<RectTransform>();
        scoreRT.anchorMin = new Vector2(1f, 1f);
        scoreRT.anchorMax = new Vector2(1f, 1f);
        scoreRT.pivot = new Vector2(1f, 1f);
        scoreRT.anchoredPosition = new Vector2(-10, -10);
        scoreRT.sizeDelta = new Vector2(200, 50);
        Text scoreText = scoreGO.GetComponent<Text>();
        scoreText.font = font;
        scoreText.fontSize = 24;
        scoreText.color = Color.green;
        scoreText.alignment = TextAnchor.UpperRight;
        scoreText.text = "Score: ";

        // ===== Add GameplayUIUpdater script =====
        GameplayUIUpdater updater = canvasGO.AddComponent<GameplayUIUpdater>();
        updater.hpText = hpText;
        updater.timerText = timerText;
        updater.scoreText = scoreText;
    }

    [MenuItem("Tools/TPQI DPU Toolkit/Create Player")]
    public static void CreatePlayer()
    {
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player.name = "Player";
        player.tag = "Player";
        player.transform.position = new Vector3(0f, 1.0f, 0f);

        Rigidbody rb = player.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        rb.useGravity = false;

        player.AddComponent<PlayerClass>();

        BoxCollider col1 = player.GetComponent<BoxCollider>();
        BoxCollider col2 = player.AddComponent<BoxCollider>();
        col2.isTrigger = true;
    }

    [MenuItem("Tools/TPQI DPU Toolkit/Create Spawner")]
    public static void CreateSpawner()
    {
        GameObject spawner = new GameObject("Spawner");
        spawner.transform.position = Vector3.zero;
        spawner.transform.rotation = Quaternion.Euler(0, 180, 0);
        spawner.AddComponent<SpawnerClass>();
    }

    [MenuItem("Tools/TPQI DPU Toolkit/Create Bullet")]
    public static void CreateBullet()
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.name = "Bullet";
        bullet.tag = "Bullet";
        bullet.transform.position = new Vector3(0f, 1.5f, 1.0f);
        bullet.transform.localScale = Vector3.one * 0.3f;

        Rigidbody rb = bullet.AddComponent<Rigidbody>();
        rb.useGravity = false;

        SphereCollider col = bullet.GetComponent<SphereCollider>();
        if (col == null) col = bullet.AddComponent<SphereCollider>();
        col.isTrigger = true;

        TrailRenderer trail = bullet.AddComponent<TrailRenderer>();
        trail.time = 0.2f;
        trail.startWidth = 0.1f;
        trail.endWidth = 0.01f;
        trail.material = new Material(Shader.Find("Sprites/Default"));
        trail.startColor = Color.yellow;
        trail.endColor = Color.clear;

        bullet.AddComponent<BulletClass>();
    }

    [MenuItem("Tools/TPQI DPU Toolkit/Create Enemy")]
    public static void CreateEnemy()
    {
        GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Cube);
        enemy.name = "Enemy";
        enemy.tag = "Enemy";
        enemy.transform.position = new Vector3(0f, 1f, 40f);
        enemy.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        Rigidbody rb = enemy.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        BoxCollider col = enemy.GetComponent<BoxCollider>();
        if (col == null) col = enemy.AddComponent<BoxCollider>();
        col.isTrigger = true;

        enemy.AddComponent<EnemyClass>();
    }


    [MenuItem("Tools/TPQI DPU Toolkit/Create Floor")]
    public static void CreateFloor()
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.name = "Floor";
        floor.transform.position = new Vector3(0f, 0f, 40f);
        floor.transform.localScale = new Vector3(10f, 10f, 10f);
        floor.AddComponent<FloorClass>();
    }

    [MenuItem("Tools/TPQI DPU Toolkit/Create MainLogic")]
    public static void CreateMainLogic()
    {
        GameObject mainLogic = new GameObject("MainLogic");
        mainLogic.AddComponent<MainLogic>();
    }

    [MenuItem("Tools/TPQI DPU Toolkit/Make Game Object to Prefab")]
    public static void MakeSelectedObjectPrefab()
    {
        GameObject selected = Selection.activeGameObject;

        if (selected == null)
        {
            EditorUtility.DisplayDialog("ไม่มี GameObject", "กรุณาเลือก GameObject ใน Hierarchy ก่อน", "ตกลง");
            return;
        }

        string folderPath = "Assets/Resources/Prefabs";

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Prefabs");
        }

        string prefabPath = Path.Combine(folderPath, selected.name + ".prefab");
        prefabPath = AssetDatabase.GenerateUniqueAssetPath(prefabPath);

        GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(selected, prefabPath, InteractionMode.UserAction);

        if (prefab != null)
        {
            Debug.Log($"สร้าง Prefab: {prefabPath}");
            EditorUtility.DisplayDialog("สำเร็จ", $"สร้าง Prefab ที่: {prefabPath}", "ตกลง");
        }
        else
        {
            EditorUtility.DisplayDialog("ผิดพลาด", "ไม่สามารถสร้าง Prefab ได้", "ตกลง");
        }
    }
}
