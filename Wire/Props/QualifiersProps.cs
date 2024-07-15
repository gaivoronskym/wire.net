﻿using Yaapii.Atoms;
using Yaapii.Atoms.IO;

namespace Wire.Props;

public sealed class QualifiersProps : PropsEnvelope
{
    public QualifiersProps()
        : this(new InputOf(new Url("qualifiers.xml")))
    {
    }

    public QualifiersProps(IInput input)
        : base(() => new XmlProps(input))
    {
    }
}