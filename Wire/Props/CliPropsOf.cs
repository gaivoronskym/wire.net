namespace Wire.Props;

public sealed class CliPropsOf(IAppContext context) : PropsEnvelope(() => context.Props("cli"));