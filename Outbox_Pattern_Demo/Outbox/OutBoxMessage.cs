public class OutboxMessage{
    public int Event_Id {get; set;}
    public string Event_Payload {get;set;}
    public DateTime Event_Time {get; set;}
    public bool IsMessageDispatched{ get; set;}
}