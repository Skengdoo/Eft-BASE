using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EFTDLL
{
    public class Cfg
    {
        public static bool MenuToggle { get; set; }
        public static bool OldCursorVisible { get; set; }
        public static CursorLockMode OldCursorLockMode { get; set; }
        public static Vector2 ScrollPosition { get; set; } = Vector2.zero;
        public static Rect WindowRect { get; set; } = new Rect(20, 20, 500, 1000);
        public static Vector2 Scroll1Position { get; set; } = Vector2.zero;
        public static Rect WindowRect1 { get; set; } = new Rect(20, 40, 400, 400);

        public static Vector3 ScreenPosition => ScreenPosition;

        public static bool LootEsp { get; set; }
        public static bool duffleBag { get; set; }
        public static bool weaponBox { get; set; }
        public static bool toolBox { get; set; }
        public static bool supplyBox { get; set; }
        public static bool woodenCrate { get; set; }
        public static bool Stash { get; set; }
        public static bool ammoCrate { get; set; }
        public static bool MoneyBox { get; set; }
        public static bool grenadeBox { get; set; }
        public static bool Safe { get; set; }

        public static bool snaplines { get; set; }

        public static bool playerESP { get; set; }

        public static bool drawPlayerBox { get; set; }

        public static bool chams { get; set; }
        public static bool name { get; set; }
        public static bool skelton { get; set; }
        public static bool health { get; set; }
        public static bool thermal { get; set; }
        public static bool extraction { get; set; }
        public static bool corpse { get; set; }
        public static bool item { get; set; }

        public static bool stamina { get; set; }
        public static bool recoil { get; set; }

        public static bool sway { get; set; }

        public static bool restrictions { get; set; }

        public static bool day { get; set; }
        public static bool fatbullet { get; set; }

        public static bool fastmags { get; set; }

        public static bool itemground { get; set; }






        internal static bool silentAimbot = false;

        internal static bool speedhack = false;


        internal static float speedValue = 1.3f;

        internal static bool FloatOff = false;

        internal static bool flyingHack = false;

        internal static float flyingSpeed = -0.05797062f;

        internal static bool MaxStats = false;

        internal static bool ExperiemntFeature = false;

    }
}
