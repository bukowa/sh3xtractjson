using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SH3Textractor;

public static class Serializer {
    public class ShouldSerializeContractResolver : DefaultContractResolver {
        public static readonly ShouldSerializeContractResolver Instance =
            new ShouldSerializeContractResolver();

        /// <summary>
        /// Logic for custom serialization of properties like;
        /// - skipping serialization of certain properties.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override JsonProperty CreateProperty
        (
            MemberInfo          member,
            MemberSerialization memberSerialization
        ) {
            var property = base.CreateProperty(member, memberSerialization);

            // e.g. skip serialization of certain properties
            // if( property.DeclaringType == typeof(ModelChunk) &&
            //    property.PropertyName == "VertexIndices" ||
            //    property.PropertyName == "TextureIndices" ||
            //    property.PropertyName == "Vertices" ||
            //    property.PropertyName == "TextureCoordinates" ||
            //    property.PropertyName == "MaterialIndices" ||
            //    property.PropertyName == "Normals" )
            //    
            // {
            // 	property.ShouldSerialize = instance => false;
            // }

            return property;
        }
    }
}