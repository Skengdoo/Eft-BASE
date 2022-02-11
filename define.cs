using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EFTDLL
{
    public class define
    {
        public static string weaponNameSimple(string weaponName)
        {
            // assult carbines
            if (weaponName.Contains("ADAR 2-15"))
                return "ADAR2-15";
            else if (weaponName.Contains("Kel-Tec RFB"))
                return "Kel-Tec";
            else if (weaponName.Contains("Simonov Semi"))
                return "Semi-Automatic";
            else if (weaponName.Contains("Vepr AKM"))
                return "AKMVPO-209";
            else if (weaponName.Contains("Vepr Hunter"))
                return "HunterVPO-101";
            else if (weaponName.Contains("Vepr KM"))
                return "KMVPO-136";
            // assault rifles
            else if (weaponName.Contains("AK"))
                return "Assault-AK";
            else if (weaponName.Contains("AKM"))
                return "Assualt-AKM";
            else if (weaponName.Contains("AKMN"))
                return "Assualt-AKMN";
            else if (weaponName.Contains("Kalashnikov AKS"))
                return "Kalashnikov-AK";
            // bolt rifle
            else if (weaponName.Contains("DVL-10"))
                return "DVL-10 Rifle";
            else if (weaponName.Contains("Mosin bolt-action"))
                return "BoltAction";
            else if (weaponName.Contains("Orsis T-5000"))
                return "Orsis Rifle";
            else if (weaponName.Contains("Remington Model"))
                return "Remington Rifle";
            else if (weaponName.Contains("SV-98"))
                return "SV-98 Rifle";

            else
                return weaponName;
        }

        public static string extractionNametoSimpleName(string extractionName)
        {
            // Factory
            if (extractionName.Contains("exit (3)"))
                return "Cellars";
            else if (extractionName.Contains("exit (1)"))
                return "Gate 3";
            else if (extractionName.Contains("exit (2)"))
                return "Gate 0";
            else if (extractionName.Contains("exit_scav_gate3"))
                return "Gate 3";
            else if (extractionName.Contains("exit_scav_camer"))
                return "Blinking Light";
            else if (extractionName.Contains("exit_scav_office"))
                return "Office";

            // Woods
            else if (extractionName.Contains("eastg"))
                return "East Gate";
            else if (extractionName.Contains("scavh"))
                return "Scav House";
            else if (extractionName.Contains("deads"))
                return "Dead Mans Place";
            else if (extractionName.Contains("var1_1_constant"))
                return "Outskirts";
            else if (extractionName.Contains("scav_outskirts"))
                return "Outskirts";
            else if (extractionName.Contains("water"))
                return "Outskirts Water";
            else if (extractionName.Contains("boat"))
                return "The Boat";
            else if (extractionName.Contains("exit_scav_mountainstash"))
                return "Mountain Stash";
            else if (extractionName.Contains("oldstation"))
                return "Old Station";
            else if (extractionName.Contains("UNroad"))
                return "UN Road Block";
            else if (extractionName.Contains("gatetofactory"))
                return "Gate to Factory";
            else if (extractionName.Contains("RUAF"))
                return "RUAF Gate";
            else if (extractionName.Contains("westborder"))
                return "West Border";
            else if (extractionName.Contains("exit_trigger"))
                return "";

            // Shoreline
            else if (extractionName.Contains("roadtoc"))
                return "Road to Customs";
            else if (extractionName.Contains("lighthouse"))
                return "Lighthouse";
            else if (extractionName.Contains("tunnel"))
                return "Tunnel";
            else if (extractionName.Contains("wreckedr"))
                return "Wrecked Road";
            else if (extractionName.Contains("deadend"))
                return "Dead End";
            else if (extractionName.Contains("housefence"))
                return "Ruined House Fence";
            else if (extractionName.Contains("gyment"))
                return "Gym Entrance";
            else if (extractionName.Contains("southfence"))
                return "South Fence Passage";
            else if (extractionName.Contains("adm_base"))
                return "Admin Basement";

            // Customs
            else if (extractionName.Contains("administrationg"))
                return "Administration Gate";
            else if (extractionName.Contains("factoryfar"))
                return "Factory Far Corner";
            else if (extractionName.Contains("oldazs"))
                return "Old Gate";
            else if (extractionName.Contains("milkp_sh"))
                return "Shack";
            else if (extractionName.Contains("beyondfuel"))
                return "Beyond Fuel Tank";
            else if (extractionName.Contains("railroadtom"))
                return "Railroad to Mil Base";
            else if (extractionName.Contains("_pay_car"))
                return "V-Exit";
            else if (extractionName.Contains("oldroadgate"))
                return "Old Road Gate";
            else if (extractionName.Contains("sniperroad"))
                return "Sniper Road Block";
            else if (extractionName.Contains("warehouse17"))
                return "Warehouse 17";
            else if (extractionName.Contains("factoryshacks"))
                return "Factory Shacks";
            else if (extractionName.Contains("railroadtotarkov"))
                return "Railroad to Tarkov";
            else if (extractionName.Contains("trailerpark"))
                return "Trailer Park";
            else if (extractionName.Contains("crossroads"))
                return "Crossroads";
            else if (extractionName.Contains("railroadtoport"))
                return "Railroad to Port";


            else if (extractionName.Contains("NW_Exfil"))
                return "North West Extract";
            else if (extractionName.Contains("SE_Exfil"))
                return "South East Extract";
            else
                return extractionName;
        }
        public static Vector3 SkeletonBonePos(Diz.Skinning.Skeleton sko, int id)
        {
            return sko.Bones.ElementAt(id).Value.position;
        }

        public static Vector3 GetBonePosByID(EFT.Player p, int id)
        {
            Vector3 result;
            try
            {
                result = SkeletonBonePos(p.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<EFT.PlayerBody>().SkeletonRootJoint, id);
            }
            catch (Exception)
            {
                result = Vector3.zero;
            }
            return result;
        }
    }
}
