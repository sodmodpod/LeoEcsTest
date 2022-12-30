using Client.Systems;

namespace UnityTemplateProjects.Installers
{
    public class GameEcsSystemsInstaller : BaseEcsSystemsInstaller
    {
        protected override void OnInstallBindings()
        {
            Container.BindSystem<VelocityResetSystem, UpdateSystemsContainer>();
            Container.BindSystem<PositionInitSystem, UpdateSystemsContainer>();
            Container.BindSystem<DestinationPositionInitSystem, UpdateSystemsContainer>();
            Container.BindSystem<RotationInitSystem, UpdateSystemsContainer>();
            
            Container.BindSystem<ButtonToDoorLinkSystem, UpdateSystemsContainer>();

            Container.BindSystem<SetDestinationSystem, UpdateSystemsContainer>();
            Container.BindSystem<SetHasToMoveSystem, UpdateSystemsContainer>();
            Container.BindSystem<MoveSystem, UpdateSystemsContainer>();
            Container.BindSystem<ButtonChangeStateSystem, UpdateSystemsContainer>();
            Container.BindSystem<DoorChangeStateSystem, UpdateSystemsContainer>();
            Container.BindSystem<DoorOpeningSystem, UpdateSystemsContainer>();
            
            Container.BindSystem<AnimatorSystem, UpdateSystemsContainer>();
            
            Container.BindSystem<TransformPositionApplySystem, UpdateSystemsContainer>();
            Container.BindSystem<TransformRotationApplySystem, UpdateSystemsContainer>();
            
            
        }
    }
}