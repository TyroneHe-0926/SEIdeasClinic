using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script requires an object tagged "StarContainer" to be present in the game core scene.
/// One star will be generated for each child object within the StarContainer gameObject.
/// This allows you to use the Unity Scene editor as a star map designing tool.
/// </summary>

namespace Sandbox
{
    public class GalaxyMapGenerator
    {   
        List<link> links = new List<link>();
        bool mapGenerated = false;
        Transform StarHolder;

        public void GenerateStarMap()
        {
            /*
            StarHolder = GameObject.FindGameObjectWithTag("StarContainer").transform;
            foreach (Transform child in StarHolder)
                Map.starList.Add(new GalaxyStar(child.name, child.transform.position));

            // This is where the warp gate links between the stars is defined. 
            AddLink("Sol", "Alpha Centauri");
            AddLink("Alpha Centauri", "Kepler 438");
            // ... more links here ...

            mapGenerated = true;
            */
        }

        #region Making Links

        bool AddLink(int firstStarIndex, int secondStarIndex)
        {
            bool linkAlreadyExists = false;
            foreach (link link in links)
                if ((link.first == firstStarIndex && link.second == secondStarIndex) || (link.first == secondStarIndex && link.second == firstStarIndex))
                    linkAlreadyExists = true;
            if (!linkAlreadyExists)
                links.Add(new link(firstStarIndex, secondStarIndex));
            return !linkAlreadyExists;
        }

        bool AddLink(string first, string second)
        {
            int firstStarIndex = StarListIndexFromStarName(first);
            int secondStarIndex = StarListIndexFromStarName(second);
            return AddLink(firstStarIndex, secondStarIndex);
        }

        int StarListIndexFromStarName(string name)
        {
            /*
            for(int i=0; i< Map.starList.Count; i++)
                if (Map.starList[i].name == name)
                    return i;
            
            */
            return -1;
        }

        struct link
        {
            public int first;
            public int second;

            public link(int first0, int secon0)
            {
                first = first0;
                second = secon0;
            }
        }
        #endregion

        public void DrawWarpLinesInEditor()
        {
            /*
            if (mapGenerated)
                foreach (link l in links)
                    Debug.DrawLine(Map.starList[l.first].galacticPosition, Map.starList[l.second].galacticPosition);
                    */
        }
    }
}
