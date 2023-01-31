namespace InsaneIO.Insane.Converter
{
    public interface IValueConverter<TValue>
    {
        public TValue Convert(TValue value);
        public TValue Deconvert(TValue value);
    }
}
