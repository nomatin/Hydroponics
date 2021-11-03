using SQLite;
namespace hydroponics.sqlClasses
{
    [Table("Pots")]
    public class ListPots
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdPot { get; set; }
        public int WateringFrequency { get; set; } = 0;
        public int WateringDelay { get; set; } = 0;
        public string StartTime { get; set; } = "";
        public string StopTime { get; set; } = "";
    }
}
