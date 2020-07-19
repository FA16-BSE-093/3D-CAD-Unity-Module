using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CreateModel : MonoBehaviour
{
    private GameObject doorPrefab;
    private GameObject wallPrefab;
    private GameObject windowPrefab;

    //Dummy Data for Dummy Model
    private readonly Vector3[,] transformData = {
        { new Vector3(0,0.06f,0),new Vector3(0,10,35)},
        { new Vector3(0,0.06f,0),new Vector3(0,10,162)},
        { new Vector3(1.62f * 2,0.06f,0),new Vector3(0,10,106)},
        { new Vector3(0,0.06f,.35f * 2),new Vector3(0,30,101)},
        { new Vector3(0,0.06f,1.36f * 2),new Vector3(0,10,36)},
        { new Vector3(0,0.06f,1.72f * 2),new Vector3(0,10,111)},
        { new Vector3(1.11f * 2,0.06f,1.72f * 2),new Vector3(0,10,190)},
        { new Vector3(1.11f * 2,0.06f,3.63f * 2),new Vector3(0,10,90)},
        { new Vector3(2.01f * 2,0.06f,3.63f * 2),new Vector3(0,30,50)},
        { new Vector3(2.51f * 2,0.06f,3.63f * 2),new Vector3(0,10,100)},
        { new Vector3(3.51f * 2,0.06f,3.63f * 2),new Vector3(0,10,66)},
        { new Vector3(3.51f * 2,0.06f,2.97f * 2),new Vector3(0,30,102)},
        { new Vector3(3.51f * 2,0.06f,1.95f * 2),new Vector3(0,10,66)},
        { new Vector3(3.51f * 2,0.06f,1.29f * 2),new Vector3(0,10,101)},
        { new Vector3(4.52f * 2,0.06f,1.29f * 2),new Vector3(0,10,161)},
        { new Vector3(4.52f * 2,0.06f,2.90f * 2),new Vector3(0,30,222)},
        { new Vector3(6.74f * 2,0.06f,2.90f * 2),new Vector3(0,10,290)},
        { new Vector3(6.74f * 2,0.06f,0),new Vector3(0,10,144)},
        { new Vector3(5.30f * 2,0.06f,0),new Vector3(0,30,201)},
        { new Vector3(3.29f * 2,0.06f,0),new Vector3(0,10,61)},
        { new Vector3(2.68f * 2,0.06f,0),new Vector3(0,10,109)},
        { new Vector3(2.68f * 2,0.06f,1.09f * 2),new Vector3(0,10,56)},
        { new Vector3(2.12f * 2,0.06f,1.09f * 2),new Vector3(0,30,50)},
        { new Vector3(1.62f * 2,0.06f,0),new Vector3(0,10,109)},
        { new Vector3(1.62f * 2,0.06f,1.09f * 2),new Vector3(0,10,63)},
        { new Vector3(1.62f * 2,0.06f,1.72f * 2),new Vector3(0,30,50)}
    };
    private readonly Vector3[] rotationData = {
        new Vector3(0,0,0),
        new Vector3(0,90,0),
        new Vector3(0,90,0),
        new Vector3(0,0,0),
        new Vector3(0,0,0),
        new Vector3(0,90,0),
        new Vector3(0,0,0),
        new Vector3(0,90,0),
        new Vector3(0,90,0),
        new Vector3(0,90,0),
        new Vector3(0,180,0),
        new Vector3(0,180,0),
        new Vector3(0,180,0),
        new Vector3(0,90,0),
        new Vector3(0,0,0),
        new Vector3(0,90,0),
        new Vector3(0,180,0),
        new Vector3(0,270,0),
        new Vector3(0,270,0),
        new Vector3(0,270,0),
        new Vector3(0,0,0),
        new Vector3(0,270,0),
        new Vector3(0,270,0),
        new Vector3(0,0,0),
        new Vector3(0,0,0),
        new Vector3(0,270,0)
    };
    private readonly Color[] color = {
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white,
        Color.white
    };
    private readonly string[] objType = {
        "wall",
        "wall",
        "wall",
        "window",
        "wall",
        "wall",
        "wall",
        "wall",
        "door",
        "wall",
        "wall",
        "window",
        "wall",
        "wall",
        "wall",
        "window",
        "wall",
        "wall",
        "window",
        "wall",
        "wall",
        "wall",
        "door",
        "wall",
        "wall",
        "door"
    };

    //Real Data for Real Model
    private List<Vector3> startPosValues, scalValues, rotationValues;
    private List<Color> color_;
    private List<string> objType_;
    private List<double> objectLength;

    // Start is called before the first frame update
    void Start()
    {
        var classRef = GameObject.Find("ScriptManager").GetComponent<UnityPublicVariables>();
        doorPrefab = classRef.GetDoorPrefab();
        wallPrefab = classRef.GetWallPrefab();
        windowPrefab = classRef.GetWindowPrefab();

        Interface._obj.SetClassRef(classRef);


        
        if (!Interface._obj.GetSavedFile() && !Interface._obj.GetTempFile())
        {
            //dummyModel();

            AndroidPluginCallBack androidPlugin = new AndroidPluginCallBack();

            JSONData jSONData = new JSONData();
            string jsonString = androidPlugin.GetJSONString();
            jSONData.JsonDataExtraction(jsonString);

            startPosValues = jSONData.GetStartPosValues();
            scalValues = jSONData.GetScalValues();
            rotationValues = jSONData.GetRotationValues();
            color_ = jSONData.GetColor();
            objType_ = jSONData.GetObjType();
            objectLength = jSONData.GetObjLength();

            RealModel();

        }

    }

    private void dummyModel()
    {
        var createObj = new CreateGameObject();
        
        var iteration = 0;
        GameObject instentiatedPrefab = null;
        string prefabName = null;
        while (iteration < transformData.GetLength(0))
        {
            var modelObj = new GameObject_Model();

            // Creating an object
            if (objType[iteration].CompareTo("wall") == 0)
            {
                instentiatedPrefab = createObj.InstantiateGameObj(wallPrefab, transformData[iteration, 0], false);
                prefabName = wallPrefab.name;
            }
            else if (objType[iteration].CompareTo("window") == 0)
            {
                instentiatedPrefab = createObj.InstantiateGameObj(windowPrefab, transformData[iteration, 0], true);
                prefabName = windowPrefab.name;
            }
            else if (objType[iteration].CompareTo("door") == 0)
            {
                instentiatedPrefab = createObj.InstantiateGameObj(doorPrefab, transformData[iteration, 0], false);
                prefabName = doorPrefab.name;
            }
            instentiatedPrefab = createObj.GameObjSetting(instentiatedPrefab, transformData[iteration, 1], rotationData[iteration], color[iteration]);

            // Adding the game object to modelObj
            modelObj.SetGameObject(instentiatedPrefab, transformData[iteration, 0], transformData[iteration, 1], rotationData[iteration], color[iteration], prefabName, 111);

            // Adding the game object to list of instantiated game objects
            InstantiatedGameObject._obj.SetInstantiatedModelObj(modelObj);
            modelObj = null;

            iteration++;

        }
        Interface._obj.SetSceneLoaded(true);

    }

    private void RealModel()
    {
        var createObj = new CreateGameObject();

        var iteration = 0;
        GameObject instentiatedPrefab = null;
        string prefabName = null;
        while (iteration < startPosValues.Count)
        {
            var modelObj = new GameObject_Model();

            // Creating an object
            if (objType_[iteration].CompareTo("Walls") == 0)
            {
                instentiatedPrefab = createObj.InstantiateGameObj(wallPrefab, startPosValues[iteration], false);
                prefabName = wallPrefab.name;
            }
            else if (objType_[iteration].CompareTo("Windows") == 0)
            {
                instentiatedPrefab = createObj.InstantiateGameObj(windowPrefab, startPosValues[iteration], true);
                prefabName = windowPrefab.name;
            }
            else if (objType_[iteration].CompareTo("Doors") == 0)
            {
                instentiatedPrefab = createObj.InstantiateGameObj(doorPrefab, startPosValues[iteration], false);
                prefabName = doorPrefab.name;
            }
            instentiatedPrefab = createObj.GameObjSetting(instentiatedPrefab, scalValues[iteration], rotationValues[iteration], color_[iteration]);

            // Adding the game object to modelObj
            modelObj.SetGameObject(instentiatedPrefab, startPosValues[iteration], scalValues[iteration], rotationValues[iteration], color_[iteration], prefabName, objectLength[iteration]);

            // Adding the game object to list of instantiated game objects
            InstantiatedGameObject._obj.SetInstantiatedModelObj(modelObj);

            iteration++;
        }
    }


}
