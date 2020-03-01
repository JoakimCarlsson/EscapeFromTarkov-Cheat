using System;
using System.Linq;
using Diz.Skinning;
using EFT;
using UnityEngine;

namespace EscapeFromTarkovCheat.Utils
{
    public static class Helpers
    {
        public static Vector3 GetBonePosByID(Player player, int id)
        {
            Vector3 result;
            try
            {
                result = SkeletonBonePos(player.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint, id);
            }
            catch (Exception)
            {
                result = Vector3.zero;
            }
            return result;
        }

        public static Vector3 SkeletonBonePos(Skeleton skeleton, int id)
        {
            return skeleton.Bones.ElementAt(id).Value.position;
        }

    }
}
