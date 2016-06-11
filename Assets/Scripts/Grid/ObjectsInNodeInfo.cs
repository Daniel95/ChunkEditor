using System;
using UnityEngine;
using System.Collections.Generic;

public class ObjectsInNodeInfo : MonoBehaviour {

    [Serializable]
    public class NodesInfo
    {
        [SerializeField]
        public Sprite sprite;

        [SerializeField]
        public Vector2 nodeSize;
    }

    public NodesInfo[] nodesInfo;

    private static List<Sprite> nodeSprites = new List<Sprite>();

    private static List<Vector2> nodeSizes = new List<Vector2>();

    public static List<Sprite> NodeSprites
    {
        get { return nodeSprites; }
    }

    public static List<Vector2> NodeSizes
    {
        get { return nodeSizes; }
    }

    void Awake()
    {
        for (int i = 0; i < nodesInfo.Length; i++) {
            nodeSprites.Add(nodesInfo[i].sprite);

            nodeSizes.Add(nodesInfo[i].nodeSize);
        }
    }
}
