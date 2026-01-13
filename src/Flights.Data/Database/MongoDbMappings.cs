using Flights.Data.Models.Notification;
using Flights.Data.Models.Query;
using Flights.Data.Models.Reference;
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
        ConfigureFlightQueryAlert();
        ConfigureFlightQueryPriceDropAlert();
        ConfigureFlightQueryNotification();
        ConfigureFlightQueryPriceDropNotification();
        ConfigureAlertTarget();
        ConfigureTelegramAlertTarget();
        ConfigureResetAlert();
        ConfigureAirport();
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
            cm.MapProperty(x => x.Stops)
                .SetElementName("stops")
                .SetSerializer(new EnumSerializer<Stops>(BsonType.String));
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
            cm.MapProperty(x => x.Provider)
                .SetElementName("provider");
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

    private static void ConfigureFlightQueryAlert()
    {
        BsonClassMap.RegisterClassMap<FlightQueryAlert>(cm =>
        {
            cm.AutoMap();
            cm.SetIsRootClass(true);
            cm.SetDiscriminator("alert");
            cm.MapIdProperty(x => x.Id)
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.MapProperty(x => x.QueryId)
                .SetElementName("queryId")
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.MapProperty(x => x.Targets)
                .SetElementName("targets");
        });
    }

    private static void ConfigureFlightQueryPriceDropAlert()
    {
        BsonClassMap.RegisterClassMap<FlightQueryPriceDropAlert>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator("priceDrop");
            cm.MapProperty(x => x.Reset)
                .SetElementName("reset");
        });
    }

    private static void ConfigureFlightQueryNotification()
    {
        BsonClassMap.RegisterClassMap<FlightQueryNotification>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator("notification");
            cm.MapIdProperty(x => x.Id)
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.MapProperty(x => x.QueryId)
                .SetElementName("queryId")
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.MapProperty(x => x.ResultId)
                .SetElementName("resultId")
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.MapProperty(x => x.NotifiedAt)
                .SetElementName("notifiedAt")
                .SetSerializer(new DateTimeSerializer(DateTimeKind.Utc));
        });
    }

    private static void ConfigureFlightQueryPriceDropNotification()
    {
        BsonClassMap.RegisterClassMap<FlightQueryPriceDropNotification>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator("priceDrop");
            cm.MapProperty(x => x.NotifiedPrice)
                .SetElementName("notifiedPrice");
        });
    }

    private static void ConfigureAlertTarget()
    {
        BsonClassMap.RegisterClassMap<AlertTarget>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator("target");
        });
    }

    private static void ConfigureTelegramAlertTarget()
    {
        BsonClassMap.RegisterClassMap<TelegramAlertTarget>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator("telegram");
            cm.MapProperty(x => x.ChatId)
                .SetElementName("chatId");
        });
    }

    private static void ConfigureResetAlert()
    {
        BsonClassMap.RegisterClassMap<ResetAlert>(cm =>
        {
            cm.AutoMap();
            cm.MapProperty(x => x.Units)
                .SetElementName("units");
            cm.MapProperty(x => x.Type)
                .SetElementName("type")
                .SetSerializer(new EnumSerializer<ResetAlertType>(BsonType.String));
        });
    }

    private static void ConfigureAirport()
    {
        BsonClassMap.RegisterClassMap<Airport>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(x => x.Id)
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.MapProperty(x => x.Code)
                .SetElementName("code");
            cm.MapProperty(x => x.Name)
                .SetElementName("name");
            cm.MapProperty(x => x.City)
                .SetElementName("city");
            cm.MapProperty(x => x.Country)
                .SetElementName("country");
        });
    }
}
