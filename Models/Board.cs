using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanBoardMVCCore5.Models
{
    public enum Status
    {
        ToDo,
        Doing,
        CodeReview,
        Done
    }
    public class Board
    {
        public Board()
        {

        }

        [Key]
        public int Id { get; set; }
        //  This property represents the Identity Table "ASPNETUsers"

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Input needs to be between 2 and 40 characters.")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Input needs to be between 2 and 255 characters.")]
        public string Description { get; set; }
        [Required]
        public int Estimation { get; set; }
        [Required]
        public int Priority { get; set; }
        public Status Status { get; set; }
        public string UserEmail { get; set; }
        public string UserId { get; set; }
    }
}
