using KanbanBoardMVCCore5.Models;
using System.Collections.Generic;

namespace KanbanBoardMVCCore5.Services
{
    public interface IBoard
    {
        void AddStory(Board board);
        List<Board> GetAllBoardUserStories();
        void Forward(Board board);
        void Backward(Board board);
        Board GetUserStoryById(int id);
    }
}
