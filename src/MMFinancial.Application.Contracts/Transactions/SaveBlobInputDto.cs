using System.ComponentModel.DataAnnotations; 
namespace MMFinancial
{  
    public class SaveBlobInputDto{ 
        public byte[] Content{ get; set; } 
        [Required] 
        public string Name{ get; set; } 
}
}