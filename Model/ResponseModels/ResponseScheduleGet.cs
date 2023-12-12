namespace ToDo_Planning.Model;

public class ResponseScheduleGet
{
    public int? TaskID { get; set; }
    public string? Title { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}