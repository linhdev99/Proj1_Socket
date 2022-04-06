using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class OptTransform
{
    public List<float> position { get; set; }
    public List<float> rotation { get; set; }
    public List<float> scale { get; set; }
}
public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    private void Awake()
    {
        if (GM != null)
            GameObject.Destroy(gameObject);
        else
            GM = this;
        DontDestroyOnLoad(this);
    }
    string dataTest;
    void Start()
    {

    }

    void Update()
    {

    }
    public string GetDataServer()
    {
        string jsonData = dataTest;
        return jsonData;
    }
    public void SetDataServer(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        OptTransform m_optTransform = new OptTransform
        {
            position = new List<float> { pos.x, pos.y, pos.z },
            rotation = new List<float> { rot.x, rot.y, rot.z, rot.w },
            scale = new List<float> { scale.x, scale.y, scale.z },
        };
        string jsonString = JsonConvert.SerializeObject(m_optTransform);
        dataTest = jsonString;
        // Debug.Log(jsonString);
    }
}
