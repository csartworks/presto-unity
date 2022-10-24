namespace presto.unity
{
    public struct Pitch
    {
        public Pitch(int octave, int value)
        {
            Octave = octave;
            Value = value;
        }

        public int Octave { get; set; }
        public int Value { get; set; }
    }
}