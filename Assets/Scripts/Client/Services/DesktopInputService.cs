using Server.Services;
using Server.Services.Data;
using UnityEngine;

namespace Client.Services
{
    public class DesktopInputService : IInputService
    {
        private InputData _inputData = new InputData();
        
        public InputData GetInputData()
        {
            _inputData.Pressed = Input.GetMouseButtonDown(0);
            _inputData.Position = Input.mousePosition;
            
            return _inputData;
        }
    }
}