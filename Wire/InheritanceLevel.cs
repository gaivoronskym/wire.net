using Yaapii.Atoms;

namespace Wire;

public sealed class InheritanceLevel(Type derived, Type super) : IScalar<int>
{
    public int Value()
    {
        int level;
        if(super == derived)
        {
            level = 0;
        }
        else
        {
            level = CalculateLevel();
        }
        
        return level;
    }
    
    private int CalculateLevel()
    {
        int level = int.MinValue;
        var sclass = derived.BaseType;
        int idx = 0;
        
        if(sclass is null)
        {
            return level;
        }
        
        while (sclass != typeof(object))
        {
            idx += 1;
            if (sclass is null || sclass == super) {
                level = idx;
                break;
            }
            
            sclass = sclass.BaseType;
        }
        
        return level;
    }
}