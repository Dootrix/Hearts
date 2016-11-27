using Hearts.Attributes;

namespace Hearts.Model
{
    public enum Pass
    {
        [Abbreviation("Left")]
        OneToLeft,

        [Abbreviation("Right")]
        OneToRight,

        [Abbreviation("No pass")]
        NoPass,

        [Abbreviation("Two to left")]
        TwoToLeft,

        [Abbreviation("Two to right")]
        TwoToRight
    }
}
