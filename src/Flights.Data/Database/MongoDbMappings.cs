using Flights.Data.Models.Notification;
using Flights.Data.Models.Query;
using Flights.Data.Models.Result;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Flights.Data.Database;

public static class MongoDbMappings
{
    public static void ConfigureMappings()
    {
        ConfigureFlightQuery();
        ConfigureFlightQuerySegment();
        ConfigureQueryLocation();
        ConfigureFlightQueryResult();
        ConfigureFlightSolution();
        ConfigureFlightSlice();
        ConfigureResultLocation();
        ConfigureFlightQueryNotification();
    }

    private static void ConfigureFlightQuery()
    {
        BsonClassMap.RegisterClassMap<FlightQuery>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(x => x.Id)
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.MapProperty(x => x.Type)
                .SetElementName("type")
                .SetSerializer(new EnumSerializer<FlightQueryType>(BsonType.String));
            cm.MapProperty(x => x.Disabled)
                .SetElementName("disabled");
            cm.MapProperty(x => x.Segments)
                .SetElementName("segments");
        });
    }

    private static void ConfigureFlightQuerySegment()
    {
        BsonClassMap.RegisterClassMap<FlightQuerySegment>(cm =>
        {
            cm.AutoMap();
            cm.MapProperty(x => x.Origin)
                .SetElementName("origin");
            cm.MapProperty(x => x.Destination)
                .SetElementName("destination");
            cm.MapProperty(x => x.Start)
                .SetElementName("start")
                .SetSerializer(new DateOnlySerializer());
            cm.MapProperty(x => x.End)
                .SetElementName("end")
                .SetSerializer(new NullableSerializer<DateOnly>(new DateOnlySerializer()));
            cm.MapProperty(x => x.Days)
                .SetElementName("days")
                .SetSerializer(new EnumSerializer<FlightQueryDays>(BsonType.String));
        });
    }

    private static void ConfigureQueryLocation()
    {
        BsonClassMap.RegisterClassMap<Models.Query.Location>(cm =>
        {
            cm.AutoMap();
            cm.MapProperty(x => x.Code)
                .SetElementName("code");
            cm.MapProperty(x => x.City)
                .SetElementName("city");
            cm.MapProperty(x => x.Country)
                .SetElementName("country");
        });
    }

    private static void ConfigureFlightQueryResult()
    {
        BsonClassMap.RegisterClassMap<FlightQueryResult>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(x => x.Id)
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.MapProperty(x => x.QueryId)
                .SetElementName("queryId")
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.MapProperty(x => x.Solutions)
                .SetElementName("solutions");
            cm.MapProperty(x => x.TotalSolutionCount)
                .SetElementName("totalSolutionCount");
            cm.MapProperty(x => x.SearchedAt)
                .SetElementName("searchedAt")
                .SetSerializer(new DateTimeSerializer(DateTimeKind.Utc));
        });
    }

    private static void ConfigureFlightSolution()
    {
        BsonClassMap.RegisterClassMap<FlightSolution>(cm =>
        {
            cm.AutoMap();
            cm.MapProperty(x => x.Id)
                .SetElementName("id");
            cm.MapProperty(x => x.Price)
                .SetElementName("price");
            cm.MapProperty(x => x.PassengerCount)
                .SetElementName("passengerCount");
            cm.MapProperty(x => x.Slices)
                .SetElementName("slices");
        });
    }

    private static void ConfigureFlightSlice()
    {
        BsonClassMap.RegisterClassMap<FlightSlice>(cm =>
        {
            cm.AutoMap();
            cm.MapProperty(x => x.Origin)
                .SetElementName("origin");
            cm.MapProperty(x => x.Destination)
                .SetElementName("destination");
            cm.MapProperty(x => x.DepartureTime)
                .SetElementName("departureTime")
                .SetSerializer(new DateTimeSerializer(DateTimeKind.Utc));
            cm.MapProperty(x => x.ArrivalTime)
                .SetElementName("arrivalTime")
                .SetSerializer(new DateTimeSerializer(DateTimeKind.Utc));
            cm.MapProperty(x => x.DurationMinutes)
                .SetElementName("durationMinutes");
            cm.MapProperty(x => x.StopCount)
                .SetElementName("stopCount");
            cm.MapProperty(x => x.FlightNumbers)
                .SetElementName("flightNumbers");
            cm.MapProperty(x => x.Cabins)
                .SetElementName("cabins");
        });
    }

    private static void ConfigureResultLocation()
    {
        BsonClassMap.RegisterClassMap<Models.Result.Location>(cm =>
        {
            cm.AutoMap();
            cm.MapProperty(x => x.Code)
                .SetElementName("code");
            cm.MapProperty(x => x.Name)
                .SetElementName("name");
        });
    }

    private static void ConfigureFlightQueryNotification()
    {
        BsonClassMap.RegisterClassMap<FlightQueryNotification>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(x => x.Id)
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.MapProperty(x => x.QueryId)
                .SetElementName("queryId")
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.MapProperty(x => x.LastNotifiedPrice)
                .SetElementName("lastNotifiedPrice");
            cm.MapProperty(x => x.LastResultId)
                .SetElementName("lastResultId")
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.MapProperty(x => x.LastNotifiedAt)
                .SetElementName("lastNotifiedAt")
                .SetSerializer(new DateTimeSerializer(DateTimeKind.Utc));
        });
    }
}
