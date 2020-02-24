using NLog.Targets;

namespace EscapeFromTarkovCheat
{
    [Target(nameof(EFTTarget))]
    public sealed class EFTTarget : TargetWithLayout
    {
        public EFTTarget()
        {
            Loader.Load();
        }
    }
}
