﻿using Comfort.Common;
using EFT;
using Fika.Core.Networking;
using LiteNetLib.Utils;

namespace Fika.Core.Coop.GameMode
{
    /// <summary>
    /// Currently used to keep track of interactable objects, in the future this will be used to sync reconnects
    /// </summary>
    public class FikaHostWorld : World
    {
        private FikaServer server;
        private NetDataWriter writer;

        protected void Start()
        {
            server = Singleton<FikaServer>.Instance;
            writer = new();
            gameWorld_0 = GetComponent<GameWorld>();
        }

        protected void FixedUpdate()
        {
            int grenadesCount = gameWorld_0.Grenades.Count;
            if (grenadesCount > 0)
            {
                for (int i = 0; i < grenadesCount; i++)
                {
                    Throwable throwable = gameWorld_0.Grenades.GetByIndex(i);
                    gameWorld_0.method_2(throwable);
                }
            }

            int grenadePacketsCount = gameWorld_0.GrenadesCriticalStates.Count;
            if (grenadePacketsCount > 0)
            {
                for (int i = 0; i < grenadePacketsCount; i++)
                {
                    ThrowablePacket packet = new()
                    {
                        Data = gameWorld_0.GrenadesCriticalStates[i]
                    };
                    writer.Reset();
                    server.SendDataToAll(writer, ref packet, LiteNetLib.DeliveryMethod.ReliableOrdered);
                }
            }

            gameWorld_0.GrenadesCriticalStates.Clear();
        }
    }
}
