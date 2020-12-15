using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

[RequireComponent(typeof(SpawnedObjectManager))]
public class Spawner : MonoBehaviour
{
    [Tooltip("Main camera of the scene.")]
    public Camera cam;
    [Tooltip("Min distance from camera of the placeholder object.")]
    public float minDistance = 1;
    [Tooltip("Max distance from camera of the placeholder object.")]
    public float maxDistance = 10;
    [Tooltip("Initial distance from camera of the placeholder object.")]
    public float initialDistance = 2;
    [Space]

    [Tooltip("List of spawnables")]
    public List<GameObject> objects;
    [Space]

    [Tooltip("List of objects already spawned in scene")]
    public List<GameObject> objectsAlreadyInScene;
    [Space]

    [Tooltip("Material assigned to the placeholder objects")]
    public Material outlineMaterial;
    [Tooltip("Particle instantiated on spawn. If not assigned it doesn't spawn a particle")]
    public GameObject spawnParticle;
    [Tooltip("Material assigned to the placeholder objects")]
    public GameObject despawnParticle;
    [Space]

    [Tooltip("Enable the marker lineRenderer below the gameobject")]
    public bool wantMarker = true;
    [Tooltip("LineRenderer's material")]
    public Material lineRendererMaterial;

    SpawnedObjectManager spawnedObjectManager;
    List<GameObject> outlinedObjects;
    int cycleIndex = 0;
    //float scrollSpeed = 10;
    GameObject parent;
    float distance;
    Vector3 currentPos;
    //Quaternion currentRotation;
    public float Rotspeed;
    public Quaternion RotAngle;
    public int[] NObjects;
    public int SObjects;
    public int[] MaxObjects;
    public int SMax;
    public bool PauseOn;
    public bool GoalOn;
    public bool TimerOff;

    void Start()
    {
        distance = initialDistance;
        outlinedObjects = new List<GameObject>();
        spawnedObjectManager = GetComponent<SpawnedObjectManager>();
        SpawnInitialObjects();
        AddAlreadySpawnedObjects();

        RecalculateCurrentPosAndRot();

        CinemachineCore.CameraUpdatedEvent.AddListener(UpdateObjectPosition);
    }

    void Awake()
    {
        foreach (int value in MaxObjects) 
        {
            SMax += value;
        }
    }

    /// <summary>
    /// Recalculate position and rotation used to spawn objects
    /// </summary>
    void RecalculateCurrentPosAndRot()
    {
        currentPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
        
        Vector3 lookPos = cam.transform.position;
        lookPos.y = 0;
        //currentRotation = Quaternion.LookRotation(lookPos);
    }

    void Update()
    {
        //New feature to be evalutated - use the mouse scroll wheel to increase or decrease the distance from the camera
        /*var dist = Input.GetAxis("Mouse ScrollWheel");
        if(dist != 0)
            distance += Time.deltaTime * (dist > 0 ? scrollSpeed : -scrollSpeed);*/

        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        if(PauseOn == false && GoalOn == false && TimerOff == false)
        {
            outlinedObjects[cycleIndex].SetActive(true);    
        }
        else
        {
            outlinedObjects[cycleIndex].SetActive(false);
        }
        
        PauseOn = GameObject.Find("CanvasPause").GetComponent<UIPause>().GameIsPaused;
        GoalOn = GameObject.Find("LevelGoal").GetComponent<Goal>().GoalOn;
        TimerOff = GameObject.Find("Timer").GetComponent<UITimer>().TimerOff;
        
        if(Input.GetMouseButton(1) && PauseOn == false && GoalOn == false && TimerOff == false)
        {
            outlinedObjects[cycleIndex].transform.Rotate(Vector3.forward * Rotspeed);
            RotAngle = outlinedObjects[cycleIndex].transform.rotation;
        }

        if(SObjects > GameObject.Find("PointsSystem").GetComponent<PointsSystem>().MaxObj)
        {
           SObjects = GameObject.Find("PointsSystem").GetComponent<PointsSystem>().MaxObj;
        }

        //Mouse left click - Instantiate the selected object
        if (Input.GetMouseButtonUp(0) && GameObject.Find("Timer").GetComponent<UITimer>().time > 0.5f && GameObject.Find("LevelGoal").GetComponent<Goal>().time > 0.5f && PauseOn == false && GoalOn == false && TimerOff == false)
        {
            if(NObjects[cycleIndex] < MaxObjects[cycleIndex])
            {
                var spawned = Instantiate(objects[cycleIndex], currentPos, RotAngle);  
                SObjects += 1;    
                AddObject(spawned);
                NObjects[cycleIndex] += 1;  
                
                if(cycleIndex != MaxObjects.Length-1)
                {
                    spawnedObjectManager.audioController.PlayRandomClip(spawnedObjectManager.audioController.forwardNoteClips);
                }
                else
                {
                    spawnedObjectManager.audioController.PlayRandomClip(spawnedObjectManager.audioController.bombNoteClips);
                }
            }
            else if(cycleIndex != MaxObjects.Length-1)
            {
                spawnedObjectManager.audioController.PlayRandomClip(spawnedObjectManager.audioController.reverseNoteClips);
            }
    
            if (spawnParticle != null && NObjects[cycleIndex] < MaxObjects[cycleIndex])
            {
                var particle = Instantiate(spawnParticle, currentPos, RotAngle);
                Destroy(particle, 3);
            }


            //animation
            //spawned.transform.DOScale(0, .25f).SetEase(Ease.OutBounce).From();

            //Play random forward audio clip
            /*if(spawnedObjectManager.audioController != null)
                spawnedObjectManager.audioController.PlayRandomClip(spawnedObjectManager.audioController.forwardNoteClips);*/
        }
        /*LeftShift + Mouse left click - Raycast and if you hit a spawned object, destroy it!
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonUp(0) && PauseOn == false)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (spawnedObjectManager.CheckAndDestroyObject(hit.transform.gameObject) && despawnParticle != null)
                {
                    var particle = Instantiate(despawnParticle, currentPos, despawnParticle.transform.rotation);
                    Destroy(particle, 3);

                    //Play random forward audio clip
                    if(spawnedObjectManager.audioController != null)
                        spawnedObjectManager.audioController.PlayRandomClip(spawnedObjectManager.audioController.reverseNoteClips);
                }
            }
        }*/

        //Cycle through the list of outlined objects
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            CycleList(false);
            outlinedObjects[cycleIndex].transform.rotation = new Quaternion(0,0,0,0);
            RotAngle = new Quaternion(0,0,0,0);
        }
            
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            CycleList(true);
            outlinedObjects[cycleIndex].transform.rotation = new Quaternion(0,0,0,0);
            RotAngle = new Quaternion(0,0,0,0);
        }
            
    }

    /// <summary>
    /// Given an object, adds all children with a rigidbody to the spawned objects.
    /// </summary>
    void AddObject(GameObject spawnedObject)
    {
        var bodies = spawnedObject.GetComponentsInChildren<Rigidbody>();
        foreach(var b in bodies)
        {
            spawnedObjectManager.AddObject(b.gameObject);
        }
    }

    /// <summary>
    /// Add the object in the "objectsAlreadyInScene" list to the spawned objects.
    /// </summary>
    void AddAlreadySpawnedObjects()
    {
        foreach(var o in objectsAlreadyInScene)
        {
            AddObject(o);
        }
    }

    /// <summary>
    /// Update the position of the current object.
    /// </summary>
    void UpdateObjectPosition(CinemachineBrain brain)
    {
        RecalculateCurrentPosAndRot();
        parent.transform.position = currentPos;
        //parent.transform.rotation = currentRotation;
    }

    /// <summary>
    /// Spawn the placeholder gameobjects. This is the list of gameobject to cycle across.
    /// Placeholders don't need colliders, rigidbodies and have a different material.
    /// </summary>
    void SpawnInitialObjects()
    {
        parent = new GameObject();
        parent.name = "OutlinedPlaceholders";

        for(int i = 0; i < objects.Count; i++)
        {
            GameObject tempObj = Instantiate(objects[i], parent.transform);
            tempObj.SetActive(false);

            var colliders = tempObj.GetComponentsInChildren<Collider>();
            foreach (var collider in colliders)
                Destroy(collider);

            var rigidbodies = tempObj.GetComponentsInChildren<Rigidbody>();
            foreach (var rigidbody in rigidbodies)
                Destroy(rigidbody);

            var meshRenderers = tempObj.GetComponentsInChildren<Renderer>();
            foreach (var mR in meshRenderers)
            {
                mR.material = outlineMaterial;
                mR.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }

            if (wantMarker)
            {
                var marker = tempObj.AddComponent<Marker>();
                if (lineRendererMaterial != null)
                    marker.lineMaterial = lineRendererMaterial;
            }

            outlinedObjects.Add(tempObj);
        }
    }

    /// <summary>
    /// Cycle through the outlined objects list
    /// </summary>
    void CycleList(bool wantIncrement)
    {
        outlinedObjects[cycleIndex].SetActive(false);
        if (wantIncrement)
        {
            cycleIndex++;
            if (cycleIndex == outlinedObjects.Count)
                cycleIndex = 0;
        }
        else
        {
            cycleIndex--;
            if (cycleIndex < 0)
                cycleIndex = outlinedObjects.Count - 1;
        }
        outlinedObjects[cycleIndex].SetActive(true);
    }

    void OnDestroy() {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(UpdateObjectPosition);
    }
}
