namespace Wire.Props;

public sealed class QualifiersPropsOf : PropsEnvelope
{
    public QualifiersPropsOf(IAppContext context)
        : base(() => context.Props("qualifiers"))
    {
    }
}