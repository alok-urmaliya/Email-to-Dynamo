namespace EmailWebhook.Controllers
{
    public class EmailData
    {
        public string subject { get; set; }
        public string body { get; set; }
        public string to { get; set; }
        public string from { get; set; }
        public string date { get; set; }
    }
}