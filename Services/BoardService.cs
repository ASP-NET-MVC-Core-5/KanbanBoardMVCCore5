using KanbanBoardMVCCore5.Models;
using System.Collections.Generic;

namespace KanbanBoardMVCCore5.Services
{
    public class BoardService : IBoard
    {
        private static List<Board> _board;
        public BoardService()
        {
            _board = new List<Board>();
        }
        // Add New Board
        public void AddStory(Board board)
        {
            _board.Add(board);
        }

        // we move the board towards backward position 
        public void Backward(Board board)
        {
            switch (board.Status)
            {
                // if board has Done status means we need to Step backward towards CodeReview
                case Status.Done:
                    // here we change the current status to CodeReview
                    board.Status = Status.CodeReview;
                    break;
                // if board has CodeReview status means we need to Step backward towards Doing
                case Status.Doing:
                    // here we change the current status to Doing
                    board.Status = Status.Doing;
                    break;
                // if board has Doing status means we need to Step forward towards ToDo
                case Status.CodeReview:
                    // here we change the current status to ToDo
                    board.Status = Status.ToDo;
                    break;
                // Then default case
                default:
                    break;
            }
        }

        // we move the board towards forward position 
        public void Forward(Board board)
        {
            // check the current board status
            switch (board.Status)
            {
                // if board has ToDo status mean means we need to Step forward towards Doing
                case Status.ToDo:
                    // here we change the current status to Doing
                    board.Status = Status.Doing;
                    break;
                // if board has Doing status means we need to Step forward towards CodeReview
                case Status.Doing:
                    // here we change the current status to CodeReview
                    board.Status = Status.CodeReview;
                    break;
                // if board has CodeReview status means we need to Step forward towards Done
                case Status.CodeReview:
                    // here we change the current status to Done
                    board.Status = Status.Done;
                    break;
                // Then default case
                default:
                    break;
            }
        }

        public List<Board> GetAllBoardUserStories()
        {
            return _board;
        }

        public Board GetUserStoryById(int id)
        {
            return _board.Find(b => b.Id == id);
        }
    }
}
