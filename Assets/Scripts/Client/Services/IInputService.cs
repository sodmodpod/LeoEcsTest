using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Client.Services
{
    public class DesktopInputData : IInputService
    {
        private InputData _inputData;
        
        public InputData GetInputData()
        {
            _inputData.Pressed = Input.GetMouseButtonDown(0);
            _inputData.Position = Input.mousePosition;
            
            return _inputData;
        }
    }
    
    public interface IInputService
    {
        public InputData GetInputData();
    }

    public struct InputData
    {
        public bool Pressed;
        public float3 Position;
    }
}