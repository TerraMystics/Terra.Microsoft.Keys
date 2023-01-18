using Terra.Microsoft.Extensions.StringExt;
using Terra.Microsoft.ProtoBufs.proto.keys;
using Google.Protobuf.WellKnownTypes;

namespace Terra.Microsoft.Keys.Extensions
{
    public static class KeysDtoExtensions
    {
        public static Any PackAny(this KeysDto dto)
        {
            return new Any()
            {
                TypeUrl = dto.TypeUrl,
                Value = TerraStringExtensions.GetBase64BytesFromString(dto.Key)
            };
        }
    }
}
