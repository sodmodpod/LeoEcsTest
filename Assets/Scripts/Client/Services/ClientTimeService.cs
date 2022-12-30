using Server.Services;
using UnityEngine;

namespace Client.Services
{
    public class ClientTimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
    }
}