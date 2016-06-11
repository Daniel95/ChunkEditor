﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeSelection : MonoBehaviour {

    [SerializeField]
    private ChunkEditor chunkEditor;

    [SerializeField]
    private ObjectSelection objectSelection;

    private Vector2 selectionRange = new Vector2(1, 1);

    private List<Vector2> lastPreviewedNodes = new List<Vector2>();

    Dictionary<Vector2, Node> allNodes = new Dictionary<Vector2, Node>();

    public void AddNode(Vector2 _position, Node _node) {
        allNodes.Add(_position, _node);
    }

    //the node has been clicked and confirmed
    public void ConfirmNode(Node _node)
    {
        ChangeObject(_node.Position, objectSelection.SelectedObjectValue, objectSelection.SelectedObjectSize);
    }

    //the node has been selected
    public void SelectedNode(Node _node)
    {
        List<Vector2> selectedPositions = GetNodeSelection(_node.Position, objectSelection.SelectedObjectSize, selectionRange);

        DeselectLastNodes();

        for (int n = 0; n < selectedPositions.Count; n++) {
            allNodes[selectedPositions[n]].Select(objectSelection.SelectedObjectValue);

            //add the nodes that to the lastPreviewedNodes, so that we will remove it next time
            lastPreviewedNodes.Add(selectedPositions[n]);
        }
    }

    //were we switch what value a node contains
    public void ChangeObject(Vector2 _confirmedNodePosition, int _objectValue, Vector2 _newObjectSize)
    {
        //if the node we are changing has already been confirmed, we need to clear its parent and the nodes the parent overlaps
        if (allNodes[_confirmedNodePosition].Confirmed) {
            //clear the parents true value, and value in the grid
            allNodes[allNodes[_confirmedNodePosition].ParentNodePosition].TrueValue = 0;
            chunkEditor.EditNodes(allNodes[allNodes[_confirmedNodePosition].ParentNodePosition].Position, allNodes[allNodes[_confirmedNodePosition].ParentNodePosition].Size, new Vector2(1, 1), 0);

            //clear all nodes the parent overlaps
            List<Vector2> positionToClear = GetNodeSelection(allNodes[allNodes[_confirmedNodePosition].ParentNodePosition].Position, allNodes[allNodes[_confirmedNodePosition].ParentNodePosition].Size, selectionRange);

            for (int n = 0; n < positionToClear.Count; n++)
            {
                allNodes[positionToClear[n]].Confirmed = false;
                allNodes[positionToClear[n]].ConfirmedValue = 0;
                allNodes[positionToClear[n]].Deselect();
            }
        }

        chunkEditor.EditNodes(_confirmedNodePosition, _newObjectSize, new Vector2(1, 1), _objectValue);

        //replace all nodes
        List<Vector2> selectedPositions = GetNodeSelection(_confirmedNodePosition, objectSelection.SelectedObjectSize, selectionRange);

        for (int n = 0; n < selectedPositions.Count; n++) {
            allNodes[selectedPositions[n]].Confirmed = true;
            allNodes[selectedPositions[n]].ConfirmedValue = objectSelection.SelectedObjectValue;
            allNodes[selectedPositions[n]].ParentNodePosition = _confirmedNodePosition;
            allNodes[selectedPositions[n]].Select(objectSelection.SelectedObjectValue);
            allNodes[selectedPositions[n]].Size = objectSelection.SelectedObjectSize;
        }
    }

    //return the parent node, and the nodes the parent overlaps
    public List<Vector2> GetNodeSelection(Vector2 _parentNodePosition, Vector2 _nodeSize, Vector2 _selectionSize)
    {
        //the position we start editing
        Vector2 positionToEdit = _parentNodePosition - new Vector2(Mathf.Floor(_nodeSize.x / 2), Mathf.Floor(_nodeSize.y / 2));

        List<Vector2> nodeSelection = new List<Vector2>();

        for (int y = 0; y < _nodeSize.y; y++)
        {
            for (int x = 0; x < _nodeSize.x; x++)
            {
                if (allNodes.ContainsKey(positionToEdit + new Vector2(x, y)))
                {
                    nodeSelection.Add(positionToEdit + new Vector2(x, y));
                }
            }
        }

        return nodeSelection;
    }

    private void DeselectLastNodes()
    {
        for (int i = 0; i < lastPreviewedNodes.Count; i++) {
            allNodes[lastPreviewedNodes[i]].Deselect();
        }

        lastPreviewedNodes.Clear();
    }
}
