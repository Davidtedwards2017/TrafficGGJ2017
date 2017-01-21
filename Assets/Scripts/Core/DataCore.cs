using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DataCore {

	//UI
	public static bool developerMode = false;
	public static bool isColorBlind = false;

}

public static class Prefabs {

    public static class ActionObjects
    {
        public static GameObject whackVolume = Resources.Load<GameObject>("Prefabs/Action/WhackVolume");
        public static GameObject homingBullet = Resources.Load<GameObject>("Prefabs/Action/HomingBullet");
        public static GameObject grenade = Resources.Load<GameObject>("Prefabs/Action/Grenade");
        public static GameObject tortoiseHead = Resources.Load<GameObject>("Prefabs/Action/TortoiseHead");

        public static GameObject straightLineAimer = Resources.Load<GameObject>("Prefabs/StraightLineAimer");
    }

    public static class Characters
    {
        public static GameObject jean = Resources.Load<GameObject>("Prefabs/Characters/PlayerJean");
        public static GameObject bb = Resources.Load<GameObject>("Prefabs/Characters/PlayerBB");
        public static GameObject illuma = Resources.Load<GameObject>("Prefabs/Characters/PlayerIlluma");
    }

    public static class UI
    {
        public static class Phone
        {
            public static GameObject OptionGroup = Resources.Load<GameObject>("Prefabs/UI/Phone/PhoneOptionGroup");
            public static GameObject Option = Resources.Load<GameObject>("Prefabs/UI/Phone/PhoneOption");
        }


        public static GameObject textPanel = Resources.Load<GameObject>("Prefabs/PointOfInterestText");

    }

    public static class FX
    {
        public static class Dust
        {
            public static GameObject footstep = Resources.Load<GameObject>("Prefabs/FX/FX_Footstep_Dust");
            public static GameObject dash = Resources.Load<GameObject>("Prefabs/FX/FX_NEW_Dash");
            public static GameObject grenadeExplosion = Resources.Load<GameObject>("Prefabs/FX/FX_Grenade_Explosion");
        }

        public static class Sparkle
        {
            
        }
    }
}