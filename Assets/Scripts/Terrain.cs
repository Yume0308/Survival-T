using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;
using Unity.VisualScripting.ReorderableList;
public class Terrain : MonoBehaviour
{
    public Terrain terrain;
    public TerrainData terrainData;
    private void Awake()
    {
        SerializedObject tagManager = new SerializedObject(
                AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]
                );
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        // Adding new tags
        AddTag(tagsProp, "Terrain", TagType.Tag);
        AddTag(tagsProp, "Grass", TagType.Tag);
        AddTag(tagsProp, "Dirt", TagType.Tag);
        AddTag(tagsProp, "Rock", TagType.Tag);
        AddTag(tagsProp, "Snow", TagType.Tag);
        AddTag(tagsProp, "Sand", TagType.Tag);
        AddTag(tagsProp, "Water", TagType.Tag);
        AddTag(tagsProp, "Tree", TagType.Tag);
        AddTag(tagsProp, "Bush", TagType.Tag);
        AddTag(tagsProp, "Flower", TagType.Tag);
        AddTag(tagsProp, "Fence", TagType.Tag);
        AddTag(tagsProp, "Cloud", TagType.Tag);
        AddTag(tagsProp, "Shore", TagType.Tag);

        tagManager.ApplyModifiedProperties();

        // Adding new layers
        SerializedProperty layersProp = tagManager.FindProperty("layers");
        terrainLayer = AddTag(layersProp, "Terrain", TagType.Layer);
        tagManager.ApplyModifiedProperties();

        // tag this obj
        this.gameObject.tag = "Terrain";
        this.gameObject.layer = terrainLayer;
    }
    private void OnEnable()
    {
        terrain = this.GetComponent<Terrain>();
        terrainData = terrain.terrainData;
    } 
    public enum TagType { Tag, Layer};
    [SerializeField] int terrainLayer = 0;
    int AddTag(SerializedProperty tagsProp, string newTag, TagType tType)
    {
        bool found = false;

        // Ensure the tag doesn't already exist
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(newTag)) { found = true; return i; }
        }

        // add your new tag
        if (!found && tType == TagType.Tag)
        {
            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty newTagProp = tagsProp.GetArrayElementAtIndex(0);
            newTagProp.stringValue = newTag;
        }

        // add your new layer
        else if (!found && tType == TagType.Layer)
        {

            for (int j = 8; j < tagsProp.arraySize; ++j)
            {

                SerializedProperty newLayer = tagsProp.GetArrayElementAtIndex(j);
                // Add layer in next slot
                if (newLayer.stringValue == "")
                {

                    Debug.Log("Adding New Layer: " + newTag);
                    newLayer.stringValue = newTag;
                    return j;
                }
            }
        }
        return -1;
    }
}
