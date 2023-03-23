using System.ComponentModel.DataAnnotations;

namespace Musicals
{
    public class ServiceBusOptions
    {
        [Required]
        public string ConnectionString { get; set; }
        
        [Required]
        public string Topic { get; set; }
    }
}
