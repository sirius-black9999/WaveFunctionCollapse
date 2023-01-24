
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.ARPG.Items
{
    public interface IItemBase
    {
        Signature Fundament { get; }
    }
    
    public interface IItemBaseMaker<out T>
    {
        public T WithSignature(Signature signature);
        public T WithElement(Element e, float value);
    }
}
