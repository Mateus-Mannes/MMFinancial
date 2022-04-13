using System.ComponentModel.DataAnnotations; 
namespace FileActionsDemo
{  
    public class SaveBlobInputDto{ 
        public byte[] Content{ get; set; } 
        [Required] 
        public string Name{ get; set; } 
}
}