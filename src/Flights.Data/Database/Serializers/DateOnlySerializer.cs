using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Flights.Data.Database.Serializers;

public sealed class DateOnlySerializer : StructSerializerBase<DateOnly>
{
    private static readonly DateTimeSerializer Inner =
        new DateTimeSerializer(dateOnly: true);

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateOnly value)
    {
        var dateTime = value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        Inner.Serialize(context, args, dateTime);
    }

    public override DateOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var dateTime = Inner.Deserialize(context, args);
        return DateOnly.FromDateTime(dateTime);
    }
}
