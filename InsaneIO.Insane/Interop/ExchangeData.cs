using Newtonsoft.Json;

namespace InsaneIO.Insane.Interop
{
    public class ExchangeData
    {
        public String Data { get; set; } = null!;
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }
        public int E { get; set; }

        public static ExchangeData Create(String json)
        {
            return JsonConvert.DeserializeObject<ExchangeData>(json);
        }

        public override string ToString()
        {
            return Exchange.GetS(Data, A, B, C, D, E);
        }
    }
}
