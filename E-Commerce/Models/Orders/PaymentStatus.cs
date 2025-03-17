using System.Text.Json.Serialization;

namespace e_comm.Models.Orders
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Pending = 1,
        Completed = 2,
        Failed = 3
    }
}
