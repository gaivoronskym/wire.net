namespace Wire.Props;

public sealed class AppPropsOf(IAppContext context) : PropsEnvelope
(
    () => context.Props("app")
);