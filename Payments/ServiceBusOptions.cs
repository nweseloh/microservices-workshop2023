using System.ComponentModel.DataAnnotations;

namespace Payments
{
    public class ServiceBusOptions
    {
        [Required]
        public string ConnectionString { get; set; }
        
        [Required]
        public string Topic { get; set; }
    }
}
