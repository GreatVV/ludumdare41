using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class CharacterPrefabCreatorEditor : EditorWindow
{
    [MenuItem("Tools/Character Editor")]
    public static void OpenWindow()
    {
        EditorWindow.GetWindow<CharacterPrefabCreatorEditor>();
    }

    public void Update()
    {
        Repaint();
    }

    private GameObject _characterRootBone;
    private GameObject _targetTentacle;

    private GameObject _targetBoneOne;
    private GameObject _targetBoneTwo;

    private float _colliderRadius = 0.05f;

    
    private SerializedProperty _tentacleSerializedProrerty;
    private SerializedObject _serializedObject;

    [System.Serializable]
    private class TentacleContainer : Object
    {
        [SerializeField] private List<GameObject> _tentacleRoots = new List<GameObject>();
    }

    private TentacleContainer _tentacleContainer = new TentacleContainer();

    private void OnEnable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
        _serializedObject = new SerializedObject(this);
        _tentacleSerializedProrerty = _serializedObject.FindProperty(nameof(_tentacleSerializedProrerty));
    }

    private void OnDestroy()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }

    public void OnGUI()
    {
        GUILayout.Label("Character Prefab Editor");

        EditorGUILayout.Space();

        _characterRootBone = (GameObject)EditorGUILayout.ObjectField("Root bone", _characterRootBone, typeof(GameObject));

        EditorGUILayout.Space();

        _targetTentacle = (GameObject)EditorGUILayout.ObjectField("Tentacle", _targetTentacle, typeof(GameObject));

        _colliderRadius = EditorGUILayout.FloatField("Collider radius", _colliderRadius);

        if(GUILayout.Button("Initialize tentacle"))
        {
            if(_targetTentacle == null)
            {
                Debug.LogWarning("No traget gameobject");
            }
            else
            {
                AddPhysicsComponentsToBones(_targetTentacle);
            }
        }


    }

    private void OnSceneGUI(SceneView scnene_view)
    {
        if(_characterRootBone != null)
        {
            Handles.color = Color.cyan;
            DrawTransformHierarchy(_characterRootBone.transform);
        }
    }

    private void DrawTransformHierarchy(Transform transform)
    {
        foreach (Transform child_transform in transform)
        {
            Handles.DrawLine(transform.position, child_transform.position);
            Handles.DrawWireCube(child_transform.position, Vector3.one * 0.01f);
            DrawTransformHierarchy(child_transform);
        }
    }

    private void AddPhysicsComponentsToBones(GameObject root_bone)
    {
        AddPhysicsComponentsToBone(root_bone, null);
    }

    private void AddPhysicsComponentsToBone(GameObject bone, Rigidbody parent_bone)
    {
        SphereCollider collider = bone.GetComponent<SphereCollider>();
        if (collider == null)
        {
            collider = bone.AddComponent<SphereCollider>();
        }
        collider.radius = _colliderRadius;

        Rigidbody rigidbody = bone.GetComponent<Rigidbody>();
        if(rigidbody == null)
        {
            rigidbody = bone.AddComponent<Rigidbody>();
        }
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;

        ConfigurableJoint joint = bone.GetComponent<ConfigurableJoint>();
        if(joint == null)
        {
            joint = bone.AddComponent<ConfigurableJoint>();
        }
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.angularXMotion = ConfigurableJointMotion.Limited;
        joint.angularYMotion = ConfigurableJointMotion.Limited;
        joint.angularZMotion = ConfigurableJointMotion.Locked;

        SoftJointLimit joint_limit = joint.lowAngularXLimit;
        joint_limit.limit = -45.0f;
        joint.lowAngularXLimit = joint_limit;

        joint_limit = joint.highAngularXLimit;
        joint_limit.limit = 45.0f;
        joint.highAngularXLimit = joint_limit;

        joint_limit = joint.angularYLimit;
        joint_limit.limit = 45.0f;
        joint.angularYLimit = joint_limit;

        if (parent_bone != null)
        {
            joint.connectedBody = parent_bone;
        }
        
        if(bone.transform.childCount > 0)
        {
            for(int i = 0; i < bone.transform.childCount; i++)
            {
                AddPhysicsComponentsToBone(bone.transform.GetChild(i).gameObject, rigidbody);
            }
        }
    }
}
