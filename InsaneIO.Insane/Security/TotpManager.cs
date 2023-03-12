using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;
using InsaneIO.Insane.Serialization;

namespace InsaneIO.Insane.Security
{
    /// <summary>
    /// Testing codes in the link - https://totp.danhersam.com/
    /// </summary>
    [RequiresPreviewFeatures]
    public class TotpManager : IJsonSerialize
    {
        public required byte[] Secret { get; init; } = null!;

        public required string Label { get; init; } = string.Empty;
        public required string Issuer { get; init; } = string.Empty;
        public TwoFactorCodeLength CodeLength { get; init; } = TwoFactorCodeLength.SixDigits;
        public HashAlgorithm HashAlgorithm { get; init; } = HashAlgorithm.Sha1;
        public uint TimePeriodInSeconds { get; init; } = TotpExtensions.TotpDefaultPeriod;

        private string _name = IBaseSerialize.GetName(typeof(TotpManager));
        public string Name
        {
            get
            {
                return _name;
            }
            init
            {
                if (_name is not null)
                {
                    return;
                }
                _name = value;
            }
        }

        public string Serialize()
        {
            return ToJsonObject().ToJsonString();
        }

        public JsonObject ToJsonObject()
        {
            return new JsonObject
            {
                [nameof(Name)] = Name,
                [nameof(Secret)] = Base32Encoder.DefaultInstance.Encode(Secret),
                [nameof(Label)] = Label,
                [nameof(Issuer)] = Issuer,
                [nameof(CodeLength)] = CodeLength.NumberValue<int>(),
                [nameof(HashAlgorithm)] = HashAlgorithm.NumberValue<int>(),
                [nameof(TimePeriodInSeconds)] = TimePeriodInSeconds,
            };
        }

        public static TotpManager Deserialize(string json)
        {
            JsonNode jsonNode = JsonNode.Parse(json)!;
            return new TotpManager
            {
                Secret = Base32Encoder.DefaultInstance.Decode(jsonNode[nameof(Secret)]!.GetValue<string>()),
                Label = jsonNode[nameof(Label)]!.GetValue<string>(),
                Issuer = jsonNode[nameof(Issuer)]!.GetValue<string>(),
                CodeLength = Enum.Parse<TwoFactorCodeLength>(jsonNode[nameof(CodeLength)]!.GetValue<uint>().ToString()),
                HashAlgorithm = Enum.Parse<HashAlgorithm>(jsonNode[nameof(HashAlgorithm)]!.GetValue<uint>().ToString()),
                TimePeriodInSeconds = jsonNode[nameof(TimePeriodInSeconds)]!.GetValue<uint>()
            };
        }

        public string ToOtpUri()
        {
            return Secret.GenerateTotpUri(Label, Issuer, HashAlgorithm, CodeLength, TimePeriodInSeconds);
        }

        public bool VerifyCode(string code)
        {
            return code.VerifyTotpCode(Secret, CodeLength, HashAlgorithm, TimePeriodInSeconds);
        }

        public bool VerifyCode(string code, DateTimeOffset now)
        {
            return code.VerifyTotpCode(Secret, now, CodeLength, HashAlgorithm, TimePeriodInSeconds);
        }

        public string ComputeCode()
        {
            return Secret.ComputeTotpCode(CodeLength, HashAlgorithm, TimePeriodInSeconds);
        }

        public string ComputeCode(DateTimeOffset now)
        {
            return Secret.ComputeTotpCode(now, CodeLength, HashAlgorithm, TimePeriodInSeconds);
        }

    }
}
