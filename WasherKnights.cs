using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using GlobalEnums;
using Modding;
using UnityEngine;

namespace WasherKnights
{
    
    public class WasherKnights : Mod
    {

        public override string GetVersion()
        {
            return "1.0";
        }

        public override List<(string, string)> GetPreloadNames()
        {
            return new List<(string, string)>
            {
                ("Ruins2_03_boss", "Battle Control/Black Knight 2/Corpse Black Knight 1(Clone)")
            };   
        }

        public GameObject wk;
        public IEnumerator ReplaceWatcherKnights()
        {
            yield return new WaitWhile(() => HeroController.instance == null);
            Assembly asm = Assembly.GetExecutingAssembly();
            foreach (string res in asm.GetManifestResourceNames())
            {   
                if(!res.EndsWith("WatcherKnights.png")) {
                    continue;
                } 
                using (Stream s = asm.GetManifestResourceStream(res))
                {
                        if (s == null) continue;
                        var buffer = new byte[s.Length];
                        s.Read(buffer, 0, buffer.Length);
                        var texture2 = new Texture2D(2, 2);
                        texture2.LoadImage(buffer.ToArray(),true);
                        wk.GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture = texture2;
                        s.Dispose();
                        
                }
            }
           
        }
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            wk = preloadedObjects["Ruins2_03_boss"]["Battle Control/Black Knight 2/Corpse Black Knight 1(Clone)"];
            GameManager.instance.StartCoroutine(ReplaceWatcherKnights());
        }

    }

}
