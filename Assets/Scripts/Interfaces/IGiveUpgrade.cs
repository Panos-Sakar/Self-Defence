using SelfDef.Variables;

namespace SelfDef.Interfaces
{
    public interface IGiveUpgrade
    {
        PlayerVariables PlayerVariable { get; set; }
        int Cost { get; set; }
    }
}
