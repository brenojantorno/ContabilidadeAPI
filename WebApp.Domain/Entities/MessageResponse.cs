using System.Collections.Generic;

namespace WebApp.Models
{
    public abstract class MessageResponse
    {
        public bool Success { get; set; }
        public List<string> BusinessValidations { get; set; }
        public MessageResponse(bool success, List<string> businessValidations)
        {
            Success = success;
            BusinessValidations = businessValidations;
        }
    }
}
