using System.Collections.Generic;
using Server.Services.Data;
using Unity.Mathematics;

namespace Server.Services
{
    public interface IInputService
    {
        public InputData GetInputData();
    }
}