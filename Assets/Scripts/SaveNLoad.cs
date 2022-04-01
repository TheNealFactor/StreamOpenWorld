using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



[SerializeField]

class ChunkData
{
    public Mesh serializableMesh;
    public Material serializableMaterial;
}

public class SaveNLoad : MonoBehaviour
{
    // Start is called before the first frame update
    ChunkData chunkData;
    string json;
    string dataPath;
    string finalFilePath;
    Material material;



    public void Save(string name, GameObject tileMesh)
    {
        if (tileMesh != null)
        {
            chunkData = new ChunkData();
            chunkData.serializableMesh = tileMesh.GetComponent<MeshFilter>().sharedMesh;
            chunkData.serializableMaterial = tileMesh.GetComponent<Renderer>().material;
            json = JsonUtility.ToJson(chunkData, true);
            dataPath = Application.persistentDataPath + "/" + name + ".json";
            print("File name is :" + dataPath);
            File.WriteAllText(dataPath, json);
        }
        else
        {
            print("Gameobject is equal to null");
        }

    }
    public void Load(string filePath, GameObject tileMesh)
    {
        if (filePath != null)
        {
            finalFilePath = Application.persistentDataPath + "/" + filePath + ".json";

            json = File.ReadAllText(finalFilePath);

            ChunkData loadedChunkData = JsonUtility.FromJson<ChunkData>(json);

            SetMesh(loadedChunkData.serializableMesh, loadedChunkData.serializableMaterial, tileMesh);

            Debug.Log("Loaded" + finalFilePath);


        }
    }



    public void SetMesh(Mesh setMesh, Material setMat, GameObject tileMesh)
    {
        if (this.GetComponent<MeshFilter>() == null)
        {
            tileMesh.gameObject.AddComponent<MeshFilter>();
            tileMesh.gameObject.AddComponent<MeshRenderer>();
            tileMesh.gameObject.AddComponent<MeshCollider>();

            tileMesh.GetComponent<MeshFilter>().sharedMesh = setMesh;
            tileMesh.GetComponent<MeshRenderer>().material = setMat;
            tileMesh.GetComponent<MeshCollider>().sharedMesh = setMesh;
            print("Set mesh on loaded terrain");
        }


        material = setMat;
    }



}

