using Client.Systems;
using Systems;
using Leopotam.EcsLite.UnityEditor;
using Server.Components;
using UnityEngine.PlayerLoop;
using UnityTemplateProjects.Systems;

namespace UnityTemplateProjects.Installers
{
    public class GameEcsSystemsInstaller : BaseEcsSystemsInstaller
    {
        protected override void OnInstallBindings()
        {
            
        }

        private void ServerBindings()
        {
            Container.BindSystem<InputToPlayerLinkSystem, UpdateSystemsContainer>();
            Container.BindSystem<ButtonToDoorLinkSystem, UpdateSystemsContainer>();
            
            Container.BindSystem<DestinationSetSystem, UpdateSystemsContainer>();
            Container.BindSystem<MoveSystem, UpdateSystemsContainer>();
            Container.BindSystem<ButtonChangeStateSystem, UpdateSystemsContainer>();
            Container.BindSystem<DoorChangeStateSystem, UpdateSystemsContainer>();
            Container.BindSystem<DoorOpeningSystem, UpdateSystemsContainer>();
        }
        
        private void ClientBindings()
        {
            Container.BindSystem<PositionRotationInitSystem, UpdateSystemsContainer>();
        }
    }
}